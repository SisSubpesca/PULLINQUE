using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.visacion;
using System.Transactions;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using Datos.Contantes;

namespace LogicaNegocio.cl.subpesca.rb.servicios.visacion
{
    public class VisacionService
    {

        Logger logger = new Logger();
        VisacionDA visacionDA = new VisacionDA();
        RequerimientoService requerimientoService = new RequerimientoService();


        /**
         *  Lista solicitudes para iniciar visaciones masivas en base a filtros
         */ 
        public List<VisacionMasiva> ListarInicioVisacionMasiva(VisacionMasiva filtro)
        {
            try
            {
                return visacionDA.ListarSolicitudInicioVisacion(filtro);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        /**
        * Lista Requerimientos para visar/firmar o corregir en base a filtros,
        * tipo = 1 => visaciones
        * tipo = 2 => firmas
        */
        public List<VisacionMasiva> ListarVisacionFirmaMasiva(VisacionMasiva filtro)
        {
            try
            {
                return visacionDA.ListarSolicitudVisacionFirma(filtro);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        /**
         *  GENERA UN INICIO DE VISACIÓN
         */ 
        public bool GenerarInicioVisacion(List<VisacionMasiva> iniciovisaciones, int idUsuario)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    Requerimiento requerimiento = null;

                    if (iniciovisaciones != null)
                    {

                        requerimiento = visacionDA.ObtenerDocumentoInicioVisacion(iniciovisaciones[0].tipoVisacion.id, iniciovisaciones[0].resultado == null ? 0 : iniciovisaciones[0].resultado.id, iniciovisaciones[0].tipoTramite.id);

                        foreach (VisacionMasiva inivioVisacion in iniciovisaciones)
                        {
                            requerimiento.idRequerimiento = 0;
                            requerimiento.ambitoTipo[0].idDocPestana = 0;

                            requerimiento.solicitud = new SolicitudConcesion();
                            requerimiento.solicitud.idSolConcesion = inivioVisacion.idSolConcesion;

                            if (!requerimientoService.guardarRequerimiento(requerimiento, idUsuario))
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
        *  GENERA UNA VISACIÓN, FIRMA O CORRECCION
        **/
        public bool GenerarVisacionesFirmasCorrecciones(List<VisacionMasiva> visaFirma, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    Requerimiento requerimiento = null;

                    if (visaFirma != null)
                    {


                        foreach (VisacionMasiva visacionFirma in visaFirma)
                        {

                            requerimiento = visacionDA.ObtenerDocumentoVisacionFirma(visacionFirma.tipoVisacion.id, visacionFirma.requerimiento.id, visacionFirma.resultado == null ? 0 : visacionFirma.resultado.id, visacionFirma.tipoTramite.id, visacionFirma.corrige, visacionFirma.idDocPestana);

                            requerimiento.solicitud = new SolicitudConcesion();
                            requerimiento.solicitud.idSolConcesion = visacionFirma.idSolConcesion;

                            if (!requerimientoService.guardarRequerimiento(requerimiento, idUsuario))
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
         * VALIDA LA EXISTENCIA DE UN GRUPO DE PERTS
         **/ 
        public string validarExistenciaPerts(VisacionMasiva visacionMasiva)
        {
            try
            {
                return visacionDA.ValidarExistenciaPerts(visacionMasiva);
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
