using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubPesca.Mantenedores.Generales;
using System.Web.UI.HtmlControls;
using Datos.Entidades;
using System.Data;

namespace SubPesca.Mantenedores.Transversales
{
    public partial class temaSubrequerimiento : System.Web.UI.Page
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

                //Cargamos combobox
                CargaCombobox("AplicaReitera");

                //Cargamos combobox
                CargaCombobox("AplicaComplementario");

                //Cargamos combobox
                CargaCombobox("EsVisacion");

                // Cargamos la grilla
                CargaGrilla();
            };
        }

        private void CargaCombobox(string combobox)
        {
            switch (combobox)
            {
                case "AplicaReitera":

                    // Cargamos el combobox: AplicaReitera
                    AplicaReitera.Items.Clear();
                    AplicaReitera.DataBind();
                    AplicaReitera.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaReitera.Items.Insert(0, new ListItem("No", "0"));
                    AplicaReitera.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaComplementario":

                    // Cargamos el combobox: AplicaComplementario
                    AplicaComplementario.Items.Clear();
                    AplicaComplementario.DataBind();
                    AplicaComplementario.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaComplementario.Items.Insert(0, new ListItem("No", "0"));
                    AplicaComplementario.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "EsVisacion":

                    // Cargamos el combobox: EsVisacion
                    EsVisacion.Items.Clear();
                    EsVisacion.DataBind();
                    EsVisacion.Items.Insert(0, new ListItem("Si", "1"));
                    EsVisacion.Items.Insert(0, new ListItem("No", "0"));
                    EsVisacion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
            }
        }

        // ACCIONES DE BOTONES
        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            SubRequerimiento subRequerimiento = new SubRequerimiento();
            subRequerimiento.nombreSubRequerimiento = nombre.Text.Trim();
            subRequerimiento.aplicaReiteraFiltro = Convert.ToInt32(AplicaReitera.SelectedItem.Value);
            subRequerimiento.aplicaComplementarioFiltro = Convert.ToInt32(AplicaComplementario.SelectedItem.Value);
            subRequerimiento.aplicaVisacionMasivaFiltro = Convert.ToInt32(EsVisacion.SelectedItem.Value);

            if (subRequerimiento != null)
            {
                DataTable dt = mantenedorGeneralService.guardarTemaSubrequerimiento(subRequerimiento);

                try
                {
                    string msg = Convert.ToString(dt.Rows[0]["msg"]);
                    if (msg == "OK")
                    {
                        mensaje = "El tema subRequerimiento '" + subRequerimiento.nombreSubRequerimiento + "' ha sido creado.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        GridView1.EditIndex = -1;
                        GridView1.PageIndex = 0;

                        nombre.Text = "";
                        AplicaReitera.SelectedValue = "-1";
                        AplicaComplementario.SelectedValue = "-1";

                        CargaGrilla();
                    }
                    else
                    {
                        mensaje = "El tema subRequerimiento '" + subRequerimiento.nombreSubRequerimiento + "' ya existe";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    };
                }
                catch
                {
                    mensaje = "Se ha producido un error al intentar crear tema subRequerimiento.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };

                
            }
            else
            {
                mensaje = "Para agregar debe ingresar los datos de tema subRequerimiento.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            // Iniciamos la query de búsqueda y cargamos la grilla
            SubRequerimiento subRequerimiento = new SubRequerimiento();
            subRequerimiento.nombreSubRequerimiento = nombre.Text;
            subRequerimiento.aplicaReiteraFiltro = Convert.ToInt32(AplicaReitera.SelectedItem.Value);
            subRequerimiento.aplicaComplementarioFiltro = Convert.ToInt32(AplicaComplementario.SelectedItem.Value);
            subRequerimiento.aplicaVisacionMasivaFiltro = Convert.ToInt32(EsVisacion.SelectedItem.Value);

            List<SubRequerimiento> dt = mantenedorGeneralService.listarTemaSubrequerimiento(subRequerimiento);

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
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar el tema subrequerimiento " + DataBinder.Eval(e.Row.DataItem, "nombreSubRequerimiento") + "?')");
                    boton_eliminar.Visible = true;
                };

                /* Aplica Reitera */

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

                /* Aplica Complementario */

                if (GridView1.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
                {

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

                if (GridView1.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
                {

                    DropDownList ddllist3 = ((DropDownList)e.Row.FindControl("ddleditCountry3"));
                    var hdnCountryName3 = ((HiddenField)e.Row.FindControl("hdnCountry3"));
                    ddllist3.AppendDataBoundItems = true;

                    ddllist3.Items.Clear();
                    ddllist3.DataBind();
                    ddllist3.Items.Insert(0, new ListItem("Si", "1"));
                    ddllist3.Items.Insert(0, new ListItem("No", "0"));
                    ddllist3.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    ddllist3.Items.FindByText(hdnCountryName3.Value).Selected = true;
                }
            };
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
            //CheckBoxList aplicaReitera = (CheckBoxList)GridView1.Rows[e.RowIndex].FindControl("geAplicaReitera");
            //CheckBoxList aplicaComplementario = (CheckBoxList)GridView1.Rows[e.RowIndex].FindControl("geAplicaComplementario");

            DropDownList aplicaReitera = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry");
            DropDownList aplicaComplementario = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry2");
            DropDownList aplicaVisacionMasiva = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry3");

            if (nombre.Text != "")
            {
                id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                Agregar.Enabled = true;
                Update(id, nombre.Text, aplicaReitera.SelectedItem.Value, aplicaComplementario.SelectedItem.Value, aplicaVisacionMasiva.SelectedItem.Value);
            }
            else
            {
                Content_msgGrilla.Visible = true;
                msgGrilla.Text = "Para modificar debe ingresar un nombre de tema subrequerimiento.";
            };
            CargaGrilla();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Agregar.Enabled = true;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void Update(int id, string nombre, string aplicaReitera, string aplicaComplementario, string aplicaVisacionMasiva)
        {
            SubRequerimiento subRequerimiento = new SubRequerimiento();
            subRequerimiento.idSubRequerimiento = id;
            subRequerimiento.nombreSubRequerimiento = nombre.Trim();
            subRequerimiento.aplicaReiteraFiltro = Convert.ToInt32(aplicaReitera);
            subRequerimiento.aplicaComplementarioFiltro = Convert.ToInt32(aplicaComplementario);
            subRequerimiento.aplicaVisacionMasivaFiltro = Convert.ToInt32(aplicaVisacionMasiva);

            //if (aplicaReitera != null && aplicaReitera.Value.Equals("Si"))
            //{
            //    subRequerimiento.aplicaReitera = true;
            //}
            //else
            //{
            //    subRequerimiento.aplicaReitera = false;
            //}

            //if (aplicaComplementario != null && aplicaComplementario.Value.Equals("Si"))
            //{
            //    subRequerimiento.aplicaComplementario= true;
            //}
            //else
            //{
            //    subRequerimiento.aplicaComplementario = false;
            //}
           
            DataTable dt = mantenedorGeneralService.actualizarTemaSubrequerimiento(subRequerimiento);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);
                if (msg == "OK")
                {
                    mensaje = "El tema subrequerimiento con ID:" + id + " ha sido actualizado.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    GridView1.EditIndex = -1;
                }
                else
                {
                    mensaje = "El nombre del tema subrequerimiento '" + nombre + "' ya existe";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar actualizar el tema subrequerimiento.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void Delete(int id)
        {
            DataTable dt = mantenedorGeneralService.eliminarTemaSubrequerimiento(id);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);

                switch (msg)
                {
                    case "OK":
                        mensaje = "El tema subrequerimiento ha sido eliminado.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        break;
                    case "En uso":
                        mensaje = "El tema subrequerimiento que intenta borrar está actualmente en uso.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                    default:
                        mensaje = "Se ha producido un error al intentar eliminar el tema subrequerimiento.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar el tema subrequerimiento.";
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
            string nom_grilla = "temaSubrequerimiento";
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "temaSubrequerimiento":
                    GridView1.Columns.RemoveAt(4);
                    grilla = GridView1;
                    ngrilla = "temaSubrequerimiento.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            SubRequerimiento subRequerimiento = new SubRequerimiento();
            subRequerimiento.nombreSubRequerimiento = nombre.Text;
            subRequerimiento.aplicaReiteraFiltro = Convert.ToInt32(AplicaReitera.SelectedItem.Value);
            subRequerimiento.aplicaComplementarioFiltro = Convert.ToInt32(AplicaComplementario.SelectedItem.Value);
            subRequerimiento.aplicaVisacionMasivaFiltro = Convert.ToInt32(EsVisacion.SelectedItem.Value);

            if (subRequerimiento != null)
            {
                List<SubRequerimiento> dt = mantenedorGeneralService.listarTemaSubrequerimiento(subRequerimiento);

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