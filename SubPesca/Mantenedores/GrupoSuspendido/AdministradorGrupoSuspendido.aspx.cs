using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using SubPesca.Mantenedores.Generales;
using Datos.Contantes;
using SubPesca.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;

namespace SubPesca.Mantenedores.GrupoSuspendido
{
    public partial class AdministradorGrupoSuspendido : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        MantenedorGeneralService mantenedorGeneralService = new MantenedorGeneralService();
        ParametroGenericoDA parametroDa = new ParametroGenericoDA();
        GrupoSuspendidoDA GrupoSusDa = new GrupoSuspendidoDA();
        PermisosService permisosService = new PermisosService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                /* Verificar si el usuario esta logeado o corresponde poder entrar a esta pagina */
                Datos.Entidades.Usuario.Serializable usuario_logeado = (Datos.Entidades.Usuario.Serializable)HttpContext.Current.Session["Usuario"];

                if (!permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.GrupoSuspendido }, usuario_logeado, null, rbAccion.EDITAR))
                {
                    Response.Redirect("~/Administrador/principal.aspx");
                }

                _CargarCombobox();
                _CargarGrilla("",0);
            }
        }
        /* boton de filtrado para la grilla */
        protected void _BuscarGrupoSuspendido(object sender, EventArgs e)
        {
            string NombreGrupo = "";
            int index = 0;
            if (NombreGrupoSus != null || !NombreGrupoSus.Equals(""))
            {
                NombreGrupo = NombreGrupoSus.Text;    
            }
            if (TipoGrupoSuspendido.SelectedIndex != 0)
            {
                index = Convert.ToInt32(TipoGrupoSuspendido.SelectedValue);
            }
            _CargarGrilla(NombreGrupo, index);

        }
        /* Boton para Redireccionar al crear grupo suspendido */
        protected void _NuevoGrupoSuspendido(object sender, EventArgs e)
        {
            Response.Redirect("../../Mantenedores/GrupoSuspendido/CrearGrupoSuspendido.aspx");
        }
        /* Metodo de opciones de los botones del grid */
        protected void _GridGrupoSuspendido_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string vigencia = ((Label)e.Row.FindControl("EstadoVigencia")).Text;

                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar?')");
                    boton_eliminar.Visible = true;
                };
                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_modificar != null)
                {
                    if (Convert.ToInt32(vigencia) != rbEstadosGenerales.GrupoSuspendidoCerrado && Convert.ToInt32(vigencia) != rbEstadosGenerales.NO_VIGENTE)
                    {
                        boton_modificar.Visible = true;
                    }
                };
                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    boton_ver.Visible = true;
                };
            }
        }
        /* Metodo de los comando de los botones */
        protected void _GridGrupoSuspendido_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = 0;
            switch (e.CommandName)
            {
                case "Eliminar":
                    id = Convert.ToInt32(e.CommandArgument);
                    GridGrupoSuspendido.EditIndex = -1;
                    _GridGrupoSuspendido_Eliminar(id);
                    break;

                case "Modificar":
                    id = Convert.ToInt32(e.CommandArgument);
                    _GridGrupoSuspendido_Modificar(id);
                    break;

                case "Ver":
                    id = Convert.ToInt32(e.CommandArgument);
                    _GridGrupoSuspendido_Ver(id);
                    break;
            };
        }

        protected void _GridGrupoSuspendido_Eliminar(int id)
        { 
            /* realizar eliminacion */
            //Pasar a no vigente el estado de idGrupoSuspendido
            bool confirmacion = false;
            

            confirmacion = mantenedorGeneralService.EliminarGrupoSuspendido(id); //dejar como no vigente

            //si la confirmacion es correcta, indicara que la dada de baja fue realizada con exito
            if (confirmacion)
            {
                _CargarCombobox();
                _CargarGrilla("", 0);
            }
            else
            {
                //error no se pudo generar la accion :(
                Page.Validators.Add(new ValidationError("grupo1", "Error, No se pudo realizar la accion"));
            }

        }

        protected void _GridGrupoSuspendido_Modificar(int id)
        { 
            /* Creamos una cookie donde se obtiene el id del GrupoSuspendido */
            //Application["id"] = id;
            Response.Redirect("../../Mantenedores/GrupoSuspendido/CrearGrupoSuspendido.aspx?id="+id);
        }

        protected void _GridGrupoSuspendido_Ver(int id)
        { 
            /* Listar todas las solicitudes dentro del Grupo suspendido */
            /* Para ello se envia una cookie a la vista VerGrupoSuspendido */
            //Application["id"] = id;
            Response.Redirect("../../Mantenedores/GrupoSuspendido/VerGrupoSuspendido.aspx?id="+id);
        }

        protected void GridGrupoSuspendido_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridGrupoSuspendido.PageIndex = e.NewPageIndex;
            GridGrupoSuspendido.DataBind();
            _CargarGrilla("",0);
        }

        /* Metodo para cargar datos en la grilla */
        protected void _CargarGrilla(string nombre, int indice)
        {
            List<GrupoSuspendidos> ListGrupoSuspendido = new List<GrupoSuspendidos>();
            string _nombre = "";
            int _indice = 0;

            if (!nombre.Equals("") && nombre != null)
            {
                //esto debe cambiar por un metodo para cargar los GrupoSuspendidos x Nombre
                _nombre = nombre;
            }
            if (indice != 0)
            {

                _indice = indice;
            }
            
                ListGrupoSuspendido = GrupoSusDa.ListarGrupoSuspendido(0, _indice, 0, _nombre);
            
            if (ListGrupoSuspendido.Count > 0)
            {
                GridGrupoSuspendido.DataSource = ListGrupoSuspendido;
                GridGrupoSuspendido.DataBind();
            }
            else
            { 
                //no hay data :(
                GridGrupoSuspendido.DataSource = ListGrupoSuspendido;
                GridGrupoSuspendido.DataBind();
            }
        }
        /* Metodo para cagar el combobox */
        protected void _CargarCombobox()
        { 
            /* Se carga el combobox con los datos de los tipos de GrupoSupendido */

            TipoGrupoSuspendido.Items.Clear();
            TipoGrupoSuspendido.DataBind();

            ParametroGenerico parametroGenericoFiltro = new ParametroGenerico();
            parametroGenericoFiltro.clave = "TIPO_AGRUPACION_SUSPEND";

            TipoGrupoSuspendido.DataSource = mantenedorGeneralService.listarParametroGenerico(parametroGenericoFiltro);
            TipoGrupoSuspendido.DataTextField = "descripcion";
            TipoGrupoSuspendido.DataValueField = "id";
            TipoGrupoSuspendido.DataBind();
            TipoGrupoSuspendido.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));


        }
        
    }
}