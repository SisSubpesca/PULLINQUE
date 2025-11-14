using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class ConsultorAmbiental
    {
        public int numInscripcion { get; set; }
        public String nombre { get; set; }
        public DateTime fechaIniVigencia { get; set; }
        public DateTime fechaFinVigencia { get; set; }
        public String condVigencia { get; set; }
        public String correo { get; set; }
        public String fono { get; set; }


        // MÉTODOS (Constructores)
        public ConsultorAmbiental()
        {
        }


        public string inicioVigenciaString
        {
            get
            {
                if (fechaIniVigencia != null && !fechaIniVigencia.Equals(""))
                {
                    return fechaIniVigencia.ToShortDateString();
                }
                return "";
            }

        }

        public string finVigenciaString
        {
            get
            {
                if (fechaFinVigencia != null && !fechaFinVigencia.Equals("") && fechaFinVigencia != default(DateTime))
                {
                    return fechaFinVigencia.ToShortDateString();
                }
                return "";
            }

        }

        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int numInscripcion { get; set; }
            public String nombre { get; set; }
            public DateTime fechaIniVigencia { get; set; }
            public DateTime fechaFinVigencia { get; set; }
            public String condVigencia { get; set; }
            public String correo { get; set; }
            public String fono { get; set; }
        
        }
    }
}
