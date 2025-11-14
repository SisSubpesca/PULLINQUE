using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using Datos.Entidades.Resolucion;
using Datos.Entidades;

namespace SubPesca.Resoluciones
{
    public partial class asignarResoluciones : System.Web.UI.UserControl
    {



        PermisosService permisosService = new PermisosService();



        protected void Page_Load(object sender, EventArgs e)
        {

        }








        //GRID RESOLUCIONES
        protected void GridResolucion_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {



                //ACCION
                HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
                if (hidden_accion != null && !hidden_accion.Value.Equals("") && Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR)
                {
                    e.Row.Attributes["style"] = "display:none";
                };


                //Borrar
                ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
                if (boton_borrar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.ELIMINAR))
                    {
                        boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar este documento?')");
                        boton_borrar.Visible = true;
                    }
                };

            };
        }


        //GRID RESOLUCIONES
        protected void GridResolucion_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument.ToString());

            ErroresInferior.Text = "";
            PanelErroresInferior.Visible = false;
            UpdatePanelErroresInferior.Update();


            switch (e.CommandName)
            {

                case "Eliminar":

                    //VALIDAR QUE SEA POSIBLE ELIMINAR
                    List<String> errores = new List<string>();
                    if (errores.Count == 0)
                    {

                        //ELIMINAR EL ELEMENTO DE LA LISTA
                        List<AsociacionResolucion> listaResoluciones = (List<AsociacionResolucion>)ViewState["Resoluciones"];

                        if (listaResoluciones != null)
                        {
                            foreach (AsociacionResolucion asociacionResolucion in listaResoluciones)
                            {

                                if (asociacionResolucion.index == index)
                                {
                                    asociacionResolucion.accion = accion.ELIMINAR;
                                }
                            }
                        }

                        ViewState["Resoluciones"] = listaResoluciones;
                    }
                    else
                    {

                        foreach (String error in errores)
                        {
                            ErroresInferior.Text = ErroresInferior.Text + error + "<br/>";
                        }
                        PanelErroresInferior.Visible = true;
                        UpdatePanelErroresInferior.Update();

                    }


                    CargarListaResoluciones();
                    break;

            };


        }


        //GRID RESOLUCIONES
        protected void GridResolucion_RowCreated(object sender, GridViewRowEventArgs e)
        {


        }


        //GRID RESOLUCIONES
        private void CargarListaResoluciones()
        {

            List<AsociacionResolucion> listaResoluciones = (List<AsociacionResolucion>)ViewState["Resoluciones"];

            if (listaResoluciones == null)
            {
                listaResoluciones = new List<AsociacionResolucion>();
            }


            GridResolucion.DataSource = listaResoluciones;
            GridResolucion.DataBind();


        }


        //GRID RESOLUCIONES
        protected void GridResolucion_Agregar(object sender, EventArgs e)
        {

            List<AsociacionResolucion> listaResoluciones = (List<AsociacionResolucion>)ViewState["Resoluciones"];
            int index = 0;


            if (listaResoluciones == null)
            {
                listaResoluciones = new List<AsociacionResolucion>();
            }
            else
            {
                index = listaResoluciones.Count;
            }


            AsociacionResolucion newReferencia = new AsociacionResolucion();
            newReferencia.index = index;
            newReferencia.accion = accion.INGRESAR;
            newReferencia.resolucion = new Resolucion();
            


            if (Convert.ToInt32(Resolucion.SelectedValue) > 0)
            {
                newReferencia.resolucion.idResolucion = Convert.ToInt32(Resolucion.SelectedValue);
            }


            if (Convert.ToInt32(Seccion.SelectedValue) > 0)
            {
                newReferencia.seccion = new ParametroGenerico(Convert.ToInt32(Seccion.SelectedValue));
            }



            //VALIDAR EL INGRESO DE LA NUEVA REFERENCIA
            List<String> errores = new List<string>();

            if (errores.Count > 0)
            {
                foreach (String error in errores)
                {
                    PanelErroresSuperior.Visible = true;
                    ErroresSuperior.Text = error;
                }
            }
            else
            {

                Resolucion.SelectedValue = "0";
                UpdatePanelResolucion.Update();


                ErroresSuperior.Text = "";
                PanelErroresSuperior.Visible = false;
                listaResoluciones.Add(newReferencia);

                ViewState["Resoluciones"] = (List<AsociacionResolucion>)listaResoluciones;
                this.CargarListaResoluciones();

            }

            UpdatePanelErroresSuperior.Update();
        }



    }
}