using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Contantes
{

    
    /**
     *  Tipos de accion a realizar en una fila dentro las grillas 
     */
    public static class accion
    {

        public static readonly int INGRESAR     = 0;
        public static readonly int MODIFICAR    = 1;
        public static readonly int ELIMINAR     = 2;
        public static readonly int IGNORAR      = 3;
        public static readonly int LISTADO      = 4;
    }

}
