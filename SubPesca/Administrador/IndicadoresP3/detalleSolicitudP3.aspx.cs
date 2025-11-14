using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using LogicaNegocio.cl.subpesca.rb.servicios.estados;
namespace SubPesca.Administrador.IndicadoresP3
{
    public partial class detalleSolicitudP3 : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        EstadoService estadoService = new EstadoService();
        Datos.Entidades.Solicitudes conn1 = new Datos.Entidades.Solicitudes();
        string defaultKeySort1 = "TipoEspecie ASC";
        string defaultKeySort2 = "TipoEspecie ASC";
        string defaultKeySort3 = "Vertice ASC";


        // PAGE_LOAD
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                // Obtención de parámetros mediante GET/POST
                ObtencionParametros();
                Volver.Visible = true;
                
                DataTable dt = estadoService.Solicitudes_Ver_RB(Convert.ToInt32(IdSolicitud.Text));
                try
                {
                    Pert.Text = Convert.ToString(dt.Rows[0]["numPert"]);
                    IdEstado.Value = Convert.ToString(dt.Rows[0]["idEstadoActual"]);
                    Estado.Text = Convert.ToString(dt.Rows[0]["nombreEstado"]);
                    Titulares.Text = Convert.ToString(dt.Rows[0]["titulares"]);
                    FechaIngreso.Text = Convert.ToString(dt.Rows[0]["fechaIngresoTramite"]);
                    Comuna.Text = Convert.ToString(dt.Rows[0]["comunas"]);
                    Superficie.Text = Convert.ToString(dt.Rows[0]["areaTotalSolicitada"]);
                    Sector.Text = Convert.ToString(dt.Rows[0]["toponimio"]);
                }
                catch { };
                KeySort1.Value = defaultKeySort1;
                KeySort2.Value = defaultKeySort2;
                KeySort3.Value = defaultKeySort3;

                // Cargamos las grillas
                CargaGrilla("Estructuras");
                CargaGrilla("Produccion");
                CargaGrilla("Vertices");
            };
        }


        protected void ObtencionParametros()
        {
            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

            
            try
            {
                if (Request.QueryString["idSol"] != null)
                {
                    IdSolicitud.Text = Convert.ToString(Convert.ToInt32(Request.QueryString["idSol"]));
                }
                else
                {
                    Response.Redirect("~/Administrador/Reportes/resumenEstados.aspx");
                };
            }
            catch
            {
                Response.Redirect("~/Administrador/Reportes/resumenEstados.aspx");
            };

            try
            {
                if (Request.QueryString["id_solicitud"] != null)
                {
                    IdTipoTramite.Value = Convert.ToString(Request.QueryString["id_solicitud"]);
                    
                };
            }
            catch
            {
                Response.Redirect("~/Administrador/Reportes/resumenEstados.aspx");
            };

            try
            {
                if (Request.QueryString["id_indicador"] != null)
                {
                    IdIndicador.Value = Convert.ToString(Request.QueryString["id_indicador"]);
                    
                };
            }
            catch
            {
                Response.Redirect("~/Administrador/Reportes/resumenEstados.aspx");
            };

            try
            {
                if (Request.QueryString["id_subTipo"] != null)
                {
                    IdSubTipo.Value = Convert.ToString(Request.QueryString["id_subTipo"]);

                };
            }
            catch
            {
                Response.Redirect("~/Administrador/Reportes/resumenEstados.aspx");
            };

            
        }


        protected void Volver_Click(object sender, EventArgs e)
        {
            string path = "~/Administrador/Reportes/resumenEstados.aspx";



            try
            {
                path = @"~/Administrador/IndicadoresP3/indicador.aspx?id_indicador=" + IdIndicador.Value.Trim() + "&id_solicitud=" + IdTipoTramite.Value.Trim() + "&id_subTipo=" + IdSubTipo.Value.Trim()+ "&rel=1";
            }
            catch (Exception ex) { 
            
            }
                    
                    
            Response.Redirect(path);
        }


        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            Button boton_clickeado = (Button)sender;
            string id_boton = boton_clickeado.ID;
            GridView grilla = new GridView();
            string nom_grilla = "grilla.xls";

            switch (id_boton)
            {
                case "ExportarGrilla1":
                    grilla = GridView1;
                    CargaGrilla("Estructuras");
                    nom_grilla = "estructurasSolicitud.xls";
                    break;
                case "ExportarGrilla2":
                    grilla = GridView2;
                    CargaGrilla("Produccion");
                    nom_grilla = "produccionSolicitud.xls";
                    break;
                case "ExportarGrilla3":
                    grilla = GridView3;
                    CargaGrilla("Vertices");
                    nom_grilla = "verticesSolicitud.xls";
                    break;
            };

            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(nom_grilla, grilla);
        }


        // EVENTOS DE LA GRILLA
        protected void CargaGrilla(string grilla)
        {
            int id_solicitud = Convert.ToInt32(IdSolicitud.Text);
            int num_registros = 0;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            switch (grilla)
            {
                case "Estructuras":
                    // Iniciamos la query de búsqueda y cargamos la grilla
                    dt = conn1.ObtieneEstructurasTecnicasSolicitud(KeySort1.Value, id_solicitud);
                    if (dt != null)
                    {
                        num_registros = dt.Rows.Count;
                        if (num_registros > 0)
                        {
                            if (num_registros == 1)
                            {
                                msgGrilla1.Text = "Se ha encontrado 1 estructura relacionada al proyecto técnico de la solicitud.";
                            }
                            else
                            {
                                msgGrilla1.Text = "Se han encontrado " + num_registros + " estructuras relacionadas al proyecto técnico de la solicitud.";
                            };
                            Ico_msgGrilla1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                            Content_msgGrilla1.Visible = true;
                            ExportarGrilla1.Visible = true;
                        }
                        else
                        {
                            msgGrilla1.Text = "No se encontraron estructuras relacionadas al proyecto técnico de la solicitud.";
                            Ico_msgGrilla1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                            Content_msgGrilla1.Visible = true;
                            ExportarGrilla1.Visible = false;
                        };

                        // Se ordena el DataTable en el server de aplicación
                        //dt.DefaultView.Sort = KeySort1.Value;
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                    else
                    {
                        Response.Redirect("~/Administrador/Reportes/ResumenEstados.aspx");
                    };
                    break;
                case "Produccion":
                    // Iniciamos la query de búsqueda y cargamos la grilla
                    dt = conn1.ObtieneProgramaProduccionSolicitud(KeySort2.Value, id_solicitud);
                    if (dt != null)
                    {
                        num_registros = dt.Rows.Count;
                        if (num_registros > 0)
                        {
                            if (num_registros == 1)
                            {
                                msgGrilla2.Text = "Se ha encontrado 1 registro de producción relacionado al proyecto técnico de la solicitud.";
                            }
                            else
                            {
                                msgGrilla2.Text = "Se han encontrado " + num_registros + " registros de producción relacionados al proyecto técnico de la solicitud.";
                            };
                            Ico_msgGrilla2.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                            Content_msgGrilla2.Visible = true;
                            ExportarGrilla2.Visible = true;
                        }
                        else
                        {
                            msgGrilla2.Text = "No se encontró información de producción relacionada al proyecto técnico de la solicitud.";
                            Ico_msgGrilla2.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                            Content_msgGrilla2.Visible = true;
                            ExportarGrilla2.Visible = false;
                        };

                        // Se ordena el DataTable en el server de aplicación
                        //dt.DefaultView.Sort = KeySort2.Value;
                        GridView2.DataSource = dt;
                        GridView2.DataBind();
                    }
                    else
                    {
                        Response.Redirect("~/Administrador/Reportes/ResumenEstados.aspx");
                    };
                    break;
                case "Vertices":
                    // Iniciamos la query de búsqueda y cargamos la grilla
                    dt = conn1.RB_Solicitudes_Vertices_Listar(KeySort3.Value, id_solicitud);
                    if (dt != null)
                    {
                        num_registros = dt.Rows.Count;
                        if (num_registros > 0)
                        {
                            if (num_registros == 1)
                            {
                                msgGrilla3.Text = "Se ha encontrado 1 vértice relacionado a la solicitud.";
                            }
                            else
                            {
                                msgGrilla3.Text = "Se han encontrado " + num_registros + " vértices relacionados a la solicitud.";
                            };
                            Ico_msgGrilla3.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                            Content_msgGrilla3.Visible = true;
                            ExportarGrilla3.Visible = true;
                        }
                        else
                        {
                            msgGrilla3.Text = "No se encontraron vértices relacionados a la solicitud.";
                            Ico_msgGrilla3.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                            Content_msgGrilla3.Visible = true;
                            ExportarGrilla3.Visible = false;
                        };

                        // Se ordena el DataTable en el server de aplicación
                        //dt.DefaultView.Sort = KeySort3.Value;
                        GridView3.DataSource = dt;
                        GridView3.DataBind();
                    }
                    else
                    {
                        Response.Redirect("~/Administrador/Reportes/ResumenEstados.aspx");
                    };
                    break;
            };
        }


        protected void GridView1_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
            CargaGrilla("Estructuras");
        }


        protected void GridView2_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            GridView2.DataBind();
            CargaGrilla("Produccion");
        }


        protected void GridView3_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridView3.PageIndex = e.NewPageIndex;
            GridView3.DataBind();
            CargaGrilla("Vertices");
        }


        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sort = KeySort1.Value;
            int pos = 0;

            pos = sort.IndexOf(e.SortExpression + " ASC");
            if (pos >= 0)
            {
                sort = e.SortExpression + " DESC";
            }
            else
            {
                sort = e.SortExpression + " ASC";
            };
            KeySort1.Value = sort;
            GridView1.PageIndex = 0;
            GridView1.DataBind();

            CargaGrilla("Estructuras");
        }


        protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sort = KeySort2.Value;
            int pos = 0;

            pos = sort.IndexOf(e.SortExpression + " ASC");
            if (pos >= 0)
            {
                sort = e.SortExpression + " DESC";
            }
            else
            {
                sort = e.SortExpression + " ASC";
            };
            KeySort2.Value = sort;
            GridView2.PageIndex = 0;
            GridView2.DataBind();

            CargaGrilla("Produccion");
        }


        protected void GridView3_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sort = KeySort3.Value;
            int pos = 0;

            pos = sort.IndexOf(e.SortExpression + " ASC");
            if (pos >= 0)
            {
                sort = e.SortExpression + " DESC";
            }
            else
            {
                sort = e.SortExpression + " ASC";
            };
            KeySort3.Value = sort;
            GridView3.PageIndex = 0;
            GridView3.DataBind();

            CargaGrilla("Vertices");
        }


    }
}
