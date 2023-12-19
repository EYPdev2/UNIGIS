using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ActualizadorDoctosUnigis.Models
{
    class xmlwriterParada
    {

        public string stringtoxml(string apikey, string RefDocto,string Estado,string idviaje,string validartrans)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            StringWriter sw = new StringWriter();
            string xmls;
            using (XmlWriter xmlw = XmlWriter.Create(sw, settings))
            {
                var soap = "http://schemas.xmlsoap.org/soap/envelope/";
                var xsd = "http://www.w3.org/2001/XMLSchema";
                var xsi = "http://www.w3.org/2001/XMLSchema-instance";
                xmlw.WriteStartDocument();
                xmlw.WriteStartElement("soap", "Envelope", soap);
                xmlw.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                xmlw.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
                xmlw.WriteStartElement("Body", soap);
                xmlw.WriteStartElement("", "ModificarEstadoParada", "http://unisolutions.com.ar/");
                xmlw.WriteStartElement("ApiKey" );xmlw.WriteString(apikey.ToString());xmlw.WriteEndElement();
                xmlw.WriteStartElement("estado" );
                xmlw.WriteStartElement("RefDocumento" ); xmlw.WriteString(RefDocto.ToString()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("Estado" ); xmlw.WriteString("Liberado"); xmlw.WriteEndElement();
                xmlw.WriteStartElement("EstadoFecha" ); xmlw.WriteString(DateTime.Now.ToString("yyy-MM-ddTHH:mm:ss")); xmlw.WriteEndElement();
                xmlw.WriteStartElement("IdViaje" );xmlw.WriteString(idviaje.ToString());xmlw.WriteEndElement();
                xmlw.WriteStartElement("mismoEstado"); xmlw.WriteString(validartrans.ToString()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("ValidarTransicion"); xmlw.WriteString(validartrans.ToString()); xmlw.WriteEndElement();
                xmlw.Close();
                XmlDocument x = new XmlDocument();

                return sw.ToString();
            }

            return "";
        }
        public string stringtoxmlM(Estructura_ParadaJS.Rootobject js,double latitud,double longitud,string idParada)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            StringWriter sw = new StringWriter();
            string xmls;
            using (XmlWriter xmlw = XmlWriter.Create(sw, settings))
            {
                var soap = "http://schemas.xmlsoap.org/soap/envelope/";
                var xsd = "http://www.w3.org/2001/XMLSchema";
                var xsi = "http://www.w3.org/2001/XMLSchema-instance";
                xmlw.WriteStartDocument();
                xmlw.WriteStartElement("soap", "Envelope", soap);
                xmlw.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                xmlw.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
                xmlw.WriteStartElement("Body", soap);
                xmlw.WriteStartElement("", "ModificarEstadoParada", "http://unisolutions.com.ar/");
                xmlw.WriteStartElement("ApiKey"); xmlw.WriteString("1234"); xmlw.WriteEndElement();
                xmlw.WriteStartElement("estado");
                xmlw.WriteStartElement("RefDocumento"); xmlw.WriteString(js.d.RefDocumento); xmlw.WriteEndElement();
                xmlw.WriteStartElement("Estado"); xmlw.WriteString("Validado"); xmlw.WriteEndElement();
                xmlw.WriteStartElement("EstadoFecha"); xmlw.WriteString(DateTime.Now.ToString("yyy-MM-ddTHH:mm:ss")); xmlw.WriteEndElement();
                xmlw.WriteStartElement("IdViaje"); xmlw.WriteString(js.d.IdViaje.ToString()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("mismoEstado"); xmlw.WriteString("False"); xmlw.WriteEndElement();
                xmlw.WriteStartElement("Latitud"); xmlw.WriteString(latitud.ToString ()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("Longitud"); xmlw.WriteString(longitud.ToString()); xmlw.WriteEndElement();
                xmlw.WriteStartElement("IdParadaTraceEstado"); xmlw.WriteString(idParada); xmlw.WriteEndElement();
                xmlw.WriteStartElement("MismoEstado"); xmlw.WriteString("false"); xmlw.WriteEndElement();
                xmlw.WriteStartElement("ValidarTransicion"); xmlw.WriteString("false"); xmlw.WriteEndElement();
                xmlw.Close();
                XmlDocument x = new XmlDocument();

                return sw.ToString();
            }

            return "";
        }
    }
}
