using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using Datos.AccesoDatos;
using System.Data;

namespace LogicaNegocio.cl.subpesca.rb.modificacion
{
    public class AlertaTramiteModConcesionDA
    {
        Logger logger = new Logger();

        public bool GuardarAlertaTramiteModConcesion(AlertaTramiteModConcesion alertaTramiteMod)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbAlertaTramiteModConcesion";
                cnn.parametros.Add("@idAlertaTramRel", alertaTramiteMod.idAlertaTramRel);
                cnn.parametros.Add("@idSolConcesion", alertaTramiteMod.idSolConcesion);
                cnn.parametros.Add("@idTipoWarning", alertaTramiteMod.tipoWarning.id);

                if (alertaTramiteMod.descripcion!=null)
                {
                    cnn.parametros.Add("@descripcion", alertaTramiteMod.descripcion);
                }

                DataTable dt = cnn.Execute();
                alertaTramiteMod.idAlertaTramRel = Convert.ToInt32(dt.Rows[0]["idAlertaTramRel"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public List<AlertaTramiteModConcesion> ListarAlertaTramiteModConcesion(int idSolConcesion, int idAlertaTramRel, int idTipoWarning)
        {
            try
            {
                AlertaTramiteModConcesion alertaModConcesion = null;
                List<AlertaTramiteModConcesion> resp = new List<AlertaTramiteModConcesion>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbAlertaTramiteModConcesion";
                if (idSolConcesion>0)
                {
                    cnn.parametros.Add("@idSolConcesion", idSolConcesion); 
                }
                if(idAlertaTramRel>0){
                    cnn.parametros.Add("@idAlertaTramRel", idAlertaTramRel);
                }
                if(idTipoWarning>0){
                    cnn.parametros.Add("@idTipoWarning", idTipoWarning); 
                }
               
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        alertaModConcesion = new AlertaTramiteModConcesion();
                        alertaModConcesion.idAlertaTramRel = Convert.ToInt32(row["idAlertaTramRel"]);
                        alertaModConcesion.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                        alertaModConcesion.tipoWarning = new ParametroGenerico(Convert.ToInt32(row["idTipoWarning"]), row["nombreTipo"].ToString());
                        
                        if (!row.IsNull("descripcion"))
                        {
                            alertaModConcesion.descripcion = row["descripcion"].ToString();
                        }
                        if (!row.IsNull("fechaIngreso"))
                        {
                            alertaModConcesion.fechaIngreso = Convert.ToDateTime(row["fechaIngreso"]);
                        }
                        if (!row.IsNull("fechaUltModificacion"))
                        {
                            alertaModConcesion.fechaUltModificacion = Convert.ToDateTime(row["fechaUltModificacion"]);
                        }
                        
                        resp.Add(alertaModConcesion);

                    }
                }
                return resp;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public bool EliminarAlertaTramiteModConcesion(int idAlertaTramRel, int idSolConcesion)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbAlertaTramiteModConcesion";

                if (idAlertaTramRel > 0)
                {
                    cnn.parametros.Add("@idAlertaTramRel", idAlertaTramRel);
                }

                cnn.parametros.Add("@idSolConcesion", idSolConcesion);

                DataTable dt = cnn.Execute();

                return true;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }



    }
}
