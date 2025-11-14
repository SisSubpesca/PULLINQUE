using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{

    [Serializable()]
    public class ValidacionSEA
    {
        public Boolean esConsistente { get; set; }

        //0 = no se sabe 
        //1 = SEA SI
        //2 = SEA NO
        public int valorSEA { get; set; }

        // SUBCLASE 
        [Serializable]
        public class Serializable 
        {
            public Boolean esConsistente { get; set; }
            public int valorSEA { get; set; }
            
        }
    }
}
