using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Datos.Entidades;

namespace SubPesca.Solicitudes.includes
{
    public partial class menuSolicitudesV : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Solicitud Concesión de Acuicultura"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Informes y Resoluciones"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Administrador de Documentos"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Portal"));
            
        }

        protected HtmlGenericControl Crea_OpcionesModulo(string modulo)
        {
            Label texto = new Label();
            texto.Text = modulo;

            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.Controls.Add(texto);


            SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session["solicitudConcesion"];
            if (solicitudConcesion == null)
            {
                Response.Redirect("~/Solicitudes/Registrar/administrarSolicitudConcesion.aspx");
            }


            string segmento = "";
            string nombre_archivo = "";
            string ext = "";
            int i = 0;
            int pos = 0;
            foreach (string S in HttpContext.Current.Request.Url.Segments)
            {
                segmento = HttpContext.Current.Request.Url.Segments[i];
                pos = segmento.LastIndexOf(".");
                if (pos > 0)
                {
                    ext = segmento.Substring(pos);
                    ext = ext.Replace(".", "");
                    if (ext == "aspx")
                    {
                        nombre_archivo = segmento;
                        break;
                    };
                };
                i++;
            };

         

            switch (modulo)
            {
                case "Solicitud Concesión de Acuicultura":
                    ul.Controls.Add(Crea_Link("Identificación del Solicitante", "~/Solicitudes/Registrar/identificacionSolicitante.aspx", "identificacionSolicitante.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Referencia Global", "~/Solicitudes/Registrar/general.aspx", "general.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Antecedentes del Sector", "~/Solicitudes/Registrar/antecedDelSector.aspx", "antecedDelSector.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Proyecto Técnico", "~/Solicitudes/Registrar/proyTecnico.aspx", "proyTecnico.aspx", nombre_archivo));
                    //ul.Controls.Add(Crea_Link("Informes y Resoluciones", "~/Solicitudes/Registrar/informesResoluciones.aspx", "informesResoluciones.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Creación de Concesión", "~/Solicitudes/Registrar/unidadEspacial.aspx", "unidadEspacial.aspx", nombre_archivo));
                    break;

                case "Informes y Resoluciones":
                    ul.Controls.Add(Crea_Link("IT U.O.T.", "~/Solicitudes/Registrar/pestanaInformeDeCartografia.aspx", "pestanaInformeDeCartografia.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Inspeccion en terreno", "~/Solicitudes/Registrar/pestanaInspeccionTerreno.aspx", "pestanaInspeccionTerreno.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Banco Natural", "~/Solicitudes/Registrar/pestanaBancoNatural.aspx", "pestanaBancoNatural.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Difusión Banco Natural", "~/Solicitudes/Registrar/pestanaDifusionBancoNatural.aspx", "pestanaDifusionBancoNatural.aspx", nombre_archivo));
                    if (solicitudConcesion.comunaFronteriza) //SOLO APLICA PARA LAS COMUNAS FRONTERIZAS
                    {
                        Difrol.Visible = true;
                        ul.Controls.Add(Crea_Link("DIFROL", "~/Solicitudes/Registrar/pestanaDifrol.aspx", "pestanaDifrol.aspx", nombre_archivo));
                    }
                    ul.Controls.Add(Crea_Link("Informe Ambiental", "~/Solicitudes/Registrar/pestanaInformeAmbiental.aspx", "pestanaInformeAmbiental.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Antecedentes Complementarios", "~/Solicitudes/Registrar/pestanaAntecedentesComplementarios.aspx", "pestanaAntecedentesComplementarios.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Planos", "~/Solicitudes/Registrar/pestanaPlanos.aspx", "pestanaPlanos.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Informe DAC", "~/Solicitudes/Registrar/pestanaInformeDAC.aspx", "pestanaInformeDAC.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Resolución SSP", "~/Solicitudes/Registrar/pestanaResolucionSSP.aspx", "pestanaResolucionSSP.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Resolución SSFFAA", "~/Solicitudes/Registrar/pestanaResolucionSSFFAA.aspx", "pestanaResolucionSSFFAA.aspx", nombre_archivo));
                    break;

                case "Administrador de Documentos":
                    ul.Controls.Add(Crea_Link("Administrador de Documentos", "~/Solicitudes/Registrar/ingresarDocumento.aspx", "ingresarDocumento.aspx", nombre_archivo));
                    break;

            };

            return ul;
        }

        protected HtmlGenericControl Crea_Link(string opcion, string urldestino, string pagina, string urlActual)
        {
            

            LinkButton link = new LinkButton();
            link.Text = opcion;
            link.PostBackUrl = urldestino;


            if (pagina != null && urlActual != null && pagina.Trim().ToUpper().Equals(urlActual.Trim().ToUpper()))
            {
                link.Style.Add("background-color", "#98A3FF;");
            }
            


            Panel div = new Panel();
            div.CssClass = "opc_menuv";
            div.Controls.Add(link);
            HtmlGenericControl li = new HtmlGenericControl("li");
            li.Controls.Add(div);

            return li;
        }

    }
}