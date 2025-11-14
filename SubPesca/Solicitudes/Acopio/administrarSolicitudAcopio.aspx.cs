using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Entidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using SubPesca.Solicitudes.Registrar;
using LogicaNegocio.cl.subpesca.rb.reportes;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;

namespace SubPesca.Solicitudes.Acopio
{
    public partial class administrarSolicitudAcopio : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        RegionDA regionDA = new RegionDA();
        SolicitudDA solicitudDA = new SolicitudDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        PermisosService permisosService = new PermisosService();
        ReporteDA reporteDA = new ReporteDA();


        protected void setearModulo()
        {
            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_SOLICITUD_ACOPIO };
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            // PAGE LOAD
            if (!Page.IsPostBack)
            {
                setearModulo();

                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (PreviousPage != null && PreviousPage is inicioSolicitudAcopio)
                {
                    MensajeSuperior.Text = ((inicioSolicitudAcopio)PreviousPage).MensajeRegistro;
                    PanelMensajeSuperior.Visible = true;
                    UpdatePanelMensajeSuperior.Update();
                }


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
                    Region.Items.Clear();
                    Region.DataSource = regionDA.ListarRegion(0);
                    Region.DataTextField = "Region";
                    Region.DataValueField = "IdRegion";
                    Region.DataBind();
                    Region.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    break;

                case "Provincia":
                    Provincia.Items.Clear();

                    if (Convert.ToInt32(Region.SelectedValue) > 0)
                    {
                        Provincia.DataSource = parametroGenericoDA.ListarProvinciaReg(0, Convert.ToInt32(Region.SelectedValue));
                        Provincia.DataTextField = "descripcion";
                        Provincia.DataValueField = "id";
                        Provincia.DataBind();
                    }

                    Provincia.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    UpdatePanelProvincia.Update();
                    break;

                case "Comuna":
                    Comuna.Items.Clear();

                    if (Convert.ToInt32(Provincia.SelectedValue) > 0)
                    {
                        Comuna.DataSource = parametroGenericoDA.ListarComuna(0, Convert.ToInt32(Provincia.SelectedValue));
                        Comuna.DataTextField = "descripcion";
                        Comuna.DataValueField = "id";
                        Comuna.DataBind();
                    }

                    Comuna.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    UpdatePanelComuna.Update();
                    break;

                case "Estado":
                    Estado.Items.Clear();
                    //falta el numero correcto del tipo de tramite
                    Estado.DataSource = reporteDA.ListarEstadosPorTipoTramite(118);
                    Estado.DataTextField = "descripcion";
                    Estado.DataValueField = "id";
                    Estado.DataBind();

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


        protected void Provincia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("Comuna");
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

            Region.SelectedValue = "0";
            Provincia.SelectedValue = "0";
            Comuna.SelectedValue = "0";
            Estado.SelectedValue = "0";
            
            UpdatePanelNPert.Update();
            UpdatePanelFechaIngresoTramiteDesde.Update();
            UpdatePanelFechaIngresoTramiteHasta.Update();
            UpdatePanelTitular.Update();
            UpdatePanelRegion.Update();
            UpdatePanelComuna.Update();
            UpdatePanelEstado.Update();
        }


        protected void Buscar_Click(object sender, EventArgs e)
        {

            MensajeSuperior.Text = "";
            PanelMensajeSuperior.Visible = false;
            UpdatePanelMensajeSuperior.Update();
            string error = "";

            SolicitudConcesion solicitudFiltro = new SolicitudConcesion();

            if (!NPert.Text.Trim().Equals(""))
            {
                solicitudFiltro.numPert = Convert.ToString(NPert.Text);
                //metodo para revisar si existe o no el pert como solicitud
                //verificamos que si es unidad espacial o aun sigue como solicitud. de ser en caso que sea unidad Espacial
                error = solicitudDA.AdminSolicitudConcesionInfo(NPert.Text);
            }
            if (!FechaIngresoTramiteDesde.Text.Trim().Equals(""))
            {
                solicitudFiltro.fechaRangoFiltro1 = Convert.ToDateTime(FechaIngresoTramiteDesde.Text);
            }
            if (!FechaIngresoTramiteHasta.Text.Trim().Equals(""))
            {
                solicitudFiltro.fechaRangoFiltro2 = Convert.ToDateTime(FechaIngresoTramiteHasta.Text);
            }


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
            if (Convert.ToInt32(Provincia.SelectedValue) > 0)
            {
                solicitudFiltro.provincia = new ParametroGenerico(Convert.ToInt32(Provincia.SelectedItem.Value));
            }
            if (Convert.ToInt32(Comuna.SelectedValue) > 0)
            {
                solicitudFiltro.comunaFiltro = new ParametroGenerico(Convert.ToInt32(Comuna.SelectedItem.Value));
            }
            if (Convert.ToInt32(Estado.SelectedValue) > 0)
            { 
                solicitudFiltro.estadoActual = new ParametroGenerico(Convert.ToInt32(Estado.SelectedItem.Value));
            }


            if (solicitudFiltro.fechaRangoFiltro1 != null && solicitudFiltro.fechaRangoFiltro2 != null)
            {

                if (solicitudFiltro.fechaRangoFiltro1 > solicitudFiltro.fechaRangoFiltro2)
                {
                    Page.Validators.Add(new ValidationError("FormBusquedaRelocalizacion", "\"Fecha Ingreso Trámite Desde\" debe ser posterior a \"Fecha Ingreso Trámite Hasta\""));

                }
                else
                {
                    DateTime fechaMax = solicitudFiltro.fechaRangoFiltro1.AddMonths(3);

                    if (solicitudFiltro.fechaRangoFiltro2 > fechaMax)
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
                if (Page.IsValid)
                {

                    PanelMensajeSuperior.Visible = false;

                    Session["Filtro_Solicitud_Centro_Acopio"] = solicitudFiltro;

                    CargaGrilla();

                }
                else
                {
                    UpdatePanelMensajesValidaciones.Update();
                }
            }
        }



        //GRID RELOCALIZACION TRAMITE
        protected void CargaGrilla()
        {
            int pagina = 0;
            ExportarGrilla.Visible = false;

            SolicitudConcesion solicitudFiltro = new SolicitudConcesion();

            try
            {
                solicitudFiltro = (SolicitudConcesion)Session["Filtro_Solicitud_Centro_Acopio"];
                pagina = solicitudFiltro.pagina;
            }
            catch { };

            if (solicitudFiltro == null)
            {
                solicitudFiltro = new SolicitudConcesion();
                Session["Filtro_Solicitud_Centro_Acopio"] = solicitudFiltro;
            }

            
            List<SolicitudConcesion> relocalizacionTramite = solicitudDA.ListarSolicitudCentroAcopioAdmin(solicitudFiltro);

            GridSolicitudesAdm.DataSource = relocalizacionTramite;
            GridSolicitudesAdm.DataBind();

            if (relocalizacionTramite != null && relocalizacionTramite.Count > 0)
            {
                ExportarGrilla.Visible = true;

                msgGrilla.Text = "";
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                Content_msgGrilla.Visible = false;
                UpdatePanelMensajeGrilla.Update();
            }
            else
            {
                msgGrilla.Text = "No se han encontrado resultados asociado a su filtro de búsqueda.";
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                Content_msgGrilla.Visible = true;
                UpdatePanelMensajeGrilla.Update();
            }
            
        }


        protected void GridSolicitudesAdm_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

            SolicitudConcesion solicitudFiltro = new SolicitudConcesion();
            solicitudFiltro = (SolicitudConcesion)Session["Filtro_Solicitud_Centro_Acopio"];
            if (solicitudFiltro == null)
            {
                solicitudFiltro = new SolicitudConcesion();
            }

            solicitudFiltro.pagina = e.NewPageIndex;
            Session["Filtro_Solicitud_Centro_Acopio"] = solicitudFiltro;

            GridSolicitudesAdm.PageIndex = e.NewPageIndex;
            GridSolicitudesAdm.DataBind();
            CargaGrilla();
        }



        protected void GridSolicitudesAdm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.ELIMINAR))
                    {
                        boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar?')");
                        boton_eliminar.Visible = true;
                    }
                };

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

            };
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


                    Session["SolicitudCentroAcopio"] = (SolicitudConcesion)solicitudVer;
                    Response.Redirect("~/Solicitudes/Acopio/ingresarDocumentoAcopio.aspx");

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

                    Session["SolicitudCentroAcopio"] = (SolicitudConcesion)solicitudVer;
                    Response.Redirect("~/Solicitudes/Acopio/identificacionSolicitanteAcopio.aspx");

                    break;

                case "Eliminar":

                    GridSolicitudesAdm.EditIndex = -1;
                    eliminarSolicitud(idSolConces);

                    break;
            };
        }

        protected void eliminarSolicitud(int idSolConcesion)
        {
            CommonService commonService = new CommonService();
            bool resp = commonService.EliminarSolicitudRel(idSolConcesion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

            if (resp)
            {
                msgGrilla.Text = "Se ha eliminado la solicitud de exitosamente.";
                GridSolicitudesAdm.Visible = true;

                this.CargaGrilla();
            }
            else
            {
                msgGrilla.Text = "No se ha eliminado la solicitud.";
                GridSolicitudesAdm.Visible = true;

                this.CargaGrilla();
            }
            Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            UpdatePanelMensajeGrilla.Update();
        }

        protected void ExportarGrilla_Click(object sender, EventArgs e)
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
            SubPesca.Utilidades.GridViewExportUtil.Export("AdministradorSolicitudes.xls", grilla);
        }



    }
}