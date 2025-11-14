using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Contantes;
using System.Collections;
using SubPesca.Solicitudes.Registrar;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.servicios.mantenedores;
using System.Data;

namespace SubPesca.Mantenedores.Transversales
{
    public partial class asocDocumentoTipo : System.Web.UI.Page
    {

        ValidacionDocumentacionDA validacionDocumentacionDA = new ValidacionDocumentacionDA();
        MantenedorDA mantenedorDA = new MantenedorDA();
        TipoDA tipoDA = new TipoDA();
        PestanaDA pestanaDA = new PestanaDA();
        RequerimientoDA requerimientoDA = new RequerimientoDA();
        

        MantenedorTransversalService mantenedorTransversalService = new MantenedorTransversalService();

        int dbPestana = rbPestana.CERO;
        int dbSeccion = rbSeccion.CERO;

        String erroresSumary = "ValidationSummary" + Convert.ToString(rbSeccion.CERO);

        protected void Page_Load(object sender, EventArgs e)
        {
            // PAGE LOAD
            if (!Page.IsPostBack)
            {

                ViewState["validacionDocumentacion"] = new ValidacionDocumentacion();
                //ViewState["HashCampos"] = validacionDocumentacionDA.ListaValidacionDocGeneral((ValidacionDocumentacion)ViewState["validacionDocumentacion"]);

                PanelFormularioIngreso.Visible = true;
                UpdatePanelFormularioIngreso.Update();

                // Inicializamos el formulario
                Initialize_Form();

                cargarGrillaValidacionDocumentacion();

            }
        }

        protected void Initialize_Form()
        {
            // Cargamos los combobox
            Initialize_Comboboxs();

        }
        
        protected void Initialize_Comboboxs()
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

            Carga_Combobox("VerificaConforme");
            VerificaConforme.SelectedValue = "-1";

            Carga_Combobox("ExtensionPlazo");
            ExtensionPlazo.SelectedValue = "-1";

            Carga_Combobox("AplicaConcesion");
            AplicaConcesion.SelectedValue = "-1";

            Carga_Combobox("AplicaModificacionAmpliacion");
            AplicaModificacionAmpliacion.SelectedValue = "-1";

            Carga_Combobox("AplicaModificacionReduccion");
            AplicaModificacionReduccion.SelectedValue = "-1";

            Carga_Combobox("AplicaModificacionEspeciePT");
            AplicaModificacionEspeciePT.SelectedValue = "-1";

            Carga_Combobox("AplicaModificacionRegularizacion");
            AplicaModificacionRegularizacion.SelectedValue = "-1";

            Carga_Combobox("AplicaRelocalizacion");
            AplicaRelocalizacion.SelectedValue = "-1";

            Carga_Combobox("AplicaAmerb");
            AplicaAmerb.SelectedValue = "-1";

            Carga_Combobox("AplicaFaenamiento");
            AplicaFaenamiento.SelectedValue = "-1";

            Carga_Combobox("AplicaAcopio");
            AplicaAcopio.SelectedValue = "-1";

            Carga_Combobox("AplicaColectores");
            AplicaColectores.SelectedValue = "-1";

            Carga_Combobox("VerificaAmpliacionPlazo");
            VerificaAmpliacionPlazo.SelectedValue = "-1";

            Carga_Combobox("VerficiaAmpliacionExtension");
            VerficiaAmpliacionExtension.SelectedValue = "-1";

            Carga_Combobox("AplicaExpAmerb");
            AplicaExpAmerb.SelectedValue = "-1";

            Carga_Combobox("AplicaExpConcesion");
            AplicaExpConcesion.SelectedValue = "-1";

            Carga_Combobox("AplicaECMPO");
            AplicaECMPO.SelectedValue = "-1";

            Carga_Combobox("AplicaModECMPOAmpliacion");
            AplicaModECMPOAmpliacion.SelectedValue = "-1";

            Carga_Combobox("AplicaModECMPOReduccion");
            AplicaModECMPOReduccion.SelectedValue = "-1";

            Carga_Combobox("AplicaModECMPOEspecie");
            AplicaModECMPOEspecie.SelectedValue = "-1";

            Carga_Combobox("AplicaModECMPO_PT");
            AplicaModECMPO_PT.SelectedValue = "-1";

            Carga_Combobox("AplicaModECMPORegularizacion");
            AplicaModECMPORegularizacion.SelectedValue = "-1";

            Carga_Combobox("AplicaModAcopioAmpliacion");
            AplicaModAcopioAmpliacion.SelectedValue = "-1";

            Carga_Combobox("AplicaModAcopioReduccion");
            AplicaModAcopioReduccion.SelectedValue = "-1";

            Carga_Combobox("AplicaModAcopioEspecie");
            AplicaModAcopioEspecie.SelectedValue = "-1";

            Carga_Combobox("AplicaModAcopioPT");
            AplicaModAcopioPT.SelectedValue = "-1";

            Carga_Combobox("AplicaModAcopioRegularizacion");
            AplicaModAcopioRegularizacion.SelectedValue = "-1";

            Carga_Combobox("AplicaModFaenamientoAmpliacion");
            AplicaModFaenamientoAmpliacion.SelectedValue = "-1";

            Carga_Combobox("AplicaModFaenamientoReduccion");
            AplicaModFaenamientoReduccion.SelectedValue = "-1";

            Carga_Combobox("AplicaModFaenamientoEspecie");
            AplicaModFaenamientoEspecie.SelectedValue = "-1";

            Carga_Combobox("AplicaModFaenamientoPT");
            AplicaModFaenamientoPT.SelectedValue = "-1";

            Carga_Combobox("AplicaModFaenamientoRegularizacion");
            AplicaModFaenamientoRegularizacion.SelectedValue = "-1";

            Carga_Combobox("AplicaModAmerbAmpliacion");
            AplicaModAmerbAmpliacion.SelectedValue = "-1";

            Carga_Combobox("AplicaModAmerbReduccion");
            AplicaModAmerbReduccion.SelectedValue = "-1";

            Carga_Combobox("AplicaModAmerbEspecie");
            AplicaModAmerbEspecie.SelectedValue = "-1";

            Carga_Combobox("AplicaModAmerbPT");
            AplicaModAmerbPT.SelectedValue = "-1";

            Carga_Combobox("AplicaModAmerbRegularizacion");
            AplicaModAmerbRegularizacion.SelectedValue = "-1";
        }

        protected void Carga_Combobox(string combobox)
        {

            switch (combobox)
            {

                case "FlujoDocumental":

                    // Cargamos el combobox: Flujo Documental
                    ValidacionDocumentacion validacionDocumentacion = (ValidacionDocumentacion)ViewState["validacionDocumentacion"];
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

                case "VerificaConforme":

                    // Cargamos el combobox: VerificaConforme
                    VerificaConforme.Items.Clear();
                    VerificaConforme.DataBind();
                    VerificaConforme.Items.Insert(0, new ListItem("Si", "1"));
                    VerificaConforme.Items.Insert(0, new ListItem("No", "0"));
                    VerificaConforme.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "ExtensionPlazo":

                    // Cargamos el combobox: ExtensionPlazo
                    ExtensionPlazo.Items.Clear();
                    ExtensionPlazo.DataBind();
                    ExtensionPlazo.Items.Insert(0, new ListItem("Si", "1"));
                    ExtensionPlazo.Items.Insert(0, new ListItem("No", "0"));
                    ExtensionPlazo.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaConcesion":

                    // Cargamos el combobox: AplicaConcesion
                    AplicaConcesion.Items.Clear();
                    AplicaConcesion.DataBind();
                    AplicaConcesion.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaConcesion.Items.Insert(0, new ListItem("No", "0"));
                    AplicaConcesion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModificacionAmpliacion":

                    // Cargamos el combobox: AplicaModificacionAmpliacion
                    AplicaModificacionAmpliacion.Items.Clear();
                    AplicaModificacionAmpliacion.DataBind();
                    AplicaModificacionAmpliacion.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModificacionAmpliacion.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModificacionAmpliacion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModificacionReduccion":

                    // Cargamos el combobox: AplicaModificacionReduccion
                    AplicaModificacionReduccion.Items.Clear();
                    AplicaModificacionReduccion.DataBind();
                    AplicaModificacionReduccion.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModificacionReduccion.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModificacionReduccion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModificacionEspeciePT":

                    // Cargamos el combobox: AplicaModificacionEspeciePT
                    AplicaModificacionEspeciePT.Items.Clear();
                    AplicaModificacionEspeciePT.DataBind();
                    AplicaModificacionEspeciePT.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModificacionEspeciePT.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModificacionEspeciePT.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModificacionRegularizacion":

                    // Cargamos el combobox: AplicaModificacionRegularizacion
                    AplicaModificacionRegularizacion.Items.Clear();
                    AplicaModificacionRegularizacion.DataBind();
                    AplicaModificacionRegularizacion.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModificacionRegularizacion.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModificacionRegularizacion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaRelocalizacion":

                    // Cargamos el combobox: AplicaRelocalizacion
                    AplicaRelocalizacion.Items.Clear();
                    AplicaRelocalizacion.DataBind();
                    AplicaRelocalizacion.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaRelocalizacion.Items.Insert(0, new ListItem("No", "0"));
                    AplicaRelocalizacion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaAmerb":

                    // Cargamos el combobox: AplicaAmerb
                    AplicaAmerb.Items.Clear();
                    AplicaAmerb.DataBind();
                    AplicaAmerb.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaAmerb.Items.Insert(0, new ListItem("No", "0"));
                    AplicaAmerb.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaFaenamiento":

                    // Cargamos el combobox: AplicaFaenamiento
                    AplicaFaenamiento.Items.Clear();
                    AplicaFaenamiento.DataBind();
                    AplicaFaenamiento.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaFaenamiento.Items.Insert(0, new ListItem("No", "0"));
                    AplicaFaenamiento.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaAcopio":

                    // Cargamos el combobox: AplicaAcopio
                    AplicaAcopio.Items.Clear();
                    AplicaAcopio.DataBind();
                    AplicaAcopio.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaAcopio.Items.Insert(0, new ListItem("No", "0"));
                    AplicaAcopio.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaColectores":

                    // Cargamos el combobox: AplicaColectores
                    AplicaColectores.Items.Clear();
                    AplicaColectores.DataBind();
                    AplicaColectores.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaColectores.Items.Insert(0, new ListItem("No", "0"));
                    AplicaColectores.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "VerificaAmpliacionPlazo":

                    // Cargamos el combobox: VerificaAmpliacionPlazo
                    VerificaAmpliacionPlazo.Items.Clear();
                    VerificaAmpliacionPlazo.DataBind();
                    VerificaAmpliacionPlazo.Items.Insert(0, new ListItem("Si", "1"));
                    VerificaAmpliacionPlazo.Items.Insert(0, new ListItem("No", "0"));
                    VerificaAmpliacionPlazo.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "VerficiaAmpliacionExtension":

                    // Cargamos el combobox: VerficiaAmpliacionExtension
                    VerficiaAmpliacionExtension.Items.Clear();
                    VerficiaAmpliacionExtension.DataBind();
                    VerficiaAmpliacionExtension.Items.Insert(0, new ListItem("Si", "1"));
                    VerficiaAmpliacionExtension.Items.Insert(0, new ListItem("No", "0"));
                    VerficiaAmpliacionExtension.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaExpAmerb":

                    // Cargamos el combobox: AplicaExpAmerb
                    AplicaExpAmerb.Items.Clear();
                    AplicaExpAmerb.DataBind();
                    AplicaExpAmerb.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaExpAmerb.Items.Insert(0, new ListItem("No", "0"));
                    AplicaExpAmerb.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaExpConcesion":

                    // Cargamos el combobox: AplicaExpConcesion
                    AplicaExpConcesion.Items.Clear();
                    AplicaExpConcesion.DataBind();
                    AplicaExpConcesion.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaExpConcesion.Items.Insert(0, new ListItem("No", "0"));
                    AplicaExpConcesion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaECMPO":

                    // Cargamos el combobox: AplicaECMPO
                    AplicaECMPO.Items.Clear();
                    AplicaECMPO.DataBind();
                    AplicaECMPO.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaECMPO.Items.Insert(0, new ListItem("No", "0"));
                    AplicaECMPO.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModECMPOAmpliacion":

                    // Cargamos el combobox: AplicaModECMPOAmpliacion
                    AplicaModECMPOAmpliacion.Items.Clear();
                    AplicaModECMPOAmpliacion.DataBind();
                    AplicaModECMPOAmpliacion.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModECMPOAmpliacion.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModECMPOAmpliacion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModECMPOReduccion":

                    // Cargamos el combobox: AplicaModECMPOReduccion
                    AplicaModECMPOReduccion.Items.Clear();
                    AplicaModECMPOReduccion.DataBind();
                    AplicaModECMPOReduccion.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModECMPOReduccion.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModECMPOReduccion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModECMPOEspecie":

                    // Cargamos el combobox: AplicaModECMPOEspecie
                    AplicaModECMPOEspecie.Items.Clear();
                    AplicaModECMPOEspecie.DataBind();
                    AplicaModECMPOEspecie.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModECMPOEspecie.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModECMPOEspecie.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModECMPO_PT":

                    // Cargamos el combobox: AplicaModECMPO_PT
                    AplicaModECMPO_PT.Items.Clear();
                    AplicaModECMPO_PT.DataBind();
                    AplicaModECMPO_PT.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModECMPO_PT.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModECMPO_PT.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModECMPORegularizacion":

                    // Cargamos el combobox: AplicaModECMPORegularizacion
                    AplicaModECMPORegularizacion.Items.Clear();
                    AplicaModECMPORegularizacion.DataBind();
                    AplicaModECMPORegularizacion.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModECMPORegularizacion.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModECMPORegularizacion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModAcopioAmpliacion":

                    // Cargamos el combobox: AplicaModAcopioAmpliacion
                    AplicaModAcopioAmpliacion.Items.Clear();
                    AplicaModAcopioAmpliacion.DataBind();
                    AplicaModAcopioAmpliacion.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModAcopioAmpliacion.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModAcopioAmpliacion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModAcopioReduccion":

                    // Cargamos el combobox: AplicaModAcopioReduccion
                    AplicaModAcopioReduccion.Items.Clear();
                    AplicaModAcopioReduccion.DataBind();
                    AplicaModAcopioReduccion.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModAcopioReduccion.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModAcopioReduccion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModAcopioEspecie":

                    // Cargamos el combobox: AplicaModAcopioEspecie
                    AplicaModAcopioEspecie.Items.Clear();
                    AplicaModAcopioEspecie.DataBind();
                    AplicaModAcopioEspecie.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModAcopioEspecie.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModAcopioEspecie.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModAcopioPT":

                    // Cargamos el combobox: AplicaModAcopioPT
                    AplicaModAcopioPT.Items.Clear();
                    AplicaModAcopioPT.DataBind();
                    AplicaModAcopioPT.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModAcopioPT.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModAcopioPT.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModAcopioRegularizacion":

                    // Cargamos el combobox: AplicaModAcopioRegularizacion
                    AplicaModAcopioRegularizacion.Items.Clear();
                    AplicaModAcopioRegularizacion.DataBind();
                    AplicaModAcopioRegularizacion.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModAcopioRegularizacion.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModAcopioRegularizacion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModFaenamientoAmpliacion":

                    // Cargamos el combobox: AplicaModFaenamientoAmpliacion
                    AplicaModFaenamientoAmpliacion.Items.Clear();
                    AplicaModFaenamientoAmpliacion.DataBind();
                    AplicaModFaenamientoAmpliacion.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModFaenamientoAmpliacion.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModFaenamientoAmpliacion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModFaenamientoReduccion":

                    // Cargamos el combobox: AplicaModFaenamientoReduccion
                    AplicaModFaenamientoReduccion.Items.Clear();
                    AplicaModFaenamientoReduccion.DataBind();
                    AplicaModFaenamientoReduccion.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModFaenamientoReduccion.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModFaenamientoReduccion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModFaenamientoEspecie":

                    // Cargamos el combobox: AplicaModFaenamientoEspecie
                    AplicaModFaenamientoEspecie.Items.Clear();
                    AplicaModFaenamientoEspecie.DataBind();
                    AplicaModFaenamientoEspecie.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModFaenamientoEspecie.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModFaenamientoEspecie.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModFaenamientoPT":

                    // Cargamos el combobox: AplicaModFaenamientoPT
                    AplicaModFaenamientoPT.Items.Clear();
                    AplicaModFaenamientoPT.DataBind();
                    AplicaModFaenamientoPT.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModFaenamientoPT.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModFaenamientoPT.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModFaenamientoRegularizacion":

                    // Cargamos el combobox: AplicaModFaenamientoRegularizacion
                    AplicaModFaenamientoRegularizacion.Items.Clear();
                    AplicaModFaenamientoRegularizacion.DataBind();
                    AplicaModFaenamientoRegularizacion.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModFaenamientoRegularizacion.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModFaenamientoRegularizacion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModAmerbAmpliacion":

                    // Cargamos el combobox: AplicaModAmerbAmpliacion
                    AplicaModAmerbAmpliacion.Items.Clear();
                    AplicaModAmerbAmpliacion.DataBind();
                    AplicaModAmerbAmpliacion.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModAmerbAmpliacion.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModAmerbAmpliacion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModAmerbReduccion":

                    // Cargamos el combobox: AplicaModAmerbReduccion
                    AplicaModAmerbReduccion.Items.Clear();
                    AplicaModAmerbReduccion.DataBind();
                    AplicaModAmerbReduccion.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModAmerbReduccion.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModAmerbReduccion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModAmerbEspecie":

                    // Cargamos el combobox: AplicaModAmerbEspecie
                    AplicaModAmerbEspecie.Items.Clear();
                    AplicaModAmerbEspecie.DataBind();
                    AplicaModAmerbEspecie.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModAmerbEspecie.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModAmerbEspecie.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModAmerbPT":

                    // Cargamos el combobox: AplicaModAmerbPT
                    AplicaModAmerbPT.Items.Clear();
                    AplicaModAmerbPT.DataBind();
                    AplicaModAmerbPT.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModAmerbPT.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModAmerbPT.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "AplicaModAmerbRegularizacion":

                    // Cargamos el combobox: AplicaModAmerbRegularizacion
                    AplicaModAmerbRegularizacion.Items.Clear();
                    AplicaModAmerbRegularizacion.DataBind();
                    AplicaModAmerbRegularizacion.Items.Insert(0, new ListItem("Si", "1"));
                    AplicaModAmerbRegularizacion.Items.Insert(0, new ListItem("No", "0"));
                    AplicaModAmerbRegularizacion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
            };
        }

        //1 = entrada
        //2 = salida
        protected void FlujoDocumental_change(object sender, EventArgs e)
        {

            TipoSalida.Items.Clear();
            TipoSalida.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            TipoSalida.DataBind();

            TipoEntrada.Items.Clear();
            TipoEntrada.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            TipoEntrada.DataBind();
            
            LimpiarPorFlujoDocumental();

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
            }



            UpdatePanelTipoSalida.Update();
            UpdatePanelTipoEntrada.Update();

        }

        protected void LimpiarPorFlujoDocumental()
        {

            TipoEntrada.SelectedValue = "0";
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
            
            PanelTipoEntrada.Visible = false;
            PanelTipoSalida.Visible = false;
            PanelOrigen.Visible = false;
            PanelAmbito.Visible = false;
            PanelTipo.Visible = false;
            PanelTipoDocumento.Visible = false;
            PanelDestinatario.Visible = false;
            PanelAplica.Visible = false;
            PanelSeccion.Visible = false;

            UpdatePanelTipoSalida.Update();
            UpdatePanelTipoEntrada.Update();
            UpdatePanelOrigen.Update();
            UpdatePanelDestinatario.Update();
            UpdatePanelTipoDocumento.Update();
            UpdatePanelAmbito.Update();
            UpdatePanelTipo.Update();
            UpdatePanelAplica.Update();
            UpdatePanelSeccion.Update();

            PanelErroresInferior.Visible = false;
            UpdatePanelErroresInferior.Update();
            
        }

        //3 = Requerimiento con Respuesta
        //4 = Informativo
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
            
            PanelDestinatario.Visible = true;
            UpdatePanelDestinatario.Update();
            
        }

        protected void LimpiarPorTipoSalida()
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

        //5 = Respuesta a un Requerimiento
        //6 = Ingreso sin Requerimiento
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

            PanelOrigen.Visible = true;
            UpdatePanelOrigen.Update();
            
        }

        protected void LimpiarPorTipoEntrada()
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
            

            if (Convert.ToInt32(Origen.SelectedValue) > 0)
            {
                Ambito.DataSource = pestanaDA.ListarPestania(0);
                Ambito.DataTextField = "descripcion";
                Ambito.DataValueField = "id";
                
            }

            Ambito.DataBind();
            Ambito.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            PanelAmbito.Visible = true;
            UpdatePanelAmbito.Update();
        }

        protected void Destinatario_Change(object sender, EventArgs e)
        {
            Ambito.Items.Clear();
            

            if (Convert.ToInt32(Destinatario.SelectedValue) > 0)
            {
                Ambito.DataSource = pestanaDA.ListarPestania(0);
                Ambito.DataTextField = "descripcion";
                Ambito.DataValueField = "id";
                
            }

            Ambito.DataBind();
            Ambito.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            PanelAmbito.Visible = true;
            UpdatePanelAmbito.Update();
        }

        protected void LimpiarPorTipoDocumento()
        {
            Ambito.SelectedValue = "0";
            Tipo.SelectedValue = "0";

            AplicaNumero.SelectedValue = "-1";
            AplicaFecha.SelectedValue = "-1";
            AplicaNumeroCI.SelectedValue = "-1";
            AplicaFechaCI.SelectedValue = "-1";
            AplicaArchivoBinario.SelectedValue = "-1";

            PanelAmbito.Visible = false;
            PanelTipo.Visible = false;
            PanelAplica.Visible = false;

            ErroresSuperior.Text = "";
            PanelErroresSuperior.Visible = false;
            UpdatePanelErroresSuperior.Update();
            
            UpdatePanelAmbito.Update();
            UpdatePanelTipo.Update();
            UpdatePanelAplica.Update();
            
        }

        //MUESTRA/OCULTA CAMPOS
        private void controlarCamposLogicos(int idTipoIO)
        {

            //ENTRADA
            if (idTipoIO == rbTipo.INGRESO_SIN_REQUERIMIENTO)
            {

                PanelOrigen.Visible = true;
                PanelAmbito.Visible = true;
                PanelTipo.Visible = true;
            }


            //ENTRADA
            if (idTipoIO == rbTipo.RESPUESTA_A_UN_REQUERIMIENTO)
            {

                PanelOrigen.Visible = true;
                PanelAmbito.Visible = false;
            }


            //SALIDA
            if (idTipoIO == rbTipo.INFORMATIVO)
            {
                PanelDestinatario.Visible = true;
                PanelAmbito.Visible = true;
                PanelTipo.Visible = true;
            }

            //SALIDA
            if (idTipoIO == rbTipo.REQUERIMIENTO_CON_RESPUESTA)
            {
                PanelDestinatario.Visible = true;
                PanelAmbito.Visible = true;
                PanelTipo.Visible = true;
                
            }
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

            PanelSeccion.Visible = true;
            Seccion.DataBind();
            UpdatePanelSeccion.Update();
        }


        protected void Seccion_change(object sender, EventArgs e)
        {

            Tipo.Items.Clear();
            Tipo.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            SubRequerimiento subRequerimientoFiltro = new SubRequerimiento();
            subRequerimientoFiltro.aplicaReiteraFiltro = -1;
            subRequerimientoFiltro.aplicaComplementarioFiltro = -1;
            subRequerimientoFiltro.aplicaVisacionMasivaFiltro = -1;

            List<SubRequerimiento> listaSubRequerimiento = requerimientoDA.ListarSubRequerimiento(subRequerimientoFiltro);

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

            PanelTipo.Visible = true;
            Tipo.DataBind();
            UpdatePanelTipo.Update();


        }

        
        protected void Tipo_change(object sender, EventArgs e)
        {
            this.controlarCamposPorTema();
        }

        //MUESTRA/OCULTA CAMPOS EN BASE AL TEMA, APLICA PARA (SALIDA - INFORMATIVA, ENTRADA - INGRESO SIN REQUERIMIENTO)
        private void controlarCamposPorTema()
        {

            /*
            if ((Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.SALIDA && 
                Convert.ToInt32(TipoSalida.SelectedValue) == rbTipo.INFORMATIVO) || (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.ENTRADA && 
                Convert.ToInt32(TipoEntrada.SelectedValue) == rbTipo.INGRESO_SIN_REQUERIMIENTO))
            {

                if (Convert.ToInt32(Tipo.SelectedValue) > 0)
                {

                    Hashtable camposObligatorios = null;
                    Hashtable hashIdTipoIO = null;
                    Hashtable hashIdTipoOrigenDestinatario = null;
                    Hashtable hashIdPestana = null;
                    Hashtable hashIdTipoDocumento = null;
                    
                    camposObligatorios = (Hashtable)ViewState["HashCampos"];

                    if (camposObligatorios != null)
                    {
                        if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.SALIDA)
                        {
                            hashIdTipoIO = (Hashtable)camposObligatorios[Convert.ToInt32(TipoSalida.SelectedValue)];
                        }
                        if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.ENTRADA)
                        {
                            hashIdTipoIO = (Hashtable)camposObligatorios[Convert.ToInt32(TipoEntrada.SelectedValue)];
                        }

                    }

                    if (hashIdTipoIO != null)
                    {
                        if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.SALIDA)
                        {
                            hashIdTipoOrigenDestinatario = (Hashtable)hashIdTipoIO[Convert.ToInt32(Destinatario.SelectedValue)];
                        }
                        if (Convert.ToInt32(FlujoDocumental.SelectedValue) == rbTipo.ENTRADA)
                        {
                            hashIdTipoOrigenDestinatario = (Hashtable)hashIdTipoIO[Convert.ToInt32(Origen.SelectedValue)];
                        }
                    }

                    if (hashIdTipoOrigenDestinatario != null)
                    {
                        hashIdPestana = (Hashtable)hashIdTipoOrigenDestinatario[Convert.ToInt32(Ambito.SelectedValue)];
                    }


                    if (hashIdPestana != null)
                    {
                        hashIdTipoDocumento = (Hashtable)hashIdPestana[Convert.ToInt32(TipoDocumento.SelectedValue)];
                    }
                }
            }
            */

                ParametroGenerico parametroGenericoFiltro = new ParametroGenerico();
                parametroGenericoFiltro.clave = "TIPO_DOCUMENTO";

                TipoDocumento.Items.Clear();
                TipoDocumento.DataSource = mantenedorDA.ListarTipo_Mantenedor(parametroGenericoFiltro);
                TipoDocumento.DataTextField = "descripcion";
                TipoDocumento.DataValueField = "id";
                TipoDocumento.DataBind();
                TipoDocumento.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                PanelTipoDocumento.Visible = true;
                UpdatePanelTipoDocumento.Update();
                 
        }

        //GRID REQUERIMIENTO
        protected void GridRequerimiento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar la Asociación subrequerimiento - tipo?')");
                    boton_eliminar.Visible = true;
                };

                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_eliminar != null)
                {
                    boton_modificar.Visible = true;
                };
                
            }

            
            if (GridRequerimiento.EditIndex == e.Row.RowIndex && e.Row.RowType == DataControlRowType.DataRow)
            {
                /* Aplica Número */
                DropDownList ddllist = ((DropDownList)e.Row.FindControl("ddleditCountry"));
                var hdnCountryName = ((HiddenField)e.Row.FindControl("hdnCountry"));
                ddllist.AppendDataBoundItems = true;

                ddllist.Items.Clear();
                ddllist.DataBind();
                ddllist.Items.Insert(0, new ListItem("Campo No Aplica", "0"));
                ddllist.Items.Insert(0, new ListItem("Campo Aplica y es Obligatorio", "1"));
                ddllist.Items.Insert(0, new ListItem("Campo Aplica y es Opcional", "2"));
                ddllist.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                ddllist.Items.FindByText(hdnCountryName.Value).Selected = true;
                
                /* Aplica Fecha */
                DropDownList ddllist2 = ((DropDownList)e.Row.FindControl("ddleditCountry2"));
                var hdnCountryName2 = ((HiddenField)e.Row.FindControl("hdnCountry2"));
                ddllist2.AppendDataBoundItems = true;

                ddllist2.Items.Clear();
                ddllist2.DataBind();
                ddllist2.Items.Insert(0, new ListItem("Campo No Aplica", "0"));
                ddllist2.Items.Insert(0, new ListItem("Campo Aplica y es Obligatorio", "1"));
                ddllist2.Items.Insert(0, new ListItem("Campo Aplica y es Opcional", "2"));
                ddllist2.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                ddllist2.Items.FindByText(hdnCountryName2.Value).Selected = true;

                /* Aplica N° CI */
                DropDownList ddllist3 = ((DropDownList)e.Row.FindControl("ddleditCountry3"));
                var hdnCountryName3 = ((HiddenField)e.Row.FindControl("hdnCountry3"));
                ddllist3.AppendDataBoundItems = true;

                ddllist3.Items.Clear();
                ddllist3.DataBind();
                ddllist3.Items.Insert(0, new ListItem("Campo No Aplica", "0"));
                ddllist3.Items.Insert(0, new ListItem("Campo Aplica y es Obligatorio", "1"));
                ddllist3.Items.Insert(0, new ListItem("Campo Aplica y es Opcional", "2"));
                ddllist3.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                ddllist3.Items.FindByText(hdnCountryName3.Value).Selected = true;

                /* Aplica Fecha CI */
                DropDownList ddllist4 = ((DropDownList)e.Row.FindControl("ddleditCountry4"));
                var hdnCountryName4 = ((HiddenField)e.Row.FindControl("hdnCountry4"));
                ddllist4.AppendDataBoundItems = true;

                ddllist4.Items.Clear();
                ddllist4.Items.Clear();
                ddllist4.DataBind();
                ddllist4.Items.Insert(0, new ListItem("Campo No Aplica", "0"));
                ddllist4.Items.Insert(0, new ListItem("Campo Aplica y es Obligatorio", "1"));
                ddllist4.Items.Insert(0, new ListItem("Campo Aplica y es Opcional", "2"));
                ddllist4.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                ddllist4.Items.FindByText(hdnCountryName4.Value).Selected = true;

                /* Aplica Archivo Adjunto */
                DropDownList ddllist5 = ((DropDownList)e.Row.FindControl("ddleditCountry5"));
                var hdnCountryName5 = ((HiddenField)e.Row.FindControl("hdnCountry5"));
                ddllist5.AppendDataBoundItems = true;

                ddllist5.Items.Clear();
                ddllist5.Items.Clear();
                ddllist5.DataBind();
                ddllist5.Items.Insert(0, new ListItem("Campo No Aplica", "0"));
                ddllist5.Items.Insert(0, new ListItem("Campo Aplica y es Obligatorio", "1"));
                ddllist5.Items.Insert(0, new ListItem("Campo Aplica y es Opcional", "2"));
                ddllist5.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                ddllist5.Items.FindByText(hdnCountryName5.Value).Selected = true;

            }

        }

        //GRID REQUERIMIENTO
        protected void GridRequerimiento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            PanelErroresInferior.Visible = false;
            int idValidacionDocumentacion = 0;
            
            switch (e.CommandName)
            {
                case "Eliminar":
                    idValidacionDocumentacion = Convert.ToInt32(e.CommandArgument);
                    GridRequerimiento.EditIndex = -1;
                    EliminarValidacionDocumentacion(idValidacionDocumentacion);
                    cargarGrillaValidacionDocumentacion();
                    break;
            }
        
        }

        protected void GridRequerimiento_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Guardar.Enabled = false;
            GridRequerimiento.EditIndex = e.NewEditIndex;
            cargarGrillaValidacionDocumentacion();
        }

        protected void GridRequerimiento_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = 0;

            DropDownList aplicaNumero = (DropDownList)GridRequerimiento.Rows[e.RowIndex].FindControl("ddleditCountry");
            DropDownList aplicaFecha = (DropDownList)GridRequerimiento.Rows[e.RowIndex].FindControl("ddleditCountry2");
            DropDownList aplicaNumeroCI = (DropDownList)GridRequerimiento.Rows[e.RowIndex].FindControl("ddleditCountry3");
            DropDownList aplicaFechaCI = (DropDownList)GridRequerimiento.Rows[e.RowIndex].FindControl("ddleditCountry4");
            DropDownList aplicaArchivoAdjunto = (DropDownList)GridRequerimiento.Rows[e.RowIndex].FindControl("ddleditCountry5");
            /*
            CheckBoxList verificaConforme = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geVerificaConforme");
            CheckBoxList nuevaFecha = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geNuevaFecha");
            CheckBoxList concesion = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geConcesion");
            CheckBoxList modAmpliacion = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geModAmpliacion");
            CheckBoxList modReduccion = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geModReduccion");
            CheckBoxList modEspeciePT = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geModEspeciePT");
            CheckBoxList modRegularizacion = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geModRegularizacion");
            CheckBoxList relocalizacion = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geRelocalizacion");
            CheckBoxList amerb = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAmerb");
            CheckBoxList faenamiento = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geFaenamiento");
            CheckBoxList acopio = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAcopio");
            CheckBoxList colector = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geColector");
            CheckBoxList verificaAmpPlazo = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geVerificaAmpPlazo");
            CheckBoxList verificaAmpExtension = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geVerificaAmpExtension");
            CheckBoxList verificaExpAmerb = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaExpAmerb");
            CheckBoxList verificaExpConcesion = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaExpConcesion");
            CheckBoxList aplicaECMPO = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaECMPO");
            CheckBoxList modECMPOAmpliacion = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaModECMPOAmpliacion");
            CheckBoxList modECMPOReduccion = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaModECMPOReduccion");
            CheckBoxList modECMPOEspecie = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaModECMPOEspecie");
            CheckBoxList modECMPOPT = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaModECMPOPT");
            CheckBoxList modECMPORegularizacion = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaModECMPORegularizacion");
            CheckBoxList modAcopioAmpliacion = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaModAcopioAmpliacion");
            CheckBoxList modAcopioReduccion = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaModAcopioReduccion");
            CheckBoxList modAcopioEspecie = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaModAcopioEspecie");
            CheckBoxList modAcopioPT = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaModAcopioPT");
            CheckBoxList modAcopioRegularizacion = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaModAcopioRegularizacion");
            CheckBoxList modFaenamientoAmpliacion = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaModFaenamientoAmpliacion");
            CheckBoxList modFaenamientoReduccion = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaModFaenamientoReduccion");
            CheckBoxList modFaenamientoEspecie = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaModFaenamientoEspecie");
            CheckBoxList modFaenamientoPT = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaModFaenamientoPT");
            CheckBoxList modFaenamientoRegularizacion = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaModFaenamientoRegularizacion");
            CheckBoxList modAmerbAmpliacion = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaModAmerbAmpliacion");
            CheckBoxList modAmerbReduccion = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaModAmerbReduccion");
            CheckBoxList modAmerbEspecie = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaModAmerbEspecie");
            CheckBoxList modAmerbPT = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaModAmerbPT");
            CheckBoxList modAmerbRegularizacion = (CheckBoxList)GridRequerimiento.Rows[e.RowIndex].FindControl("geAplicaModAmerbRegularizacion");
            */

            id = Convert.ToInt32(GridRequerimiento.DataKeys[e.RowIndex].Value);
            Guardar.Enabled = true;
            /*
            Update(id, aplicaNumero.SelectedItem.Value, aplicaFecha.SelectedItem.Value, aplicaNumeroCI.SelectedItem.Value, aplicaFechaCI.SelectedItem.Value, aplicaArchivoAdjunto.SelectedItem.Value,
                   verificaConforme.SelectedItem, nuevaFecha.SelectedItem, concesion.SelectedItem, modAmpliacion.SelectedItem, modReduccion.SelectedItem, modEspeciePT.SelectedItem, modRegularizacion.SelectedItem,
                   relocalizacion.SelectedItem, amerb.SelectedItem, faenamiento.SelectedItem, acopio.SelectedItem, colector.SelectedItem, verificaAmpPlazo.SelectedItem, verificaAmpExtension.SelectedItem,
                   verificaExpAmerb.SelectedItem, verificaExpConcesion.SelectedItem, aplicaECMPO.SelectedItem, modECMPOAmpliacion.SelectedItem, modECMPOReduccion.SelectedItem, modECMPOEspecie.SelectedItem,
                   modECMPOPT.SelectedItem, modECMPORegularizacion.SelectedItem, modAcopioAmpliacion.SelectedItem, modAcopioReduccion.SelectedItem, modAcopioEspecie.SelectedItem, modAcopioPT.SelectedItem,
                   modAcopioRegularizacion.SelectedItem, modFaenamientoAmpliacion.SelectedItem, modFaenamientoReduccion.SelectedItem, modFaenamientoEspecie.SelectedItem, modFaenamientoPT.SelectedItem,
                   modFaenamientoRegularizacion.SelectedItem, modAmerbAmpliacion.SelectedItem, modAmerbReduccion.SelectedItem, modAmerbEspecie.SelectedItem, modAmerbPT.SelectedItem, modAmerbRegularizacion.SelectedItem);
            */

            Update(id, aplicaNumero.SelectedItem.Value, aplicaFecha.SelectedItem.Value, aplicaNumeroCI.SelectedItem.Value, aplicaFechaCI.SelectedItem.Value, aplicaArchivoAdjunto.SelectedItem.Value);
                   
            cargarGrillaValidacionDocumentacion();

        }
        /*
        private void Update(int id, string aplicaNumero, string aplicaFecha, string aplicaNumeroCI, string aplicaFechaCI, string aplicaArchivoAdjunto,
            ListItem verificaConforme, ListItem nuevaFecha, ListItem concesion, ListItem modAmpliacion, ListItem modReduccion, ListItem modEspeciePT, ListItem modRegularizacion, 
            ListItem relocalizacion, ListItem amerb, ListItem faenamiento, ListItem acopio, ListItem colector, ListItem verificaAmpPlazo, ListItem verificaAmpExtension, 
            ListItem verificaExpAmerb, ListItem verificaExpConcesion, ListItem aplicaECMPO, ListItem modECMPOAmpliacion, ListItem modECMPOReduccion, ListItem modECMPOEspecie, 
            ListItem modECMPOPT, ListItem modECMPORegularizacion, ListItem modAcopioAmpliacion, ListItem modAcopioReduccion, ListItem modAcopioEspecie, ListItem modAcopioPT, 
            ListItem modAcopioRegularizacion, ListItem modFaenamientoAmpliacion, ListItem modFaenamientoReduccion, ListItem modFaenamientoEspecie, ListItem modFaenamientoPT, 
            ListItem modFaenamientoRegularizacion, ListItem modAmerbAmpliacion, ListItem modAmerbReduccion, ListItem modAmerbEspecie, ListItem modAmerbPT, ListItem modAmerbRegularizacion)
        {
        */
        private void Update(int id, string aplicaNumero, string aplicaFecha, string aplicaNumeroCI, string aplicaFechaCI, string aplicaArchivoAdjunto)
        {   
                ValidacionDocumentacion validacionDocumentacion = new ValidacionDocumentacion();

                validacionDocumentacion.idValDocumentacion = id;

                if (aplicaNumero != null && !aplicaNumero.Equals("-1"))
                {
                    validacionDocumentacion.numero = Convert.ToInt32(aplicaNumero);
                }

                if (aplicaFecha != null && !aplicaFecha.Equals("-1"))
                {
                    validacionDocumentacion.fecha = Convert.ToInt32(aplicaFecha);
                }

                if (aplicaNumeroCI != null && !aplicaNumeroCI.Equals("-1"))
                {
                    validacionDocumentacion.numeroCI = Convert.ToInt32(aplicaNumeroCI);
                }

                if (aplicaFechaCI != null && !aplicaFechaCI.Equals("-1"))
                {
                    validacionDocumentacion.fechaCI = Convert.ToInt32(aplicaFechaCI);
                }

                if (aplicaArchivoAdjunto != null && !aplicaArchivoAdjunto.Equals("-1"))
                {
                    validacionDocumentacion.archivoBinario = Convert.ToInt32(aplicaArchivoAdjunto);
                }

                /*
                if (verificaConforme != null && verificaConforme.Value.Equals("Si"))
                {
                    validacionDocumentacion.verificaConforme = true;
                }

                if (nuevaFecha != null && nuevaFecha.Value.Equals("Si"))
                {
                    validacionDocumentacion.nuevaFecha = 1;
                }

                if (concesion != null && concesion.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaConcesion = 1;
                }

                if (modAmpliacion != null && modAmpliacion.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModAmpliacion = 1;
                }

                if (modEspeciePT != null && modEspeciePT.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModEspeciePT = 1;
                }

                if (modRegularizacion != null && modRegularizacion.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModRegularizacion = 1;
                }

                if (relocalizacion != null && relocalizacion.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaRelocalizacion = 1;
                }

                if (amerb != null && amerb.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaAmerb = 1;
                }

                if (faenamiento != null && faenamiento.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaFaenamiento = 1;
                }

                if (acopio != null && acopio.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaAcopio = 1;
                }

                if (colector != null && colector.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaColectores = 1;
                }

                if (verificaAmpPlazo != null && verificaAmpPlazo.Value.Equals("Si"))
                {
                    validacionDocumentacion.verificaAmpPlazo = 1;
                }

                if (verificaAmpExtension != null && verificaAmpExtension.Value.Equals("Si"))
                {
                    validacionDocumentacion.verificaAmpExtension = 1;
                }

                if (verificaExpAmerb != null && verificaExpAmerb.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaExperimentalesAmerb = 1;
                }

                if (verificaExpConcesion != null && verificaExpConcesion.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaExpConcesion = 1;
                }

                if (aplicaECMPO != null && aplicaECMPO.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaAcuiculturaEcmpo = 1;
                }

                if (modECMPOAmpliacion != null && modECMPOAmpliacion.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModECMPOAmpl = 1;
                }

                if (modECMPOReduccion != null && modECMPOReduccion.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModECMPOReduc = 1;
                }

                if (modECMPOEspecie != null && modECMPOEspecie.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModECMPOEspecie = 1;
                }

                if (modECMPOPT != null && modECMPOPT.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModECMPO_PT = 1;
                }

                if (modECMPORegularizacion != null && modECMPORegularizacion.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModECMPO_Regulariz = 1;
                }

                if (modAcopioAmpliacion != null && modAcopioAmpliacion.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModAcopioAmpl = 1;
                }

                if (modAcopioReduccion != null && modAcopioReduccion.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModAcopioReduc = 1;
                }

                if (modAcopioEspecie != null && modAcopioEspecie.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModAcopioEspecie = 1;
                }

                if (modAcopioPT != null && modAcopioPT.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModAcopio_PT = 1;
                }

                if (modAcopioRegularizacion != null && modAcopioRegularizacion.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModAcopioRegulariz = 1;
                }

                if (modFaenamientoAmpliacion != null && modFaenamientoAmpliacion.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModFaenamAmpl = 1;
                }

                if (modFaenamientoReduccion != null && modFaenamientoReduccion.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModFaenamReduc = 1;
                }

                if (modFaenamientoEspecie != null && modFaenamientoEspecie.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModFaenamEspecie = 1;
                }

                if (modFaenamientoPT != null && modFaenamientoPT.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModFaenam_PT = 1;
                }

                if (modFaenamientoRegularizacion != null && modFaenamientoRegularizacion.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModFaenamRegulariz = 1;
                }

                if (modAmerbAmpliacion != null && modAmerbAmpliacion.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModAmerbAmpl = 1;
                }

                if (modAmerbReduccion != null && modAmerbReduccion.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModAmerbReduc = 1;
                }

                if (modAmerbEspecie != null && modAmerbEspecie.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModAmerbEspecie = 1;
                }

                if (modAmerbPT != null && modAmerbPT.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModAmerb_PT = 1;
                }

                if (modAmerbRegularizacion != null && modAmerbRegularizacion.Value.Equals("Si"))
                {
                    validacionDocumentacion.aplicaModAmerbRegulariz = 1;
                }
                */

                if (validacionDocumentacion != null)
                {

                    DataTable dt = new DataTable(); //Actualizar Validación Documentación.
                    string mensaje = "";

                    try
                    {
                        //string msg = Convert.ToString(dt.Rows[0]["msg"]);
                        string msg = "OK";
                        if (msg == "OK")
                        {
                            mensaje = "El Subrequerimiento Tipo con ID:" + id + " ha sido actualizado.";
                            GridRequerimiento.EditIndex = -1;
                        }
                    }
                    catch
                    {
                        mensaje = "Se ha producido un error al intentar actualizar el Subrequerimiento Tipo.";
                    };

                    PanelErroresInferior.Visible = true;
                    ErroresInferior.Text = mensaje;
                    UpdatePanelErroresInferior.Update();
                }
        }

        protected void GridRequerimiento_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Guardar.Enabled = true;
            GridRequerimiento.EditIndex = -1;
            cargarGrillaValidacionDocumentacion();
        }

        private void EliminarValidacionDocumentacion(int idValidacionDocumentacion)
        {
            
            DataTable dt = mantenedorTransversalService.eliminarValidacionDocumentacion(idValidacionDocumentacion);
            
            try
            {
                string msg = Convert.ToString(dt.Rows[0]["msg"]);

                switch (msg)
                {
                    case "OK":
                        ErroresInferior.Text = "Se ha eliminado exitosamente la asociación subrequerimiento - tipo";
                        PanelErroresInferior.Visible = true;
                        UpdatePanelErroresInferior.Update();

                        cargarGrillaValidacionDocumentacion();
                        UpdatePanelGridRequerimiento.Update();
                        break;
                    case "En uso":
                        ErroresInferior.Text = "La asociación subrequerimiento - tipo que intenta borrar está actualmente en uso.";
                        PanelErroresInferior.Visible = true;
                        UpdatePanelErroresInferior.Update();

                        break;
                    default:
                        ErroresInferior.Text = "No se ha eliminado la asociación subrequerimiento - tipo";
                        PanelErroresInferior.Visible = true;
                        UpdatePanelErroresInferior.Update();
                        
                        break;
                };
            }
            catch
            {
                ErroresInferior.Text = "Se ha producido un error al intentar eliminar la asociación subrequerimiento - tipo.";
                
            };
        }


        /* Método para guardar una validación - documentación*/
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
                        if (Convert.ToInt32(Seccion.SelectedValue) > 0){
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
                    else {
                        Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Aplica Número"));
                    }

                    if (Convert.ToInt32(AplicaFecha.SelectedValue) > 0)
                    {
                        validacionDocumentacion.fecha = Convert.ToInt32(AplicaFecha.SelectedValue);
                    }
                    else {
                        Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Aplica Fecha"));
                    }

                    if (Convert.ToInt32(AplicaNumeroCI.SelectedValue) > 0)
                    {
                        validacionDocumentacion.numeroCI = Convert.ToInt32(AplicaNumeroCI.SelectedValue);
                    }
                    else {
                        Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Aplica Número CI"));
                    }

                    if (Convert.ToInt32(AplicaFechaCI.SelectedValue) > 0)
                    {
                        validacionDocumentacion.fechaCI = Convert.ToInt32(AplicaFechaCI.SelectedValue);
                    }
                    else {
                        Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Aplica Fecha CI"));
                    }

                    if (Convert.ToInt32(AplicaArchivoBinario.SelectedValue) > 0)
                    {
                        validacionDocumentacion.archivoBinario = Convert.ToInt32(AplicaArchivoBinario.SelectedValue);
                    }
                    else {
                        Page.Validators.Add(new ValidationError(erroresSumary, "Seleccione Aplica Archivo Adjunto"));
                    }

                    //if(VerificaConforme.Checked){
                    //    validacionDocumentacion.verificaConforme = true;
                    //}
                    
                    //if(AplicaConcesion.Checked){
                    //    validacionDocumentacion.aplicaConcesion = 1;
                    //}
                    
                    //if(AplicaModificacionAmpliacion.Checked){
                    //    validacionDocumentacion.aplicaModAmpliacion = 1;
                    //}

                    //if(AplicaModificacionEspeciePT.Checked){
                    //    validacionDocumentacion.aplicaModEspeciePT = 1;
                    //}

                    //if(AplicaModificacionRegularizacion.Checked){
                    //    validacionDocumentacion.aplicaModRegularizacion = 1;
                    //}
                   
                    //if(AplicaAmerb.Checked){
                    //    validacionDocumentacion.aplicaAmerb = 1;
                    //}
                    
                    //if(AplicaFaenamiento.Checked){
                    //    validacionDocumentacion.aplicaFaenamiento = 1;
                    //}

                    //if(AplicaAcopio.Checked){
                    //    validacionDocumentacion.aplicaAcopio = 1;
                    //}
                    
                    //if(AplicaColectores.Checked){
                    //    validacionDocumentacion.aplicaColectores = 1;
                    //}

                    //if(VerificaAmpliacionPlazo.Checked){
                    //    validacionDocumentacion.verificaAmpPlazo = 1;
                    //}

                    //if(VerficiaAmpliacionExtension.Checked){
                    //    validacionDocumentacion.verificaAmpExtension = 1;
                    //}

                    //if (ExtensionPlazo.Checked) {
                    //    validacionDocumentacion.nuevaFecha = 1;
                    //}

                    //if (AplicaExpAmerb.Checked) {
                    //    validacionDocumentacion.aplicaExperimentalesAmerb = 1;
                    //}

                    //if (AplicaExpConcesion.Checked)
                    //{
                    //    validacionDocumentacion.aplicaExpConcesion = 1;
                    //}

                    //if (AplicaECMPO.Checked)
                    //{
                    //    validacionDocumentacion.aplicaAcuiculturaEcmpo = 1;
                    //}

                    //if (AplicaModECMPOAmpliacion.Checked)
                    //{
                    //    validacionDocumentacion.aplicaModECMPOAmpl = 1;
                    //}

                    //if (AplicaModECMPOReduccion.Checked)
                    //{
                    //    validacionDocumentacion.aplicaModECMPOReduc = 1;
                    //}

                    //if (AplicaModECMPOEspecie.Checked)
                    //{
                    //    validacionDocumentacion.aplicaModECMPOEspecie = 1;
                    //}

                    //if (AplicaModECMPO_PT.Checked)
                    //{
                    //    validacionDocumentacion.aplicaModECMPO_PT = 1;
                    //}

                    //if (AplicaModECMPORegularizacion.Checked)
                    //{
                    //    validacionDocumentacion.aplicaModECMPO_Regulariz = 1;
                    //}

                    //if (AplicaModAcopioAmpliacion.Checked)
                    //{
                    //    validacionDocumentacion.aplicaModAcopioAmpl = 1;
                    //}

                    //if (AplicaModAcopioReduccion.Checked)
                    //{
                    //    validacionDocumentacion.aplicaModAcopioReduc = 1;
                    //}

                    //if (AplicaModAcopioEspecie.Checked)
                    //{
                    //    validacionDocumentacion.aplicaModAcopioEspecie = 1;
                    //}

                    //if (AplicaModAcopioPT.Checked)
                    //{
                    //    validacionDocumentacion.aplicaModAcopio_PT = 1;
                    //}

                    //if (AplicaModAcopioRegularizacion.Checked)
                    //{
                    //    validacionDocumentacion.aplicaModAcopioRegulariz = 1;
                    //}

                    //if (AplicaModFaenamientoAmpliacion.Checked)
                    //{
                    //    validacionDocumentacion.aplicaModFaenamAmpl = 1;
                    //}

                    //if (AplicaModFaenamientoReduccion.Checked)
                    //{
                    //    validacionDocumentacion.aplicaModFaenamReduc = 1;
                    //}

                    //if (AplicaModFaenamientoEspecie.Checked)
                    //{
                    //    validacionDocumentacion.aplicaModFaenamEspecie = 1;
                    //}

                    //if (AplicaModFaenamientoPT.Checked)
                    //{
                    //    validacionDocumentacion.aplicaModFaenam_PT = 1;
                    //}

                    //if (AplicaModFaenamientoRegularizacion.Checked)
                    //{
                    //    validacionDocumentacion.aplicaModFaenamRegulariz = 1;
                    //}

                    //if (AplicaModAmerbAmpliacion.Checked)
                    //{
                    //    validacionDocumentacion.aplicaModAmerbAmpl = 1;
                    //}

                    //if (AplicaModAmerbReduccion.Checked)
                    //{
                    //    validacionDocumentacion.aplicaModAmerbReduc = 1;
                    //}

                    //if (AplicaModAmerbEspecie.Checked)
                    //{
                    //    validacionDocumentacion.aplicaModAmerbEspecie = 1;
                    //}

                    //if (AplicaModAmerbPT.Checked)
                    //{
                    //    validacionDocumentacion.aplicaModAmerb_PT = 1;
                    //}

                    //if (AplicaModAmerbRegularizacion.Checked)
                    //{
                    //    validacionDocumentacion.aplicaModAmerbRegulariz = 1;
                    //}

                    /* Aquí se debe validar el ingreso de un subrequerimiento */
                    List<String> errores = new List<string>();
                    
                    if (errores.Count == 0)
                    {
                        /* Aquí se debe guardar la asociación */
                        bool resp = mantenedorTransversalService.guardarAsociacionSubrequerimientoTipo(validacionDocumentacion);

                        if (resp)
                        {
                            cargarGrillaValidacionDocumentacion();

                            UpdatePanelGridRequerimiento.Update();

                            ErroresInferior.Text = "Se ha guardado exitosamente la asociación subrequerimiento - tipo";
                            
                            PanelErroresInferior.Visible = true;
                            UpdatePanelErroresInferior.Update();

                            this.LimpiarPorFlujoDocumental();
                            FlujoDocumental.SelectedValue = "0";
                            UpdatePanelFlujoDocumental.Update();

                        }
                        else
                        {
                            ErroresInferior.Text = "Ha ocurrido un error al guardar la asociación subrequerimiento - tipo";
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

        private void cargarGrillaValidacionDocumentacion()
        {
            ValidacionDocumentacion validacionDocumentacion = new ValidacionDocumentacion();
            List<ValidacionDocumentacion> dt = validacionDocumentacionDA.ListaValidacionDocumentacion(validacionDocumentacion);

            GridRequerimiento.DataSource = dt;
            GridRequerimiento.DataBind();

            if (dt != null && dt.Count > 0)
            {
                ExportarGrilla.Visible = true;
            }
            else {
                ExportarGrilla.Visible = false;
            }
        }

        protected void GridRequerimiento_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridRequerimiento.PageIndex = e.NewPageIndex;
            GridRequerimiento.EditIndex = -1;
            cargarGrillaValidacionDocumentacion();
        }

        //VALIDACIONES CAMPOS OBLIGATORIOS
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

        protected void TipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            PanelAplica.Visible = true;
            UpdatePanelAplica.Update();
        }

        protected void Volver_Click(object sender, EventArgs e)
        {
            string path = "~/Administrador/principal.aspx";
            Response.Redirect(path);
        }

        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();
            string nom_grilla = "asocSubrequerimientoTipo";
            string ngrilla = "";

            cargarGrillaValidacionDocumentacion();

            switch (nom_grilla)
            {
                case "asocSubrequerimientoTipo":
                    GridRequerimiento.Columns.RemoveAt(52);
                    grilla = GridRequerimiento;
                    ngrilla = "asocSubrequerimientoTipo.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
                ValidacionDocumentacion validacionDocumentacion = new ValidacionDocumentacion();

                validacionDocumentacion.flujoDocumental = new ParametroGenerico(Convert.ToInt32(FlujoDocumental.SelectedValue));

                if (validacionDocumentacion.flujoDocumental.id == rbTipo.ENTRADA)
                {
                    //Tipo entrada
                    validacionDocumentacion.tipoIO = new ParametroGenerico(Convert.ToInt32(TipoEntrada.SelectedValue));
                }

                if (validacionDocumentacion.flujoDocumental.id == rbTipo.SALIDA)
                {
                    //Tipo salida
                    validacionDocumentacion.tipoIO = new ParametroGenerico(Convert.ToInt32(TipoSalida.SelectedValue));
                }

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

                    //Ámbito
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

                    //Tipo
                    if (Convert.ToInt32(Tipo.SelectedValue) > 0)
                    {

                        validacionDocumentacion.subRequerimiento = new ParametroGenerico();
                        validacionDocumentacion.subRequerimiento.id = Convert.ToInt32(Tipo.SelectedValue);
                    }

                    //Tipo de Documento
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

                if (Convert.ToInt32(AplicaFecha.SelectedValue) > 0)
                {
                    validacionDocumentacion.fecha = Convert.ToInt32(AplicaFecha.SelectedValue);
                }

                if (Convert.ToInt32(AplicaNumeroCI.SelectedValue) > 0)
                {
                    validacionDocumentacion.numeroCI = Convert.ToInt32(AplicaNumeroCI.SelectedValue);
                }

                if (Convert.ToInt32(AplicaFechaCI.SelectedValue) > 0)
                {
                    validacionDocumentacion.fechaCI = Convert.ToInt32(AplicaFechaCI.SelectedValue);
                }

                if (Convert.ToInt32(AplicaArchivoBinario.SelectedValue) > 0)
                {
                    validacionDocumentacion.archivoBinario = Convert.ToInt32(AplicaArchivoBinario.SelectedValue);
                }

                //if (VerificaConforme.Checked)
                //{
                //    validacionDocumentacion.verificaConforme = true;
                //}

                //if (AplicaConcesion.Checked)
                //{
                //    validacionDocumentacion.aplicaConcesion = 1;
                //}

                validacionDocumentacion.aplicaConcesion = Convert.ToInt32(AplicaConcesion.SelectedItem.Value);

                //if (AplicaModificacionAmpliacion.Checked)
                //{
                //    validacionDocumentacion.aplicaModAmpliacion = 1;
                //}
                validacionDocumentacion.aplicaModAmpliacion = Convert.ToInt32(AplicaModificacionAmpliacion.SelectedItem.Value);

                //if (AplicaModificacionEspeciePT.Checked)
                //{
                //    validacionDocumentacion.aplicaModEspeciePT = 1;
                //}
                validacionDocumentacion.aplicaModEspeciePT = Convert.ToInt32(AplicaModificacionEspeciePT.SelectedItem.Value);

                //if (AplicaModificacionRegularizacion.Checked)
                //{
                //    validacionDocumentacion.aplicaModRegularizacion = 1;
                //}
                validacionDocumentacion.aplicaModRegularizacion = Convert.ToInt32(AplicaModificacionRegularizacion.SelectedItem.Value);

                //if (AplicaAmerb.Checked)
                //{
                //    validacionDocumentacion.aplicaAmerb = 1;
                //}
                validacionDocumentacion.aplicaAmerb = Convert.ToInt32(AplicaAmerb.SelectedItem.Value);

                //if (AplicaFaenamiento.Checked)
                //{
                //    validacionDocumentacion.aplicaFaenamiento = 1;
                //}
                validacionDocumentacion.aplicaFaenamiento = Convert.ToInt32(AplicaFaenamiento.SelectedItem.Value);

                //if (AplicaAcopio.Checked)
                //{
                //    validacionDocumentacion.aplicaAcopio = 1;
                //}
                validacionDocumentacion.aplicaAcopio = Convert.ToInt32(AplicaAcopio.SelectedItem.Value);

                //if (AplicaColectores.Checked)
                //{
                //    validacionDocumentacion.aplicaColectores = 1;
                //}
                validacionDocumentacion.aplicaColectores = Convert.ToInt32(AplicaColectores.SelectedItem.Value);

                //if (VerificaAmpliacionPlazo.Checked)
                //{
                //    validacionDocumentacion.verificaAmpPlazo = 1;
                //}
                validacionDocumentacion.verificaAmpPlazo = Convert.ToInt32(VerificaAmpliacionPlazo.SelectedItem.Value);

                //if (VerficiaAmpliacionExtension.Checked)
                //{
                //    validacionDocumentacion.verificaAmpExtension = 1;
                //}
                validacionDocumentacion.verificaAmpExtension = Convert.ToInt32(VerficiaAmpliacionExtension.SelectedItem.Value);

                //if (ExtensionPlazo.Checked)
                //{
                //    validacionDocumentacion.nuevaFecha = 1;
                //}
                validacionDocumentacion.nuevaFecha = Convert.ToInt32(ExtensionPlazo.SelectedItem.Value);

                //if (AplicaExpAmerb.Checked)
                //{
                //    validacionDocumentacion.aplicaExperimentalesAmerb = 1;
                //}
                validacionDocumentacion.aplicaExperimentalesAmerb = Convert.ToInt32(AplicaExpAmerb.SelectedItem.Value);

                //if (AplicaExpConcesion.Checked)
                //{
                //    validacionDocumentacion.aplicaExpConcesion = 1;
                //}
                validacionDocumentacion.aplicaExpConcesion = Convert.ToInt32(AplicaExpConcesion.SelectedItem.Value);

                //if (AplicaECMPO.Checked)
                //{
                //    validacionDocumentacion.aplicaAcuiculturaEcmpo = 1;
                //}
                validacionDocumentacion.aplicaAcuiculturaEcmpo = Convert.ToInt32(AplicaECMPO.SelectedItem.Value);

                //if (AplicaModECMPOAmpliacion.Checked)
                //{
                //    validacionDocumentacion.aplicaModECMPOAmpl = 1;
                //}
                validacionDocumentacion.aplicaModECMPOAmpl = Convert.ToInt32(AplicaModECMPOAmpliacion.SelectedItem.Value);

                //if (AplicaModECMPOReduccion.Checked)
                //{
                //    validacionDocumentacion.aplicaModECMPOReduc = 1;
                //}
                validacionDocumentacion.aplicaModECMPOReduc = Convert.ToInt32(AplicaModECMPOReduccion.SelectedItem.Value);

                //if (AplicaModECMPOEspecie.Checked)
                //{
                //    validacionDocumentacion.aplicaModECMPOEspecie = 1;
                //}
                validacionDocumentacion.aplicaModECMPOEspecie = Convert.ToInt32(AplicaModECMPOEspecie.SelectedItem.Value);

                //if (AplicaModECMPO_PT.Checked)
                //{
                //    validacionDocumentacion.aplicaModECMPO_PT = 1;
                //}
                validacionDocumentacion.aplicaModECMPO_PT = Convert.ToInt32(AplicaModECMPO_PT.SelectedItem.Value);

                //if (AplicaModECMPORegularizacion.Checked)
                //{
                //    validacionDocumentacion.aplicaModECMPO_Regulariz = 1;
                //}
                validacionDocumentacion.aplicaModECMPO_Regulariz = Convert.ToInt32(AplicaModECMPORegularizacion.SelectedItem.Value);

                //if (AplicaModAcopioAmpliacion.Checked)
                //{
                //    validacionDocumentacion.aplicaModAcopioAmpl = 1;
                //}
                validacionDocumentacion.aplicaModAcopioAmpl = Convert.ToInt32(AplicaModAcopioAmpliacion.SelectedItem.Value);

                //if (AplicaModAcopioReduccion.Checked)
                //{
                //    validacionDocumentacion.aplicaModAcopioReduc = 1;
                //}
                validacionDocumentacion.aplicaModAcopioReduc = Convert.ToInt32(AplicaModAcopioReduccion.SelectedItem.Value);

                //if (AplicaModAcopioEspecie.Checked)
                //{
                //    validacionDocumentacion.aplicaModAcopioEspecie = 1;
                //}
                validacionDocumentacion.aplicaModAcopioEspecie = Convert.ToInt32(AplicaModAcopioEspecie.SelectedItem.Value);

                //if (AplicaModAcopioPT.Checked)
                //{
                //    validacionDocumentacion.aplicaModAcopio_PT = 1;
                //}
                validacionDocumentacion.aplicaModAcopio_PT = Convert.ToInt32(AplicaModAcopioPT.SelectedItem.Value);

                //if (AplicaModAcopioRegularizacion.Checked)
                //{
                //    validacionDocumentacion.aplicaModAcopioRegulariz = 1;
                //}
                validacionDocumentacion.aplicaModAcopioRegulariz = Convert.ToInt32(AplicaModAcopioRegularizacion.SelectedItem.Value);

                //if (AplicaModFaenamientoAmpliacion.Checked)
                //{
                //    validacionDocumentacion.aplicaModFaenamAmpl = 1;
                //}
                validacionDocumentacion.aplicaModFaenamAmpl = Convert.ToInt32(AplicaModFaenamientoAmpliacion.SelectedItem.Value);

                //if (AplicaModFaenamientoReduccion.Checked)
                //{
                //    validacionDocumentacion.aplicaModFaenamReduc = 1;
                //}
                validacionDocumentacion.aplicaModFaenamReduc = Convert.ToInt32(AplicaModFaenamientoReduccion.SelectedItem.Value);

                //if (AplicaModFaenamientoEspecie.Checked)
                //{
                //    validacionDocumentacion.aplicaModFaenamEspecie = 1;
                //}
                validacionDocumentacion.aplicaModFaenamEspecie = Convert.ToInt32(AplicaModFaenamientoEspecie.SelectedItem.Value);

                //if (AplicaModFaenamientoPT.Checked)
                //{
                //    validacionDocumentacion.aplicaModFaenam_PT = 1;
                //}
                validacionDocumentacion.aplicaModFaenam_PT = Convert.ToInt32(AplicaModFaenamientoPT.SelectedItem.Value);

                //if (AplicaModFaenamientoRegularizacion.Checked)
                //{
                //    validacionDocumentacion.aplicaModFaenamRegulariz = 1;
                //}
                validacionDocumentacion.aplicaModFaenamRegulariz = Convert.ToInt32(AplicaModFaenamientoRegularizacion.SelectedItem.Value);

                //if (AplicaModAmerbAmpliacion.Checked)
                //{
                //    validacionDocumentacion.aplicaModAmerbAmpl = 1;
                //}
                validacionDocumentacion.aplicaModAmerbAmpl = Convert.ToInt32(AplicaModAmerbAmpliacion.SelectedItem.Value);

                //if (AplicaModAmerbReduccion.Checked)
                //{
                //    validacionDocumentacion.aplicaModAmerbReduc = 1;
                //}
                validacionDocumentacion.aplicaModAmerbReduc = Convert.ToInt32(AplicaModAmerbReduccion.SelectedItem.Value);

                //if (AplicaModAmerbEspecie.Checked)
                //{
                //    validacionDocumentacion.aplicaModAmerbEspecie = 1;
                //}
                validacionDocumentacion.aplicaModAmerbEspecie = Convert.ToInt32(AplicaModAmerbEspecie.SelectedItem.Value);

                //if (AplicaModAmerbPT.Checked)
                //{
                //    validacionDocumentacion.aplicaModAmerb_PT = 1;
                //}
                validacionDocumentacion.aplicaModAmerb_PT = Convert.ToInt32(AplicaModAmerbPT.SelectedItem.Value);

                //if (AplicaModAmerbRegularizacion.Checked)
                //{
                //    validacionDocumentacion.aplicaModAmerbRegulariz = 1;
                //}
                validacionDocumentacion.aplicaModAmerbRegulariz = Convert.ToInt32(AplicaModAmerbRegularizacion.SelectedItem.Value);

                if (validacionDocumentacion != null)
                {

                    List<ValidacionDocumentacion> dt = validacionDocumentacionDA.ListaValidacionDocumentacion(validacionDocumentacion);

                    GridRequerimiento.DataSource = dt;
                    GridRequerimiento.DataBind();

                    if (dt != null && dt.Count > 0)
                    {
                        ExportarGrilla.Visible = true;
                    }
                    else {
                        ExportarGrilla.Visible = false;
                    }

                    PanelErroresInferior.Visible = false;
                    ErroresInferior.Text = "";
                    UpdatePanelGridRequerimiento.Update();

                }
            } 
    }
}