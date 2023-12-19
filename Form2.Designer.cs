
namespace ActualizadorDoctosUnigis
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.lbl_jornada = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Jornada = new System.Windows.Forms.TextBox();
            this.txt_Ruta = new System.Windows.Forms.TextBox();
            this.dgv_Direcciones = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_PRoductos = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txt_fecha = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_peso = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Editar = new System.Windows.Forms.Button();
            this.btn_guardar = new System.Windows.Forms.Button();
            this.txt_estado = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.gbx_2 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.gbx_1 = new System.Windows.Forms.GroupBox();
            this.btn_imp = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Direcciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.gbx_2.SuspendLayout();
            this.gbx_1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_jornada
            // 
            this.lbl_jornada.AutoSize = true;
            this.lbl_jornada.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_jornada.Location = new System.Drawing.Point(39, 46);
            this.lbl_jornada.Name = "lbl_jornada";
            this.lbl_jornada.Size = new System.Drawing.Size(62, 13);
            this.lbl_jornada.TabIndex = 0;
            this.lbl_jornada.Text = "ID_Jornada";
            this.lbl_jornada.Click += new System.EventHandler(this.lbl_jornada_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(54, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "ID_Ruta";
            // 
            // txt_Jornada
            // 
            this.txt_Jornada.Location = new System.Drawing.Point(105, 43);
            this.txt_Jornada.Name = "txt_Jornada";
            this.txt_Jornada.Size = new System.Drawing.Size(100, 20);
            this.txt_Jornada.TabIndex = 2;
            this.txt_Jornada.TextChanged += new System.EventHandler(this.txt_Jornada_TextChanged);
            this.txt_Jornada.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Jornada_KeyDown);
            this.txt_Jornada.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Jornada_KeyPress);
            // 
            // txt_Ruta
            // 
            this.txt_Ruta.Location = new System.Drawing.Point(105, 10);
            this.txt_Ruta.Name = "txt_Ruta";
            this.txt_Ruta.Size = new System.Drawing.Size(100, 20);
            this.txt_Ruta.TabIndex = 3;
            this.txt_Ruta.TextChanged += new System.EventHandler(this.txt_Ruta_TextChanged);
            this.txt_Ruta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Ruta_KeyDown);
            this.txt_Ruta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Ruta_KeyPress);
            // 
            // dgv_Direcciones
            // 
            this.dgv_Direcciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Direcciones.Enabled = false;
            this.dgv_Direcciones.Location = new System.Drawing.Point(25, 372);
            this.dgv_Direcciones.Name = "dgv_Direcciones";
            this.dgv_Direcciones.Size = new System.Drawing.Size(868, 163);
            this.dgv_Direcciones.TabIndex = 7;
            this.dgv_Direcciones.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgv_Direcciones_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(29, 356);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Direcciones ";
            // 
            // lbl_PRoductos
            // 
            this.lbl_PRoductos.AutoSize = true;
            this.lbl_PRoductos.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_PRoductos.Location = new System.Drawing.Point(29, 119);
            this.lbl_PRoductos.Name = "lbl_PRoductos";
            this.lbl_PRoductos.Size = new System.Drawing.Size(55, 13);
            this.lbl_PRoductos.TabIndex = 10;
            this.lbl_PRoductos.Text = "Productos";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Location = new System.Drawing.Point(25, 135);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(868, 191);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellLeave);
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick_1);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.Leave += new System.EventHandler(this.dataGridView1_Leave);
            // 
            // txt_fecha
            // 
            this.txt_fecha.Enabled = false;
            this.txt_fecha.Location = new System.Drawing.Point(350, 10);
            this.txt_fecha.Name = "txt_fecha";
            this.txt_fecha.Size = new System.Drawing.Size(132, 20);
            this.txt_fecha.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(295, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "   Fecha:";
            // 
            // txt_peso
            // 
            this.txt_peso.Location = new System.Drawing.Point(964, 3);
            this.txt_peso.Name = "txt_peso";
            this.txt_peso.Size = new System.Drawing.Size(100, 20);
            this.txt_peso.TabIndex = 13;
            this.txt_peso.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(918, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Peso:";
            this.label4.Visible = false;
            // 
            // btn_Editar
            // 
            this.btn_Editar.Enabled = false;
            this.btn_Editar.Location = new System.Drawing.Point(25, 79);
            this.btn_Editar.Name = "btn_Editar";
            this.btn_Editar.Size = new System.Drawing.Size(54, 23);
            this.btn_Editar.TabIndex = 17;
            this.btn_Editar.Text = "Editar";
            this.btn_Editar.UseVisualStyleBackColor = true;
            this.btn_Editar.Click += new System.EventHandler(this.btn_Editar_Click);
            // 
            // btn_guardar
            // 
            this.btn_guardar.Enabled = false;
            this.btn_guardar.Location = new System.Drawing.Point(85, 79);
            this.btn_guardar.Name = "btn_guardar";
            this.btn_guardar.Size = new System.Drawing.Size(54, 23);
            this.btn_guardar.TabIndex = 18;
            this.btn_guardar.Text = "Guardar";
            this.btn_guardar.UseVisualStyleBackColor = true;
            this.btn_guardar.Click += new System.EventHandler(this.btn_guardar_Click);
            // 
            // txt_estado
            // 
            this.txt_estado.Enabled = false;
            this.txt_estado.Location = new System.Drawing.Point(350, 43);
            this.txt_estado.Name = "txt_estado";
            this.txt_estado.Size = new System.Drawing.Size(132, 20);
            this.txt_estado.TabIndex = 19;
            this.txt_estado.TextChanged += new System.EventHandler(this.txt_estado_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(296, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Estatus: ";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.gbx_2);
            this.panel1.Controls.Add(this.gbx_1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.dgv_Direcciones);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.lbl_PRoductos);
            this.panel1.Controls.Add(this.btn_guardar);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btn_Editar);
            this.panel1.Controls.Add(this.txt_estado);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txt_fecha);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txt_Ruta);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txt_Jornada);
            this.panel1.Controls.Add(this.lbl_jornada);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(917, 547);
            this.panel1.TabIndex = 24;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(756, 29);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 27;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // gbx_2
            // 
            this.gbx_2.Controls.Add(this.button3);
            this.gbx_2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.gbx_2.Location = new System.Drawing.Point(299, 69);
            this.gbx_2.Name = "gbx_2";
            this.gbx_2.Size = new System.Drawing.Size(60, 35);
            this.gbx_2.TabIndex = 26;
            this.gbx_2.TabStop = false;
            this.gbx_2.Text = "Ruta";
            // 
            // button3
            // 
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.Location = new System.Drawing.Point(6, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(25, 23);
            this.button3.TabIndex = 22;
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // gbx_1
            // 
            this.gbx_1.Controls.Add(this.btn_imp);
            this.gbx_1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.gbx_1.Location = new System.Drawing.Point(145, 69);
            this.gbx_1.Name = "gbx_1";
            this.gbx_1.Size = new System.Drawing.Size(60, 35);
            this.gbx_1.TabIndex = 25;
            this.gbx_1.TabStop = false;
            this.gbx_1.Text = "PreRuta";
            // 
            // btn_imp
            // 
            this.btn_imp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_imp.BackgroundImage")));
            this.btn_imp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_imp.Enabled = false;
            this.btn_imp.Location = new System.Drawing.Point(6, 12);
            this.btn_imp.Name = "btn_imp";
            this.btn_imp.Size = new System.Drawing.Size(25, 23);
            this.btn_imp.TabIndex = 15;
            this.btn_imp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_imp.UseVisualStyleBackColor = true;
            this.btn_imp.Click += new System.EventHandler(this.button2_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(437, 80);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 21;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(240, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 25);
            this.button1.TabIndex = 4;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(917, 547);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_peso);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Text = "Confirmacion Ruta";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResizeEnd += new System.EventHandler(this.Form2_ResizeEnd);
            this.SizeChanged += new System.EventHandler(this.Form2_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Direcciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbx_2.ResumeLayout(false);
            this.gbx_1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_jornada;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Jornada;
        private System.Windows.Forms.TextBox txt_Ruta;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgv_Direcciones;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_PRoductos;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txt_fecha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_peso;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_imp;
        private System.Windows.Forms.Button btn_Editar;
        private System.Windows.Forms.Button btn_guardar;
        private System.Windows.Forms.TextBox txt_estado;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox gbx_2;
        private System.Windows.Forms.GroupBox gbx_1;
        private System.Windows.Forms.Button button4;
    }
}