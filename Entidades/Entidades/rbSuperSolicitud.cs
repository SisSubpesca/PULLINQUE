using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 

namespace Datos.Entidades
{
    [Serializable]
    public class rbSuperSolicitud
    {
        public int idSuperSolicitud { get; set; }
        public int idEstadoSuper { get; set; }
        public int service_application_id { get; set; }
        public int ms_application_id { get; set; }
        public string company_rut { get; set; }
        public string cup { get; set; }
        public DateTime fechaIngreso { get; set; }
        public DateTime fechaModificacion { get; set; }
        public byte[] archivoFinal { get; set; }

        // Constructor
        public rbSuperSolicitud()
        {
        }
    }
}