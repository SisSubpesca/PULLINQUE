using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;
using Datos.Contantes;
using SubPesca.Utilidades;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;
using Datos.Entidades.Relocalizacion;
using Validaciones.cl.subpesca.rb.relocalizacion;

namespace SubPesca.Solicitudes.RelocalizacionRESA
{
    public partial class preIngresarSolicitudRelocalizacionRESA : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        PermisosService permisosService = new PermisosService();

        EnviarCorreo enviarCorreo = new EnviarCorreo();

        protected void Page_Load(object sender, EventArgs e)
        {

            // PAGE LOAD
            if (!Page.IsPostBack)
            {

                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INGRESAR_SOLICITUD_RELOCALIZACION_RESA }, (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
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


                RelocalizacionRESAService relocalizacionRESAService = new RelocalizacionRESAService();
                TramiteRelocalizacion tramiteRelocalizacionRESA = new TramiteRelocalizacion();
                TramiteRelocalizacionRESAValidacion tramiteRelocalizacionRESAValidacion = new TramiteRelocalizacionRESAValidacion();

                if (!NumPert.Text.Trim().Equals(""))
                {
                    tramiteRelocalizacionRESA.numPert = Convert.ToString(NumPert.Text);
                }
                else
                {
                    Page.Validators.Add(new ValidationError("ValidationSummaryErrores", "Ingrese Nº PERT"));
                }


                if (!FechaRecepcion.Text.Trim().Equals(""))
                {
                    tramiteRelocalizacionRESA.fechaRecepcion = Convert.ToDateTime(FechaRecepcion.Text);
                }
                else
                {
                    Page.Validators.Add(new ValidationError("ValidationSummaryErrores", "Ingrese Fecha de Recepción"));
                }


                if (!FechaIngresoTramite.Text.Trim().Equals(""))
                {
                    tramiteRelocalizacionRESA.fechaIngresoTramite = Convert.ToDateTime(FechaIngresoTramite.Text);
                }
                else
                {
                    Page.Validators.Add(new ValidationError("ValidationSummaryErrores", "Ingrese Fecha de Ingreso a Trámite"));
                }



                if (Page.IsValid)
                {

                    List<String> errores = tramiteRelocalizacionRESAValidacion.validaGenerarSolicitudRelocalizacionRESA(tramiteRelocalizacionRESA);


                    if (errores.Count == 0)
                    {

                        usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];
                        tramiteRelocalizacionRESA.estadoTramite = new ParametroGenerico(rbEstadosGenerales.TRAMITE_RELOCALIZACION_TRAMITE);

                        try
                        {

                            enviarCorreo.alertaIngresoNuevaSolicitud(tramiteRelocalizacionRESA);

                        }
                        catch (Exception)
                        {

                        }

                        Session["nuevoTramiteRelocalizacionRESA"] = tramiteRelocalizacionRESA;
                        Response.Redirect("~/Solicitudes/RelocalizacionRESA/ingresarSolicitudRelocalizacionRESA.aspx");

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