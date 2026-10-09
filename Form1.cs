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
        DataTable DAux = new DataTable();
        HttpClient client = new HttpClient();
        string user;
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(Control_KeyPress);
        }

        public Form1(string u)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(this.Control_KeyPress);
            this.WindowState = FormWindowState.Maximized;
            this.user = u;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Deseas enviar la informacion seleccionada a Unigis una vez se envie no podra eliminarse", "Informacion ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            DataTable dCod = q.Cod_estab(comboBox1.SelectedItem.ToString());
            cod_estab = dCod.Rows[0].ItemArray[0].ToString();
            if (result1 == DialogResult.Yes)
            {
                xmlwriterOrden xml = new xmlwriterOrden();
                DataTable dtLocalC = new DataTable();
                dtLocalC = DivDT(dt, dtLocalC, "");

                var x = (from r in dtLocalC.AsEnumerable()
                         select r["N° DOCUMENTO"]).Distinct().ToList();
                List<object> doc = (from r in dtLocalC.AsEnumerable()
                                    select r["N° DOCUMENTO"]).Distinct().ToList();

                Validar_folio(dtLocalC, cod_estab);
                DataTable aux = new DataTable();
                string msg = "Actualizacion Correcta :" + Environment.NewLine;

                if (dtLocalC.Rows.Count >= 1)
                {
                    foreach (var item in doc)
                    {
                        aux = DivDT(dtLocalC, aux, item.ToString());

                        System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        var endpoint = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx?op=CrearOrdenesPedido");
                        var paylod = new StringContent(xml.stringtoxml(aux), Encoding.UTF8, "text/xml");
                        string y = (xml.stringtoxml(aux));
                        var result = client.PostAsync(endpoint, paylod).Result.StatusCode.ToString();

                        if (result == "OK")
                        {
                            msg = msg + "Documento " + item.ToString() + Environment.NewLine;
                            ActualizarStatusPPE(aux, cod_estab);
                        }
                        else
                        {
                            MessageBox.Show("No se ha podido Enviar el Documento");
                        }

                        aux.Clear();
                    }

                    MessageBox.Show(msg, "Actualizacion de Documentos ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    aux.Clear();
                    dt.Clear();
                    dataGridView1.DataSource = null;
                    dataGridView1.Columns.Clear();
                }
            }
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

            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();

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
                        drLocal["NOTAS"] = dr.Cells["NOTAS"].Value;
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

        private void SetMinMax(DataGridView dg, String txt)
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
                    TimeSpan dias = DateTime.Now.Subtract(Convert.ToDateTime(row[2].ToString()));
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
                    dr = (DataRowView)row.DataBoundItem;
                    if (dr[2].ToString().Trim() != "")
                    {
                        TimeSpan dias = DateTime.Now.Subtract(Convert.ToDateTime(dr[2].ToString()));
                        CellValue = (Decimal)dias.TotalHours;
                        Color cellColor = GetColorFromValie(CellValue);
                        row.Cells[2].Style.BackColor = cellColor;
                    }
                }
            }
        }

        private void DataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (dataGridView.Columns[e.ColumnIndex].Name != "CANTIDAD")
                return;

            try
            {
                DataGridViewRow fila = dataGridView.Rows[e.RowIndex];
                string codProd = fila.Cells["CODIGO ITEM"].Value?.ToString().Trim();
                string doc = fila.Cells["N° DOCUMENTO"].Value?.ToString().Trim();
                string transaccion = fila.Cells["Transaccion"].Value?.ToString().Trim();

                DataRow original = null;
                foreach (DataRow r in DAux.Rows)
                {
                    if (r["CODIGO ITEM"].ToString().Trim() == codProd &&
                        r["N° DOCUMENTO"].ToString().Trim() == doc)
                    {
                        original = r;
                        break;
                    }
                }
                if (original == null)
                    return;

                decimal cantidadMaxima = Convert.ToDecimal(original["CANTIDAD"]);

                bool pesoEnCero = transaccion == "848";

                if (!pesoEnCero && transaccion == "30")
                {
                    string folio = doc.Substring(doc.IndexOf('-') + 1);
                    bool esOrdenCompra = (folio.Length >= 3 && char.ToUpperInvariant(folio[2]) == 'X')
                                      || (folio.Length >= 4 && char.ToUpperInvariant(folio[3]) == 'X');
                    pesoEnCero = !esOrdenCompra;
                }

                decimal pesoUnitario;
                if (pesoEnCero)
                {
                    pesoUnitario = 0M;
                }
                else
                {
                    pesoUnitario = 0.01M;
                    DataTable pesoDt = q.Consultar($"select peso_total from productos where cod_prod = '{codProd}'");
                    if (pesoDt.Rows.Count > 0 && pesoDt.Rows[0][0] != DBNull.Value)
                        pesoUnitario = Convert.ToDecimal(pesoDt.Rows[0][0]);
                    if (pesoUnitario == 0M)
                        pesoUnitario = 0.01M;
                }

                decimal cantidadNueva;
                if (!Decimal.TryParse(e.FormattedValue?.ToString(), out cantidadNueva))
                {
                    e.Cancel = true;
                    fila.Cells["CANTIDAD"].Value = cantidadMaxima;
                    fila.Cells["Peso_KG"].Value = cantidadMaxima * pesoUnitario;
                    MessageBox.Show("Por favor, ingrese solo números enteros válidos.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (cantidadNueva > cantidadMaxima)
                {
                    e.Cancel = true;
                    fila.Cells["CANTIDAD"].Value = cantidadMaxima;
                    fila.Cells["Peso_KG"].Value = cantidadMaxima * pesoUnitario;
                    MessageBox.Show($"La cantidad máxima permitida es {cantidadMaxima}.", "Límite Excedido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    fila.Cells["Peso_KG"].Value = cantidadNueva * pesoUnitario;
                }
            }
            catch (Exception)
            {
                e.Cancel = true;
                MessageBox.Show("No fue posible validar la cantidad. Por favor intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bnt_Refrescar_Click(object sender, EventArgs e)
        {
            bnt_Refrescar.Enabled = false;
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            Estab = comboBox1.SelectedItem.ToString();
            lbl_Actualizando.Text = "Consultando...";
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
            button2.Enabled = false;
            button1.Enabled = false;
            btn_Enviar.Enabled = false;
            comboBox1.Enabled = false;
            bnt_Refrescar.Enabled = false;
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            Estab = comboBox1.SelectedItem.ToString();
            lbl_Actualizando.Text = "Consultando...";
            lbl_Actualizando.Visible = true;
            ConsultarDoctos.RunWorkerAsync();
        }

        private void ConsultarDoctos_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable datos = q.selectitems(Estab);
            dt = datos;
            DAux = datos.Copy();
        }

        private void ConsultarDoctos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cmb_letra.Enabled = true;
            lbl_Actualizando.Visible = false;
            comboBox1.Enabled = true;

            if (e.Error != null)
            {
                dt = null;
                DAux = new DataTable();
                bnt_Refrescar.Enabled = true;
                btn_Enviar.Enabled = true;
                button2.Enabled = true;
                button1.Enabled = true;
                MessageBox.Show("No fue posible consultar los documentos. Verifique la conexión con el servidor e intente de nuevo." + Environment.NewLine + Environment.NewLine + e.Error.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
                    SetMinMax(dataGridView1, txt_horasmax.Text);
                    ColorCells(dataGridView1);
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
                }
            }
            catch (SqlException sql)
            {
                MessageBox.Show("Servidor a consultar no cuenta con conexion a internet", "Conexion", MessageBoxButtons.OK);
            }

            bnt_Refrescar.Enabled = true;
            btn_Enviar.Enabled = true;
            button2.Enabled = true;
            button1.Enabled = true;
            SetMinMax(dataGridView1, txt_horasmax.Text);
            ColorCells(dataGridView1);
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
            dataGridView1.Columns["EMAIL CONTACTO"].Visible = false;
            dataGridView1.Columns["CT ORIGEN"].Visible = false;
            dataGridView1.Columns["REFERENCIA DOMICILIO"].Visible = false;

            // Impide que la columna ajuste su ancho automaticamente
            dataGridView1.Columns["NOTAS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            // Establece el ancho de la columna en 350 px
            dataGridView1.Columns["NOTAS"].Width = 350;
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            SetMinMax(dataGridView1, txt_horasmax.Text);
            ColorCells(dataGridView1);
        }

        private void ActualizarStatusPPE(DataTable aux, string cod_estab)
        {
            foreach (DataRow row in aux.Rows)
            {
                var nDoc = row["N° DOCUMENTO"].ToString();
                var cod_prod = row["CODIGO ITEM"].ToString().Trim();
                var Documento = nDoc.Substring(nDoc.IndexOf('-') + 1);
                var transaccion = nDoc.Substring(0, nDoc.IndexOf('-'));
                switch (transaccion)
                {
                    case "FC": transaccion = "36"; break;
                    case "RC": transaccion = "37"; break;
                    case "PE": transaccion = "29"; break;
                    case "EdMaC": transaccion = "713"; break;
                    case "TE": transaccion = "35"; break;
                }

                q.UpdatePPE(cod_prod, Documento, transaccion, cod_estab, "1");
                q.HistoricoEnvio(
                    nDoc.Trim(),
                    cod_prod,
                    row["CANTIDAD"].ToString().Trim(),
                    row["RefDepositoExterno"].ToString().Trim(),
                    user,
                    row["FechaEntrega"].ToString().Trim(),
                    row["LATITUD"].ToString().Trim(),
                    row["LONGITUD"].ToString().Trim());
            }
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
                            if (ActiveControl.Text.ToString().Contains('.'))
                                e.Handled = true;
                            else
                                e.Handled = false;
                        }
                    }
                }
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "TOPF1", this.Top.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "LeftF1", this.Left.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "WidthF1", this.Width.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "HeightF1", this.Height.ToString());
        }

        private void button1_Click_5(object sender, EventArgs e)
        {
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int size = int.Parse(cmb_letra.Text);

            dataGridView1.Font = new Font(dataGridView1.Font.Name, size, dataGridView1.Font.Style);
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DataGridViewColumn col = new DataGridViewColumn();
            col.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void Validar_folio(DataTable auxF, string cod_estab)
        {
            string Folio;
            string cod_prod_F;
            string resultado;
            string Folio_m;
            string cod_pro_f;
            string cantidad;
            string transaccion;

            for (int i = auxF.Rows.Count - 1; i >= 0; i--)
            {
                Folio = auxF.Rows[i]["N° Documento"].ToString().Substring(auxF.Rows[i]["N° Documento"].ToString().IndexOf('-') + 1);
                cod_prod_F = auxF.Rows[i]["CODIGO ITEM"].ToString();
                Folio_m = auxF.Rows[i]["N° Documento"].ToString();
                transaccion = auxF.Rows[i]["N° Documento"].ToString().Substring(0, auxF.Rows[i]["N° Documento"].ToString().IndexOf('-'));
                switch (transaccion)
                {
                    case ("FC"): transaccion = "36"; break;
                    case ("RC"): transaccion = "37"; break;
                    case ("PE"): transaccion = "29"; break;
                    case ("EdMaC"): transaccion = "713"; break;
                    case ("TE"): transaccion = "35"; break;
                }
                DTFolio = q.Consultar("select  folio,cod_prod,cantidad,transaccion from productos_por_embarcar where status='1' and folio='" + Folio + "' and transaccion='" + transaccion + "' and cod_prod='" + cod_prod_F + "'  ", cod_estab);

                if (DTFolio.Rows.Count >= 1)
                {
                    MessageBox.Show("Documento : " + DTFolio.Rows[0]["folio"] + DTFolio.Rows[0]["cod_prod"] + DTFolio.Rows[0]["cantidad"]);

                    auxF.Rows[i].Delete();
                }
            }
        }
    }
}

