using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActualizadorDoctosUnigis
{
    public partial class Main : Form
    {
        public string usuario="", nivel="", nombreU = "";
        private int childFormNumber = 0;
       


        public Main()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
        }
       

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {


            lbl_usuario.Text = "Usuario: " + usuario +" "+nombreU;
            lbl_nivel.Text = "Nivel : " + nivel;
       
            var MyIni = new MyProg.IniFile(Directory.GetCurrentDirectory().ToString()+"\\config.ini");

            string x =MyIni.Read("N"+nivel, "ACCESOS");
            
            activarModulo(x);  
        }

        private void modificarFechaEntregaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModificarFechaEntrega Child_form = new ModificarFechaEntrega();
            Child_form.MdiParent = this;
            childFormNumber += 1;
            Child_form.Text = "Fecha Entrega " + childFormNumber;
            Child_form.Show();
        }

        private void liberarParadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_ValidaParada Child_form = new frm_ValidaParada(usuario);
            Child_form.MdiParent = this;
            childFormNumber += 1;
            Child_form.Text = "Liberar Viaje" + childFormNumber;
            Child_form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 Child_form = new Form1(usuario);
            Child_form.MdiParent = this;
            childFormNumber += 1;
            Child_form.Text = "Envio Documentos " + childFormNumber;
            Child_form.Show();
            Child_form.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        
            
            
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); 
        }

        private void windowsMenu_Click(object sender, EventArgs e)
        {

        }

        private void modificarRecogeMercanciaRToolStripMenuItem_Click(object sender, EventArgs e)
        {


            frm_RM Child_form = new frm_RM(usuario);
            Child_form.MdiParent = this;
            childFormNumber += 1;
            Child_form.Text = "Recoge Mercancia " + childFormNumber;
            Child_form.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Child_form.Show();

        }

        private void modificarFechaEntregaToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            frm_FechaEntrega Child_form = new frm_FechaEntrega(usuario);
            Child_form.MdiParent = this;
            childFormNumber += 1;
            Child_form.Text = "Fecha Entrega " + childFormNumber;
            Child_form.WindowState  = System.Windows.Forms.FormWindowState.Maximized;
            Child_form.Show();
        }

        private void validarParadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Validar_Paradas Child_form = new frm_Validar_Paradas(usuario );
            Child_form.MdiParent = this;
            childFormNumber += 1;
            Child_form.Text = "Validar Viaje" + childFormNumber;
            Child_form.Show();
        }

        private void pendientesPorEmbarcarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1Vista Child_form = new Form1Vista(usuario);
            Child_form.MdiParent = this;
            childFormNumber += 1;
            Child_form.Text = "Kilogramos Pendientes" + childFormNumber;
            Child_form.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Child_form.Show();
        }

        private void kGPendientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 Child_form = new Form6(usuario );
         
            Child_form.Show();

        }

        private void valesPendienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 Child_form = new Form7(usuario);

            Child_form.Show();
        }

        private void liquidacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void cancelarLiquidacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Liquidacion ChildForm=  new  Frm_Liquidacion(usuario);
            ChildForm.MdiParent = this;
            childFormNumber += 1;
            ChildForm.Text = "Liquidacion" + childFormNumber;
            ChildForm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ChildForm.Show();

     
        }

        private void cancelarDocumentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Cancelar ChildForm = new Frm_Cancelar();
            ChildForm.MdiParent = this;
            childFormNumber += 1;
            ChildForm.Text = "Cancelar Documentos" + childFormNumber;
            ChildForm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ChildForm.Show();
        }

        private void confirmarOrdenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
             
            Form2 Child_form = new Form2(usuario,nombreU,nivel);
            Child_form.MdiParent = this;
            childFormNumber += 1;
            Child_form.Text = "Confirmar Ordenes " + childFormNumber;
            Child_form.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Child_form.Show();
        }

        // Metodo para gestionar el acceso a los formularios dependiendo el nivel de usuario
        public void activarModulo(string a)
        {
            a = a.Replace(",", "");
            char[] acceso = new char[a.Length];
            acceso = a.ToCharArray();

            //0=false
            //1=true

            EnvioToolStripMenuItem.Enabled = Convert.ToBoolean(Convert.ToInt32(acceso[0].ToString()));
            confirmarOrdenesToolStripMenuItem.Enabled = Convert.ToBoolean(Convert.ToInt32(acceso[1].ToString()));
            liberarParadasToolStripMenuItem.Enabled = Convert.ToBoolean(Convert.ToInt32(acceso[2].ToString()));
            modificarFechaEntregaToolStripMenuItem1.Enabled = Convert.ToBoolean(Convert.ToInt32(acceso[3].ToString()));
            modificarRecogeMercanciaRToolStripMenuItem.Enabled= Convert.ToBoolean(Convert.ToInt32(acceso[4].ToString()));
            cancelarLiquidacionToolStripMenuItem.Enabled= Convert.ToBoolean(Convert.ToInt32(acceso[3].ToString()));
            cancelarDocumentosToolStripMenuItem.Enabled = Convert.ToBoolean(Convert.ToInt32(acceso[0].ToString()));
            validarParadasToolStripMenuItem.Enabled = Convert.ToBoolean(Convert.ToInt32(acceso[5].ToString()));
            pendientesPorEmbarcarToolStripMenuItem.Enabled = Convert.ToBoolean(Convert.ToInt32(acceso[6].ToString()));
            kGPendientesToolStripMenuItem.Enabled = Convert.ToBoolean(Convert.ToInt32(acceso[6].ToString()));
            valesPendienteToolStripMenuItem.Enabled = Convert.ToBoolean(Convert.ToInt32(acceso[6].ToString()));
        }
       

            

    }
}
