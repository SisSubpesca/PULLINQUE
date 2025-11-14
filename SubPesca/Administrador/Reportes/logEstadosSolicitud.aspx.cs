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
    public partial class logEstadosSolicitud : System.Web.UI.Page
    {

        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        EstadoService estadoService = new EstadoService();
        

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
                    Estado.Text = "(" + Convert.ToString(dt.Rows[0]["numeracionModelo"]) + ")" + " " + Convert.ToString(dt.Rows[0]["nombreEstado"]);
                }
                catch { };

                // Cargamos la grilla
                CargaGrilla();
            };
        }

        // EVENTOS GENERALES
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
            CargaGrilla();

            GridView grilla = GridView1;
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export("logEstadoSolicitud.xls", grilla);
        }


        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            // Cargamos los filtros
            int id_solicitud = Convert.ToInt32(IdSolicitud.Text);

            // Iniciamos la query de búsqueda y cargamos la grilla
            DataTable dt = new DataTable();
            int num_registros = 0;

            dt = estadoService.ListarHistEstadosSolicConces(id_solicitud);
            if (dt != null)
            {
                num_registros = dt.Rows.Count;
                if (num_registros > 0)
                {
                    if (num_registros == 1)
                    {
                        msgGrilla.Text = "Se ha encontrado 1 registro de log relacionado a la solicitud.";
                    }
                    else
                    {
                        msgGrilla.Text = "Se han encontrado " + num_registros + " registros de log relacionados a la solicitud.";
                    };
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Content_msgGrilla.Visible = true;
                    ExportarGrilla.Visible = true;
                }
                else
                {
                    msgGrilla.Text = "No se encontraron registros de log relacionados a la solicitud.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Content_msgGrilla.Visible = true;
                    ExportarGrilla.Visible = false;
                };

                // Se ordena el DataTable en el server de aplicación
                dt.DefaultView.Sort = KeySort.Value;
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                Response.Redirect("~/Administrador/Reportes/ResumenIndicadores.aspx");
            };

        }
        protected void GridView1_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
            CargaGrilla();
        }


        public object HT_detalleIndicador { get; set; }
    }
}
