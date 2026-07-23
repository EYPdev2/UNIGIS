using System;

namespace ActualizadorDoctosUnigis.Exceptions
{
    public class SolicitudYaRevisadaException : Exception
    {
        public string NombreRevisor { get; }

        public SolicitudYaRevisadaException() { }

        public SolicitudYaRevisadaException(string nombreRevisor)
        {
            NombreRevisor = nombreRevisor;
        }
    }
}
