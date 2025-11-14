using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Contantes;
using Datos.Entidades;
using Validaciones.cl.subpesca.rb.mantenedor;
using SubPesca.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.mantenedores;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using System.Drawing;
using Datos.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;

namespace SubPesca.Mantenedores.Titulares
{
    public partial class agregarTitular : System.Web.UI.Page
    {

        TipoDA tipoDa = new TipoDA();
        SolicitanteDA solicitanteDA = new SolicitanteDA();
        SolicitanteService solicitanteService = new SolicitanteService();
        MatrizSucursalDA matrizSucursalDA = new MatrizSucursalDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();
        OperadorDA operadorDA = new OperadorDA();
        RepLegalDA representanteLegalDA = new RepLegalDA();
        MantenedorDA mantenedorDA = new MantenedorDA();

        MantenedorTitularesValidacion mantenedorTitularesValidacion = new MantenedorTitularesValidacion();
        MantenedorTitularService mantenedorTitularService = new MantenedorTitularService();
        
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {
                ObtencionParametros();
                InicializarCombobox();
                InicializarFormulario();
            }
        }

        private void ObtencionParametros()
        {
            // Se recibe el rutPersona y la pagina que invoco el formulario.
            try
            {
                if (Request.QueryString["acc"] != null)
                {
                    String acc = Request.QueryString["acc"];
                    ViewState["acc"] = acc;

                    /* Crear */
                    if (acc !=null && acc.Equals("1"))
                    {
                        String ini = Request.QueryString["ini"];
                        if (ini != null && ini.Equals("1"))
                        {
                            Session["Titular"] = null;
                        }
                    }

                    /* Modificar */
                    else if (acc != null && acc.Equals("2")) 
                    {
                        if (Request.QueryString["rutPersona"] != null) { 
                        
                            /* Se obtiene la persona ingresada en la base de datos */
                            int rutTitular = Convert.ToInt32(Request.QueryString["rutPersona"]);
                            Solicitante solicitante = solicitanteDA.ObtenerPersona(rutTitular,0);

                            if(solicitante != null){

                                //solicitante.esRPA = solicitanteService.TiularEsAPE(rutTitular);
                                solicitante.esRPA = solicitanteService.TitularesRPA(rutTitular);

                                /* Se obtienen los contactos del titular */
                                //solicitante.matrizSucursales = matrizSucursalDA.ListarTitularMatrizSuc(rutTitular);
                                
                                /* Se obtienen las direcciones del titular */
                                solicitante.matrizSucursales = mantenedorTitularService.listarMatrizConContactos(rutTitular);

                                /* Se obtienen los contactos del titular */
                                solicitante.listaContacto = mantenedorTitularService.listarContactosTitular(rutTitular);

                                /* Se obtienen los representantes legales asociados si es que aplican */
                                solicitante.listaRepresentanteLegal = representanteLegalDA.ListarRepresentanteTitular(rutTitular);

                                /* Se obtienen los operadores asociados si es que aplican */
                                solicitante.listaOperador = operadorDA.ListarOperadorTitular(rutTitular,0);

                                Session["Titular"] = (Solicitante) solicitante;
                            }
                        }

                    }

                    DespliegaObjetosInterfaz(Convert.ToInt32(acc));
                }
                else
                {

                }

            }
            catch
            {

            };
        }

        private void DespliegaObjetosInterfaz(int accion) { 
            
            /* Crear */
            if(accion == 1){

                LabelAccion.Text = "Crear";
                RutPersona.ReadOnly = false;
                
                BuscarSolicitante.Visible = true;
                EjemploRut.Visible = true;
                cambiarNombrePersonaNatural.Visible = false;
                VerNombresPersonaNatural.Visible = false;
                cambiarPersonaJuridica.Visible = false;
                VerNombrePersonaJuridica.Visible = false;
                GuardarTitular.Visible = true;
                ModificarTitular.Visible = false;

                //PanelGeneral.Visible = false;

            }

            /* Modificar */
            else if (accion == 2) {

                LabelAccion.Text = "Modificar";
                RutPersona.ReadOnly = true;
                RutPersona.BackColor = Color.FromArgb(221, 221, 238);

                BuscarSolicitante.Visible = false;
                EjemploRut.Visible = false;
                cambiarNombrePersonaNatural.Visible = true;
                VerNombresPersonaNatural.Visible = true;
                
                cambiarPersonaJuridica.Visible = true;
                VerNombrePersonaJuridica.Visible = true;
                
                GuardarTitular.Visible = false;
                ModificarTitular.Visible = true;

                //04-05-2018 El nombre del Titular puede ser modificado a través de la funcionalidad de Modificación de nombres
                //y a través de la edición del mismo campo.

                //NombrePersonaNatural.ReadOnly = true;
                //NombrePersonaJuridica.ReadOnly = true;
                //NombrePersonaNatural.BackColor = Color.FromArgb(221, 221, 238);
                //NombrePersonaJuridica.BackColor = Color.FromArgb(221, 221, 238);

                //PanelGeneral.Visible = true;

            }
        }

        private void InicializarCombobox()
        {
            CargarComboBox("Genero");
            Genero.SelectedValue = "-1";

            CargarComboBox("TipoPersonaJuridica");
            TipoPersonaJuridica.SelectedValue = "-1";

            CargarComboBox("Holding");
            Holding.SelectedValue = "-1";

            CargarComboBox("APE");
            APE.SelectedValue = "-1";
        }

        private void InicializarFormulario()
        {
            Solicitante solicitante = (Solicitante)Session["Titular"];

            if (solicitante != null)
            {
                RutPersona.Text = Convert.ToString(solicitante.rut +"-"+ solicitante.dv);
                RutPersona2.Text = Convert.ToString(solicitante.rut + "-" + solicitante.dv);

                if (solicitante.tipoPersona.id == rbTipo.PERSONA_NATURAL)
                {
                    NombrePersonaNatural.Text = solicitante.nombreSolicitante;
                    if (solicitante.genero)
                    {
                        Genero.SelectedValue = "1";
                    }
                    else {
                        Genero.SelectedValue = "0";
                    }

                    PanelPersonaNatural.Visible = true;
                    PanelHolding.Visible = true;
                    PanelAPE.Visible = true;
                    PanelRPA.Visible = true;

                    
                }

                else if (solicitante.tipoPersona.id == rbTipo.PERSONA_JURIDICA)
                {
                    TipoPersonaJuridica.SelectedValue = Convert.ToString(solicitante.subtipoPersona.id);
                    NombrePersonaJuridica.Text = solicitante.nombreSolicitante;

                    /* Se busca la persona jurídica asociada al rut ingresado por el usuario */
                    Solicitante solicitantePersonaJuridica = mantenedorTitularService.buscarPersonaJuridica(solicitante.rut, solicitante.dv);

                    if (solicitantePersonaJuridica != null)
                    {
                        NumeroRegistroSubpesca.Text = Convert.ToString(solicitantePersonaJuridica.numeroRegistroSubpesca);
                        FechaRegistroSubpesca.Text = FechaUtils.formatearFecha(solicitantePersonaJuridica.fechaRegistroSubpesca);

                        PanelSernapesca.Visible = true;
                    }

                    PanelPersonaJuridica.Visible = true;
                    PanelPersJuridica2.Visible = true;

                    PanelPersJuridica.Visible = true;
                    UpdatePanelPersJuridica.Update();
                    PanelHolding.Visible = true;
                    PanelAPE.Visible = true;
                    PanelRPA.Visible = true;


                }

                nombreTitular.Value = solicitante.nombreSolicitante;

                //if (solicitante.numeroControlIngreso > 0)
                //{
                //    NroControlIngreso.Text = Convert.ToString(solicitante.numeroControlIngreso);
                //}

                //if (solicitante.fechaControlIngreso != default(DateTime))
                //{
                //    FechaTextRecepcion.Text = FechaUtils.formatearFecha(solicitante.fechaControlIngreso);
                //}

                if (solicitante.holding != null && solicitante.holding.id > 0)
                {
                    Holding.SelectedValue = Convert.ToString(solicitante.holding.id);
                }

                if (solicitante.estadoAPE != null && solicitante.estadoAPE.id > 0)
                {
                    APE.SelectedValue = Convert.ToString(solicitante.estadoAPE.id);
                }


                /* RPA */
                if (solicitante.esRPA)
                {
                    RPA.Text = "Sí";
                }
                else {
                    RPA.Text = "No";
                }
               

                /* Se debe cargar la grilla de direcciones */
                if (solicitante.matrizSucursales != null && solicitante.matrizSucursales.Count > 0)
                {
                    cargarGrillaContactoDireccion(solicitante.matrizSucursales);
                }

                /* Se debe cargar la grilla de contactos */
                if (solicitante.listaContacto != null && solicitante.listaContacto.Count > 0)
                {
                    cargarGrillaContactos(solicitante.listaContacto);
                }

                /* Se debe cargar la grilla de representantes legales */
                if (solicitante.listaRepresentanteLegal != null && solicitante.listaRepresentanteLegal.Count > 0)
                {
                    cargarGrillaRepresentanteLegal(solicitante.listaRepresentanteLegal);
                }

                /* Se debe cargar la grilla operador */
                if (solicitante.listaOperador != null && solicitante.listaOperador.Count > 0)
                {
                    cargarGrillaOperador(solicitante.listaOperador);
                }

            }
            
        }

        private void cargarGrillaContactos(List<Contacto> listContactos)
        {
            GridViewContacto.DataSource = listContactos;
            GridViewContacto.DataBind();
            GridViewContacto.Visible = true;
        }

        private void cargarGrillaOperador(List<Operador> listOperadores)
        {
            GridViewOperadores.DataSource = listOperadores;
            GridViewOperadores.DataBind();
            GridViewOperadores.Visible = true;
        }

        private void cargarGrillaRepresentanteLegal(List<RepLegal> listRepresentantesLegales)
        {
            GridRepresentantesLegales.DataSource = listRepresentantesLegales;
            GridRepresentantesLegales.DataBind();
            GridRepresentantesLegales.Visible = true;
        }

        private void cargarGrillaContactoDireccion(List<MatrizSucursal> listMatrizSucursales)
        {
            GridContactoMatrizSucursales.DataSource = listMatrizSucursales;
            GridContactoMatrizSucursales.DataBind();
            GridContactoMatrizSucursales.Visible = true;
        }


        private void CargarComboBox(String combobox)
        {
            switch (combobox)
            {

                case "Genero":
                    // Cargamos el combobox: Genero
                    Genero.Items.Clear();
                    Genero.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    Genero.Items.Insert(1, new ListItem("Femenino", "1"));
                    Genero.Items.Insert(2, new ListItem("Masculino", "0"));
                    Genero.DataBind();
                    break;

                case "TipoPersonaJuridica":
                    // Cargamos el combobox: Tipo
                    TipoPersonaJuridica.Items.Clear();
                    TipoPersonaJuridica.DataSource = parametroGenericoDA.ListarTipoPersonaJuridica(new ParametroGenerico());
                    TipoPersonaJuridica.DataTextField = "descripcion";
                    TipoPersonaJuridica.DataValueField = "id";
                    TipoPersonaJuridica.DataBind();
                    TipoPersonaJuridica.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

                case "Holding":
                    // Cargamos el combobox: Holding
                    Holding.Items.Clear();
                    Holding.DataSource = mantenedorDA.ListarHolding_Mantenedor(new ParametroGenerico());
                    Holding.DataTextField = "descripcion";
                    Holding.DataValueField = "id";
                    Holding.DataBind();
                    Holding.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

                case "APE":
                    // Cargamos el combobox: APE
                    List<ParametroGenerico> auxList = new List<ParametroGenerico>();
                    auxList.Add(new ParametroGenerico(-1, "-- No Disponible --"));
                    auxList.Add(new ParametroGenerico(rbEstadosGenerales.SI, "Si"));
                    auxList.Add(new ParametroGenerico(rbEstadosGenerales.NO, "No"));
                    
                    APE.Items.Clear();
                    APE.DataSource = auxList;
                    APE.DataTextField = "descripcion";
                    APE.DataValueField = "id";
                    APE.DataBind();

                    break;
            }
        }

        /* Método que busca los datos de la persona en el sistema y los despliega en la interfaz */
        protected void ButtonRutPersona_Click(object sender, EventArgs e)
        {
                msgGrilla.Text = "";
                Content_msgGrilla.Visible = false;

                MantenedorTitularService mantenedorTitularService = new MantenedorTitularService();
                Solicitante solicitante = new Solicitante();

                String rutCompleto = Convert.ToString(RutPersona.Text);
                String[] rutPartes = rutCompleto.Split('-');

                solicitante.tipoPersona = new ParametroGenerico();
                solicitante.rut = Convert.ToInt32(rutPartes[0]);
                solicitante.dv = Convert.ToChar(rutPartes[1]);

                List<string> listaErroresRutTitular = mantenedorTitularesValidacion.validaRutTitular(solicitante.rut);

                if (listaErroresRutTitular.Count <= 0)
                {
                    if (solicitante.rut < 50000000)
                    {
                        solicitante.tipoPersona.id = rbTipo.PERSONA_NATURAL;
                    }
                    else
                    {

                        solicitante.tipoPersona.id = rbTipo.PERSONA_JURIDICA;
                    }

                    if (solicitante.tipoPersona.id == rbTipo.PERSONA_NATURAL)
                    {
                        PanelPersonaNatural.Visible = true;
                        PanelPersonaJuridica.Visible = false;

                        PanelPersJuridica.Visible = false;
                        UpdatePanelPersJuridica.Update();

                        PanelPersJuridica2.Visible = false;

                        PanelHolding.Visible = true;
                        PanelAPE.Visible = true;

                    }
                    else if (solicitante.tipoPersona.id == rbTipo.PERSONA_JURIDICA)
                    {

                        /* Se busca la persona jurídica asociada al rut ingresado por el usuario */
                        solicitante = mantenedorTitularService.buscarPersonaJuridica(solicitante.rut, solicitante.dv);

                        if (solicitante != null)
                        {
                            if (solicitante.numeroRegistroSubpesca > 0)
                            {
                                NumeroRegistroSubpesca.Text = Convert.ToString(solicitante.numeroRegistroSubpesca);
                            }

                            if (solicitante.fechaRegistroSubpesca != null)
                            {
                                FechaRegistroSubpesca.Text = Convert.ToString(solicitante.fechaRegistroSubpesca);
                            }

                            PanelSernapesca.Visible = true;
                            RegistroCentralizado.Value = "true";
                            PanelPersonaJuridica.Visible = true;
                        }
                        else
                        {
                            msgGrilla.Text = "El Rut ingresado no se encuentra ingresado en el Registro de Personas de Subpesca.";
                            Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                            Content_msgGrilla.Visible = true;

                        }
                        PanelPersonaNatural.Visible = false;
                        PanelPersonaJuridica.Visible = true;

                        PanelPersJuridica.Visible = true;
                        UpdatePanelPersJuridica.Update();

                        PanelPersJuridica2.Visible = true;

                        PanelHolding.Visible = true;
                        PanelAPE.Visible = true;
                        PanelRPA.Visible = true;
                    }
                    else
                    {
                        PanelPersonaNatural.Visible = false;
                        PanelPersonaJuridica.Visible = true;
                    }
                    
                    RutPersona2.Text = Convert.ToString(rutCompleto);
                    guardarBorradorFormulario();
                }
                else
                {
                    foreach (String error in listaErroresRutTitular)
                    {
                        Page.Validators.Add(new ValidationError("grupo2", error));
                    }

                }
        }

        private void limpiarFormulario() {
            RutPersona.Text = "";
            limpiarFormularioPersonaNatural();
            limpiarFormularioPersonaJuridica();
        }

        private void limpiarFormularioPersonaNatural() {
            NombrePersonaNatural.Text = "";
            Genero.SelectedValue = "-1";
            Holding.SelectedValue = "-1";
            APE.SelectedValue = "-1";
        }

        private void limpiarFormularioPersonaJuridica()
        {
            TipoPersonaJuridica.SelectedValue = "-1";
            NombrePersonaJuridica.Text = "";
            //NroControlIngreso.Text = "";
            //FechaTextRecepcion.Text = "";
            Holding.SelectedValue = "-1";
            APE.SelectedValue = "-1";
        }

        /* Método que despliega la interfaz para agregar de Matriz y Sucursales a la Persona */
        protected void ImageButtonMatrizSucursal_Click(object sender, ImageClickEventArgs e)
        {
            guardarBorradorFormulario();

            string path = "~/Mantenedores/Titulares/agregarContactoDireccion.aspx?rutPersona=" + RutPersona.Text + "&bp=1&acc=" + ViewState["acc"].ToString();
            Response.Redirect(path);
        }

        /* Método que agregar un nuevo nombre a la Persona */
        protected void ImageButtonCambiarNombre_Click(object sender, ImageClickEventArgs e)
        {
            guardarBorradorFormulario();

            string path = "~/Mantenedores/Titulares/agregarNombres.aspx?rutPersona=" + RutPersona.Text + "&bp=1&acc=" + ViewState["acc"].ToString();
            Response.Redirect(path);
        }

        /* Método que despliega la interfaz para la asociación de Representante Legal a la Persona */
        protected void ImageRepresentanteLegal_Click(object sender, ImageClickEventArgs e)
        {
            guardarBorradorFormulario();

            string path = "~/Mantenedores/Titulares/asociarRepresentanteLegal.aspx?bp=1&acc=" + ViewState["acc"].ToString();
            Response.Redirect(path);
        }

        /* Método que despliega la interfaz para la asociación de Operador a la Persona */
        protected void ImageOperador_Click(object sender, ImageClickEventArgs e)
        {
            guardarBorradorFormulario();

            string path = "~/Mantenedores/Titulares/asociarOperador.aspx?bp=1&acc=" + ViewState["acc"].ToString();
            Response.Redirect(path);
        }

        /* Método que guarda la Persona */
        protected void GuardarTitular_Click(object sender, EventArgs e)
        {
            ButtonRutPersona_Click(null,null);
            
            msgGrilla.Text = "";
            Content_msgGrilla.Visible = false;

            guardarBorradorFormulario();
            Solicitante solicitante = (Solicitante)Session["Titular"];
            solicitante.idEstadoAsociacion = rbEstadosGenerales.VIGENTE;
            solicitante.accion = accion.INGRESAR;

            if (!RegistroCentralizado.Value.Equals(""))
            {
                solicitante.regCentralizado = Convert.ToBoolean(RegistroCentralizado.Value);
            }

            List<string> listaErroresRutTitular = mantenedorTitularesValidacion.validaTitular(solicitante);

            if (listaErroresRutTitular.Count <= 0)
            {
                
                bool resp = mantenedorTitularService.guardarTitular(solicitante);

                if (resp)
                {
                    Session["Titular"] = null;
                    string path = "~/Mantenedores/Titulares/administrarTitulares.aspx?acc=" + ViewState["acc"].ToString();
                    Response.Redirect(path);
                }
                else {

                    msgGrilla.Text = "No se ha guardado el Titular en el sistema.";
                    Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Content_msgGrilla.Visible = true;
                }
            }
            else {
                foreach (String error in listaErroresRutTitular)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
            }
        }

        private void guardarBorradorFormulario() {

            /* Se guardan los datos de la persona */
            Solicitante solicitante = (Solicitante) Session["Titular"];

            if (solicitante == null)
            {
                solicitante = new Solicitante();
            }

            String rutCompleto = Convert.ToString(RutPersona.Text);
            String[] rutPartes = rutCompleto.Split('-');

            solicitante.tipoPersona = new ParametroGenerico();
            solicitante.rut = Convert.ToInt32(rutPartes[0]);
            solicitante.dv = Convert.ToChar(rutPartes[1]);

            if (solicitante.rut < 50000000)
            {
                solicitante.tipoPersona.id = rbTipo.PERSONA_NATURAL;
            }
            else {

                solicitante.tipoPersona.id = rbTipo.PERSONA_JURIDICA;
            }


            if (solicitante.tipoPersona.id == rbTipo.PERSONA_NATURAL)
            {
                solicitante.nombreSolicitante = NombrePersonaNatural.Text;

                if (Genero.SelectedValue.Equals("1")) //Femenino
                {
                    solicitante.genero = true;
                }
                else
                {
                    solicitante.genero = false;
                }

            }
            else if (solicitante.tipoPersona.id == rbTipo.PERSONA_JURIDICA)
            {

                solicitante.subtipoPersona = new ParametroGenerico(Convert.ToInt32(TipoPersonaJuridica.SelectedValue));
                solicitante.nombreSolicitante = NombrePersonaJuridica.Text;

                if (PanelSernapesca.Visible)
                {
                    solicitante.numeroRegistroSubpesca = Convert.ToInt32(NumeroRegistroSubpesca.Text);
                    solicitante.fechaRegistroSubpesca = Convert.ToDateTime(FechaRegistroSubpesca.Text);
                }
            }

            //if (NroControlIngreso.Text != null && !NroControlIngreso.Text.Equals(""))
            //{
            //    solicitante.numeroControlIngreso = Convert.ToInt32(NroControlIngreso.Text);
            //}

            //if (FechaTextRecepcion.Text != null && !FechaTextRecepcion.Text.Equals(""))
            //{
            //    solicitante.fechaControlIngreso = Convert.ToDateTime(FechaTextRecepcion.Text);
            //}

            solicitante.nombreTitular = nombreTitular.Value;

            solicitante.holding = new ParametroGenerico();
            solicitante.holding.id = Convert.ToInt32(Holding.SelectedItem.Value);


            solicitante.estadoAPE = new ParametroGenerico();
            solicitante.estadoAPE.id = Convert.ToInt32(APE.SelectedItem.Value);

            /* aquí agregar el guardar rpa */

            Session["Titular"] = (Solicitante)solicitante;

        }

        /* Método que devuelve a la página de administración de personas */
        protected void Cancelar_Click(object sender, EventArgs e)
        {
            Session["Titular"] = null;
            string path = "~/Mantenedores/Titulares/administrarTitulares.aspx";
            Response.Redirect(path);
        }

        protected void GridContactoMatrizSucursales_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridContacto = (GridView)sender;

                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Listado de Direcciones";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridContactoMatrizSucursales.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        protected void GridViewContacto_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridContacto = (GridView)sender;

                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Listado de Contactos";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridViewContacto.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        protected void GridContactoMatrizSucursales_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GridRepresentantesLegales_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridRepresentante = (GridView)sender;

                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Listado de Representantes Legales";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridRepresentante.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        protected void GridTitulares_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                /*
                //Ver
                ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                if (boton_descargar != null)
                {
                    boton_descargar.Visible = true;
                   
                }*/

                //Eliminar
                ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
                if (boton_borrar != null)
                {
                    boton_borrar.Visible = true;

                }

            }
        }

        protected void GridViewContacto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                /* Acción */
                HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
                if (hidden_accion != null && !hidden_accion.Value.Equals("") && (Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR || Convert.ToInt32(hidden_accion.Value) == accion.IGNORAR))
                {
                    e.Row.Attributes["style"] = "display:none";
                };

                //Eliminar
                ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
                if (boton_borrar != null)
                {
                    boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar el Contacto?')");
                    boton_borrar.Visible = true;

                }

            }
        }

        protected void GridContactoMatrizSucursales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                /* Acción */
                HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
                if (hidden_accion != null && !hidden_accion.Value.Equals("") && (Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR || Convert.ToInt32(hidden_accion.Value) == accion.IGNORAR))
                {
                    e.Row.Attributes["style"] = "display:none";
                };
                
                /* Eliminar */
                ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
                if (boton_borrar != null)
                {
                    boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar la Dirección?')");
                    boton_borrar.Visible = true;

                }

                ///* Descargar */
                //String idArchivoBinario = DataBinder.Eval(e.Row.DataItem, "idArchivoBinario").ToString();
                //ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                //if (boton_descargar != null && idArchivoBinario != null && !idArchivoBinario.Equals("") && Convert.ToInt32(idArchivoBinario) > 0)
                //{
                //    boton_descargar.Visible = true;
                //};

                ///* Ver */
                //String idMatrizSuc = DataBinder.Eval(e.Row.DataItem, "idMatrizSuc").ToString();
                //ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                //if (boton_ver != null && idMatrizSuc != null && !idMatrizSuc.Equals("") && Convert.ToInt32(idMatrizSuc) > 0)
                //{
                //    boton_ver.Visible = true;

                //}

            }
        }

        protected void GridContactoMatrizSucursales_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int idMatrizSuc = Convert.ToInt32(arg[0]);
            int index = Convert.ToInt32(arg[1]);

            Solicitante solicitante = (Solicitante)Session["Titular"];
            
            switch (e.CommandName)
            {
                //case "Ver":

                //    Response.Redirect("~/Mantenedores/Titulares/verContactoTitular.aspx?idMatrizSuc=" + idMatrizSuc + "&index=" + index + "&acc=" + ViewState["acc"].ToString());

                //    break;

                //case "Descargar":

                //        /*
                //        ArchivoBinarioEspecial archivoBinarioEspecial = null;

                //        listaMatrizSucursal = solicitante.matrizSucursales;
                //        MatrizSucursal matrizSucursal = null;

                //        int i = 1;
                //        foreach (MatrizSucursal matrizSucursalSession in listaMatrizSucursal)
                //        {
                //            if (i == index)
                //            {
                //                matrizSucursal = matrizSucursalSession;
                //            }
                //            i++;
                //        }
                        
                //        archivoBinarioEspecial = matrizSucursal.archivoBinarioEspecial;
                      
                //         */

                //        List<MatrizSucursal> List_MatrizSucursales = solicitante.matrizSucursales;
                //        ArchivoBinarioEspecial archivoBinarioEspecial = null;

                //        foreach (MatrizSucursal matrizSucursal in List_MatrizSucursales)
                //        {
                //            if (matrizSucursal.index.Equals(index))
                //            {
                //                archivoBinarioEspecial = matrizSucursal.archivoBinarioEspecial;
                //            }
                //        }

                //        Response.Clear();
                //        Response.Buffer = true;
                //        Response.Charset = "";
                //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //        Response.ContentType = "application/" + archivoBinarioEspecial.formato;
                //        Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinarioEspecial.nombreArchivo + "." + archivoBinarioEspecial.formato);
                //        Response.BinaryWrite(archivoBinarioEspecial.bytes);
                //        Response.Flush();
                //        Response.End();

                //    break;

                case "Eliminar":
                        /*
                        listaMatrizSucursal = solicitante.matrizSucursales;
                        
                        foreach (MatrizSucursal matrizSucursalSession in listaMatrizSucursal)
                        {
                            if (matrizSucursalSession.index.Equals(Convert.ToInt32(index)))
                            {
                                listaMatrizSucursal.Remove(matrizSucursalSession);
                                break;
                            }
                        }

                        GridContactoMatrizSucursales.DataSource = listaMatrizSucursal;
                        GridContactoMatrizSucursales.DataBind();

                        solicitante.matrizSucursales = listaMatrizSucursal;
                        Session["Titular"] = (Solicitante) solicitante;
                        */

                        GridContactoMatrizSucursales.EditIndex = -1;
                        EliminarGrilla(index, "MatrizSucursal", solicitante);
                    
                    
                    break;
            }
        }

        protected void GridViewContacto_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int idMatrizSuc = Convert.ToInt32(arg[0]);
            int index = Convert.ToInt32(arg[1]);

            Solicitante solicitante = (Solicitante)Session["Titular"];

            switch (e.CommandName)
            {
                case "Eliminar":
                    GridViewContacto.EditIndex = -1;
                    EliminarGrilla(index, "Contacto", solicitante);


                    break;
            }
        }

        private void EliminarGrilla(int index, string tipoGrilla, Solicitante solicitante)
        {
            switch (tipoGrilla)
            {
                case "MatrizSucursal":

                    List<MatrizSucursal> List_MatrizSucursal = solicitante.matrizSucursales;

                    foreach (MatrizSucursal matrizSucursal in List_MatrizSucursal)
                    {
                        if (matrizSucursal.index.Equals(index))
                        {

                            List<String> listaErroresEspAu = mantenedorTitularesValidacion.validaEliminacionSucursalMatrizSucursal(matrizSucursal);

                            if (listaErroresEspAu.Count <= 0)
                            {
                                if (matrizSucursal.accion == accion.INGRESAR)
                                {
                                    matrizSucursal.accion = accion.IGNORAR;
                                    GridContactoMatrizSucursales.Rows[index].Attributes["style"] = "display:none";
                                }
                                if (matrizSucursal.accion == accion.LISTADO)
                                {
                                    matrizSucursal.accion = accion.ELIMINAR;
                                    GridContactoMatrizSucursales.Rows[index].Attributes["style"] = "display:none";
                                }
                            }
                            else
                            {

                                foreach (String error in listaErroresEspAu)
                                {
                                    Page.Validators.Add(new ValidationError("grupo1", error));
                                }

                                break;
                            }
                        }
                    }

                    solicitante.matrizSucursales = List_MatrizSucursal;
                    Session["Titular"] = (Solicitante) solicitante;

                    cargarGrillaContactoDireccion(List_MatrizSucursal);

                    break;

                case "Contacto":

                    List<Contacto> List_Contacto = solicitante.listaContacto;

                    foreach (Contacto contacto in solicitante.listaContacto)
                    {
                        if (contacto.index.Equals(index))
                        {

                            List<String> listaErroresEspAu = mantenedorTitularesValidacion.validaEliminacionContacto(contacto);

                            if (listaErroresEspAu.Count <= 0)
                            {
                                if (contacto.accion == accion.INGRESAR)
                                {
                                    contacto.accion = accion.IGNORAR;
                                    GridViewContacto.Rows[index].Attributes["style"] = "display:none";
                                }
                                if (contacto.accion == accion.LISTADO)
                                {
                                    contacto.accion = accion.ELIMINAR;
                                    GridViewContacto.Rows[index].Attributes["style"] = "display:none";
                                }
                            }
                            else
                            {

                                foreach (String error in listaErroresEspAu)
                                {
                                    Page.Validators.Add(new ValidationError("grupo1", error));
                                }

                                break;
                            }
                        }
                    }

                    solicitante.listaContacto = List_Contacto;
                    Session["Titular"] = (Solicitante)solicitante;

                    cargarGrillaContactos(List_Contacto);

                    break;

                case "RepresentanteLegal":

                    List<RepLegal> List_RepresentanteLegal = solicitante.listaRepresentanteLegal;

                    foreach (RepLegal repLegal in List_RepresentanteLegal)
                    {
                        if (repLegal.index.Equals(index))
                        {

                            List<String> listaErroresRepresentanteLegal = mantenedorTitularesValidacion.validaEliminacionRepresentanteLegal(repLegal);

                            if (listaErroresRepresentanteLegal.Count <= 0)
                            {
                                if (repLegal.accion == accion.INGRESAR)
                                {
                                    repLegal.accion = accion.IGNORAR;
                                    GridRepresentantesLegales.Rows[index].Attributes["style"] = "display:none";
                                }
                                if (repLegal.accion == accion.LISTADO)
                                {
                                    repLegal.accion = accion.ELIMINAR;
                                    GridRepresentantesLegales.Rows[index].Attributes["style"] = "display:none";
                                }
                            }
                            else
                            {

                                foreach (String error in listaErroresRepresentanteLegal)
                                {
                                    Page.Validators.Add(new ValidationError("grupo1", error));
                                }

                                break;
                            }
                        }
                    }

                    solicitante.listaRepresentanteLegal = List_RepresentanteLegal;
                    Session["Titular"] = (Solicitante)solicitante;

                    cargarGrillaRepresentanteLegal(List_RepresentanteLegal);

                    break;

                case "Operador":

                    List<Operador> List_Operador = solicitante.listaOperador;

                    foreach (Operador operador in List_Operador)
                    {
                        if (operador.index.Equals(index))
                        {

                            List<String> listaErroresEspAu = mantenedorTitularesValidacion.validaEliminacionOperador(operador);

                            if (listaErroresEspAu.Count <= 0)
                            {
                                if (operador.accion == accion.INGRESAR)
                                {
                                    operador.accion = accion.IGNORAR;
                                    GridViewOperadores.Rows[index].Attributes["style"] = "display:none";
                                }
                                if (operador.accion == accion.LISTADO)
                                {
                                    operador.accion = accion.ELIMINAR;
                                    GridViewOperadores.Rows[index].Attributes["style"] = "display:none";
                                }
                            }
                            else
                            {

                                foreach (String error in listaErroresEspAu)
                                {
                                    Page.Validators.Add(new ValidationError("grupo1", error));
                                }

                                break;
                            }
                        }
                    }

                    solicitante.listaOperador = List_Operador;
                    Session["Titular"] = (Solicitante)solicitante;

                    cargarGrillaOperador(List_Operador);

                    break;
            }
        }

        protected void GridRepresentantesLegales_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int idRepresentanteLegal = Convert.ToInt32(arg[0]);
            int index = Convert.ToInt32(arg[1]);

            Solicitante solicitante = (Solicitante)Session["Titular"];

            switch (e.CommandName)
            {
                
                case "Eliminar":
                    /*
                    if (idRepresentanteLegal > 0)
                    {
                    
                    }
                    else
                    {

                        List<RepLegal> listaRepLegal = solicitante.listaRepresentanteLegal;

                        foreach (RepLegal repLegalSession in listaRepLegal)
                        {
                            if (repLegalSession.index.Equals(Convert.ToInt32(index)))
                            {
                                listaRepLegal.Remove(repLegalSession);
                                break;
                            }
                        }

                        GridRepresentantesLegales.DataSource = listaRepLegal;
                        GridRepresentantesLegales.DataBind();

                        solicitante.listaRepresentanteLegal = listaRepLegal;
                        Session["Titular"] = (Solicitante)solicitante;

                    }*/

                    GridRepresentantesLegales.EditIndex = -1;
                    EliminarGrilla(index,"RepresentanteLegal",solicitante);
                    break;

                case "Descargar":
                    ArchivoBinarioEspecial archivoBinarioEspecial = null;
                    ArchivoBinario archivoBinario = null;

                    /* Obtener el archivo desde la base de datos */
                    if (idRepresentanteLegal > 0)
                    {
                        RepLegal repLegal = representanteLegalDA.ObtieneRepresentanteTitularId(idRepresentanteLegal);

                        if (repLegal != null && repLegal.archivoAsoc != null && repLegal.archivoAsoc.idArchivo > 0)
                        {
                            archivoBinario = archivoBinarioSolicitudDA.ObtenerArchivoBinarioSolicitud(repLegal.archivoAsoc.idArchivo);
                        }
                        
                    }

                    /* Obtener el archivo desde la session */
                    else {

                        List<RepLegal> listaMatrizSucursal = solicitante.listaRepresentanteLegal;
                        RepLegal repLegal = null;

                        int i = 0;
                        foreach (RepLegal repLegalSession in listaMatrizSucursal)
                        {
                            if (i == index)
                            {
                                repLegal = repLegalSession;
                            }
                            i++;
                        }

                        archivoBinarioEspecial = repLegal.archivoAsoc;
                        
                    }

                    if (archivoBinarioEspecial != null)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = "application/" + archivoBinarioEspecial.formato;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinarioEspecial.nombreFisico + "." + archivoBinarioEspecial.formato);
                        Response.BinaryWrite(archivoBinarioEspecial.bytes);
                        Response.Flush();
                        Response.End();

                    }
                    else if (archivoBinario != null)
                    {

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = "application/" + archivoBinario.formato;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinario.nombreArchivo + "." + archivoBinario.formato);
                        Response.BinaryWrite(archivoBinario.bytes);
                        Response.Flush();
                        Response.End();

                    }

                    break;
            }
        }

        protected void GridViewOperadores_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int idOperador = Convert.ToInt32(arg[0]);
            int index = Convert.ToInt32(arg[1]);

            Solicitante solicitante = (Solicitante)Session["Titular"];

            switch (e.CommandName)
            {

                case "Eliminar":

                    /*
                    if (idOperador > 0)
                    {

                    }
                    else
                    {

                        List<Operador> listaOperador = solicitante.listaOperador;

                        foreach (Operador operadorSession in listaOperador)
                        {
                            if (operadorSession.index.Equals(Convert.ToInt32(index)))
                            {
                                listaOperador.Remove(operadorSession);
                                break;
                            }
                        }

                        GridViewOperadores.DataSource = listaOperador;
                        GridViewOperadores.DataBind();

                        solicitante.listaOperador = listaOperador;
                        Session["Titular"] = (Solicitante)solicitante;

                    }*/

                    GridViewOperadores.EditIndex = -1;
                    EliminarGrilla(index,"Operador",solicitante);
                    
                    break;

                case "Descargar":
                     ArchivoBinarioEspecial archivoBinarioEspecial = null;
                     ArchivoBinario archivoBinario = null;

                    /* Obtener el archivo desde la base de datos */
                     if (idOperador > 0)
                    {

                        Operador operador = operadorDA.ObtenerOperadorTitularId(idOperador);

                        if (operador != null && operador.archivoAsoc != null && operador.archivoAsoc.idArchivo > 0)
                        {
                            archivoBinario = archivoBinarioSolicitudDA.ObtenerArchivoBinarioSolicitud(operador.archivoAsoc.idArchivo);
                        }
                        
                    }

                    /* Obtener el archivo desde la session */
                    else {

                        List<Operador> listaOperador = solicitante.listaOperador;
                        Operador operador = null;

                        int i = 0;
                        foreach (Operador operadorSession in listaOperador)
                        {
                            if (i == index)
                            {
                                operador = operadorSession;
                            }
                            i++;
                        }

                        archivoBinarioEspecial = operador.archivoAsoc;
                        
                    }

                     if (archivoBinarioEspecial != null)
                     {
                         Response.Clear();
                         Response.Buffer = true;
                         Response.Charset = "";
                         Response.Cache.SetCacheability(HttpCacheability.NoCache);
                         Response.ContentType = "application/" + archivoBinarioEspecial.formato;
                         Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinarioEspecial.nombreFisico + "." + archivoBinarioEspecial.formato);
                         Response.BinaryWrite(archivoBinarioEspecial.bytes);
                         Response.Flush();
                         Response.End();

                     }
                     else if (archivoBinario != null)
                     {

                         Response.Clear();
                         Response.Buffer = true;
                         Response.Charset = "";
                         Response.Cache.SetCacheability(HttpCacheability.NoCache);
                         Response.ContentType = "application/" + archivoBinario.formato;
                         Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinario.nombreArchivo + "." + archivoBinario.formato);
                         Response.BinaryWrite(archivoBinario.bytes);
                         Response.Flush();
                         Response.End();

                     }


                    break;
            }
        }
                

        protected void ImgAdd_PreRender(object sender, EventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            ScriptManager sc = ScriptManager.GetCurrent(this.Page);
            sc.RegisterPostBackControl(btn);
        }

        protected void GridRepresentantesLegales_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GridViewOperadores_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridOperador = (GridView)sender;

                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Operadores";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridOperador.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        protected void GridViewOperadores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //ACCION
                HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
                if (hidden_accion != null && !hidden_accion.Value.Equals("") && (Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR || Convert.ToInt32(hidden_accion.Value) == accion.IGNORAR))
                {
                    e.Row.Attributes["style"] = "display:none";
                };


                ////Descargar
                //HiddenField hidden_descargar = (HiddenField)e.Row.FindControl("gArchivoBinario");
                //if (hidden_descargar != null && !hidden_descargar.Value.Equals("") && Convert.ToInt32(hidden_descargar.Value) > 0)
                //{
                //    ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                //    if (boton_descargar != null)
                //    {
                //        boton_descargar.Visible = true;

                //    }
                //}
                 
                //Eliminar
                ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
                if (boton_borrar != null)
                {
                    boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar la asociación con el Operador?')");
                    boton_borrar.Visible = true;

                }

            }
        }

        protected void GridRepresentantesLegales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //ACCION
                HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
                if (hidden_accion != null && !hidden_accion.Value.Equals("") && (Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR || Convert.ToInt32(hidden_accion.Value) == accion.IGNORAR))
                {
                    e.Row.Attributes["style"] = "display:none";
                };

                //Descargar
                HiddenField hidden_descargar = (HiddenField)e.Row.FindControl("gArchivoBinario");
                if (hidden_descargar != null && !hidden_descargar.Value.Equals("") && Convert.ToInt32(hidden_descargar.Value) > 0)
                {
                    ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                    if (boton_descargar != null)
                    {
                        boton_descargar.Visible = true;

                    }
                }
                                 
                //Eliminar
                ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
                if (boton_borrar != null)
                {
                    boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar la asociación con el Representante Legal?')");
                    boton_borrar.Visible = true;

                }

            }
        }

        protected void TipoPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            limpiarFormulario();
            PanelPersonaNatural.Visible = false;
            PanelPersonaJuridica.Visible = false;
        }

        protected void ModificarTitular_Click(object sender, EventArgs e)
        {
            Content_msgGrilla.Visible = false;
                        
            guardarBorradorFormulario();
            Solicitante solicitante = (Solicitante)Session["Titular"];
            solicitante.accion = accion.MODIFICAR;

            List<string> listaErroresRutTitular = mantenedorTitularesValidacion.validaTitular(solicitante);

            if (listaErroresRutTitular.Count <= 0)
            {
                solicitante.idEstadoAsociacion = rbEstadosGenerales.VIGENTE;

                if (!RegistroCentralizado.Value.Equals(""))
                {
                    solicitante.regCentralizado = Convert.ToBoolean(RegistroCentralizado.Value);
                }

                bool resp = mantenedorTitularService.modificarTitular(solicitante);

                if (resp)
                {
                    Session["Titular"] = null;
                    string path = "~/Mantenedores/Titulares/administrarTitulares.aspx?acc=" + ViewState["acc"].ToString();
                    Response.Redirect(path);
                }
                else
                {
                    msgGrilla.Text = "No se ha modificado el Titular en el sistema.";
                    Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Content_msgGrilla.Visible = true;
                }
            }
            else
            {
                foreach (String error in listaErroresRutTitular)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
            }
        }

        protected void RutPersona_TextChanged(object sender, EventArgs e)
        {
            RutPersona2.Text = RutPersona.Text;
        }

        protected void NombrePersonaNatural_TextChanged(object sender, EventArgs e)
        {
            NombrePersonaNatural.Text = NombrePersonaNatural.Text.ToUpper();
            
        }

        protected void NombrePersonaJuridica_TextChanged(object sender, EventArgs e)
        {
            NombrePersonaJuridica.Text = NombrePersonaJuridica.Text.ToUpper();
            
        }

        protected void ImageButtonContacto_Click(object sender, ImageClickEventArgs e)
        {
            guardarBorradorFormulario();

            string path = "~/Mantenedores/Titulares/agregarContactos.aspx?rutPersona=" + RutPersona.Text + "&bp=1&acc=" + ViewState["acc"].ToString();
            Response.Redirect(path);
        }

    }
}