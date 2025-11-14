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
    public partial class pestanaInspeccionTerrenoModCon : System.Web.UI.Page
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

                /* Inspección en Terreno */
                List<DespliegueMenuSeccion>  despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.INSPECCION_EN_TERRENO_MODIFICACION, 0);
                if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
                {
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.INSPECCION_TERRENO))
                    {
                        PanelInspeccionTerreno.Visible = true;
                    }

                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.COORDENADAS_GEOGRAFICAS_INSPECCION_TERRENO))
                    {
                        PanelInspeccionTerrenoCoordenadas.Visible = true;
                    }

                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_INSPECCION_TERRENO))
                    {
                        PanelInspeccionTerrenoObservacion.Visible = true;
                    }
                }
               
            }
        }

    }
}