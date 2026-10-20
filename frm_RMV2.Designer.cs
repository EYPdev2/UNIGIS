
namespace ActualizadorDoctosUnigis
{
    partial class frm_RMV2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl_Titulo = new System.Windows.Forms.Label();
            this.lbl_Folio = new System.Windows.Forms.Label();
            this.lbl_tipoDocto = new System.Windows.Forms.Label();
            this.txt_folio = new System.Windows.Forms.TextBox();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.Label();
            this.cmb_tipoD = new System.Windows.Forms.ComboBox();
            this.lbl_Establecimiento = new System.Windows.Forms.Label();
            this.txtEstab = new System.Windows.Forms.TextBox();
            this.lbl_fechaDTO = new System.Windows.Forms.Label();
            this.txtFechaEmision = new System.Windows.Forms.TextBox();
            this.lbl_FE = new System.Windows.Forms.Label();
            this.txtFechaEntrega = new System.Windows.Forms.TextBox();
            this.lbl_RecogeMercancia = new System.Windows.Forms.Label();
            this.chk_RecogeMercancia = new System.Windows.Forms.CheckBox();
            this.lbl_Solicitudes = new System.Windows.Forms.Label();
            this.dgv_Solicitudes = new System.Windows.Forms.DataGridView();
            this.btn_SolicitarCambio = new System.Windows.Forms.Button();
            this.btn_Limpiar = new System.Windows.Forms.Button();
            this.btn_Actualizar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Solicitudes)).BeginInit();
            this.SuspendLayout();
            //
            // lbl_Titulo
            //
            this.lbl_Titulo.AutoSize = true;
            this.lbl_Titulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Titulo.ForeColor = System.Drawing.Color.White;
            this.lbl_Titulo.Location = new System.Drawing.Point(20, 12);
            this.lbl_Titulo.Name = "lbl_Titulo";
            this.lbl_Titulo.Size = new System.Drawing.Size(256, 22);
            this.lbl_Titulo.TabIndex = 32;
            this.lbl_Titulo.Text = "Modificar Recoge Mercancía";
            //
            // lbl_Folio
            //
            this.lbl_Folio.AutoSize = true;
            this.lbl_Folio.ForeColor = System.Drawing.Color.White;
            this.lbl_Folio.Location = new System.Drawing.Point(20, 50);
            this.lbl_Folio.Name = "lbl_Folio";
            this.lbl_Folio.Size = new System.Drawing.Size(102, 13);
            this.lbl_Folio.TabIndex = 3;
            this.lbl_Folio.Text = "Folio del documento";
            //
            // lbl_tipoDocto
            //
            this.lbl_tipoDocto.AutoSize = true;
            this.lbl_tipoDocto.ForeColor = System.Drawing.Color.White;
            this.lbl_tipoDocto.Location = new System.Drawing.Point(348, 50);
            this.lbl_tipoDocto.Name = "lbl_tipoDocto";
            this.lbl_tipoDocto.Size = new System.Drawing.Size(101, 13);
            this.lbl_tipoDocto.TabIndex = 7;
            this.lbl_tipoDocto.Text = "Tipo del documento";
            //
            // txt_folio
            //
            this.txt_folio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_folio.Location = new System.Drawing.Point(20, 66);
            this.txt_folio.Name = "txt_folio";
            this.txt_folio.Size = new System.Drawing.Size(318, 20);
            this.txt_folio.TabIndex = 8;
            this.txt_folio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_folio_KeyPress);
            //
            // lbl_Status
            //
            this.lbl_Status.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.lbl_Status.AutoSize = true;
            this.lbl_Status.ForeColor = System.Drawing.Color.White;
            this.lbl_Status.Location = new System.Drawing.Point(676, 50);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(40, 13);
            this.lbl_Status.TabIndex = 30;
            this.lbl_Status.Text = "Estado";
            //
            // txtStatus
            //
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.txtStatus.AutoSize = false;
            this.txtStatus.BackColor = System.Drawing.SystemColors.Control;
            this.txtStatus.Location = new System.Drawing.Point(676, 66);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(104, 20);
            this.txtStatus.TabIndex = 31;
            this.txtStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // cmb_tipoD
            //
            this.cmb_tipoD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_tipoD.FormattingEnabled = true;
            this.cmb_tipoD.Location = new System.Drawing.Point(348, 66);
            this.cmb_tipoD.Name = "cmb_tipoD";
            this.cmb_tipoD.Size = new System.Drawing.Size(318, 21);
            this.cmb_tipoD.TabIndex = 10;
            //
            // lbl_Establecimiento
            //
            this.lbl_Establecimiento.AutoSize = true;
            this.lbl_Establecimiento.ForeColor = System.Drawing.Color.White;
            this.lbl_Establecimiento.Location = new System.Drawing.Point(20, 100);
            this.lbl_Establecimiento.Name = "lbl_Establecimiento";
            this.lbl_Establecimiento.Size = new System.Drawing.Size(81, 13);
            this.lbl_Establecimiento.TabIndex = 4;
            this.lbl_Establecimiento.Text = "Establecimiento";
            //
            // txtEstab
            //
            this.txtEstab.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtEstab.Enabled = false;
            this.txtEstab.Location = new System.Drawing.Point(20, 116);
            this.txtEstab.Name = "txtEstab";
            this.txtEstab.ReadOnly = true;
            this.txtEstab.Size = new System.Drawing.Size(760, 20);
            this.txtEstab.TabIndex = 11;
            //
            // lbl_fechaDTO
            //
            this.lbl_fechaDTO.AutoSize = true;
            this.lbl_fechaDTO.ForeColor = System.Drawing.Color.White;
            this.lbl_fechaDTO.Location = new System.Drawing.Point(20, 152);
            this.lbl_fechaDTO.Name = "lbl_fechaDTO";
            this.lbl_fechaDTO.Size = new System.Drawing.Size(90, 13);
            this.lbl_fechaDTO.TabIndex = 5;
            this.lbl_fechaDTO.Text = "Fecha de emisión";
            //
            // txtFechaEmision
            //
            this.txtFechaEmision.Enabled = false;
            this.txtFechaEmision.Location = new System.Drawing.Point(20, 168);
            this.txtFechaEmision.Name = "txtFechaEmision";
            this.txtFechaEmision.ReadOnly = true;
            this.txtFechaEmision.Size = new System.Drawing.Size(370, 20);
            this.txtFechaEmision.TabIndex = 12;
            //
            // lbl_FE
            //
            this.lbl_FE.AutoSize = true;
            this.lbl_FE.ForeColor = System.Drawing.Color.White;
            this.lbl_FE.Location = new System.Drawing.Point(400, 152);
            this.lbl_FE.Name = "lbl_FE";
            this.lbl_FE.Size = new System.Drawing.Size(91, 13);
            this.lbl_FE.TabIndex = 6;
            this.lbl_FE.Text = "Fecha de entrega";
            //
            // txtFechaEntrega
            //
            this.txtFechaEntrega.Enabled = false;
            this.txtFechaEntrega.Location = new System.Drawing.Point(400, 168);
            this.txtFechaEntrega.Name = "txtFechaEntrega";
            this.txtFechaEntrega.ReadOnly = true;
            this.txtFechaEntrega.Size = new System.Drawing.Size(380, 20);
            this.txtFechaEntrega.TabIndex = 13;
            //
            // lbl_RecogeMercancia
            //
            this.lbl_RecogeMercancia.AutoSize = true;
            this.lbl_RecogeMercancia.ForeColor = System.Drawing.Color.White;
            this.lbl_RecogeMercancia.Location = new System.Drawing.Point(20, 200);
            this.lbl_RecogeMercancia.Name = "lbl_RecogeMercancia";
            this.lbl_RecogeMercancia.Size = new System.Drawing.Size(100, 13);
            this.lbl_RecogeMercancia.TabIndex = 33;
            this.lbl_RecogeMercancia.Text = "Recoge Mercancía";
            //
            // chk_RecogeMercancia
            //
            this.chk_RecogeMercancia.AutoCheck = false;
            this.chk_RecogeMercancia.AutoSize = true;
            this.chk_RecogeMercancia.Location = new System.Drawing.Point(22, 219);
            this.chk_RecogeMercancia.Name = "chk_RecogeMercancia";
            this.chk_RecogeMercancia.Size = new System.Drawing.Size(15, 14);
            this.chk_RecogeMercancia.Cursor = System.Windows.Forms.Cursors.Default;
            this.chk_RecogeMercancia.TabIndex = 34;
            this.chk_RecogeMercancia.TabStop = false;
            this.chk_RecogeMercancia.UseVisualStyleBackColor = true;
            //
            // lbl_Solicitudes
            //
            this.lbl_Solicitudes.AutoSize = true;
            this.lbl_Solicitudes.ForeColor = System.Drawing.Color.White;
            this.lbl_Solicitudes.Location = new System.Drawing.Point(20, 252);
            this.lbl_Solicitudes.Name = "lbl_Solicitudes";
            this.lbl_Solicitudes.Size = new System.Drawing.Size(110, 13);
            this.lbl_Solicitudes.TabIndex = 24;
            this.lbl_Solicitudes.Text = "Solicitudes de cambio";
            //
            // dgv_Solicitudes
            //
            this.dgv_Solicitudes.AllowUserToAddRows = false;
            this.dgv_Solicitudes.AllowUserToDeleteRows = false;
            this.dgv_Solicitudes.AllowUserToResizeColumns = false;
            this.dgv_Solicitudes.AllowUserToResizeRows = false;
            this.dgv_Solicitudes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Solicitudes.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.dgv_Solicitudes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Solicitudes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Solicitudes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Solicitudes.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_Solicitudes.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.dgv_Solicitudes.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgv_Solicitudes.Location = new System.Drawing.Point(20, 268);
            this.dgv_Solicitudes.Name = "dgv_Solicitudes";
            this.dgv_Solicitudes.ReadOnly = true;
            this.dgv_Solicitudes.RowHeadersVisible = false;
            this.dgv_Solicitudes.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_Solicitudes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgv_Solicitudes.Size = new System.Drawing.Size(760, 160);
            this.dgv_Solicitudes.TabIndex = 25;
            this.dgv_Solicitudes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Solicitudes_CellContentClick);
            this.dgv_Solicitudes.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_Solicitudes_CellFormatting);
            //
            // btn_SolicitarCambio
            //
            this.btn_SolicitarCambio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btn_SolicitarCambio.Enabled = false;
            this.btn_SolicitarCambio.FlatAppearance.BorderSize = 0;
            this.btn_SolicitarCambio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SolicitarCambio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SolicitarCambio.ForeColor = System.Drawing.Color.White;
            this.btn_SolicitarCambio.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.btn_SolicitarCambio.Location = new System.Drawing.Point(20, 440);
            this.btn_SolicitarCambio.Name = "btn_SolicitarCambio";
            this.btn_SolicitarCambio.Size = new System.Drawing.Size(760, 42);
            this.btn_SolicitarCambio.TabIndex = 26;
            this.btn_SolicitarCambio.Text = "Solicitar Cambio";
            this.btn_SolicitarCambio.UseVisualStyleBackColor = false;
            this.btn_SolicitarCambio.Click += new System.EventHandler(this.btn_SolicitarCambio_Click);
            //
            // btn_Limpiar
            //
            this.btn_Limpiar.BackgroundImage = global::ActualizadorDoctosUnigis.Properties.Resources.trash_solid;
            this.btn_Limpiar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Limpiar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.btn_Limpiar.Location = new System.Drawing.Point(749, 12);
            this.btn_Limpiar.Name = "btn_Limpiar";
            this.btn_Limpiar.Size = new System.Drawing.Size(31, 32);
            this.btn_Limpiar.TabIndex = 18;
            this.btn_Limpiar.UseVisualStyleBackColor = true;
            this.btn_Limpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            //
            // btn_Actualizar
            //
            this.btn_Actualizar.BackgroundImage = global::ActualizadorDoctosUnigis.Properties.Resources.actualizar;
            this.btn_Actualizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Actualizar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.btn_Actualizar.Location = new System.Drawing.Point(713, 12);
            this.btn_Actualizar.Name = "btn_Actualizar";
            this.btn_Actualizar.Size = new System.Drawing.Size(31, 32);
            this.btn_Actualizar.TabIndex = 0;
            this.btn_Actualizar.UseVisualStyleBackColor = true;
            this.btn_Actualizar.Click += new System.EventHandler(this.btn_Actualizar_Click);
            //
            // frm_RMV2
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(800, 492);
            this.MinimumSize = new System.Drawing.Size(818, 531);
            this.Controls.Add(this.btn_SolicitarCambio);
            this.Controls.Add(this.dgv_Solicitudes);
            this.Controls.Add(this.lbl_Solicitudes);
            this.Controls.Add(this.chk_RecogeMercancia);
            this.Controls.Add(this.lbl_RecogeMercancia);
            this.Controls.Add(this.txtFechaEntrega);
            this.Controls.Add(this.lbl_FE);
            this.Controls.Add(this.txtFechaEmision);
            this.Controls.Add(this.lbl_fechaDTO);
            this.Controls.Add(this.txtEstab);
            this.Controls.Add(this.lbl_Establecimiento);
            this.Controls.Add(this.lbl_Titulo);
            this.Controls.Add(this.btn_Limpiar);
            this.Controls.Add(this.cmb_tipoD);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.lbl_Status);
            this.Controls.Add(this.txt_folio);
            this.Controls.Add(this.lbl_tipoDocto);
            this.Controls.Add(this.lbl_Folio);
            this.Controls.Add(this.btn_Actualizar);
            this.Name = "frm_RMV2";
            this.Text = "Modificar Recoge Mercancía";
            this.Load += new System.EventHandler(this.frm_RM_Load);
            this.Resize += new System.EventHandler(this.frm_RM_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Solicitudes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Titulo;
        private System.Windows.Forms.Label lbl_Folio;
        private System.Windows.Forms.Label lbl_tipoDocto;
        private System.Windows.Forms.TextBox txt_folio;
        private System.Windows.Forms.Label lbl_Status;
        private System.Windows.Forms.Label txtStatus;
        private System.Windows.Forms.ComboBox cmb_tipoD;
        private System.Windows.Forms.Label lbl_Establecimiento;
        private System.Windows.Forms.TextBox txtEstab;
        private System.Windows.Forms.Label lbl_fechaDTO;
        private System.Windows.Forms.TextBox txtFechaEmision;
        private System.Windows.Forms.Label lbl_FE;
        private System.Windows.Forms.TextBox txtFechaEntrega;
        private System.Windows.Forms.Label lbl_RecogeMercancia;
        private System.Windows.Forms.CheckBox chk_RecogeMercancia;
        private System.Windows.Forms.Label lbl_Solicitudes;
        private System.Windows.Forms.DataGridView dgv_Solicitudes;
        private System.Windows.Forms.Button btn_SolicitarCambio;
        private System.Windows.Forms.Button btn_Limpiar;
        private System.Windows.Forms.Button btn_Actualizar;
    }
}
