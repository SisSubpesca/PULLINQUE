using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{

    [Serializable()]
    public class EstructuraTecnica
    {

        public int idEstructura { get; set; }
        public string nombreEstructura { get; set; }
        public ParametroGenerico tipoDimension { get; set; }
        public bool aplicaArea { get; set; }
        public bool aplicaVolumen { get; set; }
        public int aplicaAreaFiltro { get; set; }
        public int aplicaVolumenFiltro { get; set; }

        public EstructuraTecnica()
        { }

        public int _tipoDimension;


        public int idTipoDimension {

            get {

                _tipoDimension = tipoDimension.id;
                return _tipoDimension;
            
            }
        }

        public string tipoDimensionString {
            get {

                return tipoDimension.descripcion;
            }
        }

        public string aplicaAreaString {
            get {
                if (aplicaArea)
                {
                    return "Si";
                }else{
                    return "No";
                }
            }
        }

        public string aplicaVolumenString
        {
            get
            {
                if (aplicaVolumen)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idEstructura { get; set; }
            public string nombreEstructura { get; set; }
            public ParametroGenerico tipoDimension { get; set; }
            public bool aplicaArea { get; set; }
            public bool aplicaVolumen { get; set; }
            public int aplicaAreaFiltro { get; set; }
            public int aplicaVolumenFiltro { get; set; }
        }


    }
}
