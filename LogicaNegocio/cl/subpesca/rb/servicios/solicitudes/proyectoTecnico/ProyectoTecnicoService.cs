using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.errores;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.common;

namespace LogicaNegocio.cl.subpesca.rb.servicios.solicitudes
{
    public class ProyectoTecnicoService
    {
        Logger logger = new Logger();
        ProyectoTecnicoDA proyTecnicoDA = new ProyectoTecnicoDA();
        EspecieProyTecnicoDA especiePTDA = new EspecieProyTecnicoDA();
        EstructuraTecnicaPT_DA estructDA = new EstructuraTecnicaPT_DA();
        ProgrProduccionDA progrProdDA = new ProgrProduccionDA();
        SolicitudDA solicitudDA = new SolicitudDA();
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();

        public bool guardarProyectoTecnico(ProyectoTecnico proyTecnico, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    //Guardar Proyecto Tecnico
                    if (!proyTecnicoDA.GuardarProyectoTecnico(proyTecnico, idUsuario))
                    {
                        return false;
                    }

                    //Tipo Alimento
                    //if (proyTecnico.tipoAlimento != null && proyTecnico.tipoAlimento.Count>0)
                    //{
                    //    foreach (TipoAlimentoProyecto tipoAlimentoAux in proyTecnico.tipoAlimento)
                    //    {
                    //        tipoAlimentoAux.idProyectoTecnico = proyTecnico.IdProyectoTecnico;
                    //        if (!proyTecnicoDA.GuardarTipoAlimentoPorProyecto(tipoAlimentoAux))
                    //        {
                    //            return false;
                                
                    //        }
                    //    }
                    //}

                    //Especies Proy. Tecnico
                    if (proyTecnico.especieAutProyTecnico != null && proyTecnico.especieAutProyTecnico.Count > 0)
                    {
                        foreach (EspecieAutorizadaPT especieAutPTAux in proyTecnico.especieAutProyTecnico)
                        {
                            if(especieAutPTAux.accion == accion.INGRESAR){
                                
                                especieAutPTAux.idProyectoTecnico = proyTecnico.IdProyectoTecnico;
                                if (!especiePTDA.GuardarEspecieProyTecnico(especieAutPTAux))
                                {
                                    return false;
                                }

                                /* Etapa de Cultivo Multiple por Especie o Grupo */
                                foreach (EtapaCultivo etapaCultivo in especieAutPTAux.etapaCultivoList)
                                {
                                    etapaCultivo.especie = new Especies();
                                    etapaCultivo.especie.id_especie = especieAutPTAux.idEspeciePT;
                                    if (!especiePTDA.GuardarEspecieEtapaProyTecnico(especieAutPTAux.idEspeciePT, etapaCultivo.id_etapaDesarrollo))
                                    {
                                        return false;
                                    }

                                }
                            }
                        }
                    }

                    //verificar si debe ingresarse grupos autorizados automaticamente
                    if (!especiePTDA.GuardarGruposAutorizados(proyTecnico.IdProyectoTecnico))
                    {
                        return false;
                    }

                    ////Grupo Proy. Tecnico
                    //if (proyTecnico.grupoAutProyTecnico != null && proyTecnico.grupoAutProyTecnico.Count > 0)
                    //{
                    //    foreach (GrupoPT grupoAutPTAux in proyTecnico.grupoAutProyTecnico)
                    //    {
                    //        if (grupoAutPTAux.accion == accion.INGRESAR)
                    //        {
                    //            grupoAutPTAux.idProyectoTecnico = proyTecnico.IdProyectoTecnico;
                    //            if (!especiePTDA.GuardarGrupoProyTecnico(grupoAutPTAux))
                    //            {
                    //                return false;
                    //            }
                    //        }
                    //    }
                    //}

                    //Estructura tecnica a instalar cada año (General)
                    if (proyTecnico.estructTecnicaProyTecnico != null && proyTecnico.estructTecnicaProyTecnico.Count>0)
                    {
                        foreach (EstructuraTecnicaPT estructTecnPTAux in proyTecnico.estructTecnicaProyTecnico)
                        {
                            if (estructTecnPTAux.accion == accion.INGRESAR)
                            {
                                estructTecnPTAux.idProyTec = proyTecnico.IdProyectoTecnico;
                                if (!estructDA.GuardarEstructuraTecnicaPT(estructTecnPTAux, 0, 0))
                                {
                                    return false;
                                }
                                
                                if (estructTecnPTAux.anios != null && estructTecnPTAux.anios.Count > 0)
                                {
                                    foreach (ValorParametroAnioPT valorParamAux in estructTecnPTAux.anios)
                                    {
                                        valorParamAux.idclaveParametro = estructTecnPTAux.idEstructPT;
                                        if (!estructDA.GuardarValorEstructuraPorAnio(valorParamAux))
                                        {
                                            return false;
                                        }
                                    }
                                }    
                            }
                        }
                    }
                    
                    //Estructura tecnica a instalar cada año (Colectores)
                    if (proyTecnico.estructTecnicaColector != null && proyTecnico.estructTecnicaColector.numColectores > 0 && proyTecnico.estructTecnicaColector.numLineas > 0)
                    {
                        EstructuraTecnicaPT estructTecnPTAux = proyTecnico.estructTecnicaColector;

                        if (estructTecnPTAux.accion == accion.INGRESAR)
                        {
                            estructTecnPTAux.idProyTec = proyTecnico.IdProyectoTecnico;
                            if (!estructDA.GuardarEstructuraTecnicaPT(estructTecnPTAux, 0, 0))
                            {
                                return false;
                            }
                        }

                    }

                    //Metodo Cultivo Algas (si aplica)
                    //if (proyTecnico.metodoCultivoAlgas != null && proyTecnico.metodoCultivoAlgas.Count > 0)
                    //{
                    //    foreach (TipoAlimentoProyecto metAlgasAux in proyTecnico.metodoCultivoAlgas)
                    //    {
                    //        metAlgasAux.idProyectoTecnico = proyTecnico.IdProyectoTecnico;
                    //        if (!proyTecnicoDA.GuardarMetodoCultivoAlgasProy(metAlgasAux.idProyectoTecnico, metAlgasAux.tipoAlimento.id, metAlgasAux.detalle))
                    //        {
                    //            return false;
                    //        }
                    //    }
                    //}


                    //Tipo de Fondo (si aplica)
                    //if (proyTecnico.tipoFondo != null && proyTecnico.tipoFondo.Count > 0)
                    //{
                    //    foreach (TipoAlimentoProyecto tFondoAux in proyTecnico.tipoFondo)
                    //    {
                    //        tFondoAux.idProyectoTecnico = proyTecnico.IdProyectoTecnico;
                    //        if (!proyTecnicoDA.GuardarTipoFondoProy(tFondoAux.idProyectoTecnico, tFondoAux.tipoAlimento.id, tFondoAux.detalle))
                    //        {
                    //            return false;
                    //        }
                    //    }
                    //}

                    //Programa de produccion
                    if(proyTecnico.progrProduccionProyTecnico!=null && proyTecnico.progrProduccionProyTecnico.Count>0){
                        foreach (ProgrProduccionPT progrProdAux in proyTecnico.progrProduccionProyTecnico)
                        {
                            if (progrProdAux.accion == accion.INGRESAR)
                            {
                                progrProdAux.idProyectoTecnico = proyTecnico.IdProyectoTecnico;
                                if (!progrProdDA.GuardarProgrProduccion(progrProdAux,0 ,0))
                                {
                                    return false;
                                }
                                if (progrProdAux.aniosProgrProd != null && progrProdAux.aniosProgrProd.Count > 0)
                                {
                                    foreach (ValorParametroAnioPT valorParamAux in progrProdAux.aniosProgrProd)
                                    {
                                        valorParamAux.idclaveParametro = progrProdAux.idProgrProduccion;
                                        if (!progrProdDA.GuardarValorProgrProduccion(valorParamAux))
                                        {
                                            return false;
                                        }
                                    }
                                }
                            }

                        }
                    }

                    //RECALCULAR EL ESTADO DE LA SOLICITUD
                    if (!solicitudDA.TramiteRecalculaEstados(proyTecnico.idSolicitud))
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


        public bool modificarProyectoTecnico(ProyectoTecnico proyTecnico, int idUsuario, int idSolicitud)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    //Modificar Proyecto Tecnico
                    if (!proyTecnicoDA.GuardarProyectoTecnico(proyTecnico, idUsuario))
                    {
                        return false;
                    }


                    ////Tipo Alimento: BORRAR E INSERTAR
                    //if (!proyTecnicoDA.EliminarTipoAlimentoPorProyecto(proyTecnico.IdProyectoTecnico))
                    //{
                    //    return false;
                    //}
                    //if (proyTecnico.tipoAlimento != null && proyTecnico.tipoAlimento.Count > 0)
                    //{
                    //    foreach (TipoAlimentoProyecto tipoAlimentoAux in proyTecnico.tipoAlimento)
                    //    {
                    //       tipoAlimentoAux.idProyectoTecnico = proyTecnico.IdProyectoTecnico;
                    //       if (!proyTecnicoDA.GuardarTipoAlimentoPorProyecto(tipoAlimentoAux))
                    //       {
                    //          return false;

                    //       }
                    //    }
                    //}

                    //Especies o Grupo Proy. Tecnico
                    if (proyTecnico.especieAutProyTecnico != null && proyTecnico.especieAutProyTecnico.Count > 0)
                    {
                        foreach (EspecieAutorizadaPT especieAutPTAux in proyTecnico.especieAutProyTecnico)
                        {
                            especieAutPTAux.idProyectoTecnico = proyTecnico.IdProyectoTecnico;
                            
                            if (especieAutPTAux.accion == accion.ELIMINAR)
                            {

                                /* Eliminar Etapa de Cultivo Multiple por Especie o Grupo */
                                foreach (EtapaCultivo etapaCultivo in especieAutPTAux.etapaCultivoList)
                                {
                                    if (!especiePTDA.EliminarEspecieEtapaProyTecnico(especieAutPTAux.idEspeciePT, etapaCultivo.id_etapaDesarrollo))
                                    {
                                        return false;
                                    }
                                }


                                if (!especiePTDA.EliminarEspecieProyTecnico(especieAutPTAux))
                                {
                                    return false;
                                }
                                
                            }


                            if (especieAutPTAux.accion == accion.INGRESAR)
                            {
                                if (!especiePTDA.GuardarEspecieProyTecnico(especieAutPTAux))
                                {
                                    return false;
                                }

                                /* Etapa de Cultivo Multiple por Especie o Grupo */
                                foreach (EtapaCultivo etapaCultivo in especieAutPTAux.etapaCultivoList)
                                {
                                    etapaCultivo.especie = new Especies();
                                    etapaCultivo.especie.id_especie = especieAutPTAux.idEspeciePT;
                                    if (!especiePTDA.GuardarEspecieEtapaProyTecnico(especieAutPTAux.idEspeciePT, etapaCultivo.id_etapaDesarrollo))
                                    {
                                        return false;
                                    }

                                }
                            }

                        }
                    }


                    //verificar si debe ingresarse grupos autorizados automaticamente
                    if (!especiePTDA.GuardarGruposAutorizados(proyTecnico.IdProyectoTecnico))
                    {
                        return false;
                    }

                    ////Grupo Proy. Tecnico
                    //if (proyTecnico.grupoAutProyTecnico != null && proyTecnico.grupoAutProyTecnico.Count > 0)
                    //{
                    //    foreach (GrupoPT grupoAutPTAux in proyTecnico.grupoAutProyTecnico)
                    //    {
                    //        grupoAutPTAux.idProyectoTecnico = proyTecnico.IdProyectoTecnico;
                    //        if ((grupoAutPTAux.accion == accion.ELIMINAR) && (!especiePTDA.EliminarGrupoProyTecnico(grupoAutPTAux)))
                    //        {
                    //            return false;
                    //        }
                    //        if ((grupoAutPTAux.accion == accion.INGRESAR) && (!especiePTDA.GuardarGrupoProyTecnico(grupoAutPTAux)))
                    //        {
                    //            return false;
                    //        }
                    //    }
                    //}

                    //Estructura tecnica a instalar cada año (General)
                    if (proyTecnico.estructTecnicaProyTecnico != null && proyTecnico.estructTecnicaProyTecnico.Count > 0)
                    {
                        foreach (EstructuraTecnicaPT estructTecnPTAux in proyTecnico.estructTecnicaProyTecnico)
                        {
                            estructTecnPTAux.idProyTec = proyTecnico.IdProyectoTecnico;
                            if ((estructTecnPTAux.accion == accion.ELIMINAR) && (!this.eliminarEstructuraTecnica(estructTecnPTAux, idUsuario, idSolicitud)))
                            {
                                return false;
                            }
                            if ((estructTecnPTAux.accion == accion.MODIFICAR) && (!this.modificarEstructuraTecnica(estructTecnPTAux, idUsuario, idSolicitud)))
                            {
                                return false;
                            }
                            if (estructTecnPTAux.accion == accion.INGRESAR)
                            {
                                if (!estructDA.GuardarEstructuraTecnicaPT(estructTecnPTAux, idUsuario, idSolicitud))
                                {
                                    return false;
                                }else
                                {
                                    if (estructTecnPTAux.anios != null && estructTecnPTAux.anios.Count > 0)
                                    {
                                        foreach (ValorParametroAnioPT valorParamAux in estructTecnPTAux.anios)
                                        {
                                            valorParamAux.idclaveParametro = estructTecnPTAux.idEstructPT;
                                            if (!estructDA.GuardarValorEstructuraPorAnio(valorParamAux))
                                            {
                                                return false;
                                            }
                                        }
                                    }
                                }
                                
                            }
                            
                        }
                    }

                    //Estructura tecnica a instalar cada año Directo al Sustrato (General)
                    if (proyTecnico.estructTecnicaProyTecnicoDirectoSustrato != null && proyTecnico.estructTecnicaProyTecnicoDirectoSustrato.Count > 0)
                    {
                        foreach (EstructuraTecnicaPT estructTecnPTAux in proyTecnico.estructTecnicaProyTecnicoDirectoSustrato)
                        {
                            estructTecnPTAux.idProyTec = proyTecnico.IdProyectoTecnico;
                            if ((estructTecnPTAux.accion == accion.ELIMINAR) && (!this.eliminarEstructuraTecnica(estructTecnPTAux, idUsuario, idSolicitud)))
                            {
                                return false;
                            }
                            if ((estructTecnPTAux.accion == accion.MODIFICAR) && (!this.modificarEstructuraTecnica(estructTecnPTAux, idUsuario, idSolicitud)))
                            {
                                return false;
                            }
                            if (estructTecnPTAux.accion == accion.INGRESAR)
                            {
                                if (!estructDA.GuardarEstructuraTecnicaPT(estructTecnPTAux, idUsuario, idSolicitud))
                                {
                                    return false;
                                }
                                else
                                {
                                    if (estructTecnPTAux.anios != null && estructTecnPTAux.anios.Count > 0)
                                    {
                                        foreach (ValorParametroAnioPT valorParamAux in estructTecnPTAux.anios)
                                        {
                                            valorParamAux.idclaveParametro = estructTecnPTAux.idEstructPT;
                                            if (!estructDA.GuardarValorEstructuraPorAnio(valorParamAux))
                                            {
                                                return false;
                                            }
                                        }
                                    }
                                }

                            }

                        }
                    }

                    //Estructura Técnica a Instalar Colectores (si es que aplica)
                    if (proyTecnico.estructTecnicaColector != null)
                    {
                        proyTecnico.estructTecnicaColector.idProyTec = proyTecnico.IdProyectoTecnico;

                        if (!estructDA.GuardarEstructuraTecnicaPT(proyTecnico.estructTecnicaColector, idUsuario, idSolicitud))
                        {
                            return false;
                        }
                    }

                    
                    ////Metodo Cultivo Algas: BORRAR E INSERTAR
                    //if (!proyTecnicoDA.EliminarMetodoCultivoAlgasProy(proyTecnico.IdProyectoTecnico))
                    //{
                    //    return false;
                    //}

                    //if (proyTecnico.metodoCultivoAlgas != null && proyTecnico.metodoCultivoAlgas.Count>0)
                    //{
                    //    foreach (TipoAlimentoProyecto metAlgasAux in proyTecnico.metodoCultivoAlgas)
                    //    {
                    //      metAlgasAux.idProyectoTecnico = proyTecnico.IdProyectoTecnico;
                    //      if (!proyTecnicoDA.GuardarMetodoCultivoAlgasProy(metAlgasAux.idProyectoTecnico, metAlgasAux.tipoAlimento.id, metAlgasAux.detalle))
                    //      {
                    //         return false;
                    //      }
                    //    }    
                    //}
                    
                    //Tipo de Fondo BORRAR E INSERTAR
                    //if (!proyTecnicoDA.EliminarTipoFondoProy(proyTecnico.IdProyectoTecnico))
                    //{
                    //    return false;
                    //}
                    //if (proyTecnico.tipoFondo != null && proyTecnico.tipoFondo.Count > 0)
                    //{
                    //    foreach (TipoAlimentoProyecto tFondoAux in proyTecnico.tipoFondo)
                    //    {
                    //      tFondoAux.idProyectoTecnico = proyTecnico.IdProyectoTecnico;
                    //      if (!proyTecnicoDA.GuardarTipoFondoProy(tFondoAux.idProyectoTecnico, tFondoAux.tipoAlimento.id, tFondoAux.detalle))
                    //      {
                    //         return false;
                    //      }
                    //    }
                    //}

                    //Programa de produccion
                    if (proyTecnico.progrProduccionProyTecnico != null && proyTecnico.progrProduccionProyTecnico.Count > 0)
                    {
                        foreach (ProgrProduccionPT progrProdAux in proyTecnico.progrProduccionProyTecnico)
                        {
                            progrProdAux.idProyectoTecnico = proyTecnico.IdProyectoTecnico;
                            if ((progrProdAux.accion == accion.ELIMINAR) && (!this.eliminarProgramaProduccion(progrProdAux, idUsuario, idSolicitud)))
                            {
                                return false;
                            }
                            if ((progrProdAux.accion == accion.MODIFICAR) && (!this.modificarProgramaProduccion(progrProdAux, idUsuario, idSolicitud)))
                            {
                                return false;
                            }
                            if (progrProdAux.accion == accion.INGRESAR)
                            {
                                if (!progrProdDA.GuardarProgrProduccion(progrProdAux, idUsuario, idSolicitud))
                                {
                                    return false;
                                }
                                else {
                                    if (progrProdAux.aniosProgrProd != null && progrProdAux.aniosProgrProd.Count > 0)
                                    {
                                        foreach (ValorParametroAnioPT valorParamAux in progrProdAux.aniosProgrProd)
                                        {
                                            valorParamAux.idclaveParametro = progrProdAux.idProgrProduccion;
                                            if (!progrProdDA.GuardarValorProgrProduccion(valorParamAux))
                                             {
                                                return false;
                                             }
                                        }
                                    }
                                }
                            }

                        }
                    }

                    //RECALCULAR EL ESTADO DE LA SOLICITUD
                    if (!solicitudDA.TramiteRecalculaEstados(proyTecnico.idSolicitud))
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


        public bool guardarArchivoBinarioPT(ProyectoTecnico proyTecnico, ArchivosAdjPT archivosAdjPT)
        {

            if (archivosAdjPT.accion == accion.INGRESAR)
            {
                //Almacenamiento de Archivo Binario
                if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitudEspecial(archivosAdjPT.archivoBinario))
                {
                    return false;
                }

                //Asociacion de Archivo Binario con PT
                archivosAdjPT.idPT = proyTecnico.IdProyectoTecnico;
                if (!proyTecnicoDA.GuardarArchivoProyTecnico(archivosAdjPT))
                {
                    return false;
                }
            }

            return true;

        }



        private bool modificarArchivoBinarioPT(ArchivosAdjPT archivosAdjPT, int idUsuario, int idSolicitud)
        {
            //Almacenamiento de Archivo Binario
            if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitudEspecial(archivosAdjPT.archivoBinario))
            {
                return false;
            }

            if (!proyTecnicoDA.GuardarArchivoProyTecnico(archivosAdjPT))
            {
                return false;
            }

            return true;
        }

        public bool eliminarArchivoBinarioPT(int idPT, int idArchivoBinario)
        {
            if (!proyTecnicoDA.EliminarArchivoPT(idPT, idArchivoBinario))
            {
                return false;
            }
            return true;
        }


        public bool eliminarEstructuraTecnica(EstructuraTecnicaPT estructTecnica, int idUsuario, int idSolConcesion)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    //Eliminar Lista de Valor estructura por anio

                    EstructuraTecnicaPT_DA estructTecnicaDA = new EstructuraTecnicaPT_DA();

                    if (!estructDA.EliminarValorEstructuraPorAnio(estructTecnica.idEstructPT, idUsuario, idSolConcesion))
                    {
                        return false;
                    }

                    if (!estructDA.EliminarEstructuraProyectoTecnico(estructTecnica.idEstructPT, idUsuario))
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

        public bool eliminarEstructuraTecnicaColector(EstructuraTecnicaPT estructTecnica, int idUsuario, int idSolConcesion)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    if (!estructDA.EliminarEstructuraProyectoTecnico(estructTecnica.idEstructPT, idUsuario))
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


        public bool modificarEstructuraTecnica(EstructuraTecnicaPT estructTecnica, int idUsuario, int idSolicitud)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    EstructuraTecnicaPT_DA estructTecnicaDA = new EstructuraTecnicaPT_DA();
                    
                    //Eliminar Lista de Valor estructura por anio asociado a la estructura tecnica
                    if (!estructDA.EliminarValorEstructuraPorAnio(estructTecnica.idEstructPT, idUsuario, idSolicitud))
                    {
                      return false;
                    }

                    //Modificar estructura tecnica, insertando los valores de estructura por anio.
                    if (estructTecnica != null && !estructDA.GuardarEstructuraTecnicaPT(estructTecnica, idUsuario, idSolicitud))
                    {
                       return false;
                    }

                    if (estructTecnica.anios != null && estructTecnica.anios.Count > 0)
                    {
                       foreach (ValorParametroAnioPT valorParamAux in estructTecnica.anios)
                       {
                            valorParamAux.idclaveParametro = estructTecnica.idEstructPT;
                            if (!estructDA.GuardarValorEstructuraPorAnio(valorParamAux))
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

        public bool modificarEstructuraTecnicaColector(EstructuraTecnicaPT estructTecnica, int idUsuario, int idSolicitud)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    //Modificar estructura tecnica, insertando los valores de estructura por anio.
                    if (estructTecnica != null && !estructDA.GuardarEstructuraTecnicaPT(estructTecnica, idUsuario, idSolicitud))
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

        public bool eliminarProgramaProduccion(ProgrProduccionPT progrProduccion, int idUsuario, int idSolicitud)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    //Eliminar Lista de Valor produccion por anio

                    ProgrProduccionDA progrProdDA = new ProgrProduccionDA();

                    if (!progrProdDA.EliminarValorProgrProduccion(progrProduccion.idProgrProduccion, idUsuario, idSolicitud))
                    {
                        return false;
                    }

                    if (!progrProdDA.EliminarProgrProduccion(progrProduccion.idProgrProduccion, progrProduccion.idProyectoTecnico, idUsuario))
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


        public bool modificarProgramaProduccion(ProgrProduccionPT progrProduccion, int idUsuario, int idSolicitud)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    //Eliminar Lista de Valor produccion por anio

                    ProgrProduccionDA progrProdDA = new ProgrProduccionDA();

                    if (!progrProdDA.EliminarValorProgrProduccion(progrProduccion.idProgrProduccion, idUsuario, idSolicitud))
                    {
                        return false;
                    }

                    //Modificar estructura tecnica, insertando los valores de estructura por anio.
                    if (progrProduccion != null && !progrProdDA.GuardarProgrProduccion(progrProduccion, idUsuario, idSolicitud))
                    {
                        return false;
                    }
                    if (progrProduccion.aniosProgrProd != null && progrProduccion.aniosProgrProd.Count > 0)
                    {
                        foreach (ValorParametroAnioPT valorParamAux in progrProduccion.aniosProgrProd)
                        {
                            valorParamAux.idclaveParametro = progrProduccion.idProgrProduccion;
                            if (!progrProdDA.GuardarValorProgrProduccion(valorParamAux))
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


        public EspecieAutorizadaPT ObtenerEspecieAutorizadaPT(int idProyTecnico, int idEspProyTecnico)
        {

            try
            {
                return especiePTDA.ObtenerEspecieProyTecnico(idProyTecnico, idEspProyTecnico);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }

        }


        public HashSet<int> ListaEstructuraMedidas(int idEstructuraTecnica, int idTipoForma)
        {

            try
            {
                return estructDA.ListaEstructuraMedidas(idEstructuraTecnica, idTipoForma);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        public List<EstructuraTecnica> ListaEstructuraTecnica(int idEstructuraTecnica)
        {
            try
            {
                return estructDA.ListaEstructuraTecnica(idEstructuraTecnica);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public EstructuraTecnicaPT ObtenerEstructuraProyectoTecnico(int idProyTecnico, int idEspProyTecnico)
        {

            try
            {
                return estructDA.ObtenerEstructuraProyectoTecnico(idProyTecnico, idEspProyTecnico);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }

        }

        public ProyectoTecnico ObtenerProyectoTecnico(int idSolicitudConc, int idProyTecnico)
        {

            try
            {

                ProyectoTecnico proyTec = new ProyectoTecnico();
                proyTec = proyTecnicoDA.ObtieneProyectoTecnico(idSolicitudConc,0);
                if (proyTec!=null && proyTec.IdProyectoTecnico>0)
                {
                    //proyTec.tipoAlimento = proyTecnicoDA.ListarTipoAlimentoPorProyecto(proyTec.IdProyectoTecnico);
                    
                    proyTec.especieAutProyTecnico = new List<EspecieAutorizadaPT>();
                    proyTec.especieAutProyTecnico = especiePTDA.ListarEspecieProyTecnico(proyTec.IdProyectoTecnico,0);

                    proyTec.especieAutProyTecnico = OrdenarEspecieGrupoEtapa(proyTec.especieAutProyTecnico);

                    proyTec.metodoCultivoAlgas = proyTecnicoDA.ListarMetodoCultivoAlgasProy(proyTec.IdProyectoTecnico);

                    //proyTec.grupoAutProyTecnico = new List<GrupoPT>();
                    //proyTec.grupoAutProyTecnico = especiePTDA.ListarGrupoProyTecnico(proyTec.IdProyectoTecnico, 0);
                    
                    proyTec.estructTecnicaProyTecnico = new List<EstructuraTecnicaPT>();
                    proyTec.estructTecnicaProyTecnico = estructDA.ListarEstructuraProyectoTecnico(proyTec.IdProyectoTecnico, 0);
                    
                    proyTec.estructTecnicaColector = estructDA.ObtenerEstructuraPT_Colector(proyTec.IdProyectoTecnico, 0);
                    
                    //proyTec.tipoFondo = proyTecnicoDA.ListarTipoFondoProy(proyTec.IdProyectoTecnico);

                    proyTec.progrProduccionProyTecnico = new List<ProgrProduccionPT>();
                    proyTec.progrProduccionProyTecnico = progrProdDA.ListarProgrProduccion(proyTec.IdProyectoTecnico, 0);

                    proyTec.archivoBinarioList = new List<ArchivosAdjPT>();
                    proyTec.archivoBinarioList = proyTecnicoDA.ListarArchivosAdjuntoPT(proyTec.IdProyectoTecnico, 0);
                }

                return proyTec;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }

        }


        public List<ArchivosAdjPT> ListarArchivosAdjuntoPT(int idProyTecnico)
        {

            try
            {

                return proyTecnicoDA.ListarArchivosAdjuntoPT(idProyTecnico,0);
               
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }

        }


        private List<EspecieAutorizadaPT> OrdenarEspecieGrupoEtapa(List<EspecieAutorizadaPT> List_EspecieAut)
        {
            List<EspecieAutorizadaPT> List_EspecieAutOrdenada = new List<EspecieAutorizadaPT>();
            List<String> especie = new List<string>();
            List<String> grupo = new List<string>();
            int index = 0;

            if (List_EspecieAut != null && List_EspecieAut.Count > 0)
            {
                //Especie
                foreach (EspecieAutorizadaPT especieAutorizadaPT in List_EspecieAut)
                {
                    //Especie
                    if (especieAutorizadaPT.especie != null && especieAutorizadaPT.especie.id > 0 && !especie.Contains(especieAutorizadaPT.especie.id.ToString()))
                    {
                        especieAutorizadaPT.etapaCultivoList = new List<EtapaCultivo>();
                        foreach (EspecieAutorizadaPT etapa in List_EspecieAut)
                        {
                            if (especieAutorizadaPT.especie != null && etapa.especie != null && especieAutorizadaPT.especie.id == etapa.especie.id)
                            {
                                if (etapa.etapaCultivo != null && etapa.etapaCultivo.id > 0)
                                {

                                    EtapaCultivo etapaCultivo = new EtapaCultivo();
                                    etapaCultivo.id_etapaDesarrollo = etapa.etapaCultivo.id;
                                    etapaCultivo.nombreEtapaDesarrollo = etapa.etapaCultivo.descripcion;

                                    especieAutorizadaPT.etapaCultivoList.Add(etapaCultivo);
                                }
                            }
                        }
                        especie.Add(especieAutorizadaPT.especie.id.ToString());
                        especieAutorizadaPT.index = index;
                        index++;
                        List_EspecieAutOrdenada.Add(especieAutorizadaPT);
                    }
                    
                    //Grupo
                    if (especieAutorizadaPT.grupoEspecieAutoriz != null && !grupo.Contains(especieAutorizadaPT.grupoEspecieAutoriz.id.ToString()))
                    {
                        especieAutorizadaPT.etapaCultivoList = new List<EtapaCultivo>();

                        foreach (EspecieAutorizadaPT etapa in List_EspecieAut)
                        {
                            if (especieAutorizadaPT.grupoEspecieAutoriz != null && etapa.grupoEspecieAutoriz != null && especieAutorizadaPT.grupoEspecieAutoriz.id == etapa.grupoEspecieAutoriz.id)
                            {
                                if (etapa.etapaCultivo != null && etapa.etapaCultivo.id > 0)
                                {
                                    EtapaCultivo etapaCultivo = new EtapaCultivo();
                                    etapaCultivo.id_etapaDesarrollo = etapa.etapaCultivo.id;
                                    etapaCultivo.nombreEtapaDesarrollo = etapa.etapaCultivo.descripcion;

                                    especieAutorizadaPT.etapaCultivoList.Add(etapaCultivo);
                                }
                            }
                        }

                        grupo.Add(especieAutorizadaPT.grupoEspecieAutoriz.id.ToString());
                        especieAutorizadaPT.index = index;
                        index++;
                        List_EspecieAutOrdenada.Add(especieAutorizadaPT);
                    }
                }
                List_EspecieAut = List_EspecieAutOrdenada;
            }
            return List_EspecieAut;
        }



        public int ObtieneClaveProyectoTecnico(int idSolicitudConc)
        {

            try
            {

                ProyectoTecnico proyTec = new ProyectoTecnico();
                proyTec = proyTecnicoDA.ObtieneProyectoTecnico(idSolicitudConc, 0);

                if (proyTec != null && proyTec.IdProyectoTecnico > 0)
                {
                    return proyTec.IdProyectoTecnico;
                }
                return 0;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return 0;
            }

        }
        
        public bool ActualizarEstadoVigenciaArchivoPT(ArchivosAdjPT archivosAdjPT)
        {
            if (archivosAdjPT.cambiaEstado)
            {
                if (!proyTecnicoDA.ActualizarEstadoVigenciaArchivoPT(archivosAdjPT))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
