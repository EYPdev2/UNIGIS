using System.IO;
using System.Xml;

namespace WebApiXML.Models
{
    public class ActualizadorDoctosUnigis
    {
        string xmldocument;
        //public static void crearxml()
        //{

        //    // Declare a namespace with AttributeString
        //    //xmlw.WriteAttributeString("xmlns", "soap", null, "something:something");
        //    //// Use it here
        //    //xmlw.WriteStartElement("Envelope", "something:something");
        //    //xmlw.WriteAttributeString("xmlns", "soapenv", null, "http://schemas.xmlsoap.org/soap/envelope/");
        //    //xmlw.WriteAttributeString("xmlns", "unis", null, "http://unisolutions.com.ar/");
        //    //xmlw.WriteStartElement("Header");
        //    //xmlw.WriteEndElement();
        //    //xmlw.WriteStartElement("Body");
        //    //xmlw.WriteStartElement("ObtenerRutaCompleta");
        //    //xmlw.WriteStartElement("ApiKey");
        //    //xmlw.WriteString(apikey.ToString());
        //    //xmlw.WriteEndElement();
        //    //xmlw.WriteStartElement("IdJornada");
        //    //xmlw.WriteString(idjornada.ToString());
        //    //xmlw.WriteEndElement();
        //    //xmlw.WriteStartElement("IdRuta");
        //    //xmlw.WriteString(idruta.ToString());
        //    //xmlw.WriteEndElement();

        //    //xmlw.Close();

        //}

        public string stringtoxml(int apikey, int idjornada, int idruta)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            StringWriter sw = new StringWriter();
            string xmls;
            //XmlWriter xmlw = XmlWriter.Create(xmls);
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
                xmlw.WriteStartElement("unis", "ObtenerRutaCompleta", unis);
                xmlw.WriteStartElement("ApiKey", unis);
                xmlw.WriteString(apikey.ToString());
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("IdJornada", unis);
                xmlw.WriteString(idjornada.ToString());
                xmlw.WriteEndElement();
                xmlw.WriteStartElement("IdRuta", unis);
                xmlw.WriteString(idruta.ToString());
                xmlw.WriteEndElement();
                xmlw.Close();
                XmlDocument x = new XmlDocument();

                return sw.ToString();
            }
        }
    }
}
