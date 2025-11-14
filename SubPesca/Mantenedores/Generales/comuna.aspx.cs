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

namespace SubPesca.Mantenedores.Generales
{
    public partial class comuna : System.Web.UI.Page
    {
        Datos.Entidades.Usuario usuarios = new Datos.Entidades.Usuario();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
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

                //Se carga la lista de regiones
                CargarCombobox("Provincia");

                //Se carga la lista de regiones
                CargarCombobox("esFronteriza");

                // Cargamos la grilla
                CargaGrilla();
            };
        }

        private void CargarCombobox(string combobox)
        {
            switch (combobox)
            {
                case "Provincia":
                    // Cargamos el combobox: Provincia
                    Provincia.Items.Clear();
                    Provincia.DataSource = parametroGenericoDA.ListarProvinciaReg(0,0);
                    Provincia.DataTextField = "descripcion";
                    Provincia.DataValueField = "id";
                    Provincia.DataBind();
                    Provincia.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "esFronteriza":

                    // Cargamos el combobox: esFronteriza
                    esFronteriza.Items.Clear();
                    esFronteriza.DataBind();
                    esFronteriza.Items.Insert(0, new ListItem("Si", "1"));
                    esFronteriza.Items.Insert(0, new ListItem("No", "0"));
                    esFronteriza.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
            }
        }

        // ACCIONES DE BOTONES
        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            Comuna comuna = new Comuna();
            comuna.id_provincia = Convert.ToInt32(Provincia.SelectedItem.Value);
            //comuna.codigo = Convert.ToInt32(codigo.Text);
            comuna.comuna = nombre.Text.Trim();
            comuna.esFronterizaFiltro = Convert.ToInt32(esFronteriza.SelectedItem.Value);

            if (comuna != null)
            {
                DataTable dt = mantenedorGeneralService.guardarComuna(comuna);

                try
                {
                    string msg = Convert.ToString(dt.Rows[0]["msg"]);
                    if (msg == "OK")
                    {
                        mensaje = "La comuna '" + comuna.comuna + "' ha sido creada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        GridView1.EditIndex = -1;
                        GridView1.PageIndex = 0;

                        Provincia.SelectedValue = "-1";
                        //codigo.Text = "";
                        nombre.Text = "";
                        //esFronteriza.Checked = false;
                        esFronteriza.SelectedValue = "-1";

                        CargaGrilla();
                    }
                    else
                    {
                        mensaje = "La comuna '" + comuna.comuna + "' ya existe.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    };
                }
                catch
                {
                    mensaje = "Se ha producido un error al intentar crear la comuna.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };

            }
            else
            {
                mensaje = "Para agregar debe ingresar los datos de la comuna.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            // Iniciamos la query de búsqueda y cargamos la grilla
            Comuna comunaFiltro = new Comuna();
            comunaFiltro.id_provincia = Convert.ToInt32(Provincia.SelectedItem.Value);
            comunaFiltro.comuna = nombre.Text;
            comunaFiltro.esFronterizaFiltro = Convert.ToInt32(esFronteriza.SelectedItem.Value);

            List<Comuna> dt = mantenedorGeneralService.listarComuna(comunaFiltro);

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
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar la comuna " + DataBinder.Eval(e.Row.DataItem, "comuna") + "?')");
                    boton_eliminar.Visible = true;
                };
            };

            if (GridView1.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddllist1 = ((DropDownList)e.Row.FindControl("ddleditCountry"));
                var hdnCountryName = ((HiddenField)e.Row.FindControl("hdnCountry"));
                ddllist1.AppendDataBoundItems = true;
                
                ddllist1.DataSource = parametroGenericoDA.ListarProvinciaReg(0, 0);
                ddllist1.DataTextField = "descripcion";
                ddllist1.DataValueField = "id";

                ddllist1.DataBind();
                ddllist1.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                ddllist1.Items.FindByText(hdnCountryName.Value).Selected = true;


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

            DropDownList provincia = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry");
            //TextBox codigo = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geCodigo");
            TextBox nombre = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geNombre");
            //CheckBoxList esFronteriza = (CheckBoxList)GridView1.Rows[e.RowIndex].FindControl("geEsFronteriza");
            DropDownList esFronteriza = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry2");

            //if (codigo.Text != "" && nombre.Text != "" && provincia.SelectedValue != "-1")
            if (nombre.Text != "" && provincia.SelectedValue != "-1")
            {
                id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                Agregar.Enabled = true;
                //Update(id, Convert.ToInt32(codigo.Text), nombre.Text, provincia.SelectedItem.Value, esFronteriza.SelectedItem);
                Update(id, 0, nombre.Text, provincia.SelectedItem.Value, esFronteriza.SelectedItem.Value);
            }
            else
            {
                Content_msgGrilla.Visible = true;
                msgGrilla.Text = "Para modificar debe ingresar una comuna";
            };
            CargaGrilla();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Agregar.Enabled = true;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void Update(int id, int codigo, string nombre, string idProvincia, string esFronteriza)
        {
            Comuna comuna = new Comuna();
            comuna.id_comuna = id;
            //comuna.codigo = codigo;
            comuna.comuna = nombre.Trim();
            comuna.id_provincia = Convert.ToInt32(idProvincia);
            comuna.esFronterizaFiltro = Convert.ToInt32(esFronteriza);

            //if (esFronteriza != null && esFronteriza.Value.Equals("Si"))
            //{
            //    comuna.esFronteriza = true;
            //}
            //else {
            //    comuna.esFronteriza = false;
            //}

            DataTable dt = mantenedorGeneralService.actualizarComuna(comuna);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);
                if (msg == "OK")
                {
                    mensaje = "La comuna con ID:" + id + " ha sido actualizada.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    GridView1.EditIndex = -1;
                }
                else
                {
                    mensaje = "La comuna '" + nombre + "' ya existe.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar actualizar la comuna.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void Delete(int id)
        {
            DataTable dt = mantenedorGeneralService.eliminarComuna(id);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);

                switch (msg)
                {
                    case "OK":
                        mensaje = "La comuna ha sido eliminada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        break;
                    case "En uso":
                        mensaje = "La comuna que intenta borrar está actualmente en uso.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                    default:
                        mensaje = "Se ha producido un error al intentar eliminar la comuna.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar la comuna.";
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
            string nom_grilla = "comuna";
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "comuna":
                    GridView1.Columns.RemoveAt(4);
                    grilla = GridView1;
                    ngrilla = "comuna.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            Comuna comuna = new Comuna();
            comuna.id_provincia = Convert.ToInt32(Provincia.SelectedItem.Value);
            comuna.comuna = nombre.Text;
            comuna.esFronterizaFiltro = Convert.ToInt32(esFronteriza.SelectedItem.Value);

            if (comuna != null)
            {
                List<Comuna> dt = mantenedorGeneralService.listarComuna(comuna);

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