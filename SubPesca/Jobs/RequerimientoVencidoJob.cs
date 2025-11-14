using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;

namespace Subpesca.Jobs
{
    /**
     * (Alerta 19)
     */
    public class RequerimientoVencidoJob : IJob 
    {

        EnviarCorreo enviarCorreo = new EnviarCorreo();
        
        public void Execute(IJobExecutionContext context) {
            try
            {

                enviarCorreo.alertaPlazoVencido(0);

            }
            catch (Exception)
            {


            }
        }
    }
}
