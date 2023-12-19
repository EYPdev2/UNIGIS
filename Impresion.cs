using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Windows.Forms;
using System;
using System.Windows.Forms;

namespace ActualizadorDoctosUnigis
{

    public partial class Impresion : Form
    {
        CrystalDecisions.Windows.Forms.CrystalReportViewer cr = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
        ReportDocument cp;
        public Impresion()
        {
            InitializeComponent();
        }

        public Impresion(ReportDocument rp)
        {

              // 
            cr.Visible = true;
            cr.ActiveViewIndex = -1;
            cr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            cr.Cursor = System.Windows.Forms.Cursors.Default;
            cr.Dock = System.Windows.Forms.DockStyle.Fill;
            cr.Location = new System.Drawing.Point(0, 0);
            cr.Name = "crystalReportViewer1";
            cr.Size = new System.Drawing.Size(1303, 574);
            cr.TabIndex = 0;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1303, 574);
             this.Controls.Add(cr);
            this.Name = "Impresion";
            this.Text = "Impresion";
            this.Load += new System.EventHandler(this.Impresion_Load);
            this.ResumeLayout(false);
            InitializeComponent();
            cp = rp;

        }

        private void Impresion_Load(object sender, EventArgs e)
        {
            cr.ReportSource = cp;

        }
    }
}
