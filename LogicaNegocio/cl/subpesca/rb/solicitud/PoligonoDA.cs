using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using Datos.AccesoDatos;

namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class PoligonoDA
    {

        Logger logger = new Logger();

        //GUARDA UN POLIGONO ASOCIADO A UNA SOLICITUD
        public bool GuardarPoligono(Poligono poligono, int idUsuario)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbPoligono";
                cnn.parametros.Add("@idPoligono", poligono.idPoligono);
                cnn.parametros.Add("@idCoordenadaGeo", poligono.idCoordenadaGeo);

                if (poligono.tipoUso !=null && poligono.tipoUso.id > 0)
                {
                    cnn.parametros.Add("@idTipoUso", poligono.tipoUso.id);
                }
                if (poligono.toponimio != null && !poligono.toponimio.Equals(""))
                {
                    cnn.parametros.Add("@toponimio", poligono.toponimio);
                }
                if (poligono.areaCalculada != null && poligono.areaCalculada>0)
                {
                    cnn.parametros.Add("@areaCalculada", poligono.areaCalculada);
                }
                if (poligono.areaSolicitada != null && poligono.areaSolicitada>0)
                {
                    cnn.parametros.Add("@areaSolicitada", poligono.areaSolicitada);
                }
                if (poligono.areaRegularizacion != null && poligono.areaRegularizacion>0)
                {
                    cnn.parametros.Add("@areaRegularizada", poligono.areaRegularizacion);
                }
                
                if ( poligono.estado!=null && poligono.estado.id>0)
                {
                    cnn.parametros.Add("@idEstado", poligono.estado.id);
                }
                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }
               
                DataTable dt = cnn.Execute();
                poligono.idPoligono = Convert.ToInt32(dt.Rows[0]["idPoligono"]);

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
        * Obtiene un poligono asociado a una coordenada geográfica.
        */
        public Poligono ObtienePoligono(int idCoordenadaGeo, int idPoligono)
        {
            try
            {
                Poligono poligonoAux = null;
                ParametroGenerico tipoConcesAux = null;
                List<ParametroGenerico> tipoConcesResp = new List<ParametroGenerico>();
                int idPoligonoVar = 0;
                int idPoligonoVarAux = 0;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbPoligono";

                if (idCoordenadaGeo > 0)
                {
                    cnn.parametros.Add("@idCoordenadaGeo", idCoordenadaGeo);
                }
                cnn.parametros.Add("@idPoligono", idPoligono);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        idPoligonoVar = Convert.ToInt32(row["idPoligono"]);
                        if (idPoligonoVar != idPoligonoVarAux)
                        {
                            poligonoAux = new Poligono();
                            poligonoAux.idPoligono = Convert.ToInt32(row["idPoligono"]);
                            if (!row.IsNull("idTipoUso"))
                            {
                                poligonoAux.tipoUso = new ParametroGenerico(Convert.ToInt32(row["idTipoUso"]), row["nombreTipo"].ToString());
                            }
                            if (!row.IsNull("toponimio"))
                            {
                                poligonoAux.toponimio = row["toponimio"].ToString();
                            }

                            if (!row.IsNull("areaCalculada"))
                            {
                                poligonoAux.areaCalculada = Convert.ToSingle(row["areaCalculada"]);
                            }
                            if (!row.IsNull("areaSolicitada"))
                            {
                                poligonoAux.areaSolicitada = Convert.ToSingle(row["areaSolicitada"]);
                            }
                            if (!row.IsNull("areaRegularizada"))
                            {
                                poligonoAux.areaRegularizacion = Convert.ToSingle(row["areaRegularizada"]);
                            }
                            if (!row.IsNull("idEstado"))
                            {
                                poligonoAux.estado = new ParametroGenerico(Convert.ToInt32(row["idEstado"]), row["nombreEstado"].ToString());
                            }
                            
                            poligonoAux.tipoConcesion = new List<ParametroGenerico>();

                        }

                        if (!row.IsNull("IdTipoConcesion"))
                        {
                            tipoConcesAux = new ParametroGenerico(Convert.ToInt32(row["IdTipoConcesion"]), row["nombreTipoConcesion"].ToString());
                            poligonoAux.tipoConcesion.Add(tipoConcesAux);
                        }
                       
                        
                        idPoligonoVarAux = idPoligonoVar;
                    }
                }
                return poligonoAux;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        /**
        * Lista los poligonos asociados a una coordenada geográfica.
        */
        public List<Poligono> ListarPoligono(int idCoordenadaGeo, int idPoligono)
        {
            try
            {
                Poligono poligonoAux = null;
                ParametroGenerico tipoConcesAux = null;
                List<Poligono> resp = new List<Poligono>();
                int idPoligonoVar = 0;
                int idPoligonoVarAux = 0;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbPoligono";
                if (idCoordenadaGeo>0)
                {
                    cnn.parametros.Add("@idCoordenadaGeo", idCoordenadaGeo);
                }
                
                if (idPoligono>0)
                {
                    cnn.parametros.Add("@idPoligono", idPoligono);
                }
                

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        idPoligonoVar = Convert.ToInt32(row["idPoligono"]);
                        if (idPoligonoVar != idPoligonoVarAux)
                        {
                            poligonoAux = new Poligono();
                            poligonoAux.idPoligono = Convert.ToInt32(row["idPoligono"]);
                            if (!row.IsNull("idTipoUso"))
                            {
                                poligonoAux.tipoUso = new ParametroGenerico(Convert.ToInt32(row["idTipoUso"]), row["nombreTipo"].ToString());
                            }
                            if (!row.IsNull("toponimio"))
                            {
                                poligonoAux.toponimio = row["toponimio"].ToString();
                            }
                            if (!row.IsNull("areaCalculada"))
                            {
                                poligonoAux.areaCalculada = Convert.ToSingle(row["areaCalculada"]);
                            }
                            if (!row.IsNull("areaSolicitada"))
                            {
                                poligonoAux.areaSolicitada = Convert.ToSingle(row["areaSolicitada"]);
                            }
                            if (!row.IsNull("areaRegularizada"))
                            {
                                poligonoAux.areaRegularizacion = Convert.ToSingle(row["areaRegularizada"]);
                            }
                            
                            
                            if (!row.IsNull("idEstado"))
                            {
                                poligonoAux.estado = new ParametroGenerico(Convert.ToInt32(row["idEstado"]), row["nombreEstado"].ToString());
                            }
                            poligonoAux.tipoConcesion = new List<ParametroGenerico>();
                            resp.Add(poligonoAux);
                        }

                        if (!row.IsNull("IdTipoConcesion"))
                        {
                            tipoConcesAux = new ParametroGenerico(Convert.ToInt32(row["IdTipoConcesion"]), row["nombreTipoConcesion"].ToString());
                            poligonoAux.tipoConcesion.Add(tipoConcesAux);
                        }
                        idPoligonoVarAux = idPoligonoVar;
                        
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


        //Elimina un registro de Poligono
        public bool EliminarPoligono(int idCoordenadaGeo, int idPoligono, int idSolicitud, int idUsuario)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbPoligono";
                if (idCoordenadaGeo>0)
                {
                    cnn.parametros.Add("@idCoordenadaGeo", idCoordenadaGeo);
                }
                
                cnn.parametros.Add("@idPoligono", idPoligono);
                cnn.parametros.Add("@idSolicitud", idSolicitud);

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

        //GUARDA UNA ASOCIACION DE TIPO DE CONCESION DE UN POLIGONO
        public bool GuardarTipoConcesPoligono(int idPoligono, int idTipoConcesion)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbTipoConcesPoligono";
                cnn.parametros.Add("@idPoligono", idPoligono);
                cnn.parametros.Add("@IdTipoConcesion", idTipoConcesion);
                

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


        //eLIMINA REGISTROS DE TIPOS DE CONCESION ASOCIADOS A UN POLIGONO
        public bool EliminarTipoConcesPoligono(int idPoligono, int idTipoConcesion)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbTipoConcesPoligono";
                cnn.parametros.Add("@idPoligono", idPoligono);
                if (idTipoConcesion > 0)
                {
                    cnn.parametros.Add("@IdTipoConcesion", idTipoConcesion);
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


        public List<Poligono> ListarPoligonoAplicaBco(int idSolicitud)
        {
            try
            {
                Poligono poligonoAux = null;
                ParametroGenerico tipoConcesAux = null;
                List<Poligono> resp = new List<Poligono>();
                int idPoligonoVar = 0;
                int idPoligonoVarAux = 0;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbPoligonoAplicaBco";
                cnn.parametros.Add("@idSolConcesion", idSolicitud);
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        idPoligonoVar = Convert.ToInt32(row["idPoligono"]);
                        if (idPoligonoVar != idPoligonoVarAux)
                        {
                            poligonoAux = new Poligono();
                            poligonoAux.idPoligono = Convert.ToInt32(row["idPoligono"]);
                            if (!row.IsNull("idTipoUso"))
                            {
                                poligonoAux.tipoUso = new ParametroGenerico(Convert.ToInt32(row["idTipoUso"]), row["nombreTipo"].ToString());
                            }

                            if (!row.IsNull("toponimio"))
                            {
                                poligonoAux.toponimio = row["toponimio"].ToString();
                            }

                            if (!row.IsNull("areaCalculada"))
                            {
                                poligonoAux.areaCalculada = Convert.ToSingle(row["areaCalculada"]);
                            }
                            if (!row.IsNull("areaSolicitada"))
                            {
                                poligonoAux.areaSolicitada = Convert.ToSingle(row["areaSolicitada"]);
                            }
                            if (!row.IsNull("areaRegularizada"))
                            {
                                poligonoAux.areaRegularizacion = Convert.ToSingle(row["areaRegularizada"]);
                            }
                            
                            if (!row.IsNull("idEstado"))
                            {
                                poligonoAux.estado = new ParametroGenerico(Convert.ToInt32(row["idEstado"]), row["nombreEstado"].ToString());
                            }
                            poligonoAux.tipoConcesion = new List<ParametroGenerico>();
                            resp.Add(poligonoAux);
                        }

                        if (!row.IsNull("IdTipoConcesion"))
                        {
                            tipoConcesAux = new ParametroGenerico(Convert.ToInt32(row["IdTipoConcesion"]), row["nombreTipoConcesion"].ToString());
                            poligonoAux.tipoConcesion.Add(tipoConcesAux);
                        }
                        idPoligonoVarAux = idPoligonoVar;

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

        public bool GuardarComparacionPoligono(int idSolicitud,int idPoligAntecSector, int idPoligInspTerreno, int codVertice)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbComparacionPoligono";
                cnn.parametros.Add("@idCompPoligono", 0);//INSERCION
                cnn.parametros.Add("@idSolicitud", idSolicitud);
                cnn.parametros.Add("@idPoligonoInspTerreno", idPoligInspTerreno);
                cnn.parametros.Add("@idPoligonoAntecSector", idPoligAntecSector);
                cnn.parametros.Add("@codVertice", codVertice);
                

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

        public ComparacionPoligono obtenerComparacionPoligono(int idSolicitud, int idPoligonoInspTerreno, int idPoligonoAntecSector)
        {
            try
            {

                ComparacionPoligono comPoligono = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbComparacionPoligono";

                cnn.parametros.Add("@idSolicitud", idSolicitud);
                
                if (idPoligonoInspTerreno > 0)
                {
                    cnn.parametros.Add("@idPoligonoInspTerreno", idPoligonoInspTerreno);
                }
                if (idPoligonoAntecSector > 0)
                {
                    cnn.parametros.Add("@idPoligonoAntecSector", idPoligonoAntecSector);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        comPoligono = new ComparacionPoligono();
                        comPoligono.poligonoInspTerreno = new Poligono();
                        comPoligono.poligonoInspTerreno.idPoligono = Convert.ToInt32(row["idPoligonoInspTerreno"]);
                        comPoligono.poligonoInspTerreno.coordenadaPoligono = new CoordenadaGeografica();
                        comPoligono.poligonoInspTerreno.coordenadaPoligono.idCoordenadaGeo = Convert.ToInt32(row["idCoordenadaGeoIT"]);
                        comPoligono.poligonoInspTerreno.coordenadaPoligono.datum = new ParametroGenerico();
                        if (!row.IsNull("idDatumIT"))
                        {
                            comPoligono.poligonoInspTerreno.coordenadaPoligono.datum = new ParametroGenerico(Convert.ToInt32(row["idDatumIT"]), row["nombreDatumIT"].ToString());
                        }
                        if (!row.IsNull("idHusoIT"))
                        {
                            comPoligono.poligonoInspTerreno.coordenadaPoligono.tipoHuso = new ParametroGenerico(Convert.ToInt32(row["idHusoIT"]), row["nombreHusoIT"].ToString());
                        }
                        if (!row.IsNull("idDatumCartaT"))
                        {
                            comPoligono.poligonoInspTerreno.coordenadaPoligono.datum = new ParametroGenerico(Convert.ToInt32(row["idDatumCartaT"]), row["nombreDatumCartaT"].ToString());
                        }
                        if (!row.IsNull("idHusoCartaT"))
                        {
                            comPoligono.poligonoInspTerreno.coordenadaPoligono.tipoHuso = new ParametroGenerico(Convert.ToInt32(row["idHusoCartaT"]), row["nombreHusoCartaT"].ToString());
                        }
                        if (!row.IsNull("toponimioIT"))
                        {
                            comPoligono.poligonoInspTerreno.toponimio =  row["toponimioIT"].ToString();
                        }
                        if (!row.IsNull("idTipoUsoIT"))
                        {
                            comPoligono.poligonoInspTerreno.tipoUso = new ParametroGenerico(Convert.ToInt32(row["idTipoUsoIT"]), row["nombreTipoUsoIT"].ToString());
                        }
                         //
                        comPoligono.poligonoAntecSector = new Poligono();
                        comPoligono.poligonoAntecSector.idPoligono = Convert.ToInt32(row["idPoligonoAntecSector"]);
                        comPoligono.poligonoAntecSector.coordenadaPoligono = new CoordenadaGeografica();
                        comPoligono.poligonoAntecSector.coordenadaPoligono.idCoordenadaGeo = Convert.ToInt32(row["idCoordenadaGeoAS"]);
                        comPoligono.poligonoAntecSector.coordenadaPoligono.datum = new ParametroGenerico();
                        if (!row.IsNull("idDatumAS"))
                        {
                            comPoligono.poligonoAntecSector.coordenadaPoligono.datum = new ParametroGenerico(Convert.ToInt32(row["idDatumAS"]), row["nombreDatumAS"].ToString());
                        }
                        if (!row.IsNull("idHusoAS"))
                        {
                            comPoligono.poligonoAntecSector.coordenadaPoligono.tipoHuso = new ParametroGenerico(Convert.ToInt32(row["idHusoAS"]), row["nombreHusoAS"].ToString());
                        }
                        if (!row.IsNull("idDatumCartaAS2"))
                        {
                            comPoligono.poligonoAntecSector.coordenadaPoligono.datum = new ParametroGenerico(Convert.ToInt32(row["idDatumCartaAS2"]), row["nombreDatumCartaAS2"].ToString());
                        }
                        if (!row.IsNull("idHusoCartaAS2"))
                        {
                            comPoligono.poligonoAntecSector.coordenadaPoligono.tipoHuso = new ParametroGenerico(Convert.ToInt32(row["idHusoCartaAS2"]), row["nombreHusoCartaAS2"].ToString());
                        }
                        if (!row.IsNull("toponimioAS"))
                        {
                            comPoligono.poligonoAntecSector.toponimio = row["toponimioAS"].ToString();
                        }
                        if (!row.IsNull("idTipoUsoAS"))
                        {
                            comPoligono.poligonoAntecSector.tipoUso = new ParametroGenerico(Convert.ToInt32(row["idTipoUsoAS"]), row["nombreTipoUsoAS"].ToString());
                        }
                        if (!row.IsNull("resultadoId"))
                        {
                            comPoligono.estado = new ParametroGenerico(Convert.ToInt32(row["resultadoId"]), row["resultadoNombre"].ToString());
                        }
                        if (!row.IsNull("tipoConcesInspTerreno"))
                        {
                            comPoligono.tipoConcesInspTerreno = row["tipoConcesInspTerreno"].ToString();
                        }
                        if (!row.IsNull("tipoConcesAntecSector"))
                        {
                            comPoligono.tipoConcesAntecSector = row["tipoConcesAntecSector"].ToString();
                        }
                        
                        

                    }
                }
                return comPoligono;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public List<ComparacionPoligono> listarComparacionPoligono(int idSolicitud, int idPoligonoInspTerreno, int idPoligonoAntecSector)
        {
            try
            {
                List<ComparacionPoligono> resp = new List<ComparacionPoligono>();
                ComparacionPoligono comPoligono = null;
               
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbComparacionPoligono";

                cnn.parametros.Add("@idSolicitud", idSolicitud);

                if (idPoligonoInspTerreno > 0)
                {
                    cnn.parametros.Add("@idPoligonoInspTerreno", idPoligonoInspTerreno);
                }
                if (idPoligonoAntecSector > 0)
                {
                    cnn.parametros.Add("@idPoligonoAntecSector", idPoligonoAntecSector);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        comPoligono = new ComparacionPoligono();
                        comPoligono.poligonoInspTerreno = new Poligono();
                        comPoligono.poligonoInspTerreno.idPoligono = Convert.ToInt32(row["idPoligonoInspTerreno"]);
                        comPoligono.poligonoInspTerreno.coordenadaPoligono = new CoordenadaGeografica();
                        comPoligono.poligonoInspTerreno.coordenadaPoligono.idCoordenadaGeo = Convert.ToInt32(row["idCoordenadaGeoIT"]);
                        comPoligono.poligonoInspTerreno.coordenadaPoligono.datum = new ParametroGenerico();
                        if (!row.IsNull("idDatumIT"))
                        {
                            comPoligono.poligonoInspTerreno.coordenadaPoligono.datum = new ParametroGenerico(Convert.ToInt32(row["idDatumIT"]), row["nombreDatumIT"].ToString());
                        }
                        if (!row.IsNull("idHusoIT"))
                        {
                            comPoligono.poligonoInspTerreno.coordenadaPoligono.tipoHuso = new ParametroGenerico(Convert.ToInt32(row["idHusoIT"]), row["nombreHusoIT"].ToString());
                        }
                        if (!row.IsNull("idDatumCartaT"))
                        {
                            comPoligono.poligonoInspTerreno.coordenadaPoligono.datum = new ParametroGenerico(Convert.ToInt32(row["idDatumCartaT"]), row["nombreDatumCartaT"].ToString());
                        }
                        if (!row.IsNull("idHusoCartaT"))
                        {
                            comPoligono.poligonoInspTerreno.coordenadaPoligono.tipoHuso = new ParametroGenerico(Convert.ToInt32(row["idHusoCartaT"]), row["nombreHusoCartaT"].ToString());
                        }
                        if (!row.IsNull("toponimioIT"))
                        {
                            comPoligono.poligonoInspTerreno.toponimio = row["toponimioIT"].ToString();
                        }
                        if (!row.IsNull("idTipoUsoIT"))
                        {
                            comPoligono.poligonoInspTerreno.tipoUso = new ParametroGenerico(Convert.ToInt32(row["idTipoUsoIT"]), row["nombreTipoUsoIT"].ToString());
                        }
                        //
                        comPoligono.poligonoAntecSector = new Poligono();
                        comPoligono.poligonoAntecSector.idPoligono = Convert.ToInt32(row["idPoligonoAntecSector"]);
                        comPoligono.poligonoAntecSector.coordenadaPoligono = new CoordenadaGeografica();
                        comPoligono.poligonoAntecSector.coordenadaPoligono.idCoordenadaGeo = Convert.ToInt32(row["idCoordenadaGeoAS"]);
                        comPoligono.poligonoAntecSector.coordenadaPoligono.datum = new ParametroGenerico();
                        if (!row.IsNull("idDatumAS"))
                        {
                            comPoligono.poligonoAntecSector.coordenadaPoligono.datum = new ParametroGenerico(Convert.ToInt32(row["idDatumAS"]), row["nombreDatumAS"].ToString());
                        }
                        if (!row.IsNull("idHusoAS"))
                        {
                            comPoligono.poligonoAntecSector.coordenadaPoligono.tipoHuso = new ParametroGenerico(Convert.ToInt32(row["idHusoAS"]), row["nombreHusoAS"].ToString());
                        }
                        if (!row.IsNull("idDatumCartaAS2"))
                        {
                            comPoligono.poligonoAntecSector.coordenadaPoligono.datum = new ParametroGenerico(Convert.ToInt32(row["idDatumCartaAS2"]), row["nombreDatumCartaAS2"].ToString());
                        }
                        if (!row.IsNull("idHusoCartaAS2"))
                        {
                            comPoligono.poligonoAntecSector.coordenadaPoligono.tipoHuso = new ParametroGenerico(Convert.ToInt32(row["idHusoCartaAS2"]), row["nombreHusoCartaAS2"].ToString());
                        }
                        if (!row.IsNull("toponimioAS"))
                        {
                            comPoligono.poligonoAntecSector.toponimio = row["toponimioAS"].ToString();
                        }
                        if (!row.IsNull("idTipoUsoAS"))
                        {
                            comPoligono.poligonoAntecSector.tipoUso = new ParametroGenerico(Convert.ToInt32(row["idTipoUsoAS"]), row["nombreTipoUsoAS"].ToString());
                        }
                        if (!row.IsNull("resultadoId"))
                        {
                            comPoligono.estado = new ParametroGenerico(Convert.ToInt32(row["resultadoId"]), row["resultadoNombre"].ToString());
                        }
                        if (!row.IsNull("tipoConcesInspTerreno"))
                        {
                            comPoligono.tipoConcesInspTerreno = row["tipoConcesInspTerreno"].ToString();
                        }
                        if (!row.IsNull("tipoConcesAntecSector"))
                        {
                            comPoligono.tipoConcesAntecSector = row["tipoConcesAntecSector"].ToString();
                        }

                        resp.Add(comPoligono);


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

        public bool EliminarComparacionPoligono(int idPoligonoInspTerreno)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbComparacionPoligono";
                cnn.parametros.Add("@idPoligonoInspTerreno", idPoligonoInspTerreno);

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

        public bool EsCoordenadaUsadaCompPoligono(int idCoordenadaAntecSector)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbCoordenadaUsadaCompPoligono";
                cnn.parametros.Add("@idCoordenadaAntecSector", idCoordenadaAntecSector);
                bool coordenadaComp = false;

                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        coordenadaComp = Convert.ToBoolean(row["enActualComparacion"]);
                    }
                }

                return coordenadaComp;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return true;
            }
        }

    }
}
