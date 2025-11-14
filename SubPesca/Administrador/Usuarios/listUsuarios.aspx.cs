using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.usuario;
using LogicaNegocio.cl.subpesca.rb.servicios.usuario;
using System.Collections.Generic;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using Datos.Contantes;

namespace SubPesca.Administrador.Usuarios
{
    public partial class listUsuarios : System.Web.UI.Page
    {

       
        UsuarioDA usuarioDA = new UsuarioDA();
        UsuarioService usuarioService = new UsuarioService();
        PermisosService permisosService = new PermisosService();

        // PAGE_LOAD
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                Datos.Entidades.Usuario.Serializable usuario_logeado = (Datos.Entidades.Usuario.Serializable)HttpContext.Current.Session["Usuario"];

                if (usuario_logeado == null)
                {
                    Response.Redirect("~/ingreso.aspx");
                }

                //VALIDA EL ACCESO A LA SECCION
                if (!permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRACION_DE_USUARIOS }, usuario_logeado, null, 0)){
                    Response.Redirect("~/ingreso.aspx");
                }


                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRACION_DE_USUARIOS }, usuario_logeado, null, rbAccion.EDITAR))
                {
                    PanelBotonAgregar.Visible = true;
                }

                // Inicializamos el Hashtable contenedor de los filtros de búsqueda
                Initialize_HT_ListUsuarios();

                // Inicializamos el formulario
                Initialize_Form();

                // Cargamos la grilla
                CargaGrilla();
            };
        }


        // ACCIONES GENERALES
        protected void Initialize_HT_ListUsuarios()
        {
            // Se reciben los datos básicos de búsqueda
            string KeySort = "Usuario ASC";
            bool locked = true;
            int pagina = 0;
            string usuario = "";
            string nombApelli = "";
            int id_grupo = -1;
            Hashtable HT_ModUsuarios = (Hashtable)Session["Modulo_Usuarios"];
            Hashtable HT_ListUsuarios = new Hashtable();

            if (HT_ModUsuarios != null)
            {
                HT_ListUsuarios = (Hashtable)HT_ModUsuarios["ListUsuarios"];
                if (HT_ListUsuarios != null)
                {
                    locked = (bool)HT_ListUsuarios["locked"];
                    // Si el formulario de búsqueda está desbloqueado significa que fue abiertoa travez de clickeos en los links de la aplicación  
                    // De estar bloqueado, los filtros de búsqueda serían reseteados
                    if (!locked)
                    {
                        KeySort = (string)HT_ListUsuarios["KeySort"];
                        pagina = (int)HT_ListUsuarios["pagina"];
                        usuario = (string)HT_ListUsuarios["usuario"];
                        nombApelli = (string)HT_ListUsuarios["nombApelli"];
                        id_grupo = (int)HT_ListUsuarios["id_grupo"];
                    };
                };
            };
            HT_ListUsuarios = new Hashtable();
            HT_ListUsuarios.Add("locked", true);
            HT_ListUsuarios.Add("KeySort", KeySort);
            HT_ListUsuarios.Add("pagina", pagina);
            HT_ListUsuarios.Add("usuario", usuario);
            HT_ListUsuarios.Add("nombApelli", nombApelli);
            HT_ListUsuarios.Add("id_grupo", id_grupo);
           
            // Actualizamos la sesión Modulo_Usuarios
            if (HT_ModUsuarios == null)
            {
                HT_ModUsuarios = new Hashtable();
                HT_ModUsuarios.Add("ListUsuarios", (Hashtable)HT_ListUsuarios);
            }
            else
            {
                if (HT_ModUsuarios["ListUsuarios"] == null)
                {
                    HT_ModUsuarios.Add("ListUsuarios", (Hashtable)HT_ListUsuarios);
                }
                else
                {
                    HT_ModUsuarios["ListUsuarios"] = (Hashtable)HT_ListUsuarios;
                };
            };
            Session["Modulo_Usuarios"] = (Hashtable)HT_ModUsuarios;
        }
        
        protected void Initialize_Comboboxs()
        {
            Hashtable HT_ModUsuarios = (Hashtable)Session["Modulo_Usuarios"];
            Hashtable HT_ListUsuarios = (Hashtable)HT_ModUsuarios["ListUsuarios"];
            int id_grupo = (int)HT_ListUsuarios["id_grupo"];

        }
       
        protected void Initialize_Form()
        {
            Initialize_Comboboxs();
            Hashtable HT_ModUsuarios = (Hashtable)Session["Modulo_Usuarios"];
            Hashtable HT_ListUsuarios = (Hashtable)HT_ModUsuarios["ListUsuarios"];
            Usuario.Text = Convert.ToString((string)HT_ListUsuarios["usuario"]);
            Nombre.Text = Convert.ToString((string)HT_ListUsuarios["nombApelli"]);
        }
      
      


        // EVENTOS DE BOTONES PRINCIPALES
        protected void Agregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/Usuarios/formUsuario.aspx");            
        }


        protected void Filtrar_Click(object sender, EventArgs e)
        {
            Hashtable HT_ModUsuarios = (Hashtable)Session["Modulo_Usuarios"];
            Hashtable HT_ListUsuarios = (Hashtable)HT_ModUsuarios["ListUsuarios"];
            HT_ListUsuarios["usuario"] = Usuario.Text;
            HT_ListUsuarios["nombApelli"] = Nombre.Text;
            HT_ModUsuarios["ListUsuarios"] = (Hashtable)HT_ListUsuarios;
            Session["Modulo_Usuarios"] = (Hashtable)HT_ModUsuarios;

            CargaGrilla();
        }


        protected void Limpiar_Click(object sender, EventArgs e)
        {
            Usuario.Text = "";
            Nombre.Text = "";
            
            Hashtable HT_ModUsuarios = (Hashtable)Session["Modulo_Usuarios"];
            Hashtable HT_ListUsuarios = (Hashtable)HT_ModUsuarios["ListUsuarios"];
            HT_ListUsuarios["usuario"] = Usuario.Text;
            HT_ListUsuarios["nombApelli"] = Nombre.Text;
            HT_ModUsuarios["ListUsuarios"] = (Hashtable)HT_ListUsuarios;
            Session["Modulo_Usuarios"] = (Hashtable)HT_ModUsuarios;

            CargaGrilla();
        }
       

        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            // Descargamos los filtros de búsqueda del Hashtable
            Hashtable HT_ModUsuarios = new Hashtable();
            Hashtable HT_ListUsuarios = new Hashtable();
            try
            {
                HT_ModUsuarios = (Hashtable)Session["Modulo_Usuarios"];
                HT_ListUsuarios = (Hashtable)HT_ModUsuarios["ListUsuarios"];
            }
            catch
            {
                Initialize_HT_ListUsuarios();
                HT_ModUsuarios = (Hashtable)Session["Modulo_Usuarios"];
                HT_ListUsuarios = (Hashtable)HT_ModUsuarios["ListUsuarios"];
            };
            string KeySort = (string)HT_ListUsuarios["KeySort"];
            int pagina = (int)HT_ListUsuarios["pagina"];
            string usuario = (string)HT_ListUsuarios["usuario"];
            string nombApelli = (string)HT_ListUsuarios["nombApelli"];
            int id_grupo = (int)HT_ListUsuarios["id_grupo"];

            // Iniciamos la query de búsqueda y cargamos la grilla
            int num_registros = 0;


            Datos.Entidades.Usuario usuarioFiltro = new Datos.Entidades.Usuario();
            usuarioFiltro.usuario = usuario;
            usuarioFiltro.nombApelli = nombApelli;
            usuarioFiltro.id_grupo = id_grupo;

            List<Usuario> dt = usuarioService.ListarRbUsuario(usuarioFiltro);
            if (dt != null)
            {
                num_registros = dt.Count();
                if (num_registros > 0)
                {
                    if (num_registros == 1)
                    {
                        msgGrilla.Text = "Se ha encontrado 1 usuario coincidente con el filtro de búsqueda.";
                    }
                    else
                    {
                        msgGrilla.Text = "Se han encontrado " + num_registros + " usuarios coincidentes con el filtro de búsqueda.";
                    };
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Content_msgGrilla.Visible = true;
                }
                else
                {
                    msgGrilla.Text = "No se encontraron usuarios coincidentes con el filtro de búsqueda.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Content_msgGrilla.Visible = true;
                };

                // Se ordena el DataTable en el server de aplicación
                GridView1.PageIndex = pagina;
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                Response.Redirect("~/Administrador/principal.aspx");
            };
        }
       
        
        protected void GridView1_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            Hashtable HT_ModUsuarios = (Hashtable)Session["Modulo_Usuarios"];
            Hashtable HT_ListUsuarios = (Hashtable)HT_ModUsuarios["ListUsuarios"];
            HT_ListUsuarios["pagina"] = e.NewPageIndex;
            HT_ModUsuarios["ListUsuarios"] = (Hashtable)HT_ListUsuarios;
            Session["Modulo_Usuarios"] = (Hashtable)HT_ModUsuarios;

            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
            CargaGrilla();
        }
       
        
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Datos.Entidades.Usuario.Serializable usuario_logeado = (Datos.Entidades.Usuario.Serializable)HttpContext.Current.Session["Usuario"];

                //MODIFICAR USUARIOS
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRACION_DE_USUARIOS }, usuario_logeado, null, rbAccion.EDITAR))
                {
                    LinkButton boton_permisos = (LinkButton)e.Row.FindControl("gModificar");
                    boton_permisos.Visible = true;
                };

                // PERMISOS DE ACCESO
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRACION_DE_USUARIOS }, usuario_logeado, null, rbAccion.PERMISOS_DE_ACCESO))
                {
                    LinkButton boton_permisos = (LinkButton)e.Row.FindControl("gPermisos");
                    boton_permisos.Visible = true;
                };

                // PERMISOS COMUNALES
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRACION_DE_USUARIOS }, usuario_logeado, null, rbAccion.PERMISOS_COMUNALES))
                {
                    LinkButton boton_sectores = (LinkButton)e.Row.FindControl("gSectores");
                    boton_sectores.Visible = true;
                    boton_sectores.Attributes.Add("onclick", "javascript:abre_dialogo('alcanceRegional', '" + DataBinder.Eval(e.Row.DataItem, "id_usuario") + "')");                
                };


                // ASIGNACION DE PERT
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRACION_DE_USUARIOS }, usuario_logeado, null, rbAccion.ASIGNACION_DE_PERT))
                {
                    LinkButton boton_asignacion = (LinkButton)e.Row.FindControl("gPert");
                    boton_asignacion.Visible = true;
                };

                // BORRAR -- Faltan Agregar los permisos para eliminar usuarios.
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRACION_DE_USUARIOS }, usuario_logeado, null, rbAccion.ELIMINAR))
                {
                    LinkButton boton_eliminar = (LinkButton)e.Row.FindControl("gEliminar");
                    boton_eliminar.Visible = true;
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar al usuario " + DataBinder.Eval(e.Row.DataItem, "Usuario") + "?')");
                };
            };
        }
       
        
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e) 
        {
            int id_usuario = 0;
            switch (e.CommandName)
            {
                case "Modificar":
                    Response.Redirect("~/Administrador/Usuarios/formUsuario.aspx?id_usuario=" + e.CommandArgument);
                    break;
                case "Permisos":
                    Response.Redirect("~/Administrador/Usuarios/adminUsuarioRolAplicacion.aspx?id_usuario=" + e.CommandArgument);
                    break;
                case "Asignacion":
                    Response.Redirect("~/Administrador/Usuarios/adminUsuarioPertAplicacion.aspx?id_usuario=" + e.CommandArgument);
                    break;
                case "Eliminar":
                    id_usuario = Convert.ToInt32(e.CommandArgument);
                    DeleteUser(id_usuario);
                    break;            
            };
        }
       
        
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            Hashtable HT_ModUsuarios = (Hashtable)Session["Modulo_Usuarios"];
            Hashtable HT_ListUsuarios = (Hashtable)HT_ModUsuarios["ListUsuarios"];
            string KeySort = (string)HT_ListUsuarios["KeySort"];
            int pos = 0;
            
            pos = KeySort.IndexOf(e.SortExpression + " ASC");
            if (pos >= 0)
            {
                KeySort = e.SortExpression + " DESC";
            }
            else
            {
                KeySort = e.SortExpression + " ASC";
            };

            HT_ListUsuarios["KeySort"] = KeySort;
            HT_ListUsuarios["pagina"] = 0;
            HT_ModUsuarios["ListUsuarios"] = (Hashtable)HT_ListUsuarios;
            Session["Modulo_Usuarios"] = (Hashtable)HT_ModUsuarios;

            CargaGrilla();
        }
        
        
        protected void DeleteUser(int id_usuario)
        {

            bool dt = usuarioDA.EliminarLogicamente(id_usuario);
            string msj = "";
            if (dt)
            {
                msj = "El Usuario fue eliminado";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";

                CargaGrilla();
            }
            else
            {
                msj = "Se ha producido un error al intentar eliminar al usuario.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";

                CargaGrilla();
            }
            //PL para eliminar logicamente al usuario, sacarlo de la lista dejarlo con un parametro nuevo que deberia ser eliminado.
            /*
            DataTable dt = usuarioDA.Eliminar(id_usuario);
            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);
                string usuario = Convert.ToString(dt.Rows[0]["usuario"]);
                if (msg == "Done")
                {
                    mensaje = "El usuario '" + usuario + "' ha sido eliminado.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar al usuario.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };
            */
            Content_msgGrilla.Visible = true;
            msgGrilla.Text = msj;
            
        }

    }
}
