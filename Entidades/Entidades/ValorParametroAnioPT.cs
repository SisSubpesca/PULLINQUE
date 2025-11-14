using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class ValorParametroAnioPT
    {

        public int idRegistro { get; set; }
        public int idclaveParametro { get; set; }
        public int anio { get; set; }
        public int valor { get; set; }
        public float valorProgrProd { get; set; }

        public ValorParametroAnioPT(){
         }

        [Serializable]
        public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idRegistro { get; set; }
            public int idclaveParametro { get; set; }
            public int anio { get; set; }
            public int valor { get; set; }
            public float valorProgrProd { get; set; }
        }

    }
}
