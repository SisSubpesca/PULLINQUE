using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Configuration;
using Datos.AccesoDatos;

namespace LogicaNegocio.cl.subpesca.rb.errores
{
    public class Logger
    {


        private string m_exePath = string.Empty;

        public void LogWrite(string logMessage)
        {
            m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + "Rio2log.txt"))
                {
                    Log(logMessage, w);
                }
                
            }
            catch (Exception)
            {

            }
        }

        public void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception)
            {
            }
        }



        public void PrintError(Exception ex) {

            
            //this.LogWrite(ex.GetType().FullName);
            //this.LogWrite(ex.Message);
            //this.LogWrite(ex.StackTrace);

            System.Diagnostics.Debug.Write(ex.GetType().FullName);
            System.Diagnostics.Debug.Write(ex.Message);
            System.Diagnostics.Debug.Write(ex.StackTrace);

            //Console.WriteLine(ex.GetType().FullName);
            //Console.WriteLine(ex.Message);
            //Console.WriteLine(ex.StackTrace);
        }

        public void SendMailError(Exception ex) {

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
            
                try
                {

                    string mailDestinatarioErrores = ConfigurationManager.AppSettings["maiDestinatarioErrores"];

                    if (mailDestinatarioErrores != null && !mailDestinatarioErrores.Trim().Equals(""))
                    {
                        SmtpClient mail = new SmtpClient();
                        MailMessage msg = new MailMessage();

                        msg.To.Add(mailDestinatarioErrores);
                        msg.Subject = "Error en Aplicacion Pullinque 4";
                        msg.IsBodyHtml = true;


                        msg.Body = "";

                        if (ex.InnerException != null && ex.InnerException.Message != null)
                        {
                            msg.Body = msg.Body + "<br>" +  ex.InnerException.Message;
                        }

                        if (ex.InnerException != null && ex.InnerException.StackTrace != null)
                        {
                            msg.Body = msg.Body + "<br>" + ex.InnerException.StackTrace;
                        }

                        if (ex.GetType() != null && ex.GetType().FullName != null)
                        {
                            msg.Body = msg.Body + "<br>" +  ex.GetType().FullName;
                        }

                        if (ex.Message != null)
                        {
                            msg.Body = msg.Body + "<br>" + ex.Message;
                        }

                        if (ex.StackTrace != null)
                        {
                            msg.Body = msg.Body + "<br>" + ex.StackTrace;
                        }

                        


                        //if (ex.InnerException != null && ex.InnerException.Message != null)
                        //{
                        //    msg.Body = ex.InnerException.Message + " <br> " + ex.GetType().FullName + " <br> " + ex.Message;
                        //}
                        //else {
                        //    msg.Body = ex.GetType().FullName + " <br> " + ex.Message;
                        //}
                        //msg.Body = msg.Body + " <br> " + ex.StackTrace;


                        mail.Send(msg);
                    }
                    else
                    {
                        Console.WriteLine("No mailDestinatarioErrores application string");
                    }

                    /*
                    System.Configuration.Configuration rootWebConfig1 = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);
                    if (rootWebConfig1.AppSettings.Settings.Count > 0)
                    {
                        System.Configuration.KeyValueConfigurationElement customSetting = rootWebConfig1.AppSettings.Settings["maiDestinatarioErrores"];
                        if (customSetting != null && !customSetting.Value.Trim().Equals(""))
                        {
                            SmtpClient mail = new SmtpClient();
                            MailMessage msg = new MailMessage();

                            msg.To.Add(customSetting.Value);
                            msg.Subject = "Error en Aplicacion";
                            msg.IsBodyHtml = true;
                            msg.Body = ex.GetType().FullName + " - " + ex.Message;
                            msg.Body = msg.Body + " - " + ex.StackTrace;
                            mail.Send(msg);
                        }
                        else{
                            Console.WriteLine("No customsetting1 application string");
                        }
                    }
                     * */
                   
                }
                catch (Exception ex2) {
                    this.PrintError(ex2);
                }

            }).Start();
        }



        public bool LogDB(string message)
        {
           
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paInsRbLogError";
            cnn.parametros.Add("@errorEspecifico", message);
            cnn.Execute();
                
            return true;
        }
    }
}
