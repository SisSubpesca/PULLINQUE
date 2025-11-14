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
    public partial class menuColectorSemillas : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Menú Colector de Semillas"));


        }

        protected HtmlGenericControl Crea_OpcionesModulo(string modulo)
        {
            Label texto = new Label();

            if (modulo.Equals("Menú Colector de Semillas"))
            {

                SolicitudConcesion concesion = (Datos.Entidades.SolicitudConcesion)Session[paginas.solicitudColectorSession];

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
                case "Menú Colector de Semillas":
                    ul.Controls.Add(Crea_Link("Resumen de Colector de Semillas", "~/Unidades/Colector/resumenColector.aspx"));
                    ul.Controls.Add(Crea_Link("Titular", "~/Unidades/Colector/titularColector.aspx"));
                    ul.Controls.Add(Crea_Link("Antecedentes del Sector", "~/Unidades/Colector/antecedentesDelSectorColector.aspx"));
                    ul.Controls.Add(Crea_Link("Proyecto Técnico", "~/Unidades/Colector/proyectoTecnicoColector.aspx"));
                    ul.Controls.Add(Crea_Link("Referencias Productivas", "~/Unidades/Colector/referenciasProductivaColector.aspx"));
                    ul.Controls.Add(Crea_Link("Referencias Ambientales", "~/Unidades/Colector/referenciasAmbientalesColector.aspx"));
                    ul.Controls.Add(Crea_Link("Referencias Sanitarias", "~/Unidades/Colector/referenciasSanitariasColector.aspx"));
                    ul.Controls.Add(Crea_Link("Resoluciones", "~/Unidades/Colector/resolucionesColector.aspx"));
                    ul.Controls.Add(Crea_Link("Trámites Asociados", "~/Unidades/Colector/tramitesAsociadosColector.aspx"));
                    ul.Controls.Add(Crea_Link("Unidad Espacial", "~/Unidades/Colector/unidadesEspacialesColectores.aspx"));
                    ul.Controls.Add(Crea_Link("Administrar Documentos", "~/Unidades/Colector/administrarDocumentoColector.aspx"));
                    break;
            };

            return ul;
        }

        protected HtmlGenericControl Crea_Link(string opcion, string urldestino)
        {
            /*
            Image img = new Image();
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
            /*div.Controls.Add(img);*/
            div.Controls.Add(link);

            HtmlGenericControl li = new HtmlGenericControl("li");
            li.Controls.Add(div);

            return li;
        }
    }
}