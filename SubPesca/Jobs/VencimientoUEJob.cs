using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;

namespace SubPesca.Jobs
{
    public class VencimientoUEJob : IJob
    {
        EnviarCorreo enviarCorreo = new EnviarCorreo();

        public void Execute(IJobExecutionContext context)
        {

            try
            {
                enviarCorreo.obtenerUnidadesEspacialesVencidas(0);

            }
            catch (Exception)
            {


            }
        }
    }
}