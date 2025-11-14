using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using Datos.AccesoDatos;
using Datos.Contantes;

namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class DocumentosConcesionDA
    {
        Logger logger = new Logger();

        public DocumentosConcesionDA()
        {}

        public bool GuardarDocumentosConcesion(DocumentosConcesion docConcesion)
            {
	            try{

                      Conexion cnn = new Conexion();
                      cnn.procedimiento = "paInsRbDocumentosConcesion";

                      if (docConcesion.idDocConcesion>0)
                      {
                          cnn.parametros.Add("@idDocConcesion", docConcesion.idDocConcesion);
                      }
                      cnn.parametros.Add("@idSolConcesion", docConcesion.solicitud.idSolConcesion);

                      if (docConcesion.flujoDocumental != null && docConcesion.flujoDocumental.id > 0)
                      {
                          cnn.parametros.Add("@idTipoFlujoDocumental", docConcesion.flujoDocumental.id);
                      }
                      if (docConcesion.flujoDocumental != null && docConcesion.flujoDocumental.id == rbTipo.SALIDA)
                      {
                          cnn.parametros.Add("@idTipoIO", docConcesion.tipoSalida.id);
                      }
                      if (docConcesion.flujoDocumental != null && docConcesion.flujoDocumental.id == rbTipo.ENTRADA)
                      {
                          cnn.parametros.Add("@idTipoIO", docConcesion.tipoEntrada.id);
                      }
                      if (docConcesion.tipoDocumento != null && docConcesion.tipoDocumento.id>0)
                      {
                          cnn.parametros.Add("@idTipoDocumento", docConcesion.tipoDocumento.id);
                      }
                      if (docConcesion.flujoDocumental != null && docConcesion.flujoDocumental.id == rbTipo.SALIDA)
                      {
                          cnn.parametros.Add("@idTipoDestinatario", docConcesion.destinatario.id);
                      }
                      if (docConcesion.flujoDocumental != null && docConcesion.flujoDocumental.id == rbTipo.ENTRADA)
                      {
                          cnn.parametros.Add("@idTipoDestinatario", docConcesion.origen.id);
                      }
                      if (docConcesion.archivoAdjunto != null && docConcesion.archivoAdjunto.idArchivo > 0)
                      {
                          cnn.parametros.Add("@idArchivoBinSC", docConcesion.archivoAdjunto.idArchivo);
                      }
                      if (docConcesion.estadoVigencia != null && docConcesion.estadoVigencia.id > 0)
                      {
                          cnn.parametros.Add("@idEstadoVigencia", docConcesion.estadoVigencia.id);
                      }
                      
                      cnn.parametros.Add("@nombreTema", docConcesion.nombreTema);
                      
                      if (docConcesion.numero != null && !docConcesion.numero.Equals(""))
                      {
                          cnn.parametros.Add("@numero", docConcesion.numero);
                      }
                      if (docConcesion.fecha != null && docConcesion.fecha != default(DateTime))
                      {
                          cnn.parametros.Add("@fecha", docConcesion.fecha);
                      }
                      if (docConcesion.numeroCI > 0)
                      {
                          cnn.parametros.Add("@numeroCI", docConcesion.numeroCI);
                      }
                      if (docConcesion.fechaCI != null && docConcesion.fechaCI != default(DateTime))
                      {
                          cnn.parametros.Add("@fechaCI", docConcesion.fechaCI);
                      }
                      if (docConcesion.observaciones != null && !docConcesion.observaciones.Equals(""))
                      {
                          cnn.parametros.Add("@observaciones", docConcesion.observaciones);
                      }
                      cnn.parametros.Add("@idUsuario", docConcesion.idUsuario);
                      
	                  DataTable dt = cnn.Execute();

                      docConcesion.idDocConcesion = Convert.ToInt32(dt.Rows[0]["idDocConcesion"]);

                      return true;
                     }
                     catch (Exception ex)
                     {
                         logger.PrintError(ex);
                         logger.SendMailError(ex);
                         return false;
                     };
            }

        public bool EliminarDocumentosConcesion(int idDocConcesion, int idSolConcesion, int idUsuario)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbDocumentosConcesion";

                cnn.parametros.Add("@idDocConcesion", idDocConcesion);
                if (idSolConcesion > 0)
                {
                    cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                }
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

        public List<DocumentosConcesion> ListarDocumentosConcesion(int idDocConcesion, int idSolConcesion, int idEstadoVigencia)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDocumentosConcesion";

                if (idDocConcesion > 0)
                {
                    cnn.parametros.Add("@idDocConcesion", idDocConcesion);
                }
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                
                if (idEstadoVigencia > 0)
                {
                    cnn.parametros.Add("@idEstadoVigencia", idEstadoVigencia);
                }

                DataTable dt = cnn.Execute();

                List<DocumentosConcesion> resp = new List<DocumentosConcesion>();
                DocumentosConcesion docConcesion = null;


                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        docConcesion = new DocumentosConcesion();
                        docConcesion.idDocConcesion = Convert.ToInt32(row["idDocConcesion"]);
                        docConcesion.solicitud = new SolicitudConcesion();
                        docConcesion.solicitud.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                        if (!row.IsNull("idTipoFlujoDocumental"))
                        {
                            docConcesion.flujoDocumental = new ParametroGenerico(Convert.ToInt32(row["idTipoFlujoDocumental"]), row["nombreFlujoDocumental"].ToString());
                        }
                        if (docConcesion.flujoDocumental.id == rbTipo.ENTRADA && !row.IsNull("idTipoIO"))
                        {
                            docConcesion.tipoEntrada = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), row["nombreTipoIO"].ToString());
                        }
                        if (docConcesion.flujoDocumental.id == rbTipo.SALIDA && !row.IsNull("idTipoIO"))
                        {
                            docConcesion.tipoSalida = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), row["nombreTipoIO"].ToString());
                        }
                        if (!row.IsNull("idTipoDocumento"))
                        {
                            docConcesion.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDocumento"].ToString());
                        }
                        if (docConcesion.flujoDocumental.id == rbTipo.ENTRADA && !row.IsNull("idTipoDestinatario"))
                        {
                            docConcesion.origen = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        }
                        if (docConcesion.flujoDocumental.id == rbTipo.SALIDA && !row.IsNull("idTipoDestinatario"))
                        {
                            docConcesion.destinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        }
                        if (!row.IsNull("idArchivoBinSC"))
                        {
                            docConcesion.archivoAdjunto = new ArchivoBinario();
                            docConcesion.archivoAdjunto.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                        }
                        if (!row.IsNull("idEstadoVigencia"))
                        {
                            docConcesion.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
                        }
                        if (!row.IsNull("nombreTema"))
                        {
                            docConcesion.nombreTema = row["nombreTema"].ToString();
                        }
                        
                        if (!row.IsNull("numero"))
                        {
                            docConcesion.numero = row["numero"].ToString();
                        }

                        if (!row.IsNull("fecha"))
                        {
                            docConcesion.fecha = Convert.ToDateTime(row["fecha"]);
                        }

                        if (!row.IsNull("numeroCI"))
                        {
                            docConcesion.numeroCI = Convert.ToInt32(row["numeroCI"]);
                        }

                        if (!row.IsNull("fechaCI"))
                        {
                            docConcesion.fechaCI = Convert.ToDateTime(row["fechaCI"]);
                        }
                        if (!row.IsNull("fechaIngresoSistema"))
                        {
                            docConcesion.fechaIngresoSistema = Convert.ToDateTime(row["fechaIngresoSistema"]);
                        }
                        if (!row.IsNull("observaciones"))
                        {
                            docConcesion.observaciones = row["observaciones"].ToString();
                        }

                        resp.Add(docConcesion);

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


        public DocumentosConcesion ObtenerDocumentoConcesion(int idDocConcesion, int idSolConcesion)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDocumentosConcesion";
                cnn.parametros.Add("@idDocConcesion", idDocConcesion);
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                
                DataTable dt = cnn.Execute();

                DocumentosConcesion docConcesion = null;


                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        docConcesion = new DocumentosConcesion();
                        docConcesion.idDocConcesion = Convert.ToInt32(row["idDocConcesion"]);
                        docConcesion.solicitud = new SolicitudConcesion();
                        docConcesion.solicitud.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                        if (!row.IsNull("idTipoFlujoDocumental"))
                        {
                            docConcesion.flujoDocumental = new ParametroGenerico(Convert.ToInt32(row["idTipoFlujoDocumental"]), row["nombreFlujoDocumental"].ToString());
                        }
                        if (docConcesion.flujoDocumental.id == rbTipo.ENTRADA && !row.IsNull("idTipoIO"))
                        {
                            docConcesion.tipoEntrada = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), row["nombreTipoIO"].ToString());
                        }
                        if (docConcesion.flujoDocumental.id == rbTipo.SALIDA && !row.IsNull("idTipoIO"))
                        {
                            docConcesion.tipoSalida = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), row["nombreTipoIO"].ToString());
                        }
                        if (!row.IsNull("idTipoDocumento"))
                        {
                            docConcesion.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDocumento"].ToString());
                        }
                        if (docConcesion.flujoDocumental.id == rbTipo.ENTRADA && !row.IsNull("idTipoDestinatario"))
                        {
                            docConcesion.origen = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        }
                        if (docConcesion.flujoDocumental.id == rbTipo.SALIDA && !row.IsNull("idTipoDestinatario"))
                        {
                            docConcesion.destinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        }
                        if (!row.IsNull("idArchivoBinSC"))
                        {
                            docConcesion.archivoAdjunto = new ArchivoBinario();
                            docConcesion.archivoAdjunto.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                        }
                        if (!row.IsNull("idEstadoVigencia"))
                        {
                            docConcesion.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
                        }
                        if (!row.IsNull("nombreTema"))
                        {
                            docConcesion.nombreTema = row["nombreTema"].ToString();
                        }

                        if (!row.IsNull("numero"))
                        {
                            docConcesion.numero = row["numero"].ToString();
                        }

                        if (!row.IsNull("fecha"))
                        {
                            docConcesion.fecha = Convert.ToDateTime(row["fecha"]);
                        }

                        if (!row.IsNull("numeroCI"))
                        {
                            docConcesion.numeroCI = Convert.ToInt32(row["numeroCI"]);
                        }

                        if (!row.IsNull("fechaCI"))
                        {
                            docConcesion.fechaCI = Convert.ToDateTime(row["fechaCI"]);
                        }
                        if (!row.IsNull("fechaIngresoSistema"))
                        {
                            docConcesion.fechaIngresoSistema = Convert.ToDateTime(row["fechaIngresoSistema"]);
                        }
                        if (!row.IsNull("observaciones"))
                        {
                            docConcesion.observaciones = row["observaciones"].ToString();
                        }


                        break;

                    }
                }

                return docConcesion;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public List<DocumentosConcesion> ListarDocumentosConcesionOficio(int idSolConcesion)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDocumentosConcesionOficio";

                cnn.parametros.Add("@idSolConcesion", idSolConcesion);

                DataTable dt = cnn.Execute();

                List<DocumentosConcesion> resp = new List<DocumentosConcesion>();
                DocumentosConcesion docConcesion = null;

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        docConcesion = new DocumentosConcesion();
                        docConcesion.idDocConcesion = Convert.ToInt32(row["idDocConcesion"]);
                        docConcesion.solicitud = new SolicitudConcesion();
                        docConcesion.solicitud.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                        if (!row.IsNull("idTipoFlujoDocumental"))
                        {
                            docConcesion.flujoDocumental = new ParametroGenerico(Convert.ToInt32(row["idTipoFlujoDocumental"]), row["nombreFlujoDocumental"].ToString());
                        }
                        if (docConcesion.flujoDocumental.id == rbTipo.ENTRADA && !row.IsNull("idTipoIO"))
                        {
                            docConcesion.tipoEntrada = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), row["nombreTipoIO"].ToString());
                        }
                        if (docConcesion.flujoDocumental.id == rbTipo.SALIDA && !row.IsNull("idTipoIO"))
                        {
                            docConcesion.tipoSalida = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), row["nombreTipoIO"].ToString());
                        }
                        if (!row.IsNull("idTipoDocumento"))
                        {
                            docConcesion.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDocumento"].ToString());
                        }
                        if (docConcesion.flujoDocumental.id == rbTipo.ENTRADA && !row.IsNull("idTipoDestinatario"))
                        {
                            docConcesion.origen = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        }
                        if (docConcesion.flujoDocumental.id == rbTipo.SALIDA && !row.IsNull("idTipoDestinatario"))
                        {
                            docConcesion.destinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        }
                        if (!row.IsNull("idArchivoBinSC"))
                        {
                            docConcesion.archivoAdjunto = new ArchivoBinario();
                            docConcesion.archivoAdjunto.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                        }
                        if (!row.IsNull("idEstadoVigencia"))
                        {
                            docConcesion.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
                        }
                        if (!row.IsNull("nombreTema"))
                        {
                            docConcesion.nombreTema = row["nombreTema"].ToString();
                        }

                        if (!row.IsNull("numero"))
                        {
                            docConcesion.numero = row["numero"].ToString();
                        }

                        if (!row.IsNull("fecha"))
                        {
                            docConcesion.fecha = Convert.ToDateTime(row["fecha"]);
                        }

                        if (!row.IsNull("numeroCI"))
                        {
                            docConcesion.numeroCI = Convert.ToInt32(row["numeroCI"]);
                        }

                        if (!row.IsNull("fechaCI"))
                        {
                            docConcesion.fechaCI = Convert.ToDateTime(row["fechaCI"]);
                        }
                        if (!row.IsNull("fechaIngresoSistema"))
                        {
                            docConcesion.fechaIngresoSistema = Convert.ToDateTime(row["fechaIngresoSistema"]);
                        }
                        if (!row.IsNull("observaciones"))
                        {
                            docConcesion.observaciones = row["observaciones"].ToString();
                        }

                        resp.Add(docConcesion);

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

    }
}
