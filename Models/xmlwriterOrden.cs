
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Office.Interop.Excel;
using objExcel = Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using Workbook = Microsoft.Office.Interop.Excel.Workbook;
using Worksheet = Microsoft.Office.Interop.Excel.Worksheet;


namespace ActualizadorDoctosUnigis
{
    public class xmlwriterOrden
    {
        string xmldocument;
        List<string> xmlString= new List<string>();

        public string stringtoxml(DataTable dt)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            StringWriter sw = new StringWriter();
            string xmls;
            string folio = dt.Rows[0]["N° DOCUMENTO"].ToString();
            string Fd = Convert.ToDateTime(dt.Rows[0]["FECHA_DOC"].ToString()).ToString("yyyy-MM-dd");
            string Fe = Convert.ToDateTime(dt.Rows[0]["FechaEntrega"].ToString()).ToString("yyyy-MM-dd");
            string cod_cte = dt.Rows[0]["cod_cte"].ToString().Trim();
            string RZCliente = dt.Rows[0]["Razon_social"].ToString().Trim();
            string TelCl = dt.Rows[0]["TELEFONO"].ToString().Trim();
            string E_mailC = dt.Rows[0]["e_mail"].ToString().Trim();
            string dirC = dt.Rows[0]["Direccion"].ToString().Trim();
            double pesoT = 0;
            double cantidadT = 0;
            foreach (DataRow fila in dt.Rows)
            {
                pesoT = pesoT + Convert.ToDouble(fila["peso_kg"].ToString());
                cantidadT = cantidadT + Convert.ToDouble(fila["CANTIDAD"].ToString());
            }

            using (XmlWriter xmlw = XmlWriter.Create(sw, settings))
            {
                var soap = "http://schemas.xmlsoap.org/soap/envelope/";
                var unis = "http://unisolutions.com.ar/";
                xmlw.WriteStartDocument();
                xmlw.WriteStartElement("soapenv", "Envelope", soap);
                xmlw.WriteAttributeString("xmlns", "unis", null, "http://unisolutions.com.ar/");
                xmlw.WriteStartElement("Header", soap);
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("Body", soap);
                xmlw.WriteStartElement("unis", "CrearOrdenesPedido", unis);
                xmlw.WriteStartElement("ApiKey", unis); xmlw.WriteString("1234");xmlw.WriteEndElement();
                xmlw.WriteStartElement("pedidos", unis);
                xmlw.WriteStartElement("pOrdenPedido", unis);

                xmlw.WriteStartElement("RefDocumento", unis);
                xmlw.WriteString(folio);
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("RefDocumentoAdicional", unis);
                xmlw.WriteString(folio);
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("Fecha", unis);
                xmlw.WriteString(Fd);
                xmlw.WriteEndElement();
                if (dt.Rows[0]["Tipo"].ToString().Trim() == "D") { xmlw.WriteStartElement("FechaEntrega", unis); }
                else if (dt.Rows[0]["Tipo"].ToString().Trim() == "P") { xmlw.WriteStartElement("FechaRecoleccion", unis); }
                xmlw.WriteString(Fe);
                xmlw.WriteEndElement();

                if (dt.Rows[0]["Tipo"].ToString().Trim() == "D") { xmlw.WriteStartElement("Cliente", unis); }
                else if (dt.Rows[0]["Tipo"].ToString().Trim() == "P") { xmlw.WriteStartElement("Cliente2", unis); }
                
                xmlw.WriteStartElement("RefCliente", unis);
                xmlw.WriteString(cod_cte);
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("RazonSocial", unis);
                xmlw.WriteString(RZCliente);
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("Telefono", unis);
                xmlw.WriteString(TelCl);
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("Telefono2", unis);
                xmlw.WriteString(TelCl);
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("EMail", unis);
                xmlw.WriteString("aux.sistemas3@EYP74.com");
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("Direccion", unis);
                xmlw.WriteString(dirC);
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("Calle", unis);
                xmlw.WriteString("-");
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("NroPuerta", unis);
                xmlw.WriteString(dt.Rows[0]["NumeroPuerta"].ToString().Trim());
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("Barrio", unis);
                xmlw.WriteString(dt.Rows[0]["Colonia"].ToString().Trim());
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("Localidad", unis);
                xmlw.WriteString(dt.Rows[0]["Localidad"].ToString().Trim());
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("Partido", unis);
                xmlw.WriteString(dt.Rows[0]["Partido"].ToString().Trim());
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("Provincia", unis);
                xmlw.WriteString(dt.Rows[0]["Estado"].ToString().Trim());
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("Pais", unis);
                xmlw.WriteString(dt.Rows[0]["Pais"].ToString().Trim());
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("Latitud", unis);
                xmlw.WriteString(dt.Rows[0]["LATITUD"].ToString().Trim());
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("Longitud", unis);
                xmlw.WriteString(dt.Rows[0]["LONGITUD"].ToString().Trim());
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("RefDomicilioExterno", unis);
                xmlw.WriteString(cod_cte);
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("DomicilioDescripcion", unis);
                xmlw.WriteString(RZCliente);
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("InicioHorario1", unis);
                xmlw.WriteString(dt.Rows[0]["InicioHorario1"].ToString().Trim());
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("InicioHorario2", unis);
                xmlw.WriteString(dt.Rows[0]["InicioHorario2"].ToString().Trim());
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("FinHorario1", unis);
                xmlw.WriteString(dt.Rows[0]["FinHorario1"].ToString().Trim());
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("FinHorario2", unis);
                xmlw.WriteString(dt.Rows[0]["FinHorario2"].ToString().Trim());
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("TiempoEspera", unis);
                xmlw.WriteString(dt.Rows[0]["TiempoEspera"].ToString().Trim());
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("DomicilioCodigoPostal", unis);
                xmlw.WriteString(dt.Rows[0]["DomCodPostal"].ToString().Trim());
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("Contacto", unis);
                xmlw.WriteString(RZCliente);
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("CargaExclusiva", unis);
                xmlw.WriteString("false");
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("IgnorarOperacion", unis);
                xmlw.WriteString("false");
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("IgnorarOperacionDomicilioOrden", unis);
                xmlw.WriteString("true");
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("CrearDomicilioOrden", unis);
                xmlw.WriteString("true");
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("ActualizarDomicilioOrden", unis);
                xmlw.WriteString("true");
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("ValidarDomicilioOrden", unis);
                xmlw.WriteString("false");
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("Bonificacion", unis);
                xmlw.WriteString("-1");
                xmlw.WriteEndElement();
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("Telefono", unis); xmlw.WriteString(TelCl); xmlw.WriteEndElement();
                xmlw.WriteStartElement("Telefono2", unis); xmlw.WriteString(TelCl); xmlw.WriteEndElement();
                xmlw.WriteStartElement("Descripcion", unis); xmlw.WriteString(RZCliente); xmlw.WriteEndElement();
                xmlw.WriteStartElement("CodigoSucursal", unis); xmlw.WriteString("GEYP"); xmlw.WriteEndElement();
                xmlw.WriteStartElement("TipoPedido", unis); xmlw.WriteString(dt.Rows[0]["Tipo"].ToString().Trim()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("Estado", unis); xmlw.WriteString("Inicial"); xmlw.WriteEndElement();
                xmlw.WriteStartElement("Direccion", unis); xmlw.WriteString(dirC); xmlw.WriteEndElement();
                xmlw.WriteStartElement("Calle", unis); xmlw.WriteString(dt.Rows[0]["calle"].ToString().Trim()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("NroPuerta", unis); xmlw.WriteString(dt.Rows[0]["NumeroPuerta"].ToString().Trim()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("Barrio", unis); xmlw.WriteString(dt.Rows[0]["colonia"].ToString().Trim()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("Localidad", unis); xmlw.WriteString(dt.Rows[0]["Localidad"].ToString().Trim()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("Partido", unis); xmlw.WriteString(dt.Rows[0]["Partido"].ToString().Trim()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("Provincia", unis); xmlw.WriteString(dt.Rows[0]["Estado"].ToString().Trim()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("Pais", unis); xmlw.WriteString(dt.Rows[0]["Pais"].ToString().Trim()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("InicioHorario1", unis); xmlw.WriteString(dt.Rows[0]["InicioHorario1"].ToString().Trim()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("FinHorario1", unis); xmlw.WriteString(dt.Rows[0]["FinHorario1"].ToString().Trim()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("InicioHorario2", unis); xmlw.WriteString(dt.Rows[0]["InicioHorario2"].ToString().Trim()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("FinHorario2", unis); xmlw.WriteString(dt.Rows[0]["FinHorario2"].ToString().Trim()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("TiempoEspera", unis); xmlw.WriteString(dt.Rows[0]["TiempoEspera"].ToString().Trim()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("Peso", unis); xmlw.WriteString(pesoT.ToString()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("Bulto", unis); xmlw.WriteString(cantidadT.ToString()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("Latitud", unis); xmlw.WriteString(dt.Rows[0]["Latitud"].ToString().Trim()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("Longitud", unis); xmlw.WriteString(dt.Rows[0]["Longitud"].ToString().Trim()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("Observaciones", unis); xmlw.WriteString("-"); xmlw.WriteEndElement();
                xmlw.WriteStartElement("B2CPassword", unis); xmlw.WriteString("-"); xmlw.WriteEndElement();
                xmlw.WriteStartElement("Email", unis); xmlw.WriteString(""); xmlw.WriteEndElement();
                xmlw.WriteStartElement("cargaExclusiva", unis); xmlw.WriteString("false"); xmlw.WriteEndElement();
                xmlw.WriteStartElement("requiereTurno", unis); xmlw.WriteString("false"); xmlw.WriteEndElement();
                xmlw.WriteStartElement("ultimaVisita", unis); xmlw.WriteString("false"); xmlw.WriteEndElement();
                xmlw.WriteStartElement("requiereAbasto", unis); xmlw.WriteString("false"); xmlw.WriteEndElement();
                xmlw.WriteStartElement("depositoSalida", unis);
                xmlw.WriteStartElement("RefDepositoExterno", unis); xmlw.WriteString(dt.Rows[0]["RefDepositoExterno"].ToString().Trim()); xmlw.WriteEndElement(); xmlw.WriteEndElement();
                xmlw.WriteStartElement("depositoLlegada", unis);
                xmlw.WriteStartElement("CodigoPostal", unis); xmlw.WriteString(dt.Rows[0]["CodigoPostalEstab"].ToString().Trim()); xmlw.WriteEndElement(); xmlw.WriteEndElement();
                xmlw.WriteStartElement("CodigoPostal", unis); xmlw.WriteString(dt.Rows[0]["DomCodPostal"].ToString().Trim()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("CodigoOperacion", unis); xmlw.WriteString("EYP"+dt.Rows[0]["CodigoSucursal"]); xmlw.WriteEndElement();
                xmlw.WriteStartElement("Items", unis);


                foreach (DataRow fila in dt.Rows)
                {
                    xmlw.WriteStartElement("pOrdenPedidoItem", unis);
                    xmlw.WriteStartElement("RefDocumento", unis);
                    xmlw.WriteString(fila["CODIGO ITEM"].ToString().Trim());
                    xmlw.WriteEndElement();
                    xmlw.WriteStartElement("RefDocumentoAdicional", unis);
                    xmlw.WriteString(fila["CODIGO ITEM"].ToString().Trim() + "-" + fila["NOMBRE ITEM"].ToString().Trim());
                    xmlw.WriteEndElement();
                    xmlw.WriteStartElement("Producto", unis);
                    xmlw.WriteStartElement("RefProucto", unis);
                    xmlw.WriteString(fila["CODIGO ITEM"].ToString().Trim());
                    xmlw.WriteEndElement();
                    xmlw.WriteStartElement("Descripcion", unis);
                    xmlw.WriteString(fila["NOMBRE ITEM"].ToString().Trim());
                    xmlw.WriteEndElement();
                    xmlw.WriteStartElement("Linea", unis);
                    xmlw.WriteString(fila["Linea"].ToString().Trim());
                    xmlw.WriteEndElement();
                    xmlw.WriteStartElement("SubLinea", unis);
                    xmlw.WriteString(fila["SubLinea"].ToString().Trim());
                    xmlw.WriteEndElement();
                    xmlw.WriteEndElement();
                    xmlw.WriteStartElement("Cantidad", unis);
                    xmlw.WriteString(Math.Ceiling(Convert.ToDouble(fila["CANTIDAD"].ToString())).ToString());
                    xmlw.WriteEndElement();
                    xmlw.WriteStartElement("Peso", unis);
                    xmlw.WriteString(fila["Peso_KG"].ToString());
                    xmlw.WriteEndElement();
                    xmlw.WriteStartElement("Bulto", unis);
                    xmlw.WriteString(Math.Ceiling(Convert.ToDouble(fila["CANTIDAD"].ToString())).ToString());
                    xmlw.WriteEndElement();
                    xmlw.WriteStartElement("Descripcion", unis);
                    xmlw.WriteString(fila["NOMBRE ITEM"].ToString().Trim());
                    xmlw.WriteEndElement();
                    //    if (Convert.ToDouble(fila["CANTIDAD"].ToString()) % 1 > 0)
                    //{

                        xmlw.WriteStartElement("Varchar1", unis);
                        xmlw.WriteString(fila["CANTIDAD"].ToString());
                        xmlw.WriteEndElement();

                    //}
                     xmlw.WriteEndElement();
                    

                }
                xmlw.WriteEndElement();

                xmlw.WriteStartElement("IdPedido", unis);
                xmlw.WriteString("-1");
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("usarProductos", unis);
                xmlw.WriteString("false");
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("ValidarTransicion", unis);
                xmlw.WriteString("false");
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("soloInsertarProductos", unis);
                xmlw.WriteString("false");
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("agruparItems", unis);
                xmlw.WriteString("false");
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("GrupoRutas", unis);
                xmlw.WriteString("-1");
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("Unidades", unis);
                xmlw.WriteString("-1");
                xmlw.WriteEndElement();
                xmlw.WriteEndElement();
                xmlw.WriteEndDocument();
                xmlw.Close();
                XmlDocument x = new XmlDocument();

                return sw.ToString();
            }
        }

        public string validar_envios(DataTable dt)
        {
            string doc= folio(dt.Rows[0]["N° DOCUMENTO"].ToString(),0);
            string trans = folio(dt.Rows[0]["N° DOCUMENTO"].ToString(), 1);
            return "";
        }

        public string folio(string referencia_ext, int modo)
        {
            int idx = referencia_ext.IndexOf('-');
            if (modo == 0)
            {
                string s = referencia_ext.Substring(idx + 1);
                int idx2 = referencia_ext.Substring(idx + 1).IndexOf('|');
                s = s.Substring(0, idx2);
                return s;
            }
            else
            {
                string tran = referencia_ext.Substring(0, idx);
                switch (referencia_ext.Substring(0, idx))
                {
                    case "EdMaC": tran = "713"; break;
                    case "FC": tran = "36"; break;
                    case "RC": tran = "37"; break;
                }
                return tran;
            }




        }
        public List<string>   stringtoxmlMOrden(ObtenerRutaCompleta_Response.ObtenerRutaCompletaResponse orc)
        {
            
            for (int x = 0; x < orc.d.Ordenes.Count; x++)
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.OmitXmlDeclaration = true;
                StringWriter sw = new StringWriter();
                using (XmlWriter xmlw = XmlWriter.Create(sw, settings))
                {
                    var soap = "http://schemas.xmlsoap.org/soap/envelope/";
                    var unis = "http://unisolutions.com.ar/";
                    xmlw.WriteStartDocument();
                    xmlw.WriteStartElement("soapenv", "Envelope", soap);
                    xmlw.WriteAttributeString("xmlns", "unis", null, "http://unisolutions.com.ar/"); xmlw.WriteString(Environment.NewLine);
                    xmlw.WriteStartElement("Header", soap); xmlw.WriteString(Environment.NewLine);
                    xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                    xmlw.WriteStartElement("Body", soap); xmlw.WriteString(Environment.NewLine);
                    xmlw.WriteStartElement("unis", "ModificarEstadoOrdenEntrega", unis);
                    xmlw.WriteStartElement("ApiKey", unis); xmlw.WriteString("1234"); xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                    xmlw.WriteStartElement("estado", unis); xmlw.WriteString(Environment.NewLine);
                    xmlw.WriteStartElement("IdOrden", unis); xmlw.WriteString(orc.d.Ordenes[x].IdOrden.ToString()); xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                    xmlw.WriteStartElement("Estado", unis); xmlw.WriteString("Documentado"); xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                    xmlw.WriteStartElement("Items", unis);
                    for (int y = 0; y < orc.d.Ordenes[x].Items.Count; y++)
                    {
                        xmlw.WriteStartElement("pOrdenEntregaItem", unis);
                        xmlw.WriteStartElement("IdOrdenItem", unis); xmlw.WriteString(orc.d.Ordenes[x].Items[y].IdOrdenItem.ToString()); xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                        xmlw.WriteStartElement("RefDocumento", unis); xmlw.WriteString(orc.d.Ordenes[x].Items[y].RefDocumento.ToString()); xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                        xmlw.WriteStartElement("Cantidad", unis); xmlw.WriteString(orc.d.Ordenes[x].Items[y].Cantidad.ToString()); xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                        xmlw.WriteStartElement("Volumen", unis); xmlw.WriteString(orc.d.Ordenes[x].Items[y].Volumen.ToString()); xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                        xmlw.WriteStartElement("Peso", unis); xmlw.WriteString(orc.d.Ordenes[x].Items[y].Peso.ToString()); xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                        xmlw.WriteStartElement("Bulto", unis); xmlw.WriteString(orc.d.Ordenes[x].Items[y].Bulto.ToString()); xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                        xmlw.WriteStartElement("Pallets", unis); xmlw.WriteString(orc.d.Ordenes[x].Items[y].Pallets.ToString()); xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                        xmlw.WriteStartElement("ProcesarVolumetria", unis); xmlw.WriteString(orc.d.Ordenes[x].Items[y].ProcesarVolumetria.ToString().ToLower()); xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                        xmlw.WriteEndElement();//pordenentregaitem
                    }
                    xmlw.WriteEndElement();//Items
                    int aux = 0;
                    if (orc.d.Ordenes[x].ValidarTransicion.ToString().ToLower() == "true") { aux = 1; }
                    xmlw.WriteStartElement("ValidarTransicion", unis); xmlw.WriteString("1") ; xmlw.WriteEndElement();
                    xmlw.WriteEndElement();//estadoxmlw.WriteString(Environment.NewLine);
                    xmlw.WriteEndDocument();
                    xmlw.Close();
                    xmlString.Add(sw.ToString());

                }
            }
        

                return xmlString;
        }

        public List<string> stringtoxmlMOrden_INICIAL(ObtenerRutaCompleta_Response.Ordenes orc)
        {

            string estado;
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.OmitXmlDeclaration = true;
                StringWriter sw = new StringWriter();
            switch (orc.Estado)
            {
                case "Inicial":
                   estado= "Planificado";
                    break;
                case "Planificado":
                    estado = "Confirmada";
                    break;
                default:estado = "Exit";break;
            }

                
                using (XmlWriter xmlw = XmlWriter.Create(sw, settings))
                {
                    var soap = "http://schemas.xmlsoap.org/soap/envelope/";
                    var unis = "http://unisolutions.com.ar/";
                    xmlw.WriteStartDocument();
                    xmlw.WriteStartElement("soapenv", "Envelope", soap);
                    xmlw.WriteAttributeString("xmlns", "unis", null, "http://unisolutions.com.ar/"); xmlw.WriteString(Environment.NewLine);
                    xmlw.WriteStartElement("Header", soap); xmlw.WriteString(Environment.NewLine);
                    xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                    xmlw.WriteStartElement("Body", soap); xmlw.WriteString(Environment.NewLine);
                    xmlw.WriteStartElement("unis", "ModificarEstadoOrdenEntrega", unis);
                    xmlw.WriteStartElement("ApiKey", unis); xmlw.WriteString("1234"); xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                    xmlw.WriteStartElement("estado", unis); xmlw.WriteString(Environment.NewLine);
                    xmlw.WriteStartElement("IdOrden", unis); xmlw.WriteString(orc.IdOrden.ToString()); xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                    xmlw.WriteStartElement("Estado", unis); xmlw.WriteString(estado); xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                    xmlw.WriteStartElement("Items", unis);
                    for (int y = 0; y < orc.Items.Count; y++)
                    {
                        xmlw.WriteStartElement("pOrdenEntregaItem", unis);
                        xmlw.WriteStartElement("IdOrdenItem", unis); xmlw.WriteString(orc.Items[y].IdOrdenItem.ToString()); xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                        xmlw.WriteStartElement("RefDocumento", unis); xmlw.WriteString(orc.Items[y].RefDocumento.ToString()); xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                        xmlw.WriteStartElement("Cantidad", unis); xmlw.WriteString(orc.Items[y].Cantidad.ToString()); xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                        xmlw.WriteStartElement("Volumen", unis); xmlw.WriteString(orc.Items[y].Volumen.ToString()); xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                        xmlw.WriteStartElement("Peso", unis); xmlw.WriteString(orc.Items[y].Peso.ToString()); xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                        xmlw.WriteStartElement("Bulto", unis); xmlw.WriteString(orc.Items[y].Bulto.ToString()); xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                        xmlw.WriteStartElement("Pallets", unis); xmlw.WriteString(orc.Items[y].Pallets.ToString()); xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                        xmlw.WriteStartElement("ProcesarVolumetria", unis); xmlw.WriteString(orc.Items[y].ProcesarVolumetria.ToString().ToLower()); xmlw.WriteEndElement(); xmlw.WriteString(Environment.NewLine);
                        xmlw.WriteEndElement();//pordenentregaitem
                    }
                    xmlw.WriteEndElement();//Items
                    int aux = 0;
                    if (orc.ValidarTransicion.ToString().ToLower() == "true") { aux = 1; }
                    xmlw.WriteStartElement("ValidarTransicion", unis); xmlw.WriteString("1"); xmlw.WriteEndElement();
                    xmlw.WriteEndElement();//estadoxmlw.WriteString(Environment.NewLine);
                    xmlw.WriteEndDocument();
                    xmlw.Close();
                    xmlString.Add(sw.ToString());

                }
            send_XML(xmlString[0].ToString());
            if (estado != "Confirmada")
            {
                orc.Estado = estado;
                stringtoxmlMOrden_INICIAL(orc);

            }
            return xmlString;
        }

        public void send_XML(string xml)
        {
            using (var client = new HttpClient())
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                var endpoint = new Uri("https://cloud-test.unigis.com/Grupo_EYP/mapi/soap/logistic/service.asmx?op=ModificarEstadoOrdenEntrega");
                //var result = client.GetAsync(endpoint).Result;
                //var xmlR = result.Content.ReadAsStringAsync().Result;


                var paylod = new StringContent(xml, Encoding.UTF8, "text/xml");
                var result = client.PostAsync(endpoint, paylod).Result.Content.ReadAsStringAsync().Result;
            }

        }
        public void ExportarExcel(DataGridView dgv)

        {

            // dgv.Columns.RemoveAt(0);

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(true);

            int IndiceColumna = 0;


            foreach (DataGridViewColumn col in dgv.Columns)
            {

                IndiceColumna++;

                excel.Cells[1, IndiceColumna] = col.Name;

                //  excel.Cells[0].Visible = false;

            }
            int IndiceFila = 0;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                IndiceFila++;
                IndiceColumna = 0;

                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    IndiceColumna++;
                    excel.Cells[IndiceFila + 1, IndiceColumna] = row.Cells[col.Name].Value;

                }

                //string color = dgv.Rows[0].Cells[0].Value.ToString();
                // MessageBox.Show()


                // Eliminar 2 columnas en el índice 1

                excel.Visible = true;

               
            }

        }


    }
}
