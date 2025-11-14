using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades.Relocalizacion
{

    [Serializable()]
    public class OrigenSector
    {

        public int index { get; set; }
        public int accion { get; set; }
        public int idOrigenSector { get; set; }
        public int idDetalleSector { get; set; }
        public SolicitudConcesion concesionOrigen { get; set; }
        public float superficieRelocalizada { get; set; }
        public List<ParametroGenerico> preferencias { get; set; }


        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int index { get; set; }
            public int accion { get; set; }
            public int idOrigenSector { get; set; }
            public int idDetalleSector { get; set; }
            public SolicitudConcesion concesionOrigen { get; set; }
            public float superficieRelocalizada { get; set; }
            public List<ParametroGenerico> preferencias { get; set; }
        }
    }
}
