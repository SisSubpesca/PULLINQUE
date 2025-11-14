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
    public partial class menuSolicitudesExperimentalesAmerb : System.Web.UI.UserControl
    {
        DespliegueSeccionService despliegueSeccionService = new DespliegueSeccionService();
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable();

        protected void Page_Load(object sender, EventArgs e)
        {

            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Menú Solicitud Experimentales Amerb"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Informes y Resoluciones"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Administrador de Documentos"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Portal"));

            //Informe y resoluciones -Visibilidad del submenu-
            SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[paginas.solicitudExperimentalesAmerbSession];
            //URB
            URB.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_URB, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);

            //Suficiencia Formal
            SuficienciaFormal.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.EXAMEN_PRELIMINAR, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);

            //It OUT (informe cartografia)
            if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ITC_UOT, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB) == false &&
                despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.UNIDADES_DE_DEPENDENCIA, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB) == false)
            {
                ITUOT.Visible = false;
            }
            //inspeccion Terreno
            if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INSPECCION_TERRENO, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB) == false &&
                despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.COORDENADAS_GEOGRAFICAS_INSPECCION_TERRENO, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB) == false)
            {
                Inspeccionterreno.Visible = false;
            }
            //banco natural
            Banconatural.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.BANCO_NATURAL, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);

            //Difusion banco natural
            Difusionbn.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DIFUSION_BANCO_NATURAL, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);

            //Difrol
            if (solicitudConcesion.comunaFronteriza) //SOLO APLICA PARA LAS COMUNAS FRONTERIZAS
            {
                Difrol.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DIFROL, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
            }
            //Informe Ambiental
            Informeambiental.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.APLICA_SEA, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);

            //Antecedentes Complementarios
            if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_COMPLEMENTARIOS, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB) == false &&
                despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.CERTIFICADO_REGISTRO_OPERACION, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB) == false)
            {
                Antecedentescomplementarios.Visible = false;
            }

            //Planos
            if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_PLANOS, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB) == false &&
                despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INFORME_TECNICO_UOT, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB) == false)
            {
                Planos.Visible = false;
            }

            //Informedac
            if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INFORME_DAC, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB) == false &&
                despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DEVOLUCION_JURIDICA, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB) == false)
            {
                Informedac.Visible = false;
            }
            //Resolucionssp
            if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.RESOLUCION_SSP, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB) == false &&
                despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DEVOLUCION_SSFFAA, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB) == false)
            {
                Resolucionssp.Visible = false;
            }
            //Resolucionssffaa
            Resolucionssffaa.Visible = despliegueSeccionService.ObtieneDespliegueSeccion2(rbSeccion.RESOLUCION_SSFFAA, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB, this.usuario_logeado.id_usuario, rbSeccionUnidadEspacial.RESOLUCION_SSFFAA_EXPERIMENTALES_AMERB);

        }

        protected HtmlGenericControl Crea_OpcionesModulo(string modulo)
        {
            Label texto = new Label();
            texto.Text = modulo;

            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.Controls.Add(texto);

            SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[paginas.solicitudExperimentalesAmerbSession];
            if (solicitudConcesion == null)
            {
                Response.Redirect("~/Solicitudes/ExperimentalesAmerb/administrarSolicitudExperimentalesAmerb.aspx");
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
                case "Menú Solicitud Experimentales Amerb":
                    ul.Controls.Add(Crea_Link("Solicitud Experimentales Amerb", "~/Solicitudes/ExperimentalesAmerb/datosCentroExperimentalesAmerb.aspx","datosCentroExperimentalAmerb.aspx",nombre_archivo));
                    ul.Controls.Add(Crea_Link("Identificación del Solicitante", "~/Solicitudes/ExperimentalesAmerb/identificacionTitularExperimentalesAmerb.aspx", "identificacionTitularExperimentalesAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Referencia Global", "~/Solicitudes/ExperimentalesAmerb/generalExperimentalesAmerb.aspx", "generalExperimentalesAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Antecedentes del Sector", "~/Solicitudes/ExperimentalesAmerb/antecedDelSectorExperimentalesAmerb.aspx", "antecedDelSectorExperimentalesAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Proyecto Técnico", "~/Solicitudes/ExperimentalesAmerb/proyTecnicoExperimentalesAmerb.aspx", "proyTecnicoExperimentalesAmerb.aspx", nombre_archivo));
                    //ul.Controls.Add(Crea_Link("Informes y Resoluciones", "~/Solicitudes/Registrar/informesResoluciones.aspx", "informesResoluciones.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Creación de Experimentales AMERB", "~/Solicitudes/ExperimentalesAmerb/unidadEspacialExperimentalesAmerb.aspx","unidadEspacialExperimentalAmerb.aspx ",nombre_archivo));
                    break;

                case "Informes y Resoluciones":
                    ul.Controls.Add(Crea_Link("Evluación URB", "~/Solicitudes/ExperimentalesAmerb/pestanaEvaluacionURBExperimentalesAmerb.aspx", "pestanaEvaluacionURBExperimentalesAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Suficiencia Formal", "~/Solicitudes/ExperimentalesAmerb/pestanaSuficienciaFormalExperimentalesAmerb.aspx", "pestanaSuficienciaFormalExperimentalesAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("IT U.O.T.", "~/Solicitudes/ExperimentalesAmerb/pestanaInformeDeCartografiaExperimentalesAmerb.aspx", "pestanaInformeDeCartografiaExperimentalesAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Inspeccion en terreno", "~/Solicitudes/ExperimentalesAmerb/pestanaInspeccionTerrenoExperimentalesAmerb.aspx", "pestanaInspeccionTerrenoExperimentalesAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Banco Natural", "~/Solicitudes/ExperimentalesAmerb/pestanaBancoNaturalExperimentalesAmerb.aspx", "pestanaBancoNaturalExperimentalesAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Difusión Banco Natural", "~/Solicitudes/ExperimentalesAmerb/pestanaDifusionBancoNaturalExperimentalesAmerb.aspx", "pestanaDifusionBancoNaturalExperimentalesAmerb.aspx", nombre_archivo));
                    if (solicitudConcesion.comunaFronteriza) //SOLO APLICA PARA LAS COMUNAS FRONTERIZAS
                    {
                        Difrol.Visible = true;
                        ul.Controls.Add(Crea_Link("DIFROL", "~/Solicitudes/ExperimentalesAmerb/pestanaDifrolExperimentalesAmerb.aspx", "pestanaDifrolExperimentalesAmerb.aspx", nombre_archivo));
                    }
                    ul.Controls.Add(Crea_Link("Informe Ambiental", "~/Solicitudes/ExperimentalesAmerb/pestanaInformeAmbientalExperimentalesAmerb.aspx", "pestanaInformeAmbientalExperimentalesAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Antecedentes Complementarios", "~/Solicitudes/ExperimentalesAmerb/pestanaAntecedentesComplementariosExperimentalesAmerb.aspx", "pestanaAntecedentesComplementariosExperimentalesAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Planos", "~/Solicitudes/ExperimentalesAmerb/pestanaPlanosExperimentalesAmerb.aspx", "pestanaPlanosExperimentalesAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Informe DAC", "~/Solicitudes/ExperimentalesAmerb/pestanaInformeDACExperimentalesAmerb.aspx", "pestanaInformeDACExperimentalesAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Resolución SSP", "~/Solicitudes/ExperimentalesAmerb/pestanaResolucionSSPExperimentalesAmerb.aspx", "pestanaResolucionSSPExperimentalesAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Resolución SSFFAA", "~/Solicitudes/ExperimentalesAmerb/pestanaResolucionSSFFAAExperimentalesAmerb.aspx", "pestanaResolucionSSFFAAExperimentalesAmerb.aspx", nombre_archivo));
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