
namespace ActualizadorDoctosUnigis
{
    partial class frm_FechaEntrega
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_FechaEntrega));
            this.lbl_fechaEntrega = new System.Windows.Forms.Label();
            this.lbl_Folio = new System.Windows.Forms.Label();
            this.lbl_Establecimiento = new System.Windows.Forms.Label();
            this.lbl_fechaDTO = new System.Windows.Forms.Label();
            this.lbl_FE = new System.Windows.Forms.Label();
            this.lbl_tipoDocto = new System.Windows.Forms.Label();
            this.txt_folio = new System.Windows.Forms.TextBox();
            this.cmb_tipoD = new System.Windows.Forms.ComboBox();
            this.txt_estab = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.Solicita = new System.Windows.Forms.ComboBox();
            this.Razon = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Limpiar = new System.Windows.Forms.Button();
            this.btn_Guardar = new System.Windows.Forms.Button();
            this.btn_Actualizar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_fechaEntrega
            // 
            this.lbl_fechaEntrega.AutoSize = true;
            this.lbl_fechaEntrega.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_fechaEntrega.ForeColor = System.Drawing.Color.White;
            this.lbl_fechaEntrega.Location = new System.Drawing.Point(155, 13);
            this.lbl_fechaEntrega.Name = "lbl_fechaEntrega";
            this.lbl_fechaEntrega.Size = new System.Drawing.Size(293, 25);
            this.lbl_fechaEntrega.TabIndex = 2;
            this.lbl_fechaEntrega.Text = "Fecha de Entrega Factura ";
            // 
            // lbl_Folio
            // 
            this.lbl_Folio.AutoSize = true;
            this.lbl_Folio.ForeColor = System.Drawing.Color.White;
            this.lbl_Folio.Location = new System.Drawing.Point(20, 86);
            this.lbl_Folio.Name = "lbl_Folio";
            this.lbl_Folio.Size = new System.Drawing.Size(29, 13);
            this.lbl_Folio.TabIndex = 3;
            this.lbl_Folio.Text = "Folio";
            // 
            // lbl_Establecimiento
            // 
            this.lbl_Establecimiento.AutoSize = true;
            this.lbl_Establecimiento.ForeColor = System.Drawing.Color.White;
            this.lbl_Establecimiento.Location = new System.Drawing.Point(23, 125);
            this.lbl_Establecimiento.Name = "lbl_Establecimiento";
            this.lbl_Establecimiento.Size = new System.Drawing.Size(81, 13);
            this.lbl_Establecimiento.TabIndex = 4;
            this.lbl_Establecimiento.Text = "Establecimiento";
            // 
            // lbl_fechaDTO
            // 
            this.lbl_fechaDTO.AutoSize = true;
            this.lbl_fechaDTO.ForeColor = System.Drawing.Color.White;
            this.lbl_fechaDTO.Location = new System.Drawing.Point(23, 222);
            this.lbl_fechaDTO.Name = "lbl_fechaDTO";
            this.lbl_fechaDTO.Size = new System.Drawing.Size(60, 13);
            this.lbl_fechaDTO.TabIndex = 5;
            this.lbl_fechaDTO.Text = "Fecha Dto.";
            // 
            // lbl_FE
            // 
            this.lbl_FE.AutoSize = true;
            this.lbl_FE.ForeColor = System.Drawing.Color.White;
            this.lbl_FE.Location = new System.Drawing.Point(324, 226);
            this.lbl_FE.Name = "lbl_FE";
            this.lbl_FE.Size = new System.Drawing.Size(77, 13);
            this.lbl_FE.TabIndex = 6;
            this.lbl_FE.Text = "Fecha Entrega";
            // 
            // lbl_tipoDocto
            // 
            this.lbl_tipoDocto.AutoSize = true;
            this.lbl_tipoDocto.ForeColor = System.Drawing.Color.White;
            this.lbl_tipoDocto.Location = new System.Drawing.Point(206, 87);
            this.lbl_tipoDocto.Name = "lbl_tipoDocto";
            this.lbl_tipoDocto.Size = new System.Drawing.Size(86, 13);
            this.lbl_tipoDocto.TabIndex = 7;
            this.lbl_tipoDocto.Text = "Tipo Documento";
            // 
            // txt_folio
            // 
            this.txt_folio.Location = new System.Drawing.Point(55, 82);
            this.txt_folio.Name = "txt_folio";
            this.txt_folio.Size = new System.Drawing.Size(100, 20);
            this.txt_folio.TabIndex = 8;
            // 
            // cmb_tipoD
            // 
            this.cmb_tipoD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_tipoD.FormattingEnabled = true;
            this.cmb_tipoD.Location = new System.Drawing.Point(298, 82);
            this.cmb_tipoD.Name = "cmb_tipoD";
            this.cmb_tipoD.Size = new System.Drawing.Size(146, 21);
            this.cmb_tipoD.TabIndex = 10;
            this.cmb_tipoD.SelectedIndexChanged += new System.EventHandler(this.cmb_tipoD_SelectedIndexChanged);
            // 
            // txt_estab
            // 
            this.txt_estab.Location = new System.Drawing.Point(110, 122);
            this.txt_estab.Name = "txt_estab";
            this.txt_estab.ReadOnly = true;
            this.txt_estab.Size = new System.Drawing.Size(271, 20);
            this.txt_estab.TabIndex = 11;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Location = new System.Drawing.Point(87, 222);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(202, 20);
            this.dateTimePicker1.TabIndex = 12;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CalendarForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(407, 222);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(178, 20);
            this.dateTimePicker2.TabIndex = 13;
            // 
            // Solicita
            // 
            this.Solicita.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Solicita.FormattingEnabled = true;
            this.Solicita.Location = new System.Drawing.Point(89, 179);
            this.Solicita.Name = "Solicita";
            this.Solicita.Size = new System.Drawing.Size(200, 21);
            this.Solicita.TabIndex = 14;
            // 
            // Razon
            // 
            this.Razon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Razon.FormattingEnabled = true;
            this.Razon.Location = new System.Drawing.Point(376, 179);
            this.Razon.Name = "Razon";
            this.Razon.Size = new System.Drawing.Size(209, 21);
            this.Razon.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(324, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Razon";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(23, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Solicita";
            // 
            // btn_Limpiar
            // 
            this.btn_Limpiar.BackgroundImage = global::ActualizadorDoctosUnigis.Properties.Resources.trash_solid;
            this.btn_Limpiar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Limpiar.Enabled = false;
            this.btn_Limpiar.Location = new System.Drawing.Point(87, 12);
            this.btn_Limpiar.Name = "btn_Limpiar";
            this.btn_Limpiar.Size = new System.Drawing.Size(31, 32);
            this.btn_Limpiar.TabIndex = 18;
            this.btn_Limpiar.UseVisualStyleBackColor = true;
            this.btn_Limpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btn_Guardar
            // 
            this.btn_Guardar.BackgroundImage = global::ActualizadorDoctosUnigis.Properties.Resources.guardar_el_archivo;
            this.btn_Guardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Guardar.Enabled = false;
            this.btn_Guardar.Location = new System.Drawing.Point(49, 12);
            this.btn_Guardar.Name = "btn_Guardar";
            this.btn_Guardar.Size = new System.Drawing.Size(31, 32);
            this.btn_Guardar.TabIndex = 1;
            this.btn_Guardar.UseVisualStyleBackColor = true;
            this.btn_Guardar.Click += new System.EventHandler(this.btn_Guardar_Click);
            // 
            // btn_Actualizar
            // 
            this.btn_Actualizar.BackgroundImage = global::ActualizadorDoctosUnigis.Properties.Resources.actualizar;
            this.btn_Actualizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Actualizar.Location = new System.Drawing.Point(12, 12);
            this.btn_Actualizar.Name = "btn_Actualizar";
            this.btn_Actualizar.Size = new System.Drawing.Size(31, 32);
            this.btn_Actualizar.TabIndex = 0;
            this.btn_Actualizar.UseVisualStyleBackColor = true;
            this.btn_Actualizar.Click += new System.EventHandler(this.btn_Actualizar_Click);
            // 
            // frm_FechaEntrega
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(613, 370);
            this.Controls.Add(this.btn_Limpiar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Razon);
            this.Controls.Add(this.Solicita);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.txt_estab);
            this.Controls.Add(this.cmb_tipoD);
            this.Controls.Add(this.txt_folio);
            this.Controls.Add(this.lbl_tipoDocto);
            this.Controls.Add(this.lbl_FE);
            this.Controls.Add(this.lbl_fechaDTO);
            this.Controls.Add(this.lbl_Establecimiento);
            this.Controls.Add(this.lbl_Folio);
            this.Controls.Add(this.lbl_fechaEntrega);
            this.Controls.Add(this.btn_Guardar);
            this.Controls.Add(this.btn_Actualizar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_FechaEntrega";
            this.Text = "Modificacion Fecha Entrega";
            this.Load += new System.EventHandler(this.frm_FechaEntrega_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Actualizar;
        private System.Windows.Forms.Button btn_Guardar;
        private System.Windows.Forms.Label lbl_fechaEntrega;
        private System.Windows.Forms.Label lbl_Folio;
        private System.Windows.Forms.Label lbl_Establecimiento;
        private System.Windows.Forms.Label lbl_fechaDTO;
        private System.Windows.Forms.Label lbl_FE;
        private System.Windows.Forms.Label lbl_tipoDocto;
        private System.Windows.Forms.TextBox txt_folio;
        private System.Windows.Forms.ComboBox cmb_tipoD;
        private System.Windows.Forms.TextBox txt_estab;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.ComboBox Solicita;
        private System.Windows.Forms.ComboBox Razon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Limpiar;
    }
}