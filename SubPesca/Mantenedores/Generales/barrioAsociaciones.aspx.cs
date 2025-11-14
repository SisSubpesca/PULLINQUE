using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using System.Web.UI.HtmlControls;
using System.Data;
using Datos.Entidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.servicios.resoluciones;
using Datos.Entidades.Resolucion;
using Validaciones.cl.subpesca.rb.mantenedor;
using SubPesca.Utilidades;

namespace SubPesca.Mantenedores.Generales
{
    public partial class barrioAsociaciones : System.Web.UI.Page
    {
        Datos.Entidades.Usuario usuarios = new Datos.Entidades.Usuario();
        MantenedorDA mantenedorDA = new MantenedorDA();
        BarrioDA barrioDA = new BarrioDA();
        TipoDA tipoDa = new TipoDA();
        MantenedorGeneralService mantenedorGeneralService = new MantenedorGeneralService();
        MantenedorGeneralValidacion mantenedorGeneralValidacion = new MantenedorGeneralValidacion();


        // CARGA DE ARCHIVOS .JS DESDE C#
        protected void Page_Init(object sender, System.EventArgs e)
        {
            HtmlGenericControl scriptInclude = new HtmlGenericControl();

            scriptInclude = (HtmlGenericControl)Page.Header.FindControl("funciones.js");
            if (scriptInclude == null)
            {
                scriptInclude = new HtmlGenericControl("script");
                scriptInclude.Attributes["type"] = "text/javascript";
                scriptInclude.Attributes["src"] = ResolveClientUrl("~/js/funciones.js");
                scriptInclude.ID = "funciones.js";
                Page.Header.Controls.Add(scriptInclude);
            };
            scriptInclude = (HtmlGenericControl)Page.Header.FindControl("jquery.autoheight.js");
            if (scriptInclude == null)
            {
                scriptInclude = new HtmlGenericControl("script");
                scriptInclude.Attributes["type"] = "text/javascript";
                scriptInclude.Attributes["src"] = ResolveClientUrl("~/js/jquery/jquery.autoheight.js");
                scriptInclude.ID = "jquery.autoheight.js";
                Page.Header.Controls.Add(scriptInclude);
            };
        }

        // PAGE_LOAD
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {
                // Validamos los accesos al listado y a sus objetos
                Content_Panel.Visible = true;
                
                CargarCombobox("BarrioACS");
                BarrioACS.SelectedValue = "0";
                
                CargarCombobox("BarrioACM");
                BarrioACM.SelectedValue = "0";

                CargarCombobox("Tipo");
                Tipo.SelectedValue = "0";

                CargarCombobox("TipoUnidadEspacial");
                TipoUnidadEspacial.SelectedValue = "0";

                CargarCombobox("TipoSolicitud");
                TipoSolicitud.SelectedValue = "0";

                //Cargamos la grilla
                CargaGrilla();
            
            };

        }

        private void CargarCombobox(string combobox)
        {
            switch (combobox)
            {
                case "BarrioACS":

                    BarrioACS.Items.Clear();
                    BarrioACS.DataSource = barrioDA.obtenerBarrio(rbTipo.TIPO_BARRIO_ACS, 0, 0, 0);
                    BarrioACS.DataTextField = "Barrio";
                    BarrioACS.DataValueField = "IdBarrio";
                    BarrioACS.DataBind();
                    BarrioACS.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    
                    break;

                case "BarrioACM":
                    BarrioACM.Items.Clear();
                    BarrioACM.DataSource = barrioDA.obtenerBarrio(rbTipo.TIPO_BARRIO_ACM, 0, 0, 0);
                    BarrioACM.DataTextField = "Barrio";
                    BarrioACM.DataValueField = "IdBarrio";
                    BarrioACM.DataBind();
                    BarrioACM.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    
                    break;

                case "Tipo":

                    Tipo.Items.Clear();
                    Tipo.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    Tipo.Items.Add(new ListItem("Tramite", Convert.ToString(rbTipo.RESOLUCION_SUB_REFERENCIA_SOLICITUD)));
                    Tipo.Items.Add(new ListItem("Unidad Espacial", Convert.ToString(rbTipo.RESOLUCION_SUB_REFERENCIA_UE)));
                    Tipo.DataBind();
                    
                    break;

                case "TipoUnidadEspacial":

                    TipoUnidadEspacial.Items.Clear();
                    TipoUnidadEspacial.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    List<ParametroGenerico> tipoUE =  tipoDa.ListarTipo(clavesTipo.TIPO_UNIDAD_ESPACIAL);

                    if (tipoUE != null)
                    {
                        foreach (ParametroGenerico param in tipoUE)
                        {
                            TipoUnidadEspacial.Items.Add(new ListItem(param.descripcion, param.id.ToString()));
                        }
                    }

                    TipoSolicitud.DataBind();
                    
                    break;

                case "TipoSolicitud":

                    TipoSolicitud.Items.Clear();
                    TipoSolicitud.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    List<ParametroGenerico> tipoTramite = tipoDa.ListarTipo(clavesTipo.TIPO_TRAMITE);

                    if (tipoTramite != null)
                    {

                        foreach (ParametroGenerico param in tipoTramite)
                        {
                            TipoSolicitud.Items.Add(new ListItem(param.descripcion, param.id.ToString()));
                        }
                    }

                    TipoSolicitud.DataBind();
                    break;

                    //TipoSolicitud.Items.Add(new ListItem("Tramite de Solicitud Acuicultura en Amerb", "122"));
                    //TipoSolicitud.Items.Add(new ListItem("Tramite de Solicitud Acuicultura en ECMPO", "537"));
                    //TipoSolicitud.Items.Add(new ListItem("Tramite de Solicitud Centro de Acopio", "121"));
                    //TipoSolicitud.Items.Add(new ListItem("Tramite de Solicitud Centro de Faenamiento", "124"));
                    //TipoSolicitud.Items.Add(new ListItem("Tramite de Solicitud Colectores de Semilla", "123"));
                    //TipoSolicitud.Items.Add(new ListItem("Trámite de Solicitud Concesión de Acuicultura", "88"));
                    //TipoSolicitud.Items.Add(new ListItem("Tramite de Solicitud Experimentales AMERB", "535"));
                    //TipoSolicitud.Items.Add(new ListItem("Tramite de Solicitud Experimentales Concesión", "536"));
                    //TipoSolicitud.Items.Add(new ListItem("Trámite de Modificación AMERB", "558"));
                    //TipoSolicitud.Items.Add(new ListItem("Trámite de Modificación Centro de Acopio", "552"));
                    //TipoSolicitud.Items.Add(new ListItem("Trámite de Modificación Concesión de Acuicultura", "89"));
                    //TipoSolicitud.Items.Add(new ListItem("Trámite de Modificación Centro de Faenamiento", "546"));
                    //TipoSolicitud.Items.Add(new ListItem("Trámite de Modificación ECMPO", "564"));
                    //TipoSolicitud.Items.Add(new ListItem("Trámite Sector Relocalización LEY", "95"));
                    //TipoSolicitud.Items.Add(new ListItem("Trámite Sector Relocalización RESA", "624"));
                    
            }
        }


        // ACCIONES DE BOTONES
        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            PanelMensaje.Visible = false;
            UpdatePanelMensaje.Update();

            AsociacionSolicitudBarrio asociacionSolicitudBarrio = new AsociacionSolicitudBarrio();

            if (Convert.ToInt32(BarrioACS.SelectedValue) > 0)
            {
                asociacionSolicitudBarrio.barrioACS = new ParametroGenerico(Convert.ToInt32(BarrioACS.SelectedValue), BarrioACS.SelectedItem.Text);
            }

            if (Convert.ToInt32(BarrioACM.SelectedValue) > 0)
            {
                asociacionSolicitudBarrio.barrioACM = new ParametroGenerico(Convert.ToInt32(BarrioACM.SelectedValue), BarrioACM.SelectedItem.Text);
            }

            if (Convert.ToInt32(Tipo.SelectedValue) > 0)
            {
                asociacionSolicitudBarrio.tipo = new ParametroGenerico(Convert.ToInt32(Tipo.SelectedValue), Tipo.SelectedItem.Text);
            }

            if (Convert.ToInt32(TipoUnidadEspacial.SelectedValue) > 0)
            {
                asociacionSolicitudBarrio.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(TipoUnidadEspacial.SelectedValue), TipoUnidadEspacial.SelectedItem.Text);
            }

            if (Convert.ToInt32(TipoSolicitud.SelectedValue) > 0)
            {
                asociacionSolicitudBarrio.tipoSolicitud = new ParametroGenerico(Convert.ToInt32(TipoSolicitud.SelectedValue), TipoSolicitud.SelectedItem.Text);
            }

            if (!CodigoCentro.Text.Trim().Equals(""))
            {
                asociacionSolicitudBarrio.codigoCentro = CodigoCentro.Text;
            }

            if (!NumeroPert.Text.Trim().Equals(""))
            {
                asociacionSolicitudBarrio.numPert = NumeroPert.Text;
            }

            if (!NumeroIdentificador.Text.Trim().Equals(""))
            {
                asociacionSolicitudBarrio.numeroIdentificador = NumeroIdentificador.Text;
            }

            if (!NumeroSector.Text.Trim().Equals(""))
            {
                asociacionSolicitudBarrio.numSector = Convert.ToInt32(NumeroSector.Text);
            }

            //VALIDAR EL INGRESO DE LA NUEVA REFERENCIA
            List<String> errores = mantenedorGeneralValidacion.validarIngresoAsociacionSol_UE_Barrio(asociacionSolicitudBarrio);

            if (errores.Count > 0)
            {
                foreach (String error in errores)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }

                PanelMensaje.Visible = true;
                UpdatePanelMensaje.Update();

                mensaje = "";
                Content_msgGrilla.Visible = false;
                msgGrilla.Text = mensaje;
                
            }
            else
            {

                int barrioACS = 0;
                int barrioACM = 0;

                if (asociacionSolicitudBarrio.barrioACS != null && asociacionSolicitudBarrio.barrioACS.id > 0) {
                    barrioACS = asociacionSolicitudBarrio.barrioACS.id;
                }
                if (asociacionSolicitudBarrio.barrioACM != null && asociacionSolicitudBarrio.barrioACM.id > 0)
                {
                    barrioACM = asociacionSolicitudBarrio.barrioACM.id;
                }

                bool resultado = mantenedorGeneralService.ActualizaSolicitudConcesion_ACS_ACM(asociacionSolicitudBarrio.idSolConcesion, barrioACS, barrioACM, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);


                if (resultado)
                {
                    mensaje = "Registro Actualizado con éxito";
                    Content_msgGrilla.Visible = true;
                    msgGrilla.Text = mensaje;
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    CargaGrilla();
                }
                else {

                    mensaje = "Ha ocurrido un error al ejecutar la acción solicitada";
                    Content_msgGrilla.Visible = true;
                    msgGrilla.Text = mensaje;
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                }

                

            }

            

            /*
            if (barrio != null)
            {
                DataTable dt = mantenedorGeneralService.guardarBarrioTipo(barrio);

                try
                {
                    string msg = Convert.ToString(dt.Rows[0]["msg"]);
                    if (msg == "OK")
                    {
                        mensaje = "La asociación tipo barrio - barrio '" + barrio.barrio + "' ha sido creada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        GridView1.EditIndex = -1;
                        GridView1.PageIndex = 0;
                        CargaGrilla();
                    }
                    else
                    {
                        mensaje = "La asociación tipo barrio - barrio '" + barrio.barrio + "' ya existe.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    };
                }
                catch
                {
                    mensaje = "Se ha producido un error al intentar crear la asociación tipo barrio - barrio.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };

                TipoBarrio.SelectedValue = "-1";
                Barrio.SelectedValue = "-1";
            }
            else
            {
                mensaje = "Para agregar debe ingresar los datos de la asociación tipo barrio - barrio.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

             **/

          
        }



        // ACCIONES DE BOTONES
        protected void Buscar_Click(object sender, EventArgs e)
        {


            PanelMensaje.Visible = false;
            UpdatePanelMensaje.Update();

            string mensaje = "";

            CargaGrilla();
            Content_msgGrilla.Visible = false;
            msgGrilla.Text = mensaje;
        }

        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {

            
            AsociacionSolicitudBarrio asociacionSolicitudBarrio = new AsociacionSolicitudBarrio();


            

            if (Convert.ToInt32(BarrioACS.SelectedValue) > 0)
            {
                asociacionSolicitudBarrio.barrioACS = new ParametroGenerico(Convert.ToInt32(BarrioACS.SelectedValue), BarrioACS.SelectedItem.Text);
            }

            if (Convert.ToInt32(BarrioACM.SelectedValue) > 0)
            {
                asociacionSolicitudBarrio.barrioACM = new ParametroGenerico(Convert.ToInt32(BarrioACM.SelectedValue), BarrioACM.SelectedItem.Text);
            }

            if (Convert.ToInt32(Tipo.SelectedValue) > 0)
            {
                if (Convert.ToInt32(Tipo.SelectedValue) == rbTipo.RESOLUCION_SUB_REFERENCIA_SOLICITUD)
                {
                    asociacionSolicitudBarrio.tipo = new ParametroGenerico(0);
                }
                if (Convert.ToInt32(Tipo.SelectedValue) == rbTipo.RESOLUCION_SUB_REFERENCIA_UE)
                {
                    asociacionSolicitudBarrio.tipo = new ParametroGenerico(1);
                }
            }

            if (Convert.ToInt32(TipoUnidadEspacial.SelectedValue) > 0)
            {
                asociacionSolicitudBarrio.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(TipoUnidadEspacial.SelectedValue), TipoUnidadEspacial.SelectedItem.Text);
            }

            if (Convert.ToInt32(TipoSolicitud.SelectedValue) > 0)
            {
                asociacionSolicitudBarrio.tipoSolicitud = new ParametroGenerico(Convert.ToInt32(TipoSolicitud.SelectedValue), TipoSolicitud.SelectedItem.Text);
            }

            if (!CodigoCentro.Text.Trim().Equals(""))
            {
                asociacionSolicitudBarrio.codigoCentro = CodigoCentro.Text;
            }

            if (!NumeroPert.Text.Trim().Equals(""))
            {
                asociacionSolicitudBarrio.numPert = NumeroPert.Text;
            }

            if (!NumeroIdentificador.Text.Trim().Equals(""))
            {
                asociacionSolicitudBarrio.numeroIdentificador = NumeroIdentificador.Text;
            }

            if (!NumeroSector.Text.Trim().Equals(""))
            {
                try 
                {
                    asociacionSolicitudBarrio.numSector = Convert.ToInt32(NumeroSector.Text);
                }
                catch
                {
                    asociacionSolicitudBarrio.numSector = -1;
                }
                
            }
            else {
                asociacionSolicitudBarrio.numSector = -1;
            }

            List<AsociacionSolicitudBarrio> dt = mantenedorGeneralService.listarSolicitudBarrio(asociacionSolicitudBarrio);

            int num_registros = 0;
            num_registros = dt.Count;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            if (num_registros > 0)
            {
                ExportarGrilla.Visible = true;
            }
            else 
            {
                ExportarGrilla.Visible = false;
            }

          
          
        }

        protected void GridView1_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            //Agregar.Enabled = true;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Modificar
                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_modificar != null)
                {
                    boton_modificar.Visible = true;
                };

                // Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar acs y acm para el registro seleccionado?')");
                    boton_eliminar.Visible = true;
                };
            };

            if (GridView1.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
            {

                HiddenField region = ((HiddenField)e.Row.FindControl("hdnIdRegion"));
                int idRegion = 10000; //Region que no existe

                if (region != null && !region.Value.Trim().Equals("")) {
                    idRegion = Convert.ToInt32(region.Value);
                }

                DropDownList ddllist1 = ((DropDownList)e.Row.FindControl("ddleditBarrioACS"));
                var hdnBarrioACS = ((HiddenField)e.Row.FindControl("hdnBarrioACS"));
                ddllist1.AppendDataBoundItems = true;
                ddllist1.DataSource = barrioDA.obtenerBarrio(rbTipo.TIPO_BARRIO_ACS, 0, idRegion, 0);
                ddllist1.DataTextField = "Barrio";
                ddllist1.DataValueField = "IdBarrio";
                ddllist1.DataBind();
                ddllist1.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnBarrioACS.Value != null && !hdnBarrioACS.Value.Equals(""))
                {

                    try
                    {
                        ddllist1.Items.FindByValue(hdnBarrioACS.Value).Selected = true;
                    }
                    catch { }
                    
                }

                DropDownList ddllist2 = ((DropDownList)e.Row.FindControl("ddleditBarrioACM"));
                var hdnBarrioACM = ((HiddenField)e.Row.FindControl("hdnBarrioACM"));
                ddllist2.AppendDataBoundItems = true;

                ddllist2.DataSource = barrioDA.obtenerBarrio(rbTipo.TIPO_BARRIO_ACM, 0, idRegion, 0);
                ddllist2.DataTextField = "Barrio";
                ddllist2.DataValueField = "IdBarrio";
                ddllist2.DataBind();
                ddllist2.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnBarrioACM.Value != null && !hdnBarrioACM.Value.Equals(""))
                {

                    try {
                        ddllist2.Items.FindByValue(hdnBarrioACM.Value).Selected = true;
                    }catch{}

                    
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Content_msgGrilla.Visible = false;

            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            switch (e.CommandName)
            {
                case "Eliminar":

                    int idSolConcesion = Convert.ToInt32(arg[0]);
                    this.Delete(idSolConcesion);
                    GridView1.EditIndex = -1;
                    CargaGrilla();
                    break;
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

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Agregar.Enabled = false;
            GridView1.EditIndex = e.NewEditIndex;
            CargaGrilla();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            HiddenField solicitud = (HiddenField)GridView1.Rows[e.RowIndex].FindControl("hdnIdSolConcesion"); ;
            DropDownList barrioACS = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditBarrioACS"); ;
            DropDownList barrioACM = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditBarrioACM"); ;


            Agregar.Enabled = true;
            Update(Convert.ToInt32(solicitud.Value), Convert.ToInt32(barrioACS.SelectedValue), Convert.ToInt32(barrioACM.SelectedValue));
            GridView1.EditIndex = -1;
          
            CargaGrilla();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Agregar.Enabled = true;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void Update(int idSolConcesion,int idBarrio, int idACS)
        {

            string mensaje = "";

            try
            {

                bool resultado = mantenedorGeneralService.ActualizaSolicitudConcesion_ACS_ACM(idSolConcesion, idBarrio, idACS, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);


                if (resultado)
                {
                    mensaje = "Se ha actualizado la ACS y ACM para el registro seleccionado.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";

                }
                else
                {
                    mensaje = "Se ha producido un error al intentar actualizar la ACS y ACM para el registro seleccionado.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";

                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar actualizar la ACS y ACM para el registro seleccionado.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }


        protected void Delete(int idSolConcesion)
        {
            string mensaje = "";

            try
            {

                bool resultado = mantenedorGeneralService.ActualizaSolicitudConcesion_ACS_ACM(idSolConcesion, 0, 0, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);


                if (resultado)
                {
                    mensaje = "Se ha actualizado la ACS y ACM para el registro seleccionado.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                   
                }else{   
                    mensaje = "Se ha producido un error al intentar actualizar la ACS y ACM para el registro seleccionado.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar actualizar la ACS y ACM para el registro seleccionado.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void Volver_Click(object sender, EventArgs e)
        {
            string path = "~/Administrador/principal.aspx";
            Response.Redirect(path);
        }

        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();
            string nom_grilla = "barrioTipo";
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "barrioTipo":
                    GridView1.Columns.RemoveAt(8);
                    grilla = GridView1;
                    ngrilla = "barrioAsociaciones.xls";
                    break;
            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }




        protected void Tipo_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            TipoUnidadEspacial.SelectedValue = "0";
            TipoSolicitud.SelectedValue = "0";


            if (Convert.ToInt32(Tipo.SelectedValue) == rbTipo.RESOLUCION_SUB_REFERENCIA_SOLICITUD)
            {

                PanelTipoSolicitud.Visible = true;
                UpdatePanelTipoSolicitud.Update();


                PanelTipoUnidadEspacial.Visible = false;
                UpdatePanelTipoUnidadEspacial.Update();

            }
            else if (Convert.ToInt32(Tipo.SelectedValue) == rbTipo.RESOLUCION_SUB_REFERENCIA_UE)
            {

                PanelTipoSolicitud.Visible = false;
                UpdatePanelTipoSolicitud.Update();

                PanelTipoUnidadEspacial.Visible = true;
                UpdatePanelTipoUnidadEspacial.Update();

            }
            else
            {

                PanelTipoSolicitud.Visible = false;
                UpdatePanelTipoSolicitud.Update();

                PanelTipoUnidadEspacial.Visible = false;
                UpdatePanelTipoUnidadEspacial.Update();

            }

            PanelCodigoCentro.Visible = false;
            UpdatePanelCodigoCentro.Update();

            PanelNumeroPert.Visible = false;
            UpdatePanelNumeroPert.Update();

            PanelNumeroSector.Visible = false;
            UpdatePanelNumeroSector.Update();

            PanelNumeroIdentificador.Visible = false;
            UpdatePanelNumeroIdentificador.Update();
        }




        protected void TipoUnidadEspacial_SelectedIndexChanged(object sender, EventArgs e)
        {


            CodigoCentro.Text = "";
            NumeroPert.Text = "";
            NumeroIdentificador.Text = "";
            NumeroSector.Text = "";

            if (Convert.ToInt32(TipoUnidadEspacial.SelectedValue) == 120) //COLECTORES
            {

                PanelCodigoCentro.Visible = false;
                UpdatePanelCodigoCentro.Update();

                PanelNumeroPert.Visible = false;
                UpdatePanelNumeroPert.Update();

                PanelNumeroSector.Visible = false;
                UpdatePanelNumeroSector.Update();

                PanelNumeroIdentificador.Visible = true;
                UpdatePanelNumeroIdentificador.Update();


            }
            else if (Convert.ToInt32(TipoUnidadEspacial.SelectedValue) > 0)
            {
                PanelCodigoCentro.Visible = true;
                UpdatePanelCodigoCentro.Update();

                PanelNumeroPert.Visible = false;
                UpdatePanelNumeroPert.Update();

                PanelNumeroSector.Visible = false;
                UpdatePanelNumeroSector.Update();

                PanelNumeroIdentificador.Visible = false;
                UpdatePanelNumeroIdentificador.Update();


            }
            else
            {

                PanelCodigoCentro.Visible = false;
                UpdatePanelCodigoCentro.Update();

                PanelNumeroPert.Visible = false;
                UpdatePanelNumeroPert.Update();

                PanelNumeroSector.Visible = false;
                UpdatePanelNumeroSector.Update();

                PanelNumeroIdentificador.Visible = false;
                UpdatePanelNumeroIdentificador.Update();

            }


        }



        protected void TipoSolicitud_SelectedIndexChanged(object sender, EventArgs e)
        {

            CodigoCentro.Text = "";
            NumeroPert.Text = "";
            NumeroIdentificador.Text = "";
            NumeroSector.Text = "";
            

            if (Convert.ToInt32(TipoSolicitud.SelectedValue) == rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA) //COLECTORES
            {

                PanelCodigoCentro.Visible = false;
                UpdatePanelCodigoCentro.Update();

                PanelNumeroPert.Visible = false;
                UpdatePanelNumeroPert.Update();

                PanelNumeroSector.Visible = false;
                UpdatePanelNumeroSector.Update();

                PanelNumeroIdentificador.Visible = true;
                UpdatePanelNumeroIdentificador.Update();


            }

            else if (Convert.ToInt32(TipoSolicitud.SelectedValue) == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION) //Sector Relocalizacion
            {
                PanelCodigoCentro.Visible = false;
                UpdatePanelCodigoCentro.Update();

                PanelNumeroPert.Visible = true;
                UpdatePanelNumeroPert.Update();

                PanelNumeroSector.Visible = true;
                UpdatePanelNumeroSector.Update();

                PanelNumeroIdentificador.Visible = false;
                UpdatePanelNumeroIdentificador.Update();


            }
            else if (Convert.ToInt32(TipoSolicitud.SelectedValue) > 0)
            {
                PanelCodigoCentro.Visible = false;
                UpdatePanelCodigoCentro.Update();

                PanelNumeroPert.Visible = true;
                UpdatePanelNumeroPert.Update();

                PanelNumeroSector.Visible = false;
                UpdatePanelNumeroSector.Update();

                PanelNumeroIdentificador.Visible = false;
                UpdatePanelNumeroIdentificador.Update();

            }
            else
            {

                PanelCodigoCentro.Visible = false;
                UpdatePanelCodigoCentro.Update();

                PanelNumeroPert.Visible = false;
                UpdatePanelNumeroPert.Update();

                PanelNumeroSector.Visible = false;
                UpdatePanelNumeroSector.Update();

                PanelNumeroIdentificador.Visible = false;
                UpdatePanelNumeroIdentificador.Update();

            }
        }


    }


}