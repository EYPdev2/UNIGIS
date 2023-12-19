using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActualizadorDoctosUnigis
{
    public class DepositoLlegada
    {
        public string RefDepositoExterno { get; set; }
        public string Descripcion { get; set; }
        public int InicioHorario { get; set; }
        public int FinHorario { get; set; }
        public int TiempoEspera { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public double x { get; set; }
        public double y { get; set; }
    }
}
