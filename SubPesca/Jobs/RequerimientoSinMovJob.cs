using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;


namespace Subpesca.Jobs
{
    /**
     * (Alerta 6) 
     */
    public class RequerimientoSinMovJob : IJob 
    {
        EnviarCorreo enviarCorreo = new EnviarCorreo();
        
        public void Execute(IJobExecutionContext context) {

            try
            {
                enviarCorreo.alertaRequerimientoSinMov(0);

            }
            catch (Exception)
            {


            }

        }
    }
}
