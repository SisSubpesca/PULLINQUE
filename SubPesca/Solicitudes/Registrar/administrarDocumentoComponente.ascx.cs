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
using SubPesca.Mantenedores.Generales;


namespace SubPesca.Solicitudes.Registrar
{
    public partial class administrarDocumentoComponente : System.Web.UI.UserControl
    {


        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema


        PestanaDA pestanaDA = new PestanaDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        ValidacionDocumentacionDA validacionDocumentacionDA = new ValidacionDocumentacionDA();
        GrupoSuspendidoDA grupoSuspendidoDA = new GrupoSuspendidoDA();
        SolicitudDA solicitudDA = new SolicitudDA();

        IngresarDocumentoValidacion ingresarDocumentoValidacion = new IngresarDocumentoValidacion();
        AdministrarDocumentoValidacion administrarDocumentoValidacion = new AdministrarDocumentoValidacion();
        
        
        
        RequerimientoService requerimientoService = new RequerimientoService();
        PermisosService permisosService = new PermisosService();
        MantenedorGeneralService mantenedorGeneralService = new MantenedorGeneralService();

        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();
        Funciones funciones = new Funciones();

        int dbPestana = rbPestana.CERO;
        int dbSeccion = rbSeccion.CERO;



        String GrupoFlujo = "GrupoFlujo" + Convert.ToString(rbSeccion.CERO);
        String GrupoEntrada = "GrupoEntrada" + Convert.ToString(rbSeccion.CERO);
        String GrupoSalida = "GrupoSalida" + Convert.ToString(rbSeccion.CERO);

        String GrupoRespuestaRequerimiento = "GrupoRespuestaRequerimiento" + Convert.ToString(rbSeccion.CERO);
        String GrupoIngresoSinRequerimiento = "GrupoIngresoSinRequerimiento" + Convert.ToString(rbSeccion.CERO);
        String GrupoRequerimientoConRespuesta = "GrupoRequerimientoConRespuesta" + Convert.ToString(rbSeccion.CERO);
        String GrupoInformativo = "GrupoInformativo" + Convert.ToString(rbSeccion.CERO);

        String GrupoDocumentoAsociado = "GrupoDocumentoAsociado" + Convert.ToString(rbSeccion.CERO);
        String GrupoSubmitIngreso = "GrupoSubmitIngreso" + Convert.ToString(rbSeccion.CERO);

        

        public static administrarDocumentoComponente Instance { get; set; }

        public administrarDocumentoComponente()
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
            Carga_Combobox("TipoPendiente");
            UpdatePanelTipoPendiente.Update();
        }

        protected void setearModulo(int Origen)
        {

            ValidacionDocumentacion validacionDocumentacion = new ValidacionDocumentacion();

            if (funciones.retornaModulo().Equals("Registrar"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLCONCESION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLCONCESION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLCONCESION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLCONCESION;
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
                    //ViewState["URL_ORIGEN"] = Request.UrlReferrer;
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLCONCESION;
                }



                ViewState["solicitudSession"] = paginas.solicitudConcesionSession;

                validacionDocumentacion.aplicaConcesion = 1;
                ViewState["validacionDocumentacion"] = validacionDocumentacion;

            }
            else if (funciones.retornaModulo().Equals("Relocalizacion"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_RELOCALIZACION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_RELOCALIZACION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_RELOCALIZACION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_RELOCALIZACION;
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
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_RELOCALIZACION;
                }


                ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSession;

                validacionDocumentacion.aplicaRelocalizacion = 1;
                ViewState["validacionDocumentacion"] = validacionDocumentacion;
            }
            else if (funciones.retornaModulo().Equals("RelocalizacionRESA"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_RELOCALIZACION_RESA;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_RELOCALIZACION_RESA;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_RELOCALIZACION_RESA;
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
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_RELOCALIZACION_RESA;
                }


                ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSessionRESA;

                validacionDocumentacion.aplicaRelocalizacion = 1;
                ViewState["validacionDocumentacion"] = validacionDocumentacion;
            }
            else if (funciones.retornaModulo().Equals("Acopio"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_ACOPIO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_ACOPIO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_ACOPIO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_ACOPIO;
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
                    //Request.UrlReferrer => obtiene la url anterior
                    //ViewState["URL_ORIGEN"] = Request.UrlReferrer;
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_CENTRO_DE_ACOPIO;
                }


                ViewState["solicitudSession"] = paginas.solicitudAcopioSession;

                validacionDocumentacion.aplicaAcopio = 1;
                ViewState["validacionDocumentacion"] = validacionDocumentacion;

            }
            else if (funciones.retornaModulo().Equals("Faenamiento"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_FAENAMIENTO;
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
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_CENTRO_DE_FAENAMIENTO;
                }


                ViewState["solicitudSession"] = paginas.solicitudFaenamientoSession;

                validacionDocumentacion.aplicaFaenamiento = 1;
                ViewState["validacionDocumentacion"] = validacionDocumentacion;

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
                    //ViewState["URL_ORIGEN"] = Request.UrlReferrer.ToString();
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_AMERB;
                }


                ViewState["solicitudSession"] = paginas.solicitudAmerbSession;

                validacionDocumentacion.aplicaAmerb = 1;
                ViewState["validacionDocumentacion"] = validacionDocumentacion;

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
                    //ViewState["URL_ORIGEN"] = Request.UrlReferrer;
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_EXPERIMENTALES_AMERB;
                }


                ViewState["solicitudSession"] = paginas.solicitudExperimentalesAmerbSession;

                validacionDocumentacion.aplicaExperimentalesAmerb = 1;
                ViewState["validacionDocumentacion"] = validacionDocumentacion;

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
                    //ViewState["URL_ORIGEN"] = Request.UrlReferrer;
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_EXPERIMENTALES_CONCESION;
                }


                ViewState["solicitudSession"] = paginas.solicitudExperimentalesConcesionSession;

                validacionDocumentacion.aplicaExpConcesion = 1;
                ViewState["validacionDocumentacion"] = validacionDocumentacion;

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
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_ECMPO;
                }


                ViewState["solicitudSession"] = paginas.solicitudECMPOSession;

                validacionDocumentacion.aplicaAcuiculturaEcmpo = 1;
                ViewState["validacionDocumentacion"] = validacionDocumentacion;

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
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_COLECTORES_SEMILLA;
                }



                ViewState["solicitudSession"] = paginas.solicitudColectorSession;

                validacionDocumentacion.aplicaColectores = 1;
                ViewState["validacionDocumentacion"] = validacionDocumentacion;

            }
            else if (funciones.retornaModulo().Equals("ModificacionAmerb"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_AMERB;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_AMERB;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_AMERB;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_AMERB;
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
                    //ViewState["URL_ORIGEN"] = Request.UrlReferrer;
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_AMERB;
                }


                ViewState["solicitudSession"] = paginas.solicitudModificacionAmerbSession;

                ViewState["validacionDocumentacion"] = validacionDocumentacion;
            }
            else if (funciones.retornaModulo().Equals("ModificacionCentroAcopio"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
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
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                }


                ViewState["solicitudSession"] = paginas.solicitudModificacionCentroAcopioSession;

                ViewState["validacionDocumentacion"] = validacionDocumentacion;
            }
            else if (funciones.retornaModulo().Equals("ModificacionCentroFaenamiento"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
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
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                }


                ViewState["solicitudSession"] = paginas.solicitudModificacionCentroFaenamientoSession;

                ViewState["validacionDocumentacion"] = validacionDocumentacion;
            }
            else if (funciones.retornaModulo().Equals("ModificacionECMPO"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_ECMPO;
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
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_ECMPO;
                }


                ViewState["solicitudSession"] = paginas.solicitudModificacionECMPOSession;

                ViewState["validacionDocumentacion"] = validacionDocumentacion;
            }

            else
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLMOD;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLMOD;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLMOD;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLMOD;
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
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLMOD;
                }


                ViewState["solicitudSession"] = paginas.solicitudModificacionSession;

                ViewState["validacionDocumentacion"] = validacionDocumentacion;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {

            // PAGE LOAD
            if (!Page.IsPostBack)
            {

                this.setearGruposValidacion();


                //FORMULARIO DE INGRESO
                if (permisosService.tieneAccesoA())
                {
                    PanelFormularioIngreso.Visible = true;
                    UpdatePanelFormularioIngreso.Update();
                }
                else
                {
                    PanelFormularioIngreso.Visible = false;
                    UpdatePanelFormularioIngreso.Update();
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


            if (Request.Params["__EVENTTARGET"] != null)
            {
                if (Request.Params["__EVENTTARGET"] == "ctl00$rightbody$administrarDocumentoComponente$cerrar_agregarGrupoSusp")
                {
                    this.Resultado_change(null, null);
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


                    SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                    if (solicitudConcesion == null)
                    {
                        Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
                    }



                    IdSolicitud.Text = Convert.ToString(solicitudConcesion.idSolConcesion);



                    // Inicializamos el formulario
                    Initialize_Form();

                    GridSalida.DataSource = requerimientoService.ObtieneRequerimientoDeSalidaSinEstado(Convert.ToInt32(Request.QueryString["idRequerimiento"]));
                    GridSalida.DataBind();

                    GridEntrada.DataSource = requerimientoService.ObtieneRequerimientoDeEntrada(Convert.ToInt32(Request.QueryString["idRequerimiento"]));
                    GridEntrada.DataBind();



                    Hashtable camposObligatorios = new Hashtable();
                    ViewState["HashCampos"] = validacionDocumentacionDA.ListaValidacionDocGeneral((ValidacionDocumentacion)ViewState["validacionDocumentacion"]);


                    Datos.Entidades.Usuario.Serializable usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                }
            }
        }



        protected void setearGruposValidacion()
        {

            ValidationSummaryFlujo.ValidationGroup = GrupoFlujo;
            RequiredFieldFlujoDocumental.ValidationGroup = GrupoFlujo;

            ValidationSummaryEntrada.ValidationGroup = GrupoEntrada;
            RequiredFieldValidatorTipoEntrada.ValidationGroup = GrupoEntrada;

            ValidationSummarySalida.ValidationGroup = GrupoSalida;
            RequiredFieldValidatorTipoSalida.ValidationGroup = GrupoSalida;

            ValidationSummaryGrupoRespuestaRequerimiento.ValidationGroup = GrupoRespuestaRequerimiento;
            ValidationSummaryGrupoIngresoSinRequerimiento.ValidationGroup = GrupoIngresoSinRequerimiento;
            ValidationSummaryGrupoRequerimientoConRespuesta.ValidationGroup = GrupoRequerimientoConRespuesta;
            ValidationSummaryGrupoInformativo.ValidationGroup = GrupoInformativo;

            RequiredFieldFlujoDocumentalGrupoDocumento.ValidationGroup = GrupoDocumentoAsociado;
            RequiredFieldTipoSalidaGrupoDocumento.ValidationGroup = GrupoDocumentoAsociado;
            RequiredFieldAmbitoGrupoDocumento.ValidationGroup = GrupoDocumentoAsociado;
            RequiredFieldTipoGrupoDocumento.ValidationGroup = GrupoDocumentoAsociado;
            btnAgregarDocumentoAsociado.ValidationGroup = GrupoDocumentoAsociado;
            ValidationSummaryDocumentoAsociado.ValidationGroup = GrupoDocumentoAsociado;

            ValidationSummaryGrupoSubmitIngreso.ValidationGroup = GrupoSubmitIngreso;

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
            Ambito.SelectedValue = "0";

            Carga_Combobox("Tipo");
            Tipo.SelectedValue = "0";

            Carga_Combobox("TipoDocumento");
            TipoDocumento.SelectedValue = "0";

            Carga_Combobox("Resultado");
            Resultado.SelectedValue = "0";

            //Carga_Combobox("ResultadoSupeditado");
            //ResultadoSupeditado.SelectedValue = "0";

            Carga_Combobox("Destinatario");
            Destinatario.SelectedValue = "0";

            Carga_Combobox("TipoSupeditado");
            TipoSupeditado.SelectedValue = "-1";

            Carga_Combobox("TipoPendiente");
            TipoPendiente.SelectedValue = "-1";


        }


        protected void Carga_Combobox(string combobox)
        {

            switch (combobox)
            {

                case "FlujoDocumental":

                    // Cargamos el combobox: Flujo Documental
                    ValidacionDocumentacion validacionDocumentacion = (ValidacionDocumentacion)ViewState["validacionDocumentacion"];
                    validacionDocumentacion.ambito = new ParametroGenerico(Convert.ToInt32(dbPestana));
                    validacionDocumentacion.seccion = new ParametroGenerico(Convert.ToInt32(dbSeccion));

                    FlujoDocumental.Items.Clear();

                    List<ValidacionDocumentacion> resp = validacionDocumentacionDA.ListarFlujoDocumentalFiltro(validacionDocumentacion);
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
                    TipoEntrada.DataBind();
                    TipoEntrada.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                case "TipoSalida":
                    // Cargamos el combobox: Tipo Salida
                    TipoSalida.Items.Clear();
                    TipoSalida.DataBind();
                    TipoSalida.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

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

                case "Resultado":
                    // Cargamos el combobox: Tipo Resultado
                    Resultado.Items.Clear();
                    Resultado.DataBind();
                    Resultado.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                //case "ResultadoSupeditado":
                //    // Cargamos el combobox: Tipo Resultado Supeditado
                //    ResultadoSupeditado.Items.Clear();
                //    ResultadoSupeditado.DataBind();
                //    ResultadoSupeditado.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                //    break;

                case "Destinatario":
                    // Cargamos el combobox: Destinatario
                    Destinatario.Items.Clear();
                    Destinatario.DataBind();
                    Destinatario.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                case "TipoSupeditado":

                    TipoSupeditado.Items.Clear();
                    TipoSupeditado.DataBind();

                    ParametroGenerico parametroGenericoFiltro = new ParametroGenerico();
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
                    TipoPendiente.DataSource = grupoSuspendidoDA.ListarGrupoSuspendido(0,0,rbEstadosGenerales.VIGENTE,null);
                    TipoPendiente.DataTextField = "nombreGrupoSuspend";
                    TipoPendiente.DataValueField = "idGrupoSuspend";
                    TipoPendiente.DataBind();
                    TipoPendiente.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

            };
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



            msgErroresGrillaSalida.Text = "";
            Content_msgErroresGrillaSalida.Visible = false;
            UpdatePanelErroresSalida.Update();


            msgErroresGrillaEntrada.Text = "";
            Content_msgErroresGrillaEntrada.Visible = false;
            UpdatePanelErroresEntrada.Update();


            idArchivo.Text = "";
            NombreArchivo.Text = "";


            FlujoDocumental.Enabled = true;
            TipoEntrada.Enabled = true;
            TipoSalida.Enabled = true;
            Origen.Enabled = true;
            Destinatario.Enabled = true;
            TipoDocumento.Enabled = true;
            NRequerimiento.Enabled = true;


            Requerimiento requerimiento = null;
            if (tipoFlujo == rbTipo.ENTRADA)
            {
                requerimiento = requerimientoService.ObtenerRespuesta(idRequerimiento);
            }

            if (tipoFlujo == rbTipo.SALIDA)
            {
                requerimiento = requerimientoService.ObtenerRequerimientoSinEstado(idRequerimiento);
            }

            idDocGeneral.Text = Convert.ToString(requerimiento.idRequerimiento);



            try
            {

                FlujoDocumental.SelectedValue = Convert.ToString(requerimiento.flujoDocumental.id);
                FlujoDocumental_change(null, null);
                UpdatePanelFlujoDocumental.Update();

                if (requerimiento.tipoEntrada != null && requerimiento.tipoEntrada.id > 0)
                {
                    TipoEntrada.SelectedValue = Convert.ToString(requerimiento.tipoEntrada.id);
                    TipoEntrada_change(null, null);
                    UpdatePanelTipoEntrada.Update();

                    Origen.SelectedValue = Convert.ToString(requerimiento.origen.id);
                    Origen_Change(null, null);
                    UpdatePanelOrigen.Update();

                }

                if (requerimiento.tipoSalida != null && requerimiento.tipoSalida.id > 0)
                {
                    TipoSalida.SelectedValue = Convert.ToString(requerimiento.tipoSalida.id);
                    TipoSalida_change(null, null);
                    UpdatePanelTipoSalida.Update();

                    Destinatario.SelectedValue = Convert.ToString(requerimiento.destinatario.id);
                    Destinatario_Change(null, null);
                    UpdatePanelDestinatario.Update();

                }


                PanelFormulario.Visible = true;
                UpdatePanelFormulario.Update();



                TipoDocumento.SelectedValue = Convert.ToString(requerimiento.tipoDocumento.id);
                TipoDocumento_change(null, null);
                UpdatePanelTipoDocumento.Update();

                //DOCUMENTO COMPLEMENTARIO
                if (requerimiento.idReqPrincipal > 0) {


                    try
                    {
                        DocumentoPrincipal.SelectedValue = Convert.ToString(requerimiento.idReqPrincipal);
                        DocumentoPrincipal.Enabled = false;
                        UpdatePanelDocumentoPrincipal.Update();
                    }
                    catch (Exception ex) { 
                    
                    }

                }



                if (requerimiento.tipoEntrada != null && requerimiento.tipoEntrada.id == rbTipo.INGRESO_SIN_REQUERIMIENTO)
                {

                    Ambito.SelectedValue = Convert.ToString(requerimiento.ambitoTipo[0].ambito.id);
                    Ambito_change(null, null);
                    UpdatePanelAmbito.Update();


                    Tipo.SelectedValue = Convert.ToString(requerimiento.ambitoTipo[0].tipo.id);
                    Tipo_change(null, null);
                    UpdatePanelTipo.Update();


                    if (requerimiento.ambitoTipo[0].estadoResultadoResp != null && requerimiento.ambitoTipo[0].estadoResultadoResp.id > 0)
                    {

                        try
                        {
                            Resultado.SelectedValue = Convert.ToString(requerimiento.ambitoTipo[0].estadoResultadoResp.id);
                            Resultado_change(null, null);
                            UpdatePanelResultado.Update();
                        }
                        catch (Exception)
                        {
                            
                        }

                        UpdatePanelResultado.Update();
                    }

                    //if (requerimiento.ambitoTipo[0].tipoResultadoSupeditado != null && requerimiento.ambitoTipo[0].tipoResultadoSupeditado.id > 0)
                    //{

                    //    try
                    //    {
                    //        ResultadoSupeditado.SelectedValue = Convert.ToString(requerimiento.ambitoTipo[0].tipoResultadoSupeditado.id);
                    //    }
                    //    catch (Exception)
                    //    {

                    //    }

                    //    UpdatePanelResultadoSupeditado.Update();
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

                if (requerimiento.tipoSalida != null && requerimiento.tipoSalida.id == rbTipo.INFORMATIVO)
                {

                    Ambito.SelectedValue = Convert.ToString(requerimiento.ambitoTipo[0].ambito.id);
                    Ambito_change(null, null);
                    UpdatePanelAmbito.Update();

                    Tipo.SelectedValue = Convert.ToString(requerimiento.ambitoTipo[0].tipo.id);
                    Tipo_change(null, null);
                    UpdatePanelTipo.Update();

                    if (requerimiento.ambitoTipo[0].estadoResultadoResp != null && requerimiento.ambitoTipo[0].estadoResultadoResp.id > 0)
                    {
                        Resultado.SelectedValue = Convert.ToString(requerimiento.ambitoTipo[0].estadoResultadoResp.id);
                        UpdatePanelResultado.Update();
                    }

                    //if (requerimiento.ambitoTipo[0].tipoResultadoSupeditado != null && requerimiento.ambitoTipo[0].tipoResultadoSupeditado.id > 0)
                    //{
                    //    ResultadoSupeditado.SelectedValue = Convert.ToString(requerimiento.ambitoTipo[0].tipoResultadoSupeditado.id);
                    //    UpdatePanelResultadoSupeditado.Update();
                    //}
                }


                if (requerimiento.tipoSalida != null && requerimiento.tipoSalida.id == rbTipo.REQUERIMIENTO_CON_RESPUESTA)
                {
                    GridViewSalidaDocumentoAsociado.DataSource = requerimiento.ambitoTipo;
                    GridViewSalidaDocumentoAsociado.DataBind();
                    ViewState["Documentos_Asociados"] = (List<DocumentoAmbito>)requerimiento.ambitoTipo;
                    UpdatePanelDocumentosAmbito.Update();

                    mostrarCamposOpcionales(requerimiento.ambitoTipo[0]);
                }


                if (requerimiento.tipoEntrada != null && requerimiento.tipoEntrada.id == rbTipo.RESPUESTA_A_UN_REQUERIMIENTO)
                {

                    NRequerimiento.Items.Clear();
                    NRequerimiento.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    DataTable data = requerimientoService.ListarRequerimientosIdSolicitudModificacion(Convert.ToInt32(IdSolicitud.Text), Convert.ToInt32(requerimiento.idReqSalida), Convert.ToInt32(Ambito.SelectedValue));
                    if (data != null)
                    {
                        foreach (DataRow row in data.Rows)
                        {
                            NRequerimiento.Items.Add(new ListItem(Convert.ToString(row["nombreReq"]), Convert.ToString(row["idDocGeneral"])));
                        }

                    }
                    NRequerimiento.DataBind();

                    NRequerimiento.SelectedValue = Convert.ToString(requerimiento.idReqSalida);
                    UpdatePanelNumeroRequerimiento.Update();
                    NRequerimiento_change(null, null);



                }



                if (requerimiento.numero != null && !requerimiento.numero.Trim().Equals(""))
                {
                    Numero.Text = Convert.ToString(requerimiento.numero);
                }

                if (requerimiento.fecha != null && requerimiento.fecha != default(DateTime))
                {
                    Fecha.Text = FechaUtils.formatearFechaSinHora(requerimiento.fecha);
                }

                if (requerimiento.numeroCI > 0)
                {
                    NumeroCI.Text = Convert.ToString(requerimiento.numeroCI);
                }

                if (requerimiento.fechaCI != null && requerimiento.fechaCI != default(DateTime))
                {
                    FechaCI.Text = FechaUtils.formatearFechaSinHora(requerimiento.fechaCI);
                }

                if (requerimiento.nuevaFecha != null && requerimiento.nuevaFecha != default(DateTime))
                {
                    NuevaFecha.Text = FechaUtils.formatearFechaSinHora(requerimiento.nuevaFecha);
                }


                UpdatePanelNumero.Update();
                UpdatePanelFecha.Update();
                UpdatePanelNumeroCI.Update();
                UpdatePanelFechaCI.Update();
                UpdatePanelNuevaFecha.Update();



                if (requerimiento.archivoAdjunto != null && requerimiento.archivoAdjunto.idArchivo > 0)
                {
                    idArchivo.Text = Convert.ToString(requerimiento.archivoAdjunto.idArchivo);
                    NombreArchivo.Text = requerimiento.archivoAdjunto.nombreArchivo;
                    UpdatePanelArchivo.Update();
                }


                FlujoDocumental.Enabled = false;
                TipoEntrada.Enabled = false;
                TipoSalida.Enabled = false;
                Origen.Enabled = false;
                Destinatario.Enabled = false;
                TipoDocumento.Enabled = false;
                NRequerimiento.Enabled = false;
            }
            catch (Exception ex)
            {
                
            }

        }



        //1 = entrada
        //2 = salida
        protected void FlujoDocumental_change(object sender, EventArgs e)
        {

            TipoSalida.Items.Clear();
            TipoSalida.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            TipoSalida.DataBind();

            TipoEntrada.Items.Clear();
            TipoEntrada.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            TipoEntrada.DataBind();


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


            if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.SALIDA)
            {

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
            }


            if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.ENTRADA)
            {

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
            }

        }


        protected void LimpiarPorFlujoDocumental()
        {

            PanelFormulario.Visible = false;
            UpdatePanelFormulario.Update();

            TipoEntrada.SelectedValue = "0";
            TipoSalida.SelectedValue = "0";
            Ambito.SelectedValue = "0";
            Tipo.SelectedValue = "0";
            TipoDocumento.SelectedValue = "0";
            Numero.Text = "";
            Fecha.Text = "";
            NuevaFecha.Text = "";
            NumeroCI.Text = "";
            FechaCI.Text = "";
            Resultado.SelectedValue = "0";
            //ResultadoSupeditado.SelectedValue = "0";
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
            PanelNumero.Visible = false;
            PanelFecha.Visible = false;
            PanelNuevaFecha.Visible = false;
            PanelNumeroRequerimiento.Visible = false;
            PanelNumeroCI.Visible = false;
            PanelFechaCI.Visible = false;
            PanelResultado.Visible = false;
            //PanelResultadoSupeditado.Visible = false;
            PanelDestinatario.Visible = false;
            PanelArchivo.Visible = false;
            PanelListaRequerimientos.Visible = false;
            PanelMensajePlanos14TER.Visible = false;
            PanelMensajePlanosITDACAprueba.Visible = false;
            PanelMensajeResolSSP.Visible = false;
            PanelMensajeResolSSFFAA.Visible = false;


            ListViewEntradaRespuestaRequerimiento_Carga();
            FlujoDocumental.Focus();

        }

        protected void LimpiarPorFlujoDocumental2()
        {

            PanelFormulario.Visible = false;
            UpdatePanelFormulario.Update();

            TipoEntrada.SelectedValue = "0";
            TipoSalida.SelectedValue = "0";
            Ambito.SelectedValue = "0";
            Tipo.SelectedValue = "0";
            TipoDocumento.SelectedValue = "0";
            Numero.Text = "";
            Fecha.Text = "";
            NuevaFecha.Text = "";
            NumeroCI.Text = "";
            FechaCI.Text = "";
            Resultado.SelectedValue = "0";
            //ResultadoSupeditado.SelectedValue = "0";
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
            PanelNumero.Visible = false;
            PanelFecha.Visible = false;
            PanelNuevaFecha.Visible = false;
            PanelNumeroRequerimiento.Visible = false;
            PanelNumeroCI.Visible = false;
            PanelFechaCI.Visible = false;
            PanelResultado.Visible = false;
            //PanelResultadoSupeditado.Visible = false;
            PanelDestinatario.Visible = false;
            PanelArchivo.Visible = false;
            PanelListaRequerimientos.Visible = false;
            PanelMensajePlanos14TER.Visible = false;
            //PanelMensajePlanosITDACAprueba.Visible = false;
            //PanelMensajeResolSSP.Visible = false;
            //PanelMensajeResolSSFFAA.Visible = false;


            ListViewEntradaRespuestaRequerimiento_Carga();
            FlujoDocumental.Focus();

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


        }



        protected void LimpiarPorTipoSalida()
        {

            TipoEntrada.SelectedValue = "0";
            Ambito.SelectedValue = "0";
            Tipo.SelectedValue = "0";
            TipoDocumento.SelectedValue = "0";
            Numero.Text = "";
            Fecha.Text = "";
            NuevaFecha.Text = "";
            NumeroCI.Text = "";
            FechaCI.Text = "";
            Resultado.SelectedValue = "0";
            //ResultadoSupeditado.SelectedValue = "0";
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
            PanelNumero.Visible = false;
            PanelFecha.Visible = false;
            PanelNuevaFecha.Visible = false;
            PanelNumeroRequerimiento.Visible = false;
            PanelNumeroCI.Visible = false;
            PanelFechaCI.Visible = false;
            PanelResultado.Visible = false;
            //PanelResultadoSupeditado.Visible = false;
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
            FlujoDocumental.Focus();

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



        }


        protected void LimpiarPorTipoEntrada()
        {

            TipoSalida.SelectedValue = "0";
            Ambito.SelectedValue = "0";
            Tipo.SelectedValue = "0";
            TipoDocumento.SelectedValue = "0";
            Numero.Text = "";
            Fecha.Text = "";
            NuevaFecha.Text = "";
            NumeroCI.Text = "";
            FechaCI.Text = "";
            Resultado.SelectedValue = "0";
            //ResultadoSupeditado.SelectedValue = "0";
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
            PanelTipoDocumento.Visible = false;
            PanelNumero.Visible = false;
            PanelFecha.Visible = false;
            PanelNuevaFecha.Visible = false;
            PanelNumeroRequerimiento.Visible = false;
            PanelNumeroCI.Visible = false;
            PanelFechaCI.Visible = false;
            PanelResultado.Visible = false;
            //PanelResultadoSupeditado.Visible = false;
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
            FlujoDocumental.Focus();

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
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + Fecha.ClientID, script.ToString(), true);
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
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + NuevaFecha.ClientID, script.ToString(), true);

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
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaCI.ClientID, script.ToString(), true);
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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + Fecha.ClientID, script.ToString(), true);

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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaCI.ClientID, script.ToString(), true);

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
                            var idSeccion = item.FindControl("HiddenSeccionId") as HiddenField;
                            var respuesta = item.FindControl("AmbitoTipoResultado") as DropDownList;

                            documentoAmbito.idDocPestana = Convert.ToInt32(idDocPestana.Value);
                            if (!idDocGeneralResp.Value.Equals(""))
                            {
                                documentoAmbito.idDocGeneralResp = Convert.ToInt32(idDocGeneralResp.Value);
                            }
                            documentoAmbito.tipo = new ParametroGenerico(Convert.ToInt32(idTipo.Value));
                            documentoAmbito.estadoResultadoResp = new ParametroGenerico(Convert.ToInt32(respuesta.SelectedValue));

                            if (!idSeccion.Value.Equals(""))
                            {
                                documentoAmbito.seccion = new ParametroGenerico(Convert.ToInt32(idSeccion.Value));
                            }

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
                //SE NECESITA SABER DE QUE AMBITO ES EL REQUERIMIENTO A RESPONDER (EN LA RESPUESTA NO SE INDICA)
                if (idTipoIO == rbTipo.RESPUESTA_A_UN_REQUERIMIENTO)
                {

                    List<ListViewDataItem> seleccionados = ListViewEntradaRespuestaRequerimiento.GetSelectedDataKeys2("chkSeleccionado");

                    //MAS ARRIBA SE VALIDO QUE HAYA SELECCIONADO AL MENOS 1 
                    if (seleccionados.Count() > 0)
                    {
                        foreach (ListViewDataItem item in (List<ListViewDataItem>)seleccionados)
                        {
                            var idAmbito = item.FindControl("HiddenAmbitoId") as HiddenField;
                            hashIdPestana = (Hashtable)hashIdTipoOrigenDestinatario[Convert.ToInt32(idAmbito.Value)];
                            break;
                        }
                    }

                }
                else if (idTipoIO == rbTipo.REQUERIMIENTO_CON_RESPUESTA)
                {

                    //SE CONSIDERA PARA VALIDACION EL PRIMER ELEMENTO DE LA LISTA
                    List<DocumentoAmbito> List_Documentos_Asociados = (List<DocumentoAmbito>)ViewState["Documentos_Asociados"];

                    if (List_Documentos_Asociados != null)
                    {
                        foreach (DocumentoAmbito documentoAmbito in List_Documentos_Asociados)
                        {
                            if (documentoAmbito.accion == accion.INGRESAR || documentoAmbito.accion == accion.LISTADO || documentoAmbito.accion == accion.MODIFICAR)
                            {
                                hashIdPestana = (Hashtable)hashIdTipoOrigenDestinatario[Convert.ToInt32(documentoAmbito.ambito.id)];
                                break;
                            }
                        }
                    }

                }
                else
                {
                    hashIdPestana = (Hashtable)hashIdTipoOrigenDestinatario[Convert.ToInt32(Ambito.SelectedValue)];
                }
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
                    if (!ArchivoAdjunto.HasFile && idArchivo.Text.Equals(""))
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

                ////RESULTADO (SI HAY ELEMENTOS EN EL CAMPOS RESULTADO, SE DEBE SELECCIONAR ALGUNO)
                //if (ResultadoSupeditado.Items.Count > 1)
                //{
                //    if (Convert.ToInt32(ResultadoSupeditado.SelectedValue) < 1)
                //    {
                //        errores.Add("Seleccione Tipo Supeditado");
                //    }
                //}


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

        }


        protected void TipoDocumento_change(object sender, EventArgs e)
        {


            LimpiarPorTipoDocumento();


            Ambito.Items.Clear();
            Ambito.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            //TIPO (APARECERA EN CASO  QUE HAYAN TIPOS POSIBLES DE SELECCIONAR Y NO SEA UNA RESPUESTA A UN REQUERIMIENTO)
            if (!((Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.ENTRADA && Convert.ToInt32(TipoEntrada.SelectedValue) == rbTipo.RESPUESTA_A_UN_REQUERIMIENTO)))
            {

                if (Convert.ToInt32(TipoDocumento.SelectedValue) > 0)
                {

                    ValidacionDocumentacion validacionDocumentacion = (ValidacionDocumentacion)ViewState["validacionDocumentacion"];

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

                    List<ValidacionDocumentacion> resp = validacionDocumentacionDA.ListarTipoAmbitoFiltro(validacionDocumentacion);


                    if (resp != null)
                    {
                        foreach (ValidacionDocumentacion item in resp)
                        {
                            if (item.subRequerimiento != null)
                            {
                                Ambito.Items.Add(new ListItem(item.ambito.descripcion, Convert.ToString(item.ambito.id)));
                            }
                        }
                    }
                }
            }
            else
            {
                PanelAmbito.Visible = false;
            }
            Ambito.DataBind();






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


            Ambito.SelectedValue = "0";
            Tipo.SelectedValue = "0";
            Numero.Text = "";
            Fecha.Text = "";
            NuevaFecha.Text = "";
            NumeroCI.Text = "";
            NumeroCIMensaje.Text = "";
            FechaCI.Text = "";
            Resultado.SelectedValue = "0";
            //ResultadoSupeditado.SelectedValue = "0";
            NRequerimiento.SelectedValue = "0";

            ViewState["Documentos_Asociados"] = null;
            GridViewSalidaDocumentoAsociado_CargaGrilla();



            PanelAmbito.Visible = false;
            PanelTipo.Visible = false;
            PanelDocumentosAmbito.Visible = false;
            PanelNumero.Visible = false;
            PanelFecha.Visible = false;
            PanelNuevaFecha.Visible = false;
            PanelNumeroRequerimiento.Visible = false;
            PanelNumeroCI.Visible = false;
            PanelFechaCI.Visible = false;
            PanelResultado.Visible = false;
            //PanelResultadoSupeditado.Visible = false;
            PanelArchivo.Visible = false;
            PanelListaRequerimientos.Visible = false;

            ErroresSuperior.Text = "";
            PanelErroresSuperior.Visible = false;
            UpdatePanelErroresSuperior.Update();

            ErroresInferior.Text = "";
            PanelErroresInferior.Visible = false;
            UpdatePanelErroresInferior.Update();

            ListViewEntradaRespuestaRequerimiento_Carga();
            TipoDocumento.Focus();

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


        }


        protected void Tipo_change(object sender, EventArgs e)
        {

            this.controlarCamposPorTema();

            //LOS DOCUMENTOS COMPLEMENTARIOS NO TIENEN RESULTADOS
            if ((Convert.ToInt32(TipoDocumento.SelectedValue) != rbTipo.RESOLUCION_COMPLEMENTARIA && Convert.ToInt32(TipoDocumento.SelectedValue) != rbTipo.INFORME_COMPLEMENTARIO) && Convert.ToInt32(Tipo.SelectedValue) > 0 && !(Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.SALIDA && Convert.ToInt32(TipoSalida.SelectedValue) == rbTipo.REQUERIMIENTO_CON_RESPUESTA))
            {

                int IdTipoUE = 0;

                ValidacionDocumentacion validacionDocumentacion = (ValidacionDocumentacion)ViewState["validacionDocumentacion"];

                if (validacionDocumentacion != null)
                {
                    if (validacionDocumentacion.aplicaAcopio == 1)
                    {
                        IdTipoUE = rbTipo.UNID_ESPACIAL_CENTRO_DE_ACOPIO;
                    }
                    else if (validacionDocumentacion.aplicaFaenamiento == 1)
                    {
                        IdTipoUE = rbTipo.UNID_ESPACIAL_CENTRO_DE_FAENAMIENTO;
                    }
                    else if (validacionDocumentacion.aplicaAmerb == 1)
                    {
                        IdTipoUE = rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB;
                    }
                }


                DataTable data = requerimientoService.ListarPosiblesRespuestasSubRequerimiento(Convert.ToInt32(Tipo.SelectedValue), IdTipoUE);

                if (data != null && data.Rows.Count > 0)
                {

                    Resultado.Items.Clear();
                    Resultado.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    foreach (DataRow row in data.Rows)
                    {
                        Resultado.Items.Add(new ListItem(Convert.ToString(row["nombreEstado"]), Convert.ToString(row["idEstado"])));
                    }


                    Resultado.DataBind();

                    PanelResultado.Visible = true;
                }
                else
                {
                    Resultado.Items.Clear();
                    Resultado.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    Resultado.DataBind();
                    PanelResultado.Visible = false;

                }

            }
            else
            {
                Resultado.Items.Clear();
                Resultado.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                Resultado.DataBind();
                PanelResultado.Visible = false;
            }


            UpdatePanelResultado.Update();

        }


        protected void Resultado_change(object sender, EventArgs e)
        {

            this.controlarCamposPorTema();

            ////LOS DOCUMENTOS COMPLEMENTARIOS NO TIENEN RESULTADOS
            //if ((Convert.ToInt32(TipoDocumento.SelectedValue) != rbTipo.RESOLUCION_COMPLEMENTARIA && Convert.ToInt32(TipoDocumento.SelectedValue) != rbTipo.INFORME_COMPLEMENTARIO) 
            //    && Convert.ToInt32(Tipo.SelectedValue) > 0 && !(Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.SALIDA 
            //    && Convert.ToInt32(TipoSalida.SelectedValue) == rbTipo.REQUERIMIENTO_CON_RESPUESTA)
            //    && Convert.ToInt32(Resultado.SelectedValue) == rbEstadosGenerales.SUPEDITADA)
            //{
            //    List<ParametroGenerico> supeditadasList = requerimientoService.ListarPosiblesRespuestasSupeditadasSubRequerimiento(Convert.ToInt32(Resultado.SelectedValue));

            //    if (supeditadasList != null && supeditadasList.Count > 0)
            //    {

            //        ResultadoSupeditado.Items.Clear();
            //        ResultadoSupeditado.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            //        foreach (ParametroGenerico supeditada in supeditadasList)
            //        {
            //            ResultadoSupeditado.Items.Add(new ListItem(supeditada.descripcion, Convert.ToString(supeditada.id)));
            //        }


            //        ResultadoSupeditado.DataBind();

            //        PanelResultadoSupeditado.Visible = true;
            //    }
            //    else
            //    {
            //        ResultadoSupeditado.Items.Clear();
            //        ResultadoSupeditado.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            //        ResultadoSupeditado.DataBind();
            //        PanelResultadoSupeditado.Visible = false;

            //    }

            //}
            //else
            //{
            //    ResultadoSupeditado.Items.Clear();
            //    ResultadoSupeditado.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            //    ResultadoSupeditado.DataBind();
            //    PanelResultadoSupeditado.Visible = false;
            //}


            //UpdatePanelResultadoSupeditado.Update();
            //this.LimpiarPorTipoResultado();

            // Los subtipos de resultados aplican solo para IT UOT (109)
            int idSubrequerimiento = Convert.ToInt32(Tipo.SelectedItem.Value);

            if (idSubrequerimiento == rbSubRequerimiento.IT_UOT)
            {
                int resultado = Convert.ToInt32(Resultado.SelectedItem.Value);
                if (resultado == rbEstadosGenerales.RECHAZA || resultado == rbEstadosGenerales.PENDIENTE)
                {
                    PanelPendiente.Visible = true;
                    PanelSupeditado.Visible = true;
                    Carga_Combobox("TipoPendiente");
                    Carga_Combobox("TipoSupeditado");
                    UpdatePanelSupeditado.Update();
                    UpdatePanelPendiente.Update();
                    

                }
                else if (resultado == rbEstadosGenerales.SUPEDITADA || resultado == rbEstadosGenerales.RECHAZA || resultado == rbEstadosGenerales.PENDIENTE)
                {
                    PanelSupeditado.Visible = true;
                    PanelPendiente.Visible = false;
                    Carga_Combobox("TipoSupeditado");
                    UpdatePanelSupeditado.Update();
                    UpdatePanelPendiente.Update();
                    
                }
                else
                {
                    PanelSupeditado.Visible = false;
                    PanelPendiente.Visible = false;
                    UpdatePanelSupeditado.Update();
                    UpdatePanelPendiente.Update();
                    
                }
            }
        }

        protected void LimpiarPorTipoResultado()
        {

            TipoPendiente.SelectedValue = "-1";
            TipoSupeditado.SelectedValue = "-1";
            Pert.Text = "";
            Observaciones.Text = "";

            ViewState["AsocGrupoSolicitudList"] = null;
            GridPendientes_CargaGrilla();

            ViewState["DependenciaSupeditadosList"] = null;
            GridSupeditados_CargaGrilla();

            PanelMensajeAsociarPendiente.Visible = false;
            PanelPendiente.Visible = false;
            PanelGrillaPendientes.Visible = false;
            PanelMensajeAsociarSup.Visible = false;
            PanelSupeditado.Visible = false;
            PanelGrillaSupeditados.Visible = false;

            UpdatePanelMensajeAsociarPendiente.Update();
            UpdatePanelPendiente.Update();
            UpdatePanelMensajeAsociarSup.Update();
            UpdatePanelSupeditado.Update();
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

        }



        //GRIDVIEW  (SALIDA - REQUERIMIENTO CON RESPUESTA)
        protected void GridViewSalidaDocumentoAsociado_Guardar(object sender, EventArgs e)
        {


            Page.Validate(GrupoDocumentoAsociado);

            if (Page.IsValid)
            {
                GridViewSalidaDocumentoAsociado_Agregar();
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

            ValidacionDocumentacion validacionDocumentacion = validacionDocumentacionDA.obtenerSeccionporTipo(Convert.ToInt32(TipoSalida.SelectedValue), Convert.ToInt32(Destinatario.SelectedValue), Convert.ToInt32(TipoDocumento.SelectedValue), documentoAmbito.ambito.id, documentoAmbito.tipo.id);

            documentoAmbito.seccion = validacionDocumentacion.seccion;


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

                /* Se debe desplegar mensaje de alerta cuando sea: Carta Sometimiento SEA/RCA (30), la carta al titular mo (31) y Recopilación CPS e Infas (200)  */

                if (documentoAmbito.tipo.id == 30 || documentoAmbito.tipo.id == 31 || documentoAmbito.tipo.id == 200)
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


                //SI TIENEN RESPUESTA, Y LA RESPUESTA ES EL DOCUMENTO QUE ESTOY MODIFICANDO, ENTONCES SE DEBE MARCAR EL CHECK
                HiddenField idRespuesta = (HiddenField)e.Item.FindControl("HiddenIdDocGeneralResp");
                CheckBox check = (CheckBox)e.Item.FindControl("chkSeleccionado");

                if (idRespuesta.Value != null && Convert.ToInt32(idRespuesta.Value) > 0 && Convert.ToInt32(idRespuesta.Value) == Convert.ToInt32(idDocGeneral.Text) && check != null)
                {
                    check.Checked = true;
                    check.Enabled = true;

                }

                //SI TIENEN RESPUESTA, Y LA RESPUESTA NO ES EL DOCUMENTO QUE ESTOY MODIFICANDO, ENTONCES SE DEBE BLOQUEAR EL CHECK
                if (idRespuesta.Value != null && Convert.ToInt32(idRespuesta.Value) > 0 && Convert.ToInt32(idRespuesta.Value) != Convert.ToInt32(idDocGeneral.Text) && check != null)
                {
                    check.Checked = true;
                    check.Enabled = false;
                    respuesta.Enabled = false;
                }



                List<DocumentoAmbito> respu = (List<DocumentoAmbito>)ViewState["Documentos_Asociados_Respuesta"];

                //LOS DOCUMENTOS COMPLEMENTARIOS NO TIENEN RESULTADOS
                if ((Convert.ToInt32(TipoDocumento.SelectedValue) != rbTipo.RESOLUCION_COMPLEMENTARIA && Convert.ToInt32(TipoDocumento.SelectedValue) != rbTipo.INFORME_COMPLEMENTARIO) && respuesta != null && tipo != null)
                {

                    int IdTipoUE = 0;

                    ValidacionDocumentacion validacionDocumentacion = (ValidacionDocumentacion)ViewState["validacionDocumentacion"];

                    if (validacionDocumentacion != null)
                    {
                        if (validacionDocumentacion.aplicaAcopio == 1)
                        {
                            IdTipoUE = rbTipo.UNID_ESPACIAL_CENTRO_DE_ACOPIO;
                        }
                        else if (validacionDocumentacion.aplicaFaenamiento == 1)
                        {
                            IdTipoUE = rbTipo.UNID_ESPACIAL_CENTRO_DE_FAENAMIENTO;
                        }
                        else if (validacionDocumentacion.aplicaAmerb == 1)
                        {
                            IdTipoUE = rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB;
                        }
                    }


                    DataTable data = requerimientoService.ListarPosiblesRespuestasSubRequerimiento(Convert.ToInt32(tipo.Value), IdTipoUE);


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





        //GRILLA SALIDA
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



                String rowspan = ((Label)e.Row.FindControl("hidden4")).Text;

                //PRIMERA FILA CON DATOS
                if (count == 0)
                {
                    e.Row.Cells[2].Visible = true;
                    e.Row.Cells[2].RowSpan = Convert.ToInt32(rowspan);

                    e.Row.Cells[9].Visible = true;
                    e.Row.Cells[9].RowSpan = Convert.ToInt32(rowspan);

                    e.Row.BackColor = colorPlanilla.COLOR_CELESTE;


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

                        e.Row.Cells[9].Visible = true;
                        e.Row.Cells[9].RowSpan = Convert.ToInt32(rowspan);


                        if (previousRow.BackColor == colorPlanilla.COLOR_CELESTE)
                        {
                            e.Row.BackColor = colorPlanilla.COLOR_BLANCO;
                        }
                        else
                        {
                            e.Row.BackColor = colorPlanilla.COLOR_CELESTE;
                        }

                    }
                    else
                    {
                        e.Row.Cells[2].RowSpan = 0;
                        e.Row.Cells[2].Visible = false;

                        e.Row.Cells[9].RowSpan = 0;
                        e.Row.Cells[9].Visible = false;

                        e.Row.BackColor = previousRow.BackColor;
                    }
                }


                e.Row.Cells[0].Visible = false; // Invisibiling idDocGeneral Header Cell
                e.Row.Cells[1].Visible = false; // Invisibiling idPestana Header Cell


                //Modificar
                String idVisacionMasiva = ((Label)e.Row.FindControl("hiddenVisacionMasiva")).Text;

                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_modificar != null)
                {
                    if (idVisacionMasiva != null && !idVisacionMasiva.Equals("True"))
                    {
                        boton_modificar.Visible = true;
                    }

                };

                //Desgarcar
                String idArchivoBinSC = DataBinder.Eval(e.Row.DataItem, "idArchivoBinSC").ToString();
                ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                if (boton_descargar != null && idArchivoBinSC != null && !idArchivoBinSC.Equals("") && Convert.ToInt32(idArchivoBinSC) > 0)
                {
                    boton_descargar.Visible = true;
                };


                String idEstadoVigencia = ((Label)e.Row.FindControl("hidden3")).Text;

                //No Vigente
                ImageButton boton_noVigente = (ImageButton)e.Row.FindControl("gNoVigente");
                if (boton_noVigente != null)
                {
                    boton_noVigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea dejar no vigente este documento?')");
                    if (Convert.ToInt32(idEstadoVigencia) == rbEstadosGenerales.VIGENTE)
                    {
                        boton_noVigente.Visible = true;
                    }
                };


                //Vigente
                ImageButton boton_vigente = (ImageButton)e.Row.FindControl("gVigente");
                if (boton_vigente != null)
                {
                    boton_vigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea dejar vigente este documento?')");
                    if (Convert.ToInt32(idEstadoVigencia) == rbEstadosGenerales.NO_VIGENTE)
                    {
                        boton_vigente.Visible = true;
                    }

                };



                //Borrar
                ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
                if (boton_borrar != null)
                {
                    boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar este documento?')");
                    boton_borrar.Visible = true;
                };



            };
        }



        //GRILLA SALIDA
        protected void GridSalida_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int idTipoFlujoDocumental = 0;


            string[] arg = new string[3];
            arg = e.CommandArgument.ToString().Split(';');

            int idRequerimiento = Convert.ToInt32(arg[0]);
            int idPestana = Convert.ToInt32(arg[1]);

            int idArchivo = 0;
            if (!arg[2].Equals(""))
            {
                idArchivo = Convert.ToInt32(arg[2]);
            }



            switch (e.CommandName)
            {
                case "Modificar":
                    RequerimientoCarga(idRequerimiento, rbTipo.SALIDA);
                    break;

                case "Descargar":

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

                case "Eliminar":

                    idTipoFlujoDocumental = requerimientoService.ObtieneFlujoDocumentoGeneral(idRequerimiento);

                    if (idTipoFlujoDocumental == rbTipo.ENTRADA)
                    {
                        Requerimiento requerimiento = requerimientoService.ObtenerRespuesta(idRequerimiento);
                        List<String> errores = administrarDocumentoValidacion.validarEliminacionDeRequerimiento(requerimiento);
                        if (errores.Count == 0)
                        {
                            requerimientoService.EliminarRespuesta(idRequerimiento, idPestana, requerimiento.solicitud.idSolConcesion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                        }
                        else
                        {

                            foreach (String error in errores)
                            {
                                msgErroresGrillaSalida.Text = msgErroresGrillaSalida.Text + error + "<br/>";
                            }
                            Content_msgErroresGrillaSalida.Visible = true;
                            UpdatePanelErroresSalida.Update();

                        }
                    }
                    if (idTipoFlujoDocumental == rbTipo.SALIDA)
                    {
                        Requerimiento requerimiento = requerimientoService.ObtenerRequerimientoSinEstado(idRequerimiento);
                        List<String> errores = administrarDocumentoValidacion.validarEliminacionDeRequerimiento(requerimiento);
                        if (errores.Count == 0)
                        {
                            requerimientoService.EliminarRequerimiento(idRequerimiento, idPestana, requerimiento.solicitud.idSolConcesion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                        }
                        else
                        {

                            foreach (String error in errores)
                            {
                                msgErroresGrillaSalida.Text = msgErroresGrillaSalida.Text + error + "<br/>";
                            }
                            Content_msgErroresGrillaSalida.Visible = true;
                            UpdatePanelErroresSalida.Update();

                        }
                    }

                    GridSalida.DataSource = requerimientoService.ObtieneRequerimientoDeSalidaSinEstado(Convert.ToInt32(Request.QueryString["idRequerimiento"]));
                    GridSalida.DataBind();
                    break;

                case "NoVigente":

                    idTipoFlujoDocumental = requerimientoService.ObtieneFlujoDocumentoGeneral(idRequerimiento);

                    if (idTipoFlujoDocumental == rbTipo.ENTRADA)
                    {
                        Requerimiento requerimiento = requerimientoService.ObtenerRespuesta(idRequerimiento);
                        List<String> errores = administrarDocumentoValidacion.validarNoVigenteDeRequerimiento(requerimiento);
                        if (errores.Count == 0)
                        {
                            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];
                            requerimientoService.ActualizarRespuestaEstado(idRequerimiento, Convert.ToInt32(idPestana), rbEstadosGenerales.NO_VIGENTE, usuario_logeado.id_usuario);
                        }
                        else
                        {

                            foreach (String error in errores)
                            {
                                msgErroresGrillaSalida.Text = msgErroresGrillaSalida.Text + error + "<br/>";
                            }
                            Content_msgErroresGrillaSalida.Visible = true;
                            UpdatePanelErroresSalida.Update();

                        }
                    }
                    if (idTipoFlujoDocumental == rbTipo.SALIDA)
                    {
                        Requerimiento requerimiento = requerimientoService.ObtenerRequerimientoSinEstado(idRequerimiento);
                        List<String> errores = administrarDocumentoValidacion.validarNoVigenteDeRequerimiento(requerimiento);
                        if (errores.Count == 0)
                        {
                            requerimientoService.ActualizarRequerimientoEstado(idRequerimiento, Convert.ToInt32(idPestana), rbEstadosGenerales.NO_VIGENTE, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                        }
                        else
                        {

                            foreach (String error in errores)
                            {
                                msgErroresGrillaSalida.Text = msgErroresGrillaSalida.Text + error + "<br/>";
                            }
                            Content_msgErroresGrillaSalida.Visible = true;
                            UpdatePanelErroresSalida.Update();

                        }
                    }


                    GridSalida.DataSource = requerimientoService.ObtieneRequerimientoDeSalidaSinEstado(Convert.ToInt32(Request.QueryString["idRequerimiento"]));
                    GridSalida.DataBind();

                    GridEntrada.DataSource = requerimientoService.ObtieneRequerimientoDeEntrada(Convert.ToInt32(Request.QueryString["idRequerimiento"]));
                    GridEntrada.DataBind();

                    break;


                case "Vigente":

                    idTipoFlujoDocumental = requerimientoService.ObtieneFlujoDocumentoGeneral(idRequerimiento);

                    if (idTipoFlujoDocumental == rbTipo.ENTRADA)
                    {
                        Requerimiento requerimiento = requerimientoService.ObtenerRespuesta(idRequerimiento);
                        List<String> errores = administrarDocumentoValidacion.validarNoVigenteDeRequerimiento(requerimiento);
                        if (errores.Count == 0)
                        {
                            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];
                            requerimientoService.ActualizarRespuestaEstado(idRequerimiento, Convert.ToInt32(idPestana), rbEstadosGenerales.VIGENTE, usuario_logeado.id_usuario);
                        }
                        else
                        {

                            foreach (String error in errores)
                            {
                                msgErroresGrillaSalida.Text = msgErroresGrillaSalida.Text + error + "<br/>";
                            }
                            Content_msgErroresGrillaSalida.Visible = true;
                            UpdatePanelErroresSalida.Update();

                        }
                    }
                    if (idTipoFlujoDocumental == rbTipo.SALIDA)
                    {
                        Requerimiento requerimiento = requerimientoService.ObtenerRequerimientoSinEstado(idRequerimiento);
                        List<String> errores = administrarDocumentoValidacion.validarNoVigenteDeRequerimiento(requerimiento);
                        if (errores.Count == 0)
                        {
                            requerimientoService.ActualizarRequerimientoEstado(idRequerimiento, Convert.ToInt32(idPestana), rbEstadosGenerales.VIGENTE, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                        }
                        else
                        {

                            foreach (String error in errores)
                            {
                                msgErroresGrillaSalida.Text = msgErroresGrillaSalida.Text + error + "<br/>";
                            }
                            Content_msgErroresGrillaSalida.Visible = true;
                            UpdatePanelErroresSalida.Update();

                        }
                    }


                    GridSalida.DataSource = requerimientoService.ObtieneRequerimientoDeSalidaSinEstado(Convert.ToInt32(Request.QueryString["idRequerimiento"]));
                    GridSalida.DataBind();

                    GridEntrada.DataSource = requerimientoService.ObtieneRequerimientoDeEntrada(Convert.ToInt32(Request.QueryString["idRequerimiento"]));
                    GridEntrada.DataBind();


                    break;



            };
        }



        //GRILLA SALIDA
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

            e.Row.Cells[0].Visible = false; // Invisibiling idEstadoVigencia
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



                String rowspan = ((Label)e.Row.FindControl("hiddenEntradaRowspan")).Text;

                //PRIMERA FILA CON DATOS
                if (count == 0)
                {
                    e.Row.Cells[2].Visible = true;
                    e.Row.Cells[2].RowSpan = Convert.ToInt32(rowspan);

                    e.Row.Cells[13].Visible = true;
                    e.Row.Cells[13].RowSpan = Convert.ToInt32(rowspan);

                    e.Row.BackColor = colorPlanilla.COLOR_CELESTE;


                }
                else if (count > 0)
                {

                    GridViewRow previousRow = GridRequerimiento.Rows[e.Row.RowIndex - 1];

                    String idDocGeneralAnterior = ((Label)previousRow.FindControl("hiddenEntradaIdDocGeneral")).Text;
                    String idDocGeneralRespAnterior = ((Label)previousRow.FindControl("hiddenEntradaIdDocGeneralResp")).Text;
                    String idPestanaAnterior = ((Label)previousRow.FindControl("hiddenIdPestana")).Text;

                    String idDocGeneral = ((Label)e.Row.FindControl("hiddenEntradaIdDocGeneral")).Text;
                    String idDocGeneralResp = ((Label)e.Row.FindControl("hiddenEntradaIdDocGeneralResp")).Text;
                    String idPestana = ((Label)e.Row.FindControl("hiddenIdPestana")).Text;



                    if (!idDocGeneralAnterior.Equals(idDocGeneral) || !idPestanaAnterior.Equals(idPestana) || !idDocGeneralRespAnterior.Equals(idDocGeneralResp))
                    {

                        e.Row.Cells[2].Visible = true;
                        e.Row.Cells[2].RowSpan = Convert.ToInt32(rowspan);

                        e.Row.Cells[13].Visible = true;
                        e.Row.Cells[13].RowSpan = Convert.ToInt32(rowspan);


                        if (previousRow.BackColor == colorPlanilla.COLOR_CELESTE)
                        {
                            e.Row.BackColor = colorPlanilla.COLOR_BLANCO;
                        }
                        else
                        {
                            e.Row.BackColor = colorPlanilla.COLOR_CELESTE;
                        }

                    }
                    else
                    {
                        e.Row.Cells[2].RowSpan = 0;
                        e.Row.Cells[2].Visible = false;

                        e.Row.Cells[13].RowSpan = 0;
                        e.Row.Cells[13].Visible = false;

                        e.Row.BackColor = previousRow.BackColor;
                    }
                }


                e.Row.Cells[0].Visible = false; // Invisibiling idDocGeneral Header Cell
                e.Row.Cells[1].Visible = false; // Invisibiling idPestana Header Cell


                //Modificar
                String idVisacionMasiva = ((Label)e.Row.FindControl("hiddenVisacionMasiva")).Text;

                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_modificar != null)
                {
                    if (idVisacionMasiva != null && !idVisacionMasiva.Equals("True"))
                    {
                        boton_modificar.Visible = true;
                    }
                };

                //Desgarcar
                String idArchivoBinSC = DataBinder.Eval(e.Row.DataItem, "idArchivoBinSC").ToString();
                ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                if (boton_descargar != null && idArchivoBinSC != null && !idArchivoBinSC.Equals("") && Convert.ToInt32(idArchivoBinSC) > 0)
                {
                    boton_descargar.Visible = true;
                };



                String idEstadoVigencia = ((Label)e.Row.FindControl("hidden3")).Text;

                //No Vigente
                ImageButton boton_noVigente = (ImageButton)e.Row.FindControl("gNoVigente");
                if (boton_noVigente != null)
                {
                    boton_noVigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea dejar no vigente este documento?')");
                    if (Convert.ToInt32(idEstadoVigencia) == rbEstadosGenerales.VIGENTE)
                    {
                        boton_noVigente.Visible = true;
                    }
                };


                //Vigente
                ImageButton boton_vigente = (ImageButton)e.Row.FindControl("gVigente");
                if (boton_vigente != null)
                {
                    boton_vigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea dejar vigente este documento?')");
                    if (Convert.ToInt32(idEstadoVigencia) == rbEstadosGenerales.NO_VIGENTE)
                    {
                        boton_vigente.Visible = true;
                    }

                };



                //Borrar
                ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
                if (boton_borrar != null)
                {
                    boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar este documento?')");
                    boton_borrar.Visible = true;
                };

            };
        }



        //GRILLA ENTRADA
        protected void GridEntrada_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            int idTipoFlujoDocumental = 0;


            string[] arg = new string[3];
            arg = e.CommandArgument.ToString().Split(';');

            int idRequerimiento = Convert.ToInt32(arg[0]);
            int idPestana = Convert.ToInt32(arg[1]);

            int idArchivo = 0;
            if (!arg[2].Equals(""))
            {
                idArchivo = Convert.ToInt32(arg[2]);
            }



            switch (e.CommandName)
            {
                case "Modificar":

                    RequerimientoCarga(idRequerimiento, rbTipo.ENTRADA);
                    break;


                case "Descargar":

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


                case "Eliminar":

                    idTipoFlujoDocumental = requerimientoService.ObtieneFlujoDocumentoGeneral(idRequerimiento);

                    if (idTipoFlujoDocumental == rbTipo.ENTRADA)
                    {
                        Requerimiento requerimiento = requerimientoService.ObtenerRespuesta(idRequerimiento);
                        List<String> errores = administrarDocumentoValidacion.validarEliminacionDeRequerimiento(requerimiento);
                        if (errores.Count == 0)
                        {
                            requerimientoService.EliminarRespuesta(idRequerimiento, idPestana, requerimiento.solicitud.idSolConcesion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                        }
                        else
                        {

                            foreach (String error in errores)
                            {
                                msgErroresGrillaEntrada.Text = msgErroresGrillaEntrada.Text + error + "<br/>";
                            }
                            Content_msgErroresGrillaEntrada.Visible = true;
                            UpdatePanelErroresEntrada.Update();

                        }
                    }
                    if (idTipoFlujoDocumental == rbTipo.SALIDA)
                    {
                        Requerimiento requerimiento = requerimientoService.ObtenerRequerimientoSinEstado(idRequerimiento);
                        List<String> errores = administrarDocumentoValidacion.validarEliminacionDeRequerimiento(requerimiento);
                        if (errores.Count == 0)
                        {
                            requerimientoService.EliminarRequerimiento(idRequerimiento, idPestana, requerimiento.solicitud.idSolConcesion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                        }
                        else
                        {

                            foreach (String error in errores)
                            {
                                msgErroresGrillaEntrada.Text = msgErroresGrillaEntrada.Text + error + "<br/>";
                            }
                            Content_msgErroresGrillaEntrada.Visible = true;
                            UpdatePanelErroresEntrada.Update();

                        }
                    }

                    GridEntrada.DataSource = requerimientoService.ObtieneRequerimientoDeEntrada(Convert.ToInt32(Request.QueryString["idRequerimiento"]));
                    GridEntrada.DataBind();
                    break;

                case "NoVigente":

                    idTipoFlujoDocumental = requerimientoService.ObtieneFlujoDocumentoGeneral(idRequerimiento);

                    if (idTipoFlujoDocumental == rbTipo.ENTRADA)
                    {
                        Requerimiento requerimiento = requerimientoService.ObtenerRespuesta(idRequerimiento);
                        List<String> errores = administrarDocumentoValidacion.validarNoVigenteDeRequerimiento(requerimiento);
                        if (errores.Count == 0)
                        {
                            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];
                            requerimientoService.ActualizarRespuestaEstado(idRequerimiento, Convert.ToInt32(idPestana), rbEstadosGenerales.NO_VIGENTE, usuario_logeado.id_usuario);
                        }
                        else
                        {

                            foreach (String error in errores)
                            {
                                msgErroresGrillaEntrada.Text = msgErroresGrillaEntrada.Text + error + "<br/>";
                            }
                            Content_msgErroresGrillaEntrada.Visible = true;
                            UpdatePanelErroresEntrada.Update();

                        }
                    }
                    if (idTipoFlujoDocumental == rbTipo.SALIDA)
                    {
                        Requerimiento requerimiento = requerimientoService.ObtenerRequerimientoSinEstado(idRequerimiento);
                        List<String> errores = administrarDocumentoValidacion.validarNoVigenteDeRequerimiento(requerimiento);
                        if (errores.Count == 0)
                        {
                            requerimientoService.ActualizarRequerimientoEstado(idRequerimiento, Convert.ToInt32(idPestana), rbEstadosGenerales.NO_VIGENTE, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                        }
                        else
                        {

                            foreach (String error in errores)
                            {
                                msgErroresGrillaEntrada.Text = msgErroresGrillaEntrada.Text + error + "<br/>";
                            }
                            Content_msgErroresGrillaEntrada.Visible = true;
                            UpdatePanelErroresEntrada.Update();

                        }
                    }

                    GridEntrada.DataSource = requerimientoService.ObtieneRequerimientoDeEntrada(Convert.ToInt32(Request.QueryString["idRequerimiento"]));
                    GridEntrada.DataBind();
                    break;


                case "Vigente":


                    idTipoFlujoDocumental = requerimientoService.ObtieneFlujoDocumentoGeneral(idRequerimiento);

                    if (idTipoFlujoDocumental == rbTipo.ENTRADA)
                    {
                        Requerimiento requerimiento = requerimientoService.ObtenerRespuesta(idRequerimiento);
                        List<String> errores = administrarDocumentoValidacion.validarNoVigenteDeRequerimiento(requerimiento);
                        if (errores.Count == 0)
                        {
                            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];
                            requerimientoService.ActualizarRespuestaEstado(idRequerimiento, Convert.ToInt32(idPestana), rbEstadosGenerales.VIGENTE, usuario_logeado.id_usuario);
                        }
                        else
                        {

                            foreach (String error in errores)
                            {
                                msgErroresGrillaEntrada.Text = msgErroresGrillaEntrada.Text + error + "<br/>";
                            }
                            Content_msgErroresGrillaEntrada.Visible = true;
                            UpdatePanelErroresEntrada.Update();

                        }
                    }
                    if (idTipoFlujoDocumental == rbTipo.SALIDA)
                    {
                        Requerimiento requerimiento = requerimientoService.ObtenerRequerimientoSinEstado(idRequerimiento);
                        List<String> errores = administrarDocumentoValidacion.validarNoVigenteDeRequerimiento(requerimiento);
                        if (errores.Count == 0)
                        {
                            requerimientoService.ActualizarRequerimientoEstado(idRequerimiento, Convert.ToInt32(idPestana), rbEstadosGenerales.VIGENTE, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                        }
                        else
                        {

                            foreach (String error in errores)
                            {
                                msgErroresGrillaEntrada.Text = msgErroresGrillaEntrada.Text + error + "<br/>";
                            }
                            Content_msgErroresGrillaEntrada.Visible = true;
                            UpdatePanelErroresEntrada.Update();

                        }
                    }

                    GridEntrada.DataSource = requerimientoService.ObtieneRequerimientoDeEntrada(Convert.ToInt32(Request.QueryString["idRequerimiento"]));
                    GridEntrada.DataBind();
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
                HeaderCell.ColumnSpan = 12;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridEntrada.Controls[0].Controls.AddAt(0, HeaderRow);



            }

            e.Row.Cells[0].Visible = false; // Invisibiling idEstadoVigencia
        }








        protected void Limpiar_Click(object sender, EventArgs e)
        {
            FlujoDocumental.SelectedValue = "0";
            LimpiarPorFlujoDocumental();
        }


        protected void Guardar_Click(object sender, EventArgs e)
        {

            DocumentoAsociadoValidacion validacion = new DocumentoAsociadoValidacion();
            Page.Validate(GrupoFlujo);

            if (Page.IsValid)
            {

                Requerimiento requerimiento = new Requerimiento();
                requerimiento.solicitud = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                //SE CAMBIO LA SOLICITUD EN SESSION, PERO LA PAGINA NO SE HA RECARGADO
                if (requerimiento.solicitud.idSolConcesion != Convert.ToInt32(IdSolicitud.Text))
                {
                    Page.Validators.Add(new ValidationError(GrupoSubmitIngreso, "La solicitud ha cambiado, por favor recargue la página"));
                }

                if (idDocGeneral.Text.Equals(""))
                {
                    Page.Validators.Add(new ValidationError(GrupoSubmitIngreso, "Debe seleccionar documento a modificar desde la grilla de documentos"));
                }

                requerimiento.flujoDocumental = new ParametroGenerico(Convert.ToInt32(FlujoDocumental.SelectedValue));


                if (requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
                {

                    Page.Validate(GrupoEntrada);

                    if (Page.IsValid)
                    {
                        requerimiento.tipoEntrada = new ParametroGenerico(Convert.ToInt32(TipoEntrada.SelectedValue));
                    }


                    if (requerimiento.tipoEntrada != null && requerimiento.tipoEntrada.id == rbTipo.RESPUESTA_A_UN_REQUERIMIENTO)
                    {


                        List<String> errores = this.validarIngresoRequerimiento(rbTipo.RESPUESTA_A_UN_REQUERIMIENTO, requerimiento);

                        foreach (String error in errores)
                        {
                            Page.Validators.Add(new ValidationError(GrupoRespuestaRequerimiento, error));
                        }
                    }



                    if (requerimiento.tipoEntrada != null && requerimiento.tipoEntrada.id == rbTipo.INGRESO_SIN_REQUERIMIENTO)
                    {
                        List<String> errores = this.validarIngresoRequerimiento(rbTipo.INGRESO_SIN_REQUERIMIENTO, requerimiento);

                        foreach (String error in errores)
                        {
                            Page.Validators.Add(new ValidationError(GrupoIngresoSinRequerimiento, error));
                        }
                    }

                }


                if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
                {
                    Page.Validate(GrupoSalida);

                    if (Page.IsValid)
                    {
                        requerimiento.tipoSalida = new ParametroGenerico(Convert.ToInt32(TipoSalida.SelectedValue));
                    }



                    if (requerimiento.tipoSalida != null && requerimiento.tipoSalida.id == rbTipo.INFORMATIVO)
                    {
                        List<String> errores = this.validarIngresoRequerimiento(rbTipo.INFORMATIVO, requerimiento);

                        foreach (String error in errores)
                        {
                            Page.Validators.Add(new ValidationError(GrupoInformativo, error));
                        }

                    }



                    if (requerimiento.tipoSalida != null && requerimiento.tipoSalida.id == rbTipo.REQUERIMIENTO_CON_RESPUESTA)
                    {
                        List<String> errores = this.validarIngresoRequerimiento(rbTipo.REQUERIMIENTO_CON_RESPUESTA, requerimiento);

                        foreach (String error in errores)
                        {
                            Page.Validators.Add(new ValidationError(GrupoRequerimientoConRespuesta, error));
                        }

                    }
                }

                int resultadoInforme = Convert.ToInt32(Resultado.SelectedItem.Value);

                if (resultadoInforme == rbEstadosGenerales.PENDIENTE || resultadoInforme == rbEstadosGenerales.RECHAZA)
                {
                    List<AsocGrupoSolicitud> asocGrupoSolicitudList = (List<AsocGrupoSolicitud>)ViewState["AsocGrupoSolicitudList"];
                    if (asocGrupoSolicitudList != null && asocGrupoSolicitudList.Count > 0)
                    {
                        requerimiento.asocGrupoSolicitudList = asocGrupoSolicitudList;
                    }
                    else
                    {
                        if (resultadoInforme == rbEstadosGenerales.PENDIENTE)
                        {
                            int numeroPendiente = 0;
                            foreach (AsocGrupoSolicitud asocGrupoSolicitud in asocGrupoSolicitudList) {
                                if (asocGrupoSolicitud.accion != accion.INGRESAR || asocGrupoSolicitud.accion != accion.LISTADO || asocGrupoSolicitud.accion != accion.MODIFICAR)
                                {
                                    numeroPendiente++;

                                }
                            }

                            if (numeroPendiente <= 0)
                                Page.Validators.Add(new ValidationError(GrupoSubmitIngreso, "Ingrese al menos un Tipo Pendiente"));
                        }
                    }
                }



                /* Asociar Tipo Supeditado si se requiere */
                if (resultadoInforme == rbEstadosGenerales.PENDIENTE || resultadoInforme == rbEstadosGenerales.SUPEDITADA)
                {
                    List<DependenciaSupeditados> dependenciaSupeditadosList = (List<DependenciaSupeditados>)ViewState["DependenciaSupeditadosList"];
                    if (dependenciaSupeditadosList != null && dependenciaSupeditadosList.Count > 0)
                    {
                        requerimiento.dependenciaSupeditadosList = dependenciaSupeditadosList;

                    }
                    else
                    {
                        if (resultadoInforme == rbEstadosGenerales.SUPEDITADA)
                        {
                            int numeroSupeditado = 0;
                            foreach (DependenciaSupeditados dependenciaSupeditados in dependenciaSupeditadosList)
                            {
                                if (dependenciaSupeditados.accion != accion.INGRESAR || dependenciaSupeditados.accion != accion.LISTADO || dependenciaSupeditados.accion != accion.MODIFICAR)
                                {
                                    numeroSupeditado++;

                                }
                            }

                            if (numeroSupeditado <= 0)
                                Page.Validators.Add(new ValidationError(GrupoSubmitIngreso, "Ingrese al menos un Tipo de Supeditado"));
                        }
                    }
                }

                if (Page.IsValid)
                {

                    requerimiento.idRequerimiento = Convert.ToInt32(idDocGeneral.Text);

                    if (Convert.ToInt32(TipoDocumento.SelectedValue) > 0)
                    {
                        requerimiento.tipoDocumento = new ParametroGenerico(Convert.ToInt32(TipoDocumento.SelectedValue));
                    }

                    if (!Numero.Text.Trim().Equals(""))
                    {
                        requerimiento.numero = Convert.ToString(Numero.Text);
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


                    if (!DocumentoPrincipal.SelectedValue.Trim().Equals("") &&  Convert.ToInt32(DocumentoPrincipal.SelectedValue) > 0)
                    {
                        requerimiento.idReqPrincipal = Convert.ToInt32(DocumentoPrincipal.SelectedValue);
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

                        ////RESULTADO
                        //if (Convert.ToInt32(ResultadoSupeditado.SelectedValue) > 0)
                        //{
                        //    foreach (DocumentoAmbito doc in requerimiento.ambitoTipo)
                        //    {
                        //        doc.tipoResultadoSupeditado = new ParametroGenerico(Convert.ToInt32(ResultadoSupeditado.SelectedValue));
                        //        break;
                        //    }
                        //}

                        //SECCION
                        foreach (DocumentoAmbito doc in requerimiento.ambitoTipo)
                        {
                            ValidacionDocumentacion validacionDocumentacion = null;

                            if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
                            {
                                validacionDocumentacion = validacionDocumentacionDA.obtenerSeccionporTipo(requerimiento.tipoSalida.id, requerimiento.destinatario.id, requerimiento.tipoDocumento.id, doc.ambito.id, doc.tipo.id);

                            }
                            if (requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
                            {
                                validacionDocumentacion = validacionDocumentacionDA.obtenerSeccionporTipo(requerimiento.tipoEntrada.id, requerimiento.origen.id, requerimiento.tipoDocumento.id, doc.ambito.id, doc.tipo.id);
                            }

                            doc.seccion = validacionDocumentacion.seccion;
                            break;

                        }



                        Requerimiento requerimientoAux = null;
                        if (Convert.ToUInt32(FlujoDocumental.SelectedValue) == rbTipo.ENTRADA)
                        {
                            requerimientoAux = requerimientoService.ObtenerRespuesta(requerimiento.idRequerimiento);
                        }

                        if (Convert.ToUInt32(FlujoDocumental.SelectedValue) == rbTipo.SALIDA)
                        {
                            requerimientoAux = requerimientoService.ObtenerRequerimientoSinEstado(requerimiento.idRequerimiento);
                        }

                        //RECUPERAR LA INFORMACION QUE ESTABA GUARDADA EN LA BASE DE DATOS, PARA SABER SI HA MODIFICADO EL AMBITO (EN ESTOS CASOS NO SE TRABAJO CON UNA LISTA DE SUBREQUERIMIENTOS)
                        List<DocumentoAmbito> listAux = (List<DocumentoAmbito>)requerimientoAux.ambitoTipo;
                        requerimiento.ambitoTipo[0].ambitoAntiguo = listAux[0].ambitoAntiguo;
                        requerimiento.ambitoTipo[0].tipoAntiguo = listAux[0].tipoAntiguo;
                        requerimiento.ambitoTipo[0].idDocPestana = listAux[0].idDocPestana;

                    }




                    //CAMBIO EL ARCHIVO
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
                    else
                    {

                        //NO HIZO NADA CON EL ARCHIVO, SE DEBE MANTENER
                        if (!idArchivo.Text.Equals("") && Convert.ToInt32(idArchivo.Text) > 0)
                        {
                            ArchivoBinario archivoBinario = new ArchivoBinario();
                            archivoBinario.idArchivo = Convert.ToInt32(idArchivo.Text);

                            requerimiento.archivoAdjunto = archivoBinario;

                        }

                        //BORRE EL ARCHIVO
                        if (idArchivo.Text.Equals(""))
                        {
                            ArchivoBinario archivoBinario = new ArchivoBinario();
                            archivoBinario.idArchivo = 0;
                            requerimiento.archivoAdjunto = archivoBinario;
                        }

                    }

                    List<String> errores = ingresarDocumentoValidacion.validaIngresoRequerimiento(requerimiento);

                    /* Sólo sí IT DAC Aprueba se debe realizará la validación */
                    if (Convert.ToInt32(Tipo.SelectedItem.Value) == rbSubRequerimiento.INFORME_TECNICO && Convert.ToInt32(Resultado.SelectedValue) == rbEstadosGenerales.APRUEBA)
                    {
                        bool tieneDocumentosAprueba = solicitudDA.cumpleValidacionIngresoITDAC_Aprueba(Convert.ToInt32(IdSolicitud.Text));
                        if (!tieneDocumentosAprueba)
                        {
                            PanelMensajePlanosITDACAprueba.Visible = true;
                            UpdatePanelMensajeITDACAprueba.Update();
                        }
                    }
                    else
                    {
                        /* Sólo sí Resolución SSP Aprueba se debe realizará la validación */
                        if (Convert.ToInt32(Tipo.SelectedItem.Value) == rbSubRequerimiento.RESOLUCION_SSP && Convert.ToInt32(Resultado.SelectedValue) == rbEstadosGenerales.APRUEBA)
                        {
                            bool tieneITDACAprueba = solicitudDA.cumpleValidacionIngresoSSP_Aprueba(Convert.ToInt32(IdSolicitud.Text));
                            if (!tieneITDACAprueba)
                            {
                                PanelMensajeResolSSP.Visible = true;
                                UpdatePanelMensajeResolSSP.Update();
                            }
                        }
                        else
                        {
                            /* Sólo sí Resolución SSFFAA Aprueba se debe realizará la validación */
                            if (Convert.ToInt32(Tipo.SelectedItem.Value) == rbSubRequerimiento.RESOLUCION_SSFFAA && Convert.ToInt32(Resultado.SelectedValue) == rbEstadosGenerales.APRUEBA)
                            {
                                bool tieneSSPAprueba = solicitudDA.cumpleValidacionIngresoSSFFAA_Aprueba(Convert.ToInt32(IdSolicitud.Text));
                                if (!tieneSSPAprueba)
                                {
                                    PanelMensajeResolSSFFAA.Visible = true;
                                    UpdatePanelMensajeResolSSFFAA.Update();
                                }
                            }
                        }
                    }

                    if (errores.Count == 0)
                    {

                        usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                        bool resp = requerimientoService.actualizarRequerimiento(requerimiento, usuario_logeado.id_usuario);


                        if (resp)
                        {

                            GridSalida.DataSource = requerimientoService.ObtieneRequerimientoDeSalidaSinEstado(Convert.ToInt32(Request.QueryString["idRequerimiento"]));
                            GridSalida.DataBind();

                            GridEntrada.DataSource = requerimientoService.ObtieneRequerimientoDeEntrada(Convert.ToInt32(Request.QueryString["idRequerimiento"]));
                            GridEntrada.DataBind();


                            ErroresInferior.Text = "Se ha actualizado exitosamente el documento";
                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();
                            this.LimpiarPorFlujoDocumental2();
                            
                            //RECARGANDO LA INFORMACION DE LA SOLICITUD, POR SI CAMBIO EL ESTADO
                            UpdatePanel UpdatePanelInformacionSolictud = informacionSolicitud.Instance.UpdatePanelInfo;
                            informacionSolicitud.Instance.RecargarInformacion();
                            UpdatePanelInformacionSolictud.Update();

                        }
                        else
                        {
                            ErroresInferior.Text = "Ha ocurrido un error al actualizar el documento";
                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();

                        }
                    }
                    else
                    {
                        foreach (String error in errores)
                        {
                            Page.Validators.Add(new ValidationError(GrupoSubmitIngreso, error));
                        }
                    }



                }


            }
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(ViewState["URL_ORIGEN"].ToString());

        }

        /* Métodos para el funcionamiento de Tipo Supeditado y Grupo Suspendido */
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

        private void ocultarTodosLosFormularios()
        {
            PanelResultado.Visible = false;
            ocultarFormularioSupeditadoYPendiente();
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


                // Borrar
                //SOLO RELACIONES VIGENTES PUEDEN SER ELIMINADAS
                HiddenField idEstadoVigencia = (HiddenField)e.Row.FindControl("idEstadoVigencia");
                if (idEstadoVigencia != null && !idEstadoVigencia.Value.Equals("") && Convert.ToInt32(idEstadoVigencia.Value) == rbEstadosGenerales.VIGENTE)
                {
                    ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                    if (boton_eliminar != null)
                    {
                        boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar el tipo de pendiente?')");
                        boton_eliminar.Visible = true;
                    };
                }
            };
        }

        protected void GridPendientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            PanelMensajeAsociarPendiente.Visible = false;

            int id = 0;
            switch (e.CommandName)
            {
                case "Eliminar":
                    id = Convert.ToInt32(e.CommandArgument);
                    DeletePendientes(id);
                    GridPendientes.EditIndex = -1;
                    break;
            };
        }

        private void DeletePendientes(int idAsocGrupoSolicitud)
        {
            List<AsocGrupoSolicitud> asocGrupoSolicitudList = (List<AsocGrupoSolicitud>)ViewState["AsocGrupoSolicitudList"];

            foreach (AsocGrupoSolicitud asocGrupoSolicitud in asocGrupoSolicitudList)
            {

                if (asocGrupoSolicitud != null && asocGrupoSolicitud.index == idAsocGrupoSolicitud)
                {

                    List<String> listaErroresGrupo = new List<string>(); // Valida que la eliminación sea correcta y no deje inconsistencias.

                    if (listaErroresGrupo != null && listaErroresGrupo.Count <= 0)
                    {
                        if (asocGrupoSolicitud.accion == accion.INGRESAR)
                        {
                            asocGrupoSolicitud.accion = accion.IGNORAR;
                            GridPendientes.Rows[idAsocGrupoSolicitud].Attributes["style"] = "display:none";
                        }
                        if (asocGrupoSolicitud.accion == accion.LISTADO)
                        {
                            asocGrupoSolicitud.accion = accion.ELIMINAR;
                            GridPendientes.Rows[idAsocGrupoSolicitud].Attributes["style"] = "display:none";
                        }
                    }
                    else
                    {
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

            ViewState["AsocGrupoSolicitudList"] = (List<AsocGrupoSolicitud>)asocGrupoSolicitudList;
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
            PanelMensajeAsociarSup.Visible = false;

            int id = 0;
            switch (e.CommandName)
            {
                case "Eliminar":
                    id = Convert.ToInt32(e.CommandArgument);
                    DeleteSupeditados(id);
                    GridSupeditados.EditIndex = -1;
                    break;
            };
        }

        private void DeleteSupeditados(int idDepSupeditado)
        {
            List<DependenciaSupeditados> dependenciaSupeditadosList = (List<DependenciaSupeditados>)ViewState["DependenciaSupeditadosList"];

            foreach (DependenciaSupeditados dependenciaSupeditados in dependenciaSupeditadosList)
            {
                if (dependenciaSupeditados != null && dependenciaSupeditados.index == idDepSupeditado)
                {
                    List<String> listaErroresSupeditado = new List<string>(); // Valida que la eliminación sea correcta y no deje inconsistencias.

                    if (listaErroresSupeditado != null && listaErroresSupeditado.Count <= 0)
                    {
                        if (dependenciaSupeditados.accion == accion.INGRESAR)
                        {
                            dependenciaSupeditados.accion = accion.IGNORAR;
                            GridSupeditados.Rows[idDepSupeditado].Attributes["style"] = "display:none";
                        }
                        if (dependenciaSupeditados.accion == accion.LISTADO)
                        {
                            dependenciaSupeditados.accion = accion.ELIMINAR;
                            GridSupeditados.Rows[idDepSupeditado].Attributes["style"] = "display:none";
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

            if (dependenciaSupeditadosList == null)
            {
                dependenciaSupeditadosList = new List<DependenciaSupeditados>();
            }

            //if (Convert.ToInt32(Resultado.SelectedItem.Value) == rbEstadosGenerales.SUPEDITADA)
            //{
                if (Convert.ToInt32(TipoSupeditado.SelectedItem.Value) < 1)
                {
                    Page.Validators.Add(new ValidationError("grupo1", "Seleccione Tipo Supeditado."));
                }
            //}

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

                if (listaErroresDependenciaSupeditados != null && listaErroresDependenciaSupeditados.Count <= 0)
                {
                    dependenciaSupeditadosList.Add(dependenciaSupeditados);

                    GridSupeditados.DataSource = dependenciaSupeditadosList;
                    GridSupeditados.DataBind();

                    PanelGrillaSupeditados.Visible = true;
                    UpdatePanelGrillaSupeditados.Update();

                    ViewState["DependenciaSupeditadosList"] = (List<DependenciaSupeditados>)dependenciaSupeditadosList;
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
            }

            Panel1.Visible = true;
            UpdatePanelMensajesValidaciones.Update();

        }

        protected void GuardarPendiente_Click(object sender, ImageClickEventArgs e)
        {
            List<AsocGrupoSolicitud> asocGrupoSolicitudList = (List<AsocGrupoSolicitud>)ViewState["AsocGrupoSolicitudList"];
            if (asocGrupoSolicitudList == null)
            {
                asocGrupoSolicitudList = new List<AsocGrupoSolicitud>();
            }

            //if (Convert.ToInt32(Resultado.SelectedItem.Value) == rbEstadosGenerales.PENDIENTE)
            //{
                if (Convert.ToInt32(TipoPendiente.SelectedItem.Value) < 1)
                {
                    Page.Validators.Add(new ValidationError("grupo1", "Seleccione Tipo Pendiente."));
                }
            //}

            if (Page.IsValid)
            {

                AsocGrupoSolicitud asocGrupoSolicitud = new AsocGrupoSolicitud();
                asocGrupoSolicitud.solicitudConcesion = new SolicitudConcesion();
                asocGrupoSolicitud.solicitudConcesion.idConcesion = Convert.ToInt32(IdSolicitud.Text);

                asocGrupoSolicitud.grupoSuspendido = new GrupoSuspendidos();
                asocGrupoSolicitud.grupoSuspendido.idGrupoSuspend = Convert.ToInt32(TipoPendiente.SelectedItem.Value);
                asocGrupoSolicitud.grupoSuspendido.nombreGrupoSuspend = TipoPendiente.SelectedItem.Text;

                asocGrupoSolicitud.estadoVigencia = new ParametroGenerico();
                asocGrupoSolicitud.estadoVigencia.id = rbEstadosGenerales.VIGENTE;

                List<String> listaErroresPendientes = ingresarDocumentoValidacion.validaIngresoTipoPendiente(asocGrupoSolicitud, asocGrupoSolicitudList);

                if (listaErroresPendientes != null && listaErroresPendientes.Count <= 0)
                {
                    asocGrupoSolicitudList.Add(asocGrupoSolicitud);

                    GridPendientes.DataSource = asocGrupoSolicitudList;
                    GridPendientes.DataBind();

                    PanelGrillaPendientes.Visible = true;
                    UpdatePanelGrillaPendientes.Update();

                    ViewState["AsocGrupoSolicitudList"] = (List<AsocGrupoSolicitud>)asocGrupoSolicitudList;
                }
                else
                {
                    foreach (String error in listaErroresPendientes)
                    {
                        Page.Validators.Add(new ValidationError("grupo1", error));
                    }

                }

                TipoPendiente.SelectedValue = "-1";
            }
            Panel1.Visible = true;
            UpdatePanelMensajesValidaciones.Update();

        }

        /* Fin Métodos para el funcionamiento de Tipo Supeditado y Grupo Suspendido */
    }
}