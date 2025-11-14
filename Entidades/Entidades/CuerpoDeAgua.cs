using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class CuerpoDeAgua
    {
        public int idCuerpoDeAgua { get; set; }
        public ParametroGenerico tipoCuerpoAgua { get; set; }
        public Region region { get; set; }
        public Comuna comuna { get; set; }
        public String nombreCuerpoAgua { get; set; }
        public String comunaCad { get; set; } 

        // MÉTODOS (Constructores)
        public CuerpoDeAgua()
        {
        }


        public String tipoCuerpoAguaString {

            get
            {

                String tipoCuerpoAguaString = "";
                if (tipoCuerpoAgua != null && tipoCuerpoAgua.id > 0)
                {
                    tipoCuerpoAguaString = tipoCuerpoAgua.descripcion;

                }
                return tipoCuerpoAguaString;
            }
        }

        public String regionString{

            get
            {
                String regionString = "";
                if (region != null && region.id_region > 0)
                {
                    regionString = region.region;
                }
                return regionString;
            }
        }

        public String comunaString{

            get
            {
                String comunaString = "";
                if (comuna != null && comuna.id_comuna > 0)
                {
                    comunaString = comuna.comuna;
                }
                return comunaString;
            }
        }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {

            public int idCuerpoDeAgua { get; set; }
            public ParametroGenerico tipoCuerpoAgua { get; set; }
            public Region region { get; set; }
            public Comuna comuna { get; set; }
            public String nombreCuerpoAgua { get; set; }
            public String comunaCad { get; set; }
        }
    }
}
