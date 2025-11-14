

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Utilidades;
using Datos.Contantes;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace SubPesca.Unidades.Concesion
{
    public partial class resumenConcesionComponente : System.Web.UI.UserControl
    {
        
        
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        Funciones funciones = new Funciones();
        SolicitudDA solicitudDA = new SolicitudDA();

        protected void setearModulo()
        {
            if (funciones.retornaModulo().Equals("Concesion"))
            {
                ViewState["solicitudSession"] = paginas.solicitudConcesionSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_CONCESION_DE_ACUICULTURA;
            }

            else if (funciones.retornaModulo().Equals("Acopio"))
            {
                ViewState["solicitudSession"] = paginas.solicitudAcopioSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_CENTRO_ACOPIO;
            }
            else if (funciones.retornaModulo().Equals("Faenamiento"))
            {
                ViewState["solicitudSession"] = paginas.solicitudFaenamientoSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_CENTRO_DE_FAENAMIENTO;
            }

            else if (funciones.retornaModulo().Equals("Amerb"))
            {
                ViewState["solicitudSession"] = paginas.solicitudAmerbSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_CENTRO_EN_AMERB;
            }

            else if (funciones.retornaModulo().Equals("Colector"))
            {
                ViewState["solicitudSession"] = paginas.solicitudColectorSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_COLECTOR_DE_SEMILLAS;

                PanelCodigoCentro.Visible = false;
                UpdatePanelCodigoCentro.Update();

                PanelNumeroDiarioOficial.Visible = false;
                UpdatePanelNumeroDiarioOficial.Update();

                PanelFechaDiarioOficial.Visible = false;
                UpdatePanelFechaDiarioOficial.Update();

                PanelNumeroActaEntrega.Visible = true;
                UpdatePanelNumeroActaEntrega.Update();

                PanelFechaActaEntrega.Visible = true;
                UpdatePanelFechaActaEntrega.Update();

                PanelCapitaniaDePuerto.Visible = false;
                UpdatePanelCapitaniaDePuerto.Update();
            }
            else if (funciones.retornaModulo().Equals("ECMPO"))
            {
                ViewState["solicitudSession"] = paginas.solicitudECMPOSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_ECMPO;

                PanelCodigoCentro.Visible = true;
                UpdatePanelCodigoCentro.Update();

                PanelNumeroDiarioOficial.Visible = true;
                UpdatePanelNumeroDiarioOficial.Update();

                PanelFechaDiarioOficial.Visible = true;
                UpdatePanelFechaDiarioOficial.Update();

                PanelNumeroActaEntrega.Visible = true;
                UpdatePanelNumeroActaEntrega.Update();

                PanelFechaActaEntrega.Visible = true;
                UpdatePanelFechaActaEntrega.Update();

                PanelCapitaniaDePuerto.Visible = true;
                UpdatePanelCapitaniaDePuerto.Update();

            }
            else if (funciones.retornaModulo().Equals("ExperimentalesAmerb"))
            {
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesAmerbSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_EXPERIMENTALES_AMERB;

                PanelCodigoCentro.Visible = false;
                UpdatePanelCodigoCentro.Update();

                PanelNumeroDiarioOficial.Visible = false;
                UpdatePanelNumeroDiarioOficial.Update();

                PanelFechaDiarioOficial.Visible = false;
                UpdatePanelFechaDiarioOficial.Update();

                PanelNumeroActaEntrega.Visible = true;
                UpdatePanelNumeroActaEntrega.Update();

                PanelFechaActaEntrega.Visible = true;
                UpdatePanelFechaActaEntrega.Update();

                PanelCapitaniaDePuerto.Visible = false;
                UpdatePanelCapitaniaDePuerto.Update();

            }
            else if (funciones.retornaModulo().Equals("ExperimentalesConcesion"))
            {
                ViewState["solicitudSession"] = paginas.solicitudExperimentalesConcesionSession;
                ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"] = paginas.URL_ADMINISTRAR_EXPERIMENTALES_CONCESION;

                PanelCodigoCentro.Visible = false;
                UpdatePanelCodigoCentro.Update();

                PanelNumeroDiarioOficial.Visible = false;
                UpdatePanelNumeroDiarioOficial.Update();

                PanelFechaDiarioOficial.Visible = false;
                UpdatePanelFechaDiarioOficial.Update();

                PanelNumeroActaEntrega.Visible = true;
                UpdatePanelNumeroActaEntrega.Update();

                PanelFechaActaEntrega.Visible = true;
                UpdatePanelFechaActaEntrega.Update();

                PanelCapitaniaDePuerto.Visible = false;
                UpdatePanelCapitaniaDePuerto.Update();
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                setearModulo();
                
                // Inicializamos el formulario
                Initialize_Form();
            }
        }

        private void Initialize_Form()
        {

            SolicitudConcesion concesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

            if (concesion == null || usuario_logeado == null)
            {
                Response.Redirect(ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"].ToString());
            }

            if (concesion != null && concesion.idSolConcesion > 0)
            {

                SolicitudConcesion solicitudConcesion = solicitudDA.ObtieneAdminUnidadesEspaciales(concesion.idSolConcesion);

                if (solicitudConcesion == null)
                {
                    Response.Redirect(ViewState["URL_ADMINISTRAR_UNIDAD_ESPACIAL"].ToString());
                }


                NombreUnidadEspacial.Text = funciones.retornaModulo();

                if (solicitudConcesion.unidadEspacial != null)
                {

                    if (solicitudConcesion.unidadEspacial.centrosDeCultivo != null)
                    {
                        CodigoCentro.Text = solicitudConcesion.unidadEspacial.centrosDeCultivo.codigoCentro + " " + solicitudConcesion.unidadEspacial.centrosDeCultivo.nombreCentro;
                    }

                    if (solicitudConcesion.numPert != null)
                    {
                        Pert.Text = solicitudConcesion.numPert;
                    }

                    if (solicitudConcesion.unidadEspacial.numeroDiarioOficial > 0)
                    {
                        NumeroDiarioOficial.Text = Convert.ToString(solicitudConcesion.unidadEspacial.numeroDiarioOficial);
                    }

                    if (solicitudConcesion.unidadEspacial.fechaDiarioOficial != default(DateTime))
                    {
                        FechaDiarioOficial.Text = Convert.ToString(solicitudConcesion.unidadEspacial.fechaDiarioOficial);
                    }

                    if (solicitudConcesion.unidadEspacial.numeroActaEntrega > 0)
                    {
                        NumeroActaEntrega.Text = Convert.ToString(solicitudConcesion.unidadEspacial.numeroActaEntrega);
                    }

                    if (solicitudConcesion.unidadEspacial.fechaActaEntrega != default(DateTime))
                    {
                        FechaActaEntrega.Text = Convert.ToString(solicitudConcesion.unidadEspacial.fechaActaEntrega);
                    }

                    if (solicitudConcesion.unidadEspacial.capitaniaDePuerto != null)
                    {
                        CapitaniaPuerto.Text = solicitudConcesion.unidadEspacial.capitaniaDePuerto.nombreCapitaniaDePuerto;    
                    }

                    ActualTitular.Text = solicitudConcesion.titularesCad;
                    Especie.Text = solicitudConcesion.especiesCad;
                    GrupoInformativo.Text = solicitudConcesion.gruposInformativosCad;

                    if (solicitudConcesion.unidadEspacial.tipoPlazoNominal != null)
                    {
                        PlazoNominal.Text = solicitudConcesion.unidadEspacial.tipoPlazoNominal.descripcion;
                    }

                    if (solicitudConcesion.unidadEspacial.plazoInicio != default(DateTime))
                    {
                        PlazoInicio.Text = Convert.ToString(solicitudConcesion.unidadEspacial.plazoInicio);
                    }

                    if (solicitudConcesion.unidadEspacial.plazoVencimiento != default(DateTime))
                    {
                        PlazoVencimiento.Text = Convert.ToString(solicitudConcesion.unidadEspacial.plazoVencimiento);
                    }
                }
                
                /* Grilla de superficies */
                CargarListaSuperficies(usuario_logeado, solicitudConcesion.coordenadaGeografica);

                if (solicitudConcesion.region != null)
                {
                    Region.Text = solicitudConcesion.region.descripcion;
                }

                Comuna.Text = solicitudConcesion.comunasCad;

                if (solicitudConcesion.provincia != null)
                {
                    Provincia.Text = solicitudConcesion.provincia.descripcion;
                }

                if (solicitudConcesion.estadoVigencia != null)
                {
                    Vigencia.Text = solicitudConcesion.estadoVigencia.descripcion;
                }

                

            }
        }

        private void CargarListaSuperficies(Usuario.Serializable usuario_logeado, List<CoordenadaGeografica> listCoordenadaGeografica)
        {
            int pagina = 0;
            int i = 0;
            List<Poligono> listaPoligonos = null;

            foreach(CoordenadaGeografica coordenadaGeografica in listCoordenadaGeografica){
                
                if(i == 0){

                    listaPoligonos = coordenadaGeografica.listaPoligono;
                }
                i++;
            }

            GridViewSuperficie.PageIndex = pagina;
            GridViewSuperficie.DataSource = listaPoligonos;
            GridViewSuperficie.DataBind();
        }
    }
}