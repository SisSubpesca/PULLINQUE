using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Transactions;
using Datos.Entidades;
using Datos.Contantes;


namespace LogicaNegocio.cl.subpesca.rb.servicios.solicitudes
{
    public class SolicitudCentroColectorService
    {

        Logger logger = new Logger();
        SolicitudDA solicitudDA = new SolicitudDA();
        GrupoSuspendidoDA grupoSuspendidoDA = new GrupoSuspendidoDA();

        public bool guardarSolicitudColectorSemilla(SolicitudConcesion solicitudInicial, int idUsuario)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    //GUARDAR SOLICITUD
                    solicitudInicial.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
                    if (!solicitudDA.GuardarSolicitudColectoresSemilla(solicitudInicial))
                    {
                        return false;
                    }


                    //GUARDAR DATA ADICIONAL DE LA UNIDAD ESPACIAL
                    solicitudInicial.datosSolicitudUE.idSolConcesion = solicitudInicial.idSolConcesion;
                    if (!solicitudDA.GuardarDatosSolicitudUE(solicitudInicial.datosSolicitudUE, idUsuario))
                    {
                        return false;
                    }

                    transactionScope.Complete();
                    return true;

                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }

        }

        public bool guardarEvaluacionOrdenamientoTerr(EvaluacionUOT_UE evaluacionUOT_UE)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {

                    if (!grupoSuspendidoDA.GuardarEvaluacionUOT_UE(evaluacionUOT_UE))
                    {
                        return false;
                    }

                    /* Se eliminan las relaciones con supeditados si existen anteriormente para ser insert desde 0 */
                    List<DependenciaSupeditados> listDependenciaSupeditadosSolicitud = grupoSuspendidoDA.ListarDependenciaSupeditadosFiltro(0, 0, evaluacionUOT_UE.idEvaluacionUOT, 0);

                    if (listDependenciaSupeditadosSolicitud != null && listDependenciaSupeditadosSolicitud.Count > 0)
                    {
                        foreach (DependenciaSupeditados dependenciaSupeditadosAux in listDependenciaSupeditadosSolicitud)
                        {
                            if (!grupoSuspendidoDA.EliminarDependenciaSupeditados(dependenciaSupeditadosAux.idDepSupeditado, 0, evaluacionUOT_UE.idEvaluacionUOT, 0))
                            {
                                return false;
                            }
                        }
                    }


                    /* Sólo si aplica, se debe almacenar tipos de supeditados asociados */
                    if (evaluacionUOT_UE.dependenciaSupeditadosList != null && evaluacionUOT_UE.dependenciaSupeditadosList.Count > 0)
                    {
                        foreach (DependenciaSupeditados dependenciaSupeditado in evaluacionUOT_UE.dependenciaSupeditadosList)
                        {
                            dependenciaSupeditado.evaluacionUOT_UE = evaluacionUOT_UE;

                            //dependenciaSupeditado.solicitudConcesionDep = evaluacionUOT_UE.solicitudConcesion;

                            int idSolicitud = solicitudDA.obtieneSolicitudIdPert(dependenciaSupeditado.solicitudConcesionDep.numPert);
                            dependenciaSupeditado.solicitudConcesionDep.idSolConcesion = idSolicitud;

                            if (dependenciaSupeditado.accion == accion.INGRESAR || dependenciaSupeditado.accion == accion.LISTADO)
                            {
                                dependenciaSupeditado.idDepSupeditado = 0;

                                if (!grupoSuspendidoDA.GuardarDependenciaSupeditados(dependenciaSupeditado))
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    /* Se eliminan las relaciones con supeditados si existen anteriormente para ser insert desde 0 (En este caso cambian de vigencia) */
                    List<AsocGrupoSolicitud> listAsocGrupoSolicitud = grupoSuspendidoDA.ListarAsocGrupoSolicitudFiltro(0, evaluacionUOT_UE.solicitudConcesion.idSolConcesion, 0, 0, evaluacionUOT_UE.idEvaluacionUOT);

                    if (listAsocGrupoSolicitud != null && listAsocGrupoSolicitud.Count > 0)
                    {
                        foreach (AsocGrupoSolicitud asocGrupoSolicitudAux in listAsocGrupoSolicitud)
                        {
                            asocGrupoSolicitudAux.evaluacionUOT_UE = evaluacionUOT_UE;
                            asocGrupoSolicitudAux.solicitudConcesion = evaluacionUOT_UE.solicitudConcesion;
                            asocGrupoSolicitudAux.solicitudConcesion.idSolConcesion = evaluacionUOT_UE.solicitudConcesion.idConcesion;

                            if (!grupoSuspendidoDA.ActualizaAsocGrupoSolicitudVigencia(asocGrupoSolicitudAux.idAsocGrupoSolicitud, 0, 0, evaluacionUOT_UE.idEvaluacionUOT, rbEstadosGenerales.NO_VIGENTE, 0))
                            {
                                return false;
                            }
                        }
                    }

                    /* Sólo si aplica, se debe almacenar grupos suspendidos asociados */
                    if (evaluacionUOT_UE.asocGrupoSolicitudList != null && evaluacionUOT_UE.asocGrupoSolicitudList.Count > 0)
                    {
                        foreach (AsocGrupoSolicitud asocGrupoSolicitud in evaluacionUOT_UE.asocGrupoSolicitudList)
                        {
                            if (asocGrupoSolicitud.accion == accion.INGRESAR || asocGrupoSolicitud.accion == accion.LISTADO)
                            {
                                asocGrupoSolicitud.idAsocGrupoSolicitud = 0;
                                asocGrupoSolicitud.evaluacionUOT_UE = evaluacionUOT_UE;
                                asocGrupoSolicitud.solicitudConcesion = evaluacionUOT_UE.solicitudConcesion;
                                asocGrupoSolicitud.solicitudConcesion.idSolConcesion = evaluacionUOT_UE.solicitudConcesion.idConcesion;

                                if (!grupoSuspendidoDA.GuardarAsocGrupoSolicitud(asocGrupoSolicitud))
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    transactionScope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }
    }
}
