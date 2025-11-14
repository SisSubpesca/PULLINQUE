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
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace SubPesca.Mantenedores.Generales
{
    public partial class estructuraTecnica : System.Web.UI.Page
    {
        Datos.Entidades.Usuario usuarios = new Datos.Entidades.Usuario();
        MantenedorGeneralService mantenedorGeneralService = new MantenedorGeneralService();
        MantenedorDA mantenedorDA = new MantenedorDA();

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

        // PAGE_LOAD
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Validamos los accesos al listado y a sus objetos
                Content_Panel.Visible = true;

                //Cargamos combobox
                CargaCombobox("aplicaArea");

                //Cargamos combobox
                CargaCombobox("aplicaVolumen");

                // Cargamos la grilla
                CargaGrilla();
            };
        }

        private void CargaCombobox(string combobox)
        {
            switch (combobox)
            {
                case "aplicaArea":

                    // Cargamos el combobox: aplicaArea
                    aplicaArea.Items.Clear();
                    aplicaArea.DataBind();
                    aplicaArea.Items.Insert(0, new ListItem("Si", "1"));
                    aplicaArea.Items.Insert(0, new ListItem("No", "0"));
                    aplicaArea.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "aplicaVolumen":

                    // Cargamos el combobox: aplicaVolumen
                    aplicaVolumen.Items.Clear();
                    aplicaVolumen.DataBind();
                    aplicaVolumen.Items.Insert(0, new ListItem("Si", "1"));
                    aplicaVolumen.Items.Insert(0, new ListItem("No", "0"));
                    aplicaVolumen.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
            }
        }

        // ACCIONES DE BOTONES
        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            EstructuraTecnica estructuraTecnica = new EstructuraTecnica();
            estructuraTecnica.nombreEstructura = nombre.Text;
            estructuraTecnica.aplicaAreaFiltro = Convert.ToInt32(aplicaArea.SelectedItem.Value);
            estructuraTecnica.aplicaVolumenFiltro = Convert.ToInt32(aplicaVolumen.SelectedItem.Value); 

            //estructuraTecnica.aplicaArea = aplicaArea.Checked;
            //estructuraTecnica.aplicaVolumen = aplicaVolumen.Checked;

            if (estructuraTecnica != null)
            {
                DataTable dt = mantenedorGeneralService.guardarEstructuraTecnica(estructuraTecnica);

                try
                {
                    string msg = Convert.ToString(dt.Rows[0]["msg"]);
                    if (msg == "OK")
                    {
                        mensaje = "La estructura técnica '" + estructuraTecnica.nombreEstructura + "' ha sido creada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        GridView1.EditIndex = -1;
                        GridView1.PageIndex = 0;

                        nombre.Text = "";
                        aplicaArea.SelectedValue = "-1";
                        aplicaVolumen.SelectedValue = "-1";

                        CargaGrilla();
                    }
                    else
                    {
                        mensaje = "La estructura técnica '" + estructuraTecnica.nombreEstructura + "' ya existe.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    };
                }
                catch
                {
                    mensaje = "Se ha producido un error al intentar crear la estructura técnica.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };

                //nombre.Text = "";
                //aplicaArea.Checked = false;
                //aplicaVolumen.Checked = false;
            }
            else
            {
                mensaje = "Para agregar debe ingresar los datos de la estructura técnica.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            // Iniciamos la query de búsqueda y cargamos la grilla
            EstructuraTecnica estructuraTecnicaFiltro = new EstructuraTecnica();
            estructuraTecnicaFiltro.nombreEstructura = nombre.Text;
            estructuraTecnicaFiltro.aplicaAreaFiltro = Convert.ToInt32(aplicaArea.SelectedItem.Value);
            estructuraTecnicaFiltro.aplicaVolumenFiltro = Convert.ToInt32(aplicaVolumen.SelectedItem.Value); 

            List<EstructuraTecnica> dt = mantenedorGeneralService.listarEstructuraTecnica(estructuraTecnicaFiltro);

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
                // Modificar
                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_modificar != null)
                {
                    boton_modificar.Visible = true;
                };

                // Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar la estructura Tecnica " + DataBinder.Eval(e.Row.DataItem, "nombreEstructura") + "?')");
                    boton_eliminar.Visible = true;
                };
            };

            if (GridView1.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
            {
                /* Aplica Area */
                DropDownList ddllist = ((DropDownList)e.Row.FindControl("ddleditCountry"));
                var hdnCountryName = ((HiddenField)e.Row.FindControl("hdnCountry"));
                ddllist.AppendDataBoundItems = true;

                ddllist.Items.Clear();
                ddllist.DataBind();
                ddllist.Items.Insert(0, new ListItem("Si", "1"));
                ddllist.Items.Insert(0, new ListItem("No", "0"));
                ddllist.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                ddllist.Items.FindByText(hdnCountryName.Value).Selected = true;

                /* Aplica Volumen */
                DropDownList ddllist2 = ((DropDownList)e.Row.FindControl("ddleditCountry2"));
                var hdnCountryName2 = ((HiddenField)e.Row.FindControl("hdnCountry2"));
                ddllist2.AppendDataBoundItems = true;

                ddllist2.Items.Clear();
                ddllist2.DataBind();
                ddllist2.Items.Insert(0, new ListItem("Si", "1"));
                ddllist2.Items.Insert(0, new ListItem("No", "0"));
                ddllist2.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                ddllist2.Items.FindByText(hdnCountryName2.Value).Selected = true;
            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Content_msgGrilla.Visible = false;

            int id = 0;
            switch (e.CommandName)
            {
                case "Eliminar":
                    id = Convert.ToInt32(e.CommandArgument);
                    Delete(id);
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

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Agregar.Enabled = false;
            GridView1.EditIndex = e.NewEditIndex;
            CargaGrilla();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = 0;

            TextBox nombre = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geNombre");
            //CheckBoxList aplicaArea = (CheckBoxList)GridView1.Rows[e.RowIndex].FindControl("geAplicaArea");
            //CheckBoxList aplicaVolumen = (CheckBoxList)GridView1.Rows[e.RowIndex].FindControl("geAplicaVolumen");
            DropDownList aplicaArea = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry");
            DropDownList aplicaVolumen = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry2");

            if (nombre.Text != "")
            {
                id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                Agregar.Enabled = true;
                Update(id, nombre.Text, aplicaArea.SelectedItem.Value, aplicaVolumen.SelectedItem.Value);
            }
            else
            {
                Content_msgGrilla.Visible = true;
                msgGrilla.Text = "Para modificar debe ingresar los datos de la estructura técnica.";
            };
            CargaGrilla();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Agregar.Enabled = true;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void Update(int id, string nombre, string aplicaArea, string aplicaVolumen)
        {
            EstructuraTecnica estructuraTecnica = new EstructuraTecnica();
            estructuraTecnica.idEstructura = id;
            estructuraTecnica.nombreEstructura = nombre;
            estructuraTecnica.aplicaAreaFiltro = Convert.ToInt32(aplicaArea);
            estructuraTecnica.aplicaVolumenFiltro = Convert.ToInt32(aplicaVolumen); 

            DataTable dt = mantenedorGeneralService.actualizarEstructuraTecnica(estructuraTecnica);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);
                if (msg == "OK")
                {
                    mensaje = "La estructura técnica con ID:" + id + " ha sido actualizada.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    GridView1.EditIndex = -1;
                }
                else
                {
                    mensaje = "La estructura técnica '" + nombre + "' ya existe.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar actualizar la estructura técnica.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void Delete(int id)
        {
            DataTable dt = mantenedorGeneralService.eliminarEstructuraTecnica(id);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);

                switch (msg)
                {
                    case "OK":
                        mensaje = "La estructura técnica ha sido eliminada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        break;
                    case "En uso":
                        mensaje = "La estructura técnica que intenta borrar está actualmente en uso.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                    default:
                        mensaje = "Se ha producido un error al intentar eliminar la estructura técnica.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar la estructura técnica.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void Volver_Click(object sender, EventArgs e)
        {
            string path = "~/Administrador/principal.aspx";
            Response.Redirect(path);
        }

        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();
            string nom_grilla = "estructuraTecnica";
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "estructuraTecnica":
                    GridView1.Columns.RemoveAt(4);
                    grilla = GridView1;
                    ngrilla = "estructuraTecnica.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            EstructuraTecnica estructuraTecnica = new EstructuraTecnica();
            estructuraTecnica.nombreEstructura = nombre.Text;
            estructuraTecnica.aplicaAreaFiltro = Convert.ToInt32(aplicaArea.SelectedItem.Value);
            estructuraTecnica.aplicaVolumenFiltro = Convert.ToInt32(aplicaVolumen.SelectedItem.Value); 

            //estructuraTecnica.aplicaArea = aplicaArea.Checked;
            //estructuraTecnica.aplicaVolumen = aplicaVolumen.Checked;

            if (estructuraTecnica != null)
            {
                List<EstructuraTecnica> dt = mantenedorGeneralService.listarEstructuraTecnica(estructuraTecnica);

                GridView1.DataSource = dt;
                GridView1.DataBind();

                if (dt != null && dt.Count > 0)
                {
                    ExportarGrilla.Visible = true;
                }
                else {
                    ExportarGrilla.Visible = false;
                }

                Content_msgGrilla.Visible = false;
                msgGrilla.Text = "";
                upd2.Update();
            }
        }
    }
}