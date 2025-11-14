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
using SubPesca.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;
using SubPesca.Mantenedores.Generales;

namespace SubPesca.Solicitudes.ModificacionCentroAcopio
{
    public partial class unidadesEspacialesModificacionCentroAcopio : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        SolicitudDA solicitudDA = new SolicitudDA();
        UnidadEspacialDA unidadEspacialDA = new UnidadEspacialDA();
        CapitaniaDePuertoDA capitaniaDePuertoDA = new CapitaniaDePuertoDA();
        TipoDA tipoDa = new TipoDA();
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();
        UnidadEspacialValidacion unidadEspacialValidacion = new UnidadEspacialValidacion();
        UnidadEspacialService unidadEspacialService = new UnidadEspacialService();
        SolicitudConcesionService solicitudConcesionService = new SolicitudConcesionService();
        PermisosService permisosService = new PermisosService();
        MantenedorGeneralService mantenedorGeneralService = new MantenedorGeneralService();

        EnviarCorreo enviarCorreo = new EnviarCorreo();

        protected void setearModulo()
        {
            ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
            ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
            ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
            ViewState["solicitudSession"] = paginas.solicitudModificacionCentroAcopioSession;

            SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];


            if (solicitudConcesion != null)
            {

                int[] tiposModificacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

                int contador = 0;
                foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                {
                    if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE)
                    {
                        tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_ESPACIAL_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                        contador++;
                    }
                    if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_RENOVACION)
                    {
                        tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_ESPACIAL_MOD_CENTRO_ACOPIO_ESPECIE;
                        contador++;
                    }
                    if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_PT_ESPECIE)
                    {
                        tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_ESPACIAL_MOD_CENTRO_ACOPIO_PT;
                        contador++;
                    }
                    if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE)
                    {
                        tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_ESPACIAL_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                        contador++;
                    }
                    if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REGULARIZACION)
                    {
                        tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_ESPACIAL_MOD_CENTRO_ACOPIO_REGULARIZACION;
                        contador++;
                    }
                }

                ViewState["SECCION_ESPECIFICA"] = tiposModificacion;

            }
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

                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {
                    CodigoCentroReadOnly.Enabled = true;

                    NumeroDiarioOficial.Enabled = true;
                    FechaDiarioOficial.Enabled = true;
                    PanelCalendarioFechaDiarioOficial.Visible = true;

                    NumeroActaEntrega.Enabled = true;
                    FechaActaEntrega.Enabled = true;
                    PanelCalendarioFechaActaEntrega.Visible = true;

                    //CapitaniaPuerto.Enabled = true;

                    //ImagenFecha1.Visible = true;

                    //ImagenFecha2.Visible = true;

                    PanelBotonGuardar.Visible = true;
                    PanelBotonCreacion.Visible = true;

                    PlazoNominal.Enabled = true;

                    PlazoInicio.Enabled = true;
                    PanelPlazoInicio.Visible = true;

                    PlazoVencimiento.Enabled = true;
                    PanelPlazoNominalVencimiento.Visible = true;
                }
                else
                {
                    CodigoCentroReadOnly.Enabled = false;

                    NumeroDiarioOficial.Enabled = false;
                    FechaDiarioOficial.Enabled = false;
                    PanelCalendarioFechaDiarioOficial.Visible = false;

                    NumeroActaEntrega.Enabled = false;
                    FechaActaEntrega.Enabled = false;
                    PanelCalendarioFechaActaEntrega.Visible = false;

                    //CapitaniaPuerto.Enabled = false;

                    //ImagenFecha1.Visible = false;

                    //ImagenFecha2.Visible = false;

                    PanelBotonGuardar.Visible = false;
                    PanelBotonCreacion.Visible = false;

                    PlazoNominal.Enabled = false;

                    PlazoInicio.Enabled = false;
                    PanelPlazoInicio.Visible = false;

                    PlazoVencimiento.Enabled = false;
                    PanelPlazoNominalVencimiento.Visible = false;
                }

                // Inicializamos el formulario
                Initialize_Form();

                PanelCodigoCentro.Visible = true;
                UpdatePanelCodigoCentro.Update();

                if (solicitudConcesion.numPert != null && !solicitudConcesion.numPert.Equals("0")) //Si no es regularización, se debe mostrar diario oficial.
                {
                    PanelNumeroDiarioOficial.Visible = true;
                    UpdatePanelNumeroDiarioOficial.Update();

                    PanelFechaDiarioOficial.Visible = true;
                    UpdatePanelFechaDiarioOficial.Update();
                }
                else {
                    PanelNumeroDiarioOficial.Visible = false;
                    UpdatePanelNumeroDiarioOficial.Update();

                    PanelFechaDiarioOficial.Visible = false;
                    UpdatePanelFechaDiarioOficial.Update();
                }

                PanelNumeroActaEntrega.Visible = false;
                UpdatePanelNumeroActaEntrega.Update();

                PanelFechaActaEntrega.Visible = false;
                UpdatePanelFechaActaEntrega.Update();

                //if (solicitudConcesion.numPert != null && !solicitudConcesion.numPert.Equals("0")) //Si no es regularización, se debe mostrar la capitanía.
                //{
                //    PanelCapitaniaDePuerto.Visible = true;
                //    UpdatePanelCapitaniaDePuerto.Update();
                //}
                //else
                //{
                //    PanelCapitaniaDePuerto.Visible = false;
                //    UpdatePanelCapitaniaDePuerto.Update();
                //}

                PanelPlazoNominal.Visible = false;
                UpdatePanelPlazoNominal.Update();

                PanelPlazoNominalInicio.Visible = false;
                UpdatePanelPlazoNominalInicio.Update();

                PanelPlazoNominalVencimiento.Visible = false;
                UpdatePanelPlazoVencimiento.Update();

                PanelFechaInicioPeriodoAut.Visible = false;
                UpdatePanelFechaInicioPeriodoAut.Update();

                PanelMesesAut.Visible = false;
                UpdatePanelMesesAut.Update();

                PanelFechaFinPeriodoAut.Visible = false;
                UpdatePanelFechaFinPeriodoAut.Update();


                //MANEJO DE FECHAS
                if (solicitudConcesion != null)
                {
                    foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                    {
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_RENOVACION)
                        {

                            PanelPlazoNominal.Visible = true;
                            UpdatePanelPlazoNominal.Update();

                            PanelPlazoNominalInicio.Visible = true;
                            UpdatePanelPlazoNominalInicio.Update();

                            PanelNumeroPlazo.Visible = true;
                            UpdatePanelNumeroPlazo.Update();

                            PanelPlazoNominalVencimiento.Visible = true;
                            UpdatePanelPlazoVencimiento.Update();

                            PlazoNominal_change(null, null);

                            break;
                        }
                    }
                }
            }
        }


        //MANEJO DE FECHAS
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


        protected void Initialize_Form()
        {
            SolicitudConcesion solicitudAux = (SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
            if (solicitudAux != null && solicitudAux.idSolConcesion > 0)
            {

                IdSolicitud.Value = Convert.ToString(solicitudAux.idSolConcesion);

                // Cargamos los combobox
                Initialize_Comboboxs();

                Initialize_Formulario(solicitudAux);


                if (solicitudConcesionService.aplicaBotonCreaModUnidadEspacial(solicitudAux.idSolConcesion))
                {
                    PanelBotonCreacion.Visible = true;
                }
                else
                {
                    PanelBotonCreacion.Visible = false;

                }
            }
            else
            {
                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
            }

        }

        private void Initialize_Formulario(SolicitudConcesion solicitudInicial)
        {
            if (solicitudInicial != null && solicitudInicial.idSolConcesion > 0)
            {
                IdSolicitud.Value = Convert.ToString(solicitudInicial.idSolConcesion);

                UnidadEspacial unidadespacial = unidadEspacialDA.ObtieneUnidadEspacialMod(solicitudInicial.idSolConcesion, 0);

                if (unidadespacial != null)
                {
                    CodigoCentroReadOnly.Text = Convert.ToString(unidadespacial.centrosDeCultivo.codigoCentro);

                    if (unidadespacial.numeroDiarioOficial > 0)
                    {
                        NumeroDiarioOficial.Text = Convert.ToString(unidadespacial.numeroDiarioOficial);
                    }

                    if (unidadespacial.fechaDiarioOficial != null && unidadespacial.fechaDiarioOficial != default(DateTime))
                    {
                        FechaDiarioOficial.Text = FechaUtils.formatearFecha(unidadespacial.fechaDiarioOficial);
                    }

                    if (unidadespacial.numeroActaEntrega > 0)
                    {
                        NumeroActaEntrega.Text = Convert.ToString(unidadespacial.numeroActaEntrega);
                    }

                    if (unidadespacial.fechaActaEntrega != null && unidadespacial.fechaActaEntrega != default(DateTime))
                    {
                        FechaActaEntrega.Text = FechaUtils.formatearFecha(unidadespacial.fechaActaEntrega);
                    }

                    //if (unidadespacial.capitaniaDePuerto != null && unidadespacial.capitaniaDePuerto.idCapitaDePuerto > 0)
                    //{
                    //    CapitaniaPuerto.SelectedValue = Convert.ToString(unidadespacial.capitaniaDePuerto.idCapitaDePuerto);
                    //}

                    /* Campos Plazo Nominal */
                    if (unidadespacial.tipoPlazoNominal != null && unidadespacial.tipoPlazoNominal.id > 0)
                    {
                        PlazoNominal.SelectedValue = Convert.ToString(unidadespacial.tipoPlazoNominal.id);
                    }

                    if (unidadespacial.plazoInicio != null && unidadespacial.plazoInicio != default(DateTime))
                    {
                        PlazoInicio.Text = FechaUtils.formatearFecha(unidadespacial.plazoInicio);
                    }

                    if (unidadespacial.numPlazo > 0)
                    {
                        NumeroPlazo.Text = Convert.ToString(unidadespacial.numPlazo);
                    }

                    if (unidadespacial.plazoVencimiento != null && unidadespacial.plazoVencimiento != default(DateTime))
                    {
                        PlazoVencimiento.Text = FechaUtils.formatearFecha(unidadespacial.plazoVencimiento);
                    }

                }

                ViewState["unidadEspacial"] = unidadespacial;
            }
            else
            {
                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
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




        protected void LimpiarConcesion_Click(object sender, ImageClickEventArgs e)
        {
            CodigoCentroReadOnly.Text = "";
            NumeroDiarioOficial.Text = "";
            FechaDiarioOficial.Text = "";
            NumeroActaEntrega.Text = "";
            FechaActaEntrega.Text = "";
            //CapitaniaPuerto.SelectedValue = "-1";
            PlazoNominal.SelectedValue = "-1";
            PlazoInicio.Text = "";
            PlazoVencimiento.Text = "";
        }


        protected void GuardarUnidadEspacial_Click(object sender, EventArgs e)
        {
            /*
            UnidadEspacial unidadEspacial = (UnidadEspacial)ViewState["unidadEspacial"];

            if (unidadEspacial == null)
            {
                unidadEspacial = new UnidadEspacial();
            }
             * */

            UnidadEspacial unidadEspacial = new UnidadEspacial();

            unidadEspacial.idSolicitud = Convert.ToInt32(IdSolicitud.Value);
            unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();

            unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToString(CodigoCentroReadOnly.Text);

            if (NumeroDiarioOficial != null && !NumeroDiarioOficial.Text.Equals(""))
            {
                unidadEspacial.numeroDiarioOficial = Convert.ToInt32(NumeroDiarioOficial.Text);
            }

            if (FechaDiarioOficial != null && !FechaDiarioOficial.Text.Equals(""))
            {
                unidadEspacial.fechaDiarioOficial = Convert.ToDateTime(FechaDiarioOficial.Text);
            }

            if (NumeroActaEntrega != null && !NumeroActaEntrega.Text.Equals(""))
            {
                unidadEspacial.numeroActaEntrega = Convert.ToInt32(NumeroActaEntrega.Text);
            }
            if (FechaActaEntrega != null && !FechaActaEntrega.Text.Equals(""))
            {
                unidadEspacial.fechaActaEntrega = Convert.ToDateTime(FechaActaEntrega.Text);
            }

            //unidadEspacial.capitaniaDePuerto = new CapitaniaDePuerto();
            //unidadEspacial.capitaniaDePuerto.idCapitaDePuerto = Convert.ToInt32(CapitaniaPuerto.SelectedValue);

            unidadEspacial.tipoPlazoNominal = new ParametroGenerico(Convert.ToInt32(PlazoNominal.SelectedItem.Value));

            if (PlazoInicio != null && !PlazoInicio.Text.Equals(""))
            {
                unidadEspacial.plazoInicio = Convert.ToDateTime(PlazoInicio.Text);
            }

            if (PlazoVencimiento != null && !PlazoVencimiento.Text.Equals(""))
            {
                unidadEspacial.plazoVencimiento = Convert.ToDateTime(PlazoVencimiento.Text);
            }

            if (NumeroPlazo != null && !NumeroPlazo.Text.Equals(""))
            {
                unidadEspacial.numPlazo = Convert.ToInt32(NumeroPlazo.Text);
            }

            List<String> listaErroresUnidadEspacial = unidadEspacialValidacion.validaUnidadEspacialAcopio(unidadEspacial);


            if (listaErroresUnidadEspacial != null && listaErroresUnidadEspacial.Count <= 0)
            {

                //SE GUARDA LA UNIDAD ESPACIAL 
                bool resp = unidadEspacialService.guardaUnidadEspacialModificacion(unidadEspacial, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                if (resp)
                {

                    ViewState["unidadEspacial"] = unidadEspacial;

                    msgGrillaGral_1.Text = "Se ha guardado la unidad espacial exitosamente.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                    UpdatePanelMensajesSuperior.Update();

                }
                else
                {
                    msgGrillaGral_1.Text = "Ha ocurrido un error al intentar guardar la unidad espacial.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                    UpdatePanelMensajesSuperior.Update();
                }
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            }
            else
            {
                foreach (String error in listaErroresUnidadEspacial)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }

                UpdatePanelMensajesSuperior.Update();
            }
        }


        /**
      * Método que que guarda los datos de la concesión de acuicultura y la crea.
      */
        protected void CrearConcesion_Click(object sender, ImageClickEventArgs e)
        {
            UnidadEspacialService unidadEspacialService = new UnidadEspacialService();

            /*
            UnidadEspacial unidadEspacial = (UnidadEspacial)ViewState["unidadEspacial"];

            if (unidadEspacial == null)
            {
                unidadEspacial = new UnidadEspacial();
            }
             * */

            UnidadEspacial unidadEspacial = new UnidadEspacial();

            unidadEspacial.idSolicitud = Convert.ToInt32(IdSolicitud.Value);
            unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();

            unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToString(CodigoCentroReadOnly.Text);

            if (NumeroDiarioOficial != null && !NumeroDiarioOficial.Text.Equals(""))
            {
                unidadEspacial.numeroDiarioOficial = Convert.ToInt32(NumeroDiarioOficial.Text);
            }

            if (FechaDiarioOficial != null && !FechaDiarioOficial.Text.Equals(""))
            {
                unidadEspacial.fechaDiarioOficial = Convert.ToDateTime(FechaDiarioOficial.Text);
            }

            if (NumeroActaEntrega != null && !NumeroActaEntrega.Text.Equals(""))
            {
                unidadEspacial.numeroActaEntrega = Convert.ToInt32(NumeroActaEntrega.Text);
            }
            if (FechaActaEntrega != null && !FechaActaEntrega.Text.Equals(""))
            {
                unidadEspacial.fechaActaEntrega = Convert.ToDateTime(FechaActaEntrega.Text);
            }

            //unidadEspacial.capitaniaDePuerto = new CapitaniaDePuerto();
            //unidadEspacial.capitaniaDePuerto.idCapitaDePuerto = Convert.ToInt32(CapitaniaPuerto.SelectedValue);

            unidadEspacial.tipoPlazoNominal = new ParametroGenerico(Convert.ToInt32(PlazoNominal.SelectedItem.Value));

            if (PlazoInicio != null && !PlazoInicio.Text.Equals(""))
            {
                unidadEspacial.plazoInicio = Convert.ToDateTime(PlazoInicio.Text);
            }

            if (PlazoVencimiento != null && !PlazoVencimiento.Text.Equals(""))
            {
                unidadEspacial.plazoVencimiento = Convert.ToDateTime(PlazoVencimiento.Text);
            }

            if (NumeroPlazo != null && !NumeroPlazo.Text.Equals(""))
            {
                unidadEspacial.numPlazo = Convert.ToInt32(NumeroPlazo.Text);
            }

            SolicitudConcesion solicitudCentroDeAmerb = (SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
            solicitudCentroDeAmerb.unidadEspacial = unidadEspacial;

            //SE VALIDA LA UNIDAD ESPACIAL
            List<String> listaErroresUnidadEspacial = unidadEspacialValidacion.validaUnidadEspacialAcopio(solicitudCentroDeAmerb.unidadEspacial);

            /*
            //SE VALIDA LA CREACION COMO CONCESION
            if (listaErroresUnidadEspacial == null || listaErroresUnidadEspacial.Count() == 0)
            {
                listaErroresUnidadEspacial = unidadEspacialValidacion.validaCreacionConcesionAmerb(solicitudCentroDeAmerb);
            }
            */


            if (listaErroresUnidadEspacial != null && listaErroresUnidadEspacial.Count <= 0)
            {
                bool resp = unidadEspacialService.modificaCentroAcopioOriginal(unidadEspacial, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                if (resp)
                {

                    ViewState["unidadEspacial"] = unidadEspacial;

                    msgGrillaGral_1.Text = "Se ha modificado el centro de acopio exitosamente.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                    UpdatePanelMensajesSuperior.Update();
                    try
                    {
                        enviarCorreo.alertaCreacionCentroAcopio(unidadEspacial);
                    }
                    catch (Exception)
                    {

                    }
                }
                else
                {
                    msgGrillaGral_1.Text = "No se ha modificado el centro de acopio.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                    UpdatePanelMensajesSuperior.Update();
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
    }
}