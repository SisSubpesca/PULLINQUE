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

namespace SubPesca.Solicitudes.Colector
{
    public partial class datosCentroColector : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        InformacionAdicionalSolicitudesService informacionAdicionalSolicitudesService = new InformacionAdicionalSolicitudesService();
        InicioSolicitudConcesionValidacion inicioSolicitudConcesionValidacion = new InicioSolicitudConcesionValidacion();
        PermisosService permisosService = new PermisosService();


        protected void setearModulo()
        {

            ViewState["URL_VER"] = paginas.URL_VER_COLECTORES_SEMILLA;
            ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_COLECTORES_SEMILLA;
            ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_COLECTORES_SEMILLA;
            ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_COLECTORES_SEMILLA;
            ViewState["URL_ERROR"] = paginas.URL_ERROR_COLECTORES_SEMILLA;
            ViewState["solicitudSession"] = paginas.solicitudColectorSession;

            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.FICHA_COLECTOR };
          
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
                    PeriodoOperacionSolicitud.ReadOnly = false;
                    Observaciones.ReadOnly = false;
                    PanelBotonGuardar.Visible = true;
                }
                else
                {
                    PeriodoOperacionSolicitud.ReadOnly = true;
                    Observaciones.ReadOnly = true;
                    PanelBotonGuardar.Visible = false;
                }

                // Inicializamos el formulario
                Initialize_Form();

                PanelNumIdentificador.Visible = true;
                UpdatePanelNumIdentificador.Update();


            }
        }


        protected void Initialize_Form()
        {
            SolicitudConcesion solicitudAux = (SolicitudConcesion)Session[paginas.solicitudColectorSession];
            
            
            if (solicitudAux != null && solicitudAux.idSolConcesion > 0)
            {

                
                IdSolicitud.Value = Convert.ToString(solicitudAux.idSolConcesion);
                

                //INFORMACIÓN QUE SE INGRESO AL CREAR EL TRAMITE
                DatosSolicitudUE datosSolicitudUE = informacionAdicionalSolicitudesService.ObtieneDatosSolicitudUE(solicitudAux.idSolConcesion);

                NumIdentificador.Text = Convert.ToString(datosSolicitudUE.numIdentSolicitud);

                if (datosSolicitudUE != null && datosSolicitudUE.numeroCI > 0) {
                    NumeroCI.Text = Convert.ToString(datosSolicitudUE.numeroCI);
                }

                if (datosSolicitudUE != null && datosSolicitudUE.fechaCI != null && datosSolicitudUE.fechaCI != default(DateTime))
                {
                    FechaCI.Text = FechaUtils.formatearFecha(datosSolicitudUE.fechaCI);
                }



                //ESTA INFORMACIÓN ES LA QUE SE INGRESA EN LA FICHA PARTICULAR DE CADA UNIDAD ESPACIAL
                DetalleDatosSolicitud detalleDatosSolicitud = informacionAdicionalSolicitudesService.ObtieneDetalleDatosSolicitud(solicitudAux.idSolConcesion);
                if (detalleDatosSolicitud != null) {

                    ViewState["DatosFicha"] = detalleDatosSolicitud;
                    PeriodoOperacionSolicitud.Text = detalleDatosSolicitud.periodoOperacionCol;
                    Observaciones.Text = detalleDatosSolicitud.observaciones;
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
            if (detalleDatosSolicitud == null) {
                detalleDatosSolicitud = new DetalleDatosSolicitud();

                 SolicitudConcesion solicitudAux = (SolicitudConcesion)Session[paginas.solicitudColectorSession];
                 detalleDatosSolicitud.idSolConcesion = solicitudAux.idSolConcesion;
            }

            detalleDatosSolicitud.periodoOperacionCol = PeriodoOperacionSolicitud.Text;
            detalleDatosSolicitud.observaciones = Observaciones.Text;


            List<String> erroresValidacion = inicioSolicitudConcesionValidacion.validarFichaColector(detalleDatosSolicitud);


            if (erroresValidacion != null && erroresValidacion.Count <= 0)
            {

                //SE GUARDA LA UNIDAD ESPACIAL 
                bool resp = informacionAdicionalSolicitudesService.GuardarDetalleDatosSolicitudUE(detalleDatosSolicitud);
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