using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using Validaciones.cl.subpesca.rb.mantenedor;
using SubPesca.Solicitudes.Registrar;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Contantes;

namespace SubPesca.Mantenedores.Titulares
{
    public partial class asociarOperador : System.Web.UI.Page
    {


        OperadorDA operadorDA = new OperadorDA();
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

        protected void AsociarOperadorTitular_Click(object sender, EventArgs e)
        {
            Operador operador = new Operador();
            Solicitante solicitante = new Solicitante();

            try
            {

                String rutCompleto = Convert.ToString(RutPersona.Text);
                String[] rutPartes = rutCompleto.Split('-');
                solicitante.rut = Convert.ToInt32(rutPartes[0]);
                solicitante.dv = Convert.ToChar(rutPartes[1]);
                solicitante.nombreSolicitante = NombreOperador.Text;
                operador.operador = solicitante;

            }
            catch (Exception ex)
            {


            }

            //if (ArchivoAdjunto.HasFile)
            //{

            //    ArchivoBinarioEspecial archivoBinario = new ArchivoBinarioEspecial();

            //    archivoBinario.nombreFisico = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
            //    archivoBinario.nombreArchivo = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
            //    archivoBinario.formato = ArchivoAdjunto.PostedFile.FileName.Substring(ArchivoAdjunto.PostedFile.FileName.LastIndexOf(".") + 1).ToLower();
            //    archivoBinario.tamano = ArchivoAdjunto.PostedFile.InputStream.Length;
            //    archivoBinario.bytes = ArchivoAdjunto.FileBytes;
                
            //    operador.archivoAsoc = archivoBinario;
            //}

            //List<string> listaErroresOperador = mantenedorTitularesValidacion.validaAsociacionOperadorTitular(operador);
            
            Solicitante solicitanteTitular = (Solicitante)ViewState["Titular"];
            List<Operador> listaOperadores = solicitanteTitular.listaOperador;

            List<string> listaErroresOperador = mantenedorTitularesValidacion.validaRutOperador(operador);


            if (listaErroresOperador == null || listaErroresOperador.Count() == 0)
            {
                listaErroresOperador = mantenedorTitularesValidacion.validaAsociacionOperadorTitular(operador, listaOperadores);
            }


            if (listaErroresOperador.Count <= 0)
            {

                //Solicitante solicitanteTitular = (Solicitante)ViewState["Titular"];
                //List<Operador> listaOperadores = solicitanteTitular.listaOperador;

                int index = 0;
                if (listaOperadores == null)
                {
                    listaOperadores = new List<Operador>();
                }
                else {
                    index = listaOperadores.Count;
                }

                operador.accion = accion.INGRESAR;
                operador.index = Convert.ToInt32(index);

                listaOperadores.Add(operador);
                solicitanteTitular.listaOperador = listaOperadores;

                Session["Titular"] = (Solicitante)solicitanteTitular;

                msgGrilla.Text = "Se ha asociado exitosamente el Operador al Titular.";
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                Content_msgGrilla.Visible = true;

            }
            else
            {
                foreach (String error in listaErroresOperador)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }

            }
        }

        protected void ButtonRutPersona_Click(object sender, EventArgs e)
        {
            Operador operador = new Operador();
            Solicitante solicitante = new Solicitante();

            /*
            String rutCompleto = Convert.ToString(RutPersona.Text);
            String[] rutPartes = rutCompleto.Split('-');
            solicitante.rut = Convert.ToInt32(rutPartes[0]);
            solicitante.dv = Convert.ToChar(rutPartes[1]);
            operador.operador = solicitante;
            */

            String rutCompleto = Convert.ToString("");

            try
            {

                rutCompleto = Convert.ToString(RutPersona.Text);
                String[] rutPartes = rutCompleto.Split('-');
                solicitante.rut = Convert.ToInt32(rutPartes[0]);
                solicitante.dv = Convert.ToChar(rutPartes[1]);
                operador.operador = solicitante;

            }
            catch (Exception ex)
            {

                rutCompleto = Convert.ToString("");
                solicitante.rut = 0;
                solicitante.dv = Convert.ToChar("0");
                operador.operador = solicitante;
            }

            List<string> listaErroresOperador = mantenedorTitularesValidacion.validaRutOperador(operador);

            if (listaErroresOperador.Count <= 0)
            {
                /* Aquí se debe buscar el operador en la base de datos */
                operador = operadorDA.ObtenerOperador(operador.operador.rut,null);

                if (operador != null)
                {
                    NombreOperador.Text = operador.operador.nombreSolicitante;

                    PanelNombreOperador.Visible = true;
                    UpdatePanelNombreOperador.Update();
                    RutPersona2.Text = Convert.ToString(rutCompleto);
                }
                else
                {

                    NombreOperador.Text = "";
                    PanelNombreOperador.Visible = true;
                    UpdatePanelNombreOperador.Update();
                }

                RutPersona2.Text = Convert.ToString(rutCompleto);
            }
            else
            {
                NombreOperador.Text = "";
                PanelNombreOperador.Visible = true;
                UpdatePanelNombreOperador.Update();

                foreach (String error in listaErroresOperador)
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
    }
}