using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.AccesoDatos;
using Datos.Entidades;
using System.Data;
using Utilidades;
using LogicaNegocio.cl.subpesca.rb.errores;


namespace LogicaNegocio.cl.subpesca.rb.usuario
{
    public class UsuarioDA 
    {


        public Logger Log { get; set; }
        
        public UsuarioDA()
        {
            this.Log = new Logger();
        } 

        public bool GuardarRbUsuario(Datos.Entidades.Usuario.Serializable usuario)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbUsuario";
                cnn.parametros.Add("@idUsuario", usuario.id_usuario);
                cnn.parametros.Add("@idEstadoVig", usuario.estado);
                cnn.parametros.Add("@idRegion", usuario.regionUsuario.id_region);

                /*
                if (usuario.tipoUsuario != null && usuario.tipoUsuario.id > 0)
                {
                    cnn.parametros.Add("@idTipoUsuario", usuario.tipoUsuario.id);
                }
                 **/
                cnn.parametros.Add("@rutUsuario", usuario.RUT);
                cnn.parametros.Add("@dvUsuario", usuario.dvUsuario);
                cnn.parametros.Add("@nombresUsuario", usuario.nombre);
                cnn.parametros.Add("@apellidosUsuario", usuario.apellidos);
                cnn.parametros.Add("@email", usuario.correo);
                cnn.parametros.Add("@usuario", usuario.usuario);
                cnn.parametros.Add("@contrasenia", usuario.clave);

                DataTable dt = cnn.Execute();
                usuario.id_usuario = Convert.ToInt32(dt.Rows[0]["idUsuario"]);

                return true;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public Usuario ObtieneRbUsuario(Usuario usuarioFiltro)
        {
            try
            {
                Usuario usuarioAux = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbUsuario";
                if(usuarioFiltro.id_usuario>0){
                    cnn.parametros.Add("@idUsuario", usuarioFiltro.id_usuario);
                }
                if(usuarioFiltro.estado>=0){
                    //cnn.parametros.Add("@idEstadoVig", usuarioFiltro.estado);
                }
                if(usuarioFiltro.regionUsuario!=null && usuarioFiltro.regionUsuario.id_region>0){
                    cnn.parametros.Add("@idRegion", usuarioFiltro.regionUsuario.id_region);
                }

                /*
                if(usuarioFiltro.tipoUsuario!=null && usuarioFiltro.tipoUsuario.id>0){
                    cnn.parametros.Add("@idTipoUsuario", usuarioFiltro.tipoUsuario.id);
                }
                 * */
                if(usuarioFiltro.RUT>0){
                    cnn.parametros.Add("@rutUsuario", usuarioFiltro.RUT);
                }
                if(!usuarioFiltro.usuario.Equals("")){
                    cnn.parametros.Add("@usuario", usuarioFiltro.usuario);
                }
                
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        usuarioAux = new Usuario();
                        usuarioAux.id_usuario = Convert.ToInt32(row["idUsuario"]);
                        usuarioAux.estado = Convert.ToInt32(row["idEstadoVig"]);
                        usuarioAux.regionUsuario = new Region();
                        usuarioAux.regionUsuario.id_region = Convert.ToInt32(row["idRegion"]);
                        usuarioAux.regionUsuario.region = row["Region"].ToString();
                        //usuarioAux.tipoUsuario = new ParametroGenerico(Convert.ToInt32(row["idTipoUsuario"]), row["nombreTipo"].ToString());
                        usuarioAux.RUT = Convert.ToInt32(row["rutUsuario"]);
                        usuarioAux.dvUsuario = Convert.ToChar(row["dvUsuario"]);
                        usuarioAux.nombre = row["nombresUsuario"].ToString();
                        usuarioAux.apellidos = row["apellidosUsuario"].ToString();
                        usuarioAux.correo = row["email"].ToString();
                        usuarioAux.usuario = row["usuario"].ToString();

                    }
                }

                return usuarioAux;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            }
        }


        public Usuario.Serializable ObtieneRbUsuarioSerializable(Usuario usuarioFiltro)
        {
            try
            {
                Usuario.Serializable usuarioAux = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbUsuario";
                if (usuarioFiltro.id_usuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", usuarioFiltro.id_usuario);
                }
                if (usuarioFiltro.estado >= 0)
                {
                    //cnn.parametros.Add("@idEstadoVig", usuarioFiltro.estado);
                }
                if (usuarioFiltro.regionUsuario != null && usuarioFiltro.regionUsuario.id_region > 0)
                {
                    cnn.parametros.Add("@idRegion", usuarioFiltro.regionUsuario.id_region);
                }

                /*
                if(usuarioFiltro.tipoUsuario!=null && usuarioFiltro.tipoUsuario.id>0){
                    cnn.parametros.Add("@idTipoUsuario", usuarioFiltro.tipoUsuario.id);
                }
                 * */
                if (usuarioFiltro.RUT > 0)
                {
                    cnn.parametros.Add("@rutUsuario", usuarioFiltro.RUT);
                }
                if (!usuarioFiltro.usuario.Equals(""))
                {
                    cnn.parametros.Add("@usuario", usuarioFiltro.usuario);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        usuarioAux = new Usuario.Serializable();
                        usuarioAux.id_usuario = Convert.ToInt32(row["idUsuario"]);
                        usuarioAux.estado = Convert.ToInt32(row["idEstadoVig"]);
                        usuarioAux.regionUsuario = new Region();
                        usuarioAux.regionUsuario.id_region = Convert.ToInt32(row["idRegion"]);
                        usuarioAux.regionUsuario.region = row["Region"].ToString();
                        //usuarioAux.tipoUsuario = new ParametroGenerico(Convert.ToInt32(row["idTipoUsuario"]), row["nombreTipo"].ToString());
                        usuarioAux.RUT = Convert.ToInt32(row["rutUsuario"]);
                        usuarioAux.dvUsuario = Convert.ToChar(row["dvUsuario"]);
                        usuarioAux.nombre = row["nombresUsuario"].ToString();
                        usuarioAux.apellidos = row["apellidosUsuario"].ToString();
                        usuarioAux.correo = row["email"].ToString();
                        usuarioAux.usuario = row["usuario"].ToString();

                    }
                }

                return usuarioAux;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            }
        }

        public List<Usuario> ListarRbUsuario(Usuario usuarioFiltro)
        {
            try
            {
                Usuario usuarioAux = null;
                List<Usuario> resp = new List<Usuario>();
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbUsuario";
                if (usuarioFiltro.id_usuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", usuarioFiltro.id_usuario);
                }
                if (usuarioFiltro.estado >= 0)
                {
                    //cnn.parametros.Add("@idEstadoVig", usuarioFiltro.estado);
                }
                if (usuarioFiltro.regionUsuario != null && usuarioFiltro.regionUsuario.id_region > 0)
                {
                    cnn.parametros.Add("@idRegion", usuarioFiltro.regionUsuario.id_region);
                }
                /*
                if (usuarioFiltro.tipoUsuario != null && usuarioFiltro.tipoUsuario.id > 0)
                {
                    cnn.parametros.Add("@idTipoUsuario", usuarioFiltro.tipoUsuario.id);
                }
                 * */
                if (usuarioFiltro.RUT > 0)
                {
                    cnn.parametros.Add("@rutUsuario", usuarioFiltro.RUT);
                }

                if (usuarioFiltro.usuario != null && !usuarioFiltro.usuario.Equals(""))
                {
                    cnn.parametros.Add("@usuario", usuarioFiltro.usuario);
                }

                if (usuarioFiltro.nombApelli != null && !usuarioFiltro.nombApelli.Equals(""))
                {
                    cnn.parametros.Add("@nombApelli", usuarioFiltro.nombApelli);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        usuarioAux = new Usuario();
                        usuarioAux.id_usuario = Convert.ToInt32(row["idUsuario"]);
                        usuarioAux.estado = Convert.ToInt32(row["idEstadoVig"]);
                        usuarioAux.regionUsuario = new Region();
                        usuarioAux.regionUsuario.id_region = Convert.ToInt32(row["idRegion"]);
                        usuarioAux.regionUsuario.region = row["Region"].ToString();
                        //usuarioAux.tipoUsuario = new ParametroGenerico(Convert.ToInt32(row["idTipoUsuario"]), row["nombreTipo"].ToString());
                        usuarioAux.RUT = Convert.ToInt32(row["rutUsuario"]);
                        usuarioAux.dvUsuario = Convert.ToChar(row["dvUsuario"]);
                        usuarioAux.nombre = row["nombresUsuario"].ToString();
                        usuarioAux.apellidos = row["apellidosUsuario"].ToString();
                        usuarioAux.correo = row["email"].ToString();
                        usuarioAux.usuario = row["usuario"].ToString();
                        if (Convert.ToInt32(row["idEstadoVig"]) == 6)
                        {
                            usuarioAux._estado = "Vigente";
                        }
                        else 
                        {
                            usuarioAux._estado = "No Vigente";
                        }
                        resp.Add(usuarioAux);

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

        public Usuario.Serializable ObtieneUsuarioLogin(Usuario usuarioFiltro)
        {
            try
            {
                Usuario.Serializable usuarioAux = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbUsuarioLogin";

                cnn.parametros.Add("@usuario", usuarioFiltro.usuario);
                cnn.parametros.Add("@contrasenia", usuarioFiltro.clave);


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        usuarioAux = new Usuario.Serializable();
                        usuarioAux.id_usuario = Convert.ToInt32(row["idUsuario"]);
                        usuarioAux.estado = Convert.ToInt32(row["idEstadoVig"]);
                        usuarioAux.regionUsuario = new Region();
                        usuarioAux.regionUsuario.id_region = Convert.ToInt32(row["idRegion"]);
                        usuarioAux.regionUsuario.region = row["Region"].ToString();
                        //usuarioAux.tipoUsuario = new ParametroGenerico(Convert.ToInt32(row["idTipoUsuario"]), row["nombreTipo"].ToString());
                        usuarioAux.RUT = Convert.ToInt32(row["rutUsuario"]);
                        usuarioAux.dvUsuario = Convert.ToChar(row["dvUsuario"]);
                        usuarioAux.nombre = row["nombresUsuario"].ToString();
                        usuarioAux.apellidos = row["apellidosUsuario"].ToString();
                        usuarioAux.correo = row["email"].ToString();
                        usuarioAux.usuario = row["usuario"].ToString();

                    }
                }

                return usuarioAux;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            }
        }

        public DataTable ListarUsuarioPrivRegionales(int id_usuario, int idTipoTramite, string subtramite)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbUsuarioPrivRegionales";
            cnn.parametros.Add("@id_usuario", id_usuario);
            if (idTipoTramite > 0)
            {
                cnn.parametros.Add("@idTipoTramite", idTipoTramite);
            }
            if (subtramite != null && !subtramite.Equals(""))
            {
                cnn.parametros.Add("@subTipoTramCad", subtramite);
            }

            DataTable dt = cnn.Execute();
            return dt;
        }

        public DataTable ListarUsuariosPrivComunales(int id_region, int id_usuario, int idTipoTramite, string subTipoTramiteCad)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbUsuariosPrivComunales";
            cnn.parametros.Add("@id_usuario", id_usuario);
            cnn.parametros.Add("@id_region", id_region);
            if(idTipoTramite>0){
                cnn.parametros.Add("@idTipoTram", idTipoTramite);
            }
            if (subTipoTramiteCad != null && !subTipoTramiteCad.Equals(""))
            {
                cnn.parametros.Add("@subTipoTramCad", subTipoTramiteCad);
            }
            
            DataTable dt = cnn.Execute();
            return dt;
        }

        public bool ActualizarUsuarioPrivComunales(int id_usuario, int id_comuna, bool prv, int idTipoTramite, int idSubTipoTramite)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paUpdRbUsuarioPrivComunales";
            cnn.parametros.Add("@id_usuario", id_usuario);
            cnn.parametros.Add("@id_comuna", id_comuna);
            cnn.parametros.Add("@prv", prv);
            if (idTipoTramite > 0)
            {
                cnn.parametros.Add("@idTipoTramite", idTipoTramite);
            }
            if (idSubTipoTramite > 0)
            {
                cnn.parametros.Add("@idSubTipoTramite", idSubTipoTramite);
            }

            bool result = false;
            try
            {
                DataTable dt = cnn.Execute();
                result = Convert.ToBoolean(dt.Rows[0]["msg"]);
            }
            catch(Exception e)
            {
                
            };

            return result;
        }

        public bool GuardarSolicitudUsuario(int idUsuario,int idTipoSolicitud,int claveSolicitud,bool ingresoManual)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paInsRbSolicitudUsuario";
            cnn.parametros.Add("@idUsuario", idUsuario);
            cnn.parametros.Add("@idTipoSolicitud", idTipoSolicitud);
            cnn.parametros.Add("@claveSolicitud", claveSolicitud);
            cnn.parametros.Add("@ingresoManual", ingresoManual);

            bool result = false;
            try
            {
                DataTable dt = cnn.Execute();
                result = Convert.ToBoolean(dt.Rows[0]["msg"]);
            }
            catch { };

            return result;
        }

        public bool EliminarSolicitudUsuario(int idUsuario, int idTipoSolicitud, int claveSolicitud)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbSolicitudUsuario";
                cnn.parametros.Add("@idUsuario", idUsuario);
                cnn.parametros.Add("@idTipoSolicitud", idTipoSolicitud);
                if (claveSolicitud > 0)
                {
                    cnn.parametros.Add("@claveSolicitud", claveSolicitud);
                }

                DataTable dt = cnn.Execute();

                int resul = Convert.ToInt32(dt.Rows[0]["resultado"]);
                if (resul >= 0) return true;

                return false;

            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        /*
        public bool ActualizarUsuarioPrivComunales(int idUsuario)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paUpdSolicitudUsuarioPert";
            cnn.parametros.Add("@idUsuario", idUsuario);
            
            int result;
            try
            {
                DataTable dt = cnn.Execute();
                result = Convert.ToInt32(dt.Rows[0]["idusuario"]);
                
                if (result >= 0) return true;
                return false;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
           
        }
        */

        public DataTable ListarUsuarioRol(int idTipoRol)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbUsuarioRol";
            cnn.parametros.Add("@idTipoRol", idTipoRol);
            
            DataTable dt = cnn.Execute();
            return dt;
        }

        public bool aplicaUsuarioDespliegueFiltro(int idUsuario)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbUsuarioDespliegueFiltro";
                cnn.parametros.Add("@idUsuario", idUsuario);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        if (!row.IsNull("idUsuario"))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return true;
            }
        }


        public DataTable ListarPertUsuario(int idUsuario)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbSolicitudUsuarioAsignadas";
            cnn.parametros.Add("@idUsuario", idUsuario);

            DataTable dt = cnn.Execute();
            return dt;
        }

        public bool EliminarLogicamente(int id_usuario)
        {
            try 
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbUsuarioLogico";
                cnn.parametros.Add("@idUsuario", id_usuario);

                cnn.Execute();
                
                return true;
            }
            catch 
            {
                return false;
            }
            
        }

        public DataTable ListarLogTransacciones(string tabla, string accion, string identificador,DateTime fechaIni, DateTime fechaFin)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbLogTransacciones";
            if(tabla!=null && !tabla.Equals("")){
                cnn.parametros.Add("@tabla", tabla);
            }
            if (accion != null && !accion.Equals("")){
                cnn.parametros.Add("@tipoTrn", accion);
            }
            if (identificador != null && !identificador.Equals(""))
            {
                cnn.parametros.Add("@identificador", identificador);
            }
            if (fechaIni != null && fechaIni != default(DateTime)){
                cnn.parametros.Add("@fechaIni", fechaIni);
            }
            if (fechaFin != null && fechaFin != default(DateTime)){
                cnn.parametros.Add("@fechaTerm", fechaFin);
            }

            DataTable dt = cnn.Execute();
            return dt;
        }

    }
}
