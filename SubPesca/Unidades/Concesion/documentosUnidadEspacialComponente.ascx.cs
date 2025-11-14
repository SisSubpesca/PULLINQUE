using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Validaciones.cl.subpesca.rb.solicitud;
using Datos.Contantes;
using Datos.Entidades;
using System.Collections;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using SubPesca.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using Datos.Utilidades;
using Datos.Entidades.Relocalizacion;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;
using LogicaNegocio.cl.subpesca.rb.servicios.modificacion;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;
using System.Web.UI.HtmlControls;
using LogicaNegocio.cl.subpesca.rb.servicios.documento;
using Validaciones.cl.subpesca.rb.documentos;

namespace SubPesca.Unidades.Concesion
{
    public partial class documentosUnidadEspacialComponente : System.Web.UI.UserControl
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema

        
        ParametroGenericoDA parametroDA = new ParametroGenericoDA();
        TipoDA tipoDA = new TipoDA();
        PestanaDA pestanaDA = new PestanaDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        DocumentoConcesionValidacion documentoConcesionValidacion = new DocumentoConcesionValidacion();
        ValidacionDocumentacionDA validacionDocumentacionDA = new ValidacionDocumentacionDA();
        PermisosService permisosService = new PermisosService();
        GeneralValidacion generalValidacion = new GeneralValidacion();
        SolicitudDA solicitudDA = new SolicitudDA();
        DespliegueMenuSeccionDA despliegueMenuSeccionDA = new DespliegueMenuSeccionDA();
        Funciones funciones = new Funciones();
        RelocalizacionService relocalizacionService = new RelocalizacionService();
        DespliegueSeccionService despliegueSeccionService = new DespliegueSeccionService();
        SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();
        DocumentoUnidadEspacialService documentoUnidadEspacialService = new DocumentoUnidadEspacialService();
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();



        String erroresSumary = "ValidationSummaryDocumentos";



        protected void setearModulo()
        {

            ViewState["URL_VER"] = paginas.URL_VER_RESOLUCION;

            if (funciones.retornaModulo().Equals("Concesion"))
            {
                
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_CONCESION_DE_ACUICULTURA;
                ViewState["solicitudSession"] = paginas.solicitudConcesionSession;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_DOCUMENTO_CONCESION };

            }
            else if (funciones.retornaModulo().Equals("Acopio"))
            {
                
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_CENTRO_ACOPIO;
                ViewState["solicitudSession"] = paginas.solicitudAcopioSession;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_DOCUMENTO_ACOPIO };

            }
            else if (funciones.retornaModulo().Equals("Faenamiento"))
            {
                
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_CENTRO_DE_FAENAMIENTO;
                ViewState["solicitudSession"] = paginas.solicitudFaenamientoSession;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_DOCUMENTO_FAENAMIENTO };

            }
            else if (funciones.retornaModulo().Equals("Amerb"))
            {
                
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_CENTRO_EN_AMERB;
                ViewState["solicitudSession"] = paginas.solicitudAmerbSession;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_DOCUMENTO_AMERB };

            }          
            else if (funciones.retornaModulo().Equals("ECMPO"))
            {
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_ECMPO;
                ViewState["solicitudSession"] = paginas.solicitudECMPOSession;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_DOCUMENTO_ECMPO };
            
            }
            else if (funciones.retornaModulo().Equals("Colector"))
            {
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_COLECTOR_DE_SEMILLAS;
                ViewState["solicitudSession"] = paginas.solicitudColectorSession;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_DOCUMENTO_COLECTOR };

            }
            else if (funciones.retornaModulo().Equals("ExperimentalesConcesion"))
            {
                
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_EXPERIMENTALES_CONCESION;
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesConcesionSession;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_DOCUMENTO_EXPERIMENTAL_CONCESION };

            }
            else if (funciones.retornaModulo().Equals("ExperimentalesAmerb"))
            {

                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_EXPERIMENTALES_AMERB;
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesAmerbSession;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_DOCUMENTO_EXPERIMENTAL_AMERB };

            }
        }

     
        protected void Page_Load(object sender, EventArgs e)
        {

            // PAGE LOAD
            if (!Page.IsPostBack)
            {
                setearModulo();

                ValidationSummaryErrores.ValidationGroup = erroresSumary;

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];


                if (solicitudConcesion == null || usuario_logeado == null)
                {
                    Response.Redirect(ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"].ToString());
                }

                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {
                    PanelFormularioIngreso.Visible = true;
                    UpdatePanelFormularioIngreso.Update();
                }
                else
                {
                    PanelFormularioIngreso.Visible = false;
                    UpdatePanelFormularioIngreso.Update();
                }
                
                IdSolicitud.Value = Convert.ToString(solicitudConcesion.idSolConcesion);
                CargarListaDocumentos(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));


                // Inicializamos el formulario
                Initialize_Form();
            }
        }


        private void CargarListaDocumentos(Usuario.Serializable usuario_logeado, int IdSolicitud)
        {

            GridDocumento.DataSource = documentoUnidadEspacialService.ListarDocumentosConcesion(IdSolicitud);
            GridDocumento.DataBind();
        }


        protected void Initialize_Form()
        {
            // Cargamos los combobox
            Initialize_Comboboxs();

            SolicitudConcesion concesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

            if (concesion == null || usuario_logeado == null)
            {
                Response.Redirect(ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"].ToString());
            }


            if (concesion != null && concesion.idSolConcesion > 0)
            {
                //Aquí se debe cargar la información de la página.
                NombreUnidadEspacialResoluciones.Text = funciones.retornaModulo();
                UpdatePanelDocumentos.Update();
            }



        }


        protected void Initialize_Comboboxs()
        {

            Carga_Combobox("FlujoDocumental");
            FlujoDocumental.SelectedValue = "0";

            Carga_Combobox("TipoSalida");
            TipoSalida.SelectedValue = "0";

            Carga_Combobox("TipoEntrada");
            TipoEntrada.SelectedValue = "0";

            Carga_Combobox("Origen");
            Origen.SelectedValue = "0";

     
            Carga_Combobox("TipoDocumento");
            TipoDocumento.SelectedValue = "0";

            Carga_Combobox("Destinatario");
            Destinatario.SelectedValue = "0";


        }



        protected void Carga_Combobox(string combobox)
        {

            switch (combobox)
            {



                case "FlujoDocumental":


                    FlujoDocumental.Items.Clear();
                    FlujoDocumental.Items.Add(new ListItem("Entrada", Convert.ToString(rbTipo.ENTRADA)));
                    FlujoDocumental.Items.Add(new ListItem("Salida", Convert.ToString(rbTipo.SALIDA)));
                    FlujoDocumental.DataBind();
                    FlujoDocumental.Items.Insert(0, new ListItem("-- Seleccione --", "0"));


                    break;

                case "TipoEntrada":
                    // Cargamos el combobox: Tipo Entrada

                    TipoEntrada.Items.Clear();
                    TipoEntrada.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    TipoEntrada.Items.Add(new ListItem("Ingreso sin Requerimiento", Convert.ToString(rbTipo.INGRESO_SIN_REQUERIMIENTO)));
                    TipoEntrada.DataBind();


                    break;

                case "TipoSalida":
                    // Cargamos el combobox: Tipo Salida

                    TipoSalida.Items.Clear();
                    TipoSalida.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    TipoSalida.Items.Add(new ListItem("Informativo", Convert.ToString(rbTipo.INFORMATIVO)));
                    TipoSalida.DataBind();


                    break;


                case "Origen":
                    
                    Origen.Items.Clear();
                    Origen.DataSource = tipoDA.listarTipoDestinatario();
                    Origen.DataTextField = "descripcion";
                    Origen.DataValueField = "id";
                    Origen.DataBind();
                    Origen.Items.Insert(0, new ListItem("-- Seleccione --", "0"));


                    break;


                case "TipoDocumento":
                    // Cargamos el combobox: Tipo Documento
                    TipoDocumento.Items.Clear();
                    TipoDocumento.DataSource = parametroDA.ListarTiposGenerico("paSelRbTipo", "@grupo", "TIPO_DOCUMENTO", "idTipo", "nombreTipo");
                    TipoDocumento.DataTextField = "descripcion";
                    TipoDocumento.DataValueField = "id";
                    TipoDocumento.DataBind();
                    TipoDocumento.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;


                case "Destinatario":
                    // Cargamos el combobox: Destinatario
                    Destinatario.Items.Clear();
                    Destinatario.DataSource = tipoDA.listarTipoDestinatario();
                    Destinatario.DataTextField = "descripcion";
                    Destinatario.DataValueField = "id";
                    Destinatario.DataBind();
                    Destinatario.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;


            };
        }



        //1 = entrada
        //2 = salida
        protected void FlujoDocumental_change(object sender, EventArgs e)
        {

            LimpiarPorFlujoDocumental();

            if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.ENTRADA)
            {
                PanelTipoEntrada.Visible = true;
                PanelOrigen.Visible = true;

                PanelTipoSalida.Visible = false;
                PanelDestinatario.Visible = false;
            }
            else if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.SALIDA)
            {
                PanelTipoEntrada.Visible = false;
                PanelOrigen.Visible = false;

                PanelTipoSalida.Visible = true;
                PanelDestinatario.Visible = true;
            }
            else
            {
                PanelTipoEntrada.Visible = false;
                PanelOrigen.Visible = false;

                PanelTipoSalida.Visible = false;
                PanelDestinatario.Visible = false;
            }


            UpdatePanelTipoSalida.Update();
            UpdatePanelTipoEntrada.Update();
            UpdatePanelOrigen.Update();
            UpdatePanelDestinatario.Update();


        }



        protected void LimpiarPorFlujoDocumental()
        {

            TipoEntrada.SelectedValue = "0";
            TipoSalida.SelectedValue = "0";
            Origen.SelectedValue = "0";
            Destinatario.SelectedValue = "0";

            UpdatePanelTipoSalida.Update();
            UpdatePanelTipoEntrada.Update();
            UpdatePanelOrigen.Update();
            UpdatePanelDestinatario.Update();

        }




        //3 = Requerimiento con Respuesta
        //4 = Informativo
        protected void TipoSalida_change(object sender, EventArgs e)
        {

            LimpiarPorTipoSalida();

            PanelDestinatario.Visible = true;
            UpdatePanelDestinatario.Update();
            
        }



        protected void LimpiarPorTipoSalida()
        {

            TipoEntrada.SelectedValue = "0";
            Destinatario.SelectedValue = "0";
            Origen.SelectedValue = "0";


            PanelTipoEntrada.Visible = false;
            PanelDestinatario.Visible = false;
            PanelOrigen.Visible = false;
            

            ErroresSuperior.Text = "";
            PanelErroresSuperior.Visible = false;
            UpdatePanelErroresSuperior.Update();

            ErroresInferior.Text = "";
            PanelErroresInferior.Visible = false;
            UpdatePanelErroresInferior.Update();

            UpdatePanelTipoEntrada.Update();
            UpdatePanelDestinatario.Update();
            UpdatePanelOrigen.Update();
            

        }



        //5 = Respuesta a un Requerimiento
        //6 = Ingreso sin Requerimiento
        protected void TipoEntrada_change(object sender, EventArgs e)
        {

            LimpiarPorTipoEntrada();
            
            PanelOrigen.Visible = true;
            UpdatePanelOrigen.Update();
            
        }




        protected void LimpiarPorTipoEntrada()
        {

            TipoSalida.SelectedValue = "0";
            Destinatario.SelectedValue = "0";
            Origen.SelectedValue = "0";

            PanelTipoSalida.Visible = false;
            PanelOrigen.Visible = false;

            ErroresSuperior.Text = "";
            PanelErroresSuperior.Visible = false;
            UpdatePanelErroresSuperior.Update();

            ErroresInferior.Text = "";
            PanelErroresInferior.Visible = false;
            UpdatePanelErroresInferior.Update();

            UpdatePanelTipoSalida.Update();
            UpdatePanelOrigen.Update();
            UpdatePanelDestinatario.Update();


        }


     

        //MUESTRA/OCULTA CAMPOS
        private void controlarCamposLogicos(int idTipoIO)
        {

            //ENTRADA
            if (idTipoIO == rbTipo.INGRESO_SIN_REQUERIMIENTO)
            {

                PanelOrigen.Visible = true;
                PanelTema.Visible = true;
            }



            //SALIDA
            if (idTipoIO == rbTipo.INFORMATIVO)
            {
                PanelDestinatario.Visible = true;
                PanelTema.Visible = true;
            }

            
        }





        protected void LimpiarPorTipoDocumento()
        {

            Numero.Text = "";
            Fecha.Text = "";
            NumeroCI.Text = "";
            NumeroCIMensaje.Text = "";
            FechaCI.Text = "";
            
            PanelTema.Visible = false;
            PanelNumero.Visible = false;
            PanelFecha.Visible = false;
            PanelNumeroCI.Visible = false;
            PanelFechaCI.Visible = false;
            PanelArchivo.Visible = false;

            ErroresSuperior.Text = "";
            PanelErroresSuperior.Visible = false;
            UpdatePanelErroresSuperior.Update();

            ErroresInferior.Text = "";
            PanelErroresInferior.Visible = false;
            UpdatePanelErroresInferior.Update();


            //TipoDocumento.Focus();


            UpdatePanelTema.Update();
            UpdatePanelNumero.Update();
            UpdatePanelFecha.Update();
            UpdatePanelNumeroCI.Update();
            UpdatePanelFechaCI.Update();
            UpdatePanelArchivo.Update();

        }



        //GRID REQUERIMIENTO
        protected void GridDocumento_RowDataBound(object sender, GridViewRowEventArgs e)
        {

           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {



                //Ver
                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    //if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.VER))
                    //{
                    //boton_ver.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro...?')");
                    boton_ver.Visible = true;
                    //}
                };





                String idEstadoVigencia = ((Label)e.Row.FindControl("gIdEstadoVigencia")).Text;

                //No Vigente
                ImageButton boton_noVigente = (ImageButton)e.Row.FindControl("gNoVigente");
                if (boton_noVigente != null)
                {

                    boton_noVigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea dejar no vigente este documento?')");
                    if (Convert.ToInt32(idEstadoVigencia) == rbEstadosGenerales.VIGENTE)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ASOCIAR))
                        {
                            boton_noVigente.Visible = true;
                        }
                    }
                    
                };


                //Vigente
                ImageButton boton_vigente = (ImageButton)e.Row.FindControl("gVigente");
                if (boton_vigente != null)
                {
                    
                    boton_vigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea dejar vigente este documento?')");
                    if (Convert.ToInt32(idEstadoVigencia) == rbEstadosGenerales.NO_VIGENTE)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.DESASOCIAR))
                        {
                            boton_vigente.Visible = true;
                        }
                    }
                    
                };


                //Descargar
                if (DataBinder.Eval(e.Row.DataItem, "archivoAdjunto.idArchivo") != null) {

                    String idArchivoBinSC = DataBinder.Eval(e.Row.DataItem, "archivoAdjunto.idArchivo").ToString();
                    ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                    if (boton_descargar != null && idArchivoBinSC != null && !idArchivoBinSC.Equals("") && Convert.ToInt32(idArchivoBinSC) > 0)
                    {
                        boton_descargar.Visible = true;
                    };
                
                }


                //Borrar
                ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
                if (boton_borrar != null)
                {

                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ELIMINAR))
                    {
                        boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar este documento?')");
                        boton_borrar.Visible = true;
                    }
                    
                };

            };
        }


        //GRID DOCUMENTO
        protected void GridDocumento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            int idDocConcesion = Convert.ToInt32(e.CommandArgument.ToString());

            ErroresInferior.Text = "";
            PanelErroresInferior.Visible = false;
            UpdatePanelErroresInferior.Update();
          

            switch (e.CommandName)
            {
            
            
                case "Eliminar":

                    try
                    {
                        if (!documentoUnidadEspacialService.EliminarDocumentosConcesion(idDocConcesion, 0, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario))
                        {
                            ErroresInferior.Text = "Ha ocurrido un error al realizar la acción solicitada";
                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();

                        }
                        else {
                            ErroresInferior.Text = "Operación realizada con éxito";
                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();
                        
                        }

                        
                    }
                    catch (Exception ex)
                    {

                         ErroresInferior.Text = "";
                         PanelErroresInferior.Visible = false;
                         UpdatePanelErroresInferior.Update();

                    }
                    
                    CargarListaDocumentos(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));
                    break;

                case "NoVigente":

                    try
                    {
                        DocumentosConcesion docAux = documentoUnidadEspacialService.ObtenerDocumentoConcesion(idDocConcesion, Convert.ToInt32(IdSolicitud.Value));
                        docAux.estadoVigencia = new ParametroGenerico(rbEstadosGenerales.NO_VIGENTE);
                        docAux.idUsuario = ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario;
                        
                        if (!documentoUnidadEspacialService.ActualizarDocumentoConcesion(docAux))
                        {

                            ErroresInferior.Text = "Ha ocurrido un error al realizar la acción solicitada";
                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();

                        }
                        else {


                            ErroresInferior.Text = "Operación realizada con éxito";
                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();

                        }

                    }
                    catch (Exception ex) {

                        ErroresInferior.Text = "Ha ocurrido un error al realizar la acción solicitada";
                        PanelErroresInferior.Visible = true;
                        UpdatePanelErroresInferior.Update();
                    
                    }

                    
                    
                    CargarListaDocumentos(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));
                    break;


                case "Vigente":

                    try
                    {
                        DocumentosConcesion docAux = documentoUnidadEspacialService.ObtenerDocumentoConcesion(idDocConcesion, Convert.ToInt32(IdSolicitud.Value));
                        docAux.estadoVigencia = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
                        docAux.idUsuario = ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario;

                        if (!documentoUnidadEspacialService.ActualizarDocumentoConcesion(docAux))
                        {

                            ErroresInferior.Text = "Ha ocurrido un error al realizar la acción solicitada";
                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();

                        }
                        else
                        {


                            ErroresInferior.Text = "Operación realizada con éxito";
                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();

                        }

                    }
                    catch (Exception ex)
                    {

                        ErroresInferior.Text = "Ha ocurrido un error al realizar la acción solicitada";
                        PanelErroresInferior.Visible = true;
                        UpdatePanelErroresInferior.Update();

                    }
                    
                    CargarListaDocumentos(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));
                    break;


                case "Descargar":

                    try
                    {
                        DocumentosConcesion docAux = documentoUnidadEspacialService.ObtenerDocumentoConcesion(idDocConcesion, Convert.ToInt32(IdSolicitud.Value));


                        ArchivoBinario archivoBinario = archivoBinarioSolicitudDA.ObtenerArchivoBinarioSolicitud(docAux.archivoAdjunto.idArchivo);

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = archivoBinario.formato;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinario.nombreArchivo + "." + archivoBinario.formato);
                        Response.BinaryWrite(archivoBinario.bytes);
                        Response.Flush();
                        Response.End();


                    }
                    catch (Exception ex)
                    {

                        ErroresInferior.Text = "Ha ocurrido un error al realizar la acción solicitada";
                        PanelErroresInferior.Visible = true;
                        UpdatePanelErroresInferior.Update();

                    }

                    CargarListaDocumentos(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));
                    break;
            
            };


        }



        //GRID DOCUMENTO
        protected void GridDocumento_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }



        protected void Limpiar_Click(object sender, EventArgs e)
        {
            FlujoDocumental.SelectedValue = "0";
            LimpiarFormulario();
        }


        protected void LimpiarFormulario()
        {

            TipoEntrada.SelectedValue = "0";
            TipoSalida.SelectedValue = "0";
            Origen.SelectedValue = "0";
            Destinatario.SelectedValue = "0";

            TipoDocumento.SelectedValue = "0";
            Tema.Text = "";
            Numero.Text = "";
            Fecha.Text = "";
            NumeroCI.Text = "";
            FechaCI.Text = "";
            ArchivoAdjunto.Attributes.Clear();
            Observaciones.Text = "";

            UpdatePanelTipoSalida.Update();
            UpdatePanelTipoEntrada.Update();
            UpdatePanelOrigen.Update();
            UpdatePanelDestinatario.Update();

            UpdatePanelTipoDocumento.Update();
            UpdatePanelTema.Update();
            UpdatePanelNumero.Update();
            UpdatePanelFecha.Update();
            UpdatePanelNumeroCI.Update();
            UpdatePanelFechaCI.Update();
            UpdatePanelArchivo.Update();
            UpdatePanelObservaciones.Update();



        }


        protected void Guardar_Click(object sender, EventArgs e)
        {

            //Flujo Documental
            if (Convert.ToInt32(FlujoDocumental.SelectedValue) < 1)
            {
                Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Flujo Documental"));
            }


            if (Page.IsValid)
            {


                DocumentosConcesion documento = new DocumentosConcesion();
                documento.solicitud = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                //SE CAMBIO LA SOLICITUD EN SESSION, PERO LA PAGINA NO SE HA RECARGADO
                if (documento.solicitud.idSolConcesion != Convert.ToInt32(IdSolicitud.Value))
                {
                    Page.Validators.Add(new ValidationError(erroresSumary, "La solicitud ha cambiado, por favor recargue la página"));
                }
                documento.flujoDocumental = new ParametroGenerico(Convert.ToInt32(FlujoDocumental.SelectedValue));


                if (documento.flujoDocumental.id == rbTipo.ENTRADA)
                {

                    //Tipo entrada
                    if (Convert.ToInt32(TipoEntrada.SelectedValue) < 1)
                    {
                        Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Tipo de Entrada"));
                    }


                    if (Page.IsValid)
                    {
                        documento.tipoEntrada = new ParametroGenerico(Convert.ToInt32(TipoEntrada.SelectedValue));
                    }


                }


                if (documento.flujoDocumental.id == rbTipo.SALIDA)
                {
                    //Tipo salida
                    if (Convert.ToInt32(TipoSalida.SelectedValue) < 1)
                    {
                        Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Tipo de Salida"));
                    }

                    if (Page.IsValid)
                    {
                        documento.tipoSalida = new ParametroGenerico(Convert.ToInt32(TipoSalida.SelectedValue));
                    }


                }


                if (Page.IsValid)
                {

                    if (Convert.ToInt32(TipoDocumento.SelectedValue) > 0)
                    {
                        documento.tipoDocumento = new ParametroGenerico(Convert.ToInt32(TipoDocumento.SelectedValue));
                    }

                    if (!Numero.Text.Trim().Equals(""))
                    {
                        documento.numero = Convert.ToString(Numero.Text);
                    }

                    if (!Fecha.Text.Trim().Equals(""))
                    {
                        documento.fecha = Convert.ToDateTime(Fecha.Text);
                    }


                    if (Convert.ToInt32(Destinatario.SelectedValue) > 0)
                    {
                        documento.destinatario = new ParametroGenerico(Convert.ToInt32(Destinatario.SelectedValue));
                    }

                    if (Convert.ToInt32(Origen.SelectedValue) > 0)
                    {
                        documento.origen = new ParametroGenerico(Convert.ToInt32(Origen.SelectedValue));
                    }

                    if (!NumeroCI.Text.Trim().Equals(""))
                    {
                        documento.numeroCI = Convert.ToInt32(NumeroCI.Text);
                    }

                    if (!FechaCI.Text.Trim().Equals(""))
                    {
                        documento.fechaCI = Convert.ToDateTime(FechaCI.Text);
                    }

                   
                    //TEMA
                    if (!Tema.Text.Trim().Equals(""))
                    {

                        documento.nombreTema = Tema.Text.Trim();
                    }

                    //Observaciones
                    if (!Observaciones.Text.Trim().Equals(""))
                    {
                        documento.observaciones = Observaciones.Text.Trim();
                    }


                    if (ArchivoAdjunto.HasFile)
                    {

                        ArchivoBinario archivoBinario = new ArchivoBinario();

                        archivoBinario.nombreArchivo = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
                        archivoBinario.nombreFisico = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
                        archivoBinario.formato = ArchivoAdjunto.PostedFile.FileName.Substring(ArchivoAdjunto.PostedFile.FileName.LastIndexOf(".") + 1).ToLower(); ;
                        archivoBinario.tamano = ArchivoAdjunto.PostedFile.InputStream.Length;
                        archivoBinario.archivo = ArchivoAdjunto.PostedFile;

                        documento.archivoAdjunto = archivoBinario;
                    }


                    List<String> errores = documentoConcesionValidacion.validaIngresoDocumentoConcesion(documento);


                    if (errores.Count == 0)
                    {


                        bool resp = documentoUnidadEspacialService.GuardarDocumentosConcesion(documento);


                        if (resp)
                        {
                            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];
                            CargarListaDocumentos(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));

                            ErroresInferior.Text = "Se ha guardado exitosamente el documento";
                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();
                            this.LimpiarFormulario();
                            FlujoDocumental.SelectedValue = "0";
                            UpdatePanelFlujoDocumental.Update();

                        }
                        else
                        {
                            ErroresInferior.Text = "Ha ocurrido un error al guardar el documento";
                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();

                        }
                    }
                    else
                    {
                        foreach (String error in errores)
                        {
                            Page.Validators.Add(new ValidationError(erroresSumary, error));
                        }
                    }
                }
            }
            UpdatePanelMensajesValidaciones.Update();

           

        }

    }
}