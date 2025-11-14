using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using Datos.Entidades.Resolucion;
using System.Collections;
using LogicaNegocio.cl.subpesca.rb.resolucion;
using LogicaNegocio.cl.subpesca.rb.servicios.resoluciones;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;

namespace SubPesca.Resoluciones
{
    public partial class busquedaResoluciones : System.Web.UI.Page
    {
        private  Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        private PermisosService permisosService = new PermisosService();
        private String erroresSumary = "ValidationSummary";
        private ResolucionDA resolucionDA = new ResolucionDA();
        private ResolucionService resolucionService = new ResolucionService();
        private ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();
        private SolicitudDA solicitudDA = new SolicitudDA();
        private RelocalizacionService relocalizacionService = new RelocalizacionService();
        private RelocalizacionRESAService relocalizacionRESAService = new RelocalizacionRESAService();

        protected void setearModulo()
        {

            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_RESOLUCION };

        }


        protected void Page_Load(object sender, EventArgs e)
        {

            // PAGE LOAD
            if (!Page.IsPostBack)
            {
                setearModulo();

                ValidationSummaryErrores.ValidationGroup = erroresSumary;


                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (usuario_logeado == null)
                {
                    Response.Redirect("~/ingreso.aspx");

                }


                //BOTON DE INGRESO O MODIFICACION
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], this.usuario_logeado, null, rbAccion.EDITAR))
                {
                    //PanelFormularioIngreso.Visible = true;
                    //UpdatePanelFormularioIngreso.Update();
                }
                else
                {
                    Response.Redirect("~/ingreso.aspx");
                }



                if (PreviousPage != null && PreviousPage is ingresarResoluciones)
                {

                    MensajeSuperior.Text = ((ingresarResoluciones)PreviousPage).MensajeRegistro;
                    if (!MensajeSuperior.Text.Equals(""))
                    {
                        PanelMensajeSuperior.Visible = true;
                        UpdatePanelMensajeSuperior.Update();
                    }

                }



                // Inicializamos el formulario
                Initialize_Form();

            }

            string script = "calendario('" + FechaDesde.ClientID + "','" + imgFechaDesde.ClientID + "');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaDesde.ClientID, script.ToString(), true);

            script = "calendario('" + FechaHasta.ClientID + "','" + imgFechaHasta.ClientID + "');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaHasta.ClientID, script.ToString(), true);

        }





        protected void Initialize_Form()
        {
            // Cargamos los combobox
            Initialize_Comboboxs();

        }


        protected void Initialize_Comboboxs()
        {

            
            Carga_Combobox("Origen");
            Origen.SelectedValue = "0";

          

        }


        protected void Carga_Combobox(string combobox)
        {


            switch (combobox)
            {

                case "Origen":

                    Origen.Items.Clear();


                    List<ParametroGenerico> origenes = resolucionService.ListarPosiblesOrigenes();

                    origenes.Sort();

                    if (origenes != null)
                    {
                        foreach (ParametroGenerico origen in origenes)
                        {
                            Origen.Items.Add(new ListItem(origen.descripcion, Convert.ToString(origen.id)));
                        }
                    }


                    Origen.DataBind();
                    Origen.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    UpdatePanelOrigen.Update();



                    break;



            };

        }




        //GRID RESOLUCIONES
        protected void CargaGrilla()
        {

            int pagina = 0;
            ExportarGrilla.Visible = false;

            Resolucion resolucionFiltro = new Resolucion();

            try
            {
                resolucionFiltro = (Resolucion)Session["Filtro_Busqueda_Resoluciones"];
                pagina = resolucionFiltro.pagina;
            }
            catch { };

            if (resolucionFiltro == null)
            {
                resolucionFiltro = new Resolucion();
                Session["Filtro_Busqueda_Resoluciones"] = resolucionFiltro;
            }


            List<Resolucion> resolucionList = resolucionService.BusquedaResolucion(resolucionFiltro);

            GridResoluciones.DataSource = resolucionList;
            GridResoluciones.DataBind();


            if (resolucionList != null && resolucionList.Count > 0)
            {
                ExportarGrilla.Visible = true;
            }


        }


        //GRID RESOLUCIONES
        protected void GridResoluciones_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

            Resolucion resolucionFiltro = (Resolucion)Session["Filtro_Busqueda_Resoluciones"];
            if (resolucionFiltro == null)
            {
                resolucionFiltro = new Resolucion();
            }

            resolucionFiltro.pagina = e.NewPageIndex;
            Session["Filtro_Busqueda_Resoluciones"] = resolucionFiltro;

            GridResoluciones.PageIndex = e.NewPageIndex;
            GridResoluciones.DataBind();
            CargaGrilla();
        }


        //GRID RESOLUCIONES
        protected void GridResoluciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridView GridRelocalizacionTramite = (GridView)sender;



                //BOTON VER
                ImageButton boton_gVer = (ImageButton)e.Row.FindControl("gVer");
                if (boton_gVer != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.VER))
                    {
                        boton_gVer.Visible = true;
                    }
                };



            };
        }


        //GRID RESOLUCIONES
        protected void GridResoluciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int claveOrigen = Convert.ToInt32(arg[0]);
            int idClave = Convert.ToInt32(arg[1]);
            
            
            SolicitudConcesion solicitudVer = null;

            switch (e.CommandName)
            {

                case "VerResolucion":

                    //es una resolucion
                    if (claveOrigen == 0)
                    {
                        Response.Redirect("verResolucion.aspx?idResolucion=" + idClave);
                    }
                    else
                    {
                           
                        //unidad espacial o solicitud
                        if (claveOrigen == rbTipo.UNID_ESPACIAL_CONCESION)
                        {

                            solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            //solicitudVer.tieneAsignadaSolicitud = false;

                            Session["SolicitudConcesion"] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Unidades/Concesion/resolucionesConcesion.aspx");

                        }
                        else if (claveOrigen == rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA)
                        {

                            solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            //solicitudVer.tieneAsignadaSolicitud = false;

                            Session["SolicitudConcesion"] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Solicitudes/Registrar/ingresarDocumento.aspx");
                                    
                        }else if (claveOrigen == rbTipo.TIPO_TRAMITE_MODIFICACION)
                        {

                            solicitudVer = solicitudDA.ObtieneSolicitudConcesionMod(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            solicitudVer.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_MODIFICACION);
                            //solicitudVer.tieneAsignadaSolicitud = false;
                                   
                            Session["SolicitudModificacion"] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Solicitudes/Modificacion/ingresarDocumento.aspx");
                        }
                        else if (claveOrigen == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION)
                        {

                            solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            solicitudVer.sectorRelocalizacion = relocalizacionService.obtenerDetalleSector_Solicitud(solicitudVer.idSolConcesion);
                            //solicitudVer.tieneAsignadaSolicitud = false;

                            Session["SolicitudRelocalizacion"] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Solicitudes/Relocalizacion/ingresarDocumentoRelocalizacion.aspx");

                        }
                        else if (claveOrigen == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION_RESA)
                        {

                            solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            solicitudVer.sectorRelocalizacion = relocalizacionRESAService.obtenerDetalleSector_SolicitudRESA(solicitudVer.idSolConcesion);
                            //solicitudVer.tieneAsignadaSolicitud = false;

                            Session["SolicitudRelocalizacionRESA"] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Solicitudes/RelocalizacionRESA/ingresarDocumentoRelocalizacionRESA.aspx");

                        }
                        else if (claveOrigen == rbTipo.UNID_ESPACIAL_CENTRO_DE_FAENAMIENTO)
                        {


                            solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            //solicitudVer.tieneAsignadaSolicitud = false;

                            Session["SolicitudCentroFaenamiento"] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Unidades/Faenamiento/resolucionesFaenamiento.aspx");

                                    
                        }
                        else if (claveOrigen == rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO)
                        {

                            solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            //solicitudVer.tieneAsignadaSolicitud = false;

                            Session["SolicitudCentroFaenamiento"] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Solicitudes/Faenamiento/ingresarDocumentoFaenamiento.aspx");

                        }
                        else if (claveOrigen == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_FAENAMIENTO)
                        {

                            solicitudVer = solicitudDA.ObtieneSolicitudConcesionMod(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            solicitudVer.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_FAENAMIENTO);
                            //solicitudVer.tieneAsignadaSolicitud = false;

                            Session["SolicitudModificacionCentroFaenamiento"] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Solicitudes/ModificacionCentroFaenamiento/ingresarDocumentoModificacionCentroFaenamiento.aspx");

                        }
                        else if (claveOrigen == rbTipo.UNID_ESPACIAL_CENTRO_DE_ACOPIO)
                        {

                            solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            //solicitudVer.tieneAsignadaSolicitud = false;

                            Session["SolicitudCentroAcopio"] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Unidades/Acopio/resolucionesAcopio.aspx");


                        }
                        else if (claveOrigen == rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO)
                        {

                            solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            //solicitudVer.tieneAsignadaSolicitud = false;

                            Session["SolicitudCentroAcopio"] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Solicitudes/Acopio/ingresarDocumentoAcopio.aspx");
                        }
                        else if (claveOrigen == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_ACOPIO)
                        {

                            solicitudVer = solicitudDA.ObtieneSolicitudConcesionMod(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            solicitudVer.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_ACOPIO);
                            //solicitudVer.tieneAsignadaSolicitud = false;

                            Session["SolicitudModificacionCentroAcopio"] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Solicitudes/ModificacionCentroAcopio/ingresarDocumentoModificacionCentroAcopio.aspx");

                        }
                        else if (claveOrigen == rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB)
                        {

                            solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            //solicitudVer.tieneAsignadaSolicitud = false;

                            Session["SolicitudCentroAmerb"] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Unidades/Amerb/resolucionesAmerb.aspx");

                        }
                        else if (claveOrigen == rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB)
                        {
                            solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            //solicitudVer.tieneAsignadaSolicitud = false;

                            Session["SolicitudCentroAmerb"] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Solicitudes/Amerb/ingresarDocumentoAmerb.aspx");

                        }
                        else if (claveOrigen == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB)
                        {

                            solicitudVer = solicitudDA.ObtieneSolicitudConcesionMod(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            solicitudVer.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB);
                            //solicitudVer.tieneAsignadaSolicitud = false;

                            Session["SolicitudModificacionAmerb"] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Solicitudes/ModificacionAmerb/ingresarDocumentoModificacionAmerb.aspx");

                        }
                        else if (claveOrigen == rbTipo.UNID_ESPACIAL_COLECTORES_DE_SEMILLA)
                        {

                            solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            //solicitudVer.tieneAsignadaSolicitud = false;

                            Session["SolicitudCentroColector"] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Unidades/Colector/resolucionesColector.aspx");

                        }
                        else if (claveOrigen == rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA)
                        {
                            solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            //solicitudVer.tieneAsignadaSolicitud = false;

                            Session["SolicitudCentroColector"] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Solicitudes/Colector/ingresarDocumentoColector.aspx");

                        }
                        else if (claveOrigen == rbTipo.UNID_ESPACIAL_ECMPO)
                        {

                            solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            //solicitudVer.tieneAsignadaSolicitud = false;

                            Session["SolicitudCentroECMPO"] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Unidades/ECMPO/resolucionesECMPO.aspx");

                        }else if (claveOrigen == rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO)
                        {

                            solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            //solicitudVer.tieneAsignadaSolicitud = false;


                            Session["SolicitudCentroECMPO"] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Solicitudes/ECMPO/ingresarDocumentoECMPO.aspx");

                        }
                        else if (claveOrigen == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_ECMPO)
                        {

                            solicitudVer = solicitudDA.ObtieneSolicitudConcesionMod(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            solicitudVer.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_ECMPO);
                            //solicitudVer.tieneAsignadaSolicitud = false;

                            Session["SolicitudModificacionECMPO"] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Solicitudes/ModificacionECMPO/ingresarDocumentoModificacionECMPO.aspx");

                        }
                        else if (claveOrigen == rbTipo.UNID_ESPACIAL_EXPERIMENTALES_AMERB)
                        {


                            solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            //solicitudVer.tieneAsignadaSolicitud = false;

                            Session[paginas.solicitudExperimentalesAmerbSession] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Unidades/ExperimentalesAmerb/resolucionesExperimentalesAmerb.aspx");

                        }
                        else if (claveOrigen == rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB)
                        {

                            solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            //solicitudVer.tieneAsignadaSolicitud = false;

                            Session["SolicitudCentroExperimentalesAmerb"] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Solicitudes/ExperimentalesAmerb/ingresarDocumentoExperimentalesAmerb.aspx");
                            
                        }
                        else if (claveOrigen == rbTipo.UNID_ESPACIAL_EXPERIMENTALES_CONCESION)
                        {

                            solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            //solicitudVer.tieneAsignadaSolicitud = false;

                            Session[paginas.solicitudExperimentalesConcesionSession] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Unidades/ExperimentalesConcesion/resolucionesExperimentalesConcesion.aspx");

                        }
                        else if (claveOrigen == rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION)
                        {

                            solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idClave, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            //solicitudVer.tieneAsignadaSolicitud = false;


                            Session["SolicitudCentroExperimentalesConcesion"] = (SolicitudConcesion)solicitudVer;
                            Response.Redirect("~/Solicitudes/ExperimentalesConcesion/ingresarDocumentoExperimentalesConcesion.aspx");

                        }
                    }


                    break;

            };
        }


        protected void Buscar_Click(object sender, EventArgs e)
        {

            MensajeSuperior.Text = "";
            PanelMensajeSuperior.Visible = false;
            UpdatePanelMensajeSuperior.Update();


            ErroresGrilla.Text = "";
            PanelErroresGrilla.Visible = false;
            UpdatePanelErroresGrilla.Update();


            Resolucion resolucionFiltro = new Resolucion();
            
            if (Convert.ToInt32(Origen.SelectedValue) > 0)
            {
                resolucionFiltro.origen = new ParametroGenerico(Convert.ToInt32(Origen.SelectedValue), Origen.SelectedItem.Text);
            }

          
            if (Numero.Text != null && !Numero.Text.Trim().Equals(""))
            {
                resolucionFiltro.numero = Numero.Text.Trim();
            }

            if (!FechaDesde.Text.Equals(""))
            {
                resolucionFiltro.fechaDesde = Convert.ToDateTime(FechaDesde.Text);
            }
            if (!FechaHasta.Text.Equals(""))
            {
                resolucionFiltro.fechaHasta = Convert.ToDateTime(FechaHasta.Text);
            }
           

            Session["Filtro_Busqueda_Resoluciones"] = resolucionFiltro;
            CargaGrilla();



        }



        protected void Limpiar_Click(object sender, EventArgs e)
        {

            
            Origen.SelectedValue = "0";
            Numero.Text = "";
            FechaDesde.Text = "";
            FechaHasta.Text = "";
            
            UpdatePanelOrigen.Update();
            UpdatePanelNumero.Update();
            
            UpdatePanelFechaDesde.Update();
            UpdatePanelFechaHasta.Update();

            

        }

        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();

            CargaGrilla();

            int cantidad = GridResoluciones.Columns.Count;

            if (cantidad > 1)
            {
                cantidad = cantidad - 1;
            }

            GridResoluciones.Columns.RemoveAt(cantidad);
            grilla = GridResoluciones;

            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export("Resoluciones.xls", grilla);
        }

        protected void ImgAdd_PreRender(object sender, EventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            ScriptManager sc = ScriptManager.GetCurrent(this.Page);
            sc.RegisterPostBackControl(btn);
        }

    }
}