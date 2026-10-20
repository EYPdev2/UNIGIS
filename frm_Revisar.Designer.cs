namespace ActualizadorDoctosUnigis
{
    partial class frm_Revisar
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // Layout constants:
            // C1=20, CW=350, C2=390, FW=720
            // LG=18 (label height), TH=20 (textbox height), RG=20 (row gap), RS=58 (row step)
            // Row 0: y=20  | Row 1: y=78  | Row 2: y=136 | Row 3: y=194
            // label y = rowY, control y = rowY + LG(18)
            // btnY = 194 + 18 + 110 + 20 = 342

            this.lbl_Documento   = new System.Windows.Forms.Label();
            this.txt_Documento   = new System.Windows.Forms.TextBox();
            this.lbl_Estado      = new System.Windows.Forms.Label();
            this.lbl_EstadoValor = new System.Windows.Forms.Label();
            this.lbl_ValorActual = new System.Windows.Forms.Label();
            this.txt_ValorActual = new System.Windows.Forms.TextBox();
            this.lbl_ValorNuevo  = new System.Windows.Forms.Label();
            this.txt_ValorNuevo  = new System.Windows.Forms.TextBox();
            this.lbl_Solicita    = new System.Windows.Forms.Label();
            this.txt_Solicita    = new System.Windows.Forms.TextBox();
            this.lbl_Razon       = new System.Windows.Forms.Label();
            this.txt_Razon       = new System.Windows.Forms.TextBox();
            this.lbl_Comentario  = new System.Windows.Forms.Label();
            this.txt_Comentario  = new System.Windows.Forms.TextBox();
            this.btn_Rechazar    = new System.Windows.Forms.Button();
            this.btn_Autorizar   = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // --- Row 0 (y=20): Documento | Estado ---

            this.lbl_Documento.AutoSize  = true;
            this.lbl_Documento.ForeColor = System.Drawing.Color.White;
            this.lbl_Documento.Location  = new System.Drawing.Point(20, 20);
            this.lbl_Documento.Name      = "lbl_Documento";
            this.lbl_Documento.TabIndex  = 0;
            this.lbl_Documento.Text      = "Documento";

            this.txt_Documento.Location  = new System.Drawing.Point(20, 38);
            this.txt_Documento.Name      = "txt_Documento";
            this.txt_Documento.ReadOnly  = true;
            this.txt_Documento.Size      = new System.Drawing.Size(350, 20);
            this.txt_Documento.TabIndex  = 1;
            this.txt_Documento.TabStop   = false;

            this.lbl_Estado.AutoSize  = true;
            this.lbl_Estado.ForeColor = System.Drawing.Color.White;
            this.lbl_Estado.Location  = new System.Drawing.Point(390, 20);
            this.lbl_Estado.Name      = "lbl_Estado";
            this.lbl_Estado.TabIndex  = 2;
            this.lbl_Estado.Text      = "Estado";

            this.lbl_EstadoValor.AutoSize    = false;
            this.lbl_EstadoValor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbl_EstadoValor.Location    = new System.Drawing.Point(390, 38);
            this.lbl_EstadoValor.Name        = "lbl_EstadoValor";
            this.lbl_EstadoValor.Size        = new System.Drawing.Size(350, 20);
            this.lbl_EstadoValor.TabIndex    = 3;
            this.lbl_EstadoValor.TextAlign   = System.Drawing.ContentAlignment.MiddleLeft;

            // --- Row 1 (y=78): ValorActual | ValorNuevo ---

            this.lbl_ValorActual.AutoSize  = true;
            this.lbl_ValorActual.ForeColor = System.Drawing.Color.White;
            this.lbl_ValorActual.Location  = new System.Drawing.Point(20, 78);
            this.lbl_ValorActual.Name      = "lbl_ValorActual";
            this.lbl_ValorActual.TabIndex  = 4;
            this.lbl_ValorActual.Text      = "Valor Actual";

            this.txt_ValorActual.Location  = new System.Drawing.Point(20, 96);
            this.txt_ValorActual.Name      = "txt_ValorActual";
            this.txt_ValorActual.ReadOnly  = true;
            this.txt_ValorActual.Size      = new System.Drawing.Size(350, 20);
            this.txt_ValorActual.TabIndex  = 5;
            this.txt_ValorActual.TabStop   = false;

            this.lbl_ValorNuevo.AutoSize  = true;
            this.lbl_ValorNuevo.ForeColor = System.Drawing.Color.White;
            this.lbl_ValorNuevo.Location  = new System.Drawing.Point(390, 78);
            this.lbl_ValorNuevo.Name      = "lbl_ValorNuevo";
            this.lbl_ValorNuevo.TabIndex  = 6;
            this.lbl_ValorNuevo.Text      = "Valor Nuevo";

            this.txt_ValorNuevo.Location  = new System.Drawing.Point(390, 96);
            this.txt_ValorNuevo.Name      = "txt_ValorNuevo";
            this.txt_ValorNuevo.ReadOnly  = true;
            this.txt_ValorNuevo.Size      = new System.Drawing.Size(350, 20);
            this.txt_ValorNuevo.TabIndex  = 7;
            this.txt_ValorNuevo.TabStop   = false;

            // --- Row 2 (y=136): Solicita | Razon ---

            this.lbl_Solicita.AutoSize  = true;
            this.lbl_Solicita.ForeColor = System.Drawing.Color.White;
            this.lbl_Solicita.Location  = new System.Drawing.Point(20, 136);
            this.lbl_Solicita.Name      = "lbl_Solicita";
            this.lbl_Solicita.TabIndex  = 8;
            this.lbl_Solicita.Text      = "Solicita";

            this.txt_Solicita.Location  = new System.Drawing.Point(20, 154);
            this.txt_Solicita.Name      = "txt_Solicita";
            this.txt_Solicita.ReadOnly  = true;
            this.txt_Solicita.Size      = new System.Drawing.Size(350, 20);
            this.txt_Solicita.TabIndex  = 9;
            this.txt_Solicita.TabStop   = false;

            this.lbl_Razon.AutoSize  = true;
            this.lbl_Razon.ForeColor = System.Drawing.Color.White;
            this.lbl_Razon.Location  = new System.Drawing.Point(390, 136);
            this.lbl_Razon.Name      = "lbl_Razon";
            this.lbl_Razon.TabIndex  = 10;
            this.lbl_Razon.Text      = "Razón";

            this.txt_Razon.Location  = new System.Drawing.Point(390, 154);
            this.txt_Razon.Name      = "txt_Razon";
            this.txt_Razon.ReadOnly  = true;
            this.txt_Razon.Size      = new System.Drawing.Size(350, 20);
            this.txt_Razon.TabIndex  = 11;
            this.txt_Razon.TabStop   = false;

            // --- Row 3 (y=194): Comentario (ancho completo) ---

            this.lbl_Comentario.AutoSize  = true;
            this.lbl_Comentario.ForeColor = System.Drawing.Color.White;
            this.lbl_Comentario.Location  = new System.Drawing.Point(20, 194);
            this.lbl_Comentario.Name      = "lbl_Comentario";
            this.lbl_Comentario.TabIndex  = 12;
            this.lbl_Comentario.Text      = "Comentario";

            this.txt_Comentario.Location    = new System.Drawing.Point(20, 212);
            this.txt_Comentario.Multiline   = true;
            this.txt_Comentario.Name        = "txt_Comentario";
            this.txt_Comentario.ReadOnly    = true;
            this.txt_Comentario.ScrollBars  = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Comentario.Size        = new System.Drawing.Size(720, 110);
            this.txt_Comentario.TabIndex    = 13;
            this.txt_Comentario.TabStop     = false;

            // --- Botones (btnY=342) ---

            this.btn_Rechazar.BackColor                    = System.Drawing.Color.FromArgb(180, 70, 70);
            this.btn_Rechazar.FlatAppearance.BorderSize    = 0;
            this.btn_Rechazar.FlatStyle                    = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Rechazar.Font                         = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btn_Rechazar.ForeColor                    = System.Drawing.Color.White;
            this.btn_Rechazar.Location                     = new System.Drawing.Point(20, 342);
            this.btn_Rechazar.Name                         = "btn_Rechazar";
            this.btn_Rechazar.Size                         = new System.Drawing.Size(350, 42);
            this.btn_Rechazar.TabIndex                     = 14;
            this.btn_Rechazar.Text                         = "Rechazar";
            this.btn_Rechazar.UseVisualStyleBackColor      = false;
            this.btn_Rechazar.Click                       += new System.EventHandler(this.btn_Rechazar_Click);

            this.btn_Autorizar.BackColor                   = System.Drawing.Color.FromArgb(0, 120, 80);
            this.btn_Autorizar.FlatAppearance.BorderSize   = 0;
            this.btn_Autorizar.FlatStyle                   = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Autorizar.Font                        = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btn_Autorizar.ForeColor                   = System.Drawing.Color.White;
            this.btn_Autorizar.Location                    = new System.Drawing.Point(390, 342);
            this.btn_Autorizar.Name                        = "btn_Autorizar";
            this.btn_Autorizar.Size                        = new System.Drawing.Size(350, 42);
            this.btn_Autorizar.TabIndex                    = 15;
            this.btn_Autorizar.Text                        = "Autorizar";
            this.btn_Autorizar.UseVisualStyleBackColor     = false;
            this.btn_Autorizar.Click                      += new System.EventHandler(this.btn_Autorizar_Click);

            // --- Form ---

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.FromArgb(64, 64, 64);
            this.ClientSize          = new System.Drawing.Size(760, 404);
            this.FormBorderStyle     = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox         = false;
            this.MinimizeBox         = false;
            this.Name                = "frm_Revisar";
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text                = "Revisión de Solicitud";

            this.Controls.Add(this.lbl_Documento);
            this.Controls.Add(this.txt_Documento);
            this.Controls.Add(this.lbl_Estado);
            this.Controls.Add(this.lbl_EstadoValor);
            this.Controls.Add(this.lbl_ValorActual);
            this.Controls.Add(this.txt_ValorActual);
            this.Controls.Add(this.lbl_ValorNuevo);
            this.Controls.Add(this.txt_ValorNuevo);
            this.Controls.Add(this.lbl_Solicita);
            this.Controls.Add(this.txt_Solicita);
            this.Controls.Add(this.lbl_Razon);
            this.Controls.Add(this.txt_Razon);
            this.Controls.Add(this.lbl_Comentario);
            this.Controls.Add(this.txt_Comentario);
            this.Controls.Add(this.btn_Rechazar);
            this.Controls.Add(this.btn_Autorizar);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label   lbl_Documento,  lbl_Estado,     lbl_EstadoValor;
        private System.Windows.Forms.Label   lbl_ValorActual, lbl_ValorNuevo;
        private System.Windows.Forms.Label   lbl_Solicita,   lbl_Razon,      lbl_Comentario;
        private System.Windows.Forms.TextBox txt_Documento;
        private System.Windows.Forms.TextBox txt_ValorActual, txt_ValorNuevo;
        private System.Windows.Forms.TextBox txt_Solicita,   txt_Razon,      txt_Comentario;
        private System.Windows.Forms.Button  btn_Autorizar,  btn_Rechazar;
    }
}
