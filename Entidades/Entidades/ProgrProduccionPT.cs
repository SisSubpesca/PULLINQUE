using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class ProgrProduccionPT
    {

         public int idProgrProduccion { get; set; }
         public int idProyectoTecnico { get; set; }
         public ParametroGenerico especie { get; set; }
         public ParametroGenerico grupo { get; set; }
         public ParametroGenerico etapaCultivo { get; set; }
         public ParametroGenerico tipoUnidProgramaProd { get; set; }
         public float densidad { get; set; }
         public float produccionUltimoAnio { get; set; }
         public ParametroGenerico tipoPesoPromEjemplares { get; set; }
         public float pesoPromSR { get; set; }//peso prom sin rango
         public float pesoPromR1 { get; set; }//peso prom rango1
         public float pesoPromR2 { get; set; }//peso prom rango2

         public ParametroGenerico tipoAnio { get; set; }
         public List<ValorParametroAnioPT> aniosProgrProd { get; set; }

         public int index { get; set; }
         public int accion { get; set; }

         public string DescripcionEspecie { get { return especie.descripcion; } }
         public string IDEspecie { get { return (especie.id).ToString(); } }

         public string aniosCadProgrProd
         {
             get
             {
                 string aniosCadAux = "";
                 if (this.aniosProgrProd != null && this.aniosProgrProd.Count() > 0)
                 {
                     foreach (ValorParametroAnioPT anioAux in this.aniosProgrProd)
                     {
                         if (!aniosCadAux.Equals(""))
                         {
                             aniosCadAux = aniosCadAux + "<br>Año " + anioAux.anio + ": " + anioAux.valorProgrProd;
                         }
                         else
                         {
                             aniosCadAux = "<br>Año " + anioAux.anio + ": " + anioAux.valorProgrProd;
                         }
                     }

                 }
                 return aniosCadAux;
             }
         }

         public string aniosCadPT
         {
             get
             {
                 string aniosCadAux = "";
                 if (this.aniosProgrProd != null && this.aniosProgrProd.Count() > 0)
                 {
                     foreach (ValorParametroAnioPT anioAux in this.aniosProgrProd)
                     {
                         if (!aniosCadAux.Equals(""))
                         {
                             aniosCadAux = aniosCadAux + "<br>Año " + anioAux.anio + ": " + anioAux.valor;
                         }
                         else
                         {
                             aniosCadAux = "<br>Año " + anioAux.anio + ": " + anioAux.valor;
                         }


                     }

                 }
                 return aniosCadAux;
             }
         }


         public ProgrProduccionPT()
         { }

         [Serializable]
         public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
         {
             public int idProgrProduccion { get; set; }
             public int idProyectoTecnico { get; set; }
             public ParametroGenerico especie { get; set; }
             public ParametroGenerico grupo { get; set; }
             public ParametroGenerico etapaCultivo { get; set; }
             public ParametroGenerico tipoUnidProgramaProd { get; set; }
             public float densidad { get; set; }
             public float produccionUltimoAnio { get; set; }
             public float pesoPromSR { get; set; }//peso prom sin rango
             public float pesoPromR1 { get; set; }//peso prom rango1
             public float pesoPromR2 { get; set; }//peso prom rango2
             public ParametroGenerico tipoPesoPromEjemplares { get; set; }
             public ParametroGenerico tipoAnio { get; set; }
             public List<ValorParametroAnioPT> aniosProgrProd { get; set; }

             public int index { get; set; }
             public int accion { get; set; }
         }
    }
}
