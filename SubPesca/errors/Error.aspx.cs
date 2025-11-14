using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SubPesca.errors
{
    public partial class Error : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {

                try
                {
                    this.ViewState["referer"] = GetRefererURL();
                }
                catch { }

                try
                {

                    Exception ex = Server.GetLastError();

                    if (ex != null)
                    {
                        String texto = "";

                        if (ex.InnerException != null && ex.InnerException.Message != null)
                        {
                            texto = texto + "<br>" + ex.InnerException.Message;
                        }

                        if (ex.InnerException != null && ex.InnerException.StackTrace != null)
                        {
                            texto = texto + "<br>" + ex.InnerException.StackTrace;
                        }

                        if (ex.GetType() != null && ex.GetType().FullName != null)
                        {
                            texto = texto + "<br>" + ex.GetType().FullName;
                        }

                        if (ex.Message != null)
                        {
                            texto = texto + "<br>" + ex.Message;
                        }

                        if (ex.StackTrace != null)
                        {
                            texto = texto + "<br>" + ex.StackTrace;
                        }

                        mensajeError.Text = texto;
                    }

                }
                catch{}
            }

         
        }


        public static string GetRefererURL()
        {
            try
            {
              return HttpContext.Current.Request.UrlReferrer.PathAndQuery;
            }
            catch 
            {
              return "";
            }
        }


        protected void cmdVolver_Click(object sender, EventArgs e)
        {
            Regresar(ViewState["referer"]);
        }

        public static void Regresar(object sURL)
        {
            try
            {
                //HttpContext.Current.Response.Redirect(sURL.ToString());
                HttpContext.Current.Response.Redirect("~/ingreso.aspx");
            }
            catch { }
        }


    }
}