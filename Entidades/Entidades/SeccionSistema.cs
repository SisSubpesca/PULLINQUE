using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class SeccionSistema
    {
        public int idSeccionSist { get; set; }
        public ParametroGenerico estadoVigencia { get; set; }
        public ParametroGenerico menu { get; set; }
        public string nombreSeccion { get; set; }
        public List<Accion> accionesSeccion { get; set; }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idSeccionSist { get; set; }
            public ParametroGenerico estadoVigencia { get; set; }
            public ParametroGenerico menu { get; set; }
            public string nombreSeccion { get; set; }
            public List<Accion> accionesSeccion { get; set; }
        }

    }
}
