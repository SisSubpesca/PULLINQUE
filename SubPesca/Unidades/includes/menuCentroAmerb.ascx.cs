using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Datos.Entidades;
using Datos.Contantes;

namespace SubPesca.Unidades.includes
{
    public partial class menuCentroAmerb : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Menú Acuicultura Amerb"));
            

        }

        protected HtmlGenericControl Crea_OpcionesModulo(string modulo)
        {
            Label texto = new Label();


            if (modulo.Equals("Menú Acuicultura Amerb"))
            {

                SolicitudConcesion concesion = (Datos.Entidades.SolicitudConcesion)Session[paginas.solicitudAmerbSession];

                if (concesion != null && concesion.unidadEspacial != null && concesion.unidadEspacial.centrosDeCultivo != null)
                {
                    texto.Text = modulo + " " + concesion.unidadEspacial.centrosDeCultivo.codigoCentro;
                }
                else
                {
                    texto.Text = modulo;
                }


            }
            else
            {
                texto.Text = modulo;
            }



            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.Controls.Add(texto);

            switch (modulo)
            {
                case "Menú Acuicultura Amerb":
                    ul.Controls.Add(Crea_Link("Resumen de Acuicultura Amerb", "~/Unidades/Amerb/resumenAmerb.aspx"));
                    ul.Controls.Add(Crea_Link("Titular", "~/Unidades/Amerb/titularAmerb.aspx"));
                    ul.Controls.Add(Crea_Link("Antecedentes del Sector", "~/Unidades/Amerb/antecedentesDelSectorAmerb.aspx"));
                    ul.Controls.Add(Crea_Link("Proyecto Técnico", "~/Unidades/Amerb/proyectoTecnicoAmerb.aspx"));
                    ul.Controls.Add(Crea_Link("Referencias Productivas", "~/Unidades/Amerb/referenciasProductivaAmerb.aspx"));
                    ul.Controls.Add(Crea_Link("Referencias Ambientales", "~/Unidades/Amerb/referenciasAmbientalesAmerb.aspx"));
                    ul.Controls.Add(Crea_Link("Referencias Sanitarias", "~/Unidades/Amerb/referenciasSanitariasAmerb.aspx"));
                    ul.Controls.Add(Crea_Link("Resoluciones", "~/Unidades/Amerb/resolucionesAmerb.aspx"));
                    ul.Controls.Add(Crea_Link("Trámites Asociados", "~/Unidades/Amerb/tramitesAsociadosAmerb.aspx"));
                    ul.Controls.Add(Crea_Link("Unidad Espacial", "~/Unidades/Amerb/unidadesEspacialesAmerb.aspx"));
                    ul.Controls.Add(Crea_Link("Administrar Documentos", "~/Unidades/Amerb/administrarDocumentoAmerb.aspx"));
                    break;
            };

            return ul;
        }

        protected HtmlGenericControl Crea_Link(string opcion, string urldestino)
        {
            /*Image img = new Image();
            img.CssClass = "child";
            img.ImageUrl = "~/App_Themes/admin_style/images/child.gif";
            */
            LinkButton link = new LinkButton();
            link.Text = opcion;
            link.PostBackUrl = urldestino;
            link.CausesValidation = false;

            /*
            link.Command += new CommandEventHandler(WebSiteLink_Click);
            if (opcion == "WebSite Público")
            {
                link.OnClientClick = "aspnetForm.target = '_blank';";
            };
            */

            Panel div = new Panel();
            div.CssClass = "opc_menuv";
           /* div.Controls.Add(img);*/
            div.Controls.Add(link);

            HtmlGenericControl li = new HtmlGenericControl("li");
            li.Controls.Add(div);

            return li;
        }
    }
}