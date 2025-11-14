using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.servicios.modificacion;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using Datos.Entidades;
using Datos.Contantes;

namespace SubPesca.Solicitudes.Registrar
{
    public partial class checkRequiereDifusionBanco : System.Web.UI.UserControl
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        private Funciones funciones = new Funciones();
        private DespliegueSeccionService despliegueSeccionService = new DespliegueSeccionService();
        private SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();
        private SolicitudConcesionService solicitudConcesionService = new SolicitudConcesionService();
        private PermisosService permisosService = new PermisosService();


        //Aplica solo para solicitudes de concesion de acuicultura (solicitud, mod y relocalizacion)
        //utiliza el permiso de ingreso de la difusion de banco banco
        protected void setearModulo()
        {

            ValidacionDocumentacion validacionDocumentacion = new ValidacionDocumentacion();

            if (funciones.retornaModulo().Equals("Registrar"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLCONCESION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLCONCESION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLCONCESION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLCONCESION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLCONCESION;
                ViewState["solicitudSession"] = paginas.solicitudConcesionSession;

                validacionDocumentacion.aplicaConcesion = 1;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_CONCESION };
                ViewState["validacionDocumentacion"] = validacionDocumentacion;

            }
            else if (funciones.retornaModulo().Equals("Relocalizacion"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_RELOCALIZACION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_RELOCALIZACION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_RELOCALIZACION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_RELOCALIZACION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_RELOCALIZACION;
                ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSession;

                validacionDocumentacion.aplicaRelocalizacion = 1;


                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_RELOCALIZACION_CREA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_RELOCALIZACION_FUSIONA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_RELOCALIZACION_SECTOR_CERO };
                }

                ViewState["validacionDocumentacion"] = validacionDocumentacion;
            }
            else if (funciones.retornaModulo().Equals("RelocalizacionRESA"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_RELOCALIZACION_RESA;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_RELOCALIZACION_RESA;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_RELOCALIZACION_RESA;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_RELOCALIZACION_RESA;
                ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSessionRESA;

                validacionDocumentacion.aplicaRelocalizacion = 1;


                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA_RESA)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_RELOCALIZACION_CREA_RESA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA_RESA)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_RELOCALIZACION_FUSIONA_RESA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO_RESA)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_RELOCALIZACION_SECTOR_CERO_RESA };
                }

                ViewState["validacionDocumentacion"] = validacionDocumentacion;
            }
            else if (funciones.retornaModulo().Equals("Acopio"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_ACOPIO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_ACOPIO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_ACOPIO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_ACOPIO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_CENTRO_DE_ACOPIO;
                ViewState["solicitudSession"] = paginas.solicitudAcopioSession;

                validacionDocumentacion.aplicaAcopio = 1;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_ACOPIO };
                ViewState["validacionDocumentacion"] = validacionDocumentacion;

            }
            else if (funciones.retornaModulo().Equals("Faenamiento"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_CENTRO_DE_FAENAMIENTO;
                ViewState["solicitudSession"] = paginas.solicitudFaenamientoSession;

                validacionDocumentacion.aplicaFaenamiento = 1;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_FAENAMIENTO };
                ViewState["validacionDocumentacion"] = validacionDocumentacion;

            }
            else if (funciones.retornaModulo().Equals("Amerb"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_AMERB;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_AMERB;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_AMERB;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_AMERB;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_AMERB;
                ViewState["solicitudSession"] = paginas.solicitudAmerbSession;

                validacionDocumentacion.aplicaAmerb = 1;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_AMERB };
                ViewState["validacionDocumentacion"] = validacionDocumentacion;

            }
            else if (funciones.retornaModulo().Equals("ExperimentalesAmerb"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesAmerbSession;

                validacionDocumentacion.aplicaExperimentalesAmerb = 1;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_EXPERIMENTALES_AMERB };
                ViewState["validacionDocumentacion"] = validacionDocumentacion;

            }
            else if (funciones.retornaModulo().Equals("ExperimentalesConcesion"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesConcesionSession;

                validacionDocumentacion.aplicaExpConcesion = 1;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_EXPERIMENTALES_CONCESION };
                ViewState["validacionDocumentacion"] = validacionDocumentacion;

            }
            else if (funciones.retornaModulo().Equals("ECMPO"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_ECMPO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_ECMPO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_ECMPO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_ECMPO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_ECMPO;
                ViewState["solicitudSession"] = paginas.solicitudECMPOSession;

                validacionDocumentacion.aplicaAcuiculturaEcmpo = 1;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_ECMPO };
                ViewState["validacionDocumentacion"] = validacionDocumentacion;

            }

            else if (funciones.retornaModulo().Equals("Colector"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_COLECTORES_SEMILLA;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_COLECTORES_SEMILLA;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_COLECTORES_SEMILLA;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_COLECTORES_SEMILLA;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_COLECTORES_SEMILLA;
                ViewState["solicitudSession"] = paginas.solicitudColectorSession;

                validacionDocumentacion.aplicaColectores = 1;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_COLECTOR };
                ViewState["validacionDocumentacion"] = validacionDocumentacion;

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

                    funciones.seteaTipoModificacion(solicitudConcesion, validacionDocumentacion);

                    int[] tiposModificacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

                    int contador = 0;
                    foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                    {
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_AMPLIA_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_AMERB_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_AMERB_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_PT)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_AMERB_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_AMERB_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_AMERB_REGULARIZACION;
                            contador++;
                        }
                    }

                    ViewState["SECCION_ESPECIFICA"] = tiposModificacion;
                    ViewState["validacionDocumentacion"] = validacionDocumentacion;

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

                    funciones.seteaTipoModificacion(solicitudConcesion, validacionDocumentacion);

                    int[] tiposModificacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

                    int contador = 0;
                    foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                    {
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_RENOVACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_CENTRO_ACOPIO_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_PT_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_CENTRO_ACOPIO_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_CENTRO_ACOPIO_REGULARIZACION;
                            contador++;
                        }
                    }

                    ViewState["SECCION_ESPECIFICA"] = tiposModificacion;
                    ViewState["validacionDocumentacion"] = validacionDocumentacion;

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

                    funciones.seteaTipoModificacion(solicitudConcesion, validacionDocumentacion);

                    int[] tiposModificacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

                    int contador = 0;
                    foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                    {
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_RENOVACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_PT_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_CENTRO_FAENAMIENTO_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_CENTRO_FAENAMIENTO_REGULARIZACION;
                            contador++;
                        }
                    }

                    ViewState["SECCION_ESPECIFICA"] = tiposModificacion;
                    ViewState["validacionDocumentacion"] = validacionDocumentacion;

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

                    funciones.seteaTipoModificacion(solicitudConcesion, validacionDocumentacion);

                    int[] tiposModificacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

                    int contador = 0;
                    foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                    {
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_AMPLIA_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_ECMPO_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_PT)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_ECMPO_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_ECMPO_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_ECMPO_REGULARIZACION;
                            contador++;
                        }
                    }

                    ViewState["SECCION_ESPECIFICA"] = tiposModificacion;
                    ViewState["validacionDocumentacion"] = validacionDocumentacion;

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

                    funciones.seteaTipoModificacion(solicitudConcesion, validacionDocumentacion);

                    int[] tiposModificacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

                    int contador = 0;
                    foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                    {
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_AMPLIA_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_CONCESION_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_PT)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_CONCESION_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_CONCESION_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.DIFUSION_BANCO_NATURAL_MOD_CONCESION_REGULARIZACION;
                            contador++;
                        }
                    }

                    ViewState["SECCION_ESPECIFICA"] = tiposModificacion;
                    ViewState["validacionDocumentacion"] = validacionDocumentacion;

                }

            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {

            // PAGE LOAD
            if (!Page.IsPostBack)
            {

                setearModulo();


                SolicitudConcesion solicitud = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];


            


                PanelCheck.Visible = true;
                UpdatePanelCheck.Update();


                SolicitudConcesion checks = solicitudConcesionService.obtenerDecisionUsuario(solicitud.idSolConcesion);

                if (checks.omiteDifRadial != null && checks.omiteDifRadial.id > 0)
                {
                    CheckCombo.SelectedValue = checks.omiteDifRadial.id.ToString();
                }


                //BOTON DE INGRESO O MODIFICACION
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], this.usuario_logeado, solicitud, rbAccion.EDITAR))
                {
                    CheckCombo.Enabled = true;
                    Panel1.Visible = true;
                }
                else
                {
                    CheckCombo.Enabled = false;
                    Panel1.Visible = false;
                }


                 



            }
        }


        protected void Check_Guardar_Click(object sender, EventArgs e)
        {


            ErroresSuperior.Text = "";
            PanelErroresSuperior.Visible = false;
            UpdatePanelErroresSuperior.Update();


            SolicitudConcesion solicitud = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];


            if (solicitud != null)
            {

                if (solicitudConcesionService.ActualizaSolicitud_RequiereDifusionBanco(solicitud.idSolConcesion, Convert.ToInt32(CheckCombo.SelectedValue)))
                {

                    ErroresSuperior.Text = "Actualización realizada con éxito.";
                    PanelErroresSuperior.Visible = true;
                    UpdatePanelErroresSuperior.Update();

                    solicitud.omiteDifRadial = new ParametroGenerico(Convert.ToInt32(CheckCombo.SelectedValue));

                    Session[ViewState["solicitudSession"].ToString()] = solicitud;

                    //RECARGANDO LA INFORMACION DE LA SOLICITUD, POR SI CAMBIO EL ESTADO
                    UpdatePanel UpdatePanelInformacionSolictud = informacionSolicitud.Instance.UpdatePanelInfo;
                    informacionSolicitud.Instance.RecargarInformacion();
                    UpdatePanelInformacionSolictud.Update();

                }
                else
                {

                    ErroresSuperior.Text = "Ha ocurrido un error al ejecutar la acción solicitada.";
                    PanelErroresSuperior.Visible = true;
                    UpdatePanelErroresSuperior.Update();

                }


            }
            else
            {

                ErroresSuperior.Text = "Ha ocurrido un error al ejecutar la acción solicitada.";
                PanelErroresSuperior.Visible = true;
                UpdatePanelErroresSuperior.Update();

            }
        }
    }
}