
namespace ActualizadorDoctosUnigis
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.EnvioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.confirmarOrdenesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validarParadasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.liberarParadasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pendientesPorEmbarcarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kGPendientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.valesPendienteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.facturasToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarFechaEntregaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarRecogeMercanciaRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelarLiquidacionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelarDocumentosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arrangeIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lbl_usuario = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_nivel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsMenu,
            this.facturasToolStripMenuItem1,
            this.windowsMenu,
            this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.MdiWindowListItem = this.windowsMenu;
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(632, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            this.menuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip_ItemClicked);
            // 
            // toolsMenu
            // 
            this.toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EnvioToolStripMenuItem,
            this.confirmarOrdenesToolStripMenuItem,
            this.validarParadasToolStripMenuItem,
            this.liberarParadasToolStripMenuItem,
            this.pendientesPorEmbarcarToolStripMenuItem,
            this.kGPendientesToolStripMenuItem,
            this.valesPendienteToolStripMenuItem});
            this.toolsMenu.Name = "toolsMenu";
            this.toolsMenu.Size = new System.Drawing.Size(52, 20);
            this.toolsMenu.Text = "&Unigis";
            // 
            // EnvioToolStripMenuItem
            // 
            this.EnvioToolStripMenuItem.Enabled = false;
            this.EnvioToolStripMenuItem.Name = "EnvioToolStripMenuItem";
            this.EnvioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.EnvioToolStripMenuItem.Text = "&Envio Documentos";
            this.EnvioToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // confirmarOrdenesToolStripMenuItem
            // 
            this.confirmarOrdenesToolStripMenuItem.Enabled = false;
            this.confirmarOrdenesToolStripMenuItem.Name = "confirmarOrdenesToolStripMenuItem";
            this.confirmarOrdenesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.confirmarOrdenesToolStripMenuItem.Text = "&Confirmar Ordenes";
            this.confirmarOrdenesToolStripMenuItem.Click += new System.EventHandler(this.confirmarOrdenesToolStripMenuItem_Click);
            // 
            // validarParadasToolStripMenuItem
            // 
            this.validarParadasToolStripMenuItem.Enabled = false;
            this.validarParadasToolStripMenuItem.Name = "validarParadasToolStripMenuItem";
            this.validarParadasToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.validarParadasToolStripMenuItem.Text = "Validar Paradas";
            this.validarParadasToolStripMenuItem.Click += new System.EventHandler(this.validarParadasToolStripMenuItem_Click);
            // 
            // liberarParadasToolStripMenuItem
            // 
            this.liberarParadasToolStripMenuItem.Enabled = false;
            this.liberarParadasToolStripMenuItem.Name = "liberarParadasToolStripMenuItem";
            this.liberarParadasToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.liberarParadasToolStripMenuItem.Text = "Liberar Paradas";
            this.liberarParadasToolStripMenuItem.Click += new System.EventHandler(this.liberarParadasToolStripMenuItem_Click);
            // 
            // pendientesPorEmbarcarToolStripMenuItem
            // 
            this.pendientesPorEmbarcarToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.pendientesPorEmbarcarToolStripMenuItem.Enabled = false;
            this.pendientesPorEmbarcarToolStripMenuItem.Name = "pendientesPorEmbarcarToolStripMenuItem";
            this.pendientesPorEmbarcarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pendientesPorEmbarcarToolStripMenuItem.Text = "Vizualizador";
            this.pendientesPorEmbarcarToolStripMenuItem.Click += new System.EventHandler(this.pendientesPorEmbarcarToolStripMenuItem_Click);
            // 
            // kGPendientesToolStripMenuItem
            // 
            this.kGPendientesToolStripMenuItem.Enabled = false;
            this.kGPendientesToolStripMenuItem.Name = "kGPendientesToolStripMenuItem";
            this.kGPendientesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.kGPendientesToolStripMenuItem.Text = "KG Pendientes ";
            this.kGPendientesToolStripMenuItem.Click += new System.EventHandler(this.kGPendientesToolStripMenuItem_Click);
            // 
            // valesPendienteToolStripMenuItem
            // 
            this.valesPendienteToolStripMenuItem.Enabled = false;
            this.valesPendienteToolStripMenuItem.Name = "valesPendienteToolStripMenuItem";
            this.valesPendienteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.valesPendienteToolStripMenuItem.Text = "Vales Pendiente";
            this.valesPendienteToolStripMenuItem.Click += new System.EventHandler(this.valesPendienteToolStripMenuItem_Click);
            // 
            // facturasToolStripMenuItem1
            // 
            this.facturasToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modificarFechaEntregaToolStripMenuItem1,
            this.modificarRecogeMercanciaRToolStripMenuItem,
            this.cancelarLiquidacionToolStripMenuItem,
            this.cancelarDocumentosToolStripMenuItem});
            this.facturasToolStripMenuItem1.Name = "facturasToolStripMenuItem1";
            this.facturasToolStripMenuItem1.Size = new System.Drawing.Size(63, 20);
            this.facturasToolStripMenuItem1.Text = "Facturas";
            // 
            // modificarFechaEntregaToolStripMenuItem1
            // 
            this.modificarFechaEntregaToolStripMenuItem1.Enabled = false;
            this.modificarFechaEntregaToolStripMenuItem1.Name = "modificarFechaEntregaToolStripMenuItem1";
            this.modificarFechaEntregaToolStripMenuItem1.Size = new System.Drawing.Size(246, 22);
            this.modificarFechaEntregaToolStripMenuItem1.Text = "Modificar Fecha Entrega";
            this.modificarFechaEntregaToolStripMenuItem1.Click += new System.EventHandler(this.modificarFechaEntregaToolStripMenuItem1_Click);
            // 
            // modificarRecogeMercanciaRToolStripMenuItem
            // 
            this.modificarRecogeMercanciaRToolStripMenuItem.Enabled = false;
            this.modificarRecogeMercanciaRToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.modificarRecogeMercanciaRToolStripMenuItem.Name = "modificarRecogeMercanciaRToolStripMenuItem";
            this.modificarRecogeMercanciaRToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.modificarRecogeMercanciaRToolStripMenuItem.Text = "Modificar Recoge Mercancia RM";
            this.modificarRecogeMercanciaRToolStripMenuItem.Click += new System.EventHandler(this.modificarRecogeMercanciaRToolStripMenuItem_Click);
            // 
            // cancelarLiquidacionToolStripMenuItem
            // 
            this.cancelarLiquidacionToolStripMenuItem.Enabled = false;
            this.cancelarLiquidacionToolStripMenuItem.Name = "cancelarLiquidacionToolStripMenuItem";
            this.cancelarLiquidacionToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.cancelarLiquidacionToolStripMenuItem.Text = "Cancelar Liquidacion";
            this.cancelarLiquidacionToolStripMenuItem.Click += new System.EventHandler(this.cancelarLiquidacionToolStripMenuItem_Click);
            // 
            // cancelarDocumentosToolStripMenuItem
            // 
            this.cancelarDocumentosToolStripMenuItem.Name = "cancelarDocumentosToolStripMenuItem";
            this.cancelarDocumentosToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.cancelarDocumentosToolStripMenuItem.Text = "Cancelar Documentos";
            this.cancelarDocumentosToolStripMenuItem.Click += new System.EventHandler(this.cancelarDocumentosToolStripMenuItem_Click);
            // 
            // windowsMenu
            // 
            this.windowsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newWindowToolStripMenuItem,
            this.cascadeToolStripMenuItem,
            this.tileVerticalToolStripMenuItem,
            this.tileHorizontalToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.arrangeIconsToolStripMenuItem});
            this.windowsMenu.Name = "windowsMenu";
            this.windowsMenu.Size = new System.Drawing.Size(66, 20);
            this.windowsMenu.Text = "&Ventanas";
            this.windowsMenu.Click += new System.EventHandler(this.windowsMenu_Click);
            // 
            // newWindowToolStripMenuItem
            // 
            this.newWindowToolStripMenuItem.Name = "newWindowToolStripMenuItem";
            this.newWindowToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.newWindowToolStripMenuItem.Text = "&Nueva ventana";
            this.newWindowToolStripMenuItem.Click += new System.EventHandler(this.ShowNewForm);
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.cascadeToolStripMenuItem.Text = "&Cascada";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.CascadeToolStripMenuItem_Click);
            // 
            // tileVerticalToolStripMenuItem
            // 
            this.tileVerticalToolStripMenuItem.Name = "tileVerticalToolStripMenuItem";
            this.tileVerticalToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.tileVerticalToolStripMenuItem.Text = "Mosaico &vertical";
            this.tileVerticalToolStripMenuItem.Click += new System.EventHandler(this.TileVerticalToolStripMenuItem_Click);
            // 
            // tileHorizontalToolStripMenuItem
            // 
            this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
            this.tileHorizontalToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.tileHorizontalToolStripMenuItem.Text = "Mosaico &horizontal";
            this.tileHorizontalToolStripMenuItem.Click += new System.EventHandler(this.TileHorizontalToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.closeAllToolStripMenuItem.Text = "C&errar todo";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.CloseAllToolStripMenuItem_Click);
            // 
            // arrangeIconsToolStripMenuItem
            // 
            this.arrangeIconsToolStripMenuItem.Name = "arrangeIconsToolStripMenuItem";
            this.arrangeIconsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.arrangeIconsToolStripMenuItem.Text = "&Organizar iconos";
            this.arrangeIconsToolStripMenuItem.Click += new System.EventHandler(this.ArrangeIconsToolStripMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator8,
            this.aboutToolStripMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(53, 20);
            this.helpMenu.Text = "Ay&uda";
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.contentsToolStripMenuItem.Text = "&Contenido";
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("indexToolStripMenuItem.Image")));
            this.indexToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.indexToolStripMenuItem.Text = "&Índice";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("searchToolStripMenuItem.Image")));
            this.searchToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.searchToolStripMenuItem.Text = "&Buscar";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(173, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.aboutToolStripMenuItem.Text = "&Acerca de... ...";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_usuario,
            this.lbl_nivel});
            this.statusStrip.Location = new System.Drawing.Point(0, 431);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(632, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // lbl_usuario
            // 
            this.lbl_usuario.Name = "lbl_usuario";
            this.lbl_usuario.Size = new System.Drawing.Size(47, 17);
            this.lbl_usuario.Text = "Usuario";
            // 
            // lbl_nivel
            // 
            this.lbl_nivel.Name = "lbl_nivel";
            this.lbl_nivel.Size = new System.Drawing.Size(118, 17);
            this.lbl_nivel.Text = "toolStripStatusLabel1";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 453);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsMenu;
        private System.Windows.Forms.ToolStripMenuItem EnvioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsMenu;
        private System.Windows.Forms.ToolStripMenuItem newWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arrangeIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem confirmarOrdenesToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lbl_usuario;
        private System.Windows.Forms.ToolStripStatusLabel lbl_nivel;
        private System.Windows.Forms.ToolStripMenuItem liberarParadasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem facturasToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem modificarFechaEntregaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem modificarRecogeMercanciaRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validarParadasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pendientesPorEmbarcarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kGPendientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem valesPendienteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelarLiquidacionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelarDocumentosToolStripMenuItem;
    }
}



