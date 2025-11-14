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

namespace SubPesca.Solicitudes.Modificacion
{
    public partial class ingresarSolicitudModificacion : System.Web.UI.Page
    {

        IngresarSolicitudModificacionValidacion ingresarSolicitudModificacionValidacion = new IngresarSolicitudModificacionValidacion();
        SolicitudDA solicitudDA         = new SolicitudDA();
        EnviarCorreo enviarCorreo       = new EnviarCorreo();
        EstadoService estadoService     = new EstadoService();
        PermisosService permisosService = new PermisosService();








        protected void Page_Init(object sender, System.EventArgs e)
        {

            string script = "invoca_calendarios(\"ingresarSolicitudModificacion\")";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario", script.ToString(), true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            // PAGE LOAD
            if (!Page.IsPostBack)
            {

                string Lang = "es-CL";//set your culture here
                System.Threading.Thread.CurrentThread.CurrentCulture =
                new System.Globalization.CultureInfo(Lang);

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_MODIFICACION }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
                {
                    NumPert.ReadOnly = false;
                    FechaRecepcion.ReadOnly = false;
                    FechaIngresoTramite.ReadOnly = false;
                    CodigoCentro.ReadOnly = false;
                    TitularCentro.ReadOnly = false;
                    Guardar.Visible = true;
                }
                else
                {
                    NumPert.ReadOnly = true;
                    FechaRecepcion.ReadOnly = true;
                    FechaIngresoTramite.ReadOnly = true;
                    CodigoCentro.ReadOnly = true;
                    TitularCentro.ReadOnly = true;
                    Guardar.Visible = false;
                }

            }
        
        }

        protected void Guardar_Click(object sender, EventArgs e)
        {

           
            SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();
            SolicitudConcesion solicitudInicial = new SolicitudConcesion();

            try
            {
                solicitudInicial = this.generarFechas(solicitudInicial, FechaRecepcion.Text, FechaIngresoTramite.Text);
            }
            catch (Exception ex)
            {
                Page.Validators.Add(new ValidationError("InicioModificacion", "Formato de fechas es inválido."));
    
            }


            if (NumPert.Text == null || NumPert.Text.Equals(""))
            {
                solicitudInicial.numPert = "0";
            }
            else {
                solicitudInicial.numPert = NumPert.Text;
            }

            solicitudInicial.tipoUnidadEspacial = new ParametroGenerico(rbTipo.UNID_ESPACIAL_CONCESION);
            solicitudInicial.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_MODIFICACION);
            
            solicitudInicial.tramiteModConcesion = new TramiteModConcesion();
            solicitudInicial.tramiteModConcesion.titular = new Persona();


            try
            {

                String rutCompleto = Convert.ToString(TitularCentro.Text);
                String[] rutPartes = rutCompleto.Split('-');

                solicitudInicial.tramiteModConcesion.titular.rutPersona = Convert.ToInt32(rutPartes[0]);
                solicitudInicial.tramiteModConcesion.titular.dvPersona = Convert.ToChar(rutPartes[1]);

            }catch (Exception ex) {
                Page.Validators.Add(new ValidationError("InicioModificacion", "Formato de Rut Titular del Centro Incorrecto."));
            }


            try
            {
                solicitudInicial.tramiteModConcesion.centro = new ParametroGenerico(Convert.ToInt32(CodigoCentro.Text));
            }
            catch (Exception ex) {
                Page.Validators.Add(new ValidationError("InicioModificacion", "Formato de Código de Centro Incorrecto."));
            }



            solicitudInicial.tipoModificacionesTram = new List<ParametroGenerico>();

            if (Especie.Checked) {
                solicitudInicial.tipoModificacionesTram.Add(new ParametroGenerico(rbTipo.MOD_CONCESION_ESPECIE));
            }

            if (ProyectoTecnico.Checked)
            {
                solicitudInicial.tipoModificacionesTram.Add(new ParametroGenerico(rbTipo.MOD_CONCESION_PT));
            }

            if (ampliacionSuperficie.Checked)
            {
                solicitudInicial.tipoModificacionesTram.Add(new ParametroGenerico(rbTipo.MOD_CONCESION_AMPLIA_SUPERFICIE));
            }

            if (reduccionSuperficie.Checked)
            {
                solicitudInicial.tipoModificacionesTram.Add(new ParametroGenerico(rbTipo.MOD_CONCESION_REDUCE_SUPERFICIE));
            }

            if (regularizacion.Checked)
            {
                solicitudInicial.tipoModificacionesTram.Add(new ParametroGenerico(rbTipo.MOD_CONCESION_REGULARIZACION));
            }

            List<String> listErroresSolicitudInicial = ingresarSolicitudModificacionValidacion.validaIngresarSolicitudModificacion(solicitudInicial);

            if (Page.IsValid && listErroresSolicitudInicial.Count <= 0)
            {
                if (solicitudModificacionService.guardarSolicitudModificacionInicial(solicitudInicial, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario))
                {

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

                    }catch(Exception){
                    
                    }
                    
                    SolicitudConcesion solicitudDB = null;
                    solicitudDB = new SolicitudConcesion();
                    solicitudDB = solicitudDA.ObtieneSolicitudConcesionMod(solicitudInicial.idSolConcesion, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                    solicitudDB.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_MODIFICACION);


                    Session["SolicitudModificacion"] = (SolicitudConcesion)solicitudDB;
                    Response.Redirect("~/Solicitudes/Modificacion/identificacionTitularConcesion.aspx");
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

        protected void TiposModificacion_CheckedChanged(object sender, EventArgs e)
        {
            if (regularizacion.Checked)
            {
                if (Especie.Checked || ProyectoTecnico.Checked || ampliacionSuperficie.Checked || reduccionSuperficie.Checked)
                {
                    panelCampos1.Visible = true;
                    UpdatePanelCampos1.Update();

                    panelCampos2.Visible = true;
                    UpdatePanelCampos2.Update();

                    panelCampos3.Visible = true;
                    UpdatePanelCampos3.Update();
                }
                else if (!Especie.Checked && !ProyectoTecnico.Checked && !ampliacionSuperficie.Checked && !reduccionSuperficie.Checked)
                {
                    panelCampos1.Visible = false;
                    UpdatePanelCampos1.Update();

                    panelCampos2.Visible = true;
                    UpdatePanelCampos2.Update();

                    panelCampos3.Visible = true;
                    UpdatePanelCampos3.Update();

                }

            }
            else if ((Especie.Checked || ProyectoTecnico.Checked || ampliacionSuperficie.Checked || reduccionSuperficie.Checked) && !regularizacion.Checked)
            {
                panelCampos1.Visible = true;
                UpdatePanelCampos1.Update();

                panelCampos2.Visible = true;
                UpdatePanelCampos2.Update();

                panelCampos3.Visible = true;
                UpdatePanelCampos3.Update();

            }
            else {
                panelCampos1.Visible = false;
                UpdatePanelCampos1.Update();

                panelCampos2.Visible = false;
                UpdatePanelCampos2.Update();

                panelCampos3.Visible = false;
                UpdatePanelCampos3.Update();
            
            }
        }

        protected void CodigoCentro_OnTextChanged(object sender, EventArgs e)
        {

            try
            {
                RequiredFieldValidatorNombreCentro.Validate();
                CompareValidatorCodigoCentro.Validate();

                if(Page.IsValid){
                    List<String> listErroresSolicitudInicial = ingresarSolicitudModificacionValidacion.validaCodigoCentroConcesionAcuicultura(CodigoCentro.Text);

                    if (listErroresSolicitudInicial.Count > 0)
                    {
                        foreach (String mensaje in listErroresSolicitudInicial)
                        {
                            string script = @"<script type='text/javascript'>despliegaMensajeAlerta('"+ mensaje + "');</script>";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "mensaje_cargado", script, false);
                        }
                    }
                }

            }catch (Exception ex) {

                string script2 = @"<script type='text/javascript'>oculta_loading('cargando');</script>";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "mensaje_cargado", script2, false);
            }

            
        }

        protected void TitularCentro_OnTextChanged(object sender, EventArgs e)
        {

            //RequiredFieldValidatorTitularCentro.Validate();
            //ccNumCustVal.Validate();

            try
            {

                Page.Validate("InicioModificacion");

                if (Page.IsValid)
                {
                    List<String> listErroresSolicitudInicial = ingresarSolicitudModificacionValidacion.validaTitularCentroConcesionAcuicultura(CodigoCentro.Text, TitularCentro.Text);

                    if (listErroresSolicitudInicial.Count > 0)
                    {
                        foreach (String mensaje in listErroresSolicitudInicial)
                        {
                            string script = @"<script type='text/javascript'>despliegaMensajeAlerta2('" + mensaje + "','cargando');</script>";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "mensaje_cargado", script, false);
                        }
                    }
                }


            }
            catch (Exception ex)
            {

                string script2 = @"<script type='text/javascript'>oculta_loading('cargando');</script>";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "mensaje_cargado", script2, false);
            }
        }


    }
}