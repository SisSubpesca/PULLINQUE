using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using Datos.Utilidades;
using Datos.Contantes;
using Datos.Entidades.Relocalizacion;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.servicios.concesiones;

namespace SubPesca.Solicitudes.Relocalizacion
{
    public partial class datosTramiteRelocalizacion : System.Web.UI.Page
    {

        Funciones funciones = new Funciones();
        RelocalizacionService relocalizacionService = new RelocalizacionService();
        PermisosService permisosService = new PermisosService();
        ConcesionService concesionService = new ConcesionService();


        protected void setearModulo()
        {
            if (funciones.retornaModulo().Equals("Registrar"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLCONCESION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLCONCESION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLCONCESION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLCONCESION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLCONCESION;
                ViewState["solicitudSession"] = paginas.solicitudConcesionSession;
            }
            else if (funciones.retornaModulo().Equals("Relocalizacion"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_RELOCALIZACION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_RELOCALIZACION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_RELOCALIZACION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_RELOCALIZACION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_RELOCALIZACION;
                ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSession;
            }
            else
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLMOD;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLMOD;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLMOD;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLMOD;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLMOD;
                ViewState["solicitudSession"] = paginas.solicitudModificacionSession;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // PAGE LOAD
            if (!Page.IsPostBack)
            {

                setearModulo();



                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion == null)
                {
                    Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
                }


                //SECTOR
                DetalleSector aDetalleSector = relocalizacionService.obtenerDetalleSector_Solicitud(solicitudConcesion.idSolConcesion);
                ViewState["aDetalleSector"] = aDetalleSector;



                numPertSector.Text = aDetalleSector.numPert;
                identificadorDelSector.Text = Convert.ToString(aDetalleSector.numSector);
                fechaRecepcionTramiteSector.Text = Convert.ToString(solicitudConcesion.fechaRecepcion);
                fechaIngresoTramiteSector.Text = Convert.ToString(solicitudConcesion.fechaIngresoTramite);
                tipoRelocalizacion.Text = aDetalleSector.tipoRelocalizacion.descripcion;
                superficieSector.Text = Convert.ToString(aDetalleSector.superficieSector);


                ViewState["Origenes_Asociados"] = aDetalleSector.origenes;

                GridOrigen.DataSource = aDetalleSector.origenes;
                GridOrigen.DataBind();


                if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA) {

                    codigoSiepCentroDestino.Text = Convert.ToString(aDetalleSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro);
                    nombreTitularDestino.Text = aDetalleSector.concesionDestino.DescripcionTitularesComa;
                    sectorDestino.Text = aDetalleSector.concesionDestino.DescripcionToponimios;
                    comunaDestino.Text = aDetalleSector.concesionDestino.DescripcionComuna;
                    regionDestino.Text = aDetalleSector.concesionDestino.region.descripcion;
                    acDestino.Text = aDetalleSector.concesionDestino.barrio.barrio;
                    tipoCentroDestino.Text = aDetalleSector.concesionDestino.tipoUnidadEspacial.descripcion;
                    superficieTotalCentroDestino.Text = Convert.ToString(aDetalleSector.concesionDestino.superficieCalculada);
                    PanelDestino.Visible = true;
                
                }


                /*

                if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA)
                {

                    codigoSiepCentroDestino.Text = Convert.ToString(aDetalleSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro);
                    nombreTitularDestino.Text = aDetalleSector.concesionDestino.DescripcionTitularesComa;
                    sectorDestino.Text = aDetalleSector.concesionDestino.DescripcionToponimios;
                    comunaDestino.Text = aDetalleSector.concesionDestino.DescripcionComuna;
                    regionDestino.Text = aDetalleSector.concesionDestino.region.descripcion;
                    acDestino.Text = aDetalleSector.concesionDestino.barrio.barrio;
                    tipoCentroDestino.Text = aDetalleSector.concesionDestino.tipoUnidadEspacial.descripcion;
                    superficieTotalCentroDestino.Text = Convert.ToString(aDetalleSector.concesionDestino.superficieCalculada);
                    PanelDestino.Visible = true;

                }*/


            }
        }

       




    }
}