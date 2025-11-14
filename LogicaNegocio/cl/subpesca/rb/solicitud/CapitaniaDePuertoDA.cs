using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.AccesoDatos;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Data;

namespace LogicaNegocio.cl.subpesca.rb.unidadEspacial
{
    public class CapitaniaDePuertoDA
    {
        public Logger Log { get; set; }

        public CapitaniaDePuertoDA()
        {
            this.Log = new Logger();
        }

        /**
         * Obtiene listado de Capitanía de Puerto (campo Capitanía de Puerto)
         **/
        public DataTable obtenerCapitaniaDePuerto(int idCapitaniaPuerto)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRBCapitaniaPuerto";

            if (idCapitaniaPuerto > 0)
            {
                cnn.parametros.Add("@idCapitaniaPuerto", idCapitaniaPuerto);
            }

            DataTable dt = cnn.Execute();
            return dt;
        }
    }
}
