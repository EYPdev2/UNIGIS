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
    public partial class Frm_Liquidacion : Form
    {
        string usuario;
        public Frm_Liquidacion()
        {
            
        }


        Qrys c = new Qrys();
        public Frm_Liquidacion(string usuarioN)
        {
            InitializeComponent();
            usuario = usuarioN;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "")
            {
                MessageBox.Show("Favor de capturar folio");
            }
            else
            {
                Guardar();
            }
            
        }

        
        private void Guardar()
        {
            DialogResult result1 = MessageBox.Show("¿Desea Guardar la modificación?", "Informacion ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result1 == DialogResult.Yes)
            {
                if (TextBox1.Text != "")
                {
                    try
                    {
                        TextBox2.Text = c.CancelarLiq(TextBox1.Text, usuario);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("No se encontro Informacion sobre el folio proporcionado " + TextBox1.Text);
                        
                    }
                }
            }


        }
    }
}
