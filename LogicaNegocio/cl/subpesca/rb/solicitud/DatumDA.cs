using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.AccesoDatos;
using System.Data;
using Datos.Entidades;

namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
     public class DatumDA
    {
        
        public Logger Log { get; set; }

        public DatumDA()
        {
            this.Log = new Logger();
        }

         /**
         * Método que obtiene el DATUM (pudiendose filtrar por idDatum y el nombre de la carta asociada) 
         */
         public object obtenerDATUM(int idDatum, string nombreCarta)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelDatum";

            if (idDatum > 0)
            {
                cnn.parametros.Add("@idDatum", idDatum);
            }

            if (nombreCarta != null)
            {
                cnn.parametros.Add("@carta", nombreCarta);
            }

            DataTable dt = cnn.Execute();
            return dt;
        }

        public List<ParametroGenerico> ListaDatum(int idDatum)
        {
            try
            {
                ParametroGenerico paramAux = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDatum";

                if(idDatum>0){
                    cnn.parametros.Add("@idDatum", idDatum);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        paramAux = new ParametroGenerico(Convert.ToInt32(row["idDatum"]),row["nombreDatum"].ToString());
                       
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
