using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Entidades;
using Validaciones.cl.subpesca.rb.mantenedor;
using SubPesca.Utilidades;
using Datos.Contantes;

namespace SubPesca.Mantenedores.Titulares
{
    public partial class agregarContactos : System.Web.UI.Page
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema

        RegionDA regionDA = new LogicaNegocio.cl.subpesca.rb.common.RegionDA();
        ComunaDA comunaDA = new ComunaDA();
        TipoDA tipoDa = new TipoDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();

        MantenedorTitularesValidacion mantenedorTitularesValidacion = new MantenedorTitularesValidacion();
        
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ObtencionParametros();
                InicializarFormulario();
            }
        }

        private void ObtencionParametros()
        {
            // Se recibe el rutPersona y bp (que indica que pagina invocó contactoDireccion)
            try
            {
                if (Request.QueryString["rutPersona"] != null && Request.QueryString["bp"] != null && Request.QueryString["acc"] != null)
                {
                    String backPage = Request.QueryString["bp"];
                    String acc = Request.QueryString["acc"];

                    if (backPage.Equals("1"))
                    { //Titulares
                        Solicitante solicitante = (Solicitante)Session["Titular"];
                        ViewState["SOLICITANTE"] = solicitante;
                        ViewState["TIPO_SOLICITANTE"] = "Titular";
                        ViewState["SESSION_SOLICITANTE"] = "Titular";
                        ViewState["PATH_BP"] = "~/Mantenedores/Titulares/agregarTitular.aspx?acc=" + acc;
                        ViewState["ACCION"] = acc;
                    }

                    else if (backPage.Equals("2")) //Representantes Legales
                    {
                        RepLegal repLegal = (RepLegal)Session["RepresentanteLegal"];
                        Solicitante solicitante = repLegal.representanteLegal;
                        ViewState["SOLICITANTE"] = solicitante;
                        ViewState["TIPO_SOLICITANTE"] = "Representante Legal";
                        ViewState["SESSION_SOLICITANTE"] = "RepresentanteLegal";
                        ViewState["PATH_BP"] = "~/Mantenedores/Titulares/agregarRepresentanteLegal.aspx?acc=" + acc;
                        ViewState["ACCION"] = acc;
                    }

                    else if (backPage.Equals("3")) //Operadores
                    {
                        Operador operador = (Operador)Session["Operador"];
                        Solicitante solicitante = operador.operador;
                        ViewState["SOLICITANTE"] = solicitante;
                        ViewState["TIPO_SOLICITANTE"] = "Operador";
                        ViewState["SESSION_SOLICITANTE"] = "Operador";
                        ViewState["PATH_BP"] = "~/Mantenedores/Titulares/agregarOperador.aspx?acc=" + acc;
                        ViewState["ACCION"] = acc;
                    }
                }
                else
                {

                }

            }
            catch
            {

            };
        }

        private void InicializarFormulario()
        {
            Initialize_Comboboxs();
        }

        protected void Initialize_Comboboxs()
        {
            Carga_Combobox("TipoContacto");
            TipoContacto.SelectedValue = "-1";

        }

        private void Carga_Combobox(string combobox)
        {
            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

            switch (combobox)
            {
                case "TipoContacto":
                    // Cargamos el combobox: Tipo Contacto
                    TipoContacto.Items.Clear();
                    TipoContacto.DataSource = tipoDa.ListarTipo("TIPO_CONTACTO");
                    TipoContacto.DataTextField = "descripcion";
                    TipoContacto.DataValueField = "id";
                    TipoContacto.DataBind();
                    TipoContacto.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
            }
        }

        
        private void limpiarDireccionMatriz()
        {
            TipoContacto.SelectedValue = "-1";
            ValorContacto.Text = "";
            DetalleContacto.Text = "";
            
        }
        

        protected void GuardarContacto_Click(object sender, EventArgs e)
        {
            Solicitante solicitante = (Solicitante)ViewState["SOLICITANTE"];
            List<Contacto> listaContactos = solicitante.listaContacto;

            int index = 0;
            if (listaContactos == null)
            {
                listaContactos = new List<Contacto>();
            }
            else
            {
                index = listaContactos.Count;
            }

            Contacto contacto = new Contacto();
            contacto.tipoContacto = new ParametroGenerico();
            contacto.tipoContacto.id = Convert.ToInt32(TipoContacto.SelectedItem.Value);
            contacto.tipoContacto.descripcion = TipoContacto.SelectedItem.Text;
            contacto.index = Convert.ToInt32(index);

            contacto.valorContacto = ValorContacto.Text;
            contacto.detalle = DetalleContacto.Text;

            List<string> listaErroresContactos = mantenedorTitularesValidacion.validaContacto(listaContactos, contacto);

            if (listaErroresContactos.Count <= 0)
            {

                listaContactos.Add(contacto);
                solicitante.listaContacto = listaContactos;

                String sessionName = ViewState["SESSION_SOLICITANTE"].ToString();

                if (sessionName != null && sessionName.Equals("Titular"))
                {
                    Session[sessionName] = (Solicitante)solicitante;
                }
                else if (sessionName != null && sessionName.Equals("RepresentanteLegal"))
                {
                    RepLegal repLegal = new RepLegal();
                    repLegal.representanteLegal = solicitante;

                    Session[sessionName] = (RepLegal)repLegal;

                }
                else if (sessionName != null && sessionName.Equals("Operador"))
                {
                    Operador operador = new Operador();
                    operador.operador = solicitante;

                    Session[sessionName] = (Operador)operador;
                }

                LabelContacto.Text = "Se ha guardado exitosamente el contacto al " + ViewState["TIPO_SOLICITANTE"].ToString() + ".";
                Ico_Contacto.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                PanelContacto.Visible = true;

                limpiarFormulario();
            }
            else
            {
                foreach (String error in listaErroresContactos)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
            }
        }

        private void limpiarFormulario()
        {
            TipoContacto.SelectedValue = "-1";
            ValorContacto.Text = "";
            DetalleContacto.Text = "";
        }

        protected void Volver_Click(object sender, EventArgs e)
        {
            string path = ViewState["PATH_BP"].ToString();
            Response.Redirect(path);
        }
    }
}