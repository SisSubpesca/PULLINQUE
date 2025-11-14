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

namespace SubPesca.Solicitudes.ExperimentalesConcesion
{
    public partial class datosCentroExperimentalesConcesion : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        UnidadEspacialValidacion unidadEspacialValidacion = new UnidadEspacialValidacion();
        InformacionAdicionalSolicitudesService informacionAdicionalSolicitudesService = new InformacionAdicionalSolicitudesService();
        InicioSolicitudConcesionValidacion inicioSolicitudConcesionValidacion = new InicioSolicitudConcesionValidacion();
        PermisosService permisosService = new PermisosService();

        protected void setearModulo()
        {

            ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_EXPERIMENTALES_CONCESION;
            ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_CONCESION;
            ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_EXPERIMENTALES_CONCESION;
            ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_EXPERIMENTALES_CONCESION;
            ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_EXPERIMENTALES_CONCESION;
            ViewState["solicitudSession"] = paginas.solicitudExperimentalesConcesionSession;

            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.FICHA_EXPERIMENTALES_CONCESION };

        }

        protected void Page_Load(object sender, EventArgs e)
        {
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
                }
                else
                {
                    SuperficieSectorCultivo.ReadOnly = true;
                    PorcentajeSectorCultivo.ReadOnly = true;
                    PanelBotonGuardar.Visible = false;
                }

                // Inicializamos el formulario
                Initialize_Form();

                PanelNumPert.Visible = true;
                UpdatePanelNumPert.Update();


            }
        }


        protected void Initialize_Form()
        {
            SolicitudConcesion solicitudAux = (SolicitudConcesion)Session[paginas.solicitudExperimentalesConcesionSession];
            if (solicitudAux != null && solicitudAux.idSolConcesion > 0)
            {

                IdSolicitud.Value = Convert.ToString(solicitudAux.idSolConcesion);
                NumPert.Text = solicitudAux.numPert;


                //INFORMACIÓN QUE SE INGRESO AL CREAR EL TRAMITE
                DatosSolicitudUE datosSolicitudUE = informacionAdicionalSolicitudesService.ObtieneDatosSolicitudUE(solicitudAux.idSolConcesion);

                if (datosSolicitudUE != null && datosSolicitudUE.numeroCI > 0)
                {
                    NumeroCI.Text = Convert.ToString(datosSolicitudUE.numeroCI);
                }

                if (datosSolicitudUE != null && datosSolicitudUE.fechaCI != null && datosSolicitudUE.fechaCI != default(DateTime))
                {
                    FechaCI.Text = FechaUtils.formatearFecha(datosSolicitudUE.fechaCI);
                }

                //ESTA INFORMACIÓN ES LA QUE SE INGRESA EN LA FICHA PARTICULAR DE CADA UNIDAD ESPACIAL
                DetalleDatosSolicitud detalleDatosSolicitud = informacionAdicionalSolicitudesService.ObtieneDetalleDatosSolicitud(solicitudAux.idSolConcesion);

                if (detalleDatosSolicitud == null)
                {
                    detalleDatosSolicitud = new DetalleDatosSolicitud();
                }

                if (detalleDatosSolicitud != null)
                {
                    SuperficieSectorCultivo.Text = Convert.ToString(detalleDatosSolicitud.superficieSectorAmerb);
                    PorcentajeSectorCultivo.Text = Convert.ToString(detalleDatosSolicitud.porcentSectorAmerb);
                }


                DatosSolicitudUE datosSolicitudUEAux = informacionAdicionalSolicitudesService.ObtieneDatosSolicitudUENew(solicitudAux.idSolConcesion);
                if (datosSolicitudUEAux != null)
                {
                    CodigoCentro.Text = datosSolicitudUEAux.codCentro;
                }

                detalleDatosSolicitud.numIdentSolicitud = Convert.ToInt32(NumPert.Text);

                if (datosSolicitudUE != null && datosSolicitudUE.numeroCI > 0)
                {
                    detalleDatosSolicitud.numeroCI = Convert.ToInt32(NumeroCI.Text);
                }
                if (datosSolicitudUE != null && datosSolicitudUE.fechaCI != null && datosSolicitudUE.fechaCI != default(DateTime))
                {
                    detalleDatosSolicitud.fechaCI = Convert.ToDateTime(FechaCI.Text);
                }

                ViewState["DatosFicha"] = detalleDatosSolicitud;

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

                SolicitudConcesion solicitudAux = (SolicitudConcesion)Session[paginas.solicitudExperimentalesConcesionSession];
                detalleDatosSolicitud.idSolConcesion = solicitudAux.idSolConcesion;
            }


            if (!SuperficieSectorCultivo.Text.Trim().Equals(""))
            {
                detalleDatosSolicitud.superficieSectorAmerb = Convert.ToSingle(SuperficieSectorCultivo.Text);
            }

            if (!PorcentajeSectorCultivo.Text.Trim().Equals(""))
            {
                detalleDatosSolicitud.porcentSectorAmerb = Convert.ToSingle(PorcentajeSectorCultivo.Text);
            }


            List<String> erroresValidacion = inicioSolicitudConcesionValidacion.validarFichaExperimentalesConcesion(detalleDatosSolicitud);


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
    }
}