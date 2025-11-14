using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.AccesoDatos;
using Datos.Entidades;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.errores;

namespace LogicaNegocio.cl.subpesca.rb.common
{
    public class ContactoDA
    {
        Logger logger = new Logger();

        public bool GuardarContacto(Contacto contacto)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbContactos";
                cnn.parametros.Add("@idContacto", contacto.idContacto);
                cnn.parametros.Add("@idTipoContacto", contacto.tipoContacto.id);
                cnn.parametros.Add("@valorContacto", contacto.valorContacto);
                if (contacto.detalle != null && !contacto.detalle.Equals(""))
                {
                    cnn.parametros.Add("@detalle", contacto.detalle);
                }

                DataTable dt = cnn.Execute();
                contacto.idContacto = Convert.ToInt32(dt.Rows[0]["idContacto"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool EliminarContacto(int idContacto)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbContactos";
                cnn.parametros.Add("@idContacto", idContacto);
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


    }
}
