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
using SubPesca.Solicitudes.Amerb;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;

namespace SubPesca.Unidades.Amerb
{
    public partial class administrarCentroAmerb : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        RegionDA regionDA = new RegionDA();
        SolicitudDA solicitudDA = new SolicitudDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        PermisosService permisosService = new PermisosService();
        CommonService commonService = new CommonService();
        DataExternaDA dataExternaDA = new DataExternaDA();

        protected void setearModulo()
        {
            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_UNIDAD_ESPACIAL_AMERB };
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            // PAGE LOAD
            if (!Page.IsPostBack)
            {
                setearModulo();

                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (PreviousPage != null && PreviousPage is inicioSolicitudAmerb)
                {
                    MensajeSuperior.Text = ((inicioSolicitudAmerb)PreviousPage).MensajeRegistro;
                    PanelMensajeSuperior.Visible = true;
                    UpdatePanelMensajeSuperior.Update();

                }else if (PreviousPage != null && PreviousPage is unidadEspacialAmerb)
                {
                    MensajeSuperior.Text = ((unidadEspacialAmerb)PreviousPage).MensajeRegistro;
                    PanelMensajeSuperior.Visible = true;
                    UpdatePanelMensajeSuperior.Update();
                }
                
                if (usuario_logeado == null)
                {
                    Response.Redirect("~/ingreso.aspx");
                }

                Initialize_Form();

                // Cargamos la grilla
                Session["Filtro_Solicitud_Acuicultura_Amerb"] = null;
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
                    // Cargamos el combobox: Comuna
                    Estado.Items.Clear();
                    Estado.Items.Add(new ListItem("Vigente", rbEstadosGenerales.VIGENTE.ToString()));
                    Estado.Items.Add(new ListItem("No Vigente", rbEstadosGenerales.NO_VIGENTE.ToString()));
                    Estado.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
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

            CodigoSiep.Text = "";
            Pert.Text = ""; 
            FechaIngresoTramiteDesde.Text = "";
            FechaIngresoTramiteHasta.Text = "";

            TitularNombre.Text = "";

            Region.SelectedValue = "0";
            Provincia.SelectedValue = "0";
            Comuna.SelectedValue = "0";

            Amerb.Text = "";

            Estado.SelectedValue = "0";

            UpdatePanelCodigoCentro.Update();
            UpdatePanelFechaIngresoTramiteDesde.Update();
            UpdatePanelFechaIngresoTramiteHasta.Update();
            UpdatePanelTitular.Update();
            UpdatePanelRegion.Update();
            UpdatePanelComuna.Update();

        }


        protected void Buscar_Click(object sender, EventArgs e)
        {

            MensajeSuperior.Text = "";
            PanelMensajeSuperior.Visible = false;
            UpdatePanelMensajeSuperior.Update();

            SolicitudConcesion solicitudFiltro = new SolicitudConcesion();

            if (!CodigoSiep.Text.Trim().Equals(""))
            {
                solicitudFiltro.unidadEspacial = new UnidadEspacial();
                solicitudFiltro.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                solicitudFiltro.unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToString(CodigoSiep.Text);
            }

            if (!Pert.Text.Trim().Equals(""))
            {
                solicitudFiltro.numPert = Convert.ToString(Pert.Text);
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

            if (!Amerb.Text.Trim().Equals(""))
            {
                string[] split = Amerb.Text.Trim().Split(new Char[] { ':' });
                //uso el autocompletar
                if (split != null && split.Count() > 0)
                {
                    solicitudFiltro.amerbFiltro = new ParametroGenerico();

                    int i = 0;
                    bool result = int.TryParse(split[0], out i);

                    //Esta Buscando por el codigo
                    if (result)
                    {
                        solicitudFiltro.amerbFiltro.id = Convert.ToInt32(split[0]);
                    }
                    //esta buscando por un string
                    else
                    {
                        solicitudFiltro.amerbFiltro.id = 100000000; //no recuperara nada
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
                solicitudFiltro.estadoVigencia = new ParametroGenerico(Convert.ToInt32(Estado.SelectedItem.Value));
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

            if (Page.IsValid)
            {

                Session["Filtro_Solicitud_Acuicultura_Amerb"] = solicitudFiltro;

                CargaGrilla();

            }
            else
            {
                UpdatePanelMensajesValidaciones.Update();
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
                solicitudFiltro = (SolicitudConcesion)Session["Filtro_Solicitud_Acuicultura_Amerb"];
                pagina = solicitudFiltro.pagina;
            }
            catch { };

            if (solicitudFiltro == null)
            {
                solicitudFiltro = new SolicitudConcesion();
                Session["Filtro_Solicitud_Acuicultura_Amerb"] = solicitudFiltro;
            }

            //solicitudFiltro.estadoVigencia = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
            List<SolicitudConcesion> relocalizacionTramite = solicitudDA.ListarSolicitudAmerbTraspaso(solicitudFiltro);

            GridSolicitudesAdm.DataSource = relocalizacionTramite;
            GridSolicitudesAdm.DataBind();

            if (relocalizacionTramite != null && relocalizacionTramite.Count > 0)
            {
                ExportarGrilla.Visible = true;

                msgGrilla_Sol.Text = "";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                PanelSolicitudesMsg.Visible = false;
            }
            else {

                msgGrilla_Sol.Text = "No se han encontrado resultados asociado a su filtro de búsqueda.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                PanelSolicitudesMsg.Visible = true;
            
            }

        }


        protected void GridSolicitudesAdm_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

            SolicitudConcesion solicitudFiltro = new SolicitudConcesion();
            solicitudFiltro = (SolicitudConcesion)Session["Filtro_Solicitud_Acuicultura_Amerb"];
            if (solicitudFiltro == null)
            {
                solicitudFiltro = new SolicitudConcesion();
            }

            solicitudFiltro.pagina = e.NewPageIndex;
            Session["Filtro_Solicitud_Acuicultura_Amerb"] = solicitudFiltro;

            GridSolicitudesAdm.PageIndex = e.NewPageIndex;
            GridSolicitudesAdm.DataBind();
            CargaGrilla();
        }

        protected void GridSolicitudesAdm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SolicitudConcesion solicitudVer = null;
            int idSolConces = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "VerAmerb":
                    solicitudVer = new SolicitudConcesion();
                    solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idSolConces, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                    solicitudVer.tieneAsignadaSolicitud = false;

                    Session["SolicitudCentroAmerb"] = (SolicitudConcesion)solicitudVer;
                    Response.Redirect("~/Unidades/Amerb/resumenAmerb.aspx");

                    break;
                case "ModificarAmerb":
                    solicitudVer = new SolicitudConcesion();
                    solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idSolConces, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                    solicitudVer.tieneAsignadaSolicitud = true;

                    Session["SolicitudCentroAmerb"] = (SolicitudConcesion)solicitudVer;
                    Response.Redirect("~/Unidades/Amerb/resumenAmerb.aspx");

                    break;
                case "Desasociar":
                    Response.Redirect("~/Unidades/Amerb/cambiarVigenciaAmerb.aspx?idSolConcesion=" + e.CommandArgument);
                    break;

                case "Asociar":
                    Response.Redirect("~/Unidades/Amerb/cambiarVigenciaAmerb.aspx?idSolConcesion=" + e.CommandArgument);
                    break;

                case "EliminarUE":

                    if (!commonService.EliminarUnidadEspacial(idSolConces, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario))
                    {
                        msgGrilla_Sol.Text = "Ha ocurrido un error al intentar realizar la accion solicitada. (Obs: No es posible eliminar Unidades espaciales con tramites asociados)";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        PanelSolicitudesMsg.Visible = true;
                    }
                    else
                    {
                        CargaGrilla();
                        msgGrilla_Sol.Text = "Unidad Espacial Eliminada";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        PanelSolicitudesMsg.Visible = true;
                    }
                    break;

                case "TransformarUE":

                    ParametroGenerico resultado = commonService.TransformarUnidadEspacial(idSolConces, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                    if (resultado == null || resultado.id == 0)
                    {
                        msgGrilla_Sol.Text = "Ha ocurrido un error al intentar realizar la accion solicitada: " + resultado.descripcion;
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        PanelSolicitudesMsg.Visible = true;
                    }
                    else if (resultado != null && resultado.id == 1)
                    {
                        CargaGrilla();
                        msgGrilla_Sol.Text = "Unidad Espacial transformada a solicitud";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        PanelSolicitudesMsg.Visible = true;
                    }
                    else {
                        msgGrilla_Sol.Text = "Ha ocurrido un error al intentar realizar la accion solicitada";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        PanelSolicitudesMsg.Visible = true;
                    }

                    break;
            };
        }

        protected void GridSolicitudesAdm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                /*
                LinkButton boton_actaEntrega = (LinkButton)e.Row.FindControl("gActaEntrega");
                if (boton_actaEntrega != null)
                {
                    boton_actaEntrega.Visible = true;
                    boton_actaEntrega.Attributes.Add("onclick", "javascript:abre_dialogo('actaEntregaAmerb', '" + DataBinder.Eval(e.Row.DataItem, "idSolConcesion") + "')");
                }
                */

                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    boton_ver.Visible = true;
                }

                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_modificar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
                    {
                        boton_modificar.Visible = true;
                    }
                }

                String idEstadoVigencia = ((Label)e.Row.FindControl("hidden3")).Text;

                if (Convert.ToInt32(idEstadoVigencia) == rbEstadosGenerales.NO_VIGENTE)
                {
                    //Desasociar
                    ImageButton boton_desasociar = (ImageButton)e.Row.FindControl("gDesasociar");
                    if (boton_desasociar != null)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.DESASOCIAR))
                        {
                            boton_desasociar.Visible = true;
                        }

                    };

                }
                else if (Convert.ToInt32(idEstadoVigencia) == rbEstadosGenerales.VIGENTE)
                {
                    //Asociar
                    ImageButton boton_asociar = (ImageButton)e.Row.FindControl("gAsociar");
                    if (boton_asociar != null)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.ASOCIAR))
                        {
                            boton_asociar.Visible = true;
                        }

                    };
                }

                LinkButton boton_historial = (LinkButton)e.Row.FindControl("gHistorial");
                if (boton_historial != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.HISTORIAL))
                    {
                        boton_historial.Visible = true;
                        boton_historial.Attributes.Add("onclick", "javascript:abre_dialogo('historialCambioVigenciaAmerb', '" + DataBinder.Eval(e.Row.DataItem, "idSolConcesion") + "')");
                    }
                }

                LinkButton boton_ampliarVigencia = (LinkButton)e.Row.FindControl("gAmpliarVigencia");
                if (boton_ampliarVigencia != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.AMPLIAR_VIGENCIA))
                    {
                        boton_ampliarVigencia.Visible = true;
                        boton_ampliarVigencia.Attributes.Add("onclick", "javascript:abre_dialogo('extenderPlazoVigenciaAmerb', '" + DataBinder.Eval(e.Row.DataItem, "idSolConcesion") + "')");
                    }
                }

                LinkButton boton_eliminarUE = (LinkButton)e.Row.FindControl("gEliminarUE");
                if (boton_eliminarUE != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.ELIMINAR_UE))
                    {
                        boton_eliminarUE.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar esta Unidad Espacial?')");
                        boton_eliminarUE.Visible = true;

                    }
                }

                LinkButton boton_transformarUE = (LinkButton)e.Row.FindControl("gTransformarUE");
                if (boton_transformarUE != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.TRANSFORMAR_UE))
                    {
                        boton_transformarUE.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea transformar esta Unidad Espacial en una solicitud?')");
                        boton_transformarUE.Visible = true;
                    }
                }


            };
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
            SubPesca.Utilidades.GridViewExportUtil.Export("AdministrarAcuiculturaAmerb.xls", grilla);
        }



    }
}