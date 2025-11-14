using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
        public class TitularRegConcesiones
        {
            public int idTitularRCA { get; set; }
            public ParametroGenerico idEstadoRegistro { get; set; }
            public int idTramiteRCA { get; set; }
            public DateTime fechaInscripcion { get; set; }
            public string codigoCentro { get; set; }
            public string adquirienteNombre { get; set; }
            public int adquirienteRut { get; set; }
            public string adquirienteDv { get; set; }
            public int idTipoTramRCA { get; set; }
            public string nombreTipoTramRCA { get; set; }
            public DateTime fechaIngresoRB { get; set; }
            public DateTime fechaProcesamientoRB { get; set; }
            public string observaciones { get; set; }

            public string rutCompleto { get; set; }
        }
}

