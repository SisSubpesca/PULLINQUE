using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Contantes;
using Datos.Utilidades;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.common;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace SubPesca.Unidades.Concesion
{
    public partial class arrendatarioUnidadEspacialComponente : System.Web.UI.UserControl
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
                CargaGrilla();
            }
        }

        protected void GridViewArrendatarios_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridViewArrendatarios.PageIndex = e.NewPageIndex;
            GridViewArrendatarios.DataBind();
            CargaGrilla();
        }

        protected void GridViewArrendatarios_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridSolicitante = (GridView)sender;


                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Arrendatarios";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridSolicitante.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        private void CargaGrilla()
        {
            SolicitudConcesion concesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

            if (concesion == null || usuario_logeado == null)
            {
                Response.Redirect(ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"].ToString());
            }

            if (concesion != null && concesion.idSolConcesion > 0)
            {
                UnidadEspacial unidadEspacial = unidadEspacialDA.ObtieneUnidadEspacial(concesion.idSolConcesion,0);

                if (unidadEspacial != null && unidadEspacial.centrosDeCultivo != null && Convert.ToInt32(unidadEspacial.centrosDeCultivo.codigoCentro) > 0)
                {
                    //Aquí se debe ejecutar el PL para obtener arrendatarios
                    DataTable dt = dataExternaDA.Listar_SSP_TramiteArriendoRCA(Convert.ToInt32(unidadEspacial.centrosDeCultivo.codigoCentro));
                    if (dt != null)
                    {
                        GridViewArrendatarios.DataSource = dt;
                        GridViewArrendatarios.DataBind();
                    }
                }
            }
        }

    }
}