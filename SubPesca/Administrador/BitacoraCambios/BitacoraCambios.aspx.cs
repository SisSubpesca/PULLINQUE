using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.common;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.usuario;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;

namespace SubPesca.Administrador.BitacoraCambios
{
    public partial class BitacoraCambios : System.Web.UI.Page
    {
        ParametroGenericoDA parametroDa = new ParametroGenericoDA();
        PermisosService permisosService = new PermisosService();
        UsuarioDA usuarioDA = new UsuarioDA();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Datos.Entidades.Usuario.Serializable usuario_logeado = (Datos.Entidades.Usuario.Serializable)HttpContext.Current.Session["Usuario"];

                if (!permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.Bitacora_Cambios }, usuario_logeado, null, rbAccion.EDITAR))
                {
                    Response.Redirect("~/Administrador/principal.aspx");
                }
                // Inicializamos el formulario   
                cargarCombobox();
            };
        }

        protected void cargarCombobox()
        {
            NombreTabla.Items.Clear();
            NombreTabla.DataSource = parametroDa.ListarTiposGenerico("paSelRbTipo", "@grupo", "Tipo_Tabla_Transact", "idTipo", "nombreTipo");
            NombreTabla.DataTextField = "descripcion";
            NombreTabla.DataValueField = "id";
            NombreTabla.DataBind();
            NombreTabla.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            NombreTabla.SelectedValue = "0";


            Tramite.Items.Clear();
            
            Tramite.Items.Insert(0, new ListItem("Modificación", "1"));
            Tramite.Items.Insert(0, new ListItem("Eliminación", "2"));
            Tramite.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            Tramite.SelectedValue = "0";
        }

        protected void Limpiar_Click(object sender, EventArgs e)
        {
            
            NombreTabla.SelectedValue = "0";
            Tramite.SelectedValue = "0";
            NPert.Text = "";
            FechaIngresoTramiteDesde.Text = "";
            FechaIngresoTramiteHasta.Text = "";

            //Updatear los paneles

            UpdatePaneltramite.Update();
            UpdatePanelNombreTabla.Update();
            UpdatePanelNPert.Update();
            UpdatePanelFechaIngresoTramiteDesde.Update();
            UpdatePanelFechaIngresoTramiteHasta.Update();

        }
       

        protected void Buscar_Click(object sender, EventArgs e)
        {
            string npert = "";
            string tabla = "";
            string accion = "";
            DateTime FechaIngresoDesde = default(DateTime);
            DateTime FechaIngresoHasta = default(DateTime);

            if (!NPert.Text.Equals(""))
            {
                npert = Convert.ToString(NPert.Text);
            }
            if (FechaIngresoTramiteDesde.Text.Length > 0)
            {
                FechaIngresoDesde = Convert.ToDateTime(FechaIngresoTramiteDesde.Text);
            }
            if (FechaIngresoTramiteHasta.Text.Length > 0)
            {
                FechaIngresoHasta = Convert.ToDateTime(FechaIngresoTramiteHasta.Text);
            }
            if (Convert.ToInt32(NombreTabla.SelectedValue) > 0)
            {
                tabla = NombreTabla.SelectedItem.Text;
            }
            if (Convert.ToInt32(Tramite.SelectedValue) == 1)
            {
                accion = "U";
            }
            if (Convert.ToInt32(Tramite.SelectedValue) == 2)
            {
                accion = "D";
            }   
            /*if (Convert.ToDateTime(FechaIngresoTramiteHasta.Text) > Convert.ToDateTime(FechaIngresoTramiteDesde.Text))
            { 
                //error!!
            }*/
            DataTable dt = new DataTable();

            dt = usuarioDA.ListarLogTransacciones(tabla, accion, npert, FechaIngresoDesde, FechaIngresoHasta);

            cargarGrilla(dt);
        }

        protected void cargarGrilla(DataTable dt) 
        {
            if (dt == null)
            {
                //error
                //ExportarGrilla.Visible = false;
            }
            else
            {
                Session["Filtro_Bitacora"] = dt;
                GridBitacora.DataSource = dt;
                GridBitacora.DataBind();

                //ExportarGrilla.Visible = true;
            }
        }

        protected void GridBitacora_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["Filtro_Bitacora"];

            if (dt == null)
            {
                dt = new DataTable();
            }
            Session["Filtro_Bitacora"] = dt;
            GridBitacora.PageIndex = e.NewPageIndex;
            cargarGrilla(dt);
        }

        //protected void ExportarGrilla_Click(object sender, EventArgs e)
        //{
        //    GridView grilla = new GridView();
        //    string nom_grilla = "bitacoraDeCambios";
        //    string ngrilla = "";

        //    Buscar_Click(null,null);

        //    switch (nom_grilla)
        //    {
        //        case "bitacoraDeCambios":
        //            grilla = GridBitacora;
        //            ngrilla = "bitacoraDeCambios.xls";
        //            break;

        //    };
        //    grilla.AllowPaging = false;
        //    grilla.DataBind();
        //    SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        //}


    }
}