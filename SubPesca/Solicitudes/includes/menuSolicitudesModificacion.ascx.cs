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
    public partial class menuSolicitudesModificacion : System.Web.UI.UserControl
    {

        SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();
        DespliegueMenuSeccionDA despliegueMenuSeccionDA = new DespliegueMenuSeccionDA();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Solicitud de Modificación de Concesión"));
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

            SolicitudConcesion solicitudModificacion = (SolicitudConcesion)Session["SolicitudModificacion"];
            

            if (solicitudModificacion == null)
            {
                Response.Redirect("~/Solicitudes/Modificacion/administrarSolicitudModificacion.aspx");
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

                    ul.Controls.Add(Crea_Link("Datos del Trámite de Modificación", "~/Solicitudes/Modificacion/datosConcesionAcuicultura.aspx","datosConcesionAcuicultura.aspx",nombre_archivo));
                    ul.Controls.Add(Crea_Link("Identificación del Titular", "~/Solicitudes/Modificacion/identificacionTitularConcesion.aspx", "identificacionTitularConcesion.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Referencia Global", "~/Solicitudes/Modificacion/generalModificacion.aspx", "generalModificacion.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Antecedentes del Sector", "~/Solicitudes/Modificacion/antecedDelSectorModificacion.aspx", "antecedDelSectorModificacion.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Proyecto Técnico", "~/Solicitudes/Modificacion/proyTecnicoModificacion.aspx", "proyTecnicoModificacion.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Informes y Resoluciones", "~/Solicitudes/Modificacion/informesResoluciones.aspx", "informesResoluciones.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Modificación de Concesión", "~/Solicitudes/Modificacion/unidadesEspaciales.aspx", "unidadesEspaciales.aspx", nombre_archivo));

                    break;

                case "Informes y Resoluciones":

                    List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(0, 0); //OBTIENE TODA LA COMBINATORIA DE DESPLIEQUE DE SECCIONES PARA LAS MODIFICACIONES

                    //PESTAÑA IT UOT
                    HashSet<int> seccionesEvaluar = new HashSet<int>();
                    seccionesEvaluar.Add(rbSeccion.ITC_UOT);
                    seccionesEvaluar.Add(rbSeccion.UNIDADES_DE_DEPENDENCIA);
                    seccionesEvaluar.Add(rbSeccion.OBSERVACIONES_ITC_UOT);

                    if (solicitudModificacionService.despliegueMenu(solicitudModificacion, despliegueMenuSeccionList, seccionesEvaluar))
                    {
                        ITUOT.Visible = true;
                        ul.Controls.Add(Crea_Link("IT U.O.T.", "~/Solicitudes/Modificacion/pestanaInformeDeCartografiaModCon.aspx", "pestanaInformeDeCartografiaModCon.aspx", nombre_archivo));
                    }


                    //PESTAÑA INSPECCION EN TERRENO
                    seccionesEvaluar = new HashSet<int>();
                    seccionesEvaluar.Add(rbSeccion.INSPECCION_TERRENO);
                    seccionesEvaluar.Add(rbSeccion.COORDENADAS_GEOGRAFICAS_INSPECCION_TERRENO);
                    seccionesEvaluar.Add(rbSeccion.OBSERVACIONES_INSPECCION_TERRENO);

                    if (solicitudModificacionService.despliegueMenu(solicitudModificacion, despliegueMenuSeccionList, seccionesEvaluar))
                    {
                        Inspeccionterreno.Visible = true;
                        ul.Controls.Add(Crea_Link("Inspeccion en terreno", "~/Solicitudes/Modificacion/pestanaInspeccionTerrenoModCon.aspx", "pestanaInspeccionTerrenoModCon.aspx", nombre_archivo));
                    }


                    //PESTAÑA BANCO NATURAL
                    seccionesEvaluar = new HashSet<int>();
                    seccionesEvaluar.Add(rbSeccion.BANCO_NATURAL);
                    seccionesEvaluar.Add(rbSeccion.OBSERVACIONES_BCO_NATURAL);

                    if (solicitudModificacionService.despliegueMenu(solicitudModificacion, despliegueMenuSeccionList, seccionesEvaluar))
                    {
                        Banconatural.Visible = true;
                        ul.Controls.Add(Crea_Link("Banco Natural", "~/Solicitudes/Modificacion/pestanaBancoNaturalModCon.aspx", "pestanaBancoNaturalModCon.aspx", nombre_archivo));
                    }


                    //PESTAÑA DIFUSION BANCO NATURAL
                    seccionesEvaluar = new HashSet<int>();
                    seccionesEvaluar.Add(rbSeccion.DIFUSION_BANCO_NATURAL);
                    seccionesEvaluar.Add(rbSeccion.OBSERVACIONES_DIFUSION_BANCO_NATURAL);

                    if (solicitudModificacionService.despliegueMenu(solicitudModificacion, despliegueMenuSeccionList, seccionesEvaluar))
                    {
                        Difusionbn.Visible = true;
                        ul.Controls.Add(Crea_Link("Difusión Banco Natural", "~/Solicitudes/Modificacion/pestanaDifusionBancoNaturalModCon.aspx", "pestanaDifusionBancoNaturalModCon.aspx", nombre_archivo));
                    }


                    //PESTAÑA DIFROL
                    seccionesEvaluar = new HashSet<int>();
                    seccionesEvaluar.Add(rbSeccion.DIFROL);
                    seccionesEvaluar.Add(rbSeccion.OBSERVACIONES_DIFROL);

                    if (solicitudModificacionService.despliegueMenu(solicitudModificacion, despliegueMenuSeccionList, seccionesEvaluar))
                    {
                        if (solicitudModificacion.comunaFronteriza) //SOLO APLICA PARA LAS COMUNAS FRONTERIZAS
                        {
                            Difrol.Visible = true;
                            ul.Controls.Add(Crea_Link("DIFROL", "~/Solicitudes/Modificacion/pestanaDifrolModCon.aspx", "pestanaDifrolModCon.aspx", nombre_archivo));
                        }
                    }

                    //PESTAÑA INFORME AMBIENTAL
                    seccionesEvaluar = new HashSet<int>();
                    seccionesEvaluar.Add(rbSeccion.APLICA_SEA);
                    seccionesEvaluar.Add(rbSeccion.OBSERVACIONES_INFORME_AMBIENTAL);

                    if (solicitudModificacionService.despliegueMenu(solicitudModificacion, despliegueMenuSeccionList, seccionesEvaluar))
                    {
                        Informeambiental.Visible = true;
                        ul.Controls.Add(Crea_Link("Informe Ambiental", "~/Solicitudes/Modificacion/pestanaInformeAmbientalModCon.aspx", "pestanaInformeAmbientalModCon.aspx", nombre_archivo));
                    }

                    //PESTAÑA ANTECEDENTES COMPLEMENTARIOS
                    seccionesEvaluar = new HashSet<int>();
                    seccionesEvaluar.Add(rbSeccion.ANTECEDENTES_COMPLEMENTARIOS);
                    seccionesEvaluar.Add(rbSeccion.CERTIFICADO_REGISTRO_OPERACION);
                    seccionesEvaluar.Add(rbSeccion.OBSERVACIONES_ANTECEDENTES_COMPLEMENTARIOS);

                    if (solicitudModificacionService.despliegueMenu(solicitudModificacion, despliegueMenuSeccionList, seccionesEvaluar))
                    {
                        Antecedentescomplementarios.Visible = true;
                        ul.Controls.Add(Crea_Link("Antecedentes Complementarios", "~/Solicitudes/Modificacion/pestanaAntecedentesComplementariosModCon.aspx", "pestanaAntecedentesComplementariosModCon.aspx", nombre_archivo));
                    }

                    //PESTAÑA PLANOS
                    seccionesEvaluar = new HashSet<int>();
                    seccionesEvaluar.Add(rbSeccion.ANTECEDENTES_PLANOS);
                    seccionesEvaluar.Add(rbSeccion.INFORME_TECNICO_UOT);
                    seccionesEvaluar.Add(rbSeccion.OBSERVACIONES_PLANOS);

                    if (solicitudModificacionService.despliegueMenu(solicitudModificacion, despliegueMenuSeccionList, seccionesEvaluar))
                    {
                        Planos.Visible = true;
                        ul.Controls.Add(Crea_Link("Planos", "~/Solicitudes/Modificacion/pestanaPlanosModCon.aspx", "pestanaPlanosModCon.aspx", nombre_archivo));
                    }

                    //PESTAÑA INFORME DAC
                    seccionesEvaluar = new HashSet<int>();
                    seccionesEvaluar.Add(rbSeccion.INFORME_DAC);
                    seccionesEvaluar.Add(rbSeccion.DEVOLUCION_JURIDICA);
                    seccionesEvaluar.Add(rbSeccion.OBSERVACIONES_INFORME_DAC);

                    if (solicitudModificacionService.despliegueMenu(solicitudModificacion, despliegueMenuSeccionList, seccionesEvaluar))
                    {
                        Informedac.Visible = true;
                        ul.Controls.Add(Crea_Link("Informe DAC", "~/Solicitudes/Modificacion/pestanaInformeDACModCon.aspx", "pestanaInformeDACModCon.aspx", nombre_archivo));
                    }

                    //PESTAÑA RESOLUCION SSP
                    seccionesEvaluar = new HashSet<int>();
                    seccionesEvaluar.Add(rbSeccion.RESOLUCION_SSP);
                    seccionesEvaluar.Add(rbSeccion.DEVOLUCION_SSFFAA);
                    seccionesEvaluar.Add(rbSeccion.OBSERVACIONES_RESOLUCION_SSP);

                    if (solicitudModificacionService.despliegueMenu(solicitudModificacion, despliegueMenuSeccionList, seccionesEvaluar))
                    {
                        Resolucionssp.Visible = true;
                        ul.Controls.Add(Crea_Link("Resolución SSP", "~/Solicitudes/Modificacion/pestanaResolucionSSPModCon.aspx", "pestanaResolucionSSPModCon.aspx", nombre_archivo));
                    }

                    //PESTAÑA INSPECCION EN TERRENO
                    seccionesEvaluar = new HashSet<int>();
                    seccionesEvaluar.Add(rbSeccion.RESOLUCION_SSFFAA);
                    seccionesEvaluar.Add(rbSeccion.OBSERVACIONES_RESOLUCION_SSFFAA);

                    if (solicitudModificacionService.despliegueMenu(solicitudModificacion, despliegueMenuSeccionList, seccionesEvaluar))
                    {
                        Resolucionssffaa.Visible = true;
                        ul.Controls.Add(Crea_Link("Resolución SSFFAA", "~/Solicitudes/Modificacion/pestanaResolucionSSFFAAModCon.aspx", "pestanaResolucionSSFFAAModCon.aspx", nombre_archivo));
                    }
                    break;

                case "Administrador de Documentos":


                    ul.Controls.Add(Crea_Link("Administrador de Documentos", "~/Solicitudes/Modificacion/ingresarDocumento.aspx", "ingresarDocumento.aspx", nombre_archivo));

                    break;

            };

            return ul;
        }

        protected HtmlGenericControl Crea_Link(string opcion, string urldestino, string pagina, string urlActual)
        {
            //Image img = new Image();
            //img.CssClass = "child";
            //img.ImageUrl = "~/App_Themes/admin_style/images/child.gif";

            LinkButton link = new LinkButton();
            link.Text = opcion;
            link.PostBackUrl = urldestino;


            if (pagina != null && urlActual != null && pagina.Trim().ToUpper().Equals(urlActual.Trim().ToUpper())) {
                link.Style.Add("background-color", "#98A3FF;");
            }
            

            /*
            link.Command += new CommandEventHandler(WebSiteLink_Click);
            if (opcion == "WebSite Público")
            {
                link.OnClientClick = "aspnetForm.target = '_blank';";
            };
            */



            Panel div = new Panel();
            div.CssClass = "opc_menuv";
            //div.Controls.Add(img);
            div.Controls.Add(link);

            HtmlGenericControl li = new HtmlGenericControl("li");
            li.Controls.Add(div);

            return li;
        }


    }
}