using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using Validaciones.cl.subpesca.rb.modificacion;
using LogicaNegocio.cl.subpesca.rb.servicios.modificacion;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Contantes;
using SubPesca.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;
using LogicaNegocio.cl.subpesca.rb.servicios.estados;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;



namespace SubPesca.Solicitudes.ModificacionAmerb
{
    public partial class redefinirTramiteModificacionAmerb : System.Web.UI.Page
    {
        private IngresarSolicitudModificacionValidacion ingresarSolicitudModificacionValidacion = new IngresarSolicitudModificacionValidacion();
        private SolicitudDA solicitudDA = new SolicitudDA();
        private EnviarCorreo enviarCorreo = new EnviarCorreo();
        private EstadoService estadoService = new EstadoService();
        private PermisosService permisosService = new PermisosService();



        protected void Page_Load(object sender, EventArgs e)
        {

            // PAGE LOAD
            if (!Page.IsPostBack)
            {


                if (Request.QueryString["idSolConces"] != null)
                {
                    int idSolicitudConcesion = Convert.ToInt32(Request.QueryString["idSolConces"]);

                    SolicitudConcesion solicitudModificacion = solicitudDA.ObtieneSolicitudConcesionMod(idSolicitudConcesion, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                    if (solicitudModificacion != null && solicitudModificacion.idSolConcesion > 0)
                    {

                        IdSolicitud.Value = Convert.ToString(solicitudModificacion.idSolConcesion);
                        CodigoCentro.Text = Convert.ToString(solicitudModificacion.tramiteModConcesion.centro.id);
                        NumPert.Text = solicitudModificacion.numPert.ToString();
                        FechaRecepcion.Text = solicitudModificacion.fechaRecepcion.ToString();
                        FechaIngresoTramite.Text = solicitudModificacion.fechaIngresoTramite.ToString();
                        TipoModificacion.Text = solicitudModificacion.tipoModificacionString();
                    }
                    else
                    {
                        Response.Redirect("~/Inicio.aspx");
                    }
                }


                string Lang = "es-CL";//set your culture here
                System.Threading.Thread.CurrentThread.CurrentCulture =
                new System.Globalization.CultureInfo(Lang);

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_MODIFICACION_AMERB }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
                {
                    Guardar.Visible = true;
                }
                else
                {
                    Guardar.Visible = false;
                }

            }

        }

        protected void Guardar_Click(object sender, EventArgs e)
        {


            SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();
            SolicitudConcesion solicitudInicial = new SolicitudConcesion();



            solicitudInicial.idSolConcesion = Convert.ToInt32(IdSolicitud.Value);
            solicitudInicial.tipoModificacionesTram = new List<ParametroGenerico>();

            if (Especie.Checked)
            {
                solicitudInicial.tipoModificacionesTram.Add(new ParametroGenerico(rbTipo.MOD_AMERB_ESPECIE));
            }

            if (ProyectoTecnico.Checked)
            {
                solicitudInicial.tipoModificacionesTram.Add(new ParametroGenerico(rbTipo.MOD_AMERB_PT));
            }

            if (ampliacionSuperficie.Checked)
            {
                solicitudInicial.tipoModificacionesTram.Add(new ParametroGenerico(rbTipo.MOD_AMERB_AMPLIA_SUPERFICIE));
            }

            if (reduccionSuperficie.Checked)
            {
                solicitudInicial.tipoModificacionesTram.Add(new ParametroGenerico(rbTipo.MOD_AMERB_REDUCE_SUPERFICIE));
            }

            if (regularizacion.Checked)
            {
                solicitudInicial.tipoModificacionesTram.Add(new ParametroGenerico(rbTipo.MOD_AMERB_REGULARIZACION));
            }


            List<String> listErroresSolicitudInicial = new List<String>();
            if (solicitudInicial.tipoModificacionesTram.Count < 1)
            {
                listErroresSolicitudInicial.Add("Seleccione al menos 1 tipo de modificación");
            }



            if (Page.IsValid && listErroresSolicitudInicial.Count <= 0)
            {
                if (solicitudModificacionService.redefinirTramiteModificacion(solicitudInicial, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario))
                {

                    //RECALCULA EL ESTADO DE LA SOLICITUD
                    try
                    {
                        estadoService.RecalcularEstadoSolicitud(solicitudInicial.idSolConcesion);

                    }
                    catch (Exception)
                    {

                    }

                    SolicitudConcesion solicitudDB = null;
                    solicitudDB = new SolicitudConcesion();
                    solicitudDB = solicitudDA.ObtieneSolicitudConcesionMod(solicitudInicial.idSolConcesion, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                    solicitudDB.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB);


                    Session["SolicitudModificacionAmerb"] = (SolicitudConcesion)solicitudDB;
                    Response.Redirect("~/Solicitudes/ModificacionAmerb/datosModificacionAmerb.aspx");
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
                    Page.Validators.Add(new ValidationError("InicioModificacion", error));
                }
                UpdatePanelErroresValidacion.Update();

            }

            string script2 = @"<script type='text/javascript'>oculta_loading('cargando');</script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "mensaje_cargado", script2, false);
        }



    }
}