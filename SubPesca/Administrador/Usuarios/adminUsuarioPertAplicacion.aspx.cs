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
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;


namespace SubPesca.Administrador.Usuarios
{
    public partial class adminUsuarioPertAplicacion : System.Web.UI.Page
    {
        Datos.Entidades.Usuario usuarios = new Datos.Entidades.Usuario();
        Datos.Utilidades.Funciones fnc = new Datos.Utilidades.Funciones();
        UsuarioService usuarioService = new UsuarioService();
        RolService rolService = new RolService();
        TipoDA tipoDA = new TipoDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        PermisosService permisosService = new PermisosService();


        EnviarCorreo enviarCorreo = new EnviarCorreo();

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
                if (!permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.ADMINISTRACION_DE_USUARIOS }, usuario_logeado, null, rbAccion.ASIGNACION_DE_PERT))
                {
                    Response.Redirect("~/ingreso.aspx");
                };

                Guardar.Visible = true;
                //link_adminPert.Visible = true;
                

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


                Initialize_Comboboxs();

            };

            //Carga_Pert(Convert.ToInt32(Id_Usuario.Value), Convert.ToInt32(TipoTramite.SelectedValue), NPert.Text);

        }


        protected void Initialize_Comboboxs()
        {
            Carga_Combobox("TipoTramite");
        }


        protected void Carga_Combobox(string combobox)
        {
            switch (combobox)
            {

                case "TipoTramite":
                    // Cargamos el combobox: Grupos de usuarios
                    string tramite = TipoTramite.SelectedValue;
                    TipoTramite.DataSource = tipoDA.ListarTipo("TIPO_TRAMITE");
                    TipoTramite.Items.Clear();
                    TipoTramite.DataTextField = "descripcion";
                    TipoTramite.DataValueField = "id";
                    TipoTramite.DataBind();
                    TipoTramite.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    ListItem  itemRelocalizacion = TipoTramite.Items.FindByValue("139");
                    TipoTramite.Items.Remove(itemRelocalizacion);  //se remueve ya que se va a filtrar por cada sector

                    try
                    {
                        TipoTramite.SelectedValue = tramite;
                    }
                    catch
                    {
                        TipoTramite.SelectedValue = "-1";
                    };
                    break;
            };
        }


        protected void BotonFiltrar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //Carga_Pert(Convert.ToInt32(Id_Usuario.Value), Convert.ToInt32(TipoTramite.SelectedValue), NPert.Text);


                SolicitudConcesion solicitudConcesion = new SolicitudConcesion();
                DatosSolicitudUE datosSolicitudUE = new DatosSolicitudUE();


                solicitudConcesion.numPert = NPert.Text;
                solicitudConcesion.tipoTramite = new ParametroGenerico(Convert.ToInt32(TipoTramite.SelectedValue));


                ViewState["FILTRO_ASIGNACION_DE_PERT"] = solicitudConcesion;

                //SI PRESIONA EN EL BOTON FILTRAR LA LISTA DE RESULTADOS DEBE VENIR CARGADA CON LOS CHECK DESDE LA BASE DE DATOS
                Session["CheckedIDs"] = usuarioService.ListarCheckMarcados(Convert.ToInt32(TipoTramite.SelectedValue), Convert.ToInt32(Id_Usuario.Value), NPert.Text);


                CargaGrilla();



                //DESPUES DE CARGAR POR PRIMERA VEZ LA LISTA, SE DEBEN SETEAR LOS CHECK DE LA PRIMERA PAGINA
                var selectedIDs = (Session["CheckedIDs"] != null) ? Session["CheckedIDs"] as List<int> : new List<int>();

                if (selectedIDs != null && selectedIDs.Count > 0)
                {

                    bool marcarTodos = true;
                    foreach (GridViewRow row in GridVwHeaderChckbox.Rows)
                    {
                        var emailCheckBox = row.FindControl("chkEmp") as CheckBox;
                        var rowOrgID = Convert.ToInt32(GridVwHeaderChckbox.DataKeys[row.RowIndex].Value);
                        if (selectedIDs.Contains(rowOrgID))
                        {
                            emailCheckBox.Checked = true;
                        }
                        else
                        {
                            marcarTodos = false;
                        }
                    }

                    if (marcarTodos)
                    {
                        CheckBox ChkBoxHeader = (CheckBox)GridVwHeaderChckbox.HeaderRow.FindControl("chkboxSelectAll2");
                        ChkBoxHeader.Checked = true;
                    }
                }

                Session["CheckedIDs"] = (selectedIDs.Count > 0) ? selectedIDs : null;



                Session["AgregadosIDs"] = null;
                Session["BorradosIDs"] = null;


            }
        }



        protected void CargaGrilla()
        {
            MensajeBusqueda.Text = "";
            PanelMensajeBusqueda.Visible = false;
            UpdatePanelMensajeBusqueda.Update();


            PanelListaPert.Visible = false;


            SolicitudConcesion filtro = (SolicitudConcesion)ViewState["FILTRO_ASIGNACION_DE_PERT"];


            DataTable dt = usuarioService.ListarTramitesSolicitudes(filtro.tipoTramite.id, Convert.ToInt32(Id_Usuario.Value), filtro.numPert);
            GridVwHeaderChckbox.DataSource = dt;
            GridVwHeaderChckbox.DataBind();


            if (dt.Rows.Count  == 0)
            {
                MensajeBusqueda.Text = "Su búsqueda no ha obtenido resultados";
                PanelMensajeBusqueda.Visible = true;
                UpdatePanelMensajeBusqueda.Update();
                Content_Botones.Visible = false;
                
            }
            else
            {
                PanelListaPert.Visible = true;
                Content_Botones.Visible = true;
            }


        }


        /*

        protected void Carga_Pert(int idUsuario, int idTipoTramite, String nPert)
        {

            if (idUsuario > 0 && idTipoTramite > 0)
            {

                Content_Botones.Visible = true;
                Content_Pert.Visible = true;


                DataTable dt = usuarioService.ListarTramitesSolicitudes(idTipoTramite, idUsuario, nPert);

                int num_registros = dt.Rows.Count;

                if (num_registros > 0)
                {
                    // Se construye la tabla con el detalle de privilegios
                    TableHeaderCell th1 = new TableHeaderCell();
                    th1.Text = "NumPert/Identificador";
                    th1.Width = Unit.Pixel(200);

                    TableHeaderCell th2 = new TableHeaderCell();
                    th2.Text = "Tipo Tramite";
                    th2.Width = Unit.Pixel(300);

                    TableHeaderCell th3 = new TableHeaderCell();
                    th3.Text = "";
                    th3.Width = Unit.Pixel(80);

                    TableHeaderRow thread = new TableHeaderRow();
                    thread.Controls.Add(th1);
                    thread.Controls.Add(th2);
                    thread.Controls.Add(th3);
                    thread.BackColor = System.Drawing.Color.SkyBlue;
                    thread.Height = Unit.Pixel(30);

                    Table tabla = new Table();
                    tabla.ID = "Tabla_Pert";
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

                    int id = 0;
                    string numPert = "";
                    int idTipoTra = 0;
                    string nombreTipoTramite = "";
                    bool escritura = false;


                    for (int x = 0; x < num_registros; x++)
                    {
                        id = Convert.ToInt32(dt.Rows[x]["id"]);
                        numPert = Convert.ToString(dt.Rows[x]["numPert"]);
                        idTipoTra = Convert.ToInt32(dt.Rows[x]["idTipoTramite"]);
                        nombreTipoTramite = Convert.ToString(dt.Rows[x]["nombreTipoTramite"]);

                        escritura = Convert.ToBoolean(dt.Rows[x]["permiso"]);

                        fila = creaFilaTabla(x + 1, id, numPert, idTipoTra, nombreTipoTramite, escritura);
                        par = (par == false) ? true : false;
                        fila.BackColor = (par) ? System.Drawing.Color.WhiteSmoke : System.Drawing.Color.White;
                        tabla.Controls.Add(fila);
                    };

                    // Anexamos la tabla al div contenedor
                    Content_Pert.Controls.Clear();
                    Content_Pert.Controls.Add(tabla);

                };
            }else
            {
                Content_Botones.Visible = false;
                Content_Pert.Visible = false;
            };
        }

         * */

        // EVENTOS DE BOTONES PRINCIPALES
        /*
        protected void Guardar_Click(object sender, EventArgs e)
        {

            Table tabla = (Table)Content_Pert.FindControl("Tabla_Pert");
            TableRow fila = new TableRow();
            TableCell celdaID = new TableCell();
            TableCell celdaTramite = new TableCell();
            CheckBox chkescritura = new CheckBox();

            HiddenField hiddenId = new HiddenField();
            HiddenField hiddenIdTipoTramite = new HiddenField();
            
            int tot_filas = tabla.Rows.Count - 1; // menos la fila de los títulos
            int id = 0;
            int idTipoTramite = 0;



            for (int x = 1; x <= tot_filas; x++)
            {
                // Se obtienen los datos de la fila
                fila = tabla.Rows[x];
                celdaID = fila.Cells[0];
                celdaTramite = fila.Cells[1];

                hiddenId = (HiddenField)celdaTramite.FindControl(Convert.ToString(x) + "_hiddenId");
                id = Convert.ToInt32(hiddenId.Value);

                hiddenIdTipoTramite = (HiddenField)celdaTramite.FindControl(Convert.ToString(x) + "_hiddenIdTipoTramite");
                idTipoTramite = Convert.ToInt32(hiddenIdTipoTramite.Value);


                chkescritura = (CheckBox)Content_Pert.FindControl(x.ToString() + "_check");


                if (chkescritura.Checked)
                {
                    usuarioService.GuardarSolicitudUsuario(Convert.ToInt32(Id_Usuario.Value), idTipoTramite, id, true);
                }
                else
                {
                    usuarioService.EliminarSolicitudUsuario(Convert.ToInt32(Id_Usuario.Value), idTipoTramite, id);
                }

            }
        }
        */

        protected void Guardar_Click(object sender, EventArgs e)
        {

            SolicitudConcesion filtro = (SolicitudConcesion)ViewState["FILTRO_ASIGNACION_DE_PERT"];
            //List<int> CheckedIDs =  (List<int>)Session["CheckedIDs"];


            if (filtro != null) {


                var agregadosIDs = (Session["AgregadosIDs"] != null) ? Session["AgregadosIDs"] as List<int> : new List<int>();
                var borradosIDs = (Session["BorradosIDs"] != null) ? Session["BorradosIDs"] as List<int> : new List<int>();

                if (borradosIDs != null)
                {

                    foreach (int id in borradosIDs)
                    {
                        usuarioService.EliminarSolicitudUsuario(Convert.ToInt32(Id_Usuario.Value), filtro.tipoTramite.id, id);
                    }
                }



                if (agregadosIDs != null)
                {

                    foreach (int id in agregadosIDs)
                    {
                        usuarioService.GuardarSolicitudUsuario(Convert.ToInt32(Id_Usuario.Value), filtro.tipoTramite.id, id, true);
                    }

                    /* Enviar correo electrónico con los trámites asignados */

                    try
                    {

                        if (agregadosIDs != null && agregadosIDs.Count > 0) {

                            enviarCorreo.pertAsingnadoUsuario(Convert.ToInt32(Id_Usuario.Value), filtro.tipoTramite.id, agregadosIDs);
                        }

                        
                    }
                    catch (Exception)
                    {

                    }


                }


                /*
                 * 
                
                //SE BORRAN LOS PERT SEGUN LA BUSQUEDA DEL USUARIO
                usuarioService.EliminarSolicitudUsuario(Convert.ToInt32(Id_Usuario.Value), filtro.tipoTramite.id, filtro.numPert);


                //DE LA PAGINA ACTUAL SE GUARDA LA INFORMACION DE LOS CHECK MARCADOS (POR SI NUNCA APRETO EN LA PAGINACION) 
                var selectedIDs = (Session["CheckedIDs"] != null) ? Session["CheckedIDs"] as List<int> : new List<int>();

                foreach (GridViewRow row in GridVwHeaderChckbox.Rows)
                {

                    var selCheckBox = row.FindControl("chkEmp") as CheckBox;
                    var rowOrgID = Convert.ToInt32(GridVwHeaderChckbox.DataKeys[row.RowIndex].Value);
                    var isRowIDPresentInList = selectedIDs.Contains(rowOrgID);

                    if (selCheckBox.Checked && !isRowIDPresentInList)
                    {
                        selectedIDs.Add(rowOrgID);
                    }

                    if (!selCheckBox.Checked && isRowIDPresentInList)
                    {
                        selectedIDs.Remove(rowOrgID);
                    }
                }

                
                //SE VUELVEN A AGREGAR LOS MARCADOS
                if (CheckedIDs != null)
                {

                    foreach (int id in CheckedIDs)
                    {
                        usuarioService.GuardarSolicitudUsuario(Convert.ToInt32(Id_Usuario.Value), filtro.tipoTramite.id, id, true);
                    }
                }
                 * 
                 * */

            }
        }



        protected void Cancelar_Click(object sender, EventArgs e)
        {
            Content_Botones.Visible = true;
            Content_Pert.Visible = true;
        }

        protected void Finalizar_Click(object sender, EventArgs e)
        {
            Content_Botones.Visible = true;
            Content_Pert.Visible = true;
        }


        // OTROS PROCEDIMIENTOS Y FUNCIONES

        /*
        protected TableRow creaFilaTabla(int num_fila, int id, string numPert, int idTipoTramite, string nombreTipoTramite, bool lectura)
        {
            // Construímos los checkbox
            CheckBox chklectura = new CheckBox();
            chklectura.ID = num_fila.ToString() + "_check";
            chklectura.Checked = lectura;


            // Contruímos un label para el identificador
            Label objID = new Label();
            objID.Text = numPert;

            // Contruímos un label para el tipo tramite
            Label objTipoTramite = new Label();
            objTipoTramite.Text = nombreTipoTramite;

            // Construímos un hiddenfield para guardar la idSolicitud o idTipoTramite
            HiddenField hiddenId = new HiddenField();
            hiddenId.ID = num_fila.ToString() + "_hiddenId";
            hiddenId.Value = id.ToString();

            // Construímos un hiddenfield para guardar la idTipoTramite
            HiddenField hiddenIdTipoTramite = new HiddenField();
            hiddenIdTipoTramite.ID = num_fila.ToString() + "_hiddenIdTipoTramite";
            hiddenIdTipoTramite.Value = idTipoTramite.ToString();


            TableCell td1 = new TableCell();
            td1.Controls.Add(objID);
            td1.Controls.Add(hiddenId);
            td1.Style.Add("padding-left", "5px");

            TableCell td2 = new TableCell();
            td2.Controls.Add(objTipoTramite);
            td2.Controls.Add(hiddenIdTipoTramite);
            td2.Style.Add("padding-left", "5px");

            TableCell td3 = new TableCell();
            td3.Controls.Add(chklectura);
            td3.HorizontalAlign = HorizontalAlign.Center;


            TableRow fila = new TableRow();
            fila.Controls.Add(td1);
            fila.Controls.Add(td2);
            fila.Controls.Add(td3);



            return fila;
        }

        */



        //GRILLA CON PAGINACION
        protected void GridVwHeaderChckbox_RowDataBound(object sender, GridViewRowEventArgs e)
        {


        }



        protected void GridVwHeaderChckbox_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            /*

            switch (e.CommandName)
            {
                case "ModificarCheck":
                  
                    int id = Convert.ToInt32(e.CommandArgument);

                    CheckBox check = (CheckBox)sender;
                    
                    var agregadosIDs = (Session["AgregadosIDs"] != null) ? Session["AgregadosIDs"] as List<int> : new List<int>();
                    var borradosIDs = (Session["BorradosIDs"] != null) ? Session["BorradosIDs"] as List<int> : new List<int>();

                    if (check.Checked)
                    {
                        agregadosIDs.Add(id);
                        borradosIDs.Remove(id);

                    }else {
                        borradosIDs.Add(id);
                        agregadosIDs.Remove(id);
                    }
                    
                    break;
            };

             * */

        }



        protected void GridVwHeaderChckbox_RowCreated(object sender, GridViewRowEventArgs e)
        {


        }


        /**
         * Check que cambia toda la lista
         **/
        protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)GridVwHeaderChckbox.HeaderRow.FindControl("chkboxSelectAll2");

            var agregadosIDs = (Session["AgregadosIDs"] != null) ? Session["AgregadosIDs"] as List<int> : new List<int>();
            var borradosIDs = (Session["BorradosIDs"] != null) ? Session["BorradosIDs"] as List<int> : new List<int>();

            foreach (GridViewRow row in GridVwHeaderChckbox.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkEmp");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;

                    if (!agregadosIDs.Contains(Int32.Parse(ChkBoxRows.Text)))
                    {
                        agregadosIDs.Add(Int32.Parse(ChkBoxRows.Text));
                    }
                    borradosIDs.Remove(Int32.Parse(ChkBoxRows.Text));

                }
                else
                {
                    ChkBoxRows.Checked = false;

                    if (!borradosIDs.Contains(Int32.Parse(ChkBoxRows.Text)))
                    {
                        borradosIDs.Add(Int32.Parse(ChkBoxRows.Text));
                    }
                    agregadosIDs.Remove(Int32.Parse(ChkBoxRows.Text));
                }
            }

            Session["AgregadosIDs"] = agregadosIDs;
            Session["BorradosIDs"] = borradosIDs;
        }

        /**
         * Check de una fila en particular
         **/
        protected void chkEmp_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox check = (CheckBox)sender;

            var agregadosIDs = (Session["AgregadosIDs"] != null) ? Session["AgregadosIDs"] as List<int> : new List<int>();
            var borradosIDs = (Session["BorradosIDs"] != null) ? Session["BorradosIDs"] as List<int> : new List<int>();

            if (check.Checked)
            {
                if (!agregadosIDs.Contains(Int32.Parse(check.Text)))
                {
                    agregadosIDs.Add(Int32.Parse(check.Text));
                }
                borradosIDs.Remove(Int32.Parse(check.Text));

            }
            else
            {
                if (!borradosIDs.Contains(Int32.Parse(check.Text)))
                {
                    borradosIDs.Add(Int32.Parse(check.Text));
                }
                agregadosIDs.Remove(Int32.Parse(check.Text));
            }


            Session["AgregadosIDs"] =agregadosIDs;
            Session["BorradosIDs"] = borradosIDs;

        }

        



        protected void GridVwHeaderChckbox_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {


            SolicitudConcesion solicitudFiltro;
            solicitudFiltro = (SolicitudConcesion)ViewState["FILTRO_ASIGNACION_DE_PERT"];
            if (solicitudFiltro == null)
            {
                solicitudFiltro = new SolicitudConcesion();
            }


            //ANTES DE CAMBIAR DE PAGINA SE ACTUALIZACION EN SESSION LOS CHECK SELECIONADOS

            var selectedIDs = (Session["CheckedIDs"] != null) ? Session["CheckedIDs"] as List<int> : new List<int>();

            foreach (GridViewRow row in GridVwHeaderChckbox.Rows)
            {

                var selCheckBox = row.FindControl("chkEmp") as CheckBox;
                var rowOrgID = Convert.ToInt32(GridVwHeaderChckbox.DataKeys[row.RowIndex].Value);
                var isRowIDPresentInList = selectedIDs.Contains(rowOrgID);

                if (selCheckBox.Checked && !isRowIDPresentInList)
                {
                    selectedIDs.Add(rowOrgID);
                }

                if (!selCheckBox.Checked && isRowIDPresentInList)
                {
                    selectedIDs.Remove(rowOrgID);
                }
            }



            solicitudFiltro.pagina = e.NewPageIndex;
            ViewState["FILTRO_ASIGNACION_DE_PERT"] = solicitudFiltro;

            GridVwHeaderChckbox.PageIndex = e.NewPageIndex;
            GridVwHeaderChckbox.DataBind();
            CargaGrilla();




            //DESPUES DE CAMBIAR DE PAGINA, SE SETEAN LOS VALORES EN LOS CHECK SEGUN LA INFORMACION QUE ESTE EN SESSION
            bool marcarTodos = true;
            foreach (GridViewRow row in GridVwHeaderChckbox.Rows)
            {
                var emailCheckBox = row.FindControl("chkEmp") as CheckBox;
                var rowOrgID = Convert.ToInt32(GridVwHeaderChckbox.DataKeys[row.RowIndex].Value);
                if (selectedIDs.Contains(rowOrgID))
                {
                    emailCheckBox.Checked = true;
                }
                else
                {
                    marcarTodos = false;
                }
            }

            if (marcarTodos)
            {
                CheckBox ChkBoxHeader = (CheckBox)GridVwHeaderChckbox.HeaderRow.FindControl("chkboxSelectAll2");
                ChkBoxHeader.Checked = true;
            }

            Session["CheckedIDs"] = (selectedIDs.Count > 0) ? selectedIDs : null;



        }



    }
}

