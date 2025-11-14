using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    public class TipoAlimentoProyecto
    {
        public int idProyectoTecnico { get; set; }
        public ParametroGenerico tipoAlimento { get; set; }
        public string detalle { get; set; }
        
        
        public TipoAlimentoProyecto(){
        
        }


    }
}
