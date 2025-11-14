using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace SubPesca.Solicitudes.Modificacion
{
    /// <summary>
    /// Descripción breve de antDelSector
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    
    [System.Web.Script.Services.ScriptService]
    public class antDelSector : System.Web.Services.WebService
    {

        [WebMethod]
        public List<string> BuscarCartaAntSector(string prefixText, int count)
        {

            CartaDA cartaDA = new CartaDA();

            List<Carta> listaCartas = cartaDA.ListaCartaDA(null, prefixText);

            List<string> cartas = new List<string>();

            foreach (Carta carta in listaCartas)
            {
                cartas.Add(carta.descripcionCarta);
            }
            return cartas;
        }

    }
}
