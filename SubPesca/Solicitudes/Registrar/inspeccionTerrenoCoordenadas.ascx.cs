using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Entidades;
using Validaciones.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using Datos.Contantes;
using Datos.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;

namespace SubPesca.Solicitudes.Registrar
{
    public partial class inspeccionTerrenoCoordenadas : System.Web.UI.UserControl
    {

        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        TipoDA tipoDa = new TipoDA();
        DatumDA datumDA = new DatumDA();
        VerticeDA verticeDA = new VerticeDA();
        CoordenadaGeograficaDA coordenadaGeograficaDA = new CoordenadaGeograficaDA();
        PoligonoDA poligonoDA = new PoligonoDA();
        SolicitudDA solicitudDA = new SolicitudDA();

        AntecedDelSectorValidacion antecedDelSectorValidacion = new AntecedDelSectorValidacion();
        PermisosService permisosService = new PermisosService(); 
        Funciones funciones = new Funciones();
     


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

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_CONCESION };
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
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_RELOCALIZACION_CREA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_RELOCALIZACION_FUSIONA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_RELOCALIZACION_SECTOR_CERO };
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
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_RELOCALIZACION_CREA_RESA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA_RESA)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_RELOCALIZACION_FUSIONA_RESA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO_RESA)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_RELOCALIZACION_SECTOR_CERO_RESA };
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

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_ACOPIO };

            }
            else if (funciones.retornaModulo().Equals("Faenamiento"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_CENTRO_DE_FAENAMIENTO;
                ViewState["solicitudSession"] = paginas.solicitudFaenamientoSession;

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_FAENAMIENTO };

            }
            else if (funciones.retornaModulo().Equals("Amerb"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_AMERB;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_AMERB;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_AMERB;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_AMERB;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_AMERB;
                ViewState["solicitudSession"] = paginas.solicitudAmerbSession;

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_AMERB };


            }
            else if (funciones.retornaModulo().Equals("ExperimentalesAmerb"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesAmerbSession;

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_EXPERIMENTALES_AMERB };


            }
            else if (funciones.retornaModulo().Equals("ExperimentalesConcesion"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesConcesionSession;

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_EXPERIMENTALES_CONCESION };


            }
            else if (funciones.retornaModulo().Equals("ECMPO"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_ECMPO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_ECMPO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_ECMPO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_ECMPO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_ECMPO;
                ViewState["solicitudSession"] = paginas.solicitudECMPOSession;

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_ECMPO };


            }
            else if (funciones.retornaModulo().Equals("Colector"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_COLECTORES_SEMILLA;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_COLECTORES_SEMILLA;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_COLECTORES_SEMILLA;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_COLECTORES_SEMILLA;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_COLECTORES_SEMILLA;
                ViewState["solicitudSession"] = paginas.solicitudColectorSession;

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_COLECTOR };

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
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_AMERB_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_AMERB_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_PT)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_AMERB_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_AMERB_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_AMERB_REGULARIZACION;
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
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_RENOVACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_CENTRO_ACOPIO_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_PT_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_CENTRO_ACOPIO_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_CENTRO_ACOPIO_REGULARIZACION;
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
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_RENOVACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_PT_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_CENTRO_FAENAMIENTO_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_CENTRO_FAENAMIENTO_REGULARIZACION;
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
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_ECMPO_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_PT)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_ECMPO_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_ECMPO_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_ECMPO_REGULARIZACION;
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
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_CONCESION_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_PT)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_CONCESION_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_CONCESION_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.COORDENADAS_INSPECCION_TERRENO_MOD_CONCESION_REGULARIZACION;
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

                // Inicializamos el formulario
                Initialize_Form();

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (solicitudConcesion == null || usuario_logeado == null)
                {
                    Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
                }


                //BOTON DE INGRESO O MODIFICACION
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {
                    FormularioIngreso.Visible = true;
                }
                else
                {
                    FormularioIngreso.Visible = false;
                    
                }
            }
        }

        private void Initialize_Form()
        {
            SolicitudConcesion solicitudInicial = (SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

            if (solicitudInicial != null && solicitudInicial.idSolConcesion > 0)
            {
                IdSolicitud.Value = Convert.ToString(solicitudInicial.idSolConcesion);

                Initialize_Comboboxs();
                Initialize_formulario();

            }
            else
            {
                Response.Redirect(ViewState["URL_ERROR"].ToString());
            }
             
        }

        private void Initialize_formulario()
        {

            SolicitudConcesion solicitudConcesion = solicitudDA.ObtieneSolicitudConcesion(Convert.ToInt32(IdSolicitud.Value), ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
            if (solicitudConcesion != null)
            {
                despliegaCoordenadaGeograficaAntTerreno(solicitudConcesion);
            }
            else
            {
                Response.Redirect(ViewState["URL_ERROR"].ToString());
            }
        }

        private void despliegaCoordenadaGeograficaAntTerreno(SolicitudConcesion solicitudConcesion)
        {
            if (solicitudConcesion != null)
            {
                Carga_CamposCoordenadaGeograficaAntTerreno("InspeccionTerrenoCoordenadas");
            }
        }

        private void Carga_CamposCoordenadaGeograficaAntTerreno(string pestania)
        {
            List<CoordenadaGeografica> listaCoordenadas = coordenadaGeograficaDA.ListarCoordenadaGeografica(Convert.ToInt32(IdSolicitud.Value), 0);

            switch (pestania)
            {
                case "InspeccionTerrenoCoordenadas":

                    if (listaCoordenadas != null && listaCoordenadas.Count > 0)
                    {
                        foreach (CoordenadaGeografica coordenadaGeografica in listaCoordenadas)
                        {
                            if (coordenadaGeografica.tipoCoordgeografica != null && coordenadaGeografica.tipoCoordgeografica.id == rbTipo.INSPECCION_TERRENO)
                            {
                                /*
                                IdCoordenadaGeo.Value = Convert.ToString(coordenadaGeografica.idCoordenadaGeo);

                                if (!IdCoordenadaGeo.Value.Equals("") && Convert.ToInt32(IdCoordenadaGeo.Value) > 0)
                                {

                                    /*List<Poligono> listaPoligonos = poligonoDA.ListarPoligono(Convert.ToInt32(IdCoordenadaGeo.Value), 0);

                                    GridPoligonosInspeccionTerrenoCoordenadas.DataSource = listaPoligonos;

                                    List<ComparacionPoligono> listComparacionPoligono = poligonoDA.listarComparacionPoligono(Convert.ToInt32(IdSolicitud.Value),0,0);

                                    GridPoligonosInspeccionTerrenoCoordenadas.DataSource = listComparacionPoligono;
                                    GridPoligonosInspeccionTerrenoCoordenadas.DataBind();
                                    
                                }*/

                                IdCoordenadaGeo.Value = Convert.ToString("0");

                                List<ComparacionPoligono> listComparacionPoligono = poligonoDA.listarComparacionPoligono(Convert.ToInt32(IdSolicitud.Value), 0, 0);

                                GridPoligonosInspeccionTerrenoCoordenadas.DataSource = listComparacionPoligono;
                                GridPoligonosInspeccionTerrenoCoordenadas.DataBind();

                                break;
                            }
                            else
                            {
                                IdCoordenadaGeo.Value = Convert.ToString("0");
                            }
                        }
                    }
                    else
                    {
                        IdCoordenadaGeo.Value = Convert.ToString("0");
                    }

                    break;
            }
        }

        private void Initialize_Comboboxs()
        {
            Carga_Combobox("DATUMinspeccionTerrenoCoordenadas");
            DATUMinspeccionTerrenoCoordenadas.SelectedValue = "-1";

            Carga_Combobox("HusoHorarioinspeccionTerrenoCoordenadas");
            HusoHorarioinspeccionTerrenoCoordenadas.SelectedValue = "-1";

            Carga_Combobox("CoordenadaGeoinspeccionTerrenoCoordenadas");
            CoordenadaGeoinspeccionTerrenoCoordenadas.SelectedValue = "-1";

            Carga_Combobox("VerticeInspeccionTerrenoCoordenadas");
            VerticeInspeccionTerrenoCoordenadas.SelectedValue = "-1";
        }

        private void Carga_Combobox(string combobox)
        {
            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

            switch (combobox)
            { 
                case "HusoHorarioinspeccionTerrenoCoordenadas":
                    HusoHorarioinspeccionTerrenoCoordenadas.Items.Clear();
                    HusoHorarioinspeccionTerrenoCoordenadas.DataSource = coordenadaGeograficaDA.ListaHuso(0);
                    HusoHorarioinspeccionTerrenoCoordenadas.DataTextField = "descripcion";
                    HusoHorarioinspeccionTerrenoCoordenadas.DataValueField = "id";
                    HusoHorarioinspeccionTerrenoCoordenadas.DataBind();
                    HusoHorarioinspeccionTerrenoCoordenadas.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "DATUMinspeccionTerrenoCoordenadas":
                    DATUMinspeccionTerrenoCoordenadas.Items.Clear();
                    DATUMinspeccionTerrenoCoordenadas.DataSource = datumDA.ListaDatum(0);
                    DATUMinspeccionTerrenoCoordenadas.DataTextField = "descripcion";
                    DATUMinspeccionTerrenoCoordenadas.DataValueField = "id";
                    DATUMinspeccionTerrenoCoordenadas.DataBind();
                    DATUMinspeccionTerrenoCoordenadas.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "CoordenadaGeoinspeccionTerrenoCoordenadas":
                    CoordenadaGeoinspeccionTerrenoCoordenadas.Items.Clear();
                    CoordenadaGeoinspeccionTerrenoCoordenadas.DataSource = poligonoDA.ListarPoligonoAplicaBco(Convert.ToInt32(IdSolicitud.Value));
                    CoordenadaGeoinspeccionTerrenoCoordenadas.DataTextField = "poligonoAplicaBcoString";
                    CoordenadaGeoinspeccionTerrenoCoordenadas.DataValueField = "idPoligono";
                    CoordenadaGeoinspeccionTerrenoCoordenadas.DataBind();
                    CoordenadaGeoinspeccionTerrenoCoordenadas.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "VerticeInspeccionTerrenoCoordenadas":
                    VerticeInspeccionTerrenoCoordenadas.Items.Clear();
                    VerticeInspeccionTerrenoCoordenadas.DataSource = tipoDa.ListarTipo("TIPO_VERTICE");
                    VerticeInspeccionTerrenoCoordenadas.DataTextField = "descripcion";
                    VerticeInspeccionTerrenoCoordenadas.DataValueField = "id";
                    VerticeInspeccionTerrenoCoordenadas.DataBind();
                    VerticeInspeccionTerrenoCoordenadas.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

            }
        }
        

        /**
         * Método que guarda los Vertices de Inspección de Terreno Coordenadas.
         */
        protected void GuardarVerticeInspeccionTerrenoCoordenadas_Click(object sender, ImageClickEventArgs e)
        {
            AgregarGrillaVertice("InspeccionTerrenoCoordenadas");
            CargaGrillaVertice("InspeccionTerrenoCoordenadas");
        }

        private void AgregarGrillaVertice(string nombrePestania)
        {
            int index = 0;
            switch (nombrePestania)
            {

                case "InspeccionTerrenoCoordenadas":

                    List<Vertice> List_VerticesInspeccionTerrenoCoord = (List<Vertice>)ViewState["Vertices"];

                    index = 0;
                    if (List_VerticesInspeccionTerrenoCoord == null)
                    {
                        List_VerticesInspeccionTerrenoCoord = new List<Vertice>();
                    }
                    else
                    {
                        int indexAux = 0;
                        foreach (Vertice verticeAux in List_VerticesInspeccionTerrenoCoord)
                        {
                            verticeAux.index = indexAux;
                            indexAux++;
                        }
                        index = List_VerticesInspeccionTerrenoCoord.Count;
                    }

                    Vertice vertice = new Vertice();

                    vertice.idVertice = Convert.ToInt32(IdVertice.Value);
                    vertice.index = Convert.ToInt32(index);
                    vertice.vertice = new ParametroGenerico(Convert.ToInt32(VerticeInspeccionTerrenoCoordenadas.SelectedValue), Convert.ToString(VerticeInspeccionTerrenoCoordenadas.SelectedItem.Text));
                    vertice.latitudHora = Convert.ToInt32(LatitudHoraInspeccionTerrenoCoordenadas.Text);
                    vertice.latitudMinuto = Convert.ToInt32(LatitudMinutoInspeccionTerrenoCoordenadas.Text);
                    /*
                    vertice.latitudSegundo = Convert.ToSingle(LatitudSegundoInspeccionTerrenoCoordenadas.Text);
                    */
                    vertice.latitudSegundo = Convert.ToDouble(LatitudSegundoInspeccionTerrenoCoordenadas.Text);
                    vertice.longitudHora = Convert.ToInt32(LongitudHoraInspeccionTerrenoCoordenadas.Text);
                    vertice.longitudMinuto = Convert.ToInt32(LongitudMinutoInspeccionTerrenoCoordenadas.Text);
                    
                    /*
                    vertice.longitudSegundo = Convert.ToSingle(LongitudSegundoInspeccionTerrenoCoordenadas.Text);
                    */

                    /*
                    vertice.utmE = Convert.ToSingle(UTMEInspeccionTerrenoCoordenadas.Text);
                    vertice.utmN = Convert.ToSingle(UtmNInspeccionTerrenoCoordenadas.Text);
                    */

                    vertice.longitudSegundo = Convert.ToDouble(LongitudSegundoInspeccionTerrenoCoordenadas.Text);
                    vertice.utmE = Convert.ToDouble(UTMEInspeccionTerrenoCoordenadas.Text);
                    vertice.utmN = Convert.ToDouble(UtmNInspeccionTerrenoCoordenadas.Text);

                    List<String> listaErroresVerticeInspeccionTerreno = antecedDelSectorValidacion.validaVertice(vertice, List_VerticesInspeccionTerrenoCoord);

                    if (listaErroresVerticeInspeccionTerreno != null && listaErroresVerticeInspeccionTerreno.Count <= 0)
                    {

                        /**
                        * En una modificación ya existe, por lo que se quita antes de agregarlo nuevamente. 
                        */
                        if (Convert.ToInt32(IdVertice.Value) > 0)
                        {
                            foreach (Vertice verticeRemover in List_VerticesInspeccionTerrenoCoord)
                            {
                                if (verticeRemover.idVertice.Equals(Convert.ToInt32(IdVertice.Value)))
                                {
                                    vertice.index = verticeRemover.index;
                                    List_VerticesInspeccionTerrenoCoord.Remove(verticeRemover);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (Vertice verticeRemover in List_VerticesInspeccionTerrenoCoord)
                            {
                                if (verticeRemover.vertice.id.Equals(Convert.ToInt32(vertice.vertice.id)))
                                {
                                    vertice.index = verticeRemover.index;
                                    List_VerticesInspeccionTerrenoCoord.Remove(verticeRemover);
                                    break;
                                }
                            }

                        }

                        List_VerticesInspeccionTerrenoCoord.Add(vertice);

                        GridVerticeInspeccionTerrenoCoordenadas.DataSource = List_VerticesInspeccionTerrenoCoord;
                        GridVerticeInspeccionTerrenoCoordenadas.DataBind();
                        GridVerticeInspeccionTerrenoCoordenadas.Visible = true;

                        ViewState["Vertices"] = (List<Vertice>)List_VerticesInspeccionTerrenoCoord;
                        IdVertice.Value = Convert.ToString("0");
                    }
                    else
                    {
                        foreach (String error in listaErroresVerticeInspeccionTerreno)
                        {
                            Page.Validators.Add(new ValidationError("grupo2", error));
                        }
                    }
                    break;
            }

        }

        private void CargaGrillaVertice(string nombrePestania)
        {
            switch (nombrePestania)
            {

                case "InspeccionTerrenoCoordenadas":

                    List<Vertice> List_Vertices = (List<Vertice>)ViewState["Vertices"];

                    if (List_Vertices == null)
                    {
                        List_Vertices = new List<Vertice>();
                    }

                    GridVerticeInspeccionTerrenoCoordenadas.DataSource = List_Vertices;
                    GridVerticeInspeccionTerrenoCoordenadas.DataBind();
                    GridVerticeInspeccionTerrenoCoordenadas.Visible = true;

                    ViewState["Vertices"] = (List<Vertice>)List_Vertices;

                    limpiarVertice("InspeccionTerrenoCoordenadas");

                    break;
            }
        }

        private void limpiarVertice(string nombrePestania)
        {
            switch (nombrePestania)
            {

                case "InspeccionTerrenoCoordenadas":

                    VerticeInspeccionTerrenoCoordenadas.SelectedIndex = -1;
                    LatitudHoraInspeccionTerrenoCoordenadas.Text = "";
                    LatitudMinutoInspeccionTerrenoCoordenadas.Text = "";
                    LatitudSegundoInspeccionTerrenoCoordenadas.Text = "";
                    LongitudHoraInspeccionTerrenoCoordenadas.Text = "";
                    LongitudMinutoInspeccionTerrenoCoordenadas.Text = "";
                    LongitudSegundoInspeccionTerrenoCoordenadas.Text = "";
                    UTMEInspeccionTerrenoCoordenadas.Text = "";
                    UtmNInspeccionTerrenoCoordenadas.Text = "";
                    break;
            }
        }

        /**
         * Método que guarda los Polçigonos de Inspección de Terreno Coordenadas. 
         */
        protected void GuardarPoligonoInspeccionTerrenoCoordenadas_Click(object sender, ImageClickEventArgs e)
        {
            AgregarGrillaPoligono("InspeccionTerrenoCoordenadas");
        }

        private void AgregarGrillaPoligono(string nombrePestania)
        {
            AntecedentesSectorService antecedentesSectorService = new AntecedentesSectorService();
            switch (nombrePestania)
            {

                case "InspeccionTerrenoCoordenadas":

                    List<Poligono> List_Poligonos = new List<Poligono>();

                    CoordenadaGeografica coordenadaGeografica = new CoordenadaGeografica();
                    coordenadaGeografica.idSolConcesion = Convert.ToInt32(IdSolicitud.Value);
                    coordenadaGeografica.idCoordenadaGeo = Convert.ToInt32(IdCoordenadaGeo.Value);
                    coordenadaGeografica.tipoCoordgeografica = new ParametroGenerico(rbTipo.INSPECCION_TERRENO);
                    coordenadaGeografica.tipoHuso = new ParametroGenerico(Convert.ToInt32(HusoHorarioinspeccionTerrenoCoordenadas.SelectedValue));
                    coordenadaGeografica.datum = new ParametroGenerico(Convert.ToInt32(DATUMinspeccionTerrenoCoordenadas.SelectedValue));
                    coordenadaGeografica.estado = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
                    
                    Poligono poligono = new Poligono();

                    poligono.idPoligono = Convert.ToInt32(IdPoligono.Value);
                    poligono.idSolicitud = Convert.ToInt32(IdSolicitud.Value);
                    poligono.idCoordenadaGeo = Convert.ToInt32(IdCoordenadaGeo.Value);
                    poligono.estado = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
                    poligono.idPoligAntecSector = Convert.ToInt32(CoordenadaGeoinspeccionTerrenoCoordenadas.SelectedValue);

                    if (Convert.ToInt32(IdPoligono.Value) > 0)
                    {
                        poligono.existePoligono = true;
                        poligono.poligonoOriginal = poligonoDA.ObtienePoligono(Convert.ToInt32(IdCoordenadaGeo.Value), Convert.ToInt32(IdPoligono.Value));

                    }

                    List<Vertice> lista_vertices = (List<Vertice>)ViewState["Vertices"];
                    poligono.lista_vertices = lista_vertices;

                    List_Poligonos.Add(poligono);
                    coordenadaGeografica.listaPoligono = List_Poligonos;

                    List<String> listaErroresPoligono = antecedDelSectorValidacion.validaPoligonoInspeccionTerreno(poligono);

                    if (listaErroresPoligono != null && listaErroresPoligono.Count <= 0)
                    {
                        bool resp = antecedentesSectorService.guardarCoordGeograficaInspeccionTerreno(coordenadaGeografica, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                        if (resp)
                        {
                            ViewState["Vertices"] = null;
                            GridVerticeInspeccionTerrenoCoordenadas.Visible = false;
                            
                            /* Se eliminan los Vértices y sus datos del formulario */
                            limpiarVertice("InspeccionTerrenoCoordenadas");
                            limpiarPoligono("InspeccionTerrenoCoordenadas");

                            //IdCoordenadaGeo.Value = Convert.ToString(coordenadaGeografica.idCoordenadaGeo);

                            /* Se despliegan todos los Polígonos almacenados */
                            List<ComparacionPoligono> listComparacionPoligono = poligonoDA.listarComparacionPoligono(poligono.idSolicitud,0,0);                       
                              
                            //List<Poligono> listaPoligonos = antecedentesSectorService.ListarPoligonoInspeccionTerreno(Convert.ToInt32(IdCoordenadaGeo.Value), 0);

                            GridPoligonosInspeccionTerrenoCoordenadas.DataSource = listComparacionPoligono;
                            GridPoligonosInspeccionTerrenoCoordenadas.DataBind();
                            GridPoligonosInspeccionTerrenoCoordenadas.Visible = true;

                            msgGrillaGral_1.Text = "Se ha guardado el Poligono (Coord. Inspección de Terreno) exitosamente.";
                            Content_msgGrillaGral_1.Visible = true;

                            IdPoligono.Value = Convert.ToString("0");
                            IdCoordenadaGeo.Value = Convert.ToString("0");
                        }
                        else
                        {
                            msgGrillaGral_1.Text = "No se ha guardado el Polígono (Coord. Inspección de Terreno).";
                            Content_msgGrillaGral_1.Visible = true;
                        }

                        IcoGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        UpdInspeccionTerrenoCoordenadas.Update();
                    }
                    else
                    {

                        foreach (String error in listaErroresPoligono)
                        {
                            Page.Validators.Add(new ValidationError("grupo1", error));
                        }
                    }
                    DATUMinspeccionTerrenoCoordenadas.Focus();
                    break;
            }
        }


        private void limpiarPoligono(string nombrePestania)
        {
            switch (nombrePestania)
            {

                case "InspeccionTerrenoCoordenadas":

                    DATUMinspeccionTerrenoCoordenadas.SelectedValue = "-1";
                    DATUMinspeccionTerrenoCoordenadas.Enabled = true;

                    HusoHorarioinspeccionTerrenoCoordenadas.SelectedValue = "-1";
                    HusoHorarioinspeccionTerrenoCoordenadas.Enabled = true;

                    CoordenadaGeoinspeccionTerrenoCoordenadas.SelectedValue = "-1";
                    CoordenadaGeoinspeccionTerrenoCoordenadas.Enabled = true;

                    break;
            }
        }

        protected void GridVerticeInspeccionTerrenoCoordenadas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int accion = Convert.ToInt32(Accion.Value);
                if (accion == rbAccion.VER)
                {
                    // Ver
                    ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                    if (boton_ver != null)
                    {
                        boton_ver.Visible = true;
                    };
                }
                else
                {

                    // Borrar
                    ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                    if (boton_eliminar != null)
                    {
                        boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar el vértice?')");
                        boton_eliminar.Visible = true;
                    };

                    // Modificar
                    ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                    if (boton_eliminar != null)
                    {
                        boton_modificar.Visible = true;
                    };
                }
            };
        }

        protected void GridVerticeInspeccionTerrenoCoordenadas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int idVertice = Convert.ToInt32(arg[0]);
            int index = Convert.ToInt32(arg[1]);

            switch (e.CommandName)
            {
                case "Ver":
                    if (idVertice > 0) //Si existe en la Base de Datos
                    {
                        Vertice verticeModificar = verticeDA.ObtieneVertice(0, idVertice);

                        VerticeInspeccionTerrenoCoordenadas.SelectedValue = Convert.ToString(verticeModificar.vertice.id);
                        VerticeInspeccionTerrenoCoordenadas.Enabled = false;

                        LatitudHoraInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.latitudHora);
                        LatitudHoraInspeccionTerrenoCoordenadas.Enabled = false;

                        LatitudMinutoInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.latitudMinuto);
                        LatitudMinutoInspeccionTerrenoCoordenadas.Enabled = false;

                        LatitudSegundoInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.latitudSegundo);
                        LatitudSegundoInspeccionTerrenoCoordenadas.Enabled = false;

                        LongitudHoraInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.longitudHora);
                        LongitudHoraInspeccionTerrenoCoordenadas.Enabled = false;

                        LongitudMinutoInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.longitudMinuto);
                        LongitudMinutoInspeccionTerrenoCoordenadas.Enabled = false;

                        LongitudSegundoInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.longitudSegundo);
                        LongitudSegundoInspeccionTerrenoCoordenadas.Enabled = false;

                        UTMEInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.utmE);
                        UTMEInspeccionTerrenoCoordenadas.Enabled = false;

                        UtmNInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.utmN);
                        UtmNInspeccionTerrenoCoordenadas.Enabled = false;

                        IdVertice.Value = Convert.ToString(idVertice);

                        PanelBotonesInspeccionTerrenoCoordenadas.Visible = false;

                    }
                    else
                    { //Si no existe en la BD

                        List<Vertice> List_Vertices = (List<Vertice>)ViewState["Vertices"];
                        Vertice verticeModificado = null;
                        foreach (Vertice vertice in List_Vertices)
                        {
                            if (vertice.index.Equals(Convert.ToInt32(index)))
                            {
                                verticeModificado = vertice;
                                break;
                            }
                        }
                        if (verticeModificado != null)
                        {
                            VerticeInspeccionTerrenoCoordenadas.SelectedValue = Convert.ToString(verticeModificado.vertice.id);
                            VerticeInspeccionTerrenoCoordenadas.Enabled = false;

                            LatitudHoraInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificado.latitudHora);
                            LatitudHoraInspeccionTerrenoCoordenadas.Enabled = false;

                            LatitudMinutoInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificado.latitudMinuto);
                            LatitudMinutoInspeccionTerrenoCoordenadas.Enabled = false;

                            LatitudSegundoInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificado.latitudSegundo);
                            LatitudSegundoInspeccionTerrenoCoordenadas.Enabled = false;

                            LongitudHoraInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificado.longitudHora);
                            LongitudHoraInspeccionTerrenoCoordenadas.Enabled = false;

                            LongitudMinutoInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificado.longitudMinuto);
                            LongitudMinutoInspeccionTerrenoCoordenadas.Enabled = false;

                            LongitudSegundoInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificado.longitudSegundo);
                            LongitudSegundoInspeccionTerrenoCoordenadas.Enabled = false;

                            UTMEInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificado.utmE);
                            UTMEInspeccionTerrenoCoordenadas.Enabled = false;

                            UtmNInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificado.utmN);
                            UtmNInspeccionTerrenoCoordenadas.Enabled = false;

                            IdVertice.Value = Convert.ToString("0");

                            PanelBotonesInspeccionTerrenoCoordenadas.Visible = false;
                        }
                    }

                    break;
                case "Eliminar":

                    if (idVertice > 0)
                    {
                        GridVerticeInspeccionTerrenoCoordenadas.EditIndex = -1;
                        EliminarGrillaVertice(Convert.ToInt32(IdPoligono.Value), idVertice, "InspeccionTerrenoCoordenadas");
                        CargaGrillaVertice("InspeccionTerrenoCoordenadas");
                    }
                    else
                    {

                        List<Vertice> List_Vertices = (List<Vertice>)ViewState["Vertices"];
                        foreach (Vertice vertice in List_Vertices)
                        {
                            if (vertice.index.Equals(Convert.ToInt32(index)))
                            {
                                List_Vertices.Remove(vertice);
                                break;
                            }
                        }

                        GridVerticeInspeccionTerrenoCoordenadas.DataSource = List_Vertices;
                        GridVerticeInspeccionTerrenoCoordenadas.DataBind();
                        GridVerticeInspeccionTerrenoCoordenadas.Visible = true;

                        ViewState["Vertices"] = (List<Vertice>)List_Vertices;
                    }

                    break;

                case "Modificar":

                    if (idVertice > 0) //Si existe en la Base de Datos
                    {
                        Vertice verticeModificar = verticeDA.ObtieneVertice(0, idVertice);

                        VerticeInspeccionTerrenoCoordenadas.SelectedValue = Convert.ToString(verticeModificar.vertice.id);
                        LatitudHoraInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.latitudHora);
                        LatitudMinutoInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.latitudMinuto);
                        LatitudSegundoInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.latitudSegundo);

                        LongitudHoraInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.longitudHora);
                        LongitudMinutoInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.longitudMinuto);
                        LongitudSegundoInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.longitudSegundo);

                        UTMEInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.utmE);
                        UtmNInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.utmN);

                        IdVertice.Value = Convert.ToString(idVertice);

                    }
                    else
                    { //Si no existe en la BD

                        List<Vertice> List_VerticesAntEspaciales = (List<Vertice>)ViewState["Vertices"];
                        Vertice verticeModificar = null;
                        foreach (Vertice vertice in List_VerticesAntEspaciales)
                        {
                            if (vertice.index.Equals(Convert.ToInt32(index)))
                            {
                                verticeModificar = vertice;
                                break;
                            }
                        }
                        if (verticeModificar != null)
                        {
                            VerticeInspeccionTerrenoCoordenadas.SelectedValue = Convert.ToString(verticeModificar.vertice.id);
                            LatitudHoraInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.latitudHora);
                            LatitudMinutoInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.latitudMinuto);
                            LatitudSegundoInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.latitudSegundo);

                            LongitudHoraInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.longitudHora);
                            LongitudMinutoInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.longitudMinuto);
                            LongitudSegundoInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.longitudSegundo);

                            UTMEInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.utmE);
                            UtmNInspeccionTerrenoCoordenadas.Text = Convert.ToString(verticeModificar.utmN);

                            IdVertice.Value = Convert.ToString("0");
                        }
                    }
                    break;
            };
        }

        protected void LimpiarVertice_Click(object sender, ImageClickEventArgs e)
        {
            limpiarVertice("InspeccionTerrenoCoordenadas");
            IdVertice.Value = Convert.ToString("0");

            VerticeInspeccionTerrenoCoordenadas.Enabled = true;
            LatitudHoraInspeccionTerrenoCoordenadas.Enabled = true;
            LatitudMinutoInspeccionTerrenoCoordenadas.Enabled = true;
            LatitudSegundoInspeccionTerrenoCoordenadas.Enabled = true;
            LongitudHoraInspeccionTerrenoCoordenadas.Enabled = true;
            LongitudMinutoInspeccionTerrenoCoordenadas.Enabled = true;
            LongitudSegundoInspeccionTerrenoCoordenadas.Enabled = true;
            UTMEInspeccionTerrenoCoordenadas.Enabled = true;
            UtmNInspeccionTerrenoCoordenadas.Enabled = true;

            PanelBotonesInspeccionTerrenoCoordenadas.Visible = true;
        }

        protected void EliminarGrillaVertice(int idPoligono, int idVertice, string nombrePestania)
        {
            AntecedentesSectorService antecedentesSectorService= new AntecedentesSectorService();

            switch (nombrePestania)
            {

                case "InspeccionTerrenoCoordenadas":

                    List<Vertice> List_Vertices = verticeDA.ListarVertice(idPoligono,0);

                    foreach (Vertice vertice in List_Vertices)
                    {
                        if (vertice.idVertice.Equals(idVertice))
                        {
                            bool resp = antecedentesSectorService.eliminarVertice(idVertice, idPoligono, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                            if(resp){
                                msgGrillaGral_1.Text = "Se ha eliminado el vértice (Coord. Inspección de Terreno) exitosamente.";
                                Content_msgGrillaGral_1.Visible = true;

                            }else{
                                msgGrillaGral_1.Text = "No se ha eliminado el vértice (Coord. Inspección de Terreno).";
                                Content_msgGrillaGral_1.Visible = true;
                            }
                            IcoGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        }
                    }

                    List_Vertices = verticeDA.ListarVertice(idPoligono, 0);

                    ViewState["Vertices"] = (List<Vertice>)List_Vertices;
                    break;
            }
        }

        protected void GridPoligonosInspeccionTerrenoCoordenadas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Accion.Value = Convert.ToString(rbAccion.NINGUNA);
            
            int idPoligono = 0;
            //List<Poligono> listaPoligonos = null;
            List<ComparacionPoligono> listaComparacionPoligonos = null;

            ComparacionPoligono comparacionPoligono = null; 

            switch (e.CommandName)
            {
                case "Eliminar":
                    idPoligono = Convert.ToInt32(e.CommandArgument);
                    GridPoligonosInspeccionTerrenoCoordenadas.EditIndex = -1;
                    EliminarGrillaPoligono(Convert.ToInt32(IdCoordenadaGeo.Value), idPoligono, "InspeccionTerrenoCoordenadas");

                    listaComparacionPoligonos = poligonoDA.listarComparacionPoligono(Convert.ToInt32(IdSolicitud.Value),0,0);
                    GridPoligonosInspeccionTerrenoCoordenadas.DataSource = listaComparacionPoligonos;

                    //listaPoligonos = poligonoDA.ListarPoligono(Convert.ToInt32(IdCoordenadaGeo.Value), 0);
                    //GridPoligonosInspeccionTerrenoCoordenadas.DataSource = listaPoligonos;
                    GridPoligonosInspeccionTerrenoCoordenadas.DataBind();

                    limpiarPoligono("InspeccionTerrenoCoordenadas");

                    limpiarVertice("InspeccionTerrenoCoordenadas");

                    GridVerticeInspeccionTerrenoCoordenadas.Visible = false;
                    ViewState["Vertices"] = null;
                    IdVertice.Value = Convert.ToString("0"); 

                    IdPoligono.Value = Convert.ToString("0"); 

                    break;

                case "Modificar":
                    idPoligono = Convert.ToInt32(e.CommandArgument);

                    comparacionPoligono = poligonoDA.obtenerComparacionPoligono(Convert.ToInt32(IdSolicitud.Value),idPoligono,0);
                    IdCoordenadaGeo.Value = Convert.ToString(comparacionPoligono.poligonoInspTerreno.coordenadaPoligono.idCoordenadaGeo);

                    CargarPoligono(comparacionPoligono, "InspeccionTerrenoCoordenadas");

                    DATUMinspeccionTerrenoCoordenadas.Enabled = true;
                    HusoHorarioinspeccionTerrenoCoordenadas.Enabled = true;
                    CoordenadaGeoinspeccionTerrenoCoordenadas.Enabled = true;
                    
                    LimpiarVertice_Click(null, null);

                    break;
                case "Ver":
                    idPoligono = Convert.ToInt32(e.CommandArgument);

                    comparacionPoligono = poligonoDA.obtenerComparacionPoligono(Convert.ToInt32(IdSolicitud.Value), idPoligono, 0);
                    CargarPoligonoVer(comparacionPoligono,Convert.ToInt32(IdCoordenadaGeo.Value), idPoligono, "InspeccionTerrenoCoordenadas");

                    limpiarVertice("InspeccionTerrenoCoordenadas");

                    break;

            };
        }

        private void CargarPoligonoVer(ComparacionPoligono comparacionPoligono, int idCoordenadaGeo, int idPoligono, string pestana)
        {
            Poligono poligono = poligonoDA.ObtienePoligono(idCoordenadaGeo, idPoligono);
            switch (pestana)
            {
                case "InspeccionTerrenoCoordenadas":

                    if (comparacionPoligono != null)
                    {
                        DATUMinspeccionTerrenoCoordenadas.SelectedValue = Convert.ToString(comparacionPoligono.poligonoInspTerreno.coordenadaPoligono.datum.id);
                        DATUMinspeccionTerrenoCoordenadas.Enabled = false;

                        if (comparacionPoligono.poligonoInspTerreno != null && comparacionPoligono.poligonoInspTerreno.coordenadaPoligono != null &&
                            comparacionPoligono.poligonoInspTerreno.coordenadaPoligono.tipoHuso != null && comparacionPoligono.poligonoInspTerreno.coordenadaPoligono.tipoHuso.id > 0)
                        {
                            HusoHorarioinspeccionTerrenoCoordenadas.SelectedValue = Convert.ToString(comparacionPoligono.poligonoInspTerreno.coordenadaPoligono.tipoHuso.id);
                        }
                        else {
                            HusoHorarioinspeccionTerrenoCoordenadas.SelectedValue = "-1";
                        }

                        HusoHorarioinspeccionTerrenoCoordenadas.Enabled = false;

                        CoordenadaGeoinspeccionTerrenoCoordenadas.SelectedValue = Convert.ToString(comparacionPoligono.poligonoAntecSector.idPoligono);
                        CoordenadaGeoinspeccionTerrenoCoordenadas.Enabled = false;
                    }

                    IdPoligono.Value = Convert.ToString(idPoligono);
                    IdCoordenadaGeo.Value = Convert.ToString(idCoordenadaGeo);

                    List<Vertice> List_Vertices = verticeDA.ListarVertice(idPoligono, 0);

                    if (List_Vertices == null)
                    {
                        List_Vertices = new List<Vertice>();
                    }

                    ViewState["Vertices"] = (List<Vertice>)List_Vertices;

                    Accion.Value = Convert.ToString(rbAccion.VER);

                    GridVerticeInspeccionTerrenoCoordenadas.DataSource = List_Vertices;
                    GridVerticeInspeccionTerrenoCoordenadas.DataBind();
                    GridVerticeInspeccionTerrenoCoordenadas.Visible = true;

                    PanelBotonesPoligonoInspeccionTerreno.Visible = false;
                    PanelBotonesInspeccionTerrenoCoordenadas.Visible = false;
                    PanelBotonesPoligonoOriginal.Visible = false;

                    PanelCoordenadasGeograficaInspTerreno.Visible = true;
                    FormularioIngreso.Visible = true;
                    PanelVertice.Visible = true;
                    UpdInspeccionTerrenoCoordenadas.Update();

                    break;
            }
        }

        protected void LimpiarPoligono_Click(object sender, ImageClickEventArgs e)
        {
            limpiarPoligono("InspeccionTerrenoCoordenadas");
            IdPoligono.Value = Convert.ToString("0");
            
            ViewState["Vertices"] = null;

            GridVerticeInspeccionTerrenoCoordenadas.DataSource = null;
            GridVerticeInspeccionTerrenoCoordenadas.DataBind();
            GridVerticeInspeccionTerrenoCoordenadas.Visible = false;

            LimpiarVertice_Click(null, null);

            PanelBotonesPoligonoInspeccionTerreno.Visible = true;
        }

        protected void EliminarGrillaPoligono(int idCoordenadaGeo, int idPoligono, String nombrePestania)
        {
            AntecedentesSectorService antecedentesSectorService = new AntecedentesSectorService();
            switch (nombrePestania)
            {

                case "InspeccionTerrenoCoordenadas":
                    bool resp = antecedentesSectorService.eliminarPoligonoCoordenadaGeo(Convert.ToInt32(IdSolicitud.Value), idCoordenadaGeo, idPoligono, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                    if (resp)
                    {
                        Content_msgGrillaGral_1.Visible = true;
                        msgGrillaGral_1.Text = "Se ha eliminado exitosamente el Poligono (Coordenadas de Inspección de Terreno).";

                    }
                    else
                    {
                        Content_msgGrillaGral_1.Visible = true;
                        msgGrillaGral_1.Text = "No se ha eliminado el Poligono (Coordenadas de Inspección de Terreno).";
                    }

                    IcoGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";

                    break;
            }
        }

        private void CargarPoligono(ComparacionPoligono comparacionPoligono, string pestana)
        {
            Poligono poligono = poligonoDA.ObtienePoligono(comparacionPoligono.poligonoInspTerreno.coordenadaPoligono.idCoordenadaGeo, comparacionPoligono.poligonoInspTerreno.idPoligono);
            switch (pestana)
            {
                case "InspeccionTerrenoCoordenadas":
                    
                    /*
                    IdPoligono.Value = Convert.ToString(idPoligono);
                    IdCoordenadaGeo.Value = Convert.ToString(idCoordenadaGeo);

                    CoordenadaGeografica coordenadaGeografica = coordenadaGeograficaDA.ObtieneCoordenadaGeografica(Convert.ToInt32(IdSolicitud.Value),idCoordenadaGeo);

                    DATUMinspeccionTerrenoCoordenadas.SelectedValue = Convert.ToString(coordenadaGeografica.datum.id);
                    HusoHorarioinspeccionTerrenoCoordenadas.SelectedValue = Convert.ToString(coordenadaGeografica.tipoHuso.id);
                    */

                    IdPoligono.Value = Convert.ToString(poligono.idPoligono);

                    if (DATUMinspeccionTerrenoCoordenadas != null)
                    {
                        DATUMinspeccionTerrenoCoordenadas.SelectedValue = Convert.ToString(comparacionPoligono.poligonoInspTerreno.coordenadaPoligono.datum.id);

                        if (comparacionPoligono.poligonoInspTerreno != null && comparacionPoligono.poligonoInspTerreno.coordenadaPoligono != null &&
                            comparacionPoligono.poligonoInspTerreno.coordenadaPoligono.tipoHuso != null && comparacionPoligono.poligonoInspTerreno.coordenadaPoligono.tipoHuso.id > 0)
                        {
                            HusoHorarioinspeccionTerrenoCoordenadas.SelectedValue = Convert.ToString(comparacionPoligono.poligonoInspTerreno.coordenadaPoligono.tipoHuso.id);
                        }
                        else {
                            HusoHorarioinspeccionTerrenoCoordenadas.SelectedValue = "-1";
                        }
                        
                        CoordenadaGeoinspeccionTerrenoCoordenadas.SelectedValue = Convert.ToString(comparacionPoligono.poligonoAntecSector.idPoligono);

                        IdCoordenadaGeo.Value = Convert.ToString(comparacionPoligono.poligonoInspTerreno.coordenadaPoligono.idCoordenadaGeo);
                    }

                    List<Vertice> List_Vertices = verticeDA.ListarVertice(poligono.idPoligono, 0);

                    if (List_Vertices == null)
                    {
                        List_Vertices = new List<Vertice>();
                    }

                    ViewState["Vertices"] = (List<Vertice>)List_Vertices;

                    GridVerticeInspeccionTerrenoCoordenadas.DataSource = List_Vertices;
                    GridVerticeInspeccionTerrenoCoordenadas.DataBind();
                    GridVerticeInspeccionTerrenoCoordenadas.Visible = true;

                    PanelBotonesPoligonoInspeccionTerreno.Visible = true;

                    PanelBotonesInspeccionTerrenoCoordenadas.Visible = true;

                    PanelBotonesPoligonoOriginal.Visible = true;

                    break;
            }
        }

        protected void GridPoligonosInspeccionTerrenoCoordenadas_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridPoligonoAntEspaciales = (GridView)sender;


                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Listado Coordenadas de Inspección de Terreno";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridPoligonoAntEspaciales.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        protected void GridPoligonosInspeccionTerrenoCoordenadas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                if (e.Row.Cells[3].Text.Equals(colorPlanilla.POSITIVA))
                {
                    e.Row.Cells[3].BackColor = colorPlanilla.COLOR_COMPLETADO;
                }
                else 
                {
                    e.Row.Cells[3].BackColor = colorPlanilla.COLOR_PENDIENTE;
                }

                // Ver
                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    boton_ver.Visible = true;
                };
                                
                // Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ELIMINAR))
                    {
                        boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar el Poligono?')");
                        boton_eliminar.Visible = true;
                    }
                };

                // Modificar
                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_modificar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.EDITAR))
                    {
                        boton_modificar.Visible = true;
                    }
                };

            };
        }
    }
}