using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using Validaciones.cl.subpesca.rb.mantenedor;
using SubPesca.Utilidades;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Contantes;

namespace SubPesca.Mantenedores.Titulares
{
    public partial class asociarRepresentanteLegal : System.Web.UI.Page
    {


        RepLegalDA repLegalDA = new RepLegalDA();
        MantenedorTitularesValidacion mantenedorTitularesValidacion = new MantenedorTitularesValidacion();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ObtenerParametros();
            }
        }

        private void ObtenerParametros()
        {
            try
            {
                if (Request.QueryString["acc"] != null)
                {

                    String acc = Request.QueryString["acc"];
                    ViewState["acc"] = acc;
                    Solicitante solicitante = (Solicitante)Session["Titular"];

                    RutTitular.Text = solicitante.rut + "-" + solicitante.dv;

                    ViewState["Titular"] = solicitante;

                }
                else
                {

                }

            }
            catch
            {

            };
        }

        protected void AsociarRepresentante_Click(object sender, EventArgs e)
        {
            RepLegal repLegal = new RepLegal();
            Solicitante solicitante = new Solicitante();
            
            /*
            String rutCompleto = Convert.ToString(RutPersona.Text);
            String[] rutPartes = rutCompleto.Split('-');
            solicitante.rut = Convert.ToInt32(rutPartes[0]);
            solicitante.dv = Convert.ToChar(rutPartes[1]);
            solicitante.nombreSolicitante = NombreRepresentanteLegal.Text;
            repLegal.representanteLegal = solicitante;
            */

            try
            {
                String rutCompleto = Convert.ToString(RutPersona.Text);
                String[] rutPartes = rutCompleto.Split('-');
                solicitante.rut = Convert.ToInt32(rutPartes[0]);
                solicitante.dv = Convert.ToChar(rutPartes[1].Trim());
                solicitante.nombreSolicitante = NombreRepresentanteLegal.Text;
                repLegal.representanteLegal = solicitante;

            }
            catch (Exception ex)
            {


            }

            //if (ArchivoAdjunto.HasFile)
            //{
            //    ArchivoBinarioEspecial archivoBinario = new ArchivoBinarioEspecial();

            //    archivoBinario.nombreFisico = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
            //    archivoBinario.nombreArchivo = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
            //    archivoBinario.formato = ArchivoAdjunto.PostedFile.FileName.Substring(ArchivoAdjunto.PostedFile.FileName.LastIndexOf(".") + 1).ToLower(); ;
            //    archivoBinario.tamano = ArchivoAdjunto.PostedFile.InputStream.Length;
            //    archivoBinario.bytes = ArchivoAdjunto.FileBytes;
                
            //    repLegal.archivoAsoc = archivoBinario;
            //}

            //List<string> listaErroresRepresentanteLegal = mantenedorTitularesValidacion.validaAsociacionRepresentanteLegalTitular(repLegal);

            Solicitante solicitanteTitular = (Solicitante)ViewState["Titular"];
            List<RepLegal> listaRepresentantesLegales = solicitanteTitular.listaRepresentanteLegal;

            List<string> listaErroresRepresentanteLegal = mantenedorTitularesValidacion.validaRutRepresentanteLegal(repLegal);

            if (listaErroresRepresentanteLegal == null || listaErroresRepresentanteLegal.Count() == 0)
            {
                listaErroresRepresentanteLegal = mantenedorTitularesValidacion.validaAsociacionRepresentanteLegalTitular(repLegal, listaRepresentantesLegales);
            }

            if (listaErroresRepresentanteLegal.Count <= 0)
            {

               //Solicitante solicitanteTitular = (Solicitante)ViewState["Titular"];
               //List<RepLegal> listaRepresentantesLegales = solicitanteTitular.listaRepresentanteLegal;

               int index = 0;
               if (listaRepresentantesLegales == null)
               {
                   listaRepresentantesLegales = new List<RepLegal>();
               }
               else {
                   index = listaRepresentantesLegales.Count;
               }
               repLegal.index = Convert.ToInt32(index);
               repLegal.accion = accion.INGRESAR;

               listaRepresentantesLegales.Add(repLegal);
               solicitanteTitular.listaRepresentanteLegal = listaRepresentantesLegales;

               Session["Titular"] = (Solicitante) solicitanteTitular;

               msgGrilla.Text = "Se ha asociado exitosamente el Representante Legal al Titular.";
               Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
               Content_msgGrilla.Visible = true;

            }
            else {
                foreach (String error in listaErroresRepresentanteLegal)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
                
            }
        }

        protected void Volver_Click(object sender, EventArgs e)
        {
            string path = "~/Mantenedores/Titulares/agregarTitular.aspx?acc=" + ViewState["acc"].ToString();
            Response.Redirect(path);
        }

        protected void BuscarRepresentante_Click(object sender, EventArgs e)
        {

            RepLegal repLegal = new RepLegal();
            Solicitante solicitante = new Solicitante();

            String rutCompleto = Convert.ToString("");

            try
            {

                rutCompleto = Convert.ToString(RutPersona.Text);
                String[] rutPartes = rutCompleto.Split('-');
                solicitante.rut = Convert.ToInt32(rutPartes[0]);
                solicitante.dv = Convert.ToChar(rutPartes[1].Trim());
                repLegal.representanteLegal = solicitante;


            }
            catch (Exception ex)
            {

                rutCompleto = Convert.ToString("");
                solicitante.rut = 0;
                solicitante.dv = Convert.ToChar("0");
                repLegal.representanteLegal = solicitante;

            }


            List<string> listaErroresRepresentanteLegal = mantenedorTitularesValidacion.validaRutRepresentanteLegal(repLegal);

            if (listaErroresRepresentanteLegal.Count <= 0)
            {
                /* Aquí se debe buscar el representante legal en la base de datos */
                RepLegal representanteLegal = repLegalDA.ObtieneRepresentanteLegal(repLegal.representanteLegal.rut, null);

                if (representanteLegal != null)
                {
                    NombreRepresentanteLegal.Text = representanteLegal.representanteLegal.nombreSolicitante;
                    //TipoRepresentanteLegal.Text = representanteLegal.representanteLegal.tipoPersona.descripcion;

                    PanelNombreRepresentanteLegal.Visible = true;
                    UpdatePanelNombreRepresentanteLegal.Update();
                    RutPersona2.Text = Convert.ToString(rutCompleto);
                }
                else
                {

                    NombreRepresentanteLegal.Text = "";
                    PanelNombreRepresentanteLegal.Visible = true;
                    UpdatePanelNombreRepresentanteLegal.Update();
                }


            }
            else
            {

                NombreRepresentanteLegal.Text = "";
                PanelNombreRepresentanteLegal.Visible = true;
                UpdatePanelNombreRepresentanteLegal.Update();

                foreach (String error in listaErroresRepresentanteLegal)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
            }

        }
    }
}