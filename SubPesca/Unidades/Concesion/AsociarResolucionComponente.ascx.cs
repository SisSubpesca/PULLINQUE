using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Utilidades;
using Datos.Contantes;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.servicios.resoluciones;
using Validaciones.cl.subpesca.rb.resolucion;
using SubPesca.Utilidades;
using Datos.Entidades.Resolucion.ResolucionSolicitud;
using Datos.Entidades.Resolucion;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using Validaciones.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.common;

namespace SubPesca.Unidades.Concesion
{
    public partial class AsociarResolucionComponente : System.Web.UI.UserControl
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        IngresarDocumentoValidacion ingresarDocumentoValidacion = new IngresarDocumentoValidacion();
        Funciones funciones = new Funciones();
        String erroresSumary = "ValidationSummaryResolucion" ;
        PermisosService permisosService = new PermisosService();
        ResolucionService resolucionService = new ResolucionService();
        IngresoResolucionValidacion  ingresoResolucionValidacion = new IngresoResolucionValidacion();
        String labelDocumentos = "Resolución y Decretos";
        RequerimientoService requerimientoService = new RequerimientoService();

        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();


        protected void setearModulo()
        {

            ViewState["URL_VER"] = paginas.URL_VER_RESOLUCION;

            if(funciones.retornaModulo().Equals("Concesion"))
            {
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_CONCESION_DE_ACUICULTURA;
                ViewState["solicitudSession"] = paginas.solicitudConcesionSession;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ASOCIAR_RESOLUCION_CONCESION };
            }
            else if (funciones.retornaModulo().Equals("Acopio"))
            {
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_CENTRO_ACOPIO;
                ViewState["solicitudSession"] = paginas.solicitudAcopioSession;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ASOCIAR_RESOLUCION_ACOPIO };
            }
            else if (funciones.retornaModulo().Equals("Faenamiento"))
            {
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_CENTRO_DE_FAENAMIENTO;
                ViewState["solicitudSession"] = paginas.solicitudFaenamientoSession;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ASOCIAR_RESOLUCION_FAENAMIENTO };
            }
            else if (funciones.retornaModulo().Equals("Amerb"))
            {
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_CENTRO_EN_AMERB;
                ViewState["solicitudSession"] = paginas.solicitudAmerbSession;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ASOCIAR_RESOLUCION_AMERB };
            }
          
            else if (funciones.retornaModulo().Equals("ECMPO"))
            {
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_ECMPO;
                ViewState["solicitudSession"] = paginas.solicitudECMPOSession;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ASOCIAR_RESOLUCION_ECMPO };
            }
            else if (funciones.retornaModulo().Equals("Colector"))
            {
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_COLECTOR_DE_SEMILLAS;
                ViewState["solicitudSession"] = paginas.solicitudColectorSession;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ASOCIAR_RESOLUCION_COLECTOR };
            }
            else if (funciones.retornaModulo().Equals("ExperimentalesConcesion"))
            {
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_EXPERIMENTALES_CONCESION;
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesConcesionSession;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ASOCIAR_RESOLUCION_EXPERIMENTAL_CONCESION };
            }

            else if (funciones.retornaModulo().Equals("ExperimentalesAmerb"))
            {
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_EXPERIMENTALES_AMERB;
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesAmerbSession;
                ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ASOCIAR_RESOLUCION_EXPERIMENTAL_AMERB };
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            // PAGE LOAD
            if (!Page.IsPostBack)
            {
                setearModulo();

                ValidationSummaryErrores.ValidationGroup = erroresSumary;



                SolicitudConcesion unidadEspacial = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (unidadEspacial == null || usuario_logeado == null)
                {
                    Response.Redirect(ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"].ToString());
                }

                IdSolicitud.Text = Convert.ToString(unidadEspacial.idSolConcesion);


                //BOTON DE INGRESO O MODIFICACION
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], this.usuario_logeado, unidadEspacial, rbAccion.EDITAR))
                {
                    PanelFormularioIngreso.Visible = true;
                    UpdatePanelFormularioIngreso.Update();
                }
                else
                {
                    PanelFormularioIngreso.Visible = false;
                    UpdatePanelFormularioIngreso.Update();
                }



                CargarListaResoluciones(usuario_logeado, unidadEspacial.idSolConcesion);
                CargarListaRequerimientos(usuario_logeado, Convert.ToInt32(IdSolicitud.Text));


                // Inicializamos el formulario
                Initialize_Form();

            }
        }


        private void CargarListaRequerimientos(Usuario.Serializable usuario_logeado, int IdSolicitud)
        {

            GridRequerimiento.DataSource = requerimientoService.ListarResolucionesDecretos(IdSolicitud, null, new DateTime(), 0, 0);
            GridRequerimiento.DataBind();

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

        }


        protected void Initialize_Comboboxs()
        {

            Carga_Combobox("Resolucion");
            Resolucion.SelectedValue = "0";


        }


        protected void Carga_Combobox(string combobox)
        {

            List<Resolucion> resp = null;


            switch (combobox)
            {


                case "Resolucion":

                    Resolucion.Items.Clear();
                    Resolucion.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    //resp = resolucionService.ListarResolucionesManuales(Convert.ToInt32(IdSolicitud.Text));


                    //if (resp != null)
                    //{
                    //    foreach (Resolucion item in resp)
                    //    {
                    //        Resolucion.Items.Add(new ListItem(item.cadena, Convert.ToString(item.idResolucion)));
                    //    }
                    //}

                    Resolucion.DataBind();

                    break;


            };
        }



        private void CargarListaResoluciones(Usuario.Serializable usuario_logeado, int idSolicitud)
        {

            //List<ResolucionSolicitud>  listaAux   = resolucionService.ListarResolucionSolicitud(idSolicitud);
            //ViewState["ResolucionesGrilla"] = listaAux;
            
            //GridResolucionesManuales.DataSource = listaAux;
            //GridResolucionesManuales.DataBind();


        }



        //GRID REQUERIMIENTO
        protected void GridResolucionesManuales_RowDataBound(object sender, GridViewRowEventArgs e)
        {

  

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                GridView GridRequerimiento = (GridView)sender;
                int count = GridRequerimiento.Rows.Count;


                //Ver
                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.VER))
                    {
                        //boton_ver.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro...?')");
                        boton_ver.Visible = true;
                    }
                };


                //Borrar
                ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
                if (boton_borrar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ELIMINAR))
                    {
                        boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar esta asociación?')");
                        boton_borrar.Visible = true;
                    }
                };

            };
        }


        //GRID REQUERIMIENTO
        protected void GridResolucionesManuales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int idResolucion = Convert.ToInt32(arg[0]);
            int idSolConcesion = Convert.ToInt32(arg[1]);


            ErroresInferior.Text = "";
            PanelErroresInferior.Visible = false;
            UpdatePanelErroresInferior.Update();
            

            switch (e.CommandName)
            {
                case "Ver":
                    Response.Redirect(ViewState["URL_VER"].ToString() + idResolucion);
                    break;

                case "Eliminar":


                    if (!resolucionService.EliminarResolucionSolicitud(idResolucion, idSolConcesion))
                    {
                        ErroresInferior.Text = "Ha ocurrido un error al realizar la accion solicituda";
                        PanelErroresInferior.Visible = true;
                        UpdatePanelErroresInferior.Update();

                    }
                    else
                    {

                        ErroresInferior.Text = "Acción realizada con exito";
                        PanelErroresInferior.Visible = true;
                        UpdatePanelErroresInferior.Update();
                        this.CargarListaResoluciones(usuario_logeado, Convert.ToInt32(IdSolicitud.Text));
                        this.Initialize_Form();
                    }

                    break;
               
              

            };


        }


        //GRID REQUERIMIENTO
        protected void GridResolucionesManuales_RowCreated(object sender, GridViewRowEventArgs e)
        {
          
        }



        protected void Guardar_Click(object sender, EventArgs e)
        {


            ErroresInferior.Text = "";
            PanelErroresInferior.Visible = false;
            UpdatePanelErroresInferior.Update();
    

            if (Page.IsValid)
            {

                ResolucionSolicitud resolucionSolicitud = new ResolucionSolicitud();

                resolucionSolicitud.solicitud = new SolicitudConcesion();
                resolucionSolicitud.solicitud.idSolConcesion = Convert.ToInt32(IdSolicitud.Text);
                resolucionSolicitud.resolucion = new Resolucion();
                resolucionSolicitud.resolucion.idResolucion = Convert.ToInt32(Resolucion.SelectedValue);
                resolucionSolicitud.estadoVigencia = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
                resolucionSolicitud.tipoIngreso = new ParametroGenerico();
                resolucionSolicitud.tipoIngreso.id = rbTipo.TIPO_INTERFAZ_RESOLUCION_ADMIN_RESOLUCIONES;

                List<ResolucionSolicitud> resolucionesGrilla = (List<ResolucionSolicitud>)ViewState["ResolucionesGrilla"];

                List<String> errores = ingresoResolucionValidacion.validaGuardarResolucionSolicitud(resolucionSolicitud, resolucionesGrilla);

                if (errores.Count == 0)
                {

                    //bool resp = resolucionService.GuardarResolucionSolicitud(resolucionSolicitud);
                    bool resp = false;


                    if (resp)
                    {
                        usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];
                        this.CargarListaResoluciones(usuario_logeado, Convert.ToInt32(IdSolicitud.Text));
                        this.Initialize_Form();

                        ErroresInferior.Text = "Se ha guardado exitosamente el documento";
                        PanelErroresInferior.Visible = true;
                        UpdatePanelErroresInferior.Update();
                        UpdatePanelFlujoDocumental.Update();

                        resolucionesGrilla.Add(resolucionSolicitud);

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

            UpdatePanelMensajesValidaciones.Update();
        }



        protected void Resolucion_OnSelectedIndexChanged(object sender, EventArgs e)
        {


            ErroresInferior.Text = "";
            PanelErroresInferior.Visible = false;
            UpdatePanelErroresInferior.Update();
    
        }




        //GRID REQUERIMIENTO
        protected void GridRequerimiento_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false; // Invisibiling idDocGeneral Header Cell
                e.Row.Cells[1].Visible = false; // Invisibiling idPestana Header Cell
                e.Row.Cells[2].Visible = false; // Invisibiling Ambito Header Cell
                e.Row.Cells[3].Visible = false; // Invisibiling Tipo Header Cell
                e.Row.Cells[15].Visible = false; // Invisibiling Evaluación Header Cell
                e.Row.Cells[16].Visible = false; // Invisibiling Evaluación Header Cell


                e.Row.Cells[8].Style["border-left"] = colorPlanilla.RAYA_DIVISORA;


                e.Row.Cells[4].Visible = false; // quitando "salida"
                e.Row.Cells[5].Visible = false; // quitando "salida"
                e.Row.Cells[6].Visible = false; // quitando "salida"
                e.Row.Cells[7].Visible = false; // quitando "salida"
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                GridView GridRequerimiento = (GridView)sender;
                int count = GridRequerimiento.Rows.Count;

                e.Row.Cells[2].Visible = false; // quitando "ambito"
                e.Row.Cells[4].Visible = false; // quitando "salida"
                e.Row.Cells[5].Visible = false; // quitando "salida"
                e.Row.Cells[6].Visible = false; // quitando "salida"
                e.Row.Cells[7].Visible = false; // quitando "salida"



                String rowspan = ((Label)e.Row.FindControl("hidden4")).Text;

                //PRIMERA FILA CON DATOS
                if (count == 0)
                {
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[2].RowSpan = Convert.ToInt32(rowspan);



                    e.Row.Cells[15].Visible = true;
                    e.Row.Cells[15].RowSpan = Convert.ToInt32(rowspan);

                    e.Row.Cells[16].Visible = true;
                    e.Row.Cells[16].RowSpan = Convert.ToInt32(rowspan);


                    e.Row.BackColor = colorPlanilla.COLOR_CELESTE;


                 


                }
                else if (count > 0)
                {


                    GridViewRow previousRow = GridRequerimiento.Rows[e.Row.RowIndex - 1];

                    String idDocGeneralAnterior = ((Label)previousRow.FindControl("hidden1")).Text;
                    String idPestanaAnterior = ((Label)previousRow.FindControl("hidden2")).Text;

                    String idDocGeneral = ((Label)e.Row.FindControl("hidden1")).Text;
                    String idPestana = ((Label)e.Row.FindControl("hidden2")).Text;



                    if (!idDocGeneralAnterior.Equals(idDocGeneral) || !idPestanaAnterior.Equals(idPestana))
                    {

                        e.Row.Cells[2].Visible = false;
                        e.Row.Cells[2].RowSpan = Convert.ToInt32(rowspan);

                        
                        e.Row.Cells[15].Visible = true;
                        e.Row.Cells[15].RowSpan = Convert.ToInt32(rowspan);

                        e.Row.Cells[16].Visible = true;
                        e.Row.Cells[16].RowSpan = Convert.ToInt32(rowspan);



                        if (previousRow.BackColor == colorPlanilla.COLOR_CELESTE)
                        {
                            e.Row.BackColor = colorPlanilla.COLOR_BLANCO;
                        }
                        else
                        {
                            e.Row.BackColor = colorPlanilla.COLOR_CELESTE;
                        }



                        

                    }
                    else
                    {
                        e.Row.Cells[2].RowSpan = 0;
                        e.Row.Cells[2].Visible = false;

                        

                        e.Row.Cells[15].RowSpan = 0;
                        e.Row.Cells[15].Visible = false;

                        e.Row.Cells[16].RowSpan = 0;
                        e.Row.Cells[16].Visible = false;


                        e.Row.BackColor = previousRow.BackColor;
                    }
                }


                e.Row.Cells[0].Visible = false; // Invisibiling idDocGeneral Header Cell
                e.Row.Cells[1].Visible = false; // Invisibiling idPestana Header Cell
                e.Row.Cells[2].Visible = false; // Invisibiling ambito Header Cell
                e.Row.Cells[8].Style["border-left"] = colorPlanilla.RAYA_DIVISORA;


                String refUEDocGeneral = ((Label)e.Row.FindControl("hiddenIdRefUEDocGeneral")).Text;


                //NO ES UNA RESOLUCION IGNRESADA A TRAVES DEL ADMINISTRADOR DE RESOLUCIONES
                if (refUEDocGeneral == null || refUEDocGeneral.Trim().Equals(""))
                {
                    
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
                }

                //Descargar
                String idArchivo = ((Label)e.Row.FindControl("gIDArchivo")).Text;

                ImageButton boton_gDescargar = (ImageButton)e.Row.FindControl("gDescargar");
                if (boton_gDescargar != null)
                {
                    if (idArchivo != null && !idArchivo.Trim().Equals("") && Convert.ToInt32(idArchivo) > 0)
                    {
                        boton_gDescargar.Visible = true;
                    }
                };
                
                //Ver
                String idResolucion = ((Label)e.Row.FindControl("gIDResolucion")).Text;

                ImageButton boton_gVer = (ImageButton)e.Row.FindControl("gVer");
                if (boton_gVer != null)
                {
                    if (idResolucion != null && !idResolucion.Trim().Equals("") && Convert.ToInt32(idResolucion) > 0)
                    {
                        boton_gVer.Visible = true;
                    }
                };
            };

            /*
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                String observacion = ((Label)e.Row.FindControl("gObservacion")).Text;
                e.Row.ToolTip = observacion;
            }
             */
        }



        //GRID REQUERIMIENTO
        protected void GridRequerimiento_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridRequerimiento = (GridView)sender;


                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);


                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = labelDocumentos;
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridRequerimiento.Controls[0].Controls.AddAt(0, HeaderRow);



                // Creating a Row
                HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                ////Adding Ambito Column
                //HeaderCell = new TableCell();
                //HeaderCell.Text = "Materia";
                //HeaderCell.CssClass = "customHeader";
                //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                //HeaderCell.RowSpan = 2;
                //HeaderRow.Cells.Add(HeaderCell);

                //Adding Tipo Column
                HeaderCell = new TableCell();
                HeaderCell.Text = "Materia";
                HeaderCell.CssClass = "customHeader";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.RowSpan = 2;
                HeaderRow.Cells.Add(HeaderCell);


                ////Adding Salida Column
                //HeaderCell = new TableCell();
                //HeaderCell.Text = "Salida";
                //HeaderCell.CssClass = "customHeader";
                //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                //HeaderCell.ColumnSpan = 4;
                //HeaderRow.Cells.Add(HeaderCell);

                //Adding Entrada Column
                HeaderCell = new TableCell();
                HeaderCell.Text = "Entrada";
                HeaderCell.CssClass = "customHeader";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 7;
                HeaderCell.Style["border-left"] = colorPlanilla.RAYA_DIVISORA;
                HeaderRow.Cells.Add(HeaderCell);


                //Adding Evaluación Column
                HeaderCell = new TableCell();
                HeaderCell.Text = "Estado";
                HeaderCell.CssClass = "customHeader";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.RowSpan = 2;
                HeaderRow.Cells.Add(HeaderCell);


                //Adding Evaluación Column
                HeaderCell = new TableCell();
                HeaderCell.Text = "Opciones";
                HeaderCell.CssClass = "customHeader";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.RowSpan = 2;
                HeaderRow.Cells.Add(HeaderCell);


                //Adding the Row at the 0th position (first row) in the Grid
                GridRequerimiento.Controls[0].Controls.AddAt(1, HeaderRow);

            }

           
        }



        //GRID REQUERIMIENTO
        protected void GridRequerimiento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] arg = new string[3];
            arg = e.CommandArgument.ToString().Split(';');

            int idRequerimiento = Convert.ToInt32(arg[0]);
            int idPestana = Convert.ToInt32(arg[1]);
            int idResolucion = 0;
            if (arg[2] != null && !arg[2].Equals(""))
            {
                idResolucion = Convert.ToInt32(arg[2]);
            }

            ErroresInferior.Text = "";
            PanelErroresInferior.Visible = false;
            UpdatePanelErroresInferior.Update();
            int idTipoFlujoDocumental = requerimientoService.ObtieneFlujoDocumentoGeneral(idRequerimiento);

            switch (e.CommandName)
            {
               
                case "Eliminar":

                    if (idTipoFlujoDocumental == rbTipo.ENTRADA)
                    {
                        Requerimiento requerimiento = requerimientoService.ObtenerRespuesta(idRequerimiento);
                        List<String> errores = ingresarDocumentoValidacion.validarEliminacionDeRequerimiento(requerimiento);
                        if (errores.Count == 0)
                        {
                            requerimientoService.EliminarRespuesta(idRequerimiento, idPestana, requerimiento.solicitud.idSolConcesion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                        }
                        else
                        {

                            foreach (String error in errores)
                            {
                                ErroresInferior.Text = ErroresInferior.Text + error + "<br/>";
                            }
                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();

                        }
                    }
                    if (idTipoFlujoDocumental == rbTipo.SALIDA)
                    {
                        Requerimiento requerimiento = requerimientoService.ObtenerRequerimientoSinEstado(idRequerimiento);
                        List<String> errores = ingresarDocumentoValidacion.validarEliminacionDeRequerimiento(requerimiento);
                        if (errores.Count == 0)
                        {
                            requerimientoService.EliminarRequerimiento(idRequerimiento, idPestana, requerimiento.solicitud.idSolConcesion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                        }
                        else
                        {

                            foreach (String error in errores)
                            {
                                ErroresInferior.Text = ErroresInferior.Text + error + "<br/>";
                            }
                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();

                        }
                    }

                    CargarListaRequerimientos(usuario_logeado, Convert.ToInt32(IdSolicitud.Text));
                    break;

                case "DescargarArchivo":

                    Requerimiento reqDescarga= null;

                    if (idTipoFlujoDocumental == rbTipo.ENTRADA)
                    {
                        reqDescarga = requerimientoService.ObtenerRespuesta(idRequerimiento);
                    }

                    if (idTipoFlujoDocumental == rbTipo.SALIDA)
                    {
                        reqDescarga = requerimientoService.ObtenerRequerimientoSinEstado(idRequerimiento);
                    }

                    ArchivoBinario archivoBinario = archivoBinarioSolicitudDA.ObtenerArchivoBinarioSolicitud(Convert.ToInt32(reqDescarga.archivoAdjunto.idArchivo));

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = archivoBinario.formato;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinario.nombreArchivo + "." + archivoBinario.formato);
                    Response.BinaryWrite(archivoBinario.bytes);
                    Response.Flush();
                    Response.End();


                    break;

                case "VerResolucion":

                    Response.Redirect("../../Resoluciones/verResolucion.aspx?idResolucion=" + idResolucion);

                    break;
            };


        }

        protected void ImgAdd_PreRender(object sender, EventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            ScriptManager sc = ScriptManager.GetCurrent(this.Page);
            sc.RegisterPostBackControl(btn);
        }
    }


}