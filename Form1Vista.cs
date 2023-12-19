using ActualizadorDoctosUnigis.Contoller;
using ActualizadorDoctosUnigis.Models;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ClosedXML.Excel;
using System.Diagnostics;

namespace ActualizadorDoctosUnigis
{

    public partial class Form1Vista : Form
    {
        Libreria lib = new Libreria();
        Qrys q = new Qrys();
        DataTable dt;
        DataTable DTV;
        DataTable DTOCD = new DataTable();
        Decimal Min = 24;
        bool nEventsFired = false;
        Decimal Max = 0;
        Decimal Mid = 0;
        Decimal Steps = Decimal.MinValue;
        Decimal MaxV = 0;
        decimal minv = 0;
        string cod_estab;
        string Estab = "";
        DataTable DAux = new DataTable();
        HttpClient client = new HttpClient();

        DataTable DTKILOS = new DataTable();
        DataTable Daux = new DataTable();
        string user;
        public Form1Vista()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(Control_KeyPress);
          this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        public Form1Vista(string u)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(Control_KeyPress);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            user = u;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Deseas enviar la informacion seleccionada a Unigis una vez se envie no podra eliminarse", "Informacion ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result1 == System.Windows.Forms.DialogResult.Yes)
            {
                xmlwriterOrden xml = new xmlwriterOrden();
                DataTable dtLocalC = new DataTable();
                dtLocalC = DivDT(dt, dtLocalC, "");

                var x = (from r in dtLocalC.AsEnumerable()
                         select r["N° DOCUMENTO"]).Distinct().ToList();
                List<object> doc = (from r in dtLocalC.AsEnumerable()
                                    select r["N° DOCUMENTO"]).Distinct().ToList();
                DataTable DTPickup = validar_Pickup(doc);
                DataTable aux = new DataTable();
                string msg = "Actualizacion Correcta :" + Environment.NewLine;

                foreach (var item in doc)
                {
                    aux = DivDT(dtLocalC, aux, item.ToString());
                    //MessageBox.Show(xml.stringtoxml(aux));
                    //  string msj = xml.stringtoxml(aux).ToString();

                    System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    var endpoint = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx?op=CrearOrdenesPedido");
                    //var result = client.GetAsync(endpoint).Result;
                    //var xmlR = result.Content.ReadAsStringAsync().Result;
                    var paylod = new StringContent(xml.stringtoxml(aux), Encoding.UTF8, "text/xml");
                    string y = (xml.stringtoxml(aux));
                    var result = client.PostAsync(endpoint, paylod).Result.StatusCode.ToString();

                    if (result == "OK")
                    {
                        msg = msg + "Documento " + item.ToString() + Environment.NewLine;
                        ActualizarStatusPPE(aux,cod_estab);
                        bnt_Refrescar.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("No se ha podido Enviar el Documento");
                    }



                    aux.Clear();

                }

                DataTable aux2 = new DataTable();
                List<object> pp = (from r in DTPickup.AsEnumerable()
                                   select r["N° DOCUMENTO"]).Distinct().ToList();


                foreach (var itemP in pp)
                {
                    aux = DivDT(DTPickup, aux2, itemP.ToString());
                    //MessageBox.Show(xml.stringtoxml(aux));
                    //  string msj = xml.stringtoxml(aux).ToString();

                    System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    var endpoint = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx?op=CrearOrdenesPedido");
                    //var result = client.GetAsync(endpoint).Result;
                    //var xmlR = result.Content.ReadAsStringAsync().Result;
                    var paylod = new StringContent(xml.stringtoxml(aux2), Encoding.UTF8, "text/xml");
                    var y = (xml.stringtoxml(aux2));
                    var result = client.PostAsync(endpoint, paylod).Result.StatusCode.ToString();

                    if (result == "OK")
                    {
                        msg = msg + "Documento " + itemP.ToString() + Environment.NewLine;
                        ActualizarStatusPPE(aux2,cod_estab);
                        bnt_Refrescar.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("No se ha podido Enviar el Documento");
                    }
                }
                MessageBox.Show(msg, "Actualizacion de Documentos ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // MessageBox.Show(msg,"Actualizacion",MessageBoxButtons.OK);
                //

                //Qrys q = new Qrys();
                //DataTable dt = q.selectitems( );
                //MessageBox.Show(
                //xml.stringtoxml(dt));
                //using (var client = new HttpClient())
                //{
                //    var endpoint = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx?op=CrearOrdenesPedido");
                //    //var result = client.GetAsync(endpoint).Result;
                //    //var xmlR = result.Content.ReadAsStringAsync().Result;
                //    var paylod = new StringContent(xml.stringtoxml(dt), Encoding.UTF8, "text/xml");
                //    var result = client.PostAsync(endpoint, paylod).Result.Content.ReadAsStringAsync().Result;
                //    MessageBox.Show(result.ToString());
                //}
                aux.Clear();
                dataGridView1.Sort(dataGridView1.Columns["FechaEntrega"], ListSortDirection.Ascending);
                SetMinMax(dataGridView1, "400",0);
                ColorCells(dataGridView1,0);
            }

        }
        public int PosicionDR(String Abreviatura)
        {
            int J = 0;
            for (int x = 0; x < Daux.Rows.Count; x++)
            {
                if (Abreviatura == Daux.Rows[x].ItemArray[0].ToString())
                {
                    return x;
                }

            }

            return J;
        }
        private void Form1Vista_Load(object sender, EventArgs e)
        {
            
            

            //  var tamaño= Screen.PrimaryScreen.Bounds.Height;
            //MessageBox.Show(tamaño.ToString());
            ActualizarVales.RunWorkerAsync();


            //Form2 frm2 = new Form2();
            //frm2.Show();
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            //chk.HeaderText = "Enviar";
            //dataGridView1.Columns.Add(chk);

            DataTable DTCE = new DataTable();
            DTCE = q.catalogoEstab();
            DTCE = EstabUser(user, DTCE);
            DTOCD.Columns.Add("Delivery");
            DTOCD.Columns.Add("PICKUP");
            DTOCD.Columns.Add("Tipo");

            button1.Enabled = false;
            //dt = q.selectitems();
            foreach (DataRow row in DTCE.Rows)
            {
                comboBox1.Items.Add(row[0].ToString());
            }

            //dataGridView1.DataSource = dt;

            //if (dataGridView1.Rows.Count > 0)
            //{
            //    foreach (DataGridViewColumn col in dataGridView1.Columns)
            //    {
            //        if (col.Index == 0 || col.Name == "CANTIDAD")
            //            col.ReadOnly = false;
            //        else
            //            col.ReadOnly = true;
            //    }
            //}
            //dataGridView1.Sort(dataGridView1.Columns["FechaEntrega"],ListSortDirection.Ascending);
            //SetMinMax(dataGridView1, "400");
            //ColorCells(dataGridView1);
        }
        private DataTable DivDT(DataTable DTO, DataTable DTA, string folio)
        {

            //DataTable dtLocalC = new DataTable();
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
            }


            DataRow drLocal = null;
            if (folio == "")
            {
                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    bool bChecked = Convert.ToBoolean(dr.Cells[0].Value);
                    if (bChecked)
                    {
                        drLocal = DTA.NewRow();
                        drLocal["Enviar"] = dr.Cells[0].ToString();
                        drLocal["N° DOCUMENTO"] = dr.Cells["N° DOCUMENTO"].Value.ToString();
                        drLocal["FECHA_DOC"] = Convert.ToDateTime(dr.Cells["FECHA_DOC"].Value);
                        drLocal["FechaEntrega"] = Convert.ToDateTime(dr.Cells["FechaEntrega"].Value);
                        drLocal["cod_cte"] = dr.Cells["cod_cte"].Value;
                        drLocal["razon_social"] = dr.Cells["razon_social"].Value;
                        drLocal["tel1"] = dr.Cells["tel1"].Value;
                        drLocal["tel2"] = dr.Cells["tel2"].Value;
                        drLocal["e_mail"] = dr.Cells["e_mail"].Value;
                        drLocal["Peso_KG"] = dr.Cells["Peso_KG"].Value;
                        drLocal["LATITUD"] = dr.Cells["LATITUD"].Value;
                        drLocal["LONGITUD"] = dr.Cells["LONGITUD"].Value;
                        drLocal["DIRECCION"] = dr.Cells["DIRECCION"].Value;
                        drLocal["NOMBRE ITEM"] = dr.Cells["NOMBRE ITEM"].Value;
                        drLocal["CANTIDAD"] = dr.Cells["CANTIDAD"].Value;
                        drLocal["CODIGO ITEM"] = dr.Cells["CODIGO ITEM"].Value;
                        drLocal["FECHA MIN ENTREGA"] = dr.Cells["FECHA MIN ENTREGA"].Value;
                        drLocal["FECHA MAX ENTREGA"] = dr.Cells["FECHA MAX ENTREGA"].Value;
                        drLocal["MIN VENTANA HORARIA 1"] = dr.Cells["MIN VENTANA HORARIA 1"].Value.ToString();
                        drLocal["MAX VENTANA HORARIA 1"] = dr.Cells["MAX VENTANA HORARIA 1"].Value.ToString();
                        drLocal["MIN VENTANA HORARIA 2"] = dr.Cells["MIN VENTANA HORARIA 2"].Value.ToString();
                        drLocal["MAX VENTANA HORARIA 2"] = dr.Cells["MAX VENTANA HORARIA 2"].Value.ToString();
                        drLocal["COSTO ITEM"] = dr.Cells["COSTO ITEM"].Value;
                        drLocal["CAPACIDAD UNO"] = dr.Cells["CAPACIDAD UNO"].Value;
                        drLocal["CAPACIDAD DOS"] = dr.Cells["CAPACIDAD DOS"].Value;
                        drLocal["SERVICE TIME"] = dr.Cells["SERVICE TIME"].Value;
                        drLocal["IMPORTANCIA"] = dr.Cells["IMPORTANCIA"].Value;
                        drLocal["IDENTIFICADOR CONTACTO"] = dr.Cells["IDENTIFICADOR CONTACTO"].Value;
                        drLocal["NOMBRE CONTACTO"] = dr.Cells["NOMBRE CONTACTO"].Value;
                        drLocal["TELEFONO"] = dr.Cells["TELEFONO"].Value;
                        drLocal["EMAIL CONTACTO"] = dr.Cells["EMAIL CONTACTO"].Value;
                        drLocal["CT ORIGEN"] = dr.Cells["CT ORIGEN"].Value;
                        drLocal["REFERENCIA DOMICILIO"] = dr.Cells["REFERENCIA DOMICILIO"].Value;
                        drLocal["Calle"] = dr.Cells["Calle"].Value;
                        drLocal["NumeroPuerta"] = dr.Cells["NumeroPuerta"].Value;
                        drLocal["colonia"] = dr.Cells["colonia"].Value;
                        drLocal["Barrio"] = dr.Cells["Barrio"].Value;
                        drLocal["Localidad"] = dr.Cells["Localidad"].Value;
                        drLocal["Partido"] = dr.Cells["Partido"].Value;
                        drLocal["Estado"] = dr.Cells["Estado"].Value;
                        drLocal["pais"] = dr.Cells["pais"].Value;
                        drLocal["RefDomicilioExterno"] = dr.Cells["RefDomicilioExterno"].Value;
                        drLocal["DomicilioDescripcion"] = dr.Cells["DomicilioDescripcion"].Value;
                        drLocal["InicioHorario1"] = dr.Cells["InicioHorario1"].Value;
                        drLocal["InicioHorario2"] = dr.Cells["InicioHorario2"].Value;
                        drLocal["FinHorario1"] = dr.Cells["FinHorario1"].Value;
                        drLocal["FinHorario2"] = dr.Cells["FinHorario2"].Value;
                        drLocal["TiempoEspera"] = dr.Cells["TiempoEspera"].Value;
                        drLocal["DomCodPostal"] = dr.Cells["DomCodPostal"].Value;
                        drLocal["Contacto"] = dr.Cells["Contacto"].Value;
                        drLocal["CodigoSucursal"] = dr.Cells["CodigoSucursal"].Value;
                        drLocal["RefDepositoExterno"] = dr.Cells["RefDepositoExterno"].Value;
                        drLocal["CodigoPostalEstab"] = dr.Cells["CodigoPostalEstab"].Value;
                        drLocal["Linea"] = dr.Cells["Linea"].Value;
                        drLocal["Sublinea"] = dr.Cells["Sublinea"].Value;
                        drLocal["Tipo"] = dr.Cells["Tipo"].Value;

                        DTA.Rows.Add(drLocal);

                    }
                }
            }
            else
            {
                foreach (DataRow dr in DTO.Rows)
                {
                    dr[1].ToString();
                    if (folio.Trim() == dr[1].ToString().Trim())
                    {
                        drLocal = DTA.NewRow();

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
                        DTA.Rows.Add(drLocal);
                    }

                }
            }
            return DTA;
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
        private void SetMinMax(DataGridView dg, String txt,int flag)
        {
            DataTable dt = new DataTable();
            dt = dg.DataSource as DataTable;
            Decimal Temp;
            minv = 24;
            MaxV = Convert.ToDecimal(txt_horasmax.Text);

            foreach (DataRow row in dt.Rows)
            {
                if (row[2].ToString().Trim() != "")
                {
                    if (flag == 0)
                    {
                        TimeSpan dias = DateTime.Now.Subtract(Convert.ToDateTime(row[2].ToString()));
                        Temp = Convert.ToDecimal(dias.TotalHours);
                    }
                    else
                    {
                        Temp = Convert.ToDecimal(row.ItemArray[2].ToString());
                    }
                    //MessageBox.Show(dias.TotalHours.ToString() +" "+ Convert.ToDateTime(row[2].ToString()).ToString("dd-MM-yyyyy") +" " + DateTime.Now.ToString("dd-MM-yyyyy"));
                    
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

        private void ColorCells(DataGridView dg, int flag)
        {
            DataRowView dr;
            Decimal CellValue;
            foreach (DataGridViewRow row in dg.Rows)
            {
                if (!row.IsNewRow)
                {

                    dr = (DataRowView)row.DataBoundItem;
                    if (dr[2].ToString().Trim() != "")
                    {
                        if (flag == 0)
                        {
                            TimeSpan dias = DateTime.Now.Subtract(Convert.ToDateTime(dr[2].ToString()));
                            CellValue = (Decimal)dias.TotalHours;
                            Color cellColor = GetColorFromValie(CellValue);
                            row.Cells[2].Style.BackColor = cellColor;
                        }
                        else
                        {
                            CellValue = Convert.ToDecimal(dr[2].ToString());
                            Color cellColor = GetColorFromValie(CellValue);
                            if(cellColor == Color.Red) { row.Cells[1].Style.ForeColor = Color.White; }
                            row.Cells[1].Style.BackColor = cellColor;

                        }
                       
                        // dataGridView1.BackgroundColor = cellColor;
                    }
                }
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bnt_Refrescar_Click(object sender, EventArgs e)
        {
            bnt_Refrescar.Enabled = false;
            if ((dt != null))
            {
                dt.Clear();
            }
            Estab = comboBox1.SelectedItem.ToString();
            dataGridView1.Refresh();
            lbl_Actualizando.Visible = true;
            ConsultarDoctos.RunWorkerAsync();
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
            //button2.Enabled = false;
            button1.Enabled = true;
            btn_Enviar.Enabled = false;
            comboBox1.Enabled = false;
            bnt_Refrescar.Enabled = false;
            bnt_Refrescar.Enabled = false;
            if ((dt != null))
            {
                dt.Clear();
            }
            Estab = comboBox1.SelectedItem.ToString();
            dataGridView1.Refresh();
            lbl_Actualizando.Visible = true;
            ConsultarDoctos.RunWorkerAsync();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            DataTable daux = new DataTable();
            DataTable dt = new DataTable();
            daux.Clear();

            dt = q.Consultar("select  cod_estab from  EYP_Servidores where servidro_A is not null and bbdd is not null and Cod_estab not in('10','11','7','16','40')");


            foreach (DataRow row in dt.Rows)
            {

                //string sql = "select   e.abreviatura as ABREVIATURA, CONVERT(VARCHAR(50), CAST(sum(cantidad*peso_total) AS MONEY ),1) as KILOS ,sum(cantidad*peso_total) as Kilos2 \r\n" +
                //    "from  [dbo].[F1534_Embarques_unigis]('" + row.ItemArray[0].ToString() + "') emb   \r\n" +
                //    " inner join establecimientos e on e.cod_estab=emb.cod_estab and emb.status_unigis='0'  group by e.abreviatura\r\n";

                string sql = "exec EYP_UNIGIS_KG '" + row.ItemArray[0].ToString() + "'";
                // string sql = "select cod_estab,'100' from establecimientos where modo_operacion='O'";

                DTKILOS.Merge(q.Consultar(sql, row.ItemArray[0].ToString()));



                if (nEventsFired)
                {
                    datagrid_update(DTKILOS);
                }
                ActualizarVales.ReportProgress(1);
            }
            button1.Enabled = true;
        }
        public void datagrid_update(DataTable dt)
        {
            DataTable dt2 = new DataTable();
            DataTable Daux = new DataTable();
            dt2.Columns.Add("Abreviatura");
            dt2.Columns.Add("Kilos");
            dt2.Columns.Add("Kilos2");

            foreach (DataRow dr in Daux.Rows)
            {

                dt2.Rows.Add(dr.ItemArray[0].ToString(), dr.ItemArray[1].ToString(), dr.ItemArray[2].ToString());

            }

            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataRow drA in dt2.Rows)
                {
                    if (dr.ItemArray[0].ToString() == drA.ItemArray[0].ToString())
                    {
                        DTKILOS.Rows.RemoveAt(PosicionDR(drA.ItemArray[0].ToString()));


                        Daux.Rows.Add(dr.ItemArray[0].ToString(), dr.ItemArray[1].ToString(), dr.ItemArray[2].ToString());
                        // drA.ItemArray[1] = "si corrio";
                    }

                }
            }

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ActualizarVales.CancelAsync();
            //MessageBox.Show("la informacion fue cargada con exito ");
            //   btnRefrescar.Enabled = true;

            timer1.Start();

        }

        private void ConsultarDoctos_DoWork(object sender, DoWorkEventArgs e)
        {
            dt = q.selectitemsKG(Estab);
            DAux = q.selectitemsKG(Estab);



        }

        private void ConsultarDoctos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lbl_Actualizando.Visible = false;
            comboBox1.Enabled = true;
            try
            {
                if (comboBox1.SelectedItem.ToString() == "")
                {
                    DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                    chk.HeaderText = "Enviar";
                    dataGridView1.Columns.Add(chk);
                    Qrys q = new Qrys();

                    dataGridView1.DataSource = dt;

                    if (dataGridView1.Rows.Count > 0)
                    {
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                        {
                            if (col.Index == 0 || col.Name == "CANTIDAD")
                                col.ReadOnly = false;
                            else
                                col.ReadOnly = true;
                            //Si la estableces a true la columna no será editable col.ReadOnly = true;
                            //Debes hacer exactamente lo contrario, establecerla a false.

                        }
                    }
                    dataGridView1.Sort(dataGridView1.Columns["FechaEntrega"], ListSortDirection.Ascending);
                    SetMinMax(dataGridView1, txt_horasmax.Text,0);
                    ColorCells(dataGridView1,0);
                }
                else
                {
                    DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                    chk.HeaderText = "Enviar";
                    dataGridView1.Columns.Add(chk);
                    Qrys q = new Qrys();

                    dataGridView1.DataSource = dt;

                    if (dataGridView1.Rows.Count > 0)
                    {
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                        {
                            if (col.Index == 0 || col.Name == "CANTIDAD")
                                col.ReadOnly = false;




                            else
                                col.ReadOnly = true;
                            //Si la estableces a true la columna no será editable col.ReadOnly = true;
                            //Debes hacer exactamente lo contrario, establecerla a false.

                        }
                    }
                    //dataGridView1.Sort(dataGridView1.Columns["FechaEntrega"], ListSortDirection.Ascending);

                }
            }
            catch (SqlException sql) { System.Windows.Forms.MessageBox.Show("Servidor a consultar no cuenta con conexion a internet", "Conexion", MessageBoxButtons.OK); }
            bnt_Refrescar.Enabled = true;
            btn_Enviar.Enabled = true;
            //button2.Enabled = true;
            SetMinMax(dataGridView1, txt_horasmax.Text,0);
            ColorCells(dataGridView1,0);
            // DAux = dataGridView1.DataSource as DataTable;
            dataGridView1.Columns["Cod_cte"].Visible = false;
            dataGridView1.Columns["TEL1"].Visible = false;
            dataGridView1.Columns["tel2"].Visible = false;
            dataGridView1.Columns["FECHA MIN ENTREGA"].Visible = false;
            dataGridView1.Columns["FECHA MAX ENTREGA"].Visible = false;
            dataGridView1.Columns["MIN VENTANA HORARIA 1"].Visible = false;
            dataGridView1.Columns["MAX VENTANA HORARIA 1"].Visible = false;
            dataGridView1.Columns["MIN VENTANA HORARIA 1"].Visible = false;
            dataGridView1.Columns["MAX VENTANA HORARIA 2"].Visible = false;
            dataGridView1.Columns["MIN VENTANA HORARIA 2"].Visible = false;
            dataGridView1.Columns["e_mail"].Visible = false;
            dataGridView1.Columns["COSTO ITEM"].Visible = false;
            dataGridView1.Columns["CAPACIDAD UNO"].Visible = false;
            dataGridView1.Columns["CAPACIDAD DOS"].Visible = false;
            dataGridView1.Columns["SERVICE TIME"].Visible = false;
            dataGridView1.Columns["IMPORTANCIA"].Visible = false;
            dataGridView1.Columns["IDENTIFICADOR CONTACTO"].Visible = false;
            dataGridView1.Columns["NOMBRE CONTACTO"].Visible = false;
            //dataGridView1.Columns["TELEFOLO"].Visible = false;
            dataGridView1.Columns["EMAIL CONTACTO"].Visible = false;
            dataGridView1.Columns["CT ORIGEN"].Visible = false;
            dataGridView1.Columns["REFERENCIA DOMICILIO"].Visible = false;


        }


        private void dataGridView2_Sorted(object sender, EventArgs e)
        {

        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            SetMinMax(dataGridView1, txt_horasmax.Text,0);
            ColorCells(dataGridView1,0);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // obtenerViaje(27);
        }

        private void lbl_Vales_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void ActualizarStatusPPE(DataTable aux,string cod_estab)
        {
            string Documento;
            string transaccion;
            string cod_prod;
            for (int i = 0; i < aux.Rows.Count; i++)
            {
                cod_prod = aux.Rows[i]["CODIGO ITEM"].ToString();
                Documento = aux.Rows[i]["N° Documento"].ToString().Substring(aux.Rows[i]["N° Documento"].ToString().IndexOf('-') + 1);
                transaccion = aux.Rows[i]["N° Documento"].ToString().Substring(0, aux.Rows[i]["N° Documento"].ToString().IndexOf('-'));

                switch (transaccion)
                {
                    case ("FC"): transaccion = "36"; break;
                    case ("RC"): transaccion = "37"; break;
                    case ("PE"): transaccion = "29"; break;
                    case ("EdMaC"): transaccion = "713"; break;
                }
                q.UpdatePPE(cod_prod, Documento, transaccion, cod_estab, "1");


            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (Operators.CompareString("", "", false) != 0)
            {

            }
            MessageBox.Show("No entro", "Actualizacion de Documentos ", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (dataGridView1.CurrentRow.Cells["Cantidad"].Value != null)
                    Convert.ToDouble(dataGridView1.CurrentRow.Cells["Cantidad"].Value.ToString());

                //MessageBox.Show(ActiveControl.Text.ToString());
            }
            catch
            {
                dataGridView1.CurrentRow.Cells["Cantidad"].Value = "1";
                return;
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name.ToString() == "Cantidad")
            {

                string p = dataGridView1.CurrentRow.Cells["Codigo Item"].Value.ToString();

                string cantidad = lib.Decimales(dataGridView1.CurrentRow.Cells["Cantidad"].Value.ToString(), p.Trim());
                string a = DAux.Rows[dataGridView1.CurrentRow.Index]["CANTIDAD"].ToString();
                string n = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[dataGridView1.CurrentCell.ColumnIndex].Value.ToString();


                if (DAux.Rows[dataGridView1.CurrentRow.Index][dataGridView1.CurrentCell.ColumnIndex].ToString() != dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[dataGridView1.CurrentCell.ColumnIndex].Value.ToString())
                {


                    if (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CANTIDAD"].Value.ToString()) > Convert.ToDouble(DAux.Rows[dataGridView1.CurrentRow.Index]["CANTIDAD"].ToString()))
                    {
                        cantidad = DAux.Rows[e.RowIndex]["CANTIDAD"].ToString();
                        //MessageBox.Show(cantidad);
                    }
                }


                dt.Rows[e.RowIndex]["CANTIDAD"] = cantidad;
                // dataGridView1.Refresh();

            }

            //  dataGridView1.DataSource=DAux;
            // dataGridView1.Refresh();
        }
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {


            if (dataGridView1.Rows.Count > 0)
            {
                int columnIndex = dataGridView1.CurrentCell.ColumnIndex;
                string columnName = dataGridView1.Columns[columnIndex].Name;
                if (columnName == "CANTIDAD")
                {
                    if (!Char.IsDigit(e.KeyChar) &&
                 e.KeyChar != (char)Keys.Back &&
                 e.KeyChar != '.')
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        if (e.KeyChar == '.')
                        {
                            if (ActiveControl.Text.ToString().Contains('.'))//Text.Contains('.'))
                                e.Handled = true;
                            else
                                e.Handled = false;
                        }
                    }
                }
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer4_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_3(object sender, EventArgs e)
        {

        }

        private void button1_Click_4(object sender, EventArgs e)
        {
            var row = dataGridView1.Rows[dataGridView1.CurrentRow.Index];
            // row.Cells["N° DOCUMENTO"].Value.ToString();
            //Relacion_Pickup rp = new Relacion_Pickup(DTOCD, row.Cells["N° DOCUMENTO"].Value.ToString());
            //rp.ShowDialog();
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public DataTable validar_Pickup(List<object> doc)
        {
            DataTable DTP = new DataTable();
            DTP.Columns.Add("Enviar");
            DTP.Columns.Add("N° DOCUMENTO");
            DTP.Columns.Add("FECHA_DOC", typeof(DateTime));
            DTP.Columns.Add("FechaEntrega", typeof(DateTime));
            DTP.Columns.Add("cod_cte");
            DTP.Columns.Add("razon_social");
            DTP.Columns.Add("tel1");
            DTP.Columns.Add("tel2");
            DTP.Columns.Add("e_mail");
            DTP.Columns.Add("Peso_KG", typeof(System.Decimal));
            DTP.Columns.Add("LATITUD", typeof(System.Decimal));
            DTP.Columns.Add("LONGITUD", typeof(System.Decimal));
            DTP.Columns.Add("DIRECCION");
            DTP.Columns.Add("NOMBRE ITEM");
            DTP.Columns.Add("CANTIDAD", typeof(System.Decimal));
            DTP.Columns.Add("CODIGO ITEM");
            DTP.Columns.Add("FECHA MIN ENTREGA", typeof(DateTime));
            DTP.Columns.Add("FECHA MAX ENTREGA", typeof(DateTime));
            DTP.Columns.Add("MIN VENTANA HORARIA 1");
            DTP.Columns.Add("MAX VENTANA HORARIA 1");
            DTP.Columns.Add("MIN VENTANA HORARIA 2");
            DTP.Columns.Add("MAX VENTANA HORARIA 2");
            DTP.Columns.Add("COSTO ITEM", typeof(System.Decimal));
            DTP.Columns.Add("CAPACIDAD UNO", typeof(System.Decimal));
            DTP.Columns.Add("CAPACIDAD DOS", typeof(System.Decimal));
            DTP.Columns.Add("SERVICE TIME");
            DTP.Columns.Add("IMPORTANCIA");
            DTP.Columns.Add("IDENTIFICADOR CONTACTO");
            DTP.Columns.Add("NOMBRE CONTACTO");
            DTP.Columns.Add("TELEFONO");
            DTP.Columns.Add("EMAIL CONTACTO");//--
            DTP.Columns.Add("CT ORIGEN");
            DTP.Columns.Add("REFERENCIA DOMICILIO");
            DTP.Columns.Add("Calle");
            DTP.Columns.Add("NumeroPuerta");
            DTP.Columns.Add("colonia");
            DTP.Columns.Add("Barrio");
            DTP.Columns.Add("Localidad");
            DTP.Columns.Add("Partido");
            DTP.Columns.Add("Estado");
            DTP.Columns.Add("pais");
            DTP.Columns.Add("RefDomicilioExterno");
            DTP.Columns.Add("DomicilioDescripcion");
            DTP.Columns.Add("InicioHorario1");
            DTP.Columns.Add("InicioHorario2");
            DTP.Columns.Add("FinHorario1");
            DTP.Columns.Add("FinHorario2");
            DTP.Columns.Add("TiempoEspera");
            DTP.Columns.Add("DomCodPostal");
            DTP.Columns.Add("Contacto");
            DTP.Columns.Add("CodigoSucursal");
            DTP.Columns.Add("RefDepositoExterno");
            DTP.Columns.Add("CodigoPostalEstab");
            DTP.Columns.Add("Linea");
            DTP.Columns.Add("Sublinea");
            DTP.Columns.Add("Tipo");
            foreach (var item in doc)
            {
                foreach (DataRow dr2 in DTOCD.Rows)
                {
                    if (item.ToString() == dr2[0].ToString().Trim())
                    {
                        agregarPickUP(DTP, dr2[0].ToString(), dr2[1].ToString(), dr2[2].ToString());
                    }
                }

            }
            return DTP;
        }
        public void agregarPickUP(DataTable dtp, string delivery, string pickup, string tipo)
        {
            string sql = "";
            string folio = delivery.Substring(delivery.IndexOf('-') + 1, delivery.Length - delivery.IndexOf('-') - 1);

            if (tipo.Trim() == "30")
                sql = "select 'PP-" + folio + "' as[N° Documento] ,mp.fecha as Fecha_DOC,GETDATE() as FechaEntrega,  rtrim(prv.cod_prv) as cod_cte  ,rtrim(prv.razon_social) as razon_social ,prv.tel1,prv.tel2,prv.e_mail,mp.peso as Peso_KG ,ISNULL(cast(prv.coordenada_x as decimal (19,8)),0) as LATITUD, ISNULL(cast(prv.coordenada_y as decimal(19,8)),0) as LONGITUD,'' as DIRECCION,rtrim(p.descripcion) as [NOMBRE ITEM], " +
                "mp.cantidad_autorizada as CANTIDAD ,p.cod_prod as [CODIGO ITEM],GETDATE() AS [FECHA MIN ENTREGA],getdate() AS [FECHA MAX ENTREGA],'08:00' AS [MIN VENTANA HORARIA 1],'18:30' AS [MAX VENTANA HORARIA 1],'' AS [MIN VENTANA HORARIA 2],'' AS [MAX VENTANA HORARIA 2],mp.costo AS [COSTO ITEM],cast(0 as decimal(19,4)) AS[CAPACIDAD UNO],cast(0 as decimal(19,4))  AS [CAPACIDAD DOS],'15' AS[SERVICE TIME],'' AS IMPORTANCIA,PRV.COD_PRV AS [IDENTIFICADOR CONTACTO],prv.razon_social AS [NOMBRE CONTACTO],prv.tel1 AS TELEFONO,prv.e_mail AS [EMAIL CONTACTO],e.abreviatura AS [CT ORIGEN],'' AS [REFERENCIA DOMICILIO],prv.calle AS CALLE,'' AS NUMEROPUERTA,prv.colonia AS COLONIA,prv.colonia AS BARRIO,prv.poblacion AS LOCALIDAD,prv.estado AS PARTIDO,prv.pais AS PAIS,prv.cod_prv AS REFDOMICILIOEXTERNO,prv.razon_social AS DOMICILIODESCRIPCION,'0700' AS InicioHorario1,'0000' AS InicioHorario2,'2300' AS FinHorario1,'0000' AS FinHorario2,'15' AS TiempoEspera,prv.cod_postal AS DomCodPostal ,e.cod_estab  AS Contacto,'VP21' AS RefDepositoExterno,'21395' AS CodigoPostalEstab,lp.nombre AS Linea,f.nombre AS Sublinea, 'P' AS Tipo ,'18' as CodigoSucursal from mpedprv mp " +
                "left join pedprv pr on mp.folio=pr.folio " +
                "left join proveedores prv on pr.cod_prv=prv.cod_prv " +
                "left join productos p on mp.cod_prod=p.cod_prod " +
                "left join lineas_productos lp on lp.linea_producto=p.linea_producto " +
                "left join familias f on f.familia=p.familia " +
                "left join establecimientos e on e.cod_estab=pr.cod_estab_estadistica " +
                "where mp.folio='" + pickup + "'";

            if (tipo.Trim() == "29")
                sql = "select 'PP-" + folio + "' as[N° Documento] ,mp.fecha as Fecha_DOC,GETDATE() as FechaEntrega,  rtrim(prv.cod_estab) as cod_cte  ,rtrim(prv.razon_social) as razon_social ,prv.telefono1,prv.telefono2,'' as e_mail,mp.peso as Peso_KG ,ISNULL(cast(prv.coordenada_x as decimal (19,8)),0) as LATITUD, ISNULL(cast(prv.coordenada_y as decimal(19,8)),0) as LONGITUD,'' as DIRECCION,rtrim(p.descripcion) as [NOMBRE ITEM], " +
                "mp.cantidad_autorizada as CANTIDAD ,p.cod_prod as [CODIGO ITEM],GETDATE() AS[FECHA MIN ENTREGA],getdate() AS[FECHA MAX ENTREGA],'08:00' AS[MIN VENTANA HORARIA 1],'18:30' AS[MAX VENTANA HORARIA 1],'' AS[MIN VENTANA HORARIA 2],'' AS[MAX VENTANA HORARIA 2],mp.costo AS[COSTO ITEM], cast(0 as decimal(19, 4)) AS[CAPACIDAD UNO],cast(0 as decimal(19, 4))  AS[CAPACIDAD DOS],'15' AS[SERVICE TIME],'' AS IMPORTANCIA, PRV.cod_estab AS[IDENTIFICADOR CONTACTO], prv.razon_social AS[NOMBRE CONTACTO], prv.telefono1 AS TELEFONO,'' AS[EMAIL CONTACTO],e.abreviatura AS[CT ORIGEN],'' AS[REFERENCIA DOMICILIO],prv.calle AS CALLE,'' AS NUMEROPUERTA, prv.colonia AS COLONIA,prv.colonia AS BARRIO,prv.poblacion AS LOCALIDAD,prv.estado AS PARTIDO,prv.pais AS PAIS,prv.cod_estab AS REFDOMICILIOEXTERNO,prv.razon_social AS DOMICILIODESCRIPCION,'0700' AS InicioHorario1,'0000' AS InicioHorario2,'2300' AS FinHorario1,'0000' AS FinHorario2,'15' AS TiempoEspera, prv.cod_postal AS DomCodPostal ,e.cod_estab AS Contacto,'VP21' AS RefDepositoExterno,'21395' AS CodigoPostalEstab, lp.nombre AS Linea,f.nombre AS Sublinea, 'P' AS Tipo ,'18' as CodigoSucursal  " +
                "from mpedestab mp " +
                "left join pedestab pr on mp.folio = pr.folio " +
                "left join establecimientos prv on pr.cod_estab_alterno = prv.cod_estab " +
                "left join productos p on mp.cod_prod = p.cod_prod " +
                "left join lineas_productos lp on lp.linea_producto = p.linea_producto " +
                "left join familias f on f.familia = p.familia " +
                "left join establecimientos e on e.cod_estab = pr.cod_estab " +
                     "where mp.folio = '" + pickup + "'";

            DataTable DTPickup = q.Consultar(sql);


            dtp.Merge(DTPickup);



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
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool bChecked = Convert.ToBoolean(row.Cells[0].Value);
                if (bChecked)
                {
                    row.Cells[0].Value = false;
                }
                else
                    row.Cells[0].Value = true;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = q.Consultar("select cod_estab from Eyp_Servidores where servidro_A is not null and bbdd is not null", "10");
            DataTable DTKILOS = new DataTable();

            foreach (DataRow row in dt.Rows)
            {
                string sql = "select e.abreviatura, CONVERT(VARCHAR(50), CAST(sum(cantidad * peso_total) AS MONEY), 1) as KILOS ,sum(cantidad * peso_total) as Kilos2 " +
                      "from[dbo].[F1534_Embarques_Planner]('" + row.ItemArray[0].ToString() + "') " +
                      "emb inner join establecimientos e on e.cod_estab = emb.cod_estab group by e.abreviatura ";


                if (DTKILOS.Rows.Count > 0)
                {
                    DTKILOS.Merge(q.Consultar(sql, row.ItemArray[0].ToString()));
                }
                else
                { DTKILOS = q.Consultar(sql, row.ItemArray[0].ToString()); }
            }

        }

        private void ActualizarVales_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           
        }

        private void splitContainer4_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_5(object sender, EventArgs e)
        {
            //PRUEBA
            //ExportarDataGridViewExcel(dataGridView1);
            string ruta2 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            {

                try
                {
                    using (XLWorkbook workbook = new XLWorkbook())
                    {
                        workbook.Worksheets.Add(this.dt.Copy(), "Documentos" + user + "-" + Estab.Substring(0, 5));
                        workbook.SaveAs("Documentos" + user + "-" + Estab.Substring(0, 5) + ".xlsx");
                        Process.Start(new ProcessStartInfo("Documentos" + user + "-" + Estab.Substring(0, 5) + ".xlsx") { UseShellExecute = true });

                    }
                    this.TopMost = false;
                    MessageBox.Show("Archivo Exportado con exito", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.TopMost = true;
                }
                catch (Exception ex)
                {
                    this.TopMost = false;
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.TopMost = true;
                }
            }

        }
    }
}


