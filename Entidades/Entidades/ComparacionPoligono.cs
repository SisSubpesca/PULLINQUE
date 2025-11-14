using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class ComparacionPoligono
    {

        
        public Poligono poligonoAntecSector { get; set; }
        public Poligono poligonoInspTerreno { get; set; }
        public ParametroGenerico estado{ get; set; }
        public string tipoConcesInspTerreno { get; set; }
        public string tipoConcesAntecSector { get; set; }

        public string _comparacionString;



        public string comparacionString{
            get
            {
                _comparacionString = estado.descripcion;

                return _comparacionString;
            }
        }

        public ComparacionPoligono() { 
        
        }

        //SUBCLASE
        [Serializable]
        public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public Poligono poligonoAntecSector { get; set; }
            public Poligono poligonoInspTerreno { get; set; }
            public ParametroGenerico estado { get; set; }
            public string tipoConcesInspTerreno { get; set; }
            public string tipoConcesAntecSector { get; set; }
        }

    }
}
