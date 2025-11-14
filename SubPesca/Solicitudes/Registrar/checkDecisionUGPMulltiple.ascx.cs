using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Contantes;
using Datos.Utilidades;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.servicios.modificacion;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using LogicaNegocio.cl.subpesca.rb.common;

namespace SubPesca.Solicitudes.Registrar
{

    /*
    * decision sectorialisgta UGP (que camino toma despues del IT UOT) - Amerb
    *  campo en rbDecisionUsuario (estadoAplica)
    **/
    public partial class checkDecisionUGPMulltiple : System.Web.UI.UserControl
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        Funciones funciones = new Funciones();
        DespliegueSeccionService despliegueSeccionService = new DespliegueSeccionService();
        SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();
        SolicitudConcesionService solicitudConcesionService = new SolicitudConcesionService();
        PermisosService permisosService = new PermisosService();
        TipoDA tipoDa = new TipoDA();

        protected void setearModulo()
        {

            ValidacionDocumentacion validacionDocumentacion = new ValidacionDocumentacion();

            if (funciones.retornaModulo().Equals("Registrar") || funciones.retornaModulo().Equals("Concesion"))
            {

                ViewState["solicitudSession"] = paginas.solicitudConcesionSession;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CHECK_DECISION_UGP_MULTIPLE_CONCESION };

                PanelCheck.Visible = false;  //para solicitud de concesion no hay soporte a traves de la base de datos
                UpdatePanelCheck.Update();



            }
            else if (funciones.retornaModulo().Equals("Relocalizacion"))
            {

                ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSession;

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];


                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA)
                {
                    PanelCheck.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.CHECK_DECISION_UGP_MULTIPLE, rbTipo.RELOCALIZACION_CREA);
                    UpdatePanelCheck.Update();
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA)
                {
                    PanelCheck.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.CHECK_DECISION_UGP_MULTIPLE, rbTipo.RELOCALIZACION_FUSIONA);
                    UpdatePanelCheck.Update();
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO)
                {
                    PanelCheck.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.CHECK_DECISION_UGP_MULTIPLE, rbTipo.RELOCALIZACION_SECTOR_CERO);
                    UpdatePanelCheck.Update();
                }

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CHECK_DECISION_UGP_MULTIPLE_RELOCALIZACION };

            }
            else if (funciones.retornaModulo().Equals("RelocalizacionRESA"))
            {

                ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSessionRESA;

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];


                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA_RESA)
                {
                    PanelCheck.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.CHECK_DECISION_UGP_MULTIPLE, rbTipo.RELOCALIZACION_CREA_RESA);
                    UpdatePanelCheck.Update();
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA_RESA)
                {
                    PanelCheck.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.CHECK_DECISION_UGP_MULTIPLE, rbTipo.RELOCALIZACION_FUSIONA_RESA);
                    UpdatePanelCheck.Update();
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO_RESA)
                {
                    PanelCheck.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.CHECK_DECISION_UGP_MULTIPLE, rbTipo.RELOCALIZACION_SECTOR_CERO_RESA);
                    UpdatePanelCheck.Update();
                }

                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CHECK_DECISION_UGP_MULTIPLE_RELOCALIZACION_RESA };


            }
            else if (funciones.retornaModulo().Equals("Acopio"))
            {
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CHECK_DECISION_UGP_MULTIPLE_ACOPIO };
                ViewState["solicitudSession"] = paginas.solicitudAcopioSession;
                PanelCheck.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.CHECK_DECISION_UGP_MULTIPLE, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO);
                UpdatePanelCheck.Update();

            }
            else if (funciones.retornaModulo().Equals("Faenamiento"))
            {
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CHECK_DECISION_UGP_MULTIPLE_FAENAMIENTO };
                ViewState["solicitudSession"] = paginas.solicitudFaenamientoSession;
                PanelCheck.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.CHECK_DECISION_UGP_MULTIPLE, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO);
                UpdatePanelCheck.Update();


            }
            else if (funciones.retornaModulo().Equals("Amerb"))
            {
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CHECK_DECISION_UGP_MULTIPLE_AMERB };
                ViewState["solicitudSession"] = paginas.solicitudAmerbSession;
                PanelCheck.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.CHECK_DECISION_UGP_MULTIPLE, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);
                UpdatePanelCheck.Update();

            }
            else if (funciones.retornaModulo().Equals("ExperimentalesAmerb"))
            {
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CHECK_DECISION_UGP_MULTIPLE_EXPERIMENTALES_AMERB };
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesAmerbSession;
                PanelCheck.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.CHECK_DECISION_UGP_MULTIPLE, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                UpdatePanelCheck.Update();


            }
            else if (funciones.retornaModulo().Equals("ExperimentalesConcesion"))
            {
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CHECK_DECISION_UGP_MULTIPLE_EXPERIMENTALES_CONCESION };
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesConcesionSession;
                PanelCheck.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.CHECK_DECISION_UGP_MULTIPLE, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION);
                UpdatePanelCheck.Update();


            }
            else if (funciones.retornaModulo().Equals("ECMPO"))
            {
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CHECK_DECISION_UGP_MULTIPLE_ECMPO };
                ViewState["solicitudSession"] = paginas.solicitudECMPOSession;
                PanelCheck.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.CHECK_DECISION_UGP_MULTIPLE, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO);
                UpdatePanelCheck.Update();

            }
            else if (funciones.retornaModulo().Equals("Colector"))
            {
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CHECK_DECISION_UGP_MULTIPLE_COLECTOR };
                ViewState["solicitudSession"] = paginas.solicitudColectorSession;
                PanelCheck.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.CHECK_DECISION_UGP_MULTIPLE, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
                UpdatePanelCheck.Update();


            }
            else if (funciones.retornaModulo().Equals("ModificacionAmerb"))
            {
                ViewState["solicitudSession"] = paginas.solicitudModificacionAmerbSession;

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion != null)
                {

                    List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueSeccionService.ListarDespliegueMenuSeccion_UE(rbMenu.ANTECEDENTES_DEL_SECTOR_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB);

                    if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
                    {
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.CHECK_DECISION_UGP_MULTIPLE))
                        {
                            PanelCheck.Visible = true;
                            UpdatePanelCheck.Update();
                        }

                    }

                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CHECK_DECISION_UGP_MULTIPLE_MOD_AMERB };
                }
            }
            else if (funciones.retornaModulo().Equals("ModificacionCentroAcopio"))
            {

                ViewState["solicitudSession"] = paginas.solicitudModificacionCentroAcopioSession;

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion != null)
                {

                    List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueSeccionService.ListarDespliegueMenuSeccion_UE(rbMenu.ANTECEDENTES_DEL_SECTOR_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_CENTRO_DE_ACOPIO);

                    if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
                    {
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.CHECK_DECISION_UGP_MULTIPLE))
                        {
                            PanelCheck.Visible = true;
                            UpdatePanelCheck.Update();
                        }

                    }

                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CHECK_DECISION_UGP_MULTIPLE_MOD_ACOPIO };
                }
            }
            else if (funciones.retornaModulo().Equals("ModificacionCentroFaenamiento"))
            {
                ViewState["solicitudSession"] = paginas.solicitudModificacionCentroFaenamientoSession;
                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion != null)
                {

                    List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueSeccionService.ListarDespliegueMenuSeccion_UE(rbMenu.ANTECEDENTES_DEL_SECTOR_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_CENTRO_DE_FAENAMIENTO);

                    if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
                    {
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.CHECK_DECISION_UGP_MULTIPLE))
                        {
                            PanelCheck.Visible = true;
                            UpdatePanelCheck.Update();
                        }

                    }


                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CHECK_DECISION_UGP_MULTIPLE_MOD_FAENAMIENTO };

                }
            }
            else if (funciones.retornaModulo().Equals("ModificacionECMPO"))
            {

                ViewState["solicitudSession"] = paginas.solicitudModificacionECMPOSession;

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion != null)
                {

                    List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueSeccionService.ListarDespliegueMenuSeccion_UE(rbMenu.ANTECEDENTES_DEL_SECTOR_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_ECMPO);

                    if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
                    {
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.CHECK_DECISION_UGP_MULTIPLE))
                        {
                            PanelCheck.Visible = true;
                            UpdatePanelCheck.Update();
                        }

                    }

                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CHECK_DECISION_UGP_MULTIPLE_MOD_ECMPO };
                }
            }
            else
            {

                ViewState["solicitudSession"] = paginas.solicitudModificacionSession;

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion != null)
                {

                    List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueSeccionService.ListarDespliegueMenuSeccion(rbMenu.ANTECEDENTES_DEL_SECTOR_MODIFICACION, 0);

                    if (despliegueMenuSeccionList != null && despliegueMenuSeccionList.Count > 0)
                    {
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.CHECK_DECISION_UGP_MULTIPLE))
                        {
                            PanelCheck.Visible = true;
                            UpdatePanelCheck.Update();
                        }

                    }

                    ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.CHECK_DECISION_UGP_MULTIPLE_MOD_CONCESION };

                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            // PAGE LOAD
            if (!Page.IsPostBack)
            {

                setearModulo();

               List<ParametroGenerico> resp = tipoDa.ListarTipo("ESTADO_AVANCE_AMERB");

               CheckCombo.Items.Clear();
               CheckCombo.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

               if (resp != null)
               {
                   foreach (ParametroGenerico item in resp)
                   {
                       CheckCombo.Items.Add(new ListItem(item.descripcion, Convert.ToString(item.id)));
                   }
               }

               CheckCombo.DataBind();
                


                SolicitudConcesion solicitud = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];


                SolicitudConcesion checks = solicitudConcesionService.obtenerDecisionUsuario(solicitud.idSolConcesion);

                if (checks.decisionUGPMultiple != null && checks.decisionUGPMultiple.id > 0)
                {
                    CheckCombo.SelectedValue = checks.decisionUGPMultiple.id.ToString();
                }


                //BOTON DE INGRESO O MODIFICACION
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], this.usuario_logeado, solicitud, rbAccion.EDITAR))
                {
                    CheckCombo.Enabled = true;
                    Panel1.Visible = true;
                }
                else
                {
                    CheckCombo.Enabled = false;
                    Panel1.Visible = false;
                }
            }
        }


        protected void Check_Guardar_Click(object sender, EventArgs e)
        {


            ErroresSuperior.Text = "";
            PanelErroresSuperior.Visible = false;
            UpdatePanelErroresSuperior.Update();


            SolicitudConcesion solicitud = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];


            if (solicitud != null)
            {

                if (solicitudConcesionService.ActualizaSolicitud_DecisionUGPMultiple(solicitud.idSolConcesion, Convert.ToInt32(CheckCombo.SelectedValue)))
                {

                    ErroresSuperior.Text = "Actualización realizada con exito.";
                    PanelErroresSuperior.Visible = true;
                    UpdatePanelErroresSuperior.Update();

                    solicitud.decisionUGPMultiple = new ParametroGenerico(Convert.ToInt32(CheckCombo.SelectedValue));

                    Session[ViewState["solicitudSession"].ToString()] = solicitud;

                    //RECARGANDO LA INFORMACION DE LA SOLICITUD, POR SI CAMBIO EL ESTADO
                    UpdatePanel UpdatePanelInformacionSolictud = informacionSolicitud.Instance.UpdatePanelInfo;
                    informacionSolicitud.Instance.RecargarInformacion();
                    UpdatePanelInformacionSolictud.Update();

                }
                else
                {

                    ErroresSuperior.Text = "Ha ocurrido un error al ejecutar la acción solicitada.";
                    PanelErroresSuperior.Visible = true;
                    UpdatePanelErroresSuperior.Update();

                }


            }
            else
            {

                ErroresSuperior.Text = "Ha ocurrido un error al ejecutar la acción solicitada.";
                PanelErroresSuperior.Visible = true;
                UpdatePanelErroresSuperior.Update();

            }

        }



    }
}