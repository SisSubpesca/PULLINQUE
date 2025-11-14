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
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;

namespace SubPesca.Solicitudes.Faenamiento
{
    public partial class datosCentroFaenamiento : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        
        TipoDA tipoDa = new TipoDA();
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();
        UnidadEspacialValidacion unidadEspacialValidacion = new UnidadEspacialValidacion();
        InformacionAdicionalSolicitudesService informacionAdicionalSolicitudesService = new InformacionAdicionalSolicitudesService();
        InicioSolicitudConcesionValidacion inicioSolicitudConcesionValidacion = new InicioSolicitudConcesionValidacion();
        PermisosService permisosService = new PermisosService();

        protected void setearModulo()
        {

            ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_FAENAMIENTO;
            ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_FAENAMIENTO;
            ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_FAENAMIENTO;
            ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_FAENAMIENTO;
            ViewState["URL_ERROR"] = paginas.URL_ERROR_CENTRO_DE_FAENAMIENTO;
            ViewState["solicitudSession"] = paginas.solicitudFaenamientoSession;

            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.FICHA_FAENAMIENTO };
          
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
                    TipoCentroDeFaenamiento.Enabled = true;
                    ProdundidadMinima.ReadOnly = false;
                    PanelBotonGuardar.Visible = true;
                }
                else
                {
                    TipoCentroDeFaenamiento.Enabled = false;
                    ProdundidadMinima.ReadOnly = true;
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
            SolicitudConcesion solicitudAux = (SolicitudConcesion)Session[paginas.solicitudFaenamientoSession];
            if (solicitudAux != null && solicitudAux.idSolConcesion > 0)
            {

                IdSolicitud.Value = Convert.ToString(solicitudAux.idSolConcesion);
                NumPert.Text = solicitudAux.numPert;
                FechaIngresoTramite.Text = FechaUtils.formatearFecha(solicitudAux.fechaIngresoTramite);
                FechaRecepcion.Text = FechaUtils.formatearFecha(solicitudAux.fechaRecepcion);

                TipoCentroDeFaenamiento.Items.Clear();
                TipoCentroDeFaenamiento.DataSource = tipoDa.ListarTipo("TIPO_CENTRO_FAENAMIENTO");
                TipoCentroDeFaenamiento.DataTextField = "descripcion";
                TipoCentroDeFaenamiento.DataValueField = "id";
                TipoCentroDeFaenamiento.DataBind();
                TipoCentroDeFaenamiento.Items.Insert(0, new ListItem("-- Seleccione --", "0"));


                //ESTA INFORMACIÓN ES LA QUE SE INGRESA EN LA FICHA PARTICULAR DE CADA UNIDAD ESPACIAL
                DetalleDatosSolicitud detalleDatosSolicitud = informacionAdicionalSolicitudesService.ObtieneDetalleDatosSolicitud(solicitudAux.idSolConcesion);
                if (detalleDatosSolicitud != null)
                {

                    ViewState["DatosFicha"] = detalleDatosSolicitud;
                    if (detalleDatosSolicitud.tipoCentro != null)
                    {
                        TipoCentroDeFaenamiento.SelectedValue = Convert.ToString(detalleDatosSolicitud.tipoCentro.id);
                    }
                    ProdundidadMinima.Text = Convert.ToString(detalleDatosSolicitud.profundidadMin);
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

                SolicitudConcesion solicitudAux = (SolicitudConcesion)Session[paginas.solicitudFaenamientoSession];
                detalleDatosSolicitud.idSolConcesion = solicitudAux.idSolConcesion;
            }

            detalleDatosSolicitud.tipoCentro = new ParametroGenerico();
            detalleDatosSolicitud.tipoCentro.id = Convert.ToInt32(TipoCentroDeFaenamiento.SelectedValue);
            
            if (ProdundidadMinima.Text != null && !ProdundidadMinima.Text.Trim().Equals("")) { 
                detalleDatosSolicitud.profundidadMin = Convert.ToSingle(ProdundidadMinima.Text);
            }


            List<String> erroresValidacion = inicioSolicitudConcesionValidacion.validarFichaFaenamiento(detalleDatosSolicitud);


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