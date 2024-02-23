using ActualizadorDoctosUnigis.Models;
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
    public partial class frm_detalle_parada : Form
    {
        public frm_detalle_parada()
        {
            InitializeComponent();
        }

        public frm_detalle_parada(string idparada, string Estado, DataTable dt)
        {
            InitializeComponent();
            lbl_Estado.Text = Estado;
            lb_parada.Text = idparada;
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].Width = 235   ;
            colorear(dataGridView1, lbl_Estado);

        }
        private void frm_detalle_parada_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //colorea una celda dependiendo el estatus del viaje
        private void colorear(DataGridView dgv,Label lb)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (lb.Text == "No Entregado")
                {
                    
                    lb.BackColor = Color.Red;
                    lb.ForeColor = Color.White;
                }
           
                if (lb.Text == "Entregado")
                {

                    lb.BackColor = Color.Green;
                    lb.ForeColor = Color.White;
                }
                if (lb.Text == "Entrega parcial")
                {

                    lb.BackColor = Color.Yellow;
                    lb.ForeColor = Color.Black;
                }


            }

        }
    }
}
