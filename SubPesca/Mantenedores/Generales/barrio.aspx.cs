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
using Datos.Contantes;

namespace SubPesca.Mantenedores.Generales
{
    public partial class barrio : System.Web.UI.Page
    {
        Datos.Entidades.Usuario usuarios = new Datos.Entidades.Usuario();
        RegionDA regionDA = new RegionDA();
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

                //Se carga la lista de regiones
                CargarCombobox("Region");
                CargarCombobox("Macrozona");
                CargarCombobox("TipoBarrio");

                // Cargamos la grilla
                CargaGrilla();
            };
        }

        private void CargarCombobox(string combobox)
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
                case "Macrozona":
                    Macrozona.Items.Clear();
                    if (Convert.ToInt32(Region.SelectedValue) > 0)
                    {
                        Macrozona macrozonaFiltro = new Datos.Entidades.Macrozona();
                        macrozonaFiltro.id_region = Convert.ToInt32(Region.SelectedValue);
                        
                        //Macrozona.DataSource = mantenedorDA.ListarMacrozona_Mantenedor(0, Convert.ToInt32(Region.SelectedValue));
                        Macrozona.DataSource = mantenedorDA.ListarMacrozona_Mantenedor(macrozonaFiltro);
                        Macrozona.DataTextField = "macrozona";
                        Macrozona.DataValueField = "id_macrozona";
                        Macrozona.DataBind();
                    }
                    Macrozona.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    Macrozona.Items.Add(new ListItem("SIN MACROZONA","9"));
                    break;

                case "TipoBarrio":
                    // Cargamos el combobox: Tipo Barrio
                    TipoBarrio.Items.Clear();
                    TipoBarrio.DataSource = mantenedorDA.ListarTipo_Mantenedor(new ParametroGenerico(0,null,"TIPO_BARRIO"));
                    TipoBarrio.DataTextField = "descripcion";
                    TipoBarrio.DataValueField = "id";
                    TipoBarrio.DataBind();
                    TipoBarrio.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
            }
        }

        // ACCIONES DE BOTONES
        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            Barrio barrio = new Barrio();
            barrio.id_region = Convert.ToInt32(Region.SelectedItem.Value);
            barrio.barrio = nombre.Text.Trim();
            barrio.id_macrozona = Convert.ToInt32(Macrozona.SelectedItem.Value);
            barrio.tipo_barrio = new ParametroGenerico(Convert.ToInt32(TipoBarrio.SelectedItem.Value));
            barrio.vigencia = new ParametroGenerico(rbEstadosGenerales.VIGENTE); //siempre entra como vigente

            if (barrio != null)
            {
                DataTable dt = mantenedorGeneralService.guardarBarrio(barrio);

                try
                {
                    string msg = Convert.ToString(dt.Rows[0]["msg"]);
                    if (msg == "OK")
                    {
                        mensaje = "El barrio '" + barrio.barrio + "' ha sido creado.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        GridView1.EditIndex = -1;
                        GridView1.PageIndex = 0;

                        nombre.Text = "";
                        Region.SelectedValue = "-1";
                        Macrozona.SelectedValue = "-1";
                        TipoBarrio.SelectedValue = "-1";

                        CargaGrilla();
                    }
                    else
                    {
                        mensaje = "El barrio '" + barrio.barrio + "' ya existe";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    };
                }
                catch
                {
                    mensaje = "Se ha producido un error al intentar crear el barrio.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };

            }
            else
            {
                mensaje = "Para agregar debe ingresar los datos del barrio.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            // Iniciamos la query de búsqueda y cargamos la grilla
            Barrio barrio = new Barrio();
            barrio.id_region = Convert.ToInt32(Region.SelectedItem.Value);
            barrio.barrio = nombre.Text;
            barrio.id_macrozona = Convert.ToInt32(Macrozona.SelectedItem.Value);
            barrio.tipo_barrio = new ParametroGenerico(Convert.ToInt32(TipoBarrio.SelectedItem.Value));

            List<Barrio> dt = mantenedorGeneralService.listarBarrio(barrio);

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
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar el barrio " + DataBinder.Eval(e.Row.DataItem, "barrio") + "?')");
                    boton_eliminar.Visible = true;
                };


                String idEstadoVigencia = "";

                try { 
                    idEstadoVigencia = ((Label)e.Row.FindControl("Vigencia")).Text;
                }catch{
                    idEstadoVigencia = ((Label)e.Row.FindControl("VigenciaEdit")).Text;
                }

                //No Vigente
                ImageButton boton_noVigente = (ImageButton)e.Row.FindControl("gNoVigente");
                if (boton_noVigente != null)
                {
                    boton_noVigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea dejar no vigente este barrio?')");
                    if (Convert.ToInt32(idEstadoVigencia) == rbEstadosGenerales.VIGENTE)
                    {
                        boton_noVigente.Visible = true;
                    }

                };


                //Vigente
                ImageButton boton_vigente = (ImageButton)e.Row.FindControl("gVigente");
                if (boton_vigente != null)
                {
                    boton_vigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea dejar vigente este barrio?')");
                    if (Convert.ToInt32(idEstadoVigencia) == rbEstadosGenerales.NO_VIGENTE)
                    {
                        boton_vigente.Visible = true;
                    }

                };
            };

            if (GridView1.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddllist = ((DropDownList)e.Row.FindControl("ddleditCountry"));
                var hdnCountryName = ((HiddenField)e.Row.FindControl("hdnCountry"));
                ddllist.AppendDataBoundItems = true;

                ddllist.DataSource = regionDA.ListarRegion(0);
                ddllist.DataTextField = "Region";
                ddllist.DataValueField = "IdRegion";
                ddllist.DataBind();
                ddllist.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                ddllist.Items.FindByText(hdnCountryName.Value).Selected = true;


                DropDownList ddllist2 = ((DropDownList)e.Row.FindControl("ddleditCountry2"));
                var hdnCountryName2 = ((HiddenField)e.Row.FindControl("hdnCountry2"));
                ddllist2.AppendDataBoundItems = true;


                Macrozona macrozonaFiltro = new Datos.Entidades.Macrozona();
                macrozonaFiltro.id_region = Convert.ToInt32(Region.SelectedValue);

                //ddllist2.DataSource = mantenedorDA.ListarMacrozona_Mantenedor(0, Convert.ToInt32(Region.SelectedValue));
                ddllist2.DataSource = mantenedorDA.ListarMacrozona_Mantenedor(macrozonaFiltro);
                ddllist2.DataTextField = "macrozona";
                ddllist2.DataValueField = "id_macrozona";

                ddllist2.DataBind();
                ddllist2.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                ddllist2.Items.Add(new ListItem("SIN MACROZONA", "9"));
                ddllist2.Items.FindByText(hdnCountryName2.Value).Selected = true;



                DropDownList ddllist3 = ((DropDownList)e.Row.FindControl("ddleditCountry3"));
                var hdnCountryName3 = ((HiddenField)e.Row.FindControl("hdnCountry3"));
                ddllist3.AppendDataBoundItems = true;

                ddllist3.DataSource = mantenedorDA.ListarTipo_Mantenedor(new ParametroGenerico(0, null, "TIPO_BARRIO"));
                ddllist3.DataTextField = "descripcion";
                ddllist3.DataValueField = "id";

                ddllist3.DataBind();
                ddllist3.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                ddllist3.Items.FindByText(hdnCountryName3.Value).Selected = true;


              

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

                case "NoVigente":
                    id = Convert.ToInt32(e.CommandArgument);
                    PasaNoVigente(id);
                    GridView1.EditIndex = -1;
                    CargaGrilla();
                    break;

                case "Vigente":
                    id = Convert.ToInt32(e.CommandArgument);
                    PasarVigente(id);
                    GridView1.EditIndex = -1;
                    CargaGrilla();
                    break;
            };
        }


        protected void PasaNoVigente(int idBarrio)
        {
            bool resultado = mantenedorGeneralService.ActualizarVigenciaBarrio(idBarrio, rbEstadosGenerales.NO_VIGENTE);

            string mensaje = "";

            try
            {

                if (resultado)
                {
                    mensaje = "El barrio ha sido modificado.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                }
                else
                {
                    mensaje = "Se ha producido un error al intentar modificar el barrio.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                }
                    
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar modificar el barrio.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }


        protected void PasarVigente(int idBarrio)
        {
            bool resultado = mantenedorGeneralService.ActualizarVigenciaBarrio(idBarrio, rbEstadosGenerales.VIGENTE);

            string mensaje = "";

            try
            {
                if (resultado)
                {
                    mensaje = "El barrio ha sido modificado.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                }
                else
                {
                    mensaje = "Se ha producido un error al intentar modificar el barrio.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                }
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar modificar el barrio.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
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

            DropDownList region = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry"); ;
            TextBox nombre = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geNombre");
            DropDownList macrozona = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry2"); 
            DropDownList barrio = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry3");


            Label vigencia;
            
            vigencia = (Label)GridView1.Rows[e.RowIndex].FindControl("Vigencia");

            if (vigencia == null)            
            {
                vigencia = (Label)GridView1.Rows[e.RowIndex].FindControl("VigenciaEdit"); 
            }
            

            if (nombre.Text != "" && region.SelectedValue != "-1" && macrozona.SelectedValue != "-1" && barrio.SelectedValue != "-1")
            {
                id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                Agregar.Enabled = true;
                Update(id, nombre.Text, region.SelectedValue, macrozona.SelectedValue, barrio.SelectedValue, Convert.ToInt32(vigencia.Text));
            }
            else
            {
                Content_msgGrilla.Visible = true;
                msgGrilla.Text = "Para modificar debe ingresar un barrio";
            };
            CargaGrilla();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Agregar.Enabled = true;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void Update(int id, string nombre, string region, string macrozona, string tipoBarrio, int idVigencia)
        {
            Barrio barrio = new Barrio();
            barrio.id_barrio = id;
            barrio.barrio = nombre.Trim();
            barrio.id_region = Convert.ToInt32(region);
            barrio.id_macrozona = Convert.ToInt32(macrozona);
            barrio.tipo_barrio = new ParametroGenerico(Convert.ToInt32(tipoBarrio));
            barrio.vigencia = new ParametroGenerico(Convert.ToInt32(idVigencia));

            DataTable dt = mantenedorGeneralService.actualizarBarrio(barrio);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);
                if (msg == "OK")
                {
                    mensaje = "El barrio con ID:" + id + " ha sido actualizado.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    GridView1.EditIndex = -1;
                }
                else
                {
                    mensaje = "El barrio '" + nombre + "' ya existe.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar actualizar el barrio.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void Delete(int id)
        {
            DataTable dt = mantenedorGeneralService.eliminarBarrio(id);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);

                switch (msg)
                {
                    case "OK":
                        mensaje = "El barrio ha sido eliminado.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        break;
                    case "En uso":
                        mensaje = "El barrio que intenta borrar está actualmente en uso.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                    default:
                        mensaje = "Se ha producido un error al intentar eliminar el barrio.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar el barrio.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void Region_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCombobox("Macrozona");
        }

        protected void Volver_Click(object sender, EventArgs e)
        {
            string path = "~/Administrador/principal.aspx";
            Response.Redirect(path);
        }

        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();
            string nom_grilla = "barrio";
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "barrio":
                    GridView1.Columns.RemoveAt(5);
                    grilla = GridView1;
                    
                    ngrilla = "barrio.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            Barrio barrio = new Barrio();
            barrio.id_region = Convert.ToInt32(Region.SelectedItem.Value);
            barrio.barrio = nombre.Text;
            barrio.id_macrozona = Convert.ToInt32(Macrozona.SelectedItem.Value);
            barrio.tipo_barrio = new ParametroGenerico(Convert.ToInt32(TipoBarrio.SelectedItem.Value));
            
            if (barrio != null)
            {
                List<Barrio> dt = mantenedorGeneralService.listarBarrio(barrio);

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