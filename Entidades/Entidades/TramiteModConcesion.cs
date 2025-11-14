using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{

     [Serializable()]
    public class TramiteModConcesion
    {

         public int idTramiteMod { get; set; }
         public int idSolConcesion { get; set; }
         public ParametroGenerico centro  { get; set; }
         public Persona titular { get; set; }
    }

     [Serializable]
     public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
     {

         public int idTramiteMod { get; set; }
         public int idSolConcesion { get; set; }
         public ParametroGenerico centro { get; set; }
         public Persona titular { get; set; }

     }
}
