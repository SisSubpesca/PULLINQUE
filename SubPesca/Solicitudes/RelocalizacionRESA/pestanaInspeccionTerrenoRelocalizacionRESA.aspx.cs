using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;
using Datos.Entidades;
using Datos.Entidades.Relocalizacion;
using Datos.Contantes;

namespace SubPesca.Solicitudes.RelocalizacionRESA
{
    public partial class pestanaInspeccionTerrenoRelocalizacionRESA : System.Web.UI.Page
    {
        DespliegueSeccionService despliegueSeccionService = new DespliegueSeccionService();
        RelocalizacionRESAService relocalizacionRESAService = new RelocalizacionRESAService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                setearModulo();
                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion == null)
                {
                    Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
                }

                DetalleSector aDetalleSector = relocalizacionRESAService.obtenerDetalleSector_SolicitudRESA(solicitudConcesion.idSolConcesion);

                PanelInspeccionTerreno.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INSPECCION_TERRENO, aDetalleSector.tipoRelocalizacion.id);
                PanelInspeccionTerrenoCoordenadas.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.COORDENADAS_GEOGRAFICAS_INSPECCION_TERRENO, aDetalleSector.tipoRelocalizacion.id);
                
            }
        }

        protected void setearModulo()
        {
            ViewState["URL_VER"] = paginas.URL_VER_RELOCALIZACION_RESA;
            ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_RELOCALIZACION_RESA;
            ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA;
            ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_RELOCALIZACION_RESA;
            ViewState["URL_ERROR"] = paginas.URL_ERROR_RELOCALIZACION_RESA;
            ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSessionRESA;
        }
    }
}