using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.modificacion;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.modificacion;
using Validaciones.cl.subpesca.rb.modificacion;
using Datos.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using SubPesca.Utilidades;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace SubPesca.Solicitudes.Modificacion
{


    //unidades de dependencia se eliminada como funcionalidad
    public partial class unidadDeDependencia : System.Web.UI.UserControl
    {

        //Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        //SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();
        //UnidadDependenciaValidacion unidadDependenciaValidacion = new UnidadDependenciaValidacion();
        //PermisosService permisosService = new PermisosService();

        //SolicitudDA solicitudDA = new SolicitudDA();
        //UnidadDependenciaModDA unidadDependenciaModDA = new UnidadDependenciaModDA();
        //TipoDA tipoDa = new TipoDA();
        //Funciones funciones = new Funciones();

        //protected void setearModulo()
        //{

        //    ValidacionDocumentacion validacionDocumentacion = new ValidacionDocumentacion();

        //    if (funciones.retornaModulo().Equals("Registrar"))
        //    {
        //        ViewState["URL_VER"] = paginas.URL_VER_SOLCONCESION;
        //        ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLCONCESION;
        //        ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLCONCESION;
        //        ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLCONCESION;
        //        ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLCONCESION;
        //        ViewState["solicitudSession"] = paginas.solicitudConcesionSession;

        //        ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_CONCESION };

        //    }
        //    else if (funciones.retornaModulo().Equals("Relocalizacion"))
        //    {
        //        ViewState["URL_VER"] = paginas.URL_VER_RELOCALIZACION;
        //        ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_RELOCALIZACION;
        //        ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_RELOCALIZACION;
        //        ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_RELOCALIZACION;
        //        ViewState["URL_ERROR"] = paginas.URL_ERROR_RELOCALIZACION;
        //        ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSession;

        //        SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

        //        if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA)
        //        {
        //            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_RELOCALIZACION_CREA };
        //        }
        //        if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA)
        //        {
        //            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_RELOCALIZACION_FUSIONA };
        //        }
        //        if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO)
        //        {
        //            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_RELOCALIZACION_SECTOR_CERO };
        //        }

        //    }
        //    else if (funciones.retornaModulo().Equals("RelocalizacionRESA"))
        //    {
        //        ViewState["URL_VER"] = paginas.URL_VER_RELOCALIZACION_RESA;
        //        ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_RELOCALIZACION_RESA;
        //        ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA;
        //        ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_RELOCALIZACION_RESA;
        //        ViewState["URL_ERROR"] = paginas.URL_ERROR_RELOCALIZACION_RESA;
        //        ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSessionRESA;

        //        SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

        //        if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA_RESA)
        //        {
        //            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_RELOCALIZACION_CREA_RESA };
        //        }
        //        if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA_RESA)
        //        {
        //            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_RELOCALIZACION_FUSIONA_RESA };
        //        }
        //        if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO_RESA)
        //        {
        //            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_RELOCALIZACION_SECTOR_CERO_RESA };
        //        }

        //    }
        //    else if (funciones.retornaModulo().Equals("Acopio"))
        //    {
        //        ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_ACOPIO;
        //        ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_ACOPIO;
        //        ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_ACOPIO;
        //        ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_ACOPIO;
        //        ViewState["URL_ERROR"] = paginas.URL_ERROR_CENTRO_DE_ACOPIO;
        //        ViewState["solicitudSession"] = paginas.solicitudAcopioSession;

        //        ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_ACOPIO };

        //    }
        //    else if (funciones.retornaModulo().Equals("Faenamiento"))
        //    {
        //        ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_FAENAMIENTO;
        //        ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_FAENAMIENTO;
        //        ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_FAENAMIENTO;
        //        ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_FAENAMIENTO;
        //        ViewState["URL_ERROR"] = paginas.URL_ERROR_CENTRO_DE_FAENAMIENTO;
        //        ViewState["solicitudSession"] = paginas.solicitudFaenamientoSession;

        //        ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_FAENAMIENTO };

        //    }
        //    else if (funciones.retornaModulo().Equals("Amerb"))
        //    {
        //        ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_AMERB;
        //        ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_AMERB;
        //        ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_AMERB;
        //        ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_AMERB;
        //        ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_AMERB;
        //        ViewState["solicitudSession"] = paginas.solicitudAmerbSession;

        //        validacionDocumentacion.aplicaAmerb = 1;
        //        ViewState["validacionDocumentacion"] = validacionDocumentacion;

        //        ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_AMERB };

        //    }
        //    else if (funciones.retornaModulo().Equals("ExperimentalesAmerb"))
        //    {
        //        ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_EXPERIMENTALES_AMERB;
        //        ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_AMERB;
        //        ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_EXPERIMENTALES_AMERB;
        //        ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_EXPERIMENTALES_AMERB;
        //        ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_EXPERIMENTALES_AMERB;
        //        ViewState["solicitudSession"] = paginas.solicitudExperimentalesAmerbSession;

        //        validacionDocumentacion.aplicaExperimentalesAmerb = 1;
        //        ViewState["validacionDocumentacion"] = validacionDocumentacion;

        //        ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_EXPERIMENTALES_AMERB };

        //    }
        //    else if (funciones.retornaModulo().Equals("ECMPO"))
        //    {
        //        ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_ECMPO;
        //        ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_ECMPO;
        //        ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_ECMPO;
        //        ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_ECMPO;
        //        ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_ECMPO;
        //        ViewState["solicitudSession"] = paginas.solicitudECMPOSession;

        //        validacionDocumentacion.aplicaAcuiculturaEcmpo = 1;
        //        ViewState["validacionDocumentacion"] = validacionDocumentacion;

        //        ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_ECMPO };

        //    }
        //    else if (funciones.retornaModulo().Equals("ExperimentalesConcesion"))
        //    {
        //        ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_EXPERIMENTALES_CONCESION;
        //        ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_CONCESION;
        //        ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_EXPERIMENTALES_CONCESION;
        //        ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_EXPERIMENTALES_CONCESION;
        //        ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_EXPERIMENTALES_CONCESION;
        //        ViewState["solicitudSession"] = paginas.solicitudExperimentalesConcesionSession;

        //        validacionDocumentacion.aplicaExpConcesion = 1;
        //        ViewState["validacionDocumentacion"] = validacionDocumentacion;

        //        ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_EXPERIMENTALES_CONCESION };

        //    }

        //    else if (funciones.retornaModulo().Equals("Colector"))
        //    {
        //        ViewState["URL_VER"] = paginas.URL_VER_COLECTORES_SEMILLA;
        //        ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_COLECTORES_SEMILLA;
        //        ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_COLECTORES_SEMILLA;
        //        ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_COLECTORES_SEMILLA;
        //        ViewState["URL_ERROR"] = paginas.URL_ERROR_COLECTORES_SEMILLA;
        //        ViewState["solicitudSession"] = paginas.solicitudColectorSession;

        //        validacionDocumentacion.aplicaColectores = 1;
        //        ViewState["validacionDocumentacion"] = validacionDocumentacion;

        //        ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_COLECTOR };

        //    }
        //    else if (funciones.retornaModulo().Equals("ModificacionAmerb"))
        //    {
        //        ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_AMERB;
        //        ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_AMERB;
        //        ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_AMERB;
        //        ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_AMERB;
        //        ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_AMERB;
        //        ViewState["solicitudSession"] = paginas.solicitudModificacionAmerbSession;

        //        SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];


        //        if (solicitudConcesion != null)
        //        {

        //            int[] tiposModificacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

        //            int contador = 0;
        //            foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
        //            {
        //                if (tipoModificacion.id == rbTipo.MOD_AMERB_AMPLIA_SUPERFICIE)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_AMERB_AMPLIA_SUPERFICIE;
        //                    contador++;
        //                }
        //                if (tipoModificacion.id == rbTipo.MOD_AMERB_ESPECIE)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_AMERB_ESPECIE;
        //                    contador++;
        //                }
        //                if (tipoModificacion.id == rbTipo.MOD_AMERB_PT)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_AMERB_PT;
        //                    contador++;
        //                }
        //                if (tipoModificacion.id == rbTipo.MOD_AMERB_REDUCE_SUPERFICIE)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_AMERB_REDUCE_SUPERFICIE;
        //                    contador++;
        //                }
        //                if (tipoModificacion.id == rbTipo.MOD_AMERB_REGULARIZACION)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_AMERB_REGULARIZACION;
        //                    contador++;
        //                }
        //            }

        //            ViewState["SECCION_ESPECIFICA"] = tiposModificacion;

        //        }
        //    }
        //    else if (funciones.retornaModulo().Equals("ModificacionCentroAcopio"))
        //    {
        //        ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
        //        ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
        //        ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
        //        ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
        //        ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
        //        ViewState["solicitudSession"] = paginas.solicitudModificacionCentroAcopioSession;

        //        SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];


        //        if (solicitudConcesion != null)
        //        {

        //            int[] tiposModificacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

        //            int contador = 0;
        //            foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
        //            {
        //                if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
        //                    contador++;
        //                }
        //                if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_RENOVACION)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_CENTRO_ACOPIO_ESPECIE;
        //                    contador++;
        //                }
        //                if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_PT_ESPECIE)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_CENTRO_ACOPIO_PT;
        //                    contador++;
        //                }
        //                if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
        //                    contador++;
        //                }
        //                if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REGULARIZACION)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_CENTRO_ACOPIO_REGULARIZACION;
        //                    contador++;
        //                }
        //            }

        //            ViewState["SECCION_ESPECIFICA"] = tiposModificacion;

        //        }
        //    }
        //    else if (funciones.retornaModulo().Equals("ModificacionCentroFaenamiento"))
        //    {
        //        ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
        //        ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
        //        ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
        //        ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
        //        ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
        //        ViewState["solicitudSession"] = paginas.solicitudModificacionCentroFaenamientoSession;

        //        SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];


        //        if (solicitudConcesion != null)
        //        {

        //            int[] tiposModificacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

        //            int contador = 0;
        //            foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
        //            {
        //                if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
        //                    contador++;
        //                }
        //                if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_RENOVACION)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_CENTRO_FAENAMIENTO_ESPECIE;
        //                    contador++;
        //                }
        //                if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_PT_ESPECIE)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_CENTRO_FAENAMIENTO_PT;
        //                    contador++;
        //                }
        //                if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
        //                    contador++;
        //                }
        //                if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REGULARIZACION)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_CENTRO_FAENAMIENTO_REGULARIZACION;
        //                    contador++;
        //                }
        //            }

        //            ViewState["SECCION_ESPECIFICA"] = tiposModificacion;

        //        }
        //    }
        //    else if (funciones.retornaModulo().Equals("ModificacionECMPO"))
        //    {
        //        ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_ECMPO;
        //        ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_ECMPO;
        //        ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_ECMPO;
        //        ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_ECMPO;
        //        ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_ECMPO;
        //        ViewState["solicitudSession"] = paginas.solicitudModificacionECMPOSession;

        //        SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];


        //        if (solicitudConcesion != null)
        //        {

        //            int[] tiposModificacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

        //            int contador = 0;
        //            foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
        //            {
        //                if (tipoModificacion.id == rbTipo.MOD_ECMPO_AMPLIA_SUPERFICIE)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_ECMPO_AMPLIA_SUPERFICIE;
        //                    contador++;
        //                }
        //                if (tipoModificacion.id == rbTipo.MOD_ECMPO_ESPECIE)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_ECMPO_ESPECIE;
        //                    contador++;
        //                }
        //                if (tipoModificacion.id == rbTipo.MOD_ECMPO_PT)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_ECMPO_PT;
        //                    contador++;
        //                }
        //                if (tipoModificacion.id == rbTipo.MOD_ECMPO_REDUCE_SUPERFICIE)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_ECMPO_REDUCE_SUPERFICIE;
        //                    contador++;
        //                }
        //                if (tipoModificacion.id == rbTipo.MOD_ECMPO_REGULARIZACION)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_ECMPO_REGULARIZACION;
        //                    contador++;
        //                }
        //            }

        //            ViewState["SECCION_ESPECIFICA"] = tiposModificacion;

        //        }
        //    }

        //    else
        //    {
        //        ViewState["URL_VER"] = paginas.URL_VER_SOLMOD;
        //        ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLMOD;
        //        ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLMOD;
        //        ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLMOD;
        //        ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLMOD;
        //        ViewState["solicitudSession"] = paginas.solicitudModificacionSession;

        //        SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];


        //        if (solicitudConcesion != null)
        //        {

        //            int[] tiposModificacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

        //            int contador = 0;
        //            foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
        //            {
        //                if (tipoModificacion.id == rbTipo.MOD_CONCESION_AMPLIA_SUPERFICIE)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_CONCESION_AMPLIA_SUPERFICIE;
        //                    contador++;
        //                }
        //                if (tipoModificacion.id == rbTipo.MOD_CONCESION_ESPECIE)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_CONCESION_ESPECIE;
        //                    contador++;
        //                }
        //                if (tipoModificacion.id == rbTipo.MOD_CONCESION_PT)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_CONCESION_PT;
        //                    contador++;
        //                }
        //                if (tipoModificacion.id == rbTipo.MOD_CONCESION_REDUCE_SUPERFICIE)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_CONCESION_REDUCE_SUPERFICIE;
        //                    contador++;
        //                }
        //                if (tipoModificacion.id == rbTipo.MOD_CONCESION_REGULARIZACION)
        //                {
        //                    tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_DEPENDENCIA_MOD_CONCESION_REGULARIZACION;
        //                    contador++;
        //                }
        //            }

        //            ViewState["SECCION_ESPECIFICA"] = tiposModificacion;

        //        }


        //    }
        //}

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    // PAGE LOAD
        //    if (!Page.IsPostBack)
        //    {
        //        setearModulo();
        //        Initialize_Form();

        //        SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
        //        usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

        //        if (solicitudConcesion == null || usuario_logeado == null)
        //        {
        //            Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
        //        }

        //        //BOTON DE INGRESO O MODIFICACION
        //        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
        //        {
        //            PanelFormularioIngreso.Visible = true;
        //            PanelBotonGuardar.Visible = true;

        //        }else if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], this.usuario_logeado, solicitudConcesion, rbAccion.ELIMINAR))
        //        {
        //            PanelBotonGuardar.Visible = true;

        //        }else
        //        {
        //            PanelFormularioIngreso.Visible = false;
        //            PanelBotonGuardar.Visible = false;
        //        }

        //    }
        //}

        //private void Initialize_Form()
        //{
        //    UnidadDeDependencia.Items.Clear();
        //    UnidadDeDependencia.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        //    UnidadDeDependencia.Items.Insert(1, new ListItem("Sí", Convert.ToString(rbTipo.UNIDAD_DEPENDENCIA_SI)));
        //    UnidadDeDependencia.Items.Insert(2, new ListItem("No", Convert.ToString(rbTipo.UNIDAD_DEPENDENCIA_NO)));
        //    UnidadDeDependencia.DataBind();

        //    // Cargamos el combobox
        //    Initialize_Comboboxs();

        //    //Inicializar formulario
        //    Initialize_Formulario();

        //}

        //private void Initialize_Formulario()
        //{
        //    SolicitudConcesion solicitudInicial = (SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

        //   if (solicitudInicial != null && solicitudInicial.idSolConcesion > 0)
        //   {
        //       IdSolicitud.Text = Convert.ToString(solicitudInicial.idSolConcesion);

        //       UnidadDeDependencia.SelectedValue = Convert.ToString(solicitudInicial.aplicaDependencia);

        //       CargarListaDependencias(solicitudInicial.idSolConcesion);

        //       this.unidadDeDependencia_SelectedIndexChanged(null, null);
        //   }
        //}

        //private void Initialize_Comboboxs()
        //{
        //    Carga_Combobox("TipoUnidadDependencia");
        //    TipoUnidadDependencia.SelectedValue = "0";
        //}

        //private void Carga_Combobox(string combobox)
        //{
        //    switch (combobox)
        //    {

        //        case "TipoUnidadDependencia":
        //            // Cargamos el combobox: TipoUnidadDependencia
        //            TipoUnidadDependencia.Items.Clear();
        //            TipoUnidadDependencia.DataSource = tipoDa.ListarTipo("TIPO_UNID_DEPENDENCIA");
        //            TipoUnidadDependencia.DataTextField = "descripcion";
        //            TipoUnidadDependencia.DataValueField = "id";
        //            TipoUnidadDependencia.DataBind();
        //            TipoUnidadDependencia.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

        //            break;
        //    }
        //}

        //protected void unidadDeDependencia_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Content_msgGrilla.Visible = false;

        //    if (Convert.ToInt32(UnidadDeDependencia.SelectedValue) == rbTipo.UNIDAD_DEPENDENCIA_SI)
        //    {
        //        PanelUnidadDependencia.Visible = true;
        //        UpdatePanelUnidadDependencia.Update();

        //        PanelBotonUnidadDependencia.Visible = true;
        //        UpdatePanelBotonUnidadDependencia.Update();

        //        PanelGrillaUnidadDependencia.Visible = true;
        //        UpdatePanelGrillaUnidadDependencia.Update();

        //    }
        //    else if (Convert.ToInt32(UnidadDeDependencia.SelectedValue) == rbTipo.UNIDAD_DEPENDENCIA_NO)
        //    {
        //        PanelUnidadDependencia.Visible = false;
        //        UpdatePanelUnidadDependencia.Update();

        //        PanelGrillaUnidadDependencia.Visible = false;
        //        UpdatePanelGrillaUnidadDependencia.Update();

        //        PanelBotonUnidadDependencia.Visible = true;
        //        UpdatePanelBotonUnidadDependencia.Update();

                
        //    }else{
        //        PanelUnidadDependencia.Visible = false;
        //        UpdatePanelUnidadDependencia.Update();

        //        PanelBotonUnidadDependencia.Visible = true;
        //        UpdatePanelBotonUnidadDependencia.Update();

        //    }
        //}

        //protected void limpiarFormulario(SolicitudConcesion solicitudConcesion)
        //{

        //    if (solicitudConcesion != null && solicitudConcesion.aplicaDependencia == rbTipo.UNIDAD_DEPENDENCIA_SI)
        //    {
        //        //UnidadDeDependencia.SelectedValue = "0";
        //        //UpdatePanelUnidDependencia.Update();

        //        TipoUnidadDependencia.SelectedValue = "0";
        //        DetalleUnidadDependencia.Text = "";
        //        UpdatePanelUnidadDependencia.Update();

        //        PanelUnidadDependencia.Visible = true;
        //        UpdatePanelUnidadDependencia.Update();

        //        PanelGrillaUnidadDependencia.Visible = true;
        //        UpdatePanelGrillaUnidadDependencia.Update();

        //        PanelBotonUnidadDependencia.Visible = true;
        //        UpdatePanelBotonUnidadDependencia.Update();
        //    }
        //    else {

        //        //UnidadDeDependencia.SelectedValue = "0";
        //        //UpdatePanelUnidDependencia.Update();

        //        TipoUnidadDependencia.SelectedValue = "0";
        //        DetalleUnidadDependencia.Text = "";
        //        UpdatePanelUnidadDependencia.Update();

        //        PanelUnidadDependencia.Visible = false;
        //        UpdatePanelUnidadDependencia.Update();

        //        ViewState["UnidadDependenciaMod"] = null;
        //        PanelGrillaUnidadDependencia.Visible = false;
        //        UpdatePanelGrillaUnidadDependencia.Update();

        //        PanelBotonUnidadDependencia.Visible = true;
        //        UpdatePanelBotonUnidadDependencia.Update();
        //    }
        //}

        //protected void ButtonUnidadDependencia_Click(object sender, EventArgs e)
        //{
        //    SolicitudConcesion solicitudConcesion = new SolicitudConcesion();
        //    solicitudConcesion.idSolConcesion = Convert.ToInt32(IdSolicitud.Text);

        //    int unidadDependencia = Convert.ToInt32(UnidadDeDependencia.Text);
        //    if (unidadDependencia == rbTipo.UNIDAD_DEPENDENCIA_SI)
        //    {
        //        solicitudConcesion.aplicaDependencia = rbTipo.UNIDAD_DEPENDENCIA_SI;

        //        List<UnidadDependenciaMod> unidadDependenciaList = (List<UnidadDependenciaMod>)ViewState["UnidadDependenciaMod"];
        //        solicitudConcesion.unidadDependenciaList = unidadDependenciaList;

        //    }
        //    else if (unidadDependencia == rbTipo.UNIDAD_DEPENDENCIA_NO)
        //    {
        //        solicitudConcesion.aplicaDependencia = rbTipo.UNIDAD_DEPENDENCIA_NO;
        //    }else{
        //        solicitudConcesion.aplicaDependencia = 0;
        //    }

        //     List<String> listaErroresUnidadDependencia = new List<String>();

        //     if (solicitudConcesion.aplicaDependencia == rbTipo.UNIDAD_DEPENDENCIA_SI && (solicitudConcesion.unidadDependenciaList == null || solicitudConcesion.unidadDependenciaList.Count() == 0))
        //     {

        //        msgGrilla.Text = "Debe guardar al menos 1 detalle de Unidad de Dependencia antes de presionar el boton \"Guardar\"";
        //        Content_msgGrilla.Visible = true;
        //        Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
        //        UpdatePanelUnidDepend.Update();

        //        //foreach (String error in listaErroresUnidadDependencia)
        //        //{
        //        //    Page.Validators.Add(new ValidationError("grupoUnidDependencia", error));
        //        //}

        //     }
        //     else { 
             
        //         bool resp = solicitudModificacionService.guardarUnidadDependenciaSolicitud(solicitudConcesion);
                 

        //         if (resp)
        //         {

        //             SolicitudConcesion solAux = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
        //             if (solAux != null) {
        //                 solAux.aplicaDependencia = solicitudConcesion.aplicaDependencia;
        //                 Session[ViewState["solicitudSession"].ToString()] = solAux; //ACTUALIZANDO EL CAMBIO EN LA SOLICITUD DEL VIEW STATE
        //             }
                     
        //             msgGrilla.Text = "Acción realizada con éxito.";
        //         }
        //         else {
        //             msgGrilla.Text = "Ha ocurrido un error al realizar la accion solicitada.";
        //         }
        //         Content_msgGrilla.Visible = true;
        //         Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
        //         UpdatePanelUnidDepend.Update();

        //         CargarListaDependencias(Convert.ToInt32(IdSolicitud.Text));

        //         limpiarFormulario(solicitudConcesion);
                 
        //     }
             
        //}

        //protected void GridUnidadDependencia_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        // Borrar

        //        HiddenField hidden_idUnidDependencia = (HiddenField)e.Row.FindControl("gUnidadDependencia");
        //        if (hidden_idUnidDependencia != null && !hidden_idUnidDependencia.Value.Equals("") && Convert.ToInt32(hidden_idUnidDependencia.Value)>0)
        //        {
        //            ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
        //            if (boton_eliminar != null)
        //            {
        //                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ELIMINAR))
        //                {
        //                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar la dependencia?')");
        //                    boton_eliminar.Visible = true;
        //                }
        //            };
        //        }
        //    }
        //}

        //protected void GridUnidadDependencia_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    int idUnidadDependencia = Convert.ToInt32(e.CommandArgument);
        //    switch (e.CommandName)
        //    {
        //        case "Eliminar":


        //            if (idUnidadDependencia > 0)
        //            {
        //                bool resp = unidadDependenciaModDA.EliminarUnidadDependenciaModFiltro(idUnidadDependencia);
        //                if (resp)
        //                {
        //                    CargarListaDependencias(Convert.ToInt32(IdSolicitud.Text));

        //                    msgGrilla.Text = "Se ha eliminado exitosamente la unidad de dependencia de la solicitud.";
        //                }
        //                else
        //                {
        //                    msgGrilla.Text = "No se ha eliminado la unidad la dependencia a la solicitud.";
        //                }
        //                Content_msgGrilla.Visible = true;
        //                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
        //                UpdatePanelUnidDepend.Update();
        //            }
                    
        //            break;
        //    }
        //}
        
        //protected void GuardarUnindadDependencia_Click(object sender, ImageClickEventArgs e)
        //{
        //    List<UnidadDependenciaMod> unidadDependenciaList = (List<UnidadDependenciaMod>)ViewState["UnidadDependenciaMod"];
        //    UnidadDependenciaMod unidadDependenciaMod = new UnidadDependenciaMod();
            
        //    int index = 0;
        //    if (unidadDependenciaList == null)
        //    {
        //        unidadDependenciaList = new List<UnidadDependenciaMod>();
        //    }
        //    else
        //    {
        //        int indexAux = 0;
        //        foreach (UnidadDependenciaMod unidadDependenciaAux in unidadDependenciaList)
        //        {
        //            unidadDependenciaAux.index = indexAux;
        //            indexAux++;
        //        }
        //        index = unidadDependenciaList.Count;
        //    }

        //    unidadDependenciaMod.idSolConcesion = Convert.ToInt32(IdSolicitud.Text);
        //    unidadDependenciaMod.index = Convert.ToInt32(index);
        //    unidadDependenciaMod.tipoUnidDependencia = new ParametroGenerico();
        //    unidadDependenciaMod.tipoUnidDependencia.id = Convert.ToInt32(TipoUnidadDependencia.SelectedValue);
        //    unidadDependenciaMod.claveUnidDependencia = DetalleUnidadDependencia.Text;

        //    /* Se guarda el estado actual de la unidad de dependencia */

        //    int idSolicitudConcesion = unidadDependenciaModDA.ObtenerSolicitudUnidadDepExistente(unidadDependenciaMod.tipoUnidDependencia.id, unidadDependenciaMod.claveUnidDependencia);

        //    if (idSolicitudConcesion > 0)
        //    {
        //        SolicitudConcesion solicitudDependencia = solicitudDA.ObtieneSolicitudConcesion(idSolicitudConcesion, 0);

        //        unidadDependenciaMod.estadoSolicitudDep = solicitudDependencia.estadoActual;
        //    }

        //    List<String> listaErroresUnidadDependencia = unidadDependenciaValidacion.validaDetalleUnidadDependenciaMod(unidadDependenciaMod, unidadDependenciaList);

        //    if (listaErroresUnidadDependencia != null && listaErroresUnidadDependencia.Count <= 0)
        //    {
        //        unidadDependenciaList.Add(unidadDependenciaMod);

        //        GridUnidadDependencia.DataSource = unidadDependenciaList;
        //        GridUnidadDependencia.DataBind();

        //        PanelGrillaUnidadDependencia.Visible = true;
        //        UpdatePanelGrillaUnidadDependencia.Update();

        //        ViewState["UnidadDependenciaMod"] = (List<UnidadDependenciaMod>)unidadDependenciaList;
        //    }
        //    else
        //    {
        //        foreach (String error in listaErroresUnidadDependencia)
        //        {
        //            Page.Validators.Add(new ValidationError("grupoUnidDependencia", error));
        //        }
        //        UpdatePanelUnidDepend.Update();
        //    }

        //    TipoUnidadDependencia.SelectedValue = "0";
        //    DetalleUnidadDependencia.Text = "";
        //}

        //private void CargarListaDependencias(int idSolicitud)
        //{
        //    List <UnidadDependenciaMod> unidadDependenciaModList =unidadDependenciaModDA.ListarUnidadDependenciaMod(Convert.ToInt32(IdSolicitud.Text));
                
        //    int i= 0;
        //    foreach (UnidadDependenciaMod unidadDependenciaMod in unidadDependenciaModList)
        //    {
        //        unidadDependenciaMod.index = i;
        //        i++;
        //    }

        //    GridUnidadDependencia.DataSource = unidadDependenciaModList;
        //    GridUnidadDependencia.DataBind();

        //    PanelGrillaUnidadDependencia.Visible = true;
        //    UpdatePanelGrillaUnidadDependencia.Update();

        //    ViewState["UnidadDependenciaMod"] = (List<UnidadDependenciaMod>)unidadDependenciaModList;
        //}
    
    
    }
}