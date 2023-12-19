using ActualizadorDoctosUnigis.Models;
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
    public partial class frm_paradas : Form
    {
        ObtenerPAradaid.ObtenerParadaResponse OP = new ObtenerPAradaid.ObtenerParadaResponse();
        public frm_paradas()
        {
            InitializeComponent();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            ObtenerPAradaid.consultar c = new ObtenerPAradaid.consultar();
                c.ApiKey = 1234;
            c.IdParada = 20;
            string jsonString = JsonConvert.SerializeObject(c);

            //MessageBox.Show(xml.stringtoxml(aux));
            //  string msj = xml.stringtoxml(aux).ToString();
            using (var client = new HttpClient())
            {

                var endpoint = new Uri("https://cloud-test.unigis.com/Grupo_EYP/mapi/soap/logistic/service.asmx/ConsultarParadaPorId");
                //var result = client.GetAsync(endpoint).Result;
                //var xmlR = result.Content.ReadAsStringAsync().Result;

                var paylod = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var result = client.PostAsync(endpoint, paylod).Result.Content.ReadAsStringAsync().Result;
                // MessageBox.Show(result);

                OP=JsonConvert.DeserializeObject<ObtenerPAradaid.ObtenerParadaResponse>(result);


            }
            Refrescar(OP);
        }

        public void Refrescar(ObtenerPAradaid.ObtenerParadaResponse obtenerParada  )
        {
            txt_estado.Text = obtenerParada.d.EstadoParada;
            txt_Docto.Text = obtenerParada.d.RefDocumento;
            lbl_IdViaje.Text = "Id Viaje : " + obtenerParada.d.IdViaje;
            txt_codcte.Text = obtenerParada.d.Cliente.RefCliente;
            txt_RazonSocial.Text = obtenerParada.d.Cliente.RazonSocial;
            txt_direccion.Text = obtenerParada.d.Direccion;
            lbl_latitud.Text = "Latitud : "+obtenerParada.d.Latitud.ToString();
            lbl_Longitud.Text = "Longitud : "+obtenerParada.d.Longitud.ToString();
            DataTable dt = new DataTable();

            DataColumn column;
            DataRow row;
            // Se tiene que crear primero la columna asignandole Nombre y Tipo de datos    
            column = new DataColumn();
            //column.DataType = System.Type.GetType("System.String");
            //column.ColumnName = "Documento";
            //dt.Columns.Add(column);
            // Se tienen que crear todas las columnas que queramos
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Producto";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Cantidad";

            dt.Columns.Add(column); column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Peso";
            dt.Columns.Add(column);

            for(int x = 0; x < obtenerParada.d.Items.Count; x++)
            {
                row = dt.NewRow();
              //  row["Documento"] = (obtenerParada.d.Items[x].RefDocumento.ToString());

                row["Producto"] = (obtenerParada.d.Items[x].RefDocumento.ToString().TrimEnd() )   ;

                row["Cantidad"] = (obtenerParada.d.Items[x].bulto.ToString());

                row["Peso"] = (obtenerParada.d.Items[x].Peso.ToString());
                dt.Rows.Add(row);
            }
            dataGridView1.DataSource = dt;
        }

        private void frm_paradas_Load(object sender, EventArgs e)
        {

        }
    }

}
