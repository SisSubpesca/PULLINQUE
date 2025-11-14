using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Entidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using SubPesca.Solicitudes.Registrar;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;

namespace SubPesca.Unidades.Concesion
{
    public partial class administrarConcesiones : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        public static administrarConcesiones Instance { get; set; }
        
        RegionDA regionDA = new RegionDA();
        SolicitudDA solicitudDA = new SolicitudDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        PermisosService permisosService = new PermisosService();
        CommonService commonService = new CommonService();




        protected void setearModulo()
        {
            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_UNIDAD_ESPACIAL_CONCESIONES };
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
                else if (PreviousPage != null && PreviousPage is unidadEspacial)
                {
                    MensajeSuperior.Text = ((unidadEspacial)PreviousPage).MensajeRegistro;
                    PanelMensajeSuperior.Visible = true;
                    UpdatePanelMensajeSuperior.Update();
                }

                Initialize_Form();

                // Cargamos la grilla
                Session["Filtro_SolicConcesion"] = null;
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

        protected void Provincias_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("Comuna");
        }

        /*
        protected void cambiaPestania_Click(object sender, EventArgs e)
        {
            LinkButton boton = (LinkButton)sender;

            switch (boton.ID)
            {

                case "lnk_Concesiones":

                    lnk_Concesiones.CssClass = "tab1_selected";
                    UpdatePanelPestanaConcesiones.Update();

                    lnk_Solicitudes.CssClass = "tab2";
                    UpdatePanelPestanaSolicitudes.Update();



                    PanelConcesiones.Visible = true;
                    UpdatePanelConcesiones.Update();

                    PanelSolicitudes.Visible = false;
                    UpdatePanelSolicitudes.Update();

                    break;


                case "lnk_Solicitudes":

                    lnk_Concesiones.CssClass = "tab1";
                    UpdatePanelPestanaConcesiones.Update();

                    lnk_Solicitudes.CssClass = "tab2_selected";
                    UpdatePanelPestanaSolicitudes.Update();



                    PanelConcesiones.Visible = false;
                    UpdatePanelConcesiones.Update();

                    PanelSolicitudes.Visible = true;
                    UpdatePanelSolicitudes.Update();


                    break;
            };

        }
        */

        protected void Limpiar_Click(object sender, EventArgs e)
        {
            CodigoSiep.Text = "";
            Pert.Text = ""; 
            TitularNombre.Text = "";
            FechaDesde.Text = "";
            FechaHasta.Text = "";
            Region.SelectedValue = "0";
            Provincia.SelectedValue = "0";
            Comuna.SelectedValue = "0";
            Estado.SelectedValue = "0";

            string script = @"<script type='text/javascript'>invoca_calendarios('administrarSolicitudConcesion');</script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "mensaje_cargado", script, false);


        }

        protected void GridSolicitudesAdm_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

            SolicitudConcesion solicitudFiltro = new SolicitudConcesion();
            solicitudFiltro = (SolicitudConcesion)Session["Filtro_SolicConcesion"];
            if (solicitudFiltro == null)
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
                solicitudFiltro.estadoVigencia = new ParametroGenerico(Convert.ToInt32(Estado.SelectedItem.Value));
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

            Session["Filtro_SolicConcesion"] = solicitudFiltro;
            CargaGrilla();

        }


        protected void CargaGrilla()
        {
            int pagina = 0;
            ExportarGrilla.Visible = false;


            SolicitudConcesion solicitudFiltro = (SolicitudConcesion)Session["Filtro_SolicConcesion"];
            if (solicitudFiltro != null) { pagina = solicitudFiltro.pagina; }
            

            if (solicitudFiltro == null)
            {
                solicitudFiltro = new SolicitudConcesion();
                Session["Filtro_SolicConcesion"] = solicitudFiltro;
            }

            /*
            //Aquí el PL que obtiene las concesiones vigentes
            solicitudFiltro.estadoVigencia = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
            respVigentes = solicitudDA.ListarSolicitudConcesionTraspaso(solicitudFiltro);

            //CONCESION
            //Aquí el PL con las concesiones No Vigentes
            solicitudFiltro.estadoVigencia = new ParametroGenerico(rbEstadosGenerales.NO_VIGENTE);
            List<SolicitudConcesion> respNoVigentes = solicitudDA.ListarSolicitudConcesionTraspaso(solicitudFiltro);

            if ((respVigentes == null || respVigentes.Count <= 0) && (respNoVigentes == null && respNoVigentes.Count <= 0))
            {
                msgGrilla_Sol.Text = "No se han registrado estados.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                PanelSolicitudesMsg.Visible = true;
                
                respVigentes = new List<SolicitudConcesion>();
                GridSolicitudesAdm.DataSource = respVigentes;
                GridSolicitudesAdm.DataBind();

                respNoVigentes = new List<SolicitudConcesion>();
                GridConcesAdm.DataSource = respVigentes;
                GridConcesAdm.DataBind();
            }
            else
            {
                
                PanelSolicitudesMsg.Visible = false;

                lnk_Concesiones.CssClass = "tab1";
                UpdatePanelPestanaConcesiones.Update();

                lnk_Solicitudes.CssClass = "tab2_selected";
                UpdatePanelPestanaSolicitudes.Update();

                //SOLICITUD
                GridSolicitudesAdm.PageIndex = pagina;
                GridSolicitudesAdm.DataSource = respVigentes;
                GridSolicitudesAdm.DataBind();

                //CONCESION
                GridConcesAdm.PageIndex = pagina;
                GridConcesAdm.DataSource = respNoVigentes;
                GridConcesAdm.DataBind();

                PanelConcesiones.Visible = false;
                UpdatePanelConcesiones.Update();
                
                PanelSolicitudes.Visible = true;
                UpdatePanelSolicitudes.Update();
            }
            */
            List<SolicitudConcesion> unidadesEspacialesList = solicitudDA.ListarSolicitudConcesionTraspaso(solicitudFiltro);

            if (unidadesEspacialesList == null || unidadesEspacialesList.Count <= 0)
            {
                msgGrilla_Sol.Text = "No se han encontrado resultados asociado a su filtro de búsqueda.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                PanelSolicitudesMsg.Visible = true;

                unidadesEspacialesList = new List<SolicitudConcesion>();
                GridSolicitudesAdm.DataSource = unidadesEspacialesList;
                GridSolicitudesAdm.DataBind();

                unidadesEspacialesList = new List<SolicitudConcesion>();
                GridSolicitudesAdm.DataSource = unidadesEspacialesList;
                GridSolicitudesAdm.DataBind();
            }
            else {

                PanelSolicitudesMsg.Visible = false;
                
                GridSolicitudesAdm.PageIndex = pagina;
                GridSolicitudesAdm.DataSource = unidadesEspacialesList;
                GridSolicitudesAdm.DataBind();

                PanelSolicitudes.Visible = true;
                UpdatePanelSolicitudes.Update();

                if (unidadesEspacialesList != null && unidadesEspacialesList.Count > 0)
                {
                    ExportarGrilla.Visible = true;
                }
            }

        }



        protected void GridSolicitudesAdm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SolicitudConcesion solicitudVer = null;
            
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int idSolConces = Convert.ToInt32(arg[0]);
            //int codigoCentro = Convert.ToInt32(arg[1]);

            switch (e.CommandName)
            {
                case "VerConcesion":

                    solicitudVer = new SolicitudConcesion();
                    solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idSolConces, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                    solicitudVer.tieneAsignadaSolicitud = false;
                    /*
                    solicitudVer.unidadEspacial = new UnidadEspacial();
                    solicitudVer.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                    solicitudVer.unidadEspacial.centrosDeCultivo.codigoCentro = codigoCentro;
                    */

                    Session["SolicitudConcesion"] = (SolicitudConcesion)solicitudVer;
                    
                    //Response.Redirect("~/Solicitudes/Registrar/ingresarDocumento.aspx");
                    Response.Redirect("~/Unidades/Concesion/resumenConcesion.aspx");

                    break;
                case "ModificarConcesion":

                    solicitudVer = new SolicitudConcesion();
                    solicitudVer = solicitudDA.ObtieneSolicitudConcesion(idSolConces, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                    solicitudVer.tieneAsignadaSolicitud = true;

                    /*
                    solicitudVer.unidadEspacial = new UnidadEspacial();
                    solicitudVer.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                    solicitudVer.unidadEspacial.centrosDeCultivo.codigoCentro = codigoCentro;
                    */

                    Session["SolicitudConcesion"] = (SolicitudConcesion)solicitudVer;

                    Response.Redirect("~/Unidades/Concesion/resumenConcesion.aspx");

                    break;

                case "Desasociar":
                    Response.Redirect("~/Unidades/Concesion/cambiarVigenciaConcesion.aspx?idSolConcesion=" + e.CommandArgument);
                    break;

                case "Asociar":
                    Response.Redirect("~/Unidades/Concesion/cambiarVigenciaConcesion.aspx?idSolConcesion=" + e.CommandArgument);
                    break;

                case "EliminarUE":

                    if (!commonService.EliminarUnidadEspacial(idSolConces, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario))
                    {
                        msgGrilla_Sol.Text = "Ha ocurrido un error al intentar realizar la accion solicitada. (Obs: No es posible eliminar Unidades espaciales con tramites asociados)";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        PanelSolicitudesMsg.Visible = true;
                    }
                    else {
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
                    boton_actaEntrega.Attributes.Add("onclick", "javascript:abre_dialogo('actaEntregaConcesion', '" + DataBinder.Eval(e.Row.DataItem, "idSolConcesion") + "')");
                }

                LinkButton boton_historial = (LinkButton)e.Row.FindControl("gHistorial");
                if (boton_historial != null)
                {
                    boton_historial.Visible = true;
                    boton_historial.Attributes.Add("onclick", "javascript:abre_dialogo('historial', '" + DataBinder.Eval(e.Row.DataItem, "idSolConcesion") + "')");
                }
                */
                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    boton_ver.Visible = true;
                };

                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_modificar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
                    {
                        boton_modificar.Visible = true;
                    }
                };

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
                        boton_historial.Attributes.Add("onclick", "javascript:abre_dialogo('historialCambioVigenciaConcecion', '" + DataBinder.Eval(e.Row.DataItem, "idSolConcesion") + "')");
                    }
                }

                LinkButton boton_ampliarVigencia = (LinkButton)e.Row.FindControl("gAmpliarVigencia");
                if (boton_ampliarVigencia != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.AMPLIAR_VIGENCIA))
                    {
                        boton_ampliarVigencia.Visible = true;
                        boton_ampliarVigencia.Attributes.Add("onclick", "javascript:abre_dialogo('extenderPlazoVigenciaConcesion', '" + DataBinder.Eval(e.Row.DataItem, "idSolConcesion") + "')");
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
            SubPesca.Utilidades.GridViewExportUtil.Export("AdministrarConcesiones.xls", grilla);
        }

    }
}