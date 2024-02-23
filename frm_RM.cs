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
    public partial class frm_RM : Form
    {
        Qrys q = new Qrys();
        Libreria lib = new Libreria();
        string[] t = new string[5];
        string u ;
        public frm_RM()
        {
            InitializeComponent();
        }
        public frm_RM(string usuario)
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
                    txt_RM.Text = dt.Rows[0][3].ToString();
                    chk_rm.Checked = Convert.ToBoolean(dt.Rows[0][4].ToString());
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
        //Metodo que  manda a ejecutar una funcion que se conecta a la base de datos y guarda la informacion enviada 
        private void guardar(string folio,string transaccion ,string fecha)
        {
            q.ActualizarFechaEntrega(folio, transaccion, fecha, u);
        }

        private void btn_Guardar_Click  (object sender, EventArgs e)
        {
           
                int chk = 0;
                int chk2 = 0;
                if (chk_rm.Checked){
                    chk = 1;
                }
                if (chk == 0)
                {
                    chk2 = 1;
                }


                try { 


                //    MessageBox.Show(lib.Conexiones(txt_folio.Text).Rows[0][0].ToString());
                //MessageBox.Show("update facremtick set recoge_mercancia ='" + chk + "' where folio='" + txt_folio.Text + "', and transaccion='" + t[cmb_tipoD.SelectedIndex] + "'") ;
                    q.Consultar("update facremtick set recoge_mercancia ='" + chk + "' where folio='" + txt_folio.Text + "' and transaccion='" + t[cmb_tipoD.SelectedIndex] + "'", lib.Conexiones(txt_folio.Text).Rows[0][0].ToString());
                    string y = "insert into historico_cambios(codigo, transaccion, fecha, dato, valor_anterior, valor_nuevo, usuario values('" + txt_folio.Text + "')" +
                        ",'" + t[cmb_tipoD.SelectedIndex] + "','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + "','Modificacion RM APP','" + chk2 + "','" + chk + "','" + u + "'";
                    q.Consultar("insert into historico_cambios(codigo,transaccion,fecha,dato,valor_anterior,valor_nuevo,usuario) values ('"+txt_folio.Text+"'" +
                        ",'"+t[cmb_tipoD.SelectedIndex]+"','"+DateTime.Now.ToString("yyyy/MM/dd HH:mm")+"','Modificacion RM APP','"+chk2+"','"+chk+"','"+u+"')", lib.Conexiones(txt_folio.Text).Rows[0][0].ToString());
                    
                    // guardar(txt_folio.Text, t[cmb_tipoD.SelectedIndex], dateTimePicker2.Value.ToString("dd/MM/yyyy HH:mm:ss"));
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
                  

        private void lbl_fechaEntrega_Click(object sender, EventArgs e)
        {

        }

        private void txt_estab_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lbl_FE_Click(object sender, EventArgs e)
        {

        }

        private void txt_folio_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbl_Folio_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
