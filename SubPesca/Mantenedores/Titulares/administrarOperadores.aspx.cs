using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.mantenedores;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using SubPesca.Utilidades;
using Validaciones.cl.subpesca.rb.mantenedor;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;

namespace SubPesca.Mantenedores.Titulares
{
    public partial class administrarOperadores : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        OperadorDA operadorDA = new OperadorDA();
        MantenedorTitularService mantenedorTitularService = new MantenedorTitularService();
        MantenedorTitularesValidacion mantenedorTitularesValidacion = new MantenedorTitularesValidacion();
        PermisosService permisosService = new PermisosService();


        protected void setearModulo()
        {
            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_OPERADORES };
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // PAGE LOAD
            if (!Page.IsPostBack)
            {
                setearModulo();
                
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (usuario_logeado == null)
                {
                    Response.Redirect("~/ingreso.aspx");
                }

                //FORMULARIO DE INGRESO DE TITULARES
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], this.usuario_logeado, null, rbAccion.EDITAR))
                {
                    Crear.Visible = true;
                }
                else
                {
                    Crear.Visible = false;
                }


                ObtenerParametros();

                InicializarFormulario();
            }
        }

        private void ObtenerParametros()
        {
            try
            {
                if (Request.QueryString["acc"] != null)
                {
                    ViewState["acc"] = Request.QueryString["acc"];
                }
            }
            catch
            {

            };
        }

        private void InicializarFormulario()
        {
            if (ViewState["acc"] != null && ViewState["acc"].ToString().Equals("1")) //Mensaje para la creación exitosa
            {
                msgGrilla.Text = "Se ha guardado exitosamente el Operadpr en el sistema.";
                Ico_msgGrillaGral.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                Panel1.Visible = true;
                UpdatePanelMgs.Update();
            }
            else if (ViewState["acc"] != null && ViewState["acc"].ToString().Equals("2")) //Mensaje para la modificación exitosa
            {
                msgGrilla.Text = "Se ha modificado exitosamente el Operador en el sistema.";
                Ico_msgGrillaGral.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                Panel1.Visible = true;
                UpdatePanelMgs.Update();
            }
        }

        protected void Limpiar_Click(object sender, EventArgs e)
        {
            RutPersona.Text = "";
            NombrePersona.Text = "";
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            FiltrarGrilla();
            CargaGrilla();
        }

        private void FiltrarGrilla()
        {
            Operador operadorFiltro = new Operador();
            operadorFiltro.operador = new Solicitante();

            if (RutPersona.Text != null && !RutPersona.Text.Equals(""))
            {
                String rutCompleto = Convert.ToString(RutPersona.Text);
                String[] rutPartes = rutCompleto.Split('-');

                operadorFiltro.operador.tipoPersona = new ParametroGenerico();
                operadorFiltro.operador.rut = Convert.ToInt32(rutPartes[0]);
                operadorFiltro.operador.dv = Convert.ToChar(rutPartes[1]);
            }

            if (NombrePersona.Text != null && !NombrePersona.Text.Equals(""))
            {
                operadorFiltro.operador.nombreSolicitante = NombrePersona.Text;
            }

            Session["operadorFiltro"] = (Operador) operadorFiltro;
        }

        protected void GridOperadores_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridContacto = (GridView)sender;

                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Listado de Resultados";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridContacto.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        protected void GridOperadores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //Ver
                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    boton_ver.Visible = true;

                }

                //Modificar
                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_modificar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
                    {
                        boton_modificar.Visible = true;
                    }

                }

                int rut = Convert.ToInt32(GridOperadores.DataKeys[e.Row.RowIndex].Value);
                Operador operador = operadorDA.ObtenerOperador(rut,null);

                if (operador != null && operador.operador != null && operador.operador.estadoPersona != null)
                {
                    if (operador.operador.estadoPersona.id == rbEstadosGenerales.VIGENTE)
                    {
                        //Cambiar el Operador a No Vigente
                        ImageButton boton_noVigente = (ImageButton)e.Row.FindControl("gNoVigente");
                        if (boton_noVigente != null)
                        {
                            if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.ASOCIAR))
                            {                                
                                boton_noVigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar el estado del Operador?')");
                                boton_noVigente.Visible = true;
                            }

                        }
                    }
                    else if (operador.operador.estadoPersona.id == rbEstadosGenerales.NO_VIGENTE)
                    {
                        //Cambiar el Operador a Vigente
                        ImageButton boton_vigente = (ImageButton)e.Row.FindControl("gVigente");
                        if (boton_vigente != null)
                        {
                            if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.DESASOCIAR))
                            {
                                boton_vigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar el estado del Operador?')");
                                boton_vigente.Visible = true;
                            }
                        }
                    }
                }

                //Eliminar
                ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
                if (boton_borrar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.ELIMINAR))
                    {
                        boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar eliminar el Operador?')");
                        boton_borrar.Visible = true;
                    }
                }

            }
        }

        protected void GridOperadores_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int rutPersona = Convert.ToInt32(e.CommandArgument);
            string path = "";

            switch (e.CommandName)
            {

                case "Ver":
                    path = "~/Mantenedores/Titulares/verOperador.aspx?rutPersona=" + rutPersona;
                    Response.Redirect(path);
                    break;

                case "Modificar":
                    path = "~/Mantenedores/Titulares/agregarOperador.aspx?rutPersona=" + rutPersona + "&acc=2";
                    Response.Redirect(path);
                    break;

                case "NoVigente":
                    cambiarEstadoOperador(rutPersona, false);
                    CargaGrilla();
                    break;

                case "Vigente":
                    cambiarEstadoOperador(rutPersona, true);
                    CargaGrilla();
                    break;

                case "Eliminar":
                    eliminarOperador(rutPersona);
                    CargaGrilla();
                    break;
            }
        }

        private void CargaGrilla()
        {
            int pagina = 0;

            List<Operador> respOperador = new List<Operador>();
            Operador operadorFiltro = new Operador();
            
            try
            {
                operadorFiltro = (Operador)Session["operadorFiltro"];
                pagina = operadorFiltro.pagina;
            }
            catch { };

            if (operadorFiltro == null)
            {
                operadorFiltro = new Operador();
                Session["operadorFiltro"] = operadorFiltro;
            }

            //Aquí se debe hacer la consulta de los operadores registrados en el sistema
            respOperador = operadorDA.ListarOperador(operadorFiltro.operador.rut, operadorFiltro.operador.nombreSolicitante);

            if (respOperador == null || respOperador.Count <= 0)
            {
                msgGrilla.Text = "No se han registrado operadores.";
                Ico_msgGrillaGral.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                Panel1.Visible = true;

                respOperador = new List<Operador>();
                GridOperadores.DataSource = respOperador;
                GridOperadores.DataBind();
            }
            else
            {
                Panel1.Visible = false;

                GridOperadores.PageIndex = pagina;
                GridOperadores.DataSource = respOperador;
                GridOperadores.DataBind();
            }

            UpdatePanelMgs.Update();
        }

        private void cambiarEstadoOperador(int rutPersona, bool estado)
        {
            Operador operador = new Operador();
            operador.operador = new Solicitante();
            operador.operador.rut = rutPersona;
            operador.operador.estadoPersona = new ParametroGenerico();

            if (estado)
            {
                operador.operador.estadoPersona.id = rbEstadosGenerales.VIGENTE;
            }
            else {
                operador.operador.estadoPersona.id = rbEstadosGenerales.NO_VIGENTE;
            }

            List<String> listErroresOperador = mantenedorTitularesValidacion.validaCambiarEstadoOperador(operador);

            if (listErroresOperador.Count <= 0)
            {
                /* Aqui se cambia de estado el Operador */
                bool resp = mantenedorTitularService.cambiarEstadoOperador(operador);
                if (resp)
                {
                    msgGrilla.Text = "Se ha cambiado exitosamente el estado al Operador.";
                    Ico_msgGrillaGral.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Panel1.Visible = true;
                }
                else
                {
                    msgGrilla.Text = "No se ha cambiado el estado al Operador.";
                    Ico_msgGrillaGral.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Panel1.Visible = true;

                }
                UpdatePanelMgs.Update();
            }
            else
            {
                foreach (String error in listErroresOperador)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
                UpdatePanelMgs.Update();
            }
        }

        private void eliminarOperador(int rutPersona)
        {
            Operador operador = new Operador();
            operador.operador = new Solicitante();
            operador.operador.rut = rutPersona;
            operador.operador.accion = accion.ELIMINAR;

            List<String> listErroresOperador = mantenedorTitularesValidacion.validaOperador(operador);

            if (listErroresOperador.Count <= 0)
            {
                /* Aqui se elimina el Operador */
                bool resp = mantenedorTitularService.eliminaOperador(operador.operador.rut);
                if (resp)
                {
                    msgGrilla.Text = "Se ha eliminado exitosamente el Operador.";
                    Ico_msgGrillaGral.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Panel1.Visible = true;
                }
                else
                {
                    msgGrilla.Text = "No se ha eliminado el Operador.";
                    Ico_msgGrillaGral.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Panel1.Visible = true;

                }
                UpdatePanelMgs.Update();
            }
            else
            {
                foreach (String error in listErroresOperador)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
                UpdatePanelMgs.Update();
            }
            
        }
        protected void GridOperadores_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            Operador operadorFiltro = new Operador();
            operadorFiltro = (Operador)Session["operadorFiltro"];
            if (operadorFiltro == null)
            {
                operadorFiltro = new Operador();
            }

            operadorFiltro.pagina = e.NewPageIndex;
            Session["operadorFiltro"] = operadorFiltro;
            
            GridOperadores.PageIndex = e.NewPageIndex;
            GridOperadores.DataBind();
            CargaGrilla();
        }

        protected void Crear_Click(object sender, EventArgs e)
        {
            string path = "~/Mantenedores/Titulares/agregarOperador.aspx?acc=1&ini=1";
            Response.Redirect(path);
        }
    }
}