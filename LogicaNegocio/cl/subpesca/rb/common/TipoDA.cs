using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.AccesoDatos;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Data;
using Datos.Entidades;

namespace LogicaNegocio.cl.subpesca.rb.common
{
    public class TipoDA
    {

        public Logger Log { get; set; }
        public TipoDA()
        {
            this.Log = new Logger();
        }

        public List<ParametroGenerico> ListarTipo(String grupo)
        {
            try
            {
                ParametroGenerico paramAux = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTipo";

                if (grupo != null && !grupo.Equals(""))
                {
                    cnn.parametros.Add("@grupo", grupo);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                      paramAux = new ParametroGenerico(Convert.ToInt32(row["idTipo"]), row["nombreTipo"].ToString());
                      resp.Add(paramAux);
                    }
                }
                return resp;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            }
        }

       


    

        /**
        * Obtiene el tipo destinatario (campo tipo destinatario)
        **/
        public DataTable obtenerTipoDestinatario()
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbTipoDestinatario";
            
            DataTable dt = cnn.Execute();
            return dt;
        }


        /**
         *  Obtiene un origen/destinatario por su id
        */
        public DataTable obtenerTipoDestinatario(int idTipoDestinatario)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbTipoDestinatario";
            cnn.parametros.Add("@idTipoDestinatario", idTipoDestinatario);

            DataTable dt = cnn.Execute();
            return dt;
        }

        public List<ParametroGenerico> listarTipoDestinatario() {

            ParametroGenerico paramAux = null;
            List<ParametroGenerico> resp = new List<ParametroGenerico>();

            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbTipoDestinatario";

            DataTable dt = cnn.Execute();
            
            if(dt != null){
                foreach (DataRow row in dt.Rows)
                {

                    paramAux = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                    resp.Add(paramAux);
                }
            }
            return resp;
        }


        /**
        * Obtiene el tipo origen (campo tipo origen)
        **/
        public DataTable obtenerTipoOrigen()
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbTipoDestinatario";

            DataTable dt = cnn.Execute();
            return dt;
        }


        /**
        * Obtiene el tipo origen (campo tipo origen)
        **/
        public DataTable obtenerTipoOrigenPorId(int idTipoOrigen)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbTipoDestinatario";
            cnn.parametros.Add("@idTipoDestinatario", idTipoOrigen);

            DataTable dt = cnn.Execute();
            return dt;
        }

        
        /*
        public DataTable obteneSubReqPestaniaSeccion(int idPestania, int idSeccion)
        { 
        
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbSubReqPestaniaSeccion";
            cnn.parametros.Add("@idPestana", idPestania);
            cnn.parametros.Add("@idSeccion", idSeccion);

            DataTable dt = cnn.Execute();
            return dt;
        
        }
         * */

        public DataTable obtenerSeccion(int idSeccion)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbSeccion";

            if (idSeccion>0)
            {
                cnn.parametros.Add("@idSeccion", idSeccion);
            }
            

            DataTable dt = cnn.Execute();
            return dt;
        }


        public List<ParametroGenerico> listarSecciones(int idSeccion)
        {

            DataTable dt = this.obtenerSeccion(idSeccion);
            List<ParametroGenerico> listarSecciones = new List<ParametroGenerico>();
            ParametroGenerico parametroGenerico = null;

            if(dt != null){
                foreach (DataRow row in dt.Rows)
                {
                    parametroGenerico = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]), row["nombreSeccion"].ToString());
                    listarSecciones.Add(parametroGenerico);
                }
            }
            return listarSecciones;
        }

        public List<ParametroGenerico> ListarTipoAlimentoEspecie(int idClave,bool esGrupo)
        {
            try
            {
                ParametroGenerico paramAux = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTipoAlimentoEspecie";

                cnn.parametros.Add("@idClave", idClave);
                cnn.parametros.Add("@esGrupo", esGrupo);


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        paramAux = new ParametroGenerico(Convert.ToInt32(row["idTipoAlimento"]), row["nombreTipo"].ToString());
                        resp.Add(paramAux);
                    }
                }
                return resp;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            }
        }
      
    }
}
