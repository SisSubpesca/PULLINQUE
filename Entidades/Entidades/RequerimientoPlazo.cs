using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class RequerimientoPlazo
    {
        public int idSubReqPlazo { get; set; }
        public int idSolicitud { get; set; }
        public ParametroGenerico subReqOrigen { get; set; }
        public ParametroGenerico subReqDestino { get; set; }
        public ParametroGenerico subReqResuelve { get; set; }
        public ParametroGenerico tipoUnidadEspacial { get; set; }
        public int plazoDias { get; set; }
        public int plazoMeses { get; set; }

        //SUBCLASE
        [Serializable]
        public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idSubReqPlazo { get; set; }
            public int idSolicitud { get; set; }
            public ParametroGenerico subReqOrigen { get; set; }
            public ParametroGenerico subReqDestino { get; set; }
            public ParametroGenerico subReqResuelve { get; set; }
            public ParametroGenerico tipoUnidadEspacial { get; set; }
            public int plazoDias { get; set; }
            public int plazoMeses { get; set; }

        }


    }
}
