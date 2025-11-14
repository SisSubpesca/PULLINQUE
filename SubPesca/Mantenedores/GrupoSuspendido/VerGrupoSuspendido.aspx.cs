using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using SubPesca.Utilidades;
using SubPesca.Mantenedores.Generales;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;

namespace SubPesca.Mantenedores.GrupoSuspendido
{
    public partial class VerGrupoSuspendido : System.Web.UI.Page
    {
        Datos.Entidades.GrupoSuspendidos grupoSuspendido = new Datos.Entidades.GrupoSuspendidos();
        MantenedorGeneralService mantenedorGeneralService = new MantenedorGeneralService();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        GrupoSuspendidoDA GrupoSuspendidoDa = new GrupoSuspendidoDA();
        SolicitudDA solicitudDA = new SolicitudDA();
        PermisosService permisosService = new PermisosService();

        protected void Page_Load(object sender, EventArgs e)
        {
            // PAGE LOAD
            if (!Page.IsPostBack)
            {
                Datos.Entidades.Usuario.Serializable usuario_logeado = (Datos.Entidades.Usuario.Serializable)HttpContext.Current.Session["Usuario"];

                if (!permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.GrupoSuspendido }, usuario_logeado, null, rbAccion.EDITAR))
                {
                    Response.Redirect("~/Administrador/principal.aspx");
                }

                inicializarFormulario();
                string id = Request.QueryString["id"];
                //revisamos si la cookie tiene valor.
                if (id != null)
                {
                    PanelAsociacionUE.Visible = true;
                    PanelArchivoAdjuntoOculto.Visible = true;
                   
                    grupoSuspendido.idGrupoSuspend = Convert.ToInt32(id);
                    Application["id"] = Convert.ToInt32(id);

                    List<GrupoSuspendidos> ListGrupoSuspendido = new List<GrupoSuspendidos>();
                    ListGrupoSuspendido = GrupoSuspendidoDa.ListarGrupoSuspendido(Convert.ToInt32(id), 0, 0, "");

                    foreach (GrupoSuspendidos _GrupoSuspendido in ListGrupoSuspendido)
                    {
                        TipoAgrupacion.Items.FindByValue(_GrupoSuspendido.tipoAgrupacion.id.ToString()).Selected = true;
                        CodigoCentro.Text = _GrupoSuspendido.solicitudConcesion.unidadEspacial.centrosDeCultivo.codigoCentro;
                        NombreGrupo.Text = _GrupoSuspendido.nombreGrupoSuspend;

                        GridArchivoAdjunto.DataSource = GrupoSuspendidoDa.ListarArchivoGrupoSusp(_GrupoSuspendido.idGrupoSuspend, 0);
                        GridArchivoAdjunto.DataBind();
                        PanelArchivoAdjuntoOculto.Visible = true;

                        GridViewAsociacionSolUE.DataSource = GrupoSuspendidoDa.ListarAsocGrupoSolicitud(_GrupoSuspendido.idGrupoSuspend, 0, 0);
                        GridViewAsociacionSolUE.DataBind();
                        PanelGridViewAsociacionSolUE.Visible = true;
                    }
                }
            }
        }

        private void inicializarFormulario()
        {
            cargarCombobox("TipoAgrupacion");
            cargarCombobox("EsDocumentoFinal");
            cargarCombobox("TipoSolicitud");
        }

        private void cargarCombobox(string combobox)
        {
            switch (combobox)
            {

                case "EsDocumentoFinal":
                    /*
                    EsDocumentoFinal.Items.Clear();
                    EsDocumentoFinal.DataBind();
                    EsDocumentoFinal.Items.Insert(0, new ListItem("Si", "1"));
                    EsDocumentoFinal.Items.Insert(0, new ListItem("No", "0"));
                    EsDocumentoFinal.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    */
                    break;

                case "TipoAgrupacion":

                    TipoAgrupacion.Items.Clear();
                    TipoAgrupacion.DataBind();

                    ParametroGenerico parametroGenericoFiltro = new ParametroGenerico();
                    parametroGenericoFiltro.clave = "TIPO_AGRUPACION_SUSPEND";

                    TipoAgrupacion.DataSource = mantenedorGeneralService.listarParametroGenerico(parametroGenericoFiltro);
                    TipoAgrupacion.DataTextField = "descripcion";
                    TipoAgrupacion.DataValueField = "id";
                    TipoAgrupacion.DataBind();
                    TipoAgrupacion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

                case "TipoSolicitud":
                    /*
                    TipoSolicitud.Items.Clear();
                    TipoSolicitud.DataSource = parametroGenericoDA.ListarTipoTramiteEstadoSolicitud(0);
                    TipoSolicitud.DataTextField = "nombreTipoInterfaz";
                    TipoSolicitud.DataValueField = "idTipoTramite";
                    TipoSolicitud.DataBind();
                    TipoSolicitud.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                     * */
                    break;
            }
        }

        protected void AgregarArchivoAdjunto_Click(object sender, ImageClickEventArgs e)
        {
            /*
            if (ArchivoAdjunto.HasFile)
            {
                List<ArchivoBinario> archivoBinarioList = (List<ArchivoBinario>)ViewState["ArchivoBinarioList"];
                if (archivoBinarioList == null)
                {
                    archivoBinarioList = new List<ArchivoBinario>();
                }

                ArchivoBinario archivoBinario = new ArchivoBinario();

                archivoBinario.nombreFisico = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
                archivoBinario.nombreArchivo = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
                archivoBinario.formato = ArchivoAdjunto.PostedFile.FileName.Substring(ArchivoAdjunto.PostedFile.FileName.LastIndexOf(".") + 1).ToLower();
                archivoBinario.tamano = ArchivoAdjunto.PostedFile.InputStream.Length;
                archivoBinario.bytes = ArchivoAdjunto.FileBytes;

                archivoBinario.numero = NumeroCI.Text;

                if (!FechaTextRecepcionArchAdj.Text.Equals(""))
                {
                    archivoBinario.fecha = Convert.ToDateTime(FechaTextRecepcionArchAdj.Text);
                }

                if (EsDocumentoFinal.SelectedValue.Equals("0"))
                {
                    archivoBinario.docFinal = false;
                }
                else if (EsDocumentoFinal.SelectedValue.Equals("1"))
                {
                    archivoBinario.docFinal = true;
                }

                List<String> listaErroresArchivoBinario = new List<string>(); //Validar los campos del ingreso de archivos.

                if (listaErroresArchivoBinario != null && listaErroresArchivoBinario.Count <= 0)
                {
                    archivoBinarioList.Add(archivoBinario);

                    GridArchivoAdjunto.DataSource = archivoBinarioList;
                    GridArchivoAdjunto.DataBind();

                    PanelArchivoAdjunto.Visible = true;
                    UpdatePanelGrillaArchivoAdjunto.Update();

                    ViewState["ArchivoBinarioList"] = (List<ArchivoBinario>)archivoBinarioList;
                }
                else
                {
                    foreach (String error in listaErroresArchivoBinario)
                    {
                        Page.Validators.Add(new ValidationError("grupo2", error));
                    }

                }

                NumeroCI.Text = "";
                FechaTextRecepcionArchAdj.Text = "";
                EsDocumentoFinal.SelectedValue = "-1";
            }
             * */
        }

        protected void ImageButtonAsociacionSolUE_Click(object sender, ImageClickEventArgs e)
        {
            /*List<AsocGrupoSolicitud> asocGrupoSolicitudList = (List<AsocGrupoSolicitud>)ViewState["AsocGrupoSolicitudList"];
            if (asocGrupoSolicitudList == null)
            {
                asocGrupoSolicitudList = new List<AsocGrupoSolicitud>();
            }

            AsocGrupoSolicitud asocGrupoSolicitud = new AsocGrupoSolicitud();
            asocGrupoSolicitud.solicitudConcesion = new SolicitudConcesion();
            asocGrupoSolicitud.solicitudConcesion.numPert = NumeroPert.Text;
            asocGrupoSolicitud.solicitudConcesion.tipoTramite = new ParametroGenerico();
            asocGrupoSolicitud.solicitudConcesion.tipoTramite.id = Convert.ToInt32(TipoSolicitud.SelectedValue);

            List<String> listaErroresAsociacionGrupoSolicitud = new List<string>(); //Validar los campos del ingreso de archivos.

            if (listaErroresAsociacionGrupoSolicitud != null && listaErroresAsociacionGrupoSolicitud.Count <= 0)
            {
                asocGrupoSolicitudList.Add(asocGrupoSolicitud);

                GridViewAsociacionSolUE.DataSource = asocGrupoSolicitudList;
                GridViewAsociacionSolUE.DataBind();

                PanelGridViewAsociacionSolUE.Visible = true;
                UpdatePanelAsociacionSolUE.Update();

                ViewState["AsocGrupoSolicitudList"] = (List<AsocGrupoSolicitud>)asocGrupoSolicitudList;
            }
            else
            {
                foreach (String error in listaErroresAsociacionGrupoSolicitud)
                {
                    Page.Validators.Add(new ValidationError("grupo3", error));
                }

            }
            TipoSolicitud.SelectedValue = "-1";
            NumeroPert.Text = "";
            PanelCentro.Visible = false;
             * */
        }

        protected void GuardarGrupoSuspendido_Click(object sender, EventArgs e)
        {
            Datos.Entidades.GrupoSuspendidos grupoSuspendido = new Datos.Entidades.GrupoSuspendidos();
            grupoSuspendido.tipoAgrupacion = new ParametroGenerico();
            grupoSuspendido.tipoAgrupacion.id = Convert.ToInt32(TipoAgrupacion.SelectedItem.Value);

            if (!CodigoCentro.Text.Equals(""))
            {
                grupoSuspendido.solicitudConcesion = new SolicitudConcesion();
                grupoSuspendido.solicitudConcesion.unidadEspacial = new UnidadEspacial();
                grupoSuspendido.solicitudConcesion.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                grupoSuspendido.solicitudConcesion.unidadEspacial.centrosDeCultivo.codigoCentro = CodigoCentro.Text;
            }

            if (!NombreGrupo.Text.Equals(""))
            {
                grupoSuspendido.nombreGrupoSuspend = NombreGrupo.Text;
            }

            /* Listado de Archivo Adjuntos */
            List<ArchivoBinario> archivoBinarioList = (List<ArchivoBinario>)ViewState["ArchivoBinarioList"];
            if (archivoBinarioList != null && archivoBinarioList.Count > 0)
            {
                grupoSuspendido.archivoGrupoSusp = archivoBinarioList;
            }


            /* Listado de Solicitudes de Unidades Espaciales */
            List<AsocGrupoSolicitud> asocGrupoSolicitudList = (List<AsocGrupoSolicitud>)ViewState["AsocGrupoSolicitudList"];
            if (asocGrupoSolicitudList != null && asocGrupoSolicitudList.Count > 0)
            {
                grupoSuspendido.asocGrupoSolicitud = asocGrupoSolicitudList;
            }


            List<String> listaErroresGrupoSuspendido = new List<string>(); //Validar grupo suspendido

            if (listaErroresGrupoSuspendido != null && listaErroresGrupoSuspendido.Count <= 0)
            {

                //crear metodo para guardar evaluación
                bool resp = mantenedorGeneralService.guardarGrupoSuspendido(grupoSuspendido);
                if (resp)
                {
                    msgGrilla.Text = "Se ha guardado el Grupo Suspendido exitosamente.";
                    Content_msgGrilla.Visible = true;

                }
                else
                {

                    msgGrilla.Text = "No se ha guardado el Grupo Suspendido.";
                    Content_msgGrilla.Visible = true;
                }

                UpdatePanelMensajeEvalUnidOrdenamTerr.Update();

            }
            else
            {
                foreach (String error in listaErroresGrupoSuspendido)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }

            }
        }

        protected void GridArchivoAdjunto_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridArchivosAdjuntosAntTerreno = (GridView)sender;


                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Archivos Adjuntos";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridArchivoAdjunto.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        
        }

        protected void GridViewGridArchivoAdjunto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Descargar
                ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                if (boton_descargar != null)
                {
                    boton_descargar.Visible = true;
                };
            }
        }

        protected void GridViewGridArchivoAdjunto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id;
            int idArchivo = 0;
            switch (e.CommandName)
            {
                case "Descargar":
                    idArchivo = Convert.ToInt32(e.CommandArgument);
                    List<ArchivoBinario> ListarchivoBinario = GrupoSuspendidoDa.ListarArchivoGrupoSusp(0, idArchivo); //Obtener el archivo binario que se desea.
                    ArchivoBinario archivoBinario = new ArchivoBinario();

                    foreach (ArchivoBinario Arch in ListarchivoBinario)
                    {
                        archivoBinario = Arch;
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = "application/" + archivoBinario.formato;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinario.nombreFisico + "." + archivoBinario.formato);
                        Response.BinaryWrite(archivoBinario.bytes);
                        Response.Flush();
                        Response.End();
                    }
                    break;
            }
        }


        protected void GridViewAsociacionSolUE_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridArchivosAdjuntosAntTerreno = (GridView)sender;


                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Asociación Solicitud Unidad Espacial";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridViewAsociacionSolUE.Controls[0].Controls.AddAt(0, HeaderRow);

            }

        }

        protected void ImgAdd_PreRender(object sender, EventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            ScriptManager sc = ScriptManager.GetCurrent(this.Page);
            sc.RegisterPostBackControl(btn);
        }
    }
}