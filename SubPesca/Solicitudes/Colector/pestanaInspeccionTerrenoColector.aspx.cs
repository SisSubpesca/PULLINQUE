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
    public partial class pestanaInspeccionTerrenoColector : System.Web.UI.Page
    {
        DespliegueSeccionService despliegueSeccionService = new DespliegueSeccionService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PanelInspeccionTerreno.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INSPECCION_TERRENO, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
                PanelInspeccionTerrenoCoordenadas.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.COORDENADAS_GEOGRAFICAS_INSPECCION_TERRENO, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
            }   
        }
    }
}