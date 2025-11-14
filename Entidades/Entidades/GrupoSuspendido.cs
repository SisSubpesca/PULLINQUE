using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class GrupoSuspendido
    {
        
        public int idGrupoSuspend { get; set; }
        public ParametroGenerico tipoAgrupacion { get; set; }
        public ParametroGenerico estadoVigencia	{ get; set; }
        public SolicitudConcesion solicitudConcesion { get; set; }
        public string nombreGrupoSuspend { get; set; }
        public List<ArchivoBinario> archivoGrupoSusp { get; set; }
        public List<AsocGrupoSolicitud> asocGrupoSolicitud { get; set; }

        private int _index;
        private int _accion;

        public int index
        {
            get { return _index; }
            set { _index = value; }
        }

        public int accion
        {
            get { return _accion; }
            set { _accion = value; }
        }

        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idGrupoSuspend { get; set; }
            public ParametroGenerico tipoAgrupacion { get; set; }
            public ParametroGenerico estadoVigencia { get; set; }
            public SolicitudConcesion solicitudConcesion { get; set; }
            public string nombreGrupoSuspend { get; set; }
            public List<ArchivoBinario> archivoGrupoSusp { get; set; }
            public List<AsocGrupoSolicitud> asocGrupoSolicitud { get; set; }

        }
    }
}
