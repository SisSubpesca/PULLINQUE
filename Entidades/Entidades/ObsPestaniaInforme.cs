using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    public class ObsPestaniaInforme
    {

        public int idObsPestInf { get; set; }
        public int idSolConcesion { get; set; }
        public ParametroGenerico tipoGrupo { get; set; }
        public String observaciones { get; set; }

        public ObsPestaniaInforme() { }

        [Serializable]
        public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idObsPestInf { get; set; }
            public int idSolConcesion { get; set; }
            public ParametroGenerico tipoGrupo { get; set; }
            public String observaciones { get; set; }
        }


    }
}
