using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.reportes;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Contantes;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace SubPesca.Administrador.ReportesP3
{

    //@used

    public partial class ReportesCentroAcopio : System.Web.UI.Page
    {
        ReporteService reporteService = new ReporteService();
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema

        Datos.Utilidades.Funciones fnc = new Datos.Utilidades.Funciones();

        ComunaDA comunaDA = new ComunaDA();
        RegionDA regionDA = new RegionDA();
        ProvinciaDA provinciaDA = new ProvinciaDA();
        MacrozonaDA macrozonaDA = new MacrozonaDA();
        MantenedorDA mantenedorDA = new MantenedorDA();
        BarrioDA barrioDA = new BarrioDA();
        CentroCultivoDA centrosCultivoDA = new CentroCultivoDA();
        EtapaCultivoDA etapaCultivoDA = new EtapaCultivoDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        
      
      

        // PAGE LOAD
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                // Inicializamos el formulario   
                Initialize_Form();

            };

            if (PanelFechaDesde.Visible == true)
            {
                string script2 = "calendario('" + FechaDesde.ClientID + "','" + fechaDesdeImgDinamica.ClientID + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaDesde.ClientID, script2.ToString(), true);
            }

            if (PanelFechaHasta.Visible == true)
            {
                string script3 = "calendario('" + FechaHasta.ClientID + "','" + fechaHastaImgDinamica.ClientID + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaHasta.ClientID, script3.ToString(), true);
            }

            Content_Errores.Visible = false;
        }


      
        protected void Initialize_Form()
        {

            tr_tipoReporte.Visible = false;
            tr_region.Visible = false;
            tr_provincia.Visible = false;
            tr_comuna.Visible = false;
            tr_macrozona.Visible = false;
            tr_barrio.Visible = false;
            tr_codigocentro.Visible = false;
            tr_pert.Visible = false;
            tr_especies.Visible = false;
            tr_grupoEspecie.Visible = false;
            
           
            // Carga de los Comboboxs Base
            Carga_Combobox("TipoUE");
            Carga_Combobox("TiposReportes");
            Carga_Combobox("Regiones");
            Carga_Combobox("Provincias");
            Carga_Combobox("Comunas");
            Carga_Combobox("Macrozonas");
            Carga_Combobox("Barrios");
            Carga_Combobox("Periodo");
            Carga_Combobox("EtapasCultivo");
            Carga_Combobox("Especies");
            Carga_Combobox("Vigencia");
            Carga_Combobox("GrupoEspecie");

            int idTipoUE = 118;
          
            switch (idTipoUE)
            {
                case 36:  // CONCESION DE ACUICULTURA
                    tr_tipoReporte.Visible = true;
                    tr_region.Visible = true;
                    tr_provincia.Visible = true;
                    tr_comuna.Visible = true;
                    tr_macrozona.Visible = true;
                    tr_barrio.Visible = true;
                    tr_especies.Visible = true;
                    tr_grupoEspecie.Visible = true;
                    tr_codigocentro.Visible = true;

                    Carga_Combobox("TiposReportes");
                    
                    break;

                case 37://Centro de Faenamiento

                    tr_tipoReporte.Visible = true;
                    tr_region.Visible = true;
                    tr_provincia.Visible = true;
                    tr_comuna.Visible = true;
                    tr_especies.Visible = true;
                    tr_grupoEspecie.Visible = true;
                    tr_codigocentro.Visible = true;

                    Carga_Combobox("TiposReportes");


                    break;

                case 118://Centro de Acopio

                    tr_tipoReporte.Visible = true;
                    tr_region.Visible = true;
                    tr_provincia.Visible = true;
                    tr_barrio.Visible = true;
                    tr_especies.Visible = true;
                    tr_grupoEspecie.Visible = true;
                    tr_codigocentro.Visible = true;

                    Carga_Combobox("TiposReportes");


                    break;

                case 119://Acuicultura en Amerb

                    tr_tipoReporte.Visible = true;
                    tr_region.Visible = true;
                    tr_provincia.Visible = true;
                    tr_comuna.Visible = true;
                    tr_especies.Visible = true;
                    tr_grupoEspecie.Visible = true;
                    tr_codigocentro.Visible = true;

                    Carga_Combobox("TiposReportes");
                    

                    break;

                case 120://Colectores de Semilla

                    tr_tipoReporte.Visible = true;
                    tr_region.Visible = true;
                    tr_provincia.Visible = true;
                    tr_comuna.Visible = true;
                    tr_especies.Visible = true;
                    tr_grupoEspecie.Visible = true;
                    tr_codigocentro.Visible = true;

                    
                    Carga_Combobox("TiposReportes");
                    
                    break;


                case 534://Acuicultura en ECMPO

                    tr_tipoReporte.Visible = true;
                    tr_region.Visible = true;
                    tr_provincia.Visible = true;
                    tr_comuna.Visible = true;
                    tr_especies.Visible = true;
                    tr_grupoEspecie.Visible = true;
                    tr_codigocentro.Visible = true;

                    Carga_Combobox("TiposReportes");
                    
                    break;



                    
            };
        }


        protected void TipoUE_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            tr_tipoReporte.Visible = false;
            tr_region.Visible = false;
            tr_provincia.Visible = false;
            tr_comuna.Visible = false;
            tr_macrozona.Visible = false;
            tr_barrio.Visible = false;
            tr_codigocentro.Visible = false;
            tr_pert.Visible = false;
            tr_especies.Visible = false;
            tr_grupoEspecie.Visible = false;

            Limpiar_PorTipoUE(null, null);
        }

        protected void Carga_Combobox(string combobox)
        {
            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];
        

            switch (combobox)
            {


                case "TipoUE":

                    TipoUE.Items.Clear();

                    TipoUE.Items.Add(new ListItem("Concesión de Acuicultura", Convert.ToString(rbTipo.UNID_ESPACIAL_CONCESION)));
                    TipoUE.Items.Add(new ListItem("Centro de Faenamiento", Convert.ToString(rbTipo.UNID_ESPACIAL_CENTRO_DE_FAENAMIENTO)));
                    TipoUE.Items.Add(new ListItem("Centro de Acopio", Convert.ToString(rbTipo.UNID_ESPACIAL_CENTRO_DE_ACOPIO)));
                    TipoUE.Items.Add(new ListItem("Acuicultura en Amerb", Convert.ToString(rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB)));
                    TipoUE.Items.Add(new ListItem("Colectores de Semilla", Convert.ToString(rbTipo.UNID_ESPACIAL_COLECTORES_DE_SEMILLA)));
                    TipoUE.Items.Add(new ListItem("Acuicultura en ECMPO", Convert.ToString(rbTipo.UNID_ESPACIAL_ECMPO)));

                    TipoUE.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    TipoUE.DataBind();
                    TipoUE.SelectedValue = "118";
                    



                    break;

                case "TiposReportes":
                    
                    TiposReportes.Items.Clear();

                    int idTipoUE = Convert.ToInt32(TipoUE.SelectedValue);



                    if (idTipoUE == rbTipo.UNID_ESPACIAL_CENTRO_DE_ACOPIO)
                    {
                        TiposReportes.Items.Add(new ListItem(TipoReportes.REPORTES_UNIDADES_ESPACIALES_DOCUMENTOS_STRING, Convert.ToString(TipoReportes.REPORTES_UNIDADES_ESPACIALES_DOCUMENTOS)));
                        TiposReportes.Items.Add(new ListItem(TipoReportes.REPORTES_UNIDADES_ESPACIALES_COORDENADAS_STRING, Convert.ToString(TipoReportes.REPORTES_UNIDADES_ESPACIALES_COORDENADAS)));
                        TiposReportes.Items.Add(new ListItem(TipoReportes.REPORTE_UNIDADES_ESPACIALES_ANALISIS_DE_VIGENCIA_STRING, Convert.ToString(TipoReportes.REPORTE_UNIDADES_ESPACIALES_ANALISIS_DE_VIGENCIA)));
                        TiposReportes.Items.Add(new ListItem(TipoReportes.REPORTE_UNIDADES_ESPACIALES_NO_VIGENTES_RESOLUCIONES_STRING, Convert.ToString(TipoReportes.REPORTE_UNIDADES_ESPACIALES_NO_VIGENTES_RESOLUCIONES)));
                        TiposReportes.Items.Add(new ListItem(TipoReportes.REPORTE_UNIDADES_ESPACIALES_NO_VIGENTES_TRAMITES_STRING, Convert.ToString(TipoReportes.REPORTE_UNIDADES_ESPACIALES_NO_VIGENTES_TRAMITES)));
                    }


                    TiposReportes.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    TiposReportes.DataBind();
                    
                    
                    break;
                
                case "Regiones":
                    // Cargamos el combobox: Regiones
                    Regiones.Items.Clear();
                    Regiones.DataSource = regionDA.ListarRegion(0);
                    Regiones.DataTextField = "Region";
                    Regiones.DataValueField = "IdRegion";
                    Regiones.DataBind();
                    Regiones.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                
                case "Provincias":
                    // Cargamos el combobox: Provincias
                    Provincias.Items.Clear();
                    if (Convert.ToInt32(Regiones.SelectedValue) > 0)
                    {
                        Provincias.DataSource = parametroGenericoDA.ListarProvinciaRegDataTable(0, Convert.ToInt32(Regiones.SelectedValue));
                        Provincias.DataTextField = "Provincia";
                        Provincias.DataValueField = "IdProvincia";
                        Provincias.DataBind();
                    };
                    Provincias.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "Comunas":
                    // Cargamos el combobox: Comunas
                    Comunas.Items.Clear();
                    if (Convert.ToInt32(Provincias.SelectedValue) > 0)
                    {
                        Comunas.DataSource = parametroGenericoDA.ListarComunaDataTable(0, Convert.ToInt32(Provincias.SelectedValue));
                        Comunas.DataTextField = "Comuna";
                        Comunas.DataValueField = "IdComuna";
                        Comunas.DataBind();
                    };
                    Comunas.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "Macrozonas":
                    // Cargamos el combobox: Macrozonas
                    Macrozonas.Items.Clear();
                    if (Convert.ToInt32(Regiones.SelectedValue) > 0)
                    {

                        Macrozona macrozonaFiltro = new Macrozona();
                        macrozonaFiltro.id_region = Convert.ToInt32(Regiones.SelectedValue);

                        //Macrozonas.DataSource = mantenedorDA.ListarMacrozona_Mantenedor(0, Convert.ToInt32(Regiones.SelectedValue));
                        Macrozonas.DataSource = mantenedorDA.ListarMacrozona_Mantenedor(macrozonaFiltro);
                        Macrozonas.DataTextField = "Macrozona";
                        Macrozonas.DataValueField = "id_macrozona";
                        Macrozonas.DataBind();
                    };
                    Macrozonas.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "Barrios":
                    // Cargamos el combobox: Barrios
                    Barrios.Items.Clear();
                    if (Convert.ToInt32(Macrozonas.SelectedValue) > 0)
                    {

                        Barrio barrioAux = new Barrio();
                        barrioAux.id_macrozona = Convert.ToInt32(Macrozonas.SelectedValue);
                        Barrios.DataSource = mantenedorDA.ListarBarrio_Mantenedor(barrioAux);
                        Barrios.DataTextField = "barrio";
                        Barrios.DataValueField = "id_barrio";
                        Barrios.DataBind();
                    };
                    Barrios.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "Especies":
                    // Cargamos el listbox: Especies
                    Especies.Items.Clear();
                    Especies.DataSource = parametroGenericoDA.ListarEspecies(0, "", 0);
                    Especies.DataTextField = "descripcion";
                    Especies.DataValueField = "id";
                    Especies.DataBind();
                    break;

                case "GrupoEspecie":
                    
                    GrupoEspecie.Items.Clear();
                    GrupoEspecie.DataSource = parametroGenericoDA.ListarGrupoEspecieInformativo(0);
                    GrupoEspecie.DataTextField = "descripcion";
                    GrupoEspecie.DataValueField = "id";
                    GrupoEspecie.DataBind();
                    GrupoEspecie.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

            
            };
        }


        // EVENTOS
        protected void ObtencionParametros()
        {
            // Se recibe el id_tiporeporte
            try
            {
                if (Request.QueryString["id_tiporeporte"] != null)
                {
                   
                }
                
            }
            catch
            {
                IdTipoReporte.Value = "1";
            };
        }



        

        protected void TiposReportes_OnSelectedIndexChanged(object sender, EventArgs e)
        {


            if (Convert.ToInt32(TiposReportes.SelectedValue) == TipoReportes.REPORTE_UNIDADES_ESPACIALES_ANALISIS_DE_VIGENCIA)
            {
                PanelFechaDesde.Visible = true;

                string script = "calendario('" + FechaDesde.ClientID + "','" + fechaDesdeImgDinamica.ClientID + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaDesde.ClientID, script.ToString(), true);

                PanelFechaHasta.Visible = true;

                string script2 = "calendario('" + FechaHasta.ClientID + "','" + fechaHastaImgDinamica.ClientID + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaHasta.ClientID, script2.ToString(), true);
                
            }
            else
            {
                FechaDesde.Text = "";
                PanelFechaDesde.Visible = false;

                FechaHasta.Text = "";
                PanelFechaHasta.Visible = false;
            }
            UpdatePanelFechaDesde.Update();
            UpdatePanelFechaHasta.Update();

            Content_Errores.Visible = false;
            Content_Resultado.Visible = false;
            
        }

        protected void Regiones_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("Provincias");
            Carga_Combobox("Comunas");
            Carga_Combobox("Macrozonas");
            Carga_Combobox("Barrios");
        }

        protected void Provincias_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("Comunas");
        }

        protected void Macrozonas_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("Barrios");
        }
       
        protected void Filtrar_Click(object sender, EventArgs e)
        {
            Content_Resultado.Visible = false;
            if (validarForm())
            {
                this.CargaGrilla();
            }
            else
            {
                string script = @"<script type='text/javascript'>oculta_loading('cargando');</script>";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "mensaje_cargado", script, false);
            };
        }

        protected void Limpiar_Click(object sender, EventArgs e)
        {

            //TipoUE.SelectedValue = "-1";
            TiposReportes.SelectedValue = "-1";
            Regiones.SelectedValue = "-1";
            Provincias.SelectedValue = "-1";
            Comunas.SelectedValue = "-1";
            Macrozonas.SelectedValue = "-1";
            Barrios.SelectedValue = "-1";
            Especies = fnc.ListBox_ItemsSelectedNone(Especies);
            GrupoEspecie.SelectedValue = "-1";
            Pert.Text = "";
            CodigoCentro.Text = "";
            FechaDesde.Text = "";
            FechaHasta.Text = "";
            

            Carga_Combobox("Provincias");
            Carga_Combobox("Comunas");
            Carga_Combobox("Macrozonas");
            Carga_Combobox("Barrios");


            

            Content_Errores.Visible = false;
            Content_Resultado.Visible = false;

            
           
        }

        protected void Limpiar_PorTipoUE(object sender, EventArgs e)
        {
            
            TiposReportes.SelectedValue = "-1";
            Regiones.SelectedValue = "-1";
            Provincias.SelectedValue = "-1";
            Comunas.SelectedValue = "-1";
            Macrozonas.SelectedValue = "-1";
            Barrios.SelectedValue = "-1";
            Especies = fnc.ListBox_ItemsSelectedNone(Especies);
            GrupoEspecie.SelectedValue = "-1";
            Pert.Text = "";
            CodigoCentro.Text = "";
            FechaDesde.Text = "";
            FechaHasta.Text = "";

            Carga_Combobox("Provincias");
            Carga_Combobox("Comunas");
            Carga_Combobox("Macrozonas");
            Carga_Combobox("Barrios");


            Content_Errores.Visible = false;
            Content_Resultado.Visible = false;


        }

     
        protected bool validarForm()
        {

            List<string> errores_list = new List<string>();
            bool formOK = true;


            if(Convert.ToInt32(TipoUE.SelectedValue) < 0){
                errores_list.Add("Seleccione Tipo de Unidad Espacial.");
            }
            if (Convert.ToInt32(TiposReportes.SelectedValue) < 0)
            {
                errores_list.Add("Seleccione Tipo de Reporte.");
            }
            
            if (errores_list.Count > 0)
            {
                Content_Errores = (Panel)fnc.muestraErrores(errores_list, Content_Errores);
                formOK = false;
            }
            else
            {
                Content_Errores.Visible = false;
            };

            return formOK;

        }


        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {

            int idTipoUE = Convert.ToInt32(TipoUE.SelectedValue);
            int idTipoTramite = 0;
            int idTipoReporte = Convert.ToInt32(TiposReportes.SelectedValue);
            int idTipoModificacion = 0;
            int idTipoRelocalizacion = 0;
            int idRegion = Convert.ToInt32(Regiones.SelectedValue);
            int idProvincia = Convert.ToInt32(Provincias.SelectedValue);
            int idComuna = Convert.ToInt32(Comunas.SelectedValue);
            int macrozona = Convert.ToInt32(Macrozonas.SelectedValue);
            int barrio = Convert.ToInt32(Barrios.SelectedValue);
            string especies = fnc.ListBox_ItemsSelectedGET(Especies.Items);
            int idGrupoEspecie = Convert.ToInt32(GrupoEspecie.SelectedValue);
            int idEstado = 0;
            string numPert = Pert.Text.Trim();
            string codigoCentro = CodigoCentro.Text.Trim();



            DateTime fechaInicio = new DateTime();
            DateTime fechaFin = new DateTime();

            if (!FechaDesde.Text.Trim().Equals(""))
            {
                fechaInicio = Convert.ToDateTime(FechaDesde.Text);
            }

            if (!FechaHasta.Text.Trim().Equals(""))
            {
                fechaFin = Convert.ToDateTime(FechaHasta.Text);
            }

            GridView0.DataSource = reporteService.GenerarReporte(idTipoReporte, idTipoUE, idTipoTramite, idTipoRelocalizacion, idTipoModificacion, idRegion, idProvincia, idComuna, macrozona, barrio, especies, idGrupoEspecie, idEstado, numPert, codigoCentro, fechaInicio, fechaFin);
            GridView0.DataBind();
            Content_Resultado.Visible = true;

            string script = @"<script type='text/javascript'>oculta_loading('cargando');</script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "mensaje_cargado", script, false);


        
        }

        protected void GridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridView grilla = (GridView)sender;
            grilla.PageIndex = e.NewPageIndex;
            grilla.DataBind();

            CargaGrilla();
        }
        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
         
        }
        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }



        protected void LimpiarDesde_Click(object sender, EventArgs e)
        {
            FechaDesde.Text = "";
        }



        protected void LimpiarHasta_Click(object sender, EventArgs e)
        {
            FechaHasta.Text = "";
        }



        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            
            CargaGrilla();
            

            GridView grilla = new GridView();

            grilla = GridView0;

            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export("Reporte.xls", grilla);
        }
    }
}
