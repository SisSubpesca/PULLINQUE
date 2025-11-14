using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubPesca.Mantenedores.Generales;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Entidades;
using SubPesca.Solicitudes.Registrar;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;
using Validaciones.cl.subpesca.rb.modificacion;
using LogicaNegocio.cl.subpesca.rb.visacion;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.servicios.resoluciones;

namespace SubPesca.Mantenedores.GrupoSuspendido
{
    public partial class CrearGrupoSuspendido : System.Web.UI.Page
    {
        IngresarSolicitudModificacionValidacion ingresarSolicitudModificacionValidacion = new IngresarSolicitudModificacionValidacion();
        Datos.Entidades.GrupoSuspendidos grupoSuspendido = new Datos.Entidades.GrupoSuspendidos();
        MantenedorGeneralService mantenedorGeneralService = new MantenedorGeneralService();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        GrupoSuspendidoDA GrupoSuspendidoDa = new GrupoSuspendidoDA();
        SolicitudDA solicitudDA = new SolicitudDA();
        EnviarCorreo enviarCorreo = new EnviarCorreo();
        VisacionDA _VisacionDA = new VisacionDA();
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
                        //dado que es modificar se bloquea boton guardar Grupo suspendido y se pueden agragar o sacar Solicitudes o Archivos
                        PanelAsociacionUE.Visible = true;
                        PanelArchivoAdjuntoOculto.Visible = true;
                        GuardarGrupoSuspendido.Visible = false;
                        grupoSuspendido.idGrupoSuspend = Convert.ToInt32(id);
                        Application["id"] = Convert.ToInt32(id);

                        List<GrupoSuspendidos> ListGrupoSuspendido = new List<GrupoSuspendidos>();
                        ListGrupoSuspendido = GrupoSuspendidoDa.ListarGrupoSuspendido(Convert.ToInt32(id), 0, 0, "");
                        //ViewState["AsocGrupoSolicitudList"] = ListGrupoSuspendido;
                        foreach (GrupoSuspendidos _GrupoSuspendido in ListGrupoSuspendido)
                        {
                            TipoAgrupacion.Items.FindByValue(_GrupoSuspendido.tipoAgrupacion.id.ToString()).Selected = true;
                            CodigoCentro.Text = _GrupoSuspendido.solicitudConcesion.unidadEspacial.centrosDeCultivo.codigoCentro;
                            NombreGrupo.Text = _GrupoSuspendido.nombreGrupoSuspend;
                           
                            GridArchivoAdjunto.DataSource = GrupoSuspendidoDa.ListarArchivoGrupoSusp(_GrupoSuspendido.idGrupoSuspend,0);
                            GridArchivoAdjunto.DataBind();
                            PanelArchivoAdjuntoOculto.Visible = true;

                            ListarGrillaSolicitudGrupoSusp(_GrupoSuspendido.idGrupoSuspend);
                            ViewState["AsocGrupoSolicitudList"] = GrupoSuspendidoDa.ListarAsocGrupoSolicitud(_GrupoSuspendido.idGrupoSuspend, 0, 0);
                            //UpdatePanelAsociacionSolUE.Update();
                        }
                        
                    }
            }
        }

        protected void inicializarFormulario()
        {
            cargarCombobox("TipoAgrupacion");
            cargarCombobox("EsDocumentoFinal");
            cargarCombobox("TipoSolicitud");
        }

        protected void cargarCombobox(string combobox)
        {
            switch (combobox)
            {

                case "EsDocumentoFinal":

                    EsDocumentoFinal.Items.Clear();
                    EsDocumentoFinal.DataBind();
                    EsDocumentoFinal.Items.Insert(0, new ListItem("Si", "1"));
                    EsDocumentoFinal.Items.Insert(0, new ListItem("No", "0"));
                    EsDocumentoFinal.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

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
                    
                    TipoSolicitud.Items.Clear();
                    TipoSolicitud.DataSource = parametroGenericoDA.ListarTipoTramiteSolicitud();
                    TipoSolicitud.DataTextField = "nombreTipoInterfaz";
                    TipoSolicitud.DataValueField = "idTipoTramite";
                    TipoSolicitud.DataBind();
                    TipoSolicitud.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
            }
        }

        protected void SeleccionTipoAgrupacion_Selected(object sender, EventArgs e)
        {
            int Index = Convert.ToInt32(TipoAgrupacion.SelectedValue);
            if (Index == 642)
            {
                CodigoCentro.Enabled = true;
                UpdatePanelCodigoCentro.Update();
            }
            else
            {
                CodigoCentro.Enabled = false;
                CodigoCentro.Text = "";
                UpdatePanelCodigoCentro.Update();
            }
        }

        protected void AgregarArchivoAdjunto_Click(object sender, ImageClickEventArgs e)
        {
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
                archivoBinario.archivo = ArchivoAdjunto.PostedFile;

                archivoBinario.numero = NumeroCI.Text;
                archivoBinario.idGrupoSusp = Convert.ToInt32(Application["id"]);
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

                    //se debe crear un PA donde modifique o agregue los archivos a este 
                    //ViewState["ArchivoBinarioList"] = (List<ArchivoBinario>)archivoBinarioList;

                    bool resp = mantenedorGeneralService.guardarArchivoGrupoSuspendido(archivoBinario, Convert.ToInt32(Application["id"]));
                    if (resp)
                    {
                        msgGrilla.Text = "Se ha guardado el Archivo exitosamente.";
                        Content_msgGrilla.Visible = true;
                        GridArchivoAdjunto.DataSource = GrupoSuspendidoDa.ListarArchivoGrupoSusp(Convert.ToInt32(Application["id"]), 0);
                        GridArchivoAdjunto.DataBind();
                        if (archivoBinario.docFinal == true)
                        {
                            Response.Redirect("~/Mantenedores/GrupoSuspendido/AdministradorGrupoSuspendido.aspx");
                        }
                    }
                    else
                    {
                        msgGrilla.Text = "No se ha podido guardar el Archivo.";
                        Content_msgGrilla.Visible = true;
                    }


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
        }

        protected void GridViewGridArchivoAdjunto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //ELIMINAR
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar?')");
                    boton_eliminar.Visible = true;
                };
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
                case "Eliminar":
                    id = Convert.ToInt32(e.CommandArgument);
                    GridViewGridArchivoAdjunto_Eliminar(id);
                    break;

                case "Descargar":
                    idArchivo = Convert.ToInt32(e.CommandArgument);
                    List<ArchivoBinario> ListarchivoBinario = GrupoSuspendidoDa.ListarArchivoGrupoSusp(0,idArchivo); //Obtener el archivo binario que se desea.
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

        protected void GridViewGridArchivoAdjunto_Eliminar(int id)
        {
            if (id > 0)
            {
                bool bol = mantenedorGeneralService.EliminarArchivoGrupoSuspendido(Convert.ToInt32(Application["id"]), id);
                if (bol)
                {
                    //success!
                    msgGrilla.Text = "Se ha eliminado el archivo exitosamente.";

                    GridArchivoAdjunto.DataSource = GrupoSuspendidoDa.ListarArchivoGrupoSusp(Convert.ToInt32(Application["id"]), 0);
                    GridArchivoAdjunto.DataBind();
                }
                else 
                { 
                    //error °.°
                    msgGrilla.Text = "No se ha podido eliminar el archivo.";
                }
            }
        }

        protected void GridViewAsociacionSolUE_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string vigencia = ((Label)e.Row.FindControl("EstadoVigencia")).Text;

                if (Convert.ToInt32(vigencia) == rbEstadosGenerales.VIGENTE)
                {
                    ImageButton boton_novigente = (ImageButton)e.Row.FindControl("gNoVigente");
                    if (boton_novigente != null)
                    {
                        boton_novigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Esta Seguro en dejar esta solicitud en no vigente?')");
                        boton_novigente.Visible = true;
                    }
                }
                if (Convert.ToInt32(vigencia) == rbEstadosGenerales.NO_VIGENTE)
                {
                    ImageButton boton_vigente = (ImageButton)e.Row.FindControl("gVigente");
                    if (boton_vigente != null)
                    {
                        boton_vigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Esta seguro en dejar esta solicitud vigente?')");
                        boton_vigente.Visible = true;
                    }
                }
            }

        }

        protected void GridViewAsociacionSolUE_RowCommand(object sender, GridViewCommandEventArgs e)
        { 
            int id;
            switch (e.CommandName)
            {
                case "Vigente":
                    id = Convert.ToInt32(e.CommandArgument);
                    GridViewAsociacionSolUE_CambioEstado(id, 7);
                    break;

                case "NoVigente":
                    id = Convert.ToInt32(e.CommandArgument);
                    GridViewAsociacionSolUE_CambioEstado(id, 6);
                    break;
            }
        }

        protected void GridViewAsociacionSolUE_CambioEstado(int id,int vigencia)
        {
            if (id > 0)
            {
                    if (vigencia == 6)
                        {
                            
                            if (mantenedorGeneralService.CambioEstado_AsocSolConGrupoSuspendido(Convert.ToInt32(Application["id"]), id, 7))
                            {
                                msgGrilla.Text = "Se ha cambiado la vigencia exitosamente.";
                                Content_msgGrilla.Visible = true;

                                ListarGrillaSolicitudGrupoSusp(Convert.ToInt32(Application["id"]));

                               // GridViewAsociacionSolUE.DataSource = GrupoSuspendidoDa.ListarAsocGrupoSolicitud(Convert.ToInt32(Application["id"]), 0, 0);
                                //GridViewAsociacionSolUE.DataBind();
                                //UpdatePanelAsociacionSolUE.Update();
                                //UpdatePanelSolicitudesUE.Update();

                            }
                            else
                            {
                                msgGrilla.Text = "No se Pudo cambiar la vigencia.";
                                Content_msgGrilla.Visible = true;

                                ListarGrillaSolicitudGrupoSusp(Convert.ToInt32(Application["id"]));
                                
                               // GridViewAsociacionSolUE.DataSource = GrupoSuspendidoDa.ListarAsocGrupoSolicitud(Convert.ToInt32(Application["id"]), 0, 0);
                                //GridViewAsociacionSolUE.DataBind();
                                //UpdatePanelAsociacionSolUE.Update();
                                //UpdatePanelSolicitudesUE.Update();
                            }
                        }
                    if (vigencia == 7)
                        {
                               
                            if (mantenedorGeneralService.CambioEstado_AsocSolConGrupoSuspendido(Convert.ToInt32(Application["id"]), id, 6))
                            {
                                msgGrilla.Text = "Se ha cambiado la vigencia exitosamente.";
                                Content_msgGrilla.Visible = true;
                                //Listar_Grilla();
                                GridViewAsociacionSolUE.DataSource = GrupoSuspendidoDa.ListarAsocGrupoSolicitud(Convert.ToInt32(Application["id"]), 0, 0);
                                GridViewAsociacionSolUE.DataBind();
                                //UpdatePanelAsociacionSolUE.Update();
                                //UpdatePanelSolicitudesUE.Update();
                            }
                            else
                            {
                                msgGrilla.Text = "No se Pudo cambiar la vigencia.";
                                Content_msgGrilla.Visible = true;
                                //Listar_Grilla();
                                GridViewAsociacionSolUE.DataSource = GrupoSuspendidoDa.ListarAsocGrupoSolicitud(Convert.ToInt32(Application["id"]), 0, 0);
                                GridViewAsociacionSolUE.DataBind();
                                //UpdatePanelAsociacionSolUE.Update();
                                //UpdatePanelSolicitudesUE.Update();
                            }
                        }
                    }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error, No se pudo realizar la acción');", true);
            }
            
        }

        public void Identificador_TextChanged(object sender, EventArgs e)
        {
            Titulares.Text = "";
            Region.Text = "";
            Toponimio.Text = "";

            ResolucionService resolucionService = new ResolucionService();

                if (Convert.ToInt32(TipoSolicitud.SelectedValue) > 0)
                {

                    if (Convert.ToInt32(TipoSolicitud.SelectedValue) == rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA && !NumeroPert.Text.Trim().Equals("") && Convert.ToInt32(NumeroPert.Text) > 0) //COLECTORES
                    {

                        //OBTENER LA SOLICITUD
                        SolicitudConcesion sol = resolucionService.VerificaExistenciaReferencia(Convert.ToInt32(rbTipo.RESOLUCION_SUB_REFERENCIA_SOLICITUD), 0, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA, NumeroPert.Text, 0);

                        if (sol != null && sol.idSolConcesion > 0)
                        {
                            Titulares.Text = sol.titularesCad;
                            if (sol.region != null)
                            {
                                Region.Text = sol.region.descripcion;
                            }
                            Toponimio.Text = sol.toponimiosCad;
                            //UpdatePanelSolicitudesUE.Update();
                            //UpdatePanelAsociacionSolUE.Update();
                        }

                    }
                    else if (Convert.ToInt32(TipoSolicitud.SelectedValue) == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION && !NumeroPert.Text.Trim().Equals("") && !NumeroPert.Text.Trim().Equals("")) //Sector Relocalizacion
                    {

                        String pertRelocalizaciones = NumeroPert.Text.Trim() + "-" + NumeroPert.Text.Trim().ToString();

                        //VERIFICAR QUE EXISTA
                        SolicitudConcesion sol = resolucionService.VerificaExistenciaReferencia(Convert.ToInt32(rbTipo.RESOLUCION_SUB_REFERENCIA_SOLICITUD), 0, rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION, pertRelocalizaciones, Convert.ToInt32(NumeroPert.Text.Trim()));


                        if (sol != null && sol.idSolConcesion > 0)
                        {

                            Titulares.Text = sol.titularesCad;
                            if (sol.region != null)
                            {
                                Region.Text = sol.region.descripcion;
                            }
                            Toponimio.Text = sol.toponimiosCad;
                            //UpdatePanelSolicitudesUE.Update();
                            //UpdatePanelAsociacionSolUE.Update();
                        }



                    }
                    else if (!NumeroPert.Text.Trim().Equals(""))  //OTROS TIPOS DE SOLICITUDES
                    {


                        //VERIFICAR QUE EXISTA
                        SolicitudConcesion sol = resolucionService.VerificaExistenciaReferencia(Convert.ToInt32(rbTipo.RESOLUCION_SUB_REFERENCIA_SOLICITUD), 0, Convert.ToInt32(TipoSolicitud.SelectedValue), NumeroPert.Text.Trim(), 0);


                        if (sol != null && sol.idSolConcesion > 0)
                        {

                            Titulares.Text = sol.titularesCad;
                            if (sol.region != null)
                            {
                                Region.Text = sol.region.descripcion;
                            }
                            Toponimio.Text = sol.toponimiosCad;
                            //UpdatePanelSolicitudesUE.Update();
                            //UpdatePanelAsociacionSolUE.Update();
                        }
                    }
                }
            

        }

        protected void ImageButtonAsociacionSolUE_Click(object sender, ImageClickEventArgs e)
        {
            string error = "";
            List<String> listaErroresAsociacionGrupoSolicitud = new List<string>(); //Validar los campos del ingreso de archivos.
            List<AsocGrupoSolicitud> asocGrupoSolicitudList = (List<AsocGrupoSolicitud>)ViewState["AsocGrupoSolicitudList"];
            if (asocGrupoSolicitudList == null)
            {
                asocGrupoSolicitudList = new List<AsocGrupoSolicitud>();
            }
            else 
            {
                foreach (AsocGrupoSolicitud asoc in asocGrupoSolicitudList)
                {
                    if (asoc.solicitudConcesion.numPert.Equals(NumeroPert.Text))
                    {
                        error = "NO puede repetir un Pert/Identificador";
                        listaErroresAsociacionGrupoSolicitud.Add(error);
                    }
                }
            }

            AsocGrupoSolicitud asocGrupoSolicitud = new AsocGrupoSolicitud();
            asocGrupoSolicitud.solicitudConcesion = new SolicitudConcesion();

            //validar que el numero pert exista o la tabla se guardara vacia :( ademas de indicar el error! ): 
            int pert = solicitudDA.obtenerIdSolicitudFiltro(NumeroPert.Text, false);
            if (pert == 0)
            {
                error = "Debe ingresar un Pert/Identificador Valido";
                listaErroresAsociacionGrupoSolicitud.Add(error);
            }
            else
            {
                VisacionMasiva visacion = new VisacionMasiva();
                visacion.pertFiltro = NumeroPert.Text;
                visacion.tipoTramite = new ParametroGenerico();
                visacion.tipoTramite.id = Convert.ToInt32(TipoSolicitud.SelectedValue);

                string mensaje = "";
                mensaje = _VisacionDA.ValidarExistenciaPerts(visacion);
                if (mensaje != "")
                {
                    error = "El pert y el tipo de solicitud no concuerdan";
                    listaErroresAsociacionGrupoSolicitud.Add(error);
                }

                asocGrupoSolicitud.solicitudConcesion.numPert = NumeroPert.Text;
                asocGrupoSolicitud.solicitudConcesion.tipoTramite = new ParametroGenerico();
                asocGrupoSolicitud.solicitudConcesion.tipoTramite.id = Convert.ToInt32(TipoSolicitud.SelectedValue);
            }

            if (listaErroresAsociacionGrupoSolicitud != null && listaErroresAsociacionGrupoSolicitud.Count <= 0)
            {
                asocGrupoSolicitud.grupoSuspendido = new GrupoSuspendidos();
                asocGrupoSolicitud.grupoSuspendido.idGrupoSuspend = Convert.ToInt32(Application["id"]);
                asocGrupoSolicitud.estadoVigencia = new ParametroGenerico();
                asocGrupoSolicitud.estadoVigencia.id = rbEstadosGenerales.VIGENTE;
                asocGrupoSolicitud.solicitudConcesion.idConcesion = pert;
                asocGrupoSolicitud.solicitudConcesion.idSolConcesion = pert;
               //asocGrupoSolicitud.idAsocGrupoSolicitud = grupoSuspendido.idGrupoSuspend;


                asocGrupoSolicitudList.Add(asocGrupoSolicitud);

                GridViewAsociacionSolUE.DataSource = asocGrupoSolicitudList;
                GridViewAsociacionSolUE.DataBind();

                PanelGridViewAsociacionSolUE.Visible = true;
                //UpdatePanelAsociacionSolUE.Update();

                //agregar aqui en BDD
                //Se cae no estan levantados los parametrosgenericos
                //falta el valor de asociacion entre ascoGrupoSuspendido y grupoSuspendido
                bool resp = mantenedorGeneralService.guardarGrupoSuspendidoAsoc(asocGrupoSolicitud);
                if (resp)
                {
                    msgGrilla.Text = "Se ha guardado la Asociacion de Grupo/Solicitud exitosamente.";
                    Content_msgGrilla.Visible = true;
                    GridViewAsociacionSolUE.DataSource = GrupoSuspendidoDa.ListarAsocGrupoSolicitud(Convert.ToInt32(Application["id"]), 0, 0);
                    GridViewAsociacionSolUE.DataBind();

                    ViewState["AsocGrupoSolicitudList"] = GrupoSuspendidoDa.ListarAsocGrupoSolicitud(Convert.ToInt32(Application["id"]), 0, 0);
                    //UpdatePanelAsociacionSolUE.Update();

                    Titulares.Text = "";
                    Region.Text = "";
                    Toponimio.Text = "";
                    //UpdatePanelSolicitudesUE.Update();

                }
                else
                {

                    msgGrilla.Text = "No se ha guardado la Asociacion de Grupo/Solicitud.";
                    Content_msgGrilla.Visible = true;
                    //UpdatePanelAsociacionSolUE.Update();
                    Titulares.Text = "";
                    Region.Text = "";
                    Toponimio.Text = "";
                }

                //ViewState["AsocGrupoSolicitudList"] = (List<AsocGrupoSolicitud>)asocGrupoSolicitudList;
            }
            else
            {
                foreach (String errores in listaErroresAsociacionGrupoSolicitud)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }

            }
            TipoSolicitud.SelectedValue = "-1";
            NumeroPert.Text = "";
            Titulares.Text = "";
            Region.Text = "";
            Toponimio.Text = "";
            PanelCentro.Visible = true;
        }

        protected void GuardarGrupoSuspendido_Click(object sender, EventArgs e)
        {
            List<String> listaErroresGrupoSuspendido = new List<string>();
            grupoSuspendido.tipoAgrupacion = new ParametroGenerico();
            grupoSuspendido.tipoAgrupacion.id = Convert.ToInt32(TipoAgrupacion.SelectedItem.Value);

            grupoSuspendido.solicitudConcesion = new SolicitudConcesion();
            grupoSuspendido.solicitudConcesion.unidadEspacial = new UnidadEspacial();
            grupoSuspendido.solicitudConcesion.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
            grupoSuspendido.solicitudConcesion.unidadEspacial.centrosDeCultivo.codigoCentro = CodigoCentro.Text;

            if (grupoSuspendido.tipoAgrupacion.id == 642)
            {
                if (!CodigoCentro.Text.Equals(""))
                {
                    listaErroresGrupoSuspendido = ingresarSolicitudModificacionValidacion.validaCodigoCentro(CodigoCentro.Text);
                }
            }

            if (!NombreGrupo.Text.Equals(""))
            {
                grupoSuspendido.nombreGrupoSuspend = NombreGrupo.Text;
            }

            /* Listado de Archivo Adjuntos */
            /*
            List<ArchivoBinario> archivoBinarioList = (List<ArchivoBinario>)ViewState["ArchivoBinarioList"];
            if (archivoBinarioList != null && archivoBinarioList.Count > 0)
            {
                grupoSuspendido.archivoGrupoSusp = archivoBinarioList;
            }
            */

            /* Listado de Solicitudes de Unidades Espaciales */
            /*
            List<AsocGrupoSolicitud> asocGrupoSolicitudList = (List<AsocGrupoSolicitud>)ViewState["AsocGrupoSolicitudList"];
            if (asocGrupoSolicitudList != null && asocGrupoSolicitudList.Count > 0)
            {
                grupoSuspendido.asocGrupoSolicitud = asocGrupoSolicitudList;
            }
            */

            //Validar grupo suspendido

            if (listaErroresGrupoSuspendido != null && listaErroresGrupoSuspendido.Count <= 0)
            {
                grupoSuspendido.estadoVigencia = new ParametroGenerico();
                grupoSuspendido.estadoVigencia.id = rbEstadosGenerales.VIGENTE;
                //crear metodo para guardar evaluación
                bool resp = mantenedorGeneralService.guardarGrupoSuspendido(grupoSuspendido);
                if (resp)
                {
                    Application["id"] = grupoSuspendido.idGrupoSuspend;

                    msgGrilla.Text = "Se ha guardado el Grupo Suspendido exitosamente.";
                    Content_msgGrilla.Visible = true;
                    
                    PanelAsociacionUE.Visible = true;
                    PanelArchivoAdjuntoOculto.Visible = true;
                    GuardarGrupoSuspendido.Visible = false;


                    try
                    {
                        enviarCorreo.alertaGrupoSuspendido(grupoSuspendido);
                    }
                    catch (Exception)
                    {

                    }

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

        protected void ListarGrillaSolicitudGrupoSusp(int idGrupoSuspendido)
        {

            GridViewAsociacionSolUE.DataSource = GrupoSuspendidoDa.ListarAsocGrupoSolicitud(idGrupoSuspendido, 0, 0);
            GridViewAsociacionSolUE.DataBind();
            PanelCentro.Visible = true;
            PanelGridViewAsociacionSolUE.Visible = true;
        }

    }
}