using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.errores;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace LogicaNegocio.cl.subpesca.rb.servicios.estados
{
    public class EstadoService
    {

        Solicitudes solicitudes = new Solicitudes();
        SolicitudDA solicitudDA = new SolicitudDA();
        Logger logger = new Logger();




        /**
         *  Recalcula el estado de una solicitud 
         */
        public bool RecalcularEstadoSolicitud(int idSolConcesion)
        {

            try
            {
                return solicitudDA.GuardarEstadoSolicitud(idSolConcesion);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }



        /**
         * RETORNA LOS ESTADOS POR LOS CUALES HA PASADO UNA SOLICITUD
         **/
        public DataTable ListarHistEstadosSolicConces(int idSolConcesion) {

            try
            {
                return solicitudDA.ListarHistEstadosSolicConces(idSolConcesion);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        /*
         * RETORNA LOS POSIBLES ESTADOS DE UN TIPO DE SOLICITUD Y CUANTAS SOLICITUDES ESTAN EN CADA ESTADO (SOLO PARA SOLICITUDES EN TRAMITE)
         */
        public DataTable Solicitudes_Estados_Resumen_Listar_RB(int id_region, int id_provincia, int id_comuna, int id_tiposolicitud, int idUsuario, int id_usuarioSec , int id_estado, string numPert, bool check, bool check2)
        {

            try
            {

                if (check)
                {
                    return solicitudes.Solicitudes_Estados_Resumen_Listar_RB_ISLA(id_region, id_provincia, id_comuna, id_tiposolicitud, idUsuario, id_usuarioSec, id_estado, numPert, check2);
                }
                else 
                {
                    return solicitudes.Solicitudes_Estados_Resumen_Listar_RB(id_region, id_provincia, id_comuna, id_tiposolicitud, idUsuario, id_usuarioSec, id_estado, numPert, check2);
                }
            }
            catch (Exception ex) {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        /*
        * RETORNA LOS POSIBLES ESTADOS DE UN TIPO DE SOLICITUD Y CUANTAS SOLICITUDES ESTAN EN CADA ESTADO (SOLO PARA SOLICITUDES RECHAZADAS)
        */
        public DataTable Solicitudes_Estados_Rechazadas_Resumen_Listar_RB(int id_region, int id_provincia, int id_comuna, int id_tiposolicitud, int idUsuario, int id_usuarioSec, int id_estado, string numPert, bool check, bool check2)
        {

            try
            {
                if (check)
                {
                    return solicitudes.Solicitudes_Estados_Rechazadas_Resumen_Listar_RB_ISLA(id_region, id_provincia, id_comuna, id_tiposolicitud, idUsuario, id_usuarioSec, id_estado, numPert, check2);
                }
                else 
                {
                    return solicitudes.Solicitudes_Estados_Rechazadas_Resumen_Listar_RB(id_region, id_provincia, id_comuna, id_tiposolicitud, idUsuario, id_usuarioSec, id_estado, numPert, check2);
                }
                
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        /*
        * RETORNA LOS POSIBLES ESTADOS DE UN TIPO DE SOLICITUD Y CUANTAS SOLICITUDES ESTAN EN CADA ESTADO (SOLO PARA SOLICITUDES EN RECURSO DE REPOSICIÓN)
        */
        public DataTable Solicitudes_Estados_Reposicion_Resumen_Listar_RB(int id_region, int id_provincia, int id_comuna, int id_tiposolicitud, int idUsuario, int id_usuarioSec, int id_estado, string numPert, bool check2)
        {

            try
            {
                return solicitudes.Solicitudes_Estados_Reposicion_Resumen_Listar_RB(id_region, id_provincia, id_comuna, id_tiposolicitud, idUsuario, id_usuarioSec, id_estado, numPert, check2);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        /*
         * RETORNA LAS SOLICITUDES QUE ESTAN EN UN DETERMINADO ESTADO (Solicitudes en tramite)
         */
        public DataTable Solicitudes_Listar_RB(int id_estado, int id_region, int id_provincia, int id_comuna, int id_tiposolic, int id_usuario, int id_tipoSectorialista, string numPert, bool check, bool checkAvanzaAprueba)
        {

            try
            {
                if (check)
                {
                    return solicitudes.Solicitudes_Listar_RB_ISLAS(id_estado, id_region, id_provincia, id_comuna, id_tiposolic, id_usuario, id_tipoSectorialista, numPert, checkAvanzaAprueba);
                }
                else 
                {
                    return solicitudes.Solicitudes_Listar_RB(id_estado, id_region, id_provincia, id_comuna, id_tiposolic, id_usuario, id_tipoSectorialista, numPert, checkAvanzaAprueba);
                }

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        /*
        * RETORNA LAS SOLICITUDES QUE ESTAN EN UN DETERMINADO ESTADO (Solicitudes rechazadas)
        */
        public DataTable Solicitudes_Listar_Rechazadas_RB(int id_estado, int id_region, int id_provincia, int id_comuna, int id_tiposolic, int id_usuario, int id_tipoSectorialista, string numPert, bool check, bool checkAvanzaAprueba)
        {

            try
            {

                if (check)
                {
                    return solicitudes.Solicitudes_Listar_Rechazadas_RB_ISLA(id_estado, id_region, id_provincia, id_comuna, id_tiposolic, id_usuario, id_tipoSectorialista, numPert, checkAvanzaAprueba);
                }
                else
                {
                    return solicitudes.Solicitudes_Listar_Rechazadas_RB(id_estado, id_region, id_provincia, id_comuna, id_tiposolic, id_usuario, id_tipoSectorialista, numPert, checkAvanzaAprueba);
                }

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        /*
        * RETORNA LAS SOLICITUDES QUE ESTAN EN UN DETERMINADO ESTADO (Solicitudes en recurso de reposicion)
        */
        public DataTable Solicitudes_Listar_Reposicion_RB(int id_estado, int id_region, int id_provincia, int id_comuna, int id_tiposolic, int id_usuario, int id_tipoSectorialista, string numPert, bool checkAvanzaAprueba)
        {

            try
            {
                return solicitudes.Solicitudes_Listar_Reposicion_RB(id_estado, id_region, id_provincia, id_comuna, id_tiposolic, id_usuario, id_tipoSectorialista, numPert, checkAvanzaAprueba);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        /**
         ** OBTIENE UNA SOLICITUD POR SU ID PARA "VER"
         */
        public DataTable Solicitudes_Ver_RB(int id_solicitud)
        {

            try
            {
                return solicitudes.ObtieneSolicitudVer(id_solicitud);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        /**
         * LISTA LOS REQUERIMIENTOS CON PLAZOS VENCIDOS, INDICANDO CUANTAS SOLICITUDES TIENEN VENCIDO EL REQUERIMIENTO
         **/
        public DataTable Requerimientos_Vencidos_Listar_RB(int id_region, int id_provincia, int id_comuna, int id_tiposolicitud, int idUsuario, int idTipoTramite, int idSubTipoTramite, int id_tipodestinatario)
        {
            try
            {
                return solicitudes.Requerimientos_Vencidos_Listar_RB(id_region, id_provincia, id_comuna, id_tiposolicitud, idUsuario, idTipoTramite, idSubTipoTramite, id_tipodestinatario);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        /**
         * LISTA LOS REQUERIMIENTOS CON PLAZOS POR VENCER, INDICANDO CUANTAS SOLICITUDES TIENEN POR VENCER EL REQUERIMIENTO
         **/
        public DataTable Requerimientos_Por_Vencer_Listar_RB(int id_region, int id_provincia, int id_comuna, int id_tiposolicitud, int idUsuario, int idTipoTramite, int idSubTipoTramite, int id_tipodestinatario)
        {
            try
            {
                return solicitudes.Requerimientos_Por_Vencer_Listar_RB(id_region, id_provincia, id_comuna, id_tiposolicitud, idUsuario, idTipoTramite, idSubTipoTramite, id_tipodestinatario);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        /**
         * LISTA DE SOLICITUDES CON UN REQUERIMIENTO EN PARTICULAR QUE ESTA POR VENCER
         **/
        public DataTable Requerimientos_Por_Vencer_Ver_RB(int id_estado, int id_region, int id_provincia, int id_comuna, int id_tiposolicitud, int idUsuario, int idSubRequerimiento, int idTipoTramite, int idSubTipoTramite, int id_tipodestinatario)
        {

            try {

                return solicitudes.Requerimientos_Por_Vencer_Ver_RB(id_region, id_provincia, id_comuna, id_tiposolicitud, idUsuario, idSubRequerimiento, idTipoTramite, idSubTipoTramite, id_tipodestinatario);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }


        }


        /**
        * LISTA DE SOLICITUDES CON UN REQUERIMIENTO EN PARTICULAR QUE ESTA VENCIDO
        **/
        public DataTable Requerimientos_Vencidos_Ver_RB(int id_estado, int id_region, int id_provincia, int id_comuna, int id_tiposolicitud, int idUsuario, int idSubRequerimiento, int idTipoTramite, int idSubTipoTramite, int id_tipodestinatario)
        {

            try
            {
                return solicitudes.Requerimientos_Vencidos_Ver_RB(id_region, id_provincia, id_comuna, id_tiposolicitud, idUsuario, idSubRequerimiento, idTipoTramite, idSubTipoTramite, id_tipodestinatario);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }



        public DataTable Solicitudes_TipoSolicitud_Ver_RB(int id_tiposolic)
        {

            try
            {
                return solicitudes.ObtieneTipoSolicitud(id_tiposolic);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        public DataTable Solicitudes_Estados_Ver_RB(int id_estado)
        {

            try
            {
                return solicitudes.ObtieneEstadoSolicitud(id_estado);

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
