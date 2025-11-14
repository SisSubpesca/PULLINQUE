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
    public class Macrozona
    {
        public int    id_macrozona { get; set; }
        public string macrozona    { get; set; }
        public int id_region { get; set; }
        public ParametroGenerico regionMacrozona { get; set; }

        // MÉTODOS (Constructores)
        public Macrozona()
        { }


        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int id_macrozona { get; set; }
            public string macrozona { get; set; }
            public int id_region { get; set; }
            public ParametroGenerico regionMacrozona { get; set; }
        }

        public string regionMacrozonaString
        {
            get
            {

                if (regionMacrozona != null)
                {
                    return regionMacrozona.descripcion;
                }
                else
                {
                    return "";
                }
            }
        }
      
    }
}
