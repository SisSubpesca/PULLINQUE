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
    public partial class menuSolicitudesECMPO : System.Web.UI.UserControl
    {
        DespliegueSeccionService despliegueSeccionService = new DespliegueSeccionService();
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable();

        protected void Page_Load(object sender, EventArgs e)
        {

            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Menú Solicitud Acuicultura en ECMPO"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Informes y Resoluciones"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Administrador de Documentos"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Portal"));

            SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[paginas.solicitudECMPOSession];

            //It OUT (informe cartografia)
            if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ITC_UOT, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO) == false &&
                despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.UNIDADES_DE_DEPENDENCIA, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO) == false)
            {
                ITUOT.Visible = false;
            }
            //inspeccion Terreno
            if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INSPECCION_TERRENO, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO) == false &&
                despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.COORDENADAS_GEOGRAFICAS_INSPECCION_TERRENO, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO) == false)
            {
                Inspeccionterreno.Visible = false;
            }
            //banco natural
            Banconatural.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.BANCO_NATURAL, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO);

            //Difusion banco natural
            Difusionbn.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DIFUSION_BANCO_NATURAL, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO);

            //Difrol
            if (solicitudConcesion.comunaFronteriza) //SOLO APLICA PARA LAS COMUNAS FRONTERIZAS
            {
                Difrol.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DIFROL, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO);
            }
            //Informe Ambiental
            Informeambiental.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.APLICA_SEA, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO);

            //Antecedentes Complementarios
            if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_COMPLEMENTARIOS, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO) == false &&
                despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.CERTIFICADO_REGISTRO_OPERACION, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO) == false)
            {
                Antecedentescomplementarios.Visible = false;
            }

            //Planos
            if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_PLANOS, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO) == false &&
                despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INFORME_TECNICO_UOT, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO) == false)
            {
                Planos.Visible = false;
            }

            //Informedac
            if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INFORME_DAC, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO) == false &&
                despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DEVOLUCION_JURIDICA, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO) == false)
            {
                Informedac.Visible = false;
            }
            //Resolucionssp
            if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.RESOLUCION_SSP, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO) == false &&
                despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DEVOLUCION_SSFFAA, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO) == false)
            {
                Resolucionssp.Visible = false;
            }
            //Resolucionssffaa
            Resolucionssffaa.Visible = despliegueSeccionService.ObtieneDespliegueSeccion2(rbSeccion.RESOLUCION_SSFFAA, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO, this.usuario_logeado.id_usuario, rbSeccionUnidadEspacial.RESOLUCION_SSFFAA_ECMPO);

        }

        protected HtmlGenericControl Crea_OpcionesModulo(string modulo)
        {
            Label texto = new Label();
            texto.Text = modulo;

            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.Controls.Add(texto);

            SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[paginas.solicitudECMPOSession];
            if (solicitudConcesion == null)
            {
                Response.Redirect("~/Solicitudes/ECMPO/administrarSolicitudECMPO.aspx");
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
                case "Menú Solicitud Acuicultura en ECMPO":
                    ul.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", "~/Solicitudes/ECMPO/datosCentroECMPO.aspx","datosCentroECMPO.aspx",nombre_archivo));
                    ul.Controls.Add(Crea_Link("Identificación del Solicitante", "~/Solicitudes/ECMPO/identificacionSolicitanteECMPO.aspx", "identificacionSolicitanteECMPO.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Referencia Global", "~/Solicitudes/ECMPO/generalECMPO.aspx", "generalECMPO.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Antecedentes del Sector", "~/Solicitudes/ECMPO/antecedDelSectorECMPO.aspx", "antecedDelSectorECMPO.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Proyecto Técnico", "~/Solicitudes/ECMPO/proyTecnicoECMPO.aspx", "proyTecnicoECMPO.aspx", nombre_archivo));
                    //ul.Controls.Add(Crea_Link("Informes y Resoluciones", "~/Solicitudes/Registrar/informesResoluciones.aspx", "informesResoluciones.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Creación de Acuicultura en ECMPO", "~/Solicitudes/ECMPO/unidadEspacialECMPO.aspx", "unidadEspacialECMPO.aspx", nombre_archivo));
                    break;

                case "Informes y Resoluciones":
                    ul.Controls.Add(Crea_Link("IT U.O.T.", "~/Solicitudes/ECMPO/pestanaInformeDeCartografiaECMPO.aspx", "pestanaInformeDeCartografiaECMPO.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Inspeccion en terreno", "~/Solicitudes/ECMPO/pestanaInspeccionTerrenoECMPO.aspx", "pestanaInspeccionTerrenoECMPO.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Banco Natural", "~/Solicitudes/ECMPO/pestanaBancoNaturalECMPO.aspx", "pestanaBancoNaturalECMPO.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Difusión Banco Natural", "~/Solicitudes/ECMPO/pestanaDifusionBancoNaturalECMPO.aspx", "pestanaDifusionBancoNaturalECMPO.aspx", nombre_archivo));
                    if (solicitudConcesion.comunaFronteriza) //SOLO APLICA PARA LAS COMUNAS FRONTERIZAS
                    {
                        Difrol.Visible = true;
                        ul.Controls.Add(Crea_Link("DIFROL", "~/Solicitudes/ECMPO/pestanaDifrolECMPO.aspx", "pestanaDifrolECMPO.aspx", nombre_archivo));
                    }
                    ul.Controls.Add(Crea_Link("Informe Ambiental", "~/Solicitudes/ECMPO/pestanaInformeAmbientalECMPO.aspx", "pestanaInformeAmbientalECMPO.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Antecedentes Complementarios", "~/Solicitudes/ECMPO/pestanaAntecedentesComplementariosECMPO.aspx", "pestanaAntecedentesComplementariosECMPO.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Planos", "~/Solicitudes/ECMPO/pestanaPlanosECMPO.aspx", "pestanaPlanosECMPO.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Informe DAC", "~/Solicitudes/ECMPO/pestanaInformeDACECMPO.aspx", "pestanaInformeDACECMPO.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Resolución SSP", "~/Solicitudes/ECMPO/pestanaResolucionSSPECMPO.aspx", "pestanaResolucionSSPECMPO.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Resolución SSFFAA", "~/Solicitudes/ECMPO/pestanaResolucionSSFFAAECMPO.aspx", "pestanaResolucionSSFFAAECMPO.aspx", nombre_archivo));
                    break;

                case "Administrador de Documentos":
                    ul.Controls.Add(Crea_Link("Administrador de Documentos", "~/Solicitudes/ECMPO/ingresarDocumentoECMPO.aspx", "ingresarDocumento.aspx", nombre_archivo));
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