using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.resolucion;


namespace SubPesca.Unidades.ExperimentalesConcesion
{
    public partial class historialCambioVigenciaExperimentalesConcesion : System.Web.UI.Page
    {
        ResolucionDA resolucionDA = new ResolucionDA();



        // CARGA DE ARCHIVOS .JS DESDE C#
        protected void Page_Init(object sender, System.EventArgs e)
        {
            HtmlGenericControl scriptInclude = new HtmlGenericControl();

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

                //Cargamos las grillas
                CargarGrilla("HistorialCambioEstado");
            }
        }

        private void CargarGrilla(string tipo)
        {
            DataTable dt = new DataTable();

            List<HistUnidEspacialVigencia> histUnidEspacialVigenciaList = resolucionDA.ListarbHistUnidEspacialVigencia(Convert.ToInt32(IdSolConcesion.Value));

            switch (tipo)
            {
                case "HistorialCambioEstado":

                    if (histUnidEspacialVigenciaList != null && histUnidEspacialVigenciaList.Count > 0)
                    {
                        GridNombres.DataSource = histUnidEspacialVigenciaList;

                        GridNombres.DataBind();
                    }
                    break;
            }
        }

        private void ObtencionParametros()
        {
            // Se recibe el rutPersona
            try
            {
                if (Request.QueryString["idSolConcesion"] != null)
                {
                    IdSolConcesion.Value = Convert.ToString(Request.QueryString["idSolConcesion"]);

                }
                else
                {

                }

            }
            catch
            {

            };
        }

        protected void GridNombres_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridNombres.PageIndex = e.NewPageIndex;
            GridNombres.DataBind();

            //Cargamos las grillas
            CargarGrilla("HistorialCambioEstado");
        }

        protected void GridNombres_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void GridNombres_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
    }
}