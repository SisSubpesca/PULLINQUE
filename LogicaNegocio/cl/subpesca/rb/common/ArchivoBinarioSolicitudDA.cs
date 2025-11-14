using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.AccesoDatos;
using System.Data;
using Datos.Entidades;
using System.Data.SqlClient;

namespace LogicaNegocio.cl.subpesca.rb.common
{
    public class ArchivoBinarioSolicitudDA
    {

        Logger logger = new Logger();

        /**
         * Guarda un archivo para una solicitud de concesión
         *
         **/
        public bool GuardarArchivoBinarioSolicitud(ArchivoBinario archivoBinario)
        {

            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbArchivoBinSolicitudConcesion";

                cnn.parametros.Add("@idArchivoBinSC", archivoBinario.idArchivo);
                cnn.parametros.Add("@nombreFisico",archivoBinario.nombreFisico );
                cnn.parametros.Add("@nombreArchivo", archivoBinario.nombreArchivo);
                cnn.parametros.Add("@fechaDeCarga", DateTime.Now);
                cnn.parametros.Add("@tamano", archivoBinario.tamano);
                cnn.parametros.Add("@formato", archivoBinario.formato);


                byte[] file = new byte[archivoBinario.archivo.InputStream.Length];
                archivoBinario.archivo.InputStream.Read(file, 0, file.Length);

                cnn.parametros.Add("@contenido", file);
                if (archivoBinario.observaciones != null)
                {
                    cnn.parametros.Add("@observaciones", archivoBinario.observaciones);
                }

                DataTable dt = cnn.Execute();
                archivoBinario.idArchivo = Convert.ToInt32(dt.Rows[0]["idArchivoBinSC"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }


        public bool GuardarArchivoBinarioSolicitudEspecial(ArchivoBinarioEspecial archivoBinario)
        {

            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbArchivoBinSolicitudConcesion";

                cnn.parametros.Add("@idArchivoBinSC", archivoBinario.idArchivo);
                cnn.parametros.Add("@nombreFisico", archivoBinario.nombreFisico);
                cnn.parametros.Add("@nombreArchivo", archivoBinario.nombreArchivo);
                cnn.parametros.Add("@fechaDeCarga", DateTime.Now);
                cnn.parametros.Add("@tamano", archivoBinario.tamano);
                cnn.parametros.Add("@formato", archivoBinario.formato);


                /*byte[] file = new byte[archivoBinario.archivo.InputStream.Length];
                archivoBinario.archivo.InputStream.Read(file, 0, file.Length);*/

                cnn.parametros.Add("@contenido", archivoBinario.bytes);
                if (archivoBinario.observaciones != null)
                {
                    cnn.parametros.Add("@observaciones", archivoBinario.observaciones);
                }

                DataTable dt = cnn.Execute();
                archivoBinario.idArchivo = Convert.ToInt32(dt.Rows[0]["idArchivoBinSC"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }


        public ArchivoBinario ObtenerArchivoBinarioSolicitud(int idArchivoBinSC)
        {

            try
            {

                ArchivoBinario archivoBinario = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbArchivoBinSC_Contenido";
                cnn.parametros.Add("@idArchivoBinSC", idArchivoBinSC);

                DataTable dt = cnn.Execute();


                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        archivoBinario = new ArchivoBinario();
                        archivoBinario.idArchivo = idArchivoBinSC;
                        archivoBinario.nombreArchivo = Convert.ToString(row["nombreArchivo"]);
                        archivoBinario.formato = Convert.ToString(row["formato"]);
                        archivoBinario.bytes = (byte[])row["contenido"];

                    }
                }


                return archivoBinario;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };
        }

    }
}
