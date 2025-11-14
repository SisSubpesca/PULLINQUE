using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Entidades;
using Datos.Entidades.Relocalizacion;
using LogicaNegocio.cl.subpesca.rb.relocalizacion;
using Datos.Contantes;
using System.Transactions;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.servicios.concesiones;
using System.Collections;

namespace LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion
{

    public class RelocalizacionService
    {

        Logger logger = new Logger();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        TramiteRelocalizacionDA tramiteRelocalizacionDA = new TramiteRelocalizacionDA();
        DetalleSectorDA detalleSectorDA = new DetalleSectorDA();
        OrigenSectorDA origenSectorDA = new OrigenSectorDA();

        SolicitudConcesionService solicitudConcesionService = new SolicitudConcesionService();
        ConcesionService concesionService = new ConcesionService();

        ProyectoTecnicoDA proyectoTecnicoDA = new ProyectoTecnicoDA();
        CoordenadaGeograficaDA coordenadaGeograficaDA = new CoordenadaGeograficaDA();
        ErroresRelocalizacionDA erroresRelocalizacionDA = new ErroresRelocalizacionDA();
        SolicitanteService solicitanteService = new SolicitanteService();
        RequerimientoDA requerimientoDA = new RequerimientoDA();
        UnidadEspacialDA unidadEspacialDA = new UnidadEspacialDA();

        //OBTIENE LISTADO DE PREFERENCIAS PARA RELOCALIZACION      
        public List<ParametroGenerico> ListarPreferenciaRelocalizacion()
        {
            try
            {
                return parametroGenericoDA.ListarPreferenciaRelocalizacion(new ParametroGenerico());
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        //GENERA UN TRAMITE DE RELOCACION (LOS SECTORES SE INGRESAR APARTE)
        public bool GenerarTramiteDeRelocalizacion(TramiteRelocalizacion tramiteRelocalizacion)
        {
            try
            {

                return tramiteRelocalizacionDA.GuardarTramiteRelocalizacion(tramiteRelocalizacion);
                
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }



        //OBTIENE EL TRAMITE DE RELOCALIZACION (SIN SUS SECTORES)
        public TramiteRelocalizacion ObtenerTramiteRelocalizacion(int idTramiteRel)
        {

            try
            {
                return tramiteRelocalizacionDA.ObtieneTramiteRelocalizacion(idTramiteRel, 0);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }



        //OBTIENE EL TRAMITE DE RELOCALIZACION (CON SUS SECTORES)
        public TramiteRelocalizacion ObtenerTramiteRelocalizacionCompleto(int idTramiteRel)
        {

            try
            {
                TramiteRelocalizacion tramiteRelocalizacion =  tramiteRelocalizacionDA.ObtieneTramiteRelocalizacion(idTramiteRel, 0);
                if (tramiteRelocalizacion != null) {
                    tramiteRelocalizacion.sectores = detalleSectorDA.ListarDetalleSector(tramiteRelocalizacion.idTramiteRel, 0, -1);

                    if (tramiteRelocalizacion.sectores == null || tramiteRelocalizacion.sectores.Count() == 0)
                    {
                        return null;
                    }
                }

                return tramiteRelocalizacion;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        //OBTIENE LOS SECTORES QUE NO SE PUEDEN MODIFICAR REDEFINIR (TIENEN SSP)
        public List<DetalleSector> ObtenerSectoresNoRedefinibles(int idTramiteRel)
        {

            try
            {
                return  detalleSectorDA.ListarDetalleSector(idTramiteRel, 0, 1);
                
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        //OBTIENE LOS SECTORES QUE NO SE PUEDEN MODIFICAR REDEFINIR (NO TIENEN SSP)
        public List<DetalleSector> ObtenerSectoresRedefinibles(int idTramiteRel)
        {

            try
            {
                return detalleSectorDA.ListarDetalleSector(idTramiteRel, 0, 0);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        //OBTIENE LOS ERRORES ASOCIADOS A UN TRAMITE DE RELOCALIZACION
        public List<ErroresRelocalizacion> ListarErroresRelocalizacion(int idTramite)
        {
            try
            {
                return erroresRelocalizacionDA.ListarErroresRelocalizacion(idTramite, true);
                
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        //OBTIENE LAS ALERTAS ASOCIADOS A UN TRAMITE DE RELOCALIZACION
        public List<ErroresRelocalizacion> ListarAlertasRelocalizacion(int idTramite)
        {
            try
            {
                return erroresRelocalizacionDA.ListarErroresRelocalizacion(idTramite, false);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        //GUARDA LOS SECTORES DE UN TRAMITE DE RELOCALIZACION (EL TRAMITE EN GENERAL YA FUE INGRESADO)
        public bool GuardarTramiteRelocalizacion(TramiteRelocalizacion tramiteRelocalizacion, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    //SI ES UN NUEVO TRAMITE, ENTONCES SE GUARDA PRIMERO EL TRAMITE

                    if (tramiteRelocalizacion.idTramiteRel == 0) {
                        if (!this.GenerarTramiteDeRelocalizacion(tramiteRelocalizacion)) {
                            return false;
                        }
                    }


                    //SECTORES
                    foreach (DetalleSector aDetalleSector in tramiteRelocalizacion.sectores)
                    {
                        if (aDetalleSector.accion == accion.INGRESAR || aDetalleSector.accion == accion.MODIFICAR || aDetalleSector.accion == accion.LISTADO)
                        {
                            aDetalleSector.tramiteRel = tramiteRelocalizacion;
                            if (!detalleSectorDA.GuardarDetalleSector(aDetalleSector))
                            {
                                return false;
                            }


                            //ORIGENES
                            foreach (OrigenSector aOrigenSector in aDetalleSector.origenes)
                            {

                                if (aOrigenSector.accion == accion.INGRESAR || aOrigenSector.accion == accion.MODIFICAR || aOrigenSector.accion == accion.LISTADO)
                                {

                                    aOrigenSector.idDetalleSector = aDetalleSector.idDetalleSector;
                                    if (!origenSectorDA.GuardarOrigenSector(aOrigenSector))
                                    {
                                        return false;
                                    }


                                    //PREFERENCIAS (SE DEBEN ELIMINAR SI YA EXISTEN)
                                    if (!origenSectorDA.EliminarDetallePreferenciaRel(aOrigenSector.idOrigenSector))
                                    {
                                        return false;
                                    }

                                    //PREFERENCIAS 
                                    if (aOrigenSector.preferencias != null) {

                                        foreach (ParametroGenerico aPreferencia in aOrigenSector.preferencias)
                                        {

                                            if (!origenSectorDA.GuardarDetallePreferenciaRel(aOrigenSector.idOrigenSector, aPreferencia.id))
                                            {
                                                return false;
                                            }
                                        }
                                    
                                    }
                                }


                                //ELIMINA EL ORIGEN Y LAS PREFERENCIAS
                                if (aOrigenSector.accion == accion.ELIMINAR)
                                {
                                    aOrigenSector.idDetalleSector = aDetalleSector.idDetalleSector;
                                    if (!origenSectorDA.EliminarOrigenSector(aOrigenSector.idOrigenSector))
                                    {
                                        return false;
                                    }
                                }
                                
                            }
                        }



                        //SE DEBE BORRAR EL SECTOR CON TODAS SUS DEPENDENCIAS (ORIGENES Y PREFERENCIA
                        if (aDetalleSector.accion == accion.ELIMINAR)
                        {
                            if (!detalleSectorDA.EliminarDetalleSector(aDetalleSector.idDetalleSector, 0, idUsuario, 1))
                            {
                                return false;
                            }
                        }
                    }


                    //BORRAR POSIBLES ERRORES EXISTENTES (SI AUN APLICAN, SE VOLVERAN A INGRESAR, LOS QUE ESTEAN COMO "IGNORADO" NO SE BORRARAN)
                    if (!this.erroresRelocalizacionDA.EliminarErroresRelocalizacion(tramiteRelocalizacion.idTramiteRel)) {
                        return false;
                    }


                    //VERIFICAR LOS ERRORES DEL TRAMITE
                    List<ErroresRelocalizacion> errores = this.verificarErroresRelocalizacion(tramiteRelocalizacion);


                    //INGRESAR LOS ERRORES
                    if (errores != null)
                    {
                        if (!this.GuardarErroresRelocalizacion(errores))
                        {
                            return false;
                        }
                    }


                    //INTENTAR SETEAR LAS SOLICITUDES ASOCIDADAS A CADA SECTOR DEL TRAMITE
                    if (!this.setearSolicitudesDelTramite(tramiteRelocalizacion, idUsuario))
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


        //REDEFINE LOS SECTORES DE UN TRAMITE DE RELOCALIZACION 
        public bool RedefinirTramiteRelocalizacion(TramiteRelocalizacion tramiteRelocalizacion, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    //SECTORES
                    foreach (DetalleSector aDetalleSector in tramiteRelocalizacion.sectores)
                    {
                        if (aDetalleSector.accion == accion.INGRESAR)
                        {

                            if (!detalleSectorDA.GuardarDetalleSector(aDetalleSector))
                            {
                                return false;
                            }


                            //ORIGENES
                            foreach (OrigenSector aOrigenSector in aDetalleSector.origenes)
                            {

                                if (aOrigenSector.accion == accion.INGRESAR || aOrigenSector.accion == accion.MODIFICAR || aOrigenSector.accion == accion.LISTADO)
                                {

                                    aOrigenSector.idDetalleSector = aDetalleSector.idDetalleSector;
                                    if (!origenSectorDA.GuardarOrigenSector(aOrigenSector))
                                    {
                                        return false;
                                    }


                                    //PREFERENCIAS (SE DEBEN ELIMINAR SI YA EXISTEN)
                                    if (!origenSectorDA.EliminarDetallePreferenciaRel(aOrigenSector.idOrigenSector))
                                    {
                                        return false;
                                    }

                                    //PREFERENCIAS 
                                    if (aOrigenSector.preferencias != null)
                                    {

                                        foreach (ParametroGenerico aPreferencia in aOrigenSector.preferencias)
                                        {

                                            if (!origenSectorDA.GuardarDetallePreferenciaRel(aOrigenSector.idOrigenSector, aPreferencia.id))
                                            {
                                                return false;
                                            }
                                        }

                                    }
                                }


                                //ELIMINA EL ORIGEN Y LAS PREFERENCIAS
                                if (aOrigenSector.accion == accion.ELIMINAR)
                                {
                                    aOrigenSector.idDetalleSector = aDetalleSector.idDetalleSector;
                                    if (!origenSectorDA.EliminarOrigenSector(aOrigenSector.idOrigenSector))
                                    {
                                        return false;
                                    }
                                }

                            }
                        }



                        //SE DEBE BORRAR EL SECTOR CON TODAS SUS DEPENDENCIAS (ORIGENES Y PREFERENCIA
                        if (aDetalleSector.accion == accion.ELIMINAR)
                        {
                            if (!detalleSectorDA.EliminarDetalleSector(aDetalleSector.idDetalleSector, 0, idUsuario, 1))
                            {
                                return false;
                            }
                        }


                        //SECTOR MODIFICADO, SE BORRA Y SE INGRESA DE NUEVO
                        if (aDetalleSector.accion == accion.MODIFICAR)
                        {

                            //SE DEBE BORRAR EL SECTOR CON TODAS SUS DEPENDENCIAS (ORIGENES Y PREFERENCIA
                            if (!detalleSectorDA.EliminarDetalleSector(aDetalleSector.idDetalleSector, 0, idUsuario, 0))
                            {
                                return false;
                            }
                            

                            //SE DEBE GUARDAR NUEVAMENTE EL SECTOR, SE CONSERVA LA SOLICITUD ASOCIADA, PERO SUS DOCUMENTOS PASAN A ESTAR NO VIGENTES.
                            if (!requerimientoDA.ActualizarNoVigenteDocSolicitud(aDetalleSector.idSolConcesion, idUsuario))
                            {
                                return false;
                            }

                            aDetalleSector.idDetalleSector = 0;
                            if (!detalleSectorDA.GuardarDetalleSector(aDetalleSector))
                            {
                                return false;
                            }


                            //ORIGENES
                            foreach (OrigenSector aOrigenSector in aDetalleSector.origenes)
                            {

                                if (aOrigenSector.accion == accion.INGRESAR || aOrigenSector.accion == accion.MODIFICAR || aOrigenSector.accion == accion.LISTADO)
                                {
                                    aOrigenSector.idOrigenSector = 0;
                                    aOrigenSector.idDetalleSector = aDetalleSector.idDetalleSector;
                                    if (!origenSectorDA.GuardarOrigenSector(aOrigenSector))
                                    {
                                        return false;
                                    }



                                    //PREFERENCIAS 
                                    if (aOrigenSector.preferencias != null)
                                    {

                                        foreach (ParametroGenerico aPreferencia in aOrigenSector.preferencias)
                                        {

                                            if (!origenSectorDA.GuardarDetallePreferenciaRel(aOrigenSector.idOrigenSector, aPreferencia.id))
                                            {
                                                return false;
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }


                    //BORRAR POSIBLES ERRORES EXISTENTES (SI AUN APLICAN, SE VOLVERAN A INGRESAR, LOS QUE ESTEAN COMO "IGNORADO" NO SE BORRARAN)
                    if (!this.erroresRelocalizacionDA.EliminarErroresRelocalizacion(tramiteRelocalizacion.idTramiteRel))
                    {
                        return false;
                    }


                    //VERIFICAR LOS ERRORES DEL TRAMITE
                    List<ErroresRelocalizacion> errores = this.verificarErroresRelocalizacion(tramiteRelocalizacion);


                    //INGRESAR LOS ERRORES
                    if (errores != null)
                    {
                        if (!this.GuardarErroresRelocalizacion(errores))
                        {
                            return false;
                        }
                    }


                    //INTENTAR SETEAR LAS SOLICITUDES ASOCIDADAS A CADA SECTOR DEL TRAMITE
                    if (!this.setearSolicitudesDelTramite(tramiteRelocalizacion, idUsuario))
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

     

        //SETEA EL ID DE LA SOLICITUD AL SECTOR
        //SI NO HAY ERRORES O ESTAN MARCADOS COMO "IGNORAR" ENTONCES SE DEBEN CREAR LAS SOLICITUDES POR CADA SECTOR Y ACTUALIZAR LOS SECTORES CON EL ID DE LAS SOLICITUDES 
        public bool setearSolicitudesDelTramite(TramiteRelocalizacion tramiteRelocalizacion, int idUsuario)
        { 
        
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {

                    List<ErroresRelocalizacion> erroresDB = this.ListarErroresRelocalizacion(tramiteRelocalizacion.idTramiteRel);

                    int cantidadErrores = 0;
                    if (erroresDB != null) {
                        foreach (ErroresRelocalizacion aErroresRelocalizacion in erroresDB) {
                            if (aErroresRelocalizacion.estadoError.id != rbEstadosGenerales.ERROR_IGNORADO) {
                                cantidadErrores++;
                            }
                        }
                    }


                    //SI NO HAY ERRORES O ESTAN MARCADOS COMO "IGNORAR" ENTONCES SE DEBEN CREAR LAS SOLICITUDES POR CADA SECTOR Y ACTUALIZAR LOS SECTORES CON EL ID DE LAS SOLICITUDES    
                    if (cantidadErrores == 0)
                    {

                        //INSERTAR LAS SOLICITUDES POR CADA SECTOR
                        SolicitudConcesion solicitudInicial = null;
                        foreach (DetalleSector aDetalleSector in tramiteRelocalizacion.sectores)
                        {
                            if (aDetalleSector.idSolConcesion == 0 && (aDetalleSector.accion == accion.INGRESAR || aDetalleSector.accion == accion.MODIFICAR || aDetalleSector.accion == accion.LISTADO))
                            {
                                solicitudInicial = new SolicitudConcesion();
                                solicitudInicial.numPert = tramiteRelocalizacion.numPert + "-" + Convert.ToString(aDetalleSector.numSector);
                                solicitudInicial.fechaRecepcion = tramiteRelocalizacion.fechaRecepcion;
                                solicitudInicial.fechaIngresoTramite = tramiteRelocalizacion.fechaIngresoTramite;
                                solicitudInicial.tipoUnidadEspacial = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION);

                                if (!solicitudConcesionService.guardarSolicitudConcesionRelocalizacion(solicitudInicial))
                                {
                                    return false;
                                }

                                if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO && aDetalleSector.origenes != null && aDetalleSector.origenes.Count > 0)
                                {

                                    //COPIAR ANTECEDENTES DEL SECTOR DE LA CONCESION DEL SECTOR CERO
                                    if (!proyectoTecnicoDA.GuardarProyectoTecnico(solicitudInicial.idSolConcesion, aDetalleSector.origenes[0].concesionOrigen.unidadEspacial.idSolicitud))
                                    {
                                        return false;
                                    }

                                    //COPIAR ANTECEDENTES DEL SECTOR DE LA CONCESION DEL SECTOR CERO
                                    if (!coordenadaGeograficaDA.GuardarCoordenadaGeograficaTramiteMod(solicitudInicial.idSolConcesion, aDetalleSector.origenes[0].concesionOrigen.unidadEspacial.idSolicitud))
                                    {
                                        return false;
                                    }

                                    //Copiar Unidad espacial de la concesión del sector 0.
                                    UnidadEspacial unidadEspacial = unidadEspacialDA.ObtieneUnidadEspacial(aDetalleSector.origenes[0].concesionOrigen.unidadEspacial.idSolicitud, 0);
                                    unidadEspacial.idUnidadEspacial = 0;
                                    unidadEspacial.idSolicitud = solicitudInicial.idSolConcesion;
                                    unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                    unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToString(aDetalleSector.origenes[0].concesionOrigen.codigoCentro);

                                    if (!unidadEspacialDA.GuardarUnidadEspacialRel(unidadEspacial, idUsuario))
                                    {
                                        return false;
                                    }


                                }

                                if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA && aDetalleSector.concesionDestino != null && aDetalleSector.concesionDestino.unidadEspacial.idSolicitud > 0)
                                {

                                    //COPIAR ANTECEDENTES DEL SECTOR DE LA CONCESION A LA CUAL SE ESTA FUSIONANDO
                                    if (!proyectoTecnicoDA.GuardarProyectoTecnico(solicitudInicial.idSolConcesion, aDetalleSector.concesionDestino.unidadEspacial.idSolicitud))
                                    {
                                        return false;
                                    }

                                    //COPIAR ANTECEDENTES DEL SECTOR DE LA CONCESION A LA CUAL SE ESTA FUSIONANDO
                                    if (!coordenadaGeograficaDA.GuardarCoordenadaGeograficaTramiteMod(solicitudInicial.idSolConcesion, aDetalleSector.concesionDestino.unidadEspacial.idSolicitud))
                                    {
                                        return false;
                                    }

                                    //Copiar Unidad espacial de la concesión a la cual se esta fusionando.
                                    UnidadEspacial unidadEspacial = unidadEspacialDA.ObtieneUnidadEspacial(aDetalleSector.concesionDestino.unidadEspacial.idSolicitud, 0);
                                    unidadEspacial.idUnidadEspacial = 0;
                                    unidadEspacial.idSolicitud = solicitudInicial.idSolConcesion;
                                    unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                    unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToString(aDetalleSector.concesionDestino.codigoCentro);

                                    if (!unidadEspacialDA.GuardarUnidadEspacialRel(unidadEspacial, idUsuario))
                                    {
                                        return false;
                                    }

                                }

                                aDetalleSector.idSolConcesion = solicitudInicial.idSolConcesion;


                                //ACTUALIZA EL SECTOR
                                if (!detalleSectorDA.GuardarDetalleSector(aDetalleSector))
                                {
                                    return false;
                                }



                                //SETEAR LOS TITULARES EN EL SECTOR 
                                List<Solicitante> solicitantes = solicitanteService.ListarTitularesDetalleSector(aDetalleSector.idSolConcesion);
                                if (solicitantes != null) {

                                    foreach (Solicitante solicitante in solicitantes) {

                                        solicitante.solicitud = solicitudInicial;
                                        solicitante.idEstadoAsociacion = rbEstadosGenerales.VIGENTE;
                                        solicitante.idPersonasLeg = 0;

                                        if (!solicitanteService.guardarSolicitante(solicitante, idUsuario))
                                        {
                                            return false;
                                        }
                                    }
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


        //RECALCULAR ERRORES DE RELOCALIZACION (CHECK)
        public bool recalcularErroresRelocalizacion(TramiteRelocalizacion tramiteRelocalizacion, int idUsuario)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {

                    //OBTENER LOS ERRORES
                    List<ErroresRelocalizacion> errores = this.verificarErroresRelocalizacion(tramiteRelocalizacion);


                    //BORRAR POSIBLES ERRORES EXISTENTES (SI AUN APLICAN, SE VOLVERAN A INGRESAR, LOS QUE ESTEN COMO "IGNORADO" NO SE BORRARAN)
                    if (!this.erroresRelocalizacionDA.EliminarErroresRelocalizacion(tramiteRelocalizacion.idTramiteRel))
                    {
                        return false;
                    }


                    //INGRESAR LOS ERRORES (SI YA ESTABA INGRESADO, NO SE VOLVERA A INGRESAR)
                    if (errores != null)
                    {
                        if (!this.GuardarErroresRelocalizacion(errores))
                        {
                            return false;
                        }
                    }


                    if (!this.setearSolicitudesDelTramite(tramiteRelocalizacion, idUsuario))
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


        //RECALCULAR ALERTAS DE RELOCALIZACION (NO CHECK)
        public bool recalcularAlertasRelocalizacion(TramiteRelocalizacion tramiteRelocalizacion)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {

                    //OBTENER LAS ALERTAS
                    List<ErroresRelocalizacion> alertas = this.verificarAlertasRelocalizacion(tramiteRelocalizacion);


                    //BORRAR POSIBLES ALERTAS EXISTENTES (SI AUN APLICAN, SE VOLVERAN A INGRESAR)
                    if (!this.erroresRelocalizacionDA.EliminarAlertasRelocalizacion(tramiteRelocalizacion.idTramiteRel))
                    {
                        return false;
                    }


                    //INGRESAR LOS ERRORES (SI YA ESTABA INGRESADO, NO SE VOLVERA A INGRESAR)
                    if (alertas != null)
                    {
                        if (!this.GuardarErroresRelocalizacion(alertas))
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


        //GUARDA LOS ERRORES EN UN TRAMITE DE RELOCALIZACION (CHECK)
        public bool GuardarErroresRelocalizacion(List<ErroresRelocalizacion> errores)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {

                    //VERIFICAR LAS ALERTAS DEL TRAMITE O SECTOR
                    if (errores != null)
                    {
                        foreach (ErroresRelocalizacion aErroresRelocalizacion in errores)
                        {
                            if (!erroresRelocalizacionDA.GuardarErroresRelocalizacion(aErroresRelocalizacion))
                            {
                                return false;
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



        //GUARDA LAS ALERTAS EN UN TRAMITE DE RELOCALIZACION (NO CHECK)
        public bool GuardarAlertasRelocalizacion(List<ErroresRelocalizacion> alertas)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {

                    //VERIFICAR LAS ALERTAS DEL TRAMITE O SECTOR
                    if (alertas != null)
                    {
                        foreach (ErroresRelocalizacion aErroresRelocalizacion in alertas)
                        {
                            if (!erroresRelocalizacionDA.GuardarErroresRelocalizacion(aErroresRelocalizacion))
                            {
                                return false;
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




        //ERRORES DE RELOCALIZACION (CHECK)
        public List<ErroresRelocalizacion> verificarErroresRelocalizacion(TramiteRelocalizacion tramiteRelocalizacion)
        {

            //ERRORES  
            List<ErroresRelocalizacion> errores = new List<ErroresRelocalizacion>();
            ErroresRelocalizacion error = null;


            try
            {

                SolicitudConcesion solicitudConcesion = null;
                
                SolicitudConcesion concesionValidacionTitular = null;
                Hashtable titularesPrimerCentroUsado = new Hashtable();
                
                Hashtable hectareasOrigenes = new Hashtable();
                Hashtable preferenciasRelocalizacion = new Hashtable();



                Hashtable hashSectoresCero = new Hashtable();


                foreach (DetalleSector aDetalleSector in tramiteRelocalizacion.sectores)
                {
                    if (aDetalleSector.accion == accion.INGRESAR || aDetalleSector.accion == accion.LISTADO || aDetalleSector.accion == accion.MODIFICAR)
                    {
                        if (aDetalleSector.esSectorCero) {
                            hashSectoresCero.Add(aDetalleSector.origenes[0].concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro, aDetalleSector.origenes[0].concesionOrigen);
                        }
                    }
                }

                bool cumpleValidacionTitular = false;

                //CENTRO NO EXISTE Y DIFERENCIA DE HECTAREAS 
                foreach (DetalleSector aDetalleSector in tramiteRelocalizacion.sectores)
                {

                    int regionPrimerOrigen = 0;

                    if (aDetalleSector.accion == accion.INGRESAR || aDetalleSector.accion == accion.LISTADO || aDetalleSector.accion == accion.MODIFICAR)
                    {
                        //LOS QUE TIENEN SSP DE RECHAZO SE DEJAN FUERA DE LA VALIDACION
                        if (aDetalleSector.contieneSSp == false || (aDetalleSector.contieneSSp == true && aDetalleSector.estadoSSp.id == rbEstadosGenerales.APRUEBA))
                        {


                            foreach (OrigenSector aOrigenSector in aDetalleSector.origenes)
                            {
                                if (aOrigenSector.accion == accion.INGRESAR || aOrigenSector.accion == accion.LISTADO || aOrigenSector.accion == accion.MODIFICAR)
                                {
                                    
                                    solicitudConcesion = concesionService.ObtieneConcesionExistente(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro, rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);


                                    //SI ES RELOCALIZACION CREA Y DEJO UN SECTOR CERO ENTONCES DEBE ESTAR MARCADA LA OPCION "UBICACION EN COMUNA CHAITEN O FIORDO AYSEN"
                                    if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA && hashSectoresCero.ContainsKey(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro))
                                    {

                                        bool cumpleValidacionDivision = false;


                                        if (aOrigenSector.preferencias != null) { 
                                            foreach(ParametroGenerico aPreferencia in  aOrigenSector.preferencias){
                                                if (aPreferencia.id == rbPreferenciaRelocalizacion.UBICACION_CHAITEN_AYSEN) {
                                                    cumpleValidacionDivision = true;        
                                                }
                                            }
                                        }


                                        if (!cumpleValidacionDivision) {

                                            error = new ErroresRelocalizacion();
                                            error.tramiteRel = tramiteRelocalizacion;
                                            error.detSector = aDetalleSector;
                                            error.tipoError = new ParametroGenerico(rbTipo.CREA_CONCESION_SIN_PREFERENCIA);
                                            error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                                            error.invalidante = true;
                                            error.origenSec = aOrigenSector;
                                            error.centro = new ParametroGenerico(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro);
                                            errores.Add(error);
                                        
                                        }
                                    
                                    }

                                    //EXISTENCIA DEL CENTRO
                                    if (solicitudConcesion == null)
                                    {
                                        error = new ErroresRelocalizacion();
                                        error.tramiteRel = tramiteRelocalizacion;
                                        error.detSector = aDetalleSector;
                                        error.tipoError = new ParametroGenerico(rbTipo.CENTRO_NO_EXISTE);
                                        error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                                        error.invalidante = true;
                                        error.origenSec = aOrigenSector;
                                        error.centro = new ParametroGenerico(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro);
                                        errores.Add(error);

                                    }
                                    else
                                    {
                                        cumpleValidacionTitular = false;

                                        if (concesionValidacionTitular != null) {
                                            
                                            if (concesionValidacionTitular.titularesSolConcesion != null)
                                            {
                                                foreach (Persona aPersona in solicitudConcesion.titularesSolConcesion)
                                                {
                                                    if (titularesPrimerCentroUsado.ContainsKey(aPersona.rutPersona)) {
                                                        cumpleValidacionTitular = true;
                                                        break;
                                                    }
                                                }
                                            }


                                            if (!cumpleValidacionTitular) {

                                                error = new ErroresRelocalizacion();
                                                error.tramiteRel = tramiteRelocalizacion;
                                                error.detSector = aDetalleSector;
                                                error.tipoError = new ParametroGenerico(rbTipo.MANEJO_DE_TITULARES);
                                                error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                                                error.invalidante = true;
                                                error.origenSec = aOrigenSector;
                                                error.centro = new ParametroGenerico(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro);
                                                errores.Add(error);
                                            
                                            }
                                        }


                                        //PARA VALIDAR LOS TITULARES CON LA PRIMERA CONCESION QUE SE HAYA UTILIZADO EN EL TRAMITE
                                        if (concesionValidacionTitular == null)
                                        {
                                            concesionValidacionTitular = solicitudConcesion;
                                            if (concesionValidacionTitular.titularesSolConcesion != null)
                                            {
                                                foreach (Persona aPersona in concesionValidacionTitular.titularesSolConcesion)
                                                {
                                                    titularesPrimerCentroUsado.Add(aPersona.rutPersona, aPersona);
                                                }
                                            }
                                        }


                                        //VALIDAR QUE LOS ORIGENES ESTEN DENTRO DE LA MISMA REGION
                                        if (regionPrimerOrigen > 0) { 

                                            if(regionPrimerOrigen != solicitudConcesion.region.id){

                                                error = new ErroresRelocalizacion();
                                                error.tramiteRel = tramiteRelocalizacion;
                                                error.detSector = aDetalleSector;
                                                error.tipoError = new ParametroGenerico(rbTipo.DIFERENCIA_REGION);
                                                error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                                                error.invalidante = true;
                                                error.origenSec = aOrigenSector;
                                                error.centro = new ParametroGenerico(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro);
                                                errores.Add(error);
                                            
                                            }
                                        
                                        }
                                        if (regionPrimerOrigen == 0) {
                                           regionPrimerOrigen = solicitudConcesion.region.id;   
                                        }


                                        //HECTAREAS PARTE 1
                                        if (hectareasOrigenes.ContainsKey(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro))
                                        {
                                            hectareasOrigenes[aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro] = Convert.ToSingle(hectareasOrigenes[aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro]) + aOrigenSector.superficieRelocalizada;
                                        }
                                        else
                                        {
                                            hectareasOrigenes.Add(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro, aOrigenSector.superficieRelocalizada);
                                        }
                                    }


                                    //PREFERENCIAS DE RELOCALIZACION 
                                    if (!aDetalleSector.esSectorCero)
                                    {


                                        if (preferenciasRelocalizacion.ContainsKey(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro))
                                        {
                                            List<ParametroGenerico> preferenciasHash = (List<ParametroGenerico>)preferenciasRelocalizacion[aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro];

                                            if (aOrigenSector.preferencias.Count != preferenciasHash.Count)
                                            {
                                                error = new ErroresRelocalizacion();
                                                error.tramiteRel = tramiteRelocalizacion;
                                                error.detSector = aDetalleSector;
                                                error.tipoError = new ParametroGenerico(rbTipo.PREFERENCIA_DE_RELOCALIZACION);
                                                error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                                                error.invalidante = true;
                                                error.origenSec = aOrigenSector;
                                                error.centro = new ParametroGenerico(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro);
                                                errores.Add(error);

                                            }
                                            else
                                            {

                                                if (aOrigenSector.preferencias.Count > 0)
                                                {

                                                    bool preferenciaIncluida = false;
                                                    foreach (ParametroGenerico aPreferencia in aOrigenSector.preferencias)
                                                    {
                                                        if (preferenciasHash != null)
                                                        {
                                                            foreach (ParametroGenerico aPreferenciasHash in preferenciasHash)
                                                            {
                                                                if (aPreferenciasHash.id == aPreferencia.id)
                                                                {
                                                                    preferenciaIncluida = true;
                                                                    break;
                                                                }
                                                            }
                                                        }

                                                        if (!preferenciaIncluida)
                                                        {

                                                            error = new ErroresRelocalizacion();
                                                            error.tramiteRel = tramiteRelocalizacion;
                                                            error.detSector = aDetalleSector;
                                                            error.tipoError = new ParametroGenerico(rbTipo.PREFERENCIA_DE_RELOCALIZACION);
                                                            error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                                                            error.invalidante = true;
                                                            errores.Add(error);
                                                        }

                                                        preferenciaIncluida = false;

                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            preferenciasRelocalizacion.Add(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro, aOrigenSector.preferencias);
                                        }
                                    }

                                }
                            }

                            //EXISTENCIA DEL CENTRO
                            if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA)
                            {
                                solicitudConcesion = concesionService.ObtieneConcesionExistente(aDetalleSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro, rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);

                                if (solicitudConcesion == null)
                                {

                                    error = new ErroresRelocalizacion();
                                    error.tramiteRel = tramiteRelocalizacion;
                                    error.detSector = aDetalleSector;
                                    error.tipoError = new ParametroGenerico(rbTipo.CENTRO_NO_EXISTE);
                                    error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                                    error.invalidante = true;
                                    error.centro = new ParametroGenerico(aDetalleSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro);
                                    errores.Add(error);

                                }
                                else {


                                    cumpleValidacionTitular = false;

                                    if (concesionValidacionTitular != null)
                                    {
                                        if (solicitudConcesion.titularesSolConcesion != null)
                                        {
                                            foreach (Persona aPersona in solicitudConcesion.titularesSolConcesion)
                                            {
                                                if (titularesPrimerCentroUsado.ContainsKey(aPersona.rutPersona))
                                                {
                                                    cumpleValidacionTitular = true;
                                                    break;
                                                }
                                            }
                                        }


                                        if (!cumpleValidacionTitular)
                                        {
                                            error = new ErroresRelocalizacion();
                                            error.tramiteRel = tramiteRelocalizacion;
                                            error.detSector = aDetalleSector;
                                            error.tipoError = new ParametroGenerico(rbTipo.MANEJO_DE_TITULARES);
                                            error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                                            error.invalidante = true;
                                            error.centro = new ParametroGenerico(aDetalleSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro);
                                            errores.Add(error);
                                        }
                                    }


                                    //VALIDAR QUE LOS ORIGENES ESTEN DENTRO DE LA MISMA REGION
                                    if (regionPrimerOrigen > 0)
                                    {
                                        if (regionPrimerOrigen != solicitudConcesion.region.id)
                                        {
                                            error = new ErroresRelocalizacion();
                                            error.tramiteRel = tramiteRelocalizacion;
                                            error.detSector = aDetalleSector;
                                            error.tipoError = new ParametroGenerico(rbTipo.DIFERENCIA_REGION);
                                            error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                                            error.invalidante = true;
                                            error.centro = new ParametroGenerico(aDetalleSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro);
                                            errores.Add(error);

                                        }
                                    }


                                    //Alerta 7: VALIDAR ORIGEN INGRESADO EN OTRO TRÁMITE DE RELOCALIZACIÓN (DISTINTO PERT)

                                    List<TramiteRelocalizacion> listaTramiteRelocalizacion = tramiteRelocalizacionDA.aplicaCentroEnOtroTramiteRel(tramiteRelocalizacion.idTramiteRel);

                                    if (listaTramiteRelocalizacion != null && listaTramiteRelocalizacion.Count > 0)
                                    {
                                        error = new ErroresRelocalizacion();
                                        error.tramiteRel = tramiteRelocalizacion;
                                        error.detSector = aDetalleSector;
                                        error.tipoError = new ParametroGenerico(rbTipo.EXISTE_CENTRO_ORIGEN_PERT_DISTINTO);
                                        error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                                        error.invalidante = true;
                                        error.centro = new ParametroGenerico(aDetalleSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro);
                                        errores.Add(error);
                                    }
                                    
                                }
                            }
                        }
                    }
                }


                //HECTAREAS PARTE 2
                if (hectareasOrigenes != null) {
                    foreach (DictionaryEntry entry in hectareasOrigenes)
                    {
                        solicitudConcesion = concesionService.ObtieneConcesionExistente(Convert.ToString(entry.Key), rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);

                        if (solicitudConcesion != null) {
                            if (solicitudConcesion.superficieCalculadaCultivo != Convert.ToSingle(entry.Value)) {
                               
                                error = new ErroresRelocalizacion();
                                error.tramiteRel = tramiteRelocalizacion;
                                error.tipoError = new ParametroGenerico(rbTipo.DIFERENCIA_DE_HECTAREAS);
                                error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                                error.invalidante = true;
                                error.centro = new ParametroGenerico(Convert.ToInt32(entry.Key));
                                errores.Add(error);
                            }
                        }
                    }
                }


                return errores;


            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
           
        }




        //ALERTAS DE RELOCALIZACION (NO CHECK)
        public List<ErroresRelocalizacion> verificarAlertasRelocalizacion(TramiteRelocalizacion tramiteRelocalizacion)
        {

            //ALERTAS  
            List<ErroresRelocalizacion> errores = new List<ErroresRelocalizacion>();
            ErroresRelocalizacion error = null;


            try
            {

                SolicitudConcesion solicitudConcesion = null;

                //SolicitudConcesion concesionValidacionTitular = null;
                Hashtable titularesPrimerCentroUsado = new Hashtable();

                Hashtable hectareasOrigenes = new Hashtable();
                Hashtable preferenciasRelocalizacion = new Hashtable();



                Hashtable hashSectoresCero = new Hashtable();


                foreach (DetalleSector aDetalleSector in tramiteRelocalizacion.sectores)
                {
                    if (aDetalleSector.accion == accion.INGRESAR || aDetalleSector.accion == accion.LISTADO || aDetalleSector.accion == accion.MODIFICAR)
                    {
                        if (aDetalleSector.esSectorCero)
                        {
                            hashSectoresCero.Add(aDetalleSector.origenes[0].concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro, aDetalleSector.origenes[0].concesionOrigen);
                        }
                    }
                }

                //bool cumpleValidacionTitular = false;

                //CENTRO NO EXISTE Y DIFERENCIA DE HECTAREAS 
                foreach (DetalleSector aDetalleSector in tramiteRelocalizacion.sectores)
                {

                    int regionPrimerOrigen = 0;

                    if (aDetalleSector.accion == accion.INGRESAR || aDetalleSector.accion == accion.LISTADO || aDetalleSector.accion == accion.MODIFICAR)
                    {
                        //LOS QUE TIENEN SSP DE RECHAZO SE DEJAN FUERA DE LA VALIDACION
                        if (aDetalleSector.contieneSSp == false || (aDetalleSector.contieneSSp == true && aDetalleSector.estadoSSp.id == rbEstadosGenerales.APRUEBA))
                        {


                            foreach (OrigenSector aOrigenSector in aDetalleSector.origenes)
                            {
                                if (aOrigenSector.accion == accion.INGRESAR || aOrigenSector.accion == accion.LISTADO || aOrigenSector.accion == accion.MODIFICAR)
                                {

                                    solicitudConcesion = concesionService.ObtieneConcesionExistente(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro, rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);

                                    /*
                                    //SI ES RELOCALIZACION CREA Y DEJO UN SECTOR CERO ENTONCES DEBE ESTAR MARCADA LA OPCION "UBICACION EN COMUNA CHAITEN O FIORDO AYSEN"
                                    if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA && hashSectoresCero.ContainsKey(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro))
                                    {

                                        bool cumpleValidacionDivision = false;


                                        if (aOrigenSector.preferencias != null)
                                        {
                                            foreach (ParametroGenerico aPreferencia in aOrigenSector.preferencias)
                                            {
                                                if (aPreferencia.id == rbPreferenciaRelocalizacion.UBICACION_CHAITEN_AYSEN)
                                                {
                                                    cumpleValidacionDivision = true;
                                                }
                                            }
                                        }


                                        if (!cumpleValidacionDivision)
                                        {

                                            error = new ErroresRelocalizacion();
                                            error.tramiteRel = tramiteRelocalizacion;
                                            error.detSector = aDetalleSector;
                                            error.tipoError = new ParametroGenerico(rbTipo.CREA_CONCESION_SIN_PREFERENCIA);
                                            error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                                            error.invalidante = false;
                                            error.origenSec = aOrigenSector;
                                            error.centro = new ParametroGenerico(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro);
                                            errores.Add(error);

                                        }

                                    }
                                    */

                                    //EXISTENCIA DEL CENTRO
                                    if (solicitudConcesion == null)
                                    {

                                        /*
                                        error = new ErroresRelocalizacion();
                                        error.tramiteRel = tramiteRelocalizacion;
                                        error.detSector = aDetalleSector;
                                        error.tipoError = new ParametroGenerico(rbTipo.CENTRO_NO_EXISTE);
                                        error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                                        error.invalidante = false;
                                        error.origenSec = aOrigenSector;
                                        error.centro = new ParametroGenerico(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro);
                                        errores.Add(error);
                                          */

                                    }
                                    else
                                    {

                                        /*
                                        cumpleValidacionTitular = false;

                                        if (concesionValidacionTitular != null)
                                        {

                                            if (concesionValidacionTitular.titularesSolConcesion != null)
                                            {
                                                foreach (Persona aPersona in solicitudConcesion.titularesSolConcesion)
                                                {
                                                    if (titularesPrimerCentroUsado.ContainsKey(aPersona.rutPersona))
                                                    {
                                                        cumpleValidacionTitular = true;
                                                        break;
                                                    }
                                                }
                                            }


                                            if (!cumpleValidacionTitular)
                                            {

                                                error = new ErroresRelocalizacion();
                                                error.tramiteRel = tramiteRelocalizacion;
                                                error.detSector = aDetalleSector;
                                                error.tipoError = new ParametroGenerico(rbTipo.MANEJO_DE_TITULARES);
                                                error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                                                error.invalidante = false;
                                                error.origenSec = aOrigenSector;
                                                error.centro = new ParametroGenerico(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro);
                                                errores.Add(error);

                                            }
                                        }


                                        //PARA VALIDAR LOS TITULARES CON LA PRIMERA CONCESION QUE SE HAYA UTILIZADO EN EL TRAMITE
                                        if (concesionValidacionTitular == null)
                                        {
                                            concesionValidacionTitular = solicitudConcesion;
                                            if (concesionValidacionTitular.titularesSolConcesion != null)
                                            {
                                                foreach (Persona aPersona in concesionValidacionTitular.titularesSolConcesion)
                                                {
                                                    titularesPrimerCentroUsado.Add(aPersona.rutPersona, aPersona);
                                                }
                                            }
                                        }

                                        */

                                        //VALIDAR QUE LOS ORIGENES ESTEN DENTRO DE LA MISMA REGION
                                        if (regionPrimerOrigen > 0)
                                        {

                                            if (regionPrimerOrigen != solicitudConcesion.region.id)
                                            {

                                                error = new ErroresRelocalizacion();
                                                error.tramiteRel = tramiteRelocalizacion;
                                                error.detSector = aDetalleSector;
                                                error.tipoError = new ParametroGenerico(rbTipo.DIFERENCIA_REGION);
                                                error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                                                error.invalidante = false;
                                                error.origenSec = aOrigenSector;
                                                error.centro = new ParametroGenerico(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro);
                                                errores.Add(error);

                                            }

                                        }
                                        if (regionPrimerOrigen == 0 && solicitudConcesion.region != null)
                                        {
                                            regionPrimerOrigen = solicitudConcesion.region.id;
                                        }


                                        /*

                                        //HECTAREAS PARTE 1
                                        if (hectareasOrigenes.ContainsKey(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro))
                                        {
                                            hectareasOrigenes[aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro] = Convert.ToSingle(hectareasOrigenes[aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro]) + aOrigenSector.superficieRelocalizada;
                                        }
                                        else
                                        {
                                            hectareasOrigenes.Add(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro, aOrigenSector.superficieRelocalizada);
                                        }
                                         
                                         * */
                                    }

                                    /*

                                    //PREFERENCIAS DE RELOCALIZACION 
                                    if (!aDetalleSector.esSectorCero)
                                    {


                                        if (preferenciasRelocalizacion.ContainsKey(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro))
                                        {
                                            List<ParametroGenerico> preferenciasHash = (List<ParametroGenerico>)preferenciasRelocalizacion[aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro];

                                            if (aOrigenSector.preferencias.Count != preferenciasHash.Count)
                                            {
                                                error = new ErroresRelocalizacion();
                                                error.tramiteRel = tramiteRelocalizacion;
                                                error.detSector = aDetalleSector;
                                                error.tipoError = new ParametroGenerico(rbTipo.PREFERENCIA_DE_RELOCALIZACION);
                                                error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                                                error.invalidante = false;
                                                error.origenSec = aOrigenSector;
                                                error.centro = new ParametroGenerico(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro);
                                                errores.Add(error);

                                            }
                                            else
                                            {

                                                if (aOrigenSector.preferencias.Count > 0)
                                                {

                                                    bool preferenciaIncluida = false;
                                                    foreach (ParametroGenerico aPreferencia in aOrigenSector.preferencias)
                                                    {
                                                        if (preferenciasHash != null)
                                                        {
                                                            foreach (ParametroGenerico aPreferenciasHash in preferenciasHash)
                                                            {
                                                                if (aPreferenciasHash.id == aPreferencia.id)
                                                                {
                                                                    preferenciaIncluida = true;
                                                                    break;
                                                                }
                                                            }
                                                        }

                                                        if (!preferenciaIncluida)
                                                        {

                                                            error = new ErroresRelocalizacion();
                                                            error.tramiteRel = tramiteRelocalizacion;
                                                            error.detSector = aDetalleSector;
                                                            error.tipoError = new ParametroGenerico(rbTipo.PREFERENCIA_DE_RELOCALIZACION);
                                                            error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                                                            error.invalidante = false;
                                                            errores.Add(error);
                                                        }

                                                        preferenciaIncluida = false;

                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            preferenciasRelocalizacion.Add(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro, aOrigenSector.preferencias);
                                        }
                                    }
                                     * 
                                     * */

                                }
                            }


                            

                            //EXISTENCIA DEL CENTRO
                            if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA)
                            {
                                solicitudConcesion = concesionService.ObtieneConcesionExistente(aDetalleSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro, rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);

                                if (solicitudConcesion == null)
                                {

                                    /*
                                    error = new ErroresRelocalizacion();
                                    error.tramiteRel = tramiteRelocalizacion;
                                    error.detSector = aDetalleSector;
                                    error.tipoError = new ParametroGenerico(rbTipo.CENTRO_NO_EXISTE);
                                    error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                                    error.invalidante = false;
                                    error.centro = new ParametroGenerico(aDetalleSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro);
                                    errores.Add(error);
                                     * */

                                }
                                else
                                {

                                    /*

                                    cumpleValidacionTitular = false;

                                    if (concesionValidacionTitular != null)
                                    {
                                        if (solicitudConcesion.titularesSolConcesion != null)
                                        {
                                            foreach (Persona aPersona in solicitudConcesion.titularesSolConcesion)
                                            {
                                                if (titularesPrimerCentroUsado.ContainsKey(aPersona.rutPersona))
                                                {
                                                    cumpleValidacionTitular = true;
                                                    break;
                                                }
                                            }
                                        }


                                        if (!cumpleValidacionTitular)
                                        {
                                            error = new ErroresRelocalizacion();
                                            error.tramiteRel = tramiteRelocalizacion;
                                            error.detSector = aDetalleSector;
                                            error.tipoError = new ParametroGenerico(rbTipo.MANEJO_DE_TITULARES);
                                            error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                                            error.invalidante = false;
                                            error.centro = new ParametroGenerico(aDetalleSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro);
                                            errores.Add(error);
                                        }
                                    }

                                    */
                                     
                                    //VALIDAR QUE LOS ORIGENES ESTEN DENTRO DE LA MISMA REGION
                                    if (regionPrimerOrigen > 0 && solicitudConcesion.region != null)
                                    {
                                        if (regionPrimerOrigen != solicitudConcesion.region.id)
                                        {
                                            error = new ErroresRelocalizacion();
                                            error.tramiteRel = tramiteRelocalizacion;
                                            error.detSector = aDetalleSector;
                                            error.tipoError = new ParametroGenerico(rbTipo.DIFERENCIA_REGION);
                                            error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                                            error.invalidante = false;
                                            error.centro = new ParametroGenerico(aDetalleSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro);
                                            errores.Add(error);

                                        }
                                    }


                                }
                            }
                        }
                    }
                }


                /*

                //HECTAREAS PARTE 2
                if (hectareasOrigenes != null)
                {
                    foreach (DictionaryEntry entry in hectareasOrigenes)
                    {
                        solicitudConcesion = concesionService.ObtieneConcesionExistente(Convert.ToInt32(entry.Key), rbTipo.TIPO_TRAMITE_SOLICITUD);

                        if (solicitudConcesion != null)
                        {
                            if (solicitudConcesion.superficieCalculadaCultivo != Convert.ToSingle(entry.Value))
                            {

                                error = new ErroresRelocalizacion();
                                error.tramiteRel = tramiteRelocalizacion;
                                error.tipoError = new ParametroGenerico(rbTipo.DIFERENCIA_DE_HECTAREAS);
                                error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                                error.invalidante = false;
                                error.centro = new ParametroGenerico(Convert.ToInt32(entry.Key));
                                errores.Add(error);
                            }
                        }
                    }
                }

                 */

                return errores;


            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }

        }


        //OBTIENE LA LISTA DE TRAMITES DE RELOCALIZACION PARA EL ADMINISTRAR
        public List<TramiteRelocalizacion> ListarTramiteRelocalizacionAdmin_Tramite(TramiteRelocalizacion tramiteRelocalizacion, int cantidadPaginacion)
        {
            try
            {
                return tramiteRelocalizacionDA.ListarTramiteRelocalizacionAdmin_Tramite(tramiteRelocalizacion, cantidadPaginacion, rbTipo.RELOCALIZACION_LEY);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        //OBTIENE LA LISTA DE TRAMITES APROBADOS DE RELOCALIZACION PARA EL ADMINISTRAR
        public List<TramiteRelocalizacion> ListarTramiteRelocalizacionAdmin_Aprobada(TramiteRelocalizacion tramiteRelocalizacion, int cantidadPaginacion)
        {
            try
            {
                return tramiteRelocalizacionDA.ListarTramiteRelocalizacionAdmin_Aprueba(tramiteRelocalizacion, cantidadPaginacion, rbTipo.RELOCALIZACION_LEY);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }



        //OBTIENE LA LISTA DE TRAMITES RECHAZADOS DE RELOCALIZACION PARA EL ADMINISTRAR
        public List<TramiteRelocalizacion> ListarTramiteRelocalizacionAdmin_Rechazada(TramiteRelocalizacion tramiteRelocalizacion, int cantidadPaginacion)
        {
            try
            {
                return tramiteRelocalizacionDA.ListarTramiteRelocalizacionAdmin_Rechazo(tramiteRelocalizacion, cantidadPaginacion, rbTipo.RELOCALIZACION_LEY);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

     


        //DEVUELVE EL SECTOR ASOCIADO A UNA DETERMINADO ID DE SOLICITUD DE CONCESION
        public DetalleSector obtenerDetalleSector_Solicitud(int idSolConcesion) {

            try
            {
                return detalleSectorDA.obtenerDetalleSector_Solicitud(idSolConcesion);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        
        }

        //DEVUELVE UN SECTOR
        public DetalleSector obtenerDetalleSector(int idTramiteRel, int idDetalleSector)
        {

            try
            {
                return detalleSectorDA.obtenerDetalleSector(idTramiteRel, idDetalleSector);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }

        }


        //Indica si un tramite de relocalización tiene todos sus detalles con resolución ssp aprobados
        public bool Tramite_resolucion_SSpCompleto(int idTramiteRel) {

            try
            {
                return tramiteRelocalizacionDA.Tramite_resolucion_SSpCompleto(idTramiteRel);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }


        //Indica si un tramite de relocalización tiene todos sus detalles con resolución ssp aprobados
        public bool aplicaPertTramiteExistente(string numPert)
        {

            try
            {
                return tramiteRelocalizacionDA.aplicaPertTramiteExistente(numPert);
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
