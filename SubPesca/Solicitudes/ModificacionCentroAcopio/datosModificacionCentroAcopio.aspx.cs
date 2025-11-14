using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.concesiones;
using System.Collections;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.modificacion;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Validaciones.cl.subpesca.rb.modificacion;
using SubPesca.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;

namespace SubPesca.Solicitudes.ModificacionCentroAcopio
{
    public partial class datosModificacionCentroAcopio : System.Web.UI.Page
    {
        SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();
        SolicitudDA solicitudDA = new SolicitudDA();

        DatosConcesionAcuiculturaValidacion datosConcesionAcuiculturaValidacion = new DatosConcesionAcuiculturaValidacion();
        PermisosService permisosService = new PermisosService();




        protected void setearModulo()
        {

            ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
            ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
            ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
            ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
            ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
            ViewState["solicitudSession"] = paginas.solicitudModificacionCentroAcopioSession;


            SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

            if (solicitudConcesion != null)
            {
                int[] tiposModificacion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                int contador = 0;
                foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                {
                    if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE)
                    {
                        tiposModificacion[contador] = rbSeccionUnidadEspacial.FICHA_MODIFICACION_CENTRO_DE_ACOPIO_AMPLIA_SUPERFICIE;
                        contador++;
                    }
                    if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_RENOVACION)
                    {
                        tiposModificacion[contador] = rbSeccionUnidadEspacial.FICHA_MODIFICACION_CENTRO_DE_ACOPIO_ESPECIE;
                        contador++;
                    }
                    if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_PT_ESPECIE)
                    {
                        tiposModificacion[contador] = rbSeccionUnidadEspacial.FICHA_MODIFICACION_CENTRO_DE_ACOPIO_PT;
                        contador++;
                    }
                    if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE)
                    {
                        tiposModificacion[contador] = rbSeccionUnidadEspacial.FICHA_MODIFICACION_CENTRO_DE_ACOPIO_REDUCE_SUPERFICIE;
                        contador++;
                    }
                    if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REGULARIZACION)
                    {
                        tiposModificacion[contador] = rbSeccionUnidadEspacial.FICHA_MODIFICACION_CENTRO_DE_ACOPIO_REGULARIZACION;
                        contador++;
                    }

                }
                ViewState["SECCION_ESPECIFICA"] = tiposModificacion;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                setearModulo();
                
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.EDITAR))
                {
                    SuperficieAmpReducRequerida.ReadOnly = false;
                    SuperficieTotalFinal.ReadOnly = false;
                    PanelGuardarSuperficie.Visible = true;

                }
                else
                {
                    SuperficieAmpReducRequerida.ReadOnly = true;
                    SuperficieTotalFinal.ReadOnly = true;
                    PanelGuardarSuperficie.Visible = false;
                }


                // Inicializamos el formulario
                Initialize_Form();
            }
        }

        private void Initialize_Form()
        {
            ConcesionService concesionService = new ConcesionService();
            SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();

            SolicitudConcesion solicitudModificacion = (SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

            if (solicitudModificacion == null)
            {
                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");

            }

            DespliegueMenuSeccionDA despliegueMenuSeccionDA = new DespliegueMenuSeccionDA();
            List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.DATOS_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_MOD_CENTRO_ACOPIO);

            /* Datos Generales */
            if (solicitudModificacionService.despligueSeccionesMenu(solicitudModificacion, despliegueMenuSeccionList, rbSeccion.DATOS_GENERALES))
            {
                IdSolicitud.Value = Convert.ToString(solicitudModificacion.idSolConcesion);
                TipoModificacion.Text = solicitudModificacion.tipoModificacionString();
                codigoCentroConcesion.Text = Convert.ToString(solicitudModificacion.tramiteModConcesion.centro.id);

                if (!solicitudModificacionService.tieneSoloTipoModificacionRegularizacion(solicitudModificacion))
                {
                    numPertConcesion.Text = solicitudModificacion.numPert.ToString();
                    fechaRecepcionTramiteConcesion.Text = solicitudModificacion.fechaRecepcion.ToString();
                    fechaIngresoTramiteConcesion.Text = solicitudModificacion.fechaIngresoTramite.ToString();

                    PanelDatosInicio.Visible = true;
                    UpdatePanelDatosInicio.Update();
                }

                PanelDatosGeneral.Visible = true;
                UpdatePanelDatosGeneral.Update();
            }

            /* Datos de la Concesión */
            if (solicitudModificacionService.despligueSeccionesMenu(solicitudModificacion, despliegueMenuSeccionList, rbSeccion.DATOS_DE_LA_CONCESION))
            {

                SolicitudConcesion solicitudConcesion = solicitudDA.Obtiene_UE_Existente(Convert.ToString(solicitudModificacion.tramiteModConcesion.centro.id), rbTipo.UNID_ESPACIAL_CENTRO_DE_ACOPIO);

                if (solicitudConcesion != null)
                {
                    codigoCentroConcesion.Text = Convert.ToString(solicitudConcesion.unidadEspacial.centrosDeCultivo.codigoCentro);
                    SuperficieConcesion.Text = Convert.ToString(solicitudConcesion.superficieCalculada);
                }
                PanelDatosConcesion.Visible = true;
                UpdatePanelDatosConcesion.Update();
            }

            /* Datos de la Solicitud */
            if (solicitudModificacionService.despligueSeccionesMenu(solicitudModificacion, despliegueMenuSeccionList, rbSeccion.DATOS_DE_LA_SOLICITUD))
            {

                SolicitudConcesion solicitudConcesion = solicitudDA.ObtieneSolicitudConcesionMod(solicitudModificacion.idSolConcesion, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                if (solicitudConcesion != null)
                {
                    SuperficieAmpReducRequerida.Text = Convert.ToString(solicitudConcesion.superficieTramReq);
                    SuperficieTotalFinal.Text = Convert.ToString(solicitudConcesion.superficieTramFinal);
                }
                PanelDatosSol.Visible = true;
                UpdatePanelDatosSol.Update();


            }

            /* Trámites Mod pendientes del mismo código origen */
            if (solicitudModificacionService.despligueSeccionesMenu(solicitudModificacion, despliegueMenuSeccionList, rbSeccion.TRAMITES_MODIFICACION_PENDIENTES))
            {
                List<SolicitudConcesion> solicitudConcesionList = solicitudModificacionService.ListarRequerimientosPendModificacionCodCentro(solicitudModificacion.tramiteModConcesion.centro.id, solicitudModificacion.idSolConcesion, rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_ACOPIO, rbTipo.UNID_ESPACIAL_MOD_CENTRO_ACOPIO);

                if (solicitudConcesionList != null && solicitudConcesionList.Count > 0)
                {
                    GridViewSolicitudesPendientes.PageIndex = 0;
                    GridViewSolicitudesPendientes.DataSource = solicitudConcesionList;
                    GridViewSolicitudesPendientes.DataBind();

                    PanelTramitesPendientes.Visible = true;
                    UpdatePanelTramitesPendientes.Update();
                }
            }

        }

        protected void GuardarSuperficieSolicitud_Click(object sender, ImageClickEventArgs e)
        {
            SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();

            SolicitudConcesion solicitudModificacion = new SolicitudConcesion();
            solicitudModificacion.idSolConcesion = Convert.ToInt32(IdSolicitud.Value);
            solicitudModificacion.numPert = numPertConcesion.Text;

            solicitudModificacion.superficieTramFinal = Convert.ToSingle(SuperficieTotalFinal.Text);
            solicitudModificacion.superficieTramReq = Convert.ToSingle(SuperficieAmpReducRequerida.Text);

            solicitudModificacion.tipoUnidadEspacial = new ParametroGenerico(rbTipo.UNID_ESPACIAL_CONCESION);
            solicitudModificacion.tipoTramite = new ParametroGenerico(rbTipo.TIPO_TRAMITE_MODIFICACION);

            List<String> listErroresSolicitud = datosConcesionAcuiculturaValidacion.validaSuperficies(solicitudModificacion);

            if (listErroresSolicitud.Count <= 0)
            {
                bool guardaSuperficieSolicitud = solicitudModificacionService.guarDatosAdicionalesModificacion(solicitudModificacion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                if (guardaSuperficieSolicitud)
                {
                    msgGrilla.Text = "Se han guardado las superficies a la solicitud de modificación concesión de acuicultura.";
                    Content_msgGrilla.Visible = true;
                }
                else
                {
                    msgGrilla.Text = "No se han guardado las superficies a la solicitud de modificación concesión de acuicultura.";
                    Content_msgGrilla.Visible = true;

                }
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            }
            else
            {
                foreach (String error in listErroresSolicitud)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
            }
        }

        protected void GridViewSolicitudesPendientes_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            SolicitudConcesion solicitudModificacion = (SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

            GridViewSolicitudesPendientes.PageIndex = e.NewPageIndex;
            GridViewSolicitudesPendientes.DataSource = solicitudModificacionService.ListarPequerimientosPendCodCentro(solicitudModificacion.tramiteModConcesion.centro.id, solicitudModificacion.idSolConcesion);
            GridViewSolicitudesPendientes.DataBind();

            PanelTramitesPendientes.Visible = true;
            UpdatePanelTramitesPendientes.Update();
        }

        protected void GridViewSolicitudesPendientes_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridViewSolicitudesPendientes = (GridView)sender;

                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Trámites Pendientes";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridViewSolicitudesPendientes.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        protected void GridViewSolicitudesPendientes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}