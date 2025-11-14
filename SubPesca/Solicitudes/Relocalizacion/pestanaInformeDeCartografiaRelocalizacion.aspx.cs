using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using Datos.Contantes;
using Datos.Entidades.Relocalizacion;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;
using Datos.Entidades;
using Datos.Utilidades;

namespace SubPesca.Solicitudes.Relocalizacion
{
    public partial class pestanaInformeDeCartografiaRelocalizacion : System.Web.UI.Page
    {
        DespliegueSeccionService despliegueSeccionService = new DespliegueSeccionService();
        RelocalizacionService relocalizacionService = new RelocalizacionService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                setearModulo();
                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                DetalleSector aDetalleSector = relocalizacionService.obtenerDetalleSector_Solicitud(solicitudConcesion.idSolConcesion);

                PanelInforCart.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ITC_UOT, aDetalleSector.tipoRelocalizacion.id);
                PanelunidadDeDependencia.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.UNIDADES_DE_DEPENDENCIA, aDetalleSector.tipoRelocalizacion.id);
                
            }
        }

        protected void setearModulo()
        {
            Funciones funciones = new Funciones();

            if (funciones.retornaModulo().Equals("Registrar"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLCONCESION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLCONCESION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLCONCESION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLCONCESION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLCONCESION;
                ViewState["solicitudSession"] = paginas.solicitudConcesionSession;
            }
            else if (funciones.retornaModulo().Equals("Relocalizacion"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_RELOCALIZACION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_RELOCALIZACION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_RELOCALIZACION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_RELOCALIZACION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_RELOCALIZACION;
                ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSession;
            }
            else
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLMOD;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLMOD;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLMOD;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLMOD;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLMOD;
                ViewState["solicitudSession"] = paginas.solicitudModificacionSession;
            }
        }
    }
}