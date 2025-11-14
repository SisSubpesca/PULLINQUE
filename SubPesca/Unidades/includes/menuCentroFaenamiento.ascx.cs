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
    public partial class menuCentroFaenamiento : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Menú Centro Faenamiento"));

        }

        protected HtmlGenericControl Crea_OpcionesModulo(string modulo)
        {
            Label texto = new Label();



            if (modulo.Equals("Menú Centro Faenamiento"))
            {

                SolicitudConcesion concesion = (Datos.Entidades.SolicitudConcesion)Session[paginas.solicitudFaenamientoSession];

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
                case "Menú Centro Faenamiento":
                    ul.Controls.Add(Crea_Link("Resumen de Centro Faenamiento", "~/Unidades/Faenamiento/resumenFaenamiento.aspx"));
                    ul.Controls.Add(Crea_Link("Titular", "~/Unidades/Faenamiento/titularFaenamiento.aspx"));
                    ul.Controls.Add(Crea_Link("Antecedentes del Sector", "~/Unidades/Faenamiento/antecedentesDelSectorFaenamiento.aspx"));
                    ul.Controls.Add(Crea_Link("Proyecto Técnico", "~/Unidades/Faenamiento/proyectoTecnicoFaenamiento.aspx"));
                    ul.Controls.Add(Crea_Link("Referencias Productivas", "~/Unidades/Faenamiento/referenciasProductivaFaenamiento.aspx"));
                    ul.Controls.Add(Crea_Link("Referencias Ambientales", "~/Unidades/Faenamiento/referenciasAmbientalesFaenamiento.aspx"));
                    ul.Controls.Add(Crea_Link("Referencias Sanitarias", "~/Unidades/Faenamiento/referenciasSanitariasFaenamiento.aspx"));
                    ul.Controls.Add(Crea_Link("Resoluciones", "~/Unidades/Faenamiento/resolucionesFaenamiento.aspx"));
                    ul.Controls.Add(Crea_Link("Trámites Asociados", "~/Unidades/Faenamiento/tramitesAsociadosFaenamiento.aspx"));
                    ul.Controls.Add(Crea_Link("Unidad Espacial", "~/Unidades/Faenamiento/unidadesEspacialesFaenamiento.aspx"));
                    ul.Controls.Add(Crea_Link("Administrar Documentos", "~/Unidades/Faenamiento/administrarDocumentoFaenamiento.aspx"));
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
            /*div.Controls.Add(img);*/
            div.Controls.Add(link);

            HtmlGenericControl li = new HtmlGenericControl("li");
            li.Controls.Add(div);

            return li;
        }
    }
}