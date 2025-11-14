using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class Materia
    {
        public int idMateria { get; set; }
        public string nombreMateria { get; set; }

        public Materia(){
    
        }

        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idMateria { get; set; }
            public string nombreMateria { get; set; }
        }

    }
}
