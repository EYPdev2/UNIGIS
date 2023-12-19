using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActualizadorDoctosUnigis.Models
{
    public class ObtenerViaje_Response
    {
        public class Rootobject
        {
            public D d { get; set; }
        }

        public class D
        {
            public string __type { get; set; }
            public string ReferenciaExterna { get; set; }
            public string Descripcion { get; set; }
            public string Resultado { get; set; }
            public bool ViajeFinalizado { get; set; }
            public string EstadoViaje { get; set; }
            public string EstadoSeguimiento { get; set; }
            public object VehiculoEstadoSeguimiento { get; set; }
            public decimal CantidadParadas { get; set; }
            public int ParadaVisitada { get; set; }
            public DateTime? FechaParadaVisitada { get; set; }
            public object RefCliente { get; set; }
            public string RefDocumento { get; set; }
            public object RefDocumentoAdicional { get; set; }
            public float KmsPlan { get; set; }
            public float KmsRecorridos { get; set; }
            public DateTime? FechaInicioReal { get; set; }
            public DateTime? FechaFinReal { get; set; }
            public DateTime? FechaInicioPlan { get; set; }
            public DateTime? FechaFinPlan { get; set; }
            public Depositosalida DepositoSalida { get; set; }
            public Depositollegada DepositoLlegada { get; set; }
            public Vehiculo Vehiculo { get; set; }
            public object VehiculoSecundario { get; set; }
            public object VehiculoTerciario { get; set; }
            public string CategoriaViaje { get; set; }
            public string TipoViaje { get; set; }
            public string Recorrido { get; set; }
            public Operacion Operacion { get; set; }
            public string Varchar1 { get; set; }
            public string Varchar2 { get; set; }
            public string Varchar3 { get; set; }
            public string Varchar4 { get; set; }
            public string Varchar5 { get; set; }
            public string Varchar6 { get; set; }
            public object Float1 { get; set; }
            public object Float2 { get; set; }
            public float Peso { get; set; }
            public int Volumen { get; set; }
            public decimal Bultos { get; set; }
            public int Pallets { get; set; }
            public int ValorACobrar_opcional { get; set; }
            public int ValorCobrado_opcional { get; set; }
            public Conductor Conductor { get; set; }
            public Transporte Transporte { get; set; }
            public Recursosviaje[] RecursosViaje { get; set; }
            public Parada[] Paradas { get; set; }
            public object[] Incidencias { get; set; }
            public int ValorACobrar { get; set; }
            public int ValorCobrado { get; set; }
        }

        public class Depositosalida
        {
            public string __type { get; set; }
            public string RefDepositoExterno { get; set; }
            public object Descripcion { get; set; }
            public object Direccion { get; set; }
            public object NroPuerta { get; set; }
            public object Ciudad { get; set; }
            public object Municipio { get; set; }
            public object Colonia { get; set; }
            public int InicioHorario { get; set; }
            public int FinHorario { get; set; }
            public int TiempoEspera { get; set; }
            public int Latitud { get; set; }
            public int Longitud { get; set; }
            public object Calle { get; set; }
            public object EntreCalle { get; set; }
            public object Barrio { get; set; }
            public object Localidad { get; set; }
            public object Partido { get; set; }
            public object Provincia { get; set; }
            public object Pais { get; set; }
            public object X { get; set; }
            public object Y { get; set; }
            public object DistanciaMaxima_opcional { get; set; }
            public object Eliminado_opcional { get; set; }
            public object CampoDinamico { get; set; }
            public int DistanciaMaxima { get; set; }
            public bool Eliminado { get; set; }
        }

        public class Depositollegada
        {
            public string __type { get; set; }
            public string RefDepositoExterno { get; set; }
            public object Descripcion { get; set; }
            public object Direccion { get; set; }
            public object NroPuerta { get; set; }
            public object Ciudad { get; set; }
            public object Municipio { get; set; }
            public object Colonia { get; set; }
            public int InicioHorario { get; set; }
            public int FinHorario { get; set; }
            public int TiempoEspera { get; set; }
            public int Latitud { get; set; }
            public int Longitud { get; set; }
            public object Calle { get; set; }
            public object EntreCalle { get; set; }
            public object Barrio { get; set; }
            public object Localidad { get; set; }
            public object Partido { get; set; }
            public object Provincia { get; set; }
            public object Pais { get; set; }
            public object X { get; set; }
            public object Y { get; set; }
            public object DistanciaMaxima_opcional { get; set; }
            public object Eliminado_opcional { get; set; }
            public object CampoDinamico { get; set; }
            public int DistanciaMaxima { get; set; }
            public bool Eliminado { get; set; }
        }

        public class Vehiculo
        {
            public string __type { get; set; }
            public string Dominio { get; set; }
            public object DominioSemi { get; set; }
            public object NroSerie { get; set; }
            public object Prestador { get; set; }
            public string Flota { get; set; }
            public object Chasis { get; set; }
            public object Volumen { get; set; }
            public object Peso { get; set; }
            public object Ciudad { get; set; }
            public object Marca { get; set; }
            public object Modelo { get; set; }
            public object Combustible { get; set; }
            public string TipoVehiculo { get; set; }
            public object TipoCarroceria { get; set; }
            public object Propietario { get; set; }
            public object Conductor { get; set; }
            public object CoConductor { get; set; }
            public object Aseguradora { get; set; }
            public object Transporte { get; set; }
            public object Categoria { get; set; }
            public object FechaFabricacion_opcional { get; set; }
            public object IdEstado_opcional { get; set; }
            public object Grupos { get; set; }
            public string ReferenciaExterna { get; set; }
            public object Color { get; set; }
            public object Contrato { get; set; }
            public object TipoCarga { get; set; }
            public object RefTipoVehiculo { get; set; }
            public object Varchar1 { get; set; }
            public object Varchar2 { get; set; }
            public object Varchar3 { get; set; }
            public object Varchar4 { get; set; }
            public object DiasPermitidos { get; set; }
            public object Int1_opcional { get; set; }
            public object Int2_opcional { get; set; }
            public object Float1_opcional { get; set; }
            public object Float2_opcional { get; set; }
            public DateTime? FechaFabricacion { get; set; }
            public int IdEstado { get; set; }
            public int Int1 { get; set; }
            public int Int2 { get; set; }
            public decimal Float1 { get; set; }
            public decimal Float2 { get; set; }
        }

        public class Operacion
        {
            public string Descripcion { get; set; }
            public int IdOperacion { get; set; }
            public object Referencia { get; set; }
            public object Sucursal { get; set; }
            public string ReferenciaExterna { get; set; }
        }

        public class Conductor
        {
            public string __type { get; set; }
            public object Login { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public object EMail { get; set; }
            public object Telefono1 { get; set; }
            public object Telefono2 { get; set; }
            public string ReferenciaExterna { get; set; }
            public string NroDocumento { get; set; }
            public object SincronizarUsuario { get; set; }
            public object Grupo { get; set; }
            public object Licencia { get; set; }
            public DateTime? Expedicion { get; set; }
            public DateTime? Vencimiento { get; set; }
            public object Categoria { get; set; }
            public string TipoDocumento { get; set; }
            public object TipoConductor { get; set; }
            public object IdEstado_opcional { get; set; }
            public object transportes { get; set; }
            public int IdEstado { get; set; }
        }

        public class Transporte
        {
            public string __type { get; set; }
            public object Referencia { get; set; }
            public object ReferenciaAdicional { get; set; }
            public object Descripcion { get; set; }
            public string Cuit { get; set; }
            public string Direccion { get; set; }
            public string Telefono1 { get; set; }
            public string Telefono2 { get; set; }
            public bool HabilitadoAdministrativo { get; set; }
            public bool HabilitadoOperativo { get; set; }
            public object Conductores { get; set; }
            public string RazonSocial { get; set; }
            public string NombreFantasia { get; set; }
            public string Email { get; set; }
            public object TipoTransporte { get; set; }
            public object Partido { get; set; }
            public object Provincia { get; set; }
            public string Localidad { get; set; }
            public object RefernciaExternaEstadoTransporte { get; set; }
            public object Contacto { get; set; }
            public object CodigoPostal { get; set; }
            public object DescripcionEstado { get; set; }
            public object DescripcionGrupoTendering { get; set; }
            public object IdEstado_opcional { get; set; }
            public object HorarioDesdeTendering_opcional { get; set; }
            public object HorarioHastaTendering_opcional { get; set; }
            public object PrioridadTendering_opcional { get; set; }
            public object HorarioPublicacionDesdeTendering_opcional { get; set; }
            public object HorarioPublicacionHastaTendering_opcional { get; set; }
            public object Calificacion_opcional { get; set; }
            public object Latitud_opcional { get; set; }
            public object Longitud_opcional { get; set; }
            public object HabilitadoTendering_opcional { get; set; }
            public int IdEstado { get; set; }
            public int HorarioDesdeTendering { get; set; }
            public int HorarioHastaTendering { get; set; }
            public int PrioridadTendering { get; set; }
            public int HorarioPublicacionDesdeTendering { get; set; }
            public int HorarioPublicacionHastaTendering { get; set; }
            public int Calificacion { get; set; }
            public int Latitud { get; set; }
            public int Longitud { get; set; }
            public bool HabilitadoTendering { get; set; }
        }

        public class Recursosviaje
        {
            public string __type { get; set; }
            public string ReferenciaExterna { get; set; }
            public string Tipo { get; set; }
            public string Descripcion { get; set; }
            public object Calificacion { get; set; }
            public object Prioridad { get; set; }
        }

        public class Parada
        {
            public string __type { get; set; }
            public int Secuencia { get; set; }
            public string RefDocumento { get; set; }
            public object RefDocumentoAdicional { get; set; }
            public object RefDocumentoPedido { get; set; }
            public string TipoParada { get; set; }
            public string Descripcion { get; set; }
            public string Direccion { get; set; }
            public object NroPuerta { get; set; }
            public object EntreCalles { get; set; }
            public object Barrio { get; set; }
            public string Localidad { get; set; }
            public string Partido { get; set; }
            public string Provincia { get; set; }
            public float Latitud { get; set; }
            public float Longitud { get; set; }
            public int Volumen { get; set; }
            public float Peso { get; set; }
            public decimal Bulto { get; set; }
            public int Valor { get; set; }
            public int Pallets { get; set; }
            public object Telefono { get; set; }
            public object Telefono2 { get; set; }
            public object Telefono3 { get; set; }
            public object Email { get; set; }
            public int TiempoEstadia { get; set; }
            public int InicioHorario1 { get; set; }
            public int FinHorario1 { get; set; }
            public int InicioHorario2 { get; set; }
            public int FinHorario2 { get; set; }
            public int Distancia { get; set; }
            public object Observaciones { get; set; }
            public object Varchar1 { get; set; }
            public object Varchar2 { get; set; }
            public int Int1 { get; set; }
            public int Int2 { get; set; }
            public bool RequiereControl { get; set; }
            public object ValorControl { get; set; }
            public DateTime? InicioHorarioPlanificado { get; set; }
            public DateTime? FinHorarioPlanificado { get; set; }
            public DateTime? InicioVisita { get; set; }
            public object InicioVisitaReal { get; set; }
            public Cliente cliente { get; set; }
            public object CodigoPostal { get; set; }
            public object Calle { get; set; }
            public string Pais { get; set; }
            public object Varchar3 { get; set; }
            public object Varchar4 { get; set; }
            public object Varchar5 { get; set; }
            public object Varchar6 { get; set; }
            public object Varchar7 { get; set; }
            public object Varchar8 { get; set; }
            public object Varchar9 { get; set; }
            public object CampoDinamico { get; set; }
            public object Recursos { get; set; }
            public object ValorDeclarado { get; set; }
            public object Costo { get; set; }
            public decimal Float1 { get; set; }
            public decimal Float2 { get; set; }
            public decimal Float3 { get; set; }
            public decimal Float4 { get; set; }
            public DateTime? FinVisita { get; set; }
            public object FinVisitaReal { get; set; }
            public object FechaUltimaModificacion { get; set; }
            public object TiempoDetencionMinimo_opcional { get; set; }
            public int IdParada_opcional { get; set; }
            public int IdOrden_opcional { get; set; }
            public object sumarizarTotalesDesdeItems_opcional { get; set; }
            public Orden Orden { get; set; }
            public Estado[] Estados { get; set; }
            public object[] Incidencias { get; set; }
            public Item[] Items { get; set; }
            public object ClienteDador { get; set; }
            public string Estado { get; set; }
            public string ReferenciaExternaEstado { get; set; }
            public object ReferenciaMotivo { get; set; }
            public int Valor_Declarado { get; set; }
            public int _Costo { get; set; }
            public int Int_1 { get; set; }
            public int Int_2 { get; set; }
            public decimal Float_1 { get; set; }
            public decimal Float_2 { get; set; }
            public decimal Float_3 { get; set; }
            public decimal Float_4 { get; set; }
            public DateTime? Inicio_Visita { get; set; }
            public DateTime? Fin_Visita { get; set; }
            public DateTime? Inicio_VisitaReal { get; set; }
            public DateTime? Fin_VisitaReal { get; set; }
            public DateTime? Fecha_UltimaModificacion { get; set; }
            public int TiempoDetencionMinimo { get; set; }
            public int IdParada { get; set; }
            public int IdOrden { get; set; }
            public bool sumarizarTotalesDesdeItems { get; set; }
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
            public decimal Float1_opcional { get; set; }
            public decimal Float2_opcional { get; set; }
            public object Grupo { get; set; }
            public string NumeroDocumento { get; set; }
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
            public int Int1 { get; set; }
            public int Int2 { get; set; }
            public decimal Float1 { get; set; }
            public decimal Float2 { get; set; }
            public bool CargaExclusiva { get; set; }
            public bool IgnorarOperacion { get; set; }
            public bool IgnorarOperacionDomicilioOrden { get; set; }
            public bool CrearDomicilioOrden { get; set; }
            public bool ActualizarDomicilioOrden { get; set; }
            public bool ValidarDomicilioOrden { get; set; }
            public int Bonificacion { get; set; }
        }

        public class Orden
        {
            public string ReferenciaClienteDador { get; set; }
            public string RazonSocialClienteDador { get; set; }
            public string NombreFantasiaClienteDador { get; set; }
            public string ReferenciaOrdenCliente { get; set; }
            public string RazonSocialOrdenCliente { get; set; }
            public string DescripcionDomicilioOrden { get; set; }
            public string DireccionDomicilioOrden { get; set; }
            public string ReferenciaExternaOrden { get; set; }
            public object Sucursal { get; set; }
            public object Operacion { get; set; }
            public object RefDocumento { get; set; }
            public object RefDocumentoAdicional { get; set; }
            public object RefDocumentoPedido { get; set; }
            public object Tipo { get; set; }
            public object TipoOrden { get; set; }
            public object TipoParada { get; set; }
            public object CategoriaOrden { get; set; }
            public object RefDocumentoRecoleccion { get; set; }
            public string estado { get; set; }
            public object TipoJornada { get; set; }
            public object JornadaDescripcion { get; set; }
            public DateTime? FechaJornada { get; set; }
            public object Cliente { get; set; }
            public object Descripcion { get; set; }
            public object Direccion { get; set; }
            public string Calle { get; set; }
            public string NroPuerta { get; set; }
            public string EntreCalle { get; set; }
            public string Barrio { get; set; }
            public string Localidad { get; set; }
            public object Partido { get; set; }
            public string Provincia { get; set; }
            public string Pais { get; set; }
            public int InicioHorario1 { get; set; }
            public int FinHorario1 { get; set; }
            public int InicioHorario2 { get; set; }
            public int FinHorario2 { get; set; }
            public int TiempoEspera { get; set; }
            public object CrearDomicilio { get; set; }
            public int orden { get; set; }
            public DateTime? InicioHorarioPlanificado { get; set; }
            public DateTime? FinHorarioPlanificado { get; set; }
            public int Volumen { get; set; }
            public float Peso { get; set; }
            public decimal Bulto { get; set; }
            public int Pallets { get; set; }
            public int Distancia { get; set; }
            public float Latitud { get; set; }
            public float Longitud { get; set; }
            public object Observaciones { get; set; }
            public object Email { get; set; }
            public object Telefono { get; set; }
            public object Telefono2 { get; set; }
            public object Telefono3 { get; set; }
            public object CodigoPostal { get; set; }
            public object Varchar1 { get; set; }
            public object Varchar2 { get; set; }
            public object Varchar3 { get; set; }
            public object Varchar4 { get; set; }
            public object Varchar5 { get; set; }
            public object Varchar6 { get; set; }
            public object Varchar7 { get; set; }
            public object Varchar8 { get; set; }
            public object Varchar9 { get; set; }
            public int Int1 { get; set; }
            public int Int2 { get; set; }
            public int IdRuta { get; set; }
            public int PreOrden { get; set; }
            public int GrupoConsolidacion { get; set; }
            public int PrioridadAbsoluta { get; set; }
            public int GrupoRutas { get; set; }
            public int TiempoLatencia { get; set; }
            public int TiempoEsperaPromedio { get; set; }
            public object MotivoEstadoParadaDescripcion { get; set; }
            public object MotivoEstadoParadaReferencia { get; set; }
            public object[] Items { get; set; }
            public object[] Servicios { get; set; }
            public object[] Fotos { get; set; }
            public object[] ParadaItems { get; set; }
            public object DepositoSalida { get; set; }
            public object DepositoLlegada { get; set; }
            public object Remito { get; set; }
            public object EstadoOrden { get; set; }
            public object CampoDinamico { get; set; }
            public int IdOrden_opcional { get; set; }
            public int IdJornada_opcional { get; set; }
            public object PesoMaximo_opcional { get; set; }
            public object VolumenMaximo_opcional { get; set; }
            public object BultosMaximo_opcional { get; set; }
            public object CostoTotal { get; set; }
            public object InicioVisita_opcional { get; set; }
            public object FinVisita_opcional { get; set; }
            public object InicioHorarioEstimado_opcional { get; set; }
            public object FinHorarioEstimado_opcional { get; set; }
            public object InicioHorarioReal_opcional { get; set; }
            public object FinHorarioReal_opcional { get; set; }
            public object usarProductos_opcional { get; set; }
            public object UltimaVisita_opcional { get; set; }
            public object Fecha_opcional { get; set; }
            public object FechaOrden_opcional { get; set; }
            public object Datetime1_opcional { get; set; }
            public object Datetime2_opcional { get; set; }
            public object Datetime3_opcional { get; set; }
            public object FechaUltimoCambioEstado_opcional { get; set; }
            public object Unidades_opcional { get; set; }
            public object altaProductos_opcional { get; set; }
            public object obligarProductoItems_opcional { get; set; }
            public object Float1_opcional { get; set; }
            public object Float2_opcional { get; set; }
            public object Float3_opcional { get; set; }
            public object Float4_opcional { get; set; }
            public int IdOrden { get; set; }
            public int IdJornada { get; set; }
            public int PesoMaximo { get; set; }
            public int VolumenMaximo { get; set; }
            public decimal BultosMaximo { get; set; }
            public int Costo { get; set; }
            public int InicioVisita { get; set; }
            public int FinVisita { get; set; }
            public DateTime? InicioHorarioEstimado { get; set; }
            public DateTime? FinHorarioEstimado { get; set; }
            public DateTime? InicioHorarioReal { get; set; }
            public DateTime? FinHorarioReal { get; set; }
            public bool usarProductos { get; set; }
            public bool UltimaVisita { get; set; }
            public DateTime? Fecha { get; set; }
            public DateTime? FechaOrden { get; set; }
            public DateTime? Datetime1 { get; set; }
            public DateTime? Datetime2 { get; set; }
            public DateTime? Datetime3 { get; set; }
            public DateTime? FechaUltimoCambioEstado { get; set; }
            public int Unidades { get; set; }
            public bool altaProductos { get; set; }
            public bool obligarProductoItems { get; set; }
            public decimal Float1 { get; set; }
            public decimal Float2 { get; set; }
            public decimal Float3 { get; set; }
            public decimal Float4 { get; set; }
        }

        public class Estado
        {
            public string __type { get; set; }
            public string RefDocumento { get; set; }
            public string estado { get; set; }
            public object Motivo { get; set; }
            public DateTime? EstadoFecha { get; set; }
            public string Observaciones { get; set; }
            public int Latitud { get; set; }
            public int Longitud { get; set; }
            public int IdViaje { get; set; }
            public int IdParadaTraceEstado { get; set; }
            public object ReferenciaViaje { get; set; }
            public string Login { get; set; }
            public object Contenido { get; set; }
            public object _MismoEstado { get; set; }
            public object _ValidarTransicion { get; set; }
            public object Items { get; set; }
            public object ArchivosAsociados { get; set; }
            public object CampoDinamico { get; set; }
            public bool MismoEstado { get; set; }
            public bool ValidarTransicion { get; set; }
        }

        public class Item
        {
            public string __type { get; set; }
            public string RefDocumento { get; set; }
            public object RefDocumentoAdicional { get; set; }
            public string Descripcion { get; set; }
            public decimal Cantidad { get; set; }
            public int Volumen { get; set; }
            public float Peso { get; set; }
            public decimal Bulto { get; set; }
            public int Pallets { get; set; }
            public object MotivoEntrega { get; set; }
            public object Producto { get; set; }
            public object TipoRotacionPermitida { get; set; }
            public object Varchar1 { get; set; }
            public object Varchar2 { get; set; }
            public object Varchar3 { get; set; }
            public object UnidadMedida { get; set; }
            public object CodigoProducto { get; set; }
            public object ValorCorte { get; set; }
            public object ApilablePermitido { get; set; }
            public object Etiquetas { get; set; }
            public object CampoDinamico { get; set; }
            public string Estado { get; set; }
            public string ReferenciaExternaEstado { get; set; }
            public int Valor_Corte { get; set; }
            public int Apilable_Permitido { get; set; }
            public decimal CantidadEntregada { get; set; }
            public decimal CantidadReal { get; set; }
        }

    }
}
