using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class DependenciaSupeditados
    {

         public int idDepSupeditado {get; set;}
         public ParametroGenerico tipoSupeditado {get; set;}
         public DocumentoAmbito docPestana { get; set; }
         public List<DocumentoAmbito> docPestana_List { get; set; } 
         public SolicitudConcesion solicitudConcesionDep { get; set; }
         public EvaluacionUOT_UE evaluacionUOT_UE {get; set;}
         public List<EvaluacionUOT_UE> evaluacionUOT_UE_List { get; set; }
         public string observaciones { get; set; }

         public int resultadoId { get; set; }

         private int _index;
         private int _accion;

         public int index
         {
             get { return _index; }
             set { _index = value; }
         }

         public int accion
         {
             get { return _accion; }
             set { _accion = value; }
         }


         public string listPertSupeditados
         {

             get
             {
                 string pert = "";
                 int i = 0;
                 if (evaluacionUOT_UE_List != null && evaluacionUOT_UE_List.Count > 0)
                 {
                     foreach (EvaluacionUOT_UE evaluacionUOT_UE in evaluacionUOT_UE_List)
                     {
                         if (evaluacionUOT_UE != null && evaluacionUOT_UE.solicitudConcesion != null)
                         {
                             if (i == 0)
                             {
                                 pert = evaluacionUOT_UE.solicitudConcesion.numPert;
                             }
                             else
                             {
                                 pert = pert + "," + evaluacionUOT_UE.solicitudConcesion.numPert;
                             }
                             i++;
                         }
                     }
                 }
                 else if (docPestana_List != null && docPestana_List.Count > 0)
                 {
                     foreach (DocumentoAmbito documentoAmbito in docPestana_List)
                     {
                         if (documentoAmbito != null && documentoAmbito.solicitudConcesion_sub != null)
                         {
                             if (i == 0)
                             {
                                 pert = documentoAmbito.solicitudConcesion_sub.numPert;
                             }
                             else
                             {
                                 pert = pert + "," + documentoAmbito.solicitudConcesion_sub.numPert;
                             }
                             i++;
                         }
                     }
                 }
                 return pert;
             }
         }

         [Serializable]
         public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
         {
             public int idDepSupeditado { get; set; }
             public ParametroGenerico tipoSupeditado { get; set; }
             public DocumentoAmbito docPestana { get; set; }
             public List<DocumentoAmbito> docPestana_List { get; set; } 
             public SolicitudConcesion solicitudConcesionDep { get; set; }
             public EvaluacionUOT_UE evaluacionUOT_UE { get; set; }
             public List<EvaluacionUOT_UE> evaluacionUOT_UE_List { get; set; }
             public string observaciones { get; set; }

             public int resultadoId { get; set; }
         }
  
    }
}
