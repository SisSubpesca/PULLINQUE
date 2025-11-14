using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class PlazoDocumentoSolicitud
    {

        public int idPlazoDoc { get; set; }
        public ParametroGenerico docOrigen { get; set; } //REQUERIMIENTO INICIAL
        public ParametroGenerico docAmplia { get; set; } //REQUERIMIENTO QUE GATILLA LA AMPLIACIÓN DE PLAZO
        public ParametroGenerico docResuelve { get; set; } //requerimiento que resuelve la ampliación de plazo. 
        public ParametroGenerico tipoUE { get; set; }//tipo unidad espacial
        public DateTime fechaVencimientoInicial { get; set; } 
        public DateTime fechaNuevoVencimiento { get; set; }
        public int plazoDias { get; set; }
        public int plazoMeses { get; set; } 


        public string tipoUEString{
            get
            {
                if (tipoUE != null)
                {
                    return tipoUE.descripcion;
                }
                return "";
            }
         
        }

        public string docOrigenString
        {
            get
            {
                if (docOrigen != null)
                {
                    return docOrigen.descripcion;
                }
                return "";
            }

        }

         //SUBCLASE
        [Serializable]
        public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idPlazoDoc { get; set; }
            public ParametroGenerico docOrigen { get; set; } //REQUERIMIENTO INICIAL
            public ParametroGenerico docAmplia { get; set; } //REQUERIMIENTO QUE GATILLA LA AMPLIACIÓN DE PLAZO
            public ParametroGenerico docResuelve { get; set; }
            public DateTime fechaVencimientoInicial { get; set; }
            public DateTime fechaNuevoVencimiento { get; set; }
            public int plazoDias { get; set; }
            public int plazoMeses { get; set; }
            public ParametroGenerico tipoUE { get; set; }
        
        }

    }
}
