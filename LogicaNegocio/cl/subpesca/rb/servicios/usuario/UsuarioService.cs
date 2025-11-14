using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using System.Transactions;
using LogicaNegocio.cl.subpesca.rb.usuario;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using System.Collections;

namespace LogicaNegocio.cl.subpesca.rb.servicios.usuario
{
    public class UsuarioService
    {

        Logger logger = new Logger();
        UsuarioDA usuarioDA = new UsuarioDA();
        SolicitudDA solicitudDA = new SolicitudDA();
        

        //GUARDA LOS SECTORES DE UN TRAMITE DE RELOCALIZACION (EL TRAMITE EN GENERAL YA FUE INGRESADO)
        public int GuardarRbUsuario(Datos.Entidades.Usuario.Serializable nuevoUsuario, Datos.Entidades.Usuario.Serializable nuevoLogeado)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!usuarioDA.GuardarRbUsuario(nuevoUsuario)) {
                        return 0;
                    }

                    transactionScope.Complete();
                    return nuevoUsuario.id_usuario;
                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return 0;
                }

            }
        }


        public Usuario ObtenerRbUsuario(Usuario usuarioFiltro)
        {
            return usuarioDA.ObtieneRbUsuario(usuarioFiltro);
        }


        public Usuario.Serializable ObtenerRbUsuarioSerializable(Usuario usuarioFiltro)
        {
            return usuarioDA.ObtieneRbUsuarioSerializable(usuarioFiltro);
        }

        public bool ValidarRbUsuario(int id_usuario, string objeto, string valor)
        {
            return true;
        }

        public List<Usuario> ListarRbUsuario(Usuario usuarioFiltro) 
        {
            return usuarioDA.ListarRbUsuario(usuarioFiltro);
        }

        public Usuario.Serializable ObtieneUsuarioLogin(Usuario usuarioFiltro)
        {
            return usuarioDA.ObtieneUsuarioLogin(usuarioFiltro);
        }


        public DataTable ListarUsuarioPrivRegionales(int id_usuario,int tipoTramite, string subtramite) 
        {
            return usuarioDA.ListarUsuarioPrivRegionales(id_usuario, tipoTramite, subtramite);
        }


        public DataTable ListarUsuariosPrivComunales(int id_region, int id_usuario, int tipotramite, string subtipotramite)
        {
            return usuarioDA.ListarUsuariosPrivComunales(id_region, id_usuario,tipotramite,subtipotramite);
        }

        public DataTable ListarPertUsuario(int idUsuario)
        {
            return usuarioDA.ListarPertUsuario(idUsuario);
        }

        public DataTable ListarTramitesSolicitudes(int idTipoTramite, int idUsuario, string pert) 
        {
            return solicitudDA.ListarTramitesSolicitudes(idTipoTramite, idUsuario, pert);
        }

        public bool GuardarSolicitudUsuario(int idUsuario, int idTipoSolicitud, int claveSolicitud, bool ingresoManual) 
        {
            return usuarioDA.GuardarSolicitudUsuario(idUsuario, idTipoSolicitud, claveSolicitud, ingresoManual);      
        }

        public bool EliminarSolicitudUsuario(int idUsuario, int idTipoSolicitud, int claveSolicitud)
        {
            return usuarioDA.EliminarSolicitudUsuario(idUsuario, idTipoSolicitud, claveSolicitud);
        }



        public bool ActualizarUsuarioPrivComunales(int id_usuario, int id_comuna, bool prv)
        {
            return usuarioDA.ActualizarUsuarioPrivComunales(id_usuario, id_comuna, prv,0,0);
        }


        public bool ActualizarUsuarioPrivComunalesPert(int id_usuario, Hashtable HT_Regiones)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    Hashtable HT_Region = new Hashtable();
                    Hashtable HT_Comunas = new Hashtable();
                    Hashtable HT_Comuna = new Hashtable();
                    int id_comuna = 0;
                    bool prv = false;
                    bool result = false;

                    foreach (DictionaryEntry r in HT_Regiones)
                    {
                        HT_Region = (Hashtable)r.Value;
                        HT_Comunas = (Hashtable)HT_Region["Comunas"];
                        foreach (DictionaryEntry c in HT_Comunas)
                        {
                            HT_Comuna = (Hashtable)c.Value;
                            id_comuna = (int)c.Key;
                            prv = (bool)HT_Comuna["prv"];
                            result = this.ActualizarUsuarioPrivComunales(id_usuario, id_comuna, prv);

                            if (!result) {
                                return false;
                            }
                        };
                    };

                    /*

                    //ASIGNAR LAS SOLICITUDES A LOS USUARIOS 
                    if (!usuarioDA.ActualizarUsuarioPrivComunales(id_usuario)) {
                        return false;
                    }
                     * */

                    transactionScope.Complete();
                    return true;

                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }

            }
        }



        public List<int> ListarCheckMarcados(int idTipoTramite, int idUsuario, string pert)
        {
            return solicitudDA.ListarCheckMarcados(idTipoTramite, idUsuario, pert);
        }

        public DataTable ListarUsuarioPrivRegionalesPorTramite(int id_usuario, int tipoTramite, string subtramite)
        {
            return usuarioDA.ListarUsuarioPrivRegionales(id_usuario, tipoTramite, subtramite);  
        }

        public DataTable ListarUsuariosPrivComunalesPorTramite(int id_region, int id_usuario, int tipoTramite, string subtipotramite)
        {
            return usuarioDA.ListarUsuariosPrivComunales(id_region, id_usuario, tipoTramite, subtipotramite);
        }

        public bool ActualizarUsuarioPrivComunalesPertPorTramite(int id_usuario, Hashtable HT_Regiones, int tipoTramite,int subtimotramite)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    Hashtable HT_Region = new Hashtable();
                    Hashtable HT_Comunas = new Hashtable();
                    Hashtable HT_Comuna = new Hashtable();
                    int id_comuna = 0;
                    bool prv = false;
                    bool result = false;

                    foreach (DictionaryEntry r in HT_Regiones)
                    {
                        HT_Region = (Hashtable)r.Value;
                        HT_Comunas = (Hashtable)HT_Region["Comunas"];
                        foreach (DictionaryEntry c in HT_Comunas)
                        {
                            HT_Comuna = (Hashtable)c.Value;
                            id_comuna = (int)c.Key;
                            prv = (bool)HT_Comuna["prv"];
                            result = this.ActualizarUsuarioPrivComunalesPorTipoTramite(id_usuario, id_comuna, prv, tipoTramite, subtimotramite);

                            if (!result)
                            {
                                return false;
                            }
                        };
                    };

                    transactionScope.Complete();
                    return true;

                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }

            }
        }

        private bool ActualizarUsuarioPrivComunalesPorTipoTramite(int id_usuario, int id_comuna, bool prv, int tipoTramite,int subtipotramite)
        {
            return usuarioDA.ActualizarUsuarioPrivComunales(id_usuario, id_comuna, prv, tipoTramite, subtipotramite);
        }

    }
}
