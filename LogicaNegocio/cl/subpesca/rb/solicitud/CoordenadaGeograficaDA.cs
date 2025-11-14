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
    public class CoordenadaGeograficaDA
    {

        Logger logger = new Logger();

        //GUARDA COORDENADA GEOGRAFICA - ANTECEDENTES DEL SECTOR
        public bool GuardarCoordenadaGeografica(CoordenadaGeografica coordGeografica, int idUsuario)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbCoordenadasGeo";
                cnn.parametros.Add("@idCoordenadaGeo", coordGeografica.idCoordenadaGeo);

                if (coordGeografica.archivoBinario != null && coordGeografica.archivoBinario.idArchivo > 0)
                {
                    cnn.parametros.Add("@idArchivoBinSC", coordGeografica.archivoBinario.idArchivo);
                }
                if (coordGeografica.estado != null && coordGeografica.estado.id > 0)
                {
                    cnn.parametros.Add("@idEstado", coordGeografica.estado.id);
                }
                if (coordGeografica.carta != null && coordGeografica.carta.idCarta > 0)
                {
                    cnn.parametros.Add("@idCarta", coordGeografica.carta.idCarta);
                }
                if (coordGeografica.tipoHuso != null && coordGeografica.tipoHuso.id > 0)
                {
                    cnn.parametros.Add("@idHuso", coordGeografica.tipoHuso.id);
                }
                if (coordGeografica.datum != null && coordGeografica.datum.id > 0)
                {
                    cnn.parametros.Add("@idDatum", coordGeografica.datum.id);
                }
                cnn.parametros.Add("@areaTotalCalculada", coordGeografica.areaTotalCalculada);
                cnn.parametros.Add("@areaTotalSolicitada", coordGeografica.areaTotalSolicitada);
                cnn.parametros.Add("@areaRegularizada", coordGeografica.areaTotalRegularizacion);
                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }

                DataTable dt = cnn.Execute();
                coordGeografica.idCoordenadaGeo = Convert.ToInt32(dt.Rows[0]["idCoordenadaGeo"]);

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
        * OBTIENE COORDENADA GEOGRAFICA
        */
        public CoordenadaGeografica ObtieneCoordenadaGeografica(int idSolConcesion, int idCoordenadaGeo)
        {
            try
            {

                CoordenadaGeografica coordGeoResp = null;
                //string cartaAux="";

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbCoordenadasAntecSector";
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                cnn.parametros.Add("@idCoordenadaGeo", idCoordenadaGeo);
                
                DataTable dt = cnn.Execute();

                if(dt!=null){

                    foreach(DataRow row in dt.Rows){
                        coordGeoResp = new CoordenadaGeografica();
                        coordGeoResp.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                        coordGeoResp.idCoordenadaGeo = Convert.ToInt32(row["idCoordenadaGeo"]);
                        if (!row.IsNull("idDatum"))
                        {
                            coordGeoResp.datum = new ParametroGenerico(Convert.ToInt32(row["idDatum"]), row["nombreDatum"].ToString());
                        }
                        if (!row.IsNull("idHuso"))
                        {
                            coordGeoResp.tipoHuso = new ParametroGenerico(Convert.ToInt32(row["idHuso"]), row["nombreHuso"].ToString());
                        }
                        if (!row.IsNull("idCarta"))
                        {
                            coordGeoResp.carta = new Carta();
                            coordGeoResp.carta.idCarta = Convert.ToInt32(row["idCarta"]);
                            coordGeoResp.carta.region = new ParametroGenerico(Convert.ToInt32(row["idRegion"]), row["Region"].ToString());
                            coordGeoResp.carta.tipoCarta = new ParametroGenerico(Convert.ToInt32(row["idTipoCarta"]), row["nombreTipoCarta"].ToString());
                            if (!row.IsNull("idDatumCarta"))
                            {
                                coordGeoResp.carta.datum = new ParametroGenerico(Convert.ToInt32(row["idDatumCarta"]), row["nombreDatumCarta"].ToString());
                            }
                            if (!row.IsNull("idHusoCarta"))
                            {
                                coordGeoResp.carta.huso = new ParametroGenerico(Convert.ToInt32(row["idHusoCarta"]), row["nombreHusoCarta"].ToString());
                            }
                            
                            if (!row.IsNull("numeroCarta"))
                            {
                                coordGeoResp.carta.numeroCarta = row["numeroCarta"].ToString();
                            }

                            if (!row.IsNull("numEdicion"))
                            {
                                coordGeoResp.carta.numeroEdicion = Convert.ToInt32(row["numEdicion"]);
                            }

                            if (!row.IsNull("anioEdicion"))
                            {
                                coordGeoResp.carta.anioEdicion = Convert.ToInt32(row["anioEdicion"]);
                            }

                            if (!row.IsNull("A_A_A"))
                            {
                                coordGeoResp.carta.a_a_a = Convert.ToBoolean(row["A_A_A"]);
                            }

                            if (!row.IsNull("descripcionCarta"))
                            {
                                coordGeoResp.carta.descripcionCarta = row["descripcionCarta"].ToString();
                            }


                        }
                        
                        

                        coordGeoResp.archivoBinario = new ArchivoBinario();
                        if (!row.IsNull("idArchivoBinSC"))
                        {
                            coordGeoResp.archivoBinario.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                        }
                        if (!row.IsNull("idEstado"))
                        {
                            coordGeoResp.estado = new ParametroGenerico(Convert.ToInt32(row["idEstado"]), row["nombreEstado"].ToString());
                        }
                        coordGeoResp.areaTotalCalculada = Convert.ToSingle(row["areaTotalCalculada"]);
                        coordGeoResp.areaTotalSolicitada = Convert.ToSingle(row["areaTotalSolicitada"]);
                        coordGeoResp.areaTotalRegularizacion = Convert.ToSingle(row["areaRegularizada"]);
                        coordGeoResp.tipoCoordgeografica = new ParametroGenerico(Convert.ToInt32(row["idTipoCoordGeo"]));
                        coordGeoResp.aplicaBanco = Convert.ToBoolean(row["aplicaBanco"]);

                        if (!row.IsNull("aplicaVisualizMapa"))
                        {
                            coordGeoResp.aplicaVisualizadorDeMapas = Convert.ToBoolean(row["aplicaVisualizMapa"]);
                        }
                        //--
                        
                        
                    }

                }
                return coordGeoResp;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        /**
       * LISTADO DE COORDENADA GEOGRAFICAS ASOCIADAS A UNA SOLICITUD
       */
        public List<CoordenadaGeografica> ListarCoordenadaGeografica(int idSolConcesion, int idCoordenadaGeo)
        {
            try
            {

                CoordenadaGeografica coordGeoResp = null;
                List<CoordenadaGeografica> resp = new List<CoordenadaGeografica>();
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbCoordenadasAntecSector";
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                if (idCoordenadaGeo > 0)
                {
                    cnn.parametros.Add("@idCoordenadaGeo", idCoordenadaGeo);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        coordGeoResp = new CoordenadaGeografica();
                        coordGeoResp.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                        coordGeoResp.idCoordenadaGeo = Convert.ToInt32(row["idCoordenadaGeo"]);
                        if (!row.IsNull("idDatum"))
                        {
                            coordGeoResp.datum = new ParametroGenerico(Convert.ToInt32(row["idDatum"]), row["nombreDatum"].ToString());
                        }
                        if (!row.IsNull("idHuso"))
                        {
                            coordGeoResp.tipoHuso = new ParametroGenerico(Convert.ToInt32(row["idHuso"]), row["nombreHuso"].ToString());
                        }

                        coordGeoResp.carta = new Carta();
                        if (!row.IsNull("idCarta"))
                        {
                            coordGeoResp.carta.idCarta = Convert.ToInt32(row["idCarta"]);
                        }

                        if (!row.IsNull("idRegion"))
                        {
                            coordGeoResp.carta.region = new ParametroGenerico(Convert.ToInt32(row["idRegion"]), row["Region"].ToString());
                        }

                        if (!row.IsNull("idTipoCarta"))
                        {
                            coordGeoResp.carta.tipoCarta = new ParametroGenerico(Convert.ToInt32(row["idTipoCarta"]), row["nombreTipoCarta"].ToString());
                        }

                        if (!row.IsNull("idDatumCarta"))
                        {
                            coordGeoResp.carta.datum = new ParametroGenerico(Convert.ToInt32(row["idDatumCarta"]), row["nombreDatumCarta"].ToString());
                        }

                        if (!row.IsNull("idHusoCarta"))
                        {
                            coordGeoResp.carta.huso = new ParametroGenerico(Convert.ToInt32(row["idHusoCarta"]), row["nombreHusoCarta"].ToString());
                        }
                        if (!row.IsNull("numeroCarta"))
                        {
                            coordGeoResp.carta.numeroCarta = row["numeroCarta"].ToString();
                        }

                        if (!row.IsNull("numEdicion"))
                        {
                            coordGeoResp.carta.numeroEdicion = Convert.ToInt32(row["numEdicion"]);
                        }

                        if (!row.IsNull("anioEdicion"))
                        {
                            coordGeoResp.carta.anioEdicion = Convert.ToInt32(row["anioEdicion"]);
                        }

                        if (!row.IsNull("A_A_A"))
                        {
                            coordGeoResp.carta.a_a_a = Convert.ToBoolean(row["A_A_A"]);
                        }

                        if (!row.IsNull("descripcionCarta"))
                        {
                            coordGeoResp.carta.descripcionCarta = row["descripcionCarta"].ToString();
                        }

                        coordGeoResp.archivoBinario = new ArchivoBinario();
                        if (!row.IsNull("idArchivoBinSC"))
                        {
                            coordGeoResp.archivoBinario.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                        }
                        
                        if (!row.IsNull("idEstado"))
                        {
                            coordGeoResp.estado = new ParametroGenerico(Convert.ToInt32(row["idEstado"]), row["nombreEstado"].ToString());
                        }

                        coordGeoResp.areaTotalCalculada = Convert.ToSingle(row["areaTotalCalculada"]);
                        coordGeoResp.areaTotalSolicitada = Convert.ToSingle(row["areaTotalSolicitada"]);
                        coordGeoResp.areaTotalRegularizacion = Convert.ToSingle(row["areaRegularizada"]);
                        coordGeoResp.tipoCoordgeografica = new ParametroGenerico(Convert.ToInt32(row["idTipoCoordGeo"]));
                        coordGeoResp.aplicaBanco = Convert.ToBoolean(row["aplicaBanco"]);

                        if (!row.IsNull("aplicaVisualizMapa"))
                        {
                            coordGeoResp.aplicaVisualizadorDeMapas = Convert.ToBoolean(row["aplicaVisualizMapa"]);
                        }
                        resp.Add(coordGeoResp);

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

        //ELIMINA UN REGISTRO DE COORDENADA GEOGRAFICA EN BASE A SU PK
        public bool EliminarCoordenadaGeografica(int idCoordenada, int idUsuario)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbCoordenadasGeo";
                cnn.parametros.Add("@idCoordenadaGeo", idCoordenada);
                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
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

         //GUARDA ASOCIACION COORDENADA-SOLICITUD (ANTECEDENTES DEL SECTOR)
         public bool GuardarCoordenadasAntecSector(int idSolConcesion, int idCoordenadaGeo, int idTipoCoordGeo, bool aplicaBanco, bool aplicaVizMapa)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paInsRbCoordenadasAntecSector";
                 cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                 cnn.parametros.Add("@idCoordenadaGeo", idCoordenadaGeo);
                 cnn.parametros.Add("@idTipoCoordGeo", idTipoCoordGeo);
                 cnn.parametros.Add("@aplicaBanco", aplicaBanco);
                 cnn.parametros.Add("@aplicaVisualizMapa", aplicaVizMapa);
                 

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

         //GUARDA ARCHIVO DE COORDENADA GEOGRAFICA - ANTECEDENTES DEL SECTOR
         public bool GuardarArchivoCoordenadaGeografica(int idCoordenada, int idArchivo, int idTipoDocumento, int idEstado)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paInsRbArchivoCoordenadaGeo";
                 cnn.parametros.Add("@idCoordenadaGeo", idCoordenada);
                 cnn.parametros.Add("@idArchivoBinSC", idArchivo);
                 cnn.parametros.Add("@idTipoDocumento", idTipoDocumento);
                 cnn.parametros.Add("@idEstado", idEstado);


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

         public ArchivoCoordenadaGeo ObtenerArchivoBinarioCoordenada(int idCoordenada, int idArchivoBinSC)
         {

             try
             {

                 ArchivoCoordenadaGeo archCoorGeo = null;

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbArchivoCoordenadaGeo";
                 if (idCoordenada > 0)
                 {
                     cnn.parametros.Add("@idCoordenadaGeo", idCoordenada);
                 }
                 cnn.parametros.Add("@idArchivoBinSC", idArchivoBinSC);
                 
                 DataTable dt = cnn.Execute();


                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         archCoorGeo = new ArchivoCoordenadaGeo();
                         archCoorGeo.archivoBinario = new ArchivoBinario();
                         archCoorGeo.archivoBinario.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                         archCoorGeo.archivoBinario.nombreFisico = Convert.ToString(row["nombreFisico"]);
                         archCoorGeo.archivoBinario.nombreArchivo = Convert.ToString(row["nombreArchivo"]);
                         archCoorGeo.archivoBinario.formato = Convert.ToString(row["formato"]);
                         archCoorGeo.archivoBinario.bytes = (byte[])row["contenido"];
                         archCoorGeo.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipo"].ToString());
                         archCoorGeo.estado = new ParametroGenerico(Convert.ToInt32(row["idEstado"]), row["nombreEstado"].ToString());
                     }
                 }

                 return archCoorGeo;
             }

             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             };
         }

         public List<ArchivoCoordenadaGeo> ListarArchivoBinarioCoordenada(int idCoordenada, int idArchivoBinSC)
         {

             try
             {

                 List<ArchivoCoordenadaGeo> resp = new List<ArchivoCoordenadaGeo>();
                 ArchivoCoordenadaGeo archivoCoorGeo = null;

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbArchivoCoordenadaGeo";
                 if (idCoordenada>0)
                 {
                     cnn.parametros.Add("@idCoordenadaGeo", idCoordenada);
                 }
                 
                 if (idArchivoBinSC > 0)
                 {
                     cnn.parametros.Add("@idArchivoBinSC", idArchivoBinSC);
                 }

                 DataTable dt = cnn.Execute();


                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         archivoCoorGeo = new ArchivoCoordenadaGeo();
                         archivoCoorGeo.archivoBinario = new ArchivoBinario();
                         archivoCoorGeo.archivoBinario.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                         archivoCoorGeo.archivoBinario.nombreFisico = Convert.ToString(row["nombreFisico"]);
                         archivoCoorGeo.archivoBinario.nombreArchivo = Convert.ToString(row["nombreArchivo"]);
                         archivoCoorGeo.archivoBinario.formato = Convert.ToString(row["formato"]);
                         if(!row.IsNull("contenido")){
                             archivoCoorGeo.archivoBinario.bytes = (byte[])row["contenido"];
                         }

                         archivoCoorGeo.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipo"].ToString());
                         archivoCoorGeo.estado = new ParametroGenerico(Convert.ToInt32(row["idEstado"]), row["nombreEstado"].ToString());
                         resp.Add(archivoCoorGeo);
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
         //Elimina Coordenadas asociadas a una Solicitud
         public bool EliminarCoordenadasAntecSector(int idSolConcesion, int idCoordenadaGeo)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paDelRbCoordenadasAntecSector";
                 cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                 if (idCoordenadaGeo > 0)
                 {
                     cnn.parametros.Add("@idCoordenadaGeo", idCoordenadaGeo);
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

         //ELIMINA REGISTROS DE LA ASOCIACION COORDENADA-ARCHIVO BINARIO
         public bool EliminarArchivoCoordenadaGeo(int idCoordenadaGeo, int idArchivoBin)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paDelRbArchivoCoordenadaGeo";
                 cnn.parametros.Add("@idCoordenadaGeo", idCoordenadaGeo);
                 if (idArchivoBin > 0)
                 {
                     cnn.parametros.Add("@idArchivoBinSC", idArchivoBin);
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

         public List<ParametroGenerico> ListaHuso(int idHuso)
        {
            try
            {
                ParametroGenerico param = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();


                Conexion cnn = new Conexion();
                
                cnn.procedimiento = "paSelRbHuso";

                if(idHuso>0){
                    cnn.parametros.Add("@idHuso", idHuso);
                }
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        param = new ParametroGenerico(Convert.ToInt32(row["idHuso"]), row["nombreHuso"].ToString());

                        resp.Add(param);
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

         public bool ObtieneCoordSectorAplicaBanco(int idSolConcesion)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbCoordenadasSectorAplicaBanco";
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        return true;
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
         public bool ObtieneCoordSectorAplicaVizMapa(int idSolConcesion)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbCoordenadasSectorAplicaVizMapa";
                 cnn.parametros.Add("@idSolConcesion", idSolConcesion);

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {
                     foreach (DataRow row in dt.Rows)
                     {
                         return true;
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

         public bool GuardarCoordenadaGeograficaTramiteMod(int idSolicitudTramiteMod, int idConcesionSol)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paInsRbCoordenadasGeoTramiteMod";
                 cnn.parametros.Add("@idSolicitudTramiteMod", idSolicitudTramiteMod);
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

         public bool GuardarArchivoPlanilla(ArchivoBinario archivoBinario)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paInsRbArchivoPlanilla";

                 cnn.parametros.Add("@idArchivoPlanilla", archivoBinario.idArchivo);
                 cnn.parametros.Add("@nombreFisico", archivoBinario.nombreFisico);
                 cnn.parametros.Add("@nombreArchivo", archivoBinario.nombreArchivo);
                 cnn.parametros.Add("@fechaDeCarga", DateTime.Now);
                 cnn.parametros.Add("@tamano", archivoBinario.tamano);
                 cnn.parametros.Add("@formato", archivoBinario.formato);
                 cnn.parametros.Add("@idUsuario", archivoBinario.usuario.id_usuario);

                 /*
                 byte[] file = new byte[archivoBinario.archivo.InputStream.Length];
                 archivoBinario.archivo.InputStream.Read(file, 0, file.Length);
                 */

                 //cnn.parametros.Add("@contenido", file);

                 cnn.parametros.Add("@contenido", archivoBinario.bytes);
                 if (archivoBinario.observaciones != null)
                 {
                     cnn.parametros.Add("@observaciones", archivoBinario.observaciones);
                 }

                 DataTable dt = cnn.Execute();
                 archivoBinario.idArchivo = Convert.ToInt32(dt.Rows[0]["idArchivoPlanilla"]);

                 return true;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return false;
             };
         }

         //GUARDA ARCHIVO DE PLANILLA ASOCIADO A UNA SOLICITUD
         public bool GuardarPlanillaSolicitud(int idSolConcesion, int idArchivoPlanilla)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paInsRbPlanillaSolicitud";
                 cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                 cnn.parametros.Add("@idArchivoPlanilla", idArchivoPlanilla);
                 
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

         public ArchivoBinario ObtieneArchivoPlanilla(int idArchivoPlanilla)
         {
             try
             {
                 ArchivoBinario archivoCoorGeo = null;

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbArchivoPlanilla";
                 if (idArchivoPlanilla > 0)
                 {
                     cnn.parametros.Add("@idArchivoPlanilla", idArchivoPlanilla);
                 }

                 DataTable dt = cnn.Execute();


                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         archivoCoorGeo = new ArchivoBinario();
                         archivoCoorGeo.idArchivo = Convert.ToInt32(row["idArchivoPlanilla"]);
                         archivoCoorGeo.nombreFisico = Convert.ToString(row["nombreFisico"]);
                         archivoCoorGeo.nombreArchivo = Convert.ToString(row["nombreArchivo"]);
                         archivoCoorGeo.formato = Convert.ToString(row["formato"]);
                         if (!row.IsNull("contenido"))
                         {
                             archivoCoorGeo.bytes = (byte[])row["contenido"];
                         }

                         
                     }
                 }

                 return archivoCoorGeo;
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
