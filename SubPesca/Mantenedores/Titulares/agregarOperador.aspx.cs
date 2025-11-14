using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using Validaciones.cl.subpesca.rb.mantenedor;
using SubPesca.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.mantenedores;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using System.Drawing;
using Datos.Utilidades;

namespace SubPesca.Mantenedores.Titulares
{
    public partial class agregarOperador : System.Web.UI.Page
    {
        
        
        MantenedorTitularesValidacion mantenedorTitularesValidacion = new MantenedorTitularesValidacion();
        MantenedorTitularService mantenedorTitularService = new MantenedorTitularService();
        OperadorDA operadorDA = new OperadorDA();

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ObtencionParametros();
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

                    /* Crear */
                    ViewState["acc"] = acc;

                    if (acc != null && acc.Equals("1"))
                    {
                        String ini = Request.QueryString["ini"];
                        if (ini != null && ini.Equals("1"))
                        {
                            Session["Operador"] = null;
                        }
                    }

                    /* Modificar */
                    else if (acc != null && acc.Equals("2"))
                    {
                        if (Request.QueryString["rutPersona"] != null)
                        {

                            /* Se obtiene la persona ingresada en la base de datos */
                            int rutOperador = Convert.ToInt32(Request.QueryString["rutPersona"]);
                            Operador operador = operadorDA.ObtenerOperador(rutOperador,null);

                            
                            if (operador != null)
                            {
                                /* Se obtienen las direcciones del operador */
                                operador.operador.matrizSucursales = operadorDA.ListarOperadorMatrizSuc(operador.operador.rut);

                                /* Se obtienen los contactos del operador */
                                operador.operador.listaContacto = operadorDA.ListarContactoOperador(operador.operador.rut,0);

                                Session["Operador"] = (Operador)operador;
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

                GuardarOperador.Visible = true;
                ModificarOperador.Visible = false;

                //PanelGeneral.Visible = false;
            }

            /* Modificar */
            else if (accion == 2)
            {

                LabelAccion.Text = "Modificar";
                RutPersona.ReadOnly = true;
                RutPersona.BackColor = Color.FromArgb(221, 221, 238);
                EjemploRut.Visible = false;
                
                cambiarNombrePersona.Visible = false;
                VerNombresPersona.Visible = false;

                GuardarOperador.Visible = false;
                ModificarOperador.Visible = true;

                //NombreOperador.ReadOnly = true;
                //NombreOperador.BackColor = Color.FromArgb(221, 221, 238);

                //PanelGeneral.Visible = true;
            }
        }

        private void InicializarFormulario()
        {
            Operador operador = (Operador)Session["Operador"];

            if (operador != null)
            {
                RutPersona.Text = Convert.ToString(operador.operador.rut + "-" + operador.operador.dv);
                
                NombreOperador.Text = operador.operador.nombreSolicitante;
                nombreOperadorAnterior.Value = operador.operador.nombreSolicitante;

                //NumeroControlIngreso.Text = Convert.ToString(operador.operador.numeroControlIngreso);

                //FechaTextRecepcion.Text = FechaUtils.formatearFecha(operador.operador.fechaControlIngreso);

                /* Se debe cargar la grilla de direcciones */
                if (operador.operador != null && operador.operador.matrizSucursales != null && operador.operador.matrizSucursales.Count > 0)
                {
                    cargarGrillaContactoDireccion(operador.operador.matrizSucursales);
                }

                /* Se debe cargar la grilla de contactos */

                if (operador.operador != null && operador.operador.listaContacto != null && operador.operador.listaContacto.Count > 0)
                {
                    cargarGrillaContactos(operador.operador.listaContacto);
                }
            }
        }

        private void cargarGrillaContactoDireccion(List<MatrizSucursal> listMatrizSucursales)
        {
            GridContactoMatrizSucursales.DataSource = listMatrizSucursales;
            GridContactoMatrizSucursales.DataBind();
            GridContactoMatrizSucursales.Visible = true;
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

                ////Descargar
                //String idArchivoBinario = DataBinder.Eval(e.Row.DataItem, "idArchivoBinario").ToString();
                //ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                //if (boton_descargar != null && idArchivoBinario != null && !idArchivoBinario.Equals("") && Convert.ToInt32(idArchivoBinario) > 0)
                //{
                //    boton_descargar.Visible = true;
                //};

                ////Ver
                //ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                //if (boton_ver != null)
                //{
                //    boton_ver.Visible = true;

                //}

            }
        }

        protected void GridContactoMatrizSucursales_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GuardarOperador_Click(object sender, EventArgs e)
        {
            guardarBorradorFormulario();
            Operador operador = (Operador)Session["Operador"];
            operador.operador.idEstadoAsociacion = rbEstadosGenerales.VIGENTE;
            operador.accion = accion.INGRESAR;

            List<string> listaErroresOperador = mantenedorTitularesValidacion.validaOperador(operador);

            if (listaErroresOperador.Count <= 0)
            {                
                bool resp = mantenedorTitularService.guardarOperador(operador);

                if (resp)
                {

                    Session["Operador"] = null;
                    string path = "~/Mantenedores/Titulares/administrarOperadores.aspx?acc=" + ViewState["acc"].ToString();
                    Response.Redirect(path);
                }
                else {

                    msgGrilla.Text = "No se ha guardado el Operador en el sistema.";
                    Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Content_msgGrilla.Visible = true;

                }
            }
            else
            {
                foreach (String error in listaErroresOperador)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
            }
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            Session["Operador"] = null;
            string path = "~/Mantenedores/Titulares/administrarOperadores.aspx";
            Response.Redirect(path);
        }

        protected void ImageButtonMatrizSucursal_Click(object sender, ImageClickEventArgs e)
        {
            guardarBorradorFormulario();

            string path = "~/Mantenedores/Titulares/agregarContactoDireccion.aspx?rutPersona=" + RutPersona.Text + "&bp=3&acc=" + ViewState["acc"].ToString();
            Response.Redirect(path);
        }

        protected void GridContactoMatrizSucursales_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int idMatrizSuc = Convert.ToInt32(arg[0]);
            int index = Convert.ToInt32(arg[1]);

            Operador operador = (Operador)Session["Operador"];
            Solicitante solicitante = operador.operador;

            switch (e.CommandName)
            {
                //case "Ver":

                //    Response.Redirect("~/Mantenedores/Titulares/verContactoOperador.aspx?idMatrizSuc=" + idMatrizSuc + "&index=" + index + "&acc=" + ViewState["acc"].ToString());

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

                //        List<MatrizSucursal> List_MatrizSucursales = solicitante.matrizSucursales;
                //        ArchivoBinarioEspecial archivoBinarioEspecial = null;

                //        foreach (MatrizSucursal matrizSucursal in List_MatrizSucursales)
                //        {
                //            if (matrizSucursal.index.Equals(index))
                //            {
                //                archivoBinarioEspecial = matrizSucursal.archivoBinarioEspecial;
                //            }
                //        }

                //    Response.Clear();
                //    Response.Buffer = true;
                //    Response.Charset = "";
                //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //    Response.ContentType = "application/" + archivoBinarioEspecial.formato;
                //    Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinarioEspecial.nombreFisico + "." + archivoBinarioEspecial.formato);
                //    Response.BinaryWrite(archivoBinarioEspecial.bytes);
                //    Response.Flush();
                //    Response.End();

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
                    operador.operador = solicitante;

                    Session["Operador"] = (Operador)operador;
                    */

                    GridContactoMatrizSucursales.EditIndex = -1;
                    EliminarGrilla(index, "MatrizSucursal", solicitante);

                    break;
            }
        }

        private void EliminarGrilla(int index, string tipoGrilla, Solicitante solicitante)
        {
            Operador operador = null;
            
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
                    operador = (Operador) Session["Operador"];
                    operador.operador = solicitante;

                    Session["Operador"] = (Operador)operador;

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
                    operador = (Operador)Session["Operador"];
                    operador.operador = solicitante;

                    Session["Operador"] = (Operador) operador;

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

        private void guardarBorradorFormulario()
        {
            Operador operador = (Operador)Session["Operador"];
            if (operador == null)
            {
                operador = new Operador();

                if (operador.operador == null)
                {
                    operador.operador = new Solicitante();
                }
            }

            String rutCompleto = Convert.ToString(RutPersona.Text);
            String[] rutPartes = rutCompleto.Split('-');

            operador.operador.rut = Convert.ToInt32(rutPartes[0]);
            operador.operador.dv = Convert.ToChar(rutPartes[1]);

            operador.operador.nombreSolicitante = NombreOperador.Text;

            operador.operador.nombreTitular = nombreOperadorAnterior.Value;

            //if (NumeroControlIngreso.Text != null && !NumeroControlIngreso.Text.Equals(""))
            //{
            //    operador.operador.numeroControlIngreso = Convert.ToInt32(NumeroControlIngreso.Text);
            //}

            //if (FechaTextRecepcion.Text != null && !FechaTextRecepcion.Text.Equals(""))
            //{
            //    operador.operador.fechaControlIngreso = Convert.ToDateTime(FechaTextRecepcion.Text);
            //}

           
           
            
            Session["Operador"] = (Operador)operador;

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

            string path = "~/Mantenedores/Titulares/agregarNombres.aspx?rutPersona=" + RutPersona.Text + "&bp=3&acc=" + ViewState["acc"].ToString();
            Response.Redirect(path);
        }

        protected void ModificarOperador_Click(object sender, EventArgs e)
        {
            guardarBorradorFormulario();
            Operador operador = (Operador)Session["Operador"];
            operador.operador.accion = accion.MODIFICAR;

            List<string> listaErroresOperador = mantenedorTitularesValidacion.validaOperador(operador);

            if (listaErroresOperador.Count <= 0)
            {
                operador.operador.idEstadoAsociacion = rbEstadosGenerales.VIGENTE;
                bool resp = mantenedorTitularService.modificarOperador(operador);

                if (resp)
                {
                    Session["Operador"] = null;
                    string path = "~/Mantenedores/Titulares/administrarOperadores.aspx?acc=" + ViewState["acc"].ToString();
                    Response.Redirect(path);

                }else{
                    msgGrilla.Text = "No se ha modificado el Operador en el sistema.";
                    Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Content_msgGrilla.Visible = true;
                
                }

            }
            else
            {
                foreach (String error in listaErroresOperador)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
            }
        }

        protected void AgregarContacto_Click(object sender, ImageClickEventArgs e)
        {
            guardarBorradorFormulario();

            string path = "~/Mantenedores/Titulares/agregarContactos.aspx?rutPersona=" + RutPersona.Text + "&bp=3&acc=" + ViewState["acc"].ToString();
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

            Operador operador = (Operador)Session["Operador"];
            Solicitante solicitante = operador.operador;

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