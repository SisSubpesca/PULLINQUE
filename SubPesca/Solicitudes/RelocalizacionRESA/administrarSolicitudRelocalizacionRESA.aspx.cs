using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using Datos.Entidades.Relocalizacion;
using Datos.Entidades;
using SubPesca.Utilidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.reportes;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;


namespace SubPesca.Solicitudes.RelocalizacionRESA
{
    public partial class administrarSolicitudRelocalizacionRESA : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        RelocalizacionRESAService relocalizacionRESAService = new RelocalizacionRESAService();
        RegionDA regionDA = new RegionDA();
        SolicitudDA solicitudDA = new SolicitudDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        PermisosService permisosService = new PermisosService();
        int cantidadPaginacion = 50; //este valor debe ser igual a la paginacion indicada en las grillas de la pagina
        ReporteDA reporteDA = new ReporteDA();

        protected void Page_Load(object sender, EventArgs e)
        {

            // PAGE LOAD
            if (!Page.IsPostBack)
            {

                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (PreviousPage != null && PreviousPage is ingresarSolicitudRelocalizacionRESA)
                {
                    MensajeSuperior.Text = ((ingresarSolicitudRelocalizacionRESA)PreviousPage).MensajeRegistro;
                    PanelMensajeSuperior.Visible = true;
                    UpdatePanelMensajeSuperior.Update();
                }

                if (PreviousPage != null && PreviousPage is erroresTramiteRelocalizacionRESA)
                {
                    MensajeSuperior.Text = ((erroresTramiteRelocalizacionRESA)PreviousPage).MensajeRegistro;
                    PanelMensajeSuperior.Visible = true;
                    UpdatePanelMensajeSuperior.Update();
                }

                if (PreviousPage != null && PreviousPage is redefinirSolicitudRelocalizacionRESA)
                {
                    MensajeSuperior.Text = ((redefinirSolicitudRelocalizacionRESA)PreviousPage).MensajeRegistro;
                    PanelMensajeSuperior.Visible = true;
                    UpdatePanelMensajeSuperior.Update();
                }



               

                if (usuario_logeado == null)
                {
                    Response.Redirect("~/ingreso.aspx");
                }


              
         

                Initialize_Form();


                Buscar_Click(null, null);

                string strStatus = Request.QueryString["exchange"];
                if (strStatus != null && strStatus.ToUpper() == "1")
                {
                    String mensaje = "Se ha cambiando el tipo de relocalizacion exitosamente. (LEY a RESA)";
                    if (Session["PertRelocalizacionCambiado"] != null) { 
                        String pertRelocalizacionCambiado = (String)Session["PertRelocalizacionCambiado"];
                        mensaje = mensaje + " Pert : " + pertRelocalizacionCambiado;
                    }

                    Session["PertRelocalizacionCambiado"] = null;

                    MensajeSuperior.Text = mensaje;
                    PanelMensajeSuperior.Visible = true;
                    UpdatePanelMensajeSuperior.Update();
                }

            }
        }


        protected void Initialize_Form()
        {
            // Cargamos los combobox
            Initialize_Comboboxs();
        }


        protected void Initialize_Comboboxs()
        {

            Carga_Combobox("RegionOrigen");
            RegionOrigen.SelectedValue = "0";

            Carga_Combobox("ComunaOrigen");
            ComunaOrigen.SelectedValue = "0";

            Carga_Combobox("RegionDestino");
            RegionDestino.SelectedValue = "0";

            Carga_Combobox("ComunaDestino");
            ComunaDestino.SelectedValue = "0";

            Carga_Combobox("SubtipoTramite");
            SubtipoTramite.SelectedValue = "0";
            Estado.SelectedValue = "0";

        }


        protected void Carga_Combobox(string combobox)
        {

            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

            switch (combobox)
            {

                case "RegionOrigen":
                    RegionOrigen.Items.Clear();
                    RegionOrigen.DataSource = regionDA.ListarRegion(0);
                    RegionOrigen.DataTextField = "Region";
                    RegionOrigen.DataValueField = "IdRegion";
                    RegionOrigen.DataBind();
                    RegionOrigen.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    break;

                case "ComunaOrigen":
                    ComunaOrigen.Items.Clear();

                    if (Convert.ToInt32(RegionOrigen.SelectedValue) > 0)
                    {
                        ComunaOrigen.DataSource = parametroGenericoDA.ListarComunas(Convert.ToInt32(RegionOrigen.SelectedValue));
                        ComunaOrigen.DataTextField = "descripcion";
                        ComunaOrigen.DataValueField = "id";
                        ComunaOrigen.DataBind();
                    }

                    ComunaOrigen.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    UpdatePanelComunaOrigen.Update();
                    break;

                case "RegionDestino":
                    RegionDestino.Items.Clear();
                    RegionDestino.DataSource = regionDA.ListarRegion(0);
                    RegionDestino.DataTextField = "Region";
                    RegionDestino.DataValueField = "IdRegion";
                    RegionDestino.DataBind();
                    RegionDestino.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    break;

                case "ComunaDestino":
                    ComunaDestino.Items.Clear();

                    if (Convert.ToInt32(RegionDestino.SelectedValue) > 0)
                    {
                        ComunaDestino.DataSource = parametroGenericoDA.ListarComunas(Convert.ToInt32(RegionDestino.SelectedValue));
                        ComunaDestino.DataTextField = "descripcion";
                        ComunaDestino.DataValueField = "id";
                        ComunaDestino.DataBind();
                    }

                    ComunaDestino.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    UpdatePanelComunaDestino.Update();
                    break;

                case "SubtipoTramite":
                    SubtipoTramite.Items.Clear();
                    //falta el numoero correcto de administrarsolicitudexperimentalconcesion
                    SubtipoTramite.Items.Add(new ListItem("Solicitud Relocalización RESA: Sector 0", Convert.ToString(rbTipo.RELOCALIZACION_SECTOR_CERO_RESA)));
                    SubtipoTramite.Items.Add(new ListItem("Solicitud Relocalización RESA: Crea", Convert.ToString(rbTipo.RELOCALIZACION_CREA_RESA)));
                    SubtipoTramite.Items.Add(new ListItem("Solicitud Relocalización RESA: Fusiona", Convert.ToString(rbTipo.RELOCALIZACION_FUSIONA_RESA)));

                    SubtipoTramite.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    UpdatePanelSubtipoTramite.Update();

                    Estado.Items.Clear();
                    Estado.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    UpdatePanelEstado.Update();
                    break;

                case "Estado":
                    Estado.Items.Clear();
                    if (Convert.ToInt32(SubtipoTramite.SelectedValue) > 0)
                    {
                        Estado.DataSource = reporteDA.ListarEstadosPorTipoTramite(Convert.ToInt32(SubtipoTramite.SelectedValue));
                        Estado.DataTextField = "descripcion";
                        Estado.DataValueField = "id";
                        Estado.DataBind();
                    }

                    Estado.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    UpdatePanelEstado.Update();
                    break;
            };
        }


        protected void RegionOrigen_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("ComunaOrigen");
        }


        protected void RegionDestino_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("ComunaDestino");
        }

        protected void Subtipo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("Estado");
        }

        protected void cambiaPestania_Click(object sender, EventArgs e)
        {
            LinkButton boton = (LinkButton)sender;

            switch (boton.ID)
            {

                case "lnk_EnTramite":

                    lnk_EnTramite.CssClass = "tab1_selected";
                    UpdatePanelPestanaEnTramite.Update();

                    lnk_Aprobada.CssClass = "tab2";
                    UpdatePanelPestanaAprobada.Update();

                    lnk_Rechazada.CssClass = "tab3";
                    UpdatePanelPestanaRechazada.Update();



                    PanelEnTramite.Visible = true;
                    UpdatePanelEnTramite.Update();

                    PanelAprobada.Visible = false;
                    UpdatePanelAprobada.Update();

                    PanelRechazada.Visible = false;
                    UpdatePanelRechazada.Update();

                    break;


                case "lnk_Aprobada":


                    lnk_EnTramite.CssClass = "tab1";
                    UpdatePanelPestanaEnTramite.Update();

                    lnk_Aprobada.CssClass = "tab2_selected";
                    UpdatePanelPestanaAprobada.Update();

                    lnk_Rechazada.CssClass = "tab3";
                    UpdatePanelPestanaRechazada.Update();



                    PanelEnTramite.Visible = false;
                    UpdatePanelEnTramite.Update();

                    PanelAprobada.Visible = true;
                    UpdatePanelAprobada.Update();

                    PanelRechazada.Visible = false;
                    UpdatePanelRechazada.Update();



                    break;


                case "lnk_Rechazada":

                    lnk_EnTramite.CssClass = "tab1";
                    UpdatePanelPestanaEnTramite.Update();

                    lnk_Aprobada.CssClass = "tab2";
                    UpdatePanelPestanaAprobada.Update();

                    lnk_Rechazada.CssClass = "tab3_selected";
                    UpdatePanelPestanaRechazada.Update();



                    PanelEnTramite.Visible = false;
                    UpdatePanelEnTramite.Update();

                    PanelAprobada.Visible = false;
                    UpdatePanelAprobada.Update();

                    PanelRechazada.Visible = true;
                    UpdatePanelRechazada.Update();

                    break;
            };

        }


        protected void Limpiar_Click(object sender, EventArgs e)
        {

            MensajeSuperior.Text = "";
            PanelMensajeSuperior.Visible = false;
            UpdatePanelMensajeSuperior.Update();

            NPert.Text = "";
            FechaIngresoTramiteDesde.Text = "";
            FechaIngresoTramiteHasta.Text = "";

            TitularNombre.Text = "";
            CodigoSiepCentroOrigen.Text = "";
            CodigoSiepCentroDestino.Text = "";

            Estado.SelectedValue = "0";
            SubtipoTramite.SelectedValue = "0";

            RegionOrigen.SelectedValue = "0";
            ComunaOrigen.SelectedValue = "0";

            RegionDestino.SelectedValue = "0";
            ComunaDestino.SelectedValue = "0";

            UpdatePanelNPert.Update();
            UpdatePanelFechaIngresoTramiteDesde.Update();
            UpdatePanelFechaIngresoTramiteHasta.Update();
            UpdatePanelTitular.Update();
            UpdatePanelCentroOrigen.Update();
            UpdatePanelCentroDestino.Update();
            UpdatePanelRegionOrigen.Update();
            UpdatePanelComunaOrigen.Update();
            UpdatePanelRegionDestino.Update();
            UpdatePanelComunaDestino.Update();
            UpdatePanelSubtipoTramite.Update();
            UpdatePanelEstado.Update();


        }


        protected void Buscar_Click(object sender, EventArgs e)
        {

            MensajeSuperior.Text = "";
            PanelMensajeSuperior.Visible = false;
            UpdatePanelMensajeSuperior.Update();
            string error = "";

            TramiteRelocalizacion tramiteRelocalizacionRESA = new TramiteRelocalizacion();

            if (!NPert.Text.Trim().Equals(""))
            {
                tramiteRelocalizacionRESA.numPert = Convert.ToString(NPert.Text);
                error = solicitudDA.AdminSolicitudConcesionInfo(NPert.Text);
            }
            if (!FechaIngresoTramiteDesde.Text.Trim().Equals(""))
            {
                tramiteRelocalizacionRESA.fechaTramiteFiltroIni = Convert.ToDateTime(FechaIngresoTramiteDesde.Text);
            }
            if (!FechaIngresoTramiteHasta.Text.Trim().Equals(""))
            {
                tramiteRelocalizacionRESA.fechaTramiteFiltroFin = Convert.ToDateTime(FechaIngresoTramiteHasta.Text);
            }




            if (!TitularNombre.Text.Trim().Equals(""))
            {
                string[] split = TitularNombre.Text.Trim().Split(new Char[] { '-', ':' });
                if (split != null && split.Count() > 0)
                {
                    tramiteRelocalizacionRESA.titularFiltro = new Persona();

                    int i = 0;
                    bool result = int.TryParse(split[0], out i);

                    //LA PRIMERA PARTE ES UN NUMERO
                    if (result)
                    {
                        tramiteRelocalizacionRESA.titularFiltro.rutPersona = Convert.ToInt32(split[0]);
                    }
                    else  //LA PRIMERA PARTE ES UN STRING
                    {
                        tramiteRelocalizacionRESA.titularFiltro.nombreSolicitante = Convert.ToString(split[0]);
                    }
                }
            }

            if (Estado.SelectedIndex > 0)
            {
                tramiteRelocalizacionRESA.estadoFiltro = new ParametroGenerico(Convert.ToInt32(Estado.SelectedItem.Value));
            }
            if (Convert.ToInt32(SubtipoTramite.SelectedValue) > 0)
            {
                tramiteRelocalizacionRESA.tipoRelocalizacion = new ParametroGenerico(Convert.ToInt32(SubtipoTramite.SelectedValue));
            }
            if (!CodigoSiepCentroOrigen.Text.Trim().Equals(""))
            {
                string[] split = CodigoSiepCentroOrigen.Text.Trim().Split(new Char[] { ' ' });
                if (split != null && split.Count() > 0)
                {
                    tramiteRelocalizacionRESA.centroOrigenFiltro = new ParametroGenerico();
                    tramiteRelocalizacionRESA.centroOrigenFiltro.id = Convert.ToInt32(split[0]);
                }
            }
            if (!CodigoSiepCentroDestino.Text.Trim().Equals(""))
            {
                string[] split = CodigoSiepCentroDestino.Text.Trim().Split(new Char[] { ' ' });
                if (split != null && split.Count() > 0)
                {
                    tramiteRelocalizacionRESA.centroDestinoFiltro = new ParametroGenerico();
                    tramiteRelocalizacionRESA.centroDestinoFiltro.id = Convert.ToInt32(split[0]);
                }
            }



            if (Convert.ToInt32(RegionOrigen.SelectedValue) > 0)
            {
                tramiteRelocalizacionRESA.regionOrigen = new Region();
                tramiteRelocalizacionRESA.regionOrigen.id_region = Convert.ToInt32(RegionOrigen.SelectedValue);
            }
            if (Convert.ToInt32(ComunaOrigen.SelectedValue) > 0)
            {
                if (tramiteRelocalizacionRESA.regionOrigen == null)
                {
                    tramiteRelocalizacionRESA.regionOrigen = new Region();
                }
                tramiteRelocalizacionRESA.regionOrigen.comuna = new Comuna();
                tramiteRelocalizacionRESA.regionOrigen.comuna.id_comuna = Convert.ToInt32(ComunaOrigen.SelectedValue);
            }


            if (Convert.ToInt32(RegionDestino.SelectedValue) > 0)
            {
                tramiteRelocalizacionRESA.regionDestino = new Region();
                tramiteRelocalizacionRESA.regionDestino.id_region = Convert.ToInt32(RegionDestino.SelectedValue);
            }
            if (Convert.ToInt32(ComunaDestino.SelectedValue) > 0)
            {
                if (tramiteRelocalizacionRESA.regionDestino == null)
                {
                    tramiteRelocalizacionRESA.regionDestino = new Region();
                }
                tramiteRelocalizacionRESA.regionDestino.comuna = new Comuna();
                tramiteRelocalizacionRESA.regionDestino.comuna.id_comuna = Convert.ToInt32(ComunaDestino.SelectedValue);
            }


            if (tramiteRelocalizacionRESA.fechaTramiteFiltroIni != null && tramiteRelocalizacionRESA.fechaTramiteFiltroFin != null)
            {

                if (tramiteRelocalizacionRESA.fechaTramiteFiltroIni > tramiteRelocalizacionRESA.fechaTramiteFiltroFin)
                {
                    Page.Validators.Add(new ValidationError("FormBusquedaRelocalizacion", "\"Fecha Ingreso Trámite Desde\" debe ser posterior a \"Fecha Ingreso Trámite Hasta\""));

                }
                else
                {
                    DateTime fechaMax = tramiteRelocalizacionRESA.fechaTramiteFiltroIni.AddMonths(3);

                    if (tramiteRelocalizacionRESA.fechaTramiteFiltroFin > fechaMax)
                    {
                        Page.Validators.Add(new ValidationError("FormBusquedaRelocalizacion", "El rango máximo de búsqueda es de 3 meses"));
                    }
                }
            }

            if (error.Length > 0)
            {
                PanelMensajeSuperior.Visible = true;
                MensajeSuperior.Text = error;
            }
            else
            {

                
                
                    PanelMensajeSuperior.Visible = false;

                    tramiteRelocalizacionRESA.estadoTramite = new ParametroGenerico(rbEstadosGenerales.TRAMITE_RELOCALIZACION_TRAMITE);
                    Session["Filtro_Relocalizacion_Tramite"] = tramiteRelocalizacionRESA;

                    tramiteRelocalizacionRESA.estadoTramite = new ParametroGenerico(rbEstadosGenerales.TRAMITE_RELOCALIZACION_APROBADO);
                    Session["Filtro_Relocalizacion_Aprobada"] = tramiteRelocalizacionRESA;

                    tramiteRelocalizacionRESA.estadoTramite = new ParametroGenerico(rbEstadosGenerales.TRAMITE_RELOCALIZACION_RECHAZADO);
                    Session["Filtro_Relocalizacion_Rechazada"] = tramiteRelocalizacionRESA;

                    PanelMensajeSuperior.Visible = false;

                    CargaGrilla();
                    CargaGrillaAprobada();
                    CargaGrillaRechazada();
                
                    UpdatePanelMensajesValidaciones.Update();

                    if (GridRelocalizacionTramite.Rows.Count == 0 && GridRelocalizacionAprobada.Rows.Count == 0 && GridRelocalizacionRechazada.Rows.Count == 0)
                    {

                        msgGrilla.Text = "No se han encontrado resultados asociado a su filtro de búsqueda.";
                        Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        Content_msgGrilla.Visible = true;
                        UpdatePanel1.Update();


                    }
                    else
                    {

                        msgGrilla.Text = "";
                        Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        Content_msgGrilla.Visible = false;
                        UpdatePanel1.Update();

                    }

                
            }
        }



        //GRID RELOCALIZACION TRAMITE
        protected void CargaGrilla()
        {
            int pagina = 0;

            TramiteRelocalizacion tramiteRelocalizacion = new TramiteRelocalizacion();

            try
            {
                tramiteRelocalizacion = (TramiteRelocalizacion)Session["Filtro_Relocalizacion_Tramite"];
                pagina = tramiteRelocalizacion.pagina;
            }
            catch { };

            if (tramiteRelocalizacion == null)
            {
                tramiteRelocalizacion = new TramiteRelocalizacion();
                Session["Filtro_Relocalizacion_Tramite"] = tramiteRelocalizacion;
            }

            List<TramiteRelocalizacion> relocalizacionTramite = relocalizacionRESAService.ListarTramiteRelocalizacionAdmin_TramiteRESA(tramiteRelocalizacion, cantidadPaginacion);

            GridRelocalizacionTramite.DataSource = relocalizacionTramite;
            GridRelocalizacionTramite.DataBind();

            if (relocalizacionTramite != null && relocalizacionTramite.Count > 0)
            {

                ExportarGrilla1.Visible = true;

            }
            else
            {
                ExportarGrilla1.Visible = false;
            }


        }


        //GRID RELOCALIZACION TRAMITE
        protected void GridRelocalizacionTramite_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

            TramiteRelocalizacion tramiteRelocalizacion = (TramiteRelocalizacion)Session["Filtro_Relocalizacion_Tramite"];
            if (tramiteRelocalizacion == null)
            {
                tramiteRelocalizacion = new TramiteRelocalizacion();
            }

            tramiteRelocalizacion.pagina = e.NewPageIndex;
            Session["Filtro_Relocalizacion_Tramite"] = tramiteRelocalizacion;

            GridRelocalizacionTramite.PageIndex = e.NewPageIndex;
            GridRelocalizacionTramite.DataBind();
            CargaGrilla();
        }


        //GRID RELOCALIZACION TRAMITE
        protected void GridRelocalizacionTramite_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.Cells.Count > 12) //y no es una exportacion
            {

                GridView GridRelocalizacionTramite = (GridView)sender;
                int count = GridRelocalizacionTramite.Rows.Count;


                String rowspan = ((HiddenField)e.Row.FindControl("gColumnas")).Value;

                //PRIMERA FILA CON DATOS
                if (count == 0)
                {
                    e.Row.Cells[0].Visible = true;
                    e.Row.Cells[0].RowSpan = Convert.ToInt32(rowspan);

                    e.Row.Cells[12].Visible = true;
                    e.Row.Cells[12].RowSpan = Convert.ToInt32(rowspan);

                    e.Row.BackColor = colorPlanilla.COLOR_CELESTE;


                }
                else if (count > 0)
                {


                    GridViewRow previousRow = GridRelocalizacionTramite.Rows[e.Row.RowIndex - 1];

                    String numPertAnterior = ((HiddenField)previousRow.FindControl("gNumPert")).Value;
                    String numPert = ((HiddenField)e.Row.FindControl("gNumPert")).Value;


                    if (!numPertAnterior.Equals(numPert))
                    {

                        e.Row.Cells[0].Visible = true;
                        e.Row.Cells[0].RowSpan = Convert.ToInt32(rowspan);

                        e.Row.Cells[12].Visible = true;
                        e.Row.Cells[12].RowSpan = Convert.ToInt32(rowspan);


                        if (previousRow.BackColor == colorPlanilla.COLOR_CELESTE)
                        {
                            e.Row.BackColor = colorPlanilla.COLOR_BLANCO;
                        }
                        else
                        {
                            e.Row.BackColor = colorPlanilla.COLOR_CELESTE;
                        }

                    }
                    else
                    {
                        e.Row.Cells[0].RowSpan = 0;
                        e.Row.Cells[0].Visible = false;

                        e.Row.Cells[12].RowSpan = 0;
                        e.Row.Cells[12].Visible = false;

                        e.Row.BackColor = previousRow.BackColor;
                    }
                }



                //ESTADO DEL SSP
                String aEstadoSSP = Convert.ToString(((HiddenField)e.Row.FindControl("gEstadoSSP")).Value);

                //SIN SSP (ROJO)
                if (aEstadoSSP == null || aEstadoSSP.Equals(""))
                {
                    e.Row.Cells[11].BackColor = colorPlanilla.SIN_SSP;
                    e.Row.Cells[11].ForeColor = colorPlanilla.COLOR_BLANCO;
                } //CON SSP APRUEBA (VERDE)
                else if (aEstadoSSP != null && Convert.ToUInt32(aEstadoSSP.Trim()) == rbEstadosGenerales.APRUEBA)
                {
                    e.Row.Cells[11].BackColor = colorPlanilla.SSP_APROBADA;

                } //CON SSP RECHAZA (SIN COLOR)
                else if (aEstadoSSP != null && Convert.ToUInt32(aEstadoSSP.Trim()) == rbEstadosGenerales.RECHAZA)
                {

                }


                //BOTON REDEFINIR
                Boolean gDespliegaRedefinicion = Convert.ToBoolean(((HiddenField)e.Row.FindControl("gDespliegaRedefinicion")).Value);

                ImageButton boton_gRedefinir = (ImageButton)e.Row.FindControl("gRedefinir");
                if (gDespliegaRedefinicion && boton_gRedefinir != null)
                {

                    if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.REDEFINIR_TRAMITE))
                    {
                        //if (gDespliegaRedefinicion)
                        //{
                            boton_gRedefinir.Visible = true;
                        //}
                    }
                };

                //BOTON INTERCAMBIAR

                ImageButton boton_gIntercambiar = (ImageButton)e.Row.FindControl("gIntercambiar");
                if (boton_gRedefinir != null)
                {

                    if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.REDEFINIR_TRAMITE))
                    {
                        boton_gIntercambiar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar este trámite a Relocalizacion Ley?')");
                        boton_gIntercambiar.Visible = true;
                        
                    }
                };


                //BOTON ERRORES
                Boolean gDespliegaErrores = Convert.ToBoolean(((HiddenField)e.Row.FindControl("gDespliegaErrores")).Value);

                ImageButton boton_gErroresTramite = (ImageButton)e.Row.FindControl("gErroresTramite");
                if (boton_gErroresTramite != null)
                {
                    if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.VER_ERRORES_TRAMITE))
                    {
                        if (gDespliegaErrores)
                        {
                            boton_gErroresTramite.Visible = true;
                        }
                    }
                };


                //BOTON ALERTAS
                Boolean gDespliegaAlertas = Convert.ToBoolean(((HiddenField)e.Row.FindControl("gDespliegaAlertas")).Value);

                ImageButton boton_gErroresSector = (ImageButton)e.Row.FindControl("gErroresSector");
                if (boton_gErroresSector != null)
                {
                    if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.VER_ALERTAS_SOLICITUD))
                    {
                        if (gDespliegaAlertas)
                        {
                            boton_gErroresSector.Visible = true;
                        }
                    }
                };

                //Boton Eliminar

                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.ELIMINAR))
                    {
                        boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar?')");
                        boton_eliminar.Visible = true;
                    }
                }

                //BOTON MODIFICAR TRAMITE
                Boolean gDespliegaModificacion = Convert.ToBoolean(((HiddenField)e.Row.FindControl("gDespliegaModificacion")).Value);

                ImageButton boton_gModificarTramite = (ImageButton)e.Row.FindControl("gModificarTramite");
                if (boton_gModificarTramite != null)
                {
                    if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.MODIFICAR_TRAMITE))
                    {
                        if (gDespliegaModificacion)
                        {
                            boton_gModificarTramite.Visible = true;
                        }
                    }
                };


                //BOTON SOLICITUD
                HiddenField idSolConcesionSector = (HiddenField)e.Row.FindControl("gIdSolConcesion");
                LinkButton link_gModificar = (LinkButton)e.Row.FindControl("gModificar");

                Label gNumSector = (Label)e.Row.FindControl("gNumSector");
                if (Convert.ToInt32(idSolConcesionSector.Value) == 0)
                {
                    link_gModificar.Visible = false;
                    gNumSector.Visible = true;

                };

            };
        }


        //GRID RELOCALIZACION TRAMITE
        protected void GridRelocalizacionTramite_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int id = 0;
            SolicitudConcesion solicitudVer = null;

            switch (e.CommandName)
            {

                case "ModificarSector":

                    id = Convert.ToInt32(e.CommandArgument);

                    solicitudVer = new SolicitudConcesion();
                    solicitudVer = solicitudDA.ObtieneSolicitudConcesion(id, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                    solicitudVer.sectorRelocalizacion = relocalizacionRESAService.obtenerDetalleSector_SolicitudRESA(solicitudVer.idSolConcesion);



                    //RECALCULAR EL ESTADO DE LA SOLICITUD
                    try
                    {

                        if (solicitudVer != null)
                        {
                            solicitudDA.TramiteRecalculaEstados(solicitudVer.idSolConcesion);
                        }

                    }
                    catch
                    {

                    }



                    Session["SolicitudRelocalizacionRESA"] = (SolicitudConcesion)solicitudVer;
                    Response.Redirect("~/Solicitudes/RelocalizacionRESA/ingresarDocumentoRelocalizacionRESA.aspx");

                    break;

                case "VerTramite":

                    id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/Solicitudes/RelocalizacionRESA/verSolicitudRelocalizacionRESA.aspx?idTramiteRel=" + id);

                    break;


                case "redefinir":

                    id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/Solicitudes/RelocalizacionRESA/redefinirSolicitudRelocalizacionRESA.aspx?idTramiteRel=" + id);

                    break;

                case "VerErrores":

                    id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/Solicitudes/RelocalizacionRESA/erroresTramiteRelocalizacionRESA.aspx?idTramiteRel=" + id);

                    break;

                case "cambiarTipo":
                    id = Convert.ToInt32(e.CommandArgument);
                    cambiarTipoSolicitud(id, rbTipo.RELOCALIZACION_LEY);
                    break;

                case "erroresSector":

                    id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/Solicitudes/RelocalizacionRESA/alertasTramiteRelocalizacionRESA.aspx?idTramiteRel=" + id);
                    
                    break;


                case "ModificarTramite":

                    id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/Solicitudes/RelocalizacionRESA/ingresarSolicitudRelocalizacionRESA.aspx?idTramiteRel=" + id);

                    break;

                case "Eliminar":
                    id = Convert.ToInt32(e.CommandArgument);
                    EliminarRESA(id);
                    break;
            };
        }

        public void EliminarRESA(int idsolicitudcon)
        {
            CommonService common = new CommonService();
            if (common.EliminarSolicitudRel(idsolicitudcon, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario))
            {
                MensajeSuperior.Text = "Se ha eliminado la solicitud de relocalizacion exitosamente.";
                MensajeSuperior.Visible = true;
                PanelMensajeSuperior.Visible = true;
                UpdatePanelMensajeSuperior.Update();

                this.CargaGrilla();
            }
            else
            {
                MensajeSuperior.Text = "No se ha eliminado la solicitud de relocalizacion.";
                MensajeSuperior.Visible = true;
                PanelMensajeSuperior.Visible = true;
                UpdatePanelMensajeSuperior.Update();

                this.CargaGrilla();
            }

        }



        protected void cambiarTipoSolicitud(int Solcon, int nuevoTipotramite)
        {
            CommonService commonService = new CommonService();
            TramiteRelocalizacion tramiteRelocalizacionAux = relocalizacionRESAService.ObtenerTramiteRelocalizacionRESA(Solcon);
            bool resp = commonService.CambiarTipoSolicitudRel(Solcon, nuevoTipotramite);

            if (resp)
            {
               
                Session["PertRelocalizacionCambiado"] = tramiteRelocalizacionAux.numPert;
                Response.Redirect("~/Solicitudes/Relocalizacion/administrarSolicitudRelocalizacion.aspx?exchange=1");
            }
            else
            {
                MensajeSuperior.Text = "Ha ocurrido un error al intentar cambiar el tipo de relocalización.";
                MensajeSuperior.Visible = true;
                PanelMensajeSuperior.Visible = true;
                UpdatePanelMensajeSuperior.Update();

            }
        }


        //GRID RELOCALIZACION  APROBADA
        protected void CargaGrillaAprobada()
        {
            int pagina = 0;

            TramiteRelocalizacion tramiteRelocalizacion = new TramiteRelocalizacion();

            try
            {
                tramiteRelocalizacion = (TramiteRelocalizacion)Session["Filtro_Relocalizacion_Aprobada"];
                pagina = tramiteRelocalizacion.pagina;
            }
            catch { };

            if (tramiteRelocalizacion == null)
            {
                tramiteRelocalizacion = new TramiteRelocalizacion();
                Session["Filtro_Relocalizacion_Aprobada"] = tramiteRelocalizacion;
            }

            List<TramiteRelocalizacion> relocalizacionAprobada = relocalizacionRESAService.ListarTramiteRelocalizacionAdmin_AprobadaRESA(tramiteRelocalizacion, cantidadPaginacion);

            GridRelocalizacionAprobada.DataSource = relocalizacionAprobada;
            GridRelocalizacionAprobada.DataBind();

            if (relocalizacionAprobada != null && relocalizacionAprobada.Count > 0)
            {

                ExportarGrilla2.Visible = true;

            }
            else
            {
                ExportarGrilla2.Visible = false;
            }


        }


        //GRID RELOCALIZACION  APROBADA
        protected void GridRelocalizacionAprobada_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

            TramiteRelocalizacion tramiteRelocalizacion = (TramiteRelocalizacion)Session["Filtro_Relocalizacion_Aprobada"];
            if (tramiteRelocalizacion == null)
            {
                tramiteRelocalizacion = new TramiteRelocalizacion();
            }

            tramiteRelocalizacion.pagina = e.NewPageIndex;
            Session["Filtro_Relocalizacion_Aprobada"] = tramiteRelocalizacion;

            GridRelocalizacionAprobada.PageIndex = e.NewPageIndex;
            GridRelocalizacionAprobada.DataBind();
            CargaGrillaAprobada();
        }


        //GRID RELOCALIZACION APROBADA
        protected void GridRelocalizacionAprobada_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.Cells.Count > 12) //y no es una exportacion
            {

                GridView GridRelocalizacionTramite = (GridView)sender;
                int count = GridRelocalizacionTramite.Rows.Count;


                String rowspan = ((HiddenField)e.Row.FindControl("gColumnas")).Value;

                //PRIMERA FILA CON DATOS
                if (count == 0)
                {
                    e.Row.Cells[0].Visible = true;
                    e.Row.Cells[0].RowSpan = Convert.ToInt32(rowspan);

                    e.Row.Cells[12].Visible = true;
                    e.Row.Cells[12].RowSpan = Convert.ToInt32(rowspan);

                    e.Row.BackColor = colorPlanilla.COLOR_CELESTE;


                }
                else if (count > 0)
                {


                    GridViewRow previousRow = GridRelocalizacionTramite.Rows[e.Row.RowIndex - 1];

                    String numPertAnterior = ((HiddenField)previousRow.FindControl("gNumPert")).Value;
                    String numPert = ((HiddenField)e.Row.FindControl("gNumPert")).Value;


                    if (!numPertAnterior.Equals(numPert))
                    {

                        e.Row.Cells[0].Visible = true;
                        e.Row.Cells[0].RowSpan = Convert.ToInt32(rowspan);

                        e.Row.Cells[12].Visible = true;
                        e.Row.Cells[12].RowSpan = Convert.ToInt32(rowspan);


                        if (previousRow.BackColor == colorPlanilla.COLOR_CELESTE)
                        {
                            e.Row.BackColor = colorPlanilla.COLOR_BLANCO;
                        }
                        else
                        {
                            e.Row.BackColor = colorPlanilla.COLOR_CELESTE;
                        }

                    }
                    else
                    {
                        e.Row.Cells[0].RowSpan = 0;
                        e.Row.Cells[0].Visible = false;

                        e.Row.Cells[12].RowSpan = 0;
                        e.Row.Cells[12].Visible = false;

                        e.Row.BackColor = previousRow.BackColor;
                    }
                }



                //ESTADO DEL SSP
                String aEstadoSSP = Convert.ToString(((HiddenField)e.Row.FindControl("gEstadoSSP")).Value);

                //SIN SSP (ROJO)
                if (aEstadoSSP == null || aEstadoSSP.Equals(""))
                {
                    e.Row.Cells[11].BackColor = colorPlanilla.SIN_SSP;
                    e.Row.Cells[11].ForeColor = colorPlanilla.COLOR_BLANCO;
                } //CON SSP APRUEBA (VERDE)
                else if (aEstadoSSP != null && Convert.ToUInt32(aEstadoSSP.Trim()) == rbEstadosGenerales.APRUEBA)
                {
                    e.Row.Cells[11].BackColor = colorPlanilla.SSP_APROBADA;

                } //CON SSP RECHAZA (SIN COLOR)
                else if (aEstadoSSP != null && Convert.ToUInt32(aEstadoSSP.Trim()) == rbEstadosGenerales.RECHAZA)
                {

                }


                //BOTON REDEFINIR
                Boolean gDespliegaRedefinicion = Convert.ToBoolean(((HiddenField)e.Row.FindControl("gDespliegaRedefinicion")).Value);

                ImageButton boton_gRedefinir = (ImageButton)e.Row.FindControl("gRedefinir");
                if (gDespliegaRedefinicion && boton_gRedefinir != null)
                {
                    if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.REDEFINIR_TRAMITE))
                    {
                        if (gDespliegaRedefinicion)
                        {
                            boton_gRedefinir.Visible = true;
                        }
                    }
                };



                //BOTON ERRORES
                Boolean gDespliegaErrores = Convert.ToBoolean(((HiddenField)e.Row.FindControl("gDespliegaErrores")).Value);

                ImageButton boton_gErroresTramite = (ImageButton)e.Row.FindControl("gErroresTramite");
                if (boton_gErroresTramite != null)
                {
                    if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.VER_ERRORES_TRAMITE))
                    {
                        if (gDespliegaErrores)
                        {

                            boton_gErroresTramite.Visible = true;
                        }
                    }
                };


                //BOTON ALERTAS
                Boolean gDespliegaAlertas = Convert.ToBoolean(((HiddenField)e.Row.FindControl("gDespliegaAlertas")).Value);

                ImageButton boton_gErroresSector = (ImageButton)e.Row.FindControl("gErroresSector");
                if (boton_gErroresSector != null)
                {
                    if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.VER_ALERTAS_SOLICITUD))
                    {
                        if (gDespliegaAlertas)
                        {
                            boton_gErroresSector.Visible = true;
                        }
                    }
                };


                //BOTON MODIFICAR TRAMITE
                Boolean gDespliegaModificacion = Convert.ToBoolean(((HiddenField)e.Row.FindControl("gDespliegaModificacion")).Value);

                ImageButton boton_gModificarTramite = (ImageButton)e.Row.FindControl("gModificarTramite");
                if (boton_gModificarTramite != null)
                {
                    if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.MODIFICAR_TRAMITE))
                    {
                        if (gDespliegaModificacion)
                        {
                            boton_gModificarTramite.Visible = true;
                        }
                    }
                };






                //BOTON SOLICITUD
                LinkButton link_gModificar = (LinkButton)e.Row.FindControl("gModificar");

                if (permisosService.tieneAccesoA())
                {
                    link_gModificar.Attributes.Add("onclick", "javascript:return " + "confirm('Este tramite ya se encuentra aprobado, ¿desea continuar?')");
                }



            };
        }


        //GRID RELOCALIZACION APROBADA
        protected void GridRelocalizacionAprobada_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int id = 0;
            SolicitudConcesion solicitudVer = null;

            switch (e.CommandName)
            {

                case "ModificarSector":

                    id = Convert.ToInt32(e.CommandArgument);

                    solicitudVer = new SolicitudConcesion();
                    solicitudVer = solicitudDA.ObtieneSolicitudConcesion(id, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                    solicitudVer.sectorRelocalizacion = relocalizacionRESAService.obtenerDetalleSector_SolicitudRESA(solicitudVer.idSolConcesion);

                    //RECALCULAR EL ESTADO DE LA SOLICITUD
                    try
                    {

                        if (solicitudVer != null)
                        {
                            solicitudDA.TramiteRecalculaEstados(solicitudVer.idSolConcesion);
                        }

                    }
                    catch
                    {

                    }


                    Session["SolicitudRelocalizacionRESA"] = (SolicitudConcesion)solicitudVer;
                    Response.Redirect("~/Solicitudes/RelocalizacionRESA/ingresarDocumentoRelocalizacionRESA.aspx");

                    break;

                case "VerTramite":

                    id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/Solicitudes/RelocalizacionRESA/verSolicitudRelocalizacionRESA.aspx?idTramiteRel=" + id);

                    break;


                case "redefinir":

                    id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/Solicitudes/RelocalizacionRESA/redefinirSolicitudRelocalizacionRESA.aspx?idTramiteRel=" + id);

                    break;

                case "VerErrores":

                    id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/Solicitudes/RelocalizacionRESA/erroresTramiteRelocalizacionRESA.aspx?idTramiteRel=" + id);

                    break;

                case "erroresSector":

                    id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/Solicitudes/RelocalizacionRESA/alertasTramiteRelocalizacionRESA.aspx?idTramiteRel=" + id);

                    break;


                case "ModificarTramite":

                    id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/Solicitudes/RelocalizacionRESA/ingresarSolicitudRelocalizacionRESA.aspx?idTramiteRel=" + id);

                    break;
            };
        }





        //GRID RELOCALIZACION RECHAZADA
        protected void CargaGrillaRechazada()
        {
            int pagina = 0;

            TramiteRelocalizacion tramiteRelocalizacion = new TramiteRelocalizacion();

            try
            {
                tramiteRelocalizacion = (TramiteRelocalizacion)Session["Filtro_Relocalizacion_Rechazada"];
                pagina = tramiteRelocalizacion.pagina;
            }
            catch { };

            if (tramiteRelocalizacion == null)
            {
                tramiteRelocalizacion = new TramiteRelocalizacion();
                Session["Filtro_Relocalizacion_Rechazada"] = tramiteRelocalizacion;
            }

            List<TramiteRelocalizacion> relocalizacionRechazada = relocalizacionRESAService.ListarTramiteRelocalizacionAdmin_RechazadaRESA(tramiteRelocalizacion, cantidadPaginacion);

            GridRelocalizacionRechazada.DataSource = relocalizacionRechazada;
            GridRelocalizacionRechazada.DataBind();

            if (relocalizacionRechazada != null && relocalizacionRechazada.Count > 0)
            {

                ExportarGrilla3.Visible = true;

            }
            else
            {
                ExportarGrilla3.Visible = false;
            }


        }


        //GRID RELOCALIZACION RECHAZADA
        protected void GridRelocalizacionRechazada_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

            TramiteRelocalizacion tramiteRelocalizacion = (TramiteRelocalizacion)Session["Filtro_Relocalizacion_Rechazada"];
            if (tramiteRelocalizacion == null)
            {
                tramiteRelocalizacion = new TramiteRelocalizacion();
            }

            tramiteRelocalizacion.pagina = e.NewPageIndex;
            Session["Filtro_Relocalizacion_Rechazada"] = tramiteRelocalizacion;

            GridRelocalizacionRechazada.PageIndex = e.NewPageIndex;
            GridRelocalizacionRechazada.DataBind();
            CargaGrillaRechazada();
        }


        //GRID RELOCALIZACION RECHAZADA
        protected void GridRelocalizacionRechazada_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.Cells.Count > 12) //y no es una exportacion
            {

                GridView GridRelocalizacionTramite = (GridView)sender;
                int count = GridRelocalizacionTramite.Rows.Count;


                String rowspan = ((HiddenField)e.Row.FindControl("gColumnas")).Value;

                //PRIMERA FILA CON DATOS
                if (count == 0)
                {
                    e.Row.Cells[0].Visible = true;
                    e.Row.Cells[0].RowSpan = Convert.ToInt32(rowspan);

                    e.Row.Cells[12].Visible = true;
                    e.Row.Cells[12].RowSpan = Convert.ToInt32(rowspan);

                    e.Row.BackColor = colorPlanilla.COLOR_CELESTE;


                }
                else if (count > 0)
                {


                    GridViewRow previousRow = GridRelocalizacionTramite.Rows[e.Row.RowIndex - 1];

                    String numPertAnterior = ((HiddenField)previousRow.FindControl("gNumPert")).Value;
                    String numPert = ((HiddenField)e.Row.FindControl("gNumPert")).Value;


                    if (!numPertAnterior.Equals(numPert))
                    {

                        e.Row.Cells[0].Visible = true;
                        e.Row.Cells[0].RowSpan = Convert.ToInt32(rowspan);

                        e.Row.Cells[12].Visible = true;
                        e.Row.Cells[12].RowSpan = Convert.ToInt32(rowspan);


                        if (previousRow.BackColor == colorPlanilla.COLOR_CELESTE)
                        {
                            e.Row.BackColor = colorPlanilla.COLOR_BLANCO;
                        }
                        else
                        {
                            e.Row.BackColor = colorPlanilla.COLOR_CELESTE;
                        }

                    }
                    else
                    {
                        e.Row.Cells[0].RowSpan = 0;
                        e.Row.Cells[0].Visible = false;

                        e.Row.Cells[12].RowSpan = 0;
                        e.Row.Cells[12].Visible = false;

                        e.Row.BackColor = previousRow.BackColor;
                    }
                }




                //ESTADO DEL SSP
                String aEstadoSSP = Convert.ToString(((HiddenField)e.Row.FindControl("gEstadoSSP")).Value);

                //SIN SSP (ROJO)
                if (aEstadoSSP == null || aEstadoSSP.Equals(""))
                {
                    e.Row.Cells[11].BackColor = colorPlanilla.SIN_SSP;
                    e.Row.Cells[11].ForeColor = colorPlanilla.COLOR_BLANCO;
                } //CON SSP APRUEBA (VERDE)
                else if (aEstadoSSP != null && Convert.ToUInt32(aEstadoSSP.Trim()) == rbEstadosGenerales.APRUEBA)
                {
                    e.Row.Cells[11].BackColor = colorPlanilla.SSP_APROBADA;

                } //CON SSP RECHAZA (SIN COLOR)
                else if (aEstadoSSP != null && Convert.ToUInt32(aEstadoSSP.Trim()) == rbEstadosGenerales.RECHAZA)
                {

                }

                //BOTON REDEFINIR
                Boolean gDespliegaRedefinicion = Convert.ToBoolean(((HiddenField)e.Row.FindControl("gDespliegaRedefinicion")).Value);

                ImageButton boton_gRedefinir = (ImageButton)e.Row.FindControl("gRedefinir");
                if (gDespliegaRedefinicion && boton_gRedefinir != null)
                {
                    if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.REDEFINIR_TRAMITE))
                    {
                        if (gDespliegaRedefinicion)
                        {
                            boton_gRedefinir.Visible = true;
                        }
                    }
                };



                //BOTON ERRORES
                Boolean gDespliegaErrores = Convert.ToBoolean(((HiddenField)e.Row.FindControl("gDespliegaErrores")).Value);

                ImageButton boton_gErroresTramite = (ImageButton)e.Row.FindControl("gErroresTramite");
                if (boton_gErroresTramite != null)
                {
                    if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.VER_ERRORES_TRAMITE))
                    {
                        if (gDespliegaErrores)
                        {

                            boton_gErroresTramite.Visible = true;
                        }
                    }
                };


                //BOTON ALERTAS
                Boolean gDespliegaAlertas = Convert.ToBoolean(((HiddenField)e.Row.FindControl("gDespliegaAlertas")).Value);

                ImageButton boton_gErroresSector = (ImageButton)e.Row.FindControl("gErroresSector");
                if (boton_gErroresSector != null)
                {
                    if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.VER_ALERTAS_SOLICITUD))
                    {
                        if (gDespliegaAlertas)
                        {
                            boton_gErroresSector.Visible = true;
                        }
                    }
                };


                //BOTON MODIFICAR TRAMITE
                Boolean gDespliegaModificacion = Convert.ToBoolean(((HiddenField)e.Row.FindControl("gDespliegaModificacion")).Value);

                ImageButton boton_gModificarTramite = (ImageButton)e.Row.FindControl("gModificarTramite");
                if (boton_gModificarTramite != null)
                {
                    if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.MODIFICAR_TRAMITE))
                    {
                        if (gDespliegaModificacion)
                        {
                            boton_gModificarTramite.Visible = true;
                        }
                    }
                };


                //BOTON SOLICITUD
                LinkButton link_gModificar = (LinkButton)e.Row.FindControl("gModificar");

                if (permisosService.tieneAccesoA())
                {
                    link_gModificar.Attributes.Add("onclick", "javascript:return " + "confirm('Este tramite ya se encuentra rechazado, ¿desea continuar?')");
                }




            };
        }


        //GRID RELOCALIZACION RECHAZADA
        protected void GridRelocalizacionRechazada_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int id = 0;
            SolicitudConcesion solicitudVer = null;

            switch (e.CommandName)
            {

                case "ModificarSector":

                    id = Convert.ToInt32(e.CommandArgument);

                    solicitudVer = new SolicitudConcesion();
                    solicitudVer = solicitudDA.ObtieneSolicitudConcesion(id, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                    solicitudVer.sectorRelocalizacion = relocalizacionRESAService.obtenerDetalleSector_SolicitudRESA(solicitudVer.idSolConcesion);

                    //RECALCULAR EL ESTADO DE LA SOLICITUD
                    try
                    {

                        if (solicitudVer != null)
                        {
                            solicitudDA.TramiteRecalculaEstados(solicitudVer.idSolConcesion);
                        }

                    }
                    catch
                    {

                    }


                    Session["SolicitudRelocalizacionRESA"] = (SolicitudConcesion)solicitudVer;
                    Response.Redirect("~/Solicitudes/RelocalizacionRESA/ingresarDocumentoRelocalizacionRESA.aspx");

                    break;

                case "VerTramite":

                    id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/Solicitudes/RelocalizacionRESA/verSolicitudRelocalizacionRESA.aspx?idTramiteRel=" + id);

                    break;


                case "redefinir":

                    id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/Solicitudes/RelocalizacionRESA/redefinirSolicitudRelocalizacionRESA.aspx?idTramiteRel=" + id);

                    break;

                case "VerErrores":

                    id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/Solicitudes/RelocalizacionRESA/erroresTramiteRelocalizacionRESA.aspx?idTramiteRel=" + id);

                    break;


                case "erroresSector":

                    id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/Solicitudes/RelocalizacionRESA/alertasTramiteRelocalizacionRESA.aspx?idTramiteRel=" + id);

                    break;


                case "ModificarTramite":

                    id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/Solicitudes/RelocalizacionRESA/ingresarSolicitudRelocalizacionRESA.aspx?idTramiteRel=" + id);

                    break;
            };
        }



        protected void ExportarGrilla1_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();
            string nom_grilla = "solicitudRelocalizacionLeyConcesionEnTramite";
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "solicitudRelocalizacionLeyConcesionEnTramite":
                    GridRelocalizacionTramite.Columns.RemoveAt(12);
                    grilla = GridRelocalizacionTramite;
                    ngrilla = "solicitudRelocalizacionLeyConcesionEnTramite.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void ExportarGrilla2_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();
            string nom_grilla = "solicitudRelocalizacionLeyConcesionAprobada";
            string ngrilla = "";

            CargaGrillaAprobada();

            switch (nom_grilla)
            {
                case "solicitudRelocalizacionLeyConcesionAprobada":
                    GridRelocalizacionAprobada.Columns.RemoveAt(12);
                    grilla = GridRelocalizacionAprobada;
                    ngrilla = "solicitudRelocalizacionLeyConcesionAprobada.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void ExportarGrilla3_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();
            string nom_grilla = "solicitudRelocalizacionLeyConcesionRechazada";
            string ngrilla = "";

            CargaGrillaRechazada();

            switch (nom_grilla)
            {
                case "solicitudRelocalizacionLeyConcesionRechazada":
                    GridRelocalizacionRechazada.Columns.RemoveAt(12);
                    grilla = GridRelocalizacionRechazada;
                    ngrilla = "solicitudRelocalizacionLeyConcesionRechazada.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);

        }
    }
}