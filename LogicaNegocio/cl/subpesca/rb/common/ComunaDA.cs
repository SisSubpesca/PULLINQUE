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
    public class ComunaDA 
    {
        Logger logger = new Logger();
        
        public Comuna Obtener(int id_comuna)
        {
            DataTable dt = this.Ver(id_comuna);
            try
            {
                Comuna comuna           = new Comuna();
                comuna.id_comuna        = Convert.ToInt32(dt.Rows[0]["IdComuna"]);
                comuna.comuna           = Convert.ToString(dt.Rows[0]["Comuna"]);
                comuna.id_provincia     = Convert.ToInt32(dt.Rows[0]["IdProvincia"]);
                return comuna;
            }
            catch 
            {
                return null;
            };
        }

        // MÉTODOS (de BD)
        public DataTable Ver(int id_comuna)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbComuna_Mantenedor";
            cnn.parametros.Add("@idcomuna", id_comuna);
            DataTable dt = cnn.Execute();

            return dt;
        }

      
       
    }
}
