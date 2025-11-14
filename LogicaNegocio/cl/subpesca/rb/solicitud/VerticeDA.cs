using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using Datos.AccesoDatos;

namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class VerticeDA
    {

        Logger logger = new Logger();

        //GUARDA UN VERTICE 
        public bool GuardarVertice(Vertice vertice, int idUsuario)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbVertice";
                cnn.parametros.Add("@idSolicitud", vertice.idSolicitud);
                cnn.parametros.Add("@idVertice", vertice.idVertice);
                cnn.parametros.Add("@idPoligono", vertice.idPoligono);
                
                if (vertice.vertice!=null && vertice.vertice.id > 0)
                {
                    cnn.parametros.Add("@codVertice", vertice.vertice.id);
                }
                cnn.parametros.Add("@latGrados", vertice.latitudHora);
                cnn.parametros.Add("@latMinutos", vertice.latitudMinuto);
                cnn.parametros.Add("@latSegundos", vertice.latitudSegundo);
                cnn.parametros.Add("@longGrados", vertice.longitudHora);
                cnn.parametros.Add("@longMinutos", vertice.longitudMinuto);
                cnn.parametros.Add("@longSegundos", vertice.longitudSegundo);
                cnn.parametros.Add("@utmNorte", vertice.utmN);
                cnn.parametros.Add("@utmEste", vertice.utmE);
                cnn.parametros.Add("@calculoLat", vertice.latitudDecimal);
                cnn.parametros.Add("@calculoLong", vertice.longitudDecimal);
                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }

                DataTable dt = cnn.Execute();
                vertice.idVertice = Convert.ToInt32(dt.Rows[0]["idVertice"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        /**
      * Obtiene vertice asoiciado a un poligono en particular.
      */
        public Vertice ObtieneVertice(int idPoligono, int idVertice)
        {
            try
            {
                Vertice verticeResp = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbVertice";
                if (idPoligono>0)
                {
                    cnn.parametros.Add("@idPoligono", idPoligono);
                }
                
                cnn.parametros.Add("@idVertice", idVertice);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                       verticeResp = new Vertice();
                       verticeResp.idVertice = Convert.ToInt32(row["idVertice"]);
                       verticeResp.idPoligono = Convert.ToInt32(row["idPoligono"]);
                       verticeResp.vertice = new ParametroGenerico(Convert.ToInt32(row["codVertice"]), row["nombreVertice"].ToString());
                       verticeResp.latitudHora = Convert.ToInt32(row["latGrados"]);
                       verticeResp.latitudMinuto = Convert.ToInt32(row["latMinutos"]);
                       verticeResp.latitudSegundo = Convert.ToDouble(row["latSegundos"]);
                       verticeResp.longitudHora = Convert.ToInt32(row["longGrados"]);
                       verticeResp.longitudMinuto = Convert.ToInt32(row["longMinutos"]);
                       verticeResp.longitudSegundo = Convert.ToDouble(row["longSegundos"]);
                       verticeResp.utmN = Convert.ToDouble(row["utmNorte"]);
                       verticeResp.utmE = Convert.ToDouble(row["utmEste"]);
                       verticeResp.latitudDecimal = Convert.ToDouble(row["calculoLat"]);
                       verticeResp.longitudDecimal = Convert.ToDouble(row["calculoLong"]);
                    }
                }

                return verticeResp;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        /**
     * Conjunto de vertices asoiciado a un poligono en particular.
     */
        public List<Vertice> ListarVertice(int idPoligono, int idVertice)
        {
            try
            {
                Vertice verticeResp = null;
                List<Vertice> resp = new List<Vertice>();
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbVertice";
                if (idPoligono > 0)
                {
                    cnn.parametros.Add("@idPoligono", idPoligono);
                }
                if (idVertice>0)
                {
                    cnn.parametros.Add("@idVertice", idVertice);
                }
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        verticeResp = new Vertice();
                        verticeResp.idVertice = Convert.ToInt32(row["idVertice"]);
                        verticeResp.idPoligono = Convert.ToInt32(row["idPoligono"]);
                        verticeResp.vertice = new ParametroGenerico(Convert.ToInt32(row["codVertice"]), row["nombreVertice"].ToString());
                        verticeResp.latitudHora = Convert.ToInt32(row["latGrados"]);
                        verticeResp.latitudMinuto = Convert.ToInt32(row["latMinutos"]);
                        verticeResp.latitudSegundo = Convert.ToDouble(row["latSegundos"]);
                        verticeResp.longitudHora = Convert.ToInt32(row["longGrados"]);
                        verticeResp.longitudMinuto = Convert.ToInt32(row["longMinutos"]);
                        verticeResp.longitudSegundo = Convert.ToDouble(row["longSegundos"]);
                        verticeResp.utmN = Convert.ToDouble(row["utmNorte"]);
                        verticeResp.utmE = Convert.ToDouble(row["utmEste"]);
                        verticeResp.latitudDecimal = Convert.ToDouble(row["calculoLat"]);
                        verticeResp.longitudDecimal = Convert.ToDouble(row["calculoLong"]);

                        resp.Add(verticeResp);
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

        //Elimina registro de un vertice en particular o un conjunto de vertices asociados a un poligono.
        public bool EliminarVertice(int idVertice, int idPoligono, int idUsuario)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbVertice";
                cnn.parametros.Add("@idVertice", idVertice);
                if (idPoligono > 0)
                {
                    cnn.parametros.Add("@idPoligono", idPoligono);
                }
                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }

                DataTable dt = cnn.Execute();

                return true;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }



     }
}
