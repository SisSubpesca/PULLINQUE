using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Data;
using Datos.AccesoDatos;

namespace LogicaNegocio.cl.subpesca.rb.antecedentesSector
{
    public class TipoConcesionDA
    {
        public Logger Log { get; set; }

        public TipoConcesionDA()
        {
            this.Log = new Logger();
        }

        /**
         * Obtiene listado de Capitanía de Puerto (campo Capitanía de Puerto)
         **/
        public DataTable obtenerTipoConcesion(int idTipoConcesion)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelTipoConcesion";

            if (idTipoConcesion > 0)
            {
                cnn.parametros.Add("@idTipoConcesion", idTipoConcesion);
            }

            DataTable dt = cnn.Execute();
            return dt;
        }
    }
}
