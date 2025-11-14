using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class AsocGrupoSolicitud
    {
        public int idAsocGrupoSolicitud { get; set; }
        public EvaluacionUOT_UE evaluacionUOT_UE { get; set; }
        public DocumentoAmbito docPestana { get; set; } 
        public SolicitudConcesion solicitudConcesion { get; set; }
        public ParametroGenerico estadoVigencia { get; set; }
        public GrupoSuspendidos grupoSuspendido { get; set; }

        public int idResultadoIT_UOT { get; set; }

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
            public int idAsocGrupoSolicitud { get; set; }
            public EvaluacionUOT_UE evaluacionUOT_UE { get; set; }
            public DocumentoAmbito docPestana { get; set; }
            public SolicitudConcesion solicitudConcesion { get; set; }
            public ParametroGenerico estadoVigencia { get; set; }
            public GrupoSuspendidos grupoSuspendido { get; set; }

            public int idResultadoIT_UOT { get; set; }
            
        }
   
    }
}
