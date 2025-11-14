using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;
using Datos.Entidades;
using Datos.Contantes;
using Datos.Entidades.Relocalizacion;

namespace SubPesca.Solicitudes.RelocalizacionRESA
{
    public partial class erroresTramiteRelocalizacionRESA : System.Web.UI.Page
    {
        
        RelocalizacionRESAService relocalizacionRESAService = new RelocalizacionRESAService();
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
            if (!Page.IsPostBack)
            {

                if (Request.QueryString["idTramiteRel"] != null)
                {
                    TramiteRelocalizacion tramiteRelocalizacion = (TramiteRelocalizacion)relocalizacionRESAService.ObtenerTramiteRelocalizacionCompletoRESA(Convert.ToInt32(Request.QueryString["idTramiteRel"]));

                    if (tramiteRelocalizacion != null && tramiteRelocalizacion.idTramiteRel > 0)
                    {

                        //SI AUN NO ESTAN PROCESANDO SUS SECTORES, SE PUEDEN RECALCULAR LOS ERRORES
                        if (tramiteRelocalizacion.enTram == false)
                        {
                            relocalizacionRESAService.recalcularErroresRelocalizacionRESA(tramiteRelocalizacion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                        }

                        ViewState["TramiteRESA"] = tramiteRelocalizacion;
                        numeroPert.Text = Convert.ToString(tramiteRelocalizacion.numPert);

                        List<ErroresRelocalizacion> errores = relocalizacionRESAService.ListarErroresRelocalizacionRESA(tramiteRelocalizacion.idTramiteRel);


                        bool mostrarBotonGuardar = false;

                        if (errores.Count > 0)
                        {
                            foreach (ErroresRelocalizacion erroresRelocalizacion in errores)
                            {
                                if (erroresRelocalizacion.estadoError.id == rbEstadosGenerales.ERROR_AUN_NO_EVALUADO)
                                {
                                    mostrarBotonGuardar = true;
                                    break;
                                }
                            }
                        }


                        if (mostrarBotonGuardar)
                        {
                            panelBotonGuardar.Visible = true;
                        }


                        ViewState["erroresViewState"] = errores;

                        ListViewErrores.DataSource = (List<ErroresRelocalizacion>)errores;
                        ListViewErrores.DataBind();

                    }
                    else
                    {
                        Response.Redirect("~/Solicitudes/RelocalizacionRESA/administrarSolicitudRelocalizacionRESA.aspx");
                    }
                }
            }
        }



        //LISTVIEW 
        protected void ListViewErrores_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {


                //SI TIENEN RESPUESTA, ENTONCES SE DEBE MARCAR Y BLOQUEAR EL CHECK
                HiddenField hiddenEstadoErrorId = (HiddenField)e.Item.FindControl("HiddenEstadoErrorId");
                CheckBox check = (CheckBox)e.Item.FindControl("chkSeleccionado");


                TramiteRelocalizacion tramite = (TramiteRelocalizacion)ViewState["TramiteRESA"];

                if (hiddenEstadoErrorId.Value != null && Convert.ToInt32(hiddenEstadoErrorId.Value) > 0 && check != null && Convert.ToInt32(hiddenEstadoErrorId.Value) == rbEstadosGenerales.ERROR_IGNORADO)
                {
                    check.Checked = true;


                    //YA SE ESTA PROCESANDO, SE BLOQUEAN LOS CHECK
                    if (tramite.enTram == true)
                    {
                        check.Enabled = false;
                    }
                }

            }
        }


        protected void Guardar_Click(object sender, EventArgs e)
        {

            List<ErroresRelocalizacion> errores = (List<ErroresRelocalizacion>)ViewState["erroresViewState"];

            if (errores.Count() > 0)
            {
                foreach (ListViewDataItem item in ListViewErrores.Items)
                {

                    var check = item.FindControl("chkSeleccionado") as CheckBox;
                    var idError = item.FindControl("HiddenIdError") as HiddenField;

                    foreach (ErroresRelocalizacion erroresRelocalizacion in errores)
                    {
                        if (erroresRelocalizacion.idError == Convert.ToInt32(idError.Value))
                        {

                            if (check.Checked)
                            {
                                erroresRelocalizacion.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_IGNORADO);
                            }
                            else
                            {
                                erroresRelocalizacion.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                            }

                            break;
                        }

                    }
                }
            }

            if (relocalizacionRESAService.GuardarErroresRelocalizacionRESA(errores))
            {

            }

            //SE ENVIAR A VERIFICAR QUE ESTEN TODOS LOS CHECK CHEQUEADOS, SI ES ASI, SE PROCEDE A CREAR LOS SOLICITUDES POR CADA SECTOR
            TramiteRelocalizacion tramiteRelocalizacion = (TramiteRelocalizacion)ViewState["TramiteRESA"];

            if (relocalizacionRESAService.setearSolicitudesDelTramiteRESA(tramiteRelocalizacion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario))
            {


            }

            this.mensaje = "Acción Registrada con exito";
            Server.Transfer("~/Solicitudes/RelocalizacionRESA/administrarSolicitudRelocalizacionRESA.aspx");
            

        }



    }
}