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

namespace SubPesca.Solicitudes.Faenamiento
{
    public partial class inicioSolicitudFaenamiento : System.Web.UI.Page
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

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_FAENAMIENTO }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
                {

                    numPert.ReadOnly = false;
                    FechaRecepcion.ReadOnly = false;
                    FechaIngresoTramite.ReadOnly = false;

                    PanelBotonIngresar.Visible = true;
                }
                else
                {

                    numPert.ReadOnly = true;
                    FechaRecepcion.ReadOnly = true;
                    FechaIngresoTramite.ReadOnly = true;

                    PanelBotonIngresar.Visible = false;
                }

            }


        }


        /**
         * Método que guarda los datos básicos de una solicitud de concesión de acuicultura.
         */
        protected void Guardar_Click(object sender, EventArgs e)
        {
            SolicitudCentroFaenamientoService centroFaenamientoService = new SolicitudCentroFaenamientoService();
            SolicitudConcesion solicitudInicial = new SolicitudConcesion();

            solicitudInicial = this.generarFechas(solicitudInicial, FechaRecepcion.Text, FechaIngresoTramite.Text);

            solicitudInicial.numPert = Convert.ToString(numPert.Text);

            List<String> listErroresSolicitudInicial = inicioSolicitudConcesionValidacion.validaInicioSolicitudCentroFaenamiento(solicitudInicial);

            if (listErroresSolicitudInicial.Count <= 0)
            {
                if (centroFaenamientoService.guardarSolicitudCentroFaenamiento(solicitudInicial))
                {

                    solicitudInicial = solicitudDA.ObtieneSolicitudConcesion(solicitudInicial.idSolConcesion, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                    //POR DEFECTO LOS CENTROS DE FAENAMIENTO SON "NO SE SOMETE SEA"
                    solicitudConcesionService.ActualizaSolicitudSEA(solicitudInicial.idSolConcesion, rbTipo.SOMETIMIENTO_SEA_NO_SOMETE, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);


                    try
                    {
                        enviarCorreo.alertaIngresoNuevaSolicitud(solicitudInicial);
                    }
                    catch (Exception)
                    {


                    }

                    Session["SolicitudCentroFaenamiento"] = (SolicitudConcesion)solicitudInicial;
                    Response.Redirect("~/Solicitudes/Faenamiento/identificacionSolicitanteFaenamiento.aspx");
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


    }
}