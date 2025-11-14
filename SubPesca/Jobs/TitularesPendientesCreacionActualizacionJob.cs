using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using LogicaNegocio.cl.subpesca.rb.errores;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace SubPesca.Jobs
{
    public class TitularesPendientesCreacionActualizacionJob
    {
        Logger logger = new Logger();
        TitularRegConcesionesDA titularRegConcesionesDA = new TitularRegConcesionesDA();

        public void Execute(IJobExecutionContext context)
        {

            try
            {
                if (!titularRegConcesionesDA.ActualizarEstadoTitularRegConcesiones())
                {

                    throw new Exception("Error al intentar actualizar titulares.");
                }
            }
            catch(Exception exception)
            {
                logger.PrintError(exception);
                logger.SendMailError(exception);
            }

        }

    }
}