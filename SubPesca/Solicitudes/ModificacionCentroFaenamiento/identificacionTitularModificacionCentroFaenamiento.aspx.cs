using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using System.Collections;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using LogicaNegocio.cl.subpesca.rb.servicios.concesiones;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.modificacion;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;

namespace SubPesca.Solicitudes.ModificacionCentroFaenamiento
{
    public partial class identificacionTitularModificacionCentroFaenamiento : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema

        SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();
        SolicitanteDA solicitanteDA = new SolicitanteDA();
        SolicitudDA solicitudDA = new SolicitudDA();
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();
        PermisosService permisosService = new PermisosService();


        protected void setearModulo()
        {

            ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
            ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
            ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
            ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
            ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
            ViewState["solicitudSession"] = paginas.solicitudModificacionCentroFaenamientoSession;


            SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

            if (solicitudConcesion != null)
            {

                int[] tiposModificacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

                int contador = 0;
                foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                {
                    if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE)
                    {
                        tiposModificacion[contador] = rbSeccionUnidadEspacial.IDENTIFICACION_SOLICITANTE_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                        contador++;
                    }
                    if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_RENOVACION)
                    {
                        tiposModificacion[contador] = rbSeccionUnidadEspacial.IDENTIFICACION_SOLICITANTE_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                        contador++;
                    }
                    if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_PT_ESPECIE)
                    {
                        tiposModificacion[contador] = rbSeccionUnidadEspacial.IDENTIFICACION_SOLICITANTE_MOD_CENTRO_FAENAMIENTO_PT;
                        contador++;
                    }
                    if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE)
                    {
                        tiposModificacion[contador] = rbSeccionUnidadEspacial.IDENTIFICACION_SOLICITANTE_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                        contador++;
                    }
                    if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REGULARIZACION)
                    {
                        tiposModificacion[contador] = rbSeccionUnidadEspacial.IDENTIFICACION_SOLICITANTE_MOD_CENTRO_FAENAMIENTO_REGULARIZACION;
                        contador++;
                    }
                }

                ViewState["SECCION_ESPECIFICA"] = tiposModificacion;

            }

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
                    //FormularioIngreso.Visible = true;
                }
                else
                {
                    //FormularioIngreso.Visible = false;
                }


                // Inicializamos el formulario
                Initialize_Form();

            }
        }

        private void Initialize_Form()
        {
            SolicitudConcesion solicitudModificacion = (SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

            if (solicitudModificacion != null && solicitudModificacion.idSolConcesion > 0)
            {
                IdSolicitud.Value = Convert.ToString(solicitudModificacion.idSolConcesion);

                DespliegueMenuSeccionDA despliegueMenuSeccionDA = new DespliegueMenuSeccionDA();
                List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.IDENTIFICACION_TITULAR_CONCESION_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_MOD_CENTRO_FAENAMIENTO);

                if (solicitudModificacionService.despligueSeccionesMenu(solicitudModificacion, despliegueMenuSeccionList, rbSeccion.DATOS_DEL_SOLICITANTE))
                {
                    // Inicializamos el Hashtable con solicitantes de la concesion de acuicultura original
                    Initialize_HT_SolicitantesAcuicultura();
                }

                if (solicitudModificacionService.despligueSeccionesMenu(solicitudModificacion, despliegueMenuSeccionList, rbSeccion.REQUERIMIENTOS_PENDIENTES_TITULAR))
                {
                    //Inicializamos el Hashtable con las Solicitudes Pendientes de los Titulares de la Solicitud de Modificacion.
                    Initialize_HT_RequerimientosPendientesTitular();
                }
            }
            else
            {
                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");

            }
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

            SolicitudConcesion solicitudAux = (SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
            if (solicitudAux != null && solicitudAux.idSolConcesion > 0)
            {
                CargarListaSolicitudesPendientes(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));

            }
        }

        private void CargarListaSolicitudesPendientes(Usuario.Serializable usuario_logeado, int idSolConcesion)
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

            List<Requerimiento> requerimientoList = solicitudDA.ListarPequerimientosPendPersona(idSolConcesion, 0);

            if (requerimientoList != null && requerimientoList.Count > 0)
            {
                GridViewSolicitudesPendientes.PageIndex = pagina;
                GridViewSolicitudesPendientes.DataSource = requerimientoList;
                GridViewSolicitudesPendientes.DataBind();

                PanelSolicitudesPendientes.Visible = true;
                UpdatePanelSolicitudesPendientes.Update();
            }
        }

        private void Initialize_HT_SolicitantesAcuicultura()
        {
            ConcesionService concesionService = new ConcesionService();
            SolicitudConcesion solicitudModificacion = (SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

            if (solicitudModificacion != null && solicitudModificacion.idSolConcesion > 0)
            {
                SolicitudConcesion solicitudConcesionOriginal = concesionService.ObtieneUnidadEspacialExistente(Convert.ToString(solicitudModificacion.tramiteModConcesion.centro.id), rbTipo.UNID_ESPACIAL_CENTRO_DE_FAENAMIENTO);

                if (solicitudConcesionOriginal != null)
                {
                    CargarListaSolicitantes(usuario_logeado, Convert.ToInt32(solicitudConcesionOriginal.idSolConcesion));

                    PanelDatosSolicitante.Visible = true;
                    UpdatePanelDatosSolicitante.Update();
                }
            }
        }

        private void CargarListaSolicitantes(Usuario.Serializable usuario_logeado, int idSolConcesion)
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
            GridSolicitante.DataSource = solicitanteDA.VerSolicitante(idSolConcesion, 0, KeySort, 0);
            GridSolicitante.DataBind();
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

        protected void GridSolicitante_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "Ver":
                    Response.Redirect("~/Mantenedores/Titulares/detalleTitular.aspx?rutPersona=" + e.CommandArgument + "&bp=52");
                    break;
            };
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

        protected void GridViewSolicitudesPendientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "Ver":
                    int idRequerimiento = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/Solicitudes/Modificacion/verDocumento.aspx?idRequerimiento=" + idRequerimiento);

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
    }
}