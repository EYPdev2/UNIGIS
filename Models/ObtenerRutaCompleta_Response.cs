using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace ActualizadorDoctosUnigis
{
    public class ObtenerRutaCompleta_Response
    {




        public class ObtenerRutaCompletaResponse
        {

            public ObtenerRutaCompletaResult d { get; set; }



        }
        public class ListaCliente
        {
            public string RefCliente { get; set; }
            public string Latitud { get; set; }
            public string Longitud { get; set; }
            public string RefDomicilioExterno { get; set; }
            public string InicioHorario1 { get; set; }
            public string InicioHorario2 { get; set; }
            public string TiempoEspera { get; set; }
            public int Int1 { get; set; }
            public int Int2 { get; set; }
            public float Folat1 { get; set; }
            public float Float2 { get; set; }
            public int Bonificacion { get; set; }

        }


        public class Ordenes
        {
            public int IdOrdenItem { get; set; }
            public string Sucursal { get; set; }
            public string Operación { get; set; }
            public string RefDocumento { get; set; }
            public string RefDocumentoAdicional { get; set; }
            public string RefDocumentoPedido { get; set; }
            public string Tipo { get; set; }
            public string CategoriaOrden { get; set; }
            public string Estado { get; set; }
            public DateTime     FechaJornada { get; set; }
            public ListaCliente Cliente { get; set; }
            public string Descripcion { get; set; }
            public string Direccion { get; set; }
            public string calle { get; set; }
            public string NroPuerta { get; set; }
            public string EntreCalle { get; set; }
            public string Barrio { get; set; }
            public string Localidad { get; set; }
            public string Partido { get; set; }
            public string Provincia { get; set; }
            public string Pais { get; set; }
            public int InicioHorario1 { get; set; }
            public int FinHorario1 { get; set; }
            public int InicioHorario2 { get; set; }
            public int FinHorario2 { get; set; }
            public int TiempoEspera { get; set; }
            public string CrearDomicilio { get; set; }
            public int Orden { get; set; }
            public DateTime InicioHorarioPlanificado { get; set; }
            public DateTime FinHorarioPlanificado { get; set; }
            public decimal Volumen { get; set; }
            public decimal Peso { get; set; }
            public double Bulto { get; set; }
            public decimal Pallets { get; set; }
            public string Distancia { get; set; }
            public float Latitud { get; set; }
            public float Longitud { get; set; }
            public string Observaciones { get; set; }
            public int Int1 { get; set; }
            public int Int2 { get; set; }
            public string IdRuta { get; set; }
            public string PreOrden { get; set; }
            public string GrupoConsolidacion { get; set; }
            public string PrioridadAbsoluta { get; set; }
            public string GrupoRutas { get; set; }
            public string TiempoLatencia { get; set; }
            public string TiempoEsperaPromedio { get; set; }
            public DepositoSalida DepositoSalida { get; set; }
            public DepositoLlegada DepositoLlegada { get; set; }
            public List<Items> Items { get; set; }
            public int IdOrden { get; set; }
            public int IdJornada { get; set; }
            public decimal PesoMaximo { get; set; }
            public decimal VolimenMaximo { get; set; }
            public decimal BultosMaximo { get; set; }
            public decimal Costo { get; set; }
            public int InicioVisita { get; set; }
            public int FinVisita { get; set; }
            public decimal Unidades { get; set; }
            public bool altaproductos { get; set; }
            public float Float1 { get; set; }
            public float Float2 { get; set; }
            public float Float3 { get; set; }
            public float Float4 { get; set; }
            public bool ValidarTransicion { get; set; }
        }

        public class pOrdenEntrega
        {

            public string Sucursal { get; set; }
            public string Operación { get; set; }
            public string RefDocumento { get; set; }
            public string RefDocumentoAdicional { get; set; }
            public string RefDocumentoPedido { get; set; }
            public string Tipo { get; set; }
            public string CategoriaOrden { get; set; }
            public string Estado { get; set; }
            public DateTime FechaJornada { get; set; }
            public List<ListaCliente> Cliente { get; set; }
            public string Descripcion { get; set; }
            public string Direccion { get; set; }
            public string calle { get; set; }
            public string NroPuerta { get; set; }
            public string EntreCalle { get; set; }
            public string Barrio { get; set; }
            public string Localidad { get; set; }
            public string Partido { get; set; }
            public string Provincia { get; set; }
            public string Pais { get; set; }
            public int InicioHorario1 { get; set; }
            public int FinHorario1 { get; set; }
            public int InicioHorario2 { get; set; }
            public int FinHorario2 { get; set; }
            public int TiempoEspera { get; set; }
            public string CrearDomicilio { get; set; }
            public int Orden { get; set; }
            public DateTime InicioHorarioPlanificado { get; set; }
            public DateTime FinHorarioPlanificado { get; set; }
            public decimal Volumen { get; set; }
            public decimal Peso { get; set; }
            public int Bulto { get; set; }
            public int Pallets { get; set; }
            public string Distancia { get; set; }
            public float Latitud { get; set; }
            public float Longitud { get; set; }
            public string Observaciones { get; set; }
            public int Int1 { get; set; }
            public int Int2 { get; set; }
            public string IdRuta { get; set; }
            public string PreOrden { get; set; }
            public string GrupoConsolidacion { get; set; }
            public string PrioridadAbsoluta { get; set; }
            public string GrupoRutas { get; set; }
            public string TiempoLatencia { get; set; }
            public string TiempoEsperaPromedio { get; set; }
            public DepositoSalida DepositoSalida { get; set; }
            public DepositoLlegada DepositoLlegada { get; set; }
            public Items Items { get; set; }
            public int IdOrden { get; set; }
            public int IdJornada { get; set; }
            public decimal PesoMaximo { get; set; }
            public decimal VolimenMaximo { get; set; }
            public decimal BultosMaximo { get; set; }
            public decimal Costo { get; set; }
            public int InicioVisita { get; set; }
            public int FinVisita { get; set; }
            public decimal Unidades { get; set; }
            public bool altaproductos { get; set; }
            public float Float1 { get; set; }
            public float Float2 { get; set; }
            public float Float3 { get; set; }
            public float Float4 { get; set; }
        }


        public class pOrdenEntregaItems
        {


        }


        public class Items
        {
            public int IdOrdenItem { get; set; }
            public string RefDocumento { get; set; }
            public string Descripcion { get; set; }
            public string RefDocumentoAdicional { get; set; }
            public double Cantidad { get; set; }
            public decimal Volumen { get; set; }
            public double Peso { get; set; }
            public double Bulto { get; set; }
            public decimal Unidades { get; set; }
            public int Int1 { get; set; }
            public int Int2 { get; set; }
            public string Varchar1 { get; set; }
            public decimal ReferenciaCantidad { get; set; }
            public decimal ReferenciaValor { get; set; }
            public bool ProcesarVolumetria { get; set; }
            public decimal PrecioUnitario { get; set; }

            public int Pallets { get; set; }
        }

        public class DepositoSalida
        {
            public string RefDepositoExterno { get; set; }
            public string Descripcion { get; set; }
            public int InicioHorario { get; set; }
            public int FinHorario { get; set; }
            public int TiempoEspera { get; set; }
            public float Latitud { get; set; }
            public float Longitud { get; set; }
            public string X { get; set; }
            public string Y { get; set; }

        }

        public class DepositoLlegada
        {
            public string RefDepositoExterno { get; set; }
            public string Descripcion { get; set; }
            public int InicioHorario { get; set; }
            public int FinHorario { get; set; }
            public int TiempoEspera { get; set; }
            public float Latitud { get; set; }
            public float Longitud { get; set; }
            public string X { get; set; }
            public string Y { get; set; }

        }

        public class ObtenerRutaCompletaResult
        {

            public int IdRuta { get; set; }
            public  Vehiculo  Vehiculo { get; set; }
            public DepositoSalida DepositoSalida { get; set; }
            public DepositoLlegada DepositoLlegada { get; set; }
            public DateTime? FechaHoraSalida { get; set; }
            public DateTime? FechaHoraLlegada { get; set; }
            public DateTime? FechaHoraCarga { get; set; }
            public string Muelle { get; set; }
            public string Conductor { get; set; }
            public string TipoVehiculo { get; set; }
            public int Ruta { get; set; }
            public string Descripcion { get; set; }
            public int CantidadOrdenes { get; set; }
            public string ReferenciaExterna { get; set; }
            public int IdViaje { get; set; }
            public string Sucursal { get; set; }
            public string Operacion { get; set; }
            public string Estado { get; set; }
            public int EstadoJornada { get; set; }
            public int IdJornada { get; set; }
            public decimal Bultos { get; set; }
            public decimal Peso { get; set; }
            public decimal Volumen { get; set; }
            public decimal Pallets { get; set; }
            public decimal Distancia { get; set; }
            public decimal Costo { get; set; }
            public int Tiempo { get; set; }
            public int NumeroVuelta { get; set; }
            public string RazonSocialTransporte { get; set; }
            public string CUIT_transporte { get; set; }
            public string Int1 { get; set; }
            public string Int2 { get; set; }
            public string Float1 { get; set; }
            public string Float2 { get; set; }
            public int TiempoInactividad { get; set; }
            public int InicioInactividad { get; set; }
            public int FinInactividad { get; set; }
            public string CopiarDescripcionOrdenes { get; set; }
            public string PivotearPrimerVisita { get; set; }
            public List<Ordenes> Ordenes { get; set; }
            public bool PermiteCrearPedidos { get; set; }

        }
        public class Vehiculo
        {
            public string __type { get; set; }
            
            public string NroSerie { get; set; }
            public List<String> grupos { get; set; }

            public string Dominio { get; set; }
            public string DominioSemi { get; set; }
 
            public string Prestador { get; set; }
            public string Flota { get; set; }
            public string Chasis { get; set; }
            public string Volumen { get; set; }
        public string Peso { get; set; }
        public string Ciudad { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Combustible { get; set; }
        public string TipoVehiculo { get; set; }
        public string TipoCarroceria { get; set; }
        public string Propietario { get; set; }
        public string Conductor { get; set; }
        public string CoConductor { get; set; }
        public string Aseguradora { get; set; }
        public string Transporte { get; set; }
        public string Categoria { get; set; }

        public string ReferenciaExterna { get; set; }
        public string Color { get; set; }
        public string Contrato { get; set; }
        public string TipoCarga { get; set; }
        public string RefTipoVehiculo { get; set; }
        public string Varchar1 { get; set; }
        public string Varchar2 { get; set; }
        public string Varchar3 { get; set; }
        public string Varchar4 { get; set; }
        public string DiasPermitidos { get; set; }
        public DateTime FechaFabricacio { get; set; }
        public int IdEstado { get; set; }
        public int Int1 { get; set; }
        public int Int2 { get; set; }
        public float Float1
        { get; set; }
        public float Float2 { get; set; }
    }
        





}
}
