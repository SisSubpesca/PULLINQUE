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
    public partial class administrarRepresentantesLegales : System.Web.UI.Page
    {

        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        RepLegalDA repLegalDA = new RepLegalDA();
        MantenedorTitularService mantenedorTitularService = new MantenedorTitularService();
        MantenedorTitularesValidacion mantenedorTitularesValidacion = new MantenedorTitularesValidacion();
        PermisosService permisosService = new PermisosService();





        protected void setearModulo()
        {
            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_REPRESENTANTE_LEGAL };
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

                //FORMULARIO DE INGRESO DE REPRESENTANTE LEGAL
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
                msgGrilla.Text = "Se ha guardado exitosamente el Representante Legal en el sistema.";
                Ico_msgGrillaGral.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                PanelSolicitudesMsg.Visible = true;
                UpdatePanelMgs.Update();
            }
            else if (ViewState["acc"] != null && ViewState["acc"].ToString().Equals("2")) //Mensaje para la modificación exitosa
            {
                msgGrilla.Text = "Se ha modificado exitosamente el Representante Legal en el sistema.";
                Ico_msgGrillaGral.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                PanelSolicitudesMsg.Visible = true;
                UpdatePanelMgs.Update();
            }
        }

        protected void Limpiar_Click(object sender, EventArgs e)
        {
            RutPersona.Text = "";
            NombrePersona.Text = "";
        }

        protected void GridRepresentanteLegal_RowCreated(object sender, GridViewRowEventArgs e)
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

        protected void GridRepresentanteLegal_RowDataBound(object sender, GridViewRowEventArgs e)
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

                int rut = Convert.ToInt32(GridRepresentanteLegal.DataKeys[e.Row.RowIndex].Value);
                RepLegal repLegal = repLegalDA.ObtieneRepresentanteLegal(rut, null);

                if (repLegal!= null && repLegal.representanteLegal != null && repLegal.representanteLegal.estadoPersona != null)
                {

                    if (repLegal.representanteLegal.estadoPersona.id == rbEstadosGenerales.VIGENTE)
                    {
                        //Cambiar el Titular a No Vigente
                        ImageButton boton_noVigente = (ImageButton)e.Row.FindControl("gNoVigente");
                        if (boton_noVigente != null)
                        {
                            if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.ASOCIAR))
                            {
                                boton_noVigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar el estado del Representante Legal?')");
                                boton_noVigente.Visible = true;
                            }

                        }
                    }
                    else if (repLegal.representanteLegal.estadoPersona.id == rbEstadosGenerales.NO_VIGENTE)
                    {
                        //Cambiar el Titular a Vigente
                        ImageButton boton_vigente = (ImageButton)e.Row.FindControl("gVigente");
                        if (boton_vigente != null)
                        {
                            if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.DESASOCIAR))
                            {
                                boton_vigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar el estado del Representante Legal?')");
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
                        boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar el Representante Legal?')");
                        boton_borrar.Visible = true;
                    }
                }

            }
        }

        protected void GridRepresentanteLegal_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int rutPersona = Convert.ToInt32(e.CommandArgument);
            string path = "";
            
            switch (e.CommandName)
            {

                case "Ver":
                    path = "~/Mantenedores/Titulares/verRepresentanteLegal.aspx?rutPersona=" + rutPersona;
                    Response.Redirect(path);
                    break;

                case "Modificar":
                    path = "~/Mantenedores/Titulares/agregarRepresentanteLegal.aspx?rutPersona=" + rutPersona + "&acc=2";
                    Response.Redirect(path);
                    break;

                case "NoVigente":
                    cambiarEstadoRepresentanteLegal(rutPersona, false);
                    CargaGrilla();
                    break;

                case "Vigente":
                    cambiarEstadoRepresentanteLegal(rutPersona, true);
                    CargaGrilla();
                    break;

                case "Eliminar":
                    eliminarRepresentanteLegal(rutPersona);
                    CargaGrilla();
                    break;
            }
        }

        private void eliminarRepresentanteLegal(int rutPersona)
        {
            
            RepLegal repLegal = new RepLegal();
            repLegal.representanteLegal = new Solicitante();
            repLegal.representanteLegal.rut = rutPersona;
            repLegal.representanteLegal.accion = accion.ELIMINAR;

            List<String> listErroresRepresentante = mantenedorTitularesValidacion.validaRepresentanteLegal(repLegal);

            if (listErroresRepresentante.Count <= 0)
            {
                /* Aqui se elimina el Representante Legal */
                bool resp = mantenedorTitularService.eliminarRepresentanteLegal(rutPersona);
                if (resp)
                {
                    msgGrilla.Text = "Se ha eliminado exitosamente el Representante Legal.";
                    Ico_msgGrillaGral.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    PanelSolicitudesMsg.Visible = true;
                }
                else
                {
                    msgGrilla.Text = "No se ha eliminado el Representante Legal.";
                    Ico_msgGrillaGral.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    PanelSolicitudesMsg.Visible = true;

                }
                UpdatePanelMgs.Update();
            }
            else
            {
                foreach (String error in listErroresRepresentante)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }

                UpdatePanelMgs.Update();
            }
        }

        private void cambiarEstadoRepresentanteLegal(int rutPersona, bool estado)
        {
            
            RepLegal repLegal = new RepLegal();
            repLegal.representanteLegal = new Solicitante();
            repLegal.representanteLegal.rut = rutPersona;
            repLegal.representanteLegal.estadoPersona = new ParametroGenerico();

            if (estado)
            {
                repLegal.representanteLegal.estadoPersona.id = rbEstadosGenerales.VIGENTE;
            }
            else
            {
                repLegal.representanteLegal.estadoPersona.id = rbEstadosGenerales.NO_VIGENTE;

            }

            List<String> listErroresRepresentante = mantenedorTitularesValidacion.validaEstadoRepresentanteLegal(repLegal);

            if (listErroresRepresentante.Count <= 0)
            {
                /* Aqui se cambia de estado el Representante Legal */
                bool resp = mantenedorTitularService.cambiarEstadoRepresentante(repLegal);
                if (resp)
                {
                    msgGrilla.Text = "Se ha eliminado exitosamente el Representante Legal.";
                    Ico_msgGrillaGral.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    PanelSolicitudesMsg.Visible = true;
                }
                else
                {
                    msgGrilla.Text = "No se ha eliminado el Representante Legal.";
                    Ico_msgGrillaGral.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    PanelSolicitudesMsg.Visible = true;

                }
                UpdatePanelRepresentanteLegal.Update();
            }
            else
            {
                foreach (String error in listErroresRepresentante)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }

                UpdatePanelMgs.Update();
            }
        }

        private void CargaGrilla()
        {
            int pagina = 0;

            List<RepLegal> respRepLegal = new List<RepLegal>();
            RepLegal repLegalFiltro = new RepLegal();
                        
            try
            {
                repLegalFiltro = (RepLegal)Session["repLegalFiltro"];
                pagina = repLegalFiltro.pagina;
            }
            catch { };

            if (repLegalFiltro == null)
            {
                repLegalFiltro = new RepLegal();
                Session["repLegalFiltro"] = repLegalFiltro;
            }

            //Aquí se debe hacer la consulta de los representantes legales ingresados en el sistema
            respRepLegal = repLegalDA.ListarRepresentanteLegal(repLegalFiltro.representanteLegal.rut, repLegalFiltro.representanteLegal.nombreSolicitante);

            if (respRepLegal == null || respRepLegal.Count <= 0)
            {
                msgGrilla.Text = "No se han registrado Representantes Legales.";
                Ico_msgGrillaGral.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                PanelSolicitudesMsg.Visible = true;

                respRepLegal = new List<RepLegal>();
                GridRepresentanteLegal.DataSource = respRepLegal;
                GridRepresentanteLegal.DataBind();
            }
            else
            {
                PanelSolicitudesMsg.Visible = false;

                GridRepresentanteLegal.PageIndex = pagina;
                GridRepresentanteLegal.DataSource = respRepLegal;
                GridRepresentanteLegal.DataBind();
            }
            UpdatePanelMgs.Update();
        }

        protected void GridRepresentanteLegal_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            RepLegal repLegalFiltro = new RepLegal();
            repLegalFiltro = (RepLegal)Session["repLegalFiltro"];
            if (repLegalFiltro == null)
            {
                repLegalFiltro = new RepLegal();
            }

            repLegalFiltro.pagina = e.NewPageIndex;
            Session["repLegalFiltro"] = repLegalFiltro;

            GridRepresentanteLegal.PageIndex = e.NewPageIndex;
            GridRepresentanteLegal.DataBind();
            CargaGrilla();
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            FiltrarGrilla();
            CargaGrilla();
        }

        private void FiltrarGrilla()
        {
            RepLegal repLegalFiltro = new RepLegal();
            repLegalFiltro.representanteLegal = new Solicitante();
            
            if (RutPersona.Text != null && !RutPersona.Text.Equals(""))
            {
                String rutCompleto = Convert.ToString(RutPersona.Text);
                String[] rutPartes = rutCompleto.Split('-');

                repLegalFiltro.representanteLegal.tipoPersona = new ParametroGenerico();
                repLegalFiltro.representanteLegal.rut = Convert.ToInt32(rutPartes[0]);
                repLegalFiltro.representanteLegal.dv = Convert.ToChar(rutPartes[1]);
            }

            if (NombrePersona.Text != null && !NombrePersona.Text.Equals(""))
            {
                repLegalFiltro.representanteLegal.nombreSolicitante = NombrePersona.Text;
            }

            Session["repLegalFiltro"] = (RepLegal)repLegalFiltro;
        }

        protected void Crear_Click(object sender, EventArgs e)
        {
            string path = "~/Mantenedores/Titulares/agregarRepresentanteLegal.aspx?acc=1&ini=1";
            Response.Redirect(path);
        }
    }
}