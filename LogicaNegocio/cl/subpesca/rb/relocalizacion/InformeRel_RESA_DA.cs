using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.AccesoDatos;
using Datos.Entidades.Relocalizacion;
using System.Data;
using Datos.Entidades;

namespace LogicaNegocio.cl.subpesca.rb.relocalizacion
{
    public class InformeRel_RESA_DA
    {

        public Logger Log { get; set; }

        public InformeRel_RESA_DA()
        {
            this.Log = new Logger();
        }


        public bool GuardarInformeRel_Resa(InformeRel_RESA informeRel)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbInformeRel";
                cnn.parametros.Add("@idInformeRel", informeRel.idInformeRel);
                cnn.parametros.Add("@idTipoDocumento", informeRel.tipoDocumento.id);
                cnn.parametros.Add("@idTipoDestinatario", informeRel.tipoDestinatario.id);
                cnn.parametros.Add("@idMateria", informeRel.materia.id);
                if (informeRel.archivoBinSC != null && informeRel.archivoBinSC.idArchivo>0)
                {
                    cnn.parametros.Add("@idArchivoBinSC", informeRel.archivoBinSC.idArchivo);
                }
                cnn.parametros.Add("@idEstadoVigencia", informeRel.estadoVigencia.id);
                if (informeRel.numero != null && !informeRel.numero.Equals(""))
                {
                    cnn.parametros.Add("@numero", informeRel.numero);
                }
                if (informeRel.fecha != null && informeRel.fecha != default(DateTime))
                {
                    cnn.parametros.Add("@fecha", informeRel.fecha);
                }
                if (informeRel.observaciones != null && !informeRel.observaciones.Equals(""))
                {
                    cnn.parametros.Add("@observaciones", informeRel.observaciones);
                }
                if (informeRel.usuario != null && informeRel.usuario.id_usuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", informeRel.usuario.id_usuario);
                }
                
                DataTable dt = cnn.Execute();
                informeRel.idInformeRel = Convert.ToInt32(dt.Rows[0]["idInformeRel"]);

                return true;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public bool EliminarInformeRel_Resa(int idInformeRel)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbInformeRel";
                cnn.parametros.Add("@idInformeRel", idInformeRel);

                DataTable dt = cnn.Execute();

                return true;

            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public List<InformeRel_RESA> ListarInformeRes_Resa(int idInformeRel, string numero, int codCentro)
        {
            try
            {
                InformeRel_RESA informeRel = null;
                List<InformeRel_RESA> resp = new List<InformeRel_RESA>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbInformeRel_Admin";

                if(idInformeRel>0){
                    cnn.parametros.Add("@idInformeRel", idInformeRel);
                }
                if(numero!=null && !numero.Equals("")){
                    cnn.parametros.Add("@numero", numero);
                }
                if (codCentro>0)
                {
                    cnn.parametros.Add("@codCentro", codCentro);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        informeRel = new InformeRel_RESA();
                        informeRel.idInformeRel = Convert.ToInt32(row["idInformeRel"]);
                        informeRel.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDocumento"].ToString());
                        informeRel.tipoDestinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        informeRel.materia = new ParametroGenerico(Convert.ToInt32(row["idMateria"]), row["nombreMateria"].ToString());
                        if (!row.IsNull("idArchivoBinSC"))
                        {
                            informeRel.archivoBinSC = new ArchivoBinario();
                            informeRel.archivoBinSC.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                        }
                        informeRel.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
                        informeRel.numero = row["numero"].ToString();
                        if (!row.IsNull("fecha"))
                        {
                            informeRel.fecha = Convert.ToDateTime(row["fecha"]);
                        }
                        if (!row.IsNull("observaciones"))
                        {
                            informeRel.observaciones = row["observaciones"].ToString();
                        }
                        if (!row.IsNull("fechaIngresoSistema"))
                        {
                            informeRel.fechaIngresoSistema = Convert.ToDateTime(row["fechaIngresoSistema"]);
                        }
                        if (!row.IsNull("centros"))
                        {
                            informeRel.codCentros = row["centros"].ToString();
                        }

                        resp.Add(informeRel);
                    }
                }
                return resp;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
                
            }
        }


        public List<InformeRel_RESA> ListarInformeResVigente_Resa(int idInformeRel, string numero, int codCentro)
        {
            try
            {
                InformeRel_RESA informeRel = null;
                List<InformeRel_RESA> resp = new List<InformeRel_RESA>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbInformeRelVigente_Admin";

                if (idInformeRel > 0)
                {
                    cnn.parametros.Add("@idInformeRel", idInformeRel);
                }
                if (numero != null && !numero.Equals(""))
                {
                    cnn.parametros.Add("@numero", numero);
                }
                if (codCentro > 0)
                {
                    cnn.parametros.Add("@codCentro", codCentro);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        informeRel = new InformeRel_RESA();
                        informeRel.idInformeRel = Convert.ToInt32(row["idInformeRel"]);
                        informeRel.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDocumento"].ToString());
                        informeRel.tipoDestinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        informeRel.materia = new ParametroGenerico(Convert.ToInt32(row["idMateria"]), row["nombreMateria"].ToString());
                        if (!row.IsNull("idArchivoBinSC"))
                        {
                            informeRel.archivoBinSC = new ArchivoBinario();
                            informeRel.archivoBinSC.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                        }
                        informeRel.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
                        informeRel.numero = row["numero"].ToString();
                        if (!row.IsNull("fecha"))
                        {
                            informeRel.fecha = Convert.ToDateTime(row["fecha"]);
                        }
                        if (!row.IsNull("observaciones"))
                        {
                            informeRel.observaciones = row["observaciones"].ToString();
                        }
                        if (!row.IsNull("fechaIngresoSistema"))
                        {
                            informeRel.fechaIngresoSistema = Convert.ToDateTime(row["fechaIngresoSistema"]);
                        }
                        if (!row.IsNull("centros"))
                        {
                            informeRel.codCentros = row["centros"].ToString();
                        }

                        resp.Add(informeRel);
                    }
                }
                return resp;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;

            }
        }

        public InformeRel_RESA ObtenerInformeRes_Resa(int idInformeRel)
        {
            try
            {
                InformeRel_RESA informeRel = null;
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbInformeRel";

                cnn.parametros.Add("@idInformeRel", idInformeRel);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        informeRel = new InformeRel_RESA();
                        informeRel.idInformeRel = Convert.ToInt32(row["idInformeRel"]);
                        informeRel.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDocumento"].ToString());
                        informeRel.tipoDestinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        informeRel.materia = new ParametroGenerico(Convert.ToInt32(row["idMateria"]), row["nombreMateria"].ToString());
                        if (!row.IsNull("idArchivoBinSC"))
                        {
                            informeRel.archivoBinSC = new ArchivoBinario();
                            informeRel.archivoBinSC.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                        }
                        informeRel.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
                        informeRel.numero = row["numero"].ToString();
                        if (!row.IsNull("fecha"))
                        {
                            informeRel.fecha = Convert.ToDateTime(row["fecha"]);
                        }
                        if (!row.IsNull("observaciones"))
                        {
                            informeRel.observaciones = row["observaciones"].ToString();
                        }
                        if (!row.IsNull("fechaIngresoSistema"))
                        {
                            informeRel.fechaIngresoSistema = Convert.ToDateTime(row["fechaIngresoSistema"]);
                        }

                    }
                }
                
                return informeRel;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;

            }
        }

        public bool GuardarAsocInformeSolicitud(int idInformeRel, int idSolConcesion, int idEstadoAsoc)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbAsocInformeSolicitud";
                cnn.parametros.Add("@idInformeRel", idInformeRel);
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                cnn.parametros.Add("@idEstadoAsoc", idEstadoAsoc);
                
                DataTable dt = cnn.Execute();
                idInformeRel = Convert.ToInt32(dt.Rows[0]["idInformeRel"]);

                if (idInformeRel>0)
                {
                    return true;
                }
                return false; 
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public bool EliminarAsocInformeSolicitud(int idInformeRel, int idSolicitud)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbAsocInformeSolicitud";
                cnn.parametros.Add("@idInformeRel", idInformeRel);
                if (idSolicitud>0)
                {
                    cnn.parametros.Add("@idSolConcesion", idSolicitud);
                }

                DataTable dt = cnn.Execute();

                return true;

            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public List<AsocInformeSolicitud> ListarAsocInformeSolicitud(int idInformeRel)
        {
            try
            {
                AsocInformeSolicitud asocInformeRel = null;
                List<AsocInformeSolicitud> resp = new List<AsocInformeSolicitud>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbAsocInformeSolicitud";

                if (idInformeRel > 0)
                {
                    cnn.parametros.Add("@idInformeRel", idInformeRel);
                }
              
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        asocInformeRel = new AsocInformeSolicitud();
                        asocInformeRel.idInformeRel = Convert.ToInt32(row["idInformeRel"]);
                        asocInformeRel.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                        if (!row.IsNull("codigoCentro"))
                        {
                            asocInformeRel.codigoCentro = Convert.ToInt32(row["codigoCentro"]);
                        }
                        if (!row.IsNull("titularesCad"))
                        {
                            asocInformeRel.titulares = row["titularesCad"].ToString();
                        }
                        if (!row.IsNull("comunasCad"))
                        {
                            asocInformeRel.comunas = row["comunasCad"].ToString();
                        }
                        
                        asocInformeRel.estadoAsoc = new ParametroGenerico(Convert.ToInt32(row["idEstadoAsoc"]), row["nombreEstado"].ToString());

                        if (!row.IsNull("fechaIngresoSistema"))
                        {
                            asocInformeRel.fechaIngresoSistema = Convert.ToDateTime(row["fechaIngresoSistema"]);
                        }

                        resp.Add(asocInformeRel);
                    }
                }
                return resp;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;

            }
        }

    }
}
