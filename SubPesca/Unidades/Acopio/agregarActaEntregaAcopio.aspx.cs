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


namespace SubPesca.Unidades.Acopio
{
    public partial class agregarActaEntregaAcopio : System.Web.UI.Page
    {
        LogicaNegocio.cl.subpesca.rb.solicitud.UnidadEspacialDA unidadEspacialDA = new LogicaNegocio.cl.subpesca.rb.solicitud.UnidadEspacialDA();


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

        /* Obtiene el idSolConcesion de la solicitud que gatillo la acción */
        private void ObtencionParametros()
        {
            // Se recibe el idSolConcesion
            try
            {
                if (Request.QueryString["idSolConcesion"] != null)
                {
                    IdSolConcesion.Value = Convert.ToString(Request.QueryString["idSolConcesion"]);

                    UnidadEspacial unidadEspacial = unidadEspacialDA.ObtieneUnidadEspacial(Convert.ToInt32(IdSolConcesion.Value), 0);
                    if (unidadEspacial != null && unidadEspacial.numeroActaEntrega > 0 && unidadEspacial.fechaActaEntrega != null)
                    {
                        NumeroActaEntrega.Text = Convert.ToString(unidadEspacial.numeroActaEntrega);
                        FechaRecepcion.Text = FechaUtils.formatearFecha(unidadEspacial.fechaActaEntrega);

                    }

                }
                else
                {

                }

            }
            catch
            {

            };
        }

        /* Agrega la fecha y el número de acta de entrega a una concesión de acuicultura */
        protected void Agregar_Click(object sender, EventArgs e)
        {
            PanelSolicitudesMsg.Visible = false;

            UnidadEspacial unidEspacial = new UnidadEspacial();
            unidEspacial.idSolicitud = Convert.ToInt32(IdSolConcesion.Value);
            unidEspacial.numeroActaEntrega = Convert.ToInt32(NumeroActaEntrega.Text);
            unidEspacial.fechaActaEntrega = Convert.ToDateTime(FechaRecepcion.Text);

            bool resp = unidadEspacialDA.ActualizarUnidadEspacialActa(unidEspacial, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

            if (resp)
            {
                msgGrilla_Sol.Text = "Se ha guardado exitosamente los datos del Acta de Entrega.";


            }
            else
            {
                msgGrilla_Sol.Text = "No se han guardado los datos del Acta de Entrega.";

            }

            Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            PanelSolicitudesMsg.Visible = true;
            UpdatePanelMsg.Update();
        }
    }
}