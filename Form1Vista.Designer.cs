﻿
namespace ActualizadorDoctosUnigis
{
    partial class Form1Vista
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1Vista));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ActualizarVales = new System.ComponentModel.BackgroundWorker();
            this.ConsultarDoctos = new System.ComponentModel.BackgroundWorker();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.cmb_letra = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.bnt_Refrescar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lbl_Actualizando = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_Enviar = new System.Windows.Forms.Button();
            this.btn_congif = new System.Windows.Forms.Button();
            this.lbl_horasmin = new System.Windows.Forms.Label();
            this.lbl_horasmax = new System.Windows.Forms.Label();
            this.txt_horasmin = new System.Windows.Forms.TextBox();
            this.txt_horasmax = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ActualizarVales
            // 
            this.ActualizarVales.WorkerReportsProgress = true;
            this.ActualizarVales.WorkerSupportsCancellation = true;
            this.ActualizarVales.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.ActualizarVales.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ActualizarVales_ProgressChanged);
            this.ActualizarVales.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // ConsultarDoctos
            // 
            this.ConsultarDoctos.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ConsultarDoctos_DoWork);
            this.ConsultarDoctos.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ConsultarDoctos_RunWorkerCompleted);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer2.MinimumSize = new System.Drawing.Size(400, 369);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.splitContainer2.Panel1.Controls.Add(this.cmb_letra);
            this.splitContainer2.Panel1.Controls.Add(this.button3);
            this.splitContainer2.Panel1.Controls.Add(this.button1);
            this.splitContainer2.Panel1.Controls.Add(this.bnt_Refrescar);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.comboBox1);
            this.splitContainer2.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer2_Panel1_Paint);
            this.splitContainer2.Panel1MinSize = 0;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Panel2MinSize = 0;
            this.splitContainer2.Size = new System.Drawing.Size(1395, 845);
            this.splitContainer2.SplitterDistance = 68;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 0;
            // 
            // cmb_letra
            // 
            this.cmb_letra.Enabled = false;
            this.cmb_letra.FormattingEnabled = true;
            this.cmb_letra.Items.AddRange(new object[] {
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "30",
            "40",
            "45"});
            this.cmb_letra.Location = new System.Drawing.Point(974, 10);
            this.cmb_letra.Name = "cmb_letra";
            this.cmb_letra.Size = new System.Drawing.Size(121, 21);
            this.cmb_letra.TabIndex = 22;
            this.cmb_letra.SelectedIndexChanged += new System.EventHandler(this.cmb_letra_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.BackgroundImage = global::ActualizadorDoctosUnigis.Properties.Resources.font_adjustment;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(929, 6);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(38, 29);
            this.button3.TabIndex = 21;
            this.button3.Text = " ";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::ActualizadorDoctosUnigis.Properties.Resources.OIP;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(871, 6);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(43, 28);
            this.button1.TabIndex = 17;
            this.button1.Text = " ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_5);
            // 
            // bnt_Refrescar
            // 
            this.bnt_Refrescar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bnt_Refrescar.BackgroundImage")));
            this.bnt_Refrescar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bnt_Refrescar.Enabled = false;
            this.bnt_Refrescar.Location = new System.Drawing.Point(1, 10);
            this.bnt_Refrescar.Margin = new System.Windows.Forms.Padding(4);
            this.bnt_Refrescar.Name = "bnt_Refrescar";
            this.bnt_Refrescar.Size = new System.Drawing.Size(49, 23);
            this.bnt_Refrescar.TabIndex = 2;
            this.bnt_Refrescar.UseVisualStyleBackColor = true;
            this.bnt_Refrescar.Click += new System.EventHandler(this.bnt_Refrescar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(59, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Refrescar Documentos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(260, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Establecimiento:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(397, 6);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(464, 21);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer3.Location = new System.Drawing.Point(1, 4);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.splitContainer3.Panel1.Controls.Add(this.lbl_Actualizando);
            this.splitContainer3.Panel1.Controls.Add(this.dataGridView1);
            this.splitContainer3.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer3_Panel1_Paint);
            this.splitContainer3.Panel1MinSize = 0;
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.splitContainer3.Panel2.Controls.Add(this.btn_Enviar);
            this.splitContainer3.Panel2.Controls.Add(this.btn_congif);
            this.splitContainer3.Panel2.Controls.Add(this.lbl_horasmin);
            this.splitContainer3.Panel2.Controls.Add(this.lbl_horasmax);
            this.splitContainer3.Panel2.Controls.Add(this.txt_horasmin);
            this.splitContainer3.Panel2.Controls.Add(this.txt_horasmax);
            this.splitContainer3.Panel2MinSize = 0;
            this.splitContainer3.Size = new System.Drawing.Size(1391, 750);
            this.splitContainer3.SplitterDistance = 686;
            this.splitContainer3.SplitterWidth = 5;
            this.splitContainer3.TabIndex = 0;
            // 
            // lbl_Actualizando
            // 
            this.lbl_Actualizando.AutoSize = true;
            this.lbl_Actualizando.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_Actualizando.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Actualizando.Location = new System.Drawing.Point(557, 329);
            this.lbl_Actualizando.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Actualizando.Name = "lbl_Actualizando";
            this.lbl_Actualizando.Size = new System.Drawing.Size(138, 25);
            this.lbl_Actualizando.TabIndex = 12;
            this.lbl_Actualizando.Text = "Consultando...";
            this.lbl_Actualizando.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(5, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Size = new System.Drawing.Size(1382, 682);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // btn_Enviar
            // 
            this.btn_Enviar.Enabled = false;
            this.btn_Enviar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_Enviar.Location = new System.Drawing.Point(12, 33);
            this.btn_Enviar.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Enviar.Name = "btn_Enviar";
            this.btn_Enviar.Size = new System.Drawing.Size(99, 28);
            this.btn_Enviar.TabIndex = 0;
            this.btn_Enviar.Text = "Enviar";
            this.btn_Enviar.UseVisualStyleBackColor = true;
            this.btn_Enviar.Visible = false;
            this.btn_Enviar.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_congif
            // 
            this.btn_congif.Location = new System.Drawing.Point(22, 4);
            this.btn_congif.Margin = new System.Windows.Forms.Padding(4);
            this.btn_congif.Name = "btn_congif";
            this.btn_congif.Size = new System.Drawing.Size(29, 28);
            this.btn_congif.TabIndex = 4;
            this.btn_congif.Text = "button1";
            this.btn_congif.UseVisualStyleBackColor = true;
            this.btn_congif.Click += new System.EventHandler(this.btn_congif_Click);
            // 
            // lbl_horasmin
            // 
            this.lbl_horasmin.AutoSize = true;
            this.lbl_horasmin.Location = new System.Drawing.Point(61, 10);
            this.lbl_horasmin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_horasmin.Name = "lbl_horasmin";
            this.lbl_horasmin.Size = new System.Drawing.Size(55, 13);
            this.lbl_horasmin.TabIndex = 5;
            this.lbl_horasmin.Text = "Horas Min";
            this.lbl_horasmin.Visible = false;
            // 
            // lbl_horasmax
            // 
            this.lbl_horasmax.AutoSize = true;
            this.lbl_horasmax.Location = new System.Drawing.Point(175, 11);
            this.lbl_horasmax.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_horasmax.Name = "lbl_horasmax";
            this.lbl_horasmax.Size = new System.Drawing.Size(61, 13);
            this.lbl_horasmax.TabIndex = 6;
            this.lbl_horasmax.Text = "Horas Max:";
            this.lbl_horasmax.Visible = false;
            // 
            // txt_horasmin
            // 
            this.txt_horasmin.Location = new System.Drawing.Point(143, 6);
            this.txt_horasmin.Margin = new System.Windows.Forms.Padding(4);
            this.txt_horasmin.Name = "txt_horasmin";
            this.txt_horasmin.Size = new System.Drawing.Size(24, 20);
            this.txt_horasmin.TabIndex = 7;
            this.txt_horasmin.Text = "24";
            this.txt_horasmin.Visible = false;
            // 
            // txt_horasmax
            // 
            this.txt_horasmax.Location = new System.Drawing.Point(264, 7);
            this.txt_horasmax.Margin = new System.Windows.Forms.Padding(4);
            this.txt_horasmax.Name = "txt_horasmax";
            this.txt_horasmax.Size = new System.Drawing.Size(39, 20);
            this.txt_horasmax.TabIndex = 8;
            this.txt_horasmax.Text = "240";
            this.txt_horasmax.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(1430, 845);
            this.splitContainer1.SplitterDistance = 1395;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 15;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer4.Panel1MinSize = 0;
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.splitContainer4.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer4_Panel2_Paint);
            this.splitContainer4.Panel2MinSize = 0;
            this.splitContainer4.Size = new System.Drawing.Size(30, 845);
            this.splitContainer4.SplitterDistance = 49;
            this.splitContainer4.SplitterWidth = 5;
            this.splitContainer4.TabIndex = 0;
            this.splitContainer4.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer4_SplitterMoved);
            // 
            // Form1Vista
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1430, 845);
            this.Controls.Add(this.splitContainer1);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(954, 480);
            this.Name = "Form1Vista";
            this.Text = "Enviar Documentos";
            this.Load += new System.EventHandler(this.Form1Vista_Load);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker ActualizarVales;
        private System.ComponentModel.BackgroundWorker ConsultarDoctos;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button bnt_Refrescar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label lbl_Actualizando;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_Enviar;
        private System.Windows.Forms.Button btn_congif;
        private System.Windows.Forms.Label lbl_horasmin;
        private System.Windows.Forms.Label lbl_horasmax;
        private System.Windows.Forms.TextBox txt_horasmin;
        private System.Windows.Forms.TextBox txt_horasmax;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.ComboBox cmb_letra;
        private System.Windows.Forms.Button button3;
    }
}

