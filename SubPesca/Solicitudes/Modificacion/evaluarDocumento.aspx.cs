using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.modificacion;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;

namespace SubPesca.Solicitudes.Modificacion
{
    public partial class evaluarDocumento : System.Web.UI.Page
    {
        /*
        RequerimientoService requerimientoService = new RequerimientoService();

        protected void Page_Load(object sender, EventArgs e)
        {

            // PAGE LOAD
            if (!Page.IsPostBack)
            {

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session["SolicitudModificacion"];

                if (solicitudConcesion == null)
                {
                    Response.Redirect("~/Solicitudes/Modificacion/administrarSolicitudModificacion.aspx");
                }


                IdSolicitud.Text = Convert.ToString(solicitudConcesion.idSolConcesion);
                nPert.Text = Convert.ToString(solicitudConcesion.numPert);

                if (Request.QueryString["idRequerimiento"] != null)
                {

                    Requerimiento requerimiento = requerimientoService.ObtenerRequerimiento(Convert.ToInt32(Request.QueryString["idRequerimiento"]));

                    if (requerimiento != null)
                    {
                        IdRequerimiento.Text = Convert.ToString(requerimiento.idRequerimiento);


                        if (requerimiento.estadoFinal != null && requerimiento.estadoFinal.id == rbEstadosGenerales.CONFORME)
                        {
                            PanelConforme.Visible = false;
                            UpdatePanelConforme.Update();

                        }

                        if (requerimiento.estadoFinal != null && requerimiento.estadoFinal.id == rbEstadosGenerales.NO_CONFORME)
                        {
                            PanelNoConfome.Visible = false;
                            UpdatePanelNoConfome.Update();
                        }

                    }
                }
            }
        }

        protected void Conforme_Click(object sender, EventArgs e)
        {

            SolicitudConcesion solicitudModificacion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

            if (solicitudModificacion == null)
            {
                Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
            }


            if (requerimientoService.ActualizarRequerimientoEstadoFinal(Convert.ToInt32(IdRequerimiento.Text), rbEstadosGenerales.CONFORME, solicitudModificacion.idSolConcesion))
            {
                PanelNoConfome.Visible = true;
                UpdatePanelNoConfome.Update();

                PanelConforme.Visible = false;
                UpdatePanelConforme.Update();

                Mensajes.Text = "Documento Actualizado con exito.";
                UpdatePanelMensajes.Update();
            }
            else
            {


                Mensajes.Text = "Ha ocurrido un error al actualizar el documento.";
                UpdatePanelMensajes.Update();

            }



        }

        protected void NoConfome_Click(object sender, EventArgs e)
        {

            SolicitudConcesion solicitudModificacion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

            if (solicitudModificacion == null)
            {
                Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
            }

            if (requerimientoService.ActualizarRequerimientoEstadoFinal(Convert.ToInt32(IdRequerimiento.Text), rbEstadosGenerales.NO_CONFORME, solicitudModificacion.idSolConcesion))
            {

                PanelNoConfome.Visible = false;
                UpdatePanelNoConfome.Update();

                PanelConforme.Visible = true;
                UpdatePanelConforme.Update();

                Mensajes.Text = "Documento Actualizado con exito.";
                UpdatePanelMensajes.Update();
            }
            else
            {

                Mensajes.Text = "Ha ocurrido un error al actualizar el documento.";
                UpdatePanelMensajes.Update();
            }



        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Solicitudes/Modificacion/informesResoluciones.aspx");

        }
        */

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}