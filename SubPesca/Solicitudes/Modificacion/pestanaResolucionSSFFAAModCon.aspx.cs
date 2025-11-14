using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.modificacion;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Contantes;
using Datos.Entidades;

namespace SubPesca.Solicitudes.Modificacion
{
    public partial class pestanaResolucionSSFFAAModCon : System.Web.UI.Page
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


                /* Resolución SSFFAA */
                List<DespliegueMenuSeccion>  despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.RESOLUCION_SSFFAA_MODIFICACION, 0);
                if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
                {
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.RESOLUCION_SSFFAA))
                    {
                        PanelResolucionSSFFAA.Visible = true;
                    }
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_RESOLUCION_SSFFAA))
                    {
                        PanelResolucionSSFFAAObservacion.Visible = true;
                    }
                    
                }
            }
        }

    }
}