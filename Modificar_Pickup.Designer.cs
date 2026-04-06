using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ActualizadorDoctosUnigis
{
    partial class Modificar_Pickup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
 
        private TextBox txt_dcto;
        private Label label1;

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
            this.txt_dcto = new TextBox();
            this.label1 = new Label();
            this.SuspendLayout();
            this.txt_dcto.Location = new Point(95, 12);
            this.txt_dcto.Name = "txt_dcto";
            this.txt_dcto.Size = new Size(100, 20);
            this.txt_dcto.TabIndex = 0;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Documento";
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 450);
            this.Controls.Add((Control)this.label1);
            this.Controls.Add((Control)this.txt_dcto);
                     }

        #endregion
    }
}