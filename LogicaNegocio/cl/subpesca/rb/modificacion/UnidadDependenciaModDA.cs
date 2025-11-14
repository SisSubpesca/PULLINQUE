using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.AccesoDatos;
using System.Data;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.errores;



namespace LogicaNegocio.cl.subpesca.rb.modificacion
{
    public class UnidadDependenciaModDA
    {
        Logger logger = new Logger();

        /*  
     //SE ELIMINÓ LA TBLA, POR LO TANTO SE ELIMINA PROCEDIMIENTO
           public bool GuardarUnidadDependenciaMod(UnidadDependenciaMod unidadDependenciaMod)
           {
               try{
                     Conexion cnn = new Conexion();
                     cnn.procedimiento = "paInsRbUnidadDependenciaMod";
                     cnn.parametros.Add("@idSolConcesion", unidadDependenciaMod.idSolConcesion);
                     cnn.parametros.Add("@idTipoUnidDependencia", unidadDependenciaMod.tipoUnidDependencia.id);
                     cnn.parametros.Add("@claveUnidDependencia", unidadDependenciaMod.claveUnidDependencia);
                     cnn.parametros.Add("@idEstadoUnidDependencia", unidadDependenciaMod.estadoSolicitudDep.id);
                 
                     DataTable dt = cnn.Execute();
                     unidadDependenciaMod.idUnidadDependencia = Convert.ToInt32(dt.Rows[0]["idUnidadDependenciaMod"]);

                     return true;

                 }catch (Exception ex){
                     logger.PrintError(ex);
                     logger.SendMailError(ex);
                     return false;
                 };
          }
        */
        /*  
 //SE ELIMINÓ LA TBLA, POR LO TANTO SE ELIMINA PROCEDIMIENTO
           public List<UnidadDependenciaMod> ListarUnidadDependenciaMod(int idSolicitud)
          {
              try
                {
                    UnidadDependenciaMod unidadResp = null;
                    List<UnidadDependenciaMod> resp = new List<UnidadDependenciaMod>();

                    Conexion cnn = new Conexion();
                    cnn.procedimiento = "paSelRbUnidadDependenciaMod";
                    cnn.parametros.Add("@idSolConcesion", idSolicitud);
                
                    DataTable dt = cnn.Execute();

                    if (dt != null)
                    {

                        foreach (DataRow row in dt.Rows)
                        {

                            unidadResp = new UnidadDependenciaMod();
                            unidadResp.idUnidadDependencia = Convert.ToInt32(row["idUnidadDependencia"]);
                            unidadResp.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            unidadResp.aplicaDependencia = new ParametroGenerico(Convert.ToInt32(row["idAplicaDependencia"]), row["tipoAplicaDep"].ToString());
                            unidadResp.tipoUnidDependencia = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidDependencia"]), row["nombreTipoUnidDependencia"].ToString());
                            unidadResp.claveUnidDependencia = row["claveUnidDependencia"].ToString();
                            unidadResp.estadoSolicitud = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                            if (!row.IsNull("idEstadoSolicitudIns"))
                            {
                                unidadResp.estadoSolicitudDep = new ParametroGenerico(Convert.ToInt32(row["idEstadoSolicitudIns"]), row["nombreEstadoDep"].ToString());
                            }

                            resp.Add(unidadResp);
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
        */
        /*  
 //SE ELIMINÓ LA TBLA, POR LO TANTO SE ELIMINA PROCEDIMIENTO
           public int ObtenerSolicitudUnidadDepExistente(int idTipoUnidDependencia, string claveUnidDependencia)
           {
               try
               {
                   int idSolicitud=0;
               
                   Conexion cnn = new Conexion();
                   cnn.procedimiento = "paSelRbUnidadDepExistente";
                   cnn.parametros.Add("@idTipoUnidDependencia", idTipoUnidDependencia);
                   cnn.parametros.Add("@claveUnidDependencia", claveUnidDependencia);

                   DataTable dt = cnn.Execute();

                   if (dt != null)
                   {

                       foreach (DataRow row in dt.Rows)
                       {
                         idSolicitud = Convert.ToInt32(row["idSolConcesion"]);
                       
                       }
                   }

                   return idSolicitud;
               }
               catch (Exception ex)
               {
                   logger.PrintError(ex);
                   logger.SendMailError(ex);
                   return 0;
               }
           }*/
         /*  
          * //SE ELIMINÓ LA TBLA, POR LO TANTO SE ELIMINA PROCEDIMIENTO
           public bool EliminarUnidadDependenciaMod(int idSolicitud)
           {
               try
               {
                   Conexion cnn = new Conexion();
                   cnn.procedimiento = "paDelRbUnidadDependenciaMod";
                   cnn.parametros.Add("@idSolConcesion", idSolicitud);
               
                   DataTable dt = cnn.Execute();

                   return true;

               }
               catch (Exception ex)
               {
                   logger.PrintError(ex);
                   logger.SendMailError(ex);
                   return false;
               };
           }*/

        /*  
 //SE ELIMINÓ LA TBLA, POR LO TANTO SE ELIMINA PROCEDIMIENTO
       public bool EliminarUnidadDependenciaModFiltro(int idUnidadDependencia)
       {
           try
           {
               Conexion cnn = new Conexion();
               cnn.procedimiento = "paDelRbUnidadDependenciaModFiltro";
               cnn.parametros.Add("@idUnidadDependencia", idUnidadDependencia);

               DataTable dt = cnn.Execute();

               return true;

           }
           catch (Exception ex)
           {
               logger.PrintError(ex);
               logger.SendMailError(ex);
               return false;
           };
       }*/

        public bool ObtieneValidarUnidadDependencia(int idTipoUnid_Dependencia, string clave)
        {
            try
            {
                int respClave = 0;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbValidarUnidadDependencia";
                cnn.parametros.Add("@idTipoUnid_Dependencia", idTipoUnid_Dependencia);
                cnn.parametros.Add("@clave", clave);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        respClave = Convert.ToInt32(row["respClave"]);
                        if(respClave>0){
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }

/*  
 //SE ELIMINÓ LA TBLA, POR LO TANTO SE ELIMINA PROCEDIMIENTO
        public List<UnidadDependenciaMod> ListarUnidadDependencia_SolEstados()
        {
            try
            {
                UnidadDependenciaMod unidadResp = null;
                List<UnidadDependenciaMod> resp = new List<UnidadDependenciaMod>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbUnidDependencia_SolEstados";
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        unidadResp = new UnidadDependenciaMod();
                        unidadResp.idUnidadDependencia = Convert.ToInt32(row["idUnidadDependencia"]);
                        unidadResp.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                        unidadResp.solicitudPadre = new SolicitudConcesion();
                        unidadResp.solicitudPadre.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                        unidadResp.solicitudPadre.numPert = row["numPert"].ToString();
                        unidadResp.solicitudPadre.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacial"]), row["nombreTipoUE"].ToString());
                        unidadResp.solicitudPadre.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTipoTramite"].ToString());
                        unidadResp.tipoUnidDependencia = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidDependencia"]), row["nombreTipoUnidDep"].ToString());
                        if (!row.IsNull("idEstadoSolicitudIns"))
                        {
                            unidadResp.estadoSolicitudDep = new ParametroGenerico(Convert.ToInt32(row["idEstadoSolicitudIns"]), row["estadoIngresado"].ToString());
                        }
                        unidadResp.solicitudDepende = new SolicitudConcesion();
                        unidadResp.solicitudDepende.idSolConcesion = Convert.ToInt32(row["idSolicConcesDep"]);
                        unidadResp.solicitudDepende.numPert = row["claveUnidDependencia"].ToString();
                        unidadResp.solicitudDepende.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramiteDep"]), row["nomTramDep"].ToString());
                        unidadResp.solicitudDepende.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUEDep"]), row["nomTipUE"].ToString());
                        
                        resp.Add(unidadResp);
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
        }*/

     
    }
}
