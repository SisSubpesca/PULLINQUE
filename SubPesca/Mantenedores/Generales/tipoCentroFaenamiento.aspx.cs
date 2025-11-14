using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SubPesca.Mantenedores.Generales
{
    public partial class tipoCentroFaenamiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Volver_Click(object sender, EventArgs e)
        {
            string path = "~/Administrador/principal.aspx";
            Response.Redirect(path);
        }
    }
}