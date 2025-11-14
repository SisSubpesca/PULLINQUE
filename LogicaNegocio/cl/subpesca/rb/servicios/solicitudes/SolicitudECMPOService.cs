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
    public class SolicitudECMPOService
    {

        Logger logger = new Logger();
        SolicitudDA solicitudDA = new SolicitudDA();



        //GUARDA UNA SOLICITUD DE ECMPO
        public bool guardarSolicitudECMPO(SolicitudConcesion solicitudInicial)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    //GUARDAR SOLICITUD
                    solicitudInicial.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO);
                    if (!solicitudDA.GuardarSolicitudECMPO(solicitudInicial))
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
