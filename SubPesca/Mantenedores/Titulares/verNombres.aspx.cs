using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using System.Data;
using Datos.Entidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace SubPesca.Mantenedores.Titulares
{
    public partial class verNombres : System.Web.UI.Page
    {
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();
        
        SolicitanteDA solicitanteDA = new SolicitanteDA();
        OperadorDA operadorDA = new OperadorDA();
        RepLegalDA representanteDA = new RepLegalDA();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Obtención de parámetros mediante GET/POST
                ObtencionParametros();
                
                //Cargamos las grillas
                CargarGrilla("Nombres");
            }
        }

        private void ObtencionParametros()
        {
            // Se recibe el rutPersona
            try
            {
                if (Request.QueryString["rutPersona"] != null && Request.QueryString["bp"] != null)
                {
                    RutPersona.Value = Convert.ToString(Request.QueryString["rutPersona"]);
                    Bp.Value = Convert.ToString(Request.QueryString["bp"]);
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
         * Método que carga la grilla en el pop-up de Nombres.
         */ 
        private void CargarGrilla(string grilla)
        {
            SolicitanteService solicitanteService = new SolicitanteService();
            Solicitante solicitante = new Solicitante();
            DataTable dt = new DataTable();

            switch (grilla)
            {
                case "Nombres":

                    String rutCompleto = Convert.ToString(RutPersona.Value);
                    String[] rutPartes = rutCompleto.Split('-');
                    solicitante.rut = Convert.ToInt32(rutPartes[0]);

                    if (Bp != null && Bp.Value.Equals("1")) // Titular
                    {
                        GridNombres.DataSource = solicitanteDA.ListarNombrePersona(solicitante.rut,0);
                    }
                    else if (Bp != null && Bp.Value.Equals("2")) // Representante Legal
                    {
                        GridNombres.DataSource = representanteDA.ListarNombreRepresentante(solicitante.rut, 0);
                    }

                    else if (Bp != null && Bp.Value.Equals("3")) // Operador
                    {
                        GridNombres.DataSource = operadorDA.ListarNombrePersona(solicitante.rut, 0);
                    }

                    GridNombres.DataBind();
                    break;
            }
        }

        /**
        * Método que agrega al evento para cambiar de página los resultados de la grilla de 
        * Nombres de un Titular
        */
        protected void GridNombres_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridNombres.PageIndex = e.NewPageIndex;
            GridNombres.DataBind();
            CargarGrilla("Nombres");
        }

        protected void GridNombres_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Descargar":
                    int idArchivo = Convert.ToInt32(e.CommandArgument);
                    ArchivoBinario archivoBinario = archivoBinarioSolicitudDA.ObtenerArchivoBinarioSolicitud(idArchivo);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = "application/" + archivoBinario.formato;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinario.nombreArchivo + "." + archivoBinario.formato);
                    Response.BinaryWrite(archivoBinario.bytes);
                    Response.Flush();
                    Response.End();

                    break;
            };
        }

        protected void GridNombres_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Descargar
                String idArchivo = DataBinder.Eval(e.Row.DataItem, "idArchivoBinario").ToString();
                ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                if (boton_descargar != null && idArchivo != null && !idArchivo.Equals("") && Convert.ToInt32(idArchivo) > 0)
                {
                    boton_descargar.Visible = true;
                };

            }
        }

        protected void ImgAdd_PreRender(object sender, EventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            ScriptManager sc = ScriptManager.GetCurrent(this.Page);
            sc.RegisterPostBackControl(btn);
        }
    }
}