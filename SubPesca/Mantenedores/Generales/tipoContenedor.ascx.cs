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
using Datos.Utilidades;

namespace SubPesca.Mantenedores.Generales
{
    public partial class tipoContenedor1 : System.Web.UI.UserControl
    {
        Datos.Entidades.Usuario usuarios = new Datos.Entidades.Usuario();
        MantenedorGeneralService mantenedorGeneralService = new MantenedorGeneralService();

        Funciones funciones = new Funciones();


        void Page_PreInit(object sender, EventArgs e)
        {
            setearPageMaster();
        }

        protected void setearPageMaster()
        {

            int backpage = 0;
            try
            {
                if (Request.QueryString["bp"] != null)
                {
                    backpage = Convert.ToInt32(Request.QueryString["bp"]);
                };
            }
            catch
            {
            };

            switch (backpage)
            {
                case 1:
                    Page.MasterPageFile = "~/Mantenedores/SitioMantenedorGeneral.Master";
                    break;
                case 2:
                    Page.MasterPageFile = "~/Mantenedores/SitioMantenedorTransversal.Master";
                    break;
            };
        }

        protected void setearModulo()
        {
            if (funciones.retornaPagina().Contains("anio"))
            {
                ViewState["CLAVE"] = "ESTRUCTURAS_POR_AÑO";
                ViewState["TIPO"] = "Estructuras por Año";
            }
            else if (funciones.retornaPagina().Contains("rangoPesoEjemplar"))
            {
                ViewState["CLAVE"] = "RANGO_PESO_EJEMPLARES";
                ViewState["TIPO"] = "Rango Peso Ejemplar";
            }
            else if (funciones.retornaPagina().Contains("tipoAlimento"))
            {
                ViewState["CLAVE"] = "TIPO_ALIMENTO_PT";
                ViewState["TIPO"] = "Tipo Alimento";
            }
            else if (funciones.retornaPagina().Contains("tipoArchivoTitular"))
            {
                ViewState["CLAVE"] = "TIPO_ARCHIVO_TITULARES";
                ViewState["TIPO"] = "Tipo de Archivo Titular";
            }
            else if (funciones.retornaPagina().Contains("tipoBarrio"))
            {
                ViewState["CLAVE"] = "TIPO_BARRIO";
                ViewState["TIPO"] = "Tipo de Barrio";
            }
            else if (funciones.retornaPagina().Contains("tipoContacto"))
            {
                ViewState["CLAVE"] = "TIPO_CONTACTO";
                ViewState["TIPO"] = "Tipo Contacto";
            }
            else if (funciones.retornaPagina().Contains("tipoCultivo"))
            {
                ViewState["CLAVE"] = "TIPO_CULTIVO";
                ViewState["TIPO"] = "Tipo de Cultivo";
            }
            else if (funciones.retornaPagina().Contains("tipoUso"))
            {
                ViewState["CLAVE"] = "TIPO_USO";
                ViewState["TIPO"] = "Tipo de Uso";
            }
            else if (funciones.retornaPagina().Contains("tipoVertice"))
            {
                ViewState["CLAVE"] = "TIPO_VERTICE";
                ViewState["TIPO"] = "Tipo de Vértice";
            }
            else if (funciones.retornaPagina().Contains("unidadEjemplar"))
            {
                ViewState["CLAVE"] = "UNID_EJEMPLARES";
                ViewState["TIPO"] = "Unidad Medida Ejemplares";
            }
            else if (funciones.retornaPagina().Contains("volumenUnidadMedida"))
            {
                ViewState["CLAVE"] = "VOLUMEN_UNID_MEDIDA";
                ViewState["TIPO"] = "Volumen Unidad de Medida";
            }
            else if (funciones.retornaPagina().Contains("tipoDocumento"))
            {
                ViewState["CLAVE"] = "TIPO_DOCUMENTO";
                ViewState["TIPO"] = "Tipo de Documento";
            }
            else if (funciones.retornaPagina().Contains("tipoFondo"))
            {
                ViewState["CLAVE"] = "TIPO_FONDO_PROY";
                ViewState["TIPO"] = "Tipo Fondo";
            }
            else if (funciones.retornaPagina().Contains("metodoCultivo"))
            {
                ViewState["CLAVE"] = "MET_CULTIVO_ALGAS";
                ViewState["TIPO"] = "Método Cultivo de Algas";
            }
            else if (funciones.retornaPagina().Contains("tipoCentroAcopio"))
            {
                ViewState["CLAVE"] = "TIPO_CENTRO_ACOPIO";
                ViewState["TIPO"] = "Tipo Centro Acopio";
            }
            else if (funciones.retornaPagina().Contains("tipoCentroFaenamiento"))
            {
                ViewState["CLAVE"] = "TIPO_CENTRO_FAENAMIENTO";
                ViewState["TIPO"] = "Tipo Centro Faenamiento";
            }
            else if (funciones.retornaPagina().Contains("tipoCuerpoAgua"))
            {
                ViewState["CLAVE"] = "TIPO_CUERPO_AGUA";
                ViewState["TIPO"] = "Tipo Cuerpo Agua";
            }
            else if (funciones.retornaPagina().Contains("plazoNominal"))
            {
                ViewState["CLAVE"] = "TIPO_PLAZO";
                ViewState["TIPO"] = "Tipo Plazo Nominal";
            }
            else if (funciones.retornaPagina().Contains("tipoSupeditado"))
            {
                ViewState["CLAVE"] = "SUPEDITADO_TIPO";
                ViewState["TIPO"] = "Tipo Supeditado";
            }
        }

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
                setearModulo();

                TipoGeneral.Text = ViewState["TIPO"].ToString();
                TipoFormulario.Text = ViewState["TIPO"].ToString();
                TipoCampo.Text = ViewState["TIPO"].ToString();
                RequiredFieldValidatorNombre.ErrorMessage = TipoCampo.Text;

                // Validamos los accesos al listado y a sus objetos
                Content_Panel.Visible = true;

                // Cargamos la grilla
                CargaGrilla();
            };
        }

        // ACCIONES DE BOTONES
        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            ParametroGenerico parametroGenerico = new ParametroGenerico();
            parametroGenerico.descripcion = nombre.Text.Trim(); //se agrega trim para impedir los espacios en blanco.
            parametroGenerico.clave = ViewState["CLAVE"].ToString();

            if (parametroGenerico != null)
            {
                DataTable dt = mantenedorGeneralService.guardarTipo(parametroGenerico);

                try
                {
                    string msg = Convert.ToString(dt.Rows[0]["msg"]);
                    if (msg == "OK")
                    {
                        mensaje = "El tipo '" + parametroGenerico.descripcion + "' ha sido creado.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        GridView1.EditIndex = -1;
                        GridView1.PageIndex = 0;
                        nombre.Text = "";
                        CargaGrilla();
                    }
                    else
                    {
                        mensaje = "El nombre del tipo '" + parametroGenerico.descripcion + "' ya existe.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    };
                }
                catch
                {
                    mensaje = "Se ha producido un error al intentar crear el tipo.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
            }
            else
            {
                mensaje = "Para agregar debe ingresar los datos del tipo.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            // Iniciamos la query de búsqueda y cargamos la grilla
            ParametroGenerico parametroGenericoFiltro = new ParametroGenerico();
            parametroGenericoFiltro.clave = ViewState["CLAVE"].ToString();
            parametroGenericoFiltro.descripcion = nombre.Text;
            
            List<ParametroGenerico> dt = mantenedorGeneralService.listarParametroGenerico(parametroGenericoFiltro);

            int num_registros = 0;
            num_registros = dt.Count;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            if (dt != null && dt.Count > 0)
            {
                ExportarGrilla.Visible = true;
            }
            else
            {
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
                // Modificar
                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_modificar != null)
                {
                    boton_modificar.Visible = true;
                };

                // Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar el tipo " + DataBinder.Eval(e.Row.DataItem, "descripcion") + "?')");
                    boton_eliminar.Visible = true;
                };
            };
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Content_msgGrilla.Visible = false;

            int id = 0;
            switch (e.CommandName)
            {
                case "Eliminar":
                    id = Convert.ToInt32(e.CommandArgument);
                    Delete(id);
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
            TextBox nombre = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geNombre");
            
            if (nombre.Text != "")
            {
                id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                Agregar.Enabled = true;
                Update(id, nombre.Text);
            }
            else
            {
                Content_msgGrilla.Visible = true;
                msgGrilla.Text = "Para modificar debe ingresar un nombre para tipo.";
            };
            CargaGrilla();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Agregar.Enabled = true;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void Update(int id, string nombre)
        {
            ParametroGenerico parametroGenerico = new ParametroGenerico();
            parametroGenerico.id = id;
            parametroGenerico.descripcion = nombre.Trim();
            parametroGenerico.clave = ViewState["CLAVE"].ToString();

            DataTable dt = mantenedorGeneralService.actualizarTipo(parametroGenerico);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);
                if (msg == "OK")
                {
                    mensaje = "El tipo con ID:" + id + " ha sido actualizado.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    GridView1.EditIndex = -1;
                }
                else
                {
                    mensaje = "El nombre del tipo '" + nombre + "' ya existe.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar actualizar el tipo.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void Delete(int id)
        {
            DataTable dt = mantenedorGeneralService.eliminarTipo(id);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);

                switch (msg)
                {
                    case "OK":
                        mensaje = "El tipo ha sido eliminado.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        break;
                    case "En uso":
                        mensaje = "El tipo que intenta borrar está actualmente en uso.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                    default:
                        mensaje = "Se ha producido un error al intentar eliminar el tipo.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar el tipo.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();
            string nom_grilla = ViewState["TIPO"].ToString();
            string ngrilla = "";

            CargaGrilla();

            GridView1.Columns.RemoveAt(2);
            grilla = GridView1;
            ngrilla = ViewState["TIPO"].ToString().Replace(" ","")+".xls";

            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            ParametroGenerico parametroGenerico = new ParametroGenerico();
            parametroGenerico.descripcion = nombre.Text;
            parametroGenerico.clave = ViewState["CLAVE"].ToString();

            if (parametroGenerico != null)
            {
                List<ParametroGenerico> dt = mantenedorGeneralService.listarParametroGenerico(parametroGenerico);

                GridView1.DataSource = dt;
                GridView1.DataBind();

                if (dt != null && dt.Count > 0)
                {
                    ExportarGrilla.Visible = true;
                }
                else
                {
                    ExportarGrilla.Visible = false;
                }

                Content_msgGrilla.Visible = false;
                msgGrilla.Text = "";
                upd2.Update();
            }
        }
    }
}