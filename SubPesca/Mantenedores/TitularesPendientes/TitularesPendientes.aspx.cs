using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using Datos.Contantes;

namespace SubPesca.Solicitudes.TitularesPendientes
{
    public partial class TitularesPendientes : System.Web.UI.Page
    {
        TitularRegConcesionesDA TitularDA = new TitularRegConcesionesDA();
        PermisosService permisosService = new PermisosService();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                Datos.Entidades.Usuario.Serializable usuario_logeado = (Datos.Entidades.Usuario.Serializable)HttpContext.Current.Session["Usuario"];

                if (!permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.TitularesPendientes }, usuario_logeado, null, rbAccion.EDITAR))
                {
                    Response.Redirect("~/Administrador/principal.aspx");
                }
                CargarGrilla();
            }
            
        }

        private void CargarGrilla() 
        { 
            //cargar grilla de la tabla titulares faltantes
            List<TitularRegConcesiones> ListaTitulares = new List<TitularRegConcesiones>();

            ListaTitulares = TitularDA.ListarTitularesRegConcesiones();

            if (ListaTitulares != null && ListaTitulares.Count > 0)
            {
                ExportarGrilla.Visible = true;
            }
            else {
                ExportarGrilla.Visible = false;
                msgGrilla.Text = "No existen titulares pendientes de creación.";
                Content_msgGrilla.Visible = true;

            }

            GridTitularesPendientes.DataSource = ListaTitulares;
            GridTitularesPendientes.DataBind();
        }

        protected void GridTitularesPendientes_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridTitularesPendientes.PageIndex = e.NewPageIndex;
            GridTitularesPendientes.DataBind();
            CargarGrilla();
        }

        protected void GridTitularesPendientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton boton_solucionar = (ImageButton)e.Row.FindControl("gSolucionar");
                if (boton_solucionar != null)
                {
                    boton_solucionar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Esta seguro que soluciono este conflicto?')");
                    boton_solucionar.Visible = true;
                }
            }
        }
        
        protected void GridTitularesPendientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id;
            switch (e.CommandName)
            {
                case "Solucionar":
                    id = Convert.ToInt32(e.CommandArgument);
                    GridTitularesPendientes_Corregir(id);
                    break;
            }
        }

        protected void GridTitularesPendientes_Corregir(int id)
        { 
            //llamar al procedimiento para realizar el cambio de estado en la tabla de conflictos.
            if (TitularDA.ActualizarOperadorEstado(id, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario, 81))
            {
                msgGrilla.Text = "Se ha realizado el cambio correctamente.";
                Content_msgGrilla.Visible = true;

                this.CargarGrilla();
            }
            else 
            {
                msgGrilla.Text = "Error no se ha podido realizar el cambio.";
                Content_msgGrilla.Visible = true;

                this.CargarGrilla();
            }
            //CargarGrilla();
        }

        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();
            string nom_grilla = "TitularesPendientes";
            string ngrilla = "";

            CargarGrilla();

            switch (nom_grilla)
            {
                case "TitularesPendientes":
                    //GridTitularesPendientes.Columns.RemoveAt(6);
                    grilla = GridTitularesPendientes;
                    ngrilla = "TitularesPendientes.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }
    }
}