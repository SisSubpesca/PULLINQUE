using System;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Datos.AccesoDatos;
using Datos.Utilidades;



namespace Datos.Entidades
{
    public class Reportes
    {



        public DataTable Reportes_Generar_RB(int id_tiporeporte, string KeySort, int id_region, int id_provincia, int id_comuna, int id_macrozona, int id_barrio,
                                        int id_etapacultivo, int id_centrocultivo, string pert, int id_vigencia, string especies, int anio1, int mes1, int anio2,
                                        int mes2, int id_usuario, int conSinBarrio, String procedimiento, int @idTipoUnidEspacial, int idTipoTramite, int id_tipoRelocalizacion, int id_tipoModificacion)
        {
            Funciones fnc = new Funciones();
            Conexion cnn = new Conexion();
            string id_reporte = id_tiporeporte.ToString();
            if (id_tiporeporte < 10)
            {
                id_reporte = "0" + id_reporte;
            };
            cnn.procedimiento = procedimiento;
            cnn.parametros.Add("@id_usuario", id_usuario);
            if (fnc.Pertenece(id_tiporeporte, new int[] { 1, 2, 3, 4, 5, 6, 8, 9, 10 }))
            {
                if (id_region > 0)
                {
                    cnn.parametros.Add("@id_region", id_region);
                };
            };

            if (fnc.Pertenece(id_tiporeporte, new int[] { 1, 2, 6, 8, 10 }))
            {
                if (id_provincia > 0)
                {
                    cnn.parametros.Add("@id_provincia", id_provincia);
                };
                if (id_comuna > 0)
                {
                    cnn.parametros.Add("@id_comuna", id_comuna);
                };
            };

            if (fnc.Pertenece(id_tiporeporte, new int[] { 1, 2, 3, 4, 5, 6, 9 }))
            {
                if (id_macrozona > 0)
                {
                    cnn.parametros.Add("@id_macrozona", id_macrozona);
                };
                if (id_barrio > 0)
                {
                    cnn.parametros.Add("@id_barrio", id_barrio);
                };

                if (conSinBarrio > 0)
                {
                    cnn.parametros.Add("@conSinBarrio", conSinBarrio);
                };
            };

            if (fnc.Pertenece(id_tiporeporte, new int[] { 1, 2, 3, 4, 5, 9 }))
            {
                if (id_etapacultivo >= 0)
                {
                    cnn.parametros.Add("@id_etapacultivo", id_etapacultivo);
                };
            };

            if (fnc.Pertenece(id_tiporeporte, new int[] { 1, 3, 4, 5, 9 }))
            {
                cnn.parametros.Add("@mes1", mes1);
            };

            if (fnc.Pertenece(id_tiporeporte, new int[] { 1, 2, 3, 4, 5, 9 }))
            {
                cnn.parametros.Add("@anio1", anio1);
            };

            if (fnc.Pertenece(id_tiporeporte, new int[] { 3, 4, 5, 9 }))
            {
                cnn.parametros.Add("@mes2", mes2);
            };

            if (fnc.Pertenece(id_tiporeporte, new int[] { 2, 3, 4, 5, 9 }))
            {
                cnn.parametros.Add("@anio2", anio2);
            };

            if (fnc.Pertenece(id_tiporeporte, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }))
            {
                if (id_centrocultivo >= 0)
                {
                    cnn.parametros.Add("@id_centrocultivo", id_centrocultivo);
                };
            };

            if (fnc.Pertenece(id_tiporeporte, new int[] { 8 }))
            {
                if (id_vigencia >= 0)
                {
                    cnn.parametros.Add("@id_vigencia", id_vigencia);
                };
            };

            if (fnc.Pertenece(id_tiporeporte, new int[] { 1, 2, 3, 4, 5, 6, 9 }))
            {
                if (especies != null && !especies.Trim().Equals(""))
                {
                    cnn.parametros.Add("@especies", especies);
                }
            };

            if (fnc.Pertenece(id_tiporeporte, new int[] { 10 }))
            {
                if (!pert.Trim().Equals(""))
                {
                    cnn.parametros.Add("@pert", pert);
                };

                if (id_tipoRelocalizacion > 0)
                {
                    cnn.parametros.Add("@id_tipoRelocalizacion", id_tipoRelocalizacion);
                };

                if (id_tipoModificacion > 0)
                {
                    cnn.parametros.Add("@id_tipoModificacion", id_tipoModificacion);
                };

                
            };

            cnn.parametros.Add("@idTipoUnidEspacial", idTipoUnidEspacial);
            cnn.parametros.Add("@idTipoTramite", idTipoTramite);

            DataTable dt = cnn.Execute();
            return dt;
        }



        public DataTable Reportes_Resumen_GenerarRB(int id_tiporeporte, int id_centrocultivo, int id_usuario, int idTipoUnidEspacial, int idTipoTramite)
        {
            Funciones fnc = new Funciones();
            Conexion cnn = new Conexion();
            string id_reporte = id_tiporeporte.ToString();
            if (id_tiporeporte < 10)
            {
                id_reporte = "0" + id_reporte;
            };
            cnn.procedimiento = "paSelRbReporte07Generar";
            cnn.parametros.Add("@id_usuario", id_usuario);
            cnn.parametros.Add("@id_centrocultivo", id_centrocultivo);
            cnn.parametros.Add("@idTipoUnidEspacial", idTipoUnidEspacial);
            cnn.parametros.Add("@idTipoTramite", idTipoTramite);
            

            DataTable dt = cnn.Execute();
            return dt;
        }
    }
}
