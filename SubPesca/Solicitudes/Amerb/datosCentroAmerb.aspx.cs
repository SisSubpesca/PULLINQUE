using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using Datos.Entidades;
using Datos.Contantes;
using Datos.Utilidades;
using SubPesca.Utilidades;
using Validaciones.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;

namespace SubPesca.Solicitudes.Amerb
{
    public partial class datosCentroAmerb : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        UnidadEspacialValidacion unidadEspacialValidacion = new UnidadEspacialValidacion();
        InformacionAdicionalSolicitudesService informacionAdicionalSolicitudesService = new InformacionAdicionalSolicitudesService();
        InicioSolicitudConcesionValidacion inicioSolicitudConcesionValidacion = new InicioSolicitudConcesionValidacion();
        PermisosService permisosService = new PermisosService();
        DataExternaDA dataExternaDA = new DataExternaDA();
        SolicitudDA solicitudDA = new SolicitudDA();

        EnviarCorreo enviarCorreo = new EnviarCorreo();


        protected void setearModulo()
        {
          
            ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_AMERB;
            ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_AMERB;
            ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_AMERB;
            ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_AMERB;
            ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_AMERB;
            ViewState["solicitudSession"] = paginas.solicitudAmerbSession;

            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.FICHA_AMERB };
                
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


                //BOTON DE INGRESO O MODIFICACION
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {
                    SuperficieSectorCultivo.ReadOnly = false;
                    PorcentajeSectorCultivo.ReadOnly = false;
                    PanelBotonGuardar.Visible = true;

                    Amerb.ReadOnly = false;
                    PanelBotonGuardarAmerb.Visible = true;
                }
                else
                {
                    SuperficieSectorCultivo.ReadOnly = true;
                    PorcentajeSectorCultivo.ReadOnly = true;
                    PanelBotonGuardar.Visible = false;

                    Amerb.ReadOnly = true;
                    PanelBotonGuardarAmerb.Visible = false;
                }
                
                // Inicializamos el formulario
                Initialize_Form();

                PanelNumPert.Visible = true;
                UpdatePanelNumPert.Update();


            }
        }

       
        protected void Initialize_Form()
        {
            SolicitudConcesion solicitudAux = (SolicitudConcesion)Session[paginas.solicitudAmerbSession];
            if (solicitudAux != null && solicitudAux.idSolConcesion > 0)
            {

                IdSolicitud.Value = Convert.ToString(solicitudAux.idSolConcesion);
                NumPert.Text = solicitudAux.numPert;
                FechaIngresoTramite.Text = FechaUtils.formatearFecha(solicitudAux.fechaIngresoTramite);
                FechaRecepcion.Text = FechaUtils.formatearFecha(solicitudAux.fechaRecepcion);


                //INFORMACIÓN QUE SE INGRESO AL CREAR EL TRAMITE
                DatosSolicitudUE datosSolicitudUE = informacionAdicionalSolicitudesService.ObtieneDatosSolicitudUE(solicitudAux.idSolConcesion);

                if (datosSolicitudUE != null && datosSolicitudUE.numeroCI > 0)
                {
                    NumeroCI.Text = Convert.ToString(datosSolicitudUE.numeroCI);
                }

                if (datosSolicitudUE != null && datosSolicitudUE.fechaCI != null && datosSolicitudUE.fechaCI != default(DateTime))
                {
                    FechaCI.Text = FechaUtils.formatearFecha(datosSolicitudUE.fechaCI);
                }

                if (datosSolicitudUE != null && datosSolicitudUE.oficina != null && datosSolicitudUE.oficina.clave != null)
                {
                    Oficina.Text = datosSolicitudUE.oficina.descripcion;
                }


                //ESTA INFORMACIÓN ES LA QUE SE INGRESA EN LA FICHA PARTICULAR DE CADA UNIDAD ESPACIAL
                DetalleDatosSolicitud detalleDatosSolicitud = informacionAdicionalSolicitudesService.ObtieneDetalleDatosSolicitud(solicitudAux.idSolConcesion);
                if (detalleDatosSolicitud != null)
                {
                    ViewState["DatosFicha"] = detalleDatosSolicitud;
                    SuperficieSectorCultivo.Text = Convert.ToString(detalleDatosSolicitud.superficieSectorAmerb);
                    PorcentajeSectorCultivo.Text = Convert.ToString(detalleDatosSolicitud.porcentSectorAmerb);

                    DatosSolicitudUE datosSolicitudUEAux = informacionAdicionalSolicitudesService.ObtieneDatosSolicitudUENew(solicitudAux.idSolConcesion);

                    if (datosSolicitudUEAux.amerbSSP != null && datosSolicitudUEAux.amerbSSP.codigo > 0)
                    {
                        Amerb.Text = datosSolicitudUEAux.amerbSSP.descripcion;
                        this.Amerb_TextChanged(null, null);
                    }
                }
            }
            else
            {
                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
            }

        }





       
        protected void GuardarDatos_Click(object sender, ImageClickEventArgs e)
        {


            DetalleDatosSolicitud detalleDatosSolicitud = (DetalleDatosSolicitud)ViewState["DatosFicha"];
            if (detalleDatosSolicitud == null)
            {
                detalleDatosSolicitud = new DetalleDatosSolicitud();

                SolicitudConcesion solicitudAux = (SolicitudConcesion)Session[paginas.solicitudAmerbSession];
                detalleDatosSolicitud.idSolConcesion = solicitudAux.idSolConcesion;
            }


            if (!SuperficieSectorCultivo.Text.Trim().Equals(""))
            {
                detalleDatosSolicitud.superficieSectorAmerb = Convert.ToSingle(SuperficieSectorCultivo.Text);
            }
            else 
            {
                detalleDatosSolicitud.superficieSectorAmerb = 0;
            }

            if (!PorcentajeSectorCultivo.Text.Trim().Equals(""))
            {
                detalleDatosSolicitud.porcentSectorAmerb = Convert.ToSingle(PorcentajeSectorCultivo.Text);
            }
            else 
            {
                detalleDatosSolicitud.porcentSectorAmerb = 0;
            }


            List<String> erroresValidacion = inicioSolicitudConcesionValidacion.validarFichaAmerb(detalleDatosSolicitud);


            if (erroresValidacion != null && erroresValidacion.Count <= 0)
            {


                bool resp = informacionAdicionalSolicitudesService.GuardarDetalleAmerb(detalleDatosSolicitud);

                if (resp)
                {
                    ViewState["DatosFicha"] = detalleDatosSolicitud;

                    msgGrillaGral_1.Text = "Se ha guardado la información exitosamente.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                    UpdatePanelMensajesSuperior.Update();
                }
                else
                {
                    msgGrillaGral_1.Text = "Ha ocurrido un error al realizar la acción solicitada.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                    UpdatePanelMensajesSuperior.Update();
                }
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            }
            else
            {
                foreach (String error in erroresValidacion)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }

                UpdatePanelMensajesSuperior.Update();
            }
          

        }

        protected void GuardarDatosAmerb_Click(object sender, ImageClickEventArgs e)
        {
            DetalleDatosSolicitud detalleDatosSolicitud = (DetalleDatosSolicitud)ViewState["DatosFicha"];
            if (detalleDatosSolicitud == null)
            {
                detalleDatosSolicitud = new DetalleDatosSolicitud();

                SolicitudConcesion solicitudAux = (SolicitudConcesion)Session[paginas.solicitudAmerbSession];
                detalleDatosSolicitud.idSolConcesion = solicitudAux.idSolConcesion;
            }

            List<String> erroresValidacion = inicioSolicitudConcesionValidacion.validaAmerbPadre(detalleDatosSolicitud);


            if (!COD_SNP.Text.Trim().Equals(""))
            {
                detalleDatosSolicitud.codigo = Convert.ToInt32(COD_SNP.Text);
            }
            else {
                detalleDatosSolicitud.codigo = 0;

                //INTENTA GUARDA UNA AMERB QUE NO EXISTE
                if (!Amerb.Text.Trim().Equals("")) {
                    erroresValidacion.Add("Amerb no encontrada");
                }
                
            }
            
            detalleDatosSolicitud.nombreAmerbPadre = Convert.ToString(Amerb.Text);

            

            if (erroresValidacion != null && erroresValidacion.Count <= 0)
            {
                //Se guarda la relación de la amerb con su padre.
                bool resp = informacionAdicionalSolicitudesService.guardarAmerbPadreSolicitud(detalleDatosSolicitud, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                if (resp)
                {
                    ViewState["DatosFicha"] = detalleDatosSolicitud;

                    msgGrillaGral_1.Text = "Se ha guardado la información exitosamente.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                    UpdatePanelMensajesSuperior.Update();

                    try
                    {
                        enviarCorreo.alertaNombreAmerbYaExistente(detalleDatosSolicitud, NumPert.Text);
                    }
                    catch (Exception)
                    {

                    }

                }
                else
                {
                    msgGrillaGral_1.Text = "Ha ocurrido un error al realizar la acción solicitada.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                    UpdatePanelMensajesSuperior.Update();
                }
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            }
            else
            {
                foreach (String error in erroresValidacion)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }

                UpdatePanelMensajesSuperior.Update();
            }
        }

        protected void Amerb_TextChanged(object sender, EventArgs e)
        {
            if (Amerb.Text != null && !Amerb.Text.Equals(""))
            {
                DataExterna dataExterna = dataExternaDA.Obtener_SSP_DatosAmerb(Amerb.Text);

                if (dataExterna != null)
                {

                    COD_SNP.Text = Convert.ToString(dataExterna.codigo);
                    CDU01.Text = dataExterna.cdu01;
                    FCDU01.Text = dataExterna.fcdu01;
                    Region.Text = dataExterna.region;
                    Estado.Text = dataExterna.estado;
                    CDU02.Text = dataExterna.cdu02;
                    FCDU02.Text = dataExterna.fcdu02;
                    CDU03.Text = dataExterna.cdu03;
                    FCDU03.Text = dataExterna.fcdu03;
                    Superficie_Hectareas.Text = dataExterna.superficie;
                    UltimoPLazo.Text = Convert.ToString(dataExterna.ultimoPlazo);
                    Informe.Text = dataExterna.informe;

                    PanelAmerb.Visible = true;
                    UpdatePanelAmerb.Update();
                }
            }
            else {
                COD_SNP.Text = "";
                CDU01.Text = "";
                FCDU01.Text = "";
                Region.Text = "";
                Estado.Text = "";
                CDU02.Text = "";
                FCDU02.Text = "";
                CDU03.Text = "";
                FCDU03.Text = "";
                Superficie_Hectareas.Text = "";
                UltimoPLazo.Text = "";
                Informe.Text = "";

                PanelAmerb.Visible = false;
                UpdatePanelAmerb.Update();
            }
        }

    }
}