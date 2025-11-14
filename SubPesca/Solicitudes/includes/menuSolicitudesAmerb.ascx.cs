using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Datos.Entidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using Datos.Utilidades;


namespace SubPesca.Solicitudes.includes
{
    public partial class menuSolicitudesAmerb : System.Web.UI.UserControl
    {
        DespliegueSeccionService despliegueSeccionService = new DespliegueSeccionService();
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable();

        protected void Page_Load(object sender, EventArgs e)
        {

            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Menú Solicitud Acuicultura Amerb"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Informes y Resoluciones"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Administrador de Documentos"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Portal"));

            SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[paginas.solicitudAmerbSession];

            //URB
            URB.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_URB, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);

            //Suficicencia Formal
            Formal.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.EXAMEN_PRELIMINAR, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);

            //It OUT (informe cartografia)
            if((despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ITC_UOT, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB) == false &&
                despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.UNIDADES_DE_DEPENDENCIA, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB) == false))
            {
                ITUOT.Visible = false;
            }
            //inspeccion Terreno
            if((despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INSPECCION_TERRENO, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB) == false &&
                despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.COORDENADAS_GEOGRAFICAS_INSPECCION_TERRENO, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB) == false))
            {
                Inspeccionterreno.Visible = false;
            }
                //banco natural
            Banconatural.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.BANCO_NATURAL, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);

            //Difusion banco natural
            Difusionbn.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DIFUSION_BANCO_NATURAL, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);

            //Difrol
            if (solicitudConcesion.comunaFronteriza)
            {
                Difrol.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DIFROL, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);
            }
            //Informe Ambiental
            Informeambiental.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.APLICA_SEA, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);

            //Antecedentes Complementarios
            if ((despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_COMPLEMENTARIOS, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB) == false &&
                despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.CERTIFICADO_REGISTRO_OPERACION, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB) == false))
            {
                Antecedentescomplementarios.Visible = false;
            }
                //Planos
            if ((despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_PLANOS, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB) == false &&
                despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INFORME_TECNICO_UOT, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB) == false))
            {
                Planos.Visible = false;
            }
            //Informedac
            if((despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INFORME_DAC, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB) == false &&
                despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DEVOLUCION_JURIDICA, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB) == false))
            {
                Informedac.Visible = false;
            }
            //Resolucionssp
            if((despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.RESOLUCION_SSP, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB) == false &&
                despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DEVOLUCION_SSFFAA, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB) == false))
            {
                Resolucionssp.Visible = false;
            }
            //Resolucionssffaa
            Resolucionssffaa.Visible = despliegueSeccionService.ObtieneDespliegueSeccion2(rbSeccion.RESOLUCION_SSFFAA, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB, this.usuario_logeado.id_usuario, rbSeccionUnidadEspacial.RESOLUCION_SSFFAA_AMERB);


        }

        protected HtmlGenericControl Crea_OpcionesModulo(string modulo)
        {
            Label texto = new Label();
            texto.Text = modulo;

            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.Controls.Add(texto);

            SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[paginas.solicitudAmerbSession];
            if (solicitudConcesion == null)
            {
                Response.Redirect("~/Solicitudes/Amerb/administrarSolicitudAmerb.aspx");
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
                case "Menú Solicitud Acuicultura Amerb":
                    ul.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", "~/Solicitudes/Amerb/datosCentroAmerb.aspx","datosCentroAmerb.aspx",nombre_archivo));
                    ul.Controls.Add(Crea_Link("Identificación del Solicitante", "~/Solicitudes/Amerb/identificacionSolicitanteAmerb.aspx", "identificacionSolicitanteAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Referencia Global", "~/Solicitudes/Amerb/generalAmerb.aspx", "generalAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Antecedentes del Sector", "~/Solicitudes/Amerb/antecedDelSectorAmerb.aspx", "antecedDelSectorAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Proyecto Técnico", "~/Solicitudes/Amerb/proyTecnicoAmerb.aspx", "proyTecnicoAmerb.aspx", nombre_archivo));
                    //ul.Controls.Add(Crea_Link("Informes y Resoluciones", "~/Solicitudes/Registrar/informesResoluciones.aspx", "informesResoluciones.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Creación de Acuicultura en AMERB", "~/Solicitudes/Amerb/unidadEspacialAmerb.aspx", "unidadEspacialAmerb.aspx", nombre_archivo));
                    break;

                case "Informes y Resoluciones":
                    ul.Controls.Add(Crea_Link("Evaluación URB", "~/Solicitudes/Amerb/pestanaEvaluacionURBamerb.aspx", "pestanaEvaluacionURBamerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Suficiencia Formal", "~/Solicitudes/Amerb/pestanaSuficienciaFormalamerb.aspx", "pestanaSuficienciaFormalamerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("IT U.O.T.", "~/Solicitudes/Amerb/pestanaInformeDeCartografiaAmerb.aspx", "pestanaInformeDeCartografiaAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Inspeccion en terreno", "~/Solicitudes/Amerb/pestanaInspeccionTerrenoAmerb.aspx", "pestanaInspeccionTerrenoAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Banco Natural", "~/Solicitudes/Amerb/pestanaBancoNaturalAmerb.aspx", "pestanaBancoNaturalAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Difusión Banco Natural", "~/Solicitudes/Amerb/pestanaDifusionBancoNaturalAmerb.aspx", "pestanaDifusionBancoNaturalAmerb.aspx", nombre_archivo));
                    if (solicitudConcesion.comunaFronteriza) //SOLO APLICA PARA LAS COMUNAS FRONTERIZAS
                    {
                        Difrol.Visible = true;
                        ul.Controls.Add(Crea_Link("DIFROL", "~/Solicitudes/Amerb/pestanaDifrolAmerb.aspx", "pestanaDifrolAmerb.aspx", nombre_archivo));
                    }
                    ul.Controls.Add(Crea_Link("Informe Ambiental", "~/Solicitudes/Amerb/pestanaInformeAmbientalAmerb.aspx", "pestanaInformeAmbientalAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Antecedentes Complementarios", "~/Solicitudes/Amerb/pestanaAntecedentesComplementariosAmerb.aspx", "pestanaAntecedentesComplementariosAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Planos", "~/Solicitudes/Amerb/pestanaPlanosAmerb.aspx", "pestanaPlanosAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Informe DAC", "~/Solicitudes/Amerb/pestanaInformeDACAmerb.aspx", "pestanaInformeDACAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Resolución SSP", "~/Solicitudes/Amerb/pestanaResolucionSSPAmerb.aspx", "pestanaResolucionSSPAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Resolución SSFFAA", "~/Solicitudes/Amerb/pestanaResolucionSSFFAAAmerb.aspx", "pestanaResolucionSSFFAAAmerb.aspx", nombre_archivo));
                    break;

                case "Administrador de Documentos":
                    ul.Controls.Add(Crea_Link("Administrador de Documentos", "~/Solicitudes/Amerb/ingresarDocumentoAmerb.aspx", "ingresarDocumentoAmerb.aspx", nombre_archivo));
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
        /*
        protected HtmlGenericControl Crea_Link(string opcion, string urldestino)
        {
           //Image img = new Image();
           //img.CssClass = "child";
           //img.ImageUrl = "~/App_Themes/admin_style/images/child.gif";

            LinkButton link = new LinkButton();
            link.Text = opcion;
            link.PostBackUrl = urldestino;

            /*
            link.Command += new CommandEventHandler(WebSiteLink_Click);
            if (opcion == "WebSite Público")
            {
                link.OnClientClick = "aspnetForm.target = '_blank';";
            };
            
            Panel div = new Panel();
            div.CssClass = "opc_menuv";
            //div.Controls.Add(img);
            div.Controls.Add(link);

            HtmlGenericControl li = new HtmlGenericControl("li");
            li.Controls.Add(div);

            return li;
        }
*/

    }
}