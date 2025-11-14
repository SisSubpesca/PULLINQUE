using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Entidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace Validaciones.cl.subpesca.rb.solicitud
{
    public class IdentificacionSolicitanteValidacion
    {

        SolicitudDA solicitudDA = new SolicitudDA();

        public List<String> validaIdentificacionSolicitante(Solicitante solicitante)
        {
            List<String> listaErrroressolicitante = new List<String>();
            if (solicitante != null)
            {
                if (solicitante.accion == rbAccion.ELIMINAR)
                {
                    /* No puede eliminar el solicitante si existe requerimiento pendiente asociado al Titular */
                    List<Requerimiento> listaRequerimientosPendientes = solicitudDA.ListarPequerimientosPendPersona(solicitante.solicitud.idSolConcesion, 0);
                    if (listaRequerimientosPendientes != null && listaRequerimientosPendientes.Count > 0)
                    {
                        bool existe = false;
                        foreach (Requerimiento requerimiento in listaRequerimientosPendientes)
                        {

                            if (requerimiento != null && requerimiento.solicitud != null && requerimiento.solicitud.idSolConcesion == solicitante.solicitud.idSolConcesion)
                            {
                                existe = true;
                            }
                        }

                        if (existe)
                        {
                            listaErrroressolicitante.Add("No puede eliminar el solicitante si existe requerimiento pendiente asociado al Titular en la solicitud.");
                        }
                    }

                }
            
            }
            return listaErrroressolicitante;
        }
    }
}
