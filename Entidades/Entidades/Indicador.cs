using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{

    [Serializable()]
    public class Indicador
    {

        public int idIndicador { get; set; }
        public String nombreIndicador { get; set; }
        
        public String tipoPeriodo { get; set; }
        public String descripcionResultado { get; set; }
        public String unidadResultado { get; set; }
        public String formula { get; set; }
        public String consideraciones { get; set; }

        public bool fechaNumerador_desde { get; set; }
        public bool fechaNumerador_hasta { get; set; }
        public bool fechaDenominador_desde { get; set; }
        public bool fechaDenominador_hasta { get; set; }
        public bool fechaConsulta { get; set; }
        public bool regiones { get; set; }
        


        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idIndicador { get; set; }
            public String nombreIndicador { get; set; }

            public String tipoPeriodo { get; set; }
            public String descripcionResultado { get; set; }
            public String unidadResultado { get; set; }
            public String formula { get; set; }
            public String consideraciones { get; set; }

            public bool fechaNumerador_desde { get; set; }
            public bool fechaNumerador_hasta { get; set; }
            public bool fechaDenominador_desde { get; set; }
            public bool fechaDenominador_hasta { get; set; }
            public bool fechaConsulta { get; set; }
            public bool regiones { get; set; }

        }
    }
}
