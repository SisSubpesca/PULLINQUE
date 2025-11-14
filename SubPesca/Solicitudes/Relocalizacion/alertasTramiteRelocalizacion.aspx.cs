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
    public partial class alertasTramiteRelocalizacion : System.Web.UI.Page
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

                        //SI AUN NO ESTAN PROCESANDO SUS SECTORES, SE PUEDEN RECALCULAR SUS ALERTAS
                        if (tramiteRelocalizacion.despliegaAlertas == true)
                        {
                            relocalizacionService.recalcularAlertasRelocalizacion(tramiteRelocalizacion);
                        }

                        ViewState["Tramite"] = tramiteRelocalizacion;
                        numeroPert.Text = Convert.ToString(tramiteRelocalizacion.numPert);

                        List<ErroresRelocalizacion> alertas = relocalizacionService.ListarAlertasRelocalizacion(tramiteRelocalizacion.idTramiteRel);


                        if (alertas == null || alertas.Count() == 0)
                        {
                            mensajeCeroAlertas.Text = "No se registran alertas para el trámite seleccionado";
                        }

                        ViewState["erroresViewState"] = alertas;

                        ListViewErrores.DataSource = (List<ErroresRelocalizacion>)alertas;
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


            }
        }


       


    }
}