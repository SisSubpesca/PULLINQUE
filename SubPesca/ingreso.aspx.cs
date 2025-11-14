using System;
using System.Web.UI;
using System.DirectoryServices;
using System.Web.Security;
using LogicaNegocio.cl.subpesca.rb.servicios.usuario;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Contantes;
using Datos.Entidades;

namespace SubPesca
{
    public partial class ingreso : System.Web.UI.Page
    {

        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable();
        UsuarioService usuarioService = new UsuarioService();
        PermisosService permisosService = new PermisosService();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();

        private String _path;
        private String _filterAttribute;

        public ingreso()
        {
        }

        public ingreso(String path)
        {
          _path = path;
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Usuario"] != null)
            {
                FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, false);
            };

            if (!Page.IsPostBack)
            {
                //Version.Text = "Sistema Pullinque versión 4.00. 01/02/2018";
                //Version.Text = "Sistema Pullinque versión 4.02. 15/03/2018";
                //Version.Text = "Sistema Pullinque versión 4.03. 29/03/2018";
                //Version.Text = "Sistema Pullinque versión 4.04. 18/05/2018";
                //Version.Text = "Sistema Pullinque versión 4.05. 24/05/2018";
                //Version.Text = "Sistema Pullinque versión 4.06. 18/06/2018";
                //Version.Text = "Sistema Pullinque versión 4.07. 12/07/2018";
                //Version.Text = "Sistema Pullinque versión 4.08. 23/08/2018";
                //Version.Text = "Sistema Pullinque versión 4.08. 29/08/2018";
                //Version.Text = "Sistema Pullinque versión 4.09. 10/01/2019";
                //Version.Text = "Sistema Pullinque versión 4.10. 18/12/2019";
                //Version.Text = "Sistema Pullinque versión 4.10. 28/01/2020";
                //Version.Text = "Sistema Pullinque versión 4.11. 07/10/2020";
                //Version.Text = "Sistema Pullinque versión 4.12. 10/11/2020";
                //Version.Text = "Sistema Pullinque versión 4.13. 23/11/2020";
                //Version.Text = "Sistema Pullinque versión 4.15. 17/05/2021";
                //Version.Text = "Sistema Pullinque versión 4.16. 09/06/2021";
                //Version.Text = "Sistema Pullinque versión 4.17. 09/12/2024";
                Version.Text = "Sistema Pullinque versión 4.18. 04/04/2025";
            };

        }
      

        protected void Login_Click(object sender, System.EventArgs e)
        {
            
            try
            {


                ParametroGenerico parametroVALIDAR_USANDO_SSO = parametroGenericoDA.ObtenerParametro(rbParametro.VALIDAR_USANDO_SSO);
                String debeValidar = parametroVALIDAR_USANDO_SSO == null ? "0" : parametroVALIDAR_USANDO_SSO.descripcion;

                //VALIDA CON SSO
                if (Convert.ToInt32(debeValidar) == 1)
                {

                    //if(true == this.IsAuthenticated("", txtUsername.Text, txtPassword.Text))
                    if (txtUsername.Text.Trim().ToUpper().Equals("ROOT") || true == this.IsAuthenticatedSSO(txtUsername.Text, txtPassword.Text))
                    {
                        //String groups = this.GetGroups();

                        ////Create the ticket, and add the groups.
                        //bool isCookiePersistent = chkPersist.Checked;
                        //FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1,  txtUsername.Text,
                        //DateTime.Now, DateTime.Now.AddMinutes(60), isCookiePersistent, groups);

                        ////Encrypt the ticket.
                        //String encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                        ////Create a cookie, and then add the encrypted ticket to the cookie as data.
                        //HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                        //if(true == isCookiePersistent)
                        //    authCookie.Expires = authTicket.Expiration;

                        ////Add the cookie to the outgoing cookies collection.
                        //Response.Cookies.Add(authCookie);

                        //You can redirect now.
                        //Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUsername.Text, false));


                        //SI EL USUARIO ES VALIDO EN ACTIVE DIRECTORY SE VALIDA QUE EXISTA EN PULLINQUE
                        bool Authenticated = UserVerificationPullinqueSSO(txtUsername.Text, txtPassword.Text);
                        if (Authenticated)
                        {
                            // Logueamos la aplicación y creamos la sesión para el usuario.
                            // La redirección se producirá una vez terminado éste procedimiento
                            FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, false);
                            Session["Usuario"] = usuario_logeado;
                        }
                        else
                        {

                            if (txtUsername.Text.Trim().ToUpper().Equals("ROOT")) 
                            {
                                errorLabel.Text = "El nombre y/o contraseña no son correctos. Intente nuevamente.";
                            }
                            else
                            {
                                errorLabel.Text = "El usuario " + txtUsername.Text + " debe estar registrado en Pullinque para poder ingresar"; 
                            }
                            

                        };


                    }
                    else
                    {
                        errorLabel.Text = "El nombre y/o contraseña no son correctos. Intente nuevamente.";
                    }

                    //VALIDA CON PULLINQUE
                }else {

                    bool Authenticated = UserVerificationPullinque(txtUsername.Text, txtPassword.Text);
                    if (Authenticated)
                    {
                        // Logueamos la aplicación y creamos la sesión para el usuario.
                        // La redirección se producirá una vez terminado éste procedimiento
                        FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, false);
                        Session["Usuario"] = usuario_logeado;
                    }
                    else
                    {
                        errorLabel.Text = "El nombre y/o contraseña no son correctos. Intente nuevamente.";
                    };
                
                
                }
            }
            catch(Exception ex)
            {
                errorLabel.Text = "Error al intentar autenticar al usuario. " + ex.Message;
            }
        }


     

        //public bool estaAutenticado(string dominio, string user, string pass, string path)
        //{
        //    //Armamos la cadena completa de dominio y usuario

        //    string domainAndUsername = dominio + @"\" + user;

        //    //Creamos un objeto DirectoryEntry al cual le pasamos el URL, dominio/usuario y la contraseña

        //    DirectoryEntry entry = new DirectoryEntry(path, domainAndUsername, pass);
        //    try
        //    {
        //        DirectorySearcher search = new DirectorySearcher(entry);
        //        //Verificamos que los datos de logeo proporcionados son correctos
        //        SearchResult result = search.FindOne();
        //        if (result == null)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            return true;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}


        #region LOGIN SSO


        public bool IsAuthenticatedSSO(String username, String pwd)
        {

            //String adPath = ConfigurationManager.AppSettings["urlActiveDirectory"];
            //String domain = "netlogon";


            ParametroGenerico parametroURL_ACTIVE_DIRECTORY = parametroGenericoDA.ObtenerParametro(rbParametro.URL_ACTIVE_DIRECTORY);
            ParametroGenerico parametroDOMINIO_SSO = parametroGenericoDA.ObtenerParametro(rbParametro.DOMINIO_SSO);

            String adPath = parametroURL_ACTIVE_DIRECTORY == null ? "" : parametroURL_ACTIVE_DIRECTORY.descripcion;
            String domain = parametroDOMINIO_SSO == null ? "" : parametroDOMINIO_SSO.descripcion;


            this._path = adPath;

            String domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);

            try
            {
                //Bind to the native AdsObject to force authentication.
                Object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                if (null == result)
                {
                    return false;
                }

                //Update the new path to the user in the directory.
                _path = result.Path;
                _filterAttribute = (String)result.Properties["cn"][0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return true;
        }


        //protected void Login_PullinqueSSO(string usuario, string password)
        //{
        //    bool Authenticated = UserVerificationPullinqueSSO(usuario, password);
        //    if (Authenticated)
        //    {
        //        // Logueamos la aplicación y creamos la sesión para el usuario.
        //        // La redirección se producirá una vez terminado éste procedimiento
        //        FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, false);
        //        Session["Usuario"] = usuario_logeado;
        //    }
        //    else
        //    {
        //        errorLabel.Text = "El usuario " + usuario + " debe estar registrado en Pullinque para poder ingresar";
                
        //    };
        //}

        protected bool UserVerificationPullinqueSSO(string userName, string userPassword)
        {
            bool Authenticated = false;

            Datos.Entidades.Usuario usuarioAux = new Datos.Entidades.Usuario();
            usuarioAux.usuario = userName;
            usuarioAux.clave = userPassword;

            Datos.Entidades.Usuario.Serializable usuarioRB = null;

            //SI ES ROOT SE VALIDARÁ EL USUARIO CON SU CONTRASEÑA, EN CASO CONTRARIO YA SE VALIDO LAS CREDENCIALES Y SOLO SE VALIDARA QUE EL USUARIO EXISTA EN PULLINQUE
            if(userName != null && userName.ToUpper().Trim().Equals("ROOT")){
                usuarioRB = usuarioService.ObtieneUsuarioLogin(usuarioAux);
            }else{
                usuarioRB = usuarioService.ObtenerRbUsuarioSerializable(usuarioAux);
            }   
                 

            if (usuarioRB != null)
            {
                Authenticated = true;
                usuario_logeado = usuarioRB;

                usuario_logeado.accesosRB = permisosService.obtenerPermisos(usuario_logeado.id_usuario);
            };

            return Authenticated;
        }

        #endregion


        #region Pullinque  (SIN SSO)

        protected bool UserVerificationPullinque(string userName, string userPassword)
        {
            bool Authenticated = false;

            Datos.Entidades.Usuario usuarioAux = new Datos.Entidades.Usuario();
            usuarioAux.usuario = userName;
            usuarioAux.clave = userPassword;

            Datos.Entidades.Usuario.Serializable usuarioRB = usuarioService.ObtieneUsuarioLogin(usuarioAux);

            if (usuarioRB != null)
            {
                Authenticated = true;
                usuario_logeado = usuarioRB;

                usuario_logeado.accesosRB = permisosService.obtenerPermisos(usuario_logeado.id_usuario);
            };

            return Authenticated;
        }

        #endregion

    }



    //public String GetGroups()
    //{
    //   DirectorySearcher search = new DirectorySearcher(_path);
    //   search.Filter = "(cn=" + _filterAttribute + ")";
    //   search.PropertiesToLoad.Add("memberOf");
    //   StringBuilder groupNames = new StringBuilder();

    //    try
    //    {
    //        SearchResult result = search.FindOne();

    //        int propertyCount = result.Properties["memberOf"].Count;

    //        String dn;
    //        int equalsIndex, commaIndex;

    //        for(int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
    //        {
    //            dn = (String)result.Properties["memberOf"][propertyCounter];

    //            equalsIndex = dn.IndexOf("=", 1);
    //            commaIndex = dn.IndexOf(",", 1);

    //            if(-1 == equalsIndex)
    //            {
    //                return null;
    //            }

    //            groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
    //            groupNames.Append("|");

    //        }
    //    }
    //    catch(Exception ex)
    //    {
    //        throw new Exception("Error obtaining group names. " + ex.Message);
    //    }
    //    return groupNames.ToString();
    //}
}
