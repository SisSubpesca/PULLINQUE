using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Entidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using Datos.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;


namespace SubPesca.Solicitudes.ExperimentalesAmerb
{
    public partial class informesResolucionesExperimentalesAmerb : System.Web.UI.Page
    {
        SolicitudConcesionService solicitudConcesionService = new SolicitudConcesionService();
        RequerimientoService requerimientoService = new RequerimientoService();
        DespliegueSeccionService despliegueSeccionService = new DespliegueSeccionService();

        Funciones funciones = new Funciones();

        protected void setearModulo()
        {
            if (funciones.retornaModulo().Equals("Registrar"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLCONCESION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLCONCESION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLCONCESION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLCONCESION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLCONCESION;
                ViewState["solicitudSession"] = paginas.solicitudConcesionSession;
            }
            else if (funciones.retornaModulo().Equals("Acopio"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_ACOPIO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_ACOPIO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_ACOPIO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_ACOPIO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_CENTRO_DE_ACOPIO;
                ViewState["solicitudSession"] = paginas.solicitudAcopioSession;
            }
            else if (funciones.retornaModulo().Equals("Faenamiento"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_CENTRO_DE_FAENAMIENTO;
                ViewState["solicitudSession"] = paginas.solicitudFaenamientoSession;
            }
            else if (funciones.retornaModulo().Equals("Amerb"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_AMERB;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_AMERB;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_AMERB;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_AMERB;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_AMERB;
                ViewState["solicitudSession"] = paginas.solicitudAmerbSession;
            }
            else if (funciones.retornaModulo().Equals("ExperimentalesAmerb"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesAmerbSession;
            }
            else if (funciones.retornaModulo().Equals("ECMPO"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_ECMPO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_ECMPO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_ECMPO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_ECMPO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_ECMPO;
                ViewState["solicitudSession"] = paginas.solicitudECMPOSession;
            }
            else if (funciones.retornaModulo().Equals("Colector"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_COLECTORES_SEMILLA;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_COLECTORES_SEMILLA;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_COLECTORES_SEMILLA;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_COLECTORES_SEMILLA;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_COLECTORES_SEMILLA;
                ViewState["solicitudSession"] = paginas.solicitudColectorSession;


            }
            else
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLMOD;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLMOD;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLMOD;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLMOD;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLMOD;
                ViewState["solicitudSession"] = paginas.solicitudModificacionSession;
            }
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




                //ANTECEDENTES URB
                PanelAntecedentesURB.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_URB, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                if (PanelAntecedentesURB.Visible == false)
                {
                    PanelLinkEvaluacionURB.Visible = false;
                }




                /* SUFICIENCIA FORMAL */
                PanelExamenPreliminar.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.EXAMEN_PRELIMINAR, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                if (PanelExamenPreliminar.Visible == false)
                {
                    PanelLinkSuficienciaFormal.Visible = false;
                }



                //IT UOT. (informe de cartofrafia)
                PanelInforCart.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ITC_UOT, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                PanelunidadDeDependencia.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.UNIDADES_DE_DEPENDENCIA, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                if (PanelInforCart.Visible == false && PanelunidadDeDependencia.Visible == false)
                {
                    PanelInfCartografia.Visible = false;
                }



                //Inspección a Terreno
                PanelInspeccionTerreno.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INSPECCION_TERRENO, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                PanelInspeccionTerrenoCoordenadas.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.COORDENADAS_GEOGRAFICAS_INSPECCION_TERRENO, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                if (PanelInspeccionTerreno.Visible == false && PanelInspeccionTerrenoCoordenadas.Visible == false)
                {
                    PanelLinkInspeccionTerreno.Visible = false;
                }

                //Banco Natural
                PanelLinkBancoNatural.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.BANCO_NATURAL, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);

                //Difusión Banco Natural
                PanelLinkDifusionBancoNatural.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DIFUSION_BANCO_NATURAL, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);

                //Difrol
                PanelLinkDifrol.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DIFROL, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);

                //SI ES COMUNA FRONTERIZA
                if (PanelLinkDifrol.Visible)
                {

                    PanelLinkDifrol.Visible = solicitudConcesion.comunaFronteriza;
                    PanelDifrol.Visible = solicitudConcesion.comunaFronteriza;
                    PanelDifrolObservacion.Visible = solicitudConcesion.comunaFronteriza;
                }



                //Informe Ambiental
                PanelLinkInformeSEA.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.APLICA_SEA, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);


                //Antecedentes Complementarios
                PanelAntecedentesComplementarios.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_COMPLEMENTARIOS, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                PanelCertificadoOperacion.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.CERTIFICADO_REGISTRO_OPERACION, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                if (PanelAntecedentesComplementarios.Visible == false && PanelCertificadoOperacion.Visible == false)
                {
                    PanelLinkAntecedentesComplementarios.Visible = false;
                }

                //Planos
                PanelPlanos.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_PLANOS, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                PanelPlanosInformeTecnicoUOT.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INFORME_TECNICO_UOT, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                if (PanelPlanos.Visible == false && PanelPlanosInformeTecnicoUOT.Visible == false)
                {
                    PanelLinkPlanos.Visible = false;
                }

                //Informe DAC
                PanelInformeDAC.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INFORME_DAC, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                PanelInformeDACDevolucionJuridica.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DEVOLUCION_JURIDICA, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                if (PanelInformeDAC.Visible == false && PanelInformeDACDevolucionJuridica.Visible == false)
                {
                    PanelLinkInformeDAC.Visible = false;
                }

                //Resolucion SSP
                PanelResolucionSSP.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.RESOLUCION_SSP, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                PanelResolucionSSPDevolucionSSFFAA.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.DEVOLUCION_SSFFAA, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                if (PanelResolucionSSP.Visible == false && PanelResolucionSSPDevolucionSSFFAA.Visible == false)
                {
                    PanelLinkResolucionSSP.Visible = false;
                }


                //Resolución SSFFAA
                PanelLinkResolucionSSFFAA.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.RESOLUCION_SSFFAA, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);


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


            if (Convert.ToInt32(FlujoSEA.SelectedValue) == rbTipo.SOMETIMIENTO_SEA_SOMETE)
            {

                PanelInformeSEACartaTitular.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_AMBIENTALES, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB); ;
                PanelInformeSEARCA.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.RESOLUCION_CALIFICACION_AMBIENTAL, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB); ;
                PanelInformeSEAConsultaSEA.Visible = false;
                PanelInformeSEACartaAmbiental.Visible = false;
                PanelInformeSEAInformeUnidadAmbiental.Visible = false;
            }
            else if (Convert.ToInt32(FlujoSEA.SelectedValue) == rbTipo.SOMETIMIENTO_SEA_NO_SOMETE)
            {
                PanelInformeSEACartaTitular.Visible = false;
                PanelInformeSEARCA.Visible = false;
                PanelInformeSEAConsultaSEA.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.NOTIFICACION_SMA, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB); ;
                PanelInformeSEACartaAmbiental.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_AMBIENTALES_MO, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB); ;
                PanelInformeSEAInformeUnidadAmbiental.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.INFORME_AMBIENTALES_MO, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);

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