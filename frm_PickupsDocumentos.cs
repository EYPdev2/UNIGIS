using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ActualizadorDoctosUnigis
{
    // PP de las facturas y remisiones que se van a enviar. Logistica solo marca la venta; aqui se muestran los PP con el mismo folio
    // para enviarlos juntos, y se pueden desmarcar los que no deban ir en este envio
    public partial class frm_PickupsDocumentos : Form
    {
        List<DataRow> pickups;
        DataTable dtPickups = new DataTable();
        // Columnas del renglon del PP en el visualizador que se muestran tal cual (mismo nombre, tipo y valor)
        static readonly string[] ColumnasDelVisualizador = { "Productos", "FECHA_DOC", "FechaEntrega", "Foraneo", "Dist_Km", "notas" };

        // Renglones de documento (dtDocs de Envio) de los PP que quedaron marcados al confirmar
        public List<DataRow> Elegidos { get; private set; } = new List<DataRow>();

        // ultimoEnvio: envio mas reciente de cada PP segun el historico; los que nunca se han enviado no vienen y se muestran en blanco
        public frm_PickupsDocumentos(List<DataRow> pickups, Dictionary<string, string> ventaDePickup, Dictionary<string, Qrys.EnvioAnterior> ultimoEnvio)
        {
            InitializeComponent();
            this.pickups = pickups;

            // Folio es la factura o remision de origen del PP
            dtPickups.Columns.Add("Enviar", typeof(bool));
            dtPickups.Columns.Add("Folio", typeof(string));
            dtPickups.Columns.Add("Pick Up", typeof(string));
            dtPickups.Columns.Add("Último envío", typeof(DateTime));
            DataTable visualizador = pickups[0].Table;
            foreach (string columna in ColumnasDelVisualizador)
            {
                if (visualizador.Columns.Contains(columna))
                    dtPickups.Columns.Add(visualizador.Columns[columna].ColumnName, visualizador.Columns[columna].DataType);
            }

            foreach (DataRow pickup in pickups)
            {
                string nombre = pickup["N° DOCUMENTO"].ToString().Trim();
                DataRow fila = dtPickups.NewRow();
                fila["Enviar"] = true;
                fila["Folio"] = ventaDePickup[nombre];
                fila["Pick Up"] = nombre;
                Qrys.EnvioAnterior envio;
                fila["Último envío"] = ultimoEnvio.TryGetValue(nombre, out envio) ? (object)envio.Fecha : DBNull.Value;
                foreach (string columna in ColumnasDelVisualizador)
                {
                    if (dtPickups.Columns.Contains(columna))
                        fila[columna] = pickup[columna];
                }
                dtPickups.Rows.Add(fila);
            }

            dgv_Pickups.DataSource = dtPickups;
            foreach (DataGridViewColumn col in dgv_Pickups.Columns)
            {
                // Sin ordenar: el renglon i de la tabla siempre corresponde a pickups[i]
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.ReadOnly = col.Name != "Enviar";
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            // Mismo formato que el aviso de documentos ya enviados
            dgv_Pickups.Columns["Último envío"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            if (dgv_Pickups.Columns.Contains("notas"))
            {
                // Las notas pueden ser largas: toman el espacio que sobra, como en el visualizador. El minimo va antes que Fill:
                // al reves la cuadricula lanza NullReferenceException porque todavia no se muestra
                dgv_Pickups.Columns["notas"].MinimumWidth = 200;
                dgv_Pickups.Columns["notas"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            ActualizarSeleccion();
        }

        private void ActualizarSeleccion()
        {
            int marcados = 0;
            foreach (DataRow row in dtPickups.Rows)
            {
                if (Convert.ToBoolean(row["Enviar"]))
                    marcados++;
            }

            lbl_Seleccion.Text = marcados == 0
                ? "No se enviará ningún PP, solo las facturas y remisiones."
                : marcados + " de " + dtPickups.Rows.Count + " PP se enviarán.";
            lbl_Seleccion.ForeColor = marcados == 0 ? Color.FromArgb(255, 220, 130) : Color.White;
        }

        // La casilla se aplica al momento para que el conteo se actualice sin salir de la celda
        private void dgv_Pickups_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv_Pickups.IsCurrentCellDirty && dgv_Pickups.CurrentCell is DataGridViewCheckBoxCell)
                dgv_Pickups.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgv_Pickups_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgv_Pickups.Columns[e.ColumnIndex].Name == "Enviar")
                ActualizarSeleccion();
        }

        private void btn_Enviar_Click(object sender, EventArgs e)
        {
            dgv_Pickups.EndEdit();

            Elegidos = new List<DataRow>();
            for (int i = 0; i < pickups.Count; i++)
            {
                if (Convert.ToBoolean(dtPickups.Rows[i]["Enviar"]))
                    Elegidos.Add(pickups[i]);
            }

            DialogResult = DialogResult.OK;
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            dgv_Pickups.CancelEdit();
            DialogResult = DialogResult.Cancel;
        }
    }
}
