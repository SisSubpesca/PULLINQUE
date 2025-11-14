using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using SubPesca.Utilidades;
using Validaciones.cl.subpesca.rb.mantenedor;
using Datos.Utilidades;

namespace SubPesca.Mantenedores.Titulares
{
    public partial class agregarNombres : System.Web.UI.Page
    {

        MantenedorTitularesValidacion mantenedorTitularesValidacion = new MantenedorTitularesValidacion();
        Funciones funciones = new Funciones();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ObtencionParametros();
            }
        }

        private void ObtencionParametros()
        {
            int backpage = 0;
            int acc = 0;
            string rutPersona = "";
            try
            {
                if (Request.QueryString["bp"] != null)
                {
                    backpage = Convert.ToInt32(Request.QueryString["bp"]);
                };

                if (Request.QueryString["rutPersona"] != null)
                {
                    rutPersona = Request.QueryString["rutPersona"];
                }

                if (Request.QueryString["acc"] != null)
                {
                    acc = Convert.ToInt32(Request.QueryString["acc"]);
                }
            }
            catch
            {
            };

            switch (backpage)
            {
                case 1:
                    ViewState["SESSION_SOLICITANTE"] = "Titular";
                    ViewState["TIPO_SOLICITANTE"] = "Titular";
                    ViewState["URL_SOLICITANTE"] = "~/Mantenedores/Titulares/agregarTitular.aspx?acc=" + acc;
                    ViewState["bp"] = "1";
                    break;
                case 2:
                    ViewState["SESSION_SOLICITANTE"] = "RepresentanteLegal";
                    ViewState["TIPO_SOLICITANTE"] = "Representante Legal";
                    ViewState["URL_SOLICITANTE"] = "~/Mantenedores/Titulares/agregarRepresentanteLegal.aspx?acc=" + acc;
                    ViewState["bp"] = "2";
                    break;
                case 3:
                    ViewState["SESSION_SOLICITANTE"] = "Operador";
                    ViewState["TIPO_SOLICITANTE"] = "Operador";
                    ViewState["URL_SOLICITANTE"] = "~/Mantenedores/Titulares/agregarOperador.aspx?acc=" + acc;
                    ViewState["bp"] = "3";
                    break;
            };

            RutPersona.Text = rutPersona;

        }

        protected void ModificarNombre_Click(object sender, EventArgs e)
        {
            msgGrilla.Text = "";
            Content_msgGrilla.Visible = false;
            
            NombrePersona nombrePersona = new NombrePersona();

            if (NombrePersona.Text != null && !NombrePersona.Text.Equals(""))
            {
                nombrePersona.nombre = NombrePersona.Text.ToUpper();
            }

            NombrePersona.Text = NombrePersona.Text.ToUpper(); 
                        
            //nombrePersona.numeroCI = Convert.ToInt32(NumeroControlIngresoNombre.Text);
            //nombrePersona.fechaCI = Convert.ToDateTime(FechaTextRecepcion.Text);
            nombrePersona.fechaIngresoSistema = DateTime.Today;
            
            //ArchivoBinarioEspecial archivoBinarioEspecial = new ArchivoBinarioEspecial();
            
            //if (ArchivoAdjuntoNombrePersona.HasFile)
            //{
            //    archivoBinarioEspecial.nombreFisico = ArchivoAdjuntoNombrePersona.PostedFile.FileName.Substring(0, ArchivoAdjuntoNombrePersona.PostedFile.FileName.LastIndexOf("."));
            //    archivoBinarioEspecial.nombreArchivo = ArchivoAdjuntoNombrePersona.PostedFile.FileName.Substring(0, ArchivoAdjuntoNombrePersona.PostedFile.FileName.LastIndexOf("."));
            //    archivoBinarioEspecial.formato = ArchivoAdjuntoNombrePersona.PostedFile.FileName.Substring(ArchivoAdjuntoNombrePersona.PostedFile.FileName.LastIndexOf(".") + 1).ToLower();
            //    archivoBinarioEspecial.tamano = ArchivoAdjuntoNombrePersona.PostedFile.InputStream.Length;
            //    archivoBinarioEspecial.bytes = ArchivoAdjuntoNombrePersona.FileBytes;
            //    nombrePersona.archivo = archivoBinarioEspecial;
            //}
            
            List<String> listaErroresNombrePersona = mantenedorTitularesValidacion.validaNombrePersona(nombrePersona);

            if (listaErroresNombrePersona.Count <= 0)
            {

                if (ViewState["bp"].ToString().Equals("1"))
                {
                    Solicitante solicitante = (Solicitante)Session[ViewState["SESSION_SOLICITANTE"].ToString()];
                    List<NombrePersona>  listaNombrePersona = solicitante.listaNombrePersona;
                    if (listaNombrePersona == null)
                    {
                        listaNombrePersona = new List<NombrePersona>();
                    }
                    listaNombrePersona.Add(nombrePersona);

                    /* Se actualiza el nombre del titular */
                    solicitante.listaNombrePersona = listaNombrePersona;
                    solicitante.nombreSolicitante = nombrePersona.nombre;

                    Session[ViewState["SESSION_SOLICITANTE"].ToString()] = (Solicitante) solicitante;

                }
                else if (ViewState["bp"].ToString().Equals("2")) {

                    RepLegal representanteLegal = (RepLegal)Session[ViewState["SESSION_SOLICITANTE"].ToString()];
                    List<NombrePersona> listaNombrePersona = representanteLegal.representanteLegal.listaNombrePersona;
                    if (listaNombrePersona == null)
                    {
                        listaNombrePersona = new List<NombrePersona>();
                    }
                    listaNombrePersona.Add(nombrePersona);

                    /* Se actualiza el nombre del Representante Legal */
                    representanteLegal.representanteLegal.nombreSolicitante = nombrePersona.nombre;
                    representanteLegal.representanteLegal.listaNombrePersona = listaNombrePersona;

                    Session[ViewState["SESSION_SOLICITANTE"].ToString()] = representanteLegal;
                }
                else if (ViewState["bp"].ToString().Equals("3")) {

                    Operador operador = (Operador)Session[ViewState["SESSION_SOLICITANTE"].ToString()];
                    List<NombrePersona> listaNombrePersona = operador.operador.listaNombrePersona;
                    if (listaNombrePersona == null)
                    {
                        listaNombrePersona = new List<NombrePersona>();
                    }
                    listaNombrePersona.Add(nombrePersona);

                    /* Se actualiza el nombre del Operador */
                    operador.operador.nombreSolicitante = nombrePersona.nombre;
                    operador.operador.listaNombrePersona = listaNombrePersona;

                    Session[ViewState["SESSION_SOLICITANTE"].ToString()] = operador;
                }

                msgGrilla.Text = "Se ha modificado el nombre del " + ViewState["TIPO_SOLICITANTE"].ToString();
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                Content_msgGrilla.Visible = true;

            }
            else
            {
                foreach (String error in listaErroresNombrePersona)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
            }
        }

        protected void Volver_Click(object sender, EventArgs e)
        {
            string path = ViewState["URL_SOLICITANTE"].ToString();
            Response.Redirect(path);
        }

        //protected void NombrePersona_TextChanged(object sender, EventArgs e)
        //{
        //    NombrePersona.Text = NombrePersona.Text.ToUpper();
        //}
    }
}