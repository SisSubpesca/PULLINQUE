using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Entidades;
using System.Collections;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.usuario;
using LogicaNegocio.cl.subpesca.rb.errores;

namespace LogicaNegocio.cl.subpesca.rb.servicios.acceso
{
    public class PermisosService
    {


        public RolDA rolDA = new RolDA();
        Logger logger = new Logger();

        public bool tieneAccesoA() {


            return true;
        }



        /**
         * VERIFICA SI TIENE ACCESO A LA SECCIÓN Y/O ACCION
         * SE PUEDE ENVIAR MAS DE UNA SECCION (PARA EL CASO DE MODIFICACIONES DE ACUICULTURA)
         **/
        public bool tieneAccesoA2(int[] dbSeccionesEspecifica, Usuario.Serializable usuario, SolicitudConcesion solicitud, int accionRealizar)
        {
            //return true;

            if (usuario == null || usuario.accesosRB == null || usuario.accesosRB.Count == 0)
            {
                return false;
            }

            //EL USUARIO LOGEADO NO TIENE ASIGNADO LA SOLICITUD CON LA CUAL SE ESTA TRABAJANDO (NO IMPORTA QUE TENGA LA ACCION, SE LE NIEGA EL PERMISO, SALVO AL VER)
            if (solicitud != null && solicitud.tieneAsignadaSolicitud == false && accionRealizar != rbAccion.VER) {
                return false;
            }

            if (dbSeccionesEspecifica == null || dbSeccionesEspecifica.Count() == 0) {
                return false;
            }


            bool tieneAccesoSeccion = false;
            Hashtable seccionesConAcceso = new Hashtable();

            foreach (int idSeccionEspecifica in dbSeccionesEspecifica)
            {
                if (usuario.accesosRB.ContainsKey(idSeccionEspecifica))
                {
                    tieneAccesoSeccion = true;
                    seccionesConAcceso.Add(idSeccionEspecifica, idSeccionEspecifica);
                } 
            }


            if (!tieneAccesoSeccion) {
                
                return false;

            }else {

                //SOLO SE QUIERE SABER SI TIENE ACCESO A LA SECCION
                if (accionRealizar < 1)
                {
                    return true;
                }
                else
                {

                    foreach (DictionaryEntry seccion in seccionesConAcceso)
                    {

                        //TIENE PERMISO A LA SECCIÓN, Y SE QUIERE SABER SI TIENE ACCESO A LA ACCION
                        Hashtable acciones = (Hashtable)usuario.accesosRB[(int)seccion.Key];

                        if (acciones != null)
                        {
                            if (acciones.ContainsKey(accionRealizar))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;

        }


        //OBTIENE TODAS LAS SECCIONES A LA CUAL EL USUARIO TIENE ACCESO Y TODAS LA ACCIONES, DENTRO DE DICHA SECCION, EN LA CUAL EL USUARIO PUEDE REALIZAR
        public Hashtable obtenerPermisos(int idUsuario) {

            try
            {
                return rolDA.ListarSeccionesUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }


            /*
        
            Hashtable secciones = new Hashtable();
            Hashtable acciones;

            secciones.Add(rbSeccionUnidadEspacial.RESOLUCION_SSFFAA_ACOPIO, new Hashtable());

            acciones = (Hashtable)secciones[rbSeccionUnidadEspacial.RESOLUCION_SSFFAA_ACOPIO];
            acciones.Add(rbAccion.VER, 1);
            acciones.Add(rbAccion.EDITAR, 1);
            acciones.Add(rbAccion.ADMINISTRAR_REQUERIMIENTO, 1);
            acciones.Add(rbAccion.VIGENTE, 1);
            acciones.Add(rbAccion.NO_VIGENTE, 1);
            acciones.Add(rbAccion.ELIMINAR, 1);


            secciones.Add(rbSeccionUnidadEspacial.OBSERVACIONES_RESOLUCION_SSFFAA_ACOPIO, new Hashtable());
            
            acciones = (Hashtable)secciones[rbSeccionUnidadEspacial.OBSERVACIONES_RESOLUCION_SSFFAA_ACOPIO];
            acciones.Add(rbAccion.EDITAR, 1);
            

            return secciones;
             * 
             **/

        }



       
    }
}
