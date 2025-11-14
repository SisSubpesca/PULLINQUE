using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Entidades;
using System.Transactions;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.concesiones;

namespace LogicaNegocio.cl.subpesca.rb.servicios.solicitudes
{
    public class SolicitudExperimentalesAmerbService
    {

        Logger logger = new Logger();
        SolicitudDA solicitudDA = new SolicitudDA();
        SolicitanteDA solicitanteDA = new SolicitanteDA();
        UnidadEspacialDA unidadEspacialDA = new UnidadEspacialDA();
        ProyectoTecnicoDA proyectoTecnicoDA = new ProyectoTecnicoDA();
        CoordenadaGeograficaDA coordenadaGeograficaDA = new CoordenadaGeograficaDA();

        //transformar Solicitud Amerb en Experimental Amerb EB.
        public bool TransformarSolicitudAmerbExperimentalAmerb(SolicitudConcesion ExperimentalSolicitud, SolicitudConcesion SolicitudAmerb, int idUsuario)
        {
            ConcesionService concesionService = new ConcesionService();
            Logger logger = new Logger();

            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    //Guardar Solicitud Experimental
                    ExperimentalSolicitud.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                    if (!solicitudDA.GuardarSolicitudExperimentalesAmerb(ExperimentalSolicitud))
                    {
                        return false;
                    }

                    //GUARDAR DATA ADICIONAL DE LA UNIDAD ESPACIAL
                    ExperimentalSolicitud.datosSolicitudUE.idSolConcesion = ExperimentalSolicitud.idSolConcesion;
                    if (!solicitudDA.GuardarDatosSolicitudUE(ExperimentalSolicitud.datosSolicitudUE, idUsuario))
                    {
                        return false;
                    }

                    //guarda el amerb padre (en caso de estar asociado a uno)
                    if (ExperimentalSolicitud.unidadEspacial != null && ExperimentalSolicitud.unidadEspacial.centrosDeCultivo != null && ExperimentalSolicitud.unidadEspacial.centrosDeCultivo.codigoCentro != null && !ExperimentalSolicitud.unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
                    {
                        if (!solicitudDA.ActualizaSolicitud_cultivoExperimental(ExperimentalSolicitud.datosSolicitudUE.idDatosSolicitud, "AMERB_UE", Convert.ToInt32(ExperimentalSolicitud.unidadEspacial.centrosDeCultivo.codigoCentro)))
                        {
                            return false;
                        }
                    }
                    //se debe cargar la otra concesion la antigua.
                    
                    //Guarda una copia de los Solicitantes a la solicitud Experimental.
                    if (SolicitudAmerb != null && SolicitudAmerb.idSolConcesion > 0)
                    {
                        List<Solicitante> solicitanteList = solicitanteDA.listarSolicitante(SolicitudAmerb.idSolConcesion, 0, "", rbEstadosGenerales.VIGENTE);
                        if (solicitanteList != null)
                        {
                            foreach (Solicitante solicitante in solicitanteList)
                            {
                                solicitante.idPersonasLeg = 0;
                                solicitante.solicitud = new SolicitudConcesion();
                                solicitante.solicitud.idSolConcesion = ExperimentalSolicitud.idSolConcesion;
                                if (!solicitanteDA.GuardarSolicitante(solicitante, idUsuario))
                                {
                                    return false;
                                }
                            }
                        }

                        //Guarda una Copia de PT a la Experimental.
                        if (!proyectoTecnicoDA.GuardarProyectoTecnico(ExperimentalSolicitud.idSolConcesion, SolicitudAmerb.idSolConcesion))
                        {
                            return false;
                        }
                        ExperimentalSolicitud.coordenadaGeografica = new List<CoordenadaGeografica>();
                        
                        //Guarda una copia de Ant. del Sector a la Experimental     
                        if (!coordenadaGeograficaDA.GuardarCoordenadaGeograficaTramiteMod(ExperimentalSolicitud.idSolConcesion, SolicitudAmerb.idSolConcesion))
                        {
                            return false;
                        }

                        UnidadEspacial unidadEspacial = new UnidadEspacial();
                        unidadEspacial = unidadEspacialDA.ObtieneUnidadEspacial(SolicitudAmerb.idSolConcesion, 0);
                        //Se cae por null :L
                        if(unidadEspacial == null)
                        {
                            unidadEspacial = new UnidadEspacial();
                        }
                        unidadEspacial.idUnidadEspacial = 0;
                        unidadEspacial.idSolicitud = ExperimentalSolicitud.idSolConcesion;
                        unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                        unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToString(ExperimentalSolicitud.codigoCentro);

                        if (!unidadEspacialDA.GuardarUnidadEspacialModificacion(unidadEspacial, idUsuario))
                        {
                            return false;
                        }
                    }

                    if (!solicitudDA.ActualizaSolicitudConcesionEstado(SolicitudAmerb.idSolConcesion, Datos.Contantes.cierre.Transformado))
                    {
                        return false;
                    }

                    transactionScope.Complete();
                    return true;

                }
                catch(Exception e)
                {
                    logger.SendMailError(e);
                    return false;   
                }
            }
        }

        //GUARDA UNA SOLICITUD DE EXPERIMENTALES EN AMERB
        public bool guardarSolicitudExperimentalesAmerb(SolicitudConcesion solicitudInicial, int idUsuario)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    //GUARDAR SOLICITUD
                    solicitudInicial.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                    if (!solicitudDA.GuardarSolicitudExperimentalesAmerb(solicitudInicial))
                    {
                        return false;
                    }

                    //GUARDAR DATA ADICIONAL DE LA UNIDAD ESPACIAL
                    solicitudInicial.datosSolicitudUE.idSolConcesion = solicitudInicial.idSolConcesion;
                    if (!solicitudDA.GuardarDatosSolicitudUE(solicitudInicial.datosSolicitudUE, idUsuario))
                    {
                        return false;
                    }

                     //guarda el amerb padre (en caso de estar asociado a uno)
                    if (solicitudInicial.unidadEspacial != null && solicitudInicial.unidadEspacial.centrosDeCultivo != null && solicitudInicial.unidadEspacial.centrosDeCultivo.codigoCentro != null && !solicitudInicial.unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
                    {
                        if (!solicitudDA.ActualizaSolicitud_cultivoExperimental(solicitudInicial.datosSolicitudUE.idDatosSolicitud, "AMERB_UE", Convert.ToInt32(solicitudInicial.unidadEspacial.centrosDeCultivo.codigoCentro)))
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
