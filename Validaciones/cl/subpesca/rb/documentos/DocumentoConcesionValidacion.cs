using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Contantes;

namespace Validaciones.cl.subpesca.rb.documentos
{
    public class DocumentoConcesionValidacion
    {

        Logger logger = new Logger();

        public List<string> validaIngresoDocumentoConcesion(DocumentosConcesion documento)
        {

            List<String> errores = new List<String>();

            try
            {

                if (documento.flujoDocumental == null || documento.flujoDocumental.id < 1)
                {
                    errores.Add("Seleccione Flujo Documental.");
                }


                
                if (errores.Count() == 0)
                {
                    if (documento.flujoDocumental.id == rbTipo.ENTRADA) {

                        //Tipo entrada
                        if (documento.tipoEntrada == null || documento.tipoEntrada.id < 1)
                        {
                            errores.Add("Seleccione Tipo de Entrada.");
                        }

                        if (documento.origen == null || documento.origen.id < 1)
                        {
                            errores.Add("Seleccione Origen.");
                        }
                    
                    }

                    if (documento.flujoDocumental.id == rbTipo.SALIDA)
                    {

                        if (documento.tipoSalida == null || documento.tipoSalida.id < 1)
                        {
                            errores.Add("Seleccione Tipo de Salida.");
                        }

                        if (documento.destinatario == null || documento.destinatario.id < 1)
                        {
                            errores.Add("Seleccione Destinatario.");
                        }
                    }

                    if (documento.tipoDocumento == null || documento.tipoDocumento.id < 0)
                    {
                        errores.Add("Seleccione Tipo de Documento.");
                    }

                    if (documento.nombreTema == null || documento.nombreTema.Trim().Equals(""))
                    {
                        errores.Add("Ingrese Tema.");
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
