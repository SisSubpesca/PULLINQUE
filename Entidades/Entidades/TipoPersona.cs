using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class TipoPersona
    {
        public int id;
        public String descripcion;
        public TipoPersona tipoPersona;

        // MÉTODOS (Constructores)
        public TipoPersona()
        {
        }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {

            public int id { get; set; }
            public String descripcion { get; set; }
            public TipoPersona tipoPersona { get; set; }
            

        }
    }
}
