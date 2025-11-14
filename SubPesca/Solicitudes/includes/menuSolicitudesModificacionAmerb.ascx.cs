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
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using Datos.Utilidades;


namespace SubPesca.Solicitudes.includes
{
    public partial class menuSolicitudesModificacionAmerb : System.Web.UI.UserControl
    {
        SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();
        DespliegueMenuSeccionDA despliegueMenuSeccionDA = new DespliegueMenuSeccionDA();

        //protected void setearModulo()
        //{
        //    ViewState["solicitudSession"] = paginas.solicitudModificacionAmerbSession;
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Solicitud de Modificación de Amerb"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Informes y Resoluciones"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Administrador de Documentos"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Portal"));

            SolicitudConcesion solicitudConcesion = (SolicitudConcesion)Session[paginas.solicitudModificacionAmerbSession];


            List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.EVALUACION_URB, 0, rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB);
            /* EVALUACION URB */
            if (despliegueMenuSeccionList != null
                && despliegueMenuSeccionList.Count > 0)
            {
                    EvaluacionURB.Visible = (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.ANTECEDENTES_URB));
            }


            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.SUFICIENCIA_FORMAL, 0, rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB);
            /* SUFICIENCIA FORMAL */
            if (despliegueMenuSeccionList != null
                && despliegueMenuSeccionList.Count > 0)
            {
                    SuficienciaFormal.Visible = (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.EXAMEN_PRELIMINAR));
            }

            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.ITC_OUT_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB);
            /*IT UOT*/
            if (despliegueMenuSeccionList != null
                && despliegueMenuSeccionList.Count > 0)
            {
                    ITUOT.Visible = (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.ITC_UOT)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.UNIDADES_DE_DEPENDENCIA)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_ITC_UOT));
            }

            /* Inspección en Terreno */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.INSPECCION_EN_TERRENO_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                    Inspeccionterreno.Visible = (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.INSPECCION_TERRENO)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.COORDENADAS_GEOGRAFICAS_INSPECCION_TERRENO)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_INSPECCION_TERRENO));
            }

            /* Banco Natural */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.BANCO_NATURAL_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                    Banconatural.Visible = (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.BANCO_NATURAL)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_BCO_NATURAL));
            }

            /* Difusión de Banco Natural */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.DIFUSION_BANCO_NATURAL_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                    Difusionbn.Visible = (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.DIFUSION_BANCO_NATURAL)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_DIFUSION_BANCO_NATURAL));
            }

            /* DIFROL */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.DIFROL_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {          
                    Difrol.Visible = (solicitudModificacionService.esComunaFronteriza(solicitudConcesion.idSolConcesion) 
                    && (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.DIFROL)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_DIFROL))); 
            }

            /* Informe Ambiental */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.INFORME_AMBIENTAL_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                    Informeambiental.Visible = (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.APLICA_SEA)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_INFORME_AMBIENTAL));
            }

            /* Antecedentes Complementarios */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.ANTECEDENTES_COMPLEMENTARIOS_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                    Antecedentescomplementarios.Visible = (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.ANTECEDENTES_COMPLEMENTARIOS)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.CERTIFICADO_REGISTRO_OPERACION)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_ANTECEDENTES_COMPLEMENTARIOS));
            }

            /* Planos */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.PLANOS_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                    Planos.Visible = (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.ANTECEDENTES_PLANOS)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.INFORME_TECNICO_UOT)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_PLANOS));
            }

            /* Informe DAC */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.INFORME_DAC_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                    Informedac.Visible = (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.INFORME_DAC)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.DEVOLUCION_JURIDICA)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_INFORME_DAC));
            }

            /* Resolución SSP */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.RESOLUCION_SSP_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                    Resolucionssp.Visible = (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.RESOLUCION_SSP)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.DEVOLUCION_SSFFAA)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_RESOLUCION_SSP));
            }

            /* Resolución SSFFAA */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.RESOLUCION_SSFFAA_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB);
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

            SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[paginas.solicitudModificacionAmerbSession];

            if (solicitudConcesion == null)
            {
                Response.Redirect("~/Solicitudes/ModificacionAmerb/administrarSolicitudModificacionAmerb.aspx");
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
                case "Solicitud de Modificación de Concesión":
                    ul.Controls.Add(Crea_Link("Datos del Trámite de Modificación", "~/Solicitudes/ModificacionAmerb/datosModificacionAmerb.aspx", "datosModificacionAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Identificación del Titular", "~/Solicitudes/ModificacionAmerb/identificacionTitularModificacionAmerb.aspx", "identificacionTitularModificacionAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Referencia Global", "~/Solicitudes/Registrar/general.aspx", "general.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Antecedentes del Sector", "~/Solicitudes/Registrar/antecedDelSector.aspx", "antecedDelSector.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Proyecto Técnico", "~/Solicitudes/Registrar/proyTecnico.aspx", "proyTecnico.aspx", nombre_archivo));
                    //ul.Controls.Add(Crea_Link("Informes y Resoluciones", "~/Solicitudes/Registrar/informesResoluciones.aspx", "informesResoluciones.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Modificación de Amerb", "~/Solicitudes/ModificacionAmerb/unidadesEspacialesModificacionAmerb.aspx", "unidadesEspacialesModificacionAmerb.aspx", nombre_archivo));
                    break;

                case "Informes y Resoluciones":
                    ul.Controls.Add(Crea_Link("Evaluacion URB", "~/Solicitudes/ModificacionAmerb/pestanaEvaluacionURBModificacionAmerb.aspx", "pestanaEvaluacionURBModificacionAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Suficiencia Formal", "~/Solicitudes/ModificacionAmerb/pestanaSuficienciaFormalModificacionAmerb.aspx", "pestanaSuficienciaFormalModificacionAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("IT U.O.T.", "~/Solicitudes/ModificacionAmerb/pestanaInformeDeCartografiaModificacionAmerb.aspx", "pestanaInformeDeCartografiaModificacionAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Inspeccion en terreno", "~/Solicitudes/ModificacionAmerb/pestanaInspeccionTerrenoModificacionAmerb.aspx", "pestanaInspeccionTerrenoModificacionAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Banco Natural", "~/Solicitudes/ModificacionAmerb/pestanaBancoNaturalModificacionAmerb.aspx", "pestanaBancoNaturalModificacionAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Difusión Banco Natural", "~/Solicitudes/ModificacionAmerb/pestanaDifusionBancoNaturalModificacionAmerb.aspx", "pestanaDifusionBancoNaturalModificacionAmerb.aspx", nombre_archivo));
                    if (solicitudConcesion.comunaFronteriza) //SOLO APLICA PARA LAS COMUNAS FRONTERIZAS
                    {
                        Difrol.Visible = true;
                        ul.Controls.Add(Crea_Link("DIFROL", "~/Solicitudes/ModificacionAmerb/pestanaDifrolModificacionAmerb.aspx", "pestanaDifrolModificacionAmerb.aspx", nombre_archivo));
                    }
                    ul.Controls.Add(Crea_Link("Informe Ambiental", "~/Solicitudes/ModificacionAmerb/pestanaInformeAmbientalModificacionAmerb.aspx", "pestanaInformeAmbientalModificacionAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Antecedentes Complementarios", "~/Solicitudes/ModificacionAmerb/pestanaAntecedentesComplementariosModificacionAmerb.aspx", "pestanaAntecedentesComplementariosModificacionAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Planos", "~/Solicitudes/ModificacionAmerb/pestanaPlanosModificacionAmerb.aspx", "pestanaPlanosModificacionAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Informe DAC", "~/Solicitudes/ModificacionAmerb/pestanaInformeDACModificacionAmerb.aspx", "pestanaInformeDACModificacionAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Resolución SSP", "~/Solicitudes/ModificacionAmerb/pestanaResolucionSSP.aspx", "pestanaResolucionSSPModificacionAmerb.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Resolución SSFFAA", "~/Solicitudes/ModificacionAmerb/pestanaResolucionSSFFAAModificacionAmerb.aspx", "pestanaResolucionSSFFAAModificacionAmerb.aspx", nombre_archivo));
                    break;

                case "Administrador de Documentos":
                    ul.Controls.Add(Crea_Link("Administrador de Documentos", "~/Solicitudes/ModificacionAmerb/ingresarDocumentoModificacionAmerb.aspx", "ingresarDocumentoModificacionAmerb.aspx", nombre_archivo));
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