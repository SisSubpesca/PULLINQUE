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
using LogicaNegocio.cl.subpesca.rb.servicios.estados;
using Datos.Contantes;
using Datos.Utilidades;
using SubPesca.Utilidades;

//@used

namespace SubPesca.Administrador.Reportes
{
    public partial class plazosRequerimientos : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        Datos.Entidades.Solicitudes conn5 = new Datos.Entidades.Solicitudes();

        Funciones funciones = new Funciones();
        EstadoService estadoService = new EstadoService();
        TipoDA tipoDa = new TipoDA();

        ComunaDA comunaDA = new LogicaNegocio.cl.subpesca.rb.common.ComunaDA();
        RegionDA regionDA = new LogicaNegocio.cl.subpesca.rb.common.RegionDA();
        ProvinciaDA provinciaDA = new LogicaNegocio.cl.subpesca.rb.common.ProvinciaDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();


        string defaultKeySort = "Numero ASC";


        // PAGE LOAD
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
   
                // Inicializamos el Hashtable contenedor de los filtros de búsqueda
                Initialize_HT_ListEstados();

                // Inicializamos el formulario
                Initialize_Form();

                // Cargamos la grilla
                CargaGrilla();
            };
        }


        // PROCEDIMIENTOS GENERALES
        protected void Initialize_HT_ListEstados()
        {
            // Se reciben los datos básicos de búsqueda
            string KeySort = defaultKeySort;
            bool locked = true;
            string grilla = "REQUERIMIENTOS_POR_VENCER";
            int id_region = -1;
            int id_provincia = -1;
            int id_comuna = -1;
            int id_tiposolicitud = -1;
            int id_tipodestinatario = -1;
            Hashtable HT_ModPlazos = (Hashtable)Session["Modulo_Plazos"];
            Hashtable HT_ListEstados = new Hashtable();

            if (HT_ModPlazos != null)
            {
                HT_ListEstados = (Hashtable)HT_ModPlazos["ListEstados"];
                if (HT_ListEstados != null)
                {
                    locked = (bool)HT_ListEstados["locked"];
                    // Si el formulario de búsqueda está desbloqueado significa que fue abiertoa travez de clickeos en los links de la aplicación  
                    // De estar bloqueado, los filtros de búsqueda seran reseteados
                    if (!locked)
                    {
                        KeySort = (string)HT_ListEstados["KeySort"];
                        grilla = (string)HT_ListEstados["grilla"];
                        id_region = (int)HT_ListEstados["id_region"];
                        id_provincia = (int)HT_ListEstados["id_provincia"];
                        id_comuna = (int)HT_ListEstados["id_comuna"];
                        id_tiposolicitud = (int)HT_ListEstados["id_tiposolicitud"];
                        id_tipodestinatario = (int)HT_ListEstados["id_tipodestinatario"];
                    };
                };
            };
            HT_ListEstados = new Hashtable();
            HT_ListEstados.Add("locked", true);
            HT_ListEstados.Add("KeySort", KeySort);
            HT_ListEstados.Add("grilla", grilla);
            HT_ListEstados.Add("id_region", id_region);
            HT_ListEstados.Add("id_provincia", id_provincia);
            HT_ListEstados.Add("id_comuna", id_comuna);
            HT_ListEstados.Add("id_tiposolicitud", id_tiposolicitud);
            HT_ListEstados.Add("id_tipodestinatario", id_tipodestinatario);

            // Actualizamos la sesión Modulo_Reportes
            if (HT_ModPlazos == null)
            {
                HT_ModPlazos = new Hashtable();
                HT_ModPlazos.Add("ListEstados", (Hashtable)HT_ListEstados);
            }
            else
            {
                if (HT_ModPlazos["ListEstados"] == null)
                {
                    HT_ModPlazos.Add("ListEstados", (Hashtable)HT_ListEstados);
                }
                else
                {
                    HT_ModPlazos["ListEstados"] = (Hashtable)HT_ListEstados;
                };
            };
            Session["Modulo_Plazos"] = (Hashtable)HT_ModPlazos;
        }


        protected void Initialize_Comboboxs()
        {
            try
            {

                Hashtable HT_ModReportes = (Hashtable)Session["Modulo_Plazos"];
                Hashtable HT_ListEstados = (Hashtable)HT_ModReportes["ListEstados"];
                int id_region = (int)HT_ListEstados["id_region"];
                int id_provincia = (int)HT_ListEstados["id_provincia"];
                int id_comuna = (int)HT_ListEstados["id_comuna"];
                int id_tiposolicitud = (int)HT_ListEstados["id_tiposolicitud"];
                int id_tipodestinatario = (int)HT_ListEstados["id_tipodestinatario"];
                

                Carga_Combobox("Regiones");
                Regiones.SelectedValue = id_region.ToString();

                Carga_Combobox("Provincias");
                Provincias.SelectedValue = id_provincia.ToString();

                Carga_Combobox("Comunas");
                Comunas.SelectedValue = id_comuna.ToString();

                Carga_Combobox("TiposSolicitud");
                TiposSolicitud.SelectedValue = id_tiposolicitud.ToString();

                Carga_Combobox("TiposDestinatario");
                TiposDestinatario.SelectedValue = id_tipodestinatario.ToString();

            }
            catch (Exception ex) {
                throw ex;
            }

        }


        protected void Carga_Combobox(string combobox)
        {
            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

            switch (combobox)
            {
                case "Regiones":
                    // Cargamos el combobox: Regiones
                    Regiones.Items.Clear();
                    Regiones.DataSource = regionDA.ListarRegion(0);
                    Regiones.DataTextField = "Region";
                    Regiones.DataValueField = "IdRegion";
                    Regiones.DataBind();
                    Regiones.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "Provincias":
                    // Cargamos el combobox: Provincias
                    Provincias.Items.Clear();
                    if (Convert.ToInt32(Regiones.SelectedValue) > 0)
                    {
                        Provincias.DataSource = parametroGenericoDA.ListarProvinciaReg(0, Convert.ToInt32(Regiones.SelectedValue));
                        Provincias.DataTextField = "descripcion";
                        Provincias.DataValueField = "id";
                        Provincias.DataBind();
                    };
                    Provincias.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "Comunas":
                    // Cargamos el combobox: Comunas
                    Comunas.Items.Clear();
                    if (Convert.ToInt32(Provincias.SelectedValue) > 0)
                    {
                        Comunas.DataSource = parametroGenericoDA.ListarComunaDataTable(0, Convert.ToInt32(Provincias.SelectedValue));
                        Comunas.DataTextField = "Comuna";
                        Comunas.DataValueField = "IdComuna";
                        Comunas.DataBind();
                    };
                    Comunas.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "TiposSolicitud":
                    // Cargamos el combobox: Tipos de Solicitud
                    TiposSolicitud.Items.Clear();
                    TiposSolicitud.DataSource = parametroGenericoDA.ListarTipoTramiteEstadoSolicitud(0);
                    TiposSolicitud.DataTextField = "nombreTipoInterfaz";
                    TiposSolicitud.DataValueField = "idTipoTramite";
                    TiposSolicitud.DataBind();
                    TiposSolicitud.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "TiposDestinatario":
                    // Cargamos el combobox: Tipos de destinatarios
                    TiposDestinatario.Items.Clear();
                    TiposDestinatario.DataSource = tipoDa.obtenerTipoDestinatario();
                    TiposDestinatario.DataTextField = "nombreTipoDestinatario";
                    TiposDestinatario.DataValueField = "idTipoDestinatario";
                    TiposDestinatario.DataBind();
                    TiposDestinatario.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
            };
        }

        protected void Initialize_Form()
        {
            // Cargamos los combobox
            Initialize_Comboboxs();

            Hashtable HT_ModReportes = (Hashtable)Session["Modulo_Plazos"];
            Hashtable HT_ListEstados = (Hashtable)HT_ModReportes["ListEstados"];
            string grilla = (string)HT_ListEstados["grilla"];
            switch (grilla)
            {
                case "REQUERIMIENTOS_POR_VENCER":
                    lnk_PorVencer.CssClass = "tab1_selected";
                    lnk_Vencidos.CssClass = "tab2";
                    GridView1.Visible = true;
                    GridView2.Visible = false;
                    break;
                case "REQUERIMIENTOS_VENCIDOS":
                    lnk_PorVencer.CssClass = "tab1";
                    lnk_Vencidos.CssClass = "tab2_selected";
                    GridView1.Visible = false;
                    GridView2.Visible = true;
                    break;
            };
        }


        // ACCIONES DE BOTONES y COMBOBOXS
        protected void Filtrar_Click(object sender, EventArgs e)
        {

            Hashtable HT_ModReportes = (Hashtable)Session["Modulo_Plazos"];
            Hashtable HT_ListEstados = (Hashtable)HT_ModReportes["ListEstados"];
            HT_ListEstados["id_region"] = Convert.ToInt32(Regiones.SelectedValue);
            HT_ListEstados["id_provincia"] = Convert.ToInt32(Provincias.SelectedValue);
            HT_ListEstados["id_comuna"] = Convert.ToInt32(Comunas.SelectedValue);
            HT_ListEstados["id_tiposolicitud"] = Convert.ToInt32(TiposSolicitud.SelectedValue);
            HT_ListEstados["id_tipodestinatario"] = Convert.ToInt32(TiposDestinatario.SelectedValue);

            HT_ModReportes["ListEstados"] = (Hashtable)HT_ListEstados;
            Session["Modulo_Plazos"] = (Hashtable)HT_ModReportes;


            if (Convert.ToInt32(TiposSolicitud.SelectedValue) < 1) {
                Page.Validators.Add(new ValidationError("FormErrores", "Tipo de solicitud de Unidad Espacial"));
            }


            UpdatePanelMensajesValidaciones.Update();


            if (Page.IsValid) {
                CargaGrilla();
            }

        }

        protected void Limpiar_Click(object sender, EventArgs e)
        {
            Regiones.SelectedValue = "-1";
            Provincias.SelectedValue = "-1";
            Comunas.SelectedValue = "-1";
            TiposSolicitud.SelectedValue = "-1";
            TiposDestinatario.SelectedValue = "-1";

            Hashtable HT_ModReportes = (Hashtable)Session["Modulo_Plazos"];
            Hashtable HT_ListEstados = (Hashtable)HT_ModReportes["ListEstados"];
            HT_ListEstados["id_region"] = Convert.ToInt32(Regiones.SelectedValue);
            HT_ListEstados["id_provincia"] = Convert.ToInt32(Provincias.SelectedValue);
            HT_ListEstados["id_comuna"] = Convert.ToInt32(Comunas.SelectedValue);
            HT_ListEstados["id_tiposolicitud"] = Convert.ToInt32(TiposSolicitud.SelectedValue);
            HT_ModReportes["ListEstados"] = (Hashtable)HT_ListEstados;
            Session["Modulo_Plazos"] = (Hashtable)HT_ModReportes;

            CargaGrilla();
        }


        protected void Regiones_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("Provincias");
            Carga_Combobox("Comunas");
        }


        protected void Provincias_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("Comunas");
        }


        protected void cambiaGrilla_Click(object sender, EventArgs e)
        {
            LinkButton boton = (LinkButton)sender;
            Hashtable HT_ModReportes = (Hashtable)Session["Modulo_Plazos"];
            Hashtable HT_ListEstados = (Hashtable)HT_ModReportes["ListEstados"];

            switch (boton.ID)
            {
                case "lnk_PorVencer":
                    HT_ListEstados["grilla"] = "REQUERIMIENTOS_POR_VENCER";
                    lnk_PorVencer.CssClass = "tab1_selected";
                    lnk_Vencidos.CssClass = "tab2";
                    break;
                case "lnk_Vencidos":
                    HT_ListEstados["grilla"] = "REQUERIMIENTOS_VENCIDOS";
                    lnk_PorVencer.CssClass = "tab1";
                    lnk_Vencidos.CssClass = "tab2_selected";
                    break;
            };
          


            if (HT_ListEstados["grilla"].Equals("REQUERIMIENTOS_POR_VENCER"))
            {
                GridView1.Visible = true;
                GridView2.Visible = false;
            }
            else if (HT_ListEstados["grilla"].Equals("REQUERIMIENTOS_VENCIDOS"))
            {
                GridView1.Visible = false;
                GridView2.Visible = true;
            }
            else {
                GridView1.Visible = true;
                GridView2.Visible = false;
            }


            HT_ListEstados["KeySort"] = defaultKeySort;
            HT_ModReportes["ListEstados"] = (Hashtable)HT_ListEstados;
            Session["Modulo_Plazos"] = (Hashtable)HT_ModReportes;

        }


        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            // Descargamos los filtros de búsqueda del Hashtable
            Hashtable HT_ModReportes = (Hashtable)Session["Modulo_Plazos"];
            Hashtable HT_ListEstados = (Hashtable)HT_ModReportes["ListEstados"];
            GridView grilla = new GridView();
            string nom_grilla = (string)HT_ListEstados["grilla"];
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "REQUERIMIENTOS_POR_VENCER":
                    grilla = GridView1;
                    ngrilla = "requerimientos_por_vencer.xls";
                    break;
                case "REQUERIMIENTOS_VENCIDOS":
                    grilla = GridView2;
                    ngrilla = "requerimientos_vencidos.xls";
                    break;
            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }
     



        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {


            try
            {

                // Descargamos los filtros de búsqueda del Hashtable
                Hashtable HT_ModReportes = new Hashtable();
                Hashtable HT_ListEstados = new Hashtable();
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];
                try
                {
                    HT_ModReportes = (Hashtable)Session["Modulo_Plazos"];
                    HT_ListEstados = (Hashtable)HT_ModReportes["ListEstados"];
                }
                catch
                {
                    Initialize_HT_ListEstados();
                    HT_ModReportes = (Hashtable)Session["Modulo_Plazos"];
                    HT_ListEstados = (Hashtable)HT_ModReportes["ListEstados"];
                };

                string grilla = (string)HT_ListEstados["grilla"];
                int id_region = (int)HT_ListEstados["id_region"];
                int id_provincia = (int)HT_ListEstados["id_provincia"];
                int id_comuna = (int)HT_ListEstados["id_comuna"];
                int id_tiposolicitud = (int)HT_ListEstados["id_tiposolicitud"];
                int id_tipodestinatario = (int)HT_ListEstados["id_tipodestinatario"];

                int id_tiposolicitudAux = funciones.equivalenciaIdTipoUE(id_tiposolicitud);
                int idTipoTamite = funciones.equivalenciaIdTipoTramite(id_tiposolicitud);
                int idSubTipoTamite = funciones.equivalenciaIdSubTipoTramite(id_tiposolicitud);

                DataTable dt = new DataTable();
                int num_registros = 0;


                dt = estadoService.Requerimientos_Por_Vencer_Listar_RB(id_region, id_provincia, id_comuna, id_tiposolicitudAux, usuario_logeado.id_usuario, idTipoTamite, idSubTipoTamite, id_tipodestinatario);

                if (dt != null)
                {
                    num_registros = dt.Rows.Count;
                    if (num_registros > 0)
                    {
                        Content_msgGrilla.Visible = false;
                        ExportarGrilla.Visible = true;
                    }
                    else
                    {
                        msgGrilla.Text = "No se han encontrado requerimientos vencidos.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        Content_msgGrilla.Visible = true;
                        ExportarGrilla.Visible = false;
                    };

                    // Se ordena el DataTable en el server de aplicación
                    dt.DefaultView.Sort = KeySort1.Value;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }



                DataTable dt2 = new DataTable();
                int num_registros2 = 0;

                dt2 = estadoService.Requerimientos_Vencidos_Listar_RB(id_region, id_provincia, id_comuna, id_tiposolicitudAux, usuario_logeado.id_usuario, idTipoTamite, idSubTipoTamite, id_tipodestinatario);


                if (dt2 != null)
                {
                    num_registros2 = dt2.Rows.Count;
                    if (num_registros2 > 0)
                    {
                        Content_msgGrilla.Visible = false;
                        ExportarGrilla.Visible = true;
                    }
                    else
                    {
                        msgGrilla.Text = "No se han encontrado requerimientos por vencer.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        Content_msgGrilla.Visible = true;
                        ExportarGrilla.Visible = false;
                    };

                    // Se ordena el DataTable en el server de aplicación
                    dt2.DefaultView.Sort = KeySort2.Value; 
                    GridView2.DataSource = dt2;
                    GridView2.DataBind();
                }




                if (HT_ListEstados["grilla"].Equals("REQUERIMIENTOS_POR_VENCER"))
                {
                    GridView1.Visible = true;
                    GridView2.Visible = false;
                }
                else if (HT_ListEstados["grilla"].Equals("REQUERIMIENTOS_VENCIDOS"))
                {
                    GridView1.Visible = false;
                    GridView2.Visible = true;
                }
                else
                {
                    GridView1.Visible = true;
                    GridView2.Visible = false;
                }

            }
            catch (Exception ex) {
                throw ex;
            }
        }


        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Content_msgGrilla.Visible = false;

            if ((e.CommandName == "commandPorVencer") || (e.CommandName == "commandVencidos"))
            {
                int id_estado = Convert.ToInt32(e.CommandArgument);
                string path = @"~/Administrador/Reportes/detallePlazo.aspx?idSubRequerimiento=" + id_estado.ToString();

                switch (e.CommandName)
                {
                    case "commandPorVencer":
                        Response.Redirect(path + "&tipo=1");
                        break;
                    case "commandVencidos":
                        Response.Redirect(path + "&tipo=2");
                        break;
                };
            };
        }


        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int id_estado = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                id_estado = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Numero"));
                e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#555555");
                switch (id_estado)
                {
                    case 34:
                        //e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFCEB7");
                        break;
                    case 6:
                        //e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FBF5B5");
                        break;
                    default:
                        //e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#CED8BF");
                        break;
                };
            };
        }


        protected void GridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sort = KeySort1.Value;
            int pos = 0;

            pos = sort.IndexOf(e.SortExpression + " ASC");
            if (pos >= 0)
            {
                sort = e.SortExpression + " DESC";
            }
            else
            {
                sort = e.SortExpression + " ASC";
            };
            KeySort1.Value = sort;
            GridView1.PageIndex = 0;
            GridView1.DataBind();

            CargaGrilla();
        }


        protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sort = KeySort2.Value;
            int pos = 0;

            pos = sort.IndexOf(e.SortExpression + " ASC");
            if (pos >= 0)
            {
                sort = e.SortExpression + " DESC";
            }
            else
            {
                sort = e.SortExpression + " ASC";
            };
            KeySort2.Value = sort;
            GridView2.PageIndex = 0;
            GridView2.DataBind();

            CargaGrilla();
        }
      
         


    }
}
