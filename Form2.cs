using ActualizadorDoctosUnigis.Models;
using CrystalDecisions.ReportSource ;
using CrystalDecisions.CrystalReports.Engine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
 
using ActualizadorDoctosUnigis.Contoller;
 

namespace ActualizadorDoctosUnigis
{
    public partial class Form2 : Form
    {
        private TextWriter obtenerRuta;
        ObtenerRutaCompleta_Response.ObtenerRutaCompletaResponse orcr = new ObtenerRutaCompleta_Response.ObtenerRutaCompletaResponse();
        Qrys Q = new Qrys();
        DataTable DAux = new  DataTable();
        Libreria lib = new Libreria();
        string nusuario;
        string usuario;
        string estab_unigis;
        string nivel;
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(string n,string usuarioN,string Niv)
        {
            InitializeComponent();
            usuario = usuarioN;
            nusuario = n;
            nivel = Niv;
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(Control_KeyPress);
            this.WindowState= System.Windows.Forms.FormWindowState.Maximized;
        }
    

        private void Form2_Load(object sender, EventArgs e)
        {
            resize();
            if (nivel.Trim() == ("39") || nivel.Trim()== "1" ||   nivel.Trim() == "31")
            {
                gbx_2.Visible = true;
            }
            if (Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "TOPF2").ToString() != "")
            {
                this.Top = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "TOPF2").ToString());
                this.Left = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "LeftF2").ToString());
                this.Width = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "WidthF2").ToString());
                this.Height = Convert.ToInt32(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "HeightF2").ToString());
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            cargar();
        }
        public void refrescar(ObtenerRutaCompleta_Response.ObtenerRutaCompletaResponse orcr2)
        {
            dgv_Direcciones.DataSource = Direcciones(orcr2);
            dgv_Direcciones.Columns["Direccion"].Width = 200;
            dgv_Direcciones.Columns["Cliente"].Width = 250;
            dataGridView1.DataSource = Productos(orcr2); 
            
            dataGridView1.Columns["Producto"].Width = 200;
            txt_fecha.Text = orcr.d.FechaHoraSalida.Value.ToString("dd-MM-yyyy");
            txt_peso.Text = orcr.d.Peso.ToString();
            txt_estado.Text = orcr.d.Estado;
          
            //txt_estado.Text = orcr.d.CantidadOrdenes.ToString();
            if (dataGridView1.Rows.Count > 0)
               {
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    if (col.Index == 9 || col.Name == "Cantidad" || col.Name== "Motivo")
                        col.ReadOnly = false;
                    else
                        col.ReadOnly = true;
                    //Si la estableces a true la columna no será editable col.ReadOnly = true;
                    //Debes hacer exactamente lo                 contrario, establecerla a false.

                }
            }
            dataGridView1.Columns["Domicilio"].Visible = false;
            dataGridView1.Columns["Fecha"].Visible = false;
            dataGridView1.Columns["Jornada"].Visible = false;
            dataGridView1.Columns["Ruta"].Visible = false;
            dataGridView1.Columns["Nombre"].Visible = false;
            obtenerViaje(orcr2.d.IdViaje);
            ValidarMotivo(dataGridView1);
            dataGridView1.Columns["Motivo"].Width = 200;
            dataGridView1.Sort(dataGridView1.Columns["Orden"], ListSortDirection.Ascending);

        }

        public DataTable Direcciones(ObtenerRutaCompleta_Response.ObtenerRutaCompletaResponse orc)
        {

            DataTable dt = new DataTable();

            DataColumn column;
            DataRow row;

            // Se tiene que crear primero la columna asignandole Nombre y Tipo de datos    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Documento";

            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Cliente";
            dt.Columns.Add(column);
            // Se tienen que crear todas las columnas que queramos
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Direccion";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Latitud";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Longitud";
            dt.Columns.Add(column);
            
          


            // Se crea una fila por cada registro que necesitemos agregar    
            for (int x = 0; x < orc.d.Ordenes.Count; x++)
            {
                row = dt.NewRow();
                row["Documento"] = (orc.d.Ordenes[x].RefDocumento.ToString());
                row["Cliente"] = (orc.d.Ordenes[x].Cliente.RefCliente.ToString().TrimEnd() + "-" + orc.d.Ordenes[x].Descripcion.ToString());
                row["Direccion"] = (orc.d.Ordenes[x].Direccion.ToString());

                row["Latitud"] = (orc.d.Ordenes[x].Latitud.ToString()); ;

                row["Longitud"] = (orc.d.Ordenes[x].Longitud.ToString());
                dt.Rows.Add(row);

            }
            return dt;
        }

        public DataTable Productos(ObtenerRutaCompleta_Response.ObtenerRutaCompletaResponse orc)
        {

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            DataColumn column;
            DataRow row;

            // Se tiene que crear primero la columna asignandole Nombre y Tipo de datos    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Documento";
            dt.Columns.Add(column);
             
            // Se tienen que crear todas las columnas que queramos
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Producto";
            dt.Columns.Add(column);
             

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Cantidad";
            

            dt.Columns.Add(column); column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "Peso";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Cliente";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Nombre";
            dt.Columns.Add(column);



            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Domicilio";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Fecha";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Jornada";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Ruta";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.Int16");
            column.ColumnName = "Orden";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Tipo_documento";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "IMPORTE";
            dt.Columns.Add(column);
 
            

            string tipo_documento = "";
          
                

            // Se crea una fila por cada registro que necesitemos agregar    
            for (int x = 0; x < orc.d.Ordenes.Count; x++)
            { 
                string sqlD = "select cp.nombre,  f.total - f.abonos as Pendiente " +
                    " from facremtick f with (nolock) " +
                    " left join condiciones_pago cp with (nolock) on f.cond_pago = cp.condicion_pago " +
                    " left join movimientos_internos mi with (nolock) on mi.folio_referencia = f.folio and mi.transaccion_referencia = f.transaccion " +
                    " where(f.folio = '"+ lib.folio(orc.d.Ordenes[x].RefDocumento.ToString(), 0) + "' and f.transaccion = '"+ lib.folio(orc.d.Ordenes[x].RefDocumento.ToString(), 1) + "') or(mi.transaccion = '"+ lib.folio(orc.d.Ordenes[x].RefDocumento.ToString(), 1) + "' and mi.folio = '"+ lib.folio(orc.d.Ordenes[x].RefDocumento.ToString(), 0) + "')";

                DataTable de = new DataTable();
                if (lib.folio(orc.d.Ordenes[x].RefDocumento.ToString(),1) != "35")
                    {
                      de = Q.Consultar(sqlD, obtener_estab(orc.d.Ordenes[x].DepositoSalida.RefDepositoExterno));
                }


               
                int contadoritems = 0;
                if (orc.d.Ordenes[x].Items==null) { contadoritems = 0; }
                contadoritems = orc.d.Ordenes[x].Items.Count;
                for (int y = 0; y < contadoritems; y++)
                {
                    string w = lib.Decimales(orc.d.Ordenes[x].Items[y].Cantidad.ToString(), orc.d.Ordenes[x].Items[y].RefDocumento.ToString().TrimEnd());
                    string z = lib.Decimales(orc.d.Ordenes[x].Items[y].Cantidad.ToString(), orc.d.Ordenes[x].Items[y].RefDocumento.ToString().TrimEnd());

                    row = dt.NewRow();
                    row["Documento"] = (orc.d.Ordenes[x].RefDocumento.ToString());

                    row["Producto"] = (orc.d.Ordenes[x].Items[y].RefDocumento.ToString().TrimEnd() + "-" + orc.d.Ordenes[x].Items[y].Descripcion.ToString());

                    if (orc.d.Ordenes[x].Items[y].Cantidad.ToString() != "0")
                    {
                        row["Cantidad"] = lib.Decimales(orc.d.Ordenes[x].Items[y].Cantidad.ToString(), orc.d.Ordenes[x].Items[y].RefDocumento.ToString().TrimEnd());
                    }
                    else
                    {
                        //string x= lib.Decimales(orc.d.Ordenes[x].Items[y].Bulto.ToString(), orc.d.Ordenes[x].Items[y].RefDocumento.ToString().TrimEnd());
                        row["Cantidad"] = lib.Decimales(orc.d.Ordenes[x].Items[y].Bulto.ToString(), orc.d.Ordenes[x].Items[y].RefDocumento.ToString().TrimEnd());
                    }

                    row["Peso"] = Convert.ToDouble( orc.d.Ordenes[x].Items[y].Peso.ToString());
                
                    row["Cliente"] = (orc.d.Ordenes[x].Cliente.RefCliente.ToString().TrimEnd() + "-" + orc.d.Ordenes[x].Descripcion.ToString());
                    row["Domicilio"] = (orc.d.Ordenes[x].Direccion.ToString());
                    row["Fecha"] = orcr.d.FechaHoraSalida.Value.ToString("dd-MM-yyyy");
                    row["Jornada"] = orcr.d.IdJornada.ToString();
                    row["Ruta"] = orcr.d.IdRuta.ToString();
                    row["Nombre"] = usuario;
                    row["Orden"] = (orc.d.Ordenes[x].Orden);

                    //MessageBox.Show(lib.folio(orc.d.Ordenes[x].RefDocumento.ToString(), 1));
                    if (lib.folio(orc.d.Ordenes[x].RefDocumento.ToString(), 1) == "35" || lib.folio(orc.d.Ordenes[x].RefDocumento.ToString(), 1) == "" )
                    {
                        row["Tipo_documento"] = "";
                        row["Importe"] = 0;
                    }
                    else
                    {
                            row["Tipo_documento"] = de.Rows[0].ItemArray[0].ToString();
                        row["Importe"] = Convert.ToDouble(de.Rows[0].ItemArray[1].ToString());
                    }
                    dt.Rows.Add(row);
                }

            }


            dt.DefaultView.Sort = "Orden";
            dt = dt.DefaultView.ToTable(true);

            return dt;  
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btn_imp.Enabled = true;
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Leave(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e )
        {
            //MessageBox.Show(Directory.GetCurrentDirectory() + "/R1534-UNIGIS.rpt");
             ReportDocument rp = new  ReportDocument();
            //MessageBox.Show(Directory.GetCurrentDirectory() + "/R1534.rpt") ;
          rp.Load(Directory.GetCurrentDirectory()+ "/R1534-Unigis-Preruta.rpt");
            StringBuilder qry = new StringBuilder();
            DataColumn column = new DataColumn();


            if (ContainColumn("Producto", DAux))
            { DAux.Columns["Producto"].ColumnName = "Cod_Prod";}
            if (ContainColumn("Documento", DAux)) { DAux.Columns["Documento"].ColumnName = "Folio"; }
            if (ContainColumn("Jornada", DAux)) { DAux.Columns["Jornada"].ColumnName = "notas"; }
            if (ContainColumn("ruta", DAux)) { DAux.Columns["ruta"].ColumnName = "notasEmbarque"; }
            rp.SetDataSource(DAux);
            Impresion imp = new Impresion(rp);
            imp.Show(); 




         }

        public void eliminarProd(ObtenerRutaCompleta_Response.ObtenerRutaCompletaResponse orc, string Doc, string prod)
        {
            ////eliminar seleccionado 
            ////int cont = orc.d.Ordenes.Count;
            ////for (int x = 0; x < cont; x++)
            ////{
            ////    int cont2 = orc.d.Ordenes[x].Items.Count;   
            ////    for (int y = 0; y < cont2; y++)
            ////    {
                  
            ////        if (orc.d.Ordenes[x].RefDocumento.ToString() == Doc && orc.d.Ordenes[x].Items[y].RefDocumento.ToString().TrimEnd() == prod)
            ////        {
            ////            orc.d.Ordenes[x].Items.RemoveAt(y);

            ////            if (orc.d.Ordenes[x].Items.Count == 0)
            ////                {
            ////                orc.d.Ordenes.RemoveAt(x);
                            
            ////                }
            ////        }
            ////    }
            //}

            //dataGridView1.DataSource = Productos(orcr);
            //recalcularPYC(orcr);
        }
        public void recalcularPYC(ObtenerRutaCompleta_Response.ObtenerRutaCompletaResponse orc)
        {
            //orc.d.Ordenes[].Peso
            //orc.d.Ordenes[].Bulto
            //    orc.d.Peso
            //    orc.d.Bultos
            decimal pesototal=0;
            int bultosT = 0;
            for (int x = 0; x < orc.d.Ordenes.Count; x++)
            {
                decimal peso= 0;
                int bultos=0;
                for (int y = 0; y < orc.d.Ordenes[x].Items.Count; y++)
                {
                    peso = peso + Convert.ToDecimal(orc.d.Ordenes[x].Items[y].Peso);
                    bultos= bultos+ Convert.ToInt16(orc.d.Ordenes[x].Items[y].Bulto);
                }
                orc.d.Ordenes[x].Peso = peso;
                orc.d.Ordenes[x].Bulto = bultos;
                orc.d.CantidadOrdenes = orc.d.Ordenes.Count;
                pesototal = pesototal + peso;
                bultosT = bultosT + bultos;
            }
            orc.d.Peso = pesototal;
            orc.d.Bultos = bultosT;
            refrescar(orc);
         
           

        }

        private void btn_Editar_Click(object sender, EventArgs e)
        {
            int flag = 0;
            for (int i = 0; i < orcr.d.Ordenes.Count; i++)
            {
                if ( orcr.d.Ordenes[i].Estado.ToUpper() == "ENTREGADO" || orcr.d.Ordenes[i].Estado.ToUpper() == "NO ENTREGADO" || orcr.d.Ordenes[i].Estado.ToUpper() == "ENTREGA PARCIAL")
                {
                    flag = flag + 1;
                }
            }

            if (flag == 0)
            {
                dataGridView1.Enabled = true;
                dgv_Direcciones.Enabled = true;
                btn_guardar.Enabled = true;
                btn_Editar.Enabled = false; }
            else { MessageBox.Show("No se puede editar Ordenes con estatus PLANIFICADO||ENTREGADO||NO ENTREGADO||ENTREGA PARCIAL "); }
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            btn_Editar.Enabled = true;
            btn_guardar.Enabled = false;

        
            GuardarModificacion(dataGridView1, orcr);

        }

        public void GuardarModificacion(DataGridView dgv , ObtenerRutaCompleta_Response.ObtenerRutaCompletaResponse orc)
        {
            string[] prod;
                
                for (int x = 0; x < orc.d.Ordenes.Count; x++)
            {
                decimal peso = 0;
                int bultos = 0;
                for (int y = 0; y < orc.d.Ordenes[x].Items.Count; y++)
                {
                    foreach (DataGridViewRow item in dataGridView1.Rows)
                    {

                        prod = item.Cells["Producto"].Value.ToString().Split('-');
                     
                        if (prod[0].ToString() == orc.d.Ordenes[x].Items[y].RefDocumento.ToString() && item.Cells["Documento"].Value.ToString()==orc.d.Ordenes[x].RefDocumento.ToString() )
                        {
                            double pu = orc.d.Ordenes[x].Items[y].Peso / orc.d.Ordenes[x].Items[y].Bulto;
                            if (Double.IsNaN(pu))
                            {
                                pu = 0;
                            }
                            orc.d.Ordenes[x].Items[y].Bulto = Convert.ToDouble(item.Cells["Cantidad"].Value.ToString());
                            // orc.d.Ordenes[x].Items[y].Bulto = Convert.ToInt32(item.Cells[2].Value.ToString());
                            orc.d.Ordenes[x].Items[y].Cantidad = Convert.ToDouble(item.Cells["Cantidad"].Value.ToString());
                            orc.d.Ordenes[x].Items[y].Peso = Convert.ToDouble(item.Cells["Cantidad"].Value.ToString()) * pu;
                            // MessageBox.Show((Convert.ToDouble(item.Cells["Cantidad"].Value.ToString()) * pu).ToString());
                            orc.d.Ordenes[x].Items[y].IdOrdenItem.ToString();
                        }
                     }
                }
                    
            }
             
            orcr = orc;
            //recalcularPYC(orc);
            xmlwriterOrden xmlw = new xmlwriterOrden();
            DataTable dtGX = dataGridView1.DataSource as DataTable;
            dtGX.DefaultView.Sort = "ORDEN";
            dtGX = dtGX.DefaultView.ToTable();

            DAux.DefaultView.Sort = "ORDEN";
            DAux = DAux.DefaultView.ToTable();

            for (int x = 0; x < orcr.d.Ordenes.Count; x++)
            {
                if (orcr.d.Ordenes[x].Estado == "Inicial")
                {
                    xmlw.stringtoxmlMOrden_INICIAL(orcr.d.Ordenes[x]);
                }

            }




            if (ValidarCantidades(DAux, dtGX))
            {
                List<string> xmls = xmlw.stringtoxmlMOrden(orcr);

                




                try
                {
                    for (int i = 0; i < xmls.Count; i++)
                    {
                        using (var client = new HttpClient())
                        {
                            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3;
                            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                            var endpoint = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx?op=ModificarEstadoOrdenEntrega");
                            //var result = client.GetAsync(endpoint).Result;
                            //var xmlR = result.Content.ReadAsStringAsync().Result;


                            var paylod = new StringContent(xmls[i], Encoding.UTF8, "text/xml");
                             var result = client.PostAsync(endpoint, paylod).Result.Content.ReadAsStringAsync().Result;



                            btn_Editar.Enabled = true;
                            btn_guardar.Enabled = false;
                         }
                    }

                    DAux = dataGridView1.DataSource as DataTable;
                    DAux.DefaultView.Sort = "Orden";
                    DAux = DAux.DefaultView.ToTable();

               



                    MessageBox.Show("Actualizacion Correcta");
                    Q.ActualizarRuta( txt_Jornada.Text, txt_Ruta.Text, nusuario);
                    CrystalDecisions.CrystalReports.Engine.ReportDocument rp =  new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    rp.Load(Directory.GetCurrentDirectory() + "/R1534-UNIGIS.rpt");
                    StringBuilder qry = new StringBuilder();
                    DataColumn column = new DataColumn();

                    if (ContainColumn("Producto", DAux))
                    { DAux.Columns["Producto"].ColumnName = "Cod_Prod"; }
                    if (ContainColumn("Documento", DAux)) { DAux.Columns["Documento"].ColumnName = "Folio"; }
                    if (ContainColumn("Jornada", DAux)) { DAux.Columns["Jornada"].ColumnName = "notas"; }
                    if (ContainColumn("ruta", DAux)) { DAux.Columns["ruta"].ColumnName = "notasEmbarque"; }
                    rp.SetDataSource(DAux);
                    Impresion imp = new Impresion(rp);
                    imp.Show();
                    limpiar();
                    
                }


                catch (Exception e)
                {
                    MessageBox.Show("Actualización Incorrecta  Error: " + e.Message);
                }
            }

            }

        private void txt_Jornada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)  )
            {
                e.Handled = true;
            }
        }

        private void txt_Ruta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)  )
            {
                
                e.Handled = true;
            }
        }

        private void txt_Ruta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F2")
            {
               // GridBusqueda gb = new GridBusqueda(txt_Jornada, nusuario, txt_Ruta);
                GridBusqueda gb = new GridBusqueda(txt_Jornada, nusuario, txt_Ruta);
                gb.Show();
           
            }
            if (e.KeyCode == Keys.Enter){

                if (txt_Jornada.Text.Trim() == "" || txt_Ruta.Text.Trim() == "")
                {
                    MessageBox.Show("Favor de Llenar la informacion en los campos IdJornada y IdRuta", "Verificar Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    cargar();
                }
            }
        }

        private void txt_Jornada_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void CrearEmbarque(ObtenerRutaCompleta_Response.ObtenerRutaCompletaResponse orcr)
        {

            MessageBox.Show(orcr.d.FechaHoraSalida.ToString());
            MessageBox.Show(orcr.d.FechaHoraSalida.ToString());
             

        }
        public void obtenerViaje(int idviaje)
        {
            ObtenerViajeID orc = new ObtenerViajeID();
            orc.ApiKey = 1234;
            orc.IdViaje = 48;



            string jsonString = JsonConvert.SerializeObject(orc);
            ObtenerViaje_Response.Rootobject OVR = new ObtenerViaje_Response.Rootobject();
            //MessageBox.Show(xml.stringtoxml(aux));
            //  string msj = xml.stringtoxml(aux).ToString();
            using (var client = new HttpClient())
            {
            
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                var endpoint = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx/ConsultarViaje");
                //var result = client.GetAsync(endpoint).Result;
                //var xmlR = result.Content.ReadAsStringAsync().Result;

                var paylod = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var result = client.PostAsync(endpoint, paylod).Result.Content.ReadAsStringAsync().Result;
                // MessageBox.Show(result);
                OVR = JsonConvert.DeserializeObject<ObtenerViaje_Response.Rootobject>(result);
                //orcr = JsonConvert.DeserializeObject<ObtenerRutaCompleta_Response.ObtenerRutaCompletaResponse>(result);




            }
         
        }

        public void ValidarMotivo(DataGridView dgv)
        {
            bool Flag = true;
            foreach(DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.Name == "Motivo")
                {
                    Flag = false;
                }
            }
            

            if (Flag)
            {
                DataGridViewComboBoxColumn dcom = new DataGridViewComboBoxColumn();
               // dcom.HeaderText = "Motivo";
                dcom.Name = "Motivo";
                dcom.Items.Add("");
                DataTable dt = Q.Motivos("10");
                foreach (DataRow row in dt.Rows)
                {
                    dcom.Items.Add(row[0]);
                }
                dgv.Columns.Add(dcom);
            }
            else { 
                return; }

        }
        private bool ContainColumn(string columnName, DataTable table)
        {
            DataColumnCollection columns = table.Columns;
            if (columns.Contains(columnName))
            {
                return true;
            }
            return false;
        }

    

        public void resize( )
        { 
            panel1.Width = this.Width;
            panel1.Height = this.Height;
            dataGridView1.Width = Convert.ToInt16(this.Width * 0.62144);
            dgv_Direcciones.Width = Convert.ToInt32(this.Width * 0.7956);
            txt_Jornada.Width = Convert.ToInt32(this.Width * 0.09165);
            txt_fecha.Width = Convert.ToInt32(this.Width * 0.09165);
            txt_estado.Width = Convert.ToInt32(this.Width * 0.09165);
            txt_Ruta.Width = Convert.ToInt32(this.Width * 0.09165);
            double pad = 0.02474 * this.Width;
            label2.Location = new System.Drawing.Point(Convert.ToInt32(pad) + txt_Jornada.Location.X + txt_Jornada.Width, label2.Location.Y);
            pad = 0.01283 * this.Width;
            txt_fecha.Location = new System.Drawing.Point(Convert.ToInt32(pad) + label2.Location.X + label2.Width, txt_fecha.Location.Y);
            pad = 0.02474 * this.Width;
            label5.Location = new System.Drawing.Point(Convert.ToInt32(pad) + txt_Jornada.Location.X + txt_Jornada.Width, label5.Location.Y);
            pad = 0.01283 * this.Width;
            txt_estado.Location = new System.Drawing.Point(Convert.ToInt32(pad) + label5.Location.X + label5.Width, txt_estado.Location.Y);
            //Incrementar Height
            dataGridView1.Height = Convert.ToInt16(this.Height * 0.315181);
            dgv_Direcciones.Height = Convert.ToInt16(this.Height * 0.26897);
            label3.Location = new System.Drawing.Point(label3.Location.X, dataGridView1.Height + dataGridView1.Location.Y + 20);
            dgv_Direcciones.Location = new System.Drawing.Point(dgv_Direcciones.Location.X, label3.Height + label3.Location.Y + 10);
        }
        private void Form2_ResizeEnd(object sender, EventArgs e)
        {
            resize();
        }

        private void Form2_SizeChanged(object sender, EventArgs e)
        {
            resize();
        }

        private void txt_Ruta_TextChanged(object sender, EventArgs e)
        {

        }


        public bool ValidarCantidades(DataTable DA, DataTable DG)
        {
            string msg = "";
            List<string> insert= new List<string>();
            //MessageBox.Show( dataGridView1.Rows[0].Cells[9].Value.ToString()); 
            for (int x = 0; x < DA.Rows.Count; x++)
            {
                if (DA.Rows[x]["Cantidad"].ToString() != DG.Rows[x]["Cantidad"].ToString())
                {

                    if (Convert.ToDouble(DG.Rows[x]["Cantidad"].ToString()) < Convert.ToDouble(DA.Rows[x]["Cantidad"].ToString()))
                    {
                        if(dataGridView1.Rows[x].Cells["Motivo"].Value  ==null)
                        msg = msg + ("Cantidades Diferentes del producto " + DG.Rows[x][1].ToString() + " Cantidad Programada : " + DA.Rows[x]["Cantidad"].ToString() + " Cantidad Modificada : " + DG.Rows[x]["Cantidad"].ToString()+Environment.NewLine + "Favor de Seleccionar Motivo"+ Environment.NewLine+ Environment.NewLine);

                        if (dataGridView1.Rows[x].Cells["Motivo"].Value != null)
                        {
                            //MessageBox.Show(dataGridView1.Rows[x].Cells["Motivo"].Value.ToString(),usuario);
                            //  dataGridView1.Rows[x].Cells["Motivo"]  ;
                            string motivo=dataGridView1.Rows[x].Cells["Motivo"].Value.ToString().Substring(0, dataGridView1.Rows[x].Cells["Motivo"].Value.ToString().IndexOf('|'));
                            //MessageBox.Show(lib.folio(dataGridView1.Rows[x].Cells["Documento"].Value.ToString(),0));
                            //MessageBox.Show(lib.folio(dataGridView1.Rows[x].Cells["Documento"].Value.ToString(),1));
                            //MessageBox.Show( dataGridView1.Rows[x].Cells["Ruta"].Value.ToString() );
                            //MessageBox.Show(dataGridView1.Rows[x].Cells["Jornada"].Value.ToString());
                            //MessageBox.Show(Convert.ToDouble(DG.Rows[x]["Cantidad"].ToString()) + "  " + Convert.ToDouble(DA.Rows[x]["Cantidad"].ToString()));
                            // MessageBox.Show(Convert.ToDouble(DG.Rows[x]["Peso"].ToString()) + "  " + Convert.ToDouble(DA.Rows[x]["Peso"].ToString()));
                            //MessageBox.Show(dataGridView1.Rows[x].Cells["Producto"].Value.ToString().Substring(0, dataGridView1.Rows[x].Cells["Producto"].Value.ToString().IndexOf('-')));
                            

                            string sql = "Insert into Historico_unigis (Movimiento  ,Documento  ,Transaccion ,Cod_prod ,cantidad_anterior ,Cantidad , peso_anterior ,Peso ,Cliente ,Razon, usuario ,fecha )" +
                                        "Values ('ORDEN','" + lib.folio(dataGridView1.Rows[x].Cells["Documento"].Value.ToString(), 0) + "','" + lib.folio(dataGridView1.Rows[x].Cells["Documento"].Value.ToString(), 1) + "','"+ dataGridView1.Rows[x].Cells["Producto"].Value.ToString().Substring(0, dataGridView1.Rows[x].Cells["Producto"].Value.ToString().IndexOf('-')) + "'," +
                                        ""+  DA.Rows[x]["Cantidad"].ToString() + " , "+ DG.Rows[x]["Cantidad"].ToString() + ","+ DA.Rows[x]["Peso"].ToString() + ","+  DG.Rows[x]["Peso"].ToString() + ",'"+ dataGridView1.Rows[x].Cells["Cliente"].Value.ToString().Substring(0, dataGridView1.Rows[x].Cells["Cliente"].Value.ToString().IndexOf('-')) + "'," +
                                        "'"+motivo+"','500','"+DateTime.Now.ToString("yyy-MM-dd HH:mm:ss")+"')";
                            insert.Add(sql);
                        }
                    }
                    if (Convert.ToDouble(DG.Rows[x]["Cantidad"].ToString()) > Convert.ToDouble(DA.Rows[x]["Cantidad"].ToString())) {
                        msg = msg + ("Documento con Cantidades mayores a las programdas " + DG.Rows[x][1].ToString() +" Cantidad Programada : "+ DA.Rows[x]["Cantidad"].ToString() +" Cantidad Modificada : "+ DG.Rows[x]["Cantidad"].ToString() + Environment.NewLine);
                    }

                }
            }
            if (msg == "")
            {

             

                foreach(string s in insert)
                {
                    Q.Consultar(s, estab_unigis);
                }

                return true;

            }
            else {
                MessageBox.Show(msg);
                return false;
            
                }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Motivo")
            {
                return;
            }

            //Try convertir datos a numeros 
            DAux.DefaultView.Sort = "Orden";
            DAux = DAux.DefaultView.ToTable(true);
            try { 
            
                if(dataGridView1.CurrentRow.Cells["Cantidad"].Value!=null)
                Convert.ToDouble(dataGridView1.CurrentRow.Cells["Cantidad"].Value.ToString());
            }
            catch
            {
                dataGridView1.CurrentRow.Cells["Cantidad"].Value = "1";
                return;
            }
            string p=dataGridView1.CurrentRow.Cells["Producto"].Value.ToString().Substring(0, dataGridView1.CurrentRow.Cells["Producto"].Value.ToString().IndexOf('-'));
            double pu = Convert.ToDouble(DAux.Rows[e.RowIndex]["Peso"].ToString()) / Convert.ToDouble(DAux.Rows[e.RowIndex]["Cantidad"].ToString());
            string cantidad =   lib.Decimales(dataGridView1.CurrentRow.Cells["Cantidad"].Value.ToString(),p) ;
            
            
            DataTable dg = dataGridView1.DataSource as DataTable;
           
            for (int x = 0; x < DAux.Rows.Count; x++)
            {
                if (DAux.Rows[x]["Cantidad"].ToString() != dg.Rows[x]["Cantidad"].ToString().ToString())
                {


                    if (Convert.ToDouble(dg.Rows[x]["Cantidad"].ToString()) > Convert.ToDouble(DAux.Rows[x]["Cantidad"].ToString()))
                    {
                        cantidad = DAux.Rows[x]["Cantidad"].ToString();
                        break;
                    }
                }

                }

             


                DataTable dt = new DataTable();
            
            dt = dg;
            dt.DefaultView.Sort="Orden";
            dt = dt.DefaultView.ToTable(true);
            string puString = (Convert.ToDouble(cantidad) * pu).ToString();
            dt.Rows[e.RowIndex]["Cantidad"] = cantidad;
            dt.Rows[e.RowIndex]["Peso"] = puString;
            dataGridView1.DataSource = dt;
            //dataGridView1.CurrentRow.Cells["Cantidad"].Value = cantidad;
            // dataGridView1.CurrentRow.Cells["Peso"].Value =  puString;
        }

        private void dgv_Direcciones_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            
            if (dataGridView1.Rows.Count >0) 
            {
                int columnIndex = dataGridView1.CurrentCell.ColumnIndex;
                string columnName = dataGridView1.Columns[columnIndex].Name;
                if (columnName== "Cantidad")
                {
                    if (!Char.IsDigit(e.KeyChar) &&
                 e.KeyChar != (char)Keys.Back &&
                 e.KeyChar != '.')
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        if (e.KeyChar == '.')
                        { 
                            if (ActiveControl.Text.ToString().Contains('.'))//Text.Contains('.'))
                                e.Handled = true;
                            else
                                e.Handled = false;
                        }
                    }
                } 
            }
        }

    private void controlKeypress(object sender, KeyPressEventArgs e)
        {
            
        }
    public void limpiar()
        {
            
            txt_Jornada.Text = "";
            txt_Ruta.Text = "";
            txt_peso.Text = "";
            txt_fecha.Text = "";
            txt_estado.Text = "";
          //  btn_Editar.Enabled = false;
          //  btn_guardar.Enabled = false;
            dgv_Direcciones.Columns.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
            dgv_Direcciones.RefreshEdit();
        }

        public void cargar()
        {
            if (txt_Jornada.Text.Trim() == "" || txt_Ruta.Text.Trim() == "")
            {
                MessageBox.Show("Favor de Llenar la informacion en los campos IdJornada y IdRuta", "Verificar Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                xmlwriterOrden xml = new xmlwriterOrden();
                ObtenerRutaCompleta_clase orc = new ObtenerRutaCompleta_clase();
                orc.ApiKey = 1234;
                orc.IdJornada = Convert.ToInt32(txt_Jornada.Text);
                orc.IdRuta = Convert.ToInt32(txt_Ruta.Text);

                
                DataTable auth = Q.Consultar("select distinct Operacion from EYP_Unis_ruta where IdRuta='"+txt_Ruta.Text+"' and IdJornada='"+txt_Jornada.Text+"'");

                if (!EstabUser(nusuario, auth.Rows[0].ItemArray[0].ToString()))
                {
                    MessageBox.Show("El usuario no tiene Permiso para realizar movimientos en el Deposito :" + auth.Rows[0].ItemArray[0].ToString(),"ALERTA",MessageBoxButtons.OK);
                    limpiar();
                    return;
                }



                string jsonString = JsonConvert.SerializeObject(orc);

                //MessageBox.Show(xml.stringtoxml(aux));
                //  string msj = xml.stringtoxml(aux).ToString();
                using (var client = new HttpClient())
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3;
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                    var endpoint = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx/ObtenerRutaCompleta");
                    //var result = client.GetAsync(endpoint).Result;
                    //var xmlR = result.Content.ReadAsStringAsync().Result;

                    var paylod = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, paylod).Result.Content.ReadAsStringAsync().Result;
                    // MessageBox.Show(result);
                    var x = JsonConvert.DeserializeObject(result);
                    orcr = JsonConvert.DeserializeObject<ObtenerRutaCompleta_Response.ObtenerRutaCompletaResponse>(result);




                }
                if (orcr.d != null)
                {
                    //if (!lib.validaEstab(nusuario, orcr.d.DepositoSalida.RefDepositoExterno.ToString()))
                    //{
                    //    MessageBox.Show("Solo se pueben confirmar Ordendes de establecimientos asignados al Usuario","Establecimiento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}
                    btn_Editar.Enabled = true;
                    refrescar(orcr);
                    DAux = Productos(orcr);
                    estab_unigis = orcr.d.DepositoSalida.RefDepositoExterno.ToString();

                    if (dataGridView1.DataSource != null)
                    {
                        if (orcr.d.Estado == "Confirmada")
                        {
                            btn_Editar.Enabled = true;
                            btn_guardar.Enabled = false;
                            btn_imp.Enabled = true;
                           
                        }
                        else
                        {
                            MessageBox.Show("No se puede modificar jornada, Estatus diferente a Confirmada");
                            btn_imp.Enabled = true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("El IdRuta y IdJornada proporcionados no existen", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btn_imp.Enabled = false;
                }

            }
        }

        private void txt_Jornada_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbl_jornada_Click(object sender, EventArgs e)
        {

        }

        private void txt_estado_TextChanged(object sender, EventArgs e)
        {

        }

        public string obtener_estab(string deposito)
        {
            string sql = "select rtrim(Cod_estab) from eyp_servidores where unigis='" + deposito + "'";
            DataTable de = Q.Consultar(sql);
            return de.Rows[0].ItemArray[0].ToString();
        }
        public bool EstabUser(string U, string deposito)
        {
            DataTable du;
            DataTable dtX = new DataTable();
            DataColumn dc = new DataColumn();
            dc.ColumnName = "Nombre";
            dtX.Columns.Add(dc);
            DataColumn dc2 = new DataColumn();
            dc2.ColumnName = "cod_estab";
            dtX.Columns.Add(dc2);
            du = Q.Consultar("select isnull(es.Unigis,'') from establecimientos_usuario eu " +
                             "left join usuarios u on eu.usuario = u.usuario " +
                             "left join Eyp_Servidores es on es.Cod_estab=eu.cod_estab "+
                             " Where u.usuario = '" + U + "' and(u.multi_establecimiento = 1 or u.cod_estab = eu.cod_estab) and acceso = '1' and acceso='1'");
                bool f = false;
                foreach (DataRow row2 in du.Rows)
                {
                    if (row2.ItemArray[0].ToString()==deposito)
                    {
                        f = true;
                    }
                }
            return f;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {

                
                   // double pu = orc.d.Ordenes[x].Items[y].Peso / orc.d.Ordenes[x].Items[y].Bulto;
                    // MessageBox.Show(Convert.ToDouble(item.Cells[2].Value.ToString()).ToString());
                // orc.d.Ordenes[x].Items[y].Bulto = Convert.ToInt32(item.Cells[2].Value.ToString());
                MessageBox.Show(Convert.ToDouble(item.Cells[2].Value.ToString()).ToString()+"Cantidad"); 
 
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CrystalDecisions.CrystalReports.Engine.ReportDocument rp = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            //MessageBox.Show(Directory.GetCurrentDirectory() + "/R1534.rpt") ;
            rp.Load(Directory.GetCurrentDirectory() + "/R1534-Unigis.rpt");
            StringBuilder qry = new StringBuilder();
            DataColumn column = new DataColumn();


            if (ContainColumn("Producto", DAux))
            { DAux.Columns["Producto"].ColumnName = "Cod_Prod"; }
            if (ContainColumn("Documento", DAux)) { DAux.Columns["Documento"].ColumnName = "Folio"; }
            if (ContainColumn("Jornada", DAux)) { DAux.Columns["Jornada"].ColumnName = "notas"; }
            if (ContainColumn("ruta", DAux)) { DAux.Columns["ruta"].ColumnName = "notasEmbarque"; }
            rp.SetDataSource(DAux);
            Impresion imp = new Impresion(rp);
            imp.Show();

        }

        private void dataGridView1_CellMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "TOPF2", this.Top.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "LeftF2", this.Left.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "WidthF2", this.Width.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Unigis-EYP", "Formularios", "HeightF2", this.Height.ToString());

          //  MessageBox.Show(Microsoft.VisualBasic.Interaction.GetSetting("Unigis-EYP", "Formularios", "TOPF6").ToString());
         
        }

        private void Actualizar_inicial (ObtenerRutaCompleta_Response.Ordenes orc1)
        {
            xmlwriterOrden xmlw = new xmlwriterOrden();
            string Estado = xmlw.stringtoxmlMOrden_INICIAL(orc1)[0].ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }

}




 
