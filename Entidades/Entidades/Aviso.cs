using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class Aviso
    {
        public int idAviso { get; set; }
        public SolicitudConcesion solicitud { get; set; }
        public string claveTemplate { get; set; }
        public string subject { get; set; }
        public string mensaje { get; set; }
        public DateTime fechaIngreso { get; set; }

        public Aviso() { }

        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idAviso { get; set; }
            public SolicitudConcesion solicitud { get; set; }
            public string claveTemplate { get; set; }
            public string subject { get; set; }
            public string mensaje { get; set; }
            public DateTime fechaIngreso { get; set; }
        
        }
    }
}
