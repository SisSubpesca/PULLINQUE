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
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;

namespace SubPesca.Solicitudes.Registrar
{
    public partial class inicioSolicitudConcesion : System.Web.UI.Page
    {

        InicioSolicitudConcesionValidacion inicioSolicitudConcesionValidacion = new InicioSolicitudConcesionValidacion();
        PermisosService permisosService = new PermisosService();

        EnviarCorreo enviarCorreo = new EnviarCorreo();

        protected void Page_Load(object sender, EventArgs e)
        {

             // PAGE LOAD
            if (!Page.IsPostBack)
            {

                string Lang = "es-CL";//set your culture here
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Lang);
                


                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_CONCESION }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
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

        SolicitudDA solicitudDA = new SolicitudDA();
        
        /**
         * Método que guarda los datos básicos de una solicitud de concesión de acuicultura.
         */ 
        protected void Guardar_Click(object sender, EventArgs e)
        {
            SolicitudConcesionService solicitudConcesService = new SolicitudConcesionService();
            SolicitudConcesion solicitudInicial = new SolicitudConcesion();

            solicitudInicial = this.generarFechas(solicitudInicial, FechaRecepcion.Text, FechaIngresoTramite.Text);
            
            solicitudInicial.numPert = Convert.ToString(numPert.Text);

            List<String> listErroresSolicitudInicial =  inicioSolicitudConcesionValidacion.validaInicioSolicitudConcesionAcuicultura(solicitudInicial);

            if (listErroresSolicitudInicial.Count <= 0)
            {
                if (solicitudConcesService.guardarSolicitudConcesionInicial(solicitudInicial))
                {
                    solicitudInicial = solicitudDA.ObtieneSolicitudConcesion(solicitudInicial.idSolConcesion, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                    //Session["SolicitudConcesionInicial"] = (SolicitudConcesion)solicitudInicial;

                    try
                    {
                        enviarCorreo.alertaIngresoNuevaSolicitud(solicitudInicial);
                    }
                    catch (Exception)
                    {


                    }

                    Session["SolicitudConcesion"] = (SolicitudConcesion)solicitudInicial;
                    Response.Redirect("~/Solicitudes/Registrar/identificacionSolicitante.aspx");
                }
                else
                {
                    Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
                }
            }
            else {

                foreach (String error in listErroresSolicitudInicial)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
            } 
         }

        protected SolicitudConcesion generarFechas(SolicitudConcesion solicitudInicial,string fechaRecepcion, string fechaIngresoTramite)
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