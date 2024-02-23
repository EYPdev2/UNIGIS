namespace ActualizadorDoctosUnigis
{
    partial class Frm_Cancelar
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Cancelar));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Enviar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Enviar = new System.Windows.Forms.Button();
            this.ConsultarDoctos = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.bnt_Refrescar = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cmb_letra = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
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
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Enviar});
            this.dataGridView1.Location = new System.Drawing.Point(12, 27);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.Size = new System.Drawing.Size(1226, 347);
            this.dataGridView1.TabIndex = 13;
            // 
            // Enviar
            // 
            this.Enviar.HeaderText = "Enviar";
            this.Enviar.Name = "Enviar";
            this.Enviar.Width = 43;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(457, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(388, 21);
            this.comboBox1.TabIndex = 17;
            this.comboBox1.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(311, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Establecimento:";
            // 
            // btn_Enviar
            // 
            this.btn_Enviar.Enabled = false;
            this.btn_Enviar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_Enviar.Location = new System.Drawing.Point(931, 2);
            this.btn_Enviar.Name = "btn_Enviar";
            this.btn_Enviar.Size = new System.Drawing.Size(83, 23);
            this.btn_Enviar.TabIndex = 20;
            this.btn_Enviar.Text = "Enviar";
            this.btn_Enviar.UseVisualStyleBackColor = true;
            this.btn_Enviar.Click += new System.EventHandler(this.btn_Enviar_Click);
            // 
            // ConsultarDoctos
            // 
            this.ConsultarDoctos.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ConsultarDoctos_DoWork);
            this.ConsultarDoctos.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ConsultarDoctos_RunWorkerCompleted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(134, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Refrescar Documentos";
            // 
            // bnt_Refrescar
            // 
            this.bnt_Refrescar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bnt_Refrescar.BackgroundImage")));
            this.bnt_Refrescar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bnt_Refrescar.Enabled = false;
            this.bnt_Refrescar.Location = new System.Drawing.Point(68, 2);
            this.bnt_Refrescar.Name = "bnt_Refrescar";
            this.bnt_Refrescar.Size = new System.Drawing.Size(41, 19);
            this.bnt_Refrescar.TabIndex = 21;
            this.bnt_Refrescar.UseVisualStyleBackColor = true;
            this.bnt_Refrescar.Click += new System.EventHandler(this.bnt_Refrescar_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::ActualizadorDoctosUnigis.Properties.Resources.CHK;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(851, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(36, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = " ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            this.cmb_letra.Location = new System.Drawing.Point(1066, 0);
            this.cmb_letra.Name = "cmb_letra";
            this.cmb_letra.Size = new System.Drawing.Size(121, 21);
            this.cmb_letra.TabIndex = 24;
            this.cmb_letra.SelectedIndexChanged += new System.EventHandler(this.cmb_letra_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.BackgroundImage = global::ActualizadorDoctosUnigis.Properties.Resources.font_adjustment;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(1021, -4);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(38, 29);
            this.button3.TabIndex = 23;
            this.button3.Text = " ";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // Frm_Cancelar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1328, 437);
            this.Controls.Add(this.cmb_letra);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bnt_Refrescar);
            this.Controls.Add(this.btn_Enviar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dataGridView1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "Frm_Cancelar";
            this.Text = "Frm_Cancelar";
            this.Load += new System.EventHandler(this.Frm_Cancelar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Enviar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Enviar;
        private System.ComponentModel.BackgroundWorker ConsultarDoctos;
        private System.Windows.Forms.Button bnt_Refrescar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_letra;
        private System.Windows.Forms.Button button3;
    }
}