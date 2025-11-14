using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.common;
using System.Drawing;
using System.Collections;
using Datos.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using System.Data;

namespace SubPesca.Solicitudes.Registrar
{
    public partial class verDocumentoComponente : System.Web.UI.UserControl
    {
        
        
        //RequerimientoDA requerimientoDA = new RequerimientoDA();
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();
        ValidacionDocumentacionDA validacionDocumentacionDA = new ValidacionDocumentacionDA();
        GrupoSuspendidoDA grupoSuspendidoDA = new GrupoSuspendidoDA();
        MantenedorDA mantenedorDA = new MantenedorDA();

        
        RequerimientoService requerimientoService = new RequerimientoService();

        Funciones funciones = new Funciones();

        protected void setearModulo(int Origen)
        {
            if (funciones.retornaModulo().Equals("Registrar"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLCONCESION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLCONCESION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLCONCESION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLCONCESION;
                ViewState["URL_INFORMES_RESOLUCIONES"] = paginas.URL_INGRESO_DOCUMENTO_SOLCONCESION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLCONCESION;

                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_SOLCONCESION;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_SOLCONCESION;
                }
                else
                {
                    //El Request.UrlReferrer indica de que URL anterior fue la solicitud, pero si no existe un URL request arrojara null
                    if (Request.UrlReferrer != null)
                    {
                        ViewState["URL_ORIGEN"] = Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLCONCESION;
                    }
                }


                ViewState["solicitudSession"] = paginas.solicitudConcesionSession;
            }
            else if (funciones.retornaModulo().Equals("Relocalizacion"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_RELOCALIZACION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_RELOCALIZACION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_RELOCALIZACION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_RELOCALIZACION;
                ViewState["URL_INFORMES_RESOLUCIONES"] = paginas.URL_INGRESO_DOCUMENTO_RELOCALIZACION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_RELOCALIZACION;


                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_RELOCALIZACION;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_RELOCALIZACION;
                }
                else
                {
                    //El Request.UrlReferrer indica de que URL anterior fue la solicitud, pero si no existe un URL request arrojara null
                    if (Request.UrlReferrer != null)
                    {
                        ViewState["URL_ORIGEN"] = Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_RELOCALIZACION;
                    }
                }


                ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSession;
            }
            else if (funciones.retornaModulo().Equals("RelocalizacionRESA"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_RELOCALIZACION_RESA;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_RELOCALIZACION_RESA;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_RELOCALIZACION_RESA;
                ViewState["URL_INFORMES_RESOLUCIONES"] = paginas.URL_INGRESO_DOCUMENTO_RELOCALIZACION_RESA;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_RELOCALIZACION_RESA;


                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_RELOCALIZACION_RESA;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_RELOCALIZACION_RESA;
                }
                else
                {
                    //El Request.UrlReferrer indica de que URL anterior fue la solicitud, pero si no existe un URL request arrojara null
                    if (Request.UrlReferrer != null)
                    {
                        ViewState["URL_ORIGEN"] = Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_RELOCALIZACION_RESA;
                    }
                }


                ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSessionRESA;
            }
            else if (funciones.retornaModulo().Equals("Acopio"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_ACOPIO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_ACOPIO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_ACOPIO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_ACOPIO;
                ViewState["URL_INFORMES_RESOLUCIONES"] = paginas.URL_INGRESO_DOCUMENTO_CENTRO_DE_ACOPIO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_CENTRO_DE_ACOPIO;


                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_CENTRO_DE_ACOPIO;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_CENTRO_DE_ACOPIO;
                }
                else
                {
                    //El Request.UrlReferrer indica de que URL anterior fue la solicitud, pero si no existe un URL request arrojara null
                    if (Request.UrlReferrer != null)
                    {
                        ViewState["URL_ORIGEN"] = Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_CENTRO_DE_ACOPIO;
                    }
                }



                ViewState["solicitudSession"] = paginas.solicitudAcopioSession;
            }
            else if (funciones.retornaModulo().Equals("Faenamiento"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_INFORMES_RESOLUCIONES"] = paginas.URL_INGRESO_DOCUMENTO_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_CENTRO_DE_FAENAMIENTO;

                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_CENTRO_DE_FAENAMIENTO;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_CENTRO_DE_FAENAMIENTO;
                }
                else
                {
                    //El Request.UrlReferrer indica de que URL anterior fue la solicitud, pero si no existe un URL request arrojara null
                    if (Request.UrlReferrer != null)
                    {
                        ViewState["URL_ORIGEN"] = Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_CENTRO_DE_FAENAMIENTO;
                    }
                }


                ViewState["solicitudSession"] = paginas.solicitudFaenamientoSession;
            }
            else if (funciones.retornaModulo().Equals("Amerb"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_AMERB;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_AMERB;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_AMERB;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_AMERB;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_AMERB;

                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_SOLICITUD_AMERB;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_SOLICITUD_AMERB;
                }
                else
                {
                    //El Request.UrlReferrer indica de que URL anterior fue la solicitud, pero si no existe un URL request arrojara null
                    if (Request.UrlReferrer != null)
                    {
                        ViewState["URL_ORIGEN"] = Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_AMERB;
                    }
                }

                ViewState["solicitudSession"] = paginas.solicitudAmerbSession;


            }
            else if (funciones.retornaModulo().Equals("ExperimentalesAmerb"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_EXPERIMENTALES_AMERB;

                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_SOLICITUD_EXPERIMENTALES_AMERB;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_SOLICITUD_EXPERIMENTALES_AMERB;
                }
                else
                {
                    //El Request.UrlReferrer indica de que URL anterior fue la solicitud, pero si no existe un URL request arrojara null
                    if (Request.UrlReferrer != null)
                    {
                        ViewState["URL_ORIGEN"] = Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_EXPERIMENTALES_AMERB;
                    }
                }

                ViewState["solicitudSession"] = paginas.solicitudExperimentalesAmerbSession;

            }
            else if (funciones.retornaModulo().Equals("ExperimentalesConcesion"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_EXPERIMENTALES_CONCESION;

                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_SOLICITUD_EXPERIMENTALES_CONCESION;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_SOLICITUD_EXPERIMENTALES_CONCESION;
                }
                else
                {
                    //El Request.UrlReferrer indica de que URL anterior fue la solicitud, pero si no existe un URL request arrojara null
                    if (Request.UrlReferrer != null)
                    {
                        ViewState["URL_ORIGEN"] = Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_EXPERIMENTALES_CONCESION;
                    }
                }

                ViewState["solicitudSession"] = paginas.solicitudExperimentalesConcesionSession;

            }
            else if (funciones.retornaModulo().Equals("ECMPO"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_ECMPO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_ECMPO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_ECMPO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_ECMPO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_ECMPO;

                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_SOLICITUD_ECMPO;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_SOLICITUD_ECMPO;
                }
                else
                {
                    //El Request.UrlReferrer indica de que URL anterior fue la solicitud, pero si no existe un URL request arrojara null
                    if (Request.UrlReferrer != null)
                    {
                        ViewState["URL_ORIGEN"] = Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_ECMPO;
                    }
                }

                ViewState["solicitudSession"] = paginas.solicitudECMPOSession;

            }

            else if (funciones.retornaModulo().Equals("Colector"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_COLECTORES_SEMILLA;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_COLECTORES_SEMILLA;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_COLECTORES_SEMILLA;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_COLECTORES_SEMILLA;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_COLECTORES_SEMILLA;


                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_COLECTORES_SEMILLA;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_COLECTORES_SEMILLA;
                }
                else
                {
                    //El Request.UrlReferrer indica de que URL anterior fue la solicitud, pero si no existe un URL request arrojara null
                    if (Request.UrlReferrer != null)
                    {
                        ViewState["URL_ORIGEN"] = Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_COLECTORES_SEMILLA;
                    }
                }


                ViewState["solicitudSession"] = paginas.solicitudColectorSession;


            }

            else if (funciones.retornaModulo().Equals("ModificacionAmerb"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_AMERB;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_AMERB;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_AMERB;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_AMERB;
                ViewState["URL_INFORMES_RESOLUCIONES"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_AMERB;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_AMERB;


                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_SOLICITUD_MODIFICACION_AMERB;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_SOLICITUD_MODIFICACION_AMERB;
                }
                else
                {
                    //El Request.UrlReferrer indica de que URL anterior fue la solicitud, pero si no existe un URL request arrojara null
                    if (Request.UrlReferrer != null)
                    {
                        ViewState["URL_ORIGEN"] = Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_AMERB;
                    }
                }


                ViewState["solicitudSession"] = paginas.solicitudModificacionAmerbSession;
            }
            else if (funciones.retornaModulo().Equals("ModificacionCentroAcopio"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["URL_INFORMES_RESOLUCIONES"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;


                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                }
                else
                {
                    //El Request.UrlReferrer indica de que URL anterior fue la solicitud, pero si no existe un URL request arrojara null
                    if (Request.UrlReferrer != null)
                    {
                        ViewState["URL_ORIGEN"] = Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                    }
                }


                ViewState["solicitudSession"] = paginas.solicitudModificacionCentroAcopioSession;
            }
            else if (funciones.retornaModulo().Equals("ModificacionCentroFaenamiento"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["URL_INFORMES_RESOLUCIONES"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;


                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                }
                else
                {
                    //El Request.UrlReferrer indica de que URL anterior fue la solicitud, pero si no existe un URL request arrojara null
                    if (Request.UrlReferrer != null)
                    {
                        ViewState["URL_ORIGEN"] = Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                    }
                }


                ViewState["solicitudSession"] = paginas.solicitudModificacionCentroFaenamientoSession;
            }
            else if (funciones.retornaModulo().Equals("ModificacionECMPO"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["URL_INFORMES_RESOLUCIONES"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_ECMPO;


                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_SOLICITUD_MODIFICACION_ECMPO;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_SOLICITUD_MODIFICACION_ECMPO;
                }
                else
                {
                    //El Request.UrlReferrer indica de que URL anterior fue la solicitud, pero si no existe un URL request arrojara null
                    if (Request.UrlReferrer != null)
                    {
                        ViewState["URL_ORIGEN"] = Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_ECMPO;
                    }
                }


                ViewState["solicitudSession"] = paginas.solicitudModificacionECMPOSession;
            }

            else
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLMOD;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLMOD;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLMOD;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLMOD;
                ViewState["URL_INFORMES_RESOLUCIONES"] = paginas.URL_INGRESO_DOCUMENTO_SOLMOD;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLMOD;


                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_SOLMOD;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_SOLMOD;
                }
                else
                {
                    //El Request.UrlReferrer indica de que URL anterior fue la solicitud, pero si no existe un URL request arrojara null
                    if (Request.UrlReferrer != null)
                    {
                        ViewState["URL_ORIGEN"] = Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLMOD;
                    }
                }


                ViewState["solicitudSession"] = paginas.solicitudModificacionSession;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            // PAGE LOAD
            if (!Page.IsPostBack)
            {

                //origen
                //Origen = 0 -> Informes
                //Origen = 1 -> General
                //Origen = 2 -> Inspeccion Terreno

                int Origen = 0;

                if (Request.QueryString["Origen"] != null)
                {
                    Origen = Convert.ToInt32(Request.QueryString["Origen"]);
                }

                setearModulo(Origen);



                if (Request.QueryString["idRequerimiento"] != null)
                {

                    GridSalida.DataSource = requerimientoService.ObtieneRequerimientoDeSalidaSinEstado(Convert.ToInt32(Request.QueryString["idRequerimiento"]));
                    GridSalida.DataBind();

                    GridEntrada.DataSource = requerimientoService.ObtieneRequerimientoDeEntrada(Convert.ToInt32(Request.QueryString["idRequerimiento"]));
                    GridEntrada.DataBind();

                    ValidacionDocGeneral validacionDocGeneral = new ValidacionDocGeneral();

                    Hashtable camposObligatorios = new Hashtable();
                    ViewState["HashCampos"] = validacionDocumentacionDA.ListaValidacionDocGeneral(new ValidacionDocumentacion());

                }

            }
        }



        //1 = entrada
        //2 = salida

        //SALIDA
        //3 = Requerimiento con Respuesta
        //4 = Informativo

        //ENTRADA
        //5 = Respuesta a un Requerimiento
        //6 = Ingreso sin Requerimiento
        protected void RequerimientoCarga(int idRequerimiento, int tipoFlujo)
        {

            this.LimpiarPorFlujoDocumental();
            Hashtable camposObligatorios = (Hashtable)ViewState["HashCampos"];

            Requerimiento requerimiento = null;
            PanelFormulario.Visible = true;
            PanelFlujoDocumental.Visible = true;

            
            if (tipoFlujo == rbTipo.ENTRADA)
            {
                requerimiento = requerimientoService.ObtenerRespuesta(idRequerimiento);
            }

            if (tipoFlujo == rbTipo.SALIDA)
            {
                requerimiento = requerimientoService.ObtenerRequerimientoSinEstado(idRequerimiento);
            }



            //1 = entrada
            if (requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
            {


                //6 = Ingreso sin Requerimiento
                if (requerimiento.tipoEntrada.id == rbTipo.INGRESO_SIN_REQUERIMIENTO)
                {
                    this.controlarCamposLogicos(rbTipo.INGRESO_SIN_REQUERIMIENTO);
                    this.setearCampos(requerimiento, rbTipo.INGRESO_SIN_REQUERIMIENTO);

                }//5 = Respuesta a un Requerimiento
                else if (requerimiento.tipoEntrada.id == rbTipo.RESPUESTA_A_UN_REQUERIMIENTO)
                {
                    this.controlarCamposLogicos(rbTipo.RESPUESTA_A_UN_REQUERIMIENTO);
                    this.setearCampos(requerimiento, rbTipo.RESPUESTA_A_UN_REQUERIMIENTO);
                }


            }//2 = salida
            else if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
            {

                //4 = Informativo
                if (requerimiento.tipoSalida.id == rbTipo.INFORMATIVO)
                {
                    this.controlarCamposLogicos(rbTipo.INFORMATIVO);
                    this.setearCampos(requerimiento, rbTipo.INFORMATIVO);
                }//3 = Requerimiento con Respuesta
                else if (requerimiento.tipoSalida.id == rbTipo.REQUERIMIENTO_CON_RESPUESTA)
                {
                    this.controlarCamposLogicos(rbTipo.REQUERIMIENTO_CON_RESPUESTA);
                    this.setearCampos(requerimiento, rbTipo.REQUERIMIENTO_CON_RESPUESTA);
                }
            }

            UpdatePanelFormulario.Update();
            UpdatePanelFlujoDocumental.Update();
            UpdatePanelTipoSalida.Update();
            UpdatePanelTipoEntrada.Update();
            UpdatePanelOrigen.Update();
            UpdatePanelNumeroRequerimiento.Update();
            UpdatePanelListaRequerimientos.Update();
            UpdatePanelAmbito.Update();
            UpdatePanelTipo.Update();
            UpdatePanelDocumentosAmbito.Update();
            UpdatePanelTipoDocumento.Update();
            UpdatePanelDocumentoPrincipal.Update();
            UpdatePanelNumero.Update();
            UpdatePanelFecha.Update();
            UpdatePanelNumeroCI.Update();
            UpdatePanelFechaCI.Update();
            UpdatePanelResultado.Update();
            UpdatePanelResultadoSupeditado.Update();
            UpdatePanelDestinatario.Update();
            UpdatePanelArchivoAdjunto.Update();
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
                PanelTipoDocumento.Visible = true;
                PanelResultado.Visible = true;
            }


            //ENTRADA
            if (idTipoIO == rbTipo.RESPUESTA_A_UN_REQUERIMIENTO)
            {

                PanelOrigen.Visible = true;
                PanelAmbito.Visible = false;
                PanelNumeroRequerimiento.Visible = true;
                PanelListaRequerimientos.Visible = true; //CORRESPONDE A LA LISTA DE COSAS QUE SE RESPONDIERON EN LA ENTRADA
                PanelTipoDocumento.Visible = true;

            }


            //SALIDA
            if (idTipoIO == rbTipo.INFORMATIVO)
            {
                PanelDestinatario.Visible = true;
                PanelAmbito.Visible = true;
                PanelTipo.Visible = true;
                PanelTipoDocumento.Visible = true;
            }

            //SALIDA
            if (idTipoIO == rbTipo.REQUERIMIENTO_CON_RESPUESTA)
            {
                PanelDestinatario.Visible = true;
                PanelDocumentosAmbito.Visible = true;
                PanelTipoDocumento.Visible = true;
            }
        }




        private void setearCampos(Requerimiento requerimiento, int idTipoIO)
        {

            if (idTipoIO == rbTipo.INGRESO_SIN_REQUERIMIENTO || idTipoIO == rbTipo.INFORMATIVO)
            {

                DocumentoAmbito documentoAmbito = requerimiento.ambitoTipo[0];

                if (documentoAmbito.ambito != null)
                {
                    Ambito.Text = documentoAmbito.ambito.descripcion;
                }

                if (documentoAmbito.tipo != null)
                {
                    Tipo.Text = documentoAmbito.tipo.descripcion;
                }

                if (documentoAmbito.estadoResultadoResp != null)
                {
                    Resultado.Text = documentoAmbito.estadoResultadoResp.descripcion;
                }

                //if (documentoAmbito.tipoResultadoSupeditado != null)
                //{
                //    ResultadoSupeditado.Text = documentoAmbito.tipoResultadoSupeditado.descripcion;
                //}

                /* Desplegar Listado de Grupos Suspendidos */

                List<AsocGrupoSolicitud> asocList = grupoSuspendidoDA.ListarAsocGrupoSolicitudFiltro(0, Convert.ToInt32(requerimiento.solicitud.idSolConcesion), 0, requerimiento.ambitoTipo[0].idDocPestana, 0);
                
                if (asocList != null && asocList.Count > 0)
                {
                    ViewState["AsocGrupoSolicitudList"] = asocList; 
                    GridPendientes_CargaGrilla();

                    PanelGrillaPendientes.Visible = true;
                    UpdatePanelGrillaPendientes.Update();
                }

                /* Desplegar Listado de Tipos de Supeditados */
                List<DependenciaSupeditados> supeditadoList = grupoSuspendidoDA.ListarDependenciaSupeditadosFiltro(0, 0, 0, requerimiento.ambitoTipo[0].idDocPestana);

                if (supeditadoList != null && supeditadoList.Count > 0)
                {
                    ViewState["DependenciaSupeditadosList"] = supeditadoList; 
                    GridSupeditados_CargaGrilla();

                    PanelGrillaSupeditados.Visible = true;
                    UpdatePanelGrillaSupeditados.Update();
                }

            }

            if (idTipoIO == rbTipo.RESPUESTA_A_UN_REQUERIMIENTO)
            {
                ListViewEntradaRespuestaRequerimiento_Carga((List<DocumentoAmbito>)requerimiento.ambitoTipo);
            }

            if (idTipoIO == rbTipo.REQUERIMIENTO_CON_RESPUESTA)
            {
                ViewState["Documentos_Asociados"] = (List<DocumentoAmbito>)requerimiento.ambitoTipo;
                GridViewSalidaDocumentoAsociado_CargaGrilla();
            }




            FlujoDocumental.Text = requerimiento.flujoDocumental.descripcion;


            if (requerimiento.tipoSalida != null)
            {
                TipoSalida.Text = requerimiento.tipoSalida.descripcion;
            }
            if (requerimiento.tipoEntrada != null)
            {
                TipoEntrada.Text = requerimiento.tipoEntrada.descripcion;
            }

            if (requerimiento.tipoDocumento != null)
            {
                TipoDocumento.Text = requerimiento.tipoDocumento.descripcion;
            }

            if (requerimiento.destinatario != null)
            {
                Destinatario.Text = requerimiento.destinatario.descripcion;
            }

            if (requerimiento.origen != null)
            {
                Origen.Text = requerimiento.origen.descripcion;
            }


            DataTable data = requerimientoService.ListarRequerimientosIdSolicitudModificacion(requerimiento.solicitud.idSolConcesion, requerimiento.idReqSalida, 0);
            if (data != null)
            {
                foreach (DataRow row in data.Rows)
                {
                    if (requerimiento.idReqSalida == Convert.ToInt32(row["idDocGeneral"])){
                        NRequerimiento.Text = Convert.ToString(row["nombreReq"]);
                        break;
                    }
                }
            }
            


            if (requerimiento.numero != null && !requerimiento.numero.Trim().Equals(""))
            {
                Numero.Text = requerimiento.numero;
                PanelNumero.Visible = true;
            }

            if (requerimiento.fecha != null && requerimiento.fecha != default(DateTime))
            {
                Fecha.Text = Convert.ToString(requerimiento.fecha);
                PanelFecha.Visible = true;
            }

            if (requerimiento.numeroCI > 0)
            {
                NumeroCI.Text = Convert.ToString(requerimiento.numeroCI);
                PanelNumeroCI.Visible = true;
            }

            if (requerimiento.fechaCI != null && requerimiento.fechaCI != default(DateTime))
            {
                FechaCI.Text = Convert.ToString(requerimiento.fechaCI);
                PanelFechaCI.Visible = true;
            }
            
            if (requerimiento.archivoAdjunto != null)
            {
                ArchivoAdjunto.Text = requerimiento.archivoAdjunto.nombreArchivo;
                PanelArchivo.Visible = true;

            }


            if (Convert.ToInt32(requerimiento.tipoDocumento.id) == rbTipo.INFORME_COMPLEMENTARIO || Convert.ToInt32(requerimiento.tipoDocumento.id) == rbTipo.RESOLUCION_COMPLEMENTARIA)
            {

                DocumentoPrincipal.Text = Convert.ToString(requerimiento.idReqPrincipal);

                if (Convert.ToInt32(requerimiento.tipoDocumento.id) == rbTipo.INFORME_COMPLEMENTARIO)
                {
                    LiteralDocumentoPrincipal.Text = "Informe Principal";
                }
                if (Convert.ToInt32(requerimiento.tipoDocumento.id) == rbTipo.RESOLUCION_COMPLEMENTARIA)
                {
                    LiteralDocumentoPrincipal.Text = "Resolución Principal";
                }

                PanelDocumentoPrincipal.Visible = true;
                
            }

        }

        private void GridPendientes_CargaGrilla()
        {
            List<AsocGrupoSolicitud> List_AsocGrupoSolicitud = (List<AsocGrupoSolicitud>)ViewState["AsocGrupoSolicitudList"];


            if (List_AsocGrupoSolicitud == null)
            {
                List_AsocGrupoSolicitud = new List<AsocGrupoSolicitud>();
            }


            GridPendientes.DataSource = List_AsocGrupoSolicitud;
            GridPendientes.DataBind();

            ViewState["AsocGrupoSolicitudList"] = (List<AsocGrupoSolicitud>)List_AsocGrupoSolicitud;
        }

        private void GridSupeditados_CargaGrilla()
        {
            List<DependenciaSupeditados> List_DependenciaSupeditados = (List<DependenciaSupeditados>)ViewState["DependenciaSupeditadosList"];


            if (List_DependenciaSupeditados == null)
            {
                List_DependenciaSupeditados = new List<DependenciaSupeditados>();
            }

            GridSupeditados.DataSource = List_DependenciaSupeditados;
            GridSupeditados.DataBind();


            ViewState["DependenciaSupeditadosList"] = (List<DependenciaSupeditados>)List_DependenciaSupeditados;
        }


        protected void LimpiarPorFlujoDocumental()
        {
            
            TipoEntrada.Text = "";
            TipoSalida.Text = "";
            Ambito.Text = "";
            Tipo.Text = "";
            TipoDocumento.Text = "";
            DocumentoPrincipal.Text = "";
            Numero.Text = "";
            Fecha.Text = "";
            NuevaFecha.Text = "";
            NumeroCI.Text = "";
            FechaCI.Text = "";
            Resultado.Text = "";
            ResultadoSupeditado.Text = "";
            Destinatario.Text = "";
            Origen.Text = "";
            NRequerimiento.Text = "";

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
            PanelResultadoSupeditado.Visible = false;
            PanelDestinatario.Visible = false;
            PanelArchivo.Visible = false;
            PanelListaRequerimientos.Visible = false;


            ListViewEntradaRespuestaRequerimiento_Carga(null);
            FlujoDocumental.Focus();

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



        //LISTVIEW (ENTRADA - RESPUESTA A UN REQUERIMIENTO)
        protected void ListViewEntradaRespuestaRequerimiento_Carga(List<DocumentoAmbito> respu)
        {

            ListViewEntradaRespuestaRequerimiento.DataSource = (List<DocumentoAmbito>)respu;
            ListViewEntradaRespuestaRequerimiento.DataBind();

            ViewState["Documentos_Asociados_Respuesta"] = (List<DocumentoAmbito>)respu;
        }



        //GRILLA DE SALIDA
        protected void GridSalida_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false; // Invisibiling idDocGeneral Header Cell
                e.Row.Cells[1].Visible = false; // Invisibiling idPestana Header Cell
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridView GridRequerimiento = (GridView)sender;
                int count = GridRequerimiento.Rows.Count;

                GridViewRow row = e.Row;

                String rowspan = ((Label)row.FindControl("hidden4")).Text;

                //PRIMERA FILA CON DATOS
                if (count == 0)
                {
                    row.Cells[2].Visible = true;
                    row.Cells[2].RowSpan = Convert.ToInt32(rowspan);

                    row.Cells[9].Visible = true;
                    row.Cells[9].RowSpan = Convert.ToInt32(rowspan);

                    row.BackColor = Color.FromName("#EFF3FB");


                }
                else if (count > 0)
                {


                    GridViewRow previousRow = GridRequerimiento.Rows[e.Row.RowIndex - 1];

                    String idDocGeneralAnterior = ((Label)previousRow.FindControl("hidden1")).Text;
                    String idPestanaAnterior = ((Label)previousRow.FindControl("hidden2")).Text;

                    String idDocGeneral = ((Label)row.FindControl("hidden1")).Text;
                    String idPestana = ((Label)row.FindControl("hidden2")).Text;



                    if (!idDocGeneralAnterior.Equals(idDocGeneral) || !idPestanaAnterior.Equals(idPestana))
                    {

                        row.Cells[2].Visible = true;
                        row.Cells[2].RowSpan = Convert.ToInt32(rowspan);

                        row.Cells[9].Visible = true;
                        row.Cells[9].RowSpan = Convert.ToInt32(rowspan);


                        if (previousRow.BackColor.ToString().Equals("Color [#EFF3FB]"))
                        {
                            row.BackColor = Color.FromName("#FFFFFF");
                        }
                        else
                        {
                            row.BackColor = Color.FromName("#EFF3FB");
                        }

                    }
                    else
                    {
                        row.Cells[2].RowSpan = 0;
                        row.Cells[2].Visible = false;

                        row.Cells[9].RowSpan = 0;
                        row.Cells[9].Visible = false;

                        row.BackColor = previousRow.BackColor;
                    }
                }


                row.Cells[0].Visible = false; // Invisibiling idDocGeneral Header Cell
                row.Cells[1].Visible = false; // Invisibiling idPestana Header Cell



                //VER
                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    boton_ver.Visible = true;
                };

                //DESCARGAR
                String idArchivoBinSC = DataBinder.Eval(e.Row.DataItem, "idArchivoBinSC").ToString();
                ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                if (boton_descargar != null && idArchivoBinSC != null && !idArchivoBinSC.Equals("") && Convert.ToInt32(idArchivoBinSC) > 0)
                {
                    boton_descargar.Visible = true;
                };
            };
        }


        //GRILLA DE SALIDA
        protected void GridSalida_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Ver":
                    int idRequerimiento = Convert.ToInt32(e.CommandArgument);
                    RequerimientoCarga(idRequerimiento, rbTipo.SALIDA);
                    break;

                case "Descargar":
                    int idArchivo = Convert.ToInt32(e.CommandArgument);

                    ArchivoBinario archivoBinario = archivoBinarioSolicitudDA.ObtenerArchivoBinarioSolicitud(idArchivo);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = archivoBinario.formato;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinario.nombreArchivo + "." + archivoBinario.formato);
                    Response.BinaryWrite(archivoBinario.bytes);
                    Response.Flush();
                    Response.End();

                    break;

            };
        }


        //GRILLA DE SALIDA
        protected void GridSalida_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridSalida = (GridView)sender;

                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Salida";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 8;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridSalida.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }



        //GRILLA ENTRADA
        protected void GridEntrada_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false; // Invisibiling idDocGeneral Header Cell
                e.Row.Cells[1].Visible = false; // Invisibiling idPestana Header Cell
            }


            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                GridView GridRequerimiento = (GridView)sender;
                int count = GridRequerimiento.Rows.Count;

                GridViewRow row = e.Row;

                String rowspan = ((Label)row.FindControl("hiddenEntradaRowspan")).Text;

                //PRIMERA FILA CON DATOS
                if (count == 0)
                {
                    row.Cells[2].Visible = true;
                    row.Cells[2].RowSpan = Convert.ToInt32(rowspan);

                    row.Cells[12].Visible = true;
                    row.Cells[12].RowSpan = Convert.ToInt32(rowspan);

                    row.BackColor = Color.FromName("#EFF3FB");


                }
                else if (count > 0)
                {


                    GridViewRow previousRow = GridRequerimiento.Rows[e.Row.RowIndex - 1];

                    String idDocGeneralAnterior = ((Label)previousRow.FindControl("hiddenEntradaIdDocGeneral")).Text;
                    String idDocGeneralRespAnterior = ((Label)previousRow.FindControl("hiddenEntradaIdDocGeneralResp")).Text;
                    String idPestanaAnterior = ((Label)previousRow.FindControl("hiddenIdPestana")).Text;

                    String idDocGeneral = ((Label)row.FindControl("hiddenEntradaIdDocGeneral")).Text;
                    String idDocGeneralResp = ((Label)row.FindControl("hiddenEntradaIdDocGeneralResp")).Text;
                    String idPestana = ((Label)row.FindControl("hiddenIdPestana")).Text;



                    if (!idDocGeneralAnterior.Equals(idDocGeneral) || !idPestanaAnterior.Equals(idPestana) || !idDocGeneralRespAnterior.Equals(idDocGeneralResp))
                    {

                        row.Cells[2].Visible = true;
                        row.Cells[2].RowSpan = Convert.ToInt32(rowspan);

                        row.Cells[12].Visible = true;
                        row.Cells[12].RowSpan = Convert.ToInt32(rowspan);


                        if (previousRow.BackColor.ToString().Equals("Color [#EFF3FB]"))
                        {
                            row.BackColor = Color.FromName("#FFFFFF");
                        }
                        else
                        {
                            row.BackColor = Color.FromName("#EFF3FB");
                        }

                    }
                    else
                    {
                        row.Cells[2].RowSpan = 0;
                        row.Cells[2].Visible = false;

                        row.Cells[12].RowSpan = 0;
                        row.Cells[12].Visible = false;

                        row.BackColor = previousRow.BackColor;
                    }
                }


                row.Cells[0].Visible = false; // Invisibiling idDocGeneral Header Cell
                row.Cells[1].Visible = false; // Invisibiling idPestana Header Cell



                //VER
                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    boton_ver.Visible = true;
                };

                //DESCARGAR
                String idArchivoBinSC = DataBinder.Eval(e.Row.DataItem, "idArchivoBinSC").ToString();
                ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                if (boton_descargar != null && idArchivoBinSC != null && !idArchivoBinSC.Equals("") && Convert.ToInt32(idArchivoBinSC) > 0)
                {
                    boton_descargar.Visible = true;
                };
            };
        }


        //GRILLA ENTRADA
        protected void GridEntrada_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Ver":
                    int idRequerimiento = Convert.ToInt32(e.CommandArgument);
                    RequerimientoCarga(idRequerimiento, rbTipo.ENTRADA);
                    break;

                case "Descargar":
                    int idArchivo = Convert.ToInt32(e.CommandArgument);

                    ArchivoBinario archivoBinario = archivoBinarioSolicitudDA.ObtenerArchivoBinarioSolicitud(idArchivo);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = archivoBinario.formato;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinario.nombreArchivo + "." + archivoBinario.formato);
                    Response.BinaryWrite(archivoBinario.bytes);
                    Response.Flush();
                    Response.End();

                    break;

            };
        }



        //GRILLA ENTRADA
        protected void GridEntrada_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridEntrada = (GridView)sender;


                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);


                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Entrada";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 11;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridEntrada.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(ViewState["URL_ORIGEN"].ToString());
        }

    }
}