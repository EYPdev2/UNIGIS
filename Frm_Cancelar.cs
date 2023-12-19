using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActualizadorDoctosUnigis
{
    public partial class Frm_Cancelar : Form
    {
        DataTable DTCE = new DataTable();
        DataTable dt;
        DataTable DAux;
        Qrys q = new Qrys();
        string Estab = "";
        string cod_estab;
        public Frm_Cancelar()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            button2.Enabled = false;

            btn_Enviar.Enabled = false;
            comboBox1.Enabled = false;


            if ((dt != null))
            {
                dt.Clear();
            }
            Estab = comboBox1.SelectedItem.ToString();
            DataTable dCod = q.Cod_estab(comboBox1.SelectedItem.ToString());

            try
            {
                cod_estab = dCod.Rows[0].ItemArray[0].ToString();
                dataGridView1.Refresh();
               // lbl_Actualizando.Visible = true;
                ConsultarDoctos.RunWorkerAsync();
            }
            catch (Exception msg)
            {

                MessageBox.Show("Este estableciemiento no cuenta con informacion, Favor de Seleccionar otro ");
                comboBox1.Enabled = true;
            }
        }

        private void ConsultarDoctos_DoWork(object sender, DoWorkEventArgs e)
        {
            dt = q.SelecDocumentos(Estab, cod_estab);
        }

        private void ConsultarDoctos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btn_Enviar.Enabled = true;
         //   lbl_Actualizando.Visible = false;
            comboBox1.Enabled = true;

            try
            {
                if (comboBox1.SelectedItem.ToString() == "")
                {
                    // DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                    // chk.HeaderText = "Enviar";
                    //  dataGridView1.Columns.Add(chk);
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
                    // dataGridView1.Sort(dataGridView1.Columns["FechaEntrega"], ListSortDirection.Ascending);
                    // SetMinMax(dataGridView1, txt_horasmax.Text);
                    // ColorCells(dataGridView1);
                }
                else
                {

                    Qrys q = new Qrys();
                    //DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                    //chk.HeaderText = "Enviar";
                    //dataGridView1.Columns.Add(chk);
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

            dataGridView1.Columns["transaccion"].Visible = false;
            dataGridView1.Columns["Estab"].Visible = false;
            dataGridView1.Columns["folio"].Visible = false;
        }


        private DataTable DivDT(DataTable DTA, string folio)
        {

            if (DTA.Columns.Count == 0)
            {
                DTA.Columns.Add("Enviar");
                DTA.Columns.Add("folio");
                DTA.Columns.Add("transaccion");
                DTA.Columns.Add("unidad");
                DTA.Columns.Add("cod_prod");
                DTA.Columns.Add("cantidad");
                DTA.Columns.Add("Status");

            }




            DataRow drLocal = null;
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                bool bChecked = Convert.ToBoolean(dr.Cells[0].Value);
                if (bChecked)
                {
                    drLocal = DTA.NewRow();
                    //drLocal["Enviar"] = dr.Cells[0].ToString();
                    drLocal["folio"] = dr.Cells["folio"].Value.ToString();
                    drLocal["transaccion"] = dr.Cells["transaccion"].Value.ToString();
                    drLocal["unidad"] = dr.Cells["unidad"].Value.ToString();
                    drLocal["cod_prod"] = dr.Cells["cod_prod"].Value;
                    // drLocal["cantidad"] = Convert.ToDouble(dr.Cells["cantidad"].Value);
                    drLocal["Status"] = dr.Cells["Status"].Value;



                    //  DTA.Rows.RemoveAt(drLocal);


                    q.UpdatePPE(drLocal["cod_prod"].ToString(), drLocal["folio"].ToString(), drLocal["transaccion"].ToString(), cod_estab, "0");

                }
            }
            return DTA;
        }
        private void Frm_Cancelar_Load(object sender, EventArgs e)
        {
            DataTable DTCE = new DataTable();
            DTCE = q.catalogoEstab();
            foreach (DataRow row in DTCE.Rows)
            {
                comboBox1.Items.Add(row[0].ToString());
            }

        }

        private void bnt_Refrescar_Click(object sender, EventArgs e)
        {
            // bnt_Refrescar.Enabled = false;
            if ((dt != null))
            {
                dt.Clear();
            }
            Estab = comboBox1.SelectedItem.ToString();
            dataGridView1.Refresh();
           // lbl_Actualizando.Visible = true;
            ConsultarDoctos.RunWorkerAsync();
        }

        private void btn_Enviar_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Deseas enviar la informacion seleccionada a Unigis una vez se envie no podra eliminarse", "Informacion ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result1 == System.Windows.Forms.DialogResult.Yes)
            {
                DataTable dtLocalC = new DataTable();
                dtLocalC = DivDT(dt, "");
                MessageBox.Show("Se han cancelados los documentos ");
                dataGridView1.Refresh();

            }

            if ((dt != null))
            {
                dt.Clear();
            }
            Estab = comboBox1.SelectedItem.ToString();


            dataGridView1.Refresh();
           // lbl_Actualizando.Visible = true;
            ConsultarDoctos.RunWorkerAsync();
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
    }
}
