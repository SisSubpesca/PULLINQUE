using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Transactions;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Contantes;
using System.Data;

namespace LogicaNegocio.cl.subpesca.rb.servicios.cierreForzado
{
    public class CierreForzadoService
    {

        Logger logger = new Logger();
        SolicitudDA solicitudDA = new SolicitudDA();
        DocGralCierreForzadoDA docGralCierreForzadoDA = new DocGralCierreForzadoDA();
        RequerimientoService requerimientoService = new RequerimientoService();
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();



        //GUARDA UN CIERRE FORZADO
        //EL IT DE RECHAZO
        //COPIAS DEL IT DE RECHAZO EN CADA SOLICITUD CERRADA
        //UNA RELACION ENTRE LOS 2 DOCUMENTOS ANTERIORES
        public bool GuardarCierreForzado(Requerimiento cierreForzado, int idUsuario)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitud(cierreForzado.archivoAdjunto))
                    {
                        return false;
                    }


                    if (!docGralCierreForzadoDA.GuardarDocGralCierreForzado(cierreForzado)) {
                        return false;
                    }

                    Requerimiento requerimientoAux = null;

                    foreach (DocSolicitudCierre docSolicitudCierre in cierreForzado.solCierreForzado)
                    {
                        
                        //IT COPIA DEL ORIGINAL QUE SE LE ASOCIA A CADA SOLICITUD QUE SE CIERRA 
                        requerimientoAux = new Requerimiento();
                        requerimientoAux.solicitud = new SolicitudConcesion();
                        requerimientoAux.solicitud.idSolConcesion = docSolicitudCierre.idSolConcesion;
                        requerimientoAux.flujoDocumental = cierreForzado.flujoDocumental;
                        requerimientoAux.tipoEntrada = cierreForzado.tipoEntrada;
                        requerimientoAux.origen = cierreForzado.origen;
                        requerimientoAux.tipoDocumento = cierreForzado.tipoDocumento;
                        requerimientoAux.numero = cierreForzado.numero;
                        requerimientoAux.fecha = cierreForzado.fecha;
                        requerimientoAux.ambitoTipo = new List<DocumentoAmbito>();
                        requerimientoAux.ambitoTipo.Add(new DocumentoAmbito());

                        requerimientoAux.archivoAdjunto = new ArchivoBinario();
                        requerimientoAux.archivoAdjunto.idArchivo = cierreForzado.archivoAdjunto.idArchivo;

                        foreach (DocumentoAmbito doc in requerimientoAux.ambitoTipo)
                        {
                            doc.ambito = cierreForzado.ambitoTipo[0].ambito;
                            doc.tipo = cierreForzado.ambitoTipo[0].tipo;
                            doc.estadoResultadoResp = cierreForzado.estadoFinal;
                            doc.seccion = cierreForzado.ambitoTipo[0].seccion;
                            doc.estadoVigencia = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
                        }



                        //AL GUARDAR EL REQUERIMIENTO SE INTENTARA RECALCULAR EL ESTADO DE LA SOLICITUD, SIN EMBARGO, LA TABLA DE CIERRE FORZADO REQUIERE EL ID QUE SE ESTA INGRESANDO, POR LO 
                        //QUE NO PODRA IRSE POR LA RAMA DE CIERRE FORZADO, DEBEMOS RECALCULAR EL ESTADO MAS ABAJO.
                        if (!requerimientoService.guardarRequerimiento(requerimientoAux, idUsuario))
                        {
                            return false;
                        }



                        docSolicitudCierre.docITCierre = cierreForzado;
                        docSolicitudCierre.docPestanaIT = requerimientoAux.ambitoTipo[0];
                        docSolicitudCierre.flujoCierreForzado = true;
                        if (!docGralCierreForzadoDA.GuardarDocSolicitudCierre(docSolicitudCierre))
                        {
                            return false;
                        }


                        //RECALCULAR EL ESTADO DE LA SOLICITUD (COMO ES CIERRE FORZADO EL FLUJO ES CORTO)
                        if (!solicitudDA.TramiteRecalculaEstados(docSolicitudCierre.idSolConcesion))
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



        
        //GUARDA LA RESOLUCION SSP
        //COPIAS DEL RESOLUCION SSP DE RECHAZO EN CADA SOLICITUD CERRADA
        //UNA RELACION ENTRE LOS 2 DOCUMENTOS ANTERIORES
        public bool GuardarResolucionSSPCierreForzado(Requerimiento resolucionCierre, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {


                    if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitud(resolucionCierre.archivoAdjunto))
                    {
                        return false;
                    }


                    if (!docGralCierreForzadoDA.GuardarDocGralCierreForzado(resolucionCierre))
                    {
                        return false;
                    }



                    Requerimiento requerimientoAux = null;

                    foreach (DocSolicitudCierre docSolicitudCierre in resolucionCierre.solCierreForzado)
                    {

                        //IT COPIA DEL ORIGINAL QUE SE LE ASOCIA A CADA SOLICITUD QUE SE CIERRA 
                        requerimientoAux = new Requerimiento();
                        requerimientoAux.solicitud = new SolicitudConcesion();
                        requerimientoAux.solicitud.idSolConcesion = docSolicitudCierre.idSolConcesion;
                        requerimientoAux.flujoDocumental = resolucionCierre.flujoDocumental;
                        requerimientoAux.tipoEntrada = resolucionCierre.tipoEntrada;
                        requerimientoAux.origen = resolucionCierre.origen;
                        requerimientoAux.tipoDocumento = resolucionCierre.tipoDocumento;
                        requerimientoAux.numero = resolucionCierre.numero;
                        requerimientoAux.fecha = resolucionCierre.fecha;
                        requerimientoAux.numeroCI = resolucionCierre.numeroCI;
                        requerimientoAux.fechaCI = resolucionCierre.fechaCI;
                        
                        requerimientoAux.ambitoTipo = new List<DocumentoAmbito>();
                        requerimientoAux.ambitoTipo.Add(new DocumentoAmbito());

                        requerimientoAux.archivoAdjunto = resolucionCierre.archivoAdjunto;

                        foreach (DocumentoAmbito doc in requerimientoAux.ambitoTipo)
                        {
                            doc.ambito = resolucionCierre.ambitoTipo[0].ambito;
                            doc.tipo = resolucionCierre.ambitoTipo[0].tipo;
                            doc.estadoResultadoResp = resolucionCierre.estadoFinal;
                            doc.seccion = resolucionCierre.ambitoTipo[0].seccion;
                            doc.estadoVigencia = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
                        }


                        //AL GUARDAR EL REQUERIMIENTO SE INTENTARA RECALCULAR EL ESTADO DE LA SOLICITUD, SIN EMBARGO, LA TABLA DE CIERRE FORZADO REQUIERE EL ID QUE SE ESTA INGRESANDO, POR LO 
                        //QUE NO PODRA IRSE POR LA RAMA DE CIERRE FORZADO, DEBEMOS RECALCULAR EL ESTADO MAS ABAJO.
                        if (!requerimientoService.guardarRequerimiento(requerimientoAux, idUsuario))
                        {
                            return false;
                        }


                        docSolicitudCierre.docSSPCierre = resolucionCierre;
                        docSolicitudCierre.docPestanaSSP = requerimientoAux.ambitoTipo[0];


                        if (!docGralCierreForzadoDA.ActualizarDocSolicitudCierreSSP(docSolicitudCierre))
                        {
                            return false;
                        }


                        //RECALCULAR EL ESTADO DE LA SOLICITUD (COMO ES CIERRE FORZADO EL FLUJO ES CORTO)
                        if (!solicitudDA.TramiteRecalculaEstados(docSolicitudCierre.idSolConcesion))
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
         * BUSCA SOLICITUDES DE CONCESION PARA SER CERRADAS FORZOSAMENTE
         * LAS SOLICITUDES NO DEBEN ESTAR EN UN ESTADO FINAL
         * LAS SOLICITUDES DEBEN SER DE CONCESION  
         */
        public List<SolicitudConcesion> ListarSolicitudConcesionCierreForzado(SolicitudConcesion filtro) {

            try
            {
                return solicitudDA.ListarSolicitudCierreForzado(filtro);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        

        /**
        * BUSCA SOLICITUDES DE ACOPIO PARA SER CERRADAS FORZOSAMENTE
        * LAS SOLICITUDES NO DEBEN ESTAR EN UN ESTADO FINAL
        * LAS SOLICITUDES DEBEN SER DE ACOPIO  
        */
        public List<SolicitudConcesion> ListarSolicitudAcopioCierreForzado(SolicitudConcesion filtro)
        {

            try
            {
                return solicitudDA.ListarSolicitudCierreForzado(filtro);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        /**
        * BUSCA SOLICITUDES DE FAENAMIENTO PARA SER CERRADAS FORZOSAMENTE
        * LAS SOLICITUDES NO DEBEN ESTAR EN UN ESTADO FINAL
        * LAS SOLICITUDES DEBEN SER DE FAENAMIENTO  
        */
        public List<SolicitudConcesion> ListarSolicitudFaenamientoCierreForzado(SolicitudConcesion filtro)
        {

            try
            {
                return solicitudDA.ListarSolicitudCierreForzado(filtro);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        /**
        * BUSCA SOLICITUDES DE AMERB PARA SER CERRADAS FORZOSAMENTE
        * LAS SOLICITUDES NO DEBEN ESTAR EN UN ESTADO FINAL
        * LAS SOLICITUDES DEBEN SER DE AMERB  
        */
        public List<SolicitudConcesion> ListarSolicitudAmerbCierreForzado(SolicitudConcesion filtro)
        {

            try
            {
                return solicitudDA.ListarSolicitudCierreForzado(filtro);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        /**
        * BUSCA SOLICITUDES DE COLECTORES DE SEMILLA PARA SER CERRADAS FORZOSAMENTE
        * LAS SOLICITUDES NO DEBEN ESTAR EN UN ESTADO FINAL
        * LAS SOLICITUDES DEBEN SER DE COLECTORES DE SEMILLA  
        */
        public List<SolicitudConcesion> ListarSolicitudColectoresCierreForzado(SolicitudConcesion filtro)
        {

            try
            {
                return solicitudDA.ListarSolicitudCierreForzado(filtro);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        

        /**
        * BUSCA SOLICITUDES DE ECMPO PARA SER CERRADAS FORZOSAMENTE
        * LAS SOLICITUDES NO DEBEN ESTAR EN UN ESTADO FINAL
        */
        public List<SolicitudConcesion> ListarSolicitudECMPOCierreForzado(SolicitudConcesion filtro)
        {

            try
            {
                return solicitudDA.ListarSolicitudCierreForzado(filtro);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        /**
        * BUSCA SOLICITUDES DE Experimentales AMERB PARA SER CERRADAS FORZOSAMENTE
        * LAS SOLICITUDES NO DEBEN ESTAR EN UN ESTADO FINAL
        */
        public List<SolicitudConcesion> ListarSolicitudExperimentalesAmerbCierreForzado(SolicitudConcesion filtro)
        {

            try
            {
                return solicitudDA.ListarSolicitudCierreForzado(filtro);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }









        /**
         * Lista los informes usados para el cierre forzado
         */
        public DataTable ListarDocGralCierreIT(int numero, int idTipoUnidadEspacial, int idTipoTramite, string identificador)
        {

            try
            {
                return docGralCierreForzadoDA.ListarDocGralCierreIT(numero, idTipoUnidadEspacial, idTipoTramite, identificador);
                
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        //OBTIENE LA INFORMACION DE UN INFORME TECNICO DE CIERRE
        public Requerimiento ObtieneInformeTecnicoCierre(int idDocCierre)
        {
            try
            {
                return docGralCierreForzadoDA.ObtieneInformeTecnicoCierre(idDocCierre);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        
        }


        //LISTA LAS SOLICITUDES QUE SE CERRARON CON UN INFORME (PARA ASOCIALES LA SSP DE CIERRE)
        public List<SolicitudConcesion> ListarDocGralCierre_SolicitudesIT(int idDocITCierre)
        {
            try
            {
                return solicitudDA.ListarDocGralCierre_SolicitudesIT(idDocITCierre);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }




        //ELIMINA FISICAMENTE UN CIERRE FORZADO CON TODAS SUS DEPENDENCIAS
        public bool EliminarDocGralCierreForzado(int idDocCierre)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {


                    if (!docGralCierreForzadoDA.EliminarDocGralCierreForzado(idDocCierre))
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
