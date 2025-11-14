using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Transactions;
using Datos.Entidades;
using Datos.Contantes;

namespace LogicaNegocio.cl.subpesca.rb.servicios.solicitudes
{
    public class SolicitudCentroFaenamientoService
    {

        Logger logger = new Logger();
        SolicitudDA solicitudDA = new SolicitudDA();

        public bool guardarSolicitudCentroFaenamiento(SolicitudConcesion solicitudInicial)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    //GUARDAR SOLICITUD
                    solicitudInicial.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO);
                    if (!solicitudDA.GuardarSolicitudCentroFaenamiento(solicitudInicial))
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
