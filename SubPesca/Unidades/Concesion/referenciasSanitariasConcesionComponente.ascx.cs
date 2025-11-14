using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using Datos.Utilidades;
using Datos.Contantes;
using SubPesca.Mantenedores.Generales;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.common;
using System.Data;


namespace SubPesca.Unidades.Concesion
{
    public partial class referenciasSanitariasConcesionComponente : System.Web.UI.UserControl
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        Funciones funciones = new Funciones();
        MantenedorGeneralService mantenedorGeneralService = new MantenedorGeneralService();
        SolicitudDA solicitudDA = new SolicitudDA();
        DataExternaDA dataExternaDA = new DataExternaDA();
        UnidadEspacialDA unidadEspacialDA = new UnidadEspacialDA();

        protected void setearModulo()
        {
            if (funciones.retornaModulo().Equals("Concesion"))
            {
                ViewState["solicitudSession"] = paginas.solicitudConcesionSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_CONCESION_DE_ACUICULTURA;
            }

            else if (funciones.retornaModulo().Equals("Acopio"))
            {
                ViewState["solicitudSession"] = paginas.solicitudAcopioSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_CENTRO_ACOPIO;
            }
            else if (funciones.retornaModulo().Equals("Faenamiento"))
            {
                ViewState["solicitudSession"] = paginas.solicitudFaenamientoSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_CENTRO_DE_FAENAMIENTO;
            }

            else if (funciones.retornaModulo().Equals("Amerb"))
            {
                ViewState["solicitudSession"] = paginas.solicitudAmerbSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_CENTRO_EN_AMERB;
            }

            else if (funciones.retornaModulo().Equals("Colector"))
            {
                ViewState["solicitudSession"] = paginas.solicitudColectorSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_COLECTOR_DE_SEMILLAS;
            }
            else if (funciones.retornaModulo().Equals("ECMPO"))
            {
                ViewState["solicitudSession"] = paginas.solicitudECMPOSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_ECMPO;
            }
            else if (funciones.retornaModulo().Equals("ExperimentalesAmerb"))
            {
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesAmerbSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_EXPERIMENTALES_AMERB;
            }
            else if (funciones.retornaModulo().Equals("ExperimentalesConcesion"))
            {
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesConcesionSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_EXPERIMENTALES_CONCESION;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                setearModulo();

                // Inicializamos el formulario
                Initialize_Form();
            }
        }

        private void Initialize_Form()
        {
            SolicitudConcesion concesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

            if (concesion == null || usuario_logeado == null)
            {
                Response.Redirect(ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"].ToString());
            }

            if (concesion != null && concesion.idSolConcesion > 0)
            {
                //Aquí se debe cargar la información de la página.
                NombreUnidadEspacial.Text = funciones.retornaModulo();
                IdSolicitud.Value = Convert.ToString(concesion.idSolConcesion);
            }

            cargarDescansosSanitarios();

            CargaGrillaInformacionSanitaria();
        }

        private void cargarDescansosSanitarios()
        {
            ExportarGrilla.Visible = false;
            SolicitudConcesion concesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

            SolicitudConcesion solicitudConcesion = solicitudDA.ObtieneSolicitudConcesion(concesion.idSolConcesion, 0);

            if (solicitudConcesion != null && solicitudConcesion.barrio != null)
            {
                // Iniciamos la query de búsqueda y cargamos la grilla
                DescansoSanitario descansoSanitario = new DescansoSanitario();
                descansoSanitario.barrio = new ParametroGenerico();
                descansoSanitario.barrio.id = solicitudConcesion.barrio.id_barrio;

                List<DescansoSanitario> dt = mantenedorGeneralService.listarDescansoSanitario(descansoSanitario);

                int num_registros = 0;
                num_registros = dt.Count;

                if (num_registros > 0)
                {
                    GridViewDescanso.DataSource = dt;
                    GridViewDescanso.DataBind();
                    PanelDescansosSanitarios.Visible = true;
                    PanelSolicitudesMsg.Visible = false;
                    ExportarGrilla.Visible = true;
                }
                else
                {

                    msgGrilla.Text = "No existe descansos sanitarios asociados a la unidad espacial.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    PanelDescansosSanitarios.Visible = true;
                    PanelSolicitudesMsg.Visible = true;
                }
            }
            else {
                msgGrilla.Text = "No existe descansos sanitarios asociados a la unidad espacial.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                PanelDescansosSanitarios.Visible = true;
                PanelSolicitudesMsg.Visible = true;
            }

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
            cargarDescansosSanitarios();
        }

        protected void GridViewDescanso_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridViewDescanso.PageIndex = e.NewPageIndex;
            GridViewDescanso.EditIndex = -1;
            cargarDescansosSanitarios();
        }

        protected void GridViewSanitaria_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridViewSanitaria.PageIndex = e.NewPageIndex;
            GridViewSanitaria.DataBind();
            CargaGrillaInformacionSanitaria();
        }

        private void CargaGrillaInformacionSanitaria()
        {
            ExportarGrilla2.Visible = false;
            UnidadEspacial unidadEspacial = unidadEspacialDA.ObtieneUnidadEspacial(Convert.ToInt32(IdSolicitud.Value), 0);

            if (unidadEspacial != null && unidadEspacial.centrosDeCultivo != null && Convert.ToInt32(unidadEspacial.centrosDeCultivo.codigoCentro) > 0)
            {
                DataTable dt = dataExternaDA.Listar_SSP_DatosInformacionSanitaria(Convert.ToInt32(unidadEspacial.centrosDeCultivo.codigoCentro));

                if (dt != null && dt.Rows.Count > 0)
                {
                    GridViewSanitaria.DataSource = dt;
                    GridViewSanitaria.DataBind();
                    PanelSanitaria.Visible = true;
                    PanelMensajeSanitaria.Visible = false;
                    ExportarGrilla2.Visible = true;
                }
                else
                {

                    msgGrillaSanitaria.Text = "No existe información sanitaria asociadas a la unidad espacial.";
                    ImageSanitaria.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    PanelSanitaria.Visible = true;
                    PanelMensajeSanitaria.Visible = true;
                }
            }
            else {
                msgGrillaSanitaria.Text = "No existe información sanitaria asociadas a la unidad espacial.";
                ImageSanitaria.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                PanelSanitaria.Visible = true;
                PanelMensajeSanitaria.Visible = true;
            
            }
        }


        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();

            cargarDescansosSanitarios();

            grilla = GridViewDescanso;
            grilla.AllowPaging = false;
            grilla.DataBind();

            SubPesca.Utilidades.GridViewExportUtil.Export("DescansoSanitarioACS.xls", grilla);
        }



        protected void ExportarGrilla2_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();

            CargaGrillaInformacionSanitaria();

            grilla = GridViewSanitaria;
            grilla.AllowPaging = false;
            grilla.DataBind();

            SubPesca.Utilidades.GridViewExportUtil.Export("InformacionSanitaria.xls", grilla);
        }
    }
}