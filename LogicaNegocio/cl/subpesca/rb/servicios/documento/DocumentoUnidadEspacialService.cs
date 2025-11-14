using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Transactions;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Contantes;


namespace LogicaNegocio.cl.subpesca.rb.servicios.documento
{
    public class DocumentoUnidadEspacialService
    {

        Logger logger = new Logger();
        DocumentosConcesionDA documentosConcesionDA = new DocumentosConcesionDA();
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();

        public bool EliminarDocumentosConcesion(int idDocConcesion, int idSolConcesion, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!documentosConcesionDA.EliminarDocumentosConcesion(idDocConcesion, 0, idUsuario))
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

        public bool ActualizarDocumentoConcesion(DocumentosConcesion docConcesion)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    //GUARDAR EL DOCUMENTO ADJUNTO
                    if (docConcesion.archivoAdjunto != null && docConcesion.archivoAdjunto.archivo != null)
                    {
                        if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitud(docConcesion.archivoAdjunto))
                        {
                            return false;
                        }
                    }


                   
                    if (!documentosConcesionDA.GuardarDocumentosConcesion(docConcesion))
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

        public bool GuardarDocumentosConcesion(DocumentosConcesion docConcesion)
        {
          
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    //GUARDAR EL DOCUMENTO ADJUNTO
                    if (docConcesion.archivoAdjunto != null && docConcesion.archivoAdjunto.archivo != null)
                    {
                        if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitud(docConcesion.archivoAdjunto))
                        {
                            return false;
                        }
                    }


                    //DOCUMENTO
                    docConcesion.estadoVigencia = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
                    if (!documentosConcesionDA.GuardarDocumentosConcesion(docConcesion)) {
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



        /**
         * Obtiene un documento de una concesion
         */
        public DocumentosConcesion ObtenerDocumentoConcesion(int idDocConcesion, int idSolConcesion)
        {

            try
            {
                return documentosConcesionDA.ObtenerDocumentoConcesion(idDocConcesion, idSolConcesion);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
            
        }


        public List<DocumentosConcesion> ListarDocumentosConcesion(int idSolConcesion)
        {

            try
            {
                return documentosConcesionDA.ListarDocumentosConcesion(0, idSolConcesion, 0);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }

        }


    }
}
