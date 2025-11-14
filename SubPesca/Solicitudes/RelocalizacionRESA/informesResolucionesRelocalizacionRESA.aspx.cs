using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using Datos.Utilidades;
using Datos.Contantes;
using Datos.Entidades;
using Datos.Entidades.Relocalizacion;

namespace SubPesca.Solicitudes.RelocalizacionRESA
{
    public partial class informesResolucionesRelocalizacionRESA : System.Web.UI.Page
    {
        SolicitudConcesionService solicitudConcesionService = new SolicitudConcesionService();
        RequerimientoService requerimientoService = new RequerimientoService();
        RelocalizacionRESAService relocalizacionRESAService = new RelocalizacionRESAService();
        DespliegueSeccionService despliegueSeccionService = new DespliegueSeccionService();

        Funciones funciones = new Funciones();

        protected void setearModulo()
        {
            
            ViewState["URL_VER"] = paginas.URL_VER_RELOCALIZACION_RESA;
            ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_RELOCALIZACION_RESA;
            ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA;
            ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_RELOCALIZACION_RESA;
            ViewState["URL_ERROR"] = paginas.URL_ERROR_RELOCALIZACION_RESA;
            ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSessionRESA;
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // PAGE LOAD
            if (!Page.IsPostBack)
            {

                setearModulo();

                msSometimientoSEA.Text = "";
                PanelSometimientoSEA.Visible = false;
                UpdatePanelSometimientoSEA.Update();


                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion == null)
                {
                    Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
                }


                //SECTOR
                DetalleSector aDetalleSector = relocalizacionRESAService.obtenerDetalleSector_SolicitudRESA(solicitudConcesion.idSolConcesion);
                ViewState["aDetalleSector"] = aDetalleSector;


                FlujoSEA.Items.Clear();
                FlujoSEA.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                FlujoSEA.Items.Insert(1, new ListItem("Sí", Convert.ToString(rbTipo.SOMETIMIENTO_SEA_SOMETE)));
                FlujoSEA.Items.Insert(2, new ListItem("No", Convert.ToString(rbTipo.SOMETIMIENTO_SEA_NO_SOMETE)));
                FlujoSEA.DataBind();

                if (solicitudConcesion.sometimientoSEA != null)
                {
                    FlujoSEA.SelectedValue = Convert.ToString(solicitudConcesion.sometimientoSEA.id);
                }
                FlujoSEA_Secciones();

                //VALIDAR QUE HAYA CONSISTENCIA ENTRE EL SOMETIMIENTO SEA MARCADO POR EL USUARIO Y EL INDICADO EL EL DOCUMENTO DE SOMETIMIENTO SEA (SI DICHO DOCUMENTO EXISTE)
                DocumentoAmbito documentoSometimientoSEA = requerimientoService.obtenerDocPestanaSubRequerimiento(solicitudConcesion.idSolConcesion);

                if (documentoSometimientoSEA != null)
                {

                    if (documentoSometimientoSEA.estadoResultadoResp.id == rbEstadosGenerales.SE_SOMETE_AL_SEA && solicitudConcesion.sometimientoSEA.id != rbTipo.SOMETIMIENTO_SEA_SOMETE)
                    {

                        msSometimientoSEA.Text = "Existe una Resolución Pertinencia SEA que indica que esta solicitud debe someterse a SEA";
                        PanelSometimientoSEA.Visible = true;
                        UpdatePanelSometimientoSEA.Update();

                    }
                    else if (documentoSometimientoSEA.estadoResultadoResp.id == rbEstadosGenerales.NO_SE_SOMETE_AL_SEA && solicitudConcesion.sometimientoSEA.id != rbTipo.SOMETIMIENTO_SEA_NO_SOMETE)
                    {

                        msSometimientoSEA.Text = "Existe una Resolución Pertinencia SEA que indica que esta solicitud no debe someterse a SEA";
                        PanelSometimientoSEA.Visible = true;
                        UpdatePanelSometimientoSEA.Update();

                    }
                }



                //IT UOT. (informe de cartofrafia)
                PanelInforCart.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ITC_UOT, aDetalleSector.tipoRelocalizacion.id);
                PanelunidadDeDependencia.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.UNIDADES_DE_DEPENDENCIA, aDetalleSector.tipoRelocalizacion.id);
                if (PanelInforCart.Visible == false && PanelunidadDeDependencia.Visible == false)
                {
                    PanelLinkITUOT.Visible = false;
                }




                //Inspección a Terreno
                PanelInspeccionTerreno.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INSPECCION_TERRENO, aDetalleSector.tipoRelocalizacion.id);
                PanelInspeccionTerrenoCoordenadas.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.COORDENADAS_GEOGRAFICAS_INSPECCION_TERRENO, aDetalleSector.tipoRelocalizacion.id);
                if (PanelInspeccionTerreno.Visible == false && PanelInspeccionTerrenoCoordenadas.Visible == false)
                {
                    PanelLinkInspeccionTerreno.Visible = false;
                }

                //Banco Natural
                PanelLinkBancoNatural.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.BANCO_NATURAL, aDetalleSector.tipoRelocalizacion.id);

                //Difusión Banco Natural
                PanelLinkDifusionBancoNatural.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DIFUSION_BANCO_NATURAL, aDetalleSector.tipoRelocalizacion.id);

                //Difrol
                PanelLinkDifrol.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DIFROL, aDetalleSector.tipoRelocalizacion.id);

                //SI ES COMUNA FRONTERIZA
                if (PanelLinkDifrol.Visible)
                {
                    PanelLinkDifrol.Visible = solicitudConcesion.comunaFronteriza;
                    PanelDifrol.Visible = solicitudConcesion.comunaFronteriza;
                    PanelDifrolObservacion.Visible = solicitudConcesion.comunaFronteriza;
                }


                //Informe Ambiental
                PanelLinkInformeSEA.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.APLICA_SEA, aDetalleSector.tipoRelocalizacion.id);



                //Antecedentes Complementarios
                PanelAntecedentesComplementarios.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_COMPLEMENTARIOS, aDetalleSector.tipoRelocalizacion.id);
                PanelCertificadoOperacion.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.CERTIFICADO_REGISTRO_OPERACION, aDetalleSector.tipoRelocalizacion.id);
                if (PanelAntecedentesComplementarios.Visible == false && PanelCertificadoOperacion.Visible == false)
                {
                    PanelLinkAntecedentesComplementarios.Visible = false;
                }


                //Planos
                PanelPlanos.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_PLANOS, aDetalleSector.tipoRelocalizacion.id);
                PanelPlanosInformeTecnicoUOT.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INFORME_TECNICO_UOT, aDetalleSector.tipoRelocalizacion.id);
                if (PanelPlanos.Visible == false && PanelPlanosInformeTecnicoUOT.Visible == false)
                {
                    PanelLinkPlanos.Visible = false;
                }

                //Informe DAC
                PanelInformeDAC.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INFORME_DAC, aDetalleSector.tipoRelocalizacion.id);
                PanelInformeDACDevolucionJuridica.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DEVOLUCION_JURIDICA, aDetalleSector.tipoRelocalizacion.id);
                if (PanelInformeDAC.Visible == false && PanelInformeDACDevolucionJuridica.Visible == false)
                {
                    PanelLinkInformeDAC.Visible = false;
                }

                //Resolucion SSP
                PanelResolucionSSP.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.RESOLUCION_SSP, aDetalleSector.tipoRelocalizacion.id);
                PanelResolucionSSPDevolucionSSFFAA.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DEVOLUCION_SSFFAA, aDetalleSector.tipoRelocalizacion.id);
                if (PanelResolucionSSP.Visible == false && PanelResolucionSSPDevolucionSSFFAA.Visible == false)
                {
                    PanelLinkResolucionSSP.Visible = false;
                }


                //Resolución SSFFAA
                PanelLinkResolucionSSFFAA.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.RESOLUCION_SSFFAA, aDetalleSector.tipoRelocalizacion.id);


            }
        }

        protected void FlujoSEA_Secciones()
        {
            if (Convert.ToInt32(FlujoSEA.SelectedValue) == rbTipo.SOMETIMIENTO_SEA_SOMETE)
            {

                PanelInformeSEACartaTitular.Visible = true;
                PanelInformeSEARCA.Visible = true;
                PanelInformeSEAConsultaSEA.Visible = false;
                PanelInformeSEACartaAmbiental.Visible = false;
                PanelInformeSEAInformeUnidadAmbiental.Visible = false;
            }
            else if (Convert.ToInt32(FlujoSEA.SelectedValue) == rbTipo.SOMETIMIENTO_SEA_NO_SOMETE)
            {
                PanelInformeSEACartaTitular.Visible = false;
                PanelInformeSEARCA.Visible = false;
                PanelInformeSEAConsultaSEA.Visible = true;
                PanelInformeSEACartaAmbiental.Visible = true;
                PanelInformeSEAInformeUnidadAmbiental.Visible = true;

            }
            else
            {

                PanelInformeSEACartaTitular.Visible = false;
                PanelInformeSEARCA.Visible = false;
                PanelInformeSEAConsultaSEA.Visible = false;
                PanelInformeSEACartaAmbiental.Visible = false;
                PanelInformeSEAInformeUnidadAmbiental.Visible = false;

            }

            UpdatePanelInformeSEA.Update();

        }

        protected void FlujoSEA_change(object sender, EventArgs e)
        {

            DetalleSector aDetalleSector = (DetalleSector)ViewState["aDetalleSector"];

            if (Convert.ToInt32(FlujoSEA.SelectedValue) == rbTipo.SOMETIMIENTO_SEA_SOMETE)
            {

                PanelInformeSEACartaTitular.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_AMBIENTALES, aDetalleSector.tipoRelocalizacion.id); ;
                PanelInformeSEARCA.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.RESOLUCION_CALIFICACION_AMBIENTAL, aDetalleSector.tipoRelocalizacion.id); ;
                PanelInformeSEAConsultaSEA.Visible = false;
                PanelInformeSEACartaAmbiental.Visible = false;
                PanelInformeSEAInformeUnidadAmbiental.Visible = false;
            }
            else if (Convert.ToInt32(FlujoSEA.SelectedValue) == rbTipo.SOMETIMIENTO_SEA_NO_SOMETE)
            {
                PanelInformeSEACartaTitular.Visible = false;
                PanelInformeSEARCA.Visible = false;
                PanelInformeSEAConsultaSEA.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.NOTIFICACION_SMA, aDetalleSector.tipoRelocalizacion.id); ;
                PanelInformeSEACartaAmbiental.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_AMBIENTALES_MO, aDetalleSector.tipoRelocalizacion.id); ;
                PanelInformeSEAInformeUnidadAmbiental.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INFORME_AMBIENTALES_MO, aDetalleSector.tipoRelocalizacion.id); ;

            }
            else
            {

                PanelInformeSEACartaTitular.Visible = false;
                PanelInformeSEARCA.Visible = false;
                PanelInformeSEAConsultaSEA.Visible = false;
                PanelInformeSEACartaAmbiental.Visible = false;
                PanelInformeSEAInformeUnidadAmbiental.Visible = false;

            }

            UpdatePanelInformeSEA.Update();

            string script = "mostrarPestanas(" + rbPestana.INFORME_SEA + ");";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptMostrarPestanas" + rbPestana.INFORME_SEA, script.ToString(), true);

        }


        protected void Guardar_Click(object sender, EventArgs e)
        {

            SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

            msSometimientoSEA.Text = "";
            PanelSometimientoSEA.Visible = false;
            UpdatePanelSometimientoSEA.Update();


            if (solicitudConcesion == null)
            {
                Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
            }


            if (solicitudConcesionService.ActualizaSolicitudSEA(solicitudConcesion.idSolConcesion, Convert.ToInt32(FlujoSEA.SelectedValue), Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario))
            {
                FlujoSEA_change(null, null);

                msSometimientoSEA.Text = "Actualización realizada con exito.";
                PanelSometimientoSEA.Visible = true;
                UpdatePanelSometimientoSEA.Update();

                solicitudConcesion.sometimientoSEA = new ParametroGenerico(Convert.ToInt32(FlujoSEA.SelectedValue));

                Session[ViewState["solicitudSession"].ToString()] = solicitudConcesion;

                //RECARGANDO LA INFORMACION DE LA SOLICITUD, POR SI CAMBIO EL ESTADO
                UpdatePanel UpdatePanelInformacionSolictud = SubPesca.Solicitudes.Registrar.informacionSolicitud.Instance.UpdatePanelInfo;
                SubPesca.Solicitudes.Registrar.informacionSolicitud.Instance.RecargarInformacion();
                UpdatePanelInformacionSolictud.Update();

            }
            else
            {

                msSometimientoSEA.Text = "Ha ocurrido un error al ejecutar la acción solicitada.";
                PanelSometimientoSEA.Visible = true;
                UpdatePanelSometimientoSEA.Update();

            }


        }


    }

}