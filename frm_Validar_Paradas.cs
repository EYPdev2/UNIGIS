using ActualizadorDoctosUnigis.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActualizadorDoctosUnigis
{
    public partial class frm_Validar_Paradas : Form
    {
        Qrys q = new Qrys();
        DataGridViewButtonColumn button = new DataGridViewButtonColumn();
        DataGridViewButtonColumn button2 = new DataGridViewButtonColumn();
        HttpClient client = new HttpClient();
        xmlwriterParada xml = new xmlwriterParada();
        string usuario;
        string z;
        public frm_Validar_Paradas()
        {
            InitializeComponent();
        }

        public frm_Validar_Paradas(string u)
        {
            InitializeComponent();
            usuario = u;
            dataGridView1.ForeColor = Color.Black;
        }

        private void frm_Validar_Paradas_Load(object sender, EventArgs e)
        {
            if (Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "TOPF4").ToString() != "")
            {
                this.Top = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "TOPF4").ToString());
                this.Left = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "LeftF4").ToString());
                this.Width = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "WidthF4").ToString());
                this.Height = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "HeightF4").ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = q.consultarviaje(Convert.ToInt32(txt_viajeV.Text));

            button.Name = "Validar";
            button.HeaderText = "Validar";
            button.Text = "Validar";
            button.UseColumnTextForButtonValue = true;

            button2.Name = "Detalles";
            button2.HeaderText = "Detalles";
            button2.Text = "Detalles";
            button2.UseColumnTextForButtonValue = true;
            button2.Width = 50;
            this.dataGridView1.Columns.Add(button);
            this.dataGridView1.Columns.Add(button2);
            z = q.Deposito_viaje(txt_viajeV.Text);
            colorear(dataGridView1);
        }

        private void button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("si funciona");

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                if (senderGrid.Columns[e.ColumnIndex].Name == "Validar")
                {
                    //if (dataGridView1.CurrentRow.Cells[3].Value.ToString() == "Validado")
                    //    return;

                    PARADA_JSON PJ = new PARADA_JSON();
                    Estructura_ParadaJS.Rootobject EJS = new Estructura_ParadaJS.Rootobject();
                    PJ.ApiKey = 1234;
                    PJ.IdParada = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value);
                    string jsonString = JsonConvert.SerializeObject(PJ);
                    //  MessageBox.Show(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    var endpoint = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx/ConsultarParadaPorId");
                    //var result = client.GetAsync(endpoint).Result;
                    //var xmlR = result.Content.ReadAsStringAsync().Result;
                    var paylod = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, paylod).Result.Content.ReadAsStringAsync().Result;
                    EJS = JsonConvert.DeserializeObject<Estructura_ParadaJS.Rootobject>(result);
                    DataTable dtC = q.CoordenadasDeposito(z);
                    string SXML = xml.stringtoxmlM(EJS, Convert.ToDouble(dtC.Rows[0].ItemArray[0].ToString()), Convert.ToDouble(dtC.Rows[0].ItemArray[1].ToString()), dataGridView1.CurrentRow.Cells[1].Value.ToString());

                    var endpoint2 = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx?op=ModificarEstadoParada");
                    //var result = client.GetAsync(endpoint).Result;
                    //var xmlR = result.Content.ReadAsStringAsync().Result;
                    var paylod2 = new StringContent(SXML, Encoding.UTF8, "text/xml");
                    var result2 = client.PostAsync(endpoint2, paylod2).Result.StatusCode.ToString();

                    if (result2 =="OK" )
                    {
                        MessageBox.Show("Actualización Correcta");
                        dataGridView1.Columns.Clear();
                        dataGridView1.DataSource = q.consultarviaje(Convert.ToInt32(txt_viajeV.Text));

                        button.Name = "Validar";
                        button.HeaderText = "Validar";
                        button.Text = "Validar";
                        button.UseColumnTextForButtonValue = true;

                        button2.Name = "Detalles";
                        button2.HeaderText = "Detalles";
                        button2.Text = "Detalles";
                        button2.UseColumnTextForButtonValue = true;
                        button2.Width = 50;
                        this.dataGridView1.Columns.Add(button);
                        this.dataGridView1.Columns.Add(button2);
                        z = q.Deposito_viaje(txt_viajeV.Text);
                        colorear(dataGridView1);
                    }

                }
                if (senderGrid.Columns[e.ColumnIndex].Name == "Detalles")
                {

                    PARADA_JSON PJ = new PARADA_JSON();
                    Estructura_ParadaJS.Rootobject EJS = new Estructura_ParadaJS.Rootobject();
                    PJ.ApiKey = 1234;
                    PJ.IdParada = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value);
                    string jsonString = JsonConvert.SerializeObject(PJ);
                    //  MessageBox.Show(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    var endpoint = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx/ConsultarParadaPorId");
                    //var result = client.GetAsync(endpoint).Result;
                    //var xmlR = result.Content.ReadAsStringAsync().Result;
                    var paylod = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, paylod).Result.Content.ReadAsStringAsync().Result;
                    EJS = JsonConvert.DeserializeObject<Estructura_ParadaJS.Rootobject>(result);
                    DataTable dtC = q.Consultar("select top 1 Estado from EYP_Unis_parada where idParada='" + dataGridView1.CurrentRow.Cells[1].Value + "' and idViaje='" + dataGridView1.CurrentRow.Cells[0].Value + "' and Estado not in ('Validado','Liberado') order by id desc");
                    //MessageBox.Show(dtC.Rows[0].ItemArray[0].ToString());
                    // xml.stringtoxmlM(EJS, Convert.ToDouble(dtC.Rows[0].ItemArray[0].ToString()), Convert.ToDouble(dtC.Rows[0].ItemArray[0].ToString()), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    // itemstoDT(EJS, dtC.Rows[0].ItemArray[0].ToString());
                    frm_detalle_parada dtp = new frm_detalle_parada(dataGridView1.CurrentRow.Cells[1].Value.ToString(), dtC.Rows[0].ItemArray[0].ToString(), itemstoDT(EJS, dtC.Rows[0].ItemArray[0].ToString()));
                    dtp.ShowDialog();
                }


            }
        }

        private void colorear(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[3].Value.ToString() == "No Entregado")
                {
                    row.Cells[4].ReadOnly = false;
                    row.Cells[3].Style.BackColor = Color.Red;
                    row.Cells[3].Style.ForeColor = Color.White;
                }
                if (row.Cells[3].Value.ToString() == "Validado")
                {

                    row.Cells[3].Style.BackColor = Color.Peru;
                    row.Cells[3].Style.ForeColor = Color.White;
                }
                if (row.Cells[3].Value.ToString() == "Entregado")
                {

                    row.Cells[3].Style.BackColor = Color.Green;
                    row.Cells[3].Style.ForeColor = Color.White;
                }
                if (row.Cells[3].Value.ToString() == "Entrega Parcial")
                {

                    row.Cells[3].Style.BackColor = Color.Green;
                    row.Cells[3].Style.ForeColor = Color.White;
                }


            }

        }
        private DataTable itemstoDT(Estructura_ParadaJS.Rootobject EJS, string Estado)
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("Producto", typeof(String));
            dt.Columns.Add(dc);
            dc = new DataColumn("Descripcion", typeof(String));
            dt.Columns.Add(dc);
            dc = new DataColumn("Cantidad", typeof(String));
            dt.Columns.Add(dc);
            dc = new DataColumn("Cantidad Entregada", typeof(String));
            dt.Columns.Add(dc);
            dc = new DataColumn("Motivo", typeof(String));
            dt.Columns.Add(dc);
            for (int x = 0; x < EJS.d.Items.Length; x++)
            {

                DataRow dr = dt.NewRow();

                dr[0] = EJS.d.Items[x].RefDocumento.ToString();
                dr[1] = EJS.d.Items[x].Descripcion.ToString();
                dr[4] = "-";

                if (Estado == "Entrega parcial")
                {
                    //dr[0] = EJS.d.Items[x].Motivo.ToString();
                    dr[2] = (EJS.d.Items[x].Cantidad + EJS.d.Items[x].CantidadEntregada).ToString();
                    dr[3] = EJS.d.Items[x].CantidadEntregada.ToString();

                    if (EJS.d.Items[x].Motivo  is null)
                    {
                        dr[4] = "-";
                    }
                    else
                    dr[4] = EJS.d.Items[x].Motivo.ToString();
                }
                else if (Estado == "Entregado")
                {
                    dr[2] = EJS.d.Items[x].Cantidad.ToString();
                    dr[3] = EJS.d.Items[x].Cantidad.ToString();
                }
                else
                {
                    dr[2] = EJS.d.Items[x].Cantidad.ToString();
                    dr[3] = 0;
                }



                dt.Rows.Add(dr);
            }


            return dt;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F2")
            {
                GridBusqueda gb = new GridBusqueda(txt_viajeV, usuario);
                gb.Show();

            }
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_viajeV.Text != "")
                {
                    ////if (!lib.validaEstab(usuario, q.Deposito_viaje(txt_viaje.Text)))
                    ////{
                    ////    MessageBox.Show("Solo se pueben Liberer Viajes de establecimientos asignados al Usuario", "Establecimiento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ////    return;
                    ////}
                    ///
                    string x = "select Deposito_Ref from EYP_Unis_parada  where IdViaje = '" + txt_viajeV.Text + "' and tipoobjeto = 'Viaje' and Estado = 'Inactivo'";

                    DataTable auth = q.Consultar("select Deposito_Ref from EYP_Unis_parada  where IdViaje = '"+txt_viajeV.Text+"' and tipoobjeto = 'Viaje' and Estado = 'Inactivo'");

                    if (!EstabUser(usuario, auth.Rows[0].ItemArray[0].ToString()))
                    {
                        MessageBox.Show("El usuario no tiene Permiso para realizar movimientos en el Deposito :" + auth.Rows[0].ItemArray[0].ToString(), "ALERTA", MessageBoxButtons.OK);
                        txt_viajeV.Text = "";
                        return;
                    }


                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = q.consultarviaje(Convert.ToInt32(txt_viajeV.Text));

                    button.Name = "Validar";
                    button.HeaderText = "Validar";
                    button.Text = "Validar";
                    button.UseColumnTextForButtonValue = true;

                    button2.Name = "Detalles";
                    button2.HeaderText = "Detalles";
                    button2.Text = "Detalles";
                    button2.UseColumnTextForButtonValue = true;
                    button2.Width = 50;
                    this.dataGridView1.Columns.Add(button);
                    this.dataGridView1.Columns.Add(button2);
                    z = q.Deposito_viaje(txt_viajeV.Text);
                    colorear(dataGridView1);
                }
                else
                {
                    MessageBox.Show("Favor de Proporcionar el IDVIAJE");
                }

                // MessageBox.Show(soapBody.ToString());


                //orcr = JsonConvert.DeserializeObject<ObtenerRutaCompleta_Response.ObtenerRutaCompletaResponse>(result);


            }
            
        }
        public bool EstabUser(string U, string deposito)
        {
            DataTable du;
            DataTable dtX = new DataTable();
            DataColumn dc = new DataColumn();
            dc.ColumnName = "Nombre";
            dtX.Columns.Add(dc);
            DataColumn dc2 = new DataColumn();
            dc2.ColumnName = "cod_estab";
            dtX.Columns.Add(dc2);
            du = q.Consultar("select isnull(es.Unigis,'') from establecimientos_usuario eu " +
                             "left join usuarios u on eu.usuario = u.usuario " +
                             "left join Eyp_Servidores es on es.Cod_estab=eu.cod_estab " +
                             " Where u.usuario = '" + U + "' and(u.multi_establecimiento = 1 or u.cod_estab = eu.cod_estab) and acceso = '1' and acceso='1'");
            bool f = false;
            foreach (DataRow row2 in du.Rows)
            {
                if (row2.ItemArray[0].ToString() == deposito)
                {
                    f = true;
                }
            }
            return f;
        }

        private void txt_viajeV_TextChanged(object sender, EventArgs e)
        {

        }

        private void frm_Validar_Paradas_FormClosing(object sender, FormClosingEventArgs e)
        {
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "TOPF4", this.Top.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "LeftF4", this.Left.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "WidthF4", this.Width.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "HeightF4", this.Height.ToString());


        }
    }
}

