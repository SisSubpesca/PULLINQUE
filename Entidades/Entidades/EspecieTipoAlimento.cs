using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
     [Serializable()]
    public class EspecieTipoAlimento
    {
         public ParametroGenerico especie { get; set; }
         public ParametroGenerico tipoAlimento { get; set; }


         public String nombreTipoAlimento
         {

             get
             {

                 if (tipoAlimento != null)
                 {
                     return tipoAlimento.descripcion;
                 }
                 else
                 {
                     return "";
                 }
             }
         }

         public int idTipoAlimento
         {

             get
             {

                 if (tipoAlimento != null)
                 {
                     return tipoAlimento.id;
                 }
                 else
                 {
                     return 0;
                 }
             }
         }

         public int idEspecie
         {

             get
             {

                 if (especie != null)
                 {
                     return especie.id;
                 }
                 else
                 {
                     return 0;
                 }
             }
         }

         public String nombreEspecie
         {

             get
             {

                 if (especie != null)
                 {
                     return especie.descripcion;
                 }
                 else
                 {
                     return "";
                 }
             }
         }


         [Serializable]
         public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
         {
             public ParametroGenerico especie { get; set; }
             public ParametroGenerico tipoAlimento { get; set; }
         
         }
    }
}
