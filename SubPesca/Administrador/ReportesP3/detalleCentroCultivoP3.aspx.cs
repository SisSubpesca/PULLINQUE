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
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.estados;


namespace SubPesca.Administrador.ReportesP3
{
    public partial class detalleCentroCultivoP3 : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        EstadoService estadoService = new EstadoService();
        Datos.Utilidades.Funciones fnc = new Datos.Utilidades.Funciones();
        CentroCultivoDA centroCultivoDA = new CentroCultivoDA();
        SolicitudDA solicitudDA = new SolicitudDA();
        UnidadEspacialDA unidadEspacialDA = new UnidadEspacialDA();

        // PAGE LOAD
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Obtención de parámetros mediante GET/POST
                ObtencionParametros();

                // Completamos los campos del listado
                Initialize_Form();
            };
        }


        // EVENTOS GENERALES
        protected void ObtencionParametros()
        {
            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

            if (Request.QueryString["origen"] != null)
            {
                ViewState["origen"]  =  Request.QueryString["origen"];
            }


            if (Request.QueryString["id_centrocultivo"] != null)
            {
                IdSolConcesion.Value = Request.QueryString["id_centrocultivo"];
            }
            else
            {
                Response.Redirect("~/Administrador/ReportesP3/reportesConcesion.aspx");
            }

        }


        protected void Initialize_Form()
        {
            try
            {



                DataTable dt = estadoService.Solicitudes_Ver_RB(Convert.ToInt32(IdSolConcesion.Value));

                if (dt != null)
                {

                    UnidadEspacial unidadespacial = unidadEspacialDA.ObtieneUnidadEspacial(Convert.ToInt32(IdSolConcesion.Value), 0);

                    if (unidadespacial != null)
                    {
                        IdCentroCultivo.Text = Convert.ToString(unidadespacial.centrosDeCultivo.codigoCentro);
                    }

                    Titulares.Text = Convert.ToString(dt.Rows[0]["titulares"]);
                    Comuna.Text = Convert.ToString(dt.Rows[0]["comunas"]);
                    //EAutorizadas.Text = solicitudConcesion.DescripcionEspeciesComa;
                };
            }
            catch { };

            CargaCombobox();
        }


        protected void Volver_Click(object sender, EventArgs e)
        {

            int origen = Convert.ToInt32(ViewState["origen"]);
            string path = "";

            if (origen == 1)
            {
                path = "~/Administrador/ReportesP3/reportesConcesion.aspx";
            }
            else if (origen == 2)
            {
                path = "~/Administrador/ReportesP3/ReportesAcuiculturaAmerb.aspx";

            }
            else if (origen == 3)
            {
                path = "~/Administrador/ReportesP3/ReportesCentroAcopio.aspx";

            }
            else if (origen == 4)
            {
                path = "~/Administrador/ReportesP3/ReportesCentroFaenamiento.aspx";

            }
            else if (origen == 5)
            {
                path = "~/Administrador/ReportesP3/ReportesColectoresSemillas.aspx";

            }
            else {

                path = "~/Administrador/ReportesP3/reportesConcesion.aspx";
            }




            Response.Redirect(path);
        }


        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            CargaGrilla();

            GridView grilla = GridView1;
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export("detalleCentroCultivo.xls", grilla);
        }


        protected void CargaCombobox()
        {
            Anio.Items.Clear();
            for (int i = DateTime.Now.Year; i >= 2000; i--)
            {
                Anio.Items.Add(new ListItem(i.ToString(), i.ToString()));
            };
            Mes.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                Mes.Items.Add(new ListItem(fnc.Mes(i), i.ToString()));
            };
        }


        protected void Filtrar_Click(object sender, EventArgs s)
        {
            // Cargamos la grilla
            CargaGrilla();
            detalle.Visible = true;
        }

        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {

            if (!IdCentroCultivo.Text.Trim().Equals("")) {

                // Cargamos los filtros
                int id_centrocultivo = Convert.ToInt32(IdCentroCultivo.Text);
                int mes = Convert.ToInt32(Mes.SelectedValue);
                int anio = Convert.ToInt32(Anio.SelectedValue);

                // Iniciamos la query de búsqueda y cargamos la grilla
                DataTable dt = new DataTable();
                int num_registros = 0;

                dt = centroCultivoDA.CentrosDeCultivo_Detalle_Listar(id_centrocultivo, mes, anio);
                if (dt != null)
                {
                    num_registros = dt.Rows.Count;
                    if (num_registros > 0)
                    {
                        if (num_registros == 1)
                        {
                            msgGrilla.Text = "Se ha encontrado 1 período relacionado al centro de cultivo.";
                        }
                        else
                        {
                            msgGrilla.Text = "Se han encontrado " + num_registros + " períodos relacionados al centro de cultivo.";
                        };
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        Content_msgGrilla.Visible = true;
                        ExportarGrilla.Visible = true;
                    }
                    else
                    {
                        msgGrilla.Text = "No se encontraron períodos relacionados al centro de cultivo.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        Content_msgGrilla.Visible = true;
                        ExportarGrilla.Visible = false;
                    };

                    // Se carga el datatable en el gridview
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    string script = @"<script type='text/javascript'>oculta_loading('cargando');</script>";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "mensaje_cargado", script, false);
                }
                else
                {
                    Response.Redirect("~/Administrador/Reportes/reportes.aspx?id_tiporeporte=6");
                };
            
            }

        }


        protected void GridView1_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

    }
}
