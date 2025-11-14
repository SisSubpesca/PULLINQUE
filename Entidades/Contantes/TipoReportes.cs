using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Contantes
{
    public static class TipoReportes
    {

        public static readonly int REPORTES_UNIDADES_ESPACIALES_DOCUMENTOS = 10;
        public static readonly int REPORTES_UNIDADES_ESPACIALES_COORDENADAS = 11;
        public static readonly int SOLICITUD_EN_TRAMITE_CON_DOCUMENTACION_01 = 2;
        public static readonly int SOLICITUD_EN_TRAMITE_CON_DOCUMENTACION_02 = 3;
        public static readonly int SOLICITUD_EN_TRAMITE_CON_DOCUMENTACION_03 = 4;
        public static readonly int SOLICITUD_EN_TRAMITE_CON_COORDENADAS = 5;
        public static readonly int REPORTE_UNIDADES_ESPACIALES_CENTROS_DE_CULTIVO = 6;
        public static readonly int REPORTE_UNIDADES_ESPACIALES_ANALISIS_DE_VIGENCIA = 7;
        public static readonly int SOLICITUD_EN_TRAMITE_SSP_SSFFA = 20;
        public static readonly int REPORTE_UNIDADES_ESPACIALES_NO_VIGENTES_RESOLUCIONES = 21;
        public static readonly int REPORTE_UNIDADES_ESPACIALES_NO_VIGENTES_TRAMITES = 22;


        public static readonly string REPORTES_UNIDADES_ESPACIALES_DOCUMENTOS_STRING = "Reportes de Unidades Espaciales (Documentos)";
        public static readonly string REPORTES_UNIDADES_ESPACIALES_COORDENADAS_STRING = "Reportes de Unidades Espaciales (Coordenadas)";
        public static readonly string SOLICITUD_EN_TRAMITE_CON_DOCUMENTACION_01_STRING = "Solicitudes en trámite con documentación 1 (IT UOT-DIFROL)";
        public static readonly string SOLICITUD_EN_TRAMITE_CON_DOCUMENTACION_02_STRING = "Solicitudes en trámite con documentación 2 (Evaluación Ambiental)";
        public static readonly string SOLICITUD_EN_TRAMITE_CON_DOCUMENTACION_03_STRING = "Solicitudes en trámite con documentación 3 (Planos 14 ter - Resol SSFFAA)";
        public static readonly string SOLICITUD_EN_TRAMITE_CON_COORDENADAS_STRING = "Solicitudes en trámite con coordenadas";
        public static readonly string REPORTE_UNIDADES_ESPACIALES_CENTROS_DE_CULTIVO_STRING = "Reporte de centros de cultivos";
        public static readonly string REPORTE_UNIDADES_ESPACIALES_ANALISIS_DE_VIGENCIA_STRING = "Análisis de vigencia";
        public static readonly string SOLICITUD_EN_TRAMITE_SSP_SSFFA_STRING = "Solicitudes que teniendo una Resolución Marina Rechaza tienen una Resolución SSP aprueba";
        public static readonly string REPORTE_UNIDADES_ESPACIALES_NO_VIGENTES_RESOLUCIONES_STRING = "Unidades Espaciales NO vigentes con resolución SSPA o SSFFAA Vigente.";
        public static readonly string REPORTE_UNIDADES_ESPACIALES_NO_VIGENTES_TRAMITES_STRING = "Unidades Espaciales NO vigentes con trámites en curso.";

    }
}
