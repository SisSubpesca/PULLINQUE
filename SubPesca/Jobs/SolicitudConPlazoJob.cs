using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;


namespace Subpesca.Jobs
{
    public class SolicitudConPlazoJob : IJob 
    {

        EnviarCorreo enviarCorreo = new EnviarCorreo();
        
        public void Execute(IJobExecutionContext context) {

            try
            {
                enviarCorreo.solicitudCambiaEstadoConPlazo(0);

            }
            catch (Exception)
            {


            }
        }
    }
}
