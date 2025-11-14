using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using Datos.Utilidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using System.Data;

namespace SubPesca.Unidades.Concesion
{
    public partial class referenciasProductivaConcesionComponente : System.Web.UI.UserControl
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        Funciones funciones = new Funciones();
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

                CargaGrilla();
            }
        }

        protected void GridViewProductiva_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridViewProductiva.PageIndex = e.NewPageIndex;
            GridViewProductiva.DataBind();
            CargaGrilla();
        }

        private void CargaGrilla()
        {
            ExportarGrilla.Visible = false;
            UnidadEspacial unidadEspacial = unidadEspacialDA.ObtieneUnidadEspacial(Convert.ToInt32(IdSolicitud.Value), 0);

            if (unidadEspacial != null && unidadEspacial.centrosDeCultivo != null && Convert.ToInt32(unidadEspacial.centrosDeCultivo.codigoCentro) > 0)
            {
                DataTable dt = dataExternaDA.Listar_SSP_DatosProduccion(Convert.ToInt32(unidadEspacial.centrosDeCultivo.codigoCentro));

                if (dt != null && dt.Rows.Count > 0)
                {
                    GridViewProductiva.DataSource = dt;
                    GridViewProductiva.DataBind();
                    PanelSolicitudesMsg.Visible = false;
                    ExportarGrilla.Visible = true;
                }
                else
                {

                    msgGrilla.Text = "No existe referencias productivas asociadas a la unidad espacial.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    PanelSolicitudesMsg.Visible = true;
                }
            }
            else {

                msgGrilla.Text = "No existe referencias productivas asociadas a la unidad espacial.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                PanelSolicitudesMsg.Visible = true;
            }
        }

        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();

            CargaGrilla();

            grilla = GridViewProductiva;
            grilla.AllowPaging = false;
            grilla.DataBind();

            SubPesca.Utilidades.GridViewExportUtil.Export("ReferenciasProductivas.xls", grilla);
        }


    }
}