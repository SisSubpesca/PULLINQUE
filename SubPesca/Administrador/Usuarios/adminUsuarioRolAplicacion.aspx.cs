using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.usuario;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.servicios.usuario;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.common;
using System.Collections;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;

namespace SubPesca.Administrador.Usuarios
{
    public partial class adminUsuarioRolAplicacion : System.Web.UI.Page
    {
        Datos.Entidades.Usuario usuarios = new Datos.Entidades.Usuario();
        Datos.Utilidades.Funciones fnc = new Datos.Utilidades.Funciones();
        UsuarioService usuarioService = new UsuarioService();
        RolService rolService = new RolService();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
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


                //ACCESO A LA SECCION
                if (!permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRACION_DE_USUARIOS }, usuario_logeado, null, rbAccion.PERMISOS_DE_ACCESO))
                {
                    Response.Redirect("~/ingreso.aspx");
                };


                Guardar.Visible = true;


                int id_usuario = 0;

                try
                {
                    if (Request.QueryString["id_usuario"] != null)
                    {
                        id_usuario = Convert.ToInt32(Convert.ToString(Request.QueryString["id_usuario"]));

                        Usuario usuarioFiltro = new Usuario();
                        usuarioFiltro.id_usuario = id_usuario;
                        Datos.Entidades.Usuario user = usuarioService.ObtenerRbUsuario(usuarioFiltro);
                        if (user.id_usuario != 0)
                        {
                            Id_Usuario.Value = Convert.ToString(id_usuario);
                            Usuario.Text = user.usuario;
                        }
                        else
                        {
                            Response.Redirect("~/Administrador/Usuarios/listUsuarios.aspx");
                        };
                    }
                    else
                    {
                        Response.Redirect("~/Administrador/Usuarios/listUsuarios.aspx");
                    };
                }
                catch
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
                };


                
            };

            Carga_Roles(Convert.ToInt32(Id_Usuario.Value));

        }



        protected void Carga_Roles(int idUsuario)
        {

            if (idUsuario > 0)
            {
                DataTable dt = rolService.ListarRolesUsuario(idUsuario, rbEstadosGenerales.VIGENTE);
                int num_registros = dt.Rows.Count;
                
                if (num_registros > 0)
                {
                    // Se construye la tabla con el detalle de privilegios
                    TableHeaderCell th1 = new TableHeaderCell();
                    th1.Text = "Rol";
                    th1.Width = Unit.Pixel(300);
                    
                    TableHeaderCell th2 = new TableHeaderCell();
                    th2.Text = "Permiso";
                    th2.Width = Unit.Pixel(80);

                    TableHeaderRow thread = new TableHeaderRow();
                    thread.Controls.Add(th1);
                    thread.Controls.Add(th2);
                    thread.BackColor = System.Drawing.Color.SkyBlue;
                    thread.Height = Unit.Pixel(30);

                    Table tabla = new Table();
                    tabla.ID = "Tabla_Privilegios";
                    tabla.CellPadding = 4;
                    tabla.CellSpacing = 1;
                    tabla.BorderWidth = Unit.Pixel(1);
                    tabla.BorderColor = System.Drawing.Color.Silver;
                    tabla.BorderStyle = BorderStyle.Solid;
                    tabla.Width = Unit.Percentage(100);
                    tabla.Controls.Add(thread);

                    // Agregamos las filas a la tabla. Cada fila representa un objeto con el detalle de sus privilegios.
                    TableRow fila = new TableRow();
                    bool par = false;

                    int idRol = 0;
                    string rol = "";
                    bool escritura = false;


                    for (int x = 0; x < num_registros; x++)
                    {
                        idRol = Convert.ToInt32(dt.Rows[x]["idRol"]);
                        rol = Convert.ToString(dt.Rows[x]["nombreRol"]);
                        
                        escritura = Convert.ToBoolean(dt.Rows[x]["permiso"]);

                        fila = creaFilaTabla(x + 1, idRol, rol, escritura);
                        //par = (par == false) ? true : false;
                        //fila.BackColor = (par) ? System.Drawing.Color.WhiteSmoke : System.Drawing.Color.White;
                        fila.CssClass = "hoverRow";
                        
                        tabla.Controls.Add(fila);
                    };

                    // Anexamos la tabla al div contenedor
                    Content_Privilegios.Controls.Clear();
                    Content_Privilegios.Controls.Add(tabla);

                };
            }
        }


        // EVENTOS DE BOTONES PRINCIPALES
        protected void Guardar_Click(object sender, EventArgs e)
        {

            Table tabla = (Table)Content_Privilegios.FindControl("Tabla_Privilegios");
            TableRow fila = new TableRow();
            TableCell celdaRol = new TableCell();
            CheckBox chkescritura = new CheckBox();
            HiddenField hiddenIdRol = new HiddenField();
            int tot_filas = tabla.Rows.Count - 1; // menos la fila de los títulos
            int idRol = 0;
            


            for (int x = 1; x <= tot_filas; x++)
            {
                // Se obtienen los datos de la fila
                fila = tabla.Rows[x];
                celdaRol = fila.Cells[0];

                hiddenIdRol = (HiddenField)celdaRol.FindControl(Convert.ToString(x) + "_hiddenIdRol");
                idRol = Convert.ToInt32(hiddenIdRol.Value);
                chkescritura = (CheckBox)Content_Privilegios.FindControl(x.ToString() + "_check");


                if (chkescritura.Checked)
                {
                    rolService.GuardarUsuarioRol(Convert.ToInt32(Id_Usuario.Value), idRol);
                }
                else
                {
                    rolService.EliminarUsuarioRol(Convert.ToInt32(Id_Usuario.Value), idRol);
                }

            }
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            Content_Botones.Visible = true;
            Content_Privilegios.Visible = true;
        }

        protected void Finalizar_Click(object sender, EventArgs e)
        {
            Content_Botones.Visible = true;
            Content_Privilegios.Visible = true;
            Response.Redirect("~/Administrador/Usuarios/listUsuarios.aspx");
        }

        // OTROS PROCEDIMIENTOS Y FUNCIONES
        protected TableRow creaFilaTabla(int num_fila, int idRol, string rol, bool lectura)
        {
            // Construímos los checkbox
            CheckBox chklectura = new CheckBox();
            chklectura.ID = num_fila.ToString() + "_check";
            chklectura.Checked = lectura;


            // Contruímos un label para lel rol
            Label objRol = new Label();
            objRol.Text = rol;

            // Construímos un hiddenfield para guardar la idSeccion
            HiddenField hiddenIdRol = new HiddenField();
            hiddenIdRol.ID = num_fila.ToString() + "_hiddenIdRol";
            hiddenIdRol.Value = idRol.ToString();


            TableCell td1 = new TableCell();
            td1.Controls.Add(objRol);
            td1.Controls.Add(hiddenIdRol);
            td1.Style.Add("padding", "5px");
            


            TableCell td2 = new TableCell();
            td2.Controls.Add(chklectura);
            td2.HorizontalAlign = HorizontalAlign.Center;


            TableRow fila = new TableRow();
            fila.Controls.Add(td1);
            fila.Controls.Add(td2);
            


            return fila;
        }



    }
}
