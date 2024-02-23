namespace ActualizadorDoctosUnigis
{
    partial class Frm_Liquidacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Liquidacion));
            this.TextBox2 = new System.Windows.Forms.TextBox();
            this.Button1 = new System.Windows.Forms.Button();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TextBox2
            // 
            this.TextBox2.Enabled = false;
            this.TextBox2.Location = new System.Drawing.Point(22, 51);
            this.TextBox2.Multiline = true;
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new System.Drawing.Size(317, 71);
            this.TextBox2.TabIndex = 11;
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(211, 128);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(128, 23);
            this.Button1.TabIndex = 12;
            this.Button1.Text = "Cancelar Liquidacion";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(120, 21);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(128, 20);
            this.TextBox1.TabIndex = 10;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Label1.Location = new System.Drawing.Point(19, 24);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(86, 13);
            this.Label1.TabIndex = 9;
            this.Label1.Text = "Folio a Cancelar:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
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
            "32"});
            this.comboBox1.Location = new System.Drawing.Point(84, 128);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 13;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Cornsilk;
            this.label2.Location = new System.Drawing.Point(29, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "label2";
            // 
            // Frm_Liquidacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(361, 168);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.TextBox2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.Label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_Liquidacion";
            this.Text = "Frm_Liquidacion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox TextBox2;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
    }
}