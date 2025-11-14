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
    public partial class pestanaInformeDACColector : System.Web.UI.Page
    {
        DespliegueSeccionService despliegueSeccionService = new DespliegueSeccionService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PanelInformeDAC.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INFORME_DAC, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
                PanelInformeDACDevolucionJuridica.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DEVOLUCION_JURIDICA, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
            }   
        }
    }
}