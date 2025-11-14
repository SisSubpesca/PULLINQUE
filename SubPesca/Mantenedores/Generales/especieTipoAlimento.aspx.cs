using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using System.Web.UI.HtmlControls;
using Datos.Entidades;
using System.Data;

namespace SubPesca.Mantenedores.Generales
{
    public partial class especieTipoAlimento : System.Web.UI.Page
    {
        Datos.Entidades.Usuario usuarios = new Datos.Entidades.Usuario();
        MantenedorDA mantenedorDA = new MantenedorDA();

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

                //Se carga la lista de Tipo Barrio
                CargarCombobox("TipoAlimento");

                //Se carga la lista de Barrio
                CargarCombobox("EspecieAutorizadas");

                // Cargamos la grilla
                CargaGrilla();
            };
        }

        private void CargarCombobox(string combobox)
        {
            switch (combobox)
            {
                case "TipoAlimento":

                    TipoAlimento.Items.Clear();
                    TipoAlimento.DataSource = mantenedorDA.ListarTipo_Mantenedor(new ParametroGenerico(0, null, "TIPO_ALIMENTO_PT"));
                    TipoAlimento.DataTextField = "descripcion";
                    TipoAlimento.DataValueField = "id";
                    TipoAlimento.DataBind();
                    TipoAlimento.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "EspecieAutorizadas":
                    // Cargamos el combobox: Especie
                    EspecieAutorizadas.Items.Clear();

                    Especies especieFiltro = new Especies();
                    especieFiltro.esExotica = -1;
                    especieFiltro.esExperimental = -1;

                    EspecieAutorizadas.DataSource = mantenedorDA.ListarEspecieCultivo_Mantenedor(especieFiltro);
                    EspecieAutorizadas.DataTextField = "especieNombreComun";
                    EspecieAutorizadas.DataValueField = "id_especie";
                    EspecieAutorizadas.DataBind();
                    EspecieAutorizadas.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
            }
        }

        // ACCIONES DE BOTONES
        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            EspecieTipoAlimento especieTipoAlimento = new EspecieTipoAlimento();
            especieTipoAlimento.especie = new ParametroGenerico();
            especieTipoAlimento.especie.id = Convert.ToInt32(EspecieAutorizadas.SelectedItem.Value);
            especieTipoAlimento.tipoAlimento = new ParametroGenerico();
            especieTipoAlimento.tipoAlimento.id = Convert.ToInt32(TipoAlimento.SelectedItem.Value);

            if (especieTipoAlimento != null)
            {
                DataTable dt = mantenedorGeneralService.guardarTipoAlimentoEspecie(especieTipoAlimento);

                try
                {
                    string msg = Convert.ToString(dt.Rows[0]["msg"]);
                    if (msg == "OK")
                    {
                        mensaje = "La asociación especie - tipo alimento ha sido creada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        GridView1.EditIndex = -1;
                        GridView1.PageIndex = 0;

                        EspecieAutorizadas.SelectedValue = "-1";
                        TipoAlimento.SelectedValue = "-1";

                        CargaGrilla();
                    }
                    else
                    {
                        mensaje = "La asociación especie - tipo alimento ya existe.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    };
                }
                catch
                {
                    mensaje = "Se ha producido un error al intentar crear la asociación especie - tipo alimento.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };

                
            }
            else
            {
                mensaje = "Para agregar debe ingresar los datos de la asociación especie - tipo alimento.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            // Iniciamos la query de búsqueda y cargamos la grilla
            EspecieTipoAlimento especieTipoAlimento = new EspecieTipoAlimento();
            especieTipoAlimento.especie = new ParametroGenerico();
            especieTipoAlimento.especie.id = Convert.ToInt32(EspecieAutorizadas.SelectedItem.Value);
            especieTipoAlimento.tipoAlimento = new ParametroGenerico();
            especieTipoAlimento.tipoAlimento.id = Convert.ToInt32(TipoAlimento.SelectedItem.Value);

            List<EspecieTipoAlimento> dt = mantenedorGeneralService.listarTipoAlimentoEspecie(especieTipoAlimento);

            int num_registros = 0;
            num_registros = dt.Count;
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
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar la asociación especie - tipo alimento?')");
                    boton_eliminar.Visible = true;
                };
            };

            if (GridView1.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddllist1 = ((DropDownList)e.Row.FindControl("ddleditCountry"));
                var hdnCountryName = ((HiddenField)e.Row.FindControl("hdnCountry"));
                ddllist1.AppendDataBoundItems = true;

                ddllist1.DataSource = mantenedorDA.ListarTipo_Mantenedor(new ParametroGenerico(0, null, "TIPO_ALIMENTO_PT"));
                ddllist1.DataTextField = "descripcion";
                ddllist1.DataValueField = "id";
                ddllist1.DataBind();
                ddllist1.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnCountryName.Value != null && !hdnCountryName.Value.Equals(""))
                {
                    ddllist1.Items.FindByText(hdnCountryName.Value).Selected = true;
                }

                DropDownList ddllist2 = ((DropDownList)e.Row.FindControl("ddleditCountry2"));
                var hdnCountryName2 = ((HiddenField)e.Row.FindControl("hdnCountry2"));
                ddllist2.AppendDataBoundItems = true;

                Especies especieFiltro = new Especies();
                especieFiltro.esExotica = -1;
                especieFiltro.esExperimental = -1;

                ddllist2.DataSource = mantenedorDA.ListarEspecieCultivo_Mantenedor(especieFiltro);
                ddllist2.DataTextField = "especieNombreComun";
                ddllist2.DataValueField = "id_especie";

                ddllist2.DataBind();
                ddllist2.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnCountryName2.Value != null && !hdnCountryName2.Value.Equals(""))
                {
                    ddllist2.Items.FindByText(hdnCountryName2.Value).Selected = true;
                }

            }


        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Content_msgGrilla.Visible = false;

            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            switch (e.CommandName)
            {
                case "Eliminar":

                    int idTipoAlimento = Convert.ToInt32(arg[1]);
                    int idEspecie = Convert.ToInt32(arg[0]);

                    Delete(idTipoAlimento, idEspecie);
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
            DropDownList tipoAlimento = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry"); ;
            DropDownList especie = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry2"); ;

            if (tipoAlimento.SelectedValue != "-1" && especie.SelectedValue != "-1")
            {
                Agregar.Enabled = true;
                Update(tipoAlimento.SelectedValue, especie.SelectedValue);
            }
            else
            {
                Content_msgGrilla.Visible = true;
                msgGrilla.Text = "Para modificar debe ingresar la asociación tipo alimento - especie.";
            };
            CargaGrilla();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Agregar.Enabled = true;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void Update(string tipoAlimento, string especie)
        {
            EspecieTipoAlimento especieTipoAlimento = new EspecieTipoAlimento();
            especieTipoAlimento.especie = new ParametroGenerico();
            especieTipoAlimento.especie.id = Convert.ToInt32(especie);
            especieTipoAlimento.tipoAlimento = new ParametroGenerico();
            especieTipoAlimento.tipoAlimento.id = Convert.ToInt32(tipoAlimento);

            DataTable dt = mantenedorGeneralService.actualizarTipoAlimentoEspecie(especieTipoAlimento);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);
                if (msg == "OK")
                {
                    mensaje = "La asociación tipo alimento - especie ha sido actualizada.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    GridView1.EditIndex = -1;
                }
                else
                {
                    mensaje = "La asociación tipo alimento - especie ya existe.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar actualizar la asociación tipo alimento - especie.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void Delete(int idTipoAlimento, int idEspecie)
        {
            DataTable dt = mantenedorGeneralService.eliminarTipoAlimentoEspecie(idTipoAlimento, idEspecie);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);

                switch (msg)
                {
                    case "OK":
                        mensaje = "La asociación tipo alimento - especie ha sido eliminada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        break;
                    case "En uso":
                        mensaje = "La asociación tipo alimento - especie que intenta borrar está actualmente en uso.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                    default:
                        mensaje = "Se ha producido un error al intentar eliminar la asociación tipo alimento - especie.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar la asociación tipo alimento - especie.";
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
            string nom_grilla = "tipoAlimentoEspecie";
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "tipoAlimentoEspecie":
                    GridView1.Columns.RemoveAt(2);
                    grilla = GridView1;
                    ngrilla = "tipoAlimentoEspecie.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            EspecieTipoAlimento especieTipoAlimento = new EspecieTipoAlimento();
            especieTipoAlimento.especie = new ParametroGenerico();
            especieTipoAlimento.especie.id = Convert.ToInt32(EspecieAutorizadas.SelectedItem.Value);
            especieTipoAlimento.tipoAlimento = new ParametroGenerico();
            especieTipoAlimento.tipoAlimento.id = Convert.ToInt32(TipoAlimento.SelectedItem.Value);

            if (especieTipoAlimento != null)
            {
                List<EspecieTipoAlimento> dt = mantenedorGeneralService.listarTipoAlimentoEspecie(especieTipoAlimento);

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