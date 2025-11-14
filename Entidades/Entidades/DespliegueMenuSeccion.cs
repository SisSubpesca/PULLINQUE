using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    public class DespliegueMenuSeccion
    {

       
        public int idMenu;
        public int idSeccion;
        public bool aplicaModAmpliacion;
        public bool aplicaModReduccion;
        public bool aplicaModEspecie;
        public bool aplicaModRegularizacion;
        public bool aplicaModPT;

        // MÉTODOS (Constructores)
        public DespliegueMenuSeccion()
        {

        }

         //SUBCLASE
        [Serializable]
        public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idMenu;
            public int idSeccion;
            public bool aplicaModAmpliacion;
            public bool aplicaModReduccion;
            public bool aplicaModEspecie;
            public bool aplicaModRegularizacion;
            public bool aplicaModPT;
        }
         
    }
}
