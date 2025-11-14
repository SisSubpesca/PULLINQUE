using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class DocSolicitudCierre
    {
        public int idSolConcesion { get; set; }
        public Requerimiento docITCierre { get; set; }
        public Requerimiento docSSPCierre { get; set; }
        public DocumentoAmbito docPestanaIT { get; set; }
        public DocumentoAmbito docPestanaSSP { get; set; }
        public Boolean flujoCierreForzado { get; set; }

        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idSolConcesion { get; set; }
            public Requerimiento docITCierre { get; set; }
            public Requerimiento docSSPCierre { get; set; }
            public DocumentoAmbito docPestanaIT { get; set; }
            public DocumentoAmbito docPestanaSSP { get; set; }
            public Boolean flujoCierreForzado { get; set; }
        }
    }
}
