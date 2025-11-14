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
    public partial class etapaDeDesarrollo : System.Web.UI.Page
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

                // cargamos los Especie
                CargarCombobox("Especie");

                // cargamos los Grupo Especie
                CargarCombobox("GrupoEspecie");

                // cargamos los Estado
                CargarCombobox("Estado");

                // Cargamos la grilla
                CargaGrilla();
            };
        }

        private void CargarCombobox(string combobox)
        {
            switch (combobox)
            {
                case "Especie":
                    // Cargamos el combobox: Especie
                    Especie.Items.Clear();

                    Especies especieFiltro = new Especies();
                    especieFiltro.esExotica = -1;
                    especieFiltro.esExperimental = -1;

                    Especie.DataSource = mantenedorDA.ListarEspecieCultivo_Mantenedor(especieFiltro);
                    Especie.DataTextField = "especieNombreComun";
                    Especie.DataValueField = "id_especie";
                    Especie.DataBind();
                    Especie.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;


                case "GrupoEspecie":
                //    Cargamos el combobox: Grupo Especie
                    GrupoEspecie.Items.Clear();
                    
                    GrupoEspecie.DataSource = parametroGenericoDA.ListarRbGrupoEspecie(null);
                    GrupoEspecie.DataTextField = "descripcion";
                    GrupoEspecie.DataValueField = "id";

                    GrupoEspecie.DataBind();
                    GrupoEspecie.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "Estado":

                    // Cargamos el combobox: Estado
                    estado.Items.Clear();
                    estado.Items.Add(new ListItem("Vigente", Convert.ToString(1)));
                    estado.Items.Add(new ListItem("No Vigente", Convert.ToString(0)));
                    estado.DataBind();
                    estado.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
            }
        }

        // ACCIONES DE BOTONES
        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            EtapaCultivo etapaCultivo = new EtapaCultivo();
            
            etapaCultivo.especie = new Especies();
            etapaCultivo.especie.id_especie = Convert.ToInt32(Especie.SelectedItem.Value);

            etapaCultivo.grupoEspecie = new ParametroGenerico();
            etapaCultivo.grupoEspecie.id = Convert.ToInt32(GrupoEspecie.SelectedItem.Value);

            etapaCultivo.codigo = Convert.ToInt32(codigoEtapaDesarrollo.Text);
            etapaCultivo.nombreEtapaDesarrollo = nombre.Text;


            bool permiteGuardarEtapaDesarrollo = false;
            if (etapaCultivo.especie.id_especie > 0 && etapaCultivo.grupoEspecie.id < 1) {
                permiteGuardarEtapaDesarrollo = true;
            }
            else if (etapaCultivo.especie.id_especie < 1 && etapaCultivo.grupoEspecie.id > 0)
            {
                permiteGuardarEtapaDesarrollo = true;
            }

            bool registroDuplicado = false;
            if (permiteGuardarEtapaDesarrollo)
            {
                List<EtapaCultivo> dt = mantenedorGeneralService.listarEtapaCultivo(etapaCultivo);
                if(dt != null && dt.Count() > 0)
                {
                    permiteGuardarEtapaDesarrollo = false;
                    registroDuplicado = true;
                }
            }

            //Se deja bajo la validación de registro duplicado ya que el estado no se debe considerar para la validación
            etapaCultivo.estado = new ParametroGenerico();
            etapaCultivo.estado.id = Convert.ToInt32(estado.SelectedItem.Value);
        

            if (etapaCultivo != null)
            {
                if (permiteGuardarEtapaDesarrollo)
                {

                    DataTable dt = mantenedorGeneralService.guardarEtapaDeDesarrollo(etapaCultivo);

                    try
                    {
                        string msg = Convert.ToString(dt.Rows[0]["msg"]);
                        if (msg == "OK")
                        {
                            mensaje = "La etapa de cultivo '" + etapaCultivo.nombreEtapaDesarrollo + "' ha sido creada.";
                            Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                            GridView1.EditIndex = -1;
                            GridView1.PageIndex = 0;

                            Especie.SelectedValue = "-1";
                            codigoEtapaDesarrollo.Text = "";
                            nombre.Text = "";
                            estado.SelectedValue = "-1";
                            GrupoEspecie.SelectedValue = "-1";

                            CargaGrilla();
                        }
                        else
                        {
                            mensaje = "La etapa de cultivo '" + etapaCultivo.nombreEtapaDesarrollo + "' ya existe.";
                            Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        };
                    }
                    catch
                    {
                        mensaje = "Se ha producido un error al intentar crear la etapa de cultivo.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    };

                }
                else 
                {

                    if (registroDuplicado)
                    {
                        mensaje = "Registro ya ingresado.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    }
                    else 
                    {
                        mensaje = "Debe seleccionar Especie de Desarrollo o Grupo.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    }
                    
                }
            }
            else
            {
                mensaje = "Para agregar debe ingresar los datos de la etapa de cultivo.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            //// Iniciamos la query de búsqueda y cargamos la grilla
            ////EtapaCultivo etapaCultivoFiltro = new EtapaCultivo();

            //ParametroGenerico etapaCultivo = new ParametroGenerico();
            //etapaCultivo.descripcion = nombre.Text;

            EtapaCultivo etapaCultivo = new EtapaCultivo();
            
            etapaCultivo.especie = new Especies();
            etapaCultivo.especie.id_especie = Convert.ToInt32(Especie.SelectedItem.Value);

            etapaCultivo.grupoEspecie = new ParametroGenerico();
            etapaCultivo.grupoEspecie.id = Convert.ToInt32(GrupoEspecie.SelectedItem.Value);

            if (codigoEtapaDesarrollo != null && !codigoEtapaDesarrollo.Text.Equals(""))
            {
                etapaCultivo.codigo = Convert.ToInt32(codigoEtapaDesarrollo.Text);
            }
            
            etapaCultivo.nombreEtapaDesarrollo = nombre.Text;
            etapaCultivo.estado = new ParametroGenerico();
            etapaCultivo.estado.id = Convert.ToInt32(estado.SelectedItem.Value);

            List<EtapaCultivo> dt = mantenedorGeneralService.listarEtapaCultivo(etapaCultivo);

            if (dt != null)
            {
                int num_registros = 0;
                num_registros = dt.Count;
                
                ExportarGrilla.Visible = true;
            }

            else {
                ExportarGrilla.Visible = false;
            }

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
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar la etapa de cultivo " + DataBinder.Eval(e.Row.DataItem, "nombreEtapaDesarrollo") + "?')");
                    boton_eliminar.Visible = true;
                };
            };

            if (GridView1.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
            {
                /* Especie */
                DropDownList ddllist = ((DropDownList)e.Row.FindControl("ddleditCountry"));
                var hdnCountryName = ((HiddenField)e.Row.FindControl("hdnCountry"));
                ddllist.AppendDataBoundItems = true;

                Especies especieFiltro = new Especies();
                especieFiltro.esExotica = -1;
                especieFiltro.esExperimental = -1;

                ddllist.DataSource = mantenedorDA.ListarEspecieCultivo_Mantenedor(especieFiltro);
                ddllist.DataTextField = "especieNombreComun";
                ddllist.DataValueField = "id_especie";

                ddllist.DataBind();
                ddllist.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnCountryName.Value != null && !hdnCountryName.Value.Equals(""))
                {
                    ddllist.Items.FindByText(hdnCountryName.Value).Selected = true;
                }


                //GrupoEspecie
                DropDownList ddllist2 = ((DropDownList)e.Row.FindControl("ddleditCountry2"));
                var hdnCountryName2 = ((HiddenField)e.Row.FindControl("hdnCountry2"));
                ddllist2.AppendDataBoundItems = true;

                ddllist2.DataSource = parametroGenericoDA.ListarRbGrupoEspecie(null);
                ddllist2.DataTextField = "descripcion";
                ddllist2.DataValueField = "id";


                ddllist2.DataBind();
                ddllist2.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnCountryName2.Value != null && !hdnCountryName2.Value.Equals(""))
                {
                    ddllist2.Items.FindByText(hdnCountryName2.Value).Selected = true;
                }

                /* Estado */
                DropDownList ddllist10 = ((DropDownList)e.Row.FindControl("ddleditCountry10"));
                var hdnCountryName10 = ((HiddenField)e.Row.FindControl("hdnCountry10"));
                ddllist10.AppendDataBoundItems = true;

                ddllist10.Items.Add(new ListItem("Vigente", Convert.ToString(rbEstadosGenerales.VIGENTE)));
                ddllist10.Items.Add(new ListItem("No Vigente", Convert.ToString(rbEstadosGenerales.NO_VIGENTE)));

                ddllist10.DataBind();
                ddllist10.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnCountryName10.Value != null && !hdnCountryName10.Value.Equals(""))
                {
                    ddllist10.Items.FindByText(hdnCountryName10.Value).Selected = true;
                }

                ///* Grupo Especie */
                //DropDownList ddllist11 = ((DropDownList)e.Row.FindControl("ddleditCountry11"));
                //var hdnCountryName11 = ((HiddenField)e.Row.FindControl("hdnCountry11"));
                //ddllist11.AppendDataBoundItems = true;

                //GrupoEspecie grupoEspecieFiltro = new GrupoEspecie();
                //grupoEspecieFiltro.cultivoFiltro = -1;

                ////ddllist11.DataSource = mantenedorDA.ListarGrupoEspecie_Mantenedor(grupoEspecieFiltro);
                ////ddllist11.DataTextField = "grupoEspecie";
                ////ddllist11.DataValueField = "id_grupoEspecie";

                //ddllist11.DataSource = parametroGenericoDA.ListarGrupoEspecieInformativo(0);
                //ddllist11.DataTextField = "descripcion";
                //ddllist11.DataValueField = "id";

                //ddllist11.DataBind();
                //ddllist11.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                //if (hdnCountryName11.Value != null && !hdnCountryName11.Value.Equals(""))
                //{
                //    ddllist11.Items.FindByText(hdnCountryName11.Value).Selected = true;
                //}
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Content_msgGrilla.Visible = false;

            string[] arg = new string[3];
            arg = e.CommandArgument.ToString().Split(';');

            switch (e.CommandName)
            {
                case "Eliminar":
                    //id = Convert.ToInt32(e.CommandArgument);
                    //Delete(id);

                    int codigoEtapa = Convert.ToInt32(arg[0]);
                    int idEspecie = Convert.ToInt32(arg[1]);
                    int idGrupoEsp = Convert.ToInt32(arg[2]);

                    Delete(codigoEtapa, idEspecie, idGrupoEsp);

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

            DropDownList especie = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry");
            DropDownList grupoEspecie = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry2");
            DropDownList estado = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry10");


            TextBox codigo = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geCodigoEtapaDesarrollo");
            TextBox nombre = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geNombre");
            
            

            int idEspecie = Convert.ToInt32(especie.SelectedItem.Value);
            int idGrupoEspecie = Convert.ToInt32(grupoEspecie.SelectedItem.Value);

            bool permiteActualizarEtapaDesarrollo = false;
            if (idEspecie > 0 && idGrupoEspecie < 1)
            {
                permiteActualizarEtapaDesarrollo = true;
            }
            else if (idEspecie < 1 && idGrupoEspecie > 0)
            {
                permiteActualizarEtapaDesarrollo = true;
            }


            if (permiteActualizarEtapaDesarrollo)
            {
                if (codigo.Text != null && nombre.Text != "")
                {
                    id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                    Agregar.Enabled = true;
                    Update(id, especie.SelectedValue, grupoEspecie.SelectedValue, codigo.Text, nombre.Text, estado.SelectedValue);
                }
                else
                {
                    Content_msgGrilla.Visible = true;
                    msgGrilla.Text = "Para modificar debe ingresar la etapa de cultivo.";
                };
            }
            else {
                Content_msgGrilla.Visible = true;
                msgGrilla.Text = "Para modificar debe seleccionar especie o grupo";
            }
            CargaGrilla();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Agregar.Enabled = true;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void Update(int id, string especie, string grupoEspecie, string codigo, string nombre, string estado)
        {
            EtapaCultivo etapaCultivo = new EtapaCultivo();
            etapaCultivo.id_etapaDesarrollo = id;

            if (especie != null && !especie.Equals(""))
            {
                etapaCultivo.especie = new Especies();
                etapaCultivo.especie.id_especie = Convert.ToInt32(especie);
            }

            if (codigo != null && !codigo.Equals(""))
            {
                etapaCultivo.codigo = Convert.ToInt32(codigo);
                etapaCultivo.nombreEtapaDesarrollo = nombre;
            }

            if (estado != null && !estado.Equals(""))
            {
                etapaCultivo.estado = new ParametroGenerico();
                etapaCultivo.estado.id = Convert.ToInt32(estado);
            }

            if (grupoEspecie != null && !grupoEspecie.Equals(""))
            {
                etapaCultivo.grupoEspecie = new ParametroGenerico();
                etapaCultivo.grupoEspecie.id = Convert.ToInt32(grupoEspecie);
            }

            DataTable dt = mantenedorGeneralService.actualizarEtapaCultivo(etapaCultivo);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);
                if (msg == "OK")
                {
                    mensaje = "La etapa de cultivo con ID:" + id + " ha sido actualizada.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    GridView1.EditIndex = -1;
                }
                else
                {
                    mensaje = "La etapa de cultivo '" + nombre + "' ya existe.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar actualizar la etapa de cultivo.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void Delete(int idEtapaDesarrollo, int idEspecie, int idGrupoEsp)
        {
            DataTable dt = mantenedorGeneralService.eliminarEtapaCultivo(idEtapaDesarrollo, idEspecie, idGrupoEsp);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);

                switch (msg)
                {
                    case "OK":
                        mensaje = "La etapa de cultivo ha sido eliminada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        break;
                    case "En uso":
                        mensaje = "La etapa de cultivo que intenta borrar está actualmente en uso.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                    default:
                        mensaje = "Se ha producido un error al intentar eliminar la etapa de cultivo.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar la etapa de cultivo.";
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
            string nom_grilla = "etapaDeCultivo";
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "etapaDeCultivo":
                    GridView1.Columns.RemoveAt(2);
                    grilla = GridView1;
                    ngrilla = "etapaDeCultivo.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            //EtapaCultivo etapaCultivo = new EtapaCultivo();
            //etapaCultivo.nombreEtapaDesarrollo = nombre.Text;

            //ParametroGenerico etapaCultivo = new ParametroGenerico();
            //etapaCultivo.descripcion = nombre.Text;

            EtapaCultivo etapaCultivo = new EtapaCultivo();
            etapaCultivo.especie = new Especies();
            etapaCultivo.especie.id_especie = Convert.ToInt32(Especie.SelectedItem.Value);

            etapaCultivo.grupoEspecie = new ParametroGenerico();
            etapaCultivo.grupoEspecie.id = Convert.ToInt32(GrupoEspecie.SelectedItem.Value);

            if (codigoEtapaDesarrollo != null && !codigoEtapaDesarrollo.Text.Equals(""))
            {
                etapaCultivo.codigo = Convert.ToInt32(codigoEtapaDesarrollo.Text);

            }
            etapaCultivo.nombreEtapaDesarrollo = nombre.Text;
            etapaCultivo.estado = new ParametroGenerico();
            etapaCultivo.estado.id = Convert.ToInt32(estado.SelectedItem.Value);

            if (etapaCultivo != null)
            {
                List<EtapaCultivo> dt = mantenedorGeneralService.listarEtapaCultivo(etapaCultivo);
                
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