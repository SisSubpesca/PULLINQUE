using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Validaciones.cl.subpesca.rb.modificacion
{
    public class DatosConcesionAcuiculturaValidacion
    {
        /**
        * Método que valida el ingreso de las superficies 
        */
        public List<string> validaSuperficies(Datos.Entidades.SolicitudConcesion solicitudModificacion)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            if (solicitudModificacion != null)
            {
                /* Superficie total final debe ser mayor que 0 */
                if (solicitudModificacion.superficieTramFinal <= 0)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("La Superficie Total Final debe ser mayor que 0.");
                }

                /* Superfice del Trámite Ampl/Reduc requerida debe ser mayor que 0*/
                if (solicitudModificacion.superficieTramReq <= 0)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("La Superficie Amp/Reduc Requerida debe ser mayor que 0.");
                }
            }
            return listaErrroresSolicitudConcesionAcuicultura;
        }
    }
}
