using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Contantes
{
    /*
     * Copia de la tabla de estados generadors  (solo se indican los que son necesarios para tomar decisiones en el sistema)
     */ 
    public static class rbEstadosGenerales
    {

        public static readonly int CERO         = 0;
        public static readonly int VIGENTE      = 6;
        public static readonly int NO_VIGENTE   = 7;
        public static readonly int CONFORME     = 8;
        public static readonly int NO_CONFORME  = 9;
        public static readonly int SOLICITUD_INICIADA = 1;

        public static readonly int APRUEBA = 10;
        public static readonly int RECHAZA = 11;
        public static readonly int REMISION = 12;
        public static readonly int SUPEDITADA = 13;
        public static readonly int PENDIENTE = 29;

        public static readonly int SE_SOMETE_AL_SEA = 22;
        public static readonly int NO_SE_SOMETE_AL_SEA = 23;

        public static readonly int REGULARIZACION = 28;

        public static readonly int TRAMITE_RELOCALIZACION_APROBADO = 38;
        public static readonly int TRAMITE_RELOCALIZACION_RECHAZADO = 39;
        public static readonly int TRAMITE_RELOCALIZACION_TRAMITE = 40;

        public static readonly int SECTOR_RELOCALIZACION_EN_TRAMITE = 41;



        public static readonly int ERROR_IGNORADO = 42;
        public static readonly int ERROR_CORREGIDO = 43;
        public static readonly int ERROR_AUN_NO_EVALUADO = 44;



        public static readonly int SI = 65;
        public static readonly int NO = 66;

        public static readonly int DEVOLUCION_ANTECEDENTES = 83;

        public static readonly int GrupoSuspendidoCerrado = 94;


    }
}
