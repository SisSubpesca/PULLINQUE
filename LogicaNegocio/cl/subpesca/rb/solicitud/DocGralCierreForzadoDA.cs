using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.AccesoDatos;
using Datos.Entidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Data;

namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class DocGralCierreForzadoDA
    {
        Logger logger = new Logger();

        public bool GuardarDocGralCierreForzado(Requerimiento docCierre)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbDocGralCierreForzado";
                cnn.parametros.Add("@idDocCierre", docCierre.idRequerimiento);
                cnn.parametros.Add("@idTipoFlujoDocumental", docCierre.flujoDocumental.id);
                if (docCierre.flujoDocumental.id == rbTipo.SALIDA)
                {
                    cnn.parametros.Add("@idTipoIO", docCierre.tipoSalida.id);
                }

                if (docCierre.flujoDocumental.id == rbTipo.ENTRADA)
                {
                    cnn.parametros.Add("@idTipoIO", docCierre.tipoEntrada.id);
                }

                cnn.parametros.Add("@idTipoDocumento", docCierre.tipoDocumento.id);

                if (docCierre.flujoDocumental.id == rbTipo.SALIDA)
                {
                    cnn.parametros.Add("@idTipoTrayecto", docCierre.destinatario.id);
                }
                if (docCierre.flujoDocumental.id == rbTipo.ENTRADA)
                {
                    cnn.parametros.Add("@idTipoTrayecto", docCierre.origen.id);
                }
                if (docCierre.archivoAdjunto != null && docCierre.archivoAdjunto.idArchivo > 0)
                {
                    cnn.parametros.Add("@idArchivoBinSC", docCierre.archivoAdjunto.idArchivo);
                }
                if (docCierre.estadoFinal != null && docCierre.estadoFinal.id > 0)
                {
                    cnn.parametros.Add("@idEstadoFinal", docCierre.estadoFinal.id);
                }
                if (docCierre.numero != null && !docCierre.numero.Equals(""))
                {
                    cnn.parametros.Add("@numero", docCierre.numero);
                }
                if (docCierre.fecha != null && docCierre.fecha != default(DateTime))
                {
                    cnn.parametros.Add("@fecha", docCierre.fecha);
                }
                if (docCierre.numeroCI > 0)
                {
                    cnn.parametros.Add("@numeroCI", docCierre.numeroCI);
                }
                if (docCierre.fechaCI != null && docCierre.fechaCI != default(DateTime))
                {
                    cnn.parametros.Add("@fechaCI", docCierre.fechaCI);
                }
                cnn.parametros.Add("@idTipoUnidEspacial", docCierre.tipoUnidEspacial.id);
                if (docCierre.tipoTramite != null && docCierre.tipoTramite.id > 0)
                {
                    cnn.parametros.Add("@idTipoTramite", docCierre.tipoTramite.id);
                }


                DataTable dt = cnn.Execute();
                docCierre.idRequerimiento = Convert.ToInt32(dt.Rows[0]["idDocCierre"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool GuardarDocSolicitudCierre(DocSolicitudCierre docSolicitudCierre)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbDocSolicitudCierre";
                cnn.parametros.Add("@idSolConcesion", docSolicitudCierre.idSolConcesion);
                cnn.parametros.Add("@idDocITCierre", docSolicitudCierre.docITCierre.idRequerimiento);
                if (docSolicitudCierre.docSSPCierre!=null && docSolicitudCierre.docSSPCierre.idRequerimiento > 0)
                {
                    cnn.parametros.Add("@rbDocSSPCierre", docSolicitudCierre.docSSPCierre.idRequerimiento);
                }
                cnn.parametros.Add("@idDocPestanaIT", docSolicitudCierre.docPestanaIT.idDocPestana);
                if (docSolicitudCierre.docPestanaSSP != null && docSolicitudCierre.docPestanaSSP.idDocPestana > 0)
                {
                    cnn.parametros.Add("@idDocPestanaSSP", docSolicitudCierre.docPestanaSSP.idDocPestana);
                }
                cnn.parametros.Add("@flujoCierreForzado", docSolicitudCierre.flujoCierreForzado);
                
                DataTable dt = cnn.Execute();
                docSolicitudCierre.idSolConcesion = Convert.ToInt32(dt.Rows[0]["idSolConcesion"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool ActualizarDocPestanaIT(int idPestana, int idDocGeneralResp, bool flujoCierreForzado)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbDocPestanaIT";
                cnn.parametros.Add("@idPestana", idPestana);
                cnn.parametros.Add("@idDocGeneralResp", idDocGeneralResp);
                cnn.parametros.Add("@flujoCierreForzado", flujoCierreForzado);
                
                DataTable dt = cnn.Execute();
                idPestana = Convert.ToInt32(dt.Rows[0]["idPestana"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public DataTable ListarDocGralCierreIT(int numero, int idTipoUnidEspacial, int idTipoTramite, string identificador)
        {

            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDocGralCierreIT";
                
                if (numero > 0)
                {
                    cnn.parametros.Add("@numero", numero);
                }
                if (identificador!=null && !identificador.Equals(""))
                {
                    cnn.parametros.Add("@identificador", identificador);
                }
                
                if (idTipoUnidEspacial > 0)
                {
                    cnn.parametros.Add("@idTipoUnidEspacial", idTipoUnidEspacial);
                }
                if (idTipoTramite > 0)
                {
                    cnn.parametros.Add("@idTipoTramite", idTipoTramite);
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

        public DataTable EliminarDocPestanaIT(int idPestana, int idDocGeneralResp)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbDocPestanaIT";
            cnn.parametros.Add("@idPestana", idPestana);
            cnn.parametros.Add("@idDocGeneralResp", idDocGeneralResp);

            DataTable dt = cnn.Execute();
            return dt;
        }



        public Requerimiento ObtieneInformeTecnicoCierre(int idDocCierre)
        {

            try
            {

                Requerimiento requerimiento = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDocGralCierreIT_Obtener";
                if(idDocCierre>0){
                    cnn.parametros.Add("@idDocCierre", idDocCierre);
                }
                
                
                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {
                     foreach (DataRow row in dt.Rows)
                     {

                         requerimiento = new Requerimiento();
                         requerimiento.idRequerimiento = Convert.ToInt32(row["idDocCierre"]);
                         requerimiento.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]),Convert.ToString(row["nombreTipo"]));
                         requerimiento.numero = row["numero"].ToString();

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

        public bool ActualizarDocSolicitudCierreSSP(DocSolicitudCierre docSolicitudCierre)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbDocSolicitudCierreSSP";
                cnn.parametros.Add("@idSolConcesion", docSolicitudCierre.idSolConcesion);
                cnn.parametros.Add("@idDocITCierre", docSolicitudCierre.docITCierre.idRequerimiento);
                cnn.parametros.Add("@rbDocSSPCierre", docSolicitudCierre.docSSPCierre.idRequerimiento);
                cnn.parametros.Add("@idDocPestanaSSP", docSolicitudCierre.docPestanaSSP.idDocPestana);

                DataTable dt = cnn.Execute();
                docSolicitudCierre.idSolConcesion = Convert.ToInt32(dt.Rows[0]["idSolConcesion"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool EliminarDocGralCierreForzado(int idDocCierre)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbDocGralCierreForzado";
                cnn.parametros.Add("@idDocCierre", idDocCierre);
                
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

    }
}

