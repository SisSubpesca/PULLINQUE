using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
     [Serializable()]
    public class EstadoIsla
    {
         public EstadoIsla() { }

         public int idSolConcesion { get; set; }
         public ParametroGenerico estadoActual { get; set; }
         public ParametroGenerico estadoPosterior { get; set; }
         public ParametroGenerico estadoAnterior { get; set; }
         public ParametroGenerico tipoIsla { get; set; }

          //SUBCLASE
         [Serializable]
         public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
         {
             public int idSolConcesion { get; set; }
             public ParametroGenerico estadoActual { get; set; }
             public ParametroGenerico estadoPosterior { get; set; }
             public ParametroGenerico estadoAnterior { get; set; }
             public ParametroGenerico tipoIsla { get; set; }
         
         }
    }
}
