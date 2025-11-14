using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;
using Quartz;

namespace SubPesca.Jobs
{
    public class SupeditadaTerminadaAprobadaJob : IJob
    {
        EnviarCorreo enviarCorreo = new EnviarCorreo();

        public void Execute(IJobExecutionContext context)
        {

            try
            {
                enviarCorreo.supeditadaTerminadaAprobada(0);

            }
            catch (Exception)
            {


            }
        }
    }
}