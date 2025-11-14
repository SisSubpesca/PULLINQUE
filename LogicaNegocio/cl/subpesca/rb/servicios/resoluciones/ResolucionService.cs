using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades.Resolucion;
using Datos.Entidades;
using Datos.Contantes;
using System.Transactions;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using LogicaNegocio.cl.subpesca.rb.resolucion;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Entidades.Resolucion.ResolucionSolicitud;
using System.Data;

namespace LogicaNegocio.cl.subpesca.rb.servicios.resoluciones
{
    public class ResolucionService
    {

        Logger logger = new Logger();
        RequerimientoService requerimientoService = new RequerimientoService();
        ResolucionDA resolucionDA = new ResolucionDA();
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();
        

        /**
         * GUARDA UNA RESOLUCION PROVIENTE DESDE EL ADMINISTRADOR DE RESOLUCIONES
         * SI SE REQUIERE TAMBIEN HACE UNA COPIA DEL DOCUMENTO Y LO GUARDA EN LAS SOLICITUDES CORRESPONDIENTES
         */
        public bool GuardarResolucion(Resolucion resolucion, int idUsuario)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {


                    //GUARDAR EL DOCUMENTO ADJUNTO
                    if (resolucion.archivoAdjunto != null && resolucion.archivoAdjunto.archivo != null)
                    {
                        if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitud(resolucion.archivoAdjunto))
                        {
                            return false;
                        }
                    }



                    //GUARDAR LA RESOLUCION
                    if (!resolucionDA.GuardarResolucion(resolucion, idUsuario))
                    {
                        return false;
                    }



                    if (resolucion.tieneReferencia == 1) {

                        //REFERENCIAS DE TITULAR
                        if (resolucion.referenciasTitular != null && resolucion.referenciasTitular.Count() > 0) { 
                        
                            foreach(Referencia referencia in resolucion.referenciasTitular){


                                if (referencia.accion == accion.INGRESAR || referencia.accion == accion.LISTADO || referencia.accion == accion.MODIFICAR)
                                {
                                    referencia.idResolucion = resolucion.idResolucion;
                                    if (!resolucionDA.GuardarReferenciaResolucion(referencia))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }


                        //REFERENCIAS DE ESPECIE
                        if (resolucion.referenciasEspecie != null && resolucion.referenciasEspecie.Count() > 0)
                        {

                            foreach (Referencia referencia in resolucion.referenciasEspecie)
                            {


                                if (referencia.accion == accion.INGRESAR || referencia.accion == accion.LISTADO || referencia.accion == accion.MODIFICAR)
                                {
                                    referencia.idResolucion = resolucion.idResolucion;
                                    if (!resolucionDA.GuardarReferenciaResolucion(referencia))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }


                        //REFERENCIAS DE UBICACION
                        if (resolucion.referenciasUbicacion != null && resolucion.referenciasUbicacion.Count() > 0)
                        {

                            foreach (Referencia referencia in resolucion.referenciasUbicacion)
                            {


                                if (referencia.accion == accion.INGRESAR || referencia.accion == accion.LISTADO || referencia.accion == accion.MODIFICAR)
                                {
                                    referencia.idResolucion = resolucion.idResolucion;
                                    if (!resolucionDA.GuardarReferenciaResolucion(referencia))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }


                        //REFERENCIA A DOCUMENTOS
                        if (resolucion.referenciasDocumento != null && resolucion.referenciasDocumento.Count() > 0)
                        {

                            foreach (Referencia referencia in resolucion.referenciasDocumento)
                            {


                                if (referencia.accion == accion.INGRESAR || referencia.accion == accion.LISTADO || referencia.accion == accion.MODIFICAR)
                                {
                                    referencia.idResolucion = resolucion.idResolucion;
                                    if (!resolucionDA.GuardarReferenciaResolucion(referencia))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }




                        //REFENCIA A UNIDADES ESPACIALES
                        if (resolucion.referenciasUnidadEspacial != null && resolucion.referenciasUnidadEspacial.Count() > 0)
                        {

                            foreach (Referencia referencia in resolucion.referenciasUnidadEspacial)
                            {


                                if (referencia.accion == accion.INGRESAR || referencia.accion == accion.LISTADO || referencia.accion == accion.MODIFICAR)
                                {
                                    referencia.idResolucion = resolucion.idResolucion;
                                    if (!resolucionDA.GuardarReferenciaResolucion(referencia))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }



                    }


                    //DETERMINA EL DOCUMENTO EQUIVALENTE QUE SE DEBE GENERAR DENTRO DE LA SOLICITUD O UNIDAD ESPACIAL
                    ResolucionValidacion resolucionValidacion = this.obtenerCombinatoriaResolucion(resolucion);

                    
                    resolucion.resolucionValidacion = resolucionValidacion;

                    if (!this.VincularDocumento(resolucion, idUsuario, resolucionValidacion.vincular))
                    {
                        return false;
                    }


                    /// Al ingresar una resolucion que aprueba un recurso de reposicion el IT DAC, 
                    /// Resolucion SSP y Resolucion SSFFAA de las solicitudes pasan a estar no vigentes
                    if (!resolucionDA.AcogeRecursoRepResolucion(resolucion.idResolucion))
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



        /**
         * ACTUALIZA UNA RESOLUCION PROVIENTE DESDE EL ADMINISTRADOR DE RESOLUCIONES
         * SI SE REQUIERE TAMBIEN HACE UNA COPIA DEL DOCUMENTO Y LO GUARDA EN LAS SOLICITUDES CORRESPONDIENTES
         */
        public bool ActualizarResolucion(Resolucion resolucion, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {


                    //BORRADO DE REFERENCIAS    
                    if (!resolucionDA.EliminarReferenciaResolucion(resolucion.idResolucion, 0))
                    {
                        return false;
                    }


                    //BORRAR LOS RBDOCUMENTOS GENERALES ASOCIADOS (DE EXISTIR)
                    if (resolucion.referenciasUnidadEspacial != null)
                    {

                        foreach (Referencia referenciaInList in resolucion.referenciasUnidadEspacial)
                        {

                            if (referenciaInList.idRefUEDocGeneral > 0 && referenciaInList.idRefUESolConcesion > 0)
                            {

                                if (!requerimientoService.EliminarRespuesta(referenciaInList.idRefUEDocGeneral, 0, referenciaInList.idRefUESolConcesion, idUsuario))
                                {
                                    return false;
                                }


                            }
                        }
                    }






                    //GUARDAR EL NUEVO DOCUMENTO ADJUNTO DE TENERLO
                    if (resolucion.archivoAdjunto != null && resolucion.archivoAdjunto.archivo != null)
                    {
                        if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitud(resolucion.archivoAdjunto))
                        {
                            return false;
                        }
                    }



                    //ACTUALIZAR LA RESOLUCION
                    if (!resolucionDA.GuardarResolucion(resolucion, idUsuario))
                    {
                        return false;
                    }



                    //GUARDAR LAS REFERENCIAS

                    if (resolucion.tieneReferencia == 1)
                    {

                        //REFERENCIAS DE TITULAR
                        if (resolucion.referenciasTitular != null && resolucion.referenciasTitular.Count() > 0)
                        {

                            foreach (Referencia referencia in resolucion.referenciasTitular)
                            {


                                if (referencia.accion == accion.INGRESAR || referencia.accion == accion.LISTADO || referencia.accion == accion.MODIFICAR)
                                {
                                    referencia.idResolucion = resolucion.idResolucion;
                                    if (!resolucionDA.GuardarReferenciaResolucion(referencia))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }


                        //REFERENCIAS DE ESPECIE
                        if (resolucion.referenciasEspecie != null && resolucion.referenciasEspecie.Count() > 0)
                        {

                            foreach (Referencia referencia in resolucion.referenciasEspecie)
                            {


                                if (referencia.accion == accion.INGRESAR || referencia.accion == accion.LISTADO || referencia.accion == accion.MODIFICAR)
                                {
                                    referencia.idResolucion = resolucion.idResolucion;
                                    if (!resolucionDA.GuardarReferenciaResolucion(referencia))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }


                        //REFERENCIAS DE UBICACION
                        if (resolucion.referenciasUbicacion != null && resolucion.referenciasUbicacion.Count() > 0)
                        {

                            foreach (Referencia referencia in resolucion.referenciasUbicacion)
                            {


                                if (referencia.accion == accion.INGRESAR || referencia.accion == accion.LISTADO || referencia.accion == accion.MODIFICAR)
                                {
                                    referencia.idResolucion = resolucion.idResolucion;
                                    if (!resolucionDA.GuardarReferenciaResolucion(referencia))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }


                        //REFERENCIA A DOCUMENTOS
                        if (resolucion.referenciasDocumento != null && resolucion.referenciasDocumento.Count() > 0)
                        {

                            foreach (Referencia referencia in resolucion.referenciasDocumento)
                            {


                                if (referencia.accion == accion.INGRESAR || referencia.accion == accion.LISTADO || referencia.accion == accion.MODIFICAR)
                                {
                                    referencia.idResolucion = resolucion.idResolucion;
                                    if (!resolucionDA.GuardarReferenciaResolucion(referencia))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }




                        //REFENCIA A UNIDADES ESPACIALES
                        if (resolucion.referenciasUnidadEspacial != null && resolucion.referenciasUnidadEspacial.Count() > 0)
                        {

                            foreach (Referencia referencia in resolucion.referenciasUnidadEspacial)
                            {


                                if (referencia.accion == accion.INGRESAR || referencia.accion == accion.LISTADO || referencia.accion == accion.MODIFICAR)
                                {

                                    referencia.idRefUEDocGeneral = 0;
                                    referencia.idResolucion = resolucion.idResolucion;
                                    if (!resolucionDA.GuardarReferenciaResolucion(referencia))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }



                    }


                    //DETERMINA SI SE DEBE COPIAR LA RESOLUCION EN rbDocumentosGenerales
                    ResolucionValidacion resolucionValidacion = this.obtenerCombinatoriaResolucion(resolucion);

                    //TODOS LAS RESOLUCIONES DEBEN VINCULARSE EN PULLINQUE 4
                    //if (resolucionValidacion.vincular == 1)
                    //{
                        resolucion.resolucionValidacion = resolucionValidacion;

                        if (!this.VincularDocumento(resolucion, idUsuario, resolucionValidacion.vincular))
                        {
                            return false;
                        }

                    //}


                    /// Al ingresar una resolucion que aprueba un recurso de reposicion el IT DAC, 
                    /// Resolucion SSP y Resolucion SSFFAA de las solicitudes pasan a estar no vigentes
                    
                    if (!resolucionDA.AcogeRecursoRepResolucion(resolucion.idResolucion))
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


        /**
         *  ARMA UN DOCUMENTO PARA INGRESARLO A SOLICITUD O UNIDAD ESPACIAL EN BASE A UNA RESOLUCION INGRESADA EN 
         *  ADMINISTRADOR DE RESOLUCIONES
         *  @perteneceFlujo = indica si es un documento existente en la planilla documental (Resolución SSP, Resolucion SSFFAA, RCA, ETC.)
         */
        public bool VincularDocumento(Resolucion resolucion, int idUsuario, int perteneceFlujo)
        { 

            try{




                //REFERENCIA A DOCUMENTOS
                if (resolucion.referenciasUnidadEspacial != null && resolucion.referenciasUnidadEspacial.Count() > 0)
                {

                    Requerimiento documentoClonada = new Requerimiento();

                    //Flujo Documental (Se asume que siempre es entrada)
                    documentoClonada.flujoDocumental = new ParametroGenerico(rbTipo.ENTRADA);

                    //Tipo entrada
                    documentoClonada.tipoEntrada = new ParametroGenerico(rbTipo.INGRESO_SIN_REQUERIMIENTO);


                    //Tipo de documento (Si es para flujo se debe poner el tipo de resolucion necesaria)
                    if (perteneceFlujo == 1)
                    {

                        // Resolucion o Resolucion Exenta  -> Resolucion (Principal o Complementaria)
                        if (resolucion.tipoDocumento.id == rbTipo.RESOLUCION || resolucion.tipoDocumento.id == rbTipo.RESOLUCION_EXENTA)
                        {

                            if (resolucion.tipoRelacionDocumento.id == rbTipo.RESOLUCION_PRINCIPAL)
                            {
                                documentoClonada.tipoDocumento = new ParametroGenerico(rbTipo.RESOLUCION_PRINCIPAL);
                            }


                            if (resolucion.tipoRelacionDocumento.id == rbTipo.RESOLUCION_COMPLEMENTARIA)
                            {
                                documentoClonada.tipoDocumento = new ParametroGenerico(rbTipo.RESOLUCION_COMPLEMENTARIA);

                                //SI ES COMPLEMENTARIA DEBE DECIRSE A QUE DOCUMENTO COMPLEMENTA
                                //@LLENAR

                            }

                        }

                        // Decreto o Decreto Exenta  -> Decreto (Principal o Complementaria)
                        if (resolucion.tipoDocumento.id == rbTipo.DECRETO || resolucion.tipoDocumento.id == rbTipo.DECRETO_EXENTO)
                        {

                            if (resolucion.tipoRelacionDocumento.id == rbTipo.RESOLUCION_PRINCIPAL)
                            {
                                documentoClonada.tipoDocumento = new ParametroGenerico(rbTipo.DECRETO);
                            }


                            if (resolucion.tipoRelacionDocumento.id == rbTipo.RESOLUCION_COMPLEMENTARIA)
                            {
                                documentoClonada.tipoDocumento = new ParametroGenerico(rbTipo.DECRETO_COMPLEMENTARIO);

                                //SI ES COMPLEMENTARIA DEBE DECIRSE A QUE DOCUMENTO COMPLEMENTA
                                //@LLENAR

                            }

                        }


                        //si es otro tipo de documento, se asume que es una resolucion, ya que solo pertenecen al flujo resoluciones 
                        if (documentoClonada.tipoDocumento == null) {
                            documentoClonada.tipoDocumento = new ParametroGenerico(rbTipo.RESOLUCION_PRINCIPAL);
                        }
                    }
                    else 
                    {
                        documentoClonada.tipoDocumento = resolucion.tipoDocumento;
                    }
                   

                    //Origen
                    documentoClonada.origen = resolucion.origen;

                    //LAS RESOLUCIONES SSP (QUE SON OCUPADAS PARA EL FLUJO), VIENEN DESDE LA DIVISION JURIDICA, NO DESDE SSPA
                    if (perteneceFlujo == 1 && resolucion.origen != null && resolucion.origen.id == rbTipoOrigenDestinatario.SSPA)
                    {
                        documentoClonada.origen = new ParametroGenerico(rbTipoOrigenDestinatario.DIVISION_JURIDICA);
                    }
                    


                    //Numero
                    documentoClonada.numero = resolucion.numero;


                    //Numero C.I.
                    documentoClonada.numeroCI = resolucion.numeroCI;

                    //Fecha
                    documentoClonada.fecha = resolucion.fecha;


                    //Fecha CI
                    documentoClonada.fechaCI = resolucion.fechaCI;



                    //SUBREQUERIMIENTO CONTENIDO DE LA SOLUCION
                    DocumentoAmbito documentoAmbito = new DocumentoAmbito();
                    documentoClonada.ambitoTipo = new List<DocumentoAmbito>();
                    documentoAmbito.estadoVigencia = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
                    documentoClonada.ambitoTipo.Add(documentoAmbito);



                    if (perteneceFlujo == 1 && resolucion.resolucionValidacion.subRequerimiento.id == rbSubRequerimiento.RESOLUCION_SSP) //Resolución SSP (132)
                    {

                        documentoAmbito.ambito = new ParametroGenerico(rbPestana.RESOLUCION_SSP);
                        documentoAmbito.seccion = new ParametroGenerico(rbSeccion.RESOLUCION_SSP);
                        documentoAmbito.estadoVigencia = resolucion.vigencia;
                    }
                    else if (perteneceFlujo == 1 && resolucion.resolucionValidacion.subRequerimiento.id == rbSubRequerimiento.RESOLUCION_SSFFAA) //Resolución SSFFAA (130)
                    {

                        documentoAmbito.ambito = new ParametroGenerico(rbPestana.RESOLUCION_SSFFAA);
                        documentoAmbito.seccion = new ParametroGenerico(rbSeccion.RESOLUCION_SSFFAA);
                        documentoAmbito.estadoVigencia = resolucion.vigencia;

                    }
                    else if (perteneceFlujo == 1 && resolucion.resolucionValidacion.subRequerimiento.id == rbSubRequerimiento.CARTA_SOMETIMIENTO_SEA_RCA) //Carta Sometimiento SEA/RCA (30)
                    {

                        documentoAmbito.ambito = new ParametroGenerico(rbPestana.INFORME_SEA);
                        documentoAmbito.seccion = new ParametroGenerico(rbSeccion.RESOLUCION_CALIFICACION_AMBIENTAL);
                        documentoAmbito.estadoVigencia = resolucion.vigencia;

                    }
                    else if (perteneceFlujo == 1 && resolucion.resolucionValidacion.subRequerimiento.id == rbSubRequerimiento.DECRETO_SSFFAA) //Decreto SSFFAA (404)
                    {

                        documentoAmbito.ambito = new ParametroGenerico(rbPestana.RESOLUCION_SSFFAA);
                        documentoAmbito.seccion = new ParametroGenerico(rbSeccion.RESOLUCION_SSFFAA);
                        documentoAmbito.estadoVigencia = resolucion.vigencia;

                    }
                    else if (perteneceFlujo == 1 && resolucion.resolucionValidacion.subRequerimiento.id == rbSubRequerimiento.RESUELVE_APELACION) //Resuelve Apelación (135)
                    {

                        documentoAmbito.ambito = new ParametroGenerico(rbPestana.DIFUSION_BANCO_NATURAL);
                        documentoAmbito.seccion = new ParametroGenerico(rbSeccion.DIFUSION_BANCO_NATURAL);
                        documentoAmbito.estadoVigencia = resolucion.vigencia;

                    }
                    else if (perteneceFlujo == 1 && resolucion.resolucionValidacion.subRequerimiento.id == rbSubRequerimiento.RESOLUCION_RECURSO) //Resolución Recurso (133)
                    {

                        documentoAmbito.ambito = new ParametroGenerico(rbPestana.RESOLUCION_SSP);
                        documentoAmbito.seccion = new ParametroGenerico(rbSeccion.RESOLUCION_SSP);
                        documentoAmbito.estadoVigencia = resolucion.vigencia;

                    }
                    else if (perteneceFlujo == 1 && resolucion.resolucionValidacion.subRequerimiento.id == rbSubRequerimiento.RESOLUCION_AMPLIACION_CARTA_MO) //Resolución Ampliación de Plazo por Carta MO (192)
                    {

                        documentoAmbito.ambito = new ParametroGenerico(rbPestana.INFORME_SEA);
                        documentoAmbito.seccion = new ParametroGenerico(rbSeccion.ANTECEDENTES_AMBIENTALES_MO);
                        documentoAmbito.estadoVigencia = resolucion.vigencia;

                    }
                    else if (perteneceFlujo == 1 && resolucion.resolucionValidacion.subRequerimiento.id == rbSubRequerimiento.RESOLUCION_AMPLIACION_CARTA_CPS_INFAS) //Resolución Ampliación de Plazo Recopilación de CPS e INFAS (532)
                    {

                        documentoAmbito.ambito = new ParametroGenerico(rbPestana.INFORME_SEA);
                        documentoAmbito.seccion = new ParametroGenerico(rbSeccion.ANTECEDENTES_AMBIENTALES_MO);
                        documentoAmbito.estadoVigencia = resolucion.vigencia;

                    }
                    else if (perteneceFlujo == 1 && resolucion.resolucionValidacion.subRequerimiento.id == rbSubRequerimiento.RESOLUCION_AMPLIACION_CARTA_SOMETIMIENTO) //Resolución Ampliación de Plazo por Sometimiento SEIA (194)
                    {

                        documentoAmbito.ambito = new ParametroGenerico(rbPestana.INFORME_SEA);
                        documentoAmbito.seccion = new ParametroGenerico(rbSeccion.ANTECEDENTES_AMBIENTALES);
                        documentoAmbito.estadoVigencia = resolucion.vigencia;

                    }
                    else { 

                        //TODOS LOS OTROS TIPOS DE RESOLUCIONES SE GUARDAN EN LA "PESTANA RESOLUCIONES"  
                        //(NO EXISTE EN LA INTERFAZ DE LAS SOLICITUDES, PERO APARECERAN EN EL MANEJADOR GENERAL DE DOCUMENTOS)
                        documentoAmbito.ambito = new ParametroGenerico(rbPestana.RESOLUCIONES);
                        documentoAmbito.seccion = new ParametroGenerico(rbSeccion.RESOLUCIONES);
                        documentoAmbito.estadoVigencia = resolucion.vigencia;
                    
                    }


                    //TIPO (subRequerimiento/Materia) 
                    documentoAmbito.tipo = resolucion.resolucionValidacion.subRequerimiento;


                

                    //RESULTADO (SI SON OCUPADADAS PARA EL FLUJO SE DEBE PONER EL RESULTADO DEL REQUERIMIENTO, EN CASO CONTRARIO ES EL RESULTADO DE LA MATERIA) 
                    if (perteneceFlujo == 1)
                    {
                        documentoAmbito.estadoResultadoResp = resolucion.resolucionValidacion.estadoSubRequerimiento;
                    }
                    else 
                    {
                        documentoAmbito.estadoResultadoResp = resolucion.resolucionValidacion.estadoMateria;
                    }

                    //ARCHIVO BINARIO            
                    if (resolucion.archivoAdjunto != null) {
                        documentoClonada.archivoAdjunto = new ArchivoBinario();
                        documentoClonada.archivoAdjunto.idArchivo = resolucion.archivoAdjunto.idArchivo;
                        //documentoClonada.archivoAdjunto = resolucion.archivoAdjunto;
                    }



                    //VIGENCIA
                    documentoClonada.estadoVigencia = resolucion.vigencia;


                    foreach (Referencia referencia in resolucion.referenciasUnidadEspacial)
                    {
                        //SOLO SE CLONA SI LA RELACION REFERENCIA -RESOLUCION ESTA VIGENTE
                        if (referencia.estadoVigencia.id == rbEstadosGenerales.VIGENTE) { 

                            if (referencia.accion == accion.INGRESAR || referencia.accion == accion.LISTADO || referencia.accion == accion.MODIFICAR)
                            {

                                //reiniciar los identificadores
                                documentoClonada.idRequerimiento = 0;

                                if (documentoClonada.ambitoTipo != null)
                                {
                                    foreach (DocumentoAmbito auxDoc in documentoClonada.ambitoTipo)
                                    {
                                        auxDoc.accion = accion.INGRESAR;
                                        auxDoc.idDocPestana = 0;
                                    }
                                }


                                //GUARDAR EL REQUERIMIENTO
                                documentoClonada.solicitud = referencia.solicitudUnidadEspacial;
                                if (!requerimientoService.guardarRequerimiento(documentoClonada, idUsuario))
                                {
                                    return false;
                                }


                                //ACTUALIZAR LA REFERENCIA
                                referencia.idRefUEDocGeneral = documentoClonada.idRequerimiento;
                                referencia.idRefUESolConcesion = documentoClonada.solicitud.idSolConcesion;


                                if (!resolucionDA.ActualizarReferenciaResolucion(referencia.idReferencia, documentoClonada.idRequerimiento,documentoClonada.solicitud.idSolConcesion,0)) { 
                                    return false;
                                }
                 
                            }



                            //VERIFICA SI YA EXISTE LA RESOLUCION DENTRO DE LA SOLICITUD O UNIDAD ESPACIAL, COMPARA EL NUMERO, LA FECHA (DIA MES Y AÑO), EL TIPO DE DOCUMENTO Y EL ORIGEN
                            //SI YA EXISTIA SE DEBE BORRAR
                            if (!resolucionDA.EliminarResolucionTramite(documentoClonada.ambitoTipo[0].idDocPestana, idUsuario))
                            {
                                return false;
                            }

                        }
                    }
                }


                return true;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }




        //OBTIENE EL DOCUMENTO EQUIVALENTE A UNA RESOLUCION  QUE SE DEBE GENERAR DENTRO DE LA SOLICITUD O UNIDAD ESPACIAL
        //SE DETERMINA EN BASE A EL TIPO DE DOCUMENTO, EL ORIGEN (TIPO DESTINATARIO) LA MATERIA Y EL RESULTADO
        //LA COMBINACION ESTA EN LA TABLA rbMateriaSubRequerimiento
        public ResolucionValidacion obtenerCombinatoriaResolucion(Resolucion resolucion)
        {

            try
            {

                if (resolucion.resultado == null)
                {
                    return resolucionDA.obtenerCombinatoriaResolucion(resolucion.tipoDocumento.id, resolucion.origen.id, resolucion.materia.id, 0);
                }
                else {
                    return resolucionDA.obtenerCombinatoriaResolucion(resolucion.tipoDocumento.id, resolucion.origen.id, resolucion.materia.id, resolucion.resultado.id);
                }
                


            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                throw ex;
            }
        }


        /**
         * DETERMINA SI LA MATERIA TIENE POSIBLES RESULTADOS
         */ 
        public bool MateriaTieneResultados(int idTipoDocumento, int idOrigen, int idMateria) {

            try
            { 

                return resolucionDA.MateriaTieneResultados(idTipoDocumento, idOrigen, idMateria);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                throw ex;
            }
        }


        /**
         *  OBTIENE UNA SOLICITUD O UNA UNIDAD ESPACIAL 
         */
        public SolicitudConcesion VerificaExistenciaReferencia(int idTipo, int idTipoUnidadEspacial, int idTipoSolicitud, string clave, int numSector) {


            try
            {

                return resolucionDA.VerificaExistenciaReferencia(idTipo, idTipoUnidadEspacial, idTipoSolicitud, clave, numSector);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                throw ex;
            }
        
        }

        /**
         * Lista todos los posibles origenes para una resolucion decreto
         */ 
        public List<ParametroGenerico> ListarPosiblesOrigenes() {


            try
            {

                return resolucionDA.ListarPosiblesOrigenes();

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
                
            }

        }

      
        /**
         * Lista todos los posibles materias para una resolucion decreto
         */
        public List<ParametroGenerico> ListarPosiblesMaterias()
        {

            try
            {

                return resolucionDA.ListarPosiblesMaterias();

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
                
            }

        }





        //OBTIENE UNA LISTA DE RESOLUCIONES
        public List<Resolucion> ListarResolucion(Resolucion resolucionFiltro)
        {
            try {

                return resolucionDA.ListarResolucion(resolucionFiltro);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;

            }
        }


        public List<Resolucion> BusquedaResolucion(Resolucion filtro)
        {
            try
            {

                return resolucionDA.BusquedaResolucion(filtro);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;

            }
        }

        //OBTIENE UNA RESOLUCION
        public Resolucion ObtenerResolucion(Resolucion filtro)
        {

            try
            {
                Resolucion resolucionAux = resolucionDA.ObtenerResolucion(filtro);

                if(resolucionAux != null){


                    //ARCHIVO BINARIO
                    if (resolucionAux.archivoAdjunto != null && resolucionAux.archivoAdjunto.idArchivo > 0) {
                        resolucionAux.archivoAdjunto  = archivoBinarioSolicitudDA.ObtenerArchivoBinarioSolicitud(resolucionAux.archivoAdjunto.idArchivo);
                    }


                    //REFERENCIAS
                    Referencia referenciaFiltro = new Referencia();
                    referenciaFiltro.idResolucion = resolucionAux.idResolucion;

                    List<Referencia> referencias = resolucionDA.ListarReferencia(referenciaFiltro);

                    int indexUC = 0;
                    int indexDocumento = 0;
                    int indexUbicacion = 0;
                    int indexEspecie = 0;
                    int indexTitular = 0;

                    if (referencias != null) {

                        foreach (Referencia referenciaInList in referencias) {

                            if (referenciaInList.idTipoReferencia == rbTipo.RESOLUCION_REFERENCIA_UC)
                            {
                                if (resolucionAux.referenciasUnidadEspacial == null) {
                                    resolucionAux.referenciasUnidadEspacial = new List<Referencia>();
                                }

                                referenciaInList.index = indexUC;
                                indexUC++;
                                resolucionAux.referenciasUnidadEspacial.Add(referenciaInList);
                            }
                            else if (referenciaInList.idTipoReferencia == rbTipo.RESOLUCION_REFERENCIA_DOCUMENTO)
                            {
                                if (resolucionAux.referenciasDocumento == null)
                                {
                                    resolucionAux.referenciasDocumento = new List<Referencia>();
                                }

                                referenciaInList.index = indexDocumento;
                                indexDocumento++;
                                resolucionAux.referenciasDocumento.Add(referenciaInList);
                            }
                            else if (referenciaInList.idTipoReferencia == rbTipo.RESOLUCION_REFERENCIA_UBICACION)
                            {
                                if (resolucionAux.referenciasUbicacion == null)
                                {
                                    resolucionAux.referenciasUbicacion = new List<Referencia>();
                                }

                                referenciaInList.index = indexUbicacion;
                                indexUbicacion++;
                                resolucionAux.referenciasUbicacion.Add(referenciaInList);
                            }
                            else if (referenciaInList.idTipoReferencia == rbTipo.RESOLUCION_REFERENCIA_ESPECIE)
                            {
                                if (resolucionAux.referenciasEspecie == null)
                                {
                                    resolucionAux.referenciasEspecie = new List<Referencia>();
                                }

                                referenciaInList.index = indexEspecie;
                                indexEspecie++;
                                resolucionAux.referenciasEspecie.Add(referenciaInList);
                            }
                            else if (referenciaInList.idTipoReferencia == rbTipo.RESOLUCION_REFERENCIA_TITULAR)
                            {
                                if (resolucionAux.referenciasTitular == null)
                                {
                                    resolucionAux.referenciasTitular = new List<Referencia>();
                                }
                                referenciaInList.index = indexTitular;
                                indexTitular++;
                                resolucionAux.referenciasTitular.Add(referenciaInList);
                            }

                                                    
                        }
                    
                    }
                }



                return resolucionAux;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
                
            }
        }




        //ELIMINA UNA RESOLUCION JUNTO A TODAS SUS DEPENDENCIAS
        public bool EliminarResolucion(int idResolucion, int idUsuario)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    Resolucion filtro = new Resolucion();
                    filtro.idResolucion = idResolucion;
                    Resolucion resolucion = this.ObtenerResolucion(filtro);

                    if (resolucion == null || resolucion.idResolucion < 0) {
                        return false;
                    }

                    //BORRADO DE REFERENCIAS    
                    if (!resolucionDA.EliminarReferenciaResolucion(resolucion.idResolucion, 0))
                    {
                        return false;
                    }


                    //BORRAR LOS RBDOCUMENTOS GENERALES ASOCIADOS (DE EXISTIR)
                    if (resolucion.referenciasUnidadEspacial != null)
                    {

                        foreach (Referencia referenciaInList in resolucion.referenciasUnidadEspacial)
                        {

                            if (referenciaInList.idRefUEDocGeneral > 0 && referenciaInList.idRefUESolConcesion > 0)
                            {

                                if (!requerimientoService.EliminarRespuesta(referenciaInList.idRefUEDocGeneral, 0, referenciaInList.idRefUESolConcesion, idUsuario))
                                {
                                    return false;
                                }


                            }
                        }
                    }



                    //BORRAR LA RESOLUCION
                    if (!resolucionDA.EliminarResolucion(resolucion.idResolucion, idUsuario))
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

        ///**
        // *  GUARDIA UNA ASOCIACION ENTRE UNA RESOLUCION Y UNA UNIDAD ESPACIAL
        // */
        //public bool GuardarResolucionSolicitud(ResolucionSolicitud resolucionSolicitud)
        //{

        //    using (TransactionScope transactionScope = new TransactionScope())
        //    {

        //        try
        //        {

        //            //BORRADO DE REFERENCIAS    
        //            if (!resolucionDA.GuardarResolucionSolicitud(resolucionSolicitud))
        //            {
        //                return false;
        //            }


        //            transactionScope.Complete();
        //            return true;

        //        }
        //        catch (Exception ex)
        //        {
        //            logger.PrintError(ex);
        //            logger.SendMailError(ex);
        //            return false;
        //        }
        //    }
        //}



        ///**
        // * LISTA LAS RESOLUCIONES ASOCIADAS MANUALMMENTE A UNA SOLICITUD
        // */
        //public List<ResolucionSolicitud> ListarResolucionSolicitud(int idSolicitud)
        //{

        //    try
        //    {
        //        return resolucionDA.ListarResolucionSolicitud(idSolicitud);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.PrintError(ex);
        //        logger.SendMailError(ex);
        //        return null;
        //    }
        
        //}


        /**
         * ELIMINA UNA ASOCIACION MANUAL DE UNA RESOLUCION A UNA UNIDAD ESPACIAL
         */
        public bool EliminarResolucionSolicitud(int idResolucion, int idSolConcesion) {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    if (!resolucionDA.EliminarResolucionSolicitud(idResolucion, idSolConcesion))
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



        ///**
        // * LISTA LAS RESOLUCIONES QUE SON POSIBLES DE ASOCIAR MANUALMENTE A UNA UNIDAD ESPACIAL
        // */ 
        //public List<Resolucion> ListarResolucionesManuales(int idSolicitud) {

        //    try
        //    {
        //        return resolucionDA.ListarResolucionesManuales(idSolicitud);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.PrintError(ex);
        //        logger.SendMailError(ex);
        //        return null;
        //    }
        
        
        //}


        /**
         * VALIDA LA EXISTENCIA DE UNA RESOLUCION
         */
        public bool validarExistenciaResolucion(int idResolucion, int idTipoDocumento, int idOrigen, int idTipoIngreso, string numero, int anio)
        {
            try
            {
                return resolucionDA.validarExistenciaResolucion(idResolucion, idTipoDocumento, idOrigen, idTipoIngreso, numero, anio);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
            
        }


        /**
        * VALIDA LA EXISTENCIA DE UNA RESOLUCION PRINCIPAL
        */
        public Resolucion validarExistenciaResolucionPrincipal(int idTipoDocumento, int idOrigen,string numero, DateTime fecha)
        {
            try
            {
                return resolucionDA.validarExistenciaResolucionPrincipal(idTipoDocumento, idOrigen, numero, fecha);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        /**
         * DETERMINA SI UNA RESOLUCION ES COMPLEMENTADA POR OTRAS RESOLUCIONES, DE TAL FORMA DE SABER SI SE PUEDE BORRAR
         * EN CASO DE ESTAR COMPLEMENTADA NO SE PUEDE BORRAR, DEBEN PRIMERO BORRARSE LAS COMPLEMENTARIAS
         */
        public bool verificarComplementarias(int idResolucion)
        {

            try
            {
                DataTable complementarias = ListarResolucionesComplementan(idResolucion);

                if (complementarias != null && complementarias.Rows.Count > 0)
                {
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }


        }


        /**
         * Lista resoluciones y decretos que complementan una resolución principal
         */
        public DataTable ListarResolucionesComplementan(int idResolucion)
        {
            try
            {
                return resolucionDA.ListarResolucionComplementarias(idResolucion);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


       /**
       * Lista resoluciones y decretos que referencian a una resolucion 
       */
        public List<Resolucion> ListarResolucionReferenciada(int idResolucion)
        {
            try
            {
                return resolucionDA.ListarResolucionReferenciada(idResolucion);

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
