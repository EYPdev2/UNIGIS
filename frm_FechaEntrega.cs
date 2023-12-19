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
        Qrys q = new Qrys();
        Libreria lib = new Libreria();
        string[] t = new string[5];
        string u;
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
             DataTable dt= q.Consultar("Select  distinct t.nombre,t.transaccion from facremtick f inner Join transacciones t on t.transaccion = f.transaccion");
            int i = 0; 
            foreach(DataRow row in dt.Rows)
            {
                cmb_tipoD.Items.Add(row[0].ToString());
                t[i] = row[1].ToString();
                i++;
            }
            dateTimePicker2.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dateTimePicker2.Format = DateTimePickerFormat.Custom ;
        }

        private void btn_Actualizar_Click(object sender, EventArgs e)
        {
            if (txt_folio.Text!="" && cmb_tipoD.SelectedItem.ToString() != "")
            {
                try
                {
                    DataTable daux = lib.Conexiones(txt_folio.Text);
                    DataTable dt = q.Consultar("Select  f.fecha,f.fecha_entrega, rtrim(e.nombre)  ,f.status, f.recoge_mercancia from facremtick f " +
                                               "inner Join establecimientos e on f.cod_estab = e.cod_estab " +
                                               "where f.folio = '" + txt_folio.Text + "' And f.transaccion = '" + t[cmb_tipoD.SelectedIndex] + "'", daux.Rows[0][0].ToString());
                    txt_estab.Text = dt.Rows[0][2].ToString();
                    dateTimePicker1.Value = DateTime.Parse(dt.Rows[0][0].ToString());
                    dateTimePicker2.Value = DateTime.Parse(dt.Rows[0][1].ToString());
                    btn_Actualizar.Enabled = false;
                    btn_Guardar.Enabled = true;

                }
            catch
            {

            }

        }
            else
            {
                MessageBox.Show("Favor de Proporcionar la informacion Solicitada (Folio y Transaccion )");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        private void guardar(string folio,string transaccion ,string fecha)
        {
            q.ActualizarFechaEntrega(folio, transaccion, fecha, u);
        }

        private void btn_Guardar_Click  (object sender, EventArgs e)
        {
        
            //dateTimePicker1.Value.Hour = DateTime.Now.Hour;
            if (dateTimePicker2.Value >= dateTimePicker1.Value)
            {
                try
            {

                    guardar(txt_folio.Text, t[cmb_tipoD.SelectedIndex], dateTimePicker2.Value.ToString("dd/MM/yyyy HH:mm:ss"));
                    MessageBox.Show("Actualizacion correcta del Folio: " + txt_folio.Text);
                    txt_folio.Text = "";
                    txt_estab.Text = "";
                    dateTimePicker1.Value = DateTime.Now;
                    dateTimePicker2.Value = DateTime.Now;

                }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            btn_Guardar.Enabled = false;
            btn_Actualizar.Enabled = true;
            }
            else { MessageBox.Show("No se puede actualizar Documento " + txt_folio.Text + " a una fecha menor de su Documento","Atencion",MessageBoxButtons.OK);}
        }
    }
}
