using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.AccesoDatos;
using System.Data;

namespace LogicaNegocio.cl.subpesca.rb.common
{
    public class OficinaDA
    {
        Logger logger = new Logger();

        public DataTable obtenerOficina(String codDirZonal)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbDireccionZonal";

            if (codDirZonal != null && !codDirZonal.Trim().Equals("")) 
            {
                cnn.parametros.Add("@codDirZonal", codDirZonal);
            }
            
            DataTable dt = cnn.Execute();
            return dt;
        }
    }
}
