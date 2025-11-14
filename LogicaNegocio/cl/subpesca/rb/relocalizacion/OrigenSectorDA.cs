using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades.Relocalizacion;
using Datos.AccesoDatos;
using System.Data;

namespace LogicaNegocio.cl.subpesca.rb.relocalizacion
{
    public class OrigenSectorDA
    {

        public Logger Log { get; set; }

        public OrigenSectorDA()
        {
            this.Log = new Logger();

        }

        public bool GuardarOrigenSector(OrigenSector origenSector)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbOrigenSector";
                cnn.parametros.Add("@idOrigenSector", origenSector.idOrigenSector);
                cnn.parametros.Add("@idDetalleSector", origenSector.idDetalleSector);
                cnn.parametros.Add("@codigoSiep", origenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro);
                cnn.parametros.Add("@superficieRelocalizacion", origenSector.superficieRelocalizada);

                DataTable dt = cnn.Execute();
                origenSector.idOrigenSector = Convert.ToInt32(dt.Rows[0]["idOrigenSector"]);

                return true;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public bool GuardarDetallePreferenciaRel(int idOrigenSector, int idPreferenciaRel)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbDetallePreferenciaRel";
                cnn.parametros.Add("@idOrigenSector", idOrigenSector);
                cnn.parametros.Add("@idPreferenciaRel", idPreferenciaRel);

                DataTable dt = cnn.Execute();
                return true;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public bool EliminarDetallePreferenciaRel(int idOrigenSector)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbDetallePreferenciaRel";
                cnn.parametros.Add("@idOrigenSector", idOrigenSector);
                
                DataTable dt = cnn.Execute();

                return true;

            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public bool EliminarOrigenSector(int idOrigenSector)
        {
            try
            {
                Conexion cnn = new Conexion();
                int result = 0;
                cnn.procedimiento = "paDelRbOrigenSector";
                cnn.parametros.Add("@idOrigenSector", idOrigenSector);
                
                DataTable dt = cnn.Execute();
                result = Convert.ToInt32(dt.Rows[0]["resultado"]);
                if (result == 0 || result == -1)
                {
                    return true;
                }
                return false;


            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

    }
}
