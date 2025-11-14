using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using System.Collections;
using Validaciones.cl.subpesca.rb.solicitud;
using SubPesca.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;

namespace SubPesca.Solicitudes.ECMPO
{
    public partial class identificacionTitularECMPO : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        TipoDA tipoDa = new TipoDA();
        SolicitanteDA solicitanteDA = new SolicitanteDA();
        SolicitudDA solicitudDA = new SolicitudDA();
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();
        PermisosService permisosService = new PermisosService();
        IdentificacionSolicitanteValidacion identificacionSolicitanteValidacion = new IdentificacionSolicitanteValidacion();


        protected void setearModulo()
        {

            ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_ECMPO;
            ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_ECMPO;
            ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_ECMPO;
            ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_ECMPO;
            ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_ECMPO;
            ViewState["solicitudSession"] = paginas.solicitudECMPOSession;

            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.IDENTIFICACION_SOLICITANTE_ECMPO };

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                setearModulo();

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (solicitudConcesion == null || usuario_logeado == null)
                {
                    Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
                }


                //FORMULARIO DE INGRESO
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {
                    FormularioIngreso.Visible = true;
                }
                else
                {
                    FormularioIngreso.Visible = false;
                }

                // Inicializamos el formulario
                Initialize_Form();

                // Inicializamos el Hashtable con solicitantes
                Initialize_HT_SolicitantesAcuicultura();

                //Inicializamos el Hashtable con las Solicitudes Pendientes de los Titulares de la Solicitud.
                Initialize_HT_RequerimientosPendientesTitular();

            }
            else
            {

                limpiarMensajesAlerta();
            }
        }

        private void limpiarMensajesAlerta()
        {
            msgGrilla.Text = "";
            Content_msgGrilla.Visible = false;
        }

        private void Initialize_HT_RequerimientosPendientesTitular()
        {
            string KeySort = "";
            int pagina = 0;

            Hashtable HT_ModSolicitantes = (Hashtable)Session["Modulo_Solicitantes"];
            Hashtable HT_RequerimientosPendientesTitulares = new Hashtable();

            HT_RequerimientosPendientesTitulares.Add("locked", true);
            HT_RequerimientosPendientesTitulares.Add("KeySort", KeySort);
            HT_RequerimientosPendientesTitulares.Add("pagina", pagina);

            // Actualizamos la sesión Modulo_Solicitantes
            if (HT_ModSolicitantes == null)
            {
                HT_ModSolicitantes = new Hashtable();
                HT_ModSolicitantes.Add("RequerimientosPendientes", (Hashtable)HT_RequerimientosPendientesTitulares);
            }
            else
            {
                if (HT_ModSolicitantes["RequerimientosPendientes"] == null)
                {
                    HT_ModSolicitantes.Add("RequerimientosPendientes", (Hashtable)HT_RequerimientosPendientesTitulares);
                }
                else
                {
                    HT_ModSolicitantes["RequerimientosPendientes"] = (Hashtable)HT_RequerimientosPendientesTitulares;
                };
            };
            Session["Modulo_Solicitantes"] = (Hashtable)HT_ModSolicitantes;

            SolicitudConcesion solicitudAux = (SolicitudConcesion)Session[paginas.solicitudECMPOSession];
            if (solicitudAux != null && solicitudAux.idSolConcesion > 0)
            {
                //CargarListaRequerimientosPendientesTitular(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));

                CargarListaSolicitudesPendientes(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));
            }
        }

        /*
        private void CargarListaRequerimientosPendientesTitular(Usuario.Serializable usuario_logeado, int idSolicitud)
        {
            List<Requerimiento> ListaRequerimientosPendientesTitular = solicitudDA.ListarPequerimientosPendPersona(idSolicitud, 0);

            GridViewSolicitudesPendientes.DataSource = ListaRequerimientosPendientesTitular;
            GridViewSolicitudesPendientes.DataBind();
            GridViewSolicitudesPendientes.Visible = true;
        }
        */

        private void Initialize_HT_SolicitantesAcuicultura()
        {
            string KeySort = "";
            int pagina = 0;
            int tipo_persona = -1;
            int rut_persona = 0;
            char dv_persona = ' ';
            string nombre_solicitante_natural = "";
            string genero = "";
            int tipo_persona_juridica = -1;
            string nombre_solicitante_juridico = "";

            Hashtable HT_ModSolicitantes = (Hashtable)Session["Modulo_Solicitantes"];
            Hashtable HT_SolicitantesAcuicultura = new Hashtable();

            HT_SolicitantesAcuicultura.Add("locked", true);
            HT_SolicitantesAcuicultura.Add("KeySort", KeySort);
            HT_SolicitantesAcuicultura.Add("pagina", pagina);
            HT_SolicitantesAcuicultura.Add("tipo_persona", tipo_persona);
            HT_SolicitantesAcuicultura.Add("rut_persona", rut_persona);
            HT_SolicitantesAcuicultura.Add("dv_persona", dv_persona);
            HT_SolicitantesAcuicultura.Add("nombre_solicitante_natural", nombre_solicitante_natural);
            HT_SolicitantesAcuicultura.Add("genero", genero);
            HT_SolicitantesAcuicultura.Add("tipo_persona_juridica", tipo_persona_juridica);
            HT_SolicitantesAcuicultura.Add("nombre_solicitante_juridico", nombre_solicitante_juridico);

            // Actualizamos la sesión Modulo_Solicitantes
            if (HT_ModSolicitantes == null)
            {
                HT_ModSolicitantes = new Hashtable();
                HT_ModSolicitantes.Add("Solicitantes", (Hashtable)HT_SolicitantesAcuicultura);
            }
            else
            {
                if (HT_ModSolicitantes["Solicitantes"] == null)
                {
                    HT_ModSolicitantes.Add("Solicitantes", (Hashtable)HT_SolicitantesAcuicultura);
                }
                else
                {
                    HT_ModSolicitantes["Solicitantes"] = (Hashtable)HT_SolicitantesAcuicultura;
                };
            };
            Session["Modulo_Solicitantes"] = (Hashtable)HT_ModSolicitantes;
            //Gbrito
            //CargarListaSolicitantes(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));
            //SolicitudConcesion solicitudAux = (SolicitudConcesion)Session["solicitudConcesionInicial"];

            SolicitudConcesion solicitudAux = (SolicitudConcesion)Session[paginas.solicitudECMPOSession];
            if (solicitudAux != null && solicitudAux.idSolConcesion > 0)
            {
                CargarListaSolicitantes(usuario_logeado, Convert.ToInt32(solicitudAux.idSolConcesion));
            }
            //end Gbrito



            //CargarListaSolicitudesPendientes(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));
        }

        protected void Initialize_Form()
        {

            //SolicitudConcesion solicitudAux = (SolicitudConcesion)Session["SolicitudConcesionInicial"];

            SolicitudConcesion solicitudAux = (SolicitudConcesion)Session[paginas.solicitudECMPOSession];
            if (solicitudAux != null && solicitudAux.idSolConcesion > 0)
            {
                IdSolicitud.Value = Convert.ToString(solicitudAux.idSolConcesion);

                // Cargamos los combobox
                Initialize_Comboboxs();
            }
            else
            {
                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");

            }
        }

        /**
         * Método que inicializa los combobox dentro del formulario de 
         * solicitantes de concesión de acuicultura.
         */
        protected void Initialize_Comboboxs()
        {

            //Carga_Combobox("TipoPersona");
            //TipoPersona.SelectedValue = "0";    
        }

        /**
         * Método que carga los combobox de la página identificación del solicitante.
         *
        private void Carga_Combobox(string combobox)
        {
            switch (combobox)
            {

                case "TipoPersona":
                    // Cargamos el combobox: TipoPersona
                    TipoPersona.Items.Clear();
                    TipoPersona.DataSource = tipoDa.ListarTipo("TIPO_PERSONA");
                    TipoPersona.DataTextField = "descripcion";
                    TipoPersona.DataValueField = "id";
                    TipoPersona.DataBind();
                    TipoPersona.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;
            }
        }*/

        /**
         * Método que busca la información del Solicitante en la BD de Subpesca.
         */
        protected void CambiaTipoPersona_Click(object sender, EventArgs e)
        {
            PanelPersonaJuridica.Visible = false;
            PanelDatosPersonaNatural.Visible = false;
            NombreSolicitanteNatural.Text = "";
            Genero.Text = "";
            SubtipoPersonaJuridica.Text = "";
            NombreSolicitanteJuridico.Text = "";
            Content_msgGrilla.Visible = false;
        }

        /**
         * Método que busca la información del Solicitante en la BD de Subpesca.
         */
        protected void BuscarSolicitante_Click(object sender, EventArgs e)
        {
            Content_msgGrilla.Visible = false;

            ccNumCustVal.Validate();
            if (Page.IsValid)
            {
                SolicitanteService solicitanteService = new SolicitanteService();
                Solicitante solicitante = new Solicitante();
                //solicitante.tipoPersona = new ParametroGenerico(Convert.ToInt32(TipoPersona.SelectedValue));

                //solicitante.rut = Convert.ToInt32(RutPersona.Text);
                //solicitante.dv = Convert.ToChar(DVPersona.Text);

                String rutCompleto = Convert.ToString(RutPersona.Text);
                String[] rutPartes = rutCompleto.Split('-');

                solicitante.rut = Convert.ToInt32(rutPartes[0]);

                //solicitante = solicitanteService.VerPersona(solicitante.rut, solicitante.tipoPersona.id);
                solicitante = solicitanteService.VerPersona(solicitante.rut, 0);

                if (solicitante != null && solicitante.rut > 0)
                {
                    //if (Convert.ToInt32(TipoPersona.SelectedValue) == rbTipo.PERSONA_NATURAL)
                    if (solicitante.tipoPersona != null && solicitante.tipoPersona.id == rbTipo.PERSONA_NATURAL)
                    {
                        NombreSolicitanteNatural.Text = solicitante.nombreSolicitante;
                        if (solicitante.genero)
                        {
                            Genero.Text = "Femenino";
                        }
                        else
                        {
                            Genero.Text = "Masculino";
                        }
                        PanelPersonaJuridica.Visible = false;
                        PanelDatosPersonaNatural.Visible = true;
                    }

                    //else if (Convert.ToInt32(TipoPersona.SelectedValue) == rbTipo.PERSONA_JURIDICA)

                    else if (solicitante.tipoPersona != null && solicitante.tipoPersona.id == rbTipo.PERSONA_JURIDICA)
                    {
                        SubtipoPersonaJuridica.Text = solicitante.subtipoPersona.descripcion;
                        NombreSolicitanteJuridico.Text = solicitante.nombreSolicitante;
                        PanelPersonaJuridica.Visible = true;
                        PanelDatosPersonaNatural.Visible = false;
                    }
                    else
                    {
                        PanelPersonaJuridica.Visible = false;
                        PanelDatosPersonaNatural.Visible = false;
                    }
                }
                else
                {
                    msgGrilla.Text = "No se encontró persona coincidente con el filtro de búsqueda.";
                    Content_msgGrilla.Visible = true;
                    Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                }
            }

        }


        /**
         * Método que agrega un solicitante a una solicitud de concesión de 
         * acuicultura.
         */
        protected void AgregarSolicitante_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                SolicitanteService solicitanteService = new SolicitanteService();
                Solicitante solicitante = new Solicitante();

                solicitante.solicitud = new SolicitudConcesion();
                solicitante.solicitud.idSolConcesion = Convert.ToInt32(IdSolicitud.Value);

                String rutCompleto = Convert.ToString(RutPersona.Text);
                String[] rutPartes = rutCompleto.Split('-');

                solicitante.rut = Convert.ToInt32(rutPartes[0]);
                solicitante.dv = Convert.ToChar(rutPartes[1]);

                solicitante.idPersonasLeg = 0;
                solicitante.idEstadoAsociacion = rbEstadosGenerales.VIGENTE;

                /*
                solicitante.tipoPersona = new ParametroGenerico(Convert.ToInt32(TipoPersona.SelectedValue));

                if (solicitante.tipoPersona != null && solicitante.tipoPersona.id == rbTipo.PERSONA_NATURAL)
                {
                    solicitante.nombreSolicitante = NombreSolicitanteNatural.Text;
                }
                else if (solicitante.tipoPersona != null && solicitante.tipoPersona.id == rbTipo.PERSONA_JURIDICA)
                {
                    solicitante.nombreSolicitante = NombreSolicitanteJuridico.Text;
                }
                 */

                //if (solicitanteService.VerPersona(solicitante.rut, solicitante.tipoPersona.id) != null)
                if (solicitanteService.VerPersona(solicitante.rut, 0) != null)
                {
                    //solicitante = solicitanteService.existeSolicitante(IdSolicitud.Value, solicitante.rut, 0);
                    if (solicitanteService.existeSolicitante(IdSolicitud.Value, solicitante.rut, 0) == null)
                    {
                        bool resp = solicitanteService.guardarSolicitante(solicitante, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                        if (resp)
                        {
                            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                            CargarListaSolicitantes(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));

                            CargarListaSolicitudesPendientes(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));

                            msgGrilla.Text = "Se ha asociado el Solicitante de forma exitosa a la solicitud de Acuicultura en ECMPO.";
                            Content_msgGrilla.Visible = true;
                            LimpiarCamposSolicitante();

                        }
                        else
                        {
                            msgGrilla.Text = "No se ha asociado el Solicitante a la solicitud de Acuicultura en ECMPO.";
                            Content_msgGrilla.Visible = true;
                        }
                    }
                    else
                    {
                        msgGrilla.Text = "El Solicitante ya se encuentra asociado a la solicitud de Acuicultura en ECMPO.";
                        Content_msgGrilla.Visible = true;
                        LimpiarCamposSolicitante();
                    }
                    Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                }
                else
                {
                    msgGrilla.Text = "No se encontró persona coincidente con el filtro de búsqueda.";
                    Content_msgGrilla.Visible = true;
                    Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                }
            }
        }

        /**
         * Método que limpia los campos del formulario identificación del solicitante.
         */
        private void LimpiarCamposSolicitante()
        {
            //TipoPersona.SelectedValue = "0";
            RutPersona.Text = "";
            //DVPersona.Text = "";

            NombreSolicitanteNatural.Text = "";
            Genero.Text = "";
            PanelDatosPersonaNatural.Visible = false;

            SubtipoPersonaJuridica.Text = "";
            NombreSolicitanteJuridico.Text = "";
            PanelPersonaJuridica.Visible = false;
        }

        /**
         * Método de carga el listado de solicitantes asociados a la solicitud
         * de concesión de acuicultura.
         */
        private void CargarListaSolicitantes(Usuario.Serializable usuario_logeado, int IdSolicitud)
        {

            string KeySort = "IdSolicitud ASC";
            int pagina = 0;

            Hashtable HT_ModSolicitantes = new Hashtable();
            Hashtable HT_SolicitantesAcuicultura = new Hashtable();

            try
            {
                HT_ModSolicitantes = (Hashtable)Session["Modulo_Solicitantes"];
                HT_SolicitantesAcuicultura = (Hashtable)HT_ModSolicitantes["Solicitantes"];
                KeySort = (string)HT_SolicitantesAcuicultura["KeySort"];
                pagina = (int)HT_SolicitantesAcuicultura["pagina"];
            }
            catch
            { };

            GridSolicitante.PageIndex = pagina;
            GridSolicitante.DataSource = solicitanteDA.VerSolicitante(IdSolicitud, 0, KeySort, 0);
            GridSolicitante.DataBind();

        }

        /**
         * Método que carga las solicitudes pendientes de los titulares de la solicitud
         * de concesión de acuicultura del Pert seleccionado.
         */
        private void CargarListaSolicitudesPendientes(Usuario.Serializable usuario_logeado, int idSolicitud)
        {

            string KeySort = "IdSolicitud ASC";
            int pagina = 0;

            Hashtable HT_ModSolicitantes = new Hashtable();
            Hashtable HT_RequerimientosPendientesTitulares = new Hashtable();

            try
            {
                HT_ModSolicitantes = (Hashtable)Session["Modulo_Solicitantes"];
                HT_RequerimientosPendientesTitulares = (Hashtable)HT_ModSolicitantes["RequerimientosPendientes"];
                KeySort = (string)HT_RequerimientosPendientesTitulares["KeySort"];
                pagina = (int)HT_RequerimientosPendientesTitulares["pagina"];
            }
            catch
            { };

            GridViewSolicitudesPendientes.PageIndex = pagina;
            GridViewSolicitudesPendientes.DataSource = solicitudDA.ListarPequerimientosPendPersona(idSolicitud, 0);
            GridViewSolicitudesPendientes.DataBind();
        }

        /**
         * Método que dibuja las filas de la grilla de solicitantes de la solicitud
         * de concesión de acuicultura.
         */
        protected void GridSolicitante_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SolicitanteService solicitanteService = new SolicitanteService();

                //Ver
                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    //if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.VER))
                    //{
                        boton_ver.Visible = true;
                    //}
                };

                int rutPersona = Convert.ToInt32(GridSolicitante.DataKeys[e.Row.RowIndex].Value);
                Datos.Entidades.Solicitante solicitante = solicitanteService.existeSolicitante(IdSolicitud.Value, rutPersona, 0);

                if (solicitante != null && solicitante.idEstadoAsociacion == rbEstadosGenerales.NO_VIGENTE)
                {
                    //Desasociar
                    ImageButton boton_desasociar = (ImageButton)e.Row.FindControl("gDesasociar");
                    if (boton_desasociar != null)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.DESASOCIAR))
                        {
                            boton_desasociar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar de Estado al Solicitante?')");
                            boton_desasociar.Visible = true;
                        }
                    };

                }
                else if (solicitante != null && solicitante.idEstadoAsociacion == rbEstadosGenerales.VIGENTE)
                {
                    //Asociar
                    ImageButton boton_asociar = (ImageButton)e.Row.FindControl("gAsociar");
                    if (boton_asociar != null)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ASOCIAR))
                        {
                            boton_asociar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar de Estado al Solicitante?')");
                            boton_asociar.Visible = true;
                        }
                    };
                }

                //Borrar
                ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
                if (boton_borrar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ELIMINAR))
                    {
                        boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea borrar al Solicitante?')");
                        boton_borrar.Visible = true;
                    }
                };
            }
        }


        protected void GridViewSolicitudesPendientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SolicitanteService solicitanteService = new SolicitanteService();

                //Ver
                HiddenField hidden_solicitud = (HiddenField)e.Row.FindControl("gSolicitud");
                SolicitudConcesion solicitud = (SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (hidden_solicitud != null && !hidden_solicitud.Value.Equals("") && solicitud != null && solicitud.tipoTramite != null &&
                    Convert.ToInt32(hidden_solicitud.Value) == solicitud.idSolConcesion)
                {
                    ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                    if (boton_ver != null)
                    {
                        boton_ver.Visible = true;
                    };
                }

                //Descargar
                String idArchivo = DataBinder.Eval(e.Row.DataItem, "idArchivo").ToString();
                ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                if (boton_descargar != null && idArchivo != null && !idArchivo.Equals("") && Convert.ToInt32(idArchivo) > 0)
                {
                    boton_descargar.Visible = true;
                };

            }
        }


        protected void GridSolicitante_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridSolicitante = (GridView)sender;


                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Solicitantes";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridSolicitante.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        protected void GridViewSolicitudesPendientes_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridRequerimientoPendienteSolicitante = (GridView)sender;


                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Solicitudes Pendientes asociadas a los Titulares";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridRequerimientoPendienteSolicitante.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        /**
         * Método que maneja el cambio de página en la funcionalidad de paginación.
         * Por defecto el número de solicitantes a desplegar en la grilla es de 10 solicitantes.
         */
        protected void GridSolicitante_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            Hashtable HT_ModSolicitantes = (Hashtable)Session["Modulo_Solicitantes"];
            Hashtable HT_SolicitantesAcuicultura = (Hashtable)HT_ModSolicitantes["Solicitantes"];
            HT_SolicitantesAcuicultura["pagina"] = e.NewPageIndex;
            HT_ModSolicitantes["Solicitantes"] = (Hashtable)HT_SolicitantesAcuicultura;
            Session["Modulo_Solicitantes"] = (Hashtable)HT_ModSolicitantes;

            GridSolicitante.PageIndex = e.NewPageIndex;
            GridSolicitante.DataBind();
            CargarListaSolicitantes(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));
        }

        /**
         * Método que maneja el cambio de página en la funcionalidad de paginación.
         * Por defecto el número de requerimientos a desplegar en la grilla es de 10 solicitantes.
         */
        protected void GridViewSolicitudesPendientes_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            Hashtable HT_ModSolicitantes = (Hashtable)Session["Modulo_Solicitantes"];
            Hashtable HT_RequerimientosPendientesTitulares = (Hashtable)HT_ModSolicitantes["RequerimientosPendientes"];
            HT_RequerimientosPendientesTitulares["pagina"] = e.NewPageIndex;
            HT_ModSolicitantes["RequerimientosPendientes"] = (Hashtable)HT_RequerimientosPendientesTitulares;
            Session["Modulo_Solicitantes"] = (Hashtable)HT_ModSolicitantes;

            GridViewSolicitudesPendientes.PageIndex = e.NewPageIndex;
            GridViewSolicitudesPendientes.DataBind();
            CargarListaSolicitudesPendientes(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));
        }

        /**
         * Método que ordeba la grilla de solicitantes de concesión de acuicultura, de
         * acuerdo a las columnas de la grilla que fue seleccionada.
         */
        protected void GridSolicitante_Sorting(object sender, GridViewSortEventArgs e)
        {
            Hashtable HT_ModSolicitantes = (Hashtable)Session["Modulo_Solicitantes"];
            Hashtable HT_SolicitantesAcuicultura = (Hashtable)HT_ModSolicitantes["Solicitantes"];
            string KeySort = (string)HT_SolicitantesAcuicultura["KeySort"];
            int pos = 0;

            pos = KeySort.IndexOf(e.SortExpression + " ASC");
            if (pos >= 0)
            {
                KeySort = e.SortExpression + " DESC";
            }
            else
            {
                KeySort = e.SortExpression + " ASC";
            };

            HT_SolicitantesAcuicultura["KeySort"] = KeySort;
            HT_SolicitantesAcuicultura["pagina"] = 0;
            HT_ModSolicitantes["Solicitantes"] = (Hashtable)HT_SolicitantesAcuicultura;
            Session["Modulo_Solicitantes"] = (Hashtable)HT_ModSolicitantes;

            CargarListaSolicitantes(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));
        }


        /**
         * Método que ordena la grilla de requerimientos pendientes de los titulares, de
         * acuerdo a las columnas de la grilla que fue seleccionada.
         */
        protected void GridViewSolicitudesPendientes_Sorting(object sender, GridViewSortEventArgs e)
        {
            Hashtable HT_ModSolicitantes = (Hashtable)Session["Modulo_Solicitantes"];
            Hashtable HT_RequerimientosPendientesTitulares = (Hashtable)HT_ModSolicitantes["RequerimientosPendientes"];
            string KeySort = (string)HT_RequerimientosPendientesTitulares["KeySort"];
            int pos = 0;

            pos = KeySort.IndexOf(e.SortExpression + " ASC");
            if (pos >= 0)
            {
                KeySort = e.SortExpression + " DESC";
            }
            else
            {
                KeySort = e.SortExpression + " ASC";
            };

            HT_RequerimientosPendientesTitulares["KeySort"] = KeySort;
            HT_RequerimientosPendientesTitulares["pagina"] = 0;
            HT_ModSolicitantes["RequerimientosPendientes"] = (Hashtable)HT_RequerimientosPendientesTitulares;
            Session["Modulo_Solicitantes"] = (Hashtable)HT_ModSolicitantes;

            CargarListaSolicitudesPendientes(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));
        }

        /**
         * Método que agrega funcionalidad a los botones que aparecen en la grilla.
         */
        protected void GridSolicitante_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Content_msgGrilla.Visible = false;
            int rutPersona = 0;
            int idPersonasLeg = 0;

            switch (e.CommandName)
            {
                case "Ver":
                    Response.Redirect("~/Mantenedores/Titulares/detalleTitular.aspx?rutPersona=" + e.CommandArgument + "&bp=11");
                    break;
                case "Eliminar":
                    idPersonasLeg = Convert.ToInt32(e.CommandArgument);
                    GridSolicitante.EditIndex = -1;
                    EliminarGrilla(idPersonasLeg);
                    CargarListaSolicitantes(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));

                    /* Se actualiza la tabla de solicitudes pendientes de los solicitantes que quedaron */
                    CargarListaSolicitudesPendientes(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));

                    break;
                case "Desasociar":
                    rutPersona = Convert.ToInt32(e.CommandArgument);
                    CambiarEstadoGrilla(rutPersona);
                    CargarListaSolicitantes(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));
                    break;
                case "Asociar":
                    rutPersona = Convert.ToInt32(e.CommandArgument);
                    CambiarEstadoGrilla(rutPersona);
                    CargarListaSolicitantes(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));
                    break;
            };
        }


        protected void GridViewSolicitudesPendientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Content_msgGrilla.Visible = false;

            switch (e.CommandName)
            {
                case "Ver":
                    int idRequerimiento = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/Solicitudes/ECMPO/verDocumentoECMPO.aspx?idRequerimiento=" + idRequerimiento);

                    break;
                case "Descargar":
                    int idArchivo = Convert.ToInt32(e.CommandArgument);
                    ArchivoBinario archivoBinario = archivoBinarioSolicitudDA.ObtenerArchivoBinarioSolicitud(idArchivo);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = "application/" + archivoBinario.formato;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinario.nombreArchivo + "." + archivoBinario.formato);
                    Response.BinaryWrite(archivoBinario.bytes);
                    Response.Flush();
                    Response.End();

                    break;
            };
        }


        /**
         * Método que elimina un Solicitante de la grilla de solicitantes de la
         * concesión de acuicultura.
         */
        protected void EliminarGrilla(int idPersonasLeg)
        {
            bool resp = true;

            SolicitanteService solicitanteService = new SolicitanteService();
            Solicitante solicitante = new Solicitante();
            solicitante.idPersonasLeg = idPersonasLeg;
            solicitante.solicitud = new SolicitudConcesion();
            solicitante.solicitud.idSolConcesion = Convert.ToInt32(IdSolicitud.Value);
            solicitante.accion = rbAccion.ELIMINAR;

            List<String> listErroresSolicitante = identificacionSolicitanteValidacion.validaIdentificacionSolicitante(solicitante);

            if (listErroresSolicitante.Count <= 0)
            {
                resp = solicitanteService.eliminarSolicitante(solicitante, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                if (resp)
                {
                    msgGrilla.Text = "Se ha eliminado exitosamente el Solicitante seleccionado.";
                    Content_msgGrilla.Visible = true;
                }
                else
                {
                    msgGrilla.Text = "No se ha eliminado el Solicitante seleccionado.";
                    Content_msgGrilla.Visible = true;
                }

                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                CargarListaSolicitantes(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));
            }
            else
            {
                foreach (String error in listErroresSolicitante)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }

            }
        }

        /**
         * Método que cambia de estado un Solicitante de la grilla de solicitantes de la
         * concesión de acuicultura.
         */
        protected void CambiarEstadoGrilla(int rutPersona)
        {
            Boolean resp = true;

            SolicitanteService solicitanteService = new SolicitanteService();
            Solicitante solicitante = new Solicitante();
            SolicitudConcesion solicitudConsecion = new SolicitudConcesion();

            solicitante.solicitud = solicitudConsecion;
            solicitante.solicitud.idSolConcesion = Convert.ToInt32(IdSolicitud.Value);
            solicitante.rut = rutPersona;

            solicitante = solicitanteService.VerSolicitante(solicitante.solicitud.idSolConcesion, solicitante.rut, "", 0);
            if (solicitante != null && solicitante.idEstadoAsociacion == rbEstadosGenerales.VIGENTE)
            {
                solicitante.idEstadoAsociacion = rbEstadosGenerales.NO_VIGENTE;

            }
            else
            {
                solicitante.idEstadoAsociacion = rbEstadosGenerales.VIGENTE;
            }

            resp = solicitanteService.guardarSolicitante(solicitante, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
            if (resp)
            {
                msgGrilla.Text = "Se ha cambiado exitosamente el estado al Solicitante seleccionado.";
                Content_msgGrilla.Visible = true;
            }
            else
            {
                msgGrilla.Text = "No se ha cambiado el estado al Solicitante seleccionado.";
                Content_msgGrilla.Visible = true;
            }

            Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            CargarListaSolicitantes(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));
        }

        /**
         * Método que limpia los campos del formulario para el ingreso de un nuevo
         * solicitante.
         */
        protected void LimpiarSolicitante_Click(object sender, ImageClickEventArgs e)
        {
            RutPersona.Text = "";

            NombreSolicitanteNatural.Text = "";
            Genero.Text = "";
            PanelDatosPersonaNatural.Visible = false;

            SubtipoPersonaJuridica.Text = "";
            NombreSolicitanteJuridico.Text = "";
            PanelPersonaJuridica.Visible = false;

        }

    }
}