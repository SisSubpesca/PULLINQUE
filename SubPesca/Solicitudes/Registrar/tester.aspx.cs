using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;

namespace SubPesca.Solicitudes.Registrar
{
    public partial class tester : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
      
            ProyectoTecnicoService proyService = new ProyectoTecnicoService();
            ProyectoTecnico proyTecnicoForm = proyService.ObtenerProyectoTecnico(459, 0);
            CargarGrillaPT("ArchivoBinario", proyTecnicoForm);
        }


        private void CargarGrillaPT(string seccion, ProyectoTecnico proyectoTec)
        {
            switch (seccion)
            {

              
                case "ArchivoBinario":

                    ExportarGrillaAXU.Visible = false;

                    List<ArchivosAdjPT> List_ArchivoBinarioEspecial = new List<ArchivosAdjPT>();
                    if (proyectoTec != null && proyectoTec.archivoBinarioList != null && proyectoTec.archivoBinarioList.Count > 0)
                    {
                        List_ArchivoBinarioEspecial = proyectoTec.archivoBinarioList;
                    }
                    else
                    {
                        List_ArchivoBinarioEspecial = (List<ArchivosAdjPT>)ViewState["ArchivoBinario"];
                    }
                    if (List_ArchivoBinarioEspecial == null)
                    {
                        List_ArchivoBinarioEspecial = new List<ArchivosAdjPT>();
                    }

                    GridArchivoAdjunto.DataSource = List_ArchivoBinarioEspecial;
                    GridArchivoAdjunto.DataBind();

                    if (List_ArchivoBinarioEspecial != null && List_ArchivoBinarioEspecial.Count > 0)
                    {
                        ExportarGrillaAXU.Visible = true;
                    }

                    ViewState["ArchivoBinario"] = (List<ArchivosAdjPT>)List_ArchivoBinarioEspecial;
                    UpdatePanel_ArchivoAdjunto.Update();

                    break;
            }

        }

    

        protected void ExportarGrilla2_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();

            CargarGrillaPT("ArchivoBinario", null);

            int cantidad = GridArchivoAdjunto.Columns.Count;

            if (cantidad > 1)
            {
                cantidad = cantidad - 1;
            }

            GridArchivoAdjunto.Columns.RemoveAt(cantidad);
            grilla = GridArchivoAdjunto;

            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export("ProyectosTecnicos.xls", grilla);
        }
    }
}