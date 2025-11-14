using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Datos.AccesoDatos;

namespace Datos.Entidades
{
    [Serializable()]
    public class Comuna
    {
        public int id_comuna { get; set; }
        public int codigo { get; set; }
        public string comuna { get; set; }
        public int id_provincia { get; set; }
        public string nombreProv { get; set; }
        public bool esFronteriza { get; set; }
        public int esFronterizaFiltro { get; set; }
        
        // MÉTODOS (Constructores)
        public Comuna()
        {
        }

        public string esFronterizaString{
    
            get{
                if (esFronteriza)
                {
                    return "Si";
                }else{
                    return "No";
                }
            }
        }
        
         // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int id_comuna { get; set; }
            public int codigo { get; set; }
            public string comuna { get; set; }
            public int id_provincia { get; set; }
            public bool esFronteriza { get; set; }
            public string nombreProv { get; set; }
            public int esFronterizaFiltro { get; set; }
        }
     
    }
}
