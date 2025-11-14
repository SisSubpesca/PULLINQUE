using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class TipoArchivo
    {
        public int idTipoArchivo = 0;
        public string nombreTipoArchivo = "";
       
        // MÉTODOS (Constructores)
        public TipoArchivo()
        {
        }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {

            public int idTipoArchivo { get; set; }
            public String nombreTipoArchivo { get; set; }
        }
    }
}
