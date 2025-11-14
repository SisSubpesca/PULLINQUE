using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Entidades;

namespace SubPesca.Solicitudes.Modificacion
{
    /// <summary>
    /// Descripción breve de adminSolicitudModificacion
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class adminSolicitudModificacion : System.Web.Services.WebService
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
    }
}
