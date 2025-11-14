using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace LogicaNegocio.cl.subpesca.rb.servicios.solicitudes
{
    public class InformacionAdicionalSolicitudesService
    {

        Logger logger = new Logger();
        SolicitudDA solicitudDA = new SolicitudDA();
        DetalleDatosSolicitudDA detalleDatosSolicitudDA = new DetalleDatosSolicitudDA();

        /**
        * OBTIENE LA INFORMACIÓN QUE SE INGRESO EN LA CREACION DEL TRAMITE DE UNA DETERMINADA UNIDAD ESPACIAL
        **/
        public DatosSolicitudUE ObtieneDatosSolicitudUE(int idSolicitud)
        {
            try
            {
                return solicitudDA.ObtieneDatosSolicitudUE(idSolicitud);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }

        }


        /**
         * GUARDA LA INFORMACIÓN QUE SE INGRESA EN LA FICHA PARTICULAR DE CADA UNIDAD ESPACIAL
         **/
        public bool GuardarDetalleDatosSolicitud(DetalleDatosSolicitud detSolicitud, int idUsuario)
        {
            try
            {
                return detalleDatosSolicitudDA.GuardarDetalleDatosSolicitud(detSolicitud, idUsuario);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }



        /**
         * OBTIENE LA INFORMACIÓN QUE SE INGRESO EN LA FICHA PARTICULAR DE CADA UNIDAD ESPACIAL
         **/
        public DetalleDatosSolicitud ObtieneDetalleDatosSolicitud(int idSolicitud)
        {
            try
            {
                return detalleDatosSolicitudDA.ObtieneDetalleDatosSolicitud(idSolicitud);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        /**
         * OBTIENE LA INFORMACIÓN QUE SE INGRESO EN LA FICHA PARTICULAR DE CADA UNIDAD ESPACIAL
         **/
        public DatosSolicitudUE ObtieneDatosSolicitudUENew(int idSolicitud)
        {
            try
            {
                return solicitudDA.ObtieneDatosSolicitudUE(idSolicitud);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public bool guardarAmerbPadreSolicitud(DetalleDatosSolicitud detalleDatosSolicitud, int idUsuario)
        {
            try
            {
                DetalleDatosSolicitud detalleDatosSolOriginal = detalleDatosSolicitudDA.ObtieneDetalleDatosSolicitud(detalleDatosSolicitud.idSolConcesion);

                //SI NO EXISTE EL REGISTRO SE GUARDA
                if (detalleDatosSolOriginal == null && detalleDatosSolicitud.idSolConcesion > 0)
                {

                    detalleDatosSolOriginal = new DetalleDatosSolicitud();
                    detalleDatosSolOriginal.idSolConcesion = detalleDatosSolicitud.idSolConcesion;

                    if (!detalleDatosSolicitudDA.GuardarDetalleDatosSolicitud(detalleDatosSolOriginal, idUsuario))
                    {
                        return false;
                    }

                    detalleDatosSolicitud.idDetDato = detalleDatosSolOriginal.idDetDato;
                }


                if (!solicitudDA.ActualizaSolicitud_cultivoExperimental(detalleDatosSolOriginal.idDetDato, "AMERB_VISTA", detalleDatosSolicitud.codigo))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }

        public bool guardarECMPOPadreSolicitud(DetalleDatosSolicitud detalleDatosSolicitud, int idUsuario)
        {
            try
            {
                DetalleDatosSolicitud detalleDatosSolOriginal = detalleDatosSolicitudDA.ObtieneDetalleDatosSolicitud(detalleDatosSolicitud.idSolConcesion);

                //SI NO EXISTE EL REGISTRO SE GUARDA
                if (detalleDatosSolOriginal == null && detalleDatosSolicitud.idSolConcesion > 0) {

                    detalleDatosSolOriginal = new DetalleDatosSolicitud();
                    detalleDatosSolOriginal.idSolConcesion = detalleDatosSolicitud.idSolConcesion;

                    if (!detalleDatosSolicitudDA.GuardarDetalleDatosSolicitud(detalleDatosSolOriginal, idUsuario))
                    {
                        return false;
                    }

                    detalleDatosSolicitud.idDetDato = detalleDatosSolOriginal.idDetDato;
                }


                if (!solicitudDA.ActualizaSolicitud_cultivoExperimental(detalleDatosSolOriginal.idDetDato, "ECMPO_VISTA", detalleDatosSolicitud.codigo))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }

        /**
         * Actualiza tabla DatosSolicitudUE para el almacenamiento de datos especiales
         * de Solicitud de Acuicultura en Amerb.
         */
        public bool GuardarDetalleAmerb(DetalleDatosSolicitud detalleDatosSolicitud)
        {
            try
            {
                return solicitudDA.ActualizaDatosSolicitudUE_DatosAmerb(detalleDatosSolicitud.idDetDato, detalleDatosSolicitud.superficieSectorAmerb, detalleDatosSolicitud.porcentSectorAmerb);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }

        /**
         * Actualiza tabla DatosSolicitudUE para el almacenamiento de datos especiales
         * de Solicitud de Colectores de Semillas.
         */
        public bool GuardarDetalleDatosSolicitudUE(DetalleDatosSolicitud detalleDatosSolicitud)
        {
            try
            {
                return solicitudDA.ActualizaDatosSolicitudUE_DatosColector(detalleDatosSolicitud.idDetDato, detalleDatosSolicitud.periodoOperacionCol, detalleDatosSolicitud.observaciones);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }

        /**
       *  DETERMINA SI UNA SOLICITUD ESTA EN UN GRUPO SUSPENDIDO (CON SUS RELACIONES VIGENTES)
       *  O BIEN TIENE UN IT UOT VIGENTE CON RESULTADO PENDIENTE O SUPEDITA
       */ 
        public bool SolicitudEstaSuspendidaPendientSupeditada(int idSolconcesion)
        {
            try
            {
                return solicitudDA.SolicitudEstaSuspendidaPendientSupeditada(idSolconcesion);
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
