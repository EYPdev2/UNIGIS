using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualizadorDoctosUnigis.Models
{
    class ModificarParada
    {

        // NOTA: El código generado puede requerir, como mínimo, .NET Framework 4.5 o .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
        public partial class Envelope
        {

            private EnvelopeBody bodyField;

            /// <remarks/>
            public EnvelopeBody Body
            {
                get
                {
                    return this.bodyField;
                }
                set
                {
                    this.bodyField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public partial class EnvelopeBody
        {

            private ConsultarParadaPorIdResponse consultarParadaPorIdResponseField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://unisolutions.com.ar/")]
            public ConsultarParadaPorIdResponse ConsultarParadaPorIdResponse
            {
                get
                {
                    return this.consultarParadaPorIdResponseField;
                }
                set
                {
                    this.consultarParadaPorIdResponseField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://unisolutions.com.ar/")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://unisolutions.com.ar/", IsNullable = false)]
        public partial class ConsultarParadaPorIdResponse
        {

            private ConsultarParadaPorIdResponseConsultarParadaPorIdResult consultarParadaPorIdResultField;

            /// <remarks/>
            public ConsultarParadaPorIdResponseConsultarParadaPorIdResult ConsultarParadaPorIdResult
            {
                get
                {
                    return this.consultarParadaPorIdResultField;
                }
                set
                {
                    this.consultarParadaPorIdResultField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://unisolutions.com.ar/")]
        public partial class ConsultarParadaPorIdResponseConsultarParadaPorIdResult
        {

            private string resultadoField;

            private string viajeFinalizadoField;

            private string idViajeField;

            private string viajeReferenciaExternaField;

            private string secuenciaField;

            private string estadoParadaField;

            private string horarioParadaRealInicioField;

            private string horarioParadaRealFinField;

            private string refDocumentoField;

            private string refDocumentoAdicionalField;

            private string refClienteField;

            private string transporteField;

            private string dominioField;

            private string conductorField;

            private string uRLField;

            private string referenciaMotivoField;

            private string observacionesField;

            private string descripcionField;

            private string direccionField;

            private string paisField;

            private string motivoField;

            private string motivoReferenciaField;

            private string formaDePagoField;

            private string refExternaFormaPagoField;

            private string nroComprobantePagoField;

            private string inicioHorarioEstimadoField;

            private string finHorarioEstimadoField;

            private string inicioHorarioPlanificadoField;

            private string finHorarioPlanificadoField;

            private string eTAField;

            private ConsultarParadaPorIdResponseConsultarParadaPorIdResultCliente clienteField;

            private ConsultarParadaPorIdResponseConsultarParadaPorIdResultClienteDador clienteDadorField;

            private ConsultarParadaPorIdResponseConsultarParadaPorIdResultOrdenParada ordenParadaField;

            private ConsultarParadaPorIdResponseConsultarParadaPorIdResultPFoto[] fotosField;

            private ConsultarParadaPorIdResponseConsultarParadaPorIdResultPBitacora[] bitacoraField;

            private ConsultarParadaPorIdResponseConsultarParadaPorIdResultConsultarParadaResultadoItem[] itemsField;

            private ConsultarParadaPorIdResponseConsultarParadaPorIdResultPRecursos[] recursosField;

            private ConsultarParadaPorIdResponseConsultarParadaPorIdResultCampoValor[] dinamicosField;

            private string latitudField;

            private string bultosField;

            private string pesoField;

            private string valorDeclaradoField;

            private string longitudField;

            private string valorACobrarField;

            private string valorCobradoField;

            private string idFormaPagoField;

            /// <remarks/>
            public string Resultado
            {
                get
                {
                    return this.resultadoField;
                }
                set
                {
                    this.resultadoField = value;
                }
            }

            /// <remarks/>
            public string ViajeFinalizado
            {
                get
                {
                    return this.viajeFinalizadoField;
                }
                set
                {
                    this.viajeFinalizadoField = value;
                }
            }

            /// <remarks/>
            public string IdViaje
            {
                get
                {
                    return this.idViajeField;
                }
                set
                {
                    this.idViajeField = value;
                }
            }

            /// <remarks/>
            public string ViajeReferenciaExterna
            {
                get
                {
                    return this.viajeReferenciaExternaField;
                }
                set
                {
                    this.viajeReferenciaExternaField = value;
                }
            }

            /// <remarks/>
            public string Secuencia
            {
                get
                {
                    return this.secuenciaField;
                }
                set
                {
                    this.secuenciaField = value;
                }
            }

            /// <remarks/>
            public string EstadoParada
            {
                get
                {
                    return this.estadoParadaField;
                }
                set
                {
                    this.estadoParadaField = value;
                }
            }

            /// <remarks/>
            public string HorarioParadaRealInicio
            {
                get
                {
                    return this.horarioParadaRealInicioField;
                }
                set
                {
                    this.horarioParadaRealInicioField = value;
                }
            }

            /// <remarks/>
            public string HorarioParadaRealFin
            {
                get
                {
                    return this.horarioParadaRealFinField;
                }
                set
                {
                    this.horarioParadaRealFinField = value;
                }
            }

            /// <remarks/>
            public string RefDocumento
            {
                get
                {
                    return this.refDocumentoField;
                }
                set
                {
                    this.refDocumentoField = value;
                }
            }

            /// <remarks/>
            public string RefDocumentoAdicional
            {
                get
                {
                    return this.refDocumentoAdicionalField;
                }
                set
                {
                    this.refDocumentoAdicionalField = value;
                }
            }

            /// <remarks/>
            public string RefCliente
            {
                get
                {
                    return this.refClienteField;
                }
                set
                {
                    this.refClienteField = value;
                }
            }

            /// <remarks/>
            public string Transporte
            {
                get
                {
                    return this.transporteField;
                }
                set
                {
                    this.transporteField = value;
                }
            }

            /// <remarks/>
            public string Dominio
            {
                get
                {
                    return this.dominioField;
                }
                set
                {
                    this.dominioField = value;
                }
            }

            /// <remarks/>
            public string Conductor
            {
                get
                {
                    return this.conductorField;
                }
                set
                {
                    this.conductorField = value;
                }
            }

            /// <remarks/>
            public string URL
            {
                get
                {
                    return this.uRLField;
                }
                set
                {
                    this.uRLField = value;
                }
            }

            /// <remarks/>
            public string ReferenciaMotivo
            {
                get
                {
                    return this.referenciaMotivoField;
                }
                set
                {
                    this.referenciaMotivoField = value;
                }
            }

            /// <remarks/>
            public string Observaciones
            {
                get
                {
                    return this.observacionesField;
                }
                set
                {
                    this.observacionesField = value;
                }
            }

            /// <remarks/>
            public string Descripcion
            {
                get
                {
                    return this.descripcionField;
                }
                set
                {
                    this.descripcionField = value;
                }
            }

            /// <remarks/>
            public string Direccion
            {
                get
                {
                    return this.direccionField;
                }
                set
                {
                    this.direccionField = value;
                }
            }

            /// <remarks/>
            public string Pais
            {
                get
                {
                    return this.paisField;
                }
                set
                {
                    this.paisField = value;
                }
            }

            /// <remarks/>
            public string Motivo
            {
                get
                {
                    return this.motivoField;
                }
                set
                {
                    this.motivoField = value;
                }
            }

            /// <remarks/>
            public string MotivoReferencia
            {
                get
                {
                    return this.motivoReferenciaField;
                }
                set
                {
                    this.motivoReferenciaField = value;
                }
            }

            /// <remarks/>
            public string FormaDePago
            {
                get
                {
                    return this.formaDePagoField;
                }
                set
                {
                    this.formaDePagoField = value;
                }
            }

            /// <remarks/>
            public string RefExternaFormaPago
            {
                get
                {
                    return this.refExternaFormaPagoField;
                }
                set
                {
                    this.refExternaFormaPagoField = value;
                }
            }

            /// <remarks/>
            public string NroComprobantePago
            {
                get
                {
                    return this.nroComprobantePagoField;
                }
                set
                {
                    this.nroComprobantePagoField = value;
                }
            }

            /// <remarks/>
            public string InicioHorarioEstimado
            {
                get
                {
                    return this.inicioHorarioEstimadoField;
                }
                set
                {
                    this.inicioHorarioEstimadoField = value;
                }
            }

            /// <remarks/>
            public string FinHorarioEstimado
            {
                get
                {
                    return this.finHorarioEstimadoField;
                }
                set
                {
                    this.finHorarioEstimadoField = value;
                }
            }

            /// <remarks/>
            public string InicioHorarioPlanificado
            {
                get
                {
                    return this.inicioHorarioPlanificadoField;
                }
                set
                {
                    this.inicioHorarioPlanificadoField = value;
                }
            }

            /// <remarks/>
            public string FinHorarioPlanificado
            {
                get
                {
                    return this.finHorarioPlanificadoField;
                }
                set
                {
                    this.finHorarioPlanificadoField = value;
                }
            }

            /// <remarks/>
            public string ETA
            {
                get
                {
                    return this.eTAField;
                }
                set
                {
                    this.eTAField = value;
                }
            }

            /// <remarks/>
            public ConsultarParadaPorIdResponseConsultarParadaPorIdResultCliente Cliente
            {
                get
                {
                    return this.clienteField;
                }
                set
                {
                    this.clienteField = value;
                }
            }

            /// <remarks/>
            public ConsultarParadaPorIdResponseConsultarParadaPorIdResultClienteDador ClienteDador
            {
                get
                {
                    return this.clienteDadorField;
                }
                set
                {
                    this.clienteDadorField = value;
                }
            }

            /// <remarks/>
            public ConsultarParadaPorIdResponseConsultarParadaPorIdResultOrdenParada OrdenParada
            {
                get
                {
                    return this.ordenParadaField;
                }
                set
                {
                    this.ordenParadaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("pFoto", IsNullable = false)]
            public ConsultarParadaPorIdResponseConsultarParadaPorIdResultPFoto[] Fotos
            {
                get
                {
                    return this.fotosField;
                }
                set
                {
                    this.fotosField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("pBitacora", IsNullable = false)]
            public ConsultarParadaPorIdResponseConsultarParadaPorIdResultPBitacora[] Bitacora
            {
                get
                {
                    return this.bitacoraField;
                }
                set
                {
                    this.bitacoraField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("ConsultarParadaResultadoItem", IsNullable = false)]
            public ConsultarParadaPorIdResponseConsultarParadaPorIdResultConsultarParadaResultadoItem[] Items
            {
                get
                {
                    return this.itemsField;
                }
                set
                {
                    this.itemsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("pRecursos", IsNullable = false)]
            public ConsultarParadaPorIdResponseConsultarParadaPorIdResultPRecursos[] Recursos
            {
                get
                {
                    return this.recursosField;
                }
                set
                {
                    this.recursosField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("CampoValor", IsNullable = false)]
            public ConsultarParadaPorIdResponseConsultarParadaPorIdResultCampoValor[] Dinamicos
            {
                get
                {
                    return this.dinamicosField;
                }
                set
                {
                    this.dinamicosField = value;
                }
            }

            /// <remarks/>
            public string Latitud
            {
                get
                {
                    return this.latitudField;
                }
                set
                {
                    this.latitudField = value;
                }
            }

            /// <remarks/>
            public string Bultos
            {
                get
                {
                    return this.bultosField;
                }
                set
                {
                    this.bultosField = value;
                }
            }

            /// <remarks/>
            public string Peso
            {
                get
                {
                    return this.pesoField;
                }
                set
                {
                    this.pesoField = value;
                }
            }

            /// <remarks/>
            public string ValorDeclarado
            {
                get
                {
                    return this.valorDeclaradoField;
                }
                set
                {
                    this.valorDeclaradoField = value;
                }
            }

            /// <remarks/>
            public string Longitud
            {
                get
                {
                    return this.longitudField;
                }
                set
                {
                    this.longitudField = value;
                }
            }

            /// <remarks/>
            public string ValorACobrar
            {
                get
                {
                    return this.valorACobrarField;
                }
                set
                {
                    this.valorACobrarField = value;
                }
            }

            /// <remarks/>
            public string ValorCobrado
            {
                get
                {
                    return this.valorCobradoField;
                }
                set
                {
                    this.valorCobradoField = value;
                }
            }

            /// <remarks/>
            public string IdFormaPago
            {
                get
                {
                    return this.idFormaPagoField;
                }
                set
                {
                    this.idFormaPagoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://unisolutions.com.ar/")]
        public partial class ConsultarParadaPorIdResponseConsultarParadaPorIdResultCliente
        {

            private string refClienteField;

            private string razonSocialField;

            private string telefonoField;

            private string telefono2Field;

            private string telefono3Field;

            private string eMailField;

            private string direccionField;

            private string calleField;

            private string numeroPuertaField;

            private string entreCalleField;

            private string barrioField;

            private string localidadField;

            private string partidoField;

            private string provinciaField;

            private string paisField;

            private string latitudField;

            private string longitudField;

            private string refDomicilioExternoField;

            private string domicilioDescripcionField;

            private string inicioHorario1Field;

            private string inicioHorario2Field;

            private string finHorario1Field;

            private string finHorario2Field;

            private string tiempoEsperaField;

            private string varchar1Field;

            private string varchar2Field;

            private string grupoField;

            private string numeroDocumentoField;

            private string tipoDocumentoField;

            private string domicilioCodigoPostalField;

            private ConsultarParadaPorIdResponseConsultarParadaPorIdResultClienteDomicilioFiscal domicilioFiscalField;

            private object[] campoDinamicoField;

            private string contactoField;

            private string razonSocialFiscalField;

            private string identificadorFiscalField;

            private string requiereTurnoField;

            private object[] campoDinamicoDomicilioField;

            private string refExternaDocumentoFiscalField;

            private string grupoTramoField;

            private string int1Field;

            private string int2Field;

            private string float1Field;

            private string float2Field;

            private string cargaExclusivaField;

            private string ignorarOperacionField;

            private string ignorarOperacionDomicilioOrdenField;

            private string crearDomicilioOrdenField;

            private string actualizarDomicilioOrdenField;

            private string validarDomicilioOrdenField;

            private string bonificacionField;

            /// <remarks/>
            public string RefCliente
            {
                get
                {
                    return this.refClienteField;
                }
                set
                {
                    this.refClienteField = value;
                }
            }

            /// <remarks/>
            public string RazonSocial
            {
                get
                {
                    return this.razonSocialField;
                }
                set
                {
                    this.razonSocialField = value;
                }
            }

            /// <remarks/>
            public string Telefono
            {
                get
                {
                    return this.telefonoField;
                }
                set
                {
                    this.telefonoField = value;
                }
            }

            /// <remarks/>
            public string Telefono2
            {
                get
                {
                    return this.telefono2Field;
                }
                set
                {
                    this.telefono2Field = value;
                }
            }

            /// <remarks/>
            public string Telefono3
            {
                get
                {
                    return this.telefono3Field;
                }
                set
                {
                    this.telefono3Field = value;
                }
            }

            /// <remarks/>
            public string EMail
            {
                get
                {
                    return this.eMailField;
                }
                set
                {
                    this.eMailField = value;
                }
            }

            /// <remarks/>
            public string Direccion
            {
                get
                {
                    return this.direccionField;
                }
                set
                {
                    this.direccionField = value;
                }
            }

            /// <remarks/>
            public string Calle
            {
                get
                {
                    return this.calleField;
                }
                set
                {
                    this.calleField = value;
                }
            }

            /// <remarks/>
            public string NumeroPuerta
            {
                get
                {
                    return this.numeroPuertaField;
                }
                set
                {
                    this.numeroPuertaField = value;
                }
            }

            /// <remarks/>
            public string EntreCalle
            {
                get
                {
                    return this.entreCalleField;
                }
                set
                {
                    this.entreCalleField = value;
                }
            }

            /// <remarks/>
            public string Barrio
            {
                get
                {
                    return this.barrioField;
                }
                set
                {
                    this.barrioField = value;
                }
            }

            /// <remarks/>
            public string Localidad
            {
                get
                {
                    return this.localidadField;
                }
                set
                {
                    this.localidadField = value;
                }
            }

            /// <remarks/>
            public string Partido
            {
                get
                {
                    return this.partidoField;
                }
                set
                {
                    this.partidoField = value;
                }
            }

            /// <remarks/>
            public string Provincia
            {
                get
                {
                    return this.provinciaField;
                }
                set
                {
                    this.provinciaField = value;
                }
            }

            /// <remarks/>
            public string Pais
            {
                get
                {
                    return this.paisField;
                }
                set
                {
                    this.paisField = value;
                }
            }

            /// <remarks/>
            public string Latitud
            {
                get
                {
                    return this.latitudField;
                }
                set
                {
                    this.latitudField = value;
                }
            }

            /// <remarks/>
            public string Longitud
            {
                get
                {
                    return this.longitudField;
                }
                set
                {
                    this.longitudField = value;
                }
            }

            /// <remarks/>
            public string RefDomicilioExterno
            {
                get
                {
                    return this.refDomicilioExternoField;
                }
                set
                {
                    this.refDomicilioExternoField = value;
                }
            }

            /// <remarks/>
            public string DomicilioDescripcion
            {
                get
                {
                    return this.domicilioDescripcionField;
                }
                set
                {
                    this.domicilioDescripcionField = value;
                }
            }

            /// <remarks/>
            public string InicioHorario1
            {
                get
                {
                    return this.inicioHorario1Field;
                }
                set
                {
                    this.inicioHorario1Field = value;
                }
            }

            /// <remarks/>
            public string InicioHorario2
            {
                get
                {
                    return this.inicioHorario2Field;
                }
                set
                {
                    this.inicioHorario2Field = value;
                }
            }

            /// <remarks/>
            public string FinHorario1
            {
                get
                {
                    return this.finHorario1Field;
                }
                set
                {
                    this.finHorario1Field = value;
                }
            }

            /// <remarks/>
            public string FinHorario2
            {
                get
                {
                    return this.finHorario2Field;
                }
                set
                {
                    this.finHorario2Field = value;
                }
            }

            /// <remarks/>
            public string TiempoEspera
            {
                get
                {
                    return this.tiempoEsperaField;
                }
                set
                {
                    this.tiempoEsperaField = value;
                }
            }

            /// <remarks/>
            public string Varchar1
            {
                get
                {
                    return this.varchar1Field;
                }
                set
                {
                    this.varchar1Field = value;
                }
            }

            /// <remarks/>
            public string Varchar2
            {
                get
                {
                    return this.varchar2Field;
                }
                set
                {
                    this.varchar2Field = value;
                }
            }

            /// <remarks/>
            public string Grupo
            {
                get
                {
                    return this.grupoField;
                }
                set
                {
                    this.grupoField = value;
                }
            }

            /// <remarks/>
            public string NumeroDocumento
            {
                get
                {
                    return this.numeroDocumentoField;
                }
                set
                {
                    this.numeroDocumentoField = value;
                }
            }

            /// <remarks/>
            public string TipoDocumento
            {
                get
                {
                    return this.tipoDocumentoField;
                }
                set
                {
                    this.tipoDocumentoField = value;
                }
            }

            /// <remarks/>
            public string DomicilioCodigoPostal
            {
                get
                {
                    return this.domicilioCodigoPostalField;
                }
                set
                {
                    this.domicilioCodigoPostalField = value;
                }
            }

            /// <remarks/>
            public ConsultarParadaPorIdResponseConsultarParadaPorIdResultClienteDomicilioFiscal DomicilioFiscal
            {
                get
                {
                    return this.domicilioFiscalField;
                }
                set
                {
                    this.domicilioFiscalField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("CampoValor")]
            public object[] CampoDinamico
            {
                get
                {
                    return this.campoDinamicoField;
                }
                set
                {
                    this.campoDinamicoField = value;
                }
            }

            /// <remarks/>
            public string Contacto
            {
                get
                {
                    return this.contactoField;
                }
                set
                {
                    this.contactoField = value;
                }
            }

            /// <remarks/>
            public string RazonSocialFiscal
            {
                get
                {
                    return this.razonSocialFiscalField;
                }
                set
                {
                    this.razonSocialFiscalField = value;
                }
            }

            /// <remarks/>
            public string IdentificadorFiscal
            {
                get
                {
                    return this.identificadorFiscalField;
                }
                set
                {
                    this.identificadorFiscalField = value;
                }
            }

            /// <remarks/>
            public string RequiereTurno
            {
                get
                {
                    return this.requiereTurnoField;
                }
                set
                {
                    this.requiereTurnoField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("CampoValor")]
            public object[] CampoDinamicoDomicilio
            {
                get
                {
                    return this.campoDinamicoDomicilioField;
                }
                set
                {
                    this.campoDinamicoDomicilioField = value;
                }
            }

            /// <remarks/>
            public string RefExternaDocumentoFiscal
            {
                get
                {
                    return this.refExternaDocumentoFiscalField;
                }
                set
                {
                    this.refExternaDocumentoFiscalField = value;
                }
            }

            /// <remarks/>
            public string GrupoTramo
            {
                get
                {
                    return this.grupoTramoField;
                }
                set
                {
                    this.grupoTramoField = value;
                }
            }

            /// <remarks/>
            public string Int1
            {
                get
                {
                    return this.int1Field;
                }
                set
                {
                    this.int1Field = value;
                }
            }

            /// <remarks/>
            public string Int2
            {
                get
                {
                    return this.int2Field;
                }
                set
                {
                    this.int2Field = value;
                }
            }

            /// <remarks/>
            public string Float1
            {
                get
                {
                    return this.float1Field;
                }
                set
                {
                    this.float1Field = value;
                }
            }

            /// <remarks/>
            public string Float2
            {
                get
                {
                    return this.float2Field;
                }
                set
                {
                    this.float2Field = value;
                }
            }

            /// <remarks/>
            public string CargaExclusiva
            {
                get
                {
                    return this.cargaExclusivaField;
                }
                set
                {
                    this.cargaExclusivaField = value;
                }
            }

            /// <remarks/>
            public string IgnorarOperacion
            {
                get
                {
                    return this.ignorarOperacionField;
                }
                set
                {
                    this.ignorarOperacionField = value;
                }
            }

            /// <remarks/>
            public string IgnorarOperacionDomicilioOrden
            {
                get
                {
                    return this.ignorarOperacionDomicilioOrdenField;
                }
                set
                {
                    this.ignorarOperacionDomicilioOrdenField = value;
                }
            }

            /// <remarks/>
            public string CrearDomicilioOrden
            {
                get
                {
                    return this.crearDomicilioOrdenField;
                }
                set
                {
                    this.crearDomicilioOrdenField = value;
                }
            }

            /// <remarks/>
            public string ActualizarDomicilioOrden
            {
                get
                {
                    return this.actualizarDomicilioOrdenField;
                }
                set
                {
                    this.actualizarDomicilioOrdenField = value;
                }
            }

            /// <remarks/>
            public string ValidarDomicilioOrden
            {
                get
                {
                    return this.validarDomicilioOrdenField;
                }
                set
                {
                    this.validarDomicilioOrdenField = value;
                }
            }

            /// <remarks/>
            public string Bonificacion
            {
                get
                {
                    return this.bonificacionField;
                }
                set
                {
                    this.bonificacionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://unisolutions.com.ar/")]
        public partial class ConsultarParadaPorIdResponseConsultarParadaPorIdResultClienteDomicilioFiscal
        {

            private string direccionField;

            private string calleField;

            private string numeroPuertaField;

            private string entreCalleField;

            private string barrioField;

            private string localidadField;

            private string partidoField;

            private string provinciaField;

            private string paisField;

            private string latitudField;

            private string longitudField;

            private string inicioHorario1Field;

            private string inicioHorario2Field;

            private string finHorario1Field;

            private string finHorario2Field;

            private string tiempoEsperaField;

            private string int1Field;

            private string int2Field;

            private string varchar1Field;

            private string varchar2Field;

            private string float1Field;

            private string float2Field;

            private string grupoRutasField;

            private string cargaExclusivaField;

            private string codigoPostalField;

            private string eMailField;

            private string identificadorFiscalField;

            private string refDomicilioField;

            /// <remarks/>
            public string Direccion
            {
                get
                {
                    return this.direccionField;
                }
                set
                {
                    this.direccionField = value;
                }
            }

            /// <remarks/>
            public string Calle
            {
                get
                {
                    return this.calleField;
                }
                set
                {
                    this.calleField = value;
                }
            }

            /// <remarks/>
            public string NumeroPuerta
            {
                get
                {
                    return this.numeroPuertaField;
                }
                set
                {
                    this.numeroPuertaField = value;
                }
            }

            /// <remarks/>
            public string EntreCalle
            {
                get
                {
                    return this.entreCalleField;
                }
                set
                {
                    this.entreCalleField = value;
                }
            }

            /// <remarks/>
            public string Barrio
            {
                get
                {
                    return this.barrioField;
                }
                set
                {
                    this.barrioField = value;
                }
            }

            /// <remarks/>
            public string Localidad
            {
                get
                {
                    return this.localidadField;
                }
                set
                {
                    this.localidadField = value;
                }
            }

            /// <remarks/>
            public string Partido
            {
                get
                {
                    return this.partidoField;
                }
                set
                {
                    this.partidoField = value;
                }
            }

            /// <remarks/>
            public string Provincia
            {
                get
                {
                    return this.provinciaField;
                }
                set
                {
                    this.provinciaField = value;
                }
            }

            /// <remarks/>
            public string Pais
            {
                get
                {
                    return this.paisField;
                }
                set
                {
                    this.paisField = value;
                }
            }

            /// <remarks/>
            public string Latitud
            {
                get
                {
                    return this.latitudField;
                }
                set
                {
                    this.latitudField = value;
                }
            }

            /// <remarks/>
            public string Longitud
            {
                get
                {
                    return this.longitudField;
                }
                set
                {
                    this.longitudField = value;
                }
            }

            /// <remarks/>
            public string InicioHorario1
            {
                get
                {
                    return this.inicioHorario1Field;
                }
                set
                {
                    this.inicioHorario1Field = value;
                }
            }

            /// <remarks/>
            public string InicioHorario2
            {
                get
                {
                    return this.inicioHorario2Field;
                }
                set
                {
                    this.inicioHorario2Field = value;
                }
            }

            /// <remarks/>
            public string FinHorario1
            {
                get
                {
                    return this.finHorario1Field;
                }
                set
                {
                    this.finHorario1Field = value;
                }
            }

            /// <remarks/>
            public string FinHorario2
            {
                get
                {
                    return this.finHorario2Field;
                }
                set
                {
                    this.finHorario2Field = value;
                }
            }

            /// <remarks/>
            public string TiempoEspera
            {
                get
                {
                    return this.tiempoEsperaField;
                }
                set
                {
                    this.tiempoEsperaField = value;
                }
            }

            /// <remarks/>
            public string Int1
            {
                get
                {
                    return this.int1Field;
                }
                set
                {
                    this.int1Field = value;
                }
            }

            /// <remarks/>
            public string Int2
            {
                get
                {
                    return this.int2Field;
                }
                set
                {
                    this.int2Field = value;
                }
            }

            /// <remarks/>
            public string Varchar1
            {
                get
                {
                    return this.varchar1Field;
                }
                set
                {
                    this.varchar1Field = value;
                }
            }

            /// <remarks/>
            public string Varchar2
            {
                get
                {
                    return this.varchar2Field;
                }
                set
                {
                    this.varchar2Field = value;
                }
            }

            /// <remarks/>
            public string Float1
            {
                get
                {
                    return this.float1Field;
                }
                set
                {
                    this.float1Field = value;
                }
            }

            /// <remarks/>
            public string Float2
            {
                get
                {
                    return this.float2Field;
                }
                set
                {
                    this.float2Field = value;
                }
            }

            /// <remarks/>
            public string GrupoRutas
            {
                get
                {
                    return this.grupoRutasField;
                }
                set
                {
                    this.grupoRutasField = value;
                }
            }

            /// <remarks/>
            public string CargaExclusiva
            {
                get
                {
                    return this.cargaExclusivaField;
                }
                set
                {
                    this.cargaExclusivaField = value;
                }
            }

            /// <remarks/>
            public string CodigoPostal
            {
                get
                {
                    return this.codigoPostalField;
                }
                set
                {
                    this.codigoPostalField = value;
                }
            }

            /// <remarks/>
            public string EMail
            {
                get
                {
                    return this.eMailField;
                }
                set
                {
                    this.eMailField = value;
                }
            }

            /// <remarks/>
            public string IdentificadorFiscal
            {
                get
                {
                    return this.identificadorFiscalField;
                }
                set
                {
                    this.identificadorFiscalField = value;
                }
            }

            /// <remarks/>
            public string RefDomicilio
            {
                get
                {
                    return this.refDomicilioField;
                }
                set
                {
                    this.refDomicilioField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://unisolutions.com.ar/")]
        public partial class ConsultarParadaPorIdResponseConsultarParadaPorIdResultClienteDador
        {

            private string referenciaExternaField;

            private string razonSocialField;

            private string nombreFantasiaField;

            private string cuitField;

            private string telefono1Field;

            private string telefono2Field;

            private string direccionField;

            private string localidadField;

            private string eMailGestorDeFlotaField;

            private string centroDeCostoField;

            private object[] operacionesField;

            private string idEstadoField;

            /// <remarks/>
            public string ReferenciaExterna
            {
                get
                {
                    return this.referenciaExternaField;
                }
                set
                {
                    this.referenciaExternaField = value;
                }
            }

            /// <remarks/>
            public string RazonSocial
            {
                get
                {
                    return this.razonSocialField;
                }
                set
                {
                    this.razonSocialField = value;
                }
            }

            /// <remarks/>
            public string NombreFantasia
            {
                get
                {
                    return this.nombreFantasiaField;
                }
                set
                {
                    this.nombreFantasiaField = value;
                }
            }

            /// <remarks/>
            public string Cuit
            {
                get
                {
                    return this.cuitField;
                }
                set
                {
                    this.cuitField = value;
                }
            }

            /// <remarks/>
            public string Telefono1
            {
                get
                {
                    return this.telefono1Field;
                }
                set
                {
                    this.telefono1Field = value;
                }
            }

            /// <remarks/>
            public string Telefono2
            {
                get
                {
                    return this.telefono2Field;
                }
                set
                {
                    this.telefono2Field = value;
                }
            }

            /// <remarks/>
            public string Direccion
            {
                get
                {
                    return this.direccionField;
                }
                set
                {
                    this.direccionField = value;
                }
            }

            /// <remarks/>
            public string Localidad
            {
                get
                {
                    return this.localidadField;
                }
                set
                {
                    this.localidadField = value;
                }
            }

            /// <remarks/>
            public string eMailGestorDeFlota
            {
                get
                {
                    return this.eMailGestorDeFlotaField;
                }
                set
                {
                    this.eMailGestorDeFlotaField = value;
                }
            }

            /// <remarks/>
            public string CentroDeCosto
            {
                get
                {
                    return this.centroDeCostoField;
                }
                set
                {
                    this.centroDeCostoField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("pOperacion")]
            public object[] operaciones
            {
                get
                {
                    return this.operacionesField;
                }
                set
                {
                    this.operacionesField = value;
                }
            }

            /// <remarks/>
            public string IdEstado
            {
                get
                {
                    return this.idEstadoField;
                }
                set
                {
                    this.idEstadoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://unisolutions.com.ar/")]
        public partial class ConsultarParadaPorIdResponseConsultarParadaPorIdResultOrdenParada
        {

            private string idOrdenField;

            private string idEstadoOrdenField;

            private string idJornadaField;

            private string idRutaField;

            /// <remarks/>
            public string IdOrden
            {
                get
                {
                    return this.idOrdenField;
                }
                set
                {
                    this.idOrdenField = value;
                }
            }

            /// <remarks/>
            public string IdEstadoOrden
            {
                get
                {
                    return this.idEstadoOrdenField;
                }
                set
                {
                    this.idEstadoOrdenField = value;
                }
            }

            /// <remarks/>
            public string IdJornada
            {
                get
                {
                    return this.idJornadaField;
                }
                set
                {
                    this.idJornadaField = value;
                }
            }

            /// <remarks/>
            public string IdRuta
            {
                get
                {
                    return this.idRutaField;
                }
                set
                {
                    this.idRutaField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://unisolutions.com.ar/")]
        public partial class ConsultarParadaPorIdResponseConsultarParadaPorIdResultPFoto
        {

            private string idArchivoField;

            private string descripcionField;

            private string loginField;

            private string fechaCreacionField;

            private string tipoField;

            private string contenidoField;

            private string clasificacionField;

            private string firmaField;

            private string rutaField;

            /// <remarks/>
            public string IdArchivo
            {
                get
                {
                    return this.idArchivoField;
                }
                set
                {
                    this.idArchivoField = value;
                }
            }

            /// <remarks/>
            public string Descripcion
            {
                get
                {
                    return this.descripcionField;
                }
                set
                {
                    this.descripcionField = value;
                }
            }

            /// <remarks/>
            public string Login
            {
                get
                {
                    return this.loginField;
                }
                set
                {
                    this.loginField = value;
                }
            }

            /// <remarks/>
            public string FechaCreacion
            {
                get
                {
                    return this.fechaCreacionField;
                }
                set
                {
                    this.fechaCreacionField = value;
                }
            }

            /// <remarks/>
            public string Tipo
            {
                get
                {
                    return this.tipoField;
                }
                set
                {
                    this.tipoField = value;
                }
            }

            /// <remarks/>
            public string Contenido
            {
                get
                {
                    return this.contenidoField;
                }
                set
                {
                    this.contenidoField = value;
                }
            }

            /// <remarks/>
            public string Clasificacion
            {
                get
                {
                    return this.clasificacionField;
                }
                set
                {
                    this.clasificacionField = value;
                }
            }

            /// <remarks/>
            public string Firma
            {
                get
                {
                    return this.firmaField;
                }
                set
                {
                    this.firmaField = value;
                }
            }

            /// <remarks/>
            public string Ruta
            {
                get
                {
                    return this.rutaField;
                }
                set
                {
                    this.rutaField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://unisolutions.com.ar/")]
        public partial class ConsultarParadaPorIdResponseConsultarParadaPorIdResultPBitacora
        {

            private string bitacoraField;

            private string fechaField;

            private string loginField;

            /// <remarks/>
            public string Bitacora
            {
                get
                {
                    return this.bitacoraField;
                }
                set
                {
                    this.bitacoraField = value;
                }
            }

            /// <remarks/>
            public string Fecha
            {
                get
                {
                    return this.fechaField;
                }
                set
                {
                    this.fechaField = value;
                }
            }

            /// <remarks/>
            public string Login
            {
                get
                {
                    return this.loginField;
                }
                set
                {
                    this.loginField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://unisolutions.com.ar/")]
        public partial class ConsultarParadaPorIdResponseConsultarParadaPorIdResultConsultarParadaResultadoItem
        {

            private string refDocumentoField;

            private string refDocumentoAdicionalField;

            private string descripcionField;

            private string cantidadField;

            private string volumenField;

            private string pesoField;

            private string bultoField;

            private string palletsField;

            private string cantidadEntregadaField;

            private string motivoField;

            private string motivoReferenciaField;

            private string estadoParadaItemField;

            private string varchar1Field;

            private string varchar2Field;

            private string varchar3Field;

            private string cobradoField;

            private string numeroTicketField;

            private object productosField;

            private object tiposImpuestoField;

            private string valorACobrarField;

            private string valorUnitarioField;

            private string valorCobradoField;

            /// <remarks/>
            public string RefDocumento
            {
                get
                {
                    return this.refDocumentoField;
                }
                set
                {
                    this.refDocumentoField = value;
                }
            }

            /// <remarks/>
            public string RefDocumentoAdicional
            {
                get
                {
                    return this.refDocumentoAdicionalField;
                }
                set
                {
                    this.refDocumentoAdicionalField = value;
                }
            }

            /// <remarks/>
            public string Descripcion
            {
                get
                {
                    return this.descripcionField;
                }
                set
                {
                    this.descripcionField = value;
                }
            }

            /// <remarks/>
            public string Cantidad
            {
                get
                {
                    return this.cantidadField;
                }
                set
                {
                    this.cantidadField = value;
                }
            }

            /// <remarks/>
            public string Volumen
            {
                get
                {
                    return this.volumenField;
                }
                set
                {
                    this.volumenField = value;
                }
            }

            /// <remarks/>
            public string Peso
            {
                get
                {
                    return this.pesoField;
                }
                set
                {
                    this.pesoField = value;
                }
            }

            /// <remarks/>
            public string Bulto
            {
                get
                {
                    return this.bultoField;
                }
                set
                {
                    this.bultoField = value;
                }
            }

            /// <remarks/>
            public string Pallets
            {
                get
                {
                    return this.palletsField;
                }
                set
                {
                    this.palletsField = value;
                }
            }

            /// <remarks/>
            public string CantidadEntregada
            {
                get
                {
                    return this.cantidadEntregadaField;
                }
                set
                {
                    this.cantidadEntregadaField = value;
                }
            }

            /// <remarks/>
            public string Motivo
            {
                get
                {
                    return this.motivoField;
                }
                set
                {
                    this.motivoField = value;
                }
            }

            /// <remarks/>
            public string MotivoReferencia
            {
                get
                {
                    return this.motivoReferenciaField;
                }
                set
                {
                    this.motivoReferenciaField = value;
                }
            }

            /// <remarks/>
            public string EstadoParadaItem
            {
                get
                {
                    return this.estadoParadaItemField;
                }
                set
                {
                    this.estadoParadaItemField = value;
                }
            }

            /// <remarks/>
            public string Varchar1
            {
                get
                {
                    return this.varchar1Field;
                }
                set
                {
                    this.varchar1Field = value;
                }
            }

            /// <remarks/>
            public string Varchar2
            {
                get
                {
                    return this.varchar2Field;
                }
                set
                {
                    this.varchar2Field = value;
                }
            }

            /// <remarks/>
            public string Varchar3
            {
                get
                {
                    return this.varchar3Field;
                }
                set
                {
                    this.varchar3Field = value;
                }
            }

            /// <remarks/>
            public string Cobrado
            {
                get
                {
                    return this.cobradoField;
                }
                set
                {
                    this.cobradoField = value;
                }
            }

            /// <remarks/>
            public string NumeroTicket
            {
                get
                {
                    return this.numeroTicketField;
                }
                set
                {
                    this.numeroTicketField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object Productos
            {
                get
                {
                    return this.productosField;
                }
                set
                {
                    this.productosField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object TiposImpuesto
            {
                get
                {
                    return this.tiposImpuestoField;
                }
                set
                {
                    this.tiposImpuestoField = value;
                }
            }

            /// <remarks/>
            public string ValorACobrar
            {
                get
                {
                    return this.valorACobrarField;
                }
                set
                {
                    this.valorACobrarField = value;
                }
            }

            /// <remarks/>
            public string ValorUnitario
            {
                get
                {
                    return this.valorUnitarioField;
                }
                set
                {
                    this.valorUnitarioField = value;
                }
            }

            /// <remarks/>
            public string ValorCobrado
            {
                get
                {
                    return this.valorCobradoField;
                }
                set
                {
                    this.valorCobradoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://unisolutions.com.ar/")]
        public partial class ConsultarParadaPorIdResponseConsultarParadaPorIdResultPRecursos
        {

            private string referenciaExternaField;

            private string descripcionField;

            private string descripcionTipoRecursoField;

            private string referenciaExternaEstadoRecursoField;

            private string cantidadAltaRecursoField;

            private string cantidadField;

            /// <remarks/>
            public string ReferenciaExterna
            {
                get
                {
                    return this.referenciaExternaField;
                }
                set
                {
                    this.referenciaExternaField = value;
                }
            }

            /// <remarks/>
            public string Descripcion
            {
                get
                {
                    return this.descripcionField;
                }
                set
                {
                    this.descripcionField = value;
                }
            }

            /// <remarks/>
            public string DescripcionTipoRecurso
            {
                get
                {
                    return this.descripcionTipoRecursoField;
                }
                set
                {
                    this.descripcionTipoRecursoField = value;
                }
            }

            /// <remarks/>
            public string ReferenciaExternaEstadoRecurso
            {
                get
                {
                    return this.referenciaExternaEstadoRecursoField;
                }
                set
                {
                    this.referenciaExternaEstadoRecursoField = value;
                }
            }

            /// <remarks/>
            public string CantidadAltaRecurso
            {
                get
                {
                    return this.cantidadAltaRecursoField;
                }
                set
                {
                    this.cantidadAltaRecursoField = value;
                }
            }

            /// <remarks/>
            public string Cantidad
            {
                get
                {
                    return this.cantidadField;
                }
                set
                {
                    this.cantidadField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://unisolutions.com.ar/")]
        public partial class ConsultarParadaPorIdResponseConsultarParadaPorIdResultCampoValor
        {

            private string campoField;

            private string valorField;

            /// <remarks/>
            public string Campo
            {
                get
                {
                    return this.campoField;
                }
                set
                {
                    this.campoField = value;
                }
            }

            /// <remarks/>
            public string Valor
            {
                get
                {
                    return this.valorField;
                }
                set
                {
                    this.valorField = value;
                }
            }
        }


    }
}
