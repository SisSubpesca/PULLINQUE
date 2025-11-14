using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.servicios.estados;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.usuario;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.reportes;
using System.Collections.Generic;
using SubPesca.Utilidades;


//@used

namespace SubPesca.Administrador.Reportes
{
    public partial class resumenEstados : System.Web.UI.Page
    {

        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        EstadoService estadoService = new EstadoService();
        ReporteService reporteService = new ReporteService();
        TipoDA tipoDa = new TipoDA();
        ComunaDA comunaDA = new LogicaNegocio.cl.subpesca.rb.common.ComunaDA();
        RegionDA regionDA = new LogicaNegocio.cl.subpesca.rb.common.RegionDA();
        ProvinciaDA provinciaDA = new LogicaNegocio.cl.subpesca.rb.common.ProvinciaDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        UsuarioDA usuarioDA = new UsuarioDA();

        


        // PAGE LOAD
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {


                Datos.Entidades.Usuario.Serializable  usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (usuario_logeado == null)
                {
                    Response.Redirect("~/ingreso.aspx");
                }



                if (usuarioDA.aplicaUsuarioDespliegueFiltro(usuario_logeado.id_usuario)) {
                    PanelSectorialista.Visible = true;
                }


                // Inicializamos el Hashtable contenedor de los filtros de búsqueda
                Initialize_HT_ListEstados();

                // Inicializamos el formulario
                Initialize_Form();

                // Cargamos la grilla
                CargaGrilla();
                CargaGrillaReposicion();
            };
        }


        // PROCEDIMIENTOS GENERALES
        protected void Initialize_HT_ListEstados()
        {

            try
            {

                // Se reciben los datos básicos de búsqueda
                bool locked = true;
                string grilla = "SOLICITUDES_EN_TRAMITE";
                int id_region = -1;
                int id_provincia = -1;
                int id_comuna = -1;
                int id_tiposolicitud = -1;
                int id_tipoSectorialista = -1;
                int id_estado = -1;
                bool check = false;
                bool check2 = false;
                string numPert = "";

                Hashtable HT_ModEstados = (Hashtable)Session["Modulo_Estados"];
                Hashtable HT_ListEstados = new Hashtable();

                if (HT_ModEstados != null)
                {
                    HT_ListEstados = (Hashtable)HT_ModEstados["ListEstados"];
                    if (HT_ListEstados != null)
                    {
                        locked = (bool)HT_ListEstados["locked"];
                        // Si el formulario de búsqueda está desbloqueado significa que fue abiertoa travez de clickeos en los links de la aplicación  
                        // De estar bloqueado, los filtros de búsqueda seran reseteados
                        if (!locked)
                        {
                            grilla = (string)HT_ListEstados["grilla"];
                            id_region = (int)HT_ListEstados["id_region"];
                            id_provincia = (int)HT_ListEstados["id_provincia"];
                            id_comuna = (int)HT_ListEstados["id_comuna"];
                            id_tiposolicitud = (int)HT_ListEstados["id_tiposolicitud"];
                            id_tipoSectorialista = (int)HT_ListEstados["id_tipoSectorialista"];
                            id_estado = (int)HT_ListEstados["id_estado"];
                            numPert = (string)HT_ListEstados["numPert"];
                            check = (bool)HT_ListEstados["check"];
                            check2 = (bool)HT_ListEstados["check2"];
                        };
                    };
                };

                HT_ListEstados = new Hashtable();
                HT_ListEstados.Add("locked", true);
                HT_ListEstados.Add("grilla", grilla);
                HT_ListEstados.Add("id_region", id_region);
                HT_ListEstados.Add("id_provincia", id_provincia);
                HT_ListEstados.Add("id_comuna", id_comuna);
                HT_ListEstados.Add("id_tiposolicitud", id_tiposolicitud);
                HT_ListEstados.Add("id_tipoSectorialista", id_tipoSectorialista);
                HT_ListEstados.Add("id_estado", id_estado);
                HT_ListEstados.Add("numPert", numPert);
                HT_ListEstados.Add("check", check);
                HT_ListEstados.Add("check2", check2);


                // Actualizamos la sesión Modulo_Reportes
                if (HT_ModEstados == null)
                {
                    HT_ModEstados = new Hashtable();
                    HT_ModEstados.Add("ListEstados", (Hashtable)HT_ListEstados);
                }
                else
                {
                    if (HT_ModEstados["ListEstados"] == null)
                    {
                        HT_ModEstados.Add("ListEstados", (Hashtable)HT_ListEstados);
                    }
                    else
                    {
                        HT_ModEstados["ListEstados"] = (Hashtable)HT_ListEstados;
                    };
                };
                Session["Modulo_Estados"] = (Hashtable)HT_ModEstados;



            }catch (Exception) {

                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
            }
          
        }


        protected void Initialize_Comboboxs()
        {


            try
            {

                Hashtable HT_ModReportes = (Hashtable)Session["Modulo_Estados"];
                Hashtable HT_ListEstados = (Hashtable)HT_ModReportes["ListEstados"];
                int id_region = (int)HT_ListEstados["id_region"];
                int id_provincia = (int)HT_ListEstados["id_provincia"];
                int id_comuna = (int)HT_ListEstados["id_comuna"];
                int id_tiposolicitud = (int)HT_ListEstados["id_tiposolicitud"];
                int id_tipoSectorialista = (int)HT_ListEstados["id_tipoSectorialista"];
                int id_estado = (int)HT_ListEstados["id_estado"];
                string numPert = (string)HT_ListEstados["numPert"];
                bool check = (bool)HT_ListEstados["check"];
                bool check2 = (bool)HT_ListEstados["check2"];


                Carga_Combobox("TiposSolicitud");
                TiposSolicitud.SelectedValue = id_tiposolicitud.ToString();

                Carga_Combobox("Regiones");
                Regiones.SelectedValue = id_region.ToString();

                Carga_Combobox("Provincias");
                Provincias.SelectedValue = id_provincia.ToString();

                Carga_Combobox("Comunas");
                Comunas.SelectedValue = id_comuna.ToString();

                Carga_Combobox("Sectorialistas");
                Sectorialistas.SelectedValue = id_tipoSectorialista.ToString();

                Carga_Combobox("Estado");
                Estado.SelectedValue = id_estado.ToString();

                if (numPert != null && !numPert.Trim().Equals("")) {
                    Pert.Text = numPert;
                }

                if (check)
                {
                    checkIslas.Checked = true;
                }

                if (check2)
                {
                    checkAvanzaAprueba.Checked = true;
                }

            }
            catch (Exception) {

                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
            }
            
        }


        protected void Carga_Combobox(string combobox)
        {



            try
            {

                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                switch (combobox)
                {

                    case "TiposSolicitud":
                        // Cargamos el combobox: Tipos de Solicitud
                        TiposSolicitud.Items.Clear();
                        TiposSolicitud.DataSource = parametroGenericoDA.ListarTipoTramiteEstadoSolicitud(0);
                        TiposSolicitud.DataTextField = "nombreTipoInterfaz";
                        TiposSolicitud.DataValueField = "idTipoTramite";
                        TiposSolicitud.DataBind();
                        TiposSolicitud.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                        break;

                    case "Regiones":
                        // Cargamos el combobox: Regiones
                        Regiones.Items.Clear();
                        Regiones.DataSource = regionDA.ListarRegion(0);
                        Regiones.DataTextField = "Region";
                        Regiones.DataValueField = "IdRegion";
                        Regiones.DataBind();
                        Regiones.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                        break;

                    case "Provincias":
                        // Cargamos el combobox: Provincias
                        Provincias.Items.Clear();
                        if (Convert.ToInt32(Regiones.SelectedValue) > 0)
                        {
                            Provincias.DataSource = parametroGenericoDA.ListarProvinciaReg(0, Convert.ToInt32(Regiones.SelectedValue));
                            Provincias.DataTextField = "descripcion";
                            Provincias.DataValueField = "id";
                            Provincias.DataBind();
                        };
                        Provincias.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                        break;

                    case "Comunas":
                        // Cargamos el combobox: Comunas
                        Comunas.Items.Clear();
                        if (Convert.ToInt32(Provincias.SelectedValue) > 0)
                        {
                            Comunas.DataSource = parametroGenericoDA.ListarComunaDataTable(0, Convert.ToInt32(Provincias.SelectedValue));
                            Comunas.DataTextField = "Comuna";
                            Comunas.DataValueField = "IdComuna";
                            Comunas.DataBind();
                        };
                        Comunas.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                        break;

                    case "Sectorialistas":
                        // Cargamos el combobox: Tipos de Solicitud
                        Sectorialistas.Items.Clear();
                        Sectorialistas.DataSource = usuarioDA.ListarUsuarioRol(rbTipo.SECTORIALISTA);
                        Sectorialistas.DataTextField = "nombreCompleto";
                        Sectorialistas.DataValueField = "idUsuario";
                        Sectorialistas.DataBind();
                        Sectorialistas.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                        break;


                    case "Estado":


                        Estado.Items.Clear();
                        int idTipoTramite2 = Convert.ToInt32(TiposSolicitud.SelectedValue);

                        List<ParametroGenerico> resp = reporteService.ListarEstadosPorTipoTramite(idTipoTramite2);
                        if (resp != null)
                        {
                            foreach (ParametroGenerico item in resp)
                            {
                                Estado.Items.Add(new ListItem(item.descripcion, Convert.ToString(item.id)));
                            }
                        }

                        Estado.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                        Estado.DataBind();
                        UpdatePanel9.Update();

                        break;

                };

            }
            catch (Exception) {
                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
            }
        }
        

        protected void Initialize_Form()
        {

            try
            {

                // Cargamos los combobox
                Initialize_Comboboxs();

                Hashtable HT_ModReportes = (Hashtable)Session["Modulo_Estados"];
                Hashtable HT_ListEstados = (Hashtable)HT_ModReportes["ListEstados"];
                string grilla = (string)HT_ListEstados["grilla"];
                switch (grilla)
                {
                    case "SOLICITUDES_EN_TRAMITE":
                        lnk_Tramite.CssClass = "tab1_selected";
                        lnk_Rechazados.CssClass = "tab2";
                        GridTramite.Visible = true;
                        GridRechazadas.Visible = false;
                        break;
                    case "SOLICITUDES_RECHAZADAS":
                        lnk_Tramite.CssClass = "tab1";
                        lnk_Rechazados.CssClass = "tab2_selected";
                        GridTramite.Visible = false;
                        GridRechazadas.Visible = true;
                        break;
                };

            }catch (Exception) {
                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
            }

            
        }


        protected void TiposTramite_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                int idTipoTramite = Convert.ToInt32(TiposSolicitud.SelectedValue);
                Carga_Combobox("Estado");
            }
            catch (Exception)
            {
                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
            }
              
        }

        protected void Filtrar_Click(object sender, EventArgs e)
        {


            try { 

                Hashtable HT_ModReportes = (Hashtable)Session["Modulo_Estados"];
                Hashtable HT_ListEstados = (Hashtable)HT_ModReportes["ListEstados"];
                
                HT_ListEstados["id_region"] = Convert.ToInt32(Regiones.SelectedValue);
                HT_ListEstados["id_provincia"] = Convert.ToInt32(Provincias.SelectedValue);
                HT_ListEstados["id_comuna"] = Convert.ToInt32(Comunas.SelectedValue);
                HT_ListEstados["id_tiposolicitud"] = Convert.ToInt32(TiposSolicitud.SelectedValue);
                HT_ListEstados["id_tipoSectorialista"] = Convert.ToInt32(Sectorialistas.SelectedValue);
                HT_ListEstados["id_estado"] = Convert.ToInt32(Estado.SelectedValue);
                HT_ListEstados["numPert"] = Convert.ToString(Pert.Text);
                HT_ListEstados["check"] = Convert.ToBoolean(checkIslas.Checked);
                HT_ListEstados["check2"] = Convert.ToBoolean(checkAvanzaAprueba.Checked);
                HT_ModReportes["ListEstados"] = (Hashtable)HT_ListEstados;

                Session["Modulo_Reportes"] = (Hashtable)HT_ModReportes;

                if (Convert.ToInt32(TiposSolicitud.SelectedValue) < 1)
                {
                    Page.Validators.Add(new ValidationError("FormErrores", "Tipo de solicitud"));
                }


                UpdatePanelMensajesValidaciones.Update();


                if (Page.IsValid)
                {
                    CargaGrilla();
                    CargaGrillaReposicion();
                }
          
            }
            catch (Exception)
            {
                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
            }
        }
        

        protected void Limpiar_Click(object sender, EventArgs e)
        {

            try { 

                Regiones.SelectedValue = "-1";
                Provincias.SelectedValue = "-1";
                Comunas.SelectedValue = "-1";
                TiposSolicitud.SelectedValue = "-1";
                Sectorialistas.SelectedValue = "-1";
                Estado.SelectedValue = "-1";
                Pert.Text = "";
                checkIslas.Checked = false;
                checkAvanzaAprueba.Checked = false;
                TiposTramite_OnSelectedIndexChanged(null, null);


                Hashtable HT_ModReportes = (Hashtable)Session["Modulo_Reportes"];
                Hashtable HT_ListEstados = (Hashtable)HT_ModReportes["ListEstados"];
                HT_ListEstados["id_region"] = Convert.ToInt32(Regiones.SelectedValue);
                HT_ListEstados["id_provincia"] = Convert.ToInt32(Provincias.SelectedValue);
                HT_ListEstados["id_comuna"] = Convert.ToInt32(Comunas.SelectedValue);
                HT_ListEstados["id_tiposolicitud"] = Convert.ToInt32(TiposSolicitud.SelectedValue);
                HT_ListEstados["id_tipoSectorialista"] = Convert.ToInt32(Sectorialistas.SelectedValue);
                HT_ListEstados["id_estado"] = Convert.ToInt32(Estado.SelectedValue);
                HT_ListEstados["numPert"] = Convert.ToString(Pert.Text);
                HT_ListEstados["check"] = Convert.ToBoolean(checkIslas.Checked);
                HT_ListEstados["check2"] = Convert.ToBoolean(checkAvanzaAprueba.Checked);

                HT_ModReportes["ListEstados"] = (Hashtable)HT_ListEstados;
                Session["Modulo_Estados"] = (Hashtable)HT_ModReportes;

                CargaGrilla();
                CargaGrillaReposicion();


            }
            catch (Exception)
            {
                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
            }

        }


        protected void Regiones_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("Provincias");
            Carga_Combobox("Comunas");
        }


        protected void Provincias_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("Comunas");
        }
      


        protected void cambiaGrilla_Click(object sender, EventArgs e)
        {

            LinkButton boton = (LinkButton)sender;
            Hashtable HT_ModEstados = (Hashtable)Session["Modulo_Estados"];
            Hashtable HT_ListEstados = (Hashtable)HT_ModEstados["ListEstados"];

            switch (boton.ID)
            {
                case "lnk_Tramite":
                    HT_ListEstados["grilla"] = "SOLICITUDES_EN_TRAMITE";
                    lnk_Tramite.CssClass = "tab1_selected";
                    lnk_Rechazados.CssClass = "tab2";
                    break;
                case "lnk_Rechazados":
                    HT_ListEstados["grilla"] = "SOLICITUDES_RECHAZADAS";
                    lnk_Tramite.CssClass = "tab1";
                    lnk_Rechazados.CssClass = "tab2_selected";
                    break;
            };



            if (HT_ListEstados["grilla"].Equals("SOLICITUDES_EN_TRAMITE"))
            {
                GridTramite.Visible = true;
                GridRechazadas.Visible = false;
            }
            else if (HT_ListEstados["grilla"].Equals("SOLICITUDES_RECHAZADAS"))
            {
                GridTramite.Visible = false;
                GridRechazadas.Visible = true;
            }
            else
            {
                GridTramite.Visible = true;
                GridRechazadas.Visible = false;
            }
            
            HT_ModEstados["ListEstados"] = (Hashtable)HT_ListEstados;
            Session["Modulo_Estados"] = (Hashtable)HT_ModEstados;

        }




        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {

            try
            {

                // Descargamos los filtros de búsqueda del Hashtable
                Hashtable HT_ModReportes = new Hashtable();
                Hashtable HT_ListEstados = new Hashtable();
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];
                try
                {
                    HT_ModReportes = (Hashtable)Session["Modulo_Estados"];
                    HT_ListEstados = (Hashtable)HT_ModReportes["ListEstados"];
                }
                catch
                {
                    Initialize_HT_ListEstados();
                    HT_ModReportes = (Hashtable)Session["Modulo_Estados"];
                    HT_ListEstados = (Hashtable)HT_ModReportes["ListEstados"];
                };
                string grilla = (string)HT_ListEstados["grilla"];
                int id_region = (int)HT_ListEstados["id_region"];
                int id_provincia = (int)HT_ListEstados["id_provincia"];
                int id_comuna = (int)HT_ListEstados["id_comuna"];
                int id_tiposolicitud = (int)HT_ListEstados["id_tiposolicitud"];
                int id_tipoSectorialista = (int)HT_ListEstados["id_tipoSectorialista"];

                int id_estado = (int)HT_ListEstados["id_estado"];
                string numPert = (string)HT_ListEstados["numPert"];
                bool check = (bool)HT_ListEstados["check"];
                bool check2 = (bool)HT_ListEstados["check2"];


                // Iniciamos la query de búsqueda y cargamos la grilla
                DataTable dt = new DataTable();
                int num_registros = 0;
                dt = estadoService.Solicitudes_Estados_Resumen_Listar_RB(id_region, id_provincia, id_comuna, id_tiposolicitud, usuario_logeado.id_usuario, id_tipoSectorialista, id_estado, numPert, check, check2);


                if (id_tiposolicitud > 0)
                {

                    if (dt != null)
                    {
                        num_registros = dt.Rows.Count;
                        if (num_registros > 0)
                        {
                            Content_msgGrilla.Visible = false;
                            ExportarGrilla.Visible = true;
                        }
                        else
                        {
                            switch (grilla)
                            {
                                case "SOLICITUDES_EN_TRAMITE":
                                    msgGrilla.Text = "No se han encontrado resultados.";
                                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                                    Content_msgGrilla.Visible = true;
                                    ExportarGrilla.Visible = false;
                                    break;
                            };
                        };

                        dt.DefaultView.Sort = KeySort1.Value;
                        GridTramite.DataSource = dt;
                        GridTramite.DataBind();

                    }
                    else
                    {
                        Response.Redirect("~/Administrador/principal.aspx");
                    };


                }


                // Iniciamos la query de búsqueda y cargamos la grilla
                DataTable dt2 = new DataTable();
                int num_registros2 = 0;

                dt2 = estadoService.Solicitudes_Estados_Rechazadas_Resumen_Listar_RB(id_region, id_provincia, id_comuna, id_tiposolicitud, usuario_logeado.id_usuario, id_tipoSectorialista, id_estado, numPert, check, check2);


                if (dt2 != null)
                {
                    num_registros2 = dt2.Rows.Count;
                    if (num_registros2 > 0)
                    {
                        Content_msgGrilla.Visible = false;
                        ExportarGrilla.Visible = true;
                    }
                    else
                    {
                        switch (grilla)
                        {
                            case "SOLICITUDES_RECHAZADAS":
                                msgGrilla.Text = "No se han encontrado resultados.";
                                Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                                Content_msgGrilla.Visible = true;
                                ExportarGrilla.Visible = false;
                                break;
                        };
                    };

                    dt2.DefaultView.Sort = KeySort2.Value;
                    GridRechazadas.DataSource = dt2;
                    GridRechazadas.DataBind();

                }
                else
                {
                    Response.Redirect("~/Administrador/principal.aspx");
                };



            }
            catch (Exception)
            {
                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
            }
        }


        protected void CargaGrillaReposicion()
        {
            try
            {

                // Descargamos los filtros de búsqueda del Hashtable
                Hashtable HT_ModReportes = new Hashtable();
                Hashtable HT_ListEstados = new Hashtable();
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];
                try
                {
                    HT_ModReportes = (Hashtable)Session["Modulo_Estados"];
                    HT_ListEstados = (Hashtable)HT_ModReportes["ListEstados"];
                }
                catch
                {
                    Initialize_HT_ListEstados();
                    HT_ModReportes = (Hashtable)Session["Modulo_Estados"];
                    HT_ListEstados = (Hashtable)HT_ModReportes["ListEstados"];
                };



                int id_region = (int)HT_ListEstados["id_region"];
                int id_provincia = (int)HT_ListEstados["id_provincia"];
                int id_comuna = (int)HT_ListEstados["id_comuna"];
                int id_tiposolicitud = (int)HT_ListEstados["id_tiposolicitud"];
                int id_tipoSectorialista = (int)HT_ListEstados["id_tipoSectorialista"];
                int id_estado = (int)HT_ListEstados["id_estado"];
                string numPert = (string)HT_ListEstados["numPert"];
                bool check = (bool)HT_ListEstados["check"];
                bool check2 = (bool)HT_ListEstados["check2"];


                // Iniciamos la query de búsqueda y cargamos la grilla
                DataTable dt = null;
                int num_registros = 0;


                
                //BUSCAR LAS SOLICITUDES EN RECURSO DE REPOSICION
                if (id_tiposolicitud > 0)
                {
                    dt = estadoService.Solicitudes_Estados_Reposicion_Resumen_Listar_RB(id_region, id_provincia, id_comuna, id_tiposolicitud, usuario_logeado.id_usuario, id_tipoSectorialista, id_estado, numPert, check2);

                    if (dt != null)
                    {
                        num_registros = dt.Rows.Count;
                        if (num_registros > 0)
                        {
                            Content_msgGrillaReposicion.Visible = false;
                            ButtonExportarReposicion.Visible = true;
                        }
                        else
                        {

                            msgGrillaReposicion.Text = "No se han encontrado resultados.";
                            Ico_msgGrillaReposicion.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                            Content_msgGrillaReposicion.Visible = true;
                            ButtonExportarReposicion.Visible = false;

                        };

                        dt.DefaultView.Sort = KeySort3.Value;
                        GridReposicion.DataSource = dt;
                        GridReposicion.DataBind();

                    }
                    else
                    {
                        Response.Redirect("~/Administrador/principal.aspx");
                    };

                }



           


            }
            catch (Exception)
            {
                Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
            }
        }


        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {

            // Descargamos los filtros de búsqueda del Hashtable
            Hashtable HT_ModEstados = (Hashtable)Session["Modulo_Estados"];
            Hashtable HT_ListEstados = (Hashtable)HT_ModEstados["ListEstados"];
            
            GridView grilla = new GridView();
            
            string nom_grilla = (string)HT_ListEstados["grilla"];
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "SOLICITUDES_EN_TRAMITE":
                    grilla = GridTramite;
                    ngrilla = "estadosSolicitudesEnTramite.xls";
                    break;
                case "SOLICITUDES_RECHAZADAS":
                    grilla = GridRechazadas;
                    ngrilla = "estadosSolicitudesRechazadas.xls";
                    break;


            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);

        }


        #region Grilla En Tramite

        protected void GridTramite_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName != null) {

                string comando = e.CommandName;

                if (comando.Trim().Equals("Total"))
                { 
                    Content_msgGrilla.Visible = false;
                    int id_estado = Convert.ToInt32(e.CommandArgument);
                    string path = @"~/Administrador/Reportes/detalleEstado.aspx?&id_estado=" + id_estado.ToString();
                    Response.Redirect(path + "&tipo=1"); //TIPO 1 = EN TRAMITE
                }
            }
        }

        protected void GridTramite_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            int id_estado = 0;
            int id_color = 0;


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                id_estado = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "IdEstado"));
                id_color = 1; //por defecto es verde
                if (!(DataBinder.Eval(e.Row.DataItem, "color") is DBNull))
                {
                    id_color = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "color"));
                }
                e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#555555");

                switch (id_color)
                {
                    case 1:
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#CED8BF"); //verde
                        break;
                    case 2:
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FBF5B5"); //amarillo
                        break;
                    case 3:
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFCEB7");  //rojo
                        break;
                };

            };
        }


        protected void GridTramite_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sort = KeySort1.Value;
            int pos = 0;

            pos = sort.IndexOf(e.SortExpression + " ASC");
            if (pos >= 0)
            {
                sort = e.SortExpression + " DESC";
            }
            else
            {
                sort = e.SortExpression + " ASC";
            };
            KeySort1.Value = sort;
            GridTramite.PageIndex = 0;
            GridTramite.DataBind();

            CargaGrilla();
        }


        #endregion


        #region Grilla Rechazadas


        protected void GridRechazadas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName != null)
            {

                string comando = e.CommandName;

                if (comando.Trim().Equals("Total"))
                {

                    Content_msgGrilla.Visible = false;
                    int id_estado = Convert.ToInt32(e.CommandArgument);
                    string path = @"~/Administrador/Reportes/detalleEstado.aspx?op=2&id_estado=" + id_estado.ToString();
                    Response.Redirect(path + "&tipo=2"); //TIPO 2 = RECHAZADAS
                }
            }

        }

        protected void GridRechazadas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int id_estado = 0;
            int id_color = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                id_estado = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "IdEstado"));
                id_color = 1; //por defecto es verde
                if (!(DataBinder.Eval(e.Row.DataItem, "color") is DBNull))
                {
                    id_color = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "color"));
                }
                e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#555555");

                switch (id_color)
                {
                    case 1:
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#CED8BF"); //verde
                        break;
                    case 2:
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FBF5B5"); //amarillo
                        break;
                    case 3:
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFCEB7");  //rojo
                        break;
                };

            };
        }


        protected void GridRechazadas_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sort = KeySort2.Value;
            int pos = 0;

            pos = sort.IndexOf(e.SortExpression + " ASC");
            if (pos >= 0)
            {
                sort = e.SortExpression + " DESC";
            }
            else
            {
                sort = e.SortExpression + " ASC";
            };
            KeySort2.Value = sort;
            GridRechazadas.PageIndex = 0;
            GridRechazadas.DataBind();

            CargaGrilla();
        }

        #endregion


        #region Grilla Recurso Reposición

        protected void GridReposicion_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName != null)
            {
                string comando = e.CommandName;

                if (comando.Trim().Equals("Total"))
                {
                    Content_msgGrilla.Visible = false;
                    int id_estado = Convert.ToInt32(e.CommandArgument);
                    string path = @"~/Administrador/Reportes/detalleEstado.aspx?id_estado=" + id_estado.ToString();
                    Response.Redirect(path + "&tipo=3"); //TIPO 3 = RECURSO DE REPOSICION
                }
            }
        }

        protected void GridReposicion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int id_estado = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                id_estado = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "IdEstado"));
                e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#555555");

            };
        }


        protected void ExportarGrillaReposicion_Click(object sender, EventArgs e)
        {
            
            CargaGrillaReposicion();

            GridView grilla = new GridView(); 
            grilla = GridReposicion;
            string ngrilla = "estadosSolicitudesReposicion.xls";

            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);

        }

        protected void GridReposicion_Sorting(object sender, GridViewSortEventArgs e)
        {


            string sort = KeySort3.Value;
            int pos = 0;

            pos = sort.IndexOf(e.SortExpression + " ASC");
            if (pos >= 0)
            {
                sort = e.SortExpression + " DESC";
            }
            else
            {
                sort = e.SortExpression + " ASC";
            };
            KeySort3.Value = sort;
            GridReposicion.PageIndex = 0;
            GridReposicion.DataBind();

            CargaGrillaReposicion();
        }


        #endregion

    }
}
