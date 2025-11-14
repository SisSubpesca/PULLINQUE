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
    public class CentrosDeCultivo
    {

        public string codigoCentro { get; set; }
        public String nombreCentro { get; set; }


         // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public string codigoCentro { get; set; }
            public String nombreCentro { get; set; }
        }

    }
}
