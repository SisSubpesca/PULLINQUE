using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class DestinatarioTemplate
    {
        public int idDestTemplate { get; set; }
        public string claveTemplateAviso { get; set; }
        public Rol rol { get; set; }
        public string nombreDestinatario { get; set; }
        public string emailDestinatario { get; set; }
        public bool aplicaEnvio { get; set; }
        public TemplateAviso templateAviso { get; set; }
        public Usuario usuario { get; set; }
        
            
        public DestinatarioTemplate()
        { }

        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idDestTemplate { get; set; }
            public string claveTemplateAviso { get; set; }
            public Rol rol { get; set; }
            public string nombreDestinatario { get; set; }
            public string emailDestinatario { get; set; }
            public bool aplicaEnvio { get; set; }
            public TemplateAviso templateAviso { get; set; }
            public Usuario usuario { get; set; }


        }
    }
}
