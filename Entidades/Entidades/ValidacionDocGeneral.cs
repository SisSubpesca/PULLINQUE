using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{

    [Serializable()]
    public class ValidacionDocGeneral
    {

       public int idValDocGeneral { get; set; }
       public ParametroGenerico pestania { get; set; }
       public ParametroGenerico tipoIO { get; set; }
       public ParametroGenerico tipoDoc { get; set; }
       public int numero { get; set; }
       public int fecha { get; set; }
       public int numeroCI { get; set; }
       public int fechaCI { get; set; } 
       public int archivoBinario { get; set; }
       public int subRequerimiento { get; set; }
       public int tipoDocumento { get; set; }  
       public int tipoOrigen { get; set; }  
       public int tipoDestinatario { get; set; }
       public int numRequerimiento { get; set; }
       public int ambito { get; set; }
       public int panelRequerimientos { get; set; }
        

       public ValidacionDocGeneral(){
       }

       [Serializable]
       public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
       {
           public int idValDocGeneral { get; set; }
           public ParametroGenerico pestania { get; set; }
           public ParametroGenerico tipoIO { get; set; }
           public ParametroGenerico tipoDoc { get; set; }
           public int numero { get; set; }
           public int fecha { get; set; }
           public int numeroCI { get; set; }
           public int fechaCI { get; set; }
           public int archivoBinario { get; set; }
           public int subRequerimiento { get; set; }
           public int tipoDocumento { get; set; }
           public int tipoOrigen { get; set; }
           public int tipoDestinatario { get; set; }
           public int numRequerimiento { get; set; }
           public int ambito { get; set; }
           public int panelRequerimientos { get; set; }
       } 

    }
}
