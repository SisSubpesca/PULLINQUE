using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.cierreForzado;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;

namespace SubPesca.CierreForzado
{
    public partial class administrarCierreForzadoColectores : System.Web.UI.Page
    {
        PermisosService permisosService = new PermisosService();
        CierreForzadoService cierreForzadoService = new CierreForzadoService();

        protected void Page_Load(object sender, EventArgs e)
        {

            // PAGE LOAD
            if (!Page.IsPostBack)
            {
                if (PreviousPage != null && PreviousPage is GenerarCierreForzadoColectores)
                {
                    MensajeSuperior.Text = ((GenerarCierreForzadoColectores)PreviousPage).MensajeRegistro;
                    if (!MensajeSuperior.Text.Equals(""))
                    {
                        PanelMensajeSuperior.Visible = true;
                        UpdatePanelMensajeSuperior.Update();
                    }
                }

                if (PreviousPage != null && PreviousPage is CierreForzadoResolucionColectores)
                {
                    MensajeSuperior.Text = ((CierreForzadoResolucionColectores)PreviousPage).MensajeRegistro;
                    if (!MensajeSuperior.Text.Equals(""))
                    {
                        PanelMensajeSuperior.Visible = true;
                        UpdatePanelMensajeSuperior.Update();
                    }
                }
            }
        }



        protected void FiltrarCargaGrilla(object sender, EventArgs e)
        {

            msgGrilla_Sol.Text = "";
            PanelSolicitudesMsg.Visible = false;
            UpdatePanelSolicitudes.Update();


            int idInforme = 0;
            string numPert = "";

            if (!InformeTecnicoCierre.Text.Trim().Equals(""))
            {
                idInforme = Convert.ToInt32(InformeTecnicoCierre.Text);
            }

            if (!NPert.Text.Trim().Equals(""))
            {
                numPert = NPert.Text;
            }


            ViewState["InformeTecnicoCierre"] = idInforme;
            ViewState["NPert"] = numPert;

            CargaGrilla(idInforme, numPert);


        }


        protected void CargaGrilla(int idInforme, string numPert)
        {
            System.Data.DataTable dataTableCierreForzado = cierreForzadoService.ListarDocGralCierreIT(idInforme, rbTipo.COLECTORES_DE_SEMILLA, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA, numPert);

            if (dataTableCierreForzado.Rows.Count > 0)
            {
                ExportarGrilla.Visible = true;
            }
            else
            {
                ExportarGrilla.Visible = false;
            }

            GridCierreForzado.DataSource = dataTableCierreForzado;
            GridCierreForzado.DataBind();
        }


        protected void GridCierreForzado_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridCierreForzado.PageIndex = e.NewPageIndex;
            CargaGrilla(Convert.ToInt32(ViewState["InformeTecnicoCierre"]), ViewState["NPert"].ToString());


        }

        protected void GridCierreForzado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_CIERRE_FORZADO_SOLICITUD_COLECTOR }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.ACCESO))
                    {
                        boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar?')");
                        boton_eliminar.Visible = true;
                    }
                };

            };
        }

        protected void GridCierreForzado_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int id = 0;

            switch (e.CommandName)
            {

                case "AsignarResolucionSSP":

                    id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/CierreForzado/CierreForzadoResolucionColectores.aspx?idDocCierre=" + id);

                    break;

                case "EliminarCierreForzado":

                    id = Convert.ToInt32(e.CommandArgument);
                    GridCierreForzado.EditIndex = -1;
                    eliminarCierreForzado(id);

                    break;

            }
        }


        protected void eliminarCierreForzado(int id)
        {

            bool resp = cierreForzadoService.EliminarDocGralCierreForzado(id);

            if (resp)
            {
                msgGrilla_Sol.Text = "Se ha eliminado el informe exitosamente.";
                PanelSolicitudesMsg.Visible = true;

                this.CargaGrilla(0, null);
            }
            else
            {
                msgGrilla_Sol.Text = "No se ha eliminado el informe.";
                PanelSolicitudesMsg.Visible = true;

                this.CargaGrilla(0, null);
            }
            Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            UpdatePanelSolicitudes.Update();
        }

        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();
            string nom_grilla = "cierreForzadoSolicitudColectores";
            string ngrilla = "";

            CargaGrilla(Convert.ToInt32(ViewState["InformeTecnicoCierre"]), ViewState["NPert"].ToString());

            switch (nom_grilla)
            {
                case "cierreForzadoSolicitudColectores":
                    GridCierreForzado.Columns.RemoveAt(4);
                    grilla = GridCierreForzado;
                    ngrilla = "cierreForzadoSolicitudColectores.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

    }
}