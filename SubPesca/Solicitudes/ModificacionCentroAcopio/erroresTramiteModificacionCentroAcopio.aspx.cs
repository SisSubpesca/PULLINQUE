using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.modificacion;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.modificacion;

namespace SubPesca.Solicitudes.ModificacionCentroAcopio
{
    public partial class erroresTramiteModificacionCentroAcopio : System.Web.UI.Page
    {
        SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();

        AlertaTramiteModConcesionDA alertaTramiteModConcesionDA = new AlertaTramiteModConcesionDA();
        SolicitudDA solicitudDA = new SolicitudDA();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["idSolConces"] != null)
                {
                    int idSolicitudConcesion = Convert.ToInt32(Request.QueryString["idSolConces"]);

                    SolicitudConcesion solicitudModificacion = solicitudDA.ObtieneSolicitudConcesionMod(idSolicitudConcesion, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                    solicitudModificacionService.recalcularErroresSolicitud(solicitudModificacion);

                    despliegaErroresSolicitudModificacion(solicitudModificacion);
                }
            }
        }

        private void despliegaErroresSolicitudModificacion(SolicitudConcesion solicitudModificacion)
        {

            if (solicitudModificacion != null)
            {
                numeroPert.Text = Convert.ToString(solicitudModificacion.numPert);
                GridErroresSolicitud.DataSource = alertaTramiteModConcesionDA.ListarAlertaTramiteModConcesion(solicitudModificacion.idSolConcesion, 0, 0);
                GridErroresSolicitud.DataBind();
            }

        }
    }
}