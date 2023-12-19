
namespace ActualizadorDoctosUnigis
{
    partial class frm_RM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_RM));
            this.btn_Guardar = new System.Windows.Forms.Button();
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
            this.btn_Actualizar = new System.Windows.Forms.Button();
            this.chk_rm = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_RM = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
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
            // lbl_fechaEntrega
            // 
            this.lbl_fechaEntrega.AutoSize = true;
            this.lbl_fechaEntrega.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_fechaEntrega.ForeColor = System.Drawing.Color.White;
            this.lbl_fechaEntrega.Location = new System.Drawing.Point(105, 13);
            this.lbl_fechaEntrega.Name = "lbl_fechaEntrega";
            this.lbl_fechaEntrega.Size = new System.Drawing.Size(302, 25);
            this.lbl_fechaEntrega.TabIndex = 2;
            this.lbl_fechaEntrega.Text = "Recoge Mercancia Factura ";
            this.lbl_fechaEntrega.Click += new System.EventHandler(this.lbl_fechaEntrega_Click);
            // 
            // lbl_Folio
            // 
            this.lbl_Folio.AutoSize = true;
            this.lbl_Folio.ForeColor = System.Drawing.Color.White;
            this.lbl_Folio.Location = new System.Drawing.Point(14, 86);
            this.lbl_Folio.Name = "lbl_Folio";
            this.lbl_Folio.Size = new System.Drawing.Size(29, 13);
            this.lbl_Folio.TabIndex = 3;
            this.lbl_Folio.Text = "Folio";
            this.lbl_Folio.Click += new System.EventHandler(this.lbl_Folio_Click);
            // 
            // lbl_Establecimiento
            // 
            this.lbl_Establecimiento.AutoSize = true;
            this.lbl_Establecimiento.ForeColor = System.Drawing.Color.White;
            this.lbl_Establecimiento.Location = new System.Drawing.Point(10, 124);
            this.lbl_Establecimiento.Name = "lbl_Establecimiento";
            this.lbl_Establecimiento.Size = new System.Drawing.Size(81, 13);
            this.lbl_Establecimiento.TabIndex = 4;
            this.lbl_Establecimiento.Text = "Establecimiento";
            // 
            // lbl_fechaDTO
            // 
            this.lbl_fechaDTO.AutoSize = true;
            this.lbl_fechaDTO.ForeColor = System.Drawing.Color.White;
            this.lbl_fechaDTO.Location = new System.Drawing.Point(10, 163);
            this.lbl_fechaDTO.Name = "lbl_fechaDTO";
            this.lbl_fechaDTO.Size = new System.Drawing.Size(60, 13);
            this.lbl_fechaDTO.TabIndex = 5;
            this.lbl_fechaDTO.Text = "Fecha Dto.";
            // 
            // lbl_FE
            // 
            this.lbl_FE.AutoSize = true;
            this.lbl_FE.ForeColor = System.Drawing.Color.White;
            this.lbl_FE.Location = new System.Drawing.Point(228, 163);
            this.lbl_FE.Name = "lbl_FE";
            this.lbl_FE.Size = new System.Drawing.Size(77, 13);
            this.lbl_FE.TabIndex = 6;
            this.lbl_FE.Text = "Fecha Entrega";
            this.lbl_FE.Click += new System.EventHandler(this.lbl_FE_Click);
            // 
            // lbl_tipoDocto
            // 
            this.lbl_tipoDocto.AutoSize = true;
            this.lbl_tipoDocto.ForeColor = System.Drawing.Color.White;
            this.lbl_tipoDocto.Location = new System.Drawing.Point(193, 86);
            this.lbl_tipoDocto.Name = "lbl_tipoDocto";
            this.lbl_tipoDocto.Size = new System.Drawing.Size(86, 13);
            this.lbl_tipoDocto.TabIndex = 7;
            this.lbl_tipoDocto.Text = "Tipo Documento";
            // 
            // txt_folio
            // 
            this.txt_folio.Location = new System.Drawing.Point(49, 82);
            this.txt_folio.Name = "txt_folio";
            this.txt_folio.Size = new System.Drawing.Size(100, 20);
            this.txt_folio.TabIndex = 8;
            this.txt_folio.TextChanged += new System.EventHandler(this.txt_folio_TextChanged);
            // 
            // cmb_tipoD
            // 
            this.cmb_tipoD.FormattingEnabled = true;
            this.cmb_tipoD.Location = new System.Drawing.Point(285, 82);
            this.cmb_tipoD.Name = "cmb_tipoD";
            this.cmb_tipoD.Size = new System.Drawing.Size(146, 21);
            this.cmb_tipoD.TabIndex = 10;
            this.cmb_tipoD.SelectedIndexChanged += new System.EventHandler(this.cmb_tipoD_SelectedIndexChanged);
            // 
            // txt_estab
            // 
            this.txt_estab.Enabled = false;
            this.txt_estab.Location = new System.Drawing.Point(97, 121);
            this.txt_estab.Name = "txt_estab";
            this.txt_estab.Size = new System.Drawing.Size(271, 20);
            this.txt_estab.TabIndex = 11;
            this.txt_estab.TextChanged += new System.EventHandler(this.txt_estab_TextChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Location = new System.Drawing.Point(76, 163);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(147, 20);
            this.dateTimePicker1.TabIndex = 12;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Enabled = false;
            this.dateTimePicker2.Location = new System.Drawing.Point(311, 163);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(147, 20);
            this.dateTimePicker2.TabIndex = 13;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
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
            // chk_rm
            // 
            this.chk_rm.AutoSize = true;
            this.chk_rm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_rm.ForeColor = System.Drawing.Color.White;
            this.chk_rm.Location = new System.Drawing.Point(231, 214);
            this.chk_rm.Name = "chk_rm";
            this.chk_rm.Size = new System.Drawing.Size(137, 17);
            this.chk_rm.TabIndex = 14;
            this.chk_rm.Text = "Recoge Mercancia ";
            this.chk_rm.UseVisualStyleBackColor = true;
            this.chk_rm.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 210);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Estatus: ";
            // 
            // txt_RM
            // 
            this.txt_RM.Enabled = false;
            this.txt_RM.Location = new System.Drawing.Point(68, 207);
            this.txt_RM.Name = "txt_RM";
            this.txt_RM.Size = new System.Drawing.Size(23, 20);
            this.txt_RM.TabIndex = 16;
            this.txt_RM.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // frm_RM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(542, 251);
            this.Controls.Add(this.txt_RM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chk_rm);
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
            this.Name = "frm_RM";
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
        private System.Windows.Forms.CheckBox chk_rm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_RM;
    }
}