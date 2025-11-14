using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class Accion
    {
        public int idAccion { get; set; }
        public ParametroGenerico estadoVigencia { get; set; }
        public string nombreAccion { get; set; }

          // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idAccion { get; set; }
            public ParametroGenerico estadoVigencia { get; set; }
            public string nombreAccion { get; set; }
        
        }

    }
}
