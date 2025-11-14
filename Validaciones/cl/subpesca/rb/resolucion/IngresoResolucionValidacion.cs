using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades.Resolucion;
using LogicaNegocio.cl.subpesca.rb.servicios.resoluciones;
using Datos.Contantes;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using Datos.Entidades.Resolucion.ResolucionSolicitud;
using System.Data;

namespace Validaciones.cl.subpesca.rb.resolucion
{
    public class IngresoResolucionValidacion
    {


        Logger logger = new Logger();
        ResolucionService resolucionService = new ResolucionService();
        RequerimientoService requerimientoService = new RequerimientoService();


        public List<String> validaIngresoResolucion(Resolucion resolucion)
        {

            List<String> errores = new List<String>();

            try
            {
                //Tipo Documento
                if (resolucion.tipoDocumento == null || resolucion.tipoDocumento.id < 1)
                {
                    errores.Add("Seleccione tipo de documento.");
                }



                //Tipo Ingreso Resolución
                if (resolucion.tipoIngreso == null || resolucion.tipoIngreso.id < 1)
                {
                    errores.Add("Seleccione Tipo Ingreso.");
                }


                //Fecha de Inicio de Plazo < Fecha Vencimiento
                if (resolucion.fechaInicioPlazo != null && resolucion.fechaInicioPlazo != default(DateTime) && resolucion.fechaVencimiento != null && resolucion.fechaVencimiento != default(DateTime))
                {
                    if (resolucion.fechaInicioPlazo > resolucion.fechaVencimiento)
                    {
                        errores.Add("Fecha de Inicio de Plazo debe ser menor que la Fecha de Vencimiento.");
                    }
                }

                //Tipo de Relación del Documento
                if (resolucion.tipoRelacionDocumento == null || resolucion.tipoRelacionDocumento.id < 1)
                {
                    errores.Add("Seleccione Tipo de Relación del Documento.");
                }
                else {

                    if (resolucion.tipoRelacionDocumento.id == rbTipo.RESOLUCION_COMPLEMENTARIA) {

                        bool chequearPrincipal = true;

                        if (resolucion.resolucionPrincipal.tipoDocumento == null || resolucion.resolucionPrincipal.tipoDocumento.id < 1)
                        {
                            errores.Add("Seleccione tipo de documento. Resolucion/Decreto Principal");
                            chequearPrincipal = false;
                        }

                        if (resolucion.resolucionPrincipal.origen == null || resolucion.resolucionPrincipal.origen.id < 1)
                        {
                            errores.Add("Seleccione Origen. Resolucion/Decreto Principal");
                            chequearPrincipal = false;
                        }

                        if (resolucion.resolucionPrincipal.numero == null || resolucion.resolucionPrincipal.numero.Trim().Equals(""))
                        {
                            errores.Add("Ingrese Número. Resolucion/Decreto Principal");
                            chequearPrincipal = false;
                        }

                        if (resolucion.resolucionPrincipal.fecha == null || resolucion.resolucionPrincipal.fecha == default(DateTime))
                        {
                            errores.Add("Ingrese Fecha. Resolucion/Decreto Principal");
                            chequearPrincipal = false;
                        }

                        if (chequearPrincipal) {

                            Resolucion resolucionComplementaria = resolucionService.validarExistenciaResolucionPrincipal(resolucion.resolucionPrincipal.tipoDocumento.id, resolucion.resolucionPrincipal.origen.id, resolucion.resolucionPrincipal.numero.Trim(), resolucion.resolucionPrincipal.fecha);

                            if (resolucionComplementaria == null || resolucionComplementaria.idResolucion < 1)
                            {
                                errores.Add("Resolucion/Decreto Principal no encontrado.");
                            }
                            else 
                            {
                                resolucion.resolucionPrincipal.idResolucion = resolucionComplementaria.idResolucion;
                            }
                        }
                    }
                
                }

                //Origen
                if (resolucion.origen == null || resolucion.origen.id < 1)
                {
                    errores.Add("Seleccione Origen.");
                }


                //Materia
                if (resolucion.materia == null || resolucion.materia.id < 1)
                {
                    errores.Add("Seleccione Materia");
                }
                else {

                    if (errores.Count() == 0)
                    {

                        bool debeTenerResultado = resolucionService.MateriaTieneResultados(resolucion.tipoDocumento.id, resolucion.origen.id, resolucion.materia.id);

                        if (debeTenerResultado)
                        {


                            //Resultado
                            if (resolucion.resultado == null || resolucion.resultado.id < 1)
                            {
                                errores.Add("Seleccione Resultado");
                            }

                        }
                    }
                
                }


                //ARCHIVO
                if (resolucion.archivoAdjunto == null)
                {
                    errores.Add("Seleccione Archivo");
                }


                //VIGENCIA
                if (resolucion.vigencia == null || resolucion.vigencia.id < 0)
                {
                    errores.Add("Seleccione Vigencia");
                }


                if (errores.Count() == 0) {

                 

                    if (errores.Count() == 0)
                    {
                        ResolucionValidacion resolucionValidacion = resolucionService.obtenerCombinatoriaResolucion(resolucion);


                        if (resolucionValidacion != null)
                        {

                            //NUMERO
                            if (resolucionValidacion.numero == 1) 
                            {
                                if (resolucion.numero == null ||resolucion.numero.Trim().Equals(""))
                                {
                                    errores.Add("Ingrese Número");        
                                }
                            }

                            //FECHA
                            if (resolucionValidacion.fecha == 1)
                            {
                                if (resolucion.fecha == null || resolucion.fecha == default(DateTime))
                                {
                                    errores.Add("Ingrese Fecha");
                                }
                            }

                            //NUMERO C.I.
                            if (resolucionValidacion.numeroCI == 1)
                            {
                                if (resolucion.numeroCI < 1)
                                {
                                    errores.Add("Ingrese Número C.I.");
                                }
                            }

                            //NUMERO FECHA CI
                            if (resolucionValidacion.fechaCI == 1 || resolucion.fechaCI == default(DateTime))
                            {
                                if (resolucion.fechaCI == null)
                                {
                                    errores.Add("Ingrese Fecha C.I.");
                                }
                            }

                          
                            //validar que la resolucion no este ya ingresada en el sistema
                            //Las resoluciones se identifican de manera única en el sistema. No debe repetirse bajo la combinación origen – tipo documento – número – fecha
                            if (
                                    resolucion.tipoDocumento != null && resolucion.tipoDocumento.id > 0  && 
                                    resolucion.origen != null && resolucion.origen.id > 0 && 
                                    resolucion.numero != null && !resolucion.numero.Trim().Equals("") && 
                                    resolucion.fecha != null && resolucion.fecha != default(DateTime))
                            {

                                bool existe = resolucionService.validarExistenciaResolucion(resolucion.idResolucion, resolucion.tipoDocumento.id, resolucion.origen.id, resolucion.tipoIngreso.id, resolucion.numero, resolucion.fecha.Year);

                                if(existe){
                                    errores.Add("Resolución ya existe");
                                }
                            }
                                
                        }
                        else {
                            errores.Add("Imposible validar documento.");
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

        public List<string> validarIngresoUnidadEspacial(Referencia newReferencia, List<Referencia> listaUnidadesEspaciales)
        {

            List<String> errores = new List<String>();

            try
            {

                //Tipo
                if (newReferencia.tipo == null || newReferencia.tipo.id < 0)
                {
                    errores.Add("Seleccione Tipo.");
                }


                if (errores.Count() == 0) {
                    
                    if (newReferencia.tipo.id ==  rbTipo.RESOLUCION_SUB_REFERENCIA_SOLICITUD) //Solicitud
                    {
                        if (newReferencia.tipoSolicitud == null || newReferencia.tipoSolicitud.id < 0) 
                        {
                            errores.Add("Seleccione Tipo Solicitud.");
                        }
                    }


                    if (newReferencia.tipo.id == rbTipo.RESOLUCION_SUB_REFERENCIA_UE) //Unidades Espaciales
                    {
                        if (newReferencia.tipoUnidadEspacial == null || newReferencia.tipoUnidadEspacial.id < 0)
                        {
                            errores.Add("Seleccione Tipo Unidad Espacial.");
                        }
                    }
                
                }


                if (errores.Count() == 0) {


                    if (newReferencia.tipo.id == rbTipo.RESOLUCION_SUB_REFERENCIA_SOLICITUD){ //Solicitud

                        if (newReferencia.tipoSolicitud.id == rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA) //COLECTORES
                        {

                            if (newReferencia.numeroIdentificador == null || newReferencia.numeroIdentificador.Equals(""))
                            {
                                errores.Add("Ingrese Número Identificador.");
                            }

                            if (errores.Count() == 0)
                            {

                                //VERIFICAR QUE EXISTA
                                SolicitudConcesion sol = resolucionService.VerificaExistenciaReferencia(newReferencia.tipo.id, 0,newReferencia.tipoSolicitud.id, newReferencia.numeroIdentificador.ToString(), 0);


                                if (sol == null || sol.idSolConcesion < 1)
                                {
                                    errores.Add("Solicitud No Encontrada.");
                                }else {
                                    newReferencia.solicitudUnidadEspacial = sol;
                                }

                            }

                        }
                        else if (newReferencia.tipoSolicitud.id == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION || newReferencia.tipoSolicitud.id == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION_RESA) //Sector Relocalizacion
                        {

                            if (newReferencia.numeroPert == null || newReferencia.numeroPert.Trim().Equals(""))
                            {
                                errores.Add("Ingrese Pert.");
                            }

                            if (newReferencia.numSector < 0)
                            {
                                errores.Add("Ingrese Nº Sector.");
                            }

                            if (errores.Count() == 0)
                            {

                                String pertRelocalizaciones = newReferencia.numeroPert + "-" + newReferencia.numSector.ToString();

                                //VERIFICAR QUE EXISTA
                                SolicitudConcesion sol = resolucionService.VerificaExistenciaReferencia(newReferencia.tipo.id, 0, newReferencia.tipoSolicitud.id, pertRelocalizaciones, newReferencia.numSector);


                                if (sol == null || sol.idSolConcesion < 1)
                                {
                                    errores.Add("Solicitud No Encontrada.");
                                }
                                else
                                {
                                    newReferencia.solicitudUnidadEspacial = sol;
                                }


                            }

                        }
                        else //OTROS TIPOS DE SOLICITUDES
                        {
                            if ((newReferencia.numeroPert == null || newReferencia.numeroPert.Trim().Equals("")) && (newReferencia.codigoCentroRegularizado == null || newReferencia.codigoCentroRegularizado.Trim().Equals("")))
                            {
                                errores.Add("Ingrese Pert.");
                            }

                            if (errores.Count() == 0)
                            {

                                //VERIFICAR QUE EXISTA
                                SolicitudConcesion sol;

                                //ESTA BUSCANDO UNA REGULARIZACION
                                if (newReferencia.codigoCentroRegularizado != null && !newReferencia.codigoCentroRegularizado.Trim().Equals("")) 
                                {
                                    sol = resolucionService.VerificaExistenciaReferencia(newReferencia.tipo.id, 1, newReferencia.tipoSolicitud.id, newReferencia.codigoCentroRegularizado.Trim(), 0);
                                }else{
                                
                                    sol = resolucionService.VerificaExistenciaReferencia(newReferencia.tipo.id, 0, newReferencia.tipoSolicitud.id, newReferencia.numeroPert, 0);
                                }
                                
                                


                                if (sol == null || sol.idSolConcesion < 1)
                                {
                                    errores.Add("Solicitud No Encontrada.");

                                }else {
                                    newReferencia.solicitudUnidadEspacial = sol;
                                }

                            }

                        }
                    }
                }



                //UNIDADES ESPACIALES
                if (newReferencia.tipo !=null && newReferencia.tipo.id == rbTipo.RESOLUCION_SUB_REFERENCIA_UE)
                { 

                        if (newReferencia.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_COLECTORES_DE_SEMILLA) //COLECTORES
                        {

                            if (newReferencia.numeroIdentificador == null || newReferencia.numeroIdentificador.Equals("")) 
                            {
                                errores.Add("Ingrese Número Identificador.");
                            }

                            if (errores.Count() == 0)
                            {

                                //VERIFICAR QUE EXISTA
                                SolicitudConcesion sol = resolucionService.VerificaExistenciaReferencia(newReferencia.tipo.id, rbTipo.UNID_ESPACIAL_COLECTORES_DE_SEMILLA, 0, newReferencia.numeroIdentificador.ToString(), 0);


                                if (sol == null || sol.idSolConcesion < 1)
                                {
                                    errores.Add("Unidad espacial no Encontrada.");
                                }else {
                                    newReferencia.solicitudUnidadEspacial = sol;
                                }

                            }

                        }
                        else //OTROS TIPOS DE UNIDADES ESPACIALES
                        {
                            if ((newReferencia.codigoCentro == null || newReferencia.codigoCentro.Trim().Equals("")) && (newReferencia.numeroPert == null || newReferencia.numeroPert.Trim().Equals("")))
                            {
                                errores.Add("Ingrese Código Centro.");
                            }

                            if (errores.Count() == 0)
                            {

                                //VERIFICAR QUE EXISTA
                                SolicitudConcesion sol = null;

                                 //ESTA BUSCANDO UNA UNIDAD ESPACIAL POR EL PERT (EXPERIMENTALES DE AMERB Y EXPERIMENTAL DE CONCESION)
                                if (newReferencia.numeroPert != null && !newReferencia.numeroPert.Trim().Equals(""))
                                {
                                    sol = resolucionService.VerificaExistenciaReferencia(newReferencia.tipo.id, newReferencia.tipoUnidadEspacial.id, 1, newReferencia.numeroPert, 0);
                                }
                                else {
                                    sol = resolucionService.VerificaExistenciaReferencia(newReferencia.tipo.id, newReferencia.tipoUnidadEspacial.id, 0, newReferencia.codigoCentro, 0);
                                }
                                

                                if (sol == null || sol.idSolConcesion < 1)
                                {
                                    errores.Add("Unidad espacial no Encontrada.");

                                }else {
                                    newReferencia.solicitudUnidadEspacial = sol;
                                }

                            }

                        }
                }


                //VALIDAR QUE LA RESOLUCION NO HAYA SIDO INGRESADA PREVIAMENTE A TRAVES DE LOS MANEJADORES DOCUMENTALES



                //if (numero == null || numero.Trim().Equals(""))
                //{
                //    errores.Add("Ingrese el Número de la resolución/decreto para poder validar la referencia.");
                //}

                //if(fecha == null || fecha.Trim().Equals(""))
                //{
                //    errores.Add("Ingrese fecha de la resolución/decreto para poder validar la referencia.");
                //}

                //if(idTipoDocumento < 1)
                //{
                //    errores.Add("Ingrese tipo documento de la resolución/decreto para poder validar la referencia.");
                //}

                //if(idTipoRelacionDocumento < 1)
                //{
                //    errores.Add("Ingrese Tipo de Relación del Documento de la resolución/decreto para poder validar la referencia.");
                //}

                //if(idOrigen < 1)
                //{
                //    errores.Add("Ingrese Origen de la resolución/decreto para poder validar la referencia.");
                //}

                if (errores.Count() == 0)
                {

                    //REVISAR QUE NO ESTE YA EN LA LISTA
                    if (listaUnidadesEspaciales != null && listaUnidadesEspaciales.Count > 0)
                    {

                        foreach (Referencia referenciaInList in listaUnidadesEspaciales)
                        {

                            if (referenciaInList.accion == accion.LISTADO || referenciaInList.accion == accion.INGRESAR)
                            {

                                if (referenciaInList.solicitudUnidadEspacial.idSolConcesion == newReferencia.solicitudUnidadEspacial.idSolConcesion)
                                {
                                    errores.Add("Referencia ya Ingresada.");
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

        public List<string> validarIngresoTitular(Solicitante titular, List<Referencia> listaTitulares)
        {
            List<String> errores = new List<String>();

            try
            {
                SolicitanteService solicitanteService = new SolicitanteService();
                Solicitante solicitante = new Solicitante();


                solicitante = solicitanteService.VerPersona(titular.rut, 0);

                if (solicitante == null || solicitante.rut == 0) {
                    errores.Add("Titular no encontrado.");
                }


                //REVISAR QUE NO ESTE YA EN LA LISTA
                if (listaTitulares != null && listaTitulares.Count > 0) {

                    foreach (Referencia titularInList in listaTitulares) {

                        if (titularInList.accion == accion.LISTADO || titularInList.accion == accion.INGRESAR) {

                            if (titularInList.titular.rut == titular.rut) {
                                errores.Add("Titular ya Ingresado.");
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

        public List<string> validaIngresoUbicacion(Referencia newReferencia, List<Referencia> listaUbicaciones)
        {

            List<String> errores = new List<String>();

            try
            {
                
                

                if (newReferencia == null || newReferencia.region == null || newReferencia.region.id < 1)
                {
                    errores.Add("Seleccione Región.");
                }


                if (newReferencia == null || newReferencia.comuna == null || newReferencia.comuna.id < 1)
                {
                    errores.Add("Seleccione Comuna.");
                }

                if (newReferencia == null || newReferencia.sector == null || newReferencia.sector.Trim().Equals("") )
                {
                    errores.Add("Ingrese Sector.");
                }


                //REVISAR QUE NO ESTE YA EN LA LISTA
                if (errores.Count() == 0 && listaUbicaciones != null && listaUbicaciones.Count > 0)
                {

                    foreach (Referencia ubicacionInList in listaUbicaciones)
                    {

                        if (ubicacionInList.accion == accion.LISTADO || ubicacionInList.accion == accion.INGRESAR)
                        {

                            if (ubicacionInList.region.id == newReferencia.region.id && ubicacionInList.comuna.id == newReferencia.comuna.id && ubicacionInList.sector.ToUpper().Trim().Equals(newReferencia.sector.ToUpper().Trim()))
                            {
                                errores.Add("Ubicación ya Ingresada.");
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

        public List<string> validaIngresoDocumento(Referencia newReferencia, List<Referencia> listaDocumento)
        {
            List<String> errores = new List<String>();

            try
            {



                if (newReferencia == null || newReferencia.origenReferencia == null || newReferencia.origenReferencia.id < 1)
                {
                    errores.Add("Seleccione Origen.");
                }

                if (newReferencia == null || newReferencia.fechaReferencia == null || newReferencia.fechaReferencia == default(DateTime))
                {
                    errores.Add("Ingrese Fecha.");
                }

                if (newReferencia == null || newReferencia.numeroReferencia == null || newReferencia.numeroReferencia.Trim().Equals(""))
                {
                    errores.Add("Ingrese Número.");
                }


                //REVISAR QUE NO ESTE YA EN LA LISTA
                if (errores.Count() == 0 && listaDocumento != null && listaDocumento.Count > 0)
                {

                    foreach (Referencia documentoInList in listaDocumento)
                    {

                        if (documentoInList.accion == accion.LISTADO || documentoInList.accion == accion.INGRESAR)
                        {

                            if (documentoInList.origenReferencia.id == newReferencia.origenReferencia.id && documentoInList.numeroReferencia == newReferencia.numeroReferencia && documentoInList.fechaReferencia == newReferencia.fechaReferencia)
                            {
                                errores.Add("Documento ya Ingresado.");
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

        public List<string> validaIngresoEspecie(Referencia newReferencia, List<Referencia> listaEspecies)
        {
            List<String> errores = new List<String>();

            try
            {



                if (newReferencia == null || newReferencia.especie == null || newReferencia.especie.id < 1)
                {
                    errores.Add("Seleccione Especie.");
                }


                //REVISAR QUE NO ESTE YA EN LA LISTA
                if (errores.Count() == 0 && listaEspecies != null && listaEspecies.Count > 0)
                {

                    foreach (Referencia especieInList in listaEspecies)
                    {

                        if (especieInList.accion == accion.LISTADO || especieInList.accion == accion.INGRESAR)
                        {

                            if (especieInList.especie.id == newReferencia.especie.id)
                            {
                                errores.Add("Especie ya Ingresada.");
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



        public List<string> validaGuardarResolucionSolicitud(ResolucionSolicitud resolucionSolicitud, List<ResolucionSolicitud> resolucionesGrilla)
        {
            List<String> errores = new List<String>();

            try
            {



                if (resolucionSolicitud == null || resolucionSolicitud.resolucion == null || resolucionSolicitud.resolucion.idResolucion < 1)
                {
                    errores.Add("Seleccione Resolución.");
                }


                
                //REVISAR QUE NO ESTE YA EN LA LISTA
                if (errores.Count() == 0 && resolucionesGrilla != null && resolucionesGrilla.Count > 0)
                {

                    foreach (ResolucionSolicitud resolucionSolicitudInList in resolucionesGrilla)
                    {

                        if (resolucionSolicitudInList.resolucion.idResolucion == resolucionSolicitud.resolucion.idResolucion)
                        {
                            errores.Add("Resolución ya Asociada.");
                            break;
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
    }
}


