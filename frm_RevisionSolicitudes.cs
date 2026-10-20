using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ActualizadorDoctosUnigis
{
    public partial class frm_RevisionSolicitudes : Form
    {
        private readonly Qrys q = new Qrys();
        private readonly string u;

        private int _paginaActual = 1;
        private bool _columnasConfiguradas;
        private string _filtros = "";
        private const int TamanioPagina = 50;

        public frm_RevisionSolicitudes(string usuario)
        {
            InitializeComponent();
            u = usuario;
        }

        private void frm_RevisionSolicitudes_Load(object sender, EventArgs e)
        {
            cmb_Tipo.Items.Add("Todas");
            cmb_Tipo.Items.Add("Fecha Entrega");
            cmb_Tipo.Items.Add("Recoge Mercancía");
            cmb_Tipo.SelectedIndex = 0;

            cmb_Estado.Items.Add("Todos");
            cmb_Estado.Items.Add("Pendiente");
            cmb_Estado.Items.Add("Autorizada");
            cmb_Estado.Items.Add("Rechazada");
            cmb_Estado.SelectedIndex = 1;

            cmb_TipoDoc.Items.Add("Todos");
            cmb_TipoDoc.Items.Add("Facturas a clientes");
            cmb_TipoDoc.Items.Add("Remisiones a clientes");
            cmb_TipoDoc.SelectedIndex = 0;

            try
            {
                CargarSolicitudes();
            }
            catch (Exception)
            {
                MessageBox.Show("Un error inesperado no ha permitido cargar las solicitudes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Actualizar_Click(object sender, EventArgs e)
        {
            _paginaActual = 1;
            try
            {
                CargarSolicitudes();
            }
            catch (Exception)
            {
                MessageBox.Show("Un error inesperado no ha permitido cargar las solicitudes. Por favor intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Limpiar_Click(object sender, EventArgs e)
        {
            txt_Folio.Text = "";
            cmb_TipoDoc.SelectedIndex = 0;
            cmb_Tipo.SelectedIndex = 0;
            cmb_Estado.SelectedIndex = 1;
            _paginaActual = 1;
            try
            {
                CargarSolicitudes();
            }
            catch (Exception)
            {
                MessageBox.Show("Un error inesperado no ha permitido cargar las solicitudes. Por favor intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_PaginaAnterior_Click(object sender, EventArgs e)
        {
            _paginaActual--;
            try
            {
                MostrarPagina();
            }
            catch (Exception)
            {
                _paginaActual++;
                MessageBox.Show("Un error inesperado no ha permitido cargar la página anterior. Por favor intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_PaginaSiguiente_Click(object sender, EventArgs e)
        {
            _paginaActual++;
            try
            {
                MostrarPagina();
            }
            catch (Exception)
            {
                _paginaActual--;
                MessageBox.Show("Un error inesperado no ha permitido cargar la página siguiente. Por favor intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_Folio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void pnl_Paginacion_Resize(object sender, EventArgs e)
        {
            const int anchoAnterior = 85;
            const int anchoPagina = 190;
            const int anchoSiguiente = 85;
            const int gap = 5;
            const int totalAncho = anchoAnterior + gap + anchoPagina + gap + anchoSiguiente;

            int x = (pnl_Paginacion.Width - totalAncho) / 2;
            btn_PaginaAnterior.Left = x;
            lbl_Pagina.Left = x + anchoAnterior + gap;
            btn_PaginaSiguiente.Left = x + anchoAnterior + gap + anchoPagina + gap;
        }

        private void CargarSolicitudes()
        {
            var filtroTipo = "";
            if (cmb_Tipo.SelectedIndex == 1) filtroTipo = "and pc.dato = 'Fecha Entrega'";
            else if (cmb_Tipo.SelectedIndex == 2) filtroTipo = "and pc.dato = 'Recoge Mercancia'";

            var filtroEstado = "";
            if (cmb_Estado.SelectedIndex == 1) filtroEstado = "and pc.status_revision is null";
            else if (cmb_Estado.SelectedIndex == 2) filtroEstado = "and pc.status_revision = 'A'";
            else if (cmb_Estado.SelectedIndex == 3) filtroEstado = "and pc.status_revision = 'R'";

            var filtroFolio = "";
            if (!string.IsNullOrWhiteSpace(txt_Folio.Text))
                filtroFolio = $"and upper(rtrim(pc.folio)) = upper('{txt_Folio.Text.Trim().Replace("'", "''")}')";

            var filtroTipoDoc = "";
            if (cmb_TipoDoc.SelectedIndex == 1) filtroTipoDoc = "and pc.transaccion = '36'";
            else if (cmb_TipoDoc.SelectedIndex == 2) filtroTipoDoc = "and pc.transaccion = '37'";

            _filtros = $"where pc.status = 'V' {filtroTipo} {filtroEstado} {filtroFolio} {filtroTipoDoc}";
            _paginaActual = 1;
            MostrarPagina();
        }

        private void MostrarPagina()
        {
            int offset = (_paginaActual - 1) * TamanioPagina;

            var dt = q.Consultar(
                $"select COUNT(*) OVER() as total_count, pc.id, " +
                $"case when pc.status_revision is null then 'Pendiente' when pc.status_revision = 'A' then 'Autorizada' else 'Rechazada' end as estado, " +
                $"case pc.transaccion when '36' then 'FC-' + rtrim(pc.folio) when '37' then 'RC-' + rtrim(pc.folio) else pc.folio end as documento, " +
                $"pc.fecha, pc.dato, rtrim(ue.nombre) as enviada_por, pc.valor_actual, pc.valor_nuevo, rtrim(u.nombre) as solicita, rtrim(rc.Nombre) as razon, pc.comentario, " +
                $"rtrim(ur.nombre) as revisado_por, pc.fecha_revision, " +
                $"pc.folio, pc.transaccion " +
                $"from EYP_UNIGIS_solicitudes_cambios pc " +
                $"inner join usuarios u on pc.solicita = u.usuario " +
                $"inner join usuarios ue on pc.usuario = ue.usuario " +
                $"inner join razones_Cambio_RM rc on pc.razon = rc.RazonID " +
                $"left join usuarios ur on pc.usuario_revision = ur.usuario " +
                $"{_filtros} " +
                $"order by case when pc.status_revision is null then 1 when pc.status_revision = 'A' then 2 else 3 end, pc.fecha desc " +
                $"offset {offset} rows fetch next {TamanioPagina} rows only");

            int total = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["total_count"]) : 0;
            int totalPaginas = Math.Max(1, (int)Math.Ceiling((double)total / TamanioPagina));
            _paginaActual = Math.Max(1, Math.Min(_paginaActual, totalPaginas));

            dt.Columns.Remove("total_count");

            if (!_columnasConfiguradas)
            {
                dgv_Solicitudes.DataSource = null;
                dgv_Solicitudes.Columns.Clear();

                if (dt.Rows.Count > 0)
                {
                    dgv_Solicitudes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    dgv_Solicitudes.DataSource = dt;

                    dgv_Solicitudes.Columns["id"].Visible            = false;
                    dgv_Solicitudes.Columns["folio"].Visible         = false;
                    dgv_Solicitudes.Columns["transaccion"].Visible   = false;
                    dgv_Solicitudes.Columns["solicita"].Visible      = false;
                    dgv_Solicitudes.Columns["razon"].Visible         = false;
                    dgv_Solicitudes.Columns["comentario"].Visible    = false;
                    dgv_Solicitudes.Columns["estado"].HeaderText          = "Estado";
                    dgv_Solicitudes.Columns["documento"].HeaderText       = "Documento";
                    dgv_Solicitudes.Columns["fecha"].HeaderText           = "Fecha";
                    dgv_Solicitudes.Columns["dato"].HeaderText            = "Modificación";
                    dgv_Solicitudes.Columns["enviada_por"].HeaderText     = "Enviada Por";
                    dgv_Solicitudes.Columns["valor_actual"].HeaderText    = "Valor Actual";
                    dgv_Solicitudes.Columns["valor_nuevo"].HeaderText     = "Valor Nuevo";
                    dgv_Solicitudes.Columns["revisado_por"].HeaderText    = "Revisado Por";
                    dgv_Solicitudes.Columns["fecha_revision"].HeaderText  = "Fecha Revisión";

                    foreach (DataGridViewColumn col in dgv_Solicitudes.Columns)
                        col.MinimumWidth = col.Width;
                    dgv_Solicitudes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    dgv_Solicitudes.Columns.Add(new DataGridViewButtonColumn
                    {
                        Name = "btn_Revisar",
                        HeaderText = "",
                        Text = "Revisar",
                        UseColumnTextForButtonValue = true,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                        Width = 80
                    });

                    dgv_Solicitudes.AutoGenerateColumns = false;
                    _columnasConfiguradas = true;
                }
            }
            else
            {
                dgv_Solicitudes.DataSource = null;
                dgv_Solicitudes.DataSource = dt;
            }

            lbl_Pagina.Text = $"Página {_paginaActual} de {totalPaginas}   •   {total} registros";
            btn_PaginaAnterior.Enabled = _paginaActual > 1;
            btn_PaginaSiguiente.Enabled = _paginaActual < totalPaginas;
            dgv_Solicitudes.ClearSelection();
        }

        private void dgv_Solicitudes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var colName = dgv_Solicitudes.Columns[e.ColumnIndex].Name;

            if (colName == "estado")
            {
                switch (e.Value?.ToString())
                {
                    case "Pendiente":
                        e.CellStyle.BackColor = Color.Orange;
                        e.CellStyle.ForeColor = Color.White;
                        break;
                    case "Autorizada":
                        e.CellStyle.BackColor = Color.SeaGreen;
                        e.CellStyle.ForeColor = Color.White;
                        break;
                    case "Rechazada":
                        e.CellStyle.BackColor = Color.IndianRed;
                        e.CellStyle.ForeColor = Color.White;
                        break;
                }
                return;
            }

            if ((colName == "valor_actual" || colName == "valor_nuevo") &&
                dgv_Solicitudes.Rows[e.RowIndex].Cells["dato"].Value?.ToString() == "Fecha Entrega" &&
                e.Value != null && DateTime.TryParse(e.Value.ToString(), out DateTime fecha))
            {
                e.Value = fecha.ToString("dd/MM/yyyy hh:mm tt");
                e.FormattingApplied = true;
            }

            if (colName == "revisado_por" && (e.Value == null || e.Value == DBNull.Value))
            {
                e.Value = "";
                e.FormattingApplied = true;
            }

            if (colName == "fecha_revision")
            {
                if (e.Value == null || e.Value == DBNull.Value)
                {
                    e.Value = "";
                    e.FormattingApplied = true;
                }
                else if (DateTime.TryParse(e.Value.ToString(), out DateTime fechaRev))
                {
                    e.Value = fechaRev.ToString("dd/MM/yyyy hh:mm tt");
                    e.FormattingApplied = true;
                }
            }
        }

        private bool TienePermisosDeRevision()
        {
            var dt = q.Consultar(
                $"select rtrim(tipo_usuario) from usuarios where usuario = '{u}'"
            );
            if (dt == null || dt.Rows.Count == 0) return false;

            var tipo = dt.Rows[0][0]?.ToString();
            return tipo == "1" || tipo == "39";
        }

        private void dgv_Solicitudes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgv_Solicitudes.Columns["btn_Revisar"] == null) return;
            if (e.ColumnIndex != dgv_Solicitudes.Columns["btn_Revisar"].Index) return;

            var row = dgv_Solicitudes.Rows[e.RowIndex];

            try
            {
                if (!TienePermisosDeRevision())
                {
                    MessageBox.Show("No tienes permisos para revisar solicitudes.", "Sin permisos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Un error inesperado no ha permitido revisar la solicitud. Por favor intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var form = new frm_Revisar(
                q:           q,
                usuario:     u,
                estado:      row.Cells["estado"].Value?.ToString(),
                documento:   row.Cells["documento"].Value?.ToString(),
                dato:        row.Cells["dato"].Value?.ToString(),
                id:          (int)row.Cells["id"].Value,
                solicita:    row.Cells["solicita"].Value?.ToString(),
                valorActual: row.Cells["valor_actual"].Value?.ToString(),
                valorNuevo:  row.Cells["valor_nuevo"].Value?.ToString(),
                razon:       row.Cells["razon"].Value?.ToString(),
                comentario:  row.Cells["comentario"].Value?.ToString()
            );
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _paginaActual = 1;
                try
                {
                    CargarSolicitudes();
                }
                catch (Exception)
                {
                    MessageBox.Show("Un error inesperado no ha permitido recargar las solicitudes. Por favor actualice manualmente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
