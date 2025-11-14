using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;
using Datos.Entidades.Relocalizacion;
using Datos.Entidades;

namespace SubPesca.Solicitudes.RelocalizacionRESA
{
    public partial class alertasTramiteRelocalizacionRESA : System.Web.UI.Page
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

                        //SI AUN NO ESTAN PROCESANDO SUS SECTORES, SE PUEDEN RECALCULAR SUS ALERTAS
                        if (tramiteRelocalizacion.despliegaAlertas == true)
                        {
                            relocalizacionRESAService.recalcularAlertasRelocalizacionRESA(tramiteRelocalizacion);
                        }

                        ViewState["TramiteRESA"] = tramiteRelocalizacion;
                        numeroPert.Text = Convert.ToString(tramiteRelocalizacion.numPert);

                        List<ErroresRelocalizacion> alertas = relocalizacionRESAService.ListarAlertasRelocalizacionRESA(tramiteRelocalizacion.idTramiteRel);


                        if (alertas == null || alertas.Count() == 0)
                        {
                            mensajeCeroAlertas.Text = "No se registran alertas para el trámite seleccionado";
                        }

                        ViewState["erroresViewStateRESA"] = alertas;

                        ListViewErrores.DataSource = (List<ErroresRelocalizacion>)alertas;
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


            }
        }





    }
}