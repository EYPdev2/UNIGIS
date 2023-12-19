using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ActualizadorDoctosUnigis
{
    public class ObtenerRutaCompleta_clase
    {
        public int ApiKey { get; set; }
        public int IdJornada { get; set; }
        public int IdRuta { get; set; }

    }
}
