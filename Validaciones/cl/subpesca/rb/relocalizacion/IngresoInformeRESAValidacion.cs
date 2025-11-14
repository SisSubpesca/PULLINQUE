using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Entidades.Resolucion;
using LogicaNegocio.cl.subpesca.rb.errores;
using LogicaNegocio.cl.subpesca.rb.servicios.resoluciones;
using Datos.Entidades;
using Datos.Contantes;
using Datos.Entidades.Relocalizacion;

namespace Validaciones.cl.subpesca.rb.relocalizacion
{
    public class IngresoInformeRESAValidacion
    {


        Logger logger = new Logger();
        ResolucionService resolucionService = new ResolucionService();


        public List<string> validarIngresoUnidadEspacial(AsocInformeSolicitud newAsociacion)
        {

            List<String> errores = new List<String>();

            try
            {

                   if (newAsociacion.codigoCentro < 0)
                   {
                            errores.Add("Ingrese Código Centro.");
                   }

                    if (errores.Count() == 0)
                    {

                        //VERIFICAR QUE EXISTA
                        SolicitudConcesion sol = resolucionService.VerificaExistenciaReferencia(rbTipo.RESOLUCION_SUB_REFERENCIA_UE, rbTipo.UNID_ESPACIAL_CONCESION, 0, Convert.ToString(newAsociacion.codigoCentro), 0);

                        if (sol == null || sol.idSolConcesion < 1)
                        {
                            errores.Add("Unidad espacial no Encontrada.");

                        }
                        else {
                            newAsociacion.idSolConcesion = sol.idSolConcesion;
                            newAsociacion.titulares = sol.titularesCad;
                            newAsociacion.comunas = sol.comunasCad;
                            
                        }
                        

                    }

            }
            catch (Exception ex)
            {
                errores.Add("Ha ocurrido un error al realizar la acción solicitada.");
                logger.PrintError(ex);
                logger.SendMailError(ex);
            }

            return errores;

        }




    }
}
