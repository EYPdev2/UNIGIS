
namespace ActualizadorDoctosUnigis
{
    partial class Form7
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form7));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_horasmin = new System.Windows.Forms.Label();
            this.lbl_horasmax = new System.Windows.Forms.Label();
            this.txt_horasmin = new System.Windows.Forms.TextBox();
            this.txt_horasmax = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.lbl_horasmin);
            this.splitContainer1.Panel2.Controls.Add(this.lbl_horasmax);
            this.splitContainer1.Panel2.Controls.Add(this.txt_horasmin);
            this.splitContainer1.Panel2.Controls.Add(this.txt_horasmax);
            this.splitContainer1.Size = new System.Drawing.Size(416, 732);
            this.splitContainer1.SplitterDistance = 649;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(416, 649);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::ActualizadorDoctosUnigis.Properties.Resources.OIP;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Location = new System.Drawing.Point(344, 23);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(63, 37);
            this.button2.TabIndex = 14;
            this.button2.Text = " ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::ActualizadorDoctosUnigis.Properties.Resources.R;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(271, 23);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 37);
            this.button1.TabIndex = 13;
            this.button1.Text = " ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbl_horasmin
            // 
            this.lbl_horasmin.AutoSize = true;
            this.lbl_horasmin.Location = new System.Drawing.Point(16, 33);
            this.lbl_horasmin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_horasmin.Name = "lbl_horasmin";
            this.lbl_horasmin.Size = new System.Drawing.Size(72, 17);
            this.lbl_horasmin.TabIndex = 9;
            this.lbl_horasmin.Text = "Horas Min";
            this.lbl_horasmin.Visible = false;
            // 
            // lbl_horasmax
            // 
            this.lbl_horasmax.AutoSize = true;
            this.lbl_horasmax.Location = new System.Drawing.Point(129, 34);
            this.lbl_horasmax.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_horasmax.Name = "lbl_horasmax";
            this.lbl_horasmax.Size = new System.Drawing.Size(79, 17);
            this.lbl_horasmax.TabIndex = 10;
            this.lbl_horasmax.Text = "Horas Max:";
            this.lbl_horasmax.Visible = false;
            // 
            // txt_horasmin
            // 
            this.txt_horasmin.Location = new System.Drawing.Point(97, 30);
            this.txt_horasmin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_horasmin.Name = "txt_horasmin";
            this.txt_horasmin.Size = new System.Drawing.Size(24, 22);
            this.txt_horasmin.TabIndex = 11;
            this.txt_horasmin.Text = "24";
            this.txt_horasmin.Visible = false;
            // 
            // txt_horasmax
            // 
            this.txt_horasmax.Location = new System.Drawing.Point(219, 31);
            this.txt_horasmax.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_horasmax.Name = "txt_horasmax";
            this.txt_horasmax.Size = new System.Drawing.Size(39, 22);
            this.txt_horasmax.TabIndex = 12;
            this.txt_horasmax.Text = "240";
            this.txt_horasmax.Visible = false;
            // 
            // Form7
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(115F, 115F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(416, 732);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form7";
            this.Text = "Vales";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form7_FormClosing);
            this.Load += new System.EventHandler(this.Form7_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl_horasmin;
        private System.Windows.Forms.Label lbl_horasmax;
        private System.Windows.Forms.TextBox txt_horasmin;
        private System.Windows.Forms.TextBox txt_horasmax;
        private System.Windows.Forms.Button button2;
    }
}