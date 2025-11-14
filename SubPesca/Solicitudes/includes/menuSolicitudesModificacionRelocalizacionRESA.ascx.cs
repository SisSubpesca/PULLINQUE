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

    //BORRAR

    public partial class menuSolicitudesModificacionRelocalizacionRESA : System.Web.UI.UserControl
    {
        SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();
        DespliegueMenuSeccionDA despliegueMenuSeccionDA = new DespliegueMenuSeccionDA();

        protected void Page_Load(object sender, EventArgs e)
        {
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Solicitud de Modificación de Centro de Faenamiento"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Informes y Resoluciones"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Administrador de Documentos"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Portal"));

            SolicitudConcesion solicitudConcesion = (SolicitudConcesion)Session[paginas.solicitudRelocalizacionSessionRESA];

            /*IT U.O.T */
            List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.ITC_OUT_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_MOD_CENTRO_FAENAMIENTO);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.ITC_UOT)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.UNIDADES_DE_DEPENDENCIA)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_ITC_UOT))
                {
                    ITUOT.Visible = true;
                }

            }

            /* Inspección en Terreno */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.INSPECCION_EN_TERRENO_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_MOD_CENTRO_FAENAMIENTO);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.INSPECCION_TERRENO)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.COORDENADAS_GEOGRAFICAS_INSPECCION_TERRENO)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_INSPECCION_TERRENO))
                {
                    Inspeccionterreno.Visible = true;
                }

            }

            /* Banco Natural */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.BANCO_NATURAL_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_MOD_CENTRO_FAENAMIENTO);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.BANCO_NATURAL)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_BCO_NATURAL))
                {
                    Banconatural.Visible = true;
                }
            }

            /* Difusión de Banco Natural */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.DIFUSION_BANCO_NATURAL_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_MOD_CENTRO_FAENAMIENTO);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.DIFUSION_BANCO_NATURAL)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_DIFUSION_BANCO_NATURAL))
                {
                    Difusionbn.Visible = true;
                }
            }

            /* DIFROL */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.DIFROL_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_MOD_CENTRO_FAENAMIENTO);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                if ((solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.DIFROL)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_DIFROL))
                    && solicitudModificacionService.esComunaFronteriza(solicitudConcesion.idSolConcesion))
                {
                    Difrol.Visible = true;
                }

            }

            /* Informe Ambiental */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.INFORME_AMBIENTAL_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_MOD_CENTRO_FAENAMIENTO);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.APLICA_SEA)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_INFORME_AMBIENTAL))
                {
                    Difrol.Visible = true;
                }
            }

            /* Antecedentes Complementarios */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.ANTECEDENTES_COMPLEMENTARIOS_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_MOD_CENTRO_FAENAMIENTO);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.ANTECEDENTES_COMPLEMENTARIOS)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.CERTIFICADO_REGISTRO_OPERACION)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_ANTECEDENTES_COMPLEMENTARIOS))
                {
                    Antecedentescomplementarios.Visible = true;
                }

            }


            /* Planos */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.PLANOS_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_MOD_CENTRO_FAENAMIENTO);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.ANTECEDENTES_PLANOS)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.INFORME_TECNICO_UOT)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_PLANOS))
                {
                    Planos.Visible = true;
                }
            }

            /* Informe DAC */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.INFORME_DAC_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_MOD_CENTRO_FAENAMIENTO);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.INFORME_DAC)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.DEVOLUCION_JURIDICA)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_INFORME_DAC))
                {
                    Informedac.Visible = true;
                }
            }

            /* Resolución SSP */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.RESOLUCION_SSP_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_MOD_CENTRO_FAENAMIENTO);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.RESOLUCION_SSP)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.DEVOLUCION_SSFFAA)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_RESOLUCION_SSP))
                {
                    Resolucionssp.Visible = true;
                }
            }

            /* Resolución SSFFAA */
            despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.RESOLUCION_SSFFAA_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_MOD_CENTRO_FAENAMIENTO);
            if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
            {
                if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.RESOLUCION_SSFFAA)
                    || solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_RESOLUCION_SSFFAA))
                {
                    Resolucionssffaa.Visible = true;
                }
            }
        }

        protected HtmlGenericControl Crea_OpcionesModulo(string modulo)
        {
            Label texto = new Label();
            texto.Text = modulo;

            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.Controls.Add(texto);

            SolicitudConcesion solicitudModificacion = (SolicitudConcesion)Session[paginas.solicitudRelocalizacionSessionRESA];

           

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
                case "Solicitud de Modificación de Centro de Faenamiento":


                    ul.Controls.Add(Crea_Link("Datos del Trámite de Modificación", "~/Solicitudes/ModificacionCentroFaenamiento/datosModificacionCentroFaenamiento.aspx", "datosModificacionCentroFaenamiento.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Identificación del Titular", "~/Solicitudes/ModificacionCentroFaenamiento/identificacionTitularModificacionCentroFaenamiento.aspx", "identificacionTitularModificacionCentroFaenamiento.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Referencia Global", "~/Solicitudes/ModificacionCentroFaenamiento/generalModificacionCentroFaenamiento.aspx", "generalModificacionCentroFaenamiento.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Antecedentes del Sector", "~/Solicitudes/ModificacionCentroFaenamiento/antecedDelSectorModificacionCentroFaenamiento.aspx", "antecedDelSectorModificacionCentroFaenamiento.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Proyecto Técnico", "~/Solicitudes/ModificacionCentroFaenamiento/proyTecnicoModificacionCentroFaenamiento.aspx", "proyTecnicoModificacionCentroFaenamiento.aspx", nombre_archivo));
                    //ul.Controls.Add(Crea_Link("Informes y Resoluciones", "~/Solicitudes/ModificacionCentroFaenamiento/informesResolucionesModificacionCentroFaenamiento.aspx", "informesResolucionesModificacionCentroFaenamiento.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Modificación de Centro de Faenamiento", "~/Solicitudes/ModificacionCentroFaenamiento/unidadesEspacialesModificacionCentroFaenamiento.aspx", "unidadesEspacialesModificacionCentroFaenamiento.aspx", nombre_archivo));

                    break;

                case "Informes y Resoluciones":
                    ul.Controls.Add(Crea_Link("IT U.O.T.", "~/Solicitudes/ModificacionCentroFaenamiento/pestanaInformeDeCartografiaModificacionCentroFaenamiento.aspx", "pestanaInformeDeCartografiaModificacionCentroFaenamiento.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Inspeccion en terreno", "~/Solicitudes/ModificacionCentroFaenamiento/pestanaInspeccionTerrenoModificacionCentroFaenamiento.aspx", "pestanaInspeccionTerrenoModificacionCentroFaenamiento.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Banco Natural", "~/Solicitudes/ModificacionCentroFaenamiento/pestanaBancoNaturalModificacionCentroFaenamiento.aspx", "pestanaBancoNaturalModificacionCentroFaenamiento.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Difusión Banco Natural", "~/Solicitudes/ModificacionCentroFaenamiento/pestanaDifusionBancoNaturalModificacionCentroFaenamiento.aspx", "pestanaDifusionBancoNaturalModificacionCentroFaenamiento.aspx", nombre_archivo));
                    if (solicitudModificacion.comunaFronteriza) //SOLO APLICA PARA LAS COMUNAS FRONTERIZAS
                    {
                        Difrol.Visible = true;
                        ul.Controls.Add(Crea_Link("DIFROL", "~/Solicitudes/ModificacionCentroFaenamiento/pestanaDifrolModificacionCentroFaenamiento.aspx", "pestanaDifrolModificacionCentroFaenamiento.aspx", nombre_archivo));
                    }
                    ul.Controls.Add(Crea_Link("Informe Ambiental", "~/Solicitudes/ModificacionCentroFaenamiento/pestanaInformeAmbientalModificacionCentroFaenamiento.aspx", "pestanaInformeAmbientalModificacionCentroFaenamiento.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Antecedentes Complementarios", "~/Solicitudes/ModificacionCentroFaenamiento/pestanaAntecedentesComplementariosModificacionCentroFaenamiento.aspx", "pestanaAntecedentesComplementariosModificacionCentroFaenamiento.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Planos", "~/Solicitudes/ModificacionCentroFaenamiento/pestanaPlanosModificacionCentroFaenamiento.aspx", "pestanaPlanosModificacionCentroFaenamiento.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Informe DAC", "~/Solicitudes/ModificacionCentroFaenamiento/pestanaInformeDACModificacionCentroFaenamiento.aspx", "pestanaInformeDACModificacionCentroFaenamiento.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Resolución SSP", "~/Solicitudes/ModificacionCentroFaenamiento/pestanaResolucionSSPModificacionCentroFaenamiento.aspx", "pestanaResolucionSSPModificacionCentroFaenamiento.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Resolución SSFFAA", "~/Solicitudes/ModificacionCentroFaenamiento/pestanaResolucionSSFFAAModificacionCentroFaenamiento.aspx", "pestanaResolucionSSFFAAModificacionCentroFaenamiento.aspx", nombre_archivo));
                    break;

                case "Administrador de Documentos":

                    ul.Controls.Add(Crea_Link("Administrador de Documentos", "~/Solicitudes/ModificacionCentroFaenamiento/ingresarDocumentoModificacionCentroFaenamiento.aspx", "ingresarDocumentoModificacionCentroFaenamiento.aspx", nombre_archivo));

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