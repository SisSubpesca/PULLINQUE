using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.servicios.reportes;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Datos.Entidades;
using System.Web.UI.HtmlControls;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace SubPesca.Mantenedores.Generales
{
    public partial class equivalenciaEstadoUOT : System.Web.UI.Page
    {
        
        


        
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        MantenedorDA mantenedorDA = new MantenedorDA();

        ReporteService reporteService = new ReporteService();
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (usuario_logeado == null)
                {
                    Response.Redirect("~/ingreso.aspx");
                }

                // Validamos los accesos al listado y a sus objetos
                Content_Panel.Visible = true;

                Carga_Combobox("TiposSolicitud");

                Carga_Combobox("Estado");

                Carga_Combobox("EstadoUOT");

                Initialize_ListEstadosUOT();
                
            };
        }

        //Inicializar el listado de equivalencias de estados de solicitudes con estados uot
        protected void Initialize_ListEstadosUOT()
        {

            try
            {
                //Listas de equivalencias UOT
                EquivalenciaUOT equivalenciaUOT = new EquivalenciaUOT();
                equivalenciaUOT.tipoUE = new ParametroGenerico();
                equivalenciaUOT.tipoUE.id = Convert.ToInt32(TiposSolicitud.SelectedItem.Value);

                equivalenciaUOT.estadoSolicitud = new ParametroGenerico();
                equivalenciaUOT.estadoSolicitud.id = Convert.ToInt32(Estado.SelectedItem.Value);

                equivalenciaUOT.estadoUOT = new ParametroGenerico();
                equivalenciaUOT.estadoUOT.id = Convert.ToInt32(EstadoUOT.SelectedItem.Value);

                List<EquivalenciaUOT> dt = mantenedorGeneralService.listarEquivalenciaUOT(equivalenciaUOT);
                
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
            catch (Exception)
            {

                Response.Redirect("~/Administrador/principal.aspx");
            }

        }
        
        protected void Carga_Combobox(string combobox)
        {
            try
            {
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];
                switch (combobox)
                {
                    case "TiposSolicitud":
                        // Cargamos el combobox: Tipos de Solicitud
                        TiposSolicitud.Items.Clear();
                        TiposSolicitud.DataSource = parametroGenericoDA.ListarTipoTramiteEstadoSolicitud(0);
                        TiposSolicitud.DataTextField = "nombreTipoInterfaz";
                        TiposSolicitud.DataValueField = "idTipoTramite";
                        TiposSolicitud.DataBind();
                        TiposSolicitud.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                        break;

                    case "Estado":
                        Estado.Items.Clear();
                        int idTipoTramite2 = Convert.ToInt32(TiposSolicitud.SelectedValue);
                        List<ParametroGenerico> resp = reporteService.ListarEstadosPorTipoTramite(idTipoTramite2);
                        if (resp != null)
                        {
                            foreach (ParametroGenerico item in resp)
                            {
                                Estado.Items.Add(new ListItem(item.descripcion, Convert.ToString(item.id)));
                            }
                        }
                        Estado.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                        Estado.DataBind();
                        UpdatePanel9.Update();
                        break;
                    case "EstadoUOT":
                        // Cargamos el combobox: EstadoUOT
                        EstadoUOT.Items.Clear();
                        EstadoUOT.DataSource = mantenedorDA.ListarEstadosUOT_Mantenedor(new ParametroGenerico());
                        EstadoUOT.DataTextField = "descripcion";
                        EstadoUOT.DataValueField = "id";
                        EstadoUOT.DataBind();
                        EstadoUOT.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                        break;

                };
            }
            catch (Exception)
            {
                Response.Redirect("~/Administrador/principal.aspx");
            }
        }

        protected void TiposTramite_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                int idTipoTramite = Convert.ToInt32(TiposSolicitud.SelectedValue);
                Carga_Combobox("Estado");
            }
            catch (Exception)
            {
                Response.Redirect("~/Administrador/principal.aspx");
            }

        }

        protected void GridView1_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.EditIndex = -1;
            Initialize_ListEstadosUOT();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar la equivalencia EstadoUOT?");
                    boton_eliminar.Visible = true;
                };
            };

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Content_msgGrilla.Visible = false;

            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            switch (e.CommandName)
            {
                case "Eliminar":
                    int idEstadoUOT = Convert.ToInt32(arg[0]);
                    int idEstadoSolicitud = Convert.ToInt32(arg[1]);

                    Delete(idEstadoUOT, idEstadoSolicitud);

                    GridView1.EditIndex = -1;
                    Initialize_ListEstadosUOT();
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
            Initialize_ListEstadosUOT();
        }

        //protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        //{
            
        //    GridView1.EditIndex = e.NewEditIndex;
        //    Initialize_ListEstadosUOT();
        //}


        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            Initialize_ListEstadosUOT();
        }

        protected void Update(int id, string idEstado, string idEstadoUOT)
        {

            EquivalenciaUOT equivalenciaUOT = new EquivalenciaUOT();
            equivalenciaUOT.estadoSolicitud = new ParametroGenerico();
            equivalenciaUOT.estadoSolicitud.id = Convert.ToInt32(idEstado);

            equivalenciaUOT.estadoUOT = new ParametroGenerico();
            equivalenciaUOT.estadoUOT.id = Convert.ToInt32(idEstadoUOT);

            DataTable dt = mantenedorGeneralService.actualizarEquivalenciaEstadoUOT(equivalenciaUOT);
            
            string mensaje = "";

            try
            {
                
                //revisar la validación que una equivalencia no se ingrese 2 veces
                string msg = Convert.ToString(dt.Rows[0]["msg"]);
                if (msg == "OK")
                {
                    mensaje = "La equivalencia EstadoUOT ha sido actualizada.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    GridView1.EditIndex = -1;
                }
                else
                {
                    mensaje = "La equivalencia EstadoUOT ya existe.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar actualizar la equivalencia EstadoUOT.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void Delete(int idEstadoUOT, int idEstadoSolicitud)
        {
            DataTable dt = mantenedorGeneralService.eliminarEquivalenciaEstadoUOT(idEstadoUOT, idEstadoSolicitud);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);

                switch (msg)
                {
                    case "OK":
                        mensaje = "La equivalencia EstadoUOT ha sido eliminada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        break;
                    case "En uso":
                        mensaje = "La equivalencia EstadoUOT que intenta borrar está actualmente en uso.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                    default:
                        mensaje = "Se ha producido un error al intentar eliminar la equivalencia EstadoUOT.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar el holding.";
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
            string nom_grilla = "equivalenciaEstadoUOT";
            string ngrilla = "";

            Initialize_ListEstadosUOT();

            switch (nom_grilla)
            {
                case "equivalenciaEstadoUOT":
                    grilla = GridView1;
                    GridView1.Columns.RemoveAt(3);
                    ngrilla = "equivalenciaEstadoUOT.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            EquivalenciaUOT equivalenciaUOT = new EquivalenciaUOT();
            equivalenciaUOT.tipoUE = new ParametroGenerico();
            equivalenciaUOT.tipoUE.id = Convert.ToInt32(TiposSolicitud.SelectedItem.Value);

            equivalenciaUOT.estadoSolicitud = new ParametroGenerico();
            equivalenciaUOT.estadoSolicitud.id = Convert.ToInt32(Estado.SelectedItem.Value);

            equivalenciaUOT.estadoUOT = new ParametroGenerico();
            equivalenciaUOT.estadoUOT.id = Convert.ToInt32(EstadoUOT.SelectedItem.Value);


            if (equivalenciaUOT != null)
            {
                List<EquivalenciaUOT> dt = mantenedorGeneralService.listarEquivalenciaUOT(equivalenciaUOT);

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

        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            EquivalenciaUOT equivalenciaUOT = new EquivalenciaUOT();
            equivalenciaUOT.tipoUE = new ParametroGenerico();
            equivalenciaUOT.tipoUE.id = Convert.ToInt32(TiposSolicitud.SelectedItem.Value);

            equivalenciaUOT.estadoSolicitud = new ParametroGenerico();
            equivalenciaUOT.estadoSolicitud.id = Convert.ToInt32(Estado.SelectedItem.Value);

            equivalenciaUOT.estadoUOT = new ParametroGenerico();
            equivalenciaUOT.estadoUOT.id = Convert.ToInt32(EstadoUOT.SelectedItem.Value);

            if (equivalenciaUOT != null)
            {

                DataTable dt = mantenedorGeneralService.GuardarEquivalenciaEstadoUOT(equivalenciaUOT);
                try
                {
                    string msg = Convert.ToString(dt.Rows[0]["msg"]);
                    if (msg == "OK")
                    {
                        mensaje = "La equivalencia de estado UOT ha sido creada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        GridView1.EditIndex = -1;
                        GridView1.PageIndex = 0;

                        TiposSolicitud.SelectedValue = "-1";
                        Estado.SelectedValue = "-1";
                        EstadoUOT.SelectedValue = "-1";

                        Initialize_ListEstadosUOT();
                    }
                    else
                    {
                        mensaje = "La equivalencia de estado UOT ya existe.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    };
                }
                catch
                {
                    mensaje = "Se ha producido un error al intentar crear la equivalencia de estado UOT.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };

            }
            else
            {
                mensaje = "Para agregar debe ingresar la equivalencia de estado UOT.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }
    }
}