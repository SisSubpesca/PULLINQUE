using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Entidades;

namespace LogicaNegocio.cl.subpesca.rb.servicios.solicitudes
{
    public class InformacionSolicitudService
    {

        Logger logger = new Logger();
        SolicitudDA solicitudDA = new SolicitudDA();

        public bool tieneEspeciesExperimentalesSolicitud(int idSolConcesion)
        {
            try
            {
                return solicitudDA.tieneEspeciesExperimentalesSolicitud(idSolConcesion);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }

        /**
         * RETORNA LA INFORMACION DE LA SOLICITUD QUE SE MUESTRA EN LA CABECERA DE LAS PAGINAS
         */ 
        public HeaderSolicitudConcesion ObtieneSolicitudConcesionHeader(int idSolConcesion) {

            try
            {
                return solicitudDA.ObtieneSolicitudConcesionHeader(idSolConcesion);
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
