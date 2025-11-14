using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;

using LogicaNegocio.cl.subpesca.rb.errores;

namespace SubPesca.Jobs
{
    public class TitularesPendientesCreacion : IJob 
    {
        Logger logger = new Logger();

        public void Execute(IJobExecutionContext context)
        {

            try
            {

                

            }
            catch (Exception exception)
            {

                logger.PrintError(exception);
                logger.SendMailError(exception);
            }

        }
    }
}