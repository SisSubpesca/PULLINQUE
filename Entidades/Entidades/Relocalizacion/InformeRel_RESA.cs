using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades.Relocalizacion
{
    [Serializable()]
    public class InformeRel_RESA
    {

        public int idInformeRel { get; set; }
        public ParametroGenerico tipoDocumento { get; set; }
        public ParametroGenerico tipoDestinatario { get; set; }
        public ParametroGenerico materia { get; set; }
        public ArchivoBinario archivoBinSC { get; set; }
        public ParametroGenerico estadoVigencia { get; set; }
        public string numero { get; set; }
        public DateTime fecha { get; set; }
        public string observaciones { get; set; }
        public DateTime fechaIngresoSistema { get; set; }
        //public List<SolicitudConcesion> solicitudes { get; set; }
        public Usuario usuario { get; set; }
        public int idSolicitudBitacora { get; set; }
        public List<AsocInformeSolicitud> asocInformeSolicitud { get; set; }
        
        public int pagina { get; set; }
        public int codigoCentroFiltro { get; set; }
        public string codCentros { get; set; }

        public InformeRel_RESA()
        { }

         // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idInformeRel { get; set; }
            public ParametroGenerico tipoDocumento { get; set; }
            public ParametroGenerico tipoDestinatario { get; set; }
            public ParametroGenerico materia { get; set; }
            public ArchivoBinario archivoBinSC { get; set; }
            public ParametroGenerico estadoVigencia { get; set; }
            public string numero { get; set; }
            public DateTime fecha { get; set; }
            public string observaciones { get; set; }
            public DateTime fechaIngresoSistema { get; set; }
            //public List<SolicitudConcesion> solicitudes { get; set; }
            public Usuario usuario { get; set; }
            public int idSolicitudBitacora { get; set; }
            public List<AsocInformeSolicitud> asocInformeSolicitud { get; set; }
            public int pagina { get; set; }
            public int codigoCentroFiltro { get; set; }
            public string codCentros { get; set; }
        
        }

    }
}
