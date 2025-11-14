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
using SubPesca.Mantenedores.Generales;

namespace SubPesca.Mantenedores.Transversales
{
    public partial class oficinaZonal : System.Web.UI.Page
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
                CargaCombobox("EsCentral");

                // Cargamos la grilla
                CargaGrilla();
            };
        }

        private void CargaCombobox(string combobox)
        {
            switch (combobox)
            {
                case "EsCentral":

                    // Cargamos el combobox: EsCentral
                    EsCentral.Items.Clear();
                    EsCentral.DataBind();
                    EsCentral.Items.Insert(0, new ListItem("Si", "1"));
                    EsCentral.Items.Insert(0, new ListItem("No", "0"));
                    EsCentral.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
            }
        }

        // ACCIONES DE BOTONES
        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            DireccionZonal direccionZonal = new DireccionZonal();
            direccionZonal.codDirZonal = CodigoDireccionZonal.Text.Trim(); //se agrega trim para impedir los espacios en blanco al inicio y al final
            direccionZonal.nombreDirZonal = nombre.Text.Trim();  // se agrega trim para impedir los espacios en blanco al inicio y al final.

            if (EsCentral.SelectedItem.Value != null && EsCentral.SelectedItem.Value.Equals("1"))
                direccionZonal.esCentral = true;
            else
                direccionZonal.esCentral = false;

            if (direccionZonal != null)
            {
                DataTable dt = mantenedorGeneralService.GuardarDireccionZonal(direccionZonal);

                try
                {
                    string msg = Convert.ToString(dt.Rows[0]["msg"]);
                    if (msg == "OK")
                    {
                        mensaje = "La direccion Zonal '" + direccionZonal.nombreDirZonal + "' ha sido creada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        GridView1.EditIndex = -1;
                        GridView1.PageIndex = 0;

                        CodigoDireccionZonal.Text = "";
                        nombre.Text = "";
                        EsCentral.SelectedValue = "-1";

                        CargaGrilla();
                    }
                    else
                    {
                        mensaje = "La direccion Zonal '" + direccionZonal.nombreDirZonal + "' ya existe.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    };
                }
                catch
                {
                    mensaje = "Se ha producido un error al intentar crear la direccion Zonal.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };


            }
            else
            {
                mensaje = "Para agregar debe ingresar los datos de la direccion Zonal.";
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
            direccionZonal.codDirZonal = CodigoDireccionZonal.Text;
            direccionZonal.nombreDirZonal = nombre.Text;

            if (EsCentral.SelectedItem.Value != null && EsCentral.SelectedItem.Value.Equals("1"))
                direccionZonal.esCentral = true;
            else
                direccionZonal.esCentral = false;

            List<DireccionZonal> dt = mantenedorGeneralService.listarDireccionZonal(direccionZonal);

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
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar la dirección zonal " + DataBinder.Eval(e.Row.DataItem, "nombreDirZonal") + "?')");
                    boton_eliminar.Visible = true;
                };
            };

            if (GridView1.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
            {
                /* Es Central */
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

            //int id = 0;
            string codigoDireccionZonal = "";
            switch (e.CommandName)
            {
                case "Eliminar":
                    //id = Convert.ToInt32(e.CommandArgument);  
                    codigoDireccionZonal = e.CommandArgument.ToString();
                    Delete(codigoDireccionZonal);                               
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
            TextBox codigo = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geCodigo");
            TextBox nombre = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geNombre");
            DropDownList esCentral = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry");

            if (codigo.Text != "" && nombre.Text != "")
            {
                Agregar.Enabled = true;
                Update(codigo.Text, nombre.Text, esCentral.SelectedItem.Value);
            }
            else
            {
                Content_msgGrilla.Visible = true;
                msgGrilla.Text = "Para modificar debe ingresar la dirección zonal.";
            };
            CargaGrilla();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Agregar.Enabled = true;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void Update(string codigo, string nombre, string esCentral)
        {
            DireccionZonal direccionZonal = new DireccionZonal();

            direccionZonal.codDirZonal = codigo;
            direccionZonal.nombreDirZonal = nombre;

            if (esCentral != null && esCentral.Equals("1"))
                direccionZonal.esCentral = true;
            else
                direccionZonal.esCentral = false;

            DataTable dt = mantenedorGeneralService.actualizarDireccionZonal(direccionZonal);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);
                if (msg == "OK")
                {
                    mensaje = "La direccion zonal con ID:" + codigo + " ha sido actualizada.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    GridView1.EditIndex = -1;
                }
                else
                {
                    mensaje = "La direccion zonal '" + nombre + "' ya existe.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar actualizar la direccion zonal.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void Delete(string id)
        {
            DataTable dt = mantenedorGeneralService.eliminarDireccionZonal(id);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);

                switch (msg)
                {
                    case "OK":
                        mensaje = "La direccion zonal ha sido eliminada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        break;
                    case "En uso":
                        mensaje = "La direccion zonal que intenta borrar está actualmente en uso.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                    default:
                        mensaje = "Se ha producido un error al intentar eliminar la direccion zonal.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar la direccion zonal.";
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
            string nom_grilla = "direccionZonal";
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "direccionZonal":
                    GridView1.Columns.RemoveAt(4);
                    grilla = GridView1;
                    ngrilla = "direccionZonal.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            DireccionZonal direccionZonal = new DireccionZonal();
            direccionZonal.codDirZonal = CodigoDireccionZonal.Text;
            direccionZonal.nombreDirZonal = nombre.Text;
            if (EsCentral.SelectedItem.Value != null && EsCentral.SelectedItem.Value.Equals("1"))
                direccionZonal.esCentral = true;
            else
                direccionZonal.esCentral = false;

            if (direccionZonal != null)
            {
                List<DireccionZonal> dt = mantenedorGeneralService.listarDireccionZonal(direccionZonal);

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