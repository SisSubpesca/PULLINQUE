using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace SubPesca.Mantenedores.includes
{
    public partial class menuMantenedorTransversal : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Mantenedor Transversal"));

        }

        protected HtmlGenericControl Crea_OpcionesModulo(string modulo)
        {
            Label texto = new Label();
            texto.Text = modulo;

            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.Controls.Add(texto);

            switch (modulo)
            {
                case "Mantenedor Transversal":
                    ul.Controls.Add(Crea_Link("Resultado", "~/Mantenedores/Transversales/resultado.aspx"));
                    ul.Controls.Add(Crea_Link("Asociación Subrequerimiento - Resultado", "~/Mantenedores/Transversales/asocSubrequerimientoResultado.aspx"));
                    ul.Controls.Add(Crea_Link("Tipo Documento", "~/Mantenedores/Transversales/tipoDocumento.aspx?bp=2"));
                    ul.Controls.Add(Crea_Link("Asociación Subrequerimiento - Tipo Documento", "~/Mantenedores/Transversales/asocSubrequerimientoTipo.aspx"));
                    ul.Controls.Add(Crea_Link("Tema Subrequerimiento", "~/Mantenedores/Transversales/temaSubrequerimiento.aspx"));
                    ul.Controls.Add(Crea_Link("Responsable DAC", "~/Mantenedores/Transversales/responsableDAC.aspx"));
                    ul.Controls.Add(Crea_Link("Preferencia de Relocalización", "~/Mantenedores/Transversales/preferenciaRelocalizacion.aspx"));
                    //ul.Controls.Add(Crea_Link("Materia de Resolución", "~/Mantenedores/Transversales/materiaResolucion.aspx"));
                    ul.Controls.Add(Crea_Link("Tipo Supeditado", "~/Mantenedores/Transversales/tipoSupeditado.aspx"));
                    ul.Controls.Add(Crea_Link("Estado UOT", "~/Mantenedores/Generales/estadosUOT.aspx"));
                    ul.Controls.Add(Crea_Link("Equivalencias Estado UOT", "~/Mantenedores/Generales/equivalenciaEstadoUOT.aspx"));

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