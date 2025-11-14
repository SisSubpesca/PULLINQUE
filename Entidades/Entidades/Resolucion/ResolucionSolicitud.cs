using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades.Resolucion.ResolucionSolicitud
{
    [Serializable()]
    public class ResolucionSolicitud
    {
        public int idHistUE { get; set; }
        public Resolucion resolucion { get; set; }
        public DocumentosConcesion docConcesion { get; set; }
        public SolicitudConcesion solicitud { get; set; }
        public ParametroGenerico estadoVigencia { get; set; }
        public DateTime fechaIngresoSistema { get; set; }
        public string observaciones { get; set; }
        public ParametroGenerico tipoIngreso { get; set; }
        public UnidadEspacial unidEspacial { get; set; }
            
        public string cadena { get; set; }

        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idHistUE { get; set; }
            public Resolucion resolucion { get; set; }
            public DocumentosConcesion docConcesion { get; set; }
            public SolicitudConcesion solicitud { get; set; }
            public ParametroGenerico estadoVigencia { get; set; }
            public DateTime fechaIngresoSistema { get; set; }
            public string cadena { get; set; }
            public string observaciones { get; set; }
            public ParametroGenerico tipoIngreso { get; set; }
            public UnidadEspacial unidEspacial { get; set; }
        }
        
    }
}
