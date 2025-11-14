using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using System.Data;
using SubPesca.Mantenedores.Generales;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.usuario;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace SubPesca.AdministradorCorreo
{
    public partial class administradorDeCorreos : System.Web.UI.Page
    {

        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        
        MantenedorGeneralService mantenedorGeneralService = new MantenedorGeneralService();
        RolDA rolDA = new RolDA();
        TemplateAvisoDA templateAvisoDA = new TemplateAvisoDA();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (usuario_logeado == null)
                {
                    Response.Redirect("~/ingreso.aspx");

                }

                //Inicializar campos lista
                InicializarCampos();

                // Cargamos la grilla
                CargaGrilla();
            };
        }

        private void InicializarCampos()
        {
            RolUsuario.Items.Clear();
            RolUsuario.DataSource = rolDA.ListarRoles(new Rol());
            RolUsuario.DataTextField = "nombreRol";
            RolUsuario.DataValueField = "idRol";
            RolUsuario.DataBind();
            RolUsuario.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
        }

        // EVENTOS DE LA GRILLA
        protected void CargaGrilla()
        {
            TemplateAviso templateAviso = new TemplateAviso();
            List<TemplateAviso> templateAvisoList = mantenedorGeneralService.listarCorreoElectronico(templateAviso);
            
            int num_registros = 0;
            num_registros = templateAvisoList.Count;
            GridView1.DataSource = templateAvisoList;
            GridView1.DataBind();

            if (templateAvisoList != null && templateAvisoList.Count > 0)
            {
                ExportarGrilla.Visible = true;
                upd2.Update();
            }
            
        }

        protected void GridView1_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.EditIndex = -1;
            CargaGrilla();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Ver
                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    boton_ver.Visible = true;
                };

                // Modificar
                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_modificar != null)
                {
                    boton_modificar.Visible = true;
                };

            };
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Content_msgGrilla.Visible = false;

            string claveCorreoElectronico = e.CommandArgument.ToString();

            switch (e.CommandName)
            {
                case "Modificar":
                    cargarCorreoElectronico(claveCorreoElectronico, true);
                    break;

                case "Ver":
                    cargarCorreoElectronico(claveCorreoElectronico, false);
                    break;
            };
        }

        private void cargarCorreoElectronico(string claveCorreoElectronico, bool enabled)
        {
            LimpiarFormulario();
            
            Subject.Enabled = enabled;
            DescripcionCorreo.Enabled = enabled;
            Cuerpo.Enabled = enabled;
            RolUsuario.Enabled = enabled;
            Direcciones.Enabled = enabled;
            AplicaEnvio.Enabled = enabled;

            Modificar.Visible = enabled;

            TemplateAviso templateAviso = new Datos.Entidades.TemplateAviso();
            templateAviso.claveTemplateAviso = claveCorreoElectronico;
            templateAviso = mantenedorGeneralService.obtieneCorreoElectronico(templateAviso);

            if (templateAviso != null)
            {
                ClaveCorreo.Value = claveCorreoElectronico;
                ClaveTemplateAvisoDescr.Text = templateAviso.claveTemplateAvisoDescr;
                DescripcionCorreo.Text = templateAviso.descrTemplateAviso;
                Subject.Text = templateAviso.templateSubject;
                Cuerpo.Text = templateAviso.templateCuerpo;
                DescripcionCuerpo.Text = templateAviso.descrReempl;

                foreach (ListItem listItem in RolUsuario.Items)
                {
                    foreach (DestinatarioTemplate destinatarioTemplate in templateAviso.destinatariosRol)
                    {
                        if (destinatarioTemplate != null && destinatarioTemplate.rol != null && listItem.Value.Equals(Convert.ToString(destinatarioTemplate.rol.idRol)))
                        {
                            listItem.Selected = true;
                        }

                    }

                    if (listItem.Value.Equals(Convert.ToString("-1")))
                    {
                        listItem.Selected = false;
                    }
                }

                Direcciones.Text = templateAviso.destinatariosDireccionTemplateComa;

                AplicaEnvio.Checked = templateAviso.aplicaEnvio;
            }

            Content_PanelModificarCorreo.Visible = true;
            upd1.Update();
        }
        
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            CargaGrilla();
        }

        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();
            string nom_grilla = "correoElectronico";
            string ngrilla = "";

            CargaGrilla();

            switch (nom_grilla)
            {
                case "correoElectronico":
                    GridView1.Columns.RemoveAt(7);
                    grilla = GridView1;
                    ngrilla = "correoElectronico.xls";
                    break;

            };
            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export(ngrilla, grilla);
        }

        protected void Modificar_Click(object sender, EventArgs e)
        {
            TemplateAviso templateAviso = new Datos.Entidades.TemplateAviso();

            templateAviso.claveTemplateAviso = ClaveCorreo.Value;
            templateAviso.descrTemplateAviso = DescripcionCorreo.Text;
            templateAviso.templateSubject = Subject.Text;
            templateAviso.templateCuerpo = Cuerpo.Text;
            templateAviso.aplicaEnvio = AplicaEnvio.Checked;

            templateAviso.destinatariosRol = new List<DestinatarioTemplate>();

            DestinatarioTemplate destinatarioTemplate = null;

            foreach (ListItem listItem in RolUsuario.Items)
            {
                if (listItem.Selected)
                {
                    destinatarioTemplate = new Datos.Entidades.DestinatarioTemplate();
                    destinatarioTemplate.rol = new Rol();
                    destinatarioTemplate.rol.idRol = Convert.ToInt32(listItem.Value);
                    destinatarioTemplate.claveTemplateAviso = ClaveCorreo.Value;
                    templateAviso.destinatariosRol.Add(destinatarioTemplate);
                }
            }
            
            templateAviso.destinatariosTemplate = new List<DestinatarioTemplate>();

            if (!Direcciones.Text.Equals(""))
            {
                string[] direcciones = Direcciones.Text.Split(',');

                if (direcciones != null && direcciones.Length > 0)
                {
                    foreach (string direccion in direcciones)
                    {
                        destinatarioTemplate = new Datos.Entidades.DestinatarioTemplate();
                        destinatarioTemplate.emailDestinatario = direccion;
                        destinatarioTemplate.claveTemplateAviso = ClaveCorreo.Value;
                        templateAviso.destinatariosTemplate.Add(destinatarioTemplate);
                    }
                }
            }

            if (templateAviso != null) {

                bool resp = mantenedorGeneralService.modificarCorreoElectronico(templateAviso);
                if (resp)
                {
                    msgGrilla.Text = "El correo electrónico con clave " + templateAviso.claveTemplateAviso  + " ha sido modificado.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    GridView1.PageIndex = 0;
                    LimpiarFormulario();
                    CargaGrilla();
                }
                else {
                    msgGrilla.Text = "Se ha producido un error al intentar modificar el correo electrónico.";
                    Ico_msgGrilla.ImageUrl = "~/App_Themes/admin_style/images/important.gif";
                }
                Content_msgGrilla.Visible = true;

            }
            Content_PanelModificarCorreo.Visible = false;
        }

        private void LimpiarFormulario()
        {
            ClaveCorreo.Value = "";
            DescripcionCorreo.Text = "";
            Subject.Text = "";
            Cuerpo.Text = "";
            DescripcionCuerpo.Text = "";
            AplicaEnvio.Enabled = false;
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Content_PanelModificarCorreo.Visible = false;
        }

    }
}