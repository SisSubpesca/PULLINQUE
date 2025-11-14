using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.AccesoDatos;
using Datos.Entidades;
using System.Data;

namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class AvisoDA
    {

        Logger logger = new Logger();

        public bool GuardarAviso(Aviso aviso)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbAviso";

                if (aviso.solicitud != null && aviso.solicitud.idSolConcesion > 0)
                {
                    cnn.parametros.Add("@idSolConcesion", aviso.solicitud.idSolConcesion);
                }
                cnn.parametros.Add("@claveTemplateAviso", aviso.claveTemplate);
                cnn.parametros.Add("@subjectMensaje", aviso.subject);
                cnn.parametros.Add("@cuerpoMensaje", aviso.mensaje);
                
                DataTable dt = cnn.Execute();
                //DataTable dt = cnn.Execute2();
                aviso.idAviso = Convert.ToInt32(dt.Rows[0]["idAviso"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool GuardarDestinatariosAviso(int idAviso, string emailDestinatario)
        {
            try
            {
                int idDestAviso;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbDestinatariosAviso";
                cnn.parametros.Add("@idAviso", idAviso);
                cnn.parametros.Add("@emailDestinatario", emailDestinatario);
                
                DataTable dt = cnn.Execute();
                //DataTable dt = cnn.Execute2();
                idDestAviso = Convert.ToInt32(dt.Rows[0]["idDestAviso"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool ExisteAvisoEnviado(int idSolConcesion, string claveTemplateAviso, string cuerpoMensaje)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbAviso_preExistente";
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                cnn.parametros.Add("@claveTemplateAviso", claveTemplateAviso);
                cnn.parametros.Add("@cuerpoMensaje", cuerpoMensaje);

                DataTable dt = cnn.Execute();
                //DataTable dt = cnn.Execute2();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row.IsNull("idAviso"))
                        {
                            return true;
                        }
                      
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return true;
            }
        }

    }
}
