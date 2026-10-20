using ActualizadorDoctosUnigis.Exceptions;
using System;
using System.Data;
using System.Windows.Forms;

namespace ActualizadorDoctosUnigis
{
    public partial class frm_SolicitudCambioRM : Form
    {
        private readonly Qrys _q;
        private readonly string _usuario;
        private readonly string _folioDoc;
        private readonly string _transaccion;
        private readonly int _rmActual;

        private string[] _R = new string[15];
        private string[] _U = new string[100];

        public frm_SolicitudCambioRM(Qrys q, string usuario, string folioDoc, string transaccion, int rmActual)
        {
            InitializeComponent();
            _q = q;
            _usuario = usuario;
            _folioDoc = folioDoc;
            _transaccion = transaccion;
            _rmActual = rmActual;
        }

        private void frm_SolicitudCambioRM_Load(object sender, EventArgs e)
        {
            chk_RM.Checked = _rmActual == 1;

            try
            {
                DataTable dtRazones = _q.Consultar("EXEC [dbo].[EyP_Razones_RM]");
                int i = 0;
                foreach (DataRow row in dtRazones.Rows)
                {
                    cmb_Razon.Items.Add(row[1].ToString().Trim());
                    _R[i++] = row[0].ToString();
                }

                DataTable dtUsuarios = _q.Consultar("exec [dbo].[EyP_USUARIOS_RM]");
                int j = 0;
                foreach (DataRow row in dtUsuarios.Rows)
                {
                    cmb_Solicita.Items.Add(row[1].ToString().Trim());
                    _U[j++] = row[0].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No fue posible cargar la información necesaria para enviar una solicitud de cambio. Por favor intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BeginInvoke(new Action(Close));
            }
        }

        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            var valorNuevo = chk_RM.Checked ? "1" : "0";
            if (valorNuevo == _rmActual.ToString())
            {
                MessageBox.Show("El valor de Recoge Mercancía no ha cambiado. Modifícalo para poder enviar la solicitud.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmb_Solicita.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor seleccione quien solicita el cambio.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmb_Razon.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor seleccione la razón del cambio.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_Notas.Text))
            {
                MessageBox.Show("Por favor ingrese un comentario con más detalles del cambio.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable permisoUsuario = _q.Consultar($"select top 1 1 from usuarios where usuario = '{_usuario}' and tipo_usuario in (1, 4, 38, 39)");
                if (permisoUsuario.Rows.Count == 0)
                {
                    MessageBox.Show("Tu usuario no tiene permiso para enviar solicitudes de cambio.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                    return;
                }

                DataTable pendiente = _q.Consultar($"select top 1 1 from EYP_UNIGIS_solicitudes_cambios where folio = '{_folioDoc}' and transaccion = '{_transaccion}' and dato = 'Recoge Mercancia' and status = 'V' and status_revision is null");
                if (pendiente.Rows.Count > 0)
                {
                    MessageBox.Show("Este documento ya tiene una solicitud de cambio en espera de revisión. Debes esperar a que sea revisada antes de enviar una nueva.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Un error inesperado no ha permitido crear la solicitud. Por favor intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Estás a punto de enviar una solicitud para cambiar el recoge mercancía. ¿Seguro que deseas continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _q.EnviarPeticionCambio(_folioDoc, _transaccion, _usuario, "Recoge Mercancia", _rmActual.ToString(), valorNuevo, _U[cmb_Solicita.SelectedIndex], _R[cmb_Razon.SelectedIndex], txt_Notas.Text);

                MessageBox.Show("La solicitud ha sido enviada correctamente.", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (PeticionCambioPendienteExisteException)
            {
                MessageBox.Show("Ya existe una solicitud de cambio pendiente para este documento.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Un error inesperado no ha permitido enviar la solicitud. Por favor intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frm_SolicitudCambioRM_Shown(object sender, EventArgs e)
        {
            pnl_Focus.Focus();
        }

        private void txt_Notas_TextChanged(object sender, EventArgs e)
        {
            lbl_ContadorNotas.Text = $"{txt_Notas.TextLength} / {txt_Notas.MaxLength}";
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
