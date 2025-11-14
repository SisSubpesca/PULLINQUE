using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;

namespace Subpesca.Jobs
{
    /**
     *  (Alerta 4)
     */
    public class RequerimientoCambioEstadoVencJob : IJob 
    {
        EnviarCorreo enviarCorreo = new EnviarCorreo();
        
        public void Execute(IJobExecutionContext context) {
            try
            {
                //Se crea nuevo método para la obtención de destinatarios por tipo de documento.
                //enviarCorreo.alertaRequerimientoCambioEstadoPendiente(0);

                enviarCorreo.alertaRequerimientoCambioEstadoPendienteNew(0);

            }
            catch (Exception)
            {


            }
        }
    }
}
