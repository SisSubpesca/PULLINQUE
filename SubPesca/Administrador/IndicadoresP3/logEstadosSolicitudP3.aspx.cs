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
    public partial class logEstadosSolicitudP3 : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        EstadoService estadoService = new EstadoService();


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
                path = @"~/Administrador/IndicadoresP3/indicador.aspx?id_indicador=" + IdIndicador.Value.Trim() + "&id_solicitud=" + IdTipoTramite.Value.Trim() + "&id_subTipo=" + IdSubTipo.Value.Trim() + "&rel=1";
            }
            catch (Exception ex)
            {

            }


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
                Response.Redirect("~/Administrador/Reportes/ResumenEstados.aspx");
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
