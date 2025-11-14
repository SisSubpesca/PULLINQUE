using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using Datos.Entidades;
using Datos.Contantes;
using Datos.Utilidades;
using SubPesca.Utilidades;
using Validaciones.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.common;

namespace SubPesca.Solicitudes.ECMPO
{
    public partial class datosCentroECMPO : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        UnidadEspacialValidacion unidadEspacialValidacion = new UnidadEspacialValidacion();
        InformacionAdicionalSolicitudesService informacionAdicionalSolicitudesService = new InformacionAdicionalSolicitudesService();
        InicioSolicitudConcesionValidacion inicioSolicitudConcesionValidacion = new InicioSolicitudConcesionValidacion();
        PermisosService permisosService = new PermisosService();

        DataExternaDA dataExternaDA = new DataExternaDA();

        protected void setearModulo()
        {

            ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_ECMPO;
            ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_ECMPO;
            ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_ECMPO;
            ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_ECMPO;
            ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_ECMPO;
            ViewState["solicitudSession"] = paginas.solicitudECMPOSession;

            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.FICHA_ECMPO };

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            String script = @"<script type='text/javascript'>nuevaFuncion();</script>";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptAutocompletar", script.ToString(), false);

            if ((Request.Params["__EVENTTARGET"] != null) && (Request.Params["__EVENTARGUMENT"] != null))
            {
                if ((Request.Params["__EVENTTARGET"] == this.Ecmpo.ClientID) && (Request.Params["__EVENTARGUMENT"] == "onchange"))
                {
                    this.Ecmpo_TextChanged(null, null);

                }
            }

            if (!Page.IsPostBack)
            {
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
                    SuperficieSectorCultivo.ReadOnly = false;
                    PorcentajeSectorCultivo.ReadOnly = false;
                    PanelBotonGuardar.Visible = true;

                    Ecmpo.ReadOnly = false;
                    PaneGuardarBoton.Visible = true;
                }
                else
                {
                    SuperficieSectorCultivo.ReadOnly = true;
                    PorcentajeSectorCultivo.ReadOnly = true;
                    PanelBotonGuardar.Visible = false;

                    Ecmpo.ReadOnly = true;
                    PaneGuardarBoton.Visible = false;
                }
                
                // Inicializamos el formulario
                Initialize_Form();

                PanelNumPert.Visible = true;
                UpdatePanelNumPert.Update();

            }
        }

        
        protected void Initialize_Form()
        {
            SolicitudConcesion solicitudAux = (SolicitudConcesion)Session[paginas.solicitudECMPOSession];
            if (solicitudAux != null && solicitudAux.idSolConcesion > 0)
            {

                IdSolicitud.Value = Convert.ToString(solicitudAux.idSolConcesion);
                NumPert.Text = solicitudAux.numPert;
                FechaIngresoTramite.Text = FechaUtils.formatearFecha(solicitudAux.fechaIngresoTramite);
                FechaRecepcion.Text = FechaUtils.formatearFecha(solicitudAux.fechaRecepcion);


                //ESTA INFORMACIÓN ES LA QUE SE INGRESA EN LA FICHA PARTICULAR DE CADA UNIDAD ESPACIAL
                
                DetalleDatosSolicitud detalleDatosSolicitud = informacionAdicionalSolicitudesService.ObtieneDetalleDatosSolicitud(solicitudAux.idSolConcesion);
                if (detalleDatosSolicitud != null)
                {

                    ViewState["DatosFicha"] = detalleDatosSolicitud;

                    SuperficieSectorCultivo.Text = Convert.ToString(detalleDatosSolicitud.superficieSectorAmerb);
                    PorcentajeSectorCultivo.Text = Convert.ToString(detalleDatosSolicitud.porcentSectorAmerb);

                    DatosSolicitudUE datosSolicitudUE = informacionAdicionalSolicitudesService.ObtieneDatosSolicitudUENew(solicitudAux.idSolConcesion);

                    if (datosSolicitudUE.ecmpoSSP != null && datosSolicitudUE.ecmpoSSP.codigo > 0)
                    {
                        IdEcmpo.Value = Convert.ToString(datosSolicitudUE.ecmpoSSP.codigo);
                        Ecmpo.Text = datosSolicitudUE.ecmpoSSP.descripcion;
                        this.Ecmpo_TextChanged(null, null);
                    }
                }
                
            }
            else
            {
                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
            }

        }






        protected void GuardarDatos_Click(object sender, ImageClickEventArgs e)
        {


            DetalleDatosSolicitud detalleDatosSolicitud = (DetalleDatosSolicitud)ViewState["DatosFicha"];
            if (detalleDatosSolicitud == null)
            {
                detalleDatosSolicitud = new DetalleDatosSolicitud();

                SolicitudConcesion solicitudAux = (SolicitudConcesion)Session[paginas.solicitudECMPOSession];
                detalleDatosSolicitud.idSolConcesion = solicitudAux.idSolConcesion;
            }

            if (!SuperficieSectorCultivo.Text.Trim().Equals(""))
            {
                detalleDatosSolicitud.superficieSectorAmerb = Convert.ToSingle(SuperficieSectorCultivo.Text);
            }
            else {
                detalleDatosSolicitud.superficieSectorAmerb = 0;
            }


            if (!PorcentajeSectorCultivo.Text.Trim().Equals("")) {
                detalleDatosSolicitud.porcentSectorAmerb = Convert.ToSingle(PorcentajeSectorCultivo.Text);
            }
            else
            {
                detalleDatosSolicitud.porcentSectorAmerb = 0;
            }
            


            List<String> erroresValidacion = inicioSolicitudConcesionValidacion.validarFichaECMPO(detalleDatosSolicitud);


            if (erroresValidacion != null && erroresValidacion.Count <= 0)
            {

                //SE GUARDA LA UNIDAD ESPACIAL 
                bool resp = informacionAdicionalSolicitudesService.GuardarDetalleDatosSolicitud(detalleDatosSolicitud, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                if (resp)
                {
                    ViewState["DatosFicha"] = detalleDatosSolicitud;

                    msgGrillaGral_1.Text = "Se ha guardado la información exitosamente.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                    UpdatePanelMensajesSuperior.Update();
                }
                else
                {
                    msgGrillaGral_1.Text = "Ha ocurrido un error al realizar la acción solicitada.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                    UpdatePanelMensajesSuperior.Update();
                }
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            }
            else
            {
                foreach (String error in erroresValidacion)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }

                UpdatePanelMensajesSuperior.Update();
            }
        }

        protected void GuardarDatosEcmpo_Click(object sender, ImageClickEventArgs e)
        {
            DetalleDatosSolicitud detalleDatosSolicitud = (DetalleDatosSolicitud)ViewState["DatosFicha"];
            if (detalleDatosSolicitud == null)
            {
                detalleDatosSolicitud = new DetalleDatosSolicitud();

                SolicitudConcesion solicitudAux = (SolicitudConcesion)Session[paginas.solicitudECMPOSession];
                detalleDatosSolicitud.idSolConcesion = solicitudAux.idSolConcesion;
            }


            List<String> erroresValidacion = inicioSolicitudConcesionValidacion.validaECMPOPadre(detalleDatosSolicitud);

            detalleDatosSolicitud.codigo = Convert.ToInt32(IdEcmpo.Value);

            if (detalleDatosSolicitud.codigo < 1 && !Ecmpo.Text.Trim().Equals(""))
            {
                erroresValidacion.Add("ECMPO no encontrado");
            }

            

            if (erroresValidacion != null && erroresValidacion.Count <= 0)
            {
                //Se guarda la relación de la amerb con su padre.
                bool resp = informacionAdicionalSolicitudesService.guardarECMPOPadreSolicitud(detalleDatosSolicitud, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                if (resp)
                {
                    ViewState["DatosFicha"] = detalleDatosSolicitud;

                    msgGrillaGral_1.Text = "Se ha guardado la información exitosamente.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                    UpdatePanelMensajesSuperior.Update();
                }
                else
                {
                    msgGrillaGral_1.Text = "Ha ocurrido un error al realizar la acción solicitada.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                    UpdatePanelMensajesSuperior.Update();
                }
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            }
            else
            {
                foreach (String error in erroresValidacion)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }

                UpdatePanelMensajesSuperior.Update();
            }
        }

        protected void Ecmpo_TextChanged(object sender, EventArgs e)
        {
            if (Ecmpo.Text != null && !Ecmpo.Text.Equals(""))
            {
                DataExterna dataExterna = dataExternaDA.Obtener_SSP_DatosEcmpo(Ecmpo.Text);

                if (dataExterna != null)
                {
                    IdEcmpo.Value = Convert.ToString(dataExterna.codigo);

                    Solicitud_Emcpo.Text = dataExterna.nombre;

                    ComunaIndigena.Text = dataExterna.comunaIndigena;

                    Comuna.Text = dataExterna.comuna;

                    Region.Text = dataExterna.region;

                    Estado.Text = dataExterna.estado;

                    PanelECMPO.Visible = true;
                    UpdatePanelECMPO.Update();
                }
            }
            else {

                IdEcmpo.Value = "0";

                Solicitud_Emcpo.Text = "";

                ComunaIndigena.Text = "";

                Comuna.Text = "";

                Region.Text = "";

                Estado.Text = "";

                PanelECMPO.Visible = false;
                UpdatePanelECMPO.Update();
            }
        }

    }
}