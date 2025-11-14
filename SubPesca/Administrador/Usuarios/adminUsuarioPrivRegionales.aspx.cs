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
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.usuario;
using LogicaNegocio.cl.subpesca.rb.servicios.usuario;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Contantes;
using System.Collections.Generic;


namespace SubPesca.Administrador.Usuarios
{
    public partial class adminUsuarioPrivRegionales : System.Web.UI.Page
    {
        
        Datos.Entidades.Region Eregion = new Datos.Entidades.Region();
        Datos.Utilidades.Funciones fnc = new Datos.Utilidades.Funciones();
        UsuarioService usuarioService = new UsuarioService();
        TipoDA tipoDA = new TipoDA();

        // CARGA DE ARCHIVOS .JS DESDE C#
        protected void Page_Init(object sender, System.EventArgs e)
        {
            HtmlGenericControl scriptInclude = new HtmlGenericControl();

            scriptInclude = (HtmlGenericControl)Page.Header.FindControl("funciones.js");
            if (scriptInclude == null)
            {
                scriptInclude = new HtmlGenericControl("script");
                scriptInclude.Attributes["type"] = "text/javascript";
                scriptInclude.Attributes["src"] = ResolveClientUrl("~/js/funciones.js");
                scriptInclude.ID = "funciones.js";
                Page.Header.Controls.Add(scriptInclude);
            };
        }


        // PAGE LOAD
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
                // Se recibe el id_usuario
                try
                {
                    if (Request.QueryString["id_usuario"] != null)
                    {
                        IdUsuario.Value = Convert.ToString(Convert.ToInt32(Request.QueryString["id_usuario"]));
                    }
                    else
                    {
                        IdUsuario.Value = "0";
                    };

                    Datos.Entidades.Usuario usuarioFiltro = new Datos.Entidades.Usuario();
                    usuarioFiltro.id_usuario = Convert.ToInt32(IdUsuario.Value);

                    Datos.Entidades.Usuario user = usuarioService.ObtenerRbUsuario(usuarioFiltro);
                    if (user.id_usuario != 0)
                    {
                        Usuario.Text = Convert.ToString(user.usuario);
                    }
                    else
                    {
                        Response.Redirect("~/Administrador/Usuarios/listUsuarios.aspx");
                    };
                }
                catch
                {
                    IdUsuario.Value = "0";
                };


                // Asignamos id_region
                IdRegion.Value = "0";
                TipoTramite.SelectedValue = Convert.ToString(rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);

                Initialize_Comboboxs();
                
                Initialize_HT_Regiones(Convert.ToInt32(TipoTramite.SelectedValue),"");

            };

            
            if (IdRegion.Value == "0")
            {
                Carga_PrivRegionales(Convert.ToInt32(IdUsuario.Value), Convert.ToInt32(TipoTramite.SelectedValue));
            }
            else
            {
                Carga_PrivComunales(Convert.ToInt32(IdRegion.Value), Convert.ToInt32(IdUsuario.Value), Convert.ToInt32(TipoTramite.SelectedValue));
            };
            
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
                    
                    string tramite = TipoTramite.SelectedValue;
                    TipoTramite.DataSource = tipoDA.ListarTipo("TIPO_TRAMITE");
                    TipoTramite.Items.Clear();
                    TipoTramite.DataTextField = "descripcion";
                    TipoTramite.DataValueField = "id";
                    TipoTramite.DataBind();
                    TipoTramite.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    ListItem itemRelocalizacion = TipoTramite.Items.FindByValue("139");
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

        protected void SubTipo_Tramite(object sender, EventArgs e) 
        {
            int Index = Convert.ToInt32(TipoTramite.SelectedValue);

            if (Index == rbTipo.TIPO_TRAMITE_MODIFICACION)
            { 
                //Dejamos visible el Dropdown

                UpdatePanelSubtipoTramiteMod.Visible = true;
                SubTipoTramiteMod.DataSource = tipoDA.ListarTipo("TIPO MODIFICACIÓN CONCESIÓN");
                SubTipoTramiteMod.Items.Clear();
                SubTipoTramiteMod.DataTextField = "descripcion";
                SubTipoTramiteMod.DataValueField = "id";
                SubTipoTramiteMod.DataBind();
                UpdatePanelSubtipoTramiteMod.Update();
            }
            //558 modificacion amerb
            else if (Index == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB)
            {
                //Dejamos visible el Dropdown

                UpdatePanelSubtipoTramiteMod.Visible = true;
                SubTipoTramiteMod.DataSource = tipoDA.ListarTipo("TIPO MODIFICACIÓN AMERB");
                SubTipoTramiteMod.Items.Clear();
                SubTipoTramiteMod.DataTextField = "descripcion";
                SubTipoTramiteMod.DataValueField = "id";
                SubTipoTramiteMod.DataBind();
                UpdatePanelSubtipoTramiteMod.Update();
            }
            //552 modificacion acopio
            else if (Index == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_ACOPIO)
            {
                //Dejamos visible el Dropdown

                UpdatePanelSubtipoTramiteMod.Visible = true;
                SubTipoTramiteMod.DataSource = tipoDA.ListarTipo("TIPO MODIFICACIÓN CENTRO ACOPIO");
                SubTipoTramiteMod.Items.Clear();
                SubTipoTramiteMod.DataTextField = "descripcion";
                SubTipoTramiteMod.DataValueField = "id";
                SubTipoTramiteMod.DataBind();
                UpdatePanelSubtipoTramiteMod.Update();
            }
            //546 mod faenamiento
            else if (Index == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_FAENAMIENTO)
            {
                //Dejamos visible el Dropdown

                UpdatePanelSubtipoTramiteMod.Visible = true;
                SubTipoTramiteMod.DataSource = tipoDA.ListarTipo("TIPO MODIFICACIÓN FAENAMIENTO");
                SubTipoTramiteMod.Items.Clear();
                SubTipoTramiteMod.DataTextField = "descripcion";
                SubTipoTramiteMod.DataValueField = "id";
                SubTipoTramiteMod.DataBind();
                UpdatePanelSubtipoTramiteMod.Update();
            }
            //564 EMCPO
            else if (Index == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_ECMPO)
            {
                //Dejamos visible el Dropdown

                UpdatePanelSubtipoTramiteMod.Visible = true;
                SubTipoTramiteMod.DataSource = tipoDA.ListarTipo("TIPO MODIFICACIÓN ECMPO");
                SubTipoTramiteMod.Items.Clear();
                SubTipoTramiteMod.DataTextField = "descripcion";
                SubTipoTramiteMod.DataValueField = "id";
                SubTipoTramiteMod.DataBind();
                UpdatePanelSubtipoTramiteMod.Update();
            }

            else if (Index == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION) 
            {
                //genero la data para relozalización TIPO_RELOCALIZACION
                UpdatePanelSubtipoTramiteMod.Visible = true;
                SubTipoTramiteMod.DataSource = tipoDA.ListarTipo("TIPO_RELOCALIZACION");
                SubTipoTramiteMod.Items.Clear();
                SubTipoTramiteMod.DataTextField = "descripcion";
                SubTipoTramiteMod.DataValueField = "id";
                SubTipoTramiteMod.DataBind();
                UpdatePanelSubtipoTramiteMod.Update();
            }
            else if (Index == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION_RESA)
            {
                UpdatePanelSubtipoTramiteMod.Visible = true;
                SubTipoTramiteMod.DataSource = tipoDA.ListarTipo("TIPO_RELOCALIZACION RESA");
                SubTipoTramiteMod.Items.Clear();
                SubTipoTramiteMod.DataTextField = "descripcion";
                SubTipoTramiteMod.DataValueField = "id";
                SubTipoTramiteMod.DataBind();
                UpdatePanelSubtipoTramiteMod.Update();
            }
            else
            {
                UpdatePanelSubtipoTramiteMod.Visible = false;
                UpdatePanelSubtipoTramiteMod.Update();
            }
        }


        // ACCIONES GENERALES
        protected Hashtable Initialize_HT_Comunas(int id_region, int id_usuario, int tipoTramite, string subtipotramite)
        {
            Hashtable HT_Comunas = new Hashtable();
            Hashtable HT_Comuna = new Hashtable();
            int id_comuna = 0;
            DataTable dt = usuarioService.ListarUsuariosPrivComunalesPorTramite(id_region, id_usuario, tipoTramite, subtipotramite);
            int num_registros = dt.Rows.Count;
            if (num_registros > 0)
            {
                // Recorremos todas las comunas
                for (int i = 0; i < num_registros; i++)
                {
                    id_comuna = Convert.ToInt32(dt.Rows[i]["IdComuna"]);
                    HT_Comuna.Add("comuna", Convert.ToString(dt.Rows[i]["Comuna"]));
                    HT_Comuna.Add("prv", Convert.ToBoolean(dt.Rows[i]["prv"]));
                    HT_Comunas.Add(id_comuna, (Hashtable)HT_Comuna);
                    HT_Comuna = new Hashtable();
                };
            };

            return HT_Comunas;
        }

        protected void Actualiza_HT_Comunas()
        {
            Table tabla = (Table)Content_Privilegios.FindControl("Tabla_PrivComunales");
            TableRow fila = new TableRow();
            TableCell celda = new TableCell();
            CheckBox chkpermiso = new CheckBox();
            HiddenField hidden = new HiddenField();
            int tot_filas = tabla.Rows.Count - 1; // menos la fila de los títulos
            int id_objeto = 0;
            int id_usuario = Convert.ToInt32(IdUsuario.Value);
            int id_region = Convert.ToInt32(IdRegion.Value);
            Hashtable HT_Regiones = (Hashtable)ViewState["Privilegios"];
            Hashtable HT_Region = (Hashtable)HT_Regiones[id_region];
            Hashtable HT_Comunas = (Hashtable)HT_Region["Comunas"];
            Hashtable HT_Comuna = new Hashtable();
            bool prvreg = false;
            bool allcomunas = true;

            for (int x = 0; x < tot_filas; x++)
            {
                // Se obtienen los datos de la fila
                fila = tabla.Rows[x];
                celda = fila.Cells[0];
                hidden = (HiddenField)celda.FindControl(Convert.ToString(x + 1) + "_hiddencomuna");
                id_objeto = Convert.ToInt32(hidden.Value);
                chkpermiso = (CheckBox)celda.FindControl(id_objeto.ToString() + "_prvcomuna");
                HT_Comuna = (Hashtable)HT_Comunas[id_objeto];
                HT_Comuna["prv"] = chkpermiso.Checked;
                HT_Comunas[id_objeto] = (Hashtable)HT_Comuna;
                if (prvreg == false && chkpermiso.Checked == true)
                {
                    prvreg = true;
                };
                if(!chkpermiso.Checked)
                {
                    allcomunas = false;
                };
            };
            HT_Region["Comunas"] = (Hashtable)HT_Comunas;
            HT_Region["prv"] = (bool)prvreg;
            HT_Region["allcomunas"] = (bool)allcomunas;
            HT_Regiones[id_region] = (Hashtable)HT_Region;
            ViewState["Privilegios"] = (Hashtable)HT_Regiones;        
        }
       
        protected void Initialize_HT_Regiones(int tipoTramite,string subtipotramite)
        {
            Hashtable HT_Regiones = new Hashtable();
            Hashtable HT_Region = new Hashtable();
            Hashtable HT_Comunas = new Hashtable();
            Hashtable HT_Comuna = new Hashtable();
            int id_usuario = Convert.ToInt32(IdUsuario.Value);
            int id_region = 0;
            DataTable dt = usuarioService.ListarUsuarioPrivRegionalesPorTramite(id_usuario, tipoTramite, subtipotramite);
            int num_registros = dt.Rows.Count;
            if (num_registros > 0)
            {
                // Recorremos todas las regiones
                for (int i = 0; i < num_registros; i++)
                {
                    id_region = Convert.ToInt32(dt.Rows[i]["IdRegion"]);
                    HT_Comunas = Initialize_HT_Comunas(id_region, id_usuario, tipoTramite, subtipotramite);

                    HT_Region.Add("codigo", Convert.ToInt32(dt.Rows[i]["Codigo"]));
                    HT_Region.Add("region", Convert.ToString(dt.Rows[i]["Region"]));
                    HT_Region.Add("prv", Convert.ToBoolean(dt.Rows[i]["prv"]));
                    HT_Region.Add("allcomunas", Convert.ToBoolean(dt.Rows[i]["todas_las_comunas"]));
                    HT_Region.Add("Comunas", (Hashtable)HT_Comunas);
                    HT_Regiones.Add(id_region, (Hashtable)HT_Region);
                    HT_Region = new Hashtable();
                };
            };

            // Actualizamos el viewstate de Privilegios
            ViewState["Privilegios"] = (Hashtable)HT_Regiones;
        }

        protected void Carga_PrivComunales(int id_region, int id_usuario, int tipoTramite)
        {
            // Se construye la tabla con el detalle de los privilegios regionales
            TableHeaderCell th1 = new TableHeaderCell();
            th1.Text = "Asignar Comuna";
            th1.Width = Unit.Pixel(110);
            TableHeaderCell th2 = new TableHeaderCell();
            th2.Text = "Región";
            th2.Width = Unit.Pixel(60);
            TableHeaderCell th3 = new TableHeaderCell();
            th3.Text = "Comuna";
            th3.Width = Unit.Pixel(396);
            TableHeaderRow thread = new TableHeaderRow();
            thread.Controls.Add(th1);
            thread.Controls.Add(th2);
            thread.Controls.Add(th3);
            thread.BackColor = System.Drawing.Color.SkyBlue;
            thread.Height = Unit.Pixel(30);
            Table tabla = new Table();
            tabla.ID = "Tabla_PrivComunales";
            tabla.CellPadding = 4;
            tabla.CellSpacing = 1;
            tabla.BorderWidth = Unit.Pixel(1);
            tabla.BorderColor = System.Drawing.Color.Silver;
            tabla.BorderStyle = BorderStyle.Solid;
            tabla.Width = Unit.Percentage(100);
            tabla.Controls.Add(thread);

            Hashtable HT_Regiones = (Hashtable)ViewState["Privilegios"];
            Hashtable HT_Region = (Hashtable)HT_Regiones[id_region];
            Hashtable HT_Comunas = (Hashtable)HT_Region["Comunas"];
            Hashtable HT_Comuna = new Hashtable();
            Hashtable HT_ComunasTMP = new Hashtable();
            TableRow fila = new TableRow();
            int id_comuna = 0;
            RegionDA region = new RegionDA();
            MantenedorDA Mantenedor = new MantenedorDA();
            DataTable Eregion = Mantenedor.ListarRegion_Mantenedor(id_region, 0, "");
            int codigo_reg = Convert.ToInt32(Eregion.Rows[0]["Codigo"]);
            int x = 0;
            string comuna = "";
            bool prv = false;
            bool par = false;
            string comuna_menor = "";

            foreach (DictionaryEntry c in HT_Comunas)
            {
                HT_ComunasTMP.Add(c.Key, c.Value);
            };

            while (HT_ComunasTMP.Count > 0)
            {
                comuna_menor = "";
                foreach (DictionaryEntry c in HT_ComunasTMP)
                {
                    HT_Comuna = (Hashtable)c.Value;
                    comuna = (string)HT_Comuna["comuna"];
                    if (comuna_menor == "" || string.Compare(comuna, comuna_menor) == -1)
                    {
                        comuna_menor = comuna;
                        id_comuna = (int)c.Key;
                    };
                };
                HT_Comuna = (Hashtable)HT_ComunasTMP[id_comuna];

                // Agregamos las filas a la tabla. Cada fila representa una comuna con el detalle de sus privilegios.
                comuna = (string)HT_Comuna["comuna"];
                prv = (bool)HT_Comuna["prv"];
                x = x + 1;
                fila = creaFilaTabla("comuna", x, id_comuna, codigo_reg, comuna, prv, false);
                fila.BackColor = (par) ? System.Drawing.Color.WhiteSmoke : System.Drawing.Color.White;
                par = (par == false) ? true : false;
                tabla.Controls.Add(fila);

                // Quitamos la comuna del HT Temporal
                HT_ComunasTMP.Remove(id_comuna);
            };

            // Anexamos la tabla al div contenedor
            Content_Privilegios.Controls.Clear();
            Content_Privilegios.Controls.Add(tabla);
        }
        
        protected void Carga_PrivComunales(object sender, EventArgs e)
        {
            LinkButton boton_clickeado = (LinkButton)sender;
            int id_usuario = Convert.ToInt32(IdUsuario.Value);
            int id_region = Convert.ToInt32(boton_clickeado.CommandArgument);
            int tipoTramite = Convert.ToInt32(TipoTramite.SelectedValue);

            IdRegion.Value = id_region.ToString();
            Carga_PrivComunales(id_region, id_usuario, tipoTramite);
        }
        
        protected void Carga_PrivRegionales(int id_usuario, int tipoTramite)
        {
            // Se construye la tabla con el detalle de los privilegios regionales
            TableHeaderCell th1 = new TableHeaderCell();
            th1.Text = "Asignar Región";
            th1.Width = Unit.Pixel(110);
            TableHeaderCell th2 = new TableHeaderCell();
            th2.Text = "Código";
            th2.Width = Unit.Pixel(60);
            TableHeaderCell th3 = new TableHeaderCell();
            th3.Text = "Región";
            th3.Width = Unit.Pixel(396);
            TableHeaderRow thread = new TableHeaderRow();
            thread.Controls.Add(th1);
            thread.Controls.Add(th2);
            thread.Controls.Add(th3);
            thread.BackColor = System.Drawing.Color.SkyBlue;
            thread.Height = Unit.Pixel(30);
            Table tabla = new Table();
            tabla.ID = "Tabla_PrivRegionales";
            tabla.CellPadding = 4;
            tabla.CellSpacing = 1;
            tabla.BorderWidth = Unit.Pixel(1);
            tabla.BorderColor = System.Drawing.Color.Silver;
            tabla.BorderStyle = BorderStyle.Solid;
            tabla.Width = Unit.Percentage(100);
            tabla.Controls.Add(thread);

            Hashtable HT_RegionesTMP = new Hashtable();
            Hashtable HT_Regiones = (Hashtable)ViewState["Privilegios"];
            Hashtable HT_Region = new Hashtable();
            TableRow fila = new TableRow();
            int id_region = 0;
            int codigo = 0;
            int x = 0;
            string region = "";
            bool prv = false;
            bool allcomunas = false;
            bool par = false;
            int codigo_menor = 0;

            foreach (DictionaryEntry r in HT_Regiones)
            {
                HT_RegionesTMP.Add(r.Key, r.Value);
            };

            while (HT_RegionesTMP.Count > 0)
            {
                codigo_menor = 0;
                foreach (DictionaryEntry r in HT_RegionesTMP)
                {
                    HT_Region = (Hashtable)r.Value;
                    codigo = (int)HT_Region["codigo"];
                    if (codigo_menor == 0 || codigo < codigo_menor)
                    {
                        codigo_menor = codigo;
                        id_region = (int)r.Key;
                    };
                };
                HT_Region = (Hashtable)HT_RegionesTMP[id_region];

                // Agregamos las filas a la tabla. Cada fila representa una región con el detalle de sus privilegios.
                codigo = (int)HT_Region["codigo"];
                region = (string)HT_Region["region"];
                prv = (bool)HT_Region["prv"];
                allcomunas = (bool)HT_Region["allcomunas"];
                x = x + 1;
                fila = creaFilaTabla("region", x, id_region, codigo, region, prv, allcomunas);
                fila.BackColor = (par) ? System.Drawing.Color.WhiteSmoke : System.Drawing.Color.White;
                par = (par == false) ? true : false;
                tabla.Controls.Add(fila);

                // Quitamos la región del HT Temporal
                HT_RegionesTMP.Remove(id_region);
            };

            // Anexamos la tabla al div contenedor
            Content_Privilegios.Controls.Clear();
            Content_Privilegios.Controls.Add(tabla);
        }
        
        protected void Carga_PrivRegionales(object sender, EventArgs e)
        {
            // Actualizamos el HT con los cambios de permisos a las comunas
            Actualiza_HT_Comunas();
            IdRegion.Value = "0";
            int id_usuario = Convert.ToInt32(IdUsuario.Value);
            int tipoTramite = Convert.ToInt32(TipoTramite.SelectedValue);
            // verificamos que si el tipo de tramite seleccionado es un modifica
          
            Carga_PrivRegionales(id_usuario, tipoTramite);
        }
        
        protected TableRow creaFilaTabla(string tipo_fila, int num_fila, int id_item, int codigo_region, string item, bool prv, bool allcomunas)
        {
            // Construímos el checkbox
            CheckBox chkprv = new CheckBox();
            chkprv.ID = id_item.ToString() + "_prv" + tipo_fila;
            if (tipo_fila == "region")
            {
                chkprv.AutoPostBack = true;
                chkprv.CheckedChanged += new EventHandler(Modifica_PrivRegionales);
            }
            else
            {
                chkprv.AutoPostBack = false;
            };
            chkprv.Checked = prv;

            //se debe modificar
            Panel divchkbox = new Panel();
            if (tipo_fila == "region")
            {
                divchkbox.ID = id_item.ToString() + "_divprv" + tipo_fila;
                if (!prv)
                {
                    divchkbox.CssClass = "divchkbox_red";
                }
                else
                {
                    if (!allcomunas)
                    {
                        divchkbox.CssClass = "divchkbox_yellow";
                    }
                    else
                    {
                        divchkbox.CssClass = "divchkbox_green";
                    };
                };
                divchkbox.Controls.Add(chkprv);
            };

            // Construímos un hiddenfield para guardar el id_item
            HiddenField hidden = new HiddenField();
            hidden.ID = num_fila.ToString() + "_hidden" + tipo_fila;
            hidden.Value = id_item.ToString();

            // Contruímos el codregion y la region
            LinkButton lnkcodregion = new LinkButton();
            Label lbcomuna = new Label();
            Label lbcodregion = new Label();
            LinkButton lnkregion = new LinkButton();
            switch (tipo_fila)
            {
                case "comuna":
                    lnkcodregion.ID = id_item.ToString() + "_" + tipo_fila;
                    lnkcodregion.Text = Convert.ToString(codigo_region);
                    lnkcodregion.Command += new CommandEventHandler(Carga_PrivRegionales);
                    lbcomuna.Text = item;
                    break;
                case "region":
                    lbcodregion.Text = codigo_region.ToString();
                    lnkregion.ID = id_item.ToString() + "_" + tipo_fila;
                    lnkregion.Text = item;
                    lnkregion.CommandName = item;
                    lnkregion.CommandArgument = id_item.ToString();
                    lnkregion.Command += new CommandEventHandler(Carga_PrivComunales);
                    break;
            };

            // Construímos la fila y sus columnas
            TableCell td1 = new TableCell();
            if (tipo_fila == "region")
            {
                td1.Controls.Add(divchkbox);
            }
            else
            {
                td1.Controls.Add(chkprv);
            };
            td1.Controls.Add(hidden);
            td1.Height = Unit.Pixel(30);
            td1.HorizontalAlign = HorizontalAlign.Center;
            TableCell td2 = new TableCell();
            td2.HorizontalAlign = HorizontalAlign.Center;
            TableCell td3 = new TableCell();
            td3.Style.Add("padding-left", "5px");

            switch (tipo_fila)
            {
                case "comuna":
                    td2.Controls.Add(lnkcodregion);
                    td3.Controls.Add(lbcomuna);
                    break;
                case "region":
                    td2.Controls.Add(lbcodregion);
                    td3.Controls.Add(lnkregion);
                    break;
            };
            TableRow fila = new TableRow();
            fila.Controls.Add(td1);
            fila.Controls.Add(td2);
            fila.Controls.Add(td3);

            return fila;
        }
        

        // EVENTOS DE BOTONES
        protected void Modifica_PrivRegionales(object sender, EventArgs e)
        {
            CheckBox chkprv = (CheckBox)sender;
            string id = chkprv.ID;
            int id_region = Convert.ToInt32(id.Substring(0, id.LastIndexOf("_prv")));
            Hashtable HT_Regiones = (Hashtable)ViewState["Privilegios"];
            Hashtable HT_Region = (Hashtable)HT_Regiones[id_region];
            Hashtable HT_Comunas = (Hashtable)HT_Region["Comunas"];
            Hashtable HT_Comuna = new Hashtable();
            Hashtable HT_ComunasTMP = new Hashtable();
            Hashtable HT_ComunaTMP = new Hashtable();

            HT_Region["prv"] = chkprv.Checked;
            HT_Region["allcomunas"] = chkprv.Checked;
            foreach (DictionaryEntry c in HT_Comunas)
            {
                HT_Comuna = (Hashtable)c.Value;
                HT_ComunaTMP.Add("comuna", (string)HT_Comuna["comuna"]);
                HT_ComunaTMP.Add("prv", chkprv.Checked);
                HT_ComunasTMP.Add((int)c.Key, (Hashtable)HT_ComunaTMP);
                HT_ComunaTMP = new Hashtable();
            };
            HT_Region["Comunas"] = (Hashtable)HT_ComunasTMP;
            HT_Regiones[id_region] = (Hashtable)HT_Region;
            ViewState["Privilegios"] = (Hashtable)HT_Regiones;

            int id_usuario = Convert.ToInt32(IdUsuario.Value);
            int tipoTramite = Convert.ToInt32(TipoTramite.SelectedValue);

            Carga_PrivRegionales(id_usuario, tipoTramite);
        }


        protected void Guardar_Click(object sender, EventArgs e)
        {
            bool result = false;
            int id_region = Convert.ToInt32(IdRegion.Value);
            int tipoTramite = Convert.ToInt32(TipoTramite.SelectedValue);
            List<int> SelectedIndex = new List<int>();

            if (id_region > 0)
            {
                Actualiza_HT_Comunas();
            };

            Hashtable HT_Regiones = (Hashtable)ViewState["Privilegios"];
            
            int id_usuario = Convert.ToInt32(IdUsuario.Value);
            string mensaje = "";

            //falta modificar PL y probar es de a uno no se necesita la lista modificar!
            #region Modificacion
            //se debe verificar si el tipo tramite seleccionado es un modificar o un resa, debido a eso se debe verificar si se selecciona un subtipo.
            if (tipoTramite == 89 || tipoTramite == 558 || tipoTramite == 552 || tipoTramite == 546 || tipoTramite == 564)
            {
                //se verifica si tiene valor o no el subtramite
                if (SubTipoTramiteMod.SelectedValue.Count() > 0 || !SubTipoTramiteMod.SelectedValue.Equals(""))
                {
                    //agregar el campo 
                    //dado a lo conversado si no se selecciona ningun tipo este debe ser llenado con todos los subtipos.
                    foreach (ListItem item in SubTipoTramiteMod.Items)
                    {
                        if (item.Selected)
                        {
                            usuarioService.ActualizarUsuarioPrivComunalesPertPorTramite(id_usuario, HT_Regiones, tipoTramite,Convert.ToInt32(item.Value));
                        }
                    }
                    result = true;
                }
                else
                {
                    foreach (ListItem item in SubTipoTramiteMod.Items)
                    {
                        usuarioService.ActualizarUsuarioPrivComunalesPertPorTramite(id_usuario, HT_Regiones, tipoTramite, Convert.ToInt32(item.Value));
                    }
                    result = true;
                }
            }
            #endregion
            else if (tipoTramite == 95 || tipoTramite == 624)
            #region resa
            {
                if (SubTipoTramiteMod.SelectedValue.Count() > 0 || !SubTipoTramiteMod.SelectedValue.Equals(""))
                {
                    foreach (ListItem item in SubTipoTramiteMod.Items)
                    {
                        if (item.Selected)
                        {
                            usuarioService.ActualizarUsuarioPrivComunalesPertPorTramite(id_usuario, HT_Regiones, tipoTramite, Convert.ToInt32(item.Value));  
                        }
                    }
                    result = true;
                }
                else
                {
                    foreach (ListItem item in SubTipoTramiteMod.Items)
                    {
                        usuarioService.ActualizarUsuarioPrivComunalesPertPorTramite(id_usuario, HT_Regiones, tipoTramite, Convert.ToInt32(item.Value));
                    }
                    result = true;
                }
            }
            #endregion
            else
            {
                usuarioService.ActualizarUsuarioPrivComunalesPertPorTramite(id_usuario, HT_Regiones, tipoTramite,0);
                result = true;
            }

            if (result)
            {
                mensaje = "Se han actualizado las regiones visibles para el usuario.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                Content_msgGrilla.Visible = true;
                msgGrilla.Text = mensaje;
            };
        }

        protected void BotonFiltrar_Click(object sender, EventArgs e)
        {

            limpiarMensajeAlerta();

            string parametroSubT = "";
            //obtener subtipotramite
            foreach (ListItem item in SubTipoTramiteMod.Items)
            {
                if (item.Selected)
                {
                    if (parametroSubT.Length <= 0)
                    {
                        parametroSubT = item.Value.ToString();
                    }
                    else
                    {
                        parametroSubT = parametroSubT + "," + item.Value;
                    }
                }
            }

            //Obtener filtro de búsqueda por trámite del usuario
            int tipoTramite = Convert.ToInt32(TipoTramite.Text);
            //int Subtipotramite = Convert.ToInt32(SubTipoTramiteMod.Text);

            if (tipoTramite > 0)
            {
                Initialize_HT_Regiones(tipoTramite,parametroSubT);
                Carga_PrivRegionales(Convert.ToInt32(IdUsuario.Value), tipoTramite);

                PanelGuardar.Visible = true;
            }
            else {
                string mensaje = "Debe seleccionar el tipo de trámite.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                Content_msgGrilla.Visible = true;
                msgGrilla.Text = mensaje;
            }
        }


        protected void limpiarMensajeAlerta() {

            Content_msgGrilla.Visible = false;
            msgGrilla.Text = "";
        }
    }
}
