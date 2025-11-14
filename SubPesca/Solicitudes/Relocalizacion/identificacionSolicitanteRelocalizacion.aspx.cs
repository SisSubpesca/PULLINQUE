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
using Datos.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.common;

namespace SubPesca.Solicitudes.Relocalizacion
{
    public partial class identificacionSolicitanteRelocalizacion : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        SolicitudDA solicitudDA = new SolicitudDA();
        SolicitanteDA solicitanteDA = new SolicitanteDA();
        Funciones funciones = new Funciones();
        PermisosService permisosService = new PermisosService();
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();

        protected void setearModulo()
        {

            ViewState["URL_VER"] = paginas.URL_VER_RELOCALIZACION;
            ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_RELOCALIZACION;
            ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_RELOCALIZACION;
            ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_RELOCALIZACION;
            ViewState["URL_ERROR"] = paginas.URL_ERROR_RELOCALIZACION;
            ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSession;

            SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

            if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA)
            {
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.IDENTIFICACION_SOLICITANTE_RELOCALIZACION_CREA };
            }
            if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA)
            {
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.IDENTIFICACION_SOLICITANTE_RELOCALIZACION_FUSIONA };
            }
            if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO)
            {
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.IDENTIFICACION_SOLICITANTE_RELOCALIZACION_SECTOR_CERO };
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
            SolicitudConcesion solicitudSectorRelocalizar = (SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

            if (solicitudSectorRelocalizar != null && solicitudSectorRelocalizar.idSolConcesion > 0)
            {
                IdSolicitud.Value = Convert.ToString(solicitudSectorRelocalizar.idSolConcesion);

                this.CargarListaSolicitudesPendientes(solicitudSectorRelocalizar.idSolConcesion);
                this.CargarListaSolicitantes(solicitudSectorRelocalizar.idSolConcesion);
            }
            else
            {
                Response.Redirect(ViewState["URL_ERROR"].ToString());

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

        protected void GridViewSolicitudesPendientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Ver":
                    int idRequerimiento = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/Solicitudes/Relocalizacion/verDocumentoRelocalizacion.aspx?idRequerimiento=" + idRequerimiento);

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

        private void CargarListaSolicitudesPendientes(int idSolConcesion)
        {

            GridViewSolicitudesPendientes.DataSource = solicitudDA.ListarPequerimientosPendPersona(idSolConcesion, 0);
            GridViewSolicitudesPendientes.DataBind();
        }



        private void CargarListaSolicitantes(int idSolConcesion)
        {

            string KeySort = "IdSolicitud ASC";
            GridSolicitante.DataSource = solicitanteDA.VerSolicitante(idSolConcesion, 0, KeySort, 0);
            GridSolicitante.DataBind();
            
        }

        

        protected void GridSolicitante_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "Ver":
                    Response.Redirect("~/Mantenedores/Titulares/detalleTitular.aspx?rutPersona=" + e.CommandArgument + "&bp=3");
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
            GridViewSolicitudesPendientes.PageIndex = e.NewPageIndex;
            GridViewSolicitudesPendientes.DataSource = solicitudDA.ListarPequerimientosPendPersona(Convert.ToInt32(IdSolicitud.Value), 0);
            GridViewSolicitudesPendientes.DataBind();

        }

    }
}