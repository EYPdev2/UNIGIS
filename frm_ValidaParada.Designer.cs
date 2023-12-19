
namespace ActualizadorDoctosUnigis
{
    partial class frm_ValidaParada
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ValidaParada));
            this.txt_viaje = new System.Windows.Forms.TextBox();
            this.lbl_viaje = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_canc = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_viaje
            // 
            this.txt_viaje.Location = new System.Drawing.Point(55, 12);
            this.txt_viaje.Name = "txt_viaje";
            this.txt_viaje.Size = new System.Drawing.Size(100, 20);
            this.txt_viaje.TabIndex = 0;
            this.txt_viaje.TextChanged += new System.EventHandler(this.txt_viaje_TextChanged);
            this.txt_viaje.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_viaje_KeyDown);
            this.txt_viaje.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_viaje_KeyPress);
            // 
            // lbl_viaje
            // 
            this.lbl_viaje.AutoSize = true;
            this.lbl_viaje.Location = new System.Drawing.Point(12, 15);
            this.lbl_viaje.Name = "lbl_viaje";
            this.lbl_viaje.Size = new System.Drawing.Size(33, 13);
            this.lbl_viaje.TabIndex = 1;
            this.lbl_viaje.Text = "Viaje:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(15, 38);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Size = new System.Drawing.Size(554, 386);
            this.dataGridView1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(219, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Validar Paradas";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(790, 10);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(10, 10);
            this.textBox1.TabIndex = 4;
            this.textBox1.Visible = false;
            // 
            // btn_canc
            // 
            this.btn_canc.BackgroundImage = global::ActualizadorDoctosUnigis.Properties.Resources._1492790846_9cancel_84247;
            this.btn_canc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_canc.Enabled = false;
            this.btn_canc.Location = new System.Drawing.Point(168, 9);
            this.btn_canc.Name = "btn_canc";
            this.btn_canc.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_canc.Size = new System.Drawing.Size(39, 23);
            this.btn_canc.TabIndex = 5;
            this.btn_canc.UseVisualStyleBackColor = true;
            this.btn_canc.Click += new System.EventHandler(this.btn_canc_Click);
            // 
            // frm_ValidaParada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_canc);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lbl_viaje);
            this.Controls.Add(this.txt_viaje);
            this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_ValidaParada";
            this.Text = "Liberar Entregas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_ValidaParada_FormClosing);
            this.Load += new System.EventHandler(this.frm_ValidaParada_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_viaje;
        private System.Windows.Forms.Label lbl_viaje;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_canc;
    }
}