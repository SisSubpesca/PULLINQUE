using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace SubPesca.BuscadorTramites
{
    
    
    
    public partial class buscadorDeTramites : System.Web.UI.Page
    {

        
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        SolicitudDA solicitudDA = new SolicitudDA();

        protected void Page_Load(object sender, EventArgs e)
        {
             // PAGE LOAD
            if (!Page.IsPostBack)
            {
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (usuario_logeado == null)
                {
                    Response.Redirect("~/ingreso.aspx");

                }

            }
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            ErroresGrilla.Text = "";
            PanelErroresGrilla.Visible = false;
            UpdatePanelErroresGrilla.Update();


            SolicitudConcesion solicitudConcesionFiltro = new SolicitudConcesion();
            solicitudConcesionFiltro.numPert = Identificador.Text;

            Session["Filtro_Buscador"] = solicitudConcesionFiltro;
            CargaGrilla();

        }

        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();

            CargaGrilla();

            grilla = GridUE;

            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export("BuscadorDeTramites.xls", grilla);
        }

        protected void GridUE_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            SolicitudConcesion solicitudConcesionFiltro = (SolicitudConcesion)Session["Filtro_Buscador"];
            if (solicitudConcesionFiltro == null)
            {
                solicitudConcesionFiltro = new SolicitudConcesion();
            }

            solicitudConcesionFiltro.pagina = e.NewPageIndex;
            Session["Filtro_Buscador"] = solicitudConcesionFiltro;

            GridUE.PageIndex = e.NewPageIndex;
            GridUE.DataBind();
            CargaGrilla();
        }

        private void CargaGrilla()
        {
            int pagina = 0;
            ExportarGrilla.Visible = false;

            SolicitudConcesion solicitudConcesionFiltro = new SolicitudConcesion();
            
            try
            {
                solicitudConcesionFiltro = (SolicitudConcesion)Session["Filtro_Buscador"];
                pagina = solicitudConcesionFiltro.pagina;
            }
            catch { };

            if (solicitudConcesionFiltro == null)
            {
                solicitudConcesionFiltro = new SolicitudConcesion();
                Session["Filtro_Buscador"] = solicitudConcesionFiltro;
            }

            List<SolicitudConcesion> solicitudConcesionList = solicitudDA.ListarSolicitudFiltroIdentificador(solicitudConcesionFiltro.numPert);
            
            GridUE.DataSource = solicitudConcesionList;
            GridUE.DataBind();
            UpdatePanelUE.Update();

            if (solicitudConcesionList != null && solicitudConcesionList.Count > 0)
            {
                ExportarGrilla.Visible = true;
            }
            else {
                ErroresGrilla.Text = "No existe solicitud de unidad espacial, trámite asociado o unidad espacial ya creada asociado a filtro de búsqueda ingresado.";
                PanelErroresGrilla.Visible = true;
                UpdatePanelErroresGrilla.Update();
            
            }
        }
    }
}