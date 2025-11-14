using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Transactions;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Entidades;
using System.Data;

namespace LogicaNegocio.cl.subpesca.rb.servicios.mantenedores
{
    public class MantenedorTransversalService
    {

        Logger logger = new Logger();
        ValidacionDocumentacionDA validacionDocumentacionDA = new ValidacionDocumentacionDA();


        public bool guardarAsociacionSubrequerimientoTipo(Datos.Entidades.ValidacionDocumentacion validacionDocumentacion)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {

                    if (!validacionDocumentacionDA.GuardarValidacionDocumentacion(validacionDocumentacion))
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

        public DataTable eliminarValidacionDocumentacion(int idValidacionDocumentacion)
        {
            return validacionDocumentacionDA.EliminarValidacionDocumentacion(idValidacionDocumentacion);
        }
    }
}
