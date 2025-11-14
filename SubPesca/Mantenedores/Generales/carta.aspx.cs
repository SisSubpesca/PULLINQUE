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

namespace SubPesca.Mantenedores.Generales
{
    public partial class carta : System.Web.UI.Page
    {
        Datos.Entidades.Usuario usuarios = new Datos.Entidades.Usuario();
        RegionDA regionDA = new RegionDA();
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

                //Se carga la lista de regiones
                CargarCombobox("Region");

                //Se carga la lista de TipoCarta
                CargarCombobox("TipoCarta");

                //Se carga la lista de Estado
                CargarCombobox("Estado");

                //Se carga la lista de Datum
                CargarCombobox("Datum");

                //Se carga la lista de TipoHuso
                CargarCombobox("TipoHuso");

                //Se carga la lista de AnioEdicion
                CargarCombobox("AnioEdicion");

                //Se carga la lista de AAA
                CargarCombobox("AAA");

                // Cargamos la grilla
                CargaGrilla();
            };
        }

        private void CargarCombobox(string combobox)
        {
            switch (combobox)
            {
                case "Region":
                    Region.Items.Clear();
                    Region.DataSource = regionDA.ListarRegion(0);
                    Region.DataTextField = "Region";
                    Region.DataValueField = "IdRegion";
                    Region.DataBind();
                    Region.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "TipoCarta":
                    TipoCarta.Items.Clear();
                    TipoCarta.DataSource = mantenedorGeneralService.listarTipoCarta(new ParametroGenerico());
                    TipoCarta.DataTextField = "descripcion";
                    TipoCarta.DataValueField = "id";
                    TipoCarta.DataBind();
                    TipoCarta.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;
                case "Estado":
                    Estado.Items.Clear();
                    Estado.Items.Add(new ListItem("Vigente", Convert.ToString(rbEstadosGenerales.VIGENTE)));
                    Estado.Items.Add(new ListItem("No Vigente", Convert.ToString(rbEstadosGenerales.NO_VIGENTE)));
                    Estado.Items.Add(new ListItem("Regularización", Convert.ToString(rbEstadosGenerales.REGULARIZACION)));
                    Estado.DataBind();
                    Estado.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "Datum":
                    Datum.Items.Clear();
                    Datum.DataSource = mantenedorGeneralService.listarDatum(new ParametroGenerico());
                    Datum.DataTextField = "clave";
                    Datum.DataValueField = "id";
                    Datum.DataBind();
                    Datum.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "TipoHuso":
                    TipoHuso.Items.Clear();
                    TipoHuso.DataSource = mantenedorGeneralService.listarHuso(new ParametroGenerico());
                    TipoHuso.DataTextField = "descripcion";
                    TipoHuso.DataValueField = "id";
                    TipoHuso.DataBind();
                    TipoHuso.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "AnioEdicion":
                    AnioEdicion.Items.Clear();

                    for (int i = 1940; i < DateTime.Now.Year + 10; i++)
                    {
                        AnioEdicion.Items.Insert(0, new ListItem(Convert.ToString(i),Convert.ToString(i)));
                    }
                    
                    AnioEdicion.DataBind();
                    AnioEdicion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;
                case "AAA":

                    // Cargamos el combobox: AAA
                    AAA.Items.Clear();
                    AAA.DataBind();
                    AAA.Items.Insert(0, new ListItem("Si", "1"));
                    AAA.Items.Insert(0, new ListItem("No", "0"));
                    AAA.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
            }
        }

        // ACCIONES DE BOTONES
        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            Carta carta = new Carta();
            carta.region = new ParametroGenerico();
            carta.region.id = Convert.ToInt32(Region.SelectedItem.Value);
            carta.tipoCarta = new ParametroGenerico();
            carta.tipoCarta.id = Convert.ToInt32(TipoCarta.SelectedItem.Value);
            carta.datum = new ParametroGenerico();
            carta.datum.id = Convert.ToInt32(Datum.SelectedItem.Value);
            carta.huso = new ParametroGenerico();
            carta.huso.id = Convert.ToInt32(TipoHuso.SelectedItem.Value);
            carta.numeroCarta = nombre.Text;

            if (!NumeroEdicion.Text.Trim().Equals("")) {
                carta.numeroEdicion = Convert.ToInt32(NumeroEdicion.Text);
            }
            
            carta.anioEdicion = Convert.ToInt32(AnioEdicion.SelectedItem.Value);
            carta.escala = Escala.Text;
            //carta.a_a_a = AAA.Checked;
            carta.a_a_a_Filtro = Convert.ToInt32(AAA.SelectedItem.Value);

            if (ReemplazoCarta.Text != null && !ReemplazoCarta.Text.Equals(""))
            {
                carta.reemplazoCarta = ReemplazoCarta.Text;
            }

            if (FechaTextRecepcion.Text != null && !FechaTextRecepcion.Text.Equals(""))
            {
                carta.fechaReemplazo = Convert.ToDateTime(FechaTextRecepcion.Text);
            }

            if (observaciones.Text != null && !observaciones.Text.Equals(""))
            {
                carta.observaciones = observaciones.Text;
            }

            carta.estadoVigencia = new ParametroGenerico(Convert.ToInt32(Estado.SelectedItem.Value));

            if (carta != null)
            {
                DataTable dt = mantenedorGeneralService.guardarCarta(carta);

                try
                {
                    string msg = Convert.ToString(dt.Rows[0]["msg"]);
                    if (msg == "OK")
                    {
                        mensaje = "La carta '" + carta.numeroCarta + "' ha sido creada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        GridView1.EditIndex = -1;
                        GridView1.PageIndex = 0;

                        Region.SelectedValue = "-1";
                        TipoCarta.SelectedValue = "-1";
                        Estado.SelectedValue = "-1";
                        Datum.SelectedValue = "-1";
                        TipoHuso.SelectedValue = "-1";
                        nombre.Text = "";
                        NumeroEdicion.Text = "";
                        Escala.Text = "";
                        AAA.SelectedValue = "-1";
                        AnioEdicion.SelectedValue = "-1";
                        ReemplazoCarta.Text = "";
                        FechaTextRecepcion.Text = "";
                        observaciones.Text = "";

                        CargaGrilla();
                    }
                    else
                    {
                        mensaje = "La carta '" + carta.numeroCarta + "' ya existe.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    };
                }
                catch
                {
                    mensaje = "Se ha producido un error al intentar crear la carta.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };

            }
            else
            {
                mensaje = "Para agregar debe ingresar los datos de la carta.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            // Iniciamos la query de búsqueda y cargamos la grilla
            Carta carta = new Carta();
            carta.region = new ParametroGenerico();
            carta.region.id = Convert.ToInt32(Region.SelectedItem.Value);
            carta.tipoCarta = new ParametroGenerico();
            carta.tipoCarta.id = Convert.ToInt32(TipoCarta.SelectedItem.Value);
            carta.datum = new ParametroGenerico();
            carta.datum.id = Convert.ToInt32(Datum.SelectedItem.Value);
            carta.huso = new ParametroGenerico();
            carta.huso.id = Convert.ToInt32(TipoHuso.SelectedItem.Value);
            carta.numeroCarta = nombre.Text;

            if (!NumeroEdicion.Text.Trim().Equals(""))
            {
                carta.numeroEdicion = Convert.ToInt32(NumeroEdicion.Text);
            }

            carta.anioEdicion = Convert.ToInt32(AnioEdicion.SelectedItem.Value);
            carta.escala = Escala.Text;
            carta.a_a_a_Filtro = Convert.ToInt32(AAA.SelectedItem.Value);

            if (ReemplazoCarta.Text != null && !ReemplazoCarta.Text.Equals(""))
            {
                carta.reemplazoCarta = ReemplazoCarta.Text;
            }

            if (FechaTextRecepcion.Text != null && !FechaTextRecepcion.Text.Equals(""))
            {
                carta.fechaReemplazo = Convert.ToDateTime(FechaTextRecepcion.Text);
            }

            if (observaciones.Text != null && !observaciones.Text.Equals(""))
            {
                carta.observaciones = observaciones.Text;
            }

            carta.estadoVigencia = new ParametroGenerico(Convert.ToInt32(Estado.SelectedItem.Value));

            List<Carta> dt = mantenedorGeneralService.listarCarta(carta);

            int num_registros = 0;
            num_registros = dt.Count;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            if (dt != null && dt.Count > 0)
            {
                ExportarGrilla.Visible = true;
            }
            else {
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
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar la carta ID:" + DataBinder.Eval(e.Row.DataItem, "idCarta") + "?')");
                    boton_eliminar.Visible = true;
                };
            };
            
            if (GridView1.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddllist1 = ((DropDownList)e.Row.FindControl("ddleditCountry"));
                var hdnCountryName = ((HiddenField)e.Row.FindControl("hdnCountry"));
                ddllist1.AppendDataBoundItems = true;

                ddllist1.DataSource = regionDA.ListarRegion(0);
                ddllist1.DataTextField = "Region";
                ddllist1.DataValueField = "IdRegion";
                ddllist1.DataBind();
                ddllist1.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnCountryName.Value != null && !hdnCountryName.Value.Equals(""))
                {
                    ddllist1.Items.FindByText(hdnCountryName.Value).Selected = true;
                }

                DropDownList ddllist2 = ((DropDownList)e.Row.FindControl("ddleditCountry2"));
                var hdnCountryName2 = ((HiddenField)e.Row.FindControl("hdnCountry2"));
                ddllist2.AppendDataBoundItems = true;

                ddllist2.DataSource = mantenedorGeneralService.listarTipoCarta(new ParametroGenerico());
                ddllist2.DataTextField = "descripcion";
                ddllist2.DataValueField = "id";
                ddllist2.DataBind();
                ddllist2.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnCountryName2.Value != null && !hdnCountryName2.Value.Equals(""))
                {
                    ddllist2.Items.FindByText(hdnCountryName2.Value).Selected = true;
                }

                DropDownList ddllist3 = ((DropDownList)e.Row.FindControl("ddleditCountry3"));
                var hdnCountryName3 = ((HiddenField)e.Row.FindControl("hdnCountry3"));
                ddllist3.AppendDataBoundItems = true;

                ddllist3.DataSource = mantenedorGeneralService.listarDatum(new ParametroGenerico());
                ddllist3.DataTextField = "clave";
                ddllist3.DataValueField = "id";
                ddllist3.DataBind();
                ddllist3.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnCountryName3.Value != null && !hdnCountryName3.Value.Equals(""))
                {
                    ddllist3.Items.FindByText(hdnCountryName3.Value).Selected = true;
                }

                DropDownList ddllist4 = ((DropDownList)e.Row.FindControl("ddleditCountry4"));
                var hdnCountryName4 = ((HiddenField)e.Row.FindControl("hdnCountry4"));
                ddllist4.AppendDataBoundItems = true;

                ddllist4.DataSource = mantenedorGeneralService.listarHuso(new ParametroGenerico());
                ddllist4.DataTextField = "descripcion";
                ddllist4.DataValueField = "id";
                ddllist4.DataBind();
                ddllist4.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnCountryName4.Value != null && !hdnCountryName4.Value.Equals(""))
                {
                    ddllist4.Items.FindByText(hdnCountryName4.Value).Selected = true;
                }

                DropDownList ddllist5 = ((DropDownList)e.Row.FindControl("ddleditCountry5"));
                var hdnCountryName5 = ((HiddenField)e.Row.FindControl("hdnCountry5"));
                ddllist5.AppendDataBoundItems = true;

                for (int i = 1940; i < DateTime.Now.Year + 10; i++)
                {
                    ddllist5.Items.Insert(0, new ListItem(Convert.ToString(i), Convert.ToString(i)));
                }

                ddllist5.DataBind();
                ddllist5.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnCountryName5.Value != null && !hdnCountryName5.Value.Equals("") && !hdnCountryName5.Value.Equals("0"))
                {
                    ddllist5.Items.FindByText(hdnCountryName5.Value).Selected = true;
                }

                DropDownList ddllist10 = ((DropDownList)e.Row.FindControl("ddleditCountry10"));
                var hdnCountryName10 = ((HiddenField)e.Row.FindControl("hdnCountry10"));
                ddllist10.AppendDataBoundItems = true;

                ddllist10.Items.Add(new ListItem("Vigente", Convert.ToString(rbEstadosGenerales.VIGENTE)));
                ddllist10.Items.Add(new ListItem("No Vigente", Convert.ToString(rbEstadosGenerales.NO_VIGENTE)));
                ddllist10.Items.Add(new ListItem("Regularización", Convert.ToString(rbEstadosGenerales.REGULARIZACION)));

                ddllist10.DataBind();
                ddllist10.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                if (hdnCountryName10.Value != null && !hdnCountryName10.Value.Equals(""))
                {
                    ddllist10.Items.FindByText(hdnCountryName10.Value).Selected = true;
                }

                /* Numero Carta, Número edición, fecha reemplazo, aaa, reemplazo carta, observaciones, escala */

                TextBox numeroCarta = ((TextBox)e.Row.FindControl("geNumeroCarta"));
                var labelNumeroCarta = ((HiddenField)e.Row.FindControl("hdnNumeroCarta"));
                if (labelNumeroCarta.Value != null && !labelNumeroCarta.Value.Equals(""))
                {
                    numeroCarta.Text = labelNumeroCarta.Value;
                }

                TextBox numeroEdicion = ((TextBox)e.Row.FindControl("geNumeroEdicion"));
                var labelNumeroEdicion = ((HiddenField)e.Row.FindControl("hdnNumeroEdicion"));
                if (labelNumeroEdicion.Value != null && !labelNumeroEdicion.Value.Equals(""))
                {
                    numeroEdicion.Text = labelNumeroEdicion.Value;
                }

                TextBox fechaReemplazo = ((TextBox)e.Row.FindControl("geFechaReemplazo"));
                var labelfechaReemplazo = ((HiddenField)e.Row.FindControl("hdnFechaReemplazo"));
                if (labelfechaReemplazo.Value != null && !labelfechaReemplazo.Value.Equals("") && Convert.ToDateTime(labelfechaReemplazo.Value) != default(DateTime))
                {
                    fechaReemplazo.Text = labelfechaReemplazo.Value;
                }

                //CheckBoxList aaa = ((CheckBoxList)e.Row.FindControl("geAAA"));
                //var labelAaa = ((HiddenField)e.Row.FindControl("hdnAAA"));
                //if (labelAaa.Value != null && !labelAaa.Value.Equals(""))
                //{
                //    aaa.Text = labelAaa.Value;
                //}

                TextBox reemplazoCarta = ((TextBox)e.Row.FindControl("geReemplazoCarta"));
                var labelreemplazoCarta = ((HiddenField)e.Row.FindControl("hdnReemplazoCarta"));
                if (labelreemplazoCarta.Value != null && !labelreemplazoCarta.Value.Equals(""))
                {
                    reemplazoCarta.Text = labelreemplazoCarta.Value;
                }

                TextBox observaciones = ((TextBox)e.Row.FindControl("geObservaciones"));
                var labeObservaciones = ((HiddenField)e.Row.FindControl("hdnObservaciones"));
                if (labeObservaciones.Value != null && !labeObservaciones.Value.Equals(""))
                {
                    observaciones.Text = labeObservaciones.Value;
                }

                TextBox escala = ((TextBox)e.Row.FindControl("geEscala"));
                var labelEscala = ((HiddenField)e.Row.FindControl("hdnEscala"));
                if (labelEscala.Value != null && !labelEscala.Value.Equals(""))
                {
                    escala.Text = labelEscala.Value;
                }

                DropDownList ddllist20 = ((DropDownList)e.Row.FindControl("ddleditCountry20"));
                var hdnCountryName20 = ((HiddenField)e.Row.FindControl("hdnCountry20"));
                ddllist20.AppendDataBoundItems = true;

                ddllist20.Items.Clear();
                ddllist20.DataBind();
                ddllist20.Items.Insert(0, new ListItem("Si", "1"));
                ddllist20.Items.Insert(0, new ListItem("No", "0"));
                ddllist20.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                ddllist20.Items.FindByText(hdnCountryName20.Value).Selected = true;

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

            DropDownList region = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry");
            DropDownList tipoCarta = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry2");
            DropDownList datum = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry3");
            DropDownList huso = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry4");
            DropDownList estado = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry10");
            TextBox numeroCarta = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geNumeroCarta");
            TextBox numeroEdicion = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geNumeroEdicion");
            TextBox fechaReemplazo = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geFechaReemplazo");
            DropDownList anioEdicion = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry5");
            DropDownList aaa = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddleditCountry20");
            //CheckBoxList aaa = (CheckBoxList)GridView1.Rows[e.RowIndex].FindControl("geAAA");
            TextBox reemplazoCarta = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geReemplazoCarta");
            TextBox observaciones = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geObservaciones");
            TextBox escala = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geEscala");

            if (region.SelectedValue != "-1" && tipoCarta.SelectedValue != "-1" && datum.SelectedValue != "-1" && estado.SelectedValue != "-1"
                && numeroCarta.Text != "" && numeroEdicion.Text != null && anioEdicion.SelectedValue != "-1" && escala.Text != "")
            {
                id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                Agregar.Enabled = true;
                Update(id, region.SelectedItem.Value, tipoCarta.SelectedItem.Value, datum.SelectedItem.Value, huso.SelectedItem.Value, estado.SelectedItem.Value, anioEdicion.SelectedItem.Value,
                    numeroCarta.Text, numeroEdicion.Text, reemplazoCarta.Text, observaciones.Text, aaa.SelectedItem.Value, fechaReemplazo.Text, escala.Text);
            }
            else
            {
                Content_msgGrilla.Visible = true;
                msgGrilla.Text = "Para modificar debe ingresar los datos requeridos de la carta";
            };
            CargaGrilla();

        }

        private void Update(int id, string idRegion, string idTipoCarta, string idDatum, string idHuso, string idEstado, string idAnio, string numeroCarta, string numeroEdicion, string reemplazoCarta, string observaciones, string aaa, string fechaReemplazo, string escala)
        {

            Carta carta = new Carta();
            carta.idCarta = id;
            carta.region = new ParametroGenerico(Convert.ToInt32(idRegion));
            carta.tipoCarta = new ParametroGenerico(Convert.ToInt32(idTipoCarta));
            carta.datum = new ParametroGenerico(Convert.ToInt32(idDatum));
            carta.anioEdicion = Convert.ToInt32(idAnio);
            carta.numeroCarta = numeroCarta;
            carta.numeroEdicion = Convert.ToInt32(numeroEdicion);

            if (fechaReemplazo != null && !fechaReemplazo.Equals(""))
            {
                carta.fechaReemplazo = Convert.ToDateTime(fechaReemplazo);
            }

            carta.huso = new ParametroGenerico(Convert.ToInt32(idHuso));
            carta.escala = escala;
            carta.a_a_a_Filtro = Convert.ToInt32(aaa);

            //if (aaa != null && aaa.Value.Equals("Si"))
            //{
            //    carta.a_a_a = true;
            //}
            //else {
            //    carta.a_a_a = false;
            //}

            carta.reemplazoCarta = reemplazoCarta;
            carta.observaciones = observaciones;
            //carta.estadoVigencia = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
            carta.estadoVigencia = new ParametroGenerico(Convert.ToInt32(idEstado));

            DataTable dt = mantenedorGeneralService.actualizarCarta(carta);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);
                if (msg == "OK")
                {
                    mensaje = "La carta con ID:" + id + " ha sido actualizada.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    GridView1.EditIndex = -1;
                }
                else
                {
                    mensaje = "La carta '" + nombre + "' ya existe.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar actualizar la carta.";
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
            DataTable dt = mantenedorGeneralService.eliminarCarta(id);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);

                switch (msg)
                {
                    case "OK":
                        mensaje = "La carta ha sido eliminada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        break;
                    case "En uso":
                        mensaje = "La carta que intenta borrar está actualmente en uso.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                    default:
                        mensaje = "Se ha producido un error al intentar eliminar la carta.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar la carta.";
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
            string nom_grilla = "carta";
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "carta":
                    GridView1.Columns.RemoveAt(14);
                    grilla = GridView1;
                    ngrilla = "carta.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            Carta carta = new Carta();
            carta.region = new ParametroGenerico();
            carta.region.id = Convert.ToInt32(Region.SelectedItem.Value);
            carta.tipoCarta = new ParametroGenerico();
            carta.tipoCarta.id = Convert.ToInt32(TipoCarta.SelectedItem.Value);
            carta.datum = new ParametroGenerico();
            carta.datum.id = Convert.ToInt32(Datum.SelectedItem.Value);
            carta.huso = new ParametroGenerico();
            carta.huso.id = Convert.ToInt32(TipoHuso.SelectedItem.Value);
            carta.numeroCarta = nombre.Text;

            if (!NumeroEdicion.Text.Trim().Equals("")) {
                carta.numeroEdicion = Convert.ToInt32(NumeroEdicion.Text);
            }
            
            carta.anioEdicion = Convert.ToInt32(AnioEdicion.SelectedItem.Value);
            carta.escala = Escala.Text;
            carta.a_a_a_Filtro = Convert.ToInt32(AAA.SelectedItem.Value);

            if (ReemplazoCarta.Text != null && !ReemplazoCarta.Text.Equals(""))
            {
                carta.reemplazoCarta = ReemplazoCarta.Text;
            }

            if (FechaTextRecepcion.Text != null && !FechaTextRecepcion.Text.Equals(""))
            {
                carta.fechaReemplazo = Convert.ToDateTime(FechaTextRecepcion.Text);
            }

            if (observaciones.Text != null && !observaciones.Text.Equals(""))
            {
                carta.observaciones = observaciones.Text;
            }

            carta.estadoVigencia = new ParametroGenerico(Convert.ToInt32(Estado.SelectedItem.Value));

            if (carta != null)
            {
                List<Carta> dt = mantenedorGeneralService.listarCarta(carta);

                GridView1.DataSource = dt;
                GridView1.DataBind();

                if (dt != null && dt.Count > 0)
                {
                    ExportarGrilla.Visible = true;
                }
                else {
                    ExportarGrilla.Visible = false;
                }

                Content_msgGrilla.Visible = false;
                msgGrilla.Text = "";
                upd2.Update();
            }
        }

    }
}