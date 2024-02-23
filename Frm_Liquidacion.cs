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
            this.MouseWheel += func_mouseWheel;

           
        }
        int num = 0;
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
      

        private void func_mouseWheel(object sender,MouseEventArgs e)
        {
            if(e.Delta > 0)
            {
                num++;
                label2.Font = new Font(label2.Font.Name, num, label2.Font.Style);
            }
            else
            {
                num--;
                label2.Font = new Font(label2.Font.Name, num, label2.Font.Style);
            }
        }

        //Método que valida si el usuario ha digitado el folio de lo contrario se manda de lo contrario se inserta la información en la base de datos 
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int size = int.Parse(comboBox1.Text);
            label2.Font = new Font(label2.Font.Name, size, label2.Font.Style);

        }
    }
}
