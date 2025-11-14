using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class SometimientoSEA
    {
        public int idSometimientoSEA { get; set; }
        public int idSolConcesion { get; set; }
        public ConsultorAmbiental consultorAmbiental { get; set; }
        public EntidadMuestreador entidadAnalisis { get; set; }
        public EntidadMuestreador muestreadorAmb { get; set; }
        public DateTime fechaSometimiento { get; set; }
        public String nombreProySEA { get; set; }
        public String idSEA	{ get; set; }
        public String numCarpetaSEA	{ get; set; }
        public String linkSEA { get; set; }
        public String categoriaSEA { get; set; }

        // MÉTODOS (Constructores)
        public SometimientoSEA()
        { }

        [Serializable]
        public class Serializable {
            public int idSometimientoSEA { get; set; }
            public int idSolConcesion { get; set; }
            public ConsultorAmbiental consultorAmbiental { get; set; }
            public EntidadMuestreador entidadAnalisis { get; set; }
            public EntidadMuestreador muestreadorAmb { get; set; }
            public DateTime fechaSometimiento { get; set; }
            public String nombreProySEA { get; set; }
            public String idSEA { get; set; }
            public String numCarpetaSEA { get; set; }
            public String linkSEA { get; set; }
            public String categoriaSEA { get; set; }        
        }

    }
}
