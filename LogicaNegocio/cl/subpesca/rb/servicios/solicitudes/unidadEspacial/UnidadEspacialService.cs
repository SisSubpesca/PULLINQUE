using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Transactions;
using Datos.Entidades;
using Datos.Contantes;
using Datos.Entidades.Relocalizacion;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;
using LogicaNegocio.cl.subpesca.rb.servicios.concesiones;

namespace LogicaNegocio.cl.subpesca.rb.servicios.solicitudes.unidadEspacial
{
    public class UnidadEspacialService
    {
        Logger logger = new Logger();
        UnidadEspacialDA unidadEspacialDA = new UnidadEspacialDA();
        SolicitudDA solicitudDA = new SolicitudDA();
        RelocalizacionService relocalizacionService = new RelocalizacionService();
        ConcesionService concesionService = new ConcesionService();
        ProyectoTecnicoDA proyectoTecnicoDA = new ProyectoTecnicoDA();
        CoordenadaGeograficaDA coordenadaGeograficaDA = new CoordenadaGeograficaDA();
        SolicitudConcesionService solicitudConcesionService = new SolicitudConcesionService();

        public bool guardaUnidadEspacial(Datos.Entidades.UnidadEspacial unidadEspacial, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    //Guarda Unidad Espacial - Crea Concesion -
                    if (!unidadEspacialDA.GuardarUnidadEspacial(unidadEspacial, idUsuario))
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


        //PARA AVANZAR AL ULTIMO ESTADO DEL FLUJO ES NECESARIO HACER UN BYPASS HACIENDO CREER AL FLUJO QUE YA ES CONCESION
        public bool AvanzarEstadoFinalFlujos(UnidadEspacial unidadEspacial) {

            try
            {
                
                // Se setea traspasoOk verdadero a la solicitud  */
                if (!solicitudConcesionService.ActualizaSolicitud_Traspaso(unidadEspacial.idSolicitud, true))
                {
                    return false;
                }
                if (!solicitudDA.GuardarEstadoSolicitud(unidadEspacial.idSolicitud))
                {
                    return false;
                }
                // Se setea traspasoOk falso a la solicitud  */
                if (!solicitudConcesionService.ActualizaSolicitud_Traspaso(unidadEspacial.idSolicitud, false))
                {
                    return false;
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


        //GENERA LA UNIDAD ESPACIAL COMO CONCESION
        public bool CreaUnidadEspacialConcesion(UnidadEspacial unidadEspacial, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    if (!unidadEspacialDA.GuardarUnidadEspacial(unidadEspacial, idUsuario))
                    {
                        return false;
                    }


                    if (!AvanzarEstadoFinalFlujos(unidadEspacial)) 
                    {
                        return false;
                    }


                    //SE REALIZA LA CLONACION DE LA SOLICITUD PARA PASARLA A CONCESION
                    //LA SOLICITUD QUEDA CON TRASPASO OK = 0 Y CON UN TRAMITE DE HISTORICO
                    //LA CONCESION QUEDA CON TRASPADO OK = 1 Y SU TRAMITE NO ES MODIFICADO
                    if (!unidadEspacialDA.GuardarSolicitud_Clon(unidadEspacial.idSolicitud, idUsuario))
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



        public bool guardaUnidadEspacialModificacion(Datos.Entidades.UnidadEspacial unidadEspacial, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    /* Se debe guardar una copia de Unidad Espacial propia de modificación */
                    if (!unidadEspacialDA.GuardarUnidadEspacialModificacion(unidadEspacial, idUsuario))
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


        //MODFICIA UNA CONCESION
        private bool modificarConcesionAcuicultura(int idSolicitudConcesion, string codigoCentro, int idUsuario)
        {

             try{

                 SolicitudConcesion solicitudOriginal = concesionService.ObtieneConcesionExistente(codigoCentro, rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);

                //COPIAR ANTECEDENTES DEL SECTOR A LA CONCESION
                if (!proyectoTecnicoDA.GuardarProyectoTecnico(solicitudOriginal.idSolConcesion,idSolicitudConcesion))
                {
                    return false;
                }
                //COPIAR ANTECEDENTES DEL SECTOR A LA CONCESION
                if (!coordenadaGeograficaDA.GuardarCoordenadaGeograficaTramiteMod(solicitudOriginal.idSolConcesion,idSolicitudConcesion))
                {
                    return false;
                }

                //COPIAR UNIDAD ESPACIAL
                if (!unidadEspacialDA.ActualizarUnidadEspacial_UE_Mod(idSolicitudConcesion, solicitudOriginal.idSolConcesion, idUsuario)) 
                {
                    return false;
                }

                /* Se debe actualizar el OkTraspaso de la solicitud original */
                if (!solicitudDA.ActualizaSolicitud_Traspaso(solicitudOriginal.idSolConcesion, true))
                {
                    return false;
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


        public bool guardaUnidadEspacialRelocalizacion(Datos.Entidades.UnidadEspacial unidadEspacial, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    /* Se debe guardar una copia de Unidad Espacial propia de modificación */
                    if (!unidadEspacialDA.GuardarUnidadEspacialRel(unidadEspacial, idUsuario))
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
         * RELOCALIZA UNA CONCESION
        */
        public bool relocalizarConcesion(Datos.Entidades.UnidadEspacial unidadEspacial, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    /* Se actualiza el bit indicando que ya se apreta el boton de relocalizar */
                    if (!solicitudConcesionService.ActualizaSolicitud_Traspaso(unidadEspacial.idSolicitud, true))
                    {
                        return false;
                    }

                    //RECALCULAR ESTADO DE LA SOLICITUD DE MODIFICACION (HARA QUE AVANCE AL ESTADO FINAL)
                    if (!solicitudDA.GuardarEstadoSolicitud(unidadEspacial.idSolicitud))
                    {
                        return false;
                    }

                    /* Se debe guardar una copia de Unidad Espacial propia de modificación */
                    if (!unidadEspacialDA.GuardarUnidadEspacialRel(unidadEspacial, idUsuario))
                    {
                        return false;
                    }

                    /* Se hace efectiva la modificación a la concesión acuicultura */
                    if (!relocalizarUnidadEspacial(unidadEspacial.idSolicitud, idUsuario))
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
         * RELOCALIZA UNA CONCESION DE ACUICULTURA
         * 
         * SECTOR CERO, REEMPLAZAR LA INFORMACION EN EL PRIMERO ORIGEN
         * FUSION, REEMPLAZAR LA INFORMACION EN EL DESTINO
         * CREACION, CREAR UNA NUEVA SOLICITUD DE CONCESION Y COPIAR TODA LA INFORMACION QUE SE INGRESO EN SECTOR 
         */
        public bool relocalizarUnidadEspacial(int idSolicitudNueva, int idUsuario)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {
         
               try{

                   //SECTOR
                   DetalleSector aDetalleSector = relocalizacionService.obtenerDetalleSector_Solicitud(idSolicitudNueva);


                   //PULLINQUE 4. SI ES UNA RELOCALIZACION CREA, PERO EL CODIGO DE CENTRO YA EXISTE, ENTONCES SE MODIFICA ESE CENTRO (SE TRATA COMO SI FUERA UN SECTOR 0)
                   if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA || aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA_RESA)
                   {

                       if (aDetalleSector.tramiteEnCurso != null && aDetalleSector.tramiteEnCurso.unidadEspacial != null && aDetalleSector.tramiteEnCurso.unidadEspacial.centrosDeCultivo != null && !aDetalleSector.tramiteEnCurso.unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
                       {
                            //VERIFICAR SI SE INGRESO UN CODIGO DE CENTRO DE UNA UNIDAD ESPACIAL QUE EXISTE
                            SolicitudConcesion solicitudOriginal = concesionService.ObtieneConcesionExistente(aDetalleSector.tramiteEnCurso.unidadEspacial.centrosDeCultivo.codigoCentro.Trim(), rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);
                            if (solicitudOriginal != null && solicitudOriginal.idSolConcesion > 0)  //ES UNA UNIDAD ESPACIAL
                            {
                                aDetalleSector.origenes = new List<OrigenSector>();
                                OrigenSector origenSector = new OrigenSector();
                                origenSector.concesionOrigen = aDetalleSector.tramiteEnCurso;
                                aDetalleSector.origenes.Add(origenSector);

                                aDetalleSector.tipoRelocalizacion = new ParametroGenerico(rbTipo.RELOCALIZACION_SECTOR_CERO);

                            }
                       }
                   }


                   //CREACION, CREAR UNA NUEVA SOLICITUD DE CONCESION Y COPIAR TODA LA INFORMACION
                   if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA || aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA_RESA)
                   {
                       int idSolCreada = solicitudDA.GuardarNuevaConcesion(idSolicitudNueva, idUsuario);

                      if (idSolCreada < 1) {
                          return false;
                      }

                      /* Se setea OkTraspaso a verdadero para la solicitud creada */
                      if (!solicitudConcesionService.ActualizaSolicitud_Traspaso(idSolCreada, true))
                      {
                          return false;
                      }

                   }

               
                   //SECTOR CERO, REEMPLAZAR LA INFORMACION EN EL PRIMERO ORIGEN
                   if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO || aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO_RESA)
                   {

                       SolicitudConcesion solicitudOriginal = concesionService.ObtieneConcesionExistente(aDetalleSector.origenes[0].concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro, rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);


                       //COPIAR ANTECEDENTES DEL SECTOR DE LA CONCESION DEL SECTOR CERO
                       if (!proyectoTecnicoDA.GuardarProyectoTecnico(solicitudOriginal.idSolConcesion, idSolicitudNueva))
                       {
                           return false;
                       }
                       //COPIAR ANTECEDENTES DEL SECTOR DE LA CONCESION DEL SECTOR CERO
                       if (!coordenadaGeograficaDA.GuardarCoordenadaGeograficaTramiteMod(solicitudOriginal.idSolConcesion, idSolicitudNueva))
                       {
                           return false;
                       }

                       //Copiar Unidad Espacial
                       if (!unidadEspacialDA.ActualizarUnidadEspacial_UE_Rel(idSolicitudNueva, solicitudOriginal.idSolConcesion, idUsuario))
                       {
                           return false;
                       }

                       /* Se setea traspasoOk verdadero a la solicitud original */
                       if (!solicitudConcesionService.ActualizaSolicitud_Traspaso(solicitudOriginal.idSolConcesion, true))
                       {
                           return false;
                       }

                   }

               
                   //FUSION, REEMPLAZAR LA INFORMACION EN EL DESTINO
                   if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA || aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA_RESA)
                   {
                       SolicitudConcesion solicitudOriginal = concesionService.ObtieneConcesionExistente(aDetalleSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro, rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);


                       //COPIAR ANTECEDENTES DEL SECTOR DE LA CONCESION DEL SECTOR CERO
                       if (!proyectoTecnicoDA.GuardarProyectoTecnico(solicitudOriginal.idSolConcesion, idSolicitudNueva))
                       {
                           return false;
                       }
                       //COPIAR ANTECEDENTES DEL SECTOR DE LA CONCESION DEL SECTOR CERO
                       if (!coordenadaGeograficaDA.GuardarCoordenadaGeograficaTramiteMod(solicitudOriginal.idSolConcesion, idSolicitudNueva))
                       {
                           return false;
                       }

                       //Copiar Unidad Espacial
                       if (!unidadEspacialDA.ActualizarUnidadEspacial_UE_Rel(idSolicitudNueva, solicitudOriginal.idSolConcesion, idUsuario))
                       {
                           return false;
                       }

                       /* Se setea traspasoOk verdadero a la solicitud original */
                       if (!solicitudConcesionService.ActualizaSolicitud_Traspaso(solicitudOriginal.idSolConcesion, true))
                       {
                           return false;
                       }
                   
                   }

                   transactionScope.Complete();
                   return true;


               }catch(Exception ex){
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
               }

            }
        }


        //OBTIENE UNA UNIDAD ESPACIAL
        public UnidadEspacial ObtieneUnidadEspacial(int idSolicitud, int idUnidEspacial) {

            try
            {
                return unidadEspacialDA.ObtieneUnidadEspacialMod(idSolicitud, idUnidEspacial);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }

        }


        public bool modificaConcesionOriginal(UnidadEspacial unidadEspacial, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    /* Se confirma la modificación de la concesión */
                    if (!solicitudDA.ActualizaSolicitud_Traspaso(unidadEspacial.idSolicitud,true))
                    {
                        return false;
                    }

                    //RECALCULAR ESTADO DE LA SOLICITUD DE MODIFICACION (HARA QUE AVANCE AL ESTADO FINAL)
                    if (!solicitudDA.GuardarEstadoSolicitud(unidadEspacial.idSolicitud))
                    {
                        return false;
                    }

                    /* Se debe guardar una copia de Unidad Espacial propia de modificación */
                    if (!unidadEspacialDA.GuardarUnidadEspacialModificacion(unidadEspacial, idUsuario))
                    {
                        return false;
                    }

                    /* Se hace efectiva la modificación a la concesión acuicultura */
                    if (!modificarConcesionAcuicultura(unidadEspacial.idSolicitud, unidadEspacial.centrosDeCultivo.codigoCentro, idUsuario))
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



        //OBTIENE UNA UNIDAD ESPACIAL DE RELOCALIZACION
        public UnidadEspacial ObtieneUnidadEspacialRel(int idSolicitud, int idUnidEspacialRel)
        {
            try
            {
                return unidadEspacialDA.ObtieneUnidadEspacialRel(idSolicitud, idUnidEspacialRel);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }



        //GUARDA UNA UNIDAD ESPACIAL DE CENTRO DE ACOPIO
        public bool guardaUnidadEspacialAcopio(UnidadEspacial unidadEspacial, int idUsuario)
        {
            try
            {
                return unidadEspacialDA.GuardarUnidadEspacial(unidadEspacial, idUsuario);
                
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }

        //GENERA LA UNIDAD ESPACIAL ACOPIO COMO CONCESION
        public bool CreaUnidadEspacialAcopio(UnidadEspacial unidadEspacial, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    if (!unidadEspacialDA.GuardarUnidadEspacial(unidadEspacial, idUsuario))
                    {
                        return false;
                    }


                    if (!AvanzarEstadoFinalFlujos(unidadEspacial))
                    {
                        return false;
                    }

                    /* Se setea traspasoOk verdadero a la solicitud  */
                    //if (!solicitudConcesionService.ActualizaSolicitud_Traspaso(unidadEspacial.idSolicitud, true))
                    //{
                        //return false;
                    //}


                    //SE REALIZA LA CLONACION DE LA SOLICITUD PARA PASARLA A UN CENTRO DE ACOPIO
                    //LA SOLICITUD QUEDA CON TRASPASO OK = 0 Y CON UN TRAMITE DE HISTORICO
                    //LA CONCESION QUEDA CON TRASPADO OK = 1 Y SU TRAMITE NO ES MODIFICADO
                    if (!unidadEspacialDA.GuardarSolicitud_Clon(unidadEspacial.idSolicitud, idUsuario))
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



        //GUARDA UNA UNIDAD ESPACIAL DE AMERB
        public bool guardaUnidadEspacialAmerb(UnidadEspacial unidadEspacial, int idUsuario)
        {
            try
            {
                return unidadEspacialDA.GuardarUnidadEspacial(unidadEspacial, idUsuario);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }


        //GUARDA UNA UNIDAD ESPACIAL DE Experimentales en Amerb
        public bool guardaUnidadEspacialExperimentalesAmerb(UnidadEspacial unidadEspacial, int idUsuario)
        {
            try
            {
                return unidadEspacialDA.GuardarUnidadEspacial(unidadEspacial, idUsuario);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }

        //GUARDA UNA UNIDAD ESPACIAL DE Experimentales Concesión
        public bool guardaUnidadEspacialExperimentalesConcesion(UnidadEspacial unidadEspacial, int idUsuario)
        {
            try
            {
                return unidadEspacialDA.GuardarUnidadEspacial(unidadEspacial, idUsuario);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }

        //GUARDA UNA UNIDAD ESPACIAL DE ACUICULTURA EN ECMPO
        public bool guardaUnidadEspacialECMPO(UnidadEspacial unidadEspacial, int idUsuario)
        {
            try
            {
                return unidadEspacialDA.GuardarUnidadEspacial(unidadEspacial, idUsuario);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }

        //GENERA LA UNIDAD ESPACIAL AMERB COMO CONCESION
        public bool CreaUnidadEspacialAmerb(UnidadEspacial unidadEspacial, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    if (!unidadEspacialDA.GuardarUnidadEspacial(unidadEspacial, idUsuario))
                    {
                        return false;
                    }

                    if (!AvanzarEstadoFinalFlujos(unidadEspacial))
                    {
                        return false;
                    }

                    /* Se setea traspasoOk verdadero a la solicitud  */
                    //if (!solicitudConcesionService.ActualizaSolicitud_Traspaso(unidadEspacial.idSolicitud, true))
                    //{
                    //    return false;
                    //}


                    //SE REALIZA LA CLONACION DE LA SOLICITUD PARA PASARLA A CONCESION
                    //LA SOLICITUD QUEDA CON TRASPASO OK = 0 Y CON UN TRAMITE DE HISTORICO
                    //LA CONCESION QUEDA CON TRASPADO OK = 1 Y SU TRAMITE NO ES MODIFICADO
                    if (!unidadEspacialDA.GuardarSolicitud_Clon(unidadEspacial.idSolicitud, idUsuario))
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



        //GUARDA UNA UNIDAD ESPACIAL DE CENTRO DE COLECTOR
        public bool guardaUnidadEspacialColector(UnidadEspacial unidadEspacial, int idUsuario)
        {
            try
            {
                return unidadEspacialDA.GuardarUnidadEspacial(unidadEspacial, idUsuario);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }

        //GENERA LA UNIDAD ESPACIAL COLECTOR COMO CONCESION
        public bool CreaUnidadEspacialColector(UnidadEspacial unidadEspacial, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    if (!unidadEspacialDA.GuardarUnidadEspacial(unidadEspacial, idUsuario))
                    {
                        return false;
                    }

                    if (!AvanzarEstadoFinalFlujos(unidadEspacial))
                    {
                        return false;
                    }

                    /* Se setea traspasoOk verdadero a la solicitud  */
                    //if (!solicitudConcesionService.ActualizaSolicitud_Traspaso(unidadEspacial.idSolicitud, true))
                    //{
                    //    return false;
                    //}


                    //SE REALIZA LA CLONACION DE LA SOLICITUD PARA PASARLA A CONCESION
                    //LA SOLICITUD QUEDA CON TRASPASO OK = 0 Y CON UN TRAMITE DE HISTORICO
                    //LA CONCESION QUEDA CON TRASPADO OK = 1 Y SU TRAMITE NO ES MODIFICADO
                    if (!unidadEspacialDA.GuardarSolicitud_Clon(unidadEspacial.idSolicitud, idUsuario))
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



        //GUARDA UNA UNIDAD ESPACIAL DE CENTRO DE FAENAMIENTO
        public bool guardaUnidadEspacialFaenamiento(UnidadEspacial unidadEspacial, int idUsuario)
        {
            try
            {
                return unidadEspacialDA.GuardarUnidadEspacial(unidadEspacial, idUsuario);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }

        //GENERA LA UNIDAD ESPACIAL FAENAMIENTO COMO CONCESION
        public bool CreaUnidadEspacialFaenamiento(UnidadEspacial unidadEspacial, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    if (!unidadEspacialDA.GuardarUnidadEspacial(unidadEspacial, idUsuario))
                    {
                        return false;
                    }

                    if (!AvanzarEstadoFinalFlujos(unidadEspacial))
                    {
                        return false;
                    }

                    /* Se setea traspasoOk verdadero a la solicitud  */
                    //if (!solicitudConcesionService.ActualizaSolicitud_Traspaso(unidadEspacial.idSolicitud, true))
                    //{
                    //    return false;
                    //}



                    //SE REALIZA LA CLONACION DE LA SOLICITUD PARA PASARLA A CONCESION
                    //LA SOLICITUD QUEDA CON TRASPASO OK = 0 Y CON UN TRAMITE DE HISTORICO
                    //LA CONCESION QUEDA CON TRASPADO OK = 1 Y SU TRAMITE NO ES MODIFICADO
                    if (!unidadEspacialDA.GuardarSolicitud_Clon(unidadEspacial.idSolicitud, idUsuario))
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
         * VERIFICA SI EL CODIGO SIEP ESTA SIENDO OCUPADA EN OTRA UNIDAD ESPACIAL
          */
        public bool ValidaCentroUnidadesEspaciales(string codigoCentro, int idUnidadEspacial)
        {
            try
            {
                return unidadEspacialDA.ValidaCentroUnidadesEspaciales(codigoCentro, idUnidadEspacial);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }



        public bool CreaUnidadEspacialExperimentalesAmerb(UnidadEspacial unidadEspacial, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    if (!unidadEspacialDA.GuardarUnidadEspacial(unidadEspacial, idUsuario))
                    {
                        return false;
                    }

                    if (!AvanzarEstadoFinalFlujos(unidadEspacial))
                    {
                        return false;
                    }

                    /* Se setea traspasoOk verdadero a la solicitud  */
                    //if (!solicitudConcesionService.ActualizaSolicitud_Traspaso(unidadEspacial.idSolicitud, true))
                    //{
                        //return false;
                    //}


                    //SE REALIZA LA CLONACION DE LA SOLICITUD PARA PASARLA A CONCESION
                    //LA SOLICITUD QUEDA CON TRASPASO OK = 0 Y CON UN TRAMITE DE HISTORICO
                    //LA CONCESION QUEDA CON TRASPADO OK = 1 Y SU TRAMITE NO ES MODIFICADO
                    if (!unidadEspacialDA.GuardarSolicitud_Clon(unidadEspacial.idSolicitud, idUsuario))
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


        public bool CreaUnidadEspacialECMPO(UnidadEspacial unidadEspacial, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    if (!unidadEspacialDA.GuardarUnidadEspacial(unidadEspacial, idUsuario))
                    {
                        return false;
                    }


                    if (!AvanzarEstadoFinalFlujos(unidadEspacial))
                    {
                        return false;
                    }

                    /* Se setea traspasoOk verdadero a la solicitud  */
                    //if (!solicitudConcesionService.ActualizaSolicitud_Traspaso(unidadEspacial.idSolicitud, true))
                    //{
                        //return false;
                    //}

                    //SE REALIZA LA CLONACION DE LA SOLICITUD PARA PASARLA A CONCESION
                    //LA SOLICITUD QUEDA CON TRASPASO OK = 0 Y CON UN TRAMITE DE HISTORICO
                    //LA CONCESION QUEDA CON TRASPADO OK = 1 Y SU TRAMITE NO ES MODIFICADO
                    if (!unidadEspacialDA.GuardarSolicitud_Clon(unidadEspacial.idSolicitud, idUsuario))
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

        public bool CreaUnidadEspacialExperimentalesConcesion(UnidadEspacial unidadEspacial, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    if (!unidadEspacialDA.GuardarUnidadEspacial(unidadEspacial, idUsuario))
                    {
                        return false;
                    }

                    if (!AvanzarEstadoFinalFlujos(unidadEspacial))
                    {
                        return false;
                    }

                    /* Se setea traspasoOk verdadero a la solicitud  */
                    //if (!solicitudConcesionService.ActualizaSolicitud_Traspaso(unidadEspacial.idSolicitud, true))
                    //{
                        //return false;
                    //}

                    //SE REALIZA LA CLONACION DE LA SOLICITUD PARA PASARLA A CONCESION
                    //LA SOLICITUD QUEDA CON TRASPASO OK = 0 Y CON UN TRAMITE DE HISTORICO
                    //LA CONCESION QUEDA CON TRASPADO OK = 1 Y SU TRAMITE NO ES MODIFICADO
                    if (!unidadEspacialDA.GuardarSolicitud_Clon(unidadEspacial.idSolicitud, idUsuario))
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

        public bool modificaAmerbOriginal(UnidadEspacial unidadEspacial, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    /* Se confirma la modificación de la concesión */
                    if (!solicitudDA.ActualizaSolicitud_Traspaso(unidadEspacial.idSolicitud, true))
                    {
                        return false;
                    }

                    //RECALCULAR ESTADO DE LA SOLICITUD DE MODIFICACION (HARA QUE AVANCE AL ESTADO FINAL)
                    if (!solicitudDA.GuardarEstadoSolicitud(unidadEspacial.idSolicitud))
                    {
                        return false;
                    }

                    /* Se debe guardar una copia de Unidad Espacial propia de modificación */
                    if (!unidadEspacialDA.GuardarUnidadEspacialModificacion(unidadEspacial, idUsuario))
                    {
                        return false;
                    }

                    /* Se hace efectiva la modificación a la amerb */
                    if (!modificarAmerbAcuicultura(unidadEspacial.idSolicitud, unidadEspacial.centrosDeCultivo.codigoCentro, idUsuario))
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

        private bool modificarAmerbAcuicultura(int idSolicitudConcesion, string codigoCentro, int idUsuario)
        {
            try
            {

                SolicitudConcesion solicitudOriginal = concesionService.ObtieneUnidadEspacialExistente(codigoCentro, rbTipo.ACUICULTURA_EN_AMERB);

                //COPIAR ANTECEDENTES DEL SECTOR A LA CONCESION
                if (!proyectoTecnicoDA.GuardarProyectoTecnico(solicitudOriginal.idSolConcesion, idSolicitudConcesion))
                {
                    return false;
                }
                //COPIAR ANTECEDENTES DEL SECTOR A LA CONCESION
                if (!coordenadaGeograficaDA.GuardarCoordenadaGeograficaTramiteMod(solicitudOriginal.idSolConcesion, idSolicitudConcesion))
                {
                    return false;
                }

                //COPIAR UNIDAD ESPACIAL
                if (!unidadEspacialDA.ActualizarUnidadEspacial_UE_Mod(idSolicitudConcesion, solicitudOriginal.idSolConcesion, idUsuario))
                {
                    return false;
                }

                /* Se debe actualizar el OkTraspaso de la solicitud original */
                if (!solicitudDA.ActualizaSolicitud_Traspaso(solicitudOriginal.idSolConcesion, true))
                {
                    return false;
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

        public bool modificaCentroAcopioOriginal(UnidadEspacial unidadEspacial, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    /* Se confirma la modificación de la concesión */
                    if (!solicitudDA.ActualizaSolicitud_Traspaso(unidadEspacial.idSolicitud, true))
                    {
                        return false;
                    }

                    //RECALCULAR ESTADO DE LA SOLICITUD DE MODIFICACION (HARA QUE AVANCE AL ESTADO FINAL)
                    if (!solicitudDA.GuardarEstadoSolicitud(unidadEspacial.idSolicitud))
                    {
                        return false;
                    }

                    /* Se debe guardar una copia de Unidad Espacial propia de modificación */
                    if (!unidadEspacialDA.GuardarUnidadEspacialModificacion(unidadEspacial, idUsuario))
                    {
                        return false;
                    }

                    /* Se hace efectiva la modificación al centro de acopio */
                    if (!modificarCentroAcopioAcuicultura(unidadEspacial.idSolicitud, unidadEspacial.centrosDeCultivo.codigoCentro, idUsuario))
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

        public bool modificaCentroFaenamientoOriginal(UnidadEspacial unidadEspacial, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    /* Se confirma la modificación de la concesión */
                    if (!solicitudDA.ActualizaSolicitud_Traspaso(unidadEspacial.idSolicitud, true))
                    {
                        return false;
                    }

                    //RECALCULAR ESTADO DE LA SOLICITUD DE MODIFICACION (HARA QUE AVANCE AL ESTADO FINAL)
                    if (!solicitudDA.GuardarEstadoSolicitud(unidadEspacial.idSolicitud))
                    {
                        return false;
                    }

                    /* Se debe guardar una copia de Unidad Espacial propia de modificación */
                    if (!unidadEspacialDA.GuardarUnidadEspacialModificacion(unidadEspacial, idUsuario))
                    {
                        return false;
                    }

                    /* Se hace efectiva la modificación al centro de faenamiento */
                    if (!modificarCentroFaenamientoAcuicultura(unidadEspacial.idSolicitud, unidadEspacial.centrosDeCultivo.codigoCentro, idUsuario))
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

        private bool modificarCentroAcopioAcuicultura(int idSolicitudConcesion, string codigoCentro, int idUsuario)
        {
            try
            {

                SolicitudConcesion solicitudOriginal = concesionService.ObtieneUnidadEspacialExistente(codigoCentro, rbTipo.CENTRO_DE_ACOPIO);

                //COPIAR ANTECEDENTES DEL SECTOR A LA CONCESION
                if (!proyectoTecnicoDA.GuardarProyectoTecnico(solicitudOriginal.idSolConcesion, idSolicitudConcesion))
                {
                    return false;
                }
                //COPIAR ANTECEDENTES DEL SECTOR A LA CONCESION
                if (!coordenadaGeograficaDA.GuardarCoordenadaGeograficaTramiteMod(solicitudOriginal.idSolConcesion, idSolicitudConcesion))
                {
                    return false;
                }

                //COPIAR UNIDAD ESPACIAL
                if (!unidadEspacialDA.ActualizarUnidadEspacial_UE_Mod(idSolicitudConcesion, solicitudOriginal.idSolConcesion, idUsuario))
                {
                    return false;
                }

                /* Se debe actualizar el OkTraspaso de la solicitud original */
                if (!solicitudDA.ActualizaSolicitud_Traspaso(solicitudOriginal.idSolConcesion, true))
                {
                    return false;
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

        private bool modificarCentroFaenamientoAcuicultura(int idSolicitudConcesion, string codigoCentro, int idUsuario)
        {
            try
            {

                SolicitudConcesion solicitudOriginal = concesionService.ObtieneUnidadEspacialExistente(codigoCentro, rbTipo.CENTRO_DE_FAENAMIENTO);

                //COPIAR ANTECEDENTES DEL SECTOR A LA CONCESION
                if (!proyectoTecnicoDA.GuardarProyectoTecnico(solicitudOriginal.idSolConcesion, idSolicitudConcesion))
                {
                    return false;
                }
                //COPIAR ANTECEDENTES DEL SECTOR A LA CONCESION
                if (!coordenadaGeograficaDA.GuardarCoordenadaGeograficaTramiteMod(solicitudOriginal.idSolConcesion, idSolicitudConcesion))
                {
                    return false;
                }
                //COPIAR UNIDAD ESPACIAL
                if (!unidadEspacialDA.ActualizarUnidadEspacial_UE_Mod(idSolicitudConcesion, solicitudOriginal.idSolConcesion, idUsuario))
                {
                    return false;
                }
                
                /* Se debe actualizar el OkTraspaso de la solicitud original */
                if (!solicitudDA.ActualizaSolicitud_Traspaso(solicitudOriginal.idSolConcesion, true))
                {
                    return false;
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

        public bool modificaECMPOOriginal(UnidadEspacial unidadEspacial, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    /* Se confirma la modificación de la concesión */
                    if (!solicitudDA.ActualizaSolicitud_Traspaso(unidadEspacial.idSolicitud, true))
                    {
                        return false;
                    }

                    //RECALCULAR ESTADO DE LA SOLICITUD DE MODIFICACION (HARA QUE AVANCE AL ESTADO FINAL)
                    if (!solicitudDA.GuardarEstadoSolicitud(unidadEspacial.idSolicitud))
                    {
                        return false;
                    }

                    /* Se debe guardar una copia de Unidad Espacial propia de modificación */
                    if (!unidadEspacialDA.GuardarUnidadEspacialModificacion(unidadEspacial, idUsuario))
                    {
                        return false;
                    }

                    /* Se hace efectiva la modificación al ECMPO */
                    if (!modificarECMPO(unidadEspacial.idSolicitud, unidadEspacial.centrosDeCultivo.codigoCentro, idUsuario))
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

        private bool modificarECMPO(int idSolicitudConcesion, string codigoCentro, int idUsuario)
        {
            try
            {

                SolicitudConcesion solicitudOriginal = concesionService.ObtieneUnidadEspacialExistente(codigoCentro, rbTipo.ECMPO);

                //COPIAR ANTECEDENTES DEL SECTOR A LA CONCESION
                if (!proyectoTecnicoDA.GuardarProyectoTecnico(solicitudOriginal.idSolConcesion, idSolicitudConcesion))
                {
                    return false;
                }
                //COPIAR ANTECEDENTES DEL SECTOR A LA CONCESION
                if (!coordenadaGeograficaDA.GuardarCoordenadaGeograficaTramiteMod(solicitudOriginal.idSolConcesion, idSolicitudConcesion))
                {
                    return false;
                }

                //COPIAR UNIDAD ESPACIAL
                if (!unidadEspacialDA.ActualizarUnidadEspacial_UE_Mod(idSolicitudConcesion, solicitudOriginal.idSolConcesion, idUsuario))
                {
                    return false;
                }

                /* Se debe actualizar el OkTraspaso de la solicitud original */
                if (!solicitudDA.ActualizaSolicitud_Traspaso(solicitudOriginal.idSolConcesion, true))
                {
                    return false;
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
    }
}
