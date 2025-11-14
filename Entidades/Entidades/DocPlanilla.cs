using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace Datos.Entidades
{
    [Serializable()]
    public class DocPlanilla
    {

        public int idDocPlanilla { get; set; }
        public ParametroGenerico vigencia { get; set; }
        public HttpPostedFile archivo { get; set; }
        public byte[] bytes { get; set; }
        public String nombreDocPlanilla { get; set; }
        public String descripcion { get; set; }
        public DateTime fechaIngresoSistema { get; set; }
        public String tramiteCad { get; set; }
        

        public DocPlanilla()
        { }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {

            public int idDocPlanilla { get; set; }
            public ParametroGenerico vigencia { get; set; }
            public HttpPostedFile archivo { get; set; }
            public byte[] bytes { get; set; }
            public String nombreDocPlanilla { get; set; }
            public String descripcion { get; set; }
            public DateTime fechaIngresoSistema { get; set; }
            public String tramiteCad { get; set; }

        }
    }
}
