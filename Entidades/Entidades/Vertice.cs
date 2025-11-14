using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class Vertice
    {
        public Usuario usuario { get; set; }
        public int idVertice { get; set; }
        public int idPoligono { get; set; }
        public int idSolicitud { get; set; }
        public ParametroGenerico vertice { get; set; }
        public int latitudHora { get; set; }
        public int latitudMinuto { get; set; }
        public double latitudSegundo { get; set; }
        public int longitudHora { get; set; }
        public int longitudMinuto { get; set; }
        public double longitudSegundo { get; set; }
        public double latitudDecimal { get; set; }
        public double longitudDecimal { get; set; }
        public double utmN { get; set; }
        public double utmE { get; set; }
        public int index { get; set; }
        public int accion { get; set; }
        
        // MÉTODOS (Constructores)
        public Vertice()
        {
        }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public Usuario usuario { get; set; }
            public int idSolicitud { get; set; }
            public int idVertice { get; set; }
            public int idPoligono { get; set; }
            public ParametroGenerico vertice { get; set; }
            public int latitudHora { get; set; }
            public int latitudMinuto { get; set; }
            public double latitudSegundo { get; set; }
            public int longitudHora { get; set; }
            public int longitudMinuto { get; set; }
            public double longitudSegundo { get; set; }
            public double latitudDecimal { get; set; }
            public double longitudDecimal { get; set; }
            public double utmN { get; set; }
            public double utmE { get; set; }
            public int index { get; set; }
            public int accion { get; set; }
        }

    }
}
