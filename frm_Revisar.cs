using ActualizadorDoctosUnigis.Exceptions;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ActualizadorDoctosUnigis
{
    public partial class frm_Revisar : Form
    {
        private readonly Qrys   _q;
        private readonly string _usuario;
        private readonly int    _id;

        public frm_Revisar(Qrys q, string usuario, string estado, string documento, string dato,
                            int id,
                            string solicita, string valorActual, string valorNuevo,
                            string razon, string comentario)
        {
            InitializeComponent();

            _q       = q;
            _usuario = usuario;
            _id      = id;

            // Estado — label con color
            lbl_EstadoValor.Text = estado;
            switch (estado)
            {
                case "Pendiente":
                    lbl_EstadoValor.BackColor = Color.Orange;
                    lbl_EstadoValor.ForeColor = Color.White;
                    break;
                case "Autorizada":
                    lbl_EstadoValor.BackColor = Color.SeaGreen;
                    lbl_EstadoValor.ForeColor = Color.White;
                    break;
                case "Rechazada":
                    lbl_EstadoValor.BackColor = Color.IndianRed;
                    lbl_EstadoValor.ForeColor = Color.White;
                    break;
                default:
                    lbl_EstadoValor.BackColor = Color.Gray;
                    lbl_EstadoValor.ForeColor = Color.White;
                    break;
            }

            // Etiquetas dinámicas según el tipo de modificación
            if (dato == "Fecha Entrega")
            {
                lbl_ValorActual.Text = "Fecha de entrega (Actual)";
                lbl_ValorNuevo.Text  = "Fecha de entrega (Nueva)";
                if (DateTime.TryParse(valorActual, out DateTime va)) valorActual = va.ToString("dd/MM/yyyy hh:mm tt");
                if (DateTime.TryParse(valorNuevo,  out DateTime vn)) valorNuevo  = vn.ToString("dd/MM/yyyy hh:mm tt");
            }
            else if (dato == "Recoge Mercancia")
            {
                lbl_ValorActual.Text = "Recoge mercancia (Actual)";
                lbl_ValorNuevo.Text  = "Recoge mercancia (Nuevo)";
            }

            txt_Documento.Text   = documento;
            txt_ValorActual.Text = valorActual;
            txt_ValorNuevo.Text  = valorNuevo;
            txt_Solicita.Text    = solicita;
            txt_Razon.Text       = razon;
            txt_Comentario.Text  = comentario;

            if (estado != "Pendiente")
            {
                btn_Rechazar.Visible = false;
                btn_Autorizar.Visible = false;
                ClientSize = new System.Drawing.Size(ClientSize.Width, btn_Rechazar.Top + 20);
            }
        }

        private void btn_Rechazar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Estás seguro de que deseas rechazar esta solicitud?", "Atención",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _q.RevisarSolicitud(_id, _usuario, "R");

                MessageBox.Show("La solicitud ha sido rechazada correctamente.", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (SolicitudYaCanceladaException)
            {
                MessageBox.Show("La solicitud fue cancelada por el usuario antes de rechazarla.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (SolicitudYaRevisadaException ex)
            {
                MessageBox.Show($"La solicitud acaba de ser revisada por {ex.NombreRevisor}.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Un error inesperado no ha permitido rechazar la solicitud. Por favor intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Autorizar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Estás seguro de que deseas autorizar esta solicitud?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                _q.RevisarSolicitud(_id, _usuario, "A");

                MessageBox.Show("La solicitud ha sido autorizada correctamente.", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (SolicitudYaCanceladaException)
            {
                MessageBox.Show("La solicitud fue cancelada por el usuario antes de autorizarla.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (SolicitudYaRevisadaException ex)
            {
                MessageBox.Show($"La solicitud acaba de ser revisada por {ex.NombreRevisor}.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (SqlException ex) when (ex.Number == 528491)
            {
                MessageBox.Show(ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Un error inesperado no ha permitido autorizar la solicitud. Por favor intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
