using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using Validaciones.cl.subpesca.rb.solicitud;
using SubPesca.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.estados;
using Validaciones.cl.subpesca.rb.modificacion;

namespace SubPesca.Solicitudes.ExperimentalesConcesion
{
    public partial class inicioSolicitudExperimentalesConcesion : System.Web.UI.Page
    {
        InicioSolicitudConcesionValidacion inicioSolicitudConcesionValidacion = new InicioSolicitudConcesionValidacion();
        IngresarSolicitudModificacionValidacion ingresarSolicitudModificacionValidacion = new IngresarSolicitudModificacionValidacion();

        SolicitudDA solicitudDA = new SolicitudDA();
        String mensaje = "";

        EnviarCorreo enviarCorreo = new EnviarCorreo();
        PermisosService permisosService = new PermisosService();
        EstadoService estadoService = new EstadoService();



        public String MensajeRegistro
        {
            get
            {
                return mensaje;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            string Lang = "es-CL";//set your culture here
            System.Threading.Thread.CurrentThread.CurrentCulture =
            new System.Globalization.CultureInfo(Lang);

            // PAGE LOAD
            if (!Page.IsPostBack)
            {

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_EXPERIMENTALES_CONCESION }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
                {

                    numPert.ReadOnly = false;
                    NumeroCI.ReadOnly = false;
                    FechaCI.ReadOnly = false;

                    PanelBotonIngresar.Visible = true;
                }
                else
                {

                    numPert.ReadOnly = true;
                    NumeroCI.ReadOnly = true;
                    FechaCI.ReadOnly = true;

                    PanelBotonIngresar.Visible = false;
                }

            }
        }


        /**
         * Método que guarda los datos básicos de una solicitud de concesión de acuicultura.
         */
        protected void Guardar_Click(object sender, EventArgs e)
        {
            SolicitudExperimentalesConcesionService experimentalesConcesionService = new SolicitudExperimentalesConcesionService();
            SolicitudConcesion solicitudInicial = new SolicitudConcesion();


            DatosSolicitudUE datosSolicitudUE = new DatosSolicitudUE();
            datosSolicitudUE.numeroCI = Convert.ToInt32(NumeroCI.Text);
            datosSolicitudUE = this.generarFechas(datosSolicitudUE, FechaCI.Text);


            solicitudInicial.numPert = Convert.ToString(numPert.Text); //NUMERO IDENTIFICADOR DE LA SOLICITUD
            solicitudInicial.datosSolicitudUE = datosSolicitudUE;

            solicitudInicial.unidadEspacial = new UnidadEspacial();
            solicitudInicial.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
            solicitudInicial.unidadEspacial.centrosDeCultivo.codigoCentro = CodigoCentro.Text;

            List<String> listErroresSolicitudInicial = inicioSolicitudConcesionValidacion.validaInicioSolicitudExperimentalesConcesion(solicitudInicial);

            if (listErroresSolicitudInicial.Count <= 0)
            {
                if (experimentalesConcesionService.guardarSolicitudExperimentalesConcesion(solicitudInicial, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario))
                {
                    solicitudInicial = solicitudDA.ObtieneSolicitudConcesion(solicitudInicial.idSolConcesion, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                    //RECALCULA EL ESTADO DE LA SOLICITUD
                    try
                    {
                        estadoService.RecalcularEstadoSolicitud(solicitudInicial.idSolConcesion);

                    }
                    catch (Exception)
                    {

                    }

                    try
                    {
                        enviarCorreo.alertaIngresoNuevaSolicitud(solicitudInicial);
                    }
                    catch (Exception)
                    {

                    }

                    Session["SolicitudCentroExperimentalesConcesion"] = (SolicitudConcesion)solicitudInicial;
                    Response.Redirect("~/Solicitudes/ExperimentalesConcesion/identificacionTitularExperimentalesConcesion.aspx");
                }
                else
                {
                    Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
                }
            }
            else
            {

                foreach (String error in listErroresSolicitudInicial)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
            }
        }


        protected SolicitudConcesion generarFechas(SolicitudConcesion solicitudInicial, string fechaRecepcion, string fechaIngresoTramite)
        {

            if (fechaRecepcion != null && !fechaRecepcion.Equals(""))
            {
                solicitudInicial.fechaRecepcion = Convert.ToDateTime(fechaRecepcion);
            }
            if (fechaIngresoTramite != null && !fechaIngresoTramite.Equals(""))
            {
                solicitudInicial.fechaIngresoTramite = Convert.ToDateTime(fechaIngresoTramite);
            }

            return solicitudInicial;
        }

        protected DatosSolicitudUE generarFechas(DatosSolicitudUE datosSolicitudUE, string fechaCI)
        {

            if (fechaCI != null && !fechaCI.Equals(""))
            {
                datosSolicitudUE.fechaCI = Convert.ToDateTime(fechaCI);
            }

            return datosSolicitudUE;
        }

        protected void CodigoAcuiculturaAmerb_OnTextChanged(object sender, EventArgs e)
        {
            if (CodigoCentro.Text != null && !CodigoCentro.Text.Equals(""))
            {
                List<String> listErroresSolicitudInicial = ingresarSolicitudModificacionValidacion.validaCodigoCentroConcesionAcuicultura(CodigoCentro.Text);

                if (listErroresSolicitudInicial.Count > 0)
                {
                    foreach (String mensaje in listErroresSolicitudInicial)
                    {
                        string script = @"<script type='text/javascript'>despliegaMensajeAlerta('" + mensaje + "');</script>";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "mensaje_cargado", script, false);
                    }
                }
            }
        }

    }
}