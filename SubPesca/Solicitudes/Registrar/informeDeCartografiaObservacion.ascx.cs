using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using Datos.Contantes;
using Datos.Entidades;
using Datos.Utilidades;



namespace SubPesca.Solicitudes.Registrar
{
    public partial class informeDeCartografiaObservacion : System.Web.UI.UserControl
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        ObsPestaniaInformeDA obsPestaniaInformeDA = new ObsPestaniaInformeDA();
        PermisosService permisosService = new PermisosService();
        int dbTipo = rbTipo.INFO_CARTOGRAFIA;

        Funciones funciones = new Funciones();



        protected void Page_Init(object sender, EventArgs e)
        {

        }


        protected void setearModulo()
        {

            if (funciones.retornaModulo().Equals("Registrar"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLCONCESION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLCONCESION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLCONCESION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLCONCESION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLCONCESION;
                ViewState["solicitudSession"] = paginas.solicitudConcesionSession;

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_CONCESION };

            }
            else if (funciones.retornaModulo().Equals("Relocalizacion"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_RELOCALIZACION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_RELOCALIZACION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_RELOCALIZACION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_RELOCALIZACION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_RELOCALIZACION;
                ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSession;


                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_CREA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_FUSIONA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_SECTOR_CERO };

                }

            }
            else if (funciones.retornaModulo().Equals("RelocalizacionRESA"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_RELOCALIZACION_RESA;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_RELOCALIZACION_RESA;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_RELOCALIZACION_RESA;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_RELOCALIZACION_RESA;
                ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSessionRESA;


                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA_RESA)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_CREA_RESA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA_RESA)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_FUSIONA_RESA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO_RESA)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_SECTOR_CERO_RESA };

                }

            }
            else if (funciones.retornaModulo().Equals("Acopio"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_ACOPIO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_ACOPIO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_ACOPIO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_ACOPIO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_CENTRO_DE_ACOPIO;
                ViewState["solicitudSession"] = paginas.solicitudAcopioSession;

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_ACOPIO };

            }
            else if (funciones.retornaModulo().Equals("Faenamiento"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_CENTRO_DE_FAENAMIENTO;
                ViewState["solicitudSession"] = paginas.solicitudFaenamientoSession;

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_FAENAMIENTO };

            }
            else if (funciones.retornaModulo().Equals("Amerb"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_AMERB;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_AMERB;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_AMERB;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_AMERB;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_AMERB;
                ViewState["solicitudSession"] = paginas.solicitudAmerbSession;

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_AMERB };

            }
            else if (funciones.retornaModulo().Equals("ExperimentalesAmerb"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesAmerbSession;

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_EXPERIMENTALES_AMERB };

            }
            else if (funciones.retornaModulo().Equals("ExperimentalesConcesion"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesConcesionSession;

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_EXPERIMENTALES_CONCESION };

            }
            else if (funciones.retornaModulo().Equals("ECMPO"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_ECMPO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_ECMPO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_ECMPO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_ECMPO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_ECMPO;
                ViewState["solicitudSession"] = paginas.solicitudECMPOSession;

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_ECMPO };

            }
            else if (funciones.retornaModulo().Equals("Colector"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_COLECTORES_SEMILLA;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_COLECTORES_SEMILLA;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_COLECTORES_SEMILLA;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_COLECTORES_SEMILLA;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_COLECTORES_SEMILLA;
                ViewState["solicitudSession"] = paginas.solicitudColectorSession;

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_COLECTOR };

            }
            else if (funciones.retornaModulo().Equals("ModificacionAmerb"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_AMERB;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_AMERB;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_AMERB;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_AMERB;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_AMERB;
                ViewState["solicitudSession"] = paginas.solicitudModificacionAmerbSession;

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion != null)
                {

                    int[] tiposModificacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

                    int contador = 0;
                    foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                    {
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_AMPLIA_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_AMERB_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_AMERB_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_PT)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_AMERB_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_AMERB_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_AMERB_REGULARIZACION;
                            contador++;
                        }
                    }

                    ViewState["SECCION_ESPECIFICA"] = tiposModificacion;
                }
            }
            else if (funciones.retornaModulo().Equals("ModificacionCentroAcopio"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["solicitudSession"] = paginas.solicitudModificacionCentroAcopioSession;

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion != null)
                {

                    int[] tiposModificacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

                    int contador = 0;
                    foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                    {
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_RENOVACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_CENTRO_ACOPIO_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_PT_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_CENTRO_ACOPIO_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_CENTRO_ACOPIO_REGULARIZACION;
                            contador++;
                        }
                    }

                    ViewState["SECCION_ESPECIFICA"] = tiposModificacion;
                }
            }
            else if (funciones.retornaModulo().Equals("ModificacionCentroFaenamiento"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["solicitudSession"] = paginas.solicitudModificacionCentroFaenamientoSession;

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion != null)
                {

                    int[] tiposModificacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

                    int contador = 0;
                    foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                    {
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_RENOVACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_PT_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_CENTRO_FAENAMIENTO_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_CENTRO_FAENAMIENTO_REGULARIZACION;
                            contador++;
                        }
                    }

                    ViewState["SECCION_ESPECIFICA"] = tiposModificacion;
                }
            }
            else if (funciones.retornaModulo().Equals("ModificacionECMPO"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["solicitudSession"] = paginas.solicitudModificacionECMPOSession;

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion != null)
                {

                    int[] tiposModificacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

                    int contador = 0;
                    foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                    {
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_AMPLIA_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_ECMPO_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_PT)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_ECMPO_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_ECMPO_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_ECMPO_REGULARIZACION;
                            contador++;
                        }
                    }

                    ViewState["SECCION_ESPECIFICA"] = tiposModificacion;
                }
            }
            else
            {

                ViewState["URL_VER"] = paginas.URL_VER_SOLMOD;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLMOD;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLMOD;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLMOD;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLMOD;
                ViewState["solicitudSession"] = paginas.solicitudModificacionSession;

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion != null)
                {

                    int[] tiposModificacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

                    int contador = 0;
                    foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                    {
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_AMPLIA_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_CONCESION_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_PT)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_CONCESION_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_CONCESION_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_INFORME_CARTOGRAFIA_MOD_CONCESION_REGULARIZACION;
                            contador++;
                        }
                    }

                    ViewState["SECCION_ESPECIFICA"] = tiposModificacion;
                }

            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {

            // PAGE LOAD
            if (!Page.IsPostBack)
            {
                setearModulo();

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (solicitudConcesion == null || usuario_logeado == null)
                {
                    Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
                }


                //OBSERVACIONES
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {
                    Observaciones.ReadOnly = false;
                    PanelAgregarObservaciones.Visible = true;
                    UpdatePanelObservaciones.Update();
                }
                else
                {
                    Observaciones.ReadOnly = true;
                    PanelAgregarObservaciones.Visible = false;
                    UpdatePanelObservaciones.Update();
                }


                IdSolicitud.Text = Convert.ToString(solicitudConcesion.idSolConcesion);

                CargarObservaciones(Convert.ToInt32(IdSolicitud.Text));


            }
        }



        private void CargarObservaciones(int IdSolicitud)
        {
            ObsPestaniaInforme obsPestaniaInforme = obsPestaniaInformeDA.ObtieneObsPestaniaInforme(IdSolicitud, dbTipo);
            if (obsPestaniaInforme != null)
            {
                idObservacion.Value = Convert.ToString(obsPestaniaInforme.idObsPestInf);
                Observaciones.Text = obsPestaniaInforme.observaciones;
                UpdatePanelObservaciones.Update();
            }
        }

        protected void GuardarObservacion_Click(object sender, EventArgs e)
        {

            MensajeObservaciones.Text = "";
            PanelMensajeObservaciones.Visible = false;
            UpdatePanelMensajeObservaciones.Update();

            ObsPestaniaInforme obsPestaniaInforme = new ObsPestaniaInforme(); ;
            if (!idObservacion.Value.Trim().Equals(""))
            {
                obsPestaniaInforme.idObsPestInf = Convert.ToInt32(idObservacion.Value);
            }
            obsPestaniaInforme.idSolConcesion = Convert.ToInt32(IdSolicitud.Text);
            obsPestaniaInforme.observaciones = Observaciones.Text;
            obsPestaniaInforme.tipoGrupo = new ParametroGenerico(dbTipo);

            if (obsPestaniaInformeDA.GuardarObsPestaniaInforme(obsPestaniaInforme))
            {

                idObservacion.Value = Convert.ToString(obsPestaniaInforme.idObsPestInf);
                UpdatePanelObservaciones.Update();

                MensajeObservaciones.Text = "Observacion Actualizada";
                PanelMensajeObservaciones.Visible = true;
                UpdatePanelMensajeObservaciones.Update();

            }
            else
            {

                MensajeObservaciones.Text = "Ha ocurrido un error al guardar la observación";
                PanelMensajeObservaciones.Visible = true;
                UpdatePanelMensajeObservaciones.Update();
            }
        }

    }
}