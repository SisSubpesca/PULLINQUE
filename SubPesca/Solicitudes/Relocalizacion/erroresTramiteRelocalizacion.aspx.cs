using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades.Relocalizacion;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;
using Datos.Entidades;
using Datos.Contantes;

namespace SubPesca.Solicitudes.Relocalizacion
{
    public partial class erroresTramiteRelocalizacion : System.Web.UI.Page
    {

        RelocalizacionService relocalizacionService = new RelocalizacionService();
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
                    TramiteRelocalizacion tramiteRelocalizacion = (TramiteRelocalizacion)relocalizacionService.ObtenerTramiteRelocalizacionCompleto(Convert.ToInt32(Request.QueryString["idTramiteRel"]));

                    if (tramiteRelocalizacion != null && tramiteRelocalizacion.idTramiteRel > 0)
                    {

                        //SI AUN NO ESTAN PROCESANDO SUS SECTORES, SE PUEDEN RECALCULAR LOS ERRORES
                        if (tramiteRelocalizacion.enTram == false)
                        {
                            relocalizacionService.recalcularErroresRelocalizacion(tramiteRelocalizacion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                        }

                        ViewState["Tramite"] = tramiteRelocalizacion;
                        numeroPert.Text = Convert.ToString(tramiteRelocalizacion.numPert);

                        List<ErroresRelocalizacion> errores = relocalizacionService.ListarErroresRelocalizacion(tramiteRelocalizacion.idTramiteRel);


                        bool mostrarBotonGuardar = false;

                        if (errores.Count > 0) { 
                            foreach(ErroresRelocalizacion erroresRelocalizacion in errores){
                                if (erroresRelocalizacion.estadoError.id == rbEstadosGenerales.ERROR_AUN_NO_EVALUADO) {
                                    mostrarBotonGuardar = true;
                                    break;
                                }
                            }
                        }


                        if (mostrarBotonGuardar) {
                            panelBotonGuardar.Visible = true;
                        }


                        ViewState["erroresViewState"] = errores;

                        ListViewErrores.DataSource = (List<ErroresRelocalizacion>)errores;
                        ListViewErrores.DataBind();

                    }
                    else
                    {
                        Response.Redirect("~/Solicitudes/Relocalizacion/administrarSolicitudRelocalizacion.aspx");
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


                TramiteRelocalizacion tramite = (TramiteRelocalizacion)ViewState["Tramite"];

                if (hiddenEstadoErrorId.Value != null && Convert.ToInt32(hiddenEstadoErrorId.Value) > 0 && check != null && Convert.ToInt32(hiddenEstadoErrorId.Value) == rbEstadosGenerales.ERROR_IGNORADO)
                {
                    check.Checked = true;


                    //YA SE ESTA PROCESANDO, SE BLOQUEAN LOS CHECK
                    if (tramite.enTram == true) {
                        check.Enabled = false;
                    }
                }

            }
        }


        protected void Guardar_Click(object sender, EventArgs e) {

            List<ErroresRelocalizacion> errores = (List<ErroresRelocalizacion>)ViewState["erroresViewState"];

            if (errores.Count() > 0)
            {
                foreach (ListViewDataItem item in ListViewErrores.Items)
                {

                    var check = item.FindControl("chkSeleccionado") as CheckBox;
                    var idError = item.FindControl("HiddenIdError") as HiddenField;

                    foreach(ErroresRelocalizacion erroresRelocalizacion in errores){
                        if (erroresRelocalizacion.idError == Convert.ToInt32(idError.Value)) {

                            if (check.Checked)
                            {
                                erroresRelocalizacion.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_IGNORADO);
                            }
                            else {
                                erroresRelocalizacion.estadoError = new ParametroGenerico(rbEstadosGenerales.ERROR_AUN_NO_EVALUADO);
                            }

                            break;
                        }
                        
                    }
                }
            }

            if (relocalizacionService.GuardarErroresRelocalizacion(errores)) { 
                
            }

            //SE ENVIAR A VERIFICAR QUE ESTEN TODOS LOS CHECK CHEQUEADOS, SI ES ASI, SE PROCEDE A CREAR LOS SOLICITUDES POR CADA SECTOR
            TramiteRelocalizacion tramiteRelocalizacion = (TramiteRelocalizacion)ViewState["Tramite"];

            if (relocalizacionService.setearSolicitudesDelTramite(tramiteRelocalizacion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario))
            { 
            
            
            }

            this.mensaje = "Acción Registrada con exito";
            Server.Transfer("~/Solicitudes/Relocalizacion/administrarSolicitudRelocalizacion.aspx");
            //Response.Redirect("~/Solicitudes/Relocalizacion/administrarSolicitudRelocalizacion.aspx");

        }



    }
}