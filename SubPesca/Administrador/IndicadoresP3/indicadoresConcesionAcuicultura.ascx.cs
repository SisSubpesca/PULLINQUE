using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.indicadores;
using System.Collections;
using System.Data;
using Datos.Contantes;
using Datos.Entidades;
using SubPesca.Utilidades;
using LogicaNegocio.cl.subpesca.rb.common;

namespace SubPesca.Administrador.IndicadoresP3
{
    public partial class indicadoresConcesionAcuicultura : System.Web.UI.UserControl
    {

        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        Datos.Utilidades.Funciones fnc = new Datos.Utilidades.Funciones();
        IndicadoresService indicadoresService = new IndicadoresService();
        String erroresSumary = "ValidationSummaryIndicador";
        RegionDA regionDA = new RegionDA();

        // PAGE LOAD
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                ObtencionParametros();
                
                Initialize_Form();
                
                
            };
        }


        // EVENTOS GENERALES
        protected void ObtencionParametros()
        {
            try
            {
                if (Request.QueryString["id_indicador"] != null && Request.QueryString["id_solicitud"] != null)
                {
                    IdIndicador.Value = Convert.ToString(Convert.ToInt32(Request.QueryString["id_indicador"]));
                    IdTipoTramite.Value = Convert.ToString(Convert.ToInt32(Request.QueryString["id_solicitud"]));

                    if (Request.QueryString["id_subTipo"] != null && !Convert.ToString(Request.QueryString["id_subTipo"]).Equals(""))
                    {
                        IdSubTipo.Value = Convert.ToString(Convert.ToInt32(Request.QueryString["id_subTipo"]));
                    }


                    //RECARGAR EL FORMULARIO
                    if (Request.QueryString["rel"] != null && !Convert.ToString(Request.QueryString["rel"]).Equals("") && Convert.ToInt32(Request.QueryString["rel"]) == 1)
                    {
                        ViewState["FORM_INDICADORES"] = (Hashtable)Session["INDICADOR_FORM"];
                        this.RecargarFormulario();

                    }
                    else {
                        //SE LIMPIA LOS DATOS EN SESION EN CASO DE QUE NO SE REQUIERA UN RECARGAR.
                        Session["INDICADOR_FORM"] = null;
                    }

                }
                else
                {
                    Response.Redirect("~/Administrador/IndicadoresP3/resumenIndicadores.aspx");
                };

            }
            catch
            {
                Response.Redirect("~/Administrador/IndicadoresP3/resumenIndicadores.aspx");
            };
        }

        private void RecargarFormulario()
        {

            Hashtable HT_Indicador = (Hashtable)ViewState["FORM_INDICADORES"];

            if (HT_Indicador != null)
            {

                FechaNumeradorDesde.Text = (string)HT_Indicador["fechaNumerador_desde"];
                FechaNumeradorHasta.Text = (string)HT_Indicador["fechaNumerador_hasta"];
                FechaDenominadorDesde.Text = (string)HT_Indicador["fechaDenominador_desde"];
                FechaDenominadorHasta.Text = (string)HT_Indicador["fechaDenominador_hasta"];
                FechaConsulta.Text = (string)HT_Indicador["fechaConsulta"];


                string regiones = (string)HT_Indicador["fechaConsulta"]; ;


                if (regiones != null && !regiones.Trim().Equals("")) {

                    Char delimiter = '.';
                    String[] regionesId = regiones.Split(delimiter);


                    foreach (ListItem listItem in Regiones.Items)
                    {
                        foreach (var idRegion in regionesId)
                        {
                            if (listItem.Value.Equals(Convert.ToString(idRegion)))
                            {
                                listItem.Selected = true;
                            }
                        }

                        if (listItem.Value.Equals(Convert.ToString("-1")))
                        {
                            listItem.Selected = false;
                        }
                    }
                }


                this.Calcular_Click(null,null);
            
            }
            
        }


 
        
        // ACCIONES GENERALES
        protected void Initialize_Form()
        {

            if (IdIndicador.Value != null && !IdIndicador.Value.Trim().Equals("")) {

                Indicador indicador = indicadoresService.ObtenerIndicador(Convert.ToInt32(IdIndicador.Value));

                if (indicador != null) {
                    numeroIndicador.Text = Convert.ToString(indicador.idIndicador);
                    nombreIndicador.Text = indicador.nombreIndicador;
                    Formula.ImageUrl = indicador.formula;

                    //formula
                    consideraciones.Text = indicador.consideraciones;

                    if (indicador.fechaNumerador_desde) 
                    {
                        trNumeradorDesde.Visible = true;
                        string script = "calendario('" + FechaNumeradorDesde.ClientID + "','" + imgFechaNumeradorDesde.ClientID + "');";
                        ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + FechaNumeradorDesde.ClientID, script.ToString(), true);
                    }
                    if (indicador.fechaNumerador_hasta)
                    {
                        trNumeradorHasta.Visible = true;
                        string script = "calendario('" + FechaNumeradorHasta.ClientID + "','" + imgFechaNumeradorHasta.ClientID + "');";
                        ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + FechaNumeradorHasta.ClientID, script.ToString(), true);
                    }
                    if (indicador.fechaDenominador_desde)
                    {
                        trDenominadorDesde.Visible = true;
                        string script = "calendario('" + FechaDenominadorDesde.ClientID + "','" + imgFechaDenominadorDesde.ClientID + "');";
                        ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + FechaDenominadorDesde.ClientID, script.ToString(), true);
                    }
                    if (indicador.fechaDenominador_hasta)
                    {
                        trDenominadorHasta.Visible = true;
                        string script = "calendario('" + FechaDenominadorHasta.ClientID + "','" + imgFechaDenominadorHasta.ClientID + "');";
                        ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + FechaDenominadorHasta.ClientID, script.ToString(), true);
                    }
                    if (indicador.fechaConsulta)
                    {
                        trFechaConsulta.Visible = true;
                        string script = "calendario('" + FechaConsulta.ClientID + "','" + imgFechaConsulta.ClientID + "');";
                        ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + FechaConsulta.ClientID, script.ToString(), true);
                    }

                    if (indicador.regiones)
                    {
                        trRegiones.Visible = true;

                        Regiones.Items.Clear();
                        Regiones.DataSource = regionDA.ListarRegion(0);
                        Regiones.DataTextField = "Region";
                        Regiones.DataValueField = "IdRegion";
                        Regiones.DataBind();
                        Regiones.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    }

                    

                }
                else
                {
                    Response.Redirect("~/Administrador/IndicadoresP3/resumenIndicadores.aspx");
                };

            }
        }




        protected void Volver_Click(object sender, EventArgs e)
        {
            string path = "~/Administrador/IndicadoresP3/resumenIndicadores.aspx";
            Response.Redirect(path);
        }


        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            DataTable dt = CargaGrilla();

            GridView grilla = new GridView();
            int cantidadColumnas = dt.Columns.Count;
            dt.Columns.RemoveAt(cantidadColumnas - 1);

            grilla.DataSource = dt;
            grilla.AllowPaging = false;
            grilla.DataBind();

            SubPesca.Utilidades.GridViewExportUtil.Export("detalleIndicador.xls", grilla);

        }

        protected string GeneraFrase_Resultado(int result, string unidad_result, string descrip_result)
        {
            string resp = "";
            switch (unidad_result)
            {
                case "día":
                    if (result != 1)
                    {
                        unidad_result = unidad_result + "s";
                    };
                    resp = result.ToString() + " " + unidad_result + " (" + descrip_result + ")";
                    break;
                case "%":
                    resp = result.ToString() + unidad_result + " (" + descrip_result + ")";
                    break;
                case "solicitud":
                    if (result != 1)
                    {
                        unidad_result = unidad_result + "es";
                    };
                    resp = result.ToString() + " " + unidad_result + " (" + descrip_result + ")";
                    break;
            };

            return resp;
        }


        // EVENTOS DE LA GRILLA
        protected DataTable CargaGrilla()
        {


            int tipoIndicador = Convert.ToInt32(IdIndicador.Value);
            int idTipoTramite = Convert.ToInt32(IdTipoTramite.Value);
            int idSubTipo = 0;
            if (IdSubTipo.Value != null && !IdSubTipo.Value.Trim().Equals(""))
            {
                idSubTipo = Convert.ToInt32(IdSubTipo.Value);
            }

            Hashtable HT_Indicador = (Hashtable)ViewState["FORM_INDICADORES"];

            string fechaNumerador_desde = (string)HT_Indicador["fechaNumerador_desde"];
            string fechaNumerador_hasta = (string)HT_Indicador["fechaNumerador_hasta"];
            string fechaDenominador_desde = (string)HT_Indicador["fechaDenominador_desde"];
            string fechaDenominador_hasta = (string)HT_Indicador["fechaDenominador_hasta"];
            string fechaConsulta = (string)HT_Indicador["fechaConsulta"];
            string regiones = (string)HT_Indicador["regiones"];


            //SE GUARDA EN LA SESSION PARA PODER CARGAR AUTOMATICAMENTE LA GRILLA SI EL USUARIO PRESIONA EN VOLVER
            Session["INDICADOR_FORM"] = HT_Indicador;


            

            DataTable dt = new DataTable();
            int num_registros = 0;

            dt = indicadoresService.ListarDetalleIndicador(tipoIndicador, idTipoTramite, idSubTipo, fechaNumerador_desde, fechaNumerador_hasta, fechaDenominador_desde, fechaDenominador_hasta, fechaConsulta, regiones);
            
            if (dt != null)
            {
                num_registros = dt.Rows.Count;
                if (num_registros > 0)
                {
                    if (num_registros == 1)
                    {
                        msgGrilla.Text = "Se ha encontrado 1 solicitud relacionada al indicador seleccionado.";
                    }
                    else
                    {
                        msgGrilla.Text = "Se han encontrado " + num_registros + " solicitudes relacionadas al indicador seleccionado.";
                    };
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Content_msgGrilla.Visible = true;
                    ExportarGrilla.Visible = true;
                }
                else
                {
                    msgGrilla.Text = "No se encontraron solicitudes relacionadas al indicador seleccionado.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    Content_msgGrilla.Visible = true;
                    ExportarGrilla.Visible = false;
                };

                
                
                GridView1.DataSource = dt;
                GridView1.DataBind();

                indicador_detalle.Visible = true;
                GrillaResultado.Visible = true;

                UpdatePanelGrillaResultado.Update();
               
            }
            else
            {
                Response.Redirect("~/Administrador/IndicadoresP3/resumenIndicadores.aspx");
            };




            return dt;
        }


        protected void GridView1_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
            CargaGrilla();
        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                int iSolConcesion = Convert.ToInt32(e.Row.Cells[0].Text); //idSolConcesion

                HyperLink lnkSolicitud = new HyperLink();
                lnkSolicitud.NavigateUrl = "~/Administrador/IndicadoresP3/detalleSolicitudP3.aspx?idSol=" + iSolConcesion + "&id_solicitud=" + IdTipoTramite.Value + "&id_indicador=" + IdIndicador.Value + "&id_subTipo=" + IdSubTipo.Value;
                lnkSolicitud.ImageUrl = "~/App_Themes/admin_style/images/info.png";
                lnkSolicitud.ToolTip = "Detalle de la solicitud";
                

                HyperLink lnkHistorico = new HyperLink();
                lnkHistorico.NavigateUrl = "~/Administrador/IndicadoresP3/logEstadosSolicitudP3.aspx?idSol=" + iSolConcesion + "&id_solicitud=" + IdTipoTramite.Value + "&id_indicador=" + IdIndicador.Value + "&id_subTipo=" + IdSubTipo.Value;
                lnkHistorico.ImageUrl = "~/App_Themes/admin_style/images/list.png";
                lnkHistorico.ToolTip = "Historial de cambios de estado";

                Label blanco =  new Label();
                blanco.Text = " ";


                int cantidadColumnas = e.Row.Cells.Count;
                e.Row.Cells[cantidadColumnas - 1].Controls.Add(lnkSolicitud);
                e.Row.Cells[cantidadColumnas - 1].Controls.Add(blanco);
                e.Row.Cells[cantidadColumnas - 1].Controls.Add(lnkHistorico);
                e.Row.Cells[cantidadColumnas - 1].HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;



            };
        }



        protected void Limpiar_Click(object sender, EventArgs e)
        {
            

            FechaNumeradorDesde.Text = "";
            FechaNumeradorHasta.Text = "";
            FechaDenominadorDesde.Text = "";
            FechaDenominadorHasta.Text = "";
            FechaConsulta.Text ="";
            Regiones.SelectedValue = "-1";

            UpdatePanelFormulario.Update();

        }
    


        protected void Calcular_Click(object sender, EventArgs e)
        {
            Indicador indicador = indicadoresService.ObtenerIndicador(Convert.ToInt32(IdIndicador.Value));


            Page.Validate("ValidationSummaryIndicador");

            if (indicador.fechaNumerador_desde)
            {
                if (FechaNumeradorDesde.Text.Equals(""))
                {
                    Page.Validators.Add(new ValidationError(erroresSumary, "Ingrese Fecha Numerador desde"));
                }
            }

            if (indicador.fechaNumerador_hasta)
            {
                if (FechaNumeradorHasta.Text.Equals(""))
                {
                    Page.Validators.Add(new ValidationError(erroresSumary, "Ingrese Fecha Numerador hasta"));
                }
            }

            if (indicador.fechaDenominador_desde)
            {
                if (FechaDenominadorDesde.Text.Equals(""))
                {
                    Page.Validators.Add(new ValidationError(erroresSumary, "Ingrese Fecha Denominador desde"));
                }
            }

            if (indicador.fechaDenominador_hasta)
            {
                if (FechaDenominadorHasta.Text.Equals(""))
                {
                    Page.Validators.Add(new ValidationError(erroresSumary, "Ingrese Fecha Denominador hasta"));
                }
            }
            
            if (indicador.fechaConsulta)
            {
                if (FechaConsulta.Text.Equals("")) {
                    Page.Validators.Add(new ValidationError(erroresSumary, "Ingrese fecha de Consulta"));
                }
            }

           


            if (Page.IsValid)
            {
                int num_registros = 0;
                int tipoIndicador = Convert.ToInt32(IdIndicador.Value);
                int idTipoTramite = Convert.ToInt32(IdTipoTramite.Value);
                int idSubTipo = 0;

                if (IdSubTipo.Value != null && !IdSubTipo.Value.Trim().Equals("")) {
                    idSubTipo = Convert.ToInt32(IdSubTipo.Value);
                }
                

                string fechaNumerador_desde = FechaNumeradorDesde.Text.Trim();
                string fechaNumerador_hasta = FechaNumeradorHasta.Text.Trim();
                string fechaDenominador_desde = FechaDenominadorDesde.Text.Trim();
                string fechaDenominador_hasta = FechaDenominadorHasta.Text.Trim();
                string fechaConsulta  = FechaConsulta.Text.Trim();
                string regiones = fnc.ListBox_ItemsSelectedGET(Regiones.Items);


                Hashtable HT_Indicador = new Hashtable();
                HT_Indicador.Add("fechaNumerador_desde", fechaNumerador_desde);
                HT_Indicador.Add("fechaNumerador_hasta", fechaNumerador_hasta);
                HT_Indicador.Add("fechaDenominador_desde", fechaDenominador_desde);
                HT_Indicador.Add("fechaDenominador_hasta", fechaDenominador_hasta);
                HT_Indicador.Add("fechaConsulta", fechaConsulta);
                HT_Indicador.Add("regiones", regiones);

                ViewState["FORM_INDICADORES"] = (Hashtable)HT_Indicador;

                DataTable resp = indicadoresService.CalcularIndicador(tipoIndicador, idTipoTramite, idSubTipo, fechaNumerador_desde, fechaNumerador_hasta, fechaDenominador_desde, fechaDenominador_hasta, fechaConsulta, regiones);


                num_registros =  resp.Rows.Count;

                if (resp != null  &&  num_registros == 1)
                {

                    Numerador.Text = Convert.ToString(resp.Rows[0]["Numerador"]);
                    Denominador.Text = Convert.ToString(resp.Rows[0]["Denominador"]);
                    Resultado.Text = GeneraFrase_Resultado(Convert.ToInt32(resp.Rows[0]["Resultado"]), indicador.unidadResultado, indicador.descripcionResultado);


                    CargaGrilla();

                    

                }
                else
                {
                    ErroresInferior.Text = "Ha ocurrido un error al calcular el documento";
                    PanelErroresInferior.Visible = true;
                    UpdatePanelErroresInferior.Update();

                }
                    
            }


            UpdatePanelMensajesValidaciones.Update();


            if (trNumeradorDesde.Visible == true)
            {
                string script = "calendario('" + FechaNumeradorDesde.ClientID + "','" + imgFechaNumeradorDesde.ClientID + "');";
                ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + FechaNumeradorDesde.ClientID, script.ToString(), true);
            }

            if (trNumeradorHasta.Visible == true)
            {
                string script = "calendario('" + FechaNumeradorHasta.ClientID + "','" + imgFechaNumeradorHasta.ClientID + "');";
                ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + FechaNumeradorHasta.ClientID, script.ToString(), true);
            }

            if (trDenominadorDesde.Visible == true)
            {
                string script = "calendario('" + FechaDenominadorDesde.ClientID + "','" + imgFechaDenominadorDesde.ClientID + "');";
                ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + FechaDenominadorDesde.ClientID, script.ToString(), true);
            }

             if (trDenominadorHasta.Visible == true)
            {
                string script = "calendario('" + FechaDenominadorHasta.ClientID + "','" + imgFechaDenominadorHasta.ClientID + "');";
                ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + FechaDenominadorHasta.ClientID, script.ToString(), true);
            }

              if (trFechaConsulta.Visible == true)
            {
                string script = "calendario('" + FechaConsulta.ClientID + "','" + imgFechaConsulta.ClientID + "');";
                ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario" + FechaConsulta.ClientID, script.ToString(), true);
            }

        }


    }
}
