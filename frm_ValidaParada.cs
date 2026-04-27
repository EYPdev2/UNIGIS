using ActualizadorDoctosUnigis.Contoller;
using ActualizadorDoctosUnigis.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using static ActualizadorDoctosUnigis.Models.Estructura_ParadaJS;

namespace ActualizadorDoctosUnigis
{
    public partial class frm_ValidaParada : Form
    { Qrys q = new Qrys();
        Libreria lib = new Libreria();
        ConsultarParadaPorIdResponse cpr = new ConsultarParadaPorIdResponse();
        List <ConsultarParadaPorIdResponse> Lcpr= new List< ConsultarParadaPorIdResponse > () ;
        DataTable dtg = new DataTable();
        ObtenerViaje_Response.Rootobject ov = new ObtenerViaje_Response.Rootobject();
        Embarque e = new Embarque();
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
                GridBusqueda gb = new GridBusqueda(txt_viaje,usuario);
                gb.Show();
                //ObtenerParadas(txt_viaje.Text);

            }
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_viaje.Text != "")
                {
                    ////if (!lib.validaEstab(usuario, q.Deposito_viaje(txt_viaje.Text)))
                    ////{
                    ////    MessageBox.Show("Solo se pueben Liberer Viajes de establecimientos asignados al Usuario", "Establecimiento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ////    return;
                    ////}
                    ObtenerParadas(txt_viaje.Text);
                    
                    // MessageBox.Show(soapBody.ToString());


                    //orcr = JsonConvert.DeserializeObject<ObtenerRutaCompleta_Response.ObtenerRutaCompletaResponse>(result);


                
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
        //Metodo que realiza una consulta a la web api de unigis para conusltar inoformacion de las paradas por medio de el id de viaje
        public void ObtenerParadas(string idviaje)
        {
            this.dtg.Clear();
            this.txt_viaje.Enabled = false;
            this.btn_canc.Enabled = true;
            DataTable dataTable = this.q.ParadasViaje(idviaje);
            this.consultarviaje(Convert.ToInt32(this.txt_viaje.Text));
            foreach (DataRow row1 in (InternalDataCollectionBase)dataTable.Rows)
            {
                this.button1.Enabled = false;
                obtenerParadas_request obtenerParadasRequest = new obtenerParadas_request();
                obtenerParadasRequest.ApiKey = "1234";
                obtenerParadasRequest.IdParada = Convert.ToInt32(row1.ItemArray[0].ToString());
                string content1 = JsonConvert.SerializeObject((object)obtenerParadasRequest);
                using (HttpClient httpClient = new HttpClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    Uri requestUri = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx/ConsultarParadaPorId");
                    StringContent content2 = new StringContent(content1, Encoding.UTF8, "application/json");
                    this.cpr = JsonConvert.DeserializeObject<ConsultarParadaPorIdResponse>(httpClient.PostAsync(requestUri, (HttpContent)content2).Result.Content.ReadAsStringAsync().Result);
                    this.Lcpr.Add(this.cpr);
                    this.button1.Enabled = true;
                    foreach (ConsultarParadaPorIdResponse.Items items in this.cpr.d.Items)
                    {
                        DataRow row2 = this.dtg.NewRow();
                        row2["IdParada"] = (object)row1.ItemArray[0].ToString();
                        row2["Ref.Documento"] = (object)this.cpr.d.RefDocumento;
                        row2["Producto"] = (object)items.RefDocumento;
                        row2["Cantidad"] = !(items.Cantidad == 0M) ? (object)items.Cantidad : (object)items.Bulto;
                        row2["Cantidad_Entregada"] = (object)items.CantidadEntregada;
                        row2["Estatus"] = (object)this.cpr.d.EstadoParada;
                        int idParada = obtenerParadasRequest.IdParada;
                        if (this.ObtenerParadaestado(idParada.ToString()).Trim() == "Entregado")
                            row2["Cantidad_Entregada"] = !(items.Cantidad == 0M) ? (object)items.Cantidad : (object)items.Bulto;
                        idParada = obtenerParadasRequest.IdParada;
                        if (this.ObtenerParadaestado(idParada.ToString()) == "Entrega parcial")
                        {
                            row2["Cantidad"] = !(items.Cantidad == 0M) ? (object)(items.Cantidad + items.CantidadEntregada) : (object)(items.Bulto + items.CantidadEntregada);
                            row2["Cantidad"] = (object)(items.Cantidad + items.CantidadEntregada);
                        }
                        this.dtg.Rows.Add(row2);
                    }
                    this.dataGridView1.DataSource = (object)this.dtg;
                    this.dataGridView1.Columns["Cantidad"].Width = 60;
                    this.dataGridView1.Columns["Producto"].Width = 60;
                }
            }
            this.GenerarEntysal(this.dataGridView1);
        }


        public void CargarInfo(List<ConsultarParadaPorIdResponse> datos )
        {
            

        }
        public string ObtenerParadaestado(string idparada)
        {
            string sql = "select top 1 Estado ,id from EYP_Unis_parada where IdParada='"+idparada+"' and Estado in ('Entregado','No Entregado', 'Entrega Parcial') order by id desc";
            DataTable de = q.Consultar(sql);
            return de.Rows[0].ItemArray[0].ToString();
        }

        public string obtener_estab(string deposito)
        {
            string sql = "select rtrim(Cod_estab) from eyp_servidores where unigis='" + deposito + "'";
            DataTable de = q.Consultar(sql);
            return de.Rows[0].ItemArray[0].ToString();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            xmlwriterParada xmlwriterParada = new xmlwriterParada();
            this.ObtenerParadas(this.txt_viaje.Text);
            if (this.dataGridView1.Rows.Count == 0)
            {
                int num = (int)MessageBox.Show("No Existe Nada que Actualizar");
            }
            else
            {
                foreach (ConsultarParadaPorIdResponse paradaPorIdResponse in this.Lcpr)
                {
                    string content1 = xmlwriterParada.stringtoxml("1234", paradaPorIdResponse.d.RefDocumento, paradaPorIdResponse.d.EstadoParada, Convert.ToString(paradaPorIdResponse.d.IdViaje), "true");
                    using (HttpClient httpClient = new HttpClient())
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        Uri requestUri = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx?op=ModificarEstadoParada");
                        StringContent content2 = new StringContent(content1, Encoding.UTF8, "text/xml");
                        string result = httpClient.PostAsync(requestUri, (HttpContent)content2).Result.Content.ReadAsStringAsync().Result;
                        this.cod_estab = this.ov.d.DepositoSalida.RefDepositoExterno.ToString();
                    }
                }

                var dataTable = q.Consultar($"select IdParada, unis2.id from EYP_Unis_parada unis with(nolock) outer apply (select MAX(id) as id from EYP_Unis_parada e with(nolock) where TipoObjeto = 'Parada' and e.IdParada = unis.IdParada and e.IdViaje = unis.IdViaje and Estado in ('Entregado', 'No Entregado', 'Entrega parcial')) unis2 where TipoObjeto = 'Parada' and ISNULL(unis.id, 0) = ISNULL(unis2.id, 0) and IdViaje = '{txt_viaje.Text.Trim()}'");
                foreach (DataRow row1 in (InternalDataCollectionBase)dataTable.Rows)
                {
                    obtenerParadas_request obtenerParadasRequest = new obtenerParadas_request();
                    obtenerParadasRequest.ApiKey = "1234";
                    obtenerParadasRequest.IdParada = Convert.ToInt32(row1.ItemArray[0].ToString());
                    string content = JsonConvert.SerializeObject(obtenerParadasRequest);
                    using (HttpClient httpClient = new HttpClient())
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                        var response = await httpClient.PostAsync(new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx/ConsultarParadaPorId"), new StringContent(content, Encoding.UTF8, "application/json"));

                        var text = await response.Content.ReadAsStringAsync();

                        var rootobject4 = JsonConvert.DeserializeObject<Estructura_ParadaJS.Rootobject>(text);

                        var fecha = ObtenerFecha(rootobject4);

                        q.Consultar($"update EYP_Unis_parada set fecha='{fecha:yyyy/MM/dd HH:mm}' where id = '{row1.ItemArray[1]}'");
                    }
                }

                this.dtg = this.dataGridView1.DataSource as DataTable;
                this.EmbarqueC();
                this.dtg.Clear();
                this.txt_viaje.Enabled = true;
                this.btn_canc.Enabled = false;
                this.dataGridView1.Refresh();
            }

            dtg = dataGridView1.DataSource as DataTable;
            //Embarque(dtg
            // q.ActualizarViaje(txt_viaje.Text,usuario);

            EmbarqueC();
            dtg.Clear();
            txt_viaje.Enabled = true;
            btn_canc.Enabled = false;
            dataGridView1.Refresh();





        }

        //Metodo que realiza una consulta a la web api de unigis para consultar  inoformacion del viaje por medio de el id de viaje
        public void consultarviaje( int viajeid)
        {
            ConsultaViajeID opr = new ConsultaViajeID();
            opr.ApiKey = "1234";
            opr.IdViaje = viajeid;
            string jsonString = JsonConvert.SerializeObject(opr);
            using (var client = new HttpClient())
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                var endpoint = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx/ConsultarViaje");
                //var result = client.GetAsync(endpoint).Result;
                //var xmlR = result.Content.ReadAsStringAsync().Result;

                var paylod = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var result = client.PostAsync(endpoint, paylod).Result.Content.ReadAsStringAsync().Result;
                // MessageBox.Show(result);
                    

                ov = JsonConvert.DeserializeObject<ObtenerViaje_Response.Rootobject>(result);
                cod_estab=ov.d.DepositoSalida.RefDepositoExterno.ToString();
            }
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

        public void EmbarqueC()
        {
            
            
             string FolioEmb = e.Insertar_embarque(obtener_estab(cod_estab), ov,usuario, txt_viaje.Text);
            Embarque(dtg, FolioEmb);
            

                
        }

        public void Embarque (DataTable dgv, string FolioE)
        {


               
            foreach (DataRow row in dgv.Rows)
            {
                Entrega ent = new Entrega(row, ov.d.Conductor.ReferenciaExterna, ov.d.Vehiculo.Dominio);
                e.Insertar_embarque_detalle(ent, obtener_estab (cod_estab), FolioE); //Revisar Tiempo de Ejecucion 


                //if (Convert.ToDouble(row["Cantidad_Entregada"].ToString()) > 0 && Convert.ToDouble(row["Cantidad_Entregada"].ToString())== Convert.ToDouble(row["Cantidad"].ToString()))
                //{
                //    //MessageBox.Show("Entregado"); 
                    
                //}
                //else if (Convert.ToDouble(row["Cantidad_Entregada"].ToString()) > 0 && Convert.ToDouble(row["cantidad"].ToString()) > 0)
                //{ MessageBox.Show("Entrega P");}
                
                //else {// MessageBox.Show("No Entregado ");
                //      }
            }
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

            dtg = dataGridView1.DataSource as DataTable;
            dtg.Clear();
            txt_viaje.Text = "";
            txt_viaje.Enabled = true;
            btn_canc.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs ev)
        {
           string servidora= e.obtenerServidor("44") ;

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

        private DateTimeOffset ObtenerFecha(Estructura_ParadaJS.Rootobject ejs)
        {
            //foreach (Estructura_ParadaJS.Bitacora bitacora in ejs.d.Bitacora)
            //{
            //    if (bitacora.bitacora.Contains("Nuevo: Entregado (3)") || bitacora.bitacora.Contains("Nuevo: No Entregado (2)") || bitacora.bitacora.Contains("Nuevo: Entrega parcial (4)"))
            //    {
            //        DateTime utcDate = DateTime.ParseExact(bitacora.Fecha, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
            //        DateTimeOffset utc = new DateTimeOffset(utcDate, TimeSpan.Zero);

            //        return utc.ToLocalTime();
            //    }
            //}

            var bitacora = ejs.d.Bitacora.OrderBy(b => b.Fecha).LastOrDefault(b => b.bitacora.Contains("Nuevo: Entregado (3)") || b.bitacora.Contains($"Nuevo: No Entregado (2)") || b.bitacora.Contains($"Nuevo: Entrega parcial (4)"));
            if (bitacora != null)
            {
                DateTime utcDate = DateTime.ParseExact(bitacora.Fecha, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                DateTimeOffset utc = new DateTimeOffset(utcDate, TimeSpan.Zero);

                return utc.ToLocalTime();
            }

            return DateTimeOffset.Now;
        }
    }
}
