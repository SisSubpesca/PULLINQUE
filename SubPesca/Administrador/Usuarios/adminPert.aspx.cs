using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.usuario;
using LogicaNegocio.cl.subpesca.rb.servicios.usuario;
using Datos.Entidades;
using System.Collections.Generic;



namespace SubPesca.Administrador.Usuarios
{
    public partial class adminPert : System.Web.UI.Page
    {

        Datos.Entidades.Usuario usuarios = new Datos.Entidades.Usuario();
        TipoDA tipoDA = new TipoDA();
        UsuarioService usuarioService = new UsuarioService();


       
        // PAGE_LOAD
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                IdUSuarioCampo.Value = "0";

                // Se recibe el id_usuario
                try
                {

                    String idUsuario = "";

                    if (Request.QueryString["id_usuario"] != null)
                    {
                        idUsuario = Request.QueryString["id_usuario"];
                        IdUSuarioCampo.Value = idUsuario;
                        // Cargamos la grilla
                        CargaGrilla();

                    }else
                    {
                        Response.Redirect("~/Administrador/Usuarios/listUsuarios.aspx");
                    };
                }
                catch(Exception)
                {
                    Response.Redirect("~/Administrador/Usuarios/listUsuarios.aspx");
                };

            };
                
        }

      
        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            // Iniciamos la query de búsqueda y cargamos la grilla
            DataTable dt = usuarioService.ListarPertUsuario(Convert.ToInt32(IdUSuarioCampo.Value));
            int num_registros = 0;
            num_registros = dt.Rows.Count;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            };
        }


        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string orderby = KeySort.Value;
            int pos = orderby.IndexOf(e.SortExpression + " ASC");
            if (pos >= 0)
            {
                orderby = e.SortExpression + " DESC";
            }
            else
            {
                orderby = e.SortExpression + " ASC";
            };
            KeySort.Value = orderby;

            GridView1.PageIndex = 0;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }
       
      
    }
}
