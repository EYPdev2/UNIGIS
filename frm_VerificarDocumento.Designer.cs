namespace ActualizadorDoctosUnigis
{
    partial class frm_VerificarDocumento
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl_Establecimiento = new System.Windows.Forms.Label();
            this.cmb_Establecimiento = new System.Windows.Forms.ComboBox();
            this.lbl_Folio = new System.Windows.Forms.Label();
            this.txt_Folio = new System.Windows.Forms.TextBox();
            this.lbl_Transaccion = new System.Windows.Forms.Label();
            this.cmb_Transaccion = new System.Windows.Forms.ComboBox();
            this.btn_Verificar = new System.Windows.Forms.Button();
            this.btn_Limpiar = new System.Windows.Forms.Button();
            this.lbl_Mercancia = new System.Windows.Forms.Label();
            this.dgv_Resultado = new System.Windows.Forms.DataGridView();
            this.col_Producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_CantidadPe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_CantidadEmb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Aparece = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Razon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Resultado)).BeginInit();
            this.SuspendLayout();
            //
            // lbl_Establecimiento
            //
            this.lbl_Establecimiento.AutoSize = true;
            this.lbl_Establecimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Establecimiento.ForeColor = System.Drawing.Color.White;
            this.lbl_Establecimiento.Location = new System.Drawing.Point(12, 16);
            this.lbl_Establecimiento.Name = "lbl_Establecimiento";
            this.lbl_Establecimiento.Size = new System.Drawing.Size(100, 13);
            this.lbl_Establecimiento.TabIndex = 0;
            this.lbl_Establecimiento.Text = "Establecimiento:";
            //
            // cmb_Establecimiento
            //
            this.cmb_Establecimiento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Establecimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Establecimiento.FormattingEnabled = true;
            this.cmb_Establecimiento.Location = new System.Drawing.Point(125, 12);
            this.cmb_Establecimiento.Name = "cmb_Establecimiento";
            this.cmb_Establecimiento.Size = new System.Drawing.Size(330, 21);
            this.cmb_Establecimiento.TabIndex = 1;
            //
            // lbl_Folio
            //
            this.lbl_Folio.AutoSize = true;
            this.lbl_Folio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Folio.ForeColor = System.Drawing.Color.White;
            this.lbl_Folio.Location = new System.Drawing.Point(12, 46);
            this.lbl_Folio.Name = "lbl_Folio";
            this.lbl_Folio.Size = new System.Drawing.Size(38, 13);
            this.lbl_Folio.TabIndex = 2;
            this.lbl_Folio.Text = "Folio:";
            //
            // txt_Folio
            //
            this.txt_Folio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_Folio.Font =new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Folio.Location = new System.Drawing.Point(125, 42);
            this.txt_Folio.MaxLength = 20;
            this.txt_Folio.Name = "txt_Folio";
            this.txt_Folio.Size = new System.Drawing.Size(330, 20);
            this.txt_Folio.TabIndex = 3;
            //
            // lbl_Transaccion
            //
            this.lbl_Transaccion.AutoSize = true;
            this.lbl_Transaccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Transaccion.ForeColor = System.Drawing.Color.White;
            this.lbl_Transaccion.Location = new System.Drawing.Point(12, 76);
            this.lbl_Transaccion.Name = "lbl_Transaccion";
            this.lbl_Transaccion.Size = new System.Drawing.Size(81, 13);
            this.lbl_Transaccion.TabIndex = 4;
            this.lbl_Transaccion.Text = "Transacción:";
            //
            // cmb_Transaccion
            //
            this.cmb_Transaccion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Transaccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Transaccion.FormattingEnabled = true;
            this.cmb_Transaccion.Location = new System.Drawing.Point(125, 72);
            this.cmb_Transaccion.Name = "cmb_Transaccion";
            this.cmb_Transaccion.Size = new System.Drawing.Size(330, 21);
            this.cmb_Transaccion.TabIndex = 5;
            //
            // btn_Verificar
            //
            this.btn_Verificar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_Verificar.Location = new System.Drawing.Point(471, 40);
            this.btn_Verificar.Name = "btn_Verificar";
            this.btn_Verificar.Size = new System.Drawing.Size(100, 25);
            this.btn_Verificar.TabIndex = 6;
            this.btn_Verificar.Text = "Verificar";
            this.btn_Verificar.UseVisualStyleBackColor = true;
            this.btn_Verificar.Click += new System.EventHandler(this.btn_Verificar_Click);
            //
            // btn_Limpiar
            //
            this.btn_Limpiar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_Limpiar.Location = new System.Drawing.Point(471, 70);
            this.btn_Limpiar.Name = "btn_Limpiar";
            this.btn_Limpiar.Size = new System.Drawing.Size(100, 25);
            this.btn_Limpiar.TabIndex = 7;
            this.btn_Limpiar.Text = "Limpiar";
            this.btn_Limpiar.UseVisualStyleBackColor = true;
            this.btn_Limpiar.Click += new System.EventHandler(this.btn_Limpiar_Click);
            //
            // lbl_Mercancia
            //
            this.lbl_Mercancia.AutoSize = true;
            this.lbl_Mercancia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Mercancia.ForeColor = System.Drawing.Color.White;
            this.lbl_Mercancia.Location = new System.Drawing.Point(12, 121);
            this.lbl_Mercancia.Name = "lbl_Mercancia";
            this.lbl_Mercancia.Size = new System.Drawing.Size(72, 13);
            this.lbl_Mercancia.TabIndex = 8;
            this.lbl_Mercancia.Text = "Mercancía:";
            //
            // dgv_Resultado
            //
            this.dgv_Resultado.AllowUserToAddRows = false;
            this.dgv_Resultado.AllowUserToDeleteRows = false;
            this.dgv_Resultado.AllowUserToResizeRows = false;
            this.dgv_Resultado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Resultado.AutoGenerateColumns = false;
            this.dgv_Resultado.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_Resultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Resultado.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_Producto,
            this.col_Nombre,
            this.col_Unidad,
            this.col_Cantidad,
            this.col_CantidadPe,
            this.col_CantidadEmb,
            this.col_Aparece,
            this.col_Razon});
            this.dgv_Resultado.Location = new System.Drawing.Point(12, 139);
            this.dgv_Resultado.MultiSelect = false;
            this.dgv_Resultado.Name = "dgv_Resultado";
            this.dgv_Resultado.ReadOnly = true;
            this.dgv_Resultado.RowHeadersVisible = false;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgv_Resultado.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Resultado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv_Resultado.Size = new System.Drawing.Size(976, 409);
            this.dgv_Resultado.TabIndex = 9;
            this.dgv_Resultado.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_Resultado_CellFormatting);
            //
            // col_Producto
            //
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.col_Producto.DataPropertyName = "producto";
            this.col_Producto.DefaultCellStyle = dataGridViewCellStyle2;
            this.col_Producto.HeaderText = "Cod Prod";
            this.col_Producto.Name = "col_Producto";
            this.col_Producto.ReadOnly = true;
            this.col_Producto.Width = 80;
            //
            // col_Nombre
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.col_Nombre.DataPropertyName = "nombre";
            this.col_Nombre.DefaultCellStyle = dataGridViewCellStyle6;
            this.col_Nombre.HeaderText = "Nombre";
            this.col_Nombre.Name = "col_Nombre";
            this.col_Nombre.ReadOnly = true;
            this.col_Nombre.Width = 260;
            // 
            // col_Unidad
            //
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.col_Unidad.DataPropertyName = "unidad";
            this.col_Unidad.DefaultCellStyle = dataGridViewCellStyle7;
            this.col_Unidad.HeaderText = "Unidad";
            this.col_Unidad.Name = "col_Unidad";
            this.col_Unidad.ReadOnly = true;
            this.col_Unidad.Width = 60;
            //
            // col_Cantidad
            //
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle3.Format = "0.####";
            this.col_Cantidad.DataPropertyName = "cantidad";
            this.col_Cantidad.DefaultCellStyle = dataGridViewCellStyle3;
            this.col_Cantidad.HeaderText = "Cant.";
            this.col_Cantidad.Name = "col_Cantidad";
            this.col_Cantidad.ReadOnly = true;
            this.col_Cantidad.Width = 80;
            //
            // col_CantidadPe
            //
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle8.Format = "0.####";
            this.col_CantidadPe.DataPropertyName = "cantidad_pe";
            this.col_CantidadPe.DefaultCellStyle = dataGridViewCellStyle8;
            this.col_CantidadPe.HeaderText = "Cant. PE.";
            this.col_CantidadPe.Name = "col_CantidadPe";
            this.col_CantidadPe.ReadOnly = true;
            this.col_CantidadPe.Width = 80;
            //
            // col_CantidadEmb
            //
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle9.Format = "0.####";
            this.col_CantidadEmb.DataPropertyName = "cantidad_emb";
            this.col_CantidadEmb.DefaultCellStyle = dataGridViewCellStyle9;
            this.col_CantidadEmb.HeaderText = "Cant. Emb.";
            this.col_CantidadEmb.Name = "col_CantidadEmb";
            this.col_CantidadEmb.ReadOnly = true;
            this.col_CantidadEmb.Width = 90;
            //
            // col_Aparece
            //
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.col_Aparece.DataPropertyName = "estado";
            this.col_Aparece.DefaultCellStyle = dataGridViewCellStyle4;
            this.col_Aparece.HeaderText = "Estado";
            this.col_Aparece.Name = "col_Aparece";
            this.col_Aparece.ReadOnly = true;
            this.col_Aparece.Width = 90;
            //
            // col_Razon
            //
            this.col_Razon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.col_Razon.DataPropertyName = "razon";
            this.col_Razon.DefaultCellStyle = dataGridViewCellStyle5;
            this.col_Razon.HeaderText = "Mensaje";
            this.col_Razon.Name = "col_Razon";
            this.col_Razon.ReadOnly = true;
            //
            // frm_VerificarDocumento
            //
            this.AcceptButton = this.btn_Verificar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1000, 560);
            this.Controls.Add(this.btn_Limpiar);
            this.Controls.Add(this.dgv_Resultado);
            this.Controls.Add(this.lbl_Mercancia);
            this.Controls.Add(this.btn_Verificar);
            this.Controls.Add(this.cmb_Transaccion);
            this.Controls.Add(this.lbl_Transaccion);
            this.Controls.Add(this.txt_Folio);
            this.Controls.Add(this.lbl_Folio);
            this.Controls.Add(this.cmb_Establecimiento);
            this.Controls.Add(this.lbl_Establecimiento);
            this.Name = "frm_VerificarDocumento";
            this.Text = "Verificar Documento";
            this.Load += new System.EventHandler(this.frm_VerificarDocumento_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Resultado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Establecimiento;
        private System.Windows.Forms.ComboBox cmb_Establecimiento;
        private System.Windows.Forms.Label lbl_Folio;
        private System.Windows.Forms.TextBox txt_Folio;
        private System.Windows.Forms.Label lbl_Transaccion;
        private System.Windows.Forms.ComboBox cmb_Transaccion;
        private System.Windows.Forms.Button btn_Verificar;
        private System.Windows.Forms.Button btn_Limpiar;
        private System.Windows.Forms.Label lbl_Mercancia;
        private System.Windows.Forms.DataGridView dgv_Resultado;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Unidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_CantidadPe;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_CantidadEmb;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Aparece;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Razon;
    }
}
