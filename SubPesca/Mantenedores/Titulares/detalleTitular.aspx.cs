using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using Datos.Entidades;
using System.Data;
using System.Collections;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.mantenedores;

namespace SubPesca.Mantenedores.Titulares
{
    public partial class detalleTitular : System.Web.UI.Page
    {

        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();
        MatrizSucursalDA matrizSucursalDA = new MatrizSucursalDA();
        RepLegalDA repLegalDA = new RepLegalDA();
        OperadorDA operadorDA = new OperadorDA();
        SolicitanteDA solicitanteDA = new SolicitanteDA();

        MantenedorTitularService mantenedorTitularService = new MantenedorTitularService();



        void Page_PreInit(object sender, EventArgs e)
        {
            setearPageMaster();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
             
                // Obtención de parámetros mediante GET/POST
                ObtencionParametros();
                
                //Cargamos las grillas
                CargarGrilla("ContactoMatrizSucursales");
                CargarGrilla("Contactos");
                CargarGrilla("RepresentantesLegales");
                CargarGrilla("Operadores");
                CargarGrilla("ArchivosAdjuntos");
            }
        }

        protected void setearPageMaster(){
        
            int backpage = 0;
                    try
                    {
                        if (Request.QueryString["bp"] != null)
                        {
                            backpage = Convert.ToInt32(Request.QueryString["bp"]);
                        };
                    }
                    catch
                    {               
                    };

                    switch (backpage)
                    { 
                        /* Solicitud de Modificación de Concesión */
                        case 1:
                            Page.MasterPageFile ="~/Solicitudes/SitioSolicitudesModificacion.Master";
                            break;

                        /* Solicitud de Modificación de Acuicultura en Amerb */
                        case 50:
                            Page.MasterPageFile = "~/Solicitudes/SitioSolicitudesModificacionAmerb.Master";
                            break;

                        /* Solicitud de Modificación de Acuicultura de Centro de Acopio */
                        case 51:
                            Page.MasterPageFile = "~/Solicitudes/SitioSolicitudesModificacionCentroAcopio.Master";
                            break;

                        /* Solicitud de Modificación de Centro de Faenamiento */
                        case 52:
                            Page.MasterPageFile = "~/Solicitudes/SitioSolicitudesModificacionCentroFaenamiento.Master";
                            break;

                        /* Solicitud de Modificación de Acuicultura de Acuicultura en ECMPO */
                        case 53:
                            Page.MasterPageFile = "~/Solicitudes/SitioSolicitudesModificacionECMPO.Master";
                            break;

                        case 2:
                            Page.MasterPageFile = "~/Solicitudes/SitioCatastroUnidades.Master";
                            break;

                        /* Solicitud de Relocalización por Ley */
                        case 3:
                            Page.MasterPageFile = "~/Solicitudes/SitioSolicitudesRelocalizacion.Master";
                            break;

                        /* Solicitud de Relocalización RESA */
                        case 54:
                            Page.MasterPageFile = "~/Solicitudes/SitioSolicitudesRelocalizacionRESA.Master";
                            break;

                        case 4:
                            Page.MasterPageFile = "~/Mantenedores/SitioMantenedorTitulares.Master";
                            break;
                        case 5:
                            Page.MasterPageFile = "~/Solicitudes/SitioSolicitudesAcopio.Master";
                            break;
                        case 6:
                            Page.MasterPageFile = "~/Solicitudes/SitioSolicitudesAmerb.Master";
                            break;
                        case 7:
                            Page.MasterPageFile = "~/Solicitudes/SitioSolicitudesColector.Master";
                            break;
                        case 8:
                            Page.MasterPageFile = "~/Solicitudes/SitioSolicitudesFaenamiento.Master";
                            break;
                        case 9:
                            Page.MasterPageFile = "~/Solicitudes/SitioSolicitudesExperimentalesAmerb.Master";
                            break;
                        case 10:
                            Page.MasterPageFile = "~/Solicitudes/SitioSolicitudesExperimentalesConcesion.Master";
                            break;
                        case 11:
                            Page.MasterPageFile = "~/Solicitudes/SitioSolicitudesECMPO.Master";
                            break;
                        
                        case 29:
                            Page.MasterPageFile = "~/Unidades/SitioCentroECMPO.Master";
                            break;
                        case 30:
                            Page.MasterPageFile = "~/Unidades/SitioCentroAcopio.Master";
                            break;
                        case 31:
                            Page.MasterPageFile = "~/Unidades/SitioCentroAmerb.Master";
                            break;
                        case 32:
                            Page.MasterPageFile = "~/Unidades/SitioColectorSemillas.Master";
                            break;
                        case 33:
                            Page.MasterPageFile = "~/Unidades/SitioConcesionAcuicultura.Master";
                            break;
                        case 34:
                            Page.MasterPageFile = "~/Unidades/SitioCentroFaenamiento.Master";
                            break;


                        /* Páginas maestras individuales para las modificaciones de solicitudes */
                    };
        }

        /**
         * Método que recupera el dato enviado a través de get/post a esta 
         * pagina con la información del detalle del titular.
         */
        protected void ObtencionParametros()
        {

            SolicitanteService solicitanteService = new SolicitanteService();
            Solicitante solicitante = new Solicitante();
            
            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];
            
            // Se recibe el rutPersona
            try
            {
                if (Request.QueryString["rutPersona"] != null)
                {
                    solicitante.rut = Convert.ToInt32(Request.QueryString["rutPersona"]);
                    solicitante = solicitanteService.VerPersona(solicitante.rut, 0);

                    TipoPersona.Text = Convert.ToString(solicitante.tipoPersona.descripcion);
                    RutPersona.Text = Convert.ToString(solicitante.rut);
                    DVPersona.Text = Convert.ToString(solicitante.dv);
                    NombreSolicitante.Text = Convert.ToString(solicitante.nombreSolicitante);

                    //solicitante.esRPA = solicitanteService.TiularEsAPE(solicitante.rut);

                    solicitante.esRPA = solicitanteService.TitularesRPA(solicitante.rut);

                    /* RPA */
                    if (solicitante.esRPA)
                    {
                        RPA.Text = "Sí";
                    }
                    else
                    {
                        RPA.Text = "No";
                    }


                    PanelRPA.Visible = true;

                    if (solicitante.holding != null && solicitante.holding.id > 0)
                    {
                        Holding.Text = Convert.ToString(solicitante.holding.descripcion);
                    }

                    if (solicitante.estadoAPE != null && solicitante.estadoAPE.id > 0)
                    {
                        APE.Text = Convert.ToString(solicitante.estadoAPE.descripcion);
                    }

                    if (solicitante.tipoPersona != null && solicitante.tipoPersona.id == rbTipo.PERSONA_JURIDICA)
                    {
                       
                        /* Se busca la persona jurídica asociada al rut ingresado por el usuario */
                        Solicitante solicitantePersonaJuridica = mantenedorTitularService.buscarPersonaJuridica(solicitante.rut, solicitante.dv);

                        if (solicitantePersonaJuridica != null)
                        {
                            NumeroRegistroSubpesca.Text = Convert.ToString(solicitantePersonaJuridica.numeroRegistroSubpesca);
                            FechaRegistroSubpesca.Text = FechaUtils.formatearFecha(solicitantePersonaJuridica.fechaRegistroSubpesca);

                            PanelEspecialPersonaJuridica.Visible = true;
                        }

                        
                    }

                    //if (solicitante.numeroControlIngreso > 0)
                    //{
                    //    NumeroCI.Text = Convert.ToString(solicitante.numeroControlIngreso);
                    //    FechaCI.Text = Convert.ToString(solicitante.fechaControlIngreso);

                    //    PanelDatosModificacion.Visible = true;
                    //}
                }
                else { 
                
                }
                
            }
            catch
            {
                
            };

        }

        /**
         * Método que da la funcionalidad al boton volver presente
         * en la página.
         
        protected void Volver_Click(object sender, EventArgs e)
        {
            string path = "~/Solicitudes/Registrar/identificacionSolicitante.aspx";
            Hashtable HT_ModSolicitantes = (Hashtable)Session["Modulo_Solicitantes"];
            Session["Modulo_Solicitantes"] = (Hashtable)HT_ModSolicitantes;
            Response.Redirect(path);
        }*/

        /**
         * Método que carga las grillas del detalle del titular.
         * Contacto Matriz y Sucursales, Representantes Legales y Archivos Adjuntos.
         */
        protected void CargarGrilla(string grilla)
        {
            SolicitanteService solicitanteService = new SolicitanteService();
            Solicitante solicitante = new Solicitante();
            DataTable dt = new DataTable();

            switch (grilla)
            {
                case "ContactoMatrizSucursales":

                    solicitante.rut = Convert.ToInt32(RutPersona.Text);
                    //dt = solicitanteService.VerContactoMatrizSucursales(solicitante.rut,0);
                    //GridContactoMatrizSucursales.DataSource = dt;

                    GridContactoMatrizSucursales.DataSource = matrizSucursalDA.ListarTitularMatrizSuc(solicitante.rut);
                    GridContactoMatrizSucursales.DataBind();
                    break;

                case "Contactos":

                    solicitante.rut = Convert.ToInt32(RutPersona.Text);
                    GridContacto.DataSource = solicitanteDA.ListarContactoPersona(solicitante.rut, 0);
                    GridContacto.DataBind();
                    break;

                case "RepresentantesLegales":

                    solicitante.rut = Convert.ToInt32(RutPersona.Text);
                    //dt = solicitanteService.VerRepresentantesLegales(solicitante.rut,0);
                    // GridRepresentantesLegales.DataSource = dt;

                    GridRepresentantesLegales.DataSource = repLegalDA.ListarRepresentanteTitular(solicitante.rut);
                    GridRepresentantesLegales.DataBind();
                    break;

                case "Operadores":

                    solicitante.rut = Convert.ToInt32(RutPersona.Text);
                    //GridViewOperadores.DataSource = solicitanteService.VerOperadores(solicitante.rut, 0);
                    GridViewOperadores.DataSource = operadorDA.ListarOperadorTitular(solicitante.rut,0);
                    GridViewOperadores.DataBind();
                    break;

                case "ArchivosAdjuntos":
                    solicitante.rut = Convert.ToInt32(RutPersona.Text);
                    dt = solicitanteService.VerArchivosAdjuntos(solicitante.rut,0);

                    GridArchivosAdjuntos.DataSource = dt;
                    GridArchivosAdjuntos.DataBind();
                    break;
            };
        }

        protected void GridArchivosAdjuntos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Descargar
                String idArchivo = DataBinder.Eval(e.Row.DataItem, "idArchivoBinSC").ToString();
                ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                if (boton_descargar != null && idArchivo != null && !idArchivo.Equals("") && Convert.ToInt32(idArchivo) > 0)
                {
                    boton_descargar.Visible = true;
                };

            }
        }

        protected void GridRepresentantesLegales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ////Descargar
                //String idArchivo = DataBinder.Eval(e.Row.DataItem, "idArchivoBinario").ToString();
                //ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                //if (boton_descargar != null && idArchivo != null && !idArchivo.Equals("") && Convert.ToInt32(idArchivo) > 0)
                //{
                //    boton_descargar.Visible = true;
                //};

                /* Se implementa el ver del representante legal */
                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    boton_ver.Visible = true;
                }

            }
        }

        protected void GridViewOperadores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ////Descargar
                //String idArchivo = DataBinder.Eval(e.Row.DataItem, "idArchivoBinario").ToString();
                //ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                //if (boton_descargar != null && idArchivo != null && !idArchivo.Equals("") && Convert.ToInt32(idArchivo) > 0)
                //{
                //    boton_descargar.Visible = true;
                //};

                /* Se implementa el ver del operador */
                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    boton_ver.Visible = true;
                }

            }
        }

        protected void GridArchivosAdjuntos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Descargar":
                    int idArchivo = Convert.ToInt32(e.CommandArgument);
                    ArchivoBinario archivoBinario = archivoBinarioSolicitudDA.ObtenerArchivoBinarioSolicitud(idArchivo);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = "application/" + archivoBinario.formato;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinario.nombreArchivo + "." + archivoBinario.formato);
                    Response.BinaryWrite(archivoBinario.bytes);
                    Response.Flush();
                    Response.End();

                    break;
            };
        }

        protected void GridRepresentantesLegales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Ver":
                    int rutRepresentante = Convert.ToInt32(e.CommandArgument);
                    string path = "~/Mantenedores/Titulares/verRepresentanteLegal.aspx?rutPersona=" + rutRepresentante;
                    Response.Redirect(path);
                    break;
                
                //case "Descargar":
                    
                //    int idArchivoBinario = Convert.ToInt32(e.CommandArgument);

                //    ArchivoBinario archivoBinarioEspecial = archivoBinarioSolicitudDA.ObtenerArchivoBinarioSolicitud(idArchivoBinario);

                //    if (archivoBinarioEspecial != null)
                //    {
                //        Response.Clear();
                //        Response.Buffer = true;
                //        Response.Charset = "";
                //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //        Response.ContentType = "application/" + archivoBinarioEspecial.formato;
                //        Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinarioEspecial.nombreArchivo + "." + archivoBinarioEspecial.formato);
                //        Response.BinaryWrite(archivoBinarioEspecial.bytes);
                //        Response.Flush();
                //        Response.End();
                //    }

                //    break;
            };
        }

        protected void GridViewOperadores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Ver":
                    int rutOperador = Convert.ToInt32(e.CommandArgument);
                    string path = "~/Mantenedores/Titulares/verOperador.aspx?rutPersona=" + rutOperador;
                    Response.Redirect(path);
                    break;

                //case "Descargar":

                //    int idArchivoBinario = Convert.ToInt32(e.CommandArgument);

                //    ArchivoBinario archivoBinarioEspecial = archivoBinarioSolicitudDA.ObtenerArchivoBinarioSolicitud(idArchivoBinario);

                //    if (archivoBinarioEspecial != null)
                //    {
                //        Response.Clear();
                //        Response.Buffer = true;
                //        Response.Charset = "";
                //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //        Response.ContentType = "application/" + archivoBinarioEspecial.formato;
                //        Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinarioEspecial.nombreArchivo + "." + archivoBinarioEspecial.formato);
                //        Response.BinaryWrite(archivoBinarioEspecial.bytes);
                //        Response.Flush();
                //        Response.End();
                //    }

                //    break;
            };
        }

        /**
         *  Método que crea el header de la grilla de Contactos Matriz y Sucursales
         */
        protected void GridContactoMatrizSucursales_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridContacto = (GridView)sender;

                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Direcciones";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridContacto.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        /**
         * Método que agrega al evento para cambiar de página los resultados de la grilla de 
         * Contactos Matriz y Sucursales
         */
        protected void GridContactoMatrizSucursales_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            
        }

        /**
         * Método que crea el header de la grilla de Representantes Legales
         */
        protected void GridRepresentantesLegales_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridRepresentante = (GridView)sender;

                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Representantes Legales";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridRepresentante.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        protected void GridViewOperadores_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridOperador = (GridView)sender;

                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Operadores";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridOperador.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        /**
        * Método que agrega al evento para cambiar de página los resultados de la grilla de 
        * Representantes Legales
        */
        protected void GridRepresentantesLegales_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

        }

        /**
         * Método que crea el header de la grilla de Archivos Adjuntos
         */
        protected void GridArchivosAdjuntos_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridArchivosAdjuntos = (GridView)sender;

                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Lista de Archivos Adjuntos";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridArchivosAdjuntos.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        /**
       *  Método que crea el header de la grilla de Contactos Matriz y Sucursales
       */
        protected void GridContacto_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridContacto = (GridView)sender;

                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Listado de Contactos";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridContacto.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        /**
        * Método que agrega al evento para cambiar de página los resultados de la grilla de 
        * Archivos Adjuntos
        */
        protected void GridArchivosAdjuntos_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GridContacto_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GridContactoMatrizSucursales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                /* Ver */
                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    boton_ver.Visible = true;

                }

                //String idArchivoBinario = DataBinder.Eval(e.Row.DataItem, "idArchivoBinario").ToString();
                //ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                //if (boton_descargar != null && idArchivoBinario != null && !idArchivoBinario.Equals("") && Convert.ToInt32(idArchivoBinario) > 0)
                //{
                //    boton_descargar.Visible = true;

                //}

            }
        }

        protected void GridContactoMatrizSucursales_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');
            
            Solicitante solicitante = (Solicitante)Session["Titular"];

            switch (e.CommandName)
            {
                case "Ver":

                    int idMatrizSuc = Convert.ToInt32(arg[0]);
                    int index = Convert.ToInt32(arg[1]);

                    Response.Redirect("~/Mantenedores/Titulares/verContactoTitular.aspx?idMatrizSuc=" + idMatrizSuc + "&index=" + index + "&acc=5");

                    break;

                //case "Descargar":

                //    int idArchivoBinario = Convert.ToInt32(arg[0]);

                //    ArchivoBinario archivoBinarioEspecial = archivoBinarioSolicitudDA.ObtenerArchivoBinarioSolicitud(idArchivoBinario);

                //    if (archivoBinarioEspecial != null)
                //    {
                //        Response.Clear();
                //        Response.Buffer = true;
                //        Response.Charset = "";
                //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //        Response.ContentType = "application/" + archivoBinarioEspecial.formato;
                //        Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinarioEspecial.nombreArchivo + "." + archivoBinarioEspecial.formato);
                //        Response.BinaryWrite(archivoBinarioEspecial.bytes);
                //        Response.Flush();
                //        Response.End();
                //    }

                //    break;


            }
        }


        protected void ImgAdd_PreRender(object sender, EventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            ScriptManager sc = ScriptManager.GetCurrent(this.Page);
            sc.RegisterPostBackControl(btn);
        }

        protected void Volver_Click(object sender, EventArgs e)
        {
            int backpage = 0;
            String path = "";
            try
            {
                if (Request.QueryString["bp"] != null)
                {
                    backpage = Convert.ToInt32(Request.QueryString["bp"]);
                };
            }
            catch
            {
            };

            switch (backpage)
            {
                case 1:
                    path = "~/Solicitudes/Modificacion/identificacionTitularConcesion.aspx";
                    break;
                case 2:
                    path = "~/Solicitudes/Registrar/identificacionSolicitante.aspx";
                    break;
                case 3:
                    path = "~/Solicitudes/Relocalizacion/identificacionSolicitanteRelocalizacion.aspx";
                    break;
                case 4:
                    path = "~/Mantenedores/Titulares/administrarTitulares.aspx";
                    break;
                case 5:
                    path = "~/Solicitudes/Acopio/identificacionSolicitanteAcopio.aspx";
                    break;
                case 6:
                    path = "~/Solicitudes/Amerb/identificacionSolicitanteAmerb.aspx";
                    break;
                case 7:
                    path = "~/Solicitudes/Colector/identificacionSolicitanteColector.aspx";
                    break;
                case 8:
                    path = "~/Solicitudes/Faenamiento/identificacionSolicitanteFaenamiento.aspx";
                    break;
                case 9:
                    path = "~/Solicitudes/ExperimentalesAmerb/identificacionTitularExperimentalesAmerb.aspx";
                    break;
                case 10:
                    path = "~/Solicitudes/ExperimentalesConcesion/identificacionTitularExperimentalesConcesion.aspx";
                    break;
                case 11:
                    path = "~/Solicitudes/ECMPO/identificacionTitularECMPO.aspx";
                    break;

                case 29:
                    path = "~/Unidades/ECMPO/titularECMPO.aspx";
                    break;
                case 30:
                    path = "~/Unidades/Acopio/titularAcopio.aspx";
                    break;
                case 31:
                    path = "~/Unidades/Amerb/titularAmerb.aspx";
                    break;
                case 32:
                    path = "~/Unidades/Colector/titularColector.aspx";
                    break;
                case 33:
                    path = "~/Unidades/Concesion/titularConcesion.aspx";
                    break;
                case 34:
                    path = "~/Unidades/Faenamiento/titularFaenamiento.aspx";
                    break;

                /* Solicitud de Modificación Amerb */
                case 50:
                    path = "~/Solicitudes/ModificacionAmerb/identificacionTitularModificacionAmerb.aspx";
                    break;

                /* Solicitud de Modificación Centro de Acopio */
                case 51:
                    path = "~/Solicitudes/ModificacionCentroAcopio/identificacionTitularModificacionCentroAcopio.aspx";
                    break;

                /* Solicitud de Modificación Centro de Faenamiento */
                case 52:
                    path = "~/Solicitudes/ModificacionCentroFaenamiento/identificacionTitularModificacionCentroFaenamiento.aspx";
                    break;

                /* Solicitud de Modificación Acuicultura en ECMPO */
                case 53:
                    path = "~/Solicitudes/ModificacionECMPO/identificacionTitularModificacionECMPO.aspx";
                    break;

                /* Solicitud de Relocalización RESA */
                case 54:
                    path = "~/Solicitudes/RelocalizacionRESA/identificacionSolicitanteRelocalizacionRESA.aspx";
                    break;
            };

            Response.Redirect(path);
        }
    }
}