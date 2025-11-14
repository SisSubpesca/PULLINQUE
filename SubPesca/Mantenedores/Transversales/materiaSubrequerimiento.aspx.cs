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
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Contantes;
using SubPesca.Mantenedores.Generales;
using Datos.Entidades.Resolucion;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace SubPesca.Mantenedores.Transversales
{
    public partial class materiaSubrequerimiento : System.Web.UI.Page
    {
        Datos.Entidades.Usuario usuarios = new Datos.Entidades.Usuario();

        TipoDA tipoDA = new TipoDA();
        RequerimientoDA requerimientoDA = new RequerimientoDA();
        MantenedorDA mantenedorDA = new MantenedorDA();

        MantenedorGeneralService mantenedorGeneralService = new MantenedorGeneralService();


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

                //Se carga la lista de TipoDocumento
                CargarCombobox("TipoDocumento");

                //Se carga la lista de TipoDestinatario
                CargarCombobox("TipoDestinatario");

                //Se carga la lista de TipoMateria
                CargarCombobox("TipoMateria");

                //Se carga la lista de Subrequerimiento
                CargarCombobox("Subrequerimiento");

                //Se carga la lista de EstadoMateria
                CargarCombobox("EstadoMateria");

                //Se carga la lista de EstadoSubrequerimiento
                CargarCombobox("EstadoSubrequerimiento");

                //Se carga la lista de numero
                CargarCombobox("numero");

                //Se carga la lista de fecha
                CargarCombobox("fecha");

                //Se carga la lista de numeroCI
                CargarCombobox("numeroCI");

                //Se carga la lista de fechaCI
                CargarCombobox("fechaCI");

                // Cargamos la grilla
                CargaGrilla();
            };
        }

        private void CargarCombobox(string combobox)
        {
            switch (combobox)
            {
                case "TipoDocumento":
                    TipoDocumento.Items.Clear();

                    ParametroGenerico parametroGenericoFiltro = new ParametroGenerico();
                    parametroGenericoFiltro.clave = "TIPO_DOCUMENTO";

                    TipoDocumento.DataSource = mantenedorDA.ListarTipo_Mantenedor(parametroGenericoFiltro);
                    TipoDocumento.DataTextField = "descripcion";
                    TipoDocumento.DataValueField = "id";
                    TipoDocumento.DataBind();
                    TipoDocumento.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "TipoDestinatario":
                    TipoDestinatario.Items.Clear();
                    TipoDestinatario.DataSource = tipoDA.listarTipoDestinatario();
                    TipoDestinatario.DataTextField = "descripcion";
                    TipoDestinatario.DataValueField = "id";
                    TipoDestinatario.DataBind();
                    TipoDestinatario.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "TipoMateria":
                    TipoMateria.Items.Clear();
                    TipoMateria.DataSource = mantenedorDA.ListarMateria_Mantenedor(new ParametroGenerico());
                    TipoMateria.DataTextField = "descripcion";
                    TipoMateria.DataValueField = "id";
                    TipoMateria.DataBind();
                    TipoMateria.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "Subrequerimiento":
                    Subrequerimiento.Items.Clear();

                    SubRequerimiento subRequerimientoFiltro = new SubRequerimiento();
                    subRequerimientoFiltro.aplicaReiteraFiltro = -1;
                    subRequerimientoFiltro.aplicaComplementarioFiltro = -1;
                    subRequerimientoFiltro.aplicaVisacionMasivaFiltro = -1;

                    Subrequerimiento.DataSource = requerimientoDA.ListarSubRequerimiento(subRequerimientoFiltro);
                    Subrequerimiento.DataTextField = "nombreSubRequerimiento";
                    Subrequerimiento.DataValueField = "idSubRequerimiento";
                    Subrequerimiento.DataBind();
                    Subrequerimiento.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

                case "EstadoMateria":
                    EstadoMateria.Items.Clear();
                    EstadoMateria.DataSource = mantenedorGeneralService.listarResultado(new ParametroGenerico());
                    EstadoMateria.DataTextField = "descripcion";
                    EstadoMateria.DataValueField = "id";
                    EstadoMateria.DataBind();
                    EstadoMateria.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "EstadoSubrequerimiento":
                    EstadoSubrequerimiento.Items.Clear();
                    EstadoSubrequerimiento.DataSource = mantenedorGeneralService.listarResultado(new ParametroGenerico());
                    EstadoSubrequerimiento.DataTextField = "descripcion";
                    EstadoSubrequerimiento.DataValueField = "id";
                    EstadoSubrequerimiento.DataBind();
                    EstadoSubrequerimiento.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "numero":
                    numero.Items.Clear();
                    numero.DataBind();
                    numero.Items.Insert(0, new ListItem("Campo No Aplica", "0"));
                    numero.Items.Insert(0, new ListItem("Campo Aplica", "1"));
                    numero.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

                case "fecha":
                    fecha.Items.Clear();
                    fecha.DataBind();
                    fecha.Items.Insert(0, new ListItem("Campo No Aplica", "0"));
                    fecha.Items.Insert(0, new ListItem("Campo Aplica", "1"));
                    fecha.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

                case "numeroCI":
                    numeroCI.Items.Clear();
                    numeroCI.DataBind();
                    numeroCI.Items.Insert(0, new ListItem("Campo No Aplica", "0"));
                    numeroCI.Items.Insert(0, new ListItem("Campo Aplica", "1"));
                    numeroCI.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

                case "fechaCI":
                    fechaCI.Items.Clear();
                    fechaCI.DataBind();
                    fechaCI.Items.Insert(0, new ListItem("Campo No Aplica", "0"));
                    fechaCI.Items.Insert(0, new ListItem("Campo Aplica", "1"));
                    fechaCI.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;
            }
        }

        // ACCIONES DE BOTONES
        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            ResolucionValidacion resolucionValidacion = new ResolucionValidacion();
            resolucionValidacion.tipoDocumento = new ParametroGenerico();
            resolucionValidacion.tipoDocumento.id = Convert.ToInt32(TipoDocumento.SelectedItem.Value);

            resolucionValidacion.origen = new ParametroGenerico();
            resolucionValidacion.origen.id = Convert.ToInt32(TipoDestinatario.SelectedItem.Value);

            resolucionValidacion.materia = new ParametroGenerico();
            resolucionValidacion.materia.id = Convert.ToInt32(TipoMateria.SelectedItem.Value);

            resolucionValidacion.subRequerimiento = new ParametroGenerico();
            resolucionValidacion.subRequerimiento.id = Convert.ToInt32(Subrequerimiento.SelectedItem.Value);

            resolucionValidacion.estadoMateria = new ParametroGenerico();
            resolucionValidacion.estadoMateria.id = Convert.ToInt32(EstadoMateria.SelectedItem.Value);

            resolucionValidacion.estadoSubRequerimiento = new ParametroGenerico();
            resolucionValidacion.estadoSubRequerimiento.id = Convert.ToInt32(EstadoSubrequerimiento.SelectedItem.Value);

            resolucionValidacion.numero = Convert.ToInt32(numero.SelectedItem.Value);

            resolucionValidacion.fecha = Convert.ToInt32(fecha.SelectedItem.Value);

            resolucionValidacion.numeroCI = Convert.ToInt32(numeroCI.SelectedItem.Value);

            resolucionValidacion.fechaCI = Convert.ToInt32(fechaCI.SelectedItem.Value);

            if (resolucionValidacion != null)
            {
                DataTable dt = mantenedorGeneralService.guardarMateriaSubrequerimiento(resolucionValidacion);

                try
                {
                    string msg = Convert.ToString(dt.Rows[0]["msg"]);
                    if (msg == "OK")
                    {
                        mensaje = "La materia subrequerimiento ha sido creada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        GridView1.EditIndex = -1;
                        GridView1.PageIndex = 0;

                        TipoDocumento.SelectedValue = "-1";
                        TipoDestinatario.SelectedValue = "-1";
                        TipoMateria.SelectedValue = "-1";
                        Subrequerimiento.SelectedValue = "-1";
                        EstadoMateria.SelectedValue = "-1";
                        EstadoSubrequerimiento.SelectedValue = "-1";
                        numero.SelectedValue = "-1";
                        numeroCI.SelectedValue = "-1";
                        fecha.SelectedValue = "-1";
                        fechaCI.SelectedValue = "-1";

                        CargaGrilla();
                    }
                    else
                    {
                        mensaje = "La materia subrequerimiento ya existe.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    };
                }
                catch
                {
                    mensaje = "Se ha producido un error al intentar crear la materia Subrequerimiento.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };

            }
            else
            {
                mensaje = "Para agregar debe ingresar los datos de la materia Subrequerimiento.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            // Iniciamos la query de búsqueda y cargamos la grilla
            ResolucionValidacion resolucionValidacion = new ResolucionValidacion();

            resolucionValidacion.tipoDocumento = new ParametroGenerico();
            resolucionValidacion.tipoDocumento.id = Convert.ToInt32(TipoDocumento.SelectedItem.Value);

            resolucionValidacion.origen = new ParametroGenerico();
            resolucionValidacion.origen.id = Convert.ToInt32(TipoDestinatario.SelectedItem.Value);

            resolucionValidacion.materia = new ParametroGenerico();
            resolucionValidacion.materia.id = Convert.ToInt32(TipoMateria.SelectedItem.Value);

            resolucionValidacion.subRequerimiento = new ParametroGenerico();
            resolucionValidacion.subRequerimiento.id = Convert.ToInt32(Subrequerimiento.SelectedItem.Value);

            resolucionValidacion.estadoMateria = new ParametroGenerico();
            resolucionValidacion.estadoMateria.id = Convert.ToInt32(EstadoMateria.SelectedItem.Value);

            resolucionValidacion.estadoSubRequerimiento = new ParametroGenerico();
            resolucionValidacion.estadoSubRequerimiento.id = Convert.ToInt32(EstadoSubrequerimiento.SelectedItem.Value);

            resolucionValidacion.numero = Convert.ToInt32(numero.SelectedItem.Value);

            resolucionValidacion.fecha = Convert.ToInt32(fecha.SelectedItem.Value);

            resolucionValidacion.numeroCI = Convert.ToInt32(numeroCI.SelectedItem.Value);

            resolucionValidacion.fechaCI = Convert.ToInt32(fechaCI.SelectedItem.Value);

            List<ResolucionValidacion> dt = mantenedorGeneralService.listarMateriaSubrequerimiento(resolucionValidacion);

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
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar la materia seubrequerimiento ID:" + DataBinder.Eval(e.Row.DataItem, "idEquivalencia") + "?')");
                    boton_eliminar.Visible = true;
                };
            };

            if (GridView1.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
            {

                //Tipo Documento
                DropDownList ddllist1 = ((DropDownList)e.Row.FindControl("ddleditCountry"));
                var hdnCountryName = ((HiddenField)e.Row.FindControl("hdnCountry"));
                ddllist1.AppendDataBoundItems = true;

                ParametroGenerico parametroGenericoFiltro = new ParametroGenerico();
                parametroGenericoFiltro.clave = "TIPO_DOCUMENTO";

                ddllist1.DataSource = mantenedorDA.ListarTipo_Mantenedor(parametroGenericoFiltro);
                ddllist1.DataTextField = "descripcion";
                ddllist1.DataValueField = "id";
                ddllist1.DataBind();
                ddllist1.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnCountryName.Value != null && !hdnCountryName.Value.Equals(""))
                {
                    ddllist1.Items.FindByText(hdnCountryName.Value).Selected = true;
                }

                //Tipo Destinatario
                DropDownList ddllist2 = ((DropDownList)e.Row.FindControl("ddleditCountry2"));
                var hdnCountryName2 = ((HiddenField)e.Row.FindControl("hdnCountry2"));
                ddllist2.AppendDataBoundItems = true;

                ddllist2.DataSource = tipoDA.listarTipoDestinatario();
                ddllist2.DataTextField = "descripcion";
                ddllist2.DataValueField = "id";
                ddllist2.DataBind();
                ddllist2.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnCountryName2.Value != null && !hdnCountryName2.Value.Equals(""))
                {
                    ddllist2.Items.FindByText(hdnCountryName2.Value).Selected = true;
                }

                //Tipo Materia
                DropDownList ddllist3 = ((DropDownList)e.Row.FindControl("ddleditCountry3"));
                var hdnCountryName3 = ((HiddenField)e.Row.FindControl("hdnCountry3"));
                ddllist3.AppendDataBoundItems = true;

                ddllist3.DataSource = mantenedorDA.ListarMateria_Mantenedor(new ParametroGenerico());
                ddllist3.DataTextField = "descripcion";
                ddllist3.DataValueField = "id";
                ddllist3.DataBind();
                ddllist3.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnCountryName3.Value != null && !hdnCountryName3.Value.Equals(""))
                {
                    ddllist3.Items.FindByText(hdnCountryName3.Value).Selected = true;
                }

                //Subrequerimiento
                DropDownList ddllist4 = ((DropDownList)e.Row.FindControl("ddleditCountry4"));
                var hdnCountryName4 = ((HiddenField)e.Row.FindControl("hdnCountry4"));
                ddllist4.AppendDataBoundItems = true;

                SubRequerimiento subRequerimientoFiltro = new SubRequerimiento();
                subRequerimientoFiltro.aplicaReiteraFiltro = -1;
                subRequerimientoFiltro.aplicaComplementarioFiltro = -1;
                subRequerimientoFiltro.aplicaVisacionMasivaFiltro = -1;

                ddllist4.DataSource = requerimientoDA.ListarSubRequerimiento(subRequerimientoFiltro);
                ddllist4.DataTextField = "nombreSubRequerimiento";
                ddllist4.DataValueField = "idSubRequerimiento";
                ddllist4.DataBind();
                ddllist4.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnCountryName4.Value != null && !hdnCountryName4.Value.Equals(""))
                {
                    ddllist4.Items.FindByText(hdnCountryName4.Value).Selected = true;
                }

                //Estado Materia
                DropDownList ddllist5 = ((DropDownList)e.Row.FindControl("ddleditCountry5"));
                var hdnCountryName5 = ((HiddenField)e.Row.FindControl("hdnCountry5"));
                ddllist5.AppendDataBoundItems = true;
                
                ddllist5.DataSource = mantenedorGeneralService.listarResultado(new ParametroGenerico());
                ddllist5.DataTextField = "descripcion";
                ddllist5.DataValueField = "id";
                ddllist5.DataBind();
                ddllist5.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnCountryName5.Value != null && !hdnCountryName5.Value.Equals(""))
                {
                    ddllist5.Items.FindByText(hdnCountryName5.Value).Selected = true;
                }

                //Estado Subrequerimiento
                DropDownList ddllist6 = ((DropDownList)e.Row.FindControl("ddleditCountry6"));
                var hdnCountryName6 = ((HiddenField)e.Row.FindControl("hdnCountry6"));
                ddllist6.AppendDataBoundItems = true;
                
                ddllist6.DataSource = mantenedorGeneralService.listarResultado(new ParametroGenerico());
                ddllist6.DataTextField = "descripcion";
                ddllist6.DataValueField = "id";
                ddllist6.DataBind();
                ddllist6.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnCountryName6.Value != null && !hdnCountryName6.Value.Equals(""))
                {
                    ddllist6.Items.FindByText(hdnCountryName6.Value).Selected = true;
                }

                /* Aplica Número */
                DropDownList ddllist7 = ((DropDownList)e.Row.FindControl("ddleditCountry7"));
                var hdnCountryName7 = ((HiddenField)e.Row.FindControl("hdnCountry7"));
                ddllist7.AppendDataBoundItems = true;

                ddllist7.Items.Clear();
                ddllist7.DataBind();
                ddllist7.Items.Insert(0, new ListItem("Campo No Aplica", "0"));
                ddllist7.Items.Insert(0, new ListItem("Campo Aplica", "1"));
                ddllist7.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                ddllist7.Items.FindByText(hdnCountryName7.Value).Selected = true;

                /* Aplica Fecha */
                DropDownList ddllist8 = ((DropDownList)e.Row.FindControl("ddleditCountry8"));
                var hdnCountryName8 = ((HiddenField)e.Row.FindControl("hdnCountry8"));
                ddllist8.AppendDataBoundItems = true;

                ddllist8.Items.Clear();
                ddllist8.DataBind();
                ddllist8.Items.Insert(0, new ListItem("Campo No Aplica", "0"));
                ddllist8.Items.Insert(0, new ListItem("Campo Aplica", "1"));
                ddllist8.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                ddllist8.Items.FindByText(hdnCountryName8.Value).Selected = true;

                /* Aplica N° CI */
                DropDownList ddllist9 = ((DropDownList)e.Row.FindControl("ddleditCountry9"));
                var hdnCountryName9 = ((HiddenField)e.Row.FindControl("hdnCountry9"));
                ddllist9.AppendDataBoundItems = true;

                ddllist9.Items.Clear();
                ddllist9.DataBind();
                ddllist9.Items.Insert(0, new ListItem("Campo No Aplica", "0"));
                ddllist9.Items.Insert(0, new ListItem("Campo Aplica", "1"));
                ddllist9.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                ddllist9.Items.FindByText(hdnCountryName9.Value).Selected = true;

                /* Aplica Fecha CI */
                DropDownList ddllist10 = ((DropDownList)e.Row.FindControl("ddleditCountry10"));
                var hdnCountryName10 = ((HiddenField)e.Row.FindControl("hdnCountry10"));
                ddllist10.AppendDataBoundItems = true;

                ddllist10.Items.Clear();
                ddllist10.DataBind();
                ddllist10.Items.Insert(0, new ListItem("Campo No Aplica", "0"));
                ddllist10.Items.Insert(0, new ListItem("Campo Aplica", "1"));
                ddllist10.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                ddllist10.Items.FindByText(hdnCountryName10.Value).Selected = true;
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

            DropDownList tipoDocumento = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry");
            DropDownList tipoDestinatario = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry2");
            DropDownList tipoMateria = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry3");
            DropDownList subrequerimiento = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry4");
            DropDownList estadoMateria = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry5");
            DropDownList estadoSubrequerimiento = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry6");

            DropDownList numero = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry7");
            DropDownList fecha = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry8");
            DropDownList numeroCI = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry9");
            DropDownList fechaCI = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry10");


            if (tipoDocumento.SelectedValue != "-1" && tipoDestinatario.SelectedValue != "-1" && tipoMateria.SelectedValue != "-1" && subrequerimiento.SelectedValue != "-1" && estadoMateria.SelectedValue != "-1"
                && numero.SelectedValue != "-1" && numeroCI.SelectedValue != "-1" && fecha.SelectedValue != "-1" && fechaCI.SelectedValue != "-1")
            {
                id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                Agregar.Enabled = true;
                Update(id, tipoDocumento.SelectedItem.Value, tipoDestinatario.SelectedItem.Value, tipoMateria.SelectedItem.Value, subrequerimiento.SelectedItem.Value, estadoMateria.SelectedItem.Value, estadoSubrequerimiento.SelectedItem.Value,
                    numero.SelectedItem.Value, numeroCI.SelectedItem.Value, fecha.SelectedItem.Value, fechaCI.SelectedItem.Value);
            }
            else
            {
                Content_msgGrilla.Visible = true;
                msgGrilla.Text = "Para modificar debe ingresar los datos requeridos de la materia subrequerimiento.";
            };
            CargaGrilla();

        }

        private void Update(int id, string tipoDocumento, string tipoDestinatario, string tipoMateria, string subrequerimiento, string estadoMateria, string estadoSubrequerimiento, string numero, string numeroCI, string fecha, string fechaCI)
        {

            ResolucionValidacion resolucionValidacion = new ResolucionValidacion();
            resolucionValidacion.idEquivalencia = id;

            resolucionValidacion.tipoDocumento = new ParametroGenerico();
            resolucionValidacion.tipoDocumento.id = Convert.ToInt32(tipoDocumento);

            resolucionValidacion.origen = new ParametroGenerico();
            resolucionValidacion.origen.id = Convert.ToInt32(tipoDestinatario);

            resolucionValidacion.materia = new ParametroGenerico();
            resolucionValidacion.materia.id = Convert.ToInt32(tipoMateria);

            resolucionValidacion.subRequerimiento = new ParametroGenerico();
            resolucionValidacion.subRequerimiento.id = Convert.ToInt32(subrequerimiento);

            resolucionValidacion.estadoMateria = new ParametroGenerico();
            resolucionValidacion.estadoMateria.id = Convert.ToInt32(estadoMateria);

            resolucionValidacion.estadoSubRequerimiento = new ParametroGenerico();
            resolucionValidacion.estadoSubRequerimiento.id = Convert.ToInt32(estadoSubrequerimiento);

            resolucionValidacion.numero = Convert.ToInt32(numero);

            resolucionValidacion.fecha = Convert.ToInt32(fecha);

            resolucionValidacion.numeroCI = Convert.ToInt32(numeroCI);

            resolucionValidacion.fechaCI = Convert.ToInt32(fechaCI);

            DataTable dt = mantenedorGeneralService.actualizarMateriaSubrequerimiento(resolucionValidacion);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);
                if (msg == "OK")
                {
                    mensaje = "La materia subrequerimiento con ID:" + resolucionValidacion.idEquivalencia + " ha sido actualizada.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    GridView1.EditIndex = -1;
                }
                else
                {
                    mensaje = "La materia subrequerimiento '" + resolucionValidacion.materia.descripcion + "' ya existe.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar actualizar la materia subrequerimiento.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Agregar.Enabled = true;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void Delete(int id)
        {
            DataTable dt = mantenedorGeneralService.eliminarMateriaSubrequerimiento(id);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);

                switch (msg)
                {
                    case "OK":
                        mensaje = "La materia subrequerimiento ha sido eliminada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        break;
                    case "En uso":
                        mensaje = "La materia subrequerimiento que intenta borrar está actualmente en uso.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                    default:
                        mensaje = "Se ha producido un error al intentar eliminar la materia subrequerimiento.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar la materia subrequerimiento.";
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
            string nom_grilla = "materiaSubrequerimiento";
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "materiaSubrequerimiento":
                    GridView1.Columns.RemoveAt(11);
                    grilla = GridView1;
                    ngrilla = "materiaSubrequerimiento.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            ResolucionValidacion resolucionValidacion = new ResolucionValidacion();

            resolucionValidacion.tipoDocumento = new ParametroGenerico();
            resolucionValidacion.tipoDocumento.id = Convert.ToInt32(TipoDocumento.SelectedItem.Value);

            resolucionValidacion.origen = new ParametroGenerico();
            resolucionValidacion.origen.id = Convert.ToInt32(TipoDestinatario.SelectedItem.Value);

            resolucionValidacion.materia = new ParametroGenerico();
            resolucionValidacion.materia.id = Convert.ToInt32(TipoMateria.SelectedItem.Value);

            resolucionValidacion.subRequerimiento = new ParametroGenerico();
            resolucionValidacion.subRequerimiento.id = Convert.ToInt32(Subrequerimiento.SelectedItem.Value);

            resolucionValidacion.estadoMateria = new ParametroGenerico();
            resolucionValidacion.estadoMateria.id = Convert.ToInt32(EstadoMateria.SelectedItem.Value);

            resolucionValidacion.estadoSubRequerimiento = new ParametroGenerico();
            resolucionValidacion.estadoSubRequerimiento.id = Convert.ToInt32(EstadoSubrequerimiento.SelectedItem.Value);

            resolucionValidacion.numero = Convert.ToInt32(numero.SelectedItem.Value);

            resolucionValidacion.fecha = Convert.ToInt32(fecha.SelectedItem.Value);

            resolucionValidacion.numeroCI = Convert.ToInt32(numeroCI.SelectedItem.Value);

            resolucionValidacion.fechaCI = Convert.ToInt32(fechaCI.SelectedItem.Value);

            if (resolucionValidacion != null)
            {
                List<ResolucionValidacion> dt = mantenedorGeneralService.listarMateriaSubrequerimiento(resolucionValidacion);

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