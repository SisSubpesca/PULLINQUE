using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.concesiones;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using Validaciones.cl.subpesca.rb.relocalizacion;
using LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;
using Datos.Entidades;
using Datos.Entidades.Relocalizacion;
using Datos.Contantes;
using SubPesca.Utilidades;
using System.Collections;

namespace SubPesca.Solicitudes.RelocalizacionRESA
{
    public partial class ingresarSolicitudRelocalizacionRESA : System.Web.UI.Page
    {
        ConcesionService concesionService = new ConcesionService();
        PermisosService permisosService = new PermisosService();
        TramiteRelocalizacionRESAValidacion tramiteRelocalizacionRESAValidacion = new TramiteRelocalizacionRESAValidacion();
        RelocalizacionRESAService relocalizacionRESAService = new RelocalizacionRESAService();
        String mensaje = "";

        EnviarCorreo enviarCorreo = new EnviarCorreo();

        public String MensajeRegistro
        {
            get
            {
                return mensaje;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            MensajeSuperior.Text = "";
            PanelMensajeSuperior.Visible = false;
            UpdatePanelMensajeSuperior.Update();


            if (!Page.IsPostBack)
            {

                List<ParametroGenerico> resp = relocalizacionRESAService.ListarPreferenciaRelocalizacionRESA();

                if (resp != null)
                {
                    foreach (ParametroGenerico item in resp)
                    {
                        preferencias.Items.Add(new ListItem(item.descripcion, Convert.ToString(item.id)));
                    }
                }

                preferencias.DataBind();

                TramiteRelocalizacion nuevoTramiteRelocalizacion = (TramiteRelocalizacion)Session["nuevoTramiteRelocalizacionRESA"];

                //VIENE DESDE UNA MODIFICACION DE TRAMITE DE RELOCALIZACION
                if (Request.QueryString["idTramiteRel"] != null)
                {
                    TramiteRelocalizacion tramiteRelocalizacion = (TramiteRelocalizacion)relocalizacionRESAService.ObtenerTramiteRelocalizacionCompletoRESA(Convert.ToInt32(Request.QueryString["idTramiteRel"]));

                    if (tramiteRelocalizacion != null && tramiteRelocalizacion.idTramiteRel > 0)
                    {
                        ViewState["TramiteRESA"] = tramiteRelocalizacion;
                        numeroPert.Text = Convert.ToString(tramiteRelocalizacion.numPert);

                        ViewState["Sectores_TramiteRESA"] = (List<DetalleSector>)tramiteRelocalizacion.sectores;
                        GridSectores.DataSource = tramiteRelocalizacion.sectores;
                        GridSectores.DataBind();
                        UpdatePanelGridSectores.Update();
                    }
                    else
                    {
                        Response.Redirect("~/Solicitudes/RelocalizacionRESA/administrarSolicitudRelocalizacionRESA.aspx");
                    }

                } //ES UN NUEVO TRAMITE DE RELOCALIZACION
                else if (nuevoTramiteRelocalizacion != null)
                {

                    ViewState["TramiteRESA"] = nuevoTramiteRelocalizacion;
                    numeroPert.Text = Convert.ToString(nuevoTramiteRelocalizacion.numPert);

                    ViewState["Sectores_TramiteRESA"] = (List<DetalleSector>)nuevoTramiteRelocalizacion.sectores;
                    GridSectores.DataSource = nuevoTramiteRelocalizacion.sectores;
                    GridSectores.DataBind();
                    UpdatePanelGridSectores.Update();

                }
                else
                {
                    Response.Redirect("~/Solicitudes/RelocalizacionRESA/administrarSolicitudRelocalizacionRESA.aspx");
                }


            }
        }


        //SECTOR CERO
        protected void esSectorCeroOpcionChecked(object sender, EventArgs e)
        {

            this.LimpiarDatosCentroSectorCero();
            this.LimpiarFormularioOrigen();
            this.LimpiarFormularioDestino();

            CodigoSiepSectorCero.Text = "";
            HectareasSectorCero.Text = "";
            UpdatePanelSectorCero.Update();


            if (esSectorCeroOpcion1.Checked)
            {
                PanelSectorCero.Visible = true;
                UpdatePanelSectorCero.Update();

                PanelOrigen.Visible = false;
                UpdatePanelOrigen.Update();

                PanelDestino.Visible = false;
                UpdatePanelDestino.Update();
            }
            else
            {

                PanelSectorCero.Visible = false;
                UpdatePanelSectorCero.Update();

                PanelOrigen.Visible = true;
                UpdatePanelOrigen.Update();

                PanelDestino.Visible = true;
                UpdatePanelDestino.Update();

            }

        }

        //DESTINO
        protected void tipoRelocalizacionOpcionChecked(object sender, EventArgs e)
        {

            LimpiarDestino();

            if (opcionCrea.Checked)
            {
                PanelCentroDestino.Visible = false;
                UpdatePanelCentroDestino.Update();

            }
            else
            {
                PanelCentroDestino.Visible = true;
                UpdatePanelCentroDestino.Update();
            }
        }

        protected void LimpiarDestino()
        {

            codigoSiepCentroDestino.Text = "";
            UpdatePanelCentroDestino.Update();

            LimpiarDatosCentroDestino();

        }

        protected void LimpiarDatosCentroDestino()
        {
            nombreTitularDestino.Text = "";
            UpdatePanelNombreTitularDestino.Update();


            sectorDestino.Text = "";
            comunaDestino.Text = "";
            regionDestino.Text = "";
            acDestino.Text = "";
            tipoCentroDestino.Text = "";
            superficieTotalCentroDestino.Text = "";
            informeRelocalizacionDestinoRESA.Text = "";

            PanelDatosCentroDestino.Visible = false;
            UpdatePanelDatosCentroDestino.Update();
        }

        //ORIGEN
        protected void CodigoSiepOrigen_OnTextChanged(object sender, EventArgs e)
        {

            string codigoSiep = "";


            LimpiarDatosCentroOrigen();

            try
            {
                SolicitudConcesion centrosDeCultivo = null;

                if (!CodigoSiepOrigen.Text.Trim().Equals(""))
                {
                    codigoSiep = Convert.ToString(CodigoSiepOrigen.Text.Split(' ')[0]);
                    centrosDeCultivo = concesionService.ObtieneConcesionExistente(Convert.ToString(codigoSiep), rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);

                    MostrarPreferenciasOrigen(codigoSiep);

                }


                if (centrosDeCultivo != null)
                {
                    nombreTitularOrigen.Text = centrosDeCultivo.DescripcionTitularesComa;
                    UpdatePanelNombreTitularOrigen.Update();

                    sectorOrigen.Text = centrosDeCultivo.DescripcionToponimios;
                    comunaOrigen.Text = centrosDeCultivo.DescripcionComuna;

                    if (centrosDeCultivo.region != null)
                    {
                        regionOrigen.Text = centrosDeCultivo.region.descripcion;
                    }

                    if (centrosDeCultivo.barrio != null)
                    {
                        acOrigen.Text = centrosDeCultivo.barrio.barrio;
                    }

                    if (centrosDeCultivo.tipoUnidadEspacial != null)
                    {
                        tipoCentroOrigen.Text = centrosDeCultivo.tipoUnidadEspacial.descripcion;
                    }
                    superficieTotalCentroOrigen.Text = Convert.ToString(centrosDeCultivo.superficieCalculada);
                    superficieCultivoTotalCentroOrigen.Text = Convert.ToString(centrosDeCultivo.superficieCalculadaCultivo);

                    informeRelocalizacionOrigenRESA.Text = relocalizacionRESAService.mostrarInformesRESA(Convert.ToString(codigoSiep));

                    PanelDatosCentroOrigen.Visible = true;
                    UpdatePanelDatosCentroOrigen.Update();
                }
                else
                {
                    PanelDatosCentroOrigen.Visible = false;
                    UpdatePanelDatosCentroOrigen.Update();
                }

            }
            catch (Exception)
            {
                PanelDatosCentroOrigen.Visible = false;
                UpdatePanelDatosCentroOrigen.Update();
            }
        }

        //ORIGEN
        protected void MostrarPreferenciasOrigen(string codigoSiep)
        {

            List<DetalleSector> sectores = (List<DetalleSector>)ViewState["Sectores_TramiteRESA"];
            bool salir = false;

            if (sectores != null)
            {

                foreach (DetalleSector aDetalleSector in sectores)
                {
                    if (aDetalleSector.accion == accion.INGRESAR || aDetalleSector.accion == accion.LISTADO || aDetalleSector.accion == accion.MODIFICAR)
                    {
                        if (aDetalleSector.origenes != null)
                        {
                            foreach (OrigenSector aOrigenSector in aDetalleSector.origenes)
                            {
                                if (aOrigenSector.preferencias != null)
                                {
                                    if (aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro == codigoSiep && (aOrigenSector.accion == accion.INGRESAR || aOrigenSector.accion == accion.LISTADO || aOrigenSector.accion == accion.MODIFICAR))
                                    {
                                        foreach (ListItem listItem in preferencias.Items)
                                        {
                                            foreach (ParametroGenerico aPreferencia in aOrigenSector.preferencias)
                                            {
                                                if (listItem.Value.Equals(Convert.ToString(aPreferencia.id)))
                                                {
                                                    listItem.Selected = true;
                                                }
                                            }
                                        }

                                        salir = true;
                                        break;
                                    }

                                }
                            }
                        }
                    }

                    if (salir)
                    {
                        break;
                    }
                }
            }
        }

        //SECTOR CERO
        protected void CodigoSiepSectorCero_OnTextChanged(object sender, EventArgs e)
        {

            int codigoSiep = 0;


            LimpiarDatosCentroSectorCero();

            try
            {
                codigoSiep = Convert.ToInt32(CodigoSiepSectorCero.Text.Split(' ')[0]);
                SolicitudConcesion centrosDeCultivo = concesionService.ObtieneConcesionExistente(Convert.ToString(codigoSiep), rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);

                if (centrosDeCultivo != null)
                {

                    nombreTitularSectorCero.Text = centrosDeCultivo.DescripcionTitularesComa;
                    UpdatePanelNombreTitularSectorCero.Update();

                    sectorSectorCero.Text = centrosDeCultivo.DescripcionToponimios;
                    comunaSectorCero.Text = centrosDeCultivo.DescripcionComuna;

                    if (centrosDeCultivo.region != null)
                    {
                        regionSectorCero.Text = centrosDeCultivo.region.descripcion;
                    }

                    if (centrosDeCultivo.barrio != null)
                    {
                        acSectorCero.Text = centrosDeCultivo.barrio.barrio;
                    }

                    if (centrosDeCultivo.tipoUnidadEspacial != null)
                    {
                        tipoCentroSectorCero.Text = centrosDeCultivo.tipoUnidadEspacial.descripcion;
                    }
                    superficieTotalCentroSectorCero.Text = Convert.ToString(centrosDeCultivo.superficieCalculada);
                    superficieCultivoTotalCentroSectorCero.Text = Convert.ToString(centrosDeCultivo.superficieCalculadaCultivo);


                    informeRelocalizacionRESA.Text = relocalizacionRESAService.mostrarInformesRESA(Convert.ToString(codigoSiep));
                    

                    PanelDatosCentroSectorCero.Visible = true;
                    UpdatePanelDatosCentroSectorCero.Update();
                }
                else
                {
                    PanelDatosCentroSectorCero.Visible = false;
                    UpdatePanelDatosCentroSectorCero.Update();
                }

            }
            catch (Exception)
            {
                PanelDatosCentroSectorCero.Visible = false;
                UpdatePanelDatosCentroSectorCero.Update();
            }
        }

        //DESTINO
        protected void CodigoSiepDestino_OnTextChanged(object sender, EventArgs e)
        {

            int codigoSiep = 0;


            LimpiarDatosCentroDestino();

            try
            {
                codigoSiep = Convert.ToInt32(codigoSiepCentroDestino.Text.Split(' ')[0]);
                SolicitudConcesion centrosDeCultivo = concesionService.ObtieneConcesionExistente(Convert.ToString(codigoSiep), rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);

                if (centrosDeCultivo != null)
                {

                    nombreTitularDestino.Text = centrosDeCultivo.DescripcionTitularesComa;
                    UpdatePanelNombreTitularDestino.Update();

                    sectorDestino.Text = "";
                    comunaDestino.Text = centrosDeCultivo.DescripcionComuna;

                    if (centrosDeCultivo.region != null)
                    {
                        regionDestino.Text = centrosDeCultivo.region.descripcion;
                    }

                    if (centrosDeCultivo.barrio != null)
                    {
                        acDestino.Text = centrosDeCultivo.barrio.barrio;
                    }

                    if (centrosDeCultivo.tipoUnidadEspacial != null)
                    {
                        tipoCentroDestino.Text = centrosDeCultivo.tipoUnidadEspacial.descripcion;
                    }
                    superficieTotalCentroDestino.Text = Convert.ToString(centrosDeCultivo.superficieCalculada);


                    informeRelocalizacionDestinoRESA.Text = relocalizacionRESAService.mostrarInformesRESA(Convert.ToString(codigoSiep));

                    PanelDatosCentroDestino.Visible = true;
                    UpdatePanelDatosCentroDestino.Update();
                }
                else
                {
                    PanelDatosCentroDestino.Visible = false;
                    UpdatePanelDatosCentroDestino.Update();
                }

            }
            catch (Exception)
            {
                PanelDatosCentroDestino.Visible = false;
                UpdatePanelDatosCentroDestino.Update();
            }
        }

        //SECTOR CERO
        protected void LimpiarDatosCentroSectorCero()
        {

            nombreTitularSectorCero.Text = "";
            sectorSectorCero.Text = "";
            comunaSectorCero.Text = "";
            regionSectorCero.Text = "";
            acSectorCero.Text = "";
            tipoCentroSectorCero.Text = "";
            superficieTotalCentroSectorCero.Text = "";
            informeRelocalizacionRESA.Text = "";

            PanelDatosCentroSectorCero.Visible = false;
            UpdatePanelDatosCentroSectorCero.Update();
        }

        //SECTOR CERO
        protected void LimpiarFormularioSectorCero()
        {

            LimpiarDatosCentroSectorCero();

            esSectorCeroOpcion1.Checked = false;
            esSectorCeroOpcion2.Checked = false;
            UpdatePanelCheck.Update();


            CodigoSiepSectorCero.Text = "";
            HectareasSectorCero.Text = "";
            PanelSectorCero.Visible = false;
            UpdatePanelSectorCero.Update();
        }

        protected void LimpiarFormularioDestino()
        {

            this.LimpiarDestino();
            opcionCrea.Checked = false;
            opcionFusiona.Checked = false;
            UpdatePanelDestino.Update();

        }

        private int obtenerNumeroSector(DetalleSector nuevoSector)
        {


            int numeroSector = 1;

            if (nuevoSector.esSectorCero)
            {
                numeroSector = 0;
            }
            else
            {
                numeroSector = 1;
            }


            List<DetalleSector> sectores = (List<DetalleSector>)ViewState["Sectores_TramiteRESA"];


            if (sectores != null && sectores.Count > 0)
            {

                Hashtable sectoresOcupados = new Hashtable();

                foreach (DetalleSector aDetalleSector in sectores)
                {
                    if (aDetalleSector.accion == accion.INGRESAR || aDetalleSector.accion == accion.MODIFICAR || aDetalleSector.accion == accion.LISTADO)
                    {
                        sectoresOcupados.Add(aDetalleSector.numSector, aDetalleSector.numSector);
                    }
                }


                while (numeroSector < 1000)
                {

                    if (!sectoresOcupados.ContainsKey(numeroSector))
                    {
                        break;
                    }
                    numeroSector++;
                }

            }

            return numeroSector;
        }

        private float sumarHectareasOrigen(List<OrigenSector> origenes)
        {
            //SUMAR LAS HECTAREAS DE LOS ORIGENES
            float sumatoriaHectareas = 0;
            if (origenes != null)
            {
                foreach (OrigenSector aOrigenSector in origenes)
                {
                    if (aOrigenSector.accion == accion.INGRESAR || aOrigenSector.accion == accion.MODIFICAR || aOrigenSector.accion == accion.LISTADO)
                    {
                        sumatoriaHectareas = sumatoriaHectareas + aOrigenSector.superficieRelocalizada;
                    }
                }
            }
            return sumatoriaHectareas;
        }

        protected void LimpiarFormulario(object sender, EventArgs e)
        {

            this.LimpiarFormularioSectorCero();
            this.LimpiarFormularioOrigen();
            this.LimpiarFormularioDestino();

            indexSector.Value = "";

            PanelOrigen.Visible = false;
            UpdatePanelOrigen.Update();

            PanelDestino.Visible = false;
            UpdatePanelDestino.Update();

            botonGuardarOrigen.Text = "Guardar Origen";
            UpdatePanelBotonOrigen.Update();

            botonGuardarSector.Text = "Guardar Sector";
            UpdatePanelBotonSector.Update();

        }



        #region Grilla origen


        //GRID ORIGEN
        protected void GridOrigen_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //ACCION
                HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
                if (hidden_accion != null && !hidden_accion.Value.Equals("") && Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR)
                {
                    e.Row.Attributes["style"] = "display:none";
                };


                //Modificar
                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_modificar != null)
                {
                    if (permisosService.tieneAccesoA())
                    {
                        boton_modificar.Visible = true;
                    }
                };

                //Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    if (permisosService.tieneAccesoA())
                    {
                        boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar este documento?')");
                        boton_eliminar.Visible = true;
                    }
                };

            }
        }

        //GRID ORIGEN
        protected void GridOrigen_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = -1;
            switch (e.CommandName)
            {

                case "Modificar":
                    index = Convert.ToInt32(e.CommandArgument);
                    GridOrigen_Cargar(index);
                    break;
                case "Eliminar":
                    index = Convert.ToInt32(e.CommandArgument);
                    GridOrigen_Eliminar(index);
                    break;
            };

        }

        //GRID ORIGEN
        protected void GridOrigen_Cargar(int index)
        {

            this.LimpiarDatosCentroOrigen();

            List<OrigenSector> origenes = (List<OrigenSector>)ViewState["Origenes_AsociadosRESA"];

            foreach (OrigenSector aOrigenSector in origenes)
            {
                if (aOrigenSector.index == index)
                {
                    indexOrigen.Value = Convert.ToString(index);
                    CodigoSiepOrigen.Text = Convert.ToString(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro);
                    superficieRelocalizadaOrigen.Text = Convert.ToString(aOrigenSector.superficieRelocalizada);


                    foreach (ListItem listItem in preferencias.Items)
                    {
                        foreach (ParametroGenerico aPreferencia in aOrigenSector.preferencias)
                        {
                            if (listItem.Value.Equals(Convert.ToString(aPreferencia.id)))
                            {
                                listItem.Selected = true;
                            }
                        }
                    }


                    this.CodigoSiepOrigen_OnTextChanged(null, null);

                }
            }

            botonGuardarOrigen.Text = "Modificar Origen";
            UpdatePanelBotonOrigen.Update();


            GridOrigen.DataSource = origenes;
            GridOrigen.DataBind();

            UpdatePanelOrigen.Update();
        }

        //GRID ORIGEN
        protected void GuardarOrigen_Click(object sender, EventArgs e)
        {

            Page.Validate("grupoOrigen");

            if (Page.IsValid)
            {


                if (indexOrigen.Value != null && !indexOrigen.Value.Trim().Equals(""))
                {
                    this.ModificarOrigen(Convert.ToInt32(indexOrigen.Value.Trim()));
                }
                else
                {

                    List<OrigenSector> origenes = (List<OrigenSector>)ViewState["Origenes_AsociadosRESA"];

                    int index = 0;

                    if (origenes == null)
                    {
                        origenes = new List<OrigenSector>();

                    }
                    else
                    {
                        index = origenes.Count;
                    }

                    OrigenSector nuevoOrigen = new OrigenSector();


                    try
                    {
                        nuevoOrigen.concesionOrigen = concesionService.ObtieneConcesionExistente(Convert.ToString(CodigoSiepOrigen.Text.Split(' ')[0]), rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);


                        //CENTRO NO EXISTE! SE DEBE PERMITIR IGUALMENTE EL INGRESO
                        if (nuevoOrigen.concesionOrigen == null)
                        {
                            nuevoOrigen.concesionOrigen = new SolicitudConcesion();
                            nuevoOrigen.concesionOrigen.unidadEspacial = new UnidadEspacial();
                            nuevoOrigen.concesionOrigen.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                            nuevoOrigen.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToString(CodigoSiepOrigen.Text.Split(' ')[0]);
                        }

                    }
                    catch (Exception) { }

                    try
                    {
                        nuevoOrigen.superficieRelocalizada = Convert.ToSingle(superficieRelocalizadaOrigen.Text);
                    }
                    catch (Exception) { }



                    try
                    {

                        nuevoOrigen.preferencias = new List<ParametroGenerico>();
                        foreach (ListItem item in preferencias.Items)
                        {
                            if (item.Selected)
                            {
                                nuevoOrigen.preferencias.Add(new ParametroGenerico(Convert.ToInt32(item.Value), Convert.ToString(item.Text)));
                            }
                        }
                    }
                    catch (Exception) { }


                    nuevoOrigen.index = index;
                    nuevoOrigen.accion = accion.INGRESAR;


                    List<String> errores = tramiteRelocalizacionRESAValidacion.validaIngresoOrigenSectorRESA(nuevoOrigen, origenes, false);

                    if (errores.Count > 0)
                    {
                        /*
                        foreach (String error in errores)
                        {
                            PanelErroresGridOrigen.Visible = true;
                            ErroresGridOrigen.Text = error;
                        }
                         * */

                        foreach (String error in errores)
                        {
                            Page.Validators.Add(new ValidationError("validacionOrigen", error));
                        }
                        panelMensajeOrigen.Update();
                    }
                    else
                    {

                        MensajeSuperior.Text = "Origen Ingresado";
                        PanelMensajeSuperior.Visible = true;
                        UpdatePanelMensajeSuperior.Update();

                        ErroresGridOrigen.Text = "";
                        PanelErroresGridOrigen.Visible = false;

                        origenes.Add(nuevoOrigen);
                        LimpiarFormularioOrigen();


                        ViewState["Origenes_AsociadosRESA"] = (List<OrigenSector>)origenes;
                        GridOrigen.DataSource = origenes;
                        GridOrigen.DataBind();
                    }
                }
            }

            UpdatePanelErroresGridOrigen.Update();


            botonGuardarOrigen.Text = "Guardar Origen";
            UpdatePanelBotonOrigen.Update();
        }

        //GRID ORIGEN
        protected void ModificarOrigen(int indexModificar)
        {

            Page.Validate("grupoOrigen");

            if (Page.IsValid)
            {

                List<OrigenSector> origenes = (List<OrigenSector>)ViewState["Origenes_AsociadosRESA"];
                OrigenSector origenModificado = new OrigenSector();


                try
                {
                    origenModificado.concesionOrigen = concesionService.ObtieneConcesionExistente(Convert.ToString(CodigoSiepOrigen.Text.Split(' ')[0]), rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);

                    //CENTRO NO EXISTE! SE DEBE PERMITIR IGUALMENTE EL INGRESO
                    if (origenModificado.concesionOrigen == null)
                    {
                        origenModificado.concesionOrigen = new SolicitudConcesion();
                        origenModificado.concesionOrigen.unidadEspacial = new UnidadEspacial();
                        origenModificado.concesionOrigen.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                        origenModificado.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToString(CodigoSiepOrigen.Text.Split(' ')[0]);
                    }

                }
                catch (Exception) { }

                try
                {
                    origenModificado.superficieRelocalizada = Convert.ToSingle(superficieRelocalizadaOrigen.Text);
                }
                catch (Exception) { }


                try
                {

                    origenModificado.preferencias = new List<ParametroGenerico>();
                    foreach (ListItem item in preferencias.Items)
                    {
                        if (item.Selected)
                        {
                            origenModificado.preferencias.Add(new ParametroGenerico(Convert.ToInt32(item.Value), Convert.ToString(item.Text)));
                        }
                    }
                }
                catch (Exception) { }

                origenModificado.index = indexModificar;


                List<String> errores = tramiteRelocalizacionRESAValidacion.validaModificarOrigenSectorRESA(origenModificado, origenes, indexModificar);

                if (errores.Count > 0)
                {

                    /*
                    foreach (String error in errores)
                    {
                        PanelErroresGridOrigen.Visible = true;
                        ErroresGridOrigen.Text = error;
                    }
                     **/

                    foreach (String error in errores)
                    {
                        Page.Validators.Add(new ValidationError("validacionOrigen", error));
                    }

                    panelMensajeOrigen.Update();
                }
                else
                {

                    ErroresGridOrigen.Text = "";
                    PanelErroresGridOrigen.Visible = false;

                    foreach (OrigenSector aOrigenSector in origenes)
                    {

                        if (aOrigenSector.index == indexModificar)
                        {

                            MensajeSuperior.Text = "Origen Modificado";
                            PanelMensajeSuperior.Visible = true;
                            UpdatePanelMensajeSuperior.Update();


                            aOrigenSector.concesionOrigen = origenModificado.concesionOrigen;
                            aOrigenSector.superficieRelocalizada = origenModificado.superficieRelocalizada;
                            aOrigenSector.preferencias = origenModificado.preferencias;

                            //SE LE INDICA QUE SE DEBE MODIFICAR
                            if (aOrigenSector.accion == accion.LISTADO)
                            {
                                aOrigenSector.accion = accion.MODIFICAR;
                            }

                        }
                    }

                    LimpiarFormularioOrigen();


                    ViewState["Origenes_AsociadosRESA"] = (List<OrigenSector>)origenes;
                    GridOrigen.DataSource = origenes;
                    GridOrigen.DataBind();
                }
            }

            UpdatePanelErroresGridOrigen.Update();

        }

        //GRID ORIGEN
        protected void LimpiarFormularioOrigen()
        {

            indexOrigen.Value = "";
            CodigoSiepOrigen.Text = "";
            superficieRelocalizadaOrigen.Text = "";
            preferencias.ClearSelection();
            LimpiarDatosCentroOrigen();

            ViewState["Origenes_AsociadosRESA"] = null;
            GridOrigen.DataSource = null;
            GridOrigen.DataBind();

            UpdatePanelOrigen.Update();


            botonGuardarOrigen.Text = "Guardar Origen";
            UpdatePanelBotonOrigen.Update();

        }

        //GRID ORIGEN
        protected void LimpiarFormularioOrigenMenosGrilla()
        {

            indexOrigen.Value = "";
            CodigoSiepOrigen.Text = "";
            superficieRelocalizadaOrigen.Text = "";
            preferencias.ClearSelection();
            LimpiarDatosCentroOrigen();

            UpdatePanelOrigen.Update();

            botonGuardarOrigen.Text = "Guardar Origen";
            UpdatePanelBotonOrigen.Update();

        }

        //GRID ORIGEN
        protected void LimpiarDatosCentroOrigen()
        {
            nombreTitularOrigen.Text = "";
            UpdatePanelNombreTitularOrigen.Update();

            sectorOrigen.Text = "";
            comunaOrigen.Text = "";
            regionOrigen.Text = "";
            acOrigen.Text = "";
            tipoCentroOrigen.Text = "";
            superficieTotalCentroOrigen.Text = "";
            informeRelocalizacionOrigenRESA.Text = "";

            PanelDatosCentroOrigen.Visible = false;
            UpdatePanelDatosCentroOrigen.Update();
        }

        //GRID ORIGEN
        protected void GridOrigen_Eliminar(int index)
        {
            List<OrigenSector> origenes = (List<OrigenSector>)ViewState["Origenes_AsociadosRESA"];

            foreach (OrigenSector aOrigenSector in origenes)
            {
                if (aOrigenSector.index == index)
                {
                    List<String> errores = tramiteRelocalizacionRESAValidacion.validaEliminacionOrigenSectorRESA(aOrigenSector);
                    if (errores.Count > 0)
                    {
                        foreach (String error in errores)
                        {
                            ErroresGridOrigen.Text = error;
                            PanelErroresGridOrigen.Visible = true;
                        }
                    }
                    else
                    {

                        MensajeSuperior.Text = "Origen Eliminado";
                        PanelMensajeSuperior.Visible = true;
                        UpdatePanelMensajeSuperior.Update();

                        ErroresGridOrigen.Text = "";
                        PanelErroresGridOrigen.Visible = false;
                        aOrigenSector.accion = accion.ELIMINAR;
                        GridOrigen.Rows[index].Attributes["style"] = "display:none";
                        GridOrigen.DataBind();
                        break;
                    }
                }
            }

            ViewState["Origenes_AsociadosRESA"] = (List<OrigenSector>)origenes;
            GridOrigen.DataSource = origenes;
            GridOrigen.DataBind();
            UpdatePanelErroresGridOrigen.Update();

            LimpiarFormularioOrigenMenosGrilla();
        }


        #endregion




        #region Grilla Sector

        //GRID SECTOR
        protected void GuardarSector_Click(object sender, EventArgs e)
        {


            if (indexSector.Value != null && !indexSector.Value.Trim().Equals(""))
            {
                this.ModificarSector(Convert.ToInt32(indexSector.Value.Trim()));
            }
            else
            {

                List<String> errores = null;
                List<DetalleSector> sectores = (List<DetalleSector>)ViewState["Sectores_TramiteRESA"];
                int index = 0;

                if (sectores == null)
                {
                    sectores = new List<DetalleSector>();
                }
                else
                {
                    index = sectores.Count;
                }

                DetalleSector nuevoSector = new DetalleSector();
                nuevoSector.index = index;
                nuevoSector.tramiteRel = (TramiteRelocalizacion)ViewState["TramiteRESA"];
                nuevoSector.estadoSector = new ParametroGenerico(rbEstadosGenerales.SECTOR_RELOCALIZACION_EN_TRAMITE);


                if (esSectorCeroOpcion1.Checked)
                {
                    nuevoSector.esSectorCero = true;
                }
                else if (esSectorCeroOpcion2.Checked)
                {
                    nuevoSector.esSectorCero = false;
                }
                else
                {
                    Page.Validators.Add(new ValidationError("GrupoSector", "Indique si es sector 0"));
                }


                if (Page.IsValid)
                {

                    //ES UN SECTOR 0
                    if (nuevoSector.esSectorCero)
                    {

                        nuevoSector.tipoRelocalizacion = new ParametroGenerico(rbTipo.RELOCALIZACION_SECTOR_CERO_RESA, cadenas.RELOCALIZACION_SECTOR_CERO);
                        OrigenSector origenSector = new OrigenSector();

                        if (!CodigoSiepSectorCero.Text.Trim().Equals(""))
                        {
                            try
                            {
                                origenSector.concesionOrigen = concesionService.ObtieneConcesionExistente(Convert.ToString(CodigoSiepSectorCero.Text.Split(' ')[0]), rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);

                                //CENTRO NO EXISTE! SE DEBE PERMITIR IGUALMENTE EL INGRESO
                                if (origenSector.concesionOrigen == null)
                                {
                                    origenSector.concesionOrigen = new SolicitudConcesion();
                                    origenSector.concesionOrigen.unidadEspacial = new UnidadEspacial();
                                    origenSector.concesionOrigen.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                    origenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToString(CodigoSiepSectorCero.Text.Split(' ')[0]);
                                }
                            }
                            catch (Exception)
                            {

                            }

                        }


                        if (!HectareasSectorCero.Text.Trim().Equals(""))
                        {
                            try
                            {
                                origenSector.superficieRelocalizada = float.Parse(HectareasSectorCero.Text);
                            }
                            catch (Exception) { }

                        }


                        errores = tramiteRelocalizacionRESAValidacion.validaIngresoOrigenSectorRESA(origenSector, null, true);

                        if (errores.Count == 0)
                        {
                            nuevoSector.origenes = new List<OrigenSector>();
                            nuevoSector.origenes.Add(origenSector);
                        }


                    }
                    else
                    {


                        nuevoSector.origenes = (List<OrigenSector>)ViewState["Origenes_AsociadosRESA"];
                        nuevoSector.superficieSector = this.sumarHectareasOrigen(nuevoSector.origenes);



                        if (opcionCrea.Checked)
                        {
                            nuevoSector.tipoRelocalizacion = new ParametroGenerico(rbTipo.RELOCALIZACION_CREA_RESA, cadenas.RELOCALIZACION_CREA);
                        }

                        if (opcionFusiona.Checked)
                        {
                            nuevoSector.tipoRelocalizacion = new ParametroGenerico(rbTipo.RELOCALIZACION_FUSIONA_RESA, cadenas.RELOCALIZACION_FUSIONA);
                        }


                        //VA A FUSIONAR A UN CENTRO YA EXISTENTE
                        if (opcionFusiona.Checked)
                        {
                            if (!codigoSiepCentroDestino.Text.Trim().Equals(""))
                            {
                                nuevoSector.concesionDestino = new SolicitudConcesion();
                                nuevoSector.concesionDestino = concesionService.ObtieneConcesionExistente(Convert.ToString(codigoSiepCentroDestino.Text.Split(' ')[0]), rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);

                                //CENTRO NO EXISTE! SE DEBE PERMITIR IGUALMENTE EL INGRESO
                                if (nuevoSector.concesionDestino == null)
                                {
                                    nuevoSector.concesionDestino = new SolicitudConcesion();
                                    nuevoSector.concesionDestino.unidadEspacial = new UnidadEspacial();
                                    nuevoSector.concesionDestino.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                    nuevoSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToString(codigoSiepCentroDestino.Text.Split(' ')[0]);
                                }

                            }
                        }
                    }


                    //SI HAY ERRORES EN ESTE PUNTO, QUIERE DECIR QUE ERA UN SECTOR 0 QUE PRESENTA PROBLEMAS, SE DEBEN SOLUCIONAR PRIMERO ESOS, ANTES DE VALIDAR EL SECTIR
                    if (errores == null || errores.Count == 0)
                    {
                        errores = tramiteRelocalizacionRESAValidacion.validaIngresoSectorRelocalizacionRESA(nuevoSector, sectores);
                    }

                    if (errores.Count == 0)
                    {

                        nuevoSector.numSector = this.obtenerNumeroSector(nuevoSector);

                        sectores.Add(nuevoSector);
                        ViewState["Sectores_TramiteRESA"] = (List<DetalleSector>)sectores;

                        GridSectores.DataSource = sectores;
                        GridSectores.DataBind();
                        UpdatePanelGridSectores.Update();

                        this.LimpiarFormulario(null, null);

                        MensajeSuperior.Text = "Sector Ingresado";
                        PanelMensajeSuperior.Visible = true;
                        UpdatePanelMensajeSuperior.Update();

                    }
                    else
                    {
                        foreach (String error in errores)
                        {
                            Page.Validators.Add(new ValidationError("GrupoSector", error));
                        }
                    }
                }
            }

            UpdatePanelMensajesValidaciones.Update();
        }

        //GRID SECTOR
        protected void GridSectores_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //ACCION
                HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
                if (hidden_accion != null && !hidden_accion.Value.Equals("") && Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR)
                {
                    e.Row.Attributes["style"] = "display:none";
                };

                //Modificar
                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_modificar != null)
                {
                    if (permisosService.tieneAccesoA())
                    {
                        boton_modificar.Visible = true;
                    }
                };

                //Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    if (permisosService.tieneAccesoA())
                    {
                        boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar este documento?')");
                        boton_eliminar.Visible = true;
                    }
                };

            }

        }

        //GRID SECTOR
        protected void GridSectores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = -1;
            switch (e.CommandName)
            {

                case "Modificar":
                    index = Convert.ToInt32(e.CommandArgument);
                    GridSectores_Cargar(index);
                    break;

                case "Eliminar":
                    int idTipo = Convert.ToInt32(e.CommandArgument);
                    GridSectores_Eliminar(idTipo);
                    break;
            };

        }

        //GRID SECTOR
        protected void GridSectores_Cargar(int index)
        {

            this.LimpiarFormulario(null, null);

            List<DetalleSector> sectores = (List<DetalleSector>)ViewState["Sectores_TramiteRESA"];

            foreach (DetalleSector aDetalleSector in sectores)
            {
                if (aDetalleSector.index == index)
                {

                    indexSector.Value = Convert.ToString(index);

                    //SECTOR CERO
                    if (aDetalleSector.esSectorCero)
                    {
                        esSectorCeroOpcion1.Checked = true;
                        this.esSectorCeroOpcionChecked(null, null);

                        CodigoSiepSectorCero.Text = Convert.ToString(aDetalleSector.origenes[0].concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro);
                        this.CodigoSiepSectorCero_OnTextChanged(null, null);
                        HectareasSectorCero.Text = Convert.ToString(aDetalleSector.origenes[0].superficieRelocalizada);

                        ViewState["Origenes_AsociadosRESA"] = (List<OrigenSector>)aDetalleSector.origenes;
                    }
                    else
                    {

                        esSectorCeroOpcion2.Checked = true;
                        this.esSectorCeroOpcionChecked(null, null);


                        ViewState["Origenes_AsociadosRESA"] = (List<OrigenSector>)aDetalleSector.origenes;
                        GridOrigen.DataSource = aDetalleSector.origenes;
                        GridOrigen.DataBind();


                        if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA_RESA)
                        {
                            opcionCrea.Checked = true;
                        }
                        if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA_RESA)
                        {
                            opcionFusiona.Checked = true;
                        }

                        this.tipoRelocalizacionOpcionChecked(null, null);


                        if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA_RESA)
                        {

                            codigoSiepCentroDestino.Text = Convert.ToString(aDetalleSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro);
                            CodigoSiepDestino_OnTextChanged(null, null);

                        }
                    }

                    botonGuardarSector.Text = "Modificar Sector";
                    UpdatePanelCheck.Update();
                    UpdatePanelSectorCero.Update();
                    UpdatePanelOrigen.Update();
                    UpdatePanelDestino.Update();
                    UpdatePanelBotonSector.Update();

                }
            }

        }

        //GRID SECTOR
        protected void GridSectores_Eliminar(int index)
        {

            List<DetalleSector> sectores = (List<DetalleSector>)ViewState["Sectores_TramiteRESA"];

            foreach (DetalleSector aDetalleSector in sectores)
            {
                if (aDetalleSector.index == index)
                {
                    List<String> errores = new List<String>(); //VALIDAR REGLAS PARA PODER ELIMINAR
                    if (errores.Count > 0)
                    {
                        foreach (String error in errores)
                        {
                            ErroresGridSectores.Text = error;
                            PanelErroresGridSectores.Visible = true;
                        }
                    }
                    else
                    {

                        MensajeSuperior.Text = "Sector Eliminado";
                        PanelMensajeSuperior.Visible = true;
                        UpdatePanelMensajeSuperior.Update();

                        ErroresGridSectores.Text = "";
                        PanelErroresGridSectores.Visible = false;
                        aDetalleSector.accion = accion.ELIMINAR;
                        GridSectores.Rows[index].Attributes["style"] = "display:none";
                        break;
                    }
                }
            }

            ViewState["Sectores_TramiteRESA"] = (List<DetalleSector>)sectores;
            GridSectores.DataSource = sectores;
            GridSectores.DataBind();
            UpdatePanelGridSectores.Update();
            UpdatePanelErroresGridSectores.Update();
            UpdatePanelMensajesValidaciones.Update();

            this.LimpiarFormulario(null, null);


        }

        //GRID SECTOR
        protected void ModificarSector(int indexModificar)
        {


            List<String> errores = null;
            List<DetalleSector> sectores = (List<DetalleSector>)ViewState["Sectores_TramiteRESA"];


            DetalleSector nuevoSector = new DetalleSector();


            nuevoSector.index = indexModificar;


            if (esSectorCeroOpcion1.Checked)
            {
                nuevoSector.esSectorCero = true;
            }
            else if (esSectorCeroOpcion2.Checked)
            {
                nuevoSector.esSectorCero = false;
            }
            else
            {
                Page.Validators.Add(new ValidationError("GrupoSector", "Indique si es sector 0"));
            }


            if (Page.IsValid)
            {

                //ES UN SECTOR 0
                if (nuevoSector.esSectorCero)
                {


                    nuevoSector.tipoRelocalizacion = new ParametroGenerico(rbTipo.RELOCALIZACION_SECTOR_CERO_RESA, cadenas.RELOCALIZACION_SECTOR_CERO);

                    OrigenSector origenSector = new OrigenSector();

                    if (!CodigoSiepSectorCero.Text.Trim().Equals(""))
                    {
                        try
                        {
                            origenSector.concesionOrigen = concesionService.ObtieneConcesionExistente(Convert.ToString(CodigoSiepSectorCero.Text.Split(' ')[0]), rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);


                            //CENTRO NO EXISTE! SE DEBE PERMITIR IGUALMENTE EL INGRESO
                            if (origenSector.concesionOrigen == null)
                            {
                                origenSector.concesionOrigen = new SolicitudConcesion();
                                origenSector.concesionOrigen.unidadEspacial = new UnidadEspacial();
                                origenSector.concesionOrigen.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                origenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToString(CodigoSiepSectorCero.Text.Split(' ')[0]);
                            }

                        }
                        catch (Exception) { }

                    }
                    if (!HectareasSectorCero.Text.Trim().Equals(""))
                    {


                        try
                        {

                            origenSector.superficieRelocalizada = float.Parse(HectareasSectorCero.Text);
                        }
                        catch (Exception) { }

                    }


                    errores = tramiteRelocalizacionRESAValidacion.validaIngresoOrigenSectorRESA(origenSector, null, true);

                    if (errores.Count == 0)
                    {
                        nuevoSector.origenes = new List<OrigenSector>();
                        nuevoSector.origenes.Add(origenSector);
                    }


                }
                else
                {

                    nuevoSector.origenes = (List<OrigenSector>)ViewState["Origenes_AsociadosRESA"];
                    nuevoSector.superficieSector = this.sumarHectareasOrigen(nuevoSector.origenes);

                    if (opcionCrea.Checked)
                    {
                        nuevoSector.tipoRelocalizacion = new ParametroGenerico(rbTipo.RELOCALIZACION_CREA_RESA, cadenas.RELOCALIZACION_CREA);
                    }

                    if (opcionFusiona.Checked)
                    {
                        nuevoSector.tipoRelocalizacion = new ParametroGenerico(rbTipo.RELOCALIZACION_FUSIONA_RESA, cadenas.RELOCALIZACION_FUSIONA);
                    }


                    //VA A FUSIONAR A UN CENTRO YA EXISTENTE
                    if (opcionFusiona.Checked)
                    {
                        if (!codigoSiepCentroDestino.Text.Trim().Equals(""))
                        {
                            nuevoSector.concesionDestino = new SolicitudConcesion();
                            nuevoSector.concesionDestino = concesionService.ObtieneConcesionExistente(Convert.ToString(codigoSiepCentroDestino.Text.Split(' ')[0]), rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA);

                            //CENTRO NO EXISTE! SE DEBE PERMITIR IGUALMENTE EL INGRESO
                            if (nuevoSector.concesionDestino == null)
                            {
                                nuevoSector.concesionDestino = new SolicitudConcesion();
                                nuevoSector.concesionDestino.unidadEspacial = new UnidadEspacial();
                                nuevoSector.concesionDestino.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                nuevoSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToString(codigoSiepCentroDestino.Text.Split(' ')[0]);
                            }

                        }
                    }
                }


                //SI HAY ERRORES EN ESTE PUNTO, QUIERE DECIR QUE ERA UN SECTOR 0 QUE PRESENTA PROBLEMAS, SE DEBEN SOLUCIONAR PRIMERO ESOS, ANTES DE VALIDAR EL SECTIR
                if (errores == null || errores.Count == 0)
                {
                    errores = tramiteRelocalizacionRESAValidacion.validaModificarSectorRelocalizacionRESA(nuevoSector, sectores, indexModificar);
                }

                if (errores.Count == 0)
                {



                    foreach (DetalleSector aDetalleSector in sectores)
                    {

                        if (aDetalleSector.index == indexModificar)
                        {



                            //SI EL SECTOR AHORA ES SECTOR CERO 
                            if (nuevoSector.esSectorCero)
                            {

                                if (aDetalleSector.origenes != null)
                                {
                                    foreach (OrigenSector aOrigenSector in aDetalleSector.origenes)
                                    {
                                        aOrigenSector.accion = accion.ELIMINAR;
                                    }
                                }

                                //SE INGRESA EL NUEVO ORIGEN DEL SECTOR 0 EN LA PRIMERA POSICION DE LA LISTA, Y TODOS LOS DEMAS ORIGENES SE MARCAN PARA SER ELIMINADOS
                                List<OrigenSector> origenesAux = aDetalleSector.origenes;
                                aDetalleSector.origenes = null;
                                aDetalleSector.origenes = new List<OrigenSector>();
                                aDetalleSector.origenes.AddRange(nuevoSector.origenes);
                                aDetalleSector.origenes.AddRange(origenesAux);

                            }
                            //SI ANTES ERA SECTOR CERO Y AHORA NO LO ES...
                            else if (aDetalleSector.esSectorCero && !nuevoSector.esSectorCero)
                            {
                                if (aDetalleSector.origenes != null)
                                {
                                    foreach (OrigenSector aOrigenSector in aDetalleSector.origenes)
                                    {
                                        aOrigenSector.accion = accion.ELIMINAR;
                                    }
                                }
                                aDetalleSector.origenes.AddRange(nuevoSector.origenes);

                            }

                            else
                            {
                                aDetalleSector.origenes = nuevoSector.origenes;
                            }


                            aDetalleSector.esSectorCero = nuevoSector.esSectorCero;
                            aDetalleSector.tipoRelocalizacion = nuevoSector.tipoRelocalizacion;
                            aDetalleSector.concesionDestino = nuevoSector.concesionDestino;
                            aDetalleSector.superficieSector = nuevoSector.superficieSector;

                            //SE LE INDICA QUE SE DEBE MODIFICAR
                            if (aDetalleSector.accion == accion.LISTADO)
                            {
                                aDetalleSector.accion = accion.MODIFICAR;
                            }
                            break;
                        }

                    }


                    botonGuardarSector.Text = "Guardar Sector";
                    UpdatePanelBotonSector.Update();


                    ViewState["Sectores_TramiteRESA"] = (List<DetalleSector>)sectores;

                    GridSectores.DataSource = sectores;
                    GridSectores.DataBind();
                    UpdatePanelGridSectores.Update();

                    this.LimpiarFormulario(null, null);

                    MensajeSuperior.Text = "Sector Modificado";
                    PanelMensajeSuperior.Visible = true;
                    UpdatePanelMensajeSuperior.Update();

                }
                else
                {
                    foreach (String error in errores)
                    {
                        Page.Validators.Add(new ValidationError("GrupoSector", error));
                    }
                }
            }


        }

        #endregion


        #region guardar tramite

        //TRAMITE
        protected void GuardarTramite_Click(object sender, EventArgs e)
        {

            TramiteRelocalizacion tramiteRelocalizacion = (TramiteRelocalizacion)ViewState["TramiteRESA"];
            tramiteRelocalizacion.tipoRelocalizacion = new ParametroGenerico(rbTipo.RELOCALIZACION_RESA);
            tramiteRelocalizacion.sectores = (List<DetalleSector>)ViewState["Sectores_TramiteRESA"];


            List<String> errores = tramiteRelocalizacionRESAValidacion.validaTramiteRelocalizacionRESA(tramiteRelocalizacion);


            if (errores.Count == 0)
            {

                if (relocalizacionRESAService.GuardarTramiteRelocalizacionRESA(tramiteRelocalizacion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario))
                {
                    this.mensaje = "Trámite registrado con éxito. (Número Pert " + tramiteRelocalizacion.numPert + ")";

                    try
                    {

                        enviarCorreo.alertaRelocalizacion(tramiteRelocalizacion);

                    }
                    catch (Exception)
                    {

                    }

                    Server.Transfer("~/Solicitudes/RelocalizacionRESA/administrarSolicitudRelocalizacionRESA.aspx");
                    //Response.Redirect("~/Solicitudes/Relocalizacion/administrarSolicitudRelocalizacion.aspx");
                }
                else
                {
                    Page.Validators.Add(new ValidationError("GrupoSector", "Ha ocurrido un error al realizar  la acción solicitada"));
                }
            }
            else
            {
                foreach (String error in errores)
                {
                    Page.Validators.Add(new ValidationError("GrupoSector", error));
                }
            }


        }

        #endregion



    }
}