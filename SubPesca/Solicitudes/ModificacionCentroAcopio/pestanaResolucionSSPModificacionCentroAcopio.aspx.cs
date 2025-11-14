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

namespace SubPesca.Solicitudes.ModificacionCentroAcopio
{
    public partial class pestanaResolucionSSPModificacionCentroAcopio : System.Web.UI.Page
    {
        SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();
        DespliegueMenuSeccionDA despliegueMenuSeccionDA = new DespliegueMenuSeccionDA();

        protected void Page_Load(object sender, EventArgs e)
        {
            // PAGE LOAD
            if (!Page.IsPostBack)
            {
                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[paginas.solicitudModificacionCentroAcopioSession];

                if (solicitudConcesion == null)
                {
                    Response.Redirect("~/Solicitudes/ModificacionCentroAcopio/administrarSolicitudModificacionCentroAcopio.aspx");
                }


                /* Resolución SSP */
                List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.RESOLUCION_SSP_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_CENTRO_DE_ACOPIO);
                if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
                {
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.RESOLUCION_SSP))
                    {
                        PanelResolucionSSP.Visible = true;
                    }
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.DEVOLUCION_SSFFAA))
                    {
                        PanelResolucionSSPDevolucionSSFFAA.Visible = true;
                    }
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_RESOLUCION_SSP))
                    {
                        PanelResolucionSSPObservacion.Visible = true;
                    }

                }


            }
        }
    }
}