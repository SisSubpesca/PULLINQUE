using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.servicios.visacion;
using Datos.Entidades;
using SubPesca.Utilidades;
using System.Collections;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;
using System.Threading;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;

namespace SubPesca.Solicitudes.Visaciones
{
    public partial class VisarFirmar : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        RegionDA regionDA = new RegionDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        VisacionService visacionService = new VisacionService();
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

                if (!permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.VISACIONES_MASIVAS }, usuario_logeado, null, rbAccion.ACCESO))
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
                        Resultado.DataSource = parametroGenericoDA.ListarTipoDocumentoResultadoVisacion(Convert.ToInt32(TiposSolicitud.SelectedValue), Convert.ToInt32(TipoVisacion.SelectedValue));
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

            PanelMensajeVisacion14TER.Visible = false;
            UpdatePanelMensajeVisacion14TER.Update();

            PanelRelocalizaciones.Visible = false;
            UpdatePanelPanelRelocalizaciones.Update();

            if (Convert.ToInt32(TiposSolicitud.SelectedValue) > 0)
            {

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

            if (TipoVisacion.SelectedItem.Value.Equals("30") || TipoVisacion.SelectedItem.Value.Equals("31") || TipoVisacion.SelectedItem.Value.Equals("200"))
            {
                if (Convert.ToInt32(TiposSolicitud.SelectedValue) != rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB &&
                    Convert.ToInt32(TiposSolicitud.SelectedValue) != rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB &&
                    Convert.ToInt32(TiposSolicitud.SelectedValue) != rbTipo.MOD_AMERB_AMPLIA_SUPERFICIE &&
                    Convert.ToInt32(TiposSolicitud.SelectedValue) != rbTipo.MOD_AMERB_ESPECIE &&
                    Convert.ToInt32(TiposSolicitud.SelectedValue) != rbTipo.MOD_AMERB_PT &&
                    Convert.ToInt32(TiposSolicitud.SelectedValue) != rbTipo.MOD_AMERB_REDUCE_SUPERFICIE &&
                    Convert.ToInt32(TiposSolicitud.SelectedValue) != rbTipo.MOD_AMERB_REGULARIZACION)
                {
                    PanelMensajeVisacion14TER.Visible = true;
                    UpdatePanelMensajeVisacion14TER.Update();
                }
            }
            else {
                PanelMensajeVisacion14TER.Visible = false;
                UpdatePanelMensajeVisacion14TER.Update();
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

            PanelMensajeVisacion14TER.Visible = false;
            UpdatePanelMensajeVisacion14TER.Update();

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
                TipoTramiteDescHidden.Value = "";

                TipoVisacionHidden.Value = "";
                TipoVisacionDescHidden.Value = "";

                ResultadoVisacionHidden.Value = "";


                VisacionMasiva visacionMasiva = new VisacionMasiva();

                if (Convert.ToInt32(TiposSolicitud.SelectedValue) > 0)
                {
                    visacionMasiva.tipoTramite = new ParametroGenerico(Convert.ToInt32(TiposSolicitud.SelectedValue), TiposSolicitud.SelectedItem.ToString());
                    TipoTramiteHidden.Value = TiposSolicitud.SelectedValue;
                    TipoTramiteDescHidden.Value = TiposSolicitud.SelectedItem.ToString();
                }
                else
                {
                    Page.Validators.Add(new ValidationError("FormErrores", "Tipo de Trámite"));
                }

                if (Convert.ToInt32(TipoVisacion.SelectedValue) > 0)
                {
                    visacionMasiva.tipoVisacion = new ParametroGenerico(Convert.ToInt32(TipoVisacion.SelectedValue), TipoVisacion.SelectedItem.ToString());
                    TipoVisacionHidden.Value = TipoVisacion.SelectedValue;
                    TipoVisacionDescHidden.Value = TipoVisacion.SelectedItem.ToString();
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


                if (Page.IsValid)
                {

                    if (visacionMasiva.pertFiltro != null)
                    {
                        String mensajePert = "";


                        mensajePert = visacionService.validarExistenciaPerts(visacionMasiva);


                        if (mensajePert != null && !mensajePert.Trim().Equals(""))
                        {

                            Page.Validators.Add(new ValidationError("FormErrores", "Los siguientes N° Pert/Identificador no existen o no cumplen con los criterios de búsqueda: " + mensajePert));
                            PanelMensajes.Visible = true;
                            UpdatePanelMensajesValidaciones.Update();

                        }
                    }


                    ViewState["FILTRO_VISA_FIRMA_MASIVA"] = visacionMasiva;
                    CargaGrilla();

                }
                else
                {

                    PanelMensajes.Visible = true;
                    UpdatePanelMensajesValidaciones.Update();
                }



            }
            catch (Exception)
            {
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


            try
            {

                MensajeBusqueda.Text = "";
                PanelMensajeBusqueda.Visible = false;
                UpdatePanelMensajeBusqueda.Update();



                VisacionMasiva visacionMasivaFiltro = (VisacionMasiva)ViewState["FILTRO_VISA_FIRMA_MASIVA"];


                visacionMasivaFiltro.buscaVisaOFirma = 1; //busca visaciones abiertas;

                List<VisacionMasiva> resultado = visacionService.ListarVisacionFirmaMasiva(visacionMasivaFiltro);
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
                else
                {
                    ButtonExportar.Visible = true;
                    PanelBotones.Visible = true;
                    UpdatePanelPanelBotones.Update();
                }


                Session["CheckedVisaFirmaIDs"] = null;

            }
            catch (Exception ex)
            {
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


                VisacionMasiva visacionMasivaH = null;

                //SE ACTUALIZACION EN SESSION LOS CHECK SELECIONADOS EN LA PAGINA ACTUAL
                var selectedIDs = (Session["CheckedVisaFirmaIDs"] != null) ? Session["CheckedVisaFirmaIDs"] as Hashtable : new Hashtable();

                foreach (GridViewRow row in GridTramite.Rows)
                {

                    var rdBttnRow = row.FindControl("chkEmp") as RadioButtonList;
                    var rowOrgID = Convert.ToInt32(GridTramite.DataKeys[row.RowIndex].Value);
                    var isRowIDPresentInList = selectedIDs.Contains(rowOrgID);


                    visacionMasivaH = new VisacionMasiva();
                    visacionMasivaH.idDocPestana = rowOrgID;

                    var SolConcesion = row.FindControl("HiddenIdSolConcesion") as HiddenField;
                    visacionMasivaH.idSolConcesion = Convert.ToInt32(SolConcesion.Value);

                    var SubRequerimiento = row.FindControl("HiddenIdSubRequerimiento") as HiddenField;
                    visacionMasivaH.requerimiento = new ParametroGenerico(Convert.ToInt32(SubRequerimiento.Value));

                    var Pert = row.FindControl("HiddenPert") as HiddenField;
                    visacionMasivaH.pert = Pert.Value.ToString();

                    if ((rdBttnRow.SelectedValue == "1" || rdBttnRow.SelectedValue == "2"))
                    {

                        if (!isRowIDPresentInList) //no existe en el hash
                        {

                            if (rdBttnRow.SelectedValue == "1") //firma
                            {
                                visacionMasivaH.corrige = false;
                                selectedIDs.Add(rowOrgID, visacionMasivaH);
                            }

                            if (rdBttnRow.SelectedValue == "2") //corrige
                            {
                                visacionMasivaH.corrige = true;
                                selectedIDs.Add(rowOrgID, visacionMasivaH);
                            }

                        }
                        else
                        { //ya existe en el hash, se debe actualizar la decision del usuario 

                            if (rdBttnRow.SelectedValue == "1") //firma
                            {
                                ((VisacionMasiva)selectedIDs[rowOrgID]).corrige = false;
                            }
                            if (rdBttnRow.SelectedValue == "2") //corrige
                            {
                                ((VisacionMasiva)selectedIDs[rowOrgID]).corrige = true;
                            }
                        }

                    }
                    else
                    {
                        selectedIDs.Remove(rowOrgID);
                    }
                }

                Session["CheckedVisaFirmaIDs"] = (selectedIDs.Count > 0) ? selectedIDs : null;


                Hashtable seleccionados = (Hashtable)Session["CheckedVisaFirmaIDs"];

                if (seleccionados == null || seleccionados.Count == 0)
                {
                    Page.Validators.Add(new ValidationError("GrupoBotonGuardar", "Seleccione al menos una solicitud para visar/firmar o corregir"));
                    UpdatePanelMensajeGuardar.Update();

                }
                else
                {


                    if (Page.IsValid)
                    {


                        List<VisacionMasiva> visaFirma = new List<VisacionMasiva>();

                        foreach (DictionaryEntry itemSeleccionado in (Hashtable)seleccionados)
                        {

                            ((VisacionMasiva)itemSeleccionado.Value).tipoTramite = new ParametroGenerico(Convert.ToInt32(TipoTramiteHidden.Value), TipoTramiteDescHidden.Value);
                            ((VisacionMasiva)itemSeleccionado.Value).tipoVisacion = new ParametroGenerico(Convert.ToInt32(TipoVisacionHidden.Value), TipoVisacionDescHidden.Value);

                            if (!ResultadoVisacionHidden.Value.Trim().Equals(""))
                            {
                                ((VisacionMasiva)itemSeleccionado.Value).resultado = new ParametroGenerico(Convert.ToInt32(ResultadoVisacionHidden.Value));
                            }

                            visaFirma.Add((VisacionMasiva)itemSeleccionado.Value);
                        }


                        bool var = visacionService.GenerarVisacionesFirmasCorrecciones(visaFirma, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                        if (var)
                        {

                            LabelMensajeGuardado.Text = "Visación guardada con éxito.";
                            PanelLabelMensajeGuardado.Visible = true;
                            UpdatePanelLabelMensajeGuardado.Update();


                            /* Envio de correo electrónico */
                            try
                            {
                                Thread newThread = new Thread(EnvioCorreo);
                                newThread.Start(visaFirma);
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
            finally {

                string script = @"<script type='text/javascript'>oculta_loading('cargando');</script>";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "mensaje_cargado", script, false);
            }
        }


        /* Envio de correo electrónico */
        public void EnvioCorreo(object visaFirma)
        {
            
            try
            {
                EnviarCorreo enviarCorreo = new EnviarCorreo();
                enviarCorreo.alertaVisacionesFirmasCorrecciones((List<VisacionMasiva>)visaFirma);
                
            }
            catch (Exception)
            {

            }
          
        }


        #region Grilla En Tramite


        protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            RadioButtonList RdBttnHeader = (RadioButtonList)GridTramite.HeaderRow.FindControl("chkboxSelectAll2");
            foreach (GridViewRow row in GridTramite.Rows)
            {
                RadioButtonList RdBttnRows = (RadioButtonList)row.FindControl("chkEmp");
                if (RdBttnHeader.SelectedValue == "1")
                {
                    RdBttnRows.SelectedValue = "1";
                }
                else
                {
                    RdBttnRows.SelectedValue = "2";
                }
                
            }
        }


        protected void GridTramite_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {


            VisacionMasiva visacionMasivaFiltro;
            visacionMasivaFiltro = (VisacionMasiva)ViewState["FILTRO_VISA_FIRMA_MASIVA"];
            if (visacionMasivaFiltro == null)
            {
                visacionMasivaFiltro = new VisacionMasiva();
            }


            //ANTES DE CAMBIAR DE PAGINA SE ACTUALIZACION EN SESSION LOS RADIO MARCADOS


            VisacionMasiva visacionMasivaH = null;
            var selectedIDs = (Session["CheckedVisaFirmaIDs"] != null) ? Session["CheckedVisaFirmaIDs"] as Hashtable : new Hashtable();

            foreach (GridViewRow row in GridTramite.Rows)
            {

                var rdBttnRow = row.FindControl("chkEmp") as RadioButtonList;
                var rowOrgID = Convert.ToInt32(GridTramite.DataKeys[row.RowIndex].Value); //clave de la fila (idDocPestana)
                var isRowIDPresentInList = selectedIDs.Contains(rowOrgID);

                visacionMasivaH = new VisacionMasiva();
                visacionMasivaH.idDocPestana = rowOrgID;
                
                var SolConcesion = row.FindControl("HiddenIdSolConcesion") as HiddenField;
                visacionMasivaH.idSolConcesion = Convert.ToInt32(SolConcesion.Value);

                var SubRequerimiento = row.FindControl("HiddenIdSubRequerimiento") as HiddenField;
                visacionMasivaH.requerimiento = new ParametroGenerico(Convert.ToInt32(SubRequerimiento.Value));

                if ((rdBttnRow.SelectedValue == "1" || rdBttnRow.SelectedValue == "2"))
                {

                    if (!isRowIDPresentInList) //no existe en el hash
                    {

                        if (rdBttnRow.SelectedValue == "1") //firma
                        {
                            visacionMasivaH.corrige = false;
                            selectedIDs.Add(rowOrgID, visacionMasivaH);
                        }

                        if (rdBttnRow.SelectedValue == "2") //corrige
                        {
                            visacionMasivaH.corrige = true;
                            selectedIDs.Add(rowOrgID, visacionMasivaH);
                        }

                    }
                    else //ya existe en el hash, se debe actualizar la decision del usuario 
                    { 

                        if (rdBttnRow.SelectedValue == "1") //firma
                        {
                            ((VisacionMasiva)selectedIDs[rowOrgID]).corrige = false;
                        }
                        if (rdBttnRow.SelectedValue == "2") //corrige
                        {
                            ((VisacionMasiva)selectedIDs[rowOrgID]).corrige = true;
                        }
                    }

                }
                else
                {
                    selectedIDs.Remove(rowOrgID);
                }
            }



            visacionMasivaFiltro.pagina = e.NewPageIndex;
            ViewState["FILTRO_VISA_FIRMA_MASIVA"] = visacionMasivaFiltro;

            GridTramite.PageIndex = e.NewPageIndex;
            GridTramite.DataBind();
            CargaGrilla();




            //DESPUES DE CAMBIAR DE PAGINA, SE SETEAN LOS VALORES EN LOS CHECK SEGUN LA INFORMACION QUE ESTE EN SESSION
            bool marcarTodos = true;
            bool visaTodos = true;
            bool corrigeTodos = true;

            foreach (GridViewRow row in GridTramite.Rows)
            {
                var rdBttnRow = row.FindControl("chkEmp") as RadioButtonList;
                var rowOrgID = Convert.ToInt32(GridTramite.DataKeys[row.RowIndex].Value); //id de la fila (idDocPestana)

                if (selectedIDs.Contains(rowOrgID))
                {
                    if (((VisacionMasiva)selectedIDs[rowOrgID]).corrige)
                    {
                        rdBttnRow.SelectedValue = "2";
                        visaTodos = false;
                    }
                    else {
                        rdBttnRow.SelectedValue = "1";
                        corrigeTodos = false;
                    }
                }
                else
                {
                    marcarTodos = false;
                }
            }

            if (marcarTodos)
            {
                RadioButtonList RdBttnHeader = (RadioButtonList)GridTramite.HeaderRow.FindControl("chkboxSelectAll2");
                if (visaTodos) { 
                    RdBttnHeader.SelectedValue = "1";
                }
                if (corrigeTodos)
                {
                    RdBttnHeader.SelectedValue = "2";
                }
                
            }

            Session["CheckedVisaFirmaIDs"] = (selectedIDs.Count > 0) ? selectedIDs : null;



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