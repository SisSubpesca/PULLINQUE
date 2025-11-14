using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.modificacion;

namespace Validaciones.cl.subpesca.rb.modificacion
{
    public class UnidadDependenciaValidacion
    {

        UnidadDependenciaModDA unidadDependenciaModDA = new UnidadDependenciaModDA();

        public List<string> validaDetalleUnidadDependenciaMod(UnidadDependenciaMod unidadDependenciaMod, List<UnidadDependenciaMod> unidadDependenciaList)
        {
            List<string> erroresDependenciaModList = new List<string>();
            //if (unidadDependenciaMod != null)
            //{
            //    /* La combinación tipo unidad dependencia y clave debe ser valida */
            //    bool dependenciaValida = unidadDependenciaModDA.ObtieneValidarUnidadDependencia(unidadDependenciaMod.tipoUnidDependencia.id, unidadDependenciaMod.claveUnidDependencia);
            //    if (!dependenciaValida)
            //    {
            //        erroresDependenciaModList.Add("La unidad de dependencia ingresada no existe en el sistema.");
            //    }
                
            //    /* No debe existir en la lista */
            //    foreach (UnidadDependenciaMod unidDependAux in unidadDependenciaList)
            //    {
            //        if (unidadDependenciaMod != null && unidadDependenciaMod.tipoUnidDependencia != null && unidDependAux.tipoUnidDependencia != null && unidadDependenciaMod.tipoUnidDependencia.id == unidDependAux.tipoUnidDependencia.id
            //            && unidadDependenciaMod.claveUnidDependencia.Equals(unidDependAux.claveUnidDependencia))
            //        {
            //            erroresDependenciaModList.Add("La unidad de dependencia ingresada ya está dentro del listado de dependencias de la solicitud de modificación.");
            //        }
            //    }

            //    /* La unidad de dependencia ingresada no debe ser el mismo identificador o pert de la solicitud */
            //    int idSolicitudConcesion = unidadDependenciaModDA.ObtenerSolicitudUnidadDepExistente(unidadDependenciaMod.tipoUnidDependencia.id, unidadDependenciaMod.claveUnidDependencia);
            //    if (idSolicitudConcesion == unidadDependenciaMod.idSolConcesion)
            //    {
            //        erroresDependenciaModList.Add("No puede agregar su solicitud como unidad de dependencia de sí misma.");
            //    }


            //}
            return erroresDependenciaModList;
        }
    }
}
