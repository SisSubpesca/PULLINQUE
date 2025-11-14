using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Datos.Entidades
{

    [Serializable()]
    public class Combobox
    {
        public String nombreAtributo { get; set; }
        public Hashtable hash { get; set; }

        
        public Combobox(String nombreAtributo)
        {
            this.nombreAtributo = nombreAtributo;
            this.hash = new Hashtable();
        }
        

          //SUBCLASE
        [Serializable]
        public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public String nombreAtributo { get; set; }
            public Hashtable hash { get; set; }

        }
    }
}
