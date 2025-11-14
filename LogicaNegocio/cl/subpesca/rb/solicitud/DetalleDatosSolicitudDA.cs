using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Entidades;
using Datos.AccesoDatos;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.errores;


namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class DetalleDatosSolicitudDA
    {

        Logger logger = new Logger();

        //LA TABLA DE ESTE DA HA SIDO ELIMINADA, SIN EMBARGO PARA NO AFECTAR LA ESTRUCTURA DE LA DATA EN LA INTERFAZ
        //SE MANTIENEN LOS PROCEDIMIENTOS PERO APUNTANDO HACIA LA TABLA RBDATOS SOLICITUD UE

        public bool GuardarDetalleDatosSolicitud(DetalleDatosSolicitud detSolicitud, int idUsuario)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbDatosSolicitudUE";
                cnn.parametros.Add("@idDatosSolicitud", detSolicitud.idDetDato);
                cnn.parametros.Add("@idSolConcesion", detSolicitud.idSolConcesion);
                if (detSolicitud.tipoCentro!=null && detSolicitud.tipoCentro.id > 0)
                {
                    cnn.parametros.Add("@idTipoCentro", detSolicitud.tipoCentro.id); 
                }
                 if (detSolicitud.tipoEvaluacion!=null && detSolicitud.tipoEvaluacion.id > 0)
                {
                    cnn.parametros.Add("@idTipoEvaluacion", detSolicitud.tipoEvaluacion.id);
                }
                 if (detSolicitud.superficieSectorAmerb > 0)
                {
                    cnn.parametros.Add("@superficieSectorAmerb", detSolicitud.superficieSectorAmerb);
                }
                 if (detSolicitud.porcentSectorAmerb > 0 )
                {
                    cnn.parametros.Add("@porcentSectorAmerb", detSolicitud.porcentSectorAmerb); 
                }
                 if (detSolicitud.profundidadMin > 0)
                {
                   cnn.parametros.Add("@profundidadMin", detSolicitud.profundidadMin);
                }
                 if (detSolicitud.periodoOperacionCol!=null)
                {
                    cnn.parametros.Add("@periodoOperacionCol", detSolicitud.periodoOperacionCol);
                }
                 if (detSolicitud.observaciones!=null)
                {
                     cnn.parametros.Add("@observaciones", detSolicitud.observaciones);
                }
                 if (detSolicitud.vigenciaColector != null && !detSolicitud.vigenciaColector.Equals(default(DateTime)))
                {
                    cnn.parametros.Add("@vigenciaColector", detSolicitud.vigenciaColector);
                }
                if(detSolicitud.numIdentSolicitud>0){
                     cnn.parametros.Add("@numIdentSolicitud", detSolicitud.numIdentSolicitud);
                }
                if (detSolicitud.numeroCI>0)
                {
                     cnn.parametros.Add("@numeroCI", detSolicitud.numeroCI);
                }
                if (detSolicitud.fechaCI != null && detSolicitud.fechaCI != default(DateTime))
                {
                     cnn.parametros.Add("@fechaCI", detSolicitud.fechaCI);
                }
                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }
                DataTable dt = cnn.Execute();
                detSolicitud.idDetDato = Convert.ToInt32(dt.Rows[0]["idDatosSolicitud"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public DetalleDatosSolicitud ObtieneDetalleDatosSolicitud(int idSolicitud)
        {
            try
            {
                DetalleDatosSolicitud detDatosSolicitud = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDatosSolicitudUE";
                cnn.parametros.Add("@idSolConcesion", idSolicitud);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                       detDatosSolicitud = new DetalleDatosSolicitud();
                       detDatosSolicitud.idDetDato = Convert.ToInt32(row["idDatosSolicitud"]);
                       detDatosSolicitud.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                       if (!row.IsNull("idTipoCentro")){
                           detDatosSolicitud.tipoCentro = new ParametroGenerico(Convert.ToInt32(row["idTipoCentro"]), row["tipoCentro"].ToString());
                       }
                       if (!row.IsNull("idTipoEvaluacion"))
                       {
                           detDatosSolicitud.tipoCentro = new ParametroGenerico(Convert.ToInt32(row["idTipoEvaluacion"]), row["tipoEvaluacion"].ToString());
                       }
                       if (!row.IsNull("superficieSectorAmerb"))
                       {
                           detDatosSolicitud.superficieSectorAmerb = Convert.ToSingle(row["superficieSectorAmerb"]);
                       }
                       if (!row.IsNull("porcentSectorAmerb"))
                       {
                           detDatosSolicitud.porcentSectorAmerb = Convert.ToSingle(row["porcentSectorAmerb"]);
                       }
                       if (!row.IsNull("profundidadMin"))
                       {
                           detDatosSolicitud.profundidadMin = Convert.ToSingle(row["profundidadMin"]); 
                       }
                       if (!row.IsNull("periodoOperacionCol"))
                       {
                           detDatosSolicitud.periodoOperacionCol = row["periodoOperacionCol"].ToString(); 
                       }
                       if (!row.IsNull("observaciones"))
                       {
                           detDatosSolicitud.observaciones = row["observaciones"].ToString(); 
                       }
                       if (!row.IsNull("vigenciaColector"))
                       {
                           detDatosSolicitud.vigenciaColector = Convert.ToDateTime("vigenciaColector");
                       }
                      
                       
                    }
                }

                return detDatosSolicitud;
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
