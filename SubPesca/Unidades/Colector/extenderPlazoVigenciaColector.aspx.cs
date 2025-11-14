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
using SubPesca.Mantenedores.Generales;

namespace SubPesca.Unidades.Colector
{
    public partial class extenderPlazoVigenciaColector : System.Web.UI.Page
    {
        ConcesionService concesionService = new ConcesionService();
        MantenedorGeneralService mantenedorGeneralService = new MantenedorGeneralService();

        ResolucionDA resolucionDA = new ResolucionDA();
        SolicitudDA solicitudDA = new SolicitudDA();


        /* Obtiene el idSolConcesion de la solicitud que gatillo la acción */
        private void ObtencionParametros()
        {
            // Se recibe el idSolConcesion
            try
            {
                if (Request.QueryString["idSolConcesion"] != null)
                {
                    IdSolConcesion.Value = Convert.ToString(Request.QueryString["idSolConcesion"]);

                    cargarCombobox("Resolucion");
                    Resolucion.SelectedValue = "0";

                    cargarCombobox("PlazoNominal");
                    PlazoNominal.SelectedValue = "0";
                }
                else
                {

                }

            }
            catch
            {

            };
        }


        protected void PlazoNominal_change(object sender, EventArgs e)
        {

            if (Convert.ToInt32(PlazoNominal.SelectedValue) == rbTipo.FECHA_EXACTA)
            {
                PanelNumeroPlazo.Visible = false;
                UpdatePanelNumeroPlazo.Update();

                PlazoVencimiento.ReadOnly = false;
                PlazoVencimiento.CssClass = "";
                PanelFechaVencimiento.Visible = true;
                UpdatePanelPlazoVencimiento.Update();

            }
            else
            {

                PanelNumeroPlazo.Visible = true;
                UpdatePanelNumeroPlazo.Update();

                PlazoVencimiento.ReadOnly = true;
                PlazoVencimiento.CssClass = "campoDeshabilitado";
                PanelFechaVencimiento.Visible = false;
                UpdatePanelPlazoVencimiento.Update();

            }

        }

        private void cargarCombobox(string combobox)
        {
            switch (combobox)
            {

                case "Resolucion":

                    // Cargamos el combobox: Resolucion
                    Resolucion.Items.Clear();
                    Resolucion.DataSource = resolucionDA.ListarResolucionesManuales_AdminUE(Convert.ToInt32(IdSolConcesion.Value));
                    Resolucion.DataTextField = "cadena";
                    Resolucion.DataValueField = "idResolucion";
                    Resolucion.DataBind();
                    Resolucion.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                case "PlazoNominal":
                    // Cargamos el combobox: PlazoNominal
                    PlazoNominal.Items.Clear();
                    PlazoNominal.DataSource = mantenedorGeneralService.listarPlazoNominal(new ParametroGenerico("TIPO_PLAZO"));
                    PlazoNominal.DataTextField = "descripcion";
                    PlazoNominal.DataValueField = "id";
                    PlazoNominal.DataBind();
                    PlazoNominal.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

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

            if (Request.Params["__EVENTTARGET"] != null)
            {
                this.NumeroPlazo_TextChanged(null, null);
            }


            if (!Page.IsPostBack)
            {
                // Obtención de parámetros mediante GET/POST
                ObtencionParametros();

            }

            string script = "invoca_calendarios(\"extensionPlazo\");";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "invoca_calendarios", script.ToString(), true);
        }

        protected void ExtenderVigencia_Click(object sender, EventArgs e)
        {

             Page.Validate();

             if (Page.IsValid)
             {

                 ResolucionSolicitud resolucionSolicitud = new ResolucionSolicitud();

                 resolucionSolicitud.solicitud = new SolicitudConcesion();
                 resolucionSolicitud.solicitud.idSolConcesion = Convert.ToInt32(IdSolConcesion.Value);

                 resolucionSolicitud.resolucion = new Datos.Entidades.Resolucion.Resolucion();
                 resolucionSolicitud.resolucion.idResolucion = Convert.ToInt32(Resolucion.SelectedItem.Value);

                 resolucionSolicitud.observaciones = Observaciones.Text;

                 resolucionSolicitud.tipoIngreso = new ParametroGenerico();
                 resolucionSolicitud.tipoIngreso.id = rbTipo.TIPO_INTERFAZ_RESOLUCION_ADMIN_UE;

                 resolucionSolicitud.estadoVigencia = new ParametroGenerico();
                 resolucionSolicitud.estadoVigencia.id = rbEstadosGenerales.VIGENTE;

                 resolucionSolicitud.unidEspacial = new UnidadEspacial();
                 resolucionSolicitud.unidEspacial.idSolicitud = Convert.ToInt32(IdSolConcesion.Value);
                 resolucionSolicitud.unidEspacial.tipoPlazoNominal = new ParametroGenerico();
                 resolucionSolicitud.unidEspacial.tipoPlazoNominal.id = Convert.ToInt32(PlazoNominal.SelectedItem.Value);
                 resolucionSolicitud.unidEspacial.plazoInicio = Convert.ToDateTime(PlazoInicio.Text);

                 if (!NumeroPlazo.Text.Trim().Equals(""))
                 {
                     resolucionSolicitud.unidEspacial.numPlazo = Convert.ToInt32(NumeroPlazo.Text);
                 }

                 resolucionSolicitud.unidEspacial.plazoVencimiento = Convert.ToDateTime(PlazoVencimiento.Text);


                 bool resp = concesionService.extenderPlazoVigencia(resolucionSolicitud, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                 if (resp)
                 {
                     this.ObtencionParametros();

                     this.limpiarGrilla();

                     msgGrilla_Sol.Text = "Se ha guardado exitosamente la extensión de vigencia de la unidad espacial.";

                 }
                 else
                 {
                     msgGrilla_Sol.Text = "No se ha guardado la extensión vigencia de la unidad espacial.";
                 }

                 Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                 PanelSolicitudesMsg.Visible = true;
                 UpdatePanelMsg.Update();

             }
        }

        private void limpiarGrilla()
        {
            PlazoInicio.Text = "";
            NumeroPlazo.Text = "";
            PlazoVencimiento.Text = "";
            Observaciones.Text = "";
        }

        protected void NumeroPlazo_TextChanged(object sender, EventArgs e)
        {
            if (!NumeroPlazo.Text.Trim().Equals("") && Convert.ToInt32(NumeroPlazo.Text.Trim()) > 0 && PlazoInicio != null && !PlazoInicio.Text.Trim().Equals(""))
            {

                DateTime fechaInicioAux = Convert.ToDateTime(PlazoInicio.Text);

                if (PlazoNominal.SelectedItem != null && Convert.ToInt32(PlazoNominal.SelectedItem.Value) == rbTipo.TIPO_PLAZO_DIAS)
                {
                    fechaInicioAux = fechaInicioAux.AddDays(Convert.ToInt32(NumeroPlazo.Text.Trim()));
                }
                else if (PlazoNominal.SelectedItem != null && Convert.ToInt32(PlazoNominal.SelectedItem.Value) == rbTipo.TIPO_PLAZO_MESES)
                {
                    fechaInicioAux = fechaInicioAux.AddMonths(Convert.ToInt32(NumeroPlazo.Text.Trim()));
                }
                else if (PlazoNominal.SelectedItem != null && Convert.ToInt32(PlazoNominal.SelectedItem.Value) == rbTipo.TIPO_PLAZO_ANIOS)
                {
                    fechaInicioAux = fechaInicioAux.AddYears(Convert.ToInt32(NumeroPlazo.Text.Trim()));
                }
                PlazoVencimiento.Text = FechaUtils.formatearFecha(fechaInicioAux);
                UpdatePanelPlazoVencimiento.Update();
            }

        }
    }
}