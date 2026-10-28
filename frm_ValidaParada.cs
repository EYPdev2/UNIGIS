using ActualizadorDoctosUnigis.Contoller;
using ActualizadorDoctosUnigis.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace ActualizadorDoctosUnigis
{
    public partial class frm_ValidaParada : Form
    { Qrys q = new Qrys();
        Libreria lib = new Libreria();
        // Solo las paradas en Validado del viaje cargado (IdParada -> respuesta de Unigis); se rehace en cada consulta
        Dictionary<int, ConsultarParadaPorIdResponse> paradasPorLiberar = new Dictionary<int, ConsultarParadaPorIdResponse>();
        DataTable dtg = new DataTable();
        ObtenerViaje_Response.Rootobject ov = new ObtenerViaje_Response.Rootobject();
        Embarque embarque = new Embarque();
        HttpClient client = new HttpClient();
        string cod_estab="";
        string viajeCargado = "";
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
                GridBusqueda gb = new GridBusqueda(txt_viaje,usuario);
                gb.Show();
                //ObtenerParadas(txt_viaje.Text);

            }
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_viaje.Text.Trim() != "")
                {
                    Cursor = Cursors.WaitCursor;
                    try
                    {
                        if (ObtenerParadas(txt_viaje.Text.Trim()))
                        {
                            if (dtg.Rows.Count == 0)
                                MessageBox.Show("No se encontraron paradas del viaje " + viajeCargado + ".", "Liberar Paradas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else if (paradasPorLiberar.Count == 0)
                                MessageBox.Show("El viaje " + viajeCargado + " no tiene paradas validadas pendientes de liberar. Las que ya se liberaron aparecen como Liberado.", "Liberar Paradas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                    }
                }
                else
                {
                    MessageBox.Show("Favor de Proporcionar el IDVIAJE");
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

            if (Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "TOPF3").ToString() != "")
            {
                this.Top = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "TOPF3").ToString());
                this.Left = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "LeftF3").ToString());
                this.Width = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "WidthF3").ToString());
                this.Height = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "HeightF3").ToString());
            }



        }
        //Metodo que consulta en Unigis el viaje y cada una de sus paradas; solo las paradas en Validado quedan listas para liberar
        public bool ObtenerParadas(string idviaje)
        {
            dtg.Clear();
            paradasPorLiberar.Clear();
            viajeCargado = "";
            button1.Enabled = false;
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            int viajeId;
            if (!int.TryParse(idviaje, out viajeId))
            {
                MessageBox.Show("El IDVIAJE " + idviaje + " no es válido.", "Liberar Paradas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            idviaje = viajeId.ToString();

            try
            {
                consultarviaje(viajeId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible consultar el viaje " + idviaje + " en Unigis." + Environment.NewLine + Environment.NewLine + Libreria.MensajeError(ex), "Liberar Paradas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!lib.UsuarioTieneDeposito(usuario, cod_estab))
            {
                MessageBox.Show("Solo se pueden liberar viajes de los depósitos asignados al usuario. El viaje " + idviaje + " es del depósito " + cod_estab + ".", "Liberar Paradas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            DataTable dataTable = q.ParadasViaje(idviaje);
            foreach (DataRow row1 in dataTable.Rows)
            {
                int idParada;
                if (!int.TryParse(row1.ItemArray[0].ToString().Trim(), out idParada) || idParada == 0)
                    continue;

                ConsultarParadaPorIdResponse parada;
                try
                {
                    parada = ConsultarParada(idParada);
                }
                catch (Exception ex)
                {
                    AgregarFilaAviso(idParada, "", "Error al consultar en Unigis: " + Libreria.MensajeError(ex));
                    continue;
                }

                // En Unigis la parada se puede mover a otro viaje conservando el mismo IdParada
                if (parada.d.IdViaje != viajeId)
                {
                    AgregarFilaAviso(idParada, parada.d.RefDocumento, "Movida al viaje " + parada.d.IdViaje);
                    continue;
                }

                string estatus = parada.d.EstadoParada;
                string estadoParada = ObtenerParadaestado(idParada.ToString(), idviaje).Trim();
                if (estatus == "Validado")
                {
                    // Sin estado de entrega no se sabe que cantidad embarcar, asi que no se libera
                    if (estadoParada == "")
                        estatus = "Validado sin estado de entrega";
                    else
                        paradasPorLiberar[idParada] = parada;
                }

                if (parada.d.Items == null)
                    continue;

                foreach (ConsultarParadaPorIdResponse.Items items in parada.d.Items)
                {
                    DataRow row2 = dtg.NewRow();
                    row2["IdParada"] = idParada.ToString();
                    row2["Ref.Documento"] = parada.d.RefDocumento;
                    row2["Producto"] = items.RefDocumento;
                    row2["Estatus"] = estatus;
                    if (!string.Equals(estadoParada, "Entrega parcial", StringComparison.OrdinalIgnoreCase))
                    {
                        row2["Cantidad"] = items.CantidadEntregada != 0 ? (items.CantidadEntregada + items.Cantidad) : items.Cantidad;

                        if (string.Equals(estadoParada, "Entregado", StringComparison.OrdinalIgnoreCase))
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
                    dtg.Rows.Add(row2);
                }
            }

            dataGridView1.DataSource = dtg;
            dataGridView1.Columns["Cantidad"].Width = 60;
            dataGridView1.Columns["Producto"].Width = 60;

            txt_viaje.Text = idviaje;
            txt_viaje.Enabled = false;
            btn_canc.Enabled = true;
            viajeCargado = idviaje;
            button1.Enabled = paradasPorLiberar.Count > 0;
            return true;
        }

        private void AgregarFilaAviso(int idParada, string referencia, string estatus)
        {
            DataRow row = dtg.NewRow();
            row["IdParada"] = idParada.ToString();
            row["Ref.Documento"] = referencia;
            row["Estatus"] = estatus;
            dtg.Rows.Add(row);
        }

        private ConsultarParadaPorIdResponse ConsultarParada(int idParada)
        {
            obtenerParadas_request obtenerParadasRequest = new obtenerParadas_request();
            obtenerParadasRequest.ApiKey = "1234";
            obtenerParadasRequest.IdParada = idParada;
            string content1 = JsonConvert.SerializeObject(obtenerParadasRequest);
            Uri requestUri = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx/ConsultarParadaPorId");
            StringContent content2 = new StringContent(content1, Encoding.UTF8, "application/json");
            ConsultarParadaPorIdResponse parada = JsonConvert.DeserializeObject<ConsultarParadaPorIdResponse>(client.PostAsync(requestUri, content2).Result.Content.ReadAsStringAsync().Result);
            if (parada == null || parada.d == null)
                throw new Exception("Unigis no regresó información de la parada " + idParada + ".");
            return parada;
        }

        public string ObtenerParadaestado(string idparada, string idviaje)
        {
            string sql = "select top 1 Estado ,id from EYP_Unis_parada where IdParada='"+idparada+ "' and IdViaje='" + idviaje + "' and Estado in ('Entregado','No Entregado', 'Entrega Parcial') order by id desc";
            DataTable de = q.Consultar(sql);
            if (de.Rows.Count == 0)
                return "";
            return de.Rows[0].ItemArray[0].ToString();
        }

        public string obtener_estab(string deposito)
        {
            string sql = "select rtrim(Cod_estab) from eyp_servidores where unigis='" + deposito + "'";
            DataTable de = q.Consultar(sql);
            return de.Rows[0].ItemArray[0].ToString();
        }

        private void button1_Click(object sender, EventArgs ev)
        {
            if (viajeCargado == "")
                return;

            string viaje = viajeCargado;
            Cursor = Cursors.WaitCursor;
            try
            {
                // Se vuelve a consultar Unigis justo antes de liberar para trabajar con el estado actual de cada parada
                if (!ObtenerParadas(viaje))
                    return;

                if (paradasPorLiberar.Count == 0)
                {
                    MessageBox.Show("El viaje " + viaje + " ya no tiene paradas validadas pendientes de liberar.", "Liberar Paradas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int otras = Math.Max(0, dtg.AsEnumerable().Select(r => r["IdParada"].ToString()).Distinct().Count() - paradasPorLiberar.Count);
                string confirmacion = "Se van a liberar en Unigis " + paradasPorLiberar.Count + " paradas validadas del viaje " + viaje + " y se generará su embarque.";
                if (otras > 0)
                    confirmacion += Environment.NewLine + otras + " paradas no están listas para liberar y no se tocan.";
                if (MessageBox.Show(confirmacion + Environment.NewLine + Environment.NewLine + "¿Continuar?", "Liberar Paradas", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                // 1. Primero el embarque en la sucursal; si falla no se toca Unigis y se puede reintentar
                ResultadoEmbarque resultado;
                try
                {
                    List<Entrega> entregas = new List<Entrega>();
                    foreach (DataRow row in dtg.Rows)
                    {
                        if (row["Estatus"].ToString().Trim() == "Validado")
                            entregas.Add(new Entrega(row, ov.d.Conductor.ReferenciaExterna, ov.d.Vehiculo.Dominio));
                    }
                    resultado = embarque.GenerarEmbarque(obtener_estab(cod_estab), ov, usuario, viaje, entregas);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se generó el embarque del viaje " + viaje + " y no se liberó ninguna parada en Unigis." + Environment.NewLine + Environment.NewLine + Libreria.MensajeError(ex), "Liberar Paradas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. Despues Unigis, verificando que cada parada quede en Liberado
                List<string> noLiberadas = new List<string>();
                foreach (KeyValuePair<int, ConsultarParadaPorIdResponse> parada in paradasPorLiberar)
                {
                    string motivo;
                    try
                    {
                        motivo = LiberarParada(parada.Key, parada.Value);
                    }
                    catch (Exception ex)
                    {
                        motivo = Libreria.MensajeError(ex);
                    }
                    if (motivo != "")
                        noLiberadas.Add(parada.Value.d.RefDocumento + " (parada " + parada.Key + "): " + motivo);
                }

                MostrarResumen(viaje, resultado, paradasPorLiberar.Count, noLiberadas);
                Limpiar();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        // Regresa "" si la parada quedo en Liberado en Unigis; si no, el motivo
        private string LiberarParada(int idParada, ConsultarParadaPorIdResponse parada)
        {
            xmlwriterParada xmlwriterParada = new xmlwriterParada();
            string content1 = xmlwriterParada.stringtoxml("1234", parada.d.RefDocumento, parada.d.EstadoParada, Convert.ToString(parada.d.IdViaje), "true");
            Uri requestUri = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx?op=ModificarEstadoParada");
            StringContent content2 = new StringContent(content1, Encoding.UTF8, "text/xml");
            HttpResponseMessage respuesta = client.PostAsync(requestUri, content2).Result;
            if (!respuesta.IsSuccessStatusCode)
                return Libreria.ErrorSoap(respuesta.Content.ReadAsStringAsync().Result) ?? ("Unigis respondió " + (int)respuesta.StatusCode + " " + respuesta.ReasonPhrase);

            string estado = ConsultarParada(idParada).d.EstadoParada;
            return estado == "Liberado" ? "" : "Unigis la dejó en " + estado;
        }

        private void MostrarResumen(string viaje, ResultadoEmbarque resultado, int paradas, List<string> noLiberadas)
        {
            StringBuilder msg = new StringBuilder("Viaje " + viaje + Environment.NewLine + Environment.NewLine);
            if (resultado.Creado)
                msg.AppendLine("Embarque generado: " + resultado.Folio + " (" + resultado.Partidas + " partidas" + (resultado.Partidas == 0 ? "; los productos no entregados regresaron a pendientes de envío" : "") + ").");
            else
                msg.AppendLine("No se generó embarque: ningún producto validado tenía pendiente por embarcar.");
            msg.AppendLine("Paradas liberadas en Unigis: " + (paradas - noLiberadas.Count) + " de " + paradas + ".");

            if (noLiberadas.Count > 0)
            {
                msg.AppendLine();
                msg.AppendLine("No se pudieron liberar en Unigis. Vuelva a cargar el viaje y presione Liberar para reintentar (lo ya embarcado no se vuelve a embarcar):");
                foreach (string linea in noLiberadas)
                    msg.AppendLine("   " + linea);
            }

            if (resultado.SinPendiente.Count > 0)
            {
                msg.AppendLine();
                msg.AppendLine("Sin pendiente por embarcar (no se agregaron al embarque):");
                foreach (string linea in resultado.SinPendiente.Take(15))
                    msg.AppendLine("   " + linea);
                if (resultado.SinPendiente.Count > 15)
                    msg.AppendLine("   ... y " + (resultado.SinPendiente.Count - 15) + " más");
            }

            MessageBox.Show(msg.ToString().TrimEnd(), "Liberar Paradas", MessageBoxButtons.OK, noLiberadas.Count > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
        }

        private void Limpiar()
        {
            dtg.Clear();
            paradasPorLiberar.Clear();
            viajeCargado = "";
            txt_viaje.Text = "";
            txt_viaje.Enabled = true;
            btn_canc.Enabled = false;
            button1.Enabled = false;
            txt_viaje.Focus();
        }

        //Metodo que realiza una consulta a la web api de unigis para consultar  inoformacion del viaje por medio de el id de viaje
        public void consultarviaje( int viajeid)
        {
            ConsultaViajeID opr = new ConsultaViajeID();
            opr.ApiKey = "1234";
            opr.IdViaje = viajeid;
            string jsonString = JsonConvert.SerializeObject(opr);

            var endpoint = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx/ConsultarViaje");
            var paylod = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var result = client.PostAsync(endpoint, paylod).Result.Content.ReadAsStringAsync().Result;

            ov = JsonConvert.DeserializeObject<ObtenerViaje_Response.Rootobject>(result);
            if (ov == null || ov.d == null || ov.d.DepositoSalida == null || ov.d.DepositoSalida.RefDepositoExterno == null)
                throw new Exception("El viaje no existe en Unigis o no tiene depósito de salida.");
            cod_estab = ov.d.DepositoSalida.RefDepositoExterno.ToString().Trim();
        }
    public void GenerarEntysal(DataGridView dv1) { 
                
            
        }

        public void InsertEmbarque(ObtenerViaje.Rootobject ova)
        {
            ova.d.RefDocumento.ToString();
            ova.d.Conductor.ReferenciaExterna.ToString();
            ova.d.FechaInicioReal.ToString();
            ova.d.FechaFinReal.ToString();
            ova.d.Vehiculo.ReferenciaExterna.ToString(); 
            
        }

        private void txt_viaje_TextChanged(object sender, EventArgs e)
        {

        }

        private void frm_ValidaParada_FormClosing(object sender, FormClosingEventArgs e)
        {
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "TOPF3", this.Top.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "LeftF3", this.Left.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "WidthF3", this.Width.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "HeightF3", this.Height.ToString());


        }

        private void btn_canc_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void button2_Click(object sender, EventArgs ev)
        {
           string servidora= embarque.obtenerServidor("44") ;

              string servidor = servidora.Substring(0, servidora.IndexOf('-'));
            string BD = servidora.Substring(servidora.IndexOf('-') + 1);
            DataTable dt = new DataTable();
            SqlConnection openConA = new SqlConnection("Data Source = " + servidor + "; Initial Catalog = " + BD + "; User ID = sa; Password = @Sistemas74");

            using (SqlConnection openCon = new SqlConnection("Data Source = " + servidor + "; Initial Catalog = " + BD + "; User ID = sa; Password = @Sistemas74"))
            //using (SqlConnection openCon = new SqlConnection("Data Source = 192.168.8.4\\bms; Initial Catalog = BMS03082022; User ID = sa; Password = @Sistemas74"))
            {

                StringBuilder qry = new StringBuilder();
                qry.Append(" select top 1 * from facremtick ");
                string insert = qry.ToString();
                using (SqlCommand querySaveStaff = new SqlCommand(insert, openCon))
                {
                    SqlCommand cmd = new SqlCommand();

                    querySaveStaff.Connection = openCon;
                    cmd.Connection = openCon;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = insert;
                    SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                    sqlDA.Fill(dt);

                }



            }
            MessageBox.Show(dt.Rows[0].ItemArray[0].ToString());
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
