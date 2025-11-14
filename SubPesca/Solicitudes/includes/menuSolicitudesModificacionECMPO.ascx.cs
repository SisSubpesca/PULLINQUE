using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.modificacion;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.solicitud;


namespace SubPesca.Solicitudes.includes
{
    public partial class menuSolicitudesModificacionECMPO : System.Web.UI.UserControl
    {
        SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();
        DespliegueMenuSeccionDA despliegueMenuSeccionDA = new DespliegueMenuSeccionDA();

        //protected void setearModulo()
        //{
        //    ViewState["solicitudSession"] = paginas.solicitudModificacionECMPOSession;
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Solicitud de Modificación de ECMPO"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Informes y Resoluciones"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Administrador de Documentos"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Portal"));

            SolicitudConcesion solicitudConcesion = (SolicitudConcesion)Session[paginas.solicitudModificacionECMPOSession];

            /*IT U.O.T */
            List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.ITC_OUT_MODIFICACION, 0);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {

                    ITUOT.Visible = (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.ITC_UOT)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.UNIDADES_DE_DEPENDENCIA)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_ITC_UOT));
            }

            /* Inspección en Terreno */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.INSPECCION_EN_TERRENO_MODIFICACION, 0);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {

                    Inspeccionterreno.Visible = (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.INSPECCION_TERRENO)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.COORDENADAS_GEOGRAFICAS_INSPECCION_TERRENO)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_INSPECCION_TERRENO));   
            }

            /* Banco Natural */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.BANCO_NATURAL_MODIFICACION, 0);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {

                    Banconatural.Visible = (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.BANCO_NATURAL)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_BCO_NATURAL));
                
            }

            /* Difusión de Banco Natural */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.DIFUSION_BANCO_NATURAL_MODIFICACION, 0);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {

                Difusionbn.Visible = (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.DIFUSION_BANCO_NATURAL)
                || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_DIFUSION_BANCO_NATURAL));
                
            }

            /* DIFROL */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.DIFROL_MODIFICACION, 0);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                    Difrol.Visible = ((solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.DIFROL)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_DIFROL))
                    && solicitudModificacionService.esComunaFronteriza(solicitudConcesion.idSolConcesion));
            }

            /* Informe Ambiental */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.INFORME_AMBIENTAL_MODIFICACION, 0);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                    Informeambiental.Visible = (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.APLICA_SEA)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_INFORME_AMBIENTAL));
            }

            /* Antecedentes Complementarios */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.ANTECEDENTES_COMPLEMENTARIOS_MODIFICACION, 0);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                    Antecedentescomplementarios.Visible = (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.ANTECEDENTES_COMPLEMENTARIOS)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.CERTIFICADO_REGISTRO_OPERACION)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_ANTECEDENTES_COMPLEMENTARIOS));
            }

            /* Planos */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.PLANOS_MODIFICACION, 0);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                    Planos.Visible = (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.ANTECEDENTES_PLANOS)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.INFORME_TECNICO_UOT)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_PLANOS));
            }

            /* Informe DAC */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.INFORME_DAC_MODIFICACION, 0);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                    Informedac.Visible = (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.INFORME_DAC)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.DEVOLUCION_JURIDICA)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_INFORME_DAC));
            }

            /* Resolución SSP */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.RESOLUCION_SSP_MODIFICACION, 0);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                    Resolucionssp.Visible = (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.RESOLUCION_SSP)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.DEVOLUCION_SSFFAA)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_RESOLUCION_SSP));
            }

            /* Resolución SSFFAA */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.RESOLUCION_SSFFAA_MODIFICACION, 0);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {

                    Resolucionssffaa.Visible = (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.RESOLUCION_SSFFAA)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_RESOLUCION_SSFFAA));
            }
        }

        protected HtmlGenericControl Crea_OpcionesModulo(string modulo)
        {
            Label texto = new Label();
            texto.Text = modulo;

            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.Controls.Add(texto);

            SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[paginas.solicitudModificacionECMPOSession];

            if (solicitudConcesion == null)
            {
                Response.Redirect("~/Solicitudes/ModificacionECMPO/administrarSolicitudModificacionECMPO.aspx");
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
                case "Solicitud de Modificación de ECMPO":


                    ul.Controls.Add(Crea_Link("Datos del Trámite de Modificación", "~/Solicitudes/ModificacionECMPO/datosModificacionECMPO.aspx","datosModificacionECMPO.aspx",nombre_archivo));
                    ul.Controls.Add(Crea_Link("Identificación del Titular", "~/Solicitudes/ModificacionECMPO/identificacionTitularModificacionECMPO.aspx","identificacionTitularModificacionECMPO.aspx",nombre_archivo));
                    ul.Controls.Add(Crea_Link("Referencia Global", "~/Solicitudes/ModificacionECMPO/generalModificacionECMPO.aspx", "generalModificacionECMPO.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Antecedentes del Sector", "~/Solicitudes/ModificacionECMPO/antecedDelSectorModificacionECMPO.aspx","antecedDelSectorModificacionECMPO.aspx",nombre_archivo));
                    ul.Controls.Add(Crea_Link("Proyecto Técnico", "~/Solicitudes/ModificacionECMPO/proyTecnicoModificacionECMPO.aspx","proyTecnicoModificacionECMPO.aspx",nombre_archivo));
                    //ul.Controls.Add(Crea_Link("Informes y Resoluciones", "~/Solicitudes/ModificacionECMPO/informesResolucionesModificacionECMPO.aspx","informesResolucionesModificacionECMPO.aspx",nombre_archivo));
                    ul.Controls.Add(Crea_Link("Modificación de ECMPO", "~/Solicitudes/ModificacionECMPO/unidadesEspacialesModificacionECMPO.aspx","unidadesEspacialesModificacionECMPO.aspx",nombre_archivo));

                    break;

                case "Informes y Resoluciones":
                    ul.Controls.Add(Crea_Link("IT U.O.T.", "~/Solicitudes/ModificacionECMPO/pestanaInformeDeCartografiaModificacionECMPO.aspx", "pestanaInformeDeCartografiaModificacionECMPO.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Inspeccion en terreno", "~/Solicitudes/ModificacionECMPO/pestanaInspeccionTerrenoModificacionECMPO.aspx", "pestanaInspeccionTerrenoModificacionECMPO.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Banco Natural", "~/Solicitudes/ModificacionECMPO/pestanaBancoNaturalModificacionECMPO.aspx", "pestanaBancoNaturalModificacionECMPO.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Difusión Banco Natural", "~/Solicitudes/ModificacionECMPO/pestanaDifusionBancoNaturalModificacionECMPO.aspx", "pestanaDifusionBancoNaturalModificacionECMPO.aspx", nombre_archivo));
                    if (solicitudConcesion.comunaFronteriza) //SOLO APLICA PARA LAS COMUNAS FRONTERIZAS
                    {
                        Difrol.Visible = true;
                        ul.Controls.Add(Crea_Link("DIFROL", "~/Solicitudes/ModificacionECMPO/pestanaDifrolModificacionECMPO.aspx", "pestanaDifrolModificacionECMPO.aspx", nombre_archivo));
                    }
                    ul.Controls.Add(Crea_Link("Informe Ambiental", "~/Solicitudes/ModificacionECMPO/pestanaInformeAmbientalModificacionECMPO.aspx", "pestanaInformeAmbientalModificacionECMPO.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Antecedentes Complementarios", "~/Solicitudes/ModificacionECMPO/pestanaAntecedentesComplementariosModificacionECMPO.aspx", "pestanaAntecedentesComplementariosModificacionECMPO.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Planos", "~/Solicitudes/ModificacionECMPO/pestanaPlanosModificacionECMPO.aspx", "pestanaPlanosModificacionECMPO.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Informe DAC", "~/Solicitudes/ModificacionECMPO/pestanaInformeDACModificacionECMPO.aspx", "pestanaInformeDACModificacionECMPO.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Resolución SSP", "~/Solicitudes/ModificacionECMPO/pestanaResolucionSSPModificacionECMPO.aspx", "pestanaResolucionSSPModificacionECMPO.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Resolución SSFFAA", "~/Solicitudes/ModificacionCentroFaenamiento/pestanaResolucionSSFFAAModificacionCentroFaenamiento.aspx", "pestanaResolucionSSFFAAModificacionCentroFaenamiento.aspx", nombre_archivo));
                    break;

                case "Administrador de Documentos":

                    ul.Controls.Add(Crea_Link("Administrador de Documentos", "~/Solicitudes/ModificacionECMPO/ingresarDocumentoModificacionECMPO.aspx","ingresarDocumentoModificacionECMPO.aspx",nombre_archivo));

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