using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subpesca.Acceso
{
    public static class CronSchedule
    {

        /* Los jobs se ejecutaran una vez al día entre las 11:00 AM y las 12:00  */

        public static string RequerimientoCambioEstadoVenc = "0 29 11 * * ?";
        public static string RequerimientoSinMov = "0 31 11 * * ?";
        public static string CarpetaNoAsignada = "0 33 11 * * ?";
        public static string SolicitudConPlazo = "0 35 11 * * ?";
        public static string RequerimientoVencido = "0 40 11 * * ?";
        public static string AvisoPublicacionRadial = "0 42 11 * * ?";
        //Utilizar esto para pruebas
        //public static string AvisoPublicacionRadial = "0 0/1 * 1/1 * ?*";
        public static string SectorRelocalizacionRechazado = "0 27 11 * * ?";
        public static string SinCertificadoCapitaniaPuerto = "0 25 11 * * ?";
        public static string UnidadDependenciaCambiaEstado = "0 20 11 * * ?";

        public static string VencimientoUE = "0 8 11 * * ?";
        public static string SupeditadaTerminadaRechazada = "0 15 11 * * ?";
        public static string SupeditadaTerminadaAprobada = "0 10 11 * * ?";

        /* Actualización de titulares pendientes de creación */
        public static string TitularesPendientesCreacionJob = "0 0 11 * * ?";
        //public static string TitularesPendientesCreacionJob = "0 0/1 * * * ?";

        public static string TitularesPendientesCreacionActualizacionJob = "0 5 11 * * ?";
    }
}
