using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Contantes;

namespace Datos.Entidades
{
     [Serializable()]
    public class EstructuraTecnicaPT
    {

         public int idEstructPT { get; set; }
         public int idProyTec { get; set; }
         public ParametroGenerico tipoEstructura { get; set; }
         public int numEstructInstalar { get; set; }
         public ParametroGenerico formaEstructura { get; set; }
         public ParametroGenerico unidadMedida { get; set; }
         public ParametroGenerico volumenUnidadMedida { get; set; }
         public ParametroGenerico tipoAnio { get; set; }
         public ParametroGenerico estado { get; set; }
         public float largo { get; set; }
         public float ancho { get; set; }
         public float alto { get; set; }
         public float diametro { get; set; }
         public float densidadSiembra { get; set; }
         public float volumenValorMedida { get; set; }
         public List<ValorParametroAnioPT> anios { get; set; }
         public float totalAcumNumero { get; set; }
         public float totalAcumDim { get; set; }
         public int numColectores { get; set; }
         public int numLineas { get; set; }
         public List<EstructuraTecnicaPT> estructuraTecnica_directoSustrato { get; set; }
         

         public int index { get; set; }
         public int accion { get; set; }

         public string aniosCad {
             get
             {

                 string aniosCadAux = "";

                 if (this.tipoAnio != null && this.tipoAnio.id == rbTipo.ESTRUCTURAS_ANIO_MAXIMO)
                 {

                     if (this.anios != null && this.anios.Count() > 0)
                     {
                         foreach (ValorParametroAnioPT anioAux in this.anios)
                         {
                             if (!aniosCadAux.Equals(""))
                             {
                                 aniosCadAux = aniosCadAux + "<br>Máximo :" + anioAux.valor;
                             }
                             else
                             {
                                 aniosCadAux = "<br>Año " + anioAux.anio + ":" + anioAux.valor;
                             }
                         }
                     }

                 }else {

                     if (this.anios != null && this.anios.Count() > 0)
                     {
                         foreach (ValorParametroAnioPT anioAux in this.anios)
                         {
                             if (!aniosCadAux.Equals(""))
                             {
                                 aniosCadAux = aniosCadAux + "<br>Año " + anioAux.anio + ":" + anioAux.valor;
                             }
                             else
                             {
                                 aniosCadAux = "<br>Año " + anioAux.anio + ":" + anioAux.valor;
                             }
                         }
                     }
                 }

               
                 return aniosCadAux;
             }
         }

         public EstructuraTecnicaPT()
         {
         }

         [Serializable]
         public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
         {
             public int idEstructPT { get; set; }
             public int idProyTec { get; set; }
             public ParametroGenerico tipoEstructura { get; set; }
             public int numEstructInstalar { get; set; }
             public ParametroGenerico formaEstructura { get; set; }
             public ParametroGenerico unidadMedida { get; set; }
             public ParametroGenerico volumenUnidadMedida { get; set; }
             public ParametroGenerico tipoAnio { get; set; }
             public float largo { get; set; }
             public float ancho { get; set; }
             public float alto { get; set; }
             public float diametro { get; set; }
             public float densidadSiembra { get; set; }
             public float volumenValorMedida { get; set; }
             public List<ValorParametroAnioPT> anios { get; set; }
             public float totalAcumNumero { get; set; }
             public float totalAcumDim { get; set; }
             public int index { get; set; }
             public int accion { get; set; }
             public ParametroGenerico estado { get; set; }
             public int numColectores { get; set; }
             public int numLineas { get; set; }
             public List<EstructuraTecnicaPT> estructuraTecnica_directoSustrato { get; set; }
             
         }

    }
}
