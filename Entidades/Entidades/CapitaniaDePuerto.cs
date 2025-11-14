using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class CapitaniaDePuerto
    {

        public int idCapitaDePuerto { get; set; }
        public int codigo { get; set; }
        public string nombreCapitaniaDePuerto { get; set; }
        public int requiereCert { get; set; }
       
        // MÉTODOS (Constructores)
        public CapitaniaDePuerto()
        {
        }


        public string requiereCertString
        {

            get
            {
                if (requiereCert == 0)
                {
                    return "No";
                }
                else if (requiereCert == 1)
                {
                    return "Si";
                }
                else {
                    return "-- Seleccione --";
                }
            }
        }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {

            public int idCapitaDePuerto { get; set; }
            public int codigo { get; set; }
            public String nombreCapitaniaDePuerto { get; set; }
            public int requiereCert { get; set; }
        }
        
    }
}
