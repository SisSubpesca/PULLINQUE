using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.AccesoDatos;
using Datos.Entidades;
using System.Data;

namespace LogicaNegocio.cl.subpesca.rb.common
{
    public class CentroCultivoDA
    {
        
        public DataTable CentrosDeCultivo_Detalle_Listar(int id_centrocultivo, int mes, int anio)
        {
            
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbReporteUECentroCultivoDetalle";
            cnn.parametros.Add("@id_centrocultivo", id_centrocultivo);
            cnn.parametros.Add("@mes", mes);
            cnn.parametros.Add("@anio", anio);

            DataTable dt = cnn.Execute();
            return dt;
             
        }
        

    }
}
