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
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;
using Datos.Entidades.Resolucion;
using LogicaNegocio.cl.subpesca.rb.resolucion;
using Validaciones.cl.subpesca.rb.resolucion;
using LogicaNegocio.cl.subpesca.rb.servicios.resoluciones;

namespace SubPesca.Resoluciones
{
    public partial class verResolucion : System.Web.UI.Page
    {

        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema


        PestanaDA pestanaDA = new PestanaDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        IngresarDocumentoValidacion ingresarDocumentoValidacion = new IngresarDocumentoValidacion();
        ResolucionDA resolucionDA = new ResolucionDA();
        RequerimientoService requerimientoService = new RequerimientoService();
        PermisosService permisosService = new PermisosService();
        RegionDA regionDA = new RegionDA();
        ParametroGenericoDA parametroDa = new ParametroGenericoDA();
        IngresoResolucionValidacion ingresoResolucionValidacion = new IngresoResolucionValidacion();
        ResolucionService resolucionService = new ResolucionService();
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();

        Funciones funciones = new Funciones();
        EnviarCorreo enviarCorreo = new EnviarCorreo();

        String erroresSumary = "ValidationSummaryIngresoResolucion";
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

            ValidacionDocumentacion validacionDocumentacion = new ValidacionDocumentacion();

            ViewState["URL_VER"] = paginas.URL_VER_SOLCONCESION;
            ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLCONCESION;
            ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLCONCESION;
            ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLCONCESION;
            ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLCONCESION;
            ViewState["solicitudSession"] = paginas.solicitudConcesionSession;

            validacionDocumentacion.aplicaConcesion = 1;
            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.INGRESO_RESOLUCION };
            ViewState["validacionDocumentacion"] = validacionDocumentacion;

        }


        protected void Page_Load(object sender, EventArgs e)
        {

            // PAGE LOAD
            if (!Page.IsPostBack)
            {

                string Lang = "es-CL";//set your culture here
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Lang);


                setearModulo();

                ValidationSummaryErrores.ValidationGroup = erroresSumary;


                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (usuario_logeado == null)
                {
                    Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
                }


                //BOTON DE INGRESO O MODIFICACION
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], this.usuario_logeado, null, rbAccion.EDITAR))
                {
                    //PanelFormularioIngreso.Visible = true;
                    //UpdatePanelFormularioIngreso.Update();
                }
                else
                {
                    //PanelFormularioIngreso.Visible = false;
                    //UpdatePanelFormularioIngreso.Update();
                }




                ResolucionValidacion resolucionValidacion = new ResolucionValidacion();
                ViewState["HashCampos"] = resolucionDA.ListarResolucionValidacion(resolucionValidacion);


                // Inicializamos el formulario
                Initialize_Form();


                //SE TRATA DE UNA MODIFICACION
                if (Request.QueryString["idResolucion"] != null && Convert.ToInt32(Request.QueryString["idResolucion"]) > 0)
                {

                    Resolucion resolucionFiltro = new Resolucion();
                    resolucionFiltro.idResolucion = Convert.ToInt32(Request.QueryString["idResolucion"]);

                    Resolucion resolucionBD = resolucionService.ObtenerResolucion(resolucionFiltro);

                    if (resolucionBD != null)
                    {
                        this.cargarResolucion(resolucionBD);
                    }


                    Titulo.Text = "Ver Resolución";

                }
                else
                {

                    Titulo.Text = "Ingreso de Resolución";

                }

                //BLOQUEAR Y OCULTAR FORMULARIOS

                TipoDocumento.Enabled = false;
                TipoIngresoResolucion.Enabled = false;
                TipoDocumentoPrincipal.Enabled = false;
                TipoRelacionDocumento.Enabled = false;
                Origen.Enabled = false;
                OrigenPrincipal.Enabled = false;
                Materia.Enabled = false;
                Resultado.Enabled = false;
                DocumentoPrincipal.Enabled = false;
                Numero.ReadOnly = true;
                NumeroPrincipal.ReadOnly = true;
                Fecha.ReadOnly = true;
                FechaPrincipal.ReadOnly = true;
                PanelCalendarioFecha.Visible = false;
                NumeroCI.ReadOnly = true;
                FechaCI.ReadOnly = true;
                NroDiarioOficial.ReadOnly = true;
                FechaDiarioOficial.ReadOnly = true;
                PanelCalendarioFechaDiarioOficial.Visible = false;
                Vigencia.Enabled = false;
                FechaInicioPlazo.Enabled = false;
                PanelCalendarioFechaInicio.Visible = false;
                FechaVencimiento.ReadOnly = true;
                PanelCalendarioFechaVencimiento.Visible = false;
                NuevaFecha.ReadOnly = true;
                PanelCalendarioNuevaFecha.Visible = false;

                SinReferencia.Enabled = false;
                ConReferencia.Enabled = false;
                    

                UpdatePanelUE.Visible = false;
                Button1.Visible = false;

                UpdatePanelDocumentos.Visible = false;
                GuardarReferencia.Visible = false;

                UpdatePanelUbicacion.Visible = false;
                Button2.Visible = false;

                UpdatePanelEspecie.Visible = false;
                Button3.Visible = false;

                UpdatePanelTitular.Visible = false;
                Button4.Visible = false;

                Observaciones.ReadOnly = true;
                //trBotonArchivo.Visible = false;

            }


            /*
            string script = "calendario('" + Fecha.ClientID + "','" + fechaImgDinamica.ClientID + "');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + Fecha.ClientID, script.ToString(), true);

            script = "calendario('" + FechaCI.ClientID + "','" + fechaCIImgDinamica.ClientID + "');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaCI.ClientID, script.ToString(), true);

            script = "calendario('" + FechaDiarioOficial.ClientID + "','" + fechaDiarioOficialImgDinamica.ClientID + "');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaDiarioOficial.ClientID, script.ToString(), true);

            script = "calendario('" + FechaInicioPlazo.ClientID + "','" + fechaInicioPlazoImgDinamica.ClientID + "');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaInicioPlazo.ClientID, script.ToString(), true);

            script = "calendario('" + FechaVencimiento.ClientID + "','" + fechaVencimientoImgDinamica.ClientID + "');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaVencimiento.ClientID, script.ToString(), true);

            script = "calendario('" + NuevaFecha.ClientID + "','" + nuevaFechaImgDinamica.ClientID + "');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + NuevaFecha.ClientID, script.ToString(), true);

            script = "calendario('" + FechaReferencia.ClientID + "','" + fechaReferenciaImgDinamica.ClientID + "');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaReferencia.ClientID, script.ToString(), true);
             */


        }


        protected void Initialize_Form()
        {
            // Cargamos los combobox
            Initialize_Comboboxs();

        }


        protected void Initialize_Comboboxs()
        {

            Carga_Combobox("TipoDocumento");
            TipoDocumento.SelectedValue = "0";

            Carga_Combobox("TipoDocumentoPrincipal");
            TipoDocumentoPrincipal.SelectedValue = "0";

            Carga_Combobox("TipoRelacionDocumento");
            TipoRelacionDocumento.SelectedValue = "0";

            Carga_Combobox("Origen");
            Origen.SelectedValue = "0";

            Carga_Combobox("OrigenPrincipal");
            OrigenPrincipal.SelectedValue = "0";

            Carga_Combobox("Resultado");
            Resultado.SelectedValue = "0";

            Carga_Combobox("Vigencia");
            Vigencia.SelectedValue = "0";

            Carga_Combobox("Materia");
            Materia.SelectedValue = "0";

            Carga_Combobox("Tipo");
            Tipo.SelectedValue = "0";

            Carga_Combobox("OrigenReferencia");
            OrigenReferencia.SelectedValue = "0";

            Carga_Combobox("TipoUnidadEspacial");
            TipoUnidadEspacial.SelectedValue = "0";

            Carga_Combobox("TipoSolicitud");
            TipoSolicitud.SelectedValue = "0";

            Carga_Combobox("Region");
            Region.SelectedValue = "0";

            Carga_Combobox("Comuna");
            Comuna.SelectedValue = "0";

            Carga_Combobox("Especie");
            Especie.SelectedValue = "0";

            Carga_Combobox("TipoIngresoResolucion");
            TipoIngresoResolucion.SelectedValue = "0";


        }


        protected void Carga_Combobox(string combobox)
        {

            Hashtable campos = (Hashtable)ViewState["HashCampos"]; ;

            switch (combobox)
            {


                case "TipoDocumento":

                    TipoDocumento.Items.Clear();
                    if (campos != null)
                    {
                        foreach (DictionaryEntry item in campos)
                        {
                            TipoDocumento.Items.Add(new ListItem(Convert.ToString(((Combobox)item.Value).nombreAtributo), Convert.ToString(item.Key)));
                        }
                    }
                    TipoDocumento.DataBind();
                    TipoDocumento.Items.Insert(0, new ListItem("-- Seleccione --", "0"));


                    break;


                case "TipoDocumentoPrincipal":

                    TipoDocumentoPrincipal.Items.Clear();
                    if (campos != null)
                    {
                        foreach (DictionaryEntry item in campos)
                        {
                            TipoDocumentoPrincipal.Items.Add(new ListItem(Convert.ToString(((Combobox)item.Value).nombreAtributo), Convert.ToString(item.Key)));
                        }
                    }
                    TipoDocumentoPrincipal.DataBind();
                    TipoDocumentoPrincipal.Items.Insert(0, new ListItem("-- Seleccione --", "0"));


                    break;

                case "TipoRelacionDocumento":

                    TipoRelacionDocumento.Items.Clear();
                    TipoRelacionDocumento.Items.Add(new ListItem("Principal", rbTipo.RESOLUCION_PRINCIPAL.ToString()));
                    TipoRelacionDocumento.Items.Add(new ListItem("Complementario", rbTipo.RESOLUCION_COMPLEMENTARIA.ToString()));
                    TipoRelacionDocumento.DataBind();
                    TipoRelacionDocumento.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;


                case "Origen":


                    Origen.Items.Clear();
                    Origen.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    if (Convert.ToInt32(TipoDocumento.SelectedValue) > 0)
                    {

                        Hashtable nodoOrigen = ((Combobox)(campos[Convert.ToInt32(TipoDocumento.SelectedValue)])).hash;


                        if (nodoOrigen != null)
                        {
                            foreach (DictionaryEntry item in nodoOrigen)
                            {
                                Origen.Items.Add(new ListItem(Convert.ToString(((Combobox)item.Value).nombreAtributo), Convert.ToString(item.Key)));
                            }
                        }

                    }

                    Origen.DataBind();
                    UpdatePanelOrigen.Update();

                    /*
                    Origen.Items.Clear();
                    Origen.Items.Add(new ListItem("Subsecretaria de Pesca", "1"));
                    Origen.Items.Add(new ListItem("Subsecretaria de Marina", "2"));
                    Origen.Items.Add(new ListItem("Subsecretaria para las Fuerzas Armada", "3"));
                    Origen.Items.Add(new ListItem("Superintendencia del Medio Ambiente", "4"));
                    Origen.Items.Add(new ListItem("Servicio de Evaluación Ambiental", "5"));
                    Origen.Items.Add(new ListItem("Servicio de Evaluación Ambiental Regional", "6"));
                    Origen.Items.Add(new ListItem("Servicio Nacional de Pesca", "7"));
                    Origen.DataBind();
                    Origen.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    */

                    break;


                case "OrigenPrincipal":

                    OrigenPrincipal.Items.Clear();
                    OrigenPrincipal.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    if (Convert.ToInt32(TipoDocumentoPrincipal.SelectedValue) > 0)
                    {

                        Hashtable nodoOrigen = ((Combobox)(campos[Convert.ToInt32(TipoDocumentoPrincipal.SelectedValue)])).hash;


                        if (nodoOrigen != null)
                        {
                            foreach (DictionaryEntry item in nodoOrigen)
                            {
                                OrigenPrincipal.Items.Add(new ListItem(Convert.ToString(((Combobox)item.Value).nombreAtributo), Convert.ToString(item.Key)));
                            }
                        }

                    }

                    OrigenPrincipal.DataBind();
                    UpdatePanelOrigenPrincipal.Update();

                    /*
                    Origen.Items.Clear();
                    Origen.Items.Add(new ListItem("Subsecretaria de Pesca", "1"));
                    Origen.Items.Add(new ListItem("Subsecretaria de Marina", "2"));
                    Origen.Items.Add(new ListItem("Subsecretaria para las Fuerzas Armada", "3"));
                    Origen.Items.Add(new ListItem("Superintendencia del Medio Ambiente", "4"));
                    Origen.Items.Add(new ListItem("Servicio de Evaluación Ambiental", "5"));
                    Origen.Items.Add(new ListItem("Servicio de Evaluación Ambiental Regional", "6"));
                    Origen.Items.Add(new ListItem("Servicio Nacional de Pesca", "7"));
                    Origen.DataBind();
                    Origen.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    */

                    break;


                case "Materia":

                    Materia.Items.Clear();
                    Materia.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    if (Convert.ToInt32(TipoDocumento.SelectedValue) > 0 && Convert.ToInt32(Origen.SelectedValue) > 0)
                    {

                        Hashtable nodoOrigen = ((Combobox)(campos[Convert.ToInt32(TipoDocumento.SelectedValue)])).hash;
                        Hashtable nodoMateria = ((Combobox)(nodoOrigen[Convert.ToInt32(Origen.SelectedValue)])).hash;


                        if (nodoMateria != null)
                        {
                            foreach (DictionaryEntry item in nodoMateria)
                            {
                                Materia.Items.Add(new ListItem(Convert.ToString(((Combobox)item.Value).nombreAtributo), Convert.ToString(item.Key)));
                            }
                        }
                    }

                    Materia.DataBind();

                    UpdatePanelMateria.Update();


                    /*
                    Materia.Items.Clear();
                    Materia.DataBind();
                    Materia.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    */

                    break;




                case "Resultado":

                    Resultado.Items.Clear();
                    Resultado.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    if (Convert.ToInt32(TipoDocumento.SelectedValue) > 0 && Convert.ToInt32(Origen.SelectedValue) > 0 && Convert.ToInt32(Materia.SelectedValue) > 0)
                    {

                        Hashtable nodoOrigen = ((Combobox)(campos[Convert.ToInt32(TipoDocumento.SelectedValue)])).hash;
                        Hashtable nodoMateria = ((Combobox)(nodoOrigen[Convert.ToInt32(Origen.SelectedValue)])).hash;
                        Hashtable nodoResultado = ((Combobox)(nodoMateria[Convert.ToInt32(Materia.SelectedValue)])).hash;

                        if (nodoResultado != null)
                        {
                            foreach (DictionaryEntry item in nodoResultado)
                            {
                                Resultado.Items.Add(new ListItem(Convert.ToString(((ResolucionValidacion)item.Value).estadoMateria.descripcion), Convert.ToString(item.Key)));
                            }
                        }
                    }

                    Resultado.DataBind();


                    if (Resultado.Items.Count > 1)
                    {
                        PanelResultado.Visible = true;
                    }
                    else
                    {
                        PanelResultado.Visible = false;
                    }

                    UpdatePanelResultado.Update();



                    /*
                     Resultado.Items.Clear();
                     Resultado.Items.Add(new ListItem("Aprobado", "1"));
                     Resultado.Items.Add(new ListItem("Rechazado", "2"));
                     Resultado.Items.Add(new ListItem("Remisión", "3"));
                     Resultado.DataBind();
                     Resultado.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                     */

                    break;


                case "Tipo":

                    Tipo.Items.Clear();
                    Tipo.Items.Add(new ListItem("Solicitud", Convert.ToString(rbTipo.RESOLUCION_SUB_REFERENCIA_SOLICITUD)));
                    Tipo.Items.Add(new ListItem("Unidades Espaciales", Convert.ToString(rbTipo.RESOLUCION_SUB_REFERENCIA_UE)));
                    Tipo.DataBind();
                    Tipo.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;



                case "Vigencia":

                    Vigencia.Items.Clear();
                    Vigencia.Items.Add(new ListItem("Vigente", "6"));
                    Vigencia.Items.Add(new ListItem("No Vigente", "7"));
                    Vigencia.DataBind();
                    Vigencia.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;




                case "OrigenReferencia":

                    OrigenReferencia.Items.Clear();
                    //OrigenReferencia.Items.Add(new ListItem("Subsecretaria de Pesca", "1"));
                    //OrigenReferencia.Items.Add(new ListItem("Subsecretaria de Marina", "2"));
                    //OrigenReferencia.Items.Add(new ListItem("Subsecretaria para las Fuerzas Armada", "3"));
                    //OrigenReferencia.Items.Add(new ListItem("Superintendencia del Medio Ambiente", "4"));
                    //OrigenReferencia.Items.Add(new ListItem("Servicio de Evaluación Ambiental", "5"));
                    //OrigenReferencia.Items.Add(new ListItem("Servicio de Evaluación Ambiental Regional", "6"));
                    //OrigenReferencia.Items.Add(new ListItem("Servicio Nacional de Pesca", "7"));
                    OrigenReferencia.DataSource = parametroDa.ListarTiposGenerico("paSelRbTipo", "@grupo", "ORIGEN_RESOLUCION", "idTipo", "nombreTipo");
                    OrigenReferencia.DataTextField = "descripcion";
                    OrigenReferencia.DataValueField = "id";
                    OrigenReferencia.DataBind();
                    OrigenReferencia.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;


                case "TipoUnidadEspacial":

                    TipoUnidadEspacial.Items.Clear();
                    TipoUnidadEspacial.Items.Add(new ListItem("Concesión de Acuicultura", "36"));
                    TipoUnidadEspacial.Items.Add(new ListItem("Centro de Faenamiento", "37"));
                    TipoUnidadEspacial.Items.Add(new ListItem("Centro de Acopio", "118"));
                    TipoUnidadEspacial.Items.Add(new ListItem("Acuicultura en Amerb", "119"));
                    TipoUnidadEspacial.Items.Add(new ListItem("Colectores de Semilla", "120"));
                    TipoUnidadEspacial.Items.Add(new ListItem("Experimentales AMERB", "532"));
                    TipoUnidadEspacial.Items.Add(new ListItem("Experimentales Concesión", "533"));
                    TipoUnidadEspacial.Items.Add(new ListItem("Acuicultura en ECMPO", "534"));
                    TipoUnidadEspacial.DataBind();
                    TipoUnidadEspacial.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                case "TipoSolicitud":

                    TipoSolicitud.Items.Clear();
                    TipoSolicitud.Items.Add(new ListItem("Trámite de Solicitud Concesión de Acuicultura", "88"));
                    TipoSolicitud.Items.Add(new ListItem("Trámite de Modificación Concesión de Acuicultura", "89"));
                    TipoSolicitud.Items.Add(new ListItem("Trámite Sector Relocalización", "95"));
                    TipoSolicitud.Items.Add(new ListItem("Tramite de solicitud Centro de Acopio", "121"));
                    TipoSolicitud.Items.Add(new ListItem("Tramite de solicitud Acuicultura en Amerb", "122"));
                    TipoSolicitud.Items.Add(new ListItem("Tramite de solicitud Colectores de Semilla", "123"));
                    TipoSolicitud.Items.Add(new ListItem("Tramite de solicitud Centro de Faenamiento", "124"));
                    TipoSolicitud.Items.Add(new ListItem("Tramite de solicitud Experimentales AMERB", "535"));
                    TipoSolicitud.Items.Add(new ListItem("Tramite de solicitud Experimentales Concesión", "536"));
                    TipoSolicitud.Items.Add(new ListItem("Tramite de solicitud Acuicultura en ECMPO", "537"));
                    TipoSolicitud.Items.Add(new ListItem("Trámite de Modificación Centro de Faenamiento", "546"));
                    TipoSolicitud.Items.Add(new ListItem("Trámite de Modificación Centro de Acopio", "552"));
                    TipoSolicitud.Items.Add(new ListItem("Trámite de Modificación AMERB", "558"));
                    TipoSolicitud.Items.Add(new ListItem("Trámite de Modificación ECMPO", "564"));
                    TipoSolicitud.DataBind();
                    TipoSolicitud.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                case "Region":

                    Region.Items.Clear();
                    Region.DataSource = regionDA.ListarRegion(0);
                    Region.DataTextField = "Region";
                    Region.DataValueField = "IdRegion";
                    Region.DataBind();
                    Region.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                case "Comuna":

                    Comuna.Items.Clear();
                    Comuna.DataBind();
                    Comuna.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;


                case "Especie":

                    Especie.Items.Clear();
                    Especie.DataSource = parametroDa.ListarEspecies(0, "", 0);
                    Especie.DataTextField = "descripcion";
                    Especie.DataValueField = "id";
                    Especie.DataBind();
                    Especie.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;


                case "TipoIngresoResolucion":

                    TipoIngresoResolucion.Items.Clear();
                    TipoIngresoResolucion.DataSource = parametroDa.ListarTiposGenerico("paSelRbTipo", "@grupo", "TIPO_INGRESO_RESOL", "idTipo", "nombreTipo");
                    TipoIngresoResolucion.DataTextField = "descripcion";
                    TipoIngresoResolucion.DataValueField = "id";
                    TipoIngresoResolucion.DataBind();
                    TipoIngresoResolucion.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

            };
        }


        protected void Tipo_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            TipoUnidadEspacial.SelectedValue = "0";
            TipoSolicitud.SelectedValue = "0";


            if (Convert.ToInt32(Tipo.SelectedValue) == rbTipo.RESOLUCION_SUB_REFERENCIA_SOLICITUD)
            {

                PanelTipoSolicitud.Visible = true;
                UpdatePanelTipoSolicitud.Update();


                PanelTipoUnidadEspacial.Visible = false;
                UpdatePanelTipoUnidadEspacial.Update();

            }
            else if (Convert.ToInt32(Tipo.SelectedValue) == rbTipo.RESOLUCION_SUB_REFERENCIA_UE)
            {

                PanelTipoSolicitud.Visible = false;
                UpdatePanelTipoSolicitud.Update();

                PanelTipoUnidadEspacial.Visible = true;
                UpdatePanelTipoUnidadEspacial.Update();

            }
            else
            {

                PanelTipoSolicitud.Visible = false;
                UpdatePanelTipoSolicitud.Update();

                PanelTipoUnidadEspacial.Visible = false;
                UpdatePanelTipoUnidadEspacial.Update();

            }
        }


        protected void Region_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            Comuna.Items.Clear();
            if (Convert.ToInt32(Region.SelectedValue) > 0)
            {
                Comuna.DataSource = parametroGenericoDA.ListarComunas(Convert.ToInt32(Region.SelectedValue));
                Comuna.DataTextField = "descripcion";
                Comuna.DataValueField = "id";
                Comuna.DataBind();
            };
            Comuna.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            UpdatePanelComuna.Update();

        }


        protected void TipoDocumento_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            this.Carga_Combobox("Origen");
            Origen.SelectedValue = "0";

        }

        protected void TipoDocumentoPrincipal_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            this.Carga_Combobox("OrigenPrincipal");
            OrigenPrincipal.SelectedValue = "0";

        }

        protected void Origen_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            this.Carga_Combobox("Materia");
            Materia.SelectedValue = "0";

        }


        protected void Materia_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            this.Carga_Combobox("Resultado");
            Resultado.SelectedValue = "0";

            //this.DocumentoComplementario(null, null);
        }

        protected void TipoRelacionDocumento_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PanelDocumentoPrincipal.Visible = false;

            if (Convert.ToInt32(TipoRelacionDocumento.SelectedValue) == rbTipo.RESOLUCION_COMPLEMENTARIA)
            {
                /*
                DocumentoPrincipal.Items.Clear();
                DocumentoPrincipal.DataBind();
                DocumentoPrincipal.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                PanelDocumentoPrincipal.Visible = true;
                 * */

            }

            UpdatePanelDocumentoPrincipal.Update();



            PanelResolucionPrincipal.Visible = false;
            if (Convert.ToInt32(TipoRelacionDocumento.SelectedValue) == rbTipo.RESOLUCION_COMPLEMENTARIA)
            {
                PanelResolucionPrincipal.Visible = true;
            }
            UpdatePanelResolucionPrincipal.Update();

        }



        //DETERMINA QUE FORMULARIO DESPLIEGA
        protected void RadGrupoEspecieChecked(object sender, EventArgs e)
        {

            if (SinReferencia.Checked == true)
            {
                PanelFormularioReferencia.Visible = false;
                UpdatePanelFormularioReferencia.Update();


                PanelFormularioUnidadEspacial.Visible = false;
                UpdatePanelFormularioUnidadEspacial.Update();

            }
            else if (ConReferencia.Checked == true)
            {

                PanelFormularioReferencia.Visible = true;
                UpdatePanelFormularioReferencia.Update();


                PanelFormularioUnidadEspacial.Visible = true;
                UpdatePanelFormularioUnidadEspacial.Update();

            }

        }


        //MUESTRA/OCULTA CAMPOS EN BASE AL TEMA, APLICA PARA (SALIDA - INFORMATIVA, ENTRADA - INGRESO SIN REQUERIMIENTO)
        private void controlarCamposPorTema()
        {

            if (true)
            {

                Hashtable camposObligatorios = null;
                Hashtable hashIdTipoIO = null;
                Hashtable hashIdTipoOrigenDestinatario = null;
                Hashtable hashIdPestana = null;
                Hashtable hashIdTipoDocumento = null;
                ValidacionDocumentacion validacionDocumentacion = null;


                camposObligatorios = (Hashtable)ViewState["HashCampos"];



                if (hashIdTipoIO != null)
                {
                    hashIdTipoOrigenDestinatario = (Hashtable)hashIdTipoIO[Convert.ToInt32(Origen.SelectedValue)];
                }

                if (hashIdTipoOrigenDestinatario != null)
                {
                    hashIdPestana = (Hashtable)hashIdTipoOrigenDestinatario[Convert.ToInt32(0)];
                }


                if (hashIdPestana != null)
                {
                    hashIdTipoDocumento = (Hashtable)hashIdPestana[Convert.ToInt32(TipoDocumento.SelectedValue)];
                }


                validacionDocumentacion = null;



                if (validacionDocumentacion != null)
                {

                    //NUMERO
                    if (validacionDocumentacion.numero == obligatoriedadCampo.noAplica)
                    {
                        PanelNumero.Visible = false;
                    }
                    else
                    {
                        PanelNumero.Visible = true;
                        if (validacionDocumentacion.numero == obligatoriedadCampo.obligatorio)
                        {
                            RequeridoNumero.Text = "*";
                        }
                        else
                        {
                            RequeridoNumero.Text = "";
                        }
                    }

                    //FECHA
                    if (validacionDocumentacion.fecha == obligatoriedadCampo.noAplica)
                    {
                        PanelFecha.Visible = false;
                    }
                    else
                    {
                        PanelFecha.Visible = true;
                        //string script = "calendario('" + Fecha.ClientID + "','" + fechaImgDinamica.ClientID + "');";
                        //ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + Fecha.ClientID, script.ToString(), true);
                        if (validacionDocumentacion.fecha == obligatoriedadCampo.obligatorio)
                        {
                            RequeridoFecha.Text = "*";
                        }
                        else
                        {
                            RequeridoFecha.Text = "";
                        }
                    }


                    //NUEVA FECHA
                    if (validacionDocumentacion.nuevaFecha == obligatoriedadCampo.noAplica)
                    {
                        PanelNuevaFecha.Visible = false;
                    }
                    else
                    {
                        PanelNuevaFecha.Visible = true;
                        //string script = "calendario('" + NuevaFecha.ClientID + "','" + nuevaFechaImgDinamica.ClientID + "');";
                        //ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + NuevaFecha.ClientID, script.ToString(), true);

                    }

                    //NUMEROCI
                    if (validacionDocumentacion.numeroCI == obligatoriedadCampo.noAplica)
                    {
                        PanelNumeroCI.Visible = false;
                    }
                    else
                    {
                        PanelNumeroCI.Visible = true;
                        if (validacionDocumentacion.numeroCI == obligatoriedadCampo.obligatorio)
                        {
                            RequeridoNumeroCI.Text = "*";
                        }
                        else
                        {
                            RequeridoNumeroCI.Text = "";
                        }
                    }


                    //FECHACI
                    if (validacionDocumentacion.fechaCI == obligatoriedadCampo.noAplica)
                    {
                        PanelFechaCI.Visible = false;
                    }
                    else
                    {
                        PanelFechaCI.Visible = true;
                        //string script = "calendarioCI('" + FechaCI.ClientID + "','" + fechaCIImgDinamica.ClientID + "','" + NumeroCI.ClientID + "')";
                        //ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + FechaCI.ClientID, script.ToString(), true);
                        
                    }


                    //PANEL ARCHIVO
                    if (validacionDocumentacion.archivoBinario == obligatoriedadCampo.noAplica)
                    {
                        PanelArchivo.Visible = false;
                    }
                    else
                    {
                        PanelArchivo.Visible = true;
                        if (validacionDocumentacion.archivoBinario == obligatoriedadCampo.obligatorio)
                        {
                            //RequeridoArchivoAdjunto.Text = "*";
                        }
                        else
                        {
                            //RequeridoArchivoAdjunto.Text = "";
                        }
                    }
                }
            }
            else
            {
                PanelNumero.Visible = false;
                PanelFecha.Visible = false;
                PanelNuevaFecha.Visible = false;
                PanelNumeroCI.Visible = false;
                PanelFechaCI.Visible = false;
                PanelArchivo.Visible = false;
            }


            UpdatePanelNumero.Update();
            UpdatePanelFecha.Update();
            UpdatePanelNuevaFecha.Update();
            UpdatePanelNumeroCI.Update();
            UpdatePanelFechaCI.Update();
            UpdatePanelArchivo.Update();

        }




        protected void LimpiarPorTipoDocumento()
        {


            Numero.Text = "";
            Fecha.Text = "";
            NuevaFecha.Text = "";
            NumeroCI.Text = "";
            NumeroCIMensaje.Text = "";
            FechaCI.Text = "";
            Resultado.SelectedValue = "0";



            PanelNumero.Visible = false;
            PanelFecha.Visible = false;
            PanelNuevaFecha.Visible = false;
            PanelNumeroCI.Visible = false;
            PanelFechaCI.Visible = false;
            PanelResultado.Visible = false;
            PanelArchivo.Visible = false;

            ErroresSuperior.Text = "";
            PanelErroresSuperior.Visible = false;
            UpdatePanelErroresSuperior.Update();

            ErroresInferior.Text = "";
            PanelErroresInferior.Visible = false;
            UpdatePanelErroresInferior.Update();


            UpdatePanelNumero.Update();
            UpdatePanelFecha.Update();
            UpdatePanelNuevaFecha.Update();
            UpdatePanelNumeroCI.Update();
            UpdatePanelFechaCI.Update();
            UpdatePanelResultado.Update();
            UpdatePanelArchivo.Update();

        }



        protected void TipoSolicitud_SelectedIndexChanged(object sender, EventArgs e)
        {

            CodigoCentro.Text = "";
            NumeroPert.Text = "";
            NumeroIdentificador.Text = "";
            NumeroSector.Text = "";
            titularesCAD.Text = "";
            regionCad.Text = "";
            toponimioCad.Text = "";

            if (Convert.ToInt32(TipoSolicitud.SelectedValue) == rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA) //COLECTORES
            {

                PanelCodigoCentro.Visible = false;
                UpdatePanelCodigoCentro.Update();

                PanelNumeroPert.Visible = false;
                UpdatePanelNumeroPert.Update();

                PanelNumeroSector.Visible = false;
                UpdatePanelNumeroSector.Update();

                PanelNumeroIdentificador.Visible = true;
                UpdatePanelNumeroIdentificador.Update();

                PanelDatosCentro.Visible = true;
                UpdatePanelDatosCentro.Update();

            }

            else if (Convert.ToInt32(TipoSolicitud.SelectedValue) == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION) //Sector Relocalizacion
            {
                PanelCodigoCentro.Visible = false;
                UpdatePanelCodigoCentro.Update();

                PanelNumeroPert.Visible = true;
                UpdatePanelNumeroPert.Update();

                PanelNumeroSector.Visible = true;
                UpdatePanelNumeroSector.Update();

                PanelNumeroIdentificador.Visible = false;
                UpdatePanelNumeroIdentificador.Update();

                PanelDatosCentro.Visible = true;
                UpdatePanelDatosCentro.Update();

            }
            else if (Convert.ToInt32(TipoSolicitud.SelectedValue) > 0)
            {
                PanelCodigoCentro.Visible = false;
                UpdatePanelCodigoCentro.Update();

                PanelNumeroPert.Visible = true;
                UpdatePanelNumeroPert.Update();

                PanelNumeroSector.Visible = false;
                UpdatePanelNumeroSector.Update();

                PanelNumeroIdentificador.Visible = false;
                UpdatePanelNumeroIdentificador.Update();

                PanelDatosCentro.Visible = true;
                UpdatePanelDatosCentro.Update();

            }
            else
            {

                PanelCodigoCentro.Visible = false;
                UpdatePanelCodigoCentro.Update();

                PanelNumeroPert.Visible = false;
                UpdatePanelNumeroPert.Update();

                PanelNumeroSector.Visible = false;
                UpdatePanelNumeroSector.Update();

                PanelNumeroIdentificador.Visible = false;
                UpdatePanelNumeroIdentificador.Update();

                PanelDatosCentro.Visible = false;
                UpdatePanelDatosCentro.Update();

            }


        }



        protected void TipoUnidadEspacial_SelectedIndexChanged(object sender, EventArgs e)
        {


            CodigoCentro.Text = "";
            NumeroPert.Text = "";
            NumeroIdentificador.Text = "";
            NumeroSector.Text = "";
            titularesCAD.Text = "";
            regionCad.Text = "";
            toponimioCad.Text = "";


            if (Convert.ToInt32(TipoUnidadEspacial.SelectedValue) == 120) //COLECTORES
            {

                PanelCodigoCentro.Visible = false;
                UpdatePanelCodigoCentro.Update();

                PanelNumeroPert.Visible = false;
                UpdatePanelNumeroPert.Update();

                PanelNumeroSector.Visible = false;
                UpdatePanelNumeroSector.Update();

                PanelNumeroIdentificador.Visible = true;
                UpdatePanelNumeroIdentificador.Update();

                PanelDatosCentro.Visible = true;
                UpdatePanelDatosCentro.Update();

            }
            else if (Convert.ToInt32(TipoUnidadEspacial.SelectedValue) > 0)
            {
                PanelCodigoCentro.Visible = true;
                UpdatePanelCodigoCentro.Update();

                PanelNumeroPert.Visible = false;
                UpdatePanelNumeroPert.Update();

                PanelNumeroSector.Visible = false;
                UpdatePanelNumeroSector.Update();

                PanelNumeroIdentificador.Visible = false;
                UpdatePanelNumeroIdentificador.Update();

                PanelDatosCentro.Visible = true;
                UpdatePanelDatosCentro.Update();

            }
            else
            {

                PanelCodigoCentro.Visible = false;
                UpdatePanelCodigoCentro.Update();

                PanelNumeroPert.Visible = false;
                UpdatePanelNumeroPert.Update();

                PanelNumeroSector.Visible = false;
                UpdatePanelNumeroSector.Update();

                PanelNumeroIdentificador.Visible = false;
                UpdatePanelNumeroIdentificador.Update();

                PanelDatosCentro.Visible = true;
                UpdatePanelDatosCentro.Update();

            }


        }



        protected void BuscarSolicitante_Click(object sender, EventArgs e)
        {
            Content_msgGrilla.Visible = false;

            ccNumCustVal.Validate();
            if (Page.IsValid)
            {
                SolicitanteService solicitanteService = new SolicitanteService();
                Solicitante solicitante = new Solicitante();


                String rutCompleto = Convert.ToString(RutPersona.Text);
                String[] rutPartes = rutCompleto.Split('-');


                try
                {

                    solicitante.rut = Convert.ToInt32(rutPartes[0]);


                    solicitante = solicitanteService.VerPersona(solicitante.rut, 0);

                    if (solicitante != null && solicitante.rut > 0)
                    {

                        if (solicitante.tipoPersona != null && solicitante.tipoPersona.id == rbTipo.PERSONA_NATURAL)
                        {
                            NombreSolicitanteNatural.Text = solicitante.nombreSolicitante;
                            if (solicitante.genero)
                            {
                                Genero.Text = "Femenino";
                            }
                            else
                            {
                                Genero.Text = "Masculino";
                            }
                            PanelPersonaJuridica.Visible = false;
                            PanelDatosPersonaNatural.Visible = true;
                        }


                        else if (solicitante.tipoPersona != null && solicitante.tipoPersona.id == rbTipo.PERSONA_JURIDICA)
                        {
                            SubtipoPersonaJuridica.Text = solicitante.subtipoPersona.descripcion;
                            NombreSolicitanteJuridico.Text = solicitante.nombreSolicitante;
                            PanelPersonaJuridica.Visible = true;
                            PanelDatosPersonaNatural.Visible = false;
                        }
                        else
                        {
                            PanelPersonaJuridica.Visible = false;
                            PanelDatosPersonaNatural.Visible = false;
                        }
                    }
                    else
                    {
                        msgGrilla.Text = "No se encontró persona coincidente con el filtro de búsqueda.";
                        Content_msgGrilla.Visible = true;
                        Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    }

                }
                catch (Exception ex)
                {

                }


            }

        }



        protected void CambiaTipoPersona_Click(object sender, EventArgs e)
        {
            PanelPersonaJuridica.Visible = false;
            PanelDatosPersonaNatural.Visible = false;
            NombreSolicitanteNatural.Text = "";
            Genero.Text = "";
            SubtipoPersonaJuridica.Text = "";
            NombreSolicitanteJuridico.Text = "";
            Content_msgGrilla.Visible = false;
        }






        protected void Guardar_Click(object sender, EventArgs e)
        {

            Resolucion resolucion = new Resolucion();


            //SE VERIFICA SI ES UNA MODIFICACION, ESTARE SETEADO EL ID DE LA RESOLUCION EN UN CAMPO OCULTO
            if (idResolucion.Value != null && !idResolucion.Value.Trim().Equals(""))
            {
                resolucion.idResolucion = Convert.ToInt32(idResolucion.Value);
            }


            //Tipo Documento
            if (Convert.ToInt32(TipoDocumento.SelectedValue) > 0)
            {
                resolucion.tipoDocumento = new ParametroGenerico(Convert.ToInt32(TipoDocumento.SelectedValue));
            }

            //Tipo de Relación del Documento
            if (Convert.ToInt32(TipoRelacionDocumento.SelectedValue) > 0)
            {
                resolucion.tipoRelacionDocumento = new ParametroGenerico(Convert.ToInt32(TipoRelacionDocumento.SelectedValue));
            }

            //Origen
            if (Convert.ToInt32(Origen.SelectedValue) > 0)
            {
                resolucion.origen = new ParametroGenerico(Convert.ToInt32(Origen.SelectedValue));
            }


            //Materia
            if (Convert.ToInt32(Materia.SelectedValue) > 0)
            {
                resolucion.materia = new ParametroGenerico(Convert.ToInt32(Materia.SelectedValue));
            }

            //Resultado
            if (Convert.ToInt32(Resultado.SelectedValue) > 0)
            {
                resolucion.resultado = new ParametroGenerico(Convert.ToInt32(Resultado.SelectedValue));
            }

            //Documento principal
            if (Convert.ToInt32(TipoRelacionDocumento.SelectedValue) == rbTipo.RESOLUCION_COMPLEMENTARIA && Convert.ToInt32(DocumentoPrincipal.SelectedValue) > 0)
            {
                resolucion.resolucionPrincipal = new Resolucion();
                resolucion.resolucionPrincipal.idResolucion = Convert.ToInt32(DocumentoPrincipal.SelectedValue);
            }

            //Número
            if (!Numero.Text.Trim().Equals(""))
            {
                resolucion.numero = Convert.ToString(Numero.Text);
            }


            //Fecha
            if (!Fecha.Text.Trim().Equals(""))
            {
                resolucion.fecha = Convert.ToDateTime(Fecha.Text);
            }


            //Número C.I.
            if (!NumeroCI.Text.Trim().Equals(""))
            {
                resolucion.numeroCI = Convert.ToInt32(NumeroCI.Text);
            }


            //Fecha C.I.
            if (!FechaCI.Text.Trim().Equals(""))
            {
                resolucion.fechaCI = Convert.ToDateTime(FechaCI.Text);
            }


            //Nº Diario Oficial
            if (!NroDiarioOficial.Text.Trim().Equals(""))
            {
                resolucion.numeroDiarioOficial = Convert.ToString(NroDiarioOficial.Text);
            }


            //Fecha de Diario Oficial
            if (!FechaDiarioOficial.Text.Trim().Equals(""))
            {
                resolucion.fechaDiarioOficial = Convert.ToDateTime(FechaDiarioOficial.Text);
            }


            //Vigencia
            if (Convert.ToInt32(Vigencia.SelectedValue) > 0)
            {
                resolucion.vigencia = new ParametroGenerico(Convert.ToInt32(Vigencia.SelectedValue));
            }


            //Fecha Inicio Plazo
            if (!FechaInicioPlazo.Text.Trim().Equals(""))
            {
                resolucion.fechaInicioPlazo = Convert.ToDateTime(FechaInicioPlazo.Text);
            }


            //Fecha Vencimiento
            if (!FechaVencimiento.Text.Trim().Equals(""))
            {
                resolucion.fechaVencimiento = Convert.ToDateTime(FechaVencimiento.Text);
            }


            //Nueva Fecha (fecha extendida)
            if (!NuevaFecha.Text.Trim().Equals(""))
            {
                resolucion.nuevaFecha = Convert.ToDateTime(NuevaFecha.Text);
            }

            //OBSERVACIONES
            if (!Observaciones.Text.Trim().Equals(""))
            {
                resolucion.observaciones = Observaciones.Text;
            }


            //TIENE REFERENCIA
            if (SinReferencia.Checked)
            {
                resolucion.tieneReferencia = 2;
            }
            else if (ConReferencia.Checked)
            {
                resolucion.tieneReferencia = 1;
            }






            ////CAMBIO EL ARCHIVO
            //if (ArchivoAdjunto.HasFile)
            //{

            //    ArchivoBinario archivoBinario = new ArchivoBinario();

            //    archivoBinario.nombreArchivo = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
            //    archivoBinario.nombreFisico = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
            //    archivoBinario.formato = ArchivoAdjunto.PostedFile.FileName.Substring(ArchivoAdjunto.PostedFile.FileName.LastIndexOf(".") + 1).ToLower(); ;
            //    archivoBinario.tamano = ArchivoAdjunto.PostedFile.InputStream.Length;
            //    archivoBinario.archivo = ArchivoAdjunto.PostedFile;

            //    resolucion.archivoAdjunto = archivoBinario;
            //}
            //else
            //{

            //    //NO HIZO NADA CON EL ARCHIVO, SE DEBE MANTENER
            //    if (!idArchivo.Value.Equals("") && Convert.ToInt32(idArchivo.Value) > 0)
            //    {
            //        ArchivoBinario archivoBinario = new ArchivoBinario();
            //        archivoBinario.idArchivo = Convert.ToInt32(idArchivo.Value);

            //        resolucion.archivoAdjunto = archivoBinario;

            //    }

            //    //BORRE EL ARCHIVO
            //    if (idArchivo.Value.Equals(""))
            //    {
            //        resolucion.archivoAdjunto = null;
            //    }

            //}


            //Vinculación Unidad Espacial
            List<Referencia> listaUnidadesEspaciales = (List<Referencia>)ViewState["UnidadesEspaciales"];
            resolucion.referenciasUnidadEspacial = listaUnidadesEspaciales;

            //Referencia a Documentos
            List<Referencia> listaReferencias = (List<Referencia>)ViewState["ReferenciasDocumentos"];
            resolucion.referenciasDocumento = listaReferencias;

            //Ubicación
            List<Referencia> listaUbicaciones = (List<Referencia>)ViewState["Ubicaciones"];
            resolucion.referenciasUbicacion = listaUbicaciones;

            //Especie de Documento
            List<Referencia> listaEspecies = (List<Referencia>)ViewState["Especies"];
            resolucion.referenciasEspecie = listaEspecies;

            //Titular de Documento
            List<Referencia> listaTitulares = (List<Referencia>)ViewState["Titulares"];
            resolucion.referenciasTitular = listaTitulares;



            List<String> errores = ingresoResolucionValidacion.validaIngresoResolucion(resolucion);


            ErroresSuperior.Text = "";

            if (errores.Count > 0)
            {
                PanelErroresSuperior.Visible = true;
                foreach (String error in errores)
                {
                    ErroresSuperior.Text = ErroresSuperior.Text + error + "<br/>";
                }
                UpdatePanelErroresSuperior.Update();

            }
            else
            {

                if (resolucion.idResolucion == 0)
                {

                    if (!resolucionService.GuardarResolucion(resolucion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario))
                    {

                        PanelErroresSuperior.Visible = true;
                        ErroresSuperior.Text = "Ha ocurrido un error al realizar la accion solicituda";
                        UpdatePanelErroresSuperior.Update();
                    }
                    else
                    {

                        this.mensaje = "Resolución guardada con exito";
                        Server.Transfer("~/Resoluciones/administrarResoluciones.aspx");
                        UpdatePanelErroresSuperior.Update();
                    }


                }
                else
                {


                    if (!resolucionService.ActualizarResolucion(resolucion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario))
                    {

                        PanelErroresSuperior.Visible = true;
                        ErroresSuperior.Text = "Ha ocurrido un error al realizar la accion solicituda";
                        UpdatePanelErroresSuperior.Update();
                    }
                    else
                    {

                        this.mensaje = "Resolución actualizada con exito";
                        Server.Transfer("~/Resoluciones/administrarResoluciones.aspx");
                        UpdatePanelErroresSuperior.Update();
                    }


                }


            }

            UpdatePanelErroresSuperior.Update();

        }




        //GRID UNIDADES ESPACIALES
        protected void GridUnidadEspacial_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{


            //    //idResolucion
            //    HiddenField hidden_idReferencia = (HiddenField)e.Row.FindControl("gIdReferencia");
            //    if (hidden_idReferencia != null && !hidden_idReferencia.Value.Equals("") && Convert.ToInt32(hidden_idReferencia.Value) > 0)
            //    {

            //        ImageButton boton_noVigente = (ImageButton)e.Row.FindControl("gNoVigente");
            //        if (boton_noVigente != null)
            //        {

            //            HiddenField hidden_idVigencia = (HiddenField)e.Row.FindControl("gIdVigencia");

            //            boton_noVigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea dejar no vigente esta referencia?')");
            //            if (hidden_idVigencia != null && !hidden_idVigencia.Value.Equals("") && Convert.ToInt32(hidden_idVigencia.Value) == rbEstadosGenerales.VIGENTE)
            //            {
            //                boton_noVigente.Visible = true;
            //            }

            //        };

            //        ImageButton boton_vigente = (ImageButton)e.Row.FindControl("gVigente");
            //        if (boton_vigente != null)
            //        {

            //            HiddenField hidden_idVigencia = (HiddenField)e.Row.FindControl("gIdVigencia");

            //            boton_vigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea dejar vigente esta referencia?')");
            //            if (hidden_idVigencia != null && !hidden_idVigencia.Value.Equals("") && Convert.ToInt32(hidden_idVigencia.Value) == rbEstadosGenerales.NO_VIGENTE)
            //            {
            //                boton_vigente.Visible = true;
            //            }
            //        };

            //    };



            //    //ACCION
            //    HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
            //    if (hidden_accion != null && !hidden_accion.Value.Equals("") && Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR)
            //    {
            //        e.Row.Attributes["style"] = "display:none";
            //    };


            //    //Borrar
            //    ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
            //    if (boton_borrar != null)
            //    {
            //        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
            //        {
            //            boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar este registro?')");
            //            boton_borrar.Visible = true;
            //        }
            //    };

            //};
        }

        //GRID UNIDADES ESPACIALES
        protected void GridUnidadEspacial_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int claveFila = Convert.ToInt32(e.CommandArgument.ToString());
            List<Referencia> listaView = (List<Referencia>)ViewState["UnidadesEspaciales"];

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
                                foreach (Referencia referencia in listaView)
                                {

                                    if (referencia.index == claveFila)
                                    {
                                        referencia.accion = accion.ELIMINAR;
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

                            foreach (Referencia referenciaInList in listaView)
                            {

                                if (referenciaInList.idReferencia == claveFila)
                                {
                                    referenciaInList.estadoVigencia = new ParametroGenerico(rbEstadosGenerales.NO_VIGENTE);
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

                            foreach (Referencia referenciaInList in listaView)
                            {

                                if (referenciaInList.idReferencia == claveFila)
                                {
                                    referenciaInList.estadoVigencia = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
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
        protected void GridUnidadEspacial_RowCreated(object sender, GridViewRowEventArgs e)
        {


        }

        //GRID UNIDADES ESPACIALES
        private void CargarListaUnidadEspaciales()
        {

            List<Referencia> listaUnidadesEspaciales = (List<Referencia>)ViewState["UnidadesEspaciales"];

            if (listaUnidadesEspaciales == null)
            {
                listaUnidadesEspaciales = new List<Referencia>();
            }


            GridUnidadEspacial.DataSource = listaUnidadesEspaciales;
            GridUnidadEspacial.DataBind();


        }

        //GRID UNIDADES ESPACIALES
        protected void GridUnidadEspacial_Agregar(object sender, EventArgs e)
        {

            List<Referencia> listaUnidadesEspaciales = (List<Referencia>)ViewState["UnidadesEspaciales"];
            int index = 0;


            if (listaUnidadesEspaciales == null)
            {
                listaUnidadesEspaciales = new List<Referencia>();
            }
            else
            {
                index = listaUnidadesEspaciales.Count;
            }


            Referencia newReferencia = new Referencia();
            newReferencia.index = index;
            newReferencia.estadoVigencia = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
            newReferencia.accion = accion.INGRESAR;
            newReferencia.idTipoReferencia = rbTipo.RESOLUCION_REFERENCIA_UC;

            if (Convert.ToInt32(Tipo.SelectedValue) > 0)
            {
                newReferencia.tipo = new ParametroGenerico(Convert.ToInt32(Tipo.SelectedValue), Tipo.SelectedItem.Text);
            }

            if (Convert.ToInt32(TipoUnidadEspacial.SelectedValue) > 0)
            {
                newReferencia.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(TipoUnidadEspacial.SelectedValue), TipoUnidadEspacial.SelectedItem.Text);
            }

            if (Convert.ToInt32(TipoSolicitud.SelectedValue) > 0)
            {
                newReferencia.tipoSolicitud = new ParametroGenerico(Convert.ToInt32(TipoSolicitud.SelectedValue), TipoSolicitud.SelectedItem.Text);
            }

            if (!CodigoCentro.Text.Trim().Equals(""))
            {
                newReferencia.codigoCentro = CodigoCentro.Text;
            }

            if (!NumeroPert.Text.Trim().Equals(""))
            {
                newReferencia.numeroPert = NumeroPert.Text;
            }

            if (!NumeroIdentificador.Text.Trim().Equals(""))
            {
                newReferencia.numeroIdentificador = NumeroIdentificador.Text;
            }

            if (!NumeroSector.Text.Trim().Equals(""))
            {
                newReferencia.numSector = Convert.ToInt32(NumeroSector.Text);
            }

            //VALIDAR EL INGRESO DE LA NUEVA REFERENCIA
            List<String> errores = ingresoResolucionValidacion.validarIngresoUnidadEspacial(newReferencia, (List<Referencia>)ViewState["UnidadesEspaciales"]);

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
                NumeroPert.Text = "";
                NumeroIdentificador.Text = "";
                UpdatePanelCodigoCentro.Update();
                UpdatePanelNumeroPert.Update();
                UpdatePanelNumeroIdentificador.Update();


                ErroresUnidadEspacial.Text = "";
                PanelErroresUnidadEspacial.Visible = false;
                UpdatePanelErroresUnidadEspacial.Update();

                listaUnidadesEspaciales.Add(newReferencia);

                ViewState["UnidadesEspaciales"] = (List<Referencia>)listaUnidadesEspaciales;
                this.CargarListaUnidadEspaciales();

            }

            UpdatePanelErroresSuperior.Update();
        }






        //GRID REFERENCIAS DOCUMENTOS
        protected void GridReferencia_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{



            //    //ACCION
            //    HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
            //    if (hidden_accion != null && !hidden_accion.Value.Equals("") && Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR)
            //    {
            //        e.Row.Attributes["style"] = "display:none";
            //    };


            //    //Borrar
            //    ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
            //    if (boton_borrar != null)
            //    {
            //        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
            //        {
            //            boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar este documento?')");
            //            boton_borrar.Visible = true;
            //        }
            //    };

            //};
        }

        //GRID REFERENCIAS DOCUMENTOS
        protected void GridReferencia_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument.ToString());

            ErroresInferior.Text = "";
            PanelErroresInferior.Visible = false;
            UpdatePanelErroresInferior.Update();


            switch (e.CommandName)
            {

                case "Eliminar":

                    //VALIDAR QUE SEA POSIBLE ELIMINAR
                    List<String> errores = new List<string>();
                    if (errores.Count == 0)
                    {

                        //ELIMINAR EL ELEMENTO DE LA LISTA
                        List<Referencia> listaReferencias = (List<Referencia>)ViewState["ReferenciasDocumentos"];

                        if (listaReferencias != null)
                        {
                            foreach (Referencia referencia in listaReferencias)
                            {

                                if (referencia.index == index)
                                {
                                    referencia.accion = accion.ELIMINAR;
                                }
                            }
                        }

                        ViewState["ReferenciasDocumentos"] = listaReferencias;
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


                    CargarListaReferencias();
                    break;

            };


        }

        //GRID REFERENCIAS DOCUMENTOS
        protected void GridReferencia_RowCreated(object sender, GridViewRowEventArgs e)
        {


        }

        //GRID REFERENCIAS DOCUMENTOS
        private void CargarListaReferencias()
        {

            List<Referencia> listaReferencias = (List<Referencia>)ViewState["ReferenciasDocumentos"];

            if (listaReferencias == null)
            {
                listaReferencias = new List<Referencia>();
            }


            GridReferencia.DataSource = listaReferencias;
            GridReferencia.DataBind();


        }

        //GRID REFERENCIAS DOCUMENTOS
        protected void GridReferencia_Agregar(object sender, EventArgs e)
        {

            List<Referencia> listaReferencias = (List<Referencia>)ViewState["ReferenciasDocumentos"];
            int index = 0;


            if (listaReferencias == null)
            {
                listaReferencias = new List<Referencia>();
            }
            else
            {
                index = listaReferencias.Count;
            }


            Referencia newReferencia = new Referencia();
            newReferencia.index = index;
            newReferencia.accion = accion.INGRESAR;
            newReferencia.idTipoReferencia = rbTipo.RESOLUCION_REFERENCIA_DOCUMENTO;




            if (Convert.ToInt32(OrigenReferencia.SelectedValue) > 0)
            {
                newReferencia.origenReferencia = new ParametroGenerico(Convert.ToInt32(OrigenReferencia.SelectedValue), OrigenReferencia.SelectedItem.Text);
            }

            if (!NumeroReferencia.Text.Trim().Equals(""))
            {
                newReferencia.numeroReferencia = Convert.ToString(NumeroReferencia.Text.Trim());  
            }

            if (!FechaReferencia.Text.Trim().Equals(""))
            {
                newReferencia.fechaReferencia = Convert.ToDateTime(FechaReferencia.Text);
            }


            //VALIDAR EL INGRESO DE LA NUEVA REFERENCIA
            List<String> errores = ingresoResolucionValidacion.validaIngresoDocumento(newReferencia, listaReferencias);

            if (errores.Count > 0)
            {
                foreach (String error in errores)
                {
                    PanelErroresReferenciaDocumentos.Visible = true;
                    ErroresReferenciaDocumentos.Text = error;
                    UpdatePanelErroresReferenciaDocumentos.Update();
                }
            }
            else
            {


                OrigenReferencia.SelectedValue = "0";
                NumeroReferencia.Text = "";
                FechaReferencia.Text = "";

                UpdatePanelOrigenReferencia.Update();
                UpdatePanelNumeroReferencia.Update();
                UpdatePanelFechaReferencia.Update();

                ErroresReferenciaDocumentos.Text = "";
                PanelErroresReferenciaDocumentos.Visible = false;
                UpdatePanelErroresReferenciaDocumentos.Update();


                listaReferencias.Add(newReferencia);

                ViewState["ReferenciasDocumentos"] = (List<Referencia>)listaReferencias;

                this.CargarListaReferencias();

            }

            UpdatePanelErroresSuperior.Update();
        }







        //GRID UBICACION
        protected void GridUbicacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{



            //    //ACCION
            //    HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
            //    if (hidden_accion != null && !hidden_accion.Value.Equals("") && Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR)
            //    {
            //        e.Row.Attributes["style"] = "display:none";
            //    };


            //    //Borrar
            //    ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
            //    if (boton_borrar != null)
            //    {
            //        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
            //        {
            //            boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar este documento?')");
            //            boton_borrar.Visible = true;
            //        }
            //    };

            //};
        }

        //GRID UBICACION
        protected void GridUbicacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument.ToString());

            ErroresInferior.Text = "";
            PanelErroresInferior.Visible = false;
            UpdatePanelErroresInferior.Update();


            switch (e.CommandName)
            {

                case "Eliminar":

                    //VALIDAR QUE SEA POSIBLE ELIMINAR
                    List<String> errores = new List<string>();
                    if (errores.Count == 0)
                    {

                        //ELIMINAR EL ELEMENTO DE LA LISTA
                        List<Referencia> listaUbicaciones = (List<Referencia>)ViewState["Ubicaciones"];

                        if (listaUbicaciones != null)
                        {
                            foreach (Referencia referencia in listaUbicaciones)
                            {

                                if (referencia.index == index)
                                {
                                    referencia.accion = accion.ELIMINAR;
                                }
                            }
                        }

                        ViewState["Ubicaciones"] = listaUbicaciones;
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


                    CargarListaUbicaciones();
                    break;

            };


        }

        //GRID UBICACION
        protected void GridUbicacion_RowCreated(object sender, GridViewRowEventArgs e)
        {


        }

        //GRID UBICACION
        private void CargarListaUbicaciones()
        {

            List<Referencia> listaUbicaciones = (List<Referencia>)ViewState["Ubicaciones"];

            if (listaUbicaciones == null)
            {
                listaUbicaciones = new List<Referencia>();
            }


            GridUbicacion.DataSource = listaUbicaciones;
            GridUbicacion.DataBind();


        }

        //GRID UBICACION
        protected void GridUbicacion_Agregar(object sender, EventArgs e)
        {

            List<Referencia> listaUbicaciones = (List<Referencia>)ViewState["Ubicaciones"];
            int index = 0;


            if (listaUbicaciones == null)
            {
                listaUbicaciones = new List<Referencia>();
            }
            else
            {
                index = listaUbicaciones.Count;
            }


            Referencia newReferencia = new Referencia();
            newReferencia.index = index;
            newReferencia.accion = accion.INGRESAR;
            newReferencia.idTipoReferencia = rbTipo.RESOLUCION_REFERENCIA_UBICACION;



            if (Convert.ToInt32(Region.SelectedValue) > 0)
            {
                newReferencia.region = new ParametroGenerico(Convert.ToInt32(Region.SelectedValue), Region.SelectedItem.Text);
            }


            if (Convert.ToInt32(Comuna.SelectedValue) > 0)
            {
                newReferencia.comuna = new ParametroGenerico(Convert.ToInt32(Comuna.SelectedValue), Comuna.SelectedItem.Text);
            }


            if (!Sector.Text.Trim().Equals(""))
            {
                newReferencia.sector = Sector.Text;
            }

            //VALIDAR EL INGRESO DE LA NUEVA REFERENCIA
            List<String> errores = ingresoResolucionValidacion.validaIngresoUbicacion(newReferencia, listaUbicaciones);

            if (errores.Count > 0)
            {
                foreach (String error in errores)
                {
                    PanelErroresUbicacion.Visible = true;
                    ErroresUbicacion.Text = error;
                    UpdatePanelErroresUbicacion.Update();
                }
            }
            else
            {

                Region.SelectedValue = "0";
                Comuna.SelectedValue = "0";
                Sector.Text = "";

                UpdatePanelRegion.Update();
                UpdatePanelComuna.Update();
                UpdatePanelSector.Update();

                ErroresUbicacion.Text = "";
                PanelErroresUbicacion.Visible = false;
                UpdatePanelErroresUbicacion.Update();

                listaUbicaciones.Add(newReferencia);

                ViewState["Ubicaciones"] = (List<Referencia>)listaUbicaciones;
                this.CargarListaUbicaciones();

            }

            UpdatePanelErroresSuperior.Update();
        }






        //GRID ESPECIE
        protected void GridEspecie_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{



            //    //ACCION
            //    HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
            //    if (hidden_accion != null && !hidden_accion.Value.Equals("") && Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR)
            //    {
            //        e.Row.Attributes["style"] = "display:none";
            //    };


            //    //Borrar
            //    ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
            //    if (boton_borrar != null)
            //    {
            //        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
            //        {
            //            boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar este documento?')");
            //            boton_borrar.Visible = true;
            //        }
            //    };

            //};
        }

        //GRID ESPECIE
        protected void GridEspecie_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument.ToString());

            ErroresInferior.Text = "";
            PanelErroresInferior.Visible = false;
            UpdatePanelErroresInferior.Update();


            switch (e.CommandName)
            {

                case "Eliminar":

                    //VALIDAR QUE SEA POSIBLE ELIMINAR
                    List<String> errores = new List<string>();
                    if (errores.Count == 0)
                    {

                        //ELIMINAR EL ELEMENTO DE LA LISTA
                        List<Referencia> listaEspecies = (List<Referencia>)ViewState["Especies"];

                        if (listaEspecies != null)
                        {
                            foreach (Referencia referencia in listaEspecies)
                            {

                                if (referencia.index == index)
                                {
                                    referencia.accion = accion.ELIMINAR;
                                }
                            }
                        }

                        ViewState["Especies"] = listaEspecies;
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


                    CargarListaEspecies();
                    break;

            };


        }

        //GRID ESPECIE
        protected void GridEspecie_RowCreated(object sender, GridViewRowEventArgs e)
        {


        }

        //GRID ESPECIE
        private void CargarListaEspecies()
        {

            List<Referencia> listaEspecies = (List<Referencia>)ViewState["Especies"];

            if (listaEspecies == null)
            {
                listaEspecies = new List<Referencia>();
            }


            GridEspecie.DataSource = listaEspecies;
            GridEspecie.DataBind();


        }

        //GRID ESPECIE
        protected void GridEspecie_Agregar(object sender, EventArgs e)
        {

            List<Referencia> listaEspecies = (List<Referencia>)ViewState["Especies"];
            int index = 0;


            if (listaEspecies == null)
            {
                listaEspecies = new List<Referencia>();
            }
            else
            {
                index = listaEspecies.Count;
            }


            Referencia newReferencia = new Referencia();
            newReferencia.index = index;
            newReferencia.accion = accion.INGRESAR;
            newReferencia.idTipoReferencia = rbTipo.RESOLUCION_REFERENCIA_ESPECIE;


            if (Convert.ToInt32(Especie.SelectedValue) > 0)
            {
                newReferencia.especie = new ParametroGenerico(Convert.ToInt32(Especie.SelectedValue), Especie.SelectedItem.Text);
            }



            //VALIDAR EL INGRESO DE LA NUEVA REFERENCIA
            List<String> errores = ingresoResolucionValidacion.validaIngresoEspecie(newReferencia, listaEspecies);

            if (errores.Count > 0)
            {
                foreach (String error in errores)
                {
                    PanelErroresEspecie.Visible = true;
                    ErroresEspecie.Text = error;
                    UpdatePanelErroresEspecie.Update();
                }
            }
            else
            {

                Especie.SelectedValue = "0";
                UpdatePanelEspecie.Update();


                ErroresEspecie.Text = "";
                PanelErroresEspecie.Visible = false;
                UpdatePanelErroresEspecie.Update();

                listaEspecies.Add(newReferencia);

                ViewState["Especies"] = (List<Referencia>)listaEspecies;
                this.CargarListaEspecies();

            }

            UpdatePanelErroresSuperior.Update();
        }





        //GRID TITULAR
        protected void GridTitular_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{



            //    //ACCION
            //    HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
            //    if (hidden_accion != null && !hidden_accion.Value.Equals("") && Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR)
            //    {
            //        e.Row.Attributes["style"] = "display:none";
            //    };


            //    //Borrar
            //    ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
            //    if (boton_borrar != null)
            //    {
            //        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
            //        {
            //            boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar este documento?')");
            //            boton_borrar.Visible = true;
            //        }
            //    };

            //};
        }

        //GRID TITULAR
        protected void GridTitular_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument.ToString());

            ErroresInferior.Text = "";
            PanelErroresInferior.Visible = false;
            UpdatePanelErroresInferior.Update();


            switch (e.CommandName)
            {

                case "Eliminar":

                    //VALIDAR QUE SEA POSIBLE ELIMINAR
                    List<String> errores = new List<string>();
                    if (errores.Count == 0)
                    {

                        //ELIMINAR EL ELEMENTO DE LA LISTA
                        List<Referencia> listaTitulares = (List<Referencia>)ViewState["Titulares"];

                        if (listaTitulares != null)
                        {
                            foreach (Referencia referencia in listaTitulares)
                            {

                                if (referencia.index == index)
                                {
                                    referencia.accion = accion.ELIMINAR;
                                }
                            }
                        }

                        ViewState["Titulares"] = listaTitulares;
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


                    CargarListaTitulares();
                    break;

            };


        }

        //GRID TITULAR
        protected void GridTitular_RowCreated(object sender, GridViewRowEventArgs e)
        {


        }

        //GRID TITULAR
        private void CargarListaTitulares()
        {

            List<Referencia> listaTitulares = (List<Referencia>)ViewState["Titulares"];

            if (listaTitulares == null)
            {
                listaTitulares = new List<Referencia>();
            }


            GridTitular.DataSource = listaTitulares;
            GridTitular.DataBind();


        }

        //GRID TITULAR
        protected void GridTitular_Agregar(object sender, EventArgs e)
        {

            List<Referencia> listaTitulares = (List<Referencia>)ViewState["Titulares"];
            int index = 0;


            if (listaTitulares == null)
            {
                listaTitulares = new List<Referencia>();
            }
            else
            {
                index = listaTitulares.Count;
            }


            Referencia newReferencia = new Referencia();
            newReferencia.index = index;
            newReferencia.accion = accion.INGRESAR;
            newReferencia.idTipoReferencia = rbTipo.RESOLUCION_REFERENCIA_TITULAR;




            Solicitante titular = new Solicitante();

            if (!RutPersona.Text.Trim().Equals(""))
            {

                try
                {

                    String rutCompleto = Convert.ToString(RutPersona.Text);
                    String[] rutPartes = rutCompleto.Split('-');

                    titular.rut = Convert.ToInt32(rutPartes[0]);
                    titular.dv = Convert.ToChar(rutPartes[1]);

                    if (!NombreSolicitanteNatural.Text.Trim().Equals(""))
                    {
                        titular.nombreSolicitante = NombreSolicitanteNatural.Text.Trim();
                    }
                    else if (!NombreSolicitanteJuridico.Text.Trim().Equals(""))
                    {
                        titular.nombreSolicitante = NombreSolicitanteJuridico.Text.Trim();
                    }

                }
                catch (Exception ex)
                {

                }


                newReferencia.titular = titular;
            }


            //VALIDAR EL INGRESO DE LA NUEVA REFERENCIA
            List<String> errores = ingresoResolucionValidacion.validarIngresoTitular(titular, listaTitulares);

            if (errores.Count > 0)
            {
                foreach (String error in errores)
                {
                    PanelErroresTitular.Visible = true;
                    ErroresTitular.Text = error;
                    UpdatePanelErroresTitular.Update();

                }
            }
            else
            {

                RutPersona.Text = "";
                NombreSolicitanteNatural.Text = "";
                NombreSolicitanteJuridico.Text = "";
                UpdatePanelTitular.Update();


                ErroresTitular.Text = "";
                PanelErroresTitular.Visible = false;
                UpdatePanelErroresTitular.Update();

                listaTitulares.Add(newReferencia);

                ViewState["Titulares"] = (List<Referencia>)listaTitulares;
                this.CargarListaTitulares();

            }

            UpdatePanelErroresSuperior.Update();
        }


        //CARGAR INFORMACION DE LA UNIDAD ESPACIAL
        public void Identificador_TextChanged(object sender, EventArgs e)
        {

            titularesCAD.Text = "";
            regionCad.Text = "";
            toponimioCad.Text = "";
            UpdatePanelDatosCentro.Update();

            ResolucionService resolucionService = new ResolucionService();




            if (Convert.ToInt32(Tipo.SelectedValue) == rbTipo.RESOLUCION_SUB_REFERENCIA_SOLICITUD) //SOLICITUDES
            {
                if (Convert.ToInt32(TipoSolicitud.SelectedValue) > 0)
                {

                    if (Convert.ToInt32(TipoSolicitud.SelectedValue) == rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA && !NumeroIdentificador.Text.Trim().Equals("") && Convert.ToInt32(NumeroIdentificador.Text) > 0) //COLECTORES
                    {

                        //OBTENER LA SOLICITUD
                        SolicitudConcesion sol = resolucionService.VerificaExistenciaReferencia(Convert.ToInt32(Tipo.SelectedValue), 0, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA, NumeroIdentificador.Text, 0);

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
                    else if (Convert.ToInt32(TipoSolicitud.SelectedValue) == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION && !NumeroPert.Text.Trim().Equals("") && !NumeroSector.Text.Trim().Equals("")) //Sector Relocalizacion
                    {

                        String pertRelocalizaciones = NumeroPert.Text.Trim() + "-" + NumeroSector.Text.Trim().ToString();

                        //VERIFICAR QUE EXISTA
                        SolicitudConcesion sol = resolucionService.VerificaExistenciaReferencia(Convert.ToInt32(Tipo.SelectedValue), 0, rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION, pertRelocalizaciones, Convert.ToInt32(NumeroSector.Text.Trim()));


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
                    else if (!NumeroPert.Text.Trim().Equals(""))  //OTROS TIPOS DE SOLICITUDES
                    {


                        //VERIFICAR QUE EXISTA
                        SolicitudConcesion sol = resolucionService.VerificaExistenciaReferencia(Convert.ToInt32(Tipo.SelectedValue), 0, Convert.ToInt32(TipoSolicitud.SelectedValue), NumeroPert.Text.Trim(), 0);


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
                }
            }




            if (Convert.ToInt32(Tipo.SelectedValue) == rbTipo.RESOLUCION_SUB_REFERENCIA_UE) //CONCESIONES
            {
                if (Convert.ToInt32(TipoUnidadEspacial.SelectedValue) > 0)
                {

                    if (Convert.ToInt32(TipoUnidadEspacial.SelectedValue) == rbTipo.UNID_ESPACIAL_COLECTORES_DE_SEMILLA && !NumeroIdentificador.Text.Trim().Equals("") && Convert.ToInt32(NumeroIdentificador.Text) > 0) //COLECTORES
                    {

                        //OBTENER LA UNIDAD ESPACIAL
                        SolicitudConcesion sol = resolucionService.VerificaExistenciaReferencia(Convert.ToInt32(Tipo.SelectedValue), rbTipo.UNID_ESPACIAL_COLECTORES_DE_SEMILLA, 0, NumeroIdentificador.Text, 0);

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
                    else if (!CodigoCentro.Text.Trim().Equals(""))  //OTROS TIPOS DE UNIDADES ESPACIALES
                    {


                        //VERIFICAR QUE EXISTA
                        SolicitudConcesion sol = resolucionService.VerificaExistenciaReferencia(Convert.ToInt32(Tipo.SelectedValue), Convert.ToInt32(TipoUnidadEspacial.SelectedValue), 0, CodigoCentro.Text.Trim(), 0);


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
                }
            }

        }



        //CARGA UNA RESOLUCION EXISTENTE EN LA BASE DE DATOS
        protected void cargarResolucion(Resolucion resolucionDB)
        {


            if (resolucionDB != null)
            {


                idResolucion.Value = Convert.ToString(resolucionDB.idResolucion);


                TipoDocumento.SelectedValue = Convert.ToString(resolucionDB.tipoDocumento.id);
                this.TipoDocumento_OnSelectedIndexChanged(null, null);

                TipoIngresoResolucion.SelectedValue = Convert.ToString(resolucionDB.tipoIngreso.id);

                TipoRelacionDocumento.SelectedValue = Convert.ToString(resolucionDB.tipoRelacionDocumento.id);
                this.TipoRelacionDocumento_OnSelectedIndexChanged(null, null);

                Origen.SelectedValue = Convert.ToString(resolucionDB.origen.id);
                this.Carga_Combobox("Materia");

                if (resolucionDB.numero != null)
                {
                    Numero.Text = Convert.ToString(resolucionDB.numero);
                }

                if (resolucionDB.fecha != null && resolucionDB.fecha != default(DateTime))
                {
                    Fecha.Text = FechaUtils.formatearFecha(resolucionDB.fecha);
                }


                if (resolucionDB.numeroCI > 0)
                {
                    NumeroCI.Text = Convert.ToString(resolucionDB.numeroCI);
                }


                if (resolucionDB.fechaCI != null && resolucionDB.fechaCI != default(DateTime))
                {
                    FechaCI.Text = FechaUtils.formatearFecha(resolucionDB.fechaCI);
                }


                Materia.SelectedValue = Convert.ToString(resolucionDB.materia.id);
                this.Materia_OnSelectedIndexChanged(null, null);

                if (resolucionDB.resultado != null && resolucionDB.resultado.id > 0)
                {
                    Resultado.SelectedValue = Convert.ToString(resolucionDB.resultado.id);
                }


                if (resolucionDB.numeroDiarioOficial != null && !resolucionDB.numeroDiarioOficial.Trim().Equals(""))
                {
                    NroDiarioOficial.Text = resolucionDB.numeroDiarioOficial.Trim();
                }

                if (resolucionDB.fechaDiarioOficial != null && resolucionDB.fechaDiarioOficial != default(DateTime))
                {
                    FechaDiarioOficial.Text = FechaUtils.formatearFecha(resolucionDB.fechaDiarioOficial);
                }

                if (resolucionDB.resultado != null && resolucionDB.resultado.id > 0)
                {
                    Vigencia.SelectedValue = Convert.ToString(resolucionDB.vigencia.id);
                }

                if (resolucionDB.vigencia != null && resolucionDB.vigencia.id > 0)
                {
                    Vigencia.SelectedValue = Convert.ToString(resolucionDB.vigencia.id);
                }

                if (resolucionDB.fechaInicioPlazo != null && resolucionDB.fechaInicioPlazo != default(DateTime))
                {
                    FechaInicioPlazo.Text = FechaUtils.formatearFecha(resolucionDB.fechaInicioPlazo);
                }


                if (resolucionDB.fechaVencimiento != null && resolucionDB.fechaVencimiento != default(DateTime))
                {
                    FechaVencimiento.Text = FechaUtils.formatearFecha(resolucionDB.fechaVencimiento);
                }

                if (resolucionDB.nuevaFecha != null && resolucionDB.nuevaFecha != default(DateTime))
                {
                    NuevaFecha.Text = FechaUtils.formatearFecha(resolucionDB.nuevaFecha);
                }


                if (resolucionDB.observaciones != null && !resolucionDB.observaciones.Trim().Equals(""))
                {
                    Observaciones.Text = Convert.ToString(resolucionDB.observaciones);
                }


                if (resolucionDB.archivoAdjunto != null && resolucionDB.archivoAdjunto.idArchivo > 0)
                {
                    idArchivo.Value = Convert.ToString(resolucionDB.archivoAdjunto.idArchivo);
                    NombreArchivo.Text = resolucionDB.archivoAdjunto.nombreArchivo;
                    trDescargarArchivo.Visible = true;
                    UpdatePanelArchivo.Update();
                }


                if (resolucionDB.resolucionPrincipal != null && resolucionDB.resolucionPrincipal.idResolucion > 0)
                {

                    Resolucion resolucionFiltro = new Resolucion();
                    resolucionFiltro.idResolucion = resolucionDB.resolucionPrincipal.idResolucion;

                    Resolucion resolucionPrincipal = resolucionService.ObtenerResolucion(resolucionFiltro);

                    if (resolucionPrincipal != null)
                    {
                        TipoDocumentoPrincipal.SelectedValue = resolucionPrincipal.tipoDocumento.id.ToString();

                        this.Carga_Combobox("OrigenPrincipal");
                        OrigenPrincipal.SelectedValue = "0";

                        OrigenPrincipal.SelectedValue = resolucionPrincipal.origen.id.ToString();
                        NumeroPrincipal.Text = resolucionPrincipal.numero;
                        FechaPrincipal.Text = FechaUtils.formatearFecha(resolucionPrincipal.fecha);
                    }
                }



                if (resolucionDB.tieneReferencia == 1)
                {

                    SinReferencia.Checked = false;
                    ConReferencia.Checked = true;
                    this.RadGrupoEspecieChecked(null, null);


                    //Vinculación Unidad Espacial
                    ViewState["UnidadesEspaciales"] = (List<Referencia>)resolucionDB.referenciasUnidadEspacial;
                    this.CargarListaUnidadEspaciales();

                    //Referencia a Documentos
                    ViewState["ReferenciasDocumentos"] = (List<Referencia>)resolucionDB.referenciasDocumento;
                    this.CargarListaReferencias();

                    //Ubicación
                    ViewState["Ubicaciones"] = (List<Referencia>)resolucionDB.referenciasUbicacion;
                    this.CargarListaUbicaciones();

                    //Especie
                    ViewState["Especies"] = (List<Referencia>)resolucionDB.referenciasEspecie;
                    this.CargarListaEspecies();

                    //Titular
                    ViewState["Titulares"] = (List<Referencia>)resolucionDB.referenciasTitular;
                    this.CargarListaTitulares();

                }


                PanelDocumentosComplementan.Visible = false;
                UpdatePanelDocumentosComplementan.Update();

                //Carga Resoluciones que Complementan esta Resolución
                DataTable complementaDatatable = resolucionService.ListarResolucionesComplementan(resolucionDB.idResolucion);
                if (complementaDatatable != null && complementaDatatable.Rows.Count > 0)
                {

                    GridViewComplementan.DataSource = complementaDatatable;
                    GridViewComplementan.DataBind();
                    PanelDocumentosComplementan.Visible = true;
                    UpdatePanelDocumentosComplementan.Update();

                }


                //Carga Resoluciones que Referencian esta Resolución
                List<Resolucion> resolucionReferencia = resolucionService.ListarResolucionReferenciada(resolucionDB.idResolucion);
                if (resolucionReferencia != null && resolucionReferencia.Count > 0)
                {

                    GridViewReferencian.DataSource = resolucionReferencia;
                    GridViewReferencian.DataBind();
                    PanelGridViewReferencian.Visible = true;
                    UpdatePanelGridViewReferencian.Update();

                }
            }

        }


        protected void LimpiarArchivo_Click(object sender, EventArgs e)
        {
            idArchivo.Value = "";
            //ArchivoAdjunto.Attributes.Clear();
            NombreArchivo.Text = "";
            UpdatePanelArchivo.Update();

        }



        protected void DescargarArchivo_Click(object sender, ImageClickEventArgs e)
        {


            if (idArchivo.Value != null && !idArchivo.Value.Trim().Equals(""))
            {

                try
                {

                    ArchivoBinario archivoBinario = archivoBinarioSolicitudDA.ObtenerArchivoBinarioSolicitud(Convert.ToInt32(idArchivo.Value));
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


                }
            }
        }



    }

}

