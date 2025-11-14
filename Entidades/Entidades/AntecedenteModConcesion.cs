using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class AntecedenteModConcesion
    {
        public ParametroGenerico tipoModificacion { get; set; }
        public ParametroGenerico menu { get; set; }
        public Boolean despliegue { get; set; }

        public AntecedenteModConcesion() { }

        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public ParametroGenerico tipoModificacion { get; set; }
            public ParametroGenerico menu { get; set; }
            public Boolean despliegue { get; set; }
        }


    }
}
