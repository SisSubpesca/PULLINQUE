using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using System.Web.UI.HtmlControls;
using System.Data;
using Datos.Entidades;
using Datos.Contantes;
using Validaciones.cl.subpesca.rb.mantenedor;
using SubPesca.Utilidades;

namespace SubPesca.Mantenedores.Generales
{
    public partial class descasoACS : System.Web.UI.Page
    {
        Datos.Entidades.Usuario usuarios = new Datos.Entidades.Usuario();
        MantenedorDA mantenedorDA = new MantenedorDA();

        MantenedorGeneralService mantenedorGeneralService = new MantenedorGeneralService();
        MantenedorGeneralValidacion mantenedorGeneralValidacion = new MantenedorGeneralValidacion();

        protected void Page_Init() {

            //string script1 = "calendarioConHora('" + FechaRecepcion.ClientID + "','" + imgFechaRecepcion.ClientID + "');";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaRecepcion.ClientID, script1.ToString(), true);

            //string script2 = "calendarioConHora('" + FechaIngresoTramite.ClientID + "','" + imgFechaIngresoTramite.ClientID + "');";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaIngresoTramite.ClientID, script2.ToString(), true);

            //string script3 = "calendarioConHora('" + FechaInicioProduccion.ClientID + "','" + imgFechaInicioProduccion.ClientID + "');";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaInicioProduccion.ClientID, script3.ToString(), true);

            string script1 = "calendario('" + FechaRecepcion.ClientID + "','" + imgFechaRecepcion.ClientID + "');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaRecepcion.ClientID, script1.ToString(), true);

            string script2 = "calendario('" + FechaIngresoTramite.ClientID + "','" + imgFechaIngresoTramite.ClientID + "');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaIngresoTramite.ClientID, script2.ToString(), true);

            string script3 = "calendario('" + FechaInicioProduccion.ClientID + "','" + imgFechaInicioProduccion.ClientID + "');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaInicioProduccion.ClientID, script3.ToString(), true);
        }

        // PAGE_LOAD
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Validamos los accesos al listado y a sus objetos
                Content_Panel.Visible = true;
                
                //Se carga la lista de Barrio
                CargarCombobox("Barrio");
                
                // Cargamos la grilla
                CargaGrilla();
            };
        }

        private void CargarCombobox(string combobox)
        {
            switch (combobox)
            {

                case "Barrio":
                    Barrio.Items.Clear();
                    Barrio.DataSource = mantenedorDA.ListarBarrio_Mantenedor(new Barrio());
                    Barrio.DataTextField = "barrio";
                    Barrio.DataValueField = "id_barrio";
                    Barrio.DataBind();
                    Barrio.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
               
            }
        }

        private void CargaGrilla()
        {
            // Iniciamos la query de búsqueda y cargamos la grilla
            DescansoSanitario descansoSanitario = new DescansoSanitario();
            descansoSanitario.barrio = new ParametroGenerico();
            descansoSanitario.barrio.id = Convert.ToInt32(Barrio.SelectedItem.Value);
            descansoSanitario.barrio.descripcion = Barrio.SelectedValue;

            if (FechaRecepcion.Text != null && !FechaRecepcion.Text.Equals(""))
            {
                descansoSanitario.fechaInicio = Convert.ToDateTime(FechaRecepcion.Text);
            }

            if (FechaIngresoTramite.Text != null && !FechaIngresoTramite.Text.Equals(""))
            {
                descansoSanitario.fechaFin = Convert.ToDateTime(FechaIngresoTramite.Text);
            }

            if (FechaInicioProduccion.Text != null && !FechaInicioProduccion.Text.Equals(""))
            {
                descansoSanitario.fechaInicioProduccionCero = Convert.ToDateTime(FechaInicioProduccion.Text);
            }

            List<DescansoSanitario> dt = mantenedorGeneralService.listarDescansoSanitario(descansoSanitario);

            int num_registros = 0;
            num_registros = dt.Count;

            if (num_registros > 0)
            {
                ExportarGrilla.Visible = true;
            }
            else {
                ExportarGrilla.Visible = false;
            }

            GridViewDescanso.DataSource = dt;
            GridViewDescanso.DataBind();
        }

        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            DescansoSanitario descansoSanitario = new DescansoSanitario();
            descansoSanitario.barrio = new ParametroGenerico();
            descansoSanitario.barrio.id = Convert.ToInt32(Barrio.SelectedItem.Value);
            descansoSanitario.barrio.descripcion = Barrio.SelectedValue;

            if (FechaRecepcion.Text != null && !FechaRecepcion.Text.Equals(""))
            {
                descansoSanitario.fechaInicio = Convert.ToDateTime(FechaRecepcion.Text);
            }

            if (FechaIngresoTramite.Text != null && !FechaIngresoTramite.Text.Equals(""))
            {
                descansoSanitario.fechaFin = Convert.ToDateTime(FechaIngresoTramite.Text);
            }

            if (FechaInicioProduccion.Text != null && !FechaInicioProduccion.Text.Equals(""))
            {
                descansoSanitario.fechaInicioProduccionCero = Convert.ToDateTime(FechaInicioProduccion.Text);
            }

            
            descansoSanitario.tipoOperacion = new ParametroGenerico();
            descansoSanitario.tipoOperacion.id = rbTipo.DESCANSO;


            DescansoSanitario ultimoDescansoSanitario = mantenedorDA.ObtenerUltimaOperacionDescanso_Mantenedor(descansoSanitario.barrio.id);

            if (ultimoDescansoSanitario.idDescanso > 0)
            {
                descansoSanitario.numOperacion = ultimoDescansoSanitario.numOperacion + 1;
            }


            if (descansoSanitario != null)
            {
                List<String> listErroresDescansoSanitario = mantenedorGeneralValidacion.validaDescansoSanitario(descansoSanitario);

                if (listErroresDescansoSanitario.Count <= 0)
                {

                    DataTable dt = mantenedorGeneralService.guardarDescansoSanitario(descansoSanitario);

                    try
                    {
                        string msg = Convert.ToString(dt.Rows[0]["msg"]);
                        if (msg == "OK")
                        {
                            mensaje = "El descanso sanitario del barrio'" + descansoSanitario.barrio.descripcion + "' ha sido creado.";
                            Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                            GridViewDescanso.EditIndex = -1;
                            GridViewDescanso.PageIndex = 0;
                            CargaGrilla();

                            Content_msgGrilla.Visible = true;
                            msgGrilla.Text = mensaje;
                        }
                        else
                        {
                            mensaje = "El descanso sanitario del barrio'" + descansoSanitario.barrio.descripcion + "' ya existe.";
                            Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";

                            Content_msgGrilla.Visible = true;
                            msgGrilla.Text = mensaje;
                        };
                    }
                    catch
                    {
                        mensaje = "Se ha producido un error al intentar crear el descanso sanitario del barrio.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";

                        Content_msgGrilla.Visible = true;
                        msgGrilla.Text = mensaje;
                    };
                    
                }
                else
                {

                    foreach (String error in listErroresDescansoSanitario)
                    {
                        Page.Validators.Add(new ValidationError("grupo1", error));
                    }
                    UpdatePanelMgs.Update();
                }
                
            }
            else
            {
                mensaje = "Para agregar debe ingresar los datos del descanso sanitario del barrio.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";

                Content_msgGrilla.Visible = true;
                msgGrilla.Text = mensaje;
            };

            
        }


        protected void GridViewDescanso_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            Agregar.Enabled = true;
            GridViewDescanso.PageIndex = e.NewPageIndex;
            GridViewDescanso.EditIndex = -1;
            CargaGrilla();
        }

        protected void GridViewDescanso_Sorting(object sender, GridViewSortEventArgs e)
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

            GridViewDescanso.PageIndex = 0;
            GridViewDescanso.EditIndex = -1;
            CargaGrilla();
        }

        protected void GridViewDescanso_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              
                //Solo el último barrio puede ser eliminado


                String idBarrio = ((Label)e.Row.FindControl("gIdBarrio")).Text;
                String tipoOperacion = ((Label)e.Row.FindControl("gIdTipoOperacion")).Text;
                String numeroOperacion = ((Label)e.Row.FindControl("gNumeroOperacion")).Text;

                DescansoSanitario descansoSanitario = mantenedorDA.ObtenerUltimaOperacionDescanso_Mantenedor(Convert.ToInt32(idBarrio));

                if (descansoSanitario.tipoOperacion != null && descansoSanitario.tipoOperacion.id == Convert.ToInt32(tipoOperacion) && descansoSanitario.numOperacion != null &&
                    descansoSanitario.numOperacion == Convert.ToInt32(numeroOperacion))
                {
                    ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                    if (boton_eliminar != null)
                    {
                        boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar el descanso:" + DataBinder.Eval(e.Row.DataItem, "idDescanso") + "?')");
                        boton_eliminar.Visible = true;
                    };
                }


            };
        }

        protected void GridViewDescanso_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Content_msgGrilla.Visible = false;

            int id = 0;
            switch (e.CommandName)
            {
                case "Eliminar":
                    id = Convert.ToInt32(e.CommandArgument);
                    Delete(id);
                    GridViewDescanso.EditIndex = -1;
                    CargaGrilla();
                    break;
            };
        }

        protected void GridViewDescanso_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Agregar.Enabled = false;
            GridViewDescanso.EditIndex = e.NewEditIndex;
            CargaGrilla();
        }
        
        private void Delete(int id)
        {
            DataTable dt = mantenedorGeneralService.eliminarDescansoSanitario(id);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);

                switch (msg)
                {
                    case "OK":
                        mensaje = "El descanso sanitario ha sido eliminado.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        break;
                    case "En uso":
                        mensaje = "El descanso sanitario que intenta borrar está actualmente en uso.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                    default:
                        mensaje = "Se ha producido un error al intentar eliminar el descanso sanitario.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar el descanso sanitario.";
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

        protected void Barrio_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idBarrio = Convert.ToInt32(Barrio.SelectedItem.Value);
            if (idBarrio > 0)
            {

                DescansoSanitario descansoSanitario = mantenedorGeneralService.ObtenerUltimaOperacionDescanso_Mantenedor(idBarrio);

                if (descansoSanitario != null)
                {
                    if (descansoSanitario.idDescanso == 0)
                    {
                        PanelFechaInicioProduccion.Visible = true;
                        UpdatePanelFechaInicioProduccion.Update();
                    }
                    else
                    {
                        PanelFechaInicioProduccion.Visible = false;
                        UpdatePanelFechaInicioProduccion.Update();

                    }

                    //string script1 = "calendarioConHora('" + FechaRecepcion.ClientID + "','" + imgFechaRecepcion.ClientID + "');";
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaRecepcion.ClientID, script1.ToString(), true);

                    //string script2 = "calendarioConHora('" + FechaIngresoTramite.ClientID + "','" + imgFechaIngresoTramite.ClientID + "');";
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaIngresoTramite.ClientID, script2.ToString(), true);

                    //string script3 = "calendarioConHora('" + FechaInicioProduccion.ClientID + "','" + imgFechaInicioProduccion.ClientID + "');";
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaInicioProduccion.ClientID, script3.ToString(), true);

                    string script1 = "calendario('" + FechaRecepcion.ClientID + "','" + imgFechaRecepcion.ClientID + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaRecepcion.ClientID, script1.ToString(), true);

                    string script2 = "calendario('" + FechaIngresoTramite.ClientID + "','" + imgFechaIngresoTramite.ClientID + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaIngresoTramite.ClientID, script2.ToString(), true);

                    string script3 = "calendario('" + FechaInicioProduccion.ClientID + "','" + imgFechaInicioProduccion.ClientID + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaInicioProduccion.ClientID, script3.ToString(), true);
                }
            }
        }

        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();
            string nom_grilla = "descansoACS";
            string ngrilla = "";

            CargaGrilla();
         
          
            switch (nom_grilla)
            {
                case "descansoACS":
                    GridViewDescanso.Columns.RemoveAt(6);
                    grilla = GridViewDescanso;
                    ngrilla = "descansoACS.xls";
                    break;

            };


            grilla.AllowPaging = false;
            grilla.DataBind();
           

           
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
            
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            DescansoSanitario descansoSanitario = new DescansoSanitario();
            descansoSanitario.barrio = new ParametroGenerico();
            descansoSanitario.barrio.id = Convert.ToInt32(Barrio.SelectedItem.Value);
            descansoSanitario.barrio.descripcion = Barrio.SelectedValue;

            if (FechaRecepcion.Text != null && !FechaRecepcion.Text.Equals(""))
            {
                descansoSanitario.fechaInicio = Convert.ToDateTime(FechaRecepcion.Text);
            }

            if (FechaIngresoTramite.Text != null && !FechaIngresoTramite.Text.Equals(""))
            {
                descansoSanitario.fechaFin = Convert.ToDateTime(FechaIngresoTramite.Text);
            }

            if (FechaInicioProduccion.Text != null && !FechaInicioProduccion.Text.Equals(""))
            {
                descansoSanitario.fechaInicioProduccionCero = Convert.ToDateTime(FechaInicioProduccion.Text);
            }

            if (descansoSanitario != null)
            {
                List<DescansoSanitario> dt = mantenedorGeneralService.listarDescansoSanitario(descansoSanitario);

                GridViewDescanso.DataSource = dt;
                GridViewDescanso.DataBind();

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