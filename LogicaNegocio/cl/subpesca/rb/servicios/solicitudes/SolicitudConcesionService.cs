using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Transactions;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using System.Collections;
using LogicaNegocio.cl.subpesca.rb.common;
using System.Data.SqlClient;
using Datos.AccesoDatos;


namespace LogicaNegocio.cl.subpesca.rb.servicios.solicitudes
{
    public class SolicitudConcesionService
    {
        Logger logger = new Logger();
        SolicitudDA solicitudDA = new SolicitudDA();

        public bool guardarSolicitudConcesionInicial(SolicitudConcesion solicitudInicial) {
           
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    //GUARDAR SOLICITUD
                    solicitudInicial.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);
                    if (!solicitudDA.GuardarSolicitudConcesionInicial(solicitudInicial))
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
         * GUARDA UN SECTOR DE UN TRAMITE DE RELOCALIZACION  COMO UNA SOLICITUD PARA QUE COMIENCE A AVANZAR POR LOS ESTADOS
         */ 
        public bool guardarSolicitudConcesionRelocalizacion(SolicitudConcesion solicitudInicial)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    //GUARDAR SOLICITUD
                    solicitudInicial.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION);
                    if (!solicitudDA.GuardarSolicitudConcesionInicial(solicitudInicial))
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
         * GUARDA UN SECTOR DE UN TRAMITE DE RELOCALIZACION RESA COMO UNA SOLICITUD PARA QUE COMIENCE A AVANZAR POR LOS ESTADOS
         */ 
        public bool guardarSolicitudConcesionRelocalizacionRESA(SolicitudConcesion solicitudInicial)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    //GUARDAR SOLICITUD
                    solicitudInicial.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION_RESA);
                    if (!solicitudDA.GuardarSolicitudConcesionInicial(solicitudInicial))
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

        public bool guardarSolicitudConcesion(SolicitudConcesion solicitudInicial, int idUsuario)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    //GUARDAR SOLICITUD
                    if (!solicitudDA.GuardarSolicitud(solicitudInicial, idUsuario))
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
        public bool ActualizaSolicitudSEA(int idSolicitud, int aplicaSEA, int idUsuario) {


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



        //DETERMINA SI EL BOTON DE CREAR O MODIFICAR UNIDAD ESPACIAL DEBE APARECER
        public bool aplicaBotonCreaModUnidadEspacial(int idSolicitud)
        {

            try
            {

                return solicitudDA.aplicaBotonSolicitudUE(idSolicitud);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }


        }

        //ACTUALIZA EL BIT QUE INDICA SI SE MODIFICO O RELOCALIZO UNA CONCESION
        public bool ActualizaSolicitud_Traspaso(int idSolicitud, bool traspasoOk)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    //GUARDAR SOLICITUD
                    if (!solicitudDA.ActualizaSolicitud_Traspaso(idSolicitud, traspasoOk))
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


        //ACTUALIZA EL BIT QUE INDICA SI ESTA O NO EN RECURSO DE REPOSICION
        public bool ActualizaSolicitud_RecReposicion(int idSolicitud, bool recursoReposicion, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    //GUARDAR SOLICITUD
                    if (!solicitudDA.ActualizaSolicitud_RecReposicion(idSolicitud, recursoReposicion, idUsuario))
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
         * SE UTILIZA EL CAMPO SOMETIMIENTO_CPS_INFAS  PARA GUARDAR "DEBE EVALUARSE AMBIENTAL"
         */ 
        public bool ActualizaSolicitud_EvaluarseAmbientalmente(int idSolicitud, int evaluarseAmbientalmente)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {


                    if (!solicitudDA.ActualizaSolicitud_SometimientoCPS_INFAS(idSolicitud, evaluarseAmbientalmente))
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

        public bool ActualizaSolicitud_VerificaFirmaSeguimiento(int idSolicitud, int estadoFirmaConvenio, int estadoSeguimientoDia)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {


                    if (!solicitudDA.ActualizaSolicitud_VerificaFirmaSeguimiento(idSolicitud, estadoFirmaConvenio, estadoSeguimientoDia))
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

        public bool ActualizaSolicitud_cultivoExperimental(int idSolicitud, int cultivoExperimental, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {


                    if (!solicitudDA.ActualizaSolicitud_cultivoExperimental(idSolicitud, cultivoExperimental, idUsuario))
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



        public bool ActualizaSolicitud_VerificaCertOperacion(int idSolicitud, int verificaCertOperacion)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {


                    if (!solicitudDA.ActualizaSolicitud_VerificaCertOperacion(idSolicitud, verificaCertOperacion))
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



        public bool ActualizaSolicitud_evaluaUOT(int idSolicitud, int valor)
        {


            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolicitud, TipoDecisionUsuario.evaluaUOT ,valor))
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


        public bool ActualizaSolicitud_evaluaUTS(int idSolicitud, int valor)
        {


            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolicitud, TipoDecisionUsuario.tramitaUTS, valor))
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


        public bool ActualizaSolicitud_DevolucionJuridica(int idSolConcesion, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolConcesion, TipoDecisionUsuario.idDevPesca, valor))
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


        public bool ActualizaSolicitud_PertinenciaSMA(int idSolConcesion, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {


                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolConcesion, TipoDecisionUsuario.informarSMA, valor))
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

        public bool ActualizaSolicitud_AvanzaSMA(int idSolConcesion, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {


                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolConcesion, TipoDecisionUsuario.esperaRespuestaSMA, valor))
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


        public bool ActualizaSolicitud_SupeditaAvanzaAprueba(int idSolConcesion, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {


                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolConcesion, TipoDecisionUsuario.supeditaAvanzaAprueba, valor))
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


        public bool ActualizaSolicitud_SuspendidaAvanza(int idSolConcesion, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {


                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolConcesion, TipoDecisionUsuario.suspendeAvanzaEstado, valor))
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


        public bool ActualizaSolicitud_Planos(int idSolConcesion, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {


                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolConcesion, TipoDecisionUsuario.requiereIT_UOT_Plano, valor))
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

        public bool ActualizaSolicitud_TipoEvaluacionAmbiental(int idSolConcesion, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {


                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolConcesion, TipoDecisionUsuario.evalRamaNoSomete, valor))
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

        public bool ActualizaSolicitud_DevolucionMarina(int idSolConcesion, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {


                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolConcesion, TipoDecisionUsuario.idDevMarina, valor))
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

        public bool ActualizaSolicitud_CertificadoDistancia(int idSolConcesion, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolConcesion, TipoDecisionUsuario.idTipoSolCertDistancia, valor))
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


        //RAMA DE MO
        public bool ActualizaSolicitud_NuevoITCMOUOT(int idSolConcesion, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolConcesion, TipoDecisionUsuario.requiereITC_MO, valor))
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

        //RAMA DE CPS E INFAS
        public bool ActualizaSolicitud_NuevoITCCPSUOT(int idSolConcesion, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolConcesion, TipoDecisionUsuario.requiereITC_CPS, valor))
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



        //Decision Sectorialista Zonal
        public bool ActualizaSolicitud_SectorialistaZonal(int idSolConcesion, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolConcesion, TipoDecisionUsuario.envioSSPCentral_zonal, valor))
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


        //Decision Sectorialista Central
        public bool ActualizaSolicitud_SectorialistaCentral(int idSolConcesion, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolConcesion, TipoDecisionUsuario.sspCentralDevCarta, valor))
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



        public bool ActualizaSolicitud_SectorialistaZonalNotifacionInsuficiencia(int idSolConcesion, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolConcesion, TipoDecisionUsuario.confZonalNotInsuficiencia, valor))
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

        public bool ActualizaSolicitud_DecisionUGP(int idSolConcesion, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolConcesion, TipoDecisionUsuario.tramitaUGP, valor))
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

        public bool ActualizaSolicitud_DecisionUGPMultiple(int idSolConcesion, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolConcesion, TipoDecisionUsuario.estadoAplica, valor))
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

        public bool ActualizaVerifica_Antecedentes(int idSolConcesion, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolConcesion, TipoDecisionUsuario.verificaAntecedentes, valor))
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
         * OBTIENE LA DECISION QUE TOMO EL USUARIO EN LOS DISTINTOS CHECK DEL FLUJO
         */ 
        public SolicitudConcesion obtenerDecisionUsuario(int idSolConcesion)
        {
            try
            {
                return solicitudDA.obtenerDecisionUsuario(idSolConcesion);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
            
        }

        public bool ActualizaSolicitud_evaluaUOT_Cartografia(int idSolConcesion, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolConcesion, TipoDecisionUsuario.evaluaUOT_Cartografia, valor))
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


        public bool ActualizaSolicitud_IdTipoRechazoSol(int idSolConcesion, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolConcesion, TipoDecisionUsuario.idTipoRechazoSol, valor))
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



        public bool ActualizaSolicitud_SectorialistaCentralNotifacionInsuficiencia(int idSolConcesion, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolConcesion, TipoDecisionUsuario.confCentralNotInsuficiencia, valor))
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
         * OBTIENE CADENA CON REQUERIMIENTOS PENDIENTES ABIERTOS
         */ 
        public string ObtieneReqPendiente_Solicitud(int idSolConcesion)
        {
            try
            {

                return solicitudDA.ObtieneReqPendiente_Solicitud(idSolConcesion);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        public bool ActualizaSolicitud_NuevoProyectoTecnico(int idSolConcesion, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolConcesion, TipoDecisionUsuario.requiereNuevoPT, valor))
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

        public bool ActualizaSolicitud_RequiereSSFFAA(int idSolicitud, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolicitud, TipoDecisionUsuario.omiteSSFFAA, valor))
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

        public bool ActualizaSolicitud_RequiereDifusionBanco(int idSolicitud, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolicitud, TipoDecisionUsuario.omiteDifRadial, valor))
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

        public bool ActualizaSolicitud_RequiereBancoNatural(int idSolicitud, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolicitud, TipoDecisionUsuario.omiteBanco, valor))
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

        public bool ActualizaSolicitud_RequiereEvaluacionAmbiental(int idSolicitud, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolicitud, TipoDecisionUsuario.omiteEvAmbiental, valor))
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

        public bool ActualizaSolicitud_RequiereInspeccionTerreno(int idSolicitud, int valor)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!solicitudDA.ActualizaSolicitud_DecisionUsuario(idSolicitud, TipoDecisionUsuario.omiteInspTerreno, valor))
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
    }
}
