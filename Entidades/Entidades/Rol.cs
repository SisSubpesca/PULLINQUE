using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Datos.Entidades
{
    [Serializable()]
    public class Rol
    {
        public int idRol { get; set; }
        public ParametroGenerico estadoVigencia { get; set; }
        public string nombreRol { get; set; }
        public string descripcionRol { get; set; }
        public Hashtable seccionesRol { get; set; }
        public ParametroGenerico tipoRol { get; set; }
        public bool aplicaDespliegueFiltro { get; set; }
        public String aplicaDespliegueFiltroDetalle { get { return filtroToString(); } }


        public string filtroToString()
        {

            if (aplicaDespliegueFiltro) { 
                return "Sí";
            }else{
                return "No";
            }

        }


         // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idRol { get; set; }
            public ParametroGenerico estadoVigencia { get; set; }
            public string nombreRol { get; set; }
            public string descripcionRol { get; set; }
            public Hashtable seccionesRol { get; set; }
            public ParametroGenerico tipoRol { get; set; }
            public bool aplicaDespliegueFiltro { get; set; }
            
        }

    }
}
