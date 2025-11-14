using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Contantes
{

    /*
     * Url de paginas por tipo de tramite en los manejadores documentales
     */ 
    public static class paginas
    {
         //Solicitudes de Concesion
         public static String URL_VER_SOLCONCESION = "~/Solicitudes/Registrar/verDocumento.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_DOCUMENTO_SOLCONCESION = "~/Solicitudes/Registrar/administrarDocumento.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_SOLICITUD_SOLCONCESION = "~/Solicitudes/Registrar/administrarSolicitudConcesion.aspx";
         public static String URL_EVALUAR_SOLCONCESION = "~/Solicitudes/Registrar/evaluarDocumento.aspx?idRequerimiento=";
         public static String URL_INGRESO_DOCUMENTO_SOLCONCESION = "~/Solicitudes/Registrar/ingresarDocumento.aspx";
         public static String URL_ERROR_SOLCONCESION = "~/Solicitudes/Registrar/errorGeneral.aspx";

         public static String URL_REFERENCIA_GLOBAL_SOLCONCESION = "~/Solicitudes/Registrar/general.aspx";
         public static String URL_ANTECEDENTES_SECTOR_SOLCONCESION = "~/Solicitudes/Registrar/antecedDelSector.aspx";

         public static String solicitudConcesionSession = "solicitudConcesion";

         //Solicitudes de Modificaciones
         public static String URL_VER_SOLMOD = "~/Solicitudes/Modificacion/verDocumento.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_DOCUMENTO_SOLMOD = "~/Solicitudes/Modificacion/administrarDocumento.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_SOLICITUD_SOLMOD = "~/Solicitudes/Modificacion/administrarSolicitudModificacion.aspx";
         public static String URL_EVALUAR_SOLMOD = "~/Solicitudes/Modificacion/evaluarDocumento.aspx?idRequerimiento=";
         public static String URL_INGRESO_DOCUMENTO_SOLMOD = "~/Solicitudes/Modificacion/ingresarDocumento.aspx";
         public static String URL_ERROR_SOLMOD = "~/Solicitudes/Registrar/errorGeneral.aspx";

         public static String URL_REFERENCIA_GLOBAL_SOLMOD = "~/Solicitudes/Modificacion/generalModificacion.aspx";
         public static String URL_ANTECEDENTES_SECTOR_SOLMOD = "~/Solicitudes/Modificacion/antecedDelSectorModificacion.aspx";

         public static String solicitudModificacionSession = "SolicitudModificacion";

         //Solicitudes de Relocalizacion
         public static String URL_VER_RELOCALIZACION = "~/Solicitudes/Relocalizacion/verDocumentoRelocalizacion.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_DOCUMENTO_RELOCALIZACION = "~/Solicitudes/Relocalizacion/administrarDocumentoRelocalizacion.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_SOLICITUD_RELOCALIZACION = "~/Solicitudes/Relocalizacion/administrarSolicitudRelocalizacion.aspx";
         public static String URL_EVALUAR_RELOCALIZACION = "~/Solicitudes/Relocalizacion/evaluarDocumentoRelocalizacion.aspx?idRequerimiento=";
         public static String URL_INGRESO_DOCUMENTO_RELOCALIZACION = "~/Solicitudes/Relocalizacion/ingresarDocumentoRelocalizacion.aspx";
         public static String URL_ERROR_RELOCALIZACION = "~/Solicitudes/Registrar/errorGeneral.aspx";

         public static String URL_REFERENCIA_GLOBAL_RELOCALIZACION = "~/Solicitudes/Relocalizacion/generalRelocalizacion.aspx";
         public static String URL_ANTECEDENTES_SECTOR_RELOCALIZACION = "~/Solicitudes/Relocalizacion/antecedDelSectorRelocalizacion.aspx";

         public static String solicitudRelocalizacionSession = "SolicitudRelocalizacion";

         //Solicitudes de Centro de Acopio
         public static String URL_VER_CENTRO_DE_ACOPIO = "~/Solicitudes/Acopio/verDocumentoAcopio.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_ACOPIO = "~/Solicitudes/Acopio/administrarDocumentoAcopio.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_ACOPIO = "~/Solicitudes/Acopio/administrarSolicitudAcopio.aspx";
         public static String URL_EVALUAR_CENTRO_DE_ACOPIO = "~/Solicitudes/Acopio/evaluarDocumentoAcopio.aspx?idRequerimiento=";
         public static String URL_INGRESO_DOCUMENTO_CENTRO_DE_ACOPIO = "~/Solicitudes/Acopio/ingresarDocumentoAcopio.aspx";
         public static String URL_ERROR_CENTRO_DE_ACOPIO = "~/Solicitudes/Registrar/errorGeneral.aspx";

         public static String URL_REFERENCIA_GLOBAL_CENTRO_DE_ACOPIO = "~/Solicitudes/Acopio/generalAcopio.aspx";
         public static String URL_ANTECEDENTES_SECTOR_CENTRO_DE_ACOPIO = "~/Solicitudes/Acopio/antecedDelSectorAcopio.aspx";

         public static String solicitudAcopioSession = "SolicitudCentroAcopio";

         //Solicitudes de Centro de Faenamiento
         public static String URL_VER_CENTRO_DE_FAENAMIENTO = "~/Solicitudes/Faenamiento/verDocumentoFaenamiento.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_FAENAMIENTO = "~/Solicitudes/Faenamiento/administrarDocumentoFaenamiento.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_FAENAMIENTO = "~/Solicitudes/Faenamiento/administrarSolicitudFaenamiento.aspx";
         public static String URL_EVALUAR_CENTRO_DE_FAENAMIENTO = "~/Solicitudes/Faenamiento/evaluarDocumentoFaenamiento.aspx?idRequerimiento=";
         public static String URL_INGRESO_DOCUMENTO_CENTRO_DE_FAENAMIENTO = "~/Solicitudes/Faenamiento/ingresarDocumentoFaenamiento.aspx";
         public static String URL_ERROR_CENTRO_DE_FAENAMIENTO = "~/Solicitudes/Registrar/errorGeneral.aspx";

         public static String URL_REFERENCIA_GLOBAL_CENTRO_DE_FAENAMIENTO = "~/Solicitudes/Faenamiento/generalFaenamiento.aspx";
         public static String URL_ANTECEDENTES_SECTOR_CENTRO_DE_FAENAMIENTO = "~/Solicitudes/Faenamiento/antecedDelSectorFaenamiento.aspx";

         public static String solicitudFaenamientoSession = "SolicitudCentroFaenamiento";

         //Solicitudes de Acuicultura en Amerb
         public static String URL_VER_SOLICITUD_AMERB = "~/Solicitudes/Amerb/verDocumentoAmerb.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_AMERB = "~/Solicitudes/Amerb/administrarDocumentoAmerb.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_SOLICITUD_SOLICITUD_AMERB = "~/Solicitudes/Amerb/administrarSolicitudAmerb.aspx";
         public static String URL_EVALUAR_SOLICITUD_AMERB = "~/Solicitudes/Amerb/evaluarDocumentoAmerb.aspx?idRequerimiento=";
         public static String URL_INGRESO_DOCUMENTO_SOLICITUD_AMERB = "~/Solicitudes/Amerb/ingresarDocumentoAmerb.aspx";
         public static String URL_ERROR_SOLICITUD_AMERB = "~/Solicitudes/Registrar/errorGeneral.aspx";

         public static String URL_REFERENCIA_GLOBAL_SOLICITUD_AMERB = "~/Solicitudes/Amerb/generalAmerb.aspx";
         public static String URL_ANTECEDENTES_SECTOR_SOLICITUD_AMERB = "~/Solicitudes/Amerb/antecedDelSectorAmerb.aspx";

         public static String solicitudAmerbSession = "SolicitudCentroAmerb";

         //Solicitudes de Colectores de Semilla
         public static String URL_VER_COLECTORES_SEMILLA = "~/Solicitudes/Colector/verDocumentoColector.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_DOCUMENTO_COLECTORES_SEMILLA = "~/Solicitudes/Colector/administrarDocumentoColector.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_COLECTORES_SEMILLA = "~/Solicitudes/Colector/administrarSolicitudColector.aspx";
         public static String URL_EVALUAR_COLECTORES_SEMILLA = "~/Solicitudes/Colector/evaluarDocumentoColector.aspx?idRequerimiento=";
         public static String URL_INGRESO_DOCUMENTO_COLECTORES_SEMILLA = "~/Solicitudes/Colector/ingresarDocumentoColector.aspx";
         public static String URL_ERROR_COLECTORES_SEMILLA = "~/Solicitudes/Registrar/errorGeneral.aspx";

         public static String URL_REFERENCIA_GLOBAL_COLECTORES_SEMILLA = "~/Solicitudes/Colector/generalColector.aspx";
         public static String URL_ANTECEDENTES_SECTOR_COLECTORES_SEMILLA = "~/Solicitudes/Colector/antecedDelSectorColector.aspx";

         public static String solicitudColectorSession = "SolicitudCentroColector";


         //Solicitudes de Experimentales Amerb
         public static String URL_VER_SOLICITUD_EXPERIMENTALES_AMERB = "~/Solicitudes/ExperimentalesAmerb/verDocumentoExperimentalesAmerb.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_AMERB = "~/Solicitudes/ExperimentalesAmerb/administrarDocumentoExperimentalesAmerb.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_SOLICITUD_EXPERIMENTALES_AMERB = "~/Solicitudes/ExperimentalesAmerb/administrarSolicitudExperimentalesAmerb.aspx";
         public static String URL_EVALUAR_SOLICITUD_EXPERIMENTALES_AMERB = "~/Solicitudes/ExperimentalesAmerb/evaluarDocumentoExperimentalesAmerb.aspx?idRequerimiento=";
         public static String URL_INGRESO_DOCUMENTO_SOLICITUD_EXPERIMENTALES_AMERB = "~/Solicitudes/ExperimentalesAmerb/ingresarDocumentoExperimentalesAmerb.aspx";
         public static String URL_ERROR_SOLICITUD_EXPERIMENTALES_AMERB = "~/Solicitudes/Registrar/errorGeneral.aspx";

         public static String URL_REFERENCIA_GLOBAL_SOLICITUD_EXPERIMENTALES_AMERB = "~/Solicitudes/ExperimentalesAmerb/generalExperimentalesAmerb.aspx";
         public static String URL_ANTECEDENTES_SECTOR_SOLICITUD_EXPERIMENTALES_AMERB = "~/Solicitudes/ExperimentalesAmerb/antecedDelSectorExperimentalesAmerb.aspx";

         public static String solicitudExperimentalesAmerbSession = "SolicitudCentroExperimentalesAmerb";


         //Solicitudes de Experimentales Concesión
         public static String URL_VER_SOLICITUD_EXPERIMENTALES_CONCESION = "~/Solicitudes/ExperimentalesConcesion/verDocumentoExperimentalesConcesion.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_CONCESION = "~/Solicitudes/ExperimentalesConcesion/administrarDocumentoExperimentalesConcesion.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_SOLICITUD_EXPERIMENTALES_CONCESION = "~/Solicitudes/ExperimentalesConcesion/administrarSolicitudExperimentalesConcesion.aspx";
         public static String URL_EVALUAR_SOLICITUD_EXPERIMENTALES_CONCESION = "~/Solicitudes/ExperimentalesConcesion/evaluarDocumentoExperimentalesConcesion.aspx?idRequerimiento=";
         public static String URL_INGRESO_DOCUMENTO_SOLICITUD_EXPERIMENTALES_CONCESION = "~/Solicitudes/ExperimentalesConcesion/ingresarDocumentoExperimentalesConcesion.aspx";
         public static String URL_ERROR_SOLICITUD_EXPERIMENTALES_CONCESION = "~/Solicitudes/Registrar/errorGeneral.aspx";
         
         public static String URL_REFERENCIA_GLOBAL_SOLICITUD_EXPERIMENTALES_CONCESION = "~/Solicitudes/ExperimentalesConcesion/generalExperimentalesConcesion.aspx";
         public static String URL_ANTECEDENTES_SECTOR_SOLICITUD_EXPERIMENTALES_CONCESION = "~/Solicitudes/ExperimentalesConcesion/antecedDelSectorExperimentalesConcesion.aspx";

         public static String solicitudExperimentalesConcesionSession = "SolicitudCentroExperimentalesConcesion";



         //Solicitudes de Acuicultura en ECMPO
         public static String URL_VER_SOLICITUD_ECMPO = "~/Solicitudes/ECMPO/verDocumentoECMPO.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_ECMPO = "~/Solicitudes/ECMPO/administrarDocumentoECMPO.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_SOLICITUD_SOLICITUD_ECMPO = "~/Solicitudes/ECMPO/administrarSolicitudECMPO.aspx";
         public static String URL_EVALUAR_SOLICITUD_ECMPO = "~/Solicitudes/ECMPO/evaluarDocumentoECMPO.aspx?idRequerimiento=";
         public static String URL_INGRESO_DOCUMENTO_SOLICITUD_ECMPO = "~/Solicitudes/ECMPO/ingresarDocumentoECMPO.aspx";
         public static String URL_ERROR_SOLICITUD_ECMPO = "~/Solicitudes/Registrar/errorGeneral.aspx";

         public static String URL_REFERENCIA_GLOBAL_SOLICITUD_ECMPO = "~/Solicitudes/ECMPO/generalECMPO.aspx";
         public static String URL_ANTECEDENTES_SECTOR_SOLICITUD_ECMPO = "~/Solicitudes/ECMPO/antecedDelSectorECMPO.aspx";

         public static String solicitudECMPOSession = "SolicitudCentroECMPO";


         //Solicitudes de Modificación de Centro de Faenamiento
         public static String URL_VER_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO = "~/Solicitudes/ModificacionCentroFaenamiento/verDocumentoModificacionCentroFaenamiento.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO = "~/Solicitudes/ModificacionCentroFaenamiento/administrarDocumentoModificacionCentroFaenamiento.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO = "~/Solicitudes/ModificacionCentroFaenamiento/administrarSolicitudModificacionCentroFaenamiento.aspx";
         public static String URL_EVALUAR_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO = "~/Solicitudes/ModificacionCentroFaenamiento/evaluarDocumentoModificacionCentroFaenamiento.aspx?idRequerimiento=";
         public static String URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO = "~/Solicitudes/ModificacionCentroFaenamiento/ingresarDocumentoModificacionCentroFaenamiento.aspx";
         public static String URL_ERROR_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO = "~/Solicitudes/Registrar/errorGeneral.aspx";

         public static String URL_REFERENCIA_GLOBAL_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO = "~/Solicitudes/ModificacionCentroFaenamiento/generalModificacionCentroFaenamiento.aspx";
         public static String URL_ANTECEDENTES_SECTOR_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO = "~/Solicitudes/ModificacionCentroFaenamiento/antecedDelSectorModificacionCentroFaenamiento.aspx";

         public static String solicitudModificacionCentroFaenamientoSession = "SolicitudModificacionCentroFaenamiento";


         //Solicitudes de Modificación de Amerb
         public static String URL_VER_SOLICITUD_MODIFICACION_AMERB = "~/Solicitudes/ModificacionAmerb/verDocumentoModificacionAmerb.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_AMERB = "~/Solicitudes/ModificacionAmerb/administrarDocumentoModificacionAmerb.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_AMERB = "~/Solicitudes/ModificacionAmerb/administrarSolicitudModificacionAmerb.aspx";
         public static String URL_EVALUAR_SOLICITUD_MODIFICACION_AMERB = "~/Solicitudes/ModificacionAmerb/evaluarDocumentoModificacionAmerb.aspx?idRequerimiento=";
         public static String URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_AMERB = "~/Solicitudes/ModificacionAmerb/ingresarDocumentoModificacionAmerb.aspx";
         public static String URL_ERROR_SOLICITUD_MODIFICACION_AMERB = "~/Solicitudes/Registrar/errorGeneral.aspx";

         public static String URL_REFERENCIA_GLOBAL_SOLICITUD_MODIFICACION_AMERB = "~/Solicitudes/ModificacionAmerb/generalModificacionAmerb.aspx";
         public static String URL_ANTECEDENTES_SECTOR_SOLICITUD_MODIFICACION_AMERB = "~/Solicitudes/ModificacionAmerb/antecedDelSectorModificacionAmerb.aspx";

         public static String solicitudModificacionAmerbSession = "SolicitudModificacionAmerb";


         //Solicitudes de Modificación de Centro de Acopio
         public static String URL_VER_SOLICITUD_MODIFICACION_CENTRO_ACOPIO = "~/Solicitudes/ModificacionCentroAcopio/verDocumentoModificacionCentroAcopio.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_ACOPIO = "~/Solicitudes/ModificacionCentroAcopio/administrarDocumentoModificacionCentroAcopio.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_CENTRO_ACOPIO = "~/Solicitudes/ModificacionCentroAcopio/administrarSolicitudModificacionCentroAcopio.aspx";
         public static String URL_EVALUAR_SOLICITUD_MODIFICACION_CENTRO_ACOPIO = "~/Solicitudes/ModificacionCentroAcopio/evaluarDocumentoModificacionCentroAcopio.aspx?idRequerimiento=";
         public static String URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_ACOPIO = "~/Solicitudes/ModificacionCentroAcopio/ingresarDocumentoModificacionCentroAcopio.aspx";
         public static String URL_ERROR_SOLICITUD_MODIFICACION_CENTRO_ACOPIO = "~/Solicitudes/Registrar/errorGeneral.aspx";

         public static String URL_REFERENCIA_GLOBAL_SOLICITUD_MODIFICACION_CENTRO_ACOPIO = "~/Solicitudes/ModificacionCentroAcopio/generalModificacionCentroAcopio.aspx";
         public static String URL_ANTECEDENTES_SECTOR_SOLICITUD_MODIFICACION_CENTRO_ACOPIO = "~/Solicitudes/ModificacionCentroAcopio/antecedDelSectorModificacionCentroAcopio.aspx";

         public static String solicitudModificacionCentroAcopioSession = "SolicitudModificacionCentroAcopio";


         //Solicitudes de Modificación de ECMPO
         public static String URL_VER_SOLICITUD_MODIFICACION_ECMPO = "~/Solicitudes/ModificacionECMPO/verDocumentoModificacionECMPO.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_ECMPO = "~/Solicitudes/ModificacionECMPO/administrarDocumentoModificacionECMPO.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_ECMPO = "~/Solicitudes/ModificacionECMPO/administrarSolicitudModificacionECMPO.aspx";
         public static String URL_EVALUAR_SOLICITUD_MODIFICACION_ECMPO = "~/Solicitudes/ModificacionECMPO/evaluarDocumentoModificacionECMPO.aspx?idRequerimiento=";
         public static String URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_ECMPO = "~/Solicitudes/ModificacionECMPO/ingresarDocumentoModificacionECMPO.aspx";
         public static String URL_ERROR_SOLICITUD_MODIFICACION_ECMPO = "~/Solicitudes/Registrar/errorGeneral.aspx";

         public static String URL_REFERENCIA_GLOBAL_SOLICITUD_MODIFICACION_ECMPO = "~/Solicitudes/ModificacionECMPO/generalModificacionECMPO.aspx";
         public static String URL_ANTECEDENTES_SECTOR_SOLICITUD_MODIFICACION_ECMPO = "~/Solicitudes/ModificacionECMPO/antecedDelSectorModificacionECMPO.aspx";

         public static String solicitudModificacionECMPOSession = "SolicitudModificacionECMPO";


         //Páginas de las Unidades Espaciales Creadas

         //Centro de Acopio
         public static String URL_ADMINISTRAR_CENTRO_ACOPIO = "~/Unidades/Acopio/administrarCentroAcopio.aspx";
         public static String URL_RESUMEN_CENTRO_ACOPIO = "~/Unidades/Acopio/resumenAcopio.aspx";

         //Centro en Amerb
         public static String URL_ADMINISTRAR_CENTRO_EN_AMERB = "~/Unidades/Amerb/administrarCentroAmerb.aspx";
         public static String URL_RESUMEN_CENTRO_EN_AMERB = "~/Unidades/Amerb/resumenAmerb.aspx";

        //Colector de Semillas
         public static String URL_ADMINISTRAR_COLECTOR_DE_SEMILLAS = "~/Unidades/Colector/administrarColectorSemillas.aspx";
         public static String URL_RESUMEN_COLECTOR_DE_SEMILLAS = "~/Unidades/Colector/resumenColector.aspx";

         //Concesiones de Acuicultura
         public static String URL_ADMINISTRAR_CONCESION_DE_ACUICULTURA = "~/Unidades/Concesion/administrarConcesiones.aspx";
         public static String URL_RESUMEN_CONCESION_DE_ACUICULTURA = "~/Unidades/Concesion/resumenConcesion.aspx";

         //Centro de Faenamiento
         public static String URL_ADMINISTRAR_CENTRO_DE_FAENAMIENTO = "~/Unidades/Faenamiento/administrarCentroFaenamiento.aspx";
         public static String URL_RESUMEN_CENTRO_DE_FAENAMIENTO = "~/Unidades/Faenamiento/resumenFaenamiento.aspx";

        //ECMPO
         public static String URL_ADMINISTRAR_ECMPO = "~/Unidades/ECMPO/administrarCentroECMPO.aspx";
         public static String URL_RESUMEN_ECMPO = "~/Unidades/ECMPO/resumenECMPO.aspx";

        //Experimentales Amerb
         public static String URL_ADMINISTRAR_EXPERIMENTALES_AMERB = "~/Unidades/ExperimentalesAmerb/administrarCentroExperimentalesAmerb.aspx";
         public static String URL_RESUMEN_EXPERIMENTALES_AMERB = "~/Unidades/ExperimentalesAmerb/resumenExperimentalesAmerb.aspx";

        //Experimentales Concesión
         public static String URL_ADMINISTRAR_EXPERIMENTALES_CONCESION = "~/Unidades/ExperimentalesConcesion/administrarCentroExperimentalesConcesion.aspx";
         public static String URL_RESUMEN_EXPERIMENTALES_CONCESION = "~/Unidades/ExperimentalesConcesion/resumenExperimentalesConcesion.aspx";


        //RESOLUCIONES
         public static String URL_VER_RESOLUCION = "~/Resoluciones/ingresarResoluciones.aspx?idResolucion=";




         //Solicitudes de Relocalizacion ERESA
         public static String URL_VER_RELOCALIZACION_RESA = "~/Solicitudes/RelocalizacionRESA/verDocumentoRelocalizacionRESA.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_DOCUMENTO_RELOCALIZACION_RESA = "~/Solicitudes/RelocalizacionRESA/administrarDocumentoRelocalizacionRESA.aspx?idRequerimiento=";
         public static String URL_ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA = "~/Solicitudes/RelocalizacionRESA/administrarSolicitudRelocalizacionRESA.aspx";
         public static String URL_EVALUAR_RELOCALIZACION_RESA = "~/Solicitudes/RelocalizacionRESA/evaluarDocumentoRelocalizacionRESA.aspx?idRequerimiento=";
         public static String URL_INGRESO_DOCUMENTO_RELOCALIZACION_RESA = "~/Solicitudes/RelocalizacionRESA/ingresarDocumentoRelocalizacionRESA.aspx";
         public static String URL_ERROR_RELOCALIZACION_RESA = "~/Solicitudes/Registrar/errorGeneral.aspx";

         public static String URL_REFERENCIA_GLOBAL_RELOCALIZACION_RESA = "~/Solicitudes/RelocalizacionRESA/generalRelocalizacionRESA.aspx";
         public static String URL_ANTECEDENTES_SECTOR_RELOCALIZACION_RESA = "~/Solicitudes/RelocalizacionRESA/antecedDelSectorRelocalizacionRESA.aspx";

         public static String solicitudRelocalizacionSessionRESA = "SolicitudRelocalizacionRESA";
    }
}
