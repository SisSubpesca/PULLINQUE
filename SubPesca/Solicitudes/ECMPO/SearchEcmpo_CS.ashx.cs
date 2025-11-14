using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Entidades;

namespace SubPesca.Solicitudes.ECMPO
{
    /// <summary>
    /// Descripción breve de SearchEcmpo_CS
    /// </summary>
    public class SearchEcmpo_CS : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string prefixText = context.Request.QueryString["q"];

            StringBuilder sb = new StringBuilder();
            DataExternaDA dataExternaDA = new DataExternaDA();

            ParametroGenerico parametroGenerico = new ParametroGenerico();

            List<ParametroGenerico> listaEcmpo = dataExternaDA.ObtieneListaEcmpo_Externo(prefixText);
            int i = 0;
            foreach (ParametroGenerico ecmpo in listaEcmpo)
            {
                if (i < 10)
                {
                    sb.Append(ecmpo.descripcion).Append(Environment.NewLine);
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