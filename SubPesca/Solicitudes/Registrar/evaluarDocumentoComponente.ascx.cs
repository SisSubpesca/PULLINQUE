using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using Datos.Contantes;
using Datos.Utilidades;



namespace SubPesca.Solicitudes.Registrar
{
    public partial class evaluarDocumentoComponente : System.Web.UI.UserControl
    {
    

        RequerimientoService requerimientoService = new RequerimientoService();
        Funciones funciones = new Funciones();


        protected void setearModulo(int Origen)
        {
            if (funciones.retornaModulo().Equals("Registrar"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLCONCESION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLCONCESION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLCONCESION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLCONCESION;
                ViewState["URL_INFORMES_RESOLUCIONES"] = paginas.URL_INGRESO_DOCUMENTO_SOLCONCESION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLCONCESION;

                if (Origen == 1) 
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_SOLCONCESION;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_SOLCONCESION;
                }
                else 
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLCONCESION;
                }

                
                ViewState["solicitudSession"] = paginas.solicitudConcesionSession;
            }
            else if (funciones.retornaModulo().Equals("Relocalizacion"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_RELOCALIZACION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_RELOCALIZACION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_RELOCALIZACION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_RELOCALIZACION;
                ViewState["URL_INFORMES_RESOLUCIONES"] = paginas.URL_INGRESO_DOCUMENTO_RELOCALIZACION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_RELOCALIZACION;
                

                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_RELOCALIZACION;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_RELOCALIZACION;
                }
                else
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_RELOCALIZACION;
                }

                ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSession;
            }
            else if (funciones.retornaModulo().Equals("RelocalizacionRESA"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_RELOCALIZACION_RESA;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_RELOCALIZACION_RESA;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_RELOCALIZACION_RESA;
                ViewState["URL_INFORMES_RESOLUCIONES"] = paginas.URL_INGRESO_DOCUMENTO_RELOCALIZACION_RESA;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_RELOCALIZACION_RESA;


                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_RELOCALIZACION_RESA;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_RELOCALIZACION_RESA;
                }
                else
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_RELOCALIZACION_RESA;
                }

                ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSessionRESA;
            }
            else if (funciones.retornaModulo().Equals("Acopio"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_ACOPIO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_ACOPIO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_ACOPIO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_ACOPIO;
                ViewState["URL_INFORMES_RESOLUCIONES"] = paginas.URL_INGRESO_DOCUMENTO_CENTRO_DE_ACOPIO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_CENTRO_DE_ACOPIO;
                

                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_CENTRO_DE_ACOPIO;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_CENTRO_DE_ACOPIO;
                }
                else
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_CENTRO_DE_ACOPIO;
                }

                ViewState["solicitudSession"] = paginas.solicitudAcopioSession;
            }
            else if (funciones.retornaModulo().Equals("Faenamiento"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_INFORMES_RESOLUCIONES"] = paginas.URL_INGRESO_DOCUMENTO_CENTRO_DE_FAENAMIENTO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_CENTRO_DE_FAENAMIENTO;
                

                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_CENTRO_DE_FAENAMIENTO;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_CENTRO_DE_FAENAMIENTO;
                }
                else
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_CENTRO_DE_FAENAMIENTO;
                }


                ViewState["solicitudSession"] = paginas.solicitudFaenamientoSession;
            }
            else if (funciones.retornaModulo().Equals("Amerb"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_AMERB;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_AMERB;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_AMERB;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_AMERB;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_AMERB;
                

                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_SOLICITUD_AMERB;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_SOLICITUD_AMERB;
                }
                else
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_AMERB;
                }


                ViewState["solicitudSession"] = paginas.solicitudAmerbSession;

            }
            else if (funciones.retornaModulo().Equals("ExperimentalesAmerb"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_EXPERIMENTALES_AMERB;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_EXPERIMENTALES_AMERB;


                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_SOLICITUD_EXPERIMENTALES_AMERB;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_SOLICITUD_EXPERIMENTALES_AMERB;
                }
                else
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_EXPERIMENTALES_AMERB;
                }


                ViewState["solicitudSession"] = paginas.solicitudExperimentalesAmerbSession;

            }
            else if (funciones.retornaModulo().Equals("ECMPO"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_ECMPO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_ECMPO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_ECMPO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_ECMPO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_ECMPO;


                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_SOLICITUD_ECMPO;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_SOLICITUD_ECMPO;
                }
                else
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_ECMPO;
                }


                ViewState["solicitudSession"] = paginas.solicitudECMPOSession;

            }
            else if (funciones.retornaModulo().Equals("ExperimentalesConcesion"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_EXPERIMENTALES_CONCESION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_EXPERIMENTALES_CONCESION;


                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_SOLICITUD_EXPERIMENTALES_CONCESION;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_SOLICITUD_EXPERIMENTALES_CONCESION;
                }
                else
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_EXPERIMENTALES_CONCESION;
                }


                ViewState["solicitudSession"] = paginas.solicitudExperimentalesConcesionSession;

            }
            else if (funciones.retornaModulo().Equals("Colector"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_COLECTORES_SEMILLA;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_COLECTORES_SEMILLA;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_COLECTORES_SEMILLA;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_COLECTORES_SEMILLA;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_COLECTORES_SEMILLA;
                

                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_COLECTORES_SEMILLA;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_COLECTORES_SEMILLA;
                }
                else
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_COLECTORES_SEMILLA;
                }


                ViewState["solicitudSession"] = paginas.solicitudColectorSession;


            }
            else if (funciones.retornaModulo().Equals("ModificacionAmerb"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_AMERB;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_AMERB;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_AMERB;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_AMERB;
                ViewState["URL_INFORMES_RESOLUCIONES"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_AMERB;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_AMERB;


                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_SOLICITUD_MODIFICACION_AMERB;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_SOLICITUD_MODIFICACION_AMERB;
                }
                else
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_AMERB;
                }


                ViewState["solicitudSession"] = paginas.solicitudModificacionAmerbSession;
            }

            else if (funciones.retornaModulo().Equals("ModificacionCentroAcopio"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["URL_INFORMES_RESOLUCIONES"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;


                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                }
                else
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                }


                ViewState["solicitudSession"] = paginas.solicitudModificacionCentroAcopioSession;
            }
            else if (funciones.retornaModulo().Equals("ModificacionCentroFaenamiento"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["URL_INFORMES_RESOLUCIONES"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;


                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                }
                else
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                }


                ViewState["solicitudSession"] = paginas.solicitudModificacionCentroFaenamientoSession;
            }
            else if (funciones.retornaModulo().Equals("ModificacionECMPO"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["URL_INFORMES_RESOLUCIONES"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_ECMPO;


                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_SOLICITUD_MODIFICACION_ECMPO;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_SOLICITUD_MODIFICACION_ECMPO;
                }
                else
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLICITUD_MODIFICACION_ECMPO;
                }


                ViewState["solicitudSession"] = paginas.solicitudModificacionECMPOSession;
            }

            else

            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLMOD;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLMOD;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLMOD;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLMOD;
                ViewState["URL_INFORMES_RESOLUCIONES"] = paginas.URL_INGRESO_DOCUMENTO_SOLMOD;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLMOD;
                

                if (Origen == 1)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_REFERENCIA_GLOBAL_SOLMOD;
                }
                else if (Origen == 2)
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_ANTECEDENTES_SECTOR_SOLMOD;
                }
                else
                {
                    ViewState["URL_ORIGEN"] = paginas.URL_INGRESO_DOCUMENTO_SOLMOD;
                }


                ViewState["solicitudSession"] = paginas.solicitudModificacionSession;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            // PAGE LOAD
            if (!Page.IsPostBack)
            {


                //origen
                //Origen = 0 -> Informes
                //Origen = 1 -> General
                //Origen = 2 -> Inspeccion Terreno

                int Origen = 0;

                if (Request.QueryString["Origen"] != null) {
                    Origen = Convert.ToInt32(Request.QueryString["Origen"]);
                }

                setearModulo(Origen);


                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion == null)
                {
                    Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
                }


                IdSolicitud.Text = Convert.ToString(solicitudConcesion.idSolConcesion);
                

                if (Request.QueryString["idRequerimiento"] != null)
                {


                    int idTipoFlujoDocumental = requerimientoService.ObtieneFlujoDocumentoGeneral(Convert.ToInt32(Request.QueryString["idRequerimiento"]));

                    Requerimiento requerimiento = null;

                    if (idTipoFlujoDocumental == rbTipo.SALIDA)
                    {
                        requerimiento = requerimientoService.ObtenerRequerimientoSinEstado(Convert.ToInt32(Request.QueryString["idRequerimiento"]));
                    }
                    if (idTipoFlujoDocumental == rbTipo.ENTRADA)
                    {
                        requerimiento = requerimientoService.ObtenerRespuesta(Convert.ToInt32(Request.QueryString["idRequerimiento"]));
                    }

                    if (requerimiento != null)
                    {
                        IdRequerimiento.Text = Convert.ToString(requerimiento.idRequerimiento);


                        if (requerimiento.estadoFinal != null && requerimiento.estadoFinal.id == rbEstadosGenerales.CONFORME)
                        {
                            PanelConforme.Visible = false;
                            UpdatePanelConforme.Update();

                        }

                        if (requerimiento.estadoFinal != null && requerimiento.estadoFinal.id == rbEstadosGenerales.NO_CONFORME)
                        {
                            PanelNoConfome.Visible = false;
                            UpdatePanelNoConfome.Update();
                        }

                    }
                }
            }
        }

        protected void Conforme_Click(object sender, EventArgs e)
        {

            SolicitudConcesion solicitudRelocalizacion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

            if (solicitudRelocalizacion == null)
            {
                Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
            }

            if (requerimientoService.ActualizarRequerimientoEstadoFinal(Convert.ToInt32(IdRequerimiento.Text), rbEstadosGenerales.CONFORME, solicitudRelocalizacion.idSolConcesion))
            {
                PanelNoConfome.Visible = true;
                UpdatePanelNoConfome.Update();

                PanelConforme.Visible = false;
                UpdatePanelConforme.Update();

                Mensajes.Text = "Documento Actualizado con exito.";
                UpdatePanelMensajes.Update();


                //RECARGANDO LA INFORMACION DE LA SOLICITUD, POR SI CAMBIO EL ESTADO
                UpdatePanel UpdatePanelInformacionSolictud = informacionSolicitud.Instance.UpdatePanelInfo;
                informacionSolicitud.Instance.RecargarInformacion();
                UpdatePanelInformacionSolictud.Update();
            }
            else
            {


                Mensajes.Text = "Ha ocurrido un error al actualizar el documento.";
                UpdatePanelMensajes.Update();

            }



        }

        protected void NoConfome_Click(object sender, EventArgs e)
        {
            SolicitudConcesion solicitudRelocalizacion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

            if (solicitudRelocalizacion == null)
            {
                Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
            }


            if (requerimientoService.ActualizarRequerimientoEstadoFinal(Convert.ToInt32(IdRequerimiento.Text), rbEstadosGenerales.NO_CONFORME, solicitudRelocalizacion.idSolConcesion))
            {

                PanelNoConfome.Visible = false;
                UpdatePanelNoConfome.Update();

                PanelConforme.Visible = true;
                UpdatePanelConforme.Update();

                Mensajes.Text = "Documento Actualizado con exito.";
                UpdatePanelMensajes.Update();

                //RECARGANDO LA INFORMACION DE LA SOLICITUD, POR SI CAMBIO EL ESTADO
                UpdatePanel UpdatePanelInformacionSolictud = informacionSolicitud.Instance.UpdatePanelInfo;
                informacionSolicitud.Instance.RecargarInformacion();
                UpdatePanelInformacionSolictud.Update();
            }
            else
            {

                Mensajes.Text = "Ha ocurrido un error al actualizar el documento.";
                UpdatePanelMensajes.Update();
            }



        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(ViewState["URL_ORIGEN"].ToString());

        }
    }
}