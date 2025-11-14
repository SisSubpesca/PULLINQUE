using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using Datos.AccesoDatos;
using System.Data;
using Datos.Contantes;

namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class EspecieProyTecnicoDA
    {
        Logger logger = new Logger();

        public bool GuardarEspecieProyTecnico(EspecieAutorizadaPT especieAutPT)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbEspecieProyTecnico";
                cnn.parametros.Add("@idProyectoTecnico", especieAutPT.idProyectoTecnico);
                if (especieAutPT.especie!= null && especieAutPT.especie.id > 0)
                {
                    cnn.parametros.Add("@idEspecie", especieAutPT.especie.id);
                }
                cnn.parametros.Add("@autorizada", false);
                cnn.parametros.Add("@aIncorporar", false);
                if( especieAutPT.grupoEspecieAutoriz!= null &&  especieAutPT.grupoEspecieAutoriz.id>0){
                    cnn.parametros.Add("@idGrupoAutorizado", especieAutPT.grupoEspecieAutoriz.id);
                }if(especieAutPT.tipoCultivo!=null && especieAutPT.tipoCultivo.id>0){
                    cnn.parametros.Add("@idTipoCultivo", especieAutPT.tipoCultivo.id);
                }if(especieAutPT.tipoAlimento!=null && especieAutPT.tipoAlimento.id>0){
                    cnn.parametros.Add("@idTipoAlimento", especieAutPT.tipoAlimento.id);
                } if(especieAutPT.detalle!=null && !especieAutPT.detalle.Equals("")){
                    cnn.parametros.Add("@detalle", especieAutPT.detalle);
                }
                
                DataTable dt = cnn.Execute();
                especieAutPT.idEspeciePT = Convert.ToInt32(dt.Rows[0]["idEspProyTecnico"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }


        public List<EspecieAutorizadaPT> ListarEspecieProyTecnico(int idProyTecnico, int idEspProyTecnico)
        {
            try
            {

                List<EspecieAutorizadaPT> resp = new List<EspecieAutorizadaPT>();
                EspecieAutorizadaPT especieProy = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbEspecieProyTecnico";
                if (idProyTecnico > 0)
                {
                    cnn.parametros.Add("@idProyectoTecnico", idProyTecnico);
                }
                if (idEspProyTecnico>0)
                {
                    cnn.parametros.Add("@idEspProyTecnico", idEspProyTecnico);
                }
                DataTable dt = cnn.Execute();
                int indes = 0;

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        especieProy = new EspecieAutorizadaPT();
                        especieProy.idEspeciePT = Convert.ToInt32(row["idEspProyTecnico"]);
                        especieProy.idProyectoTecnico = Convert.ToInt32(row["idProyectoTecnico"]);

                        if (!row.IsNull("idEspecie"))
                        {
                            especieProy.especie = new ParametroGenerico(Convert.ToInt32(row["idEspecie"]), row["EspecieCultivo"].ToString());
                            if (!row.IsNull("idGrupoInfEsp"))
                            {
                                especieProy.grupoEspecie = new ParametroGenerico(Convert.ToInt32(row["idGrupoInfEsp"]), row["nombreGrupoInfEsp"].ToString());
                            }
                        }

                        if (!row.IsNull("IdEtapaDesarrollo"))
                        {
                            especieProy.etapaCultivo = new ParametroGenerico(Convert.ToInt32(row["IdEtapaDesarrollo"]), row["nombreEtapaCultivo"].ToString());
                        }
                        
                        especieProy.autorizada = Convert.ToBoolean(row["autorizada"]);
                        especieProy.incorporar = Convert.ToBoolean(row["aIncorporar"]);

                        if (!row.IsNull("idGrupoAutorizado"))
                        {
                            especieProy.grupoEspecieAutoriz = new ParametroGenerico(Convert.ToInt32(row["idGrupoAutorizado"]), row["GrupoEspecie"].ToString());
                            especieProy.especie = new ParametroGenerico(0, row["especieGrupoAutoriz"].ToString());
                        }
                        if (!row.IsNull("IdGrupoInfGrup"))
                        {
                            especieProy.grupoEspecie = new ParametroGenerico(Convert.ToInt32(row["IdGrupoInfGrup"]), row["nombreGrupoInfGrup"].ToString());
                        }

                        if (!row.IsNull("idTipoAlimento"))
                        {
                            especieProy.tipoAlimento = new ParametroGenerico(Convert.ToInt32(row["idTipoAlimento"]), row["nombreTipoAlimento"].ToString());
                        }

                        if (!row.IsNull("idTipoAlimento"))
                        {
                            especieProy.tipoCultivo = new ParametroGenerico(Convert.ToInt32(row["idTipoAlimento"]), row["nombreTipoCultivo"].ToString());
                        }

                        if (!row.IsNull("detalle"))
                        {
                            especieProy.detalle = row["detalle"].ToString();
                        }

                        if (!row.IsNull("especieGrupoAutoriz"))
                        {
                            especieProy.especieGrupoAutorizString = row["especieGrupoAutoriz"].ToString();
                        }

                        especieProy.accion = accion.LISTADO;
                        especieProy.index = indes;

                        resp.Add(especieProy);
                        indes++;
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

        public EspecieAutorizadaPT ObtenerEspecieProyTecnico(int idProyTecnico, int idEspProyTecnico)
        {
            try
            {

                EspecieAutorizadaPT especieProy = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbEspecieProyTecnico";
                if (idProyTecnico > 0)
                {
                    cnn.parametros.Add("@idProyectoTecnico", idProyTecnico);
                }
                if (idEspProyTecnico > 0)
                {
                    cnn.parametros.Add("@idEspProyTecnico", idEspProyTecnico);
                }
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        especieProy = new EspecieAutorizadaPT();
                        especieProy.idEspeciePT = Convert.ToInt32(row["idEspProyTecnico"]);
                        especieProy.idProyectoTecnico = Convert.ToInt32(row["idProyectoTecnico"]);
                        especieProy.especie = new ParametroGenerico(Convert.ToInt32(row["idEspecie"]), row["EspecieCultivo"].ToString());

                        if (!row.IsNull("IdEtapaDesarrollo"))
                        {
                            especieProy.etapaCultivo = new ParametroGenerico(Convert.ToInt32(row["IdEtapaDesarrollo"]), row["nombreEtapaCultivo"].ToString());
                        }
                        especieProy.autorizada = Convert.ToBoolean(row["autorizada"]);
                        especieProy.incorporar = Convert.ToBoolean(row["aIncorporar"]);

                        if (!row.IsNull("idGrupoAutorizado"))
                        {
                            especieProy.grupoEspecie = new ParametroGenerico(Convert.ToInt32(row["idGrupoAutorizado"]), row["GrupoEspecie"].ToString());
                        }

                        if (!row.IsNull("idTipoAlimento"))
                        {
                            especieProy.tipoAlimento = new ParametroGenerico(Convert.ToInt32(row["idTipoAlimento"]), row["nombreTipoAlimento"].ToString());
                        }

                        if (!row.IsNull("idTipoCultivo"))
                        {
                            especieProy.tipoCultivo = new ParametroGenerico(Convert.ToInt32(row["idTipoAlimento"]), row["nombreTipoCultivo"].ToString());
                        }


                    }
                }

                return especieProy;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public bool EliminarEspecieProyTecnico(EspecieAutorizadaPT espeAutoriz)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbEspecieProyTecnico";
                cnn.parametros.Add("@idProyectoTecnico", espeAutoriz.idProyectoTecnico);
                if (espeAutoriz.especie != null && espeAutoriz.especie.id > 0)
                {
                    cnn.parametros.Add("@idEspecie", espeAutoriz.especie.id);
                }
                if (espeAutoriz.grupoEspecie != null && espeAutoriz.grupoEspecie.id > 0)
                {
                    cnn.parametros.Add("@idGrupo", espeAutoriz.grupoEspecie.id);
                }
                if (espeAutoriz.usuario != null && espeAutoriz.usuario.id_usuario> 0)
                {
                    cnn.parametros.Add("@idUsuario", espeAutoriz.usuario.id_usuario);
                }
               
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

         public bool GrupoEspecie_enProyTecnico(int idSolicitud, int idGrupoEspecie)
        {
            try
            {
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbEspecieProyTecnicoGrupo";
                cnn.parametros.Add("@idSolConcesion", idSolicitud);
                cnn.parametros.Add("@IdGrupoEspecie", idGrupoEspecie);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        if (!row.IsNull("idEspProyTecnico"))
                        {
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
          public bool GuardarGrupoProyTecnico(GrupoPT grupoPT)
          {
              try
              {

                  Conexion cnn = new Conexion();
                  cnn.procedimiento = "paInsRbGrupoProyTecnico";
                  cnn.parametros.Add("@idProyectoTecnico", grupoPT.idProyectoTecnico);
                  cnn.parametros.Add("@idGrupoEspecie", grupoPT.grupo.id);
                  cnn.parametros.Add("@idEtapaCultivo", grupoPT.etapaCultivo.id);
                 
                  DataTable dt = cnn.Execute();
                  grupoPT.idGrupoPT = Convert.ToInt32(dt.Rows[0]["idGrupoProyTecnico"]);

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
          public bool EliminarGrupoProyTecnico(GrupoPT grupoPT)
          {
              try
              {
                  Conexion cnn = new Conexion();
                  cnn.procedimiento = "paDelRbGrupoProyTecnico";
                 
                  if (grupoPT.idProyectoTecnico > 0)
                  {
                      cnn.parametros.Add("@idProyectoTecnico", grupoPT.idProyectoTecnico);
                  }

                  cnn.parametros.Add("@idGrupoProyTecnico", grupoPT.idGrupoPT);
                 
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
         public List<GrupoPT> ListarGrupoProyTecnico(int idProyTecnico, int idGrupoProyTecnico)
         {
             try
             {

                 List<GrupoPT> resp = new List<GrupoPT>();
                 GrupoPT grupoProy = null;
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbGrupoProyTecnico";
                 if (idProyTecnico > 0)
                 {
                     cnn.parametros.Add("@idProyectoTecnico", idProyTecnico);
                 }
                 if (idGrupoProyTecnico > 0)
                 {
                     cnn.parametros.Add("@idGrupoProyTecnico", idGrupoProyTecnico);
                 }
                 DataTable dt = cnn.Execute();
                 int indes = 0;

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         grupoProy = new GrupoPT();
                         grupoProy.idGrupoPT = Convert.ToInt32(row["idGrupoProyTecnico"]);
                         grupoProy.idProyectoTecnico = Convert.ToInt32(row["idProyectoTecnico"]);

                         grupoProy.grupo = new ParametroGenerico(Convert.ToInt32(row["idGrupoEspecie"]), row["GrupoEspecie"].ToString());

                         if (!row.IsNull("idEtapaCultivo"))
                         {
                             grupoProy.etapaCultivo = new ParametroGenerico(Convert.ToInt32(row["idEtapaCultivo"]), row["nombreEtapaCultivo"].ToString());
                         }
                         grupoProy.accion = accion.LISTADO;
                         grupoProy.index = indes;

                         resp.Add(grupoProy);
                         indes++;
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

         public GrupoPT ObtenerGrupoProyTecnico(int idProyTecnico, int idGrupoProyTecnico)
         {
             try
             {

                 GrupoPT grupoProy = null;
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbGrupoProyTecnico";
                 if (idProyTecnico > 0)
                 {
                     cnn.parametros.Add("@idProyectoTecnico", idProyTecnico);
                 }
                 if (idGrupoProyTecnico > 0)
                 {
                     cnn.parametros.Add("@idGrupoProyTecnico", idGrupoProyTecnico);
                 }
                 DataTable dt = cnn.Execute();
                 
                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         grupoProy = new GrupoPT();
                         grupoProy.idGrupoPT = Convert.ToInt32(row["idGrupoProyTecnico"]);
                         grupoProy.idProyectoTecnico = Convert.ToInt32(row["idProyectoTecnico"]);

                         grupoProy.grupo = new ParametroGenerico(Convert.ToInt32(row["idGrupoEspecie"]), row["GrupoEspecie"].ToString());

                         if (!row.IsNull("idEtapaCultivo"))
                         {
                             grupoProy.etapaCultivo = new ParametroGenerico(Convert.ToInt32(row["idEtapaCultivo"]), row["nombreEtapaCultivo"].ToString());
                         }
                     }
                 }

                 return grupoProy;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             }
         }

         public bool GuardarEspecieEtapaProyTecnico(int idEspeciePT, int idProyectoTecnico)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paInsRbEspecieEtapaPT";

                 cnn.parametros.Add("@idEspProyTecnico", idEspeciePT);
                 cnn.parametros.Add("@IdEtapaDesarrollo", idProyectoTecnico);
                

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
         public bool EliminarEspecieEtapaProyTecnico(int idEspeciePT, int idEtapaDesarrollo)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paDelRbEspecieEtapaPT";
                 cnn.parametros.Add("@idEspProyTecnico", idEspeciePT);
                 if (idEtapaDesarrollo > 0)
                 {
                     cnn.parametros.Add("@IdEtapaDesarrollo", idEtapaDesarrollo);
                 }

                 DataTable dt = cnn.Execute();

                 int resul = Convert.ToInt32(dt.Rows[0]["resultado"]);
                 
                 if (resul >= 0 || resul == -1)
                 {
                     return true;
                 }

                 return false;

             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return false;
             };
         }

         //Inserción automatica de grupos autorizados si el pt cuenta con una especie autorizada.
         public bool GuardarGruposAutorizados(int idProyectoTecnico)
         {
             try
             {

                 Conexion cnn = new Conexion();

                 cnn.procedimiento = "paInsRbPTGruposAutorizados";

                 cnn.parametros.Add("@idProyectoTecnico", idProyectoTecnico);

                 DataTable dt = cnn.Execute();

                 idProyectoTecnico = Convert.ToInt32(dt.Rows[0]["idProyectoTecnico"]);

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
