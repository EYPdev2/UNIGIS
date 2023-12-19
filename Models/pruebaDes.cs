using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualizadorDoctosUnigis.Models
{
    public class pruebaDes
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

            private byte resultadoField;

            private bool viajeFinalizadoField;

            private byte idViajeField;

            private byte secuenciaField;

            private string estadoParadaField;

            private System.DateTime horarioParadaRealInicioField;

            private System.DateTime horarioParadaRealFinField;

            private string refDocumentoField;

            private string dominioField;

            private string uRLField;

            private object observacionesField;

            private string descripcionField;

            private string direccionField;

            private string paisField;

            private object motivoField;

            private System.DateTime inicioHorarioEstimadoField;

            private System.DateTime finHorarioEstimadoField;

            private System.DateTime inicioHorarioPlanificadoField;

            private System.DateTime finHorarioPlanificadoField;

            private System.DateTime eTAField;

            private ConsultarParadaPorIdResponseConsultarParadaPorIdResultCliente clienteField;

            private object fotosField;

            private ConsultarParadaPorIdResponseConsultarParadaPorIdResultItems itemsField;

            private decimal latitudField;

            private byte bultosField;

            private decimal pesoField;

            private byte valorDeclaradoField;

            private decimal longitudField;

            /// <remarks/>
            public byte Resultado
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
            public bool ViajeFinalizado
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
            public byte IdViaje
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
            public byte Secuencia
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
            public System.DateTime HorarioParadaRealInicio
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
            public System.DateTime HorarioParadaRealFin
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
            public object Observaciones
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
            public object Motivo
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
            public System.DateTime InicioHorarioEstimado
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
            public System.DateTime FinHorarioEstimado
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
            public System.DateTime InicioHorarioPlanificado
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
            public System.DateTime FinHorarioPlanificado
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
            public System.DateTime ETA
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
            public object Fotos
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
            public ConsultarParadaPorIdResponseConsultarParadaPorIdResultItems Items
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
            public decimal Latitud
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
            public byte Bultos
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
            public decimal Peso
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
            public byte ValorDeclarado
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
            public decimal Longitud
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://unisolutions.com.ar/")]
        public partial class ConsultarParadaPorIdResponseConsultarParadaPorIdResultCliente
        {

            private string refClienteField;

            private string razonSocialField;

            private object latitudField;

            private object longitudField;

            private object inicioHorario1Field;

            private object inicioHorario2Field;

            private object finHorario1Field;

            private object finHorario2Field;

            private object tiempoEsperaField;

            private string numeroDocumentoField;

            private string contactoField;

            private byte int1Field;

            private byte int2Field;

            private byte float1Field;

            private byte float2Field;

            private byte bonificacionField;

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
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object Latitud
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
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object Longitud
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
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object InicioHorario1
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
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object InicioHorario2
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
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object FinHorario1
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
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object FinHorario2
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
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object TiempoEspera
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
            public byte Int1
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
            public byte Int2
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
            public byte Float1
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
            public byte Float2
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
            public byte Bonificacion
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
        public partial class ConsultarParadaPorIdResponseConsultarParadaPorIdResultItems
        {

            private ConsultarParadaPorIdResponseConsultarParadaPorIdResultItemsConsultarParadaResultadoItem consultarParadaResultadoItemField;

            /// <remarks/>
            public ConsultarParadaPorIdResponseConsultarParadaPorIdResultItemsConsultarParadaResultadoItem ConsultarParadaResultadoItem
            {
                get
                {
                    return this.consultarParadaResultadoItemField;
                }
                set
                {
                    this.consultarParadaResultadoItemField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://unisolutions.com.ar/")]
        public partial class ConsultarParadaPorIdResponseConsultarParadaPorIdResultItemsConsultarParadaResultadoItem
        {

            private ushort refDocumentoField;

            private byte cantidadField;

            private byte volumenField;

            private byte pesoField;

            private byte bultoField;

            private byte palletsField;

            private byte cantidadEntregadaField;

            private string estadoParadaItemField;

            private object productosField;

            private object tiposImpuestoField;

            /// <remarks/>
            public ushort RefDocumento
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
            public byte Cantidad
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
            public byte Volumen
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
            public byte Peso
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
            public byte Bulto
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
            public byte Pallets
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
            public byte CantidadEntregada
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
        }


    }
}
