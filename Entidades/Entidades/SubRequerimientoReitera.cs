using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{

      [Serializable()]
    public class SubRequerimientoReitera
    {
          public SubRequerimiento padre { get; set; }
          public SubRequerimiento reitera { get; set; }

          // SUBCLASE 
          [Serializable]
          public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
          {
              public SubRequerimiento padre { get; set; }
              public SubRequerimiento reitera { get; set; }
          }
    }
}
