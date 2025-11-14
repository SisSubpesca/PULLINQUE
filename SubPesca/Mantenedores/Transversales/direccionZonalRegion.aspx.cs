using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Datos.Entidades;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.servicios.mantenedores;
using LogicaNegocio.cl.subpesca.rb.common;
using SubPesca.Mantenedores.Generales;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace SubPesca.Mantenedores.Transversales
{
    public partial class direccionZonalRegion : System.Web.UI.Page
    {

        
        Datos.Entidades.Usuario usuarios = new Datos.Entidades.Usuario();
        MantenedorGeneralService mantenedorGeneralService = new MantenedorGeneralService();

        RegionDA regionDA = new RegionDA();
        MantenedorDA MantenedorDA = new MantenedorDA();


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
            scriptInclude = (HtmlGenericControl)Page.Header.FindControl("jquery.autoheight.js");
            if (scriptInclude == null)
            {
                scriptInclude = new HtmlGenericControl("script");
                scriptInclude.Attributes["type"] = "text/javascript";
                scriptInclude.Attributes["src"] = ResolveClientUrl("~/js/jquery/jquery.autoheight.js");
                scriptInclude.ID = "jquery.autoheight.js";
                Page.Header.Controls.Add(scriptInclude);
            };
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Validamos los accesos al listado y a sus objetos
                Content_Panel.Visible = true;

                //Se carga la lista de regiones
                CargarCombobox("Region");

                //Se carga la lista de dirección zonal
                CargarCombobox("DireccionZonal");

                // Cargamos la grilla
                CargaGrilla();
            }
        }

        private void CargarCombobox(string combobox)
        {
            switch (combobox)
            {
                case "Region":
                    Region.Items.Clear();
                    Region.DataSource = regionDA.ListarRegion(0);
                    Region.DataTextField = "Region";
                    Region.DataValueField = "IdRegion";
                    Region.DataBind();
                    Region.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "DireccionZonal":
                    DireccionZonal.Items.Clear();
                    DireccionZonal.DataSource = MantenedorDA.ListarDireccionZonal_Mantenedor(new DireccionZonal());
                    DireccionZonal.DataTextField = "nombreDirZonal";
                    DireccionZonal.DataValueField = "codDirZonal";
                    DireccionZonal.DataBind();
                    DireccionZonal.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

            }
        }

        // ACCIONES DE BOTONES
        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            DireccionZonal direccionZonal = new DireccionZonal();

            direccionZonal.codDirZonal = DireccionZonal.Text;

            direccionZonal.region = new ParametroGenerico();
            direccionZonal.region.id = Convert.ToInt32(Region.SelectedItem.Value);

            if (direccionZonal != null)
            {

                DataTable dt = mantenedorGeneralService.guardarDireccionZonalRegion(direccionZonal);

                try
                {
                    string msg = Convert.ToString(dt.Rows[0]["msg"]);
                    if (msg == "OK")
                    {
                        mensaje = "La dirección zonal - región '" + direccionZonal.codDirZonal + "' ha sido creada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        GridView1.EditIndex = -1;
                        GridView1.PageIndex = 0;

                        DireccionZonal.SelectedValue = "-1";
                        Region.SelectedValue = "-1";

                        CargaGrilla();
                    }
                    else
                    {
                        mensaje = "El dirección zonal - región '" + direccionZonal.codDirZonal + "' ya existe.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    };
                }
                catch
                {
                    mensaje = "Se ha producido un error al intentar crear la dirección zonal - región.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };

                
            }
            else
            {
                mensaje = "Para agregar debe ingresar la dirección zonal - región.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;


        }

        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            // Iniciamos la query de búsqueda y cargamos la grilla
            DireccionZonal direccionZonal = new DireccionZonal();

            direccionZonal.codDirZonal = DireccionZonal.Text;

            direccionZonal.region = new ParametroGenerico();
            direccionZonal.region.id = Convert.ToInt32(Region.SelectedItem.Value);

            //Aquí se debe obtener la lista de las huso
            List<DireccionZonal> dt = mantenedorGeneralService.listarDireccionZonalRegion(direccionZonal);

            int num_registros = 0;
            num_registros = dt.Count;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            if (dt != null && dt.Count > 0)
            {
                ExportarGrilla.Visible = true;
            }
            else {
                ExportarGrilla.Visible = false;
            }
        }

        protected void GridView1_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            Agregar.Enabled = true;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            
                // Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar la dirección zonal - región " + DataBinder.Eval(e.Row.DataItem, "codDirZonal") + "?')");
                    boton_eliminar.Visible = true;
                };
            };
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Content_msgGrilla.Visible = false;

            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            switch (e.CommandName)
            {
                case "Eliminar":

                    string codDirZonal = arg[0];
                    int idRegion = Convert.ToInt32(arg[1]);

                    Delete(codDirZonal, idRegion);
                    GridView1.EditIndex = -1;
                    CargaGrilla();
                    break;
            };
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string orderby = KeySort.Value;
            int pos = orderby.IndexOf(e.SortExpression + " ASC");
            if (pos >= 0)
            {
                orderby = e.SortExpression + " DESC";
            }
            else
            {
                orderby = e.SortExpression + " ASC";
            };
            KeySort.Value = orderby;

            GridView1.PageIndex = 0;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void Delete(string codDirZonal, int idRegion)
        {
            DataTable dt = mantenedorGeneralService.eliminarDireccionZonalRegion(codDirZonal, idRegion);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);

                switch (msg)
                {
                    case "OK":
                        mensaje = "La dirección zonal - región ha sido eliminada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        break;
                    case "En uso":
                        mensaje = "La dirección zonal - región que intenta borrar está actualmente en uso.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                    default:
                        mensaje = "Se ha producido un error al intentar eliminar la dirección zonal - región.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar la dirección zonal - región.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();
            string nom_grilla = "direccionZonalRegion";
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "direccionZonalRegion":
                    GridView1.Columns.RemoveAt(2);
                    grilla = GridView1;
                    ngrilla = "direccionZonalRegion.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            DireccionZonal direccionZonal = new DireccionZonal();

            direccionZonal.codDirZonal = DireccionZonal.Text;

            direccionZonal.region = new ParametroGenerico();
            direccionZonal.region.id = Convert.ToInt32(Region.SelectedItem.Value);

            if (direccionZonal != null)
            {
                List<DireccionZonal> dt = mantenedorGeneralService.listarDireccionZonalRegion(direccionZonal);

                GridView1.DataSource = dt;
                GridView1.DataBind();

                if (dt != null && dt.Count > 0)
                {
                    ExportarGrilla.Visible = true;
                }
                else
                {
                    ExportarGrilla.Visible = false;
                }

                Content_msgGrilla.Visible = false;
                msgGrilla.Text = "";
                upd2.Update();
            }
        }
    }
}