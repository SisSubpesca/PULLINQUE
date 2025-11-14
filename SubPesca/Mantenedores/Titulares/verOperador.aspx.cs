using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using Datos.Entidades;
using System.Data;
using System.Collections;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace SubPesca.Mantenedores.Titulares
{
    public partial class verOperador : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();
        OperadorDA operadorDA = new OperadorDA();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                // Obtención de parámetros mediante GET/POST
                ObtencionParametros();

                //Cargamos las grillas
                CargarGrilla("ContactoMatrizSucursales");
                CargarGrilla("Contactos");
                CargarGrilla("ArchivosAdjuntos");
            }
        }

        /**
         * Método que recupera el dato enviado a través de get/post a esta 
         * pagina con la información del detalle del titular.
         */
        protected void ObtencionParametros()
        {

            SolicitanteService solicitanteService = new SolicitanteService();
            Operador operador = new Operador();
            operador.operador = new Solicitante();

            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

            // Se recibe el rutPersona
            try
            {
                if (Request.QueryString["rutPersona"] != null)
                {
                    operador.operador.rut = Convert.ToInt32(Request.QueryString["rutPersona"]);
                    operador = operadorDA.ObtenerOperador(operador.operador.rut,null);

                    RutPersona.Text = Convert.ToString(operador.operador.rut);
                    DVPersona.Text = Convert.ToString(operador.operador.dv);
                    NombreSolicitante.Text = Convert.ToString(operador.operador.nombreSolicitante);

                    //if (operador.operador != null && operador.operador.numeroControlIngreso > 0)
                    //{
                    //    NumeroCI.Text = Convert.ToString(operador.operador.numeroControlIngreso);
                    //    FechaCI.Text = Convert.ToString(operador.operador.fechaControlIngreso);
                    //    PanelDatosModificacion.Visible = true;
                    //}

                    Session["Operador"] = (Operador)operador;
                }
                else
                {

                }

            }
            catch
            {

            };

        }

        /**
         * Método que da la funcionalidad al boton volver presente
         * en la página.
         */
        protected void Volver_Click(object sender, EventArgs e)
        {
            string path = "~/Mantenedores/Titulares/administrarOperadores.aspx";
            Hashtable HT_ModSolicitantes = (Hashtable)Session["Modulo_Solicitantes"];
            Session["Modulo_Solicitantes"] = (Hashtable)HT_ModSolicitantes;
            Response.Redirect(path);
        }

        /**
         * Método que carga las grillas del detalle del titular.
         * Contacto Matriz y Sucursales, Representantes Legales y Archivos Adjuntos.
         */
        protected void CargarGrilla(string grilla)
        {
            Operador operador = new Operador();
            operador.operador = new Solicitante();

            switch (grilla)
            {
                case "ContactoMatrizSucursales":
                    
                    operador.operador.rut = Convert.ToInt32(RutPersona.Text);

                    List<MatrizSucursal> listaMatrizSucursal = operadorDA.ListarOperadorMatrizSuc(operador.operador.rut);

                    GridContactoMatrizSucursales.DataSource = listaMatrizSucursal;
                    GridContactoMatrizSucursales.DataBind();

                    operador.operador.matrizSucursales = listaMatrizSucursal;
                    Session["Operador"] = (Operador)operador;
                    
                    break;

                case "Contactos":

                    operador.operador.rut = Convert.ToInt32(RutPersona.Text);

                    List<Contacto> listaContactos = operadorDA.ListarContactoOperador(operador.operador.rut,0);

                    GridViewContacto.DataSource = listaContactos;
                    GridViewContacto.DataBind();

                    operador.operador.listaContacto = listaContactos;
                    Session["Operador"] = (Operador)operador;

                    break;

                case "ArchivosAdjuntos":
                    
                    operador.operador.rut = Convert.ToInt32(RutPersona.Text);

                    List<ArchivosAdjOperador> listaArchivoAdjuntoOperador = operadorDA.ListarArchivosAdjuntoPersona(operador.operador.rut, 0);

                    GridArchivosAdjuntos.DataSource = listaArchivoAdjuntoOperador;
                    GridArchivosAdjuntos.DataBind();

                    operador.operador.listaArchivosAdjOperador = listaArchivoAdjuntoOperador;
                    Session["Operador"] = (Operador)operador;

                    break;
            };
        }

        protected void GridArchivosAdjuntos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Descargar
                String idArchivo = DataBinder.Eval(e.Row.DataItem, "idArchivo").ToString();
                ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                if (boton_descargar != null && idArchivo != null && !idArchivo.Equals("") && Convert.ToInt32(idArchivo) > 0)
                {
                    boton_descargar.Visible = true;
                };

            }
        }

        protected void GridArchivosAdjuntos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Descargar":
                    int idArchivo = Convert.ToInt32(e.CommandArgument);
                    ArchivoBinario archivoBinario = archivoBinarioSolicitudDA.ObtenerArchivoBinarioSolicitud(idArchivo);

                    if (archivoBinario != null)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = "application/" + archivoBinario.formato;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinario.nombreArchivo + "." + archivoBinario.formato);
                        Response.BinaryWrite(archivoBinario.bytes);
                        Response.Flush();
                        Response.End();
                    }

                    break;
            };
        }

        /**
         *  Método que crea el header de la grilla de Contactos Matriz y Sucursales
         */
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

        /**
        *  Método que crea el header de la grilla de Contactos Matriz y Sucursales
        */
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
                GridContacto.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        /**
         * Método que agrega al evento para cambiar de página los resultados de la grilla de 
         * Contactos Matriz y Sucursales
         */
        protected void GridContactoMatrizSucursales_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GridViewContacto_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GridContactoMatrizSucursales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');
            
            Solicitante solicitante = (Solicitante)Session["Titular"];

            switch (e.CommandName)
            {
                case "Ver":

                    int idMatrizSuc = Convert.ToInt32(arg[0]);
                    int index = Convert.ToInt32(arg[1]);

                    Response.Redirect("~/Mantenedores/Titulares/verContactoOperador.aspx?idMatrizSuc=" + idMatrizSuc + "&index=" + index + "&acc=5");

                    break;

                case "Descargar":

                    int idArchivoBinario = Convert.ToInt32(arg[0]);

                    ArchivoBinario archivoBinarioEspecial = archivoBinarioSolicitudDA.ObtenerArchivoBinarioSolicitud(idArchivoBinario);

                    if (archivoBinarioEspecial != null)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = "application/" + archivoBinarioEspecial.formato;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinarioEspecial.nombreArchivo + "." + archivoBinarioEspecial.formato);
                        Response.BinaryWrite(archivoBinarioEspecial.bytes);
                        Response.Flush();
                        Response.End();
                    }

                    break;
            }
        }

        protected void GridContactoMatrizSucursales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Contactos
                ImageButton boton_contactos = (ImageButton)e.Row.FindControl("gContactos");
                if (boton_contactos != null)
                {
                    boton_contactos.Visible = true;
                    boton_contactos.Attributes.Add("onclick", "javascript:abre_dialogo('verContactoOperador', '" + DataBinder.Eval(e.Row.DataItem, "idMatrizSuc") + "')");
                }

                //Descargar
                String idArchivo = DataBinder.Eval(e.Row.DataItem, "idArchivoBinario").ToString();
                ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                if (boton_descargar != null && idArchivo != null && !idArchivo.Equals("") && Convert.ToInt32(idArchivo) > 0)
                {
                    boton_descargar.Visible = true;
                };

            }
        }

        /**
         * Método que crea el header de la grilla de Archivos Adjuntos
         */
        protected void GridArchivosAdjuntos_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridArchivosAdjuntos = (GridView)sender;

                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Lista de Archivos Adjuntos";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridArchivosAdjuntos.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        /**
        * Método que agrega al evento para cambiar de página los resultados de la grilla de 
        * Archivos Adjuntos
        */
        protected void GridArchivosAdjuntos_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

        }

        protected void ImgAdd_PreRender(object sender, EventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            ScriptManager sc = ScriptManager.GetCurrent(this.Page);
            sc.RegisterPostBackControl(btn);
        }
    }
}