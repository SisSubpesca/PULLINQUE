using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.cierreForzado;
using Datos.Contantes;
using System.Drawing;
using SubPesca.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;

namespace SubPesca.CierreForzado
{
    public partial class GenerarCierreForzadoRelocalizacionLey : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado;
        RegionDA regionDA = new RegionDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        CierreForzadoService cierreForzadoService = new CierreForzadoService();
        PestanaDA pestanaDA = new PestanaDA();
        Datos.Utilidades.Funciones fnc = new Datos.Utilidades.Funciones();
        int dbPestana = rbPestana.INFORME_DAC;
        int dbSeccion = rbSeccion.INFORME_DAC;
        PermisosService permisosService = new PermisosService();

        String mensaje = "";

        public String MensajeRegistro
        {
            get
            {
                return mensaje;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            // PAGE LOAD
            if (!Page.IsPostBack)
            {
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];
                //VERIFICAR QUE TENGA ACCESO
                if (!permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.GENERAR_CIERRE_FORZADO_SOLICITUD_RELOCALIZACION_LEY }, usuario_logeado, null, rbAccion.EDITAR))
                {
                    Response.Redirect("~/ingreso.aspx");
                }

                Initialize_Comboboxs();
            }

            string script = "calendario('" + Fecha.ClientID + "','" + fechaImgDinamica.ClientID + "');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + Fecha.ClientID, script.ToString(), true);

          


        }

        protected void Initialize_Comboboxs()
        {
            Carga_Combobox("TipoTramite");
            TipoTramite.SelectedValue = "0";

            Carga_Combobox("Region");
            Region.SelectedValue = "0";

            Carga_Combobox("Provincia");
            Provincia.SelectedValue = "0";

            Carga_Combobox("Comuna");
            Comuna.SelectedValue = "0";

            Carga_Combobox("Especie");
            Especie.SelectedValue = "0";

            Carga_Combobox("RCA");
            RCA.SelectedValue = "-1";


            Carga_Combobox("FlujoDocumental");
            FlujoDocumental.SelectedValue = "0";

            Carga_Combobox("TipoEntrada");
            TipoEntrada.SelectedValue = "0";

            Carga_Combobox("Origen");
            Origen.SelectedValue = "0";

            Carga_Combobox("TipoDocumento");
            TipoDocumento.SelectedValue = "0";

            Carga_Combobox("Resultado");
            Resultado.SelectedValue = "0";

            Carga_Combobox("Tipo");
            Tipo.SelectedValue = "0";

        }


        protected void Carga_Combobox(string combobox)
        {

            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

            switch (combobox)
            {

                case "TipoTramite":

                    TipoTramite.Items.Clear();
                    //TipoTramite.Items.Add(new ListItem("Solicitud de Concesión", Convert.ToString(rbTipo.TIPO_TRAMITE_SOLICITUD)));
                    TipoTramite.Items.Add(new ListItem("Trámite de Relocalización LEY", Convert.ToString(rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION)));
                    //TipoTramite.Items.Add(new ListItem("Trámite de Relocalización RESA", Convert.ToString(rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION_RESA)));
                    //TipoTramite.Items.Add(new ListItem("Trámite de Modificación", Convert.ToString(rbTipo.TIPO_TRAMITE_MODIFICACION)));
                    TipoTramite.DataBind();
                    TipoTramite.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;


                case "Region":
                    Region.Items.Clear();
                    Region.DataSource = regionDA.ListarRegion(0);
                    Region.DataTextField = "Region";
                    Region.DataValueField = "IdRegion";
                    Region.DataBind();
                    Region.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    break;

                case "Provincia":
                    Provincia.Items.Clear();

                    if (Convert.ToInt32(Region.SelectedValue) > 0)
                    {
                        Provincia.DataSource = parametroGenericoDA.ListarProvinciaReg(0, Convert.ToInt32(Region.SelectedValue));
                        Provincia.DataTextField = "descripcion";
                        Provincia.DataValueField = "id";
                        Provincia.DataBind();
                    }

                    Provincia.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    UpdatePanelProvincia.Update();
                    break;

                case "Comuna":
                    Comuna.Items.Clear();

                    if (Convert.ToInt32(Provincia.SelectedValue) > 0)
                    {
                        Comuna.DataSource = parametroGenericoDA.ListarComuna(0, Convert.ToInt32(Provincia.SelectedValue));
                        Comuna.DataTextField = "descripcion";
                        Comuna.DataValueField = "id";
                        Comuna.DataBind();
                    }

                    Comuna.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    UpdatePanelComuna.Update();
                    break;

                case "Especie":
                    Especie.Items.Clear();
                    Especie.DataSource = parametroGenericoDA.ListarEspecies(0, "", 0);
                    Especie.DataTextField = "descripcion";
                    Especie.DataValueField = "id";
                    Especie.DataBind();
                    Especie.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    break;

                case "RCA":
                    RCA.Items.Clear();
                    RCA.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    RCA.Items.Insert(1, new ListItem("Sí", "1"));
                    RCA.Items.Insert(2, new ListItem("No", "0"));
                    RCA.DataBind();
                    break;

                case "FlujoDocumental":

                    // Cargamos el combobox: Flujo Documental
                    FlujoDocumental.Items.Clear();
                    FlujoDocumental.Items.Add(new ListItem("Entrada", Convert.ToString("1")));
                    FlujoDocumental.DataBind();
                    FlujoDocumental.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    break;

                case "TipoEntrada":
                    // Cargamos el combobox: Tipo Salida

                    TipoEntrada.Items.Clear();
                    TipoEntrada.Items.Add(new ListItem("Ingreso sin Requerimiento", Convert.ToString("6")));
                    TipoEntrada.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    TipoEntrada.DataBind();

                    break;


                case "Origen":

                    // Cargamos el combobox: Tipo Salida
                    Origen.Items.Clear();
                    Origen.Items.Add(new ListItem("Unidad Tramites Sectoriales", Convert.ToString("11")));
                    Origen.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    Origen.DataBind();


                    break;



                case "Tipo":
                    // Cargamos el combobox: Tipo
                    Tipo.Items.Clear();
                    Tipo.Items.Add(new ListItem("Informe Técnico", Convert.ToString("85")));
                    Tipo.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    Tipo.DataBind();
                    break;


                case "TipoDocumento":
                    // Cargamos el combobox: Tipo Documento
                    TipoDocumento.Items.Clear();
                    TipoDocumento.Items.Add(new ListItem("Informe Principal", Convert.ToString("66")));
                    TipoDocumento.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    TipoDocumento.DataBind();
                    break;


                case "Resultado":
                    // Cargamos el combobox: Tipo Resultado
                    Resultado.Items.Clear();
                    Resultado.Items.Add(new ListItem("Rechaza", Convert.ToString("11")));
                    Resultado.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    Resultado.DataBind();

                    break;


            };
        }


        protected void Limpiar_Click(object sender, EventArgs e)
        {
            FlujoDocumental.SelectedValue = "0";
            TipoEntrada.SelectedValue = "0";
            Origen.SelectedValue = "0";
            TipoDocumento.SelectedValue = "0";
            Tipo.SelectedValue = "0";
            Numero.Text = "";
            Fecha.Text = "";
            Resultado.SelectedValue = "0";

        }


        protected void Region_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("Provincia");
            Carga_Combobox("Comuna");
        }


        protected void Provincia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("Comuna");
        }



        protected void FiltrarCargaGrilla(object sender, EventArgs e)
        {

            SolicitudConcesion solicitudConcesion = new SolicitudConcesion();
            DatosSolicitudUE datosSolicitudUE = new DatosSolicitudUE();


            solicitudConcesion.numPert = NPert.Text;
            if (NumIdentificador.Text != null && !NumIdentificador.Text.Trim().Equals(""))
            {
                solicitudConcesion.datosSolicitudUE = datosSolicitudUE;
                solicitudConcesion.datosSolicitudUE.numIdentSolicitud = Convert.ToInt32(NumIdentificador.Text);
            }

            //solicitudConcesion.tipoTramite = new ParametroGenerico(Convert.ToInt32(TipoTramite.SelectedValue));
            solicitudConcesion.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION);

            solicitudConcesion.region = new ParametroGenerico(Convert.ToInt32(Region.SelectedValue));
            solicitudConcesion.provincia = new ParametroGenerico(Convert.ToInt32(Provincia.SelectedValue));
            solicitudConcesion.comunaFiltro = new ParametroGenerico(Convert.ToInt32(Comuna.SelectedValue));
            solicitudConcesion.especieCadFiltro = fnc.ListBox_ItemsSelectedGET(Especie.Items);
            solicitudConcesion.tieneRCA = Convert.ToInt32(RCA.SelectedValue);

            if (TamanioConcesion.Text != null && !TamanioConcesion.Text.Trim().Equals(""))
            {
                solicitudConcesion.superficieCalculada = Convert.ToSingle(TamanioConcesion.Text);
            }

            solicitudConcesion.tipoUnidadEspacial = new ParametroGenerico(rbTipo.CONCESION_DE_ACUICULTURA);

            ViewState["FILTRO_CIERRE_FORZADO"] = solicitudConcesion;

            CargaGrilla();

        }



        protected void CargaGrilla()
        {
            MensajeBusqueda.Text = "";
            PanelMensajeBusqueda.Visible = false;
            UpdatePanelMensajeBusqueda.Update();


            PanelInformeRechazo.Visible = false;


            SolicitudConcesion filtro = (SolicitudConcesion)ViewState["FILTRO_CIERRE_FORZADO"];

            List<SolicitudConcesion> resultadosLista = (List<SolicitudConcesion>)cierreForzadoService.ListarSolicitudConcesionCierreForzado(filtro);
            GridVwHeaderChckbox.DataSource = resultadosLista;
            GridVwHeaderChckbox.DataBind();


            if (resultadosLista.Count() == 0)
            {
                MensajeBusqueda.Text = "Su búsqueda no ha obtenido resultados";
                PanelMensajeBusqueda.Visible = true;
                UpdatePanelMensajeBusqueda.Update();
            }
            else
            {
                PanelInformeRechazo.Visible = true;
            }


            Session["CheckedIDs"] = null;

        }


        public List<GridViewRow> GetSelectedDataKeys2(GridView control, string checkBoxId)
        {

            List<GridViewRow> lista = new List<GridViewRow>();

            foreach (GridViewRow row in control.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("chkEmp");

                if (check.Checked)
                {
                    lista.Add(row);
                }
            }

            return lista;
        }





        protected void GridVwHeaderChckbox_RowDataBound(object sender, GridViewRowEventArgs e)
        {


        }



        protected void GridVwHeaderChckbox_RowCommand(object sender, GridViewCommandEventArgs e)
        {


        }



        protected void GridVwHeaderChckbox_RowCreated(object sender, GridViewRowEventArgs e)
        {


        }



        protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)GridVwHeaderChckbox.HeaderRow.FindControl("chkboxSelectAll2");
            foreach (GridViewRow row in GridVwHeaderChckbox.Rows)
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




        protected void Guardar_Click(object sender, EventArgs e)
        {


            //Flujo Documental
            if (Convert.ToInt32(FlujoDocumental.SelectedValue) < 1)
            {
                Page.Validators.Add(new ValidationError("GrupoInformeTecnicoRechazo", "Seleccione Flujo Documental"));
            }


            //Tipo entrada
            if (Convert.ToInt32(TipoEntrada.SelectedValue) < 1)
            {
                Page.Validators.Add(new ValidationError("GrupoInformeTecnicoRechazo", "Seleccione Tipo de Entrada"));
            }


            //ORIGEN
            if (Convert.ToInt32(Origen.SelectedValue) < 1)
            {
                Page.Validators.Add(new ValidationError("GrupoInformeTecnicoRechazo", "Seleccione Origen"));
            }


            //TIPO DOCUMENTO
            if (Convert.ToInt32(TipoDocumento.SelectedValue) < 1)
            {
                Page.Validators.Add(new ValidationError("GrupoInformeTecnicoRechazo", "Seleccione Tipo Documento"));
            }


            //TIPO 
            if (Convert.ToInt32(Tipo.SelectedValue) < 1)
            {
                Page.Validators.Add(new ValidationError("GrupoInformeTecnicoRechazo", "Seleccione Tema"));
            }


            //NUMERO
            if (Numero.Text.Trim().Equals(""))
            {
                Page.Validators.Add(new ValidationError("GrupoInformeTecnicoRechazo", "Ingrese Número"));
            }


            //FECHA
            if (Fecha.Text.Trim().Equals(""))
            {
                Page.Validators.Add(new ValidationError("GrupoInformeTecnicoRechazo", "Ingrese Fecha"));
            }


            //ARCHIVO
            if (!ArchivoAdjunto.HasFile)
            {
                Page.Validators.Add(new ValidationError("GrupoInformeTecnicoRechazo", "Seleccione Archivo"));
            }


            //RESULTADO
            if (Convert.ToInt32(Resultado.SelectedValue) < 1)
            {
                Page.Validators.Add(new ValidationError("GrupoInformeTecnicoRechazo", "Seleccione Resultado"));
            }


            //SE ACTUALIZACION EN SESSION LOS CHECK SELECIONADOS EN LA PAGINA ACTUAL
            var selectedIDs = (Session["CheckedIDs"] != null) ? Session["CheckedIDs"] as List<int> : new List<int>();

            foreach (GridViewRow row in GridVwHeaderChckbox.Rows)
            {

                var selCheckBox = row.FindControl("chkEmp") as CheckBox;
                var rowOrgID = Convert.ToInt32(GridVwHeaderChckbox.DataKeys[row.RowIndex].Value);
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
            Session["CheckedIDs"] = (selectedIDs.Count > 0) ? selectedIDs : null;


            List<int> seleccionados = (List<int>)Session["CheckedIDs"];

            if (seleccionados == null || seleccionados.Count() == 0)
            {
                Page.Validators.Add(new ValidationError("GrupoInformeTecnicoRechazo", "Seleccione al menos una solicitud para cerrar"));
            }
            else
            {


                if (Page.IsValid)
                {


                    Requerimiento docCierre = new Requerimiento();

                    docCierre.tipoUnidEspacial = new ParametroGenerico(rbTipo.CONCESION_DE_ACUICULTURA);
                    docCierre.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION);

                    docCierre.flujoDocumental = new ParametroGenerico(Convert.ToInt32(FlujoDocumental.SelectedValue));
                    docCierre.tipoEntrada = new ParametroGenerico(Convert.ToInt32(TipoEntrada.SelectedValue));
                    docCierre.origen = new ParametroGenerico(Convert.ToInt32(Origen.SelectedValue));
                    docCierre.tipoDocumento = new ParametroGenerico(Convert.ToInt32(TipoDocumento.SelectedValue));
                    docCierre.numero = Convert.ToString(Numero.Text);
                    docCierre.fecha = Convert.ToDateTime(Fecha.Text);
                    docCierre.origen = new ParametroGenerico(Convert.ToInt32(Origen.SelectedValue));
                    docCierre.estadoFinal = new ParametroGenerico(Convert.ToInt32(Resultado.SelectedValue));

                    docCierre.ambitoTipo = new List<DocumentoAmbito>();
                    docCierre.ambitoTipo.Add(new DocumentoAmbito());

                    foreach (DocumentoAmbito doc in docCierre.ambitoTipo)
                    {
                        doc.ambito = new ParametroGenerico(rbPestana.INFORME_DAC);
                        doc.tipo = new ParametroGenerico(Convert.ToInt32(Tipo.SelectedValue));
                        doc.estadoResultadoResp = new ParametroGenerico(Convert.ToInt32(Resultado.SelectedValue));
                        doc.seccion = new ParametroGenerico(rbSeccion.INFORME_DAC);
                    }


                    if (ArchivoAdjunto.HasFile)
                    {

                        ArchivoBinario archivoBinario = new ArchivoBinario();

                        archivoBinario.nombreArchivo = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
                        archivoBinario.nombreFisico = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
                        archivoBinario.formato = ArchivoAdjunto.PostedFile.FileName.Substring(ArchivoAdjunto.PostedFile.FileName.LastIndexOf(".") + 1).ToLower(); ;
                        archivoBinario.tamano = ArchivoAdjunto.PostedFile.InputStream.Length;
                        archivoBinario.archivo = ArchivoAdjunto.PostedFile;

                        docCierre.archivoAdjunto = archivoBinario;
                    }


                    DocSolicitudCierre docSolicitudCierre = null;
                    docCierre.solCierreForzado = new List<DocSolicitudCierre>();

                    foreach (int itemSeleccionado in (List<int>)seleccionados)
                    {
                        docSolicitudCierre = new DocSolicitudCierre();
                        docSolicitudCierre.idSolConcesion = itemSeleccionado;
                        docCierre.solCierreForzado.Add(docSolicitudCierre);
                    }


                    bool var = cierreForzadoService.GuardarCierreForzado(docCierre, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                    if (var)
                    {

                        this.mensaje = "Cierre Forzado registrado con éxito. (Nº Informe Cierre " + docCierre.numero + ". SSP pendiente de ingreso)";
                        Server.Transfer("~/CierreForzado/administrarCierreForzadoRelocalizacionLey.aspx");

                    }
                    else
                    {
                        Page.Validators.Add(new ValidationError("GrupoInformeTecnicoRechazo", "Ha ocurrido un error al realizar  la acción solicitada"));
                    }

                }
            }
        }


        protected void GridVwHeaderChckbox_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {


            SolicitudConcesion solicitudFiltro;
            solicitudFiltro = (SolicitudConcesion)ViewState["FILTRO_CIERRE_FORZADO"];
            if (solicitudFiltro == null)
            {
                solicitudFiltro = new SolicitudConcesion();
            }


            //ANTES DE CAMBIAR DE PAGINA SE ACTUALIZACION EN SESSION LOS CHECK SELECIONADOS

            var selectedIDs = (Session["CheckedIDs"] != null) ? Session["CheckedIDs"] as List<int> : new List<int>();

            foreach (GridViewRow row in GridVwHeaderChckbox.Rows)
            {

                var selCheckBox = row.FindControl("chkEmp") as CheckBox;
                var rowOrgID = Convert.ToInt32(GridVwHeaderChckbox.DataKeys[row.RowIndex].Value);
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



            solicitudFiltro.pagina = e.NewPageIndex;
            ViewState["FILTRO_CIERRE_FORZADO"] = solicitudFiltro;

            GridVwHeaderChckbox.PageIndex = e.NewPageIndex;
            GridVwHeaderChckbox.DataBind();
            CargaGrilla();




            //DESPUES DE CAMBIAR DE PAGINA, SE SETEAN LOS VALORES EN LOS CHECK SEGUN LA INFORMACION QUE ESTE EN SESSION
            bool marcarTodos = true;
            foreach (GridViewRow row in GridVwHeaderChckbox.Rows)
            {
                var emailCheckBox = row.FindControl("chkEmp") as CheckBox;
                var rowOrgID = Convert.ToInt32(GridVwHeaderChckbox.DataKeys[row.RowIndex].Value);
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
                CheckBox ChkBoxHeader = (CheckBox)GridVwHeaderChckbox.HeaderRow.FindControl("chkboxSelectAll2");
                ChkBoxHeader.Checked = true;
            }

            Session["CheckedIDs"] = (selectedIDs.Count > 0) ? selectedIDs : null;



        }


        protected void Limpiar_Form_Click(object sender, EventArgs e)
        {
            TipoTramite.SelectedValue = "0";
            Region.SelectedValue = "0";
            Carga_Combobox("Provincia");
            Carga_Combobox("Comuna");

            NPert.Text = "";
            UpdatePanelInformeTecnicoCierre.Update();

            NumIdentificador.Text = "";
            UpdatePanelIdentificadorSolicitud.Update();

            Especie.SelectedValue = "0";
            UpdatePanelEspecie.Update();

            RCA.SelectedValue = "-1";
            UpdatePanelRCA.Update();

            TamanioConcesion.Text = "";
            UpdatePanelTamanio.Update();
        }


    }
}