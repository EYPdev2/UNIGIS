using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualizadorDoctosUnigis.Models
{
    class Estructura_ParadaJS
    {

        public class Rootobject
        {
            public D d { get; set; }
        }

        public class D
        {
            public string __type { get; set; }
            public int Resultado { get; set; }
            public bool ViajeFinalizado { get; set; }
            public int IdViaje { get; set; }
            public object ViajeReferenciaExterna { get; set; }
            public int Secuencia { get; set; }
            public string EstadoParada { get; set; }
            public DateTime HorarioParadaRealInicio { get; set; }
            public DateTime HorarioParadaRealFin { get; set; }
            public string RefDocumento { get; set; }
            public string RefDocumentoAdicional { get; set; }
            public object RefCliente { get; set; }
            public object Transporte { get; set; }
            public string Dominio { get; set; }
            public string Conductor { get; set; }
            public string URL { get; set; }
            public object ReferenciaMotivo { get; set; }
            public string Observaciones { get; set; }
            public string Descripcion { get; set; }
            public string Direccion { get; set; }
            public string Pais { get; set; }
            public string Motivo { get; set; }
            public object MotivoReferencia { get; set; }
            public object FormaDePago { get; set; }
            public object RefExternaFormaPago { get; set; }
            public object NroComprobantePago { get; set; }
            public object InicioHorarioEstimado { get; set; }
            public object FinHorarioEstimado { get; set; }
            public DateTime InicioHorarioPlanificado { get; set; }
            public DateTime FinHorarioPlanificado { get; set; }
            public object ETA { get; set; }
            public Cliente Cliente { get; set; }
            public object ClienteDador { get; set; }
            public Ordenparada OrdenParada { get; set; }
            public Foto[] Fotos { get; set; }
            public Bitacora[] Bitacora { get; set; }
            public Item[] Items { get; set; }
            public object Recursos { get; set; }
            public object Dinamicos { get; set; }
            public float Latitud_opcional { get; set; }
            public decimal Bultos_opcional { get; set; }
            public float Peso_opcional { get; set; }
            public object ValorDeclarado_opcional { get; set; }
            public float Longitud_opcional { get; set; }
            public int ValorACobrar_opcional { get; set; }
            public int ValorCobrado_opcional { get; set; }
            public int IdFormaPago_opcional { get; set; }
            public float Latitud { get; set; }
            public decimal Bultos { get; set; }
            public float Peso { get; set; }
            public int ValorDeclarado { get; set; }
            public float Longitud { get; set; }
            public int ValorACobrar { get; set; }
            public int ValorCobrado { get; set; }
            public int IdFormaPago { get; set; }
        }

        public class Cliente
        {
            public string __type { get; set; }
            public string RefCliente { get; set; }
            public string RazonSocial { get; set; }
            public object Telefono { get; set; }
            public object Telefono2 { get; set; }
            public object Telefono3 { get; set; }
            public object EMail { get; set; }
            public object Direccion { get; set; }
            public object Calle { get; set; }
            public object NumeroPuerta { get; set; }
            public object EntreCalle { get; set; }
            public object Barrio { get; set; }
            public object Localidad { get; set; }
            public object Partido { get; set; }
            public object Provincia { get; set; }
            public object Pais { get; set; }
            public object Latitud { get; set; }
            public object Longitud { get; set; }
            public object RefDomicilioExterno { get; set; }
            public object DomicilioDescripcion { get; set; }
            public object InicioHorario1 { get; set; }
            public object InicioHorario2 { get; set; }
            public object FinHorario1 { get; set; }
            public object FinHorario2 { get; set; }
            public object TiempoEspera { get; set; }
            public int Int1_opcional { get; set; }
            public int Int2_opcional { get; set; }
            public object Varchar1 { get; set; }
            public object Varchar2 { get; set; }
            public int Float1_opcional { get; set; }
            public int Float2_opcional { get; set; }
            public object Grupo { get; set; }
            public object NumeroDocumento { get; set; }
            public object TipoDocumento { get; set; }
            public object CargaExclusiva_opcional { get; set; }
            public object DomicilioCodigoPostal { get; set; }
            public object DomicilioFiscal { get; set; }
            public object CampoDinamico { get; set; }
            public string Contacto { get; set; }
            public object RazonSocialFiscal { get; set; }
            public object IdentificadorFiscal { get; set; }
            public object RequiereTurno { get; set; }
            public object CampoDinamicoDomicilio { get; set; }
            public object IgnorarOperacion_opcional { get; set; }
            public object IgnorarOperacionDomicilioOrden_opcional { get; set; }
            public object CrearDomicilioOrden_opcional { get; set; }
            public object ActualizarDomicilioOrden_opcional { get; set; }
            public object ValidarDomicilioOrden_opcional { get; set; }
            public object Bonificacion_opcional { get; set; }
            public object RefExternaDocumentoFiscal { get; set; }
            public object GrupoTramo { get; set; }
            public int Int1 { get; set; }
            public int Int2 { get; set; }
            public int Float1 { get; set; }
            public int Float2 { get; set; }
            public bool CargaExclusiva { get; set; }
            public bool IgnorarOperacion { get; set; }
            public bool IgnorarOperacionDomicilioOrden { get; set; }
            public bool CrearDomicilioOrden { get; set; }
            public bool ActualizarDomicilioOrden { get; set; }
            public bool ValidarDomicilioOrden { get; set; }
            public int Bonificacion { get; set; }
        }

        public class Ordenparada
        {
            public int IdOrden { get; set; }
            public int IdEstadoOrden { get; set; }
            public int IdJornada { get; set; }
            public int IdRuta { get; set; }
        }

        public class Foto
        {
            public int IdArchivo { get; set; }
            public string Descripcion { get; set; }
            public object Login { get; set; }
            public DateTime FechaCreacion { get; set; }
            public string Tipo { get; set; }
            public object Contenido { get; set; }
            public object Clasificacion { get; set; }
            public bool Firma { get; set; }
            public string Ruta { get; set; }
        }

        public class Bitacora
        {
            public string bitacora { get; set; }
            public string Fecha { get; set; }
            public string Login { get; set; }
        }

        public class Item
        {
            public string RefDocumento { get; set; }
            public object RefDocumentoAdicional { get; set; }
            public string Descripcion { get; set; }
            public decimal Cantidad { get; set; }
            public int Volumen { get; set; }
            public int Peso { get; set; }
            public int Bulto { get; set; }
            public int Pallets { get; set; }
            public decimal CantidadEntregada { get; set; }
            public object Motivo { get; set; }
            public object MotivoReferencia { get; set; }
            public string EstadoParadaItem { get; set; }
            public object Varchar1 { get; set; }
            public object Varchar2 { get; set; }
            public object Varchar3 { get; set; }
            public string Cobrado { get; set; }
            public string NumeroTicket { get; set; }
            public object[] Productos { get; set; }
            public object[] TiposImpuesto { get; set; }
            public int ValorACobrar_opcional { get; set; }
            public int ValorUnitario_opcional { get; set; }
            public int ValorCobrado_opcional { get; set; }
            public int ValorACobrar { get; set; }
            public int ValorUnitario { get; set; }
            public int ValorCobrado { get; set; }
        }

    }
}
