using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;
using Quartz.Impl;
using Quartz;
using Subpesca.Acceso;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Globalization;
using System.Threading;
using System.Security.Principal;
using System.IO;
using System.Net;



namespace SubPesca
{
    public class Global : System.Web.HttpApplication
    {

        Logger logger = new Logger();
        private static readonly NLog.Logger NLogger =  NLog.LogManager.GetCurrentClassLogger();

        protected void Application_Start(object sender, EventArgs e)
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();

            // Este scheduler se encargara de programar jobs segun cierto
            // tiempo o formato
            IScheduler scheduler = factory.GetScheduler();

            // Inicia el programador de tares
            if (!scheduler.IsStarted)
            {
                scheduler.Start();
            }

            ConexionJob conexionJob = new ConexionJob();

            try
            {
                conexionJob.ejecutarAvisoPublicacionRadial(scheduler);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
            }

            try
            {
                conexionJob.ejecutarCarpetaNoAsignada(scheduler);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
            }

            try
            {
                conexionJob.ejecutarRequerimientoCambioEstadoVenc(scheduler);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
            }

            try
            {
                conexionJob.ejecutarRequerimientoSinMov(scheduler);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
            }

            try
            {
                conexionJob.ejecutarRequerimientoVencido(scheduler);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
            }

            try
            {
                conexionJob.ejecutarSolicitudConPlazo(scheduler);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
            }

            try
            {
                conexionJob.ejecutarSectorRelocalizacionRechazado(scheduler);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
            }

            //try
            //{
            //    conexionJob.ejecutarSinCertificadoCapitaniaPuerto(scheduler);
            //}
            //catch (Exception ex)
            //{
            //    logger.PrintError(ex);
            //    logger.SendMailError(ex);
            //}

            try
            {
                conexionJob.ejecutarUnidadDependenciaCambiaEstado(scheduler);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
            }

            try
            {
                conexionJob.supeditadaTerminadaRechazada(scheduler);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
            }

            try
            {
                conexionJob.supeditadaTerminadaAprobada(scheduler);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
            }

            try
            {
                conexionJob.titularesPendientesCreacion(scheduler);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
            }
            
            try
            {
                conexionJob.TitularesPendientesCreacionActualizacion(scheduler);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
            }
            
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat.ShortDatePattern = "dd'/'MM'/'yyyy";
            newCulture.DateTimeFormat.ShortTimePattern = "HH:mm";
            newCulture.DateTimeFormat.LongDatePattern = "dd'/'MM'/'yyyy";
            newCulture.DateTimeFormat.LongTimePattern = "HH:mm";
            newCulture.DateTimeFormat.FullDateTimePattern = "dd'/'MM'/'yyyy HH:mm";
            newCulture.DateTimeFormat.DateSeparator = "/";
            Thread.CurrentThread.CurrentCulture = newCulture;

            //RegistrarRequest();
        }


        protected void RegistrarRequest()
        {
            HttpContext context = base.Context;
            if (context != null)
            {
                HttpRequest request = context.Request;

                if (request != null)
                {
                    string nombreArchivo = "C:\\procedimientos\\javascript.txt";
                    if (request.FilePath.ToString().EndsWith(".js") || request.FilePath.ToString().EndsWith(".css"))
                    {
                        using (FileStream flujoArchivo = new FileStream(nombreArchivo, FileMode.Append, FileAccess.Write, FileShare.Write))
                        {
                            using (StreamWriter escritor = new StreamWriter(flujoArchivo))
                            {
                                escritor.WriteLine(request.FilePath.ToString());
                            }
                        }
                    }
                }
            }
        }

        protected void Application_Error()
        {
            Exception exception = Server.GetLastError();
            NLogger.Error(exception, "Error no controlado en la aplicación");
        }




        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

            //String cookieName = FormsAuthentication.FormsCookieName;
            //HttpCookie authCookie = Context.Request.Cookies[cookieName];

            //if (null == authCookie)
            //{//There is no authentication cookie.
            //    return;
            //}

            //FormsAuthenticationTicket authTicket = null;

            //try
            //{
            //    authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            //}
            //catch (Exception ex)
            //{
            //    //Write the exception to the Event Log.
            //    return;
            //}

            //if (null == authTicket)
            //{//Cookie failed to decrypt.
            //    return;
            //}

            ////When the ticket was created, the UserData property was assigned a
            ////pipe-delimited string of group names.
            //String[] groups = authTicket.UserData.Split(new char[] { '|' });

            ////Create an Identity.
            //GenericIdentity id = new GenericIdentity(authTicket.Name, "ingreso");

            ////This principal flows throughout the request.
            //GenericPrincipal principal = new GenericPrincipal(id, groups);

            //Context.User = principal;
        }

        protected void Application_Error(object sender, EventArgs e)
        {

            /*
            try
            {
                Exception ex = Server.GetLastError();
                Logger Log = new Logger();
                Log.SendMailError(ex);

                System.Diagnostics.Debug.Write(ex.InnerException.Message);
            }
            catch (Exception) { }
            */

        }

        protected void Session_End(object sender, EventArgs e)
        {
            // Obtenemos la lista de los archivos que subió el administrador pero que no guardó, con el fin de proceder al borrado de éstos 

        }

        protected void Application_End(object sender, EventArgs e)
        {
          
        }

      
    }
}