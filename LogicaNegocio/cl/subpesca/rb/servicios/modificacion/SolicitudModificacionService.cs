using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using LogicaNegocio.cl.subpesca.rb.errores;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Contantes;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.concesiones;
using LogicaNegocio.cl.subpesca.rb.modificacion;

namespace LogicaNegocio.cl.subpesca.rb.servicios.modificacion
{
    public class SolicitudModificacionService
    {
        Logger logger = new Logger();
        SolicitudDA solicitudDA = new SolicitudDA();
        UnidadEspacialDA unidadEspacialDA = new UnidadEspacialDA();
        ProyectoTecnicoDA proyectoTecnicoDA = new ProyectoTecnicoDA();
        CoordenadaGeograficaDA coordenadaGeograficaDA = new CoordenadaGeograficaDA();
        AntecedenteModConcesionDA antecedenteModConcesionDA = new AntecedenteModConcesionDA();
        AlertaTramiteModConcesionDA alertaTramiteModConcesionDA = new AlertaTramiteModConcesionDA();
        SolicitanteDA solicitanteDA = new SolicitanteDA();
        UnidadDependenciaModDA unidadDependenciaModDA = new UnidadDependenciaModDA();

        /**
         * GUARDA UNA SOLICITUD DE MODIFACION DE UNA CONCESION DE ACUICULTURA 
        */
        public bool guardarSolicitudModificacionInicial(Datos.Entidades.SolicitudConcesion solicitudModificacion, int idUsuario)
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

                    SolicitudConcesion solicitudConcesion = concesionService.ObtieneConcesionExistente(Convert.ToString(solicitudModificacion.tramiteModConcesion.centro.id), rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);

                    if (solicitudConcesion != null && solicitudConcesion.idSolConcesion > 0)
                    {
                        //Guarda una copia de los Solicitantes a la solicitud de modificación.
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

                        UnidadEspacial unidadEspacial = unidadEspacialDA.ObtieneUnidadEspacial(solicitudConcesion.idSolConcesion,0);
                        unidadEspacial.idUnidadEspacial = 0;
                        unidadEspacial.idSolicitud = solicitudModificacion.idSolConcesion;
                        unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                        unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToString(solicitudModificacion.tramiteModConcesion.centro.id);

                        if (!unidadEspacialDA.GuardarUnidadEspacialModificacion(unidadEspacial, idUsuario))
                        {
                            return false;
                        }
                    }

                    //Guarda Unidad Espacial Temporal al momento de crear la ficha de la solicitud
                    /*
                    UnidadEspacial unidadEspacial = new UnidadEspacial();
                    unidadEspacial.idSolicitud = solicitudModificacion.idSolConcesion;
                    unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                    unidadEspacial.centrosDeCultivo.codigoCentro = solicitudModificacion.tramiteModConcesion.centro.id;

                    if (!unidadEspacialDA.GuardarUnidadEspacialModificacion(unidadEspacial))
                    {
                        return false;
                    }
                     * */
                   

                    //Borra los erores existentes en la solicitud de modificación
                    if (!this.alertaTramiteModConcesionDA.EliminarAlertaTramiteModConcesion(0,solicitudModificacion.idSolConcesion))
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


        /**
        * GUARDA UNA SOLICITUD DE MODIFACION DE UNA ACUICULTURA EN AMERB
        */
        public bool guardarSolicitudModificacionAmerbInicial(Datos.Entidades.SolicitudConcesion solicitudModificacion, int idUsuario)
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


                    //GUARDAR DATA ADICIONAL DE LA UNIDAD ESPACIAL
                    solicitudModificacion.datosSolicitudUE.idSolConcesion = solicitudModificacion.idSolConcesion;
                    if (!solicitudDA.GuardarDatosSolicitudUE(solicitudModificacion.datosSolicitudUE, idUsuario))
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

                    //SolicitudConcesion solicitudConcesion = concesionService.ObtieneConcesionExistente(solicitudModificacion.tramiteModConcesion.centro.id, rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB);

                    SolicitudConcesion solicitudConcesion = concesionService.ObtieneUnidadEspacialExistente(Convert.ToString(solicitudModificacion.tramiteModConcesion.centro.id), rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB);

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

                        UnidadEspacial unidadEspacial = unidadEspacialDA.ObtieneUnidadEspacial(solicitudConcesion.idSolConcesion, 0);
                        unidadEspacial.idUnidadEspacial = 0;
                        unidadEspacial.idSolicitud = solicitudModificacion.idSolConcesion;
                        unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                        unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToString(solicitudModificacion.tramiteModConcesion.centro.id);

                        if (!unidadEspacialDA.GuardarUnidadEspacialModificacion(unidadEspacial, idUsuario))
                        {
                            return false;
                        }
                    }

                    /*
                    //Guarda Unidad Espacial Temporal al momento de crear la ficha de la solicitud
                    UnidadEspacial unidadEspacial = new UnidadEspacial();
                    unidadEspacial.idSolicitud = solicitudModificacion.idSolConcesion;
                    unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                    unidadEspacial.centrosDeCultivo.codigoCentro = solicitudModificacion.tramiteModConcesion.centro.id;

                    if (!unidadEspacialDA.GuardarUnidadEspacialModificacion(unidadEspacial))
                    {
                        return false;
                    }
                     * */

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


        /**
        * GUARDA UNA SOLICITUD DE MODIFACION DE UN CENTRO DE ACOPIO
        */
        public bool guardarSolicitudModificacionAcopioInicial(Datos.Entidades.SolicitudConcesion solicitudModificacion, int idUsuario)
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

                    SolicitudConcesion solicitudConcesion = concesionService.ObtieneUnidadEspacialExistente(Convert.ToString(solicitudModificacion.tramiteModConcesion.centro.id), rbTipo.UNID_ESPACIAL_CENTRO_DE_ACOPIO);

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

                        UnidadEspacial unidadEspacial = unidadEspacialDA.ObtieneUnidadEspacial(solicitudConcesion.idSolConcesion, 0);
                        unidadEspacial.idUnidadEspacial = 0;
                        unidadEspacial.idSolicitud = solicitudModificacion.idSolConcesion;
                        unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                        unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToString(solicitudModificacion.tramiteModConcesion.centro.id);

                        if (!unidadEspacialDA.GuardarUnidadEspacialModificacion(unidadEspacial, idUsuario))
                        {
                            return false;
                        }
                    }

                    /*
                    //Guarda Unidad Espacial Temporal al momento de crear la ficha de la solicitud
                    UnidadEspacial unidadEspacial = new UnidadEspacial();
                    unidadEspacial.idSolicitud = solicitudModificacion.idSolConcesion;
                    unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                    unidadEspacial.centrosDeCultivo.codigoCentro = solicitudModificacion.tramiteModConcesion.centro.id;

                    if (!unidadEspacialDA.GuardarUnidadEspacialModificacion(unidadEspacial))
                    {
                        return false;
                    }
                     * **/

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


        /**
        * GUARDA UNA SOLICITUD DE MODIFACION DE UN CENTRO DE FAENAMIENTO 
        */
        public bool guardarSolicitudModificacionFaenamientoInicial(Datos.Entidades.SolicitudConcesion solicitudModificacion, int idUsuario)
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

                    SolicitudConcesion solicitudConcesion = concesionService.ObtieneUnidadEspacialExistente(Convert.ToString(solicitudModificacion.tramiteModConcesion.centro.id), rbTipo.UNID_ESPACIAL_CENTRO_DE_FAENAMIENTO);

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

                        UnidadEspacial unidadEspacial = unidadEspacialDA.ObtieneUnidadEspacial(solicitudConcesion.idSolConcesion, 0);
                        unidadEspacial.idUnidadEspacial = 0;
                        unidadEspacial.idSolicitud = solicitudModificacion.idSolConcesion;
                        unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                        unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToString(solicitudModificacion.tramiteModConcesion.centro.id);

                        if (!unidadEspacialDA.GuardarUnidadEspacialModificacion(unidadEspacial, idUsuario))
                        {
                            return false;
                        }
                    }

                    /*
                    //Guarda Unidad Espacial Temporal al momento de crear la ficha de la solicitud
                    UnidadEspacial unidadEspacial = new UnidadEspacial();
                    unidadEspacial.idSolicitud = solicitudModificacion.idSolConcesion;
                    unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                    unidadEspacial.centrosDeCultivo.codigoCentro = solicitudModificacion.tramiteModConcesion.centro.id;

                    if (!unidadEspacialDA.GuardarUnidadEspacialModificacion(unidadEspacial))
                    {
                        return false;
                    }
                    **/

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

                    SolicitudConcesion solicitudConcesion = solicitudDA.VerificaUnidadEspacialTitularVigente(rbTipo.UNID_ESPACIAL_CONCESION, Convert.ToString(solicitudModificacion.tramiteModConcesion.centro.id), 0);

                    if (solicitudConcesion == null)
                    {
                        alertaTramiteModConcesion = new AlertaTramiteModConcesion();
                        alertaTramiteModConcesion.idSolConcesion = solicitudModificacion.idSolConcesion;
                        alertaTramiteModConcesion.tipoWarning = new ParametroGenerico(rbTipo.CENTRO_NO_EXISTE_MODIFICACION);

                        alertaTramiteModConcesionList.Add(alertaTramiteModConcesion);
                    }
                }

                /* El rut del titular debe estar asociado al centro */
                if (solicitudModificacion.tramiteModConcesion != null && solicitudModificacion.tramiteModConcesion.titular != null && solicitudModificacion.tramiteModConcesion.titular.rutPersona > 0)
                {
                    SolicitudConcesion solicitudConcesion = solicitudDA.VerificaUnidadEspacialTitularVigente(rbTipo.UNID_ESPACIAL_CONCESION, Convert.ToString(solicitudModificacion.tramiteModConcesion.centro.id), Convert.ToInt32(solicitudModificacion.tramiteModConcesion.titular.rutPersona));

                    if (solicitudConcesion == null)
                    {
                        alertaTramiteModConcesion = new AlertaTramiteModConcesion();
                        alertaTramiteModConcesion.idSolConcesion = solicitudModificacion.idSolConcesion;
                        alertaTramiteModConcesion.tipoWarning = new ParametroGenerico(rbTipo.MANEJO_DE_TITULARES_MODIFICACION);

                        alertaTramiteModConcesionList.Add(alertaTramiteModConcesion);
                    }
                }

                /* La superficie total final debe ser igual a la superficie total */
                solicitudModificacion = solicitudDA.ObtieneSolicitudConcesionMod(solicitudModificacion.idSolConcesion, 0);

                if (Convert.ToSingle(solicitudModificacion.superficieCalculada) != Convert.ToSingle(solicitudModificacion.superficieTramFinal))
                {
                    alertaTramiteModConcesion = new AlertaTramiteModConcesion();
                    alertaTramiteModConcesion.idSolConcesion = solicitudModificacion.idSolConcesion;
                    alertaTramiteModConcesion.tipoWarning = new ParametroGenerico(rbTipo.DIFERENCIA_DE_HECTAREAS_MODIFICACION);

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

        public bool guardarSolicitudModificacionConcesion(SolicitudConcesion solicitudModificacion, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    //GUARDAR SOLICITUD
                    if (!solicitudDA.GuardarSolicitud(solicitudModificacion, idUsuario))
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
        /**
         * ACTUALIZA SEA SI/NO PARA UNA DETERMINADA SOLICITUD
         */
        public bool ActualizaSolicitudSEA(int idSolicitud, int aplicaSEA, int idUsuario)
        {


            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    //GUARDAR SOLICITUD
                    if (!solicitudDA.ActualizaSolicitudSEA(idSolicitud, aplicaSEA, idUsuario))
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

        /**
         * Método que obtiene el listado de funcionalidades que deben estar disponibles
         * de acuerdo al tipo de modificacion que el usuario este realizando.
         */
        /*
        public List<AntecedenteModConcesion> obtieneDespliegueFuncionalidades(Datos.Entidades.SolicitudConcesion solicitudModificacion) {

            String listTipoModificacion = "(";
            int i = 1;

            foreach (ParametroGenerico tipoModificacion in solicitudModificacion.tipoModificacionesTram) {

                if (i < solicitudModificacion.tipoModificacionesTram.Count)
                {
                    listTipoModificacion = listTipoModificacion + tipoModificacion.id + ",";

                }
                else {
                    listTipoModificacion = listTipoModificacion + tipoModificacion.id;
                
                }
                i++;
            }

            listTipoModificacion = listTipoModificacion + ")";

            List<AntecedenteModConcesion> antecedenteModConcesionList = antecedenteModConcesionDA.ListarAntecedenteModConcesion(listTipoModificacion);
            
            return antecedenteModConcesionList;
        }
         * */

        /**
         * Método que obtiene el listado de requerimientos pendientes por codigo de centro
         * en una solicitud de modificacion de concesión de acuicultura.
         */
        public List<SolicitudConcesion> ListarPequerimientosPendCodCentro(int codigoCentro, int idSolConcesion)
        {
            try
            {
                List<SolicitudConcesion> solicitudConcesionList = solicitudDA.ListarSolicitudConcesionTramite(codigoCentro,idSolConcesion);
                
                return solicitudConcesionList;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public List<SolicitudConcesion> ListarRequerimientosPendModificacionCodCentro(int codigoCentro, int idSolConcesion, int idTipoTramite, int idTipoUnidEspacial)
        {
            try
            {
                List<SolicitudConcesion> solicitudConcesionList = solicitudDA.ListarSolicitud_UE_Tramite(codigoCentro, idSolConcesion, idTipoTramite, idTipoUnidEspacial);

                return solicitudConcesionList;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public bool despligueSeccionesMenu(SolicitudConcesion solicitudModificacion, List<DespliegueMenuSeccion> despligueMenuSeccionList, int idSeccion)
        {
            if (solicitudModificacion != null)
            {
                List<ParametroGenerico> tipoModificacionList = solicitudModificacion.tipoModificacionesTram;

                foreach (ParametroGenerico tipoModificacion in tipoModificacionList)
                {
                    foreach (DespliegueMenuSeccion despliegueMenuSeccion in despligueMenuSeccionList)
                    {
                        if ((tipoModificacion.id == rbTipo.MOD_CONCESION_PT || tipoModificacion.id == rbTipo.MOD_AMERB_PT || tipoModificacion.id == rbTipo.MOD_ECMPO_PT || tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_PT_ESPECIE || tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_PT_ESPECIE) && despliegueMenuSeccion.aplicaModPT && (idSeccion == 0 || despliegueMenuSeccion.idSeccion == idSeccion))
                        {
                            return true;
                        }
                        if ((tipoModificacion.id == rbTipo.MOD_CONCESION_ESPECIE || tipoModificacion.id == rbTipo.MOD_AMERB_ESPECIE || tipoModificacion.id == rbTipo.MOD_ECMPO_ESPECIE || tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_RENOVACION || tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_RENOVACION) && despliegueMenuSeccion.aplicaModEspecie && (idSeccion == 0 || despliegueMenuSeccion.idSeccion == idSeccion))
                        {
                            return true;
                        }
                        if ((tipoModificacion.id == rbTipo.MOD_CONCESION_REDUCE_SUPERFICIE || tipoModificacion.id == rbTipo.MOD_AMERB_REDUCE_SUPERFICIE || tipoModificacion.id == rbTipo.MOD_ECMPO_REDUCE_SUPERFICIE || tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE || tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE) && despliegueMenuSeccion.aplicaModReduccion && (idSeccion == 0 || despliegueMenuSeccion.idSeccion == idSeccion))
                        {
                            return true;
                        }
                        if ((tipoModificacion.id == rbTipo.MOD_CONCESION_AMPLIA_SUPERFICIE || tipoModificacion.id == rbTipo.MOD_AMERB_AMPLIA_SUPERFICIE || tipoModificacion.id == rbTipo.MOD_ECMPO_AMPLIA_SUPERFICIE || tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE || tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE) && despliegueMenuSeccion.aplicaModAmpliacion && (idSeccion == 0 || despliegueMenuSeccion.idSeccion == idSeccion))
                        {
                            return true;
                        }
                        if ((tipoModificacion.id == rbTipo.MOD_CONCESION_REGULARIZACION || tipoModificacion.id == rbTipo.MOD_AMERB_REGULARIZACION || tipoModificacion.id == rbTipo.MOD_ECMPO_REGULARIZACION || tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REGULARIZACION || tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REGULARIZACION) && despliegueMenuSeccion.aplicaModRegularizacion && (idSeccion == 0 || despliegueMenuSeccion.idSeccion == idSeccion))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool debeDesplegarPestania(List<ParametroGenerico> tipoModificacionList, List<DespliegueMenuSeccion> despligueMenuSeccionList, int idPestania)
        {
           
            foreach (ParametroGenerico tipoModificacion in tipoModificacionList){

                foreach (DespliegueMenuSeccion despliegueMenuSeccion in despligueMenuSeccionList)
                {

                    if (tipoModificacion.id == rbTipo.MOD_CONCESION_REGULARIZACION)
                    {
                        if (idPestania == rbTipo.ANTECEDENTES_ESPACIALES)
                        {
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ORGINAL && despliegueMenuSeccion.aplicaModRegularizacion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.ARCHIVOS_ADJUNTOS_COORD_ORGINAL && despliegueMenuSeccion.aplicaModRegularizacion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.POLIGONO_COORD_ORIGINAL && despliegueMenuSeccion.aplicaModRegularizacion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.VERTICE_COORD_ORIGINAL && despliegueMenuSeccion.aplicaModRegularizacion)
                            {
                                return true;
                            }
                        }

                        if (idPestania == rbTipo.ANTECEDENTES_TERRENO)
                        {

                            if (despliegueMenuSeccion.idSeccion == rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL && despliegueMenuSeccion.aplicaModRegularizacion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.ARCHIVOS_ADJUNTOS_COORD_ENTREGA_MATERIAL && despliegueMenuSeccion.aplicaModRegularizacion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.POLIGONO_COORD_ENTREGA_MATERIAL && despliegueMenuSeccion.aplicaModRegularizacion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.VERTICE_COORD_ENTREGA_MATERIAL && despliegueMenuSeccion.aplicaModRegularizacion)
                            {
                                return true;
                            }
                        }

                        if (idPestania == rbTipo.REGULARIZACION)
                        {

                            if (despliegueMenuSeccion.idSeccion == rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION && despliegueMenuSeccion.aplicaModRegularizacion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.ARCHIVOS_ADJUNTOS_COORD_REGULARIZACION && despliegueMenuSeccion.aplicaModRegularizacion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.POLIGONO_COORD_REGULARIZACION && despliegueMenuSeccion.aplicaModRegularizacion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.VERTICE_COORD_REGULARIZACION && despliegueMenuSeccion.aplicaModRegularizacion)
                            {
                                return true;
                            }
                        }

                    }
                    else if (tipoModificacion.id == rbTipo.MOD_CONCESION_AMPLIA_SUPERFICIE)
                    {
                        if (idPestania == rbTipo.ANTECEDENTES_ESPACIALES)
                        {
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ORGINAL && despliegueMenuSeccion.aplicaModAmpliacion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.ARCHIVOS_ADJUNTOS_COORD_ORGINAL && despliegueMenuSeccion.aplicaModAmpliacion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.POLIGONO_COORD_ORIGINAL && despliegueMenuSeccion.aplicaModAmpliacion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.VERTICE_COORD_ORIGINAL && despliegueMenuSeccion.aplicaModAmpliacion)
                            {
                                return true;
                            }
                        }

                        if (idPestania == rbTipo.ANTECEDENTES_TERRENO)
                        {

                            if (despliegueMenuSeccion.idSeccion == rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL && despliegueMenuSeccion.aplicaModAmpliacion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.ARCHIVOS_ADJUNTOS_COORD_ENTREGA_MATERIAL && despliegueMenuSeccion.aplicaModAmpliacion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.POLIGONO_COORD_ENTREGA_MATERIAL && despliegueMenuSeccion.aplicaModAmpliacion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.VERTICE_COORD_ENTREGA_MATERIAL && despliegueMenuSeccion.aplicaModAmpliacion)
                            {
                                return true;
                            }
                        }

                        if (idPestania == rbTipo.REGULARIZACION)
                        {

                            if (despliegueMenuSeccion.idSeccion == rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION && despliegueMenuSeccion.aplicaModAmpliacion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.ARCHIVOS_ADJUNTOS_COORD_REGULARIZACION && despliegueMenuSeccion.aplicaModAmpliacion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.POLIGONO_COORD_REGULARIZACION && despliegueMenuSeccion.aplicaModAmpliacion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.VERTICE_COORD_REGULARIZACION && despliegueMenuSeccion.aplicaModAmpliacion)
                            {
                                return true;
                            }
                        }
                    }
                    else if (tipoModificacion.id == rbTipo.MOD_CONCESION_REDUCE_SUPERFICIE)
                    {
                        if (idPestania == rbTipo.ANTECEDENTES_ESPACIALES)
                        {
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ORGINAL && despliegueMenuSeccion.aplicaModReduccion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.ARCHIVOS_ADJUNTOS_COORD_ORGINAL && despliegueMenuSeccion.aplicaModReduccion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.POLIGONO_COORD_ORIGINAL && despliegueMenuSeccion.aplicaModReduccion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.VERTICE_COORD_ORIGINAL && despliegueMenuSeccion.aplicaModReduccion)
                            {
                                return true;
                            }
                        }

                        if (idPestania == rbTipo.ANTECEDENTES_TERRENO)
                        {

                            if (despliegueMenuSeccion.idSeccion == rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL && despliegueMenuSeccion.aplicaModReduccion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.ARCHIVOS_ADJUNTOS_COORD_ENTREGA_MATERIAL && despliegueMenuSeccion.aplicaModReduccion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.POLIGONO_COORD_ENTREGA_MATERIAL && despliegueMenuSeccion.aplicaModReduccion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.VERTICE_COORD_ENTREGA_MATERIAL && despliegueMenuSeccion.aplicaModReduccion)
                            {
                                return true;
                            }
                        }

                        if (idPestania == rbTipo.REGULARIZACION)
                        {

                            if (despliegueMenuSeccion.idSeccion == rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION && despliegueMenuSeccion.aplicaModReduccion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.ARCHIVOS_ADJUNTOS_COORD_REGULARIZACION && despliegueMenuSeccion.aplicaModReduccion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.POLIGONO_COORD_REGULARIZACION && despliegueMenuSeccion.aplicaModReduccion)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.VERTICE_COORD_REGULARIZACION && despliegueMenuSeccion.aplicaModReduccion)
                            {
                                return true;
                            }
                        }
                    }
                    else if (tipoModificacion.id == rbTipo.MOD_CONCESION_PT)
                    {
                        if (idPestania == rbTipo.ANTECEDENTES_ESPACIALES)
                        {
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ORGINAL && despliegueMenuSeccion.aplicaModPT)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.ARCHIVOS_ADJUNTOS_COORD_ORGINAL && despliegueMenuSeccion.aplicaModPT)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.POLIGONO_COORD_ORIGINAL && despliegueMenuSeccion.aplicaModPT)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.VERTICE_COORD_ORIGINAL && despliegueMenuSeccion.aplicaModPT)
                            {
                                return true;
                            }
                        }

                        if (idPestania == rbTipo.ANTECEDENTES_TERRENO)
                        {

                            if (despliegueMenuSeccion.idSeccion == rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL && despliegueMenuSeccion.aplicaModPT)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.ARCHIVOS_ADJUNTOS_COORD_ENTREGA_MATERIAL && despliegueMenuSeccion.aplicaModPT)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.POLIGONO_COORD_ENTREGA_MATERIAL && despliegueMenuSeccion.aplicaModPT)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.VERTICE_COORD_ENTREGA_MATERIAL && despliegueMenuSeccion.aplicaModPT)
                            {
                                return true;
                            }
                        }

                        if (idPestania == rbTipo.REGULARIZACION)
                        {

                            if (despliegueMenuSeccion.idSeccion == rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION && despliegueMenuSeccion.aplicaModPT)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.ARCHIVOS_ADJUNTOS_COORD_REGULARIZACION && despliegueMenuSeccion.aplicaModPT)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.POLIGONO_COORD_REGULARIZACION && despliegueMenuSeccion.aplicaModPT)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.VERTICE_COORD_REGULARIZACION && despliegueMenuSeccion.aplicaModPT)
                            {
                                return true;
                            }
                        }
                    }
                    else if (tipoModificacion.id == rbTipo.MOD_CONCESION_ESPECIE)
                    {
                        if (idPestania == rbTipo.ANTECEDENTES_ESPACIALES)
                        {
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ORGINAL && despliegueMenuSeccion.aplicaModEspecie)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.ARCHIVOS_ADJUNTOS_COORD_ORGINAL && despliegueMenuSeccion.aplicaModEspecie)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.POLIGONO_COORD_ORIGINAL && despliegueMenuSeccion.aplicaModEspecie)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.VERTICE_COORD_ORIGINAL && despliegueMenuSeccion.aplicaModEspecie)
                            {
                                return true;
                            }
                        }

                        if (idPestania == rbTipo.ANTECEDENTES_TERRENO)
                        {

                            if (despliegueMenuSeccion.idSeccion == rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL && despliegueMenuSeccion.aplicaModEspecie)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.ARCHIVOS_ADJUNTOS_COORD_ENTREGA_MATERIAL && despliegueMenuSeccion.aplicaModEspecie)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.POLIGONO_COORD_ENTREGA_MATERIAL && despliegueMenuSeccion.aplicaModEspecie)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.VERTICE_COORD_ENTREGA_MATERIAL && despliegueMenuSeccion.aplicaModEspecie)
                            {
                                return true;
                            }
                        }

                        if (idPestania == rbTipo.REGULARIZACION)
                        {

                            if (despliegueMenuSeccion.idSeccion == rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION && despliegueMenuSeccion.aplicaModEspecie)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.ARCHIVOS_ADJUNTOS_COORD_REGULARIZACION && despliegueMenuSeccion.aplicaModEspecie)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.POLIGONO_COORD_REGULARIZACION && despliegueMenuSeccion.aplicaModEspecie)
                            {
                                return true;
                            }
                            if (despliegueMenuSeccion.idSeccion == rbSeccion.VERTICE_COORD_REGULARIZACION && despliegueMenuSeccion.aplicaModEspecie)
                            {
                                return true;
                            }
                        }
                    }   
                }
            }
            return false;
        }

        /**
         * Método que evalúa particularmente si un campo debe ser visible de acuerdo al tipo de modificación de una
         * solicitud de modificación.
         */
        public bool debeDesplegarCampo(List<ParametroGenerico> tipoModificacionList, String campo, int idTipoPestania) {

            
            switch (campo)
            {
                case "Se usa para Banco":
                    if (idTipoPestania == rbTipo.ANTECEDENTES_ESPACIALES) {
                        return true;
                    }
                    else if (idTipoPestania == rbTipo.ANTECEDENTES_TERRENO)
                    {
                        return true;
                    }
                    else if (idTipoPestania == rbTipo.REGULARIZACION)
                    {
                        return false;
                    }
                    
                    break;
                case "Se usa para Visualizador de Mapas":
                    if (idTipoPestania == rbTipo.ANTECEDENTES_ESPACIALES)
                    {
                        return true;
                    }
                    else if (idTipoPestania == rbTipo.ANTECEDENTES_TERRENO)
                    {
                        return true;
                    }
                    else if (idTipoPestania == rbTipo.REGULARIZACION)
                    {
                        return true;
                    }

                    break;
            }

            return true;

        }

        /**
         * Método que guarda los datos de la superficie total final y de la superficie Ampl/Reduc Requerida de
         * una solicitud de modificación.
         */
        public bool guarDatosAdicionalesModificacion(SolicitudConcesion solicitudModificacion, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    /* Se guardan los datos de la superficie total final y de la superficie Ampl/Reduc Requerida */
                    if (!solicitudDA.GuardarSolicitudMod(solicitudModificacion, idUsuario))
                    {
                        return false;
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

        public bool tieneSoloTipoModificacionRegularizacion(SolicitudConcesion solicitudModificacion)
        {
           bool tieneRegularizacion = false;
           bool tieneOtroTipoMod = false;
           foreach (ParametroGenerico tipoModificacion in solicitudModificacion.tipoModificacionesTram)
           {
               if (tipoModificacion != null && (tipoModificacion.id == rbTipo.MOD_CONCESION_REGULARIZACION) || (tipoModificacion.id == rbTipo.MOD_AMERB_REGULARIZACION) ||
                   (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REGULARIZACION) || (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REGULARIZACION) ||
                   (tipoModificacion.id == rbTipo.MOD_ECMPO_REGULARIZACION))
               {
                   tieneRegularizacion = true;

               }
               else {

                   tieneOtroTipoMod = true;
               }
               
           }

           if (tieneRegularizacion && !tieneOtroTipoMod)
           {
               tieneRegularizacion = true;
           }
           else {
               tieneRegularizacion = false;
           }

           return tieneRegularizacion;
       }

        public bool eliminarSolicitudModificacion(int idSolConces, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    if (!solicitudDA.EliminarSolicitudUE(idSolConces, idUsuario))
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

        public bool eliminarSolicitudModificacionAmerb(int idSolConces, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    if (!solicitudDA.EliminarSolicitud_UE_Mod(idSolConces, rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB, idUsuario))
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

        /**
         *  Recalcula los errores de la solicitud de modificación, al ingresar a través de la opción
         *  de ver el listado de errores que posee una solicitud.
         */
        public bool recalcularErroresSolicitud(SolicitudConcesion solicitudModificacion)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    
                    List<AlertaTramiteModConcesion> alertaTramiteModConcesionList = this.verificarAlertasModificacion(solicitudModificacion);

                    //Elimina Alertas Anteriores de la Solicitud de Modificación
                    if (!alertaTramiteModConcesionDA.EliminarAlertaTramiteModConcesion(0,solicitudModificacion.idSolConcesion))
                    {
                        return true;
                    }

                    //Guarda las Alertas de Modificación de la verificación actual de la solicitud.
                    if (alertaTramiteModConcesionList != null)
                    {
                        foreach (AlertaTramiteModConcesion alertaTramiteModConcesion in alertaTramiteModConcesionList)
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

        /**
         * Método que comprueba que una solicitud de modificación posee
         * comuna fronteriza.
         */
        public bool esComunaFronteriza(int idSolicitudConcesion)
        {
            try
            {
                bool esComunaFronteriza = false;
                SolicitudConcesion solicitudConcesion = solicitudDA.ObtieneSolicitudConcesionMod(idSolicitudConcesion, 0);
                if (solicitudConcesion != null && solicitudConcesion.comunaFronteriza)
                {
                    esComunaFronteriza = true;
                }
                
                return esComunaFronteriza;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }

        //public bool guardarUnidadDependenciaSolicitud(SolicitudConcesion solicitudConcesion)
        //{
        //    try
        //    {
        //        if (solicitudConcesion != null)
        //        {
        //            /* Se guarda la respuesta del usuario declarando si tiene dependencia */
        //            if(!solicitudDA.ActualizaSolicitud_AplicaDep(solicitudConcesion.idSolConcesion,solicitudConcesion.aplicaDependencia)){
        //                return false;
        //            }

        //            /* Elimina el detalle de las unidades de dependencia de la solicitud de modificación */
        //            if (!unidadDependenciaModDA.EliminarUnidadDependenciaMod(solicitudConcesion.idSolConcesion))
        //            {
        //                return false;
        //            }
                   
        //            /* Se guarda el detalle de la unidad de dependencia nuevamente */
        //            if (solicitudConcesion.unidadDependenciaList != null && solicitudConcesion.unidadDependenciaList.Count > 0)
        //            {
        //                foreach (UnidadDependenciaMod UnidadDependenciaModin in solicitudConcesion.unidadDependenciaList)
        //                {
        //                    if (!unidadDependenciaModDA.GuardarUnidadDependenciaMod(UnidadDependenciaModin))
        //                    {
        //                        return false;
        //                    }
        //                }
        //            }

        //            return true;
        //        }

        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.PrintError(ex);
        //        logger.SendMailError(ex);
        //        return false;
        //    }
        //}

        public bool eliminarSolicitudModificacionAcopio(int idSolConces, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    if (!solicitudDA.EliminarSolicitudUE(idSolConces, idUsuario))
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

        public bool eliminarSolicitudModificacionFaenamiento(int idSolConces, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    if (!solicitudDA.EliminarSolicitudUE(idSolConces, idUsuario))
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



        /// <summary>
        /// DETERMINA SI UN MENU DEBE MOSTRARSE PARA ESA MODIFICACION (MOD DE CONCESION, MOD ACOPIO, ETC)
        /// BASTA CON QUE UNA SECCION SE MUESTRE PARA MOSTRAR EL MENU
        /// </summary>
        /// <param name="solicitudModificacion"></param>
        /// <param name="despliegueMenuSeccionList"></param>
        /// <param name="array"></param>
        /// <returns>bool indicando si aplica el menu</returns>
        public bool despliegueMenu(SolicitudConcesion solicitudModificacion, List<DespliegueMenuSeccion> despliegueMenuSeccionList, HashSet<int> seccionesEvaluar)
        {
            if (solicitudModificacion != null && despliegueMenuSeccionList != null)
            {
                List<ParametroGenerico> tipoModificacionList = solicitudModificacion.tipoModificacionesTram;

                if (tipoModificacionList != null) { 

                    foreach (ParametroGenerico tipoModificacion in tipoModificacionList)
                    {
                        foreach (DespliegueMenuSeccion despliegueMenuSeccion in despliegueMenuSeccionList)
                        {

                            if(seccionesEvaluar.Contains(despliegueMenuSeccion.idSeccion)){

                                if ((tipoModificacion.id == rbTipo.MOD_CONCESION_PT || tipoModificacion.id == rbTipo.MOD_AMERB_PT || tipoModificacion.id == rbTipo.MOD_ECMPO_PT || tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_PT_ESPECIE || tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_PT_ESPECIE) && despliegueMenuSeccion.aplicaModPT)
                                {
                                    return true;
                                }
                                if ((tipoModificacion.id == rbTipo.MOD_CONCESION_ESPECIE || tipoModificacion.id == rbTipo.MOD_AMERB_ESPECIE || tipoModificacion.id == rbTipo.MOD_ECMPO_ESPECIE || tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_RENOVACION || tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_RENOVACION) && despliegueMenuSeccion.aplicaModEspecie)
                                {
                                    return true;
                                }
                                if ((tipoModificacion.id == rbTipo.MOD_CONCESION_REDUCE_SUPERFICIE || tipoModificacion.id == rbTipo.MOD_AMERB_REDUCE_SUPERFICIE || tipoModificacion.id == rbTipo.MOD_ECMPO_REDUCE_SUPERFICIE || tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE || tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE) && despliegueMenuSeccion.aplicaModReduccion)
                                {
                                    return true;
                                }
                                if ((tipoModificacion.id == rbTipo.MOD_CONCESION_AMPLIA_SUPERFICIE || tipoModificacion.id == rbTipo.MOD_AMERB_AMPLIA_SUPERFICIE || tipoModificacion.id == rbTipo.MOD_ECMPO_AMPLIA_SUPERFICIE || tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE || tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE) && despliegueMenuSeccion.aplicaModAmpliacion)
                                {
                                    return true;
                                }
                                if ((tipoModificacion.id == rbTipo.MOD_CONCESION_REGULARIZACION || tipoModificacion.id == rbTipo.MOD_AMERB_REGULARIZACION || tipoModificacion.id == rbTipo.MOD_ECMPO_REGULARIZACION || tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REGULARIZACION || tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REGULARIZACION) && despliegueMenuSeccion.aplicaModRegularizacion)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }


        /**
     *  DETERMINA SI UNA SOLICITUD TIENE UN IT UOT VIGENTE CON RESULTADO PENDIENTE O SUPEDITA
     */
        public bool SolicitudEstaSuspendidaPendiente(int idSolconcesion)
        {
            try
            {
                return solicitudDA.SolicitudEstaSuspendidaPendiente(idSolconcesion);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }




        /**
        *  DETERMINA SI UNA SOLICITUD ESTA SUSPENDIDA 
        *  TIENE UNA RELACION VIGENTE EN UN GRUPO SUSPENDIDO VIGENTE 
        */
        public bool SolicitudEstaSuspendida(int idSolconcesion)
        {
            try
            {
                return solicitudDA.SolicitudEstaSuspendida(idSolconcesion);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }





        public bool redefinirTramiteModificacion(SolicitudConcesion solicitudModificacion, int idUsuario)
        {

            
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    //Borrar los tipos de modificacion acutuales
                    if (!solicitudDA.EliminarTipoModificacionSolicitud(solicitudModificacion.idSolConcesion, 0))
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
