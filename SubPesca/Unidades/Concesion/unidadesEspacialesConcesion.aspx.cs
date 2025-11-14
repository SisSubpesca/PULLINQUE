using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.unidadEspacial;
using LogicaNegocio.cl.subpesca.rb.common;
using System.IO;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes.unidadEspacial;
using Datos.Entidades;
using System.Collections;
using Validaciones.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Utilidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using System.Drawing;
using SubPesca.Utilidades;
using SubPesca.Mantenedores.Generales;

namespace SubPesca.Unidades.Concesion
{
    public partial class unidadesEspacialesConcesion : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        SolicitudDA solicitudDA = new SolicitudDA();
        UnidadEspacialDA unidadEspacialDA = new UnidadEspacialDA();
        CapitaniaDePuertoDA capitaniaDePuertoDA = new CapitaniaDePuertoDA();
        TipoDA tipoDa = new TipoDA();
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();
        
        PermisosService permisosService = new PermisosService();
        UnidadEspacialValidacion unidadEspacialValidacion = new UnidadEspacialValidacion();
        SolicitudConcesionService solicitudConcesionService = new SolicitudConcesionService();
        MantenedorGeneralService mantenedorGeneralService = new MantenedorGeneralService();

        EnviarCorreo enviarCorreo = new EnviarCorreo();

        protected void setearModulo()
        {
            ViewState["solicitudSession"] = paginas.solicitudConcesionSession;
            ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLCONCESION;



            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.UNIDAD_ESPACIAL_CONCESION_CREADA };
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["__EVENTTARGET"] != null)
            {
                this.NumeroPlazo_TextChanged(null, null);
            }


            if (!Page.IsPostBack)
            {

                string Lang = "es-CL";//set your culture here
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Lang);

                setearModulo();


                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (solicitudConcesion == null || usuario_logeado == null)
                {
                    Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
                }

                //BOTON DE INGRESO O MODIFICACION
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {
                    CodigoCentro.Enabled = true;

                    NumeroDiarioOficial.Enabled = true;
                    FechaDiarioOficial.Enabled = true;
                    PanelCalendario.Visible = true;

                    NumeroActaEntrega.Enabled = true;
                    FechaActaEntrega.Enabled = true;
                    PanelFechaActaEntrega.Visible = true;

                    //CapitaniaPuerto.Enabled = true;

                    PanelBotonGuardar.Visible = true;
                    
                    PlazoNominal.Enabled = true;
                    PanelPlazoNominal.Visible = true;

                    PlazoInicio.Enabled = true;
                    PanelPlazoInicio.Visible = true;

                    NumeroPlazo.Enabled = true;
                    PanelNumeroPlazo.Visible = true;

                    PlazoVencimiento.Enabled = true;
                    PanelPlazoNominalVencimiento.Visible = true;
                
                }
                
                else {
                    CodigoCentro.Enabled = false;

                    NumeroDiarioOficial.Enabled = false;
                    FechaDiarioOficial.Enabled = false;
                    PanelCalendario.Visible = false;

                    NumeroActaEntrega.Enabled = false;
                    FechaActaEntrega.Enabled = false;
                    PanelFechaActaEntrega.Visible = false;

                    //CapitaniaPuerto.Enabled = false;

                    PanelBotonGuardar.Visible = false;
                    
                    PlazoNominal.Enabled = false;
                    PanelPlazoNominal.Visible = false;

                    PlazoInicio.Enabled = false;
                    PanelPlazoInicio.Visible = false;

                    NumeroPlazo.Enabled = false;
                    PanelNumeroPlazo.Visible = false;

                    PlazoVencimiento.Enabled = false;
                    PanelPlazoNominalVencimiento.Visible = false;
                
                }


                // Inicializamos el formulario
                Initialize_Form();
                PlazoNominal_change(null, null);

            }
        }


        protected void Initialize_Form()
        {
            SolicitudConcesion solicitudAux = (SolicitudConcesion)Session["SolicitudConcesion"];

            if (solicitudAux != null && solicitudAux.idSolConcesion > 0)
            {

                IdSolicitud.Value = Convert.ToString(solicitudAux.idSolConcesion);

                //Visibilidad de campos
                Inicialize_Campos();

                // Cargamos los combobox
                Initialize_Comboboxs();

                Initialize_Formulario(solicitudAux);

            }
            else
            {
                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
            }

        }

        private void Inicialize_Campos()
        {

            PanelCodigoCentro.Visible = true;
            PanelNumeroDiarioOficial.Visible = true;
            PanelFechaDiarioOficial.Visible = true;
            PanelNumeroActadeEntrega.Visible = true;
            PanelFechaActaEnt.Visible = true;
            //PanelCapitaniaPuerto.Visible = true;
            PanelPlazoNominal.Visible = true;
            PanelPlazoNominalInicio.Visible = true;
            PanelNumeroPlazo.Visible = true;
            PanelPlazoNominalVencimiento.Visible = true;
            PanelFechaInicioPeriodoAut.Visible = false;
            PanelMesesAut.Visible = false;
            PanelFechaFinPeriodoAut.Visible = false;


        }

        private void Initialize_Formulario(SolicitudConcesion solicitudInicial)
        {
            if (solicitudInicial != null && solicitudInicial.idSolConcesion > 0)
            {
                IdSolicitud.Value = Convert.ToString(solicitudInicial.idSolConcesion);

                UnidadEspacial unidadespacial = unidadEspacialDA.ObtieneUnidadEspacial(solicitudInicial.idSolConcesion, 0);

                if (unidadespacial != null)
                {
                    CodigoCentro.Text = Convert.ToString(unidadespacial.centrosDeCultivo.codigoCentro);

                    NumeroDiarioOficial.Text = Convert.ToString(unidadespacial.numeroDiarioOficial);
                    FechaDiarioOficial.Text = FechaUtils.formatearFecha(unidadespacial.fechaDiarioOficial);

                    if (unidadespacial.numeroActaEntrega > 0)
                    {
                        NumeroActaEntrega.Text = Convert.ToString(unidadespacial.numeroActaEntrega);
                    }

                    if (unidadespacial.fechaActaEntrega != null && unidadespacial.fechaActaEntrega != default(DateTime))
                    {
                        FechaActaEntrega.Text = FechaUtils.formatearFecha(unidadespacial.fechaActaEntrega);
                    }

                    //CapitaniaPuerto.SelectedValue = Convert.ToString(unidadespacial.capitaniaDePuerto.idCapitaDePuerto);
                    IdUnidadEspacial.Value = Convert.ToString(unidadespacial.idUnidadEspacial);

                    if (unidadespacial.tipoPlazoNominal != null && unidadespacial.tipoPlazoNominal.id > 0)
                    {
                        PlazoNominal.SelectedValue = Convert.ToString(unidadespacial.tipoPlazoNominal.id);
                    }
                    PlazoInicio.Text = FechaUtils.formatearFecha(unidadespacial.plazoInicio);

                    if (unidadespacial.numPlazo > 0)
                    {
                        NumeroPlazo.Text = Convert.ToString(unidadespacial.numPlazo);
                    }

                    PlazoVencimiento.Text = FechaUtils.formatearFecha(unidadespacial.plazoVencimiento);

                    if (FechaInicioPeriodo != null && unidadespacial.fechaInicioPerAut != default(DateTime))
                    {
                        FechaInicioPeriodo.Text = FechaUtils.formatearFecha(unidadespacial.fechaInicioPerAut);
                    }
                    if (unidadespacial.mesesAut > 0)
                    {
                        MesesAutorizados.Text = Convert.ToString(unidadespacial.mesesAut);
                    }
                    if (unidadespacial != null && unidadespacial.fechaFinPerAut != default(DateTime))
                    {
                        FechaFinPeriodo.Text = FechaUtils.formatearFecha(unidadespacial.fechaFinPerAut);
                    }


                    
                    
                }

            }
            else
            {
                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
            }

        }

        //MANEJO DE FECHAS
        protected void PlazoNominal_change(object sender, EventArgs e)
        {

            if (Convert.ToInt32(PlazoNominal.SelectedValue) == rbTipo.FECHA_EXACTA)
            {
                //campo Nº de Mes/Año
                NumeroPlazo.Text = "";
                PanelNumeroPlazo.Visible = false;
                UpdatePanelNumeroPlazo.Update();

                //icono de calendario fecha vencimiento
                PanelFechaVencimiento.Visible = true;

                //campos fecha vencimiento
                PlazoVencimiento.ReadOnly = false;
                PlazoVencimiento.CssClass = "";

                //panel fecha vencimiento
                UpdatePanelPlazoVencimiento.Update();

            }
            else
            {
                //campo Nº de Mes/Año
                PanelNumeroPlazo.Visible = true;
                UpdatePanelNumeroPlazo.Update();

                //icono de calendario fecha vencimiento
                PanelFechaVencimiento.Visible = false;

                //campos fecha vencimiento
                PlazoVencimiento.ReadOnly = true;
                PlazoVencimiento.CssClass = "campoDeshabilitado";

                //panel fecha vencimiento
                UpdatePanelPlazoVencimiento.Update();

            }

        }

        protected void Initialize_Comboboxs()
        {
            //Carga_Combobox("CapitaniaPuerto");
            //CapitaniaPuerto.SelectedValue = "-1";

            Carga_Combobox("PlazoNominal");
            PlazoNominal.SelectedValue = "-1";
        }

        private void Carga_Combobox(string combobox)
        {
            switch (combobox)
            {

                //case "CapitaniaPuerto":
                //    // Cargamos el combobox: CapitaniaPuerto
                //    CapitaniaPuerto.Items.Clear();
                //    CapitaniaPuerto.DataSource = capitaniaDePuertoDA.obtenerCapitaniaDePuerto(0);
                //    CapitaniaPuerto.DataTextField = "CapitaniaPuerto";
                //    CapitaniaPuerto.DataValueField = "IdCapitaniaPuerto";
                //    CapitaniaPuerto.DataBind();
                //    CapitaniaPuerto.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                //    break;
                case "PlazoNominal":
                    // Cargamos el combobox: PlazoNominal
                    PlazoNominal.Items.Clear();
                    PlazoNominal.DataSource = mantenedorGeneralService.listarPlazoNominal(new ParametroGenerico("TIPO_PLAZO"));
                    PlazoNominal.DataTextField = "descripcion";
                    PlazoNominal.DataValueField = "id";
                    PlazoNominal.DataBind();
                    PlazoNominal.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

            }
        }


        /**
        * GUARDAR UNA UNIDAD ESPACIAL (NO CREA LA CONCESION)
        */
        protected void GuardarUnidadEspacial_Click(object sender, ImageClickEventArgs e)
        {

            UnidadEspacialService unidadEspacialService = new UnidadEspacialService();
            UnidadEspacial unidadEspacial = new UnidadEspacial();

            unidadEspacial.idUnidadEspacial = Convert.ToInt32(IdUnidadEspacial.Value);
            unidadEspacial.idSolicitud = Convert.ToInt32(IdSolicitud.Value);
            unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
            unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToString(CodigoCentro.Text);
            unidadEspacial.numeroDiarioOficial = Convert.ToInt32(NumeroDiarioOficial.Text);
            unidadEspacial.fechaDiarioOficial = Convert.ToDateTime(FechaDiarioOficial.Text);
            //unidadEspacial.capitaniaDePuerto = new CapitaniaDePuerto();
            //unidadEspacial.capitaniaDePuerto.idCapitaDePuerto = Convert.ToInt32(CapitaniaPuerto.Text);

            if (NumeroActaEntrega.Text != null && !NumeroActaEntrega.Text.Equals(""))
            {
                unidadEspacial.numeroActaEntrega = Convert.ToInt32(NumeroActaEntrega.Text);
            }
            if (FechaActaEntrega != null && !FechaActaEntrega.Text.Equals(""))
            {
                unidadEspacial.fechaActaEntrega = Convert.ToDateTime(FechaActaEntrega.Text);
            }

            unidadEspacial.tipoPlazoNominal = new ParametroGenerico(Convert.ToInt32(PlazoNominal.SelectedItem.Value));
            unidadEspacial.plazoInicio = Convert.ToDateTime(PlazoInicio.Text);

            if (NumeroPlazo != null && !NumeroPlazo.Text.Equals(""))
            {
                unidadEspacial.numPlazo = Convert.ToInt32(NumeroPlazo.Text);
            }

            unidadEspacial.plazoVencimiento = Convert.ToDateTime(PlazoVencimiento.Text);

            if (FechaInicioPeriodo != null && !FechaInicioPeriodo.Text.Equals(""))
            {
                unidadEspacial.fechaInicioPerAut = Convert.ToDateTime(FechaInicioPeriodo.Text);
            }
            if (MesesAutorizados != null && !MesesAutorizados.Text.Equals(""))
            {
                unidadEspacial.mesesAut = Convert.ToInt32(MesesAutorizados.Text);
            }
            if (FechaFinPeriodo != null && !FechaFinPeriodo.Text.Equals(""))
            {
                unidadEspacial.fechaFinPerAut = Convert.ToDateTime(FechaFinPeriodo.Text);
            }

            unidadEspacial.accion = accion.MODIFICAR;

            List<String> listaErroresUnidadEspacial = unidadEspacialValidacion.validaUnidadEspacialConcesion(unidadEspacial);

            if (listaErroresUnidadEspacial != null && listaErroresUnidadEspacial.Count <= 0)
            {
                bool resp = unidadEspacialService.guardaUnidadEspacial(unidadEspacial, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                if (resp)
                {

                    IdUnidadEspacial.Value = Convert.ToString(unidadEspacial.idUnidadEspacial);

                    msgGrillaGral_1.Text = "Se ha guardado exitosamente la unidad espacial.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;

                }
                else
                {
                    msgGrillaGral_1.Text = "Ha ocurrido un error al intentar guardar la unidad espacial.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                }
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            }
            else
            {
                foreach (String error in listaErroresUnidadEspacial)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
            }
        }

        protected void MesesAutorizados_TextChanged(object sender, EventArgs e)
        {
            if (!MesesAutorizados.Text.Trim().Equals("") && Convert.ToInt32(MesesAutorizados.Text.Trim()) > 0 && FechaInicioPeriodo != null && !FechaInicioPeriodo.Text.Trim().Equals(""))
            {

                DateTime fechaInicioAux = Convert.ToDateTime(FechaInicioPeriodo.Text);
                fechaInicioAux = fechaInicioAux.AddMonths(Convert.ToInt32(MesesAutorizados.Text.Trim()));

                FechaFinPeriodo.Text = FechaUtils.formatearFecha(fechaInicioAux);
                UpdatePanelFechaFinPeriodo.Update();
            }

        }

        protected void NumeroPlazo_TextChanged(object sender, EventArgs e)
        {
            if (!NumeroPlazo.Text.Trim().Equals("") && Convert.ToInt32(NumeroPlazo.Text.Trim()) > 0 && PlazoInicio != null && !PlazoInicio.Text.Trim().Equals(""))
            {

                DateTime fechaInicioAux = Convert.ToDateTime(PlazoInicio.Text);

                if (PlazoNominal.SelectedItem != null && Convert.ToInt32(PlazoNominal.SelectedItem.Value) == rbTipo.TIPO_PLAZO_DIAS)
                {
                    fechaInicioAux = fechaInicioAux.AddDays(Convert.ToInt32(NumeroPlazo.Text.Trim()));
                }
                else if (PlazoNominal.SelectedItem != null && Convert.ToInt32(PlazoNominal.SelectedItem.Value) == rbTipo.TIPO_PLAZO_MESES)
                {
                    fechaInicioAux = fechaInicioAux.AddMonths(Convert.ToInt32(NumeroPlazo.Text.Trim()));
                }
                else if (PlazoNominal.SelectedItem != null && Convert.ToInt32(PlazoNominal.SelectedItem.Value) == rbTipo.TIPO_PLAZO_ANIOS)
                {
                    fechaInicioAux = fechaInicioAux.AddYears(Convert.ToInt32(NumeroPlazo.Text.Trim()));
                }
                PlazoVencimiento.Text = FechaUtils.formatearFecha(fechaInicioAux);
                UpdatePanelPlazoVencimiento.Update();
            }

        }

        protected void LimpiarConcesion_Click(object sender, ImageClickEventArgs e)
        {
            CodigoCentro.Text = "";
            NumeroDiarioOficial.Text = "";
            FechaInicioPeriodo.Text = "";
            NumeroActaEntrega.Text = "";
            FechaActaEntrega.Text = "";
            //CapitaniaPuerto.SelectedValue = "-1";
            PlazoNominal.SelectedValue = "-1";
            PlazoInicio.Text = "";
            PlazoVencimiento.Text = "";
            FechaInicioPeriodo.Text = "";
        }
    }
}