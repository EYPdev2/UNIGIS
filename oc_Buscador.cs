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
    public partial class oc_Buscador : Form
    {
        DataTable dtaux;
        Qrys q = new Qrys();
        string delivery;
        public oc_Buscador()
        {
            InitializeComponent();
        }

        public oc_Buscador(string delivery,DataTable dt)
        {
            InitializeComponent();
            this.delivery = delivery;
            dtaux = dt;
        }



        private void oc_Buscador_Load(object sender, EventArgs e)
        {
            txt_delivery.Text = delivery;
            comboBox1.DataSource = q.Consultar("Select nombre, transaccion from transacciones where transaccion in ('30','29')");
            comboBox1.SelectedItem = 0;
            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "transaccion";

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (comboBox1.SelectedValue.ToString().Trim() == "30"){
                    dataGridView1.DataSource = q.Consultar("select mp.cod_prod as producto ,p.descripcion,mp.cantidad_autorizada,unidad,peso from mpedprv mp" +
                    " left join productos p on mp.cod_prod=p.cod_prod" +
                    " where folio='" + txt_oc.Text + "'");
                    if ((dataGridView1.RowCount > 0))
                    {
                        toolStripButton1.Enabled = true;
                        toolStripButton2.Enabled = true;
                        DataTable RZ = q.Consultar("select CONCAT(rtrim(p.cod_prv), '-', rtrim(prv.razon_social)) from pedprv p" +
                        " left join proveedores prv on p.cod_prv = prv.cod_prv" +
                        " where p.folio = '" + txt_oc.Text + "'");
                        lbl_provedor.Text = RZ.Rows[0].ItemArray[0].ToString();

                    }
                }

                if (comboBox1.SelectedValue.ToString().Trim() == "29")
                {
                    dataGridView1.DataSource = q.Consultar(" select pe.cod_prod,p.descripcion,pe.cantidad_autorizada,pe.unidad,pe.peso  from mpedestab pe "+
                                                " left join productos p on p.cod_prod = pe.cod_prod"+
                                                " left join pedestab pe2 on pe.folio = pe2.folio"+
                                                " where   pe.folio = '"+txt_oc.Text+"   ' and pe2.status = 'A'");
                    if ((dataGridView1.RowCount > 0))
                    {
                        toolStripButton1.Enabled = true;
                        toolStripButton2.Enabled = true;
                        DataTable RZ = q.Consultar(" select  e.nombre  from pedestab pe " +
                                        " left join establecimientos e on pe.cod_estab_alterno = e.cod_estab " +
                                        " where   pe.folio = '" + txt_oc.Text + "   '  and pe.status = 'A' ");
                        lbl_provedor.Text = RZ.Rows[0].ItemArray[0].ToString();

                    }
                }



            }
        }

        public void obtenerOC(DataTable dt)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView(dtaux);
            dv.RowFilter = "Pickup = '" + txt_oc.Text + "'";

            if (dv.Count == 0)
            {

             
                dtaux.Rows.Add(txt_delivery.Text, txt_oc.Text,comboBox1.SelectedValue.ToString());
                MessageBox.Show("Pickup Agregado Correctamente");
                limpiar();
            }
            else
            {
                MessageBox.Show("Este documento ya se encuentra Registrado en un delivery","Alerta",MessageBoxButtons.OK);
                limpiar();
            }
         
        }
        public void limpiar()
        {
            txt_oc.Text = "";
            lbl_provedor.Text = "";
            toolStripButton1.Enabled = false;
            toolStripButton2.Enabled = false;
            dataGridView1.DataSource = null;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            dataGridView1.Enabled = false;
            dataGridView1.Enabled = true;
        }
    }
}
