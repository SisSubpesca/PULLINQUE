using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class AlertaTramiteModConcesion
    {
        public int idAlertaTramRel { get; set; }
        public int idSolConcesion { get; set; }
        public ParametroGenerico tipoWarning { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaIngreso { get; set; }
        public DateTime fechaUltModificacion { get; set; }

         public AlertaTramiteModConcesion()
         {

         }
           
         [Serializable]
         public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
         {

             public int idAlertaTramRel { get; set; }
             public int idSolConcesion { get; set; }
             public ParametroGenerico tipoWarning { get; set; }
             public string descripcion { get; set; }
             public DateTime fechaIngreso { get; set; }
             public DateTime fechaUltModificacion { get; set; }

         }

    
    }
    
   

}
