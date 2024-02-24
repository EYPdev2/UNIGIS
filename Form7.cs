using ClosedXML;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActualizadorDoctosUnigis
{
    public partial class Form7 : Form
    {
        Qrys q = new Qrys();
        Decimal Min = 24;
        Decimal Max = 0;
        Decimal Mid = 0;
        Decimal Steps = Decimal.MinValue;
        Decimal MaxV = 0;
        decimal minv = 0;
        DataTable dt = new DataTable();
        string usuario = "";
        public Form7( string u )
        {
            InitializeComponent();
            usuario = u;
        }

        private void Form7_Load(object sender, EventArgs e)
        {

 
            if (Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "TOPF7").ToString() != "")
            {
                this.Top = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "TOPF7").ToString());
                this.Left = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "LeftF7").ToString());
                this.Width = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "WidthF7").ToString());
                this.Height = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "HeightF7").ToString());
            }
            dataGridView1.DataSource= q.ValesPendientes();
            dataGridView1.Sort(dataGridView1.Columns["Fecha"], ListSortDirection.Ascending);
            SetMinMax(dataGridView1, "0");
            ColorCells(dataGridView1);
            dataGridView1.Columns["Folio_Docto"].Width = 80;
            dataGridView1.Columns["Transaccion"].Width = 40;
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
                    //MessageBox.Show(dias.TotalHours.ToString() +" "+ Convert.ToDateTime(row[2].ToString()).ToString("dd-MM-yyyyy") +" " + DateTime.Now.ToString("dd-MM-yyyyy"));
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
                        // dataGridView1.BackgroundColor = cellColor;
                    }
                }
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = q.ValesPendientes();
            dataGridView1.Sort(dataGridView1.Columns["Fecha"], ListSortDirection.Ascending);
            SetMinMax(dataGridView1, "0");
            ColorCells(dataGridView1);
            dataGridView1.Columns["Folio_Docto"].Width = 80;
            dataGridView1.Columns["Transaccion"].Width = 40;
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            SetMinMax(dataGridView1, "0");
            ColorCells(dataGridView1);
        }

        private void Form7_FormClosing(object sender, FormClosingEventArgs e)
        {
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "TOPF7", this.Top.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "LeftF7", this.Left.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "WidthF7", this.Width.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "HeightF7", this.Height.ToString());


        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            string ruta2 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DataTable dtaux = new DataTable();
                dtaux = q.ValesPendientes();
            try
            {
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    workbook.Worksheets.Add( dtaux.Copy(), "Vales Pendientes" +usuario  );
                    workbook.SaveAs("Vales Pendientes" + usuario + ".xlsx");
                    Process.Start(new ProcessStartInfo("Vales Pendientes" + usuario + ".xlsx") { UseShellExecute = true });
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
            button2.Enabled = true;

        }
        private void generarExcel(DataGridView dgv)
        {
            xmlwriterOrden excel = new xmlwriterOrden();
            excel.ExportarExcel(dgv);
        }
    }
}
