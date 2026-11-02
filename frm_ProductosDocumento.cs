using ActualizadorDoctosUnigis.Contoller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ActualizadorDoctosUnigis
{
    // Productos de un documento de Envio o de Cancelar Documentos. Se trabaja sobre una copia y solo al confirmar
    // se pasan la seleccion (y en Envio las cantidades y los pesos) a los renglones originales
    public partial class frm_ProductosDocumento : Form
    {
        Qrys q;
        List<DataRow> filas;
        DataTable dtProductos = new DataTable();
        Dictionary<int, decimal> pesosUnitarios = new Dictionary<int, decimal>();
        object cantidadAlEditar;
        // En Cancelar Documentos solo se eligen productos: la cantidad se muestra sin editar y no hay peso
        bool editarCantidad = true;
        string accion = "enviar";

        // Envio de Documentos: renglones de EYP_UNIGIS con Seleccionado y CantidadOriginal
        public frm_ProductosDocumento(string documento, string cliente, List<DataRow> productos, Qrys qrys)
        {
            InitializeComponent();
            q = qrys;
            filas = productos;
            Text = "Productos de " + documento;
            lbl_Documento.Text = documento + (cliente.Trim() == "" ? "" : "   " + cliente.Trim());

            dtProductos.Columns.Add("Enviar", typeof(bool));
            dtProductos.Columns.Add("Código", typeof(string));
            dtProductos.Columns.Add("Descripción", typeof(string));
            dtProductos.Columns.Add("Pendiente", typeof(decimal));
            dtProductos.Columns.Add("Cantidad", typeof(decimal));
            dtProductos.Columns.Add("Peso KG", typeof(decimal));
            foreach (DataRow fila in filas)
            {
                dtProductos.Rows.Add(
                    Convert.ToBoolean(fila["Seleccionado"]),
                    fila["CODIGO ITEM"].ToString().Trim(),
                    fila["NOMBRE ITEM"].ToString().Trim(),
                    ADecimal(fila["CantidadOriginal"]),
                    ADecimal(fila["CANTIDAD"]),
                    ADecimal(fila["Peso_KG"]));
            }

            dgv_Productos.DataSource = dtProductos;
            ConfigurarColumnas();
            dgv_Productos.Columns["Pendiente"].DefaultCellStyle.Format = "0.####";
            dgv_Productos.Columns["Cantidad"].DefaultCellStyle.BackColor = Color.LightYellow;
            dgv_Productos.Columns["Peso KG"].DefaultCellStyle.Format = "0.##";

            ActualizarSeleccion();
        }

        // Cancelar Documentos: renglones de productos_por_embarcar (SelecDocumentos) con Seleccionado
        public frm_ProductosDocumento(string documento, List<DataRow> productos)
        {
            InitializeComponent();
            filas = productos;
            editarCantidad = false;
            accion = "cancelar";
            Text = "Productos de " + documento;
            lbl_Documento.Text = documento;

            dtProductos.Columns.Add("Enviar", typeof(bool));
            dtProductos.Columns.Add("Código", typeof(string));
            dtProductos.Columns.Add("Descripción", typeof(string));
            dtProductos.Columns.Add("Cantidad", typeof(decimal));
            foreach (DataRow fila in filas)
            {
                dtProductos.Rows.Add(
                    Convert.ToBoolean(fila["Seleccionado"]),
                    fila["cod_prod"].ToString().Trim(),
                    fila["descripcion"].ToString().Trim(),
                    ADecimal(fila["cantidad"]));
            }

            dgv_Productos.DataSource = dtProductos;
            ConfigurarColumnas();
            dgv_Productos.Columns["Enviar"].HeaderText = "Cancelar";

            ActualizarSeleccion();
        }

        private void ConfigurarColumnas()
        {
            foreach (DataGridViewColumn col in dgv_Productos.Columns)
            {
                // Sin ordenar: el renglon i de la tabla siempre corresponde a filas[i]
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.ReadOnly = col.Name != "Enviar" && !(editarCantidad && col.Name == "Cantidad");
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            dgv_Productos.Columns["Descripción"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv_Productos.Columns["Cantidad"].DefaultCellStyle.Format = "0.####";
        }

        private static decimal ADecimal(object valor)
        {
            return valor == null || valor == DBNull.Value ? 0M : Convert.ToDecimal(valor);
        }

        private void ActualizarSeleccion()
        {
            int marcados = Marcados();
            lbl_Seleccion.Text = marcados + " de " + dtProductos.Rows.Count + " productos marcados";
            if (marcados == 0)
                lbl_Seleccion.Text += ". Debe quedar al menos uno para poder " + accion + " el documento.";
            lbl_Seleccion.ForeColor = marcados == 0 ? Color.FromArgb(255, 170, 170) : Color.White;
            btn_Confirmar.Enabled = marcados > 0;
        }

        private int Marcados()
        {
            int marcados = 0;
            foreach (DataRow row in dtProductos.Rows)
            {
                if (Convert.ToBoolean(row["Enviar"]))
                    marcados++;
            }
            return marcados;
        }

        // La casilla se aplica al momento para que el conteo de marcados se actualice sin salir de la celda
        private void dgv_Productos_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv_Productos.IsCurrentCellDirty && dgv_Productos.CurrentCell is DataGridViewCheckBoxCell)
                dgv_Productos.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgv_Productos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgv_Productos.Columns[e.ColumnIndex].Name == "Enviar")
                ActualizarSeleccion();
        }

        private void dgv_Productos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox editor = e.Control as TextBox;
            if (editor == null)
                return;

            editor.KeyPress -= SoloNumeros;
            if (dgv_Productos.CurrentCell.OwningColumn.Name == "Cantidad")
                editor.KeyPress += SoloNumeros;
        }

        // Mismas teclas que se permitian al capturar la cantidad en la pantalla de envio: digitos, borrar y un solo punto
        private void SoloNumeros(object sender, KeyPressEventArgs e)
        {
            TextBox editor = (TextBox)sender;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '.')
                e.Handled = true;
            else if (e.KeyChar == '.' && editor.Text.Contains("."))
                e.Handled = true;
        }

        private void dgv_Productos_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            cantidadAlEditar = dtProductos.Rows[e.RowIndex]["Cantidad"];
        }

        private void dgv_Productos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgv_Productos.Columns[e.ColumnIndex].Name != "Cantidad" || !dgv_Productos.IsCurrentCellInEditMode)
                return;

            decimal pendiente = ADecimal(dtProductos.Rows[e.RowIndex]["Pendiente"]);
            decimal cantidad;
            string error = null;
            if (!decimal.TryParse(e.FormattedValue?.ToString(), out cantidad))
                error = "Por favor, ingrese una cantidad válida.";
            else if (cantidad > pendiente)
                error = "La cantidad máxima permitida es " + pendiente.ToString("0.####") + ".";
            else if (cantidad <= 0)
                error = "La cantidad debe ser mayor a 0. Si no quiere enviar este producto, desmárquelo.";

            if (error != null)
            {
                MessageBox.Show(error, "Cantidad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dgv_Productos.CancelEdit();
                e.Cancel = true;
            }
        }

        private void dgv_Productos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_Productos.Columns[e.ColumnIndex].Name != "Cantidad")
                return;

            DataRow row = dtProductos.Rows[e.RowIndex];
            if (Equals(row["Cantidad"], cantidadAlEditar))
                return;

            try
            {
                row["Peso KG"] = ADecimal(row["Cantidad"]) * PesoUnitario(e.RowIndex);
            }
            catch (Exception ex)
            {
                row["Cantidad"] = cantidadAlEditar;
                MessageBox.Show("No fue posible recalcular el peso, se dejó la cantidad anterior. Por favor intente de nuevo." + Environment.NewLine + Environment.NewLine + Libreria.MensajeError(ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Misma regla que se usaba al editar la cantidad en la pantalla de envio:
        // 848 y las 30 que no son orden de compra van con peso 0; lo demas usa el peso del producto (0.01 si no tiene)
        private decimal PesoUnitario(int indice)
        {
            decimal peso;
            if (pesosUnitarios.TryGetValue(indice, out peso))
                return peso;

            DataRow original = filas[indice];
            string doc = original["N° DOCUMENTO"].ToString().Trim();
            string codProd = original["CODIGO ITEM"].ToString().Trim();
            string transaccion = original.Table.Columns.Contains("Transaccion") ? original["Transaccion"].ToString().Trim() : "";

            bool pesoEnCero = transaccion == "848";
            if (!pesoEnCero && transaccion == "30")
            {
                string folio = doc.Substring(doc.IndexOf('-') + 1);
                bool esOrdenCompra = (folio.Length >= 3 && char.ToUpperInvariant(folio[2]) == 'X')
                                  || (folio.Length >= 4 && char.ToUpperInvariant(folio[3]) == 'X');
                pesoEnCero = !esOrdenCompra;
            }

            if (pesoEnCero)
            {
                peso = 0M;
            }
            else
            {
                peso = 0.01M;
                DataTable pesoDt = q.Consultar($"select peso_total from productos where cod_prod = '{codProd}'");
                if (pesoDt.Rows.Count > 0 && pesoDt.Rows[0][0] != DBNull.Value)
                    peso = Convert.ToDecimal(pesoDt.Rows[0][0]);
                if (peso == 0M)
                    peso = 0.01M;
            }

            pesosUnitarios[indice] = peso;
            return peso;
        }

        private void btn_Confirmar_Click(object sender, EventArgs e)
        {
            if (!dgv_Productos.EndEdit())
                return;

            if (Marcados() == 0)
            {
                MessageBox.Show("Debe dejar al menos un producto marcado para poder " + accion + " el documento.", "Productos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            for (int i = 0; i < filas.Count; i++)
            {
                DataRow editado = dtProductos.Rows[i];
                filas[i]["Seleccionado"] = editado["Enviar"];
                if (editarCantidad)
                {
                    filas[i]["CANTIDAD"] = editado["Cantidad"];
                    filas[i]["Peso_KG"] = editado["Peso KG"];
                }
            }

            DialogResult = DialogResult.OK;
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            dgv_Productos.CancelEdit();
            DialogResult = DialogResult.Cancel;
        }
    }
}
