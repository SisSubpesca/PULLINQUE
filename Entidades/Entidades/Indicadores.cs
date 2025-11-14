using System;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Datos.AccesoDatos;

namespace Datos.Entidades
{
    public class Indicadores
    {

        public DataTable Indicador_Valores_Obtener_RB(int indicador, string fecha_consulta, string fecha_desde, string fecha_hasta, String procedimiento)
        {

            DateTime fecha_desde_datetime = Convert.ToDateTime(fecha_desde + " 00:00:00");
            DateTime fecha_hasta_datetime = Convert.ToDateTime(fecha_hasta + " 23:59:59");

            Conexion cnn = new Conexion();
            cnn.procedimiento = procedimiento;
            cnn.parametros.Add("@fecha_desde", fecha_desde_datetime);
            cnn.parametros.Add("@fecha_hasta", fecha_hasta_datetime);
            if (indicador == 3)
            {
                cnn.parametros.Add("@fecha_consulta", fecha_consulta + " 23:59:59");
            };

            DataTable dt = cnn.Execute();
            return dt;
        }


        

       
        public DataTable VerIndicadores(int idIndicador)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbIndicadoresVer";
            cnn.parametros.Add("@id_indicador", idIndicador);

            DataTable dt = cnn.Execute();
            return dt;
        }

        

        public DataTable Indicadores_Solicitudes_Listar_RB(int indicador, string fecha_consulta, string fecha_desde, string fecha_hasta, string procedimiento)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = procedimiento;
            cnn.parametros.Add("@fecha_desde", fecha_desde + " 00:00:00");
            cnn.parametros.Add("@fecha_hasta", fecha_hasta + " 23:59:59");
            if (indicador == 3)
            {
                cnn.parametros.Add("@fecha_consulta", fecha_consulta + " 23:59:59");
            };

            DataTable dt = cnn.Execute();
            return dt;
        }




        public DataTable CalcularIndicador(String procedimiento, int idTipoTramite, int idSubTipo, string fechaNumerador_desde, string fechaNumerador_hasta, string fechaDenominador_desde, string fechaDenominador_hasta, string fechaConsulta, string regiones)
        {

            DateTime fechaNumerador_desde_datetime = new DateTime();
            DateTime fechaNumerador_hasta_datetime = new DateTime();

            DateTime fechaDenominador_desde_datetime = new DateTime();
            DateTime fechaDenominador_hasta_datetime = new DateTime();
            
            DateTime fechaConsulta_datetime = new DateTime();


            if (fechaNumerador_desde != null && !fechaNumerador_desde.Trim().Equals(""))
            {
                fechaNumerador_desde_datetime = Convert.ToDateTime(fechaNumerador_desde + " 00:00:00");
            }
            if (fechaNumerador_hasta != null && !fechaNumerador_hasta.Trim().Equals(""))
            {
                fechaNumerador_hasta_datetime = Convert.ToDateTime(fechaNumerador_hasta + " 23:59:59");
            }
            if (fechaDenominador_desde != null && !fechaDenominador_desde.Trim().Equals(""))
            {
                fechaDenominador_desde_datetime = Convert.ToDateTime(fechaDenominador_desde + " 00:00:00");
            }
            if (fechaDenominador_hasta != null && !fechaDenominador_hasta.Trim().Equals(""))
            {
                fechaDenominador_hasta_datetime = Convert.ToDateTime(fechaDenominador_hasta + " 23:59:59");
            }
            if (fechaConsulta != null && !fechaConsulta.Trim().Equals(""))
            {
                fechaConsulta_datetime = Convert.ToDateTime(fechaConsulta + " 00:00:00");
            }

            Conexion cnn = new Conexion();
            cnn.procedimiento = procedimiento;

            cnn.parametros.Add("@idTipoTramite", idTipoTramite);

            if (idSubTipo > 0) { 
                cnn.parametros.Add("@idSubTipo", idSubTipo);
            }

            if (fechaNumerador_desde_datetime != default(DateTime))
            {
                cnn.parametros.Add("@fechaNumerador_desde", fechaNumerador_desde_datetime);
            }
            if (fechaNumerador_hasta_datetime != default(DateTime))
            {
                cnn.parametros.Add("@fechaNumerador_hasta", fechaNumerador_hasta_datetime);
            }
            if (fechaDenominador_desde_datetime != default(DateTime))
            {
                cnn.parametros.Add("@fechaDenominador_desde", fechaDenominador_desde_datetime);
            }
            if (fechaDenominador_hasta_datetime != default(DateTime))
            {
                cnn.parametros.Add("@fechaDenominador_hasta", fechaDenominador_hasta_datetime);
            }
            if (fechaConsulta_datetime != default(DateTime))
            {
                cnn.parametros.Add("@fechaConsulta", fechaConsulta_datetime);
            }
            if (regiones != null && !regiones.Trim().Equals(""))
            {
                cnn.parametros.Add("@regiones", regiones);
            }

            DataTable dt = cnn.Execute();
            return dt;

        }




        public DataTable ListarDetalleIndicador(String procedimiento, int idTipoTramite, int idSubTipo, string fechaNumerador_desde, string fechaNumerador_hasta, string fechaDenominador_desde, string fechaDenominador_hasta, string fechaConsulta, string regiones)
        {

            DateTime fechaNumerador_desde_datetime = new DateTime();
            DateTime fechaNumerador_hasta_datetime = new DateTime();

            DateTime fechaDenominador_desde_datetime = new DateTime();
            DateTime fechaDenominador_hasta_datetime = new DateTime();

            DateTime fechaConsulta_datetime = new DateTime();


            if (fechaNumerador_desde != null && !fechaNumerador_desde.Trim().Equals(""))
            {
                fechaNumerador_desde_datetime = Convert.ToDateTime(fechaNumerador_desde + " 00:00:00");
            }
            if (fechaNumerador_hasta != null && !fechaNumerador_hasta.Trim().Equals(""))
            {
                fechaNumerador_hasta_datetime = Convert.ToDateTime(fechaNumerador_hasta + " 23:59:59");
            }
            if (fechaDenominador_desde != null && !fechaDenominador_desde.Trim().Equals(""))
            {
                fechaDenominador_desde_datetime = Convert.ToDateTime(fechaDenominador_desde + " 00:00:00");
            }
            if (fechaDenominador_hasta != null && !fechaDenominador_hasta.Trim().Equals(""))
            {
                fechaDenominador_hasta_datetime = Convert.ToDateTime(fechaDenominador_hasta + " 23:59:59");
            }
            if (fechaConsulta != null && !fechaConsulta.Trim().Equals(""))
            {
                fechaConsulta_datetime = Convert.ToDateTime(fechaConsulta + " 00:00:00");
            }

            Conexion cnn = new Conexion();
            cnn.procedimiento = procedimiento;

            cnn.parametros.Add("@idTipoTramite", idTipoTramite);

            if (idSubTipo > 0)
            {
                cnn.parametros.Add("@idSubTipo", idSubTipo);
            }

            if (fechaNumerador_desde_datetime != default(DateTime))
            {
                cnn.parametros.Add("@fechaNumerador_desde", fechaNumerador_desde_datetime);
            }
            if (fechaNumerador_hasta_datetime != default(DateTime))
            {
                cnn.parametros.Add("@fechaNumerador_hasta", fechaNumerador_hasta_datetime);
            }
            if (fechaDenominador_desde_datetime != default(DateTime))
            {
                cnn.parametros.Add("@fechaDenominador_desde", fechaDenominador_desde_datetime);
            }
            if (fechaDenominador_hasta_datetime != default(DateTime))
            {
                cnn.parametros.Add("@fechaDenominador_hasta", fechaDenominador_hasta_datetime);
            }
            if (fechaConsulta_datetime != default(DateTime))
            {
                cnn.parametros.Add("@fechaConsulta", fechaConsulta_datetime);
            }
            if (regiones != null && !regiones.Trim().Equals(""))
            {
                cnn.parametros.Add("@regiones", regiones);
            }

            DataTable dt = cnn.Execute();
            return dt;

        }

    }
}
