using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Datos.Entidades;
using Datos.Contantes;
using Datos.Entidades.Relocalizacion;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;

namespace SubPesca.Solicitudes.includes
{
    public partial class menuSolicitudesRelocalizacionRESA : System.Web.UI.UserControl
    {
        SolicitudConcesionService solicitudConcesionService = new SolicitudConcesionService();
        RequerimientoService requerimientoService = new RequerimientoService();
        RelocalizacionRESAService relocalizacionRESAService = new RelocalizacionRESAService();
        DespliegueSeccionService despliegueSeccionService = new DespliegueSeccionService();

        protected void Page_Load(object sender, EventArgs e)
        {

            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Menú Relocalización RESA"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Informes y Resoluciones"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Administrador de Documentos"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Portal"));

            SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[paginas.solicitudRelocalizacionSessionRESA];

            DetalleSector aDetalleSector = relocalizacionRESAService.obtenerDetalleSector_SolicitudRESA(solicitudConcesion.idSolConcesion);

            //IT UOT. (informe de cartofrafia)
            if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ITC_UOT, aDetalleSector.tipoRelocalizacion.id) == false
                && despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.UNIDADES_DE_DEPENDENCIA, aDetalleSector.tipoRelocalizacion.id) == false)
            {
                ITUOT.Visible = false;
            }

            //Inspección a Terreno
            if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INSPECCION_TERRENO, aDetalleSector.tipoRelocalizacion.id) == false
                && despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.COORDENADAS_GEOGRAFICAS_INSPECCION_TERRENO, aDetalleSector.tipoRelocalizacion.id) == false)
            {
                Inspeccionterreno.Visible = false;
            }

            //Banco Natural
            Banconatural.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.BANCO_NATURAL, aDetalleSector.tipoRelocalizacion.id);

            //Difusión Banco Natural
            Difusionbn.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DIFUSION_BANCO_NATURAL, aDetalleSector.tipoRelocalizacion.id);

            //Difrol
            if (solicitudConcesion.comunaFronteriza) //SOLO APLICA PARA LAS COMUNAS FRONTERIZAS
            {
                Difrol.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DIFROL, aDetalleSector.tipoRelocalizacion.id);
            }

            //Informe Ambiental
            Informeambiental.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.APLICA_SEA, aDetalleSector.tipoRelocalizacion.id);



            //Antecedentes Complementarios
            if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_COMPLEMENTARIOS, aDetalleSector.tipoRelocalizacion.id) == false
                && despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.CERTIFICADO_REGISTRO_OPERACION, aDetalleSector.tipoRelocalizacion.id) == false)
            {
                Antecedentescomplementarios.Visible = false;
            }


            //Planos
            if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_PLANOS, aDetalleSector.tipoRelocalizacion.id) == false
                && despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INFORME_TECNICO_UOT, aDetalleSector.tipoRelocalizacion.id) == false)
            {
                Planos.Visible = false;
            }

            //Informe DAC
            if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INFORME_DAC, aDetalleSector.tipoRelocalizacion.id) == false
                && despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DEVOLUCION_JURIDICA, aDetalleSector.tipoRelocalizacion.id) == false)
            {
                Informedac.Visible = false;
            }

            //Resolucion SSP
            if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.RESOLUCION_SSP, aDetalleSector.tipoRelocalizacion.id) == false 
                && despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DEVOLUCION_SSFFAA, aDetalleSector.tipoRelocalizacion.id) == false)
            {
                Resolucionssp.Visible = false;
            }


            //Resolución SSFFAA
            Resolucionssffaa.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.RESOLUCION_SSFFAA, aDetalleSector.tipoRelocalizacion.id);

        }

        protected HtmlGenericControl Crea_OpcionesModulo(string modulo)
        {
            Label texto = new Label();
            texto.Text = modulo;

            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.Controls.Add(texto);

            SolicitudConcesion solicitudModificacion =  (SolicitudConcesion)Session[paginas.solicitudRelocalizacionSessionRESA];
            if (solicitudModificacion == null)
            {
                Response.Redirect("~/Solicitudes/RelocalizacionRESA/administrarSolicitudRelocalizacionRESA.aspx");
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
                    ul.Controls.Add(Crea_Link("Identificación del Solicitante", "~/Solicitudes/RelocalizacionRESA/datosTramiteRelocalizacionRESA.aspx", "datosTramiteRelocalizacionRESA.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Referencia Global", "~/Solicitudes/RelocalizacionRESA/generalRelocalizacionRESA.aspx", "generalRelocalizacionRESA.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Antecedentes del Sector", "~/Solicitudes/RelocalizacionRESA/antecedDelSectorRelocalizacionRESA.aspx", "antecedDelSectorRelocalizacionRESA.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Proyecto Técnico", "~/Solicitudes/RelocalizacionRESA/proyTecnicoRelocalizacionRESA.aspx", "proyTecnicoRelocalizacionRESA.aspx", nombre_archivo));
                    //ul.Controls.Add(Crea_Link("Informes y Resoluciones", "~/Solicitudes/Registrar/informesResoluciones.aspx", "informesResoluciones.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Creación de Concesión", "~/Solicitudes/RelocalizacionRESA/unidadEspacialRelocalizacionRESA.aspx", "unidadEspacialRelocalizacionRESA.aspx", nombre_archivo));
                    break;

                case "Informes y Resoluciones":
                    ul.Controls.Add(Crea_Link("IT U.O.T.", "~/Solicitudes/RelocalizacionRESA/pestanaInformeDeCartografiaRelocalizacionRESA.aspx", "pestanaInformeDeCartografiaRelocalizacionRESA.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Inspeccion en terreno", "~/Solicitudes/RelocalizacionRESA/pestanaInspeccionTerrenoRelocalizacionRESA.aspx", "pestanaInspeccionTerrenoRelocalizacionRESA.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Banco Natural", "~/Solicitudes/RelocalizacionRESA/pestanaBancoNaturalRelocalizacionRESA.aspx", "pestanaBancoNaturalRelocalizacionRESA.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Difusión Banco Natural", "~/Solicitudes/RelocalizacionRESA/pestanaDifusionBancoNaturalRelocalizacionRESA.aspx", "pestanaDifusionBancoNaturalRelocalizacionRESA.aspx", nombre_archivo));
                    if (solicitudModificacion.comunaFronteriza) //SOLO APLICA PARA LAS COMUNAS FRONTERIZAS
                    {
                        Difrol.Visible = true;
                        ul.Controls.Add(Crea_Link("DIFROL", "~/Solicitudes/RelocalizacionRESA/pestanaDifrolRelocalizacionRESA.aspx", "pestanaDifrolRelocalizacionRESA.aspx", nombre_archivo));
                    }
                    ul.Controls.Add(Crea_Link("Informe Ambiental", "~/Solicitudes/RelocalizacionRESA/pestanaInformeAmbientalRelocalizacionRESA.aspx", "pestanaInformeAmbientalRelocalizacionRESA.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Antecedentes Complementarios", "~/Solicitudes/RelocalizacionRESA/pestanaAntecedentesComplementariosRelocalizacionRESA.aspx", "pestanaAntecedentesComplementariosRelocalizacionRESA.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Planos", "~/Solicitudes/RelocalizacionRESA/pestanaPlanosRelocalizacionRESA.aspx", "pestanaPlanosRelocalizacionRESA.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Informe DAC", "~/Solicitudes/RelocalizacionRESA/pestanaInformeDACRelocalizacionRESA.aspx", "pestanaInformeDACRelocalizacionRESA.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Resolución SSP", "~/Solicitudes/RelocalizacionRESA/pestanaResolucionSSPRelocalizacionRESA.aspx", "pestanaResolucionSSPRelocalizacionRESA.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Resolución SSFFAA", "~/Solicitudes/RelocalizacionRESA/pestanaResolucionSSFFAARelocalizacionRESA.aspx", "pestanaResolucionSSFFAARelocalizacionRESA.aspx", nombre_archivo));
                    break;

                case "Administrador de Documentos":
                    ul.Controls.Add(Crea_Link("Administrador de Documentos", "~/Solicitudes/RelocalizacionRESA/ingresarDocumentoRelocalizacionRESA.aspx", "ingresarDocumentoRelocalizacionRESA.aspx", nombre_archivo));
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