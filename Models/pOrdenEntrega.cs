using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActualizadorDoctosUnigis
{
    public class pOrdenEntrega
    {
        public string idordenitem { get; set; }
        public string RefDocumento { get; set; }
        public string Descripcion { get; set; }
        public string RefDocumentoAdicional { get; set; }
        public double Cantidad { get; set; }
        public double Volumen { get; set; }
        public double Peso { get; set; }
        public int Bulto { get; set; }
        public int Pallets { get; set; }
        public int Unidades { get; set; }
        public int Int1 { get; set; }
        public int Int2 { get; set; }
        public int ReferenciaCantidad { get; set; }
        public int ReferenciaValor { get; set; }
        public bool ProcesarVolumetria { get; set; }
        public double PrecioUnitario { get; set; }


    }
}
