using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using Datos.Contantes;

namespace SubPesca.Solicitudes.Colector
{
    public partial class pestanaResolucionSSPColector : System.Web.UI.Page
    {
        DespliegueSeccionService despliegueSeccionService = new DespliegueSeccionService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PanelResolucionSSP.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.RESOLUCION_SSP, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
                PanelResolucionSSPDevolucionSSFFAA.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DEVOLUCION_SSFFAA, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
            }   
        }
    }
}