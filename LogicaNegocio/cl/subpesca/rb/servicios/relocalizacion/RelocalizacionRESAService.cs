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
using Datos.Utilidades;
namespace LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion
{



    public class RelocalizacionRESAService
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
        
        
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();
        InformeRel_RESA_DA informeRel_RESA_DA = new InformeRel_RESA_DA();


        //GUARDA UN INFORME DE  RELOCALIZACION RESA
        public bool GuardarInformeRESA(InformeRel_RESA informe)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {


                    //GUARDAR EL DOCUMENTO ADJUNTO
                    if (informe.archivoBinSC != null && informe.archivoBinSC.archivo != null)
                    {
                        if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitud(informe.archivoBinSC))
                        {
                            return false;
                        }
                    }



                    //GUARDAR EL INFORME
                    if (!informeRel_RESA_DA.GuardarInformeRel_Resa(informe))
                    {
                        return false;
                    }


                    if (informe.asocInformeSolicitud != null)
                    {

                        foreach (AsocInformeSolicitud asocInformeSolicitud in informe.asocInformeSolicitud)
                            {
                                if (asocInformeSolicitud.accion == accion.INGRESAR || asocInformeSolicitud.accion == accion.LISTADO || asocInformeSolicitud.accion == accion.MODIFICAR)
                                {
                                    if (!informeRel_RESA_DA.GuardarAsocInformeSolicitud(informe.idInformeRel, asocInformeSolicitud.idSolConcesion, rbEstadosGenerales.VIGENTE))
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



        //LISTA LOS INFORMES 
        public List<InformeRel_RESA> ListarInformesRESA(InformeRel_RESA informeFiltro)
        {

            try {

                return informeRel_RESA_DA.ListarInformeRes_Resa(0, informeFiltro.numero, informeFiltro.codigoCentroFiltro);
            
            
            }catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }


        }


        //ELIMINA UN INFORME Y TODAS SUS DEPENDENCIAS
        public bool EliminarInformeRESA(int idInformeRel)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!informeRel_RESA_DA.EliminarAsocInformeSolicitud(idInformeRel, 0))
                    {
                        return false;
                    }

                    if (!informeRel_RESA_DA.EliminarInformeRel_Resa(idInformeRel)) {
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




        //OBTIENE UN INFORME CON TODAS SUS CENTROS ASOCIADOS
        public InformeRel_RESA ObtenerInformeRESA(InformeRel_RESA informeFiltro)
        {

            try
            {

                InformeRel_RESA informe =   informeRel_RESA_DA.ObtenerInformeRes_Resa(informeFiltro.idInformeRel);

                if (informe != null && informe.idInformeRel > 0)
                {

                    if (informe.archivoBinSC != null && informe.archivoBinSC.idArchivo > 0)
                    {
                        informe.archivoBinSC = archivoBinarioSolicitudDA.ObtenerArchivoBinarioSolicitud(informe.archivoBinSC.idArchivo);
                    }


                    informe.asocInformeSolicitud = informeRel_RESA_DA.ListarAsocInformeSolicitud(informe.idInformeRel);
                }

                return informe;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
            
        }



        //ACTUALIZA UN INFORME DE RELOCALIZACION RESA
        public bool ActualizarInformeRESA(InformeRel_RESA informe)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    //BORRADO DE ASOCIACIONES    
                    if (!informeRel_RESA_DA.EliminarAsocInformeSolicitud(informe.idInformeRel, 0))
                    {
                        return false;
                    }



                    //GUARDAR EL NUEVO DOCUMENTO ADJUNTO DE TENERLO
                    if (informe.archivoBinSC != null && informe.archivoBinSC.archivo != null)
                    {
                        if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitud(informe.archivoBinSC))
                        {
                            return false;
                        }
                    }

                    //ACTUALIZAR LA RESOLUCION
                    if (!informeRel_RESA_DA.GuardarInformeRel_Resa(informe))
                    {
                        return false;
                    }


                    //ASOCIACIONES
                    if (informe.asocInformeSolicitud != null && informe.asocInformeSolicitud.Count() > 0)
                    {

                        foreach (AsocInformeSolicitud asocInformeSolicitud in informe.asocInformeSolicitud)
                        {

                            if (asocInformeSolicitud.accion == accion.INGRESAR || asocInformeSolicitud.accion == accion.LISTADO || asocInformeSolicitud.accion == accion.MODIFICAR)
                            {

                                asocInformeSolicitud.idInformeRel = informe.idInformeRel;
                                if (!informeRel_RESA_DA.GuardarAsocInformeSolicitud(informe.idInformeRel, asocInformeSolicitud.idSolConcesion, asocInformeSolicitud.estadoAsoc.id))
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


        //ACTUALIZA EL ESTADO DE UNA ASOCIACION DE UN CENTRO A UN INFORME
        public bool ActualizarAsocInformeSolicitudRESA(int idInformeRel, int idSolConcesion, int idEstadoAsoc)
          {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    if (!informeRel_RESA_DA.GuardarAsocInformeSolicitud(idInformeRel, idSolConcesion, idEstadoAsoc))
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


        //MUESTRA LA INFORMACION DE LOS INFORMES RESA ASOCIADOS A UN CODIGO DE CENTRO
        public string mostrarInformesRESA(string codigoSiep)
        {

            try {

                String informesToString ="";
                List<InformeRel_RESA> informes =  informeRel_RESA_DA.ListarInformeResVigente_Resa(0, "", Convert.ToInt32(codigoSiep));


                if (informes != null) {

                    foreach (InformeRel_RESA informe in informes) { 
                    

                        if(informesToString.Equals("")){

                            if(!String.IsNullOrEmpty(informe.tipoDocumento.descripcion)){
                                informesToString = informesToString + informe.tipoDocumento.descripcion;
                            }
                            if(!String.IsNullOrEmpty(informe.numero)){
                                informesToString = informesToString +   " Nº "+  informe.numero + "."; 
                            }
                            if(!String.IsNullOrEmpty(informe.fecha.ToString())){
                                informesToString = informesToString + " Fecha: " + FechaUtils.formatearFechaSinHora(informe.fecha) + "."; 
                            }
                           
                        }else{

                            informesToString = informesToString + ", ";

                            if (!String.IsNullOrEmpty(informe.tipoDocumento.descripcion))
                            {
                                informesToString = informesToString + informe.tipoDocumento.descripcion;
                            }
                            if (!String.IsNullOrEmpty(informe.numero))
                            {
                                informesToString = informesToString + " Nº " + informe.numero + "."; 
                            }
                            if (!String.IsNullOrEmpty(informe.fecha.ToString()))
                            {
                                informesToString = informesToString + " Fecha: " + FechaUtils.formatearFechaSinHora(informe.fecha) + "."; 
                            }
                        }
                    }
                }

                return informesToString;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return "";
            }
        }







        
        //OBTIENE LISTADO DE PREFERENCIAS PARA RELOCALIZACION
        public List<ParametroGenerico> ListarPreferenciaRelocalizacionRESA()
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
        public bool GenerarTramiteDeRelocalizacionRESA(TramiteRelocalizacion tramiteRelocalizacion)
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
        public TramiteRelocalizacion ObtenerTramiteRelocalizacionRESA(int idTramiteRel)
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
        public TramiteRelocalizacion ObtenerTramiteRelocalizacionCompletoRESA(int idTramiteRel)
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
        public List<DetalleSector> ObtenerSectoresNoRedefiniblesRESA(int idTramiteRel)
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
        public List<DetalleSector> ObtenerSectoresRedefiniblesRESA(int idTramiteRel)
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
        public List<ErroresRelocalizacion> ListarErroresRelocalizacionRESA(int idTramite)
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
        public List<ErroresRelocalizacion> ListarAlertasRelocalizacionRESA(int idTramite)
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
        public bool GuardarTramiteRelocalizacionRESA(TramiteRelocalizacion tramiteRelocalizacion, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    //SI ES UN NUEVO TRAMITE, ENTONCES SE GUARDA PRIMERO EL TRAMITE

                    if (tramiteRelocalizacion.idTramiteRel == 0) {
                        if (!this.GenerarTramiteDeRelocalizacionRESA(tramiteRelocalizacion)) {
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
                    List<ErroresRelocalizacion> errores = this.verificarErroresRelocalizacionRESA(tramiteRelocalizacion);


                    //INGRESAR LOS ERRORES
                    if (errores != null)
                    {
                        if (!this.GuardarErroresRelocalizacionRESA(errores))
                        {
                            return false;
                        }
                    }


                    //INTENTAR SETEAR LAS SOLICITUDES ASOCIDADAS A CADA SECTOR DEL TRAMITE
                    if (!this.setearSolicitudesDelTramiteRESA(tramiteRelocalizacion, idUsuario))
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
        public bool RedefinirTramiteRelocalizacionRESA(TramiteRelocalizacion tramiteRelocalizacion, int idUsuario)
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
                    List<ErroresRelocalizacion> errores = this.verificarErroresRelocalizacionRESA(tramiteRelocalizacion);


                    //INGRESAR LOS ERRORES
                    if (errores != null)
                    {
                        if (!this.GuardarErroresRelocalizacionRESA(errores))
                        {
                            return false;
                        }
                    }


                    //INTENTAR SETEAR LAS SOLICITUDES ASOCIDADAS A CADA SECTOR DEL TRAMITE
                    if (!this.setearSolicitudesDelTramiteRESA(tramiteRelocalizacion, idUsuario))
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
        public bool setearSolicitudesDelTramiteRESA(TramiteRelocalizacion tramiteRelocalizacion, int idUsuario)
        { 
        
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {

                    List<ErroresRelocalizacion> erroresDB = this.ListarErroresRelocalizacionRESA(tramiteRelocalizacion.idTramiteRel);

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
                                solicitudInicial.tipoUnidadEspacial = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION_RESA);

                                if (!solicitudConcesionService.guardarSolicitudConcesionRelocalizacionRESA(solicitudInicial))
                                {
                                    return false;
                                }

                                if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO_RESA && aDetalleSector.origenes != null && aDetalleSector.origenes.Count > 0)
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

                                if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA_RESA && aDetalleSector.concesionDestino != null && aDetalleSector.concesionDestino.unidadEspacial.idSolicitud > 0)
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
        public bool recalcularErroresRelocalizacionRESA(TramiteRelocalizacion tramiteRelocalizacion, int idUsuario)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {

                    //OBTENER LOS ERRORES
                    List<ErroresRelocalizacion> errores = this.verificarErroresRelocalizacionRESA(tramiteRelocalizacion);


                    //BORRAR POSIBLES ERRORES EXISTENTES (SI AUN APLICAN, SE VOLVERAN A INGRESAR, LOS QUE ESTEN COMO "IGNORADO" NO SE BORRARAN)
                    if (!this.erroresRelocalizacionDA.EliminarErroresRelocalizacion(tramiteRelocalizacion.idTramiteRel))
                    {
                        return false;
                    }


                    //INGRESAR LOS ERRORES (SI YA ESTABA INGRESADO, NO SE VOLVERA A INGRESAR)
                    if (errores != null)
                    {
                        if (!this.GuardarErroresRelocalizacionRESA(errores))
                        {
                            return false;
                        }
                    }


                    if (!this.setearSolicitudesDelTramiteRESA(tramiteRelocalizacion, idUsuario))
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
        public bool recalcularAlertasRelocalizacionRESA(TramiteRelocalizacion tramiteRelocalizacion)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {

                    //OBTENER LAS ALERTAS
                    List<ErroresRelocalizacion> alertas = this.verificarAlertasRelocalizacionRESA(tramiteRelocalizacion);


                    //BORRAR POSIBLES ALERTAS EXISTENTES (SI AUN APLICAN, SE VOLVERAN A INGRESAR)
                    if (!this.erroresRelocalizacionDA.EliminarAlertasRelocalizacion(tramiteRelocalizacion.idTramiteRel))
                    {
                        return false;
                    }


                    //INGRESAR LOS ERRORES (SI YA ESTABA INGRESADO, NO SE VOLVERA A INGRESAR)
                    if (alertas != null)
                    {
                        if (!this.GuardarErroresRelocalizacionRESA(alertas))
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
        public bool GuardarErroresRelocalizacionRESA(List<ErroresRelocalizacion> errores)
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
        public bool GuardarAlertasRelocalizacionRESA(List<ErroresRelocalizacion> alertas)
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
        public List<ErroresRelocalizacion> verificarErroresRelocalizacionRESA(TramiteRelocalizacion tramiteRelocalizacion)
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
                Hashtable hashOrigenes = new Hashtable();
                Hashtable hashCentros = new Hashtable();
                
                int cantidadSectores = 0;

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

                            cantidadSectores++;

                            //SI ES RELOCALIZACION CREA Y DEJO UN SECTOR CERO ENTONCES DEBE ESTAR MARCADA LA OPCION "UBICACION EN COMUNA CHAITEN O FIORDO AYSEN"
                            if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA_RESA && !hashCentros.ContainsKey(aDetalleSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro))
                            {
                                hashCentros.Add(aDetalleSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro, aDetalleSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro);
                            }


                            foreach (OrigenSector aOrigenSector in aDetalleSector.origenes)
                            {
                                if (aOrigenSector.accion == accion.INGRESAR || aOrigenSector.accion == accion.LISTADO || aOrigenSector.accion == accion.MODIFICAR)
                                {
                                    
                                    solicitudConcesion = concesionService.ObtieneConcesionExistente(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro, rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);


                                    if (!hashOrigenes.ContainsKey(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro)) {
                                        hashOrigenes.Add(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro, aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro);
                                        hashCentros.Add(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro, aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro);
                                    }


                                    //SI ES RELOCALIZACION CREA Y DEJO UN SECTOR CERO ENTONCES DEBE ESTAR MARCADA LA OPCION "UBICACION EN COMUNA CHAITEN O FIORDO AYSEN"
                                    if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA_RESA && hashSectoresCero.ContainsKey(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro))
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
                            if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA_RESA)
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


                //ERROR: CANTIDAD DE SECTORES (MÁXIMO 3)
                if (cantidadSectores > 3)
                {

                    error = new ErroresRelocalizacion();
                    error.tramiteRel = tramiteRelocalizacion;
                    error.tipoError = new ParametroGenerico(rbTipo.CANTIDAD_SECTORES_RELOCALIZACION);
                    error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                    error.invalidante = true;
                    errores.Add(error);

                }


                //ERROR: CENTROS DENTRO DE LA RELOCALIZACION SIN INFORME
                if (hashCentros != null)
                {

                    String informes = "";

                    foreach (DictionaryEntry entry in hashCentros)
                    {
                        informes = this.mostrarInformesRESA(Convert.ToString(entry.Key));

                        if (informes == null || informes.Trim().Equals(""))
                        {
                            error = new ErroresRelocalizacion();
                            error.tramiteRel = tramiteRelocalizacion;
                            error.tipoError = new ParametroGenerico(rbTipo.ORIGEN_SIN_INFORME);
                            error.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                            error.invalidante = true;
                            error.centro = new ParametroGenerico(Convert.ToInt32(entry.Key));
                            errores.Add(error);
                            
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
        public List<ErroresRelocalizacion> verificarAlertasRelocalizacionRESA(TramiteRelocalizacion tramiteRelocalizacion)
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


                                    //EXISTENCIA DEL CENTRO
                                    if (solicitudConcesion == null)
                                    {


                                    }
                                    else
                                    {


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
                            if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA_RESA)
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
        public List<TramiteRelocalizacion> ListarTramiteRelocalizacionAdmin_TramiteRESA(TramiteRelocalizacion tramiteRelocalizacion, int cantidadPaginacion)
        {
            try
            {
                return tramiteRelocalizacionDA.ListarTramiteRelocalizacionAdmin_Tramite(tramiteRelocalizacion, cantidadPaginacion, rbTipo.RELOCALIZACION_RESA);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        //OBTIENE LA LISTA DE TRAMITES APROBADOS DE RELOCALIZACION PARA EL ADMINISTRAR
        public List<TramiteRelocalizacion> ListarTramiteRelocalizacionAdmin_AprobadaRESA(TramiteRelocalizacion tramiteRelocalizacion, int cantidadPaginacion)
        {
            try
            {
                return tramiteRelocalizacionDA.ListarTramiteRelocalizacionAdmin_Aprueba(tramiteRelocalizacion, cantidadPaginacion, rbTipo.RELOCALIZACION_RESA);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }



        //OBTIENE LA LISTA DE TRAMITES RECHAZADOS DE RELOCALIZACION PARA EL ADMINISTRAR
        public List<TramiteRelocalizacion> ListarTramiteRelocalizacionAdmin_RechazadaRESA(TramiteRelocalizacion tramiteRelocalizacion, int cantidadPaginacion)
        {
            try
            {
                return tramiteRelocalizacionDA.ListarTramiteRelocalizacionAdmin_Rechazo(tramiteRelocalizacion, cantidadPaginacion, rbTipo.RELOCALIZACION_RESA);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }



        //DEVUELVE EL SECTOR ASOCIADO A UNA DETERMINADO ID DE SOLICITUD DE CONCESION
        public DetalleSector obtenerDetalleSector_SolicitudRESA(int idSolConcesion) {

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
        public DetalleSector obtenerDetalleSectorRESA(int idTramiteRel, int idDetalleSector)
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
        public bool Tramite_resolucion_SSpCompletoRESA(int idTramiteRel) {

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



   
        public bool aplicaPertTramiteExistenteRESA(string numPert)
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
