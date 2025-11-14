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
using LogicaNegocio.cl.subpesca.rb.common;

namespace SubPesca.Solicitudes.ExperimentalesAmerb
{
    public partial class inicioSolicitudExperimentalesAmerb : System.Web.UI.Page
    {
        InicioSolicitudConcesionValidacion inicioSolicitudConcesionValidacion = new InicioSolicitudConcesionValidacion();
        SolicitudDA solicitudDA = new SolicitudDA();
        OficinaDA oficinaDA = new OficinaDA();
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

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_EXPERIMENTALES_AMERB }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
                {

                    numPert.ReadOnly = false;
                    FechaRecepcion.ReadOnly = false;
                    FechaIngresoTramite.ReadOnly = false;
                    NumeroCI.ReadOnly = false;
                    FechaCI.ReadOnly = false;

                    PanelBotonIngresar.Visible = true;
                }
                else
                {

                    numPert.ReadOnly = true;
                    FechaRecepcion.ReadOnly = true;
                    FechaIngresoTramite.ReadOnly = true;
                    NumeroCI.ReadOnly = true;
                    FechaCI.ReadOnly = true;

                    PanelBotonIngresar.Visible = false;
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


        /**
         * Método que guarda los datos básicos de una solicitud de concesión de acuicultura.
         */
        protected void Guardar_Click(object sender, EventArgs e)
        {
            SolicitudExperimentalesAmerbService experimentalesAmerbService = new SolicitudExperimentalesAmerbService();
            SolicitudConcesion solicitudInicial = new SolicitudConcesion();

            DatosSolicitudUE datosSolicitudUE = new DatosSolicitudUE();
            datosSolicitudUE.numeroCI = Convert.ToInt32(NumeroCI.Text);
            datosSolicitudUE = this.generarFechas(datosSolicitudUE, FechaCI.Text);


            solicitudInicial.numPert = Convert.ToString(numPert.Text); //NUMERO IDENTIFICADOR DE LA SOLICITUD
            solicitudInicial = this.generarFechas(solicitudInicial, FechaRecepcion.Text, FechaIngresoTramite.Text);
            solicitudInicial.datosSolicitudUE = datosSolicitudUE;

            solicitudInicial.datosSolicitudUE.oficina = new ParametroGenerico(Oficina.SelectedValue);

            solicitudInicial.unidadEspacial = new UnidadEspacial();
            solicitudInicial.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
            solicitudInicial.unidadEspacial.centrosDeCultivo.codigoCentro = CodigoAcuiculturaAmerb.Text;


            List<String> listErroresSolicitudInicial = inicioSolicitudConcesionValidacion.validaInicioSolicitudExperimentalesAmerb(solicitudInicial);

            if (listErroresSolicitudInicial.Count <= 0)
            {
                if (experimentalesAmerbService.guardarSolicitudExperimentalesAmerb(solicitudInicial, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario))
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

                    Session["SolicitudCentroExperimentalesAmerb"] = (SolicitudConcesion)solicitudInicial;
                    Response.Redirect("~/Solicitudes/ExperimentalesAmerb/identificacionTitularExperimentalesAmerb.aspx");
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
            if (CodigoAcuiculturaAmerb.Text != null && !CodigoAcuiculturaAmerb.Text.Equals(""))
            {
                List<String> listErroresSolicitudInicial = inicioSolicitudConcesionValidacion.validaCodigoCentroAcuiculturaAmerb(CodigoAcuiculturaAmerb.Text);
                
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

            numPert.Text = pertGenerado;
            UpdatePanelPert.Update();

        }


    }
}