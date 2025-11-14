using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Datos.AccesoDatos;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.errores;



namespace LogicaNegocio.cl.subpesca.rb.common
{
    public class RegionDA
    {

        public Logger Log { get; set; }

        public RegionDA() {
            this.Log = new Logger();
        } 



        /**
         * Obtiene la Región (campo Region)
         **/
        public DataTable obtenerRegion(int idRegion)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRegion";
            if (idRegion > 0)
            {
                cnn.parametros.Add("@idRegion", idRegion);
            }
            
            DataTable dt = cnn.Execute();
            return dt;
        }

        public DataTable ListarRegion(int idRegion)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRegion";
            if (idRegion>0)
            {
                cnn.parametros.Add("@idRegion", idRegion);
            }
            
            DataTable dt = cnn.Execute();
            return dt;
        }


    }
}
