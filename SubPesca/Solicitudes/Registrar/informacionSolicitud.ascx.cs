using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Contantes;
using Datos.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;

namespace SubPesca.Solicitudes.Registrar
{
    public partial class informacionSolicitud : System.Web.UI.UserControl
    {

        public static informacionSolicitud Instance { get; set; }
        SolicitudDA solicitudDA = new SolicitudDA();
        Funciones funciones = new Funciones();
        //InformacionAdicionalSolicitudesService informacionAdicionalSolicitudesService = new InformacionAdicionalSolicitudesService();
        InformacionSolicitudService informacionSolicitudService = new InformacionSolicitudService();
        //SolicitudConcesionService solicitudConcesionService = new SolicitudConcesionService();

        public informacionSolicitud()
        {
            Instance = this;
        }

        public UpdatePanel UpdatePanelInfo
        {
            get { return UpdatePanelInformacionSol; }
            set { UpdatePanelInformacionSol = value; }
        }


        public void RecargarInformacion()
        {
            setearModulo();
            // Inicializamos el formulario
            Initialize_Form();
        }

      



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                setearModulo();

                if (!esUnidadEspacial(ViewState["nombreTipoModulo"].ToString()))
                {
                    // Inicializamos el formulario
                    Initialize_Form();
                    PanelInformacionSolicitud.Visible = true;
                }
            }
        }



        protected void setearModulo()
        {

            ViewState["nombreTipoModulo"] = funciones.retornaTipoModulo();

            if (funciones.retornaModulo().Equals("Registrar") || funciones.retornaModulo().Equals("Concesion"))
            {
                ViewState["solicitudSession"] = paginas.solicitudConcesionSession;
                ViewState["origen"] = paginas.URL_RESUMEN_CONCESION_DE_ACUICULTURA;
            }
            else if (funciones.retornaModulo().Equals("Relocalizacion"))
            {
                ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSession;
                ViewState["origen"] = paginas.URL_RESUMEN_CONCESION_DE_ACUICULTURA;
            }
            else if (funciones.retornaModulo().Equals("RelocalizacionRESA"))
            {
                ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSessionRESA;
                ViewState["origen"] = paginas.URL_RESUMEN_CONCESION_DE_ACUICULTURA;
            }
            else if (funciones.retornaModulo().Equals("Acopio"))
            {
                ViewState["solicitudSession"] = paginas.solicitudAcopioSession;
                ViewState["origen"] = paginas.URL_RESUMEN_CENTRO_ACOPIO;
            }
            else if (funciones.retornaModulo().Equals("Faenamiento"))
            {
                ViewState["solicitudSession"] = paginas.solicitudFaenamientoSession;
                ViewState["origen"] = paginas.URL_RESUMEN_CENTRO_DE_FAENAMIENTO;
            }
            else if (funciones.retornaModulo().Equals("Amerb"))
            {
                ViewState["solicitudSession"] = paginas.solicitudAmerbSession;
                ViewState["origen"] = paginas.URL_RESUMEN_CENTRO_EN_AMERB;
            }
            else if (funciones.retornaModulo().Equals("ExperimentalesAmerb"))
            {
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesAmerbSession;
                ViewState["origen"] = paginas.URL_RESUMEN_CENTRO_EN_AMERB;
            }
            else if (funciones.retornaModulo().Equals("ExperimentalesConcesion"))
            {
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesConcesionSession;
                ViewState["origen"] = paginas.URL_RESUMEN_CONCESION_DE_ACUICULTURA;
            }
            else if (funciones.retornaModulo().Equals("ECMPO"))
            {
                ViewState["solicitudSession"] = paginas.solicitudECMPOSession;
                ViewState["origen"] = paginas.URL_RESUMEN_ECMPO;
            }
            else if (funciones.retornaModulo().Equals("Colector"))
            {
                ViewState["solicitudSession"] = paginas.solicitudColectorSession;
                ViewState["origen"] = paginas.URL_RESUMEN_COLECTOR_DE_SEMILLAS;
            }
            else if (funciones.retornaModulo().Equals("ModificacionAmerb"))
            {
                ViewState["solicitudSession"] = paginas.solicitudModificacionAmerbSession;
                ViewState["origen"] = paginas.URL_RESUMEN_CENTRO_EN_AMERB;
            }
            else if (funciones.retornaModulo().Equals("ModificacionCentroAcopio"))
            {
                ViewState["solicitudSession"] = paginas.solicitudModificacionCentroAcopioSession;
                ViewState["origen"] = paginas.URL_RESUMEN_CENTRO_ACOPIO;
            }
            else if (funciones.retornaModulo().Equals("ModificacionCentroFaenamiento"))
            {
                ViewState["solicitudSession"] = paginas.solicitudModificacionCentroFaenamientoSession;
                ViewState["origen"] = paginas.URL_RESUMEN_CENTRO_DE_FAENAMIENTO;
            }
            else if (funciones.retornaModulo().Equals("ModificacionECMPO"))
            {
                ViewState["solicitudSession"] = paginas.solicitudModificacionECMPOSession;
                ViewState["origen"] = paginas.URL_RESUMEN_ECMPO;
            }
            else
            {
                ViewState["solicitudSession"] = paginas.solicitudModificacionSession;
                ViewState["origen"] = paginas.URL_RESUMEN_CONCESION_DE_ACUICULTURA;
            }
        }


        /*
        * Las unidades espaciales no deben llevar la información de estados de una solicitud. 
        */
        private bool esUnidadEspacial(String nombreUnidadEspacial)
        {
            if (nombreUnidadEspacial != null && nombreUnidadEspacial.Equals("Solicitudes"))
            {
                return false;
            }
            return true;
        }


        private void Initialize_Form()
        {
            SolicitudConcesion solicitudInicial = (SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
            
            if (solicitudInicial != null && solicitudInicial.idSolConcesion > 0)
            {
                if (solicitudInicial.idConcesion > 0)
                {
                    PanelBotonVolverUE.Visible = true;
                }


                HeaderSolicitudConcesion headerSolicitudConcesion = informacionSolicitudService.ObtieneSolicitudConcesionHeader(Convert.ToInt32(solicitudInicial.idSolConcesion));
                if (headerSolicitudConcesion != null)
                {
                    //solicitudConcesion.idConcesion = solicitudInicial.idConcesion;
                    CargaInformacionGralSolicitud(headerSolicitudConcesion, solicitudInicial);

                }
            }
        }





        private void CargaInformacionGralSolicitud(HeaderSolicitudConcesion headerSolicitudConcesion, SolicitudConcesion solicitudInicial)
        {


            if (headerSolicitudConcesion.estadoActual != null)
            {
                EstadoActualSolicitud.Text = Convert.ToString(headerSolicitudConcesion.estadoActual.descripcion);
            }

            if (headerSolicitudConcesion.estadoPosterior != null)
            {
                EstadoPosteriorSolicitud.Text = Convert.ToString(headerSolicitudConcesion.estadoPosterior.descripcion);
            }

            if (headerSolicitudConcesion.estadoActual != null && headerSolicitudConcesion.estadoActual.clave != null && !headerSolicitudConcesion.estadoActual.clave.Equals("/"))
            {
                DefinicionEstado.Text = Convert.ToString(headerSolicitudConcesion.estadoActual.clave);
            }



            if (solicitudInicial.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_COLECTORES_DE_SEMILLA)
            {

                NumIdentificador.Text = Convert.ToString(headerSolicitudConcesion.numPert);
                NPertPanel.Visible = false;
                NumIdentificadorPanel.Visible = true;

                /*
                //INFORMACIÓN QUE SE INGRESO AL CREAR EL TRAMITE
                DatosSolicitudUE datosSolicitudUE = informacionAdicionalSolicitudesService.ObtieneDatosSolicitudUE(solicitudConcesion.idSolConcesion);

                if (datosSolicitudUE != null && datosSolicitudUE.numIdentSolicitud > 0)
                {
                    NumIdentificador.Text = Convert.ToString(datosSolicitudUE.numIdentSolicitud);
                    NPertPanel.Visible = false;
                    NumIdentificadorPanel.Visible = true;
                }
                 * */
            
            }
            else if (solicitudInicial.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB)
            {

                NumIdentificador.Text = Convert.ToString(headerSolicitudConcesion.numPert);
                NumIdentificadorPanel.Visible = true;
                NPertPanel.Visible = false;


                MensajesSolicitud.Text = "";

                if (headerSolicitudConcesion.especiePerteneceCultExp)
                {
                    MensajesSolicitud.Text = "Especies Cultivo Experimental: Sí";
                    PanelMensajes.Visible = true;
                }


                /*
                NumIdentificador.Text = Convert.ToString(solicitudConcesion.numPert);
                NumIdentificadorPanel.Visible = true;
                NPertPanel.Visible = false;
                

                MensajesSolicitud.Text = "";

                if (informacionSolicitudService.tieneEspeciesExperimentalesSolicitud(solicitudConcesion.idSolConcesion))
                {
                    MensajesSolicitud.Text = "Especies Cultivo Experimental: Sí";
                    PanelMensajes.Visible = true;
                }
                 * */

            }
            else if (solicitudInicial.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_EXPERIMENTALES_AMERB)
            {
                NumIdentificador.Text = Convert.ToString(headerSolicitudConcesion.numPert);
                NumIdentificadorPanel.Visible = true;
                NPertPanel.Visible = false;

                if (headerSolicitudConcesion.especiePerteneceCultExp)
                {
                    MensajesSolicitud.Text = "Especies Cultivo Experimental: Sí";
                    PanelMensajes.Visible = true;
                }

                /*
                NumIdentificador.Text = Convert.ToString(solicitudConcesion.numPert);
                NumIdentificadorPanel.Visible = true;
                NPertPanel.Visible = false;

                if(informacionSolicitudService.tieneEspeciesExperimentalesSolicitud(solicitudConcesion.idSolConcesion))
                {
                    MensajesSolicitud.Text = "Especies Cultivo Experimental: Sí";
                    PanelMensajes.Visible = true;
                }
                 * */

            }
            else if (solicitudInicial.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_EXPERIMENTALES_CONCESION)
            {

                NumIdentificador.Text = Convert.ToString(headerSolicitudConcesion.numPert);
                NumIdentificadorPanel.Visible = true;
                NPertPanel.Visible = false;

            }
            else 
            {
                NumeroPert.Text = Convert.ToString(headerSolicitudConcesion.numPert);
                NPertPanel.Visible = true;
                NumIdentificadorPanel.Visible = false;
            }


            if (headerSolicitudConcesion.docRequerido != null && !headerSolicitudConcesion.docRequerido.Equals(""))
            {
                InfoRequerimientos.Text = headerSolicitudConcesion.docRequerido;
                PanelInfoRequerimientos.Visible = true;
            }

            if (headerSolicitudConcesion.islasString() != null && !headerSolicitudConcesion.islasString().Equals(""))
            {
                EstadoActualFlujoIsla.Text = headerSolicitudConcesion.islasString();
                PanelEstadoActualFlujoIsla.Visible = true;
            }
            else 
            {
                EstadoActualFlujoIsla.Text = "";
                PanelEstadoActualFlujoIsla.Visible = false;
            }

            
            /* Valida si la solicitud ya es una concesión, a través del atributo traspasoOk */
            
            if (headerSolicitudConcesion.idConcesion > 0)
            {
                BotonVolverUE.Visible = true;
            }
            else 
            {
                BotonVolverUE.Visible = false;
            }

            this.MensajePendienteSupeditadaSuspendida(headerSolicitudConcesion);
            this.MensajesGenerales(headerSolicitudConcesion);

        }

        private void MensajePendienteSupeditadaSuspendida(HeaderSolicitudConcesion headerSolicitudConcesion)
        {

            //MENSAJE DE PENDIENTE O SUSPENDIDA
            if (((SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()]).mensajePendienteMostrado == false && (headerSolicitudConcesion.tiene_ITC_pend_sup || headerSolicitudConcesion.perteneceGrupoSuspendido))
            {
                ((SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()]).mensajePendienteMostrado = true;
                string script = "alertaMensajeIngresoDoc()";
                ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptAlertaMensajeIngresoDoc", script.ToString(), true);
            }
        }


        private void MensajesGenerales(HeaderSolicitudConcesion headerSolicitudConcesion)
        {

            PanelMensajesVarios.Visible = false;
            PanelGrupoSuspendido.Visible = false;
            PanelCheckSupeditaAvanza.Visible = false;
            PanelCheckSuspendida.Visible = false;
            
            MensajesVarios.Text = "";
            MensajeGrupoSuspendido.Text = "";
            MensajeCheckSupeditaAvanza.Text = "";
            MensajeCheckSuspendida.Text = "";

            if (headerSolicitudConcesion.requiereNuevoPT != null && headerSolicitudConcesion.requiereNuevoPT.id == rbEstadosGenerales.SI)
            {
                MensajesVarios.Text = "Se requiere el ingreso de un nuevo Proyecto Técnico";
                PanelMensajesVarios.Visible = true;
            }

            if (headerSolicitudConcesion.perteneceGrupoSuspendido  && headerSolicitudConcesion.suspendeAvanzaEstado != null && headerSolicitudConcesion.suspendeAvanzaEstado.id == rbEstadosGenerales.SI)
            {
                MensajeGrupoSuspendido.Text = "Esta Solicitud esta dentro de un Grupo Suspendido. ¿Continua tramitación?: SI";
                PanelGrupoSuspendido.Visible = true;
            }

            if (headerSolicitudConcesion.perteneceGrupoSuspendido && (headerSolicitudConcesion.suspendeAvanzaEstado == null || headerSolicitudConcesion.suspendeAvanzaEstado.id != rbEstadosGenerales.SI))
            {
                MensajeGrupoSuspendido.Text = "Esta Solicitud esta dentro de un Grupo Suspendido. ¿Continua tramitación?: NO";
                PanelGrupoSuspendido.Visible = true;
            }

            if (headerSolicitudConcesion.supeditaAvanzaAprueba != null && headerSolicitudConcesion.supeditaAvanzaAprueba.id == rbEstadosGenerales.SI)
            {
                MensajeCheckSupeditaAvanza.Text = "Esta Solicitud continuo su tramitación por IT UOT Aprueba";
                PanelCheckSupeditaAvanza.Visible = true;
            }

        }

            



        protected void BotonVolverUE_Click(object sender, EventArgs e)
        {
            
            SolicitudConcesion solicitudConcesion = (SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

            solicitudConcesion = solicitudDA.ObtieneSolicitudConcesion(solicitudConcesion.idConcesion, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

            Session[ViewState["solicitudSession"].ToString()] = (SolicitudConcesion)solicitudConcesion;
            
            Response.Redirect(ViewState["origen"].ToString());
        }

        
    }
}