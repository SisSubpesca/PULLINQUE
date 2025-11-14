using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.resoluciones;
using LogicaNegocio.cl.subpesca.rb.errores;
using LogicaNegocio.cl.subpesca.rb.common;
using System.Data;

namespace Validaciones.cl.subpesca.rb.mantenedor
{
    public class MantenedorGeneralValidacion
    {

        Logger logger = new Logger();
        MantenedorDA mantenedorDA = new MantenedorDA();
        ResolucionService resolucionService = new ResolucionService();
        BarrioDA barrioDA = new BarrioDA();
        
        public List<string> validaDescansoSanitario(DescansoSanitario descansoSanitario)
        {
           List<String> listaErrroresDescansoSanitario = new List<String>();

           if (descansoSanitario != null)
           {

               DescansoSanitario descansoSanitarioUltimo = mantenedorDA.ObtenerUltimaOperacionDescanso_Mantenedor(descansoSanitario.barrio.id);

               if (descansoSanitarioUltimo.idDescanso == 0)
               {
                   /* Fecha Inicio de Descanso debe ser menor a la fecha de fin de descanso */
                   if (descansoSanitario.fechaInicio != default(DateTime) && descansoSanitario.fechaFin != default(DateTime) && descansoSanitario.fechaInicio > descansoSanitario.fechaFin)
                   {
                       listaErrroresDescansoSanitario.Add("La fecha desde inicio de descanso debe ser menor a la fecha de fin descanso.");
                   }

               }
               else {

                   /* Fecha Inicio de Descanso debe ser menor a la fecha de fin de descanso */
                   if (descansoSanitario.fechaInicio != default(DateTime) && descansoSanitario.fechaFin != default(DateTime) && descansoSanitario.fechaInicio > descansoSanitario.fechaFin)
                   {
                       listaErrroresDescansoSanitario.Add("La fecha desde inicio de descanso debe ser menor a la fecha de fin descanso.");
                   }

                   /* Fecha de Inicio producción del ultimo descanso debe ser menor a la fecha de inicio de descanso */
                   if (descansoSanitario.fechaInicio != default(DateTime) && descansoSanitarioUltimo.fechaInicioProduccionCero != default(DateTime) && descansoSanitarioUltimo.fechaInicioProduccionCero > descansoSanitario.fechaInicio)
                   {
                       listaErrroresDescansoSanitario.Add("La fecha de inicio producción del ultimo descanso debe ser menor a la fecha de inicio de descanso.");
                   }

                   /* Fecha de inicio de descanso y fecha de fin de descanso debe ser mayor a la fecha de fin de descanso del ultimo periodo de descanso */

                   if ((descansoSanitario.fechaInicio < descansoSanitarioUltimo.fechaFin) && (descansoSanitario.fechaFin < descansoSanitarioUltimo.fechaFin))
                   {
                       listaErrroresDescansoSanitario.Add("Fecha de inicio de descanso y fecha de fin de descanso debe ser mayor a la fecha de fin de descanso del último periodo de descanso.");
                   }

               
               }

           }

           return listaErrroresDescansoSanitario;
        }




        public List<string> validarIngresoAsociacionSol_UE_Barrio(AsociacionSolicitudBarrio asociacionSolicitudBarrio)
        {

            List<String> errores = new List<String>();

            try
            {
                SolicitudConcesion sol = null;

                //Tipo
                if (asociacionSolicitudBarrio.tipo == null || asociacionSolicitudBarrio.tipo.id < 0)
                {
                    errores.Add("Seleccione Tipo.");
                }


                if (errores.Count() == 0)
                {

                    if (asociacionSolicitudBarrio.tipo.id == rbTipo.RESOLUCION_SUB_REFERENCIA_SOLICITUD) //Solicitud
                    {
                        if (asociacionSolicitudBarrio.tipoSolicitud == null || asociacionSolicitudBarrio.tipoSolicitud.id < 0)
                        {
                            errores.Add("Seleccione Tipo Solicitud.");
                        }
                    }


                    if (asociacionSolicitudBarrio.tipo.id == rbTipo.RESOLUCION_SUB_REFERENCIA_UE) //Unidades Espaciales
                    {
                        if (asociacionSolicitudBarrio.tipoUnidadEspacial == null || asociacionSolicitudBarrio.tipoUnidadEspacial.id < 0)
                        {
                            errores.Add("Seleccione Tipo Unidad Espacial.");
                        }
                    }

                }


                if (errores.Count() == 0)
                {

                    //Solicitud
                    if (asociacionSolicitudBarrio.tipo.id == rbTipo.RESOLUCION_SUB_REFERENCIA_SOLICITUD)
                    { 

                        if (asociacionSolicitudBarrio.tipoSolicitud.id == rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA) //COLECTORES
                        {

                            if (asociacionSolicitudBarrio.numeroIdentificador == null || asociacionSolicitudBarrio.numeroIdentificador.Equals(""))
                            {
                                errores.Add("Ingrese Número Identificador.");
                            }

                            if (errores.Count() == 0)
                            {

                                //VERIFICAR QUE EXISTA
                                sol = resolucionService.VerificaExistenciaReferencia(asociacionSolicitudBarrio.tipo.id, 0, asociacionSolicitudBarrio.tipoSolicitud.id, asociacionSolicitudBarrio.numeroIdentificador.ToString(), 0);


                                if (sol == null || sol.idSolConcesion < 1)
                                {
                                    errores.Add("Solicitud No Encontrada.");
                                }
                                else
                                {
                                    asociacionSolicitudBarrio.idSolConcesion = sol.idSolConcesion;
                                }

                            }

                        }
                        else if (asociacionSolicitudBarrio.tipoSolicitud.id == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION) //Sector Relocalizacion
                        {

                            if (asociacionSolicitudBarrio.numPert == null || asociacionSolicitudBarrio.numPert.Trim().Equals(""))
                            {
                                errores.Add("Ingrese Pert.");
                            }

                            if (asociacionSolicitudBarrio.numSector < 0)
                            {
                                errores.Add("Ingrese Nº Sector.");
                            }

                            if (errores.Count() == 0)
                            {

                                String pertRelocalizaciones = asociacionSolicitudBarrio.numPert + "-" + asociacionSolicitudBarrio.numSector.ToString();

                                //VERIFICAR QUE EXISTA
                                sol = resolucionService.VerificaExistenciaReferencia(asociacionSolicitudBarrio.tipo.id, 0, asociacionSolicitudBarrio.tipoSolicitud.id, pertRelocalizaciones, asociacionSolicitudBarrio.numSector);


                                if (sol == null || sol.idSolConcesion < 1)
                                {
                                    errores.Add("Solicitud No Encontrada.");
                                }
                                else
                                {
                                    asociacionSolicitudBarrio.idSolConcesion = sol.idSolConcesion;
                                }


                            }

                        }
                        else //OTROS TIPOS DE SOLICITUDES
                        {
                            if (asociacionSolicitudBarrio.numPert == null || asociacionSolicitudBarrio.numPert.Trim().Equals(""))
                            {
                                errores.Add("Ingrese Pert.");
                            }

                            if (errores.Count() == 0)
                            {

                                //VERIFICAR QUE EXISTA
                                sol = resolucionService.VerificaExistenciaReferencia(asociacionSolicitudBarrio.tipo.id, 0, asociacionSolicitudBarrio.tipoSolicitud.id, asociacionSolicitudBarrio.numPert, 0);


                                if (sol == null || sol.idSolConcesion < 1)
                                {
                                    errores.Add("Solicitud No Encontrada.");

                                }
                                else
                                {
                                    asociacionSolicitudBarrio.idSolConcesion = sol.idSolConcesion;
                                }

                            }

                        }
                    }


                    //UNIDADES ESPACIALES
                    if (asociacionSolicitudBarrio.tipo.id == rbTipo.RESOLUCION_SUB_REFERENCIA_UE)
                    { 

                        if (asociacionSolicitudBarrio.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_COLECTORES_DE_SEMILLA) //COLECTORES
                        {

                            if (asociacionSolicitudBarrio.numeroIdentificador == null || asociacionSolicitudBarrio.numeroIdentificador.Equals(""))
                            {
                                errores.Add("Ingrese Número Identificador.");
                            }

                            if (errores.Count() == 0)
                            {

                                //VERIFICAR QUE EXISTA
                                sol = resolucionService.VerificaExistenciaReferencia(asociacionSolicitudBarrio.tipo.id, rbTipo.UNID_ESPACIAL_COLECTORES_DE_SEMILLA, 0, asociacionSolicitudBarrio.numeroIdentificador.ToString(), 0);


                                if (sol == null || sol.idSolConcesion < 1)
                                {
                                    errores.Add("Unidad espacial no Encontrada.");
                                }
                                else
                                {
                                    asociacionSolicitudBarrio.idSolConcesion = sol.idSolConcesion;
                                }

                            }

                        }
                        else //OTROS TIPOS DE UNIDADES ESPACIALES
                        {
                            if (asociacionSolicitudBarrio.codigoCentro == null || asociacionSolicitudBarrio.codigoCentro.Trim().Equals(""))
                            {
                                errores.Add("Ingrese Código Centro.");
                            }

                            if (errores.Count() == 0)
                            {

                                //VERIFICAR QUE EXISTA
                                sol = resolucionService.VerificaExistenciaReferencia(asociacionSolicitudBarrio.tipo.id, asociacionSolicitudBarrio.tipoUnidadEspacial.id, 0, asociacionSolicitudBarrio.codigoCentro, 0);

                                if (sol == null || sol.idSolConcesion < 1)
                                {
                                    errores.Add("Unidad espacial no Encontrada.");

                                }
                                else
                                {
                                    asociacionSolicitudBarrio.idSolConcesion = sol.idSolConcesion;
                                }

                            }

                        }
                    }

                    //validar que la acm y acs seleccionada pertenezca a la region de la solicitud

                    if (sol != null &&  sol.idSolConcesion > 0)
                    {

                        if (sol.region.id > 0)
                        {

                            if (asociacionSolicitudBarrio.barrioACS != null && asociacionSolicitudBarrio.barrioACS.id > 0)
                            {
                                DataTable dt = (DataTable)barrioDA.obtenerBarrio(rbTipo.TIPO_BARRIO_ACS, asociacionSolicitudBarrio.barrioACS.id, sol.region.id, 0);

                                if (!(dt != null && dt.Rows.Count > 0)) {
                                    errores.Add("La Unidad espacial/trámite debe estar dentro de la misma región de la ACS seleccionada.");
                                }



                            }

                            if (asociacionSolicitudBarrio.barrioACM != null && asociacionSolicitudBarrio.barrioACM.id > 0)
                            {
                                DataTable dt = (DataTable)barrioDA.obtenerBarrio(rbTipo.TIPO_BARRIO_ACM, asociacionSolicitudBarrio.barrioACM.id, sol.region.id, 0);

                                if (!(dt != null && dt.Rows.Count > 0))
                                {
                                    errores.Add("La Unidad espacial/trámite debe estar dentro de la misma región de la ACM seleccionada.");
                                }

                            }

                        }
                        else {
                            errores.Add("La Unidad espacial/trámite debe tener asignada su región para incluirla dentro de una ACS o ACM.");
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
