using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
     [Serializable()]
    public class DetalleDatosSolicitud
    {
         public Usuario usuario { get; set; } 
         public int idDetDato { get; set; }
         public int idSolConcesion { get; set; }
         public ParametroGenerico tipoCentro { get; set; }
         public ParametroGenerico tipoEvaluacion { get; set; }
         public float superficieSectorAmerb { get; set; }
         public float porcentSectorAmerb { get; set; }
         public float profundidadMin { get; set; }
         public string periodoOperacionCol { get; set; }
         public string observaciones { get; set; }
         public DateTime vigenciaColector { get; set; }
         public int codigo { get; set; }
         public int numIdentSolicitud { get; set; }
         public int numeroCI { get; set; }
         public DateTime fechaCI { get; set; }
         public string nombreAmerbPadre { get; set; }
        
          // SUBCLASE 
         [Serializable]
         public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
         {
             public Usuario usuario { get; set; }
             public int idDetDato { get; set; }
             public int idSolConcesion { get; set; }
             public ParametroGenerico tipoCentro { get; set; }
             public ParametroGenerico tipoEvaluacion { get; set; }
             public float superficieSectorAmerb { get; set; }
             public float porcentSectorAmerb { get; set; }
             public float profundidadMin { get; set; }
             public string periodoOperacionCol { get; set; }
             public string observaciones { get; set; }
             public DateTime vigenciaColector { get; set; }
             public int codigo { get; set; }
             public int numIdentSolicitud { get; set; }
             public int numeroCI { get; set; }
             public DateTime fechaCI { get; set; }
             public string nombreAmerbPadre { get; set; }
         
         }

    }
}
