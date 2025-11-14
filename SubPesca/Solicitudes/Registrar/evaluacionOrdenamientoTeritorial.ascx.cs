using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Utilidades;
using Datos.Contantes;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using SubPesca.Mantenedores.Generales;
using Validaciones.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace SubPesca.Solicitudes.Registrar
{
    public partial class evaluacionOrdenamientoTeritorial : System.Web.UI.UserControl
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema

        public static evaluacionOrdenamientoTeritorial Instance { get; set; }

        Funciones funciones = new Funciones();
        RequerimientoService requerimientoService = new RequerimientoService();
        MantenedorGeneralService mantenedorGeneralService = new MantenedorGeneralService();
        SolicitudCentroColectorService solicitudCentroColectorService = new SolicitudCentroColectorService();
        IngresarDocumentoValidacion ingresarDocumentoValidacion = new IngresarDocumentoValidacion();
        
        GrupoSuspendidoDA grupoSuspendidoDA = new GrupoSuspendidoDA();





        public evaluacionOrdenamientoTeritorial()
        {
            Instance = this;
        }

        public UpdatePanel UpdatePanelTipoPend
        {
            get { return UpdatePanelTipoPendiente; }
            set { UpdatePanelTipoPendiente = value; }
        }

        public void RecargarInformacion()
        {
            cargarCombobox("TipoPendiente");
            UpdatePanelTipoPendiente.Update();
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

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_BANCO_NATURAL_CONCESION };

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
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_BANCO_NATURAL_CREA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_BANCO_NATURAL_FUSIONA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_BANCO_NATURAL_SECTOR_CERO };

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
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_BANCO_NATURAL_CREA_RESA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA_RESA)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_BANCO_NATURAL_FUSIONA_RESA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO_RESA)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_BANCO_NATURAL_SECTOR_CERO_RESA };

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

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_BANCO_NATURAL_ACOPIO };

            }
            else if (funciones.retornaModulo().Equals("Faenamiento"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_CENTRO_DE_FAENAMIENTO;
                ViewState["solicitudSession"] = paginas.solicitudFaenamientoSession;

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_BANCO_NATURAL_FAENAMIENTO };

            }
            else if (funciones.retornaModulo().Equals("Amerb"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_AMERB;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_AMERB;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_AMERB;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_AMERB;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_AMERB;
                ViewState["solicitudSession"] = paginas.solicitudAmerbSession;

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_BANCO_NATURAL_AMERB };

            }
            else if (funciones.retornaModulo().Equals("ExperimentalesAmerb"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesAmerbSession;

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_BANCO_NATURAL_EXPERIMENTALES_AMERB };

            }
            else if (funciones.retornaModulo().Equals("ExperimentalesConcesion"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesConcesionSession;

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_BANCO_NATURAL_EXPERIMENTALES_CONCESION };

            }
            else if (funciones.retornaModulo().Equals("ECMPO"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_ECMPO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_ECMPO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_ECMPO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_ECMPO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_ECMPO;
                ViewState["solicitudSession"] = paginas.solicitudECMPOSession;

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_BANCO_NATURAL_ECMPO };

            }

            else if (funciones.retornaModulo().Equals("Colector"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_COLECTORES_SEMILLA;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_COLECTORES_SEMILLA;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_COLECTORES_SEMILLA;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_COLECTORES_SEMILLA;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_COLECTORES_SEMILLA;
                ViewState["solicitudSession"] = paginas.solicitudColectorSession;

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_BANCO_NATURAL_COLECTOR };

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
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_MOD_AMERB_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_MOD_AMERB_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_PT)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_MOD_AMERB_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_MOD_AMERB_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_MOD_AMERB_REGULARIZACION;
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
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_RENOVACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_MOD_CENTRO_ACOPIO_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_PT_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_MOD_CENTRO_ACOPIO_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_MOD_CENTRO_ACOPIO_REGULARIZACION;
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
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_RENOVACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_PT_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_MOD_CENTRO_FAENAMIENTO_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_MOD_CENTRO_FAENAMIENTO_REGULARIZACION;
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
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_MOD_ECMPO_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_PT)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_MOD_ECMPO_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_MOD_ECMPO_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_MOD_ECMPO_REGULARIZACION;
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
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_BANCO_NATURAL_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_BANCO_NATURAL_MOD_CONCESION_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_PT)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_BANCO_NATURAL_MOD_CONCESION_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_BANCO_NATURAL_MOD_CONCESION_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.OBSERVACIONES_DIFUSION_BANCO_NATURAL_MOD_CONCESION_REGULARIZACION;
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
                
                inicializarFormulario();

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                if (solicitudConcesion != null) {
                    IdSolicitud.Text = Convert.ToString(solicitudConcesion.idSolConcesion);

                    cargarFormularioEvaluacionOrdenamientoTerritorial();
                }
           }
        }

        private void inicializarFormulario()
        {
            cargarCombobox("RequiereIT_UOT");
            cargarCombobox("Resultado");
            cargarCombobox("TipoPendiente");
            cargarCombobox("TipoSupeditado");
        }

        private void cargarFormularioEvaluacionOrdenamientoTerritorial()
        {
            /* Obtener evaluación UOT */
            EvaluacionUOT_UE evaluacionUOT_UE = grupoSuspendidoDA.ObtieneEvaluacionUOT_UE(0,Convert.ToInt32(IdSolicitud.Text));
            if (evaluacionUOT_UE != null)
            {

                IdEvaluacionUOT.Text = Convert.ToString(evaluacionUOT_UE.idEvaluacionUOT);

                /* Cargar si requiere UOT */
                RequiereIT_UOT.SelectedValue = Convert.ToString(evaluacionUOT_UE.requiereIT_UOT.id);

                if (evaluacionUOT_UE.requiereIT_UOT != null && evaluacionUOT_UE.requiereIT_UOT.id != rbEstadosGenerales.NO)
                {
                    if (evaluacionUOT_UE.estadoResultado != null && evaluacionUOT_UE.estadoResultado.id > 0)
                    {
                        Resultado.SelectedValue = Convert.ToString(evaluacionUOT_UE.estadoResultado.id);
                        PanelResultado.Visible = true;
                    }
                    UpdatePanelEvalUnidOrdenamTerr.Update();


                    if (evaluacionUOT_UE.estadoResultado != null && (evaluacionUOT_UE.estadoResultado.id == rbEstadosGenerales.PENDIENTE || evaluacionUOT_UE.estadoResultado.id == rbEstadosGenerales.RECHAZA))
                    {
                        /* Obtener tipo de pendiente */
                        List<AsocGrupoSolicitud> asocGrupoSolicitudList = grupoSuspendidoDA.ListarAsocGrupoSolicitudFiltro(0, Convert.ToInt32(IdSolicitud.Text), rbEstadosGenerales.VIGENTE, 0, evaluacionUOT_UE.idEvaluacionUOT);

                        if (asocGrupoSolicitudList != null && asocGrupoSolicitudList.Count > 0)
                        {
                            ViewState["AsocGrupoSolicitudList"] = asocGrupoSolicitudList;

                            GridPendientes.DataSource = asocGrupoSolicitudList;
                            GridPendientes.DataBind();
                            PanelPendiente.Visible = true;
                            UpdatePanelGrillaPendientes.Update();
                        }
                    }

                    if (evaluacionUOT_UE.estadoResultado != null && (evaluacionUOT_UE.estadoResultado.id == rbEstadosGenerales.SUPEDITADA || evaluacionUOT_UE.estadoResultado.id == rbEstadosGenerales.PENDIENTE || evaluacionUOT_UE.estadoResultado.id == rbEstadosGenerales.RECHAZA))
                    {
                        /* Obtener tipo de supeditado */
                        List<DependenciaSupeditados> dependenciaSupeditadosList = grupoSuspendidoDA.ListarDependenciaSupeditadosFiltro(0, 0, evaluacionUOT_UE.idEvaluacionUOT, 0);

                        if (dependenciaSupeditadosList != null && dependenciaSupeditadosList.Count > 0)
                        {
                            ViewState["DependenciaSupeditadosList"] = dependenciaSupeditadosList;

                            GridSupeditados.DataSource = dependenciaSupeditadosList;
                            GridSupeditados.DataBind();
                            PanelSupeditado.Visible = true;
                            UpdatePanelGrillaSupeditados.Update();
                        }
                    }
                }
            }
        }
        
        private void cargarCombobox(string combobox)
        {
            ParametroGenerico parametroGenericoFiltro = new ParametroGenerico();
            switch(combobox){

                case "Resultado":
                    
                    Resultado.Items.Clear();
                    Resultado.DataSource = requerimientoService.ListarPosiblesRespuestasSubRequerimiento(109);
                    Resultado.DataTextField = "nombreEstado";
                    Resultado.DataValueField = "idEstado";

                    Resultado.DataBind();
                    Resultado.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

                case "RequiereIT_UOT":

                    RequiereIT_UOT.Items.Clear();
                    RequiereIT_UOT.DataBind();
                    RequiereIT_UOT.Items.Insert(0, new ListItem("Si", Convert.ToString(rbEstadosGenerales.SI)));
                    RequiereIT_UOT.Items.Insert(0, new ListItem("No", Convert.ToString(rbEstadosGenerales.NO)));
                    RequiereIT_UOT.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

                case "TipoSupeditado":

                    TipoSupeditado.Items.Clear();
                    TipoSupeditado.DataBind();

                    
                    parametroGenericoFiltro.clave = "SUPEDITADO_TIPO";
                    
                    TipoSupeditado.DataSource = mantenedorGeneralService.listarParametroGenerico(parametroGenericoFiltro);
                    TipoSupeditado.DataTextField = "descripcion";
                    TipoSupeditado.DataValueField = "id";
                    TipoSupeditado.DataBind();
                    TipoSupeditado.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

                case "TipoPendiente":
                    TipoPendiente.Items.Clear();
                    TipoPendiente.DataBind();
                    TipoPendiente.DataSource = grupoSuspendidoDA.ListarGrupoSuspendido(0,0,0,null);
                    TipoPendiente.DataTextField = "nombreGrupoSuspend";
                    TipoPendiente.DataValueField = "idGrupoSuspend";
                    TipoPendiente.DataBind();
                    TipoPendiente.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;
            }
        }

        protected void GridPendientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //ACCION
                HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
                if (hidden_accion != null && !hidden_accion.Value.Equals("") && (Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR || Convert.ToInt32(hidden_accion.Value) == accion.IGNORAR))
                {
                    e.Row.Attributes["style"] = "display:none";
                };

                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar el tipo de pendiente?')");
                    boton_eliminar.Visible = true;
                };

            };
        }

        protected void GridPendientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;

            PanelMensajeEvalUnidOrdenamTerr.Visible = false;
            UpdatePanelMensajeEvalUnidOrdenamTerr.Update();


            switch (e.CommandName)
            {
                case "Eliminar":
                    index = Convert.ToInt32(e.CommandArgument);
                    GridPendientes.EditIndex = -1;
                    DeletePendientes(index);
                    break;
            };
        }

        private void DeletePendientes(int index)
        {
            List<AsocGrupoSolicitud> asocGrupoSolicitudList = (List<AsocGrupoSolicitud>)ViewState["AsocGrupoSolicitudList"];

            foreach (AsocGrupoSolicitud asocGrupoSolicitud in asocGrupoSolicitudList)
            {

                if (asocGrupoSolicitud != null && asocGrupoSolicitud.index == index)
                {

                    List<String> listaErroresGrupo = new List<string>(); // Valida que la eliminación sea correcta y no deje inconsistencias.

                    if (listaErroresGrupo != null && listaErroresGrupo.Count <= 0)
                    {
                        if (asocGrupoSolicitud.accion == accion.INGRESAR)
                        {
                            asocGrupoSolicitud.accion = accion.IGNORAR;
                            GridPendientes.Rows[index].Attributes["style"] = "display:none";
                        }
                        if (asocGrupoSolicitud.accion == accion.LISTADO)
                        {
                            asocGrupoSolicitud.accion = accion.ELIMINAR;
                            GridPendientes.Rows[index].Attributes["style"] = "display:none";
                        }
                    }
                    else {
                        foreach (String error in listaErroresGrupo)
                        {
                            Page.Validators.Add(new ValidationError("grupo1", error));
                        }
                        
                    }
                }
            }

            GridPendientes.DataSource = asocGrupoSolicitudList;
            GridPendientes.DataBind();
            GridPendientes.Visible = true;

            ViewState["AsocGrupoSolicitudList"] = (List <AsocGrupoSolicitud>) asocGrupoSolicitudList;

        }

        protected void GridSupeditados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //ACCION
                HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
                if (hidden_accion != null && !hidden_accion.Value.Equals("") && (Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR || Convert.ToInt32(hidden_accion.Value) == accion.IGNORAR))
                {
                    e.Row.Attributes["style"] = "display:none";
                };
                
                // Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar el tipo de supeditado?')");
                    boton_eliminar.Visible = true;
                };
            };
        }

        protected void GridSupeditados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;

            PanelMensajeEvalUnidOrdenamTerr.Visible = false;
            UpdatePanelMensajeEvalUnidOrdenamTerr.Update();


            switch (e.CommandName)
            {
                case "Eliminar":
                    index = Convert.ToInt32(e.CommandArgument);
                    GridSupeditados.EditIndex = -1;
                    DeleteSupeditados(index);
                    ocultarFormularioSupeditado();
                    break;
            };
        }

        private void DeleteSupeditados(int index)
        {
            List<DependenciaSupeditados> dependenciaSupeditadosList = (List<DependenciaSupeditados>)ViewState["DependenciaSupeditadosList"];

            foreach (DependenciaSupeditados dependenciaSupeditados in dependenciaSupeditadosList)
            {
                if (dependenciaSupeditados != null && dependenciaSupeditados.index == index)
                {
                    List<String> listaErroresSupeditado = new List<string>(); // Valida que la eliminación sea correcta y no deje inconsistencias.

                    if (listaErroresSupeditado != null && listaErroresSupeditado.Count <= 0)
                    {
                        if (dependenciaSupeditados.accion == accion.INGRESAR)
                        {
                            dependenciaSupeditados.accion = accion.IGNORAR;
                            GridSupeditados.Rows[index].Attributes["style"] = "display:none";
                        }
                        if (dependenciaSupeditados.accion == accion.LISTADO)
                        {
                            dependenciaSupeditados.accion = accion.ELIMINAR;
                            GridSupeditados.Rows[index].Attributes["style"] = "display:none";
                        }
                    }
                    else
                    {
                        foreach (String error in listaErroresSupeditado)
                        {
                            Page.Validators.Add(new ValidationError("grupo1", error));
                        }

                    }
                }
            }

            GridSupeditados.DataSource = dependenciaSupeditadosList;
            GridSupeditados.DataBind();
            GridSupeditados.Visible = true;

            ViewState["DependenciaSupeditadosList"] = (List<DependenciaSupeditados>)dependenciaSupeditadosList;
        }
        
        protected void GuardarSupeditado_Click(object sender, ImageClickEventArgs e)
        {
            List<DependenciaSupeditados> dependenciaSupeditadosList = (List<DependenciaSupeditados>)ViewState["DependenciaSupeditadosList"];
            int index = 0;

            if (dependenciaSupeditadosList == null)
            {
                dependenciaSupeditadosList = new List<DependenciaSupeditados>();
            }
            else
            {
                index = dependenciaSupeditadosList.Count;
            }

            if (Convert.ToInt32(Resultado.SelectedItem.Value) == rbEstadosGenerales.SUPEDITADA)
            {
                if (Convert.ToInt32(TipoSupeditado.SelectedItem.Value) < 1)
                {
                    Page.Validators.Add(new ValidationError("grupo1", "Seleccione Tipo Supeditado."));
                }
            }

            if (Page.IsValid)
            {
                DependenciaSupeditados dependenciaSupeditados = new DependenciaSupeditados();
                dependenciaSupeditados.tipoSupeditado = new ParametroGenerico();
                dependenciaSupeditados.tipoSupeditado.id = Convert.ToInt32(TipoSupeditado.SelectedItem.Value);
                dependenciaSupeditados.tipoSupeditado.descripcion = TipoSupeditado.SelectedItem.Text;

                dependenciaSupeditados.solicitudConcesionDep = new SolicitudConcesion();
                dependenciaSupeditados.solicitudConcesionDep.numPert = Pert.Text;

                dependenciaSupeditados.observaciones = Observaciones.Text;

                List<String> listaErroresDependenciaSupeditados = ingresarDocumentoValidacion.validaIngresoTipoSupeditado(dependenciaSupeditados, dependenciaSupeditadosList);
                bool validaSupeditadoOk = false;

                if (listaErroresDependenciaSupeditados != null && listaErroresDependenciaSupeditados.Count <= 0)
                {
                    dependenciaSupeditados.accion = accion.INGRESAR;
                    dependenciaSupeditados.index = Convert.ToInt32(index);

                    dependenciaSupeditadosList.Add(dependenciaSupeditados);
                    validaSupeditadoOk = true;
                    index++;

                    if (validaSupeditadoOk)
                    {
                        GridSupeditados.DataSource = dependenciaSupeditadosList;
                        GridSupeditados.DataBind();

                        PanelGrillaSupeditados.Visible = true;
                        UpdatePanelGrillaSupeditados.Update();

                        ViewState["DependenciaSupeditadosList"] = (List<DependenciaSupeditados>)dependenciaSupeditadosList;
                    }

                }
                else
                {
                    foreach (String error in listaErroresDependenciaSupeditados)
                    {
                        Page.Validators.Add(new ValidationError("grupo1", error));
                    }

                }

                TipoSupeditado.SelectedValue = "-1";
                Pert.Text = "";
                Observaciones.Text = "";

                ocultarFormularioSupeditado();
            }

            PanelMensajesValidaciones.Visible = true;
            UpdatePanelMensajesValidaciones.Update();
        }

        protected void GuardarPendiente_Click(object sender, ImageClickEventArgs e)
        {
            List<AsocGrupoSolicitud> asocGrupoSolicitudList = (List<AsocGrupoSolicitud>)ViewState["AsocGrupoSolicitudList"];
            int index = 0;

            if (asocGrupoSolicitudList == null)
            {
                asocGrupoSolicitudList = new List<AsocGrupoSolicitud>();
            }
            else
            {
                index = asocGrupoSolicitudList.Count;
            }

            if (Convert.ToInt32(Resultado.SelectedItem.Value) == rbEstadosGenerales.PENDIENTE)
            {
                if (Convert.ToInt32(TipoPendiente.SelectedItem.Value) < 1)
                {
                    Page.Validators.Add(new ValidationError("grupo1", "Seleccione Tipo Pendiente."));
                }
            }

            if (Page.IsValid)
            {
                AsocGrupoSolicitud asocGrupoSolicitud = new AsocGrupoSolicitud();
                asocGrupoSolicitud.solicitudConcesion = new SolicitudConcesion();

                asocGrupoSolicitud.grupoSuspendido = new GrupoSuspendidos();
                asocGrupoSolicitud.grupoSuspendido.idGrupoSuspend = Convert.ToInt32(TipoPendiente.SelectedItem.Value);
                asocGrupoSolicitud.grupoSuspendido.nombreGrupoSuspend = TipoPendiente.SelectedItem.Text;

                asocGrupoSolicitud.estadoVigencia = new ParametroGenerico();
                asocGrupoSolicitud.estadoVigencia.id = rbEstadosGenerales.VIGENTE;

                asocGrupoSolicitud.idResultadoIT_UOT = Convert.ToInt32(Resultado.SelectedItem.Value);

                List<String> listaErroresAsocGrupoSolicitud = ingresarDocumentoValidacion.validaIngresoTipoPendiente(asocGrupoSolicitud, asocGrupoSolicitudList);

                bool validaGrupoOk = false;

                if (listaErroresAsocGrupoSolicitud != null && listaErroresAsocGrupoSolicitud.Count <= 0)
                {
                    asocGrupoSolicitud.index = Convert.ToInt32(index);
                    asocGrupoSolicitud.accion = accion.INGRESAR;

                    asocGrupoSolicitudList.Add(asocGrupoSolicitud);
                    validaGrupoOk = true;
                    index++;

                    if (validaGrupoOk)
                    {
                        GridPendientes.DataSource = asocGrupoSolicitudList;
                        GridPendientes.DataBind();

                        PanelGrillaPendientes.Visible = true;
                        UpdatePanelGrillaPendientes.Update();

                        ViewState["AsocGrupoSolicitudList"] = (List<AsocGrupoSolicitud>)asocGrupoSolicitudList;

                    }
                }
                else
                {
                    foreach (String error in listaErroresAsocGrupoSolicitud)
                    {
                        Page.Validators.Add(new ValidationError("grupo1", error));
                    }
                }

                TipoPendiente.SelectedValue = "-1";
            }

            PanelMensajesValidaciones.Visible = true;
            UpdatePanelMensajesValidaciones.Update();
        }


        
        protected void Resultado_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarPorTipoResultado();
            
            int resultado = Convert.ToInt32(Resultado.SelectedItem.Value);
            if (resultado == rbEstadosGenerales.RECHAZA || resultado == rbEstadosGenerales.PENDIENTE)
            {
                PanelPendiente.Visible = true;

            }
            else {

                PanelPendiente.Visible = false;
            }

            if (resultado == rbEstadosGenerales.SUPEDITADA || resultado == rbEstadosGenerales.RECHAZA || resultado == rbEstadosGenerales.PENDIENTE)
            {
                PanelSupeditado.Visible = true;

            }
            else {

                PanelSupeditado.Visible = false;
            }
        }

        private void LimpiarPorTipoResultado()
        {
            TipoPendiente.SelectedValue = "-1";
            TipoSupeditado.SelectedValue = "-1";
            Pert.Text = "";
            Observaciones.Text = "";

            PanelPendiente.Visible = false;
            PanelGrillaPendientes.Visible = false;
            
            
            PanelSupeditado.Visible = false;
            PanelGrillaSupeditados.Visible = false;

            ViewState["AsocGrupoSolicitudList"] = null;
            GridPendientes_CargaGrilla();

            ViewState["DependenciaSupeditadosList"] = null;
            GridSupeditados_CargaGrilla();

            
            UpdatePanelGrillaPendientes.Update();
            
            UpdatePanelGrillaSupeditados.Update();
        }

        private void ocultarFormularioSupeditadoYPendiente()
        {
            PanelPendiente.Visible = false;
            PanelSupeditado.Visible = false;
        }

        protected void TipoSupeditado_SelectedIndexChanged(object sender, EventArgs e)
        {
            ocultarFormularioSupeditado();
            int tipoSupeditado = Convert.ToInt32(TipoSupeditado.SelectedItem.Value);
            if (tipoSupeditado == rbTipo.SUPEDITADO_SOLICITUD_CONCESION)
            {
                PanelPert.Visible = true;
                PanelObservaciones.Visible = false;
            }
            if (tipoSupeditado == rbTipo.SUPEDITADO_PLANOS_ORIGINALES)
            {
                PanelPert.Visible = false;
                PanelObservaciones.Visible = true;
            }
            if (tipoSupeditado == rbTipo.SUPEDITADO_FALTA_DE_ANTECEDENTES)
            {
                PanelPert.Visible = false;
                PanelObservaciones.Visible = true;
            }
            if (tipoSupeditado == rbTipo.SUPEDITADO_ENVIO_CERTIF_CAPITANIA_PUERTO)
            {
                PanelPert.Visible = false;
                PanelObservaciones.Visible = true;
            }
            if (tipoSupeditado == rbTipo.SUPEDITADO_SOLICITUD_RELOCALIZACION_LEY_RESA)
            {
                PanelPert.Visible = true;
                PanelObservaciones.Visible = false;
            }
            if (tipoSupeditado == rbTipo.SUPEDITADO_AMERB)
            {
                PanelPert.Visible = false;
                PanelObservaciones.Visible = true;
            }
            if (tipoSupeditado == rbTipo.SUPEDITADO_SOLICITUD_AMERB)
            {
                PanelPert.Visible = true;
                PanelObservaciones.Visible = false;
            }
            if (tipoSupeditado == rbTipo.SUPEDITADO_SOLICITUD_ECMPO)
            {
                PanelPert.Visible = true;
                PanelObservaciones.Visible = false;
            }
            
        }

        private void ocultarFormularioSupeditado()
        {
            PanelPert.Visible = false;
            PanelObservaciones.Visible = false;
        }

        protected void RequiereIT_UOT_SelectedIndexChanged(object sender, EventArgs e)
        {
            int requiereIT_UOT = Convert.ToInt32(RequiereIT_UOT.SelectedItem.Value);

            if (requiereIT_UOT == rbEstadosGenerales.SI)
            {
                PanelResultado.Visible = true;
            }
            else {
                PanelResultado.Visible = false;
                LimpiarPorRequiereIT_UOT();
            }
        }

        private void LimpiarPorRequiereIT_UOT()
        {
            Resultado.SelectedValue = "-1";
            TipoPendiente.SelectedValue = "-1";
            TipoSupeditado.SelectedValue = "-1";
            Pert.Text = "";
            Observaciones.Text = "";

            PanelResultado.Visible = false;

            PanelPendiente.Visible = false;
            PanelGrillaPendientes.Visible = false;
            
            PanelSupeditado.Visible = false;
            PanelGrillaSupeditados.Visible = false;

            ViewState["AsocGrupoSolicitudList"] = null;
            GridPendientes_CargaGrilla();

            ViewState["DependenciaSupeditadosList"] = null;
            GridSupeditados_CargaGrilla();

            UpdatePanelGrillaPendientes.Update();
            
            UpdatePanelGrillaSupeditados.Update();
        }

        private void GridPendientes_CargaGrilla()
        {
            List<AsocGrupoSolicitud> List_AsocGrupoSolicitud = (List<AsocGrupoSolicitud>)ViewState["AsocGrupoSolicitudList"];


            if (List_AsocGrupoSolicitud == null)
            {
                List_AsocGrupoSolicitud = new List<AsocGrupoSolicitud>();
            }

            GridSupeditados.DataSource = List_AsocGrupoSolicitud;
            GridSupeditados.DataBind();


            ViewState["AsocGrupoSolicitudList"] = (List<AsocGrupoSolicitud>)List_AsocGrupoSolicitud;
        }

        private void GridSupeditados_CargaGrilla()
        {
            List<DependenciaSupeditados> List_DependenciaSupeditados = (List<DependenciaSupeditados>)ViewState["DependenciaSupeditadosList"];


            if (List_DependenciaSupeditados == null)
            {
                List_DependenciaSupeditados = new List<DependenciaSupeditados>();
            }

            GridPendientes.DataSource = List_DependenciaSupeditados;
            GridPendientes.DataBind();


            ViewState["DependenciaSupeditadosList"] = (List<DependenciaSupeditados>)List_DependenciaSupeditados;
        }

        protected void GuardarEvaluacion_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(RequiereIT_UOT.SelectedItem.Value) == rbEstadosGenerales.SI)
            {
                if (Convert.ToInt32(Resultado.SelectedItem.Value) < 1)
                {

                    Page.Validators.Add(new ValidationError("grupo1", "Debe ingresar Resultado."));
                }
            }
            
            if (Page.IsValid)
            {

                EvaluacionUOT_UE evaluacionUOT_UE = new Datos.Entidades.EvaluacionUOT_UE();

                if (IdEvaluacionUOT.Text != null && !IdEvaluacionUOT.Text.Equals(""))
                {
                    evaluacionUOT_UE.idEvaluacionUOT = Convert.ToInt32(IdEvaluacionUOT.Text);
                }

                evaluacionUOT_UE.requiereIT_UOT = new ParametroGenerico();
                evaluacionUOT_UE.requiereIT_UOT.id = Convert.ToInt32(RequiereIT_UOT.SelectedItem.Value);

                evaluacionUOT_UE.solicitudConcesion = new SolicitudConcesion();
                evaluacionUOT_UE.solicitudConcesion.idSolConcesion = Convert.ToInt32(IdSolicitud.Text);

                if (Resultado.SelectedItem != null)
                {
                    evaluacionUOT_UE.estadoResultado = new ParametroGenerico();
                    evaluacionUOT_UE.estadoResultado.id = Convert.ToInt32(Resultado.SelectedItem.Value);
                }

                /* Agregar Supeditado */
                List<DependenciaSupeditados> dependenciaSupeditadosList = (List<DependenciaSupeditados>)ViewState["DependenciaSupeditadosList"];
                if (dependenciaSupeditadosList != null && dependenciaSupeditadosList.Count > 0)
                {
                    evaluacionUOT_UE.dependenciaSupeditadosList = dependenciaSupeditadosList;
                }
                /**************************************/


                /* Agregar Pendiente */
                List<AsocGrupoSolicitud> asocGrupoSolicitudList = (List<AsocGrupoSolicitud>)ViewState["AsocGrupoSolicitudList"];
                if (asocGrupoSolicitudList != null && asocGrupoSolicitudList.Count > 0)
                {
                    evaluacionUOT_UE.asocGrupoSolicitudList = asocGrupoSolicitudList;
                }
                /**************************************/

                List<String> listaErroresEvaluacion = ingresarDocumentoValidacion.validarEvaluacionOrdenamientoTerr(evaluacionUOT_UE);

                if (listaErroresEvaluacion != null && listaErroresEvaluacion.Count <= 0)
                {
                    bool resp = solicitudCentroColectorService.guardarEvaluacionOrdenamientoTerr(evaluacionUOT_UE);
                    if (resp)
                    {
                        MensajeEvalUnidOrdenamTerr.Text = "Se ha guardado la Evaluación de Ordenamiento Territorial exitosamente.";
                        PanelMensajeEvalUnidOrdenamTerr.Visible = true;

                        IdEvaluacionUOT.Text = Convert.ToString(evaluacionUOT_UE.idEvaluacionUOT);

                        //RECARGANDO LA INFORMACION DE LA SOLICITUD, POR SI CAMBIO EL ESTADO
                        UpdatePanel UpdatePanelInformacionSolictud = informacionSolicitud.Instance.UpdatePanelInfo;
                        informacionSolicitud.Instance.RecargarInformacion();
                        UpdatePanelInformacionSolictud.Update();

                    }
                    else
                    {

                        MensajeEvalUnidOrdenamTerr.Text = "No se ha guardado la Evaluación de Ordenamiento Territorial.";
                        PanelMensajeEvalUnidOrdenamTerr.Visible = true;
                    }

                    UpdatePanelMensajeEvalUnidOrdenamTerr.Update();

                }
                else
                {
                    foreach (String error in listaErroresEvaluacion)
                    {
                        Page.Validators.Add(new ValidationError("grupo1", error));
                    }

                }
            }

            PanelMensajesValidaciones.Visible = true;
            UpdatePanelMensajesValidaciones.Update();
        }

    }
}