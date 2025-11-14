using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using Datos.Entidades.Resolucion;
using System.Collections;
using LogicaNegocio.cl.subpesca.rb.resolucion;
using LogicaNegocio.cl.subpesca.rb.servicios.resoluciones;
using LogicaNegocio.cl.subpesca.rb.common;

namespace SubPesca.Resoluciones
{
    public partial class administrarResoluciones : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        PermisosService permisosService = new PermisosService();
        String erroresSumary = "ValidationSummary";
        ResolucionDA resolucionDA = new ResolucionDA();
        ResolucionService resolucionService = new ResolucionService();
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();

        protected void setearModulo()
        {

            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ADMINISTRAR_RESOLUCION };

        }


        protected void Page_Load(object sender, EventArgs e)
        {

            // PAGE LOAD
            if (!Page.IsPostBack)
            {
                setearModulo();

                ValidationSummaryErrores.ValidationGroup = erroresSumary;


                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (usuario_logeado == null)
                {
                    Response.Redirect("~/ingreso.aspx");
                    
                }


                //BOTON DE INGRESO O MODIFICACION
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], this.usuario_logeado, null, rbAccion.EDITAR))
                {
                    //PanelFormularioIngreso.Visible = true;
                    //UpdatePanelFormularioIngreso.Update();
                }
                else
                {
                    Response.Redirect("~/ingreso.aspx");
                }



                if (PreviousPage != null && PreviousPage is ingresarResoluciones)
                {
                    
                    MensajeSuperior.Text = ((ingresarResoluciones)PreviousPage).MensajeRegistro;
                    if (!MensajeSuperior.Text.Equals("")) {
                        PanelMensajeSuperior.Visible = true;
                        UpdatePanelMensajeSuperior.Update();
                    }
                    
                }




                ResolucionValidacion resolucionValidacion = new ResolucionValidacion();
                ViewState["HashCampos"] = resolucionDA.ListarResolucionValidacion(resolucionValidacion);
                


                // Inicializamos el formulario
                Initialize_Form();

            }

            string script = "calendario('" + FechaDesde.ClientID + "','" + imgFechaDesde.ClientID + "');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaDesde.ClientID, script.ToString(), true);

            script = "calendario('" + FechaHasta.ClientID + "','" + imgFechaHasta.ClientID + "');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaHasta.ClientID, script.ToString(), true);

        }





        protected void Initialize_Form()
        {
            // Cargamos los combobox
            Initialize_Comboboxs();

        }


        protected void Initialize_Comboboxs()
        {

            Carga_Combobox("TipoDocumento");
            TipoDocumento.SelectedValue = "0";

            Carga_Combobox("Origen");
            Origen.SelectedValue = "0";

            Carga_Combobox("Materia");
            Materia.SelectedValue = "0";

        }


        protected void Carga_Combobox(string combobox)
        {


            Hashtable campos = (Hashtable)ViewState["HashCampos"]; ;

            switch (combobox)
            {


                case "TipoDocumento":

                    TipoDocumento.Items.Clear();
                    if(campos != null){
                        foreach(DictionaryEntry item in campos){
                            TipoDocumento.Items.Add(new ListItem(Convert.ToString(((Combobox)item.Value).nombreAtributo), Convert.ToString(item.Key)));
                        }
                    }
                    TipoDocumento.DataBind();
                    TipoDocumento.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                

                    break;


                case "Origen":

                    Origen.Items.Clear();


                    List<ParametroGenerico> origenes =  resolucionService.ListarPosiblesOrigenes();

                    origenes.Sort();

                    if (origenes != null)
                    {
                        foreach (ParametroGenerico origen in origenes)
                        {
                            Origen.Items.Add(new ListItem(origen.descripcion, Convert.ToString(origen.id)));
                        }
                    }
                    

                    Origen.DataBind();
                    Origen.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    UpdatePanelOrigen.Update();



                    break;


                case "Materia":

                    Materia.Items.Clear();

                     List<ParametroGenerico> materias =  resolucionService.ListarPosiblesMaterias();

                     materias.Sort();

                    if (materias != null)
                    {
                        foreach (ParametroGenerico materia in materias)
                        {
                            Materia.Items.Add(new ListItem(materia.descripcion, Convert.ToString(materia.id)));
                        }
                    }

                    Materia.DataBind();
                    Materia.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    UpdatePanelMateria.Update();

                    break;


            };

        }




        //GRID RESOLUCIONES
        protected void CargaGrilla()
        {

            int pagina = 0;
            ExportarGrilla.Visible = false;

            Resolucion resolucionFiltro = new Resolucion();

            try
            {
                resolucionFiltro = (Resolucion)Session["Filtro_Resoluciones"];
                pagina = resolucionFiltro.pagina;
            }
            catch { };

            if (resolucionFiltro == null)
            {
                resolucionFiltro = new Resolucion();
                Session["Filtro_Resoluciones"] = resolucionFiltro;
            }


            List<Resolucion> resolucionList = resolucionService.ListarResolucion(resolucionFiltro);

            GridResoluciones.DataSource = resolucionList;
            GridResoluciones.DataBind();


            if (resolucionList != null && resolucionList.Count > 0)
            {
                ExportarGrilla.Visible = true;
            }


        }


        //GRID RESOLUCIONES
        protected void GridResoluciones_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

            Resolucion resolucionFiltro = (Resolucion)Session["Filtro_Resoluciones"];
            if (resolucionFiltro == null)
            {
                resolucionFiltro = new Resolucion();
            }

            resolucionFiltro.pagina = e.NewPageIndex;
            Session["Filtro_Resoluciones"] = resolucionFiltro;

            GridResoluciones.PageIndex = e.NewPageIndex;
            GridResoluciones.DataBind();
            CargaGrilla();
        }


        //GRID RESOLUCIONES
        protected void GridResoluciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridView GridRelocalizacionTramite = (GridView)sender;



                //BOTON VER
                ImageButton boton_gVer = (ImageButton)e.Row.FindControl("gVer");
                if (boton_gVer != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
                    {
                        boton_gVer.Visible = true;
                    }
                };



            
                //BOTON MODIFICAR
                ImageButton boton_gModificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_gModificar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
                    {
                        boton_gModificar.Visible = true;
                    }
                };




                String idArchivo = ((Label)e.Row.FindControl("gIDArchivo")).Text;

                //BOTON DESCARGAR
                ImageButton boton_gDescargar = (ImageButton)e.Row.FindControl("gDescargar");
                if (boton_gDescargar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
                    {
                        if (idArchivo !=null && !idArchivo.Trim().Equals("")  && Convert.ToInt32(idArchivo) > 0)
                        {
                            boton_gDescargar.Visible = true;
                        }                        
                    }
                };


                //BOTON ELIMINAR
                ImageButton boton_gEliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_gEliminar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
                    {
                        boton_gEliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar esta resolución?')");
                        boton_gEliminar.Visible = true;
                    }
                };


            };
        }


        //GRID RESOLUCIONES
        protected void GridResoluciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int id = 0;
            

            switch (e.CommandName)
            {

                case "ModificarVigencia":

                    id = Convert.ToInt32(e.CommandArgument);

                    break;

                case "VerResolucion":

                    id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("verResolucion.aspx?idResolucion=" + id);
                    

                    break;

                    
                case "ModificarResolucion":
                    id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("ingresarResoluciones.aspx?idResolucion=" + id);
                    break;

                case "DescargarArchivo":
                    
                    id = Convert.ToInt32(e.CommandArgument);

                    ArchivoBinario archivoBinario = archivoBinarioSolicitudDA.ObtenerArchivoBinarioSolicitud(Convert.ToInt32(id));

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = archivoBinario.formato;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinario.nombreArchivo + "." + archivoBinario.formato);
                    Response.BinaryWrite(archivoBinario.bytes);
                    Response.Flush();
                    Response.End();
                  

                    break;


                case "EliminarResolucion":

                    id = Convert.ToInt32(e.CommandArgument);

                    if (id > 0) {

                        if(!resolucionService.verificarComplementarias(id)) {
                         

                                if (!resolucionService.EliminarResolucion(id, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario))
                                {

                                    ErroresGrilla.Text = "Ha ocurrido un error al realizar la accion solicitada";
                                    PanelErroresGrilla.Visible = true;
                                    UpdatePanelErroresGrilla.Update();

                                }
                                else {

                                    ErroresGrilla.Text = "Accion realizada con exito";
                                    PanelErroresGrilla.Visible = true;
                                    UpdatePanelErroresGrilla.Update();
                                    this.CargaGrilla();
                        
                                }

                        }else{
                        
                            ErroresGrilla.Text = "Debe eliminar las resoluciones/decretos que complementan este documento antes de poder borrarlo.";
                            PanelErroresGrilla.Visible = true;
                            UpdatePanelErroresGrilla.Update();

                        }
                    
                    }

                    break;
              
            };
        }


        protected void Buscar_Click(object sender, EventArgs e)
        {
                  
            MensajeSuperior.Text = "";
            PanelMensajeSuperior.Visible = false;
            UpdatePanelMensajeSuperior.Update();


            ErroresGrilla.Text = "";
            PanelErroresGrilla.Visible = false;
            UpdatePanelErroresGrilla.Update();

            
            Resolucion resolucionFiltro = new Resolucion();


            if (Convert.ToInt32(TipoDocumento.SelectedValue) > 0)
            {
                resolucionFiltro.tipoDocumento = new ParametroGenerico(Convert.ToInt32(TipoDocumento.SelectedValue), TipoDocumento.SelectedItem.Text);
            }

            if (Convert.ToInt32(Origen.SelectedValue) > 0)
            {
                resolucionFiltro.origen = new ParametroGenerico(Convert.ToInt32(Origen.SelectedValue), Origen.SelectedItem.Text);
            }

            if (Convert.ToInt32(Materia.SelectedValue) > 0)
            {
                resolucionFiltro.materia = new ParametroGenerico(Convert.ToInt32(Materia.SelectedValue), Materia.SelectedItem.Text);
            }

            if (Numero.Text != null && !Numero.Text.Trim().Equals("")) {
                resolucionFiltro.numero = Numero.Text.Trim();
            }

            if (!FechaDesde.Text.Equals(""))
            {
                resolucionFiltro.fechaDesde = Convert.ToDateTime(FechaDesde.Text);
            }
            if (!FechaHasta.Text.Equals(""))
            {
                resolucionFiltro.fechaHasta = Convert.ToDateTime(FechaHasta.Text);
            }

            Session["Filtro_Resoluciones"] = resolucionFiltro;
            CargaGrilla();
               
          

        }



        protected void Limpiar_Click(object sender, EventArgs e) {

            TipoDocumento.SelectedValue = "0";
            Origen.SelectedValue = "0";
            Materia.SelectedValue = "0";
            Numero.Text = "";
            FechaDesde.Text = "";
            FechaHasta.Text = "";

            UpdatePanelTipoDocumento.Update();
            UpdatePanelOrigen.Update();
            UpdatePanelMateria.Update();
            UpdatePanelNumero.Update();

            UpdatePanelFechaDesde.Update();
            UpdatePanelFechaHasta.Update();

        }

        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();

            CargaGrilla();

            int cantidad = GridResoluciones.Columns.Count;

            if (cantidad > 1)
            {
                cantidad = cantidad - 1;
            }

            GridResoluciones.Columns.RemoveAt(cantidad);
            grilla = GridResoluciones;

            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export("AdministrarResoluciones.xls", grilla);
        }

        protected void ImgAdd_PreRender(object sender, EventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            ScriptManager sc = ScriptManager.GetCurrent(this.Page);
            sc.RegisterPostBackControl(btn);
        }

    }
}