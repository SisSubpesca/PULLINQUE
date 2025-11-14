using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using Datos.AccesoDatos;
using System.Data;

namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class TemplateAvisoDA
    {
        
        Logger logger = new Logger();


        public TemplateAviso ObtieneTemplateAvisoDestinatario(TemplateAviso templateFiltro)
        {
            try
            {
                TemplateAviso claseResp = null;
                DestinatarioTemplate destTemplate = null;
                String clave="";
                String claveAux="";
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTemplateAvisoDestinatario";


                if (templateFiltro.idSolicitudRevisada>0)
                {
                    cnn.parametros.Add("@idSolicitud", templateFiltro.idSolicitudRevisada);
                }
                if (templateFiltro.idTipoSolicitudRev>0)
                {
                    cnn.parametros.Add("@idTipoSolicitud", templateFiltro.idTipoSolicitudRev);
                }
                
                if (templateFiltro.claveTemplateAviso != null && !templateFiltro.claveTemplateAviso.Equals(""))
                {
                    cnn.parametros.Add("@claveTemplateAviso", templateFiltro.claveTemplateAviso);
                }

                if (templateFiltro.seccion!=null && templateFiltro.seccion.id>0)
                {
                    cnn.parametros.Add("@idSeccion", templateFiltro.seccion.id);
                }
                if (templateFiltro.requerimiento != null && templateFiltro.requerimiento.id>0)
                {
                    cnn.parametros.Add("@idSubRequerimiento", templateFiltro.requerimiento.id);
                }
                if (templateFiltro.tipoFlujoDocumental != null && templateFiltro.tipoFlujoDocumental.id>0)
                {
                    cnn.parametros.Add("@idTipoFlujoDoc", templateFiltro.tipoFlujoDocumental.id);
                }
                
                DataTable dt = cnn.Execute();
                //DataTable dt = cnn.Execute2();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        clave = row["claveTemplateAviso"].ToString();

                        if(!clave.Equals(claveAux)){
                            claseResp = new TemplateAviso();
                            claseResp.claveTemplateAviso = row["claveTemplateAviso"].ToString();

                            if (!row.IsNull("idSeccion"))
                            {
                                claseResp.seccion = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]));
                            }
                            if (!row.IsNull("idSubRequerimiento"))
                            {
                                claseResp.requerimiento = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]));
                            }
                            if (!row.IsNull("idTipoFlujoDoc"))
                            {
                                claseResp.tipoFlujoDocumental = new ParametroGenerico(Convert.ToInt32(row["idTipoFlujoDoc"]));
                            }

                            claseResp.templateSubject = row["templateSubject"].ToString();
                            claseResp.templateCuerpo = row["templateCuerpo"].ToString();
                            claseResp.destinatariosTemplate = new List<DestinatarioTemplate>();
                        }

                        destTemplate = new DestinatarioTemplate();
                       
                        if (!row.IsNull("nombreDestinatario"))
                        {
                            destTemplate.nombreDestinatario = row["nombreDestinatario"].ToString();
                        }
                        if (!row.IsNull("emailDestinatario"))
                        {
                            destTemplate.emailDestinatario = row["emailDestinatario"].ToString();
                        }
                        destTemplate.usuario = new Usuario();

                        if (!row.IsNull("nombresUsuario"))
                        {
                            destTemplate.usuario.nombre = row["nombresUsuario"].ToString();
                        }
                        if (!row.IsNull("apellidosUsuario"))
                        {
                            destTemplate.usuario.apellidos = row["apellidosUsuario"].ToString();
                        }
                        if (!row.IsNull("email"))
                        {
                            destTemplate.usuario.correo = row["email"].ToString();
                        }
                        
                        claseResp.destinatariosTemplate.Add(destTemplate);
                        claveAux = clave;
                    }
                }

                return claseResp;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public TemplateAviso ObtieneTemplateAvisoDestinatarioAdmin(string claveTemplateAviso)
        {
            try
            {
                TemplateAviso claseResp = null;
                DestinatarioTemplate destTemplate = null;
                String clave = "";
                String claveAux = "";
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTemplateAvisoDestinatarioAdmin";

                if (claveTemplateAviso != null && !claveTemplateAviso.Equals(""))
                {
                    cnn.parametros.Add("@claveTemplateAviso", claveTemplateAviso);
                }

                DataTable dt = cnn.Execute();
                
                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        clave = row["claveTemplateAviso"].ToString();

                        if (!clave.Equals(claveAux))
                        {
                            claseResp = new TemplateAviso();
                            claseResp.claveTemplateAviso = row["claveTemplateAviso"].ToString();
                            claseResp.templateSubject = row["templateSubject"].ToString();
                            claseResp.templateCuerpo = row["templateCuerpo"].ToString();

                            if (!row.IsNull("descripcionTemplateAviso"))
                            {
                                claseResp.descrTemplateAviso = row["descripcionTemplateAviso"].ToString();
                            }
                            if (!row.IsNull("aplicaEnvio"))
                            {
                                claseResp.aplicaEnvio = Convert.ToBoolean(row["aplicaEnvio"]);
                            }

                            if (!row.IsNull("descripcionReempl"))
                            {
                                claseResp.descrReempl = row["descripcionReempl"].ToString();
                            }
                            claseResp.claveTemplateAvisoDescr = row["templateSubjectDescr"].ToString();

                            claseResp.destinatariosTemplate = new List<DestinatarioTemplate>();
                            claseResp.destinatariosRol = new List<DestinatarioTemplate>();
                        }

                        if (!row.IsNull("emailDestinatario"))
                        {
                            destTemplate = new DestinatarioTemplate();
                            destTemplate.idDestTemplate = Convert.ToInt32(row["idDestTemplate"]);

                            destTemplate.emailDestinatario = row["emailDestinatario"].ToString();

                            if (!row.IsNull("nombreDestinatario"))
                            {
                                destTemplate.nombreDestinatario = row["nombreDestinatario"].ToString();
                            }
                            claseResp.destinatariosTemplate.Add(destTemplate);
                        }
                        
                        if (!row.IsNull("idRol"))
                        {
                            destTemplate = new DestinatarioTemplate();
                            destTemplate.idDestTemplate = Convert.ToInt32(row["idDestTemplate"]);
                            destTemplate.rol = new Rol();
                            destTemplate.rol.idRol = Convert.ToInt32(row["idRol"]);
                            if (!row.IsNull("nombreRol"))
                            {
                                destTemplate.rol.nombreRol = row["nombreRol"].ToString();
                            }
                            claseResp.destinatariosRol.Add(destTemplate);
                        }
                        claveAux = clave;
                    }
                }

                return claseResp;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public List<TemplateAviso> ListarTemplateAvisoDestinatarioAdmin(string claveTemplateAviso)
        {
            try
            {
                List<TemplateAviso> resp = new List<TemplateAviso>();
                TemplateAviso claseResp = null;
                DestinatarioTemplate destTemplate = null;
                String clave = "";
                String claveAux = "";
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTemplateAvisoDestinatarioAdmin";

                if (claveTemplateAviso != null && !claveTemplateAviso.Equals(""))
                {
                    cnn.parametros.Add("@claveTemplateAviso", claveTemplateAviso);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        clave = row["claveTemplateAviso"].ToString();

                        if (!clave.Equals(claveAux))
                        {
                            claseResp = new TemplateAviso();
                            claseResp.claveTemplateAviso = row["claveTemplateAviso"].ToString();
                            claseResp.templateSubject = row["templateSubject"].ToString();
                            claseResp.templateCuerpo = row["templateCuerpo"].ToString();

                            if (!row.IsNull("descripcionTemplateAviso"))
                            {
                                claseResp.descrTemplateAviso = row["descripcionTemplateAviso"].ToString();
                            }

                            if (!row.IsNull("aplicaEnvio"))
                            {
                                claseResp.aplicaEnvio = Convert.ToBoolean(row["aplicaEnvio"]);
                            }
                            claseResp.claveTemplateAvisoDescr = row["templateSubjectDescr"].ToString();

                            claseResp.destinatariosTemplate = new List<DestinatarioTemplate>();
                            claseResp.destinatariosRol = new List<DestinatarioTemplate>();
                        }

                        if (!row.IsNull("emailDestinatario"))
                        {
                            destTemplate = new DestinatarioTemplate();
                            destTemplate.idDestTemplate = Convert.ToInt32(row["idDestTemplate"]);
                            destTemplate.emailDestinatario = row["emailDestinatario"].ToString();
                            if (!row.IsNull("nombreDestinatario"))
                            {
                                destTemplate.nombreDestinatario = row["nombreDestinatario"].ToString();
                            }

                            claseResp.destinatariosTemplate.Add(destTemplate);
                        }

                        if (!row.IsNull("idRol"))
                        {
                            destTemplate = new DestinatarioTemplate();
                            destTemplate.idDestTemplate = Convert.ToInt32(row["idDestTemplate"]);
                            destTemplate.rol = new Rol();
                            destTemplate.rol.idRol = Convert.ToInt32(row["idRol"]);
                            if (!row.IsNull("nombreRol"))
                            {
                                destTemplate.rol.nombreRol = row["nombreRol"].ToString();
                            }
                            claseResp.destinatariosRol.Add(destTemplate);
                        }

                        resp.Add(claseResp);

                        claveAux = clave;
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

        public bool ActualizaTemplateAviso(TemplateAviso templAviso)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbTemplateAviso";
                cnn.parametros.Add("@claveTemplateAviso", templAviso.claveTemplateAviso);
                cnn.parametros.Add("@templateSubject", templAviso.templateSubject);
                cnn.parametros.Add("@templateCuerpo", templAviso.templateCuerpo);
                cnn.parametros.Add("@aplicaEnvio", templAviso.aplicaEnvio);
                cnn.parametros.Add("@descripcionTemplateAviso", templAviso.descrTemplateAviso);

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

        public TemplateAviso ObtieneTemplateAvisoDestinatarioSC(TemplateAviso templateFiltro)
        {
            try
            {
                TemplateAviso claseResp = null;
                DestinatarioTemplate destTemplate = null;
                String clave = "";
                String claveAux = "";
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTemplateAvisoSC";


                if (templateFiltro.idSolicitudRevisada > 0)
                {
                    cnn.parametros.Add("@idSolicitud", templateFiltro.idSolicitudRevisada);
                }
                
                if (templateFiltro.claveTemplateAviso != null && !templateFiltro.claveTemplateAviso.Equals(""))
                {
                    cnn.parametros.Add("@claveTemplateAviso", templateFiltro.claveTemplateAviso);
                }

                if (templateFiltro.seccion != null && templateFiltro.seccion.id > 0)
                {
                    cnn.parametros.Add("@idSeccion", templateFiltro.seccion.id);
                }
                if (templateFiltro.requerimiento != null && templateFiltro.requerimiento.id > 0)
                {
                    cnn.parametros.Add("@idSubRequerimiento", templateFiltro.requerimiento.id);
                }
                if (templateFiltro.tipoFlujoDocumental != null && templateFiltro.tipoFlujoDocumental.id > 0)
                {
                    cnn.parametros.Add("@idTipoFlujoDoc", templateFiltro.tipoFlujoDocumental.id);
                }

                DataTable dt = cnn.Execute();
                //DataTable dt = cnn.Execute2();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        clave = row["claveTemplateAviso"].ToString();

                        if (!clave.Equals(claveAux))
                        {
                            claseResp = new TemplateAviso();
                            claseResp.claveTemplateAviso = row["claveTemplateAviso"].ToString();

                            if (!row.IsNull("idSeccion"))
                            {
                                claseResp.seccion = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]));
                            }
                            if (!row.IsNull("idSubRequerimiento"))
                            {
                                claseResp.requerimiento = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]));
                            }
                            if (!row.IsNull("idTipoFlujoDoc"))
                            {
                                claseResp.tipoFlujoDocumental = new ParametroGenerico(Convert.ToInt32(row["idTipoFlujoDoc"]));
                            }

                            claseResp.templateSubject = row["templateSubject"].ToString();
                            claseResp.templateCuerpo = row["templateCuerpo"].ToString();
                            claseResp.destinatariosTemplate = new List<DestinatarioTemplate>();
                        }

                        destTemplate = new DestinatarioTemplate();

                        if (!row.IsNull("nombreDestinatario"))
                        {
                            destTemplate.nombreDestinatario = row["nombreDestinatario"].ToString();
                        }
                        if (!row.IsNull("emailDestinatario"))
                        {
                            destTemplate.emailDestinatario = row["emailDestinatario"].ToString();
                        }
                        destTemplate.usuario = new Usuario();

                        if (!row.IsNull("nombresUsuario"))
                        {
                            destTemplate.usuario.nombre = row["nombresUsuario"].ToString();
                        }
                        if (!row.IsNull("apellidosUsuario"))
                        {
                            destTemplate.usuario.apellidos = row["apellidosUsuario"].ToString();
                        }
                        if (!row.IsNull("email"))
                        {
                            destTemplate.usuario.correo = row["email"].ToString();
                        }
                                              
                        claseResp.destinatariosTemplate.Add(destTemplate);
                        claveAux = clave;
                    }
                }

                return claseResp;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public bool GuardarDestinatarioTemplate(DestinatarioTemplate destinatario)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbDestinatarioPorTemplate";

                cnn.parametros.Add("@idDestTemplate", destinatario.idDestTemplate);
                cnn.parametros.Add("@claveTemplateAviso", destinatario.claveTemplateAviso);

                if (destinatario.rol != null && destinatario.rol.idRol > 0)
                {
                    cnn.parametros.Add("@idRol", destinatario.rol.idRol);
                }
                if (destinatario.nombreDestinatario != null && !destinatario.nombreDestinatario.Equals(""))
                {
                    cnn.parametros.Add("@nombreDestinatario", destinatario.nombreDestinatario);
                }
                if (destinatario.emailDestinatario != null && !destinatario.emailDestinatario.Equals(""))
                {
                    cnn.parametros.Add("@emailDestinatario", destinatario.emailDestinatario);
                }
                cnn.parametros.Add("@aplicaEnvio", destinatario.aplicaEnvio);

                DataTable dt = cnn.Execute();
                destinatario.idDestTemplate = Convert.ToInt32(dt.Rows[0]["idDestTemplate"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };

        }

        public bool EliminarDestinatarioTemplate(int idDestTemplate, string claveTemplateAviso)
        {
            try
            {
                int resultado = 0;
                
                Conexion cnn = new Conexion();

                cnn.procedimiento = "paDelRbDestinatarioPorTemplate";

                if (idDestTemplate > 0)
                {
                    cnn.parametros.Add("@idDestTemplate", idDestTemplate);
                }
                
                cnn.parametros.Add("@claveTemplateAviso", claveTemplateAviso);
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {

                        if (!row.IsNull("resultado"))
                        {
                            resultado = Convert.ToInt32(row["resultado"]);
                        }
                    }
                }

                if (resultado < 0)
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
            };
        }

    }
}
