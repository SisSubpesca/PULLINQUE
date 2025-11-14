using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using Datos.Entidades;
using Datos.Utilidades;
using Datos.Entidades.Resolucion.ResolucionSolicitud;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.resolucion;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.servicios.concesiones;
using AjaxControlToolkit;


namespace SubPesca.Unidades.Colector
{
    public partial class cambiarVigenciaColector : System.Web.UI.Page
    {
        ConcesionService concesionService = new ConcesionService();
        ResolucionDA resolucionDA = new ResolucionDA();
        SolicitudDA solicitudDA = new SolicitudDA();
        UnidadEspacialDA unidadEspacialDA = new UnidadEspacialDA();
        DocumentosConcesionDA documentosConcesionDA = new DocumentosConcesionDA();




        protected void Agregar_Click(object sender, EventArgs e)
        {

            ResolucionSolicitud resolucionSolicitud = new ResolucionSolicitud();

            resolucionSolicitud.solicitud = new SolicitudConcesion();
            resolucionSolicitud.solicitud.idSolConcesion = Convert.ToInt32(IdSolConcesion.Value);

            if (Resolucion.SelectedItem != null && !Resolucion.SelectedItem.Value.Equals(""))
            {
                resolucionSolicitud.resolucion = new Datos.Entidades.Resolucion.Resolucion();
                resolucionSolicitud.resolucion.idResolucion = Convert.ToInt32(Resolucion.SelectedItem.Value);
            }

            if (Oficio.SelectedItem != null && !Oficio.SelectedItem.Value.Equals(""))
            {
                resolucionSolicitud.docConcesion = new DocumentosConcesion();
                resolucionSolicitud.docConcesion.idDocConcesion = Convert.ToInt32(Oficio.SelectedItem.Value);
            }

            resolucionSolicitud.observaciones = Observaciones.Text;

            resolucionSolicitud.tipoIngreso = new ParametroGenerico();
            resolucionSolicitud.tipoIngreso.id = rbTipo.TIPO_INTERFAZ_RESOLUCION_ADMIN_UE;

            resolucionSolicitud.estadoVigencia = new ParametroGenerico();
            resolucionSolicitud.estadoVigencia.id = rbEstadosGenerales.VIGENTE;

            SolicitudConcesion solicitudConcesion = solicitudDA.ObtieneSolicitudConcesion(resolucionSolicitud.solicitud.idSolConcesion, 0);

            if (solicitudConcesion != null)
            {
                ParametroGenerico estadoSolicitud = solicitudConcesion.estadoVigencia;
                if (estadoSolicitud != null && estadoSolicitud.id == rbEstadosGenerales.VIGENTE)
                {

                    solicitudConcesion.estadoVigencia.id = rbEstadosGenerales.NO_VIGENTE;

                }
                else if (estadoSolicitud != null && estadoSolicitud.id == rbEstadosGenerales.NO_VIGENTE)
                {
                    solicitudConcesion.estadoVigencia.id = rbEstadosGenerales.VIGENTE;
                }

                resolucionSolicitud.solicitud = solicitudConcesion;
            }

            bool resp = concesionService.cambiarVigenciaConcesion(resolucionSolicitud, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

            if (resp)
            {
                this.ObtencionParametros();
                Observaciones.Text = "";

                msgGrilla_Sol.Text = "Se ha guardado exitosamente el cambio de vigencia de la unidad espacial.";

            }
            else
            {
                msgGrilla_Sol.Text = "No se ha guardado el cambio de vigencia de la unidad espacial.";
            }

            Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            PanelSolicitudesMsg.Visible = true;
            UpdatePanelMsg.Update();
        }

        /* Obtiene el idSolConcesion de la solicitud que gatillo la acción */
        private void ObtencionParametros()
        {
            // Se recibe el idSolConcesion
            try
            {
                if (Request.QueryString["idSolConcesion"] != null)
                {
                    IdSolConcesion.Value = Convert.ToString(Request.QueryString["idSolConcesion"]);

                    Inicializar_Formulario();

                    Inicializar_Combobox();

                }
                else
                {

                }

            }
            catch
            {

            };
        }

        private void Inicializar_Combobox()
        {

            cargarCombobox("TipoDocumento");
        }

        private void Inicializar_Formulario()
        {

            UnidadEspacial unidadEspacial = unidadEspacialDA.ObtieneUnidadEspacial(Convert.ToInt32(IdSolConcesion.Value), 0);
            if (unidadEspacial != null)
            {

                CodigoCentro.Text = unidadEspacial.centrosDeCultivo.codigoCentro + " " + unidadEspacial.centrosDeCultivo.nombreCentro;

                SolicitudConcesion solicitudConcesion = solicitudDA.ObtieneAdminUnidadesEspaciales(Convert.ToInt32(IdSolConcesion.Value));

                //SolicitudConcesion solicitudConcesion = solicitudDA.ObtieneSolicitudConcesion(Convert.ToInt32(IdSolConcesion.Value), 0);

                if (solicitudConcesion != null)
                {
                    if (solicitudConcesion.estadoVigencia != null && solicitudConcesion.estadoVigencia.id > 0)
                    {
                        VigenciaActual.Text = solicitudConcesion.estadoVigencia.descripcion;
                    }

                    Titulares.Text = solicitudConcesion.titularesCad;
                }
            }

        }

        private void cargarCombobox(string combobox)
        {
            switch (combobox)
            {

                case "TipoDocumento":
                    TipoDocumento.Items.Clear();
                    TipoDocumento.Items.Add(new ListItem("Oficio", Convert.ToString(rbTipo.OFICIO)));
                    TipoDocumento.Items.Add(new ListItem("Resolución", Convert.ToString(rbTipo.RESOLUCION)));
                    TipoDocumento.DataBind();
                    TipoDocumento.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    break;


                case "Resolucion":

                    // Cargamos el combobox: Resolucion
                    Resolucion.Items.Clear();
                    Resolucion.DataSource = resolucionDA.ListarResolucionesManuales_AdminUE(Convert.ToInt32(IdSolConcesion.Value));
                    Resolucion.DataTextField = "cadena";
                    Resolucion.DataValueField = "idResolucion";
                    Resolucion.DataBind();
                    Resolucion.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                case "Oficio":

                    // Cargamos el combobox: Oficio
                    Oficio.Items.Clear();
                    Oficio.DataSource = documentosConcesionDA.ListarDocumentosConcesionOficio(Convert.ToInt32(IdSolConcesion.Value));
                    Oficio.DataTextField = "nombreTema";
                    Oficio.DataValueField = "idDocConcesion";
                    Oficio.DataBind();
                    Oficio.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                // Obtención de parámetros mediante GET/POST
                ObtencionParametros();

            }
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            string path = "~/Unidades/Colector/administrarColectorSemillas.aspx";
            Response.Redirect(path);
        }

        protected void TipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {

            limpiarForm();

            try
            {
                int idTipoDocumento = Convert.ToInt32(TipoDocumento.SelectedValue);
                if (idTipoDocumento > 0 && idTipoDocumento == rbTipo.OFICIO)
                {
                    cargarCombobox("Oficio");
                    PanelOficio.Visible = true;
                    PanelResol.Visible = false;
                    UpdatePanelOficio.Update();
                    UpdatePanelResol.Update();
                }
                else if (idTipoDocumento > 0 && idTipoDocumento == rbTipo.RESOLUCION)
                {
                    cargarCombobox("Resolucion");
                    PanelResol.Visible = true;
                    PanelOficio.Visible = false;
                    UpdatePanelResol.Update();
                    UpdatePanelOficio.Update();
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/Administrador/principal.aspx");
            }
        }

        private void limpiarForm()
        {
            PanelOficio.Visible = false;
            PanelResol.Visible = false;

            UpdatePanelOficio.Update();
            UpdatePanelResol.Update();
        }
    }
}