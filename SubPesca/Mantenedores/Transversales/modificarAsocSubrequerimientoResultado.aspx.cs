using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.common;
using SubPesca.Solicitudes.Registrar;
using LogicaNegocio.cl.subpesca.rb.servicios.mantenedores;

namespace SubPesca.Mantenedores.Transversales
{
    public partial class modificarAsocSubrequerimientoResultado : System.Web.UI.Page
    {

        MantenedorTransversalService mantenedorTransversalService = new MantenedorTransversalService();

        ValidacionDocumentacionDA validacionDocumentacionDA = new ValidacionDocumentacionDA();
        RequerimientoDA requerimientoDA = new RequerimientoDA();
        TipoDA tipoDA = new TipoDA();
        PestanaDA pestanaDA = new PestanaDA();
        MantenedorDA mantenedorDA = new MantenedorDA();

        ValidacionDocumentacion validacionDocumentacion = null;

        int dbPestana = rbPestana.CERO;
        int dbSeccion = rbSeccion.CERO;

        String erroresSumary = "ValidationSummary" + Convert.ToString(rbSeccion.CERO);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["validacionDocumentacion"] = new ValidacionDocumentacion();

                PanelFormularioIngreso.Visible = true;
                UpdatePanelFormularioIngreso.Update();
                
                ObtencionParametros();
                InicializarCombobox();
                InicializarFormulario();
            }
        }

        /**
         * Método que despliega la validación documentación para que sea modificada.
         */
        private void InicializarFormulario()
        {

            ValidacionDocumentacion validacionDocumentacion = validacionDocumentacionDA.ObtenerValidacionDocumentacion(Convert.ToInt32(IdValidacionDocumentacion.Text));
            ViewState["validacionDocumentacionAux"] = (ValidacionDocumentacion)validacionDocumentacion;

            if (validacionDocumentacion != null)
            {
                FlujoDocumental.SelectedValue = Convert.ToString(validacionDocumentacion.flujoDocumental.id);
                FlujoDocumental_change(null, null);

            }

        }

        protected void FlujoDocumental_change(object sender, EventArgs e)
        {

            TipoSalida.Items.Clear();
            TipoSalida.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            TipoSalida.DataBind();

            TipoEntrada.Items.Clear();
            TipoEntrada.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            TipoEntrada.DataBind();

            //LimpiarPorFlujoDocumental();

            if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.ENTRADA)
            {
                PanelTipoEntrada.Visible = true;
                PanelTipoSalida.Visible = false;

            }
            else if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.SALIDA)
            {
                PanelTipoSalida.Visible = true;
                PanelTipoEntrada.Visible = false;

            }


            if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.SALIDA)
            {

                TipoSalida.Items.Clear();
                TipoSalida.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                ValidacionDocumentacion validacionDocumentacion = (ValidacionDocumentacion)ViewState["validacionDocumentacion"];
                validacionDocumentacion.ambito = new ParametroGenerico(Convert.ToInt32(dbPestana));
                validacionDocumentacion.seccion = new ParametroGenerico(Convert.ToInt32(dbSeccion));
                validacionDocumentacion.flujoDocumental = new ParametroGenerico(Convert.ToInt32(FlujoDocumental.SelectedValue));

                List<ValidacionDocumentacion> resp = validacionDocumentacionDA.ListarEntradaSalidaFiltro(validacionDocumentacion);

                if (resp != null)
                {
                    foreach (ValidacionDocumentacion item in resp)
                    {
                        TipoSalida.Items.Add(new ListItem(item.tipoIO.descripcion, Convert.ToString(item.tipoIO.id)));
                    }
                }

                TipoSalida.DataBind();

                ValidacionDocumentacion validacionDocumentacionAux = (ValidacionDocumentacion) ViewState["validacionDocumentacionAux"];
                TipoSalida.SelectedValue = Convert.ToString(validacionDocumentacionAux.tipoIO.id);
                TipoSalida_change(null,null);
            }


            if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.ENTRADA)
            {

                TipoEntrada.Items.Clear();
                TipoEntrada.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                ValidacionDocumentacion validacionDocumentacion = (ValidacionDocumentacion)ViewState["validacionDocumentacion"];
                validacionDocumentacion.ambito = new ParametroGenerico(Convert.ToInt32(dbPestana));
                validacionDocumentacion.seccion = new ParametroGenerico(Convert.ToInt32(dbSeccion));
                validacionDocumentacion.flujoDocumental = new ParametroGenerico(Convert.ToInt32(FlujoDocumental.SelectedValue));

                List<ValidacionDocumentacion> resp = validacionDocumentacionDA.ListarEntradaSalidaFiltro(validacionDocumentacion);

                if (resp != null)
                {
                    foreach (ValidacionDocumentacion item in resp)
                    {
                        TipoEntrada.Items.Add(new ListItem(item.tipoIO.descripcion, Convert.ToString(item.tipoIO.id)));
                    }
                }

                TipoEntrada.DataBind();

                ValidacionDocumentacion validacionDocumentacionAux = (ValidacionDocumentacion)ViewState["validacionDocumentacionAux"];
                TipoEntrada.SelectedValue = Convert.ToString(validacionDocumentacionAux.tipoIO.id);
                TipoEntrada_change(null,null);
            }

            UpdatePanelTipoSalida.Update();
            UpdatePanelTipoEntrada.Update();

        }


        private void InicializarCombobox()
        {
            Carga_Combobox("FlujoDocumental");
            FlujoDocumental.SelectedValue = "0";

            Carga_Combobox("TipoSalida");
            TipoSalida.SelectedValue = "0";

            Carga_Combobox("TipoEntrada");
            TipoEntrada.SelectedValue = "0";

            Carga_Combobox("Origen");
            Origen.SelectedValue = "0";

            Carga_Combobox("Ambito");
            Ambito.SelectedValue = "0";

            Carga_Combobox("Seccion");
            Seccion.SelectedValue = "0";

            Carga_Combobox("TipoDocumento");
            TipoDocumento.SelectedValue = "0";

            Carga_Combobox("Destinatario");
            Destinatario.SelectedValue = "0";

            Carga_Combobox("Tipo");
            Tipo.SelectedValue = "0";

            Carga_Combobox("AplicaNumero");
            AplicaNumero.SelectedValue = "0";

            Carga_Combobox("AplicaFecha");
            AplicaFecha.SelectedValue = "0";

            Carga_Combobox("AplicaNumeroCI");
            AplicaNumeroCI.SelectedValue = "0";

            Carga_Combobox("AplicaFechaCI");
            AplicaFechaCI.SelectedValue = "0";

            Carga_Combobox("AplicaArchivoBinario");
            AplicaArchivoBinario.SelectedValue = "0";
        }

        private void Carga_Combobox(string combobox)
        {
            switch (combobox)
            {

                case "FlujoDocumental":

                    // Cargamos el combobox: Flujo Documental
                    validacionDocumentacion = (ValidacionDocumentacion)ViewState["validacionDocumentacion"];
                    validacionDocumentacion.ambito = new ParametroGenerico(Convert.ToInt32(dbPestana));
                    validacionDocumentacion.seccion = new ParametroGenerico(Convert.ToInt32(dbSeccion));

                    FlujoDocumental.Items.Clear();

                    List<ValidacionDocumentacion> resp = validacionDocumentacionDA.ListarFlujoDocumentalFiltro(validacionDocumentacion);
                    if (resp != null)
                    {
                        foreach (ValidacionDocumentacion item in resp)
                        {
                            FlujoDocumental.Items.Add(new ListItem(item.flujoDocumental.descripcion, Convert.ToString(item.flujoDocumental.id)));
                        }
                    }

                    FlujoDocumental.DataBind();
                    FlujoDocumental.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                case "TipoEntrada":
                    // Cargamos el combobox: Tipo Salida
                    TipoEntrada.Items.Clear();
                    TipoEntrada.DataBind();
                    TipoEntrada.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                case "TipoSalida":
                    // Cargamos el combobox: Tipo Salida
                    TipoSalida.Items.Clear();
                    TipoSalida.DataBind();
                    TipoSalida.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;


                case "Origen":
                    // Cargamos el combobox: Tipo Salida
                    Origen.Items.Clear();
                    Origen.DataBind();
                    Origen.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                case "Seccion":
                    // Cargamos el combobox: Seccion
                    Seccion.Items.Clear();
                    Seccion.DataBind();
                    Seccion.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                case "Ambito":
                    // Cargamos el combobox: Ambito
                    Ambito.Items.Clear();
                    Ambito.DataBind();
                    Ambito.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                case "Tipo":
                    // Cargamos el combobox: Tipo
                    Tipo.Items.Clear();
                    Tipo.DataBind();
                    Tipo.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                case "TipoDocumento":
                    // Cargamos el combobox: Tipo Documento
                    TipoDocumento.Items.Clear();
                    TipoDocumento.DataBind();
                    TipoDocumento.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                case "Destinatario":
                    // Cargamos el combobox: Destinatario
                    Destinatario.Items.Clear();
                    Destinatario.DataBind();
                    Destinatario.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                    break;

                case "AplicaNumero":
                    AplicaNumero.Items.Clear();
                    AplicaNumero.DataBind();
                    AplicaNumero.Items.Insert(0, new ListItem("Campo No Aplica", "0"));
                    AplicaNumero.Items.Insert(0, new ListItem("Campo Aplica y es Obligatorio", "1"));
                    AplicaNumero.Items.Insert(0, new ListItem("Campo Aplica y es Opcional", "2"));
                    AplicaNumero.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

                case "AplicaFecha":
                    AplicaFecha.Items.Clear();
                    AplicaFecha.DataBind();
                    AplicaFecha.Items.Insert(0, new ListItem("Campo No Aplica", "0"));
                    AplicaFecha.Items.Insert(0, new ListItem("Campo Aplica y es Obligatorio", "1"));
                    AplicaFecha.Items.Insert(0, new ListItem("Campo Aplica y es Opcional", "2"));
                    AplicaFecha.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

                case "AplicaNumeroCI":
                    AplicaNumeroCI.Items.Clear();
                    AplicaNumeroCI.DataBind();
                    AplicaNumeroCI.Items.Insert(0, new ListItem("Campo No Aplica", "0"));
                    AplicaNumeroCI.Items.Insert(0, new ListItem("Campo Aplica y es Obligatorio", "1"));
                    AplicaNumeroCI.Items.Insert(0, new ListItem("Campo Aplica y es Opcional", "2"));
                    AplicaNumeroCI.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

                case "AplicaFechaCI":
                    AplicaFechaCI.Items.Clear();
                    AplicaFechaCI.Items.Clear();
                    AplicaFechaCI.DataBind();
                    AplicaFechaCI.Items.Insert(0, new ListItem("Campo No Aplica", "0"));
                    AplicaFechaCI.Items.Insert(0, new ListItem("Campo Aplica y es Obligatorio", "1"));
                    AplicaFechaCI.Items.Insert(0, new ListItem("Campo Aplica y es Opcional", "2"));
                    AplicaFechaCI.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

                case "AplicaArchivoBinario":
                    AplicaArchivoBinario.Items.Clear();
                    AplicaArchivoBinario.Items.Clear();
                    AplicaArchivoBinario.DataBind();
                    AplicaArchivoBinario.Items.Insert(0, new ListItem("Campo No Aplica", "0"));
                    AplicaArchivoBinario.Items.Insert(0, new ListItem("Campo Aplica y es Obligatorio", "1"));
                    AplicaArchivoBinario.Items.Insert(0, new ListItem("Campo Aplica y es Opcional", "2"));
                    AplicaArchivoBinario.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

            };
        }

        private void ObtencionParametros()
        {
            if (Request.QueryString["idValidacionDocumentacion"] != null)
            {
                IdValidacionDocumentacion.Text = Request.QueryString["idValidacionDocumentacion"];
            }
        }

        protected void Modificar_Click(object sender, EventArgs e)
        {

        }

        protected void TipoSalida_change(object sender, EventArgs e)
        {
            
            LimpiarPorTipoSalida();

            Destinatario.Items.Clear();
            Destinatario.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            if (Convert.ToInt32(TipoSalida.SelectedValue) > 0)
            {

                List<ParametroGenerico> listaTipoDestinatario = tipoDA.listarTipoDestinatario();

                if (listaTipoDestinatario != null)
                {
                    foreach (ParametroGenerico item in listaTipoDestinatario)
                    {
                        Destinatario.Items.Add(new ListItem(item.descripcion, Convert.ToString(item.id)));
                    }
                }
            }

            Destinatario.DataBind();

            ValidacionDocumentacion validacionDocumentacionAux = (ValidacionDocumentacion)ViewState["validacionDocumentacionAux"];
            Destinatario.SelectedValue = Convert.ToString(validacionDocumentacionAux.tipoDestinatario.id);
            Destinatario_Change(null,null);

            PanelDestinatario.Visible = true;
            UpdatePanelDestinatario.Update();
        }

        private void LimpiarPorTipoSalida()
        {
            TipoEntrada.SelectedValue = "0";
            Ambito.SelectedValue = "0";
            Tipo.SelectedValue = "0";
            TipoDocumento.SelectedValue = "0";
            Destinatario.SelectedValue = "0";
            Origen.SelectedValue = "0";
            Seccion.SelectedValue = "0";

            AplicaNumero.SelectedValue = "-1";
            AplicaFecha.SelectedValue = "-1";
            AplicaNumeroCI.SelectedValue = "-1";
            AplicaFechaCI.SelectedValue = "-1";
            AplicaArchivoBinario.SelectedValue = "-1";

            PanelTipoEntrada.Visible = false;
            PanelOrigen.Visible = false;
            PanelAmbito.Visible = false;
            PanelTipo.Visible = false;
            PanelTipoDocumento.Visible = false;
            PanelDestinatario.Visible = false;
            PanelAplica.Visible = false;
            PanelSeccion.Visible = false;

            ErroresSuperior.Text = "";
            PanelErroresSuperior.Visible = false;
            UpdatePanelErroresSuperior.Update();

            UpdatePanelTipoEntrada.Update();
            UpdatePanelOrigen.Update();
            UpdatePanelDestinatario.Update();
            UpdatePanelTipoDocumento.Update();
            UpdatePanelAmbito.Update();
            UpdatePanelTipo.Update();
            UpdatePanelAplica.Update();
            UpdatePanelSeccion.Update();
        }

        protected void TipoEntrada_change(object sender, EventArgs e)
        {
            LimpiarPorTipoEntrada();

            Origen.Items.Clear();
            Origen.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            if (Convert.ToInt32(TipoEntrada.SelectedValue) > 0)
            {

                List<ParametroGenerico> listaTipoDestinatario = tipoDA.listarTipoDestinatario();

                if (listaTipoDestinatario != null)
                {
                    foreach (ParametroGenerico item in listaTipoDestinatario)
                    {
                        Origen.Items.Add(new ListItem(item.descripcion, Convert.ToString(item.id)));
                    }
                }
            }

            Origen.DataBind();

            ValidacionDocumentacion validacionDocumentacionAux = (ValidacionDocumentacion)ViewState["validacionDocumentacionAux"];
            Origen.SelectedValue = Convert.ToString(validacionDocumentacionAux.tipoDestinatario.id);
            Origen_Change(null,null);

            PanelOrigen.Visible = true;
            UpdatePanelOrigen.Update();
        }

        private void LimpiarPorTipoEntrada()
        {
            TipoSalida.SelectedValue = "0";
            Ambito.SelectedValue = "0";
            Tipo.SelectedValue = "0";
            TipoDocumento.SelectedValue = "0";
            Destinatario.SelectedValue = "0";
            Origen.SelectedValue = "0";
            Seccion.SelectedValue = "0";

            AplicaNumero.SelectedValue = "-1";
            AplicaFecha.SelectedValue = "-1";
            AplicaNumeroCI.SelectedValue = "-1";
            AplicaFechaCI.SelectedValue = "-1";
            AplicaArchivoBinario.SelectedValue = "-1";

            PanelTipoSalida.Visible = false;
            PanelOrigen.Visible = false;
            PanelAmbito.Visible = false;
            PanelTipo.Visible = false;
            PanelTipoDocumento.Visible = false;
            PanelDestinatario.Visible = false;
            PanelAplica.Visible = false;
            PanelSeccion.Visible = false;

            ErroresSuperior.Text = "";
            PanelErroresSuperior.Visible = false;
            UpdatePanelErroresSuperior.Update();

            UpdatePanelTipoSalida.Update();
            UpdatePanelOrigen.Update();
            UpdatePanelDestinatario.Update();
            UpdatePanelTipoDocumento.Update();
            UpdatePanelAmbito.Update();
            UpdatePanelTipo.Update();
            UpdatePanelAplica.Update();
            UpdatePanelSeccion.Update();
        }

        protected void Origen_Change(object sender, EventArgs e)
        {
            Ambito.Items.Clear();
            Ambito.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            List<ParametroGenerico> listaPestania = pestanaDA.ListarPestania(0);
            if (listaPestania != null)
            {
                foreach (ParametroGenerico item in listaPestania)
                {
                    if (item.id > 0)
                    {
                        Ambito.Items.Add(new ListItem(item.descripcion, Convert.ToString(item.id)));
                    }
                }
            }

            Ambito.DataBind();
            
            ValidacionDocumentacion validacionDocumentacionAux = (ValidacionDocumentacion)ViewState["validacionDocumentacionAux"];
            Ambito.SelectedValue = Convert.ToString(validacionDocumentacionAux.ambito.id);
            Ambito_change(null,null);

            PanelAmbito.Visible = true;
            UpdatePanelAmbito.Update();
        }

        protected void Destinatario_Change(object sender, EventArgs e)
        {
            Ambito.Items.Clear();
            Ambito.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            List<ParametroGenerico> listaPestania = pestanaDA.ListarPestania(0);
            if (listaPestania != null)
            {
                foreach (ParametroGenerico item in listaPestania)
                {
                    if (item.id > 0)
                    {
                        Ambito.Items.Add(new ListItem(item.descripcion, Convert.ToString(item.id)));
                    }
                }
            }

            Ambito.DataBind();
            
            ValidacionDocumentacion validacionDocumentacionAux = (ValidacionDocumentacion)ViewState["validacionDocumentacionAux"];
            Ambito.SelectedValue = Convert.ToString(validacionDocumentacionAux.ambito.id);
            Ambito_change(null, null);

            PanelAmbito.Visible = true;
            UpdatePanelAmbito.Update();
        }

        protected void Ambito_change(object sender, EventArgs e)
        {
            Seccion.Items.Clear();
            Seccion.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            List<ParametroGenerico> listaSecciones = tipoDA.listarSecciones(0);

            if (listaSecciones != null)
            {
                foreach (ParametroGenerico item in listaSecciones)
                {
                    if (item.id > 0)
                    {
                        Seccion.Items.Add(new ListItem(item.descripcion, Convert.ToString(item.id)));
                    }
                }
            }

            ValidacionDocumentacion validacionDocumentacionAux = (ValidacionDocumentacion)ViewState["validacionDocumentacionAux"];
            Seccion.SelectedValue = Convert.ToString(validacionDocumentacionAux.seccion.id);
            Seccion_change(null,null);

            PanelSeccion.Visible = true;
            Seccion.DataBind();
            UpdatePanelSeccion.Update();
        }

        protected void Seccion_change(object sender, EventArgs e)
        {
            Tipo.Items.Clear();
            Tipo.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            List<SubRequerimiento> listaSubRequerimiento = requerimientoDA.ListarSubRequerimiento(new SubRequerimiento());

            if (listaSubRequerimiento != null)
            {
                foreach (SubRequerimiento item in listaSubRequerimiento)
                {
                    if (item.idSubRequerimiento > 0)
                    {
                        Tipo.Items.Add(new ListItem(item.nombreSubRequerimiento, Convert.ToString(item.idSubRequerimiento)));
                    }
                }
            }

            ValidacionDocumentacion validacionDocumentacionAux = (ValidacionDocumentacion)ViewState["validacionDocumentacionAux"];
            Tipo.SelectedValue = Convert.ToString(validacionDocumentacionAux.subRequerimiento.id);
            Tipo_change(null,null);

            PanelTipo.Visible = true;
            Tipo.DataBind();
            UpdatePanelTipo.Update();
        }

        protected void Tipo_change(object sender, EventArgs e)
        {
            this.controlarCamposPorTema();
        }

        private void controlarCamposPorTema()
        {
            ParametroGenerico parametroGenericoFiltro = new ParametroGenerico();
            parametroGenericoFiltro.clave = "TIPO_DOCUMENTO";

            TipoDocumento.Items.Clear();
            TipoDocumento.Items.Insert(0, new ListItem("-- Seleccione --", "0"));


            List<ParametroGenerico> listarTipo = mantenedorDA.ListarTipo_Mantenedor(parametroGenericoFiltro);

            if (listarTipo != null)
            {
                foreach (ParametroGenerico item in listarTipo)
                {
                    if (item.id > 0)
                    {
                        TipoDocumento.Items.Add(new ListItem(item.descripcion, Convert.ToString(item.id)));
                    }
                }
            }

            TipoDocumento.DataBind();

            ValidacionDocumentacion validacionDocumentacionAux = (ValidacionDocumentacion)ViewState["validacionDocumentacionAux"];
            TipoDocumento.SelectedValue = Convert.ToString(validacionDocumentacionAux.tipoDocumento.id);
            TipoDocumento_SelectedIndexChanged(null,null);

            PanelTipoDocumento.Visible = true;
            UpdatePanelTipoDocumento.Update();
        }

        protected void TipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            validacionDocumentacion = (ValidacionDocumentacion)ViewState["validacionDocumentacionAux"];

            AplicaNumero.SelectedValue = Convert.ToString(validacionDocumentacion.numero);
            AplicaFecha.SelectedValue = Convert.ToString(validacionDocumentacion.fecha);
            AplicaNumeroCI.SelectedValue = Convert.ToString(validacionDocumentacion.numeroCI);
            AplicaFechaCI.SelectedValue = Convert.ToString(validacionDocumentacion.fechaCI);
            AplicaArchivoBinario.SelectedValue = Convert.ToString(validacionDocumentacion.archivoBinario);

            VerificaConforme.Checked = validacionDocumentacion.verificaConforme;

            if (validacionDocumentacion.nuevaFecha > 0)
            {
                ExtensionPlazo.Checked = true;
            }

            if (validacionDocumentacion.aplicaConcesion > 0)
            {
                AplicaConcesion.Checked = true;
            }

            if (validacionDocumentacion.aplicaModAmpliacion > 0) 
            {
                AplicaModificacionAmpliacion.Checked = true;
            }

            if (validacionDocumentacion.aplicaModReduccion > 0) {
                AplicaModificacionReduccion.Checked = true;
            }

            if(validacionDocumentacion.aplicaModEspeciePT > 0){
                AplicaModificacionEspeciePT.Checked = true;
            }

            if(validacionDocumentacion.aplicaModReduccion > 0){
                AplicaModificacionRegularizacion.Checked = true;
            }

            if(validacionDocumentacion.aplicaRelocalizacion > 0){
                AplicaRelocalizacion.Checked = true;
            }

            if (validacionDocumentacion.aplicaAmerb > 0)
            {
                AplicaAmerb.Checked = true;
            }

            if (validacionDocumentacion.aplicaFaenamiento > 0)
            {
                AplicaFaenamiento.Checked = true;
            }

            if(validacionDocumentacion.aplicaAcopio > 0){
                AplicaAcopio.Checked = true;
            }

            if (validacionDocumentacion.aplicaColectores > 0)
            {
                AplicaColectores.Checked = true;
            }

            if (validacionDocumentacion.verificaAmpPlazo > 0)
            {
                VerificaAmpliacionPlazo.Checked = true;
            }

            if (validacionDocumentacion.verificaAmpExtension > 0) {
                VerficiaAmpliacionExtension.Checked = true;
            }

            PanelAplica.Visible = true;
            UpdatePanelAplica.Update();

        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            
            //Flujo Documental
            if (Convert.ToInt32(FlujoDocumental.SelectedValue) < 1)
            {
                Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Flujo Documental"));
            }


            if (Page.IsValid)
            {

                ValidacionDocumentacion validacionDocumentacion = new ValidacionDocumentacion();

                validacionDocumentacion.idValDocumentacion = Convert.ToInt32(IdValidacionDocumentacion.Text);

                validacionDocumentacion.flujoDocumental = new ParametroGenerico(Convert.ToInt32(FlujoDocumental.SelectedValue));


                if (validacionDocumentacion.flujoDocumental.id == rbTipo.ENTRADA)
                {

                    //Tipo entrada
                    if (Convert.ToInt32(TipoEntrada.SelectedValue) < 1)
                    {
                        Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Tipo de Entrada"));
                    }


                    if (Page.IsValid)
                    {
                        validacionDocumentacion.tipoIO = new ParametroGenerico(Convert.ToInt32(TipoEntrada.SelectedValue));
                    }


                    if (validacionDocumentacion.tipoIO != null && validacionDocumentacion.tipoIO.id == rbTipo.RESPUESTA_A_UN_REQUERIMIENTO)
                    {


                        List<String> errores = this.validarIngresoRequerimiento(rbTipo.RESPUESTA_A_UN_REQUERIMIENTO, validacionDocumentacion);

                        foreach (String error in errores)
                        {
                            Page.Validators.Add(new ValidationError(erroresSumary, error));
                        }
                    }



                    if (validacionDocumentacion.tipoIO != null && validacionDocumentacion.tipoIO.id == rbTipo.INGRESO_SIN_REQUERIMIENTO)
                    {
                        List<String> errores = this.validarIngresoRequerimiento(rbTipo.INGRESO_SIN_REQUERIMIENTO, validacionDocumentacion);

                        foreach (String error in errores)
                        {
                            Page.Validators.Add(new ValidationError(erroresSumary, error));
                        }
                    }

                }


                if (validacionDocumentacion.flujoDocumental.id == rbTipo.SALIDA)
                {
                    //Tipo salida
                    if (Convert.ToInt32(TipoSalida.SelectedValue) < 1)
                    {
                        Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Tipo de Salida"));
                    }

                    if (Page.IsValid)
                    {
                        validacionDocumentacion.tipoIO = new ParametroGenerico(Convert.ToInt32(TipoSalida.SelectedValue));
                    }



                    if (validacionDocumentacion.tipoIO != null && validacionDocumentacion.tipoIO.id == rbTipo.INFORMATIVO)
                    {
                        List<String> errores = this.validarIngresoRequerimiento(rbTipo.INFORMATIVO, validacionDocumentacion);

                        foreach (String error in errores)
                        {
                            Page.Validators.Add(new ValidationError(erroresSumary, error));
                        }

                    }



                    if (validacionDocumentacion.tipoIO != null && validacionDocumentacion.tipoIO.id == rbTipo.REQUERIMIENTO_CON_RESPUESTA)
                    {
                        List<String> errores = this.validarIngresoRequerimiento(rbTipo.REQUERIMIENTO_CON_RESPUESTA, validacionDocumentacion);

                        foreach (String error in errores)
                        {
                            Page.Validators.Add(new ValidationError(erroresSumary, error));
                        }

                    }
                }


                if (Page.IsValid)
                {

                    if (Convert.ToInt32(Destinatario.SelectedValue) > 0)
                    {
                        validacionDocumentacion.tipoDestinatario = new ParametroGenerico(Convert.ToInt32(Destinatario.SelectedValue));
                    }

                    if (Convert.ToInt32(Origen.SelectedValue) > 0)
                    {
                        validacionDocumentacion.tipoDestinatario = new ParametroGenerico(Convert.ToInt32(Origen.SelectedValue));
                    }

                    if (validacionDocumentacion.ambito == null)
                    {

                        //AMBITO
                        if (Convert.ToInt32(Ambito.SelectedValue) > 0)
                        {
                            validacionDocumentacion.ambito = new ParametroGenerico();
                            validacionDocumentacion.ambito.id = Convert.ToInt32(Ambito.SelectedValue);
                        }

                        //Sección
                        if (Convert.ToInt32(Seccion.SelectedValue) > 0)
                        {
                            validacionDocumentacion.seccion = new ParametroGenerico();
                            validacionDocumentacion.seccion.id = Convert.ToInt32(Seccion.SelectedValue);
                        }

                        //TIPO
                        if (Convert.ToInt32(Tipo.SelectedValue) > 0)
                        {

                            validacionDocumentacion.subRequerimiento = new ParametroGenerico();
                            validacionDocumentacion.subRequerimiento.id = Convert.ToInt32(Tipo.SelectedValue);
                        }

                        if (Convert.ToInt32(TipoDocumento.SelectedValue) > 0)
                        {
                            validacionDocumentacion.tipoDocumento = new ParametroGenerico();
                            validacionDocumentacion.tipoDocumento.id = Convert.ToInt32(TipoDocumento.SelectedValue);

                        }
                    }

                    if (Convert.ToInt32(AplicaNumero.SelectedValue) > 0)
                    {
                        validacionDocumentacion.numero = Convert.ToInt32(AplicaNumero.SelectedValue);
                    }
                    else
                    {
                        Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Aplica Número"));
                    }

                    if (Convert.ToInt32(AplicaFecha.SelectedValue) > 0)
                    {
                        validacionDocumentacion.fecha = Convert.ToInt32(AplicaFecha.SelectedValue);
                    }
                    else
                    {
                        Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Aplica Fecha"));
                    }

                    if (Convert.ToInt32(AplicaNumeroCI.SelectedValue) > 0)
                    {
                        validacionDocumentacion.numeroCI = Convert.ToInt32(AplicaNumeroCI.SelectedValue);
                    }
                    else
                    {
                        Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Aplica Número CI"));
                    }

                    if (Convert.ToInt32(AplicaFechaCI.SelectedValue) > 0)
                    {
                        validacionDocumentacion.fechaCI = Convert.ToInt32(AplicaFechaCI.SelectedValue);
                    }
                    else
                    {
                        Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Aplica Fecha CI"));
                    }

                    if (Convert.ToInt32(AplicaArchivoBinario.SelectedValue) > 0)
                    {
                        validacionDocumentacion.archivoBinario = Convert.ToInt32(AplicaArchivoBinario.SelectedValue);
                    }
                    else
                    {
                        Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Aplica Archivo Adjunto"));
                    }

                    if (VerificaConforme.Checked)
                    {
                        validacionDocumentacion.verificaConforme = true;
                    }

                    if (AplicaConcesion.Checked)
                    {
                        validacionDocumentacion.aplicaConcesion = 1;
                    }

                    if (AplicaModificacionAmpliacion.Checked)
                    {
                        validacionDocumentacion.aplicaModAmpliacion = 1;
                    }

                    if (AplicaModificacionEspeciePT.Checked)
                    {
                        validacionDocumentacion.aplicaModEspeciePT = 1;
                    }

                    if (AplicaModificacionRegularizacion.Checked)
                    {
                        validacionDocumentacion.aplicaModRegularizacion = 1;
                    }

                    if (AplicaModificacionRegularizacion.Checked)
                    {
                        validacionDocumentacion.aplicaModRegularizacion = 1;
                    }

                    if (AplicaModificacionRegularizacion.Checked)
                    {
                        validacionDocumentacion.aplicaModRegularizacion = 1;
                    }

                    if (AplicaAmerb.Checked)
                    {
                        validacionDocumentacion.aplicaAmerb = 1;
                    }

                    if (AplicaFaenamiento.Checked)
                    {
                        validacionDocumentacion.aplicaFaenamiento = 1;
                    }

                    if (AplicaAcopio.Checked)
                    {
                        validacionDocumentacion.aplicaAcopio = 1;
                    }

                    if (AplicaColectores.Checked)
                    {
                        validacionDocumentacion.aplicaColectores = 1;
                    }

                    if (VerificaAmpliacionPlazo.Checked)
                    {
                        validacionDocumentacion.verificaAmpPlazo = 1;
                    }

                    if (VerficiaAmpliacionExtension.Checked)
                    {
                        validacionDocumentacion.verificaAmpExtension = 1;
                    }

                    if (ExtensionPlazo.Checked)
                    {
                        validacionDocumentacion.nuevaFecha = 1;
                    }

                    /* Aquí se debe validar el ingreso de un subrequerimiento */
                    List<String> errores = new List<string>();

                    if (errores.Count == 0)
                    {
                        /* Aquí se debe modificar la asociación */
                        bool resp = mantenedorTransversalService.guardarAsociacionSubrequerimientoTipo(validacionDocumentacion);

                        if (resp)
                        {
                            ErroresInferior.Text = "Se ha modificado exitosamente la asociación subrequerimiento - tipo";

                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();

                        }
                        else
                        {
                            ErroresInferior.Text = "Ha ocurrido un error al modificado la asociación subrequerimiento - tipo";
                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();

                        }
                    }
                    else
                    {
                        foreach (String error in errores)
                        {
                            Page.Validators.Add(new ValidationError(erroresSumary, error));
                        }
                    }
                }
            }

            UpdatePanelMensajesValidaciones.Update();
        }

        private List<String> validarIngresoRequerimiento(int idTipoIO, ValidacionDocumentacion validacionDocumentacion)
        {

            List<String> errores = new List<string>();

            //CAMPOS OBLIGATORIOS

            //ORIGEN
            if (validacionDocumentacion.flujoDocumental.id == rbTipo.ENTRADA)
            {
                if (Convert.ToInt32(Origen.SelectedValue) < 1)
                {
                    errores.Add("Seleccione Origen");
                }
            }

            //DESTINATARIO
            if (validacionDocumentacion.flujoDocumental.id == rbTipo.SALIDA)
            {
                if (Convert.ToInt32(Destinatario.SelectedValue) < 1)
                {
                    errores.Add("Seleccione Destinatario");
                }
            }


            //TIPO DOCUMENTO
            if (Convert.ToInt32(TipoDocumento.SelectedValue) < 1)
            {
                errores.Add("Seleccione Tipo Documento");
            }



            //ENTRADA
            if (idTipoIO == rbTipo.INGRESO_SIN_REQUERIMIENTO)
            {
                //AMBITO 
                if (Convert.ToInt32(Ambito.SelectedValue) < 1)
                {
                    errores.Add("Seleccione Ámbito");
                }

                //TIPO 
                if (Convert.ToInt32(Tipo.SelectedValue) < 1)
                {
                    errores.Add("Seleccione Tema");
                }
            }

            //SALIDA
            if (idTipoIO == rbTipo.INFORMATIVO)
            {
                //AMBITO 
                if (Convert.ToInt32(Ambito.SelectedValue) < 1)
                {
                    errores.Add("Seleccione Ámbito");
                }

                //TIPO 
                if (Convert.ToInt32(Tipo.SelectedValue) < 1)
                {
                    errores.Add("Seleccione Tema");
                }
            }

            //SI HAY ERRORES EN ESTE PUNTO SE DEBEN RESOLVER ANTES DE SEGUIR CON LA VALIDACION
            if (errores.Count > 0)
            {
                return errores;
            }

            return errores;
        }

        protected void Volver_Click(object sender, EventArgs e)
        {
            String path = "~/Mantenedores/Transversales/asocSubrequerimientoTipo.aspx";
            Response.Redirect(path);
        }
    }
}