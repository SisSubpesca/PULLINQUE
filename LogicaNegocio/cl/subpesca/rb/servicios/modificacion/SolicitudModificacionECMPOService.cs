using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using LogicaNegocio.cl.subpesca.rb.errores;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Entidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.concesiones;
using LogicaNegocio.cl.subpesca.rb.modificacion;

namespace LogicaNegocio.cl.subpesca.rb.servicios.modificacion
{
    public class SolicitudModificacionECMPOService
    {

        Logger logger = new Logger();
        SolicitudDA solicitudDA = new SolicitudDA();
        SolicitanteDA solicitanteDA = new SolicitanteDA();
        ProyectoTecnicoDA proyectoTecnicoDA = new ProyectoTecnicoDA();
        CoordenadaGeograficaDA coordenadaGeograficaDA = new CoordenadaGeograficaDA();
        UnidadEspacialDA unidadEspacialDA = new UnidadEspacialDA();
        AlertaTramiteModConcesionDA alertaTramiteModConcesionDA = new AlertaTramiteModConcesionDA();

        public bool guardarSolicitudModificacionECMPOInicial(Datos.Entidades.SolicitudConcesion solicitudModificacion, int idUsuario)
        {

            ConcesionService concesionService = new ConcesionService();

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    //Guarda Solicitud de Modificación de Concesión
                    if (!solicitudDA.GuardarSolModificacionConcesInicial(solicitudModificacion))
                    {
                        return false;
                    }

                    //Guarda Tipo de Modificación de Concesión
                    foreach (ParametroGenerico tipoModificacion in solicitudModificacion.tipoModificacionesTram)
                    {
                        if (!solicitudDA.GuardarTipoModificacionSolicitud(solicitudModificacion.idSolConcesion, tipoModificacion.id))
                        {
                            return false;
                        }
                    }

                    //Guarda Datos Adicionales de la Modificación de la Concesión
                    TramiteModConcesion tramiteModConcesion = solicitudModificacion.tramiteModConcesion;
                    tramiteModConcesion.idSolConcesion = solicitudModificacion.idSolConcesion;
                    if (!solicitudDA.GuardarTramiteModConcesion(tramiteModConcesion))
                    {
                        return false;
                    }

                    //SolicitudConcesion solicitudConcesion = concesionService.ObtieneConcesionExistente(solicitudModificacion.tramiteModConcesion.centro.id, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO);

                    SolicitudConcesion solicitudConcesion = solicitudDA.Obtiene_UE_Existente(Convert.ToString(solicitudModificacion.tramiteModConcesion.centro.id), rbTipo.UNID_ESPACIAL_ECMPO);

                    //Guarda una copia de los Solicitantes a la solicitud de modificación.
                    if (solicitudConcesion != null && solicitudConcesion.idSolConcesion > 0)
                    {
                        List<Solicitante> solicitanteList = solicitanteDA.listarSolicitante(solicitudConcesion.idSolConcesion, 0, "", rbEstadosGenerales.VIGENTE);
                        if (solicitanteList != null)
                        {
                            foreach (Solicitante solicitante in solicitanteList)
                            {
                                solicitante.idPersonasLeg = 0;
                                solicitante.solicitud = new SolicitudConcesion();
                                solicitante.solicitud.idSolConcesion = solicitudModificacion.idSolConcesion;
                                if (!solicitanteDA.GuardarSolicitante(solicitante, idUsuario))
                                {
                                    return false;
                                }
                            }
                        }


                        //Guarda una Copia de PT a la modificación.
                        if (!proyectoTecnicoDA.GuardarProyectoTecnico(solicitudModificacion.idSolConcesion, solicitudConcesion.idSolConcesion))
                        {
                            return false;
                        }

                        //Guarda una copia de Ant. del Sector a la modificación     
                        if (!coordenadaGeograficaDA.GuardarCoordenadaGeograficaTramiteMod(solicitudModificacion.idSolConcesion, solicitudConcesion.idSolConcesion))
                        {
                            return false;
                        }


                        //Guarda Unidad Espacial Temporal al momento de crear la ficha de la solicitud
                        UnidadEspacial unidadEspacial = new UnidadEspacial();
                        unidadEspacial.idSolicitud = solicitudModificacion.idSolConcesion;
                        unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                        unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToString(solicitudModificacion.tramiteModConcesion.centro.id);

                        //30-03-2016 Se agregan los siguientes campos a la unidad espacial temporal, pertenecientes a la unidad espacial original.
                        UnidadEspacial unidadEspacialOriginal = unidadEspacialDA.ObtieneUnidadEspacial(solicitudConcesion.idSolConcesion, 0);

                        if (unidadEspacialOriginal != null && unidadEspacialOriginal.idUnidadEspacial > 0)
                        {
                            unidadEspacial.capitaniaDePuerto = unidadEspacialOriginal.capitaniaDePuerto;
                            unidadEspacial.fechaActaEntrega = unidadEspacialOriginal.fechaActaEntrega;
                            unidadEspacial.fechaDiarioOficial = unidadEspacialOriginal.fechaDiarioOficial;
                            unidadEspacial.numeroActaEntrega = unidadEspacialOriginal.numeroActaEntrega;
                            unidadEspacial.numeroDiarioOficial = unidadEspacialOriginal.numeroDiarioOficial;
                            unidadEspacial.plazoInicio = unidadEspacialOriginal.plazoInicio;
                            unidadEspacial.tipoPlazoNominal = unidadEspacialOriginal.tipoPlazoNominal;
                            unidadEspacial.plazoVencimiento = unidadEspacialOriginal.plazoVencimiento;

                            if (!unidadEspacialDA.GuardarUnidadEspacialModificacion(unidadEspacial, idUsuario))
                            {
                                return false;
                            }
                        }
                    }

                    //Borra los erores existentes en la solicitud de modificación
                    if (!this.alertaTramiteModConcesionDA.EliminarAlertaTramiteModConcesion(0, solicitudModificacion.idSolConcesion))
                    {
                        return false;
                    }

                    //Vertifica Alertas si estas son generadas.
                    List<AlertaTramiteModConcesion> alertas = this.verificarAlertasModificacion(solicitudModificacion);

                    //Guarda Alertas si estas son generadas.
                    if (alertas != null)
                    {
                        if (!this.GuardarAlertasModificacion(alertas))
                        {
                            return false;
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


        private bool GuardarAlertasModificacion(List<AlertaTramiteModConcesion> alertas)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {

                    if (alertas != null)
                    {
                        foreach (AlertaTramiteModConcesion alertaTramiteModConcesion in alertas)
                        {
                            if (!alertaTramiteModConcesionDA.GuardarAlertaTramiteModConcesion(alertaTramiteModConcesion))
                            {
                                return false;
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



        private List<AlertaTramiteModConcesion> verificarAlertasModificacion(SolicitudConcesion solicitudModificacion)
        {
            List<AlertaTramiteModConcesion> alertaTramiteModConcesionList = new List<AlertaTramiteModConcesion>();
            AlertaTramiteModConcesion alertaTramiteModConcesion = null;

            try
            {
                /* El código de centro debe existir en el sistema */
                if (solicitudModificacion.tramiteModConcesion != null && solicitudModificacion.tramiteModConcesion.centro != null && solicitudModificacion.tramiteModConcesion.centro.id > 0)
                {

                    SolicitudConcesion solicitudConcesion = solicitudDA.VerificaUnidadEspacialTitularVigente(rbTipo.UNID_ESPACIAL_ECMPO, Convert.ToString(solicitudModificacion.tramiteModConcesion.centro.id), 0);

                    if (solicitudConcesion == null)
                    {
                        alertaTramiteModConcesion = new AlertaTramiteModConcesion();
                        alertaTramiteModConcesion.idSolConcesion = solicitudModificacion.idSolConcesion;
                        alertaTramiteModConcesion.tipoWarning = new ParametroGenerico(rbTipo.CENTRO_NO_EXISTE);

                        alertaTramiteModConcesionList.Add(alertaTramiteModConcesion);
                    }
                }

                /* El rut del titular debe estar asociado al centro */
                if (solicitudModificacion.tramiteModConcesion != null && solicitudModificacion.tramiteModConcesion.titular != null && solicitudModificacion.tramiteModConcesion.titular.rutPersona > 0)
                {
                    SolicitudConcesion solicitudConcesion = solicitudDA.VerificaUnidadEspacialTitularVigente(rbTipo.UNID_ESPACIAL_ECMPO, Convert.ToString(solicitudModificacion.tramiteModConcesion.centro.id), Convert.ToInt32(solicitudModificacion.tramiteModConcesion.titular.rutPersona));

                    if (solicitudConcesion == null)
                    {
                        alertaTramiteModConcesion = new AlertaTramiteModConcesion();
                        alertaTramiteModConcesion.idSolConcesion = solicitudModificacion.idSolConcesion;
                        alertaTramiteModConcesion.tipoWarning = new ParametroGenerico(rbTipo.MANEJO_DE_TITULARES);

                        alertaTramiteModConcesionList.Add(alertaTramiteModConcesion);
                    }
                }

                /* La superficie total final debe ser igual a la superficie total */
                solicitudModificacion = solicitudDA.ObtieneSolicitudConcesionMod(solicitudModificacion.idSolConcesion, 0);

                if (Convert.ToSingle(solicitudModificacion.superficieCalculada) != Convert.ToSingle(solicitudModificacion.superficieTramFinal))
                {
                    alertaTramiteModConcesion = new AlertaTramiteModConcesion();
                    alertaTramiteModConcesion.idSolConcesion = solicitudModificacion.idSolConcesion;
                    alertaTramiteModConcesion.tipoWarning = new ParametroGenerico(rbTipo.DIFERENCIA_DE_HECTAREAS);

                    alertaTramiteModConcesionList.Add(alertaTramiteModConcesion);
                }


                return alertaTramiteModConcesionList;
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
