using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using Datos.AccesoDatos;
using System.Data;
using System.Collections;

namespace LogicaNegocio.cl.subpesca.rb.usuario
{
    public class RolDA
    {
        Logger logger = new Logger();

        public Hashtable ListarSeccionesUsuario(int idUsuario)
        {
            try
            {
                Hashtable seccionAux = new Hashtable();
                Hashtable accionAux = new Hashtable();
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSeccionRolUsuario";
                cnn.parametros.Add("@idUsuario", idUsuario);

                int idSeccion=0;
                int idSeccionAux=0;


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        
                        idSeccion = Convert.ToInt32(row["idSeccionSist"]);
                        if(idSeccion!=idSeccionAux){
                            seccionAux.Add(Convert.ToInt32(row["idSeccionSist"]), new Hashtable());
                            accionAux = (Hashtable)seccionAux[Convert.ToInt32(row["idSeccionSist"])];
                        }
                        accionAux.Add(Convert.ToInt32(row["idAccion"]), true);
                        idSeccionAux = idSeccion;
                    }
                }

                return seccionAux;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public List<Rol> ListarRoles(Rol rol)
        {
            try
            {
                Rol rolAux = null;
                List <Rol> resp = new List<Rol>();
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbRol";
                if(rol.idRol>0){
                    cnn.parametros.Add("@idRol", rol.idRol);
                }
                if(rol.estadoVigencia!=null && rol.estadoVigencia.id>0){
                    cnn.parametros.Add("@idEstadoVig", rol.estadoVigencia.id);
                }
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        rolAux = new Rol();
                        rolAux.idRol = Convert.ToInt32(row["idRol"]);
                        rolAux.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVig"]), row["nombreEstado"].ToString());
                        rolAux.nombreRol = row["nombreRol"].ToString();
                        rolAux.descripcionRol = row["descripcion"].ToString();
                        if (!row.IsNull("idTipoRol"))
                        {
                            rolAux.tipoRol = new ParametroGenerico(Convert.ToInt32(row["idTipoRol"]), row["nombreTipo"].ToString());
                        }

                        if (!row.IsNull("aplicaDespliegueFiltro"))
                        {
                            rolAux.aplicaDespliegueFiltro = Convert.ToBoolean(row["aplicaDespliegueFiltro"]);
                        }

                        resp.Add(rolAux);
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

        public DataTable ListarAccionesSeccionRol(int idRol, int idMenu)
        {

            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbAccionesSeccion";
                cnn.parametros.Add("@idRol", idRol);
                if (idMenu>0)
                {
                    cnn.parametros.Add("@idMenu", idMenu);
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

        public bool GuardarSeccionRol(int idSeccionSist, int idRol, int idAccion)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbSeccionRol";
                cnn.parametros.Add("@idSeccionSist", idSeccionSist);
                cnn.parametros.Add("@idRol", idRol);
                cnn.parametros.Add("@idAccion", idAccion);

                DataTable dt = cnn.Execute();
                idSeccionSist = Convert.ToInt32(dt.Rows[0]["idSeccion"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool EliminarSeccionRol(int idSeccionSist, int idRol, int idAccion)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbSeccionRol";
               
                cnn.parametros.Add("@idSeccionSist", idSeccionSist);
                cnn.parametros.Add("@idRol", idRol);
                cnn.parametros.Add("@idAccion", idAccion);

                DataTable dt = cnn.Execute();

                int resul = Convert.ToInt32(dt.Rows[0]["resultado"]);
                
                return true;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public DataTable GuardarRol(int idRol, string nombreRol, int idTipoRol, bool aplicaDespliegueFiltro)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbRol";
                cnn.parametros.Add("@idRol", idRol);
                cnn.parametros.Add("@nombreRol", nombreRol);
                if (idTipoRol>0)
                {
                    cnn.parametros.Add("@idTipoRol", idTipoRol);
                }
                cnn.parametros.Add("@aplicaDespliegueFiltro", aplicaDespliegueFiltro);

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

        public DataTable EliminarRol(int idRol)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbRol";

                cnn.parametros.Add("@idRol", idRol);

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

        public DataTable ListarRolesUsuario(int idUsuario, int idEstadoVig)
        {

            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbRolUsuario";
                cnn.parametros.Add("@idUsuario", idUsuario);
                cnn.parametros.Add("@idEstadoVig", idEstadoVig);
                
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

        public bool EliminarUsuarioRol(int idRol, int idUsuario)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbUsuarioRol";
                cnn.parametros.Add("@idRol", idRol);
                cnn.parametros.Add("@idUsuario", idUsuario);
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

        public bool GuardarUsuarioRol(int idUsuario,int idRol)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbUsuarioRol";
                cnn.parametros.Add("@idUsuario", idUsuario);
                cnn.parametros.Add("@idRol", idRol);

                DataTable dt = cnn.Execute();
                idRol = Convert.ToInt32(dt.Rows[0]["idRol"]);

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
