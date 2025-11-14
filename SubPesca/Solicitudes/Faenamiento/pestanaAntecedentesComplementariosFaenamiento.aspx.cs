using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;

namespace SubPesca.Solicitudes.Faenamiento
{
    public partial class pestanaAntecedentesComplementariosFaenamiento : System.Web.UI.Page
    {
        DespliegueSeccionService despliegueSeccionService = new DespliegueSeccionService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PanelAntecedentesComplementarios.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_COMPLEMENTARIOS, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO);
                PanelCertificadoOperacion.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.CERTIFICADO_REGISTRO_OPERACION, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO);
            }   
        }
    }
}