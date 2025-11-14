using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.AccesoDatos;
using Datos.Entidades;

namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class TitularRegConcesionesDA
    {

        Logger logger = new Logger();
        
        
        //||°.°.°-°.°.°||\\

        public List<TitularRegConcesiones> ListarTitularesRegConcesiones()
        {
            Conexion cn = new Conexion();
            cn.procedimiento = "paSelRbTitularRegConcesiones";
            cn.parametros.Add("@idEstadoRegistro", 82);

            try
            {
                DataTable dt = cn.Execute();

                List<TitularRegConcesiones> ListTitularRegConcesiones = new List<TitularRegConcesiones>();

                foreach (DataRow row in dt.Rows)
                {
                    TitularRegConcesiones Titular = new TitularRegConcesiones();

                    Titular.idTitularRCA = Convert.ToInt32(row["idTitularRCA"]);
                    Titular.codigoCentro = row["codigoCentro"].ToString();
                    Titular.adquirienteNombre = row["adquirienteNombre"].ToString();
                    Titular.rutCompleto = row["adquirienteRut"].ToString() + "-" + row["adquirienteDv"].ToString();
                    Titular.fechaInscripcion = Convert.ToDateTime(row["fechaInscripcion"]);
                    Titular.observaciones = row["observaciones"].ToString();
                    ListTitularRegConcesiones.Add(Titular);
                }

                return ListTitularRegConcesiones;
            }
            catch
            {
                return null;
            }
        }
        //ACTUALIZA ESTADO DEL REGISTRO DE TITULARES RCA QUE SE ENCONTRABA CON ERRORES
        public bool ActualizarOperadorEstado(int idTitularRCA, int idUsuario, int idEstadoRegistro)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbTitularRegConcesiones";
                cnn.parametros.Add("@idTitularRCA", idTitularRCA);
                cnn.parametros.Add("@idUsuario", idUsuario);
                cnn.parametros.Add("@idEstadoRegistro", idEstadoRegistro);
                

                DataTable dt = cnn.Execute();
                idTitularRCA = Convert.ToInt32(dt.Rows[0]["idTitularRCA"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        //
        public bool InsertarPersonasLegalesSolicitud_RCA()
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbTitularRegConcesiones";

                //cnn.parametros.Add("@p_num_error", null);

                DataTable dt = cnn.Execute();
                
                foreach (DataRow row in dt.Rows)
                {

                    if (!row.IsNull("p_num_error") && Convert.ToInt32(row["p_num_error"]) != -1)
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
            };
        }

        public bool ActualizarEstadoTitularRegConcesiones()
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbEstadoTitularRegConcesiones";

                //cnn.parametros.Add("@p_num_error", null);

                DataTable dt = cnn.Execute();

                foreach (DataRow row in dt.Rows)
                {

                    if (!row.IsNull("p_num_error") && Convert.ToInt32(row["p_num_error"]) != -1)
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
            };
        }

    }
}
