//using CrystalDecisions.ReportAppServer.DataDefModel;

//using report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using ClosedXML.Excel;
using ActualizadorDoctosUnigis.Contoller;

namespace ActualizadorDoctosUnigis
{

    public partial class Form6 : Form
    {
         bool nEventsFired = false;
        Qrys q = new Qrys();
        Decimal Min = 24;
        Decimal Max = 0;
        Decimal Mid = 0;
        Decimal Steps = Decimal.MinValue;
        Decimal MaxV = 0;
        decimal minv = 0;
        string usuario = "";
        
        DataTable DTKILOS = new DataTable();
        DataTable Daux = new DataTable();
       
        public Form6(string u)
        {
            InitializeComponent();
            usuario = u;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            // timer1.Start();

            //  timer1.Stop();

            if (Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "TOPF6").ToString() != "")
            {
                this.Top = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "TOPF6").ToString());
                this.Left = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "LeftF6").ToString());
                this.Width = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "WidthF6").ToString());
                this.Height = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "HeightF6").ToString());
            }
            CargarKilos();
        }

        // Sin BackgroundWorker: ver Libreria.EnSegundoPlano (con el BackgroundWorker la pantalla se congelaba ~20 s al refrescar)
        private void CargarKilos()
        {
            button2.Enabled = false;
            btnRefrescar.Enabled = false;
            Libreria.EnSegundoPlano(this, () => q.Consultar("select * from EYP_KG_PENDIENTES", "10"), KilosCargados);
        }

        private void KilosCargados(DataTable datos, Exception error)
        {
            button2.Enabled = true;
            btnRefrescar.Enabled = true;
            if (error != null)
            {
                MessageBox.Show("No fue posible consultar los kilos pendientes. Verifique la conexión con el servidor e intente de nuevo." + Environment.NewLine + Environment.NewLine + Libreria.MensajeError(error), "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DTKILOS = datos;
            dataGridView1.DataSource = DTKILOS;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Ascending);
            SetMinMax(dataGridView1, textBox1.Text);
            ColorCells(dataGridView1);
            minv = Convert.ToDecimal(textBox1.Text);
        }

        

        private void button1_Click(object sender, EventArgs e)
        {

            // timer1.Start();
           // timer1.Stop();

         //   backgroundWorker1.RunWorkerAsync();
            
            // dataGridView1.DataSource = DTKILOS;
            // timer1.Start();
         //   button1.Enabled = false;
           // btnRefrescar.Enabled = false;

        }


        public void datagrid_update(DataTable dt)
        {
            

        }

        public int PosicionDR(String Abreviatura)
        {
            int J = 0;
            for (int x=0;x< Daux.Rows.Count; x++)
            {
                if (Abreviatura == Daux.Rows[x].ItemArray[0].ToString())
                {
                    return x;
                }

            }

            return J;
        }

        //Metodo que calcula si el valor es maximo y minimo para setearlo 
        private void SetMinMax(DataGridView dg, String txt)
        {
            DataTable dt = new DataTable();
            dt = dg.DataSource as DataTable;
            Decimal Temp;
            minv = Convert.ToDecimal(txt) ;
            MaxV = Convert.ToDecimal(txt) +500 ;

            foreach (DataRow row in dt.Rows)
            {
                if (row[2].ToString().Trim() != "")
                {

                    //TimeSpan dias = DateTime.Now.Subtract(Convert.ToDateTime(row[2].ToString()));
                    //MessageBox.Show(dias.TotalHours.ToString() +" "+ Convert.ToDateTime(row[2].ToString()).ToString("dd-MM-yyyyy") +" " + DateTime.Now.ToString("dd-MM-yyyyy"));
                    Temp = Convert.ToDecimal(row.ItemArray[2].ToString());
                    if (Temp > Max) { Max = Temp; }
                    if (Temp < Min) { Min = Temp; }
                }
            }
            Max = Convert.ToDecimal(txt);

            if (Max != 0)
            {
                Mid = ((MaxV - minv) / 2)+minv;
                Decimal total = (Max - Mid);
                Steps = total / 10;
                if (Steps % 2 == 0)
                {
                    Steps = Convert.ToInt32(Steps) + 1;
                }
            }
        }
        //Metodo que colorea las filas del datagridvied
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
                        //TimeSpan dias = DateTime.Now.Subtract(Convert.ToDateTime(dr[2].ToString()));
                        CellValue = Convert.ToDecimal(dr[2].ToString());
                        Color cellColor = GetColorFromValie(CellValue);
                        row.Cells[1].Style.BackColor = cellColor;
                        if(row.Cells[1].Style.BackColor == Color.Red)
                        {
                            row.Cells[1].Style.ForeColor = Color.White;
                        }

                        if (row.Cells[1].Style.BackColor == Color.FromArgb(255, 0, 255, 0))
                        {
                            row.Cells[1].Style.ForeColor = Color.Black;
                        }
                        // dataGridView1.BackgroundColor = cellColor;
                    }
                }
            }
        }
        //Metodo de tipo color que establece el valor del color dependiendo el maximo y el minimo
        private Color GetColorFromValie(Decimal targetValue)
        {
            if (targetValue > MaxV)
            {
                return Color.Red;
            }

            if (targetValue == MaxV) { return Color.FromArgb(255, 255, 0, 0); }
            else if (targetValue == Mid ) { return Color.FromArgb(255, 255, 255, 0); }
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
                if (offsetValue < 0)
                    offsetValue = 0;

                offsetSteps = offsetValue;
                rgbValue = 255 - Convert.ToInt32(offsetSteps);
                
                if (rgbValue > 255) { rgbValue = 255; }
                if (rgbValue < 0) { rgbValue =0; }
                return Color.FromArgb(255, 255, rgbValue, 0);
            }


            return Color.Red;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            nEventsFired = true;
            CargarKilos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CargarKilos();
        }

        private void Form6_InputLanguageChanging(object sender, InputLanguageChangingEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
            
            SetMinMax(dataGridView1, textBox1.Text);
            ColorCells(dataGridView1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                 textBox1.Text = trackBar1.Value.ToString();
                SetMinMax(dataGridView1, textBox1.Text);
                ColorCells(dataGridView1);
            }
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            CargarKilos();
        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        { 
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "TOPF6", this.Top.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "LeftF6", this.Left.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "WidthF6", this.Width.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "HeightF6", this.Height.ToString());
         
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            {
                 
                    try
                    {
                        using (XLWorkbook workbook = new XLWorkbook())
                        {
                            workbook.Worksheets.Add(this.DTKILOS.Copy(), "Kilogramos"+usuario);
                           workbook.SaveAs("KGP" + usuario + ".xlsx");
                            Process.Start(new ProcessStartInfo("KGP" + usuario + ".xlsx") { UseShellExecute = true });
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
        private void generarExcel(DataGridView dgv)
        {
            xmlwriterOrden excel = new xmlwriterOrden();
            excel.ExportarExcel(dgv);
        }

        private void cmb_letra_SelectedIndexChanged(object sender, EventArgs e)
        {
            int size = int.Parse(cmb_letra.Text);


            // SetMinMax(dataGridView1, textBox1.Text);

            dataGridView1.Font = new Font(dataGridView1.Font.Name, size, dataGridView1.Font.Style);
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DataGridViewColumn col = new DataGridViewColumn();
            col.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }
    }
}
