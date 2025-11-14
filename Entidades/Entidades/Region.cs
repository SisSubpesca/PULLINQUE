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
    public class Region
    {
        public int id_region { get; set; }
        public string region { get; set; }
        public int codigo { get; set; }
        public Comuna comuna { get; set; }

        // MÉTODOS (Constructores)
        public Region()
        {
        }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int id_region { get; set; }
            public string region { get; set; }
            public int codigo { get; set; }
            public Comuna comuna { get; set; }
        }
      
    }
}
