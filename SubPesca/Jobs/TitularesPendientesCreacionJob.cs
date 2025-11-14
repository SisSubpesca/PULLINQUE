using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using LogicaNegocio.cl.subpesca.rb.errores;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace SubPesca.Jobs
{
    public class TitularesPendientesCreacionJob : IJob 
    {
        Logger logger = new Logger();
        TitularRegConcesionesDA titularRegConcesionesDA = new TitularRegConcesionesDA();


        public void Execute(IJobExecutionContext context)
        {

            try
            {
              if(!titularRegConcesionesDA.InsertarPersonasLegalesSolicitud_RCA())
              {
                  throw new Exception("Error al intentar actualizar titulares producto de transferencia RCA."); 
              }

            }
            catch (Exception exception)
            {

                logger.PrintError(exception);
                logger.SendMailError(exception);
            }

        }
    }
}