using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using Datos.AccesoDatos;
using System.Data;
using Datos.Entidades.Relocalizacion;

namespace LogicaNegocio.cl.subpesca.rb.relocalizacion
{
    public class ErroresRelocalizacionDA
    {
        Logger logger = new Logger();

        public bool GuardarErroresRelocalizacion(ErroresRelocalizacion errorRel)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbErroresRelocalizacion";
                cnn.parametros.Add("@idError", errorRel.idError);
                if(errorRel.tramiteRel!=null && errorRel.tramiteRel.idTramiteRel>0){
                    cnn.parametros.Add("@idTramiteRel", errorRel.tramiteRel.idTramiteRel);
                }
                if (errorRel.detSector!=null && errorRel.detSector.idDetalleSector > 0)
                {
                    cnn.parametros.Add("@idDetalleSector", errorRel.detSector.idDetalleSector);
                }
                
                cnn.parametros.Add("@idTipoError", errorRel.tipoError.id);
                cnn.parametros.Add("@idEstadoError", errorRel.estadoError.id);
                cnn.parametros.Add("@invalidante", errorRel.invalidante);
                if (errorRel.centro != null && errorRel.centro.id > 0)
                {
                    cnn.parametros.Add("@codigoSiep", errorRel.centro.id);
                }
                if (errorRel.origenSec != null && errorRel.origenSec.idOrigenSector > 0)
                {
                    cnn.parametros.Add("@idOrigenSector", errorRel.origenSec.idOrigenSector);
                }
                

                DataTable dt = cnn.Execute();
                errorRel.idError = Convert.ToInt32(dt.Rows[0]["idError"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }


        public List<ErroresRelocalizacion> ListarErroresRelocalizacion(int idTramite, bool invalidante)
        {
            try
            {
                ErroresRelocalizacion erroresRel = null;
                List<ErroresRelocalizacion> resp = new List<ErroresRelocalizacion>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbErroresRelocalizacion";

                cnn.parametros.Add("@idTramiteRel", idTramite);
                cnn.parametros.Add("@invalidante", invalidante);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        erroresRel = new ErroresRelocalizacion();
                        erroresRel.idError = Convert.ToInt32(row["idError"]);
                        erroresRel.tramiteRel = new TramiteRelocalizacion();
                        erroresRel.tramiteRel.idTramiteRel = Convert.ToInt32(row["idTramiteRel"]);
                        erroresRel.detSector = new DetalleSector();

                        if (!row.IsNull("idDetalleSector"))
                        {
                            erroresRel.detSector.idDetalleSector = Convert.ToInt32(row["idDetalleSector"]);
                        }

                        if (!row.IsNull("numSector"))
                        {
                            erroresRel.detSector.numSector = Convert.ToInt32(row["numSector"]);
                        }
                        
                        if (!row.IsNull("idTipoError"))
                        {
                            erroresRel.tipoError = new ParametroGenerico(Convert.ToInt32(row["idTipoError"]), row["nombreTipoError"].ToString());
                        }
                        erroresRel.estadoError = new ParametroGenerico(Convert.ToInt32(row["idEstadoError"]), row["nombreEstado"].ToString());
                        erroresRel.fechaIngreso = Convert.ToDateTime(row["fechaIngreso"]);
                        erroresRel.fechaUltimaMod = Convert.ToDateTime(row["fechaUltModificacion"]);
                        erroresRel.invalidante = Convert.ToBoolean(row["invalidante"]);
                        if (!row.IsNull("codigoSiep"))
                        {
                            erroresRel.centro = new ParametroGenerico(Convert.ToInt32(row["codigoSiep"]),"");
                        }
                        if (!row.IsNull("idOrigenSector"))
                        {
                            erroresRel.origenSec = new OrigenSector();
                            erroresRel.origenSec.idOrigenSector = Convert.ToInt32(row["idOrigenSector"]);
                        }

                        resp.Add(erroresRel);

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


        public bool EliminarErroresRelocalizacion(int idTramiteRel)
        {
            try
            {
                Conexion cnn = new Conexion();
                int result = 0;
                cnn.procedimiento = "paDelRbErroresRelocalizacion";
                cnn.parametros.Add("@idTramiteRel", idTramiteRel);
               
                DataTable dt = cnn.Execute();
                result = Convert.ToInt32(dt.Rows[0]["resultado"]);
                if (result >= 0 || result == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }
        
        public bool EliminarAlertasRelocalizacion(int idTramiteRel)
        {
            try
            {
                Conexion cnn = new Conexion();
                int result = 0;
                cnn.procedimiento = "paDelRbAlertasRelocalizacion";
                cnn.parametros.Add("@idTramiteRel", idTramiteRel);

                DataTable dt = cnn.Execute();
                result = Convert.ToInt32(dt.Rows[0]["resultado"]);
                if (result >= 0 || result == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }


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
