using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;
using Datos.Entidades.Relocalizacion;
using SubPesca.Solicitudes.Registrar;
using Validaciones.cl.subpesca.rb.relocalizacion;
using Datos.Entidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;

namespace SubPesca.Solicitudes.Relocalizacion
{
    public partial class preIngresarSolicitudRelocalizacion : System.Web.UI.Page
    {

        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        PermisosService permisosService = new PermisosService();

        EnviarCorreo enviarCorreo = new EnviarCorreo();

        protected void Page_Load(object sender, EventArgs e)
        {
            
             // PAGE LOAD
            if (!Page.IsPostBack)
            {

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_RELOCALIZACION }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
                {

                    NumPert.ReadOnly = false;
                    FechaRecepcion.ReadOnly = false;
                    FechaIngresoTramite.ReadOnly = false;

                    PanelBotonGuardar.Visible = true;
                }
                else
                {

                    NumPert.ReadOnly = true;
                    FechaRecepcion.ReadOnly = true;
                    FechaIngresoTramite.ReadOnly = true;

                    PanelBotonGuardar.Visible = false;
                }

            }
        }



        protected void Generar_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {


                RelocalizacionService relocalizacionService = new RelocalizacionService();
                TramiteRelocalizacion tramiteRelocalizacion = new TramiteRelocalizacion();
                TramiteRelocalizacionValidacion tramiteRelocalizacionValidacion = new TramiteRelocalizacionValidacion();


                if (!NumPert.Text.Trim().Equals(""))
                {
                    tramiteRelocalizacion.numPert = Convert.ToString(NumPert.Text);
                }
                else {
                    Page.Validators.Add(new ValidationError("ValidationSummaryErrores", "Ingrese Nº PERT"));
                }


                if (!FechaRecepcion.Text.Trim().Equals(""))
                {
                    tramiteRelocalizacion.fechaRecepcion = Convert.ToDateTime(FechaRecepcion.Text);
                }
                else {
                    Page.Validators.Add(new ValidationError("ValidationSummaryErrores", "Ingrese Fecha de Recepción"));
                }


                if (!FechaIngresoTramite.Text.Trim().Equals(""))
                {
                    tramiteRelocalizacion.fechaIngresoTramite = Convert.ToDateTime(FechaIngresoTramite.Text);
                }
                else {
                    Page.Validators.Add(new ValidationError("ValidationSummaryErrores", "Ingrese Fecha de Ingreso a Trámite"));
                }


       
                if (Page.IsValid)
                {

                    List<String> errores = tramiteRelocalizacionValidacion.validaGenerarSolicitudRelocalizacion(tramiteRelocalizacion);


                    if (errores.Count == 0)
                    {

                        usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];
                        tramiteRelocalizacion.estadoTramite = new ParametroGenerico(rbEstadosGenerales.TRAMITE_RELOCALIZACION_TRAMITE);

                        try {

                            enviarCorreo.alertaIngresoNuevaSolicitud(tramiteRelocalizacion);
                            
                        }catch(Exception){
                        
                        }

                        Session["nuevoTramiteRelocalizacion"] = tramiteRelocalizacion;
                        Response.Redirect("~/Solicitudes/Relocalizacion/ingresarSolicitudRelocalizacion.aspx");

                    }
                    else
                    {
                        foreach (String error in errores)
                        {
                            Page.Validators.Add(new ValidationError("ValidationSummaryErrores", error));
                            
                        }
                    }
                }


            }
        }


    }
}