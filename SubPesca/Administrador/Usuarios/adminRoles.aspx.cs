using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.usuario;
using LogicaNegocio.cl.subpesca.rb.servicios.usuario;
using Datos.Entidades;
using System.Collections.Generic;

namespace SubPesca.Administrador.Usuarios
{
    public partial class adminRoles : System.Web.UI.Page
    {
        Datos.Entidades.Usuario usuarios = new Datos.Entidades.Usuario();
        TipoDA tipoDA = new TipoDA();
        RolService rolService = new RolService();


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
                Content_GrupoUsuario.Visible = true;


                //GRUPO
                Grupo.AppendDataBoundItems = true;
                Grupo.DataSource = tipoDA.ListarTipo("TIPO_ROL");
                Grupo.DataTextField = "descripcion";
                Grupo.DataValueField = "id";
                Grupo.DataBind();
                Grupo.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
             

                //FILTRO
                despliegue.AppendDataBoundItems = true;
                despliegue.Items.Clear();
                despliegue.Items.Add(new ListItem("No", "False"));
                despliegue.Items.Add(new ListItem("Sí", "True"));
                despliegue.DataBind();

               

                // Cargamos la grilla
                CargaGrilla();


            };
        }

        // ACCIONES DE BOTONES
        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            if (Rol.Text != "")
            {

                bool aplica = false;
                if(despliegue.SelectedValue.Equals("True")){
                    aplica = true;
                }

                DataTable dt = rolService.GuardarRol(0, Rol.Text, Convert.ToInt32(Grupo.SelectedValue), aplica);

                try
                {
                    string msg = Convert.ToString(dt.Rows[0]["msg"]);
                    if (msg == "OK")
                    {
                        mensaje = "El rol '" + Rol.Text + "' ha sido creado.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        GridView1.EditIndex = -1;
                        GridView1.PageIndex = 0;
                        CargaGrilla();
                    }
                    else
                    {
                        mensaje = "El nombre de rol '" + Rol.Text + "' ya existe";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    };
                }
                catch
                {
                    mensaje = "Se ha producido un error al intentar crear el rol.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };

                Rol.Text = "";
            }
            else
            {
                mensaje = "Para agregar debe ingresar un nombre de rol.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            // Iniciamos la query de búsqueda y cargamos la grilla
            Rol rolFiltro = new Rol();
            rolFiltro.estadoVigencia = new ParametroGenerico(6);
            List<Rol> dt = rolService.ListarRoles(rolFiltro);
            int num_registros = 0;
            num_registros = dt.Count;
            GridView1.DataSource = dt;
            GridView1.DataBind();
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



                if (GridView1.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
                {

                    //GRUPO
                    DropDownList gruposRol = ((DropDownList)e.Row.FindControl("geGrupo"));
                    var hdnGrupoID = ((HiddenField)e.Row.FindControl("hdnGrupo"));
                    gruposRol.AppendDataBoundItems = true;

                    gruposRol.DataSource = tipoDA.ListarTipo("TIPO_ROL");
                    gruposRol.DataTextField = "descripcion";
                    gruposRol.DataValueField = "id";

                    gruposRol.DataBind();
                    gruposRol.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    if (!hdnGrupoID.Value.Trim().Equals(""))
                    {
                        try
                        {
                            gruposRol.Items.FindByValue(hdnGrupoID.Value).Selected = true;
                        }
                        catch (Exception) { }
                    }

                    //FILTRO
                    DropDownList despliegueFiltro = ((DropDownList)e.Row.FindControl("geDespliegue"));
                    var hdnDespliegueID = ((HiddenField)e.Row.FindControl("hdnDespliegue"));
                    despliegueFiltro.AppendDataBoundItems = true;


                    despliegueFiltro.Items.Clear();

                    despliegueFiltro.Items.Add(new ListItem("No", "False"));
                    despliegueFiltro.Items.Add(new ListItem("Sí", "True"));
                    despliegueFiltro.DataBind();

                    if (!hdnDespliegueID.Value.Trim().Equals(""))
                    {
                        try
                        {
                            despliegueFiltro.Items.FindByValue(hdnDespliegueID.Value).Selected = true;
                        }
                        catch (Exception) { }
                    }
                }



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
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar el rol " + DataBinder.Eval(e.Row.DataItem, "nombreRol") + "?')");
                    boton_eliminar.Visible = true;
                };
            };
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Content_msgGrilla.Visible = false;

            int idRol = 0;
            switch (e.CommandName)
            {
                case "Eliminar":
                    idRol = Convert.ToInt32(e.CommandArgument);
                    DeleteRol(idRol);
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
            int idRol = 0;
            TextBox rol             = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geRol");
            DropDownList grupo      = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("geGrupo");
            DropDownList despliegue = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("geDespliegue");

            if (rol.Text != "")
            {
                idRol = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                Agregar.Enabled = true;


                bool filtra = false;
                if (despliegue.SelectedValue.Equals("True")) {
                    filtra = true;
                }

                UpdateRol(idRol, rol.Text, Convert.ToInt32(grupo.SelectedValue), filtra);
            }
            else
            {
                Content_msgGrilla.Visible = true;
                msgGrilla.Text = "Para modificar debe ingresar un nombre de rol";
            };
            CargaGrilla();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Agregar.Enabled = true;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void UpdateRol(int idRol, string nombreRol, int idTipoRol, bool aplicaDespliegueFiltro)
        {
            DataTable dt = rolService.GuardarRol(idRol, nombreRol, idTipoRol, aplicaDespliegueFiltro);
            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);
                if (msg == "OK")
                {
                    mensaje = "El rol con ID:" + idRol + " ha sido actualizado.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    GridView1.EditIndex = -1;
                }
                else
                {
                    mensaje = "El nombre de rol '" + nombreRol + "' ya existe";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar el rol.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }
        
        protected void DeleteRol(int idRol)
        {
            DataTable dt = rolService.EliminarRol(idRol);
            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);
                string rol = Convert.ToString(dt.Rows[0]["nombreRol"]);

                switch (msg)
                {
                    case "OK":
                        mensaje = "El rol '" + rol + "' ha sido eliminado.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        break;
                    case "En uso":
                        mensaje = "El rol que intenta borrar está actualmente en uso.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                    default:
                        mensaje = "Se ha producido un error al intentar eliminar el rol.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar el rol.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }
    }
}
