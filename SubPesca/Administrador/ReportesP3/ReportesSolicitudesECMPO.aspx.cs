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
    public partial class ReportesSolicitudesECMPO : System.Web.UI.Page
    {
        ReporteService reporteService = new ReporteService();
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema

        Datos.Utilidades.Funciones fnc = new Datos.Utilidades.Funciones();

        ComunaDA comunaDA = new ComunaDA();
        RegionDA regionDA = new RegionDA();
        ProvinciaDA provinciaDA = new ProvinciaDA();
        MacrozonaDA macrozonaDA = new MacrozonaDA();
        BarrioDA barrioDA = new BarrioDA();
        CentroCultivoDA centrosCultivoDA = new CentroCultivoDA();
        EtapaCultivoDA etapaCultivoDA = new EtapaCultivoDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        MantenedorDA mantenedorDA = new MantenedorDA();


        private static int idTipoUE = 534;



        // PAGE LOAD
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                // Inicializamos el formulario   
                Initialize_Form();
            };
            Content_Errores.Visible = false;
        }



        protected void Initialize_Form()
        {
            tr_tipoReporte.Visible = false;
            tr_tipoModificacion.Visible = false;
            tr_tipoRelocalizacion.Visible = false;
            tr_region.Visible = false;
            tr_provincia.Visible = false;
            tr_comuna.Visible = false;
            tr_macrozona.Visible = false;
            tr_barrio.Visible = false;
            tr_codigocentro.Visible = false;
            tr_pert.Visible = false;
            tr_especies.Visible = false;
            tr_estado.Visible = false;
            tr_grupoEspecie.Visible = false;

            // Carga de los Comboboxs Base
            Carga_Combobox("TipoUE");
            Carga_Combobox("TipoTramite");
            Carga_Combobox("TipoModificacion");
            Carga_Combobox("TipoRelocalizacion");
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
            Carga_Combobox("Estados");
            Carga_Combobox("GrupoEspecie");



        }


        protected void TiposTramite_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            tr_tipoModificacion.Visible = false;
            tr_tipoReporte.Visible = false;
            tr_tipoRelocalizacion.Visible = false;
            tr_region.Visible = false;
            tr_provincia.Visible = false;
            tr_comuna.Visible = false;
            tr_macrozona.Visible = false;
            tr_barrio.Visible = false;
            tr_codigocentro.Visible = false;
            tr_pert.Visible = false;
            tr_especies.Visible = false;
            tr_estado.Visible = false;
            tr_grupoEspecie.Visible = false;

            int idTipoTramite = Convert.ToInt32(TipoTramite.SelectedValue);

            switch (idTipoTramite)
            {
                case 88:  //SOLICITUD DE CONCESION DE ACUICULTURA
                    tr_tipoReporte.Visible = true;
                    tr_region.Visible = true;
                    tr_provincia.Visible = true;
                    tr_comuna.Visible = true;
                    tr_macrozona.Visible = true;
                    tr_barrio.Visible = true;
                    tr_especies.Visible = true;
                    tr_estado.Visible = true;
                    tr_grupoEspecie.Visible = true;

                    Carga_Combobox("TiposReportes");
                    Carga_Combobox("Estados");

                    break;

                case 89://SOLICITUD DE MODIFICACION DE CONCESION DE ACUICULTURA

                    tr_tipoModificacion.Visible = true;
                    tr_tipoReporte.Visible = true;
                    tr_region.Visible = true;
                    tr_provincia.Visible = true;
                    tr_comuna.Visible = true;
                    tr_macrozona.Visible = true;
                    tr_barrio.Visible = true;
                    tr_especies.Visible = true;
                    tr_estado.Visible = true;
                    tr_grupoEspecie.Visible = true;

                    Carga_Combobox("TiposReportes");
                    Carga_Combobox("Estados");

                    break;

                case 536://SOLICITUD DE EXPERIMENTAL DE AMERB

                    tr_tipoReporte.Visible = true;
                    tr_region.Visible = true;
                    tr_provincia.Visible = true;
                    tr_comuna.Visible = true;
                    tr_macrozona.Visible = true;
                    tr_barrio.Visible = true;
                    tr_especies.Visible = true;
                    tr_estado.Visible = true;
                    tr_grupoEspecie.Visible = true;

                    Carga_Combobox("TiposReportes");
                    Carga_Combobox("Estados");

                    break;

                case 95://Solicitud de Relocalización por LEY

                    tr_tipoRelocalizacion.Visible = true;
                    tr_tipoReporte.Visible = true;
                    tr_region.Visible = true;
                    tr_provincia.Visible = true;
                    tr_comuna.Visible = true;
                    tr_macrozona.Visible = true;
                    tr_barrio.Visible = true;
                    tr_especies.Visible = true;
                    tr_estado.Visible = true;
                    tr_grupoEspecie.Visible = true;

                    Carga_Combobox("TipoRelocalizacion");
                    Carga_Combobox("TiposReportes");
                    Carga_Combobox("Estados");

                    break;

                case 624://Solicitud de Relocalización RESA

                    tr_tipoRelocalizacion.Visible = true;
                    tr_tipoReporte.Visible = true;
                    tr_region.Visible = true;
                    tr_provincia.Visible = true;
                    tr_comuna.Visible = true;
                    tr_macrozona.Visible = true;
                    tr_barrio.Visible = true;
                    tr_especies.Visible = true;
                    tr_estado.Visible = true;
                    tr_grupoEspecie.Visible = true;

                    Carga_Combobox("TipoRelocalizacion");
                    Carga_Combobox("TiposReportes");
                    Carga_Combobox("Estados");

                    break;


                case 124://Solicitud de Centro de Faenamiento

                    tr_tipoReporte.Visible = true;
                    tr_region.Visible = true;
                    tr_provincia.Visible = true;
                    tr_comuna.Visible = true;
                    tr_especies.Visible = true;
                    tr_estado.Visible = true;
                    tr_grupoEspecie.Visible = true;

                    Carga_Combobox("TiposReportes");
                    Carga_Combobox("Estados");

                    break;

                case 546://Solicitud de Modificación Centro de Faenamiento

                    tr_tipoModificacion.Visible = true;
                    tr_tipoReporte.Visible = true;
                    tr_region.Visible = true;
                    tr_provincia.Visible = true;
                    tr_comuna.Visible = true;
                    tr_especies.Visible = true;
                    tr_estado.Visible = true;
                    tr_grupoEspecie.Visible = true;

                    Carga_Combobox("TiposReportes");
                    Carga_Combobox("Estados");

                    break;


                case 121://Solicitud de Acopio

                    tr_tipoReporte.Visible = true;
                    tr_region.Visible = true;
                    tr_provincia.Visible = true;
                    tr_comuna.Visible = true;
                    tr_especies.Visible = true;
                    tr_estado.Visible = true;
                    tr_grupoEspecie.Visible = true;

                    Carga_Combobox("TiposReportes");
                    Carga_Combobox("Estados");

                    break;

                case 552://Solicitud de Modificación Acopio

                    tr_tipoModificacion.Visible = true;
                    tr_tipoReporte.Visible = true;
                    tr_region.Visible = true;
                    tr_provincia.Visible = true;
                    tr_comuna.Visible = true;
                    tr_especies.Visible = true;
                    tr_estado.Visible = true;
                    tr_grupoEspecie.Visible = true;

                    Carga_Combobox("TiposReportes");
                    Carga_Combobox("Estados");

                    break;

                case 122://Solicitud de acuicultura en Amerb

                    tr_tipoReporte.Visible = true;
                    tr_region.Visible = true;
                    tr_provincia.Visible = true;
                    tr_comuna.Visible = true;
                    tr_especies.Visible = true;
                    tr_estado.Visible = true;
                    tr_grupoEspecie.Visible = true;

                    Carga_Combobox("TiposReportes");
                    Carga_Combobox("Estados");

                    break;

                case 558://Solicitud de Modificación de acuicultura en amerb

                    tr_tipoModificacion.Visible = true;
                    tr_tipoReporte.Visible = true;
                    tr_region.Visible = true;
                    tr_provincia.Visible = true;
                    tr_comuna.Visible = true;
                    tr_especies.Visible = true;
                    tr_estado.Visible = true;
                    tr_grupoEspecie.Visible = true;

                    Carga_Combobox("TiposReportes");
                    Carga_Combobox("Estados");

                    break;

                case 535://Solicitud de experimentales en Amerb

                    tr_tipoReporte.Visible = true;
                    tr_region.Visible = true;
                    tr_provincia.Visible = true;
                    tr_comuna.Visible = true;
                    tr_especies.Visible = true;
                    tr_estado.Visible = true;
                    tr_grupoEspecie.Visible = true;

                    Carga_Combobox("TiposReportes");
                    Carga_Combobox("Estados");

                    break;

                case 123://Solicitud de colectores de semilla

                    tr_tipoReporte.Visible = true;
                    tr_region.Visible = true;
                    tr_provincia.Visible = true;
                    tr_comuna.Visible = true;
                    tr_especies.Visible = true;
                    tr_estado.Visible = true;
                    tr_grupoEspecie.Visible = true;

                    Carga_Combobox("TiposReportes");
                    Carga_Combobox("Estados");

                    break;


                case 537://Solicitud de ECMPO

                    tr_tipoReporte.Visible = true;
                    tr_region.Visible = true;
                    tr_provincia.Visible = true;
                    tr_comuna.Visible = true;
                    tr_especies.Visible = true;
                    tr_estado.Visible = true;
                    tr_grupoEspecie.Visible = true;

                    Carga_Combobox("TiposReportes");
                    Carga_Combobox("Estados");

                    break;

                case 564://Solicitud de Modificación ECMPO

                    tr_tipoModificacion.Visible = true;
                    tr_tipoReporte.Visible = true;
                    tr_region.Visible = true;
                    tr_provincia.Visible = true;
                    tr_comuna.Visible = true;
                    tr_especies.Visible = true;
                    tr_estado.Visible = true;
                    tr_grupoEspecie.Visible = true;

                    Carga_Combobox("TiposReportes");
                    Carga_Combobox("Estados");

                    break;


            };

            Limpiar_PorTipoTramite(null, null);
        }

        protected void Carga_Combobox(string combobox)
        {
            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];


            switch (combobox)
            {


                case "TipoTramite":

                    TipoTramite.Items.Clear();


                    if (idTipoUE == rbTipo.UNID_ESPACIAL_ECMPO)
                    {
                        TipoTramite.Items.Add(new ListItem("Solicitud de Acuicultura en Ecmpo", Convert.ToString(rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO)));
                        TipoTramite.Items.Add(new ListItem("Solicitud de Modificación Acuicultura en Ecmpo", Convert.ToString(rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_ECMPO)));
                    }


                    TipoTramite.DataBind();
                    TipoTramite.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;


                case "TipoModificacion":

                    TipoModificacion.Items.Clear();

                    if (idTipoUE == rbTipo.UNID_ESPACIAL_ECMPO)
                    {
                        TipoModificacion.Items.Add(new ListItem("Solicitud Modificación ECMPO: Ampliación de Superficie", Convert.ToString(rbTipo.MOD_ECMPO_AMPLIA_SUPERFICIE)));
                        TipoModificacion.Items.Add(new ListItem("Solicitud Modificación ECMPO: Reducción de Superficie", Convert.ToString(rbTipo.MOD_ECMPO_REDUCE_SUPERFICIE)));
                        TipoModificacion.Items.Add(new ListItem("Solicitud Modificación ECMPO: Especie", Convert.ToString(rbTipo.MOD_ECMPO_ESPECIE)));
                        TipoModificacion.Items.Add(new ListItem("Solicitud Modificación ECMPO: Proyecto Técnico", Convert.ToString(rbTipo.MOD_ECMPO_PT)));
                        TipoModificacion.Items.Add(new ListItem("Solicitud Modificación ECMPO: Regularización", Convert.ToString(rbTipo.MOD_ECMPO_REGULARIZACION)));
                    }


                    TipoModificacion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    TipoModificacion.DataBind();


                    break;


                case "TipoRelocalizacion":

                    TipoRelocalizacion.Items.Clear();

                    int idTipoTramite3 = Convert.ToInt32(TipoTramite.SelectedValue);


                    if (idTipoTramite3 > 0)
                    {

                        if (idTipoUE == rbTipo.UNID_ESPACIAL_CONCESION && idTipoTramite3 == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION)
                        {

                            TipoRelocalizacion.Items.Add(new ListItem("Solicitud Relocalización: Sector 0", Convert.ToString(rbTipo.RELOCALIZACION_SECTOR_CERO)));
                            TipoRelocalizacion.Items.Add(new ListItem("Solicitud Relocalización: Crea", Convert.ToString(rbTipo.RELOCALIZACION_CREA)));
                            TipoRelocalizacion.Items.Add(new ListItem("Solicitud Relocalización: Fusiona", Convert.ToString(rbTipo.RELOCALIZACION_FUSIONA)));
                        }

                        if (idTipoUE == rbTipo.UNID_ESPACIAL_CONCESION && idTipoTramite3 == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION_RESA)
                        {
                            TipoRelocalizacion.Items.Add(new ListItem("Solicitud Relocalización RESA: Sector 0", Convert.ToString(rbTipo.RELOCALIZACION_SECTOR_CERO_RESA)));
                            TipoRelocalizacion.Items.Add(new ListItem("Solicitud Relocalización RESA: Crea", Convert.ToString(rbTipo.RELOCALIZACION_CREA_RESA)));
                            TipoRelocalizacion.Items.Add(new ListItem("Solicitud Relocalización RESA: Fusiona", Convert.ToString(rbTipo.RELOCALIZACION_FUSIONA_RESA)));

                        }

                    }


                    TipoRelocalizacion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    TipoRelocalizacion.DataBind();


                    break;
                case "TiposReportes":
                    // Cargamos el combobox: Tipos de Reportes
                    TiposReportes.Items.Clear();

                    int idTipoTramite = Convert.ToInt32(TipoTramite.SelectedValue);
                    int idTipoModificacion = Convert.ToInt32(TipoModificacion.SelectedValue);


                    if (idTipoTramite == rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO)
                    {
                        //TiposReportes.Items.Add(new ListItem(TipoReportes.REPORTES_UNIDADES_ESPACIALES_STRING, Convert.ToString(TipoReportes.REPORTES_UNIDADES_ESPACIALES)));
                        TiposReportes.Items.Add(new ListItem(TipoReportes.SOLICITUD_EN_TRAMITE_CON_DOCUMENTACION_01_STRING, Convert.ToString(TipoReportes.SOLICITUD_EN_TRAMITE_CON_DOCUMENTACION_01)));
                        TiposReportes.Items.Add(new ListItem(TipoReportes.SOLICITUD_EN_TRAMITE_CON_DOCUMENTACION_02_STRING, Convert.ToString(TipoReportes.SOLICITUD_EN_TRAMITE_CON_DOCUMENTACION_02)));
                        TiposReportes.Items.Add(new ListItem(TipoReportes.SOLICITUD_EN_TRAMITE_CON_DOCUMENTACION_03_STRING, Convert.ToString(TipoReportes.SOLICITUD_EN_TRAMITE_CON_DOCUMENTACION_03)));
                        TiposReportes.Items.Add(new ListItem(TipoReportes.SOLICITUD_EN_TRAMITE_CON_COORDENADAS_STRING, Convert.ToString(TipoReportes.SOLICITUD_EN_TRAMITE_CON_COORDENADAS)));
                        TiposReportes.Items.Add(new ListItem(TipoReportes.SOLICITUD_EN_TRAMITE_SSP_SSFFA_STRING, Convert.ToString(TipoReportes.SOLICITUD_EN_TRAMITE_SSP_SSFFA)));
                        //TiposReportes.Items.Add(new ListItem(TipoReportes.REPORTE_DE_CENTROS_DE_CULTIVO_STRING, Convert.ToString(TipoReportes.REPORTE_DE_CENTROS_DE_CULTIVO)));
                        //TiposReportes.Items.Add(new ListItem(TipoReportes.REPORTE_DE_ANALISIS_DE_VIGENCIA_STRING, Convert.ToString(TipoReportes.REPORTE_DE_ANALISIS_DE_VIGENCIA)));
                    }


                    if (idTipoTramite == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_ECMPO)
                    {

                        //TiposReportes.Items.Add(new ListItem(TipoReportes.REPORTES_UNIDADES_ESPACIALES_STRING, Convert.ToString(TipoReportes.REPORTES_UNIDADES_ESPACIALES)));
                        TiposReportes.Items.Add(new ListItem(TipoReportes.SOLICITUD_EN_TRAMITE_CON_DOCUMENTACION_01_STRING, Convert.ToString(TipoReportes.SOLICITUD_EN_TRAMITE_CON_DOCUMENTACION_01)));
                        TiposReportes.Items.Add(new ListItem(TipoReportes.SOLICITUD_EN_TRAMITE_CON_DOCUMENTACION_02_STRING, Convert.ToString(TipoReportes.SOLICITUD_EN_TRAMITE_CON_DOCUMENTACION_02)));
                        TiposReportes.Items.Add(new ListItem(TipoReportes.SOLICITUD_EN_TRAMITE_CON_DOCUMENTACION_03_STRING, Convert.ToString(TipoReportes.SOLICITUD_EN_TRAMITE_CON_DOCUMENTACION_03)));
                        TiposReportes.Items.Add(new ListItem(TipoReportes.SOLICITUD_EN_TRAMITE_CON_COORDENADAS_STRING, Convert.ToString(TipoReportes.SOLICITUD_EN_TRAMITE_CON_COORDENADAS)));
                        TiposReportes.Items.Add(new ListItem(TipoReportes.SOLICITUD_EN_TRAMITE_SSP_SSFFA_STRING, Convert.ToString(TipoReportes.SOLICITUD_EN_TRAMITE_SSP_SSFFA)));
                        //TiposReportes.Items.Add(new ListItem(TipoReportes.REPORTE_DE_CENTROS_DE_CULTIVO_STRING, Convert.ToString(TipoReportes.REPORTE_DE_CENTROS_DE_CULTIVO)));
                        //TiposReportes.Items.Add(new ListItem(TipoReportes.REPORTE_DE_ANALISIS_DE_VIGENCIA_STRING, Convert.ToString(TipoReportes.REPORTE_DE_ANALISIS_DE_VIGENCIA)));

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

                case "Estados":


                    Estado.Items.Clear();

                    int idTipoTramite2 = Convert.ToInt32(TipoTramite.SelectedValue);

                    //si es una modificacion se considera el tipo de mofificacion como tramite
                    if (idTipoTramite2 == rbTipo.TIPO_TRAMITE_MODIFICACION || idTipoTramite2 == rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION || idTipoTramite2 == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_FAENAMIENTO || idTipoTramite2 == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_ACOPIO || idTipoTramite2 == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB || idTipoTramite2 == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_ECMPO)
                    {

                        int idTipoModificacion2 = Convert.ToInt32(TipoModificacion.SelectedValue);

                        if (idTipoModificacion2 > 0)
                        {
                            idTipoTramite2 = idTipoModificacion2;
                        }
                    }

                    //si es una relocalizacion se considera el tipo de relocalizacion como tramite
                    if (idTipoTramite2 == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION || idTipoTramite2 == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION_RESA)
                    {

                        int idTipoRelocalizacion2 = Convert.ToInt32(TipoRelocalizacion.SelectedValue);

                        if (idTipoRelocalizacion2 > 0)
                        {
                            idTipoTramite2 = idTipoRelocalizacion2;
                        }
                    }


                    List<ParametroGenerico> resp = reporteService.ListarEstadosPorTipoTramite(idTipoTramite2);
                    if (resp != null)
                    {
                        foreach (ParametroGenerico item in resp)
                        {
                            Estado.Items.Add(new ListItem(item.descripcion, Convert.ToString(item.id)));
                        }
                    }


                    Estado.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    Estado.DataBind();




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



        protected void TipoRelocalizacion_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            Estado.SelectedValue = "-1";
            Carga_Combobox("Estados");

        }


        protected void TipoModificacion_OnSelectedIndexChanged(object sender, EventArgs e)
        {


            Estado.SelectedValue = "-1";
            Carga_Combobox("Estados");

        }


        protected void TiposReportes_OnSelectedIndexChanged(object sender, EventArgs e)
        {
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

            TipoTramite.SelectedValue = "-1";
            TipoModificacion.SelectedValue = "-1";
            TipoRelocalizacion.SelectedValue = "-1";
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
            Estado.SelectedValue = "-1";

            Carga_Combobox("Provincias");
            Carga_Combobox("Comunas");
            Carga_Combobox("Macrozonas");
            Carga_Combobox("Barrios");


            TiposTramite_OnSelectedIndexChanged(null, null);
            TipoModificacion_OnSelectedIndexChanged(null, null);

            Content_Errores.Visible = false;
            Content_Resultado.Visible = false;



        }

        protected void Limpiar_PorTipoTramite(object sender, EventArgs e)
        {
            TipoModificacion.SelectedValue = "-1";
            TipoRelocalizacion.SelectedValue = "-1";
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
            Estado.SelectedValue = "-1";

            Carga_Combobox("Provincias");
            Carga_Combobox("Comunas");
            Carga_Combobox("Macrozonas");
            Carga_Combobox("Barrios");


            TipoModificacion_OnSelectedIndexChanged(null, null);

            Content_Errores.Visible = false;
            Content_Resultado.Visible = false;


        }


        protected bool validarForm()
        {

            List<string> errores_list = new List<string>();
            bool formOK = true;


            if (Convert.ToInt32(TipoTramite.SelectedValue) < 0)
            {
                errores_list.Add("Seleccione Tipo de Trámite.");
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

            int idTipoTramite = Convert.ToInt32(TipoTramite.SelectedValue);
            int idTipoReporte = Convert.ToInt32(TiposReportes.SelectedValue);
            int idTipoModificacion = Convert.ToInt32(TipoModificacion.SelectedValue);
            int idTipoRelocalizacion = Convert.ToInt32(TipoRelocalizacion.SelectedValue);
            int idRegion = Convert.ToInt32(Regiones.SelectedValue);
            int idProvincia = Convert.ToInt32(Provincias.SelectedValue);
            int idComuna = Convert.ToInt32(Comunas.SelectedValue);
            int macrozona = Convert.ToInt32(Macrozonas.SelectedValue);
            int barrio = Convert.ToInt32(Barrios.SelectedValue);
            string especies = fnc.ListBox_ItemsSelectedGET(Especies.Items);
            int idGrupoEspecie = Convert.ToInt32(GrupoEspecie.SelectedValue);
            int idEstado = Convert.ToInt32(Estado.SelectedValue);
            string numPert = Pert.Text.Trim();
            string codigoCentro = CodigoCentro.Text.Trim();

            DateTime fechaInicio = new DateTime();
            DateTime fechaFin = new DateTime();

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
