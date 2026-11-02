
namespace ActualizadorDoctosUnigis
{
    partial class frm_ProductosDocumento
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
            this.lbl_Documento = new System.Windows.Forms.Label();
            this.dgv_Productos = new System.Windows.Forms.DataGridView();
            this.lbl_Seleccion = new System.Windows.Forms.Label();
            this.btn_Confirmar = new System.Windows.Forms.Button();
            this.btn_Cancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Productos)).BeginInit();
            this.SuspendLayout();
            //
            // lbl_Documento
            //
            this.lbl_Documento.AutoSize = true;
            this.lbl_Documento.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lbl_Documento.ForeColor = System.Drawing.Color.White;
            this.lbl_Documento.Location = new System.Drawing.Point(12, 12);
            this.lbl_Documento.Name = "lbl_Documento";
            this.lbl_Documento.Size = new System.Drawing.Size(92, 18);
            this.lbl_Documento.TabIndex = 0;
            this.lbl_Documento.Text = "Documento";
            //
            // dgv_Productos
            //
            this.dgv_Productos.AllowUserToAddRows = false;
            this.dgv_Productos.AllowUserToDeleteRows = false;
            this.dgv_Productos.AllowUserToResizeRows = false;
            this.dgv_Productos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Productos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Productos.Location = new System.Drawing.Point(12, 42);
            this.dgv_Productos.MultiSelect = false;
            this.dgv_Productos.Name = "dgv_Productos";
            this.dgv_Productos.RowHeadersVisible = false;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgv_Productos.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Productos.Size = new System.Drawing.Size(796, 380);
            this.dgv_Productos.TabIndex = 1;
            this.dgv_Productos.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgv_Productos_CellBeginEdit);
            this.dgv_Productos.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Productos_CellEndEdit);
            this.dgv_Productos.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv_Productos_CellValidating);
            this.dgv_Productos.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Productos_CellValueChanged);
            this.dgv_Productos.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgv_Productos_CurrentCellDirtyStateChanged);
            this.dgv_Productos.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_Productos_EditingControlShowing);
            //
            // lbl_Seleccion
            //
            this.lbl_Seleccion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_Seleccion.AutoSize = true;
            this.lbl_Seleccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbl_Seleccion.ForeColor = System.Drawing.Color.White;
            this.lbl_Seleccion.Location = new System.Drawing.Point(12, 439);
            this.lbl_Seleccion.Name = "lbl_Seleccion";
            this.lbl_Seleccion.Size = new System.Drawing.Size(160, 17);
            this.lbl_Seleccion.TabIndex = 2;
            this.lbl_Seleccion.Text = "0 de 0 productos marcados";
            //
            // btn_Confirmar
            //
            this.btn_Confirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Confirmar.BackColor = System.Drawing.Color.FromArgb(0, 120, 80);
            this.btn_Confirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Confirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btn_Confirmar.ForeColor = System.Drawing.Color.White;
            this.btn_Confirmar.Location = new System.Drawing.Point(578, 432);
            this.btn_Confirmar.Name = "btn_Confirmar";
            this.btn_Confirmar.Size = new System.Drawing.Size(112, 32);
            this.btn_Confirmar.TabIndex = 3;
            this.btn_Confirmar.Text = "Confirmar";
            this.btn_Confirmar.UseVisualStyleBackColor = false;
            this.btn_Confirmar.Click += new System.EventHandler(this.btn_Confirmar_Click);
            //
            // btn_Cancelar
            //
            this.btn_Cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancelar.BackColor = System.Drawing.Color.FromArgb(180, 70, 70);
            this.btn_Cancelar.CausesValidation = false;
            this.btn_Cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btn_Cancelar.ForeColor = System.Drawing.Color.White;
            this.btn_Cancelar.Location = new System.Drawing.Point(696, 432);
            this.btn_Cancelar.Name = "btn_Cancelar";
            this.btn_Cancelar.Size = new System.Drawing.Size(112, 32);
            this.btn_Cancelar.TabIndex = 4;
            this.btn_Cancelar.Text = "Cancelar";
            this.btn_Cancelar.UseVisualStyleBackColor = false;
            this.btn_Cancelar.Click += new System.EventHandler(this.btn_Cancelar_Click);
            //
            // frm_ProductosDocumento
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.CancelButton = this.btn_Cancelar;
            this.ClientSize = new System.Drawing.Size(820, 476);
            this.Controls.Add(this.btn_Cancelar);
            this.Controls.Add(this.btn_Confirmar);
            this.Controls.Add(this.lbl_Seleccion);
            this.Controls.Add(this.dgv_Productos);
            this.Controls.Add(this.lbl_Documento);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 300);
            this.Name = "frm_ProductosDocumento";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Productos del documento";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Productos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Documento;
        private System.Windows.Forms.DataGridView dgv_Productos;
        private System.Windows.Forms.Label lbl_Seleccion;
        private System.Windows.Forms.Button btn_Confirmar;
        private System.Windows.Forms.Button btn_Cancelar;
    }
}
