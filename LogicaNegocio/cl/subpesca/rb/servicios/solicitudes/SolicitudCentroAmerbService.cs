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
    public class SolicitudCentroAmerbService
    {

        Logger logger = new Logger();
        SolicitudDA solicitudDA = new SolicitudDA();

        public bool guardarSolicitudAcuiculturaAmerb(SolicitudConcesion solicitudInicial, int idUsuario)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    //GUARDAR SOLICITUD
                    solicitudInicial.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);
                    if (!solicitudDA.GuardarSolicitudAcuiculturaAmerb(solicitudInicial))
                    {
                        return false;
                    }

                    //GUARDAR DATA ADICIONAL DE LA UNIDAD ESPACIAL
                    solicitudInicial.datosSolicitudUE.idSolConcesion = solicitudInicial.idSolConcesion;
                    if (!solicitudDA.GuardarDatosSolicitudUE(solicitudInicial.datosSolicitudUE, idUsuario))
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
