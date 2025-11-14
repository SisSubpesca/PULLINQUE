using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using Datos.Contantes;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using LogicaNegocio.cl.subpesca.rb.doc_planilla;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;



namespace SubPesca.Solicitudes.GeneradorDocumental
{
    public partial class AdministradorDocumental : System.Web.UI.Page
    {
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        RequerimientoService requerimientoService = new RequerimientoService();
        DocPlanillaDA DocDA = new DocPlanillaDA();
        PermisosService permisosService = new PermisosService();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                Datos.Entidades.Usuario.Serializable usuario_logeado = (Datos.Entidades.Usuario.Serializable)HttpContext.Current.Session["Usuario"];
                if (!permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.AdministradorDocumental }, usuario_logeado, null, rbAccion.EDITAR))
                {
                    Response.Redirect("~/Administrador/principal.aspx");
                }

                Cargar_Dropdown();
                Listar_Grilla();
                TiposSolicitud.SelectionMode = ListSelectionMode.Multiple;
            }
        }

        protected void Cargar_Dropdown() 
        {
            TiposSolicitud.Items.Clear();
            TiposSolicitud.DataSource = parametroGenericoDA.ListarTipoTramiteEstadoSolicitud(0);
            TiposSolicitud.DataTextField = "nombreTipoInterfaz";
            TiposSolicitud.DataValueField = "idTipoTramite";
            TiposSolicitud.DataBind();
            //TiposSolicitud.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
        }
        //Listamos La grilla con los datos ingresados
        protected void Listar_Grilla()
        {
            List<DocPlanilla> _DocPlanillaList = new List<DocPlanilla>();
            DocPlanilla PlanillaFiltro = new DocPlanilla(); // no se usa aun :/

            _DocPlanillaList = DocDA.ListarDocPlanilla(0);

            GridPlanillas.DataSource = _DocPlanillaList;
            GridPlanillas.DataBind();
        }

        protected void GridPlanillas_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridPlanillas.PageIndex = e.NewPageIndex;
            GridPlanillas.DataBind();
            Listar_Grilla();
        }

        protected void GridPlanillas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Datos.Entidades.Usuario.Serializable usuario_logeado = (Datos.Entidades.Usuario.Serializable)HttpContext.Current.Session["Usuario"];
                // Borrar
                if (permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.AdministradorDocumental }, usuario_logeado, null, rbAccion.ELIMINAR))  //se usa el mismo id de la concesion 
                {
                    ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                    if (boton_eliminar != null)
                    {
                        boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar?')");
                        boton_eliminar.Visible = true;
                    };
                }
                ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                if (boton_descargar != null)
                {
                    boton_descargar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de descargar el archivo?')");
                    boton_descargar.Visible = true;
                }

                string vigencia = ((Label)e.Row.FindControl("EstadoVigencia")).Text;
                
                if (Convert.ToInt32(vigencia) == rbEstadosGenerales.VIGENTE)
                {
                    ImageButton boton_novigente = (ImageButton)e.Row.FindControl("gNoVigente");
                    if (boton_novigente != null)
                    {
                        boton_novigente.Attributes.Add("onclick", "javascript:return" + "confirm('¿Está seguro de dejar la plantilla no vigente?')");
                        boton_novigente.Visible = true;
                    }
                }
                if (Convert.ToInt32(vigencia) == rbEstadosGenerales.NO_VIGENTE)
                {
                    ImageButton boton_vigente = (ImageButton)e.Row.FindControl("gVigente");
                    if (boton_vigente != null)
                    {
                        boton_vigente.Attributes.Add("onclick", "javascript:return" + "confirm('¿Esta seguro en dejar la plantilla vigente?')");
                        boton_vigente.Visible = true;
                    }
                }
            };
        }

        protected void GridPlanillas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id;
            switch (e.CommandName)
            {
                case "Eliminar":
                    id = Convert.ToInt32(e.CommandArgument);
                    GridPlanillas.EditIndex = -1;
                    GridPlanillas_Eliminar(id);
                    break;

                case "Descargar":
                    id = Convert.ToInt32(e.CommandArgument);
                    GridPlanillas_Descargar(id);
                    break;

                case "Vigente":
                    id = Convert.ToInt32(e.CommandArgument);
                    GridPlanillas_CambioEstado(id);
                    break;

                case "NoVigente":
                    id = Convert.ToInt32(e.CommandArgument);
                    GridPlanillas_CambioEstado(id);
                    break;
            };
        }

        protected void GridPlanillas_CambioEstado(int id) 
        {
            if (id > 0)
            {
                DocPlanilla DocPlanilla = new DocPlanilla();

                DocPlanilla = DocDA.ObtenerDocPlanilla(id);

                if (DocPlanilla.vigencia.id == 6)
                {
                    bool value = requerimientoService.CambiarEstado(id, 7);
                    if (value)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Cambio realizado con exito');", true);
                        Listar_Grilla();
                    }
                }
                if (DocPlanilla.vigencia.id == 7)
                {
                    bool value = requerimientoService.CambiarEstado(id, 6);
                    if (value) 
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Cambio realizado con exito');", true);
                        Listar_Grilla();
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error, No se pudo realizar la acción');", true);
            }

        }

        protected void GridPlanillas_Eliminar(int index)
        {
            bool vacio = false;

            List<DocPlanilla> _DocPlanillaList = new List<DocPlanilla>();
            _DocPlanillaList = DocDA.ListarDocPlanilla(0);

            foreach (DocPlanilla DocPlanilla in _DocPlanillaList)
            {
                if (DocPlanilla.idDocPlanilla == index)
                {
                  //cambiarlo por un rollback!
                  // vacio =  DocDA.EliminarDocPlanilla(index);
                    vacio = requerimientoService.EliminarPlanilla(index);
                }
            }

            if (vacio == true)
            {
                //eliminacion con exito
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Eliminacion con exito);", true);
                Listar_Grilla();
            }
            else
            { 
                //nop
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error, No se pudo realizar la acción');", true);
            }
        }

        protected void GridPlanillas_Descargar(int id)
        {
            DocPlanilla DocP = DocDA.ObtenerDocPlanilla(id);
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "docx";
                Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + DocP.nombreDocPlanilla + "." + "docx");
                Response.BinaryWrite(DocP.bytes);
                Response.Flush();
                Response.End();
                Response.Close();
            }
            catch(Exception e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error, No se pudo realizar la acción');", true);
            }
        }

        protected void Guardar_Click(object sender,EventArgs e)
        { 
            //guardamos el archivo.

            if (Page.IsValid)
            {
                    DocPlanilla _DocPlanilla = new DocPlanilla();
                    // DocPlanilla _DocPlanilla = new DocPlanilla();
                    if (NombrePlantilla.Text != "" || NombrePlantilla.Text != null)
                    {
                        _DocPlanilla.nombreDocPlanilla = NombrePlantilla.Text.ToString();
                    }
                    if (DescripcionPlantilla.Text != "" || DescripcionPlantilla.Text != "")
                    {
                        _DocPlanilla.descripcion = DescripcionPlantilla.Text.ToString();
                    }
                    if (ArchivoAdjunto.HasFile)
                    {
                        _DocPlanilla.archivo = ArchivoAdjunto.PostedFile;
                    }
                    
                        

                    List<int> SelectedIndex = new List<int>();
                  
                    foreach (ListItem item in TiposSolicitud.Items)
                    {
                        if (item.Selected)
                        {
                            //SelectedIndex.Add(TiposSolicitud.Items.IndexOf(item));
                            SelectedIndex.Add(Convert.ToInt32(item.Value));
                        }
                    }


                 
                    
                    bool confirmacion = requerimientoService.GuardarPlanilla(_DocPlanilla, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario,SelectedIndex);

                    if (confirmacion)
                    { 
                        //actualizacion de gridview
                        Listar_Grilla();
                        NombrePlantilla.Text = "";
                        DescripcionPlantilla.Text = "";
                        TiposSolicitud.ClearSelection();
                    }
            }
            else
            { 
            
            }
        }
        protected void Limpiar_Click(object sender, EventArgs e)
        {
            Listar_Grilla();
            NombrePlantilla.Text = "";
            DescripcionPlantilla.Text = "";
            TiposSolicitud.ClearSelection();
            // debo borrar el archivo adjunto :(
        
        }

        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();
            string nom_grilla = "Planillas";
            string ngrilla = "";

            Listar_Grilla();

            switch (nom_grilla)
            {
                case "Planillas":
                    //GridPlanillas.Columns.RemoveAt(6);
                    grilla = GridPlanillas;
                    ngrilla = "Planillas.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

    }
}