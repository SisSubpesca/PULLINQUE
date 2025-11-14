using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;



namespace SubPesca.Solicitudes.Registrar
{
    public partial class verDocumento : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            // PAGE LOAD
            if (!Page.IsPostBack)
            {

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session["solicitudConcesion"];

                if (solicitudConcesion == null)
                {
                    Response.Redirect("~/Solicitudes/Registrar/administrarSolicitudConcesion.aspx");
                }



            }
        }
        
    }
}