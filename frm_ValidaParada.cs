using ActualizadorDoctosUnigis.Contoller;
using ActualizadorDoctosUnigis.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

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
        public void ObtenerParadas(string idviaje )
        {
            dtg.Clear();
            txt_viaje.Enabled = false;
            btn_canc.Enabled = true;

           DataTable dt= q.ParadasViaje(idviaje);
            consultarviaje(Convert.ToInt32(txt_viaje.Text));
            foreach (DataRow dr in dt.Rows)
            {
                button1.Enabled = false;
          
                obtenerParadas_request opr = new obtenerParadas_request();
                opr.ApiKey = "1234";
                opr.IdParada =Convert.ToInt32(dr.ItemArray[0].ToString());// Convert.ToInt32(txt_viaje.Text);
                string jsonString = JsonConvert.SerializeObject(opr);
                using (var client = new HttpClient())
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3;
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                    var endpoint = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx/ConsultarParadaPorId");
                    //var result = client.GetAsync(endpoint).Result;
                    //var xmlR = result.Content.ReadAsStringAsync().Result;

                    var paylod = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, paylod).Result.Content.ReadAsStringAsync().Result;
                    // MessageBox.Show(result);


                    cpr = JsonConvert.DeserializeObject<ConsultarParadaPorIdResponse>(result);
                    
                    Lcpr.Add(cpr);
                    button1.Enabled = true;
                   foreach( var subarray in cpr.d.Items)
                    {
                        
                        DataRow row1 = dtg.NewRow();
                      
                         
                        
                        //  if (cpr.d.EstadoParada == "Validado")
                        //{
                            row1["IdParada"] = dr.ItemArray[0].ToString();
                            row1["Ref.Documento"] = cpr.d.RefDocumento;
                            row1["Producto"] = subarray.RefDocumento;
                            
                            if (subarray.Cantidad == 0)
                        {
                            row1["Cantidad"] = subarray.Bulto;
                        }else { row1["Cantidad"] = subarray.Cantidad; }
                            

                        row1["Cantidad_Entregada"] = subarray.CantidadEntregada;
                            row1["Estatus"] = cpr.d.EstadoParada;
                            if (ObtenerParadaestado(opr.IdParada.ToString()) == "Entregado")
                            {
                                
                            if (subarray.Cantidad == 0)
                            {
                                row1["Cantidad_Entregada"] = subarray.Bulto;
                            }
                            else { row1["Cantidad_Entregada"] = subarray.Cantidad; }


                        }

                            if (ObtenerParadaestado(opr.IdParada.ToString()) == "Entrega parcial")
                            {
                            if (subarray.Cantidad == 0)
                            {
                                row1["Cantidad"] = subarray.Bulto + subarray.CantidadEntregada;
                            }
                            else { row1["Cantidad"] = subarray.Cantidad + subarray.CantidadEntregada; }

                            row1["Cantidad"] = subarray.Cantidad + subarray.CantidadEntregada;
                           
                        
                        }
                        //if (cpr.d.EstadoParada != "Liberado")
                        //{
                            dtg.Rows.Add(row1);
                        //}
                        //}

                        //if (q.EstatusParada(cpr.d.RefDocumento) == "Entregado")
                        //{
                        //     row1["Cantidad_Entregada"] = subarray.Cantidad;
                        //}
                        //if (q.EstatusParada(cpr.d.RefDocumento) == "Entrega parcial")
                        //{
                        //    row1["Cantidad"] = subarray.Cantidad + subarray.CantidadEntregada;
                        //}

                    }
                    dataGridView1.DataSource = dtg;
                    dataGridView1.Columns["Cantidad"].Width = 60;
                    dataGridView1.Columns["Producto"].Width = 60;

                }




            }
              GenerarEntysal(dataGridView1);
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

        private void button1_Click(object sender, EventArgs e)
        {
            xmlwriterParada xml = new xmlwriterParada();
            
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No Existe Nada que Actualizar");
                return;
            }

            foreach (var subarray in Lcpr)
            {
                string xmls = (xml.stringtoxml("1234", subarray.d.RefDocumento, subarray.d.EstadoParada, Convert.ToString(subarray.d.IdViaje), "true"));

                using (var client = new HttpClient())
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3;
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                    var endpoint = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx?op=ModificarEstadoParada");
                    //var result = client.GetAsync(endpoint).Result;
                    //var xmlR = result.Content.ReadAsStringAsync().Result;

                    var paylod = new StringContent(xmls, Encoding.UTF8, "text/xml");
                    var result = client.PostAsync(endpoint, paylod).Result.Content.ReadAsStringAsync().Result;

                    // MessageBox.Show(result);
                    cod_estab = ov.d.DepositoSalida.RefDepositoExterno.ToString();

                    //MessageBox.Show(result);
                }
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
    }

}
