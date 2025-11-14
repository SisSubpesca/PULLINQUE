using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using SubPesca.Utilidades;
using Validaciones.cl.subpesca.rb.mantenedor;
using LogicaNegocio.cl.subpesca.rb.servicios.mantenedores;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using System.Drawing;
using Datos.Utilidades;

namespace SubPesca.Mantenedores.Titulares
{
    public partial class agregarRepresentanteLegal : System.Web.UI.Page
    {

        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        RepLegalDA repLegalDA = new RepLegalDA();

        MantenedorTitularesValidacion mantenedorTitularesValidacion = new MantenedorTitularesValidacion();
        MantenedorTitularService mantenedorTitularService = new MantenedorTitularService();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ObtencionParametros();
                InicializarCombobox();
                InicializarFormulario();
            }
        }

        private void ObtencionParametros()
        {
            // Se recibe el rutPersona y la pagina que invoco el formulario.
            try
            {
                if (Request.QueryString["acc"] != null)
                {
                    String acc = Request.QueryString["acc"];
                    ViewState["acc"] = acc;

                    /* Crear */
                    if (acc != null && acc.Equals("1"))
                    {
                        String ini = Request.QueryString["ini"];
                        if (ini != null && ini.Equals("1"))
                        {
                            Session["RepresentanteLegal"] = null;
                        }
                    }

                    /* Modificar */
                    else if (acc != null && acc.Equals("2"))
                    {
                        if (Request.QueryString["rutPersona"] != null)
                        {

                            /* Se obtiene la persona ingresada en la base de datos */
                            int rutRepresentanteLegal = Convert.ToInt32(Request.QueryString["rutPersona"]);
                            RepLegal repLegal = repLegalDA.ObtieneRepresentanteLegal(rutRepresentanteLegal, null);

                            
                            if (repLegal != null)
                            {
                                /* Se obtienen las direcciones del representante legal */
                                repLegal.representanteLegal.matrizSucursales = repLegalDA.ListarRepresentanteMatrizSuc(repLegal.representanteLegal.rut);

                                /* Se obtienen los contactos del representante legal */
                                repLegal.representanteLegal.listaContacto = repLegalDA.ListarContactoRepresentante(repLegal.representanteLegal.rut,0);

                                Session["RepresentanteLegal"] = (RepLegal)repLegal;
                            }
                        }

                    }
                    DespliegaObjetosInterfaz(Convert.ToInt32(acc));
                }
                else
                {

                }

            }
            catch
            {

            };
        }

        private void DespliegaObjetosInterfaz(int accion)
        {
            /* Crear */
            if (accion == 1)
            {

                LabelAccion.Text = "Crear";
                RutPersona.ReadOnly = false;
                EjemploRut.Visible = true;
                cambiarNombrePersona.Visible = false;
                VerNombresPersona.Visible = false;

                GuardarRepresentanteLegal.Visible = true;
                ModificarRepresentanteLegal.Visible = false;

                //PanelGeneral.Visible = false;
            }

            /* Modificar */
            else if (accion == 2)
            {

                LabelAccion.Text = "Modificar";
                RutPersona.ReadOnly = true;
                RutPersona.BackColor = Color.FromArgb(221, 221, 238);
                EjemploRut.Visible = false;
                
                /* Según rn solo las personas naturales cambian de nombre */
                cambiarNombrePersona.Visible = false;
                VerNombresPersona.Visible = false;

                GuardarRepresentanteLegal.Visible = false;
                ModificarRepresentanteLegal.Visible = true;

                //NombreRepresentanteLegal.ReadOnly = true;
                //NombreRepresentanteLegal.BackColor = Color.FromArgb(221, 221, 238);

                //PanelGeneral.Visible = true;
            }
        }

        private void InicializarCombobox()
        {
            CargarComboBox("TipoPersona");
            TipoPersona.SelectedValue = "-1"; 
        }

        private void CargarComboBox(String combobox)
        {
            switch (combobox)
            {

                case "TipoPersona":
                    // Cargamos el combobox: Tipo
                    TipoPersona.Items.Clear();
                    TipoPersona.DataSource = parametroGenericoDA.ListarTipoPersonaJuridica(new ParametroGenerico());
                    TipoPersona.DataTextField = "descripcion";
                    TipoPersona.DataValueField = "id";
                    TipoPersona.DataBind();
                    TipoPersona.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;
            }
        }

        private void InicializarFormulario()
        {
            RepLegal repLegal = (RepLegal)Session["RepresentanteLegal"];

            if (repLegal != null)
            {
                RutPersona.Text = Convert.ToString(repLegal.representanteLegal.rut + "-" + repLegal.representanteLegal.dv);
                
                NombreRepresentanteLegal.Text = repLegal.representanteLegal.nombreSolicitante;
                nombreRepresentanteLegalAnterior.Value = repLegal.representanteLegal.nombreSolicitante;
                
                //NumeroControlIngreso.Text = Convert.ToString(repLegal.representanteLegal.numeroControlIngreso);

                //FechaTextRecepcion.Text = FechaUtils.formatearFecha(repLegal.representanteLegal.fechaControlIngreso);

                if (repLegal.representanteLegal != null && repLegal.representanteLegal.tipoPersona != null && repLegal.representanteLegal.tipoPersona.id > 0)
                {
                    TipoPersona.SelectedValue = Convert.ToString(repLegal.representanteLegal.tipoPersona.id);
                }
                
                /* Se debe cargar la grilla de direcciones */
                if (repLegal.representanteLegal != null && repLegal.representanteLegal.matrizSucursales != null && repLegal.representanteLegal.matrizSucursales.Count > 0)
                {
                    cargarGrillaContactoDireccion(repLegal.representanteLegal.matrizSucursales);
                }

                /* Se debe cargar la grilla de contactos */
                if (repLegal.representanteLegal != null && repLegal.representanteLegal.listaContacto != null && repLegal.representanteLegal.listaContacto.Count > 0)
                {
                    cargarGrillaContactos(repLegal.representanteLegal.listaContacto);
                }

            }
        }

        private void cargarGrillaContactoDireccion(List<MatrizSucursal> listMatrizSucursales)
        {
            GridContactoMatrizSucursales.DataSource = listMatrizSucursales;
            GridContactoMatrizSucursales.DataBind();
            GridContactoMatrizSucursales.Visible = true;
        }

        protected void GridContactoMatrizSucursales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                /* Acción */
                HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
                if (hidden_accion != null && !hidden_accion.Value.Equals("") && (Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR || Convert.ToInt32(hidden_accion.Value) == accion.IGNORAR))
                {
                    e.Row.Attributes["style"] = "display:none";
                };

                //Eliminar
                ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
                if (boton_borrar != null)
                {
                    boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar la Dirección?')");
                    boton_borrar.Visible = true;

                }

                //Descargar
                String idArchivoBinario = DataBinder.Eval(e.Row.DataItem, "idArchivoBinario").ToString();
                ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                if (boton_descargar != null && idArchivoBinario != null && !idArchivoBinario.Equals("") && Convert.ToInt32(idArchivoBinario) > 0)
                {
                    boton_descargar.Visible = true;
                };

                //Ver
                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    boton_ver.Visible = true;

                }

            }
        }

        protected void GridContactoMatrizSucursales_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int idMatrizSuc = Convert.ToInt32(arg[0]);
            int index = Convert.ToInt32(arg[1]);

            RepLegal representanteLegal = (RepLegal)Session["RepresentanteLegal"];
            Solicitante solicitante = representanteLegal.representanteLegal;

            switch (e.CommandName)
            {
                //case "Ver":

                //    Response.Redirect("~/Mantenedores/Titulares/verContactoRepresentanteLegal.aspx?idMatrizSuc=" + idMatrizSuc + "&index=" + index + "&acc=" + ViewState["acc"].ToString());

                //    break;

                //case "Descargar":

                //    /*
                //    ArchivoBinarioEspecial archivoBinarioEspecial = null;

                //    listaMatrizSucursal = solicitante.matrizSucursales;
                //    MatrizSucursal matrizSucursal = null;

                //    int i = 1;
                //    foreach (MatrizSucursal matrizSucursalSession in listaMatrizSucursal)
                //    {
                //        if (i == index)
                //        {
                //            matrizSucursal = matrizSucursalSession;
                //        }
                //        i++;
                //    }

                //    archivoBinarioEspecial = matrizSucursal.archivoBinarioEspecial;
                //    */

                //    List<MatrizSucursal> List_MatrizSucursales = solicitante.matrizSucursales;
                //        ArchivoBinarioEspecial archivoBinarioEspecial = null;

                //        foreach (MatrizSucursal matrizSucursal in List_MatrizSucursales)
                //        {
                //            if (matrizSucursal.index.Equals(index))
                //            {
                //                archivoBinarioEspecial = matrizSucursal.archivoBinarioEspecial;
                //            }
                //        }

                //        if (archivoBinarioEspecial != null)
                //        {
                //            Response.Clear();
                //            Response.Buffer = true;
                //            Response.Charset = "";
                //            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //            Response.ContentType = "application/" + archivoBinarioEspecial.formato;
                //            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinarioEspecial.nombreArchivo + "." + archivoBinarioEspecial.formato);
                //            Response.BinaryWrite(archivoBinarioEspecial.bytes);
                //            Response.Flush();
                //            Response.End();
                //        }
                //    break;

                case "Eliminar":

                    /*
                    listaMatrizSucursal = solicitante.matrizSucursales;

                    foreach (MatrizSucursal matrizSucursalSession in listaMatrizSucursal)
                    {
                        if (matrizSucursalSession.index.Equals(Convert.ToInt32(index)))
                        {
                            listaMatrizSucursal.Remove(matrizSucursalSession);
                            break;
                        }
                    }

                    GridContactoMatrizSucursales.DataSource = listaMatrizSucursal;
                    GridContactoMatrizSucursales.DataBind();

                    solicitante.matrizSucursales = listaMatrizSucursal;
                    representanteLegal.representanteLegal = solicitante;

                    Session["RepresentanteLegal"] = (RepLegal)representanteLegal;
                    */

                    GridContactoMatrizSucursales.EditIndex = -1;
                    EliminarGrilla(index, "MatrizSucursal", solicitante);

                    break;
            }
        }

        private void EliminarGrilla(int index, string tipoGrilla, Solicitante solicitante)
        {

            RepLegal representanteLegal = null;
            
            switch (tipoGrilla)
            {
                case "MatrizSucursal":

                    List<MatrizSucursal> List_MatrizSucursal = solicitante.matrizSucursales;

                    foreach (MatrizSucursal matrizSucursal in List_MatrizSucursal)
                    {
                        if (matrizSucursal.index.Equals(index))
                        {

                            List<String> listaErroresEspAu = mantenedorTitularesValidacion.validaEliminacionMatrizSucursal(matrizSucursal);

                            if (listaErroresEspAu.Count <= 0)
                            {
                                if (matrizSucursal.accion == accion.INGRESAR)
                                {
                                    matrizSucursal.accion = accion.IGNORAR;
                                    GridContactoMatrizSucursales.Rows[index].Attributes["style"] = "display:none";
                                }
                                if (matrizSucursal.accion == accion.LISTADO)
                                {
                                    matrizSucursal.accion = accion.ELIMINAR;
                                    GridContactoMatrizSucursales.Rows[index].Attributes["style"] = "display:none";
                                }
                            }
                            else
                            {

                                foreach (String error in listaErroresEspAu)
                                {
                                    Page.Validators.Add(new ValidationError("grupo1", error));
                                }

                                break;
                            }
                        }
                    }

                    solicitante.matrizSucursales = List_MatrizSucursal;

                    representanteLegal = (RepLegal) Session["RepresentanteLegal"];
                    representanteLegal.representanteLegal = solicitante;

                    Session["RepresentanteLegal"] = representanteLegal;

                    cargarGrillaContactoDireccion(List_MatrizSucursal);

                    break;

                case "Contacto":

                    List<Contacto> List_Contacto = solicitante.listaContacto;

                    foreach (Contacto contacto in solicitante.listaContacto)
                    {
                        if (contacto.index.Equals(index))
                        {

                            List<String> listaErroresEspAu = mantenedorTitularesValidacion.validaEliminacionContacto(contacto);

                            if (listaErroresEspAu.Count <= 0)
                            {
                                if (contacto.accion == accion.INGRESAR)
                                {
                                    contacto.accion = accion.IGNORAR;
                                    GridViewContacto.Rows[index].Attributes["style"] = "display:none";
                                }
                                if (contacto.accion == accion.LISTADO)
                                {
                                    contacto.accion = accion.ELIMINAR;
                                    GridViewContacto.Rows[index].Attributes["style"] = "display:none";
                                }
                            }
                            else
                            {

                                foreach (String error in listaErroresEspAu)
                                {
                                    Page.Validators.Add(new ValidationError("grupo1", error));
                                }

                                break;
                            }
                        }
                    }

                    solicitante.listaContacto = List_Contacto;

                    representanteLegal = (RepLegal) Session["RepresentanteLegal"];
                    representanteLegal.representanteLegal = solicitante;

                    Session["RepresentanteLegal"] = representanteLegal;

                    cargarGrillaContactos(List_Contacto);

                    break;
            }
        }

        private void cargarGrillaContactos(List<Contacto> listContactos)
        {
            GridViewContacto.DataSource = listContactos;
            GridViewContacto.DataBind();
            GridViewContacto.Visible = true;
        }

        protected void GridContactoMatrizSucursales_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridContacto = (GridView)sender;

                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Listado de Direcciones";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridContacto.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        protected void GridContactoMatrizSucursales_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GuardarRepresentanteLegal_Click(object sender, EventArgs e)
        {
            guardarBorradorFormulario();
            RepLegal repLegal = (RepLegal)Session["RepresentanteLegal"];
            repLegal.representanteLegal.idEstadoAsociacion = rbEstadosGenerales.VIGENTE;
            repLegal.accion = accion.INGRESAR;

            List<string> listaErroresRepresentanteLegal = mantenedorTitularesValidacion.validaRepresentanteLegal(repLegal);

            if (listaErroresRepresentanteLegal.Count <= 0)
            {
               bool resp = mantenedorTitularService.guadarRepresentanteLegal(repLegal);

                if (resp)
                {

                    Session["RepresentanteLegal"] = null;
                    string path = "~/Mantenedores/Titulares/administrarRepresentantesLegales.aspx?acc=" + ViewState["acc"].ToString();
                    Response.Redirect(path);

                }
                else {
                    msgGrilla.Text = "No se ha guardado el Representante Legal en el sistema.";
                    Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Content_msgGrilla.Visible = true;
                }
            }
            else
            {
                foreach (String error in listaErroresRepresentanteLegal)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
            }
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            Session["RepresentanteLegal"] = null;
            string path = "~/Mantenedores/Titulares/administrarRepresentantesLegales.aspx";
            Response.Redirect(path);
        }

        protected void ImageButtonMatrizSucursal_Click(object sender, ImageClickEventArgs e)
        {
            guardarBorradorFormulario();
            string path = "~/Mantenedores/Titulares/agregarContactoDireccion.aspx?rutPersona=" + RutPersona.Text + "&bp=2&acc=" + ViewState["acc"].ToString();
            Response.Redirect(path);
        }

        private void guardarBorradorFormulario()
        {
            RepLegal repLegal = (RepLegal)Session["RepresentanteLegal"];
            if (repLegal == null) {
                repLegal = new RepLegal();

                if (repLegal.representanteLegal == null)
                {
                    repLegal.representanteLegal = new Solicitante();
                }
            }

            String rutCompleto = Convert.ToString(RutPersona.Text);
            String[] rutPartes = rutCompleto.Split('-');

            repLegal.representanteLegal.rut = Convert.ToInt32(rutPartes[0]);
            repLegal.representanteLegal.dv = Convert.ToChar(rutPartes[1]);

            repLegal.representanteLegal.nombreSolicitante = NombreRepresentanteLegal.Text;
            repLegal.representanteLegal.nombreTitular = nombreRepresentanteLegalAnterior.Value;

            //if (NumeroControlIngreso.Text != null && !NumeroControlIngreso.Text.Equals(""))
            //{
            //    repLegal.representanteLegal.numeroControlIngreso = Convert.ToInt32(NumeroControlIngreso.Text);
            //}

            //if (FechaTextRecepcion.Text != null && !FechaTextRecepcion.Text.Equals(""))
            //{
            //    repLegal.representanteLegal.fechaControlIngreso = Convert.ToDateTime(FechaTextRecepcion.Text);
            //}

            repLegal.representanteLegal.tipoPersona = new ParametroGenerico(Convert.ToInt32(TipoPersona.SelectedValue));
            
            Session["RepresentanteLegal"] = (RepLegal) repLegal;

        }

        protected void ImgAdd_PreRender(object sender, EventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            ScriptManager sc = ScriptManager.GetCurrent(this.Page);
            sc.RegisterPostBackControl(btn);
        }

        protected void ImageButtonCambiarNombre_Click(object sender, ImageClickEventArgs e)
        {
            guardarBorradorFormulario();

            string path = "~/Mantenedores/Titulares/agregarNombres.aspx?rutPersona=" + RutPersona.Text + "&bp=2&acc=" + ViewState["acc"].ToString();
            Response.Redirect(path);
        }

        protected void ModificarRepresentanteLegal_Click(object sender, EventArgs e)
        {
            guardarBorradorFormulario();
            RepLegal repLegal = (RepLegal)Session["RepresentanteLegal"];
            repLegal.representanteLegal.accion = accion.MODIFICAR;

            List<string> listaErroresRepresentanteLegal = mantenedorTitularesValidacion.validaRepresentanteLegal(repLegal);

            if (listaErroresRepresentanteLegal.Count <= 0)
            {
                repLegal.representanteLegal.idEstadoAsociacion = rbEstadosGenerales.VIGENTE;
                bool resp = mantenedorTitularService.modificarRepresentanteLegal(repLegal);

                if (resp)
                {
                    msgGrilla.Text = "Se ha modificado exitosamente el Representante Legal en el sistema.";
                    Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Content_msgGrilla.Visible = true;

                    Session["RepresentanteLegal"] = null;
                    string path = "~/Mantenedores/Titulares/administrarRepresentantesLegales.aspx?acc=" + ViewState["acc"].ToString();
                    Response.Redirect(path);
                }
                else
                {
                    msgGrilla.Text = "No se ha modificado el Representante Legal en el sistema.";
                    Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Content_msgGrilla.Visible = true;
                }
            }
            else
            {
                foreach (String error in listaErroresRepresentanteLegal)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
            }
        }

        protected void AgregarContacto_Click(object sender, ImageClickEventArgs e)
        {
            guardarBorradorFormulario();

            string path = "~/Mantenedores/Titulares/agregarContactos.aspx?rutPersona=" + RutPersona.Text + "&bp=2&acc=" + ViewState["acc"].ToString();
            Response.Redirect(path);

        }

        protected void GridViewContacto_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridContacto = (GridView)sender;

                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Listado de Contactos";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridViewContacto.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        protected void GridViewContacto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                /* Acción */
                HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
                if (hidden_accion != null && !hidden_accion.Value.Equals("") && (Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR || Convert.ToInt32(hidden_accion.Value) == accion.IGNORAR))
                {
                    e.Row.Attributes["style"] = "display:none";
                };

                //Eliminar
                ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
                if (boton_borrar != null)
                {
                    boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar el Contacto?')");
                    boton_borrar.Visible = true;

                }

            }
        }

        protected void GridViewContacto_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int idMatrizSuc = Convert.ToInt32(arg[0]);
            int index = Convert.ToInt32(arg[1]);

            RepLegal representanteLegal = (RepLegal)Session["RepresentanteLegal"];
            Solicitante solicitante = representanteLegal.representanteLegal;

            switch (e.CommandName)
            {
                case "Eliminar":
                    GridViewContacto.EditIndex = -1;
                    EliminarGrilla(index, "Contacto", solicitante);


                    break;
            }
        }
    }
}