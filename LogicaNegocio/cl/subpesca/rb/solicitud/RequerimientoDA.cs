using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Datos.Entidades;
using Datos.AccesoDatos;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Data.SqlClient;

namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class RequerimientoDA
    {

        Logger logger = new Logger();


        /**
         * Guarda un requerimiento (Documento general), debe ir asociado a una solicitud de concesión.
         **/
        public bool GuardarRequerimiento(Requerimiento requerimiento, int idUsuario)
        {

            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbDocumentosGenerales";
                cnn.parametros.Add("@idDocGeneral", requerimiento.idRequerimiento);
                cnn.parametros.Add("@idSolConcesion", requerimiento.solicitud.idSolConcesion);
                cnn.parametros.Add("@idTipoFlujoDocumental", requerimiento.flujoDocumental.id);

                if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
                {
                    cnn.parametros.Add("@idTipoIO", requerimiento.tipoSalida.id);
                }

                if (requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
                {
                    cnn.parametros.Add("@idTipoIO", requerimiento.tipoEntrada.id);
                }

                cnn.parametros.Add("@idTipoDocumento", requerimiento.tipoDocumento.id);

                if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
                {
                    cnn.parametros.Add("@idTipoTrayecto", requerimiento.destinatario.id);
                }
                if (requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
                {
                    cnn.parametros.Add("@idTipoTrayecto", requerimiento.origen.id);
                }



                if (requerimiento.archivoAdjunto != null && requerimiento.archivoAdjunto.idArchivo > 0)
                {
                    cnn.parametros.Add("@idArchivoBinSC", requerimiento.archivoAdjunto.idArchivo);
                }


                //cnn.parametros.Add("@idResponsableDAC", );
                //cnn.parametros.Add("@idTipoPlazo", );
                //cnn.parametros.Add("@idEstadoResultResp",);

                if (requerimiento.numero != null && !requerimiento.numero.Equals(""))
                {
                    cnn.parametros.Add("@numero", requerimiento.numero);
                }
                if (requerimiento.fecha != null && requerimiento.fecha != default(DateTime))
                {
                    cnn.parametros.Add("@fecha", requerimiento.fecha);
                }

                if (requerimiento.numeroCI > 0)
                {
                    cnn.parametros.Add("@numeroCI", requerimiento.numeroCI);
                }

                if (requerimiento.fechaCI != null && requerimiento.fechaCI != default(DateTime))
                {
                    cnn.parametros.Add("@fechaCI", requerimiento.fechaCI);
                }
                if (requerimiento.estadoFinal!=null && requerimiento.estadoFinal.id>0)  
                {
                    cnn.parametros.Add("@idEstadoFinal", requerimiento.estadoFinal.id);
                }
                if (requerimiento.idReqPrincipal > 0)
                {
                    cnn.parametros.Add("@idDocGralPrincipal", requerimiento.idReqPrincipal);
                }
                cnn.parametros.Add("@recursoReposicion", requerimiento.esReposicion);

                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }



                DataTable dt = cnn.Execute();
                requerimiento.idRequerimiento = Convert.ToInt32(dt.Rows[0]["idDocGeneral"]);

                return true;
            }
            catch(Exception ex) {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }




      
        /**
         * guarda un "documento asociado" a un requerimiento (ambito y tipo)
         */
        public bool GuardarDocumentoAsociado(DocumentoAmbito documentoAmbito, int idUsuario)
        {

            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbDocumentacionPestana";


                cnn.parametros.Add("@idSolicitud", documentoAmbito.idSolicitud);
                cnn.parametros.Add("@idDocPestana", documentoAmbito.idDocPestana);

                if (documentoAmbito.idRequerimiento > 0)
                {
                    cnn.parametros.Add("@idDocGeneral", documentoAmbito.idRequerimiento);
                }

                if (documentoAmbito.idDocGeneralResp > 0){
                    cnn.parametros.Add("@idDocGeneralResp", documentoAmbito.idDocGeneralResp);
                }

                if (documentoAmbito.ambito != null && documentoAmbito.ambito.id > 0)
                {
                    cnn.parametros.Add("@idPestana", documentoAmbito.ambito.id);
                }

                if (documentoAmbito.tipo != null && documentoAmbito.tipo.id > 0)
                {
                    cnn.parametros.Add("@idSubRequerimiento", documentoAmbito.tipo.id);
                }

                if (documentoAmbito.estadoResultadoResp != null && documentoAmbito.estadoResultadoResp.id > 0)
                {
                    cnn.parametros.Add("@idEstadoResultadoResp", documentoAmbito.estadoResultadoResp.id);
                }

                cnn.parametros.Add("@idEstadoVigencia", documentoAmbito.estadoVigencia.id);
                cnn.parametros.Add("@fechaInsercion", DateTime.Now);

                if (documentoAmbito.seccion != null && documentoAmbito.seccion.id > 0)
                {
                    cnn.parametros.Add("@idSeccion", documentoAmbito.seccion.id);
                }

                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }

                if (documentoAmbito.tipoResultadoSupeditado != null && documentoAmbito.tipoResultadoSupeditado.id > 0)
                {
                    cnn.parametros.Add("@idTipoSupeditado", documentoAmbito.tipoResultadoSupeditado.id);
                }

                DataTable dt = cnn.Execute();
                documentoAmbito.idDocPestana = Convert.ToInt32(dt.Rows[0]["idDocPestana"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }


        /**
         * Obtiene la lista de requerimientos asociados a una solicitud
         */
        public DataTable ListarRequerimiento(int IdSolicitud, int IdPestania, int idSeccion)
        {

            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDocumentacionPestana";
                cnn.parametros.Add("@idSolConcesion", IdSolicitud);
                if (IdPestania > 0)
                {
                    cnn.parametros.Add("@idPestana", IdPestania);
                }
                if (idSeccion > 0)
                {
                    cnn.parametros.Add("@idSeccion", idSeccion);
                }


                DataTable dt = cnn.Execute();
                return dt;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };
        }


        /**
          * Obtiene la lista de resoluciones y decretos asociados a una UE
          */
        public DataTable ListarResolucionesDecretos(int IdSolicitud, string numero, DateTime fecha, int idTipoDocumento, int idOrigen)
        {

            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDocumentacionPestanaConcesion";
                cnn.parametros.Add("@idSolConcesion", IdSolicitud);

                if (numero != null && !numero.Trim().Equals(""))
                {
                    cnn.parametros.Add("@numero", numero);
                }

                if (fecha != null && fecha != default(DateTime))
                {
                    cnn.parametros.Add("@fecha", fecha.ToString());
                }

                if (idTipoDocumento > 0)
                {
                    cnn.parametros.Add("@idTipoDocumento", idTipoDocumento);
                }

                if (idOrigen > 0)
                {
                    cnn.parametros.Add("@idOrigen", idOrigen);
                }
	

                DataTable dt = cnn.Execute();
                return dt;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };
        }


        /*
         *  Obtiene la lista de documentos generales a los cuales se les puede ingresar una entrada
         *  (deben ser del tipo salida y con respuesta)
         */
        public DataTable ListarRequerimientosIdSolicitud(int IdSolicitud, int idSeccion, int idTipoTrayecto, int idTipoDoc)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDocumentosGenerales";
                cnn.parametros.Add("@idSolConcesion", IdSolicitud);
                cnn.parametros.Add("@idTipoFlujoDocumental", rbTipo.SALIDA); 
                cnn.parametros.Add("@idTipoIO",  rbTipo.REQUERIMIENTO_CON_RESPUESTA);
                if (idSeccion > 0)
                {
                    cnn.parametros.Add("@idSeccion", idSeccion);
                }
                if (idTipoTrayecto > 0)
                {
                    cnn.parametros.Add("@idTipoTrayecto", idTipoTrayecto);
                }

                cnn.parametros.Add("@idEstadoVig", rbEstadosGenerales.VIGENTE);

                if (idTipoDoc > 0)
                {
                    cnn.parametros.Add("@idTipoDocumento", idTipoDoc);
                }

                DataTable dt = cnn.Execute();
                return dt;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };
        }


        public DataTable ListarRequerimientosIdSolicitudModificacion(int IdSolicitud, int idDocGeneral, int idSeccion)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDocumentosGeneralesModificar";
                cnn.parametros.Add("@idSolConcesion", IdSolicitud);
                cnn.parametros.Add("@idTipoFlujoDocumental", rbTipo.SALIDA);
                cnn.parametros.Add("@idTipoIO", rbTipo.REQUERIMIENTO_CON_RESPUESTA);
                cnn.parametros.Add("@idEstadoVig", rbEstadosGenerales.VIGENTE);
                cnn.parametros.Add("@idDocGeneral", idDocGeneral);
                if (idSeccion>0)
                {
                    cnn.parametros.Add("@idSeccion", idSeccion);
                }
                

                DataTable dt = cnn.Execute();
                return dt;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };
        }


        /**
       * Lista de documentacion pestaña incorporando el ambito y la respuesta al documento.
       */
        public List<DocumentoAmbito> ListarDocumentacionPestanaReqMod(int idDocGeneral, int idTipoDocumento)
        {
            try
            {

                List<DocumentoAmbito> resp = new List<DocumentoAmbito>();
                DocumentoAmbito docAmbito = null;
               
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDocumentacionPestanaReqMod";

                cnn.parametros.Add("@idDocGeneral", idDocGeneral);
                cnn.parametros.Add("@idTipoDocumento", idTipoDocumento);
               

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        docAmbito = new DocumentoAmbito();
                        docAmbito.idDocPestana = Convert.ToInt32(row["idDocPestana"]);
                        docAmbito.idRequerimiento = Convert.ToInt32(row["idDocGeneral"]);
                        if (!row.IsNull("idDocGeneralResp"))
                        {
                            docAmbito.idDocGeneralResp = Convert.ToInt32(row["idDocGeneralResp"]);
                        }
                        docAmbito.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), row["nombrePestana"].ToString());
                        docAmbito.tipo = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["nombreSubRequerimiento"].ToString());

                        if (!row.IsNull("idSeccion"))
                        {
                            docAmbito.seccion = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]));
                        }

                        if (!row.IsNull("idEstadoResultadoResp"))
                        {
                            docAmbito.estadoResultadoResp = new ParametroGenerico(Convert.ToInt32(row["idEstadoResultadoResp"]), row["nombreEstado"].ToString());
                        }

                        resp.Add(docAmbito);
                    }
                }
                return resp;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        /*
         * Lista los documentos pedidos en un requerimiento. 
         */
        public List<DocumentoAmbito> ListarRequerimientosIdRequerimiento(int IdRequerimiento)
        {

            try{


                List<DocumentoAmbito> resp = new List<DocumentoAmbito>();
                DocumentoAmbito documentoAmbito = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDocumentacionPestanaReq";
                cnn.parametros.Add("@idDocGeneral", IdRequerimiento);

                //cnn.parametros.Add("@idEstadoVigencia", 0);

                DataTable dt = cnn.Execute();


                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        documentoAmbito = new DocumentoAmbito();
                        documentoAmbito.idDocPestana = Convert.ToInt32(row["idDocPestana"]);
                        documentoAmbito.idRequerimiento = Convert.ToInt32(row["idDocGeneral"]);
                        documentoAmbito.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), row["NombrePestana"].ToString());
                        documentoAmbito.tipo = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["NombreSubRequerimiento"].ToString());


                        resp.Add(documentoAmbito);
                    }
                }

                return resp;
              
            }catch(Exception ex){

                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
              
              }
          }
        

        /**
         * Lista las posibles respuestas, si aplica, en un documento pedido en un requerimiento
         */
        public DataTable ListarPosiblesRespuestasSubRequerimiento(int IdSubRequerimiento, int idTipoUE)
        {

            try{
              
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbEstadoSubRequerimiento_Req";
                cnn.parametros.Add("@idSubRequerimiento", IdSubRequerimiento);

                if (idTipoUE > 0) {
                    cnn.parametros.Add("@idTipoUE", idTipoUE);
                }
                

                //cnn.parametros.Add("@idEstadoVigencia", 0);
                
                

                DataTable dt = cnn.Execute();
                return dt;
              
            }catch(Exception ex){

                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
              
              }
          
          }



        /*
     * Guarda el estado de un requerimiento para una determinada pestaña
     * Final: Conforme o no Conforme
     * Vigente: Vigente o no Vigente
     */
        public bool GuardarEstadoFinalReqPestana(int idDocGeneral, int idPestana)
        {

            try
            {

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paInsRbEstadoFinalReqPestana";
                cnn.parametros.Add("@idDocGeneral", idDocGeneral);
                cnn.parametros.Add("@idPestana", idPestana);
                cnn.parametros.Add("@idEstadoVig", rbEstadosGenerales.VIGENTE); //TODOS ENTRAN COMO VIGENTES
                cnn.parametros.Add("@valorAux", rbEstadosGenerales.CERO); //0 = ES UN NUEVO REGISTRO


                DataTable dt = cnn.Execute();


                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        

        /**
        * Obtiene un requerimiento de salida
        */
        public DataTable ObtieneRequerimientoDeSalida(int idRequerimiento)
        {

            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbRequerimiento";
                cnn.parametros.Add("@idDocGeneral", idRequerimiento);
                cnn.parametros.Add("@idTipoFlujoDocumental", rbTipo.SALIDA);
                cnn.parametros.Add("@idEstado", rbEstadosGenerales.VIGENTE);
                

                DataTable dt = cnn.Execute();
                return dt;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }

        }

        public DataTable ObtieneRequerimientoDeSalidaSinEstado(int idRequerimiento)
        {

            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbRequerimiento";
                cnn.parametros.Add("@idDocGeneral", idRequerimiento);
                cnn.parametros.Add("@idTipoFlujoDocumental", rbTipo.SALIDA);
                

                DataTable dt = cnn.Execute();
                return dt;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }

        }

        /**
        * Obtiene un requerimiento de entrada
        */
        public DataTable ObtieneRequerimientoDeEntrada(int idRequerimiento)
        {

            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbRespuesta";
                cnn.parametros.Add("@idDocGeneral", idRequerimiento);
                cnn.parametros.Add("@idTipoFlujoDocumental", rbTipo.ENTRADA);



                DataTable dt = cnn.Execute();
                return dt;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }

        }


        public Requerimiento ObtenerRequerimiento(int idRequerimiento)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbRequerimiento";
                cnn.parametros.Add("@idDocGeneral", idRequerimiento);
                cnn.parametros.Add("@idTipoFlujoDocumental", rbTipo.SALIDA);
                cnn.parametros.Add("@idEstado", rbEstadosGenerales.VIGENTE);


                DataTable dt = cnn.Execute();
                Requerimiento requerimiento = null;
                DocumentoAmbito documentoAmbito = null;
                int idDocGeneralAux = 0;
                int index = 0;

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        int idDocGeneral =  Convert.ToInt32(row["idDocGeneral"]);
                        if (idDocGeneralAux != idDocGeneral) { 
                            
                            requerimiento = new Requerimiento();
                            requerimiento.solicitud = new SolicitudConcesion();
                            requerimiento.solicitud.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            requerimiento.idRequerimiento = idDocGeneral;
                            requerimiento.ambitoTipo = new List<DocumentoAmbito>();
                            requerimiento.flujoDocumental = new ParametroGenerico(Convert.ToInt32(row["idTipoFlujoDocumental"]), row["nombreTipoFlujoDoc"].ToString());
                            requerimiento.tipoSalida = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), row["nombreTipoIO"].ToString());
                            requerimiento.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDoc"].ToString());
                            requerimiento.destinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoTrayecto"]), row["nombreTipoTray"].ToString());

                            if (!row.IsNull("numero"))
                            {
                                requerimiento.numero = row["numero"].ToString();
                            }

                            if (!row.IsNull("fecha"))
                            {
                                requerimiento.fecha = Convert.ToDateTime(row["fecha"]);
                            }

                            if (!row.IsNull("fechaNuevoVencimiento"))
                            {
                                requerimiento.nuevaFecha = Convert.ToDateTime(row["fechaNuevoVencimiento"]);
                            }

                            requerimiento.archivoAdjunto = new ArchivoBinario();

                            if (!row.IsNull("idArchivoBinSC"))
                            {
                                requerimiento.archivoAdjunto.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                                requerimiento.archivoAdjunto.nombreArchivo = row["nombreFisico"].ToString();
                            }
                            if (!row.IsNull("idEstadoFinal"))
                            {
                                requerimiento.estadoFinal = new ParametroGenerico(Convert.ToInt32(row["idEstadoFinal"]));
                            }

                            
                        }

                        documentoAmbito = new DocumentoAmbito();
                        documentoAmbito.index = index;
                        documentoAmbito.accion = accion.LISTADO;
                        documentoAmbito.idDocPestana = Convert.ToInt32(row["idDocPestana"]);
                        documentoAmbito.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), row["NombrePestana"].ToString());
                        documentoAmbito.tipo = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["NombreSubRequerimiento"].ToString());
                        documentoAmbito.seccion = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]));
                        documentoAmbito.ambitoAntiguo   = documentoAmbito.ambito;
                        documentoAmbito.tipoAntiguo     = documentoAmbito.tipo;


                        if (!row.IsNull("idDocGeneralResp"))
                        {
                            documentoAmbito.idDocGeneralResp = Convert.ToInt32(row["idDocGeneralResp"]);
                        }
                        
                        requerimiento.ambitoTipo.Add(documentoAmbito);

                        idDocGeneralAux = idDocGeneral;
                        index++;
                    }
                }

                return requerimiento;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public Requerimiento ObtenerRequerimientoSinEstado(int idRequerimiento)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbRequerimiento";
                cnn.parametros.Add("@idDocGeneral", idRequerimiento);
                cnn.parametros.Add("@idTipoFlujoDocumental", rbTipo.SALIDA);
                

                DataTable dt = cnn.Execute();
                Requerimiento requerimiento = null;
                DocumentoAmbito documentoAmbito = null;
                int idDocGeneralAux = 0;
                int index = 0;

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        int idDocGeneral = Convert.ToInt32(row["idDocGeneral"]);
                        if (idDocGeneralAux != idDocGeneral)
                        {

                            requerimiento = new Requerimiento();
                            requerimiento.solicitud = new SolicitudConcesion();
                            requerimiento.solicitud.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            requerimiento.idRequerimiento = idDocGeneral;
                            requerimiento.ambitoTipo = new List<DocumentoAmbito>();
                            requerimiento.flujoDocumental = new ParametroGenerico(Convert.ToInt32(row["idTipoFlujoDocumental"]), row["nombreTipoFlujoDoc"].ToString());
                            requerimiento.tipoSalida = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), row["nombreTipoIO"].ToString());
                            requerimiento.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDoc"].ToString());
                            requerimiento.destinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoTrayecto"]), row["nombreTipoTray"].ToString());

                            if (!row.IsNull("numero"))
                            {
                                requerimiento.numero = row["numero"].ToString();
                            }

                            if (!row.IsNull("fecha"))
                            {
                                requerimiento.fecha = Convert.ToDateTime(row["fecha"]);
                            }

                            if (!row.IsNull("fechaNuevoVencimiento"))
                            {
                                requerimiento.nuevaFecha = Convert.ToDateTime(row["fechaNuevoVencimiento"]);
                            }

                            requerimiento.archivoAdjunto = new ArchivoBinario();

                            if (!row.IsNull("idArchivoBinSC"))
                            {
                                requerimiento.archivoAdjunto.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                                requerimiento.archivoAdjunto.nombreArchivo = row["nombreFisico"].ToString();
                            }
                            if (!row.IsNull("idEstadoFinal"))
                            {
                                requerimiento.estadoFinal = new ParametroGenerico(Convert.ToInt32(row["idEstadoFinal"]));
                            }

                        }

                        documentoAmbito = new DocumentoAmbito();
                        documentoAmbito.index = index;
                        documentoAmbito.accion = accion.LISTADO;
                        documentoAmbito.idDocPestana = Convert.ToInt32(row["idDocPestana"]);
                        documentoAmbito.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), row["NombrePestana"].ToString());
                        documentoAmbito.tipo = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["NombreSubRequerimiento"].ToString());
                        documentoAmbito.seccion = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]));

                        documentoAmbito.ambitoAntiguo = documentoAmbito.ambito;
                        documentoAmbito.tipoAntiguo = documentoAmbito.tipo;


                        if (!row.IsNull("idDocGeneralResp"))
                        {
                            documentoAmbito.idDocGeneralResp = Convert.ToInt32(row["idDocGeneralResp"]);
                        }

                        requerimiento.ambitoTipo.Add(documentoAmbito);

                        idDocGeneralAux = idDocGeneral;
                        index++;
                    }
                }

                return requerimiento;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        public Requerimiento ObtenerRespuesta(int idRequerimiento)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbRespuesta";
                cnn.parametros.Add("@idDocGeneral", idRequerimiento);
                cnn.parametros.Add("@idTipoFlujoDocumental", rbTipo.ENTRADA);


                DataTable dt = cnn.Execute();
                Requerimiento requerimiento = null;
                DocumentoAmbito documentoAmbito = null;
                int idDocGeneralAux = 0;

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        int  idDocGeneralResp= Convert.ToInt32(row["idDocGeneralResp"]);
                        if (idDocGeneralAux != idDocGeneralResp)
                        {

                            requerimiento = new Requerimiento();
                            requerimiento.solicitud = new SolicitudConcesion();
                            requerimiento.solicitud.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            requerimiento.idRequerimiento = idDocGeneralResp;
                            requerimiento.ambitoTipo = new List<DocumentoAmbito>();
                            requerimiento.flujoDocumental = new ParametroGenerico(Convert.ToInt32(row["idTipoFlujoDocumental"]), row["nombreTipoFlujoDoc"].ToString());
                            requerimiento.tipoEntrada = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), row["nombreTipoIO"].ToString());
                            requerimiento.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDoc"].ToString());
                            requerimiento.origen = new ParametroGenerico(Convert.ToInt32(row["idTipoTrayecto"]), row["nombreTipoTray"].ToString());


                            if (!row.IsNull("numero"))
                            {
                                requerimiento.numero = row["numero"].ToString();
                            }

                            if (!row.IsNull("fecha"))
                            {
                                requerimiento.fecha = Convert.ToDateTime(row["fecha"]);
                            }

                            if (!row.IsNull("numeroCI"))
                            {
                                requerimiento.numeroCI = Convert.ToInt32(row["numeroCI"]);
                            }

                            if (!row.IsNull("fechaCI"))
                            {
                                requerimiento.fechaCI = Convert.ToDateTime(row["fechaCI"]);
                            }

                            requerimiento.archivoAdjunto = new ArchivoBinario();

                            if (!row.IsNull("idArchivoBinSC"))
                            {
                                requerimiento.archivoAdjunto.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                                requerimiento.archivoAdjunto.nombreArchivo = row["nombreFisico"].ToString();
                            }

                            if (!row.IsNull("numeroRequerimiento"))
                            {
                                requerimiento.numeroReqSalida = row["numeroRequerimiento"].ToString();
                            }

                            if (!row.IsNull("idDocGeneral"))
                            {
                                requerimiento.idReqSalida = Convert.ToInt32(row["idDocGeneral"]);
                            }

                            if (!row.IsNull("idEstadoFinal"))
                            {
                                requerimiento.estadoFinal = new ParametroGenerico(Convert.ToInt32(row["idEstadoFinal"]));
                            }

                        }

                        documentoAmbito = new DocumentoAmbito();
                        documentoAmbito.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), row["NombrePestana"].ToString());
                        documentoAmbito.ambitoAntiguo = documentoAmbito.ambito;
                        documentoAmbito.idDocPestana = Convert.ToInt32(row["idDocPestana"]);
                        documentoAmbito.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
                             

                        if (!row.IsNull("idSubRequerimiento"))
                        {
                            documentoAmbito.tipo = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["NombreSubRequerimiento"].ToString());
                            documentoAmbito.tipoAntiguo = documentoAmbito.tipo;
                        }

                        if (!row.IsNull("idEstadoResultadoResp"))
                        {
                            documentoAmbito.estadoResultadoResp = new ParametroGenerico(Convert.ToInt32(row["idEstadoResultadoResp"]), row["nombreEstadoResultadoResp"].ToString());
                        }
                        if (!row.IsNull("idTipoSupeditado"))
                        {
                            documentoAmbito.tipoResultadoSupeditado = new ParametroGenerico(Convert.ToInt32(row["idTipoSupeditado"]), row["nombreTipoSupedita"].ToString());
                        }

                        requerimiento.ambitoTipo.Add(documentoAmbito);

                        idDocGeneralAux = idDocGeneralResp;
                    }
                }

                return requerimiento;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }



        //Elimina registros de la tabla RbDocumentacionPestana en base a su requerimiento y su respuesta
        public bool EliminarRbDocumentacionPestana(int idDocPestana, int idSolicitud, int idUsuario)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbDocumentacionPestana";
                cnn.parametros.Add("@idDocPestana", idDocPestana);
                cnn.parametros.Add("@idSolicitud", idSolicitud);
                cnn.parametros.Add("@idUsuario", idUsuario);
                                
                DataTable dt = cnn.Execute();

                return true;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }


        //Elimina registros de EstadoFinalReqPestana
        public bool EliminarEstadoFinalReqPestana(int idDocGeneral, int idPestana)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbEstadoFinalReqPestana";
                cnn.parametros.Add("@idDocGeneral", idDocGeneral);
                cnn.parametros.Add("@idPestana", idPestana);
                
                DataTable dt = cnn.Execute();

                return true;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }


        /**
        * guarda un Documento asociado a un requerimiento (ambito y idSubRequerimiento)
        */
        public bool ActualizarDocumentoAsociado(DocumentoAmbito documentoAmbito, int idUsuario)
        {

            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbDocumentacionPestana";

                cnn.parametros.Add("@idDocPestana", documentoAmbito.idDocPestana);

                if (documentoAmbito.ambito != null && documentoAmbito.ambito.id > 0)
                {
                    cnn.parametros.Add("@idPestana", documentoAmbito.ambito.id);
                }

                if (documentoAmbito.tipo != null && documentoAmbito.tipo.id > 0)
                {
                    cnn.parametros.Add("@idSubRequerimiento", documentoAmbito.tipo.id);
                }

                if (documentoAmbito.seccion != null && documentoAmbito.seccion.id > 0)
                {
                    cnn.parametros.Add("@idSeccion", documentoAmbito.seccion.id);
                }

                if (documentoAmbito.estadoResultadoResp != null && documentoAmbito.estadoResultadoResp.id > 0)
                {
                    cnn.parametros.Add("@idEstadoResultadoResp", documentoAmbito.estadoResultadoResp.id);
                }

                if (documentoAmbito.tipoResultadoSupeditado != null && documentoAmbito.tipoResultadoSupeditado.id > 0)
                {
                    cnn.parametros.Add("@idTipoSupeditado", documentoAmbito.tipoResultadoSupeditado.id);
                }
                cnn.parametros.Add("@idSolConcesion", documentoAmbito.idSolicitud);

                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }
                

                DataTable dt = cnn.Execute();
                documentoAmbito.idDocPestana = Convert.ToInt32(dt.Rows[0]["idDocPestana"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        /**
     * Actualiza un Documento asociado a un requerimiento (respuesta y estado de respuesta pasarán a null)
     */
        public bool ActualizarDocumentacionPestanaRequerimiento(int idDocGeneralResp, int idUsuario)
        {

            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbDocumentacionPestanaRequerimiento";

                cnn.parametros.Add("@idDocGeneralResp", idDocGeneralResp);
                cnn.parametros.Add("@idUsuario", idUsuario);

                DataTable dt = cnn.Execute();
                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        //Quita asociaciones del requerimiento y elimina referencia en la tabla rbDocumentosGenerales (si aplica) 
        public bool EliminarRequerimiento(int idDocGeneral, int idPestana, int idSolicitud, int idUsuario)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbRequerimiento";
                cnn.parametros.Add("@idDocGeneral", idDocGeneral);
                if (idPestana > 0)
                {
                    cnn.parametros.Add("@idPestana", idPestana);
                }
                cnn.parametros.Add("@idSolicitud", idSolicitud);
                cnn.parametros.Add("@idUsuario", idUsuario);

                DataTable dt = cnn.Execute();
                return true;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        //Quita asociaciones de respuesta en la tabla rbDocumentosGenerales (si aplica) 
        public bool EliminarRespuesta(int idDocGeneralResp, int idPestana, int idSolicitud, int idUsuario)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbRespuesta";
                cnn.parametros.Add("@idDocGeneralResp", idDocGeneralResp);
                if (idPestana > 0)
                {
                    cnn.parametros.Add("@idPestana", idPestana);
                }
                cnn.parametros.Add("@idSolicitud", idSolicitud);
                cnn.parametros.Add("@idUsuario", idUsuario);

                DataTable dt = cnn.Execute();
                return true;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        /**
     * Obtiene tipo de flujo documental asociado a un requerimiento
     */
        public int ObtieneFlujoDocumentoGeneral(int idDocGeneral)
        {
            try
            {
                int flujoDocumental =0;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDocumentosGeneralesI_O";
                cnn.parametros.Add("@idDocGeneral", idDocGeneral);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        flujoDocumental = Convert.ToInt32(row["idTipoFlujoDocumental"]);
                    }
                }

                return flujoDocumental;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return 0;
            }
        }



    //ACTUALIZA ESTADOS DEL REQUERIMIENTO
     public bool ActualizarRequerimientoEstado(int idDocGeneral, int idPestana, int idEstadoVigencia, int idUsuario)
     {
	    try{

              Conexion cnn = new Conexion();
              cnn.procedimiento = "paUpdRequerimientoEstado";
              cnn.parametros.Add("@idDocGeneral", idDocGeneral);
              if (idPestana > 0)
              {
                  cnn.parametros.Add("@idPestana", idPestana);
              }  
            
              cnn.parametros.Add("@idEstadoVigencia", idEstadoVigencia);
              cnn.parametros.Add("@idUsuario", idUsuario);
                  
              DataTable dt = cnn.Execute();
              
              return true;
             
        }catch (Exception ex){
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return false;
             };
    }

     //ACTUALIZA ESTADOS DE LA RESPUESTA
     public bool ActualizarRespuestaEstado(int idDocGeneral, int idPestana, int idEstadoVigencia, int idUsuario)
     {
         try
         {

             Conexion cnn = new Conexion();
             cnn.procedimiento = "paUpdRespuestaEstado";
             cnn.parametros.Add("@idDocGeneral", idDocGeneral);
             if (idPestana > 0)
             {
                 cnn.parametros.Add("@idPestana", idPestana);
             }
             cnn.parametros.Add("@idEstadoVigencia", idEstadoVigencia);
             if (idUsuario > 0)
             {
                 cnn.parametros.Add("@idUsuario", idUsuario);
             }
             

             DataTable dt = cnn.Execute();

             return true;

         }
         catch (Exception ex)
         {
             logger.PrintError(ex);
             logger.SendMailError(ex);
             return false;
         };
     }


     public bool ActualizarRequerimientoEstadoFinal(int idDocGeneral, int idEstadoFinal, int idSolicitud)
     {
         try
         {

             Conexion cnn = new Conexion();
             cnn.procedimiento = "paUpdRbDocGeneralesEstadoFinal";
             cnn.parametros.Add("@idDocGeneral", idDocGeneral);
             cnn.parametros.Add("@idEstadoFinal", idEstadoFinal);
             cnn.parametros.Add("@idSolicitud", idSolicitud);

             
             DataTable dt = cnn.Execute();
             
             return true;
         }
         catch (Exception ex)
         {
             logger.PrintError(ex);
             logger.SendMailError(ex);
             return false;
         };
     }


     public DataTable ListarRequerimientosIdSolicitudPrinc(int IdSolicitud, int idSeccion, int idTipoTrayecto, int idTipoDoc)
     {
         try
         {
             Conexion cnn = new Conexion();
             cnn.procedimiento = "paSelRbDocumentosGeneralesPrinc";
             cnn.parametros.Add("@idSolConcesion", IdSolicitud);
             cnn.parametros.Add("@idTipoFlujoDocumental", rbTipo.ENTRADA);
             //cnn.parametros.Add("@idTipoIO", rbTipo.INGRESO_SIN_REQUERIMIENTO);
             if (idSeccion > 0)
             {
                 cnn.parametros.Add("@idSeccion", idSeccion);
             }
             if (idTipoTrayecto > 0)
             {
                 cnn.parametros.Add("@idTipoTrayecto", idTipoTrayecto);
             }
             if (idTipoDoc > 0)
             {
                 cnn.parametros.Add("@idDocumento", idTipoDoc);
             }


             DataTable dt = cnn.Execute();
             return dt;
         }
         catch (Exception ex)
         {
             logger.PrintError(ex);
             logger.SendMailError(ex);
             return null;
         };
     }

     public SubRequerimientoReitera ObtieneSubReqReiteraPadre(int idSubReqReitera)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSubReqReitera";
                cnn.parametros.Add("@idSubReqReitera", idSubReqReitera);

                DataTable dt = cnn.Execute();
                SubRequerimientoReitera subRequerimientoReitera = null;

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        subRequerimientoReitera         = new SubRequerimientoReitera();
                        subRequerimientoReitera.padre   = new SubRequerimiento();
                        subRequerimientoReitera.reitera = new SubRequerimiento();

                        subRequerimientoReitera.padre.idSubRequerimiento = Convert.ToInt32(row["idSubReqPadre"]);
                        subRequerimientoReitera.padre.nombreSubRequerimiento = Convert.ToString(row["nombreSubReqPadre"]);

                        subRequerimientoReitera.reitera.idSubRequerimiento = Convert.ToInt32(row["idSubReqReitera"]);
                        subRequerimientoReitera.reitera.nombreSubRequerimiento = Convert.ToString(row["nombreSubReqReitera"]);

                        break;
                    }
                }

                return subRequerimientoReitera;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

     public DataTable ObtieneDocGralReiteraPadreBD(int idSolConcesion, int idSubReqReitera)
        {

            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDocGralReiteraPadreBD";
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                cnn.parametros.Add("@idSubReqReitera", idSubReqReitera);


                DataTable dt = cnn.Execute();
                return dt;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }

        }

     public bool ActualizarDocumentacionPestanaDocPrinc(int idSolConcesion, int idTipoDocumento, int idSubRequerimiento, int idEstadoVigencia, int idUsuario)
     {
         try
         {

             Conexion cnn = new Conexion();
             cnn.procedimiento = "paUpdRbDocumentacionPestanaDocPrinc";
             cnn.parametros.Add("@idSolConcesion", idSolConcesion);
             cnn.parametros.Add("@idTipoDocumento", idTipoDocumento);
             cnn.parametros.Add("@idSubRequerimiento", idSubRequerimiento);
             cnn.parametros.Add("@idEstadoVigencia", idEstadoVigencia);
             cnn.parametros.Add("@idUsuario", idUsuario);

             DataTable dt = cnn.Execute();
             
             return true;
         }
         catch (Exception ex)
         {
             logger.PrintError(ex);
             logger.SendMailError(ex);
             return false;
         };
     }


        //OBTIENE UN SUB-REQUERIMIENTO
     public SubRequerimiento ObtenerSubRequerimiento(SubRequerimiento subrequerimiento)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSubRequerimiento";
                 if(subrequerimiento.idSubRequerimiento>0){
                    cnn.parametros.Add("@idSubRequerimiento", subrequerimiento.idSubRequerimiento);
                 }
                 if(subrequerimiento.nombreSubRequerimiento!=null && !subrequerimiento.nombreSubRequerimiento.Equals("")){
                    cnn.parametros.Add("@nombreSubRequerimiento", subrequerimiento.nombreSubRequerimiento);
                 }
                 if (subrequerimiento.aplicaReiteraFiltro == 1 || subrequerimiento.aplicaReiteraFiltro == 0)
                 {
                     cnn.parametros.Add("@aplicaReitera", subrequerimiento.aplicaReiteraFiltro);
                 }
                 if (subrequerimiento.aplicaComplementarioFiltro == 1 || subrequerimiento.aplicaComplementarioFiltro == 0)
                 {
                     cnn.parametros.Add("@aplicaComplementario", subrequerimiento.aplicaComplementarioFiltro);
                 }
                 if (subrequerimiento.aplicaVisacionMasivaFiltro == 1 || subrequerimiento.aplicaVisacionMasivaFiltro == 0)
                 {
                     cnn.parametros.Add("@visacionMasiva", subrequerimiento.aplicaVisacionMasivaFiltro);
                 }
                 
                 DataTable dt = cnn.Execute();
                 SubRequerimiento subRequerimiento = null;
                
                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         subRequerimiento = new SubRequerimiento();
                         subRequerimiento.idSubRequerimiento = Convert.ToInt32(row["idSubRequerimiento"]);
                         subRequerimiento.nombreSubRequerimiento = Convert.ToString(row["nombreSubRequerimiento"]);

                         if (!row.IsNull("aplicaReitera"))
                         {
                             subRequerimiento.aplicaReitera = Convert.ToBoolean(row["aplicaReitera"]);
                         }
                         if (!row.IsNull("aplicaComplementario"))
                         {
                             subRequerimiento.aplicaComplementario = Convert.ToBoolean(row["aplicaComplementario"]);
                         }
                         if (!row.IsNull("visacionMasiva"))
                         {
                             subRequerimiento.aplicaVisacionMasiva = Convert.ToBoolean(row["visacionMasiva"]);
                         }

                     }
                 }

                 return subRequerimiento;

             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             }
         }




     //OBTIENE LAS SOLICITUDES DONDE SE ESTA UTLIZANDO UN DETERMINADO CONTROL DE INGRESO
     public List<SolicitudConcesion> ObtenerSolicitudPorCIusado(int idSolConcesion, int numeroCI, int anio)
     {
         try
         {

             Conexion cnn = new Conexion();
             cnn.procedimiento = "paSelRbDocumento_Solicitud";
             cnn.parametros.Add("@idSolConcesion", idSolConcesion);
             cnn.parametros.Add("@numeroCI", numeroCI);
             cnn.parametros.Add("@anio", anio);

             DataTable dt = cnn.Execute();
             List<SolicitudConcesion> solicitudes = new List<SolicitudConcesion>();
             SolicitudConcesion solicitudConcesion = null;
             int idSol = 0;
             int idSolAux = 0;
             
             if (dt != null)
             {

                 foreach (DataRow row in dt.Rows)
                 {

                     idSol = Convert.ToInt32(row["idSolConcesion"]);

                     if (idSol != idSolAux)
                     {

                        solicitudConcesion = new SolicitudConcesion();
                        solicitudConcesion.idSolConcesion = idSol;

                        if (!row.IsNull("numPert"))
                        {
                            solicitudConcesion.numPert = row["numPert"].ToString();
                        }  

                         idSolAux = idSol;
                         solicitudes.Add(solicitudConcesion);
                     }
                 }
             }

             return solicitudes;

         }
         catch (Exception ex)
         {
             logger.PrintError(ex);
             logger.SendMailError(ex);
             return null;
         }
     }



     //RETONAR LOS TEMAS QUE RESPONDIO UN DOCUMENTO PRINCIPAL ASOCIADO A UN REQUERIMIENTO DE SALIDA EN PARTICULAR 
        public List<DocumentoAmbito> buscarTemasContestadosDocumentoPrincipal(int idDocGeneral, int idDocPrincipal)
     {
         try
         {

             Conexion cnn = new Conexion();
             cnn.procedimiento = "paSelRbDocPestana_DocPrincipal";
             cnn.parametros.Add("@idDocGeneral", idDocGeneral);
             cnn.parametros.Add("@idDocPrincipal", idDocPrincipal);

             DataTable dt = cnn.Execute();
             List<DocumentoAmbito> list = new List<DocumentoAmbito>();
             DocumentoAmbito documentoAmbito = null;
             

             if (dt != null)
             {

                 foreach (DataRow row in dt.Rows)
                 {
                     documentoAmbito = new DocumentoAmbito();
                     
                     //NO SE OBTIENE LA CLAVE, PARA QUE AL INGRESAR EL COMPLEMENTARIO INGRESE UNA NUEVA RESPUESTA
                     documentoAmbito.idRequerimiento = Convert.ToInt32(row["idDocGeneral"]);
                     documentoAmbito.idDocGeneralResp = Convert.ToInt32(row["idDocGeneralResp"]);
                     documentoAmbito.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), Convert.ToString(row["nombrePestana"]));
                     documentoAmbito.tipo = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), Convert.ToString(row["nombreSubRequerimiento"]));
                     documentoAmbito.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]));
                     //LOS COMPLEMENTARIOS NO TIENEN RESULTADO
                     documentoAmbito.seccion = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]));

                     list.Add(documentoAmbito);
                     
                 }
             }

             return list;

         }
         catch (Exception ex)
         {
             logger.PrintError(ex);
             logger.SendMailError(ex);
             return null;
         }
     }

        public DocumentoAmbito obtenerDocPestanaSubRequerimiento(int idSolicitud, int idSubRequerimiento)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDocPestanaSubRequerimiento";
                cnn.parametros.Add("@idSolConcesion", idSolicitud);
                cnn.parametros.Add("@idSubRequerimiento", idSubRequerimiento);

                DataTable dt = cnn.Execute();
                DocumentoAmbito documentoAmbito = null;


                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        documentoAmbito = new DocumentoAmbito();

                        documentoAmbito.idDocPestana = Convert.ToInt32(row["idDocPestana"]);
                        documentoAmbito.idRequerimiento = Convert.ToInt32(row["idDocGeneral"]);
                        documentoAmbito.idDocGeneralResp = Convert.ToInt32(row["idDocGeneralResp"]); 
                        
                        
                        documentoAmbito.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), "");
                        documentoAmbito.tipo = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), "");
                        documentoAmbito.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]));

                        documentoAmbito.estadoResultadoResp = new ParametroGenerico(Convert.ToInt32(row["idEstadoResultadoResp"]), row["idEstadoResultadoResp"].ToString());
                        
                        
                    }
                }

                return documentoAmbito;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public bool ActualizarNoVigenteDocSolicitud(int idSolConcesion, int idUsuario)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbEstadoNoVigDocSolicitud";
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                cnn.parametros.Add("@idUsuario", idUsuario);
                
                DataTable dt = cnn.Execute();

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool ObtieneRequerimientoPlazoExtension(RequerimientoPlazo reqPlazo)
        {

            try
            {

                Conexion cnn = new Conexion();
                bool resp = false;

                cnn.procedimiento = "paSelRbSubRequerimientoPlazoExtension";

                if (reqPlazo.subReqDestino != null && reqPlazo.subReqDestino.id > 0)
                {
                    cnn.parametros.Add("@idSubReqDestino", reqPlazo.subReqDestino.id);//DOCUMENTO DONDE SE PIDE LA AMPLICACION
                }
                if (reqPlazo.subReqResuelve!=null  && reqPlazo.subReqResuelve.id > 0) //DOCUMENTO QUE TIENE LA NUEVA FECHA DE AMPLIACION
                {
                    cnn.parametros.Add("@idSubReqResuelve", reqPlazo.subReqResuelve.id);
                }
                cnn.parametros.Add("@idSolicitud", reqPlazo.idSolicitud); 
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        if (!row.IsNull("idDocPestana") && Convert.ToInt32(row["idDocPestana"]) > 0)
                        {
                            resp = true;
                        } 
                    }
                }

                return resp;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }

        }

        public bool GuardarPlazoDocumentoSolicitud(PlazoDocumentoSolicitud plazoDocSolicitud)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbPlazoDocumentoSolicitud";
                cnn.parametros.Add("@idPlazoDoc", plazoDocSolicitud.idPlazoDoc);
                cnn.parametros.Add("@idDocGeneral", plazoDocSolicitud.docOrigen.id);
                if (plazoDocSolicitud.docAmplia!=null && plazoDocSolicitud.docAmplia.id>0)
                {
                    cnn.parametros.Add("@idDocGeneralAmplia", plazoDocSolicitud.docAmplia.id);
                }
                cnn.parametros.Add("@fechaVencimientoInicial", plazoDocSolicitud.fechaVencimientoInicial);
                if (plazoDocSolicitud.fechaNuevoVencimiento != null && plazoDocSolicitud.fechaNuevoVencimiento != default(DateTime))
                {
                    cnn.parametros.Add("@fechaNuevoVencimiento", plazoDocSolicitud.fechaNuevoVencimiento);
                }
                

                DataTable dt = cnn.Execute();

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

         public bool existeRecursoReposicion(int idSolicitud)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDocsRecursoReposicion";
                cnn.parametros.Add("@idSolConcesion", idSolicitud);
                int recursoAux=0;
                bool presentaRec=false;
                bool resolucionRec=false;
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                     foreach (DataRow row in dt.Rows)
                    {
                         recursoAux = Convert.ToInt32(row["idSubRequerimiento"]);

                         if(recursoAux==129){ //Presenta Recurso de Reposición (129)
                            presentaRec=true;
                         }
                         if (recursoAux == 136 || recursoAux == 133) //Acoge a prueba Recurso de Reposición (136) -  Resolución Recurso (133)
                         {
                            resolucionRec=true;
                         }
                    }
                     if (presentaRec == true && resolucionRec == false)
                     {
                         return true;
                     }
                }

                return false;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }

         public List<SubRequerimiento> ListarSubRequerimiento(SubRequerimiento subrequerimiento)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSubRequerimiento";
                 if (subrequerimiento.idSubRequerimiento > 0)
                 {
                     cnn.parametros.Add("@idSubRequerimiento", subrequerimiento.idSubRequerimiento);
                 }
                 if (subrequerimiento.nombreSubRequerimiento != null && !subrequerimiento.nombreSubRequerimiento.Equals(""))
                 {
                     cnn.parametros.Add("@nombreSubRequerimiento", subrequerimiento.nombreSubRequerimiento);
                 }
                 if (subrequerimiento.aplicaReiteraFiltro == 1 || subrequerimiento.aplicaReiteraFiltro == 0)
                 {
                     cnn.parametros.Add("@aplicaReitera", subrequerimiento.aplicaReiteraFiltro);
                 }
                 if (subrequerimiento.aplicaComplementarioFiltro == 1 || subrequerimiento.aplicaComplementarioFiltro == 0)
                 {
                     cnn.parametros.Add("@aplicaComplementario", subrequerimiento.aplicaComplementarioFiltro);
                 }
                 if (subrequerimiento.aplicaVisacionMasivaFiltro == 1 || subrequerimiento.aplicaVisacionMasivaFiltro == 0)
                 {
                     cnn.parametros.Add("@visacionMasiva", subrequerimiento.aplicaVisacionMasivaFiltro);
                 }
                 
                 DataTable dt = cnn.Execute();

                 List<SubRequerimiento> resp = new List<SubRequerimiento>();
                 SubRequerimiento subRequerimiento = null;


                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                        subRequerimiento = new SubRequerimiento();
                         subRequerimiento.idSubRequerimiento = Convert.ToInt32(row["idSubRequerimiento"]);
                         subRequerimiento.nombreSubRequerimiento = Convert.ToString(row["nombreSubRequerimiento"]);

                         if (!row.IsNull("aplicaReitera"))
                         {
                             subRequerimiento.aplicaReitera = Convert.ToBoolean(row["aplicaReitera"]);
                         }
                         if (!row.IsNull("aplicaComplementario"))
                         {
                             subRequerimiento.aplicaComplementario = Convert.ToBoolean(row["aplicaComplementario"]);
                         }
                         if (!row.IsNull("visacionMasiva"))
                         {
                             subRequerimiento.aplicaVisacionMasiva = Convert.ToBoolean(row["visacionMasiva"]);
                         }
                         resp.Add(subRequerimiento);

                     }
                 }

                 return resp;

             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             }
         }

         public bool ActualizarRequerimientoPlazo(int idSolicitud, int idSubReqResuelve, int idDocGralResuelve, DateTime fechaNuevoVencimiento)
         {

             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paUpdRbRequerimientoPlazoExtension";


                 cnn.parametros.Add("@idSolicitud", idSolicitud);
                 cnn.parametros.Add("@idSubReqResuelve", idSubReqResuelve);
                 cnn.parametros.Add("@idDocGralResuelve", idDocGralResuelve);
                 cnn.parametros.Add("@fechaNuevoVencimiento", fechaNuevoVencimiento);
                 
                 DataTable dt = cnn.Execute();
                 idDocGralResuelve = Convert.ToInt32(dt.Rows[0]["idDocGralResuelve"]);

                 return true;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return false;
             };
         }

         public bool EliminarPlazoDocumentoSolicitud(int idPlazoDoc)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paDelRbPlazoDocumentoSolicitud";
                 cnn.parametros.Add("@idPlazoDoc", idPlazoDoc);
                 DataTable dt = cnn.Execute();

                 int resul = Convert.ToInt32(dt.Rows[0]["resultado"]);
                 if (resul >= 0) return true;

                 return false;

             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return false;
             };
         }

         public List<PlazoDocumentoSolicitud> ListarSubRequerimientoPlazo(PlazoDocumentoSolicitud subReqPlazo)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSubRequerimientoPlazo";
                 
                 if(subReqPlazo.idPlazoDoc>0){
                    cnn.parametros.Add("@idSubReqPlazo", subReqPlazo.idPlazoDoc);
                 }
                 if(subReqPlazo.docOrigen!=null && subReqPlazo.docOrigen.id>0){
                    cnn.parametros.Add("@idSubReqOrigen", subReqPlazo.docOrigen.id);
                 }
                 if(subReqPlazo.docAmplia!=null && subReqPlazo.docAmplia.id>0){
                     cnn.parametros.Add("@idSubReqDestino", subReqPlazo.docAmplia.id);
                 }
                 if (subReqPlazo.docResuelve != null && subReqPlazo.docResuelve.id > 0)
                 {
                     cnn.parametros.Add("@idSubReqResuelve", subReqPlazo.docResuelve.id);
                 }
                 if (subReqPlazo.tipoUE != null && subReqPlazo.tipoUE.id > 0)
                 {
                     cnn.parametros.Add("@idTipoUnidadEspacial", subReqPlazo.tipoUE.id);
                 }
                 if(subReqPlazo.plazoDias>=0){
                    cnn.parametros.Add("@plazoDias", subReqPlazo.plazoDias);
                 }
                 if(subReqPlazo.plazoMeses>=0){
                    cnn.parametros.Add("@plazoMeses", subReqPlazo.plazoMeses);
                 }


                 DataTable dt = cnn.Execute();
                 List<PlazoDocumentoSolicitud> list = new List<PlazoDocumentoSolicitud>();
                 PlazoDocumentoSolicitud docPlazo = null;


                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         docPlazo = new PlazoDocumentoSolicitud();

                         docPlazo.idPlazoDoc = Convert.ToInt32(row["idSubReqPlazo"]);
                         if (!row.IsNull("idSubReqOrigen"))
                         {
                             docPlazo.docOrigen = new ParametroGenerico(Convert.ToInt32(row["idSubReqOrigen"]), Convert.ToString(row["nombreSubReqOrigen"]));
                         }
                         if (!row.IsNull("idSubReqDestino"))
                         {
                             docPlazo.docAmplia = new ParametroGenerico(Convert.ToInt32(row["idSubReqDestino"]), Convert.ToString(row["nombreSubReqDestino"]));
                         }
                         if (!row.IsNull("idTipoUnidadEspacial"))
                         {
                             docPlazo.tipoUE = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidadEspacial"]), Convert.ToString(row["nombreTipo"]));
                         }
                         if (!row.IsNull("plazoDias"))
                         {
                             docPlazo.plazoDias = Convert.ToInt32(row["plazoDias"]);
                         }
                         if (!row.IsNull("plazoMeses"))
                         {
                             docPlazo.plazoMeses = Convert.ToInt32(row["plazoMeses"]);
                         }
                         list.Add(docPlazo);

                     }
                 }

                 return list;

             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             }
         }

         public DataTable GuardarSubRequerimientoPlazo(PlazoDocumentoSolicitud subReqPlazo)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paInsRbSubRequerimientoPlazo";

                 cnn.parametros.Add("@idSubReqPlazo", subReqPlazo.idPlazoDoc);
                 cnn.parametros.Add("@idSubReqOrigen", subReqPlazo.docOrigen.id);
                 if (subReqPlazo.docAmplia != null && subReqPlazo.docAmplia.id > 0){
                     cnn.parametros.Add("@idSubReqDestino", subReqPlazo.docAmplia.id);
                 }
                 cnn.parametros.Add("@idTipoUnidadEspacial", subReqPlazo.tipoUE.id);
                 cnn.parametros.Add("@plazoDias", subReqPlazo.plazoDias);
                 cnn.parametros.Add("@plazoMeses", subReqPlazo.plazoMeses);
                 if (subReqPlazo.docResuelve!= null && subReqPlazo.docResuelve.id > 0){
                     cnn.parametros.Add("@idSubReqResuelve", subReqPlazo.docResuelve.id);
                 }

                 DataTable dt = cnn.Execute();
                 return dt;

             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             };
         }

         public DataTable EliminarSubRequerimientoPlazo(int idSubReqPlazo)
         {
             Conexion cnn = new Conexion();
             cnn.procedimiento = "paDelRbSubRequerimientoPlazo";
             cnn.parametros.Add("@idSubReqPlazo", idSubReqPlazo);

             DataTable dt = cnn.Execute();
             return dt;
         }



    }

     


}
