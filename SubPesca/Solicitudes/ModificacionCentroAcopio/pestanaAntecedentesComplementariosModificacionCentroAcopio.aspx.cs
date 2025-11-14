using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.servicios.modificacion;
using Datos.Entidades;
using Datos.Contantes;

namespace SubPesca.Solicitudes.ModificacionCentroAcopio
{
    public partial class pestanaAntecedentesComplementariosModificacionCentroAcopio : System.Web.UI.Page
    {
        DespliegueMenuSeccionDA despliegueMenuSeccionDA = new DespliegueMenuSeccionDA();
        SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();


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


                /* Antecedentes Complementarios */
                List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.ANTECEDENTES_COMPLEMENTARIOS_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_CENTRO_DE_ACOPIO);
                if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
                {
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.ANTECEDENTES_COMPLEMENTARIOS))
                    {
                        PanelAntecedentesComplementarios.Visible = true;
                    }
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.CERTIFICADO_REGISTRO_OPERACION))
                    {
                        PanelCertificadoOperacion.Visible = true;
                    }
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_ANTECEDENTES_COMPLEMENTARIOS))
                    {
                        PanelAntecedentesComplementariosObservacion.Visible = true;
                    }

                }
            }
        }
    }
}