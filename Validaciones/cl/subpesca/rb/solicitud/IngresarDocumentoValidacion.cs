using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Entidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.errores;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;


namespace Validaciones.cl.subpesca.rb.solicitud
{
    public class IngresarDocumentoValidacion
    {

        RequerimientoService requerimientoService = new RequerimientoService();
        InformacionSolicitudService informacionSolicitudService = new InformacionSolicitudService();
        RequerimientoDA requerimientoDA = new RequerimientoDA();

        SolicitudDA solicitudDA = new SolicitudDA();

        Logger logger = new Logger();
        private static int idAcogeRecursoReposicion = 136;
        private static int idITC_UOT = 109;
        private static int idResolucionSSP = 132;
        private static int idResolucionSSFFAA = 130;





        public List<String> validaIngresoRequerimiento(Requerimiento requerimiento)
        {

            List<String> errores = new List<String>();


            try
            {


                //NO SE PUEDE INGRESAR IT DAC CON RESULTADO APRUEBA SI LA SOLICITUD TIENE ALGUNO DE LOS SIGUIENTES CHECK MARCADOS COMO "SI": 
                //supeditaAvanzaAprueba = indica que la solicitud avanzo como si tuviera un IT OUT con resultado Aprueba
                //suspendeAvanzaAprueba = indica que la solicitud continuo con su tramitacion a pesar de que esta dentro de un grupo suspendido.
                if(this.esITDAC_Aprueba(requerimiento)){

                    HeaderSolicitudConcesion headerSolicitudConcesion = informacionSolicitudService.ObtieneSolicitudConcesionHeader(requerimiento.solicitud.idSolConcesion);

                    bool noPuedeIngresarITDAC = false;

                    if (
                        (headerSolicitudConcesion.supeditaAvanzaAprueba != null  &&  headerSolicitudConcesion.supeditaAvanzaAprueba.id == rbEstadosGenerales.SI)  
                        ||
                        (headerSolicitudConcesion.suspendeAvanzaEstado != null && headerSolicitudConcesion.suspendeAvanzaEstado.id == rbEstadosGenerales.SI) 
                        )
                    {
                        noPuedeIngresarITDAC = true;
                    }

                    if (noPuedeIngresarITDAC)
                    {
                        errores.Add("No puede ingresar un IT DAC con resultado Aprueba en una solicutud que esta suspendida y/o no posee un IT OUT con resultado Aprueba");
                    }
                }


                bool esAcogeApruebaRecursoReposicion = false;

                if (requerimiento.ambitoTipo != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    {
                        if (documentoAmbito.accion == accion.INGRESAR || documentoAmbito.accion == accion.LISTADO || documentoAmbito.accion == accion.MODIFICAR)
                        {
                            if (documentoAmbito.tipo.id == idAcogeRecursoReposicion)
                            {
                                esAcogeApruebaRecursoReposicion = true;
                                break;
                            }
                        }
                    }
                }



                //SOLO PUEDE INGRESAR UN ACOGE RECURSO DE REPOSICIÓN SI LA SOLICITUD ESTA EN RECURSO DE REPOSICION
                if (esAcogeApruebaRecursoReposicion)
                {
                    if (!requerimientoDA.existeRecursoReposicion(requerimiento.solicitud.idSolConcesion))
                    {
                        errores.Add("Esta solicitud no tiene un recurso de reposición vigente");
                    }
                }




                //EN GENERAL TODO DOCUMENTO AMBITO (rbDocumentacionPestana) AL INGRESARLO QUEDA EN ESTADO VIGENTE, SALVO QUE SEA UN DOCUMENTO COMPLEMENTARIO, EN CUYO CASO TOMARA LA VIGENCIA DEL DOCUMENTO AL CUAL ESTA COMPLEMENTANDO
                if (requerimiento.ambitoTipo != null)
                {

                    int estadoVigencia = rbEstadosGenerales.VIGENTE;

                    if (requerimiento.tipoDocumento.id == rbTipo.INFORME_COMPLEMENTARIO || requerimiento.tipoDocumento.id == rbTipo.RESOLUCION_COMPLEMENTARIA)
                    {

                        if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
                        {

                            Requerimiento requerimientoPrincipal = requerimientoService.ObtenerRequerimientoSinEstado(requerimiento.idReqPrincipal);
                            estadoVigencia = ((DocumentoAmbito)requerimientoPrincipal.ambitoTipo[0]).estadoVigencia.id;

                        }
                        else if (requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
                        {

                            Requerimiento requerimientoPrincipal = requerimientoService.ObtenerRespuesta(requerimiento.idReqPrincipal);
                            estadoVigencia = ((DocumentoAmbito)requerimientoPrincipal.ambitoTipo[0]).estadoVigencia.id;

                        }


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

        private bool esITDAC_Aprueba(Requerimiento requerimiento)
        {
            if(requerimiento.ambitoTipo != null){

                foreach (DocumentoAmbito ambitoTipo in requerimiento.ambitoTipo)
                {

                    if (ambitoTipo.idTipo == rbSubRequerimiento.INFORME_TECNICO && ambitoTipo.idResultado == rbEstadosGenerales.APRUEBA)
                    {
                        return true;
                    }

                }
            }

            return false;
        }

        public List<String> validaEliminacionDocumentoAsociado(DocumentoAmbito documentoAsociado)
        {

            List<String> errores = new List<String>();

            try{

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


            try{

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
            catch (Exception ex) {
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

        public List<string> validaIngresoCapitaniaPuerto(CapitaniaDePuerto capitaniaDePuerto)
        {
            return new List<string>();
        }

        public List<string> validaIngresoTipoPendiente(AsocGrupoSolicitud asocGrupoSolicitud, List<AsocGrupoSolicitud> asocGrupoSolicitudList)
        {
            List<String> errores = new List<String>();

            try
            {
                /* Si el resultado del IT UOT es Pendiente, es obligatorio que al menos ingrese un tipo de pendiente */
                int count = 0;
                if (asocGrupoSolicitud.idResultadoIT_UOT == rbEstadosGenerales.PENDIENTE) {

                    if (asocGrupoSolicitudList != null && asocGrupoSolicitudList.Count > 0)
                    {
                        foreach (AsocGrupoSolicitud asocGrupoSolicitudAux in asocGrupoSolicitudList)
                        {
                            if (asocGrupoSolicitudAux.accion == accion.INGRESAR || asocGrupoSolicitudAux.accion == accion.LISTADO || asocGrupoSolicitudAux.accion == accion.MODIFICAR)
                            {
                                count++;
                            }
                        }

                        if (count < 1 && asocGrupoSolicitud.accion != accion.INGRESAR)
                        {
                            errores.Add("Debe ingresar al menos un Tipo de Pendiente.");

                        }
                    }
                }

                
                /* Tipos de pendientes no pueden repetirse en la lista de resultados */
                foreach (AsocGrupoSolicitud asocGrupoSolicitudAux in asocGrupoSolicitudList)
                {
                    if (asocGrupoSolicitudList != null && asocGrupoSolicitudList.Count > 0)
                    {
                        if (asocGrupoSolicitudAux.estadoVigencia != null && asocGrupoSolicitudAux.estadoVigencia.id == rbEstadosGenerales.VIGENTE)
                        {
                            if (asocGrupoSolicitudAux.accion == accion.INGRESAR || asocGrupoSolicitudAux.accion == accion.LISTADO || asocGrupoSolicitudAux.accion == accion.MODIFICAR)
                            {
                                if (asocGrupoSolicitud != null && asocGrupoSolicitud.grupoSuspendido.idGrupoSuspend == asocGrupoSolicitudAux.grupoSuspendido.idGrupoSuspend)
                                {
                                    errores.Add("Ya ingresó el mismo Tipo de Pendiente a la lista de resultados.");
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

        public List<string> validaIngresoTipoSupeditado(DependenciaSupeditados dependenciaSupeditados, List<DependenciaSupeditados> dependenciaSupeditadosList)
        {
            List<String> errores = new List<String>();

            try
            {
                /* Si el resultado del IT UOT es Supeditado, es obligatorio que al menos ingrese un tipo de supeditado */
                int count = 0;
                if (dependenciaSupeditados.resultadoId == rbEstadosGenerales.SUPEDITADA)
                {
                    if (dependenciaSupeditadosList != null && dependenciaSupeditadosList.Count > 0)
                    {
                        foreach (DependenciaSupeditados dependenciaSupeditadosAux in dependenciaSupeditadosList)
                        {
                            if (dependenciaSupeditadosAux.accion == accion.INGRESAR || dependenciaSupeditadosAux.accion == accion.LISTADO || dependenciaSupeditadosAux.accion == accion.MODIFICAR)
                            {
                                count++;
                            }
                        }

                        if (count < 1 && dependenciaSupeditados.accion != accion.INGRESAR)
                        {
                            errores.Add("Debe ingresar al menos un Tipo de Supeditado.");

                        }
                    }
                }


                /* Para ciertos tipos de supeditados el numero pert o identificador de solicitud debe ser valido */
                if (dependenciaSupeditados.tipoSupeditado.id == rbTipo.SUPEDITADO_SOLICITUD_RELOCALIZACION_LEY_RESA || dependenciaSupeditados.tipoSupeditado.id == rbTipo.SUPEDITADO_SOLICITUD_ECMPO || dependenciaSupeditados.tipoSupeditado.id == rbTipo.SUPEDITADO_SOLICITUD_CONCESION
                    || dependenciaSupeditados.tipoSupeditado.id == rbTipo.SUPEDITADO_SOLICITUD_AMERB)
                {
                    if (dependenciaSupeditados.accion == accion.INGRESAR || dependenciaSupeditados.accion == accion.LISTADO || dependenciaSupeditados.accion == accion.MODIFICAR)
                    {
                        if (dependenciaSupeditados.solicitudConcesionDep == null || dependenciaSupeditados.solicitudConcesionDep.numPert == null || dependenciaSupeditados.solicitudConcesionDep.numPert.Equals(""))
                        {
                            errores.Add("Debe ingresar identificador de solicitud para el tipo de trámite seleccionado.");
                        }
                        
                        
                        bool validaIdentificadorSolicitud = solicitudDA.ValidaPertTramite(dependenciaSupeditados.tipoSupeditado.id, dependenciaSupeditados.solicitudConcesionDep.numPert);
                        if (!validaIdentificadorSolicitud)
                        {
                            errores.Add("Identificador de solicitud no valido para el tipo de trámite seleccionado.");
                        }
                    }
                }


                /* Tipos de supeditados no pueden repetirse en la lista de resultados */
                foreach (DependenciaSupeditados dependenciaSupeditadosAux in dependenciaSupeditadosList)
                {
                    if (dependenciaSupeditadosList != null && dependenciaSupeditadosList.Count > 0)
                    {
                        if (dependenciaSupeditados.tipoSupeditado.id != rbTipo.SUPEDITADO_SOLICITUD_RELOCALIZACION_LEY_RESA && dependenciaSupeditados.tipoSupeditado.id != rbTipo.SUPEDITADO_SOLICITUD_ECMPO && dependenciaSupeditados.tipoSupeditado.id != rbTipo.SUPEDITADO_SOLICITUD_CONCESION
                            && dependenciaSupeditados.tipoSupeditado.id != rbTipo.SUPEDITADO_SOLICITUD_AMERB)
                        {
                            if (dependenciaSupeditados.accion == accion.INGRESAR || dependenciaSupeditados.accion == accion.LISTADO || dependenciaSupeditados.accion == accion.MODIFICAR)
                            {
                                if (dependenciaSupeditadosAux != null && dependenciaSupeditadosAux.tipoSupeditado.id == dependenciaSupeditados.tipoSupeditado.id)
                                {
                                    errores.Add("El Tipo de Supeditado seleccionado solo puede ser ingresado una vez a la lista de resultados.");
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

        public List<string> validarEvaluacionOrdenamientoTerr(EvaluacionUOT_UE evaluacionUOT_UE)
        {
             List<String> errores = new List<String>();

            try
            {
                /* Si el resultado del IT UOT es Supeditado, es obligatorio que al menos ingrese un tipo de supeditado */
                int count = 0;

                if (evaluacionUOT_UE.estadoResultado != null && evaluacionUOT_UE.estadoResultado.id == rbEstadosGenerales.SUPEDITADA)
                {
                    if (evaluacionUOT_UE.dependenciaSupeditadosList != null && evaluacionUOT_UE.dependenciaSupeditadosList.Count > 0)
                    {
                        foreach (DependenciaSupeditados dependenciaSupeditadosAux in evaluacionUOT_UE.dependenciaSupeditadosList)
                        {
                            if (dependenciaSupeditadosAux.accion == accion.INGRESAR || dependenciaSupeditadosAux.accion == accion.LISTADO || dependenciaSupeditadosAux.accion == accion.MODIFICAR)
                            {
                                count++;
                            }
                        }

                        if (count < 1)
                        {
                            errores.Add("Debe ingresar al menos un Tipo de Supeditado.");

                        }
                    }
                    else {
                        errores.Add("Debe ingresar al menos un Tipo de Supeditado.");
                    
                    }
                }

                /* Si el resultado del IT UOT es Pendiente, es obligatorio que al menos ingrese un tipo de pendiente */
                count = 0;
                if (evaluacionUOT_UE.estadoResultado != null && evaluacionUOT_UE.estadoResultado.id == rbEstadosGenerales.PENDIENTE)
                {
                    if (evaluacionUOT_UE.asocGrupoSolicitudList != null && evaluacionUOT_UE.asocGrupoSolicitudList.Count > 0)
                    {
                        foreach (AsocGrupoSolicitud asocGrupoSolicitudAux in evaluacionUOT_UE.asocGrupoSolicitudList)
                        {
                            if (asocGrupoSolicitudAux.accion == accion.INGRESAR || asocGrupoSolicitudAux.accion == accion.LISTADO || asocGrupoSolicitudAux.accion == accion.MODIFICAR)
                            {
                                count++;
                            }
                        }

                        if (count < 1)
                        {
                            errores.Add("Debe ingresar al menos un Tipo de Pendiente.");

                        }
                    }
                    else {
                        errores.Add("Debe ingresar al menos un Tipo de Pendiente.");
                    
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
