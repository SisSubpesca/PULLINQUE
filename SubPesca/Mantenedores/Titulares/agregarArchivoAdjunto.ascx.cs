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
using Datos.Utilidades;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Contantes;

namespace SubPesca.Mantenedores.Titulares
{
    public partial class agregarArchivoAdjunto1 : System.Web.UI.UserControl
    {

        TipoDA tipoDa = new TipoDA();
        MantenedorTitularesValidacion mantenedorTitularesValidacion = new MantenedorTitularesValidacion();
        SolicitanteDA solicitanteDA = new SolicitanteDA();
        RepLegalDA repLegalDA = new RepLegalDA();
        OperadorDA operadorDA = new OperadorDA();

        Funciones funciones = new Funciones();

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
            if (funciones.retornaPagina().Contains("agregarTitular"))
            {
                ViewState["SESSION_SOLICITANTE"] = "Titular";
                ViewState["TIPO_SOLICITANTE"] = "Titular";
                ViewState["ap"] = "1";
            }
            else if (funciones.retornaPagina().Contains("agregarRepresentanteLegal"))
            {
                ViewState["SESSION_SOLICITANTE"] = "RepresentanteLegal";
                ViewState["TIPO_SOLICITANTE"] = "Representante Legal";
                ViewState["ap"] = "2";
            }
            else if (funciones.retornaPagina().Contains("agregarOperador"))
            {
                ViewState["SESSION_SOLICITANTE"] = "Operador";
                ViewState["TIPO_SOLICITANTE"] = "Operador";
                ViewState["ap"] = "3";
            }
        }

        private void InicializarFormulario()
        {
            CargarComboBox("TipoArchivo");
            TipoArchivo.SelectedValue = "-1";

            Solicitante solicitante = null;
            if (ViewState["ap"].ToString().Equals("1"))
            {
                solicitante = (Solicitante)Session["Titular"];

                if (solicitante != null)
                {
                    List<ArchivosAdjTitular> listaArchivoAdjuntoTitular = solicitanteDA.ListarArchivosAdjuntoTitular(solicitante.rut, 0);

                    solicitante.listaArchivosAdjTitular = listaArchivoAdjuntoTitular;
                    Session["Titular"] = solicitante;
                    ViewState["ArchivoBinario"] = solicitante.listaArchivosAdjTitular;

                    GridArchivoAdjunto.DataSource = solicitante.listaArchivosAdjTitular;
                }

            }
            else if (ViewState["ap"].ToString().Equals("2"))
            {
                RepLegal representanteLegal = (RepLegal)Session["RepresentanteLegal"];

                if (representanteLegal != null)
                {
                    solicitante = representanteLegal.representanteLegal;

                    List<ArchivosAdjRepLegal> listaArchivoAdjuntoRepresentante = repLegalDA.ListarArchivosAdjuntoPersona(solicitante.rut, 0);

                    solicitante.listaArchivosAdjRep = listaArchivoAdjuntoRepresentante;
                    Session["RepresentanteLegal"] = (RepLegal)representanteLegal;
                    ViewState["ArchivoBinario"] = solicitante.listaArchivosAdjRep;

                    GridArchivoAdjunto.DataSource = solicitante.listaArchivosAdjRep;
                }
            }
            else if (ViewState["ap"].ToString().Equals("3"))
            {

                Operador operador = (Operador)Session["Operador"];

                if (operador != null)
                {
                    solicitante = operador.operador;
                
                    List<ArchivosAdjOperador> listaArchivoAdjuntoOp = operadorDA.ListarArchivosAdjuntoPersona(solicitante.rut, 0);

                    solicitante.listaArchivosAdjOperador = operadorDA.ListarArchivosAdjuntoPersona(solicitante.rut, 0);
                    Session["Operador"] = operador;
                    ViewState["ArchivoBinario"] = solicitante.listaArchivosAdjOperador;

                    GridArchivoAdjunto.DataSource = solicitante.listaArchivosAdjOperador;
                }
            }

            GridArchivoAdjunto.DataBind();
        }

        private void CargarComboBox(String combobox)
        {
            switch (combobox)
            {

                case "TipoArchivo":
                    // Cargamos el combobox: TipoPersona
                    TipoArchivo.Items.Clear();
                    TipoArchivo.DataSource = tipoDa.ListarTipo("TIPO_ARCHIVO_TITULARES");
                    TipoArchivo.DataTextField = "descripcion";
                    TipoArchivo.DataValueField = "id";
                    TipoArchivo.DataBind();
                    TipoArchivo.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;
            }
        }

        protected void GridArchivoAdjunto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //ACCION
                HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
                if (hidden_accion != null && !hidden_accion.Value.Equals("") && (Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR || Convert.ToInt32(hidden_accion.Value) == accion.IGNORAR))
                {
                    e.Row.Attributes["style"] = "display:none";
                };

                //Descargar
                ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                if (boton_descargar != null)
                {
                    boton_descargar.Visible = true;
                };

                //Eliminar
                ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
                if (boton_borrar != null)
                {
                    boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar el Archivo Adjunto?')");
                    boton_borrar.Visible = true;

                }

            }
        }

        protected void GridArchivoAdjunto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int idArchivoAdjunto = Convert.ToInt32(arg[0]);
            int index = Convert.ToInt32(arg[1]);

            Solicitante solicitante = null;
            if (ViewState["ap"].ToString().Equals("1")) {
                solicitante = (Solicitante) Session["Titular"];
            }
            else if (ViewState["ap"].ToString().Equals("2")) {
                RepLegal representanteLegal = (RepLegal)Session["RepresentanteLegal"];
                solicitante = representanteLegal.representanteLegal;
            }
            else if (ViewState["ap"].ToString().Equals("3")) {
                Operador operador = (Operador)Session["Operador"];
                solicitante = operador.operador;    
            }

            switch (e.CommandName)
            {
                case "Desasociar":
                    
                    break;
                
                case "Asociar":
                    
                    break;
                
                case "Descargar":
                        /*
                        ArchivoBinarioEspecial archivoBinarioEspecial = null;
                        int i;

                        if (ViewState["ap"].ToString().Equals("1"))
                        {
                            List<ArchivosAdjTitular> listaArchivoBinarioEspecial = solicitante.listaArchivosAdjTitular;
                            i = 1;
                            foreach (ArchivosAdjTitular archivosAdjTitularSession in listaArchivoBinarioEspecial)
                            {
                                if (i == index)
                                {
                                    archivoBinarioEspecial = archivosAdjTitularSession.archivoBinario;
                                }
                                i++;
                            }

                        }
                        else if (ViewState["ap"].ToString().Equals("2"))
                        {
                            List<ArchivosAdjRepLegal> listaArchivoBinarioEspecial = solicitante.listaArchivosAdjRep;
                            i = 1;
                            foreach (ArchivosAdjRepLegal archivosAdjRepLegalSession in listaArchivoBinarioEspecial)
                            {
                                if (i == index)
                                {
                                    archivoBinarioEspecial = archivosAdjRepLegalSession.archivoBinario;
                                }
                                i++;
                            }
                        }
                        else if (ViewState["ap"].ToString().Equals("3"))
                        {
                            List<ArchivosAdjOperador> listaArchivoBinarioEspecial = solicitante.listaArchivosAdjOperador;
                            i = 1;
                            foreach (ArchivosAdjOperador archivosAdjOperadorSession in listaArchivoBinarioEspecial)
                            {
                                if (i == index)
                                {
                                    archivoBinarioEspecial = archivosAdjOperadorSession.archivoBinario;
                                }
                                i++;
                            }
                        }
                        */

                        ArchivoBinarioEspecial archivoBinarioEspecial = null;
                        if (ViewState["ap"].ToString().Equals("1")) //Titular
                        {
                            List<ArchivosAdjTitular> listaArchivoBinarioEspecial = solicitante.listaArchivosAdjTitular;
                            
                            foreach (ArchivosAdjTitular archivosAdjTitular in listaArchivoBinarioEspecial)
                            {
                                if (archivosAdjTitular.index.Equals(index))
                                {
                                    archivoBinarioEspecial = archivosAdjTitular.archivoBinario;
                                }
                            }
                            
                        }
                        else if (ViewState["ap"].ToString().Equals("2")) //Representante Legal
                        {
                            List<ArchivosAdjRepLegal> listaArchivoBinarioEspecial = solicitante.listaArchivosAdjRep;
                            
                            foreach (ArchivosAdjRepLegal archivosAdjRepLegal in listaArchivoBinarioEspecial)
                            {
                                if (archivosAdjRepLegal.index.Equals(index))
                                {
                                    archivoBinarioEspecial = archivosAdjRepLegal.archivoBinario;
                                }
                            }
                        }
                        else if (ViewState["ap"].ToString().Equals("3")) //Operador
                        {
                            List<ArchivosAdjOperador> listaArchivoBinarioEspecial = solicitante.listaArchivosAdjOperador;
                            foreach (ArchivosAdjOperador archivosAdjOperador in listaArchivoBinarioEspecial)
                            {
                                if (archivosAdjOperador.index.Equals(index))
                                {
                                    archivoBinarioEspecial = archivosAdjOperador.archivoBinario;
                                }
                            }
                        }

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = "application/" + archivoBinarioEspecial.formato;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinarioEspecial.nombreFisico + "." + archivoBinarioEspecial.formato);
                        Response.BinaryWrite(archivoBinarioEspecial.bytes);
                        Response.Flush();
                        Response.End();

                    break;

                case "Eliminar":

                    /*
                    if (ViewState["ap"].ToString().Equals("1")) //Titular
                    {
                        List<ArchivosAdjTitular> listaArchivoBinarioEspecial = solicitante.listaArchivosAdjTitular;
                        foreach (ArchivosAdjTitular archivosAdjTitularSession in listaArchivoBinarioEspecial)
                        {
                            if (archivosAdjTitularSession.index.Equals(Convert.ToInt32(index)))
                            {
                                listaArchivoBinarioEspecial.Remove(archivosAdjTitularSession);
                                break;
                            }
                        }
                        GridArchivoAdjunto.DataSource = listaArchivoBinarioEspecial;
                        GridArchivoAdjunto.DataBind();

                        solicitante.listaArchivosAdjTitular = listaArchivoBinarioEspecial;
                        ViewState["ArchivoBinario"] = listaArchivoBinarioEspecial;

                        Session["Titular"] = (Solicitante)solicitante;

                    }
                    else if (ViewState["ap"].ToString().Equals("2")) //Representante Legal
                    {
                        List<ArchivosAdjRepLegal> listaArchivoBinarioEspecial = solicitante.listaArchivosAdjRep;
                        foreach (ArchivosAdjRepLegal archivosAdjRepLegalSession in listaArchivoBinarioEspecial)
                        {
                            if (archivosAdjRepLegalSession.index.Equals(Convert.ToInt32(index)))
                            {
                                listaArchivoBinarioEspecial.Remove(archivosAdjRepLegalSession);
                                break;
                            }
                        }
                        GridArchivoAdjunto.DataSource = listaArchivoBinarioEspecial;
                        GridArchivoAdjunto.DataBind();

                        solicitante.listaArchivosAdjRep = listaArchivoBinarioEspecial;
                        ViewState["ArchivoBinario"] = listaArchivoBinarioEspecial;

                        RepLegal representanteLegal = (RepLegal)Session["RepresentanteLegal"];
                        representanteLegal.representanteLegal = solicitante;
                        Session["RepresentanteLegal"] = (RepLegal)representanteLegal;

                    }
                    else if (ViewState["ap"].ToString().Equals("3")) //Operador
                    {
                        List<ArchivosAdjOperador> listaArchivoBinarioEspecial = solicitante.listaArchivosAdjOperador;
                        foreach (ArchivosAdjOperador archivosAdjOperadorSession in listaArchivoBinarioEspecial)
                        {
                            if (archivosAdjOperadorSession.index.Equals(Convert.ToInt32(index)))
                            {
                                listaArchivoBinarioEspecial.Remove(archivosAdjOperadorSession);
                                break;
                            }
                        }
                        GridArchivoAdjunto.DataSource = listaArchivoBinarioEspecial;
                        GridArchivoAdjunto.DataBind();

                        solicitante.listaArchivosAdjOperador = listaArchivoBinarioEspecial;
                        ViewState["ArchivoBinario"] = listaArchivoBinarioEspecial;

                        Operador operador = (Operador)Session["Operador"];
                        operador.operador = solicitante;
                        Session["Operador"] = (Operador)operador;
                    }

                    msgGrilla.Text = "Se ha eliminado exitosamente el archivo adjunto";
                    Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Content_msgGrilla.Visible = true;

                    limpiarArchivoAdjunto();
                    */

                    if (ViewState["ap"].ToString().Equals("1")) //Titular
                    {
                        GridArchivoAdjunto.EditIndex = -1;
                        EliminarGrillaTitular(index, "ArchivoAdjunto", solicitante);
                        break;
                    }
                    else if (ViewState["ap"].ToString().Equals("2")) //Representante Legal
                    {
                        GridArchivoAdjunto.EditIndex = -1;
                        EliminarGrillaRepresentanteLegal(index, "ArchivoAdjunto", solicitante);
                        break;
                    }
                    else if (ViewState["ap"].ToString().Equals("3")) //Operador
                    {
                        GridArchivoAdjunto.EditIndex = -1;
                        EliminarGrillaOperador(index, "ArchivoAdjunto", solicitante);
                        break;
                    }
                    break;
            }
        }

        private void EliminarGrillaTitular(int index, string tipoGrilla, Solicitante solicitante)
        {
            switch (tipoGrilla)
            {
                case "ArchivoAdjunto":

                    List<ArchivosAdjTitular> List_ArchivosAdjTitular = solicitante.listaArchivosAdjTitular;

                    foreach (ArchivosAdjTitular archivosAdjTitular in List_ArchivosAdjTitular)
                    {
                        if (archivosAdjTitular.index.Equals(index))
                        {
                            List<String> listaErroresEspAu = mantenedorTitularesValidacion.validaEliminacionArchivoAdjuntoTitular(archivosAdjTitular, List_ArchivosAdjTitular);

                            if (listaErroresEspAu.Count <= 0)
                            {
                                if (archivosAdjTitular.accion == accion.INGRESAR)
                                {
                                    archivosAdjTitular.accion = accion.IGNORAR;
                                    GridArchivoAdjunto.Rows[index].Attributes["style"] = "display:none";
                                }
                                if (archivosAdjTitular.accion == accion.LISTADO)
                                {
                                    archivosAdjTitular.accion = accion.ELIMINAR;
                                    GridArchivoAdjunto.Rows[index].Attributes["style"] = "display:none";
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

                    solicitante.listaArchivosAdjTitular = List_ArchivosAdjTitular;
                    Session["Titular"] = (Solicitante) solicitante;

                    cargarGrillaArchivoAdjTitular(List_ArchivosAdjTitular);
                    
                    break;
            }

        }

        private void EliminarGrillaRepresentanteLegal(int index, string tipoGrilla, Solicitante solicitante)
        {
            switch (tipoGrilla)
            {
                case "ArchivoAdjunto":

                    List<ArchivosAdjRepLegal> List_ArchivosAdjRepLegal = solicitante.listaArchivosAdjRep;

                    foreach (ArchivosAdjRepLegal archivosAdjRepLegal in List_ArchivosAdjRepLegal)
                    {
                        if (archivosAdjRepLegal.index.Equals(index))
                        {

                            List<String> listaErroresEspAu = mantenedorTitularesValidacion.validaEliminacionArchivoAdjuntoRepresentanteLegal(archivosAdjRepLegal, List_ArchivosAdjRepLegal);

                            if (listaErroresEspAu.Count <= 0)
                            {
                                if (archivosAdjRepLegal.accion == accion.INGRESAR)
                                {
                                    archivosAdjRepLegal.accion = accion.IGNORAR;
                                    GridArchivoAdjunto.Rows[index].Attributes["style"] = "display:none";
                                }
                                if (archivosAdjRepLegal.accion == accion.LISTADO)
                                {
                                    archivosAdjRepLegal.accion = accion.ELIMINAR;
                                    GridArchivoAdjunto.Rows[index].Attributes["style"] = "display:none";
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

                    solicitante.listaArchivosAdjRep = List_ArchivosAdjRepLegal;
                    Session["Titular"] = (Solicitante)solicitante;

                    cargarGrillaArchivoAdjRepLegal(List_ArchivosAdjRepLegal);

                    break;
            }

        }

        private void EliminarGrillaOperador(int index, string tipoGrilla, Solicitante solicitante)
        {
            switch (tipoGrilla)
            {
                case "ArchivoAdjunto":

                    List<ArchivosAdjOperador> List_ArchivosAdjOperador = solicitante.listaArchivosAdjOperador;

                    foreach (ArchivosAdjOperador archivosAdjOperador in List_ArchivosAdjOperador)
                    {
                        if (archivosAdjOperador.index.Equals(index))
                        {

                            List<String> listaErroresEspAu = mantenedorTitularesValidacion.validaEliminacionArchivoAdjuntoOperador(archivosAdjOperador, List_ArchivosAdjOperador);

                            if (listaErroresEspAu.Count <= 0)
                            {
                                if (archivosAdjOperador.accion == accion.INGRESAR)
                                {
                                    archivosAdjOperador.accion = accion.IGNORAR;
                                    GridArchivoAdjunto.Rows[index].Attributes["style"] = "display:none";
                                }
                                if (archivosAdjOperador.accion == accion.LISTADO)
                                {
                                    archivosAdjOperador.accion = accion.ELIMINAR;
                                    GridArchivoAdjunto.Rows[index].Attributes["style"] = "display:none";
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

                    solicitante.listaArchivosAdjOperador = List_ArchivosAdjOperador;
                    Session["Titular"] = (Solicitante)solicitante;

                    cargarGrillaArchivoAdjOperador(List_ArchivosAdjOperador);

                    break;
            }

        }

        private void cargarGrillaArchivoAdjOperador(List<ArchivosAdjOperador> List_ArchivosAdjOperador)
        {
            GridArchivoAdjunto.DataSource = List_ArchivosAdjOperador;
            GridArchivoAdjunto.DataBind();
            GridArchivoAdjunto.Visible = true;
            UpdatePanelArchivoAdjuntoGrilla.Update();
        }
        
        private void cargarGrillaArchivoAdjRepLegal(List<ArchivosAdjRepLegal> List_ArchivosAdjRepLegal)
        {
            GridArchivoAdjunto.DataSource = List_ArchivosAdjRepLegal;
            GridArchivoAdjunto.DataBind();
            GridArchivoAdjunto.Visible = true;
            UpdatePanelArchivoAdjuntoGrilla.Update();
        }

        private void cargarGrillaArchivoAdjTitular(List<ArchivosAdjTitular> List_ArchivosAdjTitular)
        {
            GridArchivoAdjunto.DataSource = List_ArchivosAdjTitular;
            GridArchivoAdjunto.DataBind();
            GridArchivoAdjunto.Visible = true;
            UpdatePanelArchivoAdjuntoGrilla.Update();

        }

        protected void GridArchivoAdjunto_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridArchivosAdjuntosAntEspaciales = (GridView)sender;


                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Lista Archivo Adjunto";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridArchivoAdjunto.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        protected void GuardarArchivoAdjunto_Click(object sender, ImageClickEventArgs e)
        {

            msgGrilla.Text = "";
            Content_msgGrilla.Visible = false;

            if (ViewState["ap"].ToString().Equals("1"))
            {
                agregarGrillaArchivoAdjuntoTitular();
            }
            else if (ViewState["ap"].ToString().Equals("2")) {

                agregarGrillaArchivoAdjuntoRepresentantesLegales();
            }
            else if (ViewState["ap"].ToString().Equals("3")) {

                agregarGrillaArchivoAdjuntoOperador();
            }
        }

        private void agregarGrillaArchivoAdjuntoOperador()
        {

            List<ArchivosAdjOperador> List_ArchivoBinario = (List<ArchivosAdjOperador>)ViewState["ArchivoBinario"];
            Operador operador = (Operador)Session["Operador"];

            if (operador == null)
            {
                operador = new Operador();
                operador.operador = new Solicitante();
            }

            int index = 0;
            if (List_ArchivoBinario == null)
            {
                List_ArchivoBinario = new List<ArchivosAdjOperador>();
            }
            else {
                index = List_ArchivoBinario.Count;
            }

            ArchivosAdjOperador archivosAdjOperador = new ArchivosAdjOperador();
            archivosAdjOperador.tipoDocumento = new ParametroGenerico(Convert.ToInt32(TipoArchivo.SelectedValue), TipoArchivo.SelectedItem.Text);
            archivosAdjOperador.accion = accion.INGRESAR;
            archivosAdjOperador.index = Convert.ToInt32(index);
            archivosAdjOperador.rutOperador = operador.operador.rut;

            if (NroControlIngresoArchivoAdj.Text != null && !NroControlIngresoArchivoAdj.Text.Equals(""))
            {
                archivosAdjOperador.numCI = Convert.ToInt32(NroControlIngresoArchivoAdj.Text);
            }

            if (FechaTextRecepcionArchAdj.Text != null && !FechaTextRecepcionArchAdj.Text.Equals(""))
            {
                archivosAdjOperador.fechaCI = Convert.ToDateTime(FechaTextRecepcionArchAdj.Text);
            }

            ArchivoBinarioEspecial archivoBinarioEspecial = new ArchivoBinarioEspecial();
            archivoBinarioEspecial.nombreArchivo = NombreArchivo.Text;
            
            if (ArchivoAdjunto.HasFile)
            {

                archivoBinarioEspecial.nombreFisico = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
                archivoBinarioEspecial.nombreArchivo = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
                archivoBinarioEspecial.formato = ArchivoAdjunto.PostedFile.FileName.Substring(ArchivoAdjunto.PostedFile.FileName.LastIndexOf(".") + 1).ToLower();
                archivoBinarioEspecial.tamano = ArchivoAdjunto.PostedFile.InputStream.Length;
                archivoBinarioEspecial.bytes = ArchivoAdjunto.FileBytes;
            }
            archivosAdjOperador.archivoBinario = archivoBinarioEspecial;

            List<String> listaErroresArchivoAdjunto = mantenedorTitularesValidacion.validaArchivoAdjuntoOperador(archivosAdjOperador, List_ArchivoBinario);

            if (listaErroresArchivoAdjunto.Count <= 0)
            {

                List_ArchivoBinario.Add(archivosAdjOperador);

                GridArchivoAdjunto.DataSource = List_ArchivoBinario;
                GridArchivoAdjunto.DataBind();
                GridArchivoAdjunto.Visible = true;

                ViewState["ArchivoBinario"] = (List<ArchivosAdjOperador>)List_ArchivoBinario;

                Solicitante solicitante = operador.operador;
                solicitante.listaArchivosAdjOperador = List_ArchivoBinario;

                Session["Operador"] = (Operador) operador;

                msgGrilla.Text = "Se ha guardado exitosamente el archivo adjunto al " + ViewState["TIPO_SOLICITANTE"].ToString() + ".";
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                Content_msgGrilla.Visible = true;

                limpiarArchivoAdjunto();

            }
            else
            {
                foreach (String error in listaErroresArchivoAdjunto)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
            }
        }

        private void agregarGrillaArchivoAdjuntoRepresentantesLegales()
        {

            List<ArchivosAdjRepLegal> List_ArchivoBinario = (List<ArchivosAdjRepLegal>)ViewState["ArchivoBinario"];
            RepLegal repLegal = (RepLegal)Session["RepresentanteLegal"];
            if (repLegal == null)
            {
                repLegal = new RepLegal();
                repLegal.representanteLegal = new Solicitante();
            }

            int index = 0;
            if (List_ArchivoBinario == null)
            {
                List_ArchivoBinario = new List<ArchivosAdjRepLegal>();
            }
            else {
                index = List_ArchivoBinario.Count;
            }

            ArchivosAdjRepLegal archivosAdjRepLegal = new ArchivosAdjRepLegal();
            archivosAdjRepLegal.tipoDocumento = new ParametroGenerico(Convert.ToInt32(TipoArchivo.SelectedValue), TipoArchivo.SelectedItem.Text);
            archivosAdjRepLegal.accion = accion.INGRESAR;
            archivosAdjRepLegal.index = Convert.ToInt32(index);
            archivosAdjRepLegal.rutRepLegal = repLegal.representanteLegal.rut;

            if (NroControlIngresoArchivoAdj.Text != null && !NroControlIngresoArchivoAdj.Text.Equals(""))
            {
                archivosAdjRepLegal.numCI = Convert.ToInt32(NroControlIngresoArchivoAdj.Text);
            }

            if (FechaTextRecepcionArchAdj.Text != null && !FechaTextRecepcionArchAdj.Text.Equals(""))
            {
                archivosAdjRepLegal.fechaCI = Convert.ToDateTime(FechaTextRecepcionArchAdj.Text);
            }

            ArchivoBinarioEspecial archivoBinarioEspecial = new ArchivoBinarioEspecial();
            archivoBinarioEspecial.tipoArchivo = new ParametroGenerico(Convert.ToInt32(TipoArchivo.SelectedValue), TipoArchivo.SelectedItem.Text);
            archivoBinarioEspecial.nombreArchivo = NombreArchivo.Text;

            if (ArchivoAdjunto.HasFile)
            {

                archivoBinarioEspecial.nombreFisico = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
                archivoBinarioEspecial.nombreArchivo = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
                archivoBinarioEspecial.formato = ArchivoAdjunto.PostedFile.FileName.Substring(ArchivoAdjunto.PostedFile.FileName.LastIndexOf(".") + 1).ToLower();
                archivoBinarioEspecial.tamano = ArchivoAdjunto.PostedFile.InputStream.Length;
                archivoBinarioEspecial.bytes = ArchivoAdjunto.FileBytes;
            }
            archivosAdjRepLegal.archivoBinario = archivoBinarioEspecial;

            List<String> listaErroresArchivoAdjunto = mantenedorTitularesValidacion.validaArchivoAdjuntoRepresentanteLegal(archivosAdjRepLegal, List_ArchivoBinario);

            if (listaErroresArchivoAdjunto.Count <= 0)
            {

                List_ArchivoBinario.Add(archivosAdjRepLegal);

                GridArchivoAdjunto.DataSource = List_ArchivoBinario;
                GridArchivoAdjunto.DataBind();
                GridArchivoAdjunto.Visible = true;

                ViewState["ArchivoBinario"] = (List<ArchivosAdjRepLegal>)List_ArchivoBinario;

                Solicitante solicitante = repLegal.representanteLegal;
                solicitante.listaArchivosAdjRep = List_ArchivoBinario;

                Session["RepresentanteLegal"] = repLegal;

                msgGrilla.Text = "Se ha guardado exitosamente el archivo adjunto al " + ViewState["TIPO_SOLICITANTE"].ToString() + ".";
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                Content_msgGrilla.Visible = true;

                limpiarArchivoAdjunto();

            }
            else
            {
                foreach (String error in listaErroresArchivoAdjunto)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
            }
        }

        private void agregarGrillaArchivoAdjuntoTitular()
        {

            List<ArchivosAdjTitular> List_ArchivoBinario = (List<ArchivosAdjTitular>)ViewState["ArchivoBinario"];
            Solicitante solicitante = (Solicitante)Session["Titular"];

            if (solicitante == null)
            {
                solicitante = new Solicitante();
            }

            int index = 0;
            if (List_ArchivoBinario == null)
            {
                List_ArchivoBinario = new List<ArchivosAdjTitular>();
            }
            else {
                index = List_ArchivoBinario.Count;
            }

            ArchivosAdjTitular archivosAdjTitular = new ArchivosAdjTitular();
            archivosAdjTitular.tipoDocumento = new ParametroGenerico(Convert.ToInt32(TipoArchivo.SelectedValue), TipoArchivo.SelectedItem.Text);
            archivosAdjTitular.accion = accion.INGRESAR;
            archivosAdjTitular.index = Convert.ToInt32(index);
            archivosAdjTitular.rutPersona = solicitante.rut;

            if (NroControlIngresoArchivoAdj.Text != null && !NroControlIngresoArchivoAdj.Text.Equals("")) {
                archivosAdjTitular.numCI = Convert.ToInt32(NroControlIngresoArchivoAdj.Text);
            }

            if (FechaTextRecepcionArchAdj.Text != null && !FechaTextRecepcionArchAdj.Text.Equals("")) {
                archivosAdjTitular.fechaCI = Convert.ToDateTime(FechaTextRecepcionArchAdj.Text);
            }

            archivosAdjTitular.estadoVigencia = new ParametroGenerico() { id = rbEstadosGenerales.VIGENTE, descripcion = "Vigente" };
            

            ArchivoBinarioEspecial archivoBinarioEspecial = new ArchivoBinarioEspecial();
            archivoBinarioEspecial.nombreArchivo = NombreArchivo.Text;
            
            if (ArchivoAdjunto.HasFile)
            {

                archivoBinarioEspecial.nombreFisico = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
                archivoBinarioEspecial.nombreArchivo = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
                archivoBinarioEspecial.formato = ArchivoAdjunto.PostedFile.FileName.Substring(ArchivoAdjunto.PostedFile.FileName.LastIndexOf(".") + 1).ToLower();
                archivoBinarioEspecial.tamano = ArchivoAdjunto.PostedFile.InputStream.Length;
                archivoBinarioEspecial.bytes = ArchivoAdjunto.FileBytes;
            }
            archivosAdjTitular.archivoBinario = archivoBinarioEspecial;

            List<String> listaErroresArchivoAdjunto = mantenedorTitularesValidacion.validaArchivoAdjuntoTitular(archivosAdjTitular, List_ArchivoBinario);

            if (listaErroresArchivoAdjunto.Count <= 0)
            {

                if (List_ArchivoBinario != null) { 
                    foreach(ArchivosAdjTitular archivo in List_ArchivoBinario){
                        if(archivo.tipoDocumento  != null && archivo.tipoDocumento.id == archivosAdjTitular.tipoDocumento.id)
                        {

                            if (archivo.accion == accion.LISTADO || archivo.accion == accion.MODIFICAR){
                                archivo.accion = accion.MODIFICAR;
                            }else{
                                archivo.accion = accion.INGRESAR;
                            }

                            archivo.estadoVigencia = new ParametroGenerico() { id = rbEstadosGenerales.NO_VIGENTE, descripcion = "No Vigente" };
                        }
                    }
                }


                List_ArchivoBinario.Add(archivosAdjTitular);

                GridArchivoAdjunto.DataSource = List_ArchivoBinario;
                GridArchivoAdjunto.DataBind();
                GridArchivoAdjunto.Visible = true;

                ViewState["ArchivoBinario"] = (List<ArchivosAdjTitular>)List_ArchivoBinario;

                solicitante.listaArchivosAdjTitular = (List<ArchivosAdjTitular>) List_ArchivoBinario;

                Session["Titular"] = solicitante;

                msgGrilla.Text = "Se ha guardado exitosamente el archivo adjunto al " + ViewState["TIPO_SOLICITANTE"].ToString() + ".";
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                Content_msgGrilla.Visible = true;

                limpiarArchivoAdjunto();

                UpdatePanelArchivoAdjuntoGrilla.Update(); 

            }
            else
            {
                foreach (String error in listaErroresArchivoAdjunto)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
            }

        }

        private void limpiarArchivoAdjunto()
        {
            TipoArchivo.SelectedValue = "-1";
            NombreArchivo.Text = "";
            NroControlIngresoArchivoAdj.Text = "";
            FechaTextRecepcionArchAdj.Text = "";
        }

        protected void ImgAdd_PreRender(object sender, EventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            ScriptManager sc = ScriptManager.GetCurrent(this.Page);
            sc.RegisterPostBackControl(btn);
        }
        
    }
}