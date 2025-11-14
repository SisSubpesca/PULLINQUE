using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class EjemplarPT
    {

        public int idEjemplarPT { get; set; }
        public int idProyectoTecnico { get; set; }
        public ParametroGenerico especieEjemplar { get; set; }
        public ParametroGenerico grupoEjemplar { get; set; }
        public ParametroGenerico unidEstructuraEjemplar { get; set; }
        public int ejemplaresExistentes { get; set; }
        public List<ValorParametroAnioPT> aniosEjemplar { get; set; }

        public int index { get; set; }
        public int accion { get; set; }

        public EjemplarPT() { 
        }

        [Serializable]
        public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idEjemplarPT { get; set; }
            public int idProyectoTecnico { get; set; }
            public ParametroGenerico especieEjemplar { get; set; }
            public ParametroGenerico grupoEjemplar { get; set; }
            public ParametroGenerico unidEstructuraEjemplar { get; set; }
            public int ejemplaresExistentes { get; set; }
            public List<ValorParametroAnioPT> aniosEjemplar { get; set; }

            public int index { get; set; }
            public int accion { get; set; }
        
        }

    }
}
