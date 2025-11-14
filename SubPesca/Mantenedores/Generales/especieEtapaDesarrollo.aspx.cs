using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Datos.Entidades;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.servicios.mantenedores;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.solicitud;
namespace SubPesca.Mantenedores.Generales
{
    public partial class especieEtapaDesarrollo : System.Web.UI.Page
    {
        Datos.Entidades.Usuario usuarios = new Datos.Entidades.Usuario();
        MantenedorGeneralService mantenedorGeneralService = new MantenedorGeneralService();
        MantenedorDA mantenedorDA = new MantenedorDA();

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
            scriptInclude = (HtmlGenericControl)Page.Header.FindControl("jquery.autoheight.js");
            if (scriptInclude == null)
            {
                scriptInclude = new HtmlGenericControl("script");
                scriptInclude.Attributes["type"] = "text/javascript";
                scriptInclude.Attributes["src"] = ResolveClientUrl("~/js/jquery/jquery.autoheight.js");
                scriptInclude.ID = "jquery.autoheight.js";
                Page.Header.Controls.Add(scriptInclude);
            };
        }

        // PAGE_LOAD
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Validamos los accesos al listado y a sus objetos
                Content_Panel.Visible = true;

                // cargamos los combobox
                CargarCombobox("Especie");

                CargarCombobox("EtapaDesarrollo");

                // Cargamos la grilla
                CargaGrilla();
            };
        }

        private void CargarCombobox(string combobox)
        {
            switch (combobox)
            {
                case "Especie":
                    // Cargamos el combobox: Especie
                    Especie.Items.Clear();

                    Especies especieFiltro = new Especies();
                    especieFiltro.esExotica = -1;
                    especieFiltro.esExperimental = -1;

                    Especie.DataSource = mantenedorDA.ListarEspecieCultivo_Mantenedor(especieFiltro);
                    Especie.DataTextField = "especieNombreComun";
                    Especie.DataValueField = "id_especie";
                    Especie.DataBind();
                    Especie.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "EtapaDesarrollo":
                    // Cargamos el combobox: EtapaDesarrollo
                    EtapaDesarrollo.Items.Clear();
                    EtapaDesarrollo.DataSource = mantenedorDA.ListarEtapaCultivo_Mantenedor(new EtapaCultivo());
                    EtapaDesarrollo.DataTextField = "descripcion";
                    EtapaDesarrollo.DataValueField = "id";
                    EtapaDesarrollo.DataBind();
                    EtapaDesarrollo.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
            }
        }

        // ACCIONES DE BOTONES
        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            EtapaCultivo etapaCultivo = new EtapaCultivo();

            etapaCultivo.especie = new Especies();
            etapaCultivo.especie.id_especie = Convert.ToInt32(Especie.SelectedItem.Value);
            etapaCultivo.especie.especieNombreComun = Especie.SelectedItem.Text;

            etapaCultivo.codigo = Convert.ToInt32(EtapaDesarrollo.SelectedItem.Value);
            etapaCultivo.nombreEtapaDesarrollo = EtapaDesarrollo.SelectedItem.Text;
            

            if (etapaCultivo != null)
            {
                DataTable dt = mantenedorGeneralService.guardarEspecieEtapaDeDesarrollo(etapaCultivo);

                try
                {
                    string msg = Convert.ToString(dt.Rows[0]["msg"]);
                    if (msg == "OK")
                    {
                        mensaje = "La especie - etapa de cultivo '" + etapaCultivo.especie.especieNombreComun + " - " + etapaCultivo.nombreEtapaDesarrollo + "' ha sido creada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        GridView1.EditIndex = -1;
                        GridView1.PageIndex = 0;

                        EtapaDesarrollo.SelectedValue = "-1";
                        Especie.SelectedValue = "-1";

                        CargaGrilla();
                    }
                    else
                    {
                        mensaje = "La especie - etapa de cultivo '" + etapaCultivo.especie.especieNombreComun + " - " + etapaCultivo.nombreEtapaDesarrollo + "' ya existe.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    };
                }
                catch
                {
                    mensaje = "Se ha producido un error al intentar crear la especie - etapa de cultivo.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };

            }
            else
            {
                mensaje = "Para agregar debe ingresar los datos de la especie - etapa de cultivo.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            // Iniciamos la query de búsqueda y cargamos la grilla
            EtapaCultivo etapaCultivoFiltro = new EtapaCultivo();
            etapaCultivoFiltro.especie = new Especies();

            etapaCultivoFiltro.especie.id_especie = Convert.ToInt32(Especie.SelectedItem.Value);
            etapaCultivoFiltro.codigo = Convert.ToInt32(EtapaDesarrollo.SelectedItem.Value);
            
            List<EtapaCultivo> dt = mantenedorGeneralService.listarEspecieEtapaDesarrollo(etapaCultivoFiltro);

            int num_registros = 0;
            num_registros = dt.Count;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            if (dt != null && dt.Count > 0)
            {
                ExportarGrilla.Visible = true;
            }
            else {
                ExportarGrilla.Visible = false;
            }
        }

        protected void GridView1_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            Agregar.Enabled = true;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                /*
                // Modificar
                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_modificar != null)
                {
                    boton_modificar.Visible = true;
                };
                 * */

                // Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar la especie - etapa de cultivo seleccionada?')");
                    boton_eliminar.Visible = true;
                };
            };
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Content_msgGrilla.Visible = false;
            
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            switch (e.CommandName)
            {
                case "Eliminar":

                    int idEspecie = Convert.ToInt32(arg[0]);
                    int codigoEtapa = Convert.ToInt32(arg[1]);

                    Delete(idEspecie, codigoEtapa, 0);
                    GridView1.EditIndex = -1;
                    CargaGrilla();
                    break;
            };
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string orderby = KeySort.Value;
            int pos = orderby.IndexOf(e.SortExpression + " ASC");
            if (pos >= 0)
            {
                orderby = e.SortExpression + " DESC";
            }
            else
            {
                orderby = e.SortExpression + " ASC";
            };
            KeySort.Value = orderby;

            GridView1.PageIndex = 0;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Agregar.Enabled = false;
            GridView1.EditIndex = e.NewEditIndex;
            CargaGrilla();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = 0;

            int idEspecie = 0;
            int idEtapaDesarrollo = 0;
            TextBox nombre = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geNombre");




            if (nombre.Text != "")
            {
                id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                Agregar.Enabled = true;
                Update(idEspecie, idEtapaDesarrollo);
            }
            else
            {
                Content_msgGrilla.Visible = true;
                msgGrilla.Text = "Para modificar debe ingresar una asociación especie - etapa de cultivo";
            };
            CargaGrilla();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Agregar.Enabled = true;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void Update(int idEspecie, int idEtapaDesarrollo)
        {
            EtapaCultivo etapaCultivo = new EtapaCultivo();
            etapaCultivo.id_etapaDesarrollo = idEtapaDesarrollo;
            etapaCultivo.especie = new Especies();
            etapaCultivo.especie.id_especie = idEspecie;

            DataTable dt = mantenedorGeneralService.actualizarEspecieEtapaDesarrollo(etapaCultivo);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);
                if (msg == "OK")
                {
                    mensaje = "La especie - etapa de cultivo ha sido actualizada.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    GridView1.EditIndex = -1;
                }
                else
                {
                    mensaje = "La especie - etapa de cultivo ya existe.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar actualizar la especie - etapa de cultivo.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void Delete(int idEspecie, int codigoEtapa, int idGrupoEsp)
        {
            DataTable dt = mantenedorGeneralService.eliminarEspecieEtapaDesarrollo(idEspecie, codigoEtapa, idGrupoEsp);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);

                switch (msg)
                {
                    case "OK":
                        mensaje = "La especie - etapa de cultivo ha sido eliminada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        break;
                    case "En uso":
                        mensaje = "La especie - etapa de cultivo que intenta borrar está actualmente en uso.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                    default:
                        mensaje = "Se ha producido un error al intentar eliminar la especie - etapa de cultivo.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar la especie - etapa de cultivo.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void Volver_Click(object sender, EventArgs e)
        {
            string path = "~/Administrador/principal.aspx";
            Response.Redirect(path);
        }

        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();
            string nom_grilla = "especieEtapaCultivo";
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "especieEtapaCultivo":
                    GridView1.Columns.RemoveAt(2);
                    grilla = GridView1;
                    ngrilla = "especieEtapaCultivo.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            EtapaCultivo etapaCultivo = new EtapaCultivo();

            etapaCultivo.especie = new Especies();
            etapaCultivo.especie.id_especie = Convert.ToInt32(Especie.SelectedItem.Value);
            etapaCultivo.codigo = Convert.ToInt32(EtapaDesarrollo.SelectedItem.Value);
            
            if (etapaCultivo != null)
            {
                List<EtapaCultivo> dt = mantenedorGeneralService.listarEspecieEtapaDesarrollo(etapaCultivo);

                GridView1.DataSource = dt;
                GridView1.DataBind();

                if (dt != null && dt.Count > 0)
                {
                    ExportarGrilla.Visible = true;
                }
                else {
                    ExportarGrilla.Visible = false;
                }

                Content_msgGrilla.Visible = false;
                msgGrilla.Text = "";
                upd2.Update();
            }
        }
    }
}