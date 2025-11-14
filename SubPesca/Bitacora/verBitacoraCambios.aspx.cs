using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SubPesca.Bitacora
{
    public partial class verBitacoraCambios : System.Web.UI.Page
    {

        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            // PAGE LOAD
            if (!Page.IsPostBack)
            {

                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (usuario_logeado == null)
                {
                    Response.Redirect("~/ingreso.aspx");
                }

                Initialize_Form();

                // Cargamos la grilla
                CargaGrilla();
            }
        }

        private void CargaGrilla()
        {
            int pagina = 0;
            
            GridBitacora.PageIndex = pagina;
            GridBitacora.DataSource = new List<String>();
            GridBitacora.DataBind();

        }

        protected void Limpiar_Click(object sender, EventArgs e)
        {
            NPert.Text = "";
            IdentificadorSolicitud.Text = "";
            CodigoCentro.Text = "";
            CampoModificado.SelectedValue = "-1";
        }


        protected void Initialize_Form()
        {
            // Cargamos los combobox
            Initialize_Comboboxs();

        }


        protected void Initialize_Comboboxs()
        {

            Carga_Combobox("CampoModificado");
            CampoModificado.SelectedValue = "0";
        }

        protected void Carga_Combobox(string combobox)
        {
            switch (combobox)
            {

                case "CampoModificado":
                    break;
            }
        }
        protected void FiltrarCargaGrilla(object sender, EventArgs e)
        {

        }
    }
}