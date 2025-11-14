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
    public class ProvinciaDA
    {

        public Logger Log { get; set; }

        public Provincia Obtener(int id_prov)
        {
            DataTable dt = this.Ver(id_prov);
            try
            {
                Provincia provincia = new Provincia();
                provincia.id_provincia = Convert.ToInt32(dt.Rows[0]["IdProvincia"]);
                provincia.provincia = Convert.ToString(dt.Rows[0]["Provincia"]);
                provincia.id_region = Convert.ToInt32(dt.Rows[0]["IdRegion"]);
                return provincia;
            }
            catch 
            {
                return null;
            };
        }

        // MÉTODOS (de BD)
        public DataTable Ver(int id_provincia)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbProvincia_Mantenedor";
            cnn.parametros.Add("@idProvincia", id_provincia);

            DataTable dt = cnn.Execute();
            return dt;
        }



      
    }
}
