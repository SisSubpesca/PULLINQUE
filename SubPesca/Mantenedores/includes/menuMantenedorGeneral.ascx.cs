using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace SubPesca.Mantenedores.includes
{
    public partial class menuMantenedorGeneral : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Mantenedor General"));
           
        }

        protected HtmlGenericControl Crea_OpcionesModulo(string modulo)
        {
            Label texto = new Label();
            texto.Text = modulo;

            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.Controls.Add(texto);

            switch (modulo)
            {
                case "Mantenedor General":
                    ul.Controls.Add(Crea_Link("Región", "~/Mantenedores/Generales/region.aspx"));
                    ul.Controls.Add(Crea_Link("Provincia", "~/Mantenedores/Generales/provincia.aspx"));
                    ul.Controls.Add(Crea_Link("Comuna", "~/Mantenedores/Generales/comuna.aspx"));
                    ul.Controls.Add(Crea_Link("Tipo Barrio", "~/Mantenedores/Generales/tipoBarrio.aspx?bp=1"));
                    ul.Controls.Add(Crea_Link("Barrio", "~/Mantenedores/Generales/barrio.aspx"));
                    ul.Controls.Add(Crea_Link("Asociación Barrio - Tipo", "~/Mantenedores/Generales/barrioTipo.aspx"));
                    ul.Controls.Add(Crea_Link("Macrozona", "~/Mantenedores/Generales/macrozona.aspx"));
                    ul.Controls.Add(Crea_Link("Carta SHOA /IGM Plano", "~/Mantenedores/Generales/carta.aspx"));
                    ul.Controls.Add(Crea_Link("Datum", "~/Mantenedores/Generales/datum.aspx"));
                    ul.Controls.Add(Crea_Link("Tipo Vértice", "~/Mantenedores/Generales/tipoVertice.aspx?bp=1"));
                    ul.Controls.Add(Crea_Link("Tipo Contacto", "~/Mantenedores/Generales/tipoContacto.aspx?bp=1"));
                    ul.Controls.Add(Crea_Link("Tipo Huso", "~/Mantenedores/Generales/huso.aspx"));
                    ul.Controls.Add(Crea_Link("Tipo Concesión", "~/Mantenedores/Generales/tipoConcesion.aspx"));
                    ul.Controls.Add(Crea_Link("Tipo Uso", "~/Mantenedores/Generales/tipoUso.aspx?bp=1"));
                    ul.Controls.Add(Crea_Link("Tipo Cultivo", "~/Mantenedores/Generales/tipoCultivo.aspx?bp=1"));
                    ul.Controls.Add(Crea_Link("Método Cultivo Algas", "~/Mantenedores/Generales/metodoCultivo.aspx?bp=1"));
                    ul.Controls.Add(Crea_Link("Tipo Alimento", "~/Mantenedores/Generales/tipoAlimento.aspx?bp=1"));
                    //ul.Controls.Add(Crea_Link("Tipo Fondo", "~/Mantenedores/Generales/tipoFondo.aspx?bp=1"));
                    ul.Controls.Add(Crea_Link("Especie de Cultivo", "~/Mantenedores/Generales/especie.aspx"));
                    //ul.Controls.Add(Crea_Link("Grupo de Especie", "~/Mantenedores/Generales/grupoEspecie.aspx"));
                    ul.Controls.Add(Crea_Link("Etapa de Cultivo", "~/Mantenedores/Generales/etapaDeDesarrollo.aspx"));
                    ul.Controls.Add(Crea_Link("Tipo Estructura", "~/Mantenedores/Generales/estructuraTecnica.aspx"));
                    ul.Controls.Add(Crea_Link("Forma Estructura", "~/Mantenedores/Generales/formaEstructura.aspx"));
                    ul.Controls.Add(Crea_Link("Unidad Medida Estructura", "~/Mantenedores/Generales/unidadMedidaEstructuraTecnica.aspx"));
                    ul.Controls.Add(Crea_Link("Volumen Unidad Medida Estructura", "~/Mantenedores/Generales/volumenUnidadMedida.aspx?bp=1"));
                    ul.Controls.Add(Crea_Link("Años", "~/Mantenedores/Generales/anio.aspx?bp=1"));
                    ul.Controls.Add(Crea_Link("Unidad Ejemplar", "~/Mantenedores/Generales/unidadEjemplar.aspx?bp=1"));
                    ul.Controls.Add(Crea_Link("Rango Peso Ejemplar", "~/Mantenedores/Generales/rangoPesoEjemplar.aspx?bp=1"));
                    ul.Controls.Add(Crea_Link("Capitanía de Puerto", "~/Mantenedores/Generales/capitaniaPuerto.aspx"));
                    ul.Controls.Add(Crea_Link("Tipo Archivo Titulares", "~/Mantenedores/Generales/tipoArchivoTitular.aspx?bp=1"));
                    ul.Controls.Add(Crea_Link("Tipo Archivo Coord. Originales", "~/Mantenedores/Generales/tipoArchivoAntEspaciales.aspx"));
                    ul.Controls.Add(Crea_Link("Tipo Archivo Coord. 14 TER", "~/Mantenedores/Generales/tipoArchivoAntTerreno.aspx"));
                    ul.Controls.Add(Crea_Link("Tipo Archivo Coord. Regularización", "~/Mantenedores/Generales/tipoArchivoRegularizacion.aspx"));
                    ul.Controls.Add(Crea_Link("Tipo Centro Acopio", "~/Mantenedores/Generales/tipoCentroAcopio.aspx"));
                    ul.Controls.Add(Crea_Link("Tipo Centro Faenamiento", "~/Mantenedores/Generales/tipoCentroFaenamiento.aspx"));

                    /* Pullinque 4.0*/
                    ul.Controls.Add(Crea_Link("Tipo Cuerpo Agua", "~/Mantenedores/Generales/tipoCuerpoAgua.aspx"));
                    ul.Controls.Add(Crea_Link("Cuerpo Agua", "~/Mantenedores/Generales/cuerpoDeAgua.aspx"));
                    ul.Controls.Add(Crea_Link("Plazo Nominal", "~/Mantenedores/Generales/plazoNominal.aspx"));
                    ul.Controls.Add(Crea_Link("Tipo Organización", "~/Mantenedores/Generales/tipoOrganizacion.aspx"));
                    ul.Controls.Add(Crea_Link("Holding", "~/Mantenedores/Generales/holding.aspx"));
                    ul.Controls.Add(Crea_Link("Descansos ACS", "~/Mantenedores/Generales/descasoACS.aspx"));
                    ul.Controls.Add(Crea_Link("Especie - Etapa de Cultivo", "~/Mantenedores/Generales/especieEtapaDesarrollo.aspx"));

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