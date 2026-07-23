
namespace ActualizadorDoctosUnigis
{
    partial class frm_SolicitudCambioRM
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnl_Focus = new System.Windows.Forms.Panel();
            this.chk_RM = new System.Windows.Forms.CheckBox();
            this.lbl_Solicita = new System.Windows.Forms.Label();
            this.cmb_Solicita = new System.Windows.Forms.ComboBox();
            this.lbl_Razon = new System.Windows.Forms.Label();
            this.cmb_Razon = new System.Windows.Forms.ComboBox();
            this.lbl_Notas = new System.Windows.Forms.Label();
            this.txt_Notas = new System.Windows.Forms.TextBox();
            this.lbl_ContadorNotas = new System.Windows.Forms.Label();
            this.btn_Cancelar = new System.Windows.Forms.Button();
            this.btn_Guardar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // pnl_Focus
            //
            this.pnl_Focus.Location = new System.Drawing.Point(0, 0);
            this.pnl_Focus.Name = "pnl_Focus";
            this.pnl_Focus.Size = new System.Drawing.Size(0, 0);
            this.pnl_Focus.TabIndex = 0;
            this.pnl_Focus.TabStop = true;
            //
            // chk_RM
            //
            this.chk_RM.AutoSize = true;
            this.chk_RM.ForeColor = System.Drawing.Color.White;
            this.chk_RM.Location = new System.Drawing.Point(20, 30);
            this.chk_RM.Name = "chk_RM";
            this.chk_RM.TabIndex = 1;
            this.chk_RM.Text = "Recoge Mercancía";
            this.chk_RM.UseVisualStyleBackColor = true;
            //
            // lbl_Solicita
            //
            this.lbl_Solicita.AutoSize = true;
            this.lbl_Solicita.ForeColor = System.Drawing.Color.White;
            this.lbl_Solicita.Location = new System.Drawing.Point(20, 67);
            this.lbl_Solicita.Name = "lbl_Solicita";
            this.lbl_Solicita.Size = new System.Drawing.Size(41, 13);
            this.lbl_Solicita.TabIndex = 4;
            this.lbl_Solicita.Text = "Solicita";
            //
            // cmb_Solicita
            //
            this.cmb_Solicita.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Solicita.FormattingEnabled = true;
            this.cmb_Solicita.Location = new System.Drawing.Point(20, 84);
            this.cmb_Solicita.Name = "cmb_Solicita";
            this.cmb_Solicita.Size = new System.Drawing.Size(350, 21);
            this.cmb_Solicita.TabIndex = 5;
            //
            // lbl_Razon
            //
            this.lbl_Razon.AutoSize = true;
            this.lbl_Razon.ForeColor = System.Drawing.Color.White;
            this.lbl_Razon.Location = new System.Drawing.Point(390, 67);
            this.lbl_Razon.Name = "lbl_Razon";
            this.lbl_Razon.Size = new System.Drawing.Size(38, 13);
            this.lbl_Razon.TabIndex = 6;
            this.lbl_Razon.Text = "Razón";
            //
            // cmb_Razon
            //
            this.cmb_Razon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Razon.FormattingEnabled = true;
            this.cmb_Razon.Location = new System.Drawing.Point(390, 84);
            this.cmb_Razon.Name = "cmb_Razon";
            this.cmb_Razon.Size = new System.Drawing.Size(350, 21);
            this.cmb_Razon.TabIndex = 7;
            //
            // lbl_Notas
            //
            this.lbl_Notas.AutoSize = true;
            this.lbl_Notas.ForeColor = System.Drawing.Color.White;
            this.lbl_Notas.Location = new System.Drawing.Point(20, 123);
            this.lbl_Notas.Name = "lbl_Notas";
            this.lbl_Notas.Size = new System.Drawing.Size(60, 13);
            this.lbl_Notas.TabIndex = 8;
            this.lbl_Notas.Text = "Comentario";
            //
            // txt_Notas
            //
            this.txt_Notas.Location = new System.Drawing.Point(20, 140);
            this.txt_Notas.MaxLength = 500;
            this.txt_Notas.Multiline = true;
            this.txt_Notas.Name = "txt_Notas";
            this.txt_Notas.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Notas.Size = new System.Drawing.Size(720, 130);
            this.txt_Notas.TabIndex = 9;
            this.txt_Notas.TextChanged += new System.EventHandler(this.txt_Notas_TextChanged);
            //
            // lbl_ContadorNotas
            //
            this.lbl_ContadorNotas.AutoSize = true;
            this.lbl_ContadorNotas.ForeColor = System.Drawing.Color.Silver;
            this.lbl_ContadorNotas.Location = new System.Drawing.Point(20, 274);
            this.lbl_ContadorNotas.Name = "lbl_ContadorNotas";
            this.lbl_ContadorNotas.Size = new System.Drawing.Size(42, 13);
            this.lbl_ContadorNotas.TabIndex = 12;
            this.lbl_ContadorNotas.Text = "0 / 500";
            //
            // btn_Cancelar
            //
            this.btn_Cancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btn_Cancelar.FlatAppearance.BorderSize = 0;
            this.btn_Cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancelar.ForeColor = System.Drawing.Color.White;
            this.btn_Cancelar.Location = new System.Drawing.Point(20, 334);
            this.btn_Cancelar.Name = "btn_Cancelar";
            this.btn_Cancelar.Size = new System.Drawing.Size(350, 42);
            this.btn_Cancelar.TabIndex = 10;
            this.btn_Cancelar.Text = "Cancelar";
            this.btn_Cancelar.UseVisualStyleBackColor = false;
            this.btn_Cancelar.Click += new System.EventHandler(this.btn_Cancelar_Click);
            //
            // btn_Guardar
            //
            this.btn_Guardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(80)))));
            this.btn_Guardar.FlatAppearance.BorderSize = 0;
            this.btn_Guardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Guardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Guardar.ForeColor = System.Drawing.Color.White;
            this.btn_Guardar.Location = new System.Drawing.Point(390, 334);
            this.btn_Guardar.Name = "btn_Guardar";
            this.btn_Guardar.Size = new System.Drawing.Size(350, 42);
            this.btn_Guardar.TabIndex = 11;
            this.btn_Guardar.Text = "Enviar Solicitud";
            this.btn_Guardar.UseVisualStyleBackColor = false;
            this.btn_Guardar.Click += new System.EventHandler(this.btn_Guardar_Click);
            //
            // frm_SolicitudCambioRM
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(760, 409);
            this.Controls.Add(this.pnl_Focus);
            this.Controls.Add(this.btn_Guardar);
            this.Controls.Add(this.btn_Cancelar);
            this.Controls.Add(this.lbl_ContadorNotas);
            this.Controls.Add(this.txt_Notas);
            this.Controls.Add(this.lbl_Notas);
            this.Controls.Add(this.cmb_Razon);
            this.Controls.Add(this.lbl_Razon);
            this.Controls.Add(this.cmb_Solicita);
            this.Controls.Add(this.lbl_Solicita);
            this.Controls.Add(this.chk_RM);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_SolicitudCambioRM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Solicitud de Cambio";
            this.Load += new System.EventHandler(this.frm_SolicitudCambioRM_Load);
            this.Shown += new System.EventHandler(this.frm_SolicitudCambioRM_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Focus;
        private System.Windows.Forms.Label lbl_RMActual;
        private System.Windows.Forms.CheckBox chk_RM;
        private System.Windows.Forms.Label lbl_Solicita;
        private System.Windows.Forms.ComboBox cmb_Solicita;
        private System.Windows.Forms.Label lbl_Razon;
        private System.Windows.Forms.ComboBox cmb_Razon;
        private System.Windows.Forms.Label lbl_Notas;
        private System.Windows.Forms.TextBox txt_Notas;
        private System.Windows.Forms.Label lbl_ContadorNotas;
        private System.Windows.Forms.Button btn_Guardar;
        private System.Windows.Forms.Button btn_Cancelar;
    }
}
