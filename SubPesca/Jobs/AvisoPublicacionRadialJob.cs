using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;
using LogicaNegocio.cl.subpesca.rb.errores;



namespace Subpesca.Jobs
{
    
    /**
     * Alerta 23 
     */
    public class AvisoPublicacionRadialJob : IJob 
    {

        EnviarCorreo enviarCorreo = new EnviarCorreo();
        Logger logger = new Logger();

        public void Execute(IJobExecutionContext context) {

            try
            {

                enviarCorreo.avisoPublicacionRadial(0);
        
            }
            catch (Exception exception)
            {

                logger.PrintError(exception);
                logger.SendMailError(exception);
            }

        }
    }
}
