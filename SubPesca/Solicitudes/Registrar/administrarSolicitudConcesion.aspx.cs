using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using LogicaNegocio.cl.subpesca.rb.reportes;



namespace SubPesca.Solicitudes.Registrar
{
    public partial class administrarSolicitudConcesion : System.Web.UI.Page
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
            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_CONCESION };
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

            Carga_Combobox("Estado");
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

                case "Estado":
                    //Cargamos el combobox: Estado
                    Estado.Items.Clear();
                    Estado.DataSource = reporteDA.ListarEstadosPorTipoTramite(88);
                    Estado.DataTextField = "descripcion";
                    Estado.DataValueField = "id";
                    Estado.DataBind();

                    Estado.Items.Insert(0, new ListItem("--Seleccione--", "0"));
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

 

        protected void Limpiar_Click(object sender, EventArgs e)
        {
            NPert.Text = "";
            NombreCentro.Text = "";
            TitularNombre.Text = "";
            FechaDesde.Text = "";
            FechaHasta.Text = "";
            Region.SelectedValue    = "0";
            Provincia.SelectedValue = "0";
            Comuna.SelectedValue    = "0";
            Estado.SelectedValue = "0";

            string script = @"<script type='text/javascript'>invoca_calendarios('administrarSolicitudConcesion');</script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "mensaje_cargado", script, false);

            msgGrilla.Text = "";
            Content_msgGrilla.Visible = false;
            UpdatePanelMensajeGrilla.Update();

         
        }

        protected void GridSolicitudesAdm_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

            SolicitudConcesion solicitudFiltro = new SolicitudConcesion();
            solicitudFiltro = (SolicitudConcesion)Session["Filtro_SolicConcesion"];
            if (solicitudFiltro==null)
            {
                solicitudFiltro = new SolicitudConcesion();
            }

            solicitudFiltro.pagina = e.NewPageIndex;
            Session["Filtro_SolicConcesion"] = solicitudFiltro;
            
            GridSolicitudesAdm.PageIndex = e.NewPageIndex;
            GridSolicitudesAdm.DataBind();
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
            string error = "";
            msgGrilla.Text = "";
            Content_msgGrilla.Visible = false;
            UpdatePanelMensajeGrilla.Update();

            

            SolicitudConcesion solicitudFiltro = new SolicitudConcesion();
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


            if (Convert.ToInt32(Region.SelectedValue) > 0)
            {
                solicitudFiltro.region = new ParametroGenerico(Convert.ToInt32(Region.SelectedItem.Value));
            }
            if (Convert.ToInt32(Comuna.SelectedValue) > 0)
            {
                solicitudFiltro.comunaFiltro = new ParametroGenerico(Convert.ToInt32(Comuna.SelectedItem.Value));
            }
            if (Convert.ToInt32(Provincia.SelectedValue) > 0)
            {
                solicitudFiltro.provincia = new ParametroGenerico(Convert.ToInt32(Provincia.SelectedItem.Value));
            }
            if (Convert.ToInt32(Estado.SelectedValue) > 0)
            {
                solicitudFiltro.estadoActual = new ParametroGenerico(Convert.ToInt32(Estado.SelectedItem.Value));
            }
            if (!FechaDesde.Text.Equals(""))
            {
               solicitudFiltro.fechaRangoFiltro1 = Convert.ToDateTime(FechaDesde.Text);
            }
            if (!FechaHasta.Text.Equals(""))
            {
               solicitudFiltro.fechaRangoFiltro2 = Convert.ToDateTime(FechaHasta.Text);
            }

            solicitudFiltro.pagina = new int();
            solicitudFiltro.pagina = 0;

            if (error.Length > 0)
            {
                PanelMensajeSuperior.Visible = true;
                MensajeSuperior.Text = error;
            }
            else
            {
                PanelMensajeSuperior.Visible = false;
                Session["Filtro_SolicConcesion"] = solicitudFiltro;
                CargaGrilla();
            }
        }


        protected void CargaGrilla()
        {
            int pagina = 0;
           
            SolicitudConcesion solicitudFiltro = (SolicitudConcesion)Session["Filtro_SolicConcesion"];
            if (solicitudFiltro != null) { pagina = solicitudFiltro.pagina; }
           

            if (solicitudFiltro==null)
            {
                solicitudFiltro = new SolicitudConcesion();
                Session["Filtro_SolicConcesion"] = solicitudFiltro;
            }

            List<SolicitudConcesion>  respSolicitudes = solicitudDA.ListarSolicitudConcesionAdmin(solicitudFiltro);
            
            if (respSolicitudes == null || respSolicitudes.Count <= 0)
            {

                msgGrilla.Text = "No se han encontrado resultados asociado a su filtro de búsqueda.";
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                Content_msgGrilla.Visible = true;
                UpdatePanelMensajeGrilla.Update();


                respSolicitudes = new List<SolicitudConcesion>();
                GridSolicitudesAdm.DataSource = respSolicitudes;
                GridSolicitudesAdm.DataBind();

                ExportarGrilla.Visible = false;
            }
            else
            {

                msgGrilla.Text = "";
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                Content_msgGrilla.Visible = false;
                UpdatePanelMensajeGrilla.Update();


                //PanelSolicitudesMsg.Visible = false;


                //SOLICITUD
                GridSolicitudesAdm.PageIndex = pagina;
                GridSolicitudesAdm.DataSource = respSolicitudes;
                GridSolicitudesAdm.DataBind();
                
                PanelSolicitudes.Visible = true;
                UpdatePanelSolicitudes.Update();

                ExportarGrilla.Visible = true;
            }
        }

       

        protected void GridSolicitudesAdm_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int idSolConces = Convert.ToInt32(e.CommandArgument);
            SolicitudConcesion solicitudVer = new SolicitudConcesion();

            switch (e.CommandName)
            {
                case "Modificar":

                    solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idSolConces, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                    //Session["solicitudConcesion"] = (SolicitudConcesion)solicitudVer;

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
                    
                    Session["SolicitudConcesion"] = (SolicitudConcesion)solicitudVer;
                    Response.Redirect("~/Solicitudes/Registrar/ingresarDocumento.aspx");

                    break;

                case "Ver":

                    solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idSolConces, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
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
                    
                    Session["SolicitudConcesion"] = (SolicitudConcesion)solicitudVer;
                    Response.Redirect("~/Solicitudes/Registrar/identificacionSolicitante.aspx");

                    break;

                case "Eliminar":

                    idSolConces = Convert.ToInt32(e.CommandArgument);
                    eliminarSolicitud(idSolConces);

                    break;
            };
        }

        private void eliminarSolicitud(int idSolConces)
        {
            CommonService commonService = new CommonService();
            bool resp = commonService.EliminarSolicitudRel(idSolConces, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

            if (resp)
            {
                msgGrilla.Text = "Se ha eliminado la solicitud de exitosamente.";
                Content_msgGrilla.Visible = true;


                this.CargaGrilla();
            }
            else
            {
                msgGrilla.Text = "No se ha eliminado la solicitud.";
                Content_msgGrilla.Visible = true;

                this.CargaGrilla();
            }
            Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            UpdatePanelMensajeGrilla.Update();
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

                        boton_modificar.Visible = true;
                    }
                };

                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    boton_ver.Visible = true;
                };

                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.ELIMINAR))
                    {
                        boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea borrar la Solicitud?')");
                        boton_eliminar.Visible = true;
                    }
                };

            };   
        }

        protected void GridConcesAdm_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void GridConcesAdm_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();
            string nom_grilla = "solicitudConcesion";
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "solicitudConcesion":
                    GridSolicitudesAdm.Columns.RemoveAt(7);
                    grilla = GridSolicitudesAdm;
                    ngrilla = "solicitudConcesion.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }
        



    }
}