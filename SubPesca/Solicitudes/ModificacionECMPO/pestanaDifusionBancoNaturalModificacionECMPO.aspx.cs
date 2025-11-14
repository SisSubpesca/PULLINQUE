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

namespace SubPesca.Solicitudes.ModificacionECMPO
{
    public partial class pestanaDifusionBancoNaturalModificacionECMPO : System.Web.UI.Page
    {
        SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();
        DespliegueMenuSeccionDA despliegueMenuSeccionDA = new DespliegueMenuSeccionDA();

        protected void Page_Load(object sender, EventArgs e)
        {
            // PAGE LOAD
            if (!Page.IsPostBack)
            {

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[paginas.solicitudModificacionECMPOSession];

                if (solicitudConcesion == null)
                {
                    Response.Redirect("~/Solicitudes/ModificacionECMPO/administrarSolicitudModificacionECMPO.aspx");
                }


                /* Difusión de Banco Natural */
                List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.DIFUSION_BANCO_NATURAL_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_ECMPO);
                if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
                {
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.DIFUSION_BANCO_NATURAL))
                    {
                        PanelDifusionBancoNatural.Visible = true;
                    }
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_DIFUSION_BANCO_NATURAL))
                    {
                        PanelDifusionBancoNaturalObservacion.Visible = true;
                    }

                }

            }
        }
    }
}