using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Transactions;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.relocalizacion;

namespace LogicaNegocio.cl.subpesca.rb.servicios.solicitudes
{
    public class CommonService
    {

        Logger logger = new Logger();
        SolicitudDA solicitudDA = new SolicitudDA();
        private TramiteRelocalizacionDA tramiteRelocalizacionDA = new TramiteRelocalizacionDA();

        public bool EliminarSolicitudRel(int SolConcesion, int idUsuario)
        {
            try
            {
                if (!solicitudDA.EliminarSolicitudUE(SolConcesion, idUsuario))
                {
                    return false;
                }
                return true;
            }
            catch(Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }

        /**
         * Elimina una solicitud de Unidad espacial, se debe enviar el tipo de tramite para comprobar que coincida con el
         * tipo de tramite que posee la solicitud con el id de solicitud pasado como parametro.
         */
        public bool EliminarSolicitudConcesion(int idSolConcesion, int idTipoTramSolicitud, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    if (!solicitudDA.EliminarSolicitudConcesion(idSolConcesion, idTipoTramSolicitud, idUsuario))
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
         * Elimina una solicitud de Unidad espacial
         */
        public bool EliminarUnidadEspacial(int idSolConcesion, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    if (!solicitudDA.EliminarUE_Total(null, idSolConcesion, idUsuario))
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
        * Transforma una Unidad espacial en solicitud
         * LA TRANSACCION ES MANEJADA EN EL PROCEDIMIENTO ALMACENADO
        */
        public ParametroGenerico TransformarUnidadEspacial(int idSolConcesion, int idUsuario)
        {
            
            try
            {
                return solicitudDA.PasoUE_TramiteSolicitud(idSolConcesion, idUsuario);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
            
        }

        public bool CambiarTipoSolicitudRel(int idTramiteRelocalizacion,int nuevoTipotramite)
        {
            try
            {
                return tramiteRelocalizacionDA.CambiarTipoTramiteRelocalizacion(idTramiteRelocalizacion, nuevoTipotramite);
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
