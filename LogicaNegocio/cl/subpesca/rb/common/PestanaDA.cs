using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.AccesoDatos;
using Datos.Entidades;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.errores;




namespace LogicaNegocio.cl.subpesca.rb.common
{
    public class PestanaDA
    {
        Logger logger = new Logger();
        
        public List<ParametroGenerico> ListarPestania(int idPestania)
        {
            try
            {
                ParametroGenerico paramAux = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbPestana";

                if (idPestania > 0)
                {
                    cnn.parametros.Add("@idPestana", idPestania);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        paramAux = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), row["nombrePestana"].ToString());

                        resp.Add(paramAux);

                    }
                }
                return resp;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public ParametroGenerico obtenerPestania(int idPestania)
        {
            try
            {
                ParametroGenerico paramAux = null;
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbPestana";
                cnn.parametros.Add("@idPestana", idPestania);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        paramAux = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), row["nombrePestana"].ToString());

                    }
                }
                return paramAux;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        /*
        public List<Datos.Utilidades.rbTipo> GetTipoDocumentoByPestana(int idPestana)
        {
            Datos.Utilidades.DBAmbientalDataContext dc = GetDC();
            IQueryable<Datos.Utilidades.rbTipo> query = from rbTipoDocPestania in dc.rbTipoDocPestania join rbTipo in dc.rbTipo on rbTipoDocPestania.idTipo equals rbTipo.idTipo select new { idTipo = rbTipo.idTipo, nombreTipo = rbTipo.nombreTipo, grupo = rbTipo.grupo }; 
            return ConsultaALista<Datos.Utilidades.rbTipo>(query);
        }
         * */


       
    }
}
