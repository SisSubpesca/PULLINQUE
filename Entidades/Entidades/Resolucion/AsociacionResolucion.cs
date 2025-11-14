using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades.Resolucion
{

    [Serializable()]
    public class AsociacionResolucion
    {

        public int index { get; set; }
        public int accion { get; set; }
        public Resolucion resolucion { get; set; }
        public ParametroGenerico seccion { get; set; }

            // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int index { get; set; }
            public int accion { get; set; }
            public Resolucion resolucion { get; set; }
            public ParametroGenerico seccion { get; set; }

        }
    }
}
