using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class GrupoEspecie
    {
        public int id_grupoEspecie { get; set; }
        public string grupoEspecie { get; set; }
        public bool cultivo { get; set; }
        public int cultivoFiltro { get; set; }


        // MÉTODOS (Constructores)
        public GrupoEspecie()
        {
        }

         // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int id_grupoEspecie { get; set; }
            public string grupoEspecie { get; set; }
            public bool cultivo { get; set; }
            public int cultivoFiltro { get; set; }
        }

        public string cultivoString {
            get {

                if (cultivo)
                {
                    return "Si";
                }
                else {
                    return "No";
                }
            }
        }
    }
}
