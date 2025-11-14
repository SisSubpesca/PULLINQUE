using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Contantes;
using Datos.Entidades;
using System.Collections;
using System.Data;


namespace SubPesca.Solicitudes.Registrar
{
    public partial class ingresarDocumento : System.Web.UI.Page
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


    public class ValidationError : CustomValidator
    {
        public ValidationError(string group, string msg)
            : base()
        {
            base.ValidationGroup = group;
            base.ErrorMessage = msg;
            base.IsValid = false;
        }
    }
  
}