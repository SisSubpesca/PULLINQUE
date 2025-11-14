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
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.estados;
using LogicaNegocio.cl.subpesca.rb.solicitud;

//@used

namespace SubPesca.Administrador.Reportes
{
    public partial class detallePlazo : System.Web.UI.Page
    {
        Datos.Entidades.Solicitudes conn1 = new Datos.Entidades.Solicitudes();
        EstadoService estadoService = new EstadoService();
        RequerimientoDA requerimientoDA = new RequerimientoDA();
        TipoDA tipoDa = new TipoDA();

        Usuario.Serializable usuario_logeado = new Usuario.Serializable(); // Usuario logueado en el sistema
        Datos.Utilidades.Funciones fnc = new Datos.Utilidades.Funciones();

        ComunaDA comunaDa = new ComunaDA();
        RegionDA regionDA = new RegionDA();
        ProvinciaDA provinciaDA = new ProvinciaDA();
        UsuarioDA usuarioDA = new UsuarioDA();

        // PAGE LOAD
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                // Obtención de parámetros mediante GET/POST
                ObtencionParametros();

                // Inicializamos el Hashtable contenedor de los filtros de búsqueda
                Initialize_HT_ListSolicitudes();

                // Completamos los campos del listado
                Initialize_Form();

                // Cargamos la grilla
                CargaGrilla();
            };
        }


        // ACCIONES GENERALES
        protected void Initialize_HT_ListSolicitudes()
        {
            // Se reciben los datos básicos de búsqueda
            string KeySort = "IdSolicitud ASC";
            int pagina = 0;
            int id_subRequerimiento = Convert.ToInt32(IdSubRequerimiento.Value);
            int tipo = Convert.ToInt32(Tipo.Value);
            bool locked = true;
            Hashtable HT_ModPlazos = (Hashtable)Session["Modulo_Plazos"];
            Hashtable HT_ListEstados = new Hashtable();
            Hashtable HT_ListSolicitudes = new Hashtable();
            Hashtable HT_detallePlazo = new Hashtable();

            HT_detallePlazo.Add("id_subRequerimiento", id_subRequerimiento);
            HT_detallePlazo.Add("tipo", tipo);


            if (HT_ModPlazos != null)
            {
                HT_ListSolicitudes = (Hashtable)HT_ModPlazos["ListSolicitudes"];
                if (HT_ListSolicitudes != null)
                {
                    locked = (bool)HT_ListSolicitudes["locked"];
                    // Si el formulario está desbloqueado significa que fue abiertoa travez de clickeos en los links de la aplicación  
                    // De estar bloqueado, los filtros serán reseteados
                    if (!locked)
                    {
                        KeySort = (string)HT_ListSolicitudes["KeySort"];
                        pagina = (int)HT_ListSolicitudes["pagina"];
                    };
                };
            };
            HT_ListSolicitudes = new Hashtable();
            HT_ListSolicitudes.Add("locked", true);
            HT_ListSolicitudes.Add("KeySort", KeySort);
            HT_ListSolicitudes.Add("pagina", pagina);
            HT_ListSolicitudes.Add("tipo", tipo);

            // Actualizamos la sesión Modulo_Reportes
            if (HT_ModPlazos == null)
            {
                HT_ModPlazos = new Hashtable();
                HT_ModPlazos.Add("ListSolicitudes", (Hashtable)HT_ListSolicitudes);
            }
            else
            {
                if (HT_ModPlazos["ListSolicitudes"] == null)
                {
                    HT_ModPlazos.Add("ListSolicitudes", (Hashtable)HT_ListSolicitudes);
                }
                else
                {
                    HT_ModPlazos["ListSolicitudes"] = (Hashtable)HT_ListSolicitudes;
                };
            };


            if ((Hashtable)HT_ModPlazos["detallePlazo"] != null) {
                HT_ModPlazos.Remove("detallePlazo");
            }

            HT_ModPlazos.Add("detallePlazo", HT_detallePlazo);
            Session["Modulo_Plazos"] = (Hashtable)HT_ModPlazos;
        }

        protected void Initialize_Form()
        {
            SubRequerimiento subRequerimiento = null;
            DataTable dt = new DataTable();
            Hashtable HT_ModReportes = (Hashtable)Session["Modulo_Plazos"];
            Hashtable HT_ListEstados = new Hashtable();
            usuario_logeado = (Usuario.Serializable)Session["Usuario"];
            int id_region = -1;
            int id_provincia = -1;
            int id_comuna = -1;
            int id_tiposolicitud = -1;
            int id_tipodestinatario = -1;

            SubRequerimiento subrequerimientoFiltro = new SubRequerimiento();
            subrequerimientoFiltro.idSubRequerimiento = Convert.ToInt32(IdSubRequerimiento.Value);
            subrequerimientoFiltro.aplicaReiteraFiltro = -1;
            subrequerimientoFiltro.aplicaComplementarioFiltro = -1;
            subrequerimientoFiltro.aplicaVisacionMasivaFiltro = -1;

            subRequerimiento = requerimientoDA.ObtenerSubRequerimiento(subrequerimientoFiltro);
            try
            {
                Requerimiento.Text = subRequerimiento.nombreSubRequerimiento;
            }
            catch { };

            if (HT_ModReportes != null)
            {
                HT_ListEstados = (Hashtable)HT_ModReportes["ListEstados"];
                if (HT_ListEstados != null)
                {
                    id_region = (int)HT_ListEstados["id_region"];
                    id_provincia = (int)HT_ListEstados["id_provincia"];
                    id_comuna = (int)HT_ListEstados["id_comuna"];
                    id_tiposolicitud = (int)HT_ListEstados["id_tiposolicitud"];
                    id_tipodestinatario = (int)HT_ListEstados["id_tipodestinatario"];
                };
            };

            if (id_region > 0)
            {

                DataTable Eregion = regionDA.obtenerRegion(id_region);


                if (Eregion != null) {
                    Region.Text = Convert.ToString(dt.Rows[0]["Region"]);
                }

              
            }

            if (id_provincia > 0)
            {
                Datos.Entidades.Provincia eProvincia = provinciaDA.Obtener(id_provincia);
                Provincia.Text = eProvincia.provincia;
                tr_provincia.Visible = true;
            };
            if (id_comuna > 0)
            {
                Datos.Entidades.Comuna Ecomuna = comunaDa.Obtener(id_comuna);
                Comuna.Text = Ecomuna.comuna;
                tr_comuna.Visible = true;
            };
            if (id_tiposolicitud > 0)
            {
                dt = estadoService.Solicitudes_TipoSolicitud_Ver_RB(id_tiposolicitud);
                TipoSolic.Text = Convert.ToString(dt.Rows[0]["TipoSolicitud"]);
                tr_tiposolic.Visible = true;
            };

            if (id_tipodestinatario > 0)
            {
                dt = tipoDa.obtenerTipoDestinatario(id_tipodestinatario);
                TipoDesti.Text = Convert.ToString(dt.Rows[0]["nombreTipoDestinatario"]);
                tr_tipoDesti.Visible = true;
            };

            switch (Tipo.Value)
            {
                case "1":
                    TipoSolicitud.Text = " con requerimientos por vencer";
                    break;
                case "2":
                    TipoSolicitud.Text = " con requerimientos vencidos";
                    break;
               
            };
        }


        // EVENTOS GENERALES
        protected void ObtencionParametros()
        {
            // Se recibe el tipo de consulta
            try
            {
                if (Request.QueryString["tipo"] != null)
                {
                    Tipo.Value = Convert.ToString(Convert.ToInt32(Request.QueryString["tipo"]));
                }
                else
                {
                    Tipo.Value = "1";
                };
            }
            catch
            {
                Tipo.Value = "1";
            };

            // Se recibe el idSubRequerimiento
            try
            {
                if (Request.QueryString["idSubRequerimiento"] != null)
                {
                    IdSubRequerimiento.Value = Convert.ToString(Convert.ToInt32(Request.QueryString["idSubRequerimiento"]));
                }
                else
                {
                    Response.Redirect("~/Administrador/Reportes/plazosRequerimientos.aspx");
                };
            }
            catch
            {
                Response.Redirect("~/Administrador/Reportes/plazosRequerimientos.aspx");
            };
        }


        protected void Volver_Click(object sender, EventArgs e)
        {
            string path = "~/Administrador/Reportes/plazosRequerimientos.aspx";
            Hashtable HT_ModReportes = (Hashtable)Session["Modulo_Plazos"];
            if (HT_ModReportes != null)
            {
                Hashtable HT_ListEstados = (Hashtable)HT_ModReportes["ListEstados"];
                if (HT_ListEstados != null)
                {
                    HT_ListEstados["locked"] = false;
                    HT_ModReportes["ListEstados"] = (Hashtable)HT_ListEstados;
                    Session["Modulo_Plazos"] = (Hashtable)HT_ModReportes;
                };
            };
            Response.Redirect(path);
        }
        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            CargaGrilla();

            GridView grilla = GridView1;
            grilla.AllowPaging = false;
            grilla.Columns.RemoveAt(grilla.Columns.Count - 1);
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export("detalleEstado.xls", grilla);
        }


        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            int id_subRequerimiento = Convert.ToInt32(IdSubRequerimiento.Value);
            int tipo = Convert.ToInt32(Tipo.Value);
            int id_region = -1;
            int id_provincia = -1;
            int id_comuna = -1;
            int id_tiposolicitud = -1;
            int id_tipodestinatario = -1;
            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

            // Descargamos los filtros de búsqueda del Hashtable
            Hashtable HT_ModReportes = new Hashtable();
            Hashtable HT_ListEstados = new Hashtable();
            Hashtable HT_ListSolicitudes = new Hashtable();
            try
            {
                HT_ModReportes = (Hashtable)Session["Modulo_Plazos"];
                HT_ListEstados = (Hashtable)HT_ModReportes["ListEstados"];
                id_region = (int)HT_ListEstados["id_region"];
                id_provincia = (int)HT_ListEstados["id_provincia"];
                id_comuna = (int)HT_ListEstados["id_comuna"];
                id_tiposolicitud = (int)HT_ListEstados["id_tiposolicitud"];
                id_tipodestinatario = (int)HT_ListEstados["id_tipodestinatario"];
            }
            catch
            { };
            HT_ListSolicitudes = (Hashtable)HT_ModReportes["ListSolicitudes"];
            string KeySort = (string)HT_ListSolicitudes["KeySort"];
            int pagina = (int)HT_ListSolicitudes["pagina"];

            // Iniciamos la query de búsqueda y cargamos la grilla
            DataTable dt = new DataTable();
            int num_registros = 0;

            int id_tiposolicitudAux = fnc.equivalenciaIdTipoUE(id_tiposolicitud);
            int idTipoTamite = fnc.equivalenciaIdTipoTramite(id_tiposolicitud);
            int idSubTipoTamite = fnc.equivalenciaIdSubTipoTramite(id_tiposolicitud);


            if (Tipo.Value.Equals("1")) 
            {
                dt = estadoService.Requerimientos_Por_Vencer_Ver_RB(id_subRequerimiento, id_region, id_provincia, id_comuna, id_tiposolicitudAux, usuario_logeado.id_usuario, Convert.ToInt32(IdSubRequerimiento.Value), idTipoTamite, idSubTipoTamite, id_tipodestinatario);
            }
            else if (Tipo.Value.Equals("2"))
            {
                dt = estadoService.Requerimientos_Vencidos_Ver_RB(id_subRequerimiento, id_region, id_provincia, id_comuna, id_tiposolicitudAux, usuario_logeado.id_usuario, Convert.ToInt32(IdSubRequerimiento.Value), idTipoTamite, idSubTipoTamite, id_tipodestinatario);
            }

            
            if (dt != null)
            {
                num_registros = dt.Rows.Count;
                if (num_registros > 0)
                {
                    if (num_registros == 1)
                    {
                        msgGrilla.Text = "Se ha encontrado 1 solicitud coincidente con el filtro de búsqueda indicado.";
                    }
                    else
                    {
                        msgGrilla.Text = "Se han encontrado " + num_registros + " solicitudes coincidentes con el filtro de búsqueda indicado.";
                    };
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Content_msgGrilla.Visible = true;
                    ExportarGrilla.Visible = true;
                }
                else
                {
                    msgGrilla.Text = "No se encontraron solicitudes coincidentes con el filtro de búsqueda indicado.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Content_msgGrilla.Visible = true;
                    ExportarGrilla.Visible = false;
                };

                // Se ordena el DataTable en el server de aplicación
                dt.DefaultView.Sort = KeySort;
                GridView1.PageIndex = pagina;
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                Response.Redirect("~/Administrador/Reportes/ResumenEstados.aspx");
            };
        }

        protected void GridView1_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            Hashtable HT_ModUsuarios = (Hashtable)Session["Modulo_Plazos"];
            Hashtable HT_ListSolicitudes = (Hashtable)HT_ModUsuarios["ListSolicitudes"];
            HT_ListSolicitudes["pagina"] = e.NewPageIndex;
            HT_ModUsuarios["ListSolicitudes"] = (Hashtable)HT_ListSolicitudes;
            Session["Modulo_Plazos"] = (Hashtable)HT_ModUsuarios;

            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
            CargaGrilla();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Detalle Solicitud
                ImageButton boton_detalle = (ImageButton)e.Row.FindControl("gVer");
                if (boton_detalle != null)
                {
                    boton_detalle.Visible = true;
                };
                

                // Histórico de cambios de estado
                ImageButton boton_historico = (ImageButton)e.Row.FindControl("gHistorial");
                if (boton_historico != null)
                {
                    boton_historico.Visible = true;
                };
                
            };
        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Content_msgGrilla.Visible = false;

            switch (e.CommandName)
            {
                case "Ver":
                    Response.Redirect("~/Administrador/Reportes/detalleSolicitud.aspx?id_solicitud=" + e.CommandArgument + "&bp=9");
                    break;
                case "Historial":
                    Response.Redirect("~/Administrador/Reportes/logEstadosSolicitud.aspx?id_solicitud=" + e.CommandArgument + "&bp=9");
                    break;
            };
        }


        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            Hashtable HT_ModReportes = (Hashtable)Session["Modulo_Plazos"];
            Hashtable HT_ListSolicitudes = (Hashtable)HT_ModReportes["ListSolicitudes"];
            string KeySort = (string)HT_ListSolicitudes["KeySort"];
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

            HT_ListSolicitudes["KeySort"] = KeySort;
            HT_ListSolicitudes["pagina"] = 0;
            HT_ModReportes["ListSolicitudes"] = (Hashtable)HT_ListSolicitudes;
            Session["Modulo_Plazos"] = (Hashtable)HT_ModReportes;

            CargaGrilla();
        }
    }
}
