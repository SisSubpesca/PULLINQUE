using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.unidadEspacial;
using LogicaNegocio.cl.subpesca.rb.common;
using System.IO;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes.unidadEspacial;
using Datos.Entidades;
using System.Collections;
using Validaciones.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Utilidades;
using SubPesca.Solicitudes.Registrar;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.modificacion;
using Datos.Entidades.Relocalizacion;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;
using System.Web.UI.HtmlControls;
using LogicaNegocio.cl.subpesca.rb.servicios.concesiones;

namespace SubPesca.Solicitudes.Relocalizacion
{
    public partial class unidadEspacialRelocalizacion : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        SolicitudDA solicitudDA = new SolicitudDA();
        UnidadEspacialDA unidadEspacialDA = new UnidadEspacialDA();
        CapitaniaDePuertoDA capitaniaDePuertoDA = new CapitaniaDePuertoDA();
        TipoDA tipoDa = new TipoDA();
        UnidadEspacialValidacion unidadEspacialValidacion = new UnidadEspacialValidacion();
        RelocalizacionService relocalizacionService = new RelocalizacionService();
        SolicitudConcesionService solicitudConcesionService = new SolicitudConcesionService();
        UnidadEspacialService unidadEspacialService = new UnidadEspacialService();
        PermisosService permisosService = new PermisosService();
        ConcesionService concesionService = new ConcesionService();

        EnviarCorreo enviarCorreo = new EnviarCorreo();




        protected void setearModulo()
        {
            ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSession;
            ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_RELOCALIZACION;


            SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

            if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA)
            {
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.UNIDAD_ESPACIAL_RELOCALIZACION_CREA };
            }
            if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA)
            {
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.UNIDAD_ESPACIAL_RELOCALIZACION_FUSIONA };
            }
            if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO)
            {
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.UNIDAD_ESPACIAL_RELOCALIZACION_SECTOR_CERO };
            }


        }


        protected void Page_Init(object sender, System.EventArgs e)
        {
            HtmlGenericControl scriptInclude = new HtmlGenericControl();

            scriptInclude = (HtmlGenericControl)Page.Header.FindControl("admin_reportes.js");
            if (scriptInclude == null)
            {
                scriptInclude = new HtmlGenericControl("script");
                scriptInclude.Attributes["type"] = "text/javascript";
                scriptInclude.Attributes["src"] = ResolveClientUrl("~/js/admin/admin_reportes.js");
                scriptInclude.ID = "admin_reportes.js";
                Page.Header.Controls.Add(scriptInclude);
            };
            scriptInclude = (HtmlGenericControl)Page.Header.FindControl("jscal2.js");
            if (scriptInclude == null)
            {
                scriptInclude = new HtmlGenericControl("script");
                scriptInclude.Attributes["type"] = "text/javascript";
                scriptInclude.Attributes["src"] = ResolveClientUrl("~/js/jquery/calendar/jscal2.js");
                scriptInclude.ID = "jscal2.js";
                Page.Header.Controls.Add(scriptInclude);
            };
            scriptInclude = (HtmlGenericControl)Page.Header.FindControl("es.js");
            if (scriptInclude == null)
            {
                scriptInclude = new HtmlGenericControl("script");
                scriptInclude.Attributes["type"] = "text/javascript";
                scriptInclude.Attributes["src"] = ResolveClientUrl("~/js/jquery/calendar/lang/es.js");
                scriptInclude.ID = "es.js";
                Page.Header.Controls.Add(scriptInclude);
            };
            scriptInclude = (HtmlGenericControl)Page.Header.FindControl("funciones.js");
            if (scriptInclude == null)
            {
                scriptInclude = new HtmlGenericControl("script");
                scriptInclude.Attributes["type"] = "text/javascript";
                scriptInclude.Attributes["src"] = ResolveClientUrl("~/js/funciones.js");
                scriptInclude.ID = "funciones.js";
                Page.Header.Controls.Add(scriptInclude);
            };

            string script = "invoca_calendarios(\"unidadEspacialRelocalizacion\")" ;
            ScriptManager.RegisterStartupScript(Page,Page.GetType(), "scriptCalentario", script.ToString(), true);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                string Lang = "es-CL";//set your culture here
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Lang);
                

                setearModulo();

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (solicitudConcesion == null || usuario_logeado == null)
                {
                    Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
                }



                //BOTON DE INGRESO O MODIFICACION
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {
                    CodigoCentro.Enabled = true;
                    NumeroDiarioOficial.Enabled = true;
                    FechaRecepcion.Enabled = true;
                    //CapitaniaPuerto.Enabled = true;
                    //ImagenFecha1.Visible = true;
                    PanelCalendario.Visible = true;

                    PanelBotonLimpiar.Visible = true;
                    PanelBotonGuardar.Visible = true;
                    PanelBotonRelocalizacion.Visible = true;
                }
                else
                {
                    CodigoCentro.Enabled = false;
                    NumeroDiarioOficial.Enabled = false;
                    FechaRecepcion.Enabled = false;
                    //CapitaniaPuerto.Enabled = false;

                    //ImagenFecha1.Visible = false;
                    PanelCalendario.Visible = false;

                    PanelBotonLimpiar.Visible = false;
                    PanelBotonGuardar.Visible = false;
                    PanelBotonRelocalizacion.Visible = false;
                }


                
                
                // Inicializamos el formulario
                Initialize_Form();
                VerificarCodigoCentro(null, null);

            }
        }



        protected void Initialize_Form()
        {
            SolicitudConcesion solicitudRelocalizacion = (SolicitudConcesion)Session[paginas.solicitudRelocalizacionSession];

            if (solicitudRelocalizacion != null && solicitudRelocalizacion.idSolConcesion > 0)
            {

                IdSolicitud.Value = Convert.ToString(solicitudRelocalizacion.idSolConcesion);

                // Cargamos los combobox
                Initialize_Comboboxs();

                Initialize_Formulario(solicitudRelocalizacion);


                //SECTOR
                DetalleSector aDetalleSector = relocalizacionService.obtenerDetalleSector_Solicitud(solicitudRelocalizacion.idSolConcesion);
                ViewState["aDetalleSector"] = aDetalleSector;


                if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA) {
                    CodigoCentro.ReadOnly = false;
                    NumeroDiarioOficial.ReadOnly = false;
                    FechaRecepcion.ReadOnly = false;
                    //TRCapitaniaPuerto.Visible = true;
                    TRDiarioOficial.Visible = true;

                    titulo.Text = "Relocalizar Concesión";
                    textoBoton.Text = "Guardar Unidad Espacial";
                }

                if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO)
                {
                    UnidadEspacial unidadEspacialRel = unidadEspacialDA.ObtieneUnidadEspacialRel(solicitudRelocalizacion.idSolConcesion,0);

                    if (unidadEspacialRel == null)
                    {
                        unidadEspacialRel = new UnidadEspacial();
                    }

                    CodigoCentro.Text = Convert.ToString(aDetalleSector.origenes[0].concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro);
                    CodigoCentro.ReadOnly = true;

                    if (unidadEspacialRel.numeroDiarioOficial > 0)
                    {
                        NumeroDiarioOficial.Text = Convert.ToString(unidadEspacialRel.numeroDiarioOficial);
                    }
                    NumeroDiarioOficial.ReadOnly = false;

                    if (unidadEspacialRel.fechaDiarioOficial != null && !unidadEspacialRel.fechaDiarioOficial.Equals(""))
                    {
                        FechaRecepcion.Text = FechaUtils.formatearFecha(unidadEspacialRel.fechaDiarioOficial);
                    }
                    FechaRecepcion.ReadOnly = false;

                    //if (unidadEspacialRel.capitaniaDePuerto != null && unidadEspacialRel.capitaniaDePuerto.idCapitaDePuerto > 0)
                    //{
                    //    CapitaniaPuerto.SelectedValue = Convert.ToString(unidadEspacialRel.capitaniaDePuerto.idCapitaDePuerto);
                    //    CapitaniaPuerto.Enabled = false;
                    //}
                    //TRCapitaniaPuerto.Visible = true;

                    TRDiarioOficial.Visible = true;

                    titulo.Text = "Relocalizar Concesión";
                    textoBoton.Text = "Guardar Unidad Espacial";
                }

                if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA)
                {
                    UnidadEspacial unidadEspacialRel = unidadEspacialDA.ObtieneUnidadEspacialRel(solicitudRelocalizacion.idSolConcesion, 0);

                    if (unidadEspacialRel == null)
                    {
                        unidadEspacialRel = new UnidadEspacial();
                    }

                    CodigoCentro.Text = Convert.ToString(aDetalleSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro);
                    CodigoCentro.ReadOnly = true;

                    if (unidadEspacialRel.numeroDiarioOficial > 0)
                    {
                        NumeroDiarioOficial.Text = Convert.ToString(unidadEspacialRel.numeroDiarioOficial);
                    }

                    NumeroDiarioOficial.ReadOnly = false;

                    if (unidadEspacialRel.fechaDiarioOficial != null && !unidadEspacialRel.fechaDiarioOficial.Equals(""))
                    {
                        FechaRecepcion.Text = FechaUtils.formatearFecha(unidadEspacialRel.fechaDiarioOficial);
                    }
                    FechaRecepcion.ReadOnly = false;

                    //if (unidadEspacialRel.capitaniaDePuerto != null && unidadEspacialRel.capitaniaDePuerto.idCapitaDePuerto > 0)
                    //{
                    //    CapitaniaPuerto.SelectedValue = Convert.ToString(unidadEspacialRel.capitaniaDePuerto.idCapitaDePuerto);
                    //}

                    //TRCapitaniaPuerto.Visible = true;

                    TRDiarioOficial.Visible = true;

                    titulo.Text = "Relocalizar Concesión";
                    textoBoton.Text = "Guardar Unidad Espacial";
                }


                if (solicitudConcesionService.aplicaBotonCreaModUnidadEspacial(solicitudRelocalizacion.idSolConcesion))
                {

                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], this.usuario_logeado, solicitudRelocalizacion, rbAccion.EDITAR))
                    {
                        PanelBotonRelocalizacion.Visible = true;
                    }
                }
                else {
                    PanelBotonRelocalizacion.Visible = false;
                }

                UnidadEspacial unidadEspacial = unidadEspacialService.ObtieneUnidadEspacialRel(solicitudRelocalizacion.idSolConcesion, 0);

                if (unidadEspacial == null)
                {
                    unidadEspacial = new UnidadEspacial();
                    unidadEspacial.idSolicitud = aDetalleSector.idSolConcesion;
                }
                else {

                    if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA && unidadEspacial.centrosDeCultivo != null)
                    {
                        CodigoCentro.Text = Convert.ToString(unidadEspacial.centrosDeCultivo.codigoCentro);
                    }


                    NumeroDiarioOficial.Text = Convert.ToString(unidadEspacial.numeroDiarioOficial);
                    FechaRecepcion.Text = FechaUtils.formatearFecha(unidadEspacial.fechaDiarioOficial);
                    //CapitaniaPuerto.SelectedValue = Convert.ToString(unidadEspacial.capitaniaDePuerto.idCapitaDePuerto);
                }


                ViewState["unidadEspacial"] = unidadEspacial;
                
            }
            else
            {
                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
            }
        }

        private void Initialize_Formulario(SolicitudConcesion sectorRelocalizacion)
        {
            if (sectorRelocalizacion != null && sectorRelocalizacion.idSolConcesion > 0)
            {
                IdSolicitud.Value = Convert.ToString(sectorRelocalizacion.idSolConcesion);

                UpdatePanelUnidadEspacialRelocalizacion.Update();
            }
            else
            {
                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
            }

        }

        protected void Initialize_Comboboxs()
        {

            //Carga_Combobox("CapitaniaPuerto");
            //CapitaniaPuerto.SelectedValue = "0";
           
        }

        private void Carga_Combobox(string combobox)
        {
            switch (combobox)
            {

                //case "CapitaniaPuerto":
                //    // Cargamos el combobox: CapitaniaPuerto
                //    CapitaniaPuerto.Items.Clear();
                //    CapitaniaPuerto.DataSource = capitaniaDePuertoDA.obtenerCapitaniaDePuerto(0);
                //    CapitaniaPuerto.DataTextField = "CapitaniaPuerto";
                //    CapitaniaPuerto.DataValueField = "IdCapitaniaPuerto";
                //    CapitaniaPuerto.DataBind();
                //    CapitaniaPuerto.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                //    break;
             
            }
        }





        protected void GuardarUnidadEspacial_Click(object sender, ImageClickEventArgs e)
        {

            UnidadEspacial unidadEspacial = (UnidadEspacial)ViewState["unidadEspacial"];
            unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();

            if (!CodigoCentro.Text.Trim().Equals(""))
            {
                unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToString(CodigoCentro.Text);
            }

            if (NumeroDiarioOficial.Text != null && !NumeroDiarioOficial.Text.Equals(""))
            {
                unidadEspacial.numeroDiarioOficial = Convert.ToInt32(NumeroDiarioOficial.Text);
            }

            if (FechaRecepcion.Text != null && !FechaRecepcion.Text.Equals("")){
                unidadEspacial.fechaDiarioOficial = Convert.ToDateTime(FechaRecepcion.Text);
            }

            //unidadEspacial.capitaniaDePuerto = new CapitaniaDePuerto();
            //unidadEspacial.capitaniaDePuerto.idCapitaDePuerto = Convert.ToInt32(CapitaniaPuerto.SelectedValue);

            SolicitudConcesion solicitudRelocalizacion = (SolicitudConcesion)Session[paginas.solicitudRelocalizacionSession];
            DetalleSector aDetalleSector = (DetalleSector)ViewState["aDetalleSector"];
            List<String> listaErroresUnidadEspacial = unidadEspacialValidacion.validaUnidadEspacialRelocalizacion(unidadEspacial, aDetalleSector.tramiteRel.idTramiteRel, aDetalleSector.tipoRelocalizacion.id);

            if (listaErroresUnidadEspacial != null && listaErroresUnidadEspacial.Count <= 0)
            {

                //SE GUARDA LA UNIDAD ESPACIAL 
                bool resp = unidadEspacialService.guardaUnidadEspacialRelocalizacion(unidadEspacial, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                if (resp)
                {
                    

                    msgGrillaGral_1.Text = "Se ha guardado la unidad espacial exitosamente.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                    UpdatePanelMensajesSuperior.Update();
                }
                else
                {
                    msgGrillaGral_1.Text = "Ha ocurrido un error al intentar guardar la unidad espacial.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                    UpdatePanelMensajesSuperior.Update();
                }
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            }
            else
            {
                foreach (String error in listaErroresUnidadEspacial)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }

                UpdatePanelMensajeErrores.Update();
            }
        }




        protected void RelocalizarConcesion_Click(object sender, EventArgs e)
        {

            UnidadEspacial unidadEspacial = (UnidadEspacial)ViewState["unidadEspacial"];

            unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();

            if (CodigoCentro.Text != null && !CodigoCentro.Text.Equals(""))
            {
                unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToString(CodigoCentro.Text);
            }

            if (NumeroDiarioOficial.Text != null && !NumeroDiarioOficial.Text.Equals(""))
            {
                unidadEspacial.numeroDiarioOficial = Convert.ToInt32(NumeroDiarioOficial.Text);
            }

            if (FechaRecepcion.Text != null && !FechaRecepcion.Text.Equals(""))
            {
                unidadEspacial.fechaDiarioOficial = Convert.ToDateTime(FechaRecepcion.Text);
            }
            //unidadEspacial.capitaniaDePuerto = new CapitaniaDePuerto();
            //unidadEspacial.capitaniaDePuerto.idCapitaDePuerto = Convert.ToInt32(CapitaniaPuerto.SelectedValue);

            SolicitudConcesion solicitudRelocalizacion = (SolicitudConcesion)Session[paginas.solicitudRelocalizacionSession];
            DetalleSector aDetalleSector = (DetalleSector)ViewState["aDetalleSector"];
            List<String> listaErroresUnidadEspacial = unidadEspacialValidacion.validaRelocalizacion(unidadEspacial, aDetalleSector.tramiteRel.idTramiteRel, aDetalleSector.tipoRelocalizacion.id);

            if (listaErroresUnidadEspacial != null && listaErroresUnidadEspacial.Count <= 0)
            {

                //SE GUARDA LA UNIDAD ESPACIAL Y SE REALIZA LA RELOCALIZACION
                bool resp = unidadEspacialService.relocalizarConcesion(unidadEspacial, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                if (resp)
                {

                    msgGrillaGral_1.Text = "Se ha relocalizado la concesión de acuicultura exitosamente.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                    UpdatePanelMensajesSuperior.Update();


                    PanelBotonRelocalizacion.Visible = false;

                    try
                    {
                        enviarCorreo.alertaRelocalizacionConcesion(solicitudRelocalizacion);
                    
                    }catch(Exception){
                    
                    }
                }
                else
                {
                    msgGrillaGral_1.Text = "Ha ocurrido un error al intentar relocalizar la concesión de acuilcultura.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                    UpdatePanelMensajesSuperior.Update();
                }
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            }
            else
            {
                foreach (String error in listaErroresUnidadEspacial)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }

                UpdatePanelMensajeErrores.Update();
            }
        }

        protected void LimpiarConcesion_Click(object sender, ImageClickEventArgs e)
        {
            NumeroDiarioOficial.Text = "";
            FechaRecepcion.Text = "";
            //CapitaniaPuerto.SelectedValue = "-1";

        }




        protected void VerificarCodigoCentro(object sender, EventArgs e)
        {

            PanelMensajeCodigoCentro.Visible = false;

            //INDICARLE QUE EL CODIGO DE CENTRO YA EXISTE 
            if (!CodigoCentro.Text.Trim().Equals("")) {

                //SECTOR
                SolicitudConcesion solicitudRelocalizacion = (SolicitudConcesion)Session[paginas.solicitudRelocalizacionSession];
                DetalleSector aDetalleSector = relocalizacionService.obtenerDetalleSector_Solicitud(solicitudRelocalizacion.idSolConcesion);

                if (aDetalleSector != null && aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA)
                {
                    //VERIFICAR SI SE INGRESO UN CODIGO DE CENTRO DE UNA UNIDAD ESPACIAL QUE EXISTE
                    SolicitudConcesion solicitudOriginal = concesionService.ObtieneConcesionExistente(CodigoCentro.Text.Trim(), rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);
                    if (solicitudOriginal != null && solicitudOriginal.idSolConcesion > 0)  //ES UNA UNIDAD ESPACIAL
                    {
                        PanelMensajeCodigoCentro.Visible = true;
                    }
                }
            }

        }
        
    }
}