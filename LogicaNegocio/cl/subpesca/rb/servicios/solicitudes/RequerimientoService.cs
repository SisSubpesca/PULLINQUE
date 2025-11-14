using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Transactions;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using System.Collections;
using LogicaNegocio.cl.subpesca.rb.common;
using System.Data.SqlClient;
using Datos.AccesoDatos;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.doc_planilla;

namespace LogicaNegocio.cl.subpesca.rb.servicios.solicitudes
{
    public class RequerimientoService
    {

        Logger logger = new Logger();
        RequerimientoDA requerimientoDA =  new  RequerimientoDA();
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();
        SolicitudConcesionService solicitudConcesionService = new SolicitudConcesionService();
        Hashtable hashRecursoReposicion = new Hashtable();
        DocGralCierreForzadoDA docGralCierreForzadoDA = new DocGralCierreForzadoDA();
        TipoDA tipoDa = new TipoDA();
        SometimientoSEA_DA sometimientoSEA_DA = new SometimientoSEA_DA();
        DocPlanillaDA DocDA = new DocPlanillaDA();
        GrupoSuspendidoDA grupoSuspendidoDA = new GrupoSuspendidoDA();
        SolicitudDA solicitudDA = new SolicitudDA();
        /**
         * Guarda un requerimiento; 
         **/
        public bool guardarRequerimiento(Requerimiento requerimiento, int idUsuario)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    Hashtable hashEstadoFinalReqPestana = new Hashtable();

               

                    //SI EL DOCUMENTO ES INFORME PRINCIPAL O RESOLUCION PRINCIPAL DEBE ACTUALIZAR EL ESTADO DEL DOCUMENTO PRINCIPAL ANTERIOR
                    //SOLO DEBE OCURRIR SI SON SOLICITUDES. UNIDADES ESPACIALES PUEDEN TENER MAS DE 1 TIPO
                    if (requerimiento.tipoDocumento.id == rbTipo.INFORME_PRINCIPAL || requerimiento.tipoDocumento.id == rbTipo.RESOLUCION_PRINCIPAL) {
                        if (!this.ActualizarDocumentacionPestanaDocPrinc(requerimiento.solicitud.idSolConcesion, requerimiento.tipoDocumento.id, requerimiento.ambitoTipo[0].tipo.id, rbEstadosGenerales.NO_VIGENTE, idUsuario))
                        {
                            return false;
                        }
                    }


                    //SI LA SOLICITUD ESTA EN RECURSO DE REPOSICION, ENTONCES TODOS LOS DOCUMENTOS QUE ENTRAN DEBEN MARCARSE COMO "EN REPOSICION"
                    if (requerimientoDA.existeRecursoReposicion(requerimiento.solicitud.idSolConcesion)) {
                        requerimiento.esReposicion = true;
                    }


                    if (requerimiento.flujoDocumental != null && requerimiento.flujoDocumental.id == rbTipo.SALIDA)
                    {
                        if (requerimiento.tipoSalida != null && requerimiento.tipoSalida.id == rbTipo.INFORMATIVO)
                        {

                            //GUARDAR EL DOCUMENTO ADJUNTO
                            if (requerimiento.archivoAdjunto != null && requerimiento.archivoAdjunto.archivo != null)
                            {
                                if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitud(requerimiento.archivoAdjunto))
                                {
                                    return false;
                                }
                            }

                            
                            //GUARDAR EL REQUERIMIENTO (INMEDIATAMENTE SE INGRESA COMO CONFORME, PORQUE NO TENDRA RESPUESTA)
                            requerimiento.estadoFinal = new ParametroGenerico(rbEstadosGenerales.CONFORME);
                            if(!requerimientoDA.GuardarRequerimiento(requerimiento, 0)){
                                return false;
                            }
                             


                            //GUARDAR LAS RELACIONES TABLA AMBIENTE/TIPO (PARTE 1)
                            if (requerimiento.ambitoTipo != null) {

                                foreach(DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo){

                                    if (documentoAmbito.accion == accion.INGRESAR)
                                    {
                                        if (!hashEstadoFinalReqPestana.ContainsKey(documentoAmbito.ambito.id))
                                        {
                                            hashEstadoFinalReqPestana.Add(documentoAmbito.ambito.id, null);
                                        }
                                    }
                                }
                            }



                            //GUARDA EL ESTADO DE UN REQUERIMIENTO EN UNA DETERMINADA PESTAÑA
                            foreach (DictionaryEntry entry in hashEstadoFinalReqPestana)
                            {
                                if (!requerimientoDA.GuardarEstadoFinalReqPestana(requerimiento.idRequerimiento, Convert.ToInt32(entry.Key)))
                                {
                                    return false;
                                }     
                            }


                            //GUARDAR LAS RELACIONES TABLA AMBIENTE/TIPO (PARTE 2)
                            if (requerimiento.ambitoTipo != null)
                            {

                                foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                                {

                                    if (documentoAmbito.accion == accion.INGRESAR)
                                    {
                                        documentoAmbito.idRequerimiento = requerimiento.idRequerimiento;
                                        documentoAmbito.idSolicitud = requerimiento.solicitud.idSolConcesion;
                                        if (!requerimientoDA.GuardarDocumentoAsociado(documentoAmbito, 0))
                                        {
                                            return false;
                                        }
                                    }
                                }
                            }
                            
                        }
                    }


                    if (requerimiento.flujoDocumental != null && requerimiento.flujoDocumental.id == rbTipo.SALIDA)
                    {
                        if (requerimiento.tipoSalida != null && requerimiento.tipoSalida.id == rbTipo.REQUERIMIENTO_CON_RESPUESTA)
                        {


                            //GUARDAR EL DOCUMENTO ADJUNTO
                            if (requerimiento.archivoAdjunto != null && requerimiento.archivoAdjunto.archivo != null)
                            {
                                if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitud(requerimiento.archivoAdjunto))
                                {
                                    return false;
                                }
                            }




                            //GUARDAR EL REQUERIMIENTO
                            if (!requerimientoDA.GuardarRequerimiento(requerimiento, 0))
                            {
                                return false;
                            }



                            //GUARDAR LAS RELACIONES TABLA AMBIENTE/TIPO (PARTE 1)
                            if (requerimiento.ambitoTipo != null)
                            {

                                foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                                {

                                    if (documentoAmbito.accion == accion.INGRESAR)
                                    {

                                        if (!hashEstadoFinalReqPestana.ContainsKey(documentoAmbito.ambito.id))
                                        {
                                            hashEstadoFinalReqPestana.Add(documentoAmbito.ambito.id, null);
                                        }
                                    }
                                }
                            }


                            //GUARDA EL ESTADO DE UN REQUERIMIENTO EN UNA DETERMINADA PESTAÑA
                            foreach (DictionaryEntry entry in hashEstadoFinalReqPestana)
                            {
                                if (!requerimientoDA.GuardarEstadoFinalReqPestana(requerimiento.idRequerimiento, Convert.ToInt32(entry.Key)))
                                {
                                    return false;
                                }
                            }



                            //GUARDAR LAS RELACIONES TABLA AMBIENTE/TIPO (PARTE 2)
                            if (requerimiento.ambitoTipo != null)
                            {

                                foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                                {

                                    if (documentoAmbito.accion == accion.INGRESAR)
                                    {

                                        documentoAmbito.idRequerimiento = requerimiento.idRequerimiento;
                                        documentoAmbito.idSolicitud = requerimiento.solicitud.idSolConcesion;
                                        if (!requerimientoDA.GuardarDocumentoAsociado(documentoAmbito, 0))
                                        {
                                            return false;
                                        }
                                    }
                                }
                            }


                        }
                    }




                    if (requerimiento.flujoDocumental != null && requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
                    {
                        if (requerimiento.tipoEntrada != null && requerimiento.tipoEntrada.id == rbTipo.RESPUESTA_A_UN_REQUERIMIENTO)
                        {


                            //GUARDAR EL DOCUMENTO ADJUNTO
                            if (requerimiento.archivoAdjunto != null && requerimiento.archivoAdjunto.archivo != null)
                            {
                                if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitud(requerimiento.archivoAdjunto))
                                {
                                    return false;
                                }
                            }




                            //GUARDAR EL REQUERIMIENTO
                            if (!requerimientoDA.GuardarRequerimiento(requerimiento, 0))
                            {
                                return false;
                            }



                            //ACTUALIZAR LAS RELACIONES TABLA AMBIENTE/TIPO 
                            if (requerimiento.ambitoTipo != null)
                            {

                                foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                                {
                                    documentoAmbito.idDocGeneralResp = requerimiento.idRequerimiento;
                                    documentoAmbito.idSolicitud = requerimiento.solicitud.idSolConcesion;
                                    if (!requerimientoDA.GuardarDocumentoAsociado(documentoAmbito, 0))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }


                    if (requerimiento.flujoDocumental != null && requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
                    {
                        if (requerimiento.tipoEntrada != null && requerimiento.tipoEntrada.id == rbTipo.INGRESO_SIN_REQUERIMIENTO)
                        {


                            //GUARDAR EL DOCUMENTO ADJUNTO
                            if (requerimiento.archivoAdjunto != null && requerimiento.archivoAdjunto.archivo != null)
                            {
                                if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitud(requerimiento.archivoAdjunto))
                                {
                                    return false;
                                }
                            }



                            //GUARDAR EL REQUERIMIENTO
                            if (!requerimientoDA.GuardarRequerimiento(requerimiento, 0))
                            {
                                return false;
                            }



                            //ACTUALIZAR LAS RELACIONES TABLA AMBIENTE/TIPO 
                            if (requerimiento.ambitoTipo != null)
                            {

                                foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                                {
                                    documentoAmbito.idDocGeneralResp = requerimiento.idRequerimiento;
                                    documentoAmbito.idSolicitud = requerimiento.solicitud.idSolConcesion;
                                    if (!requerimientoDA.GuardarDocumentoAsociado(documentoAmbito, 0))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }


                    //SI HAY UN DOCUMENTO EN RECURSO DE REPOSICIÓN ENTONCES SE DEBE MARCAR COMO EN REPOSICION (PARA NO RECALCULAR ESTADOS)
                    if (requerimientoDA.existeRecursoReposicion(requerimiento.solicitud.idSolConcesion))
                    {
                        if (!solicitudConcesionService.ActualizaSolicitud_RecReposicion(requerimiento.solicitud.idSolConcesion, true, idUsuario))
                        {
                            return false;
                        }

                    }else{
                        if (!solicitudConcesionService.ActualizaSolicitud_RecReposicion(requerimiento.solicitud.idSolConcesion, false, idUsuario))
                        {
                            return false;
                        }
                    }



                    //AMPLIACIONES DE PLAZO

                    if (requerimiento.nuevaFecha != null && requerimiento.nuevaFecha != default(DateTime)) {


                        if (!requerimientoDA.ActualizarRequerimientoPlazo(requerimiento.solicitud.idSolConcesion, requerimiento.ambitoTipo[0].tipo.id, requerimiento.idRequerimiento, requerimiento.nuevaFecha))
                        {
                            return false;
                        }
                    
                    }


                    //SUPEDITADO Y GRUPO SUSPENDIDO

                    if (requerimiento.dependenciaSupeditadosList != null && requerimiento.dependenciaSupeditadosList.Count > 0)
                    {

                        foreach (DependenciaSupeditados dependenciaSupeditado in requerimiento.dependenciaSupeditadosList)
                        {

                            //dependenciaSupeditado.solicitudConcesionDep = requerimiento.solicitud;

                            int idSolicitud = solicitudDA.obtieneSolicitudIdPert(dependenciaSupeditado.solicitudConcesionDep.numPert);

                            dependenciaSupeditado.solicitudConcesionDep.idSolConcesion = idSolicitud;

                            dependenciaSupeditado.docPestana = new DocumentoAmbito();
                            dependenciaSupeditado.docPestana.idDocPestana = requerimiento.ambitoTipo[0].idDocPestana;

                            if (dependenciaSupeditado.accion == accion.ELIMINAR)
                            {

                                if (!grupoSuspendidoDA.EliminarDependenciaSupeditados(dependenciaSupeditado.idDepSupeditado, dependenciaSupeditado.solicitudConcesionDep.idSolConcesion, 0, dependenciaSupeditado.docPestana.idDocPestana))
                                {
                                    return false;
                                }
                            }

                            if (dependenciaSupeditado.accion == accion.INGRESAR)
                            {
                                if (!grupoSuspendidoDA.GuardarDependenciaSupeditados(dependenciaSupeditado))
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    /* Sólo si aplica, se debe almacenar grupos suspendidos asociados */
                    if (requerimiento.asocGrupoSolicitudList != null && requerimiento.asocGrupoSolicitudList.Count > 0)
                    {
                        foreach (AsocGrupoSolicitud asocGrupoSolicitud in requerimiento.asocGrupoSolicitudList)
                        {
                            asocGrupoSolicitud.solicitudConcesion = requerimiento.solicitud;

                            if (asocGrupoSolicitud.accion == accion.ELIMINAR)
                            {
                                /* Eliminación lógica del grupo con la solicitud. Se deja no vigente */
                                if (asocGrupoSolicitud.docPestana != null)
                                {
                                    if (!grupoSuspendidoDA.ActualizaAsocGrupoSolicitudVigencia(0, 0, asocGrupoSolicitud.solicitudConcesion.idSolConcesion, 0, rbEstadosGenerales.NO_VIGENTE, asocGrupoSolicitud.docPestana.idDocPestana))
                                    {
                                        return false;
                                    }
                                }
                                else {
                                    if (!grupoSuspendidoDA.ActualizaAsocGrupoSolicitudVigencia(0, 0, asocGrupoSolicitud.solicitudConcesion.idSolConcesion, 0, rbEstadosGenerales.NO_VIGENTE, 0))
                                    {
                                        return false;
                                    }
                                }
                                
                            }

                            if (asocGrupoSolicitud.accion == accion.INGRESAR)
                            {
                                asocGrupoSolicitud.estadoVigencia = new ParametroGenerico();
                                asocGrupoSolicitud.estadoVigencia.id = rbEstadosGenerales.VIGENTE;

                                asocGrupoSolicitud.docPestana = new DocumentoAmbito();
                                asocGrupoSolicitud.docPestana.idDocPestana = requerimiento.ambitoTipo[0].idDocPestana;

                                if (!grupoSuspendidoDA.GuardarAsocGrupoSolicitud(asocGrupoSolicitud))
                                {
                                    return false;
                                }
                            }
                        }
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
         *  ACTUALIZA UN DOCUMENTO
         */ 
        public bool actualizarRequerimiento(Requerimiento requerimiento, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    Hashtable hashAgregarEstadoFinalReqPestana      = new Hashtable();
                    Hashtable hashExistentesEstadoFinalReqPestana   = new Hashtable();
                    Hashtable hashBorrarEstadoFinalReqPestana       = new Hashtable();



                    //SI LA SOLICITUD ESTA EN RECURSO DE REPOSICION, ENTONCES TODOS LOS DOCUMENTOS QUE ENTRAN DEBEN MARCARSE COMO "EN REPOSICION"
                    if (requerimientoDA.existeRecursoReposicion(requerimiento.solicitud.idSolConcesion))
                    {
                        requerimiento.esReposicion = true;
                    }

                    if (requerimiento.flujoDocumental != null && requerimiento.flujoDocumental.id == rbTipo.SALIDA)
                    {
                        if (requerimiento.tipoSalida != null && requerimiento.tipoSalida.id == rbTipo.REQUERIMIENTO_CON_RESPUESTA)
                        {


                            //GUARDAR EL DOCUMENTO ADJUNTO NUEVO
                            if (requerimiento.archivoAdjunto != null && requerimiento.archivoAdjunto.archivo != null)
                            {
                                if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitud(requerimiento.archivoAdjunto))
                                {
                                    return false;
                                }
                            }

                            //NO HICE NADA CON EL ARCHIVO, SOLO ACTUALIZAR EL REQUERIMIENTO 
                            if (requerimiento.archivoAdjunto != null && requerimiento.archivoAdjunto.idArchivo > 0)
                            {
                                //NO SE DEBE REALIZAR NADA
                            }

                            //BORRE EL ARCHIVO, SOLO ACTUALIZAR EL REQUERIMIENTO 
                            if (requerimiento.archivoAdjunto != null && requerimiento.archivoAdjunto.idArchivo == 0)
                            {
                                //NO SE DEBE REALIZAR NADA
                            }

                            //ACTUALIZAR EL REQUERIMIENTO
                            if (!requerimientoDA.GuardarRequerimiento(requerimiento, idUsuario))
                            {
                                return false;
                            }

                            
                            //GUARDAR/ELIMINAR LAS RELACIONES TABLA AMBIENTE/TIPO
                            if (requerimiento.ambitoTipo != null)
                            {

                                foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                                {

                                    //ASOCIACIONES YA EXISTENTES
                                    if (documentoAmbito.accion == accion.LISTADO)
                                    {
                                        if (!hashExistentesEstadoFinalReqPestana.ContainsKey(documentoAmbito.ambito.id))
                                        {
                                            hashExistentesEstadoFinalReqPestana.Add(documentoAmbito.ambito.id, rbEstadosGenerales.NO_CONFORME);
                                        }
                                    }


                                    //AGREGAR NUEVA ASOCIACION
                                    if (documentoAmbito.accion == accion.INGRESAR) {

                                        if (!hashAgregarEstadoFinalReqPestana.ContainsKey(documentoAmbito.ambito.id))
                                        {
                                            hashAgregarEstadoFinalReqPestana.Add(documentoAmbito.ambito.id, rbEstadosGenerales.NO_CONFORME);
                                        }

                                        documentoAmbito.idRequerimiento = requerimiento.idRequerimiento;
                                        documentoAmbito.idSolicitud = requerimiento.solicitud.idSolConcesion;
                                        if (!requerimientoDA.GuardarDocumentoAsociado(documentoAmbito, idUsuario))
                                        {
                                            return false;
                                        }
                                    }


                                    //ELIMINAR ASOCIACION
                                    if (documentoAmbito.accion == accion.ELIMINAR)
                                    {

                                        if (!hashBorrarEstadoFinalReqPestana.ContainsKey(documentoAmbito.ambito.id))
                                        {
                                            hashBorrarEstadoFinalReqPestana.Add(documentoAmbito.ambito.id, null);
                                        }

                                        documentoAmbito.idRequerimiento = requerimiento.idRequerimiento;
                                        if (!requerimientoDA.EliminarRbDocumentacionPestana(documentoAmbito.idDocPestana, requerimiento.solicitud.idSolConcesion, idUsuario))
                                        {
                                            return false;
                                        }
                                    }
                                }
                            }


                            //GUARDA EL ESTADO DE UN REQUERIMIENTO EN UNA DETERMINADA PESTAÑA (SOLO PUEDE AGREGAR SI NO ESTA EN EL HASH DE EXISTENTES)
                            foreach (DictionaryEntry entry in hashAgregarEstadoFinalReqPestana)
                            {
                                if (!hashExistentesEstadoFinalReqPestana.Contains(entry.Key)) { 
                                    if (!requerimientoDA.GuardarEstadoFinalReqPestana(requerimiento.idRequerimiento, Convert.ToInt32(entry.Key)))
                                    {
                                        return false;
                                    }
                                }
                            }


                            //ELIMINAR EL ESTADO DE UN REQUERIMIENTO EN UNA DETERMINADA PESTAÑA (SOLO PUEDE BORRAR SI NO ESTA EN EL HASH DE AGREGAR NI EN EL DE EXISTENTE)
                            foreach (DictionaryEntry entry in hashBorrarEstadoFinalReqPestana)
                            {
                                if (!hashExistentesEstadoFinalReqPestana.Contains(entry.Key)  && !hashAgregarEstadoFinalReqPestana.Contains(entry.Key))
                                {
                                    if (!requerimientoDA.EliminarEstadoFinalReqPestana(requerimiento.idRequerimiento, Convert.ToInt32(entry.Key)))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }

                    if (requerimiento.flujoDocumental != null && requerimiento.flujoDocumental.id == rbTipo.SALIDA)
                    {
                        if (requerimiento.tipoSalida != null && requerimiento.tipoSalida.id == rbTipo.INFORMATIVO)
                        {

                            //GUARDAR EL DOCUMENTO ADJUNTO NUEVO
                            if (requerimiento.archivoAdjunto != null && requerimiento.archivoAdjunto.archivo != null)
                            {
                                if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitud(requerimiento.archivoAdjunto))
                                {
                                    return false;
                                }
                            }

                            //NO HICE NADA CON EL ARCHIVO, SOLO ACTUALIZAR EL REQUERIMIENTO 
                            if (requerimiento.archivoAdjunto != null && requerimiento.archivoAdjunto.idArchivo > 0)
                            {
                                //NO SE DEBE REALIZAR NADA
                            }

                            //BORRE EL ARCHIVO, SOLO ACTUALIZAR EL REQUERIMIENTO 
                            if (requerimiento.archivoAdjunto != null && requerimiento.archivoAdjunto.idArchivo == 0)
                            {
                                //NO SE DEBE REALIZAR NADA
                            }

                            //ACTUALIZAR EL REQUERIMIENTO
                            if (!requerimientoDA.GuardarRequerimiento(requerimiento, 0))
                            {
                                return false;
                            }

                            //GUARDAR/ELIMINAR LAS RELACIONES TABLA AMBIENTE/TIPO
                            if (requerimiento.ambitoTipo != null)
                            {

                                foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                                {

                                    //ACTUALIZAR LA ASOCIACION 
                                    documentoAmbito.idRequerimiento = requerimiento.idRequerimiento;
                                    documentoAmbito.idSolicitud = requerimiento.solicitud.idSolConcesion;
                                    if (!requerimientoDA.ActualizarDocumentoAsociado(documentoAmbito, idUsuario))
                                    {
                                        return false;
                                    }

                                    //SI CAMBIO EL AMBITO, SE DEBEN BORRAR EL ANTIGUO ESTADO E INGRESAR UN NUEVO
                                    if(documentoAmbito.ambito.id != documentoAmbito.ambitoAntiguo.id){

                                         //ELIMINAR EL ESTADO DE UN REQUERIMIENTO EN UNA DETERMINADA PESTAÑA (SOLO SI CAMBIO EL AMBITO)
                                        if (!requerimientoDA.EliminarEstadoFinalReqPestana(requerimiento.idRequerimiento, documentoAmbito.ambitoAntiguo.id))
                                        {
                                            return false;
                                        }

                                        //GUARDA EL ESTADO DE UN REQUERIMIENTO EN UNA DETERMINADA PESTAÑA
                                        if (!requerimientoDA.GuardarEstadoFinalReqPestana(requerimiento.idRequerimiento, documentoAmbito.ambito.id))
                                        {
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }


                    if (requerimiento.flujoDocumental != null && requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
                    {
                        if (requerimiento.tipoEntrada != null && requerimiento.tipoEntrada.id == rbTipo.INGRESO_SIN_REQUERIMIENTO)
                        {

                            //GUARDAR EL DOCUMENTO ADJUNTO NUEVO
                            if (requerimiento.archivoAdjunto != null && requerimiento.archivoAdjunto.archivo != null)
                            {
                                if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitud(requerimiento.archivoAdjunto))
                                {
                                    return false;
                                }
                            }

                            //NO HICE NADA CON EL ARCHIVO, SOLO ACTUALIZAR EL REQUERIMIENTO 
                            if (requerimiento.archivoAdjunto != null && requerimiento.archivoAdjunto.idArchivo > 0)
                            {
                                //NO SE DEBE REALIZAR NADA
                            }

                            //BORRE EL ARCHIVO, SOLO ACTUALIZAR EL REQUERIMIENTO 
                            if (requerimiento.archivoAdjunto != null && requerimiento.archivoAdjunto.idArchivo == 0)
                            {
                                //NO SE DEBE REALIZAR NADA
                            }


                            //ACTUALIZAR EL REQUERIMIENTO
                            if (!requerimientoDA.GuardarRequerimiento(requerimiento, idUsuario))
                            {
                                return false;
                            }


                            //GUARDAR/ELIMINAR LAS RELACIONES TABLA AMBIENTE/TIPO
                            if (requerimiento.ambitoTipo != null)
                            {

                                foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                                {

                                    //ACTUALIZAR LA ASOCIACION 
                                    documentoAmbito.idRequerimiento = requerimiento.idRequerimiento;
                                    documentoAmbito.idSolicitud = requerimiento.solicitud.idSolConcesion;
                                    if (!requerimientoDA.ActualizarDocumentoAsociado(documentoAmbito, idUsuario))
                                    {
                                        return false;
                                    }
                                 
                                }
                            }
                        }
                    }

                    if (requerimiento.flujoDocumental != null && requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
                    {
                        if (requerimiento.tipoEntrada != null && requerimiento.tipoEntrada.id == rbTipo.RESPUESTA_A_UN_REQUERIMIENTO)
                        {


                            //GUARDAR EL DOCUMENTO ADJUNTO NUEVO
                            if (requerimiento.archivoAdjunto != null && requerimiento.archivoAdjunto.archivo != null)
                            {
                                if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitud(requerimiento.archivoAdjunto))
                                {
                                    return false;
                                }
                            }

                            //NO HICE NADA CON EL ARCHIVO, SOLO ACTUALIZAR EL REQUERIMIENTO 
                            if (requerimiento.archivoAdjunto != null && requerimiento.archivoAdjunto.idArchivo > 0)
                            {
                                //NO SE DEBE REALIZAR NADA
                            }

                            //BORRE EL ARCHIVO, SOLO ACTUALIZAR EL REQUERIMIENTO 
                            if (requerimiento.archivoAdjunto != null && requerimiento.archivoAdjunto.idArchivo == 0)
                            {
                                //NO SE DEBE REALIZAR NADA
                            }


                            //GUARDAR EL REQUERIMIENTO
                            if (!requerimientoDA.GuardarRequerimiento(requerimiento, 0))
                            {
                                return false;
                            }


                            //BORRAR TODAS LAS RELACIONES DONDE EL REQUERIMIENTO MODIFICADO SEA LA RESPUESTA (SE VOLVERAN A INGRESAR)
                            if (!requerimientoDA.ActualizarDocumentacionPestanaRequerimiento(requerimiento.idRequerimiento, idUsuario))
                            {
                                return false;
                            }


                            //ACTUALIZAR LAS RELACIONES TABLA AMBIENTE/TIPO 
                            if (requerimiento.ambitoTipo != null)
                            {

                                foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                                {
                                    documentoAmbito.idDocGeneralResp = requerimiento.idRequerimiento;
                                    documentoAmbito.idSolicitud = requerimiento.solicitud.idSolConcesion;
                                    if (!requerimientoDA.GuardarDocumentoAsociado(documentoAmbito, idUsuario))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }


                    //SI HAY UN DOCUMENTO EN RECURSO DE REPOSICIÓN ENTONCES SE DEBE MARCAR COMO EN REPOSICION (PARA NO RECALCULAR ESTADOS)
                    if (requerimientoDA.existeRecursoReposicion(requerimiento.solicitud.idSolConcesion))
                    {
                        if (!solicitudConcesionService.ActualizaSolicitud_RecReposicion(requerimiento.solicitud.idSolConcesion, true, idUsuario))
                        {
                            return false;
                        }

                    }
                    else
                    {
                        if (!solicitudConcesionService.ActualizaSolicitud_RecReposicion(requerimiento.solicitud.idSolConcesion, false, idUsuario))
                        {
                            return false;
                        }
                    }




                    //AMPLIACIONES DE PLAZO

                    if (requerimiento.nuevaFecha != null && requerimiento.nuevaFecha != default(DateTime))
                    {


                        if (!requerimientoDA.ActualizarRequerimientoPlazo(requerimiento.solicitud.idSolConcesion, requerimiento.ambitoTipo[0].tipo.id, requerimiento.idRequerimiento, requerimiento.nuevaFecha))
                        {
                            return false;
                        }

                    }


                    //SUPEDITADO Y GRUPO SUSPENDIDO

                    /* Se eliminan las relaciones con supeditados si existen anteriormente para ser insert desde 0 */
                    List<DependenciaSupeditados> listDependenciaSupeditadosSolicitud = grupoSuspendidoDA.ListarDependenciaSupeditadosFiltro(0, 0, 0, requerimiento.ambitoTipo[0].idDocPestana);

                    if (listDependenciaSupeditadosSolicitud != null && listDependenciaSupeditadosSolicitud.Count > 0)
                    {
                        foreach (DependenciaSupeditados dependenciaSupeditadosAux in listDependenciaSupeditadosSolicitud)
                        {
                            if (!grupoSuspendidoDA.EliminarDependenciaSupeditados(dependenciaSupeditadosAux.idDepSupeditado, 0, 0, requerimiento.ambitoTipo[0].idDocPestana))
                            {
                                return false;
                            }
                        }
                    }


                    /* Sólo si aplica, se debe almacenar tipos de supeditados asociados */
                    if (requerimiento.dependenciaSupeditadosList != null && requerimiento.dependenciaSupeditadosList.Count > 0)
                    {
                        foreach (DependenciaSupeditados dependenciaSupeditado in requerimiento.dependenciaSupeditadosList)
                        {
                            dependenciaSupeditado.docPestana = new DocumentoAmbito();
                            dependenciaSupeditado.docPestana.idDocPestana = requerimiento.ambitoTipo[0].idDocPestana;

                            if (dependenciaSupeditado.solicitudConcesionDep != null && dependenciaSupeditado.solicitudConcesionDep.numPert != null)
                            {
                                int idSolicitud = solicitudDA.obtieneSolicitudIdPert(dependenciaSupeditado.solicitudConcesionDep.numPert);
                                dependenciaSupeditado.solicitudConcesionDep.idSolConcesion = idSolicitud;
                            }

                            if (dependenciaSupeditado.accion == accion.INGRESAR || dependenciaSupeditado.accion == accion.LISTADO)
                            {
                                dependenciaSupeditado.idDepSupeditado = 0;

                                if (!grupoSuspendidoDA.GuardarDependenciaSupeditados(dependenciaSupeditado))
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    /* Se eliminan las relaciones con grupos suspendidos si existen anteriormente para ser insert desde 0 (En este caso cambian de vigencia) */
                    List<AsocGrupoSolicitud> listAsocGrupoSolicitud = grupoSuspendidoDA.ListarAsocGrupoSolicitudFiltro(0, requerimiento.solicitud.idSolConcesion, 0, requerimiento.ambitoTipo[0].idDocPestana, 0);

                    if (listAsocGrupoSolicitud != null && listAsocGrupoSolicitud.Count > 0)
                    {
                        foreach (AsocGrupoSolicitud asocGrupoSolicitudAux in listAsocGrupoSolicitud)
                        {

                            if (!grupoSuspendidoDA.ActualizaAsocGrupoSolicitudVigencia(asocGrupoSolicitudAux.idAsocGrupoSolicitud, 0, 0, 0, rbEstadosGenerales.NO_VIGENTE, requerimiento.ambitoTipo[0].idDocPestana))
                            {
                                return false;
                            }
                        }
                    }

                    /* Sólo si aplica, se debe almacenar grupos suspendidos asociados */
                    if (requerimiento.asocGrupoSolicitudList != null && requerimiento.asocGrupoSolicitudList.Count > 0)
                    {
                        foreach (AsocGrupoSolicitud asocGrupoSolicitud in requerimiento.asocGrupoSolicitudList)
                        {
                            if ((asocGrupoSolicitud.accion == accion.INGRESAR || asocGrupoSolicitud.accion == accion.LISTADO) && (asocGrupoSolicitud.estadoVigencia == null || asocGrupoSolicitud.estadoVigencia.id == rbEstadosGenerales.VIGENTE))
                            {
                                asocGrupoSolicitud.idAsocGrupoSolicitud = 0;

                                asocGrupoSolicitud.docPestana = new DocumentoAmbito();
                                asocGrupoSolicitud.docPestana.idDocPestana = requerimiento.ambitoTipo[0].idDocPestana;

                                asocGrupoSolicitud.solicitudConcesion = asocGrupoSolicitud.solicitudConcesion;
                                asocGrupoSolicitud.solicitudConcesion.idSolConcesion = asocGrupoSolicitud.solicitudConcesion.idConcesion;

                                if (!grupoSuspendidoDA.GuardarAsocGrupoSolicitud(asocGrupoSolicitud))
                                {
                                    return false;
                                }
                            }
                        }
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




        //Quita asociaciones del requerimiento y elimina referencia en la tabla rbDocumentosGenerales (si aplica) 
        public bool EliminarRequerimiento(int idDocGeneral, int idPestana, int idSolConcesion, int idUsuario)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!requerimientoDA.EliminarRequerimiento(idDocGeneral, idPestana, idSolConcesion, idUsuario))
                    {
                        return false;
                    }



                    //SI HAY UN DOCUMENTO EN RECURSO DE REPOSICIÓN ENTONCES SE DEBE MARCAR COMO EN REPOSICION (PARA NO RECALCULAR ESTADOS)
                    if (requerimientoDA.existeRecursoReposicion(idSolConcesion))
                    {
                        if (!solicitudConcesionService.ActualizaSolicitud_RecReposicion(idSolConcesion, true, idUsuario))
                        {
                            return false;
                        }

                    }
                    else
                    {
                        if (!solicitudConcesionService.ActualizaSolicitud_RecReposicion(idSolConcesion, false, idUsuario))
                        {
                            return false;
                        }
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


        //Quita asociaciones de respuesta en la tabla rbDocumentosGenerales (si aplica) 
        public bool EliminarRespuesta(int idDocGeneralResp, int idPestana, int idSolConcesion, int idUsuario)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!requerimientoDA.EliminarRespuesta(idDocGeneralResp, idPestana, idSolConcesion, idUsuario))
                    {
                        return false;
                    }

                    //SI HAY UN DOCUMENTO EN RECURSO DE REPOSICIÓN ENTONCES SE DEBE MARCAR COMO EN REPOSICION (PARA NO RECALCULAR ESTADOS)
                    if (requerimientoDA.existeRecursoReposicion(idSolConcesion))
                    {
                        if (!solicitudConcesionService.ActualizaSolicitud_RecReposicion(idSolConcesion, true, idUsuario))
                        {
                            return false;
                        }

                    }
                    else
                    {
                        if (!solicitudConcesionService.ActualizaSolicitud_RecReposicion(idSolConcesion, false, idUsuario))
                        {
                            return false;
                        }
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



        //ACTUALIZA ESTADOS DEL REQUERIMIENTO
        public bool ActualizarRequerimientoEstado(int idDocGeneral, int idPestana, int idEstadoVigencia, int idUsuario)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {



                    if (!requerimientoDA.ActualizarRequerimientoEstado(idDocGeneral, idPestana, idEstadoVigencia, idUsuario))
                    {
                        return false;
                    }

                    Requerimiento requerimiento = this.ObtenerRequerimientoSinEstado(idDocGeneral);

                    //SI HAY UN DOCUMENTO EN RECURSO DE REPOSICIÓN ENTONCES SE DEBE MARCAR COMO EN REPOSICION (PARA NO RECALCULAR ESTADOS)
                    if (requerimientoDA.existeRecursoReposicion(requerimiento.solicitud.idSolConcesion))
                    {
                        if (!solicitudConcesionService.ActualizaSolicitud_RecReposicion(requerimiento.solicitud.idSolConcesion, true, idUsuario))
                        {
                            return false;
                        }

                    }
                    else
                    {
                        if (!solicitudConcesionService.ActualizaSolicitud_RecReposicion(requerimiento.solicitud.idSolConcesion, false, idUsuario))
                        {
                            return false;
                        }
                    }


                    //SI ERA UNA RESOLUCION DE CIERRE FORZADO, ENTONCES SE ACTUALIZARA EL REGISTRO
                    if (idEstadoVigencia == rbEstadosGenerales.VIGENTE)
                    {
                        if (!docGralCierreForzadoDA.ActualizarDocPestanaIT(idPestana, idDocGeneral, true))
                        {
                            return false;
                        }
                    }

                    //SI ERA UNA RESOLUCION DE CIERRE FORZADO, ENTONCES SE ACTUALIZARA EL REGISTRO
                    if (idEstadoVigencia == rbEstadosGenerales.NO_VIGENTE)
                    {
                        if (!docGralCierreForzadoDA.ActualizarDocPestanaIT(idPestana, idDocGeneral, false))
                        {
                            return false;
                        }
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

        

        //ACTUALIZA ESTADOS DE LA RESPUESTA
        public bool ActualizarRespuestaEstado(int idDocGeneral, int idPestana, int idEstadoVigencia, int idUsuario)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!requerimientoDA.ActualizarRespuestaEstado(idDocGeneral, idPestana, idEstadoVigencia, idUsuario))
                    {
                        return false;
                    }

                    Requerimiento requerimiento = this.ObtenerRespuesta(idDocGeneral);

                    //SI HAY UN DOCUMENTO EN RECURSO DE REPOSICIÓN ENTONCES SE DEBE MARCAR COMO EN REPOSICION (PARA NO RECALCULAR ESTADOS)
                    if (requerimientoDA.existeRecursoReposicion(requerimiento.solicitud.idSolConcesion))
                    {
                        if (!solicitudConcesionService.ActualizaSolicitud_RecReposicion(requerimiento.solicitud.idSolConcesion, true, idUsuario))
                        {
                            return false;
                        }

                    }
                    else
                    {
                        if (!solicitudConcesionService.ActualizaSolicitud_RecReposicion(requerimiento.solicitud.idSolConcesion, false, idUsuario))
                        {
                            return false;
                        }
                    }


                    //SI ERA UNA RESOLUCION DE CIERRE FORZADO, ENTONCES SE ACTUALIZARA EL REGISTRO
                    if (idEstadoVigencia == rbEstadosGenerales.VIGENTE) { 
                        if(!docGralCierreForzadoDA.ActualizarDocPestanaIT(idPestana, idDocGeneral, true)){
                            return false;
                        }
                    }

                    //SI ERA UNA RESOLUCION DE CIERRE FORZADO, ENTONCES SE ACTUALIZARA EL REGISTRO
                    if (idEstadoVigencia == rbEstadosGenerales.NO_VIGENTE)
                    {
                        if(!docGralCierreForzadoDA.ActualizarDocPestanaIT(idPestana, idDocGeneral, false)){
                         return false;
                        }
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
         * ACTUALIZA LA CONFORMIDAD O NO CONFORMIDAD DE LA DOCUMENTACION ENTREGADA EN UN REQUERIMIENTO
         */
        public bool ActualizarRequerimientoEstadoFinal(int idDocGeneral, int idEstadoFinal, int idSolConcesion)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {


                    if (!requerimientoDA.ActualizarRequerimientoEstadoFinal(idDocGeneral, idEstadoFinal, idSolConcesion))
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


        /*
        //OBTIENE UN REQUERIMIENTRO (SALIDA)
        public Requerimiento ObtenerRequerimiento(int idRequerimiento) {
            
            try
            {
                return requerimientoDA.ObtenerRequerimiento(idRequerimiento);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }
         * */

        //OBTIENE UN REQUERIMIENTO SIN CONSIDERAR EL ESTADO (SALIDA)
        public Requerimiento ObtenerRequerimientoSinEstado(int idRequerimiento)
        {

            try
            {
                return requerimientoDA.ObtenerRequerimientoSinEstado(idRequerimiento);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        /**
        * Obtiene un requerimiento de salida
        */

        /*
        public DataTable ObtieneRequerimientoDeSalida(int idRequerimiento)
        {

            try
            {
                return requerimientoDA.ObtieneRequerimientoDeSalida(idRequerimiento);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }

        }
         * */

        //OBTIENE UN REQUERIMIENTO SIN CONSIDERAR EL ESTADO (SALIDA)
        public DataTable ObtieneRequerimientoDeSalidaSinEstado(int idRequerimiento) {
            try
            {
                return requerimientoDA.ObtieneRequerimientoDeSalidaSinEstado(idRequerimiento);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        
        }


        //OBTIENE UN REQUERIMIENTO (ENTRADA)
        public Requerimiento ObtenerRespuesta(int idRequerimiento) {

            try
            {
                return requerimientoDA.ObtenerRespuesta(idRequerimiento);
            }
            catch (Exception ex) {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


    


        /**
        * Obtiene un requerimiento de entrada
        */
        public DataTable ObtieneRequerimientoDeEntrada(int idRequerimiento){
            
            try
            {
                return requerimientoDA.ObtieneRequerimientoDeEntrada(idRequerimiento);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }

        }


        /*
         *  Obtiene la lista de documentos generales a los cuales se les puede ingresar una entrada
         *  (deben ser del tipo salida y con respuesta)
         */
        public DataTable ListarRequerimientosIdSolicitud(int IdSolicitud, int idSeccion, int idOrigen, int idTipoDoc)
        {

            try
            {
                return requerimientoDA.ListarRequerimientosIdSolicitud(IdSolicitud, idSeccion, idOrigen, idTipoDoc);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        
        }



        public DataTable ListarRequerimientosIdSolicitudModificacion(int IdSolicitud, int idDocGeneral, int idSeccion)
        {

            try
            {
                return requerimientoDA.ListarRequerimientosIdSolicitudModificacion(IdSolicitud, idDocGeneral, idSeccion);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }

        }



        /**
        * Lista las posibles respuestas, si aplica, en un documento pedido en un requerimiento
        */
        public DataTable ListarPosiblesRespuestasSubRequerimiento(int IdSubRequerimiento)
        {

            try
            {
                return requerimientoDA.ListarPosiblesRespuestasSubRequerimiento(IdSubRequerimiento, 0);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }

        }

        /**
        * Lista las posibles respuestas, si aplica, en un documento pedido en un requerimiento
        */
        public DataTable ListarPosiblesRespuestasSubRequerimiento(int IdSubRequerimiento, int idTipoUE)
        {

            try
            {
                return requerimientoDA.ListarPosiblesRespuestasSubRequerimiento(IdSubRequerimiento, idTipoUE);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }

        }


        /**
        * Lista de documentacion pestaña incorporando el ambito y la respuesta al documento.
        */
        public List<DocumentoAmbito> ListarDocumentacionPestanaReqMod(int idDocGeneral, int idTipoDocumento)
        {

            try
            {
                return requerimientoDA.ListarDocumentacionPestanaReqMod(idDocGeneral, idTipoDocumento);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        
        }


        /**
        * Obtiene tipo de flujo documental asociado a un requerimiento
        */
        public int ObtieneFlujoDocumentoGeneral(int idDocGeneral) {


            try
            {
                return requerimientoDA.ObtieneFlujoDocumentoGeneral(idDocGeneral);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return 0;
            }
        
        }


          /**
         * Obtiene la lista de requerimientos asociados a una solicitud
         */
        public DataTable ListarRequerimiento(int IdSolicitud, int IdPestania, int idSeccion) {

            try
            {
                return requerimientoDA.ListarRequerimiento(IdSolicitud, IdPestania, idSeccion);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        
        }




        public ValidacionSEA valorSEA(Requerimiento requerimiento) { 
        

            ValidacionSEA validacionSEA = new ValidacionSEA();
            validacionSEA.esConsistente = true;

            bool? seaSi = null;
            bool? seaNo = null;

            Hashtable hashSeccionesSI = rbSeccion.hashSeccionesSI();
            Hashtable hashSeccionesNO = rbSeccion.hashSeccionesNO();


            if(requerimiento.ambitoTipo != null){

                foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                {
                    if (documentoAmbito.accion == accion.INGRESAR || documentoAmbito.accion == accion.LISTADO || documentoAmbito.accion == accion.MODIFICAR)
                    {
                        if (hashSeccionesSI.Contains(documentoAmbito.seccion.id))
                        {
                            seaSi = true;
                        }

                        if (hashSeccionesNO.Contains(documentoAmbito.seccion.id))
                        {
                            seaNo = true;
                        }
                    }
                }
            }


            //ES UN ERROR, SOLO SON DISTINTOS DE NULL SI SON VERDADEROS Y NO PUEDEN SER AMBOS VERDADEROS
            if (seaSi.HasValue && seaNo.HasValue)
            { 
                validacionSEA.esConsistente = false;

            }else{

                if (seaSi.HasValue)
                {
                    validacionSEA.valorSEA = 1;
                }
                if (seaNo.HasValue)
                {
                    validacionSEA.valorSEA = 2;
                }
            }

            return validacionSEA;
        }


        public DataTable ListarRequerimientosIdSolicitudPrinc(int IdSolicitud, int idSeccion, int idTipoTrayecto, int idTipoDoc) {

            try
            {
                return requerimientoDA.ListarRequerimientosIdSolicitudPrinc(IdSolicitud, idSeccion, idTipoTrayecto, idTipoDoc);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }

        
        }

        //OBTIENE EL POSIBLE DOCUMENTO QUE ESTA SIENDO "REITERADO"
        public DataTable ObtieneDocGralReiteraPadreBD(int idSolConcesion, int idSubReqReitera) {
            try
            {
                return requerimientoDA.ObtieneDocGralReiteraPadreBD(idSolConcesion, idSubReqReitera);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

           //OBTIENE UN SUB-REQUERIMIENTO
        public SubRequerimiento ObtenerSubRequerimiento(int idSubRequerimiento) {
            try
            {

                SubRequerimiento subRequerimiento = new SubRequerimiento();
                subRequerimiento.idSubRequerimiento = idSubRequerimiento;
                subRequerimiento.aplicaReiteraFiltro = -1;
                subRequerimiento.aplicaComplementarioFiltro = -1;
                subRequerimiento.aplicaVisacionMasivaFiltro = -1;

                return requerimientoDA.ObtenerSubRequerimiento(subRequerimiento);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        
        }

            //SE LE PASA UN REQUERIMIENTO Y ES DEL TIPO REITERA (PADRE O HIJO), DEVUELVE EL CONTRARIO, PADRE -> HIJO, HIJO -> PADRE
        public SubRequerimientoReitera ObtieneSubReqReiteraPadre(int idSubReqReitera)
        {
            try
            {
                return requerimientoDA.ObtieneSubReqReiteraPadre(idSubReqReitera);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        //SI SE INGRESA UN NUEVO INFORME/RESOLUCION PRINCIPAL, CAMBIA EL ESTADO AL ANTERIOR EXISTENTE
        public bool ActualizarDocumentacionPestanaDocPrinc(int idSolConcesion, int idTipoDocumento, int idSubRequerimiento, int idEstadoVigencia, int idUsuario)
        {
            try
            {
                return requerimientoDA.ActualizarDocumentacionPestanaDocPrinc(idSolConcesion, idTipoDocumento, idSubRequerimiento, idEstadoVigencia, idUsuario);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        
        }


         //OBTIENE LAS SOLICITUDES DONDE SE ESTA UTLIZANDO UN DETERMINADO CONTROL DE INGRESO
        public List<SolicitudConcesion> ObtenerSolicitudPorCIusado(int idSolConcesion, int numeroCI, int anio)
        {

            try
            {
                return requerimientoDA.ObtenerSolicitudPorCIusado(idSolConcesion, numeroCI, anio);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }


        }


        //RETONAR LOS TEMAS QUE RESPONDIO UN DOCUMENTO PRINCIPAL ASOCIADO A UN REQUERIMIENTO DE SALIDA EN PARTICULAR 
        public List<DocumentoAmbito> buscarTemasContestadosDocumentoPrincipal(int idDocGeneral, int idDocPrincipal)
        {
            try
            {
                return requerimientoDA.buscarTemasContestadosDocumentoPrincipal(idDocGeneral, idDocPrincipal);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        //RETORNA EL DOCUMENTO Resolución Pertinencia SEA, PARA SABER SI UNA SOLICITUD SE SOMETE O NO A SEA
        public DocumentoAmbito obtenerDocPestanaSubRequerimiento(int idSolicitud) {

            try
            {

                DocumentoAmbito documentoAmbito  = null;
                 
                documentoAmbito =   requerimientoDA.obtenerDocPestanaSubRequerimiento(idSolicitud, rbSubRequerimiento.NOTIFICACION_SMA);

                if (documentoAmbito == null) {
                    documentoAmbito = requerimientoDA.obtenerDocPestanaSubRequerimiento(idSolicitud, rbSubRequerimiento.REITERA_NOTIFICACION_SMA);
                }


                return documentoAmbito;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        
        }

        /**
         * INDICA SI CUMPLE VALIDACION DE EXISTENCIA DE DOCUMENTO REQUERIDO PARA EXTENDER EL PLAZO
         */ 
        public bool ObtieneRequerimientoPlazoExtension(RequerimientoPlazo reqPlazo) {
            try
            {
                return requerimientoDA.ObtieneRequerimientoPlazoExtension(reqPlazo);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        
        }


        public List<ParametroGenerico> ListarPosiblesRespuestasSupeditadasSubRequerimiento(int resultado)
        {
            try
            {
                return tipoDa.ListarTipo("SUPEDITADO_TIPO");
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        /**
        * Obtiene la lista de resoluciones y decretos asociados a una UE
        */
        public DataTable ListarResolucionesDecretos(int IdSolicitud, string numero, DateTime fecha, int idTipoDocumento, int idOrigen)
        {
            try
            {
                return requerimientoDA.ListarResolucionesDecretos(IdSolicitud, numero, fecha, idTipoDocumento, idOrigen);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }
        //Metodo para guardar archivos plantillas
        public bool GuardarPlanilla(DocPlanilla _DocPlanilla, int idUsuario, List<int> Items)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    if (Items.Count <= 0)
                    {
                        return false;
                    }
                    else
                    {
                        _DocPlanilla.vigencia = new ParametroGenerico(6);

                        int id = DocDA.GuardarDocPlanilla(_DocPlanilla);

                        if (id != 0)
                        {
                            foreach (int item in Items)
                            {
                                DocDA.GuardarDocPlanillaTramite(id, item);
                            }

                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //error :(
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
                transactionScope.Complete();
                return true;

            }
        }

        public bool EliminarPlanilla(int id)
        {
            bool Eliminar = false;
            using (TransactionScope _TransactionScope = new TransactionScope())
            {
                try
                {
                    if (id > 0)
                    {
                        Eliminar = DocDA.EliminarDocPlanillaTramite(id, 0);
                        if (Eliminar)
                        {
                            Eliminar = DocDA.EliminarDocPlanilla(id);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    //error :(
                    logger.PrintError(e);
                    logger.SendMailError(e);
                    return false;
                }
                _TransactionScope.Complete();
                return true;
            }
        }

        public bool CambiarEstado(int id, int vigencia)
        {
            try
            {
                using (TransactionScope _TransactionScope = new TransactionScope())
                {
                    if (id > 0)
                    {
                        DocDA.ActualizaDocPlanillaVigencia(id, vigencia);
                    }
                    _TransactionScope.Complete();
                    return true;
                }
            }
            catch (Exception e)
            {
                //error :(
                logger.PrintError(e);
                logger.SendMailError(e);
                return false;
            }

        }


        /**
         *  GUARDA (O ACTUALIZA) los Datos SEA de una solicitud
         */
        public bool GuardarSometimientoSEA(SometimientoSEA sometimiento)
        {
            try
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    bool resp = sometimientoSEA_DA.GuardarSometimientoSEA(sometimiento);

                    if (resp)
                    {
                        transactionScope.Complete();
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                logger.PrintError(e);
                logger.SendMailError(e);
                return false;
            }

        }
    }
}
