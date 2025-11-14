using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.relocalizacion;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace LogicaNegocio.cl.subpesca.rb.servicios.acceso
{
    public class DespliegueSeccionService
    {

        Logger logger = new Logger();
        TramiteRelocalizacionDA tramiteRelocalizacionDA = new TramiteRelocalizacionDA();
        DespliegueMenuSeccionDA despliegueMenuSeccionDA = new DespliegueMenuSeccionDA();

        //SE DETERMINA SI UNA SECCION SE DEBE O NO MOSTRAR, DEPENDE DE:
        // 1) QUE LA SECCION (GENERICA) APLIQUE PARA LA UNIDAD ESPACIAL 
        // 2) QUE EL ROL DEL USUARIO LE DE ACCESO A LA SECCION (ESPECIFICA) 
        public bool ObtieneDespliegueSeccion(int idSeccionGenerica, int idTipoUnidadEspecial)
        {
            try
            {
                return tramiteRelocalizacionDA.ObtieneDespliegueSeccionUnidad(idSeccionGenerica, idTipoUnidadEspecial);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }


        //SE DETERMINA SI UNA SECCION SE DEBE O NO MOSTRAR, DEPENDE DE:
        // 1) QUE LA SECCION (GENERICA) APLIQUE PARA LA UNIDAD ESPACIAL 
        // 2) QUE EL ROL DEL USUARIO LE DE ACCESO A LA SECCION (ESPECIFICA) 
        public bool ObtieneDespliegueSeccion2(int idSeccionGenerica, int idTipoUnidadEspecial, int idUsuario, int idSeccionEspecifica)
        {
            try
            {
                return tramiteRelocalizacionDA.ObtieneDespliegueSeccionUnidad(idSeccionGenerica, idTipoUnidadEspecial);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }


        //SECCIONES VISIBLES EN LAS MODIFICACIONES (SOLO PARA SOLICITUDES DE CONCESIONES)
        public List<DespliegueMenuSeccion> ListarDespliegueMenuSeccion(int idMenu, int idSeccion)
        {
            try
            {
                return despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(idMenu, idSeccion);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        //SECCIONES VISIBLES EN LAS MODIFICACIONES (PARA UNIDADES ESPACIALES QUE NO SEAN SOLICITUDES DE CONCESIONES)
        public List<DespliegueMenuSeccion> ListarDespliegueMenuSeccion_UE(int idMenu, int idSeccion, int idTipoUnidEspacial)
        {
            try
            {
                return despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(idMenu, idSeccion, idTipoUnidEspacial);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


    }
}
