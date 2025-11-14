using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Datos.Utilidades;
using Datos.Contantes;
using Datos.Entidades;

namespace SubPesca.Unidades.includes
{
    public partial class menuConcesionAcuicultura : System.Web.UI.UserControl
    {

        Funciones funciones = new Funciones();


        protected void Page_Load(object sender, EventArgs e)
        {

            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Menú Concesión de Acuicultura"));
            //Content_MenuV.Controls.Add(Crea_OpcionesModulo("Portal"));

        }

        protected HtmlGenericControl Crea_OpcionesModulo(string modulo)
        {
            Label texto = new Label();

            if (modulo.Equals("Menú Concesión de Acuicultura")) {

                
                    SolicitudConcesion concesion = (Datos.Entidades.SolicitudConcesion)Session[paginas.solicitudConcesionSession];

                    if (concesion != null && concesion.unidadEspacial != null && concesion.unidadEspacial.centrosDeCultivo != null)
                    {
                        texto.Text = modulo + " " + concesion.unidadEspacial.centrosDeCultivo.codigoCentro;
                    }
                    else {
                        texto.Text = modulo;
                    }
                

            }else{
                texto.Text = modulo;
            }



            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.Controls.Add(texto);

            switch (modulo)
            {
                case "Menú Concesión de Acuicultura":
                    ul.Controls.Add(Crea_Link("Resumen de Concesión de Acuicultura", "~/Unidades/Concesion/resumenConcesion.aspx"));
                    ul.Controls.Add(Crea_Link("Titular", "~/Unidades/Concesion/titularConcesion.aspx"));
                    ul.Controls.Add(Crea_Link("Antecedentes del Sector", "~/Unidades/Concesion/antecedentesDelSectorConcesion.aspx"));
                    ul.Controls.Add(Crea_Link("Proyecto Técnico", "~/Unidades/Concesion/proyectoTecnicoConcesion.aspx"));
                    ul.Controls.Add(Crea_Link("Referencias Productivas", "~/Unidades/Concesion/referenciasProductivaConcesion.aspx"));
                    ul.Controls.Add(Crea_Link("Referencias Ambientales", "~/Unidades/Concesion/referenciasAmbientalesConcesion.aspx"));
                    ul.Controls.Add(Crea_Link("Referencias Sanitarias", "~/Unidades/Concesion/referenciasSanitariasConcesion.aspx"));
                    ul.Controls.Add(Crea_Link("Resoluciones", "~/Unidades/Concesion/resolucionesConcesion.aspx"));
                    ul.Controls.Add(Crea_Link("Trámites Asociados", "~/Unidades/Concesion/tramitesAsociadosConcesion.aspx"));
                    ul.Controls.Add(Crea_Link("Unidad Espacial", "~/Unidades/Concesion/unidadesEspacialesConcesion.aspx"));
                    ul.Controls.Add(Crea_Link("Administrar Documentos", "~/Unidades/Concesion/administrarDocumentoConcesion.aspx"));
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