
namespace ActualizadorDoctosUnigis
{
    partial class frm_PickupsDocumentos
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl_Mensaje = new System.Windows.Forms.Label();
            this.dgv_Pickups = new System.Windows.Forms.DataGridView();
            this.lbl_Seleccion = new System.Windows.Forms.Label();
            this.btn_Enviar = new System.Windows.Forms.Button();
            this.btn_Cancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Pickups)).BeginInit();
            this.SuspendLayout();
            //
            // lbl_Mensaje
            //
            this.lbl_Mensaje.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Mensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbl_Mensaje.ForeColor = System.Drawing.Color.White;
            this.lbl_Mensaje.Location = new System.Drawing.Point(12, 12);
            this.lbl_Mensaje.Name = "lbl_Mensaje";
            this.lbl_Mensaje.Size = new System.Drawing.Size(1076, 56);
            this.lbl_Mensaje.TabIndex = 0;
            this.lbl_Mensaje.Text = "Los siguientes PP se han seleccionado automáticamente para su envío, ya que únicamente seleccionaste su factura " +
    "o remisión de origen. Si alguno de ellos todavía no debe enviarse, desmárcalo antes de continuar.";
            //
            // dgv_Pickups
            //
            this.dgv_Pickups.AllowUserToAddRows = false;
            this.dgv_Pickups.AllowUserToDeleteRows = false;
            this.dgv_Pickups.AllowUserToResizeRows = false;
            this.dgv_Pickups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Pickups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Pickups.Location = new System.Drawing.Point(12, 76);
            this.dgv_Pickups.MultiSelect = false;
            this.dgv_Pickups.Name = "dgv_Pickups";
            this.dgv_Pickups.RowHeadersVisible = false;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgv_Pickups.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Pickups.Size = new System.Drawing.Size(1076, 370);
            this.dgv_Pickups.TabIndex = 1;
            this.dgv_Pickups.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Pickups_CellValueChanged);
            this.dgv_Pickups.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgv_Pickups_CurrentCellDirtyStateChanged);
            //
            // lbl_Seleccion
            //
            this.lbl_Seleccion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_Seleccion.AutoSize = true;
            this.lbl_Seleccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbl_Seleccion.ForeColor = System.Drawing.Color.White;
            this.lbl_Seleccion.Location = new System.Drawing.Point(12, 463);
            this.lbl_Seleccion.Name = "lbl_Seleccion";
            this.lbl_Seleccion.Size = new System.Drawing.Size(150, 17);
            this.lbl_Seleccion.TabIndex = 2;
            this.lbl_Seleccion.Text = "0 de 0 PP se enviarán.";
            //
            // btn_Enviar
            //
            this.btn_Enviar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Enviar.BackColor = System.Drawing.Color.FromArgb(0, 120, 80);
            this.btn_Enviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Enviar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btn_Enviar.ForeColor = System.Drawing.Color.White;
            this.btn_Enviar.Location = new System.Drawing.Point(858, 456);
            this.btn_Enviar.Name = "btn_Enviar";
            this.btn_Enviar.Size = new System.Drawing.Size(112, 32);
            this.btn_Enviar.TabIndex = 3;
            this.btn_Enviar.Text = "Enviar";
            this.btn_Enviar.UseVisualStyleBackColor = false;
            this.btn_Enviar.Click += new System.EventHandler(this.btn_Enviar_Click);
            //
            // btn_Cancelar
            //
            this.btn_Cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancelar.BackColor = System.Drawing.Color.FromArgb(180, 70, 70);
            this.btn_Cancelar.CausesValidation = false;
            this.btn_Cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btn_Cancelar.ForeColor = System.Drawing.Color.White;
            this.btn_Cancelar.Location = new System.Drawing.Point(976, 456);
            this.btn_Cancelar.Name = "btn_Cancelar";
            this.btn_Cancelar.Size = new System.Drawing.Size(112, 32);
            this.btn_Cancelar.TabIndex = 4;
            this.btn_Cancelar.Text = "Cancelar";
            this.btn_Cancelar.UseVisualStyleBackColor = false;
            this.btn_Cancelar.Click += new System.EventHandler(this.btn_Cancelar_Click);
            //
            // frm_PickupsDocumentos
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.CancelButton = this.btn_Cancelar;
            this.ClientSize = new System.Drawing.Size(1100, 500);
            this.Controls.Add(this.btn_Cancelar);
            this.Controls.Add(this.btn_Enviar);
            this.Controls.Add(this.lbl_Seleccion);
            this.Controls.Add(this.dgv_Pickups);
            this.Controls.Add(this.lbl_Mensaje);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(700, 300);
            this.Name = "frm_PickupsDocumentos";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Selección automática";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Pickups)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Mensaje;
        private System.Windows.Forms.DataGridView dgv_Pickups;
        private System.Windows.Forms.Label lbl_Seleccion;
        private System.Windows.Forms.Button btn_Enviar;
        private System.Windows.Forms.Button btn_Cancelar;
    }
}
