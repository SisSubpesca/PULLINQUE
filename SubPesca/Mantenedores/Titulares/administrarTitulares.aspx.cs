using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.mantenedores;
using Validaciones.cl.subpesca.rb.mantenedor;
using SubPesca.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;

namespace SubPesca.Mantenedores.Titulares
{
    public partial class administrarTitulares : System.Web.UI.Page
    {

        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        SolicitanteDA solicitanteDA = new SolicitanteDA();
        MantenedorTitularService mantenedorTitularService = new MantenedorTitularService();
        MantenedorTitularesValidacion mantenedorTitularesValidacion = new MantenedorTitularesValidacion();
        PermisosService permisosService = new PermisosService();





        protected void setearModulo()
        {
            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_TITULARES };
        }

        protected void Page_Load(object sender, EventArgs e)
        {
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
                msgGrilla.Text = "Se ha guardado exitosamente el Titular en el sistema.";
                Ico_msgGrillaGral.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                Content_msgGrillaGral_1.Visible = true;
                UpdatePanelMgs.Update();
            }
            else if (ViewState["acc"] != null && ViewState["acc"].ToString().Equals("2")) //Mensaje para la modificación exitosa
            {
                msgGrilla.Text = "Se ha modificado exitosamente el Titular en el sistema.";
                Ico_msgGrillaGral.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                Content_msgGrillaGral_1.Visible = true;
                UpdatePanelMgs.Update();
            }
        }

        protected void GridTitulares_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridRepresentante = (GridView)sender;

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
                GridTitulares.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        protected void GridTitulares_RowDataBound(object sender, GridViewRowEventArgs e)
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

                int rut = Convert.ToInt32(GridTitulares.DataKeys[e.Row.RowIndex].Value);
                Solicitante solicitante = solicitanteDA.ObtenerPersona(rut,0);

                if (solicitante != null && solicitante.estadoPersona != null)
                {
                    if (solicitante.estadoPersona.id == rbEstadosGenerales.VIGENTE)
                    {
                        //Cambiar el Titular a No Vigente
                        ImageButton boton_noVigente = (ImageButton)e.Row.FindControl("gNoVigente");
                        if (boton_noVigente != null)
                        {
                            if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.ASOCIAR))
                            {
                                boton_noVigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar el estado del Titular?')");
                                boton_noVigente.Visible = true;
                            }

                        }
                    }
                    else if (solicitante.estadoPersona.id == rbEstadosGenerales.NO_VIGENTE)
                    {
                        //Cambiar el Titular a Vigente
                        ImageButton boton_vigente = (ImageButton)e.Row.FindControl("gVigente");
                        if (boton_vigente != null)
                        {
                            if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.DESASOCIAR))
                            {
                                boton_vigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar el estado del Titular?')");
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
                        boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar el Titular?')");
                        boton_borrar.Visible = true;
                    }

                }

            }
        }

        protected void GridTitulares_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int rutPersona = Convert.ToInt32(e.CommandArgument);
            string path = "";

            switch (e.CommandName)
            {

                case "Ver":
                    path = "~/Mantenedores/Titulares/detalleTitular.aspx?rutPersona=" + rutPersona + "&bp=4";
                    Response.Redirect(path);
                    break;

                case "Modificar":
                    path = "~/Mantenedores/Titulares/agregarTitular.aspx?rutPersona=" + rutPersona + "&acc=2";
                    Response.Redirect(path);
                    break;

                case "NoVigente":
                    cambiarEstadoTitular(rutPersona, false);
                    CargaGrilla();
                    break;

                case "Vigente":
                    cambiarEstadoTitular(rutPersona, true);
                    CargaGrilla();
                    break;

                case "Eliminar":
                    eliminarTitular(rutPersona);
                    //CargaGrilla();
                    break;
            }
        }

        private void eliminarTitular(int rutPersona)
        {
            
            Solicitante solicitante = new Solicitante();
            solicitante.rut = rutPersona;
            solicitante.accion = accion.ELIMINAR;

            List<String> listErroresSolicitante = mantenedorTitularesValidacion.validaTitular(solicitante);

            if (listErroresSolicitante.Count <= 0)
            {
                /* Aqui se elimina el Titular */
                bool resp = mantenedorTitularService.eliminaTitular(rutPersona);
                if (resp)
                {
                    msgGrilla.Text = "Se ha eliminado exitosamente el Titular.";
                    Ico_msgGrillaGral.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Content_msgGrillaGral_1.Visible = true;
                    UpdatePanelMgs.Update();

                }
                else
                {
                    msgGrilla.Text = "No se ha eliminado el Titular.";
                    Ico_msgGrillaGral.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Content_msgGrillaGral_1.Visible = true;
                    UpdatePanelMgs.Update();

                }
            }
            else {
                foreach (String error in listErroresSolicitante)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
                UpdatePanelMgs.Update();
            }
        }

        private void cambiarEstadoTitular(int rutPersona, bool estado)
        {
            
            Solicitante solicitante = new Solicitante();
            solicitante.rut = rutPersona;
            solicitante.estadoPersona = new ParametroGenerico();
            
            if (estado)
            {
                solicitante.estadoPersona.id = rbEstadosGenerales.VIGENTE;
            }
            else {
                solicitante.estadoPersona.id = rbEstadosGenerales.NO_VIGENTE;

            }

            List<String> listErroresSolicitante = mantenedorTitularesValidacion.validaCambiarEstadoTitular(solicitante);

            if (listErroresSolicitante.Count <= 0)
            {
                /* Aquí se realiza el cambio de estado del titular */
                bool resp = mantenedorTitularService.cambiarEstadoTitular(solicitante);
                if (resp)
                {
                    msgGrilla.Text = "Se ha cambiado el estado exitosamente del Titular.";
                    Ico_msgGrillaGral.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Content_msgGrillaGral_1.Visible = true;
                }
                else
                {
                    msgGrilla.Text = "No se ha cambiado el estado del Titular.";
                    Ico_msgGrillaGral.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Content_msgGrillaGral_1.Visible = true;

                }
                UpdatePanelMgs.Update();
            }
            else
            {
                foreach (String error in listErroresSolicitante)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
                UpdatePanelMgs.Update();
            }
        }

        protected void Limpiar_Click(object sender, EventArgs e)
        {
            //RutTitular.Text = "";
            //NombreTitular.Text = "";
           
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            FiltrarGrilla();
            CargaGrilla();
        }

        private void FiltrarGrilla()
        {
            Solicitante solicitanteFiltro = new Solicitante();
            if (RutTitular.Text != null && !RutTitular.Text.Equals(""))
            {
                String rutCompleto = Convert.ToString(RutTitular.Text);
                String[] rutPartes = rutCompleto.Split('-');

                solicitanteFiltro.tipoPersona = new ParametroGenerico();
                solicitanteFiltro.rut = Convert.ToInt32(rutPartes[0]);
                solicitanteFiltro.dv = Convert.ToChar(rutPartes[1]);
            }

            if (NombreTitular.Text != null && !NombreTitular.Text.Equals(""))
            {
                solicitanteFiltro.nombreSolicitante = NombreTitular.Text;
            }

            Session["solicitanteFiltro"] = (Solicitante) solicitanteFiltro;

        }

        protected void CargaGrilla()
        {
            int pagina = 0;

            List<Solicitante> respSolicitantes = new List<Solicitante>();
            Solicitante solicitanteFiltro = new Solicitante();

            try
            {
                solicitanteFiltro = (Solicitante)Session["solicitanteFiltro"];
                pagina = solicitanteFiltro.pagina;
            }
            catch { };

            if (solicitanteFiltro == null)
            {
                solicitanteFiltro = new Solicitante();
                Session["solicitanteFiltro"] = solicitanteFiltro;
            }

            //Aquí se debe hacer la consulta de los titulares registrados en el sistema
            respSolicitantes = solicitanteDA.ListarTitularFiltro(solicitanteFiltro.rut, solicitanteFiltro.nombreSolicitante);

            if (respSolicitantes == null || respSolicitantes.Count <= 0)
            {
                msgGrilla.Text = "No se han registrado titulares.";
                Ico_msgGrillaGral.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                Content_msgGrillaGral_1.Visible = true;

                respSolicitantes = new List<Solicitante>();
                GridTitulares.DataSource = respSolicitantes;
                GridTitulares.DataBind();
            }
            else
            {
                Content_msgGrillaGral_1.Visible = false;

                GridTitulares.PageIndex = pagina;
                GridTitulares.DataSource = respSolicitantes;
                GridTitulares.DataBind();
            }

            UpdatePanelMgs.Update();
        }

        protected void Crear_Click(object sender, EventArgs e)
        {
            string path = "~/Mantenedores/Titulares/agregarTitular.aspx?acc=1&ini=1";
            Response.Redirect(path);
        }

        protected void GridTitulares_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            Solicitante solicitanteFiltro = new Solicitante();
            solicitanteFiltro = (Solicitante)Session["solicitanteFiltro"];
            if (solicitanteFiltro == null)
            {
                solicitanteFiltro = new Solicitante();
            }

            solicitanteFiltro.pagina = e.NewPageIndex;
            Session["solicitanteFiltro"] = solicitanteFiltro;

            
            GridTitulares.PageIndex = e.NewPageIndex;
            GridTitulares.DataBind();
            CargaGrilla();
        }
    }
}