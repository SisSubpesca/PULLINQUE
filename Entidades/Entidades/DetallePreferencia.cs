using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{

    [Serializable()]
    public class DetallePreferencia
    {
        public int idPreferenciaRel { get; set; }
        public String nombrePreferenciaRel { get; set; }

    }

    // SUBCLASE 
    [Serializable]
    public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
    {
        public int idPreferenciaRel { get; set; }
        public String nombrePreferenciaRel { get; set; }
    }
}
