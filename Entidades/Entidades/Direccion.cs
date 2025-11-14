using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class Direccion
    {
        public int rutPersona;
        public string direccion;
        public Region region;
        public Provincia provincia;
        public Comuna comuna;
        public Boolean casaMatriz;

        public List<Contacto> contactos = new List<Contacto>();

        // MÉTODOS (Constructores)
        public Direccion()
        {
        }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int rutPersona { get; set; }
            public string direccion { get; set; }
            public Region region { get; set; }
            public Provincia provincia { get; set; }
            public Comuna comuna { get; set; }
            public Boolean casaMatriz { get; set; }
        }
    }
}
