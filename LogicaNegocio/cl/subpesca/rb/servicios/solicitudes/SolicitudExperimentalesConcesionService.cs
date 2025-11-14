using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Entidades;
using System.Transactions;
using Datos.Contantes;

namespace LogicaNegocio.cl.subpesca.rb.servicios.solicitudes
{
    public class SolicitudExperimentalesConcesionService
    {
        Logger logger = new Logger();
        SolicitudDA solicitudDA = new SolicitudDA();



        //GUARDA UNA SOLICITUD DE EXPERIMENTALES CONCESION
        public bool guardarSolicitudExperimentalesConcesion(SolicitudConcesion solicitudInicial, int idUsuario)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    //GUARDAR SOLICITUD
                    solicitudInicial.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION);
                    if (!solicitudDA.GuardarSolicitudExperimentalesConcesion(solicitudInicial))
                    {
                        return false;
                    }

                    //GUARDAR DATA ADICIONAL DE LA UNIDAD ESPACIAL
                    solicitudInicial.datosSolicitudUE.idSolConcesion = solicitudInicial.idSolConcesion;
                    if (!solicitudDA.GuardarDatosSolicitudUE(solicitudInicial.datosSolicitudUE, idUsuario))
                    {
                        return false;
                    }

                    //Guarda el Código de Concesión Padre
                    if (!solicitudDA.ActualizaSolicitud_cultivoExperimental(solicitudInicial.datosSolicitudUE.idDatosSolicitud, "EXP_CONCESION", Convert.ToInt32(solicitudInicial.unidadEspacial.centrosDeCultivo.codigoCentro)))
                    {
                        return false;
                    }


                    transactionScope.Complete();
                    return true;

                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }

        }
    }
}
