using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Validaciones.cl.subpesca.rb.solicitud
{
    public class GeneralValidacion
    {
        /**
         *  Método que valida el ingreso de la solicitud de concesion de acuicultura.
         */
        public List<String> validaGeneral(Datos.Entidades.SolicitudConcesion solicitudConcesion)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            if (solicitudConcesion != null)
            {
                DateTime systemDate = DateTime.Now;

                /* Fecha de Ingreso a Trámite no debe ser mayor a la fecha del día de hoy */
                if (solicitudConcesion.fechaIngresoTramite != default(DateTime) && solicitudConcesion.fechaIngresoTramite > systemDate)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Ingreso a Trámite no debe ser mayor a la fecha de hoy.");
                }

                /* Fecha de Recepción no debe ser mayor a la fecha del día de hoy */
                if (solicitudConcesion.fechaRecepcion != default(DateTime) && solicitudConcesion.fechaRecepcion > systemDate)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Recepción no debe ser mayor a la fecha de hoy.");
                }

            }
            return listaErrroresSolicitudConcesionAcuicultura;
        }
    }
}
