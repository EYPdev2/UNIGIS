using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActualizadorDoctosUnigis
{
    public partial class GridBusqueda : Form
    {
        TextBox t1;
        TextBox t2;
        TextBox t3;
        DataTable dt;
        string u;
        Qrys q = new Qrys();
        ObtenerRutaCompleta_Response.ObtenerRutaCompletaResponse orcr = new ObtenerRutaCompleta_Response.ObtenerRutaCompletaResponse();
        public GridBusqueda()
        {
            InitializeComponent();
        }
        // Metodo que valida desde que formulario se esta solicitando la consulta de informacion 
        public GridBusqueda(TextBox t1A,TextBox t2A=null ,TextBox t3A=null)
        {
            t1 = t1A;
            t2 = t2A;
            t3 = t3A;
            InitializeComponent();
        }
        // Metodo que valida desde que formulario se esta solicitando la consulta de informacion  y el usuario que esta ingresado
        public GridBusqueda(TextBox t1A, string usuario, TextBox t2A = null, TextBox t3A = null)
        {
            t1 = t1A;
            t2 = t2A;
            t3 = t3A;
            InitializeComponent();
            u = usuario;
        }
        private void GridBusqueda_Load(object sender, EventArgs e)
        {
            



            if (t1.Name == "txt_Jornada")
            {

                


                dataGridView1.DataSource = q.Ruta("0",u);
            }
            if (t1.Name == "txt_viaje")
            {
                
                dataGridView1.DataSource = q.Viaje("0",u);
            }
            if(t1.Name == "txt_viajeV")
            {
               
                dataGridView1.DataSource = q.ViajeV("0", u);
            }

           

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (t1.Name == "txt_Jornada")
            {
                try
                {
                    t1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    t2.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    this.Close();
                }
                catch { }
            }
            if (t1.Name == "txt_viaje")
            {
                try
                {
                    t1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                 
                this.Close();
                }
                catch { }
            }
            if (t1.Name == "txt_viajeV")
            {
                try
                {
                    t1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                    this.Close();
                }
                catch { }
            }
        }

        private void chkIncluir_CheckedChanged(object sender, EventArgs e)
        {
            
            if (chkIncluir.Checked == true)
            {
                if (t1.Name == "txt_Jornada")
                {
                    dataGridView1.DataSource = q.Ruta("1 ",u);
                }
                if (t1.Name == "txt_viaje")
                {
                    dataGridView1.DataSource = q.Viaje("1",u);
                }
                if (t1.Name == "txt_viajeV")
                {
                    dataGridView1.DataSource = q.ViajeV("1", u);
                }
            }
            else
            {
                if (t1.Name == "txt_Jornada")
                {
                    dataGridView1.DataSource = q.Ruta("0",u);
                }
                if (t1.Name == "txt_viaje")
                {
                    dataGridView1.DataSource = q.Viaje("0",u);
                }
                if (t1.Name == "txt_viajeV")
                {

                    dataGridView1.DataSource = q.ViajeV("0", u);
                }
            }
        }


        public void ValidarViaje(ObtenerRutaCompleta_Response.ObtenerRutaCompletaResponse orc)
        {
            for (int x = 0; x < orc.d.Ordenes.Count; x++)
            {
                decimal peso = 0;
                int bultos = 0;
                for (int y = 0; y < orc.d.Ordenes[x].Items.Count; y++)
                {
                    peso = peso + Convert.ToDecimal(orc.d.Ordenes[x].Items[y].Peso);
                    bultos = bultos + Convert.ToInt16(orc.d.Ordenes[x].Items[y].Bulto);
                }
                orc.d.Ordenes[x].Peso = peso;
                orc.d.Ordenes[x].Bulto = bultos;
                orc.d.CantidadOrdenes = orc.d.Ordenes.Count;
               
            }
        }

        private void GridBusqueda_FormClosing(object sender, FormClosingEventArgs e)
        {
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "TOPF5", this.Top.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "LeftF5", this.Left.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "WidthF5", this.Width.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "HeightF5", this.Height.ToString());


        }

        private void button1_Click(object sender, EventArgs e)
        {

            xmlwriterOrden xml = new xmlwriterOrden();
            ObtenerRutaCompleta_clase orc = new ObtenerRutaCompleta_clase();
            DataTable dv = dataGridView1.DataSource as DataTable;
            ObtenerRutaCompleta_Response.ObtenerRutaCompletaResponse orcr = new ObtenerRutaCompleta_Response.ObtenerRutaCompletaResponse();
            for (int i = 0; i < dv.Rows.Count; i++)
            {
               

                
                orc.ApiKey = 1234;
                orc.IdJornada = Convert.ToInt32(dv.Rows[i]["IdJornada"].ToString()); 
                orc.IdRuta = Convert.ToInt32(dv.Rows[i]["IdRuta"].ToString());
                string SC = "";




                string jsonString = JsonConvert.SerializeObject(orc);
                using (var client = new HttpClient())
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3;
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                    var endpoint = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx/ObtenerRutaCompleta");
                    //var result = client.GetAsync(endpoint).Result;
                    //var xmlR = result.Content.ReadAsStringAsync().Result;

                    var paylod = new StringContent(jsonString, Encoding.UTF8, "application/json");
                   // var result = client.PostAsync(endpoint, paylod).Result.Content.ReadAsStringAsync().Result;
                    SC = client.PostAsync(endpoint, paylod).Result.StatusCode.ToString();
                    // MessageBox.Show(result);
                    // var x = JsonConvert.DeserializeObject(result);
                    //orcr = JsonConvert.DeserializeObject<ObtenerRutaCompleta_Response.ObtenerRutaCompletaResponse>(result);

                    
                    

                }
                if (SC=="OK")
                {
                    using (var client = new HttpClient())
                    {
                        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3;
                        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                        var endpoint = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx/ObtenerRutaCompleta");
                        //var result = client.GetAsync(endpoint).Result;
                        //var xmlR = result.Content.ReadAsStringAsync().Result;

                        var paylod = new StringContent(jsonString, Encoding.UTF8, "application/json");
                        var result = client.PostAsync(endpoint, paylod).Result.Content.ReadAsStringAsync().Result;
                        // var result2 = client.PostAsync(endpoint, paylod).Result.StatusCode.ToString();
                        // MessageBox.Show(result);
                        // var x = JsonConvert.DeserializeObject(result);
                        orcr = JsonConvert.DeserializeObject<ObtenerRutaCompleta_Response.ObtenerRutaCompletaResponse>(result);




                    }
                    if (orcr.d.Estado != "Confirmada")
                    {
                        q.ActualizarRuta(orc.IdJornada.ToString(), orc.IdRuta.ToString(), u);

                    }
                }
                else
                {
                  //  MessageBox.Show("esta vacio");
                    q.ActualizarRuta(orc.IdJornada.ToString(), orc.IdRuta.ToString(), u);
                }
                
                



            }
           
            
                

        }
    }
}
