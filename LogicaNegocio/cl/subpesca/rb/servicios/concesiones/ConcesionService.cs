using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.resolucion;

namespace LogicaNegocio.cl.subpesca.rb.servicios.concesiones
{
    public class ConcesionService
    {
        UnidadEspacialDA unidadEspacialDA = new UnidadEspacialDA();
        SolicitudDA solicitudDA = new SolicitudDA();
        ResolucionDA resolucionDA = new ResolucionDA();

        Logger logger = new Logger();


        //BUSCA CONCESIONES EN BASE A UN CODIGO O NOMBRE PARA EL AUTOCOMPLETAR
        public List<ParametroGenerico> buscarConcesiones(String prefixText)
        {
            try
            {
                return unidadEspacialDA.ListaUnidadesEspacialesCentro(prefixText);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        //OBTIENE UN CENTRO EN BASE A SU CODIGO SIEP
        public SolicitudConcesion ObtieneConcesionExistente(string codigoSiep, int idTipoTramite) {

            try
            {
                return solicitudDA.ObtieneConcesionExistente(codigoSiep, idTipoTramite);
                
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        
        
        }

        public SolicitudConcesion ObtieneUnidadEspacialExistente(string codigoSiep, int idTipoUnidEspacial)
        {

            try
            {
                return solicitudDA.Obtiene_UE_Existente(codigoSiep, idTipoUnidEspacial);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }


        }

        public bool cambiarVigenciaConcesion(Datos.Entidades.Resolucion.ResolucionSolicitud.ResolucionSolicitud resolucionSolicitud, int idUsuario)
        {
            try
            {
                /* Guardar Relación de Resolución y Concesión */
                if (!resolucionDA.GuardarResolucionSolicitud(resolucionSolicitud))
                {
                    return false;
                }
                
                /* Cambiar de Vigencia la Concesión */
                if (!solicitudDA.ActualizaSolicitud_Vigencia(resolucionSolicitud.solicitud.idSolConcesion, resolucionSolicitud.solicitud.estadoVigencia.id, idUsuario))
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

        public bool extenderPlazoVigencia(Datos.Entidades.Resolucion.ResolucionSolicitud.ResolucionSolicitud resolucionSolicitud, int idUsuario)
        {
            try
            {
                /* Guardar Relación de Resolución y Concesión */
                if (!resolucionDA.GuardarResolucionSolicitud(resolucionSolicitud))
                {
                    return false;
                }

                /* Cambiar los Plazos de Vigencia la Concesión */
                if (!unidadEspacialDA.ActualizarUnidadEspacial_Plazo(resolucionSolicitud.unidEspacial, idUsuario))
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
    }
}
