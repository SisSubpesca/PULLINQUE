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
using SubPesca.Utilidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.modificacion;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;
using LogicaNegocio.cl.subpesca.rb.reportes;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;

namespace SubPesca.Solicitudes.ModificacionAmerb
{
    public partial class administrarSolicitudModificacionAmerb : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        RegionDA regionDA = new RegionDA();
        ProvinciaDA provinciaDA = new ProvinciaDA();
        ComunaDA comunaDA = new ComunaDA();
        SolicitudDA solicitudDA = new SolicitudDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        PermisosService permisosService = new PermisosService();
        ReporteDA reporteDA = new ReporteDA();

        protected void setearModulo()
        {
            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ADMINSITRAR_SOLICITUD_MODIFICACION_AMERB };
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            // PAGE LOAD
            if (!Page.IsPostBack)
            {
                setearModulo();

                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (usuario_logeado == null)
                {
                    Response.Redirect("~/ingreso.aspx");
                }

                Initialize_Form();

                // Cargamos la grilla
                CargaGrilla();
            }

        }

        protected void Initialize_Form()
        {
            // Cargamos los combobox
            Initialize_Comboboxs();

        }


        protected void Initialize_Comboboxs()
        {

            Carga_Combobox("Region");
            Region.SelectedValue = "0";

            Carga_Combobox("Provincia");
            Provincia.SelectedValue = "0";

            Carga_Combobox("Comuna");
            Comuna.SelectedValue = "0";

            Carga_Combobox("SubtipoTramite");
            SubtipoTramite.SelectedValue = "0";
            Estado.SelectedValue = "0";
        }

        protected void Carga_Combobox(string combobox)
        {


            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

            switch (combobox)
            {

                case "Region":
                    // Cargamos el combobox: Region
                    Region.Items.Clear();
                    Region.DataSource = regionDA.ListarRegion(0);
                    Region.DataTextField = "Region";
                    Region.DataValueField = "IdRegion";
                    Region.DataBind();
                    Region.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                case "Provincia":
                    // Cargamos el combobox: Provincia
                    Provincia.Items.Clear();
                    if (Convert.ToInt32(Region.SelectedValue) > 0)
                    {
                        Provincia.DataSource = parametroGenericoDA.ListarProvinciaReg(0, Convert.ToInt32(Region.SelectedValue));
                        Provincia.DataTextField = "descripcion";
                        Provincia.DataValueField = "id";
                        Provincia.DataBind();
                    };
                    Provincia.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    break;
                case "Comuna":
                    // Cargamos el combobox: Comuna
                    Comuna.Items.Clear();
                    if (Convert.ToInt32(Provincia.SelectedValue) > 0)
                    {
                        Comuna.DataSource = parametroGenericoDA.ListarComunaDataTable(0, Convert.ToInt32(Provincia.SelectedValue));
                        Comuna.DataTextField = "Comuna";
                        Comuna.DataValueField = "IdComuna";
                        Comuna.DataBind();
                    };
                    Comuna.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    break;

                case "SubtipoTramite":
                    SubtipoTramite.Items.Clear();
                    //falta el numoero correcto de administrarsolicitudexperimentalconcesion
                    SubtipoTramite.Items.Add(new ListItem("Solicitud Modificación Amerb: Ampliación de Superficie", Convert.ToString(rbTipo.MOD_AMERB_AMPLIA_SUPERFICIE)));
                    SubtipoTramite.Items.Add(new ListItem("Solicitud Modificación Amerb: Reducción de Superficie", Convert.ToString(rbTipo.MOD_AMERB_REDUCE_SUPERFICIE)));
                    SubtipoTramite.Items.Add(new ListItem("Solicitud Modificación Amerb: Especie", Convert.ToString(rbTipo.MOD_AMERB_ESPECIE)));
                    SubtipoTramite.Items.Add(new ListItem("Solicitud Modificación Amerb: Proyecto Técnico", Convert.ToString(rbTipo.MOD_AMERB_PT)));
                    SubtipoTramite.Items.Add(new ListItem("Solicitud Modificación Amerb: Regularización", Convert.ToString(rbTipo.MOD_AMERB_REGULARIZACION)));

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

        protected void Region_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("Provincia");
            Carga_Combobox("Comuna");
        }

        protected void Provincias_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("Comuna");
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
            NPert.Text = "";
            NombreCentro.Text = "";
            TitularNombre.Text = "";
            FechaDesde.Text = "";
            FechaHasta.Text = "";
            Region.SelectedValue = "0";
            Provincia.SelectedValue = "0";
            Comuna.SelectedValue = "0";
            Estado.SelectedValue = "0";
            SubtipoTramite.SelectedValue = "0";

            UpdatePanelSubtipoTramite.Update();
            UpdatePanelEstado.Update();

            string script = @"<script type='text/javascript'>invoca_calendarios('administrarSolicitudModificacion');</script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "mensaje_cargado", script, false);


        }

        protected void GridSolicitudesAdm_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

            SolicitudConcesion solicitudFiltro = new SolicitudConcesion();
            solicitudFiltro = (SolicitudConcesion)Session["Filtro_SolicModificacionAmerb"];
            if (solicitudFiltro == null)
            {
                solicitudFiltro = new SolicitudConcesion();
            }

            solicitudFiltro.pagina = e.NewPageIndex;
            Session["Filtro_SolicModificacionAmerb"] = solicitudFiltro;

            GridSolicitudesAdm.PageIndex = e.NewPageIndex;
            GridSolicitudesAdm.DataBind();
            CargaGrilla();
        }

        protected void GridConcesAdm_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

            SolicitudConcesion solicitudFiltro = new SolicitudConcesion();
            solicitudFiltro = (SolicitudConcesion)Session["Filtro_SolicModificacionAmerb"];
            if (solicitudFiltro == null)
            {
                solicitudFiltro = new SolicitudConcesion();
            }

            solicitudFiltro.pagina = e.NewPageIndex;
            Session["Filtro_SolicModificacionAmerb"] = solicitudFiltro;

            GridConcesAdm.PageIndex = e.NewPageIndex;
            GridConcesAdm.DataBind();
            CargaGrilla();
        }

        protected void GridSolModRechazada_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

            SolicitudConcesion solicitudFiltro = new SolicitudConcesion();
            solicitudFiltro = (SolicitudConcesion)Session["Filtro_SolicModificacionAmerb"];
            if (solicitudFiltro == null)
            {
                solicitudFiltro = new SolicitudConcesion();
            }

            solicitudFiltro.pagina = e.NewPageIndex;
            Session["Filtro_SolicModificacionAmerb"] = solicitudFiltro;

            GridViewRechazada.PageIndex = e.NewPageIndex;
            GridViewRechazada.DataBind();
            CargaGrilla();
        }

        // ACCIONES DE BOTONES y COMBOBOXS
        protected void Filtrar_Click(object sender, EventArgs e)
        {
            /* Hashtable HT_ModReportes = (Hashtable)Session["Modulo_Reportes"];
             Hashtable HT_ListEstados = (Hashtable)HT_ModReportes["ListEstados"];
             HT_ListEstados["id_region"] = Convert.ToInt32(Regiones.SelectedValue);
             HT_ListEstados["id_provincia"] = Convert.ToInt32(Provincias.SelectedValue);
             HT_ListEstados["id_comuna"] = Convert.ToInt32(Comunas.SelectedValue);
             HT_ListEstados["id_tiposolicitud"] = Convert.ToInt32(TiposSolicitud.SelectedValue);
             HT_ModReportes["ListEstados"] = (Hashtable)HT_ListEstados;
             Session["Modulo_Reportes"] = (Hashtable)HT_ModReportes;*/

            CargaGrilla();

        }
        protected void FiltrarCargaGrilla(object sender, EventArgs e)
        {
            SolicitudConcesion solicitudFiltro = new SolicitudConcesion();
            string error = "";

            if (!NPert.Text.Trim().Equals(""))
            {
                solicitudFiltro.numPert = Convert.ToString(NPert.Text);
                error = solicitudDA.AdminSolicitudConcesionInfo(NPert.Text);
            }
            /*if (!CodigoCentro.Text.Equals("") && Convert.ToInt32(CodigoCentro.Text) > 0)
            {
                 solicitudFiltro.unidadEspacial = new UnidadEspacial();
                 solicitudFiltro.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                 solicitudFiltro.unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToInt32(CodigoCentro.Text);
            }*/


            if (!TitularNombre.Text.Trim().Equals(""))
            {
                string[] split = TitularNombre.Text.Trim().Split(new Char[] { '-', ':' });
                if (split != null && split.Count() > 0)
                {
                    solicitudFiltro.titularFiltro = new Persona();

                    int i = 0;
                    bool result = int.TryParse(split[0], out i);

                    //LA PRIMERA PARTE ES UN NUMERO
                    if (result)
                    {
                        solicitudFiltro.titularFiltro.rutPersona = Convert.ToInt32(split[0]);
                    }
                    else  //LA PRIMERA PARTE ES UN STRING
                    {
                        solicitudFiltro.titularFiltro.nombreSolicitante = Convert.ToString(split[0]);
                    }
                }
            }

            if (Region.SelectedIndex > 0)
            {
                solicitudFiltro.region = new ParametroGenerico(Convert.ToInt32(Region.SelectedItem.Value));
            }
            if (Comuna.SelectedIndex > 0)
            {
                solicitudFiltro.comunaFiltro = new ParametroGenerico(Convert.ToInt32(Comuna.SelectedItem.Value));
            }
            if (Provincia.SelectedIndex > 0)
            {
                solicitudFiltro.provincia = new ParametroGenerico(Convert.ToInt32(Provincia.SelectedItem.Value));
            }
            if (Estado.SelectedIndex > 0)
            {
                solicitudFiltro.estadoActual = new ParametroGenerico(Convert.ToInt32(Estado.SelectedItem.Value));
            }
            if (SubtipoTramite.SelectedIndex > 0)
            {
                solicitudFiltro.tipoModificacion = new ParametroGenerico(Convert.ToInt32(SubtipoTramite.SelectedItem.Value));
            }
            if (!FechaDesde.Text.Equals(""))
            {
                solicitudFiltro.fechaRangoFiltro1 = Convert.ToDateTime(FechaDesde.Text);
            }
            if (!FechaHasta.Text.Equals(""))
            {
                solicitudFiltro.fechaRangoFiltro2 = Convert.ToDateTime(FechaHasta.Text);
            }
            if (error.Length > 0)
            {
                PanelMensajeSuperior.Visible = true;
                MensajeSuperior.Text = error;
            }
            else
            {
                PanelMensajeSuperior.Visible = false;

                solicitudFiltro.pagina = new int();
                solicitudFiltro.pagina = 0;

                Session["Filtro_SolicModificacionAmerb"] = solicitudFiltro;
                CargaGrilla();
            }
        }


        protected void CargaGrilla()
        {
            int pagina = 0;
            ExportarGrilla1.Visible = false;
            ExportarGrilla2.Visible = false;
            ExportarGrilla3.Visible = false;

            List<SolicitudConcesion> respSolicitudes = new List<SolicitudConcesion>();
            List<SolicitudConcesion> respSolicitudesAutorizadas = new List<SolicitudConcesion>();
            List<SolicitudConcesion> respSolicitudesRechazadas = new List<SolicitudConcesion>();

            SolicitudConcesion solicitudFiltro = new SolicitudConcesion();

            try
            {
                solicitudFiltro = (SolicitudConcesion)Session["Filtro_SolicModificacionAmerb"];
                pagina = solicitudFiltro.pagina;
            }
            catch { };

            if (solicitudFiltro == null)
            {
                solicitudFiltro = new SolicitudConcesion();
                Session["Filtro_SolicModificacionAmerb"] = solicitudFiltro;
            }


            solicitudFiltro.tipoUnidadEspacial = new ParametroGenerico(rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB);
            solicitudFiltro.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB);

            respSolicitudes = solicitudDA.ListarSolicitud_UE_ModAdmin_Tramite(solicitudFiltro);
            respSolicitudesAutorizadas = solicitudDA.ListarSolicitud_UE_ModAdmin_Aprobada(solicitudFiltro);
            respSolicitudesRechazadas = solicitudDA.ListarSolicitud_UE_ModAdmin_Rechazada(solicitudFiltro);

            if ((respSolicitudes == null || respSolicitudes.Count <= 0) && (respSolicitudesAutorizadas == null || respSolicitudesAutorizadas.Count <= 0) && (respSolicitudesRechazadas == null || respSolicitudesRechazadas.Count <= 0))
            {
                msgGrilla.Text = "No se han encontrado resultados asociado a su filtro de búsqueda.";
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                Content_msgGrilla.Visible = true;
                UpdatePanel1.Update();

                respSolicitudes = new List<SolicitudConcesion>();
                GridConcesAdm.DataSource = respSolicitudes;
                GridConcesAdm.DataBind();

                respSolicitudes = new List<SolicitudConcesion>();
                GridSolicitudesAdm.DataSource = respSolicitudesAutorizadas;
                GridSolicitudesAdm.DataBind();

                respSolicitudes = new List<SolicitudConcesion>();
                GridViewRechazada.DataSource = respSolicitudesRechazadas;
                GridViewRechazada.DataBind();
            }
            else
            {
                lnk_EnTramite.CssClass = "tab1_selected";
                UpdatePanelPestanaEnTramite.Update();

                lnk_Aprobada.CssClass = "tab2";
                UpdatePanelPestanaAprobada.Update();

                lnk_Rechazada.CssClass = "tab3";
                UpdatePanelPestanaRechazada.Update();

                //SOLICITUD EN TRAMITE (Solicitudes de Modificación que no tienen resol. de ssp)

                GridConcesAdm.PageIndex = pagina;
                GridConcesAdm.DataSource = respSolicitudes;
                GridConcesAdm.DataBind();

                //SOLICITUD AUTORIZADA (Solicitudes de Modificación que no tienen resol. de ssp "aprobada")

                GridSolicitudesAdm.DataSource = respSolicitudesAutorizadas;
                GridSolicitudesAdm.DataBind();

                //SOLICITUD RECHAZADA (Solicitudes de Modificación que no tienen resol. de ssp "rechazada")

                GridViewRechazada.DataSource = respSolicitudesRechazadas;
                GridViewRechazada.DataBind();

                PanelEnTramite.Visible = true;
                UpdatePanelEnTramite.Update();

                PanelRechazada.Visible = false;
                UpdatePanelRechazada.Update();

                PanelAprobada.Visible = false;
                UpdatePanelAprobada.Update();

                Content_msgGrilla.Visible = false;
                UpdatePanel1.Update();


                if (respSolicitudes != null && respSolicitudes.Count > 0) {
                    ExportarGrilla1.Visible = true;
                }
                if (respSolicitudesAutorizadas != null && respSolicitudesAutorizadas.Count > 0) {
                    ExportarGrilla2.Visible = true;
                }
                if (respSolicitudesRechazadas != null && respSolicitudesRechazadas.Count > 0) {
                    ExportarGrilla3.Visible = true;
                }
                
            }
        }



        protected void GridSolicitudesAdm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idSolConces = 0;
            SolicitudConcesion solicitudVer = null;

            switch (e.CommandName)
            {
                case "Modificar":

                    idSolConces = Convert.ToInt32(e.CommandArgument);

                    solicitudVer = new SolicitudConcesion();
                    solicitudVer = solicitudDA.ObtieneSolicitudConcesionMod(idSolConces, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                    solicitudVer.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB);

                    //RECALCULAR EL ESTADO DE LA SOLICITUD
                    try
                    {

                        if (solicitudVer != null)
                        {
                            solicitudDA.TramiteRecalculaEstados(idSolConces);
                        }

                    }
                    catch
                    {

                    }

                    Session["SolicitudModificacionAmerb"] = (SolicitudConcesion)solicitudVer;
                    Response.Redirect("~/Solicitudes/ModificacionAmerb/ingresarDocumentoModificacionAmerb.aspx");

                    break;

                case "Ver":

                    idSolConces = Convert.ToInt32(e.CommandArgument);

                    solicitudVer = new SolicitudConcesion();
                    solicitudVer = solicitudDA.ObtieneSolicitudConcesionMod(idSolConces, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                    solicitudVer.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB);
                    solicitudVer.tieneAsignadaSolicitud = false;

                    //RECALCULAR EL ESTADO DE LA SOLICITUD
                    try
                    {

                        if (solicitudVer != null)
                        {
                            solicitudDA.TramiteRecalculaEstados(idSolConces);
                        }

                    }
                    catch
                    {

                    }

                    Session["SolicitudModificacionAmerb"] = (SolicitudConcesion)solicitudVer;
                    Response.Redirect("~/Solicitudes/ModificacionAmerb/identificacionTitularModificacionAmerb.aspx");

                    break;

             
            };
        }
       
        protected void GridConcesAdm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SolicitudConcesion solicitudVer = null;
            switch (e.CommandName)
            {
                case "Modificar":

                    int idSolConces = Convert.ToInt32(e.CommandArgument);

                    solicitudVer = new SolicitudConcesion();
                    solicitudVer = solicitudDA.ObtieneSolicitudConcesionMod(idSolConces, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                    solicitudVer.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB);

                    //RECALCULAR EL ESTADO DE LA SOLICITUD
                    try
                    {

                        if (solicitudVer != null)
                        {
                            solicitudDA.TramiteRecalculaEstados(idSolConces);
                        }

                    }
                    catch
                    {

                    }

                    Session["SolicitudModificacionAmerb"] = (SolicitudConcesion)solicitudVer;
                    Response.Redirect("~/Solicitudes/ModificacionAmerb/ingresarDocumentoModificacionAmerb.aspx");

                    break;

                case "Redefinir":

                    idSolConces = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/Solicitudes/ModificacionAmerb/redefinirTramiteModificacionAmerb.aspx?idSolConces=" + idSolConces);
                    break;

                case "Eliminar":

                    idSolConces = Convert.ToInt32(e.CommandArgument);
                    eliminarSolicitudModificacion(idSolConces);

                    break;

                case "ErroresSolicitud":

                    idSolConces = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/Solicitudes/ModificacionAmerb/erroresTramiteModificacionAmerb.aspx?idSolConces=" + idSolConces);
                    break;

                case "Ver":
                    idSolConces = Convert.ToInt32(e.CommandArgument);

                    solicitudVer = new SolicitudConcesion();
                    solicitudVer = solicitudDA.ObtieneSolicitudConcesionMod(idSolConces, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                    solicitudVer.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB);
                    solicitudVer.tieneAsignadaSolicitud = false;

                    //RECALCULAR EL ESTADO DE LA SOLICITUD
                    try
                    {

                        if (solicitudVer != null)
                        {
                            solicitudDA.TramiteRecalculaEstados(idSolConces);
                        }

                    }
                    catch
                    {

                    }

                    Session["SolicitudModificacionAmerb"] = (SolicitudConcesion)solicitudVer;
                    Response.Redirect("~/Solicitudes/ModificacionAmerb/identificacionTitularModificacionAmerb.aspx");
                    break;

            };
        }

        private void eliminarSolicitudModificacion(int idSolConces)
        {
            CommonService common = new CommonService();
            SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();
            bool resp = common.EliminarSolicitudRel(idSolConces, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

            if (resp)
            {
                msgGrilla.Text = "Se ha eliminado la solicitud de modificación exitosamente.";
                Content_msgGrilla.Visible = true;

            }
            else
            {
                msgGrilla.Text = "No se ha eliminado la solicitud de modificación.";
                Content_msgGrilla.Visible = true;
            }
            Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            
            UpdatePanel1.Update();

            this.CargaGrilla();
        }

        protected void GridSolModRechazada_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SolicitudConcesion solicitudVer = null;
            switch (e.CommandName)
            {
                case "Modificar":

                    int idSolConces = Convert.ToInt32(e.CommandArgument);

                    solicitudVer = new SolicitudConcesion();
                    solicitudVer = solicitudDA.ObtieneSolicitudConcesionMod(idSolConces, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                    solicitudVer.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB);

                    //RECALCULAR EL ESTADO DE LA SOLICITUD
                    try
                    {

                        if (solicitudVer != null)
                        {
                            solicitudDA.TramiteRecalculaEstados(idSolConces);
                        }

                    }
                    catch
                    {

                    }

                    Session["SolicitudModificacionAmerb"] = (SolicitudConcesion)solicitudVer;
                    Response.Redirect("~/Solicitudes/ModificacionAmerb/ingresarDocumentoModificacionAmerb.aspx");

                    break;

                case "Ver":
                    idSolConces = Convert.ToInt32(e.CommandArgument);

                    solicitudVer = new SolicitudConcesion();
                    solicitudVer = solicitudDA.ObtieneSolicitudConcesionMod(idSolConces, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                    solicitudVer.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB);
                    solicitudVer.tieneAsignadaSolicitud = false;

                    //RECALCULAR EL ESTADO DE LA SOLICITUD
                    try
                    {

                        if (solicitudVer != null)
                        {
                            solicitudDA.TramiteRecalculaEstados(idSolConces);
                        }

                    }
                    catch
                    {

                    }

                    Session["SolicitudModificacionAmerb"] = (SolicitudConcesion)solicitudVer;
                    Response.Redirect("~/Solicitudes/ModificacionAmerb/identificacionTitularModificacionAmerb.aspx");
                    break;
            };
        }

        protected void GridSolicitudesAdm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_modificar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
                    {
                        boton_modificar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea modificar una Solicitud de Modificación que ya tiene su Resol. SSP Aprobada?')");
                        boton_modificar.Visible = true;
                    }
                };
                
                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    boton_ver.Visible = true;
                };

            };
        }

        protected void GridConcesAdm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_modificar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
                    {
                        boton_modificar.Visible = true;
                    }
                };


                ImageButton boton_redefinir = (ImageButton)e.Row.FindControl("gRedefinir");
                if (boton_modificar != null)
                {
                    if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_MODIFICACION_AMERB }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
                    {
                        boton_redefinir.Visible = true;
                    }
                };

                ImageButton boton_error = (ImageButton)e.Row.FindControl("gError");
                if (boton_error != null)
                {
                    boton_error.Visible = true;
                };

                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.ELIMINAR))
                    {
                        boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea borrar la Solicitud de Modificación?')");
                        boton_eliminar.Visible = true;
                    }
                };

                
                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    boton_ver.Visible = true;
                };
                
            }
        }

        protected void GridSolModRechazada_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_modificar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
                    {
                        boton_modificar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea modificar una Solicitud de Modificación que ya tiene su Resol. SSP Rechazada?')");
                        boton_modificar.Visible = true;
                    }
                };

                
                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    boton_ver.Visible = true;
                };
            }
        }


        protected void ExportarGrilla1_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();

            CargaGrilla();

            int cantidad = GridConcesAdm.Columns.Count;

            if (cantidad > 1)
            {
                cantidad = cantidad - 1;
            }

            GridConcesAdm.Columns.RemoveAt(cantidad);
            grilla = GridConcesAdm;

            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export("AdministradorSolicitudesTramite.xls", grilla);
        }



        protected void ExportarGrilla2_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();

            CargaGrilla();

            int cantidad = GridSolicitudesAdm.Columns.Count;

            if (cantidad > 1)
            {
                cantidad = cantidad - 1;
            }

            GridSolicitudesAdm.Columns.RemoveAt(cantidad);
            grilla = GridSolicitudesAdm;

            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export("AdministradorSolicitudesAprobada.xls", grilla);
        }



        protected void ExportarGrilla3_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();

            CargaGrilla();

            int cantidad = GridViewRechazada.Columns.Count;

            if (cantidad > 1)
            {
                cantidad = cantidad - 1;
            }

            GridViewRechazada.Columns.RemoveAt(cantidad);
            grilla = GridViewRechazada;

            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export("AdministradorSolicitudesRechazada.xls", grilla);
        }


    }
}