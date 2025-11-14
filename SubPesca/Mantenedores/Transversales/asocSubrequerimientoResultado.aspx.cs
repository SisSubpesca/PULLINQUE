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

namespace SubPesca.Mantenedores.Transversales
{
    public partial class asocDocumentoResultado : System.Web.UI.Page
    {
        Datos.Entidades.Usuario usuarios = new Datos.Entidades.Usuario();
        RegionDA regionDA = new RegionDA();
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

                //Se carga la lista de subrequerimiento
                CargarCombobox("Subrequerimiento");

                //Se carga la lista de resultados
                CargarCombobox("Resultado");

                // Cargamos la grilla
                CargaGrilla();
            };
        }

        private void CargarCombobox(string combobox)
        {
            switch (combobox)
            {
                case "Subrequerimiento":
                    Subrequerimiento.Items.Clear();
                    Subrequerimiento.DataSource = mantenedorGeneralService.listarResultado(0);
                    Subrequerimiento.DataTextField = "nombreSubRequerimiento";
                    Subrequerimiento.DataValueField = "idSubRequerimiento";
                    Subrequerimiento.DataBind();
                    Subrequerimiento.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;
                case "Resultado":
                    Resultado.Items.Clear();
                    Resultado.DataSource = mantenedorGeneralService.listarResultado(new ParametroGenerico());
                    Resultado.DataTextField = "descripcion";
                    Resultado.DataValueField = "id";
                    Resultado.DataBind();
                    Resultado.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
            }
        }

        // ACCIONES DE BOTONES
        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            DocumentoAmbito documentoAmbito = new DocumentoAmbito();
            
            documentoAmbito.tipo = new ParametroGenerico();
            documentoAmbito.tipo.id = Convert.ToInt32(Subrequerimiento.SelectedItem.Value);
            
            documentoAmbito.estadoResultadoResp = new ParametroGenerico();
            documentoAmbito.estadoResultadoResp.id = Convert.ToInt32(Resultado.SelectedItem.Value);

            if (documentoAmbito != null)
            {
                DataTable dt = mantenedorGeneralService.guardarSubrequerimientoResultado(documentoAmbito);

                try
                {
                    string msg = Convert.ToString(dt.Rows[0]["msg"]);
                    if (msg == "OK")
                    {
                        mensaje = "El subrequerimiento - resultado ingresado ha sido creado.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        GridView1.EditIndex = -1;
                        GridView1.PageIndex = 0;

                        Subrequerimiento.SelectedValue = "-1";
                        Resultado.SelectedValue = "-1";

                        CargaGrilla();
                    }
                    else
                    {
                        mensaje = "El nombre de subrequerimiento - resultado ingresado ya existe.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    };
                }
                catch
                {
                    mensaje = "Se ha producido un error al intentar crear subrequerimiento - resultado.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };

            }
            else
            {
                mensaje = "Para agregar debe ingresar los datos de subrequerimiento - resultado.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            // Iniciamos la query de búsqueda y cargamos la grilla
            DocumentoAmbito documentoAmbitoFiltro = new DocumentoAmbito();
            documentoAmbitoFiltro.idRequerimiento = Convert.ToInt32(Subrequerimiento.SelectedItem.Value);
            documentoAmbitoFiltro.estadoResultadoResp = new ParametroGenerico();
            documentoAmbitoFiltro.estadoResultadoResp.id = Convert.ToInt32(Resultado.SelectedItem.Value);

            List<DocumentoAmbito> dt = mantenedorGeneralService.listarSubrequerimientoResultado(documentoAmbitoFiltro);

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
                    //boton_modificar.Visible = true;
                };

                // Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar subrequerimiento - resultado " + DataBinder.Eval(e.Row.DataItem, "tipoString") + "?')");
                    boton_eliminar.Visible = true;
                };
            };

            if (GridView1.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddllist1 = ((DropDownList)e.Row.FindControl("ddleditCountry"));
                var hdnCountryName = ((HiddenField)e.Row.FindControl("hdnCountry"));
                ddllist1.AppendDataBoundItems = true;

                ddllist1.DataSource = mantenedorGeneralService.listarResultado(0);
                ddllist1.DataTextField = "nombreSubRequerimiento";
                ddllist1.DataValueField = "idSubRequerimiento";
                ddllist1.DataBind();
                ddllist1.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                ddllist1.Items.FindByText(hdnCountryName.Value).Selected = true;


                DropDownList ddllist2 = ((DropDownList)e.Row.FindControl("ddleditCountry2"));
                var hdnCountryName2 = ((HiddenField)e.Row.FindControl("hdnCountry2"));
                ddllist2.AppendDataBoundItems = true;

                ddllist2.DataSource = mantenedorGeneralService.listarResultado(new ParametroGenerico());
                ddllist2.DataTextField = "descripcion";
                ddllist2.DataValueField = "id";
                ddllist2.DataBind();
                ddllist2.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                ddllist2.Items.FindByText(hdnCountryName2.Value).Selected = true;
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

                    int idResultado = Convert.ToInt32(arg[0]);
                    int idSubrequerimiento = Convert.ToInt32(arg[1]);

                    Delete(idResultado, idSubrequerimiento);
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

            DropDownList subrequerimiento = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry"); ;
            DropDownList resultado = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry2"); ;

            if (subrequerimiento.SelectedValue != "-1" && resultado.SelectedValue != "-1")
            {
                id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                Agregar.Enabled = true;
                Update(id, subrequerimiento.SelectedItem.Value, resultado.SelectedItem.Value);
            }
            else
            {
                Content_msgGrilla.Visible = true;
                msgGrilla.Text = "Para modificar debe datos de subrequerimiento - resultado.";
            };
            CargaGrilla();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Agregar.Enabled = true;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void Update(int id, string subrequerimiento, string resultado)
        {
            DocumentoAmbito documentoAmbito = new DocumentoAmbito();
            documentoAmbito.idRequerimiento = Convert.ToInt32(Subrequerimiento.SelectedItem.Value);
            documentoAmbito.estadoResultadoResp = new ParametroGenerico();
            documentoAmbito.estadoResultadoResp.id = Convert.ToInt32(resultado);

            DataTable dt = mantenedorGeneralService.actualizarSubrequerimientoResultado(documentoAmbito);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);
                if (msg == "OK")
                {
                    mensaje = "El subrequerimiento - resultado ha sido actualizado.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    GridView1.EditIndex = -1;
                }
                else
                {
                    mensaje = "El subrequerimiento - resultado ya existe.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar actualizar el subrequerimiento - resultado.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void Delete(int idResultado, int idSubrequerimiento)
        {
            DataTable dt = mantenedorGeneralService.eliminarSubrequerimientoResultado(idResultado, idSubrequerimiento);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);

                switch (msg)
                {
                    case "OK":
                        mensaje = "El subrequerimiento - resultado ha sido eliminado.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        break;
                    case "En uso":
                        mensaje = "El subrequerimiento - resultado que intenta borrar está actualmente en uso.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                    default:
                        mensaje = "Se ha producido un error al intentar eliminar el subrequerimiento - resultado.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar el subrequerimiento - resultado.";
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
            string nom_grilla = "asocSubrequerimientoResultado";
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "asocSubrequerimientoResultado":
                    GridView1.Columns.RemoveAt(2);
                    grilla = GridView1;
                    ngrilla = "asocSubrequerimientoResultado.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            DocumentoAmbito documentoAmbito = new DocumentoAmbito();
            documentoAmbito.idRequerimiento = Convert.ToInt32(Subrequerimiento.SelectedItem.Value);
            documentoAmbito.estadoResultadoResp = new ParametroGenerico();
            documentoAmbito.estadoResultadoResp.id = Convert.ToInt32(Resultado.SelectedItem.Value);

            if (documentoAmbito != null)
            {
                List<DocumentoAmbito> dt = mantenedorGeneralService.listarSubrequerimientoResultado(documentoAmbito);

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