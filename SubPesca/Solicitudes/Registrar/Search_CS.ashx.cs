using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.errores;

namespace SubPesca.Solicitudes.Registrar
{
    /// <summary>
    /// Descripción breve de Search_CS
    /// </summary>
    public class Search_CS : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            try
            {
                
                string prefixText = context.Request.QueryString["q"];

                StringBuilder sb = new StringBuilder();
                CartaDA cartaDA = new CartaDA();

                List<Carta> listaCartas = cartaDA.ListaCartaDA(null, prefixText);
                int i = 0;
                foreach (Carta carta in listaCartas)
                {
                    if (i < 10)
                    {
                        sb.Append(carta.descripcionCarta).Append(Environment.NewLine);
                        i++;
                    }
                }

                context.Response.Write(sb.ToString());
                 
                

            }
            catch{
                context.Response.Write("");
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}