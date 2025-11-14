using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Entidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using Validaciones.cl.subpesca.rb.solicitud;
using System.Text.RegularExpressions;
using SubPesca.Utilidades;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.servicios.concesiones;
using LogicaNegocio.cl.subpesca.rb.servicios.modificacion;

namespace SubPesca.Solicitudes.Modificacion
{
    public partial class proyectoTecnico : System.Web.UI.Page
    {
        

        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        TipoDA tipoDa = new TipoDA();
        ParametroGenericoDA     parametroDa = new ParametroGenericoDA();
        ProyectoTecnicoValidacion pTValidacion = new ProyectoTecnicoValidacion();
        SolicitudDA solicitudDA = new SolicitudDA();

        SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Inicializamos el formulario
                Initialize_Form();
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];
            }
        }

        protected void Initialize_Form()
        {
            ConcesionService concesionService = new ConcesionService();

            // Cargamos los combobox
            Initialize_Comboboxs();
            UpdatePanelPesoProm.Visible = false;
            UpdatePanelPesoPromR1.Visible = false;
            UpdatePanelPesoPromR2.Visible = false;
            PanelTipoAlimento.Visible = false;
            Panel_Agregar_EstructuraTecnica.Visible = true;

            NombreAlimentoOtro.Text = "";
            NombreAlimentoOtro.ReadOnly = true;
            AlgaOtroDef.Text = "";
            AlgaOtroDef.ReadOnly = true;
            FondoOtroDef.Text = "";
            FondoOtroDef.ReadOnly = true;
            PanelCultivoAlgas.Visible = false;
           
            SolicitudConcesion solicitudInicial = (SolicitudConcesion)Session["SolicitudModificacion"];
           
            if (solicitudInicial != null && solicitudInicial.idSolConcesion > 0)
            {
                 IdSolicitud.Value = Convert.ToString(solicitudInicial.idSolConcesion);

                 SolicitudConcesion solicitudConcesionCompleta = solicitudDA.ObtieneSolicitudConcesionMod(solicitudInicial.idSolConcesion, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                 ProyectoTecnicoService proyService = new ProyectoTecnicoService();
                 ProyectoTecnico proyTecnicoForm = proyService.ObtenerProyectoTecnico(solicitudConcesionCompleta.idSolConcesion, 0);
                
                 DespliegueMenuSeccionDA despliegueMenuSeccionDA = new DespliegueMenuSeccionDA();
                 List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.PROYECTO_TECNICO_MODIFICACION, 0);

                 if (proyTecnicoForm != null)
                 {
                    IdProyectoTecnico.Value = Convert.ToString(proyTecnicoForm.IdProyectoTecnico);

                    /* Especies Autorizadas */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesionCompleta, despliegueMenuSeccionList, rbSeccion.ESPECIES_AUTORIZADAS))
                    {
                        CargarGrillaPT("EspecieAutorizada", proyTecnicoForm);
                        Panel_EspecieAutorizada.Visible = true;
                        UpdatePanel_EspecieAutorizada.Update();
                    }

                    /* Estructura Técnica a Instalar cada Año */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesionCompleta, despliegueMenuSeccionList, rbSeccion.ESTRUCTURAS_TECNICAS_A_INSTALAR_CADA_ANIO))
                    {
                        CargarGrillaPT("EstructuraTecnica", proyTecnicoForm);
                        Panel_EstructuraTecnica.Visible = true;
                        UpdatePanel_EstructuraTecnica.Update();
                    }

                    /* Programa de Producción */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesionCompleta, despliegueMenuSeccionList, rbSeccion.PROGRAMA_DE_PRODUCCION))
                    {
                        CargarGrillaPT("ProgramaProd", proyTecnicoForm);

                        Carga_Combobox("EspecieProgramaProduccion");
                        Carga_Combobox("GrupoProgramaProduccion");

                        Panel_ProgrProduccionPT.Visible = true;
                        UpdatePanel_ProgrProduccionPT.Update();
                    }

                    /* Observaciones Proyecto Técnico*/
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesionCompleta, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_PROYECTO_TECNICO))
                    {
                        despliegaObservaciones(proyTecnicoForm);
                        PanelObservacionesPT.Visible = true;
                        UpdatePanelObservacionesPT.Update();
                    }

                    /* Forma de Cultivo*/
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesionCompleta, despliegueMenuSeccionList, rbSeccion.FORMA_CULIVO))
                    {
                        despliegaFormaCultivo(proyTecnicoForm);
                        PanelFormaCultivo.Visible = true;
                        UpdatePanelFormaCultivo.Update();
                    }

                    ModificarProyTecnico.Visible = true;
                    GuardarProyTecnico.Visible = false;
                }
                else
                {
                    IdProyectoTecnico.Value = Convert.ToString(0);

                    /* Especies Autorizadas */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesionCompleta, despliegueMenuSeccionList, rbSeccion.ESPECIES_AUTORIZADAS))
                    {
                        CargarGrillaPT("EspecieAutorizada", null);
                        Panel_EspecieAutorizada.Visible = true;
                        UpdatePanel_EspecieAutorizada.Update();
                    }

                     /* Estructura Técnica a Instalar cada Año */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesionCompleta, despliegueMenuSeccionList, rbSeccion.ESTRUCTURAS_TECNICAS_A_INSTALAR_CADA_ANIO))
                    {
                        CargarGrillaPT("EstructuraTecnica", null);
                        Panel_EstructuraTecnica.Visible = true;
                        UpdatePanel_EstructuraTecnica.Update();
                    }

                     /* Programa de Producción */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesionCompleta, despliegueMenuSeccionList, rbSeccion.PROGRAMA_DE_PRODUCCION))
                    {
                        CargarGrillaPT("ProgramaProd", null);
                        Carga_Combobox("EspecieProgramaProduccion");
                        Carga_Combobox("GrupoProgramaProduccion");

                        Panel_ProgrProduccionPT.Visible = true;
                        UpdatePanel_ProgrProduccionPT.Update();
                    }

                    /* Observaciones Proyecto Técnico*/
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesionCompleta, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_PROYECTO_TECNICO))
                    {
                        despliegaObservaciones(null);

                        PanelObservacionesPT.Visible = true;
                        UpdatePanelObservacionesPT.Update();
                    }

                    /* Forma de Cultivo*/
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesionCompleta, despliegueMenuSeccionList, rbSeccion.FORMA_CULIVO))
                    {
                        despliegaFormaCultivo(null);
                        PanelFormaCultivo.Visible = true;
                        UpdatePanelFormaCultivo.Update();
                    }

                    ModificarProyTecnico.Visible = false;
                    GuardarProyTecnico.Visible = true;
                }
            }
           
            else
            {
               // Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
                Response.Redirect("~/Solicitudes/Modificacion/administrarSolicitudModificacion.aspx");
            }
           
        }

        private void despliegaObservaciones(ProyectoTecnico pt)
        {
            if (pt != null && pt.observaciones != null && !pt.observaciones.Equals(""))
            {

                observaciones.Text = Convert.ToString(pt.observaciones);
                
            }
        }
        private void despliegaFormaCultivo(ProyectoTecnico pt)
        {
            if (pt != null && pt.tipoCultivo != null && pt.tipoCultivo.id>0)
            {
                TipoCultivo.SelectedValue = Convert.ToString(pt.tipoCultivo.id);
                AlimentoPorTC_OnSelectedIndexChanged(null,null);
                if (pt.tipoAlimento != null && pt.tipoAlimento.Count>0)
                {
                    foreach (TipoAlimentoProyecto aux in pt.tipoAlimento)
                    {
                        if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.TIPO_ALIMENTO_PT_ALGA))
                        {
                            AlimentoAlgaFresca.Checked = true;
                            despliegaMetodoCultivoAlgas(pt);
                            
                        }
                        if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.TIPO_ALIMENTO_PT_PELLET))
                        {
                            AlimentoPellet.Checked = true;
                        }
                        if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.TIPO_ALIMENTO_PT_OTRO))
                        {
                            AlimentoOtro.Checked = true;
                            NombreAlimentoOtro.ReadOnly = false;
                            NombreAlimentoOtro.Text = aux.detalle;
                        }
                    }
                }
           }
            
        }

        private void despliegaMetodoCultivoAlgas(ProyectoTecnico pt)
        {
            if (pt.metodoCultivoAlgas != null && pt.metodoCultivoAlgas.Count > 0)
                {
                    foreach (TipoAlimentoProyecto aux in pt.metodoCultivoAlgas)
                    {
                        if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.MET_ALGAS_DIR_SUSTRATO))
                        {
                            Algas_DirSustrato.Checked = true;
                        }
                        if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.MET_ALGAS_INDIR_SUSTRATO))
                        {
                            Algas_IndirSustrato.Checked = true;
                        }
                        if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.MET_ALGAS_SUSPENDIDO))
                        {
                            Algas_Suspendido.Checked = true;
                        }
                        if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.MET_ALGAS_ESTANQUE))
                        {
                            Algas_Estanque.Checked = true;
                        }
                        if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.MET_ALGAS_OTRO))
                        {
                            Algas_Otro.Checked = true;
                            AlgaOtroDef.ReadOnly = false;
                            AlgaOtroDef.Text = aux.detalle;
                        }
                    }
            }
            if (pt.mangasPlasticas != null && pt.mangasPlasticas.Equals(true))
            {
                RadioButtonListUtilizaMangasPlasticas1.Checked = true;
            }
            if (pt.mangasPlasticas != null && pt.mangasPlasticas.Equals(false))
            {
                RadioButtonListUtilizaMangasPlasticas2.Checked = true;
            }

            if (pt.densidadSiembra != null && pt.densidadSiembra > 0)
            {
                TextBoxDensidadSiembra.Text = Convert.ToString(pt.densidadSiembra);
            }
            if (pt.tipoFondo != null && pt.tipoFondo.Count > 0)
            {
                foreach (TipoAlimentoProyecto aux in pt.tipoFondo)
                {
                    if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.T_FONDO_DURO))
                    {
                        TipoFondoDuro.Checked = true;
                    }
                    if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.T_FONDO_SEMIDURO))
                    {
                        TipoFondoSemi.Checked = true;
                    }
                    if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.T_FONDO_BLANDO))
                    {
                        TipoFondoBlando.Checked = true;
                    }
                    
                    if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.T_FONDO_OTRO))
                    {
                        TipoFondoOtro.Checked = true;
                        FondoOtroDef.ReadOnly = false;
                        FondoOtroDef.Text = aux.detalle;
                    }
                }
            }

            PanelCultivoAlgas.Visible = true;
            
        }
      
        protected void Initialize_Comboboxs()
        {

            NombreAlimentoOtro.ReadOnly = true;

            Carga_Combobox("TipoCultivo");
            TipoCultivo.SelectedValue = "-1";

            Carga_Combobox("GrupoEspecieAutorizadas");
            GrupoEspecieAutorizadas.SelectedValue = "-1";
            GrupoEspecieAutorizadas.Enabled = false;

            Carga_Combobox("EspecieAutorizada");
            EspecieAutorizada.SelectedValue = "-1";
            EspecieAutorizada.Enabled = false;
            
            Carga_Combobox("EtapaDeCultivoAutorizadas");
            EtapaDeCultivoAutorizadas.SelectedValue = "-1";

            Carga_Combobox("TipoEstructura");
            TipoEstructura.SelectedValue = "-1";

            Carga_Combobox("FormaEstructura");
            FormaEstructura.SelectedValue = "-1";

            Carga_Combobox("UnidadDeMedida");
            UnidadDeMedida.SelectedValue = "-1";

            Carga_Combobox("VolumenUnidadMedida");
            VolumenUnidadMedida.SelectedValue = "-1";

            Carga_Combobox("Anio");
            Anio.SelectedValue = "-1";

            Carga_Combobox("EspecieProgramaProduccion");
            EspecieProgramaProduccion.SelectedValue = "-1";

            Carga_Combobox("GrupoProgramaProduccion");
            GrupoProgramaProduccion.SelectedValue = "-1";
            
            Carga_Combobox("EtapaCultivoProgramaProduccion");
            EtapaCultivoProgramaProduccion.SelectedValue = "-1";

            Carga_Combobox("UnidadProgramaProduccion");
            UnidadProgramaProduccion.SelectedValue = "-1";

            Carga_Combobox("PesoPromedioEjemplares");
            PesoPromedioEjemplares.SelectedValue = "-1";
        }

        protected void Carga_Combobox(string combobox)
        {
            switch (combobox)
            {

                case "TipoCultivo":
                    // Cargamos el combobox: TipoCultivo
                    TipoCultivo.Items.Clear();
                   
                    TipoCultivo.DataSource = tipoDa.ListarTipo("TIPO_CULTIVO");
                    TipoCultivo.DataTextField = "descripcion";
                    TipoCultivo.DataValueField = "id";
                    TipoCultivo.DataBind();
                    TipoCultivo.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

                case "GrupoEspecieAutorizadas":
                    // Cargamos el combobox: GrupoEspecieAutorizadas
                    GrupoEspecieAutorizadas.Items.Clear();
                    GrupoEspecieAutorizadas.DataSource = parametroDa.ListarGrupoEspecie(0);
                    GrupoEspecieAutorizadas.DataTextField = "descripcion";
                    GrupoEspecieAutorizadas.DataValueField = "id";
                    GrupoEspecieAutorizadas.DataBind();
                    GrupoEspecieAutorizadas.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;


                case "EspecieAutorizada":
                    // Cargamos el combobox: EspecieAutorizada
                    EspecieAutorizada.Items.Clear();
                    EspecieAutorizada.DataSource = parametroDa.ListarEspecies(0,"",0);
                    EspecieAutorizada.DataTextField = "descripcion";
                    EspecieAutorizada.DataValueField = "id";
                    EspecieAutorizada.DataBind();
                    EspecieAutorizada.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

                case "EtapaDeCultivoAutorizadas":
                    // Cargamos el combobox: EtapaDeCultivoAutorizadas
                    EtapaDeCultivoAutorizadas.Items.Clear();
                    if (Convert.ToInt32(EspecieAutorizada.SelectedValue) > 0)
                    {
                        EtapaDeCultivoAutorizadas.DataSource = parametroDa.ListarEtapaDesarrolloEspecie(Convert.ToInt32(EspecieAutorizada.SelectedValue), 0);
                        EtapaDeCultivoAutorizadas.DataTextField = "descripcion";
                        EtapaDeCultivoAutorizadas.DataValueField = "id";
                        EtapaDeCultivoAutorizadas.DataBind();
                    }
                    else if (Convert.ToInt32(GrupoEspecieAutorizadas.SelectedValue) > 0)
                    {
                        EtapaDeCultivoAutorizadas.DataSource = parametroDa.ListarEtapaDesarrolloGrupo(Convert.ToInt32(GrupoEspecieAutorizadas.SelectedValue));
                        EtapaDeCultivoAutorizadas.DataTextField = "descripcion";
                        EtapaDeCultivoAutorizadas.DataValueField = "id";
                        EtapaDeCultivoAutorizadas.DataBind();
                    };
                    EtapaDeCultivoAutorizadas.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "TipoEstructura":
                    // Cargamos el combobox: TipoEstructura
                    TipoEstructura.Items.Clear();
                    TipoEstructura.DataSource = parametroDa.ListarEstructuraTecnica(0);
                    TipoEstructura.DataTextField = "descripcion";
                    TipoEstructura.DataValueField = "id";
                    TipoEstructura.DataBind();
                    TipoEstructura.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;
                case "FormaEstructura":
                    // Cargamos el combobox: FormaEstructura
                    FormaEstructura.Items.Clear();
                    if (Convert.ToInt32(TipoEstructura.SelectedValue) > 0)
                    {
                        FormaEstructura.DataSource = parametroDa.ListarFormaPorEstructura(Convert.ToInt32(TipoEstructura.SelectedValue), 0, "");
                        FormaEstructura.DataTextField = "descripcion";
                        FormaEstructura.DataValueField = "id";
                        FormaEstructura.DataBind();
                    };
                    FormaEstructura.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "UnidadDeMedida":
                    // Cargamos el combobox: UnidadDeMedida
                    UnidadDeMedida.Items.Clear();
                    if (Convert.ToInt32(TipoEstructura.SelectedValue) > 0)
                    {
                        UnidadDeMedida.DataSource = parametroDa.ListarGenerico(Convert.ToInt32(TipoEstructura.SelectedValue), "paSelRbTipoMedidaEstructura", "@idEstructuraTecnica", "idTipoMedida", "siglaMedida");
                        UnidadDeMedida.DataTextField = "descripcion";
                        UnidadDeMedida.DataValueField = "id";
                        UnidadDeMedida.DataBind();
                    };
                    UnidadDeMedida.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "VolumenUnidadMedida":
                    // Cargamos el combobox: VolumenUnidadMedida
                    VolumenUnidadMedida.Items.Clear();
                    VolumenUnidadMedida.DataSource = parametroDa.ListarTiposGenerico("paSelRbTipo", "@grupo", "VOLUMEN_UNID_MEDIDA", "idTipo", "nombreTipo");
                    VolumenUnidadMedida.DataTextField = "descripcion";
                    VolumenUnidadMedida.DataValueField = "id";
                    VolumenUnidadMedida.DataBind();
                    VolumenUnidadMedida.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;
                case "Anio":
                    // Cargamos el combobox: Anio
                    Anio.Items.Clear();
                    Anio.DataSource = parametroDa.ListarTiposGenerico("paSelRbTipo", "@grupo", "ESTRUCTURAS_POR_AÑO", "idTipo", "nombreTipo");
                    Anio.DataTextField = "descripcion";
                    Anio.DataValueField = "id";
                    Anio.DataBind();
                    Anio.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;
               
                case "EspecieProgramaProduccion":
                    // Cargamos el combobox: EspecieProgramaProduccion
                    EspecieProgramaProduccion.Items.Clear();
                    List<EspecieAutorizadaPT> List_EspecieAut2 = (List<EspecieAutorizadaPT>)ViewState["EspecieAut_ProyTecnico"];
                    HashSet<int> especieHash = new HashSet<int>();
                    if (List_EspecieAut2 != null && List_EspecieAut2.Count > 0)
                    {
                        foreach (EspecieAutorizadaPT esp in List_EspecieAut2)
                        {
                            if (especieHash.Count == 0 || !especieHash.Contains(esp.especie.id))
                            {
                                especieHash.Add(esp.especie.id);
                                EspecieProgramaProduccion.Items.Add(new ListItem(esp.DescripcionEspecie, Convert.ToString(esp.IDEspecie)));
                            }
                            
                        }
                    }
                    EspecieProgramaProduccion.Items.Insert(0, new ListItem("-- Especie --", "-1"));
                    EspecieProgramaProduccion.SelectedValue = "-1";
                    EspecieProgramaProduccion.DataBind();

                    break;
                case "GrupoProgramaProduccion":
                    // Cargamos el combobox: GrupoProgramaProduccion
                    GrupoProgramaProduccion.Items.Clear();
                    string especies = "";
                    List<EspecieAutorizadaPT> List_EspecieAutGr = (List<EspecieAutorizadaPT>)ViewState["EspecieAut_ProyTecnico"];
                    if (List_EspecieAutGr != null && List_EspecieAutGr.Count > 0)
                    {
                        foreach (EspecieAutorizadaPT esp in List_EspecieAutGr)
                        {
                            if (esp.accion == accion.INGRESAR || esp.accion == accion.MODIFICAR || esp.accion == accion.LISTADO)
                            {
                                if (especies.Equals(""))
                                {
                                    especies = "(" + esp.IDEspecie;
                                }
                                else {
                                    especies = especies + "," + esp.IDEspecie;
                                }
                            }
                        }
                        especies = especies + ")";

                        List<ParametroGenerico> gruposEspeciePP = parametroDa.ListarGrupoEspecie(especies);
                        if (gruposEspeciePP != null && gruposEspeciePP.Count >0)
                        {
                            foreach (ParametroGenerico gepp in gruposEspeciePP)
                            {
                                GrupoProgramaProduccion.Items.Add(new ListItem(gepp.descripcion, Convert.ToString(gepp.id)));
                            }
                        }
                    }
                    GrupoProgramaProduccion.Items.Insert(0, new ListItem("-- Grupo --", "-1"));
                    GrupoProgramaProduccion.SelectedValue = "-1";
                    GrupoProgramaProduccion.DataBind();
                    break;

                case "EtapaCultivoProgramaProduccion":
                    // Cargamos el combobox: EtapaCultivoProgramaProduccion
                    EtapaCultivoProgramaProduccion.Items.Clear();
                    if (Convert.ToInt32(EspecieProgramaProduccion.SelectedValue) > 0)
                    {
                        EtapaCultivoProgramaProduccion.DataSource = parametroDa.ListarEtapaDesarrolloEspecie(Convert.ToInt32(EspecieProgramaProduccion.SelectedValue), 0);
                        EtapaCultivoProgramaProduccion.DataTextField = "descripcion";
                        EtapaCultivoProgramaProduccion.DataValueField = "id";
                        EtapaCultivoProgramaProduccion.DataBind();
                    }
                    else if (Convert.ToInt32(GrupoProgramaProduccion.SelectedValue) > 0)
                    {
                        EtapaCultivoProgramaProduccion.DataSource = parametroDa.ListarEtapaDesarrolloGrupo(Convert.ToInt32(GrupoProgramaProduccion.SelectedValue));
                        EtapaCultivoProgramaProduccion.DataTextField = "descripcion";
                        EtapaCultivoProgramaProduccion.DataValueField = "id";
                        EtapaCultivoProgramaProduccion.DataBind();
                    
                    }
                    EtapaCultivoProgramaProduccion.Items.Insert(0, new ListItem("-- Etapa --", "-1"));



                    /*
                      EtapaDeCultivoAutorizadas.Items.Clear();
                    if (Convert.ToInt32(EspecieAutorizada.SelectedValue) > 0)
                    {
                        EtapaDeCultivoAutorizadas.DataSource = parametroDa.ListarEtapaDesarrolloEspecie(Convert.ToInt32(EspecieAutorizada.SelectedValue), 0);
                        EtapaDeCultivoAutorizadas.DataTextField = "descripcion";
                        EtapaDeCultivoAutorizadas.DataValueField = "id";
                        EtapaDeCultivoAutorizadas.DataBind();
                    }
                    else if (Convert.ToInt32(GrupoEspecieAutorizadas.SelectedValue) > 0)
                    {
                        EtapaDeCultivoAutorizadas.DataSource = parametroDa.ListarEtapaDesarrolloGrupo(Convert.ToInt32(GrupoEspecieAutorizadas.SelectedValue));
                        EtapaDeCultivoAutorizadas.DataTextField = "descripcion";
                        EtapaDeCultivoAutorizadas.DataValueField = "id";
                        EtapaDeCultivoAutorizadas.DataBind();
                    };
                    EtapaDeCultivoAutorizadas.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                     
                     */


                    break;
                case "UnidadProgramaProduccion":
                    // Cargamos el combobox: UnidadProgramaProduccion
                    UnidadProgramaProduccion.Items.Clear();
                    UnidadProgramaProduccion.DataSource = parametroDa.ListarTiposGenerico("paSelRbTipo", "@grupo", "UNID_EJEMPLARES", "idTipo", "nombreTipo");
                    UnidadProgramaProduccion.DataTextField = "descripcion";
                    UnidadProgramaProduccion.DataValueField = "id";
                    UnidadProgramaProduccion.DataBind();
                    UnidadProgramaProduccion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;
                case "PesoPromedioEjemplares":
                    // Cargamos el combobox: PesoPromedioEjemplares
                    PesoPromedioEjemplares.Items.Clear();
                    PesoPromedioEjemplares.DataSource = parametroDa.ListarTiposGenerico("paSelRbTipo", "@grupo", "RANGO_PESO_EJEMPLARES", "idTipo", "nombreTipo");
                    PesoPromedioEjemplares.DataTextField = "descripcion";
                    PesoPromedioEjemplares.DataValueField = "id";
                    PesoPromedioEjemplares.DataBind();
                    PesoPromedioEjemplares.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;
            }
        }

        protected void RadGrupoEspecieChecked(object sender, EventArgs e)
        {
            EtapaDeCultivoAutorizadas.Items.Clear();
            EtapaDeCultivoAutorizadas.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
            
            if (EspeciesRad.Checked == true)
            {
                GrupoEspecieAutorizadas.SelectedIndex = -1;
                GrupoEspecieAutorizadas.Enabled = false;
                EspecieAutorizada.Enabled = true;
                
            }
            else if (GrupoEspeciesRad.Checked == true)
            {
                EspecieAutorizada.SelectedIndex = -1;
                EspecieAutorizada.Enabled = false;
                GrupoEspecieAutorizadas.Enabled = true;
            }

        }

        protected void TipoAlimentoChecked(object sender, EventArgs e)
        {
                if (AlimentoOtro.Checked == true)
                {
                    NombreAlimentoOtro.ReadOnly = false;
                }
                else {
                    NombreAlimentoOtro.Text="";
                    NombreAlimentoOtro.ReadOnly = true;
                }
           
        }
        protected void AlgaOtroChecked(object sender, EventArgs e)
        {
            if (Algas_Otro.Checked == true)
            {
                AlgaOtroDef.ReadOnly = false;
            }
            else
            {
                AlgaOtroDef.Text = "";
                AlgaOtroDef.ReadOnly = true;
            }

        }
        protected void FondoOtroChecked(object sender, EventArgs e)
        {
            if (TipoFondoOtro.Checked == true)
            {
                FondoOtroDef.ReadOnly = false;
            }
            else
            {
                FondoOtroDef.Text = "";
                FondoOtroDef.ReadOnly = true;
            }

        }

        protected void AlgaChecked(object sender, EventArgs e)
        {
            Algas_DirSustrato.Checked = false;
            Algas_IndirSustrato.Checked = false;
            Algas_Suspendido.Checked = false;
            Algas_Estanque.Checked = false;
            Algas_Otro.Checked = false;
            AlgaOtroDef.Text = "";
            AlgaOtroDef.ReadOnly = true;
            RadioButtonListUtilizaMangasPlasticas1.Checked = false;
            RadioButtonListUtilizaMangasPlasticas2.Checked = false;
            TextBoxDensidadSiembra.Text = "";
            TipoFondoDuro.Checked = false;
            TipoFondoSemi.Checked = false;
            TipoFondoBlando.Checked = false;
            TipoFondoOtro.Checked = false;
            FondoOtroDef.Text = "";
            FondoOtroDef.ReadOnly = true;

            SolicitudConcesion solicitudModificacion = (SolicitudConcesion)Session["SolicitudModificacion"];
            if (solicitudModificacion == null)
            {
                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");

            }

            DespliegueMenuSeccionDA despliegueMenuSeccionDA = new DespliegueMenuSeccionDA();
            List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.PROYECTO_TECNICO_MODIFICACION,0);

            /* Cultivo de Algas*/
            if (solicitudModificacionService.despligueSeccionesMenu(solicitudModificacion, despliegueMenuSeccionList, rbSeccion.CULTIVO_DE_ALGAS))
            {
                if (AlimentoAlgaFresca.Checked == true)
                {

                    PanelCultivoAlgas.Visible = true;

                }
                else
                {
                    PanelCultivoAlgas.Visible = false;
                }
            }
            UpdatePanel_PanelCultivoAlgas.Update();
            
        }


        protected void TipoAnioEstructuraTecnica_Selected(object sender, EventArgs e)
        {

            TextBoxAnio1.Text = "";
            TextBoxAnio1.ReadOnly = true;
            TextBoxAnio2.Text = "";
            TextBoxAnio2.ReadOnly = true;
            TextBoxAnio3.Text = "";
            TextBoxAnio3.ReadOnly = true;
            TextBoxAnio4.Text = "";
            TextBoxAnio4.ReadOnly = true;

            if (Convert.ToInt32(Anio.SelectedValue) == rbTipo.ESTRUCTURAS_ANIO_MAXIMO)
            {
                TextBoxAnio1.Text = "";
                TextBoxAnio1.ReadOnly = false;
                TextBoxAnio2.Text = "";
                TextBoxAnio2.ReadOnly = true;
                TextBoxAnio3.Text = "";
                TextBoxAnio3.ReadOnly = true;
                TextBoxAnio4.Text = "";
                TextBoxAnio4.ReadOnly = false;
            }
            else if (Convert.ToInt32(Anio.SelectedValue) == rbTipo.ESTRUCTURAS_ANIO)
            {
                TextBoxAnio1.Text = "";
                TextBoxAnio1.ReadOnly = false;
                TextBoxAnio2.Text = "";
                TextBoxAnio2.ReadOnly = false;
                TextBoxAnio3.Text = "";
                TextBoxAnio3.ReadOnly = false;
                TextBoxAnio4.Text = "";
                TextBoxAnio4.ReadOnly = false;
            }
            
            
        }

        protected void CalculoVolumenAutomaticoChange(object sender, EventArgs e)
        {
            this.calcularVolumenAutomatico();
        }

        protected void calcularVolumenAutomatico()
        {
            float volumen;
            volumen = 0;
            TextBoxVolumenValorMedida.ReadOnly = false;

            this.validaDataMedidasEstructura();

            if (Convert.ToInt32(FormaEstructura.SelectedValue) > 0)
            {
                 if ((Convert.ToInt32(FormaEstructura.SelectedValue) == rbTipo.TIPO_FORMA_CUADRADA) && (!TextBoxLargoM.Text.Equals("") && this.validaDec(TextBoxLargoM.Text)))//Cuadrada  -> Largo * Largo * Largo 
                 {
                     volumen = Convert.ToSingle(TextBoxLargoM.Text) * Convert.ToSingle(TextBoxLargoM.Text) * Convert.ToSingle(TextBoxLargoM.Text);
                     TextBoxVolumenValorMedida.Text = volumen.ToString();
                     TextBoxVolumenValorMedida.ReadOnly = true;
                     this.calcularDimensionAcumulado();
                 }
                 else if ((Convert.ToInt32(FormaEstructura.SelectedValue) == rbTipo.TIPO_FORMA_CIRCULAR) && (!TextBoxDiametroM.Text.Equals("") && !TextBoxAltoM.Text.Equals("") && this.validaDec(TextBoxDiametroM.Text) && this.validaDec(TextBoxAltoM.Text)))//Circular -> PI * (Diámetro/2)^2 * Alto
                 {
                     volumen = Convert.ToSingle(Math.PI) * ((Convert.ToSingle(TextBoxDiametroM.Text) / 2) * (Convert.ToSingle(TextBoxDiametroM.Text) / 2)) * (Convert.ToSingle(TextBoxAltoM.Text));
                     TextBoxVolumenValorMedida.Text = volumen.ToString();
                     TextBoxVolumenValorMedida.ReadOnly = true;
                     this.calcularDimensionAcumulado();
                 }
                 else if ((Convert.ToInt32(FormaEstructura.SelectedValue) == rbTipo.TIPO_FORMA_RECTANGULAR) && (!TextBoxAnchoM.Text.Equals("") && !TextBoxLargoM.Text.Equals("") && !TextBoxAltoM.Text.Equals("") && this.validaDec(TextBoxAnchoM.Text) && this.validaDec(TextBoxAltoM.Text)))  //Rectangular  -> Ancho  * Largo * Alto
                 {
                     volumen = Convert.ToSingle(TextBoxAnchoM.Text) * Convert.ToSingle(TextBoxLargoM.Text) * Convert.ToSingle(TextBoxAltoM.Text);
                     TextBoxVolumenValorMedida.Text = volumen.ToString();
                     TextBoxVolumenValorMedida.ReadOnly = true;
                     this.calcularDimensionAcumulado();
                 }
              }
        }

        protected void CalculoDimensionAcumuladoChange(object sender, EventArgs e)
        {
            this.calcularDimensionAcumulado();
        }

        protected void calcularDimensionAcumulado(){

            CompareValidator_TextBoxVolumenValorMedida.Validate();

            if (Convert.ToInt32(TipoEstructura.SelectedValue) > 0 && !TotalAcumuladoNumero.Text.Equals(""))
            {
                ProyectoTecnicoService proyectoService = new ProyectoTecnicoService();
                List<EstructuraTecnica> estructuraTecnicas = proyectoService.ListaEstructuraTecnica(0);
                float total = 0;
                float area = 0;
                if (estructuraTecnicas != null && estructuraTecnicas.Count > 0)
                {
                    foreach (EstructuraTecnica estructAux in estructuraTecnicas)
                    {
                        if (estructAux.idEstructura == Convert.ToInt32(TipoEstructura.SelectedValue) && (estructAux.aplicaArea && !estructAux.aplicaVolumen))
                        {
                            //SE MANEJA POR AREA, SE DEBE CALCULAR EL AREA Y MULTIPLICARLA POR LA CANTIDAD
                            area = this.calcularArea();

                            if (area > -1 && !TotalAcumuladoNumero.Text.Equals(""))
                            {
                                total = Convert.ToSingle(area) * Convert.ToInt32(TotalAcumuladoNumero.Text);
                                TotalAcumuladoDimension.Text = Convert.ToString(total);
                                UpdatePanel_TotalAcumuladoDimension.Update();
                            }
                            break;
                        }
                        else if (estructAux.idEstructura == Convert.ToInt32(TipoEstructura.SelectedValue) && (!estructAux.aplicaArea && estructAux.aplicaVolumen))
                        {
                            //SI MANEJA POR VOLUMEN, SOLO SE DEBE MULTIPLICAR VOLUMEN POR LA CANTIDAD
                            if (!TextBoxVolumenValorMedida.Text.Equals("") && !TotalAcumuladoNumero.Text.Equals(""))
                            {
                                total = Convert.ToSingle(TextBoxVolumenValorMedida.Text) * Convert.ToInt32(TotalAcumuladoNumero.Text);
                                TotalAcumuladoDimension.Text = Convert.ToString(total);
                                UpdatePanel_TotalAcumuladoDimension.Update();
                            }
                            break;
                        }
                    }
                }
            }
        }


        public float calcularArea()
        {
            float area;
            area = -1;
           
            if (Convert.ToInt32(TipoEstructura.SelectedValue) > 0 && Convert.ToInt32(FormaEstructura.SelectedValue) > 0)
            {

                if ((Convert.ToInt32(FormaEstructura.SelectedValue) == rbTipo.TIPO_FORMA_CUADRADA) && (!TextBoxLargoM.Text.Equals("")))//Cuadrada  -> Largo * Largo 
                {
                    area = Convert.ToSingle(TextBoxLargoM.Text) * Convert.ToSingle(TextBoxLargoM.Text);
                }
                else if ((Convert.ToInt32(FormaEstructura.SelectedValue) == rbTipo.TIPO_FORMA_CIRCULAR) && (!TextBoxDiametroM.Text.Equals("")))//Circular -> PI * (Diámetro/2)^2
                {
                    area = Convert.ToSingle(Math.PI) * ((Convert.ToSingle(TextBoxDiametroM.Text) / 2) * (Convert.ToSingle(TextBoxDiametroM.Text) / 2));
                }
                else if ((Convert.ToInt32(FormaEstructura.SelectedValue) == rbTipo.TIPO_FORMA_RECTANGULAR) && (!TextBoxAnchoM.Text.Equals("") && !TextBoxLargoM.Text.Equals(""))) //Rectangular  -> Ancho * Largo
                {
                    area = Convert.ToSingle(TextBoxAnchoM.Text) * Convert.ToSingle(TextBoxLargoM.Text);
                }

                if (Convert.ToInt32(TipoEstructura.SelectedValue) == 1 && !TextBoxLargoM.Text.Equals(""))//LONG-LINE
                {
                    area = Convert.ToSingle(TextBoxLargoM.Text);
                }
            }
           
            return area;
        }

        protected void ManejoCamposEstructuraMedidas()
        { 
            TextBoxLargoM.Text="";
            TextBoxLargoM.ReadOnly = true;
            TextBoxAnchoM.Text="";
            TextBoxAnchoM.ReadOnly = true;
            TextBoxAltoM.Text="";
            TextBoxAltoM.ReadOnly = true;
            TextBoxDiametroM.Text="";
            TextBoxDiametroM.ReadOnly = true;
            VolumenUnidadMedida.Items.Clear();
            VolumenUnidadMedida.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
            TextBoxVolumenValorMedida.Text = "";
            TextBoxVolumenValorMedida.ReadOnly = true;

            if(Convert.ToInt32(TipoEstructura.SelectedValue) > 0 && Convert.ToInt32(FormaEstructura.SelectedValue) > 0){
                ProyectoTecnicoService proyService = new ProyectoTecnicoService();
                HashSet<int> resp = proyService.ListaEstructuraMedidas(Convert.ToInt32(TipoEstructura.SelectedValue),Convert.ToInt32(FormaEstructura.SelectedValue));

                if(resp!=null && resp.Count>0){
                    foreach(int aux in resp){
                        if(aux==rbTipo.ESTRUCT_MEDIDA_ALTO){
                            TextBoxAltoM.Text="";
                            TextBoxAltoM.ReadOnly = false;
                        }
                        else if(aux==rbTipo.ESTRUCT_MEDIDA_ANCHO){
                            TextBoxAnchoM.Text="";
                            TextBoxAnchoM.ReadOnly = false;
                        }
                        else if(aux==rbTipo.ESTRUCT_MEDIDA_DIAMETRO){
                            TextBoxDiametroM.Text="";
                            TextBoxDiametroM.ReadOnly = false;
                        }
                        else if(aux==rbTipo.ESTRUCT_MEDIDA_LARGO){
                            TextBoxLargoM.Text="";
                            TextBoxLargoM.ReadOnly = false;
                        }
                        else if(aux==rbTipo.ESTRUCT_MEDIDA_VOLUMEN){
                            Carga_Combobox("VolumenUnidadMedida");
                            VolumenUnidadMedida.SelectedValue = "-1";
                            TextBoxVolumenValorMedida.Text="";
                            TextBoxVolumenValorMedida.ReadOnly = false;
                        }
                    }
                }
                EstructMedidas_UpdatePanel.Update();

            }   
            
        
        }

        protected void CalculoTotalAcumuladoChange(object sender, EventArgs e)
        {

            int totalAcum = 0;

            if (!TextBoxAnio1.Text.Equals("") && Convert.ToInt32(TextBoxAnio1.Text) !=null)
            {
                totalAcum = Convert.ToInt32(TextBoxAnio1.Text);
            }
            if (!TextBoxAnio2.Text.Equals("") && Convert.ToInt32(TextBoxAnio2.Text) != null)
            {
                totalAcum = totalAcum + Convert.ToInt32(TextBoxAnio2.Text);
            }
            if (!TextBoxAnio3.Text.Equals("") && Convert.ToInt32(TextBoxAnio3.Text) != null)
            {
                totalAcum = totalAcum + Convert.ToInt32(TextBoxAnio3.Text);
            }
            if (!TextBoxAnio4.Text.Equals("") && Convert.ToInt32(TextBoxAnio4.Text) != null)
            {
                totalAcum = totalAcum + Convert.ToInt32(TextBoxAnio4.Text);
            }

            TotalAcumuladoNumero.Text = Convert.ToString(totalAcum);
            this.calcularDimensionAcumulado();
            
          
        }

        protected void EtapaPorEspecieAutorizada_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("EtapaDeCultivoAutorizadas");

        }

        protected void ManejoCamposEstructuraMedidasChanged(object sender, EventArgs e)
        {
            ManejoCamposEstructuraMedidas();
        }

        protected void FormaPorEstructura_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("FormaEstructura");
            Carga_Combobox("UnidadDeMedida");
        }
        
        
//------------------------------------------------------------------------------------
  
  
        protected void GuardarEspecieAutorizada_Click(object sender, ImageClickEventArgs e)
        {
            AgregarGrillaPT("EspecieAutorizada");
            CargarGrillaPT("EspecieAutorizada",null);
        }

        protected void GuardarEstructuraTecnica_Click(object sender, ImageClickEventArgs e)
        {
            AgregarGrillaPT("EstructuraTecnica");
            CargarGrillaPT("EstructuraTecnica", null);
        }

        protected void ModificarEstructuraTecnica_Click(object sender, ImageClickEventArgs e)
        {
            ModificarGrillaPT("EstructuraTecnica");
            CargarGrillaPT("EstructuraTecnica", null);
        }

        protected void GuardarProgramaProd_Click(object sender, ImageClickEventArgs e)
        {
            AgregarGrillaPT("ProgramaProd");
            CargarGrillaPT("ProgramaProd", null);
        }

        protected void ModificarProgramaProd_Click(object sender, ImageClickEventArgs e)
        {
            ModificarGrillaPT("ProgramaProd");
            CargarGrillaPT("ProgramaProd", null);
        }

        protected void GuardarEjemplar_Click(object sender, ImageClickEventArgs e)
        {
            AgregarGrillaPT("EjemplarPT");
            CargarGrillaPT("EjemplarPT", null);
        }

//------------------------------------------------------------------------------------

       

        private Object CargaObjeto(string seccion, string accionS) {

            Object objeto = null;

            switch (seccion)
            {  
                case "EspecieAutorizada":

                     EspecieAutorizadaPT esp_aut = new EspecieAutorizadaPT();

                    if (EspeciesRad.Checked == true)
                    {
                        esp_aut.especieCheck = true;
                        esp_aut.especie = new ParametroGenerico(Convert.ToInt32(EspecieAutorizada.SelectedValue), Convert.ToString(EspecieAutorizada.SelectedItem.Text));
                    }
                    if (GrupoEspeciesRad.Checked == true)
                    {
                        esp_aut.grupoCheck = true;
                        esp_aut.grupoEspecie = new ParametroGenerico(Convert.ToInt32(GrupoEspecieAutorizadas.SelectedValue), Convert.ToString(GrupoEspecieAutorizadas.SelectedItem.Text));
                    }
                    esp_aut.etapaCultivo = new ParametroGenerico(Convert.ToInt32(EtapaDeCultivoAutorizadas.SelectedValue), Convert.ToString(EtapaDeCultivoAutorizadas.SelectedItem.Text));
                    objeto = esp_aut;

                    break;
                
              

                    
            }


            return objeto;
        
        
        }

        

        protected void EliminarGrillaPT(int index, string seccion)
        {
            switch (seccion)
            {
                case "EspecieAutorizada":
                    List<EspecieAutorizadaPT> List_EspecieAut = (List<EspecieAutorizadaPT>)ViewState["EspecieAut_ProyTecnico"];
                    foreach (EspecieAutorizadaPT espAut in List_EspecieAut)
                    {
                        if (espAut.index.Equals(index))
                        {
                            List<ProgrProduccionPT> List_ProgrProd = (List<ProgrProduccionPT>)ViewState["ProgrProd_ProyTecnico"];
                            if (List_ProgrProd != null && List_ProgrProd.Count > 0)
                            {
                                espAut.list_ProgrProd = List_ProgrProd;
                            }

                            List<String> listaErroresEspAu = pTValidacion.validaEspecieAutorizada_Eliminar(espAut);

                            if (listaErroresEspAu.Count <= 0)
                            {
                                if (espAut.accion == accion.INGRESAR)
                                {
                                    //List_EspecieAut.Remove(espAut);
                                    espAut.accion = accion.IGNORAR;
                                    GridEspecieAutorizadaProyTecnico.Rows[index].Attributes["style"] = "display:none";
                                }
                                if (espAut.accion == accion.LISTADO)
                                {
                                    espAut.accion = accion.ELIMINAR;
                                    GridEspecieAutorizadaProyTecnico.Rows[index].Attributes["style"] = "display:none";
                                }
                            }
                            else
                            {

                                foreach (String error in listaErroresEspAu)
                                {
                                    Page.Validators.Add(new ValidationError("grupo1", error));
                                }

                                UpdatePanel_MSG_EspecieAu.Update();

                                break;
                            }
                        }
                    }

                    ViewState["EspecieAut_ProyTecnico"] = (List<EspecieAutorizadaPT>)List_EspecieAut;
                    CargarGrillaPT("EspecieAutorizada",null);

                break;
               
                case "EstructuraTecnica":
                    List<EstructuraTecnicaPT> List_EstructuraTecnica = (List<EstructuraTecnicaPT>)ViewState["EstructuraTecnica_ProyTecnico"];
                    foreach (EstructuraTecnicaPT estructTec in List_EstructuraTecnica)
                    {
                        if (estructTec.index.Equals(index))
                        {
                            if (estructTec.accion == accion.INGRESAR)
                            {
                                estructTec.accion = accion.IGNORAR;
                                GridEstructuraTecnicaProyTecnico.Rows[index].Attributes["style"] = "display:none";
                            }
                            if (estructTec.accion == accion.LISTADO || estructTec.accion == accion.MODIFICAR)
                            {
                                estructTec.accion = accion.ELIMINAR;
                                GridEstructuraTecnicaProyTecnico.Rows[index].Attributes["style"] = "display:none";
                            }
                            break;
                        }
                    }

                    ViewState["EstructuraTecnica_ProyTecnico"] = (List<EstructuraTecnicaPT>)List_EstructuraTecnica;
                    CargarGrillaPT("EstructuraTecnica",null);
                break;

                case "EjemplarPT":
                     List<EjemplarPT> List_EjemplarAut = (List<EjemplarPT>)ViewState["Ejemplar_ProyTecnico"];

                     foreach (EjemplarPT ejemp in List_EjemplarAut)
                     {
                         if (ejemp.index.Equals(index))
                         {
                            break;
                         }
                     }

                    ViewState["Ejemplar_ProyTecnico"] = (List<EjemplarPT>)List_EjemplarAut;
                    CargarGrillaPT("EjemplarPT",null);
                break;

                case "ProgramaProd":

                    List<ProgrProduccionPT> List_ProgrProduc = (List<ProgrProduccionPT>)ViewState["ProgrProd_ProyTecnico"];
                    foreach (ProgrProduccionPT progrp in List_ProgrProduc)
                    {
                        if (progrp.index.Equals(index))
                        {
                            if (progrp.accion == accion.INGRESAR)
                            {
                                progrp.accion = accion.IGNORAR;
                                GridProgrProdProyTecnico.Rows[index].Attributes["style"] = "display:none";
                            }
                            if (progrp.accion == accion.LISTADO || progrp.accion == accion.MODIFICAR)
                            {
                                progrp.accion = accion.ELIMINAR;
                                GridProgrProdProyTecnico.Rows[index].Attributes["style"] = "display:none";
                            }
                            break;
                        }
                        
                    }

                    ViewState["ProgrProd_ProyTecnico"] = (List<ProgrProduccionPT>)List_ProgrProduc;
                    CargarGrillaPT("ProgramaProd",null);

                break;

            }
            
        }

        

        



       


        protected void GridEjemplarProyTecnico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Eliminar":
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridEspecieAutorizadaProyTecnico.EditIndex = -1;
                    EliminarGrillaPT(index, "EjemplarPT");
                    CargarGrillaPT("EjemplarPT",null);
                    break;
            };
        }

        protected void GridEjemplarProyTecnico_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar?')");
                    boton_eliminar.Visible = true;
                };

             /*  //Lista anidada

                GridView gridViewAnioEjemplares = (GridView)e.Row.FindControl("gridViewAnioEjemplares");
                gridViewAnioEjemplares.DataSource = (List<ValorParametroAnioPT>)e.Row.
                gridViewAnioEjemplares.DataBind();*/
                


            };
        }

       

       

        

        protected void EtapaPorEspeciePP_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            if (Convert.ToInt32(EspecieProgramaProduccion.SelectedValue) > 0)
            {
                GrupoProgramaProduccion.SelectedValue = "-1";
                UpdatePanel_GrupoProgramaProduccion.Update();
            }
            Carga_Combobox("EtapaCultivoProgramaProduccion");

        }

        protected void EtapaPorGrupoPP_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            if (Convert.ToInt32(GrupoProgramaProduccion.SelectedValue) > 0)
            {
                EspecieProgramaProduccion.SelectedValue = "-1";
                UpdatePanel_EspecieProgramaProduccion.Update();

            }
            Carga_Combobox("EtapaCultivoProgramaProduccion");

        }

        protected void DespliegaPesoProm_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            PesoPromSinRango.Text = "";
            PesoRango1.Text = "";
            PesoRango2.Text = "";
            UpdatePanelPesoProm.Visible = false;
            UpdatePanelPesoPromR1.Visible = false;
            UpdatePanelPesoPromR2.Visible = false;

            if ((Convert.ToInt32(PesoPromedioEjemplares.SelectedValue) > 0) && (Convert.ToInt32(PesoPromedioEjemplares.SelectedValue) == rbTipo.PESO_PROM_RANGO))
            {
                UpdatePanelPesoProm.Visible = false;
                UpdatePanelPesoPromR1.Visible = true;
                UpdatePanelPesoPromR2.Visible = true;
            }
            else {
                UpdatePanelPesoProm.Visible = true;
                UpdatePanelPesoPromR1.Visible = false;
                UpdatePanelPesoPromR2.Visible = false;
            }

        }

        protected void AlimentoPorTC_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            
            
            AlimentoAlgaFresca.Checked = false;
            AlimentoPellet.Checked = false;
            AlimentoOtro.Checked = false;
            NombreAlimentoOtro.ReadOnly = true;
            
            if ((Convert.ToInt32(TipoCultivo.SelectedValue) > 0) && (Convert.ToInt32(TipoCultivo.SelectedValue) == rbTipo.TIPO_CULT_EXTENSIVO ||Convert.ToInt32(TipoCultivo.SelectedValue) ==-1))
            {
                PanelTipoAlimento.Visible = false;
                AlimentoAlgaFresca.Checked = false;
                AlimentoPellet.Checked = false;
                AlimentoOtro.Checked = false;
            }
            else
            {
                PanelTipoAlimento.Visible = true;
            }
            AlgaChecked(null, null);
            TipoAlimentoChecked(null, null);

        }

       
        protected void Guardar_ProyTecnico_Click(object sender, EventArgs e)
        {
            ProyectoTecnicoService proyService = new ProyectoTecnicoService();

            ProyectoTecnico proyTecnicoSolicitud = new ProyectoTecnico();

            proyTecnicoSolicitud.idSolicitud = Convert.ToInt32(IdSolicitud.Value);
            proyTecnicoSolicitud.tipoAlimento = new List<TipoAlimentoProyecto>();
            proyTecnicoSolicitud.densidadSiembra = -1;
            proyTecnicoSolicitud.mangasPlasticasVal = -1;

            TipoAlimentoProyecto tipoAlimentoProy = new TipoAlimentoProyecto();


            if (AlimentoAlgaFresca.Checked == true)
            {
                tipoAlimentoProy = new TipoAlimentoProyecto();
                tipoAlimentoProy.tipoAlimento = new ParametroGenerico(rbTipo.TIPO_ALIMENTO_PT_ALGA);
                proyTecnicoSolicitud.tipoAlimento.Add(tipoAlimentoProy);

                //SET METODO CULTIVO ALGAS
                proyTecnicoSolicitud.metodoCultivoAlgas = new List<TipoAlimentoProyecto>();
                TipoAlimentoProyecto metCultivoAlgas = new TipoAlimentoProyecto();
                if (Algas_DirSustrato.Checked == true)
                {
                    metCultivoAlgas = new TipoAlimentoProyecto();
                    metCultivoAlgas.tipoAlimento = new ParametroGenerico(rbTipo.MET_ALGAS_DIR_SUSTRATO);
                    proyTecnicoSolicitud.metodoCultivoAlgas.Add(metCultivoAlgas);
                }
                if (Algas_IndirSustrato.Checked == true)
                {
                    metCultivoAlgas = new TipoAlimentoProyecto();
                    metCultivoAlgas.tipoAlimento = new ParametroGenerico(rbTipo.MET_ALGAS_INDIR_SUSTRATO);
                    proyTecnicoSolicitud.metodoCultivoAlgas.Add(metCultivoAlgas);
                }
                if (Algas_Suspendido.Checked == true)
                {
                    metCultivoAlgas = new TipoAlimentoProyecto();
                    metCultivoAlgas.tipoAlimento = new ParametroGenerico(rbTipo.MET_ALGAS_SUSPENDIDO);
                    proyTecnicoSolicitud.metodoCultivoAlgas.Add(metCultivoAlgas);
                }
                if (Algas_Estanque.Checked == true)
                {
                    metCultivoAlgas = new TipoAlimentoProyecto();
                    metCultivoAlgas.tipoAlimento = new ParametroGenerico(rbTipo.MET_ALGAS_ESTANQUE);
                    proyTecnicoSolicitud.metodoCultivoAlgas.Add(metCultivoAlgas);
                }
                if (Algas_Otro.Checked == true)
                {
                    metCultivoAlgas = new TipoAlimentoProyecto();
                    metCultivoAlgas.tipoAlimento = new ParametroGenerico(rbTipo.MET_ALGAS_OTRO);
                    metCultivoAlgas.detalle = AlgaOtroDef.Text;
                    proyTecnicoSolicitud.metodoCultivoAlgas.Add(metCultivoAlgas);
                }
                if(RadioButtonListUtilizaMangasPlasticas1.Checked==true){
                    proyTecnicoSolicitud.mangasPlasticasVal = 1;
                }
                if (RadioButtonListUtilizaMangasPlasticas2.Checked == true)
                {
                    proyTecnicoSolicitud.mangasPlasticasVal = 0;
                }
                if (RadioButtonListUtilizaMangasPlasticas1.Checked == false && RadioButtonListUtilizaMangasPlasticas2.Checked == false)
                {
                    proyTecnicoSolicitud.mangasPlasticasVal = -1;
                }

                CompareValidator_TextBoxDensidadSiembra.Validate();

                if (!TextBoxDensidadSiembra.Text.Equals(""))
                {
                    proyTecnicoSolicitud.densidadSiembra = Convert.ToSingle(TextBoxDensidadSiembra.Text);
                }
                
                
                //SET TIPO FONDO
                proyTecnicoSolicitud.tipoFondo = new List<TipoAlimentoProyecto>();
                TipoAlimentoProyecto tFondo = new TipoAlimentoProyecto();
                if (TipoFondoDuro.Checked == true)
                {
                    tFondo = new TipoAlimentoProyecto();
                    tFondo.tipoAlimento = new ParametroGenerico(rbTipo.T_FONDO_DURO);
                    proyTecnicoSolicitud.tipoFondo.Add(tFondo);
                }
                if (TipoFondoSemi.Checked == true)
                {
                    tFondo = new TipoAlimentoProyecto();
                    tFondo.tipoAlimento = new ParametroGenerico(rbTipo.T_FONDO_SEMIDURO);
                    proyTecnicoSolicitud.tipoFondo.Add(tFondo);
                }
                if (TipoFondoBlando.Checked == true)
                {
                    tFondo = new TipoAlimentoProyecto();
                    tFondo.tipoAlimento = new ParametroGenerico(rbTipo.T_FONDO_BLANDO);
                    proyTecnicoSolicitud.tipoFondo.Add(tFondo);
                }
                if (TipoFondoOtro.Checked == true)
                {
                    tFondo = new TipoAlimentoProyecto();
                    tFondo.tipoAlimento = new ParametroGenerico(rbTipo.T_FONDO_OTRO);
                    tFondo.detalle = FondoOtroDef.Text;
                    proyTecnicoSolicitud.tipoFondo.Add(tFondo);
                }
    
             }
            if (AlimentoOtro.Checked == true)
            {
                tipoAlimentoProy = new TipoAlimentoProyecto();
                tipoAlimentoProy.tipoAlimento = new ParametroGenerico(rbTipo.TIPO_ALIMENTO_PT_OTRO);
                tipoAlimentoProy.detalle = NombreAlimentoOtro.Text;
                proyTecnicoSolicitud.tipoAlimento.Add(tipoAlimentoProy);
            }
            if (AlimentoPellet.Checked == true)
            {
                tipoAlimentoProy = new TipoAlimentoProyecto();
                tipoAlimentoProy.tipoAlimento = new ParametroGenerico(rbTipo.TIPO_ALIMENTO_PT_PELLET);
                proyTecnicoSolicitud.tipoAlimento.Add(tipoAlimentoProy);
            }
            

            proyTecnicoSolicitud.observaciones = observaciones.Text;


            if (Convert.ToInt32(TipoCultivo.SelectedValue) > 0)
            {
                proyTecnicoSolicitud.tipoCultivo = new ParametroGenerico(Convert.ToInt32(TipoCultivo.SelectedValue));
            }

            List<EspecieAutorizadaPT> List_EspecieAut = (List<EspecieAutorizadaPT>)ViewState["EspecieAut_ProyTecnico"];
            List<EstructuraTecnicaPT> List_EstructuraTecnica = (List<EstructuraTecnicaPT>)ViewState["EstructuraTecnica_ProyTecnico"];
            List<ProgrProduccionPT> List_ProgrProd = (List<ProgrProduccionPT>)ViewState["ProgrProd_ProyTecnico"];

            proyTecnicoSolicitud.especieAutProyTecnico = List_EspecieAut;
            proyTecnicoSolicitud.estructTecnicaProyTecnico = List_EstructuraTecnica;
            proyTecnicoSolicitud.progrProduccionProyTecnico = List_ProgrProd;

            bool resp = proyService.guardarProyectoTecnico(proyTecnicoSolicitud, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
            if (resp)
            {
                msgGrillaGral_1.Text = "Proyecto Técnico guardado exitosamente.";
                msgGrillaGral_1.Focus();
                Content_msgGrillaGral_1.Visible = true;
                GuardarProyTecnico.Visible = false;
                ModificarProyTecnico.Visible = true;
                
            }
            else
            {
                msgGrillaGral_1.Text = "No se ha guardado el Proyecto Técnico.";
                msgGrillaGral_1.Focus();
                Content_msgGrillaGral_1.Visible = true;
            }

            Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            UpdatePanelMensaje.Update();

        }

        protected void Modificar_ProyTecnico_Click(object sender, EventArgs e)
        {
            ProyectoTecnicoService proyService = new ProyectoTecnicoService();

            ProyectoTecnico proyTecnicoSolicitud = new ProyectoTecnico();

            proyTecnicoSolicitud.idSolicitud = Convert.ToInt32(IdSolicitud.Value);
            proyTecnicoSolicitud.IdProyectoTecnico = 0;
            if (!IdProyectoTecnico.Value.Equals(""))
            {
                proyTecnicoSolicitud.IdProyectoTecnico = Convert.ToInt32(IdProyectoTecnico.Value);  
            }
            proyTecnicoSolicitud.tipoAlimento = new List<TipoAlimentoProyecto>();
            proyTecnicoSolicitud.densidadSiembra = -1;
            proyTecnicoSolicitud.mangasPlasticasVal = -1;

            TipoAlimentoProyecto tipoAlimentoProy = new TipoAlimentoProyecto();


            if (AlimentoAlgaFresca.Checked == true)
            {
                tipoAlimentoProy = new TipoAlimentoProyecto();
                tipoAlimentoProy.tipoAlimento = new ParametroGenerico(rbTipo.TIPO_ALIMENTO_PT_ALGA);
                proyTecnicoSolicitud.tipoAlimento.Add(tipoAlimentoProy);

                //SET METODO CULTIVO ALGAS
                proyTecnicoSolicitud.metodoCultivoAlgas = new List<TipoAlimentoProyecto>();
                TipoAlimentoProyecto metCultivoAlgas = new TipoAlimentoProyecto();
                if (Algas_DirSustrato.Checked == true)
                {
                    metCultivoAlgas = new TipoAlimentoProyecto();
                    metCultivoAlgas.tipoAlimento = new ParametroGenerico(rbTipo.MET_ALGAS_DIR_SUSTRATO);
                    proyTecnicoSolicitud.metodoCultivoAlgas.Add(metCultivoAlgas);
                }
                if (Algas_IndirSustrato.Checked == true)
                {
                    metCultivoAlgas = new TipoAlimentoProyecto();
                    metCultivoAlgas.tipoAlimento = new ParametroGenerico(rbTipo.MET_ALGAS_INDIR_SUSTRATO);
                    proyTecnicoSolicitud.metodoCultivoAlgas.Add(metCultivoAlgas);
                }
                if (Algas_Suspendido.Checked == true)
                {
                    metCultivoAlgas = new TipoAlimentoProyecto();
                    metCultivoAlgas.tipoAlimento = new ParametroGenerico(rbTipo.MET_ALGAS_SUSPENDIDO);
                    proyTecnicoSolicitud.metodoCultivoAlgas.Add(metCultivoAlgas);
                }
                if (Algas_Estanque.Checked == true)
                {
                    metCultivoAlgas = new TipoAlimentoProyecto();
                    metCultivoAlgas.tipoAlimento = new ParametroGenerico(rbTipo.MET_ALGAS_ESTANQUE);
                    proyTecnicoSolicitud.metodoCultivoAlgas.Add(metCultivoAlgas);
                }
                if (Algas_Otro.Checked == true)
                {
                    metCultivoAlgas = new TipoAlimentoProyecto();
                    metCultivoAlgas.tipoAlimento = new ParametroGenerico(rbTipo.MET_ALGAS_OTRO);
                    metCultivoAlgas.detalle = AlgaOtroDef.Text;
                    proyTecnicoSolicitud.metodoCultivoAlgas.Add(metCultivoAlgas);
                }
                if (RadioButtonListUtilizaMangasPlasticas1.Checked == true)
                {
                    proyTecnicoSolicitud.mangasPlasticasVal = 1;
                }
                if (RadioButtonListUtilizaMangasPlasticas2.Checked == true)
                {
                    proyTecnicoSolicitud.mangasPlasticasVal = 0;
                }
                if (RadioButtonListUtilizaMangasPlasticas1.Checked == false && RadioButtonListUtilizaMangasPlasticas2.Checked == false)
                {
                    proyTecnicoSolicitud.mangasPlasticasVal = -1;
                }

                CompareValidator_TextBoxDensidadSiembra.Validate();

                if (!TextBoxDensidadSiembra.Text.Equals(""))
                {
                    proyTecnicoSolicitud.densidadSiembra = Convert.ToSingle(TextBoxDensidadSiembra.Text);
                }

                //SET TIPO FONDO
                proyTecnicoSolicitud.tipoFondo = new List<TipoAlimentoProyecto>();
                TipoAlimentoProyecto tFondo = new TipoAlimentoProyecto();
                if (TipoFondoDuro.Checked == true)
                {
                    tFondo = new TipoAlimentoProyecto();
                    tFondo.tipoAlimento = new ParametroGenerico(rbTipo.T_FONDO_DURO);
                    proyTecnicoSolicitud.tipoFondo.Add(tFondo);
                }
                if (TipoFondoSemi.Checked == true)
                {
                    tFondo = new TipoAlimentoProyecto();
                    tFondo.tipoAlimento = new ParametroGenerico(rbTipo.T_FONDO_SEMIDURO);
                    proyTecnicoSolicitud.tipoFondo.Add(tFondo);
                }
                if (TipoFondoBlando.Checked == true)
                {
                    tFondo = new TipoAlimentoProyecto();
                    tFondo.tipoAlimento = new ParametroGenerico(rbTipo.T_FONDO_BLANDO);
                    proyTecnicoSolicitud.tipoFondo.Add(tFondo);
                }
                if (TipoFondoOtro.Checked == true)
                {
                    tFondo = new TipoAlimentoProyecto();
                    tFondo.tipoAlimento = new ParametroGenerico(rbTipo.T_FONDO_OTRO);
                    tFondo.detalle = FondoOtroDef.Text;
                    proyTecnicoSolicitud.tipoFondo.Add(tFondo);
                }

            }
            if (AlimentoOtro.Checked == true)
            {
                tipoAlimentoProy = new TipoAlimentoProyecto();
                tipoAlimentoProy.tipoAlimento = new ParametroGenerico(rbTipo.TIPO_ALIMENTO_PT_OTRO);
                tipoAlimentoProy.detalle = NombreAlimentoOtro.Text;
                proyTecnicoSolicitud.tipoAlimento.Add(tipoAlimentoProy);
            }
            if (AlimentoPellet.Checked == true)
            {
                tipoAlimentoProy = new TipoAlimentoProyecto();
                tipoAlimentoProy.tipoAlimento = new ParametroGenerico(rbTipo.TIPO_ALIMENTO_PT_PELLET);
                proyTecnicoSolicitud.tipoAlimento.Add(tipoAlimentoProy);
            }


            proyTecnicoSolicitud.observaciones = observaciones.Text;


            if (Convert.ToInt32(TipoCultivo.SelectedValue) > 0)
            {
                proyTecnicoSolicitud.tipoCultivo = new ParametroGenerico(Convert.ToInt32(TipoCultivo.SelectedValue));
            }

            List<EspecieAutorizadaPT> List_EspecieAut = (List<EspecieAutorizadaPT>)ViewState["EspecieAut_ProyTecnico"];
            List<EstructuraTecnicaPT> List_EstructuraTecnica = (List<EstructuraTecnicaPT>)ViewState["EstructuraTecnica_ProyTecnico"];
            List<ProgrProduccionPT> List_ProgrProd = (List<ProgrProduccionPT>)ViewState["ProgrProd_ProyTecnico"];

            proyTecnicoSolicitud.especieAutProyTecnico = List_EspecieAut;
            proyTecnicoSolicitud.estructTecnicaProyTecnico = List_EstructuraTecnica;
            proyTecnicoSolicitud.progrProduccionProyTecnico = List_ProgrProd;

            usuario_logeado =  (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

            bool resp = proyService.modificarProyectoTecnico(proyTecnicoSolicitud, usuario_logeado.id_usuario, proyTecnicoSolicitud.idSolicitud);
            if (resp)
            {
                msgGrillaGral_1.Text = "Proyecto Técnico modificado exitosamente.";
                msgGrillaGral_1.Focus();
                Content_msgGrillaGral_1.Visible = true;
            }
            else
            {
                msgGrillaGral_1.Text = "No se ha modificado el Proyecto Técnico.";
                msgGrillaGral_1.Focus();
                Content_msgGrillaGral_1.Visible = true;
            }

            Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            UpdatePanelMensaje.Update();

        }

        protected void LimpiarEstructuraTecnica_Click(object sender, ImageClickEventArgs e)
        {
            this.limpiarProyTecnico("EstructuraTecnica");
        }
        protected void LimpiarProgramaProd_Click(object sender, ImageClickEventArgs e)
        {
            this.limpiarProyTecnico("ProgramaProd");
        }

        

        private void limpiarProyTecnico(string seccion) {
            switch (seccion) {

                case "EspecieAutorizada":
                    Carga_Combobox("EspecieAutorizada");
                    EspecieAutorizada.SelectedValue = "-1";
                    Carga_Combobox("GrupoEspecieAutorizadas");
                    GrupoEspecieAutorizadas.SelectedValue = "-1";
                    Carga_Combobox("EtapaDeCultivoAutorizadas");
                    EtapaDeCultivoAutorizadas.SelectedValue = "-1";
                    break;

                case "EstructuraTecnica":
                    Carga_Combobox("TipoEstructura");
                    TipoEstructura.SelectedValue = "-1";
                    Carga_Combobox("FormaEstructura");
                    FormaEstructura.SelectedValue = "-1";
                    Carga_Combobox("UnidadDeMedida");
                    UnidadDeMedida.SelectedValue = "-1";
                    TextBoxLargoM.Text = "";
                    TextBoxAnchoM.Text = "";
                    TextBoxAltoM.Text = "";
                    TextBoxDiametroM.Text = "";
                    Carga_Combobox("VolumenUnidadMedida");
                    VolumenUnidadMedida.SelectedValue = "-1";
                    Carga_Combobox("Anio");
                    Anio.SelectedValue = "-1";
                    TotalAcumuladoDimension.Text = "";
                    TotalAcumuladoNumero.Text = "";
                    TextBoxVolumenValorMedida.Text = "";
                    TipoAnioEstructuraTecnica_Selected(null, null);
                    Panel_Agregar_EstructuraTecnica.Visible = true;
                    Panel_Modificar_EstructuraTecnica.Visible = false;
                    UpdatePanel_EstructuraTecnica.Update();
                    break;

                case "ProgramaProd":
                    Carga_Combobox("EspecieProgramaProduccion");
                    EspecieProgramaProduccion.SelectedValue = "-1";
                    EtapaPorEspeciePP_OnSelectedIndexChanged(null, null); 
                    
                    Carga_Combobox("GrupoProgramaProduccion");
                    GrupoProgramaProduccion.SelectedValue = "-1";
                    EtapaPorGrupoPP_OnSelectedIndexChanged(null, null);
            
                    Carga_Combobox("EtapaCultivoProgramaProduccion");
                    EtapaCultivoProgramaProduccion.SelectedValue = "-1";

                    Carga_Combobox("UnidadProgramaProduccion");
                    UnidadProgramaProduccion.SelectedValue = "-1";
                    
                    Carga_Combobox("PesoPromedioEjemplares");
                    PesoPromedioEjemplares.SelectedValue = "-1";
                    DespliegaPesoProm_OnSelectedIndexChanged(null, null);
                    DensidadProgProd.Text = "";
                    ProdUltimoAnio.Text = "";
                    AnioProd1.Text = "";
                    AnioProd2.Text = "";
                    AnioProd3.Text = "";
                    AnioProd4.Text = "";
                    AnioProd5.Text = "";
                    Panel_AgregarProgrProd.Visible = true;
                    Panel_ModificarProgrProd.Visible = false;
                    UpdatePanel_ProgrProduccionPT.Update();
                    break;
            
            
            }
        
        }

        protected bool validaDec(string cadena) {

            Regex regex = new Regex(@"^[0-9]+(\,[0-9]{1,4})?$");

            if (regex.IsMatch(cadena))
            {
                return true;
            }

            return false;
        }

        private void validaDataMedidasEstructura() {

            CompareValidator_TextBoxLargoM.Validate();
            CompareValidator_TextBoxAnchoM.Validate();
            CompareValidator_TextBoxAltoM.Validate();
            CompareValidator_TextBoxDiametroM.Validate();
        }


//----------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------
        private void AgregarGrillaPT(string seccion)
        {
            int index = 0;
            switch (seccion)
            {
                case "EspecieAutorizada":
                    List<EspecieAutorizadaPT> List_EspecieAut = (List<EspecieAutorizadaPT>)ViewState["EspecieAut_ProyTecnico"];

                    index = 0;
                    if (List_EspecieAut == null)
                    {
                        List_EspecieAut = new List<EspecieAutorizadaPT>();
                    }
                    else
                    {
                        index = List_EspecieAut.Count;
                    }

                    EspecieAutorizadaPT esp_aut = (EspecieAutorizadaPT)this.CargaObjeto("EspecieAutorizada", "");
                    List<String> listaErroresEspAu = pTValidacion.validaEspecieAutorizada(esp_aut);
                    bool validaOk = false;

                    if (listaErroresEspAu.Count <= 0)
                    {

                        if (EspeciesRad.Checked == true)
                        {
                            esp_aut.idEspeciePT = 0;
                            esp_aut.accion = accion.INGRESAR;
                            esp_aut.index = Convert.ToInt32(index);
                            esp_aut.especie = new ParametroGenerico(Convert.ToInt32(EspecieAutorizada.SelectedValue), Convert.ToString(EspecieAutorizada.SelectedItem.Text));
                            esp_aut.etapaCultivo = new ParametroGenerico(Convert.ToInt32(EtapaDeCultivoAutorizadas.SelectedValue), Convert.ToString(EtapaDeCultivoAutorizadas.SelectedItem.Text));
                            esp_aut.grupoEspecie = new ParametroGenerico();
                            esp_aut.grupoEspecie = parametroDa.ObtenerEspecies(esp_aut.especie.id, null, 0);


                            List<String> listaErroresEspAuLista = pTValidacion.validaEspecieAutorizadaLista(esp_aut, List_EspecieAut);
                            if (listaErroresEspAuLista.Count <= 0)
                            {
                                List_EspecieAut.Add(esp_aut);
                                validaOk = true;
                            }
                            else
                            {
                                foreach (String error in listaErroresEspAuLista)
                                {
                                    Page.Validators.Add(new ValidationError("grupo1", error));
                                    validaOk = false;
                                }
                                UpdatePanel_MSG_EspecieAu.Update();
                            }


                        }
                        if (GrupoEspeciesRad.Checked == true)
                        {
                            List<ParametroGenerico> especiesGrupo = new List<ParametroGenerico>();
                            especiesGrupo = parametroDa.ListarEspecies(0, "", Convert.ToInt32(GrupoEspecieAutorizadas.SelectedValue));

                            if (especiesGrupo != null && especiesGrupo.Count() > 0)
                            {
                                foreach (ParametroGenerico paramAux in especiesGrupo)
                                {
                                    esp_aut = new EspecieAutorizadaPT();
                                    esp_aut.idEspeciePT = 0;
                                    esp_aut.accion = accion.INGRESAR;
                                    esp_aut.index = Convert.ToInt32(index);
                                    esp_aut.especie = new ParametroGenerico(paramAux.id, paramAux.descripcion);
                                    esp_aut.etapaCultivo = new ParametroGenerico(Convert.ToInt32(EtapaDeCultivoAutorizadas.SelectedValue), Convert.ToString(EtapaDeCultivoAutorizadas.SelectedItem.Text));
                                    esp_aut.grupoEspecie = new ParametroGenerico(Convert.ToInt32(GrupoEspecieAutorizadas.SelectedValue), Convert.ToString(GrupoEspecieAutorizadas.SelectedItem.Text));

                                    List<String> listaErroresEspAuLista = pTValidacion.validaEspecieAutorizadaLista(esp_aut, List_EspecieAut);
                                    if (listaErroresEspAuLista.Count <= 0)
                                    {
                                        List_EspecieAut.Add(esp_aut);
                                        validaOk = true;
                                        index++;
                                    }
                                    else
                                    {
                                        foreach (String error in listaErroresEspAuLista)
                                        {
                                            Page.Validators.Add(new ValidationError("grupo1", error));
                                        }
                                        UpdatePanel_MSG_EspecieAu.Update();
                                        validaOk = false;
                                        break;
                                    }
                                }
                            }

                        }

                        if (validaOk)
                        {
                            GridEspecieAutorizadaProyTecnico.DataSource = List_EspecieAut;
                            GridEspecieAutorizadaProyTecnico.DataBind();

                            ViewState["EspecieAut_ProyTecnico"] = (List<EspecieAutorizadaPT>)List_EspecieAut;
                            //Carga_Combobox("EspeciesEjemplar");
                            Carga_Combobox("EspecieProgramaProduccion");
                            Carga_Combobox("GrupoProgramaProduccion");
                            limpiarProyTecnico("EspecieAutorizada");
                            UpdatePanel_ProgrProduccionPT.Update();
                           
                        }


                    }
                    else
                    {
                        foreach (String error in listaErroresEspAu)
                        {
                            Page.Validators.Add(new ValidationError("grupo1", error));
                        }

                        UpdatePanel_MSG_EspecieAu.Update();
                        
                    }


                    break;

                case "EstructuraTecnica":
                    List<EstructuraTecnicaPT> List_EstructuraTecnica = (List<EstructuraTecnicaPT>)ViewState["EstructuraTecnica_ProyTecnico"];

                    index = 0;
                    if (List_EstructuraTecnica == null)
                    {
                        List_EstructuraTecnica = new List<EstructuraTecnicaPT>();
                    }
                    else
                    {
                        index = List_EstructuraTecnica.Count;
                    }

                    EstructuraTecnicaPT estructTec = new EstructuraTecnicaPT();

                    estructTec.accion = accion.INGRESAR;
                    estructTec.tipoEstructura = new ParametroGenerico(Convert.ToInt32(TipoEstructura.SelectedValue), Convert.ToString(TipoEstructura.SelectedItem.Text));
                    estructTec.formaEstructura = new ParametroGenerico(Convert.ToInt32(FormaEstructura.SelectedValue), Convert.ToString(FormaEstructura.SelectedItem.Text));
                    estructTec.unidadMedida = new ParametroGenerico(Convert.ToInt32(UnidadDeMedida.SelectedValue), Convert.ToString(UnidadDeMedida.SelectedItem.Text));
                    if (!TextBoxLargoM.Text.Equals(""))
                    {
                        estructTec.largo = Convert.ToSingle(TextBoxLargoM.Text);
                    }
                    if (!TextBoxAnchoM.Text.Equals(""))
                    {
                        estructTec.ancho = Convert.ToSingle(TextBoxAnchoM.Text);
                    }
                    if (!TextBoxAltoM.Text.Equals(""))
                    {
                        estructTec.alto = Convert.ToSingle(TextBoxAltoM.Text);
                    }
                    if (!TextBoxDiametroM.Text.Equals(""))
                    {
                        estructTec.diametro = Convert.ToSingle(TextBoxDiametroM.Text);
                    }

                    estructTec.volumenUnidadMedida = new ParametroGenerico(Convert.ToInt32(VolumenUnidadMedida.SelectedValue), Convert.ToString(VolumenUnidadMedida.SelectedItem.Text));
                    if (!TextBoxVolumenValorMedida.Text.Equals(""))
                    {
                        estructTec.volumenValorMedida = Convert.ToSingle(TextBoxVolumenValorMedida.Text);
                    }

                    estructTec.tipoAnio = new ParametroGenerico(Convert.ToInt32(Anio.SelectedValue), Convert.ToString(Anio.SelectedItem.Text));
                    estructTec.anios = new List<ValorParametroAnioPT>();

                    ValorParametroAnioPT valorParamAux = null;

                    if (TextBoxAnio1.Text != null && !TextBoxAnio1.Text.Equals(""))
                    {
                        valorParamAux = new ValorParametroAnioPT();
                        valorParamAux.idRegistro = 0;
                        valorParamAux.idclaveParametro = 0;
                        valorParamAux.anio = 1;
                        valorParamAux.valor = Convert.ToInt32(TextBoxAnio1.Text);
                        estructTec.anios.Add(valorParamAux);
                    }


                    if (TextBoxAnio2.Text != null && !TextBoxAnio2.Text.Equals(""))
                    {
                        valorParamAux = new ValorParametroAnioPT();
                        valorParamAux.idRegistro = 0;
                        valorParamAux.idclaveParametro = 0;
                        valorParamAux.anio = 2;
                        valorParamAux.valor = Convert.ToInt32(TextBoxAnio2.Text);
                        estructTec.anios.Add(valorParamAux);
                    }
                    if (TextBoxAnio3.Text != null && !TextBoxAnio3.Text.Equals(""))
                    {
                        valorParamAux = new ValorParametroAnioPT();
                        valorParamAux.idRegistro = 0;
                        valorParamAux.idclaveParametro = 0;
                        valorParamAux.anio = 3;
                        valorParamAux.valor = Convert.ToInt32(TextBoxAnio3.Text);
                        estructTec.anios.Add(valorParamAux);

                    }
                    if (TextBoxAnio4.Text != null && !TextBoxAnio4.Text.Equals(""))
                    {
                        valorParamAux = new ValorParametroAnioPT();
                        valorParamAux.idRegistro = 0;
                        valorParamAux.idclaveParametro = 0;
                        valorParamAux.anio = 4;
                        valorParamAux.valor = Convert.ToInt32(TextBoxAnio4.Text);
                        estructTec.anios.Add(valorParamAux);
                    }

                    if (TotalAcumuladoNumero.Text != null && !TotalAcumuladoNumero.Text.Equals(""))
                    {
                        estructTec.totalAcumNumero = Convert.ToSingle(TotalAcumuladoNumero.Text);
                    }
                    if (TotalAcumuladoDimension.Text != null && !TotalAcumuladoDimension.Text.Equals(""))
                    {
                        estructTec.totalAcumNumero = Convert.ToSingle(TotalAcumuladoDimension.Text);
                    }

                    List<String> listaErroresEstructTecnica = pTValidacion.validaEstructuraTecnica(estructTec);

                    if (listaErroresEstructTecnica.Count <= 0)
                    {
                        estructTec.idEstructPT = Convert.ToInt32(IdEstructProyTecnico.Value);
                        estructTec.index = Convert.ToInt32(index);

                        List_EstructuraTecnica.Add(estructTec);

                        GridEstructuraTecnicaProyTecnico.DataSource = List_EstructuraTecnica;
                        GridEstructuraTecnicaProyTecnico.DataBind();

                        ViewState["EstructuraTecnica_ProyTecnico"] = (List<EstructuraTecnicaPT>)List_EstructuraTecnica;
                        limpiarProyTecnico("EstructuraTecnica");


                    }
                    else
                    {

                        foreach (String error in listaErroresEstructTecnica)
                        {
                            Page.Validators.Add(new ValidationError("grupo2", error));
                        }

                        UpdatePanel_MSG_EstructuraTecnica.Update();
                       
                    }


                    break;

                case "ProgramaProd":

                    List<ProgrProduccionPT> List_ProgrProd = (List<ProgrProduccionPT>)ViewState["ProgrProd_ProyTecnico"];
                    index = 0;
                    if (List_ProgrProd == null)
                    {
                        List_ProgrProd = new List<ProgrProduccionPT>();
                    }
                    else
                    {
                        index = List_ProgrProd.Count;
                    }

                    ProgrProduccionPT progrProduccion = new ProgrProduccionPT();

                    progrProduccion.accion = accion.INGRESAR;
                    progrProduccion.idProgrProduccion = 0;
                    progrProduccion.index = Convert.ToInt32(index);
                    progrProduccion.accion = accion.INGRESAR;
                    progrProduccion.especie = new ParametroGenerico(Convert.ToInt32(EspecieProgramaProduccion.SelectedValue), "");
                    if (Convert.ToInt32(EspecieProgramaProduccion.SelectedValue)>0)
                    {
                       progrProduccion.especie.descripcion = Convert.ToString(EspecieProgramaProduccion.SelectedItem.Text);
                    }
                    progrProduccion.grupo = new ParametroGenerico(Convert.ToInt32(GrupoProgramaProduccion.SelectedValue), "");
                    if (Convert.ToInt32(GrupoProgramaProduccion.SelectedValue)>0)
                    {
                       progrProduccion.grupo.descripcion = Convert.ToString(GrupoProgramaProduccion.SelectedItem.Text);
                    }
                    progrProduccion.etapaCultivo = new ParametroGenerico(Convert.ToInt32(EtapaCultivoProgramaProduccion.SelectedValue), Convert.ToString(EtapaCultivoProgramaProduccion.SelectedItem.Text));
                    progrProduccion.tipoUnidProgramaProd = new ParametroGenerico(Convert.ToInt32(UnidadProgramaProduccion.SelectedValue), Convert.ToString(UnidadProgramaProduccion.SelectedItem.Text));
                    progrProduccion.tipoPesoPromEjemplares = new ParametroGenerico(Convert.ToInt32(PesoPromedioEjemplares.SelectedValue), Convert.ToString(PesoPromedioEjemplares.SelectedItem.Text));
                    if (PesoPromSinRango != null && !PesoPromSinRango.Text.Equals(""))
                    {
                        progrProduccion.pesoPromSR = Convert.ToSingle(PesoPromSinRango.Text);
                    }
                    if (PesoRango1 != null && !PesoRango1.Text.Equals(""))
                    {
                        progrProduccion.pesoPromR1 = Convert.ToSingle(PesoRango1.Text);
                    }
                    if (PesoRango2 != null && !PesoRango2.Text.Equals(""))
                    {
                        progrProduccion.pesoPromR2 = Convert.ToSingle(PesoRango2.Text);
                    }
                    if (!DensidadProgProd.Text.Equals(""))
                    {
                        progrProduccion.densidad = Convert.ToSingle(DensidadProgProd.Text);
                    }
                    if (!ProdUltimoAnio.Text.Equals(""))
                    {
                        progrProduccion.produccionUltimoAnio = Convert.ToSingle(ProdUltimoAnio.Text);
                    }

                    progrProduccion.aniosProgrProd = new List<ValorParametroAnioPT>();

                    ValorParametroAnioPT valorParamAnioPP = null;
                    valorParamAnioPP = new ValorParametroAnioPT();
                    valorParamAnioPP.idRegistro = 0;
                    valorParamAnioPP.idclaveParametro = 0;
                    valorParamAnioPP.anio = 1;
                    valorParamAnioPP.valorProgrProd = Convert.ToSingle(AnioProd1.Text);

                    progrProduccion.aniosProgrProd.Add(valorParamAnioPP);

                    valorParamAnioPP = new ValorParametroAnioPT();
                    valorParamAnioPP.idRegistro = 0;
                    valorParamAnioPP.idclaveParametro = 0;
                    valorParamAnioPP.anio = 2;
                    valorParamAnioPP.valorProgrProd = Convert.ToSingle(AnioProd2.Text);
                    progrProduccion.aniosProgrProd.Add(valorParamAnioPP);

                    valorParamAnioPP = new ValorParametroAnioPT();
                    valorParamAnioPP.idRegistro = 0;
                    valorParamAnioPP.idclaveParametro = 0;
                    valorParamAnioPP.anio = 3;
                    valorParamAnioPP.valorProgrProd = Convert.ToSingle(AnioProd3.Text);
                    progrProduccion.aniosProgrProd.Add(valorParamAnioPP);

                    valorParamAnioPP = new ValorParametroAnioPT();
                    valorParamAnioPP.idRegistro = 0;
                    valorParamAnioPP.idclaveParametro = 0;
                    valorParamAnioPP.anio = 4;
                    valorParamAnioPP.valorProgrProd = Convert.ToSingle(AnioProd4.Text);
                    progrProduccion.aniosProgrProd.Add(valorParamAnioPP);

                    valorParamAnioPP = new ValorParametroAnioPT();
                    valorParamAnioPP.idRegistro = 0;
                    valorParamAnioPP.idclaveParametro = 0;
                    valorParamAnioPP.anio = 5;
                    valorParamAnioPP.valorProgrProd = Convert.ToSingle(AnioProd5.Text);
                    progrProduccion.aniosProgrProd.Add(valorParamAnioPP);

                    List<String> listaErroresProgrProd = pTValidacion.validaProgramaProduccion(progrProduccion);
                    
                    if (listaErroresProgrProd.Count <= 0)
                    {
                        List<String> listaErroresProgrProdLista = pTValidacion.validaProgrProduccionLista(progrProduccion, List_ProgrProd);
                        if (listaErroresProgrProdLista.Count <= 0)
                        {
                            List_ProgrProd.Add(progrProduccion);

                            GridProgrProdProyTecnico.DataSource = List_ProgrProd;
                            GridProgrProdProyTecnico.DataBind();

                            ViewState["ProgrProd_ProyTecnico"] = (List<ProgrProduccionPT>)List_ProgrProd;
                            this.limpiarProyTecnico("ProgramaProd");
                        }
                        else {
                            foreach (String error in listaErroresProgrProdLista)
                            {
                                Page.Validators.Add(new ValidationError("grupo4", error));
                            }

                            UpdatePanel_MSG_ProgrProduccionPT.Update();
                        
                        }
                        

                    }
                    else
                    {
                        foreach (String error in listaErroresProgrProd)
                        {
                            Page.Validators.Add(new ValidationError("grupo4", error));
                        }
                        UpdatePanel_MSG_ProgrProduccionPT.Update();
                       
                    }

                    break;


            }

        }

        protected void ModificarGrillaPT(string seccion)
        {

            int index = 0;
            switch (seccion)
            {
                case "EstructuraTecnica":

                    index = Convert.ToInt32(IndexEstructProyTecnico.Value);

                    List<EstructuraTecnicaPT> List_EstructuraTecnica = (List<EstructuraTecnicaPT>)ViewState["EstructuraTecnica_ProyTecnico"];
                    foreach (EstructuraTecnicaPT estructTec in List_EstructuraTecnica)
                    {
                        if (estructTec.index.Equals(index))
                        {
                            estructTec.index = Convert.ToInt32(index);
                            estructTec.tipoEstructura = new ParametroGenerico(Convert.ToInt32(TipoEstructura.SelectedValue), Convert.ToString(TipoEstructura.SelectedItem.Text));
                            estructTec.formaEstructura = new ParametroGenerico(Convert.ToInt32(FormaEstructura.SelectedValue), Convert.ToString(FormaEstructura.SelectedItem.Text));
                            estructTec.unidadMedida = new ParametroGenerico(Convert.ToInt32(UnidadDeMedida.SelectedValue), Convert.ToString(UnidadDeMedida.SelectedItem.Text));
                            estructTec.largo = Convert.ToSingle(TextBoxLargoM.Text);
                            estructTec.ancho = Convert.ToSingle(TextBoxAnchoM.Text);
                            estructTec.alto = Convert.ToSingle(TextBoxAltoM.Text);
                            estructTec.diametro = Convert.ToSingle(TextBoxDiametroM.Text);
                            estructTec.volumenUnidadMedida = new ParametroGenerico(Convert.ToInt32(VolumenUnidadMedida.SelectedValue), Convert.ToString(VolumenUnidadMedida.SelectedItem.Text));
                            estructTec.volumenValorMedida = Convert.ToSingle(TextBoxVolumenValorMedida.Text);
                            estructTec.tipoAnio = new ParametroGenerico(Convert.ToInt32(Anio.SelectedValue), Convert.ToString(Anio.SelectedItem.Text));
                            estructTec.anios = new List<ValorParametroAnioPT>();

                            ValorParametroAnioPT valorParamAux = null;

                            valorParamAux = new ValorParametroAnioPT();
                            valorParamAux.idRegistro = 0;
                            valorParamAux.idclaveParametro = 0;
                            valorParamAux.anio = 1;
                            valorParamAux.valor = Convert.ToInt32(TextBoxAnio1.Text);
                            estructTec.anios.Add(valorParamAux);
                            if (TextBoxAnio2.Text != null && !TextBoxAnio2.Text.Equals(""))
                            {
                                valorParamAux = new ValorParametroAnioPT();
                                valorParamAux.idRegistro = 0;
                                valorParamAux.idclaveParametro = 0;
                                valorParamAux.anio = 2;
                                valorParamAux.valor = Convert.ToInt32(TextBoxAnio2.Text);
                                estructTec.anios.Add(valorParamAux);
                            }
                            if (TextBoxAnio3.Text != null && !TextBoxAnio3.Text.Equals(""))
                            {
                                valorParamAux = new ValorParametroAnioPT();
                                valorParamAux.idRegistro = 0;
                                valorParamAux.idclaveParametro = 0;
                                valorParamAux.anio = 3;
                                valorParamAux.valor = Convert.ToInt32(TextBoxAnio3.Text);
                                estructTec.anios.Add(valorParamAux);

                            }
                            valorParamAux = new ValorParametroAnioPT();
                            valorParamAux.idRegistro = 0;
                            valorParamAux.idclaveParametro = 0;
                            valorParamAux.anio = 4;
                            valorParamAux.valor = Convert.ToInt32(TextBoxAnio4.Text);
                            estructTec.anios.Add(valorParamAux);

                            estructTec.totalAcumNumero = Convert.ToSingle(TotalAcumuladoNumero.Text);
                            if (TotalAcumuladoDimension.Text != null && !TotalAcumuladoDimension.Text.Equals(""))
                            {
                                estructTec.totalAcumNumero = Convert.ToSingle(TotalAcumuladoDimension.Text);
                            }
                            List<String> listaErroresEstructTecnica = pTValidacion.validaEstructuraTecnica(estructTec);

                            if (listaErroresEstructTecnica.Count <= 0)
                            {
                                if (estructTec.idEstructPT>0)
                                {
                                 estructTec.accion = accion.MODIFICAR;
                               }
                               
                                
                                GridEstructuraTecnicaProyTecnico.DataSource = List_EstructuraTecnica;
                                GridEstructuraTecnicaProyTecnico.DataBind();

                                ViewState["EstructuraTecnica_ProyTecnico"] = (List<EstructuraTecnicaPT>)List_EstructuraTecnica;
                                this.limpiarProyTecnico("EstructuraTecnica");
                                UpdatePanel_EstructuraTecnica.Update();

                            }
                            else
                            {

                                foreach (String error in listaErroresEstructTecnica)
                                {
                                    Page.Validators.Add(new ValidationError("grupo2", error));
                                }

                                UpdatePanel_MSG_EstructuraTecnica.Update();

                            }
                            break;
                        }
                    }


                    break;

                case "ProgramaProd":

                    index = Convert.ToInt32(IndexProd_ProyTecnico.Value);
                    List<ProgrProduccionPT> List_ProgrProd = (List<ProgrProduccionPT>)ViewState["ProgrProd_ProyTecnico"];
                   
                    ProgrProduccionPT progrProdFila = new ProgrProduccionPT();
                    progrProdFila = (ProgrProduccionPT)this.obtenerFormularioSeccion("ProgramaProd");
                    progrProdFila.index = Convert.ToInt32(index);

                    List<String> listaErroresProgrProd = pTValidacion.validaProgramaProduccion(progrProdFila);
                    if (listaErroresProgrProd.Count <= 0)
                    {
                        List<String> listaErroresProgrProdLista = pTValidacion.validaProgrProduccionLista(progrProdFila, List_ProgrProd);
                        if (listaErroresProgrProdLista.Count <= 0)
                        {
                            foreach (ProgrProduccionPT progrProduccion in List_ProgrProd)
                            {
                                if (progrProduccion.index.Equals(index))
                                {
                                    progrProduccion.especie = progrProdFila.especie;
                                    progrProduccion.grupo = progrProdFila.grupo;
                                    progrProduccion.etapaCultivo = progrProdFila.etapaCultivo;
                                    progrProduccion.tipoUnidProgramaProd = progrProdFila.tipoUnidProgramaProd;
                                    progrProduccion.tipoPesoPromEjemplares = progrProdFila.tipoPesoPromEjemplares;
                                    progrProduccion.pesoPromR1 = progrProdFila.pesoPromR1;
                                    progrProduccion.pesoPromR2 = progrProdFila.pesoPromR2;
                                    progrProduccion.pesoPromSR = progrProdFila.pesoPromSR;
                                    progrProduccion.densidad = progrProdFila.densidad;
                                    progrProduccion.produccionUltimoAnio = progrProdFila.produccionUltimoAnio;

                                    progrProduccion.aniosProgrProd = progrProdFila.aniosProgrProd;

                                    if (progrProduccion.idProgrProduccion>0)
                                    {
                                        progrProduccion.accion = accion.MODIFICAR; 
                                    }

                                    break;

                                }
                            }
                           
                           GridProgrProdProyTecnico.DataSource = List_ProgrProd;
                           GridProgrProdProyTecnico.DataBind();

                           ViewState["ProgrProd_ProyTecnico"] = (List<ProgrProduccionPT>)List_ProgrProd;
                           this.limpiarProyTecnico("ProgramaProd");
                        }
                        else
                        {
                           foreach (String error in listaErroresProgrProdLista)
                           {
                              Page.Validators.Add(new ValidationError("grupo4", error));
                           }
                           
                           UpdatePanel_MSG_ProgrProduccionPT.Update();
                        }
                     }
                     else
                     {
                        foreach (String error in listaErroresProgrProd)
                        {
                           Page.Validators.Add(new ValidationError("grupo4", error));
                        }
                        
                        UpdatePanel_MSG_ProgrProduccionPT.Update();
                     }

                    break;

            }

        }

        private void CargarGrillaPT(string seccion, ProyectoTecnico proyectoTec)
        {
            switch (seccion)
            {
                case "EspecieAutorizada":


                    List<EspecieAutorizadaPT> List_EspecieAut = new List<EspecieAutorizadaPT>();

                    if (proyectoTec != null && proyectoTec.especieAutProyTecnico != null && proyectoTec.especieAutProyTecnico.Count > 0)
                    {
                        List_EspecieAut = proyectoTec.especieAutProyTecnico;
                    }
                    else
                    {
                        List_EspecieAut = (List<EspecieAutorizadaPT>)ViewState["EspecieAut_ProyTecnico"];
                    }

                    if (List_EspecieAut == null)
                    {
                        List_EspecieAut = new List<EspecieAutorizadaPT>();
                    }
                    GridEspecieAutorizadaProyTecnico.DataSource = List_EspecieAut;
                    GridEspecieAutorizadaProyTecnico.DataBind();
                   // GridEspecieAutorizadaProyTecnico.Visible = true;

                    ViewState["EspecieAut_ProyTecnico"] = (List<EspecieAutorizadaPT>)List_EspecieAut;


                    break;

                case "EstructuraTecnica":
                    List<EstructuraTecnicaPT> List_EstructuraTecnica = new List<EstructuraTecnicaPT>();
                    if (proyectoTec != null && proyectoTec.estructTecnicaProyTecnico != null && proyectoTec.estructTecnicaProyTecnico.Count > 0)
                    {
                        List_EstructuraTecnica = proyectoTec.estructTecnicaProyTecnico;
                    }
                    else
                    {
                        List_EstructuraTecnica = (List<EstructuraTecnicaPT>)ViewState["EstructuraTecnica_ProyTecnico"];
                    }
                    if (List_EstructuraTecnica == null)
                    {
                        List_EstructuraTecnica = new List<EstructuraTecnicaPT>();
                    }

                    GridEstructuraTecnicaProyTecnico.DataSource = List_EstructuraTecnica;
                    GridEstructuraTecnicaProyTecnico.DataBind();
                  //GridEstructuraTecnicaProyTecnico.Visible = true;

                    ViewState["EstructuraTecnica_ProyTecnico"] = (List<EstructuraTecnicaPT>)List_EstructuraTecnica;
                    UpdatePanel_EstructuraTecnica.Update();

                    break;

                case "ProgramaProd":

                    List<ProgrProduccionPT> List_ProgrProd = new List<ProgrProduccionPT>();
                    if (proyectoTec != null && proyectoTec.progrProduccionProyTecnico != null && proyectoTec.progrProduccionProyTecnico.Count > 0)
                    {
                        List_ProgrProd = proyectoTec.progrProduccionProyTecnico;
                    }
                    else
                    {
                        List_ProgrProd = (List<ProgrProduccionPT>)ViewState["ProgrProd_ProyTecnico"];
                    }
                    if (List_ProgrProd == null)
                    {
                        List_ProgrProd = new List<ProgrProduccionPT>();
                    }
                    GridProgrProdProyTecnico.DataSource = List_ProgrProd;
                    GridProgrProdProyTecnico.DataBind();
               //     GridProgrProdProyTecnico.Visible = true;

                    ViewState["ProgrProd_ProyTecnico"] = (List<ProgrProduccionPT>)List_ProgrProd;
                    UpdatePanel_ProgrProduccionPT.Update();

                    break;
            }

        }

        //---------------------------------------------------
        
        protected void GridEspecieAutorizadaProyTecnico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Eliminar":
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridEspecieAutorizadaProyTecnico.EditIndex = -1;
                    EliminarGrillaPT(index, "EspecieAutorizada");
                   // CargarGrillaPT("EspecieAutorizada", null);
                    break;
            };
        }

        protected void GridEspecieAutorizadaProyTecnico_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //ACCION
                HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
                if (hidden_accion != null && !hidden_accion.Value.Equals("") && (Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR || Convert.ToInt32(hidden_accion.Value) == accion.IGNORAR))
                {
                    e.Row.Attributes["style"] = "display:none";
                };
                // Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar?')");
                    boton_eliminar.Visible = true;
                };

            };
        }



        protected void GridEstructuraTecnicaProyTecnico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ProyectoTecnicoService proyService = new ProyectoTecnicoService();

            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int idEstructProyTecnico = Convert.ToInt32(arg[0]);
            int index = Convert.ToInt32(arg[1]);

            switch (e.CommandName)
            {
                case "Eliminar":
                    GridEspecieAutorizadaProyTecnico.EditIndex = -1;
                    EliminarGrillaPT(index, "EstructuraTecnica");
                    break;
                case "Modificar":

                    List<EstructuraTecnicaPT> List_EstructuraTecnica = (List<EstructuraTecnicaPT>)ViewState["EstructuraTecnica_ProyTecnico"];

                    foreach (EstructuraTecnicaPT estructuraTec in List_EstructuraTecnica)
                    {
                        if (estructuraTec.index.Equals(Convert.ToInt32(index)))
                        {
                            TipoEstructura.SelectedValue = Convert.ToString(estructuraTec.tipoEstructura.id);
                            FormaPorEstructura_OnSelectedIndexChanged(null, null);
                            FormaEstructura.SelectedValue = Convert.ToString(estructuraTec.formaEstructura.id);
                            ManejoCamposEstructuraMedidasChanged(null, null);
                            UnidadDeMedida.SelectedValue = Convert.ToString(estructuraTec.unidadMedida.id);
                            TextBoxLargoM.Text = Convert.ToString(estructuraTec.largo);
                            TextBoxAnchoM.Text = Convert.ToString(estructuraTec.ancho);
                            TextBoxAltoM.Text = Convert.ToString(estructuraTec.alto);
                            TextBoxDiametroM.Text = Convert.ToString(estructuraTec.diametro);
                            if (estructuraTec.volumenUnidadMedida != null && estructuraTec.volumenUnidadMedida.id > 0)
                            {
                                VolumenUnidadMedida.SelectedValue = Convert.ToString(estructuraTec.volumenUnidadMedida.id);
                            }
                            else
                            {
                                VolumenUnidadMedida.Items.Clear();
                                VolumenUnidadMedida.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                            }

                            TextBoxVolumenValorMedida.Text = Convert.ToString(estructuraTec.volumenValorMedida);
                            Anio.SelectedValue = Convert.ToString(estructuraTec.tipoAnio.id);
                            TipoAnioEstructuraTecnica_Selected(null, null);
                            if (estructuraTec.anios != null && estructuraTec.anios.Count > 0)
                            {
                                foreach (ValorParametroAnioPT param in estructuraTec.anios)
                                {
                                    if (param.anio == 1)
                                    {
                                        TextBoxAnio1.Text = Convert.ToString(param.valor);
                                    }
                                    else if (param.anio == 2)
                                    {
                                        TextBoxAnio2.Text = Convert.ToString(param.valor);
                                    }
                                    else if (param.anio == 3)
                                    {
                                        TextBoxAnio3.Text = Convert.ToString(param.valor);
                                    }
                                    else if (param.anio == 4)
                                    {
                                        TextBoxAnio4.Text = Convert.ToString(param.valor);
                                    }
                                }
                            }

                            TotalAcumuladoNumero.Text = Convert.ToString(estructuraTec.totalAcumNumero);
                            TotalAcumuladoDimension.Text = Convert.ToString(estructuraTec.totalAcumDim);
                            CalculoVolumenAutomaticoChange(null, null);

                            IdEstructProyTecnico.Value = Convert.ToString(estructuraTec.idEstructPT);
                            IndexEstructProyTecnico.Value = Convert.ToString(index);

                            Panel_Agregar_EstructuraTecnica.Visible = false;
                            Panel_Modificar_EstructuraTecnica.Visible = true;

                            break;
                        }
                    }
                    break;

            };
        }

        protected void GridEstructuraTecnicaProyTecnico_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //ACCION
                HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
                if (hidden_accion != null && !hidden_accion.Value.Equals("") && (Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR || Convert.ToInt32(hidden_accion.Value) == accion.IGNORAR))
                {
                    e.Row.Attributes["style"] = "display:none";
                };

                // Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar?')");
                    boton_eliminar.Visible = true;
                };
                // Modificar
                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_eliminar != null)
                {
                    boton_modificar.Visible = true;
                };

            };
        }

        protected void GridProgrProdProyTecnico_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //ACCION
                HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
                if (hidden_accion != null && !hidden_accion.Value.Equals("") && (Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR || Convert.ToInt32(hidden_accion.Value) == accion.IGNORAR))
                {
                    e.Row.Attributes["style"] = "display:none";
                };
                // Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar?')");
                    boton_eliminar.Visible = true;
                };
                // Modificar
                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_modificar != null)
                {
                    boton_modificar.Visible = true;
                };


            };
        }

        protected void GridProgrProdProyTecnico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int idEstructProyTecnico = Convert.ToInt32(arg[0]);
            int index = Convert.ToInt32(arg[1]);

            switch (e.CommandName)
            {
                case "Eliminar":
                    GridProgrProdProyTecnico.EditIndex = -1;
                    EliminarGrillaPT(index, "ProgramaProd");
                   // CargarGrillaPT("ProgramaProd", null);
                    break;
                case "Modificar":

                    List<ProgrProduccionPT> List_ProgrProd = (List<ProgrProduccionPT>)ViewState["ProgrProd_ProyTecnico"];

                    foreach (ProgrProduccionPT progrProduccionAux in List_ProgrProd)
                    {
                        if (progrProduccionAux.index.Equals(Convert.ToInt32(index)))
                        {
                            if (progrProduccionAux.especie != null && progrProduccionAux.especie.id > 0)
                            {
                                EspecieProgramaProduccion.SelectedValue = Convert.ToString(progrProduccionAux.especie.id);
                                EtapaPorEspeciePP_OnSelectedIndexChanged(null, null);
                            }
                            else {
                                EspecieProgramaProduccion.SelectedValue = "-1";
                            }
                            
                            if (progrProduccionAux.grupo != null && progrProduccionAux.grupo.id > 0)
                            {
                                GrupoProgramaProduccion.SelectedValue = Convert.ToString(progrProduccionAux.grupo.id);
                                EtapaPorGrupoPP_OnSelectedIndexChanged(null,null);
                            }
                            else
                            {
                                GrupoProgramaProduccion.SelectedValue = "-1";
                            }

                            EtapaCultivoProgramaProduccion.SelectedValue = Convert.ToString(progrProduccionAux.etapaCultivo.id);

                            UnidadProgramaProduccion.SelectedValue = Convert.ToString(progrProduccionAux.tipoUnidProgramaProd.id);
                            PesoPromedioEjemplares.SelectedValue = Convert.ToString(progrProduccionAux.tipoPesoPromEjemplares.id);
                            DespliegaPesoProm_OnSelectedIndexChanged(null, null);

                            if (progrProduccionAux.tipoPesoPromEjemplares.id == rbTipo.PESO_PROM_SIN_RANGO)
                            {
                                PesoPromSinRango.Text = Convert.ToString(progrProduccionAux.pesoPromSR);
                            }
                            else if (progrProduccionAux.tipoPesoPromEjemplares.id == rbTipo.PESO_PROM_RANGO)
                            {
                                PesoRango1.Text = Convert.ToString(progrProduccionAux.pesoPromR1);
                                PesoRango2.Text = Convert.ToString(progrProduccionAux.pesoPromR2);
                            }

                            DensidadProgProd.Text = Convert.ToString(progrProduccionAux.densidad);
                            ProdUltimoAnio.Text = Convert.ToString(progrProduccionAux.produccionUltimoAnio);

                            foreach (ValorParametroAnioPT valorAnio in progrProduccionAux.aniosProgrProd)
                            {
                                if (valorAnio.anio == 1)
                                {
                                    AnioProd1.Text = Convert.ToString(valorAnio.valorProgrProd);
                                }
                                else if (valorAnio.anio == 2)
                                {
                                    AnioProd2.Text = Convert.ToString(valorAnio.valorProgrProd);
                                }
                                else if (valorAnio.anio == 3)
                                {
                                    AnioProd3.Text = Convert.ToString(valorAnio.valorProgrProd);
                                }
                                else if (valorAnio.anio == 4)
                                {
                                    AnioProd4.Text = Convert.ToString(valorAnio.valorProgrProd);
                                }
                                else if (valorAnio.anio == 5)
                                {
                                    AnioProd5.Text = Convert.ToString(valorAnio.valorProgrProd);
                                }
                            }

                            IdProgrProd_ProyTecnico.Value = Convert.ToString(progrProduccionAux.idProgrProduccion);
                            IndexProd_ProyTecnico.Value = Convert.ToString(index);
                            Panel_AgregarProgrProd.Visible = false;
                            Panel_ModificarProgrProd.Visible = true;


                        }
                    }

                    break;

            };
        }

        protected Object obtenerFormularioSeccion(string seccion) {

            Object ob = new Object();

            switch (seccion) {
                case "ProgramaProd":
                    ProgrProduccionPT progrProduccion = new ProgrProduccionPT();
                    progrProduccion.especie = new ParametroGenerico(Convert.ToInt32(EspecieProgramaProduccion.SelectedValue), "");
                    if (Convert.ToInt32(EspecieProgramaProduccion.SelectedValue) > 0)
                    {
                        progrProduccion.especie.descripcion = Convert.ToString(EspecieProgramaProduccion.SelectedItem.Text);
                    }
                    progrProduccion.grupo = new ParametroGenerico(Convert.ToInt32(GrupoProgramaProduccion.SelectedValue), "");
                    if (Convert.ToInt32(GrupoProgramaProduccion.SelectedValue) > 0)
                    {
                        progrProduccion.grupo.descripcion = Convert.ToString(GrupoProgramaProduccion.SelectedItem.Text);
                    }

                    progrProduccion.etapaCultivo = new ParametroGenerico(Convert.ToInt32(EtapaCultivoProgramaProduccion.SelectedValue), Convert.ToString(EtapaCultivoProgramaProduccion.SelectedItem.Text));
                    progrProduccion.tipoUnidProgramaProd = new ParametroGenerico(Convert.ToInt32(UnidadProgramaProduccion.SelectedValue), Convert.ToString(UnidadProgramaProduccion.SelectedItem.Text));
                    progrProduccion.tipoPesoPromEjemplares = new ParametroGenerico(Convert.ToInt32(PesoPromedioEjemplares.SelectedValue), Convert.ToString(PesoPromedioEjemplares.SelectedItem.Text));
                    if (Convert.ToInt32(PesoPromedioEjemplares.SelectedValue) == rbTipo.PESO_PROM_RANGO)
                    {
                        if (PesoRango1 != null && !PesoRango1.Text.Equals(""))
                        {
                            progrProduccion.pesoPromR1 = Convert.ToSingle(PesoRango1.Text);
                        }
                        if (PesoRango2 != null && !PesoRango2.Text.Equals(""))
                        {
                            progrProduccion.pesoPromR2 = Convert.ToSingle(PesoRango2.Text);
                        }

                        progrProduccion.pesoPromSR = Convert.ToSingle(0);
                    }
                    if (Convert.ToInt32(PesoPromedioEjemplares.SelectedValue) == rbTipo.PESO_PROM_SIN_RANGO)
                    {
                        if (PesoPromSinRango != null && !PesoPromSinRango.Text.Equals(""))
                        {
                            progrProduccion.pesoPromSR = Convert.ToSingle(PesoPromSinRango.Text);
                        }
                        progrProduccion.pesoPromR1 = Convert.ToSingle(0);
                        progrProduccion.pesoPromR2 = Convert.ToSingle(0);
                    }

                    if (!DensidadProgProd.Text.Equals(""))
                    {
                        progrProduccion.densidad = Convert.ToSingle(DensidadProgProd.Text);
                    }
                    if (!ProdUltimoAnio.Text.Equals(""))
                    {
                        progrProduccion.produccionUltimoAnio = Convert.ToSingle(ProdUltimoAnio.Text);
                    }

                    progrProduccion.aniosProgrProd = new List<ValorParametroAnioPT>();

                    ValorParametroAnioPT valorParamAnioPP = null;
                    valorParamAnioPP = new ValorParametroAnioPT();
                    valorParamAnioPP.idRegistro = 0;
                    valorParamAnioPP.idclaveParametro = 0;
                    valorParamAnioPP.anio = 1;
                    valorParamAnioPP.valorProgrProd = Convert.ToSingle(AnioProd1.Text);

                    progrProduccion.aniosProgrProd.Add(valorParamAnioPP);

                    valorParamAnioPP = new ValorParametroAnioPT();
                    valorParamAnioPP.idRegistro = 0;
                    valorParamAnioPP.idclaveParametro = 0;
                    valorParamAnioPP.anio = 2;
                    valorParamAnioPP.valorProgrProd = Convert.ToSingle(AnioProd2.Text);

                    progrProduccion.aniosProgrProd.Add(valorParamAnioPP);

                    valorParamAnioPP = new ValorParametroAnioPT();
                    valorParamAnioPP.idRegistro = 0;
                    valorParamAnioPP.idclaveParametro = 0;
                    valorParamAnioPP.anio = 3;
                    valorParamAnioPP.valorProgrProd = Convert.ToSingle(AnioProd3.Text);

                    progrProduccion.aniosProgrProd.Add(valorParamAnioPP);

                    valorParamAnioPP = new ValorParametroAnioPT();
                    valorParamAnioPP.idRegistro = 0;
                    valorParamAnioPP.idclaveParametro = 0;
                    valorParamAnioPP.anio = 4;
                    valorParamAnioPP.valorProgrProd = Convert.ToSingle(AnioProd4.Text);

                    progrProduccion.aniosProgrProd.Add(valorParamAnioPP);

                    valorParamAnioPP = new ValorParametroAnioPT();
                    valorParamAnioPP.idRegistro = 0;
                    valorParamAnioPP.idclaveParametro = 0;
                    valorParamAnioPP.anio = 5;
                    valorParamAnioPP.valorProgrProd = Convert.ToSingle(AnioProd5.Text);

                    progrProduccion.aniosProgrProd.Add(valorParamAnioPP);

                    ob = progrProduccion;

                    break;
            
            }
            return ob;

            
        
        }

      
    }
    
}