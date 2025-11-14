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

namespace SubPesca.Mantenedores.Transversales
{
    public partial class entidadesDeAnalisis : System.Web.UI.Page
    {
        Datos.Entidades.Usuario usuarios = new Datos.Entidades.Usuario();
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

                // Cargamos la grilla
                CargaGrilla();
            };
        }

        // ACCIONES DE BOTONES
        protected void Agregar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            EntidadMuestreador entidadMuestreador = new EntidadMuestreador();

            if (numEntidadAnalisis.Text != null && !numEntidadAnalisis.Text.Equals(""))
            {
                entidadMuestreador.numEntidad = Convert.ToInt32(numEntidadAnalisis.Text);
            }

            entidadMuestreador.razonSocial = nombre.Text;
            entidadMuestreador.repLegal = RepresentanteLegal.Text;
            entidadMuestreador.domicilio = Domicilio.Text;
            entidadMuestreador.categoria = Categoria.Text;

            if (Numero.Text != null && !Numero.Text.Equals(""))
            {
                entidadMuestreador.numero = Convert.ToInt32(Numero.Text);
            }

            if (Fecha.Text != null && !Fecha.Text.Equals(""))
            {
                entidadMuestreador.fecha = Convert.ToDateTime(Fecha.Text);
            }

            if (FechaTextRecepcion.Text != null && !FechaTextRecepcion.Text.Equals(""))
            {
                entidadMuestreador.inicioVigencia = Convert.ToDateTime(FechaTextRecepcion.Text);
            }

            if (FechaTerminoVigencia.Text != null && !FechaTerminoVigencia.Text.Equals(""))
            {
                entidadMuestreador.finVigencia = Convert.ToDateTime(FechaTerminoVigencia.Text);
            }

            entidadMuestreador.condVigencia = CondicionVigencia.Text;

            entidadMuestreador.correo = CorreoElectronico.Text;

            entidadMuestreador.fono = Telefono.Text;

            if (entidadMuestreador != null)
            {

                DataTable dt = mantenedorGeneralService.guardarEntidadDeAnalisis(entidadMuestreador);

                try
                {
                    string msg = Convert.ToString(dt.Rows[0]["msg"]);
                    if (msg == "OK")
                    {
                        mensaje = "La entidad de análisis '" + entidadMuestreador.razonSocial + "' ha sido creada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        GridView1.EditIndex = -1;
                        GridView1.PageIndex = 0;

                        nombre.Text = "";
                        RepresentanteLegal.Text = "";
                        Domicilio.Text = "";
                        Categoria.Text = "";
                        Numero.Text = "";
                        Fecha.Text = "";
                        FechaTextRecepcion.Text = "";
                        FechaTerminoVigencia.Text = "";
                        CondicionVigencia.Text = "";
                        CorreoElectronico.Text = "";
                        Telefono.Text = "";

                        CargaGrilla();
                    }
                    else
                    {
                        mensaje = "La entidad de análisis '" + entidadMuestreador.razonSocial + "' ya existe.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                    };
                }
                catch
                {
                    mensaje = "Se ha producido un error al intentar crear la entidad de análisis.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };

                
            }
            else
            {
                mensaje = "Para agregar debe ingresar un nombre para entidad de análisis.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            // Iniciamos la query de búsqueda y cargamos la grilla
            EntidadMuestreador entidadMuestreador = new EntidadMuestreador();

            if (numEntidadAnalisis.Text != null && !numEntidadAnalisis.Text.Equals(""))
            {
                entidadMuestreador.numEntidad = Convert.ToInt32(numEntidadAnalisis.Text);
            }

            entidadMuestreador.razonSocial = nombre.Text;
            entidadMuestreador.repLegal = RepresentanteLegal.Text;
            entidadMuestreador.domicilio = Domicilio.Text;
            entidadMuestreador.categoria = Categoria.Text;

            if (Numero.Text != null && !Numero.Text.Equals(""))
            {
                entidadMuestreador.numero = Convert.ToInt32(Numero.Text);
            }

            if (Fecha.Text != null && !Fecha.Text.Equals(""))
            {
                entidadMuestreador.fecha = Convert.ToDateTime(Fecha.Text);
            }

            if (FechaTextRecepcion.Text != null && !FechaTextRecepcion.Text.Equals(""))
            {
                entidadMuestreador.inicioVigencia = Convert.ToDateTime(FechaTextRecepcion.Text);
            }

            if (FechaTerminoVigencia.Text != null && !FechaTerminoVigencia.Text.Equals(""))
            {
                entidadMuestreador.finVigencia = Convert.ToDateTime(FechaTerminoVigencia.Text);
            }

            entidadMuestreador.condVigencia = CondicionVigencia.Text;

            entidadMuestreador.correo = CorreoElectronico.Text;

            entidadMuestreador.fono = Telefono.Text;

            List<EntidadMuestreador> dt = mantenedorGeneralService.listarEntidadDeAnalisis(entidadMuestreador);

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
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar la entidad de analisis " + DataBinder.Eval(e.Row.DataItem, "razonSocial") + "?')");
                    boton_eliminar.Visible = true;
                };
            };


            if (GridView1.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
            {

                TextBox fecha = ((TextBox)e.Row.FindControl("geFecha"));
                var labelFecha = ((HiddenField)e.Row.FindControl("hdnFecha"));
                if (labelFecha.Value != null && !labelFecha.Value.Equals("") && Convert.ToDateTime(labelFecha.Value) != default(DateTime))
                {
                    fecha.Text = labelFecha.Value;
                }

                TextBox fechaIniVigencia = ((TextBox)e.Row.FindControl("geFechaIniVigencia"));
                var labelFechaIniVigencia = ((HiddenField)e.Row.FindControl("hdnFechaIniVigencia"));
                if (labelFechaIniVigencia.Value != null && !labelFechaIniVigencia.Value.Equals("") && Convert.ToDateTime(labelFechaIniVigencia.Value) != default(DateTime))
                {
                    fechaIniVigencia.Text = labelFechaIniVigencia.Value;
                }

                TextBox fechaTerminoVigencia = ((TextBox)e.Row.FindControl("geFechaTerminoVigencia"));
                var labelfechaTerminoVigencia = ((HiddenField)e.Row.FindControl("hdnFechaTerminoVigencia"));
                if (labelfechaTerminoVigencia.Value != null && !labelfechaTerminoVigencia.Value.Equals("") && Convert.ToDateTime(labelfechaTerminoVigencia.Value) != default(DateTime))
                {
                    fechaTerminoVigencia.Text = labelfechaTerminoVigencia.Value;
                }

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
            TextBox razonSocial = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geRazonSocial");
            TextBox representanteLegal = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geRepresentanteLegal");
            TextBox domicilio = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geDomicilio");
            TextBox categoria = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geCategoria");
            TextBox numero = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geNumero");
            TextBox fecha = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geFecha");
            TextBox fechaIniVigencia = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geFechaIniVigencia");
            TextBox fechaTerminoVigencia = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geFechaTerminoVigencia");
            TextBox condVigencia = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geCondVigencia");
            TextBox correoElectronico = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geCorreoElectronico");
            TextBox telefono = (TextBox)GridView1.Rows[e.RowIndex].FindControl("geTelefono");


            if (razonSocial.Text != "" && representanteLegal.Text != "" && domicilio.Text != "" && categoria.Text != "" && numero.Text != "" && fecha.Text != ""  && fechaIniVigencia.Text != "" && condVigencia.Text != "")
            {
                id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                Agregar.Enabled = true;
                Update(id, razonSocial.Text, representanteLegal.Text, domicilio.Text, categoria.Text, numero.Text, fecha.Text, fechaIniVigencia.Text, fechaTerminoVigencia.Text, condVigencia.Text, correoElectronico.Text, telefono.Text);
            }
            else
            {
                Content_msgGrilla.Visible = true;
                msgGrilla.Text = "Para modificar debe ingresar los datos de la entidad de analisis.";
            };
            CargaGrilla();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Agregar.Enabled = true;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void Update(int id, string razonSocial, string representanteLegal, string domicilio, string categoria, string numero, string fecha, string fechaIniVigencia, string fechaTerminoVigencia, string condVigencia, string correoElectronico, string telefono)
        {
            EntidadMuestreador entidadMuestreador = new EntidadMuestreador();
            entidadMuestreador.numEntidad = id;
            entidadMuestreador.razonSocial = razonSocial;
            entidadMuestreador.repLegal = representanteLegal;
            entidadMuestreador.domicilio = domicilio;
            entidadMuestreador.categoria = categoria;

            if (numero  != null && !numero.Equals(""))
            {
                entidadMuestreador.numero = Convert.ToInt32(numero);
            }

            if (fecha != null && !fecha.Equals(""))
            {
                entidadMuestreador.fecha = Convert.ToDateTime(fecha);
            }

            if (fechaIniVigencia != null && !fechaIniVigencia.Equals(""))
            {
                entidadMuestreador.inicioVigencia = Convert.ToDateTime(fechaIniVigencia);
            }

            if (fechaTerminoVigencia != null && !fechaTerminoVigencia.Equals(""))
            {
                entidadMuestreador.finVigencia = Convert.ToDateTime(fechaTerminoVigencia);
            }

            entidadMuestreador.condVigencia = condVigencia;

            entidadMuestreador.correo = correoElectronico;

            entidadMuestreador.fono = telefono;


            DataTable dt = mantenedorGeneralService.actualizarEntidadDeAnalisis(entidadMuestreador);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);
                if (msg == "OK")
                {
                    mensaje = "La entidad de análisis:" + id + " ha sido actualizada.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    GridView1.EditIndex = -1;
                }
                else
                {
                    mensaje = "La entidad de análisis '" + nombre + "' ya existe.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar actualizar la entidad de análisis.";
                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
            };

            Content_msgGrilla.Visible = true;
            msgGrilla.Text = mensaje;
        }

        protected void Delete(int id)
        {
            DataTable dt = mantenedorGeneralService.eliminarEntidadDeAnalisis(id);

            string mensaje = "";

            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);

                switch (msg)
                {
                    case "OK":
                        mensaje = "La entidad de análisis ha sido eliminada.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        break;
                    case "En uso":
                        mensaje = "La entidad de análisis que intenta borrar está actualmente en uso.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                    default:
                        mensaje = "Se ha producido un error al intentar eliminar la entidad de análisis.";
                        Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                        break;
                };
            }
            catch
            {
                mensaje = "Se ha producido un error al intentar eliminar la entidad de análisis.";
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
            string nom_grilla = "entidadDeAnalisis";
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "entidadDeAnalisis":
                    GridView1.Columns.RemoveAt(12);
                    grilla = GridView1;
                    ngrilla = "entidadDeAnalisis.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            EntidadMuestreador entidadMuestreador = new EntidadMuestreador();

            if (numEntidadAnalisis.Text != null && !numEntidadAnalisis.Text.Equals(""))
            {
                entidadMuestreador.numEntidad = Convert.ToInt32(numEntidadAnalisis.Text);
            }

            entidadMuestreador.razonSocial = nombre.Text;
            entidadMuestreador.repLegal = RepresentanteLegal.Text;
            entidadMuestreador.domicilio = Domicilio.Text;
            entidadMuestreador.categoria = Categoria.Text;

            if (Numero.Text != null && !Numero.Text.Equals(""))
            {
                entidadMuestreador.numero = Convert.ToInt32(Numero.Text);
            }

            if (Fecha.Text != null && !Fecha.Text.Equals(""))
            {
                entidadMuestreador.fecha = Convert.ToDateTime(Fecha.Text);
            }

            if (FechaTextRecepcion.Text != null && !FechaTextRecepcion.Text.Equals(""))
            {
                entidadMuestreador.inicioVigencia = Convert.ToDateTime(FechaTextRecepcion.Text);
            }

            if (FechaTerminoVigencia.Text != null && !FechaTerminoVigencia.Text.Equals(""))
            {
                entidadMuestreador.finVigencia = Convert.ToDateTime(FechaTerminoVigencia.Text);
            }

            entidadMuestreador.condVigencia = CondicionVigencia.Text;

            entidadMuestreador.correo = CorreoElectronico.Text;

            entidadMuestreador.fono = Telefono.Text;

            if (entidadMuestreador != null)
            {
                List<EntidadMuestreador> dt = mantenedorGeneralService.listarEntidadDeAnalisis(entidadMuestreador);

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