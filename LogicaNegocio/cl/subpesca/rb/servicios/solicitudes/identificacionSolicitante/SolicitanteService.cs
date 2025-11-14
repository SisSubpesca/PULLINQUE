using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Transactions;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using System.Data;
using Datos.Entidades;

namespace LogicaNegocio.cl.subpesca.rb.servicios.solicitudes
{
    public class SolicitanteService
    {

        Logger logger = new Logger();
        SolicitanteDA solicitanteDA = new SolicitanteDA();
        OperadorDA operadorDA = new OperadorDA();
        PersonaDA personaDA = new PersonaDA();
        RepLegalDA representanteLegalDA = new RepLegalDA();

        /**
         * Método que guarda la información del o los solicitantes
         * asociados a una solicitud de concesión de acuicultura.
         */
        public bool guardarSolicitante(Datos.Entidades.Solicitante solicitante, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    //GUARDAR SOLICITANTE
                    if (!solicitanteDA.GuardarSolicitante(solicitante, idUsuario))
                    {
                        return false;
                    }

                    transactionScope.Complete();
                    return true;

                }catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }


        /**
        * Método que elimina la información del o los solicitantes
        * asociados a una solicitud de concesión de acuicultura.
        */
        public bool eliminarSolicitante(Datos.Entidades.Solicitante solicitante, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    //GUARDAR SOLICITANTE
                    if (!solicitanteDA.EliminarSolicitante(solicitante, idUsuario))
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
         * Método que recupera los datos del Solicitante presente en la BD de Subpesca.
         * Número Registro Subpesca, Fecha Registro Subpesca y boolean que india si está
         * registrado.
         */
        public Datos.Entidades.Solicitante VerPersona(int rutSolicitante, int tipoPersona)
        {
            try
            {
                Datos.Entidades.Solicitante solicitante = solicitanteDA.ObtenerPersona(rutSolicitante, tipoPersona);
                return solicitante;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public Datos.Entidades.Solicitante VerSolicitante(int idSolicitud, int rutPersona, string keysort, int idEstadoAsociacion)
        {
            try
            {
                Datos.Entidades.Solicitante solicitante = solicitanteDA.ObtenerSolicitante(idSolicitud, rutPersona, keysort, idEstadoAsociacion);
                return solicitante;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public Datos.Entidades.Solicitante existeSolicitante(string idSolicitud, int rutPersona, int idEstadoAsociacion)
        {
            try
            {
                Datos.Entidades.Solicitante solicitante = solicitanteDA.existeSolicitante(idSolicitud, rutPersona, idEstadoAsociacion);
                if (solicitante != null)
                {
                    return solicitante;
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        /**
         * Método que recupera las direcciones y contacto de la matriz y sucursales de una Persona.
         */
        public DataTable VerContactoMatrizSucursales(int rutSolicitante, int idContacto)
        {
            try
            {
                return solicitanteDA.ObtenerContactoMatrizSucursales(rutSolicitante, idContacto); 
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        /**
         * Método que recupera los representantes legales de una Persona Jurídica.
         */
        //public DataTable VerRepresentantesLegales(int rutSolicitante, int rutRepresentanteLegal)
        public List<RepLegal> VerRepresentantesLegales(int rutSolicitante, int rutRepresentanteLegal)
        {
            try
            {
                return representanteLegalDA.ListarRepresentanteTitular(rutSolicitante);
                //return solicitanteDA.ObtenerRepresentantesLegales(rutSolicitante, rutRepresentanteLegal);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        /**
         * Método que recupera los archivos adjuntos de una Persona.
         */ 
        public DataTable VerArchivosAdjuntos(int rutPersona, int idArchivoAdjunto)
        {
            try
            {
                return solicitanteDA.ObtenerArchivosAdjuntoPersona(rutPersona, idArchivoAdjunto);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        /**
         * Método que obtiene los nombres por los que ha ido mutando una
         * persona.
         */ 
        public DataTable VerNombresPersona(int rutPersona, int estadoNombrePersona)
        {
            try
            {
                return solicitanteDA.ObtenerNombresPersona(rutPersona, estadoNombrePersona);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        public List<Operador> VerOperadores(int rutPersona, int rutOperador)
        {

            try
            {
                return operadorDA.ListarOperadorTitular(rutPersona, 0);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        /**
         *  RETORNAR LOS TITULARES QUE APLICAN EN UNA RELOCALIZACION
         *  SI ES FUSIONA SON LOS TITULARES DEL CENTRO DE ORIGEN
         *  SI ES SECTOR 0 SON LOS TITULARES DEL SECTOR 0
         *  SI ES CREA SON LOS TITULARES DE LOS CENTROS DE ORIGENES
         **/
        public List<Solicitante> ListarTitularesDetalleSector(int idSolicitud){

            try
            {
                return solicitanteDA.ListarTitularesDetalleSector(idSolicitud);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        /**
        *  INDICA SI UN TITULAR ES APE
        **/
        public bool TiularEsAPE(int rutPersona)
        {
            try
            {
                return solicitanteDA.TiTularEsAPE(rutPersona);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }


        /**
         * INDICA SI UN TITULAR ES RPA 
         **/

        public bool TitularesRPA(int rutPersona)
        {
            try
            {
                return solicitanteDA.titularPerteneceRPA(rutPersona);
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
