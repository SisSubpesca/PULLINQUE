using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades.Relocalizacion
{
    [Serializable()]
    public class AsocInformeSolicitud
    {

        public int index { get; set; }
        public int accion { get; set; }
        public int idInformeRel	 { get; set; }
        public int idSolConcesion { get; set; }
        public ParametroGenerico estadoAsoc  { get; set; }
        public int codigoCentro  { get; set; }
        public string titulares  { get; set; }
        public string comunas  { get; set; }
        public DateTime fechaIngresoSistema  { get; set; }


        public AsocInformeSolicitud()
        { }

         // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int index { get; set; }
            public int accion { get; set; }
            public int idInformeRel { get; set; }
            public int idSolConcesion { get; set; }
            public ParametroGenerico estadoAsoc { get; set; }
            public int codigoCentro { get; set; }
            public string titulares { get; set; }
            public string comunas { get; set; }
            public DateTime fechaIngresoSistema { get; set; }
        
        }

    }
}
