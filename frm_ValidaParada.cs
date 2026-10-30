using ActualizadorDoctosUnigis.Contoller;
using ActualizadorDoctosUnigis.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace ActualizadorDoctosUnigis
{
    public partial class frm_ValidaParada : Form
    { Qrys q = new Qrys();
        ConsultarParadaPorIdResponse cpr = new ConsultarParadaPorIdResponse();
        List <ConsultarParadaPorIdResponse> Lcpr= new List< ConsultarParadaPorIdResponse > () ;
        DataTable dtg = new DataTable();
        ObtenerViaje_Response.Rootobject ov = new ObtenerViaje_Response.Rootobject();
        Embarque e = new Embarque();
        Libreria lib = new Libreria();
        string cod_estab="";
        string usuario;
        public frm_ValidaParada()
        {
            InitializeComponent();
        }
        public frm_ValidaParada(string u )
        {
            usuario = u;
            InitializeComponent();
        }

        private void txt_viaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_viaje_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode.ToString() == "F2")
            {
                GridBusqueda gb = null;
                try
                {
                    gb = new GridBusqueda(txt_viaje,usuario);
                    gb.Show();
                }
                catch (Exception ex)
                {
                    RegistrarLog("Error al abrir el buscador", ex.ToString());
                    if (gb != null)
                        gb.Dispose();
                    Error("Ha ocurrido un error que no ha permitido buscar los viajes. Intente de nuevo.");
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_viaje.Text != "")
                {
                    CargarViajeEnPantalla();
                }
                else
                {
                    Aviso("Por favor proporcione el número de viaje.");
                }
            }
                
            }
     

            private void frm_ValidaParada_Load(object sender, EventArgs e)
                    {

            DataColumn col1 = new DataColumn("IdParada", typeof(System.String));
            dtg.Columns.Add(col1);
            DataColumn col2 = new DataColumn("Ref.Documento", typeof(System.String));
            dtg.Columns.Add(col2);
            DataColumn col = new DataColumn("Producto", typeof(System.String));
            dtg.Columns.Add(col);
            DataColumn col3 = new DataColumn("Cantidad", typeof(System.String));
            dtg.Columns.Add(col3);
            DataColumn col5 = new DataColumn("Cantidad_Entregada", typeof(System.String));
            dtg.Columns.Add(col5);
            DataColumn col4 = new DataColumn("Estatus", typeof(System.String));
            dtg.Columns.Add(col4);

            try
            {
                if (Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "TOPF3").ToString() != "")
                {
                    this.Top = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "TOPF3").ToString());
                    this.Left = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "LeftF3").ToString());
                    this.Width = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "WidthF3").ToString());
                    this.Height = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "HeightF3").ToString());
                }
            }
            catch (Exception)
            {
            }



        }
        //Metodo que realiza una consulta a la web api de unigis para conusltar inoformacion de las paradas por medio de el id de viaje
        public bool ObtenerParadas(string idviaje)
        {
            int numeroViaje;
            if (!int.TryParse(idviaje, out numeroViaje))
            {
                Aviso("El número de viaje no es válido.");
                return false;
            }
            if (!consultarviaje(numeroViaje))
            {
                AvisarViajeNoConsultado();
                return false;
            }
            // Sin depósito no se revisa aquí; al liberar lo detiene ValidarDatosDelViaje
            if (!string.IsNullOrWhiteSpace(cod_estab) && !UsuarioPuedeLiberar(numeroViaje.ToString()))
                return false;
            return CargarParadasEnGrid(numeroViaje.ToString());
        }

        // Misma regla que Validar Paradas: el usuario debe tener asignado el depósito del viaje
        private bool UsuarioPuedeLiberar(string idviaje)
        {
            if (lib.UsuarioTieneDeposito(usuario, cod_estab))
                return true;
            Aviso("Solo se pueden liberar viajes de los depósitos asignados al usuario. El viaje " + idviaje + " es del depósito " + cod_estab.Trim() + ".");
            return false;
        }

        public bool ObtenerParadasParaLiberar(string idviaje)
        {
            return CargarParadasEnGrid(idviaje);
        }

        private bool CargarParadasEnGrid(string idviaje)
        {
            DataTable paradas = PrepararCargaParadas(idviaje);
            foreach (DataRow row1 in (InternalDataCollectionBase)paradas.Rows)
            {
                string idParada = "" + row1.ItemArray[0];
                try
                {
                    idParada = Convert.ToInt32(row1.ItemArray[0].ToString()).ToString();
                    if (!CargarParada(row1, Convert.ToInt32(idviaje)))
                        return false;
                }
                catch (Exception ex)
                {
                    RegistrarLog("Error al cargar la parada", "IdParada " + idParada + " | " + ex.ToString());
                    throw;
                }
            }
            return true;
        }

        private DataTable PrepararCargaParadas(string idviaje)
        {
            this.dtg.Clear();
            this.Lcpr.Clear();
            this.txt_viaje.Enabled = false;
            this.btn_canc.Enabled = true;
            DataTable paradas = this.q.ParadasViaje(idviaje);
            return paradas;
        }

        // Regresa false si la parada no tiene estado de entrega en este viaje (ya se avisó)
        private bool CargarParada(DataRow row1, int numeroViaje)
        {
            this.button1.Enabled = false;
            obtenerParadas_request obtenerParadasRequest = new obtenerParadas_request();
            obtenerParadasRequest.ApiKey = "1234";
            obtenerParadasRequest.IdParada = Convert.ToInt32(row1.ItemArray[0].ToString());
            string content1 = JsonConvert.SerializeObject((object)obtenerParadasRequest);
            using (HttpClient httpClient = new HttpClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                Uri requestUri = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx/ConsultarParadaPorId");
                StringContent content2 = new StringContent(content1, Encoding.UTF8, "application/json");
                this.cpr = JsonConvert.DeserializeObject<ConsultarParadaPorIdResponse>(httpClient.PostAsync(requestUri, (HttpContent)content2).Result.Content.ReadAsStringAsync().Result);
                if (this.cpr.d.IdViaje != numeroViaje)
                {
                    RegistrarLog("Parada de otro viaje omitida", "parada " + row1.ItemArray[0] + " | viaje actual " + this.cpr.d.IdViaje);
                    this.button1.Enabled = true;
                    return true;
                }

                // El estado de entrega es el último de la parada aunque sea de otro viaje: una parada No Entregado que movieron
                // a otro viaje y ahí la validaron sin nueva entrega conserva su No Entregado
                string estadoParada = ObtenerParadaestado(obtenerParadasRequest.IdParada.ToString());
                if (estadoParada == null)
                {
                    Aviso("El estado de la parada " + obtenerParadasRequest.IdParada + " no ha sido enviado por UNIGIS.");
                    return false;
                }
                estadoParada = estadoParada.Trim();

                this.Lcpr.Add(this.cpr);
                this.button1.Enabled = true;
                foreach (ConsultarParadaPorIdResponse.Items items in this.cpr.d.Items)
                {
                    DataRow row2 = this.dtg.NewRow();
                    row2["IdParada"] = (object)row1.ItemArray[0].ToString();
                    row2["Ref.Documento"] = (object)this.cpr.d.RefDocumento;
                    row2["Producto"] = (object)items.RefDocumento;
                    row2["Estatus"] = (object)this.cpr.d.EstadoParada;
                    if (estadoParada != "Entrega parcial")
                    {
                        row2["Cantidad"] = items.CantidadEntregada != 0 ? (items.CantidadEntregada + items.Cantidad) : items.Cantidad;

                        if (estadoParada == "Entregado")
                        {
                            row2["Cantidad_Entregada"] = row2["Cantidad"];
                        }
                        else
                        {
                            row2["Cantidad_Entregada"] = 0;
                        }
                    }
                    else
                    {
                        row2["Cantidad"] = items.Cantidad + items.CantidadEntregada;
                        row2["Cantidad_Entregada"] = items.CantidadEntregada;
                    }
                    this.dtg.Rows.Add(row2);
                }
                this.dataGridView1.DataSource = (object)this.dtg;
                this.dataGridView1.Columns["Cantidad"].Width = 60;
                this.dataGridView1.Columns["Producto"].Width = 60;
            }
            return true;
        }


        public string ObtenerParadaestado(string idparada)
        {
            string sql = "select top 1 Estado ,id from EYP_Unis_parada where IdParada='"+idparada+ "' and Estado in ('Entregado','No Entregado', 'Entrega Parcial') order by id desc";
            DataTable de = q.Consultar(sql);
            if (de.Rows.Count == 0)
                return null;
            return de.Rows[0].ItemArray[0].ToString();
        }

        public string obtener_estab(string deposito)
        {
            string sql = "select rtrim(Cod_estab) from eyp_servidores where unigis='" + deposito + "'";
            DataTable de = q.Consultar(sql);
            if (de.Rows.Count == 0)
                return null;
            return de.Rows[0].ItemArray[0].ToString();
        }

        private void Aviso(string mensaje, MessageBoxIcon icono = MessageBoxIcon.Warning, bool registrar = true)
        {
            if (registrar)
                RegistrarLog("Aviso", mensaje);
            MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, icono);
        }

        private void Error(string mensaje)
        {
            RegistrarLog("Error", mensaje);
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void AvisarViajeNoConsultado()
        {
            Aviso("No se pudo consultar el viaje en UNIGIS. Es posible que el viaje no exista o que haya ocurrido un error de conexión u otro que impidió consultarlo.");
        }

        private bool ValidarViajeAntesDeLiberar(string idviaje)
        {
            if (!consultarviaje(Convert.ToInt32(idviaje)))
            {
                AvisarViajeNoConsultado();
                return false;
            }

            if (ov == null || ov.d == null || ov.d.Paradas == null || ov.d.Paradas.Length == 0)
            {
                Aviso("El viaje en UNIGIS se encuentra vacío.");
                return false;
            }

            return true;
        }

        private void CargarViajeEnPantalla()
        {
            string idviaje = txt_viaje.Text.Trim();
            Cursor = Cursors.WaitCursor;
            try
            {
                if (!ObtenerParadas(idviaje))
                    RestablecerPantalla();
            }
            catch (Exception ex)
            {
                RegistrarLog("Error al consultar el viaje", ex.ToString());
                RestablecerPantalla();
                Error("No se pudo consultar el viaje." + Environment.NewLine + Environment.NewLine + Libreria.MensajeError(ex));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void RestablecerPantalla()
        {
            dtg.Clear();
            Lcpr.Clear();
            txt_viaje.Enabled = true;
            btn_canc.Enabled = false;
            button1.Enabled = false;
        }

        private void VolverAlEstadoInicial(bool conservarNumeroDeViaje = false)
        {
            RestablecerPantalla();
            dataGridView1.DataSource = null;
            if (!conservarNumeroDeViaje)
                txt_viaje.Text = "";
            txt_viaje.Focus();
        }

        private void RegistrarLog(string paso, string detalle = "")
        {
            try
            {
                string persona = string.IsNullOrWhiteSpace(usuario) ? Environment.UserName : usuario.Trim();
                foreach (char invalido in Path.GetInvalidFileNameChars())
                    persona = persona.Replace(invalido, '_');

                const string origen = "Liberar viaje";
                string linea = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " | " + Environment.MachineName + " | " + persona + " | " + origen + " | " + txt_viaje.Text.Trim() + " | " + paso + " | " + ("" + detalle).Replace(Environment.NewLine, " ") + Environment.NewLine;
                string archivo = DateTime.Now.ToString("MMyyyy", System.Globalization.CultureInfo.InvariantCulture) + "-" + persona + ".log";

                string[] carpetas = new string[]
                {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"),
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ActualizadorDoctosUnigis")
                };
                foreach (string carpeta in carpetas)
                {
                    try
                    {
                        Directory.CreateDirectory(carpeta);
                        File.AppendAllText(Path.Combine(carpeta, archivo), linea, Encoding.UTF8);
                        return;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string idviaje = txt_viaje.Text.Trim();
            Cursor = Cursors.WaitCursor;
            try
            {
                LiberarViaje(idviaje);
            }
            catch (Exception ex)
            {
                RegistrarLog("Error al liberar el viaje", ex.ToString());
                button1.Enabled = dtg.Rows.Count > 0;
                Error("No se pudo liberar el viaje." + Environment.NewLine + Environment.NewLine + Libreria.MensajeError(ex));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void LiberarViaje(string idviaje)
        {
            int numeroViaje;
            if (!int.TryParse(idviaje, out numeroViaje))
            {
                Aviso("El número de viaje no es válido.");
                return;
            }
            idviaje = numeroViaje.ToString();
            RegistrarLog("Inicio");

            if (!ValidarViajeAntesDeLiberar(idviaje))
                return;
            if (!ValidarDatosDelViaje())
                return;
            if (!UsuarioPuedeLiberar(idviaje))
                return;
            bool paradasCargadas = false;
            try
            {
                paradasCargadas = ObtenerParadasParaLiberar(idviaje);
            }
            finally
            {
                if (!paradasCargadas)
                    RestablecerPantalla();
            }

            if (!paradasCargadas)
                return;

            string estab = obtener_estab(cod_estab);
            if (estab == null)
            {
                Aviso("El depósito del viaje no existe.");
                return;
            }

            string folio = e.BuscarFolioDelViaje(estab, idviaje);
            if (folio == null)
            {
                Aviso("No se encontró la conexión del establecimiento del viaje.");
                return;
            }

            bool tieneEmbarque = folio != "";
            RegistrarLog("Embarque", tieneEmbarque ? "Ya tiene el embarque " + folio : "Sin embarque vigente (primera liberación)");
            RegistrarEstadosDeLasParadas();
            if (tieneEmbarque)
            {
                int? sinLiberar = null;
                try
                {
                    sinLiberar = ParadasNoLiberadas(numeroViaje);
                }
                catch (Exception ex)
                {
                    RegistrarLog("Error al consultar UNIGIS (verificación previa)", ex.ToString());
                }

                if (sinLiberar == null)
                {
                    Error("Ha ocurrido un error al consultar UNIGIS. Intente de nuevo.");
                    return;
                }

                if (sinLiberar == 0)
                {
                    Aviso("El viaje ya se encuentra liberado", MessageBoxIcon.Information);
                    VolverAlEstadoInicial();
                    return;
                }
            }

            if (HayParadasNoValidadas())
            {
                Aviso("No se puede liberar el viaje porque no todas las paradas pendientes están validadas");
                return;
            }

            if (!tieneEmbarque)
            {
                if (!HayParadasValidadas())
                {
                    Aviso("Este viaje tiene paradas liberadas en UNIGIS pero no tiene embarque.");
                    return;
                }

                RegistrarParadasLiberadasFueraDelEmbarque();

                List<Entrega> entregas = ConstruirEntregas();
                if (entregas == null)
                    return;

                if (!ConfirmarLiberacion(idviaje, ""))
                    return;

                List<Entrega> omitidas;
                string folioNuevo;
                try
                {
                    omitidas = e.CrearEmbarque(estab, ov, usuario, idviaje, entregas, out folioNuevo);
                }
                catch (Exception ex)
                {
                    RegistrarLog("Error al crear el embarque", ex.ToString());
                    Error("No se logró liberar el viaje. Intente de nuevo.");
                    return;
                }

                RegistrarLog("Embarque creado", "Folio " + folioNuevo + " | renglones " + entregas.Count + " | omitidos " + omitidas.Count);
                RegistrarProductosNoEmbarcados(omitidas);
                folio = folioNuevo;
            }
            else
            {
                List<Entrega> entregas = ConstruirEntregas();
                if (entregas == null)
                    return;

                if (!ConfirmarLiberacion(idviaje, folio))
                    return;

                if (entregas.Count > 0)
                {
                    List<Entrega> omitidas;
                    int yaProcesados;
                    try
                    {
                        omitidas = e.AgregarRenglones(estab, folio, entregas, out yaProcesados);
                    }
                    catch (Exception ex)
                    {
                        RegistrarLog("Error al agregar renglones al embarque", ex.ToString());
                        Error("No se logró liberar el viaje. Intente de nuevo.");
                        return;
                    }

                    RegistrarLog("Renglones agregados al embarque", "Folio " + folio + " | renglones " + entregas.Count + " | ya procesados " + yaProcesados + " | omitidos " + omitidas.Count);
                    RegistrarProductosNoEmbarcados(omitidas);
                }
            }

            EnviarLiberacionAUnigis();
            int fallos = 0;
            List<FolioDuplicado> duplicados = new List<FolioDuplicado>();
            int? pendientes = null;
            try
            {
                pendientes = ParadasNoLiberadas(numeroViaje, out fallos, out duplicados);
            }
            catch (Exception ex)
            {
                RegistrarLog("Error al consultar UNIGIS (verificación final)", ex.ToString());
            }

            if (pendientes == null)
            {
                Error("El embarque fue creado correctamente, pero ocurrió un error que no permitió consultar si las paradas fueron liberadas en UNIGIS. Por favor intente liberar el viaje nuevamente.");
                VolverAlEstadoInicial(true);
                return;
            }

            if (fallos > 0)
            {
                RegistrarLog("Liberación incompleta", fallos + " parada(s) sin liberar");
                Error("El embarque fue creado correctamente, pero ocurrió un error que no permitió liberar todas las paradas en UNIGIS. Por favor intente liberar el viaje nuevamente.");
                return;
            }

            if (duplicados.Count > 0)
            {
                AvisarFoliosDuplicados(duplicados);
                VolverAlEstadoInicial();
                return;
            }

            Aviso("El viaje ha sido liberado correctamente en el embarque " + folio + ".", MessageBoxIcon.Information);
            VolverAlEstadoInicial();
        }

        // folioEmbarque vacío = primera liberación (se generará el embarque); con folio = reintento sobre ese embarque
        private bool ConfirmarLiberacion(string idviaje, string folioEmbarque)
        {
            int paradas = 0;
            foreach (ObtenerViaje_Response.Parada p in ov.d.Paradas)
            {
                if (!string.Equals(("" + p.Estado).Trim(), "Liberado", StringComparison.OrdinalIgnoreCase))
                    paradas++;
            }

            string cuantas = paradas == 1 ? "Se va a liberar 1 parada" : "Se van a liberar " + paradas + " paradas";
            string mensaje = folioEmbarque == ""
                ? cuantas + " del viaje " + idviaje + " y se generará su embarque. ¿Continuar?"
                : cuantas + " del viaje " + idviaje + " en el embarque " + folioEmbarque + ". ¿Continuar?";
            if (MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                return true;

            RegistrarLog("Liberación cancelada por el usuario", mensaje);
            return false;
        }

        private bool ValidarDatosDelViaje()
        {
            if (ov.d.FechaInicioReal == null
                || ov.d.Vehiculo == null || string.IsNullOrWhiteSpace(ov.d.Vehiculo.Dominio)
                || ov.d.Conductor == null || string.IsNullOrWhiteSpace(ov.d.Conductor.ReferenciaExterna)
                || string.IsNullOrWhiteSpace(cod_estab))
            {
                Aviso("El viaje en UNIGIS tiene información incompleta.");
                return false;
            }
            return true;
        }

        private bool HayParadasValidadas()
        {
            foreach (ObtenerViaje_Response.Parada p in ov.d.Paradas)
            {
                if (string.Equals(("" + p.Estado).Trim(), "Validado", StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        private bool HayParadasNoValidadas()
        {
            foreach (ObtenerViaje_Response.Parada p in ov.d.Paradas)
            {
                string estado = ("" + p.Estado).Trim();
                if (string.Equals(estado, "Liberado", StringComparison.OrdinalIgnoreCase))
                    continue;
                if (estado != "Validado")
                    return true;
            }
            return false;
        }

        private void RegistrarEstadosDeLasParadas()
        {
            List<string> estados = new List<string>();
            Dictionary<string, int> conteo = new Dictionary<string, int>();
            List<string> noListas = new List<string>();
            foreach (ObtenerViaje_Response.Parada p in ov.d.Paradas)
            {
                string estado = ("" + p.Estado).Trim();
                if (estado == "")
                    estado = "(sin estado)";
                if (!conteo.ContainsKey(estado))
                {
                    conteo[estado] = 0;
                    estados.Add(estado);
                }
                conteo[estado]++;

                if (!string.Equals(estado, "Validado", StringComparison.OrdinalIgnoreCase)
                    && !string.Equals(estado, "Liberado", StringComparison.OrdinalIgnoreCase))
                    noListas.Add("parada " + p.IdParada + " (" + estado + ")");
            }

            List<string> partes = new List<string>();
            foreach (string estado in estados)
                partes.Add(estado + " " + conteo[estado]);

            string detalle = ov.d.Paradas.Length + " paradas: " + string.Join(", ", partes);
            if (noListas.Count > 0)
                detalle += " | no listas: " + string.Join(", ", noListas);
            RegistrarLog("Estados de las paradas", detalle);
        }

        private void RegistrarParadasLiberadasFueraDelEmbarque()
        {
            List<string> liberadas = new List<string>();
            foreach (ObtenerViaje_Response.Parada p in ov.d.Paradas)
            {
                if (string.Equals(("" + p.Estado).Trim(), "Liberado", StringComparison.OrdinalIgnoreCase))
                    liberadas.Add("parada " + p.IdParada + " " + ("" + p.RefDocumento).Split('|')[0].Trim());
            }
            if (liberadas.Count > 0)
                RegistrarLog("Paradas liberadas fuera del embarque", string.Join("; ", liberadas));
        }

        private List<Entrega> ConstruirEntregas()
        {
            List<Entrega> entregas = new List<Entrega>();
            foreach (DataRow row in dtg.Rows)
            {
                if (row["Estatus"].ToString().Trim() != "Validado")
                    continue;

                if (row["Ref.Documento"].ToString().Trim().StartsWith("PP-", StringComparison.Ordinal))
                    continue;

                Entrega entrega;
                try
                {
                    entrega = new Entrega(row, ov.d.Conductor.ReferenciaExterna, ov.d.Vehiculo.Dominio);
                }
                catch (Exception)
                {
                    Aviso("La parada " + row["IdParada"] + " tiene un folio inválido.");
                    return null;
                }

                entregas.Add(entrega);
            }
            return entregas;
        }

        private void RegistrarProductosNoEmbarcados(List<Entrega> omitidas)
        {
            if (omitidas.Count == 0)
                return;

            List<string> documentos = new List<string>();
            Dictionary<string, List<string>> productosPorDocumento = new Dictionary<string, List<string>>();
            foreach (Entrega entrega in omitidas)
            {
                if (!productosPorDocumento.ContainsKey(entrega.Documento))
                {
                    productosPorDocumento[entrega.Documento] = new List<string>();
                    documentos.Add(entrega.Documento);
                }
                productosPorDocumento[entrega.Documento].Add(entrega.Cod_prod.Trim());
            }

            StringBuilder detalle = new StringBuilder();
            foreach (string documento in documentos)
            {
                if (detalle.Length > 0)
                    detalle.Append("; ");
                detalle.Append(documento + ": " + string.Join(", ", productosPorDocumento[documento]));
            }
            RegistrarLog("Productos no embarcados", detalle.ToString());
        }

        private void EnviarLiberacionAUnigis()
        {
            xmlwriterParada escritor = new xmlwriterParada();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Uri requestUri = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx?op=ModificarEstadoParada");
            HashSet<string> enviados = new HashSet<string>();
            using (HttpClient httpClient = new HttpClient())
            {
                foreach (ConsultarParadaPorIdResponse parada in Lcpr)
                {
                    if (string.Equals(("" + parada.d.EstadoParada).Trim(), "Liberado", StringComparison.OrdinalIgnoreCase))
                        continue;
                    string referencia = ("" + parada.d.RefDocumento).Trim();
                    if (referencia != "" && !enviados.Add(referencia))
                        continue;
                    string documento = referencia.Split('|')[0].Trim();
                    try
                    {
                        string contenido = escritor.stringtoxml("1234", parada.d.RefDocumento, parada.d.EstadoParada, Convert.ToString(parada.d.IdViaje), "true");
                        HttpResponseMessage respuesta = httpClient.PostAsync(requestUri, new StringContent(contenido, Encoding.UTF8, "text/xml")).Result;
                        if (!respuesta.IsSuccessStatusCode)
                        {
                            string motivo = Libreria.ErrorSoap(respuesta.Content.ReadAsStringAsync().Result);
                            RegistrarLog("Liberación rechazada", documento + " HTTP " + (int)respuesta.StatusCode + (motivo != null ? ": " + motivo : ""));
                        }
                    }
                    catch (Exception ex)
                    {
                        RegistrarLog("Liberación con error", documento + ": " + Libreria.MensajeError(ex));
                    }
                }
            }
        }

        private class FolioDuplicado
        {
            public string Documento;
            public List<int> Paradas = new List<int>();
        }

        private int? ParadasNoLiberadas(int numeroViaje)
        {
            int fallos;
            List<FolioDuplicado> duplicados;
            return ParadasNoLiberadas(numeroViaje, out fallos, out duplicados);
        }

        // Folio duplicado = RefDocumento en 2 o más paradas. Si alguna de sus paradas ya está liberada, las pendientes
        // van a "duplicados" (se liberan a mano en UNIGIS); si ninguna lo está, o el folio no es duplicado, cuentan en "fallos".
        private int? ParadasNoLiberadas(int numeroViaje, out int fallos, out List<FolioDuplicado> duplicados)
        {
            fallos = 0;
            duplicados = new List<FolioDuplicado>();
            if (!consultarviaje(numeroViaje) || ov.d.Paradas == null)
                return null;

            List<string> folios = new List<string>();
            Dictionary<string, List<ObtenerViaje_Response.Parada>> paradasPorFolio = new Dictionary<string, List<ObtenerViaje_Response.Parada>>();
            foreach (ObtenerViaje_Response.Parada p in ov.d.Paradas)
            {
                string referencia = ("" + p.RefDocumento).Trim();
                if (referencia == "")
                    referencia = "#" + p.IdParada;
                if (!paradasPorFolio.ContainsKey(referencia))
                {
                    paradasPorFolio[referencia] = new List<ObtenerViaje_Response.Parada>();
                    folios.Add(referencia);
                }
                paradasPorFolio[referencia].Add(p);
            }

            int pendientes = 0;
            foreach (string referencia in folios)
            {
                List<ObtenerViaje_Response.Parada> paradas = paradasPorFolio[referencia];
                List<int> sinLiberar = new List<int>();
                foreach (ObtenerViaje_Response.Parada p in paradas)
                {
                    if (!string.Equals(("" + p.Estado).Trim(), "Liberado", StringComparison.OrdinalIgnoreCase))
                        sinLiberar.Add(p.IdParada);
                }
                pendientes += sinLiberar.Count;
                if (sinLiberar.Count == 0)
                    continue;

                bool algunaLiberada = sinLiberar.Count < paradas.Count;
                if (paradas.Count > 1 && algunaLiberada)
                {
                    sinLiberar.Sort();
                    FolioDuplicado duplicado = new FolioDuplicado();
                    duplicado.Documento = referencia.Split('|')[0].Trim();
                    duplicado.Paradas = sinLiberar;
                    duplicados.Add(duplicado);
                }
                else
                {
                    fallos += sinLiberar.Count;
                }
            }
            return pendientes;
        }

        private void AvisarFoliosDuplicados(List<FolioDuplicado> duplicados)
        {
            const int maximoLineas = 15;
            List<string> lineas = new List<string>();
            foreach (FolioDuplicado duplicado in duplicados)
                lineas.Add(duplicado.Documento + " (" + string.Join(", ", duplicado.Paradas) + ")");
            RegistrarLog("Folios duplicados por liberar a mano", string.Join("; ", lineas));

            StringBuilder mensaje = new StringBuilder();
            mensaje.AppendLine("El embarque fue creado correctamente.");
            mensaje.AppendLine();
            mensaje.AppendLine("Los siguientes folios están duplicados en el viaje. Las paradas indicadas deben liberarse manualmente en UNIGIS:");
            mensaje.AppendLine();
            for (int i = 0; i < lineas.Count && i < maximoLineas; i++)
                mensaje.AppendLine("* " + lineas[i]);
            int restantes = lineas.Count - maximoLineas;
            if (restantes > 0)
                mensaje.AppendLine("* ... (" + restantes + (restantes == 1 ? " folio más)" : " folios más)"));

            Aviso(mensaje.ToString(), MessageBoxIcon.Warning, false);
        }

        //Metodo que realiza una consulta a la web api de unigis para consultar  inoformacion del viaje por medio de el id de viaje
        public bool consultarviaje( int viajeid)
        {
            ConsultaViajeID opr = new ConsultaViajeID();
            opr.ApiKey = "1234";
            opr.IdViaje = viajeid;
            string jsonString = JsonConvert.SerializeObject(opr);
            using (var client = new HttpClient())
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                var endpoint = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx/ConsultarViaje");

                var paylod = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var respuesta = client.PostAsync(endpoint, paylod).Result;
                var result = respuesta.Content.ReadAsStringAsync().Result;

                if (!respuesta.IsSuccessStatusCode)
                {
                    RegistrarLog("ConsultarViaje rechazado", "Viaje " + viajeid + " HTTP " + (int)respuesta.StatusCode);
                    return false;
                }
                ObtenerViaje_Response.Rootobject viaje = JsonConvert.DeserializeObject<ObtenerViaje_Response.Rootobject>(result);
                if (viaje == null || viaje.d == null)
                {
                    RegistrarLog("ConsultarViaje sin datos", "Viaje " + viajeid);
                    return false;
                }
                ov = viaje;
                cod_estab = viaje.d.DepositoSalida?.RefDepositoExterno?.ToString();
                return true;
            }
        }                   

        private void frm_ValidaParada_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "TOPF3", this.Top.ToString());
                Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "LeftF3", this.Left.ToString());
                Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "WidthF3", this.Width.ToString());
                Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "HeightF3", this.Height.ToString());
            }
            catch (Exception)
            {
            }


        }

        private void btn_canc_Click(object sender, EventArgs e)
        {

            dtg.Clear();
            Lcpr.Clear();
            txt_viaje.Text = "";
            txt_viaje.Enabled = true;
            btn_canc.Enabled = false;
            button1.Enabled = false;
        }
    }
}
