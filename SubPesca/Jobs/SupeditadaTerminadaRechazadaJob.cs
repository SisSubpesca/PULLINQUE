using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;

namespace SubPesca.Jobs
{
    public class SupeditadaTerminadaRechazadaJob : IJob
    {
        EnviarCorreo enviarCorreo = new EnviarCorreo();

        public void Execute(IJobExecutionContext context)
        {

            try
            {
                enviarCorreo.supeditadaTerminadaRechazada(0);

            }
            catch (Exception)
            {


            }
        }
    }
}