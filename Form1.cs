using ActualizadorDoctosUnigis.Contoller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net;

using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using ClosedXML.Excel;

namespace ActualizadorDoctosUnigis
{
    public partial class Form1 : Form
    {
        Qrys q = new Qrys();
        DataTable dt;
        DataTable DTFolio = new DataTable();
        Decimal Min = 24;
        Decimal Max = 0;
        Decimal Mid = 0;
        Decimal Steps = Decimal.MinValue;
        Decimal MaxV = 0;
        decimal minv = 0;
        string cod_estab;
        string Estab = "";
        // dt tiene un renglon por producto (lo que regresa EYP_UNIGIS) y dtDocs uno por documento, que es lo que se muestra
        DataTable dtDocs;
        Dictionary<string, List<DataRow>> productosPorDocumento = new Dictionary<string, List<DataRow>>();
        // Columnas de EYP_UNIGIS que cambian por producto; las demas son iguales en todos los renglones del documento
        static readonly string[] ColumnasProducto = { "CODIGO ITEM", "NOMBRE ITEM", "CANTIDAD", "COSTO ITEM", "CAPACIDAD UNO", "Linea", "Sublinea" };
        HttpClient client = new HttpClient();
        string user;
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(string u)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.user = u;
        }

        private enum ResultadoEnvio { Enviado, EnviadoSinHistorico, EnviadoSinMarcar, SinConfirmar, NoEnviado }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dt == null || dtDocs == null || string.IsNullOrEmpty(Estab))
            {
                MessageBox.Show("No hay documentos cargados. Seleccione un establecimiento o refresque la lista.", "Envio de Documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dataGridView1.EndEdit();
            DataTable dtLocalC = DivDT(dt, new DataTable(), "");
            if (dtLocalC.Rows.Count == 0)
            {
                MessageBox.Show("No hay documentos seleccionados para enviar.", "Envio de Documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result1 = MessageBox.Show("Deseas enviar la informacion seleccionada a Unigis una vez se envie no podra eliminarse", "Informacion ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result1 != DialogResult.Yes)
                return;

            // Antes de mandar nada se quitan los documentos que ya se enviaron (otro usuario, otra ventana o un pick-up ya mandado)
            List<string> yaEnviados;
            try
            {
                DataTable dCod = q.Cod_estab(Estab);
                if (dCod.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró el establecimiento " + Estab + ". No se envió nada.", "Envio de Documentos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                cod_estab = dCod.Rows[0].ItemArray[0].ToString();

                yaEnviados = Validar_folio(dtLocalC, cod_estab);
                QuitarPickupYaEnviados(dtLocalC);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible verificar los documentos antes de enviarlos. No se envió nada." + Environment.NewLine + Environment.NewLine + MensajeError(ex), "Envio de Documentos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> documentos = (from r in dtLocalC.AsEnumerable()
                                       select r["N° DOCUMENTO"].ToString()).Distinct().ToList();

            Dictionary<ResultadoEnvio, List<string>> resultados = new Dictionary<ResultadoEnvio, List<string>>();
            foreach (ResultadoEnvio tipo in Enum.GetValues(typeof(ResultadoEnvio)))
                resultados[tipo] = new List<string>();

            Cursor = Cursors.WaitCursor;
            btn_Enviar.Enabled = false;
            try
            {
                System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                foreach (string documento in documentos)
                {
                    DataTable aux = DivDT(dtLocalC, new DataTable(), documento);
                    if (aux.Rows.Count == 0)
                        continue;

                    string detalle;
                    ResultadoEnvio resultado = EnviarDocumento(aux, out detalle);
                    resultados[resultado].Add(documento + (detalle == "" ? "" : ": " + detalle));
                }
            }
            finally
            {
                Cursor = Cursors.Default;
                btn_Enviar.Enabled = true;
            }

            MostrarResumenEnvio(resultados, yaEnviados);

            if (documentos.Count > 0)
                LimpiarDocumentos();
        }

        // Unigis se marca primero; si despues falla la sucursal o el historico se reporta aparte para que el documento no se reenvie
        private ResultadoEnvio EnviarDocumento(DataTable aux, out string detalle)
        {
            detalle = "";
            CompletarCorreoCliente(aux);
            string pedido;
            try
            {
                pedido = new xmlwriterOrden().stringtoxml(aux);
            }
            catch (Exception ex)
            {
                detalle = "no se pudo armar el pedido (" + MensajeError(ex) + ")";
                return ResultadoEnvio.NoEnviado;
            }

            HttpResponseMessage respuesta;
            string contenido;
            try
            {
                var endpoint = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx?op=CrearOrdenesPedido");
                var paylod = new StringContent(pedido, Encoding.UTF8, "text/xml");
                respuesta = client.PostAsync(endpoint, paylod).Result;
                contenido = respuesta.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                detalle = MensajeError(ex);
                return NoSalioLaPeticion(ex) ? ResultadoEnvio.NoEnviado : ResultadoEnvio.SinConfirmar;
            }

            if (!respuesta.IsSuccessStatusCode)
            {
                detalle = Libreria.ErrorSoap(contenido) ??("Unigis respondió " + (int)respuesta.StatusCode + " " + respuesta.ReasonPhrase);
                return ResultadoEnvio.NoEnviado;
            }

            if (contenido.IndexOf("CrearOrdenesPedidoResult", StringComparison.OrdinalIgnoreCase) < 0)
            {
                detalle = "Unigis respondió sin confirmar la creación del pedido";
                return ResultadoEnvio.SinConfirmar;
            }

            try
            {
                MarcarEnviado(aux);
            }
            catch (Exception ex)
            {
                detalle = MensajeError(ex);
                try { GuardarHistorico(aux); } catch (Exception) { }
                return ResultadoEnvio.EnviadoSinMarcar;
            }

            try
            {
                GuardarHistorico(aux);
            }
            catch (Exception ex)
            {
                detalle = MensajeError(ex);
                return ResultadoEnvio.EnviadoSinHistorico;
            }

            return ResultadoEnvio.Enviado;
        }

        // En ventas de mostrador (cliente 0) la consulta no trae correo; se usa el del cliente alterno si tiene uno valido
        private void CompletarCorreoCliente(DataTable aux)
        {
            if (xmlwriterOrden.CorreoCliente(aux.Rows[0]["e_mail"].ToString()) != "")
                return;

            string nDoc = aux.Rows[0]["N° DOCUMENTO"].ToString().Trim();
            try
            {
                string correo = q.CorreosClienteAlterno(FolioDocumento(nDoc), TransaccionDocumento(nDoc), cod_estab)
                                 .Select(xmlwriterOrden.CorreoCliente).FirstOrDefault(c => c != "");
                if (correo != null)
                    aux.Rows[0]["e_mail"] = correo;
            }
            catch (Exception)
            {
                // Sin el correo el pedido se manda igual
            }
        }

        private void MostrarResumenEnvio(Dictionary<ResultadoEnvio, List<string>> resultados, List<string> yaEnviados)
        {
            StringBuilder msg = new StringBuilder();
            AgregarSeccion(msg, "Documentos enviados a Unigis:", resultados[ResultadoEnvio.Enviado]);
            AgregarSeccion(msg, "Enviados a Unigis, pero no se guardó el histórico de envío:", resultados[ResultadoEnvio.EnviadoSinHistorico]);
            AgregarSeccion(msg, "Enviados a Unigis, pero NO se marcaron como enviados en la sucursal. No los vuelva a enviar y avise a Sistemas:", resultados[ResultadoEnvio.EnviadoSinMarcar]);
            AgregarSeccion(msg, "No se pudo confirmar si Unigis los recibió. Revise en Unigis antes de volver a enviarlos:", resultados[ResultadoEnvio.SinConfirmar]);
            AgregarSeccion(msg, "No se enviaron (refresque y vuelva a intentarlo):", resultados[ResultadoEnvio.NoEnviado]);
            AgregarSeccion(msg, "Ya estaban enviados, se quitaron de este envío:", yaEnviados);

            if (msg.Length == 0)
                msg.Append("No se envió ningún documento.");

            bool conProblemas = resultados[ResultadoEnvio.EnviadoSinHistorico].Count > 0 || resultados[ResultadoEnvio.EnviadoSinMarcar].Count > 0
                             || resultados[ResultadoEnvio.SinConfirmar].Count > 0 || resultados[ResultadoEnvio.NoEnviado].Count > 0;

            MessageBox.Show(msg.ToString().TrimEnd(), "Actualizacion de Documentos ", MessageBoxButtons.OK, conProblemas ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
        }

        private static void AgregarSeccion(StringBuilder msg, string titulo, List<string> lineas)
        {
            if (lineas.Count == 0)
                return;

            msg.AppendLine(titulo);
            foreach (string linea in lineas)
                msg.AppendLine("   " + linea);
            msg.AppendLine();
        }

        // Solo cuando no se pudo ni abrir la conexion hay certeza de que Unigis no recibio el pedido
        private static bool NoSalioLaPeticion(Exception ex)
        {
            for (Exception actual = ex; actual != null; actual = actual.InnerException)
            {
                WebException web = actual as WebException;
                if (web != null)
                    return web.Status == WebExceptionStatus.NameResolutionFailure || web.Status == WebExceptionStatus.ConnectFailure;
            }
            return false;
        }

        private static string MensajeError(Exception ex)
        {
            return Libreria.MensajeError(ex);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "TOPF1").ToString() != "")
            {
                this.Top = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "TOPF1").ToString());
                this.Left = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "LeftF1").ToString());
                this.Width = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "WidthF1").ToString());
                this.Height = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "HeightF1").ToString());
            }

            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CurrentCellDirtyStateChanged += Libreria.AplicarCasillaAlMomento;

            DataTable DTCE = new DataTable();
            DTCE = q.catalogoEstab();
            DTCE = EstabUser(user, DTCE);

            foreach (DataRow row in DTCE.Rows)
            {
                comboBox1.Items.Add(row[0].ToString());
            }
        }

        private DataTable DivDT(DataTable DTO, DataTable DTA, string folio)
        {
            if (DTA.Columns.Count == 0)
            {
                DTA.Columns.Add("Enviar");
                DTA.Columns.Add("N° DOCUMENTO");
                DTA.Columns.Add("FECHA_DOC");
                DTA.Columns.Add("FechaEntrega");
                DTA.Columns.Add("cod_cte");
                DTA.Columns.Add("razon_social");
                DTA.Columns.Add("tel1");
                DTA.Columns.Add("tel2");
                DTA.Columns.Add("e_mail");
                DTA.Columns.Add("Peso_KG");
                DTA.Columns.Add("LATITUD");
                DTA.Columns.Add("LONGITUD");
                DTA.Columns.Add("DIRECCION");
                DTA.Columns.Add("NOMBRE ITEM");
                DTA.Columns.Add("CANTIDAD");
                DTA.Columns.Add("CODIGO ITEM");
                DTA.Columns.Add("NOTAS");
                DTA.Columns.Add("FECHA MIN ENTREGA");
                DTA.Columns.Add("FECHA MAX ENTREGA");
                DTA.Columns.Add("MIN VENTANA HORARIA 1");
                DTA.Columns.Add("MAX VENTANA HORARIA 1");
                DTA.Columns.Add("MIN VENTANA HORARIA 2");
                DTA.Columns.Add("MAX VENTANA HORARIA 2");
                DTA.Columns.Add("COSTO ITEM");
                DTA.Columns.Add("CAPACIDAD UNO");
                DTA.Columns.Add("CAPACIDAD DOS");
                DTA.Columns.Add("SERVICE TIME");
                DTA.Columns.Add("IMPORTANCIA");
                DTA.Columns.Add("IDENTIFICADOR CONTACTO");
                DTA.Columns.Add("NOMBRE CONTACTO");
                DTA.Columns.Add("TELEFONO");
                DTA.Columns.Add("EMAIL CONTACTO");
                DTA.Columns.Add("CT ORIGEN");
                DTA.Columns.Add("REFERENCIA DOMICILIO");
                DTA.Columns.Add("Calle");
                DTA.Columns.Add("NumeroPuerta");
                DTA.Columns.Add("colonia");
                DTA.Columns.Add("Barrio");
                DTA.Columns.Add("Localidad");
                DTA.Columns.Add("Partido");
                DTA.Columns.Add("Estado");
                DTA.Columns.Add("pais");
                DTA.Columns.Add("RefDomicilioExterno");
                DTA.Columns.Add("DomicilioDescripcion");
                DTA.Columns.Add("InicioHorario1");
                DTA.Columns.Add("InicioHorario2");
                DTA.Columns.Add("FinHorario1");
                DTA.Columns.Add("FinHorario2");
                DTA.Columns.Add("TiempoEspera");
                DTA.Columns.Add("DomCodPostal");
                DTA.Columns.Add("Contacto");
                DTA.Columns.Add("CodigoSucursal");
                DTA.Columns.Add("RefDepositoExterno");
                DTA.Columns.Add("CodigoPostalEstab");
                DTA.Columns.Add("Linea");
                DTA.Columns.Add("Sublinea");
                DTA.Columns.Add("Tipo");
                DTA.Columns.Add("CantidadOriginal");
            }

            if (folio == "")
            {
                // Solo los productos marcados de los documentos marcados
                foreach (DataRow doc in dtDocs.Rows)
                {
                    if (!Convert.ToBoolean(doc["Enviar"]))
                        continue;

                    foreach (DataRow dr in productosPorDocumento[doc["N° DOCUMENTO"].ToString()])
                    {
                        if (Convert.ToBoolean(dr["Seleccionado"]))
                            CopiarRenglon(dr, DTA);
                    }
                }
            }
            else
            {
                foreach (DataRow dr in DTO.Rows)
                {
                    if (folio.Trim() == dr["N° DOCUMENTO"].ToString().Trim())
                        CopiarRenglon(dr, DTA);
                }
            }
            return DTA;
        }

        private static void CopiarRenglon(DataRow dr, DataTable DTA)
        {
            DataRow drLocal = DTA.NewRow();

            drLocal["N° DOCUMENTO"] = dr["N° DOCUMENTO"].ToString();
            drLocal["FECHA_DOC"] = dr["FECHA_DOC"];
            drLocal["FechaEntrega"] = dr["FechaEntrega"];
            drLocal["cod_cte"] = dr["cod_cte"];
            drLocal["razon_social"] = dr["razon_social"];
            drLocal["tel1"] = dr["tel1"];
            drLocal["tel2"] = dr["tel2"];
            drLocal["e_mail"] = dr["e_mail"];
            drLocal["Peso_KG"] = dr["Peso_KG"];
            drLocal["LATITUD"] = dr["LATITUD"];
            drLocal["LONGITUD"] = dr["LONGITUD"];
            drLocal["DIRECCION"] = dr["DIRECCION"];
            drLocal["NOMBRE ITEM"] = dr["NOMBRE ITEM"];
            drLocal["CANTIDAD"] = dr["CANTIDAD"];
            drLocal["CODIGO ITEM"] = dr["CODIGO ITEM"];
            drLocal["NOTAS"] = dr["NOTAS"];
            drLocal["FECHA MIN ENTREGA"] = dr["FECHA MIN ENTREGA"];
            drLocal["FECHA MAX ENTREGA"] = dr["FECHA MAX ENTREGA"];
            drLocal["MIN VENTANA HORARIA 1"] = dr["MIN VENTANA HORARIA 1"].ToString();
            drLocal["MAX VENTANA HORARIA 1"] = dr["MAX VENTANA HORARIA 1"].ToString();
            drLocal["MIN VENTANA HORARIA 2"] = dr["MIN VENTANA HORARIA 2"].ToString();
            drLocal["MAX VENTANA HORARIA 2"] = dr["MAX VENTANA HORARIA 2"].ToString();
            drLocal["COSTO ITEM"] = dr["COSTO ITEM"];
            drLocal["CAPACIDAD UNO"] = dr["CAPACIDAD UNO"];
            drLocal["CAPACIDAD DOS"] = dr["CAPACIDAD DOS"];
            drLocal["SERVICE TIME"] = dr["SERVICE TIME"];
            drLocal["IMPORTANCIA"] = dr["IMPORTANCIA"];
            drLocal["IDENTIFICADOR CONTACTO"] = dr["IDENTIFICADOR CONTACTO"];
            drLocal["NOMBRE CONTACTO"] = dr["NOMBRE CONTACTO"];
            drLocal["TELEFONO"] = dr["TELEFONO"];
            drLocal["EMAIL CONTACTO"] = dr["EMAIL CONTACTO"];
            drLocal["CT ORIGEN"] = dr["CT ORIGEN"];
            drLocal["REFERENCIA DOMICILIO"] = dr["REFERENCIA DOMICILIO"];
            drLocal["Calle"] = dr["Calle"];
            drLocal["NumeroPuerta"] = dr["NumeroPuerta"];
            drLocal["colonia"] = dr["colonia"];
            drLocal["Barrio"] = dr["Barrio"];
            drLocal["Localidad"] = dr["Localidad"];
            drLocal["Partido"] = dr["Partido"];
            drLocal["Estado"] = dr["Estado"];
            drLocal["pais"] = dr["pais"];
            drLocal["RefDomicilioExterno"] = dr["RefDomicilioExterno"];
            drLocal["DomicilioDescripcion"] = dr["DomicilioDescripcion"];
            drLocal["InicioHorario1"] = dr["InicioHorario1"];
            drLocal["InicioHorario2"] = dr["InicioHorario2"];
            drLocal["FinHorario1"] = dr["FinHorario1"];
            drLocal["FinHorario2"] = dr["FinHorario2"];
            drLocal["TiempoEspera"] = dr["TiempoEspera"];
            drLocal["DomCodPostal"] = dr["DomCodPostal"];
            drLocal["Contacto"] = dr["Contacto"];
            drLocal["CodigoSucursal"] = dr["CodigoSucursal"];
            drLocal["RefDepositoExterno"] = dr["RefDepositoExterno"];
            drLocal["CodigoPostalEstab"] = dr["CodigoPostalEstab"];
            drLocal["Linea"] = dr["Linea"];
            drLocal["Sublinea"] = dr["Sublinea"];
            drLocal["Tipo"] = dr["Tipo"];
            drLocal["CantidadOriginal"] = dr["CantidadOriginal"];
            DTA.Rows.Add(drLocal);
        }

        private Color GetColorFromValie(Decimal targetValue)
        {
            if (targetValue > MaxV)
            {
                return Color.Red;
            }

            if (targetValue == Max) { return Color.FromArgb(255, 255, 0, 0); }
            else if (targetValue == Mid) { return Color.FromArgb(255, 255, 255, 0); }
            else if (targetValue <= minv)
            {
                return Color.FromArgb(255, 0, 255, 0);
            }

            Decimal offsetValue;
            Decimal offsetSteps;
            Int32 rgbValue;

            if (targetValue < Mid)
            {
                offsetValue = targetValue - minv;
                offsetSteps = offsetValue;
                rgbValue = 255 - Convert.ToInt32(offsetSteps);
                rgbValue = rgbValue + 150;
                if (rgbValue > 255) { rgbValue = 255; }

                return Color.FromArgb(255, 255, rgbValue, 0);
            }

            return Color.Red;
        }

        private void SetMinMax(DataGridView dg, String txt)
        {
            DataTable dt = new DataTable();
            dt = dg.DataSource as DataTable;
            Decimal Temp;
            minv = 24;
            MaxV = Convert.ToDecimal(txt_horasmax.Text);

            foreach (DataRow row in dt.Rows)
            {
                if (row["FechaEntrega"].ToString().Trim() != "")
                {
                    TimeSpan dias = DateTime.Now.Subtract(Convert.ToDateTime(row["FechaEntrega"].ToString()));
                    Temp = Convert.ToDecimal(dias.TotalHours);
                    if (Temp > Max) { Max = Temp; }
                    if (Temp < Min) { Min = Temp; }
                }
            }
            Max = Convert.ToDecimal(txt);

            if (Max != 0)
            {
                Mid = (MaxV - minv) / 2;
                Decimal total = (Max - Mid);
                Steps = total / 10;
                if (Steps % 2 == 0)
                {
                    Steps = Convert.ToInt32(Steps) + 1;
                }
            }
        }

        private void ColorCells(DataGridView dg)
        {
            DataRowView dr;
            Decimal CellValue;
            foreach (DataGridViewRow row in dg.Rows)
            {
                if (!row.IsNewRow)
                {
                    // Igual que antes: el color sale de la fecha de entrega y se pinta en la columna FECHA_DOC
                    dr = (DataRowView)row.DataBoundItem;
                    if (dr["FechaEntrega"].ToString().Trim() != "")
                    {
                        TimeSpan dias = DateTime.Now.Subtract(Convert.ToDateTime(dr["FechaEntrega"].ToString()));
                        CellValue = (Decimal)dias.TotalHours;
                        Color cellColor = GetColorFromValie(CellValue);
                        row.Cells["FECHA_DOC"].Style.BackColor = cellColor;
                    }
                }
            }
        }

        private void bnt_Refrescar_Click(object sender, EventArgs e)
        {
            bnt_Refrescar.Enabled = false;
            btn_Enviar.Enabled = false;
            LimpiarDocumentos();
            Estab = comboBox1.SelectedItem.ToString();
            lbl_Actualizando.Text = "Consultando...";
            lbl_Actualizando.Visible = true;
            CargarDocumentos();
        }

        private void btn_congif_Click(object sender, EventArgs e)
        {
            if (txt_horasmax.Visible)
            {
                lbl_horasmax.Visible = false;
                txt_horasmax.Visible = false;
                lbl_horasmin.Visible = false;
                txt_horasmin.Visible = false;
            }
            else
            {
                lbl_horasmax.Visible = true;
                txt_horasmax.Visible = true;
                lbl_horasmin.Visible = true;
                txt_horasmin.Visible = true;
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button1.Enabled = false;
            btn_Enviar.Enabled = false;
            comboBox1.Enabled = false;
            bnt_Refrescar.Enabled = false;
            LimpiarDocumentos();
            Estab = comboBox1.SelectedItem.ToString();
            lbl_Actualizando.Text = "Consultando...";
            lbl_Actualizando.Visible = true;
            CargarDocumentos();
        }

        private void CargarDocumentos()
        {
            string estab = Estab;
            Libreria.EnSegundoPlano(this, () =>
            {
                DataTable datos = q.selectitems(estab);
                PrepararProductos(datos);
                return datos;
            }, DocumentosCargados);
        }

        private void DocumentosCargados(DataTable datos, Exception error)
        {
            cmb_letra.Enabled = true;
            lbl_Actualizando.Visible = false;
            comboBox1.Enabled = true;
            bnt_Refrescar.Enabled = true;
            btn_Enviar.Enabled = true;
            button2.Enabled = true;
            button1.Enabled = true;

            if (error != null)
            {
                LimpiarDocumentos();
                MessageBox.Show("No fue posible consultar los documentos. Verifique la conexión con el servidor e intente de nuevo." + Environment.NewLine + Environment.NewLine + error.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dt = datos;
            dtDocs = ArmarDocumentos(dt);
            MostrarDocumentos();
        }

        // Agrega a cada producto si va en el envio (todos marcados al cargar) y su cantidad pendiente, que es el maximo que se puede enviar
        private static void PrepararProductos(DataTable productos)
        {
            productos.Columns.Add("Seleccionado", typeof(bool));
            productos.Columns.Add("CantidadOriginal", typeof(decimal));
            foreach (DataRow row in productos.Rows)
            {
                row["Seleccionado"] = true;
                row["CantidadOriginal"] = row["CANTIDAD"] == DBNull.Value ? 0M : Convert.ToDecimal(row["CANTIDAD"]);
            }
        }

        // Un renglon por documento; Productos dice cuantos van marcados y Peso_KG suma solo esos
        private DataTable ArmarDocumentos(DataTable productos)
        {
            string[] quitar = ColumnasProducto.Concat(new[] { "Seleccionado", "CantidadOriginal" }).ToArray();
            DataTable docs = Libreria.AgruparPorDocumento(productos, "N° DOCUMENTO", quitar, productosPorDocumento);
            foreach (DataRow doc in docs.Rows)
                ActualizarResumenDocumento(doc);
            return docs;
        }

        private void ActualizarResumenDocumento(DataRow doc)
        {
            List<DataRow> productos = productosPorDocumento[doc["N° DOCUMENTO"].ToString()];
            int marcados = 0;
            decimal peso = 0M;
            foreach (DataRow producto in productos)
            {
                if (!Convert.ToBoolean(producto["Seleccionado"]))
                    continue;
                marcados++;
                if (producto["Peso_KG"] != DBNull.Value)
                    peso += Convert.ToDecimal(producto["Peso_KG"]);
            }

            doc["Productos"] = marcados + " de " + productos.Count;
            if (doc.Table.Columns.Contains("Peso_KG"))
                doc["Peso_KG"] = Convert.ChangeType(peso, doc.Table.Columns["Peso_KG"].DataType);
        }

        private void MostrarDocumentos()
        {
            Libreria.MostrarDocumentosEnCuadricula(dataGridView1, dtDocs);

            string[] ocultas = { "Cod_cte", "TEL1", "tel2", "FECHA MIN ENTREGA", "FECHA MAX ENTREGA", "MIN VENTANA HORARIA 1", "MAX VENTANA HORARIA 1",
                                 "MIN VENTANA HORARIA 2", "MAX VENTANA HORARIA 2", "e_mail", "CAPACIDAD DOS", "SERVICE TIME", "IMPORTANCIA",
                                 "IDENTIFICADOR CONTACTO", "NOMBRE CONTACTO", "EMAIL CONTACTO", "CT ORIGEN", "REFERENCIA DOMICILIO" };
            foreach (string columna in ocultas)
            {
                if (dataGridView1.Columns.Contains(columna))
                    dataGridView1.Columns[columna].Visible = false;
            }

            if (dataGridView1.Columns.Contains("NOTAS"))
            {
                // Impide que la columna ajuste su ancho automaticamente y la deja en 350 px
                dataGridView1.Columns["NOTAS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridView1.Columns["NOTAS"].Width = 350;
            }

            SetMinMax(dataGridView1, txt_horasmax.Text);
            ColorCells(dataGridView1);
        }

        private void LimpiarDocumentos()
        {
            dt = null;
            dtDocs = null;
            productosPorDocumento = new Dictionary<string, List<DataRow>>();
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            button1.Enabled = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dataGridView1.Columns[e.ColumnIndex].Name != "Productos")
                return;

            DataRow doc = ((DataRowView)dataGridView1.Rows[e.RowIndex].DataBoundItem).Row;
            string documento = doc["N° DOCUMENTO"].ToString();
            string cliente = doc.Table.Columns.Contains("razon_social") ? doc["razon_social"].ToString() : "";
            using (frm_ProductosDocumento frm = new frm_ProductosDocumento(documento, cliente, productosPorDocumento[documento], q))
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                    ActualizarResumenDocumento(doc);
            }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            SetMinMax(dataGridView1, txt_horasmax.Text);
            ColorCells(dataGridView1);
        }

        private void MarcarEnviado(DataTable aux)
        {
            foreach (DataRow row in aux.Rows)
            {
                var nDoc = row["N° DOCUMENTO"].ToString();
                var cod_prod = row["CODIGO ITEM"].ToString().Trim();
                q.UpdatePPE(cod_prod, FolioDocumento(nDoc), TransaccionDocumento(nDoc), cod_estab, "1");
            }
        }

        private void GuardarHistorico(DataTable aux)
        {
            foreach (DataRow row in aux.Rows)
            {
                q.HistoricoEnvio(
                    row["N° DOCUMENTO"].ToString().Trim(),
                    row["CODIGO ITEM"].ToString().Trim(),
                    row["CANTIDAD"].ToString().Trim(),
                    row["RefDepositoExterno"].ToString().Trim(),
                    user,
                    row["FechaEntrega"].ToString().Trim(),
                    row["LATITUD"].ToString().Trim(),
                    row["LONGITUD"].ToString().Trim(),
                    row["CantidadOriginal"].ToString().Trim());
            }
        }

        private static string FolioDocumento(string nDoc)
        {
            return nDoc.Substring(nDoc.IndexOf('-') + 1);
        }

        private static string TransaccionDocumento(string nDoc)
        {
            var transaccion = nDoc.Substring(0, nDoc.IndexOf('-'));
            switch (transaccion)
            {
                case "FC": return "36";
                case "RC": return "37";
                case "PE": return "29";
                case "EdMaC": return "713";
                case "TE": return "35";
                default: return transaccion;
            }
        }

        public DataTable EstabUser(string U, DataTable dt)
        {
            DataTable du;
            DataTable dtX = new DataTable();
            DataColumn dc = new DataColumn();
            dc.ColumnName = "Nombre";
            dtX.Columns.Add(dc);
            DataColumn dc2 = new DataColumn();
            dc2.ColumnName = "cod_estab";
            dtX.Columns.Add(dc2);

            du = q.Consultar("select eu.cod_estab from establecimientos_usuario eu " +
                             "left join usuarios u on eu.usuario = u.usuario " +
                             " Where u.usuario = '" + U + "' and(u.multi_establecimiento = 1 or u.cod_estab = eu.cod_estab) and acceso = '1' and acceso='1'");
            foreach (DataRow row in dt.Rows)
            {
                bool f = false;
                foreach (DataRow row2 in du.Rows)
                {
                    if (row.ItemArray[1].ToString() == row2.ItemArray[0].ToString())
                    {
                        f = true;
                    }
                }
                if (f)
                {
                    dtX.Rows.Add(row[0].ToString(), row[1].ToString());
                }
            }

            return dtX;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dtDocs == null)
                return;

            dataGridView1.EndEdit();
            foreach (DataRow doc in dtDocs.Rows)
                doc["Enviar"] = !Convert.ToBoolean(doc["Enviar"]);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "TOPF1", this.Top.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "LeftF1", this.Left.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "WidthF1", this.Width.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "HeightF1", this.Height.ToString());
        }

        private void button1_Click_5(object sender, EventArgs e)
        {
            if (dt == null || dt.Rows.Count == 0 || string.IsNullOrEmpty(Estab))
            {
                MessageBox.Show("No hay documentos para exportar. Seleccione un establecimiento o refresque la lista.", "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string nombre = "Documentos" + user + "-" + Estab.Substring(0, 5);
            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Excel Workbook|*.xlsx",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                FileName = nombre + ".xlsx"
            })
            {
                if (sfd.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    using (XLWorkbook workbook = new XLWorkbook())
                    {
                        // Se exporta por producto, como antes, sin las columnas internas de seleccion
                        DataTable exportar = this.dt.Copy();
                        exportar.Columns.Remove("Seleccionado");
                        exportar.Columns.Remove("CantidadOriginal");
                        workbook.Worksheets.Add(exportar, nombre);
                        workbook.SaveAs(sfd.FileName);
                    }
                }
                catch (System.IO.IOException ex) when (ArchivoEnUso(ex))
                {
                    MessageBox.Show("No se pudo guardar porque el archivo " + System.IO.Path.GetFileName(sfd.FileName) + " está abierto. Ciérrelo en Excel y vuelva a exportar, o guárdelo con otro nombre.", "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo exportar el archivo." + Environment.NewLine + Environment.NewLine + ex.Message, "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("El archivo se guardó en " + sfd.FileName + ", pero no se pudo abrir." + Environment.NewLine + Environment.NewLine + ex.Message, "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                MessageBox.Show("Archivo Exportado con exito", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Errores 32 y 33 de Windows: otro programa (normalmente Excel) tiene abierto el archivo
        private static bool ArchivoEnUso(System.IO.IOException ex)
        {
            int codigo = ex.HResult & 0xFFFF;
            return codigo == 32 || codigo == 33;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int size = int.Parse(cmb_letra.Text);

            dataGridView1.Font = new Font(dataGridView1.Font.Name, size, dataGridView1.Font.Style);
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DataGridViewColumn col = new DataGridViewColumn();
            col.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        // Quita de auxF los renglones que en la sucursal ya estan marcados como enviados y regresa un renglon por documento con
        // su envio anterior segun el historico
        private List<string> Validar_folio(DataTable auxF, string cod_estab)
        {
            Dictionary<string, int> quitados = new Dictionary<string, int>();
            Dictionary<string, int> productos = new Dictionary<string, int>();

            for (int i = auxF.Rows.Count - 1; i >= 0; i--)
            {
                string nDoc = auxF.Rows[i]["N° DOCUMENTO"].ToString().Trim();
                string cod_prod_F = auxF.Rows[i]["CODIGO ITEM"].ToString();
                DTFolio = q.Consultar("select  folio,cod_prod,cantidad,transaccion from productos_por_embarcar where status='1' and folio='" + FolioDocumento(nDoc) + "' and transaccion='" + TransaccionDocumento(nDoc) + "' and cod_prod='" + cod_prod_F + "'  ", cod_estab);

                int n;
                productos[nDoc] = (productos.TryGetValue(nDoc, out n) ? n : 0) + 1;
                if (DTFolio.Rows.Count >= 1)
                {
                    quitados[nDoc] = (quitados.TryGetValue(nDoc, out n) ? n : 0) + 1;
                    auxF.Rows[i].Delete();
                }
            }

            List<string> yaEnviados = new List<string>();
            if (quitados.Count == 0)
                return yaEnviados;

            // Sin el historico el aviso sale sin fecha, pero el envio sigue
            Dictionary<string, Qrys.EnvioAnterior> anteriores;
            try
            {
                anteriores = q.UltimoEnvioDocumentos(quitados.Keys.ToList());
            }
            catch (Exception)
            {
                anteriores = new Dictionary<string, Qrys.EnvioAnterior>();
            }

            foreach (string nDoc in quitados.Keys.OrderBy(d => d))
            {
                string linea = nDoc + (quitados[nDoc] == productos[nDoc] ? "" : " (" + quitados[nDoc] + " de " + productos[nDoc] + " productos)") + ": ";
                Qrys.EnvioAnterior envio;
                if (anteriores.TryGetValue(nDoc, out envio))
                {
                    linea += "enviado el " + envio.Fecha.ToString("dd/MM/yyyy HH:mm");
                    if (envio.Nombre != "")
                        linea += " por " + envio.Nombre + " (" + envio.Usuario + ")";
                    else if (envio.Usuario != "")
                        linea += " por el usuario " + envio.Usuario;
                }
                else
                {
                    linea += "ya está marcado como enviado en la sucursal, pero no aparece en el histórico de envíos";
                }
                yaEnviados.Add(linea);
            }
            return yaEnviados;
        }

        // Los PP no salen de productos_por_embarcar, asi que Validar_folio no los detecta; se revisa el historico de envios
        private void QuitarPickupYaEnviados(DataTable auxF)
        {
            List<string> pickups = (from r in auxF.AsEnumerable()
                                    let nDoc = r["N° DOCUMENTO"].ToString().Trim()
                                    where nDoc.StartsWith("PP-")
                                    select nDoc).Distinct().ToList();
            if (pickups.Count == 0)
                return;

            Dictionary<string, Qrys.EnvioAnterior> ultimos = q.UltimoEnvioDocumentos(pickups);
            if (ultimos.Count == 0)
                return;

            StringBuilder msg = new StringBuilder("Estos pick-up ya se habían enviado a Unigis:" + Environment.NewLine);
            foreach (var envio in ultimos.OrderBy(u => u.Key))
                msg.AppendLine("   " + envio.Key + " (último envío " + envio.Value.Fecha.ToString("dd/MM/yyyy HH:mm") + ")");
            msg.AppendLine();
            msg.Append("¿Desea enviarlos otra vez?" + Environment.NewLine + "Sí = enviarlos de nuevo      No = quitarlos de este envío");

            if (MessageBox.Show(msg.ToString(), "Pick-up ya enviados", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                return;

            for (int i = auxF.Rows.Count - 1; i >= 0; i--)
            {
                if (ultimos.ContainsKey(auxF.Rows[i]["N° DOCUMENTO"].ToString().Trim()))
                    auxF.Rows[i].Delete();
            }
        }
    }
}

