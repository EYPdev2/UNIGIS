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

    public partial class Relacion_Pickup : Form
    {
        DataTable dtaux = new DataTable();
        string documento;
        public Relacion_Pickup()
        {
            InitializeComponent();
        }
        public Relacion_Pickup(DataTable dt,string doc)
        {
            InitializeComponent();
            dtaux = dt;
           // dataGridView1.DataSource = dtaux;
          
            

            documento = doc;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            DataView dv = new DataView(dtaux);
            dv.RowFilter = "Delivery = '" + documento + "'";
            dataGridView1.DataSource = dv;
              
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
          //  this.Hide();
            oc_Buscador frm_OC = new oc_Buscador(documento, dtaux);
            frm_OC.ShowDialog();

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            foreach (DataRow  row    in dtaux.Rows)
            {
                var rowA = dataGridView1.Rows[dataGridView1.CurrentRow.Index];
               if (rowA.Cells[1].Value.ToString() == row.ItemArray[1].ToString())
                {
                    row.Delete();
                    return;
                }
                
            }


                   
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
