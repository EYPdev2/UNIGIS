using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActualizadorDoctosUnigis.Contoller
{
    class Libreria
    {
        Qrys Q = new Qrys();
        static readonly HttpClient clienteUnigis = new HttpClient();

        const string ServicioUnigis = "https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx";

        // Llamada JSON a un metodo del servicio de Unigis; regresa lo que trae "d" (null si no trae nada)
        static JToken LlamarUnigis(string metodo, string cuerpoJson)
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            HttpResponseMessage respuesta = clienteUnigis.PostAsync(ServicioUnigis + "/" + metodo, new StringContent(cuerpoJson, Encoding.UTF8, "application/json")).Result;
            string contenido = respuesta.Content.ReadAsStringAsync().Result;
            if (!respuesta.IsSuccessStatusCode)
                throw new Exception("Unigis respondió " + (int)respuesta.StatusCode + " " + respuesta.ReasonPhrase);

            JToken d = JObject.Parse(contenido)["d"];
            return d == null || d.Type == JTokenType.Null ? null : d;
        }

        public class OrdenUnigis
        {
            public int IdPedido;
            public string Estado = "";
            public string Operacion = "";
            public DateTime? FechaEntrega;
        }

        // Orden de pedido en Unigis por referencia (FC-XXXX). Estados vistos: Inicial, Planificar, Planificado, En transito,
        // Entregado, No entregado, Entrega parcial. Regresa null si no existe y lanza excepcion si no se pudo consultar
        public static OrdenUnigis ConsultarOrdenUnigis(string referencia)
        {
            JToken orden = LlamarUnigis("ConsultarOrdenPedido", JsonConvert.SerializeObject(new { ApiKey = "1234", ReferenciaExterna = referencia }));
            if (orden == null)
                return null;

            return new OrdenUnigis
            {
                IdPedido = (int?)orden["IdPedido"] ?? 0,
                Estado = ((string)orden["Estado"] ?? "").Trim(),
                Operacion = ((string)orden["CodigoOperacion"] ?? "").Trim(),
                FechaEntrega = (DateTime?)orden["FechaEntrega"]
            };
        }

        // Elimina la orden de pedido en Unigis (EliminarOrdenesPedido no borra los maestros de clientes y domicilios)
        public static void EliminarOrdenUnigis(string referencia, int idPedido)
        {
            LlamarUnigisSoap("EliminarOrdenesPedido", "<pedidos><pOrdenPedido>" +
                "<RefDocumento>" + WebUtility.HtmlEncode(referencia) + "</RefDocumento><IdPedido>" + idPedido + "</IdPedido>" +
                "</pOrdenPedido></pedidos>");
        }

        public class OrdenEntregaUnigis
        {
            public int IdOrden;
            public int IdJornada;
            public int IdRuta;
            public string Estado = "";
        }

        // Orden de entrega que Unigis crea al consolidar el pedido, dentro de una jornada: "FC-XXXX|01" en entregas y
        // "PP-XXXX|00" en recolecciones (Pick Up). IdRuta es el numero de ruta dentro de la jornada y vale 0 mientras no esta
        // ruteada; el estado no sirve para saberlo (hay ruteadas en Inicial). Estados vistos: Inicial, Documentado, En transito,
        // Entregado, No Entregado, Confirmada. Regresa la mas reciente: si el documento se reenvia y lo vuelven a rutear, la
        // nueva; si no, la de la ruta anterior, ya terminada. Regresa null si el pedido no se ha consolidado
        public static OrdenEntregaUnigis ConsultarOrdenEntregaUnigis(string referencia)
        {
            JToken orden = null;
            foreach (string sufijo in new[] { "|01", "|00" })
            {
                orden = LlamarUnigis("ConsultarOrdenEntrega", JsonConvert.SerializeObject(new { ApiKey = "1234", RefDocumento = referencia + sufijo, Sucursal = "", Operacion = "", TipoOperacion = "" }));
                if (orden != null)
                    break;
            }
            if (orden == null)
                return null;

            JToken estado = orden["EstadoOrden"];
            return new OrdenEntregaUnigis
            {
                IdOrden = (int?)orden["IdOrden"] ?? 0,
                IdJornada = (int?)orden["IdJornada"] ?? 0,
                IdRuta = (int?)orden["IdRuta"] ?? 0,
                Estado = estado != null && estado.Type == JTokenType.Object ? ((string)estado["Descripcion"] ?? "").Trim() : ""
            };
        }

        // Llamada SOAP. Unigis contesta 1 aunque no haya eliminado nada (tambien con una referencia que no existe), asi que
        // quien llama debe volver a consultar para saber si se elimino
        static void LlamarUnigisSoap(string metodo, string parametrosXml)
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            string xml =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"><soap:Body>" +
                "<" + metodo + " xmlns=\"http://unisolutions.com.ar/\"><apiKey>1234</apiKey>" + parametrosXml + "</" + metodo + ">" +
                "</soap:Body></soap:Envelope>";

            HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Post, ServicioUnigis);
            solicitud.Headers.Add("SOAPAction", "\"http://unisolutions.com.ar/" + metodo + "\"");
            solicitud.Content = new StringContent(xml, Encoding.UTF8, "text/xml");
            HttpResponseMessage respuesta = clienteUnigis.SendAsync(solicitud).Result;
            string contenido = respuesta.Content.ReadAsStringAsync().Result;
            if (!respuesta.IsSuccessStatusCode)
                throw new Exception(ErrorSoap(contenido) ?? ("Unigis respondió " + (int)respuesta.StatusCode + " " + respuesta.ReasonPhrase));
        }

        // Busca si una orden ya esta en una ruta de Unigis de su deposito. ObtenerRutas regresa las rutas desde que se arman
        // (Planificacion, Confirmada, Documentado, En transito, Finalizada). Guarda lo consultado para no repetirlo
        public class RutasUnigis
        {
            Dictionary<DateTime, List<JToken>> rutasPorDia = new Dictionary<DateTime, List<JToken>>();
            Dictionary<int, Tuple<string, HashSet<string>>> ordenesPorRuta = new Dictionary<int, Tuple<string, HashSet<string>>>();

            // Revisa las rutas del dia de entrega, de hoy y de manana. Regresa "ruta 71588 (Confirmada)" o null si no esta en ninguna.
            // Con ignorarFinalizadas no cuentan las rutas que ya terminaron
            public string RutaDeOrden(OrdenUnigis orden, string referencia, bool ignorarFinalizadas = false)
            {
                List<DateTime> dias = new List<DateTime>();
                if (orden.FechaEntrega != null)
                    dias.Add(orden.FechaEntrega.Value.ToLocalTime().Date);
                dias.Add(DateTime.Today);
                dias.Add(DateTime.Today.AddDays(1));

                foreach (DateTime dia in dias.Distinct())
                {
                    foreach (JToken ruta in RutasDelDia(dia))
                    {
                        // DepositoSalida viene como objeto (pDeposito); su clave es RefDepositoExterno
                        JToken deposito = ruta["DepositoSalida"];
                        string refDeposito = deposito != null && deposito.Type == JTokenType.Object ? ((string)deposito["RefDepositoExterno"] ?? "") : "";
                        string operacion = ruta["Operacion"] != null && ruta["Operacion"].Type == JTokenType.String ? (string)ruta["Operacion"] : "";
                        if (operacion != orden.Operacion && refDeposito != orden.Operacion)
                            continue;

                        Tuple<string, HashSet<string>> detalle = OrdenesDeRuta(ruta);
                        if (ignorarFinalizadas && string.Equals(detalle.Item1, "Finalizada", StringComparison.OrdinalIgnoreCase))
                            continue;
                        if (detalle.Item2.Contains(referencia))
                            return "ruta " + (int)ruta["IdRuta"] + " (" + detalle.Item1 + ")";
                    }
                }
                return null;
            }

            List<JToken> RutasDelDia(DateTime dia)
            {
                List<JToken> rutas;
                if (!rutasPorDia.TryGetValue(dia, out rutas))
                {
                    long ms = (long)(dia.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
                    JToken d = LlamarUnigis("ObtenerRutas", "{\"apiKey\":\"1234\",\"fecha\":\"\\/Date(" + ms + ")\\/\",\"CodigoSucursal\":\"\"}");
                    rutas = d == null ? new List<JToken>() : d.Children().ToList();
                    rutasPorDia[dia] = rutas;
                }
                return rutas;
            }

            // Estado de la ruta y referencias de sus ordenes (en la ruta vienen como "RC-SAA131478|01")
            Tuple<string, HashSet<string>> OrdenesDeRuta(JToken ruta)
            {
                int idRuta = (int)ruta["IdRuta"];
                Tuple<string, HashSet<string>> detalle;
                if (!ordenesPorRuta.TryGetValue(idRuta, out detalle))
                {
                    // Con IdJornada 0 Unigis falla; hay que mandarlo vacio
                    int idJornada = (int?)ruta["IdJornada"] ?? 0;
                    JToken d = LlamarUnigis("ObtenerRutaCompleta", JsonConvert.SerializeObject(new { ApiKey = "1234", IdJornada = idJornada == 0 ? (int?)null : idJornada, IdRuta = idRuta }));

                    HashSet<string> referencias = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    string estado = d == null ? "" : ((string)d["Estado"] ?? "");
                    if (d != null && d["Ordenes"] != null && d["Ordenes"].Type == JTokenType.Array)
                    {
                        foreach (JToken o in d["Ordenes"])
                            referencias.Add(((string)o["RefDocumento"] ?? "").Split('|')[0].Trim());
                    }
                    detalle = Tuple.Create(estado, referencias);
                    ordenesPorRuta[idRuta] = detalle;
                }
                return detalle;
            }
        }

        // Pantallas que muestran un renglon por documento (Envio y Cancelar Documentos): toma los datos del primer producto
        // de cada documento, quita las columnas de producto y agrega la casilla Enviar y la columna Productos ("N de M")
        public static DataTable AgruparPorDocumento(DataTable productos, string columnaDocumento, string[] columnasProducto, Dictionary<string, List<DataRow>> productosPorDocumento)
        {
            productosPorDocumento.Clear();
            DataTable docs = productos.Clone();
            foreach (string columna in columnasProducto)
            {
                if (docs.Columns.Contains(columna))
                    docs.Columns.Remove(columna);
            }
            docs.Columns.Add("Enviar", typeof(bool)).SetOrdinal(0);
            docs.Columns.Add("Productos", typeof(string)).SetOrdinal(docs.Columns[columnaDocumento].Ordinal + 1);

            foreach (DataRow producto in productos.Rows)
            {
                string documento = producto[columnaDocumento].ToString();
                List<DataRow> lista;
                if (!productosPorDocumento.TryGetValue(documento, out lista))
                {
                    lista = new List<DataRow>();
                    productosPorDocumento[documento] = lista;

                    DataRow doc = docs.NewRow();
                    foreach (DataColumn col in docs.Columns)
                    {
                        if (productos.Columns.Contains(col.ColumnName))
                            doc[col.ColumnName] = producto[col.ColumnName];
                    }
                    doc["Enviar"] = false;
                    docs.Rows.Add(doc);
                }
                lista.Add(producto);
            }
            return docs;
        }

        // Liga los documentos a la cuadricula, muestra la cuenta de productos como boton y deja editable solo la casilla Enviar
        public static void MostrarDocumentosEnCuadricula(DataGridView grid, DataTable docs)
        {
            grid.DataSource = null;
            grid.Columns.Clear();
            grid.DataSource = docs;

            DataGridViewColumn cuenta = grid.Columns["Productos"];
            int posicion = cuenta.Index;
            grid.Columns.Remove(cuenta);
            DataGridViewButtonColumn boton = new DataGridViewButtonColumn();
            boton.Name = "Productos";
            boton.HeaderText = "Productos";
            boton.DataPropertyName = "Productos";
            grid.Columns.Insert(posicion, boton);

            foreach (DataGridViewColumn col in grid.Columns)
                col.ReadOnly = col.Name != "Enviar";
        }

        // Carga en segundo plano para las pantallas con cuadricula, en lugar de un BackgroundWorker. El hilo del pool que usa el
        // BackgroundWorker se queda con una ventana IME al avisar a la pantalla que termino y ya no atiende mensajes; la siguiente
        // vez que la cuadricula oculta su barra de desplazamiento Windows espera esa ventana ~20 s y la pantalla se congelaba al
        // refrescar. Aqui el aviso sale de un hilo propio que termina enseguida, y su ventana IME se destruye con el.
        public static void EnSegundoPlano<T>(Control pantalla, Func<T> trabajo, Action<T, Exception> alTerminar)
        {
            Thread hilo = new Thread(() =>
            {
                T resultado = default(T);
                Exception error = null;
                try { resultado = trabajo(); }
                catch (Exception ex) { error = ex; }

                try { pantalla.BeginInvoke(new Action(() => alTerminar(resultado, error))); }
                catch (InvalidOperationException) { }   // la pantalla se cerro mientras se consultaba
            });
            hilo.IsBackground = true;
            hilo.Start();
        }

        // Para CurrentCellDirtyStateChanged: la casilla se aplica al momento, sin tener que salir de la celda
        public static void AplicarCasillaAlMomento(object sender, EventArgs e)
        {
            DataGridView grid = (DataGridView)sender;
            if (grid.IsCurrentCellDirty && grid.CurrentCell is DataGridViewCheckBoxCell)
                grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        // Misma regla que Validar Paradas: el usuario debe tener asignado el establecimiento del deposito
        public bool UsuarioTieneDeposito(string usuario, string deposito)
        {
            DataTable du = Q.Consultar("select isnull(es.Unigis,'') from establecimientos_usuario eu " +
                                       "left join usuarios u on eu.usuario = u.usuario " +
                                       "left join Eyp_Servidores es on es.Cod_estab=eu.cod_estab " +
                                       " Where u.usuario = '" + usuario + "' and(u.multi_establecimiento = 1 or u.cod_estab = eu.cod_estab) and acceso = '1'");
            foreach (DataRow row in du.Rows)
            {
                if (row[0].ToString().Trim() == deposito.Trim())
                    return true;
            }
            return false;
        }

        public static string MensajeError(Exception ex)
        {
            Exception origen = ex;
            while (origen.InnerException != null)
                origen = origen.InnerException;
            if (origen is TaskCanceledException)
                return "Unigis no respondió a tiempo";
            return origen.Message;
        }

        public static string ErrorSoap(string contenido)
        {
            Match falla = Regex.Match(contenido ?? "", "<faultstring[^>]*>(.*?)</faultstring>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (!falla.Success)
                return null;

            string texto = WebUtility.HtmlDecode(falla.Groups[1].Value).Trim();
            return texto.Length > 300 ? texto.Substring(0, 300) + "..." : texto;
        }

        public string Decimales(string cantidad, string producto)
        {
            DataTable decimales = Q.Consultar(" select decimales from productos where cod_prod='" + producto + "'");
            int NDecimal = Convert.ToInt32(decimales.Rows[0][0].ToString());
            if (cantidad.ToString().IndexOf('.') > -1)
            {
                int idx = cantidad.IndexOf('.');
                int idx2 = cantidad.Length;
                if (NDecimal >= cantidad.Substring(idx, cantidad.Length - idx).Length)
                {
                    if (idx + 1 - idx2 == 0)
                    {
                        return cantidad.Substring(0, idx) + ".00";
                    }
                    else if(cantidad.Substring(idx, cantidad.Length - idx).Length>0)
                    {

                        //MessageBox.Show(cantidad);
                        return cantidad ;
                    }
                    else
                    {
                        return cantidad + ".00";
                    }
                }
                else
                {
                    return cantidad;
                }

            }
            else { return cantidad + ".00"; }

        }
        public string folio(string referencia_ext, int modo)
        {
            int idx = referencia_ext.IndexOf('-');
            if (modo == 0)
            {
                string s = referencia_ext.Substring(idx + 1);
                int idx2 = referencia_ext.Substring(idx + 1).IndexOf('|');
                s = s.Substring(0, idx2);
                return s;
            }
            else
            {
                string tran = referencia_ext.Substring(0, idx);
                switch (referencia_ext.Substring(0, idx))
                {
                    case "EdMaC": tran = "713"; break;
                    case "FC": tran = "36"; break;
                    case "RC": tran = "37"; break;
                    case "TE": tran = "35";break;

                    default:tran =""; break;

                        //case "PE":tran = "29";break;
                }
                return tran;
            }




        }

        public bool validaEstab(string usuario, string Deposito)
        {
            DataTable dtUsuario = Q.Consultar("select cod_estab from establecimientos_usuario where usuario='" + usuario + "' and acceso='1'");
            DataTable dtDeposito = Q.Consultar(" select cod_estab  from Eyp_Servidores eyp where  unigis='18'");

            foreach (DataRow row in dtUsuario.Rows)
            {
                if (row[0].ToString() == "18")
                {
                    return true;
                }

            }

            return false;
        }
        public DataTable Depositos_usuario(string usuario)
        {
            DataTable dtUsuario = Q.Consultar(" select unigis from Eyp_Servidores e " +
            " left join establecimientos_usuario eu on e.Cod_estab = eu.cod_estab" +
                " where eu.usuario = '" + usuario + "' and e.unigis is not null ");
            return dtUsuario;

        }

        public DataTable Conexiones(string folio)
        {
            DataTable dt = Q.Consultar("select e.cod_estab,eyp.servidor,base_datos from establecimientos e " +
                                       "left join Eyp_Servidores eyp on e.cod_estab = eyp.Cod_estab " +
                                       "where e.abreviatura = '" + folio.Substring(0, 3) + "'");
            if (dt.Rows.Count == 0)
            {
                dt = Q.Consultar("select e.cod_estab,eyp.servidor,base_datos from establecimientos e " +
                                       "left join Eyp_Servidores eyp on e.cod_estab = eyp.Cod_estab " +
                                       "where e.abreviatura = '" + folio.Substring(0, 2) + "'");
            }



            return dt;
        }

    }
}
