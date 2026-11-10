using ActualizadorDoctosUnigis.Contoller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActualizadorDoctosUnigis
{
    // Explica por que un documento, y cada uno de sus productos, aparece o no en el visualizador (Form1).
    // Ejecuta EYP_UNIGIS_verificar_folio en el servidor del establecimiento, el mismo que consulta el visualizador,
    // y muestra en el grid lo que devuelve (producto, unidad, cantidad, aparece, razon).
    public partial class frm_VerificarDocumento : Form
    {
        Qrys q = new Qrys();
        string usuario;

        // Establecimientos que no se verifican en este formulario
        static readonly string[] EstablecimientosExcluidos = { "7", "10", "40", "45" };

        // Prefijo con el que cada transaccion se sube a Unigis ("FC-SAA123"). Los Pick Up se suben como "PP-" con el folio que
        // se captura: el de la factura o remision (848FC/848RC, 30FC/30RC) o el de la orden de compra (30OC)
        static readonly Dictionary<string, string> PrefijoUnigis = new Dictionary<string, string>
        {
            { "29", "PE" }, { "35", "TE" }, { "713", "EdMaC" }, { "36", "FC" }, { "37", "RC" },
            { "848FC", "PP" }, { "848RC", "PP" }, { "30FC", "PP" }, { "30RC", "PP" }, { "30OC", "PP" }
        };

        // Numero de la consulta a Unigis vigente: si se verifica otro folio o se limpia antes de que Unigis conteste,
        // la respuesta anterior se descarta
        int consultaUnigis;

        // Tipos de documento que sabe verificar el SP. Para agregar otro: darlo de alta aqui y en el SP.
        class TipoDocumento
        {
            public string Transaccion;
            public string Abreviatura;
            public string Nombre;

            public TipoDocumento(string transaccion, string abreviatura, string nombre)
            {
                Transaccion = transaccion;
                Abreviatura = abreviatura;
                Nombre = nombre;
            }

            public override string ToString()
            {
                return "(" + Abreviatura + ") " + Nombre;
            }
        }

        public frm_VerificarDocumento(string user)
        {
            InitializeComponent();
            usuario = user;
        }

        private void frm_VerificarDocumento_Load(object sender, EventArgs e)
        {
            cmb_Transaccion.Items.Add(new TipoDocumento("29", "PE", "Pedido entre establecimientos"));
            cmb_Transaccion.Items.Add(new TipoDocumento("35", "TE", "Transferencia entre establecimientos"));
            cmb_Transaccion.Items.Add(new TipoDocumento("713", "EdMaC", "Entrega de mercancía a clientes"));
            cmb_Transaccion.Items.Add(new TipoDocumento("36", "FC", "Factura a cliente"));
            cmb_Transaccion.Items.Add(new TipoDocumento("37", "RC", "Remisión a cliente"));
            cmb_Transaccion.Items.Add(new TipoDocumento("848FC", "PP", "Pick Up Establecimiento - Factura"));
            cmb_Transaccion.Items.Add(new TipoDocumento("848RC", "PP", "Pick Up Establecimiento - Remisión"));
            cmb_Transaccion.Items.Add(new TipoDocumento("30FC", "PP", "Pick Up Proveedor - Factura"));
            cmb_Transaccion.Items.Add(new TipoDocumento("30RC", "PP", "Pick Up Proveedor - Remisión"));
            cmb_Transaccion.Items.Add(new TipoDocumento("30OC", "PP", "Pick Up Proveedor - Orden de compra"));
            cmb_Transaccion.SelectedIndex = -1;

            try
            {
                DataTable estab = q.catalogoEstabUsuario(usuario);
                foreach (DataRow row in estab.Rows)
                {
                    if (Excluido(row[1].ToString()))
                    {
                        continue;
                    }
                    cmb_Establecimiento.Items.Add(row[0].ToString());
                }
                if (cmb_Establecimiento.Items.Count == 1)
                {
                    cmb_Establecimiento.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible consultar los establecimientos. Verifique la conexión con el servidor e intente de nuevo." + Environment.NewLine + Environment.NewLine + ex.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_Verificar_Click(object sender, EventArgs e)
        {
            if (cmb_Establecimiento.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un establecimiento.", "Verificar Documento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string folio = NormalizarFolio(txt_Folio.Text);
            if (folio == null)
            {
                MessageBox.Show("Captura un folio válido.", "Verificar Documento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_Folio.Focus();
                return;
            }

            if (cmb_Transaccion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una transacción.", "Verificar Documento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmb_Transaccion.Focus();
                return;
            }

            string establecimiento = cmb_Establecimiento.SelectedItem.ToString();
            TipoDocumento tipo = (TipoDocumento)cmb_Transaccion.SelectedItem;
            string transaccion = tipo.Transaccion;

            dgv_Resultado.DataSource = null;
            LimpiarUnigis();
            btn_Verificar.Enabled = false;
            btn_Limpiar.Enabled = false;
            UseWaitCursor = true;
            bool enfocarFolio = false;

            try
            {
                DataTable resultado = await Task.Run(() => q.VerificarFolio(establecimiento, folio, transaccion));
                if (IsDisposed) return;

                // El documento no existe: el verificador solo devuelve la razon y se avisa como un folio invalido
                if (resultado.Columns.Contains("razon") && !resultado.Columns.Contains("estado") && resultado.Rows.Count > 0)
                {
                    MessageBox.Show(resultado.Rows[0]["razon"].ToString(), "Verificar Documento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    enfocarFolio = true;
                    return;
                }

                if (!resultado.Columns.Contains("razon") || !resultado.Columns.Contains("estado"))
                {
                    MessageBox.Show("El verificador de este establecimiento es de una versión anterior. Actualiza el procedimiento EYP_UNIGIS_verificar_folio.", "Verificar Documento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ColumnasCantidad(tipo.Transaccion);
                dgv_Resultado.DataSource = resultado;
                dgv_Resultado.ClearSelection();

                string prefijo;
                if (PrefijoUnigis.TryGetValue(transaccion, out prefijo))
                {
                    _ = MostrarUnigisAsync(prefijo + "-" + folio.ToUpperInvariant());
                }
            }
            catch (SqlException ex) when (ex.Number == 2812)
            {
                if (IsDisposed) return;
                MessageBox.Show("El verificador no está instalado en el servidor de este establecimiento.", "Verificar Documento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                if (IsDisposed) return;
                MessageBox.Show("No fue posible consultar el documento. Verifique la conexión con el servidor e intente de nuevo." + Environment.NewLine + Environment.NewLine + ex.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed)
                {
                    QuitarCursorEspera();
                    btn_Verificar.Enabled = true;
                    btn_Limpiar.Enabled = true;

                    if (enfocarFolio)
                    {
                        txt_Folio.Focus();
                    }
                    else
                    {
                        btn_Verificar.Focus();
                    }
                }
            }
        }

        // Regresa el formulario a su estado inicial: sin resultado, folio vacio, transaccion sin elegir
        // y establecimiento sin elegir (o el unico que tiene el usuario)
        private void btn_Limpiar_Click(object sender, EventArgs e)
        {
            dgv_Resultado.DataSource = null;
            LimpiarUnigis();
            ColumnasCantidad("");
            txt_Folio.Clear();
            cmb_Transaccion.SelectedIndex = -1;
            cmb_Establecimiento.SelectedIndex = cmb_Establecimiento.Items.Count == 1 ? 0 : -1;

            if (cmb_Establecimiento.Items.Count == 1)
            {
                txt_Folio.Focus();
            }
            else
            {
                cmb_Establecimiento.Focus();
            }
        }

        // La celda de Aparece se pinta segun su estado: verde si aparece, rojo si no (mas oscuro cuando esta seleccionada)
        private void dgv_Resultado_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != col_Aparece.Index)
            {
                return;
            }

            string valor = e.Value as string;
            if (valor == "Aparece")
            {
                e.CellStyle.BackColor = Color.SeaGreen;
                e.CellStyle.ForeColor = Color.White;
                e.CellStyle.SelectionBackColor = Color.DarkGreen;
                e.CellStyle.SelectionForeColor = Color.White;
            }
            else if (valor == "No aparece")
            {
                e.CellStyle.BackColor = Color.Red;
                e.CellStyle.ForeColor = Color.White;
                e.CellStyle.SelectionBackColor = Color.Firebrick;
                e.CellStyle.SelectionForeColor = Color.White;
            }
        }

        // Las dos ultimas columnas de cantidad dependen de la transaccion: en Pick Up Establecimiento (848) son lo entregado
        // en EdMaC y lo pendiente del vale; en Pick Up Proveedor (30), lo recibido con entradas y lo que falta por recibir;
        // en las demas, lo pendiente por embarcar y lo embarcado
        private void ColumnasCantidad(string transaccion)
        {
            if (transaccion.StartsWith("848"))
            {
                col_CantidadPe.DataPropertyName = "cantidad_ent";
                col_CantidadPe.HeaderText = "Cant. Ent.";
                col_CantidadEmb.DataPropertyName = "cantidad_pen";
                col_CantidadEmb.HeaderText = "Cant. Pen.";
            }
            else if (transaccion.StartsWith("30"))
            {
                col_CantidadPe.DataPropertyName = "cantidad_rec";
                col_CantidadPe.HeaderText = "Cant. Rec.";
                col_CantidadEmb.DataPropertyName = "cantidad_pen";
                col_CantidadEmb.HeaderText = "Cant. Pen.";
            }
            else
            {
                col_CantidadPe.DataPropertyName = "cantidad_pe";
                col_CantidadPe.HeaderText = "Cant. PE.";
                col_CantidadEmb.DataPropertyName = "cantidad_emb";
                col_CantidadEmb.HeaderText = "Cant. Emb.";
            }
        }

        // Recuadro UNIGIS: siempre visible, vacio hasta que se verifica un folio. Muestra lo que tiene Unigis del folio (estado
        // de la orden, jornada y ruta, viaje y parada). Se consulta despues de llenar el grid, sin bloquear el formulario
        private async Task MostrarUnigisAsync(string referencia)
        {
            int consulta = ++consultaUnigis;
            MostrarLineasUnigis(new[] { "Consultando UNIGIS…", "", "" });

            string[] lineas;
            try
            {
                lineas = await Task.Run(() => LineasUnigis(referencia));
            }
            catch (Exception ex)
            {
                lineas = new[] { "No se pudo consultar UNIGIS: " + Libreria.MensajeError(ex), "—", "—" };
            }

            if (IsDisposed || consulta != consultaUnigis)
            {
                return;
            }
            MostrarLineasUnigis(lineas);
        }

        // Deja el recuadro vacio y descarta la consulta a Unigis que siga en curso
        private void LimpiarUnigis()
        {
            consultaUnigis++;
            MostrarLineasUnigis(new[] { "", "", "" });
        }

        private void MostrarLineasUnigis(string[] lineas)
        {
            lbl_UnigisFolio.Text = lineas[0];
            lbl_UnigisJornada.Text = lineas[1];
            lbl_UnigisViaje.Text = lineas[2];
        }

        // Las tres lineas del recuadro: folio, jornada y viaje. Corre fuera del hilo de la pantalla
        private static string[] LineasUnigis(string referencia)
        {
            Libreria.OrdenUnigis pedido = Libreria.ConsultarOrdenUnigis(referencia);
            Libreria.OrdenEntregaUnigis orden = Libreria.ConsultarOrdenEntregaUnigis(referencia);
            if (pedido == null && orden == null)
            {
                return new[] { "No está en UNIGIS", "—", "—" };
            }

            // Pedido en Inicial: todavia no tiene orden, o es un folio reenviado y la orden que devuelve Unigis es la del envio
            // anterior hasta que se crea la nueva (al pasar a Planificar). Solo se muestra el pedido
            if (string.Equals(pedido?.Estado, "Inicial", StringComparison.OrdinalIgnoreCase))
            {
                return new[] { pedido.Estado + ConFecha(pedido.FechaEstado, "dd/MM/yyyy HH:mm"), "—", "—" };
            }

            // Pedido ya planificado sin orden (se elimino en Unigis): a Logistica no le sirve el estado del pedido
            if (orden == null)
            {
                return new[] { "Sin orden", "—", "—" };
            }

            string folio = orden.Estado + ConFecha(orden.FechaUltimoCambioEstado, "dd/MM/yyyy HH:mm") + " · Orden " + orden.IdOrden;
            string jornada = orden.IdJornada == 0
                ? "Sin jornada"
                : orden.IdJornada + ConFecha(orden.FechaJornada, "dd/MM/yyyy") + " · " + (orden.IdRuta > 0 ? "Ruta " + orden.IdRuta : "Sin ruta");

            // La parada mas reciente del folio puede ser de una orden anterior (folio reenviado): solo cuenta si es de esta orden
            string viaje;
            try
            {
                Libreria.ParadaUnigis parada = Libreria.ConsultarParadaUnigis(orden.RefDocumento);
                if (parada != null && parada.IdOrden == orden.IdOrden)
                {
                    viaje = parada.IdViaje + " · Parada " + parada.Secuencia + " · " + parada.Estado;
                }
                else
                {
                    viaje = orden.IdRuta > 0 ? "Sin viaje" : "—";
                }
            }
            catch (Exception ex)
            {
                viaje = "No se pudo consultar el viaje: " + Libreria.MensajeError(ex);
            }

            return new[] { folio, jornada, viaje };
        }

        // " (04/11/2026 09:23)" en hora local; vacio si Unigis no trae la fecha
        private static string ConFecha(DateTime? fechaUtc, string formato)
        {
            return fechaUtc.HasValue ? " (" + fechaUtc.Value.ToLocalTime().ToString(formato, CultureInfo.InvariantCulture) + ")" : "";
        }

        // Con UseWaitCursor = false Windows deja el cursor de espera hasta que se mueve el mouse. Asignar el cursor del formulario
        // le manda WM_SETCURSOR y lo cambia en ese momento
        private void QuitarCursorEspera()
        {
            UseWaitCursor = false;
            Cursor = Cursors.Default;
        }

        private static bool Excluido(string codEstab)
        {
            return Array.IndexOf(EstablecimientosExcluidos, (codEstab ?? "").Trim()) >= 0;
        }

        // El folio se captura solo, sin el prefijo de la transaccion. Se descartan los espacios del inicio y del final,
        // solo se toman los primeros 10 caracteres y se vuelve a recortar. Devuelve null si no es un folio valido.
        private static string NormalizarFolio(string texto)
        {
            string folio = (texto ?? "").Trim();
            if (folio.Length > 10)
            {
                folio = folio.Substring(0, 10).Trim();
            }

            return Regex.IsMatch(folio, "^[A-Za-z0-9]{1,10}$") ? folio : null;
        }
    }
}
