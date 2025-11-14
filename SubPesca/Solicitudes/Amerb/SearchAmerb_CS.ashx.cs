using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Entidades;

namespace SubPesca.Solicitudes.Amerb
{
    /// <summary>
    /// Descripción breve de SearchAmerb_CS
    /// </summary>
    public class SearchAmerb_CS : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string prefixText = context.Request.QueryString["q"];

            StringBuilder sb = new StringBuilder();
            DataExternaDA dataExternaDA = new DataExternaDA();

            ParametroGenerico parametroGenerico = new ParametroGenerico();

            //Se debe cambiar al PL de Amerb
            List<ParametroGenerico> listaAmerb = dataExternaDA.ObtieneListaAMERB_Externo(prefixText);
            int i = 0;
            foreach (ParametroGenerico amerb in listaAmerb)
            {
                if (i < 10)
                {
                    sb.Append(amerb.descripcion).Append(Environment.NewLine);
                    i++;
                }
            }

            context.Response.Write(sb.ToString());

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