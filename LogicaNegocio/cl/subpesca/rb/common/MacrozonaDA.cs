using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Entidades;
using Datos.AccesoDatos;
using System.Data;

namespace LogicaNegocio.cl.subpesca.rb.common
{
    public class MacrozonaDA
    {
      
         
        /**
         * Método que obtiene las macrozonas.
         */
        public object obtenerMacrozona(int idMacrozona)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelMacrozona";

            if (idMacrozona > 0)
            {
                cnn.parametros.Add("@idMacrozona", idMacrozona);
            }
           
            DataTable dt = cnn.Execute();
            return dt;
        }
    }
}
