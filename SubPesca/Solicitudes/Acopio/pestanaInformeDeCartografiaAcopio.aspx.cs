using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;

namespace SubPesca.Solicitudes.Acopio
{
    public partial class pestanaInformeDeCartografiaAcopio : System.Web.UI.Page
    {
        DespliegueSeccionService despliegueSeccionService = new DespliegueSeccionService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PanelInforCart.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ITC_UOT, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO);
                PanelunidadDeDependencia.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.UNIDADES_DE_DEPENDENCIA, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO);
            }
        }
    }
}