using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
     public class Feriado
    {

         public int idFeriado { get; set; }
         public DateTime fecha { get; set; }
         public string descripcion { get; set; }


         public string fechaString
         {
             get
             {
                 if (fecha != null && !fecha.Equals(""))
                 {
                     return fecha.ToShortDateString();
                 }
                 return "";
             }

         }

         public Feriado()
         {
         }

         [Serializable]
         public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
         {
             public int idFeriado { get; set; }
             public DateTime fecha { get; set; }
             public string descripcion { get; set; }
         
         }
    }
}
