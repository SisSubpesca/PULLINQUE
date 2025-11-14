using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class HistUnidEspacialVigencia
    {

       public int idHistUE  { get; set; }
       public int idSolConcesion  { get; set; }
       public int idResolucion { get; set; }
       public ParametroGenerico estadoVigencia  { get; set; }
       public string observaciones  { get; set; }
       public DateTime fechaIngresoSist { get; set; }
       public string cadena { get; set; }
       public int idDocConcesion { get; set; }
       public string cadenaDocConcesion { get; set; }

        public HistUnidEspacialVigencia()
        {
        }

        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idHistUE { get; set; }
            public int idSolConcesion { get; set; }
            public int idResolucion { get; set; }
            public ParametroGenerico estadoVigencia { get; set; }
            public string observaciones { get; set; }
            public DateTime fechaIngresoSist { get; set; }
            public string cadena { get; set; }
            public int idDocConcesion { get; set; }
            public string cadenaDocConcesion { get; set; }
        }
    }
}
