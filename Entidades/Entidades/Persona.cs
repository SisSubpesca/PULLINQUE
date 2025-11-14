using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class Persona
    {
        public int rutPersona;
        public char dvPersona;
        public TipoPersona tipoPersona;
        public String nombreSolicitante;
        public String genero;
        
        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {

            public int rutPersona { get; set; }
            public char dvPersona { get; set; }
            public TipoPersona tipoPersona { get; set; }
            public String nombreSolicitante { get; set; }
            public String genero { get; set; }
           
        }

       
    }
}
