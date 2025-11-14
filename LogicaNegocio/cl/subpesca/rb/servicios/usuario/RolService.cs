using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.usuario;
using System.Data;

namespace LogicaNegocio.cl.subpesca.rb.servicios.usuario
{
    public class RolService
    {

        RolDA rolDA = new RolDA();

        public List<Rol> ListarRoles(Rol rol)
        {
            return rolDA.ListarRoles(rol);
        }

        public System.Data.DataTable ListarAccionesSeccionRol(int idRol, int idMenu)
        {
            return rolDA.ListarAccionesSeccionRol(idRol, idMenu);
        }


        public bool GuardarSeccionRol(int idRol, int idSeccionSist, int idAccion)
        {
            return rolDA.GuardarSeccionRol(idSeccionSist, idRol, idAccion);
        }

        public bool EliminarSeccionRol(int idRol, int idSeccion, int idAccion)
        {
            return rolDA.EliminarSeccionRol(idSeccion, idRol, idAccion);
        }

        public DataTable GuardarRol(int idRol, string nombreRol, int idTipoRol, bool aplicaDespliegueFiltro) 
        {
            return rolDA.GuardarRol(idRol, nombreRol, idTipoRol, aplicaDespliegueFiltro);
        }

        public DataTable EliminarRol(int idRol)
        {
            return rolDA.EliminarRol(idRol);
        }


        public DataTable ListarRolesUsuario(int idUsuario, int idEstadoVig) 
        { 
            return rolDA.ListarRolesUsuario(idUsuario, idEstadoVig);
        }


        public bool GuardarUsuarioRol(int idUsuario, int idRol)
        {
            return rolDA.GuardarUsuarioRol(idUsuario, idRol);
        }


        public bool EliminarUsuarioRol(int idRol, int idUsuario)
        {
            return rolDA.EliminarUsuarioRol(idUsuario, idRol);
        }

    }

}
