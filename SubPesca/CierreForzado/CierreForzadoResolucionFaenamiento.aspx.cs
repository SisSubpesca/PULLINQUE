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
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;

namespace SubPesca.CierreForzado
{
    public partial class CierreForzadoResolucionFaenamiento : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        RegionDA regionDA = new RegionDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        CierreForzadoService cierreForzadoService = new CierreForzadoService();
        RequerimientoService requerimientoService = new RequerimientoService();
        PestanaDA pestanaDA = new PestanaDA();
        TipoDA tipoDA = new TipoDA();
        int dbPestana = rbPestana.INFORME_DAC;
        int dbSeccion = rbSeccion.INFORME_DAC;


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


            if ((Request.Params["__EVENTTARGET"] != null) && (Request.Params["__EVENTARGUMENT"] != null))
            {
                if ((Request.Params["__EVENTTARGET"] == this.NumeroCI.ClientID) && (Request.Params["__EVENTARGUMENT"] == "onchange"))
                {
                    this.NumeroCI_TextChanged(null, null);
                }
            }


            // PAGE LOAD
            if (!Page.IsPostBack)
            {
                Initialize_Comboboxs();

                if (Request.QueryString["idDocCierre"] != null)
                {

                    Requerimiento informeCierre = cierreForzadoService.ObtieneInformeTecnicoCierre(Convert.ToInt32(Request.QueryString["idDocCierre"]));

                    if (informeCierre == null)
                    {
                        Response.Redirect("administrarCierreForzadoFaenamiento.aspx");
                    }

                    idInformeTecnico.Value = Convert.ToString(informeCierre.idRequerimiento); 
                    ViewState["informeCierre"] = informeCierre;
                    NumInforme.Text = Convert.ToString(informeCierre.numero);
                    CargaGrilla();

                }
            }

            string script = "calendario('" + Fecha.ClientID + "','" + fechaImgDinamica.ClientID + "');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + Fecha.ClientID, script.ToString(), true);

            script = "calendario('" + FechaCI.ClientID + "','" + fechaCIImgDinamica.ClientID + "');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptCalentario" + FechaCI.ClientID, script.ToString(), true);

        }

        protected void Initialize_Comboboxs()
        {


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
                    Origen.DataSource = tipoDA.obtenerTipoOrigenPorId(14);
                    Origen.DataTextField = "nombreTipoDestinatario";
                    Origen.DataValueField = "idTipoDestinatario";
                    Origen.DataBind();
                    Origen.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;


                case "Tipo":
                    // Cargamos el combobox: Tipo
                    Tipo.Items.Clear();
                    Tipo.Items.Add(new ListItem("Resolución SSP", Convert.ToString("132")));
                    Tipo.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                    Tipo.DataBind();
                    break;


                case "TipoDocumento":
                    // Cargamos el combobox: Tipo Documento
                    TipoDocumento.Items.Clear();
                    TipoDocumento.Items.Add(new ListItem("Resolución principal", Convert.ToString("69")));
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



        protected void CargaGrilla()
        {

            Requerimiento informeCierre = (Requerimiento)ViewState["informeCierre"];
            GridVwHeaderChckbox.DataSource = (List<SolicitudConcesion>)cierreForzadoService.ListarDocGralCierre_SolicitudesIT(informeCierre.idRequerimiento);
            GridVwHeaderChckbox.DataBind();

        }


        public void NumeroCI_TextChanged(object sender, EventArgs e)
        {

            try
            {


                if (!NumeroCI.Text.Trim().Equals("") && !FechaCI.Text.Trim().Equals(""))
                {

                    DateTime fechaAuxCI = Convert.ToDateTime(FechaCI.Text);
                    int anio = fechaAuxCI.Year;


                    List<SolicitudConcesion> solicitudes = requerimientoService.ObtenerSolicitudPorCIusado(0, Convert.ToInt32(NumeroCI.Text.Trim()), anio);
                    if (solicitudes != null && solicitudes.Count > 0)
                    {
                        String mensaje = "El Número C.I. ingresado esta presente en la(s) siguiente(s) solicitude(s): ";
                        foreach (SolicitudConcesion solAux in solicitudes)
                        {
                            mensaje = mensaje + "Pert: " + solAux.numPert + " - ";
                        }
                        NumeroCIMensaje.Text = mensaje;
                    }
                    else
                    {
                        NumeroCIMensaje.Text = "";
                    }
                }
                else
                {
                    NumeroCIMensaje.Text = "";
                }

            }
            catch (Exception)
            {
                NumeroCIMensaje.Text = "";
            }

        }



        public List<GridViewRow> GetSelectedDataKeysEnabled(GridView control, string checkBoxId)
        {

            List<GridViewRow> lista = new List<GridViewRow>();

            foreach (GridViewRow row in control.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("chkEmp");

                if (check.Enabled == true && check.Checked)
                {
                    lista.Add(row);
                }
            }

            return lista;
        }




        protected void GridVwHeaderChckbox_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                String tieneIT = ((HiddenField)e.Row.FindControl("HiddenTieneIT")).Value;
                String tieneSSP = ((HiddenField)e.Row.FindControl("HiddenTieneSSP")).Value;


                //YA TIENE SSP O NO TIENE INFORME TECNICO PORQUE SE BORRO EN LA SOLICITUD
                if (tieneIT.Equals("0") || tieneIT.Equals("False") || tieneSSP.Equals("1") || tieneSSP.Equals("True"))
                {
                    CheckBox ChkBoxRows = (CheckBox)e.Row.FindControl("chkEmp");
                    if (tieneSSP.Equals("1") || tieneSSP.Equals("True"))
                    {
                        ChkBoxRows.Checked = true;
                    }
                    ChkBoxRows.Enabled = false;

                }
            }

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

                if (ChkBoxRows.Enabled)
                {
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
        }

        protected void Guardar_Click(object sender, EventArgs e)
        {

            //Flujo Documental
            if (Convert.ToInt32(FlujoDocumental.SelectedValue) < 1)
            {
                Page.Validators.Add(new ValidationError("GrupoResolucionSSPRechazo", "Seleccione Flujo Documental"));
            }

            //Tipo entrada
            if (Convert.ToInt32(TipoEntrada.SelectedValue) < 1)
            {
                Page.Validators.Add(new ValidationError("GrupoResolucionSSPRechazo", "Seleccione Tipo de Entrada"));
            }

            //ORIGEN
            if (Convert.ToInt32(Origen.SelectedValue) < 1)
            {
                Page.Validators.Add(new ValidationError("GrupoResolucionSSPRechazo", "Seleccione Origen"));
            }

            //TIPO DOCUMENTO
            if (Convert.ToInt32(TipoDocumento.SelectedValue) < 1)
            {
                Page.Validators.Add(new ValidationError("GrupoResolucionSSPRechazo", "Seleccione Tipo Documento"));
            }

            //TIPO 
            if (Convert.ToInt32(Tipo.SelectedValue) < 1)
            {
                Page.Validators.Add(new ValidationError("GrupoResolucionSSPRechazo", "Seleccione Tema"));
            }

            //NUMERO
            if (Numero.Text.Trim().Equals(""))
            {
                Page.Validators.Add(new ValidationError("GrupoResolucionSSPRechazo", "Ingrese Número"));
            }

            //FECHA
            if (Fecha.Text.Trim().Equals(""))
            {
                Page.Validators.Add(new ValidationError("GrupoResolucionSSPRechazo", "Ingrese Fecha"));
            }


            /*
            //NUMERO C.I.
            if (NumeroCI.Text.Trim().Equals(""))
            {
                Page.Validators.Add(new ValidationError("GrupoResolucionSSPRechazo", "Ingrese Número C.I."));
            }

            //FECHA F.I.
            if (FechaCI.Text.Trim().Equals(""))
            {
                Page.Validators.Add(new ValidationError("GrupoResolucionSSPRechazo", "Ingrese Fecha C.I."));
            }
             * 
             **/

            //ARCHIVO
            if (!ArchivoAdjunto.HasFile)
            {
                Page.Validators.Add(new ValidationError("GrupoResolucionSSPRechazo", "Seleccione Archivo"));
            }

            //RESULTADO
            if (Convert.ToInt32(Resultado.SelectedValue) < 1)
            {
                Page.Validators.Add(new ValidationError("GrupoResolucionSSPRechazo", "Seleccione Resultado"));
            }




            //SE ACTUALIZACION EN SESSION LOS CHECK SELECIONADOS EN LA PAGINA ACTUAL
            var selectedIDs = (Session["CheckedIDs"] != null) ? Session["CheckedIDs"] as List<int> : new List<int>();

            foreach (GridViewRow row in GridVwHeaderChckbox.Rows)
            {

                var selCheckBox = row.FindControl("chkEmp") as CheckBox;
                
                if (selCheckBox.Enabled == true)
                {
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
            }
            Session["CheckedIDs"] = (selectedIDs.Count > 0) ? selectedIDs : null;


            List<int> seleccionados = (List<int>)Session["CheckedIDs"];

            if (seleccionados == null || seleccionados.Count() == 0)
            {
                Page.Validators.Add(new ValidationError("GrupoResolucionSSPRechazo", "Seleccione al menos una solicitud para incluir Resolución SSP"));
            }
            else
            {


                if (Page.IsValid)
                {


                    Requerimiento resolucionCierre = new Requerimiento();

                    resolucionCierre.tipoUnidEspacial = new ParametroGenerico(rbTipo.CENTRO_DE_FAENAMIENTO);

                    resolucionCierre.flujoDocumental = new ParametroGenerico(Convert.ToInt32(FlujoDocumental.SelectedValue));
                    resolucionCierre.tipoEntrada = new ParametroGenerico(Convert.ToInt32(TipoEntrada.SelectedValue));
                    resolucionCierre.origen = new ParametroGenerico(Convert.ToInt32(Origen.SelectedValue));
                    resolucionCierre.tipoDocumento = new ParametroGenerico(Convert.ToInt32(TipoDocumento.SelectedValue));
                    resolucionCierre.numero = Convert.ToString(Numero.Text);
                    resolucionCierre.fecha = Convert.ToDateTime(Fecha.Text);
                    //resolucionCierre.numeroCI = Convert.ToInt32(NumeroCI.Text);
                    //resolucionCierre.fechaCI = Convert.ToDateTime(FechaCI.Text);
                    resolucionCierre.estadoFinal = new ParametroGenerico(Convert.ToInt32(Resultado.SelectedValue));

                    resolucionCierre.ambitoTipo = new List<DocumentoAmbito>();
                    resolucionCierre.ambitoTipo.Add(new DocumentoAmbito());

                    foreach (DocumentoAmbito doc in resolucionCierre.ambitoTipo)
                    {
                        doc.ambito = new ParametroGenerico(rbPestana.RESOLUCION_SSP);
                        doc.tipo = new ParametroGenerico(Convert.ToInt32(Tipo.SelectedValue));
                        doc.estadoResultadoResp = new ParametroGenerico(Convert.ToInt32(Resultado.SelectedValue));
                        doc.seccion = new ParametroGenerico(rbSeccion.RESOLUCION_SSP);
                    }


                    if (ArchivoAdjunto.HasFile)
                    {

                        ArchivoBinario archivoBinario = new ArchivoBinario();

                        archivoBinario.nombreArchivo = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
                        archivoBinario.nombreFisico = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
                        archivoBinario.formato = ArchivoAdjunto.PostedFile.FileName.Substring(ArchivoAdjunto.PostedFile.FileName.LastIndexOf(".") + 1).ToLower(); ;
                        archivoBinario.tamano = ArchivoAdjunto.PostedFile.InputStream.Length;
                        archivoBinario.archivo = ArchivoAdjunto.PostedFile;

                        resolucionCierre.archivoAdjunto = archivoBinario;
                    }


                    DocSolicitudCierre docSolicitudCierre = null;
                    resolucionCierre.solCierreForzado = new List<DocSolicitudCierre>();

                    foreach (int itemSeleccionado in (List<int>)seleccionados)
                    {
                        docSolicitudCierre = new DocSolicitudCierre();
                        docSolicitudCierre.idSolConcesion = itemSeleccionado;
                        docSolicitudCierre.docITCierre = new Requerimiento();
                        docSolicitudCierre.docITCierre.idRequerimiento = Convert.ToInt32(((HiddenField)idInformeTecnico).Value);
                        resolucionCierre.solCierreForzado.Add(docSolicitudCierre);
                    }


                    bool var = cierreForzadoService.GuardarResolucionSSPCierreForzado(resolucionCierre, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                    if (var)
                    {

                        this.mensaje = "Resolución SSP registrada con éxito. (Resolución SSP " + resolucionCierre.numero + ".)";
                        Server.Transfer("~/CierreForzado/administrarCierreForzadoFaenamiento.aspx");

                    }
                    else
                    {
                        Page.Validators.Add(new ValidationError("GrupoResolucionSSPRechazo", "Ha ocurrido un error al realizar  la acción solicitada"));
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

                if (selCheckBox.Enabled == true)
                {

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
                var filaCheckBox = row.FindControl("chkEmp") as CheckBox;

                if (filaCheckBox.Enabled == true)
                {

                    var rowOrgID = Convert.ToInt32(GridVwHeaderChckbox.DataKeys[row.RowIndex].Value);
                    if (selectedIDs.Contains(rowOrgID))
                    {
                        filaCheckBox.Checked = true;
                    }
                    else
                    {
                        marcarTodos = false;
                    }
                }
            }

            if (marcarTodos)
            {
                CheckBox ChkBoxHeader = (CheckBox)GridVwHeaderChckbox.HeaderRow.FindControl("chkboxSelectAll2");
                ChkBoxHeader.Checked = true;
            }

            Session["CheckedIDs"] = (selectedIDs.Count > 0) ? selectedIDs : null;



        }


       

    }
}