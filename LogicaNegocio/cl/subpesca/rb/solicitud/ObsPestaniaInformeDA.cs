using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Datos.Entidades;
using Datos.AccesoDatos;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Data.SqlClient;


namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class ObsPestaniaInformeDA
    {
        Logger logger = new Logger();

        //GUARDA UNA OBSERVACION ASOCIADO A UNA SOLICITUD
        public bool GuardarObsPestaniaInforme(ObsPestaniaInforme obsPestaniaInf)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbObsPestaniaInforme";
                cnn.parametros.Add("@idObsPestInf", obsPestaniaInf.idObsPestInf);
                cnn.parametros.Add("@idSolConcesion", obsPestaniaInf.idSolConcesion);
                cnn.parametros.Add("@idTipoGrupo", obsPestaniaInf.tipoGrupo.id);
                cnn.parametros.Add("@observaciones", obsPestaniaInf.observaciones);
                
                DataTable dt = cnn.Execute();
                obsPestaniaInf.idObsPestInf = Convert.ToInt32(dt.Rows[0]["idObsPestInf"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        /**
     * Obtiene una observacion asociada a una solicitud de un grupo de observacion en particular.
     */
        public ObsPestaniaInforme ObtieneObsPestaniaInforme(int idSolConcesion, int idTipoGrupo)
        {
            try
            {
                ObsPestaniaInforme observacionResp = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbObsPestaniaInforme";
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                cnn.parametros.Add("@idTipoGrupo", idTipoGrupo);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                       observacionResp = new ObsPestaniaInforme();
                       observacionResp.idObsPestInf = Convert.ToInt32(row["idObsPestInf"]);
                       observacionResp.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                       observacionResp.tipoGrupo = new ParametroGenerico(Convert.ToInt32(row["idTipoGrupo"]), "");
                       observacionResp.observaciones = row["observaciones"].ToString();
                    }
                }

                return observacionResp;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }




    }
}
