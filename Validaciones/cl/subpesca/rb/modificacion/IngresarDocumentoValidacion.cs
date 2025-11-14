using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Entidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.errores;
using LogicaNegocio.cl.subpesca.rb.servicios.modificacion;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;

namespace Validaciones.cl.subpesca.rb.modificacion
{
    public class IngresarDocumentoValidacion
    {
        RequerimientoService requerimientoService = new RequerimientoService();
        RequerimientoDA requerimientoDA = new RequerimientoDA();
        Logger logger = new Logger();



        public List<String> validaIngresoRequerimiento(Requerimiento requerimiento)
        {

            List<String> errores = new List<String>();


            try
            {



                //EN GENERAL TODO DOCUMENTO AMBITO (rbDocumentacionPestana) AL INGRESARLO QUEDA EN ESTADO VIGENTE, SALVO QUE SEA UN DOCUMENTO COMPLEMENTARIO, EN CUYO CASO TOMARA LA VIGENCIA DEL DOCUMENTO AL CUAL ESTA COMPLEMENTANDO
                if (requerimiento.ambitoTipo != null)
                {

                    int estadoVigencia = rbEstadosGenerales.VIGENTE;

                    if (requerimiento.tipoDocumento.id == rbTipo.INFORME_COMPLEMENTARIO || requerimiento.tipoDocumento.id == rbTipo.RESOLUCION_COMPLEMENTARIA)
                    {
                        Requerimiento requerimientoPrincipal = requerimientoService.ObtenerRespuesta(requerimiento.idReqPrincipal);
                        estadoVigencia = ((DocumentoAmbito)requerimientoPrincipal.ambitoTipo[0]).estadoVigencia.id;
                    }

                    foreach (DocumentoAmbito doc in requerimiento.ambitoTipo)
                    {
                        doc.estadoVigencia = new ParametroGenerico(estadoVigencia);
                    }
                }


                //LA RESOLUCION/INFORME COMPLEMENTARIO DEBE TENER EL MISMO TEMA DE LA RESOLUCION/INFORME QUE COMPLEMENTA
                if (requerimiento.tipoDocumento.id == rbTipo.RESOLUCION_COMPLEMENTARIA || requerimiento.tipoDocumento.id == rbTipo.INFORME_COMPLEMENTARIO)
                {
                    Requerimiento requerimientoPrincipal = requerimientoService.ObtenerRespuesta(requerimiento.idReqPrincipal);

                    if (requerimientoPrincipal.ambitoTipo[0].tipo.id != requerimiento.ambitoTipo[0].tipo.id)
                    {
                        errores.Add("El documento complementario debe tener el mismo tema del documento al cual complementa. (" + requerimientoPrincipal.ambitoTipo[0].tipo.descripcion + ")");
                    }

                }


                //LA FECHA DEBE SER IGUAL O ANTERIOR A HOY
                DateTime systemDate = DateTime.Now;
                if (requerimiento.fecha != default(DateTime) && requerimiento.fecha > systemDate)
                {
                    errores.Add("Fecha no puede ser posterior al día de hoy.");
                }
                if (requerimiento.fechaCI != default(DateTime) && requerimiento.fechaCI > systemDate)
                {
                    errores.Add("Fecha C.I. no puede ser posterior al día de hoy.");
                }
                if (requerimiento.fechaCI != default(DateTime) && requerimiento.fecha != default(DateTime))
                {
                    if (!(requerimiento.fechaCI > requerimiento.fecha || requerimiento.fechaCI.Equals(requerimiento.fecha)))
                    {
                        errores.Add("Fecha C.I. no puede ser posterior al campo Fecha.");
                    }
                }



                if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
                {




                }


                if (requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
                {
                    if (requerimiento.tipoEntrada.id == rbTipo.RESPUESTA_A_UN_REQUERIMIENTO)
                    {
                        //SOLO SE ESTAN ITERANDO LOS QUE SELECCIONO
                        foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                        {

                            /**
                            if (documentoAmbito.idDocGeneralResp > 0 && documentoAmbito.idDocGeneralResp != requerimiento.idRequerimiento)
                            {
                                errores.Add("No puede seleccionar un requerimiento que ya haya sido respondido por otra entrada. (" + documentoAmbito.idDocGeneralResp + ")");
                            }
                             * */

                            //SI TIENE ASOCIADA RESPUESTAS, ENTONCES DEBER SELECIONAR UNO, SALVO QUE SEA UN DOCUMENTO COMPLEMENTARIO

                            if (!(requerimiento.tipoDocumento.id == rbTipo.INFORME_COMPLEMENTARIO || requerimiento.tipoDocumento.id == rbTipo.RESOLUCION_COMPLEMENTARIA))
                            {

                                DataTable data = requerimientoDA.ListarPosiblesRespuestasSubRequerimiento(Convert.ToInt32(documentoAmbito.tipo.id), 0);

                                if (data != null && data.Rows.Count > 0)
                                {
                                    if (documentoAmbito.estadoResultadoResp.id <= 0)
                                    {
                                        errores.Add("Debe seleccionar el resultado");
                                    }
                                }
                            }
                        }
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

        public List<String> validaEliminacionDocumentoAsociado(DocumentoAmbito documentoAsociado)
        {

            List<String> errores = new List<String>();

            try
            {

                if (documentoAsociado.idDocGeneralResp > 0)
                {
                    errores.Add("No puede eliminar un documento asociado que haya sido respondido.");
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



        public List<String> validaAdicionDocumentoAsociado(DocumentoAmbito documentoAsociadoAgregar, List<DocumentoAmbito> lista, int idSolicitud)
        {

            List<String> errores = new List<String>();


            try
            {

                SubRequerimiento subRequerimiento = requerimientoService.ObtenerSubRequerimiento(documentoAsociadoAgregar.tipo.id);
                //SI APLICA REITERA DEBE EXISTIR UN PRINCIPAL VIGENTE EN LA GRILLA DE REQUERIMIENTO (YA GUARDADO EN LA BASE DE DATOS)
                if (subRequerimiento.aplicaReitera)
                {

                    DataTable reqOriginal = requerimientoService.ObtieneDocGralReiteraPadreBD(idSolicitud, documentoAsociadoAgregar.tipo.id);

                    if (reqOriginal == null || reqOriginal.Rows.Count == 0)
                    {
                        errores.Add("No existe un documento que se pueda reiterar");
                    }

                }


                if (lista != null && lista.Count > 0)
                {
                    foreach (DocumentoAmbito documentoAmbitoLista in lista)
                    {
                        if (documentoAmbitoLista.accion == accion.INGRESAR || documentoAmbitoLista.accion == accion.LISTADO || documentoAmbitoLista.accion == accion.MODIFICAR)
                        {
                            if (documentoAmbitoLista.ambito.id == documentoAsociadoAgregar.ambito.id && documentoAmbitoLista.tipo.id == documentoAsociadoAgregar.tipo.id)
                            {
                                errores.Add("Documento ya ingresado en lista.");
                                break;
                            }
                        }
                    }
                }



                //NO SE PUEDE INGRESAR UN DOCUMENTO PRINCIPAL Y UN REITERA DEL DOCUMENTO PRINCIPAL EN UN MISMO REQUERIMIENTO MULTIPLE
                SubRequerimientoReitera subRequerimientoReitera = requerimientoService.ObtieneSubReqReiteraPadre(documentoAsociadoAgregar.tipo.id);

                if (lista != null && lista.Count > 0 && subRequerimientoReitera != null)
                {
                    foreach (DocumentoAmbito documentoAmbitoLista in lista)
                    {
                        if (documentoAmbitoLista.accion == accion.INGRESAR || documentoAmbitoLista.accion == accion.LISTADO || documentoAmbitoLista.accion == accion.MODIFICAR)
                        {
                            if (documentoAmbitoLista.tipo.id == subRequerimientoReitera.padre.idSubRequerimiento || documentoAmbitoLista.tipo.id == subRequerimientoReitera.reitera.idSubRequerimiento)
                            {
                                errores.Add("No puede ingresar '" + subRequerimientoReitera.padre.nombreSubRequerimiento + "' y '" + subRequerimientoReitera.reitera.nombreSubRequerimiento + "' en el mismo requerimiento.");
                                break;
                            }
                        }
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


        public List<String> validarEliminacionDeRequerimiento(Requerimiento requerimiento)
        {

            List<String> errores = new List<String>();



            try
            {

                if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
                {

                    if (requerimiento.tipoSalida.id == rbTipo.INFORMATIVO)
                    {
                        return errores;
                    }

                    //NO DEBEN ESTAR CONTESTADOS LOS REQUERIMIENTOS
                    if (requerimiento.tipoSalida.id == rbTipo.REQUERIMIENTO_CON_RESPUESTA)
                    {

                        foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                        {
                            if (documentoAmbito.idDocGeneralResp > 0)
                            {
                                errores.Add("No puede eliminar requerimientos que tengan asociados una respuesta.");
                                break;
                            }
                        }

                        return errores;
                    }
                }


                if (requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
                {

                    if (requerimiento.tipoEntrada.id == rbTipo.INGRESO_SIN_REQUERIMIENTO)
                    {
                        return errores;
                    }

                    if (requerimiento.tipoEntrada.id == rbTipo.RESPUESTA_A_UN_REQUERIMIENTO)
                    {
                        return errores;
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


        public List<string> validarNoVigenteDeRequerimiento(Requerimiento requerimiento)
        {
            List<String> errores = new List<String>();

            try
            {

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
