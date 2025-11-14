using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using Datos.AccesoDatos;
using Datos.Contantes;

namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class ProyectoTecnicoDA
    {
        Logger logger = new Logger();

        //GUARDA UN PROYECTO TECNICO ASOCIADO A UNA SOLICITUD
        public bool GuardarProyectoTecnico(ProyectoTecnico proyTecnico, int idUsuario)
        {

            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbProyectoTecnico";
                cnn.parametros.Add("@idProyectoTecnico", proyTecnico.IdProyectoTecnico);
                cnn.parametros.Add("@idSolConcesion", proyTecnico.idSolicitud);
                
                cnn.parametros.Add("@estadoProy", rbEstadosGenerales.VIGENTE);
                if (proyTecnico.mangasPlasticasVal>-1)
                {
                    if(proyTecnico.mangasPlasticasVal==1){
                        proyTecnico.mangasPlasticas = true;
                    }else{
                        proyTecnico.mangasPlasticas = false;
                    }
                    cnn.parametros.Add("@mangasPlasticas", proyTecnico.mangasPlasticas);
                }
                
                if (proyTecnico.observaciones != null && !proyTecnico.observaciones.Equals(""))
                {
                    cnn.parametros.Add("@observaciones", proyTecnico.observaciones);
                }
                
                cnn.parametros.Add("@controlaProduccion", false);
                if (proyTecnico.fechaInicio != null && proyTecnico.fechaInicio != default(DateTime))
                {
                    cnn.parametros.Add("@fechaInicio", proyTecnico.fechaInicio);
                }
                if (proyTecnico.fechaTermino != null && proyTecnico.fechaTermino != default(DateTime))
                {
                    cnn.parametros.Add("@fechaTermino", proyTecnico.fechaTermino);
                }
                if (proyTecnico.tipoCultivoAlgas != null && proyTecnico.tipoCultivoAlgas.id > 0)
                {
                    cnn.parametros.Add("@idMetodoDeCultivo", proyTecnico.tipoCultivoAlgas.id);
                }
                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }


                DataTable dt = cnn.Execute();
                proyTecnico.IdProyectoTecnico = Convert.ToInt32(dt.Rows[0]["idProyectoTecnico"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };

        }

        public ProyectoTecnico ObtieneProyectoTecnico(int idSolicitud, int idProyectoTecnico)
        {
            try
            {
                ProyectoTecnico proyTecnico = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbProyectoTecnico";
                cnn.parametros.Add("@idSolConcesion", idSolicitud);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        proyTecnico = new ProyectoTecnico();
                        proyTecnico.IdProyectoTecnico = Convert.ToInt32(row["idProyectoTecnico"]);
                        proyTecnico.idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                        proyTecnico.estadoProy = new ParametroGenerico(Convert.ToInt32(row["estadoProy"]));
                        if (!row.IsNull("mangasPlasticas"))
                        {
                            proyTecnico.mangasPlasticas = Convert.ToBoolean(row["mangasPlasticas"]);
                        }
                        /*//DENSIDAD DE SIEMBRA SE CAMBIA A TABLA RBESTRUCTURA_PT
                        if (!row.IsNull("densidadDeSiembra"))
                        {
                            proyTecnico.densidadSiembra = Convert.ToSingle(row["densidadDeSiembra"]);
                        }*/
                        proyTecnico.controlaProduccion = Convert.ToBoolean(row["controlaProduccion"]);

                        if (!row.IsNull("idMetodoDeCultivo"))
                        {
                            proyTecnico.tipoCultivoAlgas = new ParametroGenerico(Convert.ToInt32(row["idMetodoDeCultivo"]));
                        }

                        if (!row.IsNull("observaciones"))
                        {
                            proyTecnico.observaciones = row["observaciones"].ToString();
                        }
                        if (!row.IsNull("fechaInicio"))
                        {
                            proyTecnico.fechaInicio = Convert.ToDateTime(row["fechaInicio"]);
                        }
                        if (!row.IsNull("fechaTermino"))
                        {
                            proyTecnico.fechaTermino = Convert.ToDateTime(row["fechaTermino"]);
                        }
                        
                        
                    }
                }

                return proyTecnico;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        //Tipos de alimentos asociados al proyecto tecnico
        public bool GuardarTipoAlimentoPorProyecto(TipoAlimentoProyecto tipoAlimentoPT)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbTipoAlimentoPorProyecto";
                cnn.parametros.Add("@idProyectoTecnico", tipoAlimentoPT.idProyectoTecnico);
                cnn.parametros.Add("@idTipoAlimento", tipoAlimentoPT.tipoAlimento.id);
                if (tipoAlimentoPT.detalle != null && !tipoAlimentoPT.detalle.Equals(""))
                {
                    cnn.parametros.Add("@detalle", tipoAlimentoPT.detalle);
                }
                
                DataTable dt = cnn.Execute();
                tipoAlimentoPT.idProyectoTecnico = Convert.ToInt32(dt.Rows[0]["idProyectoTecnico"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool GuardarTipoFondoProy(int idProyectoTec, int idTipoFondoCultivo, string detalle)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbTipoFondoProy";
                cnn.parametros.Add("@idProyectoTecnico", idProyectoTec);
                cnn.parametros.Add("@idTipoFondoCultivo", idTipoFondoCultivo);

                if (detalle != null && !detalle.Equals(""))
                {
                    cnn.parametros.Add("@detalle", detalle);
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

        public bool EliminarTipoFondoProy(int idProyectoTec)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbTipoFondoProy";
                cnn.parametros.Add("@idProyectoTecnico", idProyectoTec);
                
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

        public bool GuardarMetodoCultivoAlgasProy(int idProyectoTec, int idMetodoDeCultivo, string detalle)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbMetodoCultivoAlgasProy";
                cnn.parametros.Add("@idProyectoTecnico", idProyectoTec);
                cnn.parametros.Add("@idMetodoDeCultivo", idMetodoDeCultivo);

                if (detalle != null && !detalle.Equals(""))
                {
                    cnn.parametros.Add("@detalle", detalle);
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

        public bool EliminarMetodoCultivoAlgasProy(int idProyectoTec)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbMetodoCultivoAlgasProy";
                cnn.parametros.Add("@idProyectoTecnico", idProyectoTec);
                
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

         public List<TipoAlimentoProyecto> ListarMetodoCultivoAlgasProy(int idProyTecnico)
        {
            try
            {
                List<TipoAlimentoProyecto> resp = new List<TipoAlimentoProyecto>();
                TipoAlimentoProyecto metodoCultAlgas = null;
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbMetodoCultivoAlgasProy";
                cnn.parametros.Add("@idProyectoTecnico", idProyTecnico);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        metodoCultAlgas = new TipoAlimentoProyecto();
                        metodoCultAlgas.idProyectoTecnico = Convert.ToInt32(row["idProyectoTecnico"]);
                        metodoCultAlgas.tipoAlimento = new ParametroGenerico(Convert.ToInt32(row["idMetodoDeCultivo"]), row["nombreTipo"].ToString());
                        if (!row.IsNull("detalle"))
                        {
                            metodoCultAlgas.detalle = row["detalle"].ToString();
                        }
                        resp.Add(metodoCultAlgas);

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

         public List<TipoAlimentoProyecto> ListarTipoFondoProy(int idProyTecnico)
         {
             try
             {
                 List<TipoAlimentoProyecto> resp = new List<TipoAlimentoProyecto>();
                 TipoAlimentoProyecto tipoFondoProy = null;

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbTipoFondoProy";
                 cnn.parametros.Add("@idProyectoTecnico", idProyTecnico);

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {

                         tipoFondoProy = new TipoAlimentoProyecto();
                         tipoFondoProy.idProyectoTecnico = Convert.ToInt32(row["idProyectoTecnico"]);
                         tipoFondoProy.tipoAlimento = new ParametroGenerico(Convert.ToInt32(row["idTipoFondoCultivo"]), row["nombreTipo"].ToString());
                         if (!row.IsNull("detalle"))
                         {
                             tipoFondoProy.detalle = row["detalle"].ToString();
                         }
                         resp.Add(tipoFondoProy);

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

         public List<TipoAlimentoProyecto> ListarTipoAlimentoPorProyecto(int idProyTecnico)
        {
            try
            {
                List<TipoAlimentoProyecto> resp = new List<TipoAlimentoProyecto>();
                TipoAlimentoProyecto tipoAlimento = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTipoAlimentoPorProyecto";
                cnn.parametros.Add("@idProyectoTecnico", idProyTecnico);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        tipoAlimento = new TipoAlimentoProyecto();
                        tipoAlimento.idProyectoTecnico =  Convert.ToInt32(row["idProyectoTecnico"]);
                        tipoAlimento.tipoAlimento = new ParametroGenerico(Convert.ToInt32(row["idTipoAlimento"]),"");
                        if (!row.IsNull("idTipoAlimento"))
                        {
                            tipoAlimento.detalle = row["detalle"].ToString();
                        }

                        resp.Add(tipoAlimento);
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

         public bool EliminarTipoAlimentoPorProyecto(int idProyTecnico)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paDelRbTipoAlimentoPorProyecto";
                 cnn.parametros.Add("@idProyectoTecnico", idProyTecnico);
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


         public bool GuardarProyectoTecnico(int idSolTramiteMod,int idConcesionSol)
         {

             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paInsRbProyectoTecnicoTramiteMod";
                 cnn.parametros.Add("@idSolTramiteMod", idSolTramiteMod);
                 cnn.parametros.Add("@idConcesionSol", idConcesionSol);
                 
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

         public bool GuardarArchivoProyTecnico(ArchivosAdjPT archivoPT)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paInsRbArchivoPT";

                 cnn.parametros.Add("@idProyectoTecnico", archivoPT.idPT);
                 cnn.parametros.Add("@idArchivoBinSC", archivoPT.archivoBinario.idArchivo);
                 cnn.parametros.Add("@idTipoDoc", archivoPT.tipoDocumento.id);
                 cnn.parametros.Add("@numeroCI", archivoPT.numCI);
                 cnn.parametros.Add("@fechaCI", archivoPT.fechaCI);
                 cnn.parametros.Add("@idEstadoVigencia", archivoPT.estadoVigencia.id);


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

         public bool EliminarArchivoPT(int idPT, int idArchivo)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paDelRbArchivoPT";
                 if (idPT > 0)
                 {
                     cnn.parametros.Add("@idProyectoTecnico", idPT);
                 }
                 if (idArchivo > 0)
                 {
                     cnn.parametros.Add("@idArchivoBinSC", idArchivo);
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

         public List<ArchivosAdjPT> ListarArchivosAdjuntoPT(int idProyectoTecnico, int idArchivoBinSC)
         {
             try
             {
                 List<ArchivosAdjPT> resp = new List<ArchivosAdjPT>();
                 ArchivosAdjPT archivoPT = null;

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbArchivoPT";
                 cnn.parametros.Add("@idProyectoTecnico", idProyectoTecnico);

                 if (idArchivoBinSC > 0)
                 {
                     cnn.parametros.Add("@idArchivoBinSC", idArchivoBinSC);
                 }

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {
                     int i = 0;
                     foreach (DataRow row in dt.Rows)
                     {

                         archivoPT = new ArchivosAdjPT();
                         archivoPT.idPT = Convert.ToInt32(row["idProyectoTecnico"]);
                         archivoPT.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDoc"]), row["nombreTipo"].ToString());
                         archivoPT.archivoBinario = new ArchivoBinarioEspecial();
                         archivoPT.archivoBinario.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                         archivoPT.archivoBinario.nombreFisico = Convert.ToString(row["nombreFisico"]);
                         archivoPT.archivoBinario.nombreArchivo = Convert.ToString(row["nombreArchivo"]);
                         archivoPT.archivoBinario.formato = Convert.ToString(row["formato"]);
                         archivoPT.archivoBinario.bytes = (byte[])row["contenido"];
                         archivoPT.numCI = Convert.ToInt32(row["numeroCI"]);
                         archivoPT.fechaCI = Convert.ToDateTime(row["fechaCI"]);
                         archivoPT.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
                         archivoPT.accion = accion.LISTADO;
                         archivoPT.index = i;

                         resp.Add(archivoPT);
                         i++;
                     }
                 }

                 return resp;

             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             };
         }

         public ArchivosAdjPT ObtenerArchivosAdjuntoPT(int idProyectoTecnico, int idArchivoBinSC)
         {
             try
             {
                 ArchivosAdjPT archivoPT = null;

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbArchivoPT";
                 cnn.parametros.Add("@idProyectoTecnico", idProyectoTecnico);

                 if (idArchivoBinSC > 0)
                 {
                     cnn.parametros.Add("@idArchivoBinSC", idArchivoBinSC);
                 }

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {
                     int i = 0;
                     foreach (DataRow row in dt.Rows)
                     {

                         archivoPT = new ArchivosAdjPT();
                         archivoPT.idPT = Convert.ToInt32(row["idProyectoTecnico"]);
                         archivoPT.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDoc"]), row["nombreTipo"].ToString());
                         archivoPT.archivoBinario = new ArchivoBinarioEspecial();
                         archivoPT.archivoBinario.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                         archivoPT.archivoBinario.nombreFisico = Convert.ToString(row["nombreFisico"]);
                         archivoPT.archivoBinario.nombreArchivo = Convert.ToString(row["nombreArchivo"]);
                         archivoPT.archivoBinario.formato = Convert.ToString(row["formato"]);
                         archivoPT.archivoBinario.bytes = (byte[])row["contenido"];
                         archivoPT.numCI = Convert.ToInt32(row["numeroCI"]);
                         archivoPT.fechaCI = Convert.ToDateTime(row["fechaCI"]);
                         archivoPT.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
                         
                     }
                 }

                 return archivoPT;

             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             };
         }

         public bool ActualizarEstadoVigenciaArchivoPT(ArchivosAdjPT archivoPT)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paUpdRbArchivoPTVigencia";

                 cnn.parametros.Add("@idProyectoTecnico", archivoPT.idPT);
                 cnn.parametros.Add("@idArchivoBinSC", archivoPT.archivoBinario.idArchivo);
                 cnn.parametros.Add("@idEstadoVigencia", archivoPT.estadoVigencia.id);

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
