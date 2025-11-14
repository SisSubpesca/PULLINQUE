using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class GrupoPT
    {
        public int idGrupoPT { get; set; }
        public int idProyectoTecnico { get; set; }
        public ParametroGenerico especie { get; set; }
        public ParametroGenerico grupo { get; set; }
        public ParametroGenerico etapaCultivo { get; set; }
        public DateTime fechaIngresoSistema { get; set; } 
        
        public int index { get; set; }
        public int accion { get; set; }

        public List<ProgrProduccionPT> list_ProgrProd { get; set; }

        public GrupoPT()
        {
        }

        [Serializable]
        public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idGrupoPT { get; set; }
            public int idProyectoTecnico { get; set; }
            public ParametroGenerico especie { get; set; }
            public ParametroGenerico grupo { get; set; }
            public ParametroGenerico etapaCultivo { get; set; }
            public DateTime fechaIngresoSistema { get; set; }

            public List<ProgrProduccionPT> list_ProgrProd { get; set; }

            public int index { get; set; }
            public int accion { get; set; }
        
        }
    }
}
