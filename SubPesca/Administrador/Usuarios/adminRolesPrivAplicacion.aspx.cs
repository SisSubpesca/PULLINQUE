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
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using Datos.Contantes;

namespace SubPesca.Administrador.Usuarios
{
    public partial class adminRolesPrivAplicacion : System.Web.UI.Page
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
                if (!permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRACION_DE_ROLES }, usuario_logeado, null, rbAccion.EDITAR))
                {
                    Response.Redirect("~/ingreso.aspx");
                };


                Guardar.Visible = true;
                Cancelar.Visible = true;
                link_adminRoles.Visible = true;

                // Inicializamos los comboboxs
                Initialize_Comboboxs();
            };

            int idRol = Convert.ToInt32(Roles.SelectedValue);
            int idMenu = Convert.ToInt32(Menus.SelectedValue);
            Carga_Privilegios(idRol, idMenu);
        }


        // ACCIONES GENERALES
        protected void Initialize_Comboboxs()
        {
            Carga_Combobox("Modulos");
            Carga_Combobox("Roles");
            Carga_Combobox("Menus");
        }


        protected void Carga_Combobox(object sender, EventArgs e)
        {
            LinkButton boton_clickeado = (LinkButton)sender;
            switch (boton_clickeado.ID)
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

                case "Modulos":
                    // Cargamos el combobox: Grupos de usuarios
                    string modulo = Modulos.SelectedValue;
                    Modulos.DataSource = parametroGenericoDA.ListarModuloSistema(0);
                    Modulos.Items.Clear();
                    Modulos.DataTextField = "nombreModuloSist";
                    Modulos.DataValueField = "idModuloSistema";
                    Modulos.DataBind();
                    Modulos.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    try
                    {
                        Modulos.SelectedValue = modulo;
                    }
                    catch
                    {
                        Modulos.SelectedValue = "-1";
                    };
                    break;

                case "Menus":
                    // Cargamos el combobox: Grupos de usuarios
                    string menu = Menus.SelectedValue;

                    Menus.Items.Clear();
                    Menus.DataBind();
                    Menus.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    try
                    {
                        Menus.SelectedValue = menu;
                    }
                    catch
                    {
                        Menus.SelectedValue = "-1";
                    };
                    break;

                case "Roles":
                    // Cargamos el combobox: Grupos de usuarios
                    string rol = Roles.SelectedValue;
                    Rol rolFiltro = new Rol();
                    rolFiltro.estadoVigencia = new ParametroGenerico(6);
                    Roles.DataSource = rolService.ListarRoles(rolFiltro);
                    Roles.Items.Clear();
                    Roles.DataTextField = "nombreRol";
                    Roles.DataValueField = "idRol";
                    Roles.DataBind();
                    Roles.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    try
                    {
                        Roles.SelectedValue = rol;
                    }
                    catch
                    {
                        Roles.SelectedValue = "-1";
                    };
                    break;
            };
        }




        protected void Modulos_change(object sender, EventArgs e)
        {

            if(Convert.ToInt32(Modulos.SelectedValue) > 0){

                Menus.DataSource = parametroGenericoDA.ListarMenuSistema(0, Convert.ToInt32(Modulos.SelectedValue));
                Menus.Items.Clear();
                Menus.DataTextField = "descripcion";
                Menus.DataValueField = "id";
                Menus.DataBind();
                Menus.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
            
            }else{

                Menus.Items.Clear();
                Menus.DataBind();
                Menus.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            
            }

            

        }

        protected void Carga_Privilegios(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                int idRol = Convert.ToInt32(Roles.SelectedValue);
                int idMenu = Convert.ToInt32(Menus.SelectedValue);
                Carga_Privilegios(idRol, idMenu);
            }

        }



        protected void Carga_Privilegios(int idRol, int idMenu)
        {
            if (idRol > 0 && idMenu > 0)
            {
                Content_Botones.Visible = true;
                Content_Privilegios.Visible = true;

                ViewState["IDROL"] = idRol;
                ViewState["IDMENU"] = idMenu;

                DataTable dt = rolService.ListarAccionesSeccionRol(idRol, idMenu);
                int num_registros = dt.Rows.Count;
                if (num_registros > 0)
                {
                    // Se construye la tabla con el detalle de privilegios
                    TableHeaderCell th1 = new TableHeaderCell();
                    th1.Text = "Menú";
                    th1.Width = Unit.Pixel(150);
                    TableHeaderCell th2 = new TableHeaderCell();
                    th2.Text = "Sección";
                    th2.Width = Unit.Pixel(200);
                    TableHeaderCell th3 = new TableHeaderCell();
                    th3.Text = "Acción";
                    th3.Width = Unit.Pixel(225);
                    TableHeaderCell th4 = new TableHeaderCell();
                    th4.Text = "Permiso";
                    th4.Width = Unit.Pixel(80);
                    
                    TableHeaderRow thread = new TableHeaderRow();
                    thread.Controls.Add(th1);
                    thread.Controls.Add(th2);
                    thread.Controls.Add(th3);
                    thread.Controls.Add(th4);
                    
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
                    
                    string menu = "";
                    int idSeccion = 0;
                    string seccion = "";
                    int idAccion = 0;
                    string accion = "";
                    bool escritura = false;
                    

                    for (int x = 0; x < num_registros; x++)
                    {
                        menu = Convert.ToString(dt.Rows[x]["nombreMenuSist"]);
                        idSeccion = Convert.ToInt32(dt.Rows[x]["idSeccionSist"]);
                        seccion = Convert.ToString(dt.Rows[x]["nombreSeccSist"]);
                        idAccion = Convert.ToInt32(dt.Rows[x]["idAccion"]);
                        accion = Convert.ToString(dt.Rows[x]["nombreAccion"]);
                        
                        escritura = Convert.ToBoolean(dt.Rows[x]["permiso"]);

                        fila = creaFilaTabla(x + 1, menu, idSeccion, seccion, idAccion, accion, escritura);
                        //fila.BackColor = (par) ? System.Drawing.Color.WhiteSmoke : System.Drawing.Color.White;
                        //par = (par == false) ? true : false;
                        fila.CssClass = "hoverRow";
                        tabla.Controls.Add(fila);
                    };

                    // Anexamos la tabla al div contenedor
                    Content_Privilegios.Controls.Clear();
                    Content_Privilegios.Controls.Add(tabla);
                };
            }
            else
            {
                Content_Botones.Visible = false;
                Content_Privilegios.Visible = false;
            };
        }


        // EVENTOS DE BOTONES PRINCIPALES
        protected void Guardar_Click(object sender, EventArgs e)
        {
            Table tabla = (Table)Content_Privilegios.FindControl("Tabla_Privilegios");
            TableRow fila = new TableRow();
            TableCell celdaSeccion = new TableCell();
            TableCell celdaAccion = new TableCell();
            CheckBox chkescritura = new CheckBox();
            HiddenField hiddenIdSeccion = new HiddenField();
            HiddenField hiddenIdAccion = new HiddenField();
            int tot_filas = tabla.Rows.Count - 1; // menos la fila de los títulos
            int idSeccion = 0;
            int idAccion = 0;
            int intRol = (int)ViewState["IDROL"];
            

            for (int x = 1; x <= tot_filas; x++)
            {
                // Se obtienen los datos de la fila
                fila = tabla.Rows[x];
                celdaSeccion = fila.Cells[1];
                celdaAccion = fila.Cells[2];

                hiddenIdSeccion = (HiddenField)celdaSeccion.FindControl(Convert.ToString(x) + "_hiddenIdSeccion");
                hiddenIdAccion = (HiddenField)celdaAccion.FindControl(Convert.ToString(x) + "_hiddenIdAccion");
                idSeccion = Convert.ToInt32(hiddenIdSeccion.Value);
                idAccion = Convert.ToInt32(hiddenIdAccion.Value);
                chkescritura = (CheckBox)Content_Privilegios.FindControl(x.ToString() + "_check");
                            
                
                if (chkescritura.Checked){
                    rolService.GuardarSeccionRol(intRol, idSeccion, idAccion);   
                }else {
                    rolService.EliminarSeccionRol(intRol, idSeccion, idAccion);
                }
                
            }
        }
        
        
        protected void Cancelar_Click(object sender, EventArgs e)
        {
            Roles.SelectedValue = "-1";
            Menus.SelectedValue = "-1";
            Content_Botones.Visible = false;
            Content_Privilegios.Visible = false;
        }
        
        
        protected void Finalizar_Click(object sender, EventArgs e)
        {
            Roles.SelectedValue = "-1";
            Menus.SelectedValue = "-1";
            Content_Botones.Visible = false;
            Content_Privilegios.Visible = false;
        }

        // OTROS PROCEDIMIENTOS Y FUNCIONES
        protected TableRow creaFilaTabla(int num_fila, string menu, int idSeccion, string seccion, int idAccion, string accion, bool lectura)
        {
            // Construímos los checkbox
            CheckBox chklectura = new CheckBox();
            chklectura.ID = num_fila.ToString() + "_check";
            chklectura.AutoPostBack = true;
            chklectura.Checked = lectura;
            
            

            // Contruímos un label para la seccion
            Label objSeccion = new Label();
            objSeccion.Text = seccion;

            // Contruímos un label para la accion
            Label objAccion = new Label();
            objAccion.Text = accion;

            // Construímos un hiddenfield para guardar la idSeccion
            HiddenField hiddenIdSeccion = new HiddenField();
            hiddenIdSeccion.ID = num_fila.ToString() + "_hiddenIdSeccion";
            hiddenIdSeccion.Value = idSeccion.ToString();


            // Construímos un hiddenfield para guardar la idSeccion
            HiddenField hiddenIdAccion = new HiddenField();
            hiddenIdAccion.ID = num_fila.ToString() + "_hiddenIdAccion";
            hiddenIdAccion.Value = idAccion.ToString();



            TableCell td1 = new TableCell();
            td1.Text = menu;
            td1.Height = Unit.Pixel(30);
            td1.Style.Add("padding", "5px");
            
            TableCell td2 = new TableCell();
            td2.Controls.Add(objSeccion);
            td2.Controls.Add(hiddenIdSeccion);
            td2.Style.Add("padding", "5px");
            
            TableCell td3 = new TableCell();
            td3.Controls.Add(objAccion);
            td3.Controls.Add(hiddenIdAccion);
            td3.HorizontalAlign = HorizontalAlign.Center;
            
            TableCell td4 = new TableCell();
            td4.Controls.Add(chklectura);
            td4.HorizontalAlign = HorizontalAlign.Center;
            
            
            TableRow fila = new TableRow();
            fila.Controls.Add(td1);
            fila.Controls.Add(td2);
            fila.Controls.Add(td3);
            fila.Controls.Add(td4);
            

            return fila;
        }

       

    }
}
