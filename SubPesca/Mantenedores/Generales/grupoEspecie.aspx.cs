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

namespace SubPesca.Mantenedores.Generales
{
    public partial class grupoEspecie : System.Web.UI.Page
    {
        Datos.Entidades.Usuario usuarios = new Datos.Entidades.Usuario();
        MantenedorGeneralService mantenedorGeneralService = new MantenedorGeneralService();


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

                //Cargar combobox
                CargarCombobox("EsCultivo");

                // Cargamos la grilla
                CargaGrilla();
            };
        }

        private void CargarCombobox(string combobox)
        {
            switch (combobox)
            {
                case "EsCultivo":

                    // Cargamos el combobox: EsCultivo
                    EsCultivo.Items.Clear();
                    EsCultivo.DataBind();
                    EsCultivo.Items.Insert(0, new ListItem("Si", "1"));
                    EsCultivo.Items.Insert(0, new ListItem("No", "0"));
                    EsCultivo.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
            }
        }

        // ACCIONES DE BOTONES
        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            GrupoEspecie grupoEspecie = new GrupoEspecie();
            grupoEspecie.grupoEspecie = nombre.Text.Trim(); // se agrega trim para los espacios en blanco al inicio y al final.

            if (EsCultivo.SelectedItem.Value.Equals("-1"))
            {
                mensaje = "error";
            }
            else
            {
                grupoEspecie.cultivoFiltro = Convert.ToInt32(EsCultivo.SelectedItem.Value);
            }

            if (mensaje.Equals("error"))
            {
                mensaje = "Debe seleccionar un tipo especie";    // se agrego este validador para verificar si se completa el tipo especie.
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            }
            else
            {
                if (grupoEspecie != null)
                {
                    DataTable dt = mantenedorGeneralService.guardarGrupoEspecie(grupoEspecie);

                    try
                    {
                        string msg = Convert.ToString(dt.Rows[0]["msg"]);
                        if (msg == "OK")
                        {
                            mensaje = "El grupo informativo '" + grupoEspecie.grupoEspecie + "' ha sido creado.";
                            Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                            GridView1.EditIndex = -1;
                            GridView1.PageIndex = 0;

                            nombre.Text = "";
                            EsCultivo.SelectedValue = "-1";

                            CargaGrilla();
                        }
                        else
                        {
                            mensaje = "El grupo informativo '" + grupoEspecie.grupoEspecie + "' ya existe";
                            Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        };
                    }
                    catch
                    {
                        mensaje = "Se ha producido un error al intentar crear el grupo informativo.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    };

                }
                else
                {
                    mensaje = "Para agregar debe ingresar los datos de grupo informativo.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
            }

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            // Iniciamos la query de búsqueda y cargamos la grilla
            GrupoEspecie grupoEspecieFiltro = new GrupoEspecie();
            grupoEspecieFiltro.grupoEspecie = nombre.Text;
            grupoEspecieFiltro.cultivoFiltro = Convert.ToInt32(EsCultivo.SelectedItem.Value);

            List<GrupoEspecie> dt = mantenedorGeneralService.listarGrupoEspecie(grupoEspecieFiltro);

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
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar grupo informativo " + DataBinder.Eval(e.Row.DataItem, "grupoEspecie") + "?')");
                    boton_eliminar.Visible = true;
                };
            };

            if (GridView1.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddllist = ((DropDownList)e.Row.FindControl("ddleditCountry"));
                var hdnCountryName = ((HiddenField)e.Row.FindControl("hdnCountry"));
                ddllist.AppendDataBoundItems = true;

                ddllist.Items.Clear();
                ddllist.DataBind();
                ddllist.Items.Insert(0, new ListItem("Si", "1"));
                ddllist.Items.Insert(0, new ListItem("No", "0"));
                ddllist.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                ddllist.Items.FindByText(hdnCountryName.Value).Selected = true;
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
            DropDownList esCultivo = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry");

            if (nombre.Text != "")
            {
                id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                Agregar.Enabled = true;
                Update(id, nombre.Text, esCultivo.SelectedItem.Value);
            }
            else
            {
                Content_msgGrilla.Visible = true;
                msgGrilla.Text = "Para modificar debe ingresar un nombre de grupo informativo";
            };
            CargaGrilla();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Agregar.Enabled = true;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void Update(int id, string nombre, string esCultivo)
        {
            GrupoEspecie grupoEspecie = new GrupoEspecie();
            grupoEspecie.id_grupoEspecie = id;
            grupoEspecie.grupoEspecie = nombre;
            grupoEspecie.cultivoFiltro = Convert.ToInt32(esCultivo);

            DataTable dt = mantenedorGeneralService.actualizarGrupoEspecie(grupoEspecie);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);
                if (msg == "OK")
                {
                    mensaje = "El grupo informativo con ID:" + id + " ha sido actualizado.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    GridView1.EditIndex = -1;
                }
                else
                {
                    mensaje = "El grupo informativo '" + nombre + "' ya existe.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar actualizar el grupo informativo.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void Delete(int id)
        {
            DataTable dt = mantenedorGeneralService.eliminarGrupoEspecie(id);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);

                switch (msg)
                {
                    case "OK":
                        mensaje = "El grupo informativo ha sido eliminada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        break;
                    case "En uso":
                        mensaje = "El grupo informativo que intenta borrar está actualmente en uso.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                    default:
                        mensaje = "Se ha producido un error al intentar eliminar el grupo informativo.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar el grupo informativo.";
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
            string nom_grilla = "grupoEspecie";
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "grupoEspecie":
                    GridView1.Columns.RemoveAt(3);
                    grilla = GridView1;
                    ngrilla = "grupoEspecie.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            GrupoEspecie grupoEspecie = new GrupoEspecie();
            grupoEspecie.grupoEspecie = nombre.Text;
            grupoEspecie.cultivoFiltro = Convert.ToInt32(EsCultivo.SelectedItem.Value);

            if (grupoEspecie != null)
            {
                List<GrupoEspecie> dt = mantenedorGeneralService.listarGrupoEspecie(grupoEspecie);

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