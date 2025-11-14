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
    public partial class pestanaInformeDeCartografiaColector : System.Web.UI.Page
    {
        DespliegueSeccionService despliegueSeccionService = new DespliegueSeccionService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PanelEvaluacionOrdenamientoColector.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ITC_UOT, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
                //PanelunidadDeDependencia.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.UNIDADES_DE_DEPENDENCIA, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
            }   
        }
    }
}