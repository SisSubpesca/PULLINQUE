using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;
using Datos.Contantes;
using Datos.Entidades.Relocalizacion;

namespace SubPesca.Solicitudes.RelocalizacionRESA
{
    public partial class administrarInformesRESA : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        PermisosService permisosService = new PermisosService();
        String erroresSumary = "ValidationSummary";
        RelocalizacionRESAService relocalizacionRESAService = new RelocalizacionRESAService();

        protected void setearModulo()
        {
            ViewState["SECCION_ESPECIFICA"] = new int[] { rbSeccionUnidadEspacial.ADMINISTRADOR_INFORMES_RESA };

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
                    Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
                }


                //BOTON DE INGRESO O MODIFICACION
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], this.usuario_logeado, null, rbAccion.EDITAR))
                {
                    //PanelFormularioIngreso.Visible = true;
                    //UpdatePanelFormularioIngreso.Update();
                }
                else
                {
                    //PanelFormularioIngreso.Visible = false;
                    //UpdatePanelFormularioIngreso.Update();
                }



                if (PreviousPage != null && PreviousPage is ingresoInformeRESA)
                {

                    MensajeSuperior.Text = ((ingresoInformeRESA)PreviousPage).MensajeRegistro;
                    if (!MensajeSuperior.Text.Equals(""))
                    {
                        PanelMensajeSuperior.Visible = true;
                        UpdatePanelMensajeSuperior.Update();
                    }

                }


            }

        }



        //GRID RESOLUCIONES
        protected void CargaGrilla()
        {



            int pagina = 0;

            InformeRel_RESA informeFiltro = new InformeRel_RESA();

            try
            {
                informeFiltro = (InformeRel_RESA)Session["Filtro_Informes"];
                pagina = informeFiltro.pagina;
            }
            catch { };

            if (informeFiltro == null)
            {
                informeFiltro = new InformeRel_RESA();
                Session["Filtro_Informes"] = informeFiltro;
            }

            GridInformes.DataSource = relocalizacionRESAService.ListarInformesRESA(informeFiltro);
            GridInformes.DataBind();


        }


        //GRID INFORMES
        protected void GridInformes_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

            InformeRel_RESA informeFiltro = (InformeRel_RESA)Session["Filtro_Informes"];
            if (informeFiltro == null)
            {
                informeFiltro = new InformeRel_RESA();
            }

            informeFiltro.pagina = e.NewPageIndex;
            Session["Filtro_Informes"] = informeFiltro;

            GridInformes.PageIndex = e.NewPageIndex;
            GridInformes.DataBind();
            CargaGrilla();
        }


        //GRID INFORMES
        protected void GridInformes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridView GridRelocalizacionTramite = (GridView)sender;



                //BOTON VER
                ImageButton boton_gVer = (ImageButton)e.Row.FindControl("gVer");
                if (boton_gVer != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.VER))
                    {
                        boton_gVer.Visible = true;
                    }
                };



                String idEstadoVigencia = ((Label)e.Row.FindControl("gEstadoVigencia")).Text;

                //No Vigente
                ImageButton boton_noVigente = (ImageButton)e.Row.FindControl("gNoVigente");
                if (boton_noVigente != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.NO_VIGENTE))
                    {
                        boton_noVigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea dejar no vigente este documento(s)?')");
                        if (Convert.ToInt32(idEstadoVigencia) == rbEstadosGenerales.VIGENTE)
                        {
                            //boton_noVigente.Visible = true;
                        }
                    }
                };


                //Vigente
                ImageButton boton_vigente = (ImageButton)e.Row.FindControl("gVigente");
                if (boton_vigente != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.VIGENTE))
                    {
                        boton_vigente.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea dejar vigente este documento(s)?')");
                        if (Convert.ToInt32(idEstadoVigencia) == rbEstadosGenerales.NO_VIGENTE)
                        {
                            //boton_vigente.Visible = true;
                        }
                    }
                };



                //BOTON MODIFICAR
                ImageButton boton_gModificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_gModificar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.VER))
                    {
                        boton_gModificar.Visible = true;
                    }
                };


                //BOTON ELIMINAR
                ImageButton boton_gEliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_gEliminar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.ELIMINAR))
                    {
                        boton_gEliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea dejar eliminar este informe?')");
                        boton_gEliminar.Visible = true;
                    }
                };


            };
        }


        //GRID RESOLUCIONES
        protected void GridInformes_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int id = 0;


            switch (e.CommandName)
            {

                case "ModificarVigencia":

                    id = Convert.ToInt32(e.CommandArgument);

                    break;

                case "VerInforme":

                    id = Convert.ToInt32(e.CommandArgument);


                    break;


                case "ModificarInforme":
                    id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("../RelocalizacionRESA/ingresoInformeRESA.aspx?idInformeRel=" + id);
                    break;

                case "EliminarInforme":

                    id = Convert.ToInt32(e.CommandArgument);

                    if (id > 0)
                    {

                        if (!relocalizacionRESAService.EliminarInformeRESA(id))
                        {

                            ErroresGrilla.Text = "Ha ocurrido un error al realizar la accion solicitada";
                            PanelErroresGrilla.Visible = true;
                            UpdatePanelErroresGrilla.Update();

                        }
                        else
                        {

                            ErroresGrilla.Text = "Accion realizada con exito";
                            PanelErroresGrilla.Visible = true;
                            UpdatePanelErroresGrilla.Update();
                            this.CargaGrilla();

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


            InformeRel_RESA informeFiltro = new InformeRel_RESA();



            if (Numero.Text != null && !Numero.Text.Trim().Equals(""))
            {
                informeFiltro.numero = Numero.Text.Trim();
            }

            if (codigoCentro.Text != null && !codigoCentro.Text.Trim().Equals(""))
            {
                informeFiltro.codigoCentroFiltro = Convert.ToInt32(codigoCentro.Text.Trim());
            }


            Session["Filtro_Informes"] = informeFiltro;
            CargaGrilla();



        }



        protected void Limpiar_Click(object sender, EventArgs e)
        {

            
            Numero.Text = "";
            codigoCentro.Text = "";
            
            UpdatePanelNumero.Update();
            UpdatePanelCodigoCentro.Update();

        }

    }
}