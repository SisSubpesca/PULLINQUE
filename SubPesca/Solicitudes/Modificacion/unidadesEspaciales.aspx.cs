using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.unidadEspacial;
using LogicaNegocio.cl.subpesca.rb.common;
using System.IO;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes.unidadEspacial;
using Datos.Entidades;
using System.Collections;
using Validaciones.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Utilidades;
using SubPesca.Utilidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.modificacion;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;

namespace SubPesca.Solicitudes.Modificacion
{
    public partial class unidadesEspaciales : System.Web.UI.Page
    {


        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        SolicitudDA solicitudDA = new SolicitudDA();
        UnidadEspacialDA unidadEspacialDA = new UnidadEspacialDA();
        CapitaniaDePuertoDA capitaniaDePuertoDA = new CapitaniaDePuertoDA();
        TipoDA tipoDa = new TipoDA();
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();
        UnidadEspacialValidacion unidadEspacialValidacion = new UnidadEspacialValidacion();
        SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();
        PermisosService permisosService = new PermisosService();

        EnviarCorreo enviarCorreo = new EnviarCorreo();

        protected void setearModulo()
        {

            ViewState["URL_VER"] = paginas.URL_VER_SOLMOD;
            ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLMOD;
            ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLMOD;
            ViewState["solicitudSession"] = paginas.solicitudModificacionSession;

            SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];


            if (solicitudConcesion != null)
            {

                int[] tiposModificacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

                int contador = 0;
                foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                {
                    if (tipoModificacion.id == rbTipo.MOD_CONCESION_AMPLIA_SUPERFICIE)
                    {
                        tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_ESPACIAL_MOD_CONCESION_AMPLIA_SUPERFICIE;
                        contador++;
                    }
                    if (tipoModificacion.id == rbTipo.MOD_CONCESION_ESPECIE)
                    {
                        tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_ESPACIAL_MOD_CONCESION_ESPECIE;
                        contador++;
                    }
                    if (tipoModificacion.id == rbTipo.MOD_CONCESION_PT)
                    {
                        tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_ESPACIAL_MOD_CONCESION_PT;
                        contador++;
                    }
                    if (tipoModificacion.id == rbTipo.MOD_CONCESION_REDUCE_SUPERFICIE)
                    {
                        tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_ESPACIAL_MOD_CONCESION_REDUCE_SUPERFICIE;
                        contador++;
                    }
                    if (tipoModificacion.id == rbTipo.MOD_CONCESION_REGULARIZACION)
                    {
                        tiposModificacion[contador] = rbSeccionUnidadEspacial.UNIDAD_ESPACIAL_MOD_CONCESION_REGULARIZACION;
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

                string Lang = "es-CL";//set your culture here
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Lang);
                

                setearModulo();


                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (solicitudConcesion == null || usuario_logeado == null)
                {
                    Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
                }


                //BOTON DE INGRESO O MODIFICACION
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {

                    NumeroDiarioOficial.Enabled = true;
                    FechaDiarioOficial.Enabled = true;
                    
                    //ImagenFecha1.Visible = true;
                    PanelCalendario.Visible = true;

                    PanelBotonLimpiar.Visible = true;
                    PanelBotonCreacion.Visible = true;
                }
                else
                {

                    NumeroDiarioOficial.Enabled = false;
                    FechaDiarioOficial.Enabled = false;
                    
                    //ImagenFecha1.Visible = false;
                    PanelCalendario.Visible = false;

                    PanelBotonLimpiar.Visible = false;
                    PanelBotonCreacion.Visible = false;
                }


                // Inicializamos el formulario
                Initialize_Form();

                // Inicializamos el Hashtable con los Archivos Adjuntos
                //Initialize_HT_ArchivosUnidadEspacial();



            }
        }

        private void Initialize_HT_ArchivosUnidadEspacial()
        {
            string KeySort = "";
            int pagina = 0;
            int codigo_centro = 0;
            string nombre_centro = "";
            int numero_diario_oficial = 0;
            int numero_acta_oficial = 0;
            DateTime fecha_acta_oficial = Convert.ToDateTime("01/01/0001");
            DateTime fecha_diario_oficial = Convert.ToDateTime("01/01/0001");
            int capitania_puerto_entrega = 0;


            Hashtable HT_ModUnidEspacial = (Hashtable)Session["Modulo_UnidadEspacial"];
            Hashtable HT_ArchivosAdjuntosUnidEspacial = new Hashtable();

            HT_ArchivosAdjuntosUnidEspacial.Add("locked", true);
            HT_ArchivosAdjuntosUnidEspacial.Add("KeySort", KeySort);
            HT_ArchivosAdjuntosUnidEspacial.Add("pagina", pagina);
            HT_ArchivosAdjuntosUnidEspacial.Add("codigo_centro", codigo_centro);
            HT_ArchivosAdjuntosUnidEspacial.Add("nombre_centro", nombre_centro);
            HT_ArchivosAdjuntosUnidEspacial.Add("numero_diario_oficial", numero_diario_oficial);
            HT_ArchivosAdjuntosUnidEspacial.Add("fecha_diario_oficial", fecha_diario_oficial);
            HT_ArchivosAdjuntosUnidEspacial.Add("numero_acta_oficial", numero_acta_oficial);
            HT_ArchivosAdjuntosUnidEspacial.Add("fecha_acta_oficial", fecha_acta_oficial);
            HT_ArchivosAdjuntosUnidEspacial.Add("capitania_puerto_entrega", capitania_puerto_entrega);

            // Actualizamos la sesión Modulo_Solicitantes
            if (HT_ModUnidEspacial == null)
            {
                HT_ModUnidEspacial = new Hashtable();
                HT_ModUnidEspacial.Add("ArchivosAdjuntosUnidEspacial", (Hashtable)HT_ArchivosAdjuntosUnidEspacial);
            }
            else
            {
                if (HT_ModUnidEspacial["ArchivosAdjuntosUnidEspacial"] == null)
                {
                    HT_ModUnidEspacial.Add("ArchivosAdjuntosUnidEspacial", (Hashtable)HT_ArchivosAdjuntosUnidEspacial);
                }
                else
                {
                    HT_ModUnidEspacial["ArchivosAdjuntosUnidEspacial"] = (Hashtable)HT_ArchivosAdjuntosUnidEspacial;
                };
            };
            Session["Modulo_UnidadEspacial"] = (Hashtable)HT_ModUnidEspacial;

            CargarListaArchivosAdjuntos(usuario_logeado, Convert.ToInt32(IdSolicitud.Value));
        }


        private void CargarListaArchivosAdjuntos(Usuario.Serializable usuario_logeado, int p)
        {
            string KeySort = "IdSolicitud ASC";
            int pagina = 0;

            Hashtable HT_ModUnidEspacial = new Hashtable();
            Hashtable HT_ArchivosAdjuntoUnidEspacial = new Hashtable();

            try
            {
                HT_ModUnidEspacial = (Hashtable)Session["Modulo_UnidadEspacial"];
                HT_ArchivosAdjuntoUnidEspacial = (Hashtable)HT_ModUnidEspacial["ArchivosAdjuntosUnidEspacial"];
                KeySort = (string)HT_ArchivosAdjuntoUnidEspacial["KeySort"];
                pagina = (int)HT_ArchivosAdjuntoUnidEspacial["pagina"];
            }
            catch
            { };

            //GridArchivoAdjuntoUnidEspacial.PageIndex = pagina;
            //GridArchivoAdjuntoUnidEspacial.DataSource = archivoBinarioSolicitudDA.verArchivoAdjuntoUnidEspacial(IdSolicitud, 0, KeySort, 0);
            //GridArchivoAdjuntoUnidEspacial.DataBind();
        }

        protected void Initialize_Form()
        {
            SolicitudConcesion solicitudAux = (SolicitudConcesion)Session["SolicitudModificacion"];
            
            if (solicitudAux != null && solicitudAux.idSolConcesion > 0)
            {

                IdSolicitud.Value = Convert.ToString(solicitudAux.idSolConcesion);
                
                Initialize_Formulario(solicitudAux);
            }
            else
            {
                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
            }

        }

        private void Initialize_Formulario(SolicitudConcesion solicitudModificacion)
        {
            if (solicitudModificacion != null && solicitudModificacion.idSolConcesion > 0)
            {
                IdSolicitud.Value = Convert.ToString(solicitudModificacion.idSolConcesion);
                
                UnidadEspacial unidadespacial = unidadEspacialDA.ObtieneUnidadEspacialMod(solicitudModificacion.idSolConcesion, 0);

                if (unidadespacial != null)
                {

                    IdUnidadEspacial.Value = Convert.ToString(unidadespacial.idUnidadEspacial);
                    CodigoCentro.Value = Convert.ToString(unidadespacial.centrosDeCultivo.codigoCentro);
                    CodigoCentroReadOnly.Text = Convert.ToString(unidadespacial.centrosDeCultivo.codigoCentro);

                    if (!solicitudModificacionService.tieneSoloTipoModificacionRegularizacion(solicitudModificacion))
                    {

                        if (unidadespacial.numeroDiarioOficial > 0) 
                        { 
                            NumeroDiarioOficial.Text = Convert.ToString(unidadespacial.numeroDiarioOficial);
                        }

                        if (unidadespacial.fechaDiarioOficial != null && unidadespacial.fechaDiarioOficial != default(DateTime)) 
                        {  
                            FechaDiarioOficial.Text = FechaUtils.formatearFecha(unidadespacial.fechaDiarioOficial);
                        }

                        //CapitaniaPuerto.Value = Convert.ToString(unidadespacial.capitaniaDePuerto.idCapitaDePuerto);
                        //CapitaniaPuertoReadOnly.Text = unidadespacial.capitaniaDePuerto.nombreCapitaniaDePuerto;

                        PanelModConcesion.Visible = true;
                        UpdatePanelModConcesion.Update();

                        PanelGuardarUnidEspacial.Visible = true;
                        UpdatePanelGuardarUnidEspacial.Update();

                        PanelBotonLimpiar.Visible = true;
                        UpdatePanelBotonLimpiar.Update();
                    }

                    bool puedeModificarConcesion = solicitudDA.aplicaBotonSolicitudUE(solicitudModificacion.idSolConcesion);
                    if (puedeModificarConcesion)
                    {
                        PanelModificaConcesion.Visible = true;
                        UpdatePanelModificaConcesion.Update();
                    }


                }
                else {
                    CodigoCentro.Value = Convert.ToString(solicitudModificacion.tramiteModConcesion.centro.id);
                    CodigoCentroReadOnly.Text = Convert.ToString(solicitudModificacion.tramiteModConcesion.centro.id);
                } 
            }
            else
            {
                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
            }

        }

        /*
        protected void Initialize_Comboboxs()
        {

            Carga_Combobox("CapitaniaPuerto");
            CapitaniaPuerto.SelectedValue = "0";

            //Carga_Combobox("TipoArchivo");
            //TipoArchivo.SelectedValue = "0";
        }

        private void Carga_Combobox(string combobox)
        {
            switch (combobox)
            {

                case "CapitaniaPuerto":
                   // Cargamos el combobox: CapitaniaPuerto
                   CapitaniaPuerto.Items.Clear();
                   CapitaniaPuerto.DataSource = capitaniaDePuertoDA.obtenerCapitaniaDePuerto(0);
                   CapitaniaPuerto.DataTextField = "CapitaniaPuerto";
                   CapitaniaPuerto.DataValueField = "IdCapitaniaPuerto";
                   CapitaniaPuerto.DataBind();
                   CapitaniaPuerto.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                   break;
               /*
               case "TipoArchivo":
                   // Cargamos el combobox: TipoArchivo
                   TipoArchivo.Items.Clear();
                   TipoArchivo.DataSource = tipoDa.ListarTipo("TIPO_DOCUMENTO");
                   TipoArchivo.DataTextField = "descripcion";
                   TipoArchivo.DataValueField = "id";
                   TipoArchivo.DataBind();
                   TipoArchivo.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                   break;
            }
        }
       */

        /**
         * Método que que guarda los datos de la concesión de acuicultura y la crea.
         */
        protected void CrearConcesion_Click(object sender, ImageClickEventArgs e)
        {
            UnidadEspacialService unidadEspacialService = new UnidadEspacialService();
            UnidadEspacial unidadEspacial = new UnidadEspacial();

            unidadEspacial.idUnidadEspacial = Convert.ToInt32(IdUnidadEspacial.Value);
            unidadEspacial.idSolicitud = Convert.ToInt32(IdSolicitud.Value);

            unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
            unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToString(CodigoCentro.Value);

            if(NumeroDiarioOficial.Text != null && !NumeroDiarioOficial.Text.Equals("")){

                unidadEspacial.numeroDiarioOficial = Convert.ToInt32(NumeroDiarioOficial.Text);
            }

            if (FechaDiarioOficial.Text != null && !FechaDiarioOficial.Text.Equals(""))
            {
                unidadEspacial.fechaDiarioOficial = Convert.ToDateTime(FechaDiarioOficial.Text);
            }

            //if (CapitaniaPuerto.Value != null && !CapitaniaPuerto.Value.Equals(""))
            //{
            //    unidadEspacial.capitaniaDePuerto = new CapitaniaDePuerto();
            //    unidadEspacial.capitaniaDePuerto.idCapitaDePuerto = Convert.ToInt32(CapitaniaPuerto.Value);
            //}

            List<String> listaErroresUnidadEspacial = unidadEspacialValidacion.validaUnidadEspacialModificacion(unidadEspacial);

            if (listaErroresUnidadEspacial != null && listaErroresUnidadEspacial.Count <= 0)
            {
                bool resp = unidadEspacialService.modificaConcesionOriginal(unidadEspacial, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                if (resp)
                {

                    IdUnidadEspacial.Value = Convert.ToString(unidadEspacial.idUnidadEspacial);

                    msgGrillaGral_1.Text = "Se ha modificado la concesión de acuicultura exitosamente.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;

                    try
                    {
                        enviarCorreo.alertaModificacionConcesion(unidadEspacial);

                    }catch(Exception){
                    
                    }
                }
                else
                {
                    msgGrillaGral_1.Text = "No se ha modificado la concesión de acuicultura.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                }
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            }
            else
            {
                foreach (String error in listaErroresUnidadEspacial)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
            }
            UpdatePanelMensaje.Update();
        }

        protected void LimpiarConcesion_Click(object sender, ImageClickEventArgs e)
        {
            NumeroDiarioOficial.Text = "";
            FechaDiarioOficial.Text = "";
        }

        protected void GuardarUnidEspacial_Click(object sender, EventArgs e)
        {
            UnidadEspacialService unidadEspacialService = new UnidadEspacialService();
            UnidadEspacial unidadEspacial = new UnidadEspacial();

            unidadEspacial.idUnidadEspacial = Convert.ToInt32(IdUnidadEspacial.Value);
            unidadEspacial.idSolicitud = Convert.ToInt32(IdSolicitud.Value);

            unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
            unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToString(CodigoCentro.Value);

            if (NumeroDiarioOficial.Text != null && !NumeroDiarioOficial.Text.Equals(""))
            {

                unidadEspacial.numeroDiarioOficial = Convert.ToInt32(NumeroDiarioOficial.Text);
            }

            if (FechaDiarioOficial.Text != null && !FechaDiarioOficial.Text.Equals(""))
            {
                unidadEspacial.fechaDiarioOficial = Convert.ToDateTime(FechaDiarioOficial.Text);
            }

            //if (CapitaniaPuerto.Value != null && !CapitaniaPuerto.Value.Equals(""))
            //{
            //    unidadEspacial.capitaniaDePuerto = new CapitaniaDePuerto();
            //    unidadEspacial.capitaniaDePuerto.idCapitaDePuerto = Convert.ToInt32(CapitaniaPuerto.Value);
            //}

            List<String> listaErroresUnidadEspacial = unidadEspacialValidacion.validaUnidadEspacialModificacion(unidadEspacial);

            if (listaErroresUnidadEspacial != null && listaErroresUnidadEspacial.Count <= 0)
            {
                bool resp = unidadEspacialService.guardaUnidadEspacialModificacion(unidadEspacial, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                if (resp)
                
                {

                    IdUnidadEspacial.Value = Convert.ToString(unidadEspacial.idUnidadEspacial);

                    msgGrillaGral_1.Text = "Se ha guardado los datos de la modificación exitosamente.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                    
                }
                else
                {
                    msgGrillaGral_1.Text = "No se ha guardado los datos de la modificación.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                    
                }
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            }
            else
            {
                foreach (String error in listaErroresUnidadEspacial)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
            }
            UpdatePanelMensaje.Update();
        }
    }
}