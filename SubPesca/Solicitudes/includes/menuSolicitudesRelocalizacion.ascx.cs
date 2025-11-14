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
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;
using Datos.Entidades.Relocalizacion;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;


namespace SubPesca.Solicitudes.includes
{
    public partial class menuSolicitudesRelocalizacion : System.Web.UI.UserControl
    {
        RelocalizacionService relocalizacionService = new RelocalizacionService();
        DespliegueSeccionService despliegueSeccionService = new DespliegueSeccionService();

        protected void Page_Load(object sender, EventArgs e)
        {

            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Menú Relocalización"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Informes y Resoluciones"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Administrador de Documentos"));
            Content_MenuV.Controls.Add(Crea_OpcionesModulo("Portal"));


            SolicitudConcesion solicitudModificacion = (SolicitudConcesion)Session[paginas.solicitudRelocalizacionSession];

            //detalle Sector
            DetalleSector aDetalleSector = relocalizacionService.obtenerDetalleSector_Solicitud(solicitudModificacion.idSolConcesion);

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
            if (solicitudModificacion.comunaFronteriza)
            {
                //Difrol
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

            SolicitudConcesion solicitudModificacion = (SolicitudConcesion)Session[paginas.solicitudRelocalizacionSession];
            
            if (solicitudModificacion == null)
            {
                Response.Redirect("~/Solicitudes/Relocalizacion/administrarSolicitudRelocalizacion.aspx");
            }

            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.Controls.Add(texto);

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
                case "Menú Relocalización":


                    ul.Controls.Add(Crea_Link("Datos del Trámite de Modificación", "~/Solicitudes/Relocalizacion/datosTramiteRelocalizacion.aspx", "datosTramiteRelocalizacion.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Identificación del Titular", "~/Solicitudes/Relocalizacion/identificacionSolicitanteRelocalizacion.aspx", "identificacionSolicitanteRelocalizacion.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Referencia Global", "~/Solicitudes/Relocalizacion/generalRelocalizacion.aspx", "generalRelocalizacion.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Antecedentes del Sector", "~/Solicitudes/Relocalizacion/antecedDelSectorRelocalizacion.aspx", "antecedDelSectorRelocalizacion.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Proyecto Técnico", "~/Solicitudes/Relocalizacion/proyTecnicoRelocalizacion.aspx", "proyTecnicoRelocalizacion.aspx", nombre_archivo));
                    //ul.Controls.Add(Crea_Link("Informes y Resoluciones", "~/Solicitudes/ModificacionCentroFaenamiento/informesResolucionesModificacionCentroFaenamiento.aspx", "informesResolucionesModificacionCentroFaenamiento.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Modificación de Centro de Faenamiento", "~/Solicitudes/Relocalizacion/unidadEspacialRelocalizacion.aspx", "unidadEspacialRelocalizacion.aspx", nombre_archivo));

                    break;

                case "Informes y Resoluciones":
                    ul.Controls.Add(Crea_Link("IT U.O.T.", "~/Solicitudes/Relocalizacion/pestanaInformeDeCartografiaRelocalizacion.aspx", "pestanaInformeDeCartografiaRelocalizacion.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Inspeccion en terreno", "~/Solicitudes/Relocalizacion/pestanaInspeccionTerrenoRelocalizacion.aspx", "pestanaInspeccionTerrenoRelocalizacion.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Banco Natural", "~/Solicitudes/Relocalizacion/pestanaBancoNaturalRelocalizacion.aspx", "pestanaBancoNaturalRelocalizacion.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Difusión Banco Natural", "~/Solicitudes/Relocalizacion/pestanaDifusionBancoNaturalRelocalizacion.aspx", "pestanaDifusionBancoNaturalRelocalizacion.aspx", nombre_archivo));
                    if (solicitudModificacion.comunaFronteriza) //SOLO APLICA PARA LAS COMUNAS FRONTERIZAS
                    {
                        Difrol.Visible = true;
                        ul.Controls.Add(Crea_Link("DIFROL", "~/Solicitudes/Relocalizacion/pestanaDifrolRelocalizacion.aspx", "pestanaDifrolRelocalizacion.aspx", nombre_archivo));
                    }
                    ul.Controls.Add(Crea_Link("Informe Ambiental", "~/Solicitudes/Relocalizacion/pestanaInformeAmbientalRelocalizacion.aspx", "pestanaInformeAmbientalRelocalizacion.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Antecedentes Complementarios", "~/Solicitudes/Relocalizacion/pestanaAntecedentesComplementariosRelocalizacion.aspx", "pestanaAntecedentesComplementariosRelocalizacion.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Planos", "~/Solicitudes/Relocalizacion/pestanaPlanosRelocalizacion.aspx", "pestanaPlanosRelocalizacion.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Informe DAC", "~/Solicitudes/Relocalizacion/pestanaInformeDACRelocalizacion.aspx", "pestanaInformeDACRelocalizacion.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Resolución SSP", "~/Solicitudes/Relocalizacion/pestanaResolucionSSPRelocalizacion.aspx", "pestanaResolucionSSPRelocalizacion.aspx", nombre_archivo));
                    ul.Controls.Add(Crea_Link("Resolución SSFFAA", "~/Solicitudes/Relocalizacion/pestanaResolucionSSFFAARelocalizacion.aspx", "pestanaResolucionSSFFAARelocalizacion.aspx", nombre_archivo));
                    break;

                case "Administrador de Documentos":

                    ul.Controls.Add(Crea_Link("Administrador de Documentos", "~/Solicitudes/Relocalizacion/ingresarDocumentoRelocalizacion.aspx", "ingresarDocumentoRelocalizacion.aspx", nombre_archivo));

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