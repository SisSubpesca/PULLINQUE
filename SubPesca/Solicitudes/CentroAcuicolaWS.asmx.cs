using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using LogicaNegocio.cl.subpesca.rb.servicios.concesiones;
using Datos.Entidades;

namespace SubPesca.Solicitudes
{
    /// <summary>
    /// Descripción breve de CentroAcuicolaWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class CentroAcuicolaWS : System.Web.Services.WebService
    {

        [WebMethod]
        public List<string> BuscarCentros(string prefixText, int count)
        {

            ConcesionService concesionService = new ConcesionService();
            List<string> concesiones = new List<string>();

            List<ParametroGenerico> paramResp = concesionService.buscarConcesiones(prefixText);

            foreach (ParametroGenerico paramAux in paramResp)
            {
                concesiones.Add(paramAux.descripcion);
            }

            return concesiones;

        }
    }
}
