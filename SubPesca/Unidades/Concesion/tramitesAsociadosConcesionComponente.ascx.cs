using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using Datos.Utilidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;

namespace SubPesca.Unidades.Concesion
{
    public partial class tramitesAsociadosConcesionComponente : System.Web.UI.UserControl
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        SolicitudDA solicitudDA = new SolicitudDA();
        UnidadEspacialDA unidadEspacialDA = new UnidadEspacialDA();
        RelocalizacionService relocalizacionService = new RelocalizacionService();

        Funciones funciones = new Funciones();

        protected void setearModulo()
        {
            if (funciones.retornaModulo().Equals("Concesion"))
            {
                ViewState["solicitudSession"] = paginas.solicitudConcesionSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_CONCESION_DE_ACUICULTURA;
                ViewState["moduloOrigen"] = "Concesion";
                
            }
            else if (funciones.retornaModulo().Equals("Relocalizacion"))
            {
                ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSession;
                
                
            }
            else if (funciones.retornaModulo().Equals("RelocalizacionRESA"))
            {
                ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSessionRESA;
                
            }

            else if (funciones.retornaModulo().Equals("Acopio"))
            {
                ViewState["solicitudSession"] = paginas.solicitudAcopioSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_CENTRO_ACOPIO;
                ViewState["moduloOrigen"] = "Acopio";
                
            }
            else if (funciones.retornaModulo().Equals("Faenamiento"))
            {
                ViewState["solicitudSession"] = paginas.solicitudFaenamientoSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_CENTRO_DE_FAENAMIENTO;
                ViewState["moduloOrigen"] = "Faenamiento";
                
            }

            else if (funciones.retornaModulo().Equals("Amerb"))
            {
                ViewState["solicitudSession"] = paginas.solicitudAmerbSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_CENTRO_EN_AMERB;
                ViewState["moduloOrigen"] = "Amerb";
                
            }

            else if (funciones.retornaModulo().Equals("Colector"))
            {
                ViewState["solicitudSession"] = paginas.solicitudColectorSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_COLECTOR_DE_SEMILLAS;
                ViewState["moduloOrigen"] = "Colector";
                
            }
            else if (funciones.retornaModulo().Equals("ECMPO"))
            {
                ViewState["solicitudSession"] = paginas.solicitudECMPOSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_ECMPO;
                ViewState["moduloOrigen"] = "ECMPO";
                
            }
            else if (funciones.retornaModulo().Equals("ExperimentalesAmerb"))
            {
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesAmerbSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_EXPERIMENTALES_AMERB;
                ViewState["moduloOrigen"] = "ExperimentalesAmerb";
                
            }
            else if (funciones.retornaModulo().Equals("ExperimentalesConcesion"))
            {
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesConcesionSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_EXPERIMENTALES_CONCESION;
                ViewState["moduloOrigen"] = "ExperimentalesConcesion";
                
            }
            else if (funciones.retornaModulo().Equals("ModificacionAmerb"))
            {
                ViewState["solicitudSession"] = paginas.solicitudModificacionAmerbSession;
                
            }
            else if (funciones.retornaModulo().Equals("ModificacionCentroAcopio"))
            {
                ViewState["solicitudSession"] = paginas.solicitudModificacionCentroAcopioSession;
                
            }
            else if (funciones.retornaModulo().Equals("ModificacionCentroFaenamiento"))
            {
                ViewState["solicitudSession"] = paginas.solicitudModificacionCentroFaenamientoSession;
                
            }
            else if (funciones.retornaModulo().Equals("ModificacionECMPO"))
            {
                ViewState["solicitudSession"] = paginas.solicitudModificacionECMPOSession;
                
            }
            else {
                ViewState["solicitudSession"] = paginas.solicitudModificacionSession;
                
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                setearModulo();

                // Inicializamos el formulario
                Initialize_Form();
            }
        }

        private void Initialize_Form()
        {
            SolicitudConcesion concesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

            if (concesion == null || usuario_logeado == null)
            {
                Response.Redirect(ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"].ToString());
            }

            if (concesion != null && concesion.idSolConcesion > 0)
            {
                
                NombreUnidadEspacial.Text = funciones.retornaModulo();

                int pagina = 0;

                List<SolicitudConcesion> listaSolicitudesEnTramite = null;
                List<SolicitudConcesion> listaSolicitudesAprobadas = null;
                List<SolicitudConcesion> listaSolicitudesRechazadas = null;
                int pestaniaEnTramite = 3;
                int pestaniaAprobada = 1;
                int pestaniaRechazada = 2;

                UnidadEspacial unidadespacial = unidadEspacialDA.ObtieneUnidadEspacial(concesion.idSolConcesion, 0);
                
                concesion.unidadEspacial = new UnidadEspacial();
                concesion.unidadEspacial.idUnidadEspacial = unidadespacial.idUnidadEspacial;
                concesion.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                concesion.unidadEspacial.centrosDeCultivo.codigoCentro = unidadespacial.centrosDeCultivo.codigoCentro;

                if (ViewState["moduloOrigen"].ToString().Equals("Acopio"))
                {
                    listaSolicitudesEnTramite = solicitudDA.ListarTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, 3, "Acopio", concesion.unidadEspacial.idUnidadEspacial);
                    listaSolicitudesAprobadas = solicitudDA.ListarTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, 1, "Acopio", concesion.unidadEspacial.idUnidadEspacial);
                    listaSolicitudesRechazadas = solicitudDA.ListarTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, 2, "Acopio", concesion.unidadEspacial.idUnidadEspacial);

                    
                }else if (ViewState["moduloOrigen"].ToString().Equals("Amerb"))
                {
                    listaSolicitudesEnTramite = solicitudDA.ListarTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, 3, "Amerb", concesion.unidadEspacial.idUnidadEspacial);
                    listaSolicitudesAprobadas = solicitudDA.ListarTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, 1, "Amerb", concesion.unidadEspacial.idUnidadEspacial);
                    listaSolicitudesRechazadas = solicitudDA.ListarTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, 2, "Amerb", concesion.unidadEspacial.idUnidadEspacial);

                }
                else if (ViewState["moduloOrigen"].ToString().Equals("Colector")) {
                    listaSolicitudesEnTramite = solicitudDA.ListarTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, 3, "Colector", concesion.unidadEspacial.idUnidadEspacial);
                    listaSolicitudesAprobadas = solicitudDA.ListarTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, 1, "Colector", concesion.unidadEspacial.idUnidadEspacial);
                    listaSolicitudesRechazadas = solicitudDA.ListarTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, 2, "Colector", concesion.unidadEspacial.idUnidadEspacial);
                }
                else if (ViewState["moduloOrigen"].ToString().Equals("Concesion")) {

                    listaSolicitudesEnTramite = solicitudDA.ListarConcesionTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, pestaniaEnTramite);
                    listaSolicitudesAprobadas = solicitudDA.ListarConcesionTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, pestaniaAprobada);
                    listaSolicitudesRechazadas = solicitudDA.ListarConcesionTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, pestaniaRechazada);
                }
                else if (ViewState["moduloOrigen"].ToString().Equals("ECMPO")) {
                    listaSolicitudesEnTramite = solicitudDA.ListarTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, 3, "Ecmpo", concesion.unidadEspacial.idUnidadEspacial);
                    listaSolicitudesAprobadas = solicitudDA.ListarTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, 1, "Ecmpo", concesion.unidadEspacial.idUnidadEspacial);
                    listaSolicitudesRechazadas = solicitudDA.ListarTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, 2, "Ecmpo", concesion.unidadEspacial.idUnidadEspacial);
                }
                else if (ViewState["moduloOrigen"].ToString().Equals("ExperimentalesAmerb"))
                {
                    listaSolicitudesAprobadas = solicitudDA.ListarTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, 1, "ExperimentalesAmerb", concesion.unidadEspacial.idUnidadEspacial);
                }
                else if (ViewState["moduloOrigen"].ToString().Equals("ExperimentalesConcesion"))
                {
                    listaSolicitudesAprobadas = solicitudDA.ListarTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, 1, "ExperimentalesConcesion", concesion.unidadEspacial.idUnidadEspacial);
                }
                else if (ViewState["moduloOrigen"].ToString().Equals("Faenamiento"))
                {
                    listaSolicitudesEnTramite = solicitudDA.ListarTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, 3, "Faenamiento", concesion.unidadEspacial.idUnidadEspacial);
                    listaSolicitudesAprobadas = solicitudDA.ListarTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, 1, "Faenamiento", concesion.unidadEspacial.idUnidadEspacial);
                    listaSolicitudesRechazadas = solicitudDA.ListarTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, 2, "Faenamiento", concesion.unidadEspacial.idUnidadEspacial);
                }


                if ((listaSolicitudesEnTramite == null || listaSolicitudesEnTramite.Count <= 0) && ((listaSolicitudesAprobadas == null || listaSolicitudesAprobadas.Count <= 0) &&
                    (listaSolicitudesRechazadas == null || listaSolicitudesRechazadas.Count <= 0)))
                {
                    msgGrilla_Sol.Text = "No existen trámites asociados a la unidad espacial.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    PanelSolicitudesMsg.Visible = true;

                    listaSolicitudesEnTramite = new List<SolicitudConcesion>();
                    GridEnTramite.DataSource = listaSolicitudesEnTramite;
                    GridEnTramite.DataBind();
                }
                else
                {

                    PanelSolicitudesMsg.Visible = false;
                    
                    /* Grilla de Solicitudes En Trámite */
                    GridEnTramite.PageIndex = pagina;
                    GridEnTramite.DataSource = listaSolicitudesEnTramite;
                    GridEnTramite.DataBind();

                    /* Grilla de Solicitudes Aprobadas */
                    GridAprobada.PageIndex = pagina;
                    GridAprobada.DataSource = listaSolicitudesAprobadas;
                    GridAprobada.DataBind();

                    /* Grilla de Solicitudes En Trámite */
                    GridRechazada.PageIndex = pagina;
                    GridRechazada.DataSource = listaSolicitudesRechazadas;
                    GridRechazada.DataBind();
                }
            }
        }

        protected void cambiaPestania_Click(object sender, EventArgs e)
        {
            LinkButton boton = (LinkButton)sender;

            switch (boton.ID)
            {

                case "lnk_EnTramite":

                    lnk_EnTramite.CssClass = "tab1_selected";
                    UpdatePanelPestanaEnTramite.Update();

                    lnk_Aprobada.CssClass = "tab2";
                    UpdatePanelPestanaAprobada.Update();

                    lnk_Rechazada.CssClass = "tab3";
                    UpdatePanelPestanaRechazada.Update();

                    PanelEnTramite.Visible = true;
                    UpdatePanelEnTramite.Update();

                    PanelAprobada.Visible = false;
                    UpdatePanelAprobada.Update();

                    PanelRechazada.Visible = false;
                    UpdatePanelRechazada.Update();

                    break;


                case "lnk_Aprobada":

                    lnk_EnTramite.CssClass = "tab1";
                    UpdatePanelPestanaEnTramite.Update();

                    lnk_Aprobada.CssClass = "tab2_selected";
                    UpdatePanelPestanaAprobada.Update();

                    lnk_Rechazada.CssClass = "tab3";
                    UpdatePanelPestanaRechazada.Update();

                    PanelEnTramite.Visible = false;
                    UpdatePanelEnTramite.Update();

                    PanelAprobada.Visible = true;
                    UpdatePanelAprobada.Update();

                    PanelRechazada.Visible = false;
                    UpdatePanelRechazada.Update();


                    break;

                case "lnk_Rechazada":

                    lnk_EnTramite.CssClass = "tab1";
                    UpdatePanelPestanaEnTramite.Update();

                    lnk_Aprobada.CssClass = "tab2";
                    UpdatePanelPestanaAprobada.Update();

                    lnk_Rechazada.CssClass = "tab3_selected";
                    UpdatePanelPestanaRechazada.Update();

                    PanelEnTramite.Visible = false;
                    UpdatePanelEnTramite.Update();

                    PanelAprobada.Visible = false;
                    UpdatePanelAprobada.Update();

                    PanelRechazada.Visible = true;
                    UpdatePanelRechazada.Update();

                    break;
            };

        }

        protected void GridEnTramite_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "VerTramite":

                    int idSolConces = Convert.ToInt32(e.CommandArgument);

                    SolicitudConcesion solicitudVer = new SolicitudConcesion();
                    solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idSolConces, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                   
                    if (solicitudVer != null && solicitudVer.tipoTramite != null)
                    {
                        int idTipoTramite = solicitudVer.tipoTramite.id;
                        string moduloDestino = getModuloDestino(idTipoTramite);

                        if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_MODIFICACION || solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB ||
                            solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_ACOPIO || solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_FAENAMIENTO ||
                            solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_ECMPO)
                        {
                            solicitudVer = solicitudDA.ObtieneSolicitudConcesionMod(idSolConces, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            solicitudVer.tipoTramite = new ParametroGenerico();
                            solicitudVer.tipoTramite.id = idTipoTramite;

                            SolicitudConcesion concesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                            solicitudVer.idConcesion = concesion.idSolConcesion;

                            solicitudVer.tieneAsignadaSolicitud = false;

                            Session[getSolicitudSessionDestino(solicitudVer.tipoTramite.id)] = (SolicitudConcesion)solicitudVer;

                            /* La redirección depende del tipo de tramite al que corresponda */
                            if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_MODIFICACION)
                            {
                                Response.Redirect("~/Solicitudes/" + moduloDestino + "/identificacionTitularConcesion.aspx");
                            }
                            else if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB)
                            {
                                Response.Redirect("~/Solicitudes/" + moduloDestino + "/identificacionTitularModificacionAmerb.aspx");
                            }
                            else if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_ACOPIO)
                            {
                                Response.Redirect("~/Solicitudes/" + moduloDestino + "/identificacionTitularModificacionCentroAcopio.aspx");
                            }
                            else if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_FAENAMIENTO)
                            {
                                Response.Redirect("~/Solicitudes/" + moduloDestino + "/identificacionTitularModificacionCentroFaenamiento.aspx");
                            }
                            else if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_ECMPO)
                            {
                                Response.Redirect("~/Solicitudes/" + moduloDestino + "/identificacionTitularModificacionECMPO.aspx");
                            }

                        }
                        else if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION || solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION_RESA)
                        {
                            solicitudVer.sectorRelocalizacion = relocalizacionService.obtenerDetalleSector_Solicitud(solicitudVer.idSolConcesion);
                            solicitudVer.tipoTramite = new ParametroGenerico();
                            solicitudVer.tipoTramite.id = idTipoTramite;

                            SolicitudConcesion concesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                            solicitudVer.idConcesion = concesion.idSolConcesion;

                            solicitudVer.tieneAsignadaSolicitud = false;

                            Session[getSolicitudSessionDestino(solicitudVer.tipoTramite.id)] = (SolicitudConcesion)solicitudVer;

                            /* La redirección depende del tipo de tramite al que corresponda */
                            if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION)
                            {
                                Response.Redirect("~/Solicitudes/" + moduloDestino + "/identificacionSolicitanteRelocalizacion.aspx");
                            }
                            else
                            {
                                Response.Redirect("~/Solicitudes/" + moduloDestino + "/identificacionSolicitanteRelocalizacionRESA.aspx");
                            }
                        }
                        else
                        {
                            solicitudVer.tipoTramite = new ParametroGenerico();
                            solicitudVer.tipoTramite.id = idTipoTramite;

                            SolicitudConcesion concesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                            solicitudVer.idConcesion = concesion.idSolConcesion;

                            solicitudVer.tieneAsignadaSolicitud = false;

                            Session[getSolicitudSessionDestino(solicitudVer.tipoTramite.id)] = (SolicitudConcesion)solicitudVer;

                            /* La redirección depende del tipo de tramite al que corresponda */
                            if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_CONCESION)
                            {
                                Response.Redirect("~/Solicitudes/Registrar/identificacionSolicitante.aspx");

                            }
                            else if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB)
                            {
                                Response.Redirect("~/Solicitudes/Amerb/identificacionSolicitanteAmerb.aspx");

                            }
                            else if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_CENTRO_DE_ACOPIO)
                            {
                                Response.Redirect("~/Solicitudes/Acopio/identificacionSolicitanteAcopio.aspx");

                            }
                            else if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_CENTRO_DE_FAENAMIENTO)
                            {
                                Response.Redirect("~/Solicitudes/Faenamiento/identificacionSolicitanteFaenamiento.aspx");

                            }
                            else if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_COLECTORES_DE_SEMILLA)
                            {
                                Response.Redirect("~/Solicitudes/Colector/identificacionSolicitanteColector.aspx");

                            }
                            else if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_ECMPO)
                            {

                                Response.Redirect("~/Solicitudes/ECMPO/identificacionTitularECMPO.aspx");
                            }
                            else if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_EXPERIMENTALES_AMERB)
                            {

                                Response.Redirect("~/Solicitudes/ExperimentalesAmerb/identificacionTitularExperimentalesAmerb.aspx");
                            }
                            else if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_EXPERIMENTALES_CONCESION)
                            {

                                Response.Redirect("~/Solicitudes/ExperimentalesConcesion/identificacionTitularExperimentalesConcesion.aspx");
                            }

                        }
                    }

                    break;
            };
        }


        private string getModuloDestino(int idTipoModificacion) {

            if (idTipoModificacion == rbTipo.TIPO_TRAMITE_MODIFICACION)
            {
                return "Modificacion";
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB)
            {
                return "ModificacionAmerb";
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_ACOPIO)
            {
                return "ModificacionCentroAcopio";
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_FAENAMIENTO)
            {
                return "ModificacionCentroFaenamiento";
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_ECMPO)
            {
                return "ModificacionECMPO";
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION)
            {
                return "Relocalizacion";
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION_RESA)
            {
                return "RelocalizacionRESA";
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA)
            {
                return "Registrar";
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB)
            {
                return "Amerb";
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO)
            {
                return "Acopio";
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO)
            {
                return "Faenamiento";
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA)
            {
                return "Colector";
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO)
            {
                return "ECMPO";
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB)
            {
                return "ExperimentalesAmerb";
            }
            else
            {
                return "ExperimentalesConcesion";
            }
        }

        private string getSolicitudSessionDestino(int idTipoModificacion) {

            if (idTipoModificacion == rbTipo.TIPO_TRAMITE_MODIFICACION)
            {
                return paginas.solicitudModificacionSession;
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB)
            {
                return paginas.solicitudModificacionAmerbSession;
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_ACOPIO)
            {
                return paginas.solicitudModificacionCentroAcopioSession;
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_FAENAMIENTO)
            {
                return paginas.solicitudModificacionCentroFaenamientoSession;
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_ECMPO)
            {
                return paginas.solicitudModificacionECMPOSession;
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION)
            {
                return paginas.solicitudRelocalizacionSession;
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION_RESA)
            {
                return paginas.solicitudRelocalizacionSessionRESA;
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA)
            {
                return paginas.solicitudConcesionSession;
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB)
            {
                return paginas.solicitudAmerbSession;
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO)
            {
                return paginas.solicitudAcopioSession;
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO)
            {
                return paginas.solicitudFaenamientoSession;
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA)
            {
                return paginas.solicitudColectorSession;
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO)
            {
                return paginas.solicitudECMPOSession;
            }
            else if (idTipoModificacion == rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB)
            {
                return paginas.solicitudExperimentalesAmerbSession;
            }
            else 
            {
                return paginas.solicitudExperimentalesConcesionSession;
            }
        
        }

        protected void GridEnTramite_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


            };   
        }

        protected void GridAprobada_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "VerTramite":

                    int idSolConces = Convert.ToInt32(e.CommandArgument);

                    SolicitudConcesion solicitudVer = new SolicitudConcesion();
                    solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idSolConces, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                   
                    if (solicitudVer != null && solicitudVer.tipoTramite != null)
                    {
                        int idTipoTramite = solicitudVer.tipoTramite.id;
                        string moduloDestino = getModuloDestino(idTipoTramite);

                        if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_MODIFICACION || solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB ||
                            solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_ACOPIO || solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_FAENAMIENTO ||
                            solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_ECMPO)
                        {
                            solicitudVer = solicitudDA.ObtieneSolicitudConcesionMod(idSolConces, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            solicitudVer.tipoTramite = new ParametroGenerico();
                            solicitudVer.tipoTramite.id = idTipoTramite;

                            SolicitudConcesion concesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                            solicitudVer.idConcesion = concesion.idSolConcesion;

                            solicitudVer.tieneAsignadaSolicitud = false;

                            Session[getSolicitudSessionDestino(solicitudVer.tipoTramite.id)] = (SolicitudConcesion)solicitudVer;

                            /* La redirección depende del tipo de tramite al que corresponda */
                            if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_MODIFICACION)
                            {
                                Response.Redirect("~/Solicitudes/" + moduloDestino + "/identificacionTitularConcesion.aspx");
                            }
                            else if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB)
                            {
                                Response.Redirect("~/Solicitudes/" + moduloDestino + "/identificacionTitularModificacionAmerb.aspx");
                            }
                            else if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_ACOPIO)
                            {
                                Response.Redirect("~/Solicitudes/" + moduloDestino + "/identificacionTitularModificacionCentroAcopio.aspx");
                            }
                            else if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_FAENAMIENTO)
                            {
                                Response.Redirect("~/Solicitudes/" + moduloDestino + "/identificacionTitularModificacionCentroFaenamiento.aspx");
                            }
                            else if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_ECMPO)
                            {
                                Response.Redirect("~/Solicitudes/" + moduloDestino + "/identificacionTitularModificacionECMPO.aspx");
                            }


                        }
                        else if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION || solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION_RESA)
                        {
                            solicitudVer.sectorRelocalizacion = relocalizacionService.obtenerDetalleSector_Solicitud(solicitudVer.idSolConcesion);
                            solicitudVer.tipoTramite = new ParametroGenerico();
                            solicitudVer.tipoTramite.id = idTipoTramite;

                            SolicitudConcesion concesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                            solicitudVer.idConcesion = concesion.idSolConcesion;

                            solicitudVer.tieneAsignadaSolicitud = false;

                            Session[getSolicitudSessionDestino(solicitudVer.tipoTramite.id)] = (SolicitudConcesion)solicitudVer;

                            /* La redirección depende del tipo de tramite al que corresponda */
                            if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION)
                            {
                                Response.Redirect("~/Solicitudes/" + moduloDestino + "/identificacionSolicitanteRelocalizacion.aspx");
                            }
                            else
                            {
                                Response.Redirect("~/Solicitudes/" + moduloDestino + "/identificacionSolicitanteRelocalizacionRESA.aspx");
                            }
                        }
                        else
                        {
                            solicitudVer.tipoTramite = new ParametroGenerico();
                            solicitudVer.tipoTramite.id = idTipoTramite;

                            SolicitudConcesion concesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                            solicitudVer.idConcesion = concesion.idSolConcesion;

                            solicitudVer.tieneAsignadaSolicitud = false;

                            //Session[getSolicitudSessionDestino(solicitudVer.tipoTramite.id)] = (SolicitudConcesion)solicitudVer;
                            Session[ViewState["solicitudSession"].ToString()] = (SolicitudConcesion)solicitudVer;

                            /* La redirección depende del tipo de tramite al que corresponda */
                            if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_CONCESION)
                            {
                                Response.Redirect("~/Solicitudes/Registrar/identificacionSolicitante.aspx");

                            }
                            else if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB)
                            {
                                Response.Redirect("~/Solicitudes/Amerb/identificacionSolicitanteAmerb.aspx");

                            }
                            else if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_CENTRO_DE_ACOPIO)
                            {
                                Response.Redirect("~/Solicitudes/Acopio/identificacionSolicitanteAcopio.aspx");

                            }
                            else if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_CENTRO_DE_FAENAMIENTO)
                            {
                                Response.Redirect("~/Solicitudes/Faenamiento/identificacionSolicitanteFaenamiento.aspx");

                            }
                            else if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_COLECTORES_DE_SEMILLA)
                            {
                                Response.Redirect("~/Solicitudes/Colector/identificacionSolicitanteColector.aspx");

                            }
                            else if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_ECMPO)
                            {

                                Response.Redirect("~/Solicitudes/ECMPO/identificacionTitularECMPO.aspx");
                            }
                            else if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_EXPERIMENTALES_AMERB)
                            {

                                Response.Redirect("~/Solicitudes/ExperimentalesAmerb/identificacionTitularExperimentalesAmerb.aspx");
                            }
                            else if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_EXPERIMENTALES_CONCESION)
                            {

                                Response.Redirect("~/Solicitudes/ExperimentalesConcesion/identificacionTitularExperimentalesConcesion.aspx");
                            }

                        }
                    }

                    break;
            };
        }

        protected void GridAprobada_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


            };   
        }

        protected void GridRechazada_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "VerTramite":

                    int idSolConces = Convert.ToInt32(e.CommandArgument);

                    SolicitudConcesion solicitudVer = new SolicitudConcesion();
                    solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idSolConces, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                   
                    if (solicitudVer != null && solicitudVer.tipoTramite != null)
                    {
                        int idTipoTramite = solicitudVer.tipoTramite.id;
                        string moduloDestino = getModuloDestino(idTipoTramite);

                        if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_MODIFICACION || solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB ||
                            solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_ACOPIO || solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_FAENAMIENTO ||
                            solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_ECMPO)
                        {
                            solicitudVer = solicitudDA.ObtieneSolicitudConcesionMod(idSolConces, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            solicitudVer.tipoTramite = new ParametroGenerico();
                            solicitudVer.tipoTramite.id = idTipoTramite;

                            SolicitudConcesion concesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                            solicitudVer.idConcesion = concesion.idSolConcesion;

                            solicitudVer.tieneAsignadaSolicitud = false;

                            Session[getSolicitudSessionDestino(solicitudVer.tipoTramite.id)] = (SolicitudConcesion)solicitudVer;

                            /* La redirección depende del tipo de tramite al que corresponda */
                            if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_MODIFICACION)
                            {
                                Response.Redirect("~/Solicitudes/" + moduloDestino + "/identificacionTitularConcesion.aspx");
                            }
                            else if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB)
                            {
                                Response.Redirect("~/Solicitudes/" + moduloDestino + "/identificacionTitularModificacionAmerb.aspx");
                            }
                            else if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_ACOPIO)
                            {
                                Response.Redirect("~/Solicitudes/" + moduloDestino + "/identificacionTitularModificacionCentroAcopio.aspx");
                            }
                            else if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_FAENAMIENTO)
                            {
                                Response.Redirect("~/Solicitudes/" + moduloDestino + "/identificacionTitularModificacionCentroFaenamiento.aspx");
                            }
                            else if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_ECMPO)
                            {
                                Response.Redirect("~/Solicitudes/" + moduloDestino + "/identificacionTitularModificacionECMPO.aspx");
                            }

                        }
                        else if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION || solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION_RESA)
                        {
                            solicitudVer.sectorRelocalizacion = relocalizacionService.obtenerDetalleSector_Solicitud(solicitudVer.idSolConcesion);
                            solicitudVer.tipoTramite = new ParametroGenerico();
                            solicitudVer.tipoTramite.id = idTipoTramite;

                            SolicitudConcesion concesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                            solicitudVer.idConcesion = concesion.idSolConcesion;

                            solicitudVer.tieneAsignadaSolicitud = false;

                            Session[getSolicitudSessionDestino(solicitudVer.tipoTramite.id)] = (SolicitudConcesion)solicitudVer;

                            /* La redirección depende del tipo de tramite al que corresponda */
                            if (solicitudVer.tipoTramite.id == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION)
                            {
                                Response.Redirect("~/Solicitudes/" + moduloDestino + "/identificacionSolicitanteRelocalizacion.aspx");
                            }
                            else
                            {
                                Response.Redirect("~/Solicitudes/" + moduloDestino + "/identificacionSolicitanteRelocalizacionRESA.aspx");
                            }
                        }
                        else
                        {
                            solicitudVer.tipoTramite = new ParametroGenerico();
                            solicitudVer.tipoTramite.id = idTipoTramite;

                            SolicitudConcesion concesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                            solicitudVer.idConcesion = concesion.idSolConcesion;

                            solicitudVer.tieneAsignadaSolicitud = false;

                            Session[getSolicitudSessionDestino(solicitudVer.tipoTramite.id)] = (SolicitudConcesion)solicitudVer;

                            /* La redirección depende del tipo de tramite al que corresponda */
                            if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_CONCESION)
                            {
                                Response.Redirect("~/Solicitudes/Registrar/identificacionSolicitante.aspx");

                            }
                            else if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB)
                            {
                                Response.Redirect("~/Solicitudes/Amerb/identificacionSolicitanteAmerb.aspx");

                            }
                            else if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_CENTRO_DE_ACOPIO)
                            {
                                Response.Redirect("~/Solicitudes/Acopio/identificacionSolicitanteAcopio.aspx");

                            }
                            else if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_CENTRO_DE_FAENAMIENTO)
                            {
                                Response.Redirect("~/Solicitudes/Faenamiento/identificacionSolicitanteFaenamiento.aspx");

                            }
                            else if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_COLECTORES_DE_SEMILLA)
                            {
                                Response.Redirect("~/Solicitudes/Colector/identificacionSolicitanteColector.aspx");

                            }
                            else if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_ECMPO)
                            {

                                Response.Redirect("~/Solicitudes/ECMPO/identificacionTitularECMPO.aspx");
                            }
                            else if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_EXPERIMENTALES_AMERB)
                            {

                                Response.Redirect("~/Solicitudes/ExperimentalesAmerb/identificacionTitularExperimentalesAmerb.aspx");
                            }
                            else if (solicitudVer.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_EXPERIMENTALES_CONCESION)
                            {

                                Response.Redirect("~/Solicitudes/ExperimentalesConcesion/identificacionTitularExperimentalesConcesion.aspx");
                            }

                        }
                    }
                    
                    break;
            };
        }

        protected void GridRechazada_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


            };   
        }

        protected void GridEnTramite_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridEnTramite.PageIndex = e.NewPageIndex;
            cargarListaTramites(3);
            GridEnTramite.DataBind();
        }

        protected void GridAprobada_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

            GridAprobada.PageIndex = e.NewPageIndex;
            cargarListaTramites(1);
            GridAprobada.DataBind();
            
        }

        protected void GridRechazada_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

            GridRechazada.PageIndex = e.NewPageIndex;
            cargarListaTramites(2);
            GridRechazada.DataBind();
            
        }

        private void cargarListaTramites(int idPestaña) {

            SolicitudConcesion concesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

            if (concesion != null)
            {
                UnidadEspacial unidadespacial = unidadEspacialDA.ObtieneUnidadEspacial(concesion.idSolConcesion, 0);

                concesion.unidadEspacial = new UnidadEspacial();
                concesion.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                concesion.unidadEspacial.centrosDeCultivo.codigoCentro = unidadespacial.centrosDeCultivo.codigoCentro;

                List<SolicitudConcesion> listaTramites = null;

                if (idPestaña == 3)
                {
                    listaTramites = solicitudDA.ListarConcesionTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, 3);
                    GridEnTramite.DataSource = listaTramites;
                }
                else if (idPestaña == 1)
                {
                    listaTramites = solicitudDA.ListarConcesionTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, 1);
                    GridAprobada.DataSource = listaTramites;
                }
                else
                {
                    listaTramites = solicitudDA.ListarConcesionTramitesSolicitud(concesion.unidadEspacial.centrosDeCultivo.codigoCentro, 2);
                    GridRechazada.DataSource = listaTramites;
                }
                
            }

        }

     
    }
}