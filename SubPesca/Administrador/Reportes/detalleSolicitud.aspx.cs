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


//@used

namespace SubPesca.Administrador.Reportes
{
    public partial class detalleSolicitud : System.Web.UI.Page
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

                // Cargamso los campos
                switch (BackPage.Value)
                {
                    case "1": // Retornamos a detalleEstado
                    case "2": // Retornamos a busquedaSolicitudes

                    case "9": // Retornamos a detallePlazo
                        Volver.Visible = true;
                        break;
                    case "10": // Retornamos a detalleIndicador
                        Volver.Visible = true;
                        break;
                    case "11": // Retornamos a detalleIndicador
                        Volver.Visible = true;
                        break;
                    case "12": // Retornamos a detalleIndicador
                        Volver.Visible = true;
                        break;
                    case "13": // Retornamos a detalleIndicador
                        Volver.Visible = true;
                        break;
                    case "14": // Retornamos a detalleIndicador
                        Volver.Visible = true;
                        break;
                    case "15": // Retornamos a detalleIndicador
                        Volver.Visible = true;
                        break;
                    case "16": // Retornamos a detalleIndicador
                        Volver.Visible = true;
                        break;
                    case "17": // Retornamos a detalleIndicador
                        Volver.Visible = true;
                        break;
                    case "18": // Retornamos a detalleIndicador
                        Volver.Visible = true;
                        break;
                    case "19": // Retornamos a detalleIndicador
                        Volver.Visible = true;
                        break;
                    case "20": // Retornamos a detalleIndicador
                        Volver.Visible = true;
                        break;
                    case "21": // Retornamos a detalleIndicador
                        Volver.Visible = true;
                        break;
                    case "22": // Retornamos a detalleIndicador
                        Volver.Visible = true;
                        break;

                };
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

            // Se recibe el id_solicitud
            try
            {
                if (Request.QueryString["id_solicitud"] != null)
                {
                    IdSolicitud.Text = Convert.ToString(Convert.ToInt32(Request.QueryString["id_solicitud"]));
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

            // Se recibe el switch para determinar que página invocó a ésta página
            try
            {
                if (Request.QueryString["bp"] != null)
                {
                    switch (Convert.ToInt32(Request.QueryString["bp"]))
                    { 
                        case 1: // Retornamos a detalleEstado
                        case 2: // Retornamos a busquedaSolicitudes

                        case 9: // Retornamos a detalleIndicador
                            BackPage.Value = Convert.ToString(Request.QueryString["bp"]);
                            break;
                        case 10: // Retornamos a detalleIndicador
                            BackPage.Value = Convert.ToString(Request.QueryString["bp"]);
                            break;
                        case 11: // Retornamos a detalleIndicador
                            BackPage.Value = Convert.ToString(Request.QueryString["bp"]);
                            break;
                        case 12: // Retornamos a detalleIndicador
                            BackPage.Value = Convert.ToString(Request.QueryString["bp"]);
                            break;
                        case 13: // Retornamos a detalleIndicador
                            BackPage.Value = Convert.ToString(Request.QueryString["bp"]);
                            break;
                        case 14: // Retornamos a detalleIndicador
                            BackPage.Value = Convert.ToString(Request.QueryString["bp"]);
                            break;
                        case 15: // Retornamos a detalleIndicador
                            BackPage.Value = Convert.ToString(Request.QueryString["bp"]);
                            break;
                        case 16: // Retornamos a detalleIndicador
                            BackPage.Value = Convert.ToString(Request.QueryString["bp"]);
                            break;
                        case 17: // Retornamos a detalleIndicador
                            BackPage.Value = Convert.ToString(Request.QueryString["bp"]);
                            break;
                        case 18: // Retornamos a detalleIndicador
                            BackPage.Value = Convert.ToString(Request.QueryString["bp"]);
                            break;
                        case 19: // Retornamos a detalleIndicador
                            BackPage.Value = Convert.ToString(Request.QueryString["bp"]);
                            break;
                        case 20: // Retornamos a detalleIndicador
                            BackPage.Value = Convert.ToString(Request.QueryString["bp"]);
                            break;
                        case 21: // Retornamos a detalleIndicador
                            BackPage.Value = Convert.ToString(Request.QueryString["bp"]);
                            break;
                        case 22: // Retornamos a detalleIndicador
                            BackPage.Value = Convert.ToString(Request.QueryString["bp"]);
                            break;

                        default:
                            Response.Redirect("~/Administrador/Reportes/resumenEstados.aspx");
                            break;
                    };
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
            Hashtable HT_ModReportes = (Hashtable)Session["Modulo_Reportes"];
            Hashtable HT_ModPlazos = (Hashtable)Session["Modulo_Plazos"];

            if (HT_ModReportes != null || (HT_ModPlazos != null && BackPage != null && BackPage.Value != null && BackPage.Value == "9"))
            {
                switch (BackPage.Value)
                { 
                    case "1":
                        Hashtable HT_ListSolicitudes = (Hashtable)HT_ModReportes["ListSolicitudes"];
                        if (HT_ListSolicitudes != null)
                        {
                            HT_ListSolicitudes["locked"] = false;
                            path = @"~/Administrador/Reportes/detalleEstado.aspx?id_estado=" + IdEstado.Value + "&tipo=" + Convert.ToString((int)HT_ListSolicitudes["tipo"]);
                            HT_ModReportes["ListSolicitudes"] = (Hashtable)HT_ListSolicitudes;
                            Session["Modulo_Reportes"] = (Hashtable)HT_ModReportes;
                        };
                        break;
                    case "9":

                        Hashtable HT_detallePlazo = (Hashtable)HT_ModPlazos["detallePlazo"];
                        if (HT_detallePlazo != null)
                        {
                            HT_detallePlazo["locked"] = false;
                            path = @"~/Administrador/Reportes/detallePlazo.aspx?idSubRequerimiento=" + Convert.ToString((int)HT_detallePlazo["id_subRequerimiento"]) + "&tipo=" + Convert.ToString((int)HT_detallePlazo["tipo"]);
                            HT_ModPlazos["detallePlazo"] = (Hashtable)HT_detallePlazo;
                            Session["Modulo_Plazos"] = (Hashtable)HT_ModPlazos;
                        };
                        break;


                    case "10":

                        Hashtable HT_detalleIndicador10 = (Hashtable)HT_ModReportes["detalleIndicador"];
                        if (HT_detalleIndicador10 != null)
                        {
                            HT_detalleIndicador10["locked"] = false;
                            path = @"~/Administrador/Reportes/detalleIndicador.aspx?indicador=" + Convert.ToString((int)HT_detalleIndicador10["indicador"]);
                            HT_ModReportes["detalleIndicador"] = (Hashtable)HT_detalleIndicador10;
                            Session["Modulo_Reportes"] = (Hashtable)HT_ModReportes;
                        };
                        break;

                    case "11":

                        Hashtable HT_detalleIndicador11 = (Hashtable)HT_ModReportes["detalleIndicador"];
                        if (HT_detalleIndicador11 != null)
                        {
                            HT_detalleIndicador11["locked"] = false;
                            path = @"~/Administrador/Reportes/detalleIndicadorRelocalizacionSectorCero.aspx?indicador=" + Convert.ToString((int)HT_detalleIndicador11["indicador"]);
                            HT_ModReportes["detalleIndicador"] = (Hashtable)HT_detalleIndicador11;
                            Session["Modulo_Reportes"] = (Hashtable)HT_ModReportes;
                        };
                        break;


                    case "12":
                        Hashtable HT_detalleIndicador12 = (Hashtable)HT_ModReportes["detalleIndicador"];
                        if (HT_detalleIndicador12 != null)
                        {
                            HT_detalleIndicador12["locked"] = false;
                            path = @"~/Administrador/Reportes/detalleIndicadorRelocalizacionFusiona.aspx?indicador=" + Convert.ToString((int)HT_detalleIndicador12["indicador"]);
                            HT_ModReportes["detalleIndicador"] = (Hashtable)HT_detalleIndicador12;
                            Session["Modulo_Reportes"] = (Hashtable)HT_ModReportes;
                        };
                        break;

                    case "13":
                        Hashtable HT_detalleIndicador13 = (Hashtable)HT_ModReportes["detalleIndicador"];
                        if (HT_detalleIndicador13 != null)
                        {
                            HT_detalleIndicador13["locked"] = false;
                            path = @"~/Administrador/Reportes/detalleIndicadorRelocalizacionCREA.aspx?indicador=" + Convert.ToString((int)HT_detalleIndicador13["indicador"]);
                            HT_ModReportes["detalleIndicador"] = (Hashtable)HT_detalleIndicador13;
                            Session["Modulo_Reportes"] = (Hashtable)HT_ModReportes;
                        };
                        break;

                    case "14":
                        Hashtable HT_detalleIndicador14 = (Hashtable)HT_ModReportes["detalleIndicador"];
                        if (HT_detalleIndicador14 != null)
                        {
                            HT_detalleIndicador14["locked"] = false;
                            path = @"~/Administrador/Reportes/detalleIndicadorModRegularizacion.aspx?indicador=" + Convert.ToString((int)HT_detalleIndicador14["indicador"]);
                            HT_ModReportes["detalleIndicador"] = (Hashtable)HT_detalleIndicador14;
                            Session["Modulo_Reportes"] = (Hashtable)HT_ModReportes;
                        };
                        break;


                    case "15":
                        Hashtable HT_detalleIndicador15 = (Hashtable)HT_ModReportes["detalleIndicador"];
                        if (HT_detalleIndicador15 != null)
                        {
                            HT_detalleIndicador15["locked"] = false;
                            path = @"~/Administrador/Reportes/detalleIndicadorModReduccion.aspx?indicador=" + Convert.ToString((int)HT_detalleIndicador15["indicador"]);
                            HT_ModReportes["detalleIndicador"] = (Hashtable)HT_detalleIndicador15;
                            Session["Modulo_Reportes"] = (Hashtable)HT_ModReportes;
                        };
                        break;


                    case "16":
                        Hashtable HT_detalleIndicador16 = (Hashtable)HT_ModReportes["detalleIndicador"];
                        if (HT_detalleIndicador16 != null)
                        {
                            HT_detalleIndicador16["locked"] = false;
                            path = @"~/Administrador/Reportes/detalleIndicadorModProyecto.aspx?indicador=" + Convert.ToString((int)HT_detalleIndicador16["indicador"]);
                            HT_ModReportes["detalleIndicador"] = (Hashtable)HT_detalleIndicador16;
                            Session["Modulo_Reportes"] = (Hashtable)HT_ModReportes;
                        };
                        break;


                    case "17":
                        Hashtable HT_detalleIndicador17 = (Hashtable)HT_ModReportes["detalleIndicador"];
                        if (HT_detalleIndicador17 != null)
                        {
                            HT_detalleIndicador17["locked"] = false;
                            path = @"~/Administrador/Reportes/detalleIndicadorModEspecie.aspx?indicador=" + Convert.ToString((int)HT_detalleIndicador17["indicador"]);
                            HT_ModReportes["detalleIndicador"] = (Hashtable)HT_detalleIndicador17;
                            Session["Modulo_Reportes"] = (Hashtable)HT_ModReportes;
                        };
                        break;


                    case "18":
                        Hashtable HT_detalleIndicador18 = (Hashtable)HT_ModReportes["detalleIndicador"];
                        if (HT_detalleIndicador18 != null)
                        {
                            HT_detalleIndicador18["locked"] = false;
                            path = @"~/Administrador/Reportes/detalleIndicadorModAmpliacion.aspx?indicador=" + Convert.ToString((int)HT_detalleIndicador18["indicador"]);
                            HT_ModReportes["detalleIndicador"] = (Hashtable)HT_detalleIndicador18;
                            Session["Modulo_Reportes"] = (Hashtable)HT_ModReportes;
                        };
                        break;


                    case "19":
                        Hashtable HT_detalleIndicador19 = (Hashtable)HT_ModReportes["detalleIndicador"];
                        if (HT_detalleIndicador19 != null)
                        {
                            HT_detalleIndicador19["locked"] = false;
                            path = @"~/Administrador/Reportes/detalleIndicadorFaenamiento.aspx?indicador=" + Convert.ToString((int)HT_detalleIndicador19["indicador"]);
                            HT_ModReportes["detalleIndicador"] = (Hashtable)HT_detalleIndicador19;
                            Session["Modulo_Reportes"] = (Hashtable)HT_ModReportes;
                        };
                        break;

                    case "20":
                        Hashtable HT_detalleIndicador20 = (Hashtable)HT_ModReportes["detalleIndicador"];
                        if (HT_detalleIndicador20 != null)
                        {
                            HT_detalleIndicador20["locked"] = false;
                            path = @"~/Administrador/Reportes/detalleIndicadorColectores.aspx?indicador=" + Convert.ToString((int)HT_detalleIndicador20["indicador"]);
                            HT_ModReportes["detalleIndicador"] = (Hashtable)HT_detalleIndicador20;
                            Session["Modulo_Reportes"] = (Hashtable)HT_ModReportes;
                        };
                        break;

                    case "21":
                        Hashtable HT_detalleIndicador21 = (Hashtable)HT_ModReportes["detalleIndicador"];
                        if (HT_detalleIndicador21 != null)
                        {
                            HT_detalleIndicador21["locked"] = false;
                            path = @"~/Administrador/Reportes/detalleIndicadorAmerb.aspx?indicador=" + Convert.ToString((int)HT_detalleIndicador21["indicador"]);
                            HT_ModReportes["detalleIndicador"] = (Hashtable)HT_detalleIndicador21;
                            Session["Modulo_Reportes"] = (Hashtable)HT_ModReportes;
                        };
                        break;

                    case "22":
                        Hashtable HT_detalleIndicador22 = (Hashtable)HT_ModReportes["detalleIndicador"];
                        if (HT_detalleIndicador22 != null)
                        {
                            HT_detalleIndicador22["locked"] = false;
                            path = @"~/Administrador/Reportes/detalleIndicadorAcopio.aspx?indicador=" + Convert.ToString((int)HT_detalleIndicador22["indicador"]);
                            HT_ModReportes["detalleIndicador"] = (Hashtable)HT_detalleIndicador22;
                            Session["Modulo_Reportes"] = (Hashtable)HT_ModReportes;
                        };
                        break;
                };
            };
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

            switch(grilla)
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
