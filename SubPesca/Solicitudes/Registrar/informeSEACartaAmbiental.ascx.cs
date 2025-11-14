using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Validaciones.cl.subpesca.rb.solicitud;
using Datos.Contantes;
using Datos.Entidades;
using System.Collections;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using SubPesca.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using Datos.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;



namespace SubPesca.Solicitudes.Registrar
{
    public partial class informeSEACartaAmbiental : System.Web.UI.UserControl
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema

        PestanaDA pestanaDA = new PestanaDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        IngresarDocumentoValidacion ingresarDocumentoValidacion = new IngresarDocumentoValidacion();
        ValidacionDocumentacionDA validacionDocumentacionDA = new ValidacionDocumentacionDA();
        RequerimientoService requerimientoService = new RequerimientoService();
        PermisosService permisosService = new PermisosService();

        int dbPestana = rbPestana.INFORME_SEA;
        int dbSeccion = rbSeccion.ANTECEDENTES_AMBIENTALES_MO;
        String labelDocumentos = "Documentos Antecedentes Ambientales MO";
        bool esInformesResoluciones = true;

        Funciones funciones = new Funciones();
        EnviarCorreo enviarCorreo = new EnviarCorreo();

        String erroresSumary = "ValidationSummary" + Convert.ToString(rbSeccion.ANTECEDENTES_AMBIENTALES_MO);



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
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CARTA_AMBIENTAL_CONCESION };
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
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CARTA_AMBIENTAL_RELOCALIZACION_CREA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CARTA_AMBIENTAL_RELOCALIZACION_FUSIONA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CARTA_AMBIENTAL_RELOCALIZACION_SECTOR_CERO };
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
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CARTA_AMBIENTAL_RELOCALIZACION_CREA_RESA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA_RESA)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CARTA_AMBIENTAL_RELOCALIZACION_FUSIONA_RESA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO_RESA)
                {
                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CARTA_AMBIENTAL_RELOCALIZACION_SECTOR_CERO_RESA };
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
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CARTA_AMBIENTAL_ACOPIO };
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
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CARTA_AMBIENTAL_FAENAMIENTO };
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
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CARTA_AMBIENTAL_AMERB };
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
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CARTA_AMBIENTAL_EXPERIMENTALES_AMERB };
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
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CARTA_AMBIENTAL_EXPERIMENTALES_CONCESION };
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
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CARTA_AMBIENTAL_ECMPO };
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
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CARTA_AMBIENTAL_COLECTOR };
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
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_AMERB_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_AMERB_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_PT)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_AMERB_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_AMERB_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_AMERB_REGULARIZACION;
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
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_RENOVACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_CENTRO_ACOPIO_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_PT_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_CENTRO_ACOPIO_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_CENTRO_ACOPIO_REGULARIZACION;
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
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_RENOVACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_PT_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_CENTRO_FAENAMIENTO_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_CENTRO_FAENAMIENTO_REGULARIZACION;
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
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_ECMPO_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_PT)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_ECMPO_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_ECMPO_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_ECMPO_REGULARIZACION;
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
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_ESPECIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_CONCESION_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_PT)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_CONCESION_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_REDUCE_SUPERFICIE)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_CONCESION_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_REGULARIZACION)
                        {
                            tiposModificacion[contador] = rbSeccionUnidadEspacial.CARTA_AMBIENTAL_MOD_CONCESION_REGULARIZACION;
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

            if ((Request.Params["__EVENTTARGET"] != null) && (Request.Params["__EVENTARGUMENT"] != null))
            {
                if ((Request.Params["__EVENTTARGET"] == this.NumeroCI.ClientID) && (Request.Params["__EVENTARGUMENT"] == "onchange"))
                {
                    this.NumeroCI_TextChanged(null, null);
                }
            }


            if (PanelFecha.Visible == true)
            {
                string script = "calendario('" + Fecha.ClientID + "','" + fechaImgDinamica.ClientID + "');";
                ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + Fecha.ClientID, script.ToString(), true);
            }

            if (PanelNuevaFecha.Visible == true)
            {
                string script = "calendario('" + NuevaFecha.ClientID + "','" + nuevaFechaImgDinamica.ClientID + "');";
                ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + NuevaFecha.ClientID, script.ToString(), true);
            }

            if (PanelFechaCI.Visible == true)
            {
                string script = "calendario('" + FechaCI.ClientID + "','" + fechaCIImgDinamica.ClientID + "');";
                ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + FechaCI.ClientID, script.ToString(), true);
            }

            // PAGE LOAD
            if (!Page.IsPostBack)
            {
                setearModulo();

                ValidationSummaryErrores.ValidationGroup = erroresSumary;


                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (solicitudConcesion == null || usuario_logeado == null)
                {
                    Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
                }


                //BOTON DE INGRESO O MODIFICACION
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {
                    PanelFormularioIngreso.Visible = true;
                    UpdatePanelFormularioIngreso.Update();
                }
                else
                {
                    PanelFormularioIngreso.Visible = false;
                    UpdatePanelFormularioIngreso.Update();
                }


                IdSolicitud.Text = Convert.ToString(solicitudConcesion.idSolConcesion);


                CargarListaRequerimientos(usuario_logeado, Convert.ToInt32(IdSolicitud.Text), false);


                ValidacionDocumentacion validacionDocumentacion = (ValidacionDocumentacion)ViewState["validacionDocumentacion"];
                validacionDocumentacion.ambito = new ParametroGenerico(this.dbPestana);
                validacionDocumentacion.seccion = new ParametroGenerico(this.dbSeccion);
                ViewState["HashCampos"] = validacionDocumentacionDA.ListaValidacionDocGeneral(validacionDocumentacion);

                // Inicializamos el formulario
                Initialize_Form();

            }
        }


        private void CargarListaRequerimientos(Usuario.Serializable usuario_logeado, int IdSolicitud, bool recargarInformacionSol)
        {

            GridRequerimiento.DataSource = requerimientoService.ListarRequerimiento(IdSolicitud, dbPestana, dbSeccion);
            GridRequerimiento.DataBind();

            if (recargarInformacionSol)
            {
                //RECARGANDO LA INFORMACION DE LA SOLICITUD, POR SI CAMBIO EL ESTADO
                UpdatePanel UpdatePanelInformacionSolictud = informacionSolicitud.Instance.UpdatePanelInfo;
                informacionSolicitud.Instance.RecargarInformacion();
                UpdatePanelInformacionSolictud.Update();
            }

        }




        protected void Initialize_Form()
        {
            // Cargamos los combobox
            Initialize_Comboboxs();

        }


        protected void Initialize_Comboboxs()
        {

            Carga_Combobox("FlujoDocumental");
            FlujoDocumental.SelectedValue = "0";

            Carga_Combobox("TipoSalida");
            TipoSalida.SelectedValue = "0";

            Carga_Combobox("TipoEntrada");
            TipoEntrada.SelectedValue = "0";

            Carga_Combobox("Origen");
            Origen.SelectedValue = "0";

            Carga_Combobox("NRequerimiento");
            NRequerimiento.SelectedValue = "0";

            Carga_Combobox("Ambito");
            Ambito.SelectedValue = Convert.ToString(dbPestana);

            Carga_Combobox("TipoDocumento");
            TipoDocumento.SelectedValue = "0";

            Carga_Combobox("DocumentoPrincipal");
            DocumentoPrincipal.SelectedValue = "0";

            Carga_Combobox("Resultado");
            Resultado.SelectedValue = "0";

            Carga_Combobox("Destinatario");
            Destinatario.SelectedValue = "0";

            Carga_Combobox("Tipo");
            Tipo.SelectedValue = "0";


        }


        protected void Carga_Combobox(string combobox)
        {

            ValidacionDocumentacion validacionDocumentacion = null;
            List<ValidacionDocumentacion> resp = null;

            switch (combobox)
            {



                case "FlujoDocumental":

                    /*
                    Hashtable campos = (Hashtable)ViewState["HashCampos"];


                    FlujoDocumentalSSFFAA.Items.Clear();
                    if(campos != null){
                        foreach(DictionaryEntry item in campos){
                            FlujoDocumentalSSFFAA.Items.Add(new ListItem(Convert.ToString(((Combobox)item.Value).nombreAtributo), Convert.ToString(item.Key)));
                        }
                    }
                    FlujoDocumentalSSFFAA.DataBind();
                    FlujoDocumentalSSFFAA.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    */



                    // Cargamos el combobox: Flujo Documental
                    validacionDocumentacion = (ValidacionDocumentacion)ViewState["validacionDocumentacion"];
                    validacionDocumentacion.ambito = new ParametroGenerico(Convert.ToInt32(dbPestana));
                    validacionDocumentacion.seccion = new ParametroGenerico(Convert.ToInt32(dbSeccion));

                    FlujoDocumental.Items.Clear();

                    resp = validacionDocumentacionDA.ListarFlujoDocumentalFiltro(validacionDocumentacion);
                    if (resp != null)
                    {
                        foreach (ValidacionDocumentacion item in resp)
                        {
                            FlujoDocumental.Items.Add(new ListItem(item.flujoDocumental.descripcion, Convert.ToString(item.flujoDocumental.id)));
                        }
                    }

                    FlujoDocumental.DataBind();
                    FlujoDocumental.Items.Insert(0, new ListItem("-- Seleccione --", "0"));


                    break;

                case "TipoEntrada":
                    // Cargamos el combobox: Tipo Salida

                    TipoEntrada.Items.Clear();
                    TipoEntrada.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    validacionDocumentacion = (ValidacionDocumentacion)ViewState["validacionDocumentacion"];
                    validacionDocumentacion.ambito = new ParametroGenerico(Convert.ToInt32(dbPestana));
                    validacionDocumentacion.seccion = new ParametroGenerico(Convert.ToInt32(dbSeccion));
                    validacionDocumentacion.flujoDocumental = new ParametroGenerico(Convert.ToInt32(rbTipo.ENTRADA)); ;

                    resp = validacionDocumentacionDA.ListarEntradaSalidaFiltro(validacionDocumentacion);

                    if (resp != null)
                    {
                        foreach (ValidacionDocumentacion item in resp)
                        {
                            TipoEntrada.Items.Add(new ListItem(item.tipoIO.descripcion, Convert.ToString(item.tipoIO.id)));
                        }
                    }

                    TipoEntrada.DataBind();


                    break;

                case "TipoSalida":
                    // Cargamos el combobox: Tipo Salida

                    TipoSalida.Items.Clear();
                    TipoSalida.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    validacionDocumentacion = (ValidacionDocumentacion)ViewState["validacionDocumentacion"];
                    validacionDocumentacion.ambito = new ParametroGenerico(Convert.ToInt32(dbPestana));
                    validacionDocumentacion.seccion = new ParametroGenerico(Convert.ToInt32(dbSeccion));
                    validacionDocumentacion.flujoDocumental = new ParametroGenerico(Convert.ToInt32(rbTipo.SALIDA));

                    resp = validacionDocumentacionDA.ListarEntradaSalidaFiltro(validacionDocumentacion);

                    if (resp != null)
                    {
                        foreach (ValidacionDocumentacion item in resp)
                        {
                            TipoSalida.Items.Add(new ListItem(item.tipoIO.descripcion, Convert.ToString(item.tipoIO.id)));
                        }
                    }

                    TipoSalida.DataBind();


                    break;


                case "Origen":
                    // Cargamos el combobox: Tipo Salida
                    Origen.Items.Clear();
                    Origen.DataBind();
                    Origen.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;


                case "NRequerimiento":
                    // Cargamos el combobox: Nº Requerimiento 
                    NRequerimiento.Items.Clear();
                    NRequerimiento.DataBind();
                    NRequerimiento.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;



                case "Ambito":
                    // Cargamos el combobox: Ambito
                    Ambito.Items.Clear();
                    Ambito.DataSource = pestanaDA.ListarPestania(rbPestana.CERO);
                    Ambito.DataTextField = "descripcion";
                    Ambito.DataValueField = "id";
                    Ambito.DataBind();
                    Ambito.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;


                case "Tipo":
                    // Cargamos el combobox: Tipo
                    Tipo.Items.Clear();
                    Tipo.DataBind();
                    Tipo.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                case "TipoDocumento":
                    // Cargamos el combobox: Tipo Documento
                    TipoDocumento.Items.Clear();
                    TipoDocumento.DataBind();
                    TipoDocumento.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                case "DocumentoPrincipal":
                    // Cargamos el combobox: Tipo Documento
                    DocumentoPrincipal.Items.Clear();
                    DocumentoPrincipal.DataBind();
                    DocumentoPrincipal.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                case "Resultado":
                    // Cargamos el combobox: Tipo Resultado
                    Resultado.Items.Clear();
                    Resultado.DataBind();
                    Resultado.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;


                case "Destinatario":
                    // Cargamos el combobox: Destinatario
                    Destinatario.Items.Clear();
                    Destinatario.DataBind();
                    Destinatario.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;


            };
        }


        //1 = entrada
        //2 = salida
        protected void FlujoDocumental_change(object sender, EventArgs e)
        {

            //TipoSalida.Items.Clear();
            //TipoSalida.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            //TipoSalida.DataBind();

            //TipoEntrada.Items.Clear();
            //TipoEntrada.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            //TipoEntrada.DataBind();


            LimpiarPorFlujoDocumental();

            if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.ENTRADA)
            {
                PanelTipoEntrada.Visible = true;
                PanelTipoSalida.Visible = false;
            }
            else if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.SALIDA)
            {
                PanelTipoSalida.Visible = true;
                PanelTipoEntrada.Visible = false;
            }
            else
            {
                PanelTipoSalida.Visible = false;
                PanelTipoEntrada.Visible = false;
            }


            if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.SALIDA)
            {
                /*
                TipoSalida.Items.Clear();
                TipoSalida.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                Hashtable campos = (Hashtable)ViewState["HashCampos"];
                campos = (Hashtable)((Combobox)(campos[Convert.ToInt32(FlujoDocumentalSSFFAA.SelectedValue)])).hash;


                if (campos != null)
                {
                    foreach (DictionaryEntry item in campos)
                    {
                        TipoSalida.Items.Add(new ListItem(Convert.ToString(((Combobox)item.Value).nombreAtributo), Convert.ToString(item.Key)));
                    }
                }
                
                TipoSalida.DataBind();
               



                TipoSalida.Items.Clear();
                TipoSalida.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                ValidacionDocumentacion validacionDocumentacion = (ValidacionDocumentacion)ViewState["validacionDocumentacion"];
                validacionDocumentacion.ambito = new ParametroGenerico(Convert.ToInt32(dbPestana));
                validacionDocumentacion.seccion = new ParametroGenerico(Convert.ToInt32(dbSeccion));
                validacionDocumentacion.flujoDocumental = new ParametroGenerico(Convert.ToInt32(FlujoDocumental.SelectedValue));

                List<ValidacionDocumentacion> resp = validacionDocumentacionDA.ListarEntradaSalidaFiltro(validacionDocumentacion);

                if (resp != null)
                {
                    foreach (ValidacionDocumentacion item in resp)
                    {
                        TipoSalida.Items.Add(new ListItem(item.tipoIO.descripcion, Convert.ToString(item.tipoIO.id)));
                    }
                }


                TipoSalida.DataBind();
                 */
            }


            if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.ENTRADA)
            {
                /*
                TipoEntrada.Items.Clear();
                TipoEntrada.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                Hashtable campos = (Hashtable)ViewState["HashCampos"];
                campos = (Hashtable)((Combobox)(campos[Convert.ToInt32(FlujoDocumentalSSFFAA.SelectedValue)])).hash;


                if (campos != null)
                {
                    foreach (DictionaryEntry item in campos)
                    {
                        TipoEntrada.Items.Add(new ListItem(Convert.ToString(((Combobox)item.Value).nombreAtributo), Convert.ToString(item.Key)));
                    }
                }
                
                TipoEntrada.DataBind();
                


                TipoEntrada.Items.Clear();
                TipoEntrada.Items.Insert(0, new ListItem("-- Seleccione --", "0"));


                ValidacionDocumentacion validacionDocumentacion = (ValidacionDocumentacion)ViewState["validacionDocumentacion"];
                validacionDocumentacion.ambito = new ParametroGenerico(Convert.ToInt32(dbPestana));
                validacionDocumentacion.seccion = new ParametroGenerico(Convert.ToInt32(dbSeccion));
                validacionDocumentacion.flujoDocumental = new ParametroGenerico(Convert.ToInt32(FlujoDocumental.SelectedValue));

                List<ValidacionDocumentacion> resp = validacionDocumentacionDA.ListarEntradaSalidaFiltro(validacionDocumentacion);

                if (resp != null)
                {
                    foreach (ValidacionDocumentacion item in resp)
                    {
                        TipoEntrada.Items.Add(new ListItem(item.tipoIO.descripcion, Convert.ToString(item.tipoIO.id)));
                    }
                }

                TipoEntrada.DataBind();
                */

            }


            UpdatePanelTipoSalida.Update();
            UpdatePanelTipoEntrada.Update();



        }





        protected void LimpiarPorFlujoDocumental()
        {

            TipoEntrada.SelectedValue = "0";
            TipoSalida.SelectedValue = "0";
            Tipo.SelectedValue = "0";
            TipoDocumento.SelectedValue = "0";
            DocumentoPrincipal.SelectedValue = "0";
            Numero.Text = "";
            Fecha.Text = "";
            NuevaFecha.Text = "";
            NumeroCI.Text = "";
            FechaCI.Text = "";
            Resultado.SelectedValue = "0";
            Destinatario.SelectedValue = "0";
            Origen.SelectedValue = "0";
            NRequerimiento.SelectedValue = "0";

            ViewState["Documentos_Asociados"] = null;
            GridViewSalidaDocumentoAsociado_CargaGrilla();


            PanelTipoEntrada.Visible = false;
            PanelTipoSalida.Visible = false;
            PanelOrigen.Visible = false;
            PanelAmbito.Visible = false;
            PanelTipo.Visible = false;
            PanelDocumentosAmbito.Visible = false;
            PanelTipoDocumento.Visible = false;
            PanelDocumentoPrincipal.Visible = false;
            PanelNumero.Visible = false;
            PanelFecha.Visible = false;
            PanelNuevaFecha.Visible = false;
            PanelNumeroRequerimiento.Visible = false;
            PanelNumeroCI.Visible = false;
            PanelFechaCI.Visible = false;
            PanelResultado.Visible = false;
            PanelDestinatario.Visible = false;
            PanelArchivo.Visible = false;
            PanelListaRequerimientos.Visible = false;
            PanelMensajePlanos14TER.Visible = false;

            ListViewEntradaRespuestaRequerimiento_Carga();
            //FlujoDocumental.Focus();

            UpdatePanelTipoSalida.Update();
            UpdatePanelTipoEntrada.Update();
            UpdatePanelOrigen.Update();
            UpdatePanelDestinatario.Update();
            UpdatePanelTipoDocumento.Update();
            UpdatePanelDocumentoPrincipal.Update();
            UpdatePanelNumeroRequerimiento.Update();
            UpdatePanelListaRequerimientos.Update();
            UpdatePanelAmbito.Update();
            UpdatePanelTipo.Update();
            UpdatePanelDocumentosAmbito.Update();
            UpdatePanelNumero.Update();
            UpdatePanelFecha.Update();
            UpdatePanelNuevaFecha.Update();
            UpdatePanelNumeroCI.Update();
            UpdatePanelFechaCI.Update();
            UpdatePanelResultado.Update();
            UpdatePanelArchivo.Update();
            UpdatePanelMensajePlanos14TER.Update();


        }


        //3 = Requerimiento con Respuesta
        //4 = Informativo
        protected void TipoSalida_change(object sender, EventArgs e)
        {

            LimpiarPorTipoSalida();

            Destinatario.Items.Clear();
            Destinatario.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            if (Convert.ToInt32(TipoSalida.SelectedValue) > 0)
            {

                ValidacionDocumentacion validacionDocumentacion = (ValidacionDocumentacion)ViewState["validacionDocumentacion"];
                validacionDocumentacion.flujoDocumental = new ParametroGenerico(Convert.ToInt32(FlujoDocumental.SelectedValue));
                validacionDocumentacion.tipoIO = new ParametroGenerico(Convert.ToInt32(TipoSalida.SelectedValue));
                validacionDocumentacion.ambito = new ParametroGenerico(Convert.ToInt32(dbPestana));
                validacionDocumentacion.seccion = new ParametroGenerico(Convert.ToInt32(dbSeccion));

                List<ValidacionDocumentacion> resp = validacionDocumentacionDA.ListarTipoDestinatarioFiltro(validacionDocumentacion);

                if (resp != null)
                {
                    foreach (ValidacionDocumentacion item in resp)
                    {
                        Destinatario.Items.Add(new ListItem(item.tipoDestinatario.descripcion, Convert.ToString(item.tipoDestinatario.id)));
                    }
                }

            }

            Destinatario.DataBind();



            //CAMPOS QUE OBLIGATORIAMENTE APARECERAN

            //PANEL DESTINATARIO
            PanelDestinatario.Visible = true;
            //TIPO DOCUMENTO
            PanelTipoDocumento.Visible = true;


            UpdatePanelDestinatario.Update();
            UpdatePanelTipoDocumento.Update();
        }



        protected void LimpiarPorTipoSalida()
        {

            TipoEntrada.SelectedValue = "0";
            Tipo.SelectedValue = "0";
            TipoDocumento.SelectedValue = "0";
            DocumentoPrincipal.SelectedValue = "0";
            Numero.Text = "";
            Fecha.Text = "";
            NuevaFecha.Text = "";
            NumeroCI.Text = "";
            FechaCI.Text = "";
            Resultado.SelectedValue = "0";
            Destinatario.SelectedValue = "0";
            Origen.SelectedValue = "0";
            NRequerimiento.SelectedValue = "0";

            ViewState["Documentos_Asociados"] = null;
            GridViewSalidaDocumentoAsociado_CargaGrilla();

            PanelTipoEntrada.Visible = false;
            PanelOrigen.Visible = false;
            PanelAmbito.Visible = false;
            PanelTipo.Visible = false;
            PanelDocumentosAmbito.Visible = false;
            PanelTipoDocumento.Visible = false;
            PanelDocumentoPrincipal.Visible = false;
            PanelNumero.Visible = false;
            PanelFecha.Visible = false;
            PanelNuevaFecha.Visible = false;
            PanelNumeroRequerimiento.Visible = false;
            PanelNumeroCI.Visible = false;
            PanelFechaCI.Visible = false;
            PanelResultado.Visible = false;
            PanelDestinatario.Visible = false;
            PanelArchivo.Visible = false;
            PanelListaRequerimientos.Visible = false;

            ErroresSuperior.Text = "";
            PanelErroresSuperior.Visible = false;
            UpdatePanelErroresSuperior.Update();

            ErroresInferior.Text = "";
            PanelErroresInferior.Visible = false;
            UpdatePanelErroresInferior.Update();


            ListViewEntradaRespuestaRequerimiento_Carga();
            //FlujoDocumental.Focus();


            UpdatePanelTipoEntrada.Update();
            UpdatePanelOrigen.Update();
            UpdatePanelDestinatario.Update();
            UpdatePanelTipoDocumento.Update();
            UpdatePanelDocumentoPrincipal.Update();
            UpdatePanelNumeroRequerimiento.Update();
            UpdatePanelListaRequerimientos.Update();
            UpdatePanelAmbito.Update();
            UpdatePanelTipo.Update();
            UpdatePanelDocumentosAmbito.Update();
            UpdatePanelNumero.Update();
            UpdatePanelFecha.Update();
            UpdatePanelNuevaFecha.Update();
            UpdatePanelNumeroCI.Update();
            UpdatePanelFechaCI.Update();
            UpdatePanelResultado.Update();
            UpdatePanelArchivo.Update();

        }


        //5 = Respuesta a un Requerimiento
        //6 = Ingreso sin Requerimiento
        protected void TipoEntrada_change(object sender, EventArgs e)
        {

            LimpiarPorTipoEntrada();


            Origen.Items.Clear();
            Origen.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            if (Convert.ToInt32(TipoEntrada.SelectedValue) > 0)
            {

                ValidacionDocumentacion validacionDocumentacion = (ValidacionDocumentacion)ViewState["validacionDocumentacion"];
                validacionDocumentacion.flujoDocumental = new ParametroGenerico(Convert.ToInt32(FlujoDocumental.SelectedValue));
                validacionDocumentacion.tipoIO = new ParametroGenerico(Convert.ToInt32(TipoEntrada.SelectedValue));
                validacionDocumentacion.ambito = new ParametroGenerico(Convert.ToInt32(dbPestana));
                validacionDocumentacion.seccion = new ParametroGenerico(Convert.ToInt32(dbSeccion));

                List<ValidacionDocumentacion> resp = validacionDocumentacionDA.ListarTipoOrigenFiltro(validacionDocumentacion);

                if (resp != null)
                {
                    foreach (ValidacionDocumentacion item in resp)
                    {
                        Origen.Items.Add(new ListItem(item.tipoDestinatario.descripcion, Convert.ToString(item.tipoDestinatario.id)));
                    }
                }

            }

            Origen.DataBind();


            //CAMPOS QUE OBLIGATORIAMENTE APARECERAN

            //ORIGEN
            PanelOrigen.Visible = true;
            //TIPO DOCUMENTO
            PanelTipoDocumento.Visible = true;


            UpdatePanelOrigen.Update();
            UpdatePanelTipoDocumento.Update();
        }


        protected void LimpiarPorTipoEntrada()
        {

            TipoSalida.SelectedValue = "0";
            Tipo.SelectedValue = "0";
            TipoDocumento.SelectedValue = "0";
            DocumentoPrincipal.SelectedValue = "0";
            Numero.Text = "";
            Fecha.Text = "";
            NuevaFecha.Text = "";
            NumeroCI.Text = "";
            FechaCI.Text = "";
            Resultado.SelectedValue = "0";
            Destinatario.SelectedValue = "0";
            Origen.SelectedValue = "0";
            NRequerimiento.SelectedValue = "0";

            ViewState["Documentos_Asociados"] = null;
            GridViewSalidaDocumentoAsociado_CargaGrilla();

            PanelTipoSalida.Visible = false;
            PanelOrigen.Visible = false;
            PanelAmbito.Visible = false;
            PanelTipo.Visible = false;
            PanelDocumentosAmbito.Visible = false;
            PanelDocumentoPrincipal.Visible = false;
            PanelTipoDocumento.Visible = false;
            PanelNumero.Visible = false;
            PanelFecha.Visible = false;
            PanelNuevaFecha.Visible = false;
            PanelNumeroRequerimiento.Visible = false;
            PanelNumeroCI.Visible = false;
            PanelFechaCI.Visible = false;
            PanelResultado.Visible = false;
            PanelDestinatario.Visible = false;
            PanelArchivo.Visible = false;
            PanelListaRequerimientos.Visible = false;

            ErroresSuperior.Text = "";
            PanelErroresSuperior.Visible = false;
            UpdatePanelErroresSuperior.Update();

            ErroresInferior.Text = "";
            PanelErroresInferior.Visible = false;
            UpdatePanelErroresInferior.Update();

            ListViewEntradaRespuestaRequerimiento_Carga();
            //FlujoDocumental.Focus();


            UpdatePanelTipoSalida.Update();
            UpdatePanelOrigen.Update();
            UpdatePanelDestinatario.Update();
            UpdatePanelTipoDocumento.Update();
            UpdatePanelDocumentoPrincipal.Update();
            UpdatePanelNumeroRequerimiento.Update();
            UpdatePanelListaRequerimientos.Update();
            UpdatePanelAmbito.Update();
            UpdatePanelTipo.Update();
            UpdatePanelDocumentosAmbito.Update();
            UpdatePanelNumero.Update();
            UpdatePanelFecha.Update();
            UpdatePanelNuevaFecha.Update();
            UpdatePanelNumeroCI.Update();
            UpdatePanelFechaCI.Update();
            UpdatePanelResultado.Update();
            UpdatePanelArchivo.Update();

        }


        public void NumeroCI_TextChanged(object sender, EventArgs e)
        {

            try
            {


                if (!NumeroCI.Text.Trim().Equals("") && !FechaCI.Text.Trim().Equals(""))
                {

                    DateTime fechaAuxCI = Convert.ToDateTime(FechaCI.Text);
                    int anio = fechaAuxCI.Year;


                    List<SolicitudConcesion> solicitudes = requerimientoService.ObtenerSolicitudPorCIusado(Convert.ToInt32(IdSolicitud.Text), Convert.ToInt32(NumeroCI.Text.Trim()), anio);
                    if (solicitudes != null && solicitudes.Count > 0)
                    {
                        String mensaje = "El Número C.I. ingresado esta presente en la(s) siguiente(s) solicitude(s): ";
                        foreach (SolicitudConcesion solAux in solicitudes)
                        {
                            mensaje = mensaje + "Pert: " + solAux.numPert + " - ";
                        }
                        NumeroCIMensaje.Text = mensaje;
                    }
                    else
                    {
                        NumeroCIMensaje.Text = "";
                    }
                }
                else
                {
                    NumeroCIMensaje.Text = "";
                }

            }
            catch (Exception)
            {
                NumeroCIMensaje.Text = "";
            }

        }


        //MUESTRA/OCULTA CAMPOS
        private void controlarCamposLogicos(int idTipoIO)
        {

            //ENTRADA
            if (idTipoIO == rbTipo.INGRESO_SIN_REQUERIMIENTO)
            {

                PanelOrigen.Visible = true;
                PanelAmbito.Visible = true;
                PanelTipo.Visible = true;
            }


            //ENTRADA
            if (idTipoIO == rbTipo.RESPUESTA_A_UN_REQUERIMIENTO)
            {

                PanelOrigen.Visible = true;
                PanelAmbito.Visible = false;
                PanelNumeroRequerimiento.Visible = true;

                NRequerimiento.Items.Clear();
                NRequerimiento.Items.Insert(0, new ListItem("-- Seleccione --", "0"));


                DataTable data = requerimientoService.ListarRequerimientosIdSolicitud(Convert.ToInt32(IdSolicitud.Text), dbSeccion, Convert.ToInt32(Origen.SelectedValue), Convert.ToInt32(TipoDocumento.SelectedValue));
                if (data != null)
                {
                    foreach (DataRow row in data.Rows)
                    {
                        NRequerimiento.Items.Add(new ListItem(Convert.ToString(row["nombreReq"]), Convert.ToString(row["idDocGeneral"])));
                    }

                }
                NRequerimiento.DataBind();
                PanelListaRequerimientos.Visible = false;
                ListViewEntradaRespuestaRequerimiento_Carga();

            }


            //SALIDA
            if (idTipoIO == rbTipo.INFORMATIVO)
            {
                PanelDestinatario.Visible = true;
                PanelAmbito.Visible = true;
                PanelTipo.Visible = true;
            }

            //SALIDA
            if (idTipoIO == rbTipo.REQUERIMIENTO_CON_RESPUESTA)
            {
                PanelDestinatario.Visible = true;
                PanelAmbito.Visible = true;
                PanelTipo.Visible = true;
                PanelDocumentosAmbito.Visible = true;
            }


            //AMBITO (PARTICULARIDAD - EL AMBITO SE SABE DE ANTEMANO)
            PanelAmbito.Visible = false;
            Ambito.SelectedValue = Convert.ToString(dbPestana);
            Ambito_change(null, null);

        }



        //MUESTRA/OCULTA CAMPOS EN BASE AL TEMA, APLICA PARA (SALIDA - INFORMATIVA, ENTRADA - INGRESO SIN REQUERIMIENTO)
        private void controlarCamposPorTema()
        {

            if ((Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.SALIDA && Convert.ToInt32(TipoSalida.SelectedValue) == rbTipo.INFORMATIVO) || (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.ENTRADA && Convert.ToInt32(TipoEntrada.SelectedValue) == rbTipo.INGRESO_SIN_REQUERIMIENTO))
            {

                if (Convert.ToInt32(Tipo.SelectedValue) > 0)
                {

                    Hashtable camposObligatorios = null;
                    Hashtable hashIdTipoIO = null;
                    Hashtable hashIdTipoOrigenDestinatario = null;
                    Hashtable hashIdPestana = null;
                    Hashtable hashIdTipoDocumento = null;
                    ValidacionDocumentacion validacionDocumentacion = null;


                    camposObligatorios = (Hashtable)ViewState["HashCampos"];

                    if (camposObligatorios != null)
                    {
                        if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.SALIDA)
                        {
                            hashIdTipoIO = (Hashtable)camposObligatorios[Convert.ToInt32(TipoSalida.SelectedValue)];
                        }
                        if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.ENTRADA)
                        {
                            hashIdTipoIO = (Hashtable)camposObligatorios[Convert.ToInt32(TipoEntrada.SelectedValue)];
                        }

                    }

                    if (hashIdTipoIO != null)
                    {
                        if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.SALIDA)
                        {
                            hashIdTipoOrigenDestinatario = (Hashtable)hashIdTipoIO[Convert.ToInt32(Destinatario.SelectedValue)];
                        }
                        if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.ENTRADA)
                        {
                            hashIdTipoOrigenDestinatario = (Hashtable)hashIdTipoIO[Convert.ToInt32(Origen.SelectedValue)];
                        }
                    }

                    if (hashIdTipoOrigenDestinatario != null)
                    {
                        hashIdPestana = (Hashtable)hashIdTipoOrigenDestinatario[Convert.ToInt32(Ambito.SelectedValue)];
                    }


                    if (hashIdPestana != null)
                    {
                        hashIdTipoDocumento = (Hashtable)hashIdPestana[Convert.ToInt32(TipoDocumento.SelectedValue)];
                    }


                    validacionDocumentacion = (ValidacionDocumentacion)hashIdTipoDocumento[Convert.ToInt32(Tipo.SelectedValue)];



                    if (validacionDocumentacion != null)
                    {

                        //NUMERO
                        if (validacionDocumentacion.numero == obligatoriedadCampo.noAplica)
                        {
                            PanelNumero.Visible = false;
                        }
                        else
                        {
                            PanelNumero.Visible = true;
                            if (validacionDocumentacion.numero == obligatoriedadCampo.obligatorio)
                            {
                                RequeridoNumero.Text = "*";
                            }
                            else
                            {
                                RequeridoNumero.Text = "";
                            }
                        }

                        //FECHA
                        if (validacionDocumentacion.fecha == obligatoriedadCampo.noAplica)
                        {
                            PanelFecha.Visible = false;
                        }
                        else
                        {
                            PanelFecha.Visible = true;
                            string script = "calendario('" + Fecha.ClientID + "','" + fechaImgDinamica.ClientID + "');";
                            ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + Fecha.ClientID, script.ToString(), true);
                            if (validacionDocumentacion.fecha == obligatoriedadCampo.obligatorio)
                            {
                                RequeridoFecha.Text = "*";
                            }
                            else
                            {
                                RequeridoFecha.Text = "";
                            }
                        }


                        //NUEVA FECHA
                        if (validacionDocumentacion.nuevaFecha == obligatoriedadCampo.noAplica)
                        {
                            PanelNuevaFecha.Visible = false;
                        }
                        else
                        {
                            PanelNuevaFecha.Visible = true;
                            string script = "calendario('" + NuevaFecha.ClientID + "','" + nuevaFechaImgDinamica.ClientID + "');";
                            ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + NuevaFecha.ClientID, script.ToString(), true);

                        }

                        //NUMEROCI
                        if (validacionDocumentacion.numeroCI == obligatoriedadCampo.noAplica)
                        {
                            PanelNumeroCI.Visible = false;
                        }
                        else
                        {
                            PanelNumeroCI.Visible = true;
                            if (validacionDocumentacion.numeroCI == obligatoriedadCampo.obligatorio)
                            {
                                RequeridoNumeroCI.Text = "*";
                            }
                            else
                            {
                                RequeridoNumeroCI.Text = "";
                            }
                        }


                        //FECHACI
                        if (validacionDocumentacion.fechaCI == obligatoriedadCampo.noAplica)
                        {
                            PanelFechaCI.Visible = false;
                        }
                        else
                        {
                            PanelFechaCI.Visible = true;
                            string script = "calendarioCI('" + FechaCI.ClientID + "','" + fechaCIImgDinamica.ClientID + "','" + NumeroCI.ClientID + "')";
                            ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + FechaCI.ClientID, script.ToString(), true);
                            if (validacionDocumentacion.fechaCI == obligatoriedadCampo.obligatorio)
                            {
                                RequeridoFechaCI.Text = "*";
                            }
                            else
                            {
                                RequeridoFechaCI.Text = "";
                            }
                        }


                        //PANEL ARCHIVO
                        if (validacionDocumentacion.archivoBinario == obligatoriedadCampo.noAplica)
                        {
                            PanelArchivo.Visible = false;
                        }
                        else
                        {
                            PanelArchivo.Visible = true;
                            if (validacionDocumentacion.archivoBinario == obligatoriedadCampo.obligatorio)
                            {
                                RequeridoArchivoAdjunto.Text = "*";
                            }
                            else
                            {
                                RequeridoArchivoAdjunto.Text = "";
                            }
                        }
                    }
                }
                else
                {
                    PanelNumero.Visible = false;
                    PanelFecha.Visible = false;
                    PanelNuevaFecha.Visible = false;
                    PanelNumeroCI.Visible = false;
                    PanelFechaCI.Visible = false;
                    PanelArchivo.Visible = false;
                }
            }


            UpdatePanelNumero.Update();
            UpdatePanelFecha.Update();
            UpdatePanelNuevaFecha.Update();
            UpdatePanelNumeroCI.Update();
            UpdatePanelFechaCI.Update();
            UpdatePanelArchivo.Update();

        }



        //MOSTRAR CAMPOS OPCIONALES (SALIDA REQUERIMIENTOS CON RESPUESTA)
        private void mostrarCamposOpcionales(DocumentoAmbito documentoAmbito)
        {

            Hashtable camposObligatorios = null;
            Hashtable hashIdTipoIO = null;
            Hashtable hashIdTipoOrigenDestinatario = null;
            Hashtable hashIdPestana = null;
            Hashtable hashIdTipoDocumento = null;
            ValidacionDocumentacion validacionDocumentacion = null;


            camposObligatorios = (Hashtable)ViewState["HashCampos"];

            if (camposObligatorios != null)
            {
                if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.SALIDA)
                {
                    hashIdTipoIO = (Hashtable)camposObligatorios[Convert.ToInt32(TipoSalida.SelectedValue)];
                }
                if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.ENTRADA)
                {
                    hashIdTipoIO = (Hashtable)camposObligatorios[Convert.ToInt32(TipoEntrada.SelectedValue)];
                }

            }

            if (hashIdTipoIO != null)
            {
                if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.SALIDA)
                {
                    hashIdTipoOrigenDestinatario = (Hashtable)hashIdTipoIO[Convert.ToInt32(Destinatario.SelectedValue)];
                }
                if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.ENTRADA)
                {
                    hashIdTipoOrigenDestinatario = (Hashtable)hashIdTipoIO[Convert.ToInt32(Origen.SelectedValue)];
                }
            }

            if (hashIdTipoOrigenDestinatario != null)
            {
                hashIdPestana = (Hashtable)hashIdTipoOrigenDestinatario[Convert.ToInt32(documentoAmbito.ambito.id)];
            }


            if (hashIdPestana != null)
            {
                hashIdTipoDocumento = (Hashtable)hashIdPestana[Convert.ToInt32(TipoDocumento.SelectedValue)];
            }


            if (hashIdTipoDocumento != null)
            {
                validacionDocumentacion = (ValidacionDocumentacion)hashIdTipoDocumento[documentoAmbito.tipo.id];
            }



            if (validacionDocumentacion != null)
            {

                //NUMERO
                if (validacionDocumentacion.numero == obligatoriedadCampo.noAplica)
                {
                    PanelNumero.Visible = false;
                }
                else
                {
                    PanelNumero.Visible = true;

                    if (validacionDocumentacion.numero == obligatoriedadCampo.obligatorio)
                    {
                        RequeridoNumero.Text = "*";
                    }
                    else
                    {
                        RequeridoNumero.Text = "";
                    }
                }

                //FECHA
                if (validacionDocumentacion.fecha == obligatoriedadCampo.noAplica)
                {
                    PanelFecha.Visible = false;
                }
                else
                {
                    PanelFecha.Visible = true;
                    string script = "calendario('" + Fecha.ClientID + "','" + fechaImgDinamica.ClientID + "');";
                    ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + Fecha.ClientID, script.ToString(), true);

                    if (validacionDocumentacion.fecha == obligatoriedadCampo.obligatorio)
                    {
                        RequeridoFecha.Text = "*";
                    }
                    else
                    {
                        RequeridoFecha.Text = "";
                    }
                }


                //NUMEROCI
                if (validacionDocumentacion.numeroCI == obligatoriedadCampo.noAplica)
                {
                    PanelNumeroCI.Visible = false;
                }
                else
                {
                    PanelNumeroCI.Visible = true;

                    if (validacionDocumentacion.numeroCI == obligatoriedadCampo.obligatorio)
                    {
                        RequeridoNumeroCI.Text = "*";
                    }
                    else
                    {
                        RequeridoNumeroCI.Text = "";
                    }

                }


                //FECHACI
                if (validacionDocumentacion.fechaCI == obligatoriedadCampo.noAplica)
                {
                    PanelFechaCI.Visible = false;
                }
                else
                {
                    PanelFechaCI.Visible = true;
                    string script = "calendarioCI('" + FechaCI.ClientID + "','" + fechaCIImgDinamica.ClientID + "','" + NumeroCI.ClientID + "')";
                    ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + FechaCI.ClientID, script.ToString(), true);

                    if (validacionDocumentacion.fechaCI == obligatoriedadCampo.obligatorio)
                    {
                        RequeridoFechaCI.Text = "*";
                    }
                    else
                    {
                        RequeridoFechaCI.Text = "";
                    }

                }


                //PANEL ARCHIVO
                if (validacionDocumentacion.archivoBinario == obligatoriedadCampo.noAplica)
                {
                    PanelArchivo.Visible = false;
                }
                else
                {
                    PanelArchivo.Visible = true;

                    if (validacionDocumentacion.archivoBinario == obligatoriedadCampo.obligatorio)
                    {
                        RequeridoArchivoAdjunto.Text = "*";
                    }
                    else
                    {
                        RequeridoArchivoAdjunto.Text = "";
                    }
                }
            }

            UpdatePanelNumero.Update();
            UpdatePanelFecha.Update();
            UpdatePanelNumeroCI.Update();
            UpdatePanelFechaCI.Update();
            UpdatePanelArchivo.Update();
        }




        //OCULTAS CAMPOS OPCIONALES (SALIDA REQUERIMIENTOS CON RESPUESTA)
        private void ocultarCamposOpcionales()
        {
            PanelNumero.Visible = false;
            PanelFecha.Visible = false;
            PanelNumeroCI.Visible = false;
            PanelFechaCI.Visible = false;
            PanelArchivo.Visible = false;

            UpdatePanelNumero.Update();
            UpdatePanelFecha.Update();
            UpdatePanelNumeroCI.Update();
            UpdatePanelFechaCI.Update();
            UpdatePanelArchivo.Update();

        }



        //VALIDACIONES CAMPOS OBLIGATORIOS
        private List<String> validarIngresoRequerimiento(int idTipoIO, Requerimiento requerimiento)
        {


            List<String> errores = new List<string>();


            //CAMPOS OBLIGATORIOS

            //ORIGEN
            if (requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
            {
                if (Convert.ToInt32(Origen.SelectedValue) < 1)
                {
                    errores.Add("Seleccione Origen");
                }
            }

            //DESTINATARIO
            if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
            {
                if (Convert.ToInt32(Destinatario.SelectedValue) < 1)
                {
                    errores.Add("Seleccione Destinatario");
                }
            }


            //TIPO DOCUMENTO
            if (Convert.ToInt32(TipoDocumento.SelectedValue) < 1)
            {
                errores.Add("Seleccione Tipo Documento");
            }




            //ENTRADA
            if (idTipoIO == rbTipo.INGRESO_SIN_REQUERIMIENTO)
            {
                //AMBITO 
                if (Convert.ToInt32(Ambito.SelectedValue) < 1)
                {
                    errores.Add("Seleccione Ámbito");
                }

                //TIPO 
                if (Convert.ToInt32(Tipo.SelectedValue) < 1)
                {
                    errores.Add("Seleccione Tema");
                }
            }

            //ENTRADA
            if (idTipoIO == rbTipo.RESPUESTA_A_UN_REQUERIMIENTO)
            {
                //NUMERO REQUERIMIENTO
                if (Convert.ToInt32(NRequerimiento.SelectedValue) < 1)
                {
                    errores.Add("Seleccione Nº Requerimiento");
                }
                else
                {

                    List<ListViewDataItem> seleccionados = ListViewEntradaRespuestaRequerimiento.GetSelectedDataKeys2("chkSeleccionado");

                    requerimiento.ambitoTipo = new List<DocumentoAmbito>();
                    DocumentoAmbito documentoAmbito = null;

                    if (seleccionados.Count() > 0)
                    {
                        foreach (ListViewDataItem item in (List<ListViewDataItem>)seleccionados)
                        {

                            documentoAmbito = new DocumentoAmbito();
                            var idDocPestana = item.FindControl("HiddenIdDocPestana") as HiddenField;
                            var idDocGeneralResp = item.FindControl("HiddenIdDocGeneralResp") as HiddenField;
                            var idTipo = item.FindControl("HiddenTipoId") as HiddenField;
                            var respuesta = item.FindControl("AmbitoTipoResultado") as DropDownList;

                            documentoAmbito.idDocPestana = Convert.ToInt32(idDocPestana.Value);
                            if (!idDocGeneralResp.Value.Equals(""))
                            {
                                documentoAmbito.idDocGeneralResp = Convert.ToInt32(idDocGeneralResp.Value);
                            }
                            documentoAmbito.tipo = new ParametroGenerico(Convert.ToInt32(idTipo.Value));
                            documentoAmbito.estadoResultadoResp = new ParametroGenerico(Convert.ToInt32(respuesta.SelectedValue));
                            documentoAmbito.seccion = new ParametroGenerico(this.dbSeccion);

                            requerimiento.ambitoTipo.Add(documentoAmbito);
                        }
                    }
                    else
                    {
                        errores.Add("Seleccione al menos 1 documento");
                    }
                }
            }

            //SALIDA
            if (idTipoIO == rbTipo.INFORMATIVO)
            {
                //AMBITO 
                if (Convert.ToInt32(Ambito.SelectedValue) < 1)
                {
                    errores.Add("Seleccione Ámbito");
                }

                //TIPO 
                if (Convert.ToInt32(Tipo.SelectedValue) < 1)
                {
                    errores.Add("Seleccione Tema");
                }
            }

            //SALIDA
            if (idTipoIO == rbTipo.REQUERIMIENTO_CON_RESPUESTA)
            {

                List<DocumentoAmbito> List_Documentos_Asociados = (List<DocumentoAmbito>)ViewState["Documentos_Asociados"];
                int cantidad = 0;

                if (List_Documentos_Asociados != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in List_Documentos_Asociados)
                    {
                        if (documentoAmbito.accion == accion.INGRESAR || documentoAmbito.accion == accion.LISTADO || documentoAmbito.accion == accion.MODIFICAR)
                        {
                            cantidad++;
                            break;
                        }
                    }
                }

                if (cantidad == 0)
                {
                    errores.Add("Ingrese al menos 1 documento");
                }
                else
                {
                    requerimiento.ambitoTipo = List_Documentos_Asociados;
                }
            }



            //SI HAY ERRORES EN ESTE PUNTO SE DEBEN RESOLVER ANTES DE SEGUIR CON LA VALIDACION
            if (errores.Count > 0)
            {
                return errores;
            }


            //VALIDACION DE RESOLUCION COMPLEMENTARIA / INFORME COMPLEMENTARIO (DEBE HABER SELECCIONADO EL PRINCIPAL)

            if (Convert.ToInt32(TipoDocumento.SelectedValue) == rbTipo.INFORME_COMPLEMENTARIO && Convert.ToInt32(DocumentoPrincipal.SelectedValue) < 1)
            {
                errores.Add("Seleccione Informe Principal");
            }

            if (Convert.ToInt32(TipoDocumento.SelectedValue) == rbTipo.RESOLUCION_COMPLEMENTARIA && Convert.ToInt32(DocumentoPrincipal.SelectedValue) < 1)
            {
                errores.Add("Seleccione Resolución Principal");
            }


            //CAMPOS OPCIONALES

            Hashtable camposObligatorios = null;
            Hashtable hashIdTipoIO = null;
            Hashtable hashIdTipoOrigenDestinatario = null;
            Hashtable hashIdPestana = null;
            Hashtable hashIdTipoDocumento = null;
            ValidacionDocumentacion validacionDocumentacion = null;


            camposObligatorios = (Hashtable)ViewState["HashCampos"];

            if (camposObligatorios != null)
            {
                if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.SALIDA)
                {
                    hashIdTipoIO = (Hashtable)camposObligatorios[Convert.ToInt32(TipoSalida.SelectedValue)];
                }
                if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.ENTRADA)
                {
                    hashIdTipoIO = (Hashtable)camposObligatorios[Convert.ToInt32(TipoEntrada.SelectedValue)];
                }

            }

            if (hashIdTipoIO != null)
            {
                if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.SALIDA)
                {
                    hashIdTipoOrigenDestinatario = (Hashtable)hashIdTipoIO[Convert.ToInt32(Destinatario.SelectedValue)];
                }
                if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.ENTRADA)
                {
                    hashIdTipoOrigenDestinatario = (Hashtable)hashIdTipoIO[Convert.ToInt32(Origen.SelectedValue)];
                }
            }

            if (hashIdTipoOrigenDestinatario != null)
            {
                hashIdPestana = (Hashtable)hashIdTipoOrigenDestinatario[Convert.ToInt32(Ambito.SelectedValue)];
            }


            if (hashIdPestana != null)
            {
                hashIdTipoDocumento = (Hashtable)hashIdPestana[Convert.ToInt32(TipoDocumento.SelectedValue)];
            }


            if (hashIdTipoDocumento != null)
            {


                if (idTipoIO == rbTipo.INGRESO_SIN_REQUERIMIENTO)
                {
                    validacionDocumentacion = (ValidacionDocumentacion)hashIdTipoDocumento[Convert.ToInt32(Tipo.SelectedValue)];
                }

                if (idTipoIO == rbTipo.INFORMATIVO)
                {
                    validacionDocumentacion = (ValidacionDocumentacion)hashIdTipoDocumento[Convert.ToInt32(Tipo.SelectedValue)];
                }

                if (idTipoIO == rbTipo.REQUERIMIENTO_CON_RESPUESTA)
                {
                    //SE CONSIDERA PARA VALIDACION EL PRIMER ELEMENTO DE LA LISTA
                    List<DocumentoAmbito> List_Documentos_Asociados = (List<DocumentoAmbito>)ViewState["Documentos_Asociados"];
                    foreach (DocumentoAmbito documentoAmbito in List_Documentos_Asociados)
                    {
                        if (documentoAmbito.accion == accion.INGRESAR || documentoAmbito.accion == accion.LISTADO || documentoAmbito.accion == accion.MODIFICAR)
                        {
                            validacionDocumentacion = (ValidacionDocumentacion)hashIdTipoDocumento[Convert.ToInt32(documentoAmbito.tipo.id)];
                            break;
                        }
                    }
                }

                if (idTipoIO == rbTipo.RESPUESTA_A_UN_REQUERIMIENTO)
                {

                    List<ListViewDataItem> seleccionados = ListViewEntradaRespuestaRequerimiento.GetSelectedDataKeys2("chkSeleccionado");

                    //MAS ARRIBA SE VALIDO QUE HAYA SELECCIONADO AL MENOS 1 
                    if (seleccionados.Count() > 0)
                    {
                        foreach (ListViewDataItem item in (List<ListViewDataItem>)seleccionados)
                        {
                            var idTipo = item.FindControl("HiddenTipoId") as HiddenField;
                            validacionDocumentacion = (ValidacionDocumentacion)hashIdTipoDocumento[Convert.ToInt32(idTipo.Value)];
                            break;
                        }
                    }
                }
            }


            if (validacionDocumentacion != null)
            {


                //NUMERO
                if (validacionDocumentacion.numero == obligatoriedadCampo.obligatorio)
                {
                    if (Numero.Text.Trim().Equals(""))
                    {
                        errores.Add("Ingrese Número");
                    }
                }

                //FECHA
                if (validacionDocumentacion.fecha == obligatoriedadCampo.obligatorio)
                {
                    if (Fecha.Text.Trim().Equals(""))
                    {
                        errores.Add("Ingrese Fecha");
                    }
                }

                //NUEVA FECHA
                if (validacionDocumentacion.nuevaFecha == obligatoriedadCampo.obligatorio)
                {
                    if (NuevaFecha.Text.Trim().Equals(""))
                    {
                        errores.Add("Ingrese Nueva Fecha");
                    }
                }

                //NUMERO CI
                if (validacionDocumentacion.numeroCI == obligatoriedadCampo.obligatorio)
                {
                    if (NumeroCI.Text.Trim().Equals(""))
                    {
                        errores.Add("Ingrese Número C.I.");
                    }
                }

                //FECHA CI
                if (validacionDocumentacion.fechaCI == obligatoriedadCampo.obligatorio)
                {
                    if (FechaCI.Text.Trim().Equals(""))
                    {
                        errores.Add("Ingrese Fecha C.I.");
                    }
                }

                //ARCHIVO
                if (validacionDocumentacion.archivoBinario == obligatoriedadCampo.obligatorio)
                {
                    if (!ArchivoAdjunto.HasFile)
                    {
                        errores.Add("Seleccione Archivo");
                    }
                }

                //RESULTADO (SI HAY ELEMENTOS EN EL CAMPOS RESULTADO, SE DEBE SELECCIONAR ALGUNO)
                if (Resultado.Items.Count > 1)
                {
                    if (PanelResultado.Visible == true && Convert.ToInt32(Resultado.SelectedValue) < 1)
                    {
                        errores.Add("Seleccione Resultado");
                    }
                }


                //VERIFICAR SI ES UN DOCUMENTO INVOLUCRADO EN LA AMPLICACION DE PLAZO (SI ES ASI, DEBE HABER UN DOCUMENTO ANTERIOR RELACIONADO)
                if (Convert.ToInt32(Tipo.SelectedValue) > 0 && validacionDocumentacion.verificaAmpPlazo == 1)
                {
                    RequerimientoPlazo reqPlazo = new RequerimientoPlazo();
                    reqPlazo.subReqDestino = new ParametroGenerico(Convert.ToInt32(Tipo.SelectedValue));
                    reqPlazo.idSolicitud = Convert.ToInt32(IdSolicitud.Text);
                    bool cumple = requerimientoService.ObtieneRequerimientoPlazoExtension(reqPlazo);

                    if (!cumple)
                    {
                        errores.Add("No existe un requerimiendo al cual se le pueda solicitar una ampliación de plazo.");
                    }
                }

                if (Convert.ToInt32(Tipo.SelectedValue) > 0 && validacionDocumentacion.verificaAmpExtension == 1)
                {
                    RequerimientoPlazo reqPlazo = new RequerimientoPlazo();
                    reqPlazo.subReqResuelve = new ParametroGenerico(Convert.ToInt32(Tipo.SelectedValue));
                    reqPlazo.idSolicitud = Convert.ToInt32(IdSolicitud.Text);
                    bool cumple = requerimientoService.ObtieneRequerimientoPlazoExtension(reqPlazo);

                    if (!cumple)
                    {
                        errores.Add("No existe documento al cual se le pueda ampliar el plazo o bien el titular no ha solicitado una ampliación de plazo.");
                    }
                }
            }
            else
            {
                errores.Add("Error. Imposible determinar los campos a validar."); //REVISAR CONSOLIDADO Y BASE DE DATOS
            }


            return errores;
        }


        protected void Origen_Change(object sender, EventArgs e)
        {

            TipoDocumento.Items.Clear();
            TipoDocumento.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            if (Convert.ToInt32(Origen.SelectedValue) > 0)
            {


                ValidacionDocumentacion validacionDocumentacion = (ValidacionDocumentacion)ViewState["validacionDocumentacion"];
                validacionDocumentacion.flujoDocumental = new ParametroGenerico(Convert.ToInt32(FlujoDocumental.SelectedValue));
                validacionDocumentacion.tipoIO = new ParametroGenerico(Convert.ToInt32(TipoEntrada.SelectedValue));
                validacionDocumentacion.tipoDestinatario = new ParametroGenerico(Convert.ToInt32(Origen.SelectedValue));
                validacionDocumentacion.ambito = new ParametroGenerico(Convert.ToInt32(dbPestana));
                validacionDocumentacion.seccion = new ParametroGenerico(Convert.ToInt32(dbSeccion));


                List<ValidacionDocumentacion> resp = validacionDocumentacionDA.ListarTipoDocumentoFiltro(validacionDocumentacion);


                if (resp != null)
                {
                    foreach (ValidacionDocumentacion item in resp)
                    {
                        TipoDocumento.Items.Add(new ListItem(item.tipoDocumento.descripcion, Convert.ToString(item.tipoDocumento.id)));
                    }
                }

            }

            TipoDocumento.DataBind();

            UpdatePanelTipoDocumento.Update();


        }

        protected void Destinatario_Change(object sender, EventArgs e)
        {


            TipoDocumento.Items.Clear();
            TipoDocumento.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            if (Convert.ToInt32(Destinatario.SelectedValue) > 0)
            {

                ValidacionDocumentacion validacionDocumentacion = (ValidacionDocumentacion)ViewState["validacionDocumentacion"];
                validacionDocumentacion.tipoIO = new ParametroGenerico(Convert.ToInt32(TipoSalida.SelectedValue));
                validacionDocumentacion.tipoDestinatario = new ParametroGenerico(Convert.ToInt32(Destinatario.SelectedValue));
                validacionDocumentacion.ambito = new ParametroGenerico(Convert.ToInt32(dbPestana));
                validacionDocumentacion.seccion = new ParametroGenerico(Convert.ToInt32(dbSeccion));
                validacionDocumentacion.flujoDocumental = new ParametroGenerico(Convert.ToInt32(FlujoDocumental.SelectedValue));


                List<ValidacionDocumentacion> resp = validacionDocumentacionDA.ListarTipoDocumentoFiltro(validacionDocumentacion);


                if (resp != null)
                {
                    foreach (ValidacionDocumentacion item in resp)
                    {
                        TipoDocumento.Items.Add(new ListItem(item.tipoDocumento.descripcion, Convert.ToString(item.tipoDocumento.id)));
                    }
                }
            }


            TipoDocumento.DataBind();

            UpdatePanelTipoDocumento.Update();
        }



        protected void TipoDocumento_change(object sender, EventArgs e)
        {


            LimpiarPorTipoDocumento();



            if (Convert.ToInt32(TipoDocumento.SelectedValue) > 0)
            {

                if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.SALIDA)
                {
                    if (Convert.ToInt32(TipoSalida.SelectedValue) == rbTipo.INFORMATIVO)
                    {
                        this.controlarCamposLogicos(rbTipo.INFORMATIVO);
                    }
                    else if (Convert.ToInt32(TipoSalida.SelectedValue) == rbTipo.REQUERIMIENTO_CON_RESPUESTA)
                    {
                        this.controlarCamposLogicos(rbTipo.REQUERIMIENTO_CON_RESPUESTA);
                    }
                }


                if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.ENTRADA)
                {
                    if (Convert.ToInt32(TipoEntrada.SelectedValue) == rbTipo.INGRESO_SIN_REQUERIMIENTO)
                    {
                        this.controlarCamposLogicos(rbTipo.INGRESO_SIN_REQUERIMIENTO);
                    }
                    else if (Convert.ToInt32(TipoEntrada.SelectedValue) == rbTipo.RESPUESTA_A_UN_REQUERIMIENTO)
                    {
                        this.controlarCamposLogicos(rbTipo.RESPUESTA_A_UN_REQUERIMIENTO);
                    }
                }


                if (Convert.ToInt32(TipoDocumento.SelectedValue) == rbTipo.INFORME_COMPLEMENTARIO || Convert.ToInt32(TipoDocumento.SelectedValue) == rbTipo.RESOLUCION_COMPLEMENTARIA)
                {

                    DocumentoPrincipal.Items.Clear();
                    DocumentoPrincipal.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    if (Convert.ToInt32(TipoDocumento.SelectedValue) == rbTipo.INFORME_COMPLEMENTARIO)
                    {
                        DataTable data = requerimientoService.ListarRequerimientosIdSolicitudPrinc(Convert.ToInt32(IdSolicitud.Text), this.dbSeccion, 0, rbTipo.INFORME_PRINCIPAL);

                        if (data != null)
                        {
                            foreach (DataRow row in data.Rows)
                            {
                                DocumentoPrincipal.Items.Add(new ListItem(Convert.ToString(row["nombreReq"]), Convert.ToString(row["idDocGeneral"])));
                            }
                        }
                    }


                    if (Convert.ToInt32(TipoDocumento.SelectedValue) == rbTipo.RESOLUCION_COMPLEMENTARIA)
                    {
                        DataTable data = requerimientoService.ListarRequerimientosIdSolicitudPrinc(Convert.ToInt32(IdSolicitud.Text), this.dbSeccion, 0, rbTipo.RESOLUCION_PRINCIPAL);

                        if (data != null)
                        {
                            foreach (DataRow row in data.Rows)
                            {
                                DocumentoPrincipal.Items.Add(new ListItem(Convert.ToString(row["nombreReq"]), Convert.ToString(row["idDocGeneral"])));
                            }
                        }
                    }

                    DocumentoPrincipal.DataBind();


                    if (Convert.ToInt32(TipoDocumento.SelectedValue) == rbTipo.INFORME_COMPLEMENTARIO)
                    {
                        LiteralDocumentoPrincipal.Text = "Informe Principal";
                    }
                    if (Convert.ToInt32(TipoDocumento.SelectedValue) == rbTipo.RESOLUCION_COMPLEMENTARIA)
                    {
                        LiteralDocumentoPrincipal.Text = "Resolución Principal";
                    }


                    PanelDocumentoPrincipal.Visible = true;
                    UpdatePanelDocumentoPrincipal.Update();
                }
            }
        }



        protected void LimpiarPorTipoDocumento()
        {

            DocumentoPrincipal.SelectedValue = "0";
            Tipo.SelectedValue = "0";
            Numero.Text = "";
            Fecha.Text = "";
            NuevaFecha.Text = "";
            NumeroCI.Text = "";
            NumeroCIMensaje.Text = "";
            FechaCI.Text = "";
            Resultado.SelectedValue = "0";
            NRequerimiento.SelectedValue = "0";

            ViewState["Documentos_Asociados"] = null;
            GridViewSalidaDocumentoAsociado_CargaGrilla();


            PanelDocumentoPrincipal.Visible = false;
            PanelNumeroRequerimiento.Visible = false;
            PanelListaRequerimientos.Visible = false;
            PanelAmbito.Visible = false;
            PanelTipo.Visible = false;
            PanelDocumentosAmbito.Visible = false;
            PanelNumero.Visible = false;
            PanelFecha.Visible = false;
            PanelNuevaFecha.Visible = false;
            PanelNumeroCI.Visible = false;
            PanelFechaCI.Visible = false;
            PanelResultado.Visible = false;
            PanelArchivo.Visible = false;

            ErroresSuperior.Text = "";
            PanelErroresSuperior.Visible = false;
            UpdatePanelErroresSuperior.Update();

            ErroresInferior.Text = "";
            PanelErroresInferior.Visible = false;
            UpdatePanelErroresInferior.Update();

            ListViewEntradaRespuestaRequerimiento_Carga();
            //TipoDocumento.Focus();


            UpdatePanelDocumentoPrincipal.Update();
            UpdatePanelNumeroRequerimiento.Update();
            UpdatePanelListaRequerimientos.Update();
            UpdatePanelAmbito.Update();
            UpdatePanelTipo.Update();
            UpdatePanelDocumentosAmbito.Update();
            UpdatePanelNumero.Update();
            UpdatePanelFecha.Update();
            UpdatePanelNuevaFecha.Update();
            UpdatePanelNumeroCI.Update();
            UpdatePanelFechaCI.Update();
            UpdatePanelResultado.Update();
            UpdatePanelArchivo.Update();

        }



        protected void Ambito_change(object sender, EventArgs e)
        {

            Tipo.Items.Clear();
            Tipo.Items.Insert(0, new ListItem("-- Seleccione --", "0"));


            //TIPO (APARECERA EN CASO  QUE HAYAN TIPOS POSIBLES DE SELECCIONAR Y NO SEA UNA RESPUESTA A UN REQUERIMIENTO)
            if (!((Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.ENTRADA && Convert.ToInt32(TipoEntrada.SelectedValue) == rbTipo.RESPUESTA_A_UN_REQUERIMIENTO)))
            {

                ValidacionDocumentacion validacionDocumentacion = (ValidacionDocumentacion)ViewState["validacionDocumentacion"];

                validacionDocumentacion.ambito = new ParametroGenerico(Convert.ToInt32(Ambito.SelectedValue));
                validacionDocumentacion.seccion = new ParametroGenerico(Convert.ToInt32(dbSeccion));
                validacionDocumentacion.flujoDocumental = new ParametroGenerico(Convert.ToInt32(FlujoDocumental.SelectedValue));
                validacionDocumentacion.tipoDocumento = new ParametroGenerico(Convert.ToInt32(TipoDocumento.SelectedValue));

                if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.ENTRADA)
                {
                    validacionDocumentacion.tipoIO = new ParametroGenerico(Convert.ToInt32(TipoEntrada.SelectedValue));
                    validacionDocumentacion.tipoDestinatario = new ParametroGenerico(Convert.ToInt32(Origen.SelectedValue));
                }
                if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.SALIDA)
                {
                    validacionDocumentacion.tipoIO = new ParametroGenerico(Convert.ToInt32(TipoSalida.SelectedValue));
                    validacionDocumentacion.tipoDestinatario = new ParametroGenerico(Convert.ToInt32(Destinatario.SelectedValue));
                }

                List<ValidacionDocumentacion> resp = validacionDocumentacionDA.ListarValidacionDocumentacion(validacionDocumentacion);


                if (resp != null)
                {
                    foreach (ValidacionDocumentacion item in resp)
                    {
                        if (item.subRequerimiento != null)
                        {
                            Tipo.Items.Add(new ListItem(item.subRequerimiento.descripcion, Convert.ToString(item.subRequerimiento.id)));
                        }
                    }
                }


                //TIPO DEPENDERA DE SI HAY ELEMENTOS QUE MOSTRAR
                if (Tipo.Items.Count > 1)
                {
                    PanelTipo.Visible = true;
                }

            }
            else
            {
                PanelTipo.Visible = false;
            }


            Tipo.DataBind();
            UpdatePanelTipo.Update();


        }


        protected void Tipo_change(object sender, EventArgs e)
        {

            this.controlarCamposPorTema();

            //LOS DOCUMENTOS COMPLEMENTARIOS NO TIENEN RESULTADOS
            if ((Convert.ToInt32(TipoDocumento.SelectedValue) != rbTipo.RESOLUCION_COMPLEMENTARIA && Convert.ToInt32(TipoDocumento.SelectedValue) != rbTipo.INFORME_COMPLEMENTARIO) && Convert.ToInt32(Tipo.SelectedValue) > 0 && !(Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.SALIDA && Convert.ToInt32(TipoSalida.SelectedValue) == rbTipo.REQUERIMIENTO_CON_RESPUESTA))
            {
                DataTable data = requerimientoService.ListarPosiblesRespuestasSubRequerimiento(Convert.ToInt32(Tipo.SelectedValue));

                if (data != null && data.Rows.Count > 0)
                {

                    Resultado.Items.Clear();
                    Resultado.DataSource = data;
                    Resultado.DataTextField = "nombreEstado";
                    Resultado.DataValueField = "idEstado";

                    Resultado.DataBind();
                    Resultado.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    PanelResultado.Visible = true;
                }
                else
                {
                    Resultado.Items.Clear();
                    Resultado.DataBind();
                    Resultado.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    PanelResultado.Visible = false;

                }

            }
            else
            {
                Resultado.Items.Clear();
                Resultado.DataBind();
                Resultado.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                PanelResultado.Visible = false;
            }


            UpdatePanelResultado.Update();

        }

        protected void NRequerimiento_change(object sender, EventArgs e)
        {

            if (Convert.ToInt32(NRequerimiento.SelectedValue) > 0)
            {
                PanelListaRequerimientos.Visible = true;
                ListViewEntradaRespuestaRequerimiento_Carga();
            }
            else
            {
                PanelListaRequerimientos.Visible = false;
                this.ocultarCamposOpcionales();
            }

            UpdatePanelListaRequerimientos.Update();
        }


        //GRIDVIEW  (SALIDA - REQUERIMIENTO CON RESPUESTA)
        protected void GridViewSalidaDocumentoAsociado_Guardar(object sender, EventArgs e)
        {

            //AMBITO
            if (Convert.ToInt32(Ambito.SelectedValue) < 1)
            {
                Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Ámbito"));

            }

            //TIPO
            if (Convert.ToInt32(Tipo.SelectedValue) < 1)
            {
                Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Tema"));

            }


            if (Page.IsValid)
            {
                GridViewSalidaDocumentoAsociado_Agregar();
            }
            else
            {
                UpdatePanelMensajesValidaciones.Update();
            }


            if (PanelFecha.Visible == true)
            {
                string script = "calendario('" + Fecha.ClientID + "','" + fechaImgDinamica.ClientID + "');";
                ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + Fecha.ClientID, script.ToString(), true);
            }

            if (PanelFechaCI.Visible == true)
            {
                string script = "calendario('" + FechaCI.ClientID + "','" + fechaCIImgDinamica.ClientID + "');";
                ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + FechaCI.ClientID, script.ToString(), true);
            }


        }


        //GRIDVIEW  (SALIDA - REQUERIMIENTO CON RESPUESTA)
        protected void GridViewSalidaDocumentoAsociado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //ACCION
                HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
                if (hidden_accion != null && !hidden_accion.Value.Equals("") && Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR)
                {
                    e.Row.Attributes["style"] = "display:none";
                };

                // Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar?')");
                    boton_eliminar.Visible = true;
                };
            };
        }


        //GRIDVIEW  (SALIDA - REQUERIMIENTO CON RESPUESTA)
        protected void GridViewSalidaDocumentoAsociado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            PanelMensajePlanos14TER.Visible = false;
            UpdatePanelMensajePlanos14TER.Update();

            switch (e.CommandName)
            {
                case "Eliminar":
                    int idTipo = Convert.ToInt32(e.CommandArgument);
                    GridViewSalidaDocumentoAsociado.EditIndex = -1;
                    GridViewSalidaDocumentoAsociado_Eliminar(idTipo);
                    break;
            };
        }



        //GRIDVIEW  (SALIDA - REQUERIMIENTO CON RESPUESTA)
        protected void GridViewSalidaDocumentoAsociado_CargaGrilla()
        {


            List<DocumentoAmbito> List_Documentos_Asociados = (List<DocumentoAmbito>)ViewState["Documentos_Asociados"];


            if (List_Documentos_Asociados == null)
            {
                List_Documentos_Asociados = new List<DocumentoAmbito>();
            }

            GridViewSalidaDocumentoAsociado.DataSource = List_Documentos_Asociados;
            GridViewSalidaDocumentoAsociado.DataBind();


            ViewState["Documentos_Asociados"] = (List<DocumentoAmbito>)List_Documentos_Asociados;


        }


        //GRIDVIEW  (SALIDA - REQUERIMIENTO CON RESPUESTA)
        protected void GridViewSalidaDocumentoAsociado_Eliminar(int index)
        {

            bool grillaVacia = true;

            List<DocumentoAmbito> List_Documentos_Asociados = (List<DocumentoAmbito>)ViewState["Documentos_Asociados"];

            foreach (DocumentoAmbito aDocumentoAmbito in List_Documentos_Asociados)
            {
                if (aDocumentoAmbito.index == index)
                {
                    List<String> errores = ingresarDocumentoValidacion.validaEliminacionDocumentoAsociado(aDocumentoAmbito);
                    if (errores.Count > 0)
                    {
                        foreach (String error in errores)
                        {
                            ErroresSuperior.Text = error;
                            PanelErroresSuperior.Visible = true;
                        }
                    }
                    else
                    {
                        ErroresSuperior.Text = "";
                        PanelErroresSuperior.Visible = false;
                        aDocumentoAmbito.accion = accion.ELIMINAR;
                        GridViewSalidaDocumentoAsociado.Rows[index].Attributes["style"] = "display:none";
                        GridViewSalidaDocumentoAsociado.DataBind();
                    }
                }

                if (aDocumentoAmbito.accion == accion.INGRESAR || aDocumentoAmbito.accion == accion.LISTADO || aDocumentoAmbito.accion == accion.MODIFICAR)
                {
                    grillaVacia = false;
                }

            }

            if (grillaVacia)
            {
                this.ocultarCamposOpcionales();
            }

            ViewState["Documentos_Asociados"] = (List<DocumentoAmbito>)List_Documentos_Asociados;
            GridViewSalidaDocumentoAsociado_CargaGrilla();

            UpdatePanelErroresSuperior.Update();
        }



        //GRIDVIEW (SALIDA - REQUERIMIENTO CON RESPUESTA)
        protected void GridViewSalidaDocumentoAsociado_Agregar()
        {

            List<DocumentoAmbito> List_Documentos_Asociados = (List<DocumentoAmbito>)ViewState["Documentos_Asociados"];
            bool primerElemento = true;
            int index = 0;


            if (List_Documentos_Asociados == null)
            {
                List_Documentos_Asociados = new List<DocumentoAmbito>();
            }
            else
            {
                index = List_Documentos_Asociados.Count;
            }


            DocumentoAmbito documentoAmbito = new DocumentoAmbito();
            documentoAmbito.ambito = new ParametroGenerico(Convert.ToInt32(Ambito.SelectedValue), Ambito.SelectedItem.Text);
            documentoAmbito.tipo = new ParametroGenerico(Convert.ToInt32(Tipo.SelectedValue), Tipo.SelectedItem.Text);
            documentoAmbito.index = index;
            documentoAmbito.accion = accion.INGRESAR;
            documentoAmbito.seccion = new ParametroGenerico(this.dbSeccion);


            List<String> errores = ingresarDocumentoValidacion.validaAdicionDocumentoAsociado(documentoAmbito, List_Documentos_Asociados, Convert.ToInt32(IdSolicitud.Text));

            if (errores.Count > 0)
            {
                foreach (String error in errores)
                {
                    PanelErroresSuperior.Visible = true;
                    ErroresSuperior.Text = error;
                }
            }
            else
            {
                foreach (DocumentoAmbito auxDoc in List_Documentos_Asociados)
                {
                    if (auxDoc.accion == accion.INGRESAR || auxDoc.accion == accion.LISTADO || auxDoc.accion == accion.MODIFICAR)
                    {
                        primerElemento = false;
                        break;
                    }
                }

                if (primerElemento)
                {
                    mostrarCamposOpcionales(documentoAmbito);
                }

                ErroresSuperior.Text = "";
                PanelErroresSuperior.Visible = false;
                List_Documentos_Asociados.Add(documentoAmbito);

                GridViewSalidaDocumentoAsociado.DataSource = List_Documentos_Asociados;
                GridViewSalidaDocumentoAsociado.DataBind();
                ViewState["Documentos_Asociados"] = (List<DocumentoAmbito>)List_Documentos_Asociados;

                /* Se debe desplegar mensaje de alerta cuando se envie la carta al titular mo (31) */

                if (documentoAmbito.tipo.id == 31 || documentoAmbito.tipo.id == 200) 
                {
                    SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                    if (solicitudConcesion != null)
                        if (Convert.ToInt32(solicitudConcesion.tipoUnidadEspacial.id) != rbTipo.ACUICULTURA_EN_AMERB &&
                            Convert.ToInt32(solicitudConcesion.tipoUnidadEspacial.id) != rbTipo.EXPERIMENTALES_AMERB &&
                            Convert.ToInt32(solicitudConcesion.tipoUnidadEspacial.id) != rbTipo.MOD_AMERB_AMPLIA_SUPERFICIE &&
                            Convert.ToInt32(solicitudConcesion.tipoUnidadEspacial.id) != rbTipo.MOD_AMERB_ESPECIE &&
                            Convert.ToInt32(solicitudConcesion.tipoUnidadEspacial.id) != rbTipo.MOD_AMERB_PT &&
                            Convert.ToInt32(solicitudConcesion.tipoUnidadEspacial.id) != rbTipo.MOD_AMERB_REDUCE_SUPERFICIE &&
                            Convert.ToInt32(solicitudConcesion.tipoUnidadEspacial.id) != rbTipo.MOD_AMERB_REGULARIZACION)
                        {
                            {
                                PanelMensajePlanos14TER.Visible = true;
                                UpdatePanelMensajePlanos14TER.Update();
                            }
                        }
                }
                else
                {
                    PanelMensajePlanos14TER.Visible = false;
                    UpdatePanelMensajePlanos14TER.Update();
                }

            }

            UpdatePanelErroresSuperior.Update();
        }



        //LISTVIEW (ENTRADA - RESPUESTA A UN REQUERIMIENTO)
        protected void ListViewEntradaRespuestaRequerimiento_Carga()
        {

            List<DocumentoAmbito> respu = new List<DocumentoAmbito>();

            if (Convert.ToInt32(NRequerimiento.SelectedValue) > 0)
            {
                respu = requerimientoService.ListarDocumentacionPestanaReqMod(Convert.ToInt32(NRequerimiento.SelectedValue), Convert.ToInt32(TipoDocumento.SelectedValue));

                if (respu != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in respu)
                    {
                        this.mostrarCamposOpcionales(documentoAmbito);
                        break;
                    }
                }
            }


            ListViewEntradaRespuestaRequerimiento.DataSource = (List<DocumentoAmbito>)respu;
            ListViewEntradaRespuestaRequerimiento.DataBind();

            ViewState["Documentos_Asociados_Respuesta"] = (List<DocumentoAmbito>)respu;
        }


        //LISTVIEW (ENTRADA - RESPUESTA A UN REQUERIMIENTO)
        protected void ListViewEntradaRespuestaRequerimiento_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {

                HiddenField idResultado = (HiddenField)e.Item.FindControl("HiddenIdResultado");
                DropDownList respuesta = (DropDownList)e.Item.FindControl("AmbitoTipoResultado");
                HiddenField tipo = (HiddenField)e.Item.FindControl("HiddenTipoId");


                //SI TIENEN RESPUESTA, ENTONCES SE DEBE MARCAR Y BLOQUEAR EL CHECK
                HiddenField idRespuesta = (HiddenField)e.Item.FindControl("HiddenIdDocGeneralResp");
                CheckBox check = (CheckBox)e.Item.FindControl("chkSeleccionado");


                if (idRespuesta.Value != null && Convert.ToInt32(idRespuesta.Value) > 0 && check != null)
                {
                    check.Checked = true;
                    check.Enabled = false;
                    respuesta.Enabled = false;
                }


                List<DocumentoAmbito> respu = (List<DocumentoAmbito>)ViewState["Documentos_Asociados_Respuesta"];

                //LOS DOCUMENTOS COMPLEMENTARIOS NO TIENEN RESULTADOS
                if ((Convert.ToInt32(TipoDocumento.SelectedValue) != rbTipo.RESOLUCION_COMPLEMENTARIA && Convert.ToInt32(TipoDocumento.SelectedValue) != rbTipo.INFORME_COMPLEMENTARIO) && respuesta != null && tipo != null)
                {
                    DataTable data = requerimientoService.ListarPosiblesRespuestasSubRequerimiento(Convert.ToInt32(tipo.Value));


                    if (data != null && data.Rows.Count > 0)
                    {

                        respuesta.Items.Clear();
                        respuesta.DataSource = data;
                        respuesta.DataTextField = "nombreEstado";
                        respuesta.DataValueField = "idEstado";
                        respuesta.DataBind();
                        respuesta.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                        if (idResultado != null && !idResultado.Value.Equals("") && Convert.ToInt32(idResultado.Value) > 0)
                        {
                            respuesta.SelectedValue = idResultado.Value;
                        }

                    }
                    else
                    {
                        respuesta.Items.Clear();
                        respuesta.DataBind();
                        respuesta.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                        respuesta.Visible = false;

                    }
                }
                else
                {
                    respuesta.Items.Clear();
                    respuesta.DataBind();
                    respuesta.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    respuesta.Visible = false;
                }
            }
        }





        //GRID REQUERIMIENTO
        protected void GridRequerimiento_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false; // Invisibiling idDocGeneral Header Cell
                e.Row.Cells[1].Visible = false; // Invisibiling idPestana Header Cell
                e.Row.Cells[2].Visible = false; // Invisibiling Ambito Header Cell
                e.Row.Cells[3].Visible = false; // Invisibiling Tipo Header Cell
                e.Row.Cells[15].Visible = false; // Invisibiling Estado Documentación Header Cell
                e.Row.Cells[16].Visible = false; // Invisibiling Evaluación Header Cell
                e.Row.Cells[17].Visible = false; // Invisibiling Acciones Header Cell

                e.Row.Cells[8].Style["border-left"] = colorPlanilla.RAYA_DIVISORA;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                GridView GridRequerimiento = (GridView)sender;
                int count = GridRequerimiento.Rows.Count;


                String rowspan = ((Label)e.Row.FindControl("hidden4")).Text;

                //PRIMERA FILA CON DATOS
                if (count == 0)
                {
                    e.Row.Cells[2].Visible = true;
                    e.Row.Cells[2].RowSpan = Convert.ToInt32(rowspan);

                    e.Row.Cells[15].Visible = true;
                    e.Row.Cells[15].RowSpan = Convert.ToInt32(rowspan);

                    e.Row.Cells[16].Visible = true;
                    e.Row.Cells[16].RowSpan = Convert.ToInt32(rowspan);

                    e.Row.Cells[17].Visible = true;
                    e.Row.Cells[17].RowSpan = Convert.ToInt32(rowspan);

                    e.Row.BackColor = colorPlanilla.COLOR_CELESTE;


                    if (e.Row.Cells[15].Text.Equals(colorPlanilla.PARCIAL))
                    {
                        e.Row.Cells[15].BackColor = colorPlanilla.COLOR_PARCIAL;
                    }
                    else if (e.Row.Cells[15].Text.Equals(colorPlanilla.COMPLETADO) || e.Row.Cells[15].Text.Equals(colorPlanilla.COMPLETADO_AUTOMATICO))
                    {
                        e.Row.Cells[15].BackColor = colorPlanilla.COLOR_COMPLETADO;
                    }
                    else if (e.Row.Cells[15].Text.Equals(colorPlanilla.PENDIENTE))
                    {
                        e.Row.Cells[15].BackColor = colorPlanilla.COLOR_PENDIENTE;
                    }

                }
                else if (count > 0)
                {


                    GridViewRow previousRow = GridRequerimiento.Rows[e.Row.RowIndex - 1];

                    String idDocGeneralAnterior = ((Label)previousRow.FindControl("hidden1")).Text;
                    String idPestanaAnterior = ((Label)previousRow.FindControl("hidden2")).Text;

                    String idDocGeneral = ((Label)e.Row.FindControl("hidden1")).Text;
                    String idPestana = ((Label)e.Row.FindControl("hidden2")).Text;



                    if (!idDocGeneralAnterior.Equals(idDocGeneral) || !idPestanaAnterior.Equals(idPestana))
                    {

                        e.Row.Cells[2].Visible = true;
                        e.Row.Cells[2].RowSpan = Convert.ToInt32(rowspan);

                        e.Row.Cells[15].Visible = true;
                        e.Row.Cells[15].RowSpan = Convert.ToInt32(rowspan);

                        e.Row.Cells[16].Visible = true;
                        e.Row.Cells[16].RowSpan = Convert.ToInt32(rowspan);

                        e.Row.Cells[17].Visible = true;
                        e.Row.Cells[17].RowSpan = Convert.ToInt32(rowspan);


                        if (previousRow.BackColor == colorPlanilla.COLOR_CELESTE)
                        {
                            e.Row.BackColor = colorPlanilla.COLOR_BLANCO;
                        }
                        else
                        {
                            e.Row.BackColor = colorPlanilla.COLOR_CELESTE;
                        }



                        if (e.Row.Cells[15].Text.Equals(colorPlanilla.PARCIAL))
                        {
                            e.Row.Cells[15].BackColor = colorPlanilla.COLOR_PARCIAL;
                        }
                        else if (e.Row.Cells[15].Text.Equals(colorPlanilla.COMPLETADO) || e.Row.Cells[15].Text.Equals(colorPlanilla.COMPLETADO_AUTOMATICO))
                        {
                            e.Row.Cells[15].BackColor = colorPlanilla.COLOR_COMPLETADO;
                        }
                        else if (e.Row.Cells[15].Text.Equals(colorPlanilla.PENDIENTE))
                        {
                            e.Row.Cells[15].BackColor = colorPlanilla.COLOR_PENDIENTE;
                        }

                    }
                    else
                    {
                        e.Row.Cells[2].RowSpan = 0;
                        e.Row.Cells[2].Visible = false;

                        e.Row.Cells[15].RowSpan = 0;
                        e.Row.Cells[15].Visible = false;

                        e.Row.Cells[16].RowSpan = 0;
                        e.Row.Cells[16].Visible = false;

                        e.Row.Cells[17].RowSpan = 0;
                        e.Row.Cells[17].Visible = false;

                        e.Row.BackColor = previousRow.BackColor;
                    }
                }


                e.Row.Cells[0].Visible = false; // Invisibiling idDocGeneral Header Cell
                e.Row.Cells[1].Visible = false; // Invisibiling idPestana Header Cell
                e.Row.Cells[8].Style["border-left"] = colorPlanilla.RAYA_DIVISORA;



                //Ver
                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.VER))
                    {
                        //boton_ver.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro...?')");
                        boton_ver.Visible = true;
                    }
                };


                Int32 codAux = 0;
                codAux = (((DataRowView)e.Row.DataItem).Row.ItemArray.Length < 54 || ((DataRowView)e.Row.DataItem).Row.ItemArray[53].ToString().Trim().Equals("")) ? 0 : Convert.ToInt32(((DataRowView)e.Row.DataItem).Row.ItemArray[53]);


                //NO SE PUEDEN MODIFICAR REGISTROS INGRESADOS A TRAVES DEL ADMINISTRADOR DE RESOLUCIONES
                if (codAux == 0)
                {

                    //Administrar
                    ImageButton boton_administrar = (ImageButton)e.Row.FindControl("gAdministrar");
                    if (boton_administrar != null)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ADMINISTRAR_REQUERIMIENTO))
                        {
                            //boton_administrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro...?')");
                            boton_administrar.Visible = true;
                        }
                    };


                    //CONFORME-NO CONFORME
                    String validaConforme = ((Label)e.Row.FindControl("validaConforme")).Text;

                    ImageButton boton_evaluar = (ImageButton)e.Row.FindControl("gEvaluar");
                    if (boton_evaluar != null)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.EVALUAR))
                        {
                            if (Convert.ToInt32(validaConforme) == 1)
                            {
                                boton_evaluar.Visible = true;
                            }
                        }
                    };


                    String idEstadoVigencia = ((Label)e.Row.FindControl("hidden3")).Text;

                    //No Vigente
                    ImageButton boton_noVigente = (ImageButton)e.Row.FindControl("gNoVigente");
                    if (boton_noVigente != null)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.NO_VIGENTE))
                        {
                            boton_noVigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea dejar no vigente este documento(s)?')");
                            if (Convert.ToInt32(idEstadoVigencia) == rbEstadosGenerales.VIGENTE)
                            {
                                boton_noVigente.Visible = true;
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(idEstadoVigencia) == rbEstadosGenerales.VIGENTE)
                            {
                                if (boton_noVigente.ImageUrl != null && boton_noVigente.ImageUrl.Contains("realizado.png"))
                                {
                                    boton_noVigente.ImageUrl = boton_noVigente.ImageUrl.Replace("realizado.png", "realizadoBlock.png");
                                    boton_noVigente.AlternateText = "Vigente (Bloqueado)";
                                    boton_noVigente.ToolTip = "Vigente (Bloqueado)";
                                    boton_noVigente.Enabled = false;
                                    boton_noVigente.Visible = true;
                                }
                            }

                        }
                    };


                    //Vigente
                    ImageButton boton_vigente = (ImageButton)e.Row.FindControl("gVigente");
                    if (boton_vigente != null)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.VIGENTE))
                        {
                            boton_vigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea dejar vigente este documento(s)?')");
                            if (Convert.ToInt32(idEstadoVigencia) == rbEstadosGenerales.NO_VIGENTE)
                            {
                                boton_vigente.Visible = true;
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(idEstadoVigencia) == rbEstadosGenerales.NO_VIGENTE)
                            {
                                if (boton_vigente.ImageUrl != null && boton_vigente.ImageUrl.Contains("unauth.png"))
                                {
                                    boton_vigente.ImageUrl = boton_vigente.ImageUrl.Replace("unauth.png", "unauthBlock.png");
                                    boton_vigente.AlternateText = "NO Vigente (Bloqueado)";
                                    boton_vigente.ToolTip = "NO Vigente (Bloqueado)";
                                    boton_vigente.Enabled = false;
                                    boton_vigente.Visible = true;
                                }
                            }
                        }
                    };



                    //Borrar
                    ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
                    if (boton_borrar != null)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ELIMINAR))
                        {
                            boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar este documento?')");
                            boton_borrar.Visible = true;
                        }
                    };

                }
                else
                {


                    String idEstadoVigencia = ((Label)e.Row.FindControl("hidden3")).Text;

                    //No Vigente
                    ImageButton boton_noVigente = (ImageButton)e.Row.FindControl("gNoVigente");
                    if (boton_noVigente != null)
                    {
                        if (Convert.ToInt32(idEstadoVigencia) == rbEstadosGenerales.VIGENTE)
                        {
                            if (boton_noVigente.ImageUrl != null && boton_noVigente.ImageUrl.Contains("realizado.png"))
                            {
                                boton_noVigente.ImageUrl = boton_noVigente.ImageUrl.Replace("realizado.png", "realizadoBlock.png");
                                boton_noVigente.AlternateText = "Vigente (Bloqueado)";
                                boton_noVigente.ToolTip = "Vigente (Bloqueado)";
                                boton_noVigente.Enabled = false;
                                boton_noVigente.Visible = true;
                            }
                        }
                    };


                    //Vigente
                    ImageButton boton_vigente = (ImageButton)e.Row.FindControl("gVigente");
                    if (boton_vigente != null)
                    {
                        if (Convert.ToInt32(idEstadoVigencia) == rbEstadosGenerales.NO_VIGENTE)
                        {
                            if (boton_vigente.ImageUrl != null && boton_vigente.ImageUrl.Contains("unauth.png"))
                            {
                                boton_vigente.ImageUrl = boton_vigente.ImageUrl.Replace("unauth.png", "unauthBlock.png");
                                boton_vigente.AlternateText = "NO Vigente (Bloqueado)";
                                boton_vigente.ToolTip = "NO Vigente (Bloqueado)";
                                boton_vigente.Enabled = false;
                                boton_vigente.Visible = true;
                            }
                        }
                    };
                }

            };
        }


        //GRID REQUERIMIENTO
        protected void GridRequerimiento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int idRequerimiento = Convert.ToInt32(arg[0]);
            int idPestana = Convert.ToInt32(arg[1]);


            ErroresInferior.Text = "";
            PanelErroresInferior.Visible = false;
            UpdatePanelErroresInferior.Update();
            int idTipoFlujoDocumental = 0;

            switch (e.CommandName)
            {
                case "Ver":
                    Response.Redirect(ViewState["URL_VER"].ToString() + idRequerimiento);
                    break;
                case "Administrar":
                    Response.Redirect(ViewState["URL_ADMINISTRAR_DOCUMENTO"].ToString() + idRequerimiento);
                    break;
                case "Evaluar":
                    Response.Redirect(ViewState["URL_EVALUAR"].ToString() + idRequerimiento);
                    break;

                case "Eliminar":

                    idTipoFlujoDocumental = requerimientoService.ObtieneFlujoDocumentoGeneral(idRequerimiento);

                    if (idTipoFlujoDocumental == rbTipo.ENTRADA)
                    {
                        Requerimiento requerimiento = requerimientoService.ObtenerRespuesta(idRequerimiento);
                        List<String> errores = ingresarDocumentoValidacion.validarEliminacionDeRequerimiento(requerimiento);
                        if (errores.Count == 0)
                        {
                            requerimientoService.EliminarRespuesta(idRequerimiento, idPestana, requerimiento.solicitud.idSolConcesion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                        }
                        else
                        {

                            foreach (String error in errores)
                            {
                                ErroresInferior.Text = ErroresInferior.Text + error + "<br/>";
                            }
                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();

                        }
                    }
                    if (idTipoFlujoDocumental == rbTipo.SALIDA)
                    {
                        Requerimiento requerimiento = requerimientoService.ObtenerRequerimientoSinEstado(idRequerimiento);
                        List<String> errores = ingresarDocumentoValidacion.validarEliminacionDeRequerimiento(requerimiento);
                        if (errores.Count == 0)
                        {
                            requerimientoService.EliminarRequerimiento(idRequerimiento, idPestana, requerimiento.solicitud.idSolConcesion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                        }
                        else
                        {

                            foreach (String error in errores)
                            {
                                ErroresInferior.Text = ErroresInferior.Text + error + "<br/>";
                            }
                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();

                        }
                    }

                    CargarListaRequerimientos(usuario_logeado, Convert.ToInt32(IdSolicitud.Text), true);
                    break;

                case "NoVigente":

                    idTipoFlujoDocumental = requerimientoService.ObtieneFlujoDocumentoGeneral(idRequerimiento);

                    if (idTipoFlujoDocumental == rbTipo.ENTRADA)
                    {
                        Requerimiento requerimiento = requerimientoService.ObtenerRespuesta(idRequerimiento);
                        List<String> errores = ingresarDocumentoValidacion.validarNoVigenteDeRequerimiento(requerimiento);
                        if (errores.Count == 0)
                        {
                            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];
                            requerimientoService.ActualizarRespuestaEstado(idRequerimiento, idPestana, rbEstadosGenerales.NO_VIGENTE, usuario_logeado.id_usuario);
                        }
                        else
                        {

                            foreach (String error in errores)
                            {
                                ErroresInferior.Text = ErroresInferior.Text + error + "<br/>";
                            }
                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();

                        }
                    }
                    if (idTipoFlujoDocumental == rbTipo.SALIDA)
                    {
                        Requerimiento requerimiento = requerimientoService.ObtenerRequerimientoSinEstado(idRequerimiento);
                        List<String> errores = ingresarDocumentoValidacion.validarNoVigenteDeRequerimiento(requerimiento);
                        if (errores.Count == 0)
                        {
                            requerimientoService.ActualizarRequerimientoEstado(idRequerimiento, idPestana, rbEstadosGenerales.NO_VIGENTE, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                        }
                        else
                        {

                            foreach (String error in errores)
                            {
                                ErroresInferior.Text = ErroresInferior.Text + error + "<br/>";
                            }
                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();

                        }
                    }

                    CargarListaRequerimientos(usuario_logeado, Convert.ToInt32(IdSolicitud.Text), true);
                    break;


                case "Vigente":

                    idTipoFlujoDocumental = requerimientoService.ObtieneFlujoDocumentoGeneral(idRequerimiento);

                    if (idTipoFlujoDocumental == rbTipo.ENTRADA)
                    {
                        Requerimiento requerimiento = requerimientoService.ObtenerRespuesta(idRequerimiento);
                        List<String> errores = ingresarDocumentoValidacion.validarNoVigenteDeRequerimiento(requerimiento);
                        if (errores.Count == 0)
                        {
                            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];
                            requerimientoService.ActualizarRespuestaEstado(idRequerimiento, idPestana, rbEstadosGenerales.VIGENTE, usuario_logeado.id_usuario);
                        }
                        else
                        {

                            foreach (String error in errores)
                            {
                                ErroresInferior.Text = ErroresInferior.Text + error + "<br/>";
                            }
                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();

                        }
                    }
                    if (idTipoFlujoDocumental == rbTipo.SALIDA)
                    {
                        Requerimiento requerimiento = requerimientoService.ObtenerRequerimientoSinEstado(idRequerimiento);
                        List<String> errores = ingresarDocumentoValidacion.validarNoVigenteDeRequerimiento(requerimiento);
                        if (errores.Count == 0)
                        {
                            requerimientoService.ActualizarRequerimientoEstado(idRequerimiento, idPestana, rbEstadosGenerales.VIGENTE, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                        }
                        else
                        {

                            foreach (String error in errores)
                            {
                                ErroresInferior.Text = ErroresInferior.Text + error + "<br/>";
                            }
                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();

                        }
                    }

                    CargarListaRequerimientos(usuario_logeado, Convert.ToInt32(IdSolicitud.Text), true);
                    break;

            };


        }


        //GRID REQUERIMIENTO
        protected void GridRequerimiento_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridRequerimiento = (GridView)sender;


                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);


                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = labelDocumentos;
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 16;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridRequerimiento.Controls[0].Controls.AddAt(0, HeaderRow);



                // Creating a Row
                HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                HeaderCell = new TableCell();
                HeaderCell.Text = "Ámbito";
                HeaderCell.CssClass = "customHeader";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.RowSpan = 2;
                HeaderRow.Cells.Add(HeaderCell);

                //Adding Tipo Column
                HeaderCell = new TableCell();
                HeaderCell.Text = "Tema";
                HeaderCell.CssClass = "customHeader";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.RowSpan = 2;
                HeaderRow.Cells.Add(HeaderCell);


                //Adding Salida Column
                HeaderCell = new TableCell();
                HeaderCell.Text = "Salida";
                HeaderCell.CssClass = "customHeader";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 4;
                HeaderRow.Cells.Add(HeaderCell);

                //Adding Entrada Column
                HeaderCell = new TableCell();
                HeaderCell.Text = "Entrada";
                HeaderCell.CssClass = "customHeader";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 7;
                HeaderCell.Style["border-left"] = colorPlanilla.RAYA_DIVISORA;
                HeaderRow.Cells.Add(HeaderCell);



                //Adding Estado Documentación Column
                HeaderCell = new TableCell();
                HeaderCell.Text = "Estado Requerimientos";
                HeaderCell.CssClass = "customHeader";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.RowSpan = 2;
                HeaderRow.Cells.Add(HeaderCell);



                //Adding Evaluación Column
                HeaderCell = new TableCell();
                HeaderCell.Text = "Evaluación";
                HeaderCell.CssClass = "customHeader";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.RowSpan = 2;
                HeaderRow.Cells.Add(HeaderCell);


                //Adding Acciones Column
                HeaderCell = new TableCell();
                HeaderCell.Text = "Acciones";
                HeaderCell.CssClass = "customHeader";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.RowSpan = 2;
                HeaderRow.Cells.Add(HeaderCell);


                //Adding the Row at the 0th position (first row) in the Grid
                GridRequerimiento.Controls[0].Controls.AddAt(1, HeaderRow);

            }
        }




        protected void Limpiar_Click(object sender, EventArgs e)
        {
            FlujoDocumental.SelectedValue = "0";
            LimpiarPorFlujoDocumental();
        }


        protected void Guardar_Click(object sender, EventArgs e)
        {

            //Flujo Documental
            if (Convert.ToInt32(FlujoDocumental.SelectedValue) < 1)
            {
                Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Flujo Documental"));
            }


            if (Page.IsValid)
            {


                Requerimiento requerimiento = new Requerimiento();
                requerimiento.solicitud = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                //SE CAMBIO LA SOLICITUD EN SESSION, PERO LA PAGINA NO SE HA RECARGADO
                if (requerimiento.solicitud.idSolConcesion != Convert.ToInt32(IdSolicitud.Text))
                {
                    Page.Validators.Add(new ValidationError(erroresSumary, "La solicitud ha cambiado, por favor recargue la página"));
                }
                requerimiento.flujoDocumental = new ParametroGenerico(Convert.ToInt32(FlujoDocumental.SelectedValue));


                if (requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
                {

                    //Tipo entrada
                    if (Convert.ToInt32(TipoEntrada.SelectedValue) < 1)
                    {
                        Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Tipo de Entrada"));
                    }


                    if (Page.IsValid)
                    {
                        requerimiento.tipoEntrada = new ParametroGenerico(Convert.ToInt32(TipoEntrada.SelectedValue));
                    }


                    if (requerimiento.tipoEntrada != null && requerimiento.tipoEntrada.id == rbTipo.RESPUESTA_A_UN_REQUERIMIENTO)
                    {


                        List<String> errores = this.validarIngresoRequerimiento(rbTipo.RESPUESTA_A_UN_REQUERIMIENTO, requerimiento);

                        foreach (String error in errores)
                        {
                            Page.Validators.Add(new ValidationError(erroresSumary, error));
                        }
                    }



                    if (requerimiento.tipoEntrada != null && requerimiento.tipoEntrada.id == rbTipo.INGRESO_SIN_REQUERIMIENTO)
                    {
                        List<String> errores = this.validarIngresoRequerimiento(rbTipo.INGRESO_SIN_REQUERIMIENTO, requerimiento);

                        foreach (String error in errores)
                        {
                            Page.Validators.Add(new ValidationError(erroresSumary, error));
                        }
                    }

                }


                if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
                {
                    //Tipo salida
                    if (Convert.ToInt32(TipoSalida.SelectedValue) < 1)
                    {
                        Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Tipo de Salida"));
                    }

                    if (Page.IsValid)
                    {
                        requerimiento.tipoSalida = new ParametroGenerico(Convert.ToInt32(TipoSalida.SelectedValue));
                    }



                    if (requerimiento.tipoSalida != null && requerimiento.tipoSalida.id == rbTipo.INFORMATIVO)
                    {
                        List<String> errores = this.validarIngresoRequerimiento(rbTipo.INFORMATIVO, requerimiento);

                        foreach (String error in errores)
                        {
                            Page.Validators.Add(new ValidationError(erroresSumary, error));
                        }

                    }



                    if (requerimiento.tipoSalida != null && requerimiento.tipoSalida.id == rbTipo.REQUERIMIENTO_CON_RESPUESTA)
                    {
                        List<String> errores = this.validarIngresoRequerimiento(rbTipo.REQUERIMIENTO_CON_RESPUESTA, requerimiento);

                        foreach (String error in errores)
                        {
                            Page.Validators.Add(new ValidationError(erroresSumary, error));
                        }

                    }
                }


                if (Page.IsValid)
                {

                    if (Convert.ToInt32(TipoDocumento.SelectedValue) > 0)
                    {
                        requerimiento.tipoDocumento = new ParametroGenerico(Convert.ToInt32(TipoDocumento.SelectedValue));
                    }
                    if (Convert.ToInt32(DocumentoPrincipal.SelectedValue) > 0)
                    {
                        requerimiento.idReqPrincipal = Convert.ToInt32(DocumentoPrincipal.SelectedValue);
                    }

                    if (!Numero.Text.Trim().Equals(""))
                    {
                        requerimiento.numero = Numero.Text;
                    }

                    if (!Fecha.Text.Trim().Equals(""))
                    {
                        requerimiento.fecha = Convert.ToDateTime(Fecha.Text);
                    }

                    if (!NuevaFecha.Text.Trim().Equals(""))
                    {
                        requerimiento.nuevaFecha = Convert.ToDateTime(NuevaFecha.Text);
                    }

                    if (Convert.ToInt32(Destinatario.SelectedValue) > 0)
                    {
                        requerimiento.destinatario = new ParametroGenerico(Convert.ToInt32(Destinatario.SelectedValue));
                    }

                    if (Convert.ToInt32(Origen.SelectedValue) > 0)
                    {
                        requerimiento.origen = new ParametroGenerico(Convert.ToInt32(Origen.SelectedValue));
                    }

                    if (!NumeroCI.Text.Trim().Equals(""))
                    {
                        requerimiento.numeroCI = Convert.ToInt32(NumeroCI.Text);
                    }

                    if (!FechaCI.Text.Trim().Equals(""))
                    {
                        requerimiento.fechaCI = Convert.ToDateTime(FechaCI.Text);
                    }



                    if (requerimiento.ambitoTipo == null)
                    {
                        requerimiento.ambitoTipo = new List<DocumentoAmbito>();
                        requerimiento.ambitoTipo.Add(new DocumentoAmbito());

                        //AMBITO
                        if (Convert.ToInt32(Ambito.SelectedValue) > 0)
                        {
                            foreach (DocumentoAmbito doc in requerimiento.ambitoTipo)
                            {
                                doc.ambito = new ParametroGenerico(Convert.ToInt32(Ambito.SelectedValue));
                                break;
                            }
                        }

                        //TIPO
                        if (Convert.ToInt32(Tipo.SelectedValue) > 0)
                        {
                            foreach (DocumentoAmbito doc in requerimiento.ambitoTipo)
                            {
                                doc.tipo = new ParametroGenerico(Convert.ToInt32(Tipo.SelectedValue));
                                break;
                            }
                        }


                        //RESULTADO
                        if (Convert.ToInt32(Resultado.SelectedValue) > 0)
                        {
                            foreach (DocumentoAmbito doc in requerimiento.ambitoTipo)
                            {
                                doc.estadoResultadoResp = new ParametroGenerico(Convert.ToInt32(Resultado.SelectedValue));
                                break;
                            }
                        }


                        //SECCION
                        foreach (DocumentoAmbito doc in requerimiento.ambitoTipo)
                        {
                            doc.seccion = new ParametroGenerico(this.dbSeccion);
                            break;

                        }
                    }



                    if (ArchivoAdjunto.HasFile)
                    {

                        ArchivoBinario archivoBinario = new ArchivoBinario();

                        archivoBinario.nombreArchivo = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
                        archivoBinario.nombreFisico = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
                        archivoBinario.formato = ArchivoAdjunto.PostedFile.FileName.Substring(ArchivoAdjunto.PostedFile.FileName.LastIndexOf(".") + 1).ToLower(); ;
                        archivoBinario.tamano = ArchivoAdjunto.PostedFile.InputStream.Length;
                        archivoBinario.archivo = ArchivoAdjunto.PostedFile;

                        requerimiento.archivoAdjunto = archivoBinario;
                    }


                    List<String> errores = ingresarDocumentoValidacion.validaIngresoRequerimiento(requerimiento);


                    if (errores.Count == 0)
                    {



                        bool resp = requerimientoService.guardarRequerimiento(requerimiento, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);


                        if (resp)
                        {
                            

                            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];
                            CargarListaRequerimientos(usuario_logeado, Convert.ToInt32(IdSolicitud.Text), true);

                            ErroresInferior.Text = "Se ha guardado exitosamente el documento";
                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();
                            this.LimpiarPorFlujoDocumental();
                            FlujoDocumental.SelectedValue = "0";
                            UpdatePanelFlujoDocumental.Update();


                            try
                            {
                                /* Envío de correo electrónico para requerimiento de salida del tipo correo electrónico */
                                enviarCorreo.envioCorreoElectronicoCartaAmbiental(requerimiento);

                            }
                            catch (Exception)
                            {

                            }

                        }
                        else
                        {
                            ErroresInferior.Text = "Ha ocurrido un error al guardar el documento";
                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();

                        }
                    }
                    else
                    {
                        foreach (String error in errores)
                        {
                            Page.Validators.Add(new ValidationError(erroresSumary, error));
                        }
                    }
                }
            }
            UpdatePanelMensajesValidaciones.Update();

            if (this.esInformesResoluciones)
            {
                string script = "mostrarPestanas(" + this.dbPestana + ");";
                ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptMostrarPestanas" + this.dbPestana, script.ToString(), true);
            }

           

        }

    }

}