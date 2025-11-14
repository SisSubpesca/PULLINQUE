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


namespace SubPesca.Solicitudes.Colector
{
    public partial class inicioSolicitudColector : System.Web.UI.Page
    {
        InicioSolicitudConcesionValidacion inicioSolicitudConcesionValidacion = new InicioSolicitudConcesionValidacion();
        SolicitudConcesionService solicitudConcesionService = new SolicitudConcesionService();
        SolicitudDA solicitudDA = new SolicitudDA();
        String mensaje = "";

        EnviarCorreo enviarCorreo = new EnviarCorreo();
        PermisosService permisosService = new PermisosService();


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

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_COLECTORES }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
                {

                    NumIdentificador.ReadOnly = false;
                    NumeroCI.ReadOnly = false;
                    FechaRecepcion.ReadOnly = false;

                    PanelBotonIngresar.Visible = true;
                }
                else
                {

                    NumIdentificador.ReadOnly = true;
                    NumeroCI.ReadOnly = true;
                    FechaRecepcion.ReadOnly = true;

                    PanelBotonIngresar.Visible = false;
                }

            }
        }


        /**
         * Método que guarda los datos básicos de una solicitud de concesión de acuicultura.
         */
        protected void Guardar_Click(object sender, EventArgs e)
        {
            SolicitudCentroColectorService centroColectorService = new SolicitudCentroColectorService();
            SolicitudConcesion solicitudInicial = new SolicitudConcesion();


            DatosSolicitudUE datosSolicitudUE = new DatosSolicitudUE();
            datosSolicitudUE.numIdentSolicitud = Convert.ToInt32(NumIdentificador.Text);
            datosSolicitudUE.numeroCI = Convert.ToInt32(NumeroCI.Text);
            datosSolicitudUE = this.generarFechas(datosSolicitudUE, FechaRecepcion.Text);


            solicitudInicial.datosSolicitudUE = datosSolicitudUE;

            List<String> listErroresSolicitudInicial = inicioSolicitudConcesionValidacion.validaInicioSolicitudColectorSemilla(solicitudInicial);

            if (listErroresSolicitudInicial.Count <= 0)
            {
                if (centroColectorService.guardarSolicitudColectorSemilla(solicitudInicial, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario))
                {
                    
                    solicitudInicial = solicitudDA.ObtieneSolicitudConcesion(solicitudInicial.idSolConcesion, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                    //POR DEFECTO LOS COLECTORES SON "NO SE SOMETE SEA"
                    solicitudConcesionService.ActualizaSolicitudSEA(solicitudInicial.idSolConcesion, rbTipo.SOMETIMIENTO_SEA_NO_SOMETE, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);


                    try
                    {
                        solicitudInicial.datosSolicitudUE = datosSolicitudUE;
                        enviarCorreo.alertaIngresoNuevaSolicitud(solicitudInicial);
                    }
                    catch (Exception)
                    {

                    }

                    Session["SolicitudCentroColector"] = (SolicitudConcesion)solicitudInicial;
                    Response.Redirect("~/Solicitudes/Colector/identificacionSolicitanteColector.aspx");
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


        protected DatosSolicitudUE generarFechas(DatosSolicitudUE datosSolicitudUE, string fechaCI)
        {

            if (fechaCI != null && !fechaCI.Equals(""))
            {
                datosSolicitudUE.fechaCI = Convert.ToDateTime(fechaCI);
            }
            
            return datosSolicitudUE;
        }


    }
}