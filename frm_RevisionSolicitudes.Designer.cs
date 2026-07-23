namespace ActualizadorDoctosUnigis
{
    partial class frm_RevisionSolicitudes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl_Titulo          = new System.Windows.Forms.Label();
            this.lbl_Folio           = new System.Windows.Forms.Label();
            this.txt_Folio           = new System.Windows.Forms.TextBox();
            this.lbl_TipoDoc         = new System.Windows.Forms.Label();
            this.cmb_TipoDoc         = new System.Windows.Forms.ComboBox();
            this.lbl_Tipo            = new System.Windows.Forms.Label();
            this.cmb_Tipo            = new System.Windows.Forms.ComboBox();
            this.lbl_Estado          = new System.Windows.Forms.Label();
            this.cmb_Estado          = new System.Windows.Forms.ComboBox();
            this.btn_Actualizar      = new System.Windows.Forms.Button();
            this.btn_Limpiar         = new System.Windows.Forms.Button();
            this.dgv_Solicitudes     = new System.Windows.Forms.DataGridView();
            this.pnl_Paginacion      = new System.Windows.Forms.Panel();
            this.btn_PaginaAnterior  = new System.Windows.Forms.Button();
            this.lbl_Pagina          = new System.Windows.Forms.Label();
            this.btn_PaginaSiguiente = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Solicitudes)).BeginInit();
            this.pnl_Paginacion.SuspendLayout();
            this.SuspendLayout();

            // lbl_Titulo
            this.lbl_Titulo.AutoSize  = true;
            this.lbl_Titulo.Font      = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Titulo.ForeColor = System.Drawing.Color.White;
            this.lbl_Titulo.Location  = new System.Drawing.Point(12, 12);
            this.lbl_Titulo.Name      = "lbl_Titulo";
            this.lbl_Titulo.TabIndex  = 0;
            this.lbl_Titulo.Text      = "Revisión de Solicitudes";

            // lbl_Folio
            this.lbl_Folio.AutoSize  = true;
            this.lbl_Folio.ForeColor = System.Drawing.Color.White;
            this.lbl_Folio.Location  = new System.Drawing.Point(12, 51);
            this.lbl_Folio.Name      = "lbl_Folio";
            this.lbl_Folio.TabIndex  = 1;
            this.lbl_Folio.Text      = "Folio del documento:";

            // txt_Folio
            this.txt_Folio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_Folio.Location        = new System.Drawing.Point(12, 67);
            this.txt_Folio.Name            = "txt_Folio";
            this.txt_Folio.Size            = new System.Drawing.Size(140, 20);
            this.txt_Folio.TabIndex        = 2;
            this.txt_Folio.KeyPress       += new System.Windows.Forms.KeyPressEventHandler(this.txt_Folio_KeyPress);

            // lbl_TipoDoc
            this.lbl_TipoDoc.AutoSize  = true;
            this.lbl_TipoDoc.ForeColor = System.Drawing.Color.White;
            this.lbl_TipoDoc.Location  = new System.Drawing.Point(162, 51);
            this.lbl_TipoDoc.Name      = "lbl_TipoDoc";
            this.lbl_TipoDoc.TabIndex  = 3;
            this.lbl_TipoDoc.Text      = "Tipo de documento:";

            // cmb_TipoDoc
            this.cmb_TipoDoc.DropDownStyle     = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_TipoDoc.FormattingEnabled = true;
            this.cmb_TipoDoc.Location          = new System.Drawing.Point(162, 67);
            this.cmb_TipoDoc.Name              = "cmb_TipoDoc";
            this.cmb_TipoDoc.Size              = new System.Drawing.Size(170, 21);
            this.cmb_TipoDoc.TabIndex          = 4;

            // lbl_Tipo
            this.lbl_Tipo.AutoSize  = true;
            this.lbl_Tipo.ForeColor = System.Drawing.Color.White;
            this.lbl_Tipo.Location  = new System.Drawing.Point(342, 51);
            this.lbl_Tipo.Name      = "lbl_Tipo";
            this.lbl_Tipo.TabIndex  = 5;
            this.lbl_Tipo.Text      = "Modificación:";

            // cmb_Tipo
            this.cmb_Tipo.DropDownStyle     = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Tipo.FormattingEnabled = true;
            this.cmb_Tipo.Location          = new System.Drawing.Point(342, 67);
            this.cmb_Tipo.Name              = "cmb_Tipo";
            this.cmb_Tipo.Size              = new System.Drawing.Size(150, 21);
            this.cmb_Tipo.TabIndex          = 6;

            // lbl_Estado
            this.lbl_Estado.AutoSize  = true;
            this.lbl_Estado.ForeColor = System.Drawing.Color.White;
            this.lbl_Estado.Location  = new System.Drawing.Point(502, 51);
            this.lbl_Estado.Name      = "lbl_Estado";
            this.lbl_Estado.TabIndex  = 7;
            this.lbl_Estado.Text      = "Estado:";

            // cmb_Estado
            this.cmb_Estado.DropDownStyle     = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Estado.FormattingEnabled = true;
            this.cmb_Estado.Location          = new System.Drawing.Point(502, 67);
            this.cmb_Estado.Name              = "cmb_Estado";
            this.cmb_Estado.Size              = new System.Drawing.Size(130, 21);
            this.cmb_Estado.TabIndex          = 8;

            // btn_Actualizar
            this.btn_Actualizar.BackColor                 = System.Drawing.Color.FromArgb(80, 80, 80);
            this.btn_Actualizar.FlatAppearance.BorderSize = 0;
            this.btn_Actualizar.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Actualizar.ForeColor                 = System.Drawing.Color.White;
            this.btn_Actualizar.Location                  = new System.Drawing.Point(662, 63);
            this.btn_Actualizar.Name                      = "btn_Actualizar";
            this.btn_Actualizar.Size                      = new System.Drawing.Size(90, 26);
            this.btn_Actualizar.TabIndex                  = 9;
            this.btn_Actualizar.Text                      = "Actualizar";
            this.btn_Actualizar.UseVisualStyleBackColor   = false;
            this.btn_Actualizar.Click                    += new System.EventHandler(this.btn_Actualizar_Click);

            // btn_Limpiar
            this.btn_Limpiar.BackColor                 = System.Drawing.Color.FromArgb(80, 80, 80);
            this.btn_Limpiar.FlatAppearance.BorderSize = 0;
            this.btn_Limpiar.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Limpiar.ForeColor                 = System.Drawing.Color.White;
            this.btn_Limpiar.Location                  = new System.Drawing.Point(762, 63);
            this.btn_Limpiar.Name                      = "btn_Limpiar";
            this.btn_Limpiar.Size                      = new System.Drawing.Size(90, 26);
            this.btn_Limpiar.TabIndex                  = 10;
            this.btn_Limpiar.Text                      = "Limpiar";
            this.btn_Limpiar.UseVisualStyleBackColor   = false;
            this.btn_Limpiar.Click                    += new System.EventHandler(this.btn_Limpiar_Click);

            // dgv_Solicitudes
            this.dgv_Solicitudes.AllowUserToAddRows              = false;
            this.dgv_Solicitudes.AllowUserToDeleteRows           = false;
            this.dgv_Solicitudes.AllowUserToResizeColumns        = false;
            this.dgv_Solicitudes.AllowUserToResizeRows           = false;
            this.dgv_Solicitudes.BackgroundColor                 = System.Drawing.Color.FromArgb(45, 45, 45);
            this.dgv_Solicitudes.BorderStyle                     = System.Windows.Forms.BorderStyle.None;
            this.dgv_Solicitudes.ColumnHeadersHeightSizeMode     = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_Solicitudes.RowHeadersWidthSizeMode         = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_Solicitudes.GridColor                       = System.Drawing.Color.FromArgb(90, 90, 90);

            dataGridViewCellStyle1.Alignment          = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor          = System.Drawing.Color.FromArgb(45, 45, 45);
            dataGridViewCellStyle1.Font               = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor          = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode           = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Solicitudes.ColumnHeadersDefaultCellStyle   = dataGridViewCellStyle1;

            dataGridViewCellStyle2.Alignment          = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor          = System.Drawing.Color.FromArgb(64, 64, 64);
            dataGridViewCellStyle2.Font               = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor          = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(90, 90, 90);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode           = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Solicitudes.DefaultCellStyle     = dataGridViewCellStyle2;

            this.dgv_Solicitudes.Anchor           = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Solicitudes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Solicitudes.Location         = new System.Drawing.Point(12, 99);
            this.dgv_Solicitudes.MultiSelect      = false;
            this.dgv_Solicitudes.Name             = "dgv_Solicitudes";
            this.dgv_Solicitudes.ReadOnly         = true;
            this.dgv_Solicitudes.RowHeadersVisible = false;
            this.dgv_Solicitudes.Size             = new System.Drawing.Size(936, 431);
            this.dgv_Solicitudes.TabIndex         = 11;
            this.dgv_Solicitudes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Solicitudes_CellContentClick);
            this.dgv_Solicitudes.CellFormatting   += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_Solicitudes_CellFormatting);

            // pnl_Paginacion
            this.pnl_Paginacion.Anchor     = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_Paginacion.BackColor  = System.Drawing.Color.FromArgb(64, 64, 64);
            this.pnl_Paginacion.Controls.Add(this.btn_PaginaAnterior);
            this.pnl_Paginacion.Controls.Add(this.lbl_Pagina);
            this.pnl_Paginacion.Controls.Add(this.btn_PaginaSiguiente);
            this.pnl_Paginacion.Location   = new System.Drawing.Point(12, 530);
            this.pnl_Paginacion.Name       = "pnl_Paginacion";
            this.pnl_Paginacion.Size       = new System.Drawing.Size(936, 34);
            this.pnl_Paginacion.TabIndex   = 12;
            this.pnl_Paginacion.Resize    += new System.EventHandler(this.pnl_Paginacion_Resize);

            // btn_PaginaAnterior
            this.btn_PaginaAnterior.BackColor                 = System.Drawing.Color.FromArgb(80, 80, 80);
            this.btn_PaginaAnterior.Enabled                   = false;
            this.btn_PaginaAnterior.FlatAppearance.BorderSize = 0;
            this.btn_PaginaAnterior.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PaginaAnterior.ForeColor                 = System.Drawing.Color.White;
            this.btn_PaginaAnterior.Location                  = new System.Drawing.Point(295, 5);
            this.btn_PaginaAnterior.Name                      = "btn_PaginaAnterior";
            this.btn_PaginaAnterior.Size                      = new System.Drawing.Size(85, 24);
            this.btn_PaginaAnterior.TabIndex                  = 0;
            this.btn_PaginaAnterior.Text                      = "Anterior";
            this.btn_PaginaAnterior.UseVisualStyleBackColor   = false;
            this.btn_PaginaAnterior.Click                    += new System.EventHandler(this.btn_PaginaAnterior_Click);

            // lbl_Pagina
            this.lbl_Pagina.ForeColor  = System.Drawing.Color.White;
            this.lbl_Pagina.Location   = new System.Drawing.Point(385, 9);
            this.lbl_Pagina.Name       = "lbl_Pagina";
            this.lbl_Pagina.Size       = new System.Drawing.Size(190, 16);
            this.lbl_Pagina.TabIndex   = 1;
            this.lbl_Pagina.Text       = "Página 1 de 1   •   0 registros";
            this.lbl_Pagina.TextAlign  = System.Drawing.ContentAlignment.MiddleCenter;

            // btn_PaginaSiguiente
            this.btn_PaginaSiguiente.BackColor                 = System.Drawing.Color.FromArgb(80, 80, 80);
            this.btn_PaginaSiguiente.Enabled                   = false;
            this.btn_PaginaSiguiente.FlatAppearance.BorderSize = 0;
            this.btn_PaginaSiguiente.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PaginaSiguiente.ForeColor                 = System.Drawing.Color.White;
            this.btn_PaginaSiguiente.Location                  = new System.Drawing.Point(580, 5);
            this.btn_PaginaSiguiente.Name                      = "btn_PaginaSiguiente";
            this.btn_PaginaSiguiente.Size                      = new System.Drawing.Size(85, 24);
            this.btn_PaginaSiguiente.TabIndex                  = 2;
            this.btn_PaginaSiguiente.Text                      = "Siguiente";
            this.btn_PaginaSiguiente.UseVisualStyleBackColor   = false;
            this.btn_PaginaSiguiente.Click                    += new System.EventHandler(this.btn_PaginaSiguiente_Click);

            // frm_RevisionSolicitudes
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.FromArgb(64, 64, 64);
            this.ClientSize          = new System.Drawing.Size(960, 564);
            this.Controls.Add(this.dgv_Solicitudes);
            this.Controls.Add(this.pnl_Paginacion);
            this.Controls.Add(this.btn_Limpiar);
            this.Controls.Add(this.btn_Actualizar);
            this.Controls.Add(this.cmb_Estado);
            this.Controls.Add(this.lbl_Estado);
            this.Controls.Add(this.cmb_Tipo);
            this.Controls.Add(this.lbl_Tipo);
            this.Controls.Add(this.cmb_TipoDoc);
            this.Controls.Add(this.lbl_TipoDoc);
            this.Controls.Add(this.txt_Folio);
            this.Controls.Add(this.lbl_Folio);
            this.Controls.Add(this.lbl_Titulo);
            this.MinimumSize = new System.Drawing.Size(880, 480);
            this.Name        = "frm_RevisionSolicitudes";
            this.Text        = "Revisión de solicitudes";
            this.Load       += new System.EventHandler(this.frm_RevisionSolicitudes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Solicitudes)).EndInit();
            this.pnl_Paginacion.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label    lbl_Titulo;
        private System.Windows.Forms.Label    lbl_Folio;
        private System.Windows.Forms.TextBox  txt_Folio;
        private System.Windows.Forms.Label    lbl_TipoDoc;
        private System.Windows.Forms.ComboBox cmb_TipoDoc;
        private System.Windows.Forms.Label    lbl_Tipo;
        private System.Windows.Forms.ComboBox cmb_Tipo;
        private System.Windows.Forms.Label    lbl_Estado;
        private System.Windows.Forms.ComboBox cmb_Estado;
        private System.Windows.Forms.Button   btn_Actualizar;
        private System.Windows.Forms.Button   btn_Limpiar;
        private System.Windows.Forms.DataGridView dgv_Solicitudes;
        private System.Windows.Forms.Panel    pnl_Paginacion;
        private System.Windows.Forms.Button   btn_PaginaAnterior;
        private System.Windows.Forms.Label    lbl_Pagina;
        private System.Windows.Forms.Button   btn_PaginaSiguiente;
    }
}
