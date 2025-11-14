using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using Datos.Contantes;

namespace SubPesca.Administrador.includes
{
    public partial class menuprinc_v : System.Web.UI.UserControl
    {

        PermisosService permisosService = new PermisosService();

        protected void Page_Load(object sender, EventArgs e)
        {

            Datos.Entidades.Usuario.Serializable usuario_logeado = (Datos.Entidades.Usuario.Serializable)HttpContext.Current.Session["Usuario"];



            #region REPORTES UNIDADES ESPACIALES

            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.REPORTE_CONCESION }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelReportesUE.Visible = true;
                PanelReporteConcesionAcuicultura.Visible = true; 
            };
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.REPORTE_CONCESION }, usuario_logeado, null, rbAccion.ACCESO))  //se usa el mismo id de la concesion 
            {
                PanelReportesUE.Visible = true;
                PanelReporteExperimentalConcesion.Visible = true;
            };
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.REPORTE_AMERB }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelReportesUE.Visible = true;
                PanelReporteAmerb.Visible = true;
            };
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.REPORTE_AMERB }, usuario_logeado, null, rbAccion.ACCESO)) //se usa el mismo id de la acuicultura en amerb
            {
                PanelReportesUE.Visible = true;
                PanelReporteExperimentalAmerb.Visible = true;
            };
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.REPORTE_ECMPO }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelReportesUE.Visible = true;
                PanelReporteECMPO.Visible = true;
            }
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.REPORTE_FAENAMIENTO }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelReportesUE.Visible = true;
                PanelReporteCentroFaenamiento.Visible = true;
            }
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.REPORTE_ACOPIO }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelReportesUE.Visible = true;
                PanelReporteAcopio.Visible = true;     
            }
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.REPORTE_COLECTORES }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelReportesUE.Visible = true;
                PanelReporteSemilla.Visible = true;
            }


            #endregion


            #region SOLICITUDES


    //INICIO CONCESION DE ACUICULTURA

            /*CONCESION DE ACUICULTURA Ingresar y administrar solicitudes acuicultura concesión*/

            //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_CONCESION }, usuario_logeado, null, rbAccion.EDITAR))
            //{
                PanelSolicitudConcesion.Visible = true;
                PanelIngresarSolConcesion.Visible = true;
            //}

            //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_CONCESION }, usuario_logeado, null, rbAccion.EDITAR))
            //{
                PanelSolicitudConcesion.Visible = true;
                PanelAdministrarSolConcesion.Visible = true;
            //}
            
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.GENERAR_CIERRE_FORZADO_SOLICITUD_CONCESION }, usuario_logeado, null, rbAccion.EDITAR))
            {
                PanelSolicitudConcesion.Visible = true;
                PanelCierreConcesion.Visible = true;
            }

            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_CIERRE_FORZADO_SOLICITUD_CONCESION }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelSolicitudConcesion.Visible = true;
                PanelAdmCierreConcesion.Visible = true;    
            }

            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_CONCESION }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelSolicitudConcesion.Visible = true;
                PanelIndicadoresConcesion.Visible = true;
            };

    //FIN CONCESION DE ACUICULTURA





    //INICIO EXPERIMENTALES DE CONCESION

            /*Ingresar solicitudes Experimentales concesión*/
            //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_EXPERIMENTALES_CONCESION }, usuario_logeado, null, rbAccion.EDITAR))
            //{
                PanelSolicitudesExpConcesion.Visible = true;
                PanelIngresarExpConcesion.Visible = true;
            //};

            //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_EXPERIMENTAL_CONCESION }, usuario_logeado, null, rbAccion.EDITAR))
            //{
                PanelSolicitudesExpConcesion.Visible = true;
                PanelAdministrarExpConcesion.Visible = true;
            //};
            

            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.GENERAR_CIERRE_FORZADO_SOLICITUD_EXPERIMENTALES_CONCESION }, usuario_logeado, null, rbAccion.EDITAR))
            {
                PanelSolicitudesExpConcesion.Visible = true;
                PanelCierreExpConcesion.Visible = true;
            };
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_CIERRE_FORZADO_SOLICITUD_EXPERIMENTALES_CONCESION }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelSolicitudesExpConcesion.Visible = true;
                PanelAdmCierreExpConcesion.Visible = true;
            };
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_EXPERIMENTALES_CONCESION }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelSolicitudesExpConcesion.Visible = true;
                PanelResumenIndicadoresExpConcesion.Visible = true;
            };
            
    //FIN EXPERIMENTALES DE CONCESION




    //INICIO MODIFICACION CONCESION DE ACUICULTURA

            /* CONCESION DE ACUICULTURA Ingresar y administrar modificación concesión*/

            //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_MODIFICACION }, usuario_logeado, null, rbAccion.EDITAR))
            //{
            PanelModificacionConcesion.Visible = true;
            PanelIngresarModConcesion.Visible = true;
            //};

            //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_MODIFICACION_CONCESION }, usuario_logeado, null, rbAccion.EDITAR))
            //{
            PanelModificacionConcesion.Visible = true;
            PanelAdministrarModConcesion.Visible = true;
            //};

            /* CONCESION DE ACUICULTURA Indicadores modificación concesión*/

            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_MODIFICACION_AMPLIACION }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelModificacionConcesion.Visible = true;
                PanelIndicadoresModConcesion.Visible = true;
                PanelIndicadorAmpliacion.Visible = true;
            };
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_MODIFICACION_REDUCCION }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelModificacionConcesion.Visible = true;
                PanelIndicadoresModConcesion.Visible = true;
                PanelIndicadorReduccion.Visible = true;
            };
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_MODIFICACION_ESPECIE }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelModificacionConcesion.Visible = true;
                PanelIndicadoresModConcesion.Visible = true;
                PanelIndicadorEspecie.Visible = true;
            };
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_MODIFICACION_PROYECTO }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelModificacionConcesion.Visible = true;
                PanelIndicadoresModConcesion.Visible = true;
                PanelIndicadorProyecto.Visible = true;
            };
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_MODIFICACION_REGULARIZACION }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelModificacionConcesion.Visible = true;
                PanelIndicadoresModConcesion.Visible = true;
                PanelIndicadorRegulariza.Visible = true;
            };

            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.GENERAR_CIERRE_FORZADO_SOLICITUD_MODIFICACION_CONCESION }, usuario_logeado, null, rbAccion.EDITAR)) 
            {
                PanelModificacionConcesion.Visible = true;
                PanelCierreModificacionConcesion.Visible = true;
            }

            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_CIERRE_FORZADO_SOLICITUD_MODIFICACION_CONCESION }, usuario_logeado, null, rbAccion.ACCESO)) 
            {
                PanelModificacionConcesion.Visible = true;
                PanelAdmCierreModificacionConcesion.Visible = true;
            }


            

    //FIN  MODIFICACION CONCESION DE ACUICULTURA



    //INICIO RELOCALIZACION POR LEY

            /* Ingresar y administrar relocalización por ley */
            //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESO_RESOLUCION }, usuario_logeado, null, rbAccion.EDITAR))
            //{
                PanelRelocalizacionLey.Visible = true;
                PanelIngresarRelocalizacion.Visible = true;
            //};

            //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_RESOLUCION }, usuario_logeado, null, rbAccion.EDITAR))
            //{
                PanelAdmRelocalizacion.Visible = true;
                PanelIngresarRelocalizacion.Visible = true;
            //};

            /* Indicadores relocalización por ley*/
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_RELOCALIZACION_SECTOR_CERO }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelRelocalizacionLey.Visible = true;
                PanelIndicadoresRelocalizacion.Visible = true;
                PanelIndicadorCero.Visible = true;
            };
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_RELOCALIZACION_CREA }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelRelocalizacionLey.Visible = true;
                PanelIndicadoresRelocalizacion.Visible = true;
                PanelIndicadorCrea.Visible = true;
            };

            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_RELOCALIZACION_FUSION }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelRelocalizacionLey.Visible = true;
                PanelIndicadoresRelocalizacion.Visible = true;
                PanelIndicadorFusion.Visible = true;
            };

            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.GENERAR_CIERRE_FORZADO_SOLICITUD_RELOCALIZACION_LEY }, usuario_logeado, null, rbAccion.EDITAR))
            {
                PanelRelocalizacionLey.Visible = true;
                PanelCierreForzadoRelocalizacionLey.Visible = true;
            }

            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_CIERRE_FORZADO_SOLICITUD_RELOCALIZACION_LEY }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelRelocalizacionLey.Visible = true;
                PanelAdmCierreForzadoRelocalizacionLey.Visible = true;
            }


    //FIN RELOCALIZACION POR LEY




    //INICIO RELOCALIZACION RESA

            //NO REQUIERE PERMISOS DE ACCESO, PUES EL USUARIO SIEMPRE DEBE PODER VISUALIZAR EL INGRESO DE LA SOLICITUD Y EL ADMINISTRAR.
            PanelIngresarRelocalizacionRESA.Visible = true;
            PanelRESA.Visible = true;

            PanelAdmRelocalizacionRESA.Visible = true;
            PanelRESA.Visible = true;

            
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESO_INFORME_RESA }, usuario_logeado, null, rbAccion.EDITAR))
            {
                PanelRESA.Visible = true;
                PanelIngresarRESA.Visible = true; //INGRESO INFORME RELOCALIZACION RESA
            };
            
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRADOR_INFORMES_RESA }, usuario_logeado, null, rbAccion.EDITAR))
            {
                PanelRESA.Visible = true;
                PanelAdministrarRESA.Visible = true; //ADMINISTRAR INFORME RELOCALIZACION RESA
            };

            /*Indicadores RELOCALIZACION RESA */

            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_RELOCALIZACION_RESA_SECTOR_CERO }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelRESA.Visible = true;
                PanelIndicadoresRESA.Visible = true;
                PanelIndicadorRESACero.Visible = true;
            };

            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_RELOCALIZACION_RESA_CREA }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelRESA.Visible = true;
                PanelIndicadoresRESA.Visible = true;
                PanelIndicadorRESACrea.Visible = true;
            };

            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_RELOCALIZACION_RESA_FUSION }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelRESA.Visible = true;
                PanelIndicadoresRESA.Visible = true;
                PanelIndicadorRESAFusion.Visible = true;
            };

            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.GENERAR_CIERRE_FORZADO_SOLICITUD_RELOCALIZACION_RESA }, usuario_logeado, null, rbAccion.EDITAR))
            {
                PanelCierreForzadoRelocalizacionRESA.Visible = true;
                PanelRESA.Visible = true;
            }

            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_CIERRE_FORZADO_SOLICITUD_RELOCALIZACION_RESA }, usuario_logeado, null, rbAccion.ACCESO))
            {
                administrarCierreForzadoRelocalizacionRESA.Visible = true;
                PanelRESA.Visible = true;
            }

    //FIN RELOCALIZACION RESA



    //INICIO FAENAMIENTO
            
            
            /* Faenamiento*/
            //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_FAENAMIENTO }, usuario_logeado, null, rbAccion.EDITAR))
            //{
                PanelSolFaenamiento.Visible = true;
                PanelIngresarSolFaenamiento.Visible = true;
            //};
            //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_FAENAMIENTO }, usuario_logeado, null, rbAccion.EDITAR))
            //{
                PanelSolFaenamiento.Visible = true;
                PanelAdministrarSolFaenamiento.Visible = true;
            //};
                    
         
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_FAENAMIENTO }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelSolFaenamiento.Visible = true;
                PanelIndicadoresFaenamiento.Visible = true;
            };
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.GENERAR_CIERRE_FORZADO_SOLICITUD_FAENAMIENTO }, usuario_logeado, null, rbAccion.EDITAR))
            {
                PanelSolFaenamiento.Visible = true;
                PanelCierreFaenamiento.Visible = true;
            };
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_CIERRE_FORZADO_SOLICITUD_FAENAMIENTO }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelSolFaenamiento.Visible = true;
                PanelAdministrarCierreFaenamiento.Visible = true;
            };

    //FIN FAENAMIENTO                


    //INICIO FAENAMIENTO MODIFICACION 

            /*Ingresar y administrar solicitud modificación faenamiento*/

            //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_MODIFICACION_FAENAMIENTO }, usuario_logeado, null, rbAccion.EDITAR))
            //{
                PanelSolModificacionFaenamiento.Visible = true;
                PanelIngresarModificacionFaenamiento.Visible = true;
            //};
            //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_MODIFICACION_FAENAMIENTO }, usuario_logeado, null, rbAccion.EDITAR))
            //{
                PanelSolModificacionFaenamiento.Visible = true;
                PanelAdministrarModificacionFaenamiento.Visible = true;
            //};


                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_FAENAMIENTO_MODIFICACION_AMPLIACION }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelSolModificacionFaenamiento.Visible = true;
                    PanelIndicadoresModFaenamiento.Visible = true;
                    PanelIndicadorFaenamientoAmpliacion.Visible = true;
                };
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_FAENAMIENTO_MODIFICACION_REDUCCION }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelSolModificacionFaenamiento.Visible = true;
                    PanelIndicadoresModFaenamiento.Visible = true;
                    PanelIndicadorFaenamientoReduccion.Visible = true;
                };
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_FAENAMIENTO_MODIFICACION_ESPECIE }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelSolModificacionFaenamiento.Visible = true;
                    PanelIndicadoresModFaenamiento.Visible = true;
                    PanelIndicadorFaenamientoEspecie.Visible = true;
                };
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_FAENAMIENTO_MODIFICACION_PROYECTO }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelSolModificacionFaenamiento.Visible = true;
                    PanelIndicadoresModFaenamiento.Visible = true;
                    PanelIndicadorFaenamientoProyectoTecnico.Visible = true;
                };
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_FAENAMIENTO_MODIFICACION_REGULARIZACION }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelSolModificacionFaenamiento.Visible = true;
                    PanelIndicadoresModFaenamiento.Visible = true;
                    PanelIndicadorFaenamientoRegularizacion.Visible = true;
                };


                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.GENERAR_CIERRE_FORZADO_SOLICITUD_MODIFICACION_FAENAMIENTO }, usuario_logeado, null, rbAccion.EDITAR))
                {
                    PanelCierreForzadoModificacionCentroFaenamiento.Visible = true;
                    PanelSolModificacionFaenamiento.Visible = true;
                }

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_CIERRE_FORZADO_SOLICITUD_MODIFICACION_FAENAMIENTO }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelAdministrarCierreForzadoModificacionCentroFaenamiento.Visible = true;
                    PanelSolModificacionFaenamiento.Visible = true;
                }


    //FIN FAENAMIENTO MODIFICACION 

    //INICIO ACOPIO

                /*Ingresar y administrar solicitud de centro de acopio*/
                //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_ACOPIO }, usuario_logeado, null, rbAccion.EDITAR))
                //{
                PanelSolicitudAcopio.Visible = true;
                PanelIngresarAcopio.Visible = true;
                //};

                //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_ACOPIO }, usuario_logeado, null, rbAccion.EDITAR))
                //{
                PanelSolicitudAcopio.Visible = true;
                PanelAdministrarAcopio.Visible = true;
                //};


                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_ACOPIO }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelSolicitudAcopio.Visible = true;
                    PanelIndicadoresAcopio.Visible = true;
                };

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.GENERAR_CIERRE_FORZADO_SOLICITUD_ACOPIO }, usuario_logeado, null, rbAccion.EDITAR))
                {
                    PanelSolicitudAcopio.Visible = true;
                    PanelCierreAcopio.Visible = true;
                };
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_CIERRE_FORZADO_SOLICITUD_ACOPIO }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelSolicitudAcopio.Visible = true;
                    PanelAdmCierreAcopio.Visible = true;
                };


    //FIN ACOPIO


    //INICIO MODIFCACION ACOPIO                

                /*Ingresar y administración solicitudes de Modificación de Centros de Acopio*/
                //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_MODIFICACION_CENTRO_DE_ACOPIO }, usuario_logeado, null, rbAccion.EDITAR))
                //{
                PanelSolModificacionAcopio.Visible = true;
                PanelIngresarModAcopio.Visible = true;
                //};

                //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_MODIFICACION_CENTRO_DE_ACOPIO }, usuario_logeado, null, rbAccion.EDITAR))
                //{
                PanelSolModificacionAcopio.Visible = true;
                PanelAdministrarModAcopio.Visible = true;
                //};
                

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_ACOPIO_MODIFICACION_AMPLIACION }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelSolModificacionAcopio.Visible = true;
                    PanelIndicadoresModAcopio.Visible = true;
                    PanelIndicadorAcopioAmpliacion.Visible = true;
                };
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_ACOPIO_MODIFICACION_REDUCCION }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelSolModificacionAcopio.Visible = true;
                    PanelIndicadoresModAcopio.Visible = true;
                    PanelIndicadorAcopioReduccion.Visible = true;
                };
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_ACOPIO_MODIFICACION_ESPECIE }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelSolModificacionAcopio.Visible = true;
                    PanelIndicadoresModAcopio.Visible = true;
                    PanelIndicadorAcopioEspecie.Visible = true;
                };
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_ACOPIO_MODIFICACION_PROYECTO }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelSolModificacionAcopio.Visible = true;
                    PanelIndicadoresModAcopio.Visible = true;
                    PanelIndicadorAcopioProyectoTecnico.Visible = true;
                };
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_ACOPIO_MODIFICACION_REGULARIZACION }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelSolModificacionAcopio.Visible = true;
                    PanelIndicadoresModAcopio.Visible = true;
                    PanelIndicadorAcopioRegularizacion.Visible = true;
                };

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.GENERAR_CIERRE_FORZADO_SOLICITUD_MODIFICACION_ACOPIO }, usuario_logeado, null, rbAccion.EDITAR))
                {
                    PanelCierreForzadoModificacionCentroAcopio.Visible = true;
                    PanelSolModificacionAcopio.Visible = true;
                }

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_CIERRE_FORZADO_SOLICITUD_MODIFICACION_ACOPIO }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelAdministrarCierreForzadoModificacionCentroAcopio.Visible = true;
                    PanelSolModificacionAcopio.Visible = true;
                }


    //FIN MODIFCACION ACOPIO





    //INICIO ECMPO            

                /*Ingresar y administrar Solicitudes de Acuicultura en ECMPO*/
                //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_ECMPO }, usuario_logeado, null, rbAccion.EDITAR))
                //{
                PanelECMPO.Visible = true;
                PanelIngresarECMPO.Visible = true;
                //}

                //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINSITRAR_SOLICITUD_ECMPO }, usuario_logeado, null, rbAccion.EDITAR))
                //{
                PanelECMPO.Visible = true;
                PanelAdmECMPO.Visible = true;
                //}

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_ECMPO }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelECMPO.Visible = true;
                    PanelIndicadoresECMPO.Visible = true;
                }
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.GENERAR_CIERRE_FORZADO_SOLICITUD_ECMPO }, usuario_logeado, null, rbAccion.EDITAR))
                {
                    PanelECMPO.Visible = true;
                    PanelCierreECMPO.Visible = true;
                }
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_CIERRE_FORZADO_SOLICITUD_ECMPO }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelECMPO.Visible = true;
                    PanelAdmCierreECMPO.Visible = true;
                }

    //FIN ECMPO


    //INICIO MODIFICACION ECMPO

                /* Modificación ECMPO */
                //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_MODIFICACION_ECMPO }, usuario_logeado, null, rbAccion.EDITAR))
                //{
                PanelModificacionECMPO.Visible = true;
                PanelIngresarModECMPO.Visible = true;
                //}
                //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_MODIFICACION_ECMPO }, usuario_logeado, null, rbAccion.EDITAR))
                //{
                PanelModificacionECMPO.Visible = true;
                PanelAdministrarModECMPO.Visible = true;
                //}
               

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_ECMPO_MODIFICACION_AMPLIACION }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelModificacionECMPO.Visible = true;
                    PanelIndicadoresModECMPO.Visible = true;
                    PanelIndicadorECMPOAmpliacion.Visible = true;
                };
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_ECMPO_MODIFICACION_REDUCCION }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelModificacionECMPO.Visible = true;
                    PanelIndicadoresModECMPO.Visible = true;
                    PanelIndicadorECMPOReduccion.Visible = true;
                };
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_ECMPO_MODIFICACION_ESPECIE }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelModificacionECMPO.Visible = true;
                    PanelIndicadoresModECMPO.Visible = true;
                    PanelIndicadorECMPOEspecie.Visible = true;
                };
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_ECMPO_MODIFICACION_PROYECTO }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelModificacionECMPO.Visible = true;
                    PanelIndicadoresModECMPO.Visible = true;
                    PanelIndicadorECMPOProyecto.Visible = true;
                };
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_ECMPO_MODIFICACION_REGULARIZACION }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelModificacionECMPO.Visible = true;
                    PanelIndicadoresModECMPO.Visible = true;
                    PanelIndicadorECMPORegulariza.Visible = true;
                };


                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.GENERAR_CIERRE_FORZADO_SOLICITUD_MODIFICACION_ECMPO }, usuario_logeado, null, rbAccion.EDITAR))
                {
                    PanelCierreForzadoModificacionECMPO.Visible = true;
                    PanelModificacionECMPO.Visible = true;
                }

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_CIERRE_FORZADO_SOLICITUD_MODIFICACION_ECMPO }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelAdministracionCierreForzadoModificacionECMPO.Visible = true;
                    PanelModificacionECMPO.Visible = true;
                }


    //FIN MODIFICACION ECMPO






    //INICIO ACUICULTURA EN AMERB

                /* Acuicultura en Amerb */
                //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_AMERB }, usuario_logeado, null, rbAccion.EDITAR))
                //{
                PanelSolicitudesAcuiculturaAmerb.Visible = true;
                PanelIngresarAmerb.Visible = true;
                //}
                //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_AMERB }, usuario_logeado, null, rbAccion.EDITAR))
                //{
                PanelSolicitudesAcuiculturaAmerb.Visible = true;
                PanelAdministrarAmerb.Visible = true;
                //}


                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_AMERB }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelSolicitudesAcuiculturaAmerb.Visible = true;
                    PanelIndicadoresSolAmerb.Visible = true;
                }
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.GENERAR_CIERRE_FORZADO_SOLICITUD_AMERB }, usuario_logeado, null, rbAccion.EDITAR))
                {
                    PanelSolicitudesAcuiculturaAmerb.Visible = true;
                    PanelCierreAmerb.Visible = true;
                }
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_CIERRE_FORZADO_SOLICITUD_AMERB }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelSolicitudesAcuiculturaAmerb.Visible = true;
                    PanelAdmCierreAmerb.Visible = true;
                }

    //FIN  ACUICULTURA EN AMERB



    //INICIO EXPERIMENTALES AMERB

                /* Solicitudes Experimentales amerb */
                //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_EXPERIMENTALES_AMERB }, usuario_logeado, null, rbAccion.EDITAR))
                //{
                PanelExperimentalesAmerb.Visible = true;
                PanelIngresaSolExperimentalAmerb.Visible = true;
                //}

                //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINSITRAR_SOLICITUD_EXPERIMENTAL_AMERB }, usuario_logeado, null, rbAccion.EDITAR))
                //{
                PanelExperimentalesAmerb.Visible = true;
                PanelAdministrarSolExperimentalAmerb.Visible = true;
                //}

                
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_EXPERIMENTALES_AMERB }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelExperimentalesAmerb.Visible = true;
                    PanelIndicadoresExperimentalAmerb.Visible = true;
                }
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.GENERAR_CIERRE_FORZADO_SOLICITUD_EXPERIMENTALES_AMERB }, usuario_logeado, null, rbAccion.EDITAR))
                {
                    PanelExperimentalesAmerb.Visible = true;
                    PanelCierreExperimentalAmerb.Visible = true;
                }

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_CIERRE_FORZADO_SOLICITUD_EXPERIMENTALES_AMERB }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelExperimentalesAmerb.Visible = true;
                    PanelAdmiCierreExperimentalAmerb.Visible = true;
                }

    //FIN EXPERIMENTALES AMERB


    //INICIO MODIFICACION AMERB

                /* Solicitudes Modificación Amerb */
                //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_MODIFICACION_AMERB }, usuario_logeado, null, rbAccion.EDITAR))
                //{
                PanelModAmerb.Visible = true;
                PanelIngresarModAmerb.Visible = true;
                //}

                //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINSITRAR_SOLICITUD_MODIFICACION_AMERB }, usuario_logeado, null, rbAccion.EDITAR))
                //{
                PanelModAmerb.Visible = true;
                PanelAdministrarModAmerb.Visible = true;
                //}

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_AMERB_MODIFICACION_AMPLIACION }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelModAmerb.Visible = true;
                    PanelIndicadoresModAMERB.Visible = true;
                    PanelIndicadorAMERBAmpliacion.Visible = true;
                };
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_AMERB_MODIFICACION_REDUCCION }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelModAmerb.Visible = true;
                    PanelIndicadoresModAMERB.Visible = true;
                    PanelIndicadorAMERBReduccion.Visible = true;
                };
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_AMERB_MODIFICACION_ESPECIE }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelModAmerb.Visible = true;
                    PanelIndicadoresModAMERB.Visible = true;
                    PanelIndicadorAMERBEspecie.Visible = true;
                };
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_AMERB_MODIFICACION_PROYECTO }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelModAmerb.Visible = true;
                    PanelIndicadoresModAMERB.Visible = true;
                    PanelIndicadorAMERBProyecto.Visible = true;
                };
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_AMERB_MODIFICACION_REGULARIZACION }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelModAmerb.Visible = true;
                    PanelIndicadoresModAMERB.Visible = true;
                    PanelIndicadorAMERBRegulariza.Visible = true;
                };
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.GENERAR_CIERRE_FORZADO_SOLICITUD_MODIFICACION_AMERB }, usuario_logeado, null, rbAccion.EDITAR))
                {
                    PanelCierreForzadoModificacionAmerb.Visible = true;
                    PanelModAmerb.Visible = true;
                }

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_CIERRE_FORZADO_SOLICITUD_MODIFICACION_AMERB }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelAdministracionCierreForzadoModificacionAmerb.Visible = true;
                    PanelModAmerb.Visible = true;
                }

    //FIN MODIFICACION AMERB


    //INICIO COLECTORES DE SEMILLA

                /* Solicitudes Colectores semillas*/

                //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_COLECTORES }, usuario_logeado, null, rbAccion.EDITAR))
                //{
                PanelSolColectorSemilla.Visible = true;
                PanelIngresarColector.Visible = true;
                //}

                //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINSITRAR_SOLICITUD_COLECTORES }, usuario_logeado, null, rbAccion.EDITAR))
                //{
                PanelSolColectorSemilla.Visible = true;
                PanelAdminColector.Visible = true;
                //}

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INDICADORES_COLECTORES }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelSolColectorSemilla.Visible = true;
                    PanelIndicadoresColector.Visible = true;
                }
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.GENERAR_CIERRE_FORZADO_SOLICITUD_COLECTOR }, usuario_logeado, null, rbAccion.EDITAR))
                {
                    PanelSolColectorSemilla.Visible = true;
                    PanelCierreForzadoColector.Visible = true;
                }

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_CIERRE_FORZADO_SOLICITUD_COLECTOR }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelSolColectorSemilla.Visible = true;
                    PanelAdminCierreForzado.Visible = true;
                }


    //FIN COLECTORES DE SEMILLA

    #endregion


            #region INICIO VISACIONES MASIVAS

    //INICIO VISACIONES MASIVAS

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INICIO_VISACIONES_MASIVAS }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelCajadeHerramientas.Visible = true;
                    PanelInicioVisacionMasiva.Visible = true;
                    PanelVisacionesMasivas.Visible = true;
                }

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.VISACIONES_MASIVAS }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelCajadeHerramientas.Visible = true;
                    PanelVisacionMasiva.Visible = true;
                    PanelVisacionesMasivas.Visible = true;
                }

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.FIRMA_MASIVAS }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    PanelCajadeHerramientas.Visible = true;
                    PanelFirmaMasiva.Visible = true;
                    PanelVisacionesMasivas.Visible = true;
                }

    //FIN VISACIONES MASIVAS

            #endregion 






            
    //INICIO REPORTES SOLICITUDES
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.REPORTE_CONCESION }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelCajadeHerramientas.Visible = true;
                PanelReporte.Visible = true;
                PanelReporteSolicitudesAcuicultura.Visible = true;
            }
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.REPORTE_FAENAMIENTO }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelCajadeHerramientas.Visible = true;
                PanelReporte.Visible = true;
                PanelReporteSolicitudesFaenamiento.Visible = true;
            }
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.REPORTE_ACOPIO }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelCajadeHerramientas.Visible = true;
                PanelReporte.Visible = true;
                PanelReporteCentroAcopio.Visible = true;
            }
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.REPORTE_COLECTORES }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelCajadeHerramientas.Visible = true;
                PanelReporte.Visible = true;
                PanelReporteCentroSemilla.Visible = true;
            }
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.REPORTE_ECMPO }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelCajadeHerramientas.Visible = true;
                PanelReporte.Visible = true;
                PanelReporteAcuiculturaECMPO.Visible = true;
            }
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.REPORTE_AMERB }, usuario_logeado, null, rbAccion.ACCESO))
            {
                PanelCajadeHerramientas.Visible = true;
                PanelReporte.Visible = true;
                PanelReporteSolicitudesAmerb.Visible = true;
            }

    //FIN REPORTES SOLICITUDES
            
            
          



        //RESUMEN DE ESTADOS 
        if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.RESUMEN_ESTADOS }, usuario_logeado, null, rbAccion.ACCESO))
        {
            PanelCajadeHerramientas.Visible = true;
            PanelResumenEstado.Visible = true;
            PanelResumenEstado2.Visible = true;
        }
        //FIN RESUMEN DE ESTADOS 


        //REQUERIMIENTOS CON PLAZOS
        if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.REQUERIMIENTOS_PLAZOS }, usuario_logeado, null, rbAccion.ACCESO))
        {
            PanelCajadeHerramientas.Visible = true;
            PanelRequerimientosPlazos.Visible = true;
            PanelRequerimientosPlazos2.Visible = true;
        }
        //FIN REQUERIMIENTOS CON PLAZOS

        //RESOLUCIONES
        if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESO_RESOLUCION }, usuario_logeado, null, rbAccion.EDITAR))
        {
            PanelIngresarResolucion.Visible = true;
            PanelResolucion.Visible = true;
        };
        if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_RESOLUCION }, usuario_logeado, null, rbAccion.EDITAR))
        {
            PanelAdministrarResolucion.Visible = true;
            PanelResolucion.Visible = true;
        };
        //FIN RESOLUCIONES


        //TITULARES
        if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_TITULARES }, usuario_logeado, null, rbAccion.EDITAR))
        {
            PanelMantenedorTitulares.Visible = true;
            PanelTitulares.Visible = true;
            PanelAdministrarTitular.Visible = true;
        }
        if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_REPRESENTANTE_LEGAL }, usuario_logeado, null, rbAccion.EDITAR))
        {
            PanelMantenedorTitulares.Visible = true;
            PanelTitulares.Visible = true;
            Pane1AdministrarRepresentante.Visible = true;
        }
        if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_OPERADORES }, usuario_logeado, null, rbAccion.EDITAR))
        {
            PanelMantenedorTitulares.Visible = true;
            PanelTitulares.Visible = true;
            PanelAdministrarOperadores.Visible = true;
        }
        /* Mantenedor de titulares menú especial */
        if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_TITULARES }, usuario_logeado, null, rbAccion.EDITAR))
        {
            PanelMantenedorTitulares.Visible = true;
            PanelTipoOrganizacion.Visible = true;
            PanelAdministrarHolding.Visible = true;
            PanelTipoContacto.Visible = true;
            PanelArchivosTitulares.Visible = true;
            
        }
        //FIN TITULARES


        //USUARIOS
        if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRACION_DE_USUARIOS }, usuario_logeado, null, 0))
        {
            PanelUsuarios.Visible = true;
            PanelAdminUsuarios.Visible = true;
        }
        if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRACION_DE_ROLES }, usuario_logeado, null, 0))
        {
            PanelUsuarios.Visible = true;
            PanelAdminRoles.Visible = true;
        }
        //FIN USUARIOS


        //INICIO MANTENEDOR DOCUMENTAL (ADMINISTRADOR DE MANTENEDORES)

        if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.MANTENEDOR_TRANSVERSAL }, usuario_logeado, null, rbAccion.EDITAR))
        {
            PanelMantenedores.Visible = true;
            PanelMantenedorTransversal.Visible = true;
            
            PanelTipoDocumento.Visible = true;
            PanelSubrequerimiento.Visible = true;
            PanelAsocSubTipo.Visible = true;
            PanelAsocSubResultado.Visible = true;
            //PanelModAsocSubreqResultado.Visible = true;
            PanelResultado.Visible = true;
            PanelPreferenciaRelocalizacion.Visible = true;
            PanelPlazoDocumentos.Visible = true;
            PanelConsultores.Visible = true;
            PanelOficinaZonal.Visible = true;
            PanelDireccionZonal.Visible = true;
            PanelEntidadesDeAnalisis.Visible = true;
            PanelEntidadesDeMuestreo.Visible = true;
            PaneltipoSupeditado.Visible = true;
            PanelMateria.Visible = true;
            PanelMateriaSubrequerimiento.Visible = true;
            PanelEstadosUOT.Visible = true;
            PanelEquivalenciaEstadoUOT.Visible = true;

        }


        if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.MANTENEDOR_GENERAL }, usuario_logeado, null, rbAccion.EDITAR))
        {
            PanelMantenedores.Visible = true;
            PanelMantenedorGeneral.Visible = true;

            //ADMINISTRACION TERRITORIAL
            PanelRegion.Visible = true;
            PanelProvidencia.Visible = true;
            PanelComuna.Visible = true;
            PanelCapitania.Visible = true;
            PanelTipoCuerpoAgua.Visible = true;
            PanelCuerpoAgua.Visible = true;

            //AGRUPACION DE CONCESION
            PanelTipoBarrio.Visible = true;
            PanelBarrio.Visible = true;
            PanelDescanso.Visible = true;
            PanelMacrozona.Visible = true;

            //CARTOGRAFIA
            PanelCarta.Visible = true;
            PanelDatum.Visible = true;
            PanelTipoHuso.Visible = true;
            PanelVertice.Visible = true;
            PanelTipoUso.Visible = true;
            PanelCoodenadasOriginales.Visible = true;
            PanelArchivoAntedentes.Visible = true;
            PanelArchivoRegularizacion.Visible = true;


            //ESPECIES
            PanelEspecieCultivo.Visible = true;
            PanelEspecieAlimento.Visible = true;
            PanelTipoCultivo.Visible = true;
            PanelUnidadEjemplar.Visible = true;
            PanelEtapaCultivo.Visible = true;
            //PanelEspecieEtapa.Visible = true;
            PanelPesoEjemplar.Visible = true;
            PanelTipoAlimento.Visible = true;
            PanelGrupoEspecie.Visible = true;


            //ESTRUCTURA TECNICA
            PanelEstructuraTecnica.Visible = true;
            PanelFormaEstructura.Visible = true;
            PanelUnidadEstructura.Visible = true;
            PanelVolumenUnidadMedidaEstructura.Visible = true;
            PanelAnio.Visible = true;
            PanelCultivoAlgas.Visible = true;


            //OTROS
            PanelTipoConcesion.Visible = true;
            PanelTipoCentroAcopio.Visible = true;
            PanelTipoCentroFaenamiento.Visible = true;
            PanelPlazoNominal.Visible = true;


            //ADMINISTRAR UNIDAD ESPACIAL O SOLICITUD EN BARRIO 
            PanelBarrioAsociaciones.Visible = true;

            /* Mantenedor transversal */
            if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.MANTENEDOR_TRANSVERSAL }, usuario_logeado, null, rbAccion.EDITAR))
            {
                PanelMaterias.Visible = true;
                PanelMantenedores.Visible = true;
                PanelMantenedorTransversal.Visible = true;
                PanelResultado.Visible = true;
                PanelAsocSubResultado.Visible = true;
                PanelTipoDocumento.Visible = true;
                PanelAsocSubTipo.Visible = true;
                PanelSubrequerimiento.Visible = true;
                Panelferiado.Visible = true;
                PanelPreferenciaRelocalizacion.Visible = true;

                PaneltipoSupeditado.Visible = true;
                PanelEntidadesDeMuestreo.Visible = true;
                PanelEntidadesDeAnalisis.Visible = true;
                PanelOficinaZonal.Visible = true;
                PanelDireccionZonal.Visible = true;
                PanelConsultores.Visible = true;
                PanelPlazoDocumentos.Visible = true;
            }
        }


        Bitacoracambios.Visible = true;
        AdministradorCorreos.Visible = true;

        /******Permisos para Bitacora de cambios, generador documental, *******/
        if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.GeneradorDocumental }, usuario_logeado, null, rbAccion.EDITAR))
        {
            PanelCajadeHerramientas.Visible = true;
            PanelGenerador_Administrar.Visible = true;
            PanelGeneradorDocumental.Visible = true;
        }
        if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.AdministradorDocumental }, usuario_logeado, null, rbAccion.EDITAR))
        {
            PanelCajadeHerramientas.Visible = true;
            PanelGenerador_Administrar.Visible = true;
            PanelAdministradorDocumental.Visible = true;
        }
        if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.Carga_Masiva_Poligonos }, usuario_logeado, null, rbAccion.EDITAR))
        {
            PanelCajadeHerramientas.Visible = true;
            PanelPoligonosMasivos.Visible = true;
        }
        if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.GrupoSuspendido }, usuario_logeado, null, rbAccion.EDITAR))
        {
            PanelCajadeHerramientas.Visible = true;
            PanelGrupoSuspendido.Visible = true;
            CrearGrupoSuspendido.Visible = true;
            AdministrarGrupoSuspendido.Visible = true;
        }
        if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.Bitacora_Cambios }, usuario_logeado, null, rbAccion.EDITAR))
        {
            PanelCajadeHerramientas.Visible = true;
            PanelPoligonosMasivos.Visible = true;
        }
        if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.TitularesPendientes }, usuario_logeado, null, rbAccion.EDITAR))
        {
            PanelTitularesPendientes.Visible = true;
        } 

            /*****/


        /****Fin*****/
            
            
        //PanelTipoFondo.Visible = true;

        //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_TITULARES }, usuario_logeado, null, rbAccion.EDITAR))
        //{
        //    PanelTitulares.Visible = true;
        //    PanelAdministrarHolding.Visible = true;
        //}

            
        //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_TITULARES }, usuario_logeado, null, rbAccion.EDITAR))
        //{

        //    PanelTitulares.Visible = true;
        //    PanelTipoContacto.Visible = true;

        //}
        //if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_TITULARES }, usuario_logeado, null, rbAccion.EDITAR))
        //{

        //    PanelTitulares.Visible = true;
        //    PanelArchivosTitulares.Visible = true;

        //}
        }
    }
}