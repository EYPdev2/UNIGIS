using ActualizadorDoctosUnigis.Contoller;
using ActualizadorDoctosUnigis.Exceptions;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ActualizadorDoctosUnigis
{
    public partial class frm_RMV2 : Form
    {
        private TextBox _txtTipoDocumento;

        private Qrys q = new Qrys();
        private Libreria lib = new Libreria();
        private string[] t = new string[5];
        private string u;

        private string _folioDoc;
        private int _transDoc;
        private int _rmActual;

        public frm_RMV2()
        {
            InitializeComponent();
        }

        public frm_RMV2(string usuario)
        {
            InitializeComponent();
            u = usuario;
        }

        private void txt_folio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void frm_RM_Load(object sender, EventArgs e)
        {
            cmb_tipoD.Items.Add("Facturas a clientes");
            t[0] = "36";
            cmb_tipoD.Items.Add("Remisiones a clientes");
            t[1] = "37";
        }

        private void frm_RM_Resize(object sender, EventArgs e)
        {
            // Folio | Tipo | Estado(fijo)
            var availableFolioTipo = ClientSize.Width - 20 - 10 - 10 - txtStatus.Width - 20;
            var halfFolioTipo = (availableFolioTipo - 10) / 2;
            txt_folio.Width = halfFolioTipo;
            lbl_tipoDocto.Left = 20 + halfFolioTipo + 10;
            cmb_tipoD.Left = 20 + halfFolioTipo + 10;
            cmb_tipoD.Width = halfFolioTipo;
            if (_txtTipoDocumento != null)
            {
                _txtTipoDocumento.Left = cmb_tipoD.Left;
                _txtTipoDocumento.Width = cmb_tipoD.Width;
            }

            // Fecha emisión | Fecha entrega
            var available = ClientSize.Width - 40;
            var half = (available - 10) / 2;
            txtFechaEmision.Width = half;
            lbl_FE.Left = 20 + half + 10;
            txtFechaEntrega.Left = 20 + half + 10;
            txtFechaEntrega.Width = half;
        }

        private void btn_Actualizar_Click(object sender, EventArgs e)
        {
            _folioDoc = txt_folio.Text;
            _transDoc = cmb_tipoD.SelectedIndex;

            if (string.IsNullOrWhiteSpace(_folioDoc))
            {
                MessageBox.Show("Por favor ingrese el folio del documento.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_transDoc == -1)
            {
                MessageBox.Show("Por favor ingrese el tipo del documento.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable resultados;
            try
            {
                resultados = lib.Conexiones(_folioDoc);
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un error inesperado. Por favor vuelva a intentarlo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (resultados.Rows.Count == 0 || string.IsNullOrWhiteSpace(resultados.Rows[0][0]?.ToString()))
            {
                MessageBox.Show("El folio tiene un formato inválido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cod_estab = resultados.Rows[0][0].ToString();

            DataTable dataTable;
            try
            {
                dataTable = q.Consultar($"select top 1 f.fecha, f.fecha_entrega, rtrim(e.nombre), f.status, f.recoge_mercancia from facremtick f inner join establecimientos e on f.cod_estab = e.cod_estab where f.folio = '{_folioDoc}' And f.transaccion = '{t[_transDoc]}'", cod_estab);
            }
            catch (Exception)
            {
                var tipoDoc = t[_transDoc] == "36" ? "factura" : "remisión";
                MessageBox.Show($"Un error inesperado no ha permitido obtener la información de la {tipoDoc}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dataTable.Rows.Count == 0 || string.IsNullOrWhiteSpace(dataTable.Rows[0][0]?.ToString()))
            {
                MessageBox.Show("El documento no existe.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var status = dataTable.Rows[0][3].ToString().Trim();
            if (status == "C")
            {
                var tipoDoc = t[_transDoc] == "36" ? "factura" : "remisión";
                MessageBox.Show($"La {tipoDoc} se encuentra cancelada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStatus.Text = "Cancelada";
                txtStatus.BackColor = Color.IndianRed;
                txtStatus.ForeColor = Color.White;
            }
            else
            {
                txtStatus.Text = "Vigente";
                txtStatus.BackColor = Color.SeaGreen;
                txtStatus.ForeColor = Color.White;
            }

            _rmActual = Convert.ToBoolean(dataTable.Rows[0][4]) ? 1 : 0;

            var fechaDoc = DateTime.Parse(dataTable.Rows[0][0].ToString());
            var fechaEntrega = DateTime.Parse(dataTable.Rows[0][1].ToString());

            txtEstab.Text = dataTable.Rows[0][2].ToString();
            txtEstab.Enabled = true;
            txtFechaEmision.Text = fechaDoc.ToString("dd/MM/yyyy hh:mm tt");
            txtFechaEmision.Enabled = true;
            txtFechaEntrega.Text = fechaEntrega.ToString("dd/MM/yyyy hh:mm tt");
            txtFechaEntrega.Enabled = true;
            chk_RecogeMercancia.Checked = _rmActual == 1;

            txt_folio.ReadOnly = true;
            btn_SolicitarCambio.Enabled = true;

            if (_txtTipoDocumento == null)
            {
                _txtTipoDocumento = new TextBox
                {
                    Location = cmb_tipoD.Location,
                    Size = cmb_tipoD.Size,
                    Anchor = cmb_tipoD.Anchor,
                    ReadOnly = true,
                    Text = cmb_tipoD.Text,
                    TabStop = false
                };
                Controls.Add(_txtTipoDocumento);
                _txtTipoDocumento.BringToFront();
            }
            else
            {
                _txtTipoDocumento.Text = cmb_tipoD.Text;
                _txtTipoDocumento.Visible = true;
            }
            cmb_tipoD.Visible = false;

            try
            {
                CargarSolicitudes();
            }
            catch (Exception)
            {
                MessageBox.Show("No fue posible cargar el historial de solicitudes de cambio. Puedes intentarlo de nuevo refrescando la información.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_Solicitudes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_Solicitudes.Columns["btn_Cancelar"] == null) return;
            if (e.ColumnIndex != dgv_Solicitudes.Columns["btn_Cancelar"].Index || e.RowIndex < 0) return;

            if (dgv_Solicitudes.Rows[e.RowIndex].Cells["Estado"].Value?.ToString() != "Pendiente")
            {
                MessageBox.Show("No es posible cancelar una solicitud ya revisada.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Estás seguro de que deseas cancelar esta solicitud?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var id = (int)dgv_Solicitudes.Rows[e.RowIndex].Cells["id"].Value;

            try
            {
                q.CancelarSolicitud(id, _folioDoc, t[_transDoc], "Recoge Mercancia", u);
                MessageBox.Show("La solicitud ha sido cancelada correctamente.", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarSolicitudes();
            }
            catch (LimiteCancelacionesDiariasException)
            {
                MessageBox.Show("Este documento ya alcanzó el límite de 3 cancelaciones de solicitudes por día.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SolicitudYaCanceladaException)
            {
                MessageBox.Show("La solicitud ya fue cancelada por otro usuario.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                try { CargarSolicitudes(); } catch (Exception) { }
            }
            catch (SolicitudYaRevisadaException)
            {
                MessageBox.Show("La solicitud ya fue revisada y no puede ser cancelada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                try { CargarSolicitudes(); } catch (Exception) { }
            }
            catch (Exception)
            {
                MessageBox.Show("Un error inesperado no ha permitido cancelar la solicitud. Por favor intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_Solicitudes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var col = dgv_Solicitudes.Columns[e.ColumnIndex].Name;

            if (col != "Estado") return;

            switch (e.Value?.ToString())
            {
                case "Pendiente":
                    e.CellStyle.BackColor = Color.Orange;
                    e.CellStyle.ForeColor = Color.White;
                    break;
                case "Autorizado":
                    e.CellStyle.BackColor = Color.SeaGreen;
                    e.CellStyle.ForeColor = Color.White;
                    break;
                case "Rechazada":
                    e.CellStyle.BackColor = Color.IndianRed;
                    e.CellStyle.ForeColor = Color.White;
                    break;
            }
        }

        private void CargarSolicitudes()
        {
            DataTable dt = q.Consultar($"select pc.id, pc.fecha, pc.valor_actual, pc.valor_nuevo, rtrim(u.nombre) as solicita, rtrim(rc.Nombre) as razon, pc.comentario, case when pc.status_revision is null then 'Pendiente' when pc.status_revision = 'A' then 'Autorizado' else 'Rechazada' end as Estado from EYP_UNIGIS_solicitudes_cambios pc inner join usuarios u on pc.solicita = u.usuario inner join razones_Cambio_RM rc on pc.razon = rc.RazonID where folio = '{_folioDoc}' and transaccion = '{t[_transDoc]}' and dato = 'Recoge Mercancia' and pc.status = 'V' order by fecha desc");

            dgv_Solicitudes.DataSource = null;
            dgv_Solicitudes.Columns.Clear();

            if (dt.Rows.Count == 0) return;

            dgv_Solicitudes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv_Solicitudes.DataSource = dt;

            if (dgv_Solicitudes.Columns.Count > 0)
            {
                dgv_Solicitudes.Columns["id"].Visible = false;
                dgv_Solicitudes.Columns["Estado"].DisplayIndex = 0;
                dgv_Solicitudes.Columns["fecha"].HeaderText = "Fecha";
                dgv_Solicitudes.Columns["valor_actual"].HeaderText = "Valor Anterior";
                dgv_Solicitudes.Columns["valor_nuevo"].HeaderText = "Valor Nuevo";
                dgv_Solicitudes.Columns["solicita"].HeaderText = "Solicita";
                dgv_Solicitudes.Columns["razon"].HeaderText = "Razón";
                dgv_Solicitudes.Columns["comentario"].HeaderText = "Comentario";
                dgv_Solicitudes.Columns["Estado"].HeaderText = "Estado";
            }

            foreach (DataGridViewColumn col in dgv_Solicitudes.Columns)
                col.MinimumWidth = col.Width;
            dgv_Solicitudes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            var btnCancelar = new DataGridViewButtonColumn
            {
                Name = "btn_Cancelar",
                HeaderText = "",
                Text = "Cancelar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 80
            };
            dgv_Solicitudes.Columns.Add(btnCancelar);
        }

        private void btn_SolicitarCambio_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable statusDoc = q.Consultar($"select top 1 f.status from facremtick f where f.folio = '{_folioDoc}' and f.transaccion = '{t[_transDoc]}'");
                if (statusDoc.Rows.Count > 0 && statusDoc.Rows[0][0].ToString().Trim() == "C")
                {
                    var tipoDoc = t[_transDoc] == "36" ? "factura" : "remisión";
                    MessageBox.Show($"No es posible solicitar un cambio de recoge mercancía de la {tipoDoc} porque se encuentra cancelada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable pendiente = q.Consultar($"select top 1 1 from EYP_UNIGIS_solicitudes_cambios where folio = '{_folioDoc}' and transaccion = '{t[_transDoc]}' and dato = 'Recoge Mercancia' and status = 'V' and status_revision is null");
                if (pendiente.Rows.Count > 0)
                {
                    MessageBox.Show("Este documento ya tiene una solicitud de cambio en espera de revisión. Debes esperar a que sea revisada antes de enviar una nueva.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error inesperado. Intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var frm = new frm_SolicitudCambioRM(q, u, _folioDoc, t[_transDoc], _rmActual);

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    CargarSolicitudes();
                }
                catch (Exception)
                {
                    MessageBox.Show("No fue posible cargar el historial de solicitudes de cambio. Se va a refrescar la información en pantalla automáticamente cuando cierre este mensaje.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btn_Actualizar_Click(null, null);
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_folio.Text) && cmb_tipoD.SelectedIndex == -1)
                return;

            txt_folio.Text = "";
            txt_folio.ReadOnly = false;

            cmb_tipoD.SelectedIndex = -1;
            cmb_tipoD.Visible = true;
            if (_txtTipoDocumento != null)
                _txtTipoDocumento.Visible = false;

            txtEstab.Text = "";
            txtEstab.Enabled = false;
            txtFechaEmision.Text = "";
            txtFechaEmision.Enabled = false;
            txtFechaEntrega.Text = "";
            txtFechaEntrega.Enabled = false;
            chk_RecogeMercancia.Checked = false;

            txtStatus.Text = "";
            txtStatus.BackColor = SystemColors.Control;
            txtStatus.ForeColor = SystemColors.ControlText;

            dgv_Solicitudes.DataSource = null;
            dgv_Solicitudes.Columns.Clear();

            btn_SolicitarCambio.Enabled = false;
        }
    }
}
