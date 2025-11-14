using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Entidades;

namespace SubPesca.Solicitudes.Registrar
{
    /// <summary>
    /// Descripción breve de adminSolicitudConcesion
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
     [System.Web.Script.Services.ScriptService]
    public class adminSolicitudConcesion : System.Web.Services.WebService
    {

        [WebMethod]
        public List<string> BuscarTitulares(string prefixText, int count)
        {
            //List<ParametroGenerico> paramResp = new List<ParametroGenerico>();

            SolicitudDA solicitudDA = new SolicitudDA();
            List<ParametroGenerico> paramResp = solicitudDA.ObtieneListaTitulares(prefixText);

            List<string> titulares = new List<string>();

            foreach (ParametroGenerico paramAux in paramResp)
            {
                titulares.Add(paramAux.descripcion);
            }


            return titulares;

        }

        [WebMethod]
        public List<string> BuscarAmerbs(string prefixText, int count)
        {
            

            DataExternaDA dataExternaDA = new DataExternaDA();
            List<ParametroGenerico> listaAmerb = dataExternaDA.ObtieneListaAMERB_Externo(prefixText);

            List<string> amerbs = new List<string>();

            foreach (ParametroGenerico paramAux in listaAmerb)
            {
                amerbs.Add(paramAux.descripcion);
            }


            return amerbs;

        }

        [WebMethod]
        public List<string> BuscarECMPOs(string prefixText, int count)
        {


            DataExternaDA dataExternaDA = new DataExternaDA();
            List<ParametroGenerico> listaECMPO = dataExternaDA.ObtieneListaEcmpo_Externo(prefixText);

            List<string> ecmpos = new List<string>();

            foreach (ParametroGenerico paramAux in listaECMPO)
            {
                ecmpos.Add(paramAux.descripcion);
            }


            return ecmpos;

        }
    }
}
