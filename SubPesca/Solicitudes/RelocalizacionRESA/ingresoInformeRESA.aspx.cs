using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using Datos.Contantes;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.resoluciones;
using Datos.Entidades.Resolucion;
using Validaciones.cl.subpesca.rb.relocalizacion;
using Datos.Entidades.Relocalizacion;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;
using Datos.Utilidades;

namespace SubPesca.Solicitudes.RelocalizacionRESA
{
    public partial class ingresoInformeRESA : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        PermisosService permisosService = new PermisosService();
        ResolucionService resolucionService = new ResolucionService();
        String erroresSumary = "ValidationSummaryInformeRESA";
        IngresoInformeRESAValidacion ingresoInformeRESAValidacion = new IngresoInformeRESAValidacion();
        RelocalizacionRESAService relocalizacionRESAService = new RelocalizacionRESAService();

        String mensaje = "";

        public String MensajeRegistro
        {
            get
            {
                return mensaje;
            }
        }



        protected void setearModulo()
        {

            ViewState["URL_VER"] = paginas.URL_VER_SOLCONCESION;
            ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLCONCESION;
            ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLCONCESION;
            ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLCONCESION;
            ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLCONCESION;
            ViewState["solicitudSession"] = paginas.solicitudConcesionSession;


            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.INGRESO_INFORME_RESA };

        }




        protected void Page_Load(object sender, EventArgs e)
        {

            // PAGE LOAD
            if (!Page.IsPostBack)
            {
                setearModulo();
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];


                ValidationSummaryErrores.ValidationGroup = erroresSumary;




                // Inicializamos el formulario
                Initialize_Form();

                //SE TRATA DE UNA MODIFICACION
                if (Request.QueryString["idInformeRel"] != null && Convert.ToInt32(Request.QueryString["idInformeRel"]) > 0)
                {

                    InformeRel_RESA informeFiltro = new InformeRel_RESA();
                    informeFiltro.idInformeRel = Convert.ToInt32(Request.QueryString["idInformeRel"]);

                    InformeRel_RESA informeBD = relocalizacionRESAService.ObtenerInformeRESA(informeFiltro);

                    if (informeBD != null)
                    {
                        this.cargarInforme(informeBD);
                    }


                    Titulo.Text = "Modificación de Informe";

                }
                else
                {

                    Titulo.Text = "Ingreso de Informe";

                }

            }
        }

       
        protected void Initialize_Form()
        {
            // Cargamos los combobox
            Initialize_Comboboxs();

        }



        protected void Initialize_Comboboxs()
        {

            Carga_Combobox("FlujoDocumental");
            FlujoDocumental.SelectedValue = "0";

            Carga_Combobox("TipoEntrada");
            TipoEntrada.SelectedValue = "0";

            Carga_Combobox("Origen");
            Origen.SelectedValue = "0";
            Carga_Combobox("TipoDocumento");
            TipoDocumento.SelectedValue = "0";

            Carga_Combobox("Materia");
            Materia.SelectedValue = "0";


        }


        protected void Carga_Combobox(string combobox)
        {

            switch (combobox)
            {

                case "FlujoDocumental":

                    FlujoDocumental.Items.Clear();
                    FlujoDocumental.Items.Add(new ListItem("Entrada", Convert.ToString(rbTipo.ENTRADA)));
                    FlujoDocumental.DataBind();
                    FlujoDocumental.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                case "TipoEntrada":
                    
                    TipoEntrada.Items.Clear();
                    TipoEntrada.Items.Add(new ListItem("Ingreso sin Requerimiento", Convert.ToString(rbTipo.INGRESO_SIN_REQUERIMIENTO)));
                    TipoEntrada.DataBind();
                    TipoEntrada.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;


                case "Origen":

                    Origen.Items.Clear();
                    Origen.Items.Add(new ListItem("Unidad Tramites Sectoriales", Convert.ToString(11)));
                    Origen.DataBind();
                    Origen.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;


                case "Materia":
                    
                    Materia.Items.Clear();
                    Materia.Items.Add(new ListItem("Informe Relocalización RESA", Convert.ToString(28)));
                    Materia.DataBind();
                    Materia.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                case "TipoDocumento":
                    
                    TipoDocumento.Items.Clear();
                    TipoDocumento.Items.Add(new ListItem("Informe Técnico", Convert.ToString(585)));
                    TipoDocumento.DataBind();
                    TipoDocumento.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;


            };
        }




        //CARGAR INFORMACION DE LA UNIDAD ESPACIAL
        public void Identificador_TextChanged(object sender, EventArgs e)
        {

            titularesCAD.Text = "";
            regionCad.Text = "";
            toponimioCad.Text = "";
            UpdatePanelDatosCentro.Update();


            //VERIFICAR QUE EXISTA
            SolicitudConcesion sol = resolucionService.VerificaExistenciaReferencia(rbTipo.RESOLUCION_SUB_REFERENCIA_UE, rbTipo.UNID_ESPACIAL_CONCESION, 0, CodigoCentro.Text.Trim(), 0);

            if (sol != null && sol.idSolConcesion > 0)
            {

                titularesCAD.Text = sol.titularesCad;
                if (sol.region != null)
                {
                    regionCad.Text = sol.region.descripcion;
                }
                toponimioCad.Text = sol.toponimiosCad;
                UpdatePanelDatosCentro.Update();
            }


        }
    



        protected void LimpiarPorFlujoDocumental()
        {

            TipoEntrada.SelectedValue = "0";
            Materia.SelectedValue = "0";
            TipoDocumento.SelectedValue = "0";
            Numero.Text = "";
            Fecha.Text = "";
            Origen.SelectedValue = "0";
            Observaciones.Text = "";
            ArchivoAdjunto.Attributes.Clear();
            idArchivo.Value = "";
            NombreArchivo.Text = "";

            UpdatePanelTipoEntrada.Update();
            UpdatePanelOrigen.Update();
            UpdatePanelSubRequerimiento.Update();
            UpdatePanelTipoDocumento.Update();
            UpdatePanelNumero.Update();
            UpdatePanelFecha.Update();
            UpdatePanelArchivo.Update();
            UpdatePanelObservaciones.Update();

        }



        protected void Limpiar_Click(object sender, EventArgs e)
        {
            FlujoDocumental.SelectedValue = "0";
            LimpiarPorFlujoDocumental();
        }


        protected void Guardar_Click(object sender, EventArgs e)
        {

            //Tipo Documento
            if (Convert.ToInt32(TipoDocumento.SelectedValue) < 1)
            {
                Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Tipo de Documento"));
            }


            //Origen
            if (Convert.ToInt32(Origen.SelectedValue) < 1)
            {
                Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Origen"));
            }


            //Materia
            if (Convert.ToInt32(Materia.SelectedValue) < 1)
            {
                Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Materia"));
            }


            //Numero
            if (Numero.Text.Trim().Equals(""))
            {
                Page.Validators.Add(new ValidationError(erroresSumary, "Ingrese Número"));
            }


            //Fecha
            if (Fecha.Text.Trim().Equals(""))
            {
                Page.Validators.Add(new ValidationError(erroresSumary, "Ingrese Fecha"));
            }

            


            bool tieneAsociaciones = false;

            List<AsocInformeSolicitud> listaView = (List<AsocInformeSolicitud>)ViewState["UnidadesEspaciales"];

            if(listaView != null){

                foreach(AsocInformeSolicitud  asocInformeSolicitudInList in listaView){

                    if (asocInformeSolicitudInList.accion == accion.INGRESAR || asocInformeSolicitudInList.accion == accion.MODIFICAR || asocInformeSolicitudInList.accion == accion.LISTADO) {
                        tieneAsociaciones = true; 
                        break;
                    }
                }
            }




            if (!tieneAsociaciones)
            {
                Page.Validators.Add(new ValidationError(erroresSumary, "Ingrese al menos una concesión de acuicultura"));
            }



            if (Page.IsValid)
            {

                InformeRel_RESA informe = new InformeRel_RESA();

                if (!IdInforme.Value.Trim().Equals(""))
                {
                    informe.idInformeRel = Convert.ToInt32(IdInforme.Value);
                }
                informe.tipoDocumento       = new ParametroGenerico(Convert.ToInt32(TipoDocumento.SelectedValue));
                informe.tipoDestinatario    = new ParametroGenerico(Convert.ToInt32(Origen.SelectedValue));
                informe.materia             = new ParametroGenerico(Convert.ToInt32(Materia.SelectedValue));
                informe.numero = Convert.ToString(Numero.Text);
                informe.fecha = Convert.ToDateTime(Fecha.Text);
                informe.estadoVigencia = new ParametroGenerico(rbEstadosGenerales.VIGENTE);

                if (!Observaciones.Text.Trim().Equals("")) {
                    informe.observaciones = Observaciones.Text;
                }
                
                informe.asocInformeSolicitud = (List<AsocInformeSolicitud>)ViewState["UnidadesEspaciales"];


                //CAMBIO EL ARCHIVO
                if (ArchivoAdjunto.HasFile)
                {

                    ArchivoBinario archivoBinario = new ArchivoBinario();

                    archivoBinario.nombreArchivo = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
                    archivoBinario.nombreFisico = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
                    archivoBinario.formato = ArchivoAdjunto.PostedFile.FileName.Substring(ArchivoAdjunto.PostedFile.FileName.LastIndexOf(".") + 1).ToLower(); ;
                    archivoBinario.tamano = ArchivoAdjunto.PostedFile.InputStream.Length;
                    archivoBinario.archivo = ArchivoAdjunto.PostedFile;

                    informe.archivoBinSC = archivoBinario;
                }
                else
                {

                    //NO HIZO NADA CON EL ARCHIVO, SE DEBE MANTENER
                    if (!idArchivo.Value.Equals("") && Convert.ToInt32(idArchivo.Value) > 0)
                    {
                        ArchivoBinario archivoBinario = new ArchivoBinario();
                        archivoBinario.idArchivo = Convert.ToInt32(idArchivo.Value);

                        informe.archivoBinSC = archivoBinario;

                    }

                    //BORRE EL ARCHIVO
                    if (idArchivo.Value.Equals(""))
                    {
                        informe.archivoBinSC = null;
                    }
                }



                
                ErroresSuperior.Text = "";

                if (informe.idInformeRel == 0)
                {

                    if (!relocalizacionRESAService.GuardarInformeRESA(informe))
                    {

                        PanelErroresSuperior.Visible = true;
                        ErroresSuperior.Text = "Ha ocurrido un error al realizar la accion solicituda";
                        UpdatePanelErroresSuperior.Update();
                    }
                    else
                    {

                        this.mensaje = "Informe guardado con exito";
                        Server.Transfer("administrarInformesRESA.aspx");
                        UpdatePanelErroresSuperior.Update();
                    }


                }
                else
                {

                    usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];
                    informe.usuario = new Usuario();
                    informe.usuario.id_usuario = usuario_logeado.id_usuario;

                    if (!relocalizacionRESAService.ActualizarInformeRESA(informe))
                    {

                        PanelErroresSuperior.Visible = true;
                        ErroresSuperior.Text = "Ha ocurrido un error al realizar la accion solicituda";
                        UpdatePanelErroresSuperior.Update();
                    }
                    else
                    {

                        this.mensaje = "Informe actualizado con éxito";
                        Server.Transfer("administrarInformesRESA.aspx");
                        UpdatePanelErroresSuperior.Update();
                    }
                    

                }

                UpdatePanelErroresSuperior.Update();


            }
            

            UpdatePanelMensajesValidaciones.Update();


            if (PanelFecha.Visible == true)
            {
                string script = "calendario('" + Fecha.ClientID + "','" + imgFecha.ClientID + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + Fecha.ClientID, script.ToString(), true);
            }


        }




        //GRID UNIDADES ESPACIALES
        protected void GridUnidadEspacial_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
             
                //idResolucion
                HiddenField hidden_idReferencia = (HiddenField)e.Row.FindControl("gIdReferencia");
                if (hidden_idReferencia != null && !hidden_idReferencia.Value.Equals("") && Convert.ToInt32(hidden_idReferencia.Value) > 0)
                {

                    ImageButton boton_noVigente = (ImageButton)e.Row.FindControl("gNoVigente");
                    if (boton_noVigente != null)
                    {

                        HiddenField hidden_idVigencia = (HiddenField)e.Row.FindControl("gIdVigencia");

                        boton_noVigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea dejar no vigente esta referencia?')");
                        if (hidden_idVigencia != null && !hidden_idVigencia.Value.Equals("") && Convert.ToInt32(hidden_idVigencia.Value) == rbEstadosGenerales.VIGENTE)
                        {
                            boton_noVigente.Visible = true;
                        }

                    };

                    ImageButton boton_vigente = (ImageButton)e.Row.FindControl("gVigente");
                    if (boton_vigente != null)
                    {

                        HiddenField hidden_idVigencia = (HiddenField)e.Row.FindControl("gIdVigencia");

                        boton_vigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea dejar vigente esta referencia?')");
                        if (hidden_idVigencia != null && !hidden_idVigencia.Value.Equals("") && Convert.ToInt32(hidden_idVigencia.Value) == rbEstadosGenerales.NO_VIGENTE)
                        {
                            boton_vigente.Visible = true;
                        }
                    };

                };



                //ACCION
                HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
                if (hidden_accion != null && !hidden_accion.Value.Equals("") && Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR)
                {
                    e.Row.Attributes["style"] = "display:none";
                };


                //Borrar
                ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
                if (boton_borrar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.ELIMINAR))
                    {
                        boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar este registro?')");
                        boton_borrar.Visible = true;
                    }
                };

                  
             
            };
        }

        //GRID UNIDADES ESPACIALES
        protected void GridUnidadEspacial_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int claveFila = Convert.ToInt32(e.CommandArgument.ToString());
            List<AsocInformeSolicitud> listaView = (List<AsocInformeSolicitud>)ViewState["UnidadesEspaciales"];

            ErroresInferior.Text = "";
            PanelErroresInferior.Visible = false;
            UpdatePanelErroresInferior.Update();


            switch (e.CommandName)
            {


                case "Eliminar":


                    try
                    {

                        //VALIDAR QUE SEA POSIBLE ELIMINAR
                        List<String> errores = new List<string>();
                        if (errores.Count == 0)
                        {

                            if (listaView != null)
                            {
                                foreach (AsocInformeSolicitud asociacion in listaView)
                                {

                                    if (asociacion.idSolConcesion == claveFila)
                                    {
                                        asociacion.accion = accion.ELIMINAR;
                                    }
                                }
                            }

                            ErroresGrillaUnidadEspacial.Text = "Accion Realizada con exito";
                            PanelErroresGrillaUnidadEspacial.Visible = true;
                            UpdatePanelErroresGrillaUnidadEspacial.Update();

                        }
                        else
                        {

                            foreach (String error in errores)
                            {
                                ErroresGrillaUnidadEspacial.Text = ErroresInferior.Text + error + "<br/>";
                            }
                            PanelErroresGrillaUnidadEspacial.Visible = true;
                            UpdatePanelErroresGrillaUnidadEspacial.Update();

                        }

                    }
                    catch (Exception ex)
                    {


                        ErroresGrillaUnidadEspacial.Text = "Ha ocurrido un error al realizar la acción solicitada";
                        PanelErroresGrillaUnidadEspacial.Visible = true;
                        UpdatePanelErroresGrillaUnidadEspacial.Update();


                    }



                    ViewState["UnidadesEspaciales"] = listaView;
                    CargarListaUnidadEspaciales();
                    break;



                case "NoVigente":

                    try
                    {

                        if (listaView != null && claveFila > 0)
                        {

                            foreach (AsocInformeSolicitud asociacionInList in listaView)
                            {

                                if (asociacionInList.idSolConcesion == claveFila)
                                {
                                    asociacionInList.estadoAsoc = new ParametroGenerico(rbEstadosGenerales.NO_VIGENTE);
                                    break;
                                }

                            }
                        }

                        ErroresGrillaUnidadEspacial.Text = "Accion Realizada con exito";
                        PanelErroresGrillaUnidadEspacial.Visible = true;
                        UpdatePanelErroresGrillaUnidadEspacial.Update();

                    }
                    catch (Exception ex)
                    {

                        ErroresGrillaUnidadEspacial.Text = "Ha ocurrido un error al realizar la acción solicitada";
                        PanelErroresGrillaUnidadEspacial.Visible = true;
                        UpdatePanelErroresGrillaUnidadEspacial.Update();

                    }

                    ViewState["UnidadesEspaciales"] = listaView;
                    this.CargarListaUnidadEspaciales();
                    break;


                case "Vigente":


                    try
                    {

                        if (listaView != null && claveFila > 0)
                        {

                            foreach (AsocInformeSolicitud asociacionInList in listaView)
                            {

                                if (asociacionInList.idSolConcesion == claveFila)
                                {
                                    asociacionInList.estadoAsoc = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
                                    break;
                                }

                            }
                        }

                        ErroresGrillaUnidadEspacial.Text = "Accion Realizada con exito";
                        PanelErroresGrillaUnidadEspacial.Visible = true;
                        UpdatePanelErroresGrillaUnidadEspacial.Update();

                    }
                    catch (Exception ex)
                    {

                        ErroresGrillaUnidadEspacial.Text = "Ha ocurrido un error al realizar la acción solicitada";
                        PanelErroresGrillaUnidadEspacial.Visible = true;
                        UpdatePanelErroresGrillaUnidadEspacial.Update();

                    }


                    ViewState["UnidadesEspaciales"] = listaView;
                    this.CargarListaUnidadEspaciales();
                    break;


            };


        }


        //GRID UNIDADES ESPACIALES
        private void CargarListaUnidadEspaciales()
        {

            List<AsocInformeSolicitud> listaUnidadesEspaciales = (List<AsocInformeSolicitud>)ViewState["UnidadesEspaciales"];

            if (listaUnidadesEspaciales == null)
            {
                listaUnidadesEspaciales = new List<AsocInformeSolicitud>();
            }


            GridUnidadEspacial.DataSource = listaUnidadesEspaciales;
            GridUnidadEspacial.DataBind();

            


        }

        //GRID UNIDADES ESPACIALES
        protected void GridUnidadEspacial_Agregar(object sender, EventArgs e)
        {

            List<AsocInformeSolicitud> listaUnidadesEspaciales = (List<AsocInformeSolicitud>)ViewState["UnidadesEspaciales"];
            int index = 0;


            if (listaUnidadesEspaciales == null)
            {
                listaUnidadesEspaciales = new List<AsocInformeSolicitud>();
            }
            else
            {
                index = listaUnidadesEspaciales.Count;
            }


            AsocInformeSolicitud newAsociacion = new AsocInformeSolicitud();
            newAsociacion.index = index;
            newAsociacion.estadoAsoc = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
            newAsociacion.accion = accion.INGRESAR;


            if (!CodigoCentro.Text.Trim().Equals(""))
            {
                newAsociacion.codigoCentro = Convert.ToInt32(CodigoCentro.Text.Trim());
            }

            //VALIDAR EL INGRESO DE LA NUEVA ASOCIACION
            List<String> errores = ingresoInformeRESAValidacion.validarIngresoUnidadEspacial(newAsociacion);

            if (errores.Count > 0)
            {
                foreach (String error in errores)
                {
                    PanelErroresUnidadEspacial.Visible = true;
                    ErroresUnidadEspacial.Text = error;
                    UpdatePanelErroresUnidadEspacial.Update();
                }
            }
            else
            {

                CodigoCentro.Text = "";
                UpdatePanelCodigoCentro.Update();
                
                ErroresUnidadEspacial.Text = "";
                PanelErroresUnidadEspacial.Visible = false;
                UpdatePanelErroresUnidadEspacial.Update();

                listaUnidadesEspaciales.Add(newAsociacion);

                ViewState["UnidadesEspaciales"] = (List<AsocInformeSolicitud>)listaUnidadesEspaciales;
                this.CargarListaUnidadEspaciales();

            }

            UpdatePanelErroresSuperior.Update();
        }





        protected void LimpiarArchivo_Click(object sender, EventArgs e)
        {
            idArchivo.Value = "";
            ArchivoAdjunto.Attributes.Clear();
            NombreArchivo.Text = "";
            UpdatePanelArchivo.Update();

        }




        //CARGA UN INFOME EXISTENTE EN LA BASE DE DATOS
        protected void cargarInforme(InformeRel_RESA informeDB)
        {


            if (informeDB != null)
            {


                IdInforme.Value = Convert.ToString(informeDB.idInformeRel);


                TipoDocumento.SelectedValue = Convert.ToString(informeDB.tipoDocumento.id);

                Origen.SelectedValue = Convert.ToString(informeDB.tipoDestinatario.id);

                if (informeDB.numero != null)
                {
                    Numero.Text = Convert.ToString(informeDB.numero);
                }

                if (informeDB.fecha != null && informeDB.fecha != default(DateTime))
                {
                    Fecha.Text = FechaUtils.formatearFecha(informeDB.fecha);
                }

                Materia.SelectedValue = Convert.ToString(informeDB.materia.id);


                if (informeDB.observaciones != null && !informeDB.observaciones.Trim().Equals(""))
                {
                    Observaciones.Text = Convert.ToString(informeDB.observaciones);
                }


                if (informeDB.archivoBinSC != null && informeDB.archivoBinSC.idArchivo > 0)
                {
                    idArchivo.Value = Convert.ToString(informeDB.archivoBinSC.idArchivo);
                    NombreArchivo.Text = informeDB.archivoBinSC.nombreArchivo;
                    UpdatePanelArchivo.Update();
                }



                if (informeDB.asocInformeSolicitud != null)
                {

                    //Vinculación Unidad Espacial
                    ViewState["UnidadesEspaciales"] = (List<AsocInformeSolicitud>)informeDB.asocInformeSolicitud;
                    this.CargarListaUnidadEspaciales();

                }


            }

        }


        public class ValidationError : CustomValidator
        {
            public ValidationError(string group, string msg)
                : base()
            {
                base.ValidationGroup = group;
                base.ErrorMessage = msg;
                base.IsValid = false;
            }
        }
  



    }
}