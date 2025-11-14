using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using Datos.Entidades;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace SubPesca.Mantenedores.Titulares
{
    public partial class verContactoOperador : System.Web.UI.Page
    {

        MatrizSucursalDA matrizSucursalDA = new MatrizSucursalDA();
        OperadorDA operadorDA = new OperadorDA();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MatrizSucursal matrizSucursal = new MatrizSucursal();

                // Obtención de parámetros mediante GET/POST
                ObtencionParametros(matrizSucursal);

                if (matrizSucursal != null)
                {
                    if (matrizSucursal.idMatrizSuc > 0)
                    {
                        //CargarGrillaContactoMatrizSucursal(matrizSucursal);
                    }
                    else
                    {

                        CargarGrillaContactoMatrizSucursalSession(matrizSucursal);
                    }

                }
            }
        }

        private void CargarGrillaContactoMatrizSucursalSession(MatrizSucursal matrizSucursal)
        {
            Operador operador = (Operador)Session["Operador"];
            Solicitante solicitante = operador.operador;

            List<MatrizSucursal> listaMatrizSucursal = solicitante.matrizSucursales;
            List<Contacto> contactosMatrizSuc = null;

            int i = 0;
            foreach (MatrizSucursal matrizSucursalSession in listaMatrizSucursal)
            {
                if (matrizSucursal.index == i)
                {
                    contactosMatrizSuc = matrizSucursalSession.contactosMatrizSuc;
                    CargarLabelMatrizSucursal(matrizSucursalSession);
                }
                i++;
            }


            //GridContactoMatrizSucursales.DataSource = contactosMatrizSuc;
            //GridContactoMatrizSucursales.DataBind();
        
        }

        private void CargarLabelMatrizSucursal(MatrizSucursal matrizSucursalSession)
        {
            Direccion.Text = matrizSucursalSession.direccion;

            //if (matrizSucursalSession.matriz)
            //{
            //    CasaMatriz.Text = "Sí";
            //}
            //else
            //{
            //    CasaMatriz.Text = "No";
            //}

            if (matrizSucursalSession.region != null)
            {
                Region.Text = matrizSucursalSession.region.region;
            }

            if (matrizSucursalSession.region != null && matrizSucursalSession.region.comuna != null)
            {
                Comuna.Text = matrizSucursalSession.region.comuna.comuna;
            }

            //Casilla.Text = matrizSucursalSession.casilla;

            //if (matrizSucursalSession.comunaCasilla != null)
            //{
            //    CasillaComuna.Text = matrizSucursalSession.comunaCasilla.comuna;
            //}

            //NumeroControlIngreso.Text = Convert.ToString(matrizSucursalSession.numeroCI);

            //FechaControlIngreso.Text = Convert.ToString(matrizSucursalSession.fechaCI);
        }

        //private void CargarGrillaContactoMatrizSucursal(MatrizSucursal matrizSucursal)
        //{
        //    matrizSucursal = matrizSucursalDA.ObtieneMatrizSucursalContactos(matrizSucursal.idMatrizSuc);

        //    //GridContactoMatrizSucursales.DataSource = matrizSucursal.contactosMatrizSuc;
        //    //GridContactoMatrizSucursales.DataBind();

        //    MatrizSucursal matrizSucursalAux = operadorDA.ObtenerOperadorMatrizSuc_filtros(0, matrizSucursal.idMatrizSuc);
        //    rutOperador.Text = Convert.ToString(matrizSucursalAux.persona.rutPersona + "-" + matrizSucursalAux.persona.dvPersona);
        //    nombreOperador.Text = matrizSucursalAux.persona.nombreSolicitante;

        //    CargarLabelMatrizSucursal(matrizSucursal);
        //}

        private void ObtencionParametros(MatrizSucursal matrizSucursal)
        {
            // Se recibe el idMatrizSuc
            try
            {

                if (Request.QueryString["idMatrizSuc"] != null && Request.QueryString["index"] != null && Request.QueryString["acc"] != null)
                {

                    int idMatrizSuc = Convert.ToInt32(Request.QueryString["idMatrizSuc"]);
                    ViewState["acc"] = Request.QueryString["acc"];

                    if (idMatrizSuc > 0)
                    {
                        matrizSucursal.idMatrizSuc = Convert.ToInt32(Request.QueryString["idMatrizSuc"]);
                        matrizSucursal.bp = Convert.ToInt32(Request.QueryString["bp"]);

                    }
                    else
                    {
                        int index = Convert.ToInt32(Request.QueryString["index"]);

                        if (index > 0)
                        {
                            matrizSucursal.index = index;

                        }
                    }
                }

            }
            catch
            {

            };
        }

        /**
       * Método que agrega al evento para cambiar de página los resultados de la grilla de 
       * Contactos de un Operador.
       */
        protected void GridContactoOperador_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

        }

        protected void Volver_Click(object sender, EventArgs e)
        {
            string path = "";

            if (ViewState["acc"] != null && ViewState["acc"].ToString() != null && ViewState["acc"].ToString().Equals("5"))
            {
                path = "~/Mantenedores/Titulares/verOperador.aspx?acc=" + ViewState["acc"].ToString();
            }
            else
            {
                path = "~/Mantenedores/Titulares/agregarOperador.aspx?acc=" + ViewState["acc"].ToString();
            }
            Response.Redirect(path);
        }
    }
}