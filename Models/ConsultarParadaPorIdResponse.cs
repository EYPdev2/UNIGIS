using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualizadorDoctosUnigis.Models
{
 
    public class ConsultarParadaPorIdResponse
    {
        public ConsultarParadaPorIdResult d { get; set; }
            public class cliente
    {
        public string RefCliente { get; set; }
        public string RazonSocial { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string InicioHorario1 { get; set; }
        public string InicioHorario2 { get; set; }
        public string FinHorario1 { get; set; }
        public string FinHorario2 { get; set; }
        public string TiempoEspera { get; set; }
        public string NumeroDocumento { get; set; }
        public string Contacto { get; set; }
        public int Int1 { get; set; }
        public int Int2 { get; set; }
        public float Float2 { get; set; }
        public float Float1 { get; set; }
        public int Bonificacion { get; set; }


    }

    public class Items
    {
            public string RefDocumento { get; set; }
            public decimal Cantidad { get; set; }
            public float Volument { get; set; }
            public float Peso { get; set; }
            public decimal Bulto { get; set; }
            public int Pallets { get; set; }
            public decimal CantidadEntregada { get; set; }
            public string EstadoParadaItem { get; set; }
        } 
    public class ConsultarParadaResultadoItem
    {
        

    }
    public class ConsultarParadaPorIdResult
    {
        public int Resultado { get; set; }
        public string ViajeFinalizado { get; set; }
        public int IdViaje { get; set; }
        public int Secuencia { get; set; }
        public string EstadoParada { get; set; }
        public DateTime? HorarioParadaRealInicio { get; set; }
        public DateTime? HorarioParadaRealFin { get; set; }
        public string RefDocumento { get; set; }
        public string Dominio { get; set; }
        public string URL { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public string Pais { get; set; }
        public DateTime? InicioHorarioEstimado { get; set; }
        public DateTime? FinHorarioEstimado { get; set; }
        public DateTime? InicioHorarioPlanioficado { get; set; }
        public DateTime? FinHorarioPlanificado { get; set; }
        public DateTime? ETA { get; set; }
        public cliente Cliente { get; set; }
        public List<Items> Items { get; set; }
        public float Latitud { get; set; }
        public double Bultos { get; set; }
        public float Peso { get; set; }
        public int ValorDeclarado { get; set; }
        public float Longitud { get; set; }

    }
    }


}
