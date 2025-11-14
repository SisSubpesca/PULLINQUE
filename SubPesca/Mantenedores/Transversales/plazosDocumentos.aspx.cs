using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Datos.Entidades;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.servicios.mantenedores;
using SubPesca.Mantenedores.Generales;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.common;



namespace SubPesca.Mantenedores.Transversales
{
    public partial class plazosDocumentos : System.Web.UI.Page
    {

        Datos.Entidades.Usuario usuarios = new Datos.Entidades.Usuario();
        MantenedorGeneralService mantenedorGeneralService = new MantenedorGeneralService();

        RequerimientoDA requerimientoDA = new RequerimientoDA();
        TipoDA tipoDa = new TipoDA();



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

                //Cargamos combobox
                CargaCombobox("SubrequerimientoOrigen");

                CargaCombobox("TipoUnidadEspacial");

                // Cargamos la grilla
                CargaGrilla();
            };
        }

        private void CargaCombobox(string combobox)
        {
            switch (combobox)
            {
                case "SubrequerimientoOrigen":

                    SubrequerimientoOrigen.Items.Clear();

                    SubRequerimiento subRequerimiento = new SubRequerimiento();
                    subRequerimiento.aplicaReiteraFiltro = -1;
                    subRequerimiento.aplicaComplementarioFiltro = -1;
                    subRequerimiento.aplicaVisacionMasivaFiltro = -1;

                    SubrequerimientoOrigen.DataSource = requerimientoDA.ListarSubRequerimiento(subRequerimiento);
                    
                    SubrequerimientoOrigen.DataTextField = "nombreSubRequerimiento";
                    SubrequerimientoOrigen.DataValueField = "idSubRequerimiento";
                    SubrequerimientoOrigen.DataBind();
                    SubrequerimientoOrigen.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "TipoUnidadEspacial":

                    TipoUnidadEspacial.Items.Clear();
                    TipoUnidadEspacial.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    List<ParametroGenerico> tipoUE =  tipoDa.ListarTipo(clavesTipo.TIPO_UNIDAD_ESPACIAL);

                    if (tipoUE != null)
                    {
                        foreach (ParametroGenerico param in tipoUE)
                        {
                            TipoUnidadEspacial.Items.Add(new ListItem(param.descripcion, param.id.ToString()));
                        }
                    }

                    TipoUnidadEspacial.DataBind();
                    
                    break;
            }
        }

        // ACCIONES DE BOTONES
        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            PlazoDocumentoSolicitud plazoDocumentoSolicitud = new PlazoDocumentoSolicitud();
            plazoDocumentoSolicitud.docOrigen = new ParametroGenerico();
            plazoDocumentoSolicitud.docOrigen.id = Convert.ToInt32(SubrequerimientoOrigen.SelectedItem.Value);
            plazoDocumentoSolicitud.docOrigen.descripcion = SubrequerimientoOrigen.Text;

            plazoDocumentoSolicitud.tipoUE = new ParametroGenerico();
            plazoDocumentoSolicitud.tipoUE.id = Convert.ToInt32(TipoUnidadEspacial.SelectedItem.Value);

            if (PlazoDias.Text != null && !PlazoDias.Text.Equals(""))
            {
                plazoDocumentoSolicitud.plazoDias = Convert.ToInt32(PlazoDias.Text);
            }

            if (PlazoMeses.Text != null && !PlazoMeses.Text.Equals(""))
            {
                plazoDocumentoSolicitud.plazoMeses = Convert.ToInt32(PlazoMeses.Text);
            }


            /* Sólo se requiere el ingreso de Plazo Días o Plazo Meses */
            bool permitirGuardar = true;
            if (plazoDocumentoSolicitud.plazoDias > 0 && plazoDocumentoSolicitud.plazoMeses > 0)
            {
                permitirGuardar = false;
            }
            

            if (plazoDocumentoSolicitud != null)
            {
                if (permitirGuardar)
                {
                    DataTable dt = mantenedorGeneralService.GuardarPlazosDocumentos(plazoDocumentoSolicitud);

                    try
                    {
                        string msg = Convert.ToString(dt.Rows[0]["msg"]);
                        if (msg == "OK")
                        {
                            mensaje = "Los plazos de documentos para '" + plazoDocumentoSolicitud.docOrigen.descripcion + "' ha sido creado.";
                            Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                            GridView1.EditIndex = -1;
                            GridView1.PageIndex = 0;

                            this.limpiarFormularioCompleto();

                            CargaGrilla();
                        }
                        else
                        {
                            mensaje = "Los plazos de documentos para '" + plazoDocumentoSolicitud.docOrigen.descripcion + "' ya existe.";
                            Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        };
                    }
                    catch
                    {
                        mensaje = "Se ha producido un error al intentar crear los plazos de documentos.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    };
                }
                else {
                    mensaje = "Debe ingresar plazo días o plazo meses para los de documentos. En ningún caso ambos.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                
                }
            }
            else
            {
                mensaje = "Para agregar debe ingresar los datos de los plazos de documentos.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        public void limpiarFormularioCompleto() {

            SubrequerimientoOrigen.SelectedValue = "-1";
            TipoUnidadEspacial.SelectedValue = "-1";
            PlazoDias.Text = "";
            PlazoDias.Enabled = true;
            PlazoMeses.Text = "";
            PlazoMeses.Enabled = true;
        }
        
        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            // Iniciamos la query de búsqueda y cargamos la grilla
            PlazoDocumentoSolicitud plazoDocumentoSolicitud = new PlazoDocumentoSolicitud();
            plazoDocumentoSolicitud.docOrigen = new ParametroGenerico();
            plazoDocumentoSolicitud.docOrigen.id = Convert.ToInt32(SubrequerimientoOrigen.SelectedItem.Value);

            plazoDocumentoSolicitud.tipoUE = new ParametroGenerico();
            plazoDocumentoSolicitud.tipoUE.id = Convert.ToInt32(TipoUnidadEspacial.SelectedItem.Value);

            if (PlazoDias.Text != null && !PlazoDias.Text.Equals(""))
            {
                plazoDocumentoSolicitud.plazoDias = Convert.ToInt32(PlazoDias.Text);
            }
            else {
                plazoDocumentoSolicitud.plazoDias = -1;
            }

            if (PlazoMeses.Text != null && !PlazoMeses.Text.Equals(""))
            {
                plazoDocumentoSolicitud.plazoMeses = Convert.ToInt32(PlazoMeses.Text);
            }
            else {
                plazoDocumentoSolicitud.plazoMeses = -1;
            }

            List<PlazoDocumentoSolicitud> dt = mantenedorGeneralService.listarPlazosDocumentos(plazoDocumentoSolicitud);

            int num_registros = 0;
            num_registros = dt.Count;

            GridView1.DataSource = dt;
            GridView1.DataBind();

            if (dt != null && dt.Count > 0)
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
            Agregar.Enabled = true;
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
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar el plazo del documento " + DataBinder.Eval(e.Row.DataItem, "idPlazoDoc") + "?')");
                    boton_eliminar.Visible = true;
                };
            };

            /* Subrequerimiento Origen */
            if (GridView1.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddllist = ((DropDownList)e.Row.FindControl("ddleditCountry"));
                var hdnCountryName = ((HiddenField)e.Row.FindControl("hdnCountry"));
                ddllist.AppendDataBoundItems = true;

                SubRequerimiento subRequerimiento = new SubRequerimiento();
                subRequerimiento.aplicaReiteraFiltro = -1;
                subRequerimiento.aplicaComplementarioFiltro = -1;
                subRequerimiento.aplicaVisacionMasivaFiltro = -1;

                ddllist.DataSource = requerimientoDA.ListarSubRequerimiento(subRequerimiento);
                ddllist.DataTextField = "nombreSubRequerimiento";
                ddllist.DataValueField = "idSubRequerimiento";
                ddllist.DataBind();
                ddllist.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                ddllist.Items.FindByText(hdnCountryName.Value).Selected = true;
                
            }

            /* Tipo Unidad Espacial */
            if (GridView1.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddllist2 = ((DropDownList)e.Row.FindControl("ddleditCountry2"));
                var hdnCountry2 = ((HiddenField)e.Row.FindControl("hdnCountry2"));
                ddllist2.AppendDataBoundItems = true;
                
                List<ParametroGenerico> tipoUE = tipoDa.ListarTipo(clavesTipo.TIPO_UNIDAD_ESPACIAL);

                if (tipoUE != null)
                {
                    foreach (ParametroGenerico param in tipoUE)
                    {
                        ddllist2.Items.Add(new ListItem(param.descripcion, param.id.ToString()));
                    }
                }

                ddllist2.DataBind();
                ddllist2.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnCountry2.Value != null && !hdnCountry2.Value.Equals(""))
                {
                    try
                    {
                        ddllist2.Items.FindByValue(hdnCountry2.Value).Selected = true;
                    }
                    catch { }


                }
                ddllist2.Items.FindByText(hdnCountry2.Value).Selected = true;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Content_msgGrilla.Visible = false;

            int id = 0;
            switch (e.CommandName)
            {
                case "Eliminar":
                    id = Convert.ToInt32(e.CommandArgument);
                    Delete(id);
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
            Agregar.Enabled = false;
            GridView1.EditIndex = e.NewEditIndex;
            CargaGrilla();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = 0;
            DropDownList subrequerimientoOrigen = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry");
            DropDownList tipoUnidadEspacial = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry2");
            TextBox dias = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geDias");
            TextBox meses = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geMeses");

            if (subrequerimientoOrigen.SelectedItem.Value != null && !subrequerimientoOrigen.SelectedItem.Value.Equals("") &&
                tipoUnidadEspacial.SelectedItem.Value != null && !tipoUnidadEspacial.SelectedItem.Value.Equals("") && 
                (!dias.Text.Equals("") || !meses.Text.Equals("")) )
            {
                id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                Agregar.Enabled = true;
                Update(id, subrequerimientoOrigen.SelectedItem.Value, tipoUnidadEspacial.SelectedItem.Value, dias.Text, meses.Text);
            }
            else
            {
                Content_msgGrilla.Visible = true;
                msgGrilla.Text = "Para modificar debe ingresar los plazos documentos.";
            };
            CargaGrilla();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Agregar.Enabled = true;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void Update(int id, string subRequerimientoOrigen, string tipoUnidadEspacial, string dias, string meses)
        {
            PlazoDocumentoSolicitud plazoDocumentoSolicitud = new PlazoDocumentoSolicitud();

            plazoDocumentoSolicitud.idPlazoDoc = id;
            plazoDocumentoSolicitud.docOrigen = new ParametroGenerico();
            plazoDocumentoSolicitud.docOrigen.id = Convert.ToInt32(subRequerimientoOrigen);

            plazoDocumentoSolicitud.tipoUE = new ParametroGenerico();
            plazoDocumentoSolicitud.tipoUE.id = Convert.ToInt32(tipoUnidadEspacial);

            plazoDocumentoSolicitud.plazoDias = Convert.ToInt32(dias);
            plazoDocumentoSolicitud.plazoMeses = Convert.ToInt32(meses);

            
            DataTable dt = mantenedorGeneralService.actualizarPlazosDocumentos(plazoDocumentoSolicitud);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);
                if (msg == "OK")
                {
                    mensaje = "Los plazos de documentos con ID:" + plazoDocumentoSolicitud.idPlazoDoc + " ha sido actualizada.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    GridView1.EditIndex = -1;
                }
                else
                {
                    mensaje = "Los plazos de documentos '" + plazoDocumentoSolicitud.idPlazoDoc + "' ya existe.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar actualizar los plazos de documentos.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void Delete(int id)
        {
            DataTable dt = mantenedorGeneralService.eliminarPlazosDocumentos(id);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);

                switch (msg)
                {
                    case "OK":
                        mensaje = "Los plazos documentos ha sido eliminado.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        break;
                    case "En uso":
                        mensaje = "Los plazos documentos que intenta borrar está actualmente en uso.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                    default:
                        mensaje = "Se ha producido un error al intentar eliminar los plazos documentos.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar los plazos documentos.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();
            string nom_grilla = "plazosDocumentos";
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "plazosDocumentos":
                    GridView1.Columns.RemoveAt(5);
                    grilla = GridView1;
                    ngrilla = "plazosDocumentos.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            PlazoDocumentoSolicitud plazoDocumentoSolicitud = new PlazoDocumentoSolicitud();
            plazoDocumentoSolicitud.docOrigen = new ParametroGenerico();
            plazoDocumentoSolicitud.docOrigen.id = Convert.ToInt32(SubrequerimientoOrigen.SelectedItem.Value);

            plazoDocumentoSolicitud.tipoUE = new ParametroGenerico();
            plazoDocumentoSolicitud.tipoUE.id = Convert.ToInt32(TipoUnidadEspacial.SelectedItem.Value);

            if (PlazoDias.Text != null && !PlazoDias.Text.Equals(""))
            {
                plazoDocumentoSolicitud.plazoDias = Convert.ToInt32(PlazoDias.Text);
            }
            else
            {
                plazoDocumentoSolicitud.plazoDias = -1;
            }

            if (PlazoMeses.Text != null && !PlazoMeses.Text.Equals(""))
            {
                plazoDocumentoSolicitud.plazoMeses = Convert.ToInt32(PlazoMeses.Text);
            }
            else
            {
                plazoDocumentoSolicitud.plazoMeses = -1;
            }

            if (plazoDocumentoSolicitud != null)
            {
                List<PlazoDocumentoSolicitud> dt = mantenedorGeneralService.listarPlazosDocumentos(plazoDocumentoSolicitud);

                GridView1.DataSource = dt;
                GridView1.DataBind();

                if (dt != null && dt.Count > 0)
                {
                    ExportarGrilla.Visible = true;
                }
                else
                {
                    ExportarGrilla.Visible = false;
                }

                Content_msgGrilla.Visible = false;
                msgGrilla.Text = "";
                upd2.Update();
            }
        }

    }
}