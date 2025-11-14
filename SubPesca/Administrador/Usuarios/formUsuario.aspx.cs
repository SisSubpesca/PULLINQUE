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
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using Datos.Contantes;
using Validaciones.cl.subpesca.rb.usuarios;
using System.Collections.Generic;
using SubPesca.Utilidades;

namespace SubPesca.Administrador.Usuarios
{
    public partial class formUsuario : System.Web.UI.Page
    {
        
        Datos.Entidades.Usuario.Serializable usuario = new Datos.Entidades.Usuario.Serializable();
        Datos.Entidades.GrupoDeUsuarios grupos = new Datos.Entidades.GrupoDeUsuarios();
        
        UsuarioValidacion usuarioValidacion = new UsuarioValidacion();
        UsuarioService usuarioService = new UsuarioService();
        RegionDA regionDA = new RegionDA();
        TipoDA tipoDA = new TipoDA();
        PermisosService permisosService = new PermisosService();


        // PAGE_LOAD
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(formUsuario));
            
            if (!Page.IsPostBack)
            {


                Datos.Entidades.Usuario.Serializable usuario_logeado = (Datos.Entidades.Usuario.Serializable)HttpContext.Current.Session["Usuario"];

                if (usuario_logeado == null)
                {
                    Response.Redirect("~/ingreso.aspx");
                }


                //ACCESO A LA SECCION
                if (!permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRACION_DE_USUARIOS }, usuario_logeado, null, rbAccion.EDITAR))
                {
                    Response.Redirect("~/ingreso.aspx");
                };

                Guardar.Visible = true;


                // Inicializamos el Hashtable contenedor del formulario
                Initialize_HT_FormUsuario();

                // Inicializamos los comboboxs
                Initialize_Comboboxs();
                
                // Inicializamos los campos de contraseñas
                Initialize_CamposdeClave(Convert.ToInt32(Id_Usuario.Value));
                
                // Completamos los campos del formulario con los datos del Hashtable
                Carga_FormUsuario();
            };
        }


        // ACCIONES GENERALES
        protected void Initialize_HT_FormUsuario()
        {
            // Se recibe la id del usuario objeto del formulario
            int id_usuario = 0;
            try
            {
                if (Request.QueryString["id_usuario"] != null)
                {
                    id_usuario = Convert.ToInt32(Request.QueryString["id_usuario"]);
                };
            }
            catch
            {
                Response.Redirect("~/Administrador/Usuarios/listUsuarios.aspx");
            };

            Usuario usuarioAux = new Usuario();
            usuarioAux.id_usuario = id_usuario;

            if (id_usuario > 0)
            {
                Datos.Entidades.Usuario user = usuarioService.ObtenerRbUsuario(usuarioAux);
                usuario.id_usuario = user.id_usuario;
                usuario.nombre = user.nombre;
                usuario.apellidos = user.apellidos;
                usuario.RUT = user.RUT;
                usuario.dvUsuario = user.dvUsuario;
                usuario.regionUsuario = user.regionUsuario;
                //usuario.tipoUsuario = user.tipoUsuario;
                usuario.correo = user.correo;
                usuario.usuario = user.usuario;
                usuario.clave = user.clave;
                usuario.id_grupo = user.id_grupo;
                usuario.estado = user.estado;
            }
           

            // Guardamos el id de usuario en una variable hidden para que sea usada mediante javascript
            Id_Usuario.Value = Convert.ToString(id_usuario);

            // Actualizamos el viewstate FormUsuario
            ViewState["FormUsuario"] = usuario;
        }
       
        protected void Initialize_Comboboxs()
        {
            Carga_Combobox("Grupos");
            Carga_Combobox("Region");
            Carga_Combobox("TipoUsuario");
        }
       
        protected void Carga_Combobox(object sender, EventArgs e)
        {
            LinkButton boton_clickeado = (LinkButton)sender;
            switch(boton_clickeado.ID)
            {
                case "cerrar_adminGrupos":
                    Carga_Combobox("Grupos");
                    break;
            };
        }
        
        protected void Carga_Combobox(string combobox)
        {

            switch (combobox)
            { 

                    /*
                case "Grupos":
                    // Cargamos el combobox: Grupos de usuarios
                    string grupo = GruposUsuarios.SelectedValue;
                    GruposUsuarios.DataSource = grupoDeUsuarioDA.Listar();
                    GruposUsuarios.Items.Clear();
                    GruposUsuarios.DataTextField = "Grupo";
                    GruposUsuarios.DataValueField = "IdGrupo";
                    GruposUsuarios.DataBind();
                    GruposUsuarios.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    try
                    {
                        GruposUsuarios.SelectedValue = grupo;
                    }
                    catch
                    {
                        GruposUsuarios.SelectedValue = "-1";
                    };
                    break;
                */
                case "Region":
                    // Cargamos el combobox: Region
                    string region = Region.SelectedValue;
                    Region.DataSource = regionDA.obtenerRegion(0);
                    Region.Items.Clear();
                    Region.DataTextField = "Region";
                    Region.DataValueField = "IdRegion";
                    Region.DataBind();
                    Region.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    try
                    {
                        Region.SelectedValue = region;
                    }
                    catch
                    {
                        Region.SelectedValue = "-1";
                    };
                    break;

                    /*
                case "TipoUsuario":
                    // Cargamos el combobox: Region
                    string tipoUsuario = TipoUsuario.SelectedValue;
                    TipoUsuario.DataSource = tipoDA.ListarTipo("TIPO_USUARIO");
                    TipoUsuario.Items.Clear();
                    TipoUsuario.DataTextField = "descripcion";
                    TipoUsuario.DataValueField = "id";
                    TipoUsuario.DataBind();
                    TipoUsuario.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    try
                    {
                        TipoUsuario.SelectedValue = tipoUsuario;
                    }
                    catch
                    {
                        TipoUsuario.SelectedValue = "-1";
                    };
                    break;
                     */

            };
        }
        
        protected void Carga_FormUsuario()
        {
            // Llenamos los campos del formulario con los datos del HashTable
            Datos.Entidades.Usuario.Serializable usuario = (Datos.Entidades.Usuario.Serializable)ViewState["FormUsuario"];
            
            Nombre.Text = usuario.nombre;
            Apellidos.Text = usuario.apellidos;

            if (usuario.RUT > 0)
            {
                RUT.Text = Convert.ToString(usuario.RUT);
            }
            if (usuario.dvUsuario != '\0')
            {
                dvUsuario.Text = Convert.ToString(usuario.dvUsuario);
            }
            
            
            if (usuario.regionUsuario == null) {
                usuario.regionUsuario = new Region();
            }
            Region.SelectedValue = usuario.regionUsuario.id_region.ToString();

            
            /*
            if (usuario.tipoUsuario == null)
            {
                usuario.tipoUsuario = new ParametroGenerico();
            }
             **/
            //TipoUsuario.SelectedValue = usuario.tipoUsuario.id.ToString();


            Correo.Text = usuario.correo;
            Nick.Text = usuario.usuario;
            //GruposUsuarios.SelectedValue = usuario.id_grupo.ToString();
            int estado = usuario.estado;
                        
            switch (estado)
            {
                case 6:
                    usu_activado.Checked = true;
                    break;
                case 7:
                    usu_desactivado.Checked = true;
                    break;
            };
        }

        protected void Actualize_HT_FormUsuario()
        {
            usuario = (Datos.Entidades.Usuario.Serializable)ViewState["FormUsuario"];
            int id_usuario = usuario.id_usuario;
            usuario.nombre = Nombre.Text;
            usuario.apellidos = Apellidos.Text;
            usuario.RUT = Convert.ToInt32(RUT.Text);
            usuario.dvUsuario = Convert.ToChar(dvUsuario.Text);

            if (usuario.regionUsuario == null) {
                usuario.regionUsuario = new Region();
            }
            usuario.regionUsuario.id_region = Convert.ToInt32(Region.SelectedValue);

            /*
            if (usuario.tipoUsuario == null)
            {
                usuario.tipoUsuario = new ParametroGenerico();
            }
             **/
            //usuario.tipoUsuario.id = Convert.ToInt32(TipoUsuario.SelectedValue);

            usuario.correo = Correo.Text;
            usuario.usuario = Nick.Text;
            
            if (id_usuario == 0)
            {
                usuario.clave = Clave.Text;
            }
            else
            {
                if (cambiar_clave.Visible == false)
                {
                    usuario.clave = NewClave.Text;
                };
            };
            //usuario.id_grupo = Convert.ToInt32(GruposUsuarios.SelectedValue);
            usuario.estado = (usu_activado.Checked == true) ? 6 : 7;

            ViewState["FormUsuario"] = usuario;
        }

        protected void Initialize_CamposdeClave(int id_usuario)
        {
            Clave.Attributes.Add("value", "");
            NewClave.Attributes.Add("value", "");
            ReClave.Attributes.Add("value", "");

            switch(id_usuario)
            {
                case 0:
                    fila_clave.Visible = true;
                    fila_newclave.Visible = false;
                    fila_reclave.Visible = true;
                    CompareValidator1.ControlToCompare = "Clave";
                    break;
                default:
                    fila_clave.Visible = false;
                    fila_newclave.Visible = true;
                    fila_reclave.Visible = false;
                    CompareValidator1.ControlToCompare = "NewClave";

                    cambiar_clave.Visible = true;
                    NewClave.Visible = false;
                    RequiredFieldValidator6.Visible = false;
                    break;
            };
        }


        // VALIDADORES
        [Ajax.AjaxMethod()]
        public bool validaDatosUsuario(int id_usuario, string objeto, string valor)
        {
            // Se valida si el nick de usuario y su mail ya están en uso 
            bool resp = usuarioService.ValidarRbUsuario(id_usuario, objeto, valor);

            return resp;
        }


        // EVENTOS DE BOTONES PRINCIPALES
        protected void CambiarClave_Click(object sender, EventArgs e)
        {
            cambiar_clave.Visible = false;
            NewClave.Visible = true;
            RequiredFieldValidator6.Visible = true;
            fila_reclave.Visible = true;
        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                String rut = "";
                rut = Convert.ToInt32(RUT.Text) + "-" + Convert.ToChar(dvUsuario.Text);


                List<String> errores = usuarioValidacion.validarRut(rut);

                if (errores.Count == 0)
                {

                    Actualize_HT_FormUsuario();
                    Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable();
                    usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                    usuario = (Datos.Entidades.Usuario.Serializable)ViewState["FormUsuario"];


                    int id_usuario = usuarioService.GuardarRbUsuario(usuario, usuario_logeado);

                    usuario.id_usuario = id_usuario;
                    ViewState["FormUsuario"] = usuario;


                    Id_Usuario.Value = Convert.ToString(id_usuario);

                    string script = @"<script type='text/javascript'>open_message('msgGuardar');</script>";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "mensaje_guardar", script, false);

                    // Inicializamos los campos de contraseñas
                    Initialize_CamposdeClave(Convert.ToInt32(Id_Usuario.Value));


                }else {

                    foreach (String error in errores)
                    {
                        Page.Validators.Add(new ValidationError("RutValidacion", error));
                    }
                
                }


            };
        }


    

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            Hashtable HT_ModUsuarios = (Hashtable)Session["Modulo_Usuarios"];
            if (HT_ModUsuarios != null)
            {
                Hashtable HT_ListUsuarios = (Hashtable)HT_ModUsuarios["ListUsuarios"];
                if (HT_ListUsuarios != null)
                {
                    HT_ListUsuarios["locked"] = false;
                    HT_ModUsuarios["ListUsuarios"] = (Hashtable)HT_ListUsuarios;
                    Session["Modulo_Usuarios"] = (Hashtable)HT_ModUsuarios;
                };
            };
            Response.Redirect("~/Administrador/Usuarios/listUsuarios.aspx");
        }

        protected void Continuar_Click(object sender, EventArgs e)
        {

        }

        protected void Finalizar_Click(object sender, EventArgs e)
        {
            Hashtable HT_ModUsuarios = (Hashtable)Session["Modulo_Usuarios"];
            if (HT_ModUsuarios != null)
            {
                Hashtable HT_ListUsuarios = (Hashtable)HT_ModUsuarios["ListUsuarios"];
                if (HT_ListUsuarios != null)
                {
                    HT_ListUsuarios["locked"] = false;
                    HT_ModUsuarios["ListUsuarios"] = (Hashtable)HT_ListUsuarios;
                    Session["Modulo_Usuarios"] = (Hashtable)HT_ModUsuarios;
                };
            };
            Response.Redirect("~/Administrador/Usuarios/listUsuarios.aspx");
        }

        
    }
}

