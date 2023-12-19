using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualizadorDoctosUnigis.Models
{
    public class ObtenerPAradaid
    {
        public class consultar
        {
            public int ApiKey { get; set; }
            public int IdParada { get; set; }
        }


        public class ObtenerParadaResponse
        { 
            public ConsultarParadaPorIdResult d { get; set; }
        }
         public class ConsultarParadaPorIdResult2
        {
            public List<ConsultarParadaPorIdResult> ConsultarParadaPorIdResult { get; set; }
        }

        public class Items
        {
            public string RefDocumento { get; set; }
            public int Cantidad { get; set; }
            public decimal Peso { get; set; }
            public decimal bulto { get; set; }
            public int Pallets { get; set; }
            public Decimal CantidadEntregada { get; set; }
            public string EstadoParadaItem { get; set; }
        }
        public class Cliente
        {
            public string RefCliente { get; set; }
            public string RazonSocial { get; set; }
            public bool? Longitud { get; set; }
            public bool? Latitud { get; set; }
            public bool? InicioHorario1 { get; set; }
            public bool? InicioHorario2 { get; set; }
            public bool? FinHorario1 { get; set; }
            public bool? FinHorario2 { get; set; }
            public bool? TiempoEspera { get; set; }
            public string NumeroDocumento { get; set; }
            public string Contacto { get; set; }
            public int Int1 { get; set; }
            public int Int2 { get; set; }
            public float Float1 { get; set; }
            public float Float2 { get; set; }
            public int Bonificacion { get; set; }
        }

        public class ConsultarParadaPorIdResult
        {
            public int Resultado { get; set; }
            public bool ViajeFinalizado { get; set; }
            public int IdViaje { get; set; }
            public string Secuencia { get; set; }
            public string EstadoParada { get; set; }
            public DateTime HorarioParadaRealInicio { get; set; }
            public DateTime HorarioParadaRealFin { get; set; }
            public string RefDocumento { get; set; }
            public string Dominio { get; set; }
            public string URL { get; set; }
            public string Descripcion { get; set; }
            public string Direccion { get; set; }
            public string Pais { get; set; }
            public DateTime InicioHorarioEstimado { get; set; }
            public DateTime FinHorarioEstimado { get; set; }
            public DateTime InicioHorarioPlanificado { get; set; }
            public DateTime FinHorarioPlanificado { get; set; }
            public DateTime ETA { get; set; }
            public  Cliente  Cliente { get; set; }
            public List<Items> Items { get; set; }
            public decimal Latitud { get; set; }
            public decimal Bultos { get; set; }
            public decimal Peso { get; set; }
            public int ValorDeclarado { get; set; }
        public decimal Longitud { get; set; }

        }

    }

}

