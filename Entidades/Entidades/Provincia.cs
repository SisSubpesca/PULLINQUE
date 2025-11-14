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
    public class Provincia
    {
        public int id_provincia { get; set; }
        public int codigo_provincia { get; set; }
        public string provincia { get; set; }
        public int id_region { get; set; }
        public string nombreReg { get; set; }

        // MÉTODOS (Constructores)
        public Provincia()
        {
        }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int id_provincia { get; set; }
            public string provincia { get; set; }
            public int id_region { get; set; }
            public string nombreReg { get; set; }
        }


    

    }
}
