using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Entidades;
using Datos.Contantes;

namespace SubPesca.Administrador.includes
{
    public partial class migas : System.Web.UI.UserControl
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        MatrizSucursalDA matrizSucursalDA = new MatrizSucursalDA();
        RepLegalDA repLegalDA = new RepLegalDA();
        OperadorDA operadorDA = new OperadorDA();

        protected void Page_Load(object sender, EventArgs e)
        {

            
            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

            if (usuario_logeado == null)
            {
                Response.Redirect("~/ingreso.aspx");
            }

            Bienvenido.Text = " | Bienvenido, " + usuario_logeado.nombre + " " + usuario_logeado.apellidos;



            string segmento = "";
            string modulo = "";
            string nombre_archivo = "";
            string ext = "";
            int i = 0;
            int pos = 0;
            foreach (string S in HttpContext.Current.Request.Url.Segments)
            {
                segmento = HttpContext.Current.Request.Url.Segments[i];
                pos = segmento.LastIndexOf(".");
                if (pos > 0)
                {
                    ext = segmento.Substring(pos);
                    ext = ext.Replace(".", "");
                    if (ext == "aspx")
                    {
                        nombre_archivo = segmento;
                        break;
                    };
                };
                i++;
            };

            modulo = HttpContext.Current.Request.Url.Segments[i - 1];
            modulo = modulo.Replace("/", "");
            int acc = 0;
            
            switch (nombre_archivo)
            {


                #region INICIO
                
                case "principal.aspx":
                    Content_Migas.Controls.Add(Crea_Label("Inicio", ""));
                    break;

                #endregion


                #region VISACIONES MASIVAS

                case "InicioVisacion.aspx":
                case "VisarFirmar.aspx":
                case "FirmaMasiva.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("", nombre_archivo));
                    break;

                #endregion


                #region INICIO SOLICITUD (SALVO RELOCALIZACION)

                case "inicioSolicitudConcesion.aspx":
                case "inicioSolicitudAcopio.aspx":
                case "inicioSolicitudExperimentalesConcesion.aspx":
                case "inicioSolicitudFaenamiento.aspx":
                case "inicioSolicitudECMPO.aspx":
                case "inicioSolicitudExperimentalesAmerb.aspx":
                case "inicioSolicitudAmerb.aspx":
                case "inicioSolicitudColector.aspx":
                case "ingresarSolicitudModificacion.aspx":
                case "ingresarSolicitudModificacionCentroAcopio.aspx":
                case "ingresarSolicitudModificacionCentroFaenamiento.aspx":
                case "ingresarSolicitudModificacionECMPO.aspx":
                case "ingresarSolicitudModificacionAmerb.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Nueva Solicitud", nombre_archivo));
                    break;

                #endregion


                #region RESOLUCIONES

                case "administrarResoluciones.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Administrar Resoluciones", nombre_archivo));
                    break;
                case "busquedaResoluciones.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Busqueda de Solicitudes", nombre_archivo));
                    break;
                case "ingresarResoluciones.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Nueva Resolucion", nombre_archivo));
                    break;
                case "verResolucion.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Administrar Resoluciones", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Ver Resolucion", nombre_archivo));
                    break;

                #endregion 


                #region RESUMEN DE ESTADOS Y REQUERIMIENTOS CON PLAZOS

                case "resumenEstados.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Solicitudes", nombre_archivo));
                    break;
                case "detalleEstado.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Resumen de Estados", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Detalle del Estado", nombre_archivo));
                    break;
                case "plazosRequerimientos.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Solicitudes", nombre_archivo));
                    break;
                case "detallePlazo.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Requerimientos con Plazos", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Solicitudes", nombre_archivo));
                    break;
                case "detalleSolicitud.aspx":
                case "logEstadosSolicitud.aspx":
                    // Se recibe el switch para determinar que página invocó a ésta página
                    int backpage = 0;
                    try
                    {
                        if (Request.QueryString["bp"] != null)
                        {
                            backpage = Convert.ToInt32(Request.QueryString["bp"]);
                        };
                    }
                    catch
                    {
                        Response.Redirect("~/Administrador/Reportes/resumenEstados.aspx");
                    };
                    switch (backpage)
                    {
                        case 1:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Resumen de Estados", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Detalle del Estado", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Detalle de la Solicitud", nombre_archivo));
                            break;
                        case 9:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Requerimientos con Plazos", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Detalle del Requerimiento con Plazo", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Detalle de la Solicitud", nombre_archivo));
                            break;
                        case 10:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Detalle del Indicador", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Detalle de la Solicitud", nombre_archivo));
                            break;
                        case 13:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Relocalización Crea", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Detalle del Indicador Relocalización Crea", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Detalle de la Solicitud", nombre_archivo));
                            break;
                        case 12:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Relocalización Fusiona", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Detalle del Indicador Relocalización Fusión", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Detalle de la Solicitud", nombre_archivo));
                            break;
                        case 11:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Relocalización Sector 0", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Detalle del Indicador Relocalización Sector 0", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Detalle de la Solicitud", nombre_archivo));
                            break;
                        case 18:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Modificación Ampliación", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Detalle del Indicador Modificación Ampliación", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Detalle de la Solicitud", nombre_archivo));
                            break;
                        case 15:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Modificación Reducción", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Detalle del Indicador Modificación Reducción", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Detalle de la Solicitud", nombre_archivo));
                            break;
                        case 17:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Modificación de Especie", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Detalle del Indicador Modificación de Especie", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Detalle de la Solicitud", nombre_archivo));
                            break;
                        case 16:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Modificación de Proyecto Técnico", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Detalle del Indicador Modificación de Proyecto Técnico", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Detalle de la Solicitud", nombre_archivo));
                            break;
                        case 14:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Modificación Regularización", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Detalle del Indicador Modificación Regularización", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Detalle de la Solicitud", nombre_archivo));
                            break;
                        case 22:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Solicitud Centro de Acopio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Detalle del Indicador Solicitud de Acopio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Detalle de la Solicitud", nombre_archivo));
                            break;
                        case 19:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Solicitud Faenamiento", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Detalle del Indicador Solicitud Faenamiento", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Detalle de la Solicitud", nombre_archivo));
                            break;
                        case 21:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Solicitud Amerb", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Detalle del Indicador Solicitud Amerb", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Detalle de la Solicitud", nombre_archivo));
                            break;
                        case 20:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Solicitud Colectores de Semilla", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Detalle del Indicador Solicitud Colectores de Semilla", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Detalle de la Solicitud", nombre_archivo));
                            break;

                    };

                    break;

                #endregion


                #region USUARIOS

                case "listUsuarios.aspx":
                case "adminRolesPrivAplicacion.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Usuarios", nombre_archivo));
                    break;
                case "formUsuario.aspx":
                case "adminUsuarioRolAplicacion.aspx":
                case "adminUsuarioPertAplicacion.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Administración de Usuarios", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Usuarios", nombre_archivo));
                    break;

                #endregion


                #region REPORTES

                //REPORTES SOLICITUDES
                case "reportesSolicitudesAcopio.aspx":
                case "reportesSolicitudesAcuicultura.aspx":
                case "reportesSolicitudesAMERB.aspx":
                case "reportesSolicitudesECMPO.aspx":
                case "reportesSolicitudesFaenamiento.aspx":
                case "reportesSolicitudesSemillas.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Reportes", nombre_archivo));
                    break;

                //REPORTES UNIDADES ESPACIALES
                case "reportesConcesion.aspx":
                case "ReportesExperimentalConcesion.aspx":
                case "reportesAcuiculturaAmerb.aspx":
                case "ReportesExperimentalAmerb.aspx":
                case "reportesCentroFaenamiento.aspx":
                case "reportesCentroAcopio.aspx":
                case "reportesColectoresSemillas.aspx":
                case "reportesECMPO.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Reportes", nombre_archivo));
                    break;

                #endregion


                #region  CIERRE FORZADO

                case "GenerarCierreForzado.aspx":
                case "GenerarCierreForzadoAcopio.aspx":
                case "GenerarCierreForzadoFaenamiento.aspx":
                case "GenerarCierreForzadoAmerb.aspx":
                case "GenerarCierreForzadoColectores.aspx":
                case "GenerarCierreForzadoECMPO.aspx":
                case "GenerarCierreForzadoExperimentalesAmerb.aspx":
                case "GenerarCierreForzadoExperimentalesConcesion.aspx":
                case "GenerarCierreForzadoModificacionAmerb.aspx":
                case "GenerarCierreForzadoModificacionCentroAcopio.aspx":
                case "GenerarCierreForzadoModificacionCentroFaenamiento.aspx":
                case "GenerarCierreForzadoModificacionConcesion.aspx":
                case "GenerarCierreForzadoModificacionECMPO.aspx":
                case "GenerarCierreForzadoRelocalizacionLey.aspx":
                case "GenerarCierreForzadoRelocalizacionRESA.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Generar Cierres Forzados", nombre_archivo));
                    break;


                case "administrarCierreForzado.aspx":
                case "administrarCierreForzadoAcopio.aspx":
                case "administrarCierreForzadoFaenamiento.aspx":
                case "administrarCierreForzadoAmerb.aspx":
                case "administrarCierreForzadoColectores.aspx":
                case "administrarCierreForzadoECMPO.aspx":
                case "administrarCierreForzadoExperimentalesAmerb.aspx":
                case "administrarCierreForzadoExperimentalesConcesion.aspx":
                case "administrarCierreForzadoModificacionAmerb.aspx":
                case "administrarCierreForzadoModificacionCentroAcopio.aspx":
                case "administrarCierreForzadoModificacionCentroFaenamiento.aspx":
                case "administrarCierreForzadoModificacionConcesion.aspx":
                case "administrarCierreForzadoModificacionECMPO.aspx":
                case "administrarCierreForzadoRelocalizacionLey.aspx":
                case "administrarCierreForzadoRelocalizacionRESA.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Administrar Cierres Forzados", nombre_archivo));
                    break;


                case "CierreForzadoResolucion.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Administrar Cierres Forzados Solicitud Concesión", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Administrar Cierres Forzados ", nombre_archivo));
                    break;

                case "CierreForzadoResolucionExperimentalesConcesion.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Administrar Cierres Forzados Experimentales de Concesión", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Administrar Cierres Forzados ", nombre_archivo));
                    break;

                case "CierreForzadoResolucionModificacionConcesion.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Administrar Cierres Forzados Modificación Concesión", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Administrar Cierres Forzados ", nombre_archivo));
                    break;

                case "CierreForzadoResolucionRelocalizacionLey.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Administrar Cierres Forzados Relocalización LEY", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Administrar Cierres Forzados ", nombre_archivo));
                    break;

                case "CierreForzadoResolucionRelocalizacionRESA.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Administrar Cierres Forzados Relocalización RESA", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Administrar Cierres Forzados ", nombre_archivo));
                    break;


                case "CierreForzadoResolucionAcopio.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Administrar Cierres Forzados Solicitudes de Centro de Acopio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Administrar Cierres Forzados ", nombre_archivo));
                    break;

                case "CierreForzadoResolucionModificacionCentroAcopio.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Administrar Cierres Forzados Modificación Acopio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Administrar Cierres Forzados ", nombre_archivo));
                    break;


                case "CierreForzadoResolucionFaenamiento.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Administrar Cierres Forzados Solicitudes de Centro de Faenamiento", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Administrar Cierres Forzados ", nombre_archivo));
                    break;

                case "CierreForzadoResolucionModificacionCentroFaenamiento.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Administrar Cierres Forzados Modificación Faenamiento", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Administrar Cierres Forzados ", nombre_archivo));
                    break;


                case "CierreForzadoResolucionECMPO.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Administrar Cierres Forzados de ECMPO", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Administrar Cierres Forzados ", nombre_archivo));
                    break;
                case "CierreForzadoResolucionModificacionECMPO.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Administrar Cierres Forzados Modificación de Acuicultura en ECMPO", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Administrar Cierres Forzados ", nombre_archivo));
                    break;


                case "CierreForzadoResolucionAmerb.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Administrar Cierres Forzados Solicitudes de Acuicultura Amerb", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Administrar Cierres Forzados ", nombre_archivo));
                    break;
                case "CierreForzadoResolucionExperimentalesAmerb.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Administrar Cierres Forzados de Experimentales AMERB", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Administrar Cierres Forzados ", nombre_archivo));
                    break;
                case "CierreForzadoResolucionModificacionAmerb.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Administrar Cierres Forzados Modificación Amerb", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Administrar Cierres Forzados ", nombre_archivo));
                    break;


                case "CierreForzadoResolucionColectores.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Administrar Cierres Forzados Solicitudes de Colectores de Semilla", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Administrar Cierres Forzados ", nombre_archivo));
                    break;

              


                

                #endregion 


                #region ADMINISTRADOR SOLICITUDES
  
                case "administrarSolicitudConcesion.aspx":
                case "administrarSolicitudModificacion.aspx":
                case "administrarSolicitudExperimentalesConcesion.aspx":
                case "administrarSolicitudRelocalizacion.aspx":
                case "administrarSolicitudRelocalizacionRESA.aspx":
                case "administrarSolicitudAcopio.aspx":
                case "administrarSolicitudModificacionCentroAcopio.aspx":
                case "administrarSolicitudECMPO.aspx":
                case "administrarSolicitudModificacionECMPO.aspx": 
                case "administrarSolicitudAmerb.aspx":
                case "administrarSolicitudModificacionAmerb.aspx":
                case "administrarSolicitudExperimentalesAmerb.aspx":
                case "administrarSolicitudFaenamiento.aspx":
                case "administrarSolicitudModificacionCentroFaenamiento.aspx":
                case "administrarSolicitudColector.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Administrar Solicitudes", nombre_archivo));
                    break;

                #endregion


                #region ADMINISTRAR UNIDADES ESPACIALES

                case "administrarConcesiones.aspx":
                case "administrarCentroAmerb.aspx":
                case "administrarCentroECMPO.aspx":
                case "administrarCentroFaenamiento.aspx":
                case "administrarCentroAcopio.aspx":
                case "administrarColectorSemillas.aspx":
                case "administrarCentroExperimentalesConcesion.aspx":  //REVISAR
                case "administrarCentroExperimentalesAmerb.aspx": //REVISAR
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Administrar Concesiones", nombre_archivo));
                    break;

                #endregion




                #region REDEFINIR TRAMITES DE MOD

                case "redefinirTramiteModificacion.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Administrar Solicitudes de Modificación de Concesión", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Redefinir Trámite", nombre_archivo));
                    break;

                case "redefinirTramiteModificacionAmerb.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Administrar Solicitudes de Modificación de Amerb", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Redefinir Trámite", nombre_archivo));
                    break;


                case "redefinirTramiteModificacionAcopio.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Administrar Solicitudes de Modificación de Centro de Acopio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Redefinir Trámite", nombre_archivo));
                    break;

                case "redefinirTramiteModificacionFaenamiento.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Administrar Solicitudes de Modificación de Centro de Faenamiento", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Redefinir Trámite", nombre_archivo));
                    break;


                case "redefinirTramiteModificacionECMPO.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Administrar Solicitudes de Modificación de ECMPO", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Redefinir Trámite", nombre_archivo));
                    break;

                #endregion


                #region ERRORES Y ALERTAS

                case "erroresTramiteModificacion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Administrar Solicitudes de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Listado Errores", nombre_archivo));
                        break;
                case "erroresTramiteRelocalizacion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Administrar Trámites de Relocalización", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Listado Errores", nombre_archivo));
                        break;
                case "erroresTramiteRelocalizacionRESA.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Administrar Trámites de Relocalización RESA", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Listado Errores", nombre_archivo));
                        break;
                case "erroresTramiteModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Administrar Solicitudes de Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Listado Alertas", nombre_archivo));
                        break;
                case "erroresTramiteModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Administrar Solicitudes de Modificación de Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Listado Alertas", nombre_archivo));
                        break;
                case "alertasTramiteRelocalizacion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Administrar Trámites de Relocalización", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Listado Alertas", nombre_archivo));
                        break;

                #endregion


                #region TITULAR

                case "detalleTitular.aspx":

                    backpage = 0;
                    try
                    {
                        if (Request.QueryString["bp"] != null)
                        {
                            backpage = Convert.ToInt32(Request.QueryString["bp"]);
                        };
                    }
                    catch
                    {};

                    switch (backpage)
                    {
                        case 1:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Identificación del Titular", "identificacionTitularConcesion.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Ver Titular", nombre_archivo));
                            break;
                        case 2:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Solicitud Concesión de Acuicultura", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Identificación del Solicitante", "identificacionSolicitante.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Ver Titular", nombre_archivo));
                            break;
                        case 4:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Administrar Titulares", "administrarTitulares.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Ver Titular", nombre_archivo));
                            break;

                        case 7:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Solicitud Colectores de Semillas", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Identificación del Solicitante", "identificacionSolicitanteColector.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Ver Titular", nombre_archivo));
                            break;

                        case 8:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Faenamiento", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Identificación del Solicitante", "identificacionSolicitanteFaenamiento.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Ver Titular", nombre_archivo));
                            break;

                        case 9:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link(" Solicitud Experimentales en AMERB", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Identificación del Solicitante", "identificacionTitularExperimentalesAmerb.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Ver Titular", nombre_archivo));
                            break;

                        case 10:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Solicitudes de Experimentales Concesión", "ingresarDocumentoExperimentalesConcesion.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Identificación del Solicitante", "identificacionTitularExperimentalesConcesion.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Ver Titular", nombre_archivo));
                            break;

                        case 11:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", "ingresarDocumentoECMPO.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Identificación del Solicitante", "identificacionTitularECMPO.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Ver Titular", nombre_archivo));
                            break;

                        case 29:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Centro Acuicultura ECMPO", "resumenECMPO.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Titular", "titularECMPO.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Ver Titular", nombre_archivo));
                            break;


                        case 30:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Centro de Acopio", "resumenAcopio.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Titular", "titularAcopio.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Ver Titular", nombre_archivo));
                            break;

                        case 31:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Centro en Amerb", "resumenConcesion.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Titular", "titularConcesion.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Ver Titular", nombre_archivo));
                            break;

                        case 32:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Colector de Semillas", "resumenColector.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Titular", "titularColector.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Ver Titular", nombre_archivo));
                            break;

                        case 33:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Concesión de Acuicultura ", "resumenConcesion.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Titular", "titularConcesion.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Ver Titular", nombre_archivo));
                            break;

                        case 34:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Centro de Faenamiento", "resumenFaenamiento.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Titular", "titularFaenamiento.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Ver Titular", nombre_archivo));
                            break;

                    };
                    break;


                #endregion


                #region INDICADORES

                case "indicador.aspx":

                    int idSolicitud = 0;
                    int idSubTipo = 0;
                    try
                    {
                        if (Request.QueryString["id_Solicitud"] != null)
                        {
                            idSolicitud = Convert.ToInt32(Request.QueryString["id_Solicitud"]);
                            idSubTipo = Convert.ToInt32(Request.QueryString["id_Subtipo"]);
                        };
                    }
                    catch
                    {};

                    switch (idSolicitud)
                    {
                        case 88:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Solicitudes Concesión", "resumenIndicadores.aspx"));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(new LiteralControl("Indicador Solicitud Concesión"));
                            break;

                        case 536:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Experimentales Concesión", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(new LiteralControl("Indicador Solicitud Experimental Concesión"));
                            break;

                        case 89: // Trámite de Modificación
                            if (idSubTipo == rbTipo.MOD_CONCESION_AMPLIA_SUPERFICIE)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores de Ampliación", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Ampliación"));
  
                            }
                            else if(idSubTipo == rbTipo.MOD_CONCESION_REDUCE_SUPERFICIE)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores de Reducción", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Reducción"));
                            }
                            else if (idSubTipo == rbTipo.MOD_CONCESION_ESPECIE)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores de Especie", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Especie"));
                            }
                            else if (idSubTipo == rbTipo.MOD_CONCESION_PT)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores de Proyecto Técnico",""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Proyecto Técnico"));
                            }
                            else if (idSubTipo == rbTipo.MOD_CONCESION_REGULARIZACION)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores de Regularización", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Regularización"));
                            }
                            break;

                        case 95: //Trámite Sector Relocalización
                            if (idSubTipo == rbTipo.RELOCALIZACION_SECTOR_CERO) 
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Relocalización Sector 0", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador Relocalización Sector 0"));
                            }
                            else if (idSubTipo == rbTipo.RELOCALIZACION_CREA)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Relocalización Crea", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador Relocalización Crea"));
                            }
                            else if (idSubTipo == rbTipo.RELOCALIZACION_FUSIONA) 
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Relocalización Fusión", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador Relocalización Fusión"));
                            } 

                            break;

                        case 624: //Trámite Sector Relocalización RESA
                            if (idSubTipo == rbTipo.RELOCALIZACION_CREA_RESA)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores RESA Crea", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Relocalización RESA Crea"));
                            }

                            else if (idSubTipo == rbTipo.RELOCALIZACION_SECTOR_CERO_RESA)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores RESA Sector 0", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Relocalización RESA Sector Cero"));
                            }

                            else if (idSubTipo == rbTipo.RELOCALIZACION_FUSIONA_RESA)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores RESA Fusión", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Relocalización RESA Fusión"));
                            }
                            break;


                        case 124:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Solicitudes Centro de Faenamiento", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(new LiteralControl("Indicador de Centro de Faenamiento"));
                            break;

                        case 546: // Trámite de Modificación Centro de Faenamiento
                            if (idSubTipo == rbTipo.MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores de Faenamiento Ampliación", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Ampliación"));

                            }
                            else if (idSubTipo == rbTipo.MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores de Faenamiento Reducción", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Reducción"));
                            }
                            else if (idSubTipo == rbTipo.MOD_CENTRO_FAENAMIENTO_RENOVACION)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores de Faenamiento Renovación", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Renovación"));
                            }
                            else if (idSubTipo == rbTipo.MOD_CENTRO_FAENAMIENTO_PT_ESPECIE)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores de Faenamiento Proyecto Técnico/Especie", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Proyecto Técnico/Especie"));
                            }
                            else if (idSubTipo == rbTipo.MOD_CENTRO_FAENAMIENTO_REGULARIZACION)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores de Faenamiento Regularización", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Regularización"));
                            }
                            break;
                        
                        case 121:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Solicitud Centro de Acopio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(new LiteralControl("Indicador de Solicitud Centro de Acopio"));
                            break;

                        case 552: // Trámite de Modificación Centro de Acopio
                            if (idSubTipo == rbTipo.MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores de Acopio Ampliación", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Ampliación"));

                            }
                            else if (idSubTipo == rbTipo.MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores de Acopio Reducción", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Reducción"));
                            }
                            else if (idSubTipo == rbTipo.MOD_CENTRO_FAENAMIENTO_RENOVACION)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores de Acopio Renovación", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Renovación"));
                            }
                            else if (idSubTipo == rbTipo.MOD_CENTRO_FAENAMIENTO_PT_ESPECIE)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores de Acopio Proyecto Técnico/Especie", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Proyecto Técnico/Especie"));
                            }
                            else if (idSubTipo == rbTipo.MOD_CENTRO_FAENAMIENTO_REGULARIZACION)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores de Acopio Regularización", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Regularización"));
                            }
                            break;

                        
                        case 537:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Solicitud Acuicultura ECMPO", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(new LiteralControl("Indicador de Solicitud Acuicultura ECMPO"));
                            break;

                        case 564: //Trámite de Modificación ECMPO
                            if (idSubTipo == rbTipo.MOD_ECMPO_AMPLIA_SUPERFICIE)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores de ECMPO Ampliación", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Ampliación"));

                            }
                            else if (idSubTipo == rbTipo.MOD_ECMPO_REDUCE_SUPERFICIE)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores de ECMPO Reducción", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Reducción"));
                            }
                            else if (idSubTipo == rbTipo.MOD_ECMPO_ESPECIE)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores de ECMPO Especie", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Especie"));
                            }
                            else if (idSubTipo == rbTipo.MOD_ECMPO_PT)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores de ECMPO Proyecto Técnico", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Proyecto Técnico"));
                            }
                            else if (idSubTipo == rbTipo.MOD_ECMPO_REGULARIZACION)
                            {
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores de ECMPO Regularización", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(new LiteralControl("Indicador de Regularización"));
                            }
                            break;
                        
                        case 122:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Solicitud Acuicultura AMERB", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(new LiteralControl("Indicador de Solicitud AMERB"));
                            break;

                        case 535:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Solicitud Experimentales AMERB", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(new LiteralControl("Indicador de Experimentales AMERB"));
                            break;

                        case 123:
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Solicitud Colectores de Semillas", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(new LiteralControl("Indicador de Solicitud Colectores de Semillas"));
                            break;
                            

                    };
                    break;


                //INDICADORES
                case "resumenIndicadores.aspx":
                case "resumenIndicadoresRelocalizacionCREA.aspx":
                case "resumenIndicadoresLeyCrea.aspx":
                case "resumenIndicadoresRelocalizacionFusiona.aspx":
                case "resumenIndicadoresLeyFusion.aspx":
                case "resumenIndicadoresRESACrea.aspx":
                case "resumenIndicadoresRESACero.aspx":
                case "resumenIndicadoresRESAFusion.aspx":
                case "resumenIndicadoresECMPO.aspx":
                case "resumenIndicadoresSemillas.aspx":
                case "resumenIndicadoresExperimentalAmerb.aspx":
                case "resumenIndicadoresRelocalizacionSectorCero.aspx":
                case "resumenIndicadoresLeyCero.aspx":
                case "resumenIndicadoresModAmpliacion.aspx":
                case "resumenIndicadoresModReduccion.aspx":
                case "resumenIndicadoresModEspecie.aspx":
                case "resumenIndicadoresModProyecto.aspx":
                case "resumenIndicadoresModProyTecnico.aspx":
                case "resumenIndicadoresModRegularizacion.aspx":
                case "resumenIndicadoresAcopio.aspx":
                case "resumenIndicadoresFaenamiento.aspx":
                case "resumenIndicadoresAmerb.aspx":
                case "resumenIndicadoresColectores.aspx":
                case "resumenIndicadoresExpConcesion.aspx":
                case "resumenIndicadoresECMPOModAmpliacion.aspx":
                case "resumenIndicadoresECMPOModReduccion.aspx":
                case "resumenIndicadoresECMPOModEspecie.aspx":
                case "resumenIndicadoresECMPOModProyTecnico.aspx":
                case "resumenIndicadoresECMPOModRegularizacion.aspx":
                case "resumenIndicadoresAMERBModAmpliacion.aspx":
                case "resumenIndicadoresAMERBModReduccion.aspx":
                case "resumenIndicadoresAMERBModEspecie.aspx":
                case "resumenIndicadoresAMERBModProyTecnico.aspx":
                case "resumenIndicadoresAMERBModRegularizacion.aspx":
                case "resumenIndicadoresAcopioModAmpliacion.aspx":
                case "resumenIndicadoresAcopioModEspecie.aspx":
                case "resumenIndicadoresAcopioModProyTecnico.aspx":
                case "resumenIndicadoresAcopioModReduccion.aspx":
                case "resumenIndicadoresAcopioModRegularizacion.aspx":
                case "resumenIndicadoresFaenamientoModAmpliacion.aspx":
                case "resumenIndicadoresFaenamientoModEspecie.aspx":
                case "resumenIndicadoresFaenamientoModProyTecnico.aspx":
                case "resumenIndicadoresFaenamientoModReduccion.aspx":
                case "resumenIndicadoresFaenamientoModRegularizacion.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Resumen de Indicadores", nombre_archivo));
                    break;


                case "detalleIndicador.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Detalle del Indicador", nombre_archivo));
                    break;
                case "detalleIndicadorRelocalizacionCrea.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Relocalización Crea", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Detalle del Indicador", nombre_archivo));
                    break;
                case "detalleIndicadorRelocalizacionFusiona.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Relocalización Fusiona", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Detalle del Indicador", nombre_archivo));
                    break;
                case "detalleIndicadorRelocalizacionSectorCero.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Relocalización Sector 0", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Detalle del Indicador", nombre_archivo));
                    break;
                case "detalleIndicadorModAmpliacion.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Modificación Ampliación", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Detalle del Indicador", nombre_archivo));
                    break;
                case "detalleIndicadorModReduccion.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Modificación Reducción", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Detalle del Indicador", nombre_archivo));
                    break;
                case "detalleIndicadorModEspecie.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Modificación de Especie", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Detalle del Indicador", nombre_archivo));
                    break;
                case "detalleIndicadorModProyTecnico.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Modificación de Proyecto Técnico", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Detalle del Indicador", nombre_archivo));
                    break;
                case "detalleIndicadorModRegularizacion.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Modificación Regularización", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Detalle del Indicador", nombre_archivo));
                    break;
                case "detalleIndicadorAcopio.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Solicitud de Acopio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Detalle del Indicador", nombre_archivo));
                    break;
                case "detalleIndicadorFaenamiento.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Solicitud Faenamiento", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Detalle del Indicador", nombre_archivo));
                    break;
                case "detalleIndicadorAmerb.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Solicitud Amerb", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Detalle del Indicador", nombre_archivo));
                    break;
                case "detalleIndicadorColectores.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Resumen de Indicadores Solicitud Colectores de Semilla", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Detalle del Indicador", nombre_archivo));
                    break;



                #endregion


                #region SOLICITUD CONCESION  (Y MODIFICACION)


                case "pestanaInformeDeCartografia.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Concesión de Acuicultura", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("IT U.O.T",""));
                        break;
                case "pestanaInformeDeCartografiaModCon.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("IT U.O.T", ""));
                        break;
                case "pestanaInspeccionTerreno.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Concesión de Acuicultura", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Inspección en Terreno", ""));
                        break;
                case "pestanaInspeccionTerrenoModCon.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Inspección en Terreno", ""));
                        break;
                case "pestanaBancoNatural.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Concesión de Acuicultura", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Banco Natural", ""));
                        break;
                case "pestanaBancoNaturalModCon.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Banco Natural", ""));
                        break;
                case "pestanaDifusionBancoNatural.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Concesión de Acuicultura", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difusión Banco Natural", ""));
                        break;
                case "pestanaDifusionBancoNaturalModCon.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difusión Banco Natural", ""));
                        break;
                case "pestanaDifrol.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Concesión de Acuicultura", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difrol", ""));
                        break;
                case "pestanaDifrolModCon.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difrol", ""));
                        break;
                case "pestanaInformeAmbiental.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Concesión de Acuicultura", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe Ambiental", ""));
                        break;
                case "pestanaInformeAmbientalModCon.aspx":    
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe Ambiental", ""));
                        break;
                case "pestanaAntecedentesComplementarios.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Concesión de Acuicultura", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes Complementarios", ""));
                        break;
                case "pestanaAntecedentesComplementariosModCon.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes Complementarios", ""));
                        break;
                case "pestanaPlanos.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Concesión de Acuicultura", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Planos", ""));
                        break;
                case "pestanaPlanosModCon.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Planos", ""));
                        break;
                case "pestanaInformeDAC.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Concesión de Acuicultura", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe DAC", ""));
                        break;
                case "pestanaInformeDACModCon.aspx" :
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe DAC", ""));
                        break;
                case "pestanaResolucionSSP.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Concesión de Acuicultura", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolución SSP", ""));
                        break;
                case "pestanaResolucionSSPModCon.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolución SSP", ""));
                        break;
                case "pestanaResolucionSSFFAA.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Concesión de Acuicultura", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolución SSFFAA", ""));
                        break;
                case "pestanaResolucionSSFFAAModCon.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolución SSFFAA", ""));
                        break;
                case "pestanaInformeDeCartografiaExperimentalesConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("IT UOT", ""));
                        break;
                case "pestanaInspeccionTerrenoExperimentalesConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Inspección Terreno", ""));
                        break;
                case "pestanaBancoNaturalExperimentalesConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Banco Natural", ""));
                        break;
                case "pestanaDifusionBancoNaturalExperimentalesConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difusión Banco Natural", ""));
                        break;
                case "pestanaDifrolExperimentalesConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difrol", ""));
                        break;
                case "pestanaInformeAmbientalExperimentalesConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe Ambiental", ""));
                        break;
                case "pestanaAntecedentesComplementariosExperimentalesConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes Complementarios", ""));
                        break;
                case "pestanaPlanosExperimentalesConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Planos", ""));
                        break;
                case "pestanaInformeDACExperimentalesConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe DAc", ""));
                        break;
                case "pestanaResolucionSSPExperimentalesConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolusión SSP", ""));
                        break;
                case "pestanaResolucionSSFFAAExperimentalesConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolucion SSFFAA", ""));
                        break;
                


                case "informesResoluciones.aspx":
                    if (modulo == "Registrar")
                    {
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Concesión de Acuicultura", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informes y Resoluciones", nombre_archivo));
                        break;
                    }
                    else
                    {
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informes y Resoluciones", nombre_archivo));
                        break;

                    }

                    case "ingresarDocumento.aspx":
                    if (modulo == "Registrar")
                    {
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Concesión de Acuicultura", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Administrador de Documentos", nombre_archivo));
                        break;
                    }
                    else
                    {
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Administrador de Documentos", nombre_archivo));
                        break;
                    }

                    case "general.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Solicitud Concesión de Acuicultura", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Label("Referencia Global Sernapesca", nombre_archivo));
                    break;

                    case "administrarDocumento.aspx":
                    if (modulo == "Registrar")
                    {
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Concesión de Acuicultura", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Modificar Documento", nombre_archivo));
                        break;
                    }
                    else
                    {
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Modificar Documento", nombre_archivo));
                        break;
                    }

                    case "evaluarDocumento.aspx":
                    if (modulo == "Registrar")
                    {
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Concesión de Acuicultura", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Evaluación de Documento", nombre_archivo));
                        break;
                    }
                    else
                    {
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Evaluación de Documento", nombre_archivo));
                        break;
                    }

                    case "verDocumento.aspx":
                    if (modulo == "Registrar")
                    {
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Concesión de Acuicultura", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Ver Documento", nombre_archivo));
                        break;
                    }
                    if (modulo == "Solicitudes")
                    {
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Ver Documento", nombre_archivo));
                        break;
                    }
                    else
                    {
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Ver Documento", nombre_archivo));
                        break;
                    }


                    case "identificacionSolicitante.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Concesión de Acuicultura", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Identificación del Solicitante", nombre_archivo));
                        break;

                    case "antecedDelSector.aspx":
                        if (modulo == "Registrar")
                        {
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Solicitud Concesión de Acuicultura", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                            break;
                        }
                        else
                        {
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                            break;
                        }

                    case "unidadEspacial.aspx":
                        if (modulo == "Registrar")
                        {
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Solicitud Concesión de Acuicultura", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Creación de Concesión", nombre_archivo));
                            break;
                        }
                        else
                        {
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Creación de Concesión", nombre_archivo));
                            break;
                        }

                    case "proyTecnico.aspx":
                        if (modulo == "Registrar")
                        {
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Solicitud Concesión de Acuicultura", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Proyecto Técnico", nombre_archivo));
                            break;
                        }
                        else
                        {
                            Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                            Content_Migas.Controls.Add(new LiteralControl(" » "));
                            Content_Migas.Controls.Add(Crea_Label("Proyecto Técnico", nombre_archivo));
                            break;
                        }

        

                case "datosConcesionAcuicultura.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Datos de la Concesión de Acuicultura", nombre_archivo));
                        break;
                case "identificacionTitularConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Identificador del Titular de la Concesión", nombre_archivo));
                        break;
                case "referenciaGlobal.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Referencia Global", nombre_archivo));
                        break;
                case "generalModificacion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Referencia Global", nombre_archivo));
                        break;
                case "antecedDelSectorModificacion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                        break;
                case "proyTecnicoModificacion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Proyecto Técnico", nombre_archivo));
                        break;
                case "unidadesEspaciales.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Modificación de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Modificación de Concesión", nombre_archivo));
                        break;


            
              


                #endregion


                #region RELOCALIZACION

                    //GENERACION DEL TRAMITE
                case "preIngresarSolicitudRelocalizacion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Nueva Solicitud", nombre_archivo));
                        break;
                case "preIngresarSolicitudRelocalizacionRESA.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Nueva Solicitud", nombre_archivo));
                        break;
                case "ingresarSolicitudRelocalizacion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Generar Solicitud de Relocalización", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Nueva Solicitud", nombre_archivo));
                        break;
                case "ingresarSolicitudRelocalizacionRESA.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Generar Solicitud de Relocalización RESA", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Nueva Solicitud", nombre_archivo));
                        break;
                case "verSolicitudRelocalizacion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Administrar Trámites de Relocalización", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Trámite de Relocalización", nombre_archivo));
                        break;
                case "verSolicitudRelocalizacionRESA.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Administrar Trámites de Relocalización RESA", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Trámite de Relocalización RESA", nombre_archivo));
                        break;
                case "redefinirSolicitudRelocalizacion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Administrar Trámites de Relocalización", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Redefinir Trámite de Relocalización", nombre_archivo));
                        break;
                case "redefinirSolicitudRelocalizacionRESA.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Administrar Trámites de Relocalización RESA", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Redefinir Trámite de Relocalización RESA", nombre_archivo));
                        break;
                case "ingresoInformeRESA.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Nueva Solicitud", nombre_archivo));
                        break;
                case "administrarInformesRESA.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Nueva Solicitud", nombre_archivo));
                        break;
               
                


                case "datosTramiteRelocalizacion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Relocalización de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Datos del Trámite de Relocalización", nombre_archivo));
                        break;
                case "datosTramiteRelocalizacionRESA.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Relocalización RESA", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Datos del Trámite de Relocalización", nombre_archivo));
                        break;
                case "identificacionSolicitanteRelocalizacion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Relocalización de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Identificación del Titular de la Concesión", nombre_archivo));
                        break;
                case "identificacionSolicitanteRelocalizacionRESA.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Relocalización RESA", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Identificación del Titular de la Concesión", nombre_archivo));
                        break;
                case "generalRelocalizacion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Relocalización de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Referencia Global Sernapesca", nombre_archivo));
                        break;
                case "generalRelocalizacionRESA.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Relocalización RESA", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Referencia Global Sernapesca", nombre_archivo));
                        break;
                case "antecedDelSectorRelocalizacion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Relocalización de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                        break;
                case "antecedDelSectorRelocalizacionRESA.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Relocalización RESA", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                        break;
                case "proyTecnicoRelocalizacion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Relocalización de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Proyecto Técnico", nombre_archivo));
                        break;
                case "proyTecnicoRelocalizacionRESA.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Relocalización RESA", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Proyecto Técnico", nombre_archivo));
                        break;
                case "informesResolucionesRelocalizacion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Relocalización de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informes y Resoluciones", nombre_archivo));
                        break;
                case "informesResolucionesRelocalizacionRESA.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Relocalización RESA", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informes y Resoluciones", nombre_archivo));
                        break;
                case "unidadEspacialRelocalizacion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Relocalización de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Modificar Concesión", nombre_archivo));
                        break;
                case "unidadEspacialRelocalizacionRESA.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Relocalización RESA", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Modificar Concesión", nombre_archivo));
                        break;
                case "ingresarDocumentoRelocalizacion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Relocalización de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Administrador de Documentos", nombre_archivo));
                        break;
                case "ingresarDocumentoRelocalizacionRESA.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Relocalización RESA", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Administrador de Documentos", nombre_archivo));
                        break;
                case "verDocumentoRelocalizacion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Relocalización de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Ver Documento", nombre_archivo));
                        break;
                case "verDocumentoRelocalizacionRESA.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Relocalización RESA", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Ver Documento", nombre_archivo));
                        break;
                case "administrarDocumentoRelocalizacion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Relocalización de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Modificar Documento", nombre_archivo));
                        break;
                case "administrarDocumentoRelocalizacionRESA.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Relocalización RESA", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Modificar Documento", nombre_archivo));
                        break;
                case "evaluarDocumentoRelocalizacion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Relocalización de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Evaluar Documento", nombre_archivo));
                        break;
                case "evaluarDocumentoRelocalizacionRESA.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Relocalización RESA", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Evaluar Documento", nombre_archivo));
                        break;
                
                    
                //pestañas Relocalizacion por ley.

                case "pestanaAntecedentesComplementariosRelocalizacion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Relocalizacion Por Ley", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Antecedentes Complementarios", ""));
                    break;
                case "pestanaBancoNaturalRelocalizacion.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Relocalizacion Por Ley", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Banco Natrual", ""));
                    break;
                case "pestanaDifrolRelocalizacion.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Relocalizacion Por Ley", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Difrol", ""));
                    break;
                case "pestanaDifusionBancoNaturalRelocalizacion.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Relocalizacion Por Ley", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Difusion Banco Natural", ""));
                    break;
                case "pestanaInformeAmbientalRelocalizacion.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Relocalizacion Por Ley", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Informe Ambiental", ""));
                    break;
                case "pestanaInformeDACRelocalizacion.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Relocalizacion Por Ley", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Informe DAC", ""));
                    break;
                case "pestanaInformeDeCartografiaRelocalizacion.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Relocalizacion Por Ley", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("IT UOT", ""));
                    break;
                case "pestanaInspeccionTerrenoRelocalizacion.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Relocalizacion Por Ley", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Inspeccion de Terreno", ""));
                    break;
                case "pestanaPlanosRelocalizacion.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Relocalizacion Por Ley", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Planos", ""));
                    break;
                case "pestanaResolucionSSFFAARelocalizacion.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Relocalizacion Por Ley", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Resolucion SSFFAA", ""));
                    break;
                case "pestanaResolucionSSPRelocalizacion.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Relocalizacion Por Ley", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Resolucion SSP", ""));
                    break;

                    //pestañas relocalizacion resa
                case "pestanaAntecedentesComplementariosRelocalizacionRESA.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Relocalizacion RESA", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Antecedentes Complementarios", ""));
                    break;
                case "pestanaBancoNaturalRelocalizacionRESA.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Relocalizacion RESA", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Banco Natrual", ""));
                    break;
                case "pestanaDifrolRelocalizacionRESA.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Relocalizacion RESA", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Difrol", ""));
                    break;
                case "pestanaDifusionBancoNaturalRelocalizacionRESA.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Relocalizacion RESA", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Difusion Banco Natural", ""));
                    break;
                case "pestanaInformeAmbientalRelocalizacionRESA.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Relocalizacion RESA", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Informe Ambiental", ""));
                    break;
                case "pestanaInformeDACRelocalizacionRESA.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Relocalizacion RESA", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Informe DAC", ""));
                    break;
                case "pestanaInformeDeCartografiaRelocalizacionRESA.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Relocalizacion RESA", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("IT UOT", ""));
                    break;
                case "pestanaInspeccionTerrenoRelocalizacionRESA.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Relocalizacion RESA", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Inspeccion de Terreno", ""));
                    break;
                case "pestanaPlanosRelocalizacionRESA.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Relocalizacion RESA", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Planos", ""));
                    break;
                case "pestanaResolucionSSFFAARelocalizacionRESA.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Relocalizacion RESA", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Resolucion SSFFAA", ""));
                    break;
                case "pestanaResolucionSSPRelocalizacionRESA.aspx":
                    Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Relocalizacion RESA", ""));
                    Content_Migas.Controls.Add(new LiteralControl(" » "));
                    Content_Migas.Controls.Add(Crea_Link("Resolucion SSP", ""));
                    break;

                #endregion 


                #region ACOPIO


                case "datosCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Solicitud Centro de Acopio", nombre_archivo));
                        break;
                case "identificacionSolicitanteAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Identificación del Solicitante", nombre_archivo));
                        break;
                case "generalAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Referencia Global Sernapesca", nombre_archivo));
                        break;
                case "antecedDelSectorAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                        break;
                case "proyTecnicoAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Proyecto Técnico", nombre_archivo));
                        break;
                case "informesResolucionesAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informes y Resoluciones", nombre_archivo));
                        break;
                case "unidadEspacialAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Creación de Centro de Acopio", nombre_archivo));
                        break;
                case "ingresarDocumentoAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Administrador de Documentos", nombre_archivo));
                        break;
                case "verDocumentoAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Ver Documento", nombre_archivo));
                        break;
                case "administrarDocumentoAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Modificar Documento", nombre_archivo));
                        break;
                case "evaluarDocumentoAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Evaluar Documento", nombre_archivo));
                        break;
                case "pestanaInformeDeCartografiaAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("IT U.O.T", ""));
                        break;
                case "pestanaInspeccionTerrenoAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Inspección Terreno", ""));
                        break;
                case "pestanaBancoNaturalAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Banco Natural", ""));
                        break;
                case "pestanaDifusionBancoNaturalAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difusión Banco Natural", ""));
                        break;
                case "pestanaDifrolAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difrol", ""));
                        break;
                case "pestanaInformeAmbientalAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe Ambiental", ""));
                        break;
                case "pestanaAntecedentesComplementariosAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes Complementarios", ""));
                        break;
                case "pestanaPlanosAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Planos", ""));
                        break;
                case "pestanaInformeDACAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe DAC", ""));
                        break;
                case "pestanaResolucionSSPAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolución SSP", ""));
                        break;
                case "pestanaResolucionSSFFAAAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolución SSFFAA", ""));
                        break;



                    //MODIFACION ACOPIO

                case "datosModificacionCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Datos Centro de Acopio", nombre_archivo));
                        break;
                case "identificacionTitularModificacionCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Titular", nombre_archivo));
                        break;
                case "generalModificacionCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Referencia Global", nombre_archivo));
                        break;
                case "antecedDelSectorModificacionCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                        break;
                case "proyTecnicoModificacionCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Proyecto Técnico", nombre_archivo));
                        break;
                case "informesResolucionesModificacionCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informes y Resoluciones", nombre_archivo));
                        break;
                case "unidadesEspacialesModificacionCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Modificación de Centro de Acopio", nombre_archivo));
                        break;
                case "ingresarDocumentoModificacionCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Administrador de Documentos", nombre_archivo));
                        break;
                case "verDocumentoModificacionCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Ver Documento", nombre_archivo));
                        break;
                case "administrarDocumentoModificacionCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Modificar Documento", nombre_archivo));
                        break;
                case "evaluarDocumentoModificacionCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Evaluar Documento", nombre_archivo));
                        break;
                case "pestanaInformeDeCartografiaModificacionCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("IT UOT", ""));
                        break;
                case "pestanaInspeccionTerrenoModificacionCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Inspecció Terreno", ""));
                        break;
                case "pestanaBancoNaturalModificacionCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Banco Natural", ""));
                        break;
                case "pestanaDifusionBancoNaturalModificacionCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difusión Banco Natural", ""));
                        break;
                case "pestanaDifrolModificacionCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difrol", ""));
                        break;
                case "pestanaInformeAmbientalModificacionCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe Ambiental", ""));
                        break;
                case "pestanaAntecedentesComplementariosModificacionCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes Complementarios", ""));
                        break;
                case "pestanaPlanosModificacionCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Planos", ""));
                        break;
                case "pestanaInformeDACModificacionCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe DAC", ""));
                        break;
                case "pestanaResolucionSSPModificacionCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolución SSP", ""));
                        break;
                case "pestanaResolucionSSFFAAModificacionCentroAcopio.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Acopio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolución SSFFAA", ""));
                        break;
                

                #endregion


                #region FAENAMIENTO

                case "datosCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Solicitud Centro de Acopio", nombre_archivo));
                        break;
                case "identificacionSolicitanteFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Identificación del Solicitante", nombre_archivo));
                        break;
                case "generalFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Referencia Global Sernapesca", nombre_archivo));
                        break;
                case "antecedDelSectorFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                        break;
                case "proyTecnicoFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Proyecto Técnico", nombre_archivo));
                        break;
                case "informesResolucionesFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informes y Resoluciones", nombre_archivo));
                        break;
                case "unidadEspacialFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Creación de Centro de Faenamiento", nombre_archivo));
                        break;
                case "ingresarDocumentoFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Administrador de Documentos", nombre_archivo));
                        break;
                case "verDocumentoFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Ver Documento", nombre_archivo));
                        break;
                case "administrarDocumentoFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Modificar Documento", nombre_archivo));
                        break;
                case "evaluarDocumentoFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Evaluar Documento", nombre_archivo));
                        break;
                case "pestanaInformeDeCartografiaFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("IT UOT", ""));
                        break;
                case "pestanaInspeccionTerrenoFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Inspección Terreno", ""));
                        break;
                case "pestanaBancoNaturalFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Banco Natural", ""));
                        break;
                case "pestanaDifusionBancoNaturalFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difusión Banco Natural", ""));
                        break;
                case "pestanaDifrolFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difrol", ""));
                        break;
                case "pestanaInformeAmbientalFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe Ambiental", ""));
                        break;
                case "pestanaAntecedentesComplementariosFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes Complementarios", ""));
                        break;
                case "pestanaPlanosFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Planos", ""));
                        break;
                case "pestanaInformeDACFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe DAC", ""));
                        break;
                case "pestanaResolucionSSPFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolución SSP", ""));
                        break;
                case "pestanaResolucionSSFFAAFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolución SSFFAA", ""));
                        break;

                    //MODIFICACION DE FAENAMIENTO
                case "datosModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Datos de Trámite Modificación", nombre_archivo));
                        break;
                case "identificacionTitularModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Identificación del Titular", nombre_archivo));
                        break;
                case "generalModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Referencia Global", nombre_archivo));
                        break;
                case "antecedDelSectorModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                        break;
                case "proyTecnicoModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Proyecto Técnico", nombre_archivo));
                        break;
                case "informesResolucionesModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informes y Resoluciones", nombre_archivo));
                        break;
                case "unidadesEspacialesModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Modificación de Centro de Faenamiento", nombre_archivo));
                        break;
                case "ingresarDocumentoModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Administrador de Documentos", nombre_archivo));
                        break;
                case "verDocumentoModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Ver Documento", nombre_archivo));
                        break;
                case "administrarDocumentoModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Modificar Documento", nombre_archivo));
                        break;
                case "evaluarDocumentoModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Evaluar Documento", nombre_archivo));
                        break;
                case "pestanaInformeDeCartografiaModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("IT UOT", ""));
                        break;
                case "pestanaInspeccionTerrenoModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Inspección Terreno", ""));
                        break;
                case "pestanaBancoNaturalModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Banco Natural", ""));
                        break;
                case "pestanaDifusionBancoNaturalModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difusión Banco Natural", ""));
                        break;
                case "pestanaDifrolModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difrol", ""));
                        break;
                case "pestanaInformeAmbientalModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe Ambiental", ""));
                        break;
                case "pestanaAntecedentesComplementariosModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes Complementarios", ""));
                        break;
                case "pestanaPlanosModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Planos", ""));
                        break;
                case "pestanaInformeDACModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe DAC", ""));
                        break;
                case "pestanaResolucionSSPModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolución SSP", ""));
                        break;
                case "pestanaResolucionSSFFAAModificacionCentroFaenamiento.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de Centro de Faenamiento", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolución SSFFAA", ""));
                        break;
                


                #endregion


                #region ECMPO



                case "datosCentroECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Solicitud de Acuicultura en ECMPO", nombre_archivo));
                        break;
                case "identificacionTitularECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Identificación del Titular", nombre_archivo));
                        break;
                case "generalECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Referencia Global", nombre_archivo));
                        break;
                case "antecedDelSectorECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                        break;
                case "proyTecnicoECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Proyecto Técnico", nombre_archivo));
                        break;
                case "informesResolucionesECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informes y Resoluciones", nombre_archivo));
                        break;
                case "unidadEspacialECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Creación de Acuicultura en ECMPO", nombre_archivo));
                        break;
                case "ingresarDocumentoECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Ingresar Documento", nombre_archivo));
                        break;
                case "verDocumentoECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Ver Documento", nombre_archivo));
                        break;
                case "administrarDocumentoECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Centro Acuicultura ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Administrar Documento", nombre_archivo));
                        break;
                case "evaluarDocumentoECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Evaluar", nombre_archivo));
                        break;
                case "pestanaInformeDeCartografiaECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("IT U.O.T", ""));
                        break;
                case "pestanaInspeccionTerrenoECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Inspección Terreno", ""));
                        break;
                case "pestanaBancoNaturalECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Banco Natural", ""));
                        break;
                case "pestanaDifusionBancoNaturalECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difusión Banco Natural", ""));
                        break;
                case "pestanaDifrolECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difrol", ""));
                        break;
                case "pestanaInformeAmbientalECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe Ambiental", ""));
                        break;
                case "pestanaAntecedentesComplementariosECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes Complementarios", ""));
                        break;
                case "pestanaPlanoECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Planos", ""));
                        break;
                case "pestanaInformeDACECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe DAC", ""));
                        break;
                case "pestanaResolucionSSPECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolución SSP", ""));
                        break;
                case "pestanaResolucionSSFFAAECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolución SSFFAA", ""));
                        break;




                //MODIFCACION ECMPO

                case "datosModificacionECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Ingresar Documento", nombre_archivo));
                        break;
                case "identificacionTitularModificacionECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Identificación del Titular", nombre_archivo));
                        break;
                case "generalModificacionECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Referencia Global", nombre_archivo));
                        break;
                case "antecedDelSectorModificacionECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                        break;
                case "proyTecnicoModificacionECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Proyecto Técnico", nombre_archivo));
                        break;
                case "informesResolucionesModificacionECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                        break;
                case "unidadesEspacialesModificacionECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Modificación de ECMPO", nombre_archivo));
                        break;
                case "ingresarDocumentoModificacionECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Administrar Documentos", nombre_archivo));
                        break;
                case "verDocumentoModificacionECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Ver Documento", nombre_archivo));
                        break;
                case "administrarDocumentoModificacionECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Administrar Documento", nombre_archivo));
                        break;
                case "evaluarDocumentoModificacionECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Evaluar", nombre_archivo));
                        break;
                case "pestanaInformeDeCartografiaModificacionECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("IT UOT", ""));
                        break;
                case "pestanaInspeccionTerrenoModificacionECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Inspección Terreno", ""));
                        break;
                case "pestanaBancoNaturalModificacionECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Banco Natural", ""));
                        break;
                case "pestanaDifusionBancoNaturalModificacionECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difusión Banco Natural", ""));
                        break;
                case "pestanaDifrolModificacionECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difrol", ""));
                        break;
              
                case "pestanaInformeAmbientalModificacionECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe Ambiental", ""));
                        break;
                case "pestanaAntecedentesComplementariosModificacionECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes Complementarios", ""));
                        break;
                case "pestanaPlanosModificacionECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Planos", ""));
                        break;
                case "pestanaInformeDACModificacionECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe DAC", ""));
                        break;
                case "pestanaResolucionSSPModificacionECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolucion SSP", ""));
                        break;
                case "pestanaResolucionSSFFAAModificacionECMPO.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación Acuicultura en ECMPO", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolucion SSFFAA", ""));
                        break;
                
                #endregion


                #region AMERB


                case "datosCentroAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Solicitud Acuicultura Amerb", nombre_archivo));
                        break;
                case "identificacionSolicitanteAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Identificación del Titular", nombre_archivo));
                        break;
                case "generalAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Referencia Global Sernapesca", nombre_archivo));
                        break;
                case "antecedDelSectorAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                        break;
                case "proyTecnicoAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Proyecto Técnico", nombre_archivo));
                        break;
                case "informesResolucionesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informes y Resoluciones", nombre_archivo));
                        break;
                case "unidadEspacialAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Creación de Centro de Auicultura en AMERB", nombre_archivo));
                        break;
                case "ingresarDocumentoAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Administrador de Documentos", nombre_archivo));
                        break;
                case "verDocumentoAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Ver Documento", nombre_archivo));
                        break;
                case "administrarDocumentoAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Centro en Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Modificar Documento", nombre_archivo));
                        break;
                case "evaluarDocumentoAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Evaluar Documento", nombre_archivo));
                        break;
                case "pestanaInformeDeCartografiaAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("IT U.O.T", ""));
                        break;
                case "pestanaInspeccionTerrenoAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Inspección Terreno", ""));
                        break;
                case "pestanaBancoNaturalAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Banco Natural", ""));
                        break;
                case "pestanaDifusionBancoNaturalAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difusión Banco Natural", ""));
                        break;
                case "pestanaDifrolAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difrol", ""));
                        break;
                case "pestanaInformeAmbientalAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe Ambiental", ""));
                        break;
                case "pestanaAntecedentesComplementariosAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes Complementarios", ""));
                        break;
                case "pestanaPlanosAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Planos", ""));
                        break;
                case "pestanaInformeDACAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe DAC", ""));
                        break;
                case "pestanaResolucionSSPAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolución SSP", ""));
                        break;
                case "pestanaResolucionSSFFAAAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolución SSFFAA", ""));
                        break;
                case "pestanaEvaluacionURBamerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Evaluación Amerb", ""));
                        break;
                case "pestanaSuficienciaFormalamerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Acuicultura Amerb", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Suficiencia Formal", ""));
                        break;


                //MODIFICACION AMERB

                case "datosModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Datos Modificación de Trámite AMERB", nombre_archivo));
                        break;
                case "identificacionTitularModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Identificación de Titular", nombre_archivo));
                        break;
                case "generalModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Referencia Global", nombre_archivo));
                        break;
                case "antecedDelSectorModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                        break;
                case "proyTecnicoModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Proyecto Técnico", nombre_archivo));
                        break;
                case "informesResolucionesModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informes y Resoluciones", nombre_archivo));
                        break;
                case "unidadesEspacialesModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Modificación de Amerb", nombre_archivo));
                        break;
                case "ingresarDocumentoModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Administrador de Documentos", nombre_archivo));
                        break;
                case "verDocumentoModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Ver Documento", nombre_archivo));
                        break;
                case "administrarDocumentoModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Modificar Documento", nombre_archivo));
                        break;
                case "evaluarDocumentoModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Evaluar Documento", nombre_archivo));
                        break;
                case "pestanaInformeDeCartografiaModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("IT UOT", ""));
                        break;
                case "pestanaInspeccionTerrenoModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Inspección Terreno", ""));
                        break;
                case "pestanaBancoNaturalModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Banco Natural", ""));
                        break;
                case "pestanaDifusionBancoNaturalModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difusión Banco Natural", ""));
                        break;
                case "pestanaDifrolModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difrol", ""));
                        break;
                case "pestanaInformeAmbientalModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe Ambiental", ""));
                        break;
                case "pestanaAntecedentesComplementariosModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes Complementarios", ""));
                        break;
                case "pestanaPlanosModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Planos", ""));
                        break;
                case "pestanaInformeDACModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe DAC", ""));
                        break;
                case "pestanaResolucionSSPModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolución SSP", ""));
                        break;
                case "pestanaResolucionSSFFAAModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolución SSFFAA", ""));
                        break;
                case "pestanaEvaluacionURBModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Evaluación URB", ""));
                        break;
                case "pestanaSuficienciaFormalModificacionAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Modificación de AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Sufuciencia Formal", ""));
                        break;
                

                #endregion AMERB


                #region EXPERIMENTAL AMERB

                case "datosCentroExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en  AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Solicitud Experimental AMERB", nombre_archivo));
                        break;
                case "identificacionTitularExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Identificación del Solicitante", nombre_archivo));
                        break;
                case "generalExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Referencia Global", nombre_archivo));
                        break;
                case "antecedDelSectorExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                        break;
                case "proyTecnicoExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Proyecto Técnico", nombre_archivo));
                        break;
                case "informesResolucionesExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informes y Resoluciones", nombre_archivo));
                        break;
                case "unidadEspacialExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Creación de Experimental en AMERB", nombre_archivo));
                        break;
                case "ingresarDocumentoExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Administrador de Documentos", nombre_archivo));
                        break;
                case "verDocumentoExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Referencia Global", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Ver Documento", nombre_archivo));
                        break;
                case "administrarDocumentoExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Modificar Documento", nombre_archivo));
                        break;
                case "evaluarDocumentoExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Evaluar Documento", nombre_archivo));
                        break;
                case "pestanaInformeDeCartografiaExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("IT U.O.T", ""));
                        break;
                case "pestanaInspeccionTerrenoExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Inspección Terreno", ""));
                        break;
                case "pestanaBancoNaturalExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Banco Natural", ""));
                        break;
                case "pestanaDifusionBancoNaturalExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difusión Banco Natural", ""));
                        break;
                case "pestanaDifrolExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difrol", ""));
                        break;
                case "pestanaInformeAmbientalExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe Ambiental", ""));
                        break;
                case "pestanaAntecedentesComplementariosExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes Complementarios", ""));
                        break;
                case "pestanaPlanosExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Planos", ""));
                        break;
                case "pestanaInformeDACExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe DAC", ""));
                        break;
                case "pestanaResolucionSSPExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolución SSP", ""));
                        break;
                case "pestanaResolucionSSFFAAExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolución SSFFAA", ""));
                        break;
                case "pestanaEvaluacionURBExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Evaluación URB", ""));
                        break;
                case "pestanaSuficienciaFormalExperimentalesAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Experimentales en AMERB", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Suficiencia Formal", ""));
                        break;
                case "TransformarAmerb_ExperimentalAmerb.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Transformar Amerb en Experimental Amerb", ""));
                        break;
                #endregion 


                #region EXPERIMENTAL DE CONCESION


                case "datosCentroExperimentalesConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud de Experimental de Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Solicitud Experimental de Concesión", nombre_archivo));
                        break;
                case "identificacionTitularExperimentalesConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitudes de Experimentales Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Identificación del Solicitante", nombre_archivo));
                        break;
                case "generalExperimentalesConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitudes de Experimentales Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Referencia Global", nombre_archivo));
                        break;
                case "antecedDelSectorExperimentalesConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitudes de Experimentales Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Identificación del Solicitante", nombre_archivo));
                        break;
                case "proyTecnicoExperimentalesConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitudes de Experimentales Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Identificación del Solicitante", nombre_archivo));
                        break;
                case "informesResolucionesExperimentalesConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitudes de Experimentales Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Identificación del Solicitante", nombre_archivo));
                        break;
                case "unidadEspacialExperimentalesConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitudes de Experimentales Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Identificación del Solicitante", nombre_archivo));
                        break;
                case "ingresarDocumentoExperimentalesConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitudes de Experimentales Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Administrador de Documentos", nombre_archivo));
                        break;
                case "verDocumentoExperimentalesConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitudes de Experimentales Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Ver Documento", nombre_archivo));
                        break;
                case "administrarDocumentoExperimentalesConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitudes de Experimentales Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Modificar Documento", nombre_archivo));
                        break;
                case "evaluarDocumentoExperimentalesConcesion.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitudes de Experimentales Concesión", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Evaluar Documento", nombre_archivo));
                        break;



                #endregion


                #region COLECTORES


                case "datosCentroColector.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Colectores de Semillas", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Solicitud Acuicultura Amerb", nombre_archivo));
                        break;
                case "identificacionSolicitanteColector.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Colectores de Semillas", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Identificación del Titular", nombre_archivo));
                        break;
                case "generalColector.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Colectores de Semillas", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Referencia Global", nombre_archivo));
                        break;
                case "antecedDelSectorColector.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Colectores de Semillas", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                        break;
                case "proyTecnicoColector.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Colectores de Semillas", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Proyecto Técnico", nombre_archivo));
                        break;
                case "informesResolucionesColector.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Colectores de Semillas", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informes y Resoluciones", nombre_archivo));
                        break;
                case "unidadEspacialColector.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Colectores de Semillas", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Creación de Colector de Semilla", nombre_archivo));
                        break;
                case "ingresarDocumentoColector.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Colectores de Semillas", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Administrador de Documentos", nombre_archivo));
                        break;
                case "verDocumentoColector.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Colectores de Semillas", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Ver Documento", nombre_archivo));
                        break;
                case "administrarDocumentoColector.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Colector de Semillas", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Modificar Documento", nombre_archivo));
                        break;
                case "evaluarDocumentoColector.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Colectores de Semillas", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Evaluar Documento", nombre_archivo));
                        break;
                case "pestanaInformeDeCartografiaColector.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Colectores de Semillas", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("IT. U.O.T.", ""));
                        break;
                case "pestanaInspeccionTerrenoColector.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Colectores de Semillas", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Inspección Terreno.", ""));
                        break;
                case "pestanaBancoNaturalColector.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Colectores de Semillas", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Banco Natural", ""));
                        break;
                case "pestanaDifusionBancoNaturalColector.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Colectores de Semillas", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difusión Banco Natural", ""));
                        break;
                case "pestanaDifrolColector.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Colectores de Semillas", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Difrol", ""));
                        break;
                case "pestanaInformeAmbientalColector.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Colectores de Semillas", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe Ambiental", ""));
                        break;
                case "pestanaAntecedentesComplementariosColector.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Colectores de Semillas", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Antecedentes Complementarios", ""));
                        break;
                case "pestanaPlanosColector.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Colectores de Semillas", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Planos", ""));
                        break;
                case "pestanaInformeDACColector.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Colectores de Semillas", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Informe DAC", ""));
                        break;
                case "pestanaResolucionSSPColector.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Colectores de Semillas", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolución SSP", ""));
                        break;
                case "pestanaResolucionSSFFAAColector.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Solicitud Colectores de Semillas", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Resolución SSFFAA", ""));
                        break;

                #endregion


                #region MANTENEDOR TITULAR, REPRESENTANTE LEGAL, OPERADOR Y HOLDING

                
                case "administrarTitulares.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Administrar Titulares", nombre_archivo));
                       break;
                case "agregarTitular.aspx":
                
                        acc = 0;
                        try
                        {
                            if (Request.QueryString["acc"] != null)
                            {
                                acc = Convert.ToInt32(Request.QueryString["acc"]);
                            };
                        }
                        catch
                        {};

                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Administrar Titular", "administrarTitulares.aspx"));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));

                       if (acc == 1)
                       {
                           Content_Migas.Controls.Add(Crea_Label("Agregar Titular", nombre_archivo));
                       }
                       else {
                           Content_Migas.Controls.Add(Crea_Label("Modificar Titular", nombre_archivo));
                       }
                       break;
                case "administrarRepresentantesLegales.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Administrar Representante Legal", nombre_archivo));
                       break;
                case "agregarRepresentanteLegal.aspx": 
                    
                        acc = 0;
                        try
                        {
                            if (Request.QueryString["acc"] != null)
                            {
                                acc = Convert.ToInt32(Request.QueryString["acc"]);
                            };
                        }
                        catch
                        {};

                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Administrar Representante Legal", "administrarRepresentantesLegales.aspx"));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));

                       if (acc == 1)
                       {
                           Content_Migas.Controls.Add(Crea_Label("Agregar Representante Legal", nombre_archivo));
                       }
                       else
                       {
                           Content_Migas.Controls.Add(Crea_Label("Modificar Representante Legal", nombre_archivo));
                       }

                       break;
                case "administrarOperadores.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Administrar Operador", nombre_archivo));
                       break;
                case "agregarOperador.aspx": //Falta modificar
                        
                        acc = 0;
                        try
                        {
                            if (Request.QueryString["acc"] != null)
                            {
                                acc = Convert.ToInt32(Request.QueryString["acc"]);
                            };
                        }
                        catch
                        {};

                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Link("Administrar Operador", "administrarOperadores.aspx"));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));

                        if (acc == 1)
                        {
                            Content_Migas.Controls.Add(Crea_Label("Agregar Operador", nombre_archivo));
                        }
                        else {
                            Content_Migas.Controls.Add(Crea_Label("Modificar Operador", nombre_archivo));
                        }
                       break;
                case "agregarContactoDireccion.aspx":
                       
                        acc = 0;
                        int bp = 0;
                        try
                        {
                            if (Request.QueryString["bp"] != null)
                            {
                                bp = Convert.ToInt32(Request.QueryString["bp"]);
                            };
                            if (Request.QueryString["acc"] != null)
                            {
                                acc = Convert.ToInt32(Request.QueryString["acc"]);
                            };
                        }
                        catch
                        {};

                        switch (bp)
                        {
                            case 1:
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Administrar Titular", "administrarTitulares.aspx"));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                if (acc == 1)
                                {
                                    Content_Migas.Controls.Add(Crea_Link("Crear Titular", "agregarTitular.aspx"));
                                }
                                else {
                                    Content_Migas.Controls.Add(Crea_Link("Modificar Titular", "agregarTitular.aspx"));
                                }
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Label("Matriz y Sucursales", nombre_archivo));
                                break;
                            case 2:
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Administrar Representante Legal", "administrarRepresentanteLegal.aspx"));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                if (acc == 1)
                                {
                                    Content_Migas.Controls.Add(Crea_Link("Crear Representante Legal", "agregarRepresentanteLegal.aspx"));
                                }
                                else {
                                    Content_Migas.Controls.Add(Crea_Link("Modificar Representante Legal", "agregarRepresentanteLegal.aspx"));
                                }
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Label("Matriz y Sucursales", nombre_archivo));
                                break;
                            case 3:
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Administrar Operador", "administrarOperadores.aspx"));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                if (acc == 1)
                                {
                                    Content_Migas.Controls.Add(Crea_Link("Crear Operador", "agregarOperador.aspx"));
                                }
                                else {
                                    Content_Migas.Controls.Add(Crea_Link("Modificar Operador", "agregarOperador.aspx"));
                                }
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Label("Matriz y Sucursales", nombre_archivo));
                                break;
                        }
                        break;

                case "asociarRepresentanteLegal.aspx":

                        acc = 0;
                        bp = 0;
                        try
                        {
                            if (Request.QueryString["bp"] != null)
                            {
                                bp = Convert.ToInt32(Request.QueryString["bp"]);
                            };
                            if (Request.QueryString["acc"] != null)
                            {
                                acc = Convert.ToInt32(Request.QueryString["acc"]);
                            };
                        }
                        catch
                        { };

                        switch (bp)
                        {
                            case 1:
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Administrar Titular", "administrarTitulares.aspx"));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                if (acc == 1)
                                {
                                    Content_Migas.Controls.Add(Crea_Link("Crear Titular", "agregarTitular.aspx"));
                                }
                                else
                                {
                                    Content_Migas.Controls.Add(Crea_Link("Modificar Titular", "agregarTitular.aspx"));
                                }
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Label("Asociar Representante Legal", nombre_archivo));
                                break;
                           
                        }
                        break;
                case "asociarOperador.aspx":

                        acc = 0;
                        bp = 0;
                        try
                        {
                            if (Request.QueryString["bp"] != null)
                            {
                                bp = Convert.ToInt32(Request.QueryString["bp"]);
                            };
                            if (Request.QueryString["acc"] != null)
                            {
                                acc = Convert.ToInt32(Request.QueryString["acc"]);
                            };
                        }
                        catch
                        { };

                        switch (bp)
                        {
                            case 1:
                                Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Link("Administrar Titular", "administrarTitulares.aspx"));
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                if (acc == 1)
                                {
                                    Content_Migas.Controls.Add(Crea_Link("Crear Titular", "agregarTitular.aspx"));
                                }
                                else
                                {
                                    Content_Migas.Controls.Add(Crea_Link("Modificar Titular", "agregarTitular.aspx"));
                                }
                                Content_Migas.Controls.Add(new LiteralControl(" » "));
                                Content_Migas.Controls.Add(Crea_Label("Asociar Operador", nombre_archivo));
                                break;

                        }
                        break;

                case "holding.aspx":
                        Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                        Content_Migas.Controls.Add(new LiteralControl(" » "));
                        Content_Migas.Controls.Add(Crea_Label("Administrar Solicitudes", nombre_archivo));
                        break;
              


                #endregion


                #region MANTENEDOR GENERAL


                case "region.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Región", nombre_archivo));
                       break;
                case "provincia.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Provincia", nombre_archivo));
                       break;
                case "comuna.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Comuna", nombre_archivo));
                       break;
                case "tipoBarrio.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Tipo Barrio", nombre_archivo));
                       break;
                case "barrio.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Barrio", nombre_archivo));
                       break;
                case "barrioTipo.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Asociación Barrio -  Tipo", nombre_archivo));
                       break;
                case "especieEtapaDesarrollo.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Asociación Especie -  Etapa de Cultivo", nombre_archivo));
                       break;
                case "macrozona.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Macrozona", nombre_archivo));
                       break;
                case "carta.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Carta SHOA / IGM Plano", nombre_archivo));
                       break;
                case "datum.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Datum", nombre_archivo));
                       break;
                case "tipoVertice.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Tipo Vértice", nombre_archivo));
                       break;
                case "tipoContacto.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Tipo Contacto", nombre_archivo));
                       break;
                case "huso.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Tipo Huso", nombre_archivo));
                       break;
                case "tipoConcesion.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Tipo Concesión", nombre_archivo));
                       break;
                case "tipoUso.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Tipo Uso", nombre_archivo));
                       break;
                case "tipoCultivo.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Tipo Cultivo", nombre_archivo));
                       break;
                case "metodoCultivo.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Método Cultivo Algas", nombre_archivo));
                       break;
                case "tipoAlimento.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Tipo Alimento", nombre_archivo));
                       break;
                case "especie.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Especie de Cultivo", nombre_archivo));
                       break;
                case "etapaDeDesarrollo.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Etapa de Cultivo", nombre_archivo));
                       break;
                case "estructuraTecnica.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Tipo Estructura", nombre_archivo));
                       break;
                case "formaEstructura.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Forma Estructura", nombre_archivo));
                       break;
                case "unidadMedidaEstructuraTecnica.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Unidad Medida Estructura", nombre_archivo));
                       break;
                case "volumenUnidadMedida.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Volumen Unidad Medida Estructura", nombre_archivo));
                       break;
                case "anio.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Años", nombre_archivo));
                       break;
                case "unidadEjemplar.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Unidad Ejemplar", nombre_archivo));
                       break;
                case "rangoPesoEjemplar.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Rango Peso Ejemplar", nombre_archivo));
                       break;
                case "capitaniaPuerto.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Capitanía de Puerto", nombre_archivo));
                       break;
                case "tipoArchivoTitular.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Tipo Archivo Titulares", nombre_archivo));
                       break;
                case "tipoArchivoAntEspaciales.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Tipo Archivo Coord. Originales", nombre_archivo));
                       break;
                case "tipoArchivoAntTerreno.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Tipo Archivo Coord. Entrega de Material", nombre_archivo));
                       break;
                case "tipoArchivoRegularizacion.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Tipo Archivo Coord. Regularización", nombre_archivo));
                       break;
                case "tipoCentroAcopio.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Tipo Centro Acopio", nombre_archivo));
                       break;
                case "tipoCentroFaenamiento.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Tipo Centro Faenamiento", nombre_archivo));
                       break;
                case "tipoCuerpoAgua.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Tipo Cuerpo de Agua", nombre_archivo));
                       break;
                case "cuerpoDeAgua.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Cuerpo de Agua", nombre_archivo));
                       break;
                case "plazoNominal.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Plazo Nominal", nombre_archivo));
                       break;
                case "tipoOrganizacion.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Tipo Organización", nombre_archivo));
                       break;
                case "descasoACS.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Descanso ACS", nombre_archivo));
                       break;
                case "especieTipoAlimento.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Asociacion Especie - Alimento", ""));
                       break;
                case "grupoEspecie.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Grupo - Especie", ""));
                       break;
                case "barrioAsociaciones.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Administrar Unidad Espacial", ""));
                       break;
                case "administradorDeCorreos.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Administrador De Correos", ""));
                       break;
                #endregion


                #region MANTENEDOR TRANSVERSAL

                
                case "resultado.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Resultado", nombre_archivo));
                       break;
                case "AsocSubrequerimientoResultado.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Asociación Subrequerimiento - Resultado", nombre_archivo));
                       break;
                case "tipoDocumento.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Tipo de Documento", nombre_archivo));
                       break;
                case "asocSubrequerimientoTipo.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Asociación Subrequerimiento - Tipo Documento", nombre_archivo));
                       break;
                case "temaSubrequerimiento.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Tema Subrequerimiento", nombre_archivo));
                       break;
                case "responsableDAC.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Responsable DAC", nombre_archivo));
                       break;
                case "preferenciaRelocalizacion.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Preferencia de Relocalización", nombre_archivo));
                       break;
                case "consultores.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Consultores", ""));
                       break;
                case "direccionZonalRegion.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Direccion Zonal Region", ""));
                       break;
                case "entidadesDeAnalisis.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Entidades de Analisis", ""));
                       break;
                case "entidadesDeMuestreo.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Entidades de Muestreo", ""));
                       break;
                case "modificarAsocSubrequerimientoResultado.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Modificar Asociacion Subrequermiento Resultado", ""));
                       break;
                case "oficinaZonal.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Direccion Zonal", ""));
                       break;
                case "plazosDocumentos.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Plazos Documentales", ""));
                       break;
                case "tipoSupeditado.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Tipo Supeditado", ""));
                       break;
                #endregion


                #region MANTENEDOR DE PERSONAS 

                case "verRepresentanteLegal.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Administrar Representante Legal", "administrarRepresentantesLegales.aspx"));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Ver Representante Legal", nombre_archivo));
                       break;
                case "verContactoRepresentanteLegal.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Ver Representante Legal", "verRepresentanteLegal.aspx"));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Ver Matriz y Sucursal Representante Legal", nombre_archivo));
                       break;

                case "verContactoTitular.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Ver Titular", "detalleTitular.aspx"));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Ver Matriz y Sucursal Titular", nombre_archivo));
                       break;
                case "verOperador.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Administrar Operador", "administrarOperadores.aspx"));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Ver Operador", nombre_archivo));
                       break;
                case "verContactoOperador.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Ver Operador", "verOperador.aspx"));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Ver Matriz y Sucursal Operador", nombre_archivo));
                       break;
                case "agregarNombres.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Modificar Titular", "agregarTitular.aspx"));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Agregar Nombre", nombre_archivo));
                       break;

                #endregion


                #region ANTECEDENTES DEL SECTOR EN LA UNIDAD ESPACIAL

                case "antecedentesDelSectorAcopio.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro de Acopio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                       break;
                case "antecedentesDelSectorAmerb.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro en Amerb", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                       break;
                case "antecedentesDelSectorExperimentalesAmerb.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro Experimentales en Amerb", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                       break;
                case "antecedentesDelSectorColector.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Colector de Semillas", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                       break;
                case "antecedentesDelSectorFaenamiento.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro de Faenamiento", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                       break;
                case "antecedentesDelSectorECMPO.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro Acuicultura ECMPO", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                       break;
                case "antecedentesDelSectorConcesion.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Administrar Concesiones de Acuicultura", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Concesión de Acuicultura", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Antecedentes del Sector", nombre_archivo));
                       break;
             

                #endregion


                #region PROYECTO TECNICO EN LA UNIDAD ESPACIAL

                case "proyectoTecnicoConcesion.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Administrar Concesiones de Acuicultura", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Concesión de Acuicultura", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Proyecto Técnico", nombre_archivo));
                       break;
                case "proyectoTecnicoAcopio.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro de Acopio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Proyecto Técnico", nombre_archivo));
                       break;
                case "proyectoTecnicoAmerb.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro en Amerb", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Proyecto Técnico", nombre_archivo));
                       break;
                case "proyectoTecnicoExperimentalesAmerb.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro Experimentales en Amerb", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Proyecto Técnico", nombre_archivo));
                       break;
                case "proyectoTecnicoColector.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Colector de Semillas", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Proyecto Técnico", nombre_archivo));
                       break;
                case "proyectoTecnicoFaenamiento.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro de Faenamiento", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Proyecto Técnico", nombre_archivo));
                       break;
                case "proyectoTecnicoECMPO.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro Acuicultura ECMPO", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Proyecto Técnico", nombre_archivo));
                       break;
             
                #endregion


                #region REFERENCIAS PRODUCTIVAS SANITARIAS Y AMBIENTALES EN LA UNIDAD ESPACIAL

                case "referenciasProductivaECMPO.aspx.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro Acuicultura ECMPO ", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Referencias Productivas", nombre_archivo));
                       break;
                case "referenciasAmbientalesECMPO.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro Acuicultura ECMPO ", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Referencias Ambientales", nombre_archivo));
                       break;
                case "referenciasSanitariasECMPO.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro Acuicultura ECMPO", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Referencias Sanitarias", nombre_archivo));
                       break;
                case "referenciasAmbientalesAcopio.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro de Acopio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Referencias Ambientales", nombre_archivo));
                       break;
                case "referenciasAmbientalesAmerb.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro en Amerb", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Referencias Ambientales", nombre_archivo));
                       break;
                case "referenciasAmbientalesExperimentalesAmerb.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro Experimentales en Amerb", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Referencias Ambientales", nombre_archivo));
                       break;
                case "referenciasAmbientalesColector.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Colector de Semillas", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Referencias Ambientales", nombre_archivo));
                       break;
                case "referenciasAmbientalesFaenamiento.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro de Faenamiento", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Referencias Ambientales", nombre_archivo));
                       break;
                case "referenciasAmbientalesConcesion.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Administrar Concesiones de Acuicultura", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Concesión de Acuicultura", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Referencias Ambientales", nombre_archivo));
                       break;
                case "referenciasProductivaAcopio.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro de Acopio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Referencias Productivas", nombre_archivo));
                       break;
                case "referenciasProductivaAmerb.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro en Amerb", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Referencias Productivas", nombre_archivo));
                       break;
                case "referenciasProductivaColector.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Colector de Semillas", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Referencias Productivas", nombre_archivo));
                       break;
                case "referenciasProductivaFaenamiento.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro de Faenamiento", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Referencias Productivas", nombre_archivo));
                       break;
                case "referenciasProductivaConcesion.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Administrar Concesiones de Acuicultura", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Concesión de Acuicultura", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Referencias Productivas", nombre_archivo));
                       break;
                case "referenciasSanitariasAcopio.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro de Acopio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Referencias Sanitarias", nombre_archivo));
                       break;
                case "referenciasSanitariasAmerb.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro en Amerb", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Referencias Sanitarias", nombre_archivo));
                       break;
                case "referenciasSanitariasExperimentalesAmerb.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro Experimentales en Amerb", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Referencias Sanitarias", nombre_archivo));
                       break;
                case "referenciasSanitariasColector.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Colector de Semillas", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Referencias Sanitarias", nombre_archivo));
                       break;
                case "referenciasSanitariasFaenamiento.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro de Faenamiento", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Referencias Sanitarias", nombre_archivo));
                       break;
                case "referenciasSanitariasConcesion.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Administrar Concesiones de Acuicultura", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Concesión de Acuicultura", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Referencias Sanitarias", nombre_archivo));
                       break;
                case "referenciasProductivaECMPO.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro Acuicultura ECMPO", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Referencias Productivas", nombre_archivo));
                       break;




                #endregion


                #region RESOLUCIONES EN LA UNIDAD ESPACIAL


                case "resolucionesAcopio.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro de Acopio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Resoluciones", nombre_archivo));
                       break;
                case "resolucionesAmerb.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro en Amerb", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Resoluciones", nombre_archivo));
                       break;
                case "resolucionesExperimentalesAmerb.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro Experimentales en Amerb", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Resoluciones", nombre_archivo));
                       break;
                case "resolucionesColector.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Colector de Semillas", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Resoluciones", nombre_archivo));
                       break;
                case "resolucionesFaenamiento.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro de Faenamiento", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Resoluciones", nombre_archivo));
                       break;
                case "resolucionesConcesion.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Administrar Concesiones de Acuicultura", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Concesión de Acuicultura", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Resoluciones", nombre_archivo));
                       break;
                case "resolucionesECMPO.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro Acuicultura ECMPO", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Resoluciones", nombre_archivo));
                       break;


                #endregion


                #region RESUMEN EN LA UNIDAD ESPACIAL

                case "resumenAcopio.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro de Acopio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Resumen", nombre_archivo));
                       break;
                case "resumenAmerb.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro en Amerb", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Resumen", nombre_archivo));
                       break;
                case "resumenExperimentalesAmerb.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro Experimentales en Amerb", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Resumen", nombre_archivo));
                       break;
                case "resumenECMPO.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro Acuicultura ECMPO", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Resumen", nombre_archivo));
                       break;
                case "resumenColector.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Colector de Semillas", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Resumen", nombre_archivo));
                       break;
                case "resumenFaenamiento.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro de Faenamiento", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Resumen", nombre_archivo));
                       break;
                case "resumenConcesion.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Administrar Concesiones de Acuicultura", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Concesión de Acuicultura", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Resumen", nombre_archivo));
                       break;

                #endregion


                #region TITULAR EN LA UNIDAD ESPACIAL


                case "titularECMPO.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro Acuicultura ECMPO", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Titular", nombre_archivo));
                       break;
                case "titularAcopio.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro de Acopio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Titular", nombre_archivo));
                       break;
                case "titularAmerb.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro en Amerb", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Titular", nombre_archivo));
                       break;
                case "titularColector.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Colector de Semillas", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Titular", nombre_archivo));
                       break;
                case "titularFaenamiento.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro de Faenamiento", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Titular", nombre_archivo));
                       break;
                case "titularConcesion.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Administrar Concesiones de Acuicultura", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Concesión de Acuicultura", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Titular", nombre_archivo));
                       break;

                #endregion


                #region TRAMITES ASOCIADOS EN LA UNIDAD ESPACIAL


                case "tramitesAsociadosAcopio.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro de Acopio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Trámites Asociados", nombre_archivo));
                       break;
                case "tramitesAsociadosAmerb.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro en Amerb", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Trámites Asociados", nombre_archivo));
                       break;
                case "tramitesAsociadosExperimentalesAmerb.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro Experimentales en Amerb", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Trámites Asociados", nombre_archivo));
                       break;
                case "tramitesAsociadosColector.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Colector de Semillas", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Trámites Asociados", nombre_archivo));
                       break;
                case "tramitesAsociadosFaenamiento.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro de Faenamiento", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Trámites Asociados", nombre_archivo));
                       break;
                case "tramitesAsociadosConcesion.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Administrar Concesiones de Acuicultura", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Concesión de Acuicultura", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Trámites Asociados", nombre_archivo));
                       break;
                case "tramitesAsociadosECMPO.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro Acuicultura ECMPO", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Trámites Asociados", nombre_archivo));
                       break;

                #endregion


                #region UNIDAD ESPACIAL (EN LA UNIDAD ESPACIAL)

                case "unidadesEspacialesAcopio.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro de Acopio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Unidades Espaciales", nombre_archivo));
                       break;
                case "unidadesEspacialesAmerb.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro en Amerb", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Unidades Espaciales", nombre_archivo));
                       break;
                case "unidadesEspacialesExperimentalesAmerb.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro Experimentales en Amerb", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Unidades Espaciales", nombre_archivo));
                       break;
                case "unidadesEspacialesColectores.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Colector de Semillas", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Unidades Espaciales", nombre_archivo));
                       break;
                case "unidadesEspacialesFaenamiento.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro de Faenamiento", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Unidades Espaciales", nombre_archivo));
                       break;
                case "unidadesEspacialesConcesion.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Administrar Concesiones de Acuicultura", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Concesión de Acuicultura", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Unidades Espaciales", nombre_archivo));
                       break;
                case "unidadesEspacialesECMPO.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Centro Acuicultura ECMPO", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Unidad Espacial", nombre_archivo));
                       break;

                #endregion


                #region BITACORA

                case "BitacoraCambios.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Bitácora de Cambios", ""));
                       break;

                #endregion

                

                case "detalleConcesion.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Concesiones", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Concesiones", nombre_archivo));
                       break;

                case "administrarDocumentoConcesion.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Administrar Concesiones de Acuicultura", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Concesión de Acuicultura", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Label("Administrar Documento", nombre_archivo));
                       break;


                #region Documentos Masivos

                case "GeneradorDocumental.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Generador Documental", ""));
                       break;
                case "AdministradorDocumental.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Administrador de Documentos", ""));
                       break;
                case "PoligonosMasivos.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Subida de Poligonos Masivos", ""));
                       break;

                #endregion

                #region Grupo Suspendido

                case "AdministradorGrupoSuspendido.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Administrador Grupo Suspendido", ""));
                       break;

                case "VerGrupoSuspendido.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Administrador Grupo Suspendido", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Ver Grupo Suspendido", ""));
                       break;

                case "CrearGrupoSuspendido.aspx":
                       Content_Migas.Controls.Add(Crea_Link("Inicio", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Administrador Grupo Suspendido", ""));
                       Content_Migas.Controls.Add(new LiteralControl(" » "));
                       Content_Migas.Controls.Add(Crea_Link("Crear Grupo Suspendido", ""));
                       break;


                #endregion



            };
        }


        
        protected LinkButton Crea_Link(string modulo, string pagina)
        {
            LinkButton link = new LinkButton();
            link.CausesValidation = false;
            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];
            string parametros = "";
            Hashtable HT_ModReportes = new Hashtable();

            int acc = 0;
            int idMatrizSuc = 0;
            String rutPersona = "";
            MatrizSucursal matrizSucursalAux = null;

            switch (pagina)
            {
                case "":
                    link.Text = modulo;
                    switch (modulo)
                    {



                        /*Resoluciones */

                        case "Administrar Resoluciones":
                            link.PostBackUrl = @"~/Resoluciones/administrarResoluciones.aspx";
                            break;

                            //REDEFINICION DE TRAMITES DE RELOCALIZACION
                        case "Redefinir Trámite de Relocalización":
                            link.PostBackUrl = @"~/Solicitudes/RelocalizacionRESA/redefinirSolicitudRelocalizacion.aspx";
                            break;

                        case "Redefinir Trámite de Relocalización RESA":
                            link.PostBackUrl = @"~/Solicitudes/RelocalizacionRESA/redefinirSolicitudRelocalizacionRESA.aspx";
                            break;


                        case "Inicio":
                            link.PostBackUrl = @"~/Administrador/principal.aspx";
                            break;
                        case "Solicitudes":
                            link.PostBackUrl = @"~/Administrador/Reportes/resumenEstados.aspx";
                            break;
                        case "Concesiones":
                            link.PostBackUrl = @"~/Administrador/Reportes/reportes.aspx";
                            break;
                        case "Solicitud Concesión de Acuicultura":
                            link.PostBackUrl = @"~/Solicitudes/Registrar/ingresarDocumento.aspx";
                            break;
                        case "Solicitud de Modificación de Concesión":
                            link.PostBackUrl = @"~/Solicitudes/Modificacion/ingresarDocumento.aspx";
                            break;
                        case "Solicitud de Relocalización de Concesión":
                            link.PostBackUrl = @"~/Solicitudes/Relocalizacion/datosTramiteRelocalizacion.aspx";
                            break;
                        case "Solicitud de Relocalización RESA":
                            link.PostBackUrl = @"~/Solicitudes/RelocalizacionRESA/datosTramiteRelocalizacionRESA.aspx";
                            break;
                        case "Solicitud de Centro de Acopio":
                            link.PostBackUrl = @"~/Solicitudes/Acopio/ingresarDocumentoAcopio.aspx";
                            break;


                        //RESUMEN DE ESTADOS
                        case "Resumen de Estados":
                            link.PostBackUrl = @"~/Administrador/Reportes/resumenEstados.aspx";
                            break;
                        case "Detalle del Estado":
                            link.PostBackUrl = @"~/Administrador/Reportes/detalleEstado.aspx";
                            break;

                        //REQUERIMIENTOS CON PLAZOS
                        case "Requerimientos con Plazos":
                            link.PostBackUrl = @"~/Administrador/Reportes/plazosRequerimientos.aspx";
                            break;
                        case "Detalle del Requerimiento con Plazo":
                            link.PostBackUrl = @"~/Administrador/Reportes/detallePlazo.aspx";
                            break;

                            

                            //USUARIOS
                        case "Administración de Usuarios":
                            link.PostBackUrl = @"~/Administrador/Usuarios/listUsuarios.aspx";
                            break;


                            //INDICADORES
                        case "Resumen de Indicadores Solicitud Concesión":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadores.aspx";
                            break;
                        case "Resumen de Indicadores Experimentales Concesión":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresExpConcesion.aspx";
                            break;
                        case "Resumen de Indicadores de Ampliación":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresModAmpliacion.aspx";
                            break;
                        case "Resumen de Indicadores de Reducción":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresModReduccion.aspx";
                            break;
                        case "Resumen de Indicadores de Especie":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresModEspecie.aspx";
                            break;
                        case "Resumen de Indicadores de Proyecto Técnico":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresModProyTecnico.aspx";
                            break;
                        case "Resumen de Indicadores de Regularización":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresModRegularizacion.aspx";
                            break;
                        case "Resumen de Indicadores Relocalización Sector Cero":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresLeyCero.aspx";
                            break;
                        case "Resumen de Indicadores Relocalización Crea":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresLeyCrea.aspx";
                            break;
                        case "Resumen de Indicadores Relocalización Fusión":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresLeyFusion.aspx";
                            break;


                        case "resumenIndicadoresExpConcesion.aspx":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresExpConcesion.aspx";
                            break;
                        case "Detalle del Indicador":
                            link.PostBackUrl = @"~/Administrador/Reportes/detalleIndicador.aspx";
                            break;
                            

                        case "Detalle del Indicador Relocalización Crea":
                            link.PostBackUrl = @"~/Administrador/Reportes/detalleIndicadorRelocalizacionCrea.aspx";
                            break;

                        case "Resumen de Indicadores Relocalización Fusiona":
                            link.PostBackUrl = @"~/Administrador/Reportes/resumenIndicadoresRelocalizacionFusiona.aspx";
                            break;
                        case "Resumen de Indicadores Relocalización Fusiona2":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresLeyFusion.aspx";
                            break;

                        case "Resumen de Indicadores RESA Crea":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresRESACrea.aspx";
                            break;


                        case "Resumen de Indicadores RESA Sector 0":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresRESACero.aspx";
                            break;

                        case "Resumen de Indicadores RESA Fusión":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresRESAFusion.aspx";
                            break;
                        case "Resumen de Indicadores Solicitud Acuicultura ECMPO":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresECMPO.aspx";
                            break;
                            
                        case "Resumen de Indicadores Solicitud Colectores de Semillas":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresSemillas.aspx";
                            break;


                        case "Resumen de Indicadores Solicitud Experimentales AMERB":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresExperimentalAmerb.aspx";
                            break;



                        case "Detalle del Indicador Relocalización Fusiona":
                            link.PostBackUrl = @"~/Administrador/Reportes/detalleIndicadorRelocalizacionFusiona.aspx";
                            break;

                        case "Resumen de Indicadores Relocalización Sector 0":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresLeyCero.aspx";
                            break;
                            
                        case "Resumen de Indicadores Relocalización Sector":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresLeyCero.aspx";
                            break;
                            
                        case "Detalle del Indicador Relocalización Sector 0":
                            link.PostBackUrl = @"~/Administrador/Reportes/detalleIndicadorRelocalizacionSectorCero.aspx";
                            break;

                        case "Detalle del Indicador Modificación Ampliación":
                            link.PostBackUrl = @"~/Administrador/Reportes/detalleIndicadorModAmpliacion.aspx";
                            break;


                        case "Detalle del Indicador Modificación Reducción":
                            link.PostBackUrl = @"~/Administrador/Reportes/detalleIndicadorModReduccion.aspx";
                            break;


                        case "Resumen de Indicadores Modificación de Especie":
                            link.PostBackUrl = @"~/Administrador/Reportes/resumenIndicadoresModEspecie.aspx";
                            break;
                        case "Detalle del Indicador Modificación de Especie":
                            link.PostBackUrl = @"~/Administrador/Reportes/detalleIndicadorModEspecie.aspx";
                            break;


                        case "Resumen de Indicadores Modificación de Proyecto Técnico":
                            link.PostBackUrl = @"~/Administrador/Reportes/resumenIndicadoresModProyTecnico.aspx";
                            break;
                        case "Detalle del Indicador Modificación de Proyecto Técnico":
                            link.PostBackUrl = @"~/Administrador/Reportes/detalleIndicadorModProyTecnico.aspx";
                            break;


                        case "Detalle del Indicador Modificación Regularización":
                            link.PostBackUrl = @"~/Administrador/Reportes/detalleIndicadorModRegularizacion.aspx";
                            break;


                        case "Resumen de Indicadores Solicitud Centro de Acopio":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresAcopio.aspx";
                            break;
                        case "Detalle del Indicador Solicitud de Acopio":
                            link.PostBackUrl = @"~/Administrador/Reportes/detalleIndicadorAcopio.aspx";
                            break;


                        case "Resumen de Indicadores Solicitudes Centro de Faenamiento":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresFaenamiento.aspx";
                            break;
                        case "Detalle del Indicador Solicitud Faenamiento":
                            link.PostBackUrl = @"~/Administrador/Reportes/detalleIndicadorFaenamiento.aspx";
                            break;


                        case "Resumen de Indicadores Solicitud Acuicultura AMERB":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresAmerb.aspx";
                            break;
                        case "Detalle del Indicador Solicitud Amerb":
                            link.PostBackUrl = @"~/Administrador/Reportes/detalleIndicadorAmerb.aspx";
                            break;

                        case "Resumen de Indicadores Experimentales AMERB":
                            link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadoresExperimentalAmerb.aspx";
                            break;



                        case "Resumen de Indicadores Solicitud Colectores de Semilla":
                            link.PostBackUrl = @"~/Administrador/Reportes/resumenIndicadoresColectores.aspx";
                            break;
                        case "Detalle del Indicador Solicitud Colectores de Semilla":
                            link.PostBackUrl = @"~/Administrador/Reportes/detalleIndicadorColectores.aspx";
                            break;


                        //CIERRE FORZADO
                        case "Administrar Cierres Forzados Solicitud Concesión":
                            link.PostBackUrl = @"~/CierreForzado/administrarCierreForzado.aspx";
                            break;
                        case "Administrar Cierres Forzados Experimentales de Concesión":
                            link.PostBackUrl = @"~/CierreForzado/administrarCierreForzadoExperimentalesConcesion.aspx";
                            break;
                        case "Administrar Cierres Forzados Modificación Concesión":
                            link.PostBackUrl = @"~/CierreForzado/administrarCierreForzadoModificacionConcesion.aspx";
                            break;
                        case "Administrar Cierres Forzados Relocalización LEY":
                            link.PostBackUrl = @"~/CierreForzado/administrarCierreForzadoRelocalizacionLey.aspx";
                            break;
                        case "Administrar Cierres Forzados Relocalización RESA":
                            link.PostBackUrl = @"~/CierreForzado/administrarCierreForzadoRelocalizacionRESA.aspx";
                            break;

                        case "Administrar Cierres Forzados Solicitudes de Centro de Faenamiento":
                            link.PostBackUrl = @"~/CierreForzado/administrarCierreForzadoFaenamiento.aspx";
                            break;
                        case "Administrar Cierres Forzados Modificación Faenamiento":
                            link.PostBackUrl = @"~/CierreForzado/administrarCierreForzadoModificacionCentroFaenamiento.aspx";
                            break;

                        case "Administrar Cierres Forzados Solicitudes de Centro de Acopio":
                            link.PostBackUrl = @"~/CierreForzado/administrarCierreForzadoAcopio.aspx";
                            break;
                        case "Administrar Cierres Forzados Modificación Acopio":
                            link.PostBackUrl = @"~/CierreForzado/administrarCierreForzadoModificacionCentroAcopio.aspx";
                            break;

                        case "Administrar Cierres Forzados de ECMPO":
                            link.PostBackUrl = @"~/CierreForzado/administrarCierreForzadoECMPO.aspx";
                            break;
                        case "Administrar Cierres Forzados Modificación de Acuicultura en ECMPO":
                            link.PostBackUrl = @"~/CierreForzado/administrarCierreForzadoModificacionECMPO.aspx";
                            break;

                        
                        case "Administrar Cierres Forzados Solicitudes de Acuicultura Amerb":
                            link.PostBackUrl = @"~/CierreForzado/administrarCierreForzadoAmerb.aspx";
                            break;
                        case "Administrar Cierres Forzados de Experimentales AMERB":
                            link.PostBackUrl = @"~/CierreForzado/administrarCierreForzadoExperimentalesAmerb.aspx";
                            break;
                        case "Administrar Cierres Forzados Modificación Amerb":
                            link.PostBackUrl = @"~/CierreForzado/administrarCierreForzadoModificacionAmerb.aspx";
                            break;


                        case "Administrar Cierres Forzados Solicitudes de Colectores de Semilla":
                            link.PostBackUrl = @"~/CierreForzado/administrarCierreForzadoColectores.aspx";
                            break;
                        
                        

                        //RELOCALIZACION
                        case "Generar Solicitud de Relocalización":
                            link.PostBackUrl = @"~/Solicitudes/Relocalizacion/preIngresarSolicitudRelocalizacion.aspx";
                            break;

                        case "Generar Solicitud de Relocalización RESA":
                            link.PostBackUrl = @"~/Solicitudes/RelocalizacionRESA/preIngresarSolicitudRelocalizacionRESA.aspx";
                            break;

                        case "Ingreso Informe Relocalización RESA":
                            link.PostBackUrl = @"~/Solicitudes/RelocalizacionRESA/ingresoInformeRESA.aspx";
                            break;

                        case "Administrar Informe Relocalización RESA":
                            link.PostBackUrl = @"~/Solicitudes/RelocalizacionRESA/administrarInformesRESA.aspx";
                            break;



                        /* Unidades Espaciales */
                        case "Centro de Acopio":
                            link.PostBackUrl = @"~/Unidades/Acopio/resumenAcopio.aspx";
                            break;
                        case "Centro en Amerb":
                            link.PostBackUrl = @"~/Unidades/Amerb/resumenAmerb.aspx";
                            break;
                        case "Centro Experimentales en Amerb":
                            link.PostBackUrl = @"~/Unidades/ExperimentalesAmerb/resumenExperimentalesAmerb.aspx";
                            break;
                        case "Centro Acuicultura ECMPO":
                            link.PostBackUrl = @"~/Unidades/ECMPO/resumenECMPO.aspx";
                            break;

                        case "Colector de Semillas":
                            link.PostBackUrl = @"~/Unidades/Colector/resumenColector.aspx";
                            break;
                        case "Centro de Faenamiento":
                            link.PostBackUrl = @"~/Unidades/Faenamiento/resumenFaenamiento.aspx";
                            break;
                       case "Administrar Concesiones de Acuicultura":
                            link.PostBackUrl = @"~/Unidades/Concesion/administrarConcesiones.aspx";
                            break;
                        case "Concesión de Acuicultura":
                            link.PostBackUrl = @"~/Unidades/Concesion/resumenConcesion.aspx";
                            break;

                        /* Solicitudes de Unidades Espaciales  */


                        case "Solicitudes de Experimentales Concesión":
                            link.PostBackUrl = @"~/Solicitudes/ExperimentalesConcesion/ingresarDocumentoExperimentalesConcesion.aspx";
                            break;

                        case "Administrar Trámites de Relocalización":
                            link.PostBackUrl = @"~/Solicitudes/Relocalizacion/administrarSolicitudRelocalizacion.aspx";
                            break;

                        case "Administrar Trámites de Relocalización RESA":
                            link.PostBackUrl = @"~/Solicitudes/RelocalizacionRESA/administrarSolicitudRelocalizacionRESA.aspx";
                            break;

                      


                        case "Solicitud de Centro de Faenamiento":
                            link.PostBackUrl = @"~/Solicitudes/Faenamiento/ingresarDocumentoFaenamiento.aspx";
                            break;

                        case "Solicitud Modificación de Centro de Faenamiento":
                            link.PostBackUrl = @"~/Solicitudes/ModificacionCentroFaenamiento/ingresarDocumentoModificacionCentroFaenamiento.aspx";
                            break;

                        case "Solicitud Modificación de Centro de Acopio":
                            link.PostBackUrl = @"~/Solicitudes/ModificacionCentroAcopio/ingresarDocumentoModificacionCentroAcopio.aspx";
                            break;

                        case "Solicitud Acuicultura en ECMPO":
                            link.PostBackUrl = @"~/Solicitudes/ECMPO/ingresarDocumentoECMPO.aspx";
                            break;

                        case "Solicitud Modificación Acuicultura en ECMPO":
                            link.PostBackUrl = @"~/Solicitudes/ModificacionECMPO/ingresarDocumentoModificacionECMPO.aspx";
                            break;


                        case "Solicitud Acuicultura Amerb":
                            link.PostBackUrl = @"~/Solicitudes/Amerb/ingresarDocumentoAmerb.aspx";
                            break;
 
                        case "Solicitud Experimentales en AMERB":
                            link.PostBackUrl = @"~/Solicitudes/ExperimentalesAmerb/ingresarDocumentoExperimentalesAmerb.aspx";
                            break;
                            
                        case "Solicitud Modificación de AMERB":
                            link.PostBackUrl = @"~/Solicitudes/ModificacionAmerb/ingresarDocumentoModificacionAmerb.aspx";
                            break;

                        case "Solicitud Colectores de Semillas":
                            link.PostBackUrl = @"~/Solicitudes/Colector/ingresarDocumentoColector.aspx";
                            break;

                        case "Administrar Solicitudes de Modificación de Concesión":
                            link.PostBackUrl = @"~/Solicitudes/Modificacion/administrarSolicitudModificacion.aspx";
                            break;

                        case "Administrar Solicitudes de Modificación de Centro de Faenamiento":
                            link.PostBackUrl = @"~/Solicitudes/ModificacionCentroFaenamiento/administrarSolicitudModificacionCentroFaenamiento.aspx";
                            break;

                        case "Administrar Solicitudes de Modificación de ECMPO":
                            link.PostBackUrl = @"~/Solicitudes/ModificacionECMPO/administrarSolicitudModificacionECMPO.aspx";
                            break;
                             

                        case "Administrar Solicitudes de Modificación de Centro de Acopio":
                            link.PostBackUrl = @"~/Solicitudes/ModificacionCentroAcopio/administrarSolicitudModificacionCentroAcopio.aspx";
                            break;

                        case "Administrar Solicitudes de Modificación de Amerb":
                            link.PostBackUrl = @"~/Solicitudes/ModificacionAmerb/administrarSolicitudModificacionAmerb.aspx";
                            break;
   

                    };
                    break;

                //case "detalleEstado.aspx":
                //    // Se recibe el id_solicitud
                //    try
                //    {
                //        if (Request.QueryString["id_solicitud"] != null)
                //        {
                //            Datos.Entidades.Solicitudes conn1 = new Datos.Entidades.Solicitudes();
                //            int id_solicitud = Convert.ToInt32(Request.QueryString["id_solicitud"]);
                            
                //            // Obtenemos el estado de la solicitud
                //            DataTable dt = conn1.Solicitudes_Ver(id_solicitud);
                //            // Asignamos los valores de los parametros segun corresponda
                //            parametros = "?id_estado=" + Convert.ToString(dt.Rows[0]["IdEstado"]);
                //            HT_ModReportes = (Hashtable)Session["Modulo_Reportes"];
                //            if (HT_ModReportes != null)
                //            {
                //                Hashtable HT_ListSolicitudes = (Hashtable)HT_ModReportes["ListSolicitudes"];
                //                if (HT_ListSolicitudes != null)
                //                {
                //                    HT_ListSolicitudes["locked"] = false;
                //                    parametros = parametros + "&tipo=" + Convert.ToString((int)HT_ListSolicitudes["tipo"]);
                //                    HT_ModReportes["ListSolicitudes"] = (Hashtable)HT_ListSolicitudes;
                //                    Session["Modulo_Reportes"] = (Hashtable)HT_ModReportes;
                //                };
                //            };
                //        };
                //    }
                //    catch
                //    { };

                //    link.Text = "Detalle del Estado";
                //    link.PostBackUrl = @"~/Administrador/Reportes/detalleEstado.aspx" + parametros;
                //    break;
                case "detalleIndicador.aspx":
                    HT_ModReportes = (Hashtable)Session["Modulo_Reportes"];
                    if (HT_ModReportes != null)
                    {
                        Hashtable HT_detalleIndicador = (Hashtable)HT_ModReportes["detalleIndicador"];
                        if (HT_detalleIndicador != null)
                        {
                            HT_detalleIndicador["locked"] = false;
                            parametros = "?indicador=" + Convert.ToString((int)HT_detalleIndicador["indicador"]);
                            HT_ModReportes["detalleIndicador"] = (Hashtable)HT_detalleIndicador;
                            Session["Modulo_Reportes"] = (Hashtable)HT_ModReportes;
                        };
                    };
                    link.Text = "Detalle del Indicador";
                    link.PostBackUrl = @"~/Administrador/Reportes/detalleIndicador.aspx" + parametros;
                    break;
                case "resumenEstados.aspx":
                    link.Text = "Resumen de Estados";
                    link.PostBackUrl = @"~/Administrador/Reportes/resumenEstados.aspx";
                    break;
                case "resumenIndicadores.aspx":
                    link.Text = "Resumen de Indicadores Solicitud Concesión";
                    link.PostBackUrl = @"~/Administrador/IndicadoresP3/resumenIndicadores.aspx";
                    break;

                case "listUsuarios.aspx":
                    link.Text = "Administración de Usuarios";
                    link.PostBackUrl = @"~/Administrador/Usuarios/listUsuarios.aspx";
                    break;
                
                
                case "informesResoluciones.aspx":
                    if (modulo == "Registrar")
                    {
                        link.Text = "Solicitud Concesión de Acuicultura";
                        link.PostBackUrl = @"~/Solicitudes/Registrar/ingresarDocumento.aspx";
                        break;
                    }
                    else {
                        link.Text = "Solicitud de Modificación de Concesión";
                        link.PostBackUrl = @"~/Solicitudes/Modificacion/ingresarDocumento.aspx";
                        break;
                    
                    }
                case "ingresarDocumento.aspx":
                    if (modulo == "Registrar")
                    {
                        link.Text = "Solicitud Concesión de Acuicultura";
                        link.PostBackUrl = @"~/Solicitudes/Registrar/ingresarDocumento.aspx";
                        break;
                    }
                    else {
                        link.Text = "Solicitud de Modificación de Concesión";
                        link.PostBackUrl = @"~/Solicitudes/Modificacion/ingresarDocumento.aspx";
                        break;
                    }
                case "general.aspx":
                    link.Text = "Solicitud Concesión de Acuicultura";
                    link.PostBackUrl = @"~/Solicitudes/Registrar/ingresarDocumento.aspx";
                    break;
                case "administrarDocumento.aspx":
                    if (modulo == "Registrar")
                    {
                        link.Text = "Solicitud Concesión de Acuicultura";
                        link.PostBackUrl = @"~/Solicitudes/Registrar/ingresarDocumento.aspx";
                        break;
                    }
                    else {
                        link.Text = "Solicitud de Modificación de Concesión";
                        link.PostBackUrl = @"~/Solicitudes/Modificacion/ingresarDocumento.aspx";
                        break;
                    }

                case "verDocumento.aspx":
                    if (modulo == "Registrar")
                    {
                        link.Text = "Solicitud Concesión de Acuicultura";
                        link.PostBackUrl = @"~/Solicitudes/Registrar/ingresarDocumento.aspx";
                        break;
                    }
                    if (modulo == "Solicitudes")
                        {
                            link.Text = " Solicitud Acuicultura en ECMPO";
                            link.PostBackUrl = @"~/Solicitudes/ECMPO/ingresarDocumentoECMPO.aspx";
                            break;
                        }
                        else
                        {
                            link.Text = "Solicitud de Modificación de Concesión";
                            link.PostBackUrl = @"~/Solicitudes/Modificacion/ingresarDocumento.aspx";
                            break;
                        }
                    


                case "identificacionSolicitante.aspx":
                    link.Text = "Identificación del Solicitante";
                    link.PostBackUrl = @"~/Solicitudes/Registrar/identificacionSolicitante.aspx";
                    break;

                case "identificacionSolicitanteFaenamiento.aspx":
                    link.Text = "Identificación del Solicitante";
                    link.PostBackUrl = @"~/Solicitudes/Faenamiento/identificacionSolicitanteFaenamiento.aspx";
                    break;

                case "identificacionSolicitanteColector.aspx":
                    link.Text = "Identificación del Solicitante";
                    link.PostBackUrl = @"~/Solicitudes/Colector/identificacionSolicitanteColector.aspx";
                    break;

                case "identificacionTitularExperimentalesAmerb.aspx":
                    link.Text = "Identificación del Solicitante";
                    link.PostBackUrl = @"~/Solicitudes/ExperimentalesAmerb/identificacionTitularExperimentalesAmerb.aspx";
                    break;

                case "antecedDelSector.aspx":
                    if (modulo == "Registrar")
                    {
                        link.Text = "Solicitud Concesión de Acuicultura";
                        link.PostBackUrl = @"~/Solicitudes/Registrar/ingresarDocumento.aspx";
                        break;
                    }
                    else {
                        link.Text = "Solicitud de Modificación de Concesión";
                        link.PostBackUrl = @"~/Solicitudes/Modificacion/ingresarDocumento.aspx";
                        break;
                    }
                case "proyTecnico.aspx":
                    if (modulo == "Registrar")
                    {
                        link.Text = "Solicitud Concesión de Acuicultura";
                        link.PostBackUrl = @"~/Solicitudes/Registrar/ingresarDocumento.aspx";
                        break;
                    }
                    else {
                        link.Text = "Solicitud de Modificación de Concesión";
                        link.PostBackUrl = @"~/Solicitudes/Modificacion/ingresarDocumento.aspx";
                        break;
                    }
                case "unidadEspacial.aspx":
                    link.Text = "Solicitud Concesión de Acuicultura";
                    link.PostBackUrl = @"~/Solicitudes/Registrar/ingresarDocumento.aspx";
                    break;

                case "detalleTitular.aspx":
                    if (modulo == "Registrar")
                    {
                        link.Text = "Identificación del Solicitante";
                        link.PostBackUrl = @"~/Solicitudes/Registrar/identificacionSolicitante.aspx";
                        break;
                    }
                    else if (modulo == "Ver Titular")
                    {
                        link.Text = "Ver Titular";

                        acc = 0;
                        idMatrizSuc = 0;

                        try
                        {
                            if (Request.QueryString["acc"] != null)
                            {
                                acc = Convert.ToInt32(Request.QueryString["acc"]);
                            };
                            if (Request.QueryString["idMatrizSuc"] != null)
                            {
                                idMatrizSuc = Convert.ToInt32(Request.QueryString["idMatrizSuc"]);
                            };
                        }
                        catch
                        { };

                        rutPersona = "";
                        matrizSucursalAux = matrizSucursalDA.ObtenerTitularMatrizSucFiltros(0, idMatrizSuc);

                        if (matrizSucursalAux != null)
                        {
                            rutPersona = Convert.ToString(matrizSucursalAux.persona.rutPersona);
                        }

                        link.PostBackUrl = @"~/Mantenedores/Titulares/detalleTitular.aspx?rutPersona=" + rutPersona + "&bp=4";
                        break;
                    }
                    else {
                        link.Text = "Identificación del Titular de la Concesión";
                        link.PostBackUrl = @"~/Solicitudes/Modificacion/identificacionTitularConcesion.aspx";
                        break;
                    }

                //Modificación
                case "datosConcesionAcuicultura.aspx":
                    link.Text = "Datos Concesión de Acuicultura";
                    link.PostBackUrl = @"~/Solicitudes/Modificacion/datosConcesionAcuicultura.aspx";
                    break;

                case "identificacionTitularConcesion.aspx":
                    link.Text = "Identificación del Titular de la Concesión";
                    link.PostBackUrl = @"~/Solicitudes/Modificacion/identificacionTitularConcesion.aspx";
                    break;

                case "referenciaGlobal.aspx":
                    link.Text = "Referencia Global";
                    link.PostBackUrl = @"~/Solicitudes/Modificacion/referenciaGlobal.aspx";
                    break;

                case "unidadesEspaciales.aspx":
                    link.Text = "Modificación de Concesión";
                    link.PostBackUrl = @"~/Solicitudes/Modificacion/unidadesEspaciales.aspx";
                    break;

                //Ver
                case "identificacionSolicitanteVer.aspx":
                    link.Text = "Identificación del Solicitante";
                    link.PostBackUrl = @"~/Solicitudes/Ver/identificacionSolicitanteVer.aspx";
                    break;
                case "referenciasGeneralesVer.aspx":
                    link.Text = "Referencia Global";
                    link.PostBackUrl = @"~/Solicitudes/Ver/referenciasGeneralesVer.aspx";
                    break;
                case "proyTecnicoVer.aspx":
                    link.Text = "Proyecto Técnico";
                    link.PostBackUrl = @"~/Solicitudes/Ver/proyTecnicoVer.aspx";
                    break;
                case "informesResolucionesVer.aspx":
                    link.Text = "Informes y Resoluciones";
                    link.PostBackUrl = @"~/Solicitudes/Ver/informesResolucionesVer.aspx";
                    break;
                case "unidadEspacialVer.aspx":
                    link.Text = "Unidad Espacial";
                    link.PostBackUrl = @"~/Solicitudes/Ver/unidadEspacialVer.aspx";
                    break;
                case "titularConcesion.aspx":
                    link.Text = "Titular";
                    link.PostBackUrl = @"~/Unidades/Concesion/titularConcesion.aspx";
                    break;
              
                /* Mantenedores */
                case "administrarTitulares.aspx":
                    link.Text = "Administrar Titulares";
                    link.PostBackUrl = @"~/Mantenedores/Titulares/administrarTitulares.aspx";
                    break;
                case "agregarTitular.aspx":

                    try
                        {
                            if (Request.QueryString["acc"] != null)
                            {
                                acc = Convert.ToInt32(Request.QueryString["acc"]);
                            };
                            if (Request.QueryString["rutPersona"] != null)
                            {
                                rutPersona = Convert.ToString(Request.QueryString["rutPersona"]);
                            };
                        }
                    catch
                    { };

                    if (acc == 2)
                    {
                        link.Text = "Modificar Titular";
                        link.PostBackUrl = @"~/Mantenedores/Titulares/agregarTitular.aspx?rutPersona=" + rutPersona.Split('-')[0] + "&acc=2";
                    }
                    else {
                        link.Text = "Crear Titular";
                        link.PostBackUrl = @"~/Mantenedores/Titulares/agregarTitular.aspx";
                    }

                    
                    break;

                case "administrarRepresentantesLegales.aspx":
                    link.Text = "Administrar Representantes Legales";
                    link.PostBackUrl = @"~/Mantenedores/Titulares/administrarRepresentantesLegales.aspx";
                    break;

                case "agregarRepresentanteLegal.aspx":

                    try
                        {
                            if (Request.QueryString["acc"] != null)
                            {
                                acc = Convert.ToInt32(Request.QueryString["acc"]);
                            };
                            if (Request.QueryString["rutPersona"] != null)
                            {
                                rutPersona = Convert.ToString(Request.QueryString["rutPersona"]);
                            };
                        }
                    catch
                    { };

                    if (acc == 2)
                    {

                        link.Text = "Modificar Representante Legal";
                        link.PostBackUrl = @"~/Mantenedores/Titulares/agregarRepresentanteLegal.aspx?rutPersona=" + rutPersona.Split('-')[0] + "&acc=2";
                    }
                    else {

                        link.Text = "Crear Representante Legal";
                        link.PostBackUrl = @"~/Mantenedores/Titulares/agregarRepresentanteLegal.aspx";
                    }

                    break;
                case "administrarOperadores.aspx":
                    link.Text = "Administrar Operadores";
                    link.PostBackUrl = @"~/Mantenedores/Titulares/administrarOperadores.aspx";
                    break;
                case "agregarOperador.aspx":

                    try
                        {
                            if (Request.QueryString["acc"] != null)
                            {
                                acc = Convert.ToInt32(Request.QueryString["acc"]);
                            };
                            if (Request.QueryString["rutPersona"] != null)
                            {
                                rutPersona = Convert.ToString(Request.QueryString["rutPersona"]);
                            };
                        }
                    catch
                    { };

                    if (acc == 2)
                    {

                        link.Text = "Modificar Representante Legal";
                        link.PostBackUrl = @"~/Mantenedores/Titulares/agregarOperador.aspx?rutPersona=" + rutPersona.Split('-')[0] + "&acc=2";
                    }
                    else {

                        link.Text = "Crear Operador";
                        link.PostBackUrl = @"~/Mantenedores/Titulares/agregarOperador.aspx";
                    }

                    
                    break;

                /* Modificación */
                case "proyTecnicoModificacion.aspx":
                    link.Text = "Proyecto Técnico";
                    link.PostBackUrl = @"~/Solicitudes/Modificacion/proyTecnicoModificacion.aspx";
                    break;
                case "generalModificacion.aspx":
                    link.Text = "Referencia Global";
                    link.PostBackUrl = @"~/Solicitudes/Modificacion/generalModificacion.aspx";
                    break;
                case "antecedDelSectorModificacion.aspx":
                    link.Text = "Antecedentes del Sector";
                    link.PostBackUrl = @"~/Solicitudes/Modificacion/antecedDelSectorModificacion.aspx";
                    break;

                /* Mantenedor General */
                case "region.aspx":
                    link.Text = "Región";
                    link.PostBackUrl = @"~/Mantenedores/Generales/region.aspx";
                    break;
                case "provincia.aspx":
                    link.Text = "Provincia";
                    link.PostBackUrl = @"~/Mantenedores/Generales/provincia.aspx";
                    break;
                case "comuna.aspx":
                    link.Text = "Comuna";
                    link.PostBackUrl = @"~/Mantenedores/Generales/comuna.aspx";
                    break;
                case "tipoBarrio.aspx":
                    link.Text = "Tipo Barrio";
                    link.PostBackUrl = @"~/Mantenedores/Generales/tipoBarrio.aspx?bp=1";
                    break;
                case "barrio.aspx":
                    link.Text = "Barrio";
                    link.PostBackUrl = @"~/Mantenedores/Generales/barrioTipo.aspx";
                    break;
                case "especieEtapaDesarrollo.aspx":
                    link.Text = "Asociación Especie - Etapa de Cultivo";
                    link.PostBackUrl = @"~/Mantenedores/Generales/especieEtapaDesarrollo.aspx";
                    break;
                case "macrozona.aspx":
                    link.Text = "Macrozona";
                    link.PostBackUrl = @"~/Mantenedores/Generales/macrozona.aspx";
                    break;
                case "carta.aspx":
                    link.Text = "Carta SHOA / IGM Plano";
                    link.PostBackUrl = @"~/Mantenedores/Generales/carta.aspx";
                    break;
                case "datum.aspx":
                    link.Text = "Datum";
                    link.PostBackUrl = @"~/Mantenedores/Generales/datum.aspx";
                    break;
                case "tipoVertice.aspx":
                    link.Text = "Tipo Vértice";
                    link.PostBackUrl = @"~/Mantenedores/Generales/tipoVertice.aspx?bp=1";
                    break;
                case "tipoContacto.aspx":
                    link.Text = "Tipo Vértice";
                    link.PostBackUrl = @"~/Mantenedores/Generales/tipoContacto.aspx?bp=1";
                    break;
                case "huso.aspx":
                    link.Text = "Tipo Huso";
                    link.PostBackUrl = @"~/Mantenedores/Generales/huso.aspx";
                    break;
                case "tipoConcesion.aspx":
                    link.Text = "Tipo Concesión";
                    link.PostBackUrl = @"~/Mantenedores/Generales/tipoConcesion.aspx";
                    break;
                case "tipoUso.aspx":
                    link.Text = "Tipo Uso";
                    link.PostBackUrl = @"~/Mantenedores/Generales/tipoUso.aspx?bp=1";
                    break;
                case "tipoCultivo.aspx":
                    link.Text = "Tipo Cultivo";
                    link.PostBackUrl = @"~/Mantenedores/Generales/tipoCultivo.aspx?bp=1";
                    break;
                case "metodoCultivo.aspx":
                    link.Text = "Método Cultivo Algas";
                    link.PostBackUrl = @"~/Mantenedores/Generales/metodoCultivo.aspx?bp=1";
                    break;
                case "tipoAlimento.aspx":
                    link.Text = "Tipo Alimento";
                    link.PostBackUrl = @"~/Mantenedores/Generales/tipoAlimento.aspx?bp=1";
                    break;
                case "especie.aspx":
                    link.Text = "Especie de Cultivo";
                    link.PostBackUrl = @"~/Mantenedores/Generales/especie.aspx";
                    break;
                case "etapaDeDesarrollo.aspx":
                    link.Text = "Etapa de Cultivo";
                    link.PostBackUrl = @"~/Mantenedores/Generales/etapaDeDesarrollo.aspx";
                    break;
                case "estructuraTecnica.aspx":
                    link.Text = "Tipo de Estructura";
                    link.PostBackUrl = @"~/Mantenedores/Generales/estructuraTecnica.aspx";
                    break;
                case "formaEstructura.aspx":
                    link.Text = "Forma Estructura";
                    link.PostBackUrl = @"~/Mantenedores/Generales/formaEstructura.aspx";
                    break;
                case "unidadMedidaEstructuraTecnica.aspx":
                    link.Text = "Unidad Medida Estructura";
                    link.PostBackUrl = @"~/Mantenedores/Generales/unidadMedidaEstructuraTecnica.aspx";
                    break;
                case "volumenUnidadMedida.aspx":
                    link.Text = "Volumen Unidad Medida Estructura";
                    link.PostBackUrl = @"~/Mantenedores/Generales/volumenUnidadMedida.aspx?bp=1";
                    break;
                case "anio.aspx":
                    link.Text = "Años";
                    link.PostBackUrl = @"~/Mantenedores/Generales/anio.aspx?bp=1";
                    break;
                case "unidadEjemplar.aspx":
                    link.Text = "Unidad Ejemplar";
                    link.PostBackUrl = @"~/Mantenedores/Generales/unidadEjemplar.aspx?bp=1";
                    break;
                case "rangoPesoEjemplar.aspx":
                    link.Text = "Rango Peso Ejemplar";
                    link.PostBackUrl = @"~/Mantenedores/Generales/unidadEjemplar.aspx?bp=1";
                    break;
                case "capitaniaPuerto.aspx":
                    link.Text = "Capitanía de Puerto";
                    link.PostBackUrl = @"~/Mantenedores/Generales/capitaniaPuerto.aspx";
                    break;
                case "tipoArchivoTitular.aspx":
                    link.Text = "Tipo Archivo Titulares";
                    link.PostBackUrl = @"~/Mantenedores/Generales/tipoArchivoTitular.aspx?bp=1";
                    break;
                case "tipoArchivoAntEspaciales.aspx":
                    link.Text = "Tipo Archivo Coord. Originales";
                    link.PostBackUrl = @"~/Mantenedores/Generales/tipoArchivoTitular.aspx?bp=1";
                    break;
                case "tipoArchivoAntTerreno.aspx":
                    link.Text = "Tipo Archivo Coord. Entrega de Material";
                    link.PostBackUrl = @"~/Mantenedores/Generales/tipoArchivoAntTerreno.aspx";
                    break;
                case "tipoArchivoRegularizacion.aspx":
                    link.Text = "Tipo Archivo Coord. Regularización";
                    link.PostBackUrl = @"~/Mantenedores/Generales/tipoArchivoRegularizacion.aspx";
                    break;
                case "tipoCentroAcopio.aspx":
                    link.Text = "Tipo Centro Acopio";
                    link.PostBackUrl = @"~/Mantenedores/Generales/tipoCentroAcopio.aspx";
                    break;
                case "tipoCentroFaenamiento.aspx":
                    link.Text = "Tipo Centro Faenamiento";
                    link.PostBackUrl = @"~/Mantenedores/Generales/tipoCentroFaenamiento.aspx";
                    break;
                case "tipoCuerpoAgua.aspx":
                    link.Text = "Tipo Cuerpo de Agua";
                    link.PostBackUrl = @"~/Mantenedores/Generales/tipoCuerpoAgua.aspx";
                    break;
                case "cuerpoDeAgua.aspx":
                    link.Text = "Cuerpo de Agua";
                    link.PostBackUrl = @"~/Mantenedores/Generales/cuerpoDeAgua.aspx";
                    break;
                case "plazoNominal.aspx":
                    link.Text = "Plazo Nominal";
                    link.PostBackUrl = @"~/Mantenedores/Generales/plazoNominal.aspx";
                    break;
                case "tipoOrganizacion.aspx":
                    link.Text = "Tipo Organización";
                    link.PostBackUrl = @"~/Mantenedores/Generales/tipoOrganizacion.aspx";
                    break;
                case "descasoACS.aspx":
                    link.Text = "Descanso ACS";
                    link.PostBackUrl = @"~/Mantenedores/Generales/descasoACS.aspx";
                    break;

                /* Reportes */
                case "reportesConcesion.aspx":
                    link.Text = "Reportes";
                    link.PostBackUrl = @"~/Administrador/ReportesP3/reportesConcesion.aspx";
                    break;
                case "ReportesExperimentalConcesion.aspx":
                    link.Text = "Reportes";
                    link.PostBackUrl = @"~/Administrador/ReportesP3/ReportesExperimentalConcesion.aspx";
                    break;
                case "reportesAcuiculturaAmerb.aspx":
                    link.Text = "Reportes";
                    link.PostBackUrl = @"~/Administrador/ReportesP3/reportesAcuiculturaAmerb.aspx";
                    break;
                case "ReportesExperimentalAmerb.aspx":
                    link.Text = "Reportes";
                    link.PostBackUrl = @"~/Administrador/ReportesP3/ReportesExperimentalAmerb.aspx";
                    break;
                case "reportesCentroFaenamiento.aspx":
                    link.Text = "Reportes";
                    link.PostBackUrl = @"~/Administrador/ReportesP3/reportesCentroFaenamiento.aspx";
                    break;
                case "reportesCentroAcopio.aspx":
                    link.Text = "Reportes";
                    link.PostBackUrl = @"~/Administrador/ReportesP3/reportesCentroAcopio.aspx";
                    break;
                case "reportesColectoresSemillas.aspx":
                    link.Text = "Reportes";
                    link.PostBackUrl = @"~/Administrador/ReportesP3/reportesColectoresSemillas.aspx";
                    break;
                case "reportesECMPO.aspx":
                    link.Text = "Reportes";
                    link.PostBackUrl = @"~/Administrador/ReportesP3/reportesECMPO.aspx";
                    break;
                
                /* Reportes Solicitudes */

                case "reportesSolicitudesAcuicultura.aspx":
                    link.Text = "Reportes";
                    link.PostBackUrl = @"~/Administrador/ReportesP3/reportesSolicitudesAcuicultura.aspx";
                    break;
                case "reportesSolicitudesFaenamiento.aspx":
                    link.Text = "Reportes";
                    link.PostBackUrl = @"~/Administrador/ReportesP3/reportesSolicitudesFaenamiento.aspx";
                    break;
                case "reportesSolicitudesAcopio.aspx":
                    link.Text = "Reportes";
                    link.PostBackUrl = @"~/Administrador/ReportesP3/reportesSolicitudesAcopio.aspx";
                    break;
                case "reportesSolicitudesSemillas.aspx":
                    link.Text = "Reportes";
                    link.PostBackUrl = @"~/Administrador/ReportesP3/reportesSolicitudesSemillas.aspx";
                    break;
                case "reportesSolicitudesECMPO.aspx":
                    link.Text = "Reportes";
                    link.PostBackUrl = @"~/Administrador/ReportesP3/reportesSolicitudesECMPO.aspx";
                    break;
                case "reportesSolicitudesAMERB.aspx":
                    link.Text = "Reportes";
                    link.PostBackUrl = @"~/Administrador/ReportesP3/reportesSolicitudesAMERB.aspx";
                    break;

                /* Mantenedor Transversal */
                case "resultado.aspx":
                    link.Text = "Resultados";
                    link.PostBackUrl = @"~/Mantenedores/Transversales/resultado.aspx";
                    break;
                case "AsocSubrequerimientoResultado.aspx":
                    link.Text = "Asociación Subrequerimiento - Resultados";
                    link.PostBackUrl = @"~/Mantenedores/Transversales/AsocSubrequerimientoResultado.aspx";
                    break;
                case "tipoDocumento.aspx":
                    link.Text = "Tipo Documento";
                    link.PostBackUrl = @"~/Mantenedores/Transversales/tipoDocumento.aspx";
                    break;
                case "asocSubrequerimientoTipo.aspx":
                    link.Text = "Asociación Subrequerimiento - Tipo Documento";
                    link.PostBackUrl = @"~/Mantenedores/Transversales/asocSubrequerimientoTipo.aspx";
                    break;
                case "temaSubrequerimiento.aspx":
                    link.Text = "Tema Subrequerimiento";
                    link.PostBackUrl = @"~/Mantenedores/Transversales/temaSubrequerimiento.aspx";
                    break;
                case "responsableDAC.aspx":
                    link.Text = "Responsable DAC";
                    link.PostBackUrl = @"~/Mantenedores/Transversales/responsableDAC.aspx";
                    break;
                case "preferenciaRelocalizacion.aspx":
                    link.Text = "Preferencia de Relocalización";
                    link.PostBackUrl = @"~/Mantenedores/Transversales/preferenciaRelocalizacion.aspx";
                    break;


                /* Mantenedor de Personas */
                case "verRepresentanteLegal.aspx":
                    link.Text = "Ver Representante Legal";
                    
                        acc = 0;
                        idMatrizSuc = 0;

                        try
                        {
                            if (Request.QueryString["acc"] != null)
                            {
                                acc = Convert.ToInt32(Request.QueryString["acc"]);
                            };
                            if (Request.QueryString["idMatrizSuc"] != null)
                            {
                                idMatrizSuc = Convert.ToInt32(Request.QueryString["idMatrizSuc"]);
                            };
                        }
                        catch
                        { };

                        rutPersona = "";
                        matrizSucursalAux = repLegalDA.ObtenerRepresentanteMatrizSuc_filtros(0, idMatrizSuc);

                        if (matrizSucursalAux != null)
                        {
                            rutPersona = Convert.ToString(matrizSucursalAux.persona.rutPersona);
                        }
                        link.PostBackUrl = @"~/Mantenedores/Titulares/verRepresentanteLegal.aspx?rutPersona=" + rutPersona;
                    break;
                case "verContactoRepresentanteLegal.aspx":
                    link.Text = "Ver Contacto Representante Legal";
                    link.PostBackUrl = @"~/Mantenedores/Titulares/verContactoRepresentanteLegal.aspx";
                    break;
                case "verContactoTitular.aspx":
                    link.Text = "Ver Contacto Titular";
                    link.PostBackUrl = @"~/Mantenedores/Titulares/verContactoTitular.aspx";
                    break;
                case "verOperador.aspx":
                    link.Text = "Ver Operador";
                    acc = 0;
                        idMatrizSuc = 0;

                        try
                        {
                            if (Request.QueryString["acc"] != null)
                            {
                                acc = Convert.ToInt32(Request.QueryString["acc"]);
                            };
                            if (Request.QueryString["idMatrizSuc"] != null)
                            {
                                idMatrizSuc = Convert.ToInt32(Request.QueryString["idMatrizSuc"]);
                            };
                        }
                        catch
                        { };

                        rutPersona = "";
                        matrizSucursalAux = operadorDA.ObtenerOperadorMatrizSuc_filtros(0, idMatrizSuc);

                        if (matrizSucursalAux != null)
                        {
                            rutPersona = Convert.ToString(matrizSucursalAux.persona.rutPersona);
                        }

                    link.PostBackUrl = @"~/Mantenedores/Titulares/verOperador.aspx?rutPersona=" + rutPersona;
                    break;
                case "verContactoOperador.aspx":
                    link.Text = "Ver Contacto Operador";
                    link.PostBackUrl = @"~/Mantenedores/Titulares/verContactoOperador.aspx";
                    break;
                case "agregarNombres.aspx":
                    link.Text = "Ver Contacto Operador";
                    link.PostBackUrl = @"~/Mantenedores/Titulares/agregarNombres.aspx";
                    break;
                case "agregarContactoDireccion.aspx":
                    link.Text = "Ver Contacto Dirección";
                    link.PostBackUrl = @"~/Mantenedores/Titulares/agregarContactoDireccion.aspx";
                    break;
                case "asociarRepresentanteLegal.aspx":
                    link.Text = "Asociar Representante Legal";
                    link.PostBackUrl = @"~/Mantenedores/Titulares/asociarRepresentanteLegal.aspx";
                    break;
                case "asociarOperador.aspx":
                    link.Text = "Asociar Operador";
                    link.PostBackUrl = @"~/Mantenedores/Titulares/asociarOperador.aspx";
                    break;

               /* Administrador de Unidades Espaciles - Nuevo - */
                case "antecedentesDelSectorAcopio.aspx":
                    link.Text = "Antecedentes del Sector";
                    link.PostBackUrl = @"~/Unidades/Acopio/antecedentesDelSectorAcopio.aspx";
                    break;
                case "antecedentesDelSectorAmerb.aspx":
                    link.Text = "Antecedentes del Sector";
                    link.PostBackUrl = @"~/Unidades/Amerb/antecedentesDelSectorAmerb.aspx";
                    break;
                case "antecedentesDelSectorExperimentalesAmerb.aspx":
                    link.Text = "Antecedentes del Sector";
                    link.PostBackUrl = @"~/Unidades/ExperimentalesAmerb/antecedentesDelSectorExperimentalesAmerb.aspx";
                    break;
                case "antecedentesDelSectorColector.aspx":
                    link.Text = "Antecedentes del Sector";
                    link.PostBackUrl = @"~/Unidades/Colector/antecedentesDelSectorColector.aspx";
                    break;
                case "antecedentesDelSectorFaenamiento.aspx":
                    link.Text = "Antecedentes del Sector";
                    link.PostBackUrl = @"~/Unidades/Faenamiento/antecedentesDelSectorFaenamiento.aspx";
                    break;
                case "antecedentesDelSectorECMPO.aspx":
                    link.Text = "Antecedentes del Sector";
                    link.PostBackUrl = @"~/Unidades/ECMPO/antecedentesDelSectorECMPO.aspx";
                    break;
                case "antecedentesDelSectorConcesion.aspx":
                    link.Text = "Antecedentes del Sector";
                    link.PostBackUrl = @"~/Unidades/Concesion/antecedentesDelSectorConcesion.aspx";
                    break;
                case "proyectoTecnicoConcesion.aspx":
                    link.Text = "Proyecto Técnico";
                    link.PostBackUrl = @"~/Unidades/Concesion/proyectoTecnicoConcesion.aspx";
                    break;

                case "administrarDocumentoConcesion.aspx":
                    link.Text = "Administrar Documento";
                    link.PostBackUrl = @"~/Unidades/Concesion/administrarDocumentoConcesion.aspx";
                    break;
                    
                case "proyectoTecnicoAcopio.aspx":
                    link.Text = "Proyecto Técnico";
                    link.PostBackUrl = @"~/Unidades/Acopio/proyectoTecnicoAcopio.aspx";
                    break;
                case "proyectoTecnicoAmerb.aspx":
                    link.Text = "Proyecto Técnico";
                    link.PostBackUrl = @"~/Unidades/Amerb/proyectoTecnicoAmerb.aspx";
                    break;
                case "proyectoTecnicoExperimentalesAmerb.aspx":
                    link.Text = "Proyecto Técnico";
                    link.PostBackUrl = @"~/Unidades/ExperimentalesAmerb/proyectoTecnicoExperimentalesAmerb.aspx";
                    break;
                case "proyectoTecnicoECMPO.aspx":
                    link.Text = "Proyecto Técnico";
                    link.PostBackUrl = @"~/Unidades/ECMPO/proyectoTecnicoECMPO.aspx";
                    break;
                case "proyectoTecnicoColector.aspx":
                    link.Text = "Proyecto Técnico";
                    link.PostBackUrl = @"~/Unidades/Colector/proyectoTecnicoColector.aspx";
                    break;
                case "proyectoTecnicoFaenamiento.aspx":
                    link.Text = "Proyecto Técnico";
                    link.PostBackUrl = @"~/Unidades/Faenamiento/proyectoTecnicoFaenamiento.aspx";
                    break;
                case "referenciasAmbientalesAcopio.aspx":
                    link.Text = "Referencias Ambientales";
                    link.PostBackUrl = @"~/Unidades/Acopio/referenciasAmbientalesAcopio.aspx";
                    break;
                case "referenciasAmbientalesAmerb.aspx":
                    link.Text = "Referencias Ambientales";
                    link.PostBackUrl = @"~/Unidades/Amerb/referenciasAmbientalesAmerb.aspx";
                    break;
                case "referenciasAmbientalesExperimentalesAmerb.aspx":
                    link.Text = "Referencias Ambientales";
                    link.PostBackUrl = @"~/Unidades/ExperimentalesAmerb/referenciasAmbientalesExperimentalesAmerb.aspx";
                    break;
                case "referenciasSanitariasAmerb.aspx":
                    link.Text = "Referencias Ambientales";
                    link.PostBackUrl = @"~/Unidades/Amerb/referenciasSanitariasAmerb.aspx";
                    break;
                case "referenciasSanitariasExperimentalesAmerb.aspx":
                    link.Text = "Referencias Ambientales";
                    link.PostBackUrl = @"~/Unidades/ExperimentalesAmerb/referenciasSanitariasExperimentalesAmerb.aspx";
                    break;
                case "referenciasSanitariasECMPO.aspx":
                    link.Text = "Referencias Ambientales";
                    link.PostBackUrl = @"~/Unidades/Amerb/referenciasSanitariasECMPO.aspx";
                    break;
                case "referenciasAmbientalesECMPO.aspx":
                    link.Text = "Referencias Ambientales";
                    link.PostBackUrl = @"~/Unidades/Amerb/referenciasAmbientalesECMPO.aspx";
                    break;
                case "referenciasAmbientalesColector.aspx":
                    link.Text = "Referencias Ambientales";
                    link.PostBackUrl = @"~/Unidades/Colector/referenciasAmbientalesColector.aspx";
                    break;
                case "referenciasAmbientalesFaenamiento.aspx":
                    link.Text = "Referencias Ambientales";
                    link.PostBackUrl = @"~/Unidades/Faenamiento/referenciasAmbientalesFaenamiento.aspx";
                    break;
                case "referenciasAmbientalesConcesion.aspx":
                    link.Text = "Referencias Ambientales";
                    link.PostBackUrl = @"~/Unidades/Concesion/referenciasAmbientalesConcesion.aspx";
                    break;
                case "referenciasProductivaAcopio.aspx":
                    link.Text = "Referencias Productivas";
                    link.PostBackUrl = @"~/Unidades/Acopio/referenciasProductivaAcopio.aspx";
                    break;
                case "referenciasProductivaAmerb.aspx":
                    link.Text = "Referencias Productivas";
                    link.PostBackUrl = @"~/Unidades/Amerb/referenciasProductivaAmerb.aspx";
                    break;
                case "referenciasProductivaColector.aspx":
                    link.Text = "Referencias Productivas";
                    link.PostBackUrl = @"~/Unidades/Colector/referenciasProductivaColector.aspx";
                    break;
                case "referenciasProductivaFaenamiento.aspx":
                    link.Text = "Referencias Productivas";
                    link.PostBackUrl = @"~/Unidades/Faenamiento/referenciasProductivaFaenamiento.aspx";
                    break;
                case "referenciasProductivaConcesion.aspx":
                    link.Text = "Referencias Productivas";
                    link.PostBackUrl = @"~/Unidades/Concesion/referenciasProductivaConcesion.aspx";
                    break;
                case "referenciasSanitariasAcopio.aspx":
                    link.Text = "Referencias Sanitarias";
                    link.PostBackUrl = @"~/Unidades/Acopio/referenciasSanitariasAcopio.aspx";
                    break;

                case "referenciasSanitariasColector.aspx":
                    link.Text = "Referencias Sanitarias";
                    link.PostBackUrl = @"~/Unidades/Colector/referenciasSanitariasColector.aspx";
                    break;
                case "referenciasSanitariasFaenamiento.aspx":
                    link.Text = "Referencias Sanitarias";
                    link.PostBackUrl = @"~/Unidades/Faenamiento/referenciasSanitariasFaenamiento.aspx";
                    break;
                case "referenciasSanitariasConcesion.aspx":
                    link.Text = "Referencias Sanitarias";
                    link.PostBackUrl = @"~/Unidades/Concesion/referenciasSanitariasConcesion.aspx";
                    break;
                case "resolucionesAcopio.aspx":
                    link.Text = "Resoluciones";
                    link.PostBackUrl = @"~/Unidades/Acopio/resolucionesAcopio.aspx";
                    break;
                case "resolucionesAmerb.aspx":
                    link.Text = "Resoluciones";
                    link.PostBackUrl = @"~/Unidades/Amerb/resolucionesAmerb.aspx";
                    break;
                case "resolucionesExperimentalesAmerb.aspx":
                    link.Text = "Resoluciones";
                    link.PostBackUrl = @"~/Unidades/ExperimentalesAmerb/resolucionesExperimentalesAmerb.aspx";
                    break;
                case "resolucionesECMPO.aspx":
                    link.Text = "Resoluciones";
                    link.PostBackUrl = @"~/Unidades/Amerb/resolucionesECMPO.aspx";
                    break;
                case "resolucionesColector.aspx":
                    link.Text = "Resoluciones";
                    link.PostBackUrl = @"~/Unidades/Colector/resolucionesColector.aspx";
                    break;
                case "resolucionesFaenamiento.aspx":
                    link.Text = "Resoluciones";
                    link.PostBackUrl = @"~/Unidades/Faenamiento/resolucionesFaenamiento.aspx";
                    break;
                case "resolucionesConcesion.aspx":
                    link.Text = "Resoluciones";
                    link.PostBackUrl = @"~/Unidades/Concesion/resolucionesConcesion.aspx";
                    break;
                case "resumenAcopio.aspx":
                    link.Text = "Centro de Acopio";
                    link.PostBackUrl = @"~/Unidades/Acopio/resumenAcopio.aspx";
                    break;
                case "resumenAmerb.aspx":
                    link.Text = "Resoluciones";
                    link.PostBackUrl = @"~/Unidades/Amerb/resumenAmerb.aspx";
                    break;
                case "resumenExperimentalesAmerb.aspx":
                    link.Text = "Resoluciones";
                    link.PostBackUrl = @"~/Unidades/ExperimentalesAmerb/resumenExperimentalesAmerb.aspx";
                    break;
                case "resumenECMPO.aspx":
                    link.Text = "Centro Acuicultura ECMPO";
                    link.PostBackUrl = @"~/Unidades/ECMPO/resumenECMPO.aspx";
                    break;
                case "ingresarDocumentoECMPO.aspx":
                    link.Text = "Solicitud Acuicultura en ECMPO";
                    link.PostBackUrl = @"~/Solicitudes/ECMPO/identificacionTitularECMPO.aspx";
                    break;
                case "identificacionTitularECMPO.aspx":
                    link.Text = " Identificación del Solicitante";
                    link.PostBackUrl = @"~/Solicitudes/ECMPO/identificacionTitularECMPO.aspx";
                    break;  
                    
                case "titularECMPO.aspx":
                    link.Text = "Titular";
                    link.PostBackUrl = @"~/Unidades/ECMPO/titularECMPO.aspx";
                    break;

                    

                case "resumenColector.aspx":
                    link.Text = "Colector de Semillas";
                    link.PostBackUrl = @"~/Unidades/Colector/resumenColector.aspx";
                    break;
                case "resumenFaenamiento.aspx":
                    link.Text = "Centro de Faenamiento";
                    link.PostBackUrl = @"~/Unidades/Faenamiento/resumenFaenamiento.aspx";
                    break;
                case "resumenConcesion.aspx":
                    link.Text = " Concesión de Acuicultura";
                    link.PostBackUrl = @"~/Unidades/Concesion/resumenConcesion.aspx";
                    break;
                case "titularAcopio.aspx":
                    link.Text = "Titular";
                    link.PostBackUrl = @"~/Unidades/Acopio/titularAcopio.aspx";
                    break;
                case "titularAmerb.aspx":
                    link.Text = "Resoluciones";
                    link.PostBackUrl = @"~/Unidades/Amerb/titularAmerb.aspx";
                    break;
                case "ingresarDocumentoExperimentalesConcesion.aspx":
                    link.Text = "Solicitudes de Experimentales Concesión";
                    link.PostBackUrl = @"~/Solicitudes/ExperimentalesConcesion/ingresarDocumentoExperimentalesConcesion.aspx";
                    break;
                case "identificacionTitularExperimentalesConcesion.aspx":
                    link.Text = "Identidficación del Solicitante";
                    link.PostBackUrl = @"~/Solicitudes/ExperimentalesConcesion/identificacionTitularExperimentalesConcesion.aspx";
                    break;
                case "generalExperimentalesConcesion.aspx":
                    link.Text = "Experimentales";
                    link.PostBackUrl = @"~/Solicitudes/ExperimentalesConcesion/generalExperimentalesConcesion.aspx";
                    break;
                case "antecedDelSectorExperimentalesConcesion.aspx":
                    link.Text = "Experimentales";
                    link.PostBackUrl = @"~/Solicitudes/ExperimentalesConcesion/antecedDelSectorExperimentalesConcesion.aspx";
                    break;
                case "proyTecnicoExperimentalesConcesion.aspx":
                    link.Text = "Experimentales";
                    link.PostBackUrl = @"~/Solicitudes/ExperimentalesConcesion/proyTecnicoExperimentalesConcesion.aspx";
                    break;
                case "informesResolucionesExperimentalesConcesion.aspx":
                    link.Text = "Experimentales";
                    link.PostBackUrl = @"~/Solicitudes/ExperimentalesConcesion/informesResolucionesExperimentalesConcesion.aspx";
                    break;
                case "unidadEspacialExperimentalesConcesion.aspx":
                    link.Text = "Experimentales";
                    link.PostBackUrl = @"~/Solicitudes/ExperimentalesConcesion/unidadEspacialExperimentalesConcesion.aspx";
                    break;
                case "verSolicitudRelocalizacion.aspx":
                    link.Text = "Relocalización";
                    link.PostBackUrl = @"~/Solicitudes/Relocalizacion/verSolicitudRelocalizacion.aspx";
                    break;
                    


                case "titularColector.aspx":
                    link.Text = "Titular";
                    link.PostBackUrl = @"~/Unidades/Colector/titularColector.aspx";
                    break;
                case "titularFaenamiento.aspx":
                    link.Text = "Titular";
                    link.PostBackUrl = @"~/Unidades/Faenamiento/titularFaenamiento.aspx";
                    break;

                case "tramitesAsociadosAcopio.aspx":
                    link.Text = "Resoluciones";
                    link.PostBackUrl = @"~/Unidades/Acopio/tramitesAsociadosAcopio.aspx";
                    break;
                case "tramitesAsociadosAmerb.aspx":
                    link.Text = "Resoluciones";
                    link.PostBackUrl = @"~/Unidades/Amerb/tramitesAsociadosAmerb.aspx";
                    break;
                case "tramitesAsociadosExperimentalesAmerb.aspx":
                    link.Text = "Resoluciones";
                    link.PostBackUrl = @"~/Unidades/ExperimentalesAmerb/tramitesAsociadosExperimentalesAmerb.aspx";
                    break;
                    

                case "tramitesAsociadosECMPO.aspx":
                    link.Text = "Resoluciones";
                    link.PostBackUrl = @"~/Unidades/Amerb/tramitesAsociadosECMPO.aspx";
                    break;
                case "tramitesAsociadosColector.aspx":
                    link.Text = "Resoluciones";
                    link.PostBackUrl = @"~/Unidades/Colector/tramitesAsociadosColector.aspx";
                    break;
                case "tramitesAsociadosFaenamiento.aspx":
                    link.Text = "Resoluciones";
                    link.PostBackUrl = @"~/Unidades/Faenamiento/tramitesAsociadosFaenamiento.aspx";
                    break;
                case "tramitesAsociadosConcesion.aspx":
                    link.Text = "Resoluciones";
                    link.PostBackUrl = @"~/Unidades/Concesion/tramitesAsociadosConcesion.aspx";
                    break;
                case "unidadesEspacialesAcopio.aspx":
                    link.Text = "Unidades Espaciales";
                    link.PostBackUrl = @"~/Unidades/Acopio/unidadesEspacialesAcopio.aspx";
                    break;
                case "unidadesEspacialesAmerb.aspx":
                    link.Text = "Unidades Espaciales";
                    link.PostBackUrl = @"~/Unidades/Amerb/unidadesEspacialesAmerb.aspx";
                    break;
                case "unidadesEspacialesExperimentalesAmerb.aspx":
                    link.Text = "Unidades Espaciales";
                    link.PostBackUrl = @"~/Unidades/Amerb/unidadesEspacialesAmerb.aspx";
                    break;
                case "unidadesEspacialesECMPO.aspx":
                    link.Text = "Unidades Espaciales";
                    link.PostBackUrl = @"~/Unidades/Amerb/unidadesEspacialesECMPO.aspx";
                    break;
                case "unidadesEspacialesColectores.aspx":
                    link.Text = "Unidades Espaciales";
                    link.PostBackUrl = @"~/Unidades/Colectores/unidadesEspacialesColectores.aspx";
                    break;
                case "unidadesEspacialesFaenamiento.aspx":
                    link.Text = "Unidades Espaciales";
                    link.PostBackUrl = @"~/Unidades/Faenamiento/unidadesEspacialesFaenamiento.aspx";
                    break;
                case "unidadesEspacialesConcesion.aspx":
                    link.Text = "Unidades Espaciales";
                    link.PostBackUrl = @"~/Unidades/Concesion/unidadesEspacialesConcesion.aspx";          
                    break;
                    


                /* Bitácora de Cambios */
                case "verBitacoraCambios.aspx":
                    link.Text = "Unidades Espaciales";
                    link.PostBackUrl = @"~/Bitacora/verBitacoraCambios.aspx";
                    break;




            };

            return link;
        }

        
        protected Label Crea_Label(string modulo, string pagina)
        {
            Label label = new Label();
            int acc = 0;

            switch (pagina)
            {
                case "":
                    label.Text = modulo;
                    break;


                #region VISACIONES MASIVAS

                case "InicioVisacion.aspx":
                    label.Text = "Iniciar Visación Masiva";
                    break;
                case "VisarFirmar.aspx":
                    label.Text = "Visación Masiva";
                    break;
                case "FirmaMasiva.aspx":
                    label.Text = "Firma Masiva";
                    break;

                #endregion


                #region RELOCALIZACION

                case "preIngresarSolicitudRelocalizacion.aspx":
                    label.Text = "Generar Solicitud de Relocalización";
                    break;
                case "preIngresarSolicitudRelocalizacionRESA.aspx":
                    label.Text = "Generar Solicitud de Relocalización RESA";
                    break;
                case "ingresarSolicitudRelocalizacion.aspx":
                    label.Text = "Generar Trámite de Relocalización";
                    break;
                case "ingresarSolicitudRelocalizacionRESA.aspx":
                    label.Text = "Generar Trámite de Relocalización RESA";
                    break;
                case "administrarSolicitudRelocalizacion.aspx":
                    label.Text = "Administrar Trámites de Relocalización";
                    break;
                case "administrarSolicitudRelocalizacionRESA.aspx":
                    label.Text = "Administrar Trámites de Relocalización RESA";
                    break;
                case "redefinirSolicitudRelocalizacion.aspx":
                    label.Text = "Redefinir Trámite de Relocalización";
                    break;
                case "redefinirSolicitudRelocalizacionRESA.aspx":
                    label.Text = "Redefinir Trámite de Relocalización RESA";
                    break;
                case "verSolicitudRelocalizacion.aspx":
                    label.Text = "Ver Trámite de Relocalización";
                    break;
                case "verSolicitudRelocalizacionRESA.aspx":
                    label.Text = "Ver Trámite de Relocalización RESA";
                    break;
                case "datosTramiteRelocalizacion.aspx":
                    label.Text = "Datos del Trámite de Relocalización";
                    break;
                case "datosTramiteRelocalizacionRESA.aspx":
                    label.Text = "Datos del Trámite de Relocalización";
                    break;
                case "identificacionSolicitanteRelocalizacion.aspx":
                    label.Text = "Identificación del Titular de la Concesión";
                    break;
                case "identificacionSolicitanteRelocalizacionRESA.aspx":
                    label.Text = "Identificación del Titular de la Concesión";
                    break;
                case "generalRelocalizacion.aspx":
                    label.Text = "Referencia Global Sernapesca";
                    break;
                case "generalRelocalizacionRESA.aspx":
                    label.Text = "Referencia Global Sernapesca";
                    break;
                case "antecedDelSectorRelocalizacion.aspx":
                    label.Text = "Antecedentes del Sector";
                    break;
                case "antecedDelSectorRelocalizacionRESA.aspx":
                    label.Text = "Antecedentes del Sector";
                    break;
                case "proyTecnicoRelocalizacion.aspx":
                    label.Text = "Proyecto Técnico";
                    break;
                case "proyTecnicoRelocalizacionRESA.aspx":
                    label.Text = "Proyecto Técnico";
                    break;
                case "informesResolucionesRelocalizacion.aspx":
                    label.Text = "Informes y Resoluciones";
                    break;
                case "informesResolucionesRelocalizacionRESA.aspx":
                    label.Text = "Informes y Resoluciones";
                    break;
                case "unidadEspacialRelocalizacion.aspx":
                    label.Text = "Relocalizar Concesión";
                    break;
                case "unidadEspacialRelocalizacionRESA.aspx":
                    label.Text = "Relocalizar Concesión";
                    break;
                case "ingresarDocumentoRelocalizacion.aspx":
                    label.Text = "Administrador de Documentos";
                    break;
                case "ingresarDocumentoRelocalizacionRESA.aspx":
                    label.Text = "Administrador de Documentos";
                    break;
                case "verDocumentoRelocalizacion.aspx":
                    label.Text = "Ver Documento";
                    break;
                case "verDocumentoRelocalizacionRESA.aspx":
                    label.Text = "Ver Documento";
                    break;
                case "administrarDocumentoRelocalizacion.aspx":
                    label.Text = "Modificar Documento";
                    break;
                case "administrarDocumentoRelocalizacionRESA.aspx":
                    label.Text = "Modificar Documento";
                    break;
                case "evaluarDocumentoRelocalizacion.aspx":
                    label.Text = "Evaluar Documento";
                    break;
                case "evaluarDocumentoRelocalizacionRESA.aspx":
                    label.Text = "Evaluar Documento";
                    break;
              

                //LISTADO ALERTAS

                case "alertasTramiteRelocalizacion.aspx":
                    label.Text = "Listado de Alertas";
                    break;
                case "alertasTramiteRelocalizacionRESA.aspx":
                    label.Text = "Listado de Alertas";
                    break;

                    //INFORME RESA
                case "ingresoInformeRESA.aspx":
                    label.Text = "Ingreso Informe Relocalización RESA";
                    break;
                case "administrarInformesRESA.aspx":
                    label.Text = "Administrar Informe Relocalización RESA";
                    break;

                #endregion


                #region REDEFINIR TRAMITES DE MODIFICACION
                case "redefinirTramiteModificacion.aspx":
                    label.Text = "Redefinir Trámite";
                    break;

                case "redefinirTramiteModificacionAmerb.aspx":
                    label.Text = "Redefinir Trámite";
                    break;

                case "redefinirTramiteModificacionAcopio.aspx":
                    label.Text = "Redefinir Trámite";
                    break;

                case "redefinirTramiteModificacionFaenamiento.aspx":
                    label.Text = "Redefinir Trámite";
                    break;

                case "redefinirTramiteModificacionECMPO.aspx":
                    label.Text = "Redefinir Trámite";
                    break;

                #endregion

                #region LISTADO DE ERRORES

                case "erroresTramiteModificacion.aspx":
                    label.Text = "Listado Errores";
                    break;
                case "erroresTramiteRelocalizacion.aspx":
                    label.Text = "Listado Errores";
                    break;
                case "erroresTramiteRelocalizacionRESA.aspx":
                    label.Text = "Listado Errores";
                    break;
                case "erroresTramiteModificacionCentroFaenamiento.aspx":
                    label.Text = "Listado Errores";
                    break;
                case "erroresTramiteModificacionAmerb.aspx":
                    label.Text = "Listado Errores";
                    break;

                #endregion


                #region  RESUMEN DE ESTADOS Y REQUERIMIENTOS CON PLAZOS

                case "resumenEstados.aspx":
                    label.Text = "Resumen de Estados";
                    break;
                case "detalleEstado.aspx":
                    label.Text = "Detalle del Estado";
                    break;
                case "plazosRequerimientos.aspx":
                    label.Text = "Requerimientos con Plazos";
                    break;
                case "detallePlazo.aspx":
                    label.Text = "Detalle del Requerimiento con Plazo";
                    break;

                #endregion


                #region USUARIOS

                case "listUsuarios.aspx":
                    label.Text = "Administración de usuarios";
                    break;
                case "adminRolesPrivAplicacion.aspx":
                    label.Text = "Asignación de Accesos a Roles";
                    break;
                case "formUsuario.aspx":
                    label.Text = "Formulario de Usuario";
                    break;
                case "adminUsuarioRolAplicacion.aspx":
                    label.Text = "Asignación de Roles a Usuario";
                    break;
                case "adminUsuarioPertAplicacion.aspx":
                    label.Text = "Asignación de Pert/Identificadores a Usuario";
                    break;

                #endregion


                #region INDICADORES

                case "resumenIndicadores.aspx":
                    label.Text = "Resumen de Indicadores Solicitud Concesión";
                    break;
                case "resumenIndicadoresExpConcesion.aspx":
                    label.Text = "Resumen de Indicadores Experimentales Concesión";
                    break;
                case "resumenIndicadoresLeyCrea.aspx":
                    label.Text = "Resumen de Indicadores Relocalización Crea";
                    break;

                case "resumenIndicadoresRelocalizacionFusiona.aspx":
                    label.Text = "Resumen de Indicadores Relocalización Fusiona";
                    break;

                case "resumenIndicadoresLeyFusion.aspx":
                    label.Text = "Resumen de Indicadores Relocalización Fusión";
                    break;

                case "resumenIndicadoresRESACrea.aspx":
                    label.Text = "Resumen de Indicadores RESA Crea";
                    break;

                case "resumenIndicadoresRESACero.aspx":
                    label.Text = "Resumen de Indicadores RESA Sector 0";
                    break;

                case "resumenIndicadoresRESAFusion.aspx":
                    label.Text = "Resumen de Indicadores RESA Fusión";
                    break;

                case "resumenIndicadoresECMPO.aspx":
                    label.Text = "Resumen de Indicadores Solicitud Acuicultura ECMPO";
                    break;

                case "resumenIndicadoresExperimentalAmerb.aspx":
                    label.Text = "Resumen de Indicadores Solicitud Experimentales AMERB";
                    break;

                case "resumenIndicadoresSemillas.aspx":
                    label.Text = "Resumen de Indicadores Solicitud Colectores de Semillas";
                    break;
                    


                case "resumenIndicadoresRelocalizacionSectorCero.aspx":
                    label.Text = "Resumen de Indicadores Relocalización Sector 0";
                    break;
                case "resumenIndicadoresLeyCero.aspx":
                    label.Text = "Resumen de Indicadores Relocalización Sector 0";
                    break;
                case "resumenIndicadoresModAmpliacion.aspx":
                    label.Text = "Resumen de Indicadores de Ampliación";
                    break;
                case "resumenIndicadoresModReduccion.aspx":
                    label.Text = "Resumen de Indicadores de Reducción";
                    break;
                case "resumenIndicadoresModEspecie.aspx":
                    label.Text = "Resumen de Indicadores de Especie";
                    break;
                case "resumenIndicadoresModProyecto.aspx":
                    label.Text = "Resumen de Indicadores Modificación de Proyecto Técnico";
                    break;

                case "resumenIndicadoresModProyTecnico.aspx":
                    label.Text = "Resumen de Indicadores de Proyecto Técnico";
                    break;
                    
                case "resumenIndicadoresModRegularizacion.aspx":
                    label.Text = "Resumen de Indicadores de Regularización";
                    break;
                case "resumenIndicadoresAcopio.aspx":
                    label.Text = "Resumen de Indicadores Solicitud Centro de Acopio";
                    break;
                case "resumenIndicadoresFaenamiento.aspx":
                    label.Text = "Resumen de Indicadores Solicitudes Centro de Faenamiento";
                    break;
                case "resumenIndicadoresAmerb.aspx":
                    label.Text = "Resumen de Indicadores Solicitud Acuicultura AMERB";
                    break;
                case "resumenIndicadoresColectores.aspx":
                    label.Text = "Resumen de Indicadores Solicitud Colectores de Semilla";
                    break;


                case "resumenIndicadoresECMPOModAmpliacion.aspx":
                    label.Text = "Resumen de Indicadores de ECMPO Ampliación";
                    break;
                case "resumenIndicadoresECMPOModReduccion.aspx":
                    label.Text = "Resumen de Indicadores de ECMPO Reducción";
                    break;
                case "resumenIndicadoresECMPOModEspecie.aspx":
                    label.Text = "Resumen de Indicadores de ECMPO Especie";
                    break;
                case "resumenIndicadoresECMPOModProyTecnico.aspx":
                    label.Text = "Resumen de Indicadores de ECMPO Proyecto Técnico";
                    break;
                case "resumenIndicadoresECMPOModRegularizacion.aspx":
                    label.Text = "Resumen de Indicadores de ECMPO Regularización";
                    break;


                case "resumenIndicadoresAMERBModAmpliacion.aspx":
                    label.Text = "Resumen de Indicadores de AMERB Ampliación";
                    break;
                case "resumenIndicadoresAMERBModReduccion.aspx":
                    label.Text = "Resumen de Indicadores de AMERB Reducción";
                    break;
                case "resumenIndicadoresAMERBModEspecie.aspx":
                    label.Text = "Resumen de Indicadores de AMERB Especie";
                    break;
                case "resumenIndicadoresAMERBModProyTecnico.aspx":
                    label.Text = "Resumen de Indicadores de AMERB Proyecto Técnico";
                    break;
                case "resumenIndicadoresAMERBModRegularizacion.aspx":
                    label.Text = "Resumen de Indicadores de AMERB Regularización";
                    break;


                case "resumenIndicadoresAcopioModAmpliacion.aspx":
                    label.Text = "Resumen de Indicadores de Acopio Ampliación";
                    break;
                case "resumenIndicadoresAcopioModReduccion.aspx":
                    label.Text = "Resumen de Indicadores de Acopio Reducción";
                    break;
                case "resumenIndicadoresAcopioModEspecie.aspx":
                    label.Text = "Resumen de Indicadores de Acopio Renovación";
                    break;
                case "resumenIndicadoresAcopioModProyTecnico.aspx":
                    label.Text = "Resumen de Indicadores de Acopio Proyecto Técnico/Especie";
                    break;
                case "resumenIndicadoresAcopioModRegularizacion.aspx":
                    label.Text = "Resumen de Indicadores de Acopio Regularización";
                    break;

                case "resumenIndicadoresFaenamientoModAmpliacion.aspx":
                    label.Text = "Resumen de Indicadores de Faenamiento Ampliación";
                    break;
                case "resumenIndicadoresFaenamientoModReduccion.aspx":
                    label.Text = "Resumen de Indicadores de Faenamiento Reducción";
                    break;
                case "resumenIndicadoresFaenamientoModEspecie.aspx":
                    label.Text = "Resumen de Indicadores de Faenamiento Renovación";
                    break;
                case "resumenIndicadoresFaenamientoModProyTecnico.aspx":
                    label.Text = "Resumen de Indicadores de Faenamiento Proyecto Técnico/Especie";
                    break;
                case "resumenIndicadoresFaenamientoModRegularizacion.aspx":
                    label.Text = "Resumen de Indicadores de Faenamiento Regularización";
                    break;




                case "detalleIndicador.aspx":
                    label.Text = "Detalle del Indicador";
                    break;
                case "detalleIndicadorRelocalizacionCrea.aspx":
                    label.Text = "Detalle del Indicador Relocalización Crea";
                    break;
                case "detalleIndicadorRelocalizacionFusiona.aspx":
                    label.Text = "Detalle del Indicador Relocalización Fusiona";
                    break;
                case "detalleIndicadorRelocalizacionSectorCero.aspx":
                    label.Text = "Detalle del Indicador Relocalización Sector 0";
                    break;
                case "detalleIndicadorModAmpliacion.aspx":
                    label.Text = "Detalle del Indicador Modificación de Ampliación";
                    break;
                case "detalleIndicadorModReduccion.aspx":
                    label.Text = "Detalle del Indicador Modificación de Reducción";
                    break;
                case "detalleIndicadorModEspecie.aspx":
                    label.Text = "Detalle del Indicador Modificación de Especie";
                    break;
                case "detalleIndicadorModProyTecnico.aspx":
                    label.Text = "Detalle del Indicador Modificación de Proyecto Técnico";
                    break;
                case "detalleIndicadorModRegularizacion.aspx":
                    label.Text = "Detalle del Indicador Modificación Regularización";
                    break;
                case "detalleIndicadorAcopio.aspx":
                    label.Text = "Detalle del Indicador Solicitud de Acopio";
                    break;
                case "detalleIndicadorFaenamiento.aspx":
                    label.Text = "Detalle del Indicador Solicitud Faenamiento";
                    break;
                case "detalleIndicadorAmerb.aspx":
                    label.Text = "Detalle del Indicador Solicitud Amerb";
                    break;
                case "detalleIndicadorColectores.aspx":
                    label.Text = "Detalle del Indicador Solicitud Colectores de Semilla";
                    break;

                #endregion


                #region REPORTES

                    //UNIDADES ESPACIALES
                case "reportesConcesion.aspx":
                    label.Text = "Reportes Concesión de Acuicultura";
                    break;
                case "ReportesExperimentalConcesion.aspx":
                    label.Text = "Reportes Actividades Experimentales de Concesión";
                    break;
                case "reportesAcuiculturaAmerb.aspx":
                    label.Text = "Reportes Actividades de Acuicultura Amerb";
                    break;
                case "ReportesExperimentalAmerb.aspx":
                    label.Text = "Reportes Actividades Experimentales en Amerb";
                    break;
                case "reportesCentroFaenamiento.aspx":
                    label.Text = "Reportes Centro de Faenamiento";
                    break;
                case "reportesCentroAcopio.aspx":
                    label.Text = "Reportes Centros de Acopio";
                    break;
                case "reportesColectoresSemillas.aspx":
                    label.Text = "Reportes Colectores de Semillas";
                    break;
                case "reportesECMPO.aspx":
                    label.Text = "Reportes de Acuicultura ECMPO";
                    break;
                case "detalleCentroCultivoP3.aspx":
                    label.Text = "Detalle del Centro de Cultivo";
                    break;


                    //SOLICITUDES
                case "reportesSolicitudesAcuicultura.aspx":
                    label.Text = "Reportes Solicitudes de Concesión Acuicultura";
                    break;
                case "reportesSolicitudesFaenamiento.aspx":
                    label.Text = "Reportes Solicitudes Centro de Faenamiento";
                    break;
                case "reportesSolicitudesAcopio.aspx":
                    label.Text = "Reportes Solicitudes Centro de Acopio";
                    break;
                case "reportesSolicitudesSemillas.aspx":
                    label.Text = "Reportes Solicitudes Colectores de Semillas";
                    break;
                case "reportesSolicitudesECMPO.aspx":
                    label.Text = "Reportes Solicitudes ECMPO";
                    break;
                case "reportesSolicitudesAMERB.aspx":
                    label.Text = "Reportes Solicitudes Acuicultura de AMERB";
                    break;


                /*
                case "ReportesAcuiculturaAmerb.aspx":
                    label.Text = "Reportes Solicitudes de Colectores de Semilla";
                    break;
                case "reportes.aspx":
                    label.Text = "Reportes";
                    break;
                case "reportesRelocalizacion.aspx":
                    label.Text = "Reportes Solicitudes de Relocalización de Concesión";
                    break;
                case "reportesModificacion.aspx":
                    label.Text = "Reportes Solicitudes de Modificación";
                    break;
                case "reportesAcopio.aspx":
                    label.Text = "Reportes Solicitudes de Centro de Acopio";
                    break;
                case "reportesFaenamiento.aspx":
                    label.Text = "Reportes Solicitudes de Centro de Faenamiento";
                    break;
                case "reportesAmerb.aspx":
                    label.Text = "Reportes Solicitudes de Acuicultura Amerb";
                    break;
                
                case "ReportesSolicitudesAcopio.aspx":
                    label.Text = "Reportes de Centros de Acopio";
                    break;
                case "ReportesSolicitudesAcuicultura.aspx":
                    label.Text = "Reportes de Acuicultura";
                    break;
                case "ReportesSolicitudesAMERB.aspx":
                    label.Text = "Reportes de Amerb";
                    break;
                case "ReportesSolicitudesECMPO.aspx":
                    label.Text = "Reportes de ECMPO";
                    break;
                case "ReportesSolicitudesFaenamiento.aspx":
                    label.Text = "Reportes de Centro de Faenamiento";
                    break;
                case "ReportesSolicitudesSemillas.aspx":
                    label.Text = "Reportes de Colectores de Semilla";
                    break;
              
               */
               
              


                #endregion


                #region CIERRE FORZADO


                case "GenerarCierreForzado.aspx":
                    label.Text = "Generar Cierres Forzados Solicitud de Concesión";
                    break;
                case "administrarCierreForzado.aspx":
                    label.Text = "Administrar Cierres Forzados Solicitud Concesión";
                    break;
                case "CierreForzadoResolucion.aspx":
                    label.Text = "Cierre Forzado - Resumen";
                    break;


                case "GenerarCierreForzadoExperimentalesConcesion.aspx":
                    label.Text = "Generar Cierres Forzados Solicitudes Experimentales Concesión";
                    break;
                case "administrarCierreForzadoExperimentalesConcesion.aspx":
                    label.Text = "Administrar Cierres Forzados de Experimentales Concesión";
                    break;
                case "CierreForzadoResolucionExperimentalesConcesion.aspx":
                    label.Text = "Cierre Forzado - Resumen";
                    break;


                case "GenerarCierreForzadoModificacionConcesion.aspx":
                    label.Text = "Generar Cierres Forzados Modificación Concesión";
                    break;
                case "administrarCierreForzadoModificacionConcesion.aspx":
                    label.Text = "Administrar Cierres Forzados Modificación Concesión";
                    break;
                case "CierreForzadoResolucionModificacionConcesion.aspx":
                    label.Text = "Cierre Forzado - Resumen";
                    break;


                case "GenerarCierreForzadoRelocalizacionLey.aspx":
                    label.Text = "Generar Cierres Forzados Relocalización Ley";
                    break;
                case "administrarCierreForzadoRelocalizacionLey.aspx":
                    label.Text = "Administrar Cierres Forzados Relocalización Ley";
                    break;
                case "CierreForzadoResolucionRelocalizacionLey.aspx":
                    label.Text = "Cierre Forzado - Resumen";
                    break;


                case "GenerarCierreForzadoRelocalizacionRESA.aspx":
                    label.Text = "Generar Cierres Forzados Relocalización RESA";
                    break;
                case "administrarCierreForzadoRelocalizacionRESA.aspx":
                    label.Text = "Administrar Cierres Forzados Relocalización RESA";
                    break;
                case "CierreForzadoResolucionRelocalizacionRESA.aspx":
                    label.Text = "Cierre Forzado - Resumen";
                    break;


                case "GenerarCierreForzadoFaenamiento.aspx":
                    label.Text = "Generar Cierres Forzados Solicitudes de Centro de Faenamiento";
                    break;
                case "administrarCierreForzadoFaenamiento.aspx":
                    label.Text = "Administrar Cierres Forzados Solicitudes de Centro de Faenamiento";
                    break;
                case "CierreForzadoResolucionFaenamiento.aspx":
                    label.Text = "Cierre Forzado - Resumen";
                    break;

                case "GenerarCierreForzadoModificacionCentroFaenamiento.aspx":
                    label.Text = "Generar Cierres Forzados Solicitudes de Modificación Centro de Faenamiento";
                    break;
                case "administrarCierreForzadoModificacionCentroFaenamiento.aspx":
                    label.Text = "Administrar Cierres Forzados Solicitudes Modificación de Centro de Faenamiento";
                    break;
                case "CierreForzadoResolucionModificacionCentroFaenamiento.aspx":
                    label.Text = "Cierre Forzado - Resumen";
                    break;


                case "GenerarCierreForzadoAcopio.aspx":
                    label.Text = "Generar Cierres Forzados Solicitudes de Centro de Acopio";
                    break;
                case "administrarCierreForzadoAcopio.aspx":
                    label.Text = "Administrar Cierres Forzados Solicitudes de Centro de Acopio";
                    break;
                case "CierreForzadoResolucionAcopio.aspx":
                    label.Text = "Cierre Forzado - Resumen";
                    break;


                case "GenerarCierreForzadoModificacionCentroAcopio.aspx":
                    label.Text = "Generar Cierres Forzados Modificación Centro de Acopio";
                    break;
                case "administrarCierreForzadoModificacionCentroAcopio.aspx":
                    label.Text = "Administrar Cierres Forzados Modificación Centro de Acopio";
                    break;
                case "CierreForzadoResolucionModificacionCentroAcopio.aspx":
                    label.Text = "Cierre Forzado - Resumen";
                    break;
                    

                case "GenerarCierreForzadoECMPO.aspx":
                    label.Text = "Generar Cierres Forzados de ECMPO";
                    break;
                case "administrarCierreForzadoECMPO.aspx":
                    label.Text = "Administrar Cierres Forzados de ECMPO";
                    break;
                case "CierreForzadoResolucionECMPO.aspx":
                    label.Text = "Cierre Forzado - Resumen";
                    break;

                case "GenerarCierreForzadoModificacionECMPO.aspx":
                    label.Text = "Generar Cierres Forzados Modificación ECMPO";
                    break;
                case "administrarCierreForzadoModificacionECMPO.aspx":
                    label.Text = "Administrar Cierres Forzados Modificación de Acuicultura en ECMPO";
                    break;
                case "CierreForzadoResolucionModificacionECMPO.aspx":
                    label.Text = "Cierre Forzado - Resumen";
                    break;



                
                case "GenerarCierreForzadoAmerb.aspx":
                    label.Text = "Generar Cierres Forzados Solicitudes de Acuicultura Amerb";
                    break;
                case "administrarCierreForzadoAmerb.aspx":
                    label.Text = "Administrar Cierres Forzados Solicitudes de Acuicultura Amerb";
                    break;
                case "CierreForzadoResolucionAmerb.aspx":
                    label.Text = "Cierre Forzado - Resumen";
                    break;

                case "GenerarCierreForzadoModificacionAmerb.aspx":
                    label.Text = "Generar Cierres Forzados Modificación Acuicultura Amerb";
                    break;
                case "administrarCierreForzadoModificacionAmerb.aspx":
                    label.Text = "Administrar Cierres Forzados Modificación Amerb";
                    break;
                case "CierreForzadoResolucionModificacionAmerb.aspx":
                    label.Text = "Cierre Forzado - Resumen";
                    break;


                case "GenerarCierreForzadoExperimentalesAmerb.aspx":
                    label.Text = "Generar Cierres Forzados de Experimentales AMERB";
                    break;
                case "administrarCierreForzadoExperimentalesAmerb.aspx":
                    label.Text = "Administrar Cierres Forzados de Experimentales AMERB";
                    break;
                case "CierreForzadoResolucionExperimentalesAmerb.aspx":
                    label.Text = "Cierre Forzado - Resumen";
                    break;


                case "GenerarCierreForzadoColectores.aspx":
                    label.Text = "Generar Cierres Forzados Solicitudes de Colectores de Semilla";
                    break;
                case "administrarCierreForzadoColectores.aspx":
                    label.Text = "Administrar Cierres Forzados Solicitudes de Colectores de Semilla  ";
                    break;
                case "CierreForzadoResolucionColectores.aspx":
                    label.Text = "Cierre Forzado - Resumen";
                    break;

               



                #endregion


                #region INICIO SOLICITUD (SALVO RELOCALIZACION)

                case "inicioSolicitudConcesion.aspx":
                    label.Text = "Nueva Solicitud de Concesión de Acuicultura";
                    break;
                case "inicioSolicitudExperimentalesConcesion.aspx":
                    label.Text = "Nueva Solicitud de Experimentales Concesión";
                    break;
                case "ingresarSolicitudModificacion.aspx":
                    label.Text = "Nueva Solicitud de Modificación de Concesión";
                    break;
                case "inicioSolicitudAcopio.aspx":
                    label.Text = "Nueva Solicitud de Centros de Acopio";
                    break;
                case "inicioSolicitudFaenamiento.aspx":
                    label.Text = "Nueva Solicitud de Centros de Faenamiento";
                    break;
                case "ingresarSolicitudModificacionCentroFaenamiento.aspx":
                    label.Text = "Nueva Solicitud de Modificación de Centro de Faenamiento";
                    break; 
                case "inicioSolicitudECMPO.aspx":
                    label.Text = "Nueva Solicitud de Acuicultura ECMPO";
                    break;
                case "ingresarSolicitudModificacionECMPO.aspx":
                    label.Text = "Nueva Solicitud de Modificación de ECMPO";
                    break;
                case "inicioSolicitudExperimentalesAmerb.aspx":
                    label.Text = "Nueva Solicitud de Experimentales Amerb";
                    break;
                case "ingresarSolicitudModificacionAmerb.aspx":
                    label.Text = "Nueva Solicitud de Modificación de Amerb";
                    break;
                case "ingresarSolicitudModificacionCentroAcopio.aspx":
                    label.Text = "Nueva Solicitud de Modificación de Centro de Acopio";
                    break;
                case "inicioSolicitudAmerb.aspx":
                    label.Text = "Nueva Solicitud de Acuicultura Amerb";
                    break;
                case "inicioSolicitudColector.aspx":
                    label.Text = "Nueva Solicitud de Colectores de Semilla";
                    break;

                #endregion


                #region ADMINISTRAR SOLICITUDES


                case "administrarSolicitudConcesion.aspx":
                    label.Text = "Administrar Solicitudes de Concesión";
                    break;
                case "administrarSolicitudExperimentalesConcesion.aspx":
                    label.Text = "Administrar Solicitudes Experimentales Concesión";
                    break;
                case "administrarSolicitudModificacionCentroFaenamiento.aspx":
                    label.Text = "Administrar Solicitudes de Modificación de Centro de Faenamiento";
                    break;      
                case "administrarSolicitudModificacion.aspx":
                    label.Text = "Administrar Solicitudes de Modificación de Concesión";
                    break;
                case "administrarSolicitudAcopio.aspx":
                    label.Text = "Administrar Trámites de Solicitudes de Centro de Acopio";
                    break;
                case "administrarSolicitudECMPO.aspx":
                    label.Text = "Administrar Trámites de Solicitudes de Acuicultura en ECMPO";
                    break;
                case "administrarSolicitudModificacionECMPO.aspx":
                    label.Text = "Administrar Solicitudes de Modificación ECMPO";
                    break;
                case "administrarSolicitudExperimentalesAmerb.aspx":
                    label.Text = "Administrar Trámites de Solicitudes de Experimentales Amerb";
                    break;
                case "administrarSolicitudModificacionAmerb.aspx":
                    label.Text = "Administrar Solicitudes de Modificación de Amerb";
                    break;
                case "administrarSolicitudModificacionCentroAcopio.aspx":
                    label.Text = "Administrar Solicitudes de Modificación de Centro de Acopio";
                    break;
                case "administrarSolicitudFaenamiento.aspx":
                    label.Text = "Administrar Trámites de Solicitudes de Centro de Faenamiento";
                    break;
                case "administrarSolicitudAmerb.aspx":
                    label.Text = "Administrar Trámites de Solicitudes de Acuicultura Amerb";
                    break;
                case "administrarSolicitudColector.aspx":
                    label.Text = "Administrar Trámites de Solicitudes de Colector de Semillas";
                    break;

                #endregion
       
           
           
                #region ADMINISTRAR CONCESIONES

                case "administrarConcesiones.aspx":
                    label.Text = "Administrar Concesiones de Acuicultura";
                    break;
                case "administrarCentroExperimentalesConcesion.aspx":
                    label.Text = "Administrar Experimentales Concesión";
                    break;
                case "administrarCentroExperimentalesAmerb.aspx":
                    label.Text = "Administrar Experimentales en Amerb";
                    break;
                case "administrarCentroAcopio.aspx":
                    label.Text = "Administrar Centro de Acopio";
                    break;
                case "administrarCentroFaenamiento.aspx":
                    label.Text = "Administrar Centro de Faenamiento";
                    break;
                case "administrarCentroECMPO.aspx":
                    label.Text = "Administrar Acuicultura en ECMPO";
                    break;
                case "administrarCentroAmerb.aspx":
                    label.Text = "Administrar Acuicultura Amerb";
                    break;
                case "administrarColectorSemillas.aspx":
                    label.Text = "Administrar Colector de Semillas";
                    break;

                #endregion


                //Solicitudes de Concesión
                case "detalleSolicitud.aspx":
                    label.Text = "Formulario de Solicitud";
                    break;
                case "logEstadosSolicitud.aspx":
                    label.Text = "Histórico de Cambios de Estado";
                    break;


                
                //OTROS
                case "detalleConcesion.aspx":
                    label.Text = "Formulario de Concesión";
                    break;

                // USUARIOS Y GRUPOS
                case "adminGruposPrivAplicacion.aspx":
                    label.Text = "Administración de Grupos de Usuarios";
                    break;
                case "adminUsuarioPrivAplicacion.aspx":
                    label.Text = "Privilegios de Usuario";
                    break;



                #region SOLICITUD CONCESION

                case "identificacionSolicitante.aspx":
                    label.Text = "Identificación del Solicitante";
                    break;
                case "general.aspx":
                    label.Text = "Referencia Global";
                    break;
                case "antecedDelSector.aspx":
                    label.Text = "Antecedentes del Sector";
                    break;
                case "proyTecnico.aspx":
                    label.Text = "Proyecto Técnico";
                    break;
                case "informesResoluciones.aspx":
                    label.Text = "Informes y Resoluciones";
                    break;
                case "unidadEspacial.aspx":
                    label.Text = "Creación de Concesión";
                    break;
                case "ingresarDocumento.aspx":
                    label.Text = "Administrador de Documentos";
                    break;
                case "verDocumento.aspx":
                    label.Text = "Ver Documento";
                    break;
                case "administrarDocumento.aspx":
                    label.Text = "Modificar Documento";
                    break;
                case "evaluarDocumento.aspx":
                    label.Text = "Evaluación de Documento";
                    break;



                    //MODIFICACION (OTRAS SECCIONES TIENEN EL MISMO NOMBRE QUE LOS DE SOLICITUD)

                case "datosConcesionAcuicultura.aspx":
                    label.Text = "Datos Concesión de Acuicultura";
                    break;
                case "identificacionTitularConcesion.aspx":
                    label.Text = "Identificación del Titular";
                    break;
                case "generalModificacion.aspx":
                    label.Text = "Referencia Global";
                    break;
                case "antecedDelSectorModificacion.aspx":
                    label.Text = "Antecedentes del Sector";
                    break;
                case "proyTecnicoModificacion.aspx":
                    label.Text = "Proyecto Técnico";
                    break;
                //Informes y Resoluciones
                case "unidadesEspaciales.aspx":
                    label.Text = "Modificación de Concesión";
                    break;
                //Administrador de Documentos
                //Ver Documento
                //Modificar Documento
                //Evaluación de Documento

                #endregion


                #region FAENAMIENTO

                case "datosCentroFaenamiento.aspx":
                    label.Text = "Solicitud Centro de Faenamiento";
                    break;
                case "identificacionSolicitanteFaenamiento.aspx":
                    label.Text = "Identificación del Titular";
                    break;
                case "generalFaenamiento.aspx":
                    label.Text = "Referencia Global";
                    break;
                case "antecedDelSectorFaenamiento.aspx":
                    label.Text = "Antecedentes del Sector";
                    break;
                case "proyTecnicoFaenamiento.aspx":
                    label.Text = "Proyecto Técnico";
                    break;
                case "informesResolucionesFaenamiento.aspx":
                    label.Text = "Informes y Resoluciones";
                    break;
                case "unidadEspacialFaenamiento.aspx":
                    label.Text = "Creación de Centro de Faenamiento";
                    break;
                case "ingresarDocumentoFaenamiento.aspx":
                    label.Text = "Administrador de Documentos";
                    break;
                case "verDocumentoFaenamiento.aspx":
                    label.Text = "Ver Documento";
                    break;
                case "administrarDocumentoFaenamiento.aspx":
                    label.Text = "Modificar Documento";
                    break;
                case "evaluarDocumentoFaenamiento.aspx":
                    label.Text = "Evaluar Documento";
                    break;

                    //MODIFICACION DE FAENAMIENTO

                case "datosModificacionCentroFaenamiento.aspx":
                    label.Text = "Datos de Modificación de Centro de Faenamiento";
                    break;
                case "identificacionTitularModificacionCentroFaenamiento.aspx":
                    label.Text = "Identificación del Titular";
                    break;
                case "generalModificacionCentroFaenamiento.aspx":
                    label.Text = "Referencia Global";
                    break;
                case "antecedDelSectorModificacionCentroFaenamiento.aspx":
                    label.Text = "Antecedentes del Sector";
                    break;
                case "proyTecnicoModificacionCentroFaenamiento.aspx":
                    label.Text = "Proyecto Técnico";
                    break;
                case "informesResolucionesModificacionCentroFaenamiento.aspx":
                    label.Text = "Informes y Resoluciones";
                    break;
                case "unidadesEspacialesModificacionCentroFaenamiento.aspx":
                    label.Text = "Modificación de Centro de Faenamiento";
                    break;
                case "ingresarDocumentoModificacionCentroFaenamiento.aspx":
                    label.Text = "Administrador de Documentos";
                    break;
                case "verDocumentoModificacionCentroFaenamiento.aspx":
                    label.Text = "Ver Documento";
                    break;
                case "administrarDocumentoModificacionCentroFaenamiento.aspx":
                    label.Text = "Modificar Documento";
                    break;
                case "evaluarDocumentoModificacionCentroFaenamiento.aspx":
                    label.Text = "Evaluar Documento";
                    break;

                #endregion 


                #region ACOPIO

                case "datosCentroAcopio.aspx":
                    label.Text = "Solicitud Centro de Acopio";
                    break;
                case "identificacionSolicitanteAcopio.aspx":
                    label.Text = "Identificación del Solicitante";
                    break;
                case "generalAcopio.aspx":
                    label.Text = "Referencia Global";
                    break;
                case "antecedDelSectorAcopio.aspx":
                    label.Text = "Antecedentes del Sector";
                    break;
                case "proyTecnicoAcopio.aspx":
                    label.Text = "Proyecto Técnico";
                    break;
                case "informesResolucionesAcopio.aspx":
                    label.Text = "Informes y Resoluciones";
                    break;
                case "unidadEspacialAcopio.aspx":
                    label.Text = "Creación de Centro de Acopio";
                    break;
                case "ingresarDocumentoAcopio.aspx":
                    label.Text = "Administrador de Documentos";
                    break;
                case "verDocumentoAcopio.aspx":
                    label.Text = "Ver Documento";
                    break;
                case "administrarDocumentoAcopio.aspx":
                    label.Text = "Modificar Documento";
                    break;
                case "evaluarDocumentoAcopio.aspx":
                    label.Text = "Evaluar Documento";
                    break;
                    
              
                    //MODIFICACION DE ACOPIO 

                case "datosModificacionCentroAcopio.aspx":
                    label.Text = "Datos Centro de Acopio";
                    break;
                case "identificacionTitularModificacionCentroAcopio.aspx":
                    label.Text = "Identificación del Titular";
                    break;
                case "generalModificacionCentroAcopio.aspx":
                    label.Text = "Referencia Global";
                    break;
                case "antecedDelSectorModificacionCentroAcopio.aspx":
                    label.Text = "Antecedentes del Sector";
                    break;
                case "proyTecnicoModificacionCentroAcopio.aspx":
                    label.Text = "Proyecto Técnico";
                    break;
                case "informesResolucionesModificacionCentroAcopio.aspx":
                    label.Text = "Informes y Resoluciones";
                    break;
                case "unidadesEspacialesModificacionCentroAcopio.aspx":
                    label.Text = "Modificación de Centro de Acopio";
                    break;
                case "ingresarDocumentoModificacionCentroAcopio.aspx":
                    label.Text = "Administrador de Documentos";
                    break;
                case "verDocumentoModificacionCentroAcopio.aspx":
                    label.Text = "Ver Documento";
                    break;
                case "administrarDocumentoModificacionCentroAcopio.aspx":
                    label.Text = "Modificar Documento";
                    break;
                case "evaluarDocumentoModificacionCentroAcopio.aspx":
                    label.Text = "Evaluar Documento";
                    break;  
              


                #endregion


                #region ECMPO

                case "datosCentroECMPO.aspx":
                    label.Text = "Solicitud de Acuicultura en ECMPO";
                    break;
                case "identificacionTitularECMPO.aspx":
                    label.Text = "Identificación del Solicitante";
                    break;
                case "generalECMPO.aspx":
                    label.Text = "Referencia Global";
                    break;
                case "antecedDelSectorECMPO.aspx":
                    label.Text = "Antecedentes del Sector";
                    break;
                case "proyTecnicoECMPO.aspx":
                    label.Text = "Proyecto Técnico";
                    break;
                case "informesResolucionesECMPO.aspx":
                    label.Text = "Informes y Resoluciones";
                    break;
                case "unidadEspacialECMPO.aspx":
                    label.Text = "Creación de Acuicultura en ECMPO";
                    break;
                case "ingresarDocumentoECMPO.aspx":
                    label.Text = "Administrar Documentos";
                    break;
                case "verDocumentoECMPO.aspx":
                    label.Text = "Ver Documento";
                    break;
                case "administrarDocumentoECMPO.aspx":
                    label.Text = "Modificar Documento";
                    break;
                case "evaluarDocumentoECMPO.aspx":
                    label.Text = "Evaluar Documento";
                    break;


                case "datosModificacionECMPO.aspx":
                    label.Text = "Datos del Trámite de Modificación ECMPO";
                    break;
                case "identificacionTitularModificacionECMPO.aspx":
                    label.Text = "Titular";
                    break;
                case "generalModificacionECMPO.aspx":
                    label.Text = "Referencia Global";
                    break; 
                case "antecedDelSectorModificacionECMPO.aspx":
                    label.Text = "Antecedentes del Sector";
                    break;
                case "proyTecnicoModificacionECMPO.aspx":
                    label.Text = "Proyecto Técnico";
                    break; 
                case "informesResolucionesModificacionECMPO.aspx":
                    label.Text = "Informes y Resoluciones";
                    break;
                case "unidadesEspacialesModificacionECMPO.aspx":
                    label.Text = "Modificación de ECMPO";
                    break;
                case "ingresarDocumentoModificacionECMPO.aspx":
                    label.Text = "Administrar Documentos";
                    break;
                case "verDocumentoModificacionECMPO.aspx":
                    label.Text = "Ver Documento";
                    break;
                case "administrarDocumentoModificacionECMPO.aspx":
                    label.Text = "Modificar Documento";
                    break;
                case "evaluarDocumentoModificacionECMPO.aspx":
                    label.Text = "Evaluar Documento";
                    break;



                #endregion


                #region AMERB

                case "datosCentroAmerb.aspx":
                    label.Text = "Solicitud Acuicultura Amerb";
                    break;
                case "identificacionSolicitanteAmerb.aspx":
                    label.Text = "Identificación del Solicitante";
                    break;
                case "generalAmerb.aspx":
                    label.Text = "Referencia Global";
                    break;
                case "antecedDelSectorAmerb.aspx":
                    label.Text = "Antecedentes del Sector";
                    break;
                case "proyTecnicoAmerb.aspx":
                    label.Text = "Proyecto Técnico";
                    break;
                case "informesResolucionesAmerb.aspx":
                    label.Text = "Informes y Resoluciones";
                    break;
                case "unidadEspacialAmerb.aspx":
                    label.Text = "Creación de Acuicultura en AMERB";
                    break;
                case "ingresarDocumentoAmerb.aspx":
                    label.Text = "Administrador de Documentos";
                    break;
                case "verDocumentoAmerb.aspx":
                    label.Text = "Ver Documento";
                    break;
                case "administrarDocumentoAmerb.aspx":
                    label.Text = "Modificar Documento";
                    break;
                case "evaluarDocumentoAmerb.aspx":
                    label.Text = "Evaluar Documento";
                    break;
    


                case "datosModificacionAmerb.aspx":
                    label.Text = "Datos Modificación de Trámite AMERB";
                    break;
                case "identificacionTitularModificacionAmerb.aspx":
                    label.Text = "Identificación de Titular";
                    break;
                case "generalModificacionAmerb.aspx":
                    label.Text = "Referencia Global";
                    break;
                case "antecedDelSectorModificacionAmerb.aspx":
                    label.Text = "Antecedentes del Sector";
                    break;
                case "proyTecnicoModificacionAmerb.aspx":
                    label.Text = "Proyecto Técnico";
                    break;
                case "informesResolucionesModificacionAmerb.aspx":
                    label.Text = "Informes y Resoluciones";
                    break;
                case "unidadesEspacialesModificacionAmerb.aspx":
                    label.Text = "Modificación de AMERB";
                    break;
                case "ingresarDocumentoModificacionAmerb.aspx":
                    label.Text = "Administrador de Documentos";
                    break;
                case "verDocumentoModificacionAmerb.aspx":
                    label.Text = "Ver Documento";
                    break;
                case "administrarDocumentoModificacionAmerb.aspx":
                    label.Text = "Modificar Documento";
                    break;
                case "evaluarDocumentoModificacionAmerb.aspx":
                    label.Text = "Evaluar Documento";
                    break;


                #endregion
                

                #region EXPERIMENTAL AMERB


                case "datosCentroExperimentalesAmerb.aspx":
                    label.Text = "Solicitud Experimental AMERB";
                    break;
                case "identificacionTitularExperimentalesAmerb.aspx":
                    label.Text = "Identificación del Solicitante";
                    break;
                case "generalExperimentalesAmerb.aspx":
                    label.Text = "Referencia Global";
                    break;
                case "antecedDelSectorExperimentalesAmerb.aspx":
                    label.Text = "Antecedentes del Sector";
                    break;
                case "proyTecnicoExperimentalesAmerb.aspx":
                    label.Text = "Proyecto Técnico";
                    break;
                case "informesResolucionesExperimentalesAmerb.aspx":
                    label.Text = "Informes y Resoluciones";
                    break;
                case "unidadEspacialExperimentalesAmerb.aspx":
                    label.Text = "Creación de Centro Experimental en AMERB";
                    break;
                case "ingresarDocumentoExperimentalesAmerb.aspx":
                    label.Text = "Administrador de Documentos";
                    break;
                case "verDocumentoExperimentalesAmerb.aspx":
                    label.Text = "Ver Documento";
                    break;
                case "administrarDocumentoExperimentalesAmerb.aspx":
                    label.Text = "Modificar Documento";
                    break;
                case "evaluarDocumentoExperimentalesAmerb.aspx":
                    label.Text = "Evaluar Documento";
                    break;


                #endregion


                #region EXPERIMENTAL CONCESION

                case "datosCentroExperimentalesConcesion.aspx":
                    label.Text = "Solicitud Experimental Concesión";
                    break;
                case "identificacionTitularExperimentalesConcesion.aspx":
                    label.Text = "Identificación del Solicitante";
                    break;
                case "generalExperimentalesConcesion.aspx":
                    label.Text = "Referencias Globales";
                    break;
                case "antecedDelSectorExperimentalesConcesion.aspx":
                    label.Text = "Antecedentes del Sector";
                    break;
                case "proyTecnicoExperimentalesConcesion.aspx":
                    label.Text = "Proyecto Técnico";
                    break;
                case "informesResolucionesExperimentalesConcesion.aspx":
                    label.Text = "Informes y Resoluciones";
                    break;
                case "unidadEspacialExperimentalesConcesion.aspx":
                    label.Text = "Creación de Concesión";
                    break;
                case "ingresarDocumentoExperimentalesConcesion.aspx":
                    label.Text = "Administrador de documentos";
                    break;
                case "verDocumentoExperimentalesConcesion.aspx":
                    label.Text = "Ver Documento";
                    break;
                case "administrarDocumentoExperimentalesConcesion.aspx":
                    label.Text = "Modificar Documento";
                    break;
                case "evaluarDocumentoExperimentalesConcesion.aspx":
                    label.Text = "Evaluar Documento";
                    break;


                #endregion 


                #region COLECTOR

                case "datosCentroColector.aspx":
                    label.Text = "Solicitud Colectores de Semilla";
                    break;
                case "identificacionSolicitanteColector.aspx":
                    label.Text = "Identificación del Solicitante";
                    break;
                case "generalColector.aspx":
                    label.Text = "Referencia Global";
                    break;
                case "antecedDelSectorColector.aspx":
                    label.Text = "Antecedentes del Sector";
                    break;
                case "proyTecnicoColector.aspx":
                    label.Text = "Proyecto Técnico";
                    break;
                case "informesResolucionesColector.aspx":
                    label.Text = "Informes y Resoluciones";
                    break;
                case "unidadEspacialColector.aspx":
                    label.Text = "Creación de Colector de Semilla";
                    break;
                case "ingresarDocumentoColector.aspx":
                    label.Text = "Administrador de Documentos";
                    break;
                case "verDocumentoColector.aspx":
                    label.Text = "Ver Documento";
                    break;
                case "administrarDocumentoColector.aspx":
                    label.Text = "Modificar Documento";
                    break;
                case "evaluarDocumentoColector.aspx":
                    label.Text = "Evaluar Documento";
                    break;

                #endregion


                #region MANTENEDORES TITULARES


                case "administrarTitulares.aspx":
                    label.Text = "Administrar Titulares";
                    break;

                case "agregarTitular.aspx":
                    acc = 0;
                    try
                        {
                            if (Request.QueryString["acc"] != null)
                            {
                                acc = Convert.ToInt32(Request.QueryString["acc"]);
                            };
                            
                        }
                        catch
                        { };

                    if(acc==1){
                        label.Text = "Crear Titulares";
                    }else{
                        label.Text = "Modificar Titulares";
                    }
                    break;

                case "administrarRepresentantesLegales.aspx":
                    label.Text = "Administrar Representantes Legales";
                    break;

                case "agregarRepresentanteLegal.aspx":
                    
                    acc = 0;
                    try
                        {
                            if (Request.QueryString["acc"] != null)
                            {
                                acc = Convert.ToInt32(Request.QueryString["acc"]);
                            };
                            
                        }
                        catch
                        { };

                    if(acc==1){
                        label.Text = "Crear Representante Legal";
                    }else{
                        label.Text = "Modificar Representante Legal";
                    }
                   
                    break;

                case "administrarOperadores.aspx":
                    label.Text = "Administrar Operadores";
                    break;

                case "agregarOperador.aspx":

                    acc = 0;
                    try
                        {
                            if (Request.QueryString["acc"] != null)
                            {
                                acc = Convert.ToInt32(Request.QueryString["acc"]);
                            };
                            
                        }
                        catch
                        { };

                    if(acc==1){
                        label.Text = "Crear Operador";
                    }else{
                        label.Text = "Modificar Operador";
                    }

                    break;

                #endregion


                #region MANTENEDORES

                case "region.aspx":
                    label.Text = "Región";
                    break;
                case "provincia.aspx":
                    label.Text = "Provincia";
                    break;
                case "comuna.aspx":
                    label.Text = "Comuna";
                    break;
                case "tipoBarrio.aspx":
                    label.Text = "Tipo Barrio";
                    break;
                case "barrio.aspx":
                    label.Text = "Barrio";
                    break;
                case "barrioTipo.aspx":
                    label.Text = "Asociación Barrio - Tipo";
                    break;
                case "especieEtapaDesarrollo.aspx":
                    label.Text = "Asociación Especie - Etapa de Cultivo";
                    break;
                case "macrozona.aspx":
                    label.Text = "Macrozona";
                    break;
                case "carta.aspx":
                    label.Text = "Carta SHOA / IGM Plano";
                    break;
                case "datum.aspx":
                    label.Text = "Datum";
                    break;
                case "tipoVertice.aspx":
                    label.Text = "Tipo Vértice";
                    break;
                case "tipoContacto.aspx":
                    label.Text = "Tipo Contacto";
                    break;
                case "huso.aspx":
                    label.Text = "Tipo Huso";
                    break;
                case "tipoConcesion.aspx":
                    label.Text = "Tipo Concesión";
                    break;
                case "tipoUso.aspx":
                    label.Text = "Tipo Uso";
                    break;
                case "tipoCultivo.aspx":
                    label.Text = "Tipo Cultivo";
                    break;
                case "metodoCultivo.aspx":
                    label.Text = "Método Cultivo Algas";
                    break;
                case "tipoAlimento.aspx":
                    label.Text = "Tipo Alimento";
                    break;
                case "especie.aspx":
                    label.Text = "Especie de Cultivo";
                    break;
                case "etapaDeDesarrollo.aspx":
                    label.Text = "Etapa de Cultivo";
                    break;
                case "estructuraTecnica.aspx":
                    label.Text = "Tipo de Estructura";
                    break;
                case "formaEstructura.aspx":
                    label.Text = "Forma Estructura";
                    break;
                case "unidadMedidaEstructuraTecnica.aspx":
                    label.Text = "Unidad Medida Estructura";
                    break;
                case "volumenUnidadMedida.aspx":
                    label.Text = "Volumen Unidad Medida Estructura";
                    break;
                case "anio.aspx":
                    label.Text = "Años";
                    break;
                case "unidadEjemplar.aspx":
                    label.Text = "Unidad Ejemplar";
                    break;
                case "rangoPesoEjemplar.aspx":
                    label.Text = "Rango Peso Ejemplar";
                    break;
                case "capitaniaPuerto.aspx":
                    label.Text = "Capitanía de Puerto";
                    break;
                case "tipoArchivoTitular.aspx":
                    label.Text = "Tipo Archivos Titulares";
                    break;
                case "tipoArchivoAntEspaciales.aspx":
                    label.Text = "Tipo Archivo Coord. Originales";
                    break;
                case "tipoArchivoAntTerreno.aspx":
                    label.Text = "Tipo Archivo Coord. Entrega de Material";
                    break;
                case "tipoArchivoRegularizacion.aspx":
                    label.Text = "Tipo Archivo Coord. Regularización";
                    break;
                case "tipoCentroAcopio.aspx":
                    label.Text = "Tipo Centro Acopio";
                    break;
                case "tipoCentroFaenamiento.aspx":
                    label.Text = "Tipo Centro Faenamiento";
                    break;
                case "cuerpoDeAgua.aspx":
                    label.Text = "Cuerpo de Agua";
                    break;
                case "tipoCuerpoAgua.aspx":
                    label.Text = "Tipo Cuerpo de Agua";
                    break;
                case "plazoNominal.aspx":
                    label.Text = "Plazo Nominal";
                    break;
                case "tipoOrganizacion.aspx":
                    label.Text = "Tipo Organización";
                    break;
                case "descasoACS.aspx":
                    label.Text = "Descanso ACS";
                    break;

                #endregion


                #region MANTENEDOR TRANSVERSAL

                case "resultado.aspx":
                    label.Text = "Resultado";
                    break;
                case "AsocSubrequerimientoResultado.aspx":
                    label.Text = "Asociación Subrequerimiento - Resultado";
                    break;
                case "tipoDocumento.aspx":
                    label.Text = "Tipo Documento";
                    break;
                case "asocSubrequerimientoTipo.aspx":
                    label.Text = "Asociación Subrequerimiento - Tipo Documento";
                    break;
                case "temaSubrequerimiento.aspx":
                    label.Text = "Tema Subrequerimiento";
                    break;
                case "responsableDAC.aspx":
                    label.Text = "Responsable DAC";
                    break;
                case "preferenciaRelocalizacion.aspx":
                    label.Text = "Preferencia de Relocalización";
                    break;

                #endregion


                /* Mantenedor de Personas Titulares - Representante Legal - Operadores */
                case "verRepresentanteLegal.aspx":
                    label.Text = "Ver Representante Legal";
                    break;
                case "verContactoRepresentanteLegal.aspx":
                    label.Text = "Ver Matriz y Sucursal Representante Legal";
                    break;
                case "verContactoTitular.aspx":
                    label.Text = "Ver Matriz y Sucursal Titular";
                    break;
                case "verOperador.aspx":
                    label.Text = "Ver Operador";
                    break;
                case "verContactoOperador.aspx":
                    label.Text = "Ver Matriz y Sucursal Operador";
                    break;
                case "agregarNombres.aspx":
                    label.Text = "Agregar Nombre";
                    break;
                case "agregarContactoDireccion.aspx":
                    label.Text = "Agregar Matriz y Sucursal";
                    break;
                case "asociarRepresentanteLegal.aspx":
                    label.Text = "Asociar Representante Legal";
                    break;
                case "asociarOperador.aspx":
                    label.Text = "Asociar Operador";
                    break;


                #region UNIDAD ESPACIALES (SECCIONES QUE SE PUEDEN MODIFICAR EN LA UNIDAD ESPACIAL)

                case "antecedentesDelSectorAcopio.aspx":
                    label.Text = "Antecedentes del Sector";
                    break;
                case "antecedentesDelSectorAmerb.aspx":
                    label.Text = "Antecedentes del Sector";
                    break;
                case "antecedentesDelSectorExperimentalesAmerb.aspx":
                    label.Text = "Antecedentes del Sector";
                    break;
                case "antecedentesDelSectorColector.aspx":
                    label.Text = "Antecedentes del Sector";
                    break;
                case "antecedentesDelSectorFaenamiento.aspx":
                    label.Text = "Antecedentes del Sector";
                    break;
                case "antecedentesDelSectorECMPO.aspx":
                    label.Text = "Antecedentes del Sector";
                    break;
                case "antecedentesDelSectorConcesion.aspx":
                    label.Text = "Antecedentes del Sector";
                    break;
                case "proyectoTecnicoConcesion.aspx":
                    label.Text = "Proyecto Técnico";
                    break;
                case "proyectoTecnicoAcopio.aspx":
                    label.Text = "Proyecto Técnico";
                    break;
                case "proyectoTecnicoAmerb.aspx":
                    label.Text = "Proyecto Técnico";
                    break;
                case "proyectoTecnicoExperimentalesAmerb.aspx":
                    label.Text = "Proyecto Técnico";
                    break;
                case "proyectoTecnicoECMPO.aspx":
                    label.Text = "Proyecto Técnico";
                    break;
                case "proyectoTecnicoColector.aspx":
                    label.Text = "Proyecto Técnico";
                    break;
                case "proyectoTecnicoFaenamiento.aspx":
                    label.Text = "Proyecto Técnico";
                    break;
                case "administrarDocumentoConcesion.aspx":
                    label.Text = "Administrar Documento";
                    break;

                #endregion


                #region REFERENCIAS (Ambientales, Sanitarias, Productivas)

                case "referenciasAmbientalesAcopio.aspx":
                    label.Text = "Referencias Ambientales";
                    break;
                case "referenciasAmbientalesAmerb.aspx":
                    label.Text = "Referencias Ambientales";
                    break;
                case "referenciasAmbientalesExperimentalesAmerb.aspx":
                    label.Text = "Referencias Ambientales";
                    break;
                case "referenciasAmbientalesColector.aspx":
                    label.Text = "Referencias Ambientales";
                    break;
                case "referenciasAmbientalesFaenamiento.aspx":
                    label.Text = "Referencias Ambientales";
                    break;
                case "referenciasAmbientalesConcesion.aspx":
                    label.Text = "Referencias Ambientales";
                    break;
                case "referenciasAmbientalesECMPO.aspx":
                    label.Text = "Referencias Ambientales";
                    break;
                case "referenciasProductivaAcopio.aspx":
                    label.Text = "Referencias Productivas";
                    break;
                case "referenciasProductivaAmerb.aspx":
                    label.Text = "Referencias Productivas";
                    break;
                case "referenciasProductivaExperimentalesConcesion.aspx":
                    label.Text = "Referencias Productivas";
                    break;
                case "referenciasProductivaColector.aspx":
                    label.Text = "Referencias Productivas";
                    break;
                case "referenciasProductivaFaenamiento.aspx":
                    label.Text = "Referencias Productivas";
                    break;
                case "referenciasProductivaConcesion.aspx":
                    label.Text = "Referencias Productivas";
                    break;
                case "referenciasProductivaECMPO.aspx":
                    label.Text = "Referencias Productivas";
                    break;
                case "referenciasSanitariasAcopio.aspx":
                    label.Text = "Referencias Sanitarias";
                    break;
                case "referenciasSanitariasAmerb.aspx":
                    label.Text = "Referencias Sanitarias";
                    break;
                case "referenciasSanitariasExperimentalesAmerb.aspx":
                    label.Text = "Referencias Sanitarias";
                    break;
                case "referenciasSanitariasColector.aspx":
                    label.Text = "Referencias Sanitarias";
                    break;
                case "referenciasSanitariasFaenamiento.aspx":
                    label.Text = "Referencias Sanitarias";
                    break;
                case "referenciasSanitariasConcesion.aspx":
                    label.Text = "Referencias Sanitarias";
                    break;
                case "referenciasSanitariasECMPO.aspx":
                    label.Text = "Referencias Sanitarias";
                    break;                    


                #endregion


                #region RESOLUCIONES (ADMINISTRADOR)


                case "ingresarResoluciones.aspx":
                    label.Text = "Ingreso de Resolución";
                    break;
                case "administrarResoluciones.aspx":
                    label.Text = "Administrar Resoluciones";
                    break;
                case "busquedaResoluciones.aspx":
                    label.Text = "Búsqueda de Resoluciones";
                    break;
                case "verResolucion.aspx":
                    label.Text = "Ver Resolución";
                    break;
                    

                #endregion


                #region RESOLUCIONES (DENTRO DE LA UNIDAD ESPACIAL)

                case "resolucionesAcopio.aspx":
                    label.Text = "Resoluciones";
                    break;
                case "resolucionesAmerb.aspx":
                    label.Text = "Resoluciones";
                    break;
                case "resolucionesExperimentalesAmerb.aspx":
                    label.Text = "Resoluciones";
                    break;
                case "resolucionesColector.aspx":
                    label.Text = "Resoluciones";
                    break;
                case "resolucionesFaenamiento.aspx":
                    label.Text = "Resoluciones";
                    break;
                case "resolucionesConcesion.aspx":
                    label.Text = "Resoluciones";
                    break;

                #endregion


                #region RESUMEN

                case "resumenAcopio.aspx":
                    label.Text = "Resumen";
                    break;
                case "resumenAmerb.aspx":
                    label.Text = "Resumen";
                    break;
                case "resumenECMPO.aspx":
                    label.Text = "Resumen";
                    break;
                case "resumenColector.aspx":
                    label.Text = "Resumen";
                    break;
                case "resumenFaenamiento.aspx":
                    label.Text = "Resumen";
                    break;
                case "resumenConcesion.aspx":
                    label.Text = "Resumen";
                    break;

                #endregion


                #region TITULAR (SOLICITUD)

                case "titularAcopio.aspx":
                    label.Text = "Titular";
                    break;
                case "titularAmerb.aspx":
                    label.Text = "Titular";
                    break;
                case "titularECMPO.aspx":
                    label.Text = "Titular";
                    break;
                case "titularColector.aspx":
                    label.Text = "Titular";
                    break;
                case "titularFaenamiento.aspx":
                    label.Text = "Titular";
                    break;
                case "titularConcesion.aspx":
                    label.Text = "Titular";
                    break;

                #endregion


                #region TRAMITES ASOCIADOS

                case "tramitesAsociadosAcopio.aspx":
                    label.Text = "Trámites Asociados";
                    break;
                case "tramitesAsociadosAmerb.aspx":
                    label.Text = "Trámites Asociados";
                    break;
                case "tramitesAsociadosExperimentalesAmerb.aspx":
                    label.Text = "Trámites Asociados";
                    break;
                case "tramitesAsociadosECMPO.aspx":
                    label.Text = "Trámites Asociados";
                    break;
                case "tramitesAsociadosColector.aspx":
                    label.Text = "Trámites Asociados";
                    break;
                case "tramitesAsociadosFaenamiento.aspx":
                    label.Text = "Trámites Asociados";
                    break;
                case "tramitesAsociadosConcesion.aspx":
                    label.Text = "Trámites Asociados";
                    break;

                #endregion


                #region CREAR UNIDAD ESPACIAL

                case "unidadesEspacialesAcopio.aspx":
                    label.Text = "Unidades Espaciales";
                    break;
                case "unidadesEspacialesAmerb.aspx":
                    label.Text = "Unidades Espaciales";
                    break;
                case "unidadesEspacialesExperimentalesAmerb.aspx":
                    label.Text = "Unidades Espaciales";
                    break;
                case "unidadesEspacialesECMPO.aspx":
                    label.Text = "Unidades Espaciales";
                    break;
                case "unidadesEspacialesColectores.aspx":
                    label.Text = "Unidades Espaciales";
                    break;
                case "unidadesEspacialesFaenamiento.aspx":
                    label.Text = "Unidades Espaciales";
                    break;
                case "unidadesEspacialesConcesion.aspx":
                    label.Text = "Unidades Espaciales";
                    break;

                #endregion



                #region bitacora de campos

                case "verBitacoraCambios.aspx":
                    label.Text = "Bitácora de Cambios";
                    break;


                #endregion




                case "detalleTitular.aspx":
                    label.Text = "Ver Titular";
                    break;




                case "holding.aspx":
                    label.Text = "Administrar Holding";
                    break;

              
                case "referenciaGlobal.aspx":
                    label.Text = "Referencia Global";
                    break;

             
             
                case "resolucionesECMPO.aspx":
                    label.Text = "Informes y Resoluciones";
                    break;


            };

            return label;
        }
      
  
        protected void Logout_Click(object sender, EventArgs e)
        {
            // Cerramos la sesión actual y deslogueamos la aplicación
            Session.Abandon();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
   
    
    }
}