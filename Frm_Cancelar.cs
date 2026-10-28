using ActualizadorDoctosUnigis.Contoller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActualizadorDoctosUnigis
{
    public partial class Frm_Cancelar : Form
    {
        DataTable DTCE = new DataTable();
        // dt tiene un renglon por producto enviado (productos_por_embarcar en status 1) y dtDocs uno por documento, que es lo que se muestra
        DataTable dt;
        DataTable dtDocs;
        Dictionary<string, List<DataRow>> productosPorDocumento = new Dictionary<string, List<DataRow>>();
        // Columnas de SelecDocumentos que cambian por producto; las demas son iguales en todos los renglones del documento
        static readonly string[] ColumnasProducto = { "unidad", "cod_prod", "cantidad", "descripcion", "Status", "Seleccionado" };
        // Estados de una orden que sigue pendiente en Unigis (nace en "Inicial" o "Planificar" y pasa a "Planificado")
        static readonly HashSet<string> EstadosPendientesUnigis = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Inicial", "Planificar", "Planificado" };
        // Estados de una orden de entrega cuya ruta ya paso
        static readonly HashSet<string> EstadosFinalesEntrega = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Entregado", "No Entregado", "Entrega parcial" };
        Qrys q = new Qrys();
        string Estab = "";
        string cod_estab;
        string usuario;
        public Frm_Cancelar( string user)
        {
            InitializeComponent();
            usuario = user;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CurrentCellDirtyStateChanged += Libreria.AplicarCasillaAlMomento;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            Estab = comboBox1.SelectedItem.ToString();
            DataTable dCod = q.Cod_estab(comboBox1.SelectedItem.ToString());

            try
            {
                cod_estab = dCod.Rows[0].ItemArray[0].ToString();
                Recargar();
            }
            catch (Exception)
            {
                MessageBox.Show("Este estableciemiento no cuenta con informacion, Favor de Seleccionar otro ");
                comboBox1.Enabled = true;
            }
        }

        private void Recargar()
        {
            button2.Enabled = false;
            btn_Enviar.Enabled = false;
            bnt_Refrescar.Enabled = false;
            comboBox1.Enabled = false;
            LimpiarDocumentos();
            // La consulta tarda unos segundos; sin aviso los botones grises parecian un bloqueo
            label1.Text = "Consultando...";
            UseWaitCursor = true;

            string estab = cod_estab;
            Libreria.EnSegundoPlano(this, () =>
            {
                DataTable datos = q.SelecDocumentos(estab);
                datos.Columns.Add("Seleccionado", typeof(bool));
                foreach (DataRow row in datos.Rows)
                    row["Seleccionado"] = true;
                return datos;
            }, DocumentosCargados);
        }

        private void DocumentosCargados(DataTable datos, Exception error)
        {
            label1.Text = "Refrescar Documentos";
            UseWaitCursor = false;
            cmb_letra.Enabled = true;
            button2.Enabled = true;
            btn_Enviar.Enabled = true;
            bnt_Refrescar.Enabled = true;
            comboBox1.Enabled = true;

            if (error != null)
            {
                LimpiarDocumentos();
                MessageBox.Show("No fue posible consultar los documentos enviados. Verifique la conexión con el servidor e intente de nuevo." + Environment.NewLine + Environment.NewLine + Libreria.MensajeError(error), "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dt = datos;
            dtDocs = Libreria.AgruparPorDocumento(dt, "Documento", ColumnasProducto, productosPorDocumento);
            foreach (DataRow doc in dtDocs.Rows)
                ActualizarResumenDocumento(doc);

            Libreria.MostrarDocumentosEnCuadricula(dataGridView1, dtDocs);
            dataGridView1.Columns["Enviar"].HeaderText = "Cancelar";
            foreach (string columna in new[] { "transaccion", "Estab", "folio" })
            {
                if (dataGridView1.Columns.Contains(columna))
                    dataGridView1.Columns[columna].Visible = false;
            }
        }

        private void ActualizarResumenDocumento(DataRow doc)
        {
            List<DataRow> productos = productosPorDocumento[doc["Documento"].ToString()];
            int marcados = productos.Count(p => Convert.ToBoolean(p["Seleccionado"]));
            doc["Productos"] = marcados + " de " + productos.Count;
        }

        private void LimpiarDocumentos()
        {
            dt = null;
            dtDocs = null;
            productosPorDocumento = new Dictionary<string, List<DataRow>>();
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dataGridView1.Columns[e.ColumnIndex].Name != "Productos")
                return;

            DataRow doc = ((DataRowView)dataGridView1.Rows[e.RowIndex].DataBoundItem).Row;
            string documento = doc["Documento"].ToString();
            using (frm_ProductosDocumento frm = new frm_ProductosDocumento(documento, productosPorDocumento[documento]))
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                    ActualizarResumenDocumento(doc);
            }
        }

        // Lo que se hace con cada documento segun su orden en Unigis
        private class RevisionUnigis
        {
            public string Documento;
            public int IdPedido;
            public string Estado = "";
            public bool Eliminar;     // pendiente sin consolidar: se elimina el pedido en Unigis antes de cancelar
            public string Bloqueo;    // motivo para no cancelarlo; null si se puede cancelar
            public string EntregaTerminada;  // pedido pendiente otra vez cuya ruta ya paso: se cancela solo en el ERP
        }

        // Al consolidar, Unigis crea una orden de entrega (REF|01, o REF|00 en Pick Up) en una jornada, aparte del pedido. Esa
        // orden no se puede eliminar por API (EliminarOrdenesEntrega contesta 1 y no la borra), y si solo se borra el pedido se
        // queda huerfana; por eso el documento consolidado no se cancela hasta que eliminen la orden en Order Management (no
        // basta con desconsolidarla), y ya ruteado no se cancela (asi lo pide Proceso). Pedido pendiente (Inicial, Planificar o Planificado) sin consolidar: se
        // elimina en Unigis. Las rutas llevan ordenes de entrega, asi que sin orden no esta ruteado aunque diga Planificado: el
        // pedido se queda asi cuando eliminan su orden en Order Management (FC-GSQ66578). En otro estado, o si no existe, se
        // cancela solo en el ERP. Si Unigis no responde no se cancela, porque no se puede verificar.
        // Una orden de entrega ya entregada o no entregada no detiene la cancelacion: su ruta ya paso. Si el documento se reenvio
        // despues, el pedido vuelve a Inicial; si lo vuelven a rutear, Unigis regresa la orden de entrega nueva y se revisa como
        // cualquier otra. Si no (FC-GSQ66307 quedo en Planificado sin ruta ni orden nueva), se cancela solo en el ERP mientras no
        // este en una ruta que no haya terminado, y no se elimina en Unigis para no perder su entrega anterior (al reenviarlo se
        // actualiza el mismo pedido)
        private List<RevisionUnigis> RevisarEnUnigis(List<DataRow> productos)
        {
            Libreria.RutasUnigis rutas = new Libreria.RutasUnigis();
            List<RevisionUnigis> revisiones = new List<RevisionUnigis>();
            foreach (string documento in productos.Select(p => p["Documento"].ToString()).Distinct().ToList())
            {
                RevisionUnigis revision = new RevisionUnigis { Documento = documento };
                string referencia = documento.Trim();
                try
                {
                    Libreria.OrdenUnigis pedido = Libreria.ConsultarOrdenUnigis(referencia);
                    Libreria.OrdenEntregaUnigis entrega = Libreria.ConsultarOrdenEntregaUnigis(referencia);
                    string entregaTerminada = null;
                    if (entrega != null && EstadosFinalesEntrega.Contains(entrega.Estado))
                    {
                        entregaTerminada = (entrega.IdRuta > 0 ? "ruta " + entrega.IdRuta + " de la jornada " + entrega.IdJornada : "jornada " + entrega.IdJornada) + ", " + entrega.Estado;
                        entrega = null;
                    }
                    bool pendiente = (pedido != null && EstadosPendientesUnigis.Contains(pedido.Estado))
                                     || (entrega != null && EstadosPendientesUnigis.Contains(entrega.Estado));
                    if (pendiente && entrega != null)
                    {
                        revision.Bloqueo = entrega.IdRuta > 0
                            ? "está en la ruta " + entrega.IdRuta + " de la jornada " + entrega.IdJornada + " de Unigis (" + entrega.Estado + ")"
                            : "está consolidada en Unigis (orden " + entrega.IdOrden + ", jornada " + entrega.IdJornada + ", " + entrega.Estado
                              + "); elimine la orden " + entrega.IdOrden + " en Order Management de Unigis y vuelva a cancelar";
                    }
                    else if (pendiente)
                    {
                        revision.Estado = pedido.Estado;
                        revision.IdPedido = pedido.IdPedido;
                        string ruta = rutas.RutaDeOrden(pedido, referencia, entregaTerminada != null);
                        if (ruta != null)
                            revision.Bloqueo = "está en la " + ruta + " de Unigis";
                        else if (entregaTerminada != null)
                            revision.EntregaTerminada = entregaTerminada;
                        else
                            revision.Eliminar = true;
                    }
                }
                catch (Exception ex)
                {
                    revision.Bloqueo = "no se pudo consultar en Unigis (" + Libreria.MensajeError(ex) + ")";
                }
                revisiones.Add(revision);
            }
            return revisiones;
        }

        private static void AgregarLista(StringBuilder msg, string titulo, List<string> lineas)
        {
            if (lineas.Count == 0)
                return;

            msg.AppendLine(titulo);
            foreach (string linea in lineas.Take(15))
                msg.AppendLine("   " + linea);
            if (lineas.Count > 15)
                msg.AppendLine("   ... y " + (lineas.Count - 15) + " más");
            msg.AppendLine();
        }

        // Productos marcados de los documentos marcados
        private List<DataRow> ProductosACancelar()
        {
            List<DataRow> productos = new List<DataRow>();
            foreach (DataRow doc in dtDocs.Rows)
            {
                if (!Convert.ToBoolean(doc["Enviar"]))
                    continue;

                foreach (DataRow producto in productosPorDocumento[doc["Documento"].ToString()])
                {
                    if (Convert.ToBoolean(producto["Seleccionado"]))
                        productos.Add(producto);
                }
            }
            return productos;
        }

        private void Frm_Cancelar_Load(object sender, EventArgs e)
        {
            DataTable DTCE = new DataTable();
            DTCE = q.catalogoEstab();
            foreach (DataRow row in DTCE.Rows)
            {
                comboBox1.Items.Add(row[0].ToString());
            }

        }

        private void bnt_Refrescar_Click(object sender, EventArgs e)
        {
            Recargar();
        }

        private void btn_Enviar_Click(object sender, EventArgs e)
        {
            if (dtDocs == null)
                return;

            dataGridView1.EndEdit();
            List<DataRow> productos = ProductosACancelar();
            if (productos.Count == 0)
            {
                MessageBox.Show("No hay documentos seleccionados para cancelar.", "Cancelar Documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<RevisionUnigis> revisiones;
            Cursor = Cursors.WaitCursor;
            try
            {
                revisiones = RevisarEnUnigis(productos);
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            List<RevisionUnigis> bloqueadas = revisiones.Where(r => r.Bloqueo != null).ToList();
            List<RevisionUnigis> aEliminar = revisiones.Where(r => r.Bloqueo == null && r.Eliminar).ToList();
            productos.RemoveAll(p => bloqueadas.Any(b => b.Documento == p["Documento"].ToString()));

            StringBuilder aviso = new StringBuilder();
            AgregarLista(aviso, "No se cancelan " + bloqueadas.Count + " documentos:", bloqueadas.Select(b => b.Documento.Trim() + ": " + b.Bloqueo).ToList());
            if (productos.Count == 0)
            {
                MessageBox.Show(aviso.ToString().TrimEnd(), "Cancelar Documentos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            AgregarLista(aviso, "Siguen pendientes en Unigis sin consolidar; se eliminará su orden en Unigis:", aEliminar.Select(r => r.Documento.Trim() + " (" + r.Estado + ")").ToList());
            AgregarLista(aviso, "Su ruta en Unigis ya terminó, pero Unigis los sigue mostrando pendientes porque se reenviaron; se cancelan solo en el ERP:",
                revisiones.Where(r => r.Bloqueo == null && r.EntregaTerminada != null).Select(r => r.Documento.Trim() + " (" + r.EntregaTerminada + "; pedido en " + r.Estado + ")").ToList());

            int documentos = productos.Select(p => p["Documento"].ToString()).Distinct().Count();
            aviso.Append("Se va a cancelar el envío de " + productos.Count + " productos de " + documentos + " documentos; volverán a quedar pendientes en Envío de Documentos." + Environment.NewLine + Environment.NewLine + "¿Desea continuar?");
            DialogResult result1 = MessageBox.Show(aviso.ToString(), "Cancelar Documentos", MessageBoxButtons.YesNo, bloqueadas.Count > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Question);
            if (result1 != DialogResult.Yes)
                return;

            List<string> errores = new List<string>();
            List<string> eliminadas = new List<string>();
            Cursor = Cursors.WaitCursor;
            try
            {
                // 1. Primero Unigis: si no se puede eliminar la orden, ese documento no se cancela en el ERP. Unigis contesta igual
                // aunque no la elimine, asi que se vuelve a consultar
                foreach (RevisionUnigis revision in aEliminar)
                {
                    string referencia = revision.Documento.Trim();
                    try
                    {
                        Libreria.EliminarOrdenUnigis(referencia, revision.IdPedido);
                        Libreria.OrdenUnigis despues = Libreria.ConsultarOrdenUnigis(referencia);
                        if (despues != null)
                            throw new Exception("Unigis la sigue mostrando en " + despues.Estado);
                        eliminadas.Add(referencia);
                    }
                    catch (Exception ex)
                    {
                        errores.Add(referencia + ": no se pudo eliminar la orden en Unigis y no se canceló (" + Libreria.MensajeError(ex) + ")");
                        productos.RemoveAll(p => p["Documento"].ToString() == revision.Documento);
                    }
                }

                // 2. Despues el ERP. Si aqui falla un documento cuya orden ya se elimino, al reintentar ya no existe en Unigis
                // y se cancela solo en el ERP
                foreach (DataRow producto in productos)
                {
                    try
                    {
                        q.UpdatePPE(producto["cod_prod"].ToString(), producto["folio"].ToString(), producto["transaccion"].ToString(), cod_estab, "0", usuario);
                    }
                    catch (Exception ex)
                    {
                        bool ordenEliminada = aEliminar.Any(r => r.Documento == producto["Documento"].ToString());
                        errores.Add(producto["Documento"].ToString().Trim() + " producto " + producto["cod_prod"].ToString().Trim() + ": " + Libreria.MensajeError(ex)
                                    + (ordenEliminada ? " (la orden ya se eliminó en Unigis; vuelva a cancelar el documento)" : ""));
                    }
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            StringBuilder resumen = new StringBuilder();
            if (errores.Count == 0)
                resumen.AppendLine("Se han cancelado los documentos.").AppendLine();
            AgregarLista(resumen, "Órdenes eliminadas en Unigis:", eliminadas);
            AgregarLista(resumen, "No se pudo cancelar:", errores);
            MessageBox.Show(resumen.ToString().TrimEnd(), "Cancelar Documentos", MessageBoxButtons.OK, errores.Count == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            Recargar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dtDocs == null)
                return;

            dataGridView1.EndEdit();
            foreach (DataRow doc in dtDocs.Rows)
                doc["Enviar"] = !Convert.ToBoolean(doc["Enviar"]);
        }

        private void cmb_letra_SelectedIndexChanged(object sender, EventArgs e)
        {
            int size = int.Parse(cmb_letra.Text);


            // SetMinMax(dataGridView1, textBox1.Text);

            dataGridView1.Font = new Font(dataGridView1.Font.Name, size, dataGridView1.Font.Style);
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DataGridViewColumn col = new DataGridViewColumn();
            col.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }
    }
}
