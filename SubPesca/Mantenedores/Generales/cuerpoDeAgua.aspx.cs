using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.common;
using System.Web.UI.HtmlControls;
using System.Data;
using Datos.Entidades;

namespace SubPesca.Mantenedores.Generales
{
    public partial class cuerpoDeAgua : System.Web.UI.Page
    {

        MantenedorGeneralService mantenedorGeneralService = new MantenedorGeneralService();

        RegionDA regionDA = new LogicaNegocio.cl.subpesca.rb.common.RegionDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        TipoDA tipoDa = new TipoDA();


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

        protected void Page_Load(object sender, EventArgs e)
        {
            // PAGE LOAD
            if (!Page.IsPostBack)
            {
                Initialize_Form();

                CargaGrilla();
            }
        }

        private void CargaGrilla()
        {
            // Iniciamos la query de búsqueda y cargamos la grilla
            CuerpoDeAgua cuerpoDeAgua = new CuerpoDeAgua();
            cuerpoDeAgua.tipoCuerpoAgua = new ParametroGenerico();
            cuerpoDeAgua.tipoCuerpoAgua.id = Convert.ToInt32(TipoCuerpoAgua.SelectedItem.Value);
            cuerpoDeAgua.region = new Region();
            cuerpoDeAgua.region.id_region = Convert.ToInt32(Region.SelectedItem.Value);
            cuerpoDeAgua.comuna = new Comuna();
            cuerpoDeAgua.comuna.id_comuna = Convert.ToInt32(Comuna.SelectedItem.Value);
            cuerpoDeAgua.nombreCuerpoAgua = NombreCuerpoAgua.Text;

            List<CuerpoDeAgua> dt = mantenedorGeneralService.listarCuerpoDeAgua(cuerpoDeAgua);

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

        protected void Initialize_Form()
        {
            Initialize_Comboboxs();
        }

        /**
         * Método que inicializa los combobox del formulario. 
         */
        protected void Initialize_Comboboxs()
        {
            Carga_Combobox("TipoCuerpoAgua");
            TipoCuerpoAgua.SelectedValue = "-1";
            
            Carga_Combobox("Region");
            Region.SelectedValue = "-1";
            
            Carga_Combobox("Comuna");
            Comuna.SelectedValue = "-1";
        }

        /**
         * Método que carga los combobox del formulario completo. 
         */
        protected void Carga_Combobox(string combobox)
        {
            
            switch (combobox)
            {
                case "Region":
                    // Cargamos el combobox: Regiones
                    Region.Items.Clear();
                    Region.DataSource = regionDA.ListarRegion(0);
                    Region.DataTextField = "Region";
                    Region.DataValueField = "IdRegion";
                    Region.DataBind();
                    Region.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "Comuna":
                    // Cargamos el combobox: Comuna
                    Comuna.Items.Clear();
                    if (Convert.ToInt32(Region.SelectedValue) > 0)
                    {
                        Comuna.DataSource = parametroGenericoDA.ListarComunaDataTable(Convert.ToInt32(Region.SelectedValue), 0);
                        Comuna.DataTextField = "Comuna";
                        Comuna.DataValueField = "IdComuna";
                        Comuna.DataBind();
                    };
                    Comuna.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
               
                case "TipoCuerpoAgua":
                    // Cargamos el combobox: Tipo Barrio
                    TipoCuerpoAgua.Items.Clear();
                    TipoCuerpoAgua.DataSource = tipoDa.ListarTipo("TIPO_CUERPO_AGUA");
                    TipoCuerpoAgua.DataTextField = "descripcion";
                    TipoCuerpoAgua.DataValueField = "id";
                    TipoCuerpoAgua.DataBind();
                    TipoCuerpoAgua.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
            }
            
        }

        protected void Region_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("Comuna");
        }

        protected void geRegion_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.Rows[GridView1.EditIndex];
            DropDownList ddllist3 = ((DropDownList)row.FindControl("geComuna"));
            DropDownList ddllistRegion = ((DropDownList)row.FindControl("geRegion"));
            
            ddllist3.Items.Clear();
            
            var hdnCountryName3 = ((HiddenField)row.FindControl("hdnCountry3"));
            ddllist3.AppendDataBoundItems = true;

            ddllist3.DataSource = parametroGenericoDA.ListarComunaDataTable(Convert.ToInt32(ddllistRegion.SelectedValue), 0);
            ddllist3.DataTextField = "Comuna";
            ddllist3.DataValueField = "IdComuna";
            ddllist3.DataBind();
            ddllist3.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

            upd2.Update();
        }

        // ACCIONES DE BOTONES
        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            CuerpoDeAgua cuerpoDeAgua = new CuerpoDeAgua();
            cuerpoDeAgua.tipoCuerpoAgua = new ParametroGenerico();
            cuerpoDeAgua.tipoCuerpoAgua.id = Convert.ToInt32(TipoCuerpoAgua.SelectedItem.Value);
            cuerpoDeAgua.region = new Region();
            cuerpoDeAgua.region.id_region = Convert.ToInt32(Region.SelectedItem.Value);
            cuerpoDeAgua.comuna = new Comuna();
            cuerpoDeAgua.comuna.id_comuna = Convert.ToInt32(Comuna.SelectedItem.Value);
            cuerpoDeAgua.nombreCuerpoAgua = NombreCuerpoAgua.Text.Trim();

            if (cuerpoDeAgua != null)
            {
                DataTable dt = mantenedorGeneralService.guardarCuerpoDeAgua(cuerpoDeAgua);

                try
                {
                    string msg = Convert.ToString(dt.Rows[0]["msg"]);
                    if (msg == "OK")
                    {
                        mensaje = "El cuerpo de agua '" + cuerpoDeAgua.nombreCuerpoAgua + "' ha sido creado.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        GridView1.EditIndex = -1;
                        GridView1.PageIndex = 0;

                        TipoCuerpoAgua.SelectedValue = "-1";
                        Region.SelectedValue = "-1";
                        Comuna.SelectedValue = "-1";
                        NombreCuerpoAgua.Text = "";

                        CargaGrilla();
                    }
                    else
                    {
                        mensaje = "El cuerpo de agua '" + cuerpoDeAgua.nombreCuerpoAgua + "' ya existe.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    };
                }
                catch
                {
                    mensaje = "Se ha producido un error al intentar crear el cuerpo de agua.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
                
            }
            else
            {
                mensaje = "Para agregar debe ingresar los datos del cuerpo de agua.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
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
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar el cuerpo de agua " + DataBinder.Eval(e.Row.DataItem, "nombreCuerpoAgua") + "?')");
                    boton_eliminar.Visible = true;
                };
            };


            if (GridView1.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddllist1 = ((DropDownList)e.Row.FindControl("geTipoCuerpoAgua"));
                var hdnCountryName = ((HiddenField)e.Row.FindControl("hdnCountry"));
                ddllist1.AppendDataBoundItems = true;

                ddllist1.DataSource = tipoDa.ListarTipo("TIPO_CUERPO_AGUA");
                ddllist1.DataTextField = "descripcion";
                ddllist1.DataValueField = "id";
                ddllist1.DataBind();
                ddllist1.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnCountryName.Value != null && !hdnCountryName.Value.Equals(""))
                {
                    ddllist1.Items.FindByText(hdnCountryName.Value).Selected = true;
                }

                DropDownList ddllist2 = ((DropDownList)e.Row.FindControl("geRegion"));
                var hdnCountryName2 = ((HiddenField)e.Row.FindControl("hdnCountry2"));
                ddllist2.AppendDataBoundItems = true;

                ddllist2.DataSource = regionDA.ListarRegion(0);
                ddllist2.DataTextField = "Region";
                ddllist2.DataValueField = "IdRegion";
                ddllist2.DataBind();
                ddllist2.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnCountryName2.Value != null && !hdnCountryName2.Value.Equals(""))
                {
                    ddllist2.Items.FindByText(hdnCountryName2.Value).Selected = true;
                }

                DropDownList ddllist3 = ((DropDownList)e.Row.FindControl("geComuna"));
                var hdnCountryName3 = ((HiddenField)e.Row.FindControl("hdnCountry3"));
                ddllist3.AppendDataBoundItems = true;

                ddllist3.DataSource = parametroGenericoDA.ListarComunaDataTable(Convert.ToInt32(Region.SelectedValue), 0);
                ddllist3.DataTextField = "Comuna";
                ddllist3.DataValueField = "IdComuna";
                ddllist3.DataBind();
                ddllist3.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnCountryName3.Value != null && !hdnCountryName3.Value.Equals(""))
                {
                    ddllist3.Items.FindByText(hdnCountryName3.Value).Selected = true;
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

            TextBox nombreCuerpoAgua = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geNombre");
            DropDownList tipoCuerpoAgua = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("geTipoCuerpoAgua");
            DropDownList region = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("geRegion");
            DropDownList comuna = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("geComuna");

            if (nombreCuerpoAgua.Text != "" && tipoCuerpoAgua.SelectedValue != "-1" && region.SelectedValue != "-1" && comuna.SelectedValue != "-1")
            {
                id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                Agregar.Enabled = true;
                Update(id, nombreCuerpoAgua.Text, tipoCuerpoAgua.SelectedItem.Value, region.SelectedItem.Value, comuna.SelectedItem.Value);
            }
            else
            {
                Content_msgGrilla.Visible = true;
                msgGrilla.Text = "Para modificar debe ingresar un cuerpo de agua.";
            };
            CargaGrilla();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Agregar.Enabled = true;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void Update(int id, string nombre, string tipoCuerpoAgua, string region, string comuna)
        {
            CuerpoDeAgua cuerpoDeAgua = new CuerpoDeAgua();
            cuerpoDeAgua.idCuerpoDeAgua = id;
            cuerpoDeAgua.nombreCuerpoAgua = nombre.Trim();
            cuerpoDeAgua.tipoCuerpoAgua = new ParametroGenerico(Convert.ToInt32(tipoCuerpoAgua));
            cuerpoDeAgua.region = new Datos.Entidades.Region();
            cuerpoDeAgua.region.id_region = Convert.ToInt32(region);
            cuerpoDeAgua.comuna = new Datos.Entidades.Comuna();
            cuerpoDeAgua.comuna.id_comuna = Convert.ToInt32(comuna);


            DataTable dt = mantenedorGeneralService.actualizarCuerpoDeAgua(cuerpoDeAgua);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);
                if (msg == "OK")
                {
                    mensaje = "El cuerpo de agua con ID:" + id + " ha sido actualizado.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    GridView1.EditIndex = -1;
                }
                else
                {
                    mensaje = "El cuerpo de agua '" + nombre + "' ya existe.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar actualizar el cuerpo de agua.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void Delete(int id)
        {
            DataTable dt = mantenedorGeneralService.eliminarCuerpoDeAgua(id);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);

                switch (msg)
                {
                    case "OK":
                        mensaje = "El cuerpo de agua ha sido eliminado.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        break;
                    case "En uso":
                        mensaje = "El cuerpo de agua que intenta borrar está actualmente en uso.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                    default:
                        mensaje = "Se ha producido un error al intentar eliminar el cuerpo de agua.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar el cuerpo de agua.";
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
            //GridView grilla = new GridView();
            //string nom_grilla = "cuerpoDeAgua";
            //string ngrilla = "";

            //CargaGrilla();

            //switch (nom_grilla)
            //{
            //    case "cuerpoDeAgua":
            //        GridView1.Columns.RemoveAt(5);
            //        grilla = GridView1;
            //        ngrilla = "cuerpoDeAgua.xls";
            //        break;

            //};
            //grilla.AllowPaging = false;
            //grilla.DataBind();
            //SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);

            GridView grilla = new GridView();
            CargaGrilla();

            GridView1.Columns.RemoveAt(5);
            grilla = GridView1;
            grilla.AllowPaging = false;
            grilla.DataBind();

            SubPesca.Utilidades.GridViewExportUtil.Export("cuerpoDeAgua.xls", grilla);
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            CuerpoDeAgua cuerpoDeAgua = new CuerpoDeAgua();
            cuerpoDeAgua.tipoCuerpoAgua = new ParametroGenerico();
            cuerpoDeAgua.tipoCuerpoAgua.id = Convert.ToInt32(TipoCuerpoAgua.SelectedItem.Value);
            cuerpoDeAgua.region = new Region();
            cuerpoDeAgua.region.id_region = Convert.ToInt32(Region.SelectedItem.Value);
            cuerpoDeAgua.comuna = new Comuna();
            cuerpoDeAgua.comuna.id_comuna = Convert.ToInt32(Comuna.SelectedItem.Value);
            cuerpoDeAgua.nombreCuerpoAgua = NombreCuerpoAgua.Text;

            if (cuerpoDeAgua != null)
            {
                List<CuerpoDeAgua> dt = mantenedorGeneralService.listarCuerpoDeAgua(cuerpoDeAgua);

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