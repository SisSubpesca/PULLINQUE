using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using System.Data;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.reportes;

namespace LogicaNegocio.cl.subpesca.rb.servicios.reportes
{
    public class ReporteService
    {

        Logger logger = new Logger();
        Reportes reportes = new Reportes();
        ReporteDA reporteDA = new ReporteDA();

        public DataTable Reportes_Generar_RB(int id_tiporeporte, string KeySort, int id_region, int id_provincia, int id_comuna, int id_macrozona, int id_barrio,
                                       int id_etapacultivo, int id_centrocultivo, string pert, int id_vigencia, string especies, int anio1, int mes1, int anio2,
                                       int mes2, int id_usuario, int conSinBarrio, int idTipoUnidEspacial, int idTipoTramite, int id_tipoRelocalizacion, int id_tipoModificacion)
        {

             try
            {

                String procedimiento = "";

                if (id_tiporeporte == Datos.Contantes.tipoReporte.REPORTE_DE_CENTROS_DE_CULTIVO)
                {
                    procedimiento = "paSelRbReporte06";
                }
                else if (id_tiporeporte == Datos.Contantes.tipoReporte.ANALISIS_DE_VIGENCIA)
                {
                    procedimiento = "paSelRbReporte07";
                }
                else if (id_tiporeporte == Datos.Contantes.tipoReporte.REPORTE_DE_CONCESIONES)
                {
                    procedimiento = "paSelRbReporte08";
                }
                else if (id_tiporeporte == Datos.Contantes.tipoReporte.REPORTE_DE_SOLICITUDES)
                {
                    procedimiento = "paSelRbReporte10";
                }

                    
                 return reportes.Reportes_Generar_RB(id_tiporeporte, KeySort, id_region, id_provincia, id_comuna, id_macrozona, id_barrio,
                                               id_etapacultivo, id_centrocultivo, pert, id_vigencia, especies, anio1, mes1, anio2,
                                               mes2, id_usuario, conSinBarrio, procedimiento, idTipoUnidEspacial, idTipoTramite, id_tipoRelocalizacion, id_tipoModificacion);



            }
            catch (Exception ex) {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }

        }


        public DataTable Reportes_Resumen_Generar_RB(int id_tiporeporte, int id_centrocultivo, int id_usuario, int idTipoUnidEspacial, int idTipoTramite)
        { 
        
            try
            {
                return reportes.Reportes_Resumen_GenerarRB(id_tiporeporte, id_centrocultivo, id_usuario, idTipoUnidEspacial, idTipoTramite);
            }
            catch (Exception ex)
            {
                  logger.PrintError(ex);
                  logger.SendMailError(ex);
                  return null;
            }
        
        }





        //NUEVOS REPORTES



        public DataTable GenerarReporte(int idTipoReporte, int idTipoUE, int idTipoTramite, int idTipoRelocalizacion, int idTipoModificacion, int idRegion, int idProvincia, int idComuna, int macrozona, int barrio, string especies, int idGrupoEspecie, int idEstado, string numPert, string codigoCentro, DateTime fechaInicio, DateTime fechaFin)
        {

            try
            {

                if (idTipoReporte == TipoReportes.SOLICITUD_EN_TRAMITE_CON_DOCUMENTACION_01) 
                {
                    return reporteDA.ReporteTramiteDocumentacion01(idTipoTramite, idTipoRelocalizacion, idTipoModificacion, idRegion, idProvincia, idComuna, macrozona, barrio, especies, idGrupoEspecie, idEstado, numPert, codigoCentro);
                }

                if (idTipoReporte == TipoReportes.SOLICITUD_EN_TRAMITE_CON_DOCUMENTACION_02)
                {
                    return reporteDA.ReporteTramiteDocumentacion02(idTipoTramite, idTipoRelocalizacion, idTipoModificacion, idRegion, idProvincia, idComuna, macrozona, barrio, especies, idGrupoEspecie, idEstado, numPert, codigoCentro);
                }

                if (idTipoReporte == TipoReportes.SOLICITUD_EN_TRAMITE_CON_DOCUMENTACION_03)
                {
                    return reporteDA.ReporteTramiteDocumentacion03(idTipoTramite, idTipoRelocalizacion, idTipoModificacion, idRegion, idProvincia, idComuna, macrozona, barrio, especies, idGrupoEspecie, idEstado, numPert, codigoCentro);
                }

                if (idTipoReporte == TipoReportes.SOLICITUD_EN_TRAMITE_CON_COORDENADAS)
                {
                    return reporteDA.ReporteTramiteCoordenadas(idTipoTramite, idTipoRelocalizacion, idTipoModificacion, idRegion, idProvincia, idComuna, macrozona, barrio, especies, idGrupoEspecie, idEstado, numPert, codigoCentro);
                }

                if (idTipoReporte == TipoReportes.REPORTES_UNIDADES_ESPACIALES_COORDENADAS)
                {
                    return reporteDA.ReporteUECoordenadas(idTipoUE, idTipoRelocalizacion, idTipoModificacion, idRegion, idProvincia, idComuna, macrozona, barrio, especies, idGrupoEspecie, idEstado, numPert, codigoCentro);
                }

                if (idTipoReporte == TipoReportes.REPORTES_UNIDADES_ESPACIALES_DOCUMENTOS)
                {
                    return reporteDA.ReporteUEDocumentos(idTipoUE, idTipoRelocalizacion, idTipoModificacion, idRegion, idProvincia, idComuna, macrozona, barrio, especies, idGrupoEspecie, idEstado, numPert, codigoCentro);
                }

                if (idTipoReporte == TipoReportes.REPORTE_UNIDADES_ESPACIALES_ANALISIS_DE_VIGENCIA)
                {
                    return reporteDA.ReporteUEAnalisisVigencia(idTipoUE, idTipoRelocalizacion, idTipoModificacion, idRegion, idProvincia, idComuna, macrozona, barrio, especies, idGrupoEspecie, idEstado, numPert, codigoCentro, fechaInicio, fechaFin);
                }

                if (idTipoReporte == TipoReportes.REPORTE_UNIDADES_ESPACIALES_CENTROS_DE_CULTIVO)
                {
                    return reporteDA.ReporteUECentroCultivo(idTipoUE, idTipoRelocalizacion, idTipoModificacion, idRegion, idProvincia, idComuna, macrozona, barrio, especies, idGrupoEspecie, idEstado, numPert, codigoCentro);
                }

                if (idTipoReporte == TipoReportes.SOLICITUD_EN_TRAMITE_SSP_SSFFA)
                {
                    return reporteDA.ReporteTramiteSSP_SSFFAA(idTipoTramite, idTipoRelocalizacion, idTipoModificacion, idRegion, idProvincia, idComuna, macrozona, barrio, especies, idGrupoEspecie, idEstado, numPert, codigoCentro);
                }


                if (idTipoReporte == TipoReportes.REPORTE_UNIDADES_ESPACIALES_NO_VIGENTES_RESOLUCIONES)
                {
                    return reporteDA.ReporteUENoVigentesResoluciones(idTipoUE, idRegion, idProvincia, idComuna, macrozona, barrio, especies, idGrupoEspecie, codigoCentro);
                }

                if (idTipoReporte == TipoReportes.REPORTE_UNIDADES_ESPACIALES_NO_VIGENTES_TRAMITES)
                {
                    return reporteDA.ReporteUENOVigentesTramites(idTipoUE, idRegion, idProvincia, idComuna, macrozona, barrio, especies, idGrupoEspecie, codigoCentro);
                }

                return null;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        
        }


        /**
         *  OBTIENE LISTA DE ESTADOS POR TIPO DE TRAMITE
         */ 
        public List<ParametroGenerico> ListarEstadosPorTipoTramite(int idTipoTramite) {

            try
            {

                return reporteDA.ListarEstadosPorTipoTramite(idTipoTramite);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


    }
}
