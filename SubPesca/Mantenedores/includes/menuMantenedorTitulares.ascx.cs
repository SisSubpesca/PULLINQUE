using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace SubPesca.Mantenedores.includes
{
    public partial class menuMantenedorTitulares : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Mantenedor Titular"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Mantenedor Representante Legal"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Mantenedor Operador"));
        }

        protected HtmlGenericControl Crea_OpcionesModulo(string modulo)
        {
            Label texto = new Label();
            texto.Text = modulo;

            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.Controls.Add(texto);

            switch (modulo)
            {
                case "Mantenedor Titular":
                    //ul.Controls.Add(Crea_Link("Ingresar Nuevo", "~/Mantenedores/Titulares/agregarTitular.aspx"));
                    ul.Controls.Add(Crea_Link("Administrar", "~/Mantenedores/Titulares/administrarTitulares.aspx"));
                    break;

                case "Mantenedor Representante Legal":
                    //ul.Controls.Add(Crea_Link("Ingresar Nuevo", "~/Mantenedores/Titulares/agregarRepresentanteLegal.aspx"));
                    ul.Controls.Add(Crea_Link("Administrar", "~/Mantenedores/Titulares/administrarRepresentantesLegales.aspx"));
                    break;

                case "Mantenedor Operador":
                    //ul.Controls.Add(Crea_Link("Ingresar Nuevo", "~/Mantenedores/Titulares/agregarOperador.aspx"));
                    ul.Controls.Add(Crea_Link("Administrar", "~/Mantenedores/Titulares/administrarOperadores.aspx"));
                    break;

            };

            return ul;
        }

        protected HtmlGenericControl Crea_Link(string opcion, string urldestino)
        {
            Image img = new Image();
            img.CssClass = "child";
            img.ImageUrl = "~/App_Themes/admin_style/images/child.gif";

            LinkButton link = new LinkButton();
            link.Text = opcion;
            link.PostBackUrl = urldestino;

            /*
            link.Command += new CommandEventHandler(WebSiteLink_Click);
            if (opcion == "WebSite Público")
            {
                link.OnClientClick = "aspnetForm.target = '_blank';";
            };
            */

            Panel div = new Panel();
            div.CssClass = "opc_menuv";
            div.Controls.Add(img);
            div.Controls.Add(link);

            HtmlGenericControl li = new HtmlGenericControl("li");
            li.Controls.Add(div);

            return li;
        }
    }
}