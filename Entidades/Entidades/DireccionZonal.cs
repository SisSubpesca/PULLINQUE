using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    public class DireccionZonal
    {
        public String codDirZonal { get; set; }
        public String nombreDirZonal { get; set; }
        public String descripcion { get; set; }
        public List<Region> regiones { get; set; }
        public ParametroGenerico region { get; set; }
        public bool esCentral { get; set; }

        public DireccionZonal()
        {
        }

        public string esCentralString
        {

            get
            {
                if (esCentral)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public int idRegion
        {
            get {

                if (region != null && region.id > 0) {

                    return region.id;
                }

                return 0;
            }
        }

        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public String codDirZonal { get; set; }
            public String nombreDirZonal { get; set; }
            public String descripcion { get; set; }
            public List<Region> regiones { get; set; }
            public ParametroGenerico region { get; set; }
            public bool esCentral { get; set; }
        
        }

    }
}
