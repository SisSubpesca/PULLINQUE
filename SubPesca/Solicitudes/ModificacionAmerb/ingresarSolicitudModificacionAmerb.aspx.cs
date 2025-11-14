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
using LogicaNegocio.cl.subpesca.rb.common;

namespace SubPesca.Solicitudes.ModificacionAmerb
{
    public partial class ingresarSolicitudModificacionAmerb : System.Web.UI.Page
    {
        IngresarSolicitudModificacionValidacion ingresarSolicitudModificacionValidacion = new IngresarSolicitudModificacionValidacion();
        SolicitudDA solicitudDA = new SolicitudDA();
        EnviarCorreo enviarCorreo = new EnviarCorreo();
        EstadoService estadoService = new EstadoService();
        PermisosService permisosService = new PermisosService();
        OficinaDA oficinaDA = new OficinaDA();



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

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_MODIFICACION_AMERB }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
                {
                    NumPert.ReadOnly = false;
                    FechaRecepcion.ReadOnly = false;
                    FechaIngresoTramite.ReadOnly = false;
                    CodigoCentro.ReadOnly = false;
                    TitularCentro.ReadOnly = false;
                    NumeroCI.ReadOnly = false;
                    FechaCI.ReadOnly = false;
                    Guardar.Visible = true;
                }
                else
                {
                    NumPert.ReadOnly = true;
                    FechaRecepcion.ReadOnly = true;
                    FechaIngresoTramite.ReadOnly = true;
                    CodigoCentro.ReadOnly = true;
                    TitularCentro.ReadOnly = true;
                    NumeroCI.ReadOnly = true;
                    FechaCI.ReadOnly = true;
                    Guardar.Visible = false;
                }

                Initialize_Comboboxs();
            }
        }

        protected void Initialize_Comboboxs()
        {
            Carga_Combobox("Oficina");
            Oficina.SelectedValue = "0";

        }

        protected void Carga_Combobox(string combobox)
        {


            switch (combobox)
            {


                case "Oficina":


                    // Cargamos el combobox: Oficina
                    Oficina.Items.Clear();
                    Oficina.DataSource = oficinaDA.obtenerOficina(null);
                    Oficina.DataTextField = "nombreDirZonal";
                    Oficina.DataValueField = "codDirZonal";
                    Oficina.DataBind();
                    Oficina.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

            };
        }


        protected void Guardar_Click(object sender, EventArgs e)
        {

            SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();
            SolicitudConcesion solicitudInicial = new SolicitudConcesion();

            DatosSolicitudUE datosSolicitudUE = new DatosSolicitudUE();
            datosSolicitudUE.numeroCI = Convert.ToInt32(NumeroCI.Text);

            try
            {
                datosSolicitudUE = this.generarFechas(datosSolicitudUE, FechaCI.Text);
            }
            catch (Exception ex)
            {
                Page.Validators.Add(new ValidationError("InicioModificacion", "Formato de fechas es inválido."));

            }
            
            solicitudInicial.datosSolicitudUE = datosSolicitudUE;


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
            else
            {
                solicitudInicial.numPert = NumPert.Text;
            }

            solicitudInicial.tipoUnidadEspacial = new ParametroGenerico(rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB);
            solicitudInicial.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB);

            solicitudInicial.tramiteModConcesion = new TramiteModConcesion();
            solicitudInicial.tramiteModConcesion.titular = new Persona();

            solicitudInicial.datosSolicitudUE.oficina = new ParametroGenerico(Oficina.SelectedValue);

            try
            {

                String rutCompleto = Convert.ToString(TitularCentro.Text);
                String[] rutPartes = rutCompleto.Split('-');

                solicitudInicial.tramiteModConcesion.titular.rutPersona = Convert.ToInt32(rutPartes[0]);
                solicitudInicial.tramiteModConcesion.titular.dvPersona = Convert.ToChar(rutPartes[1]);

            }
            catch (Exception ex)
            {
                Page.Validators.Add(new ValidationError("InicioModificacion", "Formato de Rut Titular del Centro Incorrecto."));
            }


            try
            {
                solicitudInicial.tramiteModConcesion.centro = new ParametroGenerico(Convert.ToInt32(CodigoCentro.Text));
            }
            catch (Exception ex)
            {
                Page.Validators.Add(new ValidationError("InicioModificacion", "Formato de Código de Centro Incorrecto."));
            }


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

            List<String> listErroresSolicitudInicial = ingresarSolicitudModificacionValidacion.validaIngresarSolicitudModificacionAmerb(solicitudInicial);

            if (Page.IsValid && listErroresSolicitudInicial.Count <= 0)
            {
                if (solicitudModificacionService.guardarSolicitudModificacionAmerbInicial(solicitudInicial, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario))
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

                    }
                    catch (Exception)
                    {

                    }

                    SolicitudConcesion solicitudDB = null;
                    solicitudDB = new SolicitudConcesion();
                    solicitudDB = solicitudDA.ObtieneSolicitudConcesionMod(solicitudInicial.idSolConcesion, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                    solicitudDB.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB);


                    Session["SolicitudModificacionAmerb"] = (SolicitudConcesion)solicitudDB;
                    Response.Redirect("~/Solicitudes/ModificacionAmerb/identificacionTitularModificacionAmerb.aspx");
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

                    panelCampos4.Visible = true; //oficina y nº tramite
                    UpdatepanelCampos4.Update();
                }
                else if (!Especie.Checked && !ProyectoTecnico.Checked && !ampliacionSuperficie.Checked && !reduccionSuperficie.Checked)
                {

                    FechaRecepcion.Text = "";
                    FechaIngresoTramite.Text = "";
                    panelCampos1.Visible = false;
                    UpdatePanelCampos1.Update();

                    panelCampos2.Visible = true;
                    UpdatePanelCampos2.Update();

                    panelCampos3.Visible = true;
                    UpdatePanelCampos3.Update();


                    Oficina.SelectedValue = "0";
                    NumPert.Text = "";
                    panelCampos4.Visible = false; //oficina y nº tramite
                    UpdatepanelCampos4.Update();

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

                panelCampos4.Visible = true; //oficina y nº tramite
                UpdatepanelCampos4.Update();

            }
            else
            {
                panelCampos1.Visible = false;
                UpdatePanelCampos1.Update();

                panelCampos2.Visible = false;
                UpdatePanelCampos2.Update();

                panelCampos3.Visible = false;
                UpdatePanelCampos3.Update();

                panelCampos4.Visible = false; //oficina y nº tramite
                UpdatepanelCampos4.Update();

            }
        }

        protected void CodigoCentro_OnTextChanged(object sender, EventArgs e)
        {

            try
            {

            RequiredFieldValidatorNombreCentro.Validate();
            CompareValidatorCodigoCentro.Validate();

                if (Page.IsValid)
                {
                    List<String> listErroresSolicitudInicial = ingresarSolicitudModificacionValidacion.validaCodigoCentroAmerb(CodigoCentro.Text);

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
            catch (Exception ex)
            {

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



        protected DatosSolicitudUE generarFechas(DatosSolicitudUE datosSolicitudUE, string fechaCI)
        {

            if (fechaCI != null && !fechaCI.Equals(""))
            {
                datosSolicitudUE.fechaCI = Convert.ToDateTime(fechaCI);
            }

            return datosSolicitudUE;
        }


        protected void NumeroCI_TextChanged(object sender, EventArgs e)
        {
            GenerarPert();
        }

        protected void FechaCI_TextChanged(object sender, EventArgs e)
        {
            GenerarPert();
        }

        protected void Oficina_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerarPert();
        }


        protected void GenerarPert()
        {

            string pertGenerado = "";

            if (!NumeroCI.Text.Trim().Equals("") && !FechaCI.Text.Trim().Equals("") && !Oficina.SelectedValue.Trim().Equals("0"))
            {

                try
                {
                    DateTime time = Convert.ToDateTime(FechaCI.Text);
                    pertGenerado = NumeroCI.Text.Trim() + Oficina.SelectedValue + time.Year;

                }
                catch { }
            }

            NumPert.Text = pertGenerado;
            UpdatePanelPert.Update();

        }



    }
}