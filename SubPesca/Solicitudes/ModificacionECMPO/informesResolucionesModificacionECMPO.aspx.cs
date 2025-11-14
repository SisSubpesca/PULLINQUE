using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Entidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.modificacion;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace SubPesca.Solicitudes.ModificacionECMPO
{
    public partial class informesResolucionesModificacionECMPO : System.Web.UI.Page
    {
        SolicitudModificacionService solicitudConcesionService = new SolicitudModificacionService();
        RequerimientoService requerimientoService = new RequerimientoService();
        SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();

        DespliegueMenuSeccionDA despliegueMenuSeccionDA = new DespliegueMenuSeccionDA();


        protected void setearModulo()
        {
            ViewState["solicitudSession"] = paginas.solicitudModificacionECMPOSession;
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
                    Response.Redirect("~/Solicitudes/ModificacionECMPO/administrarSolicitudModificacionECMPO.aspx");
                }



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



                /* Se consulta cuales son las pestañas que deben ser desplegadas de acuerdo al tipo de modificación seleccionada por el usuario */

                /*IT U.O.T */
                List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.ITC_OUT_MODIFICACION, 0);
                if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
                {
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.ITC_UOT))
                    {
                        PanelInforCart.Visible = true;
                    }

                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.UNIDADES_DE_DEPENDENCIA))
                    {
                        PanelunidadDeDependencia.Visible = true;
                    }

                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_ITC_UOT))
                    {

                        PanelInforCartObservacion.Visible = true;
                    }

                    if (PanelInforCart.Visible || PanelunidadDeDependencia.Visible || PanelInforCartObservacion.Visible)
                    {
                        PanelInfCartografia.Visible = true;
                    }
                }

                /* Inspección en Terreno */
                despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.INSPECCION_EN_TERRENO_MODIFICACION, 0);
                if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
                {
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.INSPECCION_TERRENO))
                    {
                        PanelInspeccionTerreno.Visible = true;
                    }

                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.COORDENADAS_GEOGRAFICAS_INSPECCION_TERRENO))
                    {
                        PanelInspeccionTerrenoCoordenadas.Visible = true;
                    }

                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_INSPECCION_TERRENO))
                    {
                        PanelInspeccionTerrenoObservacion.Visible = true;
                    }

                    if (PanelInspeccionTerreno.Visible || PanelInspeccionTerrenoCoordenadas.Visible || PanelInspeccionTerrenoObservacion.Visible)
                    {
                        PanelInspTerreno.Visible = true;
                    }
                }

                /* Banco Natural */
                despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.BANCO_NATURAL_MODIFICACION, 0);
                if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
                {
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.BANCO_NATURAL))
                    {
                        PanelBancoNatural.Visible = true;
                    }

                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_BCO_NATURAL))
                    {
                        PanelBancoNaturalObservacion.Visible = true;
                    }

                    if (PanelBancoNatural.Visible || PanelBancoNaturalObservacion.Visible)
                    {
                        PanelBancoNat.Visible = true;
                    }
                }

                /* Difusión de Banco Natural */
                despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.DIFUSION_BANCO_NATURAL_MODIFICACION, 0);
                if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
                {
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.DIFUSION_BANCO_NATURAL))
                    {
                        PanelDifusionBancoNatural.Visible = true;
                    }
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_DIFUSION_BANCO_NATURAL))
                    {
                        PanelDifusionBancoNaturalObservacion.Visible = true;
                    }

                    if (PanelDifusionBancoNatural.Visible || PanelDifusionBancoNaturalObservacion.Visible)
                    {
                        PanelDifBancoNat.Visible = true;
                    }
                }

                /* DIFROL */
                despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.DIFROL_MODIFICACION, 0);
                if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
                {
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.DIFROL))
                    {
                        PanelDifrol.Visible = true;
                        PanelDifrol.Visible = solicitudConcesion.comunaFronteriza;

                    }
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_DIFROL))
                    {
                        PanelDifrolObservacion.Visible = true;
                        PanelDifrolObservacion.Visible = solicitudConcesion.comunaFronteriza;
                    }

                    if (solicitudModificacionService.esComunaFronteriza(solicitudConcesion.idSolConcesion) && (PanelDifrol.Visible || PanelDifrolObservacion.Visible))
                    {
                        TabDifrol.Visible = true;
                        TabDifrol.Visible = solicitudConcesion.comunaFronteriza;
                    }
                }

                /* Informe Ambiental */
                despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.INFORME_AMBIENTAL_MODIFICACION, 0);
                if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
                {
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.APLICA_SEA))
                    {
                        PanelInformeSEA.Visible = true;
                    }
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_INFORME_AMBIENTAL))
                    {
                        PanelInformeSEAObservacion.Visible = true;
                    }
                    if (PanelInformeSEA.Visible || PanelInformeSEAObservacion.Visible)
                    {
                        PanelInfAmbiental.Visible = true;
                    }
                }

                /* Antecedentes Complementarios */
                despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.ANTECEDENTES_COMPLEMENTARIOS_MODIFICACION, 0);
                if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
                {
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.ANTECEDENTES_COMPLEMENTARIOS))
                    {
                        PanelAntecedentesComplementarios.Visible = true;
                    }
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.CERTIFICADO_REGISTRO_OPERACION))
                    {
                        PanelCertificadoOperacion.Visible = true;
                    }
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_ANTECEDENTES_COMPLEMENTARIOS))
                    {
                        PanelAntecedentesComplementariosObservacion.Visible = true;
                    }
                    if (PanelAntecedentesComplementarios.Visible || PanelAntecedentesComplementariosObservacion.Visible)
                    {
                        PanelAntCompl.Visible = true;

                    }
                }

                /* Planos */
                despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.PLANOS_MODIFICACION, 0);
                if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
                {
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.ANTECEDENTES_PLANOS))
                    {
                        PanelPlanos.Visible = true;
                    }
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.INFORME_TECNICO_UOT))
                    {
                        PanelPlanosInformeTecnicoUOT.Visible = true;
                    }
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_PLANOS))
                    {
                        PanelPlanosObservacion.Visible = true;
                    }

                    if (PanelPlanos.Visible || PanelPlanosInformeTecnicoUOT.Visible || PanelPlanosObservacion.Visible)
                    {
                        TabPlanos.Visible = true;
                    }
                }

                /* Informe DAC */
                despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.INFORME_DAC_MODIFICACION, 0);
                if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
                {
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.INFORME_DAC))
                    {
                        PanelInformeDAC.Visible = true;
                    }
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.DEVOLUCION_JURIDICA))
                    {
                        PanelInformeDACDevolucionJuridica.Visible = true;
                    }
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_INFORME_DAC))
                    {
                        PanelInformeDACObservacion.Visible = true;
                    }

                    if (PanelInformeDAC.Visible || PanelInformeDACDevolucionJuridica.Visible || PanelInformeDACObservacion.Visible)
                    {
                        TabInformeDAC.Visible = true;
                    }
                }

                /* Resolución SSP */
                despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.RESOLUCION_SSP_MODIFICACION, 0);
                if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
                {
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.RESOLUCION_SSP))
                    {
                        PanelResolucionSSP.Visible = true;
                    }
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.DEVOLUCION_SSFFAA))
                    {
                        PanelResolucionSSPDevolucionSSFFAA.Visible = true;
                    }
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_RESOLUCION_SSP))
                    {
                        PanelResolucionSSPObservacion.Visible = true;
                    }
                    if (PanelResolucionSSP.Visible || PanelResolucionSSPDevolucionSSFFAA.Visible || PanelResolucionSSPObservacion.Visible)
                    {
                        PanelResolSSP.Visible = true;
                    }
                }

                /* Resolución SSFFAA */
                despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.RESOLUCION_SSFFAA_MODIFICACION, 0);
                if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
                {
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.RESOLUCION_SSFFAA))
                    {
                        PanelResolucionSSFFAA.Visible = true;
                    }
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_RESOLUCION_SSFFAA))
                    {
                        PanelResolucionSSFFAAObservacion.Visible = true;
                    }
                    if (PanelResolucionSSFFAA.Visible || PanelResolucionSSFFAAObservacion.Visible)
                    {
                        PanelResolSSFFAA.Visible = true;
                    }
                }
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
            SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

            //List<AntecedenteModConcesion> antecedenteModConcesionList = solicitudModificacionService.obtieneDespliegueFuncionalidades(solicitudConcesion);
            DespliegueMenuSeccionDA despliegueMenuSeccionDA = new DespliegueMenuSeccionDA();

            List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.INFORME_AMBIENTAL_MODIFICACION, 0);

            if (Convert.ToInt32(FlujoSEA.SelectedValue) == rbTipo.SOMETIMIENTO_SEA_SOMETE)
            {

                if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.ANTECEDENTES_AMBIENTALES))
                {
                    PanelInformeSEACartaTitular.Visible = true;
                }

                if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.RESOLUCION_CALIFICACION_AMBIENTAL))
                {
                    PanelInformeSEARCA.Visible = true;
                }

                PanelInformeSEAConsultaSEA.Visible = false;
                PanelInformeSEACartaAmbiental.Visible = false;
                PanelInformeSEAInformeUnidadAmbiental.Visible = false;
            }
            else if (Convert.ToInt32(FlujoSEA.SelectedValue) == rbTipo.SOMETIMIENTO_SEA_NO_SOMETE)
            {
                PanelInformeSEACartaTitular.Visible = false;
                PanelInformeSEARCA.Visible = false;

                if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.NOTIFICACION_SMA))
                {
                    PanelInformeSEAConsultaSEA.Visible = true;
                }

                if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.ANTECEDENTES_AMBIENTALES_MO))
                {
                    PanelInformeSEACartaAmbiental.Visible = true;
                }

                if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.INFORME_AMBIENTALES_MO))
                {
                    PanelInformeSEAInformeUnidadAmbiental.Visible = true;
                }

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
                Response.Redirect("~/Solicitudes/ModificacionECMPO/administrarSolicitudModificacionECMPO.aspx");
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