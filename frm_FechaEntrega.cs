using ActualizadorDoctosUnigis.Contoller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActualizadorDoctosUnigis
{
    public partial class frm_FechaEntrega : Form
    {
        private Qrys q = new Qrys();
        private Libreria lib = new Libreria();
        private string[] t = new string[5];
        private string u;
        private string[] R = new string[15];
        private string[] U = new string[100];
        private bool valor = true;
        public frm_FechaEntrega()
        {
            InitializeComponent();
        }
        public frm_FechaEntrega(string usuario)
        {
            InitializeComponent();
            u = usuario;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmb_tipoD_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frm_FechaEntrega_Load(object sender, EventArgs e)
        {

            DataTable dataTable1 = this.q.Consultar("Select  distinct t.nombre,t.transaccion from facremtick f inner Join transacciones t on t.transaccion = f.transaccion");
            int index1 = 0;
            foreach (DataRow row in (InternalDataCollectionBase)dataTable1.Rows)
            {
                this.cmb_tipoD.Items.Add((object)row[0].ToString());
                this.t[index1] = row[1].ToString();
                ++index1;
            }
            DataTable dataTable2 = this.q.Consultar("EXEC [dbo].[EyP_Razones_RM] ");
            int index2 = 0;
            foreach (DataRow row in (InternalDataCollectionBase)dataTable2.Rows)
            {
                this.Razon.Items.Add((object)row[1].ToString());
                this.R[index2] = row[0].ToString();
                ++index2;
            }
            DataTable dataTable3 = this.q.Consultar("exec [dbo].[EyP_USUARIOS_RM]  ");
            int index3 = 0;
            foreach (DataRow row in (InternalDataCollectionBase)dataTable3.Rows)
            {
                this.Solicita.Items.Add((object)row[1].ToString());
                this.U[index3] = row[0].ToString();
                ++index3;
            }
            this.dateTimePicker2.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
        }

        private void btn_Actualizar_Click(object sender, EventArgs e)
        {
            if (this.txt_folio.Text != "" && this.cmb_tipoD.SelectedItem.ToString() != "")
            {
                try
                {
                    DataTable dataTable = this.q.Consultar($"Select  f.fecha,f.fecha_entrega, rtrim(e.nombre)  ,f.status, f.recoge_mercancia from facremtick f inner Join establecimientos e on f.cod_estab = e.cod_estab where f.folio = '{this.txt_folio.Text}' And f.transaccion = '{this.t[this.cmb_tipoD.SelectedIndex]}'", this.lib.Conexiones(this.txt_folio.Text).Rows[0][0].ToString());
                    this.txt_estab.Text = dataTable.Rows[0][2].ToString();
                    this.dateTimePicker1.Value = DateTime.Parse(dataTable.Rows[0][0].ToString());
                    this.dateTimePicker2.Value = DateTime.Parse(dataTable.Rows[0][1].ToString());
                    this.btn_Actualizar.Enabled = false;
                    this.btn_Guardar.Enabled = true;
                }
                catch
                {
                }
            }
            else
            {
                int num = (int)MessageBox.Show("Favor de Proporcionar la informacion Solicitada (Folio y Transaccion )");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        //Metodo que  manda a ejecutar una funcion que se conecta a la base de datos y guarda la informacion enviada 
        private void guardar(
     string folio,
     string transaccion,
     string fecha,
     string solicita,
     string razon)
        {
            this.q.ActualizarFechaEntrega(folio, transaccion, fecha, this.u, solicita, razon);
        }

        private void btn_Guardar_Click  (object sender, EventArgs e)
        {
            if (this.dateTimePicker2.Value >= this.dateTimePicker1.Value)
            {
                try
                {
                    if (this.Solicita.SelectedItem != null && this.Razon.SelectedItem != null)
                    {
                        this.guardar(this.txt_folio.Text, this.t[this.cmb_tipoD.SelectedIndex], this.dateTimePicker2.Value.ToString("dd/MM/yyyy HH:mm:ss"), this.U[this.Solicita.SelectedIndex], this.R[this.Razon.SelectedIndex]);
                        int num = (int)MessageBox.Show("Actualizacion correcta del Folio: " + this.txt_folio.Text);
                        txt_folio.Text = "";
                        txt_estab.Text = "";
                        dateTimePicker1.Value = DateTime.Now;
                        dateTimePicker2.Value = DateTime.Now;
                        Solicita.SelectedIndex = -1;
                        Razon.SelectedIndex = -1;
                        cmb_tipoD.Text = "";
                    }
                    else
                    {
                        int num1 = (int)MessageBox.Show("No se puede actualizar Documento , Seleccione Solicita y Razon ", "Atencion", MessageBoxButtons.OK);
                    }
                }
                catch (Exception ex)
                {
                    int num = (int)MessageBox.Show(ex.Message);
                }
                this.btn_Guardar.Enabled = false;
                this.btn_Actualizar.Enabled = true;
            }
            else
            {
                int num2 = (int)MessageBox.Show($"No se puede actualizar Documento {this.txt_folio.Text} a una fecha menor de su Documento", "Atencion", MessageBoxButtons.OK);
            }
        }
    }
}
