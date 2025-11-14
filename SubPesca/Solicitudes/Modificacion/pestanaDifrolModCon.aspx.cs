using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.modificacion;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Entidades;
using Datos.Contantes;

namespace SubPesca.Solicitudes.Modificacion
{
    public partial class pestanaDifrolModCon : System.Web.UI.Page
    {

        SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();
        DespliegueMenuSeccionDA despliegueMenuSeccionDA = new DespliegueMenuSeccionDA();

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


                /* DIFROL */
                List<DespliegueMenuSeccion>  despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.DIFROL_MODIFICACION, 0);
                if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
                {
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.DIFROL))
                    {
                        PanelDifrol.Visible = true;
                        PanelDifrol.Visible = solicitudConcesion.comunaFronteriza;

                    }
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_DIFROL))
                    {
                        PanelDifrolObservacion.Visible = true;
                        PanelDifrolObservacion.Visible = solicitudConcesion.comunaFronteriza;
                    }
                }

            
            }
        }

    }
}