using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class EvaluacionUOT_UE
    {
        public int idEvaluacionUOT { get; set; }
        public SolicitudConcesion solicitudConcesion { get; set; }
        public ParametroGenerico requiereIT_UOT { get; set; }
        public ParametroGenerico estadoResultado { get; set; }
        public string observaciones { get; set; }
        public List<DependenciaSupeditados> dependenciaSupeditadosList { get; set; }
        public List<AsocGrupoSolicitud> asocGrupoSolicitudList { get; set; }
        

        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idEvaluacionUOT { get; set; }
            public SolicitudConcesion solicitudConcesion { get; set; }
            public ParametroGenerico requiereIT_UOT { get; set; }
            public ParametroGenerico estadoResultado { get; set; }
            public string observaciones { get; set; }
            public List<DependenciaSupeditados> dependenciaSupeditadosList { get; set; }
            public List<AsocGrupoSolicitud> asocGrupoSolicitudList { get; set; }
        }
        
    }
}
