using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.mantenedores;

namespace SubPesca.Mantenedores.Titulares
{
    public partial class verContactoTitular : System.Web.UI.Page
    {
        LogicaNegocio.cl.subpesca.rb.solicitud.MatrizSucursalDA matrizSucursalDA = new LogicaNegocio.cl.subpesca.rb.solicitud.MatrizSucursalDA();

        
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

        /* Carga la grilla de Contactos desde la session */
        private void CargarGrillaContactoMatrizSucursalSession(MatrizSucursal matrizSucursal)
        {
            
            Solicitante solicitante = (Solicitante) Session["Titular"];
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

            rutTitular.Text = Convert.ToString(solicitante.rut + "-" + solicitante.dv);
            nombreTitular.Text = solicitante.nombreSolicitante;
        
        }

        private void CargarLabelMatrizSucursal(MatrizSucursal matrizSucursalSession)
        {

            Direccion.Text = matrizSucursalSession.direccion;

            //if (matrizSucursalSession.matriz)
            //{
            //    CasaMatriz.Text = "Sí";
            //}else{
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

        ///* Carga la grilla desde la base de datos */
        //private void CargarGrillaContactoMatrizSucursal(MatrizSucursal matrizSucursal)
        //{
            
        //    //MantenedorTitularService mantenedorTitularService = new MantenedorTitularService();
        //    //List<MatrizSucursal> listMatrizSucursal = mantenedorTitularService.obtenerContactoMatrizSucursal(matrizSucursal);

        //    matrizSucursal = matrizSucursalDA.ObtieneMatrizSucursalContactos(matrizSucursal.idMatrizSuc);

        //    //GridContactoMatrizSucursales.DataSource = matrizSucursal.contactosMatrizSuc;
        //    //GridContactoMatrizSucursales.DataBind();

        //    MatrizSucursal matrizSucursalAux = matrizSucursalDA.ObtenerTitularMatrizSucFiltros(0, matrizSucursal.idMatrizSuc);
        //    rutTitular.Text = Convert.ToString(matrizSucursalAux.persona.rutPersona + "-" + matrizSucursalAux.persona.dvPersona);
        //    nombreTitular.Text = matrizSucursalAux.persona.nombreSolicitante;

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
                    else {
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

    }
}