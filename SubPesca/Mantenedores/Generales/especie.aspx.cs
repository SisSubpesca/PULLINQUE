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
    public partial class especie : System.Web.UI.Page
    {
        Datos.Entidades.Usuario usuarios = new Datos.Entidades.Usuario();
        MantenedorGeneralService mantenedorGeneralService = new MantenedorGeneralService();
        
        MantenedorDA mantenedorDA = new MantenedorDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();

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

                //Se carga la lista de GrupoEspecie
                CargarCombobox("GrupoEspecie");

                //Cargamos combobox
                CargarCombobox("especieExotica");

                //Cargamos combobox
                CargarCombobox("especieExperimental");

                //Cargamos combobox
                CargarCombobox("GrupoAutorizado");

                // Cargamos la grilla
                CargaGrilla();
            };
        }

        private void CargarCombobox(string combobox)
        {
            //GrupoEspecie grupoEspecieFiltro = null;

            switch (combobox)
            {
                case "GrupoEspecie":
                    //GrupoEspecieAutorizadas.Items.Clear();
                    //grupoEspecieFiltro = new GrupoEspecie();
                    //grupoEspecieFiltro.cultivoFiltro = -1;

                    //GrupoEspecieAutorizadas.DataSource = mantenedorDA.ListarGrupoEspecie_Mantenedor(grupoEspecieFiltro);
                    //GrupoEspecieAutorizadas.DataTextField = "grupoEspecie";
                    //GrupoEspecieAutorizadas.DataValueField = "id_grupoEspecie";


                    GrupoEspecieAutorizadas.DataSource = parametroGenericoDA.ListarRbGrupoEspecie(null);
                    //GrupoEspecieAutorizadas.DataSource = parametroGenericoDA.ListarGrupoEspecieInformativo(0);
                    GrupoEspecieAutorizadas.DataTextField = "descripcion";
                    GrupoEspecieAutorizadas.DataValueField = "id";

                    GrupoEspecieAutorizadas.DataBind();
                    GrupoEspecieAutorizadas.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "especieExotica":

                    // Cargamos el combobox: especieExotica
                    especieExotica.Items.Clear();
                    especieExotica.DataBind();
                    especieExotica.Items.Insert(0, new ListItem("Si", "1"));
                    especieExotica.Items.Insert(0, new ListItem("No", "0"));
                    especieExotica.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "especieExperimental":

                    // Cargamos el combobox: especieExperimental
                    especieExperimental.Items.Clear();
                    especieExperimental.DataBind();
                    especieExperimental.Items.Insert(0, new ListItem("Si", "1"));
                    especieExperimental.Items.Insert(0, new ListItem("No", "0"));
                    especieExperimental.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "GrupoAutorizado":
                    //GrupoAutorizado.Items.Clear();
                    //grupoEspecieFiltro = new GrupoEspecie();
                    //grupoEspecieFiltro.cultivoFiltro = -1;

                    //GrupoAutorizado.DataSource = mantenedorDA.ListarGrupoEspecie_Mantenedor(grupoEspecieFiltro);
                    //GrupoAutorizado.DataTextField = "grupoEspecie";
                    //GrupoAutorizado.DataValueField = "id_grupoEspecie";


                    GrupoAutorizado.DataSource = parametroGenericoDA.ListarRbGrupoEspecie(null);
                    //GrupoAutorizado.DataSource = parametroGenericoDA.ListarGrupoEspecie(0);
                    GrupoAutorizado.DataTextField = "descripcion";
                    GrupoAutorizado.DataValueField = "id";

                    GrupoAutorizado.DataBind();
                    GrupoAutorizado.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
            }
        }

        // ACCIONES DE BOTONES
        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            Especies especies = new Especies();
            especies.grupoEspecie = new GrupoEspecie();
            especies.grupoEspecie.id_grupoEspecie = Convert.ToInt32(GrupoEspecieAutorizadas.SelectedItem.Value);
            especies.codigoSernapesca = Convert.ToInt32(codigo.Text);

            especies.especieNombreComun = nombre.Text.Trim();
            especies.especieNombreCientifico = nombreCientifico.Text.Trim();
            
            //if(especieExotica.Checked){
            //    especies.esExotica = 1;
            //}else{
            //    especies.esExotica = 0;
            //}

            especies.esExotica = Convert.ToInt32(especieExotica.SelectedItem.Value);
            especies.esExperimental = Convert.ToInt32(especieExperimental.SelectedItem.Value);

            especies.grupoEspecieAutorizado = new GrupoEspecie();
            especies.grupoEspecieAutorizado.id_grupoEspecie = Convert.ToInt32(GrupoAutorizado.SelectedItem.Value);
            
            if (especies != null)
            {
                DataTable dt = mantenedorGeneralService.guardarEspecie(especies);

                try
                {
                    string msg = Convert.ToString(dt.Rows[0]["msg"]);
                    if (msg == "OK")
                    {
                        mensaje = "La especie '" + especies.especieNombreComun + "' ha sido creada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        GridView1.EditIndex = -1;
                        GridView1.PageIndex = 0;

                        GrupoEspecieAutorizadas.SelectedValue = "-1";
                        codigo.Text = "";
                        nombre.Text = "";
                        nombreCientifico.Text = "";
                        especieExotica.SelectedValue = "-1";
                        especieExperimental.SelectedValue = "-1";
                        GrupoAutorizado.SelectedValue = "-1";

                        CargaGrilla();
                    }
                    else
                    {
                        mensaje = "La especie '" + especies.especieNombreComun + "' ya existe.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    };
                }
                catch
                {
                    mensaje = "Se ha producido un error al intentar crear la especie.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };

                codigo.Text = "";
                nombre.Text = "";
            }
            else
            {
                mensaje = "Para agregar debe ingresar los datos de la especie.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            // Iniciamos la query de búsqueda y cargamos la grilla
            Especies especieFiltro = new Especies();
            especieFiltro.grupoEspecie = new GrupoEspecie();
            especieFiltro.grupoEspecie.id_grupoEspecie = Convert.ToInt32(GrupoEspecieAutorizadas.SelectedItem.Value);

            if (!codigo.Text.Equals(""))
            {
                especieFiltro.codigoSernapesca = Convert.ToInt32(codigo.Text);
            }

            especieFiltro.especieNombreComun = nombre.Text;
            especieFiltro.especieNombreCientifico = nombreCientifico.Text;
            especieFiltro.esExotica = Convert.ToInt32(especieExotica.SelectedItem.Value);
            especieFiltro.esExperimental = Convert.ToInt32(especieExperimental.SelectedItem.Value);

            especieFiltro.grupoEspecieAutorizado = new GrupoEspecie();
            especieFiltro.grupoEspecieAutorizado.id_grupoEspecie = Convert.ToInt32(GrupoAutorizado.SelectedItem.Value);

            List<Especies> dt = mantenedorGeneralService.listarEspecies(especieFiltro);

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
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar la especie " + DataBinder.Eval(e.Row.DataItem, "especieNombreComun") + "?')");
                    boton_eliminar.Visible = true;
                };
            };

            //GrupoEspecie grupoEspecieFiltro = null;

            if (GridView1.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddllist1 = ((DropDownList)e.Row.FindControl("ddleditCountry"));
                var hdnCountryName = ((HiddenField)e.Row.FindControl("hdnCountry"));
                ddllist1.AppendDataBoundItems = true;

                //grupoEspecieFiltro = new GrupoEspecie();
                //grupoEspecieFiltro.cultivoFiltro = -1;

                //ddllist1.DataSource = mantenedorDA.ListarGrupoEspecie_Mantenedor(grupoEspecieFiltro);
                //ddllist1.DataTextField = "grupoEspecie";
                //ddllist1.DataValueField = "id_grupoEspecie";


                ddllist1.DataSource = parametroGenericoDA.ListarRbGrupoEspecie(null);
                //ddllist1.DataSource = parametroGenericoDA.ListarGrupoEspecieInformativo(0);
                ddllist1.DataTextField = "descripcion";
                ddllist1.DataValueField = "id";

                ddllist1.DataBind();
                ddllist1.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                ddllist1.Items.FindByText(hdnCountryName.Value).Selected = true;


                /* Es Exotica */
                DropDownList ddllist2 = ((DropDownList)e.Row.FindControl("ddleditCountry2"));
                var hdnCountryName2 = ((HiddenField)e.Row.FindControl("hdnCountry2"));
                ddllist2.AppendDataBoundItems = true;

                ddllist2.Items.Clear();
                ddllist2.DataBind();
                ddllist2.Items.Insert(0, new ListItem("Si", "1"));
                ddllist2.Items.Insert(0, new ListItem("No", "0"));
                ddllist2.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                ddllist2.Items.FindByText(hdnCountryName2.Value).Selected = true;


                /* Es Experimental */
                DropDownList ddllist3 = ((DropDownList)e.Row.FindControl("ddleditCountry3"));
                var hdnCountryName3 = ((HiddenField)e.Row.FindControl("hdnCountry3"));
                ddllist3.AppendDataBoundItems = true;

                ddllist3.Items.Clear();
                ddllist3.DataBind();
                ddllist3.Items.Insert(0, new ListItem("Si", "1"));
                ddllist3.Items.Insert(0, new ListItem("No", "0"));
                ddllist3.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                ddllist3.Items.FindByText(hdnCountryName3.Value).Selected = true;

                /* Grupo Autorizado */
                DropDownList ddllist4 = ((DropDownList)e.Row.FindControl("ddleditCountry4"));
                var hdnCountryName4 = ((HiddenField)e.Row.FindControl("hdnCountry4"));
                ddllist4.AppendDataBoundItems = true;

                //grupoEspecieFiltro = new GrupoEspecie();
                //grupoEspecieFiltro.cultivoFiltro = -1;

                //ddllist4.DataSource = mantenedorDA.ListarGrupoEspecie_Mantenedor(grupoEspecieFiltro);
                //ddllist4.DataTextField = "grupoEspecie";
                //ddllist4.DataValueField = "id_grupoEspecie";




                ddllist4.DataSource = parametroGenericoDA.ListarRbGrupoEspecie(null);
                //ddllist4.DataSource = parametroGenericoDA.ListarGrupoEspecie(0);
                ddllist4.DataTextField = "descripcion";
                ddllist4.DataValueField = "id";

                ddllist4.DataBind();
                ddllist4.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnCountryName4.Value != null && !hdnCountryName4.Value.Equals(""))
                {
                    ddllist4.Items.FindByText(hdnCountryName4.Value).Selected = true;
                }

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

            DropDownList grupo = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry"); ;
            TextBox codigo = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geCodigo");
            TextBox nombreComun = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geNombre");
            TextBox nombreCientifico = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geNombreCientifico");

            //CheckBoxList esEspecieExotica = (CheckBoxList)GridView1.Rows[e.RowIndex].FindControl("geEspecieExotica");

            DropDownList esExotica = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry2");
            DropDownList esExperimental = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry3");
            DropDownList grupoAutorizado = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry4");

            if (codigo.Text != "" && nombreComun.Text != "" && nombreCientifico.Text != "" && grupo.SelectedValue != "-1" && esExotica.SelectedValue != "-1" && esExperimental.SelectedValue != "-1")
            {
                id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                Agregar.Enabled = true;
                Update(id, Convert.ToInt32(codigo.Text), nombreComun.Text, nombreCientifico.Text, grupo.SelectedItem.Value, 
                    esExotica.SelectedItem.Value, esExperimental.SelectedItem.Value, grupoAutorizado.SelectedItem.Value);
            }
            else
            {
                Content_msgGrilla.Visible = true;
                msgGrilla.Text = "Para modificar debe ingresar una especie.";
            };
            CargaGrilla();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Agregar.Enabled = true;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void Update(int id, int codigo, string nombreComun, string nombreCientifico, string grupo, string esExotica, string esExperimental, string grupoAutorizado)
        {
            
            Especies especies = new Especies();
            especies.id_especie = id;
            especies.codigoSernapesca = codigo;
            especies.especieNombreComun = nombreComun;
            especies.especieNombreCientifico = nombreCientifico;
            especies.grupoEspecie = new GrupoEspecie();
            especies.grupoEspecie.id_grupoEspecie = Convert.ToInt32(grupo);
            especies.esExotica = Convert.ToInt32(esExotica);
            especies.esExperimental = Convert.ToInt32(esExperimental);
            especies.grupoEspecieAutorizado = new GrupoEspecie();
            especies.grupoEspecieAutorizado.id_grupoEspecie = Convert.ToInt32(grupoAutorizado);

            DataTable dt = mantenedorGeneralService.actualizarEspecies(especies);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);
                if (msg == "OK")
                {
                    mensaje = "La especie con ID:" + id + " ha sido actualizada.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    GridView1.EditIndex = -1;
                }
                else
                {
                    mensaje = "La especie '" + nombre + "' ya existe.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar actualizar la especie.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void Delete(int id)
        {
            DataTable dt = mantenedorGeneralService.eliminarEspecie(id);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);

                switch (msg)
                {
                    case "OK":
                        mensaje = "La especie ha sido eliminada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        break;
                    case "En uso":
                        mensaje = "La especie que intenta borrar está actualmente en uso.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                    default:
                        mensaje = "Se ha producido un error al intentar eliminar la especie.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar la especie.";
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
            string nom_grilla = "especie";
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "especie":
                    GridView1.Columns.RemoveAt(8);
                    grilla = GridView1;
                    ngrilla = "especie.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            Especies especies = new Especies();
            especies.grupoEspecie = new GrupoEspecie();
            especies.grupoEspecie.id_grupoEspecie = Convert.ToInt32(GrupoEspecieAutorizadas.SelectedItem.Value);

            if (!codigo.Text.Equals(""))
            {
                especies.codigoSernapesca = Convert.ToInt32(codigo.Text);
            }
            especies.especieNombreComun = nombre.Text;
            especies.especieNombreCientifico = nombreCientifico.Text;

            especies.esExotica = Convert.ToInt32(especieExotica.SelectedItem.Value);
            especies.esExperimental = Convert.ToInt32(especieExperimental.SelectedItem.Value);

            especies.grupoEspecieAutorizado = new GrupoEspecie();
            especies.grupoEspecieAutorizado.id_grupoEspecie = Convert.ToInt32(GrupoAutorizado.SelectedItem.Value);


            if (especies != null)
            {
                List<Especies> dt = mantenedorGeneralService.listarEspecies(especies);

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