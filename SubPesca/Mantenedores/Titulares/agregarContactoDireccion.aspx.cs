using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Entidades;
using Validaciones.cl.subpesca.rb.mantenedor;
using SubPesca.Utilidades;
using Datos.Contantes;

namespace SubPesca.Mantenedores.Titulares
{
    public partial class agregarContactoDireccion : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema

        RegionDA regionDA = new LogicaNegocio.cl.subpesca.rb.common.RegionDA();
        ComunaDA comunaDA = new ComunaDA();
        TipoDA tipoDa = new TipoDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();

        MantenedorTitularesValidacion mantenedorTitularesValidacion = new MantenedorTitularesValidacion();


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
            // Se recibe el rutPersona y bp (que indica que pagina invocó contactoDireccion)
            try
            {
                if (Request.QueryString["rutPersona"] != null && Request.QueryString["bp"] != null && Request.QueryString["acc"] != null)
                {
                    String backPage = Request.QueryString["bp"];
                    String acc = Request.QueryString["acc"];
                    
                    if(backPage.Equals("1")){ //Titulares
                        Solicitante solicitante = (Solicitante)Session["Titular"];
                        ViewState["SOLICITANTE"] = solicitante;
                        ViewState["TIPO_SOLICITANTE"] = "Titular";
                        ViewState["SESSION_SOLICITANTE"] = "Titular";
                        ViewState["PATH_BP"] = "~/Mantenedores/Titulares/agregarTitular.aspx?acc=" + acc;
                        ViewState["ACCION"] = acc;
                    }

                    else if (backPage.Equals("2")) //Representantes Legales
                    {
                        RepLegal repLegal = (RepLegal)Session["RepresentanteLegal"];
                        Solicitante solicitante = repLegal.representanteLegal;
                        ViewState["SOLICITANTE"] = solicitante;
                        ViewState["TIPO_SOLICITANTE"] = "Representante Legal";
                        ViewState["SESSION_SOLICITANTE"] = "RepresentanteLegal";
                        ViewState["PATH_BP"] = "~/Mantenedores/Titulares/agregarRepresentanteLegal.aspx?acc=" + acc;
                        ViewState["ACCION"] = acc;
                    }

                    else if (backPage.Equals("3")) //Operadores
                    {
                        Operador operador = (Operador)Session["Operador"];
                        Solicitante solicitante = operador.operador;
                        ViewState["SOLICITANTE"] = solicitante;
                        ViewState["TIPO_SOLICITANTE"] = "Operador";
                        ViewState["SESSION_SOLICITANTE"] = "Operador";
                        ViewState["PATH_BP"] = "~/Mantenedores/Titulares/agregarOperador.aspx?acc=" + acc;
                        ViewState["ACCION"] = acc;
                    }
                }
                else
                {

                }

            }
            catch
            {

            };
        }

        private void InicializarFormulario()
        {
            Initialize_Comboboxs();

            String accion = (String) ViewState["ACCION"];
            Inicialize_Campos(accion);

        }

        private void Inicialize_Campos(string accion)
        {

            int accionEntero = Convert.ToInt32(accion);
            if (accionEntero == 1)
            {
                //PanelGeneral.Visible = false;
            }else{
                //PanelGeneral.Visible = true;
            }

        }

        protected void Initialize_Comboboxs()
        {
            Carga_Combobox("Region");
            Region.SelectedValue = "-1";
            
            Carga_Combobox("Comuna");
            Comuna.SelectedValue = "-1";

            //Carga_Combobox("ComunaCasilla");
            //ComunaCasilla.SelectedValue = "-1";

            //Carga_Combobox("TipoContacto");
            //TipoContacto.SelectedValue = "-1";
        }

        private void Carga_Combobox(string combobox)
        {
            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

            switch (combobox)
            {
                case "Region":
                    // Cargamos el combobox: Región
                    Region.Items.Clear();
                    //Region.DataSource = regionDA.Listar(usuario_logeado.id_usuario);
                    Region.DataSource = regionDA.ListarRegion(0);
                    Region.DataTextField = "Region";
                    Region.DataValueField = "IdRegion";
                    Region.DataBind();
                    Region.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "Comuna":
                    // Cargamos el combobox: Comuna
                    Comuna.Items.Clear();
                    if (Convert.ToInt32(Region.SelectedValue) > 0)
                    {
                        Comuna.DataSource = parametroGenericoDA.ListarComunas(Convert.ToInt32(Region.SelectedValue));
                        Comuna.DataTextField = "descripcion";
                        Comuna.DataValueField = "id";
                        Comuna.DataBind();
                    }
                    Comuna.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                //case "ComunaCasilla":
                //    ComunaCasilla.Items.Clear();
                //    if (Convert.ToInt32(Region.SelectedValue) > 0)
                //    {
                //        ComunaCasilla.DataSource = parametroGenericoDA.ListarComunas(Convert.ToInt32(Region.SelectedValue));
                //        ComunaCasilla.DataTextField = "descripcion";
                //        ComunaCasilla.DataValueField = "id";
                //        ComunaCasilla.DataBind();
                //    }
                //    ComunaCasilla.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                //    break;
                //case "TipoContacto":
                //    // Cargamos el combobox: Tipo Contacto
                //    TipoContacto.Items.Clear();
                //    TipoContacto.DataSource = tipoDa.ListarTipo("TIPO_CONTACTO");
                //    TipoContacto.DataTextField = "descripcion";
                //    TipoContacto.DataValueField = "id";
                //    TipoContacto.DataBind();
                //    TipoContacto.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                //    break;
            }
        }
        
        //protected void AgregarContacto_Click(object sender, ImageClickEventArgs e)
        //{
        //    agregarGrillaContactoDireccion();
        //    cargarGrillaContactoDireccion();
            
        //}

        //private void cargarGrillaContactoDireccion()
        //{
        //    List<Contacto> List_DireccionMatriz = (List<Contacto>)ViewState["Contacto_DireccionMatriz"];

        //    if (List_DireccionMatriz == null)
        //    {
        //        List_DireccionMatriz = new List<Contacto>();
        //    }

        //    GridDireccionMatriz.DataSource = List_DireccionMatriz;
        //    GridDireccionMatriz.DataBind();
        //    GridDireccionMatriz.Visible = true;

        //    ViewState["Contacto_DireccionMatriz"] = (List<Contacto>)List_DireccionMatriz;

        //    limpiarDireccionMatriz();
        //}

        //private void agregarGrillaContactoDireccion()
        //{
        //    List<Contacto> List_DireccionMatriz = (List<Contacto>)ViewState["Contacto_DireccionMatriz"];

        //    int index = 0;
        //    if (List_DireccionMatriz == null)
        //    {
        //        List_DireccionMatriz = new List<Contacto>();
        //    }
        //    else
        //    {
        //        index = List_DireccionMatriz.Count;
        //    }

        //    Contacto contacto = new Contacto();
        //    contacto.accion = accion.INGRESAR;
        //    contacto.index = Convert.ToInt32(index);
        //    contacto.tipoContacto = new ParametroGenerico(Convert.ToInt32(TipoContacto.SelectedValue),TipoContacto.SelectedItem.Text);
        //    contacto.valorContacto = ValorContacto.Text;
        //    contacto.detalle = DetalleContacto.Text;
        //    if (Publico.Checked)
        //    {
        //        contacto.accesoPublico = true;
        //    }
        //    else
        //    {
        //        contacto.accesoPublico = false;
        //    }

        //    List<String> listaErroresDireccionMatriz = mantenedorTitularesValidacion.validaContactoMatrizSucursal(contacto, List_DireccionMatriz);

        //    if (listaErroresDireccionMatriz.Count <= 0)
        //    {

        //        List_DireccionMatriz.Add(contacto);

        //        GridDireccionMatriz.DataSource = List_DireccionMatriz;
        //        GridDireccionMatriz.DataBind();
        //        GridDireccionMatriz.Visible = true;

        //        ViewState["Contacto_DireccionMatriz"] = (List<Contacto>)List_DireccionMatriz;

        //        LabelContactoMatrizSucursales.Text = "Se ha guardado existosamente el contacto a la dirección.";
        //        Ico_ContactoMatrizSucursales.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
        //        PanelContactoMatrizSucursales.Visible = true;

        //    }
        //    else
        //    {
        //        foreach (String error in listaErroresDireccionMatriz)
        //        {
        //            Page.Validators.Add(new ValidationError("grupo2", error));
        //        }
        //    }
            
        //}


        //private void limpiarDireccionMatriz()
        //{
        //    TipoContacto.SelectedValue = "-1";
        //    ValorContacto.Text = "";
        //    DetalleContacto.Text = "";
        //    Publico.Checked = false;
        //}

        private void limpiarFormulario() {


            Direccion.Text = "";
            //CasaMatriz.Checked = false;
            Region.SelectedValue = "-1";
            Comuna.SelectedValue = "-1";
            //Casilla.Text = "";
            //ComunaCasilla.SelectedValue = "-1";
            //NumeroControlIngresoDireccion.Text = "";
            //FechaTextRecepcion.Text = "";
            //ArchivoAdjunto = null;

            //LabelContactoMatrizSucursales.Text = "";
            //PanelContactoMatrizSucursales.Visible = false;

            //limpiarDireccionMatriz();
            //GridDireccionMatriz.Visible = false;
        }

        protected void Region_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("Comuna");
            Carga_Combobox("ComunaCasilla");
        }

        //protected void GridDireccionMatriz_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {

        //            // Borrar
        //            ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
        //            if (boton_eliminar != null)
        //            {
        //                boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar el contacto?')");
        //                boton_eliminar.Visible = true;
        //            };

        //        };
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        //protected void GridDireccionMatriz_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    int idDireccionMatriz = 0;
        //    switch (e.CommandName)
        //    {
        //        case "Eliminar":
        //            idDireccionMatriz = Convert.ToInt32(e.CommandArgument);
        //            GridDireccionMatriz.EditIndex = -1;
        //            EliminarGrillaDireccionMatriz(idDireccionMatriz);
        //            cargarGrillaContactoDireccion();
        //            break;
        //    }
        //}

        private void EliminarGrillaDireccionMatriz(int idDireccionMatriz)
        {
                
        }

        //protected void GridDireccionMatriz_RowCreated(object sender, GridViewRowEventArgs e)
        //{

        //    if (e.Row.RowType == DataControlRowType.Header)
        //    {
        //        GridView GridArchivosAdjuntosAntEspaciales = (GridView)sender;


        //        // Creating a Row
        //        GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

        //        //Adding Ambito Column
        //        TableCell HeaderCell = new TableCell();
        //        HeaderCell.Text = "Contacto Matriz y Sucursales";
        //        HeaderCell.CssClass = "customGeneralTitle";
        //        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //        HeaderCell.ColumnSpan = 14;

        //        HeaderRow.Cells.Add(HeaderCell);

        //        //Adding the Row at the 0th position (first row) in the Grid
        //        GridDireccionMatriz.Controls[0].Controls.AddAt(0, HeaderRow);

        //    }
        //}

        protected void GuardarDireccion_Click(object sender, EventArgs e)
        {

            Solicitante solicitante = (Solicitante)ViewState["SOLICITANTE"];
            List<MatrizSucursal> listaMatrizSucursal = solicitante.matrizSucursales;

            int index = 0;
            if (listaMatrizSucursal == null)
            {
                listaMatrizSucursal = new List<MatrizSucursal>();
            }
            else
            {
                index = listaMatrizSucursal.Count;
            }

            MatrizSucursal matrizSucursal = new MatrizSucursal();
            matrizSucursal.accion = accion.INGRESAR;
            matrizSucursal.index = Convert.ToInt32(index);
            matrizSucursal.direccion = Direccion.Text;
            //if (CasaMatriz.Checked)
            //{
            //    matrizSucursal.matriz = true;
            //}
            //else {
            //    matrizSucursal.matriz = false;

            //}
            matrizSucursal.region = new Region();
            matrizSucursal.region.id_region = Convert.ToInt32(Region.SelectedValue);
            matrizSucursal.region.region = Region.SelectedItem.Text;
            matrizSucursal.region.comuna = new Comuna();
            matrizSucursal.region.comuna.id_comuna = Convert.ToInt32(Comuna.SelectedValue);
            matrizSucursal.region.comuna.comuna = Comuna.SelectedItem.Text;
            //matrizSucursal.casilla = Casilla.Text;

            //if (!ComunaCasilla.SelectedValue.Equals("-1"))
            //{
            //    matrizSucursal.comunaCasilla = new Comuna();
            //    matrizSucursal.comunaCasilla.id_comuna = Convert.ToInt32(ComunaCasilla.SelectedValue);
            //    matrizSucursal.comunaCasilla.comuna = ComunaCasilla.SelectedItem.Text;
            //}

            //if (!NumeroControlIngresoDireccion.Text.Equals(""))
            //{
            //    matrizSucursal.numeroCI = Convert.ToInt32(NumeroControlIngresoDireccion.Text);
            //}

            //if (!FechaTextRecepcion.Text.Equals(""))
            //{
            //    matrizSucursal.fechaCI = Convert.ToDateTime(FechaTextRecepcion.Text);
            //}


            //if (ArchivoAdjunto.HasFile)
            //{
            //    ArchivoBinarioEspecial archivoBinario = new ArchivoBinarioEspecial();

            //    archivoBinario.nombreFisico = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
            //    archivoBinario.nombreArchivo = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
            //    archivoBinario.formato = ArchivoAdjunto.PostedFile.FileName.Substring(ArchivoAdjunto.PostedFile.FileName.LastIndexOf(".") + 1).ToLower(); ;
            //    archivoBinario.tamano = ArchivoAdjunto.PostedFile.InputStream.Length;
            //    archivoBinario.bytes = ArchivoAdjunto.FileBytes;

            //    matrizSucursal.archivoBinarioEspecial = archivoBinario;
            //}

            //List<Contacto> listaContacto = (List<Contacto>)ViewState["Contacto_DireccionMatriz"];
            //matrizSucursal.contactosMatrizSuc = listaContacto;

            List<string> listaErroresContactoDireccion = mantenedorTitularesValidacion.validaMatrizSucursal(matrizSucursal);

            if (listaErroresContactoDireccion.Count <= 0)
            {
                
                listaMatrizSucursal.Add(matrizSucursal);
                solicitante.matrizSucursales = listaMatrizSucursal;

                String sessionName = ViewState["SESSION_SOLICITANTE"].ToString();

                if (sessionName != null && sessionName.Equals("Titular"))
                {
                    Session[sessionName] = (Solicitante)solicitante;
                }
                else if (sessionName != null && sessionName.Equals("RepresentanteLegal")) {
                    RepLegal repLegal = new RepLegal();
                    repLegal.representanteLegal = solicitante;

                    Session[sessionName] = (RepLegal) repLegal;

                }
                else if (sessionName != null && sessionName.Equals("Operador")) {
                    Operador operador = new Operador();
                    operador.operador = solicitante;

                    Session[sessionName] = (Operador) operador;
                }
                
                msgGrilla.Text = "Se ha guardado exitosamente la dirección al " + ViewState["TIPO_SOLICITANTE"].ToString() + ".";
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                Content_msgGrilla.Visible = true;

                limpiarFormulario();
            }
            else {
                foreach (String error in listaErroresContactoDireccion)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
            }
        }

        protected void Volver_Click(object sender, EventArgs e)
        {
            string path = ViewState["PATH_BP"].ToString();
            Response.Redirect(path);
        }
    }
}