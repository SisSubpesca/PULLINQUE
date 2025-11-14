using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.visacion;
using SubPesca.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;
using System.Threading;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;

namespace SubPesca.Solicitudes.Visaciones
{
    public partial class InicioVisacion : System.Web.UI.Page
    {

        Datos.Entidades.Usuario.Serializable usuario_logeado; // Usuario logueado en el sistema
        RegionDA regionDA = new RegionDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        VisacionService visacionService = new VisacionService();
        EnviarCorreo enviarCorreo = new EnviarCorreo();
        PermisosService permisosService = new PermisosService();

        protected void Page_Load(object sender, EventArgs e)
        {
            // PAGE LOAD
            if (!Page.IsPostBack)
            {

                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (usuario_logeado == null)
                {
                    Response.Redirect("~/ingreso.aspx");
                }

                if (!permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.INICIO_VISACIONES_MASIVAS }, usuario_logeado, null, rbAccion.ACCESO))
                {
                    Response.Redirect("~/ingreso.aspx");
                }

                Initialize_Form();

            }
        }


        protected void Initialize_Form()
        {
            Carga_Combobox("TiposSolicitud");
            TiposSolicitud.SelectedValue = "0";

            Carga_Combobox("TipoVisacion");
            TipoVisacion.SelectedValue = "0";

            Carga_Combobox("Region");
            Region.SelectedValue = "0";

            Carga_Combobox("Provincia");
            Provincia.SelectedValue = "0";

            Carga_Combobox("Comuna");
            Comuna.SelectedValue = "0";
        }


        protected void Carga_Combobox(string combobox)
        {


            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

            switch (combobox)
            {

                case "TiposSolicitud":
                    // Cargamos el combobox: Tipos de Solicitud
                    TiposSolicitud.Items.Clear();
                    TiposSolicitud.DataSource = parametroGenericoDA.ListarTipoTramiteSolicitud();
                    TiposSolicitud.DataTextField = "nombreTipoInterfaz";
                    TiposSolicitud.DataValueField = "idTipoTramite";
                    TiposSolicitud.DataBind();
                    TiposSolicitud.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    break;

                case "TipoVisacion":
                    // Cargamos el combobox: Tipos de Visacion
                    TipoVisacion.Items.Clear();
                    if (Convert.ToInt32(TiposSolicitud.SelectedValue) > 0)
                    {
                        TipoVisacion.DataSource = parametroGenericoDA.ListarTipoDocumentoVisacion(Convert.ToInt32(TiposSolicitud.SelectedValue));
                        TipoVisacion.DataTextField = "descripcion";
                        TipoVisacion.DataValueField = "id";
                        TipoVisacion.DataBind();
                    }
                    TipoVisacion.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    break;


                case "Resultado":
                    // Cargamos el combobox: Resultado
                    Resultado.Items.Clear();
                    if (Convert.ToInt32(TipoVisacion.SelectedValue) > 0)
                    {
                        Resultado.DataSource = parametroGenericoDA.ListarTipoDocumentoResultadoVisacion(Convert.ToInt32(TiposSolicitud.SelectedValue),Convert.ToInt32(TipoVisacion.SelectedValue));
                        Resultado.DataTextField = "descripcion";
                        Resultado.DataValueField = "id";
                        Resultado.DataBind();
                    }
                    Resultado.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    break;

                case "Region":
                    // Cargamos el combobox: Region
                    Region.Items.Clear();
                    Region.DataSource = regionDA.ListarRegion(0);
                    Region.DataTextField = "Region";
                    Region.DataValueField = "IdRegion";
                    Region.DataBind();
                    Region.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                case "Provincia":
                    // Cargamos el combobox: Provincia
                    Provincia.Items.Clear();
                    if (Convert.ToInt32(Region.SelectedValue) > 0)
                    {
                        Provincia.DataSource = parametroGenericoDA.ListarProvinciaReg(0, Convert.ToInt32(Region.SelectedValue));
                        Provincia.DataTextField = "descripcion";
                        Provincia.DataValueField = "id";
                        Provincia.DataBind();
                    };
                    Provincia.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    break;
                case "Comuna":
                    // Cargamos el combobox: Comuna
                    Comuna.Items.Clear();
                    if (Convert.ToInt32(Provincia.SelectedValue) > 0)
                    {
                        Comuna.DataSource = parametroGenericoDA.ListarComunaDataTable(0, Convert.ToInt32(Provincia.SelectedValue));
                        Comuna.DataTextField = "Comuna";
                        Comuna.DataValueField = "IdComuna";
                        Comuna.DataBind();
                    };
                    Comuna.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    break;

            };
        }

        protected void Region_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("Provincia");
            Carga_Combobox("Comuna");
        }

        protected void Provincias_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("Comuna");
        }

        protected void TiposSolicitud_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("TipoVisacion");

            PanelRelocalizaciones.Visible = false;
            UpdatePanelPanelRelocalizaciones.Update();

            if (Convert.ToInt32(TiposSolicitud.SelectedValue) > 0) {

                if (Convert.ToInt32(TiposSolicitud.SelectedValue) == rbTipo.RELOCALIZACION_SECTOR_CERO_RESA ||
                    Convert.ToInt32(TiposSolicitud.SelectedValue) == rbTipo.RELOCALIZACION_CREA_RESA ||
                    Convert.ToInt32(TiposSolicitud.SelectedValue) == rbTipo.RELOCALIZACION_FUSIONA_RESA ||
                    Convert.ToInt32(TiposSolicitud.SelectedValue) == rbTipo.RELOCALIZACION_SECTOR_CERO ||
                    Convert.ToInt32(TiposSolicitud.SelectedValue) == rbTipo.RELOCALIZACION_CREA ||
                    Convert.ToInt32(TiposSolicitud.SelectedValue) == rbTipo.RELOCALIZACION_FUSIONA
                    )
                {
                    PanelRelocalizaciones.Visible = true;
                    UpdatePanelPanelRelocalizaciones.Update();
                }
            
            }
        }


        protected void TipoVisacion_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("Resultado");

            if (Resultado.Items.Count > 1)
            {
                PanelResultado.Visible = true;
                UpdatePanelResultado.Update();
            }
            else 
            {
                PanelResultado.Visible = false;
                UpdatePanelResultado.Update();
            }

        }


        protected void Limpiar_Click(object sender, EventArgs e)
        {

            TiposSolicitud.SelectedValue = "0";
            UpdatePanelTiposSolicitud.Update();

            TipoVisacion.SelectedValue = "0";
            UpdatePanelTipoVisacion.Update();

            Resultado.SelectedValue = "0";
            PanelResultado.Visible = false;
            UpdatePanelResultado.Update();

            Region.SelectedValue = "0";
            UpdatePanelRegion.Update();

            Provincia.SelectedValue = "0";
            UpdatePanelProvincia.Update();

            Comuna.SelectedValue = "0";
            UpdatePanelComuna.Update();

            Pert.Text = "";
            UpdatePanelPert.Update();

            PanelRelocalizaciones.Visible = false;
            UpdatePanelPanelRelocalizaciones.Update();

            

        }


        protected void Buscar_Click(object sender, EventArgs e)
        {

            try
            {


                LabelMensajeGuardado.Text = "";
                PanelLabelMensajeGuardado.Visible = false;
                UpdatePanelLabelMensajeGuardado.Update();


                PanelMensajes.Visible = false;
                UpdatePanelMensajesValidaciones.Update();


                TipoTramiteHidden.Value = "";
                TipoVisacionHidden.Value = "";
                ResultadoVisacionHidden.Value = "";

                TipoTramiteDescripcionHidden.Value = "";
                TipoVisacionDescripcionHidden.Value = "";
                ResultadoVisacionDescripcionHidden.Value = "";


                VisacionMasiva visacionMasiva = new VisacionMasiva();

                if (Convert.ToInt32(TiposSolicitud.SelectedValue) > 0)
                {
                    visacionMasiva.tipoTramite = new ParametroGenerico(Convert.ToInt32(TiposSolicitud.SelectedValue));
                    TipoTramiteHidden.Value = TiposSolicitud.SelectedValue;
                    TipoTramiteDescripcionHidden.Value = TiposSolicitud.SelectedItem.Text;
                }
                else
                {
                    Page.Validators.Add(new ValidationError("FormErrores", "Tipo de Trámite"));
                }

                if (Convert.ToInt32(TipoVisacion.SelectedValue) > 0)
                {
                    visacionMasiva.tipoVisacion = new ParametroGenerico(Convert.ToInt32(TipoVisacion.SelectedValue), TipoVisacion.SelectedItem.ToString());
                    TipoVisacionHidden.Value = TipoVisacion.SelectedValue;
                    TipoVisacionDescripcionHidden.Value = TipoVisacion.SelectedItem.Text;
                }
                else
                {
                    Page.Validators.Add(new ValidationError("FormErrores", "Tipo de Visación"));
                }


                if (Resultado.Items.Count > 1) //PUEDE NO APLICAR RESULTADO PARA EL DOCUMENTO
                {
                    if (Convert.ToInt32(Resultado.SelectedValue) > 0)
                    {
                        visacionMasiva.resultado = new ParametroGenerico(Convert.ToInt32(Resultado.SelectedValue), Resultado.SelectedItem.ToString());
                        ResultadoVisacionHidden.Value = Resultado.SelectedValue;
                        ResultadoVisacionDescripcionHidden.Value = Resultado.SelectedItem.Text;
                    }
                    else
                    {
                        Page.Validators.Add(new ValidationError("FormErrores", "Resultado"));
                    }
                }


                if (Convert.ToInt32(Region.SelectedValue) > 0)
                {
                    visacionMasiva.region = new ParametroGenerico(Convert.ToInt32(Region.SelectedValue));
                }

                if (Convert.ToInt32(Provincia.SelectedValue) > 0)
                {
                    visacionMasiva.provincia = new ParametroGenerico(Convert.ToInt32(Provincia.SelectedValue));
                }

                if (Convert.ToInt32(Comuna.SelectedValue) > 0)
                {
                    visacionMasiva.comunaFiltro = new ParametroGenerico(Convert.ToInt32(Comuna.SelectedValue));
                }


                if (!Pert.Text.Trim().Equals(""))
                {
                    visacionMasiva.pertFiltro = Pert.Text;
                }


                visacionMasiva.usuario = (Datos.Entidades.Usuario.Serializable)Session["Usuario"]; ;

                if (Page.IsValid)
                {

                    if (visacionMasiva.pertFiltro != null)
                    {
                        String mensajePert = "";


                        mensajePert = visacionService.validarExistenciaPerts(visacionMasiva);


                        if (mensajePert != null && !mensajePert.Trim().Equals(""))
                        {

                            Page.Validators.Add(new ValidationError("FormErrores", "Los siguientes N° Pert/Identificador no existen, no cumplen con los criterios de búsqueda o no los tiene asignados: " + mensajePert));
                            PanelMensajes.Visible = true;
                            UpdatePanelMensajesValidaciones.Update();
                        
                        }
                    }
                  
                    //Alerta 9: Al momento de mandar a visar la carta ambiental, debe salir una alerta recordando que se debe incorporar la visación de planos 14 TER.



                    ViewState["FILTRO_VISACION_MASIVA"] = visacionMasiva;
                    CargaGrilla();

                }
                else
                {

                    PanelMensajes.Visible = true;
                    UpdatePanelMensajesValidaciones.Update();
                }



            }catch (Exception) {
                Page.Validators.Add(new ValidationError("FormErrores", "Ha ocurrido un error al realizar la acción solicitada"));
            }

            

        }

        protected void Limpiar_Grilla()
        {

            try
            {

                PanelBotones.Visible = false;
                UpdatePanelPanelBotones.Update();

                GridTramite.DataSource = null;
                GridTramite.DataBind();
                UpdatePanelGrilla.Update();

                Session["CheckedVisaFirmaIDs"] = null;


            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        protected void CargaGrilla()
        {


            try { 

                MensajeBusqueda.Text = "";
                PanelMensajeBusqueda.Visible = false;
                UpdatePanelMensajeBusqueda.Update();



                VisacionMasiva visacionMasivaFiltro = (VisacionMasiva)ViewState["FILTRO_VISACION_MASIVA"];

                List<VisacionMasiva> resultado = visacionService.ListarInicioVisacionMasiva(visacionMasivaFiltro);
                GridTramite.DataSource = resultado;
                GridTramite.DataBind();


                if (resultado.Count() == 0)
                {

                    PanelBotones.Visible = false;
                    UpdatePanelPanelBotones.Update();

                    MensajeBusqueda.Text = "Su búsqueda no ha obtenido resultados";
                    PanelMensajeBusqueda.Visible = true;
                    UpdatePanelMensajeBusqueda.Update();
                }
                else {

                    ButtonExportar.Visible = true;
                    PanelBotones.Visible = true;
                    UpdatePanelPanelBotones.Update();
                }


                Session["CheckedVisacionIDs"] = null;

            }
            catch(Exception ex){
                throw ex;
            }


        }



        protected void Guardar_Click(object sender, EventArgs e)
        {


            try
            {

                LabelMensajeGuardado.Text = "";
                PanelLabelMensajeGuardado.Visible = false;
                UpdatePanelLabelMensajeGuardado.Update();


                //SE ACTUALIZACION EN SESSION LOS CHECK SELECIONADOS EN LA PAGINA ACTUAL
                var selectedIDs = (Session["CheckedVisacionIDs"] != null) ? Session["CheckedVisacionIDs"] as List<int> : new List<int>();

                foreach (GridViewRow row in GridTramite.Rows)
                {

                    var selCheckBox = row.FindControl("chkEmp") as CheckBox;
                    var rowOrgID = Convert.ToInt32(GridTramite.DataKeys[row.RowIndex].Value);
                    var isRowIDPresentInList = selectedIDs.Contains(rowOrgID);

                    if (selCheckBox.Checked && !isRowIDPresentInList)
                    {
                        selectedIDs.Add(rowOrgID);
                    }

                    if (!selCheckBox.Checked && isRowIDPresentInList)
                    {
                        selectedIDs.Remove(rowOrgID);
                    }
                }
                Session["CheckedVisacionIDs"] = (selectedIDs.Count > 0) ? selectedIDs : null;


                List<int> seleccionados = (List<int>)Session["CheckedVisacionIDs"];

                if (seleccionados == null || seleccionados.Count() == 0)
                {
                    Page.Validators.Add(new ValidationError("GrupoBotonGuardar", "Seleccione al menos una solicitud para iniciar visación"));
                    UpdatePanelMensajeGuardar.Update();

                }
                else
                {


                    if (Page.IsValid)
                    {



                        List<VisacionMasiva> iniciovisaciones = new List<VisacionMasiva>();
                        VisacionMasiva visacion = null;

                        foreach (int itemSeleccionado in (List<int>)seleccionados)
                        {
                            visacion = new VisacionMasiva();
                            visacion.idSolConcesion = itemSeleccionado;

                            visacion.tipoTramite = new ParametroGenerico(Convert.ToInt32(TipoTramiteHidden.Value), TipoTramiteDescripcionHidden.Value);
                            visacion.tipoVisacion = new ParametroGenerico(Convert.ToInt32(TipoVisacionHidden.Value), TipoVisacionDescripcionHidden.Value);

                            //seteando el pert para el envio de correos
                            foreach (GridViewRow row in GridTramite.Rows)
                            {
                                var rowOrgID = Convert.ToInt32(GridTramite.DataKeys[row.RowIndex].Value);

                                if (rowOrgID == visacion.idSolConcesion) {
                                    GridViewRow gvRow = (GridViewRow)GridTramite.Rows[row.RowIndex]; 
                                    HiddenField hiddenFieldPert = (HiddenField)row.FindControl("HiddenPert");
                                    visacion.pert =  (hiddenFieldPert == null) ? "" : hiddenFieldPert.Value;
                                    break;
                                }
                            }

                            

                            

                            if (!ResultadoVisacionHidden.Value.Trim().Equals(""))
                            {
                                visacion.resultado = new ParametroGenerico(Convert.ToInt32(ResultadoVisacionHidden.Value), ResultadoVisacionDescripcionHidden.Value);
                            }

                            iniciovisaciones.Add(visacion);
                        }


                        bool var = visacionService.GenerarInicioVisacion(iniciovisaciones, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                        if (var)
                        {

                            LabelMensajeGuardado.Text = "Inicio Visación guardado con exito.";
                            PanelLabelMensajeGuardado.Visible = true;
                            UpdatePanelLabelMensajeGuardado.Update();

                            /* Envio de correo electrónico */
                            try
                            {
                                Thread newThread = new Thread(EnvioCorreo);
                                newThread.Start(iniciovisaciones);
                            }
                            catch (Exception)
                            {

                            }

                            //Dejar grilla en blanco
                            //Limpiar_Click(null, null);
                            Limpiar_Grilla();

                        }
                        else
                        {
                            Page.Validators.Add(new ValidationError("GrupoBotonGuardar", "Ha ocurrido un error al realizar la acción solicitada"));
                        }

                    }
                }

            }
            catch (Exception)
            {
                Page.Validators.Add(new ValidationError("GrupoBotonGuardar", "Ha ocurrido un error al realizar la acción solicitada"));
            }
            finally
            {

                string script = @"<script type='text/javascript'>oculta_loading('cargando');</script>";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "mensaje_cargado", script, false);
            }

        }


        /* Envio de correo electrónico */
        public void EnvioCorreo(object iniciovisaciones)
        {

            try
            {
                EnviarCorreo enviarCorreo = new EnviarCorreo();
                enviarCorreo.alertaInicioVisacion((List<VisacionMasiva>)iniciovisaciones);

            }
            catch (Exception)
            {

            }

        }



        #region Grilla En Tramite


        protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)GridTramite.HeaderRow.FindControl("chkboxSelectAll2");
            foreach (GridViewRow row in GridTramite.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkEmp");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
        }


        protected void GridTramite_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {


            VisacionMasiva visacionMasivaFiltro;
            visacionMasivaFiltro = (VisacionMasiva)ViewState["FILTRO_VISACION_MASIVA"];
            if (visacionMasivaFiltro == null)
            {
                visacionMasivaFiltro = new VisacionMasiva();
            }


            //ANTES DE CAMBIAR DE PAGINA SE ACTUALIZACION EN SESSION LOS CHECK SELECIONADOS

            var selectedIDs = (Session["CheckedVisacionIDs"] != null) ? Session["CheckedVisacionIDs"] as List<int> : new List<int>();

            foreach (GridViewRow row in GridTramite.Rows)
            {

                var selCheckBox = row.FindControl("chkEmp") as CheckBox;
                var rowOrgID = Convert.ToInt32(GridTramite.DataKeys[row.RowIndex].Value);
                var isRowIDPresentInList = selectedIDs.Contains(rowOrgID);

                if (selCheckBox.Checked && !isRowIDPresentInList)
                {
                    selectedIDs.Add(rowOrgID);
                }

                if (!selCheckBox.Checked && isRowIDPresentInList)
                {
                    selectedIDs.Remove(rowOrgID);
                }
            }



            visacionMasivaFiltro.pagina = e.NewPageIndex;
            ViewState["FILTRO_VISACION_MASIVA"] = visacionMasivaFiltro;

            GridTramite.PageIndex = e.NewPageIndex;
            GridTramite.DataBind();
            CargaGrilla();




            //DESPUES DE CAMBIAR DE PAGINA, SE SETEAN LOS VALORES EN LOS CHECK SEGUN LA INFORMACION QUE ESTE EN SESSION
            bool marcarTodos = true;
            foreach (GridViewRow row in GridTramite.Rows)
            {
                var emailCheckBox = row.FindControl("chkEmp") as CheckBox;
                var rowOrgID = Convert.ToInt32(GridTramite.DataKeys[row.RowIndex].Value);
                if (selectedIDs.Contains(rowOrgID))
                {
                    emailCheckBox.Checked = true;
                }
                else
                {
                    marcarTodos = false;
                }
            }

            if (marcarTodos)
            {
                CheckBox ChkBoxHeader = (CheckBox)GridTramite.HeaderRow.FindControl("chkboxSelectAll2");
                ChkBoxHeader.Checked = true;
            }

            Session["CheckedVisacionIDs"] = (selectedIDs.Count > 0) ? selectedIDs : null;



        }


        protected void GridTramite_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }


        protected void GridTramite_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }


        protected void ExportarGrillaReposicion_Click(object sender, EventArgs e)
        {

            CargaGrilla();

            GridView grilla = new GridView();
            grilla = GridTramite;
            string ngrilla = "solicitudVisacion.xls";


            GridTramite.Columns.RemoveAt(0);

            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);

        }


        #endregion

    }
}