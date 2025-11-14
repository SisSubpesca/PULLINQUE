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
    public partial class capitaniaPuerto : System.Web.UI.Page
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
                CargaCombobox("AplicaDistanciaColector");

                // Cargamos la grilla
                CargaGrilla();
            };
        }

        private void CargaCombobox(string combobox)
        {
            switch (combobox)
            {
                case "AplicaDistanciaColector":

                    // Cargamos el combobox: AplicaDistanciaColector
                    AplicaDistanciaColector.Items.Clear();
                    AplicaDistanciaColector.DataBind();
                    AplicaDistanciaColector.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaDistanciaColector.Items.Insert(0, new ListItem("No", "0"));
                    AplicaDistanciaColector.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
            }
        }

        // ACCIONES DE BOTONES
        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            CapitaniaDePuerto capitaniaDePuerto = new CapitaniaDePuerto();
            capitaniaDePuerto.codigo = Convert.ToInt32(codigo.Text);
            capitaniaDePuerto.nombreCapitaniaDePuerto = nombre.Text.Trim();
            capitaniaDePuerto.requiereCert = Convert.ToInt32(AplicaDistanciaColector.SelectedItem.Value);

            if (capitaniaDePuerto != null)
            {
                DataTable dt = mantenedorGeneralService.guardarCapitaniaPuerto(capitaniaDePuerto);

                try
                {
                    string msg = Convert.ToString(dt.Rows[0]["msg"]);
                    if (msg == "OK")
                    {
                        mensaje = "La capitanía de puerto '" + capitaniaDePuerto.nombreCapitaniaDePuerto + "' ha sido creada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        GridView1.EditIndex = -1;
                        GridView1.PageIndex = 0;

                        codigo.Text = "";
                        nombre.Text = "";
                        AplicaDistanciaColector.SelectedValue = "-1";

                        CargaGrilla();
                    }
                    else
                    {
                        mensaje = "La capitanía de puerto '" + capitaniaDePuerto.nombreCapitaniaDePuerto + "' ya existe.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    };
                }
                catch
                {
                    mensaje = "Se ha producido un error al intentar crear la capitanía de puerto.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };


            }
            else
            {
                mensaje = "Para agregar debe ingresar los datos de la capitanía de puerto.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            // Iniciamos la query de búsqueda y cargamos la grilla
            CapitaniaDePuerto capitaniaDePuertoFiltro = new CapitaniaDePuerto();
                        
            if (!codigo.Text.Equals(""))
                capitaniaDePuertoFiltro.codigo = Convert.ToInt32(codigo.Text);

            capitaniaDePuertoFiltro.nombreCapitaniaDePuerto = nombre.Text;
            capitaniaDePuertoFiltro.requiereCert = Convert.ToInt32(AplicaDistanciaColector.SelectedItem.Value);

            List<CapitaniaDePuerto> dt = mantenedorGeneralService.listarCapitaniaPuerto(capitaniaDePuertoFiltro);
            
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
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar la capitanía de puerto " + DataBinder.Eval(e.Row.DataItem, "nombreCapitaniaDePuerto") + "?')");
                    boton_eliminar.Visible = true;
                };
            };

            if (GridView1.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
            {
                /* Aplica Certificado de Distancia */
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
            TextBox codigo = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geCodigo");
            TextBox nombre = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geNombre");
            DropDownList requiereCertificado = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry");
            
            if (codigo.Text != "" && nombre.Text != "")
            {
                id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                Agregar.Enabled = true;
                Update(id, Convert.ToInt32(codigo.Text), nombre.Text, requiereCertificado.SelectedItem.Value);
            }
            else
            {
                Content_msgGrilla.Visible = true;
                msgGrilla.Text = "Para modificar debe ingresar la capitanía de puerto";
            };
            CargaGrilla();

        }
        
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Agregar.Enabled = true;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void Update(int id, int codigo, string nombre, string requiereCertificado)
        {
            CapitaniaDePuerto capitaniaPuerto = new CapitaniaDePuerto();
            capitaniaPuerto.idCapitaDePuerto = id;
            capitaniaPuerto.codigo = codigo;
            capitaniaPuerto.nombreCapitaniaDePuerto = nombre.Trim();
            capitaniaPuerto.requiereCert = Convert.ToInt32(requiereCertificado);

            DataTable dt = mantenedorGeneralService.actualizarCapitaniaPuerto(capitaniaPuerto);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);
                if (msg == "OK")
                {
                    mensaje = "La capitania de puerto con ID:" + id + " ha sido actualizada.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    GridView1.EditIndex = -1;
                }
                else
                {
                    mensaje = "La capitanía de puerto '" + nombre + "' ya existe.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar actualizar la capitania de puerto.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void Delete(int id)
        {
            DataTable dt = mantenedorGeneralService.eliminarCapitaniaPuerto(id);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);

                switch (msg)
                {
                    case "OK":
                        mensaje = "La capitania de puerto ha sido eliminada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        break;
                    case "En uso":
                        mensaje = "La capitania de puerto que intenta borrar está actualmente en uso.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                    default:
                        mensaje = "Se ha producido un error al intentar eliminar la capitania de puerto.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar la capitania de puerto.";
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
            string nom_grilla = "capitaniaPuerto";
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "capitaniaPuerto":
                    GridView1.Columns.RemoveAt(4);
                    grilla = GridView1;
                    ngrilla = "capitaniaPuerto.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            CapitaniaDePuerto capitaniaDePuertoFiltro = new CapitaniaDePuerto();
            
            if (!codigo.Text.Equals(""))
                capitaniaDePuertoFiltro.codigo = Convert.ToInt32(codigo.Text);

            capitaniaDePuertoFiltro.nombreCapitaniaDePuerto = nombre.Text;
            capitaniaDePuertoFiltro.requiereCert = Convert.ToInt32(AplicaDistanciaColector.SelectedItem.Value);

            if (capitaniaDePuertoFiltro != null)
            {
                List<CapitaniaDePuerto> dt = mantenedorGeneralService.listarCapitaniaPuerto(capitaniaDePuertoFiltro);

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