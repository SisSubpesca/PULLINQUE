using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class DocumentosConcesion
    {
        public int idDocConcesion { get; set; }
        public int idUsuario { get; set; }
        public SolicitudConcesion solicitud { get; set; }
        public ParametroGenerico flujoDocumental { get; set; }
        public ParametroGenerico tipoSalida { get; set; }
        public ParametroGenerico tipoEntrada { get; set; }
        public ParametroGenerico tipoDocumento { get; set; }
        public ParametroGenerico destinatario { get; set; }
        public ParametroGenerico origen { get; set; }
        public ParametroGenerico estadoVigencia { get; set; }
        public ArchivoBinario archivoAdjunto { get; set; }
        public string nombreTema { get; set; }
        public string numero { get; set; }
        public string observaciones { get; set; }
        public DateTime fecha { get; set; }
        public DateTime nuevaFecha { get; set; }
        public int numeroCI { get; set; }
        public DateTime fechaCI { get; set; }
        public DateTime fechaIngresoSistema { get; set; }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idDocConcesion { get; set; }
            public int idUsuario { get; set; }
            public SolicitudConcesion solicitud { get; set; }
            public ParametroGenerico flujoDocumental { get; set; }
            public ParametroGenerico tipoSalida { get; set; }
            public ParametroGenerico tipoEntrada { get; set; }
            public ParametroGenerico tipoDocumento { get; set; }
            public ParametroGenerico destinatario { get; set; }
            public ParametroGenerico origen { get; set; }
            public ParametroGenerico estadoVigencia { get; set; }
            public ArchivoBinario archivoAdjunto { get; set; }
            public string nombreTema { get; set; }
            public string numero { get; set; }
            public string observaciones { get; set; }
            public DateTime fecha { get; set; }
            public DateTime nuevaFecha { get; set; }
            public int numeroCI { get; set; }
            public DateTime fechaCI { get; set; }
            public DateTime fechaIngresoSistema { get; set; }

        }
    }
}

