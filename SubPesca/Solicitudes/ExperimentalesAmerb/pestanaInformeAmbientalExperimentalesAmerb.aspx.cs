using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using Datos.Entidades;
using Datos.Contantes;

namespace SubPesca.Solicitudes.ExperimentalesAmerb
{
    public partial class pestanaInformeAmbientalExperimentalesAmerb : System.Web.UI.Page
    {
        SolicitudConcesionService solicitudConcesionService = new SolicitudConcesionService();
        RequerimientoService requerimientoService = new RequerimientoService();


        public void Update()
        {
            this.UpdatePanelInformeSEA.Update();
        }

        public void RecargarPanelRequerimientos()
        {
            UpdatePanelInformeSEA.Update();
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            // PAGE LOAD
            if (!Page.IsPostBack)
            {

                msSometimientoSEA.Text = "";
                PanelSometimientoSEA.Visible = false;
                UpdatePanelSometimientoSEA.Update();


                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[paginas.solicitudExperimentalesAmerbSession];

                if (solicitudConcesion == null)
                {
                    Response.Redirect("~/Solicitudes/Registrar/administrarSolicitudConcesion.aspx");
                }



                FlujoSEA.Items.Clear();
                FlujoSEA.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                FlujoSEA.Items.Insert(1, new ListItem("Sí", Convert.ToString(rbTipo.SOMETIMIENTO_SEA_SOMETE)));
                FlujoSEA.Items.Insert(2, new ListItem("No", Convert.ToString(rbTipo.SOMETIMIENTO_SEA_NO_SOMETE)));
                FlujoSEA.DataBind();

                if (solicitudConcesion.sometimientoSEA != null)
                {
                    FlujoSEA.SelectedValue = Convert.ToString(solicitudConcesion.sometimientoSEA.id);
                }
                FlujoSEA_Secciones();

                //VALIDAR QUE HAYA CONSISTENCIA ENTRE EL SOMETIMIENTO SEA MARCADO POR EL USUARIO Y EL INDICADO EL EL DOCUMENTO DE SOMETIMIENTO SEA (SI DICHO DOCUMENTO EXISTE)
                DocumentoAmbito documentoSometimientoSEA = requerimientoService.obtenerDocPestanaSubRequerimiento(solicitudConcesion.idSolConcesion);

                if (documentoSometimientoSEA != null)
                {

                    if (documentoSometimientoSEA.estadoResultadoResp.id == rbEstadosGenerales.SE_SOMETE_AL_SEA && solicitudConcesion.sometimientoSEA.id != rbTipo.SOMETIMIENTO_SEA_SOMETE)
                    {


                        //msSometimientoSEA.Text = "Existe una Resolución Pertinencia SEA que indica que esta solicitud debe someterse a SEA";

                        msSometimientoSEA.Text = "Existe una Notificación de SMA que indica que esta solicitud debe someterse a SEA";
                        PanelSometimientoSEA.Visible = true;
                        UpdatePanelSometimientoSEA.Update();

                    }
                    else if (documentoSometimientoSEA.estadoResultadoResp.id == rbEstadosGenerales.NO_SE_SOMETE_AL_SEA && solicitudConcesion.sometimientoSEA.id != rbTipo.SOMETIMIENTO_SEA_NO_SOMETE)
                    {

                        //msSometimientoSEA.Text = "Existe una Resolución Pertinencia SEA que indica que esta solicitud no debe someterse a SEA";

                        msSometimientoSEA.Text = "Existe una Notificación SMA que indica que esta solicitud no debe someterse a SEA";
                        PanelSometimientoSEA.Visible = true;
                        UpdatePanelSometimientoSEA.Update();

                    }
                }


            }
        }


        protected void FlujoSEA_Secciones()
        {
            if (Convert.ToInt32(FlujoSEA.SelectedValue) == rbTipo.SOMETIMIENTO_SEA_SOMETE)
            {

                PanelInformeSEACartaTitular.Visible = true;
                PanelInformeSEARCA.Visible = true;
                PanelInformeSEAConsultaSEA.Visible = false;
                PanelInformeSEACartaAmbiental.Visible = false;
                PanelInformeSEAInformeUnidadAmbiental.Visible = false;
            }
            else if (Convert.ToInt32(FlujoSEA.SelectedValue) == rbTipo.SOMETIMIENTO_SEA_NO_SOMETE)
            {
                PanelInformeSEACartaTitular.Visible = false;
                PanelInformeSEARCA.Visible = false;
                PanelInformeSEAConsultaSEA.Visible = true;
                PanelInformeSEACartaAmbiental.Visible = true;
                PanelInformeSEAInformeUnidadAmbiental.Visible = true;

            }
            else
            {

                PanelInformeSEACartaTitular.Visible = false;
                PanelInformeSEARCA.Visible = false;
                PanelInformeSEAConsultaSEA.Visible = false;
                PanelInformeSEACartaAmbiental.Visible = false;
                PanelInformeSEAInformeUnidadAmbiental.Visible = false;

            }

            UpdatePanelInformeSEA.Update();

        }

        protected void FlujoSEA_change(object sender, EventArgs e)
        {


            if (Convert.ToInt32(FlujoSEA.SelectedValue) == rbTipo.SOMETIMIENTO_SEA_SOMETE)
            {

                PanelInformeSEACartaTitular.Visible = true;
                PanelInformeSEARCA.Visible = true;
                PanelInformeSEAConsultaSEA.Visible = false;
                PanelInformeSEACartaAmbiental.Visible = false;
                PanelInformeSEAInformeUnidadAmbiental.Visible = false;
            }
            else if (Convert.ToInt32(FlujoSEA.SelectedValue) == rbTipo.SOMETIMIENTO_SEA_NO_SOMETE)
            {
                PanelInformeSEACartaTitular.Visible = false;
                PanelInformeSEARCA.Visible = false;
                PanelInformeSEAConsultaSEA.Visible = true;
                PanelInformeSEACartaAmbiental.Visible = true;
                PanelInformeSEAInformeUnidadAmbiental.Visible = true;

            }
            else
            {

                PanelInformeSEACartaTitular.Visible = false;
                PanelInformeSEARCA.Visible = false;
                PanelInformeSEAConsultaSEA.Visible = false;
                PanelInformeSEACartaAmbiental.Visible = false;
                PanelInformeSEAInformeUnidadAmbiental.Visible = false;

            }

            UpdatePanelInformeSEA.Update();


            string script = "mostrarPestanas(" + rbPestana.INFORME_SEA + ");";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptMostrarPestanas" + rbPestana.INFORME_SEA, script.ToString(), true);

        }



        protected void Guardar_Click(object sender, EventArgs e)
        {

            SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[paginas.solicitudExperimentalesAmerbSession];

            msSometimientoSEA.Text = "";
            PanelSometimientoSEA.Visible = false;
            UpdatePanelSometimientoSEA.Update();


            if (solicitudConcesion == null)
            {
                Response.Redirect("~/Solicitudes/Registrar/administrarSolicitudConcesion.aspx");
            }


            if (solicitudConcesionService.ActualizaSolicitudSEA(solicitudConcesion.idSolConcesion, Convert.ToInt32(FlujoSEA.SelectedValue), Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario))
            {
                FlujoSEA_change(null, null);

                msSometimientoSEA.Text = "Actualización realizada con exito.";
                PanelSometimientoSEA.Visible = true;
                UpdatePanelSometimientoSEA.Update();

                solicitudConcesion.sometimientoSEA = new ParametroGenerico(Convert.ToInt32(FlujoSEA.SelectedValue));

                Session["solicitudConcesion"] = solicitudConcesion;

                //RECARGANDO LA INFORMACION DE LA SOLICITUD, POR SI CAMBIO EL ESTADO
                UpdatePanel UpdatePanelInformacionSolictud = SubPesca.Solicitudes.Registrar.informacionSolicitud.Instance.UpdatePanelInfo;//informacionSolicitud.Instance.UpdatePanelInfo;
                SubPesca.Solicitudes.Registrar.informacionSolicitud.Instance.RecargarInformacion();
                UpdatePanelInformacionSolictud.Update();

            }
            else
            {

                msSometimientoSEA.Text = "Ha ocurrido un error al ejecutar la acción solicitada.";
                PanelSometimientoSEA.Visible = true;
                UpdatePanelSometimientoSEA.Update();

            }



        }
    }
}